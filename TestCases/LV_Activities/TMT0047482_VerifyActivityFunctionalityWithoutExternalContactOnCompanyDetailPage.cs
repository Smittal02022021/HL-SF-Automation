using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;
using SF_Automation.Pages.Activities;

namespace SF_Automation.TestCases.LV_Activities
{
    class TMT0047482_VerifyActivityFunctionalityWithoutExternalContactOnCompanyDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage homePageLV = new LVHomePage();
        LV_CompanyDetailsPage lvCompanyDetailsPage = new LV_CompanyDetailsPage();
        LVCompaniesActivityDetailPage lvCompaniesActivityDetailPage = new LVCompaniesActivityDetailPage();

        LV_AddActivity addActivity = new LV_AddActivity();
        LV_ActivitiesList activitiesList = new LV_ActivitiesList();
        LV_ActivityDetailPage activityDetailPage = new LV_ActivityDetailPage();

        public static string fileTMT0047482 = "TMT0047482_VerifyActivityFunctionalityWithoutExternalContactOnCompanyDetailPage";

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
        public void ActivityFunctionalityWithoutExternalContact()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT0047482;
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login. ");

                //Search CF Financial User user by global search
                homePage.SearchUserByGlobalSearch(fileTMT0047482, valUser);
                usersLogin.LoginAsSelectedUser();
                login.SwitchToClassicView();

                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + stdUser + " is able to login into lightning view. ");

                login.SwitchToLightningExperience();
                extentReports.CreateLog("User: " + stdUser + " Switched to Lightning View ");
                homePageLV.ClickAppLauncher();
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectApp(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateLog(appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                //Search Company
                CompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1);
                lvCompanyDetailsPage.SearchCompanyInLightning(CompanyNameExl);
                extentReports.CreateStepLogs("Info", " Company: " + CompanyNameExl + " found and selected ");
               
                string tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 2);                
                extentReports.CreateStepLogs("Info", tabNameExl + " tab is available on " + CompanyNameExl + " Company Detail Page. ");
                lvCompanyDetailsPage.NavigateToAParticularTab("Activity");
                extentReports.CreateStepLogs("Info", " User navigated to Activity Detail page from " + CompanyNameExl + " :Company Detail Page. ");

                //TMT0047482 Verify that the user is able to Save the activity with "No External Contact" added to the activity.
                string subject = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 2);
                int beforeCount = activitiesList.GetActivityCount();

                lvCompanyDetailsPage.CreateNewActivityWithoutExternalContactFromCompanyDetailPage(fileTMT0047482);
                lvCompaniesActivityDetailPage.CloseTab("View Activity");
                Assert.IsTrue(activitiesList.VerifyCreatedActivityIsDisplayedUnderActivitiesList(beforeCount));
                extentReports.CreateStepLogs("Passed", "Activity created successfully without any external contact.");

                //Deleting Created Activity
                activitiesList.ViewActivityFromList(subject);
                extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");
                activityDetailPage.DeleteActivity();
                int afterCount = activitiesList.GetActivityCount();
                Assert.AreEqual(beforeCount, afterCount);
                extentReports.CreateStepLogs("Passed", "Activity deleted successfully. ");

                //Logout from SF Lightning View
                homePageLV.LogoutFromSFLightningAsApprover();
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
