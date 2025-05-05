using SF_Automation.Pages.Common;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;
using SF_Automation.Pages.HomePage;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_TMTT0024858_TMTT0030610_TMTT0035436_VerifyNewJobTypeIsForFVAOnlyLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTMTI0056883 = "LV_TMTI0056883_VerifyNewJobTypeIsForFVAOnly";
        string valJobType;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyNewJobTypeIsForFVAOnlyLV()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0056883;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Info", driver.Title + " is displayed ");               
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login ");
                
                //Login as Standard User profile and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                //TMTI0056883 Verify new job type for LOB other than FVA
                //TMTI0071649 Verify new job type - CVAS-IP Valuation for CF and FR LOB.
                //TMTI0084225 Verify new job type for LOB other than CF
                extentReports.CreateLog("Verify the JobTypes are available only for Opportunity LOB: FVA ");
                int rowRecordType = ReadExcelData.GetRowCount(excelPath, "RecordType");
                for (int row = 2; row <= rowRecordType; row++)
                {
                    //Call function to open Add Opportunity Page
                    string valRecordType= ReadExcelData.ReadDataMultipleRows(excelPath, "RecordType", row, 1);
                    valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 1);
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB: " + valRecordType);
                    extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");
                   
                    Assert.IsFalse(opportunityDetails.IsJobTypePresentInDropdownOppDetailPageLV(valJobType), " Verify " + valJobType + " is present not Present on Opportunity Detail Page for LOB: " + valRecordType + "under Job Type Dropdown ");
                    extentReports.CreateLog(" Job Type: " + valJobType + " is not Found for LOB: " + valRecordType);
                }
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "User: " + valUser + " logged out");
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                homePageLV.LogoutFromSFLightningAsApprover();
                driver.Quit();
            }
        }
    }
}