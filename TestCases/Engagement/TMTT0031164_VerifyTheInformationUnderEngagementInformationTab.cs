using AventStack.ExtentReports.Gherkin.Model;
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
    class TMTT0031164_VerifyTheInformationUnderEngagementInformationTab : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        public static string fileTMTT0031164 = "TMTT0024518_VerifyTheFunctionalityOfFREngagementSummaryInSFLightning";

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
                int rowJobType = ReadExcelData.GetRowCount(excelPath, "Engagement");
                Console.WriteLine("rowCount " + rowJobType);
                string JobType = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", 2, 1);
                engHome.ValidateSearchFunctionalityOfEngagementsByJobType(JobType);

                //TMTI0072768_Click FR Engagement Summary button and validate the displayed tab
                string value = engagementDetails.ClickFREngSummaryButtonL();
                 Assert.AreEqual("Engagement Info", value);
                 extentReports.CreateLog("Tab with name: " + value + " is displayed upon clicking Engagement Summary (FR) button ");

                //TMTI0072770_Validate all fields of Engagement Info tab                
                string lblTxnType = summaryPage.ValidateFieldTransactionTypeL();
                Assert.AreEqual("Transaction Type", lblTxnType);
                extentReports.CreateLog("Field with name: " + lblTxnType + " is displayed on " + value + " ");

                string lblTxnStatus = summaryPage.ValidateFieldPostTransactionStatusL();
                Assert.AreEqual("Post Transaction Status", lblTxnStatus);
                extentReports.CreateLog("Field with name: " + lblTxnStatus + " is displayed on " + value + " ");

                string lblCompDesc = summaryPage.ValidateFieldCompDescL();
                Assert.AreEqual("Company Description", lblCompDesc);
                extentReports.CreateLog("Field with name: " + lblCompDesc + " is displayed on " + value + " ");

                string lblClientDesc = summaryPage.ValidateFieldClientDescL();
                Assert.AreEqual("Client Description", lblClientDesc);
                extentReports.CreateLog("Field with name: " + lblClientDesc + " is displayed on " + value + " ");

                string lblReTxnDesc = summaryPage.ValidateFieldRestructuringTxnDescL();
                Assert.AreEqual("Restructuring Transaction Description", lblReTxnDesc);
                extentReports.CreateLog("Field with name: " + lblReTxnDesc + " is displayed on " + value + " ");



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


