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
    class LV_TMTT0039653_VerifyTFRSupervisorUserChangesImplementedOnCVASTimeTrackingLightningView : BaseClass
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

        public static string fileTMTT0039653 = "LV_TMTT0039653_VerifyChangesImplementedOnCVASTimeTracking";

        private string projectNameExl;
        private string hoursExl;
        private string msgSuccess;
        private string actualEnteredHours;
        private string userExl;
        private string groupName;
        private string profile;

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0039653;

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

                int rowSearchValue = ReadExcelData.GetRowCount(excelPath, "SupervisorUser");
                for (int row = 2; row <= rowSearchValue; row++)
                {
                    //Login as User and validate the user
                    userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SupervisorUser", row, 1);
                    groupName = ReadExcelData.ReadDataMultipleRows(excelPath, "SupervisorUser", row, 2);
                    profile = ReadExcelData.ReadDataMultipleRows(excelPath, "SupervisorUser", row, 3);
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " from Group: " + groupName + " with Profile: " + profile + " logged in ");

                    //Search CF Financial user by global search
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

                    //TMTI0098465 Verify that the Supervisor who is part of the Time Tracking TFR Supervisor group can access the "Time Tracking" module.
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + userExl + " from Time Tracking TFR Supervisor group");

                    //TMTI0098471	Verify that the Supervisor can access the Time Clock Recorder tab.
                    refreshButton.GoToTimeClockRecorderPageLV();
                    string TimeClockRecorderPageTitle = refreshButton.GetTitleTimeClockRecorderPageLV();
                    string TimeClockRecorderPageTitleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Projects", 2, 7);
                    Assert.AreEqual(TimeClockRecorderPageTitleExl, TimeClockRecorderPageTitle);
                    extentReports.CreateLog(TimeClockRecorderPageTitle + " is displayed upon clicking Time Clock Recorder ");

                    //TMTI0098467 Verify that the Supervisor can add hours to the project for any user.
                    //Select Staff Member from the list
                    string staffNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    //timeEntry.GoToStaffTimeSheetTabLV();
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    timeEntry.SelectStaffMemberLV(staffNameExl);
                    string staffName = timeEntry.GetSelectedStaffNameLV();
                    Assert.AreEqual(staffNameExl, staffName);
                    extentReports.CreateStepLogs("Passed", "Staff : " + staffName + " is Selected from list ");

                    //If any previous unwanted entered is left 
                    timeEntry.DeleteTimeEntryLV();
                    projectNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Projects", 2, 1);
                    hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Projects", 2, 3);

                    timeEntry.GoToWeeklyEntryMatrixLV();
                    Assert.IsTrue(timeEntry.IsProjectSelectedLV(projectNameExl), "Verify that the Time Tracking TFR Supervisor user is able to search with " + projectNameExl + " on the Weekly Entry Matrix for selected staff member");
                    extentReports.CreateStepLogs("Passed", "Time Tracking TFR Supervisor User is able to search and select the Project :" + projectNameExl + " on Weekly Entry Matrix for selected staff member");

                    timeEntry.LogCurrentDateHoursforSpecialProjectLV(hoursExl);
                    extentReports.CreateStepLogs("Passed", " Hours: " + hoursExl + " entered for selected Project: " + projectNameExl + " on Weekly Entry Matrix ");

                    actualEnteredHours = timeEntry.GetTotalForcastedHoursLV();
                    Assert.AreEqual(hoursExl, actualEnteredHours, " Verify the entered hours to the Project: Forecast are displayed in the report under Forcasted Hours");
                    extentReports.CreateStepLogs("Passed", " Entered hours to the Project: Forecast are displayed in the report under Forcasted Hours");

                    //TMTI0098469	Verify that the Supervisor can remove the entered hours of any user.
                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Passed", "Entered hours are deleted by Time Tracking TFR Supervisor user");

                    //TMTI0098473	Verify that the Supervisor can access the Summary Log tab and add hours.
                    extentReports.CreateStepLogs("Info", "Verify that the Time Tracking TFR Supervisor user can access the Summary Log tab and add hours for selected user");
                    timeEntry.GoToSummaryLogLV();
                    extentReports.CreateStepLogs("Info", "Special Project: " + projectNameExl + "on Summary Log");
                    string msgSuccessExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Projects", 2, 6);
                    msgSuccess = timeEntry.EnterSummaryLogsHoursSpecialProjectLV(projectNameExl, hoursExl);
                    Assert.AreEqual(msgSuccessExl, msgSuccess, "Verify Record is added");

                    timeEntry.GoToWeeklyEntryMatrixLV();
                    actualEnteredHours = timeEntry.GetTotalForcastedHoursLV();
                    Assert.AreEqual(hoursExl, actualEnteredHours, " Verify the Summary Log entered hours to the Project: Forecast are displayed in the report under Forcasted Hours");
                    extentReports.CreateStepLogs("Passed", " Summary Log Entered hours to the Project: Forecast are displayed in the report under Forcasted Hours");

                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Info", "Time Entry Deleted");

                    //TMTI0098475	Verify that the Supervisor can update the entered activity and hrs from the Detail Logs tab.
                    extentReports.CreateStepLogs("Info", "Verify that the Time Tracking TFR Supervisor  user can access the Detail Logs tab and add hours for selected user");
                    timeEntry.GoToDetailLogsLV();
                    msgSuccess = timeEntry.EnterDetailLogsHoursSpecialProjectLV(projectNameExl, hoursExl);
                    extentReports.CreateStepLogs("Info", "Special Project: " + projectNameExl + "on Detail Logs");
                    Assert.AreEqual(msgSuccessExl, msgSuccess, "Verify Record is added");

                    timeEntry.GoToWeeklyEntryMatrixLV();
                    actualEnteredHours = timeEntry.GetTotalForcastedHoursLV();
                    Assert.AreEqual(hoursExl, actualEnteredHours, " Verify the Detail Logs entered hours to the Project: Forecast are displayed in the report under Forcasted Hours");
                    extentReports.CreateStepLogs("Passed", " Detail Logs Entered hours to the Project: Forecast are displayed in the report under Forcasted Hours");

                    //TMTI0098477	Verify that the Supervisor can access the Detail Logs tab and add hours.
                    extentReports.CreateStepLogs("Info", "Verify that the Time Tracking TFR Supervisor user can update the entered hrs from the Detail Logs tab");
                    string newHoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Projects", 2, 4);
                    timeEntry.GoToDetailLogsLV();
                    timeEntry.UpdateDetailLogsHoursSpecialProjectLV(newHoursExl);
                    extentReports.CreateStepLogs("Passed", "Hours Updated on Detail Logs Page by Time Tracking TFR Supervisor");
                    string txtLatestHours = timeEntry.GetDetailLogsHours();
                    Assert.AreEqual(txtLatestHours, newHoursExl);

                    //TMTI0098479	Verify that the Supervisor can delete entered hours from the Detail Logs tab.
                    extentReports.CreateStepLogs("Info", "Verify that the Time Tracking TFR Supervisor user can delete entered hours from the Detail Logs tab");
                    timeEntry.RemoveRecordFromDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "Deleted record entry successfully from Detail Logs ");

                    //TMTI0098481	Verify that the Supervisor can access and add hours from the Weekly Overview tab for the selected projects.
                    extentReports.CreateStepLogs("Info", "Verify that the Supervisor can access and add hours from the Weekly Overview tab for the selected projects from the Weekly Overview tab");
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

                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " logged out");
                }

                //TC - End
                lvHomePage.UserLogoutFromSFLightningView();
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
