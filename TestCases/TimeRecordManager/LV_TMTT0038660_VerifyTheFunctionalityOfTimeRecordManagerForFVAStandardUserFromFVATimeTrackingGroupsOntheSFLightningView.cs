using SF_Automation.Pages.Common;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using NUnit.Framework;
using SF_Automation.TestData;
using System;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class LV_TMTT0038660_VerifyTheFunctionalityOfTimeRecordManagerForFVAStandardUserFromFVATimeTrackingGroupsOntheSFLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();        
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();

        public static string fileTMTT0038660 = "LV_TMTT0038660_VerifyTheFunctionalityOfTimeRecordManagerForFVAStandardUserOntheSFLightningView";

        private string textMessage;
        private string hoursExl;
        private string activityExl;
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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0038660;                
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
                    login.SwitchToClassicView();
                    string user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(userExl), true);
                    extentReports.CreateStepLogs("Passed", "Standard User: " + userExl + " from Time Tracking Group: "+ userGrpNameExl+"  logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateLog("User: " + userExl + " Switched to Lightning View ");
                    //homePageLV.ClickAppLauncher();
                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    //TMTI0093788 Verify that the FVA User can access the Time Tracking module in Lightning and logged-in user name is displayed on the top
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + user);
                    string GetTimeRecordUserNameLV = timeEntry.GetTimeRecordUserNameLV();
                    Assert.AreEqual(GetTimeRecordUserNameLV, user, "Verify Logged-in FVA User name is displayed on the top of Time Record Manager Page ");
                    extentReports.CreateStepLogs("Passed", "User Name: " + GetTimeRecordUserNameLV + " is displayed on the top of Time Record Manager Page ");

                    //TMTI0093762	Verify that the FVA User can add hours for the project in Weekly Entry Matrix tab
                    selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 1);
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    extentReports.CreateStepLogs("Info", "User: " + user + " is on Weekly Entry Matrix Page");

                    //TMTI0093781	Verify that the Time Record Period is Defaulted to the Current Week for the FVA User.
                    string defaultTimeRecordPeriod = timeEntry.GetDefaultTimeRecordPeriodLV();
                    extentReports.CreateStepLogs("Info", "Default selected Time Record Period Start Date : " + defaultTimeRecordPeriod);
                    string weekStartDate = timeEntry.GetWeekStartDateLV();
                    extentReports.CreateStepLogs("Info", "Actual Week Start Date: " + weekStartDate);
                    Assert.AreEqual(weekStartDate, defaultTimeRecordPeriod);
                    extentReports.CreateStepLogs("Passed", "Default Time Record Period Start Date is same as System Week Start Date:" + defaultTimeRecordPeriod);

                    timeEntry.SelectProjectWeeklyEntryMatrixLV(selectProject);
                    extentReports.CreateStepLogs("Info", "Project: " + selectProject + " selected on Weekly Entry Matrix ");
                    txtHours = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 2); ;
                    //Enter time under weekly time matrix
                    if(userGrpNameExl== "Time Tracking TFR")
                    {
                        extentReports.CreateStepLogs("Info", "Activity List is not available for TFR group");
                        timeEntry.LogCurrentDateHoursTFRGroupLV(txtHours);
                        extentReports.CreateStepLogs("Passed", "Hours entered on Weekly Entry Matrix Page");
                    }
                    else
                    {
                        timeEntry.LogCurrentDateHoursLV(txtHours);
                        extentReports.CreateStepLogs("Passed", "Hours entered on Weekly Entry Matrix Page");
                    }

                    //TMTI0093769	Verify that the FVA User can remove entered hours from the Weekly Entry Matrix tab
                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");

                    //TMTI0093782	Verify that the FVA user can access the Summary Log tab and can add hours
                    timeEntry.GoToSummaryLogLV();
                    extentReports.CreateStepLogs("Passed", "User: " + user + " is on Summary Log Page ");
                    activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 3);
                    hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 2);
                    //Enter time under Summary Logs
                    if (userGrpNameExl == "Time Tracking TFR")
                    {
                        extentReports.CreateStepLogs("Info", "Activity List is not available for TFR group");
                        textMessage = timeEntry.EnterSummaryLogsHoursTFRGroupLV(selectProject,hoursExl);
                        Assert.AreEqual(textMessage, "Time Record Added");
                        extentReports.CreateStepLogs("Passed", " Hours entered on Summary Logs Page with Success Message: " + textMessage);
                    }
                    else
                    {
                        textMessage = timeEntry.EnterSummaryLogsHoursLV(selectProject, activityExl, hoursExl);
                        Assert.AreEqual(textMessage, "Time Record Added");
                        extentReports.CreateStepLogs("Passed", " Hours entered on Summary Logs Page with Success Message: " + textMessage);
                    }

                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");

                    //TMTI0093763 Verify that the FVA user can access the Detail Logs tab.
                    timeEntry.GoToDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "User: " + user + " is on Detail Logs Page ");
                    selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 1);
                    hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 2);
                    activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 3);
                    string newHoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateData", row, 1);
                    string newActivityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateData", row, 2);
                    if (userGrpNameExl == "Time Tracking TFR")
                    {
                        extentReports.CreateStepLogs("Info", "Activity List is not available for TFR group");
                        //TMTI0093776	Verify that the FVA user can add hours from the Detail Logs tab and a success message appears on the screen.
                        textMessage = timeEntry.EnterDetailLogsHoursTFRGroupLV(selectProject, hoursExl);
                        Assert.AreEqual(textMessage, "Time Record Added");
                        extentReports.CreateStepLogs("Passed", " Hours entered on Detail Logs Page with Success Message: " + textMessage);

                        // TMTI0093770 Verify that the FVA user can update the entered activity and hours from the Detail Logs tab.
                        timeEntry.UpdateDetailLogsHoursTFRGroupLV(newHoursExl);
                        extentReports.CreateStepLogs("Passed", "Activity and Hours Updated on Detail Logs Page ");
                        string txtLatestHours = timeEntry.GetDetailLogsHours();                        
                        Assert.AreEqual(txtLatestHours, newHoursExl);
                        extentReports.CreateStepLogs("Passed", "Activity and Hours are Updated on Detail Logs Page ");
                    }
                    else
                    {
                        //TMTI0093776	Verify that the FVA user can add hours from the Detail Logs tab and a success message appears on the screen.
                        textMessage = timeEntry.EnterDetailLogsHoursLV(selectProject, activityExl, hoursExl);
                        Assert.AreEqual(textMessage, "Time Record Added");
                        extentReports.CreateStepLogs("Passed", " Hours entered on Detail Logs Page with Success Message: " + textMessage);

                        // TMTI0093770 Verify that the FVA user can update the entered activity and hours from the Detail Logs tab.
                        timeEntry.UpdateDetailLogsHoursLV(newActivityExl, newHoursExl);
                        extentReports.CreateStepLogs("Passed", "Activity and Hours Updated on Detail Logs Page ");
                        string txtLatestActivity = timeEntry.GetDetailLogsActivity();
                        string txtLatestHours = timeEntry.GetDetailLogsHours();
                        Assert.AreEqual(txtLatestActivity, newActivityExl);
                        Assert.AreEqual(txtLatestHours, newHoursExl);
                        extentReports.CreateStepLogs("Passed", "Activity and Hours are Updated on Detail Logs Page ");
                     }

                    //TMTI0093783	Verify that the FVA user can delete entered hours from the Detail Logs tab..
                    timeEntry.RemoveRecordFromDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");

                    //TMTI0093764	Verify that the FVA user can access the Weekly Overview tab and see entered hours.
                    timeEntry.GoToWeeklyOverviewLV();
                    extentReports.CreateStepLogs("Passed", "User: " + user + " is on Weekly Overview Page ");
                    selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "WeeklyOverview", row, 1);
                    hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "WeeklyOverview", row, 2);
                    activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "WeeklyOverview", row, 3);

                    if (userGrpNameExl == "Time Tracking TFR")
                    {
                        extentReports.CreateStepLogs("Info", "Activity List is not available for TFR group");
                        //TMTI0093771	Verify that the FVA user can add hours from the Weekly Overview tab for the selected project and success message appears on the screen.
                        textMessage = timeEntry.EnterWeeklyOverviewHoursTFRGroupLV(selectProject, hoursExl);
                        Assert.AreEqual(textMessage, "Time Record Added");
                        extentReports.CreateStepLogs("Passed", " Hours entered on Weekly Overview Page with Success Message: " + textMessage);
                    }
                    else
                    {
                        //TMTI0093771	Verify that the FVA user can add hours from the Weekly Overview tab for the selected project and success message appears on the screen.
                        textMessage = timeEntry.EnterWeeklyOverviewHoursLV(selectProject, activityExl, hoursExl);
                        Assert.AreEqual(textMessage, "Time Record Added");
                        extentReports.CreateStepLogs("Passed", " Hours entered on Weekly Overview Page with Success Message: " + textMessage);
                    }

                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");

                    usersLogin.ClickLogoutFromLightningView();
                    login.SwitchToClassicView();
                    extentReports.CreateStepLogs("Info", "User: " + user + " logged out");                    
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
