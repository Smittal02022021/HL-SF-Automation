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
    class TMT0072172_TMT0072174_VerifyBothPrimaryAndNonPrimaryAttendeeCanSendNotificationFromSF : BaseClass
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

        public static string fileTMTC0032668 = "TMTC0032668_VerifyBothPrimaryAndNonPrimaryAttendeeCanSendNotificationFromSF";
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
        public void VerifyBothPrimaryAndNonPrimaryAttendeeCanSendNotificationFromSF()
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

                //TMT0072172 Verify that the Non-Primary Attendee of the Activity can "Send Notification" from the Salesforce.
                //TMT0072174 Verify that the Primary Attendee of the Activity can "Send Notification" from the Salesforce.

                int totalActivity = ReadExcelData.GetRowCount(excelPath, "Activity");

                for(int row = 2; row <= totalActivity; row++)
                {
                    string type = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 1);
                    string subject = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 2);
                    string additionalExtAttendee = ReadExcelData.ReadData(excelPath, "MoreAttendees", 1);
                    string additionalHLAttendee = ReadExcelData.ReadData(excelPath, "MoreAttendees", 2);

                    int beforeCount = LV_ContactsActivityList.GetActivityCount();

                    if(row == 2)
                    {
                        //Create new activity
                        addActivity.CreateNewActivityWithMultipleAttendees(fileTMTC0032668, row);
                        lvContactDetails.CloseTab("View Activity");

                        Assert.IsTrue(LV_ContactsActivityList.VerifyCreatedActivityIsDisplayedUnderActivitiesList(beforeCount));
                        extentReports.CreateStepLogs("Passed", "Activity created successfully with multiple attendees for call type: " + type);

                        LV_ContactsActivityList.ViewActivityFromList(subject);
                        extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");

                        //Send Notification
                        activityDetailPage.SendNotification(fileTMTC0032668);

                        //Logout from SF Lightning View
                        lvHomePage.LogoutFromSFLightningAsApprover();
                        extentReports.CreateLog("User Logged Out from SF Lightning View. ");

                        driver.Quit();

                        //Launch outlook window
                        OutLookInitialize();

                        //Login into Outlook
                        outlook.LoginOutlook(fileOutlook);
                        string outlookLabel = outlook.GetLabelOfOutlook();
                        Assert.AreEqual("Outlook", outlookLabel);
                        extentReports.CreateLog("User is logged in to outlook ");

                        Assert.IsTrue(outlook.VerifyActivityNotificationIsRecieved(subject));
                        extentReports.CreateStepLogs("Info", "Primary attendee of the activity is able to send notification from SF successfully.");

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

                        //Deleting Created Activity
                        LV_ContactsActivityList.ViewActivityFromList(subject);
                        extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");
                        activityDetailPage.DeleteActivity();

                        //Verify activity is deleted successfully
                        int afterCount = activitiesList.GetActivityCount();
                        Assert.AreEqual(beforeCount, afterCount);
                        extentReports.CreateStepLogs("Passed", "Activity with call type: " + type + " deleted successfully. ");
                    }
                    else
                    {
                        //Create new activity with multiple attendees
                        addActivity.CreateNewActivityWithMultipleAttendees(fileTMTC0032668, row);
                        lvContactDetails.CloseTab("View Activity");

                        Assert.IsTrue(LV_ContactsActivityList.VerifyCreatedActivityIsDisplayedUnderActivitiesList(beforeCount));
                        extentReports.CreateStepLogs("Passed", "Activity created successfully with multiple attendees for call type: " + type);

                        //Logout from SF Lightning View
                        lvHomePage.LogoutFromSFLightningAsApprover();
                        extentReports.CreateStepLogs("Info", "User Logged Out from SF Lightning View. ");

                        //Login user
                        homePage.SearchUserByGlobalSearch(fileTMTC0032668, additionalHLAttendee);
                        usersLogin.LoginAsSelectedUser();

                        //Switch to lightning view
                        if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                        {
                            homePage.SwitchToLightningView();
                            extentReports.CreateStepLogs("Passed", "CF Financial User: " + additionalHLAttendee + " is able to login into lightning view. ");
                        }
                        else
                        {
                            extentReports.CreateStepLogs("Passed", "CF Financial User: " + additionalHLAttendee + " is able to login into lightning view. ");
                        }

                        //Search external contact
                        lvHomePage.SearchContactFromMainSearch(extContactName);
                        Assert.IsTrue(lvContactDetails.VerifyUserLandedOnCorrectContactDetailsPage(extContactName));
                        extentReports.CreateStepLogs("Passed", "User navigated to external contact details page. ");

                        //Navigate to Activity tab
                        lvContactDetails.NavigateToActivityTabInsideCFFinancialUser();
                        Assert.IsTrue(LV_ContactsActivityList.VerifyUserLandsOnActivityTab());
                        extentReports.CreateStepLogs("Passed", "User landed on the Activity tab of external contact. ");

                        //View created activity
                        LV_ContactsActivityList.ViewActivityFromList(subject);
                        extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");

                        //Send Notification
                        activityDetailPage.SendNotification(fileTMTC0032668);

                        //Logout from SF Lightning View
                        lvHomePage.LogoutFromSFLightningAsApprover();
                        extentReports.CreateLog("User Logged Out from SF Lightning View. ");

                        driver.Quit();

                        //Launch outlook window
                        OutLookInitialize();

                        //Login into Outlook
                        outlook.LoginOutlook(fileOutlook);
                        string outlookLabel = outlook.GetLabelOfOutlook();
                        Assert.AreEqual("Outlook", outlookLabel);
                        extentReports.CreateLog("User is logged in to outlook ");

                        Assert.IsTrue(outlook.VerifyActivityNotificationIsRecieved(subject));
                        extentReports.CreateStepLogs("Info", "Non-Primary attendee of the activity is able to send notification from SF successfully.");

                        driver.Quit();

                        Initialize();

                        //Calling Login function                
                        login.LoginApplication();

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

                        //Deleting Created Activity
                        LV_ContactsActivityList.ViewActivityFromList(subject);
                        extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");
                        activityDetailPage.DeleteActivity();

                        //Verify activity is deleted successfully
                        int afterCount = activitiesList.GetActivityCount();
                        Assert.AreEqual(beforeCount, afterCount);
                        extentReports.CreateStepLogs("Passed", "Activity with call type: " + type + " deleted successfully. ");
                    }
                }

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
