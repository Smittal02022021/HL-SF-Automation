using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.TimeRecordManager
{
    class LV_TMTT0002649_TMTT0029770_VerifyTimeEntriesInTimeRecordManagerRoundOffToFirstDecimalPlaceOfAnHourLightningView : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        LVHomePage homePageLV = new LVHomePage();
      
        public static string TMTT0002649 = "LV_TMTT0002649_TimeRecordManager_VerifyTimeEntriesareRoundOffToFirstDecimalPlace";
        private string txtHours;
        private string txtActivity;
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
        public void VerifyTimeEntriesInTimeRecordManager_RoundOffToFirstDecimalPlaceOfAnHour()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + TMTT0002649;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();
                string TimeRecordManagerUser = login.ValidateUser();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string userExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(userExl);
                login.SwitchToClassicView();
                string user = login.ValidateUser();
                Assert.AreEqual(user.Contains(userExl), true);
                extentReports.CreateStepLogs("Passed", "User: " + user + " logged in ");
                login.SwitchToLightningExperience();
                extentReports.CreateStepLogs("Info", "User: " + user + " Switched to Lightning View ");
                homePageLV.ClickAppLauncher();
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectApp(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + user);
                
                //Click time record manager                
                string timeRecordManagerTitle = timeEntry.GetTimeRecordManagerTitleLV();
                string timeRecordManagerTitleExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 5);
                Assert.AreEqual(timeRecordManagerTitleExl, timeRecordManagerTitle);
                extentReports.CreateStepLogs("Passed", "Time Record Manager Title: " + timeRecordManagerTitle + " is displayed upon click of Time Record Manager tab ");

                //TMTI0003744:Verify the scenario, when user provide time entries in time record manager, it should rounded off to the first decimal place of the hour
                //TMTI0068979_68980_68982_68984_68986_Entering hours with updated Project name(Including Project Name,ClientName,LOB,Eng Number)
                // User is entering hours with Activity selection in all below section 
                //Enter weekly entry matrix
                
                timeEntry.GoToWeeklyEntryMatrixLV();
                extentReports.CreateStepLogs("Info", "User: " + user + " is on Weekly Entry Matrix Page");
                selectProject = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 1);
                txtHours = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 2);
                txtActivity = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 3);                
                timeEntry.EnterWeeklyEntryMatrixLV(selectProject, txtHours, txtActivity);
                
                string sundayTimeRoundOff = timeEntry.GetSundayTimeEntryLV();
                string sundayTimeRoundOffExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 4);
                Assert.AreEqual(sundayTimeRoundOffExl, sundayTimeRoundOff);
                extentReports.CreateStepLogs("Passed", "Time Entry Value:: "+ txtHours+" is rounded off to Value: " + sundayTimeRoundOff + " is updated from more than one decimal value to one decimal value ");
                
                timeEntry.DeleteTimeEntryLV();
                extentReports.CreateStepLogs("Info", "Time Entry Deleted");

                //Clear time record from weekly entry matrix
                string defaultSelectedProjectOption = timeEntry.GetDefaultSelectedProjectOptionLV();
                string defaultSelectedProjectOptionExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 6);
                Assert.AreEqual(defaultSelectedProjectOptionExl, defaultSelectedProjectOption);
                extentReports.CreateStepLogs("Passed", "Time Entry Project Default: " + defaultSelectedProjectOption + " is displayed upon deletion of time entry ");

                // Enter summary logs entry
                timeEntry.GoToSummaryLogLV();
                extentReports.CreateStepLogs("Info", "User: " + user + " is on Summary Log Page ");
                selectProject = ReadExcelData.ReadData(excelPath, "SummaryLogs", 1);
                txtHours = ReadExcelData.ReadData(excelPath, "SummaryLogs", 2);
                txtActivity = ReadExcelData.ReadData(excelPath, "SummaryLogs",3);                
                //Enter time under Summary Logs
                timeEntry.EnterSummaryLogsHoursLV(selectProject, txtActivity, txtHours);

                string summaryLogTimeEntryRoundOff = timeEntry.GetSummaryLogsTimeEntryLV();
                string summaryLogTimeEntryRoundOffExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 4);
                Assert.AreEqual(summaryLogTimeEntryRoundOffExl, summaryLogTimeEntryRoundOff);
                extentReports.CreateStepLogs("Passed", "Summary Log Value:: "+ txtHours + " is Rounded Off to Value: " + summaryLogTimeEntryRoundOff + " is updated from more than one decimal value to one decimal value ");
                
                timeEntry.DeleteTimeEntryLV();
                extentReports.CreateStepLogs("Info", "Time Entry Deleted");

                //Enter detail logs entry
                timeEntry.GoToDetailLogsLV();
                extentReports.CreateStepLogs("Passed", "User: " + user + " is on Detail Logs Page ");
                selectProject = ReadExcelData.ReadData(excelPath, "DetailLogs", 1);
                txtHours = ReadExcelData.ReadData(excelPath, "DetailLogs", 2);
                txtActivity = ReadExcelData.ReadData(excelPath, "DetailLogs", 3);                
                //Enter time under Detail Logs
                timeEntry.EnterDetailLogsHoursLV(selectProject, txtActivity, txtHours);

                string detailLogTimeEntryRoundOff = timeEntry.GetDetailLogsTimeEntryLV();
                string detailLogTimeEntryRoundOffExl = ReadExcelData.ReadData(excelPath, "DetailLogs", 4);
                Assert.AreEqual(detailLogTimeEntryRoundOff, detailLogTimeEntryRoundOffExl);
                extentReports.CreateStepLogs("Passed", "Detail Log Value:: "+ detailLogTimeEntryRoundOff + "is Rounded Off to Value: " + detailLogTimeEntryRoundOff + " is updated from more than one decimal value to one decimal value ");
                
                timeEntry.DeleteTimeEntryLV();
                extentReports.CreateStepLogs("Info", "Time Entry Deleted");

                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "User: " + user + " logged out");
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

