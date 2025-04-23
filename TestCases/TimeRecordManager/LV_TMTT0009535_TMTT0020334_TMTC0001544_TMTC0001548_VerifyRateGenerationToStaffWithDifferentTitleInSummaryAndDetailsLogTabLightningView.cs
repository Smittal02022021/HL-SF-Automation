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
    class LV_TMTT0009535_TMTT0020334_TMTC0001544_TMTC0001548_VerifyRateGenerationToStaffWithDifferentTitleInSummaryAndDetailsLogTabLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();

        public static string fileTMTT0009535 = "LV_TMTT0009535_VerifyRateGenerationToStaffWithDifferentTitleInSummaryAndDetailsLogTabLightningView";
        private string engagementExl;
        private string textMessage;
        private string selectProject;
        private string hoursExl;
        private string activityExl;
        private string rateSheetExl;
        private string userExl;
        private string userGrpNameExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyNewTitleAddedinTitleRateSheetLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0009535;

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

                //Login as supervisor
                userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                userGrpNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);

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
                extentReports.CreateStepLogs("Passed", "CAO User: " + userExl + " from Time Tracking Group: " + userGrpNameExl + "  logged in ");
                
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", userExl +" is  on Module:  " + moduleNameExl);

                int rowCount = ReadExcelData.GetRowCount(excelPath, "StaffMember");
                for (int row = 2; row <= rowCount; row++)
                {
                    //Get the default rate of user as per role
                    
                    string staffNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 1);
                    string staffTitle = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 2);
                    string defaultRateExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 3);
                    extentReports.CreateLog(staffTitle + " is : USD " + defaultRateExl + " ");

                    //Select Staff Member from the list                    
                    timeEntry.GoToStaffTimeSheetTabLV();
                    timeEntry.SelectStaffMemberLV(staffNameExl);
                    string staffName = timeEntry.GetSelectedStaffNameLV();
                    Assert.AreEqual(staffNameExl, staffName);
                    extentReports.CreateStepLogs("Passed", "Staff : " + staffName + " is Selected from list ");

                    //Enter Rate Sheet details
                    engagementExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 1);
                    rateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 2);
                    rateSheetMgt.EnterRateSheetLV(engagementExl, rateSheetExl);

                    //Verify selected rate sheet
                    string selectedRateSheet = rateSheetMgt.GetSelectedRateSheetLV();
                    Assert.AreEqual(rateSheetExl, selectedRateSheet);
                    extentReports.CreateStepLogs("Passed", "Rate Sheet added for Project: " + engagementExl);

                    timeEntry.GoToStaffTimeSheetTabLV();

                    timeEntry.GoToSummaryLogLV();
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " is on Summary Log Page for Selected Staff: "+ staffName);
                    selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 1);
                    hoursExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 2);
                    activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 3);

                    //Enter time under Summary Logs                    
                    textMessage = timeEntry.EnterSummaryLogsHoursLV(selectProject, activityExl, hoursExl);
                    Assert.AreEqual(textMessage, "Time Record Added");
                    extentReports.CreateStepLogs("Passed", " Hours entered on Summary Logs Page with Success Message: " + textMessage);

                    double expectedBilledAmount = Convert.ToDouble(hoursExl) * Convert.ToDouble(defaultRateExl);
                    extentReports.CreateStepLogs("Passed", "Expected Calculated Amount with the Selected sheet for Staff Title: " + staffTitle + " should be:: " + expectedBilledAmount);

                    //TMTI0019541  Verify that the FVA Supervisor can see the calculated on Summay logs amount as per the rate sheet
                    double actualSummaryLogsBilledAmount = timeEntry.GetTotalAmountLV();
                    Assert.AreEqual(expectedBilledAmount, actualSummaryLogsBilledAmount, "Verify the Amount is calculaton as per the selected Rate sheet on Summary Logs page ");
                    extentReports.CreateStepLogs("Passed", "Amount is calculaton as per the selected Rate sheet on Summary Logs page");

                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");

                    //Verify that the FVA user can access the Detail Logs tab.
                    timeEntry.GoToDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " is on Detail Logs Pagefor Selected Staff: " + staffName);
                    selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 1);
                    hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 2);
                    activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 3);
                    
                    textMessage = timeEntry.EnterDetailLogsHoursLV(selectProject, activityExl, hoursExl);
                    Assert.AreEqual(textMessage, "Time Record Added");
                    extentReports.CreateStepLogs("Passed", " Hours entered on Detail Logs Page with Success Message: " + textMessage);

                    //TMTI0045475 Verify that the FVA Supervisor can see the calculated on Detail Logs logs amount as per the rate sheet
                    double actualDetailLogsBilledAmount = timeEntry.GetTotalAmountLV();
                    Assert.AreEqual(expectedBilledAmount, actualDetailLogsBilledAmount, "Verify the Amount is calculaton as per the selected Rate sheet on Detail Logs page ");
                    extentReports.CreateStepLogs("Passed", "Amount is calculaton as per the selected Rate sheet on Detail Logs page");

                    timeEntry.RemoveRecordFromDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted from Detail Logs Page");

                    timeEntry.GoToStaffTimeSheetTabLV();
                    rateSheetMgt.DeleteRateSheetLV(engagementExl);
                    extentReports.CreateStepLogs("Passed", "Ratesheet: " + rateSheetExl + " deleted from Ratesheet Manageement page");                    
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
