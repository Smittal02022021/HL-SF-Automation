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
    class TMTT0046855_CFEngagementSummary_VerificationOfELEngagementDynamicsSectionAndSubsections : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        CFEngagementSummaryPage summaryPage = new CFEngagementSummaryPage();
        public static string fileTMTT0046855 = "TMTT0046855_VerificationOfELEngagementDynamicsSectionAndSubsections";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerificationOfELEngagementDynamicsSectionAndSubsections()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0046855;
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

                //1.   TMTI0114580_ Verify the "EL/Engagements Dynamics" subsections of the CF Engagement Summary
                engagementDetails.ClickCFEngsummaryButtonL();
                string secDynamics = summaryPage.ValidateELEngagementsDynamicsSection();                
                Assert.AreEqual("EL / Engagement Dynamics", secDynamics);
                extentReports.CreateLog("Section with name: " + secDynamics + " is displayed after clicking EL Engagements Dynamics section ");
                              
                Assert.IsTrue(summaryPage.VerifySubSectionsUnderELEngDynamicsSection(), "Verify that displayed sections under EL/Engagements Dynamics section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields under EL/Engagements Dynamics section are as expected ");

                //Verify Fees (Actual Amount) Help Text
                string helpText6 = summaryPage.GetFeesHelpText();
                Assert.AreEqual("Enter actual currency amounts I.e. one million would be 1,000,000", helpText6);
                extentReports.CreateLog("Help text displayed for Fees (Actual Amount) is: " + helpText6 + " ");

                //Verify Totals (Actual Amount) Help Text
                string helpText2 = summaryPage.GetTotalsHelpText();
                Assert.AreEqual("Enter actual currency amounts I.e. one million would be 1,000,000", helpText2);
                extentReports.CreateLog("Help text displayed for Totals (Actual Amount) is: " + helpText2 + " ");

                //Verify Incentive Structure (Actual Amount) Help Text
                string helpText3 = summaryPage.GetIncentiveStructureHelpText();
                Assert.AreEqual("Enter actual currency amounts I.e. one million would be 1,000,000", helpText3);
                extentReports.CreateLog("Help text displayed for Incentive Structure (Actual Amount) is: " + helpText3 + " ");

                //2.   TMTI0114579_Verify the fields and values of "EL/Engagement Dynamics – Fees (Actual Amount)" sub-section   
                Assert.IsTrue(summaryPage.VerifyFieldsUnderFeesActualAmountSection(), "Verify that displayed fields under Fees (Actual Amount) section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields under Fees (Actual Amount) section are as expected ");

                string cancelRetainer =summaryPage.ValidateCancelFunctionlaityOfFeeSection(fileTMTT0046855);
                Assert.AreEqual("USD 0.00 (GBP 0.00)", cancelRetainer);
                extentReports.CreateLog("Entered details are not saved after clicking Cancel button ");

                string saveRetainer = summaryPage.ValidateSaveFunctionlaityOfFeeSection(fileTMTT0046855,"1000");
                Assert.AreEqual("USD 1,000", saveRetainer);
                extentReports.CreateLog("Entered details are saved after clicking Save button ");

                summaryPage.ValidateSaveFunctionlaityOfFeeSection(fileTMTT0046855, "0");

                //3.   TMTI0114581_Verify the fields and values of "EL/Engagement Dynamics – Transaction (Actual Amount)" sub-section

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


