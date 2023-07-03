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
    class TMTI0045472_VerifyBetaUserWithDifferentTitleIsAbletoEnterHoursfromTimeRecordmanager : BaseClass
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
        private string TimeRecordManagerUser;
        private double defaultRateForStaffDetailLogs = 0.00;
        private double enteredHoursDetailLogs = 0.00;
        private double enteredHoursSummaryLogs = 0.00;
        private double totalAmountCalculated = 0.00;
        private double totalAmountDisplayedSummaryLogs = 0.00;
        private double totalAmountDisplayedInDetailLogs = 0.00;

        public static string fileTC2286_TC2287 = "TMTI0045472_VerifyBetaUserWithDifferentTitleIsAbletoEnterHoursfromTimeRecordmanager" +
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
                TimeRecordManagerUser = login.ValidateUser();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
            
                int rowUser = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row <= rowUser; row++)
                {

                    string userName = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    string userTitle= ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);
                    homePage.SearchUserByGlobalSearch(fileTC2286_TC2287, userName);
                    string userPeople = homePage.GetPeopleOrUserName();
                    extentReports.CreateLog("User " + userPeople + " details are displayed ");

                    usersLogin.LoginAsSelectedUser();
                    TimeRecordManagerUser = login.ValidateUser();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1).Contains(TimeRecordManagerUser), true);
                    extentReports.CreateLog("Time Record Beta User: " + TimeRecordManagerUser + " with title "+ userTitle + "is able to login ");

                    //Click Time Record Manager Tab
                    homePage.ClickTimeRecordManagerTab();
                    string timeRecordManagerTitle = timeEntry.GetTimeRecordManagerTitle();
                    string timeRecordManagerTitleExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 4);
                    Assert.AreEqual(timeRecordManagerTitleExl, timeRecordManagerTitle);
                    extentReports.CreateLog("Time Record Manager Title: " + timeRecordManagerTitle + " is displayed upon click of Time Record Manager tab ");

                    //Enter on Summary Logs
                    timeEntry.BetaUserEnterSummaryLogs(ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 1), fileTC2286_TC2287);
                    timeEntry.ClickAddButton();

                    //Verify project or engagement on Summary Logs
                    string valueProjectOrEngagment = timeEntry.GetProjectOrEngagementValue();
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

                    //Delete Time Entry
                    timeEntry.DeleteTimeEntry();
                    extentReports.CreateLog("Deleted time entry successfully after verification ");

                    // Enter Detail logs
                    timeEntry.BetaUserEnterDetailLogs(ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", row, 1), fileTC2286_TC2287);
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
                    timeEntry.RemoveRecordFromDetailLogs();
                    extentReports.CreateLog("Deleted record entry successfully from detail log ");
                    usersLogin.UserLogOut();
                }
                
                usersLogin.UserLogOut();
                //usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                //usersLogin.UserLogOut();
                driver.Quit();
            }
        }






    }
}
