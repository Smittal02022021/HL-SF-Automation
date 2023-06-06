using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages.Contact;

namespace SF_Automation.TestCases.Opportunity
{
    class TMTI0027313_27309_27310_27311_27312_27291_27292_27293_27294_VerifyCFIndustryGroupUpdatedToTECHonOpportunities : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();        
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        ContractDetailsPage contractDetail = new ContractDetailsPage();
        ContactHomePage contactHome = new ContactHomePage();
        ContactDetailsPage contactDetail = new ContactDetailsPage();


        public static string fileTMTI0027313 = "TMTI0027313_VerifyCFIndustryGroupUpdatedToTECHonOpportunities";
       
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ValidateOpportunitiesEngagementsIndstryTypesUpdatedForCF()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0027313;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowIndustryType = ReadExcelData.GetRowCount(excelPath, "IndustryType");
               
                //Login as Standard User profile and validate the user
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                extentReports.CreateLog("Verify the Industry Type is present on Opportunity Home Page ");

                // TMTI0027313	Verify the CF Industry Group(Drop-down) Changes TECH is updated in place of TMT & D&A on Opportunity Home Page 
                opportunityHome.ClickOpportunityTabAdvanceSearch();
                extentReports.CreateLog("User is on Opportunity Home Page ");
                string industryGroupExl = ReadExcelData.ReadData(excelPath, "IndustryType", 1);
                Assert.IsTrue(opportunityHome.IsIndustryTypePresentInDropdownHomePage(industryGroupExl), " Verify " + industryGroupExl + " is present on Opportunity Home Page under Industry Group Dropdown ");
                extentReports.CreateLog(" Industry Group: " + industryGroupExl + " Found in Drpdown on Opportunity Home Page");

                //Search via New Industry Group Type
                //TMTI0027309 Verify User is able to search Opportunity with Industry Group TECH is on Opportunity Home page
                Assert.AreEqual("Record found", opportunityHome.SearchOpportunityWithIndustryType(industryGroupExl));
                extentReports.CreateLog("Opportunity Found with Industry Group: "+ industryGroupExl+ " on Opportunities Home Page ");

