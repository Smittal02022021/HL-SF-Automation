using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunity
{
    class TMTT0014637_EstimatedValuationFieldIsIndependentInputFieldOnOpportunityAndNBC : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        CNBCForm form = new CNBCForm();
        NBCForm nform = new NBCForm();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileCNBC = "TMTT0013974_VerifyThePopUpLoadsToSelectTheFormType.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void EstimatedValuationField()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileCNBC;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 3, 3);
              
                //Search for created opportunity
                string message= opportunityHome.SearchOpportunityWithJobTypeAndStge(valJobType, "Low");
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matching to mentioned search criteria are displayed ");

                //Update the Est. Transaction Size and get its updated value
                string updTxnSize= opportunityDetails.UpdateEstTransactionSize();
                extentReports.CreateLog("Est Transaction Size is updated with value: "+ updTxnSize + " ");

                //Open the NBC form and navigate to Opportunity Overview tab
                string title = opportunityDetails.ClickNBCFormL();
                Assert.AreEqual("Public Sensitivity",title);
                extentReports.CreateLog("Page with default tab: " + title + " is displayed upon clicking NBC-L form button for Opportunity with Job Type : "+valJobType +" ");

                string oppOverview = nform.ClickOpportunityOverview();
                Assert.AreEqual("Opportunity Overview", oppOverview);
                extentReports.CreateLog("Page with tab: " + oppOverview + " is displayed upon clicking Opportunity Overview tab ");

                //Validate the value of Estimated Valuation on NBC Form
                string estVal= nform.GetEstimatedValuation();
                Assert.AreNotEqual(updTxnSize, estVal);
                extentReports.CreateLog("Est Transaction Size is not copied to Estimated Valuation in NBC ");

                //Update Estimated Valuation in NBC now
                string updEstVal = nform.UpdateEstimatedValuation();
                extentReports.CreateLog("Estimated Valuation in NBC is updated with value: " + updEstVal + " ");

                //Validate the value of Est Transaction Size on Opportunity Details
                form.SwitchFrame();
                string txnSize = opportunityDetails.GetEstTransactionSize();
                Assert.AreNotEqual(updEstVal, txnSize);
                extentReports.CreateLog("Estimated Valuation is not copied to Est Transaction Size in Opportunity Details ");
                                            
                
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
    



