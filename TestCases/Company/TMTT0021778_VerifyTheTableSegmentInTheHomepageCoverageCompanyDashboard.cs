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
    class TMTT0021778_VerifyTheTableSegmentInTheHomepageCoverageCompanyDashboard : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_CompanyDetailsPage lvCompanyDetailsPage = new LV_CompanyDetailsPage();

        public static string fileTMTT0021778 = "TMTT0021778_VerifyTheTableSegmentInTheHomepageCoverageCompanyDashboard";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTheTableSegmentInTheHomepageCoverageCompanyDashboard()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0021778;
                Console.WriteLine(excelPath);

                string user = ReadExcelData.ReadData(excelPath, "Users", 1);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login. ");

                //Search standard user by global search
                homePage.SearchUserByGlobalSearch(fileTMTT0021778, user);
                extentReports.CreateStepLogs("Info", "User " + user + " details are displayed. ");

                //Login user
                usersLogin.LoginAsSelectedUser();

                //Switch to lightning view
                if (driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Passed", "User switched to lightning view. ");
                }

                Assert.IsTrue(login.ValidateUserLightningView(fileTMTT0021778, 2));
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + user + " is able to login into lightning view. ");

                //TC - TMTI0051053 - Verifiy the availability of My Coverage tab under Activities filter
                lvHomePage.NavigateToHomePageTabFromHLBankerDropdown();
                extentReports.CreateStepLogs("Info", "User has navigated to Homepage tab under Home option from HL Banker dropdown. ");

                Assert.IsTrue(lvHomePage.VerifyIfActivitiesFilterGridIsAvailableNextToEngAndOppFilters());
                extentReports.CreateStepLogs("Passed", "Activities filter grid is available next to Engagement & Opportunity filters. ");

                Assert.IsTrue(lvHomePage.VerifyIfUserCanSeeMyCoverageTabUnderActivitiesFilter());
                extentReports.CreateStepLogs("Passed", "My Coverage tab is available on Activities dashboard for CF users. ");

                //TC - TMTI0051068 - Verify that Activity Start Date Filter cell is by default selected 7 days ago
                Assert.IsTrue(lvHomePage.VerifyDefaultValueSelectedInActivityStartDateFilter());
                extentReports.CreateStepLogs("Passed", "Default value selected under Activity Start Date Filter is: Last 7 Days. ");

                //TC - TMTI0054954 - Verification of available columns on My Coverage dashboard in detail activities table
                Assert.IsTrue(lvHomePage.VerifyAvailableColumnsInActivitiesTableOnMyCoverageDashboard(fileTMTT0021778));
                extentReports.CreateStepLogs("Passed", "All columns are displayed as expected on My Coverage Dashboard in detail activities table. ");

                //TC - TMTI0051061 - Verify the availabilty of Activity Start Date Filter on Activity Dashboard
                Assert.IsTrue(lvHomePage.VerifyAvailabilityOfStartDateFilterOnActivityDashboard(fileTMTT0021778));
                extentReports.CreateStepLogs("Passed", "Activity start date filter is available on Activity Dashboard. ");

                //TC - TMTI0054949 - Verify the availabilty of KPI metrices on My Coverage dashboard
                Assert.IsTrue(lvHomePage.VerifyAvailabilityOfKPIMetricesOnMyCoverageDashboard(fileTMTT0021778));
                extentReports.CreateStepLogs("Passed", "All KPI Metrices are available on My Coverage Dashboard. ");

                //TC - TMTI0051067 - Verify the functionality of Activity Start Date grid filter available on My Coverage dashboard
                Assert.IsTrue(lvHomePage.VerifyFunctionalityOfActivityStartDateGridFilterOnMyCoverageDashboard());
                extentReports.CreateStepLogs("Passed", "The functionality of Activity Start Date grid filter is working as expected. ");

                //TC - TMTI0054952 - Verify the functionality of KPI metrices on My Coverage dashboard
                Assert.IsTrue(lvHomePage.VerifyFunctionalityOfKPIMetricesOnMyCoverageDashboard(fileTMTT0021778));
                extentReports.CreateStepLogs("Passed", "The functionality of KPI Metrices is working as expected. ");
                
                //TC - TMTI0054960 - Check the functionality for adding new activities and verify added activity in My Coverage dashboard
                lvHomePage.NavigateToAnItemFromHLBankerDropdown("Companies");
                extentReports.CreateStepLogs("Info", "User has navigated to Companies option from HL Banker dropdown. ");

                string companyName = ReadExcelData.ReadData(excelPath,"Company",1);
                lvHomePage.SearchCompanyFromMainSearch(companyName);
                extentReports.CreateStepLogs("Info", "Company : " + companyName + " detail page is opened. ");

                lvCompanyDetailsPage.NavigateToAParticularTab("Coverage");
                Assert.IsTrue(lvCompanyDetailsPage.VerifyCoverageTabIsOpened());
                extentReports.CreateStepLogs("Passed", "Coverage tab is opened successfully. ");

                Assert.IsTrue(lvCompanyDetailsPage.VerifyLoggedInUserHasIndustryCoverageForACompany(user));
                extentReports.CreateStepLogs("Passed", "User : " + user + " has coverage for the company : " + companyName + " .");

                lvCompanyDetailsPage.NavigateToAParticularTab("Activity");
                Assert.IsTrue(lvCompanyDetailsPage.VerifyActivityTabIsOpened());
                extentReports.CreateStepLogs("Passed", "Activity tab is opened successfully. ");

                lvCompanyDetailsPage.CreateNewActivityFromCompanyDetailPage(fileTMTT0021778);
                lvHomePage.NavigateToHomePageTabFromHLBankerDropdown();
                Assert.IsTrue(lvHomePage.VerifyIfActivitiesFilterGridIsAvailableNextToEngAndOppFilters());
                Assert.IsTrue(lvHomePage.VerifyIfUserCanSeeMyCoverageTabUnderActivitiesFilter());

                extentReports.CreateStepLogs("Passed", "A new activity is created and is visible under My Coverage dashboard. ");

                //Logout from SF Lightning View
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "User Logged Out from SF Lightning View. ");

                //Logout from SF Classic View
                usersLogin.UserLogOut();
                extentReports.CreateStepLogs("Info", "User Logged Out from SF Classic View. ");

                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.Quit();
            }
        }
    }
}
