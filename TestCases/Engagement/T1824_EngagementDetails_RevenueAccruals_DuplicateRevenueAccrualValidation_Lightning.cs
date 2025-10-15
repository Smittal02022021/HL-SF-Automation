using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Globalization;

namespace SF_Automation.TestCases.Engagement
{
    class T1824_EngagementDetails_RevenueAccruals_DuplicateRevenueAccrualValidation_Lightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        ValuationPeriods valuationPeriods = new ValuationPeriods();
        public static string fileTC1824 = "T1824_EngagementDetails_RevenueAccruals_DuplicateRevenueAccrualValidation";

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1824;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as CAO User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 2);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("CAO User: " + stdUser + " is able to login ");

                //Clicking on Engagement Tab and search for Engagement by entering Job type         
                string message =engHome.SearchEngagementWithNumberOnLightning("106347", "Discretionary Advisory");
                Assert.AreEqual("Project Aspire", message);
                extentReports.CreateLog("Records matching with selected Job Type are displayed ");

                //Validate title of Engagement Details page
                string titleEngDetails = engHome.ClickEngNumAndValidateThePage();
                Assert.AreEqual("Details", titleEngDetails);
                extentReports.CreateLog("Engagement Details page is displayed upon clicking Engagement number ");

                //Validate if revenue accural for current month exists
                engDetails.ValidateRevenueTab();
                string month = engDetails.GetMonthFromRevenueAccrualRecordL();
                Console.WriteLine(DateTime.Now.ToString("yyyy - M ", CultureInfo.InvariantCulture));
                Assert.AreEqual("2025 - 09", month);
                extentReports.CreateLog("Revenue Accrual record with : " + month + " exists ");

                //Get the value of Revenue Record Id
                 string ID = engDetails.GetRevenueRecordNumberL();
                 //extentReports.CreateLog("Revenue record number : " + ID + " is displayed ");
                 string extID = ID.Substring(76, 18);
                 Console.WriteLine("Id:", extID);

                //Click on Add Revenue Accrual button and try to create a new record     
                string errorMsg= engDetails.AddRevenueAccrualL();
                Console.WriteLine(errorMsg);
                Assert.AreEqual("duplicate value found: External_Id__c duplicates value on record with id: " +extID, errorMsg);
                extentReports.CreateLog(errorMsg + " is displayed ");

                usersLogin.LightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();

            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }       
    }
}
