using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class TMTI0068990_68992_68995_68998_68988_69000_VerifyUserCanSearchProjectWithNameLOBNumberOnTimeRecordManager:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();

        public static string fileTMTI0068990 = "TMTI0068990_VerifyUserCanSearchProjectWithNameOnTimeRecordManager";
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyUserCanSearchProjectOnTimeRecordManager()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0068990;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();
                string TimeRecordManagerUser = login.ValidateUser();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as FVA User
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTMTI0068990, user);
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                usersLogin.LoginAsSelectedUser();
                TimeRecordManagerUser = login.ValidateUser();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Users", 1).Contains(TimeRecordManagerUser), true);
                extentReports.CreateLog("Time Record Manager User: " + TimeRecordManagerUser + " is able to login ");
                extentReports.CreateLog("Verify that the user is able to search on the Time Record Manager ");
                homePage.ClickTimeRecordManagerTab();
                extentReports.CreateLog("Verify that the user is able to search with Project/Client, Name, LOB, Project No. on the Time Record Manager ");
                int rowSearchValue = ReadExcelData.GetRowCount(excelPath, "SearchValue");
                for (int row = 2; row <= rowSearchValue; row++)
                {
                    //Existing Engagement with Deal team member of Logged in user
                    //Verify that the user is able to search with Project Name on the time record manager
                    //TMTI0069000-Verify that selecting the project name and removing it takes the user back to the search bar to search project.
                    
                    string selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "SearchValue", row,1);

                    //Weekly Entry Matrix
                    timeEntry.ClickWeeklyEntryMatrixTab();
                    Assert.IsTrue(timeEntry.IsProjectSelected(selectProject), "Verify that the user is able to search with "+ selectProject + " on the time record manager ");
                    extentReports.CreateLog("User is able to search and select the Project with value:" + selectProject + " on Weekly Entry Matrix ");
                    
                    //Verify after selecting the project name changed the search bar to old select project drop-down. 
                    Assert.IsTrue(timeEntry.IsComboSelectProjectDisplayed(), "Verify after selecting the project, the search bar to changed to old Select Project Drop-Down");
                    extentReports.CreateLog("After selecting the project, the search bar to changed to old Select Project Drop-Down on Weekly Entry Matrix ");

                    //Time Clock Recorder
                    timeEntry.ClickTimeClockRecorderTab();
                    Assert.IsTrue(timeEntry.IsProjectSelected(selectProject), "Verify that the user is able to search with " + selectProject + " on the time record manager ");
                    extentReports.CreateLog("User is able to search and select the Project with value:" + selectProject + " on Time Clock Recorder ");
                    
                    //Verify after selecting the project name changed the search bar to old select project drop-down. 
                    Assert.IsTrue(timeEntry.IsComboSelectProjectDisplayed(), "Verify after selecting the project, the search bar to changed to old Select Project Drop-Down");
                    extentReports.CreateLog("After selecting the project, the search bar to changed to old Select Project Drop-Down on Time Clock Recorder ");
                    
                    //Summary Logs
                    timeEntry.ClickSummaryLogsTab();
                    Assert.IsTrue(timeEntry.IsProjectSelected(selectProject), "Verify that the user is able to search with " + selectProject + " on the time record manager ");
                    extentReports.CreateLog("User is able to search and select the Project with value:" + selectProject + " on Summary Logs ");
                    
                    //Verify after selecting the project name changed the search bar to old select project drop-down. 
                    Assert.IsTrue(timeEntry.IsComboSelectProjectDisplayed(), "Verify after selecting the project, the search bar to changed to old Select Project Drop-Down");
                    extentReports.CreateLog("After selecting the project, the search bar to changed to old Select Project Drop-Down  on Summary Logs ");

                    //Detail Logs 
                    timeEntry.ClickWeeklyEntryMatrixTab();
                    timeEntry.ClickDetailLogsTab();
                    Assert.IsTrue(timeEntry.IsProjectSelected(selectProject), "Verify that the user is able to search with " + selectProject + " on the time record manager ");
                    extentReports.CreateLog("User is able to search and select the Project with value:" + selectProject + " on Detail Logs ");
                    
                    //Verify after selecting the project name changed the search bar to old select project drop-down. 
                    Assert.IsTrue(timeEntry.IsComboSelectProjectDisplayed(), "Verify after selecting the project, the search bar to changed to old Select Project Drop-Down");
                    extentReports.CreateLog("After selecting the project, the search bar to changed to old Select Project Drop-Down on Detail Logs ");

                    //Weekly Overview
                    timeEntry.ClickWeeklyEntryMatrixTab();
                    timeEntry.ClickWeeklyOverviewTab();
                    Assert.IsTrue(timeEntry.IsProjectSelected(selectProject), "Verify that the user is able to search with " + selectProject + " on the time record manager ");
                    extentReports.CreateLog("User is able to search and select the Project with value:" + selectProject + " on Weekly Overview ");
                    
                    //Verify after selecting the project name changed the search bar to old select project drop-down. 
                    Assert.IsTrue(timeEntry.IsComboSelectProjectDisplayed(), "Verify after selecting the project, the search bar to changed to old Select Project Drop-Down");
                    extentReports.CreateLog("After selecting the project, the search bar to changed to old Select Project Drop-Down on Weekly Overview ");
                }


                usersLogin.UserLogOut();
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
            }
        }
        
    }
}
