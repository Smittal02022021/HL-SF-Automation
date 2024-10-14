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
        ContactHomePage conHome = new ContactHomePage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
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

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                int rowUserType = ReadExcelData.GetRowCount(excelPath, "UsersType");
                for (int rows = 2; rows <= rowUserType; rows++)
                {
                    if (ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", rows, 1).Equals("HR"))
                    {
                        // Search HR user by global search
                        string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                        homePage.SearchUserByGlobalSearch(fileTC1063, user);

                        //Verify searched user
                        string userPeople = homePage.GetPeopleOrUserName();
                        string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                        Assert.AreEqual(userPeopleExl, userPeople);
                        extentReports.CreateStepLogs("Passed", "User " + userPeople + " details are displayed ");

                        //Login as HR user
                        usersLogin.LoginAsSelectedUser();
                        string HRUser = login.ValidateUser();
                        string HRUserExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                        Assert.AreEqual(HRUserExl.Contains(HRUser), true);
                        extentReports.CreateStepLogs("Info", "HR User: " + HRUser + " is able to login ");
                    }
                    else
                    {
                        //Switch to lightning view
                        try
                        {
                            homePage.SwitchToLightningView();
                            extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                        }
                        catch(Exception)
                        {
                            extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                        }
                    }

                    //Navigate to Contacts page
                    lvHomePage.NavigateToAnItemFromHLBankerDropdown("Contacts");
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Recently Viewed | Contacts | Salesforce"), true);
                    extentReports.CreateStepLogs("Passed", "User navigated to contacts list page. ");

                    int rowContact = ReadExcelData.GetRowCount(excelPath, "Contact");

                    for (int row = 2; row <= rowContact; row ++)
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

                        Assert.AreEqual(firstName+ " " + lastName, contactDetailHeading);
                        extentReports.CreateStepLogs("Passed", "Contact type: " + contactType + " is created successfuly.");

                        //To edit contact
                        contactEdit.EditContact(fileTC1063, row, rows);
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
                        Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 9) + "\r\n" + ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 10) + ", " + ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 11) + " 92001\r\n" + ReadExcelData.ReadData(excelPath, "Contact", 12), contactCompleteAddress);
                        extentReports.CreateStepLogs("Passed", "Contact address: " + contactCompleteAddress + " including street,city and country in edit contact page matches on contact details page ");

                        //Validated Status
                        string contactStatus = lvContactDetails.GetContactStatus();
                        Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 13), contactStatus);
                        extentReports.CreateStepLogs("Passed", "Contact status: " + contactStatus + " in edit contact page matches on contact details page of HL contact ");

                        //Validated Title
                        string contactTitle = lvContactDetails.GetContactTitle();
                        Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 16), contactTitle);
                        extentReports.CreateStepLogs("Passed", "Contact Title: " + contactTitle + " in edit contact page matches on contact details page of HL contact ");

                        //Validated Department
                        string contactDept = lvContactDetails.GetContactDepartment();
                        Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 17), contactDept);
                        extentReports.CreateStepLogs("Passed", "Contact Department: " + contactDept + " in edit contact page matches on contact details page of HL contact ");

                        //Delete Created Contact
                        lvContactDetails.DeleteContact();
                        extentReports.CreateStepLogs("Info", "Created contact deleted successfully.");
                    }

                    if(ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", rows, 1).Equals("Admin"))
                    {
                        //Switch Back To Classic View
                        lvHomePage.SwitchBackToClassicView();
                        extentReports.CreateStepLogs("Info", "Admin User Switched Back to Classic View. ");
                    }
                    else
                    {
                        //Logout from SF Lightning View
                        lvHomePage.UserLogoutFromSFLightningView();
                        extentReports.CreateStepLogs("Info", "User Logged Out from SF Lightning View. ");
                    }
                }

                usersLogin.UserLogOut();
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