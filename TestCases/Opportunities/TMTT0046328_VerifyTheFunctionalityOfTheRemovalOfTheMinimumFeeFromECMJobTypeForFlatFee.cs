using AventStack.ExtentReports.Gherkin.Model;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Linq;
using System.Threading;

namespace SF_Automation.TestCases.Opportunities
{
    class TMTT0046328_VerifyTheFunctionalityOfTheRemovalOfTheMinimumFeeFromECMJobTypeForFlatFee : BaseClass
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
        CustomFunctions customFunctions = new CustomFunctions();

        public static string fileCNBC = "TMTT0046328_VerifyTheFunctionalityOfTheRemovalOfTheMinimumFee1.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void CNBCFormSubmitforReview()
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
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                Console.WriteLine("rowCount " + rowJobType);

                for (int row = 2; row <= rowJobType; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    //Search for created opportunity
                    string opportunityNumber = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 4);

                    opportunityHome.SearchMyOpportunitiesInLightning(opportunityNumber, valUser);
                    //opportunityHome.SearchMyOpportunitiesInLightning("78446",valUser);                    
                    extentReports.CreateLog("Records matching to mentioned search criteria are displayed ");
                    opportunityDetails.ValidateFeesAndFinancialsTabL();
                    string valTxnSizeOpp = opportunityDetails.GetTransactionSizeL();

                        //1.  TMTI0113194_Verify that the "Estimated Fee" field is added in the CNBC form on the existing Opportunities
                        string title = opportunityDetails.ClickNBCFormLCNBC();
                        nform.ClickFeesTab();
                        string txnFee = nform.ValidateEstFeeFieldUponSelectingOtherFeeType();
                        string valTxnFee = nform.GetEstFeeValueUponSavingOtherFeeType();
                        string valMinFee = nform.GetMinFeeValue();
                        Console.WriteLine(valTxnFee);
                        Assert.AreEqual("Estimated Contingent Fee", txnFee);
                        Assert.AreEqual(valTxnSizeOpp, valMinFee);
                        extentReports.CreateLog("Field with name: "+txnFee + " and value: "+ valMinFee + " is displayed upon saving Transaction Fee as Other in CNBC Form ");

                        //2.  TMTI0113196_Verify that the "Estimated Fee" field is added in the Cognos report at the top of the Fees page of the existing opportunity.
                        //Connect Cogno and fetch the report
                        string pdfPath = nform.ConnectCognoAndOpenPDF();
                        Console.WriteLine("pdfPath:" + pdfPath);
                        string estFee=  nform.VerifyEstimatedFeeFieldinReport();                       
                        string estFeeValue = nform.ValidaterEstimatedFeeValueInReport();
                        Console.WriteLine("estFeeValue:" + estFeeValue);
                        Assert.AreEqual("Estimated Fee: ", estFee);
                        Assert.AreEqual(valTxnFee, estFeeValue);
                        extentReports.CreateLog("The 'Estimated Fee' field is added in the Cognos report and its value is same as Estimated Total Fee when Fee Type is 'Other' ");

                        //3.  TMTI0113198_Verify that the "Minimum Fee" field is hidden for the Transaction Type - "Flat Fee" from the CNBC form and the Cognos Report PDF on the existing Opportunities of the Equity Capital Market job types.
                        bool flatFee = nform.ValidateMinFeeFieldUponSelectingFlatAndIncentiveFeeType("Flat Fee");                        
                        Assert.AreEqual(false, flatFee);
                        extentReports.CreateLog("Estimated Contingent Fee field is not displayed upon saving Transaction Fee as Flat Fee in CNBC Form ");

                        string flatFeeReport = nform.VerifyMinFeeFieldForFlatAndIncentiveFeeinReport("Flat Fee");
                        Assert.AreEqual("Flat Fee: ", flatFeeReport);
                        extentReports.CreateLog("Minimum Fee field is not displayed in Cognos report upon saving Transaction Fee as Flat Fee ");

                        //4.   TMTI0113200_Verify that the "Minimum Fee" field is available for the Transaction Type - "Incentive Fee" on the CNBC form and the Cognos Report PDF on the existing Opportunities of the Equity Capital Market job types
                        bool incenFee = nform.ValidateMinFeeFieldUponSelectingFlatAndIncentiveFeeType("Incentive Structure (Value Based)");
                        Assert.AreEqual(true, incenFee);
                        extentReports.CreateLog("Minimum Fee field is displayed upon saving Transaction Fee as Incentive Fee in CNBC Form ");

                        string incenFeeReport = nform.VerifyMinFeeFieldForFlatAndIncentiveFeeinReport("Incentive Structure (Value Based)");
                        Assert.AreEqual("Minimum Fee: ", incenFeeReport);
                        extentReports.CreateLog("Minimum Fee field is displayed in Cognos report upon saving Transaction Fee as Incentive Fee ");

                    driver.SwitchTo().Window(driver.WindowHandles.First());
                                            
                }
                usersLogin.DiffLightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.DiffLightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
    



