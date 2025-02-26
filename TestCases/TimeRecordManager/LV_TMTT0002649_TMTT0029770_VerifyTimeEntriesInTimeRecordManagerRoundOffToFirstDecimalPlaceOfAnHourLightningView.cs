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
    class LV_TMTT0002649_TMTT0029770_VerifyTimeEntriesInTimeRecordManagerRoundOffToFirstDecimalPlaceOfAnHourLightningView : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();

        public static string TMTT0002649 = "LV_TMTT0002649_TimeRecordManager_VerifyTimeEntriesareRoundOffToFirstDecimalPlace";
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
        public void VerifyTimeEntriesInTimeRecordManagerRoundOffForCFFinancialUserLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + TMTT0002649;

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

                string userExl = ReadExcelData.ReadData(excelPath, "Users", 1);

                //Search CF Financial user by global search
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
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + userExl + " has logged in ");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + userExl);
                
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
                extentReports.CreateStepLogs("Info", "User: " + userExl + " is on Weekly Entry Matrix Page");
                selectProjectExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 1);
                hoursExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 2);
                activityExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 3);                
                timeEntry.EnterWeeklyEntryMatrixLV(selectProjectExl, hoursExl, activityExl);
                
                string sundayTimeRoundOff = timeEntry.GetSundayTimeEntryLV();
                string sundayTimeRoundOffExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 4);
                Assert.AreEqual(sundayTimeRoundOffExl, sundayTimeRoundOff);
                extentReports.CreateStepLogs("Passed", "Time Entry Value:: "+ hoursExl+" is rounded off to Value: " + sundayTimeRoundOff + " is updated from more than one decimal value to one decimal value ");
                
                timeEntry.DeleteTimeEntryLV();
                extentReports.CreateStepLogs("Info", "Time Entry Deleted");

                //Clear time record from weekly entry matrix
                string defaultSelectedProjectOption = timeEntry.GetDefaultSelectedProjectOptionLV();
                string defaultSelectedProjectOptionExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 6);
                Assert.AreEqual(defaultSelectedProjectOptionExl, defaultSelectedProjectOption);
                extentReports.CreateStepLogs("Passed", "Time Entry Project Default: " + defaultSelectedProjectOption + " is displayed upon deletion of time entry ");

                // Enter summary logs entry
                timeEntry.GoToSummaryLogLV();
                extentReports.CreateStepLogs("Info", "User: " + userExl + " is on Summary Log Page ");
                selectProjectExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 1);
                hoursExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 2);
                activityExl = ReadExcelData.ReadData(excelPath, "SummaryLogs",3); 
                
                //Enter time under Summary Logs
                timeEntry.EnterSummaryLogsHoursLV(selectProjectExl, activityExl, hoursExl);

                string summaryLogTimeEntryRoundOff = timeEntry.GetSummaryLogsTimeEntryLV();
                string summaryLogTimeEntryRoundOffExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 4);
                Assert.AreEqual(summaryLogTimeEntryRoundOffExl, summaryLogTimeEntryRoundOff);
                extentReports.CreateStepLogs("Passed", "Summary Log Value:: "+ hoursExl + " is Rounded Off to Value: " + summaryLogTimeEntryRoundOff + " is updated from more than one decimal value to one decimal value ");
                
                timeEntry.DeleteTimeEntryLV();
                extentReports.CreateStepLogs("Info", "Time Entry Deleted");

                //Enter detail logs entry
                timeEntry.GoToDetailLogsLV();
                extentReports.CreateStepLogs("Passed", "User: " + userExl + " is on Detail Logs Page ");
                selectProjectExl = ReadExcelData.ReadData(excelPath, "DetailLogs", 1);
                hoursExl = ReadExcelData.ReadData(excelPath, "DetailLogs", 2);
                activityExl = ReadExcelData.ReadData(excelPath, "DetailLogs", 3);
                
                //Enter time under Detail Logs
                timeEntry.EnterDetailLogsHoursLV(selectProjectExl, activityExl, hoursExl);

                string detailLogTimeEntryRoundOff = timeEntry.GetDetailLogsTimeEntryLV();
                string detailLogTimeEntryRoundOffExl = ReadExcelData.ReadData(excelPath, "DetailLogs", 4);
                Assert.AreEqual(detailLogTimeEntryRoundOff, detailLogTimeEntryRoundOffExl);
                extentReports.CreateStepLogs("Passed", "Detail Log Value:: "+ hoursExl + " is Rounded Off to Value: " + detailLogTimeEntryRoundOff + " is updated from more than one decimal value to one decimal value ");
                
                timeEntry.DeleteTimeEntryLV();
                extentReports.CreateStepLogs("Info", "Time Entry Deleted");

                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "User: " + userExl + " logged out");

                //TC - End
                lvHomePage.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", "Admin User Logged Out from SF Lightning View. ");

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

