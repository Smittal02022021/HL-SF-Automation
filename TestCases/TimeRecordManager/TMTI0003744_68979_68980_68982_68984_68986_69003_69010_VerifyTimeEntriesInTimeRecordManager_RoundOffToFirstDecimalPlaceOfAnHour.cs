using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Company;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;


namespace SF_Automation.TestCases.TimeRecordManager
{
    class TMTI0003744_68979_68980_68982_68984_68986_69003_69010_VerifyTimeEntriesInTimeRecordManager_RoundOffToFirstDecimalPlaceOfAnHour : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();

        public static string fileTMTI0003744 = "TMTI0003744_TimeRecordManager_VerifyTimeEntriesInTimeRecordManager_RoundOffToFirstDecimalPlaceOfAnHour";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyTimeEntriesInTimeRecordManager_RoundOffToFirstDecimalPlaceOfAnHour()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0003744;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTMTI0003744, user);
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                usersLogin.LoginAsSelectedUser();
                string TimeRecordManagerUser = login.ValidateUser();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Users", 1).Contains(TimeRecordManagerUser), true);
                extentReports.CreateLog("Time Record Manager User: " + TimeRecordManagerUser + " is able to login ");

                //Click time record manager
                homePage.ClickTimeRecordManagerTab();
                string timeRecordManagerTitle = timeEntry.GetTimeRecordManagerTitle();
                string timeRecordManagerTitleExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 5);
                Assert.AreEqual(timeRecordManagerTitleExl, timeRecordManagerTitle);
                extentReports.CreateLog("Time Record Manager Title: " + timeRecordManagerTitle + " is displayed upon click of Time Record Manager tab ");

                //TMTI0068979_68980_68982_68984_68986_Entering hours with updated Project name(Including Project Name,ClientName,LOB,Eng Number)
                //TMTI0069010	Verify that activity type is not removed for the users who are not part of the CVAS team(TFR time tracking) but are part of other groups.
                // User is entering hours with Activity selection in all below section 
                //Enter weekly entry matrix
                string selectProject = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 1);
                timeEntry.EnterWeeklyEntryMatrix(selectProject, fileTMTI0003744);
                string sundayTimeRoundOff = timeEntry.GetSundayTimeEntry();
                string sundayTimeRoundOffExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 4);
                Assert.AreEqual(sundayTimeRoundOffExl, sundayTimeRoundOff);
                extentReports.CreateLog("Time Entry rounded off value: " + sundayTimeRoundOff + " is updated from more than one decimal value to one decimal value ");

                //Clear time record from weekly entry matrix
                timeEntry.ClearTimeRecord();
                //string defaultSelectedProjectOption = timeEntry.GetDefaultSelectedProjectOption();
                string defaultSelectedProjectOption = timeEntry.GetDefaultSelectedProjectOptionN();
                string defaultSelectedProjectOptionExl = ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 6);
                Assert.AreEqual(defaultSelectedProjectOptionExl, defaultSelectedProjectOption);
                extentReports.CreateLog("Time Entry Project Default: " + defaultSelectedProjectOption + " is displayed upon deletion of time entry ");

                // Enter summary logs entry
                timeEntry.EnterSummaryLogs(ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", 2, 1), fileTMTI0003744);
                string summaryLogTimeEntryRoundOff = timeEntry.GetSummaryLogsTimeEntry();
                string summaryLogTimeEntryRoundOffExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 4);
                Assert.AreEqual(summaryLogTimeEntryRoundOffExl, summaryLogTimeEntryRoundOff);
                extentReports.CreateLog("Summary Log Rounded Off Value: " + summaryLogTimeEntryRoundOff + " is updated from more than one decimal value to one decimal value ");

                //Enter detail logs entry
                timeEntry.EnterDetailLogs(ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", 2, 1), fileTMTI0003744);
                string detailLogTimeEntryRoundOff = timeEntry.GetDetailLogsTimeEntry();
                string detailLogTimeEntryRoundOffExl = ReadExcelData.ReadData(excelPath, "DetailLogs", 4);
                Assert.AreEqual(detailLogTimeEntryRoundOff, detailLogTimeEntryRoundOffExl);
                extentReports.CreateLog("Detail Log Rounded Off Value: " + detailLogTimeEntryRoundOff + " is updated from more than one decimal value to one decimal value ");

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