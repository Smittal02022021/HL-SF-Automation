using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;
using System.Threading;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class LV_TMTT0011411_TMTT0011412_TMTT0011417_TMTT0011427_VerifyUserWithTitleTAGOutsourcedContractorIsAbleToEnterMoreThan24hoursForFutureDatesAsWellLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileTMT1411 = "LV_TMTT0011411VerifyUserWithTitleTAGOutsourcedContractor";

        private string DetailLogTime;
        private Double DetailLogTimedb;
        private string summaryLogTime;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyTAGOutsourcedContractorFunctionalitiesLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT1411;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string userExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(userExl);

                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(userExl), true);
                extentReports.CreateStepLogs("Passed", " User: " + userExl + " logged in ");
                //homePageLV.ClickAppLauncher();

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
                string timeRecordManagerTitleExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 2);
                Assert.AreEqual(timeRecordManagerTitleExl, timeRecordManagerTitle);
                extentReports.CreateStepLogs("Passed", "Time Record Manager Title: " + timeRecordManagerTitle + " is displayed upon click of Time Record Manager tab ");
                extentReports.CreateStepLogs("Info", "Verify user with Title :TAG Outsourced Contractor is able to enter more than 24 hours for future dates as well");

                //Click Time Record Manager Tab
                timeEntry.GoToWeeklyEntryMatrixLV();
                extentReports.CreateStepLogs("Info", "User: " + userExl + " is on Weekly Entry Matrix Page");

                string selectProject = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 1);
                string txtHours = ReadExcelData.ReadData(excelPath, "Update_Hours", 1);
                timeEntry.SelectProjectWeeklyEntryMatrixLV(selectProject);

                //Enter hours for future dates on Weekly Entry Matrix
                string weekDay = timeEntry.LogFutureDateHoursLV(txtHours);
                timeEntry.GoToWeeklyEntryMatrixLV();
                //Go to Summary Logs
                timeEntry.GoToSummaryLogLV();
                extentReports.CreateLog("User has navigated to Summary logs ");

                //Get Summary Log Time Entry
                summaryLogTime = timeEntry.GetSummaryLogsTimeEntryLV();
                Assert.AreEqual(txtHours.ToString(), summaryLogTime);
                extentReports.CreateLog("Hours: " + summaryLogTime + " is logged in Sumamry logs ");

                //Go to Details log
                timeEntry.GoToDetailLogsLV();
                extentReports.CreateLog("User has navigated to Detail logs ");

                //Verify detail logged hours
                DetailLogTime = timeEntry.GetDetailLogsTimeEntryLV(); 
                DetailLogTimedb = Convert.ToDouble(DetailLogTime);

                Assert.AreEqual(Convert.ToDouble(txtHours), DetailLogTimedb);
                extentReports.CreateLog("Time displaying in detail log: " + DetailLogTimedb + " Hours ");

                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntryLV();
                extentReports.CreateLog("User has deleted the record ");

                extentReports.CreateStepLogs("Info", "Verify the maximum hours of time entries for Title: TAG Outsourced Contractor(limit technically is 1000 hours) Weekly Time Entries, Summary Log, Detail Log");

                // Go to Weekly Entry Matrix
                timeEntry.GoToWeeklyEntryMatrixLV();
                extentReports.CreateStepLogs("Info", "User: " + userExl + " is on Weekly Entry Matrix Page");

                selectProject = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 1);
                string activityExl = ReadExcelData.ReadData(excelPath, "Activity_List", 1);
                timeEntry.SelectProjectWeeklyEntryMatrixLV(selectProject);
                txtHours = ReadExcelData.ReadData(excelPath, "Update_Timer", 1);
                extentReports.CreateStepLogs("Passed", "Selected Project and Activity from Drop down");
                //Log 1000 hours
                timeEntry.LogCurrentDateHoursLV(txtHours);

                //Go to Summary Logs
                timeEntry.GoToSummaryLogLV();
                extentReports.CreateLog("User has navigated to Summary logs ");

                //Get Summary Log Time Entry
                summaryLogTime = timeEntry.GetSummaryLogsTimeEntryLV();
                Assert.AreEqual(summaryLogTime, txtHours);
                extentReports.CreateLog("Hours: " + txtHours + " is logged in Sumamry logs ");

                //Go to Details log
                timeEntry.GoToDetailLogsLV();
                extentReports.CreateLog("User has naigated to details log ");
                //Verify detail logged hours
                DetailLogTime = timeEntry.GetDetailLogsTimeEntryLV();
                DetailLogTimedb = Convert.ToDouble(DetailLogTime);   

                Assert.AreEqual(Convert.ToDouble(txtHours), DetailLogTimedb);
                extentReports.CreateLog("Time displaying in detail log: " + DetailLogTimedb + " Hours ");

                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntryLV();
                extentReports.CreateLog("User has deleted the record ");

                extentReports.CreateStepLogs("Info", "Verify the User with Title :TAG Outsourced Contractor is able to enter own hours more than 24 hours ( Weekly Sheet, Summary Log and Detail Log tabs");
                txtHours= ReadExcelData.ReadData(excelPath, "Update_Hours", 2);
                activityExl= ReadExcelData.ReadData(excelPath, "Project_Title", 2);
                //Go to Summary Logs
                timeEntry.GoToSummaryLogLV();
                extentReports.CreateLog("User has navigated to Summary logs ");
                timeEntry.EnterSummaryLogsHoursLV(selectProject, activityExl,txtHours);

                //Go to Details log
                timeEntry.GoToDetailLogsLV();
                extentReports.CreateLog("User has naigated to details log ");
                //Verify detail logged hours
                DetailLogTime = timeEntry.GetDetailLogsTimeEntryLV();
                DetailLogTimedb = Convert.ToDouble(DetailLogTime);

                Assert.AreEqual(DetailLogTimedb, Convert.ToDouble(txtHours));
                extentReports.CreateLog("Time displaying in detail log: " + DetailLogTime + " Hours ");

                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntryLV();
                extentReports.CreateLog("User has deleted the record ");

                //Go to Details log
                timeEntry.GoToDetailLogsLV();
                extentReports.CreateLog("User has naigated to details log ");

                timeEntry.EnterDetailLogsHoursLV(selectProject, activityExl, txtHours);
                //Go to Summary Logs
                timeEntry.GoToSummaryLogLV();
                extentReports.CreateLog("User has navigated to Summary logs ");

                //Get Summary Log Time Entry
                summaryLogTime = timeEntry.GetSummaryLogsTimeEntryLV();
                //string summaryhours1 = txtHours;

                Assert.AreEqual(summaryLogTime, txtHours);
                extentReports.CreateLog("Hours: " + summaryLogTime + " is logged in Sumamry logs ");

                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntryLV();
                extentReports.CreateStepLogs("Info", "User has deleted the record ");
                extentReports.CreateStepLogs("Info", "Verify the Only Forecast option should be available in Activity List for future dates");
                                
                //Go to Summary Logs
                timeEntry.GoToWeeklyEntryMatrixLV();
                timeEntry.SelectFutureTimePeriodLV();
                extentReports.CreateStepLogs("Info", "Future Dates are selected from Time Record Period Drop-down");
                timeEntry.SelectProjectWeeklyEntryMatrixLV(selectProject);
                extentReports.CreateStepLogs("Info", "Project is selected for Future date dates");
                timeEntry.VerifyActivityDropDownForFuturePeriodLV(fileTMT1411);
                extentReports.CreateStepLogs("Passed", "Forecast option is available in  Activity List for future dates");                

                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "User: " + userExl + " logged out");
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
