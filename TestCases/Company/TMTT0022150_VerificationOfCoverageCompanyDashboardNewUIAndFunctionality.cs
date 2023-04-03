using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Companies
{
    class TMTT0022150_VerificationOfCoverageCompanyDashboardNewUIAndFunctionality: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_RecentlyViewedCompaniesPage lvRecentlyViewCompany = new LV_RecentlyViewedCompaniesPage();
        LV_CompanyDetailsPage lvCompanyDetailsPage = new LV_CompanyDetailsPage();

        public static string fileTMTT0022150 = "TMTT0022150_VerificationOfCoverageCompanyDashboardNewUIAndFunctionality";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerificationOfCoverageCompanyDashboardNewUIAndFunctionality()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0022150;
                Console.WriteLine(excelPath);

                string user = ReadExcelData.ReadData(excelPath, "Users", 1);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login. ");

                //Search standard user by global search
                homePage.SearchUserByGlobalSearch(fileTMTT0022150, user);
                extentReports.CreateLog("User " + user + " details are displayed. ");

                //Login user
                usersLogin.LoginAsSelectedUser();

                //Switch to lightning view
                if (driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateLog("User switched to lightning view. ");
                }

                Assert.IsTrue(login.ValidateUserLightningView(fileTMTT0022150, 2));
                extentReports.CreateLog("CF Financial User: " + user + " is able to login into lightning view. ");

                /*
                //TC - TMTI0051053 - Verifiy the availability of My Coverage tab under Activities filter
                lvHomePage.NavigateToHomePageTabFromHLBankerDropdown();
                extentReports.CreateLog("User has navigated to Homepage tab under Home option from HL Banker dropdown. ");

                Assert.IsTrue(lvHomePage.VerifyIfActivitiesFilterGridIsAvailableNextToEngAndOppFilters());
                extentReports.CreateLog("Activities filter grid is available next to Engagement & Opportunity filters. ");

                Assert.IsTrue(lvHomePage.VerifyIfUserCanSeeMyCoverageTabUnderActivitiesFilter());
                extentReports.CreateLog("My Coverage tab is available on Activities dashboard for CF users. ");

                //TC - TMTI0054954 - Verification of available columns on My Coverage dashboard in detail activities table
                Assert.IsTrue(lvHomePage.VerifyAvailableColumnsInActivitiesTableOnMyCoverageDashboard(fileTMTT0022150));
                extentReports.CreateLog("All columns are displayed as expected on My Coverage Dashboard in detail activities table. ");

                //TC - TMTI0051061 - Verify the availabilty of Activity Start Date Filter on Activity Dashboard
                Assert.IsTrue(lvHomePage.VerifyAvailabilityOfStartDateFilterOnActivityDashboard(fileTMTT0022150));
                extentReports.CreateLog("Activity start date filter is available on Activity Dashboard. ");

                //TC - TMTI0051068 - Verify that Activity Start Date Filter cell is by default selected 7 days ago
                Assert.IsTrue(lvHomePage.VerifyDefaultValueSelectedInActivityStartDateFilter());
                extentReports.CreateLog("Default value selected under Activity Start Date Filter is: Last 7 Days. ");

                //TC - TMTI0054949 - Verify the availabilty of KPI metrices on My Coverage dashboard
                Assert.IsTrue(lvHomePage.VerifyAvailabilityOfKPIMetricesOnMyCoverageDashboard(fileTMTT0022150));
                extentReports.CreateLog("All KPI Metrices are available on My Coverage Dashboard. ");

                //TC - TMTI0054952 - Verify the functionality of KPI metrices on My Coverage dashboard
                Assert.IsTrue(lvHomePage.VerifyFunctionalityOfKPIMetricesOnMyCoverageDashboard(fileTMTT0022150));
                extentReports.CreateLog("The functionality of KPI Metrices is working as expected. ");
                
                //TC - TMTI0051067 - Verify the functionality of Activity Start Date grid filter available on My Coverage dashboard
                Assert.IsTrue(lvHomePage.VerifyFunctionalityOfActivityStartDateGridFilterOnMyCoverageDashboard());
                extentReports.CreateLog("The functionality of Activity Start Date grid filter is working as expected. ");

                driver.SwitchTo().DefaultContent();
                */

                //TC - TMTI0054960 - Check the functionality for adding new activities and verify added activity in My Coverage dashboard
                lvHomePage.NavigateToAnItemFromHLBankerDropdown("Companies");
                extentReports.CreateLog("User has navigated to Companies option from HL Banker dropdown. ");

                string companyName = ReadExcelData.ReadData(excelPath,"Company",1);
                lvRecentlyViewCompany.SearchAndNavigateToCompanyDetailFromRecentlyViewedCompaniesListBasedOnView(companyName);
                extentReports.CreateLog("Company : " + companyName + " detail page is opened. ");

                lvCompanyDetailsPage.NavigateToAParticularTab("Coverage");
                Assert.IsTrue(lvCompanyDetailsPage.VerifyCoverageTabIsOpened());
                extentReports.CreateLog("Coverage tab is opened successfully. ");

                Assert.IsTrue(lvCompanyDetailsPage.VerifyLoggedInUserHasIndustryCoverageForACompany(user));
                extentReports.CreateLog("User : " + user + " has coverage for the company : " + companyName + " .");

                lvCompanyDetailsPage.NavigateToAParticularTab("Activity");
                Assert.IsTrue(lvCompanyDetailsPage.VerifyActivityTabIsOpened());
                extentReports.CreateLog("Activity tab is opened successfully. ");

                //Logout from SF Lightning View
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateLog("User Logged Out from SF Lightning View. ");

                //Logout from SF Classic View
                usersLogin.UserLogOut();
                extentReports.CreateLog("User Logged Out from SF Classic View. ");

                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                driver.Quit();
            }
        }
    }
}
