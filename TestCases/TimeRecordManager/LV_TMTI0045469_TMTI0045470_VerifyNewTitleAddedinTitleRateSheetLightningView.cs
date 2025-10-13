using AventStack.ExtentReports.Gherkin.Model;
using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.IO;
using System.Web.UI.DataVisualization.Charting;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class LV_TMTI0045469_TMTI0045470_VerifyNewTitleAddedinTitleRateSheetLightningView : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();

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
                //Get path of Test data file
                string excelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestData", fileTMTI0045469 + ".xlsx");
                excelPath = Path.GetFullPath(excelPath);

                extentReports.CreateStepLogs("Info", "Creating New Opportunity and Converting to Engagement LOB:FVA On Lightning View");

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                //Validate user logged in
                Assert.AreEqual(driver.Url.Contains("lightning"), true);
                extentReports.CreateStepLogs("Passed", "Admin User is able to login into SF");

                //Select HL Banker app
                try
                {
                    homePageLV.SelectAppLV("HL Banker");
                }
                catch(Exception)
                {
                    homePageLV.SelectAppLV1("HL Banker");
                }

                ////////////////Login as Supervisor  User////////////////
                string userSupervisorExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                string userGrpNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);

                //Search Supervisor user by global search
                lvHomePage.SearchUserFromMainSearch(userSupervisorExl);

                //Verify searched user
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, userSupervisorExl + " | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User " + userSupervisorExl + " details are displayed ");

                //Login as Supervisor user
                lvHomePage.UserLogin();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(userSupervisorExl));
                extentReports.CreateStepLogs("Passed", "Supervisor User: " + userSupervisorExl + " from Time Tracking Group: " + userGrpNameExl + "  logged in ");
                
                //Go to Opportunity module in Lightning View                 
                homePageLV.SelectAppLV(appNameExl);
                Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");

                //Navigate to Title Rate Sheets page
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);

                lvHomePage.NavigateToAnItemFromHLBankerDropdown(moduleNameExl);

                //homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                randomPages.SelectListViewLV("All");
                extentReports.CreateStepLogs("Info", " All List option is selected ");
                 
                int rowCount = ReadExcelData.GetRowCount(excelPath, "TitleRateSheet");
                for (int row = 2; row <= rowCount; row++)
                {
                    //TMTI0045469 Verify the Rate sheet is updated with title "Senior Advisor"
                    //Click on the new title rate sheet name
                    string nameRateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TitleRateSheet", row, 1);
                    string pageTitle = rateSheetMgt.SelectTitleRateSheetLV(nameRateSheetExl);
                    Assert.AreEqual(pageTitle, nameRateSheetExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + pageTitle + " Page");

                    string userTitleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TitleRateSheet", row, 2);
                    string titleDefaultRateExl= ReadExcelData.ReadDataMultipleRows(excelPath, "TitleRateSheet", row, 3);

                    //TMTI0045470 Verify the Amount in Rate sheet for title "Senior Advisor" 
                    double defaultRate = rateSheetMgt.GetDefaultRateAsPerRoleLV(userTitleExl);
                    Assert.AreEqual(Convert.ToDouble(titleDefaultRateExl), defaultRate);
                    extentReports.CreateStepLogs("Passed", "Title: " + userTitleExl + " Default Rate: USD " + defaultRate);
                    
                    randomPages.CloseActiveTab(nameRateSheetExl);
                    extentReports.CreateStepLogs("Info", "Rate Sheet: " + nameRateSheetExl + " page is closed ");                    
                }
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "User: " + userSupervisorExl + " logged out");

                //TC - End
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "Admin User Logged Out from SF Lightning View. ");

                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);                
                
                driver.Quit();
            }
        }
    }
}