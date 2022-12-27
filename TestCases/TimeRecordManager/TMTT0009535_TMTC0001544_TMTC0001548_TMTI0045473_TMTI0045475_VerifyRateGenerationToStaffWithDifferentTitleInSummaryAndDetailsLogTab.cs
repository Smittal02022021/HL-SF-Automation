using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class TMTT0009535_TMTC0001544_TMTC0001548_TMTI0045473_TMTI0045475_VerifyRateGenerationToStaffWithDifferentTitleInSummaryAndDetailsLogTab : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();

        private string valueActivity;
        private string valueActivityExl;
        private string valueEnteredHours;
        private string valueEnteredHoursExl;
        private string valueDefaultDollarBasedOnTitle;
        private string valueActivityInDetailLogs;
        private string valueActivityInExlDetailLogs;
        private string valueEnteredHoursInDetailLogs;
        private string valueEnteredHoursInExl;
        private string valueDefaultDollarDetailLogs;
        private string valueDefaultDollarDetailLogsExl;
        private string valueProjectOrEngagmentInDetailLogs;
        private string valueProjectOrEngagment;
        private double defaultRateForStaffDetailLogs = 0.00;
        private double enteredHoursDetailLogs = 0.00;
        private double enteredHoursSummaryLogs = 0.00;
        private double totalAmountCalculated = 0.00;
        private double totalAmountDisplayedSummaryLogs = 0.00;
        private double totalAmountDisplayedInDetailLogs = 0.00;

        public static string fileTC2286_TC2287 = "TMTT0009535_TMTC0001544_TMTC0001548_TMTI0045473_TMTI0045475_VerifyRateGenerationToStaffWithDifferentTitleInSummaryAndDetailsLogTab" +
            "";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyRateGenerationToStaffMemberAreDeterminedThroughTheirTitles()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2286_TC2287;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Handling salesforce Lightning
                //login.HandleSalesforceLightningPage();
                string TimeRecordManagerUser = login.ValidateUser();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as supervisor
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC2286_TC2287, user);
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                usersLogin.LoginAsSelectedUser();
                TimeRecordManagerUser = login.ValidateUser();
                //string TimeRecordManagerUser = login.ValidateUser();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Users", 1).Contains(TimeRecordManagerUser), true);
                extentReports.CreateLog("Time Record Manager User: " + TimeRecordManagerUser + " is able to login ");

                int rowRateSheet = ReadExcelData.GetRowCount(excelPath, "RateSheetManagement");
                for (int row = 2; row <= rowRateSheet; row++)
                {
                    //Click Time Record Manager Tab
                    homePage.ClickTimeRecordManagerTab();
                    string timeRecordManagerTitle = timeEntry.GetTimeRecordManagerTitle();
                    string timeRecordManagerTitleExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 5);
                    Assert.AreEqual(timeRecordManagerTitleExl, timeRecordManagerTitle);
                    extentReports.CreateLog("Time Record Manager Title: " + timeRecordManagerTitle + " is displayed upon click of Time Record Manager tab ");

                    //Select Staff Member from the list
                    string name = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 1);

                    // string pTagValue = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 3);
                    timeEntry.SelectStaffMember(name);
                    string selectedStaffMember = timeEntry.GetSelectedStaffMember();
                    string selectedStaffMemberExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", row, 1);
                    Assert.AreEqual(selectedStaffMemberExl, selectedStaffMember);
                    extentReports.CreateLog("Staff Member Title: " + selectedStaffMember + " is displayed upon click of staff Member link ");

                    //Enter Rate Sheet details
                    string engagement = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 1);
                    string rateSheet = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 2);
                    rateSheetMgt.EnterRateSheet(engagement, rateSheet);

                    //Verify selected rate sheet
                    string selectedRateSheet = rateSheetMgt.GetSelectedRateSheet();
                    string selectedRateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 2);
                    Assert.AreEqual(selectedRateSheetExl, selectedRateSheet);
                    extentReports.CreateLog("Selected Rate Sheet: " + selectedRateSheet + " is displayed upon entering rate sheet details ");

                    //Enter on Summary Logs
                    timeEntry.EnterSummaryLogs(ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 1), fileTC2286_TC2287);
                    timeEntry.ClickAddButton();

                    //Verify project or engagement on Summary Logs
                    valueProjectOrEngagment = timeEntry.GetProjectOrEngagementValue();
                    string valueProjectOrEngagmentExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 1);
                    if (valueProjectOrEngagmentExl.Contains(valueProjectOrEngagment))
                    {
                        extentReports.CreateLog("Project Or Engagement: " + valueProjectOrEngagment + " is displayed upon entering time entry in summary log ");
                    }

                    //Verify selected activity on Summary Logs
                    valueActivity = timeEntry.GetSelectedActivity();
                    valueActivityExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 3);
                    Assert.AreEqual(valueActivityExl, valueActivity);
                    extentReports.CreateLog("Selected Activity: " + valueActivity + " is displayed upon entering time entry in summary log ");

                    //Verify entered hours 
                    valueEnteredHours = timeEntry.GetEnteredHoursInSummaryLog();
                    valueEnteredHoursExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 2);
                    Assert.AreEqual(valueEnteredHoursExl, valueEnteredHours);
                    extentReports.CreateLog("Entered Hours: " + valueEnteredHours + " is displayed upon entering time entry in summary log ");

                    //Verify default dollar based on the title of the staff member on Summary Logs
                    valueDefaultDollarBasedOnTitle = timeEntry.GetDefaultRateForStaff();

                    extentReports.CreateLog("Default Dollar Based On title amount: " + valueDefaultDollarBasedOnTitle + " is displayed upon entering time entry in summary log with Rate Sheet Added");

                    //Get default rate displayed on Summary Logs
                    double DefaultRateForStaffWithRateSheet = timeEntry.GetDefaultRateForStaffValue();

                    //Get Entered Hours displayed
                    enteredHoursSummaryLogs = timeEntry.GetEnteredHoursInSummaryLogValue();

                    // Get total amount calculated with default rate and entered hours on Summary Logs
                    //double 
                    totalAmountCalculated = DefaultRateForStaffWithRateSheet * enteredHoursSummaryLogs;

                    // Get total amount displayed on Summary Logs
                    double totalAmountDisplayedSummaryLogs = timeEntry.GetTotalAmount();

                    //Verify calculated and displayed amount should matches on Summary Logs
                    Assert.AreEqual(totalAmountCalculated, totalAmountDisplayedSummaryLogs);
                    extentReports.CreateLog("Total Amount: " + totalAmountDisplayedSummaryLogs + " displayed is matching with the calculation based on total hours entered and the default rate based on staff title ");

                    //Delete Time Entry
                    timeEntry.DeleteTimeEntry();
                    extentReports.CreateLog("Deleted time entry successfully after verification ");

                    // Enter Detail logs
                    timeEntry.EnterDetailLogs(ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 1), fileTC2286_TC2287);
                    timeEntry.ClickAddButton();

                    //Verify project or engagement on Detail Logs 
                    valueProjectOrEngagmentInDetailLogs = timeEntry.GetProjectOrEngagementValue();
                    if (valueProjectOrEngagmentExl.Contains(valueProjectOrEngagmentInDetailLogs))
                    {
                        extentReports.CreateLog("Project Or Engagement: " + valueProjectOrEngagment + " is displayed upon entering time entry in detail log ");
                    }

                    //Verify selected activity on Detail Logs 
                    valueActivityInDetailLogs = timeEntry.GetSelectedActivityOnDetailLog();
                    valueActivityInExlDetailLogs = ReadExcelData.ReadData(excelPath, "DetailLogs", 3);
                    Assert.AreEqual(valueActivityInExlDetailLogs, valueActivityInDetailLogs);
                    extentReports.CreateLog("Selected Activity: " + valueActivity + " is displayed upon entering time entry on Detail Logs ");

                    //Verify entered hours on Detail Logs 
                    valueEnteredHoursInDetailLogs = timeEntry.GetEnteredHoursInDetailLogs();
                    valueEnteredHoursInExl = ReadExcelData.ReadData(excelPath, "DetailLogs", 2);
                    Assert.AreEqual(valueEnteredHoursInExl, valueEnteredHoursInDetailLogs);
                    extentReports.CreateLog("Entered Hours: " + valueEnteredHoursInDetailLogs + " is displayed upon entering time entry in detail log ");

                    //Verify default dollar based on the title of the staff member on Detail Logs 
                    valueDefaultDollarDetailLogs = timeEntry.GetDefaultRateForStaffInDetailLogs();
                    valueDefaultDollarDetailLogsExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 4);
                    Assert.AreEqual(valueDefaultDollarDetailLogsExl, valueDefaultDollarDetailLogs);
                    extentReports.CreateLog("Default Dollar Based On title amount: " + valueDefaultDollarDetailLogs + " is displayed upon entering time entry on Detail Logs ");

                    //Get default rate displayed on Detail Logs 
                    defaultRateForStaffDetailLogs = timeEntry.GetDefaultRateForStaffValueInDetailLogs();

                    //Get Entered Hours displayed on Detail Logs 
                    enteredHoursDetailLogs = timeEntry.GetEnteredHoursInDetailLogValue();

                    // Get total amount calculated with default rate and entered hours on Detail Logs 
                    totalAmountCalculated = defaultRateForStaffDetailLogs * enteredHoursDetailLogs;

                    // Get total amount displayed
                    totalAmountDisplayedInDetailLogs = timeEntry.GetTotalAmount();

                    //Verify calculated and displayed amount should matches on Detail Logs 
                    Assert.AreEqual(totalAmountCalculated, totalAmountDisplayedInDetailLogs);
                    extentReports.CreateLog("Total Amount: " + totalAmountDisplayedInDetailLogs + " displayed is matching with the calculation based on total hours entered and the default rate based on staff title ");

                    // Delete time entry records from detail log 
                    timeEntry.RemoveRecordFromDetailLogs();
                    extentReports.CreateLog("Deleted record entry successfully from detail log ");

                    //Delete rate sheet
                    rateSheetMgt.DeleteRateSheet(ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 1));
                    extentReports.CreateLog("Deleted rate sheet entry successfully after verification ");

                    //TMTI0045473: Verify that if Rate sheet is removed for any project, time record will not calculate rate on entering hours after wards.
                    extentReports.CreateLog("Verify the Calculation amount and default rate after deleting the Rate Sheet ");
                    //Enter summary log details
                    timeEntry.EnterSummaryLogs(ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 1), fileTC2286_TC2287);
                    timeEntry.ClickAddButton();

                    //Verify project or engagement
                    valueProjectOrEngagment = timeEntry.GetProjectOrEngagementValue();
                    //string valueProjectOrEngagmentExl1= ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 1);
                    if (valueProjectOrEngagmentExl.Contains(valueProjectOrEngagment))
                    {
                        extentReports.CreateLog("Project Or Engagement: " + valueProjectOrEngagment + " is displayed upon entering time entry on Summary logs ");
                    }

                    //Verify selected activity on Summary Logs
                    valueActivity = timeEntry.GetSelectedActivity();
                    Assert.AreEqual(valueActivityExl, valueActivity);
                    extentReports.CreateLog("Selected Activity: " + valueActivity + " is displayed upon entering time entry on Summary logs ");

                    //Verify entered hours on Summary Logs
                    valueEnteredHours = timeEntry.GetEnteredHoursInSummaryLog();
                    Assert.AreEqual(valueEnteredHoursExl, valueEnteredHours);
                    extentReports.CreateLog("Entered Hours: " + valueEnteredHours + " is displayed upon entering time entry on Summary logs ");

                    //Verify default dollar based on the title of the staff member on Summary Logs
                    valueDefaultDollarBasedOnTitle = timeEntry.GetDefaultRateForStaff();

                    extentReports.CreateLog("Default Dollar Based On title amount: " + valueDefaultDollarBasedOnTitle + " is displayed upon entering time entry in Summary Log without Rate Sheet Added");

                    //Get default rate displayed on Summary Logs
                    double DefaultRateForStaffWithoutRateSheet = timeEntry.GetDefaultRateForStaffValue();

                    //Get Entered Hours displayed on Summary Logs
                    enteredHoursSummaryLogs = timeEntry.GetEnteredHoursInSummaryLogValue();

                    // Get total amount calculated with default rate and entered hours on Summary Logs
                    totalAmountCalculated = DefaultRateForStaffWithoutRateSheet * enteredHoursSummaryLogs;
                    // Get total amount displayed on Summary Logs
                    totalAmountDisplayedSummaryLogs = timeEntry.GetTotalAmount();

                    //Verify calculated and displayed amount should matches on Summary Logs
                    Assert.AreEqual(totalAmountCalculated, totalAmountDisplayedSummaryLogs);
                    extentReports.CreateLog("Total Amount: " + totalAmountDisplayedSummaryLogs + " displayed is matching with the calculation based on total hours entered and the default rate based on staff title ");

                    //Delete Time Entry on Summary Logs
                    timeEntry.DeleteTimeEntry();
                    extentReports.CreateLog("Deleted time entry successfully after verification on Summary Logs");

                    //--------------------Detailed Logs----------------------//
                    // Enter detail logs
                    timeEntry.EnterDetailLogs(ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 1), fileTC2286_TC2287);
                    timeEntry.ClickAddButton();

                    //Verify project or engagement on Detail Logs
                    valueProjectOrEngagmentInDetailLogs = timeEntry.GetProjectOrEngagementValue();
                    if (valueProjectOrEngagmentExl.Contains(valueProjectOrEngagmentInDetailLogs))
                    {
                        extentReports.CreateLog("Project Or Engagement: " + valueProjectOrEngagment + " is displayed upon entering time entry in Detail Log ");
                    }

                    //Verify selected activity on Detail Logs
                    valueActivityInDetailLogs = timeEntry.GetSelectedActivityOnDetailLog();
                    Assert.AreEqual(valueActivityInExlDetailLogs, valueActivityInDetailLogs);
                    extentReports.CreateLog("Selected Activity: " + valueActivity + " is displayed upon entering time entry in detail log ");

                    //Verify entered hours  on Detail Logs
                    valueEnteredHoursInDetailLogs = timeEntry.GetEnteredHoursInDetailLogs();
                    // string valueEnteredHoursInExl1 = ReadExcelData.ReadData(excelPath, "DetailLogs", 2);
                    Assert.AreEqual(valueEnteredHoursInExl, valueEnteredHoursInDetailLogs);
                    extentReports.CreateLog("Entered Hours: " + valueEnteredHoursInDetailLogs + " is displayed upon entering time entry in Detail Log ");

                    //Verify default dollar based on the title of the staff member on Detail Logs
                    valueDefaultDollarDetailLogs = timeEntry.GetDefaultRateForStaffInDetailLogs();
                    valueDefaultDollarDetailLogsExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 5);
                    Assert.AreEqual(valueDefaultDollarDetailLogsExl, valueDefaultDollarDetailLogs);
                    extentReports.CreateLog("Default Dollar Based On title amount: " + valueDefaultDollarDetailLogs + " is displayed upon entering time entry in Detail Log ");

                    //Get default rate displayed on Detail Logs
                    defaultRateForStaffDetailLogs = timeEntry.GetDefaultRateForStaffValueInDetailLogs();

                    //Get Entered Hours displayed on Detail Logs
                    enteredHoursDetailLogs = timeEntry.GetEnteredHoursInDetailLogValue();

                    // Get total amount calculated with default rate and entered hours on Detail Logs
                    totalAmountCalculated = defaultRateForStaffDetailLogs * enteredHoursDetailLogs;

                    // Get total amount displayed on Detail Logs
                    totalAmountDisplayedInDetailLogs = timeEntry.GetTotalAmount();

                    //Verify calculated and displayed amount should matches
                    Assert.AreEqual(totalAmountCalculated, totalAmountDisplayedInDetailLogs);
                    extentReports.CreateLog("Total Amount: " + totalAmountDisplayedInDetailLogs + " displayed is matching with the calculation based on total hours entered and the default rate based on staff title ");

                    // Delete time entry records from detail log 
                    timeEntry.RemoveRecordFromDetailLogs();
                    extentReports.CreateLog("Deleted record entry successfully from detail log ");
                }
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}