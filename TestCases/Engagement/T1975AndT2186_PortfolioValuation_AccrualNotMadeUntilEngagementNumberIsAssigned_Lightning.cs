
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Engagement
{
    class T1975AndT2186_PortfolioValuation_AccrualNotMadeUntilEngagementNumberIsAssigned_Lightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        ValuationPeriods valuationPeriods = new ValuationPeriods();
        public static string fileTC2186 = "T2186_PortfolioValuation_AccrualNotMadeUntilEngagementNumberIsAssigned";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void AccrualNotMadeUntilEngagementNumberIsAssigned()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2186;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

               //Clicking on Engagement Tab and search for Engagement by entering Job type         
                string message = engHome.SearchEngagementWithName(ReadExcelData.ReadData(excelPath, "Engagement", 2));
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matching with selected Job Type are displayed ");

                //Validate title of Engagement Details page
                string title = engagementDetails.GetTitle();
                Assert.AreEqual("Engagement", title);
                extentReports.CreateLog("Page with title: " + title + " is displayed ");

                //Get Engagement Name from Engagement
                string engName = engagementDetails.GetEngName();

                //Function to clear Engagement number and save it
                string engNum = engagementDetails.ClearEngNumberAndSave();
                Assert.AreEqual(" ", engNum);
                extentReports.CreateLog("Engagement number is removed successfuly ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();                
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                //Search for same Engagement
                engHome.SearchEngagementWithNumberOnLightning(ReadExcelData.ReadData(excelPath, "Engagement", 2), ReadExcelData.ReadData(excelPath, "Engagement", 1));

                //Click on Portfolio Valuation,click Engagement Valuation Period and validate Engagement Valuation Period page
                engagementDetails.ClickPortfolioValuationL();
                string titleValPeriod = valuationPeriods.ClickEngValuationPeriodL();
                Assert.AreEqual("New Engagement Valuation Period", titleValPeriod);
                extentReports.CreateLog("Page with title: " + titleValPeriod + " is displayed upon clicking New Engagement Valuation Period button ");

                //Enter Engagement Valuation Period details, save it and Validate Engagement Valuation Period Detail page
                valuationPeriods.EnterAndSaveEngValuationPeriodDetailsL(engName);
                string titleValPeriodDetail = valuationPeriods.GetEngValPeriodDetailTitle();
                Assert.AreEqual("Engagement Valuation Period Detail", titleValPeriodDetail);
                extentReports.CreateLog("Page with title: " + titleValPeriodDetail + " is displayed upon saving Engagement Valuation Period details ");

                //Enter Eng Valuation Period Position details and validate entered Valuation Period Position
                valuationPeriods.ValidateMessageWhileClickingSaveButtonOnPeriodPosition();
                string valPeriodPosition = valuationPeriods.EnterAndSaveEngValuationPeriodPositionDetailsL(ReadExcelData.ReadData(excelPath, "ValuationPeriod",4));
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 4), valPeriodPosition);
                extentReports.CreateLog("Engagement Valuation Period Position with name: " + valPeriodPosition + " is added successfully ");

                //Click on added Period Position             
                valuationPeriods.ClickPositionAndSaveTeamMembers();
                extentReports.CreateLog("Team members details are saved ");

                //Update Status to Completed and validate error messages
                string message1 = valuationPeriods.UpdateStatusAndSaveL();
                extentReports.CreateLog("Status is updated to Completed, Generate Accrual ");
                Assert.AreEqual("The accrual should not be made until the engagement number is assigned. FVA rule.", message1);
                extentReports.CreateLog("1st error message: " + message1 + " is displayed upon accepting alert ");

                string message2 = valuationPeriods.GetErrorMessage();
                Assert.AreEqual("Please update the stage to generate a revenue accrual.", message2);
                extentReports.CreateLog("2nd error message: " + message2 + " is displayed upon accepting alert ");

                usersLogin.DiffLightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }       
    }
}

