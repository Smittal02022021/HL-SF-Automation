using SF_Automation.Pages.Common;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using NUnit.Framework;
using SF_Automation.TestData;
using System;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using Microsoft.Office.Interop.Excel;
using AventStack.ExtentReports.Gherkin.Model;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace SF_Automation.TestCases.TimeRecordManager
{
    
    class LV_TMTT0038660_VerifyTheFunctionalityOfTimeRecordManagerForFVACAOUserOntheSFLightningView : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();

        public static string fileTMTT0038660 = "LV_TMTT0038660_VerifyTheFunctionalityOfTimeRecordManagerForFVACAOUserOntheSFLightningView";
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
        public void VerifyTheFunctionalityOfTimeRecordManagerForFVACAOUserLV()
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
                // select the Rate from rate sheet
                rateSheetMgt.NavigateToTitleRateSheetsPage();
                extentReports.CreateLog(driver.Title + " page is displayed ");

                //Click on the new title rate sheet name
                string rateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", 2, 2);

                string initialValue = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", 2, 3);
                rateSheetMgt.SelectSheetIntials(initialValue);
                extentReports.CreateLog(rateSheetExl + " Rate Sheet Initials Selected ");
                //Selecting the desired Rate Sheet 
                rateSheetMgt.SelectRateSheet(rateSheetExl);
                extentReports.CreateLog("User Selected the " + rateSheetExl);

                //Get the default rate of user as per role
                string staffRole = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", 2, 2);
                double defaultRate = rateSheetMgt.GetDefaultRateAsPerRole(staffRole);
                extentReports.CreateLog(staffRole + " is : USD " + defaultRate + " ");

                int rowCount = ReadExcelData.GetRowCount(excelPath, "Users");

                for (int row = 2; row <= rowCount; row++)
                {
                    //Login as Standard User profile and validate the user
                    string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    usersLogin.SearchUserAndLogin(userExl);
                    login.SwitchToClassicView();
                    string user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(userExl), true);
                    extentReports.CreateStepLogs("Passed", "CAO User: " + userExl + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateLog("User: " + userExl + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    //TMTI0093774	Verify that the FVA CAO who is part of the Time Tracking Beta Supervisor group can access the "Time Tracking" module.
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + user);
                    
                    //Select Staff Member from the list
                    string staffNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 1);

                    timeEntry.SelectStaffMemberLV(staffNameExl);
                    string staffName = timeEntry.GetSelectedStaffName();
                    Assert.AreEqual(staffNameExl, staffName);
                    extentReports.CreateStepLogs("Passed", "Staff : " + staffName + " is Selected from list ");

                    // Select the rate sheet
                    string selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 1);
                    //Enter Rate Sheet details
                    //TMTI0093787	Verify that the FVA CAO can access the Rate Sheet Management and can add Rate Sheet for the selected Engagement for the selected period.
                    rateSheetMgt.EnterRateSheetLV(selectProject, rateSheetExl);

                    //Verify selected rate sheet
                    string selectedRateSheet = rateSheetMgt.GetSelectedRateSheetLV();
                    //string selectedRateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", 1);
                    Assert.AreEqual(rateSheetExl, selectedRateSheet);
                    timeEntry.GoToStaffTimeSheetTabLV();

                    //TMTI0093780	Verify that the FVA CAO can check, add, and remove hours of the selected staff users
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    extentReports.CreateStepLogs("Info", "User is on Weekly Entry Matrix Page");
                    timeEntry.SelectProjectWeeklyEntryMatrixLV(selectProject);
                    extentReports.CreateStepLogs("Info", "Project: " + selectProject + " selected on Weekly Entry Matrix ");
                    string txtHours = ReadExcelData.ReadData(excelPath, "SummaryLogs", 2);
                    //Enter time under weekly time matrix
                    timeEntry.LogCurrentDateHoursLV(txtHours);
                    extentReports.CreateStepLogs("Passed", "Hours entered on Weekly Entry Matrix Page");
                                    
                    //double billedAmount = rateSheetMgt.GetBillingAmountFromBillingPreparationTabLV();
                    
                    double expectedBilledAmount = Convert.ToDouble(txtHours) *Convert.ToDouble(defaultRate);
                    extentReports.CreateStepLogs("Passed", "Expected Calculated Amount for the Selected sheet should be:: "+ expectedBilledAmount);

                    //TMTI0093768 Verify that the FVA CAO can access the Billing Preparation tab and see Send Notification Button is only enabled if any record is selected from list.

                    rateSheetMgt.GoToBillingPreparationTabLV();
                    bool btnStatus= rateSheetMgt.GetSendNotificatioButtonStatusLV();
                    Assert.IsFalse(btnStatus, "Verify Send Notification Button is default Disabled ");
                    extentReports.CreateStepLogs("Passed", "Send Notification Button is default Disabled");

                    rateSheetMgt.SelectBillingPreparationRecordLV();
                    extentReports.CreateStepLogs("Info", "Record Selected from list on Billing Preparation tab");

                    btnStatus = rateSheetMgt.GetSendNotificatioButtonStatusLV();
                    Assert.IsTrue(btnStatus, "Verify Send Notification Button is default Disabled ");
                    extentReports.CreateStepLogs("Passed", "Send Notification Button is Enabled after selecting any record from list on Billing Preparation tab");

                    //Assert.AreEqual(expectedAmount, billedAmount);
                    timeEntry.GoToStaffTimeSheetTabLV();
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    bool IsEntryDeleted= timeEntry.ClickDeleteAndCancel();                    
                    Assert.IsFalse(IsEntryDeleted, "Verify that on clicking the Cancel button from confirmation message will not remove the entry");
                    extentReports.CreateStepLogs("Passed", "Clicking the Cancel button from confirmation message will not remove the entry");
                                        
                    IsEntryDeleted = timeEntry.ClickDeleteAndOK();
                    Assert.IsTrue(IsEntryDeleted, "Verify that on clicking the OK button from confirmation message will remove the entry");
                    extentReports.CreateStepLogs("Passed", "Clicking the OK button from confirmation message will remove the entry");
                    engagementExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 1);
                    
                    //timeEntry.GoToStaffTimeSheetTabLV();
                    timeEntry.GoToSummaryLogLV();
                    extentReports.CreateStepLogs("Passed", "User: " + user + " is on Summary Log Page ");
                    string activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 3);
                    string hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 2);
                    
                    //Enter time under Summary Logs
                    string textMessage = timeEntry.EnterSummaryLogsHoursLV(selectProject, activityExl, hoursExl);
                    Assert.AreEqual(textMessage, "Time Record Added");
                    extentReports.CreateStepLogs("Passed", " Hours entered on Summary Logs Page with Success Message: " + textMessage);

                    //S3	Verify that the FVA CAO can see the calculated on Summay logs amount as per the rate sheet
                    double actualSummaryLogsBilledAmount = timeEntry.GetTotalAmountLV();
                    Assert.AreEqual(expectedBilledAmount, actualSummaryLogsBilledAmount,"Verify the Amount is calculaton as per the selected Rate sheet on Summary Logs page ");
                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");

                    //Verify that the FVA user can access the Detail Logs tab.
                    timeEntry.GoToDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "User: " + user + " is on Detail Logs Page ");

                    //Verify that the FVA user can add hours from the Detail Logs tab and a success message appears on the screen.
                    textMessage = timeEntry.EnterDetailLogsHoursLV(selectProject, activityExl, hoursExl);
                    Assert.AreEqual(textMessage, "Time Record Added");
                    extentReports.CreateStepLogs("Passed", " Hours entered on Detail Logs Page with Success Message: " + textMessage);
                    //S4	Verify that the FVA CAO can see the calculated on Detail Logs logs amount as per the rate sheet
                    
                    double actualDetailLogsBilledAmount = timeEntry.GetTotalAmountLV();
                    Assert.AreEqual(expectedBilledAmount, actualDetailLogsBilledAmount, "Verify the Amount is calculaton as per the selected Rate sheet on Detail Logs page ");
                    
                    timeEntry.RemoveRecordFromDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted from Detail Logs Page");

                    timeEntry.GoToStaffTimeSheetTabLV();
                    rateSheetMgt.DeleteRateSheetLV(engagementExl);
                    extentReports.CreateStepLogs("Passed", "Ratesheet: "+ rateSheetExl+ " deleted from Ratesheet Manageement page");

                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "User: " + user + " logged out");
                    
                }
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                timeEntry.GoToStaffTimeSheetTabLV();
                timeEntry.DeleteTimeEntryLV();
                rateSheetMgt.DeleteRateSheetLV(engagementExl);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
