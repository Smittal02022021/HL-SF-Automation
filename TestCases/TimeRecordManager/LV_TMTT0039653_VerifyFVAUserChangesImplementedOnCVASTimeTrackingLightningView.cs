
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.IO;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class LV_TMTT0039653_VerifyFVAUserChangesImplementedOnCVASTimeTrackingLightningView : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RefreshButtonFunctionality refreshButton = new RefreshButtonFunctionality();
        LVHomePage homePageLV = new LVHomePage();
        LVHomePage lvHomePage = new LVHomePage();

        public static string fileTMTT0039653 = "LV_TMTT0039653_VerifyChangesImplementedOnCVASTimeTracking";

        private string projectNameExl;
        private string hoursExl;
        private string msgSuccess;
        private string actualEnteredHours;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyChangesImplementedOnCVASTimeTrackingLightningViewLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestData", fileTMTT0039653 + ".xlsx");
                excelPath = Path.GetFullPath(excelPath);

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
                string userExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                lvHomePage.SearchUserFromMainSearch1(userExl);

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
                extentReports.CreateStepLogs("Passed", "User: " + userExl + " logged in ");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + userExl);

                int rowSearchValue = ReadExcelData.GetRowCount(excelPath, "Projects");
                for (int row = 2; row <= rowSearchValue; row++)
                {
                    extentReports.CreateStepLogs("Passed", "User on Weekly Entry Matrix Page");

                    //TMTI0098439	Verify the Time Record Period is Defaulted to the Current Week for the FVA User.
                    string defaultTimeRecordPeriod = timeEntry.GetDefaultTimeRecordPeriodLV();
                    extentReports.CreateStepLogs("Info", "Default selected Time Record Period Start Date : "+ defaultTimeRecordPeriod);
                    string weekStartDate = timeEntry.GetWeekStartDateLV();
                    extentReports.CreateStepLogs("Info", "Actual Week Start Date: " + weekStartDate);
                    Assert.AreEqual(weekStartDate, defaultTimeRecordPeriod);
                    extentReports.CreateStepLogs("Passed", "Default Time Record Period Start Date is same as System Week Start Date:" + defaultTimeRecordPeriod);

                    //TMTI0098441	Verify that the user is able to input activity up to three(3) weeks forward to the current week.
                    string futureTimeRecordPeriod = timeEntry.GetFutureTimeRecordPeriodLV();
                    extentReports.CreateStepLogs("Info", "Future Time Record Period Start Date : " + futureTimeRecordPeriod);
                    string futureWeekStartDate= timeEntry.GetFutureWeekStartDateLV(3);
                    extentReports.CreateStepLogs("Info", "Actual Future Week Start Date: " + futureWeekStartDate);
                    Assert.AreEqual(futureTimeRecordPeriod, futureWeekStartDate);
                    extentReports.CreateStepLogs("Passed", "Future Time Record Period Start Date:" + defaultTimeRecordPeriod+" i.e. upto 3 future weeks ");

                    //If any previous unwanted entered is left 
                    timeEntry.DeleteTimeEntryLV();

                    //TMTI0098443   Verify that the Special Project for the Forecasting hours is introduced in the project list in the Weekly Entry Matrix tab.
                    projectNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Projects", row, 1);
                    hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Projects", row, 3);
                    timeEntry.GoToWeeklyEntryMatrixLV(); 
                    //timeEntry.SelectFutureTimePeriodLV();
                    //extentReports.CreateStepLogs("Info", "Future Dates are selected from Time Record Period Drop-down");
                    Assert.IsTrue(timeEntry.IsProjectSelectedLV(projectNameExl), "Verify that the user is able to search with " + projectNameExl + " on the Weekly Entry Matrix ");
                    extentReports.CreateStepLogs("Passed", "User is able to search and select the Project :" + projectNameExl + " on Weekly Entry Matrix ");

                    //TMTI0098445   Verify that the FVA User can add hours for the Special Project - Forcast in the Weekly Entry Matrix tab.
                    //TMTI0098449	Verify that the FVA User can add hours for the project in the Weekly Entry Matrix tab.
                    timeEntry.LogCurrentDateHoursforSpecialProjectLV(hoursExl);
                    extentReports.CreateStepLogs("Passed", " Hours: " + hoursExl+" entered for selected Project: " + projectNameExl + " on Weekly Entry Matrix ");

                    //TMTI0098447	Verify the entered hours to the Special Project are displayed in the report under Forcasted Hours.
                    actualEnteredHours= timeEntry.GetTotalForcastedHoursLV();
                    Assert.AreEqual(hoursExl, actualEnteredHours, " Verify the entered hours to the Project: Forecast are displayed in the report under Forcasted Hours");
                    extentReports.CreateStepLogs("Passed", " Entered hours to the Project: Forecast are displayed in the report under Forcasted Hours");

                    //TMTI0098451 Verify that the FVA User can remove the entered special project hours.
                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");
                    actualEnteredHours = timeEntry.GetTotalForcastedHoursLV();
                    string defaultForcastedTimeExl= ReadExcelData.ReadDataMultipleRows(excelPath, "Projects", row, 5);
                    Assert.AreNotEqual(hoursExl, actualEnteredHours, " Verify the entered hours to the Project: Forecast are deleted and udpated under Forcasted Hours");
                    Assert.AreEqual(defaultForcastedTimeExl, actualEnteredHours, " Verify the entered hours to the Project: Forecast are deleted and udpated under Forcasted Hours");
                    extentReports.CreateStepLogs("Passed", "Entered hours to the Project: Forecast are deleted and udpated under Forcasted Hours");

                    //TMTI0098453  Verify that the FVA user can access the Time Clock Recorder tab and start the time clock.
                    refreshButton.GoToTimeClockRecorderPageLV();
                    string TimeClockRecorderPageTitle = refreshButton.GetTitleTimeClockRecorderPageLV();
                    string TimeClockRecorderPageTitleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Projects", row, 7);
                    Assert.AreEqual(TimeClockRecorderPageTitleExl, TimeClockRecorderPageTitle);
                    extentReports.CreateLog(TimeClockRecorderPageTitle + " is displayed upon clicking Time Clock Recorder ");                    

                    //TMTI0098455   Verify that the FVA user can access the Summary Log tab and add hours.
                    extentReports.CreateStepLogs("Info", "Verify that the FVA user can access the Summary Log tab and add hours");
                    timeEntry.GoToSummaryLogLV();
                    extentReports.CreateStepLogs("Info", "Special Project: " + projectNameExl + "on Summary Log");
                    string msgSuccessExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Projects", row, 6);
                    msgSuccess = timeEntry.EnterSummaryLogsHoursSpecialProjectLV(projectNameExl, hoursExl);
                    Assert.AreEqual(msgSuccessExl, msgSuccess, "Verify Record is added");

                    timeEntry.GoToWeeklyEntryMatrixLV();
                    actualEnteredHours = timeEntry.GetTotalForcastedHoursLV();
                    Assert.AreEqual(hoursExl, actualEnteredHours, " Verify the Summary Logentered hours to the Project: Forecast are displayed in the report under Forcasted Hours");
                    extentReports.CreateStepLogs("Passed", " Summary Log Entered hours to the Project: Forecast are displayed in the report under Forcasted Hours");

                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Info", "Time Entry Deleted");

                    //TMTI0098457   Verify that the FVA user can access the Detail Logs tab and add hours.
                    extentReports.CreateStepLogs("Info", "Verify that the FVA user can access the Detail Logs tab and add hours");
                    timeEntry.GoToDetailLogsLV();
                    msgSuccess = timeEntry.EnterDetailLogsHoursSpecialProjectLV(projectNameExl, hoursExl);
                    extentReports.CreateStepLogs("Info", "Special Project: " + projectNameExl + "on Detail Logs");
                    Assert.AreEqual(msgSuccessExl, msgSuccess, "Verify Record is added");

                    timeEntry.GoToWeeklyEntryMatrixLV();
                    actualEnteredHours = timeEntry.GetTotalForcastedHoursLV();
                    Assert.AreEqual(hoursExl, actualEnteredHours, " Verify the Detail Logs entered hours to the Project: Forecast are displayed in the report under Forcasted Hours");
                    extentReports.CreateStepLogs("Passed", " Detail Logs Entered hours to the Project: Forecast are displayed in the report under Forcasted Hours");

                    //TMTI0098459	Verify that the FVA user can update the entered hrs from the Detail Logs tab.
                    extentReports.CreateStepLogs("Info", "Verify that the FVA user can update the entered hrs from the Detail Logs tab");
                    string newHoursExl= ReadExcelData.ReadDataMultipleRows(excelPath, "Projects", row, 4);
                    timeEntry.GoToDetailLogsLV();
                    timeEntry.UpdateDetailLogsHoursSpecialProjectLV(newHoursExl);
                    extentReports.CreateStepLogs("Passed", "Hours Updated on Detail Logs Page ");
                    string txtLatestHours = timeEntry.GetDetailLogsHours();
                    Assert.AreEqual(txtLatestHours, newHoursExl);

                    //TMTI0098461	Verify that the FVA user can delete entered hours from the Detail Logs tab.
                    extentReports.CreateStepLogs("Info", "Verify that the FVA user can delete entered hours from the Detail Logs tab");
                    timeEntry.RemoveRecordFromDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "Deleted record entry successfully from Detail Logs ");

                    //TMTI0098463	Verify that the FVA user can access and add hours for the selected projects from the Weekly Overview tab.
                    timeEntry.GoToWeeklyOverviewLV();
                    msgSuccess = timeEntry.EnterWeeklyOverviewHoursSpecialProjectLV(projectNameExl, hoursExl);
                    extentReports.CreateStepLogs("Info", "Special Project: " + projectNameExl + "on Weekly Overview");
                    Assert.AreEqual(msgSuccessExl, msgSuccess, "Verify Record is added");

                    timeEntry.GoToWeeklyEntryMatrixLV();
                    actualEnteredHours = timeEntry.GetTotalForcastedHoursLV();
                    Assert.AreEqual(hoursExl, actualEnteredHours, " Verify the Weekly Overview entered hours to the Project: Forecast are displayed in the report under Forcasted Hours");
                    extentReports.CreateStepLogs("Passed", " Weekly Overview Entered hours to the Project: Forecast are displayed in the report under Forcasted Hours");

                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Info", "Time Entry Deleted");
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
