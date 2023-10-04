using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunity
{
    class TMTT0013844_CNBCForm_RequiredFields_SubmitForReview : BaseClass
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

        public static string fileCNBC = "CNBC_EndToEnd.xlsx";

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
                string value = addOpportunity.AddOpportunities(valJobType, fileCNBC);
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileCNBC);
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
                opportunityDetails.UpdateInternalTeamDetails(fileCNBC);

                //Logout of user and validate Admin login
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //update CC and NBC checkboxes 
                opportunityDetails.UpdateOutcomeDetails(fileCNBC);
                opportunityDetails.UpdateCCOnly();
                opportunityDetails.UpdatePitchDate();

                //Login as Standard User and validate the user
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //Click on NBC and validate title of page
                string title = opportunityDetails.ClickNBCFormLCNBC();
                Assert.AreEqual("Opportunity Overview", title);
                extentReports.CreateLog("CNBC Form page is displayed with default tab : " + title + " ");

                //Validate pre populated fields on CNBC form               
                string oppCNBC = form.ValidateOppName();
                Assert.AreEqual(oppNum, oppCNBC);
                extentReports.CreateLog("Opportunity Name: " + oppCNBC + " in CNBC form matches with Opportunity details page ");

                string clientComp = form.ValidateClientCompanyHeader();
                Assert.AreEqual("Client Company", clientComp);
                extentReports.CreateLog("Field with name " + clientComp + " is displayed on the header of CNBC page ");

                string clientOwner = form.ValidateClientOwnershipHeader();
                Assert.AreEqual("Client Ownership", clientOwner);
                extentReports.CreateLog("Field with name " + clientOwner + " is displayed on the header of CNBC page ");

                string subComp = form.ValidateSubjectCompanyHeader();
                Assert.AreEqual("Subject Company", subComp);
                extentReports.CreateLog("Field with name " + subComp + " is displayed on the header of CNBC page ");

                string subOwner = form.ValidateSubjectOwnershipHeader();
                Assert.AreEqual("Subject Ownership", subOwner);
                extentReports.CreateLog("Field with name " + subOwner + " is displayed on the header of CNBC page ");

                string jobTypeHeader = form.ValidateJobTypeHeader();
                Assert.AreEqual("Job Type", jobTypeHeader);
                extentReports.CreateLog("Field with name " + jobTypeHeader + " is displayed on the header of CNBC page ");

                string indGroup = form.ValidateIGHeader();
                Assert.AreEqual("Primary Industry Group", indGroup);
                extentReports.CreateLog("Field with name " + indGroup + " is displayed on the header of CNBC page ");

                string clientNBC = form.ValidateClient();
                Assert.AreEqual(clientName, clientNBC);
                extentReports.CreateLog("Client Company: " + clientNBC + " in CNBC form matches with Opportunity details page ");

                string valClientOwner = form.ValidateClientOwnership();
                Assert.AreEqual(clientOwnership, valClientOwner);
                extentReports.CreateLog("Client Ownership: " + valClientOwner + " in CNBC form matches with Opportunity details page ");

                string subjectNBC = form.ValidateSubject();
                Assert.AreEqual(subjectName, subjectNBC);
                extentReports.CreateLog("Subject Company: " + subjectNBC + " in CNBC form matches with Opportunity details page ");

                string valSubjectOwner = form.ValidateSubjectOwnership();
                Assert.AreEqual(subjectOwnership, valSubjectOwner);
                extentReports.CreateLog("Subject Ownership: " + valSubjectOwner + " in CNBC form matches with Opportunity details page ");

                string jobTypeNBC = form.ValidateJobType();
                Assert.AreEqual(jobType, jobTypeNBC);
                extentReports.CreateLog("Job Type: " + jobTypeNBC + " in CNBC form matches with Opportunity details page ");

                string valIG = form.ValidateIG();
                Assert.AreEqual(IG, valIG);
                extentReports.CreateLog("Industry Group: " + valIG + " in CNBC form matches with Opportunity details page ");

                //Select the Review Submission button
                nform.ClickReviewSubmission();

                //Validate mandatory validations for Opportunity Overview tab 
                string txtOppOverview = nform.ClickOpportunityOverview();
                Assert.AreEqual("Opportunity Overview", txtOppOverview);
                extentReports.CreateLog("Tab with name " + txtOppOverview + " is displayed upon clicking the tab. ");

                string actTransOverValidation = nform.GetFieldsValidationsOfOppOverview();
                Console.WriteLine(actTransOverValidation);
                string expTransOverValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 1);
                Assert.AreEqual(expTransOverValidation, actTransOverValidation);
                extentReports.CreateLog("Validation: " + actTransOverValidation + " is displayed for Transaction Overview field ");

                string actCurrentStatusVal = nform.GetValidationOfCurrentStatus();
                Console.WriteLine(actCurrentStatusVal);
                string expCurrentStatusVal = ReadExcelData.ReadData(excelPath, "NBCForm", 35);
                Assert.AreEqual(expCurrentStatusVal, actCurrentStatusVal);
                extentReports.CreateLog("Validation: " + actCurrentStatusVal + " is displayed for Current Status field ");

                string actAsiaAngleVal = nform.GetValidationOfAsiaAngle();
                string expAsiaAngleVal = ReadExcelData.ReadData(excelPath, "NBCForm", 40);
                Assert.AreEqual(expAsiaAngleVal, actAsiaAngleVal);
                extentReports.CreateLog("Validation: " + actAsiaAngleVal + " is displayed for Asia Angle field ");

                string actRealEstateVal = nform.GetValidationOfRealEstateAngle();
                string expRealEstateVal = ReadExcelData.ReadData(excelPath, "NBCForm", 38);
                Assert.AreEqual(expRealEstateVal, actRealEstateVal);
                extentReports.CreateLog("Validation: " + actRealEstateVal + " is displayed for Real Estate Angle field ");

                string actIntAngleVal = nform.GetValidationOfInternationalAngle();
                string expIntAngleVal = ReadExcelData.ReadData(excelPath, "NBCForm", 42);
                Assert.AreEqual(expIntAngleVal, actIntAngleVal);
                extentReports.CreateLog("Validation: " + actIntAngleVal + " is displayed for International Angle field ");

                string actRiskFactVal = nform.GetValidationOfRiskFactors();
                string expRiskFactVal = ReadExcelData.ReadData(excelPath, "NBCForm", 41);
                Assert.AreEqual(expRiskFactVal, actRiskFactVal);
                extentReports.CreateLog("Validation: " + actRiskFactVal + " is displayed for Risk Factors field ");

                string actExistingOrRepeatValidation = nform.GetValidationOfExistingOrRepeatClient();
                string expExistingOrRepeatValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 55);
                Assert.AreEqual(expExistingOrRepeatValidation, actExistingOrRepeatValidation);
                extentReports.CreateLog("Validation: " + actExistingOrRepeatValidation + " is displayed for Existing or Repeat Client? field ");

                string actExistingRelValidation = nform.GetValidationOfExistingRel();
                string expExistingRelValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 54);
                Assert.AreEqual(expExistingRelValidation, actExistingRelValidation);
                extentReports.CreateLog("Validation: " + actExistingRelValidation + " is displayed for Existing Relationships field ");

                string actHLCompValidation = nform.GetValidationOfHLComp();
                string expHLCompValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 52);
                Assert.AreEqual(expHLCompValidation, actHLCompValidation);
                extentReports.CreateLog("Validation: " + actHLCompValidation + " is displayed for HL Competition field ");

                string actOwnershipVal = nform.GetValidationOfOwnershipAndCapStr();
                string expOwnershipVal = ReadExcelData.ReadData(excelPath, "NBCForm", 39);
                Assert.AreEqual(expOwnershipVal, actOwnershipVal);
                extentReports.CreateLog("Validation: " + actOwnershipVal + " is displayed for Ownership and Capital Structure field ");

                string actTotalDebt = nform.GetValidationOfTotalDebt();
                Console.WriteLine(actTotalDebt);
                string expTotalDebtVal = ReadExcelData.ReadData(excelPath, "NBCForm", 33);
                Assert.AreEqual(expTotalDebtVal, actTotalDebt);
                extentReports.CreateLog("Validation: " + actTotalDebt + " is displayed for Total Debt field ");

                string actUseOfProceeds = form.GetValidationOfUseOfProceeds();
                string expUseOfProceeds = ReadExcelData.ReadData(excelPath, "NBCForm", 65);
                Assert.AreEqual(expUseOfProceeds, actUseOfProceeds);
                extentReports.CreateLog("Validation: " + actUseOfProceeds + " is displayed for Use Of Proceeds field ");

                string actOwnershipValidation = form.GetValidationOfStructureAndPricing();
                string expOwnershipValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 64);
                Assert.AreEqual(expOwnershipValidation, actOwnershipValidation);
                extentReports.CreateLog("Validation: " + actOwnershipValidation + " is displayed for Ownership Structure & Capital Structure field ");

                string actSanctionsValidation = form.GetValidationOfSanctionsConcerns();
                string expSanctionsValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 70);
                Assert.AreEqual(expSanctionsValidation, actSanctionsValidation);
                extentReports.CreateLog("Validation: " + actSanctionsValidation + " is displayed for Ownership Structure & Capital Structure field ");
                               
                string actGroupValidation = nform.GetValidationOfGroupHeadApproval();
                string expGroupValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 63);
                Assert.AreEqual(expGroupValidation, actGroupValidation);
                extentReports.CreateLog("Validation: " + actGroupValidation + " is displayed for Group Head Approval field ");

                //Click Financials tab and validate its mandatory validations 
                string txtFinancials = nform.ClickFinancialsTab();
                Assert.AreEqual("Financials", txtFinancials);
                extentReports.CreateLog("Tab with name " + txtFinancials + " is displayed upon clicking Financials tab. ");

                string actFinSubjectValidation = nform.GetValidationOfFinancialsSubject();
                string expFinSubjectValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 45);
                Assert.AreEqual(expFinSubjectValidation, actFinSubjectValidation);
                extentReports.CreateLog("Validation: " + actFinSubjectValidation + " is displayed for Financials Subject to Audit field ");

                string actNoFinValidation = form.GetValidationOfCapitalRaise();
                string expNoFinValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 66);
                Assert.AreEqual(expNoFinValidation, actNoFinValidation);
                extentReports.CreateLog("Validation: " + actNoFinValidation + " is displayed for No Financials field ");

                //Click Fees tab and validate its mandatory validations 
                string txtFees = nform.ClickFeesTab();
                Assert.AreEqual("Fees", txtFees);
                extentReports.CreateLog("Tab with name " + txtFees + " is displayed upon clicking Fees tab. ");

                string actRetainerFeeValidation = nform.GetValidationOfRetainerFee();
                string expRetainerFeeValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 48);
                Assert.AreEqual(expRetainerFeeValidation, actRetainerFeeValidation);
                extentReports.CreateLog("Validation: " + actRetainerFeeValidation + " is displayed for Retainer Fee field ");

                string actProgressFeeValidation = nform.GetValidationOfProgressFee();
                string expProgressFeeValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 49);
                Assert.AreEqual(expProgressFeeValidation, actProgressFeeValidation);
                extentReports.CreateLog("Validation: " + actProgressFeeValidation + " is displayed for Progress Fee field ");

                string actMinFeeValidation = nform.GetValidationOfMinFee();
                string expMinFeeValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 71);
                Assert.AreEqual(expMinFeeValidation, actMinFeeValidation);
                extentReports.CreateLog("Validation: " + actMinFeeValidation + " is displayed for Minimum Fee field ");
                                             
                string actEstFeeValidation = form.GetValidationOfEstimatedFee();
                string expEstFeeValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 67);
                Assert.AreEqual(actEstFeeValidation, expEstFeeValidation);
                extentReports.CreateLog("Validation: " + expEstFeeValidation + " is displayed for Estimated Fee field ");

                string actLockupsValidation = nform.GetValidationOfLockups();
                string expLockupsValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 53);
                Assert.AreEqual(expLockupsValidation, actLockupsValidation);
                extentReports.CreateLog("Validation: " + actLockupsValidation + " is displayed for Lockups on Future M&A on Financing work field ");

                string actRefFeeValidation = form.GetValidationOfReferralFee();
                string expRefFeeValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 68);
                Assert.AreEqual(expRefFeeValidation, actRefFeeValidation);
                extentReports.CreateLog("Validation: " + actRefFeeValidation + " is displayed for Referral Fee Owned field ");

                //Click Administrative tab and validate its mandatory validations 
                string txtAdmin = form.ClickAdministrativeTab();
                Assert.AreEqual("Administrative", txtAdmin);
                extentReports.CreateLog("Tab with name " + txtAdmin + " is displayed upon clicking Administrative tab. ");

                string actRestrictedValidation = form.GetValidationOfRestrictedList();
                string expRestrictedValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 69);
                Assert.AreEqual(expRestrictedValidation, actRestrictedValidation);
                extentReports.CreateLog("Validation: " + actRestrictedValidation + " is displayed for Restricted List field ");

                //Save all the mandatory fields details in all tabs.
                nform.ClickOpportunityOverview();
                form.SaveAllReqFieldsInOppOverview(fileCNBC);

                nform.ClickFinancialsTab();
                form.SaveAllReqFieldsInFinancials(fileCNBC);

                nform.ClickFeesTab();
                form.SaveAllReqFieldsInFees(fileCNBC);

                form.ClickAdministrativeTab();
                form.SaveAllReqFieldsInAdministrative(fileCNBC);

                form.UpdateNextSchCall();

                //Validate title of Email Template page
                string pageTitle = form.ClickSubmitButton();
                Assert.AreEqual("Send Email", pageTitle);
                extentReports.CreateLog(pageTitle + " page is displayed upon clicking Submit for Review ");

                //Validate Opportunity Name in Email and navigate to Opportunity details page
                string emailOppName = form.GetOpportunityName();
                Assert.AreEqual(value, emailOppName);
                extentReports.CreateLog(" Email Template with Opportunity " + emailOppName + " is displayed ");
                form.SwitchFrame();       

                usersLogin.UserLogOut();
                //Login as CAO User i.e., Brian Miller                
                string valCAOUser = ReadExcelData.ReadData(excelPath, "Users", 2);
                usersLogin.SearchUserAndLogin(valCAOUser);
                string caoUser = login.ValidateUser();
                Assert.AreEqual(caoUser.Contains(valCAOUser), true);
                extentReports.CreateLog("User: " + caoUser + " logged in ");

                //Search for the same Opportunity                
                opportunityHome.SearchOpportunity(value);               
                opportunityDetails.ClickNBCFormLCNBC();

                //Validate Submit for Review button before approval
                string submit =form.ValidateSubmitForReviewButton();
                Assert.AreEqual("Submit for Review button is displayed", submit);
                extentReports.CreateLog(submit + " for CAO user before approval ");

                //Click on Review tab
                string tabName= form.ClickReviewTab();
                Assert.AreEqual("Review", tabName);
                extentReports.CreateLog("Tab with name: "+tabName + " is displayed upon clicking Review tab ");

                //Update Grade and validate the same
                string grade= form.UpdateGrade();
                Assert.AreEqual("A+", grade);
                extentReports.CreateLog("Grade with value: " + grade + " is updated ");

                //Validate Submit for Review button before approval
                string submit1 = form.ValidateSubmitForReviewButton();
                Assert.AreEqual("Submit for Review button is not displayed", submit1);
                extentReports.CreateLog(submit1 + " for CAO user after approval ");
                form.SwitchFrame();
                usersLogin.UserLogOut();

                //Login as Admin and validate NBC Approved checkbox
                //Search for the same Opportunity                
                opportunityHome.SearchOpportunity(value);
                string check = opportunityDetails.ValidateNBCApprovedCheckbox();
                Assert.AreEqual("NBC Approved checkbox is checked", check);
                extentReports.CreateLog(check + " after approving CNBC Form ");

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
    



