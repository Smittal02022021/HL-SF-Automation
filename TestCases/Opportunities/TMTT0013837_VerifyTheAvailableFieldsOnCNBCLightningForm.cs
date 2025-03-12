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
    class TMTT0013837_VerifyTheAvailableFieldsOnCNBCLightningForm : BaseClass
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

        public static string fileTC1232 = "CNBC_EndToEnd.xlsx";

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

                //Click on NBC and validate title of page
                string title = opportunityDetails.ClickNBCFormLCNBC();
                Assert.AreEqual("Opportunity Overview", title);
                extentReports.CreateLog("CNBC Form page is displayed with default tab : " + title + " ");

                //Click on Add Financials                
                string txtAddFin = form.ClickAddFinancialsButton();
                Assert.AreEqual("Add Financials", txtAddFin);
                extentReports.CreateLog("Page with name " + txtAddFin + " is displayed upon clic  king the tab. ");

                //Validate the year validation
                string txtYear = form.GetYearValidation();
                Assert.AreEqual("Complete this field.", txtYear);
                extentReports.CreateLog("Validation :" + txtYear + " is displayed corresponding to Year upon clicking Save button ");

                //Validate the Type validation
                string txtType = form.GetTypeValidation();
                Assert.AreEqual("Complete this field.", txtType);
                extentReports.CreateLog("Validation :" + txtType + " is displayed corresponding to Type upon clicking Save button ");

                //Validate the Related Company Field
                string txtRel = form.GetRelatedCompanyField();
                Assert.AreEqual("Related Company", txtRel);
                extentReports.CreateLog("Field with name :" + txtRel + " is displayed ");

                //Validate the Type Field
                string txtTypeField = form.GetTypeField();
                Assert.AreEqual("Type", txtTypeField);
                extentReports.CreateLog("Field with name :" + txtTypeField + " is displayed ");

                //Validate the Year Field
                string txtYearField = form.GetYearField();
                Assert.AreEqual("Year", txtYearField);
                extentReports.CreateLog("Field with name :" + txtYearField + " is displayed ");

                //Validate the As of Date Field
                string txtAsOf = form.GetAsOfDateField();
                Assert.AreEqual("As of Date", txtAsOf);
                extentReports.CreateLog("Field with name :" + txtAsOf + " is displayed ");

                //TMTI0078917 - Verify that on the Add Financials page, the following tooltip is added to the "As of Date" label
                string msgAsOfDate = form.ValidateAsOfDateToolTip();
                Assert.AreEqual("Default is 12/31/XX, but please specify if otherwise", msgAsOfDate);
                extentReports.CreateLog("Tool tip with message: " + msgAsOfDate + " is displayed upon hovering on As Of Date ");

                //Validate the Revenue (MM) Field
                string txtRevneue = form.GetRevenueMMField();
                Assert.AreEqual("Revenue (MM)", txtRevneue);
                extentReports.CreateLog("Field with name :" + txtRevneue + " is displayed ");

                //Validate the Annual Recurring Revenue (MM) Field
                string txtAnnual = form.GetAnnualRecurringRevenue();
                Assert.AreEqual("Annual Recurring Revenue (MM)", txtAnnual);
                extentReports.CreateLog("Field with name :" + txtAnnual + " is displayed ");

                //Validate the EBIT (MM) Field
                string txtEBIT = form.GetEBIT();
                Assert.AreEqual("EBIT (MM)", txtEBIT);
                extentReports.CreateLog("Field with name :" + txtEBIT + " is displayed ");

                //Validate the Currency Field
                string txtCurrency = form.GetCurrency();
                Assert.AreEqual("Currency", txtCurrency);
                extentReports.CreateLog("Field with name :" + txtCurrency + " is displayed ");

                //Validate the Face Value (MM) Field
                string txtFaceValue = form.GetFaceValue();
                Assert.AreEqual("Face Value (MM)", txtFaceValue);
                extentReports.CreateLog("Field with name :" + txtFaceValue + " is displayed ");

                //Validate the Net Asset Value(MM) Field
                string txtNetAssetValue = form.GetNetAssetValue();
                Assert.AreEqual("Net Asset Value (MM)", txtNetAssetValue);
                extentReports.CreateLog("Field with name :" + txtNetAssetValue + " is displayed ");

                //Validate the Number of Companies Field
                string txtNoOfComp = form.GetNumberOfCompanies();
                Assert.AreEqual("Number of Companies", txtNoOfComp);
                extentReports.CreateLog("Field with name :" + txtNoOfComp + " is displayed ");

                //Validate the Number of Loans Field
                string txtNoOfLoan = form.GetNumberOfLoans();
                Assert.AreEqual("Number of Loans", txtNoOfLoan);
                extentReports.CreateLog("Field with name :" + txtNoOfLoan + " is displayed ");

                //Validate the Number of Interests Field
                string txtNoOfInterests = form.GetNumberOfInterests();
                Assert.AreEqual("Number of Interests", txtNoOfInterests);
                extentReports.CreateLog("Field with name :" + txtNoOfInterests + " is displayed ");

                //Validate the Number of Policies Field
                string txtNoOfPolicies = form.GetNumberOfPolicies();
                Assert.AreEqual("Number of Policies", txtNoOfPolicies);
                extentReports.CreateLog("Field with name :" + txtNoOfPolicies + " is displayed ");

                //Validate the EBITDA (MM) Field
                string txtEBITDA = form.GetEBITDA();
                Assert.AreEqual("EBITDA (MM)", txtEBITDA);
                extentReports.CreateLog("Field with name :" + txtEBITDA + " is displayed ");

                //Validate Interest and Fee Income (MM) Field
                string txtInterest = form.GetInterestAndFeeIncome();
                Assert.AreEqual("Interest and Fee Income (MM)", txtInterest);
                extentReports.CreateLog("Field with name :" + txtInterest + " is displayed ");

                //Validate Pre - Tax Income(MM) Field
                string txtPreTax = form.GetPreTaxIncome();
                Assert.AreEqual("Pre-Tax Income (MM)", txtPreTax);
                extentReports.CreateLog("Field with name :" + txtPreTax + " is displayed ");

                //Validate Book Value (MM) Field
                string txtBook = form.GetBookValue();
                Assert.AreEqual("Book Value (MM)", txtBook);
                extentReports.CreateLog("Field with name :" + txtBook + " is displayed ");

                //Validate Assets Under Management (MM) Field
                string txtAssets = form.GetAssetsUnderManagement();
                Assert.AreEqual("Assets Under Management (MM)", txtAssets);
                extentReports.CreateLog("Field with name :" + txtAssets + " is displayed ");

                //Click Opportunity Overview tab and validate its available fields
                string txtOppOverview = form.ClickOpportunityOverview();
                Assert.AreEqual("Opportunity Overview", txtOppOverview);
                extentReports.CreateLog("Tab with name " + txtOppOverview + " is displayed upon clicking the tab. ");

                string txtRelOpp = form.GetLabelRelatedOpportunity();
                Assert.AreEqual("Related Opportunity", txtRelOpp);
                extentReports.CreateLog("Field with name: " + txtRelOpp + " is displayed ");

                string txtTxnOver = form.GetLabelTxnOverview();
                Assert.AreEqual("Transaction Overview", txtTxnOver);
                extentReports.CreateLog("Field with name: " + txtTxnOver + " is displayed ");

                string txtCurrentStatus = cnbc.GetLabelCurrentStatus();
                Assert.AreEqual("Current Status", txtCurrentStatus);
                extentReports.CreateLog("Field with name: " + txtCurrentStatus + " is displayed ");

                string txtCompDesc = form.GetLabelCompDesc();
                Assert.AreEqual("Company Description", txtCompDesc);
                extentReports.CreateLog("Field with name: " + txtCompDesc + " is displayed ");

                string txtRiskFact = cnbc.GetLabelRiskFactors();
                Assert.AreEqual("Risk Factors", txtRiskFact);
                extentReports.CreateLog("Field with name: " + txtRiskFact + " is displayed ");

                string txtExistingRel = cnbc.GetLabelExistingRel();
                Assert.AreEqual("Existing Relationships", txtExistingRel);
                extentReports.CreateLog("Field with name: " + txtExistingRel + " is displayed ");

                string txtExisting = cnbc.GetLabelExistingOrRepeatClient();
                Assert.AreEqual("Existing or Repeat Client?", txtExisting);
                extentReports.CreateLog("Field with name: " + txtExisting + " is displayed ");

                string txtAsiaAngle = cnbc.GetLabelAsiaAngle();
                Assert.AreEqual("Asia Angle", txtAsiaAngle);
                extentReports.CreateLog("Field with name: " + txtAsiaAngle + " is displayed ");

                string txtRealEst = cnbc.GetLabelRealEstAngle();
                Assert.AreEqual("Real Estate Angle", txtRealEst);
                extentReports.CreateLog("Field with name: " + txtRealEst + " is displayed ");

                string txtIntAngle = cnbc.GetLabelIntAngle();
                Assert.AreEqual("International Angle?", txtIntAngle);
                extentReports.CreateLog("Field with name: " + txtIntAngle + " is displayed ");

                string msgHLComp = cnbc.GetSectionHLComp();
                Assert.AreEqual("Houlihan Lokey Competition", msgHLComp);
                extentReports.CreateLog("Section with name: " + msgHLComp + " is displayed ");

                string txtHLComp = cnbc.GetLabelHLComp();
                Assert.AreEqual("Houlihan Lokey Competition", txtHLComp);
                extentReports.CreateLog("Field with name: " + txtHLComp + " is displayed ");

                string msgDesc = cnbc.GetSectionDescribeOther();
                Assert.AreEqual("Describe Other use(s) of proceeds if applicable.", msgDesc);
                extentReports.CreateLog("Section with name: " + msgDesc + " is displayed ");

                string txtProceeds = cnbc.GetLabelUseOfProceeds();
                Assert.AreEqual("Use of Proceeds", txtProceeds);
                extentReports.CreateLog("Field with name: " + txtProceeds + " is displayed ");

                string txtProDetails = cnbc.GetLabelUseOfProceedDetail();
                Assert.AreEqual("Use of Proceeds Detail", txtProDetails);
                extentReports.CreateLog("Field with name: " + txtProDetails + " is displayed ");

                string msgOwnership = cnbc.GetSectionOwnershipStr();
                Assert.AreEqual("Ownership Structure & Capital Structure (PLEASE INCLUDE DEBT SUMMARY)", msgOwnership);
                extentReports.CreateLog("Section with name: " + msgOwnership + " is displayed ");

                string txtOwnership = cnbc.GetLabelOwnershipStr();
                Assert.AreEqual("Ownership and Capital Structure", txtOwnership);
                extentReports.CreateLog("Field with name: " + txtOwnership + " is displayed ");

                string msgTotalDebt = cnbc.GetSectionTotalDebt();
                Assert.AreEqual("Total Debt(MM)", msgTotalDebt);
                extentReports.CreateLog("Section with name: " + msgTotalDebt + " is displayed ");

                string txtTotalDebt = cnbc.GetLabelTotalDebt();
                Assert.AreEqual("Total Debt (MM)", txtTotalDebt);
                extentReports.CreateLog("Field with name: " + txtTotalDebt + " is displayed ");

                string msgPrePitch = cnbc.GetSectionPrePitch();
                Assert.AreEqual("Pre-Pitch", msgPrePitch);
                extentReports.CreateLog("Section with name: " + msgPrePitch + " is displayed ");

                string txtPrePitch = cnbc.GetLabelWillThereBePitch();
                Assert.AreEqual("Will There Be a Pitch?", txtPrePitch);
                extentReports.CreateLog("Field with name: " + txtPrePitch + " is displayed ");

                string msgStructure = cnbc.GetSectionStructureAndPricing();
                Assert.AreEqual("Structure and Pricing Expectations (if relevant)", msgStructure);
                extentReports.CreateLog("Section with name: " + msgStructure + " is displayed ");

                string txtStructure = cnbc.GetLabelStructureAndPricing();
                Assert.AreEqual("Structure and Pricing Expectations", txtStructure);
                extentReports.CreateLog("Field with name: " + txtStructure + " is displayed ");

                string msgReferral = cnbc.GetSectionReferral();
                Assert.AreEqual("Referral", msgReferral);
                extentReports.CreateLog("Section with name: " + msgReferral + " is displayed ");

                string txtReferralType = cnbc.GetLabelReferralType();
                Assert.AreEqual("Referral Type", txtReferralType);
                extentReports.CreateLog("Field with name: " + txtReferralType + " is displayed ");

                string txtReferralSource = cnbc.GetLabelReferralSource();
                Assert.AreEqual("Referral Source", txtReferralSource);
                extentReports.CreateLog("Field with name: " + txtReferralSource + " is displayed ");

                string msgPlease = cnbc.GetSectionPlease();
                Assert.AreEqual("Please confirm that a group head has approved prior to submitting to the committee", msgPlease);
                extentReports.CreateLog("Section with name: " + msgPlease + " is displayed ");

                string txtGroup = cnbc.GetLabelGroupHead();
                Assert.AreEqual("Group Head Approval", txtGroup);
                extentReports.CreateLog("Field with name: " + txtGroup + " is displayed ");


                //Click Financials tab and validate its available fields
                string txtFinancials = form.ClickFinancialsTab();
                Assert.AreEqual("Financials", txtFinancials);
                extentReports.CreateLog("Tab with name " + txtFinancials + " is displayed upon clicking Financials tab. ");

                //TMTI0078911 - Verify that the red bolded text on the Financials tab 
                string msgAddFin = form.GetAddFinancialsText();
                Console.WriteLine("msgAddFin: " + msgAddFin);
                string colorMsg = form.GetColorOfAddFinancialsText();
                Assert.AreEqual("To add financials, please go to the “Add Financials” button at the top right of this form", msgAddFin);
                Assert.AreEqual("font-size: 14px; color: rgb(215, 13, 13);", colorMsg);
                extentReports.CreateLog("Message : " + msgAddFin + " is displayed in red color ");

                string msgFinUnavail = cnbc.GetMessageFinUnavailable();
                Assert.AreEqual("Financials Unavailable", msgFinUnavail);
                extentReports.CreateLog("Message : " + msgFinUnavail + " is displayed ");

                //TMTI0078913_Verify that the "No Financials" label is "Insufficient Financials" on the Financials tab along with the tooltip
                string txtNoFin = cnbc.GetLabelNoFin();
                Assert.AreEqual("Insufficient Financials", txtNoFin);
                extentReports.CreateLog("Field with name: " + txtNoFin + " is displayed ");

                string msgInSuffFin = form.ValidateInsufficientFinancialsToolTip();
                Assert.AreEqual("The committee would like to see at least four years of financials. If you don’t have at least four, add what you can and complete the explanation field below.", msgInSuffFin);
                extentReports.CreateLog("Tool tip with message: " + msgInSuffFin + " is displayed upon hovering on Insufficient Financials ");

                //TMTI0078915- Verify that the "No Financials Explanation" label is to "Insufficient Financials Explanation" with the following tooltip
                string txtNoFinExp = cnbc.GetLabelNoFinExp();
                Assert.AreEqual("Insufficient Financials Explanation", txtNoFinExp);
                extentReports.CreateLog("Field with name: " + txtNoFinExp + " is displayed ");

                string msgInSuffFinEx = form.ValidateInsufficientFinancialsExpToolTip();
                Assert.AreEqual("Please explain why there are insufficient financials:", msgInSuffFinEx);
                extentReports.CreateLog("Tool tip with message: " + msgInSuffFinEx + " is displayed upon hovering on Insufficient Financials ");

                string msgAboveFin = cnbc.GetMessageFinSubToAudit();
                Assert.AreEqual("Have the above Financials been subject to an audit?", msgAboveFin);
                extentReports.CreateLog("Message : " + msgAboveFin + " is displayed ");

                string txtFinSub = cnbc.GetLabelFinSubToAudit();
                Assert.AreEqual("Financials Subject to Audit", txtFinSub);
                extentReports.CreateLog("Field with name: " + txtFinSub + " is displayed ");

                string msgCapital = cnbc.GetMessageCapitalRaise();
                Assert.AreEqual("Capital Raise (MM) (Best estimate of capital to be raised expressed as a currency value in millions)", msgCapital);
                extentReports.CreateLog("Message : " + msgCapital + " is displayed ");

                string txtCapitalRaise = cnbc.GetLabelCapitalRaise();
                Assert.AreEqual("Capital Raise (MM)", txtCapitalRaise);
                extentReports.CreateLog("Field with name: " + txtCapitalRaise + " is displayed ");

                //Click Fees tab and validate its available fields
                string txtFees = form.ClickFeesTab();
                Assert.AreEqual("Fees", txtFees);
                extentReports.CreateLog("Tab with name " + txtFees + " is displayed upon clicking Fees tab. ");

                string txtRetainer = cnbc.GetLabelRetainer();
                Assert.AreEqual("Retainer", txtRetainer);
                extentReports.CreateLog("Field with name: " + txtRetainer + " is displayed ");

                string txtProgFee = form.GetLabelProgressFee();
                Assert.AreEqual("Progress Fee", txtProgFee);
                extentReports.CreateLog("Field with name: " + txtProgFee + " is displayed ");

                string txtMinFee = form.GetLabelMinimumFee();
                Assert.AreEqual("Engagement Letter Minimum Fee (MM)", txtMinFee);
                extentReports.CreateLog("Field with name: " + txtMinFee + " is displayed ");

                string txtEstFee = form.GetLabelTxnFeeType();
                Assert.AreEqual("Estimated Fee (MM)", txtEstFee);
                extentReports.CreateLog("Field with name: " + txtEstFee + " is displayed ");

                string txtFee = cnbc.GetLabelFeeStructure();
                Assert.AreEqual("Fee Structure", txtFee);
                extentReports.CreateLog("Field with name: " + txtFee + " is displayed ");

                string txtLockups = cnbc.GetLabelLockupsOnFuture();
                Assert.AreEqual("Lockups on Future M&A or Financing Work", txtLockups);
                extentReports.CreateLog("Field with name: " + txtLockups + " is displayed ");

                string txtReferral = cnbc.GetLabelReferralFeeOwed();
                Assert.AreEqual("Referral Fee Owed (MM)", txtReferral);
                extentReports.CreateLog("Field with name: " + txtReferral + " is displayed ");

                string txtRetainerFee = cnbc.GetLabelRetainerFeeCred();
                Assert.AreEqual("Retainer Fee Creditable ?", txtRetainerFee);
                extentReports.CreateLog("Field with name: " + txtRetainerFee + " is displayed ");

                string txtProgressFee = cnbc.GetLabelProgressFeeCred();
                Assert.AreEqual("Progress Fee Creditable ?", txtProgressFee);
                extentReports.CreateLog("Field with name: " + txtProgressFee + " is displayed ");

                //Click HL Internal Team tab and validate its available fields 
                string txtHLInt = form.ClickHLInternalTeamTab();
                Assert.AreEqual("HL Internal Team", txtHLInt);
                extentReports.CreateLog("Tab with name " + txtHLInt + " is displayed upon clicking HL Internal Team tab. ");

                string txtStaff = form.GetLabelOfCNBCStaff();
                Assert.AreEqual("Staff:", txtStaff);
                extentReports.CreateLog("Field with name: " + txtStaff + " is displayed ");

                string txtSave = form.ValidateSaveButton();
                Assert.AreEqual("Save", txtSave);
                extentReports.CreateLog("Button with name: " + txtSave + " is displayed ");

                string txtReturn = form.ValidateReturnToOppButton();
                Assert.AreEqual("Return To Opportunity", txtReturn);
                extentReports.CreateLog("Button with name: " + txtReturn + " is displayed ");

                string txtRole = form.ValidateRoleDefButton();
                Assert.AreEqual("Role Definitions", txtRole);
                extentReports.CreateLog("Button with name: " + txtRole + " is displayed ");

                //Click Administative tab and validate all its available fields
                string txtAdmin = cnbc.ClickAdministrativeTab();
                Assert.AreEqual("Administrative", txtAdmin);
                extentReports.CreateLog("Tab with name " + txtAdmin + " is displayed upon clicking Administrative ");

                string msgAdmin = cnbc.GetSectionAdministrative();
                Assert.AreEqual("Administrative", msgAdmin);
                extentReports.CreateLog("Section with name: " + msgAdmin + " is displayed ");

                string txtRestricted = cnbc.GetLabelRestrictedList();
                Assert.AreEqual("Restricted List", txtRestricted);
                extentReports.CreateLog("Field with name: " + txtRestricted + " is displayed ");

                string msgCCInfo = cnbc.GetSectionCCInfo();
                Assert.AreEqual("Conflicts Check Information", msgCCInfo);
                extentReports.CreateLog("Section with name: " + msgCCInfo + " is displayed ");

                string txtCCStatus = cnbc.GetLabelCCStatus();
                Assert.AreEqual("Conflict Check Status", txtCCStatus);
                extentReports.CreateLog("Field with name: " + txtCCStatus + " is displayed ");

                //TMTI0078901_Validate Submit For Review's available fields
                string txtToSubmit = cnbc.ValidateSectionSubmitCNBC();
                Assert.AreEqual("To Submit An CNBC Form:", txtToSubmit);
                extentReports.CreateLog("Section with name: " + txtToSubmit + " is displayed ");

                //TMTI0078903---
                string txtNextSch = form.ValidateNextScheduledCallCheckbox();
                Assert.AreEqual("Is this for the next scheduled call?", txtNextSch);
                extentReports.CreateLog("Checkbox with name: " + txtNextSch + " is displayed ");

                string msgToolTip = form.ValidateNextScheduledCallToolTip();
                Assert.AreEqual("This is the optimum and preference of the committee. Calls are typically Monday unless it is an HL Holiday.", msgToolTip);
                extentReports.CreateLog("Tool tip with message: " + msgToolTip + " is displayed ");

                string txtReqFeedback = form.ValidateReqFeedbackCheckbox();
                Assert.AreEqual("Feedback required before next call?", txtReqFeedback);
                extentReports.CreateLog("Checkbox with name: " + txtReqFeedback + " is displayed ");

                //TMTI0078905_Validate additional fields of Req feedback along with its validations
                string ques1 = form.ValidateQuestion1OfReqFeedback();
                Assert.AreEqual("When is feedback needed by?", ques1);
                extentReports.CreateLog("Question 1: " + ques1 + " is displayed upon selecting Feedback required before next call? checkbox ");

                string ques2 = form.Validate2ndQuestionOfReqFeedback();
                Assert.AreEqual("*Why can't this wait until next call?", ques2);
                extentReports.CreateLog("Question 1: " + ques2 + " is displayed upon selecting Feedback required before next call? checkbox ");

                string message1 = form.Validate1stMessageOfReqFeedback();
                Assert.AreEqual("When is feedback needed by?", message1);
                extentReports.CreateLog("Mandatory Validation: " + message1 + " is displayed upon clicking Save button without entering its values ");

                string message2 = form.Validate2ndMessageOfReqFeedback();
                Assert.AreEqual("Why can't this wait until next call?", message2);
                extentReports.CreateLog("Mandatory Validation: " + message2 + " is displayed upon clicking Save button without entering its values ");

                string txtRequiresGMT = form.ValidateRequiresFeedbackInGMTCheckbox();
                Assert.AreEqual("Requires Feedback Date&Time in GMT", txtRequiresGMT);
                extentReports.CreateLog("Checkbox with name: " + txtRequiresGMT + " is displayed ");

                //TMTI0078909 ---
                string txtReviewSub = form.ValidateReviewSubCheckbox();
                Assert.AreEqual("Form Check (required to submit)", txtReviewSub);
                extentReports.CreateLog("Checkbox with name: " + txtReviewSub + " is displayed ");

                string msgToolTipForm = form.ValidateFormCheckToolTip();
                Assert.AreEqual("First successfully complete the form check. After, you will be able to successfully submit by clicking “Submit for Review” (button will appear in upper right corner after form check is complete).", msgToolTipForm);
                extentReports.CreateLog("Tool tip with message: " + msgToolTipForm + " is displayed ");

                //TMTI0078907_Validate Question for supporting document and its tool tip
                string supportingQues = form.ValidateSupportingDocQuestion();
                Assert.AreEqual("Do you have any supporting documents?", supportingQues);
                extentReports.CreateLog("Question : " + supportingQues + " is displayed ");

                string supportingTip = form.ValidateSupportingDocQuestionTooltip();
                Assert.AreEqual("*Such as potential buyers, financials, Supplementary pages only, not full decks.", supportingTip);
                extentReports.CreateLog("Tool tip : " + supportingTip + " is displayed for Supporting document ");

                string txtFiles = form.ValidateFilesSection();
                Assert.AreEqual("Files", txtFiles);
                extentReports.CreateLog("Section with name: " + txtFiles + " is displayed ");

                string txtUpload = form.ValidateUploadFilesButton();
                Assert.AreEqual("Upload Files", txtUpload);
                extentReports.CreateLog("Button with name: " + txtUpload + " is displayed ");

                string excelPath1 = ReadJSONData.data.filePaths.testData;
                string successMsg = form.UploadFileAndValidate(excelPath1 + "UploadFile.txt");
                Assert.AreEqual("UploadFile", successMsg);
                extentReports.CreateLog("Selected File has been uploaded ");


                //string txtOwner = form.ValidateOwnerDetailsSection();
                //Assert.AreEqual("Ownership Details", txtOwner);
                //extentReports.CreateLog("Section with name: " + txtOwner + " is displayed ");

                //string txtClient= form.ValidateClientOwnershipField();
                //Assert.AreEqual("Client Ownership", txtClient);
                //extentReports.CreateLog("Field with name: " + txtClient + " is displayed ");

                //string txtSub = form.ValidateSubjectOwnershipField();
                //Assert.AreEqual("Subject Ownership", txtSub);
                //extentReports.CreateLog("Field with name: " + txtSub + " is displayed ");

                //string valClient = form.ValidateValueOfClientOwnership();
                //Assert.AreEqual("Public Equity", valClient);
                //extentReports.CreateLog("Value of Client Ownership : " + valClient + " is displayed ");

                //string valSubject = form.ValidateValueOfSubjectOwnership();
                //Assert.AreEqual("Public Equity", valSubject);
                //extentReports.CreateLog("Value of Subject Ownership : " + valSubject + " is displayed ");

                //string secApproval = form.ValidateApprovalHistorySection();
                //Assert.AreEqual("Approval History", secApproval);
                //extentReports.CreateLog("Section with name: " + secApproval + " is displayed ");                

                form.SwitchFrame();

                usersLogin.DiffLightningLogout();
                usersLogin.DiscardChanges();
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

