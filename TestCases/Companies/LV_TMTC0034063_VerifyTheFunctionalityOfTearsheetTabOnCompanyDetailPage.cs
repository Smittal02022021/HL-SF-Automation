using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTC0034063_VerifyTheFunctionalityOfTearsheetTabOnCompanyDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        HomeMainPage homePage = new HomeMainPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTC0034063 = "TMTC0034027_VerifyTheFunctionalityOfFinancialsTabOnCompanyDetailPage";

        private int rowCompanyName;
        private string companyNameExl;
        private string excelPath;
        private string valUser;
        private string valAdminUser;
        private string user;
        private string appNameExl;
        private string appName;
        private string moduleNameExl;
        private string tabNameExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMT0076605 Verify the availability of the "Tearheet" tab on the Company detail page.
        //TMT0076607 Verify that the "Tearsheet" tab list all the important details related to the company.

        [Test]
        public void VerifyTheFunctionalityOfTearsheetTabOnCompanyDetailPageLV()
        {
            try
            {
                //Get path of Test data file
                excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0034063;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "CF Fin User: " + valUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged in on Lightning View");

                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Companies");
                for(int row = 2; row <= rowCompanyName; row++)
                {
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 1);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Passed", "Company: " + companyNameExl + " found and selected");

                    //TMT0076605	Verify the availability of the "Tearheet" tab on the Company detail page.
                    tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TabName", 6, 1);
                    Assert.IsTrue(companyDetail.IsCompanyDetailPageTabPresentLV(tabNameExl), "Verify the availability of the '" + tabNameExl + "' tab on the Company detail page");
                    extentReports.CreateStepLogs("Passed", tabNameExl + " tab available on the Company detail page");

                    //TMT0076607	Verify that the "Tearsheet" tab list all the important details(Tearsheet,Eng,RecentInfo related to the company.
                    companyDetail.ClickCompanyDetailPageTabLV(tabNameExl);
                    int rowTabTearsheet = ReadExcelData.GetRowCount(excelPath, "TeearsheetSubTabs");

                    for(int subTabrow = 2; subTabrow <= rowTabTearsheet; subTabrow++)
                    {
                        string subTabTearsheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TeearsheetSubTabs", subTabrow, 1);
                        Assert.IsTrue(companyDetail.IsSubTabDisplayedLV(subTabTearsheetExl), "Verify that the 'Tearsheet'tab has sub tab: " + subTabTearsheetExl);
                        extentReports.CreateStepLogs("Passed", " '" + subTabTearsheetExl + "' tab is displayed on Tearsheet tab");
                    }

                    for(int subTabrow = rowTabTearsheet; subTabrow >= 2; subTabrow--)
                    {
                        string subTabTearsheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TeearsheetSubTabs", subTabrow, 1);
                        companyDetail.ClickSubTabLV(subTabTearsheetExl);
                        extentReports.CreateStepLogs("Passed", " '" + subTabTearsheetExl + "' tab is displayed with company details on Tearsheet tab ");
                    }

                    randomPages.CloseActiveTab(companyNameExl);
                }
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "System Administrator User: " + valAdminUser + " logged out");
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch(Exception ex)
            {
                extentReports.CreateExceptionLog(ex.Message);
                homePageLV.LogoutFromSFLightningAsApprover();
                driver.Quit();
            }
        }
    }
}