using NUnit.Framework;
using OpenQA.Selenium;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace SF_Automation.TestCases.Contact
{
    class LV_T1048_T1942_T1943_T1944_T1945_T1946_T1951_T1954_T1955_T1957_T1958_Contact_CreateNewContactRecordTypeHoulihanEmployee : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_RecentlyViewedContactsPage lvRecentlyViewContact = new LV_RecentlyViewedContactsPage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();
        LV_ContactsCreatePage lvCreateContact = new LV_ContactsCreatePage();
        LV_ContactEditPage contactEdit = new LV_ContactEditPage();

        public static string fileTC1048 = "T1048_Contact_CreateNewContactRecordTypeHoulihanEmployee";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CreateContactTypeHoulihanEmployee()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1048;
                Console.WriteLine(excelPath);

                string adminUser = ReadExcelData.ReadData(excelPath, "Users", 1);

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

                int rowUserType = ReadExcelData.GetRowCount(excelPath, "UsersType");
                for (int row = 2; row <= rowUserType; row++)
                {
                    string extContactFullName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 6);
                    string compName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1);
                    string firstName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2);
                    string lastName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 3);
                    string email = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 4);
                    string PhnNo = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 5);

                    //Navigate to Contacts page
                    lvHomePage.NavigateToAnItemFromHLBankerDropdown("Contacts");
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Recently Viewed | Contacts | Salesforce"), true);
                    extentReports.CreateStepLogs("Passed", "User navigated to contacts list page. ");

                    //Select Contact type and click continue
                    lvRecentlyViewContact.NavigateToContactTypeSelectionPage();
                    extentReports.CreateStepLogs("Info", "User navigated to contacts type selection page. ");
                    
                    string contactType = ReadExcelData.ReadData(excelPath, "Contact", 7);
                    lvRecentlyViewContact.SelectContactType(contactType);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Contact: Houlihan Employee | Salesforce"), true);
                    extentReports.CreateStepLogs("Passed", "User selected contact type as: " + contactType + ".");

                    //Validate FirstName, LastName and CompanyName display with red flag as mandatory fields
                    Assert.IsTrue(lvCreateContact.ValidateMandatoryFieldsForHLEmployee(), "Validate Mandatory fields");
                    extentReports.CreateStepLogs("Passed", "Validated Last Name displayed with red flag as mandatory fields ");

                    //Calling click save button function
                    lvCreateContact.ClickSaveButton();

                    //Validation of last name error message
                    Assert.IsTrue(lvCreateContact.GetMandatoryFieldErrMsgForLastNameField().Contains("Complete this field."));
                    extentReports.CreateStepLogs("Passed", "Last name error message displayed upon click of save button without entering details ");

                    //Create New HL Contact
                    lvCreateContact.CreateNewHLContactMultipleRows(fileTC1048, row);

                    //Assertion to validate contact name displayed on the contacts detail page
                    string extContactName = lvContactDetails.GetExternalContactName();
                    Assert.AreEqual(extContactFullName, extContactName);
                    extentReports.CreateStepLogs("Passed", "New HL contact: " + extContactFullName + " is created successfully.");

                    // Assertion to validate the company name selected display on contact details page
                    string companyName = lvContactDetails.GetCompanyName();
                    Assert.AreEqual(compName, companyName);
                    extentReports.CreateStepLogs("Passed", "Company Name: " + companyName + " in add contact page matches on contact details page");

                    //Assertion to validate contact record type
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "ContactTypes", 1), lvContactDetails.GetContactRecordTypeValue());
                    extentReports.CreateStepLogs("Passed", "Validation of contact with Record Type " + lvContactDetails.GetContactRecordTypeValue() + " created with detailed information" +
                        " ,Contact Record type is displayed under system information section ");

                    //Verify error message is displaying when departure date is before the hire date
                    contactEdit.EditContact(fileTC1048, 2, 2);

                    contactEdit.UpdateDepartureDate();
                    contactEdit.ClickSaveBtn();

                    String errMsg = contactEdit.TxtErrorMessageDepartureDate();
                    Assert.AreEqual("Departure Date cannot be earlier than Hire Date", errMsg);
                    extentReports.CreateStepLogs("Passed", "Error message: " + errMsg + " is displaying when departure date is before the hire date ");

                    contactEdit.ClickCancelBtn();

                    //Verify error message for Industry group when LOB is CF
                    contactEdit.VerifyIndustryGroupValidation();
                    contactEdit.ClickSaveBtn();

                    string errMsg2 = contactEdit.TxtErrorMessageIndustryGroup();
                    Assert.AreEqual("Industry Group must be selected when LOB is CF and Product Specialty is not Capital Solutions.", errMsg2);
                    extentReports.CreateStepLogs("Passed", "Error message: " + errMsg2 + " is displaying when industry group must be selected when LOB is CF ");

                    contactEdit.ClickCancelBtn();

                    //Delete Created Contact
                    lvContactDetails.DeleteContact();
                    extentReports.CreateStepLogs("Info", "Created contact deleted successfully.");
                }

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