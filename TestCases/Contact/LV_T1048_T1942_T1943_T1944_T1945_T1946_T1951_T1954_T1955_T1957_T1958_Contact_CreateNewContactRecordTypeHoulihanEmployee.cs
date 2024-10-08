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
                string extContactFullName = ReadExcelData.ReadData(excelPath, "Contact", 6);
                string compName = ReadExcelData.ReadData(excelPath, "Contact", 1);
                string firstName = ReadExcelData.ReadData(excelPath, "Contact", 2);
                string lastName = ReadExcelData.ReadData(excelPath, "Contact", 3);
                string email = ReadExcelData.ReadData(excelPath, "Contact", 4);
                string PhnNo = ReadExcelData.ReadData(excelPath, "Contact", 5);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowUserType = ReadExcelData.GetRowCount(excelPath, "UsersType");
                for (int row = 2; row <= 2; row++)
                {
                    if (ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", row, 1).Equals("HR"))
                    {
                        // Search standard user by global search
                        string user = ReadExcelData.ReadData(excelPath, "Users", 2);
                        homePage.SearchUserByGlobalSearch(fileTC1048, user);

                        //Verify searched user
                        string userPeople = homePage.GetPeopleOrUserName();
                        string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 2);
                        Assert.AreEqual(userPeopleExl, userPeople);
                        extentReports.CreateLog("User " + userPeople + " details are displayed ");

                        //Login as HR user
                        usersLogin.LoginAsSelectedUser();
                        string HRUser = login.ValidateUser();
                        string HRUserExl = ReadExcelData.ReadData(excelPath, "Users", 2);
                        Assert.AreEqual(HRUserExl.Contains(HRUser), true);
                        extentReports.CreateLog("HR User: " + HRUser + " is able to login ");

                        //Switch to lightning view
                        if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                        {
                            homePage.SwitchToLightningView();
                            extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                        }
                    }
                    else
                    {
                        //Search SF Admin user by global search
                        homePage.SearchUserByGlobalSearch(fileTC1048, adminUser);
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
                    }

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
                    extentReports.CreateStepLogs("Passed", "User selected contact type as :" + contactType + ".");

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

                    //Create New HL Contact
                    lvCreateContact.CreateNewContact(fileTC1048);
                    driver.SwitchTo().DefaultContent();

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

                    if (ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", row, 1).Equals("HR"))
                    {
                        //Delete Created Contact
                        lvContactDetails.DeleteContact();
                        extentReports.CreateStepLogs("Info", "Created contact deleted successfully.");

                    }
                    else
                    {
                        //Verify error message is displaying when departure date is before the hire date
                        contactEdit.EditContact(fileTC1048, 2, 2);

                        contactEdit.UpdateDepartureDate();
                        contactEdit.ClickSaveBtn();

                        String errMsg = contactEdit.TxtErrorMessageDepartureDate();
                        Assert.AreEqual("Error: Departure Date cannot be earlier than Hire Date", errMsg);
                        extentReports.CreateStepLogs("Passed", "Error message: " + errMsg + " is displaying when departure date is before the hire date ");
                        contactEdit.UpdateDepartureDateforInactiveEmployee();

                        contactEdit.ClickSaveBtn();

                        //Verify error message is displaying when departure date is not provided for inactive employee
                        string errMsg1 = contactEdit.TxtErrorMessageDepartureDate();
                        Assert.AreEqual("Error: Departure Date is required for Inactive employees hired after 1/1/2009", errMsg1);
                        extentReports.CreateStepLogs("Passed", "Error message: " + errMsg1 + " is displaying when departure date is required for inactive employees");

                        contactEdit.ClickCancelBtn();

                        //Verify error message for Industry group when LOB is CF
                        contactEdit.VerifyIndustryGroupValidation();
                        contactEdit.ClickSaveBtn();

                        string errMsg2 = contactEdit.TxtErrorMessageDepartureDate();
                        Assert.AreEqual("Error: Industry Group must be selected when LOB is CF", errMsg2);
                        extentReports.CreateStepLogs("Passed", "Error message: " + errMsg2 + " is displaying when industry group must be selected when LOB is CF ");

                        //Verify PDC Validation when primary PDC is null
                        contactEdit.ClickCancelBtn();
                        contactEdit.VerifyPrimaryPDCValidation();
                        contactEdit.ClickSaveBtn();

                        string errMsg3 = contactEdit.TxtErrorMessageDepartureDate();
                        Assert.AreEqual("Error: Primary PDC cannot be blank when there is a Secondary PDC", errMsg3);
                        extentReports.CreateStepLogs("Passed", "Error message: " + errMsg3 + " is displaying when Primary PDC is null ");

                        contactEdit.ClickCancelBtn();

                        //Verify error message is displaying when flag reason comment is not provided
                        contactEdit.VerifyFlagReasonValidation("Contact Has Left Company");
                        contactEdit.ClickSaveBtn();
                        string errMessage = contactEdit.TxtErrorMessageDepartureDate();
                        contactEdit.ClickCancelBtn();

                        Assert.AreEqual("Error: Please provide additional information for selected Flag Reason.", errMessage);
                        extentReports.CreateStepLogs("Passed", "Error message: " + errMessage + " is displaying when flag reason is not selected");

                        //Delete Created Contact
                        lvContactDetails.DeleteContact();
                        extentReports.CreateStepLogs("Info", "Created contact deleted successfully.");
                    }

                    //Logout from SF Lightning View
                    lvHomePage.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Info", "User Logged Out from SF Lightning View. ");
                }

                usersLogin.UserLogOut();
                extentReports.CreateStepLogs("Info", "User Logged Out from SF Classic View. ");

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