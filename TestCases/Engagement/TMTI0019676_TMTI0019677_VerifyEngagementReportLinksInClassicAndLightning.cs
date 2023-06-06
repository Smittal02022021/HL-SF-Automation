using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Engagement

{
    class TMTI0019676_TMTI0019677_VerifyEngagementReportLinksInClassicAndLightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();    
        UsersLogin usersLogin = new UsersLogin();        
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engHome = new EngagementHomePage();

        public static string TMTI0019676 = "TMTT0017889_CommentsAndContactsMappingToEngUponConversionFromOpportunity.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyEngagementReportLinks()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + TMTI0019676;
                string excelPath1 = ReadJSONData.data.filePaths.testData;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");   

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                 Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                 extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                               
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);

                //Login as Financial User and validate the user                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Open the selected Engagement
                string searchedEng = engHome.ValidateSearchFunctionalityOfEngagements("106347");
                engHome.ClickEngNumber();

                //Validate Report tab
                string report= engagementDetails.ValidateReportTab();
                Assert.AreEqual("Report", report);
                extentReports.CreateLog("Tab: " + report + " is displayed under More tab on Engagement details page ");

                //Validate Engagement AR Receipt report
                string titleEngAR = engagementDetails.ValidateEngARReceiptReport();
                Assert.AreEqual("Engagement AR Receipt", titleEngAR);
                extentReports.CreateLog("Page with title: " + titleEngAR + " is displayed upon clicking Engagement AR Receipt report link ");

                //Validate Engagement Expenses report
                string titleEngExp= engagementDetails.ValidateEngExpReport();
                Assert.AreEqual("Engagement Expenses", titleEngExp);
                extentReports.CreateLog("Page with title: " + titleEngExp + " is displayed upon clicking Engagement Expenses report link ");

                //Validate Engagement Invoice Details report
                string titleEngInvoice = engagementDetails.ValidateEngInvoiceReport();
                Assert.AreEqual("Engagement Invoice Details", titleEngInvoice);
                extentReports.CreateLog("Page with title: " + titleEngInvoice + " is displayed upon clicking Engagement Invoice Details link ");

                //Logout of the user and click on Switch To Lightning Experience link
                usersLogin.LightningLogout();
                usersLogin.SearchUserAndLogin("Emre Abale");
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains("Emre Abale"), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Open the selected Engagement
                engHome.SearchEngagementWithNumber("106347");                

                //Validate Engagement Report page
                string reportAdmin = engagementDetails.ValidateEngReportButton();
                Assert.AreEqual("Report", reportAdmin);
                extentReports.CreateLog("Page: " + reportAdmin + " is displayed after clicking the Engagement Report button ");

                //Validate Engagement Working Group list
                string titleEngWorkingClassic = engagementDetails.ValidateEngWorkingGroupListReport();
                Assert.AreEqual("Working Group List", titleEngWorkingClassic);
                extentReports.CreateLog("Page with title: " + titleEngWorkingClassic + " is displayed upon clicking Engagement Working Group list link ");

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

    

