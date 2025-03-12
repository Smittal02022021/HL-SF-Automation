using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
 
namespace SF_Automation.TestCases.Opportunities

{

    class TS25_ValidateAllContractsCreatedInOpportunityAreDisplayedInEngagementUponConversion_Lightning : BaseClass

    {

        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
                OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
                UsersLogin usersLogin = new UsersLogin();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
             AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        AddOppCounterparty counterparty = new AddOppCounterparty();
        AddOpportunityContact addContact = new AddOpportunityContact();

        public static string ERP = "TS25_ValidateAllContracts.xlsx";

        [OneTimeSetUp]

        public void OneTimeSetUp()

        {

            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);

        }

        [Test]

        public void ValidateIsMainContractCheckboxIsTRUE()
        {
            try
            {

                //Get path of Test data file

                string excelPath = ReadJSONData.data.filePaths.testData + ERP;
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

                //Verify the availability of Opportunity under HL Banker list
                string tagOpp = opportunityHome.ValidateOppUnderHLBanker();
                Assert.AreEqual("Opportunities", tagOpp);
                extentReports.CreateLog(tagOpp + " is displayed under HL Banker dropdown ");

                //Verify that choose LOB is displayed after clicking New button
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                string titleOpp = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                Assert.AreEqual("New Opportunity: " + valRecordType, titleOpp);
                extentReports.CreateLog("Page with title: " + titleOpp + " is displayed upon clicking next button ");

                //Calling AddOpportunities function
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string opportunityNumber = addOpportunity.AddOpportunitiesLightning(valJobType, ERP);
                Console.WriteLine("value : " + opportunityNumber);
                extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(ERP);
                Assert.AreEqual("Info", displayedTab);
                extentReports.CreateLog("Tab with name: " + displayedTab + " is displayed upon saving internal deal team members details ");

                //Create External Primary Contact      
                //Update all required fields for Conversion to Engagement
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                counterparty.ClickViewCounterparties();
                opportunityDetails.UpdateReqFieldsForCFConversionL(ERP);
                extentReports.CreateLog("All required details are saved ");
                opportunityDetails.ClickAddCFOppContact();
                addContact.CreateContactL(ERP);

                //Logout of Standard user and login as CAO user
                usersLogin.DiffLightningLogout();
                opportunityHome.SearchOpportunity(opportunityNumber);
                opportunityDetails.UpdateInternalTeamDetails(ERP);
                extentReports.CreateLog("Internal Team members details are saved ");
                string valcaoUser = ReadExcelData.ReadData(excelPath, "Users", 2);
                usersLogin.SearchUserAndLogin(valcaoUser);
                string caoUser = login.ValidateUserLightning();
                Assert.AreEqual(caoUser.Contains(valcaoUser), true);
                extentReports.CreateLog("CAO User: " + caoUser + " is able to login ");

                //Search for added opportunity
                opportunityHome.SearchMyOpportunitiesInLightning(opportunityNumber, valUser);

                //Click on New Contract button
                string titleContract = opportunityDetails.ClickNewContractL();
                Assert.AreEqual("New Contract", titleContract);
                extentReports.CreateLog("Page with title : " + titleContract + " is displayed upon clicking on New Contract button ");

                //Add the contract by not selecting "Is Main Contract" option
                string titleDetail = opportunityDetails.AddContractByNotSelectingIsMainContractL("Test Contract", valContact);
                Assert.AreEqual("Test Contract", titleDetail);
                extentReports.CreateLog("Contract with name: " + titleDetail + " is added ");

                //Validate if Is Main Contract checkbox is checked
                string valueIsMain = opportunityDetails.ValidateIsMainContractL();
                Assert.AreEqual("Is Main Contract checkbox is checked", valueIsMain);
                extentReports.CreateLog(valueIsMain + " of added Contract even it was not selected while saving the details ");

                //Add one more contract by selecting Is Main Contract checkbox
                opportunityDetails.ClickNewContractL();
                string titleDetailIsMainTrue = opportunityDetails.AddContractBySelectingIsMainContractL("Additional Contract", valContact);
                Assert.AreEqual("Additional Contract", titleDetailIsMainTrue);
                extentReports.CreateLog("New Contract with name: " + titleDetailIsMainTrue + " is added ");

                //Validate if Is Main Contract checkbox is checked for new contract
                string valueNewIsMainCon = opportunityDetails.ValidateIsMainContractOfNewContractL();
                Assert.AreEqual("Is Main Contract checkbox is checked", valueNewIsMainCon);
                extentReports.CreateLog(valueNewIsMainCon + " for newly added contract ");

                //Logout of CAO user and login as Standard user
                usersLogin.DiffLightningLogout();

                //Logout of user and validate Admin login              
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
               
                //Search for created opportunity
                opportunityHome.SearchOpportunity(opportunityNumber);

                //update CC and NBC checkboxes 
                opportunityDetails.UpdateOutcomeDetails(ERP);
                opportunityDetails.UpdateNBCApproval();
                extentReports.CreateLog("Conflict Check and NBC approval fields are updated ");

                //Login again as Standard User
                //Login as Financial User and validate the user                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser2 = login.ValidateUserLightning();
                Assert.AreEqual(stdUser2.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser2 + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchMyOpportunitiesInLightning(opportunityNumber, valUser);

                //Requesting for engagement and validate the success message
                opportunityDetails.ClickRequestoEngL();                

                //Log out of Standard User
                usersLogin.DiffLightningLogout();

                //Login as CAO user to approve the Opportunity
                usersLogin.SearchUserAndLogin(valcaoUser);
                string caoUser2 = login.ValidateUserLightning();
                Assert.AreEqual(caoUser2.Contains(valcaoUser), true);
                extentReports.CreateLog("CAO User: " + caoUser2 + " is able to login ");

                //Search for created opportunity
                opportunityHome.SearchMyOpportunitiesInLightning(opportunityNumber, valUser);

                //Approve the Opportunity 
                string status = opportunityDetails.ClickApproveButtonLV2();
                Assert.AreEqual("Approved", status);
                extentReports.CreateLog("Opportunity is approved ");

                //Calling function to convert to Engagement
                opportunityDetails.ClickOppName();
                string engDetails = opportunityDetails.ClickReqToEngagement();
                Assert.AreEqual("Engagement", engDetails);
                extentReports.CreateLog("Opportunity is converted to Engagement after clicking Request To Engagement button ");

                //Validate the Engagement name in Engagement details page
                string engName = engagementDetails.GetEngNumL();
                Assert.AreEqual(opportunityNumber, engName);
                extentReports.CreateLog("Name of Engagement : " + engName + " is similar to Opportunity name ");

                //Validate that contracts added in Opportunity are displayed in Engagement as well
                string contract1 = engagementDetails.Get1stContractNameL();
                Assert.AreEqual(titleDetailIsMainTrue, contract1);
                string contract2 = engagementDetails.Get2ndContractNameL();
                Assert.AreEqual(titleDetail, contract2);
                extentReports.CreateLog(contract1 + " and " + contract2 + " added in Opportunity, are displayed in engagement details page ");

                //Validate Is Main checkbox checked for same contract as it is in Opportunity page
                string IsMainEng = engagementDetails.GetIfIsMainContractCheckedL();
                Assert.AreEqual("Is Main Contract checkbox is checked", IsMainEng);
                extentReports.CreateLog(valueNewIsMainCon + " for 2nd contract added in Opportunity ");
                
                //Validate that contracts are still displayed in Opportunity post conversion to engagement
                string oppContract1 = opportunityDetails.Get1stContractNameL();
                Assert.AreEqual(titleDetailIsMainTrue, oppContract1);
                string oppContract2 = opportunityDetails.Get2ndContractNameL();
                Assert.AreEqual(titleDetail, oppContract2);
                extentReports.CreateLog(oppContract1 + " and " + oppContract2 + " are displayed in Opportunity post conversion to engagement ");

                usersLogin.DiffLightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();
            }

            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.DiffLightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}

