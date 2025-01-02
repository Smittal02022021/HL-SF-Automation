using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Company;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Contact
{
    class LV_T1137_T1138_Contacts_AffiliatedCompany_NewAffiliation_SaveCancelEditUpdate : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactHomePage contactHome = new ContactHomePage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        CompanyListCreatePage companyListCreate = new CompanyListCreatePage();
        HomeMainPage homePage = new HomeMainPage();

        LVHomePage lvHomePage = new LVHomePage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();
        LV_AddAffiliatedCompanies addAffiliated = new LV_AddAffiliatedCompanies();

        public static string fileTC1137_TC1138 = "T1137_T1138_Contacts_AffiliatedCompany_NewAffiliation_SaveCancelEditUpdate";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Contacts_AffiliatedCompanyNewAffiliationSaveAndCancel()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1137_TC1138;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

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

                string contactName = ReadExcelData.ReadData(excelPath, "AffiliatedCompany", 2);

                //Validate user logged in
                Assert.AreEqual(driver.Url.Contains("lightning"), true);
                extentReports.CreateLog("User is able to login into SF");

                //Search external contact
                lvHomePage.SearchContactFromMainSearch(contactName);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, contactName + " | Contact | Salesforce"), true);
                extentReports.CreateLog("User landed on Contacts detail page.");

                //Navigate to Affiliated Companies page from quick link
                lvContactDetails.NavigateToNewAffiliatedCompaniesPage();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Affiliation | Salesforce"), true);
                extentReports.CreateLog("User navigated to New Affiliation page.");

                //Click on save button
                addAffiliated.ClickSaveButton();
                Assert.IsTrue(addAffiliated.GetPageLevelError());
                extentReports.CreateLog("New Affiliation page error message displayed upon click of save button without entering details. ");

                //Create new affiliation
                addAffiliated.EnterNewAffilationCompaniesDetails(fileTC1137_TC1138);
                addAffiliated.ClickSaveButton();

                //Get Affiliation ID
                string affID = addAffiliated.GetAffiliationID();
                extentReports.CreateLog("New Affiliation has been created successfully with ID: " + affID);

                //Validate company name
                string affiliationCompanyName = addAffiliated.GetAffiliationCompanyName();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AffiliatedCompany", 1), affiliationCompanyName);
                extentReports.CreateLog("Affiliation Company Name: " + affiliationCompanyName + " on contact details page matches with value entered in new affiliation company page ");

                //Validate company status
                string affiliationCompanyStatus = addAffiliated.GetAffiliationCompanyStatus();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AffiliatedCompany", 3), affiliationCompanyStatus);
                extentReports.CreateLog("Affiliation Company Status: " + affiliationCompanyStatus + " on contact details page matches with value entered in new affiliation company page ");

                //Validate company type
                string affiliationCompanyType = addAffiliated.GetAffiliationCompanyType();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AffiliatedCompany", 4), affiliationCompanyType);
                extentReports.CreateLog("Affiliation Company Type: " + affiliationCompanyType + " on contact details page matches with value entered in new affiliation company page ");

                /*
                //Edit affiliation company 
                addAffiliated.EditNewAffilationCompaniesDetails(fileTC1137_TC1138);
                addAffiliated.ClickSaveAndNewButton();

                string getCompanyName = addAffiliated.GetCompanyNameText();
                Assert.AreEqual("", getCompanyName);
                extentReports.CreateLog("Company name is blank upon edit details of affilation company and click on save and new button ");

                string getContactName = addAffiliated.GetContactNameText();
                Assert.AreEqual("", getContactName);
                extentReports.CreateLog("Contact name is blank upon edit details of affilation company and click on save and new button ");
                
                //Click on cancel button
                addAffiliated.ClickCancelButton();
               
                //Validate company status
                string updatedAffiliationCompanyStatus = contactDetails.GetAffiliationCompanyStatus();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "EditAffiliatedCompany", 1), updatedAffiliationCompanyStatus);
                extentReports.CreateLog("Affiliation Company Status: " + affiliationCompanyStatus + " on contact details page matches with value entered in new affiliation company page ");

                //Validate company type
                string updatedAffiliationCompanyType = contactDetails.GetAffiliationCompanyType();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "EditAffiliatedCompany", 2), updatedAffiliationCompanyType);
                extentReports.CreateLog("Affiliation Company Type: " + affiliationCompanyType + " on contact details page matches with value entered in new affiliation company page ");

                */

                // Delete affiliated companies
                addAffiliated.DeleteAffiliatedCompanies();
                Assert.IsFalse(contactDetails.ValidateNewAffilationCompaniesCreation(), "Affiliation company is not available after deletion ");
                extentReports.CreateLog("Affiliation company is not available after deletion ");

                //Logout from SF Lightning View
                lvHomePage.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", "User Logged Out from SF Lightning View. ");

                driver.Quit();
        }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
}
            
    }
}
