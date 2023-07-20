using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SF_Automation.Pages.Opportunity
{
    class NBCForm : BaseClass
    {
        By OppName = By.CssSelector("span[id*='id40']");
        By lblClientComp = By.XPath("//records-highlights-details-item[1]/div/p[1]");
        By lblClientOwner = By.XPath("//records-highlights-details-item[2]/div/p[1]");
        By lblSubjectComp = By.XPath("//records-highlights-details-item[3]/div/p[1]");
        By lblSubjectOwnership = By.XPath("//records-highlights-details-item[4]/div/p[1]");
        By lblJobType = By.XPath("//records-highlights-details-item[5]/div/p[1]");
        By lblIG = By.XPath("//records-highlights-details-item[6]/div/p[1]");
        By valclientComp = By.XPath("//records-highlights-details-item[1]/div/p[2]/slot/records-formula-output/slot/lightning-formatted-text");
        By valclientOwnership = By.XPath("//records-highlights-details-item[2]/div/p[2]/slot/records-formula-output/slot/lightning-formatted-text");
        By valsubjectOwnership = By.XPath("//records-highlights-details-item[4]/div/p[2]/slot/records-formula-output/slot/lightning-formatted-text");
        By valsubjectComp = By.XPath("//records-highlights-details-item[3]/div/p[2]/slot/records-formula-output/slot/lightning-formatted-text");
        By valjobType = By.XPath("//records-highlights-details-item[5]/div/p[2]/slot/records-formula-output/slot/lightning-formatted-text");
        By valprimaryIG = By.XPath("//records-highlights-details-item[6]/div/p[2]/slot/records-formula-output/slot/lightning-formatted-text");

        By valOppName = By.XPath("//div[2]/h1/slot/records-formula-output/slot/lightning-formatted-text");
        By lnkEditReviewSub = By.XPath("//button[@title='Edit Form Check (required to submit)']");
        By lnkEditReviewSub2nd = By.XPath("//button[@title='Edit Form Check (required to submit)']");
        By lnkEditFeedback = By.XPath("//div[2]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/slot/slot/flexipage-tab2[1]/slot/flexipage-component2/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field[2]/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");
        By btnSubmit = By.XPath("//flexipage-component2[4]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/span/slot/records-record-layout-checkbox/lightning-input/div/span/input");
        By btnUpdSubmit = By.XPath("(//span[@class='slds-checkbox slds-checkbox_standalone']/input)[5]");
        By btnUpdSubmit2nd = By.XPath("(//span[@class='slds-checkbox slds-checkbox_standalone']/input)[4]");
        By chkNextSchCall = By.XPath("(//span[@class='slds-checkbox slds-checkbox_standalone']/input)[3]");
        By btnSave = By.XPath("//li[2]/runtime_platform_actions-action-renderer/runtime_platform_actions-executor-lwc-headless/slot[1]/slot/lightning-button/button");
        By btnClose = By.XPath("//records-record-edit-error-header/lightning-button-icon/button/lightning-primitive-icon");
        By OppOverviewTab = By.XPath("//lightning-tab-bar/ul/li[@title='Opportunity Overview']");
        By FinancialsTab = By.XPath("//lightning-tab-bar/ul/li[@title='Financials']");
        By FeesTab = By.XPath("//lightning-tab-bar/ul/li[@title='Fees']");
        By PitchTab = By.XPath("//lightning-tab-bar/ul/li[@title='Pitch']");
        By FairnessTab = By.XPath("//lightning-tab-bar/ul/li[@title='Fairness/Admin Checklist']");
        By PublicTab = By.XPath("//lightning-tab-bar/ul/li[@title='Public Sensitivity']");
        By HLInternalTab = By.XPath("//span[text()='HL Internal Team']");
        By HLInternalTabA = By.XPath("//a[text()='HL Internal Team']");
        By MoreTab = By.XPath("//flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar/ul/li[7]/lightning-button-menu/button");
        By ReviewTab = By.XPath("//lightning-tab-bar/ul/li[@title='Review']");
        By btnAddFin = By.XPath("//button[@name= 'Opportunity_Approval__c.Add_Financials']");
        By titleAddFin = By.XPath("//h2[@class='slds-modal__title']");
        By btnSaveAddFin = By.XPath("//footer/button[2]");
        By txtYear = By.XPath("//div[3]/div[1]/div/div/ul/li");
        By txtType = By.XPath("//div[2]/div[1]/div/div/ul/li");
        By lblRelatedComp = By.XPath("//div[1]/div/div/div/div/label/span[1]");
        By lblType =        By.XPath("//div[2]/div[1]/div/div/div/span/span[1]");
        By lblYear = By.XPath("//div[3]/div[1]/div/div/div/span/span[1]");
        By lblAsOfDate = By.XPath("//div[4]/div[1]/div/div/div/label/span");
        By lblRevenue = By.XPath("//div[5]/div[1]/div/div/div/label/span");
        By lblAnnualRec = By.XPath("//div[6]/div[1]/div/div/div/label/span");
        By lblEBIT = By.XPath("//div[7]/div[1]/div/div/div/label/span");
        By lblCurrency = By.XPath("//div[2]/div/div/div/span/span");
        By lblFaceValue = By.XPath("//div[2]/div[2]/div/div/div/label/span");
        By lblNetAsset = By.XPath("//div[3]/div[2]/div/div/div/label/span");
        By lblNoOfComp = By.XPath("//div[4]/div[2]/div/div/div/label/span");
        By lblNoOfLoans = By.XPath("//div[5]/div[2]/div/div/div/label/span");
        By lblNoOfInterests = By.XPath("//div[6]/div[2]/div/div/div/label/span");
        By lblNoOfPolicies = By.XPath("//div[7]/div[2]/div/div/div/label/span");
        By lblEBITDA = By.XPath("//div[8]/div[1]/div/div/div/label/span");
        By lblInterest = By.XPath("//div[9]/div[1]/div/div/div/label/span");
        By lblPreTax = By.XPath("//div[10]/div[1]/div/div/div/label/span");
        By lblBookValue = By.XPath("//div[11]/div[1]/div/div/div/label/span");
        By lblAssetsUnder = By.XPath("//div[12]/div[1]/div/div/div/label/span");
        By btnCancelAddFin = By.XPath("//div[2]/div/div/footer/button[1]/span");

        By lnkEditOppOverviewTab = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");
        By msgTransOverview = By.XPath("//lightning-textarea/label[text()='Transaction Overview']/following::div[3]");
        By msgTotalDebt = By.XPath("//label[text()='Total Debt (MM)']/following::div[2]");
        By msgEstValuation= By.XPath("//label[text()='Valuation Expectations']/following::div[3]");
        By msgCurrentStatus = By.XPath("//label[text()='Current Status']/following::div[7]");
        By msgValuationExp = By.XPath("//label[text()='Valuation Expectations']/following::div[3]");
        By msgCompanyDesc = By.XPath("//label[text()='Company Description']/following::div[3]");
        By msgRealEstate = By.XPath("//label[text()='Real Estate Angle']/following::div[6]");
        By msgOwnershipStr = By.XPath("//label[text()='Ownership and Capital Structure']/following::div[3]");
        By msgAsiaAngle = By.XPath("//label[text()='Asia Angle']/following::div[6]");
        By msgRiskFact = By.XPath("//label[text()='Risk Factors']/following::div[3]");
        By msgInterAngle = By.XPath("//label[text()='International Angle?']/following::div[6]");

        By msgCapMkt = By.XPath("//label[text()='Capital Markets Consulted']/following::div[7]");
        By msgExistingFin = By.XPath("//label[text()='Existing Financial Arrangement Notes']/following::div[3]");
        By msgFinSubject = By.XPath("//label[text()='Financials Subject to Audit']/following::div[6]");
        By msgNoFin = By.XPath("//div[text()='Financials: Add min 2 Historical or current and future Financial records when submitting the NBC form']");

        By msgRetainer = By.XPath("//label[text()='Retainer']/following::div[2]");
        By msgRetainerFee = By.XPath("//label[text()='Retainer Fee Creditable ?']/following::div[3]");
        By msgProgressFee = By.XPath("//label[text()='Progress Fee Creditable ?']/following::div[3]");
        By msgMinFee = By.XPath("//label[text()='Engagement Letter Minimum Fee']/following::div[2]");
        By msgTxnFeeType = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[4]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div[2]");
        By msgEstTxnValue = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[5]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-base-input/lightning-input/div[2]");

        By msgWillThere = By.XPath("//label[text()='Will There Be a Pitch?']/following::div[6]");
        By msgHLComp = By.XPath("//label[text()='Houlihan Lokey Competition']/following::div[2]");
        By msgLockups = By.XPath("//label[text()='Lockups on Future M&A or Financing Work']/following::div[6]");
        By msgExistingRel = By.XPath("//label[text()='Existing Relationships']/following::div[6]");
        By msgExistingOrRepeat = By.XPath("//label[text()='Existing or Repeat Client?']/following::div[6]");
        By msgTAS = By.XPath("//label[text()='TAS/Bridge Assistance Benefit?']/following::div[7]");
        By msgOutside = By.XPath("//label[text()='Outside Council']/following::div[2]");

        By msgFairnessOpinion = By.XPath("//label[text()='Fairness Opinion Provided']/following::div[7]");

        By msgA = By.XPath("//label[text()='A']/following::div[6]");
        By msgB = By.XPath("//label[text()='B']/following::div[6]");
        By msgC = By.XPath("//label[text()='C']/following::div[6]");
        By msgD = By.XPath("//label[text()='D']/following::div[6]");
        By msgGroupHead = By.XPath("//label/span[text()='Group Head Approval']/following::div[2]");

        By txtTxnOverview = By.XPath("//lightning-textarea/label[text()='Transaction Overview']/following::textarea[1]");
        By txtTotalDebt = By.XPath("//label[text()='Total Debt (MM)']/following::input[1]");
        By txtEstValuation = By.XPath("//label[text()='Estimated Transaction Value (MM)']/following::input[1]");
        By btnCurrentStatus = By.XPath("//label[text()='Current Status']/following::button[2]");
        By comboCurrentStatus = By.XPath("//label[text()='Current Status']/following::lightning-base-combobox-item/span[2]/span[text()='Pitched']");
        By txtValuationExp= By.XPath("//label[text()='Valuation Expectations']/following::textarea");
        By txtCompanyDesc = By.XPath("//label[text()='Company Description']/following::textarea[1]");
        By comboRealEstate = By.XPath("//label[text()='Real Estate Angle']/following::button[1]");
        By txtOwnership = By.XPath("//label[text()='Ownership and Capital Structure']/following::textarea[1]");
        By comboAsia = By.XPath("//label[text()='Asia Angle']/following::button[1]");
        By txtRisk = By.XPath("//label[text()='Risk Factors']/following::textarea[1]");
        By comboInternational = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[7]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[1]/button");
        By comboSanctions = By.XPath("(//lightning-base-combobox)[6]");

        By btnCapMkt = By.XPath("//label[text()='Capital Markets Consulted']/following::button[2]");
        By txtExistingFin = By.XPath("//label[text()='Existing Financial Arrangement Notes']/following::textarea[1]");
        By btnFinSubject = By.XPath("(//lightning-base-combobox)[8]");
        By txtFinAuditNotes = By.XPath("//label[text()='Financials Audit Notes']/following::textarea[1]");
        By chkNoFin = By.XPath("//span[text()='Insufficient Financials']/following::input[1]");
        By txtNoFinExp = By.XPath("//label[text()='Insufficient Financials Explanation']/following::textarea[1]");

        By txtRetainer = By.XPath("//*[@name='Retainer1__c']");
        By txtRetainerFeeCred = By.XPath("//input[@name='Retainer_Creditable__c']");
        By txtProgressFee = By.XPath("//input[@name='Progress_Fee__c']");
        By txtProgressFeeCred = By.XPath("//input[@name='Is_Progress_Fee_Creditable__c']");
        By txtMinFee = By.XPath("//input[@name='Estimated_Minimum_Fee__c']");
        By txtEstTxnVal = By.XPath("//input[@name='Transaction_Value_for_Fee_Calc__c']");
        By valEstTxnVal = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[5]/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/lightning-formatted-text");
        By btnTxnFeeType = By.XPath("(//lightning-base-combobox)[9]");
        By valTxnFeeType = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[4]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span");
        By txtReferralFee = By.XPath("//label[text()='Referral Fee Owed (MM)']/following::div[1]/input");
        By lblFlatFee = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[5]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-base-input/lightning-input/div/label");
        By lblOtherFee = By.XPath("//flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[5]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-text-area/lightning-textarea/label");
        By txtOtherFee = By.XPath("//label[text()='Other Fee Structure']/following::textarea[1]");
        By msgOtherFee = By.XPath("//label[text()='Other Fee Structure']/following::div[2]");
        By lblIncentiveFeeStr = By.XPath("//span[@title='Incentive Fee structure']");
        By txtProgressFeeMM = By.XPath("//input[@name='Progress_Fee__c']");
        By txtMinFeeMM = By.XPath("//input[@name='Estimated_Minimum_Fee__c']");
        By txtEstTxnValueMM = By.XPath("//input[@name='Transaction_Value_for_Fee_Calc__c']");
        By txtReferralFeeOwnedMM = By.XPath("//input[@name='Referral_Fee__c']");
        By valEstTotalFeeMM = By.XPath("//flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/records-formula-output/slot/lightning-formatted-text");
        By lnkEditProgressFeeCred = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[2]/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");
        By valFlatFee = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[5]/slot/record_flexipage-record-field/div/div/div[2]/span");
        By lnkEditCurrency = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[3]/slot/record_flexipage-record-field/div/div/div[2]/button");
        By txtFlatFee = By.XPath("//label[text()='Flat Fee (MM)']/following::div[1]/input");
        By valMinFee = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[3]/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/lightning-formatted-text");
        By btnWillThere = By.XPath("//label[text()='Will There Be a Pitch?']/following::button[1]");
        By txtHLComp = By.XPath("//label[text()='Houlihan Lokey Competition']/following::textarea[1]");
        By btnLockups = By.XPath("//label[text()='Lockups on Future M&A or Financing Work']/following::button[1]");
        By btnExistingRel = By.XPath("//label[text()='Existing Relationships']/following::button[1]");
        By btnExistingClient = By.XPath("//label[text()='Existing or Repeat Client?']/following::button[1]");
        By btnTAS = By.XPath("//label[text()='TAS/Bridge Assistance Benefit?']/following::button[2]");
        By txtOutsideCouncil = By.XPath("//label[text()='Outside Council']/following::textarea[1]");
        By msgEstTransValue = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[5]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-base-input/lightning-input/div[2]");

        By txtBaseFee = By.XPath("//label[text()='Base Fee (MM)']/following::input[1]");
        By valBaseFee = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/lightning-formatted-text");
        By lnkUpdBaseFee = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");
        By txt1stRatchetPer = By.XPath("//label[text()='First Ratchet Percent']/following::input[1]");
        By txt2ndRatchetPer = By.XPath("//label[text()='Second Ratchet Percent']/following::input[1]");
        By txt3rdRatchetPer = By.XPath("//label[text()='Third Ratchet Percent']/following::input[1]");
        By txt4thRatchetPer = By.XPath("//label[text()='Fourth Ratchet Percent']/following::input[1]");
        By txtFinalRatchetPer = By.XPath("//label[text()='Final Ratchet Percent']/following::input[1]");
        By lnkUpd1stRatchet = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[2]/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");
        By lnkUpd2ndRatchet = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[5]/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");
        By lnkUpd3rdRatchet = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");
        By lnkUpd4thRatchet = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[4]/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");
        By lnkUpdFinalRatchet = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[7]/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");

        By msg1stRatchetFromAmount = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[3]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-base-input/lightning-input/div[2]");
        By msg1stRatchetToAmount = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[4]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-base-input/lightning-input/div[2]");
        By msg2ndRatchetFromAmount = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[6]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-base-input/lightning-input/div[2]");
        By msg2ndRatchetToAmount = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[7]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-base-input/lightning-input/div[2]");
        By msg3rdRatchetFromAmount = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[2]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-base-input/lightning-input/div[2]");
        By msg3rdRatchetToAmount = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[3]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-base-input/lightning-input/div[2]");
        By msg4thRatchetFromAmount = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[5]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-base-input/lightning-input/div[2]");
        By msg4thRatchetToAmount = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[6]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-base-input/lightning-input/div[2]");
        By txt1stRatchetFromAmount = By.XPath("//input[@name='First_Ratchet_From_Amount__c']");
        By txt1stRatchetToAmount = By.XPath("//input[@name='First_Ratchet_To_Amount__c']");
        By txt2ndRatchetFromAmount = By.XPath("//input[@name='Second_Ratchet_From_Amount__c']");
        By txt2ndRatchetToAmount = By.XPath("//input[@name='Second_Ratchet_To_Amount__c']");
        By txt3rdRatchetFromAmount = By.XPath("//input[@name='Third_Ratchet_From_Amount__c']");
        By txt3rdRatchetToAmount = By.XPath("//input[@name='Third_Ratchet_To_Amount__c']");
        By txt4thRatchetFromAmount = By.XPath("//input[@name='Fourth_Ratchet_From_Amount__c']");
        By txt4thRatchetToAmount = By.XPath("//input[@name='Fourth_Ratchet_To_Amount__c']");
        By txtFinalRatchetAmount = By.XPath("//input[@name='Final_Ratchet_Amount__c']");

        By msg1stRatchetGreaterToAmount = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[4]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-base-input/lightning-input/div[2]");
        By msg2ndRatchetGreaterToAmount = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[7]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-base-input/lightning-input/div[2]");
        By msg3rdRatchetGreaterToAmount = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[3]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-base-input/lightning-input/div[2]");
        By msg4thRatchetGreaterToAmount = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[6]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-base-input/lightning-input/div[2]");


        By btnFairnessOpinion = By.XPath("//label[text()='Fairness Opinion Provided']/following::button[2]");
        By tabAdmin = By.XPath("//*[@id='flexipage_tab8__item']");
        By tabFairness = By.XPath("//*[@id='flexipage_tab7__item']");
        By btnRestricted = By.XPath("//label[text()='Restricted List']/following::button[1]");
        By btnYes1 = By.XPath("(//lightning-base-combobox)[19]");
        By btnYes2 = By.XPath("(//lightning-base-combobox)[20]");
        By btnYes3 = By.XPath("(//lightning-base-combobox)[21]");
        By btnYes4 = By.XPath("(//lightning-base-combobox)[22]");
        By btnYes5 = By.XPath("(//lightning-base-combobox)[23]");

        By btnA = By.XPath("//label[text()='A']/following::button[1]");
        By btnB = By.XPath("//label[text()='B']/following::button[1]");
        By btnC = By.XPath("//label[text()='C']/following::button[1]");
        By btnD = By.XPath("(//lightning-base-combobox)[27]");
        By chkGroupHead = By.XPath("//span[text()='Group Head Approval']/following::input[1]");

        By lblRelOpp = By.XPath("//div[1]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/slot/slot/flexipage-tab2[1]/slot/flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblTxnOver= By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordRelated_Opportunity_cField1']/slot[1]/following::span[1]");
        By lblCurrentStatus = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordTransaction_Overview__cField']/slot[1]/following::span[1]");
        By lblCompDesc = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordStatus_cField1']/slot[1]/following::span[1]");
        By lblOwnerCapStr = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordCompany_Description__cField']/slot[1]/following::span[1]");
        By lblRiskFact = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordOwnership_and_Capital_Structure__cField']/slot[1]/following::span[1]");
        By lblIntAngle = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordRisk_Factors_cField1']/slot[1]/following::span[1]");
        By lblTotalDebtMM = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblEstimatedValuation = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[@data-field-id='RecordTotal_Debt_MM_cField2']/slot[1]/following::span[1]");
        By lblValExp = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[@data-field-id='RecordEstimated_Valuation_cField2']/slot[1]/following::span[1]");
        By lblRealEstate = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[@data-field-id='RecordValuation_Expectations_cField1']/slot[1]/following::span[1]");
        By lblAsiaAngle = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[@data-field-id='RecordReal_Estate_Angle_cField1']/slot[1]/following::span[1]");
        By lblSanctions = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[5]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");

        By msgCapMarket = By.XPath("//span[@title='Has the Capital Markets Group been Consulted regarding financing or capital structure?']");
        By lblCapMkt = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Opportunity_Approval__c.Capital_Markets_Consulted__c']/div/div[1]/span[1]");
        By lblExistFin = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordCapital_Markets_Consulted_cField2']/slot[1]/following::span[1]");
        By msgAboveFin = By.XPath("//span[text()='Have the above financials been subject to an audit?']");
        By lblFinSub = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Opportunity_Approval__c.Financials_Subject_to_Audit__c']/div[1]/div[1]/span");
        By msgFinAvail = By.XPath("//span[@title='Financials Unavailable']");
        By lblNoFin = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Opportunity_Approval__c.No_Financials__c']/div[1]/div[1]/span");
        By lblNoFinExp = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Opportunity_Approval__c.No_Financials_Explanation__c']/div[1]/div[1]/span");

        By lblRetainer = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblProgressFee = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordRetainer1_cField1']/slot[1]/following::span[1]");
        By lblMinFee = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordProgress_Fee_cField1']/slot[1]/following::span[1]");
        By lblTxnFee = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordEstimated_Minimum_Fee_cField1']/slot[1]/following::span[1]");
        By lblEstTxn = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordTransaction_Fee_Type_cField1']/slot[1]/following::span[1]");
        By lblEstTxnValueReport = By.XPath("//flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[6]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblProgFeeCredit = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[@data-field-id='RecordRetainer_Creditable_cField1']/slot[1]/following::span[1]");
        By lblRetainerFeeCred = By.XPath("//flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblCurrencyFee = By.XPath("//flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[3]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");

        By secPrePitch = By.XPath("//span[@title='Pre-Pitch']");
        By lblWillThere = By.XPath("//flexipage-tab2[4]/slot/flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblHLComp = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordWill_there_be_a_pitch_cField1']/slot[1]/following::span[1]");
        By lblExistingRel = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordHoulihan_Lokey_Competition_cField1']/slot[1]/following::span[1]");
        By lblExisting = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordExisting_Relationships_cField1']/slot[1]/following::span[1]");
        By lblWhoAre = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordExisting_or_Repeat_Client_cField1']/slot[1]/following::span[1]");
        By lblLockups = By.XPath("//flexipage-tab2[4]/slot/flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[2]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secWould = By.XPath("//span[@title='Would the opportunity benefit from TAS Assistance?']");
        By lblTAS = By.XPath("//flexipage-tab2[4]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field/following::span[1]");
        By secIfKnown = By.XPath("//slot/flexipage-tab2[4]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/h3/button/span");
        By lblOutside = By.XPath("//flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/slot/slot/flexipage-tab2[4]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secReferral = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordOutsideCouncil_cField1']/slot[1]/following::span[2]");
        By lblRefType = By.XPath("//flexipage-tab2[4]/slot/flexipage-component2[4]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblRefSource = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordReferral_Type_cField1']/slot[1]/following::span[1]");

        By secIsPotential = By.XPath("//span[@title='Is there a potential Fairness Opinion component to this assignment?']");
        By lblFairnessOpinion = By.XPath("//flexipage-field[@data-field-id='RecordFairness_Opinion_Provided_cField4']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");

        By secRestricted = By.XPath("//slot/flexipage-tab2[2]/slot/flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/h3/button/span");
        By lblRestictedList = By.XPath("//flexipage-tab2[2]/slot/flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secCCInfo = By.XPath("//span[@title='Conflicts Check Information - (the answers to each of these questions must be verified with each member of the deal team)']");
        By lblCCStatus = By.XPath("//slot/flexipage-field[@data-field-id='RecordConflict_Check_Status_cField1']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secAreThereAnyPitch = By.XPath("//flexipage-component2[@data-component-id='flexipage_richText6']/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lbl1stYesNo = By.XPath("//slot/flexipage-field[@data-field-id='RecordConflicts_2a_Not_Listed_cField3']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secHaveAny1 = By.XPath("//flexipage-component2[@data-component-id='flexipage_richText15']/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lbl2ndYesNo = By.XPath("//slot/flexipage-field[@data-field-id='RecordConflicts_3a_Related_to_Transaction_cField1']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secHaveAny2 = By.XPath("//flexipage-component2[@data-component-id='flexipage_richText16']/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lbl3rdYesNo = By.XPath("//slot/flexipage-field[@data-field-id='RecordConflicts_35a_Related_to_Client_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secHaveAny3 = By.XPath("//flexipage-component2[9]/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lbl4thYesNo = By.XPath("//slot/flexipage-field[@data-field-id='RecordConflicts_4a_Conflict_of_Interest_cField3']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secHaveAny4 = By.XPath("//flexipage-component2[11]/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lbl5thYesNo = By.XPath("//slot/flexipage-field[@data-field-id='RecordConflicts_5a_Other_Conflicts_cField1']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");

        By secA = By.XPath("//flexipage-component2[@data-component-id='flexipage_richText']/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lblA = By.XPath("//flexipage-field[@data-field-id='RecordA_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secB = By.XPath("//flexipage-component2[@data-component-id='flexipage_richText2']/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lblB = By.XPath("//flexipage-field[@data-field-id='RecordB_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secC = By.XPath("//flexipage-component2[@data-component-id='flexipage_richText3']/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lblC = By.XPath("//flexipage-field[@data-field-id='RecordC_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secD = By.XPath("//flexipage-component2[@data-component-id='flexipage_richText4']/slot/flexipage-aura-wrapper/div/div/div[1]/p/b");
        By lblD = By.XPath("//flexipage-field[@data-field-id='RecordD_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secPlsConfirm = By.XPath("//flexipage-component2[@data-component-id='flexipage_fieldSection15']/slot/flexipage-field-section2/div/div/div/h3/button/span");
        By lblGroupHead = By.XPath("//flexipage-field[@data-field-id='RecordHead_Approval_cField1']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");

        By lblStaff =      By.XPath("/html/body/span[2]/form/div[1]/div/div[2]/span[2]/table/tbody/tr[3]/td[1]/div[1]/label");
        By btnSaveITTeam  =By.XPath("//div[1]/div/div[1]/table/tbody/tr/td[2]/span/input[1]");       
        By btnReturnToOpp =By.XPath("//div[1]/div/div[1]/table/tbody/tr/td[2]/span/input[2]");
        By btnRoleDef =    By.XPath("//div[1]/div/div[1]/table/tbody/tr/td[2]/span/input[3]");

        By lblNextSchCall = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Opportunity_Approval__c.Next_Scheduled_Call__c']/div[1]/div[1]/span[1]");
        By lblReqFeedback = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Opportunity_Approval__c.Req_feedback_prior_to_normal_sched_call__c']/div[1]/div[1]/span");
        By lblRequiresFeedback = By.XPath("//slot/div/slot/flexipage-column2/div/slot/flexipage-field[@data-field-id='RecordReq_feedback_prior_to_normal_sched_call_cField1']/following::span[1]");
        By lblReviewSub = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Opportunity_Approval__c.Submit_For_Review__c']/div[1]/div[1]/span");

        By lnkAttachments = By.XPath("//a[@data-label='Attachments']");
        By secFiles = By.XPath("//span[@title='Files']");
        By btnUploadFiles = By.XPath("//*[@class='slds-file-selector__body']/span[1]");
        By secOwnershipDetails = By.XPath("//a[@data-aura-class='uiOutputURL']");
        By secApprovalHistory = By.XPath("//span[@title='Approval History']");
        By lblClientOwnership = By.XPath("//*[@class='test-id__section-content slds-section__content section__content slds-p-top--none']/div/div/div[1]/div/div[1]/span");
        By valClientOwnership = By.XPath("//*[@class='test-id__section-content slds-section__content section__content slds-p-top--none']/div/div/div[1]/div/div[2]/span/span");
        By lblSubOwnership = By.XPath("//*[@class='test-id__section-content slds-section__content section__content slds-p-top--none']/div/div/div[2]/div/div[1]/span");
        By valSubOwnership = By.XPath("//*[@class='test-id__section-content slds-section__content section__content slds-p-top--none']/div/div/div[2]/div/div[2]/span/span");

        By lnkEditFairnessOpinion = By.XPath("//flexipage-tab2[5]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/slot/slot/flexipage-tab2[1]/slot/flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");
        By lblFairnessOpinionPublicly = By.XPath("//flexipage-field[@data-field-id='RecordFairness_Opinion_Publicly_Disclosed_cField4']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblShareholderVote = By.XPath("//flexipage-field[@data-field-id='RecordShareholder_Vote_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblProposedFee = By.XPath("//flexipage-field[@data-field-id='RecordProposed_Fee_Range_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblFairnessFee = By.XPath("//flexipage-field[@data-field-id='RecordFairness_Fee_Inclusion_cField4']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblFairnessOpinionStaff = By.XPath("//flexipage-field[@data-field-id='RecordFairness_Opinion_Staffing_Notes_cField4']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblUnaffiliatedStockHolders = By.XPath("//flexipage-component2[@data-component-id='flexipage_fieldSection8']/slot/flexipage-field-section2/div/div/div/h3/button/span");
        By lblCheck1 = By.XPath("//flexipage-field[@data-field-id='RecordFEIS_Opine_Option_1_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblCompInTrans = By.XPath("//flexipage-component2[@data-component-id='flexipage_fieldSection13']/slot/flexipage-field-section2/div/div/div/h3/button/span");
        By lblCheck2 = By.XPath("//flexipage-field[@data-field-id='RecordFEIS_Opine_Option_2_cField1']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblCompInTrans2 = By.XPath("//flexipage-component2[@data-component-id='flexipage_fieldSection20']/slot/flexipage-field-section2/div/div/div/h3/button/span");
        By lblCheck3= By.XPath("//flexipage-field[@data-field-id='RecordFEIS_Opine_Option_3_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblExchangeRatio = By.XPath("//flexipage-component2[@data-component-id='flexipage_fieldSection21']/slot/flexipage-field-section2/div/div/div/h3/button/span");
        By lblCheck4 = By.XPath("//flexipage-field[@data-field-id='RecordFEIS_Opine_Option_4_cField1']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblExchangeRatioUnaffil = By.XPath("//flexipage-component2[@data-component-id='flexipage_fieldSection22']/slot/flexipage-field-section2/div/div/div/h3/button/span");
        By lblCheck5 = By.XPath("//flexipage-field[@data-field-id='RecordFEIS_Opine_Option_5_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblNotYet = By.XPath("//flexipage-component2[@data-component-id='flexipage_fieldSection23']/slot/flexipage-field-section2/div/div/div/h3/button/span");
        By lblCheck6 = By.XPath("//flexipage-field[@data-field-id='RecordForm_of_Opinion_Not_Yet_Known_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblOthersSpecify = By.XPath("//flexipage-component2[@data-component-id='flexipage_fieldSection32']/slot/flexipage-field-section2/div/div/div/h3/button/span");
        By lblCheck7 = By.XPath("//flexipage-field[@data-field-id='RecordFEIS_Opine_Option_6_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblFormOfOpinion = By.XPath("//flexipage-field[@data-field-id='RecordForm_of_Opinion_Notes_cField1']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblRelatedParty = By.XPath("//flexipage-component2[@data-component-id='flexipage_fieldSection33']/slot/flexipage-field-section2/div/div/div/h3/button/span");
        By lblRelatedPartyTxn = By.XPath("//flexipage-field[@data-field-id='RecordRelated_Party_Transaction_cField2']/slot/record_flexipage-record-field/div/div/div[1]/span[1]");

        By btnSubmitForReview = By.XPath("//lightning-button/button[text()='Submit for Review']");
        By tabReview = By.XPath("//lightning-tab-bar/ul/li[@title='Review']");
        By lnkEditGrade = By.XPath("//span[text()='Grade']/following::div[1]/button/span[1]");
        By btnGrade = By.XPath("//label[text()='Grade']/following::lightning-base-combobox[1]/div[1]/div/button");
        By valGrade = By.XPath("//flexipage-tab2[8]/slot/flexipage-component2/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/lightning-formatted-text");

        By valOppNum = By.XPath("/html/body/p[8]/span");
        By btnSendEmail = By.XPath("//div[1]/table/tbody/tr/td[2]/input[1]");

        By errorList = By.CssSelector("#j_id0\\:NBCForm\\:j_id2\\:j_id3\\:j_id4\\:0\\:j_id5\\:j_id6\\:j_id18 > ul");
        By btnCancel = By.CssSelector("input[value='Cancel Submission']");
        By checkToggleTabs = By.Id("toggleTabs");
        By tabList = By.Id("tabsList");
        By comboFinancialOpinion = By.CssSelector("select[id*='MajoritySale']");
        By checkConfirm = By.CssSelector("input[id*='HeadApproval']");
        By txtTranOverview = By.CssSelector("textarea[name*='id78']");
        By txtHLCompPG = By.CssSelector("textarea[name*='id75']");
        //By txtCurrentStatus = By.CssSelector("textarea[name*='id80']");
        //By txtCompDesc = By.CssSelector("textarea[name*='id82']");
        By comboCrossBorder = By.CssSelector("select[name*='InternationalAngle']");
        By comboAsiaAngle = By.CssSelector("select[name*='AsiaAngle']");        
        By txtOwnershipStr = By.CssSelector("textarea[name*='id104']");        
        By comboAudit = By.CssSelector("select[name*='FinAudit01']");
        //By txtEstVal = By.CssSelector("input[name*='estValu']");        
        By txtRiskFactors = By.CssSelector("textarea[name*='id157']");
        By txtEstFee = By.CssSelector("input[name*='estMinFee']");
        By txtFeeStr = By.CssSelector("textarea[name*= 'id247']");
        By comboLockUps = By.CssSelector("select[name*= 'id251']");
        By comboReferralFee = By.CssSelector("select[name*= 'id256']");
        By comboPitch = By.CssSelector("select[name*= 'Pitch00']");
        By comboClient = By.CssSelector("select[id='j_id0:NBCForm:j_id31:j_id260:Exist']");
        //By txtHLComp = By.CssSelector("textarea[name*= 'id273']");
        By comboExistingRel = By.CssSelector("select[name*= 'ExistingRel']");
        By comboTASAssist = By.CssSelector("select[name*= 'TAS00']");
        //By txtOutsideCouncil = By.CssSelector("textarea[name*= 'OutsideCouncil']");
        //By comboCapMkt = By.CssSelector("select[name*= 'id311']");
        By txtSum = By.CssSelector("textarea[name*= 'id314']");
        By comboFairness = By.CssSelector("select[name*= 'Fairness']");
        By comboResList = By.CssSelector("select[name*= 'RestrictedList']");
        By comboCCStatus = By.CssSelector("select[name*= 'Conflicts2a']");
        By comboCCStatus1 = By.CssSelector("select[name*= 'Conflicts3a']");
        By comboCCStatus2 = By.CssSelector("select[name*= 'Conflicts35a']");
        By comboCCStatus3 = By.CssSelector("select[name*= 'Conflicts4a']");
        By comboCCStatus4 = By.CssSelector("select[name*= 'Conflicts5a']");
        By titleEmailPage = By.XPath("//div[@class='pbHeader']/table/tbody/tr/td/h2[@class='mainTitle']");
        By valEmailOppName = By.CssSelector("body[id*='Body_rta_body'] > span:nth-child(10) > span");
        By btnCancelEmail = By.XPath("//div[@class='pbHeader']/table/tbody/tr/td/input[@value='Cancel']");
        By btnReturntoOpp = By.CssSelector("input[value*='Return to Opportunity']");
        By btnReturntoOppCFUser = By.CssSelector("span[id*=':j_id34'] > a");
        By statusCC = By.CssSelector("div[id*='tabs-6']>div>div[class='pbSubsection']>table>tbody>tr:nth-child(4)>td>span>table>tbody>tr>td:nth-child(2)>span");
        By valFinOption = By.CssSelector("select[id*='MajoritySale']>option[selected='selected']");
        By txtComment = By.CssSelector("span[id*='MajoritySale01']");
        By tblSuggestedFee = By.CssSelector("span[id*='j_id163'] >table");
        By btnEUOverride = By.CssSelector("span[id*='1:j_id33'] > input[id*='euOverride']");
        By txtEUComment = By.CssSelector("tr[id *= 'MajoritySale02'] > td");
        By tblEUFee = By.CssSelector("span[id*='j_id218'] > table");
        By lblReview = By.CssSelector("div[id*='tabs-7']>div>div>h3");
        By comboGrade = By.CssSelector("select[id*='reviewGrade']");
        //By valGrade = By.CssSelector("select[id*='reviewGrade']>option[selected='selected']");
        By txtNotes = By.CssSelector("textarea[id*='reviewNotes']");
        By txtDateSubmitted = By.CssSelector("input[id*='dateSubmit']");
        By txtReason = By.CssSelector("textarea[id*='reasonWonLost']");
        By txtFeeDiff = By.CssSelector("textarea[id*='feeDiff']");
        By btnPDFView = By.XPath("//a[contains(text(),'PDF View')]");
        By btnAttachFile = By.CssSelector("button[type='button']");
        By btnAddFinancials = By.CssSelector("input[id*='newFinancials']");

        By lnkEditRetainer = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");
        By valRetainer = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/lightning-formatted-text");
        By valProgressFee = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[2]/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/lightning-formatted-text");
        By valEstTotalFee = By.XPath("//flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/records-formula-output/slot/lightning-formatted-text");
        By valEstValuation = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[5]/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/lightning-formatted-text");
        By lnkEditEstValuation = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[@data-field-id='RecordTotal_Debt_MM_cField2']/slot[1]/following::button[2]/span[1]");
        By msgCCStatus = By.XPath("//flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/records-formula-output/slot/lightning-formatted-text");
        By msgCC1 = By.XPath("//flexipage-tab2[2]/slot/flexipage-component2[4]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div[2]");
        By msgCC2 = By.XPath("//flexipage-tab2[2]/slot/flexipage-component2[6]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div[2]");
        By msgCC3 = By.XPath("//flexipage-tab2[2]/slot/flexipage-component2[8]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div[2]");
        By msgCC4 = By.XPath("//flexipage-tab2[2]/slot/flexipage-component2[10]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div[2]");
        By msgCC5 = By.XPath("//flexipage-tab2[2]/slot/flexipage-component2[12]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div[2]");
        By valDateSubmitted = By.XPath("//flexipage-field[3]/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/lightning-formatted-text");
        By lblBaseFee = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lbl1stRatchetPercent= By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[2]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lbl1stRatchetFromAmt = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[3]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lbl1stRatchetToAmt = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[4]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lbl2ndRatchetPercent = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[5]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lbl2ndRatchetFromAmt = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[6]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lbl2ndRatchetToAmt = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[7]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblFeeComments = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[8]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lbl3rdRatchetPercent = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lbl3rdRatchetFromAmt = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[2]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lbl3rdRatchetToAmt = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[3]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lbl4thRatchetPercent = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[4]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lbl4thRatchetFromAmt = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[5]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lbl4thRatchetToAmt = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[6]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblFinalRatchet = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[7]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblFinalRatchetAmt = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[8]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");

        By txtEstimatedFee = By.XPath("//label[text()='Estimated Fee (MM)']/following::input[1]");
        //Validate Opp Name
        public string ValidateOppName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valOppName, 50);
            string valOpp = driver.FindElement(valOppName).Text;
            return valOpp;
        }

        //Validate Client Company Label
        public string ValidateClientCompanyHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblClientComp);
            string valclient = driver.FindElement(lblClientComp).Text;
            return valclient;
        }

        //Validate Client Ownership Label
        public string ValidateClientOwnershipHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblClientOwner);
            string valclient = driver.FindElement(lblClientOwner).Text;
            return valclient;
        }

        //Validate Subject Company Label
        public string ValidateSubjectCompanyHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblSubjectComp);
            string valclient = driver.FindElement(lblSubjectComp).Text;
            return valclient;
        }

        //Validate Subject Ownership Label
        public string ValidateSubjectOwnershipHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblSubjectOwnership);
            string valclient = driver.FindElement(lblSubjectOwnership).Text;
            return valclient;
        }

        //Validate Job Type Label
        public string ValidateJobTypeHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblJobType);
            string valclient = driver.FindElement(lblJobType).Text;
            return valclient;
        }

        //Validate IG Label
        public string ValidateIGHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblIG);
            string valclient = driver.FindElement(lblIG).Text;
            return valclient;
        }

        //Validate Client Name
        public string ValidateClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valclientComp);
            string valclient = driver.FindElement(valclientComp).Text;
            return valclient;
        }

        //Validate Client Ownership
        public string ValidateClientOwnership()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valclientOwnership);
            string valclient = driver.FindElement(valclientOwnership).Text;
            return valclient;
        }
        //Validate Subject Name
        public string ValidateSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver,valsubjectComp);
            string valSubject = driver.FindElement(valsubjectComp).Text;
            return valSubject;
        }

        //Validate Subject Ownership
        public string ValidateSubjectOwnership()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valsubjectOwnership);
            string valSubject = driver.FindElement(valsubjectOwnership).Text;
            return valSubject;
        }
        //Validate JobType
        public string ValidateJobType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valjobType);
            string valJobType = driver.FindElement(valjobType).Text;
            return valJobType;
        }

        //Validate Primary Industry Group
        public string ValidateIG()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valprimaryIG);
            string valIG = driver.FindElement(valprimaryIG).Text;
            return valIG;
        }

        //Click Opportunity Overview tab
        public string ClickOpportunityOverview()
        {
            
            WebDriverWaits.WaitUntilEleVisible(driver, OppOverviewTab,80);
            driver.FindElement(OppOverviewTab).Click();
            string valTab = driver.FindElement(OppOverviewTab).Text;
            return valTab;
        }

        //Click Add Financials button
        public string ClickAddFinancialsButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddFin, 80);
            driver.FindElement(btnAddFin).Click();
            Thread.Sleep(4000);
            string valTab = driver.FindElement(titleAddFin).Text;
            return valTab;
        }

        //Validate Year validation
        public string GetYearValidation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveAddFin, 80);
            driver.FindElement(btnSaveAddFin).Click();
            Thread.Sleep(4000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,250)");
            string valYear = driver.FindElement(txtYear).Text;
            return valYear;
        }

        //Validate Type validation
        public string GetTypeValidation()
        {           
            string valType= driver.FindElement(txtType).Text;
            return valType;
        }

        //Get field Related Company 
        public string GetRelatedCompanyField()
        {
            string valRel = driver.FindElement(lblRelatedComp).Text;
            return valRel;
        }

        //Get field Type 
        public string GetTypeField()
        {
            string valType = driver.FindElement(lblType).Text;
            return valType;
        }
        //Get field Year 
        public string GetYearField()
        {
            string valYear = driver.FindElement(lblYear).Text;
            return valYear;
        }

        //Get field As of Date
        public string GetAsOfDateField()
        {
            string valDate = driver.FindElement(lblAsOfDate).Text;
            return valDate;
        }

        //Get Revenue (MM)
        public string GetRevenueMMField()
        {
            string valRev = driver.FindElement(lblRevenue).Text;
            return valRev;
        }

        //Get Annual Recurring Revenue (MM)
        public string GetAnnualRecurringRevenue()
        {
            string valRec = driver.FindElement(lblAnnualRec).Text;
            return valRec;
        }

        //Get EBIT (MM) 
        public string GetEBIT()
        {
            string valRec = driver.FindElement(lblEBIT).Text;
            return valRec;
        }

        //Get Currency
        public string GetCurrency()
        {
            string valCur = driver.FindElement(lblCurrency).Text;
            return valCur;
        }

        //Get Face Value label
        public string GetFaceValue()
        {
            string valValue = driver.FindElement(lblFaceValue).Text;
            return valValue;
        }

        //Get Net Asset Value (MM) label
        public string GetNetAssetValue()
        {
            string valValue = driver.FindElement(lblNetAsset).Text;
            return valValue;
        }

        //Get Number of Companies label
        public string GetNumberOfCompanies()
        {
            string valValue = driver.FindElement(lblNoOfComp).Text;
            return valValue;
        }

        //Get Number of Loans label
        public string GetNumberOfLoans()
        {
            string valValue = driver.FindElement(lblNoOfLoans).Text;
            return valValue;
        }

        //Get Number of Interests label
        public string GetNumberOfInterests()
        {
            string valValue = driver.FindElement(lblNoOfInterests).Text;
            return valValue;
        }

        //Get Number of Policies label
        public string GetNumberOfPolicies()
        {
            string valValue = driver.FindElement(lblNoOfPolicies).Text;
            return valValue;
        }

        //Get EBITDA (MM) label
        public string GetEBITDA()
        {
            string valValue = driver.FindElement(lblEBITDA).Text;
            return valValue;
        }

        //Get Interest and Fee Income (MM) label
        public string GetInterestAndFeeIncome()

        {
            string valValue = driver.FindElement(lblInterest).Text;
            return valValue;
        }

        //Get Pre-Tax Income (MM) label
        public string GetPreTaxIncome()
        {
            string valValue = driver.FindElement(lblPreTax).Text;
            return valValue;
        }

        //Get Book Value (MM) label
        public string GetBookValue()
        {
            string valValue = driver.FindElement(lblBookValue).Text;
            return valValue;
        }

        //Get Assets Under Management (MM) label
        public string GetAssetsUnderManagement()
        {
            string valValue = driver.FindElement(lblAssetsUnder).Text;
            driver.FindElement(btnCancelAddFin).Click();
            Thread.Sleep(4000);
            return valValue;
        }

        //Click Financials tab
        public string ClickFinancialsTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, FinancialsTab, 80);
            driver.FindElement(FinancialsTab).Click();
            string valTab = driver.FindElement(FinancialsTab).Text;
           
            return valTab;
        }

        //Click Fees tab
        public string ClickFeesTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, FeesTab, 80);
            driver.FindElement(FeesTab).Click();
            string valTab = driver.FindElement(FeesTab).Text;
            return valTab;
        }

        //Validate Transaction Type Fee
        public bool VerifyTxnTypeFee()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,400)");
            Thread.Sleep(4000);
            driver.FindElement(btnTxnFeeType).Click();           
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(valTxnFeeType);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            //string[] expectedValue = {"CF", "Conflicts Check", "FAS","FR", "HL Internal Opportunity", "OPP DEL","SC"};
            string[] expectedValue = { "--None--", "Flat Fee", "Incentive Structure", "Other Fee Structure" };
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

        //Validate Flat Fee field
        public string ValidateFlatFeeField()
        {
            //driver.FindElement(btnTxnFeeType).SendKeys("Flat Fee");
            driver.FindElement(By.XPath("//label[text()='Transaction Fee Type']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            Thread.Sleep(3000);
            string label = driver.FindElement(lblFlatFee).Text;
            return label;
        }

        //Validate Other Fee Structure field
        public string ValidateOtherFeeField()
        {
            //driver.FindElement(btnTxnFeeType).SendKeys("Flat Fee");
            driver.FindElement(By.XPath("//label[text()='Transaction Fee Type']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item[4]/span[2]/span")).Click();
            Thread.Sleep(3000);
            string label = driver.FindElement(lblOtherFee).Text;
            return label;
        }

        //Get Validation of Other Fee Structure field
        public string GetValidationOfOtherFeeField()
        {
            Thread.Sleep(3000);          
            string label = driver.FindElement(msgOtherFee).Text;
            return label;
        }

        //Validate Incentive Fee section
        public string ValidateIncentiveFeeSection()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,450)");
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//span[text()='Incentive Fee structure']"));
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblIncentiveFeeStr, 250);
            string label = driver.FindElement(lblIncentiveFeeStr).Text;
            return label;
        }

        //Validate Base Fee
        public string ValidateBaseFee()
        {
            Thread.Sleep(4000);
            string label =  driver.FindElement(lblBaseFee).Text;
            return label;
        }

        //Validate First Ratchet Percent
        public string ValidateFirstRatchetPercent()
        {
            string label = driver.FindElement(lbl1stRatchetPercent).Text;
            return label;
        }

        //Validate First Ratchet From Amount
        public string ValidateFirstRatchetFromAmt()
        {
            string label = driver.FindElement(lbl1stRatchetFromAmt).Text;
            return label;
        }

        //Validate First Ratchet To Amount
        public string ValidateFirstRatchetToAmt()
        {
            string label = driver.FindElement(lbl1stRatchetToAmt).Text;
            return label;
        }

        //Validate 2nd Ratchet Percent
        public string Validate2ndRatchetPercent()
        {
            string label = driver.FindElement(lbl2ndRatchetPercent).Text;
            return label;
        }

        //Validate 2nd Ratchet From Amount
        public string Validate2ndRatchetFromAmt()
        {
            string label = driver.FindElement(lbl2ndRatchetFromAmt).Text;
            return label;
        }

        //Validate 2nd Ratchet To Amount
        public string Validate2ndRatchetToAmt()
        {
            string label = driver.FindElement(lbl2ndRatchetToAmt).Text;
            return label;
        }

        //Validate Fee Comments
        public string ValidateFeeComments()
        {
            string label = driver.FindElement(lblFeeComments).Text;
            return label;
        }

        //Validate 3rd Ratchet Percent
        public string Validate3rdRatchetPercent()
        {
            string label = driver.FindElement(lbl3rdRatchetPercent).Text;
            return label;
        }

        //Validate 3rd Ratchet From Amount
        public string Validate3rdRatchetFromAmt()
        {
            string label = driver.FindElement(lbl3rdRatchetFromAmt).Text;
            return label;
        }

        //Validate 3rd Ratchet To Amount
        public string Validate3rdRatchetToAmt()
        {
            string label = driver.FindElement(lbl3rdRatchetToAmt).Text;
            return label;
        }

        //Validate 4th Ratchet Percent
        public string Validate4thRatchetPercent()
        {
            string label = driver.FindElement(lbl4thRatchetPercent).Text;
            return label;
        }

        //Validate 4th Ratchet From Amount
        public string Validate4thRatchetFromAmt()
        {
            string label = driver.FindElement(lbl4thRatchetFromAmt).Text;
            return label;
        }

        //Validate 4th Ratchet To Amount
        public string Validate4thRatchetToAmt()
        {
            string label = driver.FindElement(lbl4thRatchetToAmt).Text;
            return label;
        }

        //Validate Final Ratchet Percent
        public string ValidateFinalRatchetPercent()
        {
            string label = driver.FindElement(lblFinalRatchet).Text;
            return label;
        }

        //Validate Final Ratchet Amount (MM)
        public string ValidateFinalRatchetAmt()
        {
            string label = driver.FindElement(lblFinalRatchetAmt).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,-950)");
            return label;
        }
        
        //Click Pitch tab
        public string ClickPitchTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, PitchTab, 140);
            driver.FindElement(PitchTab).Click();
            string valTab = driver.FindElement(PitchTab).Text;
            return valTab;
        }

        //Click Fairness/Admin Checklist tab
        public string ClickFairnessAdminChecklistTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, FairnessTab, 80);
            driver.FindElement(FairnessTab).Click();
            string valTab = driver.FindElement(FairnessTab).Text;
            return valTab;
        }

        //Click Administrative tab
        public string ClickAdministrativeTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabAdmin, 150);
            driver.FindElement(tabAdmin).Click();
            string valTab = driver.FindElement(tabAdmin).Text;
            return valTab;
        }

        //Click Fairness Checklist tab
        public string ClickFairnessChecklistTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabFairness, 150);
            driver.FindElement(tabFairness).Click();
            string valTab = driver.FindElement(tabFairness).Text;
            return valTab;
        }

        //Click Public Sensitivity tab
        public string ClickPublicSensitivityTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, PublicTab, 80);
            driver.FindElement(PublicTab).Click();
            string valTab = driver.FindElement(PublicTab).Text;
            return valTab;
        }

        public void ClickMoreTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, MoreTab, 80);
            driver.FindElement(MoreTab).Click();
        }

        //Click HL Internal Team tab
        public string ClickHLInternalTeamTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, HLInternalTabA, 180);
            driver.FindElement(HLInternalTabA).Click();
            string valTab = driver.FindElement(HLInternalTabA).Text;
            return valTab;
        }

        //Click Review Submission button
        public void ClickReviewSubmission()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditReviewSub, 100);
            driver.FindElement(lnkEditReviewSub).Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,50)");
            Thread.Sleep(5000);
            js.ExecuteScript("window.scrollTo(0,150)");
            Thread.Sleep(5000);
            driver.FindElement(btnSubmit).Click();
            Console.WriteLine("Submit clicked once");            
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
        }

        //Edit Review Submission button
        public void ClickEditReviewSubmission()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditReviewSub, 100);
            driver.FindElement(lnkEditReviewSub).Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,50)");
            Thread.Sleep(6000);            
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();            
        }

        //Update only Review Submission checkbox
        public void UpdateReviewSubmissionOnly()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditReviewSub2nd, 100);
            driver.FindElement(lnkEditReviewSub2nd).Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,200)");
            Thread.Sleep(6000);
            driver.FindElement(btnUpdSubmit).Click();          
             Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
        }

            //Update Review Submission and Referral Fee Owned
            public void UpdateReviewSubmissionAndUpdateReferralFee()
        {
            Thread.Sleep(3000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,100)");            
            driver.FindElement(btnUpdSubmit).Click();            
            Console.WriteLine("Submit clicked once");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(6000);           
            js.ExecuteScript("window.scrollTo(0,120)");
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditReviewSub, 100);
            driver.FindElement(lnkEditFeedback).Click();            
            Thread.Sleep(5000);
            //js.ExecuteScript("window.scrollTo(0,100)");
            driver.FindElement(btnUpdSubmit).Click();
            Thread.Sleep(3000);
            //driver.FindElement(btnUpdSubmit).Click();
            //Thread.Sleep(4000);
            driver.FindElement(FeesTab).Click();
            Thread.Sleep(3000);           
            js.ExecuteScript("window.scrollTo(0,450)");
            WebDriverWaits.WaitUntilEleVisible(driver, txtReferralFee, 200);
            driver.FindElement(txtReferralFee).SendKeys("10");
            Thread.Sleep(3000);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-550)");
            Thread.Sleep(3000);
            driver.FindElement(chkNextSchCall).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
        }

        //Update Administrative tab
        public void UpdateAdministrativeTab(string file)
        {
            Thread.Sleep(2000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,300)");
            string lockup = ReadExcelData.ReadData(excelPath, "NBCForm", 2);
            driver.FindElement(btnYes1).Click();
            driver.FindElement(By.XPath("(//lightning-base-combobox)[19]/div[1]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            Thread.Sleep(3000);
            js.ExecuteScript("window.scrollTo(0,600)");
            driver.FindElement(btnYes2).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//lightning-base-combobox)[20]/div[1]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            Thread.Sleep(4000);
            js.ExecuteScript("window.scrollTo(0,800)");
            driver.FindElement(btnYes3).Click();
            driver.FindElement(By.XPath("(//lightning-base-combobox)[21]/div[1]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            Thread.Sleep(4000);
            js.ExecuteScript("window.scrollTo(0,1150)");
            driver.FindElement(btnYes4).Click();
            driver.FindElement(By.XPath("(//lightning-base-combobox)[22]/div[1]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            Thread.Sleep(4000);
            js.ExecuteScript("window.scrollTo(0,1600)");
            driver.FindElement(btnYes5).Click();
            driver.FindElement(By.XPath("(//lightning-base-combobox)[23]/div[1]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnSave).Click();            
        }

        //Click Submit button
        public string ClickSubmitButton()
        {
            Thread.Sleep(5000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,-2500)");
            Thread.Sleep(4000);           
            driver.FindElement(btnSubmitForReview).Click();
            Thread.Sleep(9000);
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            string title = driver.FindElement(titleEmailPage).Text;
            return title;
        }

        public string GetOpportunityName()
        {
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@class='cke_wysiwyg_frame cke_reset']")));
            Thread.Sleep(3000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,300)");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valOppNum, 112);
            string emailSub = driver.FindElement(valOppNum).Text;
            Console.WriteLine(emailSub);
            driver.SwitchTo().ParentFrame();
            Thread.Sleep(2000);
            driver.FindElement(btnSendEmail).Click();
            Console.WriteLine("Clicked Send Email button");
            return emailSub;
        }       

        public string GetDateSubmitted()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valDateSubmitted, 112);
            string date = driver.FindElement(valDateSubmitted).Text;
            Console.WriteLine(date);
            return date;
        }

        //Get Validations of Opportunity Overview tab
        public string GetFieldsValidationsOfOppOverview()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgTransOverview, 130);
            string value =driver.FindElement(msgTransOverview).Text;
            return value;
        }

        //Get Validation of Total Debt
        public string GetValidationOfTotalDebt()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, msgTotalDebt, 80);
            string value = driver.FindElement(msgTotalDebt).Text;
            return value;
        }

        //Get Validation of Estimated Valuation
        public string GetValidationOfEstVal()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgEstValuation, 80);
            string value = driver.FindElement(msgEstValuation).Text;
            return value;
        }

        //Get Validation of Current Status
        public string GetValidationOfCurrentStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgCurrentStatus, 80);
            string value = driver.FindElement(msgCurrentStatus).Text;
            return value;
        }

        //Get Validation of Valuation Expectations
        public string GetValidationOfValuationExp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgValuationExp, 80);
            string value = driver.FindElement(msgValuationExp).Text;
            return value;
        }

        //Get Validation of Company Description
        public string GetValidationOfCompDesc()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgCompanyDesc, 80);
            string value = driver.FindElement(msgCompanyDesc).Text;
            return value;
        }

        //Get Validation of Real Estate Angle
        public string GetValidationOfRealEstateAngle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgRealEstate, 80);
            string value = driver.FindElement(msgRealEstate).Text;
            return value;
        }

        //Get Validation of Ownership and Capital Structure
        public string GetValidationOfOwnershipAndCapStr()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgOwnershipStr, 80);
            string value = driver.FindElement(msgOwnershipStr).Text;
            return value;
        }
        //Get Validation of Asia Angle
        public string GetValidationOfAsiaAngle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgAsiaAngle, 80);
            string value = driver.FindElement(msgAsiaAngle).Text;
            return value;
        }
         
        //Get Validation of Risk Factors
        public string GetValidationOfRiskFactors()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgRiskFact, 80);
            string value = driver.FindElement(msgRiskFact).Text;
            return value;
        }
        //Get Validation of International Angle
        public string GetValidationOfInternationalAngle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgInterAngle, 80);
            string value = driver.FindElement(msgInterAngle).Text;
            return value;
        }

        //Get Validation of Capital Markets Consulted
        public string GetValidationOfCapMarketConsulted()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgCapMkt, 80);
            string value = driver.FindElement(msgCapMkt).Text;
            return value;
        }

        //Get Validation of Existing Financial Arrangement Notes
        public string GetValidationOfExistingFin()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgExistingFin, 80);
            string value = driver.FindElement(msgExistingFin).Text;
            return value;
        }

        //Get Validation of Financials Subject to Audit
        public string GetValidationOfFinancialsSubject()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgFinSubject, 80);
            string value = driver.FindElement(msgFinSubject).Text;
            return value;
        }

        //Get Validation of No Financials 
        public string GetValidationOfNoFinancials()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgNoFin, 80);
            string value = driver.FindElement(msgNoFin).Text;
            return value;
        }

        //Get Validation of Retainer 
        public string GetValidationOfRetainer()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgRetainer, 80);
            string value = driver.FindElement(msgRetainer).Text;
            return value;
        }

        //Get Validation of Retainer Fee 
        public string GetValidationOfRetainerFee()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgRetainerFee, 80);
            string value = driver.FindElement(msgRetainerFee).Text;
            return value;
        }

        //Get Validation of Progress Fee 
        public string GetValidationOfProgressFee()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgProgressFee, 80);
            string value = driver.FindElement(msgProgressFee).Text;
            return value;
        }

        //Get Validation of Minimum Fee 
        public string GetValidationOfMinFee()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgMinFee, 80);
            string value = driver.FindElement(msgMinFee).Text;
            return value;
        }

        //Get Validation of Minimum Fee 
        public string GetValidationOfMinimumFee()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgMinFee, 80);
            string value = driver.FindElement(msgMinFee).Text;
            return value;
        }

        //Get Validation of Transaction Fee 
        public string GetValidationOfTxnFee()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgTxnFeeType, 80);
            string value = driver.FindElement(msgTxnFeeType).Text;
            return value;
        }

        //Get Validation of Estimated Transaction Value (MM)
        public string GetValidationOfEstTxnValue()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgEstTxnValue, 80);
            string value = driver.FindElement(msgEstTxnValue).Text;
            return value;
        }
        //Get Validation of Will There Be a Pitch 
        public string GetValidationOfPitch()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgWillThere, 80);
            string value = driver.FindElement(msgWillThere).Text;
            return value;
        }

        //Get Validation of HL Competition 
        public string GetValidationOfHLComp()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgHLComp, 80);
            string value = driver.FindElement(msgHLComp).Text;
            return value;
        }

        //Get Validation of Lockups on Future M&A or Financing Work
        public string GetValidationOfLockups()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgLockups, 80);
            string value = driver.FindElement(msgLockups).Text;
            return value;
        }
        //Get Validation of Existing Relationships
        public string GetValidationOfExistingRel()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgExistingRel, 80);
            string value = driver.FindElement(msgExistingRel).Text;
            return value;
        }

        //Get Validation of Existing or Repeat Client
        public string GetValidationOfExistingOrRepeatClient()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgExistingOrRepeat, 80);
            string value = driver.FindElement(msgExistingOrRepeat).Text;
            return value;
        }

        //Get Validation of TAS/Bridge Assistance Benefit
        public string GetValidationOfTASBridgeAssist()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgTAS, 80);
            string value = driver.FindElement(msgTAS).Text;
            return value;
        }

        //Get Validation of Outside Council
        public string GetValidationOfOutsideCouncil()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgOutside, 80);
            string value = driver.FindElement(msgOutside).Text;
            return value;
        }

        //Get Validation of Fairness Opinion
        public string GetValidationOfFairnessOpinion()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgFairnessOpinion, 80);
            string value = driver.FindElement(msgFairnessOpinion).Text;
            return value;
        }

        //Get Validation of A
        public string GetValidationOfA()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgA, 80);
            string value = driver.FindElement(msgA).Text;
            return value;
        }

        //Get Validation of B
        public string GetValidationOfB()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgB, 80);
            string value = driver.FindElement(msgB).Text;
            return value;
        }
        //Get Validation of C
        public string GetValidationOfC()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgC, 80);
            string value = driver.FindElement(msgC).Text;
            return value;
        }
        //Get Validation of D
        public string GetValidationOfD()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgD, 80);
            string value = driver.FindElement(msgD).Text;
            return value;
        }

        //Get Validation of Group Head Approval
        public string GetValidationOfGroupHeadApproval()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgGroupHead, 80);
            string value = driver.FindElement(msgGroupHead).Text;
            return value;
        }

        //Save all required details in Opportunity Overview tab
        public void SaveAllReqFieldsInOppOverview(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(txtTxnOverview).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 3));
            driver.FindElement(txtTotalDebt).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 9));
            //driver.FindElement(txtEstValuation).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 11));
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,80)");
            Thread.Sleep(4000);
            driver.FindElement(txtValuationExp).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 12));
            driver.FindElement(btnCurrentStatus).Click();
            string name = ReadExcelData.ReadData(excelPath, "NBCForm", 4);
            driver.FindElement(By.XPath("//label[text()='Current Status']/following::lightning-base-combobox-item/span[2]/span[text()='" + name + "']")).Click();
            driver.FindElement(txtCompanyDesc).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 5));
            js.ExecuteScript("window.scrollTo(0,250)");
            Thread.Sleep(4000);
            string real = ReadExcelData.ReadData(excelPath, "NBCForm", 7);
            driver.FindElement(comboRealEstate).Click();
            driver.FindElement(By.XPath("//label[text()='Real Estate Angle']/following::lightning-base-combobox-item/span[2]/span[text()='" + real + "']")).Click();
            driver.FindElement(txtOwnership).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 8));
            js.ExecuteScript("window.scrollTo(0,380)");
            Thread.Sleep(4000);
            driver.FindElement(comboAsia).Click();
            driver.FindElement(By.XPath("//label[text()='Asia Angle']/following::lightning-base-combobox-item/span[2]/span[text()='" + real + "']")).Click();
            driver.FindElement(txtRisk).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 13));
            js.ExecuteScript("window.scrollTo(0,480)");
            Console.WriteLine("about to enter int angle ");
            driver.FindElement(comboSanctions).Click();
            driver.FindElement(By.XPath("//label[text()='Sanctions Concerns/Issues?']/following::lightning-base-combobox-item/span[2]/span[text()='No']")).Click();
            Thread.Sleep(5000);
            driver.FindElement(comboInternational).Click();
            Thread.Sleep(4000);            
            driver.FindElement(By.XPath("//flexipage-field[7]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-200)"); 
        }

        //Save all required details in Financial Overview tab
        public void SaveAllReqFieldsInFinancialsOverview(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(5000);
            driver.FindElement(btnCapMkt).Click();
            string name = ReadExcelData.ReadData(excelPath, "NBCForm", 7);
            driver.FindElement(By.XPath("//label[text()='Capital Markets Consulted']/following::div[6]/lightning-base-combobox-item/span[2]/span[text()='"+name+"']")).Click();
            driver.FindElement(txtExistingFin).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 3));
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,250)");
            Thread.Sleep(4000);            
            driver.FindElement(btnFinSubject).Click();
            driver.FindElement(btnFinSubject).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//label[text()='Financials Subject to Audit']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtFinAuditNotes).SendKeys("Testing");
            js.ExecuteScript("window.scrollTo(0,650)");
            Thread.Sleep(4000);
            driver.FindElement(chkNoFin).Click();
            driver.FindElement(chkNoFin).Click();
            Thread.Sleep(5000);
            js.ExecuteScript("window.scrollTo(0,250)");
            driver.FindElement(txtNoFinExp).SendKeys("Testing");
            Thread.Sleep(3000);              
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-150)");
        }

        //Save all required details in Fees tab
        public void SaveAllReqFieldsInFees(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,250)");
            string fee = ReadExcelData.ReadData(excelPath, "NBCForm", 9);            
            driver.FindElement(txtRetainer).SendKeys(fee);
            driver.FindElement(txtRetainerFeeCred).SendKeys(fee);
            driver.FindElement(txtProgressFee).SendKeys(fee);
            driver.FindElement(txtProgressFeeCred).SendKeys(fee);
            js.ExecuteScript("window.scrollTo(0,350)");
            driver.FindElement(txtMinFee).SendKeys(fee);
            driver.FindElement(txtEstTxnVal).SendKeys(fee);
            //driver.FindElement(btnTxnFeeType).Click();           
            //Thread.Sleep(5000);
            //driver.FindElement(By.XPath("//label[text()='Transaction Fee Type']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-150)");
        }

        public void SaveAllReqFieldsInFeesWhenTypeIsIncentive(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,250)");
            string fee = ReadExcelData.ReadData(excelPath, "NBCForm", 64);
            string feeCred = ReadExcelData.ReadData(excelPath, "NBCForm", 65);
            driver.FindElement(txtRetainer).Clear();
            driver.FindElement(txtRetainer).SendKeys(fee);
            driver.FindElement(txtRetainerFeeCred).SendKeys(feeCred);
            driver.FindElement(txtProgressFee).Clear();
            driver.FindElement(txtProgressFee).SendKeys(fee);
            driver.FindElement(txtProgressFeeCred).SendKeys(feeCred);
            js.ExecuteScript("window.scrollTo(0,300)");            
            driver.FindElement(txtMinFee).SendKeys(fee);
            driver.FindElement(txtEstTxnVal).SendKeys(fee);
            Thread.Sleep(6000);           
            driver.FindElement(btnUpdSubmit).Click();
            js.ExecuteScript("window.scrollTo(0,300)");     
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);           
        }

        //Save all required details in Pitch tab
        public void SaveAllReqFieldsInPitch(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            string text = ReadExcelData.ReadData(excelPath, "NBCForm", 3);
            string fee = ReadExcelData.ReadData(excelPath, "NBCForm", 10);
            string lockup = ReadExcelData.ReadData(excelPath, "NBCForm",2);
            driver.FindElement(btnWillThere).Click();           
            driver.FindElement(By.XPath("//label[text()='Will There Be a Pitch?']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + fee + "']")).Click();
            driver.FindElement(txtHLComp).SendKeys(text);
            driver.FindElement(btnLockups).Click();
            driver.FindElement(By.XPath("//label[text()='Lockups on Future M&A or Financing Work']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            js.ExecuteScript("window.scrollTo(0,250)");
            Thread.Sleep(3000);
            driver.FindElement(btnExistingRel).Click();           
            driver.FindElement(By.XPath("(//lightning-base-combobox)[12]/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            driver.FindElement(btnExistingClient).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("(//lightning-base-combobox)[13]/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            js.ExecuteScript("window.scrollTo(0,450)");
            driver.FindElement(btnTAS).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//label[text()='TAS/Bridge Assistance Benefit?']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + fee + "']")).Click();
            driver.FindElement(txtOutsideCouncil).SendKeys(text);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-150)");
        }

        //Save all required details in Fairness/AdminChecklist tab
        public void SaveAllReqFieldsInFairnessAdminChecklist(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;           
            string lockup = ReadExcelData.ReadData(excelPath, "NBCForm", 2);
            driver.FindElement(btnFairnessOpinion).Click();
            driver.FindElement(By.XPath("//label[text()='Fairness Opinion Provided']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            driver.FindElement(tabAdmin).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnRestricted).Click();
            driver.FindElement(By.XPath("//label[text()='Restricted List']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-150)");
        }

        //Save all required details in Public Senstivity tab
        public void SaveAllReqFieldsInPublicSenstivity(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            string lockup = ReadExcelData.ReadData(excelPath, "NBCForm", 2);
            driver.FindElement(btnA).Click();
            driver.FindElement(By.XPath("//label[text()='A']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            js.ExecuteScript("window.scrollTo(0,280)");
            driver.FindElement(btnB).Click();
            driver.FindElement(By.XPath("//label[text()='B']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo(0,400)");
            driver.FindElement(btnC).Click();
            driver.FindElement(By.XPath("//label[text()='C']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            Thread.Sleep(3000);
            js.ExecuteScript("window.scrollTo(0,600)");
            Thread.Sleep(2000);
            driver.FindElement(btnD).Click();
            driver.FindElement(By.XPath("//label[text()='D']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            Thread.Sleep(3000);
            js.ExecuteScript("window.scrollTo(0,800)");
            Thread.Sleep(3000);
            driver.FindElement(chkGroupHead).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-150)");
        }

        //Fetch the label of Related Opportunity
        public string GetLabelRelatedOpportunity()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblRelOpp, 100);
            string text = driver.FindElement(lblRelOpp).Text;
            return text;
        }

        //Fetch the label of Transaction Overview
        public string GetLabelTxnOverview()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblTxnOver, 100);
            string text = driver.FindElement(lblTxnOver).Text;
            return text;
        }
        //Fetch the label of Current Status
        public string GetLabelCurrentStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCurrentStatus, 100);
            string text = driver.FindElement(lblCurrentStatus).Text;
            return text;
        }
        //Fetch the label of Company Description
        public string GetLabelCompDesc()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCompDesc, 100);
            string text = driver.FindElement(lblCompDesc).Text;
            return text;
        }
        //Fetch the label of Ownership and Capital Sructure
        public string GetLabelOnwershipStructure()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblOwnerCapStr, 100);
            string text = driver.FindElement(lblOwnerCapStr).Text;
            return text;
        }

        //Fetch the label of Risk Factors
        public string GetLabelRiskFactors()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRiskFact, 100);
            string text = driver.FindElement(lblRiskFact).Text;
            return text;
        }

        //Fetch the label of International Angle
        public string GetLabelIntAngle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblIntAngle, 100);
            string text = driver.FindElement(lblIntAngle).Text;
            return text;
        }

        //Fetch the label of Total Debt (MM)
        public string GetLabelTotalDebtMM()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblTotalDebtMM, 100);
            string text = driver.FindElement(lblTotalDebtMM).Text;
            return text;
        }

        //Fetch the label of Estimated Valuation
        public string GetLabelExtVal()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblEstimatedValuation, 100);
            string text = driver.FindElement(lblEstimatedValuation).Text;
            return text;
        }

        //Fetch the label of Sanctions
        public string GetLabelSanctions()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblSanctions, 100);
            string text = driver.FindElement(lblSanctions).Text;
            return text;
        }

        //Fetch the label of Real Estate Angle
        public string GetLabelRealEstAngle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRealEstate, 100);
            string text = driver.FindElement(lblRealEstate).Text;
            return text;
        }

        //Fetch the label of Asia Angle
        public string GetLabelAsiaAngle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblAsiaAngle, 100);
            string text = driver.FindElement(lblAsiaAngle).Text;
            return text;
        }

        //Fetch message of Capital Market
        public string GetMessageCapitalMarket()
            {
            
            WebDriverWaits.WaitUntilEleVisible(driver, msgCapMarket, 100);
            string text = driver.FindElement(msgCapMarket).Text;
            return text;
            }

        //Fetch the label of Capital Market Consulted
        public string GetLabelCapMktConsult()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCapMkt, 100);
            string text = driver.FindElement(lblCapMkt).Text;
            return text;
        }

        //Fetch the label of Existing Financial Arrangement Notes
        public string GetLabelExistingFinNotes()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblExistFin, 100);
            string text = driver.FindElement(lblExistFin).Text;
            return text;
        }

        //Fetch message of Above Financials
        public string GetMessageFinSubToAudit()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, msgAboveFin, 100);
            string text = driver.FindElement(msgAboveFin).Text;
            return text;
        }

        //Fetch the label of Financials Subject to Audit
        public string GetLabelFinSubToAudit()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblFinSub, 100);
            string text = driver.FindElement(lblFinSub).Text;
            return text;
        }

        //Fetch message of Financials Unavailable
        public string GetMessageFinUnavailable()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, msgFinAvail, 100);
            string text = driver.FindElement(msgFinAvail).Text;
            return text;
        }

        //Fetch the label of No Financials 
        public string GetLabelNoFin()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblNoFin, 100);
            string text = driver.FindElement(lblNoFin).Text;
            return text;
        }

        //Fetch the label of No Financials Explanation
        public string GetLabelNoFinExp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblNoFinExp, 100);
            string text = driver.FindElement(lblNoFinExp).Text;
            return text;
        }

        //Fetch the label of Retainer
        public string GetLabelRetainer()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRetainer, 100);
            string text = driver.FindElement(lblRetainer).Text;
            return text;
        }

        //Fetch the label of Progress Fee MM
        public string GetLabelProgressFee()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblProgressFee, 100);
            string text = driver.FindElement(lblProgressFee).Text;
            return text;
        }

        //Fetch the label of Minimum Fee MM
        public string GetLabelMinimumFee()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblMinFee, 100);
            string text = driver.FindElement(lblMinFee).Text;
            return text;
        }

        //Fetch the label of Transaction Fee Type
        public string GetLabelTxnFeeType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblTxnFee, 100);
            string text = driver.FindElement(lblTxnFee).Text;
            return text;
        }

        //Fetch the label of Retainer Fee Creditable
        public string GetLabelEstTxn()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblEstTxn, 100);
            string text = driver.FindElement(lblEstTxn).Text;
            return text;
        }

        //Fetch the label of Estimated Transaction Value (MM) Report
        public string GetLabelEstTxnValueReport()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblEstTxnValueReport, 100);
            string text = driver.FindElement(lblEstTxnValueReport).Text;
            return text;
        }

        //Fetch the label of Retainer Fee Creditable ?
        public string GetLabelRetainerFeeCred()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRetainerFeeCred, 100);
            string text = driver.FindElement(lblRetainerFeeCred).Text;
            return text;
        }

        //Fetch the label of Currency
        public string GetLabelCurrency()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCurrencyFee, 100);
            string text = driver.FindElement(lblCurrencyFee).Text;
            return text;
        }

        //Fetch the label of Progress Fee Creditable
        public string GetLabelProgressFeeCred()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblProgFeeCredit, 100);
            string text = driver.FindElement(lblProgFeeCredit).Text;
            return text;
        }

        //Fetch the label of Pre Pitch section
        public string GetLabelPrePitch()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secPrePitch, 100);
            string text = driver.FindElement(secPrePitch).Text;
            return text;
        }

        //Fetch the label of Will There Be a Pitch?
        public string GetLabelWillThereBePitch()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblWillThere, 100);
            string text = driver.FindElement(lblWillThere).Text;
            return text;
        }

        //Fetch the label of Houlihan Lokey Competition
        public string GetLabelHLComp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblHLComp, 100);
            string text = driver.FindElement(lblHLComp).Text;
            return text;
        }

        //Fetch the label of Existing Relationships
        public string GetLabelExistingRel()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblExistingRel, 100);
            string text = driver.FindElement(lblExistingRel).Text;
            return text;
        }

        //Fetch the label of Existing or Repeat Client?
        public string GetLabelExistingOrRepeatClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblExisting, 100);
            string text = driver.FindElement(lblExisting).Text;
            return text;
        }

        //Fetch the label of Who Are The Key Decision-Makers?
        public string GetLabelWhoAreTheKeyDecisionMakers()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblWhoAre, 100);
            string text = driver.FindElement(lblWhoAre).Text;
            return text;
        }

        //Fetch the label of Lockups on Future M&A or Financing Work
        public string GetLabelLockupsOnFutureMA()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblLockups, 150);
            string text = driver.FindElement(lblLockups).Text;
            return text;
        }

        //Fetch the label of If known, identify the name(s) of the client’s outside counsel and/or other advisors (If any)
        public string GetLabelIfKnown()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secIfKnown, 100);
            string text = driver.FindElement(secIfKnown).Text;
            return text;
        }

        //Fetch the label of Outside Council
        public string GetLabelOutsideCouncil()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblOutside, 210);
            string text = driver.FindElement(lblOutside).Text;
            return text;
        }

        //Fetch the label of Referral
        public string GetLabelReferral()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secReferral, 100);
            string text = driver.FindElement(secReferral).Text;
            return text;
        }

        //Fetch the label of Referral Type
        public string GetLabelReferralType()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,250)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblRefType, 130);
            string text = driver.FindElement(lblRefType).Text;
            return text;
        }

        //Fetch the label of Referral Source
        public string GetLabelReferralSource()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRefSource, 100);
            string text = driver.FindElement(lblRefSource).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,-500)");
            return text;
        }

        //Fetch the label of Section Is there a potential Fairness Opinion component to this assignment?
        public string GetLabelIsTherePotentialFairness()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secIsPotential, 100);
            string text = driver.FindElement(secIsPotential).Text;
            return text;
        }

        //Fetch the label of Fairness Opinion Provided
        public string GetLabelFairnessOpinionProvided()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblFairnessOpinion, 100);
            string text = driver.FindElement(lblFairnessOpinion).Text;
            return text;
        }

        //Fetch the label of Restricted List Information section
        public string GetLabelRestrictedListInformation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secRestricted, 100);
            string text = driver.FindElement(secRestricted).Text;
            return text;
        }

        //Fetch the label of Restricted List 
        public string GetLabelRestrictedList()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRestictedList, 100);
            string text = driver.FindElement(lblRestictedList).Text;
            return text;
        }

        //Fetch the label of CC Information section
        public string GetLabelCCInformation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secCCInfo, 100);
            string text = driver.FindElement(secCCInfo).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,300)");
            Thread.Sleep(3000);
            return text;
        }

        //Fetch the label of CC Status
        public string GetLabelCCStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCCStatus, 100);
            string text = driver.FindElement(lblCCStatus).Text;           
            return text;
        }

        //Fetch the label of Are There any Pitch section
        public string GetLabelAreThereAnyPitch()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secAreThereAnyPitch, 100);
            string text = driver.FindElement(secAreThereAnyPitch).Text;
            return text;
        }

        //Fetch the label of 1st YES/NO
        public string GetLabel1stYesNo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lbl1stYesNo, 100);
            string text = driver.FindElement(lbl1stYesNo).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,300)");
            Thread.Sleep(3000);
            return text;
        }

        //Fetch the label of 1st Have any of deal team section
        public string GetLabelRespectiveAffiliates()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secHaveAny1, 100);
            string text = driver.FindElement(secHaveAny1).Text;
            return text;
        }

        //Fetch the label of 2nd YES/NO
        public string GetLabel2ndYesNo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lbl2ndYesNo, 100);
            string text = driver.FindElement(lbl2ndYesNo).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,450)");
            Thread.Sleep(3000);
            return text;
        }

        //Fetch the label of 2nd Have any of deal team section
        public string GetLabelRespectiveMgmt()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secHaveAny2, 100);
            string text = driver.FindElement(secHaveAny2).Text;
            return text;
        }

        //Fetch the label of 3rd YES/NO
        public string GetLabel3rdYesNo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lbl3rdYesNo, 100);
            string text = driver.FindElement(lbl3rdYesNo).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,750)");
            Thread.Sleep(4000);
            return text;
        }

        //Fetch the label of 3rd Have any of deal team section
        public string GetLabelConflictOfInterest()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secHaveAny3, 100);
            string text = driver.FindElement(secHaveAny3).Text;
            return text;
        }

        //Fetch the label of 4th YES/NO
        public string GetLabel4thYesNo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lbl4thYesNo, 100);
            string text = driver.FindElement(lbl4thYesNo).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,500)");
            Thread.Sleep(3000);
            return text;
        }

        //Fetch the label of perception of a conflict of interest?
        public string GetLabelPerceptionOfConflictOfInterest()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secHaveAny4, 100);
            string text = driver.FindElement(secHaveAny4).Text;
            return text;
        }

        //Fetch the label of 4th YES/NO
        public string GetLabel5thYesNo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lbl5thYesNo, 100);
            string text = driver.FindElement(lbl5thYesNo).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,-1000)");
            Thread.Sleep(3000);
            return text;
        }

        //Fetch the section A
        public string GetMessageOfA()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secA, 100);
            string text = driver.FindElement(secA).Text;
            return text;
        }

        //Fetch the label of A
        public string GetLabelOfA()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblA, 100);
            string text = driver.FindElement(lblA).Text;           
            return text;
        }

        //Fetch the section B
        public string GetMessageOfB()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secB, 100);
            string text = driver.FindElement(secB).Text;
            return text;
        }

        //Fetch the label of B
        public string GetLabelOfB()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblB, 100);
            string text = driver.FindElement(lblB).Text;
            return text;
        }

        //Fetch the section C
        public string GetMessageOfC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secC, 100);
            string text = driver.FindElement(secC).Text;
            return text;
        }

        //Fetch the label of C
        public string GetLabelOfC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblC, 100);
            string text = driver.FindElement(lblC).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,450)");
            Thread.Sleep(3000);
            return text;
        }
        //Fetch the section D
        public string GetMessageOfD()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secD, 100);
            string text = driver.FindElement(secD).Text;
            return text;
        }

        //Fetch the label of D
        public string GetLabelOfD()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblD, 100);
            string text = driver.FindElement(lblD).Text;
            return text;
        }

        //Fetch the section of Confirmation
        public string GetMessageOfConfirmation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secPlsConfirm, 100);
            string text = driver.FindElement(secPlsConfirm).Text;
            return text;
        }

        //Fetch the label of Group Head Approval
        public string GetLabelOfGroupHeadApproval()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblGroupHead, 100);
            string text = driver.FindElement(lblGroupHead).Text;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,-850)");
            Thread.Sleep(3000);
            return text;
        }

        //Fetch the label of Staff
        public string GetLabelOfStaff()
        {
            Thread.Sleep(6000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//flexipage-tab2[7]/slot/flexipage-component2/slot/flexipage-aura-wrapper/div/article/div[2]/div/force-aloha-page/div/iframe")));
            //driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblStaff, 150);
            string text = driver.FindElement(lblStaff).Text;
            return text;
        }

        //Fetch the label of Staff
        public string GetLabelOfCNBCStaff()
        {
            Thread.Sleep(6000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//flexipage-tab2[4]/slot/flexipage-component2/slot/flexipage-aura-wrapper/div/article/div[2]/div/force-aloha-page/div/iframe")));
            //driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblStaff, 150);
            string text = driver.FindElement(lblStaff).Text;
            return text;
        }


        //Validate Save button
        public string ValidateSaveButton()
        {
            Thread.Sleep(4000);          
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveITTeam, 130);
            string text = driver.FindElement(btnSaveITTeam).GetAttribute("value");
            return text;
        }

        //Validate Return To Opportunity button
        public string ValidateReturnToOppButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 100);
            string text = driver.FindElement(btnReturnToOpp).GetAttribute("value");
            return text;
        }

        //Validate Role Definitions button
        public string ValidateRoleDefButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnRoleDef, 100);
            string text = driver.FindElement(btnRoleDef).GetAttribute("value");
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(3000);
            return text;
        }

        //Validate Next Scheduled Call checkbox
        public string ValidateNextScheduledCallCheckbox()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblNextSchCall, 100);
            string text = driver.FindElement(lblNextSchCall).Text;
            return text;
        }

        //Validate Req feedback prior to normal sched call checkbox
        public string ValidateReqFeedbackCheckbox()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblReqFeedback, 100);
            string text = driver.FindElement(lblReqFeedback).Text;
            return text;
        }

        //Validate Review Submission checkbox
        public string ValidateRequiresFeedbackInGMTCheckbox()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRequiresFeedback, 100);
            string text = driver.FindElement(lblRequiresFeedback).Text;            
            Thread.Sleep(4000);
            return text;
        }

        //Validate Review Submission checkbox
        public string ValidateReviewSubCheckbox()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblReviewSub, 100);
            string text = driver.FindElement(lblReviewSub).Text;
            Thread.Sleep(4000);
            return text;
        }

        //Validate Attachments tab
        public string ValidateAttachemntsTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkAttachments, 100);
            string text = driver.FindElement(lnkAttachments).Text;
            driver.FindElement(lnkAttachments).Click();
            Thread.Sleep(6000);
            return text;
        }

        //Validate Files section
        public string ValidateFilesSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secFiles, 100);
            string text = driver.FindElement(secFiles).Text;           
            return text;
        }

        //Validate Upload Files Button
        public string ValidateUploadFilesButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnUploadFiles, 100);
            string text = driver.FindElement(btnUploadFiles).Text;
            return text;
        }

        //Validate Ownership Details section
        public string ValidateOwnerDetailsSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secOwnershipDetails, 100);
            string text = driver.FindElement(secOwnershipDetails).Text;
            return text;
        }

        //Validate Client Ownership Field
        public string ValidateClientOwnershipField()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblClientOwnership, 100);
            string text = driver.FindElement(lblClientOwnership).Text;
            return text;
        }
        //Validate Subject Ownership Field
        public string ValidateSubjectOwnershipField()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblSubOwnership, 100);
            string text = driver.FindElement(lblSubOwnership).Text;
            return text;
        }

        //Validate Value of Client Ownership 
        public string ValidateValueOfClientOwnership()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valClientOwnership, 100);
            string text = driver.FindElement(valClientOwnership).Text;
            return text;
        }

        //Validate Value of Subject Ownership Field
        public string ValidateValueOfSubjectOwnership()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSubOwnership, 100);
            string text = driver.FindElement(valSubOwnership).Text;
            return text;
        }
        //Validate Approval History section
        public string ValidateApprovalHistorySection()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, secApprovalHistory, 100);
            string text = driver.FindElement(secApprovalHistory).Text;            
            Thread.Sleep(5000);
            return text;
        }

        //Update Fairness Opinion Provided option
        public void UpdateFairnessOpinionProvided()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditFairnessOpinion, 100);
            driver.FindElement(lnkEditFairnessOpinion).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnFairnessOpinion).Click();
            driver.FindElement(By.XPath("//label[text()='Fairness Opinion Provided']/following::lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='Yes']")).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
        }

        //Validate Fairness Opinion Publicy Disclosed
        public string ValidateFairnessOpinionPublicly()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblFairnessOpinionPublicly, 100);
            string text = driver.FindElement(lblFairnessOpinionPublicly).Text;          
            return text;
        }

        //Validate Shareholder Vote?
        public string ValidateShareholderVote()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblShareholderVote, 100);
            string text = driver.FindElement(lblShareholderVote).Text;
            return text;
        }

        //Validate Proposed Fee Range
        public string ValidateProposedFeeRange()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblProposedFee, 100);
            string text = driver.FindElement(lblProposedFee).Text;
            return text;
        }

        //Validate Fairness Fee Inclusion
        public string ValidateFairnessFeeInclusion()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblFairnessFee, 100);
            string text = driver.FindElement(lblFairnessFee).Text;
            return text;
        }

        //Validate Fairness Opinion Staffing Notes
        public string ValidateFairnessOpinionStaffingNotes()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblFairnessOpinionStaff, 100);
            string text = driver.FindElement(lblFairnessOpinionStaff).Text;
            return text;
        }

        //Validate The consideration to be received by the Unaffiliated Stockholders in the Transaction is fair to them from a financial point of view.
        public string ValidateUnaffiliatedStockHoldersText()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,300)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblUnaffiliatedStockHolders, 100);
            string text = driver.FindElement(lblUnaffiliatedStockHolders).Text;
            return text;
        }

        //Validate Check if applicable of Unaffiliated Stockholders
        public string ValidateCheckIfOfUnaffiliated()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCheck1, 100);
            string text = driver.FindElement(lblCheck1).Text;
            return text;
        }

        //Validate The consideration to be received by the Company in the Transaction is fair to the Company from a financial point of view.
        public string ValidateCompanyInTransactionText()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,450)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblCompInTrans, 100);
            string text = driver.FindElement(lblCompInTrans).Text;
            return text;
        }

        //Validate Check if applicable of Company in the Transaction
        public string ValidateCheckIfOfComp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCheck2, 100);
            string text = driver.FindElement(lblCheck2).Text;
            return text;
        }

        //Validate The consideration to be paid by the Company in the Transaction is fair from a financial point of view.
        public string ValidateCompanyInTransaction2ndText()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,300)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblCompInTrans2, 100);
            string text = driver.FindElement(lblCompInTrans2).Text;
            return text;
        }

        //Validate Check if applicable of Company in the Transaction 2nd
        public string ValidateCheckIfOfComp2nd()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCheck3, 100);
            string text = driver.FindElement(lblCheck3).Text;
            return text;
        }

        //Validate The Exchange Ratio provided for in the Transaction is fair to the Company from a financial point of view.
        public string ValidateExchangeRatioComp()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,300)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblExchangeRatio, 100);
            string text = driver.FindElement(lblExchangeRatio).Text;
            return text;
        }

        //Validate Check if applicable of Exchange Ratio to Company
        public string ValidateCheckIfOfExchangeRatioToComp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCheck4, 100);
            string text = driver.FindElement(lblCheck4).Text;
            return text;
        }

        //Validate The Exchange Ratio provided for in the Transaction is fair to the Unaffiliated Stockholders from a financial point of view.
        public string ValidateExchangeRatioUnaffiliated()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblExchangeRatioUnaffil, 100);
            string text = driver.FindElement(lblExchangeRatioUnaffil).Text;
            return text;
        }

        //Validate Check if applicable of Exchange Ratio to to the Unaffiliated Stockholders
        public string ValidateCheckIfOfExchangeRatioUnaffiliated()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,450)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblCheck5, 100);
            string text = driver.FindElement(lblCheck5).Text;
            return text;
        }

        //Validate Not Yet Known
        public string ValidateNotYetKnown()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblNotYet, 100);
            string text = driver.FindElement(lblNotYet).Text;
            return text;
        }

        //Validate Check if applicable of Not Yet Known
        public string ValidateCheckIfOfNotYetKnown()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCheck6, 100);
            string text = driver.FindElement(lblCheck6).Text;
            return text;
        }

        //Validate Other specify, below
        public string ValidateOtherSpecify()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,400)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblOthersSpecify, 100);
            string text = driver.FindElement(lblOthersSpecify).Text;
            return text;
        }

        //Validate Check if applicable of Other specify, below
        public string ValidateCheckIfOfOthersSpecify()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCheck7, 100);
            string text = driver.FindElement(lblCheck7).Text;
            return text;
        }

        //Validate Form of Opinion Notes
        public string ValidateFormofOpinionNotes()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblFormOfOpinion, 100);
            string text = driver.FindElement(lblFormOfOpinion).Text;
            return text;
        }

        //Validate Is this a Related Party Transaction?
        public string ValidateRelatedPartyTransaction()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,300)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblRelatedParty, 100);
            string text = driver.FindElement(lblRelatedParty).Text;
            return text;
        }

        //Validate Related Party Transaction
        public string ValidateRelatedPartyTxn()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRelatedPartyTxn, 100);
            string text = driver.FindElement(lblRelatedPartyTxn).Text;
            //driver.Close();
            Thread.Sleep(5000);
            return text;
        }

        //Fetch validations for mandatory fields
        public string GetFieldsValidations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmit, 80);            
            driver.FindElement(btnSubmit).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 80);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();            
            WebDriverWaits.WaitUntilEleVisible(driver, errorList, 90);
            string errorDetails = driver.FindElement(errorList).Text.Replace("\r\n", ", ").ToString();
            return errorDetails;
        }
        public void ClickCancelAndAcceptAlert()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(2000);
            driver.SwitchTo().Alert().Accept();
        }
        public string ClickToggleAndValidateTabs()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkToggleTabs, 80);
            driver.FindElement(checkToggleTabs).Click();
            bool tabsPresent = driver.FindElement(tabList).Displayed;
            if (tabsPresent == true)
            {
                string lblTabslist = driver.FindElement(tabList).Text.Replace("\r\n", ", ").ToString();
                Console.WriteLine(lblTabslist);
                return lblTabslist;
            }
            else
            {
                return "Tabs are not displayed";
            }
        }


        public void SwitchFrame()
        {
            driver.Close();
            Console.WriteLine("Closed the last window");
            Thread.Sleep(6000);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        //Validate Submit for Review button
        public string ValidateSubmitForReviewButton()
        {
            try
            {
                Thread.Sleep(5000);
                Console.WriteLine("waiting for review button");
                WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitForReview,200);
                string value = driver.FindElement(btnSubmitForReview).Displayed.ToString();
                Console.WriteLine(value);
                if (value.Equals("True"))
                {
                    return "Submit for Review button is displayed";
                }
                else
                {
                    return "Submit for Review button is not displayed";
                }
            }
            catch (Exception e)
            {
                return "Submit for Review button is not displayed";
            }
        }

        //Click Review tab
        public string ClickReviewTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabReview, 150);
            driver.FindElement(tabReview).Click();
            string valTab = driver.FindElement(tabReview).Text;
            return valTab;
        }

        //Update Grade value
        public string UpdateGrade()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditGrade, 290);
            driver.FindElement(lnkEditGrade).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnGrade).Click();
            Console.WriteLine("Clicked Grade");
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//label[text()='Grade']/following::lightning-base-combobox[1]/div[1]/div[2]/lightning-base-combobox-item/span[2]/span[text()='A+']")).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(valGrade).Text;
            return value;
        }
       
        public string ValidateHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, titleEmailPage, 70);
            string title = driver.FindElement(titleEmailPage).Text;
            Console.WriteLine(title);
            return title;
        }
        public string GetOppName()
        {
            driver.SwitchTo().Frame(0);            
            WebDriverWaits.WaitUntilEleVisible(driver, valEmailOppName, 112);
            string emailSub = driver.FindElement(valEmailOppName).Text;
            Console.WriteLine(emailSub);
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnCancelEmail).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturntoOpp, 70);
            driver.FindElement(btnReturntoOpp).Click();
            return emailSub;
        }

        public string GetCCStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, statusCC, 90);
            string status = driver.FindElement(statusCC).Text;
            return status;
        }     

        //Update FinancialOption
        public string UpdateFinancialOption(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboFinancialOpinion, 80);
            driver.FindElement(comboFinancialOpinion).SendKeys(value);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valFinOption, 80);
            string valFin = driver.FindElement(valFinOption).Text;
            return valFin;
        }

        //Get Role Text
        public string GetRoleText()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtComment, 80);
            string txtValue = driver.FindElement(txtComment).Text;
            return txtValue;
        }

        //Get Suggested Fees
        public string GetSuggestedFees()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tblSuggestedFee, 100);
            string txtFees = driver.FindElement(tblSuggestedFee).Text.Replace("\r\n", " ");
            return txtFees;
        }

        //Click EU Override
        public void ClickEUOverrideButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEUOverride, 80);
            driver.FindElement(btnEUOverride).Click();
        }

        //Get EU Override Text
        public string GetEUOverrideText()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEUComment, 80);
            string txtValue = driver.FindElement(txtEUComment).Text.Replace("\r\n", " ");
            return txtValue;
        }

        //Get EU Fees
        public string GetEUFees()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tblEUFee, 80);
            string txtFees = driver.FindElement(tblEUFee).Text.Replace("\r\n", " ");
            return txtFees;
        }

        //Validate Review section 
        public string ValidateReviewSection()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lblReview);
                string txtReview = driver.FindElement(lblReview).Text;
                return txtReview;
            }
            catch(Exception e)
            {
                return "No Review section";
            }
            
        }

        //To validate NBC form is disabled
        public string ValidateIfFormIsEditable()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, txtTotalDebt, 60);
            string value = driver.FindElement(txtTotalDebt).Enabled.ToString();
            
            if (value.Equals("True"))
            {
                return "Form is editable";
            }
            else
            {
                return "Form is not editable";
            }
        }

        //To validate NBC form is disabled
        public string ValidateIfFormIsEditableForPG()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtTranOverview, 60);
            string value = driver.FindElement(txtTranOverview).Enabled.ToString();

            if (value.Equals("True"))
            {
                return "Form is editable";
            }
            else
            {
                return "Form is not editable";
            }
        }

        //To validate NBC form is disabled
        public string ValidateIfCNBCFormIsEditableForPG()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtHLCompPG, 60);
            string value = driver.FindElement(txtHLCompPG).Enabled.ToString();

            if (value.Equals("True"))
            {
                return "Form is editable";
            }
            else
            {
                return "Form is not editable";
            }
        }
        //Save the Grade value
        public void SaveGradeValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboGrade);
            driver.FindElement(comboGrade).SendKeys("A+");
            driver.FindElement(btnSave).Click();
        }

        //Fetch the value of Grade field
        public string GetGradeValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valGrade, 80);
            string value = driver.FindElement(valGrade).Text;
            return value;
        }

        //Validate Estimated Fee Field
        public string ValidateEstimatedFeeField()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEstFee);
            string value = driver.FindElement(txtEstFee).Enabled.ToString();
            return value;
        }

        //Validate Grade Field
        public string ValidateGradeField()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboGrade, 10);
                string value = driver.FindElement(comboGrade).Enabled.ToString();
                return value;
            }
            catch (Exception e)
            {
                return "No Grade field";
            }
        }

        //Validate Notes Field
        public string ValidateNotesField()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtNotes, 10);
                string value = driver.FindElement(txtNotes).Enabled.ToString();
                return value;
            }
            catch (Exception e)
            {
                return "No Notes field";
            }
        }

        //Validate Date Submitted Field
        public string ValidateDateSubmittedField()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtDateSubmitted,10);
                string value = driver.FindElement(txtDateSubmitted).Enabled.ToString();
                return value;
            }
            catch (Exception e)
            {
                return "No Date Submitted field";
            }
        }

        //Validate Reason Field
        public string ValidateReasonField()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtReason, 10);
                string value = driver.FindElement(txtReason).Enabled.ToString();
                return value;
            }
            catch (Exception e)
            {
                return "No Reason field";
            }
        }

        //Validate Fee Differences Field
        public string ValidateFeeDifferencesField()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtFeeDiff,10);
                string value = driver.FindElement(txtFeeDiff).Enabled.ToString();
                return value;
            }
            catch (Exception e)
            {
                return "No Fee Differences field";
            }
        }

        //Validate Save NBC button
        public string ValidateSaveNBCButton()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 10);
                string value = driver.FindElement(btnSave).Enabled.ToString();
                return value;
            }
            catch (Exception e)
            {
                return "No Save button";
            }
        }

        //Validate Return To Opportunity button
        public string ValidateReturnToOpportunityButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturntoOpp, 10);
            string value = driver.FindElement(btnReturntoOpp).Enabled.ToString();
            return value;
        }

        //Validate Return To Opportunity button
        public string ValidateReturnToOpportunityButtonForCFUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturntoOppCFUser,10);
            string value = driver.FindElement(btnReturntoOppCFUser).Enabled.ToString();
            return value;
        }
        //Validate EU Override button
        public string ValidateEUOverrideButton()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEUOverride,10);
                string value = driver.FindElement(btnEUOverride).Enabled.ToString();
                return value;
            }
            catch (Exception e)
            {
                return "No EU Override button";
            }
        }

        //Validate PDF View button
        public string ValidatePDFViewButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnPDFView,10);
            string value = driver.FindElement(btnPDFView).Enabled.ToString();
            return value;
        }

        //Validate Attach File button
        public string ValidateAttachFileButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAttachFile);
            string value = driver.FindElement(btnAttachFile).Enabled.ToString();
            return value;
        }

        //Get Retainer Fee 
        public string GetRetainer()
        {
            Thread.Sleep(2000);
            string retainer = driver.FindElement(valRetainer).Text;
            return retainer;
        }

        //Get Progress Fee
        public string GetProgressFee()
        {
            string progressFee = driver.FindElement(valProgressFee).Text;
            return progressFee;
        }

        //Validate Add Financials button
        public string ValidateAddFinancialsButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddFinancials);
            string value = driver.FindElement(btnAddFinancials).Enabled.ToString();
            return value;
        }

        //Update Retainer in Fees tab
        public string UpdateRetainerAndValidate()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,250)");
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditRetainer);
            driver.FindElement(lnkEditRetainer).Click();
            driver.FindElement(txtRetainer).Clear();
            driver.FindElement(txtRetainer).SendKeys("1000000");
            Thread.Sleep(3000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);
            string value = driver.FindElement(valRetainer).Text;
            string actual = value.Substring(0,16);
            return actual;
        }

        //Get Estimated Total Fee (MM)
        public string GetEstimatedTotalFee()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,650)");
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, valEstValuation, 150);
            string value = (driver.FindElement(valEstValuation).Text).Substring(0,8);
            return value;
        }

        //Get Estimated Total Fee (MM)
        public string GetEstimatedTotalFeeForIncentive()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,650)");
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, valEstTotalFee,150);
            string value = (driver.FindElement(valEstTotalFee).Text).Substring(4,5);
            return value;
        }

        //Get Estimated Total Fee (MM)
        public string GetEstimatedTotalFeeForIncentiveWithRatchets()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,650)");
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, valEstTotalFee, 150);
            string value = (driver.FindElement(valEstTotalFee).Text).Substring(4, 5);
            return value;
        }

        //Get value of Estimated Valuation
        public string GetEstimatedValuation()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, valEstValuation);
            string value = driver.FindElement(valEstValuation).Text;
            string actual = value.Substring(0, 8);
            return actual;
        }

        //Update Estimated Valuation
        public string UpdateEstimatedValuation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditCurrency);
            driver.FindElement(lnkEditCurrency).Click();
            driver.FindElement(txtEstValuation).Clear();
           driver.FindElement(txtEstValuation).SendKeys("20");
            Thread.Sleep(3000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);
            string value = driver.FindElement(valEstValuation).Text;           
            string actual = value.Substring(0, 8);
            return actual;
        }
    
        //Get CC Status
        public string GetCCStatusMessage()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgCCStatus);
            string value = driver.FindElement(msgCCStatus).Text;            
            return value;
        }

        //Get CC Valiidation 1
        public string GetCC1stValidation()
        {
            Thread.Sleep(4000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,250)");
            WebDriverWaits.WaitUntilEleVisible(driver, msgCC1,160);
            string message = driver.FindElement(msgCC1).Text;
            return message;
        }

        //Get CC Valiidation 2
        public string GetCC2ndValidation()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,250)");
            WebDriverWaits.WaitUntilEleVisible(driver, msgCC2);
            string message = driver.FindElement(msgCC2).Text;
            return message;
        }

        //Get CC Valiidation 3
        public string GetCC3rdValidation()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,250)");
            WebDriverWaits.WaitUntilEleVisible(driver, msgCC3);
            string message = driver.FindElement(msgCC3).Text;
            return message;
        }

        //Get CC Valiidation 4
        public string GetCC4thValidation()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,250)");
            WebDriverWaits.WaitUntilEleVisible(driver, msgCC4);
            string message = driver.FindElement(msgCC4).Text;
            return message;
        }

        public void NavigateToPreviousWindow()
        {
            Thread.Sleep(6000);
            driver.SwitchTo().Window(driver.WindowHandles[0]);
        }

        public void NavigateToNextWindow()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().Window(driver.WindowHandles[1]);
        }

        //Clear Progress Fee related fields and others except Retainer ones
        public string UpdateReviewSubAndProgressFee(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(4000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,150)");
            driver.FindElement(btnUpdSubmit).Click();            
            js.ExecuteScript("window.scrollTo(0,450)");
            WebDriverWaits.WaitUntilEleVisible(driver, txtRetainer, 150);
            Thread.Sleep(5000);
            driver.FindElement(txtRetainer).Clear();
            string fee = ReadExcelData.ReadData(excelPath, "NBCForm", 64);
            Console.WriteLine(fee);
            string feeCred = ReadExcelData.ReadData(excelPath, "NBCForm", 65);
            driver.FindElement(txtRetainer).SendKeys(fee);
            driver.FindElement(txtRetainerFeeCred).Clear();
            driver.FindElement(txtRetainerFeeCred).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 65));
            WebDriverWaits.WaitUntilEleVisible(driver, txtProgressFeeMM, 150);
            driver.FindElement(txtProgressFeeMM).Clear();
            driver.FindElement(txtProgressFee).Clear();
            js.ExecuteScript("window.scrollTo(0,750)");
            driver.FindElement(txtMinFeeMM).Clear();
            driver.FindElement(txtEstTxnValueMM).Clear();
            js.ExecuteScript("window.scrollTo(0,750)");
            driver.FindElement(txtReferralFeeOwnedMM).Clear();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            double estTotalFee = (Convert.ToDouble((driver.FindElement(valEstTotalFeeMM).Text).Substring(4, 4)));
            Console.WriteLine("estTotalFee: " + estTotalFee.ToString("0.00"));
            return estTotalFee.ToString("0.00");
        }


        //Get CC Valiidation 5
        public string GetCC5thValidation()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,250)");
            WebDriverWaits.WaitUntilEleVisible(driver, msgCC5);
            string message = driver.FindElement(msgCC5).Text;
            return message;
        }

        //Clear Retainer Fee related fields 
        public string UpdateRetainerAndProgressFee(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(6000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,270)");
            Thread.Sleep(6000);
            driver.FindElement(lnkEditRetainer).Click();
            Thread.Sleep(6000);
            js.ExecuteScript("window.scrollTo(0,400)");
            WebDriverWaits.WaitUntilEleVisible(driver, txtRetainer, 150);
            Thread.Sleep(5000);
            driver.FindElement(txtRetainer).Clear();
            string fee = ReadExcelData.ReadData(excelPath, "NBCForm", 64);
            Console.WriteLine(fee);
            string feeCred = ReadExcelData.ReadData(excelPath, "NBCForm", 65);
            driver.FindElement(txtProgressFeeMM).SendKeys(fee);
            driver.FindElement(txtRetainerFeeCred).Clear();
            driver.FindElement(txtProgressFeeCred).Clear();
            driver.FindElement(txtProgressFeeCred).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 65));
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            double estTotalFee = (Convert.ToDouble((driver.FindElement(valEstTotalFeeMM).Text).Substring(4, 4)));
            Console.WriteLine("estTotalFee: " + estTotalFee.ToString("0.00"));
            return estTotalFee.ToString("0.00");
        }

        //Enter both Retainer and Progree Fee related fields 
        public string UpdateBothRetainerAndProgressFee(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(6000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,270)");
            Thread.Sleep(7000);
            driver.FindElement(lnkEditProgressFeeCred).Click();
            Thread.Sleep(4000);
            js.ExecuteScript("window.scrollTo(0,400)");
            WebDriverWaits.WaitUntilEleVisible(driver, txtRetainer, 150);
            Thread.Sleep(5000);
            string fee = ReadExcelData.ReadData(excelPath, "NBCForm", 64);
            Console.WriteLine(fee);
            driver.FindElement(txtRetainer).SendKeys(fee);
            string feeCred = ReadExcelData.ReadData(excelPath, "NBCForm", 65);
            driver.FindElement(txtRetainerFeeCred).SendKeys(feeCred);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);
            double estTotalFee = (Convert.ToDouble((driver.FindElement(valEstTotalFeeMM).Text).Substring(4, 4)));
            Console.WriteLine("estTotalFee2: " + estTotalFee.ToString("0.00"));
            return estTotalFee.ToString("0.00");
        }

        //Fetch Flat Fee 
        public string GetFlatFee()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, valFlatFee, 250);
            string fee = driver.FindElement(valFlatFee).Text;
            return fee;
        }

        //Adding Flat Fee
        public string SaveFlatFee(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,300)");
            Thread.Sleep(7000);
            string fee = ReadExcelData.ReadData(excelPath, "NBCForm", 64);
            Console.WriteLine(fee);
            driver.FindElement(lnkEditCurrency).Click();
            Thread.Sleep(4000);
            js.ExecuteScript("window.scrollTo(0,230)");
            driver.FindElement(txtFlatFee).SendKeys(fee);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            string flatFee = (Convert.ToDouble((driver.FindElement(valFlatFee).Text).Substring(4, 6))).ToString("0.0");
            //Console.WriteLine("flatFee: " + flatFee.ToString("0.00"));            
            return flatFee;
        }

        //Fetch Estimated Total Fee 
        public string GetEstTotalFee()
        {
            double estTotalFee = (Convert.ToDouble((driver.FindElement(valEstTotalFeeMM).Text).Substring(4, 5)));
            Console.WriteLine("estTotalFee2: " + estTotalFee.ToString("0.00"));
            return estTotalFee.ToString("0.00");
        }

        //Update Minimum Fee
        public string UpdateMinFee()
        {
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditCurrency, 180);
            driver.FindElement(lnkEditCurrency).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtMinFeeMM, 100);
            driver.FindElement(txtMinFeeMM).SendKeys("230");
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 180);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(7000);
            double MinFee = (Convert.ToDouble((driver.FindElement(valMinFee).Text).Substring(4, 6)));
            Console.WriteLine("MinFee: " + MinFee.ToString("0.00"));
            return MinFee.ToString("0.00");
        }

        //Update Minimum Fee which is less than Estimated Total Fee
        public string UpdateMinFeeLessThanEstTotalFee()
        {
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditCurrency, 180);
            driver.FindElement(lnkEditCurrency).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtMinFeeMM, 100);
            driver.FindElement(txtMinFeeMM).Clear();
            Thread.Sleep(3000);
            driver.FindElement(txtMinFeeMM).SendKeys("100");
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 180);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(7000);
            double MinFee = (Convert.ToDouble((driver.FindElement(valMinFee).Text).Substring(4, 3)));
            Console.WriteLine("MinFee: " + MinFee.ToString("0.00"));
            return MinFee.ToString("0.00");
        }

        //Update Estimated Transaction Value
        public string UpdateEstTransValue()
        {
            Thread.Sleep(3000);
            driver.FindElement(lnkEditCurrency).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtEstTxnVal).Clear();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(valEstTxnVal).Text;
            return value;
        }

        //Get Estimated Transaction Value
        public string GetValidationOfEstTransValue()
        {
            Thread.Sleep(4000);          
            string message = driver.FindElement(msgEstTransValue).Text;            
            return message;
        }
        //Update Review Submission and Base Fee
        public string UpdateReviewSubmissionAndBaseFee()
        {
            Thread.Sleep(4000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            //js.ExecuteScript("window.scrollTo(0,100)");            
            driver.FindElement(btnUpdSubmit).Click();           
            js.ExecuteScript("window.scrollTo(0,300)");
            driver.FindElement(txtMinFeeMM).Clear();
            driver.FindElement(txtBaseFee).SendKeys("10");
            driver.FindElement(btnSave).Click();
            Thread.Sleep(7000);
            string value = driver.FindElement(valBaseFee).Text;
            return value;           
        }
        //Update 1st Ratchet pecent, 2nd Ratchet pecent, 3rd Ratchet pecentand 4th Ratchet pecent fields
        public string UpdateAllRatchetPercent()
        {
            driver.FindElement(lnkUpdBaseFee).Click();
            Thread.Sleep(4000);
            driver.FindElement(txt1stRatchetPer).SendKeys("10");
            driver.FindElement(txt2ndRatchetPer).SendKeys("10");
            driver.FindElement(txt3rdRatchetPer).SendKeys("10");
            driver.FindElement(txt4thRatchetPer).SendKeys("10");
            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnClose).Click();
            string message = driver.FindElement(msg1stRatchetFromAmount).Text;
            return message;
        }
        //Get 1st Ratchet To Amount field validation
        public string Get1stRatchetToAmount()
        {
            string message = driver.FindElement(msg1stRatchetToAmount).Text;
            return message;
        }

        //Get 2nd Ratchet From Amount field validation
        public string Get2ndRatchetFromAmount()
        {
            string message = driver.FindElement(msg2ndRatchetFromAmount).Text;
            return message;
        }

        //Get 2nd Ratchet To Amount field validation
        public string Get2ndRatchetToAmount()
        {
            string message = driver.FindElement(msg2ndRatchetToAmount).Text;
            return message;
        }

        //Get 3rd Ratchet From Amount field validation
        public string Get3rdRatchetFromAmount()
        {
            string message = driver.FindElement(msg3rdRatchetFromAmount).Text;
            return message;
        }

        //Get 3rd Ratchet To Amount field validation
        public string Get3rdRatchetToAmount()
        {
            string message = driver.FindElement(msg3rdRatchetToAmount).Text;
            return message;
        }

        //Get 4th Ratchet From Amount field validation
        public string Get4thRatchetFromAmount()
        {
            string message = driver.FindElement(msg4thRatchetFromAmount).Text;
            return message;
        }

        //Get 4th Ratchet To Amount field validation
        public string Get4thRatchetToAmount()
        {
            string message = driver.FindElement(msg4thRatchetToAmount).Text;
            return message;
        }

        //Update all Ratchets From and To amount fields
        public string UpdateAllRatchetFromAndToAmt()
        {
            Thread.Sleep(4000);
            driver.FindElement(txt1stRatchetFromAmount).SendKeys("100");
            driver.FindElement(txt1stRatchetToAmount).SendKeys("50");
            driver.FindElement(txt2ndRatchetFromAmount).SendKeys("100");
            driver.FindElement(txt2ndRatchetToAmount).SendKeys("50");
            driver.FindElement(txt3rdRatchetFromAmount).SendKeys("100");
            driver.FindElement(txt3rdRatchetToAmount).SendKeys("50");
            driver.FindElement(txt4thRatchetFromAmount).SendKeys("100");
            driver.FindElement(txt4thRatchetToAmount).SendKeys("50");
            driver.FindElement(btnSave).Click();
            Thread.Sleep(6000);
            driver.FindElement(btnClose).Click();
            string message = driver.FindElement(msg1stRatchetGreaterToAmount).Text;
            return message;
        }

        //Get 2nd Ratchet To Amount validation for greater field
        public string Get2ndRatchetToAmountGreaterValue()
        {
            string message = driver.FindElement(msg2ndRatchetGreaterToAmount).Text;
            return message;
        }

        //Get 3rd Ratchet To Amount validation for greater field
        public string Get3rdRatchetToAmountGreaterValue()
        {
            string message = driver.FindElement(msg3rdRatchetGreaterToAmount).Text;
            return message;
        }

        //Get 2nd Ratchet To Amount validation for greater field
        public string Get4thRatchetToAmountGreaterValue()
        {
            string message = driver.FindElement(msg4thRatchetGreaterToAmount).Text;
            return message;
        }

        //Update 1st Ratchet From and To amount fields
        public void Update1stRatchetFromAndToAmt(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string fromAmt = ReadExcelData.ReadData(excelPath, "NBCForm", 66);
            string ToAmt = ReadExcelData.ReadData(excelPath, "NBCForm", 67);
            Thread.Sleep(4000);
            driver.FindElement(txtEstTxnVal).SendKeys(ToAmt);
            driver.FindElement(txt1stRatchetPer).Clear();
            driver.FindElement(txt1stRatchetFromAmount).Clear();
            driver.FindElement(txt1stRatchetToAmount).Clear();
            driver.FindElement(txt1stRatchetPer).SendKeys(fromAmt);
            driver.FindElement(txt1stRatchetFromAmount).SendKeys(fromAmt);
            driver.FindElement(txt1stRatchetToAmount).SendKeys(ToAmt);
            driver.FindElement(txt2ndRatchetPer).Clear();
            driver.FindElement(txt2ndRatchetFromAmount).Clear();
            driver.FindElement(txt2ndRatchetToAmount).Clear();

            driver.FindElement(txt3rdRatchetPer).Clear();
            driver.FindElement(txt3rdRatchetFromAmount).Clear();
            driver.FindElement(txt3rdRatchetToAmount).Clear();

            driver.FindElement(txt4thRatchetPer).Clear();
            driver.FindElement(txt4thRatchetFromAmount).Clear();
            driver.FindElement(txt4thRatchetToAmount).Clear();

            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
        }

        //Update Estimated Transaction Value (MM)
        public string UpdateEstimatedTransactionValue(string value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,-50)");
            Thread.Sleep(4000);
            driver.FindElement(lnkUpd1stRatchet).Click();                     
            //js.ExecuteScript("window.scrollTo(0,-100)");          
            Thread.Sleep(3000);
            driver.FindElement(txtEstTxnVal).Clear();
            driver.FindElement(txtEstTxnVal).SendKeys(value);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(7000);
            string estValue = driver.FindElement(valEstTxnVal).Text;
            return estValue.Substring(4,5);
        }

        //Update Estimated Transaction Value (MM)
        public string UpdateMinFeeValue()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,150)");
            Thread.Sleep(4000);
            driver.FindElement(lnkEditCurrency).Click();
            //js.ExecuteScript("window.scrollTo(0,-100)");          
            Thread.Sleep(3000);
            driver.FindElement(txtMinFee).Clear();
            driver.FindElement(txtMinFee).SendKeys("300");
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
            string estValue = driver.FindElement(valEstTotalFeeMM).Text.Replace(",", "");
            return estValue.Substring(4, 3);
        }

        //Update 2nd Ratchet From and To amount fields
        public void Update2ndRatchetFromAndToAmt(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string fromAmt = ReadExcelData.ReadData(excelPath, "NBCForm", 66);
            string ToAmt = ReadExcelData.ReadData(excelPath, "NBCForm", 67);
            Thread.Sleep(3000);
            driver.FindElement(lnkUpd2ndRatchet).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtMinFee).Clear();
            driver.FindElement(txtEstTxnVal).Clear();
            driver.FindElement(txtEstTxnVal).SendKeys(ToAmt);
            driver.FindElement(txt1stRatchetPer).Clear();
            driver.FindElement(txt1stRatchetToAmount).Clear();
            driver.FindElement(txt1stRatchetFromAmount).Clear();
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,150)");
            Thread.Sleep(3000);
            driver.FindElement(txt2ndRatchetPer).SendKeys(fromAmt);
            driver.FindElement(txt2ndRatchetFromAmount).SendKeys(fromAmt);
            driver.FindElement(txt2ndRatchetToAmount).SendKeys(ToAmt);          
                        
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
        }

        //Update 3rd Ratchet From and To amount fields
        public void Update3rdRatchetFromAndToAmt(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string fromAmt = ReadExcelData.ReadData(excelPath, "NBCForm", 66);
            string ToAmt = ReadExcelData.ReadData(excelPath, "NBCForm", 67);
            Thread.Sleep(3000);
            driver.FindElement(lnkUpd3rdRatchet).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtEstTxnVal).Clear();
            driver.FindElement(txtEstTxnVal).SendKeys(ToAmt);
            driver.FindElement(txt2ndRatchetPer).Clear();
            driver.FindElement(txt2ndRatchetToAmount).Clear();
            driver.FindElement(txt2ndRatchetFromAmount).Clear();
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,150)");
            Thread.Sleep(3000);
            driver.FindElement(txt3rdRatchetPer).SendKeys(fromAmt);
            driver.FindElement(txt3rdRatchetFromAmount).SendKeys(fromAmt);
            driver.FindElement(txt3rdRatchetToAmount).SendKeys(ToAmt);

            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
        }

        //Update 4th Ratchet From and To amount fields
        public void Update4thRatchetFromAndToAmt(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string fromAmt = ReadExcelData.ReadData(excelPath, "NBCForm", 66);
            string ToAmt = ReadExcelData.ReadData(excelPath, "NBCForm", 67);
            Thread.Sleep(3000);
            driver.FindElement(lnkUpd4thRatchet).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtEstTxnVal).Clear();
            driver.FindElement(txtEstTxnVal).SendKeys(ToAmt);
            driver.FindElement(txt3rdRatchetPer).Clear();
            driver.FindElement(txt3rdRatchetToAmount).Clear();
            driver.FindElement(txt3rdRatchetFromAmount).Clear();
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,150)");
            Thread.Sleep(3000);
            driver.FindElement(txt4thRatchetPer).SendKeys(fromAmt);
            driver.FindElement(txt4thRatchetFromAmount).SendKeys(fromAmt);
            driver.FindElement(txt4thRatchetToAmount).SendKeys(ToAmt);

            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
        }

        //Update Final Ratchet amount 
        public string UpdateFinalRatchetAmt(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string fromAmt = ReadExcelData.ReadData(excelPath, "NBCForm", 66);
            string ToAmt = ReadExcelData.ReadData(excelPath, "NBCForm", 67);
            Thread.Sleep(3000);
            driver.FindElement(lnkUpdFinalRatchet).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtEstTxnVal).Clear();
            driver.FindElement(txtEstTxnVal).SendKeys(ToAmt);
            driver.FindElement(txt4thRatchetPer).Clear();
            driver.FindElement(txt4thRatchetToAmount).Clear();
            driver.FindElement(txt4thRatchetFromAmount).Clear();
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,150)");
            Thread.Sleep(3000);
            driver.FindElement(txtFinalRatchetPer).SendKeys(fromAmt);
            driver.FindElement(txtFinalRatchetAmount).SendKeys(fromAmt);
            
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(valEstTxnVal).Text;
            return value.Substring(4, 5);
        }

        //Update all Ratchet values
        public string UpdateAllRatchetValues(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string fromAmt = ReadExcelData.ReadData(excelPath, "NBCForm", 66);
            string ToAmt = ReadExcelData.ReadData(excelPath, "NBCForm", 67);
            Thread.Sleep(3000);
            driver.FindElement(lnkUpdBaseFee).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtEstTxnVal).Clear();
            driver.FindElement(txtEstTxnVal).SendKeys("100");
            driver.FindElement(txt1stRatchetPer).SendKeys(fromAmt);
            driver.FindElement(txt1stRatchetFromAmount).SendKeys(fromAmt);
            driver.FindElement(txt1stRatchetToAmount).SendKeys(ToAmt);
            driver.FindElement(txt2ndRatchetPer).SendKeys(fromAmt);
            driver.FindElement(txt2ndRatchetFromAmount).SendKeys(fromAmt);
            driver.FindElement(txt2ndRatchetToAmount).SendKeys(ToAmt);
            driver.FindElement(txt3rdRatchetPer).SendKeys(fromAmt);
            driver.FindElement(txt3rdRatchetFromAmount).SendKeys(fromAmt);
            driver.FindElement(txt3rdRatchetToAmount).SendKeys(ToAmt);
            driver.FindElement(txt4thRatchetPer).SendKeys(fromAmt);
            driver.FindElement(txt4thRatchetFromAmount).SendKeys(fromAmt);
            driver.FindElement(txt4thRatchetToAmount).SendKeys(ToAmt);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(valEstTotalFee).Text;
            return value.Substring(4,6);
        }

        //Update Other Fee Structure
        public string UpdateOtherFeeStructure()
        {
            driver.FindElement(txtOtherFee).SendKeys("10");
            Thread.Sleep(3000);
            driver.FindElement(txtEstimatedFee).SendKeys("10");
            driver.FindElement(txtReferralFeeOwnedMM).SendKeys("10");
            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnClose).Click();
            Thread.Sleep(3000);
            try
            {
                string message = driver.FindElement(msgOtherFee).Text.ToString();
                if (message.Equals("Complete this field."))
                {
                    return "Validation appears";
                }
                else
                {
                    return "Validation did not appear";
                }
            }
            catch(Exception e)
            {
                return "Validation did not appear";
            }
        }

        //Update Fee Creditabe to 0 and 100
        public string UpdateFeeCreditables(string value)
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditCurrency, 180);
            driver.FindElement(lnkEditCurrency).Click();
            driver.FindElement(txtRetainerFeeCred).Clear();
            driver.FindElement(txtRetainerFeeCred).SendKeys(value);
            driver.FindElement(txtProgressFeeCred).Clear();
            driver.FindElement(txtProgressFeeCred).SendKeys(value);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 180);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            string fee = driver.FindElement(valEstTotalFeeMM).Text;
            return fee.Substring(4, 6).Replace(",", "");
        }

       
    }


}


