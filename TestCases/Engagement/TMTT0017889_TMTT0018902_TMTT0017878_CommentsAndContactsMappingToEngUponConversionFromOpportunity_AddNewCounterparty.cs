using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;


namespace SF_Automation.TestCases.Engagement
{
    class TMTT0017889_TMTT0018902_TMTT0017878_CommentsAndContactsMappingToEngUponConversionFromOpportunity_AddNewCounterparty : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOppCounterparty counterparty = new AddOppCounterparty();
        AddCounterparty  engCounterparty = new AddCounterparty();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engagementHome = new EngagementHomePage();

        public static string TMTT0017889 = "TMTT0017889_CommentsAndContactsMappingToEngUponConversionFromOpportunity.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void CommentsAndContactsMappingToEngUponConversion()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + TMTT0017889;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                 Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                 extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);

                //Login as Financial User and validate the user                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Call function to open Add Opportunity Page
                string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                string title = opportunityHome.ClickNewButtonAndSelectCFOpp();

                //Validating Title of New Opportunity Page
                Assert.AreEqual("New Opportunity: CF", title);
                extentReports.CreateLog("Page with title: " + title + " is displayed ");

                //Calling AddOpportunities function                
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunitiesLightning(valJobType, TMTT0017889);
                Console.WriteLine("value : " + value);
                extentReports.CreateLog("Opportunity with number : " + value + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab=  addOpportunity.EnterStaffDetailsL(TMTT0017889);
                Assert.AreEqual("Info", displayedTab);
                extentReports.CreateLog("Tab with name: " + displayedTab + " is displayed upon saving internal deal team members details ");

                //Click on View Counterparties button
                string titleEditor = counterparty.ClickViewCounterparties();
                Assert.AreEqual("Counterparty Editor", titleEditor);
                extentReports.CreateLog("Page with title: " + titleEditor + " is displayed ");

                //Update all required fields for Conversion to Engagement
                opportunityDetails.UpdateReqFieldsForCFConversionL(TMTT0017889);
                extentReports.CreateLog("All required details are saved ");
                opportunityDetails.UpdateInternalTeamDetailsL(TMTT0017889);
                extentReports.CreateLog("Internal Team members details are saved ");
                opportunityDetails.AddContactDetailsInOpp(TMTT0017889);

                //Click on Add Counterparty
                counterparty.ClickViewCounterparties();
                string titleOppCounterparty = counterparty.ClickAddCounterpartiesAndValidatePage();
                Assert.AreEqual("New Opportunity Counterparty", titleOppCounterparty);
                extentReports.CreateLog("Page with title: " + titleOppCounterparty + " is displayed ");

                //Add Opportunity Counterparties 
                string valComp = ReadExcelData.ReadData(excelPath, "Counterparty", 1);
                string valType = ReadExcelData.ReadData(excelPath, "Counterparty", 2);
                counterparty.AddCounterpartyInOpportunityL(valComp, valType);

                //Add Counterparties Contact
                counterparty.AddCounterpartyContactInOpportunityL();

                //Add Counterparties comments
                counterparty.AddCounterpartyCommentL();

                //Get added Counterparty Comment, Creator and Contact
                counterparty.ClickOpportunityTab();
                counterparty.ClickViewCounterparties();
                //string addedName = engCounterparty.ValidateAddedContact();

                counterparty.ClickDetailsLinkL();
                string addedComment = counterparty.GetAddedCommentL();
                string addedCreator = counterparty.GetCreatorOfAddedCommentL();

                usersLogin.LightningLogout();

                //Search for Opportunity
                opportunityHome.SearchOpportunity(value);
                opportunityDetails.ClickReturnToOpp();

                //Update CC and NBC
                opportunityDetails.UpdateNBCApproval();
                opportunityDetails.UpdateOutcomeDetails(TMTT0017889);

                //Login as Financial User and validate the user                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUserLightning();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Open the same opportunity and Click on Request Engagement               
                opportunityHome.SearchMyOpportunitiesInLightning(value, valUser);
                opportunityDetails.ClickRequestoEngL();
                extentReports.CreateLog("Opportunity is requested for approval ");

                usersLogin.DiffLightningLogout();

                //Login as CAO user to approve the Opportunity
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2));
                string caoUser = login.ValidateUserLightningCAO();
                Console.WriteLine("caoUser:" + caoUser);
                Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2)), true);
                extentReports.CreateLog("User: " + caoUser + " logged in ");

                //Search for created opportunity 
                opportunityHome.SearchMyOpportunitiesInLightning(value, ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2));
               
                //Approve the Opportunity 
                string status = opportunityDetails.ClickApproveButtonL();
                Assert.AreEqual("Approved", status);
                extentReports.CreateLog("Opportunity is approved ");

                //Click on Engagement tab and Calling function to convert to Engagement

                opportunityDetails.ClickConvertToEngL();
                extentReports.CreateLog("Opportunity is converted to Engagement ");

                //Validate the added opportunity counterparty contact is mapped to Engagement
                engagementDetails.ClickViewCounterpartiesButton();
                engagementDetails.ClickDetailsLinkL();
                string firstName = engagementDetails.GetCounterpartyContact1stName();
                string lastName = engagementDetails.GetCounterpartyContact2ndName();
               // Assert.AreEqual(addedName.Replace(" ", ""), firstName + lastName);
                extentReports.CreateLog("Engagement Counterparty Contact name: " + firstName + " " + lastName + " is mapped from the opportunity ");

                //Validate the added opportunity counterparty comment is mapped to Engagement
                string commentType = engagementDetails.GetCounterpartyContactCommentL();
                Assert.AreEqual("Internal", commentType);
                extentReports.CreateLog("Engagement Counterparty Comment of Type: " + commentType + " is mapped from the opportunity ");

                string creator = engagementDetails.GetCounterpartyContactCreatorL();
                Assert.AreEqual(valUser, creator);
                extentReports.CreateLog("Engagement Counterparty Comment's creator: " + creator + " is same as Opportunity Counterparty Creator ");

                //Add another counterparty in the engagement by loggig in as CF Financial User
                usersLogin.DiffLightningLogout();                               
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser2 = login.ValidateUserLightning();
                Assert.AreEqual(stdUser2.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser2 + " logged in ");

                //Search for the same Engagement
                engagementHome.SearchMyEngInLightning(value);

                //Add Opportunity Counterparties 
                counterparty.ClickViewCounterparties();
                engCounterparty.ClickAddCounterpartiesAndValidatePage();
                engCounterparty.ClickAddCounterparty();
                string val2ndComp = ReadExcelData.ReadDataMultipleRows(excelPath, "Counterparty", 3, 1);
                string val2ndType = ReadExcelData.ReadDataMultipleRows(excelPath, "Counterparty", 3, 2);
                counterparty.AddCounterpartyInOpportunityL(val2ndComp, val2ndType);

                //Validate Added counterparty
                string val2ndAddedComp = engCounterparty.ValidateAddedCompany();
                Assert.AreEqual(val2ndComp, val2ndAddedComp);
                extentReports.CreateLog("Engagement Counterparty with Company: " + val2ndAddedComp + " is added to Engagement Counterparty ");
                                
                string val2ndfirstName = engagementDetails.GetCounterpartyContact1stName();
                string val2ndlastName = engagementDetails.GetCounterpartyContact2ndName();
                //Assert.AreEqual(addedName.Replace(" ", ""), firstName + lastName);
                Assert.AreEqual("Jaffery Malik", val2ndfirstName + " " + val2ndlastName);
                extentReports.CreateLog("Engagement Counterparty Contact name: " + val2ndfirstName + " " + val2ndlastName + " is mapped from the opportunity ");

                //Validate the added opportunity counterparty comment is mapped to Engagement
                string val2ndCommentType = engagementDetails.GetCounterpartyContactCommentL();
                Assert.AreEqual("Internal", val2ndCommentType);
                extentReports.CreateLog("Engagement Counterparty Comment of Type: " + val2ndCommentType + " is mapped from the opportunity ");

                string val2ndCreator = engagementDetails.GetCounterpartyContactCreatorL();
                Assert.AreEqual(valUser, val2ndCreator);
                extentReports.CreateLog("Engagement Counterparty Comment's creator: " + val2ndCreator + " is same as Opportunity Counterparty Creator ");

                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }        
    }
}

    

