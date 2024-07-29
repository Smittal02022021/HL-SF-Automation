using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.Activities;

namespace SF_Automation.TestCases.LV_Activities
{
    class TMT0047484_47893_47895_47897_VerifyPrimaryAttendeeSelection : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage homePageLV = new LVHomePage();
        LV_CompanyDetailsPage lvCompanyDetailsPage = new LV_CompanyDetailsPage();
        LVCompaniesActivityDetailPage lvCompaniesActivityDetailPage = new LVCompaniesActivityDetailPage();
        RandomPages randomPages = new RandomPages();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();

        LV_AddActivity addActivity = new LV_AddActivity();
        LV_ActivitiesList activitiesList = new LV_ActivitiesList();
        LV_ActivityDetailPage activityDetailPage = new LV_ActivityDetailPage();

        public static string fileTMT0047484 = "TMT0047484_VerifyPrimaryAttendeeSelection";
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
        public void VerifyPrimaryAttendeeSelection()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT0047484;
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
                homePage.SearchUserByGlobalSearch(fileTMT0047484, valUser);
                usersLogin.LoginAsSelectedUser();
                login.SwitchToClassicView();

                string cfFinancialUser = login.ValidateUser();
                Assert.AreEqual(cfFinancialUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + cfFinancialUser + " is able to login into lightning view. ");

                login.SwitchToLightningExperience();
                extentReports.CreateLog("User: " + cfFinancialUser + " Switched to Lightning View ");
                homePageLV.ClickAppLauncher();
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectApp(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Info", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                
                //Search Company
                CompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1);
                lvCompanyDetailsPage.SearchCompanyInLightning(CompanyNameExl);
                extentReports.CreateStepLogs("Info", " Company: " + CompanyNameExl + " found and selected ");

                string tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 2);
                extentReports.CreateStepLogs("Info", tabNameExl + " tab is available on " + CompanyNameExl + " Company Detail Page. ");
                lvCompanyDetailsPage.NavigateToAParticularTab("Activity");
                extentReports.CreateStepLogs("Info", " User navigated to Activity Detail page from " + CompanyNameExl + " :Company Detail Page. ");

                /////////////////////////////////////////////////////
                //Crteating new Activity with additional HL Attandee
                /////////////////////////////////////////////////////
                string subject = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 2);
                int beforeCount = activitiesList.GetActivityCount();

                lvCompanyDetailsPage.CreateNewActivityAdditionalHLAttandeeFromCompanyDetailPage(fileTMT0047484);
                lvCompaniesActivityDetailPage.CloseTab("View Activity");
                Assert.IsTrue(activitiesList.VerifyCreatedActivityIsDisplayedUnderActivitiesList(beforeCount));
                extentReports.CreateStepLogs("Passed", "Activity created successfully with additional HL Attandee.");

                //TMT0047484 Verify that if the non-primary attendee removed the primary attendee while editing the activity, the next available activity attendee will become the primary attendee by default.

                string addHLAttandee = ReadExcelData.ReadData(excelPath, "Activity", 8);
                activitiesList.ViewActivityFromList(subject);
                string primayContact =lvCompaniesActivityDetailPage.GetPrimaryHlAttandeeHLAttandee();
                extentReports.CreateStepLogs("Info", "Default Primay Contact: "+ primayContact);
                lvCompaniesActivityDetailPage.RemovePrimaryContact(primayContact);
                extentReports.CreateStepLogs("Info", "Primary Contact: " + primayContact+" is removed ");
                
                primayContact = lvCompaniesActivityDetailPage.GetPrimaryHlAttandeeHLAttandee();
                Assert.AreEqual(addHLAttandee, primayContact);
                extentReports.CreateStepLogs("Passed", "New Primary Contact after removing default : " + primayContact);

                lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Save");
                //Get popup message
                msgSaveActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                msgSaveActivityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SaveActivityPopUpMsg", 2, 1);
                Assert.AreEqual(msgSaveActivityExl, msgSaveActivity);
                extentReports.CreateStepLogs("Passed", "Message: " + msgSaveActivity + " is Displayed for Required fields ");

                //TMT0047893 Verify that clicking the "View All" link will take the user to a new tab where all the activities are listed on one page.
                
                lvCompanyDetailsPage.ClickViewAllActivities();
                Assert.IsTrue(lvCompanyDetailsPage.IsCompanyActivityListNewTabDispayed(),"Verify View All link is redirects to new tab ");
                extentReports.CreateStepLogs("Passed", "View All link is redirects to Company Activity new tab ");

                //TMT0047895 Verify the availability of the Search bar on the "View All" link of the company activity list tab.
                Assert.IsTrue(lvCompanyDetailsPage.IsSearchBoxDisplayed(), "Verify Searchbox is displayed on Company Activity new tab ");
                extentReports.CreateStepLogs("Passed", "Searchbox is displayed on Company Activity new tab ");

                //TMT0047897 Verify that the application will filter out activities as per the entered keyword in the search bar.
                string activity = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 1);
                bool IsResultFound= lvCompanyDetailsPage.SearchAcvtivity(activity);
                Assert.IsTrue(IsResultFound, "Verify application will filter out activities as per the entered keyword in the search bar ");
                extentReports.CreateStepLogs("Passed", "Application filtered out activities as per the entered keyword in the search bar ");

                randomPages.CloseActiveTab("Company Activity");
                extentReports.CreateStepLogs("Info", "Company Activity tab is closed ");

                lvCompanyDetailsPage.DeleteActivity();
                msgSaveActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                extentReports.CreateStepLogs("Info", msgSaveActivity);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
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
        }
        */
    }
}
