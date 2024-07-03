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
    class LV_TMTT0011419_TMTT0011420_TMTT0011424_TMTT0011414_TMTT0011415_VerifyTASSupervisorCanonlyEnterLessThan24HoursForOneDayforAssociatesLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages  = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTMTT0011419 = "LV_TMTT0011419_VerifyTASSupervisorCanonlyEnterLessThan24HoursForOneDayforAssociatesLightningView";

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
        public void VerifyTASSupervisorFunctionalitiesforLessThan24HoursLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0011419;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                //Login as Supervisor user 
                string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                string userGrpNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);
                //usersLogin.SearchUserAndLogin(userExl);
                homePage.SearchUserByGlobalSearchN(userExl);
                extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();

                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(userExl), true);
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
                randomPages.WaitForPageLoaderLV();
                timeEntry.GoToWeeklyEntryMatrixLV();
                string selectProject = ReadExcelData.ReadData(excelPath, "Project_Title", 1);
                string txtHours = ReadExcelData.ReadData(excelPath, "Update_Timer", 1);

                timeEntry.SelectProjectWeeklyEntryMatrixLV(selectProject);
                timeEntry.LogCurrentDateHoursLV(txtHours);
                extentReports.CreateStepLogs("Passed", "Hours: " + txtHours + " is logged on Weekly Entry Matrix Page ");

                //Go to Summary Logs
                timeEntry.GoToSummaryLogLV();
                extentReports.CreateStepLogs("Info", "User has navigated to Summary logs ");

                //Get Summary Log Time Entry
                double summaryLogTime = timeEntry.GetEnteredHoursInSummaryLogValueLV();

                Assert.AreEqual(Convert.ToDouble(txtHours), summaryLogTime);
                extentReports.CreateStepLogs("Passed", "Added Hours: " + summaryLogTime + " are available on Sumamry logs ");

                //Go to Details log
                timeEntry.GoToDetailLogsLV();
                extentReports.CreateStepLogs("Info", "User has navigated to details log ");

                //Verify detail logged hours
                string DetailLogsTime = timeEntry.GetDetailLogsTimeEntryLV();
                double DetailLogTime = Convert.ToDouble(DetailLogsTime);
                //double extectedhours = Convert.ToDouble(txtHours);
                Assert.AreEqual(Convert.ToDouble(txtHours), DetailLogTime);
                extentReports.CreateStepLogs("Passed", "Time displaying in detail log: " + DetailLogsTime + " Hours ");                

                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntryLV();
                extentReports.CreateStepLogs("Info", "User has deleted the record ");
                
                //Add the Weekly Entry Matrix more than 24 hrs
                selectProject = ReadExcelData.ReadData(excelPath, "Project_Title", 1);
                txtHours = ReadExcelData.ReadData(excelPath, "Update_Hours", 1);
                timeEntry.SelectProjectWeeklyEntryMatrixLV(selectProject);
                string weekDay = timeEntry.LogCurrentDateHoursLV(txtHours); //timeEntry.LogFutureDateHoursLV(txtHours);
                extentReports.CreateStepLogs("Info", "Trying to add " + txtHours + " hours on Weekly Matrix Entry Page ");                
                string boarderColor = timeEntry.GetBorderColorTimeEntryLV(weekDay);
                Assert.AreEqual(boarderColor, "Red");
                extentReports.CreateStepLogs("Passed", "Hour's Input has color: " + boarderColor+" for hours: "+ txtHours);

                //TMTT0011420 Verify User other than Title TAG Outsourced Contractor cannot enter more than 24 hours on Summay Logs Page
                string activityExl = ReadExcelData.ReadData(excelPath, "Project_Title", 2);
                timeEntry.GoToSummaryLogLV();
                extentReports.CreateStepLogs("Info", "User has navigated to Summary logs ");
                string textMessage = timeEntry.EnterSummaryLogsHoursLV(selectProject, activityExl, txtHours);
                Assert.AreNotEqual(textMessage, "Time Record Added");
                extentReports.CreateStepLogs("Passed", "User other than Title TAG Outsourced Contractor cannot enter more than 24 hours on Summay Logs Page");

                ////TMTT0011420 Verify User other than Title TAG Outsourced Contractor cannot enter more than 24 hours on Detail Logs Page
                timeEntry.GoToWeeklyEntryMatrixLV();
                timeEntry.GoToDetailLogsLV();
                extentReports.CreateStepLogs("Info", "User has naigated to details log ");
                textMessage = timeEntry.EnterDetailLogsHoursLV(selectProject, activityExl, txtHours);
                Assert.AreNotEqual(textMessage, "Time Record Added");
                extentReports.CreateStepLogs("Passed", "User other than Title TAG Outsourced Contractor cannot enter more than 24 hours on Summay Logs Page");

                //Verify the TAS Supervisor can only enter hours less than 24 hours for one day for Associates
                //Select Staff Member from the list
                string staffNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", 2, 1);
                timeEntry.SelectStaffMemberLV(staffNameExl);
                string selectedStaffMember = timeEntry.GetSelectedStaffNameLV();
                Assert.AreEqual(staffNameExl, selectedStaffMember);
                extentReports.CreateStepLogs("Passed", "Staff Member Title: " + selectedStaffMember + " is displayed upon click of staff Member link ");

                //Go to Weekly Entry Matrix
                timeEntry.GoToWeeklyEntryMatrixLV();
                selectProject = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 1);                
                timeEntry.SelectProjectWeeklyEntryMatrixLV(selectProject);

                //Add the Weekly Entry Matrix more than 24 hrs
                weekDay = timeEntry.LogFutureDateHoursLV(txtHours);
                extentReports.CreateStepLogs("Info", "Trying to add " + txtHours + " hours on Weekly Matrix Entry Page ");

                //Get border color of weekly entry
                boarderColor = timeEntry.GetBorderColorTimeEntryLV(weekDay);
                Assert.AreEqual(boarderColor, "Red");//Red
                extentReports.CreateStepLogs("Passed", "Hour's Input has color: " + boarderColor + " for hours: " + txtHours);                

                //Verify Time Entires provided on Weekly time sheet by TAS Supervisor user is reflecting on Summary and Detail Log tabs for selected associate
                txtHours = ReadExcelData.ReadData(excelPath, "Update_Timer", 1);                 
                timeEntry.LogCurrentDateHoursLV(txtHours);
                driver.Navigate().Refresh();
                randomPages.WaitForPageLoaderLV();
                timeEntry.SelectStaffMemberLV(staffNameExl);
                string selectedStaffMember1 = timeEntry.GetSelectedStaffNameLV();
                Assert.AreEqual(staffNameExl, selectedStaffMember);
                extentReports.CreateStepLogs("Passed", "Staff Member Title: " + selectedStaffMember + " is displayed upon click of staff Member link ");

                //Go to Summary Logs
                timeEntry.GoToSummaryLogLV();
                extentReports.CreateStepLogs("Info", "User has navigated to Summary logs ");

                //Get Summary Log Time Entry
                summaryLogTime = timeEntry.GetEnteredHoursInSummaryLogValueLV();
                Assert.AreEqual(Convert.ToDouble(txtHours), summaryLogTime);
                extentReports.CreateStepLogs("Passed", "Added Hours: " + summaryLogTime + " are available on Sumamry logs ");

                //Go to Details log
                timeEntry.GoToDetailLogsLV();
                extentReports.CreateStepLogs("Info", "User has naigated to details log ");

                //Verify detail logged hours
                DetailLogsTime = timeEntry.GetDetailLogsTimeEntryLV();
                DetailLogTime = Convert.ToDouble(DetailLogsTime);
                Assert.AreEqual(Convert.ToDouble(txtHours), DetailLogTime);
                extentReports.CreateStepLogs("Passed", "Added Hours: " + DetailLogTime + " are available on Detail logs Page");

                //Enter Rate Sheet details
                string engagementExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", 2, 1);
                string rateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", 2, 2);
                rateSheetMgt.EnterRateSheetLV(engagementExl, rateSheetExl);

                //Verify selected rate sheet
                string selectedRateSheet = rateSheetMgt.GetSelectedRateSheetLV();
                Assert.AreEqual(rateSheetExl, selectedRateSheet);
                extentReports.CreateStepLogs("Passed", "Rate Sheet: " + selectedRateSheet + " selected for Project: "+ engagementExl);

                timeEntry.GoToStaffTimeSheetTabLV();
                timeEntry.GoToWeeklyEntryMatrixLV();

                //Go to Summary Logs
                timeEntry.GoToSummaryLogLV();
                extentReports.CreateStepLogs("Info", "User has navigated to Summary logs ");

                //Get default rate displayed
                string DefaultRateForStaffString = timeEntry.GetDefaultRateForStaffLV();
                double DefaultRateForStaff = Convert.ToDouble(DefaultRateForStaffString);

                //Get Entered Hours displayed
                double enteredHours = timeEntry.GetEnteredHoursInSummaryLogValueLV();

                //Get total amount calculated with default rate and entered hours
                double totalAmountCalculated = DefaultRateForStaff * enteredHours;

                //Verify calculated and displayed amount should matches with Actual total amount displayed
                double totalAmountDisplayed = timeEntry.GetTotalAmountLV();
                Assert.AreEqual(totalAmountCalculated, totalAmountDisplayed);
                extentReports.CreateStepLogs("Passed", "Total Amount: " + totalAmountDisplayed + " displayed is matching with the calculation based on total hours entered and the default rate based for staff title on Summary Log");

                //Go to Details log
                timeEntry.GoToDetailLogsLV();
                extentReports.CreateStepLogs("Info", "User has naigated to details log ");

                //Verify calculated and displayed amount should matches with Actual total amount displayed
                double totalAmountDisplayedInDetailLog = timeEntry.GetTotalAmountLV();                
                Assert.AreEqual(totalAmountCalculated, totalAmountDisplayed);
                extentReports.CreateStepLogs("Passed", "Total Amount: " + totalAmountDisplayedInDetailLog + " displayed is matching with the calculation based on total hours entered and the default rate based on staff title on Detail Log");

                //Delete time entry records from detail log 
                timeEntry.RemoveRecordFromDetailLogsLV();
                extentReports.CreateStepLogs("Info", "Deleted record entry successfully from detail log ");

                //Delete rate sheet
                rateSheetMgt.DeleteRateSheetLV(engagementExl);
                extentReports.CreateStepLogs("Info", "Deleted rate sheet entry successfully after verification ");

                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "User: " + userExl + " logged out");
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

