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

                //Get the value of Transaction type
                string txnType = engagementDetails.GetValueOfTransactionTypeL();

                //Get the value of Post Transaction Status
                string txnStatus = engagementDetails.GetValueOfPostTransactionStatusL();

                //Get the value of Company Description
                string compDesc = engagementDetails.GetValueOfCompDescL();

                //Get the value of Business Description
                string busDesc = engagementDetails.GetValueOfBusinessDescL();

                //Get the value of Restructuring Description 
                string reDesc = engagementDetails.GetValueOfRestructuringDescL();

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

                //TMTI0072772_Verify that the values displayed in the Transaction Type field will be fetched from the engagement detail page.
                string txnTypeInSummary = summaryPage.GetValueOfTransactionTypeInFREngL();
                Assert.AreEqual(txnType, txnTypeInSummary);
                extentReports.CreateLog("Value of Transaction Type: " + txnTypeInSummary + " in Engagement Info tab is same as it is in engagement details page " );

                //TMTI0072774- Verify that the values displayed in the Post-Transaction Status field will be fetched from the engagement detail page
                string txnStatusInSummary = summaryPage.GetValueOfPostTransactionStatusInFREngL();
                Assert.AreEqual(txnStatus, txnStatusInSummary);
                extentReports.CreateLog("Value of Post Transaction Status: " + txnStatusInSummary + " in Engagement Info tab is same as it is in engagement details page ");

                //TMTI0072776- Verify that the Client Description field will pull the Company description given on the engagement page under the Engagement and Business Description section
                string clientDescInSummary = summaryPage.GetValueOfClientDescInFREngL();
                Assert.AreEqual(compDesc, clientDescInSummary);
                extentReports.CreateLog("Value of Client Description: " + clientDescInSummary + " in Engagement Info tab is same as it is in Company Description field on engagement details page ");

                //TMTI0072778_Verify that the Company Description field will pull the Business Description given on the engagement page under the Engagement and Business Description section
                string compDescInSummary = summaryPage.GetValueOfCompDescInFREngL();
                Assert.AreEqual(busDesc, compDescInSummary);
                extentReports.CreateLog("Value of Company Description: " + compDescInSummary + " in Engagement Info tab is same as it is in Business Description field on engagement details page ");

                //TMTI0072780 -Verify that the Restructuring Transaction Description field will pull the details given on the engagement page under the Engagement and Business Description section
                string reTxnDescInSummary = summaryPage.GetValueOfReTxnDescInFREngL();
                Assert.AreEqual(reDesc, reTxnDescInSummary);
                extentReports.CreateLog("Value of Restructuring Transaction Description: " + reTxnDescInSummary + " in Engagement Info tab is same as it is in Restructuring Description field on engagement details page ");

                //TMTI0072782 - Verify the default note displayed on the Engagement Information tab
                string noteInSummary = summaryPage.GetNoteDisplayedOnEngInfoL();
                Assert.AreEqual("*Please fill out the required information in the Engagement and Business Information Section of the Engagement", noteInSummary);
                extentReports.CreateLog("Message: " + noteInSummary + " is dispalyed on the bottom of Engagement Info tab ");

                //TMTI0072784 -Verify that the fields in the engagement info section are all non-editable fields on the screen
                string editFields = summaryPage.ValidateTransactionTypeIfEditable();
                Assert.AreEqual("False", editFields);
                extentReports.CreateLog("Fields are non editable on Engagement Info tab ");            

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


