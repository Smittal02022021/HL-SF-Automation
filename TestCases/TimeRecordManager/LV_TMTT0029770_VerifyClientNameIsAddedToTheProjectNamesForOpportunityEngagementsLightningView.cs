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
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                string TimeRecordManagerUser = login.ValidateUser();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string userExl = ReadExcelData.ReadData(excelPath, "Users",1);
                //usersLogin.SearchUserAndLogin(userExl);
                homePage.SearchUserByGlobalSearchN(userExl);
                extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();

                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(userExl), true);
                
               // homePageLV.ClickAppLauncher();
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
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
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
