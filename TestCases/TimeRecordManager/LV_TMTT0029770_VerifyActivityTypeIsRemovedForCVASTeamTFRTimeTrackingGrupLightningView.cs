﻿using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class LV_TMTT0029770_VerifyActivityTypeIsRemovedForCVASTeamTFRTimeTrackingGrupLightningView: BaseClass
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
        public static string fileTMTI0069005 = "LV_TMTT0029770_VerifyUserCanSearchProjectWithNameOnTimeRecordManager";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyActivityIsRemovedForTRFGroupuser()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0069005;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();
                string TimeRecordManagerUser = login.ValidateUser();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3,1);
                usersLogin.SearchUserAndLogin(userExl);

                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(userExl), true);
                extentReports.CreateStepLogs("Passed", " User: " + userExl + " logged in ");
                homePageLV.ClickAppLauncher();

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectApp(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + userExl);
                int rowSearchValue = ReadExcelData.GetRowCount(excelPath, "ProjectActivity");

                for (int row = 2; row <= rowSearchValue; row++)
                {
                    //Existing Engagement with Deal team member of Logged in user
                    string valueProjectExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ProjectActivity", row, 1);
                    extentReports.CreateLog("Project Searching with Number: " + valueProjectExl + " ");
                    //Weekly Entry Matrix
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    string projectFullname = timeEntry.SearchProjectandGetFullNameLV(valueProjectExl);
                    string clientNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ProjectActivity", row, 2);
                    Assert.IsTrue(projectFullname.Contains(clientNameExl), "Verify that the Client Name is added to the project full name on Time Record Manger");
                    extentReports.CreateStepLogs("Passed", "Client Name: " + clientNameExl + " is added to the Project full name: " + projectFullname + " on Time Record Manger on Weekly Entry Matrix ");

                    //TMTI0069008	Verify that the activity type is removed for the CVAS team(TFR time tracking).

                    Assert.IsFalse(timeEntry.IsActivityListDisplayedLV(projectFullname), "Verify Activity List is not displayed for logged in user");
                    extentReports.CreateLog("Activity List is not displayed for logged in user on Weekly Entry Matrix ");

                    //Time Clock Recorder
                    refreshButton.GoToTimeClockRecorderPageLV();
                    projectFullname = timeEntry.SearchProjectandGetFullNameLV(valueProjectExl);
                    Assert.IsTrue(projectFullname.Contains(clientNameExl), "Verify that the Client Name is added to the project full name on Time Record Manger");
                    extentReports.CreateStepLogs("Passed", "Client Name: " + clientNameExl + " is added to the Project full name: " + projectFullname + " on Time Record Manger on Time Clock Recorder ");

                    //TMTI0069008	Verify that the activity type is removed for the CVAS team(TFR time tracking).
                    Assert.IsFalse(timeEntry.IsActivityListDisplayedLV(projectFullname), "Verify Activity List is not displayed for logged in user");
                    extentReports.CreateStepLogs("Passed", "Activity List is not displayed for logged in user on Time Clock Recorder ");

                    //Summary Logs
                    timeEntry.GoToSummaryLogLV();
                    projectFullname = timeEntry.SearchProjectandGetFullNameLV(valueProjectExl);
                    Assert.IsTrue(projectFullname.Contains(clientNameExl), "Verify that the Client Name is added to the project full name on Time Record Manger");
                    extentReports.CreateStepLogs("Passed", "Client Name: " + clientNameExl + " is added to the Project full name: " + projectFullname + " on Time Record Manger on Summary Logs ");

                    //TMTI0069008	Verify that the activity type is removed for the CVAS team(TFR time tracking).
                    Assert.IsFalse(timeEntry.IsActivityListDisplayedLV(projectFullname), "Verify Activity List is not displayed for logged in user");
                    extentReports.CreateStepLogs("Passed", "Activity List is not displayed for logged in user on Summary Logs ");

                    //Detail Logs
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    timeEntry.GoToDetailLogsLV();
                    projectFullname = timeEntry.SearchProjectandGetFullNameLV(valueProjectExl);
                    Assert.IsTrue(projectFullname.Contains(clientNameExl), "Verify that the Client Name is added to the project full name on Time Record Manger");
                    extentReports.CreateStepLogs("Passed", "Client Name: " + clientNameExl + " is added to the Project full name: " + projectFullname + " on Time Record Manger on Detail Logs ");

                    //TMTI0069008	Verify that the activity type is removed for the CVAS team(TFR time tracking).
                    Assert.IsFalse(timeEntry.IsActivityListDisplayedLV(projectFullname), "Verify Activity List is not displayed for logged in user");
                    extentReports.CreateStepLogs("Passed", "Activity List is not displayed for logged in user on Detail Logs ");

                    //Weekly Overview
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    timeEntry.GoToWeeklyOverviewLV();
                    projectFullname = timeEntry.SearchProjectandGetFullNameLV(valueProjectExl);
                    Assert.IsTrue(projectFullname.Contains(clientNameExl), "Verify that the Client Name is added to the project full name on Time Record Manger");
                    extentReports.CreateStepLogs("Passed", "Client Name: " + clientNameExl + " is added to the Project full name: " + projectFullname + " on Time Record Manger on Weekly Overview ");

                    //TMTI0069008	Verify that the activity type is removed for the CVAS team(TFR time tracking).
                    Assert.IsFalse(timeEntry.IsActivityListDisplayedLV(projectFullname), "Verify Activity List is not displayed for logged in user");
                    extentReports.CreateStepLogs("Passed", "Activity List is not displayed for logged in user on Weekly Overview ");
                }
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "User: " + userExl + " logged out");
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