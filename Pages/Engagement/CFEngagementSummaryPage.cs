using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;

namespace SF_Automation.Pages.Engagement
{
    class CFEngagementSummaryPage : BaseClass
    {
        By lblEngagementDynamicsSection = By.XPath("//span[@title='EL / Engagement Dynamics']");
        By lblFeesActualAmount = By.XPath("//h2/span[contains(text(),'Fees')]");
        By lblTransactionFeeCalcActualAmount = By.XPath("//h2/span[contains(text(),'Transaction Fee Calc')]");
        By lblIncentiveStructureActualAmount = By.XPath("//h2/span[contains(text(),'Incentive')]");
        By lblTransactionActualAmount = By.XPath("//h2/span[contains(text(),'Transaction (Actual Amount)')]");
        By lblTotalsActualAmount = By.XPath("//h2/span[contains(text(),'Totals')]");

        By btnSave = By.XPath("//button[@type='submit']");
        By btnCancel = By.XPath("//button[@name='cancel']");

        //Fees (Actual Amount) section elements
        By btnEditFeeDetails = By.XPath("//button[@title='Edit: Retainer_Fees__c']");
        By chkRetainerFees = By.XPath("//input[@name='Is_Retainer_Fee_Creditable__c']");
        By txtRetainerFees = By.XPath("//input[@name='Retainer_Fees__c']");
        By valRetainerFees = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[1]");

        By chkCompletionOfCIM = By.XPath("//input[@name='Is_Completion_of_CIM_Creditable__c']");
        By txtCompletionOfCIM = By.XPath("//input[@name='Completion_Of_CIM__c']");
        By valCompletionOfCIM = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[2]");

        By chkFirstRoundBid = By.XPath("//input[@name='Is_First_Round_Bid_Creditable__c']");
        By txtFirstRoundBid = By.XPath("//input[@name='First_Round_Bid__c']");
        By valFirstRoundBid = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[3]");

        By chkSecondRoundBid = By.XPath("//input[@name='Is_Second_Round_Bid_Creditable__c']");
        By txtSecondRoundBid = By.XPath("//input[@name='Second_Round_Bid__c']");
        By valSecondRoundBid = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[4]");

        By chkLOI = By.XPath("//input[@name='Is_LOI_Creditable__c']");
        By txtLOI = By.XPath("//input[@name='LOI__c']");
        By valLOI = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[5]");

        By chkSignedAgreement = By.XPath("//input[@name='Is_Signed_Agreement_Creditable__c']");
        By txtSignedAgreement = By.XPath("//input[@name='Signed_Agreement__c']");
        By valSignedAgreement = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[6]");

        By btnEditOtherFee = By.XPath("//button[@title='Edit: Other_Fee_Type_01__c']");
        By txtOtherFeeType01 = By.XPath("//input[@name='Other_Fee_Type_01__c']");
        By txtOtherFee01 = By.XPath("//input[@name='Other_Fee_01__c']");
        By chkOtherFee01 = By.XPath("//input[@name='Is_Other_Fee_01_Creditable__c']");
        By valOtherFee01 = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[8]");

        By txtOtherFeeType02 = By.XPath("//input[@name='Other_Fee_Type_02__c']");
        By txtOtherFee02 = By.XPath("//input[@name='Other_Fee_02__c']");
        By chkOtherFee02 = By.XPath("//input[@name='Is_Other_Fee_02_Creditable__c']");
        By valOtherFee02 = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[10]");

        By iconFeesInfo = By.XPath("(//span[text()='Fees (Actual Amount)']/following::div/h2/c-hl-universal-pop-over/div/lightning-icon)[1]");
        By lblFeeHelpText1 = By.XPath("(//span[text()='Fees (Actual Amount)']/following::*/div/section/div/div/h3)[1]/span");
        By lblFeeHelpText2 = By.XPath("(//span[text()='Fees (Actual Amount)']/following::*/div/section/div/div/h3)[1]/span/following::p");

        //Transaction (Actual Amount) section elements
        By btnEditTransactionDetails = By.XPath("//button[@title='Edit: Transaction_Fee_Type__c']");
        By dropdownTransactionType = By.XPath("//label[text()='Transaction Fee Type']/following::*[@name='Transaction_Fee_Type__c']");
        By txtTransactionFee = By.XPath("//input[@name='Transaction_Fee__c']");
        By valTransactionFee = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[12]");

        By iconTransactionFeeInfo = By.XPath("(//span[text()='Transaction Fee']/following::div/button)[1]");
        By lblTransactionFeeHelpText = By.XPath("//div[@class='slds-popover__body']");

        //Transaction Fee Calc (Actual Amount) section elements
        By btnEditTransactionFeeCalcDetails = By.XPath("//button[@title='Edit: Transaction_Value_for_Fee_Calc__c']");
        By txtTransactionValueForFeeCalc = By.XPath("//input[@name='Transaction_Value_for_Fee_Calc__c']");
        By valTransactionValueForFeeCalc = By.XPath("(//span[text()='Retainer Fees']/following::div/lightning-formatted-text)[13]");

        By iconTransactionValueForFeeCalcInfo = By.XPath("(//span[text()='Transaction Value for Fee Calc (Actual)']/following::div/button)[1]");
        By lblTransactionValueForFeeCalcHelpText = By.XPath("//div[@class='slds-popover__body']");

        //Incentive Structure (Actual Amount) section elements
        By btnEditIncentiveStructureDetails = By.XPath("//button[@title='Edit: First_Ratchet_Percent__c']");

        By txtFirstRatchetPercent = By.XPath("//input[@name='First_Ratchet_Percent__c']");
        By valFirstRatchetPercent = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-number)[1]");
        By txtFirstRatchetFromAmount = By.XPath("//input[@name='First_Ratchet_From_Amount__c']");
        By valFirstRatchetFromAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[1]");
        By txtFirstRatchetToAmount = By.XPath("//input[@name='First_Ratchet_To_Amount__c']");
        By valFirstRatchetToAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[2]");

