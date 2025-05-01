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
    class TMTT0046849_VerificationOfEngagementBasicInformationOnEngagementSummaryPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        CFEngagementSummaryPage summaryPage = new CFEngagementSummaryPage();
        public static string fileTMTT0031164 = "TMTT0046849_VerificationOfEngagementBasicInformation";

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0031164;
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
                string message=  engHome.SearchEngagementWithNumberOnLightning(ReadExcelData.ReadData(excelPath, "Engagement", 2), ReadExcelData.ReadData(excelPath, "Engagement", 1));
                Assert.AreEqual("Project Moon", message);
                extentReports.CreateLog("Records matching with selected Job Type are displayed ");

                //1. TMTI0114536_Verify the "Line Of Business" label and mapping value is displayed.
                string LOB = engagementDetails.ValidateLOBOnHeader();
                string lobValue= engagementDetails.ValidateLOBValueOnHeader();
                Assert.AreEqual("Line of Business", LOB);
                Assert.AreEqual("CF", lobValue);
                extentReports.CreateLog("The label "+LOB +" and mapping value " + lobValue + " is displayed.");

                //2. TMTI0114535_Verify that the label External Disclosure Status and mapping value are displayed.
                string discStatus = engagementDetails.GetExtDisclosureStatus();
                string discStatusValue = engagementDetails.GetDisclosureStatusValue();

                engagementDetails.ClickCFEngsummaryButtonL();
                string status = summaryPage.ValidateDiscStatusOnHeader();
                string statusValue = summaryPage.ValidateDiscStatusValueOnHeader();
                Assert.AreEqual(discStatus, status);
                Assert.AreEqual(discStatusValue, statusValue);
                extentReports.CreateLog("The label " + status + " and mapping value " + statusValue + " is displayed as displayed in Engagement details page. ");

                //3. TMTI0114537_Verify that the "Subject" label and mapping value are displayed.
                 


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


