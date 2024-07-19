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
                extentReports.CreateStepLogs("Info", " User navigated to Activity Detail page from " + CompanyNameExl + " :Company Detail Page. ");

                /////////////////////////////////////////////////////
                //Crteating new Activity 
                /////////////////////////////////////////////////////
                addActivity.CreateNewActivityAdditionalHLAttandeeFromCompanyDetailPage(fileTMT0047476);
                lvCompaniesActivityDetailPage.CloseTab("View Activity");

                Assert.IsTrue(activitiesList.VerifyCreatedActivityIsDisplayedUnderActivitiesList());
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
                extentReports.CreateStepLogs("Info", " User navigated to Activity Detail page from " + CompanyNameExl + " :Company Detail Page. ");

                //TMT0047492 Verify that the "Private" is masked by the activity details in the list view and in the detail view for non-activity members.

                string actualMask;
                actualMask = lvCompanyDetailsPage.GetValueFromActivityList("Type");
                Assert.AreEqual("Private", actualMask, "Verify that Activity Type is Masked as Private for Non-Activity member ");
                extentReports.CreateStepLogs("Pass", "Activity Type is Masked as " + actualMask + " for Non-Activity member ");

                actualMask = lvCompanyDetailsPage.GetValueFromActivityList("Subject");
                Assert.AreEqual("Private", actualMask, "Verify that Activity Subject is Masked as Private for Non-Activity member ");
                extentReports.CreateStepLogs("Pass", "Activity Subject is Masked as " + actualMask + " for Non-Activity member ");

                actualMask = lvCompanyDetailsPage.GetValueFromActivityList("Description");
                Assert.AreEqual("Private", actualMask, "Verify that Activity Description is Masked as Private for Non-Activity member ");
                extentReports.CreateStepLogs("Pass", "Activity Description is Masked as " + actualMask + " for Non-Activity member ");

                actualMask = lvCompanyDetailsPage.GetValueFromActivityList("Meeting/Call Notes");
                Assert.AreEqual("Private", actualMask, "Verify that Activity Meeting/Call Notes is Masked as Private for Non-Activity member ");
                extentReports.CreateStepLogs("Pass", "Activity Meeting/Call Notes is Masked as " + actualMask + " for Non-Activity member ");

                //Select the Activity
                lvCompanyDetailsPage.ViewActivity();
                extentReports.CreateStepLogs("Info", "Non-Activity User clicked on View option from Activities List and redirected Activity Detail Page ");
                msgSaveActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                msgSaveActivityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SaveActivityPopUpMsg", 2, 4);
                Assert.AreEqual(msgSaveActivityExl, msgSaveActivity, "Verify Non - Activity member is not able to access Private activity ");
                extentReports.CreateStepLogs("Passed", "Message: " + msgSaveActivity + "is Displayed while view Private Activity by Non-Activity member");

                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                extentReports.CreateLog("User: " + nonActivityUser + " logged out ");

                string valUser1 = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                extentReports.CreateStepLogs("Info", "User " + valUser1 + " details are displayed. ");

                //Login user
                homePage.SearchUserByGlobalSearch(fileTMT0047476, valUser1);
                usersLogin.LoginAsSelectedUser();;

                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateLog("User: " + valUser1 + " Switched to Lightning View ");
                }
                else
                {
                    extentReports.CreateStepLogs("Passed", "CF Financial User: " + valUser1 + " is able to login into lightning view. ");
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
                extentReports.CreateStepLogs("Info", " User navigated to Activity Detail page from " + CompanyNameExl + " :Company Detail Page. ");

                lvCompanyDetailsPage.DeleteActivity();
                msgSaveActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                extentReports.CreateStepLogs("Info", msgSaveActivity);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                extentReports.CreateStepLogs("Info", valUser1 + " logged out ");

            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
            }
        }
        /*
        [TearDown]
        public void TearDown()
        {
            //companyhome.SearchCompany(CompanyNameExl);
            companyDetail.DeleteCompany(CompanyNameExl);
            Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
            extentReports.CreateStepLogs("Info", "Created company is deleted successfully ");
        }*/
    }
}
