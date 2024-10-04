using SF_Automation.Pages.Common;
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
    class TMT0072201_TMT0072203_TMT0072205_TMT0072207_TMT0072209_VerifyThatDelegateOfAPrimaryAttendeeBankerCan_Add_View_Edit_SendNotification_Delete_TheActivity : BaseClass
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
        Outlook outlook = new Outlook();

        public static string fileTMTC0032670 = "TMTC0032670_VerifyThatDelegateOfPrimaryAttendeeCan_Add_View_Edit_SendNotification_Delete_TheActivity";
        public static string fileOutlook = "Outlook";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyThatDelegateWhoIsPrimaryAttendeeCan_Add_View_Edit_SendNotification_Delete_TheActivity()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0032670;
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                string extContactName = ReadExcelData.ReadData(excelPath, "Contact", 1);
                string relatedCompany = ReadExcelData.ReadData(excelPath, "Contact", 2);
                string tabName = ReadExcelData.ReadData(excelPath, "Contact", 3);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login. ");

                //Search CF Financial User user by global search
                extentReports.CreateStepLogs("Info", "User " + valUser + " details are displayed. ");

                //Login user
                homePage.SearchUserByGlobalSearch(fileTMTC0032670, valUser);
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

                //TMT0072205 Verify that the delegate of the banker (Primary Attendee) can "Add" the activity.
                int totalActivity = ReadExcelData.GetRowCount(excelPath, "Activity");

                string type = ReadExcelData.ReadData(excelPath, "Activity", 1);
                string subject = ReadExcelData.ReadData(excelPath, "Activity", 2);
                string additionalExtAttendee = ReadExcelData.ReadData(excelPath, "MoreAttendees", 1);
                string additionalHLAttendee = ReadExcelData.ReadData(excelPath, "MoreAttendees", 2);
                string updatedSubject = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", 2, 1);

                int beforeCount = LV_ContactsActivityList.GetActivityCount();

                //Create new activity
                addActivity.CreateNewActivityWithDelegates(fileTMTC0032670, 2);
                lvContactDetails.CloseTab("View Activity");

                Assert.IsTrue(LV_ContactsActivityList.VerifyCreatedActivityIsDisplayedUnderActivitiesList(beforeCount));
                extentReports.CreateStepLogs("Passed", "Delegate of Banker is able to add activity successfully. ");

                //TMT0072207 Verify that the delegate of the banker (Primary Attendee) can "Send Notification".
                LV_ContactsActivityList.ViewActivityFromList(subject);
                extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");

                //Send Notification
                activityDetailPage.SendNotification(fileTMTC0032670);

                //Logout from SF Lightning View
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "User Logged Out from SF Lightning View. ");

                driver.Quit();

                //Launch outlook window
                OutLookInitialize();

                //Login into Outlook
                outlook.LoginOutlook(fileOutlook);
                string outlookLabel = outlook.GetLabelOfOutlook();
                Assert.AreEqual("Outlook", outlookLabel);
                extentReports.CreateStepLogs("Passed", "User is logged in to outlook ");

                Assert.IsTrue(outlook.VerifyActivityNotificationIsRecieved(subject));
                extentReports.CreateStepLogs("Passed", "Delegate of the activity is able to send notification from SF successfully.");

                driver.Quit();

                Initialize();

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login. ");

                //Search CF Financial User user by global search
                extentReports.CreateStepLogs("Info", "User " + valUser + " details are displayed. ");

                //Login user
                homePage.SearchUserByGlobalSearch(fileTMTC0032670, valUser);
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

                //TMT0072201 Verify that the delegate of the banker (Primary Attendee) can "View" the activity.
                //TMT0072203 Verify that the delegate of the banker (Primary Attendee) can "Edit" the activity.
                LV_ContactsActivityList.ViewActivityFromList(subject);
                extentReports.CreateStepLogs("Passed", "Delegate is able to View the activity. ");

                //Edit Activity
                activityDetailPage.ClickEditActivityButton();

                //Update Activity as Delegate
                activityDetailPage.UpdateActivityByDelegate(fileTMTC0032670, 2);
                lvContactDetails.CloseTab("View Activity");

                Assert.IsTrue(LV_ContactsActivityList.VerifyUpdatedActivityIsDisplayedUnderActivitiesList(fileTMTC0032670, 2));
                extentReports.CreateStepLogs("Passed", "Activity updated successfully by Delegate of Banker.");

                //TMT0072209 Verify that the delegate of the banker (Primary Attendee) can "Delete" the activity.
                //Deleting Created Activity
                LV_ContactsActivityList.ViewActivityFromList(updatedSubject);
                extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");
                activityDetailPage.DeleteActivity();

                //Verify activity is deleted successfully
                int afterCount = activitiesList.GetActivityCount();
                Assert.AreEqual(beforeCount, afterCount);
                extentReports.CreateStepLogs("Passed", "Delegate is able to delete the activity successfully. ");

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
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
            }
        }
    }
}
