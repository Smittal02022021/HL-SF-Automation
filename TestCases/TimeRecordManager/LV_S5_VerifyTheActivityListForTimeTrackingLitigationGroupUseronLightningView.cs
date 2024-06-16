using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class LV_S5_VerifyTheActivityListForTimeTrackingLitigationGroupUseronLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RefreshButtonFunctionality refreshButton = new RefreshButtonFunctionality();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileTMT111 = "LV_VerifyTheActivityListForUseronLightningView";
                
        private string hoursExl;
        private string selectProject;
        private string txtHours;
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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT111;
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
                    usersLogin.SearchUserAndLogin(userExl);

                    login.SwitchToLightningExperience();
                    string user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(userExl), true);
                    extentReports.CreateStepLogs("Passed", "CF User: " + userExl + " from Time Tracking Group: " + userGrpNameExl + "  logged in ");

                    //homePageLV.ClickAppLauncher();
                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + userExl);
                    string GetTimeRecordUserNameLV = timeEntry.GetTimeRecordUserNameLV();
                    Assert.AreEqual(GetTimeRecordUserNameLV, userExl, "Verify Logged-in FVA User name is displayed on the top of Time Record Manager Page ");
                    extentReports.CreateStepLogs("Passed", "User Name: " + GetTimeRecordUserNameLV + " is displayed on the top of Time Record Manager Page ");

                    //******************
                    selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 1);
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " is on Weekly Entry Matrix Page");
                    timeEntry.SelectProjectWeeklyEntryMatrixLV(selectProject);
                    extentReports.CreateStepLogs("Info", "Project: " + selectProject + " selected on Weekly Entry Matrix ");
                    txtHours = ReadExcelData.ReadDataMultipleRows(excelPath, "WeeklyEntryMatrix", row, 2);
                    timeEntry.LogCurrentDateHoursActivityOptionsLV(txtHours);
                    extentReports.CreateStepLogs("Info", "Project selected and hours entered on Weekly Entry Matrix Page");

                    bool IsActivityListCorrect= timeEntry.ValidateActiviyListDropdownOptionsLV(fileTMT111, userGrpNameExl);
                    Assert.IsTrue(IsActivityListCorrect,"Verify Activty List for logged in user is correct ");
                    extentReports.CreateStepLogs("Passed", "Activty List is correct on Weekly Entry Matrix Page for logged in user:" + userExl);

                    //********************
                    //Click on Time Clock Recorder Tab
                    refreshButton.GoToTimeClockRecorderPageLV();
                    //Check recorder is reset or not
                    refreshButton.ClickResetButtonLV();

                    refreshButton.SelectDropDownProjectAndActivityOptionsLV(selectProject);
                    extentReports.CreateStepLogs("Passed", " Project selected on Time Clock Recorder Page");

                    IsActivityListCorrect = timeEntry.ValidateActiviyListDropdownOptionsLogsPageLV(fileTMT111, userGrpNameExl);
                    Assert.IsTrue(IsActivityListCorrect, "Verify Activty List for logged in user is correct ");
                    extentReports.CreateStepLogs("Passed", "Activty List is correct on Time Clock Recorder Page for logged in user:" + userExl);

                    //******************
                    timeEntry.GoToSummaryLogLV();
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " is on Summary Log Page ");
                    
                    hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 2);                   
                    timeEntry.EnterHoursLogsActivityOptionsLV(selectProject,hoursExl);
                    extentReports.CreateStepLogs("Passed", " Project selected on Summary Logs Page");

                    IsActivityListCorrect = timeEntry.ValidateActiviyListDropdownOptionsLogsPageLV(fileTMT111, userGrpNameExl);
                    Assert.IsTrue(IsActivityListCorrect, "Verify Activty List for logged in user is correct ");
                    extentReports.CreateStepLogs("Passed", "Activty List is correct on Summary Logs Page for logged in user:" + userExl);

                    //******************
                    timeEntry.GoToDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " is on Detail Logs Page ");
                    selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 1);
                    hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 2);

                    timeEntry.EnterHoursLogsActivityOptionsLV(selectProject,hoursExl);
                    extentReports.CreateStepLogs("Passed", " Project selected on Detail Logs Page");

                    IsActivityListCorrect = timeEntry.ValidateActiviyListDropdownOptionsLogsPageLV(fileTMT111, userGrpNameExl);
                    Assert.IsTrue(IsActivityListCorrect, "Verify Activty List for logged in user is correct ");
                    extentReports.CreateStepLogs("Passed", "Activty List is correct on Detail Logs for logged in user:" + userExl);

                    //*******************
                    timeEntry.GoToWeeklyOverviewLV();
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " is on Weekly Overview Page ");
                    selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "WeeklyOverview", row, 1);
                    hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "WeeklyOverview", row, 2);                   
                    timeEntry.EnterHoursLogsActivityOptionsLV(selectProject, hoursExl);
                    extentReports.CreateStepLogs("Passed", " Project selected on Weekly Overview Page");

                    IsActivityListCorrect = timeEntry.ValidateActiviyListDropdownOptionsLogsPageLV(fileTMT111, userGrpNameExl);
                    Assert.IsTrue(IsActivityListCorrect, "Verify Activty List for logged in user is correct ");
                    extentReports.CreateStepLogs("Passed", "Activty List is correct on Weekly Overview Page for logged in user:" + userExl);

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
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}