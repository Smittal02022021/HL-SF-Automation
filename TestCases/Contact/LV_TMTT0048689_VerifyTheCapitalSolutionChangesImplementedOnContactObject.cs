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
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_RecentlyViewedContactsPage lvRecentlyViewContact = new LV_RecentlyViewedContactsPage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();

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

                //TMTI0119921 - Verify that the "Staff Industry" picklist value "CS" is added in the list.

                //Search HL Employee contact
                lvRecentlyViewContact.ChangeContactListView("HL Employee");
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "HL Employee | Contacts | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "HL Employee listing page is opened.");

                //Navigate to an HL Employee detail page
                lvRecentlyViewContact.NavigateToHLEmployeeDetailPage();
                extentReports.CreateStepLogs("Passed", "HL Employee detail page is opened.");

                //Click Edit contact button
                lvContactDetails.ClickEditContactButton();

                //Verify Staff Industry picklist value has 'CS' in it
                Assert.IsTrue(lvContactDetails.VerifyStaffIndustryPicklistValue("CS"));
                extentReports.CreateStepLogs("Passed", "The Staff Industry picklist value - CS is added in the list.");

                //TMTI0119923 - Verify that the "Staff Industry" do not contain picklist values "CM" and "PFG".
                Assert.IsTrue(lvContactDetails.VerifyValuesNotPresentInStaffIndustry("CM", "PFG"));
                extentReports.CreateStepLogs("Passed", "The Staff Industry do not contain picklist values - CM & PFG.");

                lvContactDetails.CLickCancelButton();
                lvContactDetails.ClickEditContactButton();

                //TMTI0119925 - Verify that the "Product Specialty" picklist values "Capital Solutions" and "Mergers & Acquisitions" are added in the list.
                Assert.IsTrue(lvContactDetails.VerifyProductSpecialtyPicklistValue("Capital Solutions", "Mergers & Acquisitions"));
                extentReports.CreateStepLogs("Passed", "The Product Specialty picklist values - Capital Solutions and Mergers & Acquisitions are added in the list.");

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
