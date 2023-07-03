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
    class TMTC0019612_VerifyExternalContactFeaturesOnSFLightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_RecentlyViewedContactsPage lvRecentlyViewContact = new LV_RecentlyViewedContactsPage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();
        LV_ContactsAddActivityPage lvAddActivityForContact = new LV_ContactsAddActivityPage();
        LV_ContactsActivityDetailPage lvActivityDetailForContact = new LV_ContactsActivityDetailPage();
        LV_ContactsCreatePage lvCreateContact = new LV_ContactsCreatePage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();

        public static string fileTMTC0019612 = "TMTC0019612_VerifyExternalContactFeaturesOnSFLightning";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyExternalContactFeaturesOnSFLightning()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0019612;
                Console.WriteLine(excelPath);

                string user = ReadExcelData.ReadData(excelPath, "Users", 1);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login. ");

                //Search standard user by global search
                homePage.SearchUserByGlobalSearch(fileTMTC0019612, user);
                extentReports.CreateLog("User " + user + " details are displayed. ");

                //Login user
                usersLogin.LoginAsSelectedUser();

                //Switch to lightning view
                if (driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateLog("User switched to lightning view. ");
                }
                
                Assert.IsTrue(login.ValidateUserLightningView(fileTMTC0019612, 2));
                extentReports.CreateLog("CF Financial User: " + user + " is able to login into lightning view. ");

                //TC - TMT0033921 - Verify that there is Contacts Navigational Item on HL Banker dropdown
                Assert.IsTrue(lvHomePage.VerifyThereExistContactsOptionAsANavigationalItemOnHLBanker());
                extentReports.CreateLog("There exists Contacts Navigational Item on HL Banker dropdown. ");

                //TC - TMT0033922 - Verify that selecting contact will show the "Recently Viewed" by default selected list view of the contacts. 
                Assert.IsTrue(lvHomePage.VerifyUserNavigatedToRecentlyViewedContactsPage());
                extentReports.CreateLog("User has navigated to Recently Viewed Contacts Page. ");

                //lvRecentlyViewContact.ChangeContactListView("External Contact List");
                lvRecentlyViewContact.NavigateToCreateNewContactPage();
                extentReports.CreateLog("User has navigated to Create New Contacts Page. ");

                //TC - TMT0034856 - Verify new Contact with Record Type "External Contact" is created upon furnishing the detailed information along with the required information.
                //Validate FirstName, LastName and CompanyName display with red flag as mandatory fields
                Assert.IsTrue(lvCreateContact.ValidateMandatoryFields(),"Validate Mandatory fields");
                extentReports.CreateLog("Validated FirstName, LastName and CompanyName displayed with red flag as mandatory fields ");

                //Calling click save button function
                lvCreateContact.ClickSaveButton();

                //Validation of company error message
                Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver,"Company Name").Text.Contains("You must enter a value"));
                extentReports.CreateLog("Company name error message displayed upon click of save button without entering details ");

                //Validation of first name error message
                Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver,"First Name").Text.Contains("You must enter a value"));
                extentReports.CreateLog("First name error message displayed upon click of save button without entering details ");

                //Validation of last name error message
                Assert.IsTrue(CustomFunctions.ContactInformationFieldsErrorElement(driver,"Last Name").Text.Contains("You must enter a value"));
                extentReports.CreateLog("Last name error message displayed upon click of save button without entering details ");

                lvCreateContact.CreateNewContact(fileTMTC0019612);
                string extContactName = lvContactDetails.GetExternalContactName();
                extentReports.CreateLog("New external contact is created. ");

                driver.SwitchTo().DefaultContent();
                lvContactDetails.CloseTab(extContactName);

                lvRecentlyViewContact.ChangeContactListView("Recently Viewed");
                lvRecentlyViewContact.SearchAndNavigateToContactDetailFromRecentlyViewedContactsListBasedOnView(extContactName);
                extentReports.CreateLog("Search functionality on recently viewed contact page is working as expected. ");

                //TC Start - TMT0034863 - As a CF Finacial User, Verify the Quick Links and Related Objects for contact in the Contact Detail page for External Record Type. 
                Assert.IsTrue(lvContactDetails.VerifyButtonsDisplayedAtTheTopOfExternalContactDetailsPageForCFFinancialUser());
                extentReports.CreateLog("External contact have Edit, Add Relationship L, Add Activity, and Printable View buttons displayed at the top for CF Financial user. ");

                Assert.IsTrue(lvContactDetails.VerifyTabsDisplayedOnExternalContactDetailPageForCFFinancialUser());
                extentReports.CreateLog("External contact have Info, Relationship, Activity, Coverage, Campaign History and History tabs displayed at the top for CF Financial user. ");

                //TC - TMT0034884 - Verify adding a New Houlihan Lokey Relationship through the "Add Relationship" button on the External Contact Detail Page. 
                lvContactDetails.AddNewRelationshipUsingAddRelationshipButtonForCFFinancialUser(fileTMTC0019612);
                Assert.IsTrue(lvContactDetails.VerifyNewRelationshipVisibleUnderHLRelationshipTabCFFinancialUser(fileTMTC0019612));
                extentReports.CreateLog("New Relationship has been added successfully for external contact by CF Financial user. ");

                //TC - TMT0034889 - Verify the Relationships can be edited and deleted through Edit and Delete link for respective relationships in Houlihan Lokey Relationships of External Contact.
                Assert.IsTrue(lvContactDetails.VerifyCFFinancialUserIsAbleToEditNewRelationship("Update"));
                extentReports.CreateLog("CF Financial user is able to edit the created New Relationship successfully. ");





                //TC Start - TMT0034847 - Verify the functionality of the Add Activity with a private check for the external type contact.
                Assert.IsTrue(lvContactDetails.VerifyUserNavigatedToAddActivityPageForExternalContact());
                extentReports.CreateLog("User has navigated to Add Activity page. ");

                //Add Private Activity
                lvAddActivityForContact.AddPrivateActivity(fileTMTC0019612);
                string activityDetailheading = lvActivityDetailForContact.GetActivityDetailsHeading();
                Assert.IsTrue(activityDetailheading == "Activity Details");
                extentReports.CreateLog("Private Activity added successfully. ");

                driver.SwitchTo().DefaultContent();
                lvActivityDetailForContact.CloseTab(extContactName);

                //Verify Created activity is displayed for External Contact
                lvRecentlyViewContact.SearchAndNavigateToContactDetailFromRecentlyViewedContactsListBasedOnView(extContactName);
                lvContactDetails.NavigateToActivityTabInsideExternalContact();

                Assert.IsTrue(lvContactDetails.VerifyCreatedActivityDisplayedUnderExternalContact(extContactName));
                extentReports.CreateLog("Created Activity is displayed for external contact. ");

                driver.SwitchTo().DefaultContent();
                lvContactDetails.CloseTab(extContactName);

                //Verify Created activity is displayed for logged in CF Financial User
                lvRecentlyViewContact.SearchAndNavigateToContactDetailFromRecentlyViewedContactsListBasedOnView(user);
                lvContactDetails.NavigateToActivityTabInsideCFFinancialUser();

                Assert.IsTrue(lvContactDetails.VerifyCreatedActivityDisplayedUnderCFFinancialUser(extContactName));
                extentReports.CreateLog("Created Activity is displayed for CF Financial User. ");

                //TC - TMT0034849 - Verify that only members of that Private activity can able to edit the private activity.
                Assert.IsTrue(lvContactDetails.VerifyCFFinancialUserIsAbleToEditActivity());
                extentReports.CreateLog("CF Financial User is able to edit the private activity. ");

                driver.SwitchTo().DefaultContent();
                
                //Logout from SF Lightning View
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateLog("CF Financial User Logged Out from SF Lightning View. ");
                
                /*
                homePage.SwitchToLightningView();
                extentReports.CreateLog("System Admin User switched to lightning view. ");

                //TC - TMT0034866 - As a System Admin User, Verify the Quick Links and Related Objects for contact in the Contact Detail page for External Record Type. 
                lvHomePage.SearchContactFromMainSearch(extContactName);
                Assert.IsTrue(lvContactDetails.VerifyQuickLinksOnContactDetailPageForSysAdminUser(fileTMTC0019612));
                extentReports.CreateLog("All the quick links are displayed as expected for System Admin user. ");

                Assert.IsTrue(lvContactDetails.VerifyTabsDisplayedOnExternalContactDetailPageForSysAdminUser());
                extentReports.CreateLog("External contact have Details, Related and News tabs displayed at the top for System Admin user. ");

                Assert.IsTrue(lvContactDetails.VerifyButtonsDisplayedAtTheTopOfExternalContactDetailsPageForSysAdminUser());
                extentReports.CreateLog("External contact have Tearsheet, Add Relationship, Contact Reports M, Delete and Submit for Approval buttons displayed at the top for System Admin user. ");

                //TC - TMT0034877 - Verify the related Links for external contacts - HL Relationships, Affiliated Companies, Campaign History, Industry Focus, Opportunity Contacts  - leads to respective pages.
                Assert.IsTrue(lvContactDetails.VerifyIfClickingOnQuickLinksLeadsToRespectivePages(fileTMTC0019612));
                extentReports.CreateLog("All quick links leads to respective pages. ");

                //TC - TMT0034884 - Verify adding a New Houlihan Lokey Relationship through the "Add Relationship" button on the External Contact Detail Page. 
                lvContactDetails.AddNewRelationshipUsingAddRelationshipButtonForSysAdmin(fileTMTC0019612);
                Assert.IsTrue(lvContactDetails.VerifyNewRelationshipVisibleUnderHLRelationshipSectionForSysAdmin(fileTMTC0019612));
                extentReports.CreateLog("New Relationship has been added successfully for external contact by System Admin user. ");

                //Switch Back to SF Classic View
                lvHomePage.SwitchBackToClassicView();
                extentReports.CreateLog("System Admin user switched back to SF classic view. ");
                */

                //To Delete created contact
                contactDetails.DeleteCreatedContact(fileTMTC0019612,"External Contact");
                extentReports.CreateLog("Created External Contact is deleted successfully. ");

                //Logout from SF Classic View
                usersLogin.UserLogOut();
                extentReports.CreateLog("System Admin User Logged Out from SF Classic View. ");
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
