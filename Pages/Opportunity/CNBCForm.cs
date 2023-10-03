using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Linq;
using System.Threading;

namespace SF_Automation.Pages.Opportunity
{
    class CNBCForm : BaseClass
    {
        By valOppName = By.XPath("//div[2]/h1/slot/records-formula-output/slot/lightning-formatted-text");
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

        By msgStructure = By.XPath("//label[text()='Structure and Pricing Expectations']/following::div[2]");
        By msgSanctions = By.XPath("//div[text()='Overview and Financials: Sanctions concerns/issues?']");
        By msgUseOfProceeds = By.XPath("//lightning-picklist/lightning-dual-listbox/div/span");
        By msgCapitalRaise = By.XPath("//label[text()='Capital Raise (MM)']/following::div[2]");
        By msgEstimatedFee = By.XPath("//label[text()='Estimated Fee (MM)']/following::div[2]");
        By msgReferralFee = By.XPath("//label[text()='Referral Fee Owed (MM)']/following::div[2]");

        By tabAdmin = By.XPath("//*[@id='flexipage_tab5__item']");
        By tabReview = By.XPath("//lightning-tab-bar/ul/li[@title='Review']");

        By msgRestrictedList = By.XPath("//div[text()='Administrative: Restricted List?']");

        By txtTxnOverview = By.XPath("//lightning-textarea/label[text()='Transaction Overview']/following::textarea[1]");
        By txtTotalDebt = By.XPath("//label[text()='Total Debt (MM)']/following::input[1]");
        By txtEstValuation = By.XPath("//label[text()='Estimated Valuation (MM)']/following::input[1]");
        By btnCurrentStatus = By.XPath("//label[text()='Current Status']/following::button[2]");
        By comboCurrentStatus = By.XPath("//label[text()='Current Status']/following::lightning-base-combobox-item/span[2]/span[text()='Pitched']");
        By txtValuationExp = By.XPath("//label[text()='Valuation Expectations']/following::textarea");
        By txtCompanyDesc = By.XPath("//label[text()='Company Description']/following::textarea[1]");
        By comboRealEstate = By.XPath("//label[text()='Real Estate Angle']/following::button[1]");
        By txtOwnership = By.XPath("//label[text()='Ownership and Capital Structure']/following::textarea[1]");
        By comboAsia = By.XPath("//label[text()='Asia Angle']/following::button[1]");
        By txtRisk = By.XPath("//label[text()='Risk Factors']/following::textarea[1]");
        By comboSanctions = By.XPath("(//lightning-base-combobox)[8]");
        By btnExistingRel = By.XPath("//label[text()='Existing Relationships']/following::button[1]");
        By btnExistingClient = By.XPath("//label[text()='Existing or Repeat Client?']/following::button[1]");
        By comboInternational = By.XPath("//label[text()='International Angle?']/following::button[1]");
        By txtHLComp = By.XPath("//label[text()='Houlihan Lokey Competition']/following::textarea[1]");
        By txtStructure = By.XPath("//label[text()='Structure and Pricing Expectations']/following::textarea[1]");
        By chkGroupHead = By.XPath("//span[text()='Group Head Approval']/following::input[1]");
        By valProceeds = By.XPath("//li[2]/div/span/span");
        By btnChosen = By.XPath("//div[4]/lightning-button-icon[1]/button");
        By btnPrePitch = By.XPath("//label[text()='Will There Be a Pitch?']/following::button[1]");

        By btnFinSubject = By.XPath("(//lightning-base-combobox)[10]");
        By txtCapitalRaise = By.XPath("//label[text()='Capital Raise (MM)']/following::input[1]");

        By txtRetainerFeeCred = By.XPath("//input[@name='Retainer_Creditable__c']");
        By txtProgressFee = By.XPath("//input[@name='Is_Progress_Fee_Creditable__c']");
        By txtMinFee = By.XPath("//input[@name='Estimated_Minimum_Fee__c']");
        By txtEstimatedFee = By.XPath("//input[@name='Total_Otherfee__c']");
        By btnLockUp = By.XPath("(//lightning-base-combobox)[11]");
        By txtReferralFee = By.XPath("//label[text()='Referral Fee Owed (MM)']/following::div[1]/input");

