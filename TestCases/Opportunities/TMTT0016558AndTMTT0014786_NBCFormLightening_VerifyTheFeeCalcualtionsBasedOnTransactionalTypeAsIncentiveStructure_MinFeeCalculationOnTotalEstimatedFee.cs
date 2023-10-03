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
    class TMTT0016558AndTMTT0014786_NBCFormLightening_VerifyTheFeeCalcualtionsBasedOnTransactionalTypeAsIncentiveStructure_MinFeeCalculationOnTotalEstimatedFee : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        NBCForm form = new NBCForm();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTC1232 = "T1232_NBCFormSubmitforReview.xlsx";

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

                form.SaveAllReqFieldsInFeesWhenTypeIsIncentive(fileTC1232);

                //Validate that Incentive Fee structure section appears upon selecting Incentive Fee structure type
                string incentiveFee = form.ValidateIncentiveFeeSection();
                Assert.AreEqual("Incentive Fee structure", incentiveFee);
                extentReports.CreateLog("Section with name : " + incentiveFee + " is displayed upon selecting Transaction Fee Type as Incentive structure ");

                //Validate the Base Fee (MM) field 
                string lblBaseFee = form.ValidateBaseFee();
                Assert.AreEqual("Base Fee (MM)", lblBaseFee);
                extentReports.CreateLog("Field with name : " + lblBaseFee + " is displayed upon saving Transaction Fee Type as Incentive structure ");

                //Validate First Ratchet Percent field
                string lbl1stRatchetPer = form.ValidateFirstRatchetPercent();
                Assert.AreEqual("First Ratchet Percent", lbl1stRatchetPer);
                extentReports.CreateLog("Field with name : " + lbl1stRatchetPer + " is displayed upon saving Transaction Fee Type as Incentive structure ");

                //Validate the First Ratchet From Amount (MM) field
                string lbl1stRatchetFromAmt = form.ValidateFirstRatchetFromAmt();
                Assert.AreEqual("First Ratchet From Amount (MM)", lbl1stRatchetFromAmt);
                extentReports.CreateLog("Field with name : " + lbl1stRatchetFromAmt + " is displayed upon saving Transaction Fee Type as Incentive structure ");

                //Validate the First Ratchet To Amount (MM)
                string lbl1stRatchetToAmt = form.ValidateFirstRatchetToAmt();
                Assert.AreEqual("First Ratchet To Amount (MM)", lbl1stRatchetToAmt);
                extentReports.CreateLog("Field with name : " + lbl1stRatchetToAmt + " is displayed upon saving Transaction Fee Type as Incentive structure ");

                //Validate 2nd Ratchet Percent field
                string lbl2ndRatchetPer = form.Validate2ndRatchetPercent();
                Assert.AreEqual("Second Ratchet Percent", lbl2ndRatchetPer);
                extentReports.CreateLog("Field with name : " + lbl2ndRatchetPer + " is displayed upon saving Transaction Fee Type as Incentive structure ");

                //Validate the 2nd Ratchet From Amount (MM) field
                string lbl2ndRatchetFromAmt = form.Validate2ndRatchetFromAmt();
                Assert.AreEqual("Second Ratchet From Amount (MM)", lbl2ndRatchetFromAmt);
                extentReports.CreateLog("Field with name : " + lbl2ndRatchetFromAmt + " is displayed upon saving Transaction Fee Type as Incentive structure ");

                //Validate the 2nd Ratchet To Amount (MM)
                string lbl2ndRatchetToAmt = form.Validate2ndRatchetToAmt();
                Assert.AreEqual("Second Ratchet To Amount (MM)", lbl2ndRatchetToAmt);
                extentReports.CreateLog("Field with name : " + lbl2ndRatchetToAmt + " is displayed upon saving Transaction Fee Type as Incentive structure ");

                //Validate the Fee Comment, If applicable
                string lblFeeComment = form.ValidateFeeComments();
                Assert.AreEqual("Fee Comment, If applicable", lblFeeComment);
                extentReports.CreateLog("Field with name : " + lblFeeComment + " is displayed upon saving Transaction Fee Type as Incentive structure ");

                //Validate 3rd Ratchet Percent field
                string lbl3rdRatchetPer = form.Validate3rdRatchetPercent();
                Assert.AreEqual("Third Ratchet Percent", lbl3rdRatchetPer);
                extentReports.CreateLog("Field with name : " + lbl3rdRatchetPer + " is displayed upon saving Transaction Fee Type as Incentive structure ");

                //Validate the 3rd Ratchet From Amount (MM) field
                string lbl3rdRatchetFromAmt = form.Validate3rdRatchetFromAmt();
                Assert.AreEqual("Third Ratchet From Amount (MM)", lbl3rdRatchetFromAmt);
                extentReports.CreateLog("Field with name : " + lbl3rdRatchetFromAmt + " is displayed upon saving Transaction Fee Type as Incentive structure ");

                //Validate the 3rd Ratchet To Amount (MM)
                string lbl3rdRatchetToAmt = form.Validate3rdRatchetToAmt();
                Assert.AreEqual("Third Ratchet To Amount (MM)", lbl3rdRatchetToAmt);
                extentReports.CreateLog("Field with name : " + lbl3rdRatchetToAmt + " is displayed upon saving Transaction Fee Type as Incentive structure ");

                //Validate 4th Ratchet Percent field
                string lbl4thRatchetPer = form.Validate4thRatchetPercent();
                Assert.AreEqual("Fourth Ratchet Percent", lbl4thRatchetPer);
                extentReports.CreateLog("Field with name : " + lbl4thRatchetPer + " is displayed upon saving Transaction Fee Type as Incentive structure ");

                //Validate the 4th Ratchet From Amount (MM) field
                string lbl4thRatchetFromAmt = form.Validate4thRatchetFromAmt();
                Assert.AreEqual("Fourth Ratchet From Amount (MM)", lbl4thRatchetFromAmt);
                extentReports.CreateLog("Field with name : " + lbl4thRatchetFromAmt + " is displayed upon saving Transaction Fee Type as Incentive structure ");

                //Validate the 4th Ratchet To Amount (MM)
                string lbl4thRatchetToAmt = form.Validate4thRatchetToAmt();
                Assert.AreEqual("Fourth Ratchet To Amount (MM)", lbl4thRatchetToAmt);
                extentReports.CreateLog("Field with name : " + lbl4thRatchetToAmt + " is displayed upon saving Transaction Fee Type as Incentive structure ");

                //Validate Final Ratchet Percent
                string lblFinalRatchetPer = form.ValidateFinalRatchetPercent();
                Assert.AreEqual("Final Ratchet Percent", lblFinalRatchetPer);
                extentReports.CreateLog("Field with name : " + lblFinalRatchetPer + " is displayed upon saving Transaction Fee Type as Incentive structure ");

                //Validate Final Ratchet Amount (MM)
                string lblFinalRatchetAmt = form.ValidateFinalRatchetAmt();
                Assert.AreEqual("Final Ratchet Amount (MM)", lblFinalRatchetAmt);
                extentReports.CreateLog("Field with name : " + lblFinalRatchetAmt + " is displayed upon saving Transaction Fee Type as Incentive structure ");

                //Validate that no error is displayed when Incentive Fee fields are blank and saved               
                extentReports.CreateLog("No error is displayed when Incentive Fee is left blank and saved ");

                string valEstTxn = form.UpdateEstTransValue();
                Assert.AreEqual("", valEstTxn);
                extentReports.CreateLog("Estimated Transaction Value is updated with blank value ");

                form.UpdateReviewSubmissionOnly();

                //Validate the mandatory field validation for Estimated Transaction Value (MM) when it is not filled
                string valEst = form.GetValidationOfEstTransValue();
                Assert.AreEqual("Estimated Transaction Value (MM ) should be greater than 0", valEst);
                extentReports.CreateLog("Mandatory validation : " + valEst + " is displayed when Estimated Transaction Value (MM) field s not filled and saved ");

                //Update Review Submission and Base Fee and validate the Estimated Total Fee
                string fee = ReadExcelData.ReadData(excelPath, "NBCForm", 64);
                double actualFee = Convert.ToDouble(fee);
                string feeCred = ReadExcelData.ReadData(excelPath, "NBCForm", 65);
                double actualFeeCred = Convert.ToDouble(feeCred);

                string valBaseFee = form.UpdateReviewSubmissionAndBaseFee();
                Assert.AreEqual("GBP 10.00", valBaseFee);
                extentReports.CreateLog("Base Fee with value : " + valBaseFee + " is displayed upon saving ");
                string actBaseFee = (Convert.ToDouble((valBaseFee).Substring(4, 5))).ToString("0.0");
                Console.WriteLine("actBaseFee: " + actBaseFee);

                string valEstBaseFee = form.GetEstimatedTotalFeeForIncentive();
                Console.WriteLine("EstTotalFee: " + valEstBaseFee);
                Assert.AreEqual((((((actualFee - ((actualFee * actualFeeCred) / 100)) / 1000000) + ((actualFee - ((actualFee * actualFeeCred) / 100))))) + Convert.ToDouble(actBaseFee)).ToString("0.00"), (valEstBaseFee.Replace(",", "")));
                extentReports.CreateLog("Estimated Total Fee with value : " + valEstBaseFee + " is displayed when Base Fee in saved along with Retainer and Progress fields ");

                //Enter the value for 1st, 2nd, 3rd and 4th Ratchet pecent fields and validare validation for respective from and to amount fields
                string msg1stRatchetFromAmt = form.UpdateAllRatchetPercent();
                Assert.AreEqual("Enter To/From Ratchet amount; if final ratchet, please use Final Ratchet field", msg1stRatchetFromAmt);
                extentReports.CreateLog("Validation : " + msg1stRatchetFromAmt + " for 1st Ratchet From Amount is displayed when only 1st Ratchet percent is entered ");

                //Get 1st Ratchet To Amount validation
                string msg1stRatchetToAmt = form.Get1stRatchetToAmount();
                Assert.AreEqual("Enter To/From Ratchet amount; if final ratchet, please use Final Ratchet field", msg1stRatchetToAmt);
                extentReports.CreateLog("Validation: " + msg1stRatchetToAmt + " for 1st Ratchet To Amount is displayed when only 1st Ratchet percent is entered ");

                //Get 2nd Ratchet From Amount validation
                string msg2ndRatchetFromAmt = form.Get2ndRatchetFromAmount();
                Assert.AreEqual("Enter To/From Ratchet amount; if final ratchet, please use Final Ratchet field", msg2ndRatchetFromAmt);
                extentReports.CreateLog("Validation: " + msg2ndRatchetFromAmt + " for 2nd Ratchet To Amount is displayed when only 2nd Ratchet percent is entered ");

                //Get 2nd Ratchet To Amount validation
                string msg2ndRatchetToAmt = form.Get2ndRatchetToAmount();
                Assert.AreEqual("Enter To/From Ratchet amount; if final ratchet, please use Final Ratchet field", msg2ndRatchetToAmt);
                extentReports.CreateLog("Validation: " + msg2ndRatchetToAmt + "for 2nd Ratchet To Amount is displayed when only 2nd Ratchet percent is entered ");

                //Get 3rd Ratchet From Amount validation
                string msg3rdRatchetFromAmt = form.Get3rdRatchetFromAmount();
                Assert.AreEqual("Enter To/From Ratchet amount; if final ratchet, please use Final Ratchet field", msg3rdRatchetFromAmt);
                extentReports.CreateLog("Validation: " + msg3rdRatchetFromAmt + " for 3rd Ratchet To Amount is displayed when only 3rd Ratchet percent is entered ");

                //Get 3rd Ratchet To Amount validation
                string msg3rdRatchetToAmt = form.Get3rdRatchetToAmount();
                Assert.AreEqual("Enter To/From Ratchet amount; if final ratchet, please use Final Ratchet field", msg3rdRatchetToAmt);
                extentReports.CreateLog("Validation: " + msg3rdRatchetToAmt + "for 3rd Ratchet To Amount is displayed when only 3rd Ratchet percent is entered ");

                //Get 4th Ratchet From Amount validation
                string msg4tRatchetFromAmt = form.Get4thRatchetFromAmount();
                Assert.AreEqual("Enter To/From Ratchet amount; if final ratchet, please use Final Ratchet field", msg4tRatchetFromAmt);
                extentReports.CreateLog("Validation: " + msg4tRatchetFromAmt + " for 4th Ratchet To Amount is displayed when only 4th Ratchet percent is entered ");

                //Get 4th Ratchet To Amount validation
                string msg4thRatchetToAmt = form.Get4thRatchetToAmount();
                Assert.AreEqual("Enter To/From Ratchet amount; if final ratchet, please use Final Ratchet field", msg4thRatchetToAmt);
                extentReports.CreateLog("Validation: " + msg4thRatchetToAmt + " for 4th Ratchet To Amount is displayed when only 4th Ratchet percent is entered ");

                //Update all Ratchet From and To Amount as well
                string msg1stGreaterToAmount = form.UpdateAllRatchetFromAndToAmt();
                Assert.AreEqual("TO amount must be greater than FROM amount on Ratchets 1-4", msg1stGreaterToAmount);
                extentReports.CreateLog("Validation: " + msg1stGreaterToAmount + " for 1st Ratchet To Amount field is displayed when greater value for 1st Ratchet From Amount is entered ");

                //Get 2nd Ratchet To Amount greater validation
                string msg2ndGreaterToAmount = form.Get2ndRatchetToAmountGreaterValue();
                Assert.AreEqual("TO amount must be greater than FROM amount on Ratchets 1-4", msg2ndGreaterToAmount);
                extentReports.CreateLog("Validation: " + msg2ndGreaterToAmount + " for 2nd Ratchet To Amount field is displayed when greater value for 2nd Ratchet From Amount is entered ");

                //Get 3rd Ratchet To Amount greater validation
                string msg3rdGreaterToAmount = form.Get3rdRatchetToAmountGreaterValue();
                Assert.AreEqual("TO amount must be greater than FROM amount on Ratchets 1-4", msg3rdGreaterToAmount);
                extentReports.CreateLog("Validation: " + msg3rdGreaterToAmount + " for 3rd Ratchet To Amount field is displayed when greater value for 3rd Ratchet From Amount is entered ");

                //Get 4th Ratchet To Amount greater validation
                string msg4thGreaterToAmount = form.Get4thRatchetToAmountGreaterValue();
                Assert.AreEqual("TO amount must be greater than FROM amount on Ratchets 1-4", msg4thGreaterToAmount);
                extentReports.CreateLog("Validation: " + msg4thGreaterToAmount + " for 4th Ratchet To Amount field is displayed when greater value for 4th Ratchet From Amount is entered ");

                //Update 1st Ratchet amount fields and validate Estimated Total Fee when Estimated Transaction value is >= 1st Ratchet To Amount
                form.Update1stRatchetFromAndToAmt(fileTC1232);

                string valEst1stRatchetFee = form.GetEstimatedTotalFeeForIncentive();
                Console.WriteLine("EstTotalFee: " + valEst1stRatchetFee);
                string fromAmt = ReadExcelData.ReadData(excelPath, "NBCForm", 66);
                string ToAmt = ReadExcelData.ReadData(excelPath, "NBCForm", 67);
                double actualFromFee = Convert.ToDouble(fromAmt);
                double actualToFee = Convert.ToDouble(ToAmt);
                double finalRetainer = (actualFee - ((actualFee * actualFeeCred) / 100)) / 1000000;
                double finalProgress = (actualFee - ((actualFee * actualFeeCred) / 100));
                double final1stRatchet = (((actualToFee - actualFromFee) * actualFromFee) / 100);
                Console.WriteLine("finalRetainer" + finalRetainer);
                Console.WriteLine("finalProgress" + finalProgress);
                Console.WriteLine("final1stRatchet" + final1stRatchet);
                Assert.AreEqual(((Convert.ToDouble(finalRetainer + finalProgress + final1stRatchet)) + (Convert.ToDouble((valBaseFee).Substring(4, 5)))).ToString("0.00"), valEst1stRatchetFee.Replace(",", ""));
                extentReports.CreateLog("Estimated Total Fee with value : " + valEst1stRatchetFee + " is displayed when Estimated Transaction value is >= 1st Ratchet To Amount ");

                //Update Estimated Transaction value to a value which is greater than 1st Ratchet From amount and less than 1st Ratchet To amount
                string updEstValue = ReadExcelData.ReadData(excelPath, "NBCForm", 68);
                string updatedEstValue = form.UpdateEstimatedTransactionValue(updEstValue);
                Assert.AreEqual(updEstValue, updatedEstValue);
                extentReports.CreateLog("Estimated Transaction value: " + updatedEstValue + " is displayed post updation ");

                //Validate Estimated Total Fee
                string valEst1stRatchetWithUpdEstTxnValue = form.GetEstimatedTotalFeeForIncentive();
                Console.WriteLine("valEst1stRatchetWithUpdEstTxnValue: " + valEst1stRatchetWithUpdEstTxnValue);
                double final1stRatchetWithUpdEstTxnValue = (((Convert.ToDouble(updatedEstValue) - actualFromFee) * actualFromFee) / 100);
                Assert.AreEqual(((Convert.ToDouble(finalRetainer + finalProgress + final1stRatchetWithUpdEstTxnValue)) + (Convert.ToDouble((valBaseFee).Substring(4, 5)))).ToString("0.00"), valEst1stRatchetWithUpdEstTxnValue.Replace(",", ""));
                extentReports.CreateLog("Estimated Total Fee with value : " + valEst1stRatchetWithUpdEstTxnValue + " is displayed when Estimated Transaction value is entered greater than 1st Ratchet From amount and less than 1st Ratchet To amount along with Retainer and Progress fields ");

                //Validate Estimated Total Fee when Minimum Fee is greater than Estimated Total Fee (calculated from Retainer, Progressm, Base and Ratchet fee)
                string updatedMinFee = form.UpdateMinFeeValue();
                Assert.AreEqual("300000", updatedMinFee);
                extentReports.CreateLog("Estimated Total Fee: " + updatedMinFee + " is displayed same as Minimum Fee when Minimum Fee is greater than Estimated Total Fee (calculated from Retainer, Progressm, Base and Ratchet fee) ");

                //Update 2nd Ratchet amount fields and validate Estimated Total Fee when Estimated Transaction value is >= 2nd Ratchet To Amount
                form.Update2ndRatchetFromAndToAmt(fileTC1232);

                string valEst2ndRatchetFee = form.GetEstimatedTotalFeeForIncentive();
                Console.WriteLine("valEst2ndRatchetFee: " + valEst2ndRatchetFee);
                double final2ndRatchet = (((actualToFee - actualFromFee) * actualFromFee) / 100);
                Console.WriteLine("final2ndRatchet" + final2ndRatchet);
                Assert.AreEqual(((Convert.ToDouble(finalRetainer + finalProgress + final2ndRatchet)) + (Convert.ToDouble((valBaseFee).Substring(4, 5)))).ToString("0.00"), valEst2ndRatchetFee.Replace(",", ""));
                extentReports.CreateLog("Estimated Total Fee with value : " + valEst2ndRatchetFee + " is displayed when Estimated Transaction value is >= 2nd Ratchet To Amount ");

                //Update Estimated Transaction value to a value which is greater than 2nd Ratchet From amount and less than 2nd Ratchet To amount
                string updatedEstValueFor2ndRatchet = form.UpdateEstimatedTransactionValue(updEstValue);
                Assert.AreEqual(updEstValue, updatedEstValueFor2ndRatchet);
                extentReports.CreateLog("Estimated Transaction value: " + updatedEstValueFor2ndRatchet + " is displayed post updation ");

                //Validate Estimated Total Fee
                string valEst2ndRatchetWithUpdEstTxnValue = form.GetEstimatedTotalFeeForIncentive();
                Console.WriteLine("valEst2ndRatchetWithUpdEstTxnValue: " + valEst1stRatchetWithUpdEstTxnValue);
                double final2ndRatchetWithUpdEstTxnValue = (((Convert.ToDouble(updatedEstValue) - actualFromFee) * actualFromFee) / 100);
                Assert.AreEqual(((Convert.ToDouble(finalRetainer + finalProgress + final2ndRatchetWithUpdEstTxnValue)) + (Convert.ToDouble((valBaseFee).Substring(4, 5)))).ToString("0.00"), valEst2ndRatchetWithUpdEstTxnValue.Replace(",", ""));
                extentReports.CreateLog("Estimated Total Fee with value : " + valEst2ndRatchetWithUpdEstTxnValue + " is displayed when Estimated Transaction value is entered greater than 2nd Ratchet From amount and less than 2nd Ratchet To amount along with Retainer and Progress fields ");

                //Update 3rd Ratchet amount fields and validate Estimated Total Fee when Estimated Transaction value is >= 3rd Ratchet To Amount
                form.Update3rdRatchetFromAndToAmt(fileTC1232);

                string valEst3rdRatchetFee = form.GetEstimatedTotalFeeForIncentive();
                Console.WriteLine("valEst3rdRatchetFee: " + valEst3rdRatchetFee);
                double final3rdRatchet = (((actualToFee - actualFromFee) * actualFromFee) / 100);
                Console.WriteLine("final3rdRatchet" + final3rdRatchet);
                Assert.AreEqual(((Convert.ToDouble(finalRetainer + finalProgress + final3rdRatchet)) + (Convert.ToDouble((valBaseFee).Substring(4, 5)))).ToString("0.00"), valEst3rdRatchetFee.Replace(",", ""));
                extentReports.CreateLog("Estimated Total Fee with value : " + valEst3rdRatchetFee + " is displayed when Estimated Transaction value is >= 3rd Ratchet To Amount ");

                //Update Estimated Transaction value to a value which is greater than 3rd Ratchet From amount and less than 3rd Ratchet To amount
                string updatedEstValueFor3rdRatchet = form.UpdateEstimatedTransactionValue(updEstValue);
                Assert.AreEqual(updEstValue, updatedEstValueFor3rdRatchet);
                extentReports.CreateLog("Estimated Transaction value: " + updatedEstValueFor3rdRatchet + " is displayed post updation ");

                //Validate Estimated Total Fee
                string valEst3rdRatchetWithUpdEstTxnValue = form.GetEstimatedTotalFeeForIncentive();
                Console.WriteLine("valEst3rdRatchetWithUpdEstTxnValue: " + valEst3rdRatchetWithUpdEstTxnValue);
                double final3rdRatchetWithUpdEstTxnValue = (((Convert.ToDouble(updatedEstValue) - actualFromFee) * actualFromFee) / 100);
                Assert.AreEqual(((Convert.ToDouble(finalRetainer + finalProgress + final3rdRatchetWithUpdEstTxnValue)) + (Convert.ToDouble((valBaseFee).Substring(4, 5)))).ToString("0.00"), valEst3rdRatchetWithUpdEstTxnValue.Replace(",", ""));
                extentReports.CreateLog("Estimated Total Fee with value : " + valEst3rdRatchetWithUpdEstTxnValue + " is displayed when Estimated Transaction value is entered greater than 3rd Ratchet From amount and less than 3rd Ratchet To amount along with Retainer and Progress fields ");

                //Update 4th Ratchet amount fields and validate Estimated Total Fee when Estimated Transaction value is >= 4th Ratchet To Amount
                form.Update4thRatchetFromAndToAmt(fileTC1232);

                string valEst4thRatchetFee = form.GetEstimatedTotalFeeForIncentive();
                Console.WriteLine("valEst4thRatchetFee: " + valEst4thRatchetFee);
                double final4thRatchet = (((actualToFee - actualFromFee) * actualFromFee) / 100);
                Console.WriteLine("final4thRatchet" + final4thRatchet);
                Assert.AreEqual(((Convert.ToDouble(finalRetainer + finalProgress + final4thRatchet)) + (Convert.ToDouble((valBaseFee).Substring(4, 5)))).ToString("0.00"), valEst4thRatchetFee.Replace(",", ""));
                extentReports.CreateLog("Estimated Total Fee with value : " + valEst4thRatchetFee + " is displayed when Estimated Transaction value is >= 4th Ratchet To Amount ");

                //Update Estimated Transaction value to a value which is greater than 4th Ratchet From amount and less than 4th Ratchet To amount
                string updatedEstValueFor4thRatchet = form.UpdateEstimatedTransactionValue(updEstValue);
                Assert.AreEqual(updEstValue, updatedEstValueFor4thRatchet);
                extentReports.CreateLog("Estimated Transaction value: " + updatedEstValueFor4thRatchet + " is displayed post updation ");

                //Validate Estimated Total Fee
                string valEst4thRatchetWithUpdEstTxnValue = form.GetEstimatedTotalFeeForIncentive();
                Console.WriteLine("valEst4thRatchetWithUpdEstTxnValue: " + valEst4thRatchetWithUpdEstTxnValue);
                double final4thRatchetWithUpdEstTxnValue = (((Convert.ToDouble(updatedEstValue) - actualFromFee) * actualFromFee) / 100);
                Assert.AreEqual(((Convert.ToDouble(finalRetainer + finalProgress + final4thRatchetWithUpdEstTxnValue)) + (Convert.ToDouble((valBaseFee).Substring(4, 5)))).ToString("0.00"), valEst4thRatchetWithUpdEstTxnValue.Replace(",", ""));
                extentReports.CreateLog("Estimated Total Fee with value : " + valEst4thRatchetWithUpdEstTxnValue + " is displayed when Estimated Transaction value is entered greater than 4th Ratchet From amount and less than 4th Ratchet To amount along with Retainer and Progress fields ");

                //Update Final Ratchet amount fields and validate Estimated Total Fee when Estimated Transaction value is >= Final Ratchet Amount
                string updEstValueForFinalRatchet = form.UpdateFinalRatchetAmt(fileTC1232);
                double actualEstValueForFinalRatchet = Convert.ToDouble(updEstValueForFinalRatchet);

                string valEstFinalRatchetFee = form.GetEstimatedTotalFeeForIncentive();
                Console.WriteLine("valEstFinalRatchetFee: " + valEstFinalRatchetFee);
                double finalFinalRatchet = (((actualEstValueForFinalRatchet - actualFromFee) * actualFromFee) / 100);
                Console.WriteLine("finalFinalRatchet" + finalFinalRatchet);
                Assert.AreEqual(((Convert.ToDouble(finalRetainer + finalProgress + finalFinalRatchet)) + (Convert.ToDouble((valBaseFee).Substring(4, 5)))).ToString("0.00"), valEstFinalRatchetFee.Replace(",", ""));
                extentReports.CreateLog("Estimated Total Fee with value : " + valEstFinalRatchetFee + " is displayed when Estimated Transaction value is >= Final Ratchet Amount ");

                //Update Estimated Transaction value to a value which is less than Final Ratchet amount
                string updatedEstValueForFinalRatchet = form.UpdateEstimatedTransactionValue(feeCred);
                Assert.AreEqual(feeCred, updatedEstValueForFinalRatchet);
                extentReports.CreateLog("Estimated Transaction value: " + updatedEstValueForFinalRatchet + " is displayed post updation ");

                //Validate Estimated Total Fee
                string valEstFinalRatchetWithUpdEstTxnValue = form.GetEstimatedTotalFeeForIncentive();
                Console.WriteLine("valEstFinalRatchetWithUpdEstTxnValue: " + valEstFinalRatchetWithUpdEstTxnValue);
                //double finalFinalRatchetWithUpdEstTxnValue = (((Convert.ToDouble(updatedEstValueForFinalRatchet) - actualFromFee) * actualFromFee) / 100);
                Assert.AreEqual(((Convert.ToDouble(finalRetainer + finalProgress)) + (Convert.ToDouble((valBaseFee).Substring(4, 5)))).ToString("0.00"), valEstFinalRatchetWithUpdEstTxnValue.Replace(",", ""));
                extentReports.CreateLog("Estimated Total Fee with value : " + valEstFinalRatchetWithUpdEstTxnValue + " is displayed when Estimated Transaction value is entered less than Final Ratchet amount along with Retainer and Progress fields ");

                //Update all Ratchet values i.e., 1st, 2nd, 3rd and 4th
                string updEstAllRatchets = form.UpdateAllRatchetValues(fileTC1232);
                Console.WriteLine("updEstAllRatchets: " + updEstAllRatchets);

                //Validate Estimated Total Fee when all Ratchet values are saved               
                Assert.AreEqual(((Convert.ToDouble(finalRetainer + finalProgress + final1stRatchet + final2ndRatchet + final3rdRatchet + final4thRatchet + finalFinalRatchet)) + (Convert.ToDouble((valBaseFee).Substring(4, 5)))).ToString("0.00"), updEstAllRatchets.Replace(",", ""));
                extentReports.CreateLog("Estimated Total Fee with value : " + updEstAllRatchets + " is displayed when all Ratchet amounts are entered along with Retainer and Progress fields ");



                form.SwitchFrame();
                usersLogin.UserLogOut();

                usersLogin.UserLogOut();
                driver.Quit();

            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }

        }
    }
}



