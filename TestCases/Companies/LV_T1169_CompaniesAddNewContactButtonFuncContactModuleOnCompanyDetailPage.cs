using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;
using SF_Automation.Pages.Contact;

namespace SF_Automation.TestCases.Companies
{
    class LV_T1169_CompaniesAddNewContactButtonFuncContactModuleOnCompanyDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        HomeMainPage homePage = new HomeMainPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();

        public static string fileTC1169 = "LV_T1169_Companies_AddNewContactButtonFuncContactModuleOnCompanyDetailPage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTC0005848/ T1169 Verify the functionality of New Contact button in Contacts related Object of Company Details Page.
        [Test]
        public void AddNewContactButtonFuncContactModuleOnCompanyDetailPageLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1169;
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                string valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(valAdminUser);
                extentReports.CreateStepLogs("Info", "User: " + valAdminUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valAdminUser), true);
                extentReports.CreateStepLogs("Passed", "User: " + valAdminUser + " logged in on Lightning View");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                int companyTypeRow = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int row = 2; row <= companyTypeRow; row++)
                {                    
                    //Search for the Company
                    string companyType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);                                        
                    string companyName = companyDetail.GetCompanyNameHeaderLV(); 
                    Assert.AreEqual(companyNameExl, companyName);
                    extentReports.CreateStepLogs("Passed", "Company: " + companyName + " with Company Type: "+ companyType+" is displayed upon searching company ");
                    companyDetail.ClickQuickLinkLV("Contacts", companyType);
                    companyDetail.ClickRelatedNewContactButtonLV();
                    string txtDialogHeader= companyDetail.GetNewContactDialogHeaderLV();
                    Assert.AreEqual("New Contact",txtDialogHeader);
                    extentReports.CreateStepLogs("Passed", "Select contact record type page is displayed upon clicking New button from Contact section ");

                    //validating ContactRecordType function to validate all types of contact record type
                    Assert.IsTrue(conSelectRecord.AreContactTypesDisplayedLV(fileTC1169), "Verify All Contact Types are Displayed ");
                    extentReports.CreateStepLogs("Passed", "All Contact Types are Displayed for Company: " + companyName + " with Company Type: "+ companyType);
                    conSelectRecord.ClickCancelContactTypePageLV();
                    randomPages.CloseActiveTab(companyName);
                    randomPages.CloseActiveTab(companyName);
                }
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "System Admin: " + valAdminUser + " logged out");
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully ");
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
