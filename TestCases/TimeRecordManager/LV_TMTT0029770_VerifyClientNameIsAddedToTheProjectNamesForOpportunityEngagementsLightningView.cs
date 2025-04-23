using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class LV_TMTT0029770_VerifyClientNameIsAddedToTheProjectNamesForOpportunityEngagementsLightningView : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();
        RefreshButtonFunctionality refreshButton = new RefreshButtonFunctionality();
        TimeRecorderFunctionalities timeRecorder = new TimeRecorderFunctionalities();
        LVHomePage homePageLV = new LVHomePage();
        LVHomePage lvHomePage = new LVHomePage();

        public static string fileTMTT0029770 = "LV_TMTT0029770_VerifyUserCanSearchProjectWithNameOnTimeRecordManager";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyClientNameIsAddedToTheProjectNamesForOpportunityEngagementsLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0029770;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                //Validate user logged in
                Assert.AreEqual(driver.Url.Contains("lightning"), true);
                extentReports.CreateStepLogs("PAssed", "Admin User is able to login into SF");

                //Select HL Banker app
                try
                {
                    lvHomePage.SelectAppLV("HL Banker");
                }
                catch(Exception)
                {
                    lvHomePage.SelectAppLV1("HL Banker");
                }

                //Login as Standard User and validate the user
                string userExl = ReadExcelData.ReadData(excelPath, "Users",1);
                lvHomePage.SearchUserFromMainSearch(userExl);

                //Verify searched user
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, userExl + " | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User " + userExl + " details are displayed ");

                //Login as CF Financial user
                lvHomePage.UserLogin();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }
                Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(userExl));
                extentReports.CreateLog("User: " + userExl + " logged in on Lightning View");

                //Select App
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                //Select Module
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + userExl);
 
                int rowSearchValue = ReadExcelData.GetRowCount(excelPath, "SearchValue");
                for (int row = 2; row <= rowSearchValue; row++)
                {
                    //Existing Engagement with Deal team member of Logged in user
                    //Verify that the user is able to search with Project Name on the time record manager
                    //TMTI0069000-Verify that selecting the project name and removing it takes the user back to the search bar to search project.

                    string selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "SearchValue", row, 1);

                    //Weekly Entry Matrix
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    Assert.IsTrue(timeEntry.IsProjectSelectedLV(selectProject), "Verify that the user is able to search with " + selectProject + " on the Weekly Entry Matrix ");
                    extentReports.CreateStepLogs("Passed", "User is able to search and select the Project with value:" + selectProject + " on Weekly Entry Matrix ");

                    //Verify after selecting the project name changed the search bar to old select project drop-down. 
                    Assert.IsTrue(timeEntry.IsComboSelectProjectDisplayedLV(), "Verify after selecting the project, the search bar to changed to old Select Project Drop-Down");
                    extentReports.CreateStepLogs("Passed", "After selecting the project, the search bar to changed to old Select Project Drop-Down on Weekly Entry Matrix ");

                    //Time Clock Recorder
                    refreshButton.GoToTimeClockRecorderPageLV();
                    //Check recorder is reset or not
                    refreshButton.ClickResetButtonLV();
                    Assert.IsTrue(timeEntry.IsProjectSelectedLV(selectProject), "Verify that the user is able to search with " + selectProject + " on the Time Clock Recorder ");
                    extentReports.CreateStepLogs("Passed", "User is able to search and select the Project with value:" + selectProject + " on Time Clock Recorder ");

                    //Verify after selecting the project name changed the search bar to old select project drop-down. 
                    Assert.IsTrue(timeEntry.IsComboSelectProjectDisplayedLV(), "Verify after selecting the project, the search bar to changed to old Select Project Drop-Down");
                    extentReports.CreateStepLogs("Passed", "After selecting the project, the search bar to changed to old Select Project Drop-Down on Time Clock Recorder ");

                    //Summary Logs
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    timeEntry.GoToSummaryLogLV();
                    Assert.IsTrue(timeEntry.IsProjectSelectedLV(selectProject), "Verify that the user is able to search with " + selectProject + " on the Summary Logs ");
                    extentReports.CreateStepLogs("Passed", "User is able to search and select the Project with value:" + selectProject + " on Summary Logs ");

                    //Verify after selecting the project name changed the search bar to old select project drop-down. 
                    Assert.IsTrue(timeEntry.IsComboSelectProjectDisplayedLV(), "Verify after selecting the project, the search bar to changed to old Select Project Drop-Down");
                    extentReports.CreateStepLogs("Passed", "After selecting the project, the search bar to changed to old Select Project Drop-Down  on Summary Logs ");

                    //Detail Logs 
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    timeEntry.GoToDetailLogsLV();
                    Assert.IsTrue(timeEntry.IsProjectSelectedLV(selectProject), "Verify that the user is able to search with " + selectProject + " on the Detail Logs ");
                    extentReports.CreateStepLogs("Passed", "User is able to search and select the Project with value:" + selectProject + " on Detail Logs ");

                    //Verify after selecting the project name changed the search bar to old select project drop-down. 
                    Assert.IsTrue(timeEntry.IsComboSelectProjectDisplayedLV(), "Verify after selecting the project, the search bar to changed to old Select Project Drop-Down");
                    extentReports.CreateStepLogs("Passed", "After selecting the project, the search bar to changed to old Select Project Drop-Down on Detail Logs ");

                    //Weekly Overview
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    timeEntry.GoToWeeklyOverviewLV();
                    Assert.IsTrue(timeEntry.IsProjectSelectedLV(selectProject), "Verify that the user is able to search with " + selectProject + " on the Weekly Overview ");
                    extentReports.CreateStepLogs("Passed", "User is able to search and select the Project with value:" + selectProject + " on Weekly Overview ");

                    //Verify after selecting the project name changed the search bar to old select project drop-down. 
                    Assert.IsTrue(timeEntry.IsComboSelectProjectDisplayedLV(), "Verify after selecting the project, the search bar to changed to old Select Project Drop-Down");
                    extentReports.CreateStepLogs("Passed", "After selecting the project, the search bar to changed to old Select Project Drop-Down on Weekly Overview ");
                }
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "User: " + userExl + " logged out");

                //TC - End
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "Admin User Logged Out from SF Lightning View. ");

                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.Quit();
            }
        }
    }
}
