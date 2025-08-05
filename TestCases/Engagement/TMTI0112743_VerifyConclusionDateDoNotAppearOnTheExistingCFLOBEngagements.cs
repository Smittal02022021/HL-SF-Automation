using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Threading;

namespace SF_Automation.TestCases.Engagement
{
    class TMTI0112743_VerifyConclusionDateDoNotAppearOnTheExistingCFLOBEngagements : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_EngagementDetailsPage lvEngagementDetails = new LV_EngagementDetailsPage();
        
        public static string fileTMTI0112743 = "TMTI0112743_VerifyConclusionDateDoNotAppearOnTheExistingCFLOBEngagements";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyConclusionDateDoNotAppearOnTheExistingCFLOBEngagements()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0112743;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                //Validate user logged in
                Assert.AreEqual(driver.Url.Contains("lightning"), true);
                extentReports.CreateStepLogs("Passed", "Admin User is able to login into SF");

                int rowCount = ReadExcelData.GetRowCount(excelPath, "Engagements");

                for(int row = 2; row <= rowCount; row++)
                {
                    //Select HL Banker app
                    try
                    {
                        lvHomePage.SelectAppLV("HL Banker");
                    }
                    catch(Exception)
                    {
                        lvHomePage.SelectAppLV1("HL Banker");
                    }

                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    string engName = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagements", row, 1);

                    //Search Deal Team member user by global search
                    lvHomePage.SearchUserFromMainSearch(valUser);

                    //Verify searched user
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, valUser + " | Salesforce"), true);
                    extentReports.CreateLog("User " + valUser + " details are displayed ");

                    //Login as Deal Team member user
                    lvHomePage.UserLogin();

                    //Switch to lightning view
                    if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                    {
                        homePage.SwitchToLightningView();
                        extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                    }

                    //Validate user logged in
                    Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(valUser));
                    extentReports.CreateStepLogs("Passed", "Deal Team member: " + valUser + " is able to login into lightning view. ");

                    //Search the Engagement by global search
                    lvHomePage.SearchEngFromMainSearch(engName);
                    extentReports.CreateStepLogs("Info", "User navigated to Engagement detail page - " + engName);

                    //TC - End
                    lvHomePage.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Info", "Deal Team member Logged Out from SF Lightning View. ");
                }

                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.Quit();
            }
        }
    }
}
