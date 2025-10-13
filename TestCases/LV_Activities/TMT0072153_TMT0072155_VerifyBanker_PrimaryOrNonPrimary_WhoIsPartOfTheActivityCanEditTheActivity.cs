using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Activities;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.IO;

namespace SF_Automation.TestCases.LV_Activities
{
    class TMT0072153_TMT0072155_VerifyBanker_PrimaryOrNonPrimary_WhoIsPartOfTheActivityCanEditTheActivity : BaseClass
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

        public static string fileTMTC0032668 = "TMTC0032668_VerifyBanker_PrimaryOrNonPrimary_WhoIsPartOfTheActivityCanEditTheActivity";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyBanker_PrimaryOrNonPrimary_WhoIsPartOfTheActivityCanEditTheActivity()
        {
            try
            {
                //Get path of Test data file
                string excelPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\TestData", fileTMTC0032668 + ".xlsx");
                excelPath = Path.GetFullPath(excelPath);

                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                string extContactName = ReadExcelData.ReadData(excelPath, "Contact", 1);
                string relatedCompany = ReadExcelData.ReadData(excelPath, "Contact", 2);
                string tabName = ReadExcelData.ReadData(excelPath, "Contact", 3);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

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
                extentReports.CreateLog("Admin User is able to login into SF");

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
                lvHomePage.SearchUserFromMainSearch(valUser);

                //Verify searched user
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, valUser + " | Salesforce"), true);
                extentReports.CreateLog("User " + valUser + " details are displayed ");

                //Login as CF Financial user
                lvHomePage.UserLogin();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                //Validate user logged in
                Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(valUser));
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + valUser + " is able to login into lightning view. ");

                //Select HL Banker app
                try
                {
                    lvHomePage.SelectAppLV("HL Banker");
                }
                catch(Exception)
                {
                    lvHomePage.SelectAppLV1("HL Banker");
                }

                //Search external contact
                lvHomePage.SearchContactFromMainSearch(extContactName);
                Assert.IsTrue(lvContactDetails.VerifyUserLandedOnCorrectContactDetailsPage(extContactName));
                extentReports.CreateStepLogs("Passed", "User navigated to external contact details page. ");

                //Navigate to Activity tab
                lvContactDetails.NavigateToActivityTabInsideCFFinancialUser();
                Assert.IsTrue(LV_ContactsActivityList.VerifyUserLandsOnActivityTab());
                extentReports.CreateStepLogs("Passed", "User landed on the Activity tab of external contact. ");

                //TMT0072153 Verify that the banker (Primary Attendee) who is part of the Activity can edit the activity.
                //TMT0072155 Verify that the banker(Non-Primary Attendee) who is part of the Activity can edit the activity.

                int totalActivity = ReadExcelData.GetRowCount(excelPath, "Activity");

                for(int row = 2; row <= totalActivity; row++)
                {
                    string type = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 1);
                    string subject = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 2);
                    string updatedSubject = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 1);
                    string additionalHLAttendee = ReadExcelData.ReadData(excelPath, "MoreAttendees", 2);

                    int beforeCount = LV_ContactsActivityList.GetActivityCount();

                    if(row == 2)
                    {
                        //Create new activity
                        addActivity.CreateMultipleActivityFromContactActivityPage(fileTMTC0032668, row);
                        lvContactDetails.CloseTab("View Activity");

                        Assert.IsTrue(LV_ContactsActivityList.VerifyCreatedActivityIsDisplayedUnderActivitiesList(beforeCount));
                        extentReports.CreateStepLogs("Passed", "Activity created successfully with call type: " + type);

                        LV_ContactsActivityList.ViewActivityFromList(subject);
                        extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");

                        //Edit Activity
                        activityDetailPage.ClickEditActivityButton();

                        //Update Activity as Primary HL Attendee
                        activityDetailPage.UpdateActivityByPrimaryHLAttendee(fileTMTC0032668, row);
                        lvContactDetails.CloseTab("View Activity");

                        Assert.IsTrue(LV_ContactsActivityList.VerifyUpdatedActivityIsDisplayedUnderActivitiesList(fileTMTC0032668, row));
                        extentReports.CreateStepLogs("Passed", "Activity updated successfully by primary HL Attendee");
                    }
                    else
                    {
                        //Create new activity with multiple attendees
                        addActivity.CreateNewActivityWithMultipleAttendees(fileTMTC0032668, row);
                        lvContactDetails.CloseTab("View Activity");

                        Assert.IsTrue(LV_ContactsActivityList.VerifyCreatedActivityIsDisplayedUnderActivitiesList(beforeCount));
                        extentReports.CreateStepLogs("Passed", "Activity created successfully with multiple attendees.");

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

                        //Search user
                        lvHomePage.SearchUserFromMainSearch(additionalHLAttendee);

                        //Verify searched user
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, additionalHLAttendee + " | Salesforce"), true);
                        extentReports.CreateLog("User " + additionalHLAttendee + " details are displayed ");

                        //Login as CF Financial user
                        lvHomePage.UserLogin();
                        Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(additionalHLAttendee));
                        extentReports.CreateStepLogs("Passed", "CF Financial User: " + additionalHLAttendee + " is able to login into lightning view. ");

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

                        //Edit Activity
                        activityDetailPage.ClickEditActivityButton();

                        //Update Activity as Non-Primary HL Attendee
                        activityDetailPage.UpdateActivityByNonPrimaryHLAttendee(fileTMTC0032668, row);
                        lvContactDetails.CloseTab("View Activity");

                        Assert.IsTrue(LV_ContactsActivityList.VerifyUpdatedActivityIsDisplayedUnderActivitiesList(fileTMTC0032668, row));
                        extentReports.CreateStepLogs("Passed", "Activity updated successfully by non-primary HL Attendee");
                    }

                    //Deleting Created Activity
                    LV_ContactsActivityList.ViewActivityFromList(updatedSubject);
                    extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");
                    activityDetailPage.DeleteActivity();

                    //Verify activity is deleted successfully
                    int afterCount = activitiesList.GetActivityCount();
                    Assert.AreEqual(beforeCount, afterCount);
                    extentReports.CreateStepLogs("Passed", "Activity with call type: " + type + " deleted successfully. ");
                }

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
            }
        }
    }
}
