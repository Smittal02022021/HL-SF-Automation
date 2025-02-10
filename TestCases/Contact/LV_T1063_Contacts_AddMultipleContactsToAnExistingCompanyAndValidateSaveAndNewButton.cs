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
    class LV_T1063_Contacts_AddMultipleContactsToAnExistingCompanyAndValidateSaveAndNewButton : BaseClass
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

        public static string fileTC1063 = "T1063_Contacts_AddMultipleContactsToAnExistingCompanyAndValidateSaveAndNewButton";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void AddMultipleContactsToAnExistingCompanyAndValidateSaveAndNewButton()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1063;
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

                //Navigate to Contacts page
                lvHomePage.NavigateToAnItemFromHLBankerDropdown("Contacts");
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Recently Viewed | Contacts | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User navigated to contacts list page. ");

                int rowContact = ReadExcelData.GetRowCount(excelPath, "Contact");

                for(int row = 2; row <= rowContact; row++)
                {
                    //Select Contact type and click continue
                    lvRecentlyViewContact.NavigateToContactTypeSelectionPage();
                    extentReports.CreateStepLogs("Info", "User navigated to contacts type selection page. ");

                    //Calling select record type and click continue function
                    string contactType = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 7);
                    lvRecentlyViewContact.SelectContactType(contactType);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Contact: " + contactType + " | Salesforce"), true);
                    extentReports.CreateStepLogs("Passed", "User selected contact type as :" + contactType + ".");

                    //Create New Contact
                    lvCreateContact.CreateNewContactMultipleRows(fileTC1063, row);
                    driver.SwitchTo().DefaultContent();

                    string contactDetailHeading = lvContactDetails.GetContactDetailsHeading();
                    string firstName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2);
                    string lastName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 3);

                    Assert.AreEqual(firstName + " " + lastName, contactDetailHeading);
                    extentReports.CreateStepLogs("Passed", "Contact type: " + contactType + " is created successfuly.");

                    //To edit contact
                    contactEdit.EditContact(fileTC1063, row, 2);
                    extentReports.CreateStepLogs("Info", "Contact details updated successfuly.");

                    //Validate the company name selected display on contact details page
                    string companyName = lvContactDetails.GetCompanyName();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1), companyName);
                    extentReports.CreateStepLogs("Passed", "Company Name: " + companyName + " in add contact page matches on contact details page");

                    //Validate the contact full name selected display on contact details page
                    string getFirstName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2);
                    string getMiddleName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 8);
                    string getLastName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 3);

                    Assert.AreEqual(getFirstName + " " + getMiddleName + " " + getLastName, lvContactDetails.GetFirstAndLastName());
                    extentReports.CreateStepLogs("Passed", "First Name,Middle Name and Last Name: " + lvContactDetails.GetFirstAndLastName() + " in add contact page matches on contact details page");

                    //Validated Address
                    string contactCompleteAddress = lvContactDetails.GetContactCompleteAddress();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 9) + "\r\n" + ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 10), contactCompleteAddress);
                    extentReports.CreateStepLogs("Passed", "Contact address: " + contactCompleteAddress + " including street,city and country in edit contact page matches on contact details page ");

                    //Validated Title
                    string contactTitle = lvContactDetails.GetContactTitle(fileTC1063, row);
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 15), contactTitle);
                    extentReports.CreateStepLogs("Passed", "Contact Title: " + contactTitle + " in edit contact page matches on contact details page of HL contact ");

                    //Validated Department
                    string contactDept = lvContactDetails.GetContactDepartment(fileTC1063, row);
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 16), contactDept);
                    extentReports.CreateStepLogs("Passed", "Contact Department: " + contactDept + " in edit contact page matches on contact details page of HL contact ");

                    //Delete Created Contact
                    lvContactDetails.DeleteContact();
                    extentReports.CreateStepLogs("Info", "Created contact deleted successfully.");
                }

                //User Logout
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