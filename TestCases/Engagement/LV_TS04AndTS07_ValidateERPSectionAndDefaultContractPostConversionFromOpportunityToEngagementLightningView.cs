using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesForce_Project.TestCases.Engagement
{
    class LV_TS04AndTS07_ValidateERPSectionAndDefaultContractPostConversionFromOpportunityToEngagementLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        RandomPages pages = new RandomPages();
        LegalEntityDetail entityDetails = new LegalEntityDetail();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        ContactHomePage contactHome = new ContactHomePage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();

        public static string ERPTS04 = "LV_TS04AndTS07_ValidateERPSection";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            InitializeZoom();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ValidateERPSectionLV()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + ERPTS04;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                login.LoginApplication();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                //Calling functions to validate for all LOBs operation
                int rowUsers = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row <= rowUsers; row++)
                {
                    string valUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 25);

                    usersLogin.SearchUserAndLogin(valUserExl);
                    login.SwitchToLightningExperience();
                    string stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(valUserExl), true);
                    extentReports.CreateStepLogs("Passed", "User: " + valUserExl + " logged in on Lightning View");

                    homePageLV.ClickAppLauncher();
                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                    //Validating Title of New Opportunity Page
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                    extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                    extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType);
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, ERPTS04);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(ERPTS04);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Passed", "User is on Opportunity detail " + displayedTab + " tab ");

                    ////Validating Opportunity details  
                    string oppNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                    extentReports.CreateStepLogs("Passed", "Opportunity with number : " + oppNumber + " is created ");

                    //Create External Primary Contact      
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);

                    addOpportunityContact.CickAddOpportunityContact(valRecordType);
                    addOpportunityContact.CreateContactL2(ERPTS04);
                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                    //Fetch values of Opportunity Name, Client, Subject and Job Type
                    string oppName = opportunityDetails.GetOpportunityNameL();
                    string clientName = opportunityDetails.GetClientLV();
                    string subjectName = opportunityDetails.GetSubjectLV();
                    string jobType = opportunityDetails.GetJobTypeLV();

                    if (valRecordType == "CF")
                    {
                        opportunityDetails.UpdateReqFieldsForCFConversionLV2(ERPTS04);
                    }
                    if (valRecordType == "FR")
                    {
                        opportunityDetails.UpdateReqFieldsForFRConversionLV(ERPTS04);
                    }
                    if (valRecordType == "FVA")
                    {
                        opportunityDetails.UpdateReqFieldsForFVAConversionLV(ERPTS04);
                    }
                    extentReports.CreateStepLogs("Info", "Required fields are entered for LOB: " + valRecordType);
                    randomPages.CloseActiveTab(oppName);
                    extentReports.CreateStepLogs("Info", "Opportunity tab is closed");
                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "User: " + valUserExl + " logged out");

                    //------Only System Admin can see the ERP Section on Opportunity Detail page//
                    string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 3);
                    usersLogin.SearchUserAndLogin(adminUserExl);
                    login.SwitchToLightningExperience();
                    string userName = login.ValidateUserLightningView();
                    Assert.AreEqual(userName.Contains(adminUserExl), true);
                    extentReports.CreateLog("System Administrator User: " + adminUserExl + " logged in on Lightning View");
                    homePageLV.ClickAppLauncher();
                    homePageLV.SelectApp(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    opportunityHome.SearchOpportunityInLightning(oppName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                    
                    opportunityDetails.UpdateOutcomeDetailsLV();
                    extentReports.CreateStepLogs("Info", "Conflict Check Details Provided ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(ERPTS04);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityL();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab("Internal Team"); 
                    
                    extentReports.CreateStepLogs("Info", "Opportunity tab is closed");

                    //Validate ERP section details------

                }
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}

