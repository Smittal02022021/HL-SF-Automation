using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_TMTT0012115_ValidateLegacyPeriodAccuredFeesFieldOnRevenueAccruals : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        MonthlyRevenue monthlyRevenuePage = new MonthlyRevenue();

        public static string fileTMTT0012115 = "LV_TMTT0012115_ValidateLegacyPeriodAccuredFeesFieldOnRevenueAccruals";
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ValidateLegacyPeriodAccuredFeesFieldOnRevenueAccrualsLV()
        {
            try
            { //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0012115;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(userExl);
                extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(userExl), true);
                extentReports.CreateStepLogs("Passed", "User: " + userExl + " logged in on Lightning View");
                
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                randomPages.SelectListViewLV("All Monthly Revenue Process Controls");

                //Navigate to Current Month Revenue Accrual Page
                monthlyRevenuePage.SelectCurrentMonthRevenuePageLV();
                int rowCount = ReadExcelData.GetRowCount(excelPath, "LOB");
                for (int row = 2; row <= rowCount; row++)
                {
                    string lobExl = ReadExcelData.ReadDataMultipleRows(excelPath, "LOB", row, 1);
                    string numRevAccu = monthlyRevenuePage.SelectRevenueAccrualLV(lobExl);
                    extentReports.CreateStepLogs("Info", "Current Revenue Accrual:: "+ numRevAccu+" page for LOB: " + lobExl+" is displayed. ");
                    Assert.IsTrue(monthlyRevenuePage.IsLegacyPeriodAccruedFeesExistLV(), "Verify Legacy Period Accrued Fees field is Displayed");
                    extentReports.CreateStepLogs("Passed", "Legacy Period Accrued Fees field is Displayed for LOB: " + lobExl);
                    randomPages.CloseActiveTab(numRevAccu);
                    randomPages.CloseActiveTab("Revenue Accruals");
                    //Input into Field is pending
                }

                homePageLV.UserLogoutFromSFLightningView();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                homePageLV.LogoutFromSFLightningAsApprover();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}