        By btnRestricted = By.XPath("(//lightning-base-combobox)[13]");

        By chkNextSchCall = By.XPath("(//span[@class='slds-checkbox slds-checkbox_standalone']/input)[3]");
        By btnSubmitForReview = By.XPath("//lightning-button/button[text()='Submit for Review']");
        By lnkEditGrade = By.XPath("//span[text()='Grade']/following::div[1]/button/span[1]");
        By btnGrade = By.XPath("//label[text()='Grade']/following::lightning-base-combobox[1]/div/div[1]/div/button");
        By valGrade = By.XPath("//span[text()='Grade']/following::div[1]/span/slot/lightning-formatted-text[1]");

        By btnClose = By.XPath("//records-record-edit-error-header/lightning-button-icon/button/lightning-primitive-icon");
        By btnSave = By.XPath("//li[2]/runtime_platform_actions-action-renderer/runtime_platform_actions-executor-lwc-headless/slot[1]/slot/lightning-button/button");

        By lblCurrentStatus = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordTransaction_Overview_cField2']/slot[1]/following::span[1]");
        By lblRiskFact = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordCompany_Description_cField2']/slot[1]/following::span[1]");
        By lblExistingRel = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordExisting_or_Repeat_Client_cField1']/slot[1]/following::span[1]");
        By lblExisting = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordRisk_Factors_cField1']/slot[1]/following::span[1]");
        By lblAsiaAngle = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordExisting_Relationships_cField1']/slot[1]/following::span[1]");
        By lblRealEstate = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[@data-field-id='RecordAsia_Angle_cField1']/slot[1]/following::span[1]");
        By lblIntAngle = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[@data-field-id='RecordReal_Estate_Angle_cField1']/slot[1]/following::span[1]");
        By secHLComp = By.XPath("//span[@title='Houlihan Lokey Competition']");
        By lblHLComp = By.XPath("//flexipage-tab2[1]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secDescribeOther = By.XPath("//div[1]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/slot/slot/flexipage-tab2[1]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/h3/button/span");
        By lblUseOfPro = By.XPath("//flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblProDetails = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordUse_of_Proceeds_cField1']/slot[1]/following::span[1]");
        By secOwnership = By.XPath("//slot/flexipage-tab2[1]/slot/flexipage-component2[4]/slot/flexipage-field-section2/div/div/div/h3/button/span");
        By lblOwnership = By.XPath("//flexipage-component2[4]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secTotalDebt = By.XPath("//span[@title='Total Debt(MM)']");
        By lblTotalDebt = By.XPath("//flexipage-component2[5]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secPrePitch = By.XPath("//flexipage-tab2[1]/slot/flexipage-component2[6]/slot/flexipage-field-section2/div/div/div/h3/button/span");
        By lblPrePitch = By.XPath("//flexipage-tab2[1]/slot/flexipage-component2[6]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[1]/span[1]");

        By secStrcuture = By.XPath("//span[@title='Structure and Pricing Expectations (if relevant)']");
        By lblStrcuture = By.XPath("//flexipage-component2[7]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secReferral = By.XPath("//span[@title='Referral']");
        By lblReferralType = By.XPath("//flexipage-component2[8]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblReferralSource = By.XPath("//flexipage-component2[8]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field[2]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secPls = By.XPath("//span[@title='Please confirm that a group head has approved prior to submitting to the committee']");
        By lblGroup = By.XPath("//flexipage-component2[9]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[1]/span[1]");

        By msgFinUnavailable = By.XPath("//span[@title='Financials Unavailable']");
        By lblNoFin = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Opportunity_Approval__c.No_Financials__c']/div[1]/div[1]/span");
        By lblNoFinExp = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Opportunity_Approval__c.No_Financials_Explanation__c']/div[1]/div[1]/span");
        By msgAboveFin = By.XPath("//span[text()='Have the above Financials been subject to an audit?']");
        By lblFinSub = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Opportunity_Approval__c.Financials_Subject_to_Audit__c']/div[1]/div[1]/span");
        By msgCapital = By.XPath("//span[@title='Capital Raise (MM) (Best estimate of capital to be raised expressed as a currency value in millions)']");
        By lblCapRaise = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Opportunity_Approval__c.Capital_Raise__c']/div[1]/div[1]/span");

        By lblRetainer = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By lblFeeStructure = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordTotal_Otherfee_cField2']/slot[1]/following::span[1]");
        By lblLockups = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordFee_Structure_cField1']/slot[1]/following::span[1]");
        By lblReferral = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordLockups_on_Future_M_A_or_Financing_Work_cField2']/slot[1]/following::span[1]");
        By lblRetainerFee = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[@data-field-id='RecordReferral_Fee_cField2']/slot[1]/following::span[1]");
        By lblProgress = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[@data-field-id='RecordRetainer_Creditable_cField1']/slot[1]/following::span[1]");

        By msgAdmin = By.XPath("//span[@title='Administrative']");
        By lblRestrictedList = By.XPath("//flexipage-tab2[5]/slot/flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By msgCCInfo = By.XPath("//span[@title='Conflicts Check Information']");
        By lblCCStatus = By.XPath("//flexipage-tab2[5]/slot/flexipage-component2[2]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[1]/span[1]");
        By secToSubmit = By.XPath("//h3/button/span[text()='To Submit An CNBC Form:']");
        By lnkAttachments = By.XPath("//a[@data-label='Attachment']");

        By valOppNum = By.XPath("//body[@id='j_id0:j_id29:pbSendEmail:j_id47:Body_rta_body']/p[9]/span");
        By btnSendEmail = By.XPath("//div[1]/table/tbody/tr/td[2]/input[1]");

        By clientComp = By.CssSelector("span[id*='id45']");
        By subjectComp = By.CssSelector("span[id*='id48']");
        By jobType = By.CssSelector("span[id*='id61']");
        By btnSubmit = By.CssSelector("input[id*='j_id33:btnSubmitForReview']");
        By errorList = By.CssSelector("#j_id0\\:CNBCForm\\:j_id2\\:j_id3\\:j_id4\\:0\\:j_id5\\:j_id6\\:j_id18 > ul");
        By btnCancel = By.CssSelector("input[value='Cancel Submission']");
        By checkToggleTabs = By.Id("toggleTabs");
        By tabList = By.Id("tabsList");
        By checkConfirm = By.CssSelector("input[id*='HeadApproval']");
        By txtTranOverview = By.CssSelector("textarea[name*='id98']");
        By txtCurrentStatus = By.CssSelector("textarea[name*='id100']");
        By txtCompDesc = By.CssSelector("textarea[name*='id103']");
        By comboCrossBorder = By.CssSelector("select[name*='InternationalAngle']");
        By comboAsiaAngle = By.CssSelector("select[name*='AsiaAngle']");
        //By comboRealEstate = By.CssSelector("select[name*='RealEstateAngle']");
        By txtOwnershipStr = By.CssSelector("textarea[name*='id124']");
        //By txtTotalDebt = By.CssSelector("input[name*='TotalDebt']");
        By comboAudit = By.CssSelector("select[name*='FinAudit01']");
        By txtCapRaise = By.CssSelector("input[name*='capRaiseReq']");
        By txtStrExp = By.CssSelector("textarea[name *= 'StructPriceTXT00']");
        By txtRiskFactors = By.CssSelector("textarea[name*='id168']");
        By txtEstFee = By.CssSelector("input[name*='estMinFee']");
        By txtFeeStr = By.CssSelector("textarea[name*= 'id179']");
        By comboLockUps = By.CssSelector("select[name*= 'id183']");
        By comboReferralFee = By.CssSelector("select[name*= 'id188']");
        By comboClient = By.CssSelector("select[id='j_id0:CNBCForm:j_id31:j_id64:Exist']");
        //By txtHLComp = By.CssSelector("textarea[name*= 'id75']");
        By comboExistingRel = By.CssSelector("select[name*= 'ExistingRel']");
        By comboResList = By.CssSelector("select[name*= 'RestrictedList']");
        By titleEmailPage = By.XPath("//div[@class='pbHeader']/table/tbody/tr/td/h2[@class='mainTitle']");
        By valEmailOppName = By.CssSelector("body[id*='Body_rta_body'] > span:nth-child(10) > span");
        By btnCancelEmail = By.CssSelector("input[value='Cancel']");
        By btnReturntoOpp = By.CssSelector("input[value*='Return to Opportunity']");
        By btnReturntoOppCFUser = By.CssSelector("span[id*=':j_id34'] > a");
        By txtAvailProceeds = By.CssSelector("select[id*='UseofProceeds_unselected']> optgroup > option[value='0']");
        By imgArrow = By.CssSelector("img[id*='UseofProceeds_right_arrow']");
        By lblReview = By.CssSelector("label[for*='reviewGrade']");
        By lblReviewAdmin = By.CssSelector("div[id*='210']>div>h3");
        By comboGrade = By.CssSelector("select[id*='reviewGrade']");
        //By btnSave = By.CssSelector("input[name*='id33:j_id37']");
        //By valGrade = By.CssSelector("select[id*='reviewGrade']>option[selected='selected']");
        By txtNotes = By.CssSelector("textarea[id*='reviewNotes']");
        By txtDateSubmitted = By.CssSelector("input[id*='dateSubmit']");
        By txtReason = By.CssSelector("textarea[id*='reasonWonLost']");
        By txtFeeDiff = By.CssSelector("textarea[id*='feeDiff']");
        By btnEUOverride = By.CssSelector("span[id*='1:j_id33'] > input[id*='euOverride']");
        By btnPDFView = By.XPath("//a[contains(text(),'PDF View')]");
        By btnAttachFile = By.CssSelector("button[type='button']");
        By btnAddFinancials = By.CssSelector("input[id*='newFinancials']");

        //Validate Opp Name
        public string ValidateOppName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valOppName, 80);
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
            WebDriverWaits.WaitUntilEleVisible(driver, valsubjectComp);
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

        //Get Validation of Overview and Financials: Structure and Pricing Expectations.
        public string GetValidationOfStructureAndPricing()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgStructure, 80);
            string value = driver.FindElement(msgStructure).Text;
            return value;
        }

        //Get Validation of Opportunity Overview: Sanctions concerns/issues?
        public string GetValidationOfSanctionsConcerns()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgSanctions, 80);
            string value = driver.FindElement(msgSanctions).Text;
            return value;
        }

        //Get Validation of Overview and Financials: Use Of Proceeds
        public string GetValidationOfUseOfProceeds()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgUseOfProceeds, 80);
            string value = driver.FindElement(msgUseOfProceeds).Text;
            return value;
        }

