using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Data;
using System.Reflection;

namespace SF_Automation.TestCases.Engagement
{
    class TMTT0046854_CFEngagementSummary_VerificationOfMarketingProcessDataSectionAndSubsections : BaseClass
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
        public void VerificationOfMarketingProcessDataSectionAndSubsections()
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
                string valJobType = ReadExcelData.ReadData(excelPath, "Engagement", 1);
                string message = engHome.SearchEngagementWithNumberOnLightning(ReadExcelData.ReadData(excelPath, "Engagement", 2), valJobType);
                Assert.AreEqual("Project Moon", message);
                extentReports.CreateLog("Records matching with selected Job Type are displayed ");

                //1.   TMTI0114576_ Verify the "Marketing Process Data" subsections of the CF Engagement Summary
                engagementDetails.ClickCFEngsummaryButtonL();
                string secMarketing = summaryPage.ValidateMarketingSection();                
                Assert.AreEqual("Marketing Process Data", secMarketing);
                extentReports.CreateLog("Section with name: " + secMarketing + " is displayed after clicking Marketing Process Data section ");
                              
                Assert.IsTrue(summaryPage.VerifySectionsUnderMarketingSection(), "Verify that displayed fields under Marketing Process Data section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields under Marketing Process Data section are as expected ");

                //2.  TMTI0114578_Verify the sub-sections are displayed with appropriate information under the Outreach Metrics section
                Assert.IsTrue(summaryPage.VerifyColumnssUnderMarketingSection(), "Verify that displayed Columns under Outreach Metrics section are same");
                extentReports.CreateStepLogs("Passed", "Displayed Columns under Outreach Metrics section are as expected ");

                Assert.IsTrue(summaryPage.VerifyRowssUnderMarketingSection(), "Verify that displayed rows under Outreach Metrics section are same");
                extentReports.CreateStepLogs("Passed", "Displayed rows under Outreach Metrics section are as expected ");

                Assert.IsTrue(summaryPage.VerifySubColumnsUnderMarketingSection(), "Verify that displayed sub columns under Outreach Metrics section are same");
                extentReports.CreateStepLogs("Passed", "Displayed sub columns under Outreach Metrics section are as expected ");

                string potCounterparty = summaryPage.GetOverallTotalOfPotentialCounterpartyContacted();
                string bidCounterparty = summaryPage.GetBidCounterparty();

                //3.   TMTI0114577_Verify that the sub-section Bid History is displayed with the appropriate information
                Assert.IsTrue(summaryPage.VerifyColumnsUnderBidHistory(), "Verify that displayed Columns under Bid History section are same");
                extentReports.CreateStepLogs("Passed", "Displayed Columns under Bid History section are as expected ");

                Assert.IsTrue(summaryPage.VerifySubColumnsUnderBidHistorySection(), "Verify that displayed sub columns under Bid History section are same");
                extentReports.CreateStepLogs("Passed", "Displayed sub columns under Bid History section are as expected ");

                string totalCounterparty = engagementDetails.GetInitialContactOfCounterparties();
                Assert.AreEqual(totalCounterparty, potCounterparty);
                extentReports.CreateLog("Potential Counterparty Contacts of Outreach Metrics section are same as Initial Contacts of Engagement's Counterparty ");

                string propsoedCounterparty = engagementDetails.Get1stProposedCounterparty();
                Assert.AreEqual(propsoedCounterparty, bidCounterparty);
                extentReports.CreateLog("Counterparty displayed in Bid History is same as it is displayed in Engagement Counterparties ");

                usersLogin.LightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();            
            }

            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.LightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}


