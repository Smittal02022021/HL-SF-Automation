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
    class LV_T1047_T1100_T1941_T1945_T2146_T2145_Contact_CreateNewContactRecordTypeExternalContact : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_RecentlyViewedContactsPage lvRecentlyViewContact = new LV_RecentlyViewedContactsPage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();
        LV_ContactsCreatePage lvCreateContact = new LV_ContactsCreatePage();

        public static string fileTC1047 = "T1047_Contact_CreateNewContactRecordTypeExternalContact";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CreateContactTypeExternalContact()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1047;
                Console.WriteLine(excelPath);

                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                string adminUser = ReadExcelData.ReadData(excelPath, "Users", 2);
                string extContactFullName = ReadExcelData.ReadData(excelPath, "Contact", 6);
                string compName = ReadExcelData.ReadData(excelPath, "Contact", 1);
                string firstName = ReadExcelData.ReadData(excelPath, "Contact", 2);
                string lastName = ReadExcelData.ReadData(excelPath, "Contact", 3);
                string email = ReadExcelData.ReadData(excelPath, "Contact", 4);
                string PhnNo = ReadExcelData.ReadData(excelPath, "Contact", 5);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login. ");

                //Search CF Financial user by global search
                homePage.SearchUserByGlobalSearch(fileTC1047, user);
                extentReports.CreateStepLogs("Info", "User " + user + " details are displayed. ");

                //Login user
                usersLogin.LoginAsSelectedUser();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                Assert.IsTrue(login.ValidateUserLightningView(fileTC1047, 2));
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + user + " is able to login into lightning view. ");

                //Navigate to Contacts page
                lvHomePage.NavigateToAnItemFromHLBankerDropdown("Contacts");
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Recently Viewed | Contacts | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User navigated to contacts list page. ");

                //Navigate to Create New Contacts Page
                lvRecentlyViewContact.NavigateToCreateNewContactPage();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Contact | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User has navigated to Create New Contacts Page. ");

                //Validate FirstName, LastName and CompanyName display with red flag as mandatory fields
                Assert.IsTrue(lvCreateContact.ValidateMandatoryFields(), "Validate Mandatory fields");
                extentReports.CreateStepLogs("Passed", "Validated FirstName, LastName and CompanyName displayed with red flag as mandatory fields ");

                //Calling click save button function
                lvCreateContact.ClickSaveButton();

                //Validation of company error message
                Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver, "Company Name").Text.Contains("You must enter a value"));
                extentReports.CreateStepLogs("Passed", "Company name error message displayed upon click of save button without entering details ");

                //Validation of first name error message
                Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver, "First Name").Text.Contains("You must enter a value"));
                extentReports.CreateStepLogs("Passed", "First name error message displayed upon click of save button without entering details ");

                //Validation of last name error message
                Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver, "Last Name").Text.Contains("You must enter a value"));
                extentReports.CreateStepLogs("Passed", "Last name error message displayed upon click of save button without entering details ");

                //Create New External Contact
                lvCreateContact.CreateNewContact(fileTC1047);
                driver.SwitchTo().DefaultContent();

                //Assertion to validate contact name displayed on the contacts detail page
                string extContactName = lvContactDetails.GetExternalContactName();
                Assert.AreEqual(extContactFullName, extContactName);
                extentReports.CreateStepLogs("Passed", "New external contact: " + extContactFullName + " is created successfully.");

                // Assertion to validate the company name selected display on contact details page
                string companyName = lvContactDetails.GetCompanyName();
                Assert.AreEqual(compName, companyName);
                extentReports.CreateStepLogs("Passed", "Company Name: " + companyName + " in add contact page matches on contact details page");

                //Assertion to validate contact record type
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "ContactTypes", 1), lvContactDetails.GetContactRecordTypeValue());
                extentReports.CreateStepLogs("Passed", "Validation of contact with Record Type " + lvContactDetails.GetContactRecordTypeValue() + " created with detailed information" +
                    " ,Contact Record type is displayed under system information section ");

                /*
                //Verified quick links and Related objects for external contact 
                Assert.IsTrue(contactEdit.ValidateQuickLink(fileTC1047));
                extentReports.CreateStepLogs("Passed", "Verified quick links and Related objects for external contact ");

                //Verify error message is dispalyed when user tries to change company
                contactEdit.EditCompany();
                contactEdit.ClickSaveBtn();
                String errMsg = contactEdit.TxtErrorMessageCompany();
                Assert.AreEqual("Error: Invalid Data.\r\nReview all error messages below to correct your data.\r\nYou do not have rights to move a Contact to another Company.", errMsg);
                extentReports.CreateStepLogs("Passed", "Error message: "+ errMsg+" is displaying when user tries to change company");
                */

                //Logout from SF Lightning View
                lvHomePage.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", "User Logged Out from SF Lightning View. ");

                //Search SF Admin user by global search
                homePage.SearchUserByGlobalSearch(fileTC1047, adminUser);
                extentReports.CreateStepLogs("Info", "User " + adminUser + " details are displayed. ");

                //Login user
                usersLogin.LoginAsSelectedUser();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                extentReports.CreateStepLogs("Info", "SF Admin User: " + adminUser + " is able to login into lightning view. ");

                lvHomePage.SearchContactFromMainSearch(extContactFullName);
                Assert.IsTrue(lvContactDetails.VerifyUserLandedOnCorrectContactDetailsPage(extContactFullName));
                extentReports.CreateStepLogs("Passed", "User navigated to contact details page. ");

                //Delete Created Contact
                lvContactDetails.DeleteContact();
                extentReports.CreateStepLogs("Info", "Created contact deleted successfully.");

                //Logout from SF Lightning View
                lvHomePage.UserLogoutFromSFLightningView();
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