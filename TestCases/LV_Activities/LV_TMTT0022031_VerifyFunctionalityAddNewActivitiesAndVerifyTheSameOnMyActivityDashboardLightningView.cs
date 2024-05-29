using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SalesForce_Project.TestCases.LV_Activities
{
    class LV_TMTT0022031_VerifyFunctionalityAddNewActivitiesAndVerifyTheSameOnMyActivityDashboardLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage homePageLV = new LVHomePage();
        LV_CompanyDetailsPage lvCompanyDetailsPage = new LV_CompanyDetailsPage();
        LVCompaniesActivityDetailPage lvCompaniesActivityDetailPage = new LVCompaniesActivityDetailPage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();

        public static string fileTMTT0022031 = "LV_TMTT0022031_VerifyFunctionalityAddNewActivitiesAndVerifyTheSameOnMyActivityDashboard";
        string msgActivity;
        string CompanyNameExl;
        string msgSaveActivity;
        string msgSaveActivityExl;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyFunctionalityAddNewActivitiesAndVerifyTheSameOnMyActivityDashboardLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0022031;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");
                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login. ");

                //Search CF Financial User by global search
                //Login user
                string userExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(userExl);
                login.SwitchToLightningExperience();
                string userName = login.ValidateUserLightningView();
                Assert.AreEqual(userName.Contains(userExl), true);
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + userExl + " logged in on Lightning View");
                homePageLV.ClickAppLauncher();
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectApp(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateLog(appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateLog("User is on " + moduleNameExl + " Page ");
                extentReports.CreateStepLogs("Info", "User is navigated to Homepage tab under Home option from HL Banker dropdown. ");

                CompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1);
                lvCompanyDetailsPage.SearchCompanyInLightning(CompanyNameExl);
                extentReports.CreateStepLogs("Info", " Company: " + CompanyNameExl + " found and selected ");

                string tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 2);
                Assert.IsTrue(lvCompanyDetailsPage.IsTabAvailable(tabNameExl));
                extentReports.CreateStepLogs("Passed", tabNameExl + " tab is available on " + CompanyNameExl + " Company Detail Page. ");

                extentReports.CreateStepLogs("Passed", " User navigating to " + tabNameExl+" tab");

                lvCompanyDetailsPage.NavigateToAParticularTab(tabNameExl);

                string type = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 1);
                string subject = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 2);
                string industryGroup = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 3);
                string productType = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 4);
                string description = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 5);
                string meetingNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 6);
                string extAttendee = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 7);

                lvCompanyDetailsPage.CreateNewActivityFromCompanyDetailPageLV(type, subject, industryGroup, productType, description, meetingNotes, extAttendee);
                msgSaveActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                msgSaveActivityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SaveActivityPopUpMsg", 2, 2);
                Assert.AreEqual(msgSaveActivityExl, msgSaveActivity);
                extentReports.CreateStepLogs("Passed", "Message: " + msgSaveActivity + "is Displayed for Activity Type: " + type + " ");
                Assert.IsTrue(lvCompaniesActivityDetailPage.IsActivityListDisplayedLV(), "Verify user redirects to list view on clicking Cancel button of Add New Activity page ");
                extentReports.CreateStepLogs("Passed", "User redirected to Activity list view on clicking Cancel button from Add New Activity page ");
                lvCompanyDetailsPage.RefreshActivitiesList();
                    


            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
            }

        }
    }
}
