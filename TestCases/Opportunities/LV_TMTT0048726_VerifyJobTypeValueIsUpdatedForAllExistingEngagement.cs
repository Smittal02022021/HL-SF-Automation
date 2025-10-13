using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_TMTT0048726_VerifyJobTypeValueIsUpdatedForAllExistingEngagement:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTI0055389 = "TMTI0055389_EditExistingOppEngToNewCFJobType";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0120038	Verify that the Job type value is updated for all the existing engagement

        [Test]
        public void VerifyJobTypeValueIsUpdatedForAllExistingEngagementLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0055389;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                //Login as Standard User profile and validate the user
                //Login as CAO user to approve the Opportunity
                string userCAOExl = ReadExcelData.ReadData(excelPath, "CAOUsers", 1);
                //usersLogin.SearchUserAndLogin(userCAOExl);
                homePage.SearchUserByGlobalSearchN(userCAOExl);
                extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();

                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(userCAOExl), true);
                extentReports.CreateStepLogs("Passed", "CAO User: " + userCAOExl + " logged in on Lightning View");
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Info", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                int rowEng = ReadExcelData.GetRowCount(excelPath, "NewEngJobTypes");
                for (int row = 2; row <= rowEng; row++)
                {
                    string engementName= ReadExcelData.ReadDataMultipleRows(excelPath, "NewEngJobTypes", row, 1);
                    engagementHome.SearchEngagementInLightningView(engementName);
                    engagementDetails.ClickTabHistoryLV();
                    string originalValueExl= ReadExcelData.ReadDataMultipleRows(excelPath, "NewEngJobTypes", row, 3);
                    string newValueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewEngJobTypes", row, 4);
                    string actualOriginalValue= engagementDetails.GetEngagementHistoryOriginalValueLV();
                    Assert.AreEqual(originalValueExl, actualOriginalValue, "Verify that the Job type value is updated for all the existing Engagement");
                    extentReports.CreateStepLogs("Info", "Job type Original value is "+ actualOriginalValue+" updated for all the existing Engagement");

                    string actualNewValue = engagementDetails.GetEngagementHistoryNewValueLV();
                    Assert.AreEqual(newValueExl, actualNewValue, "Verify that the Job type value is updated for all the existing Engagement");
                    extentReports.CreateStepLogs("Info", "Job type New value is " + actualNewValue + " updated for all the existing Engagement");

                    randomPages.CloseActiveTab(engementName);
                    randomPages.ReloadPage();
                }

                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " logged out");
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Passed", "Browser Closed Successfully!");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                homePageLV.LogoutFromSFLightningAsApprover();
                driver.Quit();
            }
        }
    }
}