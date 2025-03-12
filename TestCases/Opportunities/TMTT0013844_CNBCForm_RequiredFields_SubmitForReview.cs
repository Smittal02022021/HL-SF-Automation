using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
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
                string opportunityNumber = addOpportunity.AddOpportunitiesLightning(valJobType, fileCNBC);
                Console.WriteLine("value : " + opportunityNumber);
                extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileCNBC);
                Assert.AreEqual("Info", displayedTab);
                extentReports.CreateLog("Tab with name: " + displayedTab + " is displayed upon saving internal deal team members details ");

                string clientName = opportunityDetails.GetClientCompanyL();
                string subjectName= opportunityDetails.GetSubjectCompanyL();                
                string jobType = opportunityDetails.GetJobTypeL();
                opportunityDetails.UpdateClientSubjectOwnershipL();
                string clientOwnership = opportunityDetails.GetClientOwnershipLPostUpdate();
                string subjectOwnership = opportunityDetails.GetSubjectOwnershipLPostUpdate();

                //Logout of user and validate Admin login
                usersLogin.DiffLightningLogout();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(opportunityNumber);

                //update CC and NBC checkboxes 
                opportunityDetails.UpdateInternalTeamDetails(fileCNBC);
                opportunityDetails.UpdateOutcomeDetails(fileCNBC);
                opportunityDetails.UpdateCCOnly();
                opportunityDetails.UpdatePitchDate();

                //Login as Standard User and validate the user
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUserLightning();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchMyOpportunitiesInLightning(opportunityNumber, valUser);
                string oppNumber = opportunityDetails.GetOpportunityNumberLightning();

                //Click on NBC and validate title of page
                string title = opportunityDetails.ClickNBCFormLCNBC();
                Assert.AreEqual("Opportunity Overview", title);
                extentReports.CreateLog("CNBC Form page is displayed with default tab : " + title + " ");

                //Validate pre populated fields on CNBC form               
                string oppCNBC = form.ValidateOppNameL();               
                Assert.AreEqual(oppNumber, oppCNBC);
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

                //string valIG = form.ValidateIG();
                //Assert.AreEqual(IG, valIG);
                //extentReports.CreateLog("Industry Group: " + valIG + " in CNBC form matches with Opportunity details page ");

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
                Assert.AreEqual("Current Status\r\nOpportunity Overview: Current Status", actCurrentStatusVal);
                extentReports.CreateLog("Validation: " + actCurrentStatusVal + " is displayed for Current Status field ");

                string actAsiaAngleVal = nform.GetValidationOfAsiaAngle();
                string expAsiaAngleVal = ReadExcelData.ReadData(excelPath, "NBCForm", 40);
                Assert.AreEqual("Asia Angle\r\nOpportunity Overview: Asia Angle", actAsiaAngleVal);
                extentReports.CreateLog("Validation: " + actAsiaAngleVal + " is displayed for Asia Angle field ");

                string actRealEstateVal = nform.GetValidationOfRealEstateAngle();
                string expRealEstateVal = ReadExcelData.ReadData(excelPath, "NBCForm", 38);
                Assert.AreEqual("Real Estate Angle\r\nOpportunity Overview: Real Estate Angle", actRealEstateVal);
                extentReports.CreateLog("Validation: " + actRealEstateVal + " is displayed for Real Estate Angle field ");

                string actIntAngleVal = nform.GetValidationOfInternationalAngle();
                string expIntAngleVal = ReadExcelData.ReadData(excelPath, "NBCForm", 42);
                Assert.AreEqual("International Angle?\r\nOpportunity Overview: Cross-border Angle", actIntAngleVal);
                extentReports.CreateLog("Validation: " + actIntAngleVal + " is displayed for International Angle field ");

                string actRiskFactVal = nform.GetValidationOfRiskFactors();
                string expRiskFactVal = ReadExcelData.ReadData(excelPath, "NBCForm", 41);
                Assert.AreEqual(expRiskFactVal, actRiskFactVal);
                extentReports.CreateLog("Validation: " + actRiskFactVal + " is displayed for Risk Factors field ");

                string actExistingOrRepeatValidation = nform.GetValidationOfExistingOrRepeatClient();
                string expExistingOrRepeatValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 55);
                Assert.AreEqual("Existing or Repeat Client?\r\nPre-Pitch: Existing or Repeat Client?", actExistingOrRepeatValidation);
                extentReports.CreateLog("Validation: " + actExistingOrRepeatValidation + " is displayed for Existing or Repeat Client? field ");

                string actExistingRelValidation = nform.GetValidationOfExistingRel();
                string expExistingRelValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 54);
                Assert.AreEqual("Existing Relationships\r\nPre-Pitch: Have you checked Salesforce for existing relationships?", actExistingRelValidation);
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
                Assert.AreEqual("Total Debt (MM)\r\nOpportunity Overview: Total Debt(MM)", actTotalDebt);
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
                Assert.AreEqual("Sanctions Concerns/Issues?\r\nOverview and Financials: Sanctions concerns/issues?", actSanctionsValidation);
                extentReports.CreateLog("Validation: " + actSanctionsValidation + " is displayed for Ownership Structure & Capital Structure field ");
                               
                string actGroupValidation = nform.GetValidationOfGroupHeadApproval();
                string expGroupValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 63);
                Assert.AreEqual("Group Head Approval\r\nOpportunity Overview: Please confirm that a group head has approved prior to submitting to the committee.", actGroupValidation);
                extentReports.CreateLog("Validation: " + actGroupValidation + " is displayed for Group Head Approval field ");

                //Click Financials tab and validate its mandatory validations 
                string txtFinancials = nform.ClickFinancialsTab();
                Assert.AreEqual("Financials", txtFinancials);
                extentReports.CreateLog("Tab with name " + txtFinancials + " is displayed upon clicking Financials tab. ");

                string actFinSubjectValidation = nform.GetValidationOfFinancialsSubject();
                string expFinSubjectValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 45);
                Assert.AreEqual("Financials Subject to Audit\r\nFinancials: Have the financials been subject to an audit?", actFinSubjectValidation);
                extentReports.CreateLog("Validation: " + actFinSubjectValidation + " is displayed for Financials Subject to Audit field ");

                string actNoFinValidation = form.GetValidationOfCapitalRaise();
                string expNoFinValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 66);
                Assert.AreEqual("Capital Raise (MM)\r\nFinancials : Capital Raise (MM)", actNoFinValidation);
                extentReports.CreateLog("Validation: " + actNoFinValidation + " is displayed for No Financials field ");

                //Click Fees tab and validate its mandatory validations 
                string txtFees = nform.ClickFeesTab();
                Assert.AreEqual("Fees", txtFees);
                extentReports.CreateLog("Tab with name " + txtFees + " is displayed upon clicking Fees tab. ");

                string actRetainerFeeValidation = nform.GetValidationOfRetainerFee();
                string expRetainerFeeValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 48);
                Assert.AreEqual("Retainer Fee Creditable ?\r\nThe value can't be null for 'Retainer Fee Creditable'", actRetainerFeeValidation);
                extentReports.CreateLog("Validation: " + actRetainerFeeValidation + " is displayed for Retainer Fee field ");

                string actProgressFeeValidation = nform.GetValidationOfProgressFee();
                string expProgressFeeValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 49);
                Assert.AreEqual("Progress Fee Creditable ?\r\nThe value can't be null for 'Progress Fee Creditable ?'", actProgressFeeValidation);
                extentReports.CreateLog("Validation: " + actProgressFeeValidation + " is displayed for Progress Fee field ");

                string actMinFeeValidation = nform.GetValidationOfMinFee();
                string expMinFeeValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 71);
                Assert.AreEqual("Engagement Letter Minimum Fee (MM)\r\nEngagement Letter Minimum Fee (MM) is required.", actMinFeeValidation);
                extentReports.CreateLog("Validation: " + actMinFeeValidation + " is displayed for Minimum Fee field ");
                                             
                string actEstFeeValidation = form.GetValidationOfEstimatedFee();
                string expEstFeeValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 67);
                Assert.AreEqual("Fees: Estimated Fee (MM)", expEstFeeValidation);
                extentReports.CreateLog("Validation: " + expEstFeeValidation + " is displayed for Estimated Fee field ");

                string actLockupsValidation = nform.GetValidationOfLockups();
                string expLockupsValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 53);
                Assert.AreEqual("Lockups on Future M&A or Financing Work\r\nPre-Pitch: Lockups on Future M&A or Financing Work", actLockupsValidation);
                extentReports.CreateLog("Validation: " + actLockupsValidation + " is displayed for Lockups on Future M&A on Financing work field ");

                string actRefFeeValidation = form.GetValidationOfReferralFee();
                string expRefFeeValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 68);
                Assert.AreEqual("Referral Fee Owed (MM)\r\nFees: Referral Fee Owed", actRefFeeValidation);
                extentReports.CreateLog("Validation: " + actRefFeeValidation + " is displayed for Referral Fee Owned field ");

                //Click Administrative tab and validate its mandatory validations 
                string txtAdmin = form.ClickAdministrativeTab();
                Assert.AreEqual("Administrative", txtAdmin);
                extentReports.CreateLog("Tab with name " + txtAdmin + " is displayed upon clicking Administrative tab. ");

                string actRestrictedValidation = form.GetValidationOfRestrictedList();
                string expRestrictedValidation = ReadExcelData.ReadData(excelPath, "NBCForm", 69);
                Assert.AreEqual("Restricted List\r\nAdministrative: Restricted List?", actRestrictedValidation);
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
                Assert.AreEqual(opportunityNumber, emailOppName);
                extentReports.CreateLog(" Email Template with Opportunity " + emailOppName + " is displayed ");
                form.SwitchFrame();       

                usersLogin.DiffLightningLogout();
                //Login as CAO User i.e., Brian Miller                
                string valCAOUser = ReadExcelData.ReadData(excelPath, "Users", 2);
                usersLogin.SearchUserAndLogin(valCAOUser);
                string caoUser = login.ValidateUserLightning();
                Assert.AreEqual(caoUser.Contains(valCAOUser), true);
                extentReports.CreateLog("User: " + caoUser + " logged in ");

                //Search for the same Opportunity                
                opportunityHome.SearchMyOpportunitiesInLightning(opportunityNumber, valCAOUser);               
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
                usersLogin.DiffLightningLogout();

                //Login as Admin and validate NBC Approved checkbox
                //Search for the same Opportunity                
                opportunityHome.SearchOpportunity(opportunityNumber);
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
    



