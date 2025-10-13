using SF_Automation.Pages.Common;
using SF_Automation.Pages.Company;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using NUnit.Framework;
using SF_Automation.TestData;
using SF_Automation.Pages.HomePage;
using System;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTT0012453_TMTT0012465_VerifyIndustryTypeIsUpdatedForCampaignsAndCapIQCompaniesWithSystemAdminLightningView : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        CampaignHomePage campaignHome = new CampaignHomePage();
        RandomPages randomPages = new RandomPages();
        CapIQCompaniesHomePage capIQCompaniesHome = new CapIQCompaniesHomePage();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTMTT0012453 = "LV_TMTT0012453_VerifyIndustryTypeIsUpdatedForCampaignsAndCapIQCompanies";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyIndustryTypeIsUpdatedForCampaignsAndCapIQCompaniesLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0012453;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed. ");
                //Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login. ");

                //performing actions as System Admin 
                //Login user
                string userExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                //usersLogin.SearchUserAndLogin(userExl);
                homePage.SearchUserByGlobalSearchN(userExl);
                extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();

                login.SwitchToLightningExperience();
                string userName = login.ValidateUserLightningView();
                Assert.AreEqual(userName.Contains(userExl), true);
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + userExl + " logged in on Lightning View");
                //homePageLV.ClickAppLauncher();
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                //TMTI0027300	Verify the Industry Name is updated for Campaigns
                extentReports.CreateStepLogs("Info", "Verify Industry Group Type is updated on Campaigns List ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on Module: " + moduleNameExl + " Page ");

                string ListViewExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ListView", 2, 1);
                randomPages.SelectListViewLV(ListViewExl);
                extentReports.CreateStepLogs("Info", "List View: " + ListViewExl + " is selected");

                string IndustryTypeExl = ReadExcelData.ReadData(excelPath, "IndustryType", 1);
                Assert.IsTrue(campaignHome.IsIndustryGroupAvailableOnCampaignPageLV(IndustryTypeExl), "Verify the Industry Group Type is available in Campaigns List");
                extentReports.CreateStepLogs("Passed", "Campaign with Industry Group Type: " + IndustryTypeExl + " is found in Campaign List ");

                //TMTI0027318	Verify the Industry Name is updated for CapIQCompanies page
                extentReports.CreateStepLogs("Info", "Verify Industry Group Type is updated on CapIQ Company page ");
                //capIQCompaniesHome.ClickCapIQCompanyModule();
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on Module: " + moduleNameExl + " Page ");
                Assert.IsTrue(capIQCompaniesHome.IsIndustryGroupAvailableOnNewCapIQCompanyPageLV(IndustryTypeExl), "Verify Industry Group Type  is available on New CapIQ Company page");
                extentReports.CreateStepLogs("Passed", "Industry Group Type: " + IndustryTypeExl + " is updated on CapIQ Company Page ");

                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Pass", "User: " + userExl + " Logged out");
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Pass", "Browser Closed");

            }
            catch(Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}