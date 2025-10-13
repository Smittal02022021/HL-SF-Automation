using NUnit.Framework;
using OpenQA.Selenium;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.IO;

namespace SF_Automation.TestCases.Contact
{
    class LV_T1950_Contacts_ValidationRules_PreventHCMProfileFromEditingInternalContactRecords : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        ContactEditPage contactEdit = new ContactEditPage();

        LVHomePage lvHomePage = new LVHomePage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();

        public static string fileTC1950 = "T1950_Contacts_ValidationRules_PreventHCMProfileFromEditingInternalContactRecords";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Contacts_ValidationRules_PreventHCMProfileFromEditingInternalContactRecords()
        {
            try
            {
                //Get path of Test data file
                string excelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestData", fileTC1950 + ".xlsx");
                excelPath = Path.GetFullPath(excelPath);

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
                extentReports.CreateStepLogs("Passed", "Admin User is able to login into SF Lightning View");

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
                extentReports.CreateStepLogs("Passed", "User " + user + " details are displayed ");

                //Login as CF Financial user
                lvHomePage.UserLogin();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(user));
                extentReports.CreateStepLogs("Passed", "HCM User: " + user + " is able to login into lightning view. ");

                //Search Houlihan contact
                string contactName = ReadExcelData.ReadData(excelPath, "Contact", 4);

                lvHomePage.SearchContactFromMainSearch(contactName);
                Assert.IsTrue(lvContactDetails.VerifyUserLandedOnCorrectContactDetailsPage(contactName));
                extentReports.CreateStepLogs("Passed", "User navigated to external contact details page. ");

                //Validate if Office field on Edit Contact page is editable
                lvContactDetails.ClickEditContactButton();
                Assert.IsTrue(lvContactDetails.ValidateOffileFieldEditableForHCMUser());
                extentReports.CreateStepLogs("Passed", "Office Field is non-editable for HCM user on edit contacts page. ");

                //Logout from SF Lightning View
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "CF Financial user Logged Out from SF Lightning View. ");

                //TC - End
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "Admin User Logged Out from SF Lightning View. ");

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