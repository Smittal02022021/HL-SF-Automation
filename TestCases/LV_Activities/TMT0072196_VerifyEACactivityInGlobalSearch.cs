using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;
using SF_Automation.Pages.Activities;
using SF_Automation.Pages.Contact;

namespace SF_Automation.TestCases.LV_Activities
{
    class TMT0072196_VerifyEACactivityInGlobalSearch : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();
        LV_ContactsActivityListPage LV_ContactsActivityList = new LV_ContactsActivityListPage();

        LV_AddActivity addActivity = new LV_AddActivity();
        LV_ActivitiesList activitiesList = new LV_ActivitiesList();
        LV_ActivityDetailPage activityDetailPage = new LV_ActivityDetailPage();

        public static string fileTMTC0032668 = "TMTC0032668_VerifyEACactivityInGlobalSearch";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyEACactivityInGlobalSearch()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0032668;
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                string extContactName = ReadExcelData.ReadData(excelPath, "Contact", 1);
                string relatedCompany = ReadExcelData.ReadData(excelPath, "Contact", 2);
                string tabName = ReadExcelData.ReadData(excelPath, "Contact", 3);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login. ");

                //Search CF Financial User user by global search
                extentReports.CreateStepLogs("Info", "User " + valUser + " details are displayed. ");

                //Login user
                homePage.SearchUserByGlobalSearch(fileTMTC0032668, valUser);
                usersLogin.LoginAsSelectedUser();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Passed", "CF Financial User: " + valUser + " is able to login into lightning view. ");
                }
                else
                {
                    extentReports.CreateStepLogs("Passed", "CF Financial User: " + valUser + " is able to login into lightning view. ");
                }

                //Search external contact
                lvHomePage.SearchContactFromMainSearch(extContactName);
                Assert.IsTrue(lvContactDetails.VerifyUserLandedOnCorrectContactDetailsPage(extContactName));
                extentReports.CreateStepLogs("Passed", "User navigated to external contact details page. ");

                //Navigate to Activity tab
                lvContactDetails.NavigateToActivityTabInsideCFFinancialUser();
                Assert.IsTrue(LV_ContactsActivityList.VerifyUserLandsOnActivityTab());
                extentReports.CreateStepLogs("Passed", "User landed on the Activity tab of external contact. ");

                //TMT0072196 Verify the EAC activity/events in Global Search.

                int totalActivity = ReadExcelData.GetRowCount(excelPath, "Activity");

                for(int row = 2; row <= totalActivity; row++)
                {
                    string type = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 1);
                    string subject = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 2);
                    string additionalExtAttendee = ReadExcelData.ReadData(excelPath, "MoreAttendees", 1);
                    string additionalHLAttendee = ReadExcelData.ReadData(excelPath, "MoreAttendees", 2);

                    //Create new activity
                    int beforeCount = LV_ContactsActivityList.GetActivityCount();
                    addActivity.CreateNewActivityWithMultipleAttendees(fileTMTC0032668, row);
                    lvContactDetails.CloseTab("View Activity");

                    Assert.IsTrue(LV_ContactsActivityList.VerifyCreatedActivityIsDisplayedUnderActivitiesList(beforeCount));
                    extentReports.CreateStepLogs("Passed", "Activity created successfully with call type: " + type);

                    //Logout from SF Lightning View
                    lvHomePage.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "User Logged Out from SF Lightning View. ");

                    //Switch to lightning view
                    if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                    {
                        homePage.SwitchToLightningView();
                        extentReports.CreateStepLogs("Passed", "Admin User is able to login into lightning view. ");
                    }

                    //Verify Admin is able to search activity from Global Search.
                    Assert.IsTrue(lvHomePage.VerifyBankerIsAbleToSearchActivityFromGlobalSearch(subject, valUser, additionalHLAttendee));
                    extentReports.CreateStepLogs("Passed", "Admin user is able to search activity from Global Search.");

                    //Verify if activity details match
                    Assert.IsTrue(activityDetailPage.VerifyIfActivityDetailsMatchWhenNavigatedFromGlobalSearch(fileTMTC0032668, subject, row));
                    extentReports.CreateStepLogs("Passed", "Activity details match when navigated from Global Search.");

                    //Search external contact
                    lvHomePage.SearchContactFromMainSearch(extContactName);
                    Assert.IsTrue(lvContactDetails.VerifyUserLandedOnCorrectContactDetailsPage(extContactName));
                    extentReports.CreateStepLogs("Passed", "User navigated to external contact details page. ");

                    //Navigate to Activity tab
                    lvContactDetails.NavigateToActivityTabInsideCFFinancialUser();
                    Assert.IsTrue(LV_ContactsActivityList.VerifyUserLandsOnActivityTab());
                    extentReports.CreateStepLogs("Passed", "User landed on the Activity tab of external contact. ");

                    //Deleting Main Created Activity
                    LV_ContactsActivityList.ViewActivityFromList(subject);
                    extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");
                    activityDetailPage.DeleteActivityForAdmin();

                    //Verify main activity is deleted successfully
                    int afterCount1 = activitiesList.GetActivityCount();
                    Assert.AreEqual(beforeCount, afterCount1);
                    extentReports.CreateStepLogs("Passed", "Main Activity with call type: " + type + " deleted successfully. ");
                }

                //Switch Back to Classic View
                lvHomePage.SwitchBackToClassicView();

                //Logout from SF Classic View
                usersLogin.UserLogOut();
                extentReports.CreateStepLogs("Info", "Admin User Logged Out from SF Classic View. ");

                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
            }
        }
    }
}
