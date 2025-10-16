using NUnit.Framework;
using OpenQA.Selenium;
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
    class TMTT0046328_VerifyTheFunctionalityOfTheRemovalOfTheMinimumFeeFromECMJobTypeForFlatFee_Part2 : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        NBCForm form = new NBCForm();
        CNBCForm cnbc = new CNBCForm();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTC1232 = "VerifyTheFunctionalityOfTheRemovalOfTheMinimumFeeFromECMJobTypeForFlatFee_Part2.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void NBCFormSubmitforReview()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1232;
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
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                Console.WriteLine("rowCount " + rowJobType);

                for (int row = 2; row <= 2; row++)
                {

                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);

                    //Verify the availability of Opportunity under HL Banker list
                    string tagOpp = opportunityHome.ClickOppUnderHLBanker();
                    Assert.AreEqual("Opportunities", tagOpp);
                    extentReports.CreateLog(tagOpp + " is displayed under HL Banker dropdown ");

                    //Verify that choose LOB is displayed after clicking New button
                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                    string titleOpp = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                    Assert.AreEqual("New Opportunity: " + valRecordType, titleOpp);
                    extentReports.CreateLog("Page with title: " + titleOpp + " is displayed upon clicking next button ");

                    //Calling AddOpportunities function
                    string opportunityNumber = addOpportunity.AddOpportunitiesLightning(valJobType, fileTC1232);
                    Console.WriteLine("value : " + opportunityNumber);
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTC1232);
                    Assert.AreEqual("Info", displayedTab);
                    extentReports.CreateLog("Tab with name: " + displayedTab + " is displayed upon saving internal deal team members details ");

                    string clientName = opportunityDetails.GetClientCompanyL();
                    string subjectName = opportunityDetails.GetSubjectCompanyL();
                    string jobType = opportunityDetails.GetJobTypeL();
                    opportunityDetails.ValidateFeesAndFinancialsTabL();
                    string valTxnSizeOpp = opportunityDetails.UpdateAndGetTransactionSizeL();

                    //Click on NBC and validate title of page
                    string title = opportunityDetails.ClickNBCFormLCNBC();
                    Assert.AreEqual("Opportunity Overview", title);
                    extentReports.CreateLog("CNBC Form page is displayed with default tab : " + title + " ");

                    //Click Fees tab  
                    string txtFees = form.ClickFeesTab();
                    Assert.AreEqual("Fees", txtFees);
                    extentReports.CreateLog("Tab with name " + txtFees + " is displayed upon clicking Fees tab. ");

                    //5.  TMTI0113202_Verify that the "Minimum Fee" field is hidden for the Transaction Type - "Flat Fee" from the CNBC form and the Cognos Report on the New Opportunities of the Equity Capital Market job type
                    bool flatFee = form.ValidateMinFeeFieldUponSelectingFlatAndIncentiveFeeType("Flat Fee");
                    Assert.AreEqual(false, flatFee);
                    extentReports.CreateLog("Minimum Fee field is not displayed upon saving Transaction Fee as Flat Fee in CNBC Form for Job Type :" + jobType + " ");

                    if (valJobType.Equals("Equity Placements"))
                    {
                        string pdfPath = form.ConnectCognoAndOpenPDF();
                    }
                    else
                    {
                        Console.WriteLine("No need to connect again");
                    }
                    string flatFeeReport = form.VerifyMinFeeFieldForFlatAndIncentiveFeeinReportInNewOpp("Flat Fee");
                    Assert.AreEqual("Flat Fee: ", flatFeeReport);
                    extentReports.CreateLog("Minimum Fee field is not displayed in Cognos report upon saving Transaction Fee as Flat Fee for Job Type :" + jobType + " ");

                    //6(a)   TMTI0113204_Verify that the "Minimum Fee" field is available for the Transaction Types—"Incentive Fee" and "Other" on the CNBC form and the Cognos Report PDF on the New Opportunities of the Equity Capital Market job type
                    bool incenFee = form.ValidateMinFeeFieldUponSelectingFlatAndIncentiveFeeType("Incentive Structure (Value Based)");
                    form.UpdateAllRatchetValues(fileTC1232, "Incentive Structure (Value Based)");
                    form.UpdateFinalRatchetAmt(fileTC1232);
                    Assert.AreEqual(true, incenFee);
                    extentReports.CreateLog("Minimum Fee field is displayed upon saving Transaction Fee as Incentive Fee in CNBC Form ");

                    IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                    js.ExecuteScript("window.scrollTo(0,-350)");
                    Thread.Sleep(4000);
                    string incenFeeReport = form.VerifyMinFeeFieldForFlatAndIncentiveFeeinReport("Incentive Structure (Value Based)");
                    Assert.AreEqual("Minimum Fee: ", incenFeeReport);
                    extentReports.CreateLog("Minimum Fee field is displayed in Cognos report upon saving Transaction Fee as Incentive Fee ");

                    //7.  TMTI0113206_Verify that the word "above" is added after the first value on the Final Ratchet percent on the Cognos PDF form. . 
                    string wordAbove = form.ValidateAboveKeywordInFinalRatchetPer();
                    Assert.AreEqual(" Above ", wordAbove);
                    extentReports.CreateLog("Word: " + wordAbove + " is displayed in Cognos report after the first value of the Final Ratchet percent.");

                    //6(b)-----for Other Fee type in CNBC Form and Congnos Report
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                    string txnFee = form.ValidateEstFeeFieldUponSelectingOtherFeeTypeInNewOpp();
                    Assert.AreEqual("Engagement Letter Minimum Fee", txnFee);
                    extentReports.CreateLog("Field with name: " + txnFee + " is displayed upon saving Transaction Fee as Other in CNBC Form ");

                    string estFee = form.VerifyMinFeeFieldForFlatAndIncentiveFeeinReport("Other Fee Structure");
                    Assert.AreEqual("Minimum Fee: ", estFee);
                    extentReports.CreateLog("The 'Minimum Fee' field is added in the Cognos report when Fee Type is 'Other' ");

                    //8.  TMTI0113208_Verify that the "Minimum Fee" field is hidden for the Transaction Type - "Flat Fee" from the NBC form and the Cognos Report PDF on the existing Opportunities 
                    driver.SwitchTo().Window(driver.WindowHandles.First());
                }
                form.SwitchFrame();

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

    

