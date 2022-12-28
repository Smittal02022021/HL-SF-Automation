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
                string subjectName = opportunityDetails.GetSubject();
                string jobType = opportunityDetails.GetJobType();
                Console.WriteLine(jobType);

                //Call function to update HL -Internal Team details
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

                //Click on NBC page and validate title of page
                string title = opportunityDetails.ClickNBCFormLCNBC();
                Assert.AreEqual("Opportunity Overview", title);
                extentReports.CreateLog("CNBC Form page is displayed with default tab : " + title + " ");

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
                extentReports.CreateLog("SEction with name: " + msgHLComp + " is displayed ");

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

                string txtReferralType= cnbc.GetLabelReferralType();
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

                string msgFinUnavail = cnbc.GetMessageFinUnavailable();
                Assert.AreEqual("Financials Unavailable", msgFinUnavail);
                extentReports.CreateLog("Message : " + msgFinUnavail + " is displayed ");

                string txtNoFin = cnbc.GetLabelNoFin();
                Assert.AreEqual("No Financials", txtNoFin);
                extentReports.CreateLog("Field with name: " + txtNoFin + " is displayed ");

                string txtNoFinExp = cnbc.GetLabelNoFinExp();
                Assert.AreEqual("No Financials Explanation", txtNoFinExp);
                extentReports.CreateLog("Field with name: " + txtNoFinExp + " is displayed ");

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
                Assert.AreEqual("Progress Fee (MM)", txtProgFee);
                extentReports.CreateLog("Field with name: " + txtProgFee + " is displayed ");

                string txtMinFee = form.GetLabelMinimumFee();
                Assert.AreEqual("Minimum Fee (MM)", txtMinFee);
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

                string txtReferral= cnbc.GetLabelReferralFeeOwed();
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

                string txtCCStatus= cnbc.GetLabelCCStatus();
                Assert.AreEqual("Conflict Check Status", txtCCStatus);
                extentReports.CreateLog("Field with name: " + txtCCStatus + " is displayed ");

                //Validate Submit For Review's available fields
                string txtNextSch = form.ValidateNextScheduledCallCheckbox();
                Assert.AreEqual("Next Scheduled Call", txtNextSch);
                extentReports.CreateLog("Checkbox with name: " + txtNextSch + " is displayed ");

                string txtReqFeedback = form.ValidateReqFeedbackCheckbox();
                Assert.AreEqual("Req feedback prior to normal sched call", txtReqFeedback);
                extentReports.CreateLog("Checkbox with name: " + txtReqFeedback + " is displayed ");

                string txtReviewSub = form.ValidateReviewSubCheckbox();
                Assert.AreEqual("Review Submission", txtReviewSub);
                extentReports.CreateLog("Checkbox with name: " + txtReviewSub + " is displayed ");

                //Validate Attachments section
                string txtAttachTab = cnbc.ValidateAttachemntsTab();
                Assert.AreEqual("Attachment", txtAttachTab);
                extentReports.CreateLog("Tab with name: " + txtAttachTab + " is displayed ");

                string txtFiles = form.ValidateFilesSection();
                Assert.AreEqual("Files", txtFiles);
                extentReports.CreateLog("Section with name: " + txtFiles + " is displayed ");

                string txtUpload = form.ValidateUploadFilesButton();
                Assert.AreEqual("Upload Files", txtUpload);
                extentReports.CreateLog("Button with name: " + txtUpload + " is displayed ");

                string txtOwner = form.ValidateOwnerDetailsSection();
                Assert.AreEqual("Ownership Details", txtOwner);
                extentReports.CreateLog("Section with name: " + txtOwner + " is displayed ");

                string txtClient= form.ValidateClientOwnershipField();
                Assert.AreEqual("Client Ownership", txtClient);
                extentReports.CreateLog("Field with name: " + txtClient + " is displayed ");

                string txtSub = form.ValidateSubjectOwnershipField();
                Assert.AreEqual("Subject Ownership", txtSub);
                extentReports.CreateLog("Field with name: " + txtSub + " is displayed ");

                string valClient = form.ValidateValueOfClientOwnership();
                Assert.AreEqual("Public Equity", valClient);
                extentReports.CreateLog("Value of Client Ownership : " + valClient + " is displayed ");

                string valSubject = form.ValidateValueOfSubjectOwnership();
                Assert.AreEqual("Public Equity", valSubject);
                extentReports.CreateLog("Value of Subject Ownership : " + valSubject + " is displayed ");

                string secApproval = form.ValidateApprovalHistorySection();
                Assert.AreEqual("Approval History", secApproval);
                extentReports.CreateLog("Section with name: " + secApproval + " is displayed ");                
              
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

    

