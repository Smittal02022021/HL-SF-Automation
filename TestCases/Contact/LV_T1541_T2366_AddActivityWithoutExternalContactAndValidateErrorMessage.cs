using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Activities;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.IO;

namespace SF_Automation.TestCases.Contact
{
    class LV_T1541_T2366_AddActivityWithoutExternalContactAndValidateErrorMessage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();

        LVHomePage lvHomePage = new LVHomePage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();
        LV_ContactsActivityListPage LV_ContactsActivityList = new LV_ContactsActivityListPage();
        LV_AddActivity addActivity = new LV_AddActivity();

        public static string fileTC2366 = "T2366_Contact_AddActivityWithoutExternalContact";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void AddActivityWithoutExternalContact()
        {
            try
            {
                //Get path of Test data file
                string excelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestData", fileTC2366 + ".xlsx");
                excelPath = Path.GetFullPath(excelPath);

                string extContactName = ReadExcelData.ReadData(excelPath, "Contact", 1);
                string relatedCompany = ReadExcelData.ReadData(excelPath, "Contact", 2);
                string tabName = ReadExcelData.ReadData(excelPath, "Contact", 3);

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

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(user));
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + user + " is able to login into lightning view. ");

                //Search external contact
                lvHomePage.SearchContactFromMainSearch(extContactName);
                Assert.IsTrue(lvContactDetails.VerifyUserLandedOnCorrectContactDetailsPage(extContactName));
                extentReports.CreateStepLogs("Passed", "User navigated to external contact details page. ");

                //Navigate to Activity tab
                lvContactDetails.NavigateToActivityTabInsideCFFinancialUser();
                Assert.IsTrue(LV_ContactsActivityList.VerifyUserLandsOnActivityTab());
                extentReports.CreateStepLogs("Passed", "User landed on the Activity tab of external contact. ");

                string type = ReadExcelData.ReadData(excelPath, "Activity", 1);
                string subject = ReadExcelData.ReadData(excelPath, "Activity", 2);
                string msg1 = ReadExcelData.ReadData(excelPath, "Activity", 8);
                string msg2 = ReadExcelData.ReadData(excelPath, "Activity", 9);

                addActivity.ClickAddActivityBtn();

                //Verify that validation appears if the External Contact or Company is not selected while creating an activity.
                Assert.IsTrue(addActivity.VerifyValidationMessageIfBankerTriesToSaveActivityWithoutHLAttendee(fileTC2366, user));
                extentReports.CreateStepLogs("Passed", "Validation message :" + msg2 + " appears if Banker tries to create activity without HL Attendee.");

                addActivity.CloseTab("Add New Activity");
                addActivity.ClickAddActivityBtn();

                //Verify that the validation appears on creating Activity without External Attendee and Company.
                Assert.IsTrue(addActivity.VerifyValidationMessageWithoutExternalAttendeeAndCompany(fileTC2366, extContactName));
                extentReports.CreateStepLogs("Passed", "Validation message :" + msg1 + " appears if Banker tries to create activity without External Attendee and Company.");

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