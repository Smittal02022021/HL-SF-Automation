using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Contact
{
    class TMTC0019251_VerifyBasicUIOfContactsOnSFLightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_RecentlyViewedContactsPage lvRecentlyViewContact = new LV_RecentlyViewedContactsPage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();
        LV_ContactsCreatePage lvCreateContact = new LV_ContactsCreatePage();

        public static string fileTMTC0019251 = "TMTC0019251_VerifyBasicUIOfContactsOnSFLightning";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyBasicUIOfContactsOnSFLightning()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0019251;
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
                homePage.SearchUserByGlobalSearch(fileTMTC0019251, user);
                extentReports.CreateLog("User " + user + " details are displayed. ");

                //Login user
                usersLogin.LoginAsSelectedUser();

                //Switch to lightning view
                if (driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateLog("User switched to lightning view. ");
                }

                Assert.IsTrue(login.ValidateUserLightningView(fileTMTC0019251, 2));
                extentReports.CreateLog("CF Financial User: " + user + " is able to login into lightning view. ");

                //TC - TMT0033921 - Verify that there is Contacts Navigational Item on HL Banker dropdown
                Assert.IsTrue(lvHomePage.VerifyThereExistContactsOptionAsANavigationalItemOnHLBanker());
                extentReports.CreateLog("There exists Contacts Navigational Item on HL Banker dropdown. ");

                //TC - TMT0033922 - Verify that selecting contact will show the "Recently Viewed" by default selected list view of the contacts. 
                Assert.IsTrue(lvHomePage.VerifyUserNavigatedToRecentlyViewedContactsPage());
                extentReports.CreateLog("User has navigated to Recently Viewed Contacts Page. ");

                //TC - TMT0033923 - Verify the different list view options available to view the contacts object.
                Assert.IsTrue(lvRecentlyViewContact.ValidateDiffListViewOptions(fileTMTC0019251));
                extentReports.CreateLog("All list view options are available on recently viewed contacts page. ");

                //TC - TMT0033926 - Verify that clicking "New" will allow the user to navigate to the contact creation form.
                lvRecentlyViewContact.NavigateToCreateNewContactPage();
                extentReports.CreateLog("User navigates to Create New Contact Page from Recently Viewed Contacts Page upon clicking the New button. ");

                lvRecentlyViewContact.CloseTab("New Contact");

                //TC - TMT0033924, TMT0033925 - Verify that the Search functionality on recently viewed contact page as per the entered keyword.
                lvRecentlyViewContact.SearchAndNavigateToContactDetailFromRecentlyViewedContactsListBasedOnView("Test External");
                extentReports.CreateLog("Search functionality on recently viewed contact page is working as expected. ");

                //TC - TMT0034266 - Verify that as a CF Finacial user, external contact have Edit, Add Relationship L, Add Activity, and Printable View as Menu options.
                Assert.IsTrue(lvContactDetails.VerifyButtonsDisplayedAtTheTopOfExternalContactDetailsPageForCFFinancialUser());
                extentReports.CreateLog("External contact have Edit, Add Relationship L, Add Activity, and Printable View buttons displayed at the top. ");

                //TC - TMT0034276 - Verify that as a CF Finacial user, external contact have Company Name,  title, phone and Email in top bar. 
                Assert.IsTrue(lvContactDetails.VerifyDetailsDisplayedAtTheTopBarForExternalContact());
                extentReports.CreateLog("External contact have Company Name,  title, phone and Email in top bar. ");

                //TC - TMT0034269 - Verify that as a CF Finacial user, the external contact has Info, Relationships, coverage, activity, campaign activity, and History tabs. 
                Assert.IsTrue(lvContactDetails.VerifyTabsDisplayedInRightSideForExternalContact(fileTMTC0019251));
                extentReports.CreateLog("External contact have Info, Relationships, coverage, activity, campaign activity, and History tabs. ");

                //TC - TMT0034271 - As CF Finacial user, verify that the Flag Contact and Company details coming in the Quick Updates. 
                Assert.IsTrue(lvContactDetails.VerifyFlagContactAndCompanyDetailSectionsAreDisplayedInRightSideForExternalContact(fileTMTC0019251));
                extentReports.CreateLog("External contact have Flag Contact and Company details coming in the Quick Updates. ");

                lvContactDetails.CloseTab("Test External");

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
                extentReports.CreateExceptionLog(e.Message);
                driver.Quit();
            }
        }
    }
}
