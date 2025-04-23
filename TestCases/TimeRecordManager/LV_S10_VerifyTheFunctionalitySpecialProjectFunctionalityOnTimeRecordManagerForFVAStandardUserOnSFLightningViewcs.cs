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
    class LV_S10_VerifyTheFunctionalitySpecialProjectFunctionalityOnTimeRecordManagerForFVAStandardUserOnSFLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RefreshButtonFunctionality refreshButton = new RefreshButtonFunctionality();
        LVHomePage lvHomePage = new LVHomePage();

        public static string fileTMT = "LV_VerifyTheFunctionalitySpecialProjectFunctionalityOnTimeRecordManagerForFVAStandardUserOnSFLightningView";

        private string hoursExl;
        private string projectNameExl;
        private string user;
        private string userExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyTheFunctionalitySpecialProjectFunctionalityOnTimeRecordManagerForFVAStandardUser()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT;

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
                int rowUser = ReadExcelData.GetRowCount(excelPath, "Users");

                for (int row = 2; row <= rowUser; row++)
                {
                    userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);

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

                    //Select Module
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + userExl);

                    //Existing Engagement with Deal team member of Logged in user
                    projectNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "WeeklyEntryMatrix", row, 1);
                    hoursExl= ReadExcelData.ReadDataMultipleRows(excelPath, "WeeklyEntryMatrix", row, 2);
                    extentReports.CreateLog("Project Searching with Name: " + projectNameExl + " ");

                    //S9	Verify the Functionalty of Time Record manager for Special Project(Only Comment box opens instead of Activity list) of Weekly Entry Metrix 
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    timeEntry.SelectProjectWeeklyEntryMatrixLV(projectNameExl);
                    timeEntry.LogCurrentDateHoursforSpecialProjectLV(hoursExl);
                    extentReports.CreateLog("Special Project: " + projectNameExl+ " on Weekly Entry Matrix ");

                    //Verify that the Comments box is present instead of activity list for special project selection

                    Assert.IsFalse(timeEntry.IsActivityListDisplayedLV(), "Verify Activity List is not displayed on Weekly Entry Matrix");
                    extentReports.CreateStepLogs("Passed", "Activity List is not displayed for logged in user on Weekly Entry Matrix ");
                    //Comments
                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Info", "Time Entry Deleted");
                    
                    //Time Clock Recorder
                    refreshButton.GoToTimeClockRecorderPageLV();
                    refreshButton.ClickResetButtonLV();
                    timeEntry.SelectProjectWeeklyEntryMatrixLV(projectNameExl);
                    extentReports.CreateStepLogs("Info", "Special Project: " + projectNameExl + " on Time Clock Recorder Page");
                    
                    Assert.IsFalse(timeEntry.IsActivityListDisplayedLV(), "Verify Activity List is not displayed on Time Clock Recorder Page");
                    extentReports.CreateStepLogs("Passed", "Activity List is not displayed for logged in user on Time Clock Recorder ");
                    Assert.IsTrue(timeEntry.IsTimeClockRecorderCommentBoxDisplayedLV(), "Verify A Comments Box is Displayed instead on Activity List on Time Clock Recorder");
                    extentReports.CreateStepLogs("Passed", "A Comments Box is Displayed instead on Activity List on Time Clock Recorder");
                    
                    //S10	Verify the Functionalty of Time Record manager for Special Project(Only Comment box opens instead of Activity list) of Summary Logs Metrix 
                    timeEntry.GoToSummaryLogLV();
                    extentReports.CreateStepLogs("Info", "Special Project: " + projectNameExl+ "on Summary Log");
                    bool isActivityDisplayed= timeEntry.EnterSummaryLogsHoursValidateActivityListLV(projectNameExl, hoursExl);
                    Assert.IsFalse(isActivityDisplayed, "Verify Activity List is not displayed on Summary Logs");
                    extentReports.CreateStepLogs("Passed", "Activity List is not displayed for logged in user on Summary Logs ");
                    //bool IsCommentsBoxDisplayed = timeEntry.IsLogsCommentBoxDisplayedLV();
                    //Assert.IsTrue(IsCommentsBoxDisplayed, "Verify Comments Box Displayed on Summary Logs for Special Project");
                    //extentReports.CreateStepLogs("Passed", "A Comments Box is Displayed instead on Summary Logs for Special Project");

                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Info", "Time Entry Deleted");

                    //S11	Verify the Functionalty of Time Record manager for Special Project(Only Comment box opens instead of Activity list) of Detail Logs Metrix 
                    timeEntry.GoToDetailLogsLV();
                    isActivityDisplayed = timeEntry.EnterDetailLogsHoursValidateActivityListLV(projectNameExl, hoursExl);
                    extentReports.CreateStepLogs("Info", "Special Project: " + projectNameExl + "on Detail Logs");
                    Assert.IsFalse(isActivityDisplayed, "Verify Activity List is not displayed on Detail Logs");
                    extentReports.CreateStepLogs("Passed", "Activity List is not displayed for logged in user on Detail Logs ");
                    //IsCommentsBoxDisplayed = timeEntry.IsLogsCommentBoxDisplayedLV();
                    //Assert.IsTrue(IsCommentsBoxDisplayed, "Verify Comments Box Displayed on Detail Logs Logs for Special Project");
                    //extentReports.CreateStepLogs("Passed", "A Comments Box is Displayed instead on Detail Logs Logs for Special Project");

                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Info", "Time Entry Deleted");

                    //S12	Verify the Functionalty of Time Record manager for Special Project(Only Comment box opens instead of Activity list) of Weekly Overview 
                    timeEntry.GoToWeeklyOverviewLV();
                    isActivityDisplayed = timeEntry.EnterWeeklyOverviewHoursValidateActivityListLV(projectNameExl, hoursExl);
                    extentReports.CreateStepLogs("Info", "Special Project: " + projectNameExl + "on Weekly Overview");
                    Assert.IsFalse(isActivityDisplayed, "Verify Activity List is not displayed on Weekly Overview");
                    extentReports.CreateStepLogs("Passed", "Activity List is not displayed for logged in user on Weekly Overview ");
                    //IsCommentsBoxDisplayed = timeEntry.IsLogsCommentBoxDisplayedLV();
                    //Assert.IsTrue(IsCommentsBoxDisplayed, "Verify Comments Box Displayed on Weekly Overview for Special Project");
                    //extentReports.CreateStepLogs("Passed", "A Comments Box is Displayed instead on Activity List on Weekly Overview for Special Project");

                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Info", "Time Entry Deleted");                    
                }

                //Logout for CF Financial User
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "CF Financial User Logged Out from SF Lightning View. ");

                //TC - End
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "Admin User Logged Out from SF Lightning View. ");

                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                timeEntry.DeleteTimeEntryLV();
                driver.Quit();
            }
        }
    }
}