        By txtSecondRatchetPercent = By.XPath("//input[@name='Second_Ratchet_Percent__c']");
        By valSecondRatchetPercent = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-number)[2]");
        By txtSecondRatchetFromAmount = By.XPath("//input[@name='Second_Ratchet_From_Amount__c']");
        By valSecondRatchetFromAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[3]");
        By txtSecondRatchetToAmount = By.XPath("//input[@name='Second_Ratchet_To_Amount__c']");
        By valSecondRatchetToAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[4]");

        By txtThirdRatchetPercent = By.XPath("//input[@name='Third_Ratchet_Percent__c']");
        By valThirdRatchetPercent = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-number)[3]");
        By txtThirdRatchetFromAmount = By.XPath("//input[@name='Third_Ratchet_From_Amount__c']");
        By valThirdRatchetFromAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[5]");
        By txtThirdRatchetToAmount = By.XPath("//input[@name='Third_Ratchet_To_Amount__c']");
        By valThirdRatchetToAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[6]");

        By txtFourthRatchetPercent = By.XPath("//input[@name='Fourth_Ratchet_Percent__c']");
        By valFourthRatchetPercent = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-number)[4]");
        By txtFourthRatchetFromAmount = By.XPath("//input[@name='Fourth_Ratchet_From_Amount__c']");
        By valFourthRatchetFromAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[7]");
        By txtFourthRatchetToAmount = By.XPath("//input[@name='Fourth_Ratchet_To_Amount__c']");
        By valFourthRatchetToAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[8]");

        By txtFinalRatchetPercent = By.XPath("//input[@name='Final_Ratchet_Percent__c']");
        By valFinalRatchetPercent = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-number)[5]");
        By txtFinalRatchetAmount = By.XPath("//input[@name='Final_Ratchet_Amount__c']");
        By valFinalRatchetAmount = By.XPath("(//span[text()='First Ratchet Percent']/following::div/lightning-formatted-text)[9]");

        By iconIncentiveStructureInfo = By.XPath("(//span[text()='Incentive Structure (Actual Amount)']/following::c-hl-universal-pop-over/div/lightning-icon)[1]");
        By lblIncentiveStructureHelpText1 = By.XPath("(//span[text()='Incentive Structure (Actual Amount)']/following::*/div/section/div/div/h3)[1]/span");
        By lblIncentiveStructureHelpText2 = By.XPath("(//span[text()='Incentive Structure (Actual Amount)']/following::*/div/section/div/div/h3)[1]/span/following::p");

        //Total (Actual Amount) section elements
        By lblTotalFee = By.XPath("(//span[contains(text(),'Total Fee')]/following::div/lightning-formatted-text)[1]");
        By lblTotalCredits = By.XPath("(//span[contains(text(),'Total Credits')]/following::div/lightning-formatted-text)[1]");
        By lblPaymentOnClosing = By.XPath("(//span[contains(text(),'Payment On Closing')]/following::div/lightning-formatted-text)[1]");

        By btnEditSubjectToContingentFees = By.XPath("//button[@title='Edit: Fee_Subject_To_Contingent_Fees__c']");
        By chkSubjectToContingentFee = By.XPath("//input[@name='Fee_Subject_To_Contingent_Fees__c']");

        By iconTotalsInfo = By.XPath("(//span[text()='Totals (Actual Amount)']/following::div/h2/c-hl-universal-pop-over/div/lightning-icon)[1]");
        By lblTotalsHelpText1 = By.XPath("(//span[text()='Totals (Actual Amount)']/following::*/div/section/div/div/h3)[1]/span");
        By lblTotalsHelpText2 = By.XPath("(//span[text()='Totals (Actual Amount)']/following::*/div/section/div/div/h3)[1]/span/following::p");

        By iconTotalCreditsInfo = By.XPath("//span[text()='Total Credits']/following::div/button");
        By lblTotalCreditsHelpText = By.XPath("//div[@class='slds-popover__body']");
        By iconFeeSubjectToContingentFeesInfo = By.XPath("//span[text()='Fee Subject To Contingent Fees']/following::div/button");
        By lblFeeSubjectToContingentFeesHelpText = By.XPath("//*[@class='slds-popover slds-popover_tooltip slds-nubbin_bottom-left slds-rise-from-ground']/div");

        //Error Message elements
        By lblHeaderErrorMsg = By.XPath("//div[@class='slds-notify__content']/h2");
        By lblFirstRatchetFromAmtErrMsg = By.XPath("//label[text()='First Ratchet From Amount']/following::div[2]");
        By lblFirstRatchetToAmtErrMsg = By.XPath("//label[text()='First Ratchet To Amount']/following::div[2]");
        By lblSecondRatchetFromAmtErrMsg = By.XPath("//label[text()='Second Ratchet From Amount']/following::div[2]");
        By lblSecondRatchetToAmtErrMsg = By.XPath("//label[text()='Second Ratchet To Amount']/following::div[2]");
        By lblThirdRatchetFromAmtErrMsg = By.XPath("//label[text()='Third Ratchet From Amount']/following::div[2]");
        By lblThirdRatchetToAmtErrMsg = By.XPath("//label[text()='Third Ratchet To Amount']/following::div[2]");
        By lblFourthRatchetFromAmtErrMsg = By.XPath("//label[text()='Fourth Ratchet From Amount']/following::div[2]");
        By lblFourthRatchetToAmtErrMsg = By.XPath("//label[text()='Fourth Ratchet To Amount']/following::div[2]");
        By lblFinalRatchetAmtErrMsg = By.XPath("//label[text()='Final Ratchet Amount']/following::div[2]");

        //SF Lightening elements
        By btnSearchBox = By.XPath("//button[@aria-label='Search']");
        By txtSearchBox = By.XPath("//label[text()='Search Opportunities and more']/following::div/input");
        By btnAdditionalTabs = By.XPath("//button[@class='slds-button slds-button_icon-border-filled']");
        By linkCFEngSummary = By.XPath("//span[text()='Engagement Summary (CF)']");
        By lblExtDisclosureStatus = By.XPath("//li//span[text()='External Disclosure Status']");
        By valExtDisclosureStatus = By.XPath("//li//span[text()='External Disclosure Status']/ancestor::li//div[2]");
        By lblLOB = By.XPath("//li//span[text()='Line of Business']");
        By valLOB = By.XPath("//li//span[text()='Line of Business']/ancestor::li/div[2]");
        By lblEngNum = By.XPath("//li//span[text()='Engagement Number']");
        By valEngNum = By.XPath("//li//span[text()='Engagement Number']//ancestor::li/div[2]");
        By lblSubOwner = By.XPath("//li//span[text()='Subject Ownership']");
        By valSubOwner = By.XPath("//li//span[text()='Subject Ownership']//ancestor::li/div[2]");
        By lblSubject = By.XPath("//li/div/span[text()='Subject']");
        By valSubject = By.XPath("//li//span[text()='Subject']/ancestor::li//a");
        By lblClient = By.XPath("//li/div/span[text()='Client']");
        By valClient = By.XPath("//li//span[text()='Client']/ancestor::li//a");
        By lblTxnSize = By.XPath("//li//span[text()='Est. Transaction Size / Market Cap (MM)']/ancestor::li//div[1]/span");
        By valTxnSize = By.XPath("//li//span[text()='Est. Transaction Size / Market Cap (MM)']/ancestor::li//div[2]");
        By msgTxnSize = By.XPath("//li//span[text()='Est. Transaction Size / Market Cap (MM)']/ancestor::li//div[1]//button//span[2]");
        By msgEngNum = By.XPath("//li//span[text()='Engagement Number']/ancestor::li//div[1]//button//span[2]");
        By btnSummaryReport = By.XPath("//button[text()='Summary Report']");
        By txtCognoUser = By.XPath("//input[@id='CAMUsername']");
        By txtCognoPass = By.XPath("//input[@id='CAMPassword']");
        By btnSignin = By.XPath("//button[text()='Sign in']");
        By lblCloseDate = By.XPath("//li//span[text()='Close Date']/ancestor::li//div[1]/span");
        By valCloseDate = By.XPath("//li//span[text()='Close Date']/ancestor::li//div[2]");
        By lblJoType = By.XPath("//li//span[text()='Job Type']/ancestor::li//div[1]/span");
        By valJobType = By.XPath("//li//span[text()='Job Type']/ancestor::li//div[2]");
        By secParties = By.XPath("//span[text()='Parties']");
        By secMarketing = By.XPath("//span[text()='Marketing Process Data']");
        By secELEngDynamics = By.XPath("//span[text()='EL / Engagement Dynamics']");
        By valTypeBuyside = By.XPath("//span[text()='Company']/ancestor::div[2]//div[2]//lightning-formatted-text");
        By valCompanyBuyside = By.XPath("//span[text()='Company']/ancestor::lightning-output-field//div[1]//a");
        By valIGBuysdie = By.XPath("//span[text()='Buyer']/ancestor::article[1]//span[text()='Industry Group']/ancestor::div[1]//div/lightning-formatted-text");
        By valOwnershipBuyside = By.XPath("//span[text()='Buyer']/ancestor::article[1]//span[text()='Ownership']/ancestor::div[1]//div/lightning-formatted-text");
        By valSectorBuyside = By.XPath("//span[text()='Buyer']/ancestor::article[1]//span[text()='Sector']/ancestor::div[1]//div/lightning-formatted-text");

        By secBuyerStrategy = By.XPath("//span[text()='Buyer Strategy']");
        By lblBuyerStrategy = By.XPath("//span[text()='Buyer Strategy']/ancestor::div[1]/div//form//div[1]//div[1]/div/lightning-output-field/span");
        By btnParties = By.XPath("//span[text()='Parties']/ancestor::button");
        By secSeller = By.XPath("//h2/span[text()='Seller']");
        By secBuyer = By.XPath("//h2/span[text()='Buyer']");
        By secMarketings = By.XPath("//span[text()='Marketing Process Data']/ancestor::section[1]/div[2]//h2/span");
        By colOutreachMetric = By.XPath("//span[text()='Outreach Metrics']/ancestor::article[1]//table/thead/tr[1]/td");
        By rowOutreachMetric = By.XPath("//span[text()='Outreach Metrics']/ancestor::article[1]//table/tbody/tr/th//a");
        By subcolOutreachMetric = By.XPath("//span[text()='Outreach Metrics']/ancestor::article[1]//table/thead/tr[2]//div");
        By valPotCounterparty = By.XPath("//div[@title='Potential Counterparties Contacted']/ancestor::tr/td[5]//lightning-formatted-number");
        By colBidHistory = By.XPath("//span[text()='Bid History']/ancestor::article[1]//table/thead/tr[1]/td");
        By subcolBid = By.XPath("//span[text()='Bid History']/ancestor::article[1]//table/thead/tr[2]/th/div");
        By subsecELEngDynamics = By.XPath("//span[text()='EL / Engagement Dynamics']/ancestor::Section[1]//h2/span");
        By lblFeesActualFields = By.XPath("//span[text()='Fees (Actual Amount)']/ancestor::article[1]/div[2]//lightning-output-field/span");
            
        By lblSellerSection = By.XPath("//lightning-output-field/span[text()='Client']/ancestor::div[4]//lightning-output-field/span");
        By iconSeller = By.XPath("//h2/span[text()='Seller']/ancestor::h2/c-hl-universal-pop-over/div/lightning-icon//lightning-primitive-icon");
        By iconBuyer = By.XPath("//h2/span[text()='Buyer']/ancestor::h2/c-hl-universal-pop-over/div/lightning-icon//lightning-primitive-icon");
        By valCompany = By.XPath("//h2/span[text()='Buyer']/ancestor::h2/c-hl-universal-pop-over/div/section//footer//li[1]//span//span");
        By lblSellerBackgroundSection = By.XPath("//span[text()='Seller Background']/ancestor::div[1]//lightning-output-field/span");
        By lnkClientCompany = By.XPath("//span[text()='Client']/ancestor::div[4]/div[1]/div[1]/div[1]//lightning-formatted-lookup");
        By valIGCompany = By.XPath("//span[text()='Ticker Symbol']/ancestor::flexipage-column2//span[text()='Industry Group']/ancestor::div[2]//lightning-formatted-text");
        By valSectorCompany = By.XPath("//span[text()='Ticker Symbol']/ancestor::flexipage-column2//span[text()='Sector']/ancestor::div[2]//lightning-formatted-text");
        By valDesc = By.XPath("//span[text()='Description']/ancestor::div[2]//lightning-formatted-text");
        By valIGSummary = By.XPath("//span[text()='Seller Background']/ancestor::div[1]//span[text()='Industry Group']/ancestor::lightning-output-field//lightning-formatted-text");
        By valSectorSummary = By.XPath("//span[text()='Seller Background']/ancestor::div[1]//span[text()='Sector']/ancestor::lightning-output-field//lightning-formatted-text");
        By valDescSummary = By.XPath("//span[text()='Seller Background']/ancestor::div[1]//span[text()='Description']/ancestor::lightning-output-field//lightning-formatted-text");
        By tabEngsummary = By.XPath("//a/span[text()='Engagement Summary']");
        By lblSellerDetailsSection = By.XPath("//span[text()='Seller Details (MM)']/ancestor::div[1]//lightning-output-field/span");
        By iconSellerDetails = By.XPath("//span[text()='Seller Details (MM)']/ancestor::h3//c-hl-universal-pop-over/div/lightning-icon//lightning-primitive-icon");
        By lblTxnRationale = By.XPath("//li[1]/span[text()='Transaction Rationale']");
        By btnEditTxnRationale = By.XPath("//button[@title='Edit: Transaction_Rationale__c']");
        By btnTxnRationale = By.XPath("//button[@name='Transaction_Rationale__c']");
        By valTxnRationale = By.XPath("//button[@name='Transaction_Rationale__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span");
        By valTxnRationaleBeforeUpdate = By.XPath("//button[@title='Edit: Transaction_Rationale__c']/ancestor::div[1]/lightning-formatted-text");
        By iconAddRecord = By.XPath("//span[text()='Seller Financials']/ancestor::h3//button[@title='Add Record']");
        By iconAddRecordContact = By.XPath("//span[text()='Seller Contacts']/ancestor::h3//button[@title='Add Record']");
        By iconAddRecordBuyerContact = By.XPath("//span[text()='Buyer Contacts']/ancestor::h3//button[@title='Add Record']");

        By lblAddRecordSection = By.XPath("//span[text()='Add/Edit Record']/ancestor::article[1]/div[2]//label");
        By lblAddContactSection = By.XPath("//span[text()='Add/Edit Record']/ancestor::article[1]/div[2]//label");
        By btnType = By.XPath("//button[@name='Type__c']");
        By valType = By.XPath("//button[@name='Type__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span");
        By btnRole = By.XPath("//button[@name='Role__c']");
        By valRole = By.XPath("//button[@name='Role__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span");

        By msgType = By.XPath("//label[text()='Type']/ancestor::div[1]/lightning-helptext//span[2]");
        By msgContact = By.XPath("//label[text()='Contact']/ancestor::div[1]//div[text()='Complete this field.']");
        By txtRevMM = By.XPath("//input[@name='Revenue_LTM_MM__c']");
        By txtEBITDA = By.XPath("//input[@name='EBITDA_LTM_MM__c']");
        By valRevMM = By.XPath("//span[text()='Seller Financials']/ancestor::div[1]//td[2]//lightning-formatted-number");
        By btnCurrency = By.XPath("//label[text()='Currency']/ancestor::div[1]/div//button");
        By tabEngagementL = By.XPath("//div[2]/section//ul[2]/li[2]/a/span[2]");
        By tabSummary = By.XPath("//div[2]/section//ul[2]/li[3]/a/span[text()='Engagement Summary']");
        By tabSummaryBuyer = By.XPath("//div[2]/section//ul[2]/li[5]/a/span[text()='Engagement Summary']");

        By tabContacts = By.XPath("//a[text()='Contacts']");
        By tabFees = By.XPath("//a[@data-label='Fees & Financials']");
        By valAddedRevs = By.XPath("//span[@title='Financials']/ancestor::article//tr/td[2]//span//lst-formatted-text/span");
        By btnAddFinancial = By.XPath("//button[text()='New Financials']");
        By txtRelatedEng = By.XPath("//input[@placeholder='Search Engagements...']");
        By btnSaveFin = By.XPath("//button[@name='SaveEdit']");
        By valAddedFin = By.XPath("//records-entity-label[text()='Engagement Financials']/ancestor::div[@class='slds-grid slds-wrap simpleRecordHomeTemplate']//span[text()='Revenue LTM (MM)']/ancestor::div[2]//span//lightning-formatted-text");
        By btnRefreshFin = By.XPath("//span[text()='Seller Financials']/ancestor::h3//button[@title='Refresh Table']");
        By btnClose = By.XPath("//button[@title='Close']");
        By lblSellerContacts = By.XPath("//span[text()='Seller Contacts']");
        By btnRefreshContact = By.XPath("//span[text()='Seller Contacts']/ancestor::h3//button[@title='Add Record']/ancestor::div[1]//button[@title='Refresh Table']");
        By btnRefreshBuyerContact = By.XPath("//span[text()='Buyer Contacts']/ancestor::h3//button[@title='Add Record']/ancestor::div[1]//button[@title='Refresh Table']");

        By btnMoreFin = By.XPath("//tr[1]/td[6]//lightning-button-menu/button");
        By lnkEditRecord = By.XPath("//span[text()='Edit']");
        By chkEngFinCheck = By.XPath("//input[@name='Engagement_Financials_Check__c']");
        By chkEngContactCheck = By.XPath("//input[@name='Engagement_Contacts_Seller_Check__c']");
        By chkEngContactNoAttorneyCheck = By.XPath("//input[@name='Engagement_Contact_Seller_No_Attorney__c']");
        By chkEngContactCheckBuyer = By.XPath("//input[@name='Engagement_Contacts_Buyer_Check__c']");
        By chkEngContactNoAttorneyCheckBuyer = By.XPath("//input[@name='Engagement_Contact_Buyer_No_Attorney__c']");
        By lnkDeleteRecord = By.XPath("//span[text()='Delete']");
        By btnWinCancel = By.XPath("//button[text()='Cancel']");
        By btnWinOk = By.XPath("//button[text()='Ok']");
        By msgDelete = By.XPath("//h2[text()='Record was deleted.']");
        By iconSellerFin = By.XPath("//span[text()='Seller Financials']/ancestor::h3//lightning-icon[@title='Click to keep open']//lightning-primitive-icon");
        By msgSellerFin = By.XPath("//section[@role='dialog']//li[1]/span[text()='Engagement Financials Check']");
        By iconEngFinCheck = By.XPath("//span[text()='Seller Financials']/ancestor::div[1]/div//lightning-output-field//button/span[2]");
        By txtContact = By.XPath("//span[text()='Add/Edit Record']/ancestor::article[1]/div[2]//input[@placeholder='Search Contacts...']");
        By valAddedContact = By.XPath("//span[text()='Seller Contacts']/ancestor::div[1]//tbody/tr[1]/th//span//a");
        By valAdded2ndContact = By.XPath("//span[text()='Seller Contacts']/ancestor::div[1]//tbody/tr[2]/th//span//a");
        By valAddedBuyerContact = By.XPath("//span[text()='Buyer Contacts']/ancestor::div[1]//tbody/tr[1]/th//span//a");
        By btnShowMoreContact = By.XPath("//span[text()='Seller Contacts']/ancestor::div[1]//tbody/tr[1]/td[5]//button");
        By valAdded2ndBuyerContact = By.XPath("//span[text()='Buyer Contacts']/ancestor::div[1]//tbody/tr[2]/th//span//a");
        By btnShowMoreBuyerContact = By.XPath("//span[text()='Buyer Contacts']/ancestor::div[1]//tbody/tr[1]/td[5]//button");
        By valBidCompany = By.XPath("//div[text()='Counterparty List']/ancestor::table/tbody/tr[1]//a");

        By lnkEdit = By.XPath("//span[text()='Edit']");
        By iconEngContactCheck = By.XPath("//span[text()='Seller Contacts']/ancestor::div[1]/div//span[text()='Engagement Contacts Seller Check']/ancestor::lightning-output-field//button/span[2]");
        By iconEngContactAttorneyCheck = By.XPath("//span[text()='Seller Contacts']/ancestor::div[1]/div//span[text()='Engagement Contact Seller No Attorney']/ancestor::lightning-output-field//button/span[2]");
        By iconEngContactCheckBuyer = By.XPath("//span[text()='Buyer Contacts']/ancestor::div[1]/div//span[text()='Engagement Contacts Buyer Check']/ancestor::lightning-output-field//button/span[2]");
        By iconEngContactAttorneyCheckBuyer = By.XPath("//span[text()='Buyer Contacts']/ancestor::div[1]/div//span[text()='Engagement Contact Buyer No Attorney']/ancestor::lightning-output-field//button/span[2]");
        By btnEditCheckbox = By.XPath("//button[@title='Edit: Engagement_Contact_Seller_No_Attorney__c']");
        By btnEditBuyerCheckbox = By.XPath("//button[@title='Edit: Engagement_Contact_Buyer_No_Attorney__c']");

        By chkNoAttorney = By.XPath("//input[@name='Engagement_Contact_Seller_No_Attorney__c']");
        By chkNoAttorneyBuyer = By.XPath("//input[@name='Engagement_Contact_Buyer_No_Attorney__c']");

        By secCapitalization = By.XPath("//span[text()='Capitalization Details']");
        By subSecCapitalization = By.XPath("//span[text()='Capitalization Details']/ancestor::section[1]/div[2]//h2/span");
        By subSecTimeline = By.XPath("//span[text()='Engagement Timeline']/ancestor::section[1]/div[2]//h3//span");
        By lblSourceOfFundsFields = By.XPath("//span[text()='Capitalization Details']/ancestor::section[1]/div[2]//div[1]/div[contains(@class,'sourcefunds')]//form//div[1]//lightning-output-field/span");
        By btnEditCreditFacility = By.XPath("//button[@title='Edit: Source_Revolving_Credit_Facility__c']");
        By txtCreditFacility = By.XPath("//input[@name='Source_Revolving_Credit_Facility__c']");
        By valCreditFacility = By.XPath("//span[text()='Revolving Credit Facility']/ancestor::div[1]//div[1]/lightning-formatted-text");
        By lblUseOfFundsFields = By.XPath("//span[text()='Capitalization Details']/ancestor::section[1]/div[2]//div[1]/div[contains(@class,'usefunds')]//form//div[1]//lightning-output-field/span");
        By btnEditPurchasePrice = By.XPath("//button[@title='Edit: Use_Purchase_Price__c']");
        By txtPurchasePrice = By.XPath("//input[@name='Use_Purchase_Price__c']");
        By valPurchasePrice = By.XPath("//span[text()='Purchase Price']/ancestor::div[1]//div[1]/lightning-formatted-text");
        By secEngTimeline = By.XPath("//span[text()='Engagement Timeline']");
        By lblBiddingFields = By.XPath("//span[text()='Engagement Timeline']/ancestor::section[1]/div[2]//div[1]/div[contains(@class,'medium')]/div//lightning-output-field/span");
        By btnEditPitchBookDate = By.XPath("//button[@title='Edit: Pitch_Book_Date__c']");
        By txtPitchBookDate = By.XPath("//input[@name='Pitch_Book_Date__c']");
        By valPitchBookDate = By.XPath("//span[text()='Pitch Book Date']/ancestor::div[1]//div[1]/lightning-formatted-text");
        By msgDateEngaged = By.XPath("//span[text()='Date Engaged']/ancestor::div[1]//lightning-helptext//span[2]");
        By lblSigningFields = By.XPath("//span[text()='Engagement Timeline']/ancestor::section[1]/div[2]//div[1]/div[contains(@class,'secondaryRecord')]/div//lightning-output-field/span");
        By btnSigningDate = By.XPath("//button[@title='Edit: Closing_Bid_Due_Date__c']");
        By txtSigningDate = By.XPath("//input[@name='Closing_Bid_Due_Date__c']");
        By valSigningDate = By.XPath("//span[text()='Signing Date']/ancestor::div[1]//div[1]/lightning-formatted-text");
        By iconBuyerStratergy = By.XPath("//span[text()='Buyer Strategy']/ancestor::button[1]/c-hl-universal-pop-over/div/lightning-icon");
        By lblClosingFields = By.XPath("//span[text()='Engagement Timeline']/ancestor::section[1]/div[2]//div[1]/div[contains(@class,'tertiaryRecord')]/div//lightning-output-field/span");
        By msgBuyerStrategy1 = By.XPath("//span[text()='Buyer Strategy']/ancestor::button//section//ul//li[1]/span");
        By msgBuyerStrategy2 = By.XPath("//span[text()='Buyer Strategy']/ancestor::button//section//ul//li[2]/span");
        By btnEditBuyerProcessType = By.XPath("//button[@title='Edit: Buyer_Process_Type__c']");
        By btnBuyerProcessType = By.XPath("//button[@name='Buyer_Process_Type__c']");
        By valBuyerProcessType = By.XPath("//button[@name='Buyer_Process_Type__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span");
        By btnBuyerPlatType = By.XPath("//button[@name='Buyer_Platform_Type__c']");
        By valBuyerPlatType = By.XPath("//button[@name='Buyer_Platform_Type__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span");
        By valProcessTypePostSave = By.XPath("//span[text()='Buyer Process Type']/ancestor::div[1]//div[1]/lightning-formatted-text");
        By valPlatformTypePostSave = By.XPath("   //span[text()='Buyer Platform Type']/ancestor::div[1]//div[1]/lightning-formatted-text");


        public void ClickEngagementDynamicsSection()
        {
            Thread.Sleep(5000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(lblEngagementDynamicsSection)).Perform();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblEngagementDynamicsSection, 120);
            driver.FindElement(lblEngagementDynamicsSection).Click();
            Thread.Sleep(10000);
        }

        public bool VerifySectionsExistsUnderEngagementDynamicsSection()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(lblFeesActualAmount)).Perform();

            bool result = false;
            if(driver.FindElement(lblFeesActualAmount).Displayed && driver.FindElement(lblTransactionFeeCalcActualAmount).Displayed && driver.FindElement(lblIncentiveStructureActualAmount).Displayed && driver.FindElement(lblTransactionActualAmount).Displayed && driver.FindElement(lblTotalsActualAmount).Displayed)
            {
                result = true;
            }
            return result;
        }

        public string GetFeeSubjectToContingentFeesHelpText()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(iconFeeSubjectToContingentFeesInfo)).Perform();

            CustomFunctions.MouseOver(driver, iconFeeSubjectToContingentFeesInfo, 60);
            Thread.Sleep(2000);
            string result = driver.FindElement(lblFeeSubjectToContingentFeesHelpText).Text;
            return result;
        }

        public string GetTotalCreditsHelpText()
        {
            CustomFunctions.MouseOver(driver, iconTotalCreditsInfo, 60);
            Thread.Sleep(2000);
            string result = driver.FindElement(lblTotalCreditsHelpText).Text;
            return result;
        }

        public string GetTransactionValueForFeeCalcHelpText()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(iconTransactionValueForFeeCalcInfo)).Perform();
            Thread.Sleep(2000);

            CustomFunctions.MouseOver(driver, iconTransactionValueForFeeCalcInfo, 60);
            Thread.Sleep(2000);
            string result = driver.FindElement(lblTransactionValueForFeeCalcHelpText).Text;
            return result;
        }

        public string GetTransactionFeeHelpText()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(iconTransactionFeeInfo)).Perform();
            Thread.Sleep(2000);

            CustomFunctions.MouseOver(driver, iconTransactionFeeInfo, 60);
            Thread.Sleep(2000);
            string result = driver.FindElement(lblTransactionFeeHelpText).Text;
            return result;
        }

        public string GetTotalsHelpText()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(iconTotalsInfo)).Perform();
            Thread.Sleep(2000);

            CustomFunctions.MouseOver(driver, iconTotalsInfo, 60);
            Thread.Sleep(2000);
            string result1 = driver.FindElement(lblTotalsHelpText1).Text;
            string result2 = driver.FindElement(lblTotalsHelpText2).Text;
            string result = result1 + " " + result2;
            return result;
        }

        public string GetIncentiveStructureHelpText()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(iconIncentiveStructureInfo)).Perform();
            Thread.Sleep(2000);

            CustomFunctions.MouseOver(driver, iconIncentiveStructureInfo, 60);
            Thread.Sleep(2000);
            string result1 = driver.FindElement(lblIncentiveStructureHelpText1).Text;
            string result2 = driver.FindElement(lblIncentiveStructureHelpText2).Text;
            string result = result1 + " " + result2;
            return result;
        }

        public string GetFeesHelpText()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(iconFeesInfo)).Perform();
            Thread.Sleep(2000);

            CustomFunctions.MouseOver(driver, iconFeesInfo, 60);
            Thread.Sleep(2000);
            string result1 = driver.FindElement(lblFeeHelpText1).Text;
            string result2 = driver.FindElement(lblFeeHelpText2).Text;
            string result = result1 + " " + result2;
            return result;
        }

        public void EnterFeesUnderFeeSection(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,1000)");
            Thread.Sleep(2000);

            driver.FindElement(btnEditFeeDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtRetainerFees).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 1));
            driver.FindElement(txtCompletionOfCIM).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 2));
            driver.FindElement(txtFirstRoundBid).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 3));
            driver.FindElement(txtSecondRoundBid).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 4));
            driver.FindElement(txtLOI).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 5));
            driver.FindElement(txtSignedAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 6));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            driver.FindElement(btnEditOtherFee).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtOtherFee01).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 7));
            driver.FindElement(txtOtherFee02).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 8));
            Thread.Sleep(2000);

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);
        }

        public string ValidateCancelFunctionlaityOfFeeSection(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,1000)");
            Thread.Sleep(2000);

            driver.FindElement(btnEditFeeDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtRetainerFees).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 1));
            driver.FindElement(txtCompletionOfCIM).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 2));
            driver.FindElement(txtFirstRoundBid).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 3));
            driver.FindElement(txtSecondRoundBid).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 4));
            driver.FindElement(txtLOI).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 5));
            driver.FindElement(txtSignedAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 6));

            driver.FindElement(btnCancel).Click();

            string value = driver.FindElement(valRetainerFees).Text;
            return value;            
        }

        public string ValidateSaveFunctionlaityOfFeeSection(string file, string fees)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,1000)");
            Thread.Sleep(2000);

            driver.FindElement(btnEditFeeDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtRetainerFees).Clear();
            driver.FindElement(txtRetainerFees).SendKeys(fees);
            driver.FindElement(txtCompletionOfCIM).Clear();
            driver.FindElement(txtCompletionOfCIM).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 2));
            driver.FindElement(txtFirstRoundBid).Clear();
            driver.FindElement(txtFirstRoundBid).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 3));
            driver.FindElement(txtSecondRoundBid).Clear();           
            driver.FindElement(txtSecondRoundBid).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 4));
            driver.FindElement(txtLOI).Clear();
            driver.FindElement(txtLOI).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 5));
            driver.FindElement(txtSignedAgreement).Clear();
            driver.FindElement(txtSignedAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "Fees(Actual Amount)", 6));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(6000);
            string value = driver.FindElement(valRetainerFees).Text;
            return value.Substring(0,9);
        }

        public bool VerifyCalculationForFeeSectionWhenCrediableAreUnchecked()
        {
            bool result = false;

            string retFees = driver.FindElement(valRetainerFees).Text;
            string compOfCIM = driver.FindElement(valCompletionOfCIM).Text;
            string firstRdBid = driver.FindElement(valFirstRoundBid).Text;
            string secondRdBid = driver.FindElement(valSecondRoundBid).Text;
            string loi = driver.FindElement(valLOI).Text;
            string sgAgreement = driver.FindElement(valSignedAgreement).Text;
            string otherFee1 = driver.FindElement(valOtherFee01).Text;
            string otherFee2 = driver.FindElement(valOtherFee02).Text;

            double retainerFees = Convert.ToDouble(retFees.Split(' ')[1].Trim());
            double completionOfCIM = Convert.ToDouble(compOfCIM.Split(' ')[1].Trim());
            double firstRoundBid = Convert.ToDouble(firstRdBid.Split(' ')[1].Trim());
            double secondRoundBid = Convert.ToDouble(secondRdBid.Split(' ')[1].Trim());
            double lOI = Convert.ToDouble(loi.Split(' ')[1].Trim());
            double signedAgreement = Convert.ToDouble(sgAgreement.Split(' ')[1].Trim());
            double otherFee01 = Convert.ToDouble(otherFee1.Split(' ')[1].Trim());
            double otherFee02 = Convert.ToDouble(otherFee2.Split(' ')[1].Trim());

            double cal = retainerFees + completionOfCIM + firstRoundBid + secondRoundBid + lOI + signedAgreement + otherFee01 + otherFee02;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            string totalFee = driver.FindElement(lblTotalFee).Text;
            double actualTotalFee = Convert.ToDouble(totalFee.Split(' ')[1].Trim());

            string totalCredits = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCredits = Convert.ToDouble(totalCredits.Split(' ')[1].Trim());

            string totalPaymentOnClosing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosing = Convert.ToDouble(totalPaymentOnClosing.Split(' ')[1].Trim());

            if (actualTotalFee == cal && actualTotalCredits == 0 && actualTotalPaymentOnClosing == actualTotalFee)
            {
                result = true;
            }

            return result;
        }

        public bool VerifyCalculationForFeeSectionWhenCrediableAreChecked()
        {
            bool result = false;

            string retFees = driver.FindElement(valRetainerFees).Text;
            string compOfCIM = driver.FindElement(valCompletionOfCIM).Text;
            string firstRdBid = driver.FindElement(valFirstRoundBid).Text;
            string secondRdBid = driver.FindElement(valSecondRoundBid).Text;
            string loi = driver.FindElement(valLOI).Text;
            string sgAgreement = driver.FindElement(valSignedAgreement).Text;
            string otherFee1 = driver.FindElement(valOtherFee01).Text;
            string otherFee2 = driver.FindElement(valOtherFee02).Text;

            double retainerFees = Convert.ToDouble(retFees.Split(' ')[1].Trim());
            double completionOfCIM = Convert.ToDouble(compOfCIM.Split(' ')[1].Trim());
            double firstRoundBid = Convert.ToDouble(firstRdBid.Split(' ')[1].Trim());
            double secondRoundBid = Convert.ToDouble(secondRdBid.Split(' ')[1].Trim());
            double lOI = Convert.ToDouble(loi.Split(' ')[1].Trim());
            double signedAgreement = Convert.ToDouble(sgAgreement.Split(' ')[1].Trim());
            double otherFee01 = Convert.ToDouble(otherFee1.Split(' ')[1].Trim());
            double otherFee02 = Convert.ToDouble(otherFee2.Split(' ')[1].Trim());

            double cal = retainerFees + completionOfCIM + firstRoundBid + secondRoundBid + lOI + signedAgreement + otherFee01 + otherFee02;

            driver.FindElement(btnEditFeeDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(chkRetainerFees).Click();
            driver.FindElement(chkCompletionOfCIM).Click();
            driver.FindElement(chkFirstRoundBid).Click();
            driver.FindElement(chkSecondRoundBid).Click();
            driver.FindElement(chkLOI).Click();
            driver.FindElement(chkSignedAgreement).Click();

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            driver.FindElement(btnEditOtherFee).Click();
            Thread.Sleep(2000);

            driver.FindElement(chkOtherFee01).Click();
            driver.FindElement(chkOtherFee02).Click();

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            string totalFee = driver.FindElement(lblTotalFee).Text;
            double actualTotalFee = Convert.ToDouble(totalFee.Split(' ')[1].Trim());

            string totalCredits = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCredits = Convert.ToDouble(totalCredits.Split(' ')[1].Trim());

            string totalPaymentOnClosing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosing = Convert.ToDouble(totalPaymentOnClosing.Split(' ')[1].Trim());

            if (actualTotalFee == 0 && actualTotalCredits == cal &&  actualTotalPaymentOnClosing == -actualTotalCredits)
            {
                result = true;
            }

            return result;
        }

        public bool VerifyCalculationForTransactionFeeCalcWhenIncentiveStructureIsEmpty(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            string totalFeeBeforeEditing = driver.FindElement(lblTotalFee).Text;
            double actualTotalFeeBeforeEditing = Convert.ToDouble(totalFeeBeforeEditing.Split(' ')[1].Trim());

            string totalCreditsBeforeEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsBeforeEditing = Convert.ToDouble(totalCreditsBeforeEditing.Split(' ')[1].Trim());

            string totalPaymentOnClosingBeforeEditing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosingBeforeEditing = Convert.ToDouble(totalPaymentOnClosingBeforeEditing.Split(' ')[1].Trim());

            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(btnEditTransactionFeeCalcDetails)).Perform();
            Thread.Sleep(2000);

            driver.FindElement(btnEditTransactionFeeCalcDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtTransactionValueForFeeCalc).Clear();
            driver.FindElement(txtTransactionValueForFeeCalc).SendKeys(ReadExcelData.ReadData(excelPath, "TransactionFeeCalc", 1));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            string totalFeeAfterEditingTransactionFeeCalc = driver.FindElement(lblTotalFee).Text;
            double actualTotalFeeAfterEditing = Convert.ToDouble(totalFeeAfterEditingTransactionFeeCalc.Split(' ')[1].Trim());

            string totalCreditsAfterEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsAfterEditing = Convert.ToDouble(totalCreditsAfterEditing.Split(' ')[1].Trim());

            string totalPaymentOnClosingAfterEditing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosingAfterEditing = Convert.ToDouble(totalPaymentOnClosingAfterEditing.Split(' ')[1].Trim());

            if (actualTotalFeeBeforeEditing==actualTotalFeeAfterEditing && actualTotalCreditsBeforeEditing==actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing)
            {
                result = true;
            }

            return result;
        }

        public bool VerifyCalculationForTransactionSectionWithDifferentFeeTypes(string file, string transFeeType, int row, double actualTotalPaymentOnClosingBeforeEditing)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            string totalFeeBeforeEditing = driver.FindElement(lblTotalFee).Text;
            double actualTotalFeeBeforeEditing = Convert.ToDouble(totalFeeBeforeEditing.Split(' ')[1].Trim());

            string totalCreditsBeforeEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsBeforeEditing = Convert.ToDouble(totalCreditsBeforeEditing.Split(' ')[1].Trim());

            driver.FindElement(btnEditTransactionDetails).Click();
            Thread.Sleep(2000);
            driver.FindElement(dropdownTransactionType).SendKeys(transFeeType);
            driver.FindElement(dropdownTransactionType).SendKeys(Keys.Enter);
            driver.FindElement(txtTransactionFee).Clear();
            driver.FindElement(txtTransactionFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Transaction(Actual Amount)", row, 2));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string transFee = driver.FindElement(valTransactionFee).Text;
            double actualTransFee = Convert.ToDouble(transFee.Split(' ')[1].Trim());

            if(transFeeType == "Minimum Fee")
            {
                double actualTotalFee = actualTotalFeeBeforeEditing + actualTransFee;

                string totalFeeAfterEditingTransactionFee = driver.FindElement(lblTotalFee).Text;
                double actualTotalFeeAfterEditing = Convert.ToDouble(totalFeeAfterEditingTransactionFee.Split(' ')[1].Trim());

                string totalCreditsAfterEditing = driver.FindElement(lblTotalCredits).Text;
                double actualTotalCreditsAfterEditing = Convert.ToDouble(totalCreditsAfterEditing.Split(' ')[1].Trim());

                string totalPaymentOnClosingAfterEditing = driver.FindElement(lblPaymentOnClosing).Text;
                double actualTotalPaymentOnClosingAfterEditing = Convert.ToDouble(totalPaymentOnClosingAfterEditing.Split(' ')[1].Trim());

                if (actualTotalFeeAfterEditing==actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + actualTotalFeeAfterEditing)
                {
                    result = true;
                }
            }
            else if(transFeeType == "Flat Fee")
            {
                double actualTotalFee = actualTransFee;

                string totalFeeAfterEditingTransactionFee = driver.FindElement(lblTotalFee).Text;
                double actualTotalFeeAfterEditing = Convert.ToDouble(totalFeeAfterEditingTransactionFee.Split(' ')[1].Trim());

                string totalCreditsAfterEditing = driver.FindElement(lblTotalCredits).Text;
                double actualTotalCreditsAfterEditing = Convert.ToDouble(totalCreditsAfterEditing.Split(' ')[1].Trim());

                string totalPaymentOnClosingAfterEditing = driver.FindElement(lblPaymentOnClosing).Text;
                double actualTotalPaymentOnClosingAfterEditing = Convert.ToDouble(totalPaymentOnClosingAfterEditing.Split(' ')[1].Trim());

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + actualTotalFeeAfterEditing)
                {
                    result = true;
                }
            }

            return result;
        }

        public double GetactualTotalFeeBeforeEditing()
        {
            Thread.Sleep(5000);
            string totalFee = driver.FindElement(lblTotalFee).Text;
            double actualTotalFee = Convert.ToDouble(totalFee.Split(' ')[1].Trim());

            return actualTotalFee;
        }

        public double GetactualPaymentClosingFeeBeforeEditing()
        {
            Thread.Sleep(5000);
            string totalPaymentOnClosingFee = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosingFee = Convert.ToDouble(totalPaymentOnClosingFee.Split(' ')[1].Trim());

            return actualTotalPaymentOnClosingFee;
        }

        public bool VerifyCalculationForFirstRatchetIncentiveStructure(string file, int row, double actualTotalFeeBeforeEditing, double actualTotalPaymentOnClosingBeforeEditing)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            string transactionFeeCalc = driver.FindElement(valTransactionValueForFeeCalc).Text;
            double actualtransactionFeeCalc = Convert.ToDouble(transactionFeeCalc.Split(' ')[1].Trim());

            string totalCreditsBeforeEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsBeforeEditing = Convert.ToDouble(totalCreditsBeforeEditing.Split(' ')[1].Trim());

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtFirstRatchetPercent).Clear();
            driver.FindElement(txtFirstRatchetPercent).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 1));
            driver.FindElement(txtFirstRatchetFromAmount).Clear();
            driver.FindElement(txtFirstRatchetFromAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 2));
            driver.FindElement(txtFirstRatchetToAmount).Clear();
            driver.FindElement(txtFirstRatchetToAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 3));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string firstPercentage = driver.FindElement(valFirstRatchetPercent).Text;
            double firstPercentValue = Convert.ToDouble(firstPercentage.Split('%')[0].Trim());
            string firstRacFromAmount = driver.FindElement(valFirstRatchetFromAmount).Text;
            double firstRacFromAmountValue = Convert.ToDouble(firstRacFromAmount.Split(' ')[1].Trim());
            string firstRacToAmount = driver.FindElement(valFirstRatchetToAmount).Text;
            double firstRacToAmountValue = Convert.ToDouble(firstRacToAmount.Split(' ')[1].Trim());

            string totalFeeAfterEditingTransactionFee = driver.FindElement(lblTotalFee).Text;
            double actualTotalFeeAfterEditing = Convert.ToDouble(totalFeeAfterEditingTransactionFee.Split(' ')[1].Trim());

            string totalCreditsAfterEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsAfterEditing = Convert.ToDouble(totalCreditsAfterEditing.Split(' ')[1].Trim());

            string totalPaymentOnClosingAfterEditing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosingAfterEditing = Convert.ToDouble(totalPaymentOnClosingAfterEditing.Split(' ')[1].Trim());

            if (firstRacToAmountValue <= actualtransactionFeeCalc)
            {
                double firstRatchetCalc= (firstRacToAmountValue - firstRacFromAmountValue) * firstPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + firstRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + firstRatchetCalc)
                {
                    result = true;
                }
            }
            else
            {
                double firstRatchetCalc = (actualtransactionFeeCalc - firstRacFromAmountValue) * firstPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + firstRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + firstRatchetCalc)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool VerifyCalculationForSecondRatchetIncentiveStructure(string file, int row, double actualTotalFeeBeforeEditing, double actualTotalPaymentOnClosingBeforeEditing)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            string transactionFeeCalc = driver.FindElement(valTransactionValueForFeeCalc).Text;
            double actualtransactionFeeCalc = Convert.ToDouble(transactionFeeCalc.Split(' ')[1].Trim());

            string totalCreditsBeforeEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsBeforeEditing = Convert.ToDouble(totalCreditsBeforeEditing.Split(' ')[1].Trim());

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtSecondRatchetPercent).Clear();
            driver.FindElement(txtSecondRatchetPercent).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 1));
            driver.FindElement(txtSecondRatchetFromAmount).Clear();
            driver.FindElement(txtSecondRatchetFromAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 4));
            driver.FindElement(txtSecondRatchetToAmount).Clear();
            driver.FindElement(txtSecondRatchetToAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 5));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string secondPercentage = driver.FindElement(valSecondRatchetPercent).Text;
            double secondPercentValue = Convert.ToDouble(secondPercentage.Split('%')[0].Trim());
            string secondRacFromAmount = driver.FindElement(valSecondRatchetFromAmount).Text;
            double secondRacFromAmountValue = Convert.ToDouble(secondRacFromAmount.Split(' ')[1].Trim());
            string secondRacToAmount = driver.FindElement(valSecondRatchetToAmount).Text;
            double secondRacToAmountValue = Convert.ToDouble(secondRacToAmount.Split(' ')[1].Trim());

            string totalFeeAfterEditingTransactionFee = driver.FindElement(lblTotalFee).Text;
            double actualTotalFeeAfterEditing = Convert.ToDouble(totalFeeAfterEditingTransactionFee.Split(' ')[1].Trim());

            string totalCreditsAfterEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsAfterEditing = Convert.ToDouble(totalCreditsAfterEditing.Split(' ')[1].Trim());

            string totalPaymentOnClosingAfterEditing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosingAfterEditing = Convert.ToDouble(totalPaymentOnClosingAfterEditing.Split(' ')[1].Trim());

            if (secondRacToAmountValue <= actualtransactionFeeCalc)
            {
                double secondRatchetCalc = (secondRacToAmountValue - secondRacFromAmountValue) * secondPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + secondRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + secondRatchetCalc)
                {
                    result = true;
                }
            }
            else
            {
                double secondRatchetCalc = (actualtransactionFeeCalc - secondRacFromAmountValue) * secondPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + secondRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + secondRatchetCalc)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool VerifyCalculationForThirdRatchetIncentiveStructure(string file, int row, double actualTotalFeeBeforeEditing, double actualTotalPaymentOnClosingBeforeEditing)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            string transactionFeeCalc = driver.FindElement(valTransactionValueForFeeCalc).Text;
            double actualtransactionFeeCalc = Convert.ToDouble(transactionFeeCalc.Split(' ')[1].Trim());

            string totalCreditsBeforeEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsBeforeEditing = Convert.ToDouble(totalCreditsBeforeEditing.Split(' ')[1].Trim());

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtThirdRatchetPercent).Clear();
            driver.FindElement(txtThirdRatchetPercent).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 1));
            driver.FindElement(txtThirdRatchetFromAmount).Clear();
            driver.FindElement(txtThirdRatchetFromAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 6));
            driver.FindElement(txtThirdRatchetToAmount).Clear();
            driver.FindElement(txtThirdRatchetToAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 7));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string thirdPercentage = driver.FindElement(valThirdRatchetPercent).Text;
            double thirdPercentValue = Convert.ToDouble(thirdPercentage.Split('%')[0].Trim());
            string thirdRacFromAmount = driver.FindElement(valThirdRatchetFromAmount).Text;
            double thirdRacFromAmountValue = Convert.ToDouble(thirdRacFromAmount.Split(' ')[1].Trim());
            string thirdRacToAmount = driver.FindElement(valThirdRatchetToAmount).Text;
            double thirdRacToAmountValue = Convert.ToDouble(thirdRacToAmount.Split(' ')[1].Trim());

            string totalFeeAfterEditingTransactionFee = driver.FindElement(lblTotalFee).Text;
            double actualTotalFeeAfterEditing = Convert.ToDouble(totalFeeAfterEditingTransactionFee.Split(' ')[1].Trim());

            string totalCreditsAfterEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsAfterEditing = Convert.ToDouble(totalCreditsAfterEditing.Split(' ')[1].Trim());

            string totalPaymentOnClosingAfterEditing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosingAfterEditing = Convert.ToDouble(totalPaymentOnClosingAfterEditing.Split(' ')[1].Trim());

            if (thirdRacToAmountValue <= actualtransactionFeeCalc)
            {
                double thirdRatchetCalc = (thirdRacToAmountValue - thirdRacFromAmountValue) * thirdPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + thirdRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + thirdRatchetCalc)
                {
                    result = true;
                }
            }
            else
            {
                double thirdRatchetCalc = (actualtransactionFeeCalc - thirdRacFromAmountValue) * thirdPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + thirdRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + thirdRatchetCalc)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool VerifyCalculationForFourthRatchetIncentiveStructure(string file, int row, double actualTotalFeeBeforeEditing, double actualTotalPaymentOnClosingBeforeEditing)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            string transactionFeeCalc = driver.FindElement(valTransactionValueForFeeCalc).Text;
            double actualtransactionFeeCalc = Convert.ToDouble(transactionFeeCalc.Split(' ')[1].Trim());

            string totalCreditsBeforeEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsBeforeEditing = Convert.ToDouble(totalCreditsBeforeEditing.Split(' ')[1].Trim());

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtFourthRatchetPercent).Clear();
            driver.FindElement(txtFourthRatchetPercent).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 1));
            driver.FindElement(txtFourthRatchetFromAmount).Clear();
            driver.FindElement(txtFourthRatchetFromAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 8));
            driver.FindElement(txtFourthRatchetToAmount).Clear();
            driver.FindElement(txtFourthRatchetToAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 9));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string fourthPercentage = driver.FindElement(valFourthRatchetPercent).Text;
            double fourthPercentValue = Convert.ToDouble(fourthPercentage.Split('%')[0].Trim());
            string fourthRacFromAmount = driver.FindElement(valFourthRatchetFromAmount).Text;
            double fourthRacFromAmountValue = Convert.ToDouble(fourthRacFromAmount.Split(' ')[1].Trim());
            string fourthRacToAmount = driver.FindElement(valFourthRatchetToAmount).Text;
            double fourthRacToAmountValue = Convert.ToDouble(fourthRacToAmount.Split(' ')[1].Trim());

            string totalFeeAfterEditingTransactionFee = driver.FindElement(lblTotalFee).Text;
            double actualTotalFeeAfterEditing = Convert.ToDouble(totalFeeAfterEditingTransactionFee.Split(' ')[1].Trim());

            string totalCreditsAfterEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsAfterEditing = Convert.ToDouble(totalCreditsAfterEditing.Split(' ')[1].Trim());

            string totalPaymentOnClosingAfterEditing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosingAfterEditing = Convert.ToDouble(totalPaymentOnClosingAfterEditing.Split(' ')[1].Trim());

            if (fourthRacToAmountValue <= actualtransactionFeeCalc)
            {
                double fourthRatchetCalc = (fourthRacToAmountValue - fourthRacFromAmountValue) * fourthPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + fourthRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + fourthRatchetCalc)
                {
                    result = true;
                }
            }
            else
            {
                double fourthRatchetCalc = (actualtransactionFeeCalc - fourthRacFromAmountValue) * fourthPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + fourthRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + fourthRatchetCalc)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool VerifyCalculationForFinalRatchetIncentiveStructure(string file, int row, double actualTotalFeeBeforeEditing, double actualTotalPaymentOnClosingBeforeEditing)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            string transactionFeeCalc = driver.FindElement(valTransactionValueForFeeCalc).Text;
            double actualtransactionFeeCalc = Convert.ToDouble(transactionFeeCalc.Split(' ')[1].Trim());

            string totalCreditsBeforeEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsBeforeEditing = Convert.ToDouble(totalCreditsBeforeEditing.Split(' ')[1].Trim());

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtFinalRatchetPercent).Clear();
            driver.FindElement(txtFinalRatchetPercent).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 1));
            driver.FindElement(txtFinalRatchetAmount).Clear();
            driver.FindElement(txtFinalRatchetAmount).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "IncentiveStructure", row, 10));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string finalPercentage = driver.FindElement(valFinalRatchetPercent).Text;
            double finalPercentValue = Convert.ToDouble(finalPercentage.Split('%')[0].Trim());
            string finalRacAmount = driver.FindElement(valFinalRatchetAmount).Text;
            double finalRacAmountValue = Convert.ToDouble(finalRacAmount.Split(' ')[1].Trim());

            string totalFeeAfterEditingTransactionFee = driver.FindElement(lblTotalFee).Text;
            double actualTotalFeeAfterEditing = Convert.ToDouble(totalFeeAfterEditingTransactionFee.Split(' ')[1].Trim());

            string totalCreditsAfterEditing = driver.FindElement(lblTotalCredits).Text;
            double actualTotalCreditsAfterEditing = Convert.ToDouble(totalCreditsAfterEditing.Split(' ')[1].Trim());

            string totalPaymentOnClosingAfterEditing = driver.FindElement(lblPaymentOnClosing).Text;
            double actualTotalPaymentOnClosingAfterEditing = Convert.ToDouble(totalPaymentOnClosingAfterEditing.Split(' ')[1].Trim());


            if (finalRacAmountValue <= actualtransactionFeeCalc)
            {
                double finalRatchetCalc = (actualtransactionFeeCalc - finalRacAmountValue) * finalPercentValue / 100;
                double actualTotalFee = actualTotalFeeBeforeEditing + finalRatchetCalc;

                if (actualTotalFeeAfterEditing == actualTotalFee && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + finalRatchetCalc)
                {
                    result = true;
                }
            }
            else
            {
                double finalRatchetCalc = 0;
                if (actualTotalFeeAfterEditing == actualTotalFeeBeforeEditing && actualTotalCreditsBeforeEditing == actualTotalCreditsAfterEditing && actualTotalPaymentOnClosingAfterEditing == actualTotalPaymentOnClosingBeforeEditing + finalRatchetCalc)
                {
                    result = true;
                }
            }

            return result;
        }

        public bool VerifyValidationRulesForBlankValuesOfFirstRatchetAmount(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtFirstRatchetPercent).Clear();
            driver.FindElement(txtFirstRatchetPercent).SendKeys(ReadExcelData.ReadData(excelPath, "IncentiveStructure", 1));
            
            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string headerError = driver.FindElement(lblHeaderErrorMsg).Text;
            string firstRatchetFromErrMsg = driver.FindElement(lblFirstRatchetFromAmtErrMsg).Text;
            string firstRatchetToErrMsg = driver.FindElement(lblFirstRatchetToAmtErrMsg).Text;

            if (headerError == ReadExcelData.ReadData(excelPath, "ErrorMessages", 1) && firstRatchetFromErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 2) && firstRatchetToErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 3))
            {
                result = true;
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyValidationRulesForBlankValuesOfSecondRatchetAmount(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtSecondRatchetPercent).Clear();
            driver.FindElement(txtSecondRatchetPercent).SendKeys(ReadExcelData.ReadData(excelPath, "IncentiveStructure", 1));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string headerError = driver.FindElement(lblHeaderErrorMsg).Text;
            string secondRatchetFromErrMsg = driver.FindElement(lblSecondRatchetFromAmtErrMsg).Text;
            string secondRatchetToErrMsg = driver.FindElement(lblSecondRatchetToAmtErrMsg).Text;

            if (headerError == ReadExcelData.ReadData(excelPath, "ErrorMessages", 1) && secondRatchetFromErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 2) && secondRatchetToErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 3))
            {
                result = true;
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyValidationRulesForBlankValuesOfThirdRatchetAmount(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtThirdRatchetPercent).Clear();
            driver.FindElement(txtThirdRatchetPercent).SendKeys(ReadExcelData.ReadData(excelPath, "IncentiveStructure", 1));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string headerError = driver.FindElement(lblHeaderErrorMsg).Text;
            string thirdRatchetFromErrMsg = driver.FindElement(lblThirdRatchetFromAmtErrMsg).Text;
            string thirdRatchetToErrMsg = driver.FindElement(lblThirdRatchetToAmtErrMsg).Text;

            if (headerError == ReadExcelData.ReadData(excelPath, "ErrorMessages", 1) && thirdRatchetFromErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 2) && thirdRatchetToErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 3))
            {
                result = true;
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyValidationRulesForBlankValuesOfFourthRatchetAmount(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtFourthRatchetPercent).Clear();
            driver.FindElement(txtFourthRatchetPercent).SendKeys(ReadExcelData.ReadData(excelPath, "IncentiveStructure", 1));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string headerError = driver.FindElement(lblHeaderErrorMsg).Text;
            string fourthRatchetFromErrMsg = driver.FindElement(lblFourthRatchetFromAmtErrMsg).Text;
            string fourthRatchetToErrMsg = driver.FindElement(lblFourthRatchetToAmtErrMsg).Text;

            if (headerError == ReadExcelData.ReadData(excelPath, "ErrorMessages", 1) && fourthRatchetFromErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 2) && fourthRatchetToErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 3))
            {
                result = true;
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyValidationRulesWhenFirstRatchetFromAmountIsGreaterThanFirstRatchetToAmount(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtFirstRatchetFromAmount).Clear();
            driver.FindElement(txtFirstRatchetFromAmount).SendKeys(ReadExcelData.ReadData(excelPath, "ErrorMessages", 5));
            driver.FindElement(txtFirstRatchetToAmount).Clear();
            driver.FindElement(txtFirstRatchetToAmount).SendKeys(ReadExcelData.ReadData(excelPath, "ErrorMessages", 6));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string firstRacFromAmount = ReadExcelData.ReadData(excelPath, "ErrorMessages", 5);
            double firstRacFromAmountValue = Convert.ToDouble(firstRacFromAmount);
            string firstRacToAmount = ReadExcelData.ReadData(excelPath, "ErrorMessages", 6);
            double firstRacToAmountValue = Convert.ToDouble(firstRacToAmount);

            string headerError = driver.FindElement(lblHeaderErrorMsg).Text;
            string firstRatchetToErrMsg = driver.FindElement(lblFirstRatchetToAmtErrMsg).Text;

            if(firstRacFromAmountValue > firstRacToAmountValue)
            {
                if (headerError == ReadExcelData.ReadData(excelPath, "ErrorMessages", 1) && firstRatchetToErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 4))
                {
                    result = true;
                }
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyValidationRulesWhenSecondRatchetFromAmountIsGreaterThanSecondRatchetToAmount(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtSecondRatchetFromAmount).Clear();
            driver.FindElement(txtSecondRatchetFromAmount).SendKeys(ReadExcelData.ReadData(excelPath, "ErrorMessages", 5));
            driver.FindElement(txtSecondRatchetToAmount).Clear();
            driver.FindElement(txtSecondRatchetToAmount).SendKeys(ReadExcelData.ReadData(excelPath, "ErrorMessages", 6));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string secondRacFromAmount = ReadExcelData.ReadData(excelPath, "ErrorMessages", 5);
            double secondRacFromAmountValue = Convert.ToDouble(secondRacFromAmount);
            string secondRacToAmount = ReadExcelData.ReadData(excelPath, "ErrorMessages", 6);
            double secondRacToAmountValue = Convert.ToDouble(secondRacToAmount);

            string headerError = driver.FindElement(lblHeaderErrorMsg).Text;
            string secondRatchetToErrMsg = driver.FindElement(lblSecondRatchetToAmtErrMsg).Text;

            if (secondRacFromAmountValue > secondRacToAmountValue)
            {
                if (headerError == ReadExcelData.ReadData(excelPath, "ErrorMessages", 1) && secondRatchetToErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 4))
                {
                    result = true;
                }
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyValidationRulesWhenThirdRatchetFromAmountIsGreaterThanThirdRatchetToAmount(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtThirdRatchetFromAmount).Clear();
            driver.FindElement(txtThirdRatchetFromAmount).SendKeys(ReadExcelData.ReadData(excelPath, "ErrorMessages", 5));
            driver.FindElement(txtThirdRatchetToAmount).Clear();
            driver.FindElement(txtThirdRatchetToAmount).SendKeys(ReadExcelData.ReadData(excelPath, "ErrorMessages", 6));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string thirdRacFromAmount = ReadExcelData.ReadData(excelPath, "ErrorMessages", 5);
            double thirdRacFromAmountValue = Convert.ToDouble(thirdRacFromAmount);
            string thirdRacToAmount = ReadExcelData.ReadData(excelPath, "ErrorMessages", 6);
            double thirdRacToAmountValue = Convert.ToDouble(thirdRacToAmount);

            string headerError = driver.FindElement(lblHeaderErrorMsg).Text;
            string thirdRatchetToErrMsg = driver.FindElement(lblThirdRatchetToAmtErrMsg).Text;

            if (thirdRacFromAmountValue > thirdRacToAmountValue)
            {
                if (headerError == ReadExcelData.ReadData(excelPath, "ErrorMessages", 1) && thirdRatchetToErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 4))
                {
                    result = true;
                }
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyValidationRulesWhenFourthRatchetFromAmountIsGreaterThanFourthRatchetToAmount(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            driver.FindElement(btnEditIncentiveStructureDetails).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtFourthRatchetFromAmount).Clear();
            driver.FindElement(txtFourthRatchetFromAmount).SendKeys(ReadExcelData.ReadData(excelPath, "ErrorMessages", 5));
            driver.FindElement(txtFourthRatchetToAmount).Clear();
            driver.FindElement(txtFourthRatchetToAmount).SendKeys(ReadExcelData.ReadData(excelPath, "ErrorMessages", 6));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);

            string fourthRacFromAmount = ReadExcelData.ReadData(excelPath, "ErrorMessages", 5);
            double fourthRacFromAmountValue = Convert.ToDouble(fourthRacFromAmount);
            string fourthRacToAmount = ReadExcelData.ReadData(excelPath, "ErrorMessages", 6);
            double fourthRacToAmountValue = Convert.ToDouble(fourthRacToAmount);

            string headerError = driver.FindElement(lblHeaderErrorMsg).Text;
            string fourthRatchetToErrMsg = driver.FindElement(lblFourthRatchetToAmtErrMsg).Text;

            if (fourthRacFromAmountValue > fourthRacToAmountValue)
            {
                if (headerError == ReadExcelData.ReadData(excelPath, "ErrorMessages", 1) && fourthRatchetToErrMsg == ReadExcelData.ReadData(excelPath, "ErrorMessages", 4))
                {
                    result = true;
                }
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);
            return result;
        }

        public void NavigateToCFEngSummaryPage(string engName)
        {
            Thread.Sleep(10000);
            driver.FindElement(btnSearchBox).Click();
            Thread.Sleep(2000);
            driver.FindElement(txtSearchBox).SendKeys(engName);
            driver.FindElement(txtSearchBox).SendKeys(Keys.Enter);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"(//a[@title='{engName}'])[2]")).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnAdditionalTabs).Click();
            driver.FindElement(linkCFEngSummary).Click();
            Thread.Sleep(3000);
        }
        //Get LOB on the header
        public string ValidateLOBOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblLOB);
            string status = driver.FindElement(lblLOB).Text;
            return status;
        }
        public string ValidateLOBValueOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLOB);
            string value = driver.FindElement(valLOB).Text;
            return value;
        }

        public string ValidateEngNumOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblEngNum);
            string status = driver.FindElement(lblEngNum).Text;
            return status;
        }
        public string ValidateEngNumValueOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEngNum);
            string value = driver.FindElement(valEngNum).Text;
            return value;
        }

        public string ValidateSubOwnerOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblSubOwner);
            string status = driver.FindElement(lblSubOwner).Text;
            return status;
        }
        public string ValidateSubOwnerValueOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSubOwner);
            string value = driver.FindElement(valSubOwner).Text;
            return value;
        }

        //Get Ext Disclosure Status on the header
        public string ValidateDiscStatusOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblExtDisclosureStatus);
            string status = driver.FindElement(lblExtDisclosureStatus).Text;
            return status;
        }
        public string ValidateDiscStatusValueOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valExtDisclosureStatus);
            string value = driver.FindElement(valExtDisclosureStatus).Text;
            return value;
        }
        
        public string ValidateSubjectOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblSubject);
            string status = driver.FindElement(lblSubject).Text;
            return status;
        }
        public string ValidateSubjectValueOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSubject);
            string value = driver.FindElement(valSubject).Text;
            return value;
        }

        public string ValidateClientOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblClient);
            string status = driver.FindElement(lblClient).Text;
            return status;
        }
        public string ValidateClientValueOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valClient);
            string value = driver.FindElement(valClient).Text;
            return value;
        }
        public string ValidateEstTxnSizeOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblTxnSize);
            string status = driver.FindElement(lblTxnSize).Text;
            return status;
        }
        public string ValidateEstTxnSizeValueOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTxnSize);
            string value = driver.FindElement(valTxnSize).Text;
            return value;
        }
        public string ValidateEstTxnSizeMessageOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgTxnSize);
            string value = driver.FindElement(msgTxnSize).Text;
            return value;
        }

        public string ValidateCloseDateOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCloseDate);
            string status = driver.FindElement(lblCloseDate).Text;
            return status;
        }
        public string ValidateCloseDateValueOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCloseDate);
            string value = driver.FindElement(valCloseDate).Text;
            return value;
        }
        public string ValidateJobTypeOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblJoType);
            string status = driver.FindElement(lblJoType).Text;
            return status;
        }
        public string ValidateJobTypeValueOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valJobType);
            string value = driver.FindElement(valJobType).Text;
            return value;
        }
        public string ValidateEngNumberMessageOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgEngNum);
            string value = driver.FindElement(msgEngNum).Text;
            return value;
        }
        public string ValidateSummaryReportButtonOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSummaryReport);
            string value = driver.FindElement(btnSummaryReport).Text;
            return value;
        }

        public string ConnectCognoAndOpenPDF()
        {

            driver.FindElement(btnSummaryReport).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(12000);
            driver.FindElement(txtCognoUser).SendKeys("SSharma0427");
            driver.FindElement(txtCognoPass).SendKeys("Avika_Ashok@2024");
            driver.FindElement(btnSignin).Click();
            Thread.Sleep(15000);
            string pdf = driver.Url;
            return pdf;
        }


        public string VerifyEngSummaryinReport()
        {
            Thread.Sleep(17000);
            driver.FindElement(By.XPath("//td[8]//td[3]")).Click();
            Thread.Sleep(7000);
            driver.FindElement(By.XPath("//td[text()='View in HTML Format']")).Click();
            Thread.Sleep(22000);            
            string engSummary = driver.FindElement(By.XPath("//tr[3]/td//tr[1]/td/div/span")).Text;
            driver.SwitchTo().Window(driver.WindowHandles.First());
            Thread.Sleep(6000);
            return engSummary;
        }

        public string ValidatePartiesSection()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, secParties);
            string value = driver.FindElement(secParties).Text;
            return value;
        }
        public string ValidateMarketingSection()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, secMarketing);
            string value = driver.FindElement(secMarketing).Text;
            driver.FindElement(secMarketing).Click();
            return value;
        }

        public string ValidateELEngagementsDynamicsSection()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, secELEngDynamics);
            string value = driver.FindElement(secELEngDynamics).Text;
            driver.FindElement(secELEngDynamics).Click();
            return value;
        }

        public string ValidateTypeOfBuyer()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valTypeBuyside);
            string value = driver.FindElement(valTypeBuyside).Text;
            return value;
        }

        public string ValidateCompanyOfBuyer()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyBuyside);
            string value = driver.FindElement(valCompanyBuyside).Text;
            return value;
        }
        public string ValidateIGOfBuyer()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valIGBuysdie);
            string value = driver.FindElement(valIGBuysdie).Text;
            return value;
        }


        public string ValidateOwnershipOfBuyer()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver,valOwnershipBuyside);
            string value = driver.FindElement(valOwnershipBuyside).Text;
            return value;
        }

        public string ValidateSectorOfBuyer()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valSectorBuyside);
            string value = driver.FindElement(valSectorBuyside).Text;
            return value;
        }

        public string ValidateStrategySectionOfBuyer()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver,secBuyerStrategy);
            string value = driver.FindElement(secBuyerStrategy).Text;
            return value;
        }

        public bool VerifyFieldsUnderBuyerStrategySection()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblBuyerStrategy);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Buyer Process Type", "Buyer Platform Type" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }


        public bool VerifySectionsUnderMarketingSection()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(secMarketings);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Outreach Metrics", "Bid History" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public bool VerifySubSectionsUnderELEngDynamicsSection()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(subsecELEngDynamics);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Fees (Actual Amount)", "Transaction (Actual Amount)" , "Transaction Fee Calc (Actual Amount)", "Incentive Structure (Actual Amount)", "Totals (Actual Amount)" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public bool VerifyFieldsUnderFeesActualAmountSection()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblFeesActualFields);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Retainer Fees", "Is Retainer Fee Creditable", "Completion Of CIM", "Is Completion of CIM Creditable", "First Round Bid", "Is First Round Bid Creditable", "Second Round Bid", "Is Second Round Bid Creditable", "LOI", "Is LOI Creditable", "Signed Agreement", "Is Signed Agreement Creditable", "Other Fee Type 01", "Other Fee 01", "Is Other Fee 01 Creditable", "Other Fee Type 02", "Other Fee 02", "Is Other Fee 02 Creditable" };
           
        
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        public bool VerifyColumnssUnderMarketingSection()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(colOutreachMetric);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "", "","", "Percent of Potential Counterparties Contacted", "Percent of Books Sent" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        public bool VerifyColumnsUnderBidHistory()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(colBidHistory);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "", "First Round Bids", "Final Bids", "Change from First Round Bid" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        public bool VerifySubColumnsUnderMarketingSection()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(subcolOutreachMetric);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Metric", "Domestic Strategics", "Intl. Strategics", "Total Strategics", "Financial", "Overall Total", "Domestic Strategics", "Intl Strategics", "Total Strategics", "Financial", "Yield", "Strategic", "Financial","Total" };
            bool isSame = true;

            Console.WriteLine(expectedValue[1]);
            Console.WriteLine(expectedValue[2]);
            Console.WriteLine(expectedValue[3]);
            
            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public bool VerifySubColumnsUnderBidHistorySection()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(subcolBid);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Counterparty List", "Mid-Point Value (MM)", "EBITDA Mult. LTM", "EBITDA Mult. FY+1", "Price/Book", "Mid-Point Value (MM)", "EBITDA Mult. LTM", "EBITDA Mult. FY+1", "Price/Book", "Percent Change", "Mid-Point Value (MM)", "Mid-Point % Value", "EBITDA Multiple LTM" };
            bool isSame = true;

            Console.WriteLine(expectedValue[1]);
            Console.WriteLine(expectedValue[2]);
            Console.WriteLine(expectedValue[3]);

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }



        public bool VerifyRowssUnderMarketingSection()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(rowOutreachMetric);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Potential Counterparties Contacted", "Books Sent", "Preliminary Bids", "Management Presentations", "Final Bids" };
            bool isSame = true;
            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public string GetOverallTotalOfPotentialCounterpartyContacted()
        {
            Thread.Sleep(5000);
            string value = driver.FindElement(valPotCounterparty).Text;
            return value;
        }
        public string ValidateSellerSection()
        {
            driver.FindElement(btnParties).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, secSeller);
            string value = driver.FindElement(secSeller).Text;
            return value;
        }
        public string ValidateBuyerSection()
        {
            
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, secBuyer);
            string value = driver.FindElement(secBuyer).Text;
            return value;
        }

        public string ValidateMandatoryValidationOfBuyerCompany()
        {
            Thread.Sleep(4000);
            driver.FindElement(iconBuyer).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(valCompany).Text;
            return value;

        }
        public bool VerifyFieldsUnderSellerSection()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblSellerSection);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Client", "Client Ownership", "Subject", "Subject Ownership" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public string ValidateSellerIcon()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconSeller);
            string value = driver.FindElement(iconSeller).GetAttribute("variant");
            return value;
        }
        public bool VerifyFieldsUnderSellerBackGroundSection()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblSellerBackgroundSection);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Industry Group", "Sector", "Description" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public string ValidateIGValueOfCompany()
        {

            driver.FindElement(lnkClientCompany).Click();
            Thread.Sleep(7000);
            string value = driver.FindElement(valIGCompany).Text;
            return value;
        }

        public string ValidateSectorValueOfCompany()
        {             
            string value = driver.FindElement(valSectorCompany).Text;
            return value;
        }

        public string ValidateDescriptionValueOfCompany()           
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,450)");
            Thread.Sleep(6000);
            string value = driver.FindElement(valDesc).Text;
            return value;
        }

        public string ValidateIGValueInSellerBackground()
        {            
            Thread.Sleep(7000);
            string value = driver.FindElement(valIGSummary).Text;
            return value;
        }

        public string ValidateSectorValueInSellerBackground()
        {            
            string value = driver.FindElement(valSectorSummary).Text;
            return value;
        }

        public string ValidateDescriptionValueInSellerBackground()
        {           
            string value = driver.FindElement(valDescSummary).Text;
            return value;
        }

        public bool VerifyFieldsUnderSellerDetailsSection()
        {
            driver.FindElement(tabEngsummary).Click();
            Thread.Sleep(5000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,450)");
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblSellerDetailsSection);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Private Company with Public Debt", "Engagement Letter Base", "Pitch EBITDA LTM", "Pitch EBITDA FYE", "Pitch Value Low", "Pitch Value High", "Transaction Rationale" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public string ValidateSellerDetailsIcon()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconSellerDetails);
            driver.FindElement(iconSellerDetails).Click();
            string value = driver.FindElement(lblTxnRationale).Text;
            return value;
        }

        public string GetValueOfTxnRationale()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,350)");
            Thread.Sleep(6000);
            string value = driver.FindElement(valTxnRationaleBeforeUpdate).Text;
            return value;
        }
        public bool VerifyTxnRationaleValues()
        {
            driver.FindElement(btnEditTxnRationale).Click();            
            Thread.Sleep(6000);
            driver.FindElement(btnTxnRationale).Click();
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(valTxnRationale);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Public - Activist Shareholder", "Public - Hostile", "Maximizing Current Value / Proceeds", "Estate Planning / Family Transition", "Distressed Sale", "PE - End of Fund Life", "PE - Investment Maturity" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //Validate Cancel functionality of Selller details
        public string ValidateCancelFunctionalityOfSellerDetailsSection()
        {
            driver.FindElement(By.XPath("//button[@name='Transaction_Rationale__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item[3]/span[2]/span")).Click(); ;
            Thread.Sleep(4000);
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(valTxnRationaleBeforeUpdate).Text;
            return value;
        }


        //Validate Save functionality of Selller details
        public string ValidateSaveFunctionalityOfSellerDetailsSection(string value)
        {
            driver.FindElement(btnEditTxnRationale).Click();
            Thread.Sleep(4000);            
            driver.FindElement(btnTxnRationale).Click();
            driver.FindElement(By.XPath("//button[@name='Transaction_Rationale__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span[text()='"+value+"']")).Click(); ;

            Thread.Sleep(4000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
            string txn = driver.FindElement(valTxnRationaleBeforeUpdate).Text;
            return txn;
        }
        public string ValidateAddRecordIcon()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,650)");
            WebDriverWaits.WaitUntilEleVisible(driver, iconAddRecord);
            driver.FindElement(iconAddRecord).Click();
            string value = driver.FindElement(iconAddRecord).GetAttribute("title");
            return value;
        }
        public string ValidateAddRecordContactIcon()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,650)");
            WebDriverWaits.WaitUntilEleVisible(driver, iconAddRecordContact);
            driver.FindElement(iconAddRecordContact).Click();
            string value = driver.FindElement(iconAddRecordContact).GetAttribute("title");
            return value;
        }

        public string ValidateAddRecordBuyerContactIcon()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,750)");
            WebDriverWaits.WaitUntilEleVisible(driver, iconAddRecordBuyerContact);
            driver.FindElement(iconAddRecordBuyerContact).Click();
            string value = driver.FindElement(iconAddRecordBuyerContact).GetAttribute("title");
            return value;
        }
        public bool VerifyAddRecordFields()
        {            
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblAddRecordSection);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Type", "As Of Date", "Revenue LTM (MM)", "EBITDA LTM (MM)", "Revenue FY (MM)", "EBITDA FY (MM)", "Revenue FY+1 (MM)", "EBITDA FY+1 (MM)", "Net Income LTM (MM)", "Book Value Current (MM)", "Total Assets (MM)", "Currency" };
            Console.WriteLine(expectedValue[1]);
            Console.WriteLine(expectedValue[2]);

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public bool VerifyAddRecordFieldsOfAddContact()
        {
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblAddContactSection);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = {  "*Contact", "Type", "Role", "Description" };
            Console.WriteLine(expectedValue[1]);
            Console.WriteLine(expectedValue[2]);

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public bool ValidateTypeValues()
        {
            driver.FindElement(btnType).Click();
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(valType);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "First", "Closing", "Final", "Second",  "Third" };
            Console.WriteLine(expectedValue[1]);
            Console.WriteLine(expectedValue[2]);

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        public bool ValidateTypeValuesOfSellerContacts()
        {
            driver.FindElement(btnType).Click();
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(valType);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Client", "External" };
            Console.WriteLine(expectedValue[1]);
            Console.WriteLine(expectedValue[2]);

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public bool ValidateRoleValuesOfSellerContacts()
        {
            driver.FindElement(btnRole).Click();
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(valRole);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Attorney", "Board of Directors", "Company Contact", "Equity Sponsor", "External Financial Advisor" };
            Console.WriteLine(expectedValue[1]);
            Console.WriteLine(expectedValue[2]);

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        public string ValidateTypeMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgType);
            string value = driver.FindElement(msgType).Text;
            return value;
        }

        public string ValidateMandatoryMessageOfContact()
        {
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgContact);
            string value = driver.FindElement(msgContact).Text;
            return value;
        }

        public string ValidateSaveFunctionalityOfAddRecord(string amount,  string type)
        {
            Thread.Sleep(5000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,1050)");
            Thread.Sleep(5000);
            driver.FindElement(txtRevMM).SendKeys(amount);
            driver.FindElement(txtEBITDA).SendKeys(amount);
            driver.FindElement(btnType).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//button[@name='Type__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span/span[text()='" + type + "']")).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(6000);
            if (type.Equals("Closing"))
            {
                string value = driver.FindElement(By.XPath("//span[text()='Seller Financials']/ancestor::div[1]//tr[1]/td[2]//lightning-formatted-number")).Text;
                return value;
            }
            else
            {
                string value = driver.FindElement(By.XPath("//span[text()='Seller Financials']/ancestor::div[1]//tr[1]/td[2]//lightning-formatted-number")).Text;
                return value;
            }
        }

        public bool ValidateAddedRecordsInEngDetails()
        {
            driver.Navigate().Refresh();
            Thread.Sleep(4000);
            driver.FindElement(tabEngagementL).Click();
            Thread.Sleep(6000);
            driver.FindElement(tabFees).Click();
            Thread.Sleep(4000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,650)");
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(valAddedRevs);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = {  "GBP 10.00", "GBP 20.00" };
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }


        public string ValidateAddFinancialsFunctionalityOfEngagement()
        {
            driver.FindElement(btnAddFinancial).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtRelatedEng).Click();          
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//input[@placeholder='Search Engagements...']/ancestor::div[4]/div[2]//li[2]/lightning-base-combobox-item/span[1]")).Click();
            driver.FindElement(txtRevMM).SendKeys("25");
            driver.FindElement(txtEBITDA).SendKeys("10");
            driver.FindElement(btnSaveFin).Click();
            Thread.Sleep(8000);
            string value = driver.FindElement(valAddedFin).Text;
            return value;
        }

        public string GetAdded2ndRevenueInSellerFinanials()
        {
            driver.FindElement(btnRefreshFin).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(By.XPath("//span[text()='Seller Financials']/ancestor::div[1]//tr[1]/td[2]//lightning-formatted-number")).Text;
            return value;
        }


        public string ValidateEngFinCheckbox()
        {
                if (driver.FindElement(chkEngFinCheck).Displayed)
                {
                    CustomFunctions.MoveToElement(driver, driver.FindElement(chkEngFinCheck));
                    if (driver.FindElement(chkEngFinCheck).Selected)
                    {
                        return "Engagement Financials Check checkbox is displayed and checked";
                    }
                    else
                    {
                        return "Engagement Financials Check checkbox is displayed and not-checked";
                    }
                }
                else
                {
                    return "Engagement Financials check checkbox is not displayed";
                }
            }
        
        public string ValidateAddFinancialsInCFEngSummary()
        {
            driver.FindElement(tabEngsummary).Click();
            Thread.Sleep(4000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,850)");
            Thread.Sleep(6000);
            driver.FindElement(btnParties).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnRefreshFin).Click();
            Thread.Sleep(4000);            
            string value = driver.FindElement(By.XPath("//span[text()='Seller Financials']/ancestor::div[1]//tr[1]/td[2]//lightning-formatted-number")).Text;
            return value;
        }

        public string ValidateCancelFunctionalityOfAddRecord(String amount)
        {
            driver.FindElement(btnMoreFin).Click();
            Thread.Sleep(4000);
            driver.FindElement(lnkEditRecord).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtRevMM).SendKeys(amount);
            driver.FindElement(txtEBITDA).SendKeys(amount);
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnRefreshFin).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(By.XPath("//span[text()='Seller Financials']/ancestor::div[1]//tr[1]/td[2]//lightning-formatted-number")).Text;
            return value;
        }

        public string ValidateEditFunctionalityOfAddRecord(String amount)
        {
            driver.FindElement(btnMoreFin).Click();
            Thread.Sleep(4000);
            driver.FindElement(lnkEditRecord).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtRevMM).Clear();
            driver.FindElement(txtRevMM).SendKeys(amount);
            driver.FindElement(txtEBITDA).SendKeys(amount);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnRefreshFin).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(By.XPath("//span[text()='Seller Financials']/ancestor::div[1]//tr[1]/td[2]//lightning-formatted-number")).Text;
            return value;
        }
        public string ValidateCancelDeleteFunctionalityOfAddRecord()
        {
            driver.FindElement(btnMoreFin).Click();
            Thread.Sleep(4000);
            driver.FindElement(lnkDeleteRecord).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnWinCancel).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnRefreshFin).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(By.XPath("//span[text()='Seller Financials']/ancestor::div[1]//tr[1]/td[2]//lightning-formatted-number")).Text;
            return value;
        }
        public string ValidateConfirmDeleteFunctionalityOfAddRecord()
        {
            driver.FindElement(btnMoreFin).Click();
            Thread.Sleep(4000);
            driver.FindElement(lnkDeleteRecord).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnWinOk).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnRefreshFin).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(msgDelete).Text;
            return value;
        }
        public string ValidateSellerFinIcon()
        {          
            Thread.Sleep(6000);
            string value = driver.FindElement(iconSellerFin).GetAttribute("variant");
            return value;
        }
        public string ValidateMandatoryMessageOfSellerFin()
        {
            Actions actions = new Actions(driver);
            driver.FindElement(btnClose).Click();
            Thread.Sleep(6000);
            actions.MoveToElement(driver.FindElement(iconSellerFin)).Perform();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSellerFin,250);
            string value = driver.FindElement(msgSellerFin).Text;
            return value;
        }
        public string ValidateEngFinCheckIcon()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconEngFinCheck);
            string value = driver.FindElement(iconEngFinCheck).Text;
            return value;
        }

        public string ValidateSaveContactFunctionality(string name, string role)
        {
            driver.FindElement(txtContact).Click();
            driver.FindElement(txtContact).SendKeys(name);
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//input[@placeholder='Search Contacts...']/ancestor::div[@role='list']//li[1]//span[2]/span[1]")).Click();
            driver.FindElement(btnRole).Click();
            driver.FindElement(By.XPath("//button[@name='Role__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span[text()='"+role+"']")).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(6000);
            string value = driver.FindElement(valAddedContact).Text;
            return value;
        }

        public string ValidateSaveBuyerContactFunctionality(string name, string role)
        {
            Thread.Sleep(7000);
            Console.WriteLine("clicked contact");
            driver.FindElement(txtContact).Click();            
            driver.FindElement(txtContact).SendKeys(name);
            Thread.Sleep(5000);
            Console.WriteLine("entered name");
            driver.FindElement(By.XPath("//input[@placeholder='Search Contacts...']/ancestor::div[@role='list']//li[1]//span[2]/span[1]")).Click();
            driver.FindElement(btnRole).Click();
            driver.FindElement(By.XPath("//button[@name='Role__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + role + "']")).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(6000);
            string value = driver.FindElement(valAddedBuyerContact).Text;
            return value;
        }

        public string ValidateAddedContactInEng()
        {
            Thread.Sleep(6000);
            driver.FindElement(tabSummary).Click();           
            Thread.Sleep(10000);                   
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,1000)");
            Thread.Sleep(6000);
            driver.FindElement(btnRefreshContact).Click();
            Thread.Sleep(7000);          
            string value = driver.FindElement(valAdded2ndContact).Text;
            return value;
        }

        public string ValidateAddedBuyerContactInEng()
        {
            Thread.Sleep(6000);
            driver.FindElement(tabSummaryBuyer).Click();
            Thread.Sleep(10000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,1200)");
            Thread.Sleep(6000);
            driver.FindElement(btnRefreshBuyerContact).Click();
            Thread.Sleep(7000);
            string value = driver.FindElement(valAdded2ndBuyerContact).Text;
            return value;
        }

        public string GetRoleOfContactAddedInEng(string name)
        {
            string value = driver.FindElement(By.XPath("//span[text()='Seller Contacts']/ancestor::div[1]//tbody/tr/th//a[text()='"+name+"']/ancestor::tr[1]/td[3]//lightning-base-formatted-text")).Text;
            return value;
        }


        public string GetRoleOfBuyerContactAddedInEng(string name)
        {
            string value = driver.FindElement(By.XPath("//span[text()='Buyer Contacts']/ancestor::div[1]//tbody/tr/th//a[text()='" + name + "']/ancestor::tr[1]/td[3]//lightning-base-formatted-text")).Text;
            return value;
        }


        public string ValidateCancelContactFunctionality(string name,string role)
        {
            driver.FindElement(btnShowMoreContact).Click();
            driver.FindElement(lnkEdit).Click();
            Thread.Sleep(5000);           
            driver.FindElement(btnRole).Click();
            driver.FindElement(By.XPath("//button[@name='Role__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + role + "']")).Click();
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(6000);
            string value = driver.FindElement(By.XPath("//span[text()='Seller Contacts']/ancestor::div[1]//tbody/tr/th//a[text()='" + name + "']/ancestor::tr[1]/td[3]//lightning-base-formatted-text")).Text;
            return value;
        }

        public string ValidateEditContactFunctionality(string name, string role)
        {
            driver.FindElement(btnShowMoreContact).Click();
            driver.FindElement(lnkEdit).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnRole).Click();
            driver.FindElement(By.XPath("//button[@name='Role__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + role + "']")).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(6000);
            string value = driver.FindElement(By.XPath("//span[text()='Seller Contacts']/ancestor::div[1]//tbody/tr/th//a[text()='" + name + "']/ancestor::tr[1]/td[3]//lightning-base-formatted-text")).Text;
            return value;
        }
        public string ValidateCancelBuyerContactFunctionality(string name, string role)
        {
            driver.FindElement(btnShowMoreBuyerContact).Click();
            driver.FindElement(lnkEdit).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnRole).Click();
            driver.FindElement(By.XPath("//button[@name='Role__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + role + "']")).Click();
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(6000);
            string value = driver.FindElement(By.XPath("//span[text()='Buyer Contacts']/ancestor::div[1]//tbody/tr/th//a[text()='" + name + "']/ancestor::tr[1]/td[3]//lightning-base-formatted-text")).Text;
            return value;
        }

        public string GetBidCounterparty()
        {            
            Thread.Sleep(4000);
            string title = driver.FindElement(valBidCompany).Text;
            return title;
        }
        public string ValidateEditBuyerContactFunctionality(string name, string role)
        {
            driver.FindElement(btnShowMoreBuyerContact).Click();
            driver.FindElement(lnkEdit).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnRole).Click();
            driver.FindElement(By.XPath("//button[@name='Role__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + role + "']")).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(6000);
            string value = driver.FindElement(By.XPath("//span[text()='Buyer Contacts']/ancestor::div[1]//tbody/tr/th//a[text()='" + name + "']/ancestor::tr[1]/td[3]//lightning-base-formatted-text")).Text;
            return value;
        }
        public string ValidateCancelDeleteFunctionalityOfSellerContact()
        {
            driver.FindElement(btnShowMoreContact).Click();
            Thread.Sleep(4000);
            driver.FindElement(lnkDeleteRecord).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnWinCancel).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnRefreshContact).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(valAddedContact).Text;
            return value;
        }
        public string ValidateConfirmDeleteFunctionalityOfSellerContact()
        {
            driver.FindElement(btnShowMoreContact).Click();
            Thread.Sleep(4000);
            driver.FindElement(lnkDeleteRecord).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnWinOk).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnRefreshContact).Click();
            Thread.Sleep(4000);
            try
            {
                string value = driver.FindElement(valAddedContact).Text;
                return value;
            }
            catch
            {
                return "No contact exists";
            }
        }
        public string ValidateCancelDeleteFunctionalityOfBuyerContact()
        {
            driver.FindElement(btnShowMoreBuyerContact).Click();
            Thread.Sleep(4000);
            driver.FindElement(lnkDeleteRecord).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnWinCancel).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnRefreshContact).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(valAddedBuyerContact).Text;
            return value;
        }
        public string ValidateConfirmDeleteFunctionalityOfBuyerContact()
        {
            driver.FindElement(btnShowMoreBuyerContact).Click();
            Thread.Sleep(4000);
            driver.FindElement(lnkDeleteRecord).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnWinOk).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnRefreshContact).Click();
            Thread.Sleep(4000);
            try
            {
                string value = driver.FindElement(valAddedBuyerContact).Text;
                return value;
            }
            catch
            {
                return "No contact exists";
            }
        }
        public string ValidateEngContactCheckIcon()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconEngContactCheck);
            string value = driver.FindElement(iconEngContactCheck).Text;
            return value;
        }

        public string ValidateEngContactAtorneyCheckIcon()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconEngContactAttorneyCheck);
            string value = driver.FindElement(iconEngContactAttorneyCheck).Text;
            return value;
        }

        public string ValidateEngContactCheckIconBuyer()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconEngContactCheckBuyer);
            string value = driver.FindElement(iconEngContactCheckBuyer).Text;
            return value;
        }

        public string ValidateEngContactAtorneyCheckIconBuyer()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconEngContactAttorneyCheckBuyer);
            string value = driver.FindElement(iconEngContactAttorneyCheckBuyer).Text;
            return value;
        }
        public string ValidateEngContactSellerCheckbox()
        {
            if (driver.FindElement(chkEngContactCheck).Displayed)
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(chkEngContactCheck));
                if (driver.FindElement(chkEngContactCheck).Selected)
                {
                    return "Engagement Contacts Seller Check checkbox is displayed and checked";
                }
                else
                {
                    return "Engagement Contacts Seller Check checkbox is displayed and not-checked";
                }
            }
            else
            {
                return "Engagement Contacts Seller Check checkbox is not displayed";
            }
        }

        public string ValidateEngContactSellerNoAttorneyCheckbox()
        {
            if (driver.FindElement(chkEngContactNoAttorneyCheck).Displayed)
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(chkEngContactNoAttorneyCheck));
                if (driver.FindElement(chkEngContactNoAttorneyCheck).Selected)
                {
                    return "Engagement Contact Seller No Attorney checkbox is displayed and checked";
                }
                else
                {
                    return "Engagement Contact Seller No Attorney checkbox is displayed and not-checked";
                }
            }
            else
            {
                return "Engagement Contact Seller No Attorney checkbox is not displayed";
            }
        }

        public string ValidateEngContactBuyerCheckbox()
        {
            if (driver.FindElement(chkEngContactCheckBuyer).Displayed)
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(chkEngContactCheckBuyer));
                if (driver.FindElement(chkEngContactCheckBuyer).Selected)
                {
                    return "Engagement Contacts Buyer Check checkbox is displayed and checked";
                }
                else
                {
                    return "Engagement Contacts Buyer Check checkbox is displayed and not-checked";
                }
            }
            else
            {
                return "Engagement Contacts Buyer Check checkbox is not displayed";
            }
        }

        public string ValidateEngContactBuyerNoAttorneyCheckbox()
        {
            if (driver.FindElement(chkEngContactNoAttorneyCheckBuyer).Displayed)
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(chkEngContactNoAttorneyCheckBuyer));
                if (driver.FindElement(chkEngContactNoAttorneyCheckBuyer).Selected)
                {
                    return "Engagement Contact Buyer No Attorney checkbox is displayed and checked";
                }
                else
                {
                    return "Engagement Contact Buyer No Attorney checkbox is displayed and not-checked";
                }
            }
            else
            {
                return "Engagement Contact Buyer No Attorney checkbox is not displayed";
            }
        }


        public string ValidateEngContactSellerCheckboxAfterCheckingNoAttorney()
        {
            Thread.Sleep(4000);
            driver.FindElement(btnEditCheckbox).Click();
            Thread.Sleep(5000);
            driver.FindElement(chkNoAttorney).Click();
            driver.FindElement(btnSave).Click();

            if (driver.FindElement(chkEngContactCheck).Displayed)
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(chkEngContactCheck));
                if (driver.FindElement(chkEngContactCheck).Selected)
                {
                    return "Engagement Contacts Seller Check checkbox is displayed and checked";
                }
                else
                {
                    return "Engagement Contacts Seller Check checkbox is displayed and not-checked";
                }
            }
            else
            {
                return "Engagement Contacts Seller Check checkbox is not displayed";
            }
        }
        public string ValidateEngContactBuyerCheckboxAfterCheckingNoAttorney()
        {
            Thread.Sleep(4000);
            driver.FindElement(btnEditBuyerCheckbox).Click();
            Thread.Sleep(5000);
            driver.FindElement(chkNoAttorneyBuyer).Click();
            driver.FindElement(btnSave).Click();

            if (driver.FindElement(chkEngContactCheckBuyer).Displayed)
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(chkEngContactCheckBuyer));
                if (driver.FindElement(chkEngContactCheckBuyer).Selected)
                {
                    return "Engagement Contacts Buyer Check checkbox is displayed and checked";
                }
                else
                {
                    return "Engagement Contacts Buyer Check checkbox is displayed and not-checked";
                }
            }
            else
            {
                return "Engagement Contacts Buyer Check checkbox is not displayed";
            }
        }
        public string ValidateEngContactSellerCheckboxAfterAttorneyByPass()
        {
            Thread.Sleep(4000);           

            if (driver.FindElement(chkEngContactCheck).Displayed)
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(chkEngContactCheck));
                if (driver.FindElement(chkEngContactCheck).Selected)
                {
                    driver.FindElement(btnEditCheckbox).Click();
                    Thread.Sleep(5000);
                    driver.FindElement(chkNoAttorney).Click();
                    driver.FindElement(btnSave).Click();
                    return "Engagement Contacts Seller Check checkbox is displayed and checked";
                }
                else
                {
                    return "Engagement Contacts Seller Check checkbox is displayed and not-checked";
                }
            }
            else
            {
                return "Engagement Contacts Seller Check checkbox is not displayed";
            }
        }

        public string ValidateEngContactBuyerCheckboxAfterAttorneyChecked()
        {           
                CustomFunctions.MoveToElement(driver, driver.FindElement(chkEngContactCheckBuyer));
               
                    driver.FindElement(btnEditBuyerCheckbox).Click();
                    Thread.Sleep(5000);
                    driver.FindElement(chkNoAttorneyBuyer).Click();
                    driver.FindElement(btnSave).Click();

                driver.FindElement(btnRefreshBuyerContact).Click();
                Thread.Sleep(8000);
                if (driver.FindElement(chkEngContactCheckBuyer).Selected)
                {
                    
                    return "Engagement Contacts Buyer Check checkbox is displayed and checked";
                }
                else
                {
                    return "Engagement Contacts Buyer Check checkbox is displayed and not checked";
                }           
        }

        public string ValidateCapitalizationSection()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, secCapitalization);
            string value = driver.FindElement(secCapitalization).Text;
            return value;
        }

        public bool VerifySubSectionsOfCapitalization()
        {
            driver.FindElement(secCapitalization).Click();
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valSections = driver.FindElements(subSecCapitalization);
            var actualValue = valSections.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Source of Funds", "Use of Funds" };
            Console.WriteLine(expectedValue[1]);
           

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        public bool VerifyFieldsOfSourceFunds()
        {            
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valSections = driver.FindElements(lblSourceOfFundsFields);
            var actualValue = valSections.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Revolving Credit Facility", "Source Revolving Credit Facility Percent", "Term Loan A", "Source Term Loan A Percent", "Term Loan B", "Source Term Loan B Percent", "Term Loan C", "Source Term Loan C Percent", "Delayed Draw Term Loan", "Source Delayed Draw Term Loan Percent", "Senior Subordinated Debt", "Source Senior Subordinated Debt Percent", "Junior Subordinated Debt", "Source Junior Subordinated Debt Percent", "Unitranche Debt", "Source Unitranche Debt Percent", "Preferred Equity", "Source Preferred Equity Percent", "Common Equity", "Source Common Equity Percent", "Seller Notes", "Source Seller Notes Percent", "Company Cash / AR", "Source Company Cash / AR Percent", "Total Sources"};
            Console.WriteLine(expectedValue[1]);


            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public string ValidateCancelFunctionalityOfSourceOfFunds(string number)
        {
            driver.FindElement(btnEditCreditFacility).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtCreditFacility).SendKeys(number);
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(6000);
            string value = driver.FindElement(valCreditFacility).Text;
            return value.Substring(0, 5); 
        }

        public string ValidateEditFunctionalityOfSourceOfFunds(string number)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,-150)");
            Thread.Sleep(5000);
            driver.FindElement(btnEditCreditFacility).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtCreditFacility).Clear();
            driver.FindElement(txtCreditFacility).SendKeys(number);
            Thread.Sleep(5000);
            driver.FindElement(btnSave).Click();            
            js.ExecuteScript("window.scrollTo(0,-150)");
            Thread.Sleep(5000);
            string value = driver.FindElement(valCreditFacility).Text;
            return value.Substring(0,6);
        }


        public string ValidateEditFunctionalityOfBidding(string number)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,-150)");
            Thread.Sleep(5000);
            driver.FindElement(btnEditPitchBookDate).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtPitchBookDate).Clear();
            driver.FindElement(txtPitchBookDate).SendKeys(number);
            Thread.Sleep(5000);
            driver.FindElement(btnSave).Click();
            js.ExecuteScript("window.scrollTo(0,-150)");
            Thread.Sleep(5000);
            string value = driver.FindElement(valPitchBookDate).Text;
            return value;
        }

        public bool VerifyFieldsOfUseOfFunds()
        {
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valSections = driver.FindElements(lblUseOfFundsFields);
            var actualValue = valSections.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Purchase Price", "Use Purchase Price Percent", "Fees & Expenses", "Use Fees & Expenses Percent", "Closing Cash Balance", "Use Closing Cash Balance Percent", "Proceeds to Shareholders", "Use Proceeds To Shareholders Percent", "Debt Paid at Closing", "Use Debt Paid at Closing Percent", "Assumption Of Debt", "Use Assumption of Debt Percent", "Deal Bonuses", "Use Deal Bonuses Percent", "Other", "Use Other Percent", "Total Uses" };
            Console.WriteLine(expectedValue[1]);


            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public string ValidateCancelFunctionalityOfUseOfFunds(string number)
        {
            driver.FindElement(btnEditPurchasePrice).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtPurchasePrice).SendKeys(number);
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(valPurchasePrice).Text;
            return value.Substring(0, 5);
        }

        public string ValidateEditFunctionalityOfUseOfFunds(string number)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,-150)");
            Thread.Sleep(5000);
            driver.FindElement(btnEditPurchasePrice).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtPurchasePrice).Clear();
            driver.FindElement(txtPurchasePrice).SendKeys(number);
            Thread.Sleep(5000);
            driver.FindElement(btnSave).Click();
            js.ExecuteScript("window.scrollTo(0,-120)");
            Thread.Sleep(6000);
            string value = driver.FindElement(valPurchasePrice).Text;
            return value.Substring(0, 6);
        }

        public string ValidateEngTimelineSection()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, secEngTimeline);
            string value = driver.FindElement(secEngTimeline).Text;
            return value;
        }

        public bool VerifySubSectionsOfTimeline()
        {
            driver.FindElement(secEngTimeline).Click();
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valSections = driver.FindElements(subSecTimeline);
            var actualValue = valSections.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Bidding", "Signing","Closing" };
            Console.WriteLine(expectedValue[1]);


            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public bool VerifyFieldsOfBidding()
        {
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valSections = driver.FindElements(lblBiddingFields);
            var actualValue = valSections.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Pitch Book Date", "Expected In Market Date", "Date Engaged", "First Bid Due Date" , "Second Bid Due Date", "Final Bid Due Date" };

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        public string ValidateDateEngagedMessageOnHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgDateEngaged);
            string value = driver.FindElement(msgDateEngaged).Text;
            return value;
        }

        public bool VerifyFieldsOfSigning()
        {
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valSections = driver.FindElements(lblSigningFields);
            var actualValue = valSections.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Signing Date", "Signing Date - Weeks From Date Engaged" };

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }


        public string ValidateEditFunctionalityOfSigning(string number)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,-150)");
            Thread.Sleep(5000);
            driver.FindElement(btnSigningDate).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtSigningDate).Clear();
            driver.FindElement(txtSigningDate).SendKeys(number);
            Thread.Sleep(5000);
            driver.FindElement(btnSave).Click();
            js.ExecuteScript("window.scrollTo(0,-150)");
            Thread.Sleep(5000);
            string value = driver.FindElement(valSigningDate).Text;
            return value;
        }

        public bool VerifyFieldsOfClosing()
        {
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valSections = driver.FindElements(lblClosingFields);
            var actualValue = valSections.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Close Date", "Closed - Weeks From Date Engaged" };

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        public string ValidateMandatoryField1OfBuyerStrategy()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconBuyerStratergy);
            driver.FindElement(iconBuyerStratergy).Click();
            string value = driver.FindElement(msgBuyerStrategy1).Text;
            return value;
        }

        public string ValidateMandatoryField2OfBuyerStrategy()
        {           
            string value = driver.FindElement(msgBuyerStrategy2).Text;
            return value;
        }

        public bool VerifyValuesOfBuyerProcessType()
        {
            Thread.Sleep(6000);
            driver.FindElement(btnEditBuyerProcessType).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnBuyerProcessType).Click();
            IReadOnlyCollection<IWebElement> valSections = driver.FindElements(valBuyerProcessType);
            var actualValue = valSections.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Broad Auction", "Controlled Auction", "Direct Negotiation", "DM&A", "Targeted Negotiations (5 to 10 parties)" };

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }


        public bool VerifyValuesOfBuyerPlatformType()
        {            
            Thread.Sleep(4000);
            driver.FindElement(btnBuyerPlatType).Click();
            IReadOnlyCollection<IWebElement> valSections = driver.FindElements(valBuyerPlatType);
            var actualValue = valSections.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "N/A", "Platform", "Bolt-On", "Strategic Add-On", "Product Extension" };

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public string GetValueOfBuyerProcessType()
        {
            string value = driver.FindElement(valProcessTypePostSave).Text;
            return value;
        }

        public string GetValueOfBuyerPlatformType()
        {
            string value = driver.FindElement(valPlatformTypePostSave).Text;
            return value;
        }

        //Validate Cancel functionality of Buyer details
        public string ValidateCancelFunctionalityOfBuyerStrategySection(string process, string platform)
        {
            driver.FindElement(btnBuyerProcessType).Click();
            driver.FindElement(By.XPath("//button[@name='Buyer_Process_Type__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span[text()='"+process+"']")).Click(); ;
            Thread.Sleep(4000);
            driver.FindElement(btnBuyerPlatType).Click();
            driver.FindElement(By.XPath("//button[@name='Buyer_Platform_Type__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + platform + "']")).Click(); ;
            Thread.Sleep(4000);
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(valProcessTypePostSave).Text;
            return value;
        }

        //Validate Cancel functionality of Buyer details
        public string ValidateEditFunctionalityOfBuyerStrategySection(string process, string platform)
        {
            driver.FindElement(btnEditBuyerProcessType).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnBuyerProcessType).Click();
            driver.FindElement(By.XPath("//button[@name='Buyer_Process_Type__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + process + "']")).Click(); ;
            Thread.Sleep(4000);
            driver.FindElement(btnBuyerPlatType).Click();
            driver.FindElement(By.XPath("//button[@name='Buyer_Platform_Type__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + platform + "']")).Click(); ;
            Thread.Sleep(4000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(valProcessTypePostSave).Text;
            return value;
        }
    }

}


