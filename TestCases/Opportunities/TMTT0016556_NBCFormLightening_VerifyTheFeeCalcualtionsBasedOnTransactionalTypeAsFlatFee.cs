using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Globalization;

namespace SF_Automation.TestCases.Opportunity
{
    class TMTT0016556_NBCFormLightening_VerifyTheFeeCalcualtionsBasedOnTransactionalTypeAsFlatFee : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        NBCForm form = new NBCForm();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTC1232 = "T1232_NBCFormSubmitforReview6.xlsx";

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

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling AddOpportunities function                
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunities(valJobType, fileTC1232);
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC1232);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details page 
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityDetails.ValidateOpportunityDetails() + " is created ");

                //Fetch values of Opportunity Name, Client, Subject and Job Type
                string oppNum = opportunityDetails.GetOppNumber();
                string clientName = opportunityDetails.GetClient();
                string clientOwnership = opportunityDetails.GetClientOwnership();
                string subjectName = opportunityDetails.GetSubject();
                string subjectOwnership = opportunityDetails.GetSubjectOwnership();
                string jobType = opportunityDetails.GetJobType();
                string IG = opportunityDetails.GetIG();
                Console.WriteLine(jobType);

                //Call function to update HL -Internal Team details
                opportunityDetails.UpdateRetainerAndMonthlyFee();
                opportunityDetails.UpdateInternalTeamDetails(fileTC1232);

                //Logout of user and validate Admin login
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //update CC and NBC checkboxes 
                opportunityDetails.UpdateOutcomeDetails(fileTC1232);
                opportunityDetails.UpdateCCOnly();

                //Login as Standard User and validate the user
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                string retainer = opportunityDetails.GetRetainer();
                string progressFee = opportunityDetails.GetMonthlyFee();

                //Click on NBC page and validate title of page
                string title = opportunityDetails.ClickNBCFormL();
                Assert.AreEqual("Public Sensitivity", title);
                extentReports.CreateLog("NBC Form page is displayed with default tab : " + title + " ");

                //Click on Fees Tab
                form.ClickFeesTab();
                string nbcRetainer = form.GetRetainer();
                string nbcProgressFee = form.GetProgressFee();

                //Select the Review Submission button
                form.ClickReviewSubmission();
                
                //Save all the mandatory fields details in all tabs.
                form.ClickOpportunityOverview();
                form.SaveAllReqFieldsInOppOverview(fileTC1232);

                form.ClickFinancialsTab();
                form.SaveAllReqFieldsInFinancialsOverview(fileTC1232);

                form.ClickFeesTab();
                extentReports.CreateLog("Opened Fees tab");

                Assert.IsTrue(form.VerifyTxnTypeFee(), "Verified that displayed Transaction Fee Types are same");
                extentReports.CreateLog("Displayed Transaction Fee Types are correct ");

                //Validate that Flat Fee (MM) field appears upon selecting Flat Fee type
                string flatFee = form.ValidateFlatFeeField();
                Assert.AreEqual("Flat Fee (MM)", flatFee);
                extentReports.CreateLog("Field with name : " + flatFee + " is displayed upon selecting Transaction Fee Type as Flat Fee ");

                //Get Retainer from NBC form
                Assert.AreEqual(retainer, nbcRetainer);
                extentReports.CreateLog("Retainer value in NBC form " + nbcRetainer + " matches with Retainer in Opportunity details page ");
                               
                Assert.AreEqual(progressFee, nbcProgressFee);
                extentReports.CreateLog("Progress Fee in NBC form " + nbcProgressFee + " matches with Progress Fee in Opportunity details page ");

                form.SaveAllReqFieldsInFees(fileTC1232);

                //Navigate to previous window and validate Retainer and Monthly Fee
                form.NavigateToPreviousWindow();
                string latestRetainer = opportunityDetails.GetRetainer();
                Assert.AreEqual(retainer, latestRetainer);
                extentReports.CreateLog("Retainer value in Opportunity details: " + latestRetainer + " matches with earlier value of Retainer ");

                string latestProgressFee = opportunityDetails.GetMonthlyFee();
                Assert.AreEqual(progressFee, latestProgressFee);
                extentReports.CreateLog("Progress Fee value in Opportunity details: " + latestProgressFee + " matches with earlier value of Progress Fee ");

                //Clear out the Progress Fee and Progress Fee Creditable fields
                form.NavigateToNextWindow();
                string estFeeWithRetainer = form.UpdateReviewSubAndProgressFee(fileTC1232);
                Console.WriteLine(estFeeWithRetainer);

                string fee = ReadExcelData.ReadData(excelPath, "NBCForm", 64);
                double actualFee = Convert.ToDouble(fee);
                string feeCred = ReadExcelData.ReadData(excelPath, "NBCForm", 65);
                double actualFeeCred = Convert.ToDouble(feeCred);
                Assert.AreEqual(((actualFee- ((actualFee*actualFeeCred)/100))/1000000).ToString("0.00"), estFeeWithRetainer);
                extentReports.CreateLog("Estimated Total Fee (MM) " + estFeeWithRetainer + " is getting calculated as expected when only Retainer and Retainer Fee Creditable is entered for Flat Fee ");

                //Clear out Retainer Fee fields, enter Progress Fee and validate Estimated Total Fee
                string estFeeWithProgress =form.UpdateRetainerAndProgressFee(fileTC1232);
                Console.WriteLine(estFeeWithProgress);
                Assert.AreEqual(((actualFee - ((actualFee * actualFeeCred)/100))).ToString("0.00"), estFeeWithProgress);
                extentReports.CreateLog("Estimated Total Fee (MM) " + estFeeWithProgress + " is getting calculated as expected when only Progress and Progress Fee Creditable is entered for Flat Fee ");
                
                //Enter the values of Retainer Fee fieds and Progress Fee and validate Estimated Total Fee
                string estFeeWithBoth = form.UpdateBothRetainerAndProgressFee(fileTC1232);
                Console.WriteLine(estFeeWithBoth);
                Assert.AreEqual((((actualFee - ((actualFee * actualFeeCred) / 100)) / 1000000)+((actualFee - ((actualFee * actualFeeCred) / 100)))).ToString("0.00"), estFeeWithBoth);
                extentReports.CreateLog("Estimated Total Fee (MM) " + estFeeWithBoth + " is getting calculated as expected when Retainer, Retainer Fee Creditable,Progress and Progress Fee Creditable is entered for Flat Fee ");

                //Validate that Flat Fee (MM) field is blank
                //string flatFeeMM = form.GetFlatFee();
                //Assert.AreEqual("", flatFeeMM);
                extentReports.CreateLog("No error is displayed when Flat Fee is left blank and saved ");

                //Save the Flat Fee and validate the saved value
                string savedFlatFee = form.SaveFlatFee(fileTC1232);
                string valueFee = ReadExcelData.ReadData(excelPath, "NBCForm", 64);
                Assert.AreEqual(valueFee, savedFlatFee);
                extentReports.CreateLog("Flat Fee with value: "+savedFlatFee + " is saved ");

                //Validate Estimated Total Fee after entering Flat Fee
                string estFeeWithFlatFee = form.GetEstTotalFee();
                Assert.AreEqual((((((actualFee - ((actualFee * actualFeeCred) / 100)) / 1000000) + ((actualFee - ((actualFee * actualFeeCred) / 100)))))+ Convert.ToDouble(savedFlatFee)).ToString("0.00"), (estFeeWithFlatFee));
                extentReports.CreateLog("Estimated Total Fee (MM) " + estFeeWithBoth + " is getting calculated as expected when Retainer, Retainer Fee Creditable,Progress and Progress Fee Creditable is entered along with Flat Fee ");

                //Validate that if Minimum Fee is greater than Estimated Total Fee then it will be copied to Estimated Total Fee
                string MinFee = form.UpdateMinFee();
                string EstFeeWithGreaterMinFee = form.GetEstTotalFee();
                Assert.AreEqual(MinFee, EstFeeWithGreaterMinFee);
                extentReports.CreateLog("Estimated Total Fee (MM) " + EstFeeWithGreaterMinFee + " is getting displayed same as Minimum Fee when Minimum Fee is greater than Estimated Total Fee ");

                //Validate that if Minimum Fee is less than Estimated Total Fee then it will not be copied to Estimated Total Fee
                string MinFeeLess = form.UpdateMinFeeLessThanEstTotalFee();
                string EstFeeWithLessMinFee = form.GetEstTotalFee();
                Assert.AreNotEqual(MinFee, EstFeeWithLessMinFee);
                Assert.AreEqual((((((actualFee - ((actualFee * actualFeeCred) / 100)) / 1000000) + ((actualFee - ((actualFee * actualFeeCred) / 100))))) + Convert.ToDouble(savedFlatFee)).ToString("0.00"), EstFeeWithLessMinFee);
                extentReports.CreateLog("Estimated Total Fee (MM) " + EstFeeWithGreaterMinFee + " is getting displayed as it is when Minimum Fee is less than Estimated Total Fee ");
                                                                                          
                form.SwitchFrame();
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

    

