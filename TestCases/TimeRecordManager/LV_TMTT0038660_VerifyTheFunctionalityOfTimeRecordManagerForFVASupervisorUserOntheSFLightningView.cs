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
        /*
            Time Tracking PV Supervisor
            Time Tracking Litigation Supervisor 
            Time Tracking Beta Supervisor 
         */
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        LVHomePage lvHomePage = new LVHomePage();

        public static string fileTMTT0038660 = "LV_TMTT0038660_VerifyTheFunctionalityOfTimeRecordManagerForFVASupervisorUserOntheSFLightningView";
        string engagementExl;

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

                ////Click on the new title rate sheet name               
                int rowCount = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row <= rowCount; row++)
                {
                    //Login as Standard User profile and validate the user
                    string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    string userGrpNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);
                    string rateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 2);

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
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " from Time Tracking Group: " + userGrpNameExl + "  logged in ");

                    //Select App
                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    //TMTI0093777	Verify that the FVA Supervisor who is part of the Time Tracking PV Supervisor group can access the "Time Tracking" module.
                    string moduleNameExl = ReadExcelData.ReadData(excelPath, "ModuleName",1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + userExl);
                    
                    //Select Staff Member from the list
                    string staffNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 1);
                    timeEntry.SelectStaffMemberLV(staffNameExl);
                    string staffName= timeEntry.GetSelectedStaffNameLV();
                    Assert.AreEqual(staffNameExl, staffName);
                    extentReports.CreateStepLogs("Passed", "Staff : " + staffName + " is Selected from list ");

                    // Select the rate sheet
                    string selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 1);
                    timeEntry.GoToWeeklyEntryMatrixLV();

                    //TMTI0093784	Verify the Time Record Period is Defaulted to the Current Week for FVA Supervisor.
                    string defaultTimeRecordPeriod = timeEntry.GetDefaultTimeRecordPeriodLV();
                    extentReports.CreateStepLogs("Info", "Default selected Time Record Period Start Date : " + defaultTimeRecordPeriod);
                    string weekStartDate = timeEntry.GetWeekStartDateLV();
                    extentReports.CreateStepLogs("Info", "Actual Week Start Date: " + weekStartDate);
                    Assert.AreEqual(weekStartDate, defaultTimeRecordPeriod);
                    extentReports.CreateStepLogs("Passed", "Default Time Record Period Start Date is same as System Week Start Date:" + defaultTimeRecordPeriod);

                    //TMTI0093765	Verify that the FVA Supervisor can add hours to the project for any user.                    
                    extentReports.CreateStepLogs("Info", "User is on Weekly Entry Matrix Page");
                    timeEntry.SelectProjectWeeklyEntryMatrixLV(selectProject);
                    extentReports.CreateStepLogs("Info", "Project: " + selectProject + " selected on Weekly Entry Matrix ");
                    string txtHours = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 2);

                    //Enter time under weekly time matrix
                    timeEntry.LogCurrentDateHoursLV(txtHours);
                    extentReports.CreateStepLogs("Passed", "Hours entered on Weekly Entry Matrix Page");                    
                    bool IsEntryDeleted = timeEntry.ClickDeleteAndCancel();
                    Assert.IsFalse(IsEntryDeleted, "Verify that on clicking the Cancel button from confirmation message will not remove the entry");
                    extentReports.CreateStepLogs("Passed", "Clicking the Cancel button from confirmation message will not remove the entry");

                    //TMTI0093772	Verify that the FVA Supervisor can remove the entered hours of any user.
                    IsEntryDeleted = timeEntry.ClickDeleteAndOK();
                    Assert.IsTrue(IsEntryDeleted, "Verify that on clicking the OK button from confirmation message will remove the entry");
                    extentReports.CreateStepLogs("Passed", "Clicking the OK button from confirmation message will remove the entry");
                    engagementExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 1);

                    //TMTI0093785	Verify that the FVA Supervisor can access the Summary Log tab and can add hours.
                    timeEntry.GoToSummaryLogLV();
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " is on Summary Log Page ");
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
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " is on Detail Logs Page ");
                    textMessage = timeEntry.EnterDetailLogsHoursLV(selectProject, activityExl, hoursExl);
                    Assert.AreEqual(textMessage, "Time Record Added");
                    extentReports.CreateStepLogs("Passed", " Hours entered on Detail Logs Page with Success Message: " + textMessage);

                    //TMTI0093773	Verify that the FVA Supervisor can update the entered activity and hours from the Detail Logs tab.
                    string newHoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateData", row, 1);
                    string newActivityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateData",row, 2);                      
                    timeEntry.UpdateDetailLogsHoursLV(newActivityExl, newHoursExl);
                    extentReports.CreateStepLogs("Passed", "Activity and Hours Updated on Detail Logs Page ");
                    string txtLatestActivity = timeEntry.GetDetailLogsActivity();
                    string txtLatestHours = timeEntry.GetDetailLogsHours();
                    Assert.AreEqual(txtLatestActivity, newActivityExl);
                    Assert.AreEqual(txtLatestHours, newHoursExl);
                    extentReports.CreateStepLogs("Passed", "Activity and Hours are Updated on Detail Logs Page ");

                    //TMTI0093779	Verify that the FVA Supervisor can delete entered hours from the Detail Logs tab
                    timeEntry.RemoveRecordFromDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted from Detail Logs Page");

                    //TMTI0093786	Verify that the FVA Supervisor can access the Weekly Overview tab and can see entered hours
                    timeEntry.GoToWeeklyOverviewLV();
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " is on Weekly Overview Page ");

                    //TMTI0093767	Verify that the FVA Supervisor can add hours from the Weekly Overview tab for the selected project and success message appears on the screen.
                    textMessage = timeEntry.EnterWeeklyOverviewHoursLV(selectProject, activityExl, hoursExl);
                    Assert.AreEqual(textMessage, "Time Record Added");
                    extentReports.CreateStepLogs("Passed", " Hours entered on Weekly Overview Page with Success Message: " + textMessage);

                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");

                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " logged out");

                    //Select HL Banker app
                    try
                    {
                        lvHomePage.SelectAppLV("HL Banker");
                    }
                    catch(Exception)
                    {
                        lvHomePage.SelectAppLV1("HL Banker");
                    }
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
