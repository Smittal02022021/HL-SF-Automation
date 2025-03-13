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
    class LV_T1103_T1106_Contact_CreateNewRelationshipInHoulihanEmpDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_RecentlyViewedContactsPage lvRecentlyViewContact = new LV_RecentlyViewedContactsPage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();
        LV_ContactsCreatePage lvCreateContact = new LV_ContactsCreatePage();
        LV_ContactEditPage contactEdit = new LV_ContactEditPage();

        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LV_ContactRelationshipPage lVContactRelationship = new LV_ContactRelationshipPage();

        public static string fileTC1103 = "T1103_CreateNewRelationshipInHoulihanEmployeeDetailPage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CreateNewHLRelationshipInHLEmployeeDetailPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1103;
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

                //Search CF Financial user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                lvHomePage.SearchUserFromMainSearch(user);

                //Verify searched user
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, user + " | Salesforce"), true);
                extentReports.CreateLog("User " + user + " details are displayed ");

                //Login as CF Financial user
                lvHomePage.UserLogin();
                Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(user));
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + user + " is able to login into lightning view. ");

                string extContactFullName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 6);
                string compName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 1);
                string firstName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 2);
                string lastName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 3);
                string email = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 4);
                string PhnNo = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 5);

                //Navigate to Contacts page
                lvHomePage.NavigateToAnItemFromHLBankerDropdown("Contacts");
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Recently Viewed | Contacts | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User navigated to contacts list page. ");

                //Create New External Contact
                lvRecentlyViewContact.NavigateToContactTypeSelectionPage();
                lvCreateContact.CreateNewContact(fileTC1103);
                driver.SwitchTo().DefaultContent();

                //Assertion to validate contact name displayed on the contacts detail page
                string extContactName = lvContactDetails.GetExternalContactName();
                Assert.AreEqual(extContactFullName, extContactName);
                extentReports.CreateStepLogs("Passed", "New External contact: " + extContactFullName + " is created successfully.");

                //Calling Create Relationship method to create relationship of external contact with HL contact
                string extContact = ReadExcelData.ReadData(excelPath, "Relationship", 4);
                string hlContact = ReadExcelData.ReadData(excelPath, "Relationship", 1);

                lvContactDetails.CreateRelationship(fileTC1103);
                extentReports.CreateStepLogs("Info", "New HL Contact Relationship is added.");

                //Verify relationship is linked to external contact
                lvContactDetails.NavigateToRelationshipTab();
                Assert.IsTrue(lVContactRelationship.VerifyHLRelationshipIsLinkedToExternContact(hlContact));
                extentReports.CreateStepLogs("Passed", "New HL Contact Relationship is linked to external contact successfully.");

                //Verify Relationship Details
                lVContactRelationship.NavigateToRelationshipDetailPage();
                Assert.IsTrue(lVContactRelationship.VerifyRelationshipDetails(hlContact));
                extentReports.CreateStepLogs("Passed", "Relationship details displayed are correct. ");

                //Delete Relationship
                lVContactRelationship.DeleteRelationship();
                extentReports.CreateStepLogs("Info", "Relationship deleted successfully. ");

                //Logout from SF Lightning View
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "User Logged Out from SF Lightning View. ");

                //Select HL Banker app
                try
                {
                    lvHomePage.SelectAppLV("HL Banker");
                }
                catch(Exception)
                {
                    lvHomePage.SelectAppLV1("HL Banker");
                }

                extentReports.CreateStepLogs("Info", "SF Admin User is able to login into lightning view. ");

                lvHomePage.SearchContactFromMainSearch(extContactFullName);
                Assert.IsTrue(lvContactDetails.VerifyUserLandedOnCorrectContactDetailsPage(extContactFullName));
                extentReports.CreateStepLogs("Passed", "User navigated to contact details page. ");

                //Delete Created Contact
                lvContactDetails.DeleteContact();
                extentReports.CreateStepLogs("Info", "Created contact deleted successfully.");

                //TC - End
                lvHomePage.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", "Admin User Logged Out from SF Lightning View. ");

                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }

    }
}