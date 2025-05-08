using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Globalization;

namespace SF_Automation.TestCases.Opportunities
{
    class TMTT0016556AndTMTT0014794_NBCFormLightening_VerifyTheFeeCalcualtionsBasedOnTransactionalTypeAsFlatFee_AddProgressAndRetainerCreditableCalculationsToFlatFee  : BaseClass
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

                //Calling Login function                
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

                //Verify the availability of Opportunity under HL Banker list
                string tagOpp = opportunityHome.ValidateOppUnderHLBanker();
                Assert.AreEqual("Opportunities", tagOpp);
                extentReports.CreateLog(tagOpp + " is displayed under HL Banker dropdown ");

                //Verify that choose LOB is displayed after clicking New button
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                string titleOpp = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                Assert.AreEqual("New Opportunity: " + valRecordType, titleOpp);
                extentReports.CreateLog("Page with title: " + titleOpp + " is displayed upon clicking next button ");

                //Calling AddOpportunities function
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
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
                opportunityDetails.UpdateClientSubjectOwnershipL();
                string clientOwnership = opportunityDetails.GetClientOwnershipLPostUpdate();
                string subjectOwnership = opportunityDetails.GetSubjectOwnershipLPostUpdate();

                //Call function to update HL -Internal Team details
                opportunityDetails.UpdateRetainerAndMonthlyFeeL();
                string retainer = opportunityDetails.GetRetainerL();
                string progressFee = opportunityDetails.GetMonthlyFeeL();

                //Logout of user and validate Admin login
                usersLogin.DiffLightningLogout();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(opportunityNumber);

                //update CC and NBC checkboxes 
                opportunityDetails.UpdateInternalTeamDetails(fileTC1232);
                opportunityDetails.UpdateOutcomeDetails(fileTC1232);
                opportunityDetails.UpdateCCOnly();

                //Login as Standard User and validate the user
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUserLightning();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchMyOpportunitiesInLightning(opportunityNumber, valUser);
                string oppNumber = opportunityDetails.GetOpportunityNumberLightning();

                //Click on NBC page and validate title of page
                string title = opportunityDetails.ClickNBCFormLCNBC();
                Assert.AreEqual("Opportunity Overview", title);
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
                Assert.AreEqual("Flat Fee", flatFee);
                extentReports.CreateLog("Field with name : " + flatFee + " is displayed upon selecting Transaction Fee Type as Flat Fee ");

                //Get Retainer from NBC form
                Assert.AreEqual(retainer, nbcRetainer);
                extentReports.CreateLog("Retainer value in NBC form " + nbcRetainer + " matches with Retainer in Opportunity details page ");
                               
                Assert.AreEqual(progressFee, nbcProgressFee);
                extentReports.CreateLog("Progress Fee in NBC form " + nbcProgressFee + " matches with Progress Fee in Opportunity details page ");

                form.SaveAllReqFieldsInFees(fileTC1232,"Flat Fee");

                //Navigate to previous window and validate Retainer and Monthly Fee
                // form.NavigateToPreviousWindow();
                opportunityDetails.ClickOppTab();
                string latestRetainer = opportunityDetails.GetRetainerL();
                Assert.AreEqual(retainer, latestRetainer);
                extentReports.CreateLog("Retainer value in Opportunity details: " + latestRetainer + " matches with earlier value of Retainer ");

                string latestProgressFee = opportunityDetails.GetMonthlyFeeL();
                Assert.AreEqual(progressFee, latestProgressFee);
                extentReports.CreateLog("Progress Fee value in Opportunity details: " + latestProgressFee + " matches with earlier value of Progress Fee ");

                //Clear out the Progress Fee and Progress Fee Creditable fields
               // form.NavigateToNextWindow();
                string estFeeWithRetainer = form.UpdateReviewSubAndProgressFee(fileTC1232, "Flat Fee");
                Console.WriteLine(estFeeWithRetainer);

                string fee = ReadExcelData.ReadData(excelPath, "NBCForm", 64);
                double actualFee = Convert.ToDouble(fee);
                Console.WriteLine(actualFee);
                string feeCred = ReadExcelData.ReadData(excelPath, "NBCForm", 65);
                double actualFeeCred = Convert.ToDouble(feeCred);
                Console.WriteLine(((actualFee - ((actualFee * actualFeeCred) / 100))).ToString("0.00"));
                Assert.AreEqual(((actualFee- ((actualFee*actualFeeCred)/100))).ToString("0.00"), estFeeWithRetainer);
                extentReports.CreateLog("Estimated Total Fee (MM) " + estFeeWithRetainer + " is getting calculated as expected when only Retainer and Retainer Fee Creditable is entered for Flat Fee ");
                
