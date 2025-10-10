using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using SF_Automation.TestData;
using NUnit.Framework;
using System;
using Microsoft.Office.Interop.Excel;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_1_TMTT0047923_TMTT0047926_TMTT0047929_TMTT0024858_TMTT0030610_TMTT0024069_VerifyJobTypesGetsDispalyedOnAddingJobTypeFiltersWhileCreatingReports : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTT0047923 = "LV_3_TMTT0047923_TMTT0047926_TMTT0047929_VerifyJobTypesGetsDispalyedOnAddingJobTypeFiltersWhileCreatingReports";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0117815 Verify that the Job type "DRC -Dispute" gets dispalyed on adding Job type column in Filters while creating the reports.
        //TMTI0117852 Verify that the Job type "DRC -ESOP" gets displayed on adding Job type column in Filters while creating the reports.
        //TMTI0117864 Verify that the Job type "DRC - Estate & Gift" gets displayed on adding Job type column in Filters while creating the reports.
        //TMTI0071646 Verify the New Job Types are updated while creating the relevant reports from the Reports tab.
        //TMTI0056871 Verify the New Job Types are updated while creating the relevant reports from Reports tab
        //TMTI0055400 Verify the New Job Types are updated while creating the relevant reports from Reports tab


        [Test]
        public void VerifyJobTypesGetsDispalyedOnAddingJobTypeFiltersWhileCreatingReports()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0047923;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                string stdUserExl = ReadExcelData.ReadData(excelPath, "StandardUser", 1);
                homePage.SearchUserByGlobalSearchN(stdUserExl);
                extentReports.CreateStepLogs("Info", "User: " + stdUserExl + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(stdUserExl), true);
                extentReports.CreateLog("User: " + stdUserExl + " logged in on Lightning View");
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                //Loop for Report Type
                int rowReportType = ReadExcelData.GetRowCount(excelPath, "ReportType");
                for (int row = 2; row <= rowReportType; row++)
                {
                    string reportTypeExl= ReadExcelData.ReadDataMultipleRows(excelPath, "ReportType", row, 1);
                    randomPages.CreateNewReportLV(reportTypeExl);
                    extentReports.CreateStepLogs("Info", "Creating new Report of Type: "+ reportTypeExl);

                    //Loop for FilterType
                    int rowFilterType = ReadExcelData.GetRowCount(excelPath, "ReportFilter");
                    for (int rowFilter = 2; rowFilter <= rowFilterType; rowFilter++)
                    {
                        string reportFilterExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ReportFilter", rowFilter, 1);
                        randomPages.AddReportFilterLV(reportFilterExl);
                        extentReports.CreateStepLogs("Info", "Addig Filter: "+ reportFilterExl+" on Report Type: "+ reportTypeExl);

                        //Loop for FilterValue
                        //TMTI0117815 Verify that the Job type "DRC -Dispute" gets dispalyed on adding Job type column in Filters while creating the reports.
                        //TMTI0117852 Verify that the Job type "DRC -ESOP" gets displayed on adding Job type column in Filters while creating the reports.
                        //TMTI0117864 Verify that the Job type "DRC - Estate & Gift" gets displayed on adding Job type column in Filters while creating the reports.
                        
                        int rowFilterValue = ReadExcelData.GetRowCount(excelPath, "FilterValue");
                        for (int rowValue = 2; rowValue <= rowFilterValue; rowValue++)
                        {
                            string filterValueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterValue", rowValue, 1);
                            Assert.IsTrue(randomPages.IsFilterValueDisplayedLV(filterValueExl));
                            extentReports.CreateStepLogs("Passed", "Job Type: " + filterValueExl + " Displayed for Filter: " + reportFilterExl + " on Report Type: " + reportTypeExl);
                        }
                        randomPages.CancelFilterBySectionLV();
                        extentReports.CreateStepLogs("Info", "Filter By Section Closed");
                        randomPages.CloseActiveTab("Report Builder");
                    }
                }
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + stdUserExl + "Loggout ");
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully ");
            }
            catch (Exception e)
            {
                extentReports.CreateStepLogs("Failed", e.Message);
                driver.Quit();
            }


        }
    }
   
}
