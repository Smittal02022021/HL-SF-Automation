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
    class LV_TMTT0038660_VerifyTheFunctionalityOfTimeRecordManagerForFVACAOUserOntheSFLightningView : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();

        public static string fileTMTT0038660 = "LV_TMTT0038660_VerifyTheFunctionalityOfTimeRecordManagerForFVACAOUserOntheSFLightningView";
        private string engagementExl;
        private string textMessage;
        private string selectProject;
        private string hoursExl;
        private string activityExl;

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

                int rowCount = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row <= rowCount; row++)
                {
                    //Navigate to Title rate sheet page
                    lvHomePage.NavigateToAnItemFromHLBankerDropdown("Title Rate Sheets");
                    extentReports.CreateStepLogs("Info", driver.Title + " page is displayed ");

                    string rateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 2);
                    string initialValue = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 3);
                    
                    //Selecting the desired Rate Sheet 
                    rateSheetMgt.SelectAListView("All");
                    extentReports.CreateStepLogs("Info", "All list view is selected");

                    rateSheetMgt.SelectRateSheet(rateSheetExl);
                    extentReports.CreateStepLogs("Info", "User Selected the " + rateSheetExl);

                    //Get the default rate of user as per role
                    string staffRole = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 2);
                    double defaultRate = rateSheetMgt.GetDefaultRateAsPerRole(staffRole);
                    extentReports.CreateStepLogs("Info", staffRole + " is : USD " + defaultRate + " ");

                    //Login as CF Financial User profile and validate the user
                    string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    string userGrpNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);

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
                    extentReports.CreateStepLogs("Passed", "CAO User: " + userExl + " from Time Tracking Group: " + userGrpNameExl + "  logged in ");

                    //Select App
                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    //TMTI0093774	Verify that the FVA CAO who is part of the Time Tracking Beta Supervisor group can access the "Time Tracking" module.
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + userExl);
                    
                    //Select Staff Member from the list
                    string staffNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 1);
                    timeEntry.SelectStaffMemberLV(staffNameExl);
                    string staffName = timeEntry.GetSelectedStaffNameLV();
                    Assert.AreEqual(staffNameExl, staffName);
                    extentReports.CreateStepLogs("Passed", "Staff : " + staffName + " is Selected from list ");

                    //Enter Rate Sheet details
                    //TMTI0093787	Verify that the FVA CAO can access the Rate Sheet Management and can add Rate Sheet for the selected Engagement for the selected period.
                    engagementExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 1);
                    rateSheetMgt.EnterRateSheetLV(engagementExl, rateSheetExl);

                    //Verify selected rate sheet
                    string selectedRateSheet = rateSheetMgt.GetSelectedRateSheetLV();
                    Assert.AreEqual(rateSheetExl, selectedRateSheet);
                    extentReports.CreateStepLogs("Passed", "Rate Sheet added for Project: "+ engagementExl);
                    timeEntry.GoToStaffTimeSheetTabLV();

                    //TMTI0093780	Verify that the FVA CAO can check, add, and remove hours of the selected staff users
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    extentReports.CreateStepLogs("Info", "User is on Weekly Entry Matrix Page");
                    selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "WeeklyEntryMatrix", row, 1);
                    hoursExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 2);
                    timeEntry.SelectProjectWeeklyEntryMatrixLV(selectProject);
                    extentReports.CreateStepLogs("Info", "Project: " + selectProject + " selected on Weekly Entry Matrix ");
                    
                    //Enter time under weekly time matrix
                    if (userGrpNameExl == "Time Tracking TFR Supervisor")
                    {
                        extentReports.CreateStepLogs("Info", "Activity List is not available for TFR group");
                        timeEntry.LogCurrentDateHoursTFRGroupLV(hoursExl);
                        extentReports.CreateStepLogs("Passed", "Hours entered on Weekly Entry Matrix Page");
                    }
                    else
                    {
                        timeEntry.LogCurrentDateHoursLV(hoursExl);
                        extentReports.CreateStepLogs("Passed", "Hours entered on Weekly Entry Matrix Page");
                    }                                         
                    double expectedBilledAmount = Convert.ToDouble(hoursExl) *Convert.ToDouble(defaultRate);
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

                    timeEntry.GoToStaffTimeSheetTabLV();
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    bool IsEntryDeleted= timeEntry.ClickDeleteAndCancel();                    
                    Assert.IsFalse(IsEntryDeleted, "Verify that on clicking the Cancel button from confirmation message will not remove the entry");
                    extentReports.CreateStepLogs("Passed", "Clicking the Cancel button from confirmation message will not remove the entry");
                                        
                    IsEntryDeleted = timeEntry.ClickDeleteAndOK();
                    Assert.IsTrue(IsEntryDeleted, "Verify that on clicking the OK button from confirmation message will remove the entry");
                    extentReports.CreateStepLogs("Passed", "Clicking the OK button from confirmation message will remove the entry");
                    
                    timeEntry.GoToSummaryLogLV();
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " is on Summary Log Page ");                    
                    selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 1);
                    hoursExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 2);
                    activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 3);

                    //Enter time under Summary Logs
                    if (userGrpNameExl == "Time Tracking TFR Supervisor")
                    {
                        extentReports.CreateStepLogs("Info", "Activity List is not available for TFR group");
                        textMessage = timeEntry.EnterSummaryLogsHoursTFRGroupLV(selectProject, hoursExl);
                        Assert.AreEqual(textMessage, "Time Record Added");
                        extentReports.CreateStepLogs("Passed", " Hours entered on Summary Logs Page with Success Message: " + textMessage);
                    }
                    else
                    {
                        textMessage = timeEntry.EnterSummaryLogsHoursLV(selectProject, activityExl, hoursExl);
                        Assert.AreEqual(textMessage, "Time Record Added");
                        extentReports.CreateStepLogs("Passed", " Hours entered on Summary Logs Page with Success Message: " + textMessage);
                    }
                    //S3	Verify that the FVA CAO can see the calculated on Summay logs amount as per the rate sheet
                    double actualSummaryLogsBilledAmount = timeEntry.GetTotalAmountLV();
                    Assert.AreEqual(expectedBilledAmount, actualSummaryLogsBilledAmount,"Verify the Amount is calculaton as per the selected Rate sheet on Summary Logs page ");
                    extentReports.CreateStepLogs("Passed", "Amount is calculaton as per the selected Rate sheet on Summary Logs page");
                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");

                    //Verify that the FVA user can access the Detail Logs tab.
                    timeEntry.GoToDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " is on Detail Logs Page ");
                    selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 1);
                    hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs",row, 2);
                    activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 3);

                    //Verify that the FVA user can add hours from the Detail Logs tab and a success message appears on the screen.
                    if (userGrpNameExl == "Time Tracking TFR Supervisor")
                    {
                        extentReports.CreateStepLogs("Info", "Activity List is not available for TFR group");
                        textMessage = timeEntry.EnterDetailLogsHoursTFRGroupLV(selectProject, hoursExl);
                        Assert.AreEqual(textMessage, "Time Record Added");
                        extentReports.CreateStepLogs("Passed", " Hours entered on Summary Logs Page with Success Message: " + textMessage);
                    }
                    else
                    {
                        textMessage = timeEntry.EnterDetailLogsHoursLV(selectProject, activityExl, hoursExl);
                        Assert.AreEqual(textMessage, "Time Record Added");
                        extentReports.CreateStepLogs("Passed", " Hours entered on Detail Logs Page with Success Message: " + textMessage);
                    }

                    //S4	Verify that the FVA CAO can see the calculated on Detail Logs logs amount as per the rate sheet
                    double actualDetailLogsBilledAmount = timeEntry.GetTotalAmountLV();
                    Assert.AreEqual(expectedBilledAmount, actualDetailLogsBilledAmount, "Verify the Amount is calculaton as per the selected Rate sheet on Detail Logs page ");
                    extentReports.CreateStepLogs("Passed", "Amount is calculaton as per the selected Rate sheet on Detail Logs page");
                    timeEntry.RemoveRecordFromDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted from Detail Logs Page");

                    timeEntry.GoToStaffTimeSheetTabLV();
                    rateSheetMgt.DeleteRateSheetLV(engagementExl);
                    extentReports.CreateStepLogs("Passed", "Ratesheet: "+ rateSheetExl+ " deleted from Ratesheet Manageement page");

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
