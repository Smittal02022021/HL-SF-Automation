using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.Contact;

namespace SF_Automation.TestCases.Contacts
{
    class LV_TMTT0012454_VerifyTheIndustryNameIsUpdatedOnContactDetailPageLightningVew:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        ContactHomePage contactHome = new ContactHomePage();
        RandomPages randomPages = new RandomPages();
        ContactDetailsPage contactDetail = new ContactDetailsPage();

        public static string fileTC1197 = "LV_TMTI0027301_VerifyTheIndustryNameIsUpdatedOnContactDetailPage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            InitializeZoom70();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyTheIndustryNameIsUpdatedOnContactDetailPageLV()
        {
            try
            {
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1197;
                string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                usersLogin.SearchUserAndLogin(userExl);
                login.SwitchToLightningExperience();
                string userName = login.ValidateUserLightningView();
                Assert.AreEqual(userName.Contains(userExl), true);
                extentReports.CreateStepLogs("Passed", "User: " + userExl + " logged in on Lightning View");
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                //homePageLV.ClickAppLauncher();
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadData(excelPath, "ModuleName", 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on Module: " + moduleNameExl + " Page ");

                extentReports.CreateStepLogs("Info", "Verify the Industry Type is present on Contact Detail Page");
                string industryType= ReadExcelData.ReadDataMultipleRows(excelPath, "IndustryType", 2, 1);
                string contact = moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 2);
                contactHome.SearchContactInLightning(contact);
                extentReports.CreateStepLogs("Info", "Contact found and selected");

                Assert.IsTrue(contactDetail.IsIndustryTypePresentInDropdownContactDetailPageLV(industryType), "Verify the Desired Industry Group is available in IG List");
                extentReports.CreateStepLogs("Info", "Industry Group:: "+ industryType + " is available in IG List");
                randomPages.CloseActiveTab(contact);
                extentReports.CreateStepLogs("Info", "Contact tab is closed");

                homePageLV.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Pass", "User: " + userExl + " logged out ");
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}