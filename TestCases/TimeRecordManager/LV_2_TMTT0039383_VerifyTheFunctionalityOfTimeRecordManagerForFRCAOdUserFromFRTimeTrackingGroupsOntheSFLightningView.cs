using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using NUnit.Framework;
using SF_Automation.TestData;
using System;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class LV_2_TMTT0039383_VerifyTheFunctionalityOfTimeRecordManagerForFRCAOdUserFromFRTimeTrackingGroupsOntheSFLightningView: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();

        public static string fileTMTT0038660 = "LV_TMTT0039383_VerifyTheFunctionalityOfTimeRecordManagerForFRCAOdUserFromFRTimeTrackingGroupsOntheSFLightningView";
        
        private string selectProject;
        private string user;

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
                login.SwitchToClassicView();
                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowCount = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row <= rowCount; row++)
                {  
                    //Login as CAO  User profile and validate the user
                    string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    string userGrpNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);
                    homePage.SearchUserByGlobalSearchN(userExl);
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                    usersLogin.LoginAsSelectedUser();

                    login.SwitchToLightningExperience();
                    user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(userExl), true);
                    extentReports.CreateLog("User: " + userExl + " logged in on Lightning View");

                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    //TMTI0097554	Verify that the CAO who is part of the Time Tracking FR Supervisor group is able to access the "Time Tracking" module
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Module:: " + moduleNameExl + " is available for Logged-in user: " + userExl);

                    //TMTI0097558	Verify that the CAO is able to add hours to the project for any user.
                    string staffNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMembers", row, 1);

                    timeEntry.SelectStaffMemberLV(staffNameExl);
                    string staffName = timeEntry.GetSelectedStaffNameLV();
                    Assert.AreEqual(staffNameExl, staffName);
                    extentReports.CreateStepLogs("Passed", "Staff : " + staffName + " is Selected from list ");

                    selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 1);
                    string weakDayExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 2);
                    string weakEndExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", row, 3);

                    string msgSuccess = timeEntry.EnterFRUserHoursLV(selectProject, weakDayExl, weakEndExl);
                    Assert.AreEqual("-Time Record Added", msgSuccess);
                    extentReports.CreateStepLogs("Passed", "Project: " + selectProject + " selected on Weekly Entry Matrix ");
                    extentReports.CreateStepLogs("Passed", " Hours entered for FR User with Success Message: " + msgSuccess);

                    //TMTI0097560	Verify that the CAO is able to remove the entered hours of any user.
                    timeEntry.RemoveRecordFromDetailLogsLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");

                    usersLogin.ClickLogoutFromLightningView();
                    login.SwitchToClassicView();
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " logged out");
                }
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                timeEntry.RemoveRecordFromDetailLogsLV();
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
