using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.IO;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class LV_TMTT0011418_TMTT0011410_VerifyTASSupervisorCanMoreThan24hoursForOneDayForAssociatesWithTitleTAGOutsourcedContractorLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();

        public static string fileTMT1418 = "LV_TMTT0011418_VerifyTASSupervisorCanMoreThan24hoursForOneDayForAssociatesWithTitleTAGOutsourcedContractor";

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
        public void VerifyTASSupervisorFunctionalitiesLV() 
        {
            try
            {
                //Get path of Test data file
                string excelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestData", fileTMT1418 + ".xlsx");
                excelPath = Path.GetFullPath(excelPath);

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

                //Login as Supervisor user 
                string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                string userGrpNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);

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
                extentReports.CreateStepLogs("Passed", "Supervisor User: " + userExl + " from Time Tracking Group: " + userGrpNameExl + "  logged in ");

                //homePageLV.ClickAppLauncher();
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + userExl);

                //Select Staff Member from the list
                string staffNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", 2, 1);
                timeEntry.SelectStaffMemberLV(staffNameExl);
                string staffName = timeEntry.GetSelectedStaffNameLV();
                Assert.AreEqual(staffNameExl, staffName);
                extentReports.CreateStepLogs("Passed", "Staff : " + staffName + " is Selected from list ");

                string selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "WeeklyEntryMatrix", 2, 1);
                string txtHours= ReadExcelData.ReadData(excelPath, "Update_Timer", 1);
                timeEntry.SelectProjectWeeklyEntryMatrixLV(selectProject);
                timeEntry.LogCurrentDateHoursLV(txtHours);
                
                //Go to Summary Logs
                timeEntry.GoToSummaryLogLV();
                extentReports.CreateStepLogs("Passed", "User has navigated to Summary logs ");

                //Get Summary Log Time Entry
                string summaryLogTime = timeEntry.GetSummaryLogsTimeEntryLV();
                Assert.AreEqual(txtHours, summaryLogTime);
                extentReports.CreateStepLogs("Passed", "Hours: " + summaryLogTime + " is logged in Sumamry logs ");

                //Go to Details log
                timeEntry.GoToDetailLogsLV();
                extentReports.CreateStepLogs("Info", "User has naigated to details log ");

                //Verify detail logged hours
                string DetailLogsTime = timeEntry.GetDetailLogsTimeEntryLV();
                Double DetailLogTime = Convert.ToDouble(DetailLogsTime);

                Assert.AreEqual(Convert.ToDouble(txtHours), DetailLogTime);
                extentReports.CreateStepLogs("Passed", "Time displaying in detail log: " + DetailLogTime + " Hours ");

                //Go to Weekly Entry Matrix
                timeEntry.GoToWeeklyEntryMatrixLV();
                extentReports.CreateLog("User is navigated to Weekly Entry Matrix ");

                //Enter Rate Sheet details
                engagementExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", 2, 1);
                string rateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", 2, 2);
                rateSheetMgt.EnterRateSheetLV(engagementExl, rateSheetExl);

                //Verify selected rate sheet
                string selectedRateSheet = rateSheetMgt.GetSelectedRateSheetLV();
                //string selectedRateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", 2, 2);
                Assert.AreEqual(rateSheetExl, selectedRateSheet);
                extentReports.CreateStepLogs("Passed", "Selected Rate Sheet: " + selectedRateSheet + " is displayed upon entering rate sheet details ");

                timeEntry.GoToStaffTimeSheetTabLV();

                //Go to Summary Logs
                timeEntry.GoToSummaryLogLV();
                extentReports.CreateStepLogs("Info", "User has navigated to Summary logs ");

                //Get default rate displayed
                string DefaultRateForStaffString = timeEntry.GetDefaultRateForStaffLV();
                double DefaultRateForStaff = Convert.ToDouble(DefaultRateForStaffString);

                //Get Entered Hours displayed
                double enteredHours = timeEntry.GetEnteredHoursInSummaryLogValueLV();

                // Get total amount calculated with default rate and entered hours
                double totalAmountCalculated = DefaultRateForStaff * enteredHours;

                // Get total amount displayed
                double totalAmountDisplayed = timeEntry.GetTotalAmountLV();

                //Verify calculated and displayed amount should matches
                Assert.AreEqual(totalAmountCalculated, totalAmountDisplayed);
                extentReports.CreateStepLogs("Info", "Total Amount: " + totalAmountDisplayed + " displayed is matching with the calculation based on total hours entered and the default rate based on staff title ");

                //Go to Details log
                timeEntry.GoToDetailLogsLV();
                extentReports.CreateStepLogs("Info", "User has naigated to details log ");

                // Get total amount displayed
                double totalAmountDisplayedInDetailLog = timeEntry.GetTotalAmountLV();

                //Verify calculated and displayed amount should matches
                Assert.AreEqual(totalAmountCalculated, totalAmountDisplayed);
                extentReports.CreateStepLogs("Passed", "Total Amount: " + totalAmountDisplayedInDetailLog + " displayed is matching with the calculation based on total hours entered and the default rate based on staff title ");

                // Delete time entry records from detail log 
                timeEntry.RemoveRecordFromDetailLogsLV();
                extentReports.CreateStepLogs("Info", "Deleted record entry successfully from detail log ");

                //Delete rate sheet
                rateSheetMgt.DeleteRateSheetLV(engagementExl);
                extentReports.CreateStepLogs("Info", "Deleted rate sheet entry successfully after verification ");

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
