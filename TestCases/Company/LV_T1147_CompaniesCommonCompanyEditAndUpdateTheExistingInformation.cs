using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Company;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Company
{
    class LV_T1147_CompaniesCommonCompanyEditAndUpdateTheExistingInformation:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileTC1147 = "LV_T1147_CompaniesEditAndUpdateTheExistingInformation";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void EditAndUpdateCompanyExistingInformationLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1147;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                // Search standard user by global search
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");


                // Calling Search Company function

                string companyNameExl= ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 2);
                companyHome.GlobalSearchCompanyInLightningView(companyNameExl);

                string companyDetailHeading = companyDetail.GetCompanyDetailsHeadingLV();
                Assert.AreEqual(companyNameExl, companyDetailHeading);
                extentReports.CreateLog("Page with heading: " + companyDetailHeading + " is displayed upon searching company ");

                // Update existing company details
                companyEdit.UpdateExistingCompanyDetailsLV(fileTC1147);
                extentReports.CreateLog("Existing company details are updated sucessfully. ");

                //Validated updated address
                string companyCompleteAddress = companyDetail.GetCompanyCompleteAddressLV();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 6) + " " + ReadExcelData.ReadData(excelPath, "Company", 7) + ", " + ReadExcelData.ReadData(excelPath, "Company", 8) + " " + ReadExcelData.ReadData(excelPath, "Company", 9) + " " + ReadExcelData.ReadData(excelPath, "Company", 5), companyCompleteAddress);
                extentReports.CreateLog("Updated Company address: " + companyCompleteAddress + " includes street,city,state,zip code and country in edit company page matches on company details page ");

                //Validate company text description
                string companyDesc = companyDetail.GetCompanyDescriptionLV();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 10), companyDesc);
                extentReports.CreateLog("Updated Company description: " + companyDesc + " in edit company page matches on company details page ");

                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
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