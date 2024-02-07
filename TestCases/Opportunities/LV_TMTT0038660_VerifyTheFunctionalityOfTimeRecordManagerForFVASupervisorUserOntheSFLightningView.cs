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
    class LV_TMTT0038660_VerifyTheFunctionalityOfTimeRecordManagerForFVASupervisorUserOntheSFLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();

        public static string fileTMTT0038660 = "LV_TMTT0038660_VerifyTheFunctionalityOfTimeRecordManagerForFVASupervisorUserOntheSFLightningView";

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0038660;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowCount = ReadExcelData.GetRowCount(excelPath, "Users");

                for (int row = 2; row <= rowCount; row++)
                {
                    //Login as Standard User profile and validate the user
                    string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    usersLogin.SearchUserAndLogin(userExl);
                    login.SwitchToClassicView();
                    string user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(userExl), true);
                    extentReports.CreateStepLogs("Passed", "Standard User: " + userExl + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateLog("User: " + userExl + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    //TMTI0093777	Verify that the FVA Supervisor who is part of the Time Tracking PV Supervisor group can access the "Time Tracking" module.

                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + user);
                    //string GetTimeRecordUserNameLV = timeEntry.GetTimeRecordUserNameLV();
                    //Assert.AreEqual(GetTimeRecordUserNameLV, user, "Verify Logged-in FVA User name is displayed on the top of Time Record Manager Page ");
                    //extentReports.CreateStepLogs("Passed", "User Name: " + GetTimeRecordUserNameLV + " is displayed on the top of Time Record Manager Page ");

                    //Select Staff Member from the list
                    string staffNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 1);

                    timeEntry.SelectStaffMemberLV(staffNameExl);
                    string staffName= timeEntry.GetSelectedStaffName();
                    Assert.AreEqual(staffNameExl, staffName);
                    extentReports.CreateStepLogs("Passed", "Staff : " + staffName + " is Selected from list ");

                    //TMTI0093765	Verify that the FVA Supervisor can add hours to the project for any user.
                    //timeEntry.GetDefaultTimeRecordPeriodLV();

                    string selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 1);
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    extentReports.CreateStepLogs("Info", "User is on Weekly Entry Matrix Page");

                    timeEntry.SelectProjectWeeklyEntryMatrixLV(selectProject);
                    extentReports.CreateStepLogs("Info", "Project: " + selectProject + " selected on Weekly Entry Matrix ");

                    //Enter time under weekly time matrix
                    timeEntry.LogCurrentDateHoursLV(fileTMTT0038660);
                    extentReports.CreateStepLogs("Passed", "Hours entered on Weekly Entry Matrix Page");

                    //TMTI0093772	Verify that the FVA Supervisor can remove the entered hours of any user.
                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");

                    //TMTI0093785	Verify that the FVA Supervisor can access the Summary Log tab and can add hours.
                    timeEntry.GoToSummaryLogLV();
                    extentReports.CreateStepLogs("Passed", "User: " + user + " is on Summary Log Page ");
                    string activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 3);
                    string hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 2);
                    //Enter time under Summary Logs
                    string textMessage = timeEntry.EnterSummaryLogsHoursLV(selectProject, activityExl, hoursExl);
                    Assert.AreEqual(textMessage, "Time Record Added");
                    extentReports.CreateStepLogs("Passed", " Hours entered on Summary Logs Page with Success Message: " + textMessage);

                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");

                    //TMTI0093766	Verify that the FVA Supervisor can access the Detail Logs tab.
                    timeEntry.GoToDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "User: " + user + " is on Detail Logs Page ");
                    textMessage = timeEntry.EnterDetailLogsHoursLV(selectProject, activityExl, hoursExl);
                    Assert.AreEqual(textMessage, "Time Record Added");
                    extentReports.CreateStepLogs("Passed", " Hours entered on Detail Logs Page with Success Message: " + textMessage);

                    // TTMTI0093773	Verify that the FVA Supervisor can update the entered activity and hours from the Detail Logs tab.
                    string newActivityExl = ReadExcelData.ReadData(excelPath, "UpdateData", 2);
                    string newHoursExl = ReadExcelData.ReadData(excelPath, "UpdateData", 1);
                    timeEntry.UpdateDetailLogsHoursLV(newActivityExl, newHoursExl);
                    extentReports.CreateStepLogs("Passed", "Activity and Hours Updated on Detail Logs Page ");
                    string txtLatestActivity = timeEntry.GetDetailLogsActivity();
                    string txtLatestHours = timeEntry.GetDetailLogsHours();
                    Assert.AreEqual(txtLatestActivity, newActivityExl);
                    Assert.AreEqual(txtLatestHours, newHoursExl);
                    extentReports.CreateStepLogs("Passed", "Activity and Hours are Updated on Detail Logs Page ");

                    ///////////////
                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");

                    //TMTI0093786	Verify that the FVA Supervisor can access the Weekly Overview tab and can see entered hours
                    timeEntry.GoToWeeklyOverviewLV();
                    extentReports.CreateStepLogs("Passed", "User: " + user + " is on Weekly Overview Page ");

                    //TMTI0093771	Verify that the FVA user can add hours from the Weekly Overview tab for the selected project and success message appears on the screen.
                    textMessage = timeEntry.EnterWeeklyOverviewHoursLV(selectProject, activityExl, hoursExl);
                    Assert.AreEqual(textMessage, "Time Record Added");
                    extentReports.CreateStepLogs("Passed", " Hours entered on Weekly Overview Page with Success Message: " + textMessage);

                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");
                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "User: " + user + " logged out");
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
