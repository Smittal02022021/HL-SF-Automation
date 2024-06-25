using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_TMTT0024858_TMTT0030610_TMTT0012824_TMTT0024069_TMTT0035436_VerifyNewJobTypeOnJobTypeObjPageLightningView: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();

        //System Admin: Verify New Job Type is present on Job Type Object page
        //TMTI0056876 Verify New / Updated Job Type And Job Code Under Job Type Object/tab
        //TMTI0071641 Verify New / Updated Job type and Job code under Job Type Object/tab.
        //TMTI0028200 Verify New / updated Job type and Job code under Job type Object/tab
        //TMTI0055398 Verify New/updated Job type and code under Job type Object/tab
        //TMTI0084213 Verify New/updated Job type and code under Job type Object/tab

        public static string fileTMTT0024858 = "LV_TMTT0024858_VerifyNewJobTypeOnJobTypeObjPageLightningView";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            InitializeZoom70();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyNewJobTypeOnJobTypesObjectPageLV()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0024858;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(adminUserExl);
                extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string userName = login.ValidateUserLightningView();
                Assert.AreEqual(userName.Contains(adminUserExl), true);
                extentReports.CreateStepLogs("Passed", "System Administrator User: " + adminUserExl + " logged in on Lightning View");
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadData(excelPath, "ModuleName", 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on Module: " + moduleNameExl + " Page ");

                //Calling functions to validate for all LOBs operation
                int rowJobType = ReadExcelData.GetRowCount(excelPath, "JobType");
                for (int row = 2; row <= rowJobType; row++)
                {
                    extentReports.CreateStepLogs("Info", "Verify New Job Type is present on Job Type Object page as System Administrator ");
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 1);
                    string pageTitle = randomPages.SelectJobTypesLV(valJobType);
                    Assert.AreEqual(valJobType, pageTitle);
                    extentReports.CreateStepLogs("Passed", "Page with title: " + pageTitle + " is displayed upon clicking Job Types link ");
                    string expectedJobCode= ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 2);
                    string actualJobCode = randomPages.GetJobCodeLV();
                    Assert.AreEqual(expectedJobCode, actualJobCode);
                    extentReports.CreateStepLogs("Passed", "Job Type: " + valJobType + " with Job Code:  " + actualJobCode+" is available");
                    randomPages.CloseActiveTab(valJobType);
                }
                homePageLV.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Pass", "System Administrator: " + adminUserExl + " logged out ");
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
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