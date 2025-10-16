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
    class TMTT0046852_CFEngagementSumamry_VerificationOfTimelineSectionAndSubsections : BaseClass
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
                string valJobType = ReadExcelData.ReadData(excelPath, "Engagement", 1);
                string message = engHome.SearchEngagementWithNumberOnLightning(ReadExcelData.ReadData(excelPath, "Engagement", 2), valJobType);
                Assert.AreEqual("Project Moon", message);
                extentReports.CreateLog("Records matching with selected Job Type are displayed ");

                engagementDetails.ValidateFeesTab();
                string valCurrency = engagementDetails.GetCurrencyL();
                string finalCurrency = valCurrency.Substring(0, 3);

                //1.  TMTI0114562_Verify the sub-sections "Bidding, Signing and Closing" present under the "Engagement Timeline" section
                engagementDetails.ClickCFEngsummaryButtonL();
                string secParties = summaryPage.ValidateEngTimelineSection();           
                
                Assert.IsTrue(summaryPage.VerifySubSectionsOfTimeline(), "Verify that displayed sub sections under Engagement Timeline section are same");
                extentReports.CreateStepLogs("Passed", "Displayed sub sections under Engagement Timeline section are as expected ");

                //--Verify the fields of Bidding section
                Assert.IsTrue(summaryPage.VerifyFieldsOfBidding(), "Verify that displayed fields of Bidding section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields of Bidding section are as expected ");

                string dateEngagedMessage = summaryPage.ValidateDateEngagedMessageOnHeader();                
                Assert.AreEqual("Date on Engagement Letter", dateEngagedMessage);
                extentReports.CreateLog("Tool tip Message " + dateEngagedMessage + " is displayed on Date Engaged field in Bidding section ");

                //---Validate Edit Functionality
                string editValue = summaryPage.ValidateEditFunctionalityOfBidding("27-Jun-2023");
                Console.WriteLine("EditValue: " + editValue);
                Assert.AreEqual("27/06/2023", editValue);
                extentReports.CreateLog("Entered value for Pitch Book Date: "+editValue+ " is saved after clicking Save button ");

                //--Verify the fields of Signing section
                Assert.IsTrue(summaryPage.VerifyFieldsOfSigning(), "Verify that displayed fields of Signing section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields of Signing section are as expected ");

                //---Validate Edit Functionality
                string editValueSigning = summaryPage.ValidateEditFunctionalityOfSigning("27-Jun-2023");
                Console.WriteLine("EditValue: " + editValueSigning);
                Assert.AreEqual("27/06/2023", editValueSigning);
                extentReports.CreateLog("Entered value for Signing Date: " + editValueSigning + " is saved after clicking Save button ");

                //--Verify the fields of Closing section
                Assert.IsTrue(summaryPage.VerifyFieldsOfSigning(), "Verify that displayed fields of Closing section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields of Closing section are as expected ");

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


