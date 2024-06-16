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
    class LV_TMTT0011413_VerifyTimeEntriesRoundOffForSupervisorInTimeRecordManagerLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        static string fileTMTT0011413 = "LV_TMTT0011413_VerifyTimeEntriesRoundOffForSupervisorInTimeRecordManagerLightningView";

        private string hoursExl;
        private string activityExl;
        private string selectProjectExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyTheFunctionalityOfTimeRecordManagerForFVASupervisorUserLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0011413;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                ////Click on the new title rate sheet name               
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
                    extentReports.CreateStepLogs("Passed", "Supervisor User: " + userExl + " from Time Tracking Group: " + userGrpNameExl + "  logged in ");

                    //homePageLV.ClickAppLauncher();
                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    string moduleNameExl = ReadExcelData.ReadData(excelPath, "ModuleName", 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + userExl);

                    //Select Staff Member from the list
                    string staffNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 1);
                    timeEntry.SelectStaffMemberLV(staffNameExl);
                    string staffName = timeEntry.GetSelectedStaffNameLV();
                    Assert.AreEqual(staffNameExl, staffName);
                    extentReports.CreateStepLogs("Passed", "Staff : " + staffName + " is Selected from list ");
                    //Click time record manager                
                    string timeRecordManagerTitle = timeEntry.GetTimeRecordManagerTitleLV();
                    string timeRecordManagerTitleExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 5);
                    Assert.AreEqual(timeRecordManagerTitleExl, timeRecordManagerTitle);
                    extentReports.CreateStepLogs("Passed", "Time Record Manager Title: " + timeRecordManagerTitle + " is displayed upon click of Time Record Manager tab ");

                    //TMTI0024496:Verify the time entries should be rounded off to 1 decimal place automatically

                    //Weekly Entry Matrix entry
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    extentReports.CreateStepLogs("Info", "User: " + user + " is on Weekly Entry Matrix Page");
                    selectProjectExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 1);
                    hoursExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 2);
                    activityExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 3);
                    timeEntry.EnterWeeklyEntryMatrixLV(selectProjectExl, hoursExl, activityExl);

                    string sundayTimeRoundOff = timeEntry.GetSundayTimeEntryLV();
                    string sundayTimeRoundOffExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 4);
                    Assert.AreEqual(sundayTimeRoundOffExl, sundayTimeRoundOff);
                    extentReports.CreateStepLogs("Passed", "Time Entry Value:: " + hoursExl + " is rounded off to Value: " + sundayTimeRoundOff + " is updated from more than one decimal value to one decimal value ");

                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Info", "Time Entry Deleted");

                    // Enter Summary logs entry
                    timeEntry.GoToSummaryLogLV();
                    extentReports.CreateStepLogs("Info", "User: " + user + " is on Summary Log Page ");
                    selectProjectExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 1);
                    hoursExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 2);
                    activityExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 3);
                    timeEntry.EnterSummaryLogsHoursLV(selectProjectExl, activityExl, hoursExl);

                    string summaryLogTimeEntryRoundOff = timeEntry.GetSummaryLogsTimeEntryLV();
                    string summaryLogTimeEntryRoundOffExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 4);
                    Assert.AreEqual(summaryLogTimeEntryRoundOffExl, summaryLogTimeEntryRoundOff);
                    extentReports.CreateStepLogs("Passed", "Summary Log Value:: " + hoursExl + " is Rounded Off to Value: " + summaryLogTimeEntryRoundOff + " is updated from more than one decimal value to one decimal value ");

                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Info", "Time Entry Deleted");

                    //Enter Detail logs entry
                    timeEntry.GoToDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " is on Detail Logs Page ");
                    selectProjectExl = ReadExcelData.ReadData(excelPath, "DetailLogs", 1);
                    hoursExl = ReadExcelData.ReadData(excelPath, "DetailLogs", 2);
                    activityExl = ReadExcelData.ReadData(excelPath, "DetailLogs", 3);
                    timeEntry.EnterDetailLogsHoursLV(selectProjectExl, activityExl, hoursExl);

                    string detailLogTimeEntryRoundOff = timeEntry.GetDetailLogsTimeEntryLV();
                    string detailLogTimeEntryRoundOffExl = ReadExcelData.ReadData(excelPath, "DetailLogs", 4);
                    Assert.AreEqual(detailLogTimeEntryRoundOff, detailLogTimeEntryRoundOffExl);
                    extentReports.CreateStepLogs("Passed", "Detail Log Value:: " + hoursExl + " is Rounded Off to Value: " + detailLogTimeEntryRoundOff + " is updated from more than one decimal value to one decimal value ");

                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Info", "Time Entry Deleted");

                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " logged out");
                    usersLogin.UserLogOut();
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Browser Closed");
                }
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