using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using NUnit.Framework;
using SF_Automation.TestData;
using System;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Company;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTT0012451_TMTT0012456_VerifyCFIndustryGroupUpdatedToTECHonCompaniesLightningView : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        CompanyDetailsPage companyDetails = new CompanyDetailsPage();
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();

        public static string fileTMTT0012451 = "LV_TMTT0012451_VerifyCFIndustryGroupUpdatedToTECHonCompanies";

        private string companyName;
        private string appNameExl;
        private string moduleNameExl;
        private string[] arrayCompany = new string[3];
        private int index = 0;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ValidateCompaniesIndstryTypesUpdatedForCFLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0012451;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed. ");
                //Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login. ");
                //Login user
                string userExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearchN(userExl);
                extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string userName = login.ValidateUserLightningView();
                Assert.AreEqual(userName.Contains(userExl), true);
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + userExl + " logged in on Lightning View");
                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on Module: " + moduleNameExl + " Page ");


                ////*************************
               
                //Search via New Industry Group Type
                //TMTI0027298 Verify User is able to search Companies with Industry Group TECH is on Companies Home page
                //Search Company page is different on LV from Classic so this test case is OUT OF SCOPE
                
                //*************************

                //Add New Company
                //TMTI0027296 Verify the CF Industry Group Changes TECH is updated in place of TMT & D&A While Creating Companies 
                int countCompanyRecordTypeExl = ReadExcelData.GetRowCount(excelPath, "CompanyType");
                string industryGroupExl = ReadExcelData.ReadData(excelPath, "IndustryType", 1);
                for (int record = 2; record <= countCompanyRecordTypeExl; record++)
                {
                    string companyRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyType", record, 1);
                     companyName = createCompany.CreateCompanyLV(fileTMTT0012451,companyRecordTypeExl);
                    extentReports.CreateStepLogs("Info", "New Company:  " + companyName + " with Record Type :" + companyRecordTypeExl + " is created");
                    arrayCompany[index] = companyName;
                    index++;
                    Assert.IsTrue(companyDetails.IsIndustryTypePresentLV(industryGroupExl), "Verify New Industry Group Type:TECH is available on Company Detail page");
                    extentReports.CreateStepLogs("Info", "Industry Group Type: TECH is available on Company Detail page ");

                    //TMTI0027303	Verify the Industry Name is updated for Coverage Team page
                    string companyDetailPageTabExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Tabs", 2, 1);
                    companyDetails.ClickCompanyDetailPageTabLV(companyDetailPageTabExl);
                    companyDetails.ClickNewCoverageTeamDefaultRTButtonLV();
                    extentReports.CreateStepLogs("Info", "User is on New Coverage Team with detault Record Type");

                    string coverageIndustryGroupExl = ReadExcelData.ReadData(excelPath, "IndustryType", 2);
                    Assert.IsTrue(companyDetails.IsIndustryTypePresentonCoverageTeamLV(coverageIndustryGroupExl), "Verify Industry Group is updated on Company's Coverage Team under Type Drop-Down ");
                    extentReports.CreateStepLogs("Info", "Industry Group: " + industryGroupExl + " is updated on Company's Coverage Team Type Drop-Down ");
                    randomPages.CloseActiveTab(companyName);
                }
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Pass", "User: " + userExl + " Logged out");
                login.SwitchToClassicView();
                //Deleting ceated compnay as garbage collection 
                for (int i = 0; i < index; i++)
                {
                    companyDetails.DeleteCompanyNew(arrayCompany[i]);
                    extentReports.CreateStepLogs("Info", arrayCompany[i]+" is deleted ");
                }
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }

            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                if (companyName != null)
                {
                    usersLogin.ClickLogoutFromLightningView();
                    login.SwitchToClassicView();
                    for (int i = 0; i < index; i++)
                    {
                        companyDetails.DeleteCompanyNew(arrayCompany[i]);
                        extentReports.CreateStepLogs("Info", arrayCompany[i] + " is deleted ");
                    }
                    usersLogin.UserLogOut();
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Browser Closed");
                }
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
            } 

        }
    }
}
         
