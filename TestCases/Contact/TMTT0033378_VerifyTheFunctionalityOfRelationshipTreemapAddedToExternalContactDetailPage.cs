using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using SF_Automation.Pages.Contact;
using System;


namespace SF_Automation.TestCases.Contact
{
    class TMTT0033378_VerifyTheFunctionalityOfRelationshipTreemapAddedToExternalContactDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_ContactDetailsPage lvContactDetailsPage = new LV_ContactDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        
        public static string fileTCTMTT0033378 = "TMTT0033378_VerifyTheFunctionalityOfRelationshipTreemapAddedToExternalContactDetailPage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTheFunctionalityOfRelationshipTreemapAddedToExternalContactDetailPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTCTMTT0033378;
                Console.WriteLine(excelPath);

                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                string externalContactName = ReadExcelData.ReadData(excelPath, "ExternalContact", 1);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "Admin User: " + login.ValidateUser() + " is able to login in SF TEST environment. ");

                //Search CF Financial user by global search
                homePage.SearchUserByGlobalSearch(fileTCTMTT0033378, user);
                extentReports.CreateStepLogs("Info", "CF Financial User: " + user + " details are displayed after doing a global search. ");

                //Login user
                usersLogin.LoginAsSelectedUser();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Passed", "User switched to lightning view. ");
                }

                Assert.IsTrue(login.ValidateUserLightningView(fileTCTMTT0033378, 2));
                extentReports.CreateStepLogs("Passed", "User is able to login into lightning view. ");

                //TC - TMTI0078920, TMTI0078922 - Verify the availability of "Relationship Treemap" and its sections on the External Contact Detail Page.
                lvHomePage.SearchContactFromMainSearch(externalContactName);

                Assert.IsTrue(lvContactDetailsPage.VerifyUserLandedOnCorrectContactDetailsPage(externalContactName));
                extentReports.CreateStepLogs("Passed", "User successfully navigated to an external contact: " + externalContactName + " details page. ");

                Assert.IsTrue(lvContactDetailsPage.VerifyTheAvailableSectionsUnderRelationshipTreeMapOnContactDetailsPage(excelPath));
                extentReports.CreateStepLogs("Passed", "All the expected Relationship Treemap sections are available on the external contact details page. ");

                //TC - TMTI0078924 - Verify the fields displayed in the "Contact Information" section on the Relationship tree.
                Assert.IsTrue(lvContactDetailsPage.VerifyTheAvailableFieldsUnderContactInformationSectionOnContactDetailsPage(excelPath));
                extentReports.CreateStepLogs("Passed", "All the expected fields are available under Contact Information section on the external contact details page. ");

                //Logout from SF Lightning View
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "User Logged Out from SF Lightning View. ");

                //Logout from SF Classic View
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
