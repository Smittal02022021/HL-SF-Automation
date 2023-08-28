using AventStack.ExtentReports.Gherkin.Model;
using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Reflection;

namespace SF_Automation.TestCases.Engagement
{
    class TMTT0031167_VerifyTheFieldsAndFunctionalityOfFinancialsProjectionsTab : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        public static string fileTMTT0031167 = "TMTT0024518_VerifyTheFunctionalityOfFREngagementSummaryInSFLightning";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTheInformationUnderEngagementInformationTab()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0031167;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Financial User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);                               
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateFRUserLightning();
                Console.WriteLine("stdUser: " + stdUser);
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");
                               
                //Search for the required engagement           
                int rowJobType = ReadExcelData.GetRowCount(excelPath, "Engagement");
                Console.WriteLine("rowCount " + rowJobType);
                string JobType = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", 2, 1);
                engHome.ValidateSearchFunctionalityOfEngagementsByJobType(JobType);

                //Get the value of Currency
                string txnType = engagementDetails.GetCurrencyL();

                //TMTI0072810_Validate the Financials and Projections tab
                string value = engagementDetails.ClickFREngSummaryButtonL();
                string financials = engagementDetails.ValidateFinancialsProjectionsTabL();
                 Assert.AreEqual("Financials / Projections", financials);
                 extentReports.CreateLog("Tab with name: " + financials + " is displayed on FR Engagement Summary ");

                //TMTI0072812_Validate all fields of Financials and Projections tab                
                string lblCurrency = summaryPage.ValidateFieldCurrencyFinancialsAreReportedInL();
                Assert.AreEqual("Currency Financials are reported in", lblCurrency);
                extentReports.CreateLog("Field with name: " + lblCurrency + " is displayed on " + financials + " ");

                string lblProjections = summaryPage.ValidateFieldProjectionsAreAsOfL();
                Assert.AreEqual("Projections are as of:", lblProjections);
                extentReports.CreateLog("Field with name: " + lblProjections + " is displayed on " + financials + " ");

                string lblLTMProjections = summaryPage.ValidateFieldLTMProjectionsAreAsOfL();
                Assert.AreEqual("LTM Projections are as of:", lblLTMProjections);
                extentReports.CreateLog("Field with name: " + lblLTMProjections + " is displayed on " + financials + " ");

                Assert.IsTrue(summaryPage.VerifyRevenueFieldsL(), "Verified that displayed Revenue fields are same");
                extentReports.CreateLog("Revenue fields are displayed as expected ");

                Assert.IsTrue(summaryPage.VerifyEBITDAFieldsL(), "Verified that displayed EBITDA fields are same");
                extentReports.CreateLog("EBITDA fields are displayed as expected ");

                Assert.IsTrue(summaryPage.VerifyCapexFieldsL(), "Verified that displayed Capex fields are same");
                extentReports.CreateLog("Capex fields are displayed as expected ");

                //TMTI0072814 -Verify that the Revenue fields are all editable on the screen
                string editFields = summaryPage.ValidateIfRevenueFieldsAreEditable();
                Assert.AreEqual("True", editFields);
                extentReports.CreateLog("Revenue Fields are editable on Financials and Projections tab ");

                //TMTI0072816 -Verify that the EBITDA fields are all editable on the screen
                string editFieldsEBITDA = summaryPage.ValidateIfEBITDAFieldsAreEditable();
                Assert.AreEqual("True", editFieldsEBITDA);
                extentReports.CreateLog("EBITDA Fields are editable on Financials and Projections tab ");

                //TMTI0072818 -Verify that the Capex fields are all editable on the screen
                string editFieldsCapex = summaryPage.ValidateIfCapexFieldsAreEditable();
                Assert.AreEqual("True", editFieldsCapex);
                extentReports.CreateLog("Capex Fields are editable on Financials and Projections tab ");

                //TMTI0072820 -Verify that the Capex fields are all editable on the screen
                //string editFieldsCapex = summaryPage.ValidateIfCapexFieldsAreEditable();
                //Assert.AreEqual("True", editFieldsCapex);
                //extentReports.CreateLog("Capex Fields are editable on Financials and Projections tab ");

                usersLogin.LightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();
            }

            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}


