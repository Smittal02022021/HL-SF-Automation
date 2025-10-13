using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Activities;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Company;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.IO;

namespace SF_Automation.TestCases.Contact
{
    class LV_T2057_T1841_Contact_ContactDetailsPage_ActivitiesEdit_AddNewCompany : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();

        LVHomePage lvHomePage = new LVHomePage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();
        LV_ContactsActivityListPage LV_ContactsActivityList = new LV_ContactsActivityListPage();
        LV_AddActivity addActivity = new LV_AddActivity();
        LV_ActivityDetailPage activityDetailPage = new LV_ActivityDetailPage();
        LV_CompanyDetailsPage companyDetailsPage = new LV_CompanyDetailsPage();

        public static string fileTC2057 = "T2057_ContactDetailsPage_ActivitiesEdit_AddNewCompany";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]

        public void ContactDetailsPage_Activities_AddNewContact()
        {
            try
            {
                //Get path of Test data file
                string excelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestData", fileTC2057 + ".xlsx");
                excelPath = Path.GetFullPath(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

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
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + user + " is able to login into lightning view. ");

                string contactType = ReadExcelData.ReadData(excelPath, "ContactTypes", 1);
                string contactName = ReadExcelData.ReadData(excelPath, "Contact", 2);
                string relatedCompany = ReadExcelData.ReadData(excelPath, "Contact", 1);
                string companyType = ReadExcelData.ReadData(excelPath, "Company", 1);
                string companyName = ReadExcelData.ReadData(excelPath, "Company", 2);
                string type = ReadExcelData.ReadData(excelPath, "Activity", 1);
                string subject = ReadExcelData.ReadData(excelPath, "Activity", 2);

                //Search external contact
                lvHomePage.SearchContactFromMainSearch(contactName);
                Assert.IsTrue(lvContactDetails.VerifyUserLandedOnCorrectContactDetailsPage(contactName));
                extentReports.CreateStepLogs("Passed", contactType + " details page is opened. ");

                //Navigate to Activity tab
                lvContactDetails.NavigateToActivityTabInsideCFFinancialUser();
                Assert.IsTrue(LV_ContactsActivityList.VerifyUserLandsOnActivityTab());
                extentReports.CreateStepLogs("Passed", "User landed on the Activity tab of the contact. ");

                //Function to add an activity with new company
                int beforeCount = LV_ContactsActivityList.GetActivityCount();

                addActivity.AddAnActivityWithNewCompany(fileTC2057);
                lvContactDetails.CloseTab("View Activity");

                Assert.IsTrue(LV_ContactsActivityList.VerifyCreatedActivityIsDisplayedUnderActivitiesList(beforeCount));
                extentReports.CreateLog("An activity is created with new company name: " + companyName + " and company type: " + companyType);

                //Deleting Created Activity
                LV_ContactsActivityList.ViewActivityFromList(subject);
                extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");
                activityDetailPage.DeleteActivity();

                //Verify activity is deleted successfully
                int afterCount = LV_ContactsActivityList.GetActivityCount();
                Assert.AreEqual(beforeCount, afterCount);
                extentReports.CreateStepLogs("Passed", "Activity with call type: " + type + " deleted successfully. ");

                //Logout from SF Lightning View
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "CF Financial user Logged Out from SF Lightning View. ");

                //Select HL Banker app
                try
                {
                    lvHomePage.SelectAppLV("HL Banker");
                }
                catch(Exception)
                {
                    lvHomePage.SelectAppLV1("HL Banker");
                }

                //Search newly created company
                lvHomePage.SearchCompanyFromMainSearch(companyName);
                Assert.IsTrue(companyDetailsPage.VerifyUserLandsOnCorrectCompanyDetailPage(companyName));
                extentReports.CreateStepLogs("Passed", "User lands on the new company detail page. ");

                //Delete the newly created company
                companyDetailsPage.DeleteCompany();
                extentReports.CreateStepLogs("Info", "Newly created company is deleted successfully. ");

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