using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.Reports;

namespace SalesForce_Project.TestCases.Opportunities
{
    class LV_TMTT0048826_VerifyTASProjectStagesForTASDealsWithFVALOBOnReport:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        ReportHomePage reportHomePage = new ReportHomePage();

        public static string fileTMTT0047923 = "LV_TMTT0048826_VerifyNewTASProjectStagesForTASDealsWithFVALOBOnOppertunityAndEngagementPage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyJobTypesGetsDispalyedOnAddingJobTypeFiltersWhileCreatingReports()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0047923;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 3, 1);
                homePage.SearchUserByGlobalSearchN(adminUserExl);
                extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(adminUserExl), true);
                extentReports.CreateLog("User: " + adminUserExl + " logged in on Lightning View");
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 4, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                //Search Report
                reportHomePage.SearchReportLV("TASProjectStageAuditRecordsReport");
                reportHomePage.EditReportLV();
                reportHomePage.AddReportFilterLV("Record ID", "a0IOx000007F7bRMAS");
                extentReports.CreateStepLogs("Info", "Audit Report found and selected ");
                Assert.AreEqual("a0IOx000007F7bRMAS",reportHomePage.GetReportRecordIDLV("TASProjectStageAuditRecordsReport"));
                extentReports.CreateStepLogs("Passed", "Record ID found on selected Audit Report");


                Assert.IsTrue(reportHomePage.GetRecordtFieldLV("TASProjectStageAuditRecordsReport").Contains("TAS"));
                extentReports.CreateStepLogs("Passed", "Field found on selected Audit Report");

                Assert.AreEqual("Active Opportunity",reportHomePage.GetRecordOldValueLV("TASProjectStageAuditRecordsReport"));
                extentReports.CreateStepLogs("Passed", "Old Value found on selected Audit Report");

                Assert.AreEqual("Closed Opportunity – Deal Died",reportHomePage.GetRecordNewValueLV("TASProjectStageAuditRecordsReport"));
                extentReports.CreateStepLogs("Passed", "New Value found on selected Audit Report");

                Assert.AreEqual("Dimitri Drone", reportHomePage.GetRecordLastModifiedByLV("TASProjectStageAuditRecordsReport"));
                extentReports.CreateStepLogs("Passed", "Modified By User Name found on selected Audit Report");

                Assert.AreEqual("8/21/2025", reportHomePage.GetRecordLastModifiedDateLV("TASProjectStageAuditRecordsReport"));
                extentReports.CreateStepLogs("Passed", "Modified Date found on selected Audit Report");


                reportHomePage.CloseReportLV();
                extentReports.CreateStepLogs("Info", "Audit Report Closed");
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "Admin User: " + adminUserExl + " Loggout ");
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully ");
            }

            catch (Exception e)
            {
                extentReports.CreateStepLogs("Failed", e.Message);
                driver.Quit();
            }
        }
    }
}
