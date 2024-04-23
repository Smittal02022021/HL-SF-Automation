using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.Engagement;

namespace SF_Automation.TestCases.Engagements
{
    class LV_TS06_ValidateERPSubmittedAndERPLastIntegrationResponsePostManualSyncEngagementLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engDetails = new EngagementDetailsPage();

        public static string fileERPTS02 = "LV_TS05_ValidateERPSection";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void PostEngagementManualSyncLV()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + fileERPTS02;
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");              
                login.LoginApplication();                  
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 3);
                usersLogin.SearchUserAndLogin(adminUserExl);
                login.SwitchToLightningExperience();
                string userName = login.ValidateUserLightningView();
                Assert.AreEqual(userName.Contains(adminUserExl), true);
                extentReports.CreateLog("System Administrator User: " + adminUserExl + " logged in on Lightning View");
                homePageLV.ClickAppLauncher();
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectApp(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 2);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                //Search for an opportunity
                string EngName = "FGIC";
                engHome.SearchEngagementInLightning(EngName);
                extentReports.CreateLog("Matching record is displayed ");

                //Validating Opportunity details page 
                string EngagementNumber = engDetails.GetEngagementNumberL();
                Assert.IsNotNull(EngagementNumber);
                extentReports.CreateLog("Engagement with number : " + EngagementNumber + " is displayed ");

                //Get ERP Submitted to Sync, Status, ERP Update DFF checkbox and ERP Last Integration Response Date
                string ERPSubmitted = randomPages.GetERPSubmittedToSyncLV();
                extentReports.CreateLog("Engagement ERP Submitted to Sync before update is: " + ERPSubmitted + " ");

                string ERPResDate = randomPages.GetERPLastIntegrationResponseDateLV();
                extentReports.CreateLog("Engagement ERP Last Integration Response Date in ERP section: " + ERPResDate + " is displayed ");

                string ERPStatus = randomPages.GetERPLastIntegrationStatusLV();
                extentReports.CreateLog("Engagement ERP Last Integration Status in ERP section: " + ERPStatus + " is displayed ");

                //-----Schedule ERP Submitted to Sync manually, validate ERP Update DFF checkbox, ERP Sync Date, Status and Last Integration Status -----
                randomPages.UpdateERPSyncManuallyInlineLV();
                string ERPSubmittedPostSync = randomPages.GetERPSubmittedToSyncLV();
                Assert.AreNotEqual(ERPSubmitted, ERPSubmittedPostSync);
                extentReports.CreateLog("Engagement ERP Submitted to Sync Old:: " + ERPSubmitted+ " New:: "+ ERPSubmittedPostSync + " is updated post scheduling ERP sync ");

                string ERPResDatePostSync = randomPages.GetERPLastIntegrationResponseDateLV();
                Assert.AreNotEqual(ERPResDate, ERPResDatePostSync); 
                extentReports.CreateLog("Engagement ERP Last Integration Response Date in ERP section: " + ERPResDatePostSync + " is displayed post ERP sync ");

                string ERPStatusPostSync = randomPages.GetERPLastIntegrationStatusLV();
                Assert.AreEqual("Success", ERPStatusPostSync); 
                extentReports.CreateLog("Engagement ERP Last Integration Status in ERP section: " + ERPStatusPostSync + " is displayed post ERP sync ");

                randomPages.CloseActiveTab(EngName);
                extentReports.CreateStepLogs("Info", "Engagement is closed");
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " logged out");
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}