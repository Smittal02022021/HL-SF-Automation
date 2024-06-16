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
    class LV_TMTI0045472_VerifyBetaUserWithDifferentTitleIsAbletoEnterHoursfromTimeRecordmanagerLightningView:BaseClass
    {
        //TMTT0020334
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        LVHomePage homePageLV = new LVHomePage();

        private string valueActivity;
        private string valueEnteredHours;
        private string valueActivityInDetailLogs;
        private string valueEnteredHoursInDetailLogs;
        private string valueProjectOrEngagmentSummaryLogs;
        private double actualSummaryLogsEnteredHours;
        private string actualDetailLogsEnteredHours;
        private string selectProjectExl;
        private string hoursExl;
        private string activityExl;
        private string textMessage;

        public static string fileTMTI0045472 = "LV_TMTI0045472_VerifyBetaUserWithDifferentTitleIsAbletoEnterHoursfromTimeRecordmanager";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyBetaUserWithDifferentTitleIsAbletoEnterHoursfromTimeRecordmanager()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0045472;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowUser = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row <= rowUser; row++)
                {
                    string userNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    string userGrpNameExl= ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);
                    string userTitleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 3);
                    usersLogin.SearchUserAndLogin(userNameExl);

                    login.SwitchToLightningExperience();
                    string user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(userNameExl), true);
                    extentReports.CreateStepLogs("Passed", "Standard User: " + userNameExl + " from Time Tracking Group: " + userGrpNameExl + " with Title: " + userTitleExl + " logged in ");
                    //homePageLV.ClickAppLauncher();

                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + userNameExl);

                    string timeRecordManagerTitle = timeEntry.GetTimeRecordManagerTitleLV();
                    string timeRecordManagerTitleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs",row, 4);
                    Assert.AreEqual(timeRecordManagerTitleExl, timeRecordManagerTitle);
                    extentReports.CreateLog("Time Record Manager Title: " + timeRecordManagerTitle + " is displayed upon click of Time Record Manager tab ");
                    //------------------------------------------------
                    //Enter time under weekly time matrix
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    extentReports.CreateStepLogs("Info", "User is on Weekly Entry Matrix Page");
                    selectProjectExl = ReadExcelData.ReadDataMultipleRows(excelPath, "WeeklyEntryMatrix", row, 1);
                    hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "WeeklyEntryMatrix",row, 2);
                    activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "WeeklyEntryMatrix", row, 3);
                    timeEntry.SelectProjectWeeklyEntryMatrixLV(selectProjectExl);
                    extentReports.CreateStepLogs("Info", "Project: " + selectProjectExl + " selected on Weekly Entry Matrix ");
                    timeEntry.LogCurrentDateHoursLV(hoursExl);
                    extentReports.CreateStepLogs("Passed", "Hours entered on Weekly Entry Matrix Page");

                    timeEntry.GoToSummaryLogLV();
                    valueProjectOrEngagmentSummaryLogs = timeEntry.GetProjectOrEngagementValueLV();
                    Assert.IsTrue(selectProjectExl.Contains(valueProjectOrEngagmentSummaryLogs));
                    extentReports.CreateStepLogs("Passed", "Project Or Engagement:" + valueProjectOrEngagmentSummaryLogs + " is displayed upon entering time entry from Weekly Entry Metrix on Summary Logs");

                    valueActivity = timeEntry.GetSelectedActivityOnSummaryLogsLV();
                    Assert.AreEqual(activityExl, valueActivity);
                    extentReports.CreateStepLogs("Passed", "Selected Activity: " + valueActivity + " is displayed upon entering time entry from Weekly Entry Metrix on Summary Logs ");

                    //Verify entered hours 
                    actualSummaryLogsEnteredHours = timeEntry.GetEnteredHoursInSummaryLogValueLV();
                    //valueEnteredHoursExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 2);
                    Assert.AreEqual(8, actualSummaryLogsEnteredHours);//hoursExl
                    extentReports.CreateStepLogs("Passed", "Entered Hours: " + valueEnteredHours + " is displayed upon entering time entry from Weekly Entry Metrix on Summary Logs ");

                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateLog("Deleted time entry successfully after verification ");
                    //--------------------------------------------------

                    //Enter on Summary Logs
                    timeEntry.GoToSummaryLogLV();
                    extentReports.CreateStepLogs("Passed", "User: " + userNameExl + " is on Summary Log Page ");
                    activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 3);
                    hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 2);

                    textMessage = timeEntry.EnterSummaryLogsHoursLV(selectProjectExl, activityExl, hoursExl);
                    Assert.AreEqual(textMessage, "Time Record Added");
                    extentReports.CreateStepLogs("Passed", " Hours entered on Summary Logs Page with Success Message: " + textMessage);

                    //Verify project or engagement on Summary Logs
                    valueProjectOrEngagmentSummaryLogs = timeEntry.GetProjectOrEngagementValueLV();
                    Assert.IsTrue(selectProjectExl.Contains(valueProjectOrEngagmentSummaryLogs));
                    extentReports.CreateStepLogs("Passed", "Project Or Engagement:" + valueProjectOrEngagmentSummaryLogs + " is displayed upon entering time entry on Summary Logs");
                   
                    //Verify selected activity on Summary Logs
                    valueActivity = timeEntry.GetSelectedActivityOnSummaryLogsLV();
                    Assert.AreEqual(activityExl, valueActivity);
                    extentReports.CreateStepLogs("Passed", "Selected Activity: " + valueActivity + " is displayed upon entering time entry on Summary Logs ");

                    //Verify entered hours 
                    actualSummaryLogsEnteredHours = timeEntry.GetEnteredHoursInSummaryLogValueLV();
                    Assert.AreEqual(8, actualSummaryLogsEnteredHours);//hoursExl
                    extentReports.CreateStepLogs("Passed", "Entered Hours: " + valueEnteredHours + " is displayed upon entering time entry on Summary Logs ");

                    //Delete Time Entry
                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateLog("Deleted time entry successfully after verification ");
                    //-----------------------------------------------------------------------------------

                    // Enter Detail logs
                    timeEntry.GoToDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "User: " + userNameExl + " is on Detail Logs Page ");
                    selectProjectExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 1);
                    hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 2);
                    activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 3);

                    textMessage = timeEntry.EnterDetailLogsHoursLV(selectProjectExl, activityExl, hoursExl);
                    Assert.AreEqual(textMessage, "Time Record Added");
                    extentReports.CreateStepLogs("Passed", " Hours entered on Detail Logs Page with Success Message: " + textMessage);

                    //Verify project or engagement on Detail Logs 
                    string valueProjectOrEngagmentInDetailLogs = timeEntry.GetProjectOrEngagementValueLV();
                    Assert.IsTrue(selectProjectExl.Contains(valueProjectOrEngagmentInDetailLogs));
                    extentReports.CreateStepLogs("Passed", "Project Or Engagement:" + valueProjectOrEngagmentSummaryLogs + " is displayed upon entering time entry on Detail Logs");

                    //Verify selected activity on Detail Logs 
                    valueActivityInDetailLogs = timeEntry.GetSelectedActivityOnDetailLogsLV();
                    Assert.AreEqual(activityExl, valueActivityInDetailLogs);
                    extentReports.CreateStepLogs("Passed", "Selected Activity: " + valueActivity + " is displayed upon entering time entry on Detail Logs ");

                    //Verify entered hours on Detail Logs 
                    actualDetailLogsEnteredHours = timeEntry.GetEnteredHoursInDetailLogsLV();
                    Assert.AreEqual(hoursExl, actualDetailLogsEnteredHours);
                    extentReports.CreateStepLogs("Passed", "Entered Hours: " + valueEnteredHoursInDetailLogs + " is displayed upon entering time entry on Detail Logs ");
                    
                    timeEntry.RemoveRecordFromDetailLogsLV();
                    extentReports.CreateLog("Deleted record entry successfully from Detail Logs ");

                    //--------------------------------------------------------

                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "User: " + userNameExl + " logged out");
                }

                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                timeEntry.DeleteTimeEntryLV();
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}