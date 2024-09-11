using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Engagement
{
    class T2198_Engagement_PortfolioValuation_VerifyAbsenceOfPortfolioValuationButtonForNonPortfolioValuationEngagementRecordTypesWithinFASLOB_Lightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        ValuationPeriods valuationPeriods = new ValuationPeriods();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();

        public static string fileTC2198 = "T2198_PortfolioValuation_VerifyAbsenceOfPortfolioValuationButton";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyAbsenceOfPortfolioValuationButton()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2198;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "Engagement");

                for (int row = 2; row <= rowJobType; row++)
                {
                    //Login as Standard User and validate the user
                    string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                    usersLogin.SearchUserAndLogin(valUser);
                    string stdUser = login.ValidateUserLightning();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                    //Clicking on Engagement Tab and search for Engagement by entering Job type
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 1);
                    string valEng= ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 2);

                    string message = engHome.SearchEngagementWithNumberOnLightning(valEng, valJobType);
                    Assert.AreEqual(valEng, message);
                    extentReports.CreateLog("Records matching with selected Job Type are displayed ");

                    if (valJobType.Equals("FA - Portfolio-Valuation"))
                    {

                        //Validate title of Engagement Details page
                        string titleEngDetails = engHome.ClickEngNumAndValidateThePage();
                        Assert.AreEqual("Details", titleEngDetails);
                        extentReports.CreateLog("Engagement Details page is displayed upon clicking Engagement number ");

                        //Validate the visibility of Portfolio Valuation button
                        string portfolioValuation = opportunityDetails.ValidatePortfolioValuationButton();
                        string jobTypePV = engagementDetails.GetJobTypeL();
                        Assert.AreEqual("Portfolio Valuation button is displayed", portfolioValuation);
                        extentReports.CreateLog("Portfolio Valuation Button is displayed for the Engagement with Job type: " + jobTypePV + " ");
                    }

                    else
                    {
                        //Validate title of Engagement Details page     
                        string titleEngDetails1 = engHome.ClickEngNumAndValidateThePage();
                        Assert.AreEqual("Details", titleEngDetails1);
                        extentReports.CreateLog("Engagement Details page is displayed upon clicking Engagement number ");

                        //Validate the visibility of Portfolio Valuation button
                        string btnPV1 = opportunityDetails.ValidatePortfolioValuationButton();
                        string jobTypePV = engagementDetails.GetJobTypeL();
                        Assert.AreEqual("Portfolio Valuation button is not displayed", btnPV1);
                        extentReports.CreateLog("Portfolio Valuation Button is not displayed for the Engagement with Job type: " + jobTypePV + " ");
                    }
                    usersLogin.DiffLightningLogout();
                }               
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

