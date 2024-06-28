using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class LV_1_TMTT0039383_VerifyTheFunctionalityOfTimeRecordManagerForFRStandardUserFromFRTimeTrackingGroupsOntheSFLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();

        public static string fileTMTT0039383 = "LV_TMTT0039383_VerifyTheFunctionalityOfTimeRecordManagerForFRStandardUserFromFRTimeTrackingGroupsOntheSFLightningView";

        private string msgSuccess;
        private string selectProject;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyTheFunctionalityOfTimeRecordManagerForFVAStandardUserLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0039383;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowCount = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row <= rowCount; row++)
                {
                    //Login as Standard User profile and validate the user
                    string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    string userGrpNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);
                    //Search CF Financial user by global search
                    homePage.SearchUserByGlobalSearchN(userExl);
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(userExl), true);
                    extentReports.CreateStepLogs("Passed", "FR User: " + userExl + " from Time Tracking Group: " + userGrpNameExl + "  logged in ");

                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    //TMTI0097561 Verify that the FR Financial User is able to access the Time Tracking module in Lightning.
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + userExl);
                    string GetTimeRecordUserNameLV = timeEntry.GetTimeRecordUserNameLV();
                    Assert.AreEqual(GetTimeRecordUserNameLV, userExl, "Verify Logged-in FR User name is displayed on the top of Time Record Manager Page ");
                    extentReports.CreateStepLogs("Passed", "User Name: " + userExl + " is displayed on the top of Time Record Manager Page ");

                    //TMTI0097557	Verify that the FR Financial User is able to add hours in with respect to the project.
                    selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 1);                    
                    string weakDayExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 2); 
                    string weakEndExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 3);

                    msgSuccess= timeEntry.EnterFRUserHoursLV(selectProject, weakDayExl, weakEndExl);
                    Assert.AreEqual("-Time Record Added",msgSuccess);
                    extentReports.CreateStepLogs("Passed", "Project: " + selectProject + " selected on Weekly Entry Matrix ");
                    extentReports.CreateStepLogs("Passed", " Hours entered for FR User with Success Message: " + msgSuccess);

                    //TMTI0097559 Verify that the FR Financial User is able to remove entered hours.
                    timeEntry.RemoveRecordFromDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");

                    usersLogin.ClickLogoutFromLightningView();
                    login.SwitchToClassicView();
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " logged out");
                }
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                timeEntry.RemoveRecordFromDetailLogsLV();
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
