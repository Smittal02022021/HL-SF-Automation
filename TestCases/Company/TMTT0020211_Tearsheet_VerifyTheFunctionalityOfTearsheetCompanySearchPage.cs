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
    class TMTT0020211_Tearsheet_VerifyTheFunctionalityOfTearsheetCompanySearchPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LVTearsheetPage lvTearsheetPage = new LVTearsheetPage();

        public static string fileTC20211 = "TMTT0020211_Tearsheet_VerifyTheFunctionalityOfTearsheetCompanySearchPage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTheFunctionalityOfTearsheetCompanySearchPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC20211;
                Console.WriteLine(excelPath);

                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                string companyName = ReadExcelData.ReadData(excelPath, "Companies", 1);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login. ");

                //Search standard user by global search
                homePage.SearchUserByGlobalSearch(fileTC20211, user);
                extentReports.CreateLog("User " + user + " details are displayed. ");

                //Login user
                usersLogin.LoginAsSelectedUser();

                //Switch to lightning view
                if (driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateLog("User switched to lightning view. ");
                }

                Assert.IsTrue(login.ValidateUserLightningView(fileTC20211, 2));
                extentReports.CreateLog("CF Financial User: " + user + " is able to login into lightning view. ");

                //TC - TMTI0045148 - Verify that the Tearsheet Company Search Page will have a Navigational Item on HL Banker
                Assert.IsTrue(lvHomePage.VerifyThereExistTearsheetAsANavigationalItemOnHLBanker());
                extentReports.CreateLog("The Tearsheet Company Search Page have a Navigational Item on HL Banker. ");

                Assert.IsTrue(lvHomePage.VerifyUserNavigatedToTearsheetSearchCompanyPage());
                extentReports.CreateLog("User has navigated to Tearsheet Company Search Page. ");

                //TC - TMTI0045143 - Verify that Tearsheet Search page will have company search field.
                Assert.IsTrue(lvTearsheetPage.VerifyTearsheetSearchPageHaveCompanySearchField());
                extentReports.CreateLog("Tearsheet search page have Company Search field. ");

                //TC - TMTI0045141 - Verify that on clicking "x" sign, will clear out the Search bar.
                Assert.IsTrue(lvTearsheetPage.VerifyClickingOnXSignClearsOutSearchField(companyName));
                extentReports.CreateLog("Clicking x sign, clears out the Search bar. ");

                //TC - TMTI0045144 - Verify that when user inputs the name of a company, the dropdown will populate with companies that matches with typed name.
                Assert.IsTrue(lvTearsheetPage.VerifyCompanyDropdownIsPopulatedWhenUserEntersCompanyNameInCompanySearchField(companyName));
                extentReports.CreateLog("When user inputs the name of a company, the dropdown search results starts populating the companies that matches with typed name. ");

                //TC - TMTI0045147 - Verify that the companies that are HQ will have the flag "HQ" in the search.
                Assert.IsTrue(lvTearsheetPage.VerifyHQCompanyHaveHQFlagInSearch());
                extentReports.CreateLog("HQ Companies have the flag HQ when user does a company search. ");

                //TC - TMTI0045145 - Verify that on selecting a company from drop down, the Tearsheet of the company opens up.
                Assert.IsTrue(lvTearsheetPage.VerifyCompanyTearsheetOpensUpUponSelectingCompanyFromDropdown(companyName));
                extentReports.CreateLog("Company Tearsheet opens up upon selecting company from drop down as expected. ");

                //TC - TMTI0045142 - Verify that the companies that are HQ will have the flag "HQ" on top of their name on Tearsheet.
                Assert.IsTrue(lvTearsheetPage.VerifyHQCompanyHaveFlagHQOnTopOfTheirNameOnTearsheet());
                extentReports.CreateLog("HQ companies have flag HQ on top of their name on Tearsheet. ");

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
