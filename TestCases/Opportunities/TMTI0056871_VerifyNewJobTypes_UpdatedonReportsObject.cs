using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunity
{
    class TMTI0056871_VerifyNewJobTypes_UpdatedonReportsObject : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTI0056877 = "TMTI0056871_VerifyNewJobTypes_UpdatedonReportsObject";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyNewJobTypeOnOppEngManagerPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0056877;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "JobType");

                for (int row = 2; row <= rowJobType; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 1);
                    string valReportOption = ReadExcelData.ReadDataMultipleRows(excelPath, "ReportOption", row, 1);
                    string valFilter = ReadExcelData.ReadDataMultipleRows(excelPath, "Filter", row, 1);

                    string pageHeader = randomPages.ClickReportsTab();
                    Assert.IsTrue(pageHeader.Contains("Reports & Dashboards"));
                    extentReports.CreateLog("User is on " + pageHeader + " page ");

                    string reportPageHeader = randomPages.CreateNewReport(valReportOption);
                    Assert.IsTrue(reportPageHeader.Contains(valReportOption), "Verify User is on New Report Page ");
                    extentReports.CreateLog("User is on new " + valReportOption + " Report page ");

                    string fieldValue = randomPages.AddFilter(valFilter);
                    Assert.AreEqual(fieldValue, valFilter);
                    extentReports.CreateLog("Field: " + fieldValue + " is selected ");

                    Assert.IsTrue(randomPages.IsJobTypeVailableOnReportsPage(valJobType), "Verify New Job Type is available in Job Type List");
                    extentReports.CreateLog("Job Type " + valJobType + " is avaialable in Job Type List on Reports page ");
                    pageHeader = randomPages.CloseUnsavedReport();
                    Assert.IsTrue(pageHeader.Contains("Reports & Dashboards"));
                    extentReports.CreateLog("Report is closed and User is on " + pageHeader + " page ");
                }
                usersLogin.UserLogOut();
                extentReports.CreateLog("User logged out ");
                driver.Quit();
                extentReports.CreateLog("Browser Closed ");
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
            }
        }
    }
}
