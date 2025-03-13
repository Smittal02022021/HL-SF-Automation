using AventStack.ExtentReports.Gherkin.Model;
using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class TMTT0024858_27295_27302_71646_TMTT0024069_VerifyNewJobTypes_UpdatedonReportsObject : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTI0056871 = "TMTI0056871_VerifyNewJobTypes_UpdatedonReportsObject";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyNewFilterRecordOnReportsObject()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0056871;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Selecting Reports Tab
                string pageHeader = randomPages.ClickReportsTab();
                Assert.IsTrue(pageHeader.Contains("Reports & Dashboards"));
                extentReports.CreateLog("User is on " + pageHeader + " page ");

                int rowItems = ReadExcelData.GetRowCount(excelPath, "FilterRecord");

                for (int row = 2; row <= rowItems; row++)
                {
                    string valFilterRecord = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterRecord", row, 1);
                    string valReportOption = ReadExcelData.ReadDataMultipleRows(excelPath, "ReportOption", row, 1);
                    string valFilter = ReadExcelData.ReadDataMultipleRows(excelPath, "Filter", row, 1);

                    string reportPageHeader = randomPages.CreateNewReport(valReportOption);
                    Assert.IsTrue(reportPageHeader.Contains(valReportOption), "Verify User is on New Report Page ");
                    extentReports.CreateLog("User is on new " + valReportOption + " Report page ");

                    string fieldValue = randomPages.AddFilter(valFilter);
                    Assert.AreEqual(fieldValue, valFilter);
                    extentReports.CreateLog("Field: " + fieldValue + " is selected ");

                    //TMTI0027302_71646_56871, TMTI0027295 Verify the Industry Group is changed while creating Opportunities & Engagements Reports
                    //TMTI0071646 Verify the New Job Types are updated while creating the relevant reports from the Reports tab.
                    //TMTI0056871 Verify the New Job Types are updated while creating the relevant reports from Reports tab
                    //TMTI0055400 Verify the New Job Types are updated while creating the relevant reports from Reports tab
                    Assert.IsTrue(randomPages.IsRecordAvailableOnReportsPage(valFilterRecord), "Verify " + valFilterRecord + " is available in " + valFilter + " List");
                    extentReports.CreateLog(valFilter + ": " + valFilterRecord + " is avaialable in  List on Reports page ");
                    pageHeader = randomPages.CloseUnsavedReport();
                    Assert.IsTrue(pageHeader.Contains("Reports & Dashboards"));
                    extentReports.CreateLog("Report is closed and User is on " + pageHeader + " page ");
                }
                usersLogin.UserLogOut();
                driver.Quit();



            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
            }
        }
    }
}
