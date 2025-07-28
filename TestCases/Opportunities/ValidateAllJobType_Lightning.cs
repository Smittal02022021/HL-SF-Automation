using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using SF_Automation.TestData;
using System;
using SF_Automation.Pages.Common;

namespace SF_Automation.TestCases.Opportunities
{
    class ValidateAllJobType_Lightning : BaseClass
    {
       ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        RandomPages pages = new RandomPages();


        public static string fileTC1197 = "LV_TC1197andTC1198AddOpportunityInOpportunityHome.xlsx";

       [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void JobType()
        {
            try
            {
                //Get Test Data file path                
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1197;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function           
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();             
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                //Validate all Job Types                 
                string jobTypes = pages.SelectJobType();
                Assert.AreEqual("Job Types", jobTypes);
                extentReports.CreateLog(jobTypes + " is displayed under HL Banker dropdown ");
                                
                pages.SelectListViewLV("All");
                //pages.GetJobTypes("CVAS - Tangible Asset Valuation");
                pages.GetJobTypes("Debt Advisory");
                pages.GetJobTypes("DRC - Exp Wit-Litigation");
                pages.GetJobTypes("FA - Portfolio-Auto Loans");
                pages.GetJobTypes("InSource");
                pages.GetJobTypes("Portfolio Acquisition");
                pages.GetJobTypes("Real Estate Brokerage");
                pages.GetJobTypes("Strategic Consulting");
                pages.GetJobTypes("Valuation Advisory");
                Assert.IsTrue(pages.ValidateJobTypesLV(), "Verified that displayed Job Types are same");
                extentReports.CreateLog("Displayed Job Types are as expected ");

                //Validate all Product Lines                
                
                pages.GetProductLines();                        
                Assert.IsTrue(pages.ValidateProductLines(), "Verified that displayed Product Lines are same");
                extentReports.CreateLog("Displayed Product Lines are as expected ");

                usersLogin.DiffLightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();
            }

            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.DiffLightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }        
    }
}
