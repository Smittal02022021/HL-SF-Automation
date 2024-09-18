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
    class TMT0072188_TMT0072190_TMT0072193_VerifyBothPrimaryAndNonPrimaryAttendeeCanEditAndDeleteFollowUpActivity : BaseClass
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

        public static string fileTMTC0032668 = "TMTC0032668_VerifyBothPrimaryAndNonPrimaryAttendeeCanEditFollowUpActivity";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyBothPrimaryAndNonPrimaryAttendeeCanEditAndDeleteFollowUpActivity()
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

                //TMT0072188 Verify that the primary attendee of the activity can edit the Follow-up Activities.
                //TMT0072190 Verify that the Non-primary attendee of the activity can edit the Follow-up Activities.
                //TMT0072193 The banker of the activity should be able to delete the Follow-up Activities.

                int totalActivity = ReadExcelData.GetRowCount(excelPath, "Activity");

                for(int row = 2; row <= totalActivity; row++)
                {
                    string type = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 1);
                    string subject = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 2);
                    string followUpActivityType = ReadExcelData.ReadDataMultipleRows(excelPath, "Followup", row, 1);
                    string updatedSubject = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 1);
                    string additionalExtAttendee = ReadExcelData.ReadData(excelPath, "MoreAttendees", 1);
                    string additionalHLAttendee = ReadExcelData.ReadData(excelPath, "MoreAttendees", 2);

                    //Create new activity
                    int beforeCount = LV_ContactsActivityList.GetActivityCount();
                    addActivity.CreateNewActivityWithMultipleAttendees(fileTMTC0032668, row);
                    lvContactDetails.CloseTab("View Activity");

                    Assert.IsTrue(LV_ContactsActivityList.VerifyCreatedActivityIsDisplayedUnderActivitiesList(beforeCount));
                    extentReports.CreateStepLogs("Passed", "Activity created successfully with call type: " + type);

                    //View Created activity
                    LV_ContactsActivityList.ViewActivityFromList(subject);
                    extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");

                    //Create Followup Activity
                    activityDetailPage.CreateFolloupActivity(fileTMTC0032668);
                    lvContactDetails.CloseTab("View Activity");
                    lvContactDetails.CloseTab("View Activity");

                    int afterCount = LV_ContactsActivityList.GetActivityCount();

                    //Verify Followup activity is created successfully
                    Assert.IsTrue(LV_ContactsActivityList.VerifyFollowupActivityIsCreatedSuccessfully(subject));
                    extentReports.CreateStepLogs("Passed", followUpActivityType + " Followup Activity created successfully. ");

                    if(row == 3)
                    {
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

                        //Edit Followp activity
                        LV_ContactsActivityList.ViewActivityFromList("Follow-up: " + subject);
                        activityDetailPage.ClickEditActivityButton();
                        activityDetailPage.UpdateActivityByNonPrimaryHLAttendee(fileTMTC0032668, row);
                        lvContactDetails.CloseTab("View Activity");

                        Assert.IsTrue(LV_ContactsActivityList.VerifyUpdatedActivityIsDisplayedUnderActivitiesList(fileTMTC0032668, row));
                        extentReports.CreateStepLogs("Passed", "Followup Activity updated successfully by Non-primary HL Attendee");
                    }
                    else
                    {
                        //Edit Followp activity
                        LV_ContactsActivityList.ViewActivityFromList("Follow-up: " + subject);
                        activityDetailPage.ClickEditActivityButton();
                        activityDetailPage.UpdateFollowupActivityByPrimaryHLAttendee(fileTMTC0032668, row);
                        lvContactDetails.CloseTab("View Activity");

                        Assert.IsTrue(LV_ContactsActivityList.VerifyUpdatedActivityIsDisplayedUnderActivitiesList(fileTMTC0032668, row));
                        extentReports.CreateStepLogs("Passed", "Followup Activity updated successfully by primary HL Attendee");
                    }

                    //Deleting Followup Activity
                    LV_ContactsActivityList.ViewActivityFromList(updatedSubject);
                    extentReports.CreateStepLogs("Info", "User redirected to Followup Activity Detail Page ");
                    activityDetailPage.DeleteActivity();

                    //Verify Followup activity is deleted successfully
                    Assert.IsTrue(LV_ContactsActivityList.VerifyFollowupActivityIsDeleted(afterCount));
                    extentReports.CreateStepLogs("Passed", "Activity with call type: " + type + " deleted successfully. ");

                    //Deleting Main Created Activity
                    LV_ContactsActivityList.ViewActivityFromList(subject);
                    extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");
                    activityDetailPage.DeleteActivity();

                    //Verify main activity is deleted successfully
                    int afterCount1 = activitiesList.GetActivityCount();
                    Assert.AreEqual(beforeCount, afterCount1);
                    extentReports.CreateStepLogs("Passed", "Main Activity with call type: " + type + " deleted successfully. ");
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
