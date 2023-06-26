using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using NUnit.Framework;
using SF_Automation.TestData;
using System;
using OpenQA.Selenium;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class TMTI0069005_69007_69008_VerifyClientNameIsAddedToTheProjectNamesForOpportunityEngagements: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();

        public static string fileTMTI0069005 = "TMTI0069005_VerifyClientNameIsAddedToTheProjectNamesForOpportunityEngagements";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyClientNameIsAddedToTheProjectNamesForOpportunityEngagements()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0069005;

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
                homePage.SearchUserByGlobalSearch(fileTMTI0069005, user);
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
                    string valueProjectExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SearchValue", row, 1);
                    extentReports.CreateLog("Project Searching with Number: "+ valueProjectExl+" ");
                    //Weekly Entry Matrix
                    timeEntry.ClickWeeklyEntryMatrixTab();
                    string projectFullname = timeEntry.SearchProjectandGetFullName(valueProjectExl);
                    string clientNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SearchValue", row, 2);
                    Assert.IsTrue(projectFullname.Contains(clientNameExl), "Verify that the Client Name is added to the project full name on Time Record Manger");
                    extentReports.CreateLog("Client Name: "+ clientNameExl + " is added to the Project full name: "+ projectFullname + " on Time Record Manger on Weekly Entry Matrix ");
                    
                    //TMTI0069008	Verify that the activity type is removed for the CVAS team(TFR time tracking).

                    Assert.IsFalse(timeEntry.IsActivityListDisplayed(projectFullname, fileTMTI0069005), "Verify Activity List is not displayed for logged in user");
                    extentReports.CreateLog("Activity List is not displayed for logged in user on Weekly Entry Matrix ");

                    //Time Clock Recorder
                    timeEntry.ClickTimeClockRecorderTab();
                    projectFullname = timeEntry.SearchProjectandGetFullName(valueProjectExl);
                    Assert.IsTrue(projectFullname.Contains(clientNameExl), "Verify that the Client Name is added to the project full name on Time Record Manger");
                    extentReports.CreateLog("Client Name: " + clientNameExl + " is added to the Project full name: " + projectFullname + " on Time Record Manger on Time Clock Recorder ");
                    
                    //TMTI0069008	Verify that the activity type is removed for the CVAS team(TFR time tracking).
                    Assert.IsFalse(timeEntry.IsActivityListDisplayed(projectFullname, fileTMTI0069005), "Verify Activity List is not displayed for logged in user");
                    extentReports.CreateLog("Activity List is not displayed for logged in user on Time Clock Recorder ");

                    //Summary Logs
                    timeEntry.ClickSummaryLogsTab();
                    projectFullname = timeEntry.SearchProjectandGetFullName(valueProjectExl);
                    Assert.IsTrue(projectFullname.Contains(clientNameExl), "Verify that the Client Name is added to the project full name on Time Record Manger");
                    extentReports.CreateLog("Client Name: " + clientNameExl + " is added to the Project full name: " + projectFullname + " on Time Record Manger on Summary Logs ");

                    //TMTI0069008	Verify that the activity type is removed for the CVAS team(TFR time tracking).
                    Assert.IsFalse(timeEntry.IsActivityListDisplayed(projectFullname, fileTMTI0069005), "Verify Activity List is not displayed for logged in user");
                    extentReports.CreateLog("Activity List is not displayed for logged in user on Summary Logs ");
                    
                    //Detail Logs
                    timeEntry.ClickWeeklyEntryMatrixTab();
                    timeEntry.ClickDetailLogsTab();
                    projectFullname = timeEntry.SearchProjectandGetFullName(valueProjectExl);
                    Assert.IsTrue(projectFullname.Contains(clientNameExl), "Verify that the Client Name is added to the project full name on Time Record Manger");
                    extentReports.CreateLog("Client Name: " + clientNameExl + " is added to the Project full name: " + projectFullname + " on Time Record Manger on Detail Logs ");

                    //TMTI0069008	Verify that the activity type is removed for the CVAS team(TFR time tracking).
                    Assert.IsFalse(timeEntry.IsActivityListDisplayed(projectFullname, fileTMTI0069005), "Verify Activity List is not displayed for logged in user");
                    extentReports.CreateLog("Activity List is not displayed for logged in user on Detail Logs ");
                    
                    //Weekly Overview
                    timeEntry.ClickWeeklyEntryMatrixTab();
                    timeEntry.ClickWeeklyOverviewTab();
                    projectFullname = timeEntry.SearchProjectandGetFullName(valueProjectExl);
                    Assert.IsTrue(projectFullname.Contains(clientNameExl), "Verify that the Client Name is added to the project full name on Time Record Manger");
                    extentReports.CreateLog("Client Name: " + clientNameExl + " is added to the Project full name: " + projectFullname + " on Time Record Manger on Weekly Overview ");

                    //TMTI0069008	Verify that the activity type is removed for the CVAS team(TFR time tracking).
                    Assert.IsFalse(timeEntry.IsActivityListDisplayed(projectFullname, fileTMTI0069005), "Verify Activity List is not displayed for logged in user");
                    extentReports.CreateLog("Activity List is not displayed for logged in user on Weekly Overview ");
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
