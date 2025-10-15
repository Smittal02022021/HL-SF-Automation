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
    class TMTT0046851_CFEngagementSumamry_VerificationOfCapitalizationDetailsSectionAndSubsections : BaseClass
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

                //1. TMTI0114561_Verify the availability of Capitalization Details subsections on the CF Engagement Summary
                engagementDetails.ClickCFEngsummaryButtonL();
                string secParties = summaryPage.ValidateCapitalizationSection();           
                
                Assert.IsTrue(summaryPage.VerifySubSectionsOfCapitalization(), "Verify that displayed sub sections under Capitalization section are same");
                extentReports.CreateStepLogs("Passed", "Displayed sub sections under Capitalization section are as expected ");

                //2. TMTI0114560_Verify the fields and values added on the Capitalization Details – Source of Funds subsection
                Assert.IsTrue(summaryPage.VerifyFieldsOfSourceFunds(), "Verify that displayed fields of Source Of funds section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields of source Of funds section are as expected ");

                //---Validate Cancel Functionality
                string cancelValue = summaryPage.ValidateCancelFunctionalityOfSourceOfFunds("12");
                Assert.AreEqual(finalCurrency + " 0", cancelValue);
                extentReports.CreateLog("Entered value for Revolving Credit Facility field is not saved after clicking Cancel button ");

                //---Validate Save Functionality
                string editValue = summaryPage.ValidateEditFunctionalityOfSourceOfFunds("12");
                Console.WriteLine("EditValue: " + editValue);
                Assert.AreEqual(finalCurrency+" 12", editValue);
                extentReports.CreateLog("Entered value for Revolving Credit Facility: "+editValue+ " is saved after clicking Save button with the same currency as of Engagement ");
                summaryPage.ValidateEditFunctionalityOfSourceOfFunds("0");

                //3.	TMTI0114559_ Verify the fields and values added on the Capitalization Details – Use of Funds subsection
                Assert.IsTrue(summaryPage.VerifyFieldsOfUseOfFunds(), "Verify that displayed fields of Use Of funds section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields of Use Of funds section are as expected ");

                //---Validate Cancel Functionality
                string cancelValueUse = summaryPage.ValidateCancelFunctionalityOfUseOfFunds("12");
                Assert.AreEqual(finalCurrency + " 0", cancelValueUse);
                extentReports.CreateLog("Entered value for Purchase Price field is not saved after clicking Cancel button ");

                //---Validate Save Functionality
                string editValueUse = summaryPage.ValidateEditFunctionalityOfUseOfFunds("12");
                Console.WriteLine("EditValue: " + editValueUse);
                Assert.AreEqual(finalCurrency + " 12", editValueUse);
                extentReports.CreateLog("Entered value for Purchase Price " + editValueUse + " is saved after clicking Save button with the same currency as of Engagement ");
                summaryPage.ValidateEditFunctionalityOfUseOfFunds("0");


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