        //Get Validation of Overview and Financials: Capital Raise (MM)
        public string GetValidationOfCapitalRaise()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgCapitalRaise, 80);
            string value = driver.FindElement(msgCapitalRaise).Text;
            return value;
        }

        //Get Validation of Overview and Financials: Estimated Fee (MM)
        public string GetValidationOfEstimatedFee()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgEstimatedFee, 80);
            string value = driver.FindElement(msgEstimatedFee).Text;
            return value;
        }

        //Get Validation of Overview and Financials: Referral Fee Owned (MM)
        public string GetValidationOfReferralFee()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgReferralFee, 80);
            string value = driver.FindElement(msgReferralFee).Text;
            return value;
        }

        //Click Administrative tab
        public string ClickAdministrativeTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabAdmin, 150);
            driver.FindElement(tabAdmin).Click();
            string valTab = driver.FindElement(tabAdmin).Text;
            return valTab;
        }

        //Click Review tab
        public string ClickReviewTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabReview, 150);
            driver.FindElement(tabReview).Click();
            string valTab = driver.FindElement(tabReview).Text;
            return valTab;
        }

        //Get Validation of Restricted List
        public string GetValidationOfRestrictedList()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgRestrictedList, 80);
            string value = driver.FindElement(msgRestrictedList).Text;
            return value;
        }

        //Save all required details in Opportunity Overview tab
        public void SaveAllReqFieldsInOppOverview(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(txtTxnOverview).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 3));
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,150)");
            Thread.Sleep(5000);
            driver.FindElement(btnCurrentStatus).Click();
            driver.FindElement(btnCurrentStatus).Click();
            string name = ReadExcelData.ReadData(excelPath, "NBCForm", 4);
            string real = ReadExcelData.ReadData(excelPath, "NBCForm", 7);
            string lockup = ReadExcelData.ReadData(excelPath, "NBCForm", 2);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//label[text()='Current Status']/following::div/lightning-base-combobox-item/span[2]/span[text()='" + name + "']")).Click();
            driver.FindElement(comboAsia).Click();
            driver.FindElement(By.XPath("//label[text()='Asia Angle']/following::lightning-base-combobox-item/span[2]/span[text()='" + real + "']")).Click();
            driver.FindElement(comboRealEstate).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//label[text()='Real Estate Angle']/following::lightning-base-combobox-item/span[2]/span[text()='" + real + "']")).Click();

            js.ExecuteScript("window.scrollTo(0,400)");
            driver.FindElement(comboInternational).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//label[text()='International Angle?']/following::lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            driver.FindElement(comboSanctions).Click();
            driver.FindElement(By.XPath("//label[text()='Sanctions Concerns/Issues?']/following::div/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='No']")).Click();
            driver.FindElement(txtRisk).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 13));
            js.ExecuteScript("window.scrollTo(0,380)");
            driver.FindElement(btnExistingClient).Click();
            driver.FindElement(btnExistingClient).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//div/lightning-base-combobox)[3]/div/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='"+lockup+"']")).Click();
            driver.FindElement(btnExistingRel).Click();
            driver.FindElement(By.XPath("(//div/lightning-base-combobox)[4]/div/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + lockup + "']")).Click();
            js.ExecuteScript("window.scrollTo(0,700)");
            string text = ReadExcelData.ReadData(excelPath, "NBCForm", 3);
            js.ExecuteScript("window.scrollTo(0,700)");
            driver.FindElement(txtHLComp).SendKeys(text);
            js.ExecuteScript("window.scrollTo(0,900)");
            driver.FindElement(valProceeds).Click();
            driver.FindElement(btnChosen).Click();
            js.ExecuteScript("window.scrollTo(0,700)");
            driver.FindElement(txtOwnership).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 8));
            driver.FindElement(txtTotalDebt).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 9));
            Thread.Sleep(4000);
            js.ExecuteScript("window.scrollTo(0,1650)");
            Thread.Sleep(3000);
            //Selecting Pre Pitch value
            driver.FindElement(btnPrePitch).Click();
            driver.FindElement(btnPrePitch).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("(//div/lightning-base-combobox)[9]/div/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='Yes']")).Click();
            js.ExecuteScript("window.scrollTo(0,1000)");
            driver.FindElement(txtStructure).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 9));
            js.ExecuteScript("window.scrollTo(0,2500)");
            Thread.Sleep(3000);
            driver.FindElement(chkGroupHead).Click();
            driver.FindElement(chkGroupHead).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-1400)");
        }

        //Save all required details in Financial Overview tab
        public void SaveAllReqFieldsInFinancials(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,600)");
            driver.FindElement(btnFinSubject).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//label[text()='Financials Subject to Audit']/following::lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[3]/span[2]/span")).Click();
            js.ExecuteScript("window.scrollTo(0,350)");
            string fee = ReadExcelData.ReadData(excelPath, "NBCForm", 9);
            driver.FindElement(txtCapitalRaise).SendKeys(fee);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-350)");
        }

        //Save all required details in Fees tab
        public void SaveAllReqFieldsInFees(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            string fee = ReadExcelData.ReadData(excelPath, "NBCForm", 9);
            driver.FindElement(txtRetainerFeeCred).SendKeys(fee);
            driver.FindElement(txtProgressFee).SendKeys(fee);
            js.ExecuteScript("window.scrollTo(0,350)");
            driver.FindElement(txtMinFee).SendKeys(fee);
            driver.FindElement(txtEstimatedFee).SendKeys(fee);
            js.ExecuteScript("window.scrollTo(0,350)");
            Thread.Sleep(3000);
            driver.FindElement(btnLockUp).Click();
            driver.FindElement(btnLockUp).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//label[text()='Lockups on Future M&A or Financing Work']/following::lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            js.ExecuteScript("window.scrollTo(0,350)");
            driver.FindElement(txtReferralFee).SendKeys(fee);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            js.ExecuteScript("window.scrollTo(0,-150)");
        }

        //Save all required details in Administrative tab
        public void SaveAllReqFieldsInAdministrative(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);
            driver.FindElement(btnRestricted).Click();
            //driver.FindElement(btnRestricted).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//label[text()='Restricted List']/following::lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[3]/span[2]/span")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            Thread.Sleep(6000);

        }

        //Update Next Scheduled Call checkbox
        public void UpdateNextSchCall()
        {
            driver.FindElement(chkNextSchCall).Click();
            //driver.FindElement(chkNextSchCall).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 150);
            driver.FindElement(btnSave).Click();
        }

        //Click Submit button
        public string ClickSubmitButton()
        {            
            Thread.Sleep(7000);
            driver.FindElement(btnSubmitForReview).Click();
            Thread.Sleep(7000);
            driver.SwitchTo().Frame(0);
            Console.WriteLine("In the frame");
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
                Thread.Sleep(4000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitForReview, 190);
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

        //Update Grade value
        public string UpdateGrade()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditGrade, 290);
            driver.FindElement(lnkEditGrade).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnGrade).Click();
            Console.WriteLine("Clicked Grade");
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//label[text()='Grade']/following::lightning-base-combobox[1]/div/div[1]/div[2]/lightning-base-combobox-item/span[2]/span[text()='A+']")).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(valGrade).Text;
            return value;
        }

        //Fetch the label of Current Status
        public string GetLabelCurrentStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCurrentStatus, 100);
            string text = driver.FindElement(lblCurrentStatus).Text;
            return text;
        }

        //Fetch the label of Risk Factors
        public string GetLabelRiskFactors()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRiskFact, 100);
            string text = driver.FindElement(lblRiskFact).Text;
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

        //Fetch the label of Asia Angle
        public string GetLabelAsiaAngle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblAsiaAngle, 100);
            string text = driver.FindElement(lblAsiaAngle).Text;
            return text;
        }

        //Fetch the label of Real Estate Angle
        public string GetLabelRealEstAngle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRealEstate, 100);
            string text = driver.FindElement(lblRealEstate).Text;
            return text;
        }

        //Fetch the label of International Angle
        public string GetLabelIntAngle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblIntAngle, 100);
            string text = driver.FindElement(lblIntAngle).Text;
            return text;
        }

        //Fetch the section name Houlihan Lokey Competition
        public string GetSectionHLComp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secHLComp, 100);
            string text = driver.FindElement(secHLComp).Text;
            return text;
        }

        //Fetch the label of Houlihan Lokey Competition
        public string GetLabelHLComp()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,600)");
            WebDriverWaits.WaitUntilEleVisible(driver, lblHLComp, 200);
            string text = driver.FindElement(lblHLComp).Text;
            return text;
        }

        //Fetch the section name Describe Other use(s) of proceeds if applicable.
        public string GetSectionDescribeOther()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secDescribeOther, 100);
            string text = driver.FindElement(secDescribeOther).Text;
            return text;
        }

        //Fetch the label of Use of Proceeds
        public string GetLabelUseOfProceeds()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,600)");
            WebDriverWaits.WaitUntilEleVisible(driver, lblUseOfPro, 120);
            string text = driver.FindElement(lblUseOfPro).Text;
            return text;
        }
        //Fetch the label of Use of Proceeds Detail
        public string GetLabelUseOfProceedDetail()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblProDetails, 100);
            string text = driver.FindElement(lblProDetails).Text;
            return text;
        }

        //Fetch the section name Ownership Structure & Capital Structure (PLEASE INCLUDE DEBT SUMMARY)
        public string GetSectionOwnershipStr()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secOwnership, 100);
            string text = driver.FindElement(secOwnership).Text;
            return text;
        }

        //Fetch the label of Ownership and Capital Structure
        public string GetLabelOwnershipStr()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblOwnership, 100);
            string text = driver.FindElement(lblOwnership).Text;
            return text;
        }

        //Fetch the section name Total Debt(MM)
        public string GetSectionTotalDebt()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secTotalDebt, 100);
            string text = driver.FindElement(secTotalDebt).Text;
            return text;
        }

        //Fetch the label of Total Debt (MM)
        public string GetLabelTotalDebt()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblTotalDebt, 100);
            string text = driver.FindElement(lblTotalDebt).Text;
            return text;
        }

        //Fetch the section name Pre - Pitch
        public string GetSectionPrePitch()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,600)");
            WebDriverWaits.WaitUntilEleVisible(driver, secPrePitch, 100);
            string text = driver.FindElement(secPrePitch).Text;
            return text;
        }

        //Fetch the label of Will there be a pitch
        public string GetLabelWillThereBePitch()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPrePitch, 100);
            string text = driver.FindElement(lblPrePitch).Text;
            return text;
        }


        //Fetch the section name Structure and Pricing Expectations (if relevant)
        public string GetSectionStructureAndPricing()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,400)");
            WebDriverWaits.WaitUntilEleVisible(driver, secStrcuture, 100);
            string text = driver.FindElement(secStrcuture).Text;
            return text;
        }

        //Fetch the label of Structure and Pricing Expectations
        public string GetLabelStructureAndPricing()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblStrcuture, 100);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,700)");
            string text = driver.FindElement(lblStrcuture).Text;
            return text;
        }

        //Fetch the section name Referral
        public string GetSectionReferral()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secReferral, 100);
            string text = driver.FindElement(secReferral).Text;
            return text;
        }

        //Fetch the label of Referral Type
        public string GetLabelReferralType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblReferralType, 100);
            string text = driver.FindElement(lblReferralType).Text;
            return text;
        }

        //Fetch the label of Referral Source
        public string GetLabelReferralSource()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblReferralSource, 100);
            string text = driver.FindElement(lblReferralSource).Text;
            return text;
        }

        public string GetSectionPlease()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,500)");
            WebDriverWaits.WaitUntilEleVisible(driver, secPls, 100);
            string text = driver.FindElement(secPls).Text;
            return text;
        }

        //Fetch the label of Group Head Approval
        public string GetLabelGroupHead()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            WebDriverWaits.WaitUntilEleVisible(driver, lblGroup, 100);
            string text = driver.FindElement(lblGroup).Text;
            js.ExecuteScript("window.scrollTo(0,-900)");
            return text;
        }
        //Fetch message of Financials Unavailable
        public string GetMessageFinUnavailable()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, msgFinUnavailable, 100);
            string text = driver.FindElement(msgFinUnavailable).Text;
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

        //Fetch message of Capital Raise (MM) (Best estimate of capital to be raised expressed as a currency value in millions)
        public string GetMessageCapitalRaise()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, msgCapital, 100);
            string text = driver.FindElement(msgCapital).Text;
            return text;
        }

        //Fetch the label of Capital Raise
        public string GetLabelCapitalRaise()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,400)");
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblCapRaise, 100);
            string text = driver.FindElement(lblCapRaise).Text;
            js.ExecuteScript("window.scrollTo(0,-400)");
            return text;
        }

        //Fetch the label of Retainer
        public string GetLabelRetainer()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRetainer, 100);
            string text = driver.FindElement(lblRetainer).Text;
            return text;
        }

        //Fetch the label of Fee Structure
        public string GetLabelFeeStructure()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblFeeStructure, 100);
            string text = driver.FindElement(lblFeeStructure).Text;
            return text;
        }

        //Fetch the label of Lockups on Future M&A or Financing Work
        public string GetLabelLockupsOnFuture()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblLockups, 100);
            string text = driver.FindElement(lblLockups).Text;
            return text;
        }

        //Fetch the label of Referral Fee Owed (MM)
        public string GetLabelReferralFeeOwed()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblReferral, 100);
            string text = driver.FindElement(lblReferral).Text;
            return text;
        }

        //Fetch the label of Retainer Fee Creditable
        public string GetLabelRetainerFeeCred()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRetainerFee, 100);
            string text = driver.FindElement(lblRetainerFee).Text;
            return text;
        }

        //Fetch the label of Progress Fee Creditable
        public string GetLabelProgressFeeCred()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblProgress, 100);
            string text = driver.FindElement(lblProgress).Text;
            return text;
        }

        //Fetch the Section name Administrative
        public string GetSectionAdministrative()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgAdmin, 100);
            string text = driver.FindElement(msgAdmin).Text;
            return text;
        }

        //Fetch the label of Restricted List
        public string GetLabelRestrictedList()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRestrictedList, 100);
            string text = driver.FindElement(lblRestrictedList).Text;
            return text;
        }

        //Fetch the Section name Conflicts Check Information
        public string GetSectionCCInfo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgCCInfo, 100);
            string text = driver.FindElement(msgCCInfo).Text;
            return text;
        }

        //Fetch the label of Conflict Check Status
        public string GetLabelCCStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCCStatus, 100);
            string text = driver.FindElement(lblCCStatus).Text;
            return text;
        }
        //Validate section To submit CNBC Form
        public string ValidateSectionSubmitCNBC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secToSubmit, 100);
            string text = driver.FindElement(secToSubmit).Text;
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


        public void EnterDetailsAndClickSubmit(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            //Opportunity Overview
            WebDriverWaits.WaitUntilEleVisible(driver, checkConfirm, 90);
            driver.FindElement(checkConfirm).Click();
            driver.FindElement(comboClient).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 19));
            driver.FindElement(txtHLComp).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 20));
            driver.FindElement(txtAvailProceeds).Click();
            driver.FindElement(imgArrow).Click();
            driver.FindElement(comboExistingRel).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 21));

            //Overview and Financials
            driver.FindElement(txtTranOverview).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 3));
            driver.FindElement(txtCurrentStatus).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 4));
            driver.FindElement(txtCompDesc).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 5));
            driver.FindElement(comboCrossBorder).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 6));
            driver.FindElement(comboAsiaAngle).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 7));
            driver.FindElement(comboRealEstate).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 7));
            driver.FindElement(txtOwnershipStr).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 8));
            driver.FindElement(txtTotalDebt).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 9));
            driver.FindElement(comboAudit).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 10));
            driver.FindElement(txtCapRaise).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 11));
            driver.FindElement(txtStrExp).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 12));
            driver.FindElement(txtRiskFactors).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 13));

            //Fees
            driver.FindElement(txtEstFee).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 14));
            driver.FindElement(txtFeeStr).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 15));
            driver.FindElement(comboLockUps).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 16));
            driver.FindElement(comboReferralFee).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 17));

            //Administrative
            driver.FindElement(comboResList).SendKeys(ReadExcelData.ReadData(excelPath, "CNBCForm", 27));

            driver.FindElement(btnSubmit).Click();
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
            WebDriverWaits.WaitUntilEleVisible(driver, valEmailOppName, 70);
            string emailSub = driver.FindElement(valEmailOppName).Text;
            Console.WriteLine(emailSub);
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnCancelEmail).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturntoOpp, 70);
            driver.FindElement(btnReturntoOpp).Click();
            return emailSub;
        }

        //Validate Review section 
        public string ValidateReviewSectionForAdmin()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lblReviewAdmin);
                string txtReview = driver.FindElement(lblReviewAdmin).Text;
                return txtReview;
            }
            catch (Exception e)
            {
                return "No Review section";
            }
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
            catch (Exception e)
            {
                return "No Review section";
            }
        }

        //To validate NBC form is disabled
        public string ValidateIfFormIsEditable()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtTotalDebt, 90);
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
                WebDriverWaits.WaitUntilEleVisible(driver, comboGrade, 20);
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
                WebDriverWaits.WaitUntilEleVisible(driver, txtNotes, 20);
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
                WebDriverWaits.WaitUntilEleVisible(driver, txtDateSubmitted, 20);
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
                WebDriverWaits.WaitUntilEleVisible(driver, txtReason, 20);
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
                WebDriverWaits.WaitUntilEleVisible(driver, txtFeeDiff);
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
                WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 20);
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
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturntoOpp, 20);
            string value = driver.FindElement(btnReturntoOpp).Enabled.ToString();
            return value;
        }

        //Validate Return To Opportunity button
        public string ValidateReturnToOpportunityButtonForCFUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturntoOppCFUser);
            string value = driver.FindElement(btnReturntoOppCFUser).Enabled.ToString();
            return value;
        }
        //Validate EU Override button
        public string ValidateEUOverrideButton()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEUOverride, 20);
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
            WebDriverWaits.WaitUntilEleVisible(driver, btnPDFView);
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

        //Validate Add Financials button
        public string ValidateAddFinancialsButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddFinancials);
            string value = driver.FindElement(btnAddFinancials).Enabled.ToString();
            return value;
        }
    }
}