                //Creating New Opp with New IG
                //TMTI0027312 Verify the CF Industry Group Changes TECH is updated in place of TMT & D&A While Creating Opportunity 
                opportunityHome.ClickOpportunity();
                string valJobType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 3);
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);
                string value = addOpportunity.AddOpportunities(valJobType, fileTMTI0027313);
                extentReports.CreateLog("Opportunity : " + value + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTMTI0027313);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details  
                string opportunityName = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with Name : " + opportunityName + " is created ");

                //Create External Primary Contact         
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                addOpportunityContact.CreateContact(fileTMTI0027313, valContact, valRecordType, valContactType);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityName + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                //Get IG after save opp
                //TMTI0027310 Verify the User is able to create new opportunity with Industry Group Changes TECH
                Assert.IsTrue(opportunityDetails.IsGetIndustryGroupSaved(industryGroupExl),"Verify Opportunity with Industry Type: TECH is saved ");
                extentReports.CreateLog("Opportunity with Industry Type: "+ industryGroupExl+" is saved ");

                //Update required Opportunity fields for conversion and Internal team details
                opportunityDetails.UpdateReqFieldsForCFConversion(fileTMTI0027313);
                opportunityDetails.UpdateInternalTeamDetails(fileTMTI0027313);

                //Verify the New IF on Edit Opp page
                //TMTI0027311 Verify the CF Industry Group Changes TECH is updated in place of TMT & D&A While Editing Opportunity
                Assert.IsTrue(opportunityDetails.IsIndustryTypePresentInDropdownOppDetailPage(industryGroupExl), "Verify the New Industry Type: TECH is availanle on Edit Opportunity page");
                extentReports.CreateLog("Opportunity with Industry Type: " + industryGroupExl + " is available while Editing Opportunity ");
                
                //Logout of user and validate Admin login
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);
                opportunityDetails.UpdateOutcomeDetails(fileTMTI0027313);
                
                //Login again as Standard User
                usersLogin.SearchUserAndLogin(stdUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(stdUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);
                
                //Requesting for engagement and validate the success message
                string msgSuccess = opportunityDetails.ClickRequestEng();
                Assert.AreEqual(msgSuccess, "Submission successful! Please wait for the approval");
                extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");

                //Log out of Standard User
                usersLogin.UserLogOut();

                //Login as CAO user to approve the Opportunity
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 2));
                string caoUser = login.ValidateUser();
                Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                extentReports.CreateLog("User: " + caoUser + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //Approve the Opportunity 
                opportunityDetails.ClickApproveButton();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityName + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog("Opportunity is approved ");

                //Calling function to convert to Engagement
                opportunityDetails.ClickConvertToEng();
                extentReports.CreateLog("Opportunity: "+ opportunityName + " is Converted into Engagement ");

                //Validate the Engagement name in Engagement details page
                string engagementName = engagementDetails.GetEngName();
                Assert.AreEqual(opportunityName, engagementName);
                extentReports.CreateLog("Name of Engagement : " + engagementName + " is similar to Opportunity name ");

                //Verify Engagement with Industry Type: TECH is converted from Opportunity
                Assert.IsTrue(opportunityDetails.IsGetIndustryGroupSaved(industryGroupExl), "Verify Engagement with Industry Type: TECH is converted from Opportunity ");
                extentReports.CreateLog("Opportunity with Industry Type: " + industryGroupExl + " is converted into Engagement ");

                //TMTI0027294	Verify the CF Industry Group Changes TECH is updated in place of TMT & D&A While Editing Engagement detail Page 
                Assert.IsTrue(engagementDetails.IsIndustryTypePresentInDropdownOppDetailPage(industryGroupExl), "Verify the New Industry Type: TECH is availanle on Edit Engagement page "); ;
                extentReports.CreateLog("New Industry Type: TECH is available on Edit Engagement page ");

                //TMTI0027305 Validate the ERP Last Integration Status on Engagement details page
                string ERPStatusIG = opportunityDetails.GetERPIntegrationStatus();
                Assert.AreEqual("Success", ERPStatusIG);
                extentReports.CreateLog("ERP Last Integration Status in ERP section: " + ERPStatusIG + " is displayed ");

                //TMTI0027291	Verify Industry Group is updated on Contract detailed page after converting Opportunity into Engagement 
                string txtQuickLinkExl = ReadExcelData.ReadData(excelPath, "Engagement", 1);
                engagementDetails.ClickDetailPageQuickLink(txtQuickLinkExl);
                string contractName= engagementDetails.GetContractName();
                extentReports.CreateLog("Contract is generated with Name: "+ contractName+ " on Engagement page ");

                engagementDetails.ClickContractName();
                string txtContractIGCode= contractDetail.GetContractIGCode();
                Assert.IsTrue(industryGroupExl.Contains(txtContractIGCode), "Verify New Industry Group Code is updated on Contract detail page ");
                extentReports.CreateLog("Industry Group Code : " + txtContractIGCode + " is available on Contract Detail page ");

                //TMTI0027292	Verify the CF Industry Group Changes TECH is updated in place of TMT & D&A on Engagement Home Page
                engagementHome.ClickEngagementTabAdvanceSearch();
                extentReports.CreateLog("User is on Engagement Home Page ");
                Assert.IsTrue(opportunityHome.IsIndustryTypePresentInDropdownHomePage(industryGroupExl), " Verify " + industryGroupExl + " is present on Engagement Home Page under Industry Group Dropdown ");
                extentReports.CreateLog(" Industry Group: " + industryGroupExl + " Found in Drpdown on Engagement Home Page ");

                //Search via New Industry Group Type
                //TMTI0027293 Verify User is able to search Engagement with Industry Group TECH is on Engagement home Page 

                Assert.AreEqual("Record found", engagementHome.SearchEngagementsWithIndustryType(industryGroupExl));
                extentReports.CreateLog("Engagement Found with Industry Group: " + industryGroupExl+ " on Engagement Home Page ");


                usersLogin.UserLogOut();
                extentReports.CreateLog("User: " + stdUser + " logged Out ");
                driver.Quit();

            }
            catch (Exception ex)
            {
                extentReports.CreateLog(ex.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
