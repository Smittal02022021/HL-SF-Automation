using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SF_Automation.Pages;
using SF_Automation.Pages.Activities;
using SF_Automation.Pages.ActivitiesList;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Diagnostics;
using System.Threading;

namespace SF_Automation.TestCases.Contact
{
    class LV_T1503_T1603_T1604_Contact_ContactDetailsPage_AddActivityEditDelete : BaseClass
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

        public static string fileTC1503 = "T1503_Contact_ContactDetails_AddEditDeleteActivity";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void AddEditAndDeleteActivity()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1503;
                Console.WriteLine(excelPath);

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

                //Loop to iterate Activity Create, Edit and Delete for HL and External Contact
                int rowContactName = ReadExcelData.GetRowCount(excelPath, "ContactTypes");

                for(int row = 2; row <= rowContactName; row++)
                {
                    string contactType = ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", row, 1);
                    string contactName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1);
                    string relatedCompany = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2);

                    //Search external contact
                    lvHomePage.SearchContactFromMainSearch(contactName);
                    Assert.IsTrue(lvContactDetails.VerifyUserLandedOnCorrectContactDetailsPage(contactName));
                    extentReports.CreateStepLogs("Passed", contactType + " details page is opened. ");

                    //Navigate to Activity tab
                    lvContactDetails.NavigateToActivityTabInsideCFFinancialUser();
                    Assert.IsTrue(LV_ContactsActivityList.VerifyUserLandsOnActivityTab());
                    extentReports.CreateStepLogs("Passed", "User landed on the Activity tab of the contact. ");

                    //Verify that the Banker can add the activity of Type - Meeting in Salesforce.
                    //Verify that the Banker can add the activity of Type - Call in Salesforce.
                    //Verify that the Banker can add the activity of Type - Email in Salesforce.
                    //Verify that the Banker can add the activity of Type - Other in Salesforce.

                    int totalActivity = ReadExcelData.GetRowCount(excelPath, "Activity");

                    for(int row1 = 2; row1 <= totalActivity; row1++)
                    {
                        string type = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row1, 1);
                        string subject = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row1, 2);

                        int beforeCount = LV_ContactsActivityList.GetActivityCount();

                        if(contactType == "External Contact")
                        {
                            //Create new activity
                            addActivity.CreateMultipleActivityFromContactActivityPage(fileTC1503, row1);
                        }
                        else
                        {
                            //Create new activity
                            addActivity.CreateMultipleActivityForHLEmployee(fileTC1503, row1);
                        }

                        lvContactDetails.CloseTab("View Activity");

                        Assert.IsTrue(LV_ContactsActivityList.VerifyCreatedActivityIsDisplayedUnderActivitiesList(beforeCount));
                        extentReports.CreateStepLogs("Passed", "Activity created successfully with call type: " + type);

                        //Deleting Created Activity
                        LV_ContactsActivityList.ViewActivityFromList(subject);
                        extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");
                        activityDetailPage.DeleteActivity();

                        //Verify activity is deleted successfully
                        int afterCount = LV_ContactsActivityList.GetActivityCount();
                        Assert.AreEqual(beforeCount, afterCount);
                        extentReports.CreateStepLogs("Passed", "Activity with call type: " + type + " deleted successfully. ");
                    }

                    lvContactDetails.CloseTab(contactName);
                    lvContactDetails.CloseTab(contactName);
                }

                //Logout from SF Lightning View
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "CF Financial user Logged Out from SF Lightning View. ");

                //TC - End
                lvHomePage.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", "Admin User Logged Out from SF Lightning View. ");

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