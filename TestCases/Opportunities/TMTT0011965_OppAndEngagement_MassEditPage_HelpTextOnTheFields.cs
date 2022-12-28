using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
namespace SF_Automation.TestCases.Opportunity
{
    class TMTT0011965_OppAndEngagement_MassEditPage_HelpTextOnTheFields : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        UsersLogin usersLogin = new UsersLogin();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTMTI0018624 = "TMTI0018624_FREngagementSummary_VerifyLendersAddedunderPreAndPostTransaction";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void FREngagementSummary_VerifyLenderAddedUnderPreAndPostTransaction()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0018624;
                Console.WriteLine(excelPath);

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
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");
                                
                //Search engagement with LOB - FR
                string message = engHome.SearchEngagementWithName(ReadExcelData.ReadData(excelPath, "Engagement", 2));
                Console.WriteLine(message);
                extentReports.CreateLog("Records matches to LOB-FR are found and Engagement Detail page is displayed ");
                               
                //Validate the title of page upon clicking Mass Edit Records button
                string titleAdditionalClientSubEng = engagementDetails.ClickMassEditRecordsButton();
                Assert.AreEqual("Additional Clients/Subjects", titleAdditionalClientSubEng);
                extentReports.CreateLog("Page with title : " + titleAdditionalClientSubEng + " is displayed upon clicking Mass Edit Records button ");

                //Validate the help text displayed on Client Holdings%
                string clientHoldingHelpText = clientSubjectsPage.GetClientHoldingsHelpText();
                Console.WriteLine("clientHoldingHelpText " + clientHoldingHelpText);
                Assert.AreEqual("If company-side mandate, input the % shareholding.\r\n\r\nSplit of clients' total debt holdings as at the point of HL's engagement(i.e. for each client = client debt holdings across all debt tranches / total holdings of clients across all debt tranches)", clientHoldingHelpText);
                extentReports.CreateLog("Help Text : " + clientHoldingHelpText + " is displayed on Client Holdings % ");




                usersLogin.UserLogOut();
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



