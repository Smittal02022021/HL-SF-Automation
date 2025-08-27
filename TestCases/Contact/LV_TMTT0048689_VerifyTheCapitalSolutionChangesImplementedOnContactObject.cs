using Microsoft.CodeAnalysis;
using NUnit.Framework;
using OpenQA.Selenium.BiDi.Input;
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

                string hlEmployee = ReadExcelData.ReadData(excelPath, "Contact", 1);
                string hlEmployee1 = ReadExcelData.ReadData(excelPath, "Contact", 2);
                string hlEmployee2 = ReadExcelData.ReadData(excelPath, "Contact", 3);
                string hlEmployee3 = ReadExcelData.ReadData(excelPath, "Contact", 4);

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
                //Navigate to an HL Employee detail page
                lvHomePage.SearchHLEmpContactFromMainSearch(hlEmployee);
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

                lvContactDetails.CLickCancelButton();

                lvContactDetails.CloseTab(hlEmployee);
                lvContactDetails.CloseTab(hlEmployee);

                //TMTI0119927 - Verify that the industry group field non-NULL validation is removed for "Capital Solutions" product specialty.
                //Open a new HL Employee having "Product Specialty"="Capital Solutions".

                lvHomePage.SearchHLEmpContactFromMainSearch(hlEmployee1);
                Assert.IsTrue(lvContactDetails.VerifyProductSpecialtyIsCapitalSolution("Capital Solutions"));
                extentReports.CreateStepLogs("Passed", "User navigated to details page of HL Employee having Product Specialty = Capital Solutions.");

                lvContactDetails.ClickEditContactButton();

                string valIndGrp = ReadExcelData.ReadData(excelPath, "Industry Group", 1);

                lvContactDetails.ChangeIndustryGroupValue(valIndGrp);
                Assert.IsTrue(lvContactDetails.VerifyIndustryGroupIsSavedSuccessfully(valIndGrp));
                extentReports.CreateStepLogs("Passed", "The industry group field non-NULL validation is removed for Capital Solutions product specialty.");

                //TMTI0119929 - Verify that the industry group field with value <> None does not display validation when product specialty field having value "Capital Solutions".
                lvContactDetails.ClickEditContactButton();
                string valIndGrp1 = ReadExcelData.ReadData(excelPath, "Industry Group", 2);

                lvContactDetails.ChangeIndustryGroupValue(valIndGrp1);
                Assert.IsTrue(lvContactDetails.VerifyIndustryGroupIsSavedSuccessfully(valIndGrp1));
                extentReports.CreateStepLogs("Passed", "Contact record is getting saved successfully for the Industry group = " + valIndGrp1 + "  having value when Product Specialty = Capital Solutions.");

                lvContactDetails.CloseTab(hlEmployee1);
                lvContactDetails.CloseTab(hlEmployee1);

                lvHomePage.SearchHLEmpContactFromMainSearch(hlEmployee2);

                //TMTI0119966 - Verify that the Staff Industry is assigned the value "CS" if Product Specialty is "Capital Solutions" in Contacts.
                Assert.IsTrue(lvContactDetails.VerifyStaffIndustryIsCSWhenProductSpecialtyIsCapitalSolutions());
                extentReports.CreateStepLogs("Passed", "The Staff Industry is assigned the value as CS, if Product Specialty is Capital Solutions for Contact = " + hlEmployee2);

                //TMTI0120053 - Verify that the HCM Cost Center in the Contact details page displays "CS". 
                Assert.IsTrue(lvContactDetails.VerifyHCMCostCenterDisplaysCSWhenProductSpecialtyIsCapitalSolutions());
                extentReports.CreateStepLogs("Passed", "The HCM Cost Center in the Contact details page displays CS, when Product Specialty = Capital Solutions for contact = " + hlEmployee2);

                //TMTI0119931 - Verify that the "Industry Umbrella" fields appear blank when "Staff Industry" and Product Specialty is "Capital Solutions" in Contacts.
                Assert.IsTrue(lvContactDetails.VerifyIndustryUmbrellaIsBlankWhenProductSpecialtyIsCapitalSolutionsAndStaffIndustryIsCS());
                extentReports.CreateStepLogs("Passed", "The Industry Umbrella fields appear blank when Staff Industry = CS and Product Specialty = Capital Solutions for contact = " + hlEmployee2);

                lvContactDetails.CloseTab(hlEmployee2);
                lvContactDetails.CloseTab(hlEmployee2);

                //TMTI0119933 - Verify that the Product Specialty is assigned as "Mergers & Acquisitions'" for non-CS Product Specialty contacts in Contacts.
                lvHomePage.SearchHLEmpContactFromMainSearch(hlEmployee3);

                Assert.IsTrue(lvContactDetails.VerifyProductSpecialtyIsMergersAcquisitionsForNonCSProductSpecialtyContact());
                extentReports.CreateStepLogs("Passed", "The Product Specialty is = Mergers & Acquisitions for non-CS Product Specialty contact = " + hlEmployee3);

                lvContactDetails.CloseTab(hlEmployee3);
                lvContactDetails.CloseTab(hlEmployee3);

                //TMTI0121384 - Verify that the "Product Specialty" picklist values "Capital Solutions" and "Mergers & Acquisitions" are added and displaying in the list in New Contact page.
                
                //Select Contact type and click continue
                lvRecentlyViewContact.NavigateToContactTypeSelectionPage();
                extentReports.CreateStepLogs("Info", "User navigated to contacts type selection page. ");

                string contactType = ReadExcelData.ReadData(excelPath, "Contact Type", 2);
                lvRecentlyViewContact.SelectContactType(contactType);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Contact: Houlihan Employee | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User selected contact type as: " + contactType + ".");

                Assert.IsTrue(lvCreateContact.VerifyProductSpecialtyPicklistValueInNewContactPage("Capital Solutions", "Mergers & Acquisitions"));
                extentReports.CreateStepLogs("Passed", "The Product Specialty picklist values - Capital Solutions and Mergers & Acquisitions are added in the list on New Contact page.");

                //TC - End
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "Admin user logged out of SF Lightning View. ");

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
