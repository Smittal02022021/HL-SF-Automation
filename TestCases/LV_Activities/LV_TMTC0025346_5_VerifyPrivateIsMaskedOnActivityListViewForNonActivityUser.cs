using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Activities;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.Contact;

namespace SF_Automation.TestCases.LV_Activities
{
    internal class LV_TMTC0025346_5_VerifyPrivateIsMaskedOnActivityListViewForNonActivityUser : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage homePageLV = new LVHomePage();
        LV_CompanyDetailsPage lvCompanyDetailsPage = new LV_CompanyDetailsPage();
        LVCompaniesActivityDetailPage lvCompaniesActivityDetailPage = new LVCompaniesActivityDetailPage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();

        LV_AddActivity addActivity = new LV_AddActivity();
        LV_ActivitiesList activitiesList = new LV_ActivitiesList();
        LV_ActivityDetailPage activityDetailPage = new LV_ActivityDetailPage();

        public static string fileTMT0047476 = "TMT0047476_VerifyActivityAccessRelatedFunctionalityOnCompanyDetailPage";
        string msgSaveActivity;
        string msgSaveActivityExl;
        string CompanyNameExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyPrivateIsMaskedOnActivityListViewForNonActivityUser()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT0047476;
                string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                string type = ReadExcelData.ReadData(excelPath, "Activity", 1);
                string subject = ReadExcelData.ReadData(excelPath, "Activity", 2);
                string description = ReadExcelData.ReadData(excelPath, "Activity", 5);
                string meetingNotes = ReadExcelData.ReadData(excelPath, "Activity", 6);

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
                homePage.SearchUserByGlobalSearch(fileTMT0047476, valUser);
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

                homePageLV.ClickAppLauncher();
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectApp(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Info", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                CompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1);
                lvCompanyDetailsPage.SearchCompanyInLightning(CompanyNameExl);
                extentReports.CreateStepLogs("Info"," Company: "+ CompanyNameExl+" found and selected ");
                
                string tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 2);
                extentReports.CreateStepLogs("Info", tabNameExl + " tab is available on " + CompanyNameExl + " Company Detail Page. ");
                lvCompanyDetailsPage.NavigateToAParticularTab("Activity");
                extentReports.CreateStepLogs("Info", " User navigated to Activity List page from " + CompanyNameExl + " :Company Detail Page. ");

                /////////////////////////////////////////////////////
                //Crteating new Activity 
                /////////////////////////////////////////////////////
                int beforeCount = activitiesList.GetActivityCount();
                addActivity.CreateNewActivityAdditionalHLAttandeeFromCompanyDetailPage(fileTMT0047476);
                lvCompaniesActivityDetailPage.CloseTab("View Activity");

                Assert.IsTrue(activitiesList.VerifyCreatedActivityIsDisplayedUnderActivitiesList(beforeCount));
                extentReports.CreateStepLogs("Info", "Activity created successfully. ");

                //lvCompaniesActivityDetailPage.ClickPrivateCheckbox();
                //extentReports.CreateStepLogs("Info", "Activity is Marked as Private ");
                //lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Save");

                //Get popup message
                //msgSaveActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                //msgSaveActivityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SaveActivityPopUpMsg", 2, 1);
                //Assert.AreEqual(msgSaveActivityExl, msgSaveActivity);
                //extentReports.CreateStepLogs("Passed", "Message: " + msgSaveActivity + "is Displayed for Required fields ");

                //lvCompanyDetailsPage.RefreshActivitiesList();

                //Logout from SF Lightning View
                homePageLV.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", "User: " + valUser + " logged out ");

                //TMT0047478 Verify that the non - activity member is not able to edit the activity.
                string nonActivityUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 4, 1);

                //Search CF Financial User Non-Activity user by global search                
                extentReports.CreateStepLogs("Info", "User " + nonActivityUser + " details are displayed. ");

                //Login user
                homePage.SearchUserByGlobalSearch(fileTMT0047476, nonActivityUser);
                usersLogin.LoginAsSelectedUser();

                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateLog("User: " + nonActivityUser + " Switched to Lightning View ");
                }
                else
                {
                    extentReports.CreateStepLogs("Passed", "CF Financial User: " + nonActivityUser + " is able to login into lightning view. ");
                }

                homePageLV.ClickAppLauncher();
                homePageLV.SelectApp(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Info", appName + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                lvCompanyDetailsPage.SearchCompanyInLightning(CompanyNameExl);
                extentReports.CreateStepLogs("Info", " Company: " + CompanyNameExl + " found and selected ");
                extentReports.CreateStepLogs("Info", tabNameExl + " tab is available on " + CompanyNameExl + " Company Detail Page. ");
                lvCompanyDetailsPage.NavigateToAParticularTab("Activity");
                extentReports.CreateStepLogs("Info", " User navigated to Activity List page from " + CompanyNameExl + " :Company Detail Page. ");

                string actualMask;
                actualMask = activitiesList.GetActivityTypeFromList();
                Assert.AreEqual(type, actualMask);
                extentReports.CreateStepLogs("Passed", "Activity Type is displayed as " + actualMask + " for Non-Activity member ");

                actualMask = activitiesList.GetActivitySubjectFromList();
                Assert.AreEqual(subject, actualMask);
                extentReports.CreateStepLogs("Passed", "Activity Subject is displayed as " + actualMask + " for Non-Activity member ");

                actualMask = activitiesList.GetActivityDescriptionFromList();
                Assert.AreEqual(description, actualMask);
                extentReports.CreateStepLogs("Passed", "Activity Description is displayed as " + actualMask + " for Non-Activity member ");

                actualMask = activitiesList.GetActivityMeetingNotesFromList();
                Assert.AreEqual(meetingNotes, actualMask);
                extentReports.CreateStepLogs("Passed", "Activity Meeting/Call Notes is displayed as " + actualMask + " for Non-Activity member ");

                //Logout from SF Lightning View
                homePageLV.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", "User: " + nonActivityUser + " logged out ");

                extentReports.CreateStepLogs("Info", "User " + valUser + " details are displayed. ");

                //Login user
                homePage.SearchUserByGlobalSearch(fileTMT0047476, valUser);
                usersLogin.LoginAsSelectedUser();;

                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateLog("User: " + valUser + " Switched to Lightning View ");
                }
                else
                {
                    extentReports.CreateStepLogs("Passed", "CF Financial User: " + valUser + " is able to login into lightning view. ");
                }

                homePageLV.ClickAppLauncher();
                homePageLV.SelectApp(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Info", appName + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                lvCompanyDetailsPage.SearchCompanyInLightning(CompanyNameExl);
                extentReports.CreateStepLogs("Info", " Company: " + CompanyNameExl + " found and selected ");
                extentReports.CreateStepLogs("Info", tabNameExl + " tab is available on " + CompanyNameExl + " Company Detail Page. ");
                lvCompanyDetailsPage.NavigateToAParticularTab("Activity");
                extentReports.CreateStepLogs("Info", " User navigated to Activity List page from " + CompanyNameExl + " :Company Detail Page. ");

                //Select the Activity
                activitiesList.ViewActivityFromList();
                extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");

                activityDetailPage.DeleteActivity();
                msgSaveActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                extentReports.CreateStepLogs("Info", msgSaveActivity);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                extentReports.CreateStepLogs("Info", valUser + " logged out ");
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
