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

                    // select the Rate from rate sheet
                    rateSheetMgt.NavigateToTitleRateSheetsPage();
                    extentReports.CreateLog(driver.Title + " page is displayed ");

                    //Click on the new title rate sheet name
                    string rateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 2);
                    rateSheetMgt.ClickNewTitleRateSheet(rateSheetExl);
                    extentReports.CreateStepLogs("Info", "Rate sheet: " + rateSheetExl + " is selected. ");

                    //Verify the correct title rate sheet is opened
                    //Assert.AreEqual(WebDriverWaits.TitleContains(driver, rateSheet), true);
                    //extentReports.CreateLog(driver.Title + " page is displayed ");

                    //Get the default rate of user as per role
                    string staffRole = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 2);
                    double defaultRate = rateSheetMgt.GetDefaultRateAsPerRole(staffRole);
                    extentReports.CreateLog(staffRole + " is : USD " + defaultRate + " ");


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

                    //get amount on Billing Prepration tag 
                    double billedAmount = rateSheetMgt.GetBillingAmountFromBillingPreparationTabLV();
                    double expectedAmount = Convert.ToDouble(txtHours) *Convert.ToDouble(defaultRate);
                    Assert.AreEqual(expectedAmount, billedAmount);
                    timeEntry.GoToStaffTimeSheetTabLV();
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    bool IsEntryDeleted= timeEntry.ClickDeleteAndCancel();                    
                    Assert.IsFalse(IsEntryDeleted, "Verify that on clicking the Cancel button from confirmation message will not remove the entry");
                    extentReports.CreateStepLogs("Passed", "Clicking the Cancel button from confirmation message will not remove the entry");

                    IsEntryDeleted = timeEntry.ClickDeleteAndOK();
                    Assert.IsTrue(IsEntryDeleted, "Verify that on clicking the OK button from confirmation message will remove the entry");
                    extentReports.CreateStepLogs("Passed", "Clicking the OK button from confirmation message will remove the entry");
                    engagementExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 1);
                    //string rateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 2);

                    //TMTI0093787	Verify that the FVA CAO can access the Rate Sheet Management and can add Rate Sheet for the selected Engagement for the selected period.
                    //rateSheetMgt.EnterRateSheetLV(engagementExl, rateSheetExl);
                    //extentReports.CreateStepLogs("Passed", "Ratesheet: "+ rateSheetExl+" Added for Engagement: "+ engagementExl+" on Ratesheet Manageement page ");

                    rateSheetMgt.DeleteRateSheetLV(engagementExl);
                    extentReports.CreateStepLogs("Passed", "Ratesheet: "+ rateSheetExl+ " deleted from Ratesheet Manageement page");

                    //TMTI0093768	Verify that the FVA CAO can access the Billing Preparation tab and see the amount as per the rate sheet
                    
                }
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
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
