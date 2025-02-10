using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Threading;

namespace SF_Automation.TestCases.Contact
{
    class LV_T1109_Contact_SelectionAndDeselectionOfSubscriptionPreferencesForExternalContact : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();

        LVHomePage lvHomePage = new LVHomePage();
        LV_RecentlyViewedContactsPage lvRecentlyViewContact = new LV_RecentlyViewedContactsPage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();
        LV_ContactsCreatePage lvCreateContact = new LV_ContactsCreatePage();

        public static string fileTC1109 = "T1109_Contact_SelectionAndDeselectionOfSubscriptionPreferencesForExternalContact";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void SelectionAndDeselectionOfSubscriptionPreferencesForExternalContact()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1109;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

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

                string extContactFullName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 6);
                string compName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 1);
                string firstName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 2);
                string lastName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 3);
                string email = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 4);
                string PhnNo = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 5);

                //Navigate to Contacts page
                lvHomePage.NavigateToAnItemFromHLBankerDropdown("Contacts");
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Recently Viewed | Contacts | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User navigated to contacts list page. ");

                //Select Contact type and click continue
                lvRecentlyViewContact.NavigateToContactTypeSelectionPage();
                extentReports.CreateStepLogs("Info", "User navigated to contacts type selection page. ");

                //Calling select record type and click continue function
                string contactType = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 7);
                lvRecentlyViewContact.SelectContactType(contactType);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Contact: " + contactType + " | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User selected contact type as :" + contactType + ".");

                //Create New External Contact
                lvCreateContact.CreateNewContact(fileTC1109);
                driver.SwitchTo().DefaultContent();

                //Assertion to validate contact name displayed on the contacts detail page
                string extContactName = lvContactDetails.GetExternalContactName();
                Assert.AreEqual(extContactFullName, extContactName);
                extentReports.CreateStepLogs("Passed", "New External contact: " + extContactFullName + " is created successfully.");

                //Navigate to Marketing Tab
                lvContactDetails.NavigateToMarketingTab();
                extentReports.CreateStepLogs("Info", "User navigated to marketing tab. ");

                //Update & Verify Subscription Preferences options
                lvContactDetails.UpdateSubscriptionPreferences();
                Assert.IsTrue(lvContactDetails.VerifySubscriptionPreferencesAreUpdated());
                extentReports.CreateStepLogs("Passed", "Subscription Preferences are updated successfully.");

                //Delete Created Contact
                lvContactDetails.DeleteContact();
                extentReports.CreateStepLogs("Info", "Created contact deleted successfully.");

                //TC - End
                lvHomePage.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", "Admin User Logged Out from SF Lightning View. ");

                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}