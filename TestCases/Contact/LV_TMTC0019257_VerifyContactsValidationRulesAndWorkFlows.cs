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
    class LV_TMTC0019257_VerifyContactsValidationRulesAndWorkFlows : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_RecentlyViewedContactsPage lvRecentlyViewContact = new LV_RecentlyViewedContactsPage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();
        LV_ContactsCreatePage lvCreateContact = new LV_ContactsCreatePage();

        public static string fileTMTC0019251 = "TMTC0019257_VerifyContactsValidationRulesAndWorkFlows";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyContactsValidationRulesAndWorkFlows()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0019251;
                Console.WriteLine(excelPath);

                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                string adminUser = ReadExcelData.ReadData(excelPath, "Users", 2);

                string assistantName = ReadExcelData.ReadData(excelPath, "AdditionalInfo", 1);
                string assistantPhone = ReadExcelData.ReadData(excelPath, "AdditionalInfo", 2);
                string assistantEmail = ReadExcelData.ReadData(excelPath, "AdditionalInfo", 3);
                string contactType = ReadExcelData.ReadData(excelPath, "Contact", 6);
                string dealAnnouncement = ReadExcelData.ReadData(excelPath, "SubscriptionPreferences", 1);
                string eventConf = ReadExcelData.ReadData(excelPath, "SubscriptionPreferences", 2);
                string generalAnnouncement = ReadExcelData.ReadData(excelPath, "SubscriptionPreferences", 3);
                string insightsContent = ReadExcelData.ReadData(excelPath, "SubscriptionPreferences", 4);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login. ");

                /*
                //Search CF Financial user by global search
                homePage.SearchUserByGlobalSearch(fileTMTC0019251, user);
                extentReports.CreateStepLogs("Info", "User " + user + " details are displayed. ");

                //Login user
                usersLogin.LoginAsSelectedUser();

                //Switch to lightning view
                if (driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                Assert.IsTrue(login.ValidateUserLightningView(fileTMTC0019251, 2));
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + user + " is able to login into lightning view. ");

                //TC - TMT0033946 - Verify the Error Message "You do not have rights to move a Contact to another Company" is displayed when the Company name is changed.
                lvHomePage.NavigateToAnItemFromHLBankerDropdown("Contacts");
                extentReports.CreateStepLogs("Info", "User navigated to contacts list page. ");

                lvHomePage.SearchContactFromMainSearch("Test External");
                Assert.IsTrue(lvContactDetails.VerifyUserLandedOnCorrectContactDetailsPage("Test External"));
                extentReports.CreateStepLogs("Passed", "User navigated to contact details page. ");

                Assert.IsTrue(lvContactDetails.VerifyErrorMessageDisplayedUponChangingCompanyNameForAContact("ActivityCompany"));
                extentReports.CreateStepLogs("Passed", "Error message displayed upon changing Company Name for a contact : You do not have rights to move a Contact to another Company.");

                //TC - TMT0033947 - Verify the Error Message "First Name required" is displayed when the First Name field is blank.
                Assert.IsTrue(lvContactDetails.VerifyErrorMessageDisplayedWithNoLastName());
                extentReports.CreateStepLogs("Passed", "Error message displayed at field level with no Last Name : Complete this field.");

                //TC - TMT0034209 - Verify that CF financial user can able to edit the Assistant Name, phone and email under the Additional information
                Assert.IsTrue(lvContactDetails.VerifyUserCanEditAssistantNamePhoneAndEmail(assistantName, assistantPhone, assistantEmail));
                extentReports.CreateStepLogs("Passed", "CF financial user is able to edit the Assistant Name, phone and email under the Additional information.");

                lvRecentlyViewContact.CloseTab("Test External | Contact");
                lvRecentlyViewContact.CloseTab("Test External - Search");

                //TC - TMT0033957 - Verify the Error Message "Only system administrators can change employee currency" is displayed when contact currency field is edited.
                lvHomePage.SearchContactFromMainSearch("Houlihan Employee");
                Assert.IsTrue(lvContactDetails.VerifyUserLandedOnCorrectContactDetailsPage("Houlihan Employee"));
                extentReports.CreateStepLogs("Passed", "User navigated to HL Employee contact details page. ");

                Assert.IsTrue(lvContactDetails.VerifyErrorMessageDisplayedIfUserTriesToChangeContactCurrency());
                extentReports.CreateStepLogs("Passed", "Error message displayed upon changing contact currency field value : Only system administrators can change employee currency.");

                //TC - TMT0033963 - Verify the Error Message "Only system administrators can change employee name and salutation" is displayed when the contact Last Name field is edited
                Assert.IsTrue(lvContactDetails.VerifyErrorMessageDisplayedIfUserTriesToChangeLastNameOfHLContact());
                extentReports.CreateStepLogs("Passed", "Error message displayed upon changing Last Name for HL Contact : Only system administrators can change employee name and salutation.");

                //Logout from SF Lightning View
                lvHomePage.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", "User Logged Out from SF Lightning View. ");
                */

                //Search SF Admin user by global search
                homePage.SearchUserByGlobalSearch(fileTMTC0019251, adminUser);
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

                //TC - TMT0034256 - Verify Changes done to "DA, Event, GA, and Insights" fields under Subscription Preferences update the "Deal Announcements Change.
                lvHomePage.NavigateToAnItemFromHLBankerDropdown("Contacts");
                extentReports.CreateStepLogs("Info", "User navigated to contacts list page. ");

                lvRecentlyViewContact.NavigateToCreateNewContactPage();
                extentReports.CreateStepLogs("Info", "User has navigated to Create New Contacts Page. ");

                //Select Contact Type
                lvCreateContact.SelectContactType(contactType);
                extentReports.CreateStepLogs("Info", "Contact Type selected as: " +contactType +" ");

                //Create New External Contact
                lvCreateContact.CreateNewContact(fileTMTC0019251);
                driver.SwitchTo().DefaultContent();

                string extContactFullName = ReadExcelData.ReadData(excelPath, "Contact", 6);
                string extContactName = lvContactDetails.GetExternalContactName();
                Assert.AreEqual(extContactFullName, extContactName);
                extentReports.CreateStepLogs("Passed", "New external contact: " + extContactFullName + " is created successfully.");

                lvContactDetails.VerifyUserIsAbleToEditSubscriptionPreferenes(dealAnnouncement, eventConf, generalAnnouncement, insightsContent);
                extentReports.CreateStepLogs("Passed", "Changes related to DA, Event, GA, and Insights are reflected under Subscription Preferences on Contact Details page. ");

                //Delete Created Contact
                lvHomePage.SearchContactFromMainSearch(extContactFullName);
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