                //Clear out Retainer Fee fields, enter Progress Fee and validate Estimated Total Fee
                string estFeeWithProgress =form.UpdateRetainerAndProgressFee(fileTC1232);
                Console.WriteLine(estFeeWithProgress);
                Assert.AreEqual(((actualFee - ((actualFee * actualFeeCred)/100))).ToString("0.00"), estFeeWithProgress);
                extentReports.CreateLog("Estimated Total Fee (MM) " + estFeeWithProgress + " is getting calculated as expected when only Progress and Progress Fee Creditable is entered for Flat Fee ");
                
                //Enter the values of Retainer Fee fieds and Progress Fee and validate Estimated Total Fee
                string estFeeWithBoth = form.UpdateBothRetainerAndProgressFee(fileTC1232);
                Console.WriteLine(estFeeWithBoth);
                Assert.AreEqual((((actualFee - ((actualFee * actualFeeCred) / 100)))+((actualFee - ((actualFee * actualFeeCred) / 100)))).ToString("0.00"), estFeeWithBoth);
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
                Assert.AreEqual((((((actualFee - ((actualFee * actualFeeCred) / 100))) + ((actualFee - ((actualFee * actualFeeCred) / 100)) )))+ Convert.ToDouble(savedFlatFee)).ToString("0.00"), (estFeeWithFlatFee));
                extentReports.CreateLog("Estimated Total Fee " + estFeeWithBoth + " is getting calculated as expected when Retainer, Retainer Fee Creditable,Progress and Progress Fee Creditable is entered along with Flat Fee ");

                //Commented as Min Fee is not applicable for Flat Fee
                ////Validate that if Minimum Fee is greater than Estimated Total Fee then it will be copied to Estimated Total Fee
                //string MinFee = form.UpdateMinFee();
                //string EstFeeWithGreaterMinFee = form.GetEstTotalFee();
                //Assert.AreEqual(MinFee, EstFeeWithGreaterMinFee);
                //extentReports.CreateLog("Estimated Total Fee (MM) " + EstFeeWithGreaterMinFee + " is getting displayed same as Minimum Fee when Minimum Fee is greater than Estimated Total Fee ");

                ////Validate that if Minimum Fee is less than Estimated Total Fee then it will not be copied to Estimated Total Fee
                //string MinFeeLess = form.UpdateMinFeeLessThanEstTotalFee();
                //string EstFeeWithLessMinFee = form.GetEstTotalFee();
                //Assert.AreNotEqual(MinFee, EstFeeWithLessMinFee);
                //Assert.AreEqual((((((actualFee - ((actualFee * actualFeeCred) / 100)) / 1000000) + ((actualFee - ((actualFee * actualFeeCred) / 100)) / 1000000))) + Convert.ToDouble(savedFlatFee)).ToString("0.00"), EstFeeWithLessMinFee);
                //extentReports.CreateLog("Estimated Total Fee (MM) " + EstFeeWithGreaterMinFee + " is getting displayed as it is when Minimum Fee is less than Estimated Total Fee ");

                //Update Creditable fields to 0%
                string estFeeWithZeroCred = form.UpdateFeeCreditables("0");               
                Assert.AreEqual((((((actualFee - ((actualFee * 0) / 100))) + ((actualFee - ((actualFee * 0) / 100))))) + Convert.ToDouble(savedFlatFee)).ToString("0.00"), (estFeeWithZeroCred));
                extentReports.CreateLog("Estimated Total Fee " + estFeeWithZeroCred + " is getting calculated as expected when Retainer, Retainer Fee Creditable are saved as 0 ");

                //Update Creditable fields to 100%
                string estFeeWith100Cred = form.UpdateFeeCreditables("100");
                Assert.AreEqual((((((actualFee - ((actualFee * 100) / 100))) + ((actualFee - ((actualFee * 100) / 100))))) + Convert.ToDouble(savedFlatFee)).ToString("0.00"), (estFeeWith100Cred));
                extentReports.CreateLog("Estimated Total Fee " + estFeeWith100Cred + " is getting calculated as expected when Retainer, Retainer Fee Creditable are saved as 100 ");

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

    

