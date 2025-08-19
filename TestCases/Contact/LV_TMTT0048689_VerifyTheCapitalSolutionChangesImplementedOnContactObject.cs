using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.IO;

namespace SF_Automation.TestCases.Contact
{
    class LV_TMTT0048689_VerifyTheCapitalSolutionChangesImplementedOnContactObject : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_RecentlyViewedContactsPage lvRecentlyViewContact = new LV_RecentlyViewedContactsPage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();
        LV_ContactsCreatePage lvCreateContact = new LV_ContactsCreatePage();

        public static string fileTMTT0048689 = "LV_TMTT0048689_VerifyTheCapitalSolutionChangesImplementedOnContactObject";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTheCapitalSolutionChangesImplementedOnContactObject()
        {
            try
            {
                //Get path of Test data file
                string excelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestData", fileTMTT0048689 + ".xlsx");
                excelPath = Path.GetFullPath(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                //Validate user logged in
                Assert.AreEqual(driver.Url.Contains("lightning"), true);
                extentReports.CreateLog("Admin User is able to login into SF Lightning View");

                //Select HL Banker app
                try
                {
                    lvHomePage.SelectAppLV("HL Banker");
                }
                catch(Exception)
                {
                    lvHomePage.SelectAppLV1("HL Banker");
                }

                //Navigate to Contacts page
                lvHomePage.NavigateToAnItemFromHLBankerDropdown("Contacts");
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Recently Viewed | Contacts | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User navigated to contacts list page.");

                // TMTI0119921 - Verify that the "Staff Industry" picklist value "CS" is added in the list.
                //Search HL Employee contact
                lvRecentlyViewContact.ChangeContactListView("HL Employee");
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "HL Employee | Contacts | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "HL Employee listing page is opened.");

                //Navigate to an HL Employee detail page
                lvRecentlyViewContact.NavigateToHLEmployeeDetailPage();

                //TC - End
                lvHomePage.LogoutFromSFLightningAsApprover();
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
