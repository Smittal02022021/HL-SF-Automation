using AventStack.ExtentReports.Gherkin.Model;
using iTextSharp.text.pdf.security;
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
    class TMTT0046850_CFEngagementSumamry_VerificationOfSellerSectionAndSubsections : BaseClass
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
                string message=  engHome.SearchEngagementWithNumberOnLightning(ReadExcelData.ReadData(excelPath, "Engagement", 2),valJobType );
                Assert.AreEqual("Project Moon", message);
                extentReports.CreateLog("Records matching with selected Job Type are displayed ");

                //1. TMTI0114550_Verify the availability of sections "Seller" and "Buyer" are displayed under Parties
                engagementDetails.ClickCFEngsummaryButtonL();
                string secParties = summaryPage.ValidatePartiesSection();
                string secSeller = summaryPage.ValidateSellerSection();
                string secBuyer = summaryPage.ValidateBuyerSection();

                Assert.AreEqual("Parties", secParties);
                Assert.AreEqual("Seller", secSeller);
                Assert.AreEqual("Buyer", secBuyer);
                extentReports.CreateLog("Sub sections " + secSeller + " and " + secBuyer + " is displayed under section "+ secParties + " on CF Engagement Summary page ");

                //2. TMTI0114547_Verify the Seller's basic information is displayed and mapped to the fields on the corresponding Engagement
                Assert.IsTrue(summaryPage.VerifyFieldsUnderSellerSection(), "Verify that displayed fields under Seller section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields under Seller section are as expected for job Type: " +valJobType +" ");

                string iconSeller = summaryPage.ValidateSellerIcon();
                Assert.AreEqual("success", iconSeller);
                extentReports.CreateLog("Green tick is displayed on section " + secSeller + " indicating the field information is pre-populated ");

                //3.  TMTI0114551_Seller - Verify that the "Seller's Background" information is displayed under the subsection Seller Background
                Assert.IsTrue(summaryPage.VerifyFieldsUnderSellerBackGroundSection(), "Verify that displayed fields under Seller Background section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields under Seller Background section are as expected for job Type: " + valJobType + " ");

                string IG = summaryPage.ValidateIGValueInSellerBackground();
                string Sector = summaryPage.ValidateSectorValueInSellerBackground();
                string Desc = summaryPage.ValidateDescriptionValueInSellerBackground();
                Console.WriteLine("Desc " +Desc);

                string industryGroup = summaryPage.ValidateIGValueOfCompany();
                Assert.AreEqual(industryGroup, IG);
                extentReports.CreateLog("Industry Group " + industryGroup + " value is mapped with Company's Industry Group ");

                string sector = summaryPage.ValidateSectorValueOfCompany();
                Assert.AreEqual(Sector, sector);
                extentReports.CreateLog("Sector " + sector + " value is mapped with Company's Sector ");

                string description = summaryPage.ValidateDescriptionValueOfCompany();
                Assert.AreEqual(Desc, description);
                extentReports.CreateLog("Description " + description + " value is mapped with Company's Description ");

                //4.   TMTI0114555_Verify that the "Seller's Details" information is displayed under the subsection Seller Details.
                Assert.IsTrue(summaryPage.VerifyFieldsUnderSellerDetailsSection(), "Verify that displayed fields under Seller Details section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields under Seller Details section are as expected for job Type: " + valJobType + " ");

                string iconSellerDetails = summaryPage.ValidateSellerDetailsIcon();
                Assert.AreEqual("Transaction Rationale", iconSellerDetails);
                extentReports.CreateLog("Mandatory field " + iconSellerDetails + " is displayed upon clicking Seller Details icon ");

                string txnRationale = summaryPage.GetValueOfTxnRationale();

                Assert.IsTrue(summaryPage.VerifyTxnRationaleValues(), "Verify that displayed values of Transaction Rationale are same");
                extentReports.CreateStepLogs("Passed", "Displayed values of Transaction Rationale are as expected ");

                //5. TMTI0114556_Verify the "Edit" functionality on the Seller Details of the CF Engagement Summary
                //---Validate Cancel functionality of Selller details
                string cancelTxnRationale = summaryPage.ValidateCancelFunctionalityOfSellerDetailsSection();
                Console.WriteLine("cancelTxnRationale" + cancelTxnRationale);
                Assert.AreEqual(txnRationale, cancelTxnRationale);
                extentReports.CreateLog("Details are not saved upon clicking Cancel button in Seller Details button ");

                //-- Valdiate Save functionality of Seller details
                string saveTxnRationale = summaryPage.ValidateSaveFunctionalityOfSellerDetailsSection("Public - Hostile");
                Assert.AreNotEqual(cancelTxnRationale, saveTxnRationale);
                extentReports.CreateLog("Details are saved upon clicking Save button in Seller Details button ");

                summaryPage.ValidateSaveFunctionalityOfSellerDetailsSection("Public - Activist Shareholder");

                //6. TMTI0114540_Verify that the "Seller Financials" is displayed under subsection Seller Financials.
                string iconAddRecord = summaryPage.ValidateAddRecordIcon();
                Assert.AreEqual("Add Record", iconAddRecord);
                extentReports.CreateLog("Icon " + iconAddRecord + " is displayed in Seller Financials section ");

                Assert.IsTrue(summaryPage.VerifyAddRecordFields(), "Verify that displayed fields on Add Record section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields on Add Record section are as expected ");




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


