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
    class LV_TMTI0045469_TMTI0045470_VerifyNewTitleAddedinTitleRateSheetLightningView: BaseClass
    {//TMTT0020334
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTI0045469 = "LV_TMTI0045469_VerifyNewTitleAddedinTitleRateSheet";        

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyNewTitleAddedinTitleRateSheetLightningViewLV()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0045469;
                extentReports.CreateStepLogs("Info", "Creating New Opportunity and Converting to Engagement LOB:FVA On Lightning View");
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);

                ////////////////Login as Supervisor  User////////////////
                string userSupervisorExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                string userGrpNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);

                //Search CF Financial user by global search
                homePage.SearchUserByGlobalSearchN(userSupervisorExl);
                extentReports.CreateStepLogs("Info", "User: " + userSupervisorExl + " details are displayed. ");

                //Login user
                usersLogin.LoginAsSelectedUser();

                login.SwitchToLightningExperience();
                string userSupervisor = login.ValidateUserLightningView();
                Assert.AreEqual(userSupervisor.Contains(userSupervisorExl), true);
                extentReports.CreateStepLogs("Passed", "Supervosor User: " + userSupervisorExl + " from Time Tracking Group: " + userGrpNameExl + "  logged in ");
                //homePageLV.ClickAppLauncher();

                //Go to Opportunity module in Lightning View                 
                homePageLV.SelectAppLV(appNameExl);
                Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                //Navigate to Title Rate Sheets page
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                randomPages.SelectListViewLV("All");
                extentReports.CreateStepLogs("Info", " All List option is selected ");

                int rowCount = ReadExcelData.GetRowCount(excelPath, "TitleRateSheet");
                for (int row = 2; row <= rowCount; row++)
                {
                    //Click on the new title rate sheet name
                    string nameRateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TitleRateSheet", row, 1);
                    string pageTitle = rateSheetMgt.SelectTitleRateSheetLV(nameRateSheetExl);
                    Assert.AreEqual(pageTitle, nameRateSheetExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + pageTitle + " Page");

                    string userTitleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TitleRateSheet", row, 2);
                    string titleDefaultRateExl= ReadExcelData.ReadDataMultipleRows(excelPath, "TitleRateSheet", row, 3);

                    double defaultRate = rateSheetMgt.GetDefaultRateAsPerRoleLV(userTitleExl);
                    Assert.AreEqual(Convert.ToDouble(titleDefaultRateExl), defaultRate);
                    extentReports.CreateStepLogs("Passed", "Title: " + userTitleExl + " Default Rate: USD " + defaultRate);
                    
                    randomPages.CloseActiveTab(nameRateSheetExl);
                    extentReports.CreateStepLogs("Info", "Rate Sheet: " + nameRateSheetExl + " page is closed ");                    
                }
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "User: " + userSupervisorExl + " logged out");
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);                
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}