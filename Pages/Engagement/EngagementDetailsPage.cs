﻿using AventStack.ExtentReports.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SF_Automation.TestCases.Opportunities;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;

namespace SF_Automation.Pages.Engagement
{
    class EngagementDetailsPage : BaseClass
    {
        By valEngContact2 = By.CssSelector("div[id*='D7QcI_body'] table th a:nth-child(2)");
        By lnkContract = By.CssSelector("div[id*='M0ecq_body'] > table > tbody > tr:nth-child(2) > th > a");
        By valEngName = By.CssSelector("div[id='Namej_id0_j_id4_ileinner']");
        By valStage = By.CssSelector("td[id*='id0_j_id4_ilecell']>div[id='00Ni000000D7NlWj_id0_j_id4_ileinner']");
        By valRecordType = By.CssSelector("div[id*='RecordType']");
        By valLegalEntity = By.CssSelector("div[id*='eecj']");
        By valHLEntity = By.CssSelector("div[id *= '00Ni000000D96Bbj_id0_j_id4_ileinner']");
        By titleEngPage = By.CssSelector("h1[class='pageType']");
        By btnEdit = By.CssSelector("input[value=' Edit ']");
        By txtEngNum = By.CssSelector("input[name*='D96p8']");
        By btnSave = By.CssSelector("input[name='save']");
        By valEngNum = By.CssSelector("div[id*='D96p8j']");
        By btnPortfolioValuation = By.CssSelector("input[title='Portfolio Valuation']");
        By btnFREngSummary = By.CssSelector("input[value='FR Engagement Summary']");
        By btnFREngSummaryL = By.XPath("//button[@name='Engagement__c.Engagement_Summary_FR']");
        By tabDefault = By.XPath("//li/a[text()='Engagement Info']");
        By tabFinancialsL = By.XPath("//li/a[text()='Financials / Projections']");
        By tabDMAL = By.XPath("//li/a[text()='DM&A Info']");
        By tabHLFinancingL = By.XPath("//li/a[text()='HL Financing']");
        By tabPreTransL = By.XPath("//li/a[text()='Pre-Transaction Info']");
        By tabPostTransL = By.XPath("//li/a[text()='Post-Transaction Info']");
        By tabHLPostTransL = By.XPath("//li/a[text()='HL Post-Transaction Opportunities']");
        By tabClientL = By.XPath("//a[text()='Client/Subject & Referral']");
        By valTxnType = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.Transaction_Type__c']/div//dd/div[1]/span/slot/lightning-formatted-text");
        By valPostTxnStatus = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.Post_Transaction_Status__c']/div[1]//dd/div/span/slot/lightning-formatted-text");
        By valCompDesc = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.FR_Client__c']/div//dd/div[1]/span/slot/lightning-formatted-text");
        By valBusDesc = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.Business_Description__c']/div//dd/div[1]/span/slot/lightning-formatted-text");
        By valReDesc = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.Restructuring_Description__c']/div//dd/div[1]/span/slot/lightning-formatted-text"); 
        By titleFREngSum = By.CssSelector("h2[class='pageDescription']");
        By lblTransType = By.CssSelector("div[id*='id37'] > div[class='pbBody'] > table > tbody > tr:nth-child(1) > td:nth-child(1) > label");
        By valRevAccural = By.CssSelector("div[id*='saB_body']>table>tbody>tr:nth-child(2)>th>a:nth-child(2)");
        By lnkDeleteAccurals = By.CssSelector("div[id*='saB_body']>table>tbody>tr>td>a[title*='Delete']");
        By btnAddRevenueAccurals = By.CssSelector("input[title='Add Revenue Accrual']");
        By txtPeriodAccuredFees = By.CssSelector("input[name*='Ehsau']");
        By txtTotalEstFees = By.CssSelector("div[id*='zPj_id0_j_id4_ileinner']");
        By txtEngName = By.CssSelector("div[id*='Namej_id0_j_id4_ileinner']");
        By valPeriodAccrual = By.CssSelector("div[id*='00Ni000000EhsaB_body']>table>tbody>tr:nth-child(2)>td:nth-child(3)");
        By valTotalEstFee = By.CssSelector("div[id*='hsaB_body']>table>tbody>tr:nth-child(2)>td:nth-child(4)");
        By valPeriodAccrualFee = By.CssSelector("div[id*='FvixUj_id0_j_id4_ileinner']");
        //By valPeriodAccrualFeeFAS = By.CssSelector("div[id*='FnLOWj_id0_j_id4_ileinner']");s
        By txtTotalEstFeesFAS = By.CssSelector("input[id*='00Ni000000FmBzP']");
        By valTotalEstFeesFAS = By.CssSelector("div[id*='00Ni000000FmBzP']");
        By valYearMonth = By.CssSelector("div[id*='hsaB_body']>table>tbody>tr:nth-child(2)>th>a:nth-child(2)");
        By valYearMonthL = By.XPath("//tr[1]/th//span/a[2]");
        By valTotalEstFeeL = By.XPath("//span[text()='Total Estimated Fee']/ancestor::div[2]/dd//lightning-formatted-text");
        By tabFees2ndEngL = By.XPath("//section[3]//div[1]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar//li[3]/a");
        By tabInfo2ndEngL = By.XPath("//section[3]//div[1]//flexipage-record-home-template-desktop2//div[1]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar//li[1]/a");
        By valTotalEstFee2ndEngL = By.XPath("//section[3]//flexipage-tab2[3]/slot/flexipage-component2[2]//flexipage-column2[1]//flexipage-field[3]//lightning-formatted-text");

        By txtStage = By.CssSelector("select[name*='NlW']");
        By lnkEditContact = By.CssSelector("div[id*='cI_body'] > table > tbody > tr > td.actionColumn > a:nth-child(1)");
        By txtContact = By.CssSelector("span>input[id*='OPH']");
        By valContactL = By.XPath("//flexipage-column2[1]//record_flexipage-record-field//slot[1]/lightning-formatted-name");
        By btnBillingRequest = By.CssSelector("input[value='Billing Request']");
        By msgContact = By.CssSelector("div[id*='id34:j_id36']");
        By msgContactL = By.XPath("//div[@class='messageText']");
        By btnBackToManagement = By.CssSelector("input[value='Back To Engagement']");
        By btnBackToEngagementL = By.XPath("//input[@value='Back To Engagement']");
        By btnSendEmail = By.CssSelector("input[value='Send Email']");
        By comboAccountingStatus = By.CssSelector("select[id='00Ni000000FF7XF']");

        By titleBillingForm = By.CssSelector("h2[class='mainTitle']");

        By txtParty = By.CssSelector("select[name*='M0eMS']");
        By valContact = By.CssSelector("div[id*='QcI_body']> table > tbody > tr.dataRow.even.last.first > th>a:nth-child(2)");       
        
        By comboStage = By.CssSelector("select[id = '00Ni000000D7NlW']");
        By chkExpApplication = By.CssSelector("img[id*='jj_id0_j_id4_chkbox']");
        By valAccountingStatus = By.CssSelector("div[id*='7XFj_id0_j_id4_ileinner']");
        By lnkRevenueMonth = By.CssSelector("div[id*='00Ni000000EhsaB_body'] > table> tbody >tr:nth-child(2) >th >a:nth-child(2)");
        By valRevID = By.CssSelector("div[id='Name_ileinner']");
        By valRevIDL = By.XPath("//span[text()='Revenue Accrual #']/ancestor::div[2]/dd//slot[1]/lightning-formatted-text");
        By lnkEngagement = By.CssSelector("a[id*='EhsaB']");
        By btnCounterParties = By.CssSelector("td[id*='topButtonRow'] > input[value='Counterparties']");
        By btnAddRevenueAccrual = By.CssSelector("input[value='Add Revenue Accrual']");
        By errorMessage = By.CssSelector("div[id='errorDiv_ep']");
        By errorMessageL = By.XPath("//a[@class='errorsListLink']");
        By tabEngagement = By.CssSelector("a[title*='Engagements Tab - Selected']");
        By tabEngagementL = By.XPath("//div[2]/section//ul[2]/li[2]/a/span[2]");
        By tabEngRevProj = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Revenue_Projection__c.Engagement__c']/div/dd//span//records-hoverable-link");
        By comboClientOwnership = By.CssSelector("select[id*='d2R']");
        By txtDebt = By.CssSelector("input[id*='LfH']");
        By valClientOwnership = By.CssSelector("div[id*='d2Rj_id0_j_id4_ileinner']");
        By valClientOwnershipL = By.XPath("//flexipage-column2[2]//flexipage-field[3]//slot[1]/lightning-formatted-text");
        By valTotalDebt = By.CssSelector("div[id*='fHj_id0_j_id4_ileinner']");
        By lnkRecChange = By.CssSelector("div[id*='RecordType'] > a");
        By comboRecType = By.CssSelector("select[id*='p3']");
        By btnContinue = By.CssSelector("input[value='Continue']");
        By txtComments = By.CssSelector("textarea[id*='FlHaO']");
        By lnkFinalReport = By.CssSelector("div:nth-child(13) > table > tbody > tr:nth-child(1) > td:nth-child(4) > span > span > a");
        By lnkReDisplayRec = By.CssSelector("table > tbody > tr:nth-child(2) > td > a:nth-child(4)");
        By valPOC = By.CssSelector("div[id*='GQj_id0_j_id4_ileinner']");
        By valERPSubmittedToSync = By.CssSelector("div[id*='eQj']");
        By valERPID = By.CssSelector("div[id*='e6j']");
        By valERPHLEntity = By.CssSelector("div[id*='e5j']");
        By valERPLegalEntityId = By.CssSelector("div[id*='eEj']");
        By valERPProjectNumber = By.CssSelector("div[id*='eMj']");
        By valERPProjectName = By.CssSelector("div[id*='eLj']");
        By valLOB = By.CssSelector("div[id*='oEj']");
        By valLOBL = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.Line_of_Business__c']//dd//lightning-formatted-text");
        By valClientCompL = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.Client__c']//dd//records-hoverable-link//span//slot//slot");
        By valLegalEntityL = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.Legal_Entity__c']//dd//records-hoverable-link//span//slot//slot");

        By valERPLOB = By.CssSelector("div[id*='e8j']");
        By valIG = By.CssSelector("div[id*='Axj']");
        By valERPIG = By.CssSelector("div[id*='e7j']");
        By valERPLastIntStatus = By.CssSelector("div[id*='eCj']");
        By valERPResponseDate = By.CssSelector("div[id*='eBj']");
        By valERPError = By.CssSelector("div[id*='e9j']");
        By valJobType = By.CssSelector("div[id*='5sj']");
        By valJobTypeL = By.XPath("//flexipage-field[@data-field-id='RecordJob_Type_cField1']/slot/record_flexipage-record-field//lightning-formatted-text");
        By btnPortfolioVL = By.XPath("//button[text()='Portfolio Valuation']");
        By msgNoValL = By.XPath("//div[text()='Currently there are no valuation periods for this Engagement. To proceed, please create a new valuation period.']");
        By btnBackToEngL = By.XPath("//input[@value='Back To Engagement']");
        By tabDetails = By.XPath("//a[text()='Details']");
        By lnkStageL = By.XPath("//flexipage-tabset2//flexipage-column2[2]//flexipage-field[5]//dt/div/span/ancestor::div[2]/dd//button");
        By btnStageL = By.XPath("//flexipage-field[5]//div[2]/slot//button/span");
        By valStageL = By.XPath("//flexipage-tab2[1]//flexipage-tab2[1]//flexipage-column2[2]/div/slot/flexipage-field[5]//lightning-combobox//lightning-base-combobox-item/span[2]/span[text()='Bill/File']");
        By tabOpportunityL = By.XPath("//div[2]/div/div/ul[2]/li[2]/a");
        By valImportedValPeriod = By.XPath("//tr[1]/td[2]/a");
        By valSavedStageL = By.XPath("//flexipage-tabset2//flexipage-column2[2]//flexipage-field[5]//dt/div/span/ancestor::div[2]/dd//slot[1]/lightning-formatted-text");

        By valImportedPositionL = By.XPath("//span/table/tbody/tr[1]/td[2]/a[1]");

        By valERPProductType = By.CssSelector("div[id*='eej']");
        By valERPProductTypeCode = By.CssSelector("div[id*='eHj']");
        By valERPTemplate = By.CssSelector("div[id*='eUj']");
        By valERPUnitID = By.CssSelector("div[id*='e2j']");
        By valERPUnit = By.CssSelector("div[id*='e3j']");
        By valERPLegalEntityID = By.CssSelector("div[id*='eDj']");
        By valERPEntityCode = By.CssSelector("div[id*='e4j']");
        By valERPLegCode = By.CssSelector("div[id*='eFj']");
        By comboPrimaryOffice = By.CssSelector("select[id*='Lq0']");
        By valPrimaryOffice = By.CssSelector("div[id*='Lq0']");
        By checkERPUpdateDFF = By.CssSelector("div[id*='eWj']>img");
        By comboIG = By.CssSelector("select[id*='6Ax']");
        By valIGCF = By.CssSelector("div[id*='6Ax']");
        By comboSector = By.CssSelector("select[id*='6B7']");
        By valSector = By.CssSelector("div[id*='6B7']");
        By comboJobType = By.CssSelector("select[id*= '65s']");
        By lnkRecordTypeChange = By.CssSelector("div[id*='RecordTypej_id0_j_id4_ileinner'] > a");
        By comboLOB = By.CssSelector("select[id*='LoE']");
        By lnkClient = By.XPath("//*[text()='Client']/.. //td[2]//div//a");
        By lnkSecondContract = By.CssSelector("div[id*='M0ecq_body'] > table > tbody > tr:nth-child(3) > th > a");
        By lnkSyncDate = By.CssSelector("table > tbody > tr:nth-child(1) > td.dataCol.col02 > span > span > a");
        By rowContract = By.CssSelector("div[id*='cq_body']>table>tbody>tr.dataRow.even.last.first>th>a");
        By valERPContractType = By.CssSelector("div[id*='cq_body']>table>tbody>tr.dataRow.even.last.first>td:nth-child(4)");
        By valERPBusUnit = By.CssSelector("div[id*='cq_body']>table>tbody>tr.dataRow.even.last.first>td:nth-child(5)");
        By valERPLegalEntity = By.CssSelector("div[id*='cq_body']>table>tbody>tr.dataRow.even.last.first>td:nth-child(6)");
        By valERPBillPlan = By.CssSelector("div[id*='cq_body']>table>tbody>tr.dataRow.even.last.first>td:nth-child(7)");
        By valBillTo = By.CssSelector("div[id*='cq_body']>table>tbody>tr.dataRow.even.last.first>td:nth-child(8)>a");
        By valCompName = By.CssSelector("div[id*='QcI_body']>table>tbody>tr.dataRow.even.last.first>td:nth-child(10)>a");
        By valStartDate = By.CssSelector("div[id*='cq_body']>table>tbody>tr.dataRow.even.last.first>td:nth-child(9)");
        By valIsMain = By.CssSelector("div[id*='cq_body']>table>tbody>tr:nth-child(2)>td:nth-child(11)>img");
        By valContract1 = By.CssSelector("div[id*='ecq_body'] > table > tbody > tr:nth-child(2) > th > a");
        By valContract1L = By.XPath("//span[text()='Additional Contract']");
        By valContract2 = By.CssSelector("div[id*='ecq_body'] > table > tbody > tr:nth-child(3) > th > a");
        By valContract2L = By.XPath("//span[text()='Test Contract']");
        By lnk2ndContractL = By.XPath("//table[@aria-label='Contract']/tbody/tr[1]/th//records-hoverable-link");
        By checkIsMainL = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Contract__c.Is_Main_Contract__c']//dd//label/span[1]");
        By tabOppNameL = By.XPath("//section[1]/div/div/div/div/div/ul[2]/li[2]/a");
        By lnkOppL = By.XPath("//flexipage-component2[1]//dl/slot/records-record-layout-row[6]//records-hoverable-link");
        By btnCloseOppL = By.XPath("//span[@title='Engagement  c']/ancestor::li[1]//button[contains(@title,'Close')]");
        //By btnCloseReqEngFVAL = By.XPath("//button[@title='Close this window']");
        
        By lnkOpp = By.CssSelector("div[id*='zAzj']>a");
        By btnCancel = By.CssSelector("input[value='Cancel']");
        //By valNewSubject = By.CssSelector("div[id*='aho5_00Ni000000D9DbX_body'] > table > tbody > tr:nth-child(4)>th>a");

        By lnkDelClient = By.CssSelector("div[id*='DbX_body'] > table > tbody > tr.dataRow.even.first> td.actionColumn > a:nth-child(2)");
        By valWomenLed = By.CssSelector("div[id *= 'NgVj_id0_j_id4_ileinner']");
        By txtSecWomenled = By.CssSelector("div[id*='Cyy_ep_j_id0_j_id4']>h3");
        By txtSecWomenLedESOP = By.CssSelector("div[id*='2Cxi_ep_j_id0_j_id4");
        By txtSecWomenLedOther = By.CssSelector("div[id*='2G_ep_j_id0_j_id4']>h3");
        By txtSecWomenLedActivism = By.CssSelector("div[id*='X1_ep_j_id0_j_id4']>h3");
        By txtSecWomenLedFVA = By.CssSelector("div[id*='9E_ep_j_id0_j_id4']>h3");
        By txtSecWomenLedFR = By.CssSelector("div[id*='PL_ep_j_id0_j_id4']>h3");

        By btnAdditionalClientSubject = By.CssSelector("input[value*='New Opportunity Client/Subject']");
        By imputCoExist = By.XPath("//input[@id='00N6e00000MRVFN']");
        By shwAllTab = By.CssSelector("li[id='AllTab_Tab'] > a > img");
        By imgCoverageSectorDependencies = By.CssSelector("img[alt = 'Coverage Sector Dependencies']");

        By txtCoverageType = By.CssSelector("input[id='00N6e00000MRMtkEAHCoverage_Sector_Dependency__c']");
        By txtPrimarySector = By.CssSelector("input[id='00N6e00000MRMtlEAHCoverage_Sector_Dependency__c']");
        By txtSecondarySector = By.CssSelector("input[id='00N6e00000MRMtmEAHCoverage_Sector_Dependency__c']");
        By txtTertiarySector = By.CssSelector("input[id='00N6e00000MRMtnEAHCoverage_Sector_Dependency__c']");
        By btnApplyFilters = By.XPath("//input[@title='Apply Filters']");
        By linkShowAllResults = By.XPath("//a[contains(text(),'Show all results')]");

        //By btnSendEmail = By.CssSelector("input[value='Send Email']");

        By titlePage = By.CssSelector("h2[class='pageDescription']");
        By titleOppDetails = By.CssSelector("div[id*='j_id55'] > div.pbHeader > table > tbody > tr > td.pbTitle > h2");


        By textEngagementDetailEngagementName = By.XPath("//span[@class='test-id__field-label'][normalize-space()='Engagement Name']/parent::div/following-sibling::div//lightning-formatted-text");
        By textEngagementDetailEngagementNumber = By.XPath("//p[@title='Engagement Number']//following-sibling::p//slot//lightning-formatted-text");
        By chkNBCApproved = By.CssSelector("img[id*='FmBzhj_id0_j_id55_chkbox']");
        By titlePopUpNBC = By.XPath("//div[@class='custPopup']/p");

        By labelWomenLed = By.CssSelector("div:nth-child(33) > table > tbody > tr:nth-child(9) > td:nth-child(1)");
        By labelWomenLedJob = By.CssSelector("div:nth-child(33) > table > tbody > tr:nth-child(7) > td:nth-child(3)");// div:nth-child(33) > table > tbody > tr:nth-child(7) > td:nth-child(3)");
        By labelWomenLedActivism = By.CssSelector("div:nth-child(35) > table > tbody > tr:nth-child(7) > td:nth-child(1)");
        By labelWomenFVA = By.CssSelector("div:nth-child(25)  > table > tbody > tr:nth-child(3) > td:nth-child(1)");
        By labelWomenFR = By.CssSelector("div:nth-child(29) > table > tbody > tr:nth-child(13) > td:nth-child(1)");
        By valAddedClient = By.CssSelector("div[id*='DbX_body']> table > tbody > tr:nth-child(2) > td:nth-child(3)");
        By valAddedClientName = By.CssSelector("div[id *= 'DbX_body']> table > tbody > tr:nth-child(6) >th>a");
        By tabClientSubject = By.XPath("//a[text()='Client/Subject & Referral']");
        By valAddedKeyCred = By.CssSelector("div[id*='DbX_body']> table > tbody > tr:nth-child(6) >th>a");
        By valAddedKeyCredType = By.CssSelector("div[id*='DbX_body']> table > tbody > tr:nth-child(6) > td:nth-child(3)");
        By lnkShowMore = By.CssSelector("div[id*='DbX_body'] > div > a:nth-child(1)");
        //By lnkShowMore = By.CssSelector("div[id*='DuhQp_body'] > div > a:nth-child(1)");
        By btnMassEditRecords = By.CssSelector("input[value*='Mass Edit Records']");
        By titleMassEditPage = By.XPath("//span[@class='slds-text-heading_small slds-truncate']");
        By btnBackToEng = By.XPath("//div[1]/span/lightning-button/button");
        By lblEngagement = By.XPath("//records-entity-label[text()='Engagement']");
        By titleEngDetails = By.CssSelector("div[id*='j_id4'] > div.pbHeader > table > tbody > tr > td.pbTitle > h2");
        By btnMassEditRecordsL = By.XPath("//button[text()='Mass Edit Records']");
        By titleMassEditPageL = By.XPath("//header/div[2]/h2/span");
        By btnNewEngAdditionalClientSub = By.CssSelector("input[value='New Engagement Client/Subject']");
        By btnAdditionalClientSub = By.XPath("//div[2]/span/lightning-button/button");
        By btnDeleteRecords = By.XPath("//div[3]/span/lightning-button/button");
        By btnEditMassEdit = By.XPath("//header/div[2]/slot/lightning-button/button");
        By txtRefresh = By.XPath("//div[2]/div[2]/span/p");
        By comboTypeMassEdit = By.XPath("//lightning-base-combobox-item[contains(@id,'button-17')]/span[2]/span");
        By colTableColumns = By.XPath("//table/thead/tr/td/div");
        By txtAlertMessage = By.XPath("//slot/div/div/h2");
        By btnCloseError = By.XPath("//div/div/div/lightning-button-icon/button");
        By valType = By.XPath("//button[contains(@id,'button-17')]");
        By valSelectedType = By.XPath("//div[@id='dropdown-element-17']/lightning-base-combobox-item[@aria-checked='true']");
        //By valSelectedType = By.XPath("//lightning-base-combobox/div/div[@id='dropdown-element-16']/lightning-base-combobox-item[@aria-checked='true']");

        By valCurrencyL = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.CurrencyIsoCode']/div[1]//lightning-formatted-text");
        By valTotalDebtMM = By.CssSelector("div[id*='fHj_id0_j_id4_ileinner']");
        By iconExpandMoreButonL = By.XPath("(//lightning-button-menu//button[contains(@class,'slds-button slds-button_icon-border-filled')])[3]");
        By btnViewCounterpartiesL = By.XPath("//button[text()='View Counterparties']");
        By btnViewCounterparties = By.XPath("//button[@name='Engagement__c.ViewCounterparties']");
        By btnClose = By.XPath("//section/div[1]/div/div[1]/div[2]/div/div/ul[2]/li[2]/div[2]/button");
        By btnDetails = By.XPath("//tr/td[1]/div/div[1]/lightning-formatted-rich-text/span/a");
        By lnkAddedCounterparty = By.XPath("//tbody/tr/th/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/c-s-l-company-link-column/lightning-layout/slot/lightning-layout-item[2]/slot/lightning-formatted-url");
        By btnEngCounterpartyContact = By.XPath("//button[@name='Engagement_Counterparty__c.New_Engagement_Counterparty_Contact']");
        By lblEngCounterpartyContactSearch = By.XPath("//lightning-card/article/div[1]/header/div[1]/h2/span");
        By btnEditBids = By.XPath("//button[text()='Edit Bids']");
        By lnkDetailsL = By.XPath("//td[1]/div/div[1]/lightning-formatted-rich-text/span/a");
        By lnk2ndDetailsL = By.XPath("//tr[2]/td[1]/div/div[1]/lightning-formatted-rich-text/span/a");
        By valFirstNameL = By.XPath("//dt[text()='First Name:']/ancestor::dl/dd[1]/lst-template-list-field/lst-formatted-text");
        By valLastNameL = By.XPath("//dt[text()='Last Name:']/ancestor::dl/dd[2]/lst-template-list-field/lst-formatted-text");
        By valCommentTypeL = By.XPath("//dt[text()='Comment Type:']/ancestor::dl/dd[1]/lst-template-list-field/lst-formatted-text");
        By valCreatorL = By.XPath("//dt[text()='Creator:']/ancestor::dl/dd[2]/lst-template-list-field/formula-output-formula-html/lightning-formatted-rich-text/span");
        By tabInternalTeam = By.XPath("//a[text()='Internal Team']");
        By txtMember = By.XPath("//div[text()='Member']");
        By linkCoverageSectorDependencyName = By.XPath("//a[@href='#']");
        By btnSaveEngagementSector = By.XPath("(//input[@title='Save'])[1]");
        By txtERPLegalEntity = By.CssSelector("input[id*='M0eec'][type='text']");
        //By checkBoxCoExist = By.CssSelector("div[id*='00N6e00000MRVFOj_id0_j_id55_ileinner'] > img");
        By checkBoxCoExist = By.CssSelector("div[id*='00N6e00000MRVFNj_id0_j_id4_ileinner'] > img");
        //By imputCoExist = By.XPath("//input[@id='00N6e00000MRVFO']");
        By btnCFEngagementSummary = By.XPath("(//input[@title='CF Engagement Summary (lwc)'])[1]");
        By lblHeaderText = By.XPath("//h1/span[2]");
        By linkSectorName = By.XPath("//*/th[contains(text(),'Engagement Sector')]/following::tr/th/a");
        By btnDeleteEngagementSector = By.XPath("(//input[@title='Delete'])[1]");
        By linkEngagementName = By.XPath("(//td[contains(text(),'Engagement')])[2]/../td[2]/div/a");
        By btnNewEngagementSector = By.XPath("//input[@value='New Engagement Sector']");

        By valEngagementSectorName = By.XPath("//td[contains(text(),'Engagement Sector')]/following::div[1]");
        By imgCoverageSectorDependencyLookUp = By.XPath("//img[@alt='Coverage Sector Dependency Lookup (New Window)']");

        By valNoOfContract = By.CssSelector("div[id*='M0ecq'] > table > tbody > tr");
        By lnkBillTo = By.CssSelector("a[id*='A00000M0ebc']");
        By linkEngagementSector = By.XPath("//*/span[contains(text(),'Engagement Sectors')]");
        By btnGo = By.XPath("//input[@type='submit']");
        By txtSearchBox = By.XPath("//input[@title='Go!']/preceding::input[1]");
        By valEnggNumberSuffix = By.CssSelector("div[id*='M0eeaj']");
        By inputCoverageType = By.XPath("//input[@id='00N6e00000MRMtkEAHCoverage_Sector_Dependency__c']");
        By inputPrimarySector = By.XPath("//input[@id='00N6e00000MRMtlEAHCoverage_Sector_Dependency__c']");
        By inputSecondarySector = By.XPath("//input[@id='00N6e00000MRMtmEAHCoverage_Sector_Dependency__c']");
        By inputTertiarySector = By.XPath("//input[@id='00N6e00000MRMtnEAHCoverage_Sector_Dependency__c']");
        By btnEditCompCoverageSector = By.XPath("//input[@title='Edit']");
        By btnAddOppContact = By.CssSelector("input[name='new_external_team']");
        By comboRole = By.CssSelector("select[name*='D7Qcn']");
        By comboParty = By.CssSelector("select[name*='M0eMp']");
        By checkAckBillingContact = By.CssSelector("input[name*='M0jSL']");
        By checkBillingContact = By.CssSelector("input[name*='Gz3dK']");
        By comboType = By.CssSelector("select[name*='D9Dbh']");
        By checkPrimaryContact = By.CssSelector("input[name*='D7OP7']");
        By valClientL = By.XPath("//span[text()='Engagement Name']/ancestor::div[4]/slot//following::flexipage-field[@data-field-id=\"RecordClient_cField1\"]//div[contains(@data-target-selection-name,\".Client__c\")]//dd//records-hoverable-link//a/span/slot/span/slot");
        By valSubjectL = By.XPath("//span[text()='Engagement Name']/ancestor::div[4]/slot//following::flexipage-field[@data-field-id=\"RecordSubject_cField1\"]//div[contains(@data-target-selection-name,\".Subject__c\")]//dd//records-hoverable-link//a/span/slot/span/slot");
        By tabInfo = By.XPath("//a[@aria-controls='tab-1']");
        By tabInformationL = By.XPath("//div[1]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar/ul/li[1]/a");
        By tabInfoL = By.XPath("//section[2]/div//div[1]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar/ul/li[1]");
        By subTabDetails = By.XPath("//flexipage-tab2[1]/slot/flexipage-component2//lightning-tab-bar/ul/li[1]/a[text()='Details']");
        By subTabImpDates = By.XPath("//flexipage-tab2[1]/slot/flexipage-component2//lightning-tab-bar/ul/li[2]/a[text()='Important Dates']");
        By subTabAdmin = By.XPath("//flexipage-tab2[1]/slot/flexipage-component2//lightning-tab-bar/ul/li[3]/a");
        By subTabAdminEngL = By.XPath("//section[3]//flexipage-component2//flexipage-tab2[1]/slot//ul/li[3]/a");
        By subTabClosingInfo = By.XPath("//flexipage-tab2[1]/slot/flexipage-component2//lightning-tab-bar/ul/li[4]/a");
        By subTabComments = By.XPath("//flexipage-tab2[1]/slot/flexipage-component2//lightning-tab-bar/ul/li[4]/a");
        By subTabBilling = By.XPath("//flexipage-tab2[1]/slot/flexipage-component2//lightning-tab-bar/ul/li[5]/a");
        By lnkEditEngName = By.XPath("//flexipage-tab2[1]/slot/flexipage-component2[1]//slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div//button");
        By tabImpDates = By.XPath("//a[text()='Important Dates']");
        By tabInfo2ndL = By.XPath("//a[text()='Info']");
        By valFinalReportL = By.XPath("//flexipage-tab2[2]//flexipage-column2[1]/div/slot/flexipage-field[4]//lightning-formatted-text");
                
        By subTabCST = By.XPath("//ul/li[@title='CST Questionnaire Details']/a[@data-tab-value='flexipage_tab20']");
        
        By btnCancelL = By.XPath("//button[@name='CancelEdit']");
        By valClientOwnershipBefore = By.XPath("//label[text()='Client Ownership']/ancestor::lightning-combobox/div[1]/div/lightning-base-combobox/div/div[1]/div/button/span");
        By btnClientOwnership = By.XPath("//label[text()='Client Ownership']/ancestor::lightning-combobox/div[1]/div/lightning-base-combobox//button");
        By valClientOwnershipAfter = By.XPath("//flexipage-component2//flexipage-tab2[1]/slot/flexipage-component2[1]//flexipage-column2[2]/div/slot/flexipage-field[3]//slot[1]/lightning-formatted-text");
        By lnkImpDates = By.XPath("//flexipage-tab2[1]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar/ul/li[2]/a");
        By lnkEditDateEngL = By.XPath("//flexipage-tab2[2]/slot/flexipage-component2//flexipage-column2[1]//flexipage-field[1]/slot//dd/div[1]/button");
        By txtEstMktDate = By.XPath("//input[@name='Expected_In_Market_Date__c']");
        By valEstMktDate = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.Expected_In_Market_Date__c']//lightning-formatted-text");
        By lnkAdmin = By.XPath("//flexipage-tab2[1]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar/ul/li[3]/a");
        By lnkEditAccStatus = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[1]//flexipage-column2[1]/div/slot/flexipage-field[2]/slot//button/span[1]");
        By txtDealCloudID = By.XPath("//input[@name='Legacy_SLX_ID__c']");
        By valDealCloudIDPostUpdate = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2[1]//flexipage-column2[1]/div/slot/flexipage-field[5]/slot//lightning-formatted-text");
        By lnkEditIntDeal = By.XPath("//button[@title='Edit Internal Deal Announcement']");
        By btnIntDeal = By.XPath("//label[text()='Internal Deal Announcement']/ancestor::div[1]/div//button");
        By valIntDeal = By.XPath("//label[text()='Internal Deal Announcement']/ancestor::div[1]//lightning-base-combobox-item[3]/span[2]/span");
        By valIntDealPostUpdate = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.Internal_Announcement__c']//slot[1]/lightning-formatted-text");
        By lnkEditCST = By.XPath("//flexipage-tab2[5]/slot/flexipage-component2/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[2]/button");
        By btnCST = By.XPath("//button[@aria-label='CST Questionnaire, --None--']");
        By valCST = By.XPath("//flexipage-tab2[5]/slot/flexipage-component2/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/div/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[2]");
        By valCSTPostUpdate = By.XPath("//flexipage-tab2[5]/slot/flexipage-component2/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/lightning-formatted-text");
        By btnNewBilling = By.XPath("//span[text()='Billing Comments']/ancestor::div[4]/div[3]//button");
        By btnCloseBilling = By.XPath("//button[@title='Close error dialog']");
        By msgDate = By.XPath("//records-record-layout-item[2]/div/span/slot/lightning-input/lightning-datepicker/div[2]");
        By msgStatus = By.XPath("//records-form-picklist/lightning-picklist/lightning-combobox/div/div[2]");
        By msgComment = By.XPath("//records-record-layout-text-area/lightning-textarea/div[2]");
        By txtDate = By.XPath("//input[@name='Date__c']");
        By btnStatus = By.XPath("//label[text()='Status']/ancestor::div[1]/div//button");
        By valStatus = By.XPath("//lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[2]/span[2]/span");
        By txtBillingComment = By.XPath("//span/slot/records-record-layout-text-area/lightning-textarea/div[1]/textarea");
        By valBillingID = By.XPath("//h1//records-entity-label[text()='Billing Comment']/ancestor::h1/slot/lightning-formatted-text[1]");
        By btnEditBillingComment = By.XPath("//li[@data-target-selection-name='sfdc:StandardButton.Billing_Comment__c.Edit']");
        By btnEditComment = By.XPath("//a[@title='Edit']");
        By lnkAddedComment = By.XPath("//lightning-primitive-custom-cell/force-lookup/div/records-hoverable-link/div/a/slot/slot/span");
        By valEditComment = By.XPath("//records-record-layout-section[2]//slot/records-record-layout-item//lightning-formatted-text");

        By tabEng = By.XPath("//ul[2]/li[4]/a/span[2]");
        By tabExistingEng = By.XPath("//section/div/div/div/div/div/ul[2]/li[2]/a/span[2]");
        //By valRevAccrualL = By.XPath("//flexipage-tab2[6]//flexipage-tab2[1]/slot//span[2]");
        By valRevAccrualL = By.XPath("//tr[1]/td[1]//lst-formatted-text/span");
        By tabPositionL = By.XPath("//a[@title='BE Networks']");

        By btnDeleteComment = By.XPath("//li[@data-target-selection-name='sfdc:StandardButton.Billing_Comment__c.Delete']");
        By btnConfirmDelete = By.XPath("//span[text()='Delete']");
        By secDocChecklist = By.XPath("//span[@title='Document Checklist']");
        By tabFees = By.XPath("//a[@data-label='Fees & Financials']");
        By lnkEditCurrency = By.XPath("//button[@title='Edit Currency']");
        By txtEBITDA = By.XPath("//input[@name='EBITDA_MM__c']");
        By valEBITDA = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.EBITDA_MM__c']/div//lightning-formatted-text");
        By tabClientSub = By.XPath("//a[text()='Client/Subject & Referral']");
        By lnkRefType = By.XPath("//button[@title='Edit Referral Type']");
        By txtEstRefFee = By.XPath("//flexipage-tab2[6]//flexipage-column2[1]/div/slot/flexipage-field[3]//lightning-primitive-input-simple//input");
        By valEstFee = By.XPath("//flexipage-tab2[6]//flexipage-field[3]//slot[1]/lightning-formatted-text");
        By btnShowMore = By.XPath("//tr[1]/td[8]/lightning-primitive-cell-factory/span//lst-list-view-row-level-action");
        By btnEditClient = By.XPath("//body/div[8]//a");
        By btnTypeClient = By.XPath("//label[text()='Type']/ancestor::div[1]/div[1]//button[1]");
        By valUpdatedType = By.XPath("//tbody/tr[1]/td[2]/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/lst-formatted-text");
        By btnCloseMsg = By.XPath("//button[@title='Close error dialog']");
        By tabRevenue = By.XPath("//li[7]/a[@data-label='Revenue']");
        By tabRevenueL = By.XPath("//a[@aria-controls='tab-13']");
        By subtabContracts = By.XPath("//a[@data-label='Contracts']");
        By valContractNumberL = By.XPath("//flexipage-tab2[7]//flexipage-tab2[4]//tr[1]/td[1]//span//span");
        By valContractNameL = By.XPath("//flexipage-tab2[7]//flexipage-tab2[4]//tr[2]/th[1]//span//records-hoverable-link//a//span//span/slot");
        By lnkContractL = By.XPath("//flexipage-tab2[7]//flexipage-tab2[4]//tr[2]/th[1]//span//records-hoverable-link");
        By valIsMainCheckboxL = By.XPath("//records-record-layout-row[11]//slot[1]/lightning-input//label/span[1]");
        By tabCompliance = By.XPath("//a[text()='Compliance & Legal']");
        By subTabCompliance = By.XPath("//a[text()='Compliance']");
        By subTabLegal = By.XPath("//a[text()='Legal Matters']");
        By subTabCC = By.XPath("//a[text()='Conflict Check']");
        By btnAddRevenue = By.XPath("// button[text()='Add Accrual']");
        By btnSaveRevenue = By.XPath("//footer/button[2]/span");
        By valRevAccID = By.XPath("//span[@title='(1)']");
        By btnShowMoreRev = By.XPath("//section[2]//td[10]//lightning-button-menu/button");
        By lnkViewAllRev = By.XPath("//lst-dynamic-related-list//lst-related-list-view-manager/a/span[text()='View All']");
        By lnkRevYearL = By.XPath("//tr/th[@data-col-key-value='Year_Month__c-formulaOutputFormulaHtml-2']//a[2]");

        By btnEditRevenue = By.XPath("/html/body/div[9]/div/ul/li/a");
        By txtPeriodAccural = By.XPath("//input[@name='Period_Accrued_Fees__c']");
        By valPeriodAccural = By.XPath("//table/tbody/tr/td[3]/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/lst-formatted-text/span");
        By tabRevProj = By.XPath("//a[text()='Revenue Projection']");
        By titleRevProj = By.XPath("//span[text()='Revenue Projections']");
        By btnEditRevProj = By.XPath("//button[text()='Update Revenue Projection']");
        By txtProjMonFee = By.XPath("//tr[1]/td[3]/lightning-input/lightning-primitive-input-simple/div/div/input");
        By btnSaveRevproj = By.XPath("//button[@title='Save']");
        By valRevProj = By.XPath("//tr/td[2]/lightning-primitive-cell-factory/span/div/lightning-formatted-number");
        By lnkClearRevProj = By.XPath("//table/tbody/tr[1]/td[7]/a");
        By msgNoRec = By.XPath("//p[text()='No Records To Display']");
        By btnStartingMonth = By.XPath("//lightning-layout-item[1]/slot/lightning-combobox/div/lightning-base-combobox/div/div[1]/button");
        By valStartingMonth = By.XPath("//lightning-base-combobox-item[5]/span[2]/span");
        By valStartingMonthDisplayed = By.XPath("//section/div/div/div[2]/table/tbody/tr[1]/td[1]");
        By btnSubmitRevProj = By.XPath("//button[text()='submit']");
        By btnReturnToEng = By.XPath("//button[text()='Return to Engagement']");
        By lnkEditBeneficial = By.XPath("//button[@title='Edit Beneficial Owner & Control Person form?']");
        By btnBeneficial = By.XPath("//label[text()='Beneficial Owner & Control Person form?']/parent::div//button");//button[@aria-label='Beneficial Owner & Control Person form?, Yes']");
        By txtComplianceDate = By.XPath("//input[@name='Received_by_Compliance_Date__c']");
        By valBeneficial = By.XPath("//flexipage-field[@data-field-id='RecordBeneficial_Owner_Control_Person_form_cField1']/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/lightning-formatted-text");
        By valComplianceDate = By.XPath("//flexipage-tab2[6]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/slot/slot/flexipage-tab2[1]/slot/flexipage-component2/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[2]/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/lightning-formatted-text");
        By lnkEditConfAgree = By.XPath("//button[@title='Edit Confidentiality Agreement']");
        By txtDateSigned = By.XPath("//input[@name='Date_CA_Signed__c']");
        By valDateSigned = By.XPath("//flexipage-tab2[6]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/slot/slot/flexipage-tab2[2]/slot/flexipage-component2/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/lightning-formatted-text");
        By tabComments = By.XPath("//forcegenerated-flexipage_engagement_record_page_hlbanker_cf_engagement__c__view_js/record_flexipage-desktop-record-page-decorator/div[1]/records-record-layout-event-broker/slot/slot/flexipage-record-home-template-desktop2/div/div[2]/div[2]/slot/flexipage-component2[2]/slot/flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar/ul/li[1]/a");
        By tabFinancials = By.XPath("//forcegenerated-flexipage_engagement_record_page_hlbanker_cf_engagement__c__view_js/record_flexipage-desktop-record-page-decorator/div[1]/records-record-layout-event-broker/slot/slot/flexipage-record-home-template-desktop2/div/div[2]/div[2]/slot/flexipage-component2[2]/slot/flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar/ul/li[2]/a");
        By tabEngContacts = By.XPath("//forcegenerated-flexipage_engagement_record_page_hlbanker_cf_engagement__c__view_js/record_flexipage-desktop-record-page-decorator/div[1]/records-record-layout-event-broker/slot/slot/flexipage-record-home-template-desktop2/div/div[2]/div[2]/slot/flexipage-component2[2]/slot/flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar/ul/li[3]/a");
        
        By tabCST = By.XPath("//forcegenerated-flexipage_engagement_record_page_hlbanker_cf_engagement__c__view_js/record_flexipage-desktop-record-page-decorator/div[1]/records-record-layout-event-broker/slot/slot/flexipage-record-home-template-desktop2/div/div[2]/div[2]/slot/flexipage-component2[2]/slot/flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar/ul/li[4]/a");
        By btnView = By.XPath("//lst-list-view-row-level-action/lightning-button-menu/button");
        By btnViewDel = By.XPath("//force-aura-action-wrapper/div/ul/li/div/div/div/div/a");
        By lnkEditComments = By.XPath("//button[text()='Change Owner']/ancestor::ul/li[1]//button");
        By btnEditEngComment = By.XPath("//table/tbody/tr/td[8]/span/div/a");
        By txtEditComment = By.XPath("//textarea");
        By lnkDeleteComment = By.XPath("//button[text()='Change Owner']/ancestor::ul/li[2]//button");
        By msgDeleteComment = By.XPath("//span[text()='Comments']/ancestor::a/span[2]");
        By lnkComments = By.XPath("//slot/lst-dynamic-related-list-with-user-prefs//ul/li/lightning-button-menu/button");
        By lnkNewComment = By.XPath("//span[text()='New']");
        By btnComments = By.XPath("//button[@data-value='Internal']");
        By valCommentsType = By.XPath("//lightning-combobox/div/div/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[2]/span[2]/span");
        By txtCommentNotes = By.XPath("//textarea");
        By btnSaveComments = By.XPath("//c-engagement-comments/lightning-card/article/div[2]/slot/lightning-layout/slot/lightning-layout-item/slot/lightning-record-edit-form/lightning-record-edit-form-create/form/slot/slot/div/div[4]/div/lightning-button/button");
        By valAddedCommentType = By.XPath("//dt[text()='Comment Type:']/ancestor::dl/dd[2]/lst-template-list-field/lst-formatted-text");
        By valAddedComment = By.XPath("//flexipage-component2[1]/slot//records-record-layout-section[2]//slot[1]/lightning-formatted-text");
        //By tabFinancials = By.XPath("//section[2]/div/div/section/div/div[2]/div[1]/div[1]/div/div/div/one-record-home-flexipage2/forcegenerated-adg-rollup_component___force-generated__flexipage_-record-page___-engagement_-record_-page_-h-l-banker_-c-f___-engagement__c___-v-i-e-w/forcegenerated-flexipage_engagement_record_page_hlbanker_cf_engagement__c__view_js/record_flexipage-desktop-record-page-decorator/div[1]/records-record-layout-event-broker/slot/slot/flexipage-record-home-template-desktop2/div/div[2]/div[2]/slot/flexipage-component2[2]/slot/flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar/ul/li[2]/a");
        By btnFinancials = By.XPath("//lst-dynamic-related-list-with-user-prefs/lst-related-list-view-manager/lst-common-list-internal/lst-list-view-manager-header/div/div[1]/div[3]/div/runtime_platform_actions-actions-ribbon/ul/li/lightning-button-menu/button");
        By lnkAddFinancials = By.XPath("//span[text()='New Financials']");
        By txtRelatedEng = By.XPath("//input[@placeholder='Search Engagements...']");
        By valRelatedEng = By.XPath("//lightning-grouped-combobox/div[1]/div/lightning-base-combobox/div/div/div[2]/ul[1]/li[2]");
        By valFinancials = By.XPath("//records-entity-label[text()='Engagement Financials']/ancestor::h1/slot[1]/lightning-formatted-text");
        By tabEngagementNumL = By.XPath("//section[1]/div/div/div/div/div/ul[2]/li[2]/a/span[2]");
        By lnkEngName = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Revenue_Accrual__c.Engagement__c']/div/dd//span//records-hoverable-link");

        //By tabFinancials = By.XPath("//section[2]/div/div/section/div/div[2]/div[1]/div[1]/div/div/div/one-record-home-flexipage2/forcegenerated-adg-rollup_component___force-generated__flexipage_-record-page___-engagement_-record_-page_-h-l-banker_-c-f___-engagement__c___-v-i-e-w/forcegenerated-flexipage_engagement_record_page_hlbanker_cf_engagement__c__view_js/record_flexipage-desktop-record-page-decorator/div[1]/records-record-layout-event-broker/slot/slot/flexipage-record-home-template-desktop2/div/div[2]/div[2]/slot/flexipage-component2[2]/slot/flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar/ul/li[2]/a");
        By btnEngContact = By.XPath("//article/lst-related-list-view-manager/lst-common-list-internal//lst-template-list-field/lst-list-view-row-level-action/lightning-button-menu/button");

        By btnClearContact = By.XPath("//records-record-layout-lookup/lightning-lookup/lightning-lookup-desktop/lightning-grouped-combobox/div/div/lightning-base-combobox/div/div/div[1]/div/button/lightning-primitive-icon");
        By btnCloseEngContact = By.XPath("//records-record-edit-error-header/lightning-button-icon");
        By txtEngContact = By.XPath("//input[@placeholder='Search Contacts...']");
        By btnParty = By.XPath("//label[text()='Type']/ancestor::div[1]/following::button[2]");
        By btnUpdateBacklog = By.XPath("//button[@name='Engagement__c.Update_Engagement']");
        By txtEngNameBacklog = By.XPath("//div[2]/div/div/div/input");
        By btnSaveBacklog = By.XPath("//footer/button[2]/span");
        By msgEngNameBacklog = By.XPath("//section/div/div[2]/div/ul/li");
        By valEngNameL = By.XPath("//span[text()='Engagement Name']/ancestor::record_flexipage-record-field/div[1]/div[1]//div[1]/span/slot[1]/lightning-formatted-text");
        By valEngNumL = By.XPath("//div/span[text()='Engagement Name']/ancestor::div[2]//slot/lightning-formatted-text");
        By btnAddClientL = By.XPath("//button[@name='Engagement__c.Add_Client_L']");
        By txtCompanies = By.XPath("//input[@placeholder='Search Companies...']");
        By valCompanyName = By.XPath("//tbody/tr[3]/th/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/force-lookup/div/records-hoverable-link/div/a/slot/slot/span");
        By btnAddSubjectL = By.XPath("//button[@name='Engagement__c.Add_Subject_L']");
        By valSubjectComp = By.XPath("//tbody/tr[4]/th/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/force-lookup/div/records-hoverable-link/div/a/slot/slot/span");
        By valSubjectType = By.XPath("//table/tbody/tr[4]/td[2]/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/lst-formatted-text");
        By valClientType = By.XPath("//table/tbody/tr[3]/td[2]/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/lst-formatted-text");
        By btnAddOtherL = By.XPath("//button[@name='Engagement__c.Add_Other_Party_L']");
        By valOtherComp = By.XPath("//tbody/tr[5]/th/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/force-lookup/div/records-hoverable-link/div/a/slot/slot/span");
        By valOtherType = By.XPath("//table/tbody/tr[5]/td[2]/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/lst-formatted-text");
        By btnAddEngContact = By.XPath("//button[@name='Engagement__c.Add_CF_Engagement_Contact']");
        By txtContacts = By.XPath("//input[@placeholder='Search Contacts...']");
        By btnPartyContact = By.XPath("//div[4]/div[1]/div/div/div/div/div[1]/div/div/a");
        By valAddedContact = By.XPath("//lst-template-list-item-factory[1]/lst-related-preview-card/article/div/div[1]/h3/lst-template-list-field/formula-output-formula-html/lightning-formatted-rich-text/span/a[2]");

        
        By valAddedContactNum = By.XPath("//flexipage-tab2[3]/slot/flexipage-component2/slot/lst-related-list-single-container/laf-progressive-container/slot/lst-related-list-single-app-builder-mapper/article/lst-related-list-view-manager/lst-common-list-internal/lst-list-view-manager-header/div/div[1]/div[1]/div/div/h2/a/span[2]");
        By btnBillingRequestL = By.XPath("//button[text()='Billing Request']");
        By secAdditionalCC = By.XPath("//div[1]/div/div/div/div[2]/div[2]/div[1]/h3");
        By btnSendEmailL = By.XPath("//div[1]/table/tbody/tr/td[2]/input[1]");
        By msgSendEmail = By.XPath("//table/tbody/tr[1]/td[2]/div");

        By btnCancelSendEmail = By.XPath("//div/div[1]/table/tbody/tr/td[2]/input[2]");
        By txtTo = By.XPath("//input[@name='j_id0:j_id58:pbSendEmail:pbsMain:j_id60:inputToId']");
        By tabMore = By.XPath("//div[1]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar/ul/li[9]/lightning-button-menu");
        By lblBid = By.XPath("//span[text()='Bids']");
        By lblReport = By.XPath("//span[text()='Report']");
        By lblBidAdmin = By.XPath("//flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar/ul/li[8]/a");
        By tblBids = By.XPath("//div/slot/lightning-tab/slot/lightning-card/article/div[2]/slot/div/lightning-datatable/div[2]/div/div/table");
        By btnNewBid = By.XPath("//button[text()='New Bid Round']");
        By btnSelectNewRound = By.XPath("//button[@aria-label='Select New Round, Select New Round']");
        By tabAddedBid = By.XPath("//a[text()='Round First']");
        By lnkEditMinBid = By.XPath("//table/tbody/tr[1]/td[2]/lightning-primitive-cell-factory/span/button");
        By lnkEditMaxBid = By.XPath("//table/tbody/tr[1]/td[3]/lightning-primitive-cell-factory/span/button");
        By txtMinBid = By.XPath("//input[@name='dt-inline-edit-currency']");
        By txtBidDate = By.XPath("//input[@name='dt-inline-edit-dateLocal']");
        By btnSaveMinBid = By.XPath("//lightning-primitive-datatable-status-bar/div/div/button[2]");
        By btnMsgBid = By.XPath("//button[@class='slds-button slds-button_icon slds-button_icon-error']");
        By msgMinBid = By.XPath("//lightning-primitive-datatable-tooltip-bubble/section/div/ul/li[1]");
        By msgMaxBid = By.XPath("//lightning-primitive-datatable-tooltip-bubble/section/div/ul/li[2]");
        By lblMinBid = By.XPath("//span[@title='Min Bid']");
        By lnkEditBidDate = By.XPath("//table/tbody/tr[1]/td[6]/lightning-primitive-cell-factory/span/button");
        By msgBidDate = By.XPath("//lightning-primitive-datatable-tooltip-bubble/section/div/ul/li[3]");
        By btnManage = By.XPath("//button[text()='Manage']");
        By valMinBid = By.XPath("//tr[1]/td[2]/lightning-primitive-cell-factory/span/div/lightning-formatted-number");
        By btnMoreAdmin = By.XPath("//slot/flexipage-component2[1]/slot/flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar/ul/li[9]/lightning-button-menu/button");
        By tabBidAdmin = By.XPath("//span[text()='Bids']");
        By lnkEngAR = By.XPath("//span/a[text()='Engagement AR Receipt']");
        By lnkEngExp = By.XPath("//span/a[text()='Engagement Expenses']");
        By lnkEngInvoice = By.XPath("//span/a[text()='Engagement Invoice Details']");
        By lblEngAR = By.XPath("//h1/span[2]");
        By btnEngReport = By.XPath("//div[@class='pbHeader']/table/tbody/tr/td[2]/input[@name='engagement_reports']");
        By titleEngReport = By.XPath("//table/tbody/tr[1]/th[1]");
        By lnkEngWorking = By.XPath("//table/tbody/tr/td/a[text()='Engagement Working Group List']");
        By lblEngWorking = By.XPath("//div[@class='pbTitle']/h1");
        By btnMoreEng = By.XPath("//runtime_platform_actions-actions-ribbon/ul/li[11]/lightning-button-menu/button");
        By lnkEngReports = By.XPath("//span[text()='Engagement Reports']");
        By tblReports = By.XPath("//div[@class='pbBody']/div[3]/table/tbody/tr/td[1]/a");
        By btnReturnToEngLightning = By.XPath("//input[@value='Return to Engagement']");
        By valRelatedOppL = By.XPath("//span[text()='Related Opportunity']/ancestor::dt/following::dd[1]//a//span[1]/slot/span");
        By btnPortfolioValL = By.XPath("//section[2]/div/div[2]//div//runtime_platform_actions-actions-ribbon/ul/li/runtime_platform_actions-action-renderer//lightning-button/button[text()='Portfolio Valuation']");
        By btnNewOppValPeriodL = By.XPath("//input[@value='New Opportunity Valuation Period']");
        By btnNewEngValPeriodL = By.XPath("//input[@value='New Engagement Valuation Period']");
        By btnEngReportsL = By.XPath("//button[text()='Engagement Reports']");

        By txtEngStageL = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.Stage__c']/div//dd/div/span/slot/lightning-formatted-text");//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.Stage__c']/div/div/span/slot/lightning-formatted-text");
        By txtEngRecordTypeL = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.RecordTypeId']//div[contains(@class,'recordType')]/div/span");
        By tabEngAdministratorL = By.XPath("//h1/div[contains(@class,'entityNameTitle')]//records-entity-label[text()='Engagement']//ancestor::flexipage-record-home-template-desktop2//following::flexipage-component2//li[@title='Administration']/a");//div[text()='Engagement']/ancestor::div/following::flexipage-component2//li[@title='Administration']/a");// (//li/a[@data-label='Administration'])[2]");
        By txtEngLegalEntityL = By.XPath("//div[contains(@data-target-selection-name,'Engagement__c.Legal_Entity')]//a//span/slot/span/slot");//div[contains(@data-target-selection-name,'Engagement__c.Legal_Entity')]//a//span");
        By tabEngImpDate = By.XPath("//li/a[@data-label='Important Dates']");
        By txtStartDate = By.XPath("//div[contains(@data-target-selection-name,'Engagement__c.Start_Date')]//span[contains(@class,'field-value')]//lightning-formatted-text");

        By valEngInternalMember = By.CssSelector("[action*='HL_EngagementInternalTeamView'] table tbody tr.dataRow.even.first.last label");
        //By valEngContact = By.CssSelector("div[id*='D7QcI_body'] table th a:nth-child(2)");
        By valEngContact = By.XPath("//h3/lst-template-list-field/formula-output-formula-html/lightning-formatted-rich-text/span/a[2]");

        By iconExpandMoreButon = By.XPath("(//lightning-button-menu//button[contains(@class,'slds-button_icon-border-filled')])[1]");
        By iconCSTTabInline = By.XPath("(//span[text()='CST Questionnaire']/following::div/button)[1]");
        By DropdownIconCST = By.XPath("//button[contains(@aria-label,'CST Questionnaire')]");
        By drodownCSTOptions = By.XPath("//flexipage-field[@data-field-id='RecordCST_Questionnaire_cField']//lightning-base-combobox-item//span[@class='slds-truncate']");
        By btnSaveCSTQuestionnaire = By.XPath("//button[@name='SaveEdit']");
        By btnCancelCSTQuestionnaire = By.XPath("//button[@name='CancelEdit']");
        By valueYesSaved = By.XPath("//lightning-formatted-text[normalize-space()='Yes']");
        By valueNoSaved = By.XPath("//lightning-formatted-text[normalize-space()='No']");
        By reasonNoOption = By.XPath("//input[@name='Reason_Why_CST_is_Not_Being_Used__c']");
        By buttonContinue = By.XPath("//button[normalize-space()='Continue']");
        By headerPageInfo = By.XPath("//div[normalize-space()='Questionnaire']");
        By tabCSTForm = By.CssSelector("a[role='tab'] span[title='Questionnaire  c']");
        By questionnaireNumber = By.XPath("//p[@title='Questionnaire Number']//following-sibling::p//lightning-formatted-text");
        By warningMeetingSelection = By.XPath("//div[contains(text(),'Please select Meeting type')]");
        By meetingTypes = By.XPath("//div[@class='slds-form-element__control']//span[@class='slds-radio']//span[@class='slds-form-element__label']");
        By iconClosePopUp = By.XPath("//button[@title='Close this window']");
        By searchBox = By.XPath("//lightning-input[@class='slds-form-element']");
        By selectItem = By.CssSelector("table[class*='slds-table'] tbody tr th a");
        By linkFirstCase = By.XPath("//table[contains(@class,'slds-table')]//tbody//tr[1]//th//a");
        By LinkQuestionnaire = By.XPath("//table[@aria-label='Questionnaires']//tbody//tr[1]//th[1]//a//span");
        By lvCSTformWarningmessage = By.XPath("//h2[@title='We hit a snag.']");
        By iconInlineEdit = By.XPath("//button[@title='Edit Preferred City']");
        By iconInlineEditAssignTo = By.XPath("//button[@title='Edit Assigned To Coordinator']");
        By fieldAssignTo = By.XPath("//records-record-layout-item[@field-label='Assigned To Coordinator']//input");
        By iconClearAssignToSelection = By.XPath("//records-record-layout-item[@field-label='Assigned To Coordinator']//input/following::button[@title='Clear Selection'][1]");
        By tabSection = By.XPath("//span[@title='Details']");
        By btnFormCancel = By.XPath("//button[@name='CancelEdit']");
        By btnFormSave = By.XPath("//button[@name='SaveEdit']");
        By listRequiredFields = By.XPath("//records-record-edit-error//ul//li/a");
        By txtValidation = By.XPath("//records-record-edit-error//ul//li");
        //formRequiredFields
        By fieldExpectedBidDate = By.XPath("//input[@name='What_is_the_expected_bid_date__c']");
        By dropdownExpectedBidDateSolidified = By.XPath("//button[contains(@aria-label,'Expected Meeting Dates Solidified')]");
        By optionYesExpectedBidDateSolidified = By.XPath("//lightning-base-combobox-item[2]");
        By optionNoExpectedBidDateSolidified = By.XPath("//lightning-base-combobox-item[3]");
        By fieldNumberOfExpectedMeetingType = By.XPath("//input[@name='of_expected_meetings_of_this_type__c']");
        By fieldProvideStartDates = By.XPath("//input[@name='Provide_Dates_To__c']");
        By fieldProvideEndDates = By.XPath("//input[@name='Provide_Dates_From__c']");
        By fieldPreferredCity = By.XPath("//input[@name='Preferred_City__c']");
        By fieldExpectedMeetingStartDate = By.XPath("//input[@name='Expected_Meeting_Dates__c']");
        By fieldExpectedMeetingEndDate = By.XPath("//input[@name='Expected_Meeting_Dates_From__c']");
        By caseSavedStatus = By.XPath("//p[@title='Status']//following-sibling::p//lightning-formatted-text");
        By editStartDate = By.XPath("//button[@title='Edit Provide Start Dates']");
        By fieldInvitation = By.XPath("(//span[@class='test-id__field-label'])[contains(text(),'Would you like CST to send invitations?')]");
        By dropdownIconInvitation = By.XPath("//button[contains(@aria-label,'Would you like CST to send invitations?')]");
        By optionsInvitation = By.XPath("//flexipage-field[contains(@data-field-id,'CST_to_send_invitations')]//lightning-base-combobox-item//span[@class='slds-truncate']");
        By fieldInvitationPreference = By.XPath("(//span[@class='test-id__field-label'])[contains(text(),'Outlook Invitation Preference')]");
        By dropdownIconInvitationPreference = By.XPath("//button[contains(@aria-label,'Invitation Preference')]");
        By optionsInvitationPreference = By.XPath("//flexipage-field[contains(@data-field-id,'Outlook_Invitation_Preference')]//lightning-base-combobox-item//span[@class='slds-truncate']");
        By tabOutlookInvitations = By.XPath("(//span[contains(text(),'Outlook Invitations')])[4]");
        By autoDropdownDealTeamContacts = By.XPath("(//input[contains(@placeholder,'Select Contacts')])[1]");
        By autoDropdownDealTeamMembers = By.XPath("(//input[contains(@placeholder,'Select Contacts')])[2]");
        By autoDropdownClients = By.XPath("//input[contains(@placeholder,'Select Clients')]");
        By autoDropdownDealTeamCounterparties = By.XPath("//input[contains(@placeholder,'Select Buyers')]");
        By lablesOutlookInvitationFields = By.XPath("(//lightning-layout-item[@class='slds-size_4-of-12'])");
        By linkInternalTeam = By.CssSelector("lightning-tab-bar a[data-label='Internal Team']");
        By InternalTeamList = By.XPath("//div[@class='pbBody']//table");
        By dropdownOption = By.XPath("//div[@class='slds-combobox slds-dropdown-trigger slds-dropdown-trigger_click slds-is-open']//ul[@role='presentation']//li[1]");
        By btnOutlookInvitationFieldsSave = By.XPath("//button[@type='button'][text()='Save']");
        By toastMsgPopup = By.XPath("//span[contains(@class,'toastMessage')]");
        By toastMsgCloseIcon = By.XPath("//button[@title='Close']");
        By optionsSelected = By.XPath("//button[contains(@title,'Remove selected option')][@type='button']");
        By btnSubmitQuestionnaire = By.XPath("//button[@name='Questionnaire__c.Submit']");
        By btnSaveConfirmSubmit = By.XPath("//footer//button//span[text()='Save']");
        By caseNumber = By.XPath("//p[@title='Case']//following-sibling::p//a//span");
        By textCaseDetailEngagementName = By.XPath("//p[@title='Engagement']//following-sibling::p//a//span");
        By textCaseDetailEngagementNumber = By.XPath("(//p[@title='Engagement Number']//following-sibling::p//lightning-formatted-text)[2]");
        By textCaseDetailsMeetingType = By.XPath("//p[@title='Meeting Type']//following-sibling::p//slot//lightning-formatted-text");
        By imgProfile = By.CssSelector("div[class*='profileTrigger ']>span[class='uiImage']");
        By lnkSwitchToClassic = By.XPath("//a[text()='Switch to Salesforce Classic']");
        By linksRelatedList = By.XPath("//flexipage-component2[@data-component-id='force_relatedListQuickLinksContainer']//ul[@class='slds-grid slds-wrap list']//a//span");
        By btnAddMeeting = By.XPath("//button[text()='Add Meeting']");
        By btnAccept = By.XPath("//button[@name='MassAccept']");
        By btnAccural = By.XPath("//button[text()='Add Accrual']");
        By headerNewMeeting = By.XPath("//h2[text()='New Meeting']");
        By searchCounterparty = By.XPath("//input[@placeholder='Search Engagement Counterparties...']");
        By optionAddNewCounterparty = By.XPath("//input[@placeholder='Search Engagement Counterparties...']//following::div//span[@title='New Engagement Counterparty']");
        By btnNextAddNewCounterparty = By.XPath("(//div[@class='forceChangeRecordTypeFooter']//button)[2]");
        By searchCompany = By.XPath("//input[@title='Search Companies']");
        By searchEngagement = By.XPath("//input[@title='Search Engagements']");
        By optionNewCompany = By.XPath("//div[contains(@class,'createNew')]//span");
        By fieldCompanyName = By.XPath("//label//span[text()='Company Name']//following::Input[@aria-required='true']");
        By btnAddCounterpartyRequiredItem = By.XPath("(//button[@title='Save'])[2]");
        By comboDropdownResult = By.XPath("(//ul[@role='group']//li)[1]");
        By dropdownist = By.XPath("//button[contains(@title,'Select a List View')]");
        By searchList = By.XPath("//div[@role='dialog']//input");
        By listOption = By.XPath("//div[@role='dialog']//div//ul//li");
        By caseLink = By.XPath("//table//tbody//tr[1]//th//a");
        By fieldCaseOwner = By.XPath("//span[contains(@class,'owner-name')]//a//span");
        By panelCST = By.XPath("//a[@data-label='CST']");
        By panelCSTQuestionnaires = By.XPath("//a//span[@title='Questionnaires']");
        By PanelCSTFiles = By.XPath("//a//span[@title='Files']");
        By questionnaireCST = By.XPath("//table[@aria-label='Questionnaires']//tbody//tr//button//span[text()='Show Actions']//parent::button");//div[@aria-label='Questionnaires|Questionnaires|List View']//tbody//tr//a[@role='button']");
        By btnRowAction = By.XPath("//table[@aria-label='All Open Cases']//tbody//tr//a[@role='button']");
        By linkDelete = By.XPath("//a[@title='Delete']");
        By buttonDelete = By.XPath("//button[@title='Delete']");
        By tabDropdown = By.XPath("//div[@title='Actions for Questionnaires']");
        By tabRefresh = By.XPath("//ul//li[@title='Refresh Tab']//a");
        By sectionCaseComments = By.XPath("//span[@title='Case Comments']");
        By savedComments = By.XPath("//div[@title='Comment:']//following::div[contains(@class,'slds-truncate')]//span");
        By iconCommentsAction = By.XPath("//span[@title='Case Comments']//following::a[@role='button'][1]");
        By iconNotesAction = By.XPath("//span[@title='Notes']//following::a[@role='button'][1]");
        By tabRelated = By.XPath("//li[@class='slds-tabs_default__item']//a[@data-label='Related']");
        By linkNew = By.XPath("//a[@title='New']");
        By textArea = By.XPath("//textarea[@role='textbox']");
        By txtNotesTitle = By.XPath("//div[contains(@class,'notesEditPanel')]//input");
        By txtNotes = By.XPath("//div[@aria-label='Compose text']");
        By btnNotesDone = By.XPath("//div[contains(@class,'bottomBar')]//button[text()='Done']");
        //Edit CST Meeting Page
        By iconMeetingInlineEdit = By.XPath("(//button[@title='Edit Preferred City'])[2]");
        By iconDropownVenueType = By.XPath("//button[contains(@aria-label,'Venue Type')]");
        By optionsVanueType = By.XPath("//flexipage-field[contains(@data-field-id,'Venue_Type')]//lightning-base-combobox-item//span[@class='slds-truncate']");
        By txtFieldSearchCompany = By.XPath("//input[contains(@placeholder,'Search Companies')]");
        By btnConfirmEditMeetingPage = By.XPath("//div[contains(@class,'modal-footer')]//button[@name='continue']");
        By venueName = By.XPath("//flexipage-field[contains(@data-field-id,'Name')]//a//span");
        By venueLocation = By.XPath("//flexipage-field[contains(@data-field-id,'Location')]//lightning-formatted-text");
        By Phone = By.XPath("//flexipage-field[contains(@data-field-id,'Phone')]//lightning-formatted-text");
        By Website = By.XPath("//flexipage-field[contains(@data-field-id,'Website')]//a");
        By txtEngNumberL = By.XPath("//span[contains(@class,'field-label')][normalize-space()='Engagement Number']/ancestor::dt/following-sibling::dd//lightning-formatted-text");//::dl//dd//lightning-formatted-text");//span[contains(@class,'field-label')][normalize-space()='Engagement Number']/parent::div/following-sibling::div//lightning-formatted-text");
        By txtEngNameL = By.XPath("//span[contains(@class,'field-label')][normalize-space()='Engagement Name']/ancestor::dt/following-sibling::dd//lightning-formatted-text");//::dl//dd//lightning-formatted-text");//span[@class='test-id__field-label'][normalize-space()='Engagement Name']/parent::div/following-sibling::div//lightning-formatted-text");
        By listStaff = By.XPath("/html/body/ul");
        By txtStaff = By.CssSelector("input[placeholder*='Begin Typing Name']");
        By tabEngInternalTeamL = By.XPath("(//lightning-tab-bar/ul/li/a[text()='Internal Team'])[2]");
        By checkCFSpeciality = By.CssSelector("input[name*='internalTeam:j_id64:6:j_id66']");
        By checkSpeciality = By.CssSelector("input[name*='internalTeam:j_id64:7:j_id66");
        By btnSaveITTeam = By.CssSelector("input[name*=':bottom:j_id121']");
        By linkHLInternalTeam = By.XPath("//a//span[@id='internalTeamList_link']");
        By frameInternalTeam = By.XPath("(//iframe[@title='HL_EngagementInternalTeamView'])");
        By btnEngModifyRoles = By.XPath("(//div[contains(@class,'Custom')]//table//a[text()='Modify Roles'])[1]");
        By btnModifyRolesL = By.XPath("//div[1]/table/tbody/tr/td[2]/a");

        By frameInternalTeamDetailPage = By.XPath("//iframe[@title='accessibility title']");
        By frameInternalTeamModifyPage = By.XPath("//article/div[2]/div/iframe");
        By btnEditL = By.XPath("//li[contains(@data-target-selection-name,'Engagement__c.Edit')]");
        By comboClientOwnershipL = By.XPath("//button[contains(@aria-label,'Client Ownership')]");

        //records-lwc-highlights-panel/records-lwc-record-layout/forcegenerated-highlightspanel_opportunity__c___012i0000000tpyfaau___compact___view___recordlayout2/records-highlights2/div[1]/div[1]/div[3]/div/runtime_platform_actions-actions-ribbon/ul/li[1]/runtime_platform_actions-action-renderer/runtime_platform_actions-executor-page-reference/slot/slot/lightning-button/button");
        By txtAssociatedEngLabelL = By.XPath("//span[text()='Associated Engagement']");
        By editAssociatedEngFieldL = By.XPath("//flexipage-field[contains(@data-field-id,'Associated_Engagement')]//input");
        By txtAssociatedEngL = By.XPath("(//flexipage-field[contains(@data-field-id,'Associated_Engagement')]//a//span/slot)[2]");
        By btnCancelEditFormL = By.XPath("//button[@name='CancelEdit']");
        By btnSaveDetailsL = By.XPath("//button[@name='SaveEdit']");
        By btnEditEngL = By.XPath("//ul//li[contains(@data-target-selection-name,'Button.Engagement')]//button[@name='Edit']");
        By txtClientSubjectL = By.XPath("//input[@placeholder='Search Companies...']");
        By lnkEngClientSubL = By.XPath("//span[text()='Engagement']/ancestor::div/dd/div[1]//force-lookup//records-hoverable-link");

        By txtAssociatedEngLabel = By.XPath("//table[@class='detailList']//td[text()='Associated Engagement']");
        By editAssociatedEngField = By.XPath("//input[@name='CF00N6e00000MfcTw']");
        By txtAssociatedEng = By.XPath("//table[@class='detailList']//td[text()='Associated Engagement']//following::td//a[contains(@id,'MfcTw')]");
        By btnCancelEditForm = By.XPath("//td[@id='topButtonRow']//input[@name='cancel']");
        By txtPrivileges = By.XPath("//span[text()='Insufficient Privileges']");
        By iconClearAssociatedEngL = By.XPath("//flexipage-field[contains(@data-field-id,'Associated_Opportunity')]//div[contains(@class,'icon-group_right')]//button");
        By comboIGOptions = By.CssSelector("select[id*='6Ax'] option");
        By valEngERPLastIntStatus = By.CssSelector("div[id*='eeCj']");
        By btnViewCounterpartyL = By.XPath("//li[contains(@data-target-selection-name,'QuickAction.Engagement')]//button[contains(text(),'View Counterparties')]");
        By headerText = By.XPath("//h1//div[text()='Engagement']");
        By labelWomenLedCVAS = By.CssSelector("div:nth-child(29) > table > tbody > tr:nth-child(3) > td:nth-child(1)");
        By txtSecWomenLedCVAS = By.CssSelector("div[id*='5y_ep_j_id0_j_id4']>h3");
        By txtSecWomenLedTAS = By.CssSelector("div[id*='Kd_ep_j_id0_j_id4']>h3");
        By comboJobTypeptions = By.CssSelector("select[id*='65s'] option");// By.CssSelector("select[id*= '65s']");
        By txtEngDescL2 = By.XPath("//label[text()='Engagement Description']");
        By lblSICCode = By.XPath("//label[text()='SIC Code']");
        By internalTeamFrame = By.XPath("//iframe[contains(@src,'InternalTeamModifyView')]");
        By listInternalDealTeam = By.XPath("//span[contains(@id,'internalTeam:team')]//table//tbody//tr[@class='slds-hint-parent']");
        By btnReturnToOpporEng = By.XPath("//input[contains(@value,'Return To')]");
        By valEngInternalMemberMulti = By.XPath("//form[contains(@action,'HL_EngagementInternalTeamView')]//table[contains(@id,'HLInternalTeam')]//tbody//tr[1]//label");
        By textWomenLedLE = By.CssSelector("div:nth-child(35) > table > tbody > tr:nth-child(11) > td:nth-child(1)");
        By txtWomenLedSell = By.CssSelector("div[id*='pBs_ep_j_id0_j_id4']>h3");
        By tabEngActivity = By.XPath("//li[@title='Activity']//a[@id='flexipage_tab13__item']");
        By txtInternalTeamMemberL = By.XPath("//table[contains(@id,'HLInternalTeam')]//tbody/tr[1]/td[1]//label");
        By tabSidePanelL = By.XPath("//ul[@role='tablist']//li//a[@data-label='Eng Contacts']");
        By txtEngContactL = By.XPath("//article[@aria-label='Engagement Contacts']//h3//span");
        By tabInternalTeamL = By.XPath("//h1/div[contains(@class,'entityNameTitle')]//records-entity-label[text()='Engagement']//ancestor::flexipage-record-home-template-desktop2//following::flexipage-component2//li[@title='Internal Team']/a");//div[text()='Engagement']/ancestor::div/following::flexipage-component2//li[@title='Internal Team']/a");////div[@class='onePanelManagerScoped']//lightning-tab-bar/ul/li/a[text()='Internal Team']");
        By lblWomenLedL = By.XPath("//h3/button/span[@title='Administrative Info']/ancestor::h3/parent::div/laf-progressive-container//flexipage-field[contains(@data-field-id,'RecordWomen_Led')]//div[contains(@class,'field-label')]/span");
        By txtWomenLedL = By.XPath("//div[contains(@data-target-selection-name,'Women_Led')]//dd//span//slot/lightning-formatted-text");//div[contains(@data-target-selection-name,'Women_Led')]//dl//dd//span//slot/lightning-formatted-text");//div[contains(@data-target-selection-name,'Women_Led')]/div/div/span/slot/lightning-formatted-text");
        By linkRelatedOppL = By.XPath("//span[text()='Related Opportunity']/ancestor::dt/following::dd[1]//a/../..");//span[contains(@class,'field-label')][normalize-space()='Related Opportunity']/ancestor::dt/following-sibling::dd//lightning-formatted-text");//::dl//dd//records-hoverable-link//a//span");//span[contains(@class,'field-label')][normalize-space()='Related Opportunity']/parent::div/following-sibling::div//div/a//span");
        By btnEditSharingGroup = By.XPath("//div[contains(@class,'recordsRecordShare')]//button[text()='Edit']");
        By btnCancelSharingGroup = By.XPath("//div[contains(@class,'recordsRecordShare')]//button[text()='Cancel']");
        By tblSharingGroup = By.XPath("//div[contains(@class,'recordsRecordShare')]//table//tbody");
        By txtSharingUser = By.XPath("//div[contains(@class,'recordsRecordShare')]//table//tbody//tr//lightning-base-formatted-text[contains(text(),'james.craven')]");
        By txtValueWomenLedL = By.XPath("//h3/button/span[@title='Administrative Info']/ancestor::h3/parent::div/laf-progressive-container//flexipage-field[contains(@data-field-id,'RecordWomen_Led')]//div//span[contains(@class,'field-value')]/slot/lightning-formatted-text");////div[contains(@data-target-selection-name,'Engagement__c.Women_Led')]//div//span[contains(@class,'field-value')]");
        By btnSharingL = By.XPath("//button[contains(text(),'Sharing')]");
        By btnMoreSharingL = By.XPath("//span[contains(text(),'Sharing')]");
        By linkQuesionnaireNumber = By.XPath("//div[@aria-label='Questionnaires|Questionnaires|List View']//tbody//th//a");
        By txtMeetingType = By.XPath("//div[@aria-label='Questionnaires|Questionnaires|List View']//tbody//td[2]");
        By linkCaseNumber = By.XPath("//div[@aria-label='Questionnaires|Questionnaires|List View']//tbody//td[3]//a");
        By txtCaseStatus = By.XPath("//div[@aria-label='Questionnaires|Questionnaires|List View']//tbody//td[4]//span[@class='slds-truncate']");
        By txtPreviousJobType = By.XPath("(//flexipage-field[contains(@data-field-id,'RecordJob_Type')]//slot//lightning-formatted-text)[2]");
        By labelWomenLedSectionLV = By.XPath("//h3[contains(@data-target-reveals,'Engagement__c.Women_Led')]/button/span");
        By labelWomenLedLV = By.XPath("//flexipage-field[@data-field-id='RecordWomen_Led__cField']//label");
        By labelAdminSectionLV = By.XPath("//h3/button/span[@title='Administrative Info']");
        By headerEditBox = By.XPath("//h2[contains(text(),'Edit')]");
        By lblExpense = By.XPath("//span[text()='Expense']");
        By btnSaveL = By.XPath("//button[text()='Save']");
        By btnChangeRecordTypeL = By.XPath("//div[contains(@data-target-selection-name,'RecordType')]//dd//button[@title='Change Record Type']");
        By headerChangeRT = By.XPath("//h1[contains(text(),'Change')]");
        By valRecordTypeL = By.XPath("//div[contains(@data-target-selection-name,'RecordType')]//dd//div[contains(@class,'recordTypeName')]/span");
        By btnChangeRTNextL = By.XPath("//div[contains(@class,'ChangeRecordTypeFooter')]//button[2]");
        By valERPProductTypeL = By.XPath("//records-record-layout-item[@field-label='Product Type']//dd//lightning-formatted-text");
        By valERPProductTypCodeL = By.XPath("//records-record-layout-item[@field-label='ERP Product Type Code']//dd//lightning-formatted-text");
        By txtEstFee = By.XPath("//input[@name='Fee__c']");
        By btnClearHLSectionL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordIndustry_Sector')]//lightning-base-combobox//button");
        By inputHLSectorIDL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordIndustry_Sector')]//lightning-base-combobox//input");
        By listHLSectorL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordIndustry_Sector')]//div[@role='listbox']/ul/li[2]");
        By txtHLSectorIDL = By.XPath("//flexipage-field[contains(@data-field-id,'Industry_Sector_cField')]//records-hoverable-link//a//span");
        By txtHLSectorComboL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordSector_Combo_cField')]//dd//lightning-formatted-text");
        By iconInlinePrimaryOfficeL = By.XPath("//records-record-layout-item[@field-label='Primary Office']//dd//button");
        By lblSICL = By.XPath("//label[text()='SIC Code']");
        By comboPrimaryOfficeL = By.XPath("//label[text()='Primary Office']/parent::div//button");
        By btnJobTypeL = By.XPath("//label[text()='Job Type']/parent::div//button");
        By btnLOBL = By.XPath("//label[text()='Line of Business']/parent::div//button");        
        By optionsJobTypeL = By.XPath("//div[@aria-label='Job Type']//lightning-base-combobox-item//span/span");//div[@aria-label='Job Type']//lightning-base-combobox-item//span[@class='slds-truncate']");
        By lblEngDesc = By.XPath("//div//label[text()='Engagement Description']");
        By tabAdministationL1 = By.XPath("//lightning-tab-bar/ul/li/a[text()='Administration']");
        By tabAdministationL = By.XPath("(//lightning-tab-bar/ul/li/a[text()='Administration'])[2]");
        By lblEngDescL = By.XPath("//label[text()='Engagement Description']");
        By rowContractL = By.XPath("//div//table[@aria-label='Contract']//tbody/tr/th//a//span[@class='slds-truncate']//slot"); //div//table[@aria-label='Contract']//tbody/tr/th//a//span");
        By valERPContractTypeL = By.XPath("//div//table[@aria-label='Contract']//tbody/tr/td[4]//lst-formatted-text/span");
        By valERPBusUnitL = By.XPath("//div//table[@aria-label='Contract']//tbody/tr/td[5]//lst-formatted-text/span");
        By valERPLegalEntityL = By.XPath("//div//table[@aria-label='Contract']//tbody/tr/td[6]//lst-formatted-text/span");
        By valERPBillPlanL = By.XPath("//div//table[@aria-label='Contract']//tbody/tr/td[7]//lst-formatted-text/span");
        By valBillToL = By.XPath("//div//table[@aria-label='Contract']//tbody/tr/td[8]//a");
        By valStartDateL = By.XPath("//div//table[@aria-label='Contract']//tbody/tr/td[9]//div//lightning-formatted-date-time");
        By valIsMainL = By.XPath("//div//table[@aria-label='Contract']//tbody/tr/td[11]//input[@type='checkbox']");
        By valCompNameL = By.XPath("//table[@aria-label='Engagement Contacts']//tbody//tr//td[10]//a");
        By tableContractsL = By.XPath("//table[@aria-label='Contract']");
        By lnkViewAllContactsL = By.XPath("//article[@aria-label='Engagement Contacts']//span[text()='View All']//parent::a");
        By tableContactsL = By.XPath("//table[@aria-label='Engagement Contacts']");
        By btnAddCounterpartiesL = By.XPath("//lightning-layout-item[3]/slot/div/lightning-button-group/div/slot/lightning-button[3]/button");
        By lnkExistingCompanies = By.XPath("//span[text()='Get Companies from existing Company List']");
        By btnViewAllCompList = By.XPath("//button[text()='View All Company List']");
        By radioCompName = By.XPath("//table/tbody/tr[2]/td[1]/div/input");
        By btnOK = By.XPath("//button[@title='OK']");
        By chkCompany = By.XPath("//table/tbody/tr[5]/td[1]/lightning-primitive-cell-checkbox/span/label/span[1]");
        By btnAddCounterpartyTo = By.XPath("//button[text()='Add Counterparty to Project Astro']");
        By btnBackCounterparties = By.XPath("//button[text()='Back']");
        By lnkCompCounterparty = By.XPath("//a[@title='Skyhive']");
        By txtSearchCounterparty = By.XPath("//input[@placeholder='Search']");
        By btnSearchContact = By.XPath("//button[@title='Search']");
        By chkContact = By.XPath("//tr[1]/td[1]/lightning-primitive-cell-checkbox/span/label/span[1]");
        By btnAddContact = By.XPath("//button[@title='counterparty']");
        By lnkContacts = By.XPath("//c-s-l-company-link-column/lightning-layout/slot/lightning-layout-item[2]/slot/div/p");
        By btnPartyL = By.XPath("//div[4]//dl[4]/div[1]/div/div/div/div/div[1]/div/div/a");
        By txtContactL = By.XPath("//input[@title='Search Contacts']");
        By btnSaveContactL = By.XPath("//footer//button/span[text()='Save']");
        By comboCommentTypeL = By.XPath("//button[@aria-label='Comment Type']");
        By tabCommentsL = By.XPath("//div[contains(@class,'sidebar-right')]//li[@title='Comments']");
        By inputCommentL = By.XPath("//label[text()='Comment']/..//textarea");
        By btnSidebarSave = By.XPath("//div[contains(@class,'sidebar-right')]//button[@name='save']");
        By tabFSEngL = By.XPath("//lightning-tab-bar/ul/li/a[text()='FS Engagements']");
        By tabEngFeeAndFinanciaL = By.XPath("//lightning-tab-bar/ul/li/a[text()='Fees & Financials']");
        By lnkMoretabFSEngL = By.XPath("//lightning-tab-bar/ul/li/lightning-button-menu//a/span[text()='FS Engagements']");
        By iconHeaderMoreTabsL = By.XPath("(//lightning-tab-bar/ul/li/lightning-button-menu/button[@title='More Tabs'])[1]");
        By btnNewFSEngL = By.XPath("//article[@aria-label='FS Engagements']//button[@name='New']");
        By inputSponsorCompanyL = By.XPath("//input[@placeholder='Search Companies...']");
        By optionSponsorCompanyL = By.XPath("(//div[@role='listbox']//li)[1]");
        By checkBoxCoExistEngL = By.XPath("//input[@name='Co_exist__c']");
        By checkBoxCoExistEngL2 = By.XPath("(//input[@name='Co_exist__c'])[2]");
        By btnReqFullEngL = By.XPath("//button[text()='Request Full Engagement']");
        By linkReqFullEngL = By.XPath("//a/span[contains(text(),'Request Full Engagement')]");
        By txtEngAlertHeaderErrorsL = By.XPath("//c-engagement-verbally-engaged-approval//div[@role='alert']//h2/lightning-formatted-text");
        By lblVEEngEditFormLabelsL = By.XPath("//c-engagement-verbally-engaged-approval//lightning-input-field//label");
        By iconCloseErrorL = By.XPath("//button[@title='Close this window']");
        By inputSSExpL = By.XPath("//c-engagement-verbally-engaged-approval//lightning-input-field//label[text()='Shared Services Expense']/..//input");
        By inputExpCapL = By.XPath("//c-engagement-verbally-engaged-approval//lightning-input-field//label[text()='Expense Cap']/..//input");
        By inputLegalCapL = By.XPath("//c-engagement-verbally-engaged-approval//lightning-input-field//label[text()='Legal Cap']/..//input");
        By btnIndLngL = By.XPath("//c-engagement-verbally-engaged-approval//lightning-input-field//label[text()='Indemnification Language']/..//button");
        By inputRetainerL = By.XPath("//c-engagement-verbally-engaged-approval//lightning-input-field//label[text()='Retainer']/..//input");
        By inputProgMnthFL = By.XPath("//c-engagement-verbally-engaged-approval//lightning-input-field//label[text()='Progress/Monthly Fee']/..//input");
        By inputContgFeeL = By.XPath("//c-engagement-verbally-engaged-approval//lightning-input-field//label[text()='Contingent Fee']/..//input");
        By inputTailExpL = By.XPath("//c-engagement-verbally-engaged-approval//lightning-input-field//label[text()='Tail Expires']/..//input");
        By btnConfAggL = By.XPath("//c-engagement-verbally-engaged-approval//lightning-input-field//label[text()='Confidentiality Agreement']/..//button");
        By inputFairnessOppL = By.XPath("//c-engagement-verbally-engaged-approval//lightning-input-field//label[text()='Fairness Opinion Component']/..//button[@role='combobox']");
        By inputDateEngdL = By.XPath("//c-engagement-verbally-engaged-approval//lightning-input-field//label[text()='Date Engaged']/..//input");
        By btnEngInfoSaveL = By.XPath("//footer//button[@type='submit']");
        By chkBillingContactL = By.XPath("//span[text()='Billing Contact']/following::input[1]");
        By chkAckBillingContactL = By.XPath("//span[text()='Acknowledge Billing Contact']/following::input[1]");
        By chkPrimaryContactL = By.XPath("//span[text()='Primary Contact']/following::input[1]");
        By btnInlineEditCoExistEngL = By.XPath("//button[@title='Edit Co-exist']");
        By btnInlineEditCoExistEngL2 = By.XPath("(//button[@title='Edit Co-exist'])[2]");
        By txtFSEngIDL = By.XPath("//table[@aria-label='FS Engagements']//tr[1]//th//lightning-primitive-cell-factory[@data-label='FS Engagement ID']//a//slot//slot");
        By txtFSEngNameL = By.XPath("//h1//records-entity-label[text()='FS Engagement']/../../..//slot[@name='primaryField']//lightning-formatted-text");
        By tabContactsL = By.XPath("(//lightning-tab-bar/ul/li/a[text()='Contacts'])[2]");
        By txtContactNameL = By.XPath("//article[@aria-label='Engagement Contacts']//table//tbody/tr//th[@data-label='Name']//a[2]");
        By tabEngContactsL = By.XPath("//lightning-tab-bar/ul/li/a[text()='Contacts']");
        By textEngCommentsL = By.XPath("//article[@aria-label='Comments']//lightning-base-formatted-text"); //h2[text()='Tabs']/..//table//lightning-base-formatted-text");
        By tabcommentsL = By.XPath("(//lightning-tab-bar/ul/li/a[text()='Comments'])[1]");
        By counterpartyNameL = By.XPath("//table//th[@data-label='Company']");
        By tabContactsL2 = By.XPath("//lightning-tab-bar/ul/li/a[text()='Contacts']");        
        By lnkContactL = By.XPath("//table/tbody/tr//span/a[2]");
        By btnEditContactL = By.XPath("//button[@name='Contact.Add_Relationship_QuickAction']/ancestor::ul/li[1]//button");
        By txtFirstNameL = By.XPath("//input[@name='firstName']");
        By txt2ndNameL = By.XPath("//input[@name='lastName']");
        By txtEmailL = By.XPath("//input[@name='Email']");
        By txtTitleL = By.XPath("//input[@name='Title']");
        By txtSecWomenLedLV = By.XPath("//span[text()='Women Led']/ancestor::dl/../../../h3//button/span");

        //By btnCloseReqEngFVAL = By.XPath("//button[@title='Close this window']");
        By btnCloseReqEngFVAL = By.XPath("//button[@title ='Cancel and close']"); 
        By subTabEng = By.XPath("//section[2]//ul[2]/li[2]/a/span[2]");        
        By btnStartingYear = By.XPath("//button[@aria-label='Year']");
        By valStartingYear = By.XPath("//label[text()='Year']/ancestor::div[1]/div[1]//div[2]/lightning-base-combobox-item[5]//span[2]/span");
        By btnViewCounterparty = By.XPath("//li[contains(@data-target-selection-name,'QuickAction.Engagement')]//button[contains(text(),'View Counterparties')]");

        private By _elmRecordType(string text)
        {
            return By.XPath($"//div[contains(@class,'changeRecordTypeRightColumn')]//label//div//span[@class='slds-form-element__label'][text()='{text}']");
        }
        private By _sharingGroup(string text)
        {
            return By.XPath($"//div[contains(@class,'recordsRecordShare')]//table//tbody//tr//lightning-base-formatted-text[text()='{text}']");
        }
        private By _linkQuestionnaireNumer(string caseNumber)
        {
            return By.XPath($"//a[contains(text(),'{caseNumber}')]/ancestor::tr//th//a");
        }
        private By _tabActionFor(string tabName)
        {
            return By.XPath($"//button//span[contains(text(),'Actions for {tabName}')]//parent::button");
        }
        private By _relatedQuickLink(string linkText)
        {
            return By.XPath($"//ul//li[@class='rlql-relatedListQuickLink']//a//span[contains(text(),'{linkText}')]");
        }
        private By _tabDetailPage(string tabName)
        {
            return By.XPath($"//a[@data-label='{tabName}']");
        }
        private By _button(string buttonName)
        {
            return By.XPath($"//span[contains(text(),'{buttonName}')]");
        }
        private By _optionMeetingType(string meetingType)
        {
            return By.XPath($"//span[contains(text(),'{meetingType}')]/ancestor::span//input");
        }
        private By _formSection(string sectionName)
        {
            return By.XPath($"//span[contains(text(),'{sectionName}')]");
        }
        private By _closeTab(string number)
        {
            return By.XPath($"//button[contains(@title,'{number}')]");
        }
        private By _invitationFields(string value)
        {
            return By.XPath($"//lightning-layout-item/slot[contains(text(),'{value}')]");
        }
        private By _DetailPageQuickLink(string name)
        {
            return By.XPath($"//div[@class='listHoverLinks']//a//span[text()='{name}']");
        }
        private By _panelRightEngagement(string name)
        {
            return By.XPath($"//h2//a//span[contains(@title,'{name}')]");
        }
        private By _ActivitySubject(string activitySubject)
        {
            return By.XPath($"//h2//span[text()='Engagement Activity']//ancestor::article//lightning-primitive-cell-factory[@data-label='Subject']//lightning-base-formatted-text[text()='{activitySubject}']");//(//lightning-datatable//table//tbody//td//lightning-primitive-cell-factory[@data-label='Subject']//lightning-base-formatted-text[text()='{activitySubject}'])[2]
        }
        private By _btnAddContactL(string lob)
        {
            return By.XPath($"//button[contains(@name,'Add_{lob}_Engagement_Contact')]");
        }


        public string GetRecordTypeL()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, subTabAdminEngL, 60);
            driver.FindElement(subTabAdminEngL).Click();
            Thread.Sleep(5000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,650)");
            Thread.Sleep(5000);
            string recordType = driver.FindElement(valRecordTypeL).Text;
            return recordType;
        }
        public bool IsViewCounterpartyButtonEngagementPageL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewCounterparty, 60);
            return driver.FindElement(btnViewCounterparty).Displayed;
        }
        public void ClickViewCounterpartyButtonEngagementPageL()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewCounterparty, 60);
            driver.FindElement(btnViewCounterparty).Click();
            Thread.Sleep(8000);
        }

        public string GetEngNumL()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valEngNumL, 200);
            string name = driver.FindElement(valEngNumL).Text;
            return name;
        }
        public void ClickPanelRightEngagementPage(string name)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,150)");

            //CustomFunctions.MoveToElement(driver, driver.FindElement(footerSection));
            WebDriverWaits.WaitUntilEleVisible(driver, _panelRightEngagement(name), 60);
            driver.FindElement(_panelRightEngagement(name)).Click();
            By header = By.XPath($"//h1[contains(@title,'{name}')]");
            WebDriverWaits.WaitUntilEleVisible(driver, header, 20);
        }
        public void CreateBillingContactLV(string name, string party)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactL, 20);
            driver.FindElement(txtContactL).SendKeys(name);
            Thread.Sleep(3000);
            try
            {
                By listContactOption = By.XPath($"//div[@role='listbox']//ul//li//a//div[2]//div[1][@title='{name}']");
                WebDriverWaits.WaitUntilEleVisible(driver, listContactOption, 5);
                driver.FindElement(listContactOption).Click();
            }
            catch (Exception ex)
            {
                By iconContactSearchItem = By.XPath("//div[contains(@class,'searchButton')]");
                WebDriverWaits.WaitUntilEleVisible(driver, iconContactSearchItem, 5);
                driver.FindElement(iconContactSearchItem).Click();
                By txtContact = By.XPath("//div[contains(@class,'gridInScroller')]//table//tbody//tr[1]//td[1]//a");
                WebDriverWaits.WaitUntilEleVisible(driver, txtContact, 20);
                driver.FindElement(txtContact).Click();
            }

            WebDriverWaits.WaitUntilEleVisible(driver, btnPartyL, 20);
            driver.FindElement(btnPartyL).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div[8]/div/ul/li/a[text()='" + party + "']")).Click();
            driver.FindElement(chkBillingContactL).Click();
            driver.FindElement(chkAckBillingContactL).Click();
            driver.FindElement(chkPrimaryContactL).Click();
            driver.FindElement(btnSaveContactL).Click();
            Thread.Sleep(5000);
        }

        public void EnterRequestFullEngagementReqValuesLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, inputSSExpL, 20);
            driver.FindElement(inputSSExpL).SendKeys("10");
            WebDriverWaits.WaitUntilEleVisible(driver, inputExpCapL, 20);
            driver.FindElement(inputExpCapL).SendKeys("10");
            WebDriverWaits.WaitUntilEleVisible(driver, inputLegalCapL, 20);
            driver.FindElement(inputLegalCapL).SendKeys("10");

            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEngInfoSaveL));
            WebDriverWaits.WaitUntilEleVisible(driver, btnIndLngL, 5);
            driver.FindElement(btnIndLngL).Click();
            By eleOptionIngLng = By.XPath("//c-engagement-verbally-engaged-approval//lightning-input-field//label[text()='Indemnification Language']/..//button/../..//lightning-base-combobox-item//span[@title='No']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleOptionIngLng, 5);
            driver.FindElement(eleOptionIngLng).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, inputRetainerL, 20);
            driver.FindElement(inputRetainerL).SendKeys("10");
            WebDriverWaits.WaitUntilEleVisible(driver, inputProgMnthFL, 20);
            driver.FindElement(inputProgMnthFL).SendKeys("10");
            //WebDriverWaits.WaitUntilEleVisible(driver, inputContgFeeL, 20);
            //driver.FindElement(inputContgFeeL).SendKeys("10");
            //WebDriverWaits.WaitUntilEleVisible(driver, inputTailExpL, 20);
            //string dateTailExp = DateTime.Today.AddDays(20).ToString("dd MMM yyyy");
            //driver.FindElement(inputTailExpL).SendKeys(dateTailExp);

            WebDriverWaits.WaitUntilEleVisible(driver, btnConfAggL, 20);
            driver.FindElement(btnConfAggL).Click();
            By eleOptionConfAgg = By.XPath("//c-engagement-verbally-engaged-approval//lightning-input-field//label[text()='Confidentiality Agreement']/..//button/../..//lightning-base-combobox-item//span[@title='No']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleOptionConfAgg, 5);
            driver.FindElement(eleOptionConfAgg).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, inputFairnessOppL, 5);
            driver.FindElement(inputFairnessOppL).Click();
            By eleOptionFairnessOpp = By.XPath("//c-engagement-verbally-engaged-approval//lightning-input-field//label[text()='Fairness Opinion Component']/..//button[@role='combobox']/../..//lightning-base-combobox-item//span[@title='No']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleOptionFairnessOpp, 5);
            driver.FindElement(eleOptionFairnessOpp).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, inputDateEngdL, 5);
            string dateEngd = DateTime.Today.AddDays(-2).ToString("dd-MMM-yyyy");
            driver.FindElement(inputDateEngdL).SendKeys(dateEngd);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEngInfoSaveL));
            WebDriverWaits.WaitUntilEleVisible(driver, btnEngInfoSaveL, 5);
            driver.FindElement(btnEngInfoSaveL).Click();
            //driver.FindElement(iconCloseErrorL).Click();
        }
        public void ClickRequestFullEngagementLV()
        {
            try
            {
                Thread.Sleep(5000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnReqFullEngL, 20);
                driver.FindElement(btnReqFullEngL).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButonL, 10);
                driver.FindElement(iconExpandMoreButonL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, linkReqFullEngL, 20);
                driver.FindElement(linkReqFullEngL).Click();
            }
        }
        public string GetVerballyFullEngValidationHeaderErrorsLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngAlertHeaderErrorsL, 10);
            Thread.Sleep(2000);
            string pageLevelError = driver.FindElement(txtEngAlertHeaderErrorsL).Text;
            string formatedHeaderError = Regex.Replace(pageLevelError, @"\t|\n|\r", "");
            return formatedHeaderError;
        }
        public string GetVerballyFullEngReqFieldsLV()
        {
            IList<IWebElement> fieldLevelErrors = driver.FindElements(lblVEEngEditFormLabelsL);
            string formatedReqFieldLabels = "";
            foreach (IWebElement txtFieldLevelError in fieldLevelErrors)
            {
                string fieldLevelError = txtFieldLevelError.Text;
                string formatedfieldLevelLabels = Regex.Replace(fieldLevelError, @"\t|\n|\r", "");
                formatedReqFieldLabels = formatedReqFieldLabels + formatedfieldLevelLabels;
            }
            //driver.FindElement(iconCloseErrorL).Click();
            Thread.Sleep(2000);
            return formatedReqFieldLabels;
        }
        public string ValidateIfCoExistFieldIsPresentAndCheckedOrNotLV()
        {
            try
            {
                //driver.FindElement(tabAdministationL).Click();
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, checkBoxCoExistEngL, 5);
                    if (driver.FindElement(checkBoxCoExistEngL).Displayed)
                    {
                        if (driver.FindElement(checkBoxCoExistEngL).Selected)
                        {
                            return "Co-Exist checkbox is displayed and checked";
                        }
                        else
                        {
                            return "Co-Exist checkbox is displayed and not-checked";
                        }
                    }
                    else
                    {
                        return "Co-Exist checkbox is not displayed.";
                    }
                }
                catch
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, checkBoxCoExistEngL2, 5);
                    if (driver.FindElement(checkBoxCoExistEngL2).Displayed)
                    {
                        if (driver.FindElement(checkBoxCoExistEngL2).Selected)
                        {
                            return "Co-Exist checkbox is displayed and checked";
                        }
                        else
                        {
                            return "Co-Exist checkbox is displayed and not-checked";
                        }
                    }
                    else
                    {
                        return "Co-Exist checkbox is not displayed.";
                    }
                }

            }
            catch
            {
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, tabAdministationL, 5);
                    driver.FindElement(tabAdministationL).Click();
                }
                catch
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, tabAdministationL1, 5);
                    driver.FindElement(tabAdministationL1).Click();
                }


                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, checkBoxCoExistEngL, 5);
                    if (driver.FindElement(checkBoxCoExistEngL).Displayed)
                    {
                        if (driver.FindElement(checkBoxCoExistEngL).Selected)
                        {
                            return "Co-Exist checkbox is displayed and checked";
                        }
                        else
                        {
                            return "Co-Exist checkbox is displayed and not-checked";
                        }
                    }
                    else
                    {
                        return "Co-Exist checkbox is not displayed.";
                    }
                }
                catch
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, checkBoxCoExistEngL2, 5);
                    if (driver.FindElement(checkBoxCoExistEngL2).Displayed)
                    {
                        if (driver.FindElement(checkBoxCoExistEngL2).Selected)
                        {
                            return "Co-Exist checkbox is displayed and checked";
                        }
                        else
                        {
                            return "Co-Exist checkbox is displayed and not-checked";
                        }
                    }
                    else
                    {
                        return "Co-Exist checkbox is not displayed.";
                    }
                }

            }
        }

        public string VerifyIfCoExistFieldIsEditableOrNotLV()
        {
            driver.FindElement(tabAdministationL).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnInlineEditCoExistEngL, 5);
                if (driver.FindElement(btnInlineEditCoExistEngL).Displayed)
                {
                    return "Co-Exist field is editable";
                }
                else
                {
                    return "Co-Exist field is not editable";
                }
            }
            catch
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnInlineEditCoExistEngL2, 5);
                if (driver.FindElement(btnInlineEditCoExistEngL2).Displayed)
                {
                    return "Co-Exist field is editable";
                }
                else
                {
                    return "Co-Exist field is not editable";
                }

            }

        }
        public void CreateContact(string file, string contact, string valRecType, string valType, int rowNumber)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file; WebDriverWaits.WaitUntilEleVisible(driver, btnAddOppContact, 50);
            driver.FindElement(btnAddOppContact).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 80);
            driver.FindElement(txtContact).SendKeys(contact);
            driver.FindElement(comboRole).SendKeys(TestData.ReadExcelData.ReadData(excelPath, "AddContact", 2));
            driver.FindElement(comboType).SendKeys(valType); string Type = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowNumber, 4);
            if (Type.Equals("Client"))
            {
                driver.FindElement(checkPrimaryContact).Click();
            }
            if (valRecType.Equals("CF"))
            {
                driver.FindElement(comboParty).SendKeys(ReadExcelData.ReadData(excelPath, "AddContact", 3));
            }
            if (Type.Equals("External"))
            {
                driver.FindElement(checkPrimaryContact).Click();
                driver.FindElement(checkAckBillingContact).Click();
                driver.FindElement(checkBillingContact).Click();
            }
            driver.FindElement(btnSave).Click();
        }

        public string ClickICOContractsLink(int row)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContract, 180);
            string contractName = driver.FindElement(lnkContract).Text;
            if (contractName.Contains("ICO"))
            {
                driver.FindElement(lnkContract).SendKeys(Keys.Control + Keys.Return);
                if (row.Equals(2))
                {
                    CustomFunctions.SwitchToWindow(driver, 1);
                    driver.Navigate().Refresh();
                }
                else if (row.Equals(3))
                {
                    CustomFunctions.SwitchToWindow(driver, 4);
                    driver.Navigate().Refresh();
                }
                else
                {
                    CustomFunctions.SwitchToWindow(driver, 7);
                    driver.Navigate().Refresh();
                }
                return "ICO contract created";
            }
            else
            {
                return "NO ICO contract created";
            }
        }

        public int numberOfContractCounts()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNoOfContract);
            int countContracts = driver.FindElements(valNoOfContract).Count;
            return countContracts;
        }
        public void ClickContractLink(int row)
        {
            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContract, 180);
            driver.FindElement(lnkContract).SendKeys(Keys.Control + Keys.Return);
            if (row.Equals(2))
            {
                CustomFunctions.SwitchToWindow(driver, 1);
                driver.Navigate().Refresh();
            }
            else
            {
                CustomFunctions.SwitchToWindow(driver, 4);
                driver.Navigate().Refresh();
            }
        }
        public void ClickBillToForContract(int row)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkBillTo, 180);
            driver.FindElement(lnkBillTo).SendKeys(Keys.Control + Keys.Return);
            if (row.Equals(2))
            {
                CustomFunctions.SwitchToWindow(driver, 2);
                driver.Navigate().Refresh();
            }
            else
            {
                CustomFunctions.SwitchToWindow(driver, 5);
                driver.Navigate().Refresh();
            }
        }

        //Search and select the desired case
        public void SelectCase(string CaseNumber)
        {
            driver.FindElement(searchBox).SendKeys(CaseNumber);
            driver.FindElement(searchBox).SendKeys(Keys.Enter);
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//table[contains(@class,'slds-table')]//tbody//tr//th//a[@title='" + CaseNumber + "']")).Click();
            Thread.Sleep(4000);
        }
        public void SelectListView(string view)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownist, 5);
            driver.FindElement(dropdownist).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, searchList, 5);
            driver.FindElement(searchList).Click();
            driver.FindElement(searchList).SendKeys(view);
            WebDriverWaits.WaitUntilEleVisible(driver, listOption, 5);
            driver.FindElement(listOption).Click();
            Thread.Sleep(4000);
        }
        public void SelectOpenCase()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, selectItem, 10);
            driver.FindElement(selectItem).Click();
            Thread.Sleep(4000);
        }
        public void SelectFirstCase()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkFirstCase, 10);
            driver.FindElement(linkFirstCase).Click();
            Thread.Sleep(4000);
        }
        public void SearchCaseNumber(string CaseNumber)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, searchBox, 10);
            driver.FindElement(searchBox).SendKeys(CaseNumber);
            driver.FindElement(searchBox).SendKeys(Keys.Enter);
            WebDriverWaits.WaitUntilEleVisible(driver, By.XPath("//table[contains(@class,'slds-table')]//tbody//tr//th//a[@title='" + CaseNumber + "']"), 10);
            Thread.Sleep(5000);
        }
        public bool IsDeleteOptionDisplayed()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRowAction, 10);
                driver.FindElement(btnRowAction).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, linkDelete, 5);
                return driver.FindElement(linkDelete).Displayed;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public string DeleteRecord()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            WebDriverWaits.WaitUntilEleVisible(driver, linkDelete, 5);
            driver.FindElement(linkDelete).Click();
            Thread.Sleep(1000);
            WebDriverWaits.WaitUntilEleVisible(driver, buttonDelete, 5);
            IWebElement btnDeleteEle = driver.FindElement(buttonDelete);
            js.ExecuteScript("arguments[0].click();", btnDeleteEle);
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 5);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(1000);
            driver.FindElement(toastMsgCloseIcon).Click();
            return toasMsg;
        }
        public void SwitchToClassicView()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, imgProfile, 150);
                driver.FindElement(imgProfile).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, lnkSwitchToClassic, 120);
                driver.FindElement(lnkSwitchToClassic).Click();
                Thread.Sleep(2000);
            }
            catch (Exception e) { }
        }

        public bool ValidateRelatedQuickLink(string linkText)
        {
            try
            {
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, _relatedQuickLink(linkText), 20);
                return driver.FindElement(_relatedQuickLink(linkText)).Displayed;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public void ClickRelatedQuickLink(string linkText)
        {
            driver.FindElement(_relatedQuickLink(linkText)).Click();
            //Thread.Sleep(2000);
        }
        public string GetCaseQuestionnaireNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, LinkQuestionnaire, 20);
            return driver.FindElement(LinkQuestionnaire).Text;
        }
        public void ClickQuestionnairesLink()
        {

            driver.FindElement(LinkQuestionnaire).Click();
        }
        public bool IsButtonClientServicesQuestionnaire(string buttonName)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, _button(buttonName), 10);
                return driver.FindElement(_button(buttonName)).Displayed;
            }
            catch (Exception e)
            {
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButon, 10);
                    driver.FindElement(iconExpandMoreButon).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, _button(buttonName), 10);
                    return driver.FindElement(_button(buttonName)).Displayed;

                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public void SelectTabDetailPage(string tabName)
        {
            driver.FindElement(_tabDetailPage(tabName)).Click();
        }
        public bool ValidateCSTQuestionnaireDropdownOptions(string file)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconCSTTabInline, 10);
            driver.FindElement(iconCSTTabInline).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, DropdownIconCST, 10);
            driver.FindElement(DropdownIconCST).Click();
            Thread.Sleep(2000);
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(drodownCSTOptions);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = new string[3];

            //bool isSame = true;
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            int RowPreSetTemplateList = ReadExcelData.GetRowCount(excelPath, "CSQuestionnaireDetailTabOptions");
            int index;
            for (int row = 2; row <= RowPreSetTemplateList; row++)
            {
                index = row - 2;
                string valueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CSQuestionnaireDetailTabOptions", row, 1);
                if (valueExl == "None")
                {
                    valueExl = "--" + valueExl + "--";
                }
                expectedValue[index] = valueExl;
            }
            bool equal = actualValue.SequenceEqual(expectedValue);
            driver.FindElement(btnCancelCSTQuestionnaire).Click();
            return equal;
        }
        public bool ValidateButtonSaveOption(string option)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconCSTTabInline, 10);
            driver.FindElement(iconCSTTabInline).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, DropdownIconCST, 10);
            driver.FindElement(DropdownIconCST).Click();
            Thread.Sleep(2000);
            IList<IWebElement> valTypes = driver.FindElements(drodownCSTOptions);
            bool resultSaved = false;
            if (option.Equals("Yes"))
            {
                for (int rec = 0; rec < valTypes.Count; rec++)
                {
                    string value = valTypes[rec].Text;
                    if (option.Equals(value))
                    {
                        valTypes[rec].Click();
                        driver.FindElement(btnSaveCSTQuestionnaire).Click();
                        WebDriverWaits.WaitUntilEleVisible(driver, valueYesSaved, 20);
                        resultSaved = driver.FindElement(valueYesSaved).Displayed;
                        break;
                    }
                }
            }
            else
            {
                for (int rec = 0; rec < valTypes.Count; rec++)
                {
                    string value = valTypes[rec].Text;
                    if (option.Equals(value))
                    {
                        valTypes[rec].Click();
                        WebDriverWaits.WaitUntilEleVisible(driver, reasonNoOption, 20);
                        //driver.FindElement(reasonNoOption);
                        driver.FindElement(reasonNoOption).Clear();
                        driver.FindElement(reasonNoOption).SendKeys("Reason Why CST is Not Being Used: Test Comments ");
                        driver.FindElement(btnSaveCSTQuestionnaire).Click();
                        WebDriverWaits.WaitUntilEleVisible(driver, valueNoSaved, 20);
                        resultSaved = driver.FindElement(valueNoSaved).Displayed;
                        break;
                    }
                }
            }

            return resultSaved;
        }
        public bool ValidateAddionalField(string option)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconCSTTabInline, 10);
            driver.FindElement(iconCSTTabInline).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, DropdownIconCST, 10);
            driver.FindElement(DropdownIconCST).Click();
            Thread.Sleep(2000);
            IList<IWebElement> valTypes = driver.FindElements(drodownCSTOptions);
            bool reasonTextBox = false;
            for (int rec = 0; rec < valTypes.Count; rec++)
            {
                string value = valTypes[rec].Text;
                if (option.Equals(value))
                {
                    valTypes[rec].Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, reasonNoOption, 20);
                    reasonTextBox = driver.FindElement(reasonNoOption).Displayed;
                    Thread.Sleep(1000);
                    break;
                }
            }
            driver.FindElement(btnCancelCSTQuestionnaire).Click();
            return reasonTextBox;
        }
        public string GetEngagementDetailEngagementName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, textEngagementDetailEngagementName, 5);
            return driver.FindElement(textEngagementDetailEngagementName).Text;
        }
        public string GetEngagementDetailEngagementNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, textEngagementDetailEngagementNumber, 5);
            return driver.FindElement(textEngagementDetailEngagementNumber).Text;
        }
        public string GetCaseDetailEngagementName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, textCaseDetailEngagementName, 5);
            return driver.FindElement(textCaseDetailEngagementName).Text;
        }
        public string GetCaseDetailEngagementNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, textCaseDetailEngagementNumber, 5);
            return driver.FindElement(textCaseDetailEngagementNumber).Text;
        }
        public bool IsRelatedTabDisplayed()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,0)");

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tabRelated, 10);
                return driver.FindElement(tabRelated).Displayed;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool IsCommentSectionDisplayed()
        {
            try
            {
                driver.FindElement(tabRelated).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, sectionCaseComments, 10);
                return driver.FindElement(sectionCaseComments).Displayed;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string AddCaseComments(string caseComments)
        {
            //Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, iconCommentsAction, 10);
            driver.FindElement(iconCommentsAction).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkNew, 5);
            Thread.Sleep(1000);
            driver.FindElement(linkNew).Click();
            Thread.Sleep(1000);
            WebDriverWaits.WaitUntilEleVisible(driver, textArea, 5);
            driver.FindElement(textArea).SendKeys(caseComments);
            driver.FindElement(btnSaveComments).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 5);
            string txtmsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            driver.FindElement(toastMsgCloseIcon).Click();
            return txtmsg;

        }
        public void AddCaseNotes(string notes)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,450)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, iconNotesAction, 10);
            driver.FindElement(iconNotesAction).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkNew, 5);
            Thread.Sleep(1000);
            driver.FindElement(linkNew).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtNotesTitle, 10);
            driver.FindElement(txtNotesTitle).SendKeys(notes);
            driver.FindElement(txtNotes).SendKeys(notes);
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilClickable(driver, btnNotesDone, 10);
            driver.FindElement(btnNotesDone).Click();
        }
        public string GetSavedCaseComments()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, savedComments, 10);
            return driver.FindElement(savedComments).Text;

        }

        public string GetCaseDetailMeetingType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, textCaseDetailsMeetingType, 5);
            string meetingType = driver.FindElement(textCaseDetailsMeetingType).Text;
            if (meetingType == "Early Look Meetings")
            {
                return "Early look Meetings";
            }
            else
                return meetingType;
        }
        public string AreCaseDetailPageQuickLinksDisplayed(string file)
        {
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(linksRelatedList);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            //string[] expectedValue = new string[3];
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            int quickLinks = ReadExcelData.GetRowCount(excelPath, "CaseQuickLinks");
            bool isFound = false; ;
            for (int row = 2; row <= quickLinks; row++)
            {
                isFound = false;
                string valueExl = ReadExcelData.ReadData(excelPath, "CaseQuickLinks", row);
                for (int rec = 0; rec < actualValue.Length; rec++)
                {
                    if (actualValue[rec].Contains(valueExl))
                    {
                        isFound = true;
                        break;
                    }
                }
            }
            if (isFound)
                return "All Quick Links are Displayed";
            else
                return "All Quick Links are not Displayed";
        }
        public void ClickButtonCSQ(string buttonName)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, _button(buttonName), 5);
                driver.FindElement(_button(buttonName)).Click();
            }
            catch (Exception e)
            {
                try
                {
                    driver.FindElement(iconExpandMoreButon).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, _button(buttonName), 5);
                    driver.FindElement(_button(buttonName)).Click();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public string EngagementCSTUploadFile(string filePath)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                WebDriverWaits.WaitUntilEleVisible(driver, panelCST, 10);
                driver.FindElement(panelCST).Click();
                js.ExecuteScript("window.scrollTo(0,100)");
                WebDriverWaits.WaitUntilEleVisible(driver, PanelCSTFiles, 30);
                CustomFunctions.FileUpload(driver, filePath);
                WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
                return driver.FindElement(toastMsgPopup).Text;
            }
            catch (Exception ex)
            {
                return "Fle not uploaded";
            }

        }

        public string CSTCaseUploadFile(string filePath)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript("window.scrollTo(0,400)");
                Thread.Sleep(3000);
                CustomFunctions.FileUpload(driver, filePath);
                WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
                string toasMsg = driver.FindElement(toastMsgPopup).Text;
                Thread.Sleep(1000);
                driver.FindElement(toastMsgCloseIcon).Click();
                return toasMsg;
            }
            catch (Exception ex)
            {
                return "Fle not uploaded";
            }

        }
        public void SelectMeetingType(string meetingType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _optionMeetingType(meetingType), 20);
            Thread.Sleep(2000);
            IWebElement el = driver.FindElement(_optionMeetingType(meetingType));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", el);
        }
        public void ButtonClickContinue()
        {
            driver.FindElement(buttonContinue).Click();
        }

        public bool VerifyQuestionnaireInfoPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabCSTForm, 20);
            driver.FindElement(tabCSTForm).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerPageInfo, 10);
            return driver.FindElement(headerPageInfo).Displayed;
        }
        public void ButtonClientServicesQuestionaire(string buttonName)
        {
            try
            {
                driver.FindElement(_button(buttonName)).Click();
            }
            catch (Exception e)
            {
                driver.FindElement(iconExpandMoreButon).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, _button(buttonName), 10);
                driver.FindElement(_button(buttonName)).Click();
            }
        }
        public bool ValidateMeetingTypes(string file)
        {
            bool meetingTypesDisplayed = false;
            WebDriverWaits.WaitUntilEleVisible(driver, meetingTypes, 10);
            IReadOnlyCollection<IWebElement> valMeetingTypes = driver.FindElements(meetingTypes);
            var actualValue = valMeetingTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = new string[4];
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            int RowPreSetTemplateList = ReadExcelData.GetRowCount(excelPath, "MeetingTypes");
            int index;
            for (int row = 2; row <= RowPreSetTemplateList; row++)
            {
                index = row - 2;
                expectedValue[index] = ReadExcelData.ReadDataMultipleRows(excelPath, "MeetingTypes", row, 1);
            }
            meetingTypesDisplayed = actualValue.SequenceEqual(expectedValue);
            WebDriverWaits.WaitUntilEleVisible(driver, iconClosePopUp, 10);
            driver.FindElement(iconClosePopUp).Click();
            return meetingTypesDisplayed;
        }
        public bool ValidateFormSections(string meetingType, string file)
        {
            bool sectionsDisplayed = false;
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            int formRowsCountExl = ReadExcelData.GetRowCount(excelPath, "MeetingTypes");
            for (int row = 2; row <= formRowsCountExl; row++)
            {
                string rowValue = ReadExcelData.ReadDataMultipleRows(excelPath, "MeetingTypes", row, 1);
                if (rowValue == meetingType)
                {
                    int formFieldsCountExl = ReadExcelData.GetColumnCount(excelPath, "MeetingTypes");
                    for (int col = 2; col <= formFieldsCountExl; col++)
                    {
                        string sectionValue = ReadExcelData.ReadDataMultipleRows(excelPath, "MeetingTypes", row, col);
                        if (sectionValue.IsNullOrEmpty())
                            break;
                        sectionsDisplayed = driver.FindElement(_button(sectionValue)).Displayed;
                    }
                    break;
                }
            }
            return sectionsDisplayed;
        }
        public bool ValidateOutlookInvitaionFieldDisplayed()
        {

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, fieldInvitation, 10);
                return driver.FindElement(fieldInvitation).Displayed;
            }
            catch
            {
                return false;
            }

        }
        public bool ValidateCSTOutlookInvitationsOptions(string file, int row)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;

            IWebElement iconInlineEditEle = driver.FindElement(iconInlineEdit);
            WebDriverWaits.WaitUntilEleVisible(driver, iconInlineEdit, 20);
            iconInlineEditEle.Click();
            Thread.Sleep(1000);
            IWebElement dropdownIconInvitationEle = driver.FindElement(dropdownIconInvitation);
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownIconInvitation, 10);
            if (row == 3 || row == 4)
            {
                js.ExecuteScript("window.scrollTo(0,900)");
            }
            else
            {
                js.ExecuteScript("window.scrollTo(0,1800)");
            }
            dropdownIconInvitationEle.Click();
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(optionsInvitation);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = new string[3];

            //bool isSame = true;
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            int RowPreSetTemplateList = ReadExcelData.GetRowCount(excelPath, "CSTOutlookInvitations");
            int index;
            for (int rowTemplate = 2; rowTemplate <= RowPreSetTemplateList; rowTemplate++)
            {
                index = rowTemplate - 2;
                string valueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CSTOutlookInvitations", rowTemplate, 1);
                if (valueExl == "None")
                {
                    valueExl = "--" + valueExl + "--";
                }
                expectedValue[index] = valueExl;
            }
            bool equal = actualValue.SequenceEqual(expectedValue);
            driver.FindElement(btnCancelCSTQuestionnaire).Click();
            return equal;
        }
        public bool ValidateOutlookInvitaionPreferenceFieldDisplayed()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, fieldInvitationPreference, 10);
                return driver.FindElement(fieldInvitationPreference).Displayed;
            }
            catch
            {
                return false;
            }

        }
        public bool ValidateCSTOutlookInvitationsPreferenceOptions(string file, int row)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            driver.FindElement(tabSection);
            js.ExecuteScript("window.scrollTo(0,200)");
            IWebElement iconInlineEditEle = driver.FindElement(iconInlineEdit);
            WebDriverWaits.WaitUntilEleVisible(driver, iconInlineEdit, 20);
            iconInlineEditEle.Click();
            Thread.Sleep(1000);
            IWebElement dropdownIconInvitationPreferenceEle = driver.FindElement(dropdownIconInvitationPreference);
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownIconInvitationPreference, 5);

            if (row == 3 || row == 4)
            {
                js.ExecuteScript("window.scrollTo(0,900)");
            }
            else
            {
                js.ExecuteScript("window.scrollTo(0,1860)");
            }
            dropdownIconInvitationPreferenceEle.Click();
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(optionsInvitationPreference);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = new string[3];
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            int RowPreSetTemplateList = ReadExcelData.GetRowCount(excelPath, "CSTOutlookInvitations");
            int index;
            for (int rowTemplate = 2; rowTemplate <= RowPreSetTemplateList; rowTemplate++)
            {
                index = rowTemplate - 2;
                string valueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CSTOutlookInvitations", rowTemplate, 2);
                if (valueExl == "None")
                {
                    valueExl = "--" + valueExl + "--";
                }
                expectedValue[index] = valueExl;
            }
            bool equal = actualValue.SequenceEqual(expectedValue);
            driver.FindElement(btnCancelCSTQuestionnaire).Click();
            return equal;
        }
        public bool AreCSTOutlookInvitationFieldsDisplayed(string file, int row)
        {

            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            if (row == 3 || row == 4)
            {
                js.ExecuteScript("window.scrollTo(0,710)");
            }
            else
            {
                js.ExecuteScript("window.scrollTo(0,1200)");
            }
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(lablesOutlookInvitationFields);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = new string[4];
            int RowFieldLabels = ReadExcelData.GetRowCount(excelPath, "CSTOutlookInvitationsFields");
            int index;
            for (int rowLabel = 2; rowLabel <= RowFieldLabels; rowLabel++)
            {
                index = rowLabel - 2;
                string valueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CSTOutlookInvitationsFields", rowLabel, 1);
                expectedValue[index] = valueExl;
            }
            bool equal = actualValue.SequenceEqual(expectedValue);
            return equal;
        }
        public string[] GetIntenalTeamMembers()
        {
            driver.FindElement(linkInternalTeam).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, InternalTeamList, 10);
            IList<IWebElement> InternalTeamTableRows = driver.FindElements(By.XPath("//div[@class='pbBody']//table//tbody//tr"));
            int MembersCount = InternalTeamTableRows.Count;
            string[] InternalTeamMembers = new string[MembersCount];
            int index = 0;
            for (int tableRow = 0; tableRow < MembersCount; tableRow++)
            {
                InternalTeamMembers[index] = driver.FindElement(By.XPath($"//div[@class='pbBody']//table//tbody//tr[{tableRow}]//td[1]//label")).Text;
                index++;
            }
            return InternalTeamMembers;
        }

        public void SelectDealTeamContact()
        {
            driver.FindElement(autoDropdownDealTeamContacts).Click();
            driver.FindElement(dropdownOption).Click();
            Thread.Sleep(1000);

        }
        public void SelectDealTeamMember()
        {
            driver.FindElement(autoDropdownDealTeamMembers).Click();
            driver.FindElement(dropdownOption).Click();
            Thread.Sleep(1000);
        }
        public void SelectClientInvitation()
        {
            driver.FindElement(autoDropdownClients).Click();
            driver.FindElement(dropdownOption).Click();
            Thread.Sleep(1000);
        }
        public void SelectCounterpartiesInvitation()
        {
            driver.FindElement(autoDropdownDealTeamCounterparties).Click();
            driver.FindElement(dropdownOption).Click();
            Thread.Sleep(1000);
        }
        public string SaveOutlookInvitationFields()
        {
            driver.FindElement(btnOutlookInvitationFieldsSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);

            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            driver.FindElement(toastMsgCloseIcon).Click();
            return toasMsg;
        }
        public void RemoveSelectedOptions()
        {
            IList<IWebElement> selection = driver.FindElements(optionsSelected);
            for (int icon = 0; icon < selection.Count; icon++)
            {
                selection[icon].Click();
                Thread.Sleep(1000);
            }
        }

        public string GetQuestionnaireNumber()
        {
            Thread.Sleep(1000);
            WebDriverWaits.WaitUntilEleVisible(driver, questionnaireNumber, 10);
            return driver.FindElement(questionnaireNumber).Text;
        }
        public string GetCaseStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, caseSavedStatus, 20);
            return driver.FindElement(caseSavedStatus).Text;
        }
        public void CloseQuestionnaire(string number)
        {

            driver.FindElement(_closeTab("Close " + number)).Click();
        }
        public void CloseTab(string number)
        {
            driver.FindElement(_closeTab("Close " + number)).Click();
        }
        public void ValidateFormFields(string meetingType, string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            int formRowsCountExl = ReadExcelData.GetRowCount(excelPath, "FormFields");
            for (int row = 2; row <= formRowsCountExl; row++)
            {
                string rowValue = ReadExcelData.ReadDataMultipleRows(excelPath, "FormFields", row, 1);
                if (rowValue == meetingType)
                {
                    int formFieldsCountExl = ReadExcelData.GetColumnCount(excelPath, "FormFields");
                    for (int col = 3; col <= formFieldsCountExl; col++)
                    {
                        string valueFormFieldExl = ReadExcelData.ReadDataMultipleRows(excelPath, "FormFields", row, col);

                    }
                }
            }
        }
        public void ClickEditInlineIcon()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconInlineEdit, 20);
            driver.FindElement(iconInlineEdit).Click();
            Thread.Sleep(3000);
        }
        public void FillCSTFormRequiredFields(string meetingTypeExl)
        {
            string expectedBidDate = DateTime.Today.AddDays(2).ToString("dd/MM/yyyy");
            string startDate = DateTime.Today.AddDays(2).ToString("dd/MM/yyyy");
            string endDate = DateTime.Today.AddDays(3).ToString("dd/MM/yyyy");
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            IWebElement tabSectionEle = driver.FindElement(tabSection);

            switch (meetingTypeExl)
            {
                case "Management Presentation + Client Meal (Optional)":
                    WebDriverWaits.WaitUntilEleVisible(driver, fieldExpectedBidDate, 10);
                    Thread.Sleep(1000);
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    driver.FindElement(fieldExpectedBidDate).Clear();
                    driver.FindElement(fieldExpectedBidDate).SendKeys(expectedBidDate);
                    WebDriverWaits.WaitUntilEleVisible(driver, dropdownExpectedBidDateSolidified, 40);
                    Thread.Sleep(1000);
                    IWebElement dropdownEl = driver.FindElement(dropdownExpectedBidDateSolidified);
                    js.ExecuteScript("arguments[0].click();", dropdownEl);
                    WebDriverWaits.WaitUntilEleVisible(driver, optionYesExpectedBidDateSolidified, 10);
                    Thread.Sleep(1000);
                    driver.FindElement(optionYesExpectedBidDateSolidified).Click();
                    Thread.Sleep(1000);
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    driver.FindElement(fieldNumberOfExpectedMeetingType).Clear();
                    driver.FindElement(fieldNumberOfExpectedMeetingType).SendKeys("5");
                    Thread.Sleep(1000);
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    driver.FindElement(fieldProvideStartDates).Clear();
                    driver.FindElement(fieldProvideStartDates).SendKeys(startDate);
                    Thread.Sleep(1000);
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    driver.FindElement(fieldProvideEndDates).Clear();
                    driver.FindElement(fieldProvideEndDates).SendKeys(endDate);
                    Thread.Sleep(1000);
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    driver.FindElement(fieldPreferredCity).Clear();
                    driver.FindElement(fieldPreferredCity).SendKeys("LA");
                    break;
                case "Client Lunch / Dinner Only":
                    WebDriverWaits.WaitUntilEleVisible(driver, fieldExpectedBidDate, 10);
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    Thread.Sleep(1000);
                    driver.FindElement(fieldExpectedBidDate).Clear();
                    driver.FindElement(fieldExpectedBidDate).SendKeys(expectedBidDate);
                    WebDriverWaits.WaitUntilEleVisible(driver, dropdownExpectedBidDateSolidified, 40);
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    IWebElement el = driver.FindElement(dropdownExpectedBidDateSolidified);
                    js.ExecuteScript("arguments[0].click();", el);
                    Thread.Sleep(1000);
                    WebDriverWaits.WaitUntilEleVisible(driver, optionYesExpectedBidDateSolidified, 10);
                    driver.FindElement(optionYesExpectedBidDateSolidified).Click();
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    Thread.Sleep(1000);
                    driver.FindElement(fieldProvideStartDates).Clear();
                    driver.FindElement(fieldProvideStartDates).SendKeys(startDate);
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    Thread.Sleep(1000);
                    driver.FindElement(fieldProvideEndDates).Clear();
                    driver.FindElement(fieldProvideEndDates).SendKeys(endDate);
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    Thread.Sleep(1000);
                    driver.FindElement(fieldPreferredCity).Clear();
                    driver.FindElement(fieldPreferredCity).SendKeys("LA");
                    break;
                case "Closing Events":
                    WebDriverWaits.WaitUntilEleVisible(driver, dropdownExpectedBidDateSolidified, 40);
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    IWebElement dropdown = driver.FindElement(dropdownExpectedBidDateSolidified);
                    js.ExecuteScript("arguments[0].click();", dropdown);
                    Thread.Sleep(1000);
                    WebDriverWaits.WaitUntilEleVisible(driver, optionYesExpectedBidDateSolidified, 10);
                    driver.FindElement(optionYesExpectedBidDateSolidified).Click();
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    Thread.Sleep(1000);
                    driver.FindElement(fieldProvideStartDates).Clear();
                    driver.FindElement(fieldProvideStartDates).SendKeys(startDate);
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    Thread.Sleep(1000);
                    driver.FindElement(fieldProvideEndDates).Clear();
                    driver.FindElement(fieldProvideEndDates).SendKeys(endDate);
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    Thread.Sleep(1000);
                    driver.FindElement(fieldPreferredCity).Clear();
                    driver.FindElement(fieldPreferredCity).SendKeys("LA");
                    break;
                case "Early look Meetings":
                    WebDriverWaits.WaitUntilEleVisible(driver, dropdownExpectedBidDateSolidified, 40);
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    IWebElement dropdownBidDate = driver.FindElement(dropdownExpectedBidDateSolidified);
                    js.ExecuteScript("arguments[0].click();", dropdownBidDate);
                    Thread.Sleep(1000);
                    WebDriverWaits.WaitUntilEleVisible(driver, optionYesExpectedBidDateSolidified, 10);
                    driver.FindElement(optionYesExpectedBidDateSolidified).Click();
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    Thread.Sleep(1000);
                    driver.FindElement(fieldNumberOfExpectedMeetingType).Clear();
                    driver.FindElement(fieldNumberOfExpectedMeetingType).SendKeys("5");
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    Thread.Sleep(1000);
                    driver.FindElement(fieldProvideStartDates).Clear();
                    driver.FindElement(fieldProvideStartDates).SendKeys(startDate);
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    Thread.Sleep(1000);
                    driver.FindElement(fieldProvideEndDates).Clear();
                    driver.FindElement(fieldProvideEndDates).SendKeys(endDate);
                    js.ExecuteScript("arguments[0].scrollIntoView();", tabSectionEle);
                    Thread.Sleep(1000);
                    driver.FindElement(fieldPreferredCity).Clear();
                    driver.FindElement(fieldPreferredCity).SendKeys("LA");
                    break;
            }
        }
        public void EditCaseDetails(string userName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconInlineEditAssignTo, 10);
            driver.FindElement(iconInlineEditAssignTo).Click();
            string startDate;
            string endDate;
            if (userName == "James Craven")
            {
                startDate = DateTime.Today.AddDays(3).ToString("dd/MM/yyyy");//dd/MM/yyyy
                endDate = DateTime.Today.AddDays(4).ToString("dd/MM/yyyy");//dd/MM/yyyy
            }
            else
            {
                startDate = DateTime.Today.AddDays(3).ToString("MM/dd/yyyy");//MM/dd/yyyy
                endDate = DateTime.Today.AddDays(4).ToString("MM/dd/yyyy");//MM/dd/yyyy
            }

            Thread.Sleep(1000);
            driver.FindElement(fieldExpectedMeetingStartDate).Clear();
            driver.FindElement(fieldExpectedMeetingStartDate).SendKeys(startDate);
            Thread.Sleep(1000);
            driver.FindElement(fieldExpectedMeetingEndDate).Clear();
            driver.FindElement(fieldExpectedMeetingEndDate).SendKeys(endDate);

        }
        public void AssignToCoOrdinator(string user)
        {
            driver.FindElement(iconInlineEditAssignTo).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconClearAssignToSelection, 5);
                driver.FindElement(iconClearAssignToSelection).Click();
                driver.FindElement(fieldAssignTo).SendKeys(user);
                WebDriverWaits.WaitUntilEleVisible(driver, comboDropdownResult, 10);
                driver.FindElement(comboDropdownResult).Click();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                driver.FindElement(fieldAssignTo).SendKeys(user);
                WebDriverWaits.WaitUntilEleVisible(driver, comboDropdownResult, 10);
                driver.FindElement(comboDropdownResult).Click();
                Thread.Sleep(2000);
            }

        }
        public bool IsBtnAcceptDisplayed()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnAccept, 20);
                return driver.FindElement(btnAccept).Displayed;
            }
            catch (Exception ex)
            {
                return false;
            }


        }
        public string ClickBtnAccept()
        {
            driver.FindElement(btnAccept).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
            string textMessage = driver.FindElement(toastMsgPopup).Text;
            driver.FindElement(toastMsgCloseIcon).Click();
            return textMessage;
        }
        public string GetCaseOwner()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, fieldCaseOwner, 10);
            return driver.FindElement(fieldCaseOwner).Text;
        }
        public void ClickFormSave()
        {
            driver.FindElement(btnFormSave).Click();
            Thread.Sleep(5000);
        }
        public string GetMeetingNumberToastMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
            string message = driver.FindElement(toastMsgPopup).Text;
            driver.FindElement(toastMsgCloseIcon).Click();
            string meeting = message.Split(' ')[1].Trim();
            string meetingNumber = meeting.Split('\"')[1].Trim();
            driver.FindElement(toastMsgCloseIcon).Click();
            return meetingNumber;
        }
        public void ClickFormCancel()
        {
            driver.FindElement(btnFormCancel).Click();
            Thread.Sleep(2000);
        }
        public string SubmitQuestionnaire()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            driver.FindElement(btnSubmitQuestionnaire).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveConfirmSubmit, 10);
            driver.FindElement(btnSaveConfirmSubmit).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 30);
            string message = driver.FindElement(toastMsgPopup).Text;
            //Thread.Sleep(2000);
            //driver.FindElement(toastMsgCloseIcone).Click();
            return message;
        }
        public string GetCaseNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, caseNumber, 5);
            return driver.FindElement(caseNumber).Text;
        }
        public void ClickCaseNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, caseNumber, 5);
            driver.FindElement(caseNumber).Click();
            Thread.Sleep(2000);
        }
        public bool IsFieldsWarningMessageDisplayed()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lvCSTformWarningmessage, 5);
                return driver.FindElement(lvCSTformWarningmessage).Displayed;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public void AddMeetingCounterparty(string companyName, string engagementName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, optionAddNewCounterparty, 5);
            driver.FindElement(optionAddNewCounterparty).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnNextAddNewCounterparty, 5);
            driver.FindElement(btnNextAddNewCounterparty).Click();
            driver.FindElement(searchCompany).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, optionNewCompany, 5);
            driver.FindElement(optionNewCompany).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnNextAddNewCounterparty, 5);
            driver.FindElement(btnNextAddNewCounterparty).Click();
            driver.FindElement(fieldCompanyName).Click();
            driver.FindElement(fieldCompanyName).SendKeys(companyName);
            driver.FindElement(btnAddCounterpartyRequiredItem).Click();
            driver.FindElement(searchEngagement).Click();
            driver.FindElement(searchEngagement).SendKeys(engagementName);


        }
        public void AddMeeting(string conterpartyName)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddMeeting, 5);
            driver.FindElement(btnAddMeeting).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerNewMeeting, 10);
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, searchCounterparty, 30);
            IWebElement counterpartySearch = driver.FindElement(searchCounterparty);
            counterpartySearch.Click();
            //this.AddMeetingCounterparty(companyName);
            counterpartySearch.SendKeys(conterpartyName);
            WebDriverWaits.WaitUntilEleVisible(driver, comboDropdownResult, 10);
            driver.FindElement(comboDropdownResult).Click();
            Thread.Sleep(2000);
        }
        public void MeetingInlineEdit()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconMeetingInlineEdit, 10);
            driver.FindElement(iconMeetingInlineEdit).Click();
        }
        public bool ValidateVenueTypeOption(string file)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,200)");
            WebDriverWaits.WaitUntilEleVisible(driver, iconDropownVenueType, 10);
            driver.FindElement(iconDropownVenueType).Click();
            Thread.Sleep(2000);
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(optionsVanueType);
            var actualValue = valTypes.Select(x => x.Text).ToArray(); string[] expectedValue = new string[5];
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            int RowVanueType = ReadExcelData.GetRowCount(excelPath, "VenueType");
            int index;
            for (int row = 2; row <= RowVanueType; row++)
            {
                index = row - 2;
                string valueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "VenueType", row, 1);
                if (valueExl == "None")
                {
                    valueExl = "--" + valueExl + "--";
                }
                expectedValue[index] = valueExl;
            }
            bool equal = actualValue.SequenceEqual(expectedValue);
            return equal;
        }
        public void SelectVanueType(string option)
        {
            IList<IWebElement> valTypes = driver.FindElements(optionsVanueType);
            for (int rec = 0; rec < valTypes.Count; rec++)
            {
                string value = valTypes[rec].Text;
                if (option.Equals(value))
                {
                    valTypes[rec].Click();
                    break;
                }
            }
        }
        public void SelectMeetingCompany(string companyName)
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtFieldSearchCompany, 30);
            IWebElement companySearch = driver.FindElement(txtFieldSearchCompany);
            companySearch.Click();
            companySearch.SendKeys(companyName);
            WebDriverWaits.WaitUntilEleVisible(driver, comboDropdownResult, 10);
            driver.FindElement(comboDropdownResult).Click();
            Thread.Sleep(2000);
        }
        public void ClickConfirmMeetingChanges()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmEditMeetingPage, 10);
            driver.FindElement(btnConfirmEditMeetingPage).Click();
            Thread.Sleep(5000);
        }
        public string GetVenueName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, venueName, 10);
            return driver.FindElement(venueName).Text;
        }
        public string GetVenueLocation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, venueLocation, 10);
            return driver.FindElement(venueLocation).Text;
        }
        public string GetPhone()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, Phone, 10);
            return driver.FindElement(Phone).Text;
        }
        public string GetWebsite()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, Website, 10);
            return driver.FindElement(Website).Text;
        }

        public string GetRelatedListQuicklinkCount(string quickLinkText, string caseNumber)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript("window.scrollTo(0,0)");
                WebDriverWaits.WaitUntilEleVisible(driver, _tabActionFor(caseNumber), 5);
                driver.FindElement(_tabActionFor(caseNumber)).Click();
                driver.FindElement(tabRefresh).Click();
                Thread.Sleep(5000);

                WebDriverWaits.WaitUntilEleVisible(driver, linksRelatedList, 5);
                IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(linksRelatedList);
                var actualValue = valTypes.Select(x => x.Text).ToArray();
                string meetingCount = "";
                for (int rec = 0; rec < actualValue.Length; rec++)
                {
                    if (actualValue[rec].Contains(quickLinkText))
                    {
                        string textRelatedQuickLink = actualValue[rec].Split('(')[1].Trim();
                        meetingCount = textRelatedQuickLink.Split(')')[0].Trim();
                        break;
                    }
                }
                return meetingCount;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }


        }

        public string GetAccessValidationMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtValidation, 10);
            return driver.FindElement(txtValidation).Text;
        }
        public string ValidateRequiredFields(string file, int row)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, listRequiredFields, 10);
            IReadOnlyCollection<IWebElement> valMeetingTypes = driver.FindElements(listRequiredFields);
            var actualValue = valMeetingTypes.Select(x => x.Text).ToArray();
            bool equal = false;
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            int formFieldsCountExl = ReadExcelData.GetColumnCount(excelPath, "CSTFormRequiredFields");
            int index;
            switch (row)
            {
                case 2:
                    string[] expectedValueM1 = new string[4];
                    for (int col = 2; col <= formFieldsCountExl; col++)
                    {
                        index = col - 2;
                        string fieldsvalueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CSTFormRequiredFields", row, col);
                        if (fieldsvalueExl.IsNullOrEmpty())
                            break;
                        expectedValueM1[index] = fieldsvalueExl;
                    }
                    equal = actualValue.SequenceEqual(expectedValueM1);
                    break;
                case 3:
                    string[] expectedValueM2 = new string[3];
                    for (int col = 2; col <= formFieldsCountExl; col++)
                    {
                        index = col - 2;
                        string fieldsvalueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CSTFormRequiredFields", row, col);
                        if (fieldsvalueExl.IsNullOrEmpty())
                            break;
                        expectedValueM2[index] = fieldsvalueExl;
                    }
                    equal = actualValue.SequenceEqual(expectedValueM2);
                    break;
                case 4:
                    string[] expectedValueM3 = new string[2];
                    for (int col = 2; col <= formFieldsCountExl; col++)
                    {
                        index = col - 2;
                        string fieldsvalueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CSTFormRequiredFields", row, col);
                        if (fieldsvalueExl.IsNullOrEmpty())
                            break;
                        expectedValueM3[index] = fieldsvalueExl;
                    }
                    equal = actualValue.SequenceEqual(expectedValueM3);
                    break;
                case 5:
                    string[] expectedValueM4 = new string[3];
                    for (int col = 2; col <= formFieldsCountExl; col++)
                    {
                        index = col - 2;
                        string fieldsvalueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CSTFormRequiredFields", row, col);
                        if (fieldsvalueExl.IsNullOrEmpty())
                            break;
                        expectedValueM4[index] = fieldsvalueExl;
                    }
                    equal = actualValue.SequenceEqual(expectedValueM4);
                    break;
                default:
                    equal = false;
                    break;
            }
            if (equal)
            {
                return "Desired Required fields are Displayed in Validation Popup";
            }
            else
            {
                {
                    return "Desired Required fields are not Displayed in Validation Popup";
                }
            }
        }

        public string IsCSTTabDisplayed()
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                js.ExecuteScript("window.scrollTo(0,100)");
                WebDriverWaits.WaitUntilEleVisible(driver, panelCST, 30);
                bool tabCSTDisplayed = driver.FindElement(panelCST).Displayed;
                if (tabCSTDisplayed)
                    return "CST tab is Displayed";
                else
                {
                    return "CST tab is not Displayed";
                }
            }
            catch (Exception ex)
            {
                return "CST tab is not Displayed";
            }
        }
        public void QuestionnaireList()
        {
            driver.FindElement(panelCST).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, panelCSTQuestionnaires, 30);
            driver.FindElement(panelCSTQuestionnaires).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, questionnaireCST, 20);
        }

        public string GetQuestionnaireListQuestionnaireNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkQuesionnaireNumber, 10);
            return driver.FindElement(linkQuesionnaireNumber).Text;
        }

        public string GetQuestionnaireListMeetingType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtMeetingType, 10);
            string meetingType = driver.FindElement(txtMeetingType).Text;
            if (meetingType == "Early Look Meetings")//Letter Case UI Issue
                return "Early look Meetings";
            else
                return meetingType;
        }
        public string GetQuestionnaireListCaseNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCaseNumber, 10);
            return driver.FindElement(linkCaseNumber).Text;
        }
        public string GetQuestionnaireListCaseStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCaseStatus, 10);
            return driver.FindElement(txtCaseStatus).Text;
        }
        public void ClickQuestionnaireLink(string caseNumber)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCaseStatus, 20);
            driver.FindElement(_linkQuestionnaireNumer(caseNumber)).Click();
        }
        public void UpdateQuestionnaire(int row)
        {
            string startDate = DateTime.Today.AddDays(3).ToString("dd/MM/yyyy");
            string endDate = DateTime.Today.AddDays(4).ToString("dd/MM/yyyy");
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,100)");
            WebDriverWaits.WaitUntilEleVisible(driver, iconInlineEdit, 20);
            driver.FindElement(iconInlineEdit).Click();
            Thread.Sleep(3000); driver.FindElement(fieldProvideStartDates).Clear();
            driver.FindElement(fieldProvideStartDates).SendKeys(startDate);
            Thread.Sleep(1000);
            driver.FindElement(fieldProvideEndDates).Clear();
            driver.FindElement(fieldProvideEndDates).SendKeys(endDate);
            Thread.Sleep(1000); if (row == 3 || row == 4)
            {
                js.ExecuteScript("window.scrollTo(0,900)");
            }
            else
            {
                js.ExecuteScript("window.scrollTo(0,1860)");
            }
            IWebElement dropdownIconInvitationPreferenceEle = driver.FindElement(dropdownIconInvitationPreference);
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownIconInvitationPreference, 5);
            dropdownIconInvitationPreferenceEle.Click();
            WebDriverWaits.WaitUntilEleVisible(driver, optionYesExpectedBidDateSolidified, 10);
            Thread.Sleep(1000);
            driver.FindElement(optionYesExpectedBidDateSolidified).Click();
            Thread.Sleep(1000);
        }
        public string DeleteCretedQuestionnaire(string tabName)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                WebDriverWaits.WaitUntilEleVisible(driver, questionnaireCST, 20);
                IList<IWebElement> questionnaire = driver.FindElements(questionnaireCST);
                string msgSuccess = "";
                while (questionnaire.Count > 0)
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, questionnaireCST, 10);
                    questionnaire[0].Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, linkDelete, 5);
                    driver.FindElement(linkDelete).Click();
                    Thread.Sleep(1000);
                    WebDriverWaits.WaitUntilEleVisible(driver, buttonDelete, 5);
                    IWebElement btnDeleteEle = driver.FindElement(buttonDelete);
                    js.ExecuteScript("arguments[0].click();", btnDeleteEle);
                    WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
                    msgSuccess = driver.FindElement(toastMsgPopup).Text;
                    driver.FindElement(toastMsgCloseIcon).Click();
                    Thread.Sleep(2000);
                    driver.FindElement(_tabActionFor(tabName)).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, tabRefresh, 5);
                    driver.FindElement(tabRefresh).Click();
                    try
                    {
                        WebDriverWaits.WaitUntilEleVisible(driver, questionnaireCST, 10);
                        questionnaire = driver.FindElements(questionnaireCST);
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }

                return msgSuccess;
            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }

        public void DeleteQuestionnaires(string tabName)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
                WebDriverWaits.WaitUntilEleVisible(driver, panelCST, 30);
                driver.FindElement(panelCST).Click();
                js.ExecuteScript("window.scrollTo(0,100)");
                WebDriverWaits.WaitUntilEleVisible(driver, panelCSTQuestionnaires, 30);
                driver.FindElement(panelCSTQuestionnaires).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, questionnaireCST, 20);
                IList<IWebElement> questionnaire = driver.FindElements(questionnaireCST);
                while (questionnaire.Count > 0)
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, questionnaireCST, 10);
                    questionnaire[0].Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, linkDelete, 5);
                    driver.FindElement(linkDelete).Click();
                    Thread.Sleep(1000);
                    WebDriverWaits.WaitUntilEleVisible(driver, buttonDelete, 5);
                    IWebElement btnDeleteEle = driver.FindElement(buttonDelete);
                    js.ExecuteScript("arguments[0].click();", btnDeleteEle);
                    WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
                    driver.FindElement(toastMsgCloseIcon).Click();
                    Thread.Sleep(2000);
                    driver.FindElement(_tabActionFor(tabName)).Click();
                    //driver.FindElement(tabDropdown).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, tabRefresh, 5);
                    driver.FindElement(tabRefresh).Click();
                    try
                    {
                        WebDriverWaits.WaitUntilEleVisible(driver, questionnaireCST, 10);
                        questionnaire = driver.FindElements(questionnaireCST);
                    }
                    catch (Exception ex)
                    {
                        break;
                    }

                }

                WebDriverWaits.WaitUntilEleVisible(driver, imgProfile, 150);
                driver.FindElement(imgProfile).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, lnkSwitchToClassic, 120);
                driver.FindElement(lnkSwitchToClassic).Click();
                //Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, imgProfile, 150);
                driver.FindElement(imgProfile).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, lnkSwitchToClassic, 120);
                driver.FindElement(lnkSwitchToClassic).Click();
                Thread.Sleep(2000);
            }




        }
        //To get name of Page
        public string GetTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, titleEngPage, 100);
            string title = driver.FindElement(titleEngPage).Text;
            return title;
        }

        public string GetEngName()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valEngName, 110);
            string name = driver.FindElement(valEngName).Text;
            return name;
        }

        public string GetEngNumber()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valEngNum, 110);
            string num = driver.FindElement(valEngNum).Text;
            return num;
        }

        public string GetStage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStage, 80);
            string stage = driver.FindElement(valStage).Text;
            return stage;
        }

        //Get Engagement Number
        public string GetEngagementNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEngNum, 90);
            string Name = driver.FindElement(valEngNum).Text;
            return Name;
        }

        public string GetRecordType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRecordType, 60);
            string recordType = driver.FindElement(valRecordType).Text;
            return recordType;
        }

        public string GetHLEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valHLEntity, 60);
            string HLEntity = driver.FindElement(valHLEntity).Text;
            return HLEntity;
        }

        public string GetWomenLed()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valWomenLed, 60);
            string value = driver.FindElement(valWomenLed).Text;
            return value;
        }

        public bool IsEngExternalContactPresent()
        {
            try
            {
                driver.Navigate().Refresh();
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, valEngContact, 30);
                    return driver.FindElement(valEngContact).Displayed;
                }
                catch (Exception ex)
                {
                    driver.Navigate().Refresh();
                    WebDriverWaits.WaitUntilEleVisible(driver, valEngContact, 30);
                    return driver.FindElement(valEngContact).Displayed;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        
        public string GetEngDealTeamMember()
        {

            try
            {
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='HL_EngagementInternalTeamView']")));
                string value = driver.FindElement(valEngInternalMemberMulti).Text.Trim();
                driver.SwitchTo().DefaultContent();
                return value;
            }
            catch (Exception e)
            {
                driver.Navigate().Refresh();
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='HL_EngagementInternalTeamView']")));
                string value = driver.FindElement(valEngInternalMemberMulti).Text.Trim();
                driver.SwitchTo().DefaultContent();
                return value;
            }
        }

        public string GetEngExternalContact()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valEngContact2, 30);
                return driver.FindElement(valEngContact2).Text.Trim();
            }
            catch (Exception e)
            {
                driver.Navigate().Refresh();
                WebDriverWaits.WaitUntilEleVisible(driver, valEngContact2, 30);
                return driver.FindElement(valEngContact2).Text.Trim();
            }

        }
        public string GetLegalEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLegalEntity, 60);
            string HLEntity = driver.FindElement(valHLEntity).Text;
            return HLEntity;
        }
        //To clear Engagement number and save it
        public string ClearEngNumberAndSave()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngNum, 60);
            driver.FindElement(txtEngNum).Clear();
            driver.FindElement(btnSave).Click();
            string engNum = driver.FindElement(valEngNum).Text;
            return engNum;
        }
        //Validate Portfolio Valuation button and click on it
        public void ClickPortfolioValuation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnPortfolioValuation, 90);
            driver.FindElement(btnPortfolioValuation).Click();
        }

        //To Validate FR Engagement Summary button 
        public string ValidateFREngSummaryButton()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnFREngSummary, 150);
            string valFREngSum = driver.FindElement(btnFREngSummary).Displayed.ToString();
            return valFREngSum;
        }

        //To Validate FR Engagement Summary button in Lightning
        public string ValidateFREngSummaryButtonInLightning()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnFREngSummaryL, 180);
            string valFREngSum = driver.FindElement(btnFREngSummaryL).Displayed.ToString();
            return valFREngSum;
        }
        //Click FR Engagement Summary button 
        public string ClickFREngSummaryButton()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnFREngSummary, 150);
            driver.FindElement(btnFREngSummary).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleFREngSum, 90);
            string title = driver.FindElement(titleFREngSum).Text;
            return title;
        }

        //Click FR Engagement Summary button on Lightning
        public string ClickFREngSummaryButtonL()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnFREngSummaryL, 150);
            driver.FindElement(btnFREngSummaryL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabDefault, 90);
            string title = driver.FindElement(tabDefault).Text;
            return title;
        }

        public string ValidateFinancialsProjectionsTabL()
        {
            Thread.Sleep(4000);
            driver.FindElement(tabFinancialsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabFinancialsL, 90);
            string title = driver.FindElement(tabFinancialsL).Text;
            return title;
        }

        public string ValidateDMAndInformationTabL()
        {
            Thread.Sleep(4000);
            driver.FindElement(tabDMAL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabDMAL, 90);
            string title = driver.FindElement(tabDMAL).Text;
            return title;
        }

        public string ValidateHLFinancingTabL()
        {
            Thread.Sleep(4000);
            driver.FindElement(tabHLFinancingL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabHLFinancingL, 90);
            string title = driver.FindElement(tabHLFinancingL).Text;
            return title;
        }

        public string ValidatePreTransTabL()
        {
            Thread.Sleep(4000);
            driver.FindElement(tabPreTransL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabPreTransL, 90);
            string title = driver.FindElement(tabPreTransL).Text;
            return title;
        }
        public string ValidatePostTransTabL()
        {
            Thread.Sleep(4000);
            driver.FindElement(tabPostTransL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabPostTransL, 90);
            string title = driver.FindElement(tabPostTransL).Text;
            return title;
        }
        public string ValidateHLPostTransOppTabL()
        {
            Thread.Sleep(4000);
            driver.FindElement(tabHLPostTransL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabHLPostTransL, 90);
            string title = driver.FindElement(tabHLPostTransL).Text;
            return title;
        }

        //Get value of Transaction Type
        public string GetValueOfTransactionTypeL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabClientL, 150);
            driver.FindElement(tabClientL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valTxnType, 90);
            string title = driver.FindElement(valTxnType).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, tabInfo, 150);
            driver.FindElement(tabInfo).Click();
            return title;

        }

        //Get value of Post Transaction Status
        public string GetValueOfPostTransactionStatusL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPostTxnStatus, 90);
            string title = driver.FindElement(valPostTxnStatus).Text;
            return title;

        }

        //Get value of Company Description
        public string GetValueOfCompDescL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompDesc, 90);
            string title = driver.FindElement(valCompDesc).Text;
            return title;

        }

        //Get value of Business Description
        public string GetValueOfBusinessDescL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valBusDesc, 110);
            string title = driver.FindElement(valBusDesc).Text;
            return title;

        }

        //Get value of Restructuring Description
        public string GetValueOfRestructuringDescL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valReDesc, 100);
            string title = driver.FindElement(valReDesc).Text;
            return title;

        }


        //Get value of Total Estimated Fee
        public string GetTotalEstFee()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtTotalEstFees, 90);
            string Fees = driver.FindElement(txtTotalEstFees).Text;
            return Fees;
        }

        //To update value of Total Estimated Fee
        public void UpdateTotalEstFee(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtTotalEstFeesFAS, 90);
            driver.FindElement(txtTotalEstFeesFAS).Clear();
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(txtTotalEstFeesFAS).SendKeys(value);
            driver.FindElement(btnSave).Click();
        }

        //Get value of Total Estimated Fee FAS
        public string GetTotalEstFeeFAS()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTotalEstFeesFAS, 90);
            string Fees = driver.FindElement(valTotalEstFeesFAS).Text;
            return Fees;
        }

        //To get month from created Revenue Accrual record
        public string GetMonthFromRevenueAccrualRecord()
        {
            Thread.Sleep(1000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.Navigate().Refresh();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valYearMonth, 170);
            string value = driver.FindElement(valYearMonth).Text;
            return value;
        }
        //To get month from created Revenue Accrual record
        public string GetMonthFromRevenueAccrualRecordL()
        {
            By eleJobType = By.XPath("//tr[1]/th//span/a[2]");
            Thread.Sleep(4000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,750)");
            Thread.Sleep(5000);
            string value = driver.FindElement(valYearMonthL).Text;
            return value;
        }

        //To get value of Total Estimated Fee from created Revenue Accrual record
        public string GetTotalEstFeeFromRevenueAccrualRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTotalEstFee, 100);
            string value = driver.FindElement(valTotalEstFee).Text;
            return value;
        }

        //Delete any existing Revenue Accurals
        public void DeleteExistingAccurals()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRevAccural, 100);
            string value = driver.FindElement(valRevAccural).Text;
            if (value.Equals("Action"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkDeleteAccurals, 110);
                driver.FindElement(lnkDeleteAccurals).Click();
                Thread.Sleep(3000);
                /* IJavaScriptExecutor js = driver as IJavaScriptExecutor;
                 js.ExecuteScript("window.confirm = function() { return true;}");
                 js.ExecuteScript("arguments[0].click()");*/
                try
                {
                    IAlert alert = driver.SwitchTo().Alert();
                    alert.Accept();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("No existing reveue accurals");
                Thread.Sleep(2000);
            }
        }

        //Click Add Revenue Accurals 
        public string AddNewRevenueAccurals()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddRevenueAccurals, 90);
            driver.FindElement(btnAddRevenueAccurals).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtPeriodAccuredFees, 90);
            driver.FindElement(txtPeriodAccuredFees).SendKeys("10");
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valPeriodAccrual, 100);
            string value = driver.FindElement(valPeriodAccrual).Text;
            return value;
        }

        //To get Period Accured Fee Net value from Revenue Accrual record
        public string GetPeriodAccrualFeeNetValue()
        {
            Thread.Sleep(5000);
            driver.Navigate().Refresh();
            Thread.Sleep(8000);
            string value = driver.FindElement(valPeriodAccrual).Text;
            return value;
        }

        //To get Period Accrued Fees of FAS record 
        public string GetPeriodAccrualValueFAS()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valPeriodAccrualFee, 120);
            string value = driver.FindElement(valPeriodAccrualFee).Text;
            return value;
        }

        //To get value of Period Accrual value from engagement details
        public string GetPeriodAccrualValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPeriodAccrualFee, 120);
            string value = driver.FindElement(valPeriodAccrualFee).Text;
            return value;
        }

        //To update stage of Engagement
        public void UpdateStage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 150);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(txtStage).SendKeys("Retained");
            driver.FindElement(btnSave).Click();
        }

        //To get message when no Revenue Accrual exists
        public string GetRevAccrualMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRevAccural, 120);
            string message = driver.FindElement(valRevAccural).Text;
            return message;
        }

        //To update Engagement contact details
        public string UpdateEngContact(string Name, string LOB)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditContact, 70);
            driver.FindElement(lnkEditContact).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtContact, 70);
            driver.FindElement(txtContact).Clear();
            driver.FindElement(txtContact).SendKeys(Name);
            if (LOB.Equals("CF"))
            {
                driver.FindElement(txtParty).SendKeys("Buyer");
                driver.FindElement(btnSave).Click();
            }
            else
            {
                driver.FindElement(btnSave).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, valContact, 90);
            Thread.Sleep(2000);
            string contact = driver.FindElement(valContact).Text;
            return contact;
        }
        //To update Engagement contact details
        public string UpdateEngContactL(string Name, string LOB, string Last)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabContactsL, 70);
            driver.FindElement(tabContactsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContactL, 90);
            driver.FindElement(lnkContactL).Click();
            Thread.Sleep(6000);
            driver.FindElement(btnEditContactL).Click();
            Thread.Sleep(5000);
            driver.FindElement(txtFirstNameL).Clear();
            driver.FindElement(txtFirstNameL).SendKeys(Name);
            Thread.Sleep(4000);
            driver.FindElement(txt2ndNameL).Clear();
            driver.FindElement(txt2ndNameL).SendKeys(Last);
            if (Name.Equals("Tatiana"))
            {
                Console.WriteLine("LOB is CF");
                driver.FindElement(txtTitleL).Clear();
                driver.FindElement(txtEmailL).Clear();
            }
            else
            {
                driver.FindElement(txtEmailL).SendKeys("abc@yopmail.com");
                driver.FindElement(txtTitleL).SendKeys("Ms.");
            }
            driver.FindElement(btnSaveDetailsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valContactL, 90);
            Thread.Sleep(4000);
            string contact = driver.FindElement(valContactL).Text;
            return contact;
        }

        //To click on billing request button
        public void ClickBillingRequestButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBillingRequest, 70);
            driver.FindElement(btnBillingRequest).Click();
        }
        //To click on billing request button
        public void ClickBillingRequestButtonL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngagementNumL, 70);
            driver.FindElement(tabEngagementNumL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnBillingRequestL, 70);
            driver.FindElement(btnBillingRequestL).Click();
        }
        //To get validation message for contact details
        public string GetContactValidationMessageL()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='accessibility title']")));
            WebDriverWaits.WaitUntilEleVisible(driver, msgContactL, 100);
            string message = driver.FindElement(msgContactL).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToEngagementL, 70);
            driver.FindElement(btnBackToEngagementL).Click();
            driver.SwitchTo().DefaultContent();
            return message;

        }

        //To get validation message for contact details
        public string GetContactValidationMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgContact, 70);
            string message = driver.FindElement(msgContact).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToManagement, 70);
            driver.FindElement(btnBackToManagement).Click();
            return message;
        }

        //To get Subject from Billing Request Form
        public string GetTitleOfSendEmailButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSendEmail, 70);
            string title = driver.FindElement(btnSendEmail).GetAttribute("value");
            return title;
        }
        //To get Subject from Billing Request Form
        public string GetTitleOfSendEmailButtonL()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='accessibility title']")));
            WebDriverWaits.WaitUntilEleVisible(driver, btnSendEmailL, 70);
            string title = driver.FindElement(btnSendEmailL).GetAttribute("value");
            driver.SwitchTo().DefaultContent();
            return title;
        }

        //To get Subject from Billing Request Form
        public string GetTitleOfBillingReqForm()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, titleBillingForm, 70);
            string title = driver.FindElement(titleBillingForm).Text;
            return title;
        }

        public string NavigateToCFEngagementSummaryPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCFEngagementSummary, 120);
            driver.FindElement(btnCFEngagementSummary).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblHeaderText, 140);
            string h1Text = driver.FindElement(lblHeaderText).Text;
            Thread.Sleep(10000);
            return h1Text;
        }

        public bool VerifyFiltersFunctionalityOnCoverageSectorDependencyPopUp(string file, string covSectorDependencyName)
        {
            bool result = false;
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            //Click Edit button on Company Sector detail page 
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditCompCoverageSector, 120);
            driver.FindElement(btnEditCompCoverageSector).Click();
            Thread.Sleep(2000);

            //Click on Coverage Sector Dependency LookUp icon
            WebDriverWaits.WaitUntilEleVisible(driver, imgCoverageSectorDependencyLookUp, 120);
            driver.FindElement(imgCoverageSectorDependencyLookUp).Click();
            Thread.Sleep(2000);

            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);

            //Enter search frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("searchFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='searchFrame']")));
            Thread.Sleep(2000);

            //Clear Search box
            driver.FindElement(txtSearchBox).Clear();
            driver.SwitchTo().DefaultContent();

            //Enter results frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("resultsFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='resultsFrame']")));
            Thread.Sleep(2000);           

            //Enter filter values
            driver.FindElement(inputCoverageType).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 1));
            driver.FindElement(inputPrimarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 2));
            driver.FindElement(inputSecondarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 3));
            driver.FindElement(inputTertiarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 4));

            //Click on Apply filters button
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(2000);

            if (driver.FindElement(linkCoverageSectorDependencyName).Text == covSectorDependencyName)
            {
                //Select the desired dependency name from the result
                driver.FindElement(linkCoverageSectorDependencyName).Click();
                Thread.Sleep(4000);

                //Switch back to original window
                CustomFunctions.SwitchToWindow(driver, 0);

                result = true;
            }
            return result;
        }


        //To update Accounting Status and stage
        public void UpdateAccountingStatusAndStage(string Status, string Stage)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboAccountingStatus).SendKeys(Status);
            driver.FindElement(comboStage).SendKeys(Stage);
            driver.FindElement(btnSave).Click();
        }

        //To get value of Accounting Status
        public string GetAccontingStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAccountingStatus, 90);
            string value = driver.FindElement(valAccountingStatus).Text;
            return value;
        }

        //To check if Available in Expense Application checkbox is checked or not
        public string ValidateIfExpenseApplicationIsChecked()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chkExpApplication, 70);
            string value = driver.FindElement(chkExpApplication).Selected.ToString();
            if (value.Equals("True"))
            {
                return "Expense Application checkbox is checked";
            }
            else
            {
                return "Expense Application checkbox is not checked";
            }
        }

        //Get Revenue record number
        public string GetRevenueRecordNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRevenueMonth, 80);
            driver.FindElement(lnkRevenueMonth).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valRevID, 80);
            //string id = driver.FindElement(valRevID).Text;
            string id = driver.Url;
            driver.FindElement(lnkEngagement).Click();
            return id;
        }
        //Get Revenue record number
        public string GetRevenueRecordNumberL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valYearMonthL, 80);
            driver.FindElement(valYearMonthL).Click();
            //driver.SwitchTo().Frame(0);
            Thread.Sleep(7000);
            string id = driver.Url;
            Thread.Sleep(5000);
            driver.FindElement(tabEngagementNumL).Click();
            Thread.Sleep(5000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,-750)");
            return id;
        }
         

        //Create new Revenue Accrual record
        public string AddRevenueAccrualL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAccural, 80);
            driver.FindElement(btnAccural).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnSaveBacklog).Click();
            Thread.Sleep(4000);
            string message = driver.FindElement(errorMessageL).Text;
            return message;
        }

        //Create new Revenue Accrual record
        public string AddRevenueAccrual()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddRevenueAccrual, 80);
            driver.FindElement(btnAddRevenueAccrual).Click();
            driver.FindElement(btnSave).Click();
            string message = driver.FindElement(errorMessage).Text.Replace("\r\n", " ");
            return message;
        }

        //Click Counterparties button 
        public string ClickCounterpartiesButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCounterParties, 120);
            driver.FindElement(btnCounterParties).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleEngPage, 90);
            string title = driver.FindElement(titleEngPage).Text;
            return title;
        }
        //Validate the visibility of Portfolio Valuation button
        public string ValidatePortfolioValuationButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 60);
            Thread.Sleep(4000);
            try
            {
                string value = driver.FindElement(btnPortfolioValuation).Displayed.ToString();
                Console.WriteLine("Portfolio Valuation button: " + value);
                return "Portfolio Valuation button is displayed";
            }
            catch (Exception)
            {
                return "Portfolio Valuation button is not displayed";
            }
        }

        //Click on Engagement tab
        public void ClickEngagementTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngagement, 80);
            driver.FindElement(tabEngagement).Click();
        }
        //Click on Engagement tab
        public void ClickEngagementTabL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngagementL, 80);
            driver.FindElement(tabEngagementL).Click();
        }      
        

        //Update Client Ownership and Total Debt
        public string UpdateClientOwnershipAndDebtL(string Ownership, string Debt)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 80);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboClientOwnershipL, 100);
            driver.FindElement(comboClientOwnershipL).SendKeys(Ownership);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//lightning-base-combobox-item/span[2]/span[text()='" + Ownership + "']")).Click();

            driver.FindElement(btnSaveDetailsL).Click();
            string clientOwnership = driver.FindElement(valClientOwnershipL).Text;
            return clientOwnership;
        }

        //Update Client Ownership and Total Debt
        public string UpdateClientOwnershipAndDebt(string Ownership, string Debt)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(4000);
            driver.FindElement(comboClientOwnership).SendKeys(Ownership);
            //driver.FindElement(txtDebt).Clear();
            //driver.FindElement(txtDebt).SendKeys(Debt);
            driver.FindElement(btnSave).Click();
            string clientOwnership = driver.FindElement(valClientOwnership).Text;
            return clientOwnership;
        }

        //To get Debt
        public string GetTotalDebt()
        {
            string Debt = driver.FindElement(valTotalDebt).Text;
            return Debt;
        }

        //To update Record Type of Engagement
        public string UpdateRecordType(string Type)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRecChange, 120);
            driver.FindElement(lnkRecChange).Click();
            driver.FindElement(comboRecType).SendKeys(Type);
            driver.FindElement(btnContinue).Click();
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valRecordType, 100);
            string value = driver.FindElement(valRecordType).Text;
            return value;
        }

        //To update stage of Engagement
        public string UpdateEngStage(string Stage)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
                driver.FindElement(btnEdit).Click();
                if (Stage.Equals("Dead"))
                {
                    driver.FindElement(txtStage).SendKeys(Stage);
                    driver.FindElement(txtComments).SendKeys("Test Comments");
                }
                else if (Stage.Equals("Opinion Report"))
                {
                    driver.FindElement(txtStage).SendKeys(Stage);
                    driver.FindElement(txtComments).Clear();
                }
                else if (Stage.Equals("Bill/File"))
                {
                    driver.FindElement(txtStage).SendKeys(Stage);
                    driver.FindElement(lnkFinalReport).Click();
                }
                else
                {
                    driver.FindElement(txtStage).SendKeys(Stage);
                }
                driver.FindElement(btnSave).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valStage, 120);
                string value = driver.FindElement(valStage).Text;
                return value;
            }
            catch (Exception)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 100);
                driver.FindElement(lnkReDisplayRec).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
                driver.FindElement(btnEdit).Click();
                if (Stage.Equals("Dead"))
                {
                    driver.FindElement(txtStage).SendKeys(Stage);
                    driver.FindElement(txtComments).SendKeys("Test Comments");
                }
                else if (Stage.Equals("Opinion Report"))
                {
                    driver.FindElement(txtStage).SendKeys(Stage);
                    driver.FindElement(txtComments).Clear();
                }
                else if (Stage.Equals("Bill/File"))
                {
                    driver.FindElement(txtStage).SendKeys(Stage);
                    driver.FindElement(lnkFinalReport).Click();
                }
                else
                {
                    driver.FindElement(txtStage).SendKeys(Stage);
                }
                driver.FindElement(btnSave).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valStage, 120);
                string value = driver.FindElement(valStage).Text;
                return value;
            }
        }

        //To get value of POC
        public string GetPOCValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            string value = driver.FindElement(valPOC).Text;
            return value;
        }

        //Get ERP Submitted To Sync in ERP section
        public string GetERPSubmittedToSync()
        {
            Thread.Sleep(2000);
            driver.Navigate().Refresh();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valERPSubmittedToSync, 120);
            string syncDate = driver.FindElement(valERPSubmittedToSync).Text;
            return syncDate;
        }

        //Get ERP ID in ERP section
        public string GetERPID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPID, 80);
            string id = driver.FindElement(valERPID).Text;
            return id;
        }

        //Get ERP HL Entity in ERP section
        public string GetERPHLEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPHLEntity, 80);
            string ERPEntity = driver.FindElement(valERPHLEntity).Text;
            return ERPEntity;
        }

        //Get ERP Legal Entity in ERP section
        public string GetERPLegalEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityId, 80);
            string ERPEntity = driver.FindElement(valERPLegalEntityId).Text;
            return ERPEntity;
        }


        //Get ERP Project Number in ERP section
        public string GetERPProjectNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjectNumber, 80);
            string Number = driver.FindElement(valERPProjectNumber).Text;
            return Number;
        }

        //Get ERP Project Name in ERP section
        public string GetERPProjectName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjectName, 80);
            string Number = driver.FindElement(valERPProjectName).Text;
            return Number;
        }
        //Get LOB
        public string GetLOB()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLOB);
            string LOB = driver.FindElement(valLOB).Text;
            return LOB;
        }
        //Get LOB
        public string GetLOBL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLOBL);
            string LOB = driver.FindElement(valLOBL).Text;
            return LOB;
        }
        public string GetEngClientCompanyL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valClientCompL);
            string LOB = driver.FindElement(valClientCompL).Text;
            return LOB;
        }

        //Get ERP LOB
        public string GetERPLOB()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLOB);
            string LOB = driver.FindElement(valERPLOB).Text;
            return LOB;
        }

        //Get Industry Group
        public string GetIndustryGroup()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valIG);
            string IG = driver.FindElement(valIG).Text.Substring(0, 3);
            return IG;
        }
        //Get ERP Industry Group
        public string GetERPIndustryGroup()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPIG, 80);
            string IG = driver.FindElement(valERPIG).Text;
            return IG;
        }

        //Get ERP Last Integration Status
        public string GetERPIntegrationStatus()
        {
            Thread.Sleep(8000);
            driver.Navigate().Refresh();
            Thread.Sleep(2000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntStatus, 80);
            string status = driver.FindElement(valERPLastIntStatus).Text;
            return status;
        }

        //Get ERP Last Integration Response Date
        public string GetERPIntegrationResponseDate()
        {
            Thread.Sleep(7000);
            driver.Navigate().Refresh();
            Thread.Sleep(4000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valERPResponseDate, 120);
            string date = driver.FindElement(valERPResponseDate).Text;
            return date;
        }

        //Get ERP Last Integration Error Description
        public string GetERPIntegrationError()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPError, 80);
            string error = driver.FindElement(valERPError).Text;
            return error;
        }

        //Get Job Type
        public string GetJobType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valJobType, 80);
            string type = driver.FindElement(valJobType).Text;
            return type;
        }
        //Get Job Type
        public string GetJobTypeL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valJobTypeL, 190);
            string jobType = driver.FindElement(valJobTypeL).Text;
            return jobType;
        }

        //Click Portfolio valuation button and get title of page
        public void ClickPortfolioValuationL()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnPortfolioVL, 120);
                driver.FindElement(btnPortfolioVL).Click();
                Thread.Sleep(9000);
                driver.SwitchTo().Frame(0);
                Thread.Sleep(8000);

            }
            catch (Exception)
            {
                driver.SwitchTo().Frame(6);
                Thread.Sleep(9000);
            }
        }
        //Fetch Validate imported valuation period upon conversion
        public string ValidateImportedValPeriod()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, valImportedValPeriod, 190);
            string addedPeriod = driver.FindElement(valImportedValPeriod).Text;
            return addedPeriod;
        }

        //Fetch Validate imported period position upon conversion
        public string ValidateImportedPeriodPosition(string name)
        {
            driver.FindElement(By.XPath("//a[text()='" + name + "']")).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valImportedPositionL, 190);
            string addedPosition = driver.FindElement(valImportedPositionL).Text;
            return addedPosition;
        }

        //Validate Back To Engagement button
        public string ValidateReturnToEngButton()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, btnBackToEngL, 250);
            Thread.Sleep(6000);
            string value = driver.FindElement(btnBackToEngL).GetAttribute("value");
            return value;
        }

        //Validate Engagement details page upon clicking Back to Engagement button
        public string ValidateEngDetailsPageUponClickOfBackToEngButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToEngL, 120);
            driver.FindElement(btnBackToEngL).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(4000);
            string tab = driver.FindElement(tabDetails).Text;
            driver.FindElement(tabDetails).Click();
            return tab;

        }

        //Validate New Eng Valuation Period button
        public string ValidateNewEngValuationPeriodButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewEngValPeriodL, 120);
            string value = driver.FindElement(btnNewEngValPeriodL).GetAttribute("value");
            return value;

        }
        //Get ERP Product Type
        public string GetERPProductType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProductType, 80);
            string type = driver.FindElement(valERPProductType).Text;
            return type;
        }

        //Get ERP Product Type Code
        public string GetERPProductTypeCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProductTypeCode, 80);
            string code = driver.FindElement(valERPProductTypeCode).Text;
            return code;
        }

        //Get ERP Template
        public string GetERPTemplate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPTemplate, 80);
            string number = driver.FindElement(valERPTemplate).Text;
            return number;
        }

        //Get ERP Business Unit ID
        public string GetERPBusinessUnitID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPUnitID, 80);
            string number = driver.FindElement(valERPUnitID).Text;
            return number;
        }
        //Get ERP Business Unit
        public string GetERPBusinessUnit()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPUnit, 80);
            string unit = driver.FindElement(valERPUnit).Text;
            return unit;
        }

        //Get ERP Legal Entity ID
        public string GetERPLegalEntityID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityID, 80);
            string id = driver.FindElement(valERPLegalEntityID).Text;
            return id;
        }

        //Get ERP Entity Code
        public string GetERPEntityCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPEntityCode, 80);
            string code = driver.FindElement(valERPEntityCode).Text;
            return code;
        }
        //Get ERP Legislation Code
        public string GetERPLegCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegCode, 80);
            string code = driver.FindElement(valERPLegCode).Text;
            return code;
        }

        //Update Primary Office
        public string UpdatePrimaryOffice(string value)
        {
            Thread.Sleep(50000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboPrimaryOffice).SendKeys(value);
            driver.FindElement(btnSave).Click();
            string office = driver.FindElement(valPrimaryOffice).Text;
            return office;
        }
        //Get ERP Update DFF
        public string GetERPUpdateDFFIfChecked()
        {
            //Thread.Sleep(2000);
            //driver.Navigate().Refresh();            
            WebDriverWaits.WaitUntilEleVisible(driver, checkERPUpdateDFF, 100);
            string value = driver.FindElement(checkERPUpdateDFF).GetAttribute("title");
            if (value.Equals("Not Checked"))
            {
                return "Checkbox is not checked";
            }
            else
            {
                return "Checkbox is checked";
            }
        }

        //Update Industry Group
        public string UpdateIndustryGroup(string group)
        {
            Thread.Sleep(50000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboIG).SendKeys(group);
            driver.FindElement(comboSector).SendKeys("Dental");
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valIG, 100);
            string IG = driver.FindElement(valIG).Text;
            return IG;
        }

        //Update Sector
        public string UpdateSector(string sector)
        {
            Thread.Sleep(50000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboSector).SendKeys(sector);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valSector, 100);
            string Sector = driver.FindElement(valSector).Text;
            return Sector;
        }
        //To update Job type for ERP
        public string UpdateJobTypeERP(string jobType)
        {
            driver.Navigate().Refresh();
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 100);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType, 80);
            driver.FindElement(comboJobType).SendKeys(jobType);
            driver.FindElement(btnSave).Click();
            string type = driver.FindElement(valJobType).Text;
            return type;
        }
        //Update Client Ownership
        public string UpdateClientOwnership(string client)
        {
            driver.Navigate().Refresh();
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboClientOwnership).SendKeys(client);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valClientOwnership, 120);
            string Client = driver.FindElement(valClientOwnership).Text;
            return Client;
        }
        //To update ERP Record Type
        public string UpdateRecordTypeAndLOBERP()
        {
            driver.Navigate().Refresh();
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRecordTypeChange, 120);
            driver.FindElement(lnkRecordTypeChange).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboRecType, 90);
            driver.FindElement(comboRecType).SendKeys("Buyside");
            driver.FindElement(btnContinue).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboLOB, 90);
            driver.FindElement(comboLOB).SendKeys("CF");
            driver.FindElement(comboJobType).SendKeys("Buyside");
            driver.FindElement(btnSave).Click();
            string LOB = driver.FindElement(valLOB).Text;
            return LOB;
        }

        //To schedule ERP Submitted to Sync manually
        public void ScheduleERPSyncManually()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkSyncDate, 90);
            driver.FindElement(lnkSyncDate).Click();
            driver.FindElement(btnSave).Click();
        }

        //To validate if contract is created or not
        public string ValidateContractExists()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, rowContract, 100);
                string id = driver.FindElement(rowContract).Text;
                return id;
            }
            catch (Exception)
            {
                return "Contract does not exist";
            }
        }
        //Get the type of Contract
        public string GetFinalReportSentDate()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabExistingEng, 110);
            driver.FindElement(tabExistingEng).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabInfo2ndL, 130);
            driver.FindElement(tabInfo2ndL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabImpDates, 120);
            driver.FindElement(tabImpDates).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valFinalReportL, 140);
            string value = driver.FindElement(valFinalReportL).Text;
            return value;
        }

        //Update stage in Details tab
        public string UpdateStageInDetailsTab()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabDetails, 120);
            driver.FindElement(tabDetails).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkStageL, 140);
            driver.FindElement(lnkStageL).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnStageL).Click();
            Thread.Sleep(5000);
            driver.FindElement(valStageL).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, valSavedStageL, 240);
            string value = driver.FindElement(valSavedStageL).Text;
            return value;
        }
        //Get the Revenue Accrual
        public string GetRevenueAccrual()
        {
            Thread.Sleep(7000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,600)");
            WebDriverWaits.WaitUntilEleVisible(driver, valRevAccrualL, 140);
            string value = driver.FindElement(valRevAccrualL).Text;
            Console.WriteLine("value:" + value);
            //IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,-600)");
            Thread.Sleep(5000);
            driver.FindElement(tabPositionL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(6000);
            Console.WriteLine("value: " + value);
            return value;
        }
        //Get the type of Contract
        public string GetERPContractType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContractType, 100);
            string type = driver.FindElement(valERPContractType).Text;
            return type;
        }

        //Get Contract ERP Business Unit
        public string GetContractERPBusinessUnit()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusUnit, 100);
            string unit = driver.FindElement(valERPBusUnit).Text;
            return unit;
        }

        //Get Contract ERP Legal Entity Name
        public string GetContractERPLegalEntityName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntity, 100);
            string entity = driver.FindElement(valERPLegalEntity).Text;
            return entity;
        }

        //Get Contract ERP Bill Plan
        public string GetContractERPBillPlan()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBillPlan, 100);
            string plan = driver.FindElement(valERPBillPlan).Text;
            return plan;
        }

        //Get Contract Bill To
        public string GetContractBillTo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valBillTo, 100);
            string bill = driver.FindElement(valBillTo).Text;
            return bill;
        }

        //Get Company name of contact
        public string GetCompanyNameOfContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompName, 100);
            string name = driver.FindElement(valCompName).Text;
            return name;
        }

        //Get Contract Start Date
        public string GetContractStartDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStartDate, 100);
            string date = driver.FindElement(valStartDate).Text;
            return date;
        }

        //Get if Main Contract checkbox is checked
        public string GetIfIsMainContractChecked()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valIsMain, 100);
            string main = driver.FindElement(valIsMain).GetAttribute("title");
            if (main.Equals("Checked"))
            {
                return "Is Main Contract checkbox is checked";
            }
            else
            {
                return "Is Main Contract checkbox is not checked";
            }
        }
        //Get if Main Contract checkbox is checked
        public string GetIfIsMainContractCheckedL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnk2ndContractL, 100);
            driver.FindElement(lnk2ndContractL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, checkIsMainL, 90);
            string main = driver.FindElement(checkIsMainL).Text;
            Console.WriteLine("main:" + main);
            if (main.Equals(""))
            {
                driver.FindElement(lnkOppL).Click();
                Thread.Sleep(4000);
                driver.FindElement(btnCloseReqEngFVAL).Click();
                return "Is Main Contract checkbox is checked";
            }
            else
            {
                driver.FindElement(lnkOppL).Click();
                Thread.Sleep(4000);
                driver.FindElement(btnCloseReqEngFVAL).Click();
                return "Is Main Contract checkbox is not checked";
            }

        }


        //Get 1st contract name
        public string Get1stContractName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContract1, 100);
            string name = driver.FindElement(valContract1).Text;
            return name;
        }
        //Get 1st contract name
        public string Get1stContractNameL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabRevenue, 100);
            driver.FindElement(tabRevenue).Click();
            Thread.Sleep(5000);
            driver.FindElement(subtabContracts).Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,650)");
            WebDriverWaits.WaitUntilEleVisible(driver, valContract1L, 180);
            string name = driver.FindElement(valContract1L).Text;
            return name;
        } 

        //Get 2nd contract name
        public string Get2ndContractNameL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContract2L, 100);
            string name = driver.FindElement(valContract2L).Text;
            return name;
        }

        //Get 2nd contract name
        public string Get2ndContractName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContract2, 100);
            string name = driver.FindElement(valContract2).Text;
            return name;
        }

        //Click on Related Opportunity link
        public string ClickRelatedOpportunityLink()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkOpp, 100);
            driver.FindElement(lnkOpp).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleEngPage, 100);
            string name = driver.FindElement(titleEngPage).Text;
            return name;
        }
        
        public string GetSectionNameOfWomenLedFieldLV(string JobType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSecWomenLedLV, 10);
            return driver.FindElement(txtSecWomenLedLV).Text; 
        }
        //Get section name of Women Led in Engagement details page
        public string GetSectionNameOfWomenLedField(string JobType)
        {
            if (JobType.Equals("Buyside"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSecWomenled, 10);
                string value = driver.FindElement(txtSecWomenled).Text;
                return value;
            }
            else if (JobType.Equals("Sellside"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtWomenLedSell, 10);
                string value = driver.FindElement(txtWomenLedSell).Text;
                return value;
            }
            else if (JobType.Equals("ESOP Corporate Finance") || JobType.Contains("General Financial Advisory") || JobType.Contains("Real Estate Brokerage") || JobType.Contains("Special Committee Advisory") || JobType.Contains("Strategic Alternatives Study") || JobType.Contains("Take Over Defense") || JobType.Contains("Strategy") || JobType.Contains("Post Merger Integration") || JobType.Contains("Valuation Advisory"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSecWomenLedESOP, 10);
                string value = driver.FindElement(txtSecWomenLedESOP).Text;
                return value;
            }
            else if (JobType.Equals("Activism Advisory"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSecWomenLedActivism, 10);
                string value = driver.FindElement(txtSecWomenLedActivism).Text;
                return value;
            }
            else if (JobType.Equals("FA - Portfolio-Advis/Consulting") || JobType.Equals("FA - Portfolio-Auto Loans") || JobType.Equals("FA - Portfolio-Auto Struct Prd") || JobType.Equals("FA - Portfolio-Deriv/Risk Mgmt") || JobType.Equals("FA - Portfolio-Diligence/Assets") || JobType.Equals("FA - Portfolio-Funds Transfer") || JobType.Equals("FA - Portfolio-GP interest") || JobType.Equals("FA - Portfolio-Real Estate") || JobType.Equals("FA - Portfolio-Valuation") || JobType.Equals("FA - Portfolio-Auto Struct Prd/Consulting"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSecWomenLedFVA, 10);
                string value = driver.FindElement(txtSecWomenLedFVA).Text;
                return value;
            }
            else if (JobType.Equals("Creditor Advisors") || JobType.Equals("Debtor Advisors") || JobType.Equals("DM&A Buyside") || JobType.Equals("DM&A Sellside") || JobType.Equals("Equity Advisors") || JobType.Equals("PBAS") || JobType.Equals("Liability Mgmt") || JobType.Equals("Regulator/Other"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSecWomenLedFR, 10);
                string value = driver.FindElement(txtSecWomenLedFR).Text;
                return value;
            }
            else if (JobType.Contains("CVAS"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSecWomenLedCVAS, 10);
                string value = driver.FindElement(txtSecWomenLedCVAS).Text;
                return value;
            }
            else if (JobType.Contains("TAS"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSecWomenLedTAS, 20);
                string value = driver.FindElement(txtSecWomenLedTAS).Text;
                return value;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSecWomenLedOther, 120);
                string value = driver.FindElement(txtSecWomenLedOther).Text;
                return value;
            }
        }

        //Get field name of Women Led in Engagement details page
        public string ValidateWomenLedField(string JobType)
        {
            if (JobType.Contains("ESOP Corporate Finance") || JobType.Contains("General Financial Advisory") || JobType.Contains("Real Estate Brokerage") || JobType.Contains("Special Committee Advisory") || JobType.Contains("Strategic Alternatives Study") || JobType.Contains("Take Over Defense") || JobType.Contains("Strategy") || JobType.Contains("Post Merger Integration") || JobType.Contains("Valuation Advisory"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenLedJob, 20);
                string value = driver.FindElement(labelWomenLedJob).Text;
                return value;
            }
            else if (JobType.Equals("Activism Advisory"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenLedActivism, 20);
                string value = driver.FindElement(labelWomenLedActivism).Text;
                return value;
            }
            else if (JobType.Equals("FA - Portfolio-Advis/Consulting") || JobType.Equals("FA - Portfolio-Auto Loans") || JobType.Equals("FA - Portfolio-Auto Struct Prd") || JobType.Equals("FA - Portfolio-Deriv/Risk Mgmt") || JobType.Equals("FA - Portfolio-Diligence/Assets") || JobType.Equals("FA - Portfolio-Funds Transfer") || JobType.Equals("FA - Portfolio-GP interest") || JobType.Equals("FA - Portfolio-Real Estate") || JobType.Equals("FA - Portfolio-Valuation") || JobType.Equals("FA - Portfolio-Auto Struct Prd/Consulting") || JobType.Equals("TAS - ESG Due Diligence & Analytics"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenFVA, 25);
                string value = driver.FindElement(labelWomenFVA).Text;
                return value;
            }
            else if (JobType.Equals("Creditor Advisors") || JobType.Equals("Debtor Advisors") || JobType.Equals("DM&A Buyside") || JobType.Equals("DM&A Sellside") || JobType.Equals("Equity Advisors") || JobType.Equals("PBAS") || JobType.Equals("Liability Mgmt") || JobType.Equals("Regulator/Other"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenFR, 25);
                string value = driver.FindElement(labelWomenFR).Text;
                return value;
            }
            else if (JobType.Contains("CVAS"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenLedCVAS, 25);
                string value = driver.FindElement(labelWomenLedCVAS).Text;
                return value;
            }
            else if (JobType.Equals("Lender Education"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, textWomenLedLE, 25);
                string value = driver.FindElement(textWomenLedLE).Text;
                return value;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenLed, 25);
                string value = driver.FindElement(labelWomenLed).Text;
                return value;
            }
        }

        //Get type of added additional client record
        public string GetTypeOfAdditionalAddedClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedClient, 80);
            string value = driver.FindElement(valAddedClient).Text;
            return value;
        }

        //Get name of added additional client record
        public string GetNameOfAdditionalAddedClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedClientName, 80);
            string value = driver.FindElement(valAddedClientName).Text;
            return value;
        }
        //Get name of added additional client record
        public string GetNameOfAdditionalAddedClientEngL()
        {
            string value = driver.FindElement(By.XPath("//span[text()='Private Equity']/ancestor::tr/td//span[text()='Client']/ancestor::tr/th/lightning-primitive-cell-factory//records-hoverable-link/div/a/span/slot/span/slot")).Text;
            return value;
        }
        //Validate additional Subject added from Additional Client/Subject Pop up
        public string ValidateAdditionalSubjectFromPopUpL(string name)
        {
            if (name.Equals("A&D Mortgage LLC"))
            {
                Thread.Sleep(7000);
                //string value = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']")).Displayed.ToString();
                string type = driver.FindElement(By.XPath("//table/tbody/tr/th//slot[text()='A&D Mortgage LLC']/ancestor::tr/td[3]//lst-formatted-text/span")).Text;
                return type;
            }
            else
            {
                Thread.Sleep(6000);
                string type = driver.FindElement(By.XPath("//table/tbody/tr/th//slot[text()='" + name + "']/ancestor::tr/td[3]//lst-formatted-text/span")).Text;
                return type;
            }
        }

        //To Click Mass Edit Records button button
        public string ClickMassEditRecordsButtonLightning()
        {
            driver.FindElement(btnMassEditRecordsL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, titleMassEditPageL, 120);
            string name = driver.FindElement(titleMassEditPageL).Text;
            return name;
        }
        //Validate additional Subject added from Additional Client/Subject Pop up
        //public string ValidateUpdatedValuessFromMassEdit(string name)
        //{
        //    Thread.Sleep(5000);
        //    string value = driver.FindElement(By.XPath("//div/div[1]/div/lightning-formatted-text[text()='" + name + "']")).Displayed.ToString();
        //    if (value.Equals("True"))
        //    {
        //        string type = driver.FindElement(By.XPath("//div/div[1]/div/lightning-formatted-text[text()='" + name + "']/ancestor::tr/td[12]/div/lightning-formatted-text")).Text;
        //        return type;
        //    }
        //    else
        //    {
        //        return "Not required value";
        //    }
        //}


        // To validate save functionality of Additional client
        public string ValidateSaveFunctionalityOfAdditionalClientThruAdditionalClientButtonL(string name, string type, string recordType)
        {
            if (type.Equals("Creditor Advisors") && name.Equals("Accupac") || type.Equals("Debtor Advisors") && name.Equals("Accupac"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtClientSubjectL, 80);
                driver.FindElement(txtClientSubjectL).SendKeys(name);
                Thread.Sleep(4000);
                driver.FindElement(By.XPath("//ul/li[1]/lightning-base-combobox-item//span[2]//strong")).Click();
                driver.FindElement(btnSaveDetailsL).Click();
                Thread.Sleep(7000);
                driver.FindElement(lnkEngClientSubL).Click();
                Thread.Sleep(5000);
                driver.FindElement(tabClientSubject).Click();
                Thread.Sleep(5000);
                string value = driver.FindElement(By.XPath("//span[text()='Private Equity']/ancestor::tr/td//span[text()='Client']/ancestor::tr/th/lightning-primitive-cell-factory//records-hoverable-link/div/a/span/slot/span/slot")).Text;
                return value;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtClientSubjectL, 80);
                driver.FindElement(txtClientSubjectL).SendKeys(name);
                Thread.Sleep(4000);
                driver.FindElement(By.XPath("//ul/li[1]/lightning-base-combobox-item//span[2]//strong")).Click();
                driver.FindElement(btnSaveDetailsL).Click();
                Thread.Sleep(5000);
                //driver.FindElement(lnkShowMoreL).Click();
                //Thread.Sleep(5000);
                string value = driver.FindElement(By.XPath("//lightning-formatted-text[text()='" + recordType + "']/ancestor::records-record-layout-row/slot/records-record-layout-item[1]//dd//a//slot/span/slot")).Text;
                return value;
            }
        }
        //To click on Back To Engagement button
        public string ClickBackToEngButtonAndValidatePageL()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToEng, 150);
            driver.FindElement(btnBackToEng).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(7000);
            string name = driver.FindElement(lblEngagement).Text;
            //driver.SwitchTo().DefaultContent();
            return name;
        }
        public void SwitchDefaultFrame()
        {
            driver.SwitchTo().DefaultContent();
        }
        public string ValidateEngAdditionalSubjectFromPopUpL(string jobType, string name, string value)
        {//added accupac for 8330
            if (jobType.Equals("Creditor Advisors") && name.Equals("Accupac"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkShowMore, 200);
                driver.FindElement(lnkShowMore).Click();
                Thread.Sleep(8000);
                string value1 = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']")).Displayed.ToString();
                string type = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']/ancestor::th/following-sibling::td[1]")).Text;
                return type;
            }
            //else if (jobType.Equals("Debtor Advisors") && name.Equals("2Agriculture") || jobType.Equals("Debtor Advisors") && name.Equals("ABC"))
            //{
            //    WebDriverWaits.WaitUntilEleVisible(driver, lnkShowMore, 150);
            //    driver.FindElement(lnkShowMore).Click();
            //    Thread.Sleep(10000);
            //    string value1 = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']")).Displayed.ToString();
            //    string type = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']/ancestor::th/following-sibling::td[1]")).Text;
            //    return type;
            //}
            else
            {
                Thread.Sleep(8000);
                Console.WriteLine("Entered else");
                string type = driver.FindElement(By.XPath("//lightning-formatted-text[text()='" + value + "']")).Text;
                driver.FindElement(By.XPath("//lightning-formatted-text[text()='" + value + "']/ancestor::records-record-layout-section/div//dl//records-record-layout-row[2]//force-lookup//records-hoverable-link")).Click();
                Thread.Sleep(5000);
                driver.FindElement(tabClientSubject).Click();
                Thread.Sleep(5000);
                return type;
            }
        }
        //Validate the company name of Key creditor 
        public string GetCompanyNameOfAddedKeyCreditor()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedKeyCred, 80);
            string value = driver.FindElement(valAddedKeyCred).Text;
            return value;
        }
        //Get type of added Key creditor record
        public string GetTypeOfAdditionalAddedKeyCreditor()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedKeyCredType, 80);
            string value = driver.FindElement(valAddedKeyCredType).Text;
            return value;
        }

        //Validate additional Subject added from Additional Client/Subject Pop up
        public string ValidateAdditionalSubjectFromPopUp(string name)
        {
            if (name.Equals("A&D Mortgage LLC"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkShowMore, 150);
                driver.FindElement(lnkShowMore).Click();
                Thread.Sleep(7000);
                string value = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']")).Displayed.ToString();
                string type = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']/ancestor::th/following-sibling::td[1]")).Text;
                return type;
            }
            else
            {
                Thread.Sleep(6000);
                string value = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']")).Displayed.ToString();
                string type = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']/ancestor::th/following-sibling::td[1]")).Text;
                return type;
            }
        }
        public string ValidateEngAdditionalSubjectFromPopUp(string name, string jobType)
        {//added accupac for 8330
            if (jobType.Equals("Creditor Advisors") && name.Equals("Accupac") || jobType.Equals("Creditor Advisors") && name.Equals("A&D Mortgage LLC") || jobType.Equals("Creditor Advisors") && name.Equals("ABC") || jobType.Equals("Creditor Advisors") && name.Equals("2Agriculture") || jobType.Equals("Creditor Advisors") && name.Equals("Ad Exchange Group") || jobType.Equals("Creditor Advisors") && name.Equals("Bel Pastry Inc."))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkShowMore, 200);
                driver.FindElement(lnkShowMore).Click();
                Thread.Sleep(8000);
                string value = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']")).Displayed.ToString();
                string type = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']/ancestor::th/following-sibling::td[1]")).Text;
                return type;
            }
            else if (jobType.Equals("Creditor Advisors") && name.Equals("Bell & Howell"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkShowMore, 150);
                driver.FindElement(lnkShowMore).Click();
                Thread.Sleep(8000);
                driver.FindElement(lnkShowMore).Click();
                Thread.Sleep(8000);
                string value = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']")).Displayed.ToString();
                string type = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']/ancestor::th/following-sibling::td[1]")).Text;
                return type;
            }
            else if (jobType.Equals("Debtor Advisors") && name.Equals("Ad Exchange Group") || jobType.Equals("Debtor Advisors") && name.Equals("ABC") || jobType.Equals("Debtor Advisors") && name.Equals("2Agriculture") || jobType.Equals("Debtor Advisors") && name.Equals("ABC") || jobType.Equals("Debtor Advisors") && name.Equals("Bel Pastry Inc.") || jobType.Equals("Debtor Advisors") && name.Equals("Bell & Howell"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkShowMore, 150);
                driver.FindElement(lnkShowMore).Click();
                Thread.Sleep(10000);
                string value = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']")).Displayed.ToString();
                string type = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']/ancestor::th/following-sibling::td[1]")).Text;
                return type;
            }
            else
            {
                string value = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']")).Displayed.ToString();
                string type = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr/th/a[text()='" + name + "']/ancestor::th/following-sibling::td[1]")).Text;
                return type;
            }
        }
        //Validate the visibility of New Engagement Client/Subject button
        public string ValidateNewEngClientSubjectButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            string name = driver.FindElement(btnNewEngAdditionalClientSub).Displayed.ToString();
            if (name.Equals("True"))
            {
                return "New Engagement Client/Subject";
            }
            else
            {
                return "No such button";
            }
        }


        //Validate Mass Edit Records button
        public string ValidateMassEditRecordsButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            string name = driver.FindElement(btnMassEditRecords).Displayed.ToString();
            if (name.Equals("True"))
            {
                return "Mass Edit Records";
            }
            else
            {
                return "No such button";
            }
        }


        //To Click Mass Edit Records button 
        public string ClickMassEditRecordsButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnMassEditRecords).Click();
            Thread.Sleep(3000);
            driver.SwitchTo().Frame(0);
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, titleMassEditPage, 120);
            string name = driver.FindElement(titleMassEditPage).Text;
            return name;
        }

        //To Click Additional Client Subject button 
        public string ClickAdditionalClientSubjectButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnNewEngAdditionalClientSub).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, titleFREngSum, 120);
            string name = driver.FindElement(titleFREngSum).Text;
            return name;
        }

        //Validate additional Subject added from Additional Client/Subject Pop up
        public string ValidateUpdatedValuessFromMassEdit(string name)
        {
            Thread.Sleep(5000);
            string value = driver.FindElement(By.XPath("//div/div[1]/div/lightning-formatted-text[text()='" + name + "']")).Displayed.ToString();
            if (value.Equals("True"))
            {
                string type = driver.FindElement(By.XPath("//div/div[1]/div/lightning-formatted-text[text()='" + name + "']/ancestor::tr/td[12]/div/lightning-formatted-text")).Text;
                return type;
            }
            else
            {
                return "Not required value";
            }
        }
        //To click on Back To Engagement button
        public string ClickBackToEngButtonAndValidatePage()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToEng, 150);
            driver.FindElement(btnBackToEng).Click();
            driver.SwitchTo().DefaultContent();
            string name = driver.FindElement(titleEngDetails).Text;
            return name;
        }

        //Validate additional Subject added from Additional Client/Subject Pop up
        public string ValidateAdditionalSubjectFromPopUp(string jobType, string name)
        {
            if (jobType.Equals("Creditor Advisors"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkShowMore, 150);
                driver.FindElement(lnkShowMore).Click();
                Thread.Sleep(7000);
                string value = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr[7]/th/a[text()='" + name + "']")).Displayed.ToString();
                string type = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr[7]/th/a[text()='" + name + "']/ancestor::th/following-sibling::td[1]")).Text;
                return type;
            }
            else
            {
                Thread.Sleep(4000);
                string value = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr[7]/th/a[text()='" + name + "']")).Displayed.ToString();
                string type = driver.FindElement(By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr[7]/th/a[text()='" + name + "']/ancestor::th/following-sibling::td[1]")).Text;
                return type;
            }
        }

        //Validate Additional Clients/Subjects button
        public string ValidateAdditionalClientSubjectButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdditionalClientSub, 120);
            string name = driver.FindElement(btnAdditionalClientSub).Text;
            return name;
        }

        //Validate Delete Records button
        public string ValidateDeleteRecordsButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteRecords, 120);
            string name = driver.FindElement(btnDeleteRecords).Text;
            return name;
        }

        //Validate Edit button
        public string ValidateEditButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditMassEdit, 120);
            string name = driver.FindElement(btnEditMassEdit).Text;
            return name;
        }

        //Validate Refresh button
        public string ValidateRefreshButton()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtRefresh, 120);
            string name = driver.FindElement(txtRefresh).Text;
            return name;
        }

        //Validate all displayed Type dropdown values
        
        public bool VerifyTypes()
        {
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//button[contains(@id,'button-17')]")).Click();
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(comboTypeMassEdit);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "All", "Client", "Contra", "Counterparty", "Equity Holder", "Key Creditor", "Lender", "Other", "PE Firm", "Subject" };
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

        public string Get1stColumn()
        {
            Thread.Sleep(7000);
            string column1 = driver.FindElement(By.XPath("//table/thead/tr/td[1]/div")).Text;
            return column1;
        }

        public bool ValidateTableColumns()
        {
            Thread.Sleep(3000);
            IReadOnlyCollection<IWebElement> valColumns = driver.FindElements(colTableColumns);
            var actualValue = valColumns.Select(x => x.Text).ToArray();
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[1]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[2]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[3]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[4]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[5]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[6]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[7]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[8]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[9]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[10]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[11]/div")).Text + "col1");
            string[] expectedValue = { "Client/Subject  ", "Primary  ", "Type  ", "Role", "Client Holdings (MM) - USD   ", "Client Holdings %  ", "Debt Holdings (MM) - USD   ", "Debt Holdings % Total Debt  ", "Key Creditor Importance  ", "Key Creditor Weighting %  ", "Revenue Allocation %  " };
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

        //Click Delete button without selecting records and validate error message
        public string ClickDeleteAndValidateErrorMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteRecords);
            driver.FindElement(btnDeleteRecords).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtAlertMessage, 100);
            string message = driver.FindElement(txtAlertMessage).Text;
            return message;
        }

        //Close the error message and validate if it is still displayed
        public string ClickCloseAndValidateErrorMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCloseError);
            driver.FindElement(btnCloseError).Click();
            try
            {
                string message = driver.FindElement(txtAlertMessage).Displayed.ToString();
                return message;
            }
            catch (Exception)
            {
                return "No validate message is displayed";
            }
        }

        public bool ValidateTableColumnsForEachType(string name)
        {
            if (name.Equals("Client"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, colTableColumns, 100);
                IReadOnlyCollection<IWebElement> valColumns = driver.FindElements(colTableColumns);
                var actualValue = valColumns.Select(x => x.Text).ToArray();
                string[] expectedValue = { "Client/Subject  ", "Primary  ", "Type  ", "Role", "Client Holdings (MM) - USD   ", "Client Holdings %  ", "Debt Holdings (MM) - USD   ", "Debt Holdings % Total Debt  ", "Key Creditor Importance  ", "Key Creditor Weighting %  ", "Revenue Allocation %  " };
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
            else if (name.Equals("Key Creditor"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, colTableColumns, 100);
                IReadOnlyCollection<IWebElement> valColumns = driver.FindElements(colTableColumns);
                var actualValue = valColumns.Select(x => x.Text).ToArray();
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[1]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[2]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[3]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[4]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[5]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[6]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[7]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[8]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[9]/div")).Text + "col1");
                string[] expectedValue = { "Client/Subject  ", "Type  ", "Role", "Debt Holdings (MM) - USD   ", "Debt Holdings % Total Debt  ", "Key Creditor Importance  ", "Key Creditor Weighting %  ", "Revenue Allocation %  ", "Notes" };
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
            else if (name.Equals("Subject"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, colTableColumns, 100);
                IReadOnlyCollection<IWebElement> valColumns = driver.FindElements(colTableColumns);
                var actualValue = valColumns.Select(x => x.Text).ToArray();
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[1]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[2]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[3]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[4]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[5]/div")).Text + "col1");

                string[] expectedValue = { "Client/Subject  ", "Primary  ", "Type  ", "Role", "Revenue Allocation %  " };
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
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, colTableColumns, 100);
                IReadOnlyCollection<IWebElement> valColumns = driver.FindElements(colTableColumns);
                var actualValue = valColumns.Select(x => x.Text).ToArray();
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[1]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[2]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[3]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[4]/div")).Text + "col1");
                string[] expectedValue = { "Client/Subject  ", "Type  ", "Role", "Revenue Allocation %  " };
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
        }

        //Select value from Type dropdown and validate the displayed values
        //Select value from Type dropdown and validate the displayed values
        public string SelectValueFromTypeDropdown(string name)
        {
            Thread.Sleep(7000);
            driver.FindElement(valType).Click();
            Thread.Sleep(3000);
            if (name.Equals("PE Firm") || name.Equals("Subject"))
            {
                var element = driver.FindElement(By.XPath("//div[@id='dropdown-element-17']/lightning-base-combobox-item/span[2]/span[text()='" + name + "']"));
                Actions action = new Actions(driver);
                action.MoveToElement(element);
                action.Perform();
                Thread.Sleep(3000);
                driver.FindElement(By.XPath("//div[@id='dropdown-element-17']/lightning-base-combobox-item/span[2]/span[text()='" + name + "']")).Click();
                Thread.Sleep(6000);
                //string value = driver.FindElement(valSelectedType).Text;
                string value = driver.FindElement(valSelectedType).GetAttribute("data-value");
                return value;
            }
            else
            {
                Thread.Sleep(4000);
                driver.FindElement(By.XPath("//div[@id='dropdown-element-17']/lightning-base-combobox-item/span[2]/span[text()='" + name + "']")).Click();
                Thread.Sleep(6000);
                //string value = driver.FindElement(valSelectedType).Text;
                string value = driver.FindElement(valSelectedType).GetAttribute("data-value");
                return value;
            }
        }

        //Get Currency
        public string GetCurrencyL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabFees);
            driver.FindElement(tabFees).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valCurrencyL, 150);
            string currency = driver.FindElement(valCurrencyL).Text;
            return currency;
        }

        //Get Total Debt Currency
        public string GetTotalDebtMM()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTotalDebtMM);
            string value = driver.FindElement(valTotalDebtMM).Text;
            return value;
        }
        //Get Total Estimated Fee
        public string GetTotalEstimatedFeeL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTotalEstFeeL, 150);
            string fee = driver.FindElement(valTotalEstFeeL).Text;
            return fee.Substring(4, 9);
        }

        //Get Total Estimated Fee
        public string GetTotalEstimatedFeeOf2ndEngL()
        {
            Thread.Sleep(4000);
            driver.FindElement(tabFees2ndEngL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valTotalEstFee2ndEngL, 150);
            string fee = driver.FindElement(valTotalEstFee2ndEngL).Text;
            driver.FindElement(tabInfo2ndEngL).Click();
            Thread.Sleep(4000);
            return fee.Substring(4, 10);
        }

        //Get Currency
        public string GetLegalEntityL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, subTabAdmin);
            driver.FindElement(subTabAdmin).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valLegalEntityL, 150);
            string currency = driver.FindElement(valLegalEntityL).Text;
            return currency;
        }

        //Get Contract Number
        public string GetContractNumberL()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, tabEngProject);
            //driver.FindElement(tabEngProject).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabRevenue);
            driver.FindElement(tabRevenue).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, subtabContracts);
            driver.FindElement(subtabContracts).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valContractNumberL, 150);
            string number = driver.FindElement(valContractNumberL).Text;
            return number;
        }
        public string ValidateIsMainOfAddedContractL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, subTabEng, 170);
            driver.FindElement(subTabEng).Click();
            Thread.Sleep(7000);
            driver.FindElement(tabRevenue).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, subtabContracts);
            driver.FindElement(subtabContracts).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContractL, 210);
            driver.FindElement(lnkContractL).Click();
            Thread.Sleep(5000);
            string isMain = driver.FindElement(valIsMainCheckboxL).Text;
            driver.Navigate().Refresh();
            Thread.Sleep(5000);
            return isMain;
        }
        //Click on Lightning Counterparties button, click on details and click on Eng CounterpartyContact
        public void ClickViewCounterpartiesButton()
        {
            Thread.Sleep(18000);
            Console.WriteLine("Entered function");
            //WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButonL, 160);
            //driver.FindElement(iconExpandMoreButonL).Click();
            //Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewCounterparties, 150);
            driver.FindElement(btnViewCounterparties).Click();
        }
        public void ClickViewCounterpartiesButtonL()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewCounterpartiesL, 150);
            driver.FindElement(btnViewCounterpartiesL).Click();
        }




        //Click Engagement Counterparty Button
        //Click Engagement Counterparty Button
        public string ClickEngCounterpartyButton()
        {
            Thread.Sleep(4000);
            Console.WriteLine("Looking for added counterpatry link");
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkAddedCounterparty, 150);
            driver.FindElement(lnkAddedCounterparty).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEngCounterpartyContact, 150);
            driver.FindElement(btnEngCounterpartyContact).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblEngCounterpartyContactSearch, 240);
            string value = driver.FindElement(lblEngCounterpartyContactSearch).Text;
            return value;
        }

        //Validate View Counterparties button
        public string ValidateViewCounterpartiesButton(string jobType)
        {
            if (jobType.Equals("Buyside"))
            {
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnViewCounterparties, 350);
                string value = driver.FindElement(btnViewCounterparties).Text;
                return value;
            }
            else
            {
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnViewCounterparties, 350);
                string value = driver.FindElement(btnViewCounterparties).Text;
                WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 160);
                driver.FindElement(btnClose).Click();
                return value;
            }
        }
        public string ValidateVisibilityOfViewCounterpartiesButton()
        {
            Thread.Sleep(4000);
            try
            {
                string value = driver.FindElement(btnViewCounterparties).Displayed.ToString();
                Console.WriteLine(value);
                return "View Counterparties button is displayed";
            }
            catch (Exception e)
            {
                return "View Counterparties button is not displayed";
            }
        }


        public void ClickDetailsLinkL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDetailsL, 190);
            driver.FindElement(lnkDetailsL).Click();
        }

        public void Click2ndDetailsLinkL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnk2ndDetailsL, 190);
            driver.FindElement(lnk2ndDetailsL).Click();
        }

        //Get Counterparty Contact's First Name
        public string GetCounterpartyContact1stName()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valFirstNameL, 190);
            string name = driver.FindElement(valFirstNameL).Text;
            return name;
        }

        //Get Counterparty Contact's 2nd Name
        public string GetCounterpartyContact2ndName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLastNameL, 190);
            string name = driver.FindElement(valLastNameL).Text;
            return name;
        }

        //Get Counterparty Contact's comment
        public string GetCounterpartyContactCommentL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCommentTypeL, 190);
            string name = driver.FindElement(valCommentTypeL).Text;
            return name;
        }

        //Get Counterparty Contact Comment's creator
        public string GetCounterpartyContactCreatorL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCreatorL, 190);
            string name = driver.FindElement(valCreatorL).Text;
            return name;
        }
        //Click Internal team tab
        public string ClickInternalTeamTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeam, 190);
            driver.FindElement(tabInternalTeam).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtMember, 190);
            string name = driver.FindElement(txtMember).Text;
            return name;
        }

        By valERPBusinessUnitId = By.CssSelector("div[id*='M0ee2j']"); public string GetERPBusinessUnitId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusinessUnitId, 60);
            string valueERPBusinessUnitId = driver.FindElement(valERPBusinessUnitId).Text;
            return valueERPBusinessUnitId;
        }
        By valERPId = By.CssSelector("div[id*='M0ee6j']"); public string GetERPId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPId, 60);
            string valueERPId = driver.FindElement(valERPId).Text;
            return valueERPId;
        }
        public string ClickICOContractLink(int row)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContract, 180);
            string contractName = driver.FindElement(lnkContract).Text;
            if (contractName.Contains("ICO"))
            {
                driver.FindElement(lnkContract).SendKeys(Keys.Control + Keys.Return);
                if (row.Equals(2))
                {
                    CustomFunctions.SwitchToWindow(driver, 1);
                    driver.Navigate().Refresh();
                }
                if (row.Equals(3))
                {
                    CustomFunctions.SwitchToWindow(driver, 3);
                    driver.Navigate().Refresh();
                }
                else if (row.Equals(4))
                {
                    CustomFunctions.SwitchToWindow(driver, 5);
                    driver.Navigate().Refresh();
                }
                return "ICO contract created";
            }
            else
            {
                return "NO ICO contract created";
            }
        }

        public string getLegalEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLegalEntity, 90);
            string legalEntity = driver.FindElement(valLegalEntity).Text;
            return legalEntity;
        }
        public void ChangeLegalEntity(string legalEntity)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnEdit).Click();
            if (legalEntity.Equals("HL Capital, Inc."))
            {
                driver.FindElement(txtERPLegalEntity).Clear();
                driver.FindElement(txtERPLegalEntity).SendKeys("HL Consulting, Inc.");
            }
            else if (legalEntity.Equals("HL Consulting, Inc."))
            {
                driver.FindElement(txtERPLegalEntity).Clear();
                driver.FindElement(txtERPLegalEntity).SendKeys("HL (China) Ltd");
            }
            else if (legalEntity.Equals("HL (China) Ltd"))
            {
                driver.FindElement(txtERPLegalEntity).Clear();
                driver.FindElement(txtERPLegalEntity).SendKeys("HL (Australia) Pty Ltd");
            }
            driver.FindElement(btnSave).Click();
        }

        public string GetEnggNumberSuffix()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEnggNumberSuffix, 90);
            string valueEnggNumberSuffix = driver.FindElement(valEnggNumberSuffix).Text;
            return valueEnggNumberSuffix;
        }
        public string GetIsMainContractCheckBoxStatus(int tr)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"div[id*='M0ecq'] > table > tbody > tr:nth-child({tr}) > td.dataCell.booleanColumn > img"));
            string IsMainContractChkBox = driver.FindElement(By.CssSelector($"div[id*='M0ecq'] > table > tbody > tr:nth-child({tr}) > td.dataCell.booleanColumn > img")).GetAttribute("title");
            return IsMainContractChkBox;
        }
        public string GetEndDate(int tr)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"div[id*='M0ecq'] > table > tbody > tr:nth-child({tr}) > td:nth-child(10)"));
            string endDate = driver.FindElement(By.CssSelector($"div[id*='M0ecq'] > table > tbody > tr:nth-child({tr}) > td:nth-child(10)")).Text;
            Console.WriteLine("endDate::::" + endDate);
            return endDate;
        }

        public string VerifyIfCoExistFieldIsEditableOrNot()
        {
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
            string enb = driver.FindElement(imputCoExist).GetAttribute("Type");
            if (enb == "hidden")
            {
                driver.FindElement(btnCancel).Click();
                Thread.Sleep(2000);
                return "CoExist field is not editable ";
            }
            else
            {
                driver.FindElement(btnCancel).Click();
                Thread.Sleep(2000);
                return "CoExist field is editable ";
            }
        }
        public string ValidateIfCoExistFieldIsPresentAndCheckedOrNot()
        {
            if (driver.FindElement(checkBoxCoExist).Displayed)
            {
                string value = driver.FindElement(checkBoxCoExist).GetAttribute("alt");
                if (value == "Not Checked")
                {
                    return "CoExist checkbox is displayed and not-checked";
                }
                else
                {
                    return "CoExist checkbox is displayed and checked";
                }
            }
            else
            {
                return "CoExist checkbox is not displayed.";
            }
        }

        public void DeleteEngagementSector()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkSectorName, 120);
            driver.FindElement(linkSectorName).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteEngagementSector, 120);
            driver.FindElement(btnDeleteEngagementSector).Click();
            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(3000);
        }
        public bool VerifyEngagementSectorAddedToEngagementOrNot(string sectorName)
        {
            Thread.Sleep(5000);
            bool result = false;
            if (driver.FindElement(linkSectorName).Text == sectorName)
            {
                result = true;
            }
            return result;
        }
        public void NavigateToEngagementDetailPageFromEngagementSectorDetailPage()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkEngagementName, 120);
            driver.FindElement(linkEngagementName).Click();
            Thread.Sleep(2000);
        }
        public void SaveNewEngagementSectorDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveEngagementSector, 120);
            driver.FindElement(btnSaveEngagementSector).Click();
            Thread.Sleep(2000);
        }
        public string GetEngagementSectorName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEngagementSectorName, 120);
            string name = driver.FindElement(valEngagementSectorName).Text;
            return name;
        }
        public void SelectCoverageSectorDependency(string covSectorDependencyName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, imgCoverageSectorDependencyLookUp, 120);
            driver.FindElement(imgCoverageSectorDependencyLookUp).Click();
            Thread.Sleep(2000);             // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);             //Enter search frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("searchFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='searchFrame']")));
            Thread.Sleep(2000);             //Enter dependency name
            driver.FindElement(txtSearchBox).SendKeys(covSectorDependencyName);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(2000); driver.SwitchTo().DefaultContent();             //Enter results frame & click on the result
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("resultsFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='resultsFrame']")));
            Thread.Sleep(2000);
            driver.FindElement(linkCoverageSectorDependencyName).Click();
            Thread.Sleep(4000);             //Switch back to original window
            CustomFunctions.SwitchToWindow(driver, 0);
        }
        public void ClickNewEngagementSectorButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewEngagementSector, 120);
            driver.FindElement(btnNewEngagementSector).Click();
            Thread.Sleep(2000);
        }
        public bool VerifyIfEngagementSectorQuickLinkIsDisplayed()
        {
            bool result = false;
            if (driver.FindElement(linkEngagementSector).Displayed)
            {
                result = true;
            }
            return result;
        }


        public void ClickFirstContractLink(int row)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContract, 180);
            driver.FindElement(lnkSecondContract).SendKeys(Keys.Control + Keys.Return); CustomFunctions.SwitchToWindow(driver, 4);
            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
            driver.Navigate().Refresh();
        }
        public void ClickClientLinkForContract(int row)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkClient, 180);
            driver.FindElement(lnkClient).SendKeys(Keys.Control + Keys.Return);
            if (row.Equals(2))
            {
                CustomFunctions.SwitchToWindow(driver, 2);
                driver.Navigate().Refresh();
            }
            else
            {
                CustomFunctions.SwitchToWindow(driver, 5);
                driver.Navigate().Refresh();
            }
        }
        public void ClickSecondContractLink(int row)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkSecondContract, 180);
            driver.FindElement(lnkSecondContract).SendKeys(Keys.Control + Keys.Return);
            if (row.Equals(2))
            {
                CustomFunctions.SwitchToWindow(driver, 1);
                driver.Navigate().Refresh();
                driver.Navigate().Refresh();
                driver.Navigate().Refresh();
                driver.Navigate().Refresh();
            }
            else
            {
                CustomFunctions.SwitchToWindow(driver, 4);
                driver.Navigate().Refresh();
                driver.Navigate().Refresh();
                driver.Navigate().Refresh();
            }
        }

        //Get Client value
        public string GetClientCompanyL()
        {
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(Driver, valClientL, 100);
            string value = driver.FindElement(valClientL).Text;
            return value;

        }

        //Get Subject value
        public string GetSubjectCompanyL()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(Driver, valSubjectL, 100);
            string value = driver.FindElement(valSubjectL).Text;
            return value;

        }

        //Get Info value
        //Get Info value
        public string ValidateInfoTab()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(Driver, tabInformationL, 100);
            string value = driver.FindElement(tabInformationL).Text;
            return value;

        }

        //Get Fees & Financials value
        public string ValidateFeesTab()
        {
            //Thread.Sleep(3000);
            //WebDriverWaits.WaitUntilEleVisible(Driver, tabEng, 100);
            //driver.FindElement(tabEng).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(Driver, tabFees, 100);
            string value = driver.FindElement(tabFees).Text;
            driver.FindElement(tabFees).Click();
            return value;

        }

        //Get Client/Subject & Referral value
        public string ValidateClientSubjectAndReferralTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(Driver, tabClientSub, 100);
            string value = driver.FindElement(tabClientSub).Text;
            driver.FindElement(tabClientSub).Click();
            return value;

        }
        //Get Details Sub tab
        public string ValidateDetailsSubTab()
        {

            WebDriverWaits.WaitUntilEleVisible(Driver, subTabDetails, 100);
            string value = driver.FindElement(subTabDetails).Text;
            return value;
        }
        //Get Important Dates Sub tab
        public string ValidateImportantDatesSubTab()
        {

            WebDriverWaits.WaitUntilEleVisible(Driver, subTabImpDates, 100);
            string value = driver.FindElement(subTabImpDates).Text;
            return value;
        }

        //Get Administration Sub tab
        public string ValidateAdministrationSubTab()
        {

            WebDriverWaits.WaitUntilEleVisible(Driver, subTabAdmin, 100);
            string value = driver.FindElement(subTabAdmin).Text;
            return value;
        }
        //Get Closing Info Sub tab
        public string ValidateClosingInfoSubTab()
        {

            WebDriverWaits.WaitUntilEleVisible(Driver, subTabClosingInfo, 100);
            string value = driver.FindElement(subTabClosingInfo).Text;
            return value;
        }

        //Get CST Questionnaire Sub tab
        public string ValidateCSTQuestionnaireDetailsSubTab()
        {

            WebDriverWaits.WaitUntilEleVisible(Driver, subTabCST, 100);
            string value = driver.FindElement(subTabCST).Text;
            return value;
        }

        //Get Billing CommentsSub tab
        public string ValidateBillingCommentsSubTab()
        {

            WebDriverWaits.WaitUntilEleVisible(Driver, subTabBilling, 100);
            string value = driver.FindElement(subTabBilling).Text;
            return value;
        }

        //Get Revenue tab
        public string ValidateRevenueTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(Driver, tabRevenue, 100);
            string value = driver.FindElement(tabRevenue).Text;
            driver.FindElement(tabRevenue).Click();
            return value;

        }

        //Get Compliance & Legal tab
        //Get Compliance & Legal tab
        public string ValidateComplianceAndLegalTab()
        {
            //Thread.Sleep(4000);
            //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            //js.ExecuteScript("window.scrollTo(0,-850)");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(Driver, tabEngRevProj, 100);
            driver.FindElement(tabEngRevProj).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(Driver, tabCompliance, 100);
            string value = driver.FindElement(tabCompliance).Text;
            driver.FindElement(tabCompliance).Click();
            return value;

        }

        //Get Compliance Sub tab
        public string ValidateComplianceSubTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(Driver, subTabCompliance, 100);
            string value = driver.FindElement(subTabCompliance).Text;
            driver.FindElement(subTabCompliance).Click();
            return value;

        }

        //Get Legal Matters Sub tab
        public string ValidateLegalMattersSubTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(Driver, subTabLegal, 100);
            string value = driver.FindElement(subTabLegal).Text;
            driver.FindElement(subTabLegal).Click();
            return value;

        }

        //Get Conflict Check Sub tab
        public string ValidateConflictCheckSubTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(Driver, subTabCC, 100);
            string value = driver.FindElement(subTabCC).Text;
            driver.FindElement(subTabCC).Click();
            return value;

        }
        //Validate if Details tab is editable after clicking pencil icon
        public string ValidateDetailsTabIsEditable()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditEngName, 150);
            driver.FindElement(lnkEditEngName).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            string value = driver.FindElement(btnSaveDetailsL).Displayed.ToString();
            return value;
        }

        //Validate if Fees and Financials tab is editable after clicking pencil icon
        public string ValidateFeesTabIsEditable()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditCurrency, 150);
            driver.FindElement(lnkEditCurrency).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            string value = driver.FindElement(btnSaveDetailsL).Displayed.ToString();
            return value;
        }

        //Validate if Client/Subject & Referral tab is editable after clicking pencil icon
        public string ValidateClientSubjectAndReferralTabIsEditable()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRefType, 150);
            driver.FindElement(lnkRefType).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            string value = driver.FindElement(btnSaveDetailsL).Displayed.ToString();
            return value;
        }

        //Validate Add Revenue Accural functionality
        public string ValidateAddRevenueFunctionality()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddRevenue, 150);
            driver.FindElement(btnAddRevenue).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveRevenue, 150);
            driver.FindElement(btnSaveRevenue).Click();
            Thread.Sleep(8000);
            string id = driver.FindElement(valRevAccID).Text;
            return id;
        }

        //Validate Edit Revenue Accural functionality
        public string ValidateEditRevenueFunctionality()
        {
            Thread.Sleep(6000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,850)");
            driver.FindElement(lnkRevYearL).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditRevenue, 170);
            driver.FindElement(btnEditRevenue).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtPeriodAccural, 250);
            driver.FindElement(txtPeriodAccural).SendKeys("10");
            Thread.Sleep(4000);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(7000);
            string id = driver.FindElement(valPeriodAccural).Text;
            return id.Substring(4, 5);
        }

        //Click Revenue Projection Tab
        public string ValidateAndClickRevenueProjectionTab()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngName, 150);
            driver.FindElement(lnkEngName).Click();
            //WebDriverWaits.WaitUntilEleVisible(driver, tabRevenue, 150);
            //driver.FindElement(tabRevenue).Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,-850)");
            WebDriverWaits.WaitUntilEleVisible(driver, tabRevProj, 250);
            driver.FindElement(tabRevProj).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, titleRevProj, 250);
            string title = driver.FindElement(titleRevProj).Text;
            return title;
        }

        //Validate Edit Revenue Projection
        public string ValidateEditRevenueProjFunctionality()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditRevProj, 150);
            driver.FindElement(btnEditRevProj).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtProjMonFee, 250);
            driver.FindElement(txtProjMonFee).SendKeys("10");
            driver.FindElement(btnSaveRevproj).Click();
            Thread.Sleep(5000);
            string id = driver.FindElement(valRevProj).Text;
            return id;
        }
        //Validate displayed reports for deal team member
        public bool VerifyReportNamesForDealTeamMemberLightning()
        {
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='accessibility title']")));
            IReadOnlyCollection<IWebElement> valReportNames = driver.FindElements(tblReports);
            var actualValue = valReportNames.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Capital Markets Contact Log", "Counterparty History Report", "Counterparty List and Contact Log", "Counterparty List Report", "Engagement Working Group List", "PIF", "Potential Counterparty List - Client Copy", "Potential Counterparty List - Client Status", "Potential Counterparty List - Long", "Potential Counterparty List - Medium", "Potential Counterparty List Summary - Multi-Page", "Potential Counterparty List Summary - Single Page", "Potential Counterparty List- Short", "Racetrack Report" };
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
            driver.FindElement(btnReturnToEngLightning).Click();
            driver.SwitchTo().DefaultContent();
            return isSame;
        }


        //Validate Clear functionality of Revenue Projection
        public string ValidateClearRevenueProjFunctionality()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditRevProj, 150);
            driver.FindElement(btnEditRevProj).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkClearRevProj, 250);
            driver.FindElement(lnkClearRevProj).Click();
            driver.FindElement(btnSaveRevproj).Click();
            Thread.Sleep(5000);
            string message = driver.FindElement(msgNoRec).Text;
            return message;
        }


        //Validate Submit functionality of Revenue Projection
        public string ValidateSubmitRevenueProjFunctionality()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditRevProj, 150);
            driver.FindElement(btnEditRevProj).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnStartingYear, 150);
            driver.FindElement(btnStartingYear).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, valStartingYear, 250);
            driver.FindElement(valStartingYear).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnStartingMonth, 150);
            driver.FindElement(btnStartingMonth).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, valStartingMonth, 250);
            driver.FindElement(valStartingMonth).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnSaveCSTQuestionnaire).Click();
            Thread.Sleep(5000);
            string month = driver.FindElement(valStartingMonthDisplayed).Text;
            return month;
        }

        //Validate Return To Engagement functionality 
        public string ValidateReturnToEngFunctionality()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToEng, 150);
            driver.FindElement(btnReturnToEng).Click();
            Thread.Sleep(5000);
            string label = driver.FindElement(tabRevProj).Text;
            return label;
        }

        //Get default value of Client Ownership
        public string GetClientOwnershipL()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valClientOwnershipBefore, 150);
            string value = driver.FindElement(valClientOwnershipBefore).Text;
            return value;
        }

        //Get default value of Client Ownership
        public string GetClientOwnershipLPostUpdate()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, valClientOwnershipAfter, 150);
            string value = driver.FindElement(valClientOwnershipAfter).Text;
            return value;
        }
        //Click Imp Dates tab
        public void ClickImpDates()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(Driver, subTabImpDates, 150);
            driver.FindElement(subTabImpDates).Click();
        }

        //Click Imp Dates tab
        public void ClickImpDatesLV()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(Driver, subTabImpDates, 10);
            driver.FindElement(subTabImpDates).Click();
        }
        By lblTailExpires= By.XPath("//span[text()='Tail Expires']");
        public bool IsTailExpiresFieldPresentLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(Driver, lblTailExpires, 10);
                return driver.FindElement(lblTailExpires).Displayed;
            }
            catch{ return false; }

        }

        //Click Admin tab
        public void ClickAdmin()
        {
            WebDriverWaits.WaitUntilEleVisible(Driver, subTabAdmin, 150);
            driver.FindElement(subTabAdmin).Click();
        }

        //Click Closing Info tab
        public void ClickClosingInfo()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,-350)");
            WebDriverWaits.WaitUntilEleVisible(Driver, subTabClosingInfo, 150);
            driver.FindElement(subTabClosingInfo).Click();
        }

        //Click CST Questionnaire Details tab
        public void ClickCSTQuesTab()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,-450)");
            WebDriverWaits.WaitUntilEleVisible(Driver, subTabCST, 150);
            driver.FindElement(subTabCST).Click();
        }

        //Click Billing Comments tab
        public void ClickBillingCommentsTab()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,-450)");
            WebDriverWaits.WaitUntilEleVisible(Driver, subTabBilling, 150);
            driver.FindElement(subTabBilling).Click();
        }


        //Validate if Important Dates tab is editable after clicking pencil icon
        public string ValidateImpDatesTabIsEditable()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditDateEngL, 150);
            driver.FindElement(lnkEditDateEngL).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            string value = driver.FindElement(btnSaveDetailsL).Displayed.ToString();
            return value;
        }

        //Validate if Administration tab is editable after clicking pencil icon
        public string ValidateAdministrationTabIsEditable()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditAccStatus, 150);
            driver.FindElement(lnkEditAccStatus).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            string value = driver.FindElement(btnSaveDetailsL).Displayed.ToString();
            return value;
        }

        //Validate if section Document Checklist is present
        public string ValidateSectionDocChecklist()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secDocChecklist, 150);
            string value = driver.FindElement(secDocChecklist).Text;
            return value;
        }

        //Validate if Closing tab is editable after clicking pencil icon
        public string ValidateClosingInfoTabIsEditable()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditIntDeal, 150);
            driver.FindElement(lnkEditIntDeal).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            string value = driver.FindElement(btnSaveDetailsL).Displayed.ToString();
            return value;
        }

        //Validate if CST is editable after clicking pencil icon
        public string ValidateCSTTabIsEditable()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditCST, 150);
            driver.FindElement(lnkEditCST).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            string value = driver.FindElement(btnSaveDetailsL).Displayed.ToString();
            return value;
        }

        //Validate the mandatory message for Comment Date
        public string ValidateCommentDateMandatoryValidation()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewBilling, 150);
            driver.FindElement(btnNewBilling).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnCloseBilling).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgDate, 150);
            string value = driver.FindElement(msgDate).Text;
            return value;
        }

        //Validate the mandatory message for Comment Status
        public string ValidateCommentStatusMandatoryValidation()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgStatus, 150);
            string value = driver.FindElement(msgStatus).Text;
            return value;
        }

        //Validate the mandatory message for Comment
        public string ValidateBillingCommentMandatoryValidation()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgComment, 150);
            string value = driver.FindElement(msgComment).Text;
            return value;
        }

        //Update the value of Client Ownership
        public void UpdateClientOwnershipL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnClientOwnership, 150);
            driver.FindElement(btnClientOwnership).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//label[text()='Client Ownership']/ancestor::lightning-combobox/div//lightning-base-combobox-item[14]/span[2]/span")).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(3000);
        }

        //Update the value of DealCloud ID
        public string UpdateDealCloudIDAndValidate(string deal)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtDealCloudID, 150);
            driver.FindElement(txtDealCloudID).SendKeys(deal);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(6000);
            string value = driver.FindElement(valDealCloudIDPostUpdate).Text;
            return value;
        }

        //Update the value of Expected Market date
        public void UpdateExpectedMktDateL()
        {

            driver.FindElement(txtEstMktDate).SendKeys("3/14/2023");
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(3000);
        }

        //Get default value of Expected In Market Date
        public string GetExpMktDatePostUpdate()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valEstMktDate, 150);
            string value = driver.FindElement(valEstMktDate).Text;
            return value;
        }

        //Update the value of Internal deal announcement
        public string UpdateIntDealAndValidate()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,500)");
            WebDriverWaits.WaitUntilEleVisible(driver, btnIntDeal, 150);
            driver.FindElement(btnIntDeal).Click();
            Thread.Sleep(3000);
            driver.FindElement(valIntDeal).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, valIntDealPostUpdate, 190);
            string value = driver.FindElement(valIntDealPostUpdate).Text;
            return value;
        }

        //Update the value of CST Questionnaire
        public string UpdateCSTQuestionnaireAndValidate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCST, 150);
            driver.FindElement(btnCST).Click();
            Thread.Sleep(3000);
            driver.FindElement(valCST).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(8000);
            string value = driver.FindElement(valCSTPostUpdate).Text;
            return value;
        }

        //Update the value of EBITDA in Fees and Financials tab
        public string UpdateFeesAndFinAndValidate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEBITDA, 150);
            driver.FindElement(txtEBITDA).SendKeys("10");
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(11000);
            string value = driver.FindElement(valEBITDA).Text.Substring(0, 8);
            return value;
        }

        //Update the value of Est Referral Fee in Client/Subject & Referral tab
        public string UpdateEstReferralFeeAndValidate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEstRefFee, 150);
            driver.FindElement(txtEstRefFee).SendKeys("10");
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(valEstFee).Text.Substring(0, 8);
            return value;
        }

        //Validate the edit functionality of Client/Subject & Referral section
        public string ValidateMandatoryValidationOfClientSubject()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,350)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnShowMore, 150);
            driver.FindElement(btnShowMore).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditClient, 150);
            driver.FindElement(btnEditClient).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnTypeClient).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/div/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[4]/span[2]/span")).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnCloseMsg).Click();
            driver.FindElement(btnCancelL).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, valUpdatedType, 150);
            string value = driver.FindElement(valUpdatedType).Text;
            return value;
        }

        //Save the Billing Comment
        public string SaveBillingComment()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtDate, 150);
            driver.FindElement(txtDate).SendKeys("3/22/2023");
            WebDriverWaits.WaitUntilEleVisible(driver, btnStatus, 210);
            Thread.Sleep(3000);
            driver.FindElement(btnStatus).Click();
            driver.FindElement(btnStatus).Click();
            Thread.Sleep(3000);
            driver.FindElement(valStatus).Click();
            driver.FindElement(txtBillingComment).SendKeys("Testing");
            driver.FindElement(btnSaveDetailsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valBillingID, 150);
            string id = driver.FindElement(valBillingID).Text;
            return id;
        }

        //Edit the Billing Comment and validate error message
        public string ValidateMandatoryMessageUponEditingBillingComment()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditBillingComment, 150);
            driver.FindElement(btnEditBillingComment).Click();
            //WebDriverWaits.WaitUntilEleVisible(driver, btnEditComment, 190);
            //driver.FindElement(btnEditComment).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtBillingComment, 190);
            driver.FindElement(txtBillingComment).Clear();
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnCloseBilling).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgComment, 150);
            string value = driver.FindElement(msgComment).Text;
            return value;
        }

        //Edit billing comments and validate the updated comments'
        public string ValidateEditFunctionalityOfBillingComment(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtBillingComment, 190);
            driver.FindElement(txtBillingComment).SendKeys(value);
            driver.FindElement(btnSaveDetailsL).Click();
            //WebDriverWaits.WaitUntilEleVisible(driver, lnkAddedComment, 190);
            //driver.FindElement(lnkAddedComment).Click();
            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valEditComment, 180);
            string comment = driver.FindElement(valEditComment).Text;
            return comment;
        }

        //Delete Billing Comments and validate the same
        public string ValidateDeleteFunctionalityOfBillingComment()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, tabEng, 190);
            //driver.FindElement(tabEng).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteComment, 150);
            driver.FindElement(btnDeleteComment).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmDelete, 150);
            driver.FindElement(btnConfirmDelete).Click();
            Thread.Sleep(3000);
            try
            {
                driver.Navigate().Refresh();
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, valBillingID, 150);
                string id = driver.FindElement(valBillingID).Text;
                return id;
            }

            catch (Exception e)
            {
                return "Billing comment does not exist";
            }
        }

        //Validate if Legal Matters sub tab is editable after clicking pencil icon
        public string ValidateLegalMattersSubTabIsEditable()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditConfAgree, 150);
            driver.FindElement(lnkEditConfAgree).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            string value = driver.FindElement(btnSaveDetailsL).Displayed.ToString();
            return value;
        }
        //Update the value of Compliance Date in Compliance tab
        public string UpdateComplianceDetailsAndValidate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBeneficial, 150);
            driver.FindElement(btnBeneficial).Click();
            driver.FindElement(By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[1]//lightning-base-combobox-item[3]/span[2]/span")).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(9000);
            WebDriverWaits.WaitUntilEleVisible(driver, valBeneficial, 150);
            string value = driver.FindElement(valBeneficial).Text;
            return value;
        }

        //Update the value of Date Signed in Legal Matters tab
        public string UpdateDateSignedAndValidate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtDateSigned, 150);
            driver.FindElement(txtDateSigned).SendKeys("4/11/2023");
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(valDateSigned).Text;
            return value;
        }

        //Get Comments tab
        public string ValidateCommentsTab()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabComments, 100);
            string value = driver.FindElement(tabComments).Text;
            return value;
        }

        //Get Financials tab
        public string ValidateFinancialsTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabFinancials, 100);
            string value = driver.FindElement(tabFinancials).Text;
            return value;

        }

        //Get Eng Contacts tab
        public string ValidateEngContactsTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngContacts, 100);
            string value = driver.FindElement(tabEngContacts).Text;
            return value;

        }

        //Get CST tab
        public string ValidateCSTTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabCST, 100);
            string value = driver.FindElement(tabCST).Text;
            return value;

        }

        //Validate update functionality of existing comment
        public string ValidateUpdateFunctionalityOfEngComment()
        {
            Thread.Sleep(3000);
            //WebDriverWaits.WaitUntilEleVisible(driver, btnView, 190);
            //driver.FindElement(btnView).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditComments, 150);
            driver.FindElement(lnkEditComments).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtEditComment, 170);
            driver.FindElement(txtEditComment).Clear();
            driver.FindElement(txtEditComment).SendKeys("Test Comments");
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedComment, 180);
            string name = driver.FindElement(valAddedComment).Text;
            return name;
        }

        //Add Engagement Comments
        public void AddEngCommentaAndValidate()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabComments, 150);
            driver.FindElement(tabComments).Click();
            Thread.Sleep(4000);
            //WebDriverWaits.WaitUntilEleVisible(driver, lnkComments, 150);
            //driver.FindElement(lnkComments).Click();
            //Thread.Sleep(5000);
            //WebDriverWaits.WaitUntilEleVisible(driver, lnkNewComment, 150);
            //driver.FindElement(lnkNewComment).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnComments, 150);
            driver.FindElement(btnComments).Click();
            Thread.Sleep(4000);
            driver.FindElement(valCommentsType).Click();
            driver.FindElement(txtCommentNotes).SendKeys("Testing");
            driver.FindElement(btnSaveComments).Click();
            Thread.Sleep(4000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,550)");
            //WebDriverWaits.WaitUntilEleVisible(driver, valAddedCommentType, 170);
            //string commentType = driver.FindElement(valAddedCommentType).Text;
            //return commentType;
        }


        //Add Financials.
        public string AddFinancialsAndValidate()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabFinancials, 150);
            driver.FindElement(tabFinancials).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnFinancials, 150);
            driver.FindElement(btnFinancials).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkAddFinancials, 160);
            driver.FindElement(lnkAddFinancials).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtRelatedEng, 160);
            driver.FindElement(txtRelatedEng).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valRelatedEng, 170);
            driver.FindElement(valRelatedEng).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 180);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(6000);
            string id = driver.FindElement(valFinancials).Text;
            return id;
        }
        //Validate Eng Contacts
        public string ValidateEngContacts()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngagementNumL, 150);
            driver.FindElement(tabEngagementNumL).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngContacts, 150);
            driver.FindElement(tabEngContacts).Click();
            Thread.Sleep(6000);
            string name = driver.FindElement(valEngContact).Text;
            return name;
        }

        //Validate update functionality of Engagement Contact
        public string ValidateUpdateFunctionalityOfEngContacts()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEngContact, 150);
            driver.FindElement(btnEngContact).Click();
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditComment, 160);
            driver.FindElement(btnEditComment).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClearContact, 170);
            driver.FindElement(btnClearContact).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngContact, 180);
            driver.FindElement(txtEngContact).SendKeys("Sonika Goyal");
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//lightning-lookup/lightning-lookup-desktop/lightning-grouped-combobox/div[1]/div/lightning-base-combobox/div/div//div[2]/ul/li/lightning-base-combobox-item")).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnParty).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//slot/records-record-layout-row[4]/slot//lightning-combobox/div/div[1]/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 190);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(8000);
            string id = driver.FindElement(valEngContact).Text;
            return id;
        }

        //Validate Update Backlog button
        public string ValidateUpdateBacklogButton()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnUpdateBacklog, 100);
            string value = driver.FindElement(btnUpdateBacklog).Text;
            return value;
        }

        //Validate validation of Update Backlog button
        public string ValidateMandatoryValidationOfUpdateBacklog()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnUpdateBacklog, 100);
            driver.FindElement(btnUpdateBacklog).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngNameBacklog, 200);
            driver.FindElement(txtEngNameBacklog).Clear();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveBacklog, 200);
            driver.FindElement(btnSaveBacklog).Click();
            string validation = driver.FindElement(msgEngNameBacklog).Text;
            return validation;
        }

        //Validate validation of Update Backlog button
        public string ValidateEditFunctionalityOfUpdateBacklog()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngNameBacklog, 100);
            driver.FindElement(txtEngNameBacklog).Clear();
            driver.FindElement(txtEngNameBacklog).SendKeys("Testing");
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveBacklog, 200);
            driver.FindElement(btnSaveBacklog).Click();
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
            string value = driver.FindElement(valEngNameL).Text;
            return value;
        }


        //Validate Add Client button
        public string ValidateAddClientButton()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddClientL, 100);
            string value = driver.FindElement(btnAddClientL).Text;
            return value;
        }

        //Validate validation of Add Client button
        public string ValidateMandatoryValidationOfAddClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddClientL, 100);
            driver.FindElement(btnAddClientL).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveBacklog, 200);
            driver.FindElement(btnSaveBacklog).Click();
            string validation = driver.FindElement(msgEngNameBacklog).Text;
            return validation;
        }

        //Validate validation of Add Client button
        public string ValidateEditFunctionalityOfAddClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanies, 100);
            driver.FindElement(txtCompanies).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div[2]/ul/li[2]/a/div[2]/div[1]")).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveBacklog, 200);
            driver.FindElement(btnSaveBacklog).Click();
            Thread.Sleep(4000);
            driver.FindElement(tabClientSub).Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,450)");
            Thread.Sleep(4000);
            string name = driver.FindElement(valCompanyName).Text;
            return name;
        }

        //Validate Add Subject button
        public string ValidateAddSubjectButton()
        {
            Thread.Sleep(3000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,-400)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddSubjectL, 100);
            string value = driver.FindElement(btnAddSubjectL).Text;
            return value;
        }

        //Validate validation of Add Subject button
        public string ValidateMandatoryValidationOfAddSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddSubjectL, 100);
            driver.FindElement(btnAddSubjectL).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveBacklog, 200);
            driver.FindElement(btnSaveBacklog).Click();
            Thread.Sleep(3000);
            string validation = driver.FindElement(msgEngNameBacklog).Text;
            return validation;
        }

        //Validate Edit functionality of Add Subject button
        public string ValidateEditFunctionalityOfAddSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanies, 100);
            driver.FindElement(txtCompanies).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div[2]/ul/li[2]/a/div[2]/div[1]")).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveBacklog, 200);
            driver.FindElement(btnSaveBacklog).Click();
            Thread.Sleep(4000);
            driver.FindElement(tabClientSub).Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,450)");
            Thread.Sleep(4000);
            string name = driver.FindElement(valSubjectComp).Text;
            return name;
        }

        //Get type of added company
        public string GetTypeOfSubCompany()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valSubjectType, 200);
            string name = driver.FindElement(valSubjectType).Text;
            return name;
        }

        //Get type of added client company
        public string GetTypeOfClientCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valClientType, 200);
            string name = driver.FindElement(valClientType).Text;
            return name;
        }

        //Validate Add Other Party button
        public string ValidateAddOtherPartyButton()
        {
            Thread.Sleep(3000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,-400)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddOtherL, 100);
            string value = driver.FindElement(btnAddOtherL).Text;
            return value;
        }

        //Validate validation of Add Other Party button
        public string ValidateMandatoryValidationOfAddOtherParty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddOtherL, 100);
            driver.FindElement(btnAddOtherL).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveBacklog, 200);
            driver.FindElement(btnSaveBacklog).Click();
            Thread.Sleep(3000);
            string validation = driver.FindElement(msgEngNameBacklog).Text;
            return validation;
        }

        //Validate Edit functionality of Add Other Party button
        //Validate Edit functionality of Add Other Party button
        public string ValidateEditFunctionalityOfAddOtherParty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanies, 100);
            driver.FindElement(txtCompanies).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//div/div/div/div[1]/div[1]/div/div/div[2]/ul/li[1]")).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveBacklog, 200);
            driver.FindElement(btnSaveBacklog).Click();
            Thread.Sleep(4000);
            driver.FindElement(tabClientSub).Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,450)");
            Thread.Sleep(4000);
            string name = driver.FindElement(valOtherComp).Text;
            return name;
        }
        //Get type of added Other company
        public string GetTypeOfOtherCompany()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,-350)");
            WebDriverWaits.WaitUntilEleVisible(driver, valOtherType, 200);
            string name = driver.FindElement(valOtherType).Text;
            return name;
        }

        //Validate Add CF Engagement Contact button
        public string ValidateAddCFEngContactButton()
        {
            Thread.Sleep(3000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,-400)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddEngContact, 100);
            string value = driver.FindElement(btnAddEngContact).Text;
            return value;
        }

        //Validate validation of Add CF Engagement Contact button
        public string ValidateMandatoryValidationOfAddCFEngContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddEngContact, 100);
            driver.FindElement(btnAddEngContact).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveBacklog, 200);
            driver.FindElement(btnSaveBacklog).Click();
            Thread.Sleep(3000);
            string validation = driver.FindElement(msgEngNameBacklog).Text;
            return validation;
        }

        //Validate Edit functionality of Add CF Engagement Contact button
        public string ValidateEditFunctionalityOfAddEngContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtContacts, 100);
            driver.FindElement(txtContacts).SendKeys("Shivali Sharma");
            Thread.Sleep(12000);
            driver.FindElement(By.XPath("//div[2]/ul/li[1]/a/div[1]/span/img")).Click();
            Thread.Sleep(3000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,300)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnPartyContact, 100);
            driver.FindElement(btnPartyContact).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//a[text()='Seller']")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveBacklog, 200);
            driver.FindElement(btnSaveBacklog).Click();
            Thread.Sleep(6000);
            driver.FindElement(tabEngContacts).Click();
            Thread.Sleep(5000);
            string name = driver.FindElement(valAddedContactNum).Text;
            return name;
        }

        public string GetEngNumberL()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valEngNumL, 200);
            string name = driver.FindElement(valEngNumL).Text;
            return name;
        }

        //Validate Billing Request button
        public string ValidateBillingRequestButton()
        {
            Thread.Sleep(3000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,-150)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnBillingRequestL, 100);
            string value = driver.FindElement(btnBillingRequestL).Text;
            return value;
        }

        //Validate Additional CCs section on Billing Request button
        public string ValidateAdditionalCCSecction()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnBillingRequestL, 100);
            driver.FindElement(btnBillingRequestL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//div[1]/div/div/div/force-aloha-page/div/iframe")));
            //frmae //div[1]/div/div/div/force-aloha-page/div/iframe
            WebDriverWaits.WaitUntilEleVisible(driver, secAdditionalCC, 100);
            string value = driver.FindElement(secAdditionalCC).Text;
            return value;
        }

        //Validate validation of Billing Request
        public string ValidateMandatoryValidationOfBillingRequest()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSendEmailL, 200);
            driver.FindElement(btnSendEmailL).Click();
            Thread.Sleep(3000);
            string validation = driver.FindElement(msgSendEmail).Text;
            return validation;
        }

        //Validate Cancel Functionality of Billing Request
        public string ValidateCancelFunctionalityOfBillingRequest()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelSendEmail, 200);
            driver.FindElement(btnCancelSendEmail).Click();
            Thread.Sleep(3000);
            string name = driver.FindElement(tabInfo).Text;
            return name;
        }

        //Validate Send Email Functionality of Billing Request
        public string ValidateSendEmailFunctionalityOfBillingRequest()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtTo, 200);
            driver.FindElement(txtTo).SendKeys("Sonika Goyal");
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("/html/body/ul[1]/li/a")).Click();
            driver.FindElement(btnSendEmailL).Click();
            string name = driver.FindElement(tabInfo).Text;
            return name;
        }

        //Validate Bid tab
        public string ValidateBidTab()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabMore, 200);
            driver.FindElement(tabMore).Click();
            Thread.Sleep(3000);
            string name = driver.FindElement(lblBid).Text;
            driver.FindElement(lblBid).Click();
            return name;
        }

        //Validate Report tab
        public string ValidateReportTab()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabMore, 200);
            driver.FindElement(tabMore).Click();
            Thread.Sleep(3000);
            string name = driver.FindElement(lblReport).Text;
            driver.FindElement(lblReport).Click();
            return name;
        }

        //Validate Engagement Report button
        public string ValidateEngReportButton()
        {
            Thread.Sleep(4000);
            driver.FindElement(btnEngReport).Click();
            Thread.Sleep(3000);
            string name = driver.FindElement(titleEngReport).Text;
            driver.FindElement(titleEngReport).Click();
            return name;
        }

        //Click More button on Top panel
        public void ClickEngReportsButton()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnMoreEng, 120);
                driver.FindElement(btnMoreEng).Click();
                Thread.Sleep(3000);
                driver.FindElement(lnkEngReports).Click();
                Thread.Sleep(4000);
            }
            catch (Exception)
            {
                driver.FindElement(btnEngReportsL).Click();
                Thread.Sleep(4000);
            }
        }

        //Validate displayed reports
        public bool VerifyReportNamesForNonDealTeamMemberLightning()
        {
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='accessibility title']")));
            IReadOnlyCollection<IWebElement> valReportNames = driver.FindElements(tblReports);
            var actualValue = valReportNames.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Capital Markets Contact Log", "Counterparty List Report", "PIF" };
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
            driver.FindElement(btnReturnToEngLightning).Click();
            driver.SwitchTo().DefaultContent();
            return isSame;
        }
        public string GetEngagementNumberL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngNumberL, 30);
            return driver.FindElement(txtEngNumberL).Text;
        }
        public string GetEngagementNameL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngNameL, 30);
            return driver.FindElement(txtEngNameL).Text;
        }
        public int AddEngMultipleDealTeamMembers(string RecordType, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkHLInternalTeam, 20);
            driver.FindElement(linkHLInternalTeam).Click();
            Thread.Sleep(2000);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeam));
            Thread.Sleep(2000);
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnEngModifyRoles));
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            int rowCount = ReadExcelData.GetRowCount(excelPath, "EngDealTeamMembers");
            int totalDealTeamMemberadded = 0;
            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "EngDealTeamMembers", row, 1);
                    Thread.Sleep(5000);
                    WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 20);
                    driver.FindElement(txtStaff).SendKeys(valStaff);
                    Thread.Sleep(5000);
                    CustomFunctions.MultiSelectValueWithoutSelect(driver, listStaff, valStaff);
                    Thread.Sleep(2000);
                    if (RecordType == "CF")
                    {
                        WebDriverWaits.WaitUntilEleVisible(driver, checkCFSpeciality, 20);
                        driver.FindElement(checkCFSpeciality).Click();
                    }
                    else
                    {
                        WebDriverWaits.WaitUntilEleVisible(driver, checkSpeciality, 20);
                        driver.FindElement(checkSpeciality).Click();
                    }
                    driver.FindElement(btnSaveITTeam).Click();
                    totalDealTeamMemberadded = row - 2;
                }
                catch (Exception)
                {
                    return row - 2;
                }
            }
            return totalDealTeamMemberadded;
        }
        public int AddEngMultipleDealTeamMembersLV(string RecordType, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngInternalTeamL, 30);
            driver.FindElement(tabEngInternalTeamL).Click();
            Thread.Sleep(8000);

            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamDetailPage));
            Thread.Sleep(4000);
            driver.FindElement(btnModifyRolesL).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamModifyPage));

            int rowCount = ReadExcelData.GetRowCount(excelPath, "EngDealTeamMembers");
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveITTeam));
            int totalDealTeamMemberadded = 0;
            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "EngDealTeamMembers", row, 1);
                    Thread.Sleep(5000);
                    WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 20);
                    driver.FindElement(txtStaff).SendKeys(valStaff);
                    Thread.Sleep(5000);
                    CustomFunctions.MultiSelectValueWithoutSelect(driver, listStaff, valStaff);
                    Thread.Sleep(2000);
                    try
                    {
                        if (RecordType == "CF")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkCFSpeciality, 20);
                            driver.FindElement(checkCFSpeciality).Click();
                        }
                        else
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkSpeciality, 20);
                            driver.FindElement(checkSpeciality).Click();
                        }
                        driver.FindElement(btnSaveITTeam).Click();
                        totalDealTeamMemberadded = row - 2;
                    }
                    catch (Exception e)
                    {
                        WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 20);
                        driver.FindElement(txtStaff).Clear();
                        driver.FindElement(txtStaff).SendKeys(valStaff);
                        Thread.Sleep(5000);
                        CustomFunctions.MultiSelectValueWithoutSelect(driver, listStaff, valStaff);
                        Thread.Sleep(2000);
                        if (RecordType == "CF")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkCFSpeciality, 20);
                            driver.FindElement(checkCFSpeciality).Click();
                        }
                        else
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkSpeciality, 20);
                            driver.FindElement(checkSpeciality).Click();
                        }
                        driver.FindElement(btnSaveITTeam).Click();
                        totalDealTeamMemberadded = row - 2;
                    }

                }
                catch (Exception)
                {
                    return row - 2;
                }
            }
            driver.SwitchTo().DefaultContent();
            return totalDealTeamMemberadded;
        }
        public bool IsAssociatedEngFieldPresent()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtAssociatedEngLabel, 10);
                return driver.FindElement(txtAssociatedEngLabel).Displayed;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //Validate displayed reports
        public bool VerifyReportNamesForNonDealMemberClassic()
        {
            IReadOnlyCollection<IWebElement> valReportNames = driver.FindElements(tblReports);
            var actualValue = valReportNames.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Capital Markets Contact Log", "Counterparty List Report", "PIF" };
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
            driver.FindElement(btnReturnToEngLightning).Click();
            return isSame;
        }
        public bool IsAssociatedEngFieldPresentLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtAssociatedEngLabelL, 10);
                return driver.FindElement(txtAssociatedEngLabelL).Displayed;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        //Validate if Compliance sub tab is editable after clicking pencil icon
        public string ValidateComplianceSubTabIsEditable()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditBeneficial, 150);
            driver.FindElement(lnkEditBeneficial).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            string value = driver.FindElement(btnSaveDetailsL).Displayed.ToString();
            return value;
        }
        //Validate delete functionality of existing comment
        public string ValidateDeleteFunctionalityOfEngComment()
        {
            Thread.Sleep(4000);
            //WebDriverWaits.WaitUntilEleVisible(driver, btnViewDel, 150);
            //driver.FindElement(btnViewDel).Click();
            //Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDeleteComment, 160);
            driver.FindElement(lnkDeleteComment).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmDelete, 170);
            driver.FindElement(btnConfirmDelete).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgDeleteComment, 180);
            string message = driver.FindElement(msgDeleteComment).Text;
            return message;
        }
        public bool IsAssociatedEngFieldEditable()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 10);
                driver.FindElement(btnEdit).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, editAssociatedEngField, 10);
                bool IsDisplayed = driver.FindElement(editAssociatedEngField).Displayed;
                WebDriverWaits.WaitUntilEleVisible(driver, btnCancelEditForm, 10);
                driver.FindElement(btnCancelEditForm).Click();
                return IsDisplayed;
            }
            catch (Exception e)
            {
                try
                {
                    driver.FindElement(btnCancelEditForm).Click();
                }
                catch (Exception ex)
                {
                    if (driver.FindElement(txtPrivileges).Displayed)
                    {
                        return false;
                    }
                }
                return false;
            }
        }
        public bool IsAssociatedEngFieldEditableLV()
        {
            try
            {
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEditEngL, 10);
                driver.FindElement(btnEditEngL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, editAssociatedEngFieldL, 10);
                bool IsDisplayed = driver.FindElement(editAssociatedEngFieldL).Displayed;
                WebDriverWaits.WaitUntilEleVisible(driver, btnCancelEditFormL, 10);
                driver.FindElement(btnCancelEditFormL).Click();
                return IsDisplayed;
            }
            catch (Exception e)
            {
                try
                {
                    if (driver.FindElement(txtPrivileges).Displayed)
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    driver.FindElement(btnCancelEditFormL).Click();
                }
                return false;
            }
        }
        public void EnterAssociatedEngagement(string name)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 10);
                driver.FindElement(btnEdit).Click();
                driver.FindElement(editAssociatedEngField).Clear();
                driver.FindElement(editAssociatedEngField).SendKeys(name);
                driver.FindElement(btnSave).Click();
                Thread.Sleep(8000);
            }
            catch (Exception e)
            {
                driver.FindElement(btnCancelEditForm).Click();
            }
        }
        public string GetAssociatedEngagement()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtAssociatedEng, 20);
            return driver.FindElement(txtAssociatedEng).Text;
        }
        public void EnterAssociatedEngagementLV(string name)
        {
            Thread.Sleep(2000);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEditEngL, 10);
                driver.FindElement(btnEditEngL).Click();

                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, iconClearAssociatedEngL, 10);
                    CustomFunctions.MoveToElement(driver, driver.FindElement(lblEngDesc));
                    jse.ExecuteScript("arguments[0].click();", driver.FindElement(iconClearAssociatedEngL));
                    driver.FindElement(editAssociatedEngFieldL).SendKeys(name);
                    driver.FindElement(By.XPath($"//div[@role='listbox']//ul//li//lightning-base-combobox-formatted-text[@title='{name}']")).Click();
                    CustomFunctions.SelectValueWithoutSelect(driver, editAssociatedEngFieldL, name);
                }
                catch (Exception e)
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, editAssociatedEngFieldL, 10);
                    driver.FindElement(editAssociatedEngFieldL).SendKeys(name);
                    Thread.Sleep(2000);
                    driver.FindElement(By.XPath($"//div[@role='listbox']//ul//li//lightning-base-combobox-formatted-text[@title='{name}']")).Click();
                }

                driver.FindElement(btnSaveDetailsL).Click();
                Thread.Sleep(10000);
            }
            catch (Exception e)
            {
                driver.FindElement(btnCancelEditFormL).Click();
            }
        }
        public string GetAssociatedEngagementLV()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtAssociatedEngL, 20);
            return driver.FindElement(txtAssociatedEngL).Text;
        }
        public bool IsIndustryTypePresentInDropdownOppDetailPage(string IndustryType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 10);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboIG, 10);
            driver.FindElement(comboIG).Click();
            bool isFound = false;
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboIGOptions);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            for (int row = 0; row < actualValue.Length; row++)
            {
                if (actualValue[row].Contains(IndustryType))
                {
                    isFound = true;
                    break;
                }
            }
            driver.FindElement(btnCancel).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 20);
            return isFound;
        }

        public void ClickDetailPageQuickLink(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _DetailPageQuickLink(value), 20);
            driver.FindElement(_DetailPageQuickLink(value)).Click();
        }

        public string GetContractName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContract, 20);
            return driver.FindElement(lnkContract).Text;
        }

        public void ClickContractName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContract, 20);
            driver.FindElement(lnkContract).Click();
        }
        public string GetOppJobType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valJobType, 10);
            return driver.FindElement(valJobType).Text;
        }
        public string UpdateJobType(string jobType)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
                driver.FindElement(btnEdit).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, comboJobType, 80);
                driver.FindElement(comboJobType).SendKeys(jobType);
                driver.FindElement(btnSave).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valJobType, 20);
                return driver.FindElement(valJobType).Text;
            }
            catch (Exception)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 100);
                driver.FindElement(lnkReDisplayRec).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 100);
                driver.FindElement(btnEdit).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, comboJobType, 80);
                driver.FindElement(comboJobType).SendKeys(jobType);
                driver.FindElement(btnSave).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valJobType, 20);
                return driver.FindElement(valJobType).Text;
            }
        }
        public string GetEngERPIntegrationStatus()
        {
            Thread.Sleep(8000);
            driver.Navigate().Refresh();
            Thread.Sleep(2000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valEngERPLastIntStatus, 80);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valEngERPLastIntStatus));
            string status = driver.FindElement(valEngERPLastIntStatus).Text;
            return status;
        }
        public bool IsViewCounterpartyButtonEngagementPageLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewCounterpartyL, 60);
            return driver.FindElement(btnViewCounterpartyL).Displayed;
        }
        public void ClickViewCounterpartyButtonEngagementPageLV()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewCounterpartyL, 60);
            driver.FindElement(btnViewCounterpartyL).Click();
            Thread.Sleep(8000);
        }
        public void ClickPanelRightEngagementPageLV(string name)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,150)");
            WebDriverWaits.WaitUntilEleVisible(driver, _panelRightEngagement(name), 60);
            driver.FindElement(_panelRightEngagement(name)).Click();
            By header = By.XPath($"//h1[contains(@title,'{name}')]");
            WebDriverWaits.WaitUntilEleVisible(driver, header, 20);
        }
        public bool IsJobTypePresentInDropdownOppDetailPage(string JobType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 10);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType, 10);
            driver.FindElement(comboJobType).Click();
            bool isFound = false;
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboJobTypeptions);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            for (int row = 0; row < actualValue.Length; row++)
            {
                if (actualValue[row].Contains(JobType))
                {
                    isFound = true;
                    break;
                }
            }
            driver.FindElement(btnCancel).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 20);
            return isFound;
        }
        public string GetDetailPageJobTypeLV()
        {
            Thread.Sleep(4000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtPreviousJobType, 20);
                return driver.FindElement(txtPreviousJobType).Text;
            }
            catch (Exception) { return "Job Type not found"; }
        }
        public void UpdateJobTypeLV(string oldJobType, string newJobType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(4000);
            By btnJobTypeL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordJob_Type')]//button[@data-value='" + oldJobType + "']");
            WebDriverWaits.WaitUntilEleVisible(driver, btnJobTypeL, 20);
            driver.FindElement(btnJobTypeL).Click();
            Thread.Sleep(3000);
            By eleJobType = By.XPath("//flexipage-field[contains(@data-field-id,'RecordJob_Type')]//lightning-base-combobox//div[2]//lightning-base-combobox-item//span[text()='" + newJobType + "']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleJobType));
            WebDriverWaits.WaitUntilEleVisible(driver, eleJobType, 20);
            Thread.Sleep(2000);
            driver.FindElement(eleJobType).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
        }


        public bool IsLinkedActivityDisplayed(string activity)
        {
            Thread.Sleep(5000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tabEngActivity, 20);
                driver.FindElement(tabEngActivity).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, _ActivitySubject(activity), 20);
                return driver.FindElement(_ActivitySubject(activity)).Displayed;
            }
            catch { return false; }
        }
        public int GetInernalTeamMembersCount()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkHLInternalTeam, 20);
            driver.FindElement(linkHLInternalTeam).Click();
            Thread.Sleep(2000);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeam));
            Thread.Sleep(2000);
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnEngModifyRoles));
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, listInternalDealTeam, 20);
            int memberCount = driver.FindElements(listInternalDealTeam).Count();
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpporEng);
            driver.FindElement(btnReturnToOpporEng).Click();
            return memberCount;
        }
        public void UpdateJobType(string oldJobType, string newJobType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(4000);
            By btnJobTypeL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordJob_Type')]//button[@data-value='" + oldJobType + "']");
            WebDriverWaits.WaitUntilEleVisible(driver, btnJobTypeL, 20);
            driver.FindElement(btnJobTypeL).Click();
            Thread.Sleep(3000);
            By eleJobType = By.XPath("//flexipage-field[contains(@data-field-id,'RecordJob_Type')]//lightning-base-combobox//div[2]//lightning-base-combobox-item//span[text()='" + newJobType + "']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleJobType));
            WebDriverWaits.WaitUntilEleVisible(driver, eleJobType, 20);
            driver.FindElement(eleJobType).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
        }

        //Validate displayed reports for deal team member
        public bool VerifyReportNamesForDealTeamMemberClassic()
        {
            IReadOnlyCollection<IWebElement> valReportNames = driver.FindElements(tblReports);
            var actualValue = valReportNames.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Capital Markets Contact Log", "Counterparty History Report", "Counterparty List and Contact Log", "Counterparty List Report", "Engagement Working Group List", "PIF", "Potential Counterparty List - Client Copy", "Potential Counterparty List - Client Status", "Potential Counterparty List - Long", "Potential Counterparty List - Medium", "Potential Counterparty List Summary - Multi-Page", "Potential Counterparty List Summary - Single Page", "Potential Counterparty List- Short", "Racetrack Report" };
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
            driver.FindElement(btnReturnToEngLightning).Click();
            return isSame;
        }

        //Validate Bid tab
        public string ValidateBidTabForAdmin()
        {
            Thread.Sleep(5000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,-500)");
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnMoreAdmin, 200);
            driver.FindElement(btnMoreAdmin).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabBidAdmin, 200);
            driver.FindElement(tabBidAdmin).Click();
            Thread.Sleep(3000);
            string name = driver.FindElement(lblBidAdmin).Text;
            driver.FindElement(lblBidAdmin).Click();
            return name;
        }

        //Validate existing bids
        public string ValidateExistingBids()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tblBids, 200);
            string table = driver.FindElement(tblBids).Displayed.ToString();
            return table;
        }

        //Validate the newly added bid
        public string ValidateAddedBid()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewBid, 200);
            driver.FindElement(btnNewBid).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSelectNewRound, 200);
            driver.FindElement(btnSelectNewRound).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//lightning-base-combobox-item/span[2]/span[text()='First']")).Click();
            string tab = driver.FindElement(tabAddedBid).Text;
            driver.Navigate().Refresh();
            return tab;
        }

        //Validate Mandatory fields validation
        public string ValidateMandatoryFieldValidationOfMinBid()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditMinBid, 200);
            driver.FindElement(lnkEditMinBid).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtMinBid, 350);
            driver.FindElement(txtMinBid).Clear();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblMinBid, 200);
            driver.FindElement(lblMinBid).Click();

            //Clear Max Bid
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditMaxBid, 350);
            driver.FindElement(lnkEditMaxBid).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtMinBid, 350);
            driver.FindElement(txtMinBid).Clear();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblMinBid, 200);
            driver.FindElement(lblMinBid).Click();

            //Clear Bid Date
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditBidDate, 350);
            driver.FindElement(lnkEditBidDate).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtBidDate, 350);
            driver.FindElement(txtBidDate).Clear();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblMinBid, 200);
            driver.FindElement(lblMinBid).Click();

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,500)");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveMinBid, 240);
            driver.FindElement(btnSaveMinBid).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnMsgBid, 200);
            driver.FindElement(btnMsgBid).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgMinBid, 200);
            string message = driver.FindElement(msgMinBid).Text;
            return message;
        }

        //Validate Mandatory fields validation
        public string ValidateMandatoryFieldValidationOfMaxBid()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgMaxBid, 200);
            string message = driver.FindElement(msgMaxBid).Text;
            return message;
        }

        //Validate Mandatory fields validation of Bid Date
        public string ValidateMandatoryFieldValidationOfBidDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgBidDate, 200);
            string message = driver.FindElement(msgBidDate).Text;
            driver.Navigate().Refresh();
            return message;
        }

        //Get the value of Min Bid
        public string GetMinBidValue()
        {
            Thread.Sleep(4000);
            string value = driver.FindElement(valMinBid).Text;
            return value;
        }

        //Validate manage functionality
        public string ValidateManageBidFunctionality(string bid)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnManage, 200);
            driver.FindElement(btnManage).Click();
            driver.FindElement(txtMinBid).Clear();
            driver.FindElement(txtMinBid).SendKeys(bid);
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblMinBid, 200);
            driver.FindElement(lblMinBid).Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,500)");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveMinBid, 240);
            driver.FindElement(btnSaveMinBid).Click();
            js.ExecuteScript("window.scrollTo(0,-300)");
            Thread.Sleep(3000);
            string value = driver.FindElement(valMinBid).Text;
            return value;
        }

        //Valdiate Engagement AR Receipt report
        public string ValidateEngARReceiptReport()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngAR, 140);
            driver.FindElement(lnkEngAR).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(4000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//div/iframe")));
            WebDriverWaits.WaitUntilEleVisible(driver, lblEngAR, 240);
            string value = driver.FindElement(lblEngAR).Text;
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            return value;
        }

        //Valdiate Engagement Expenses report
        public string ValidateEngExpReport()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngExp, 140);
            driver.FindElement(lnkEngExp).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(4000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//div/iframe")));
            WebDriverWaits.WaitUntilEleVisible(driver, lblEngAR, 240);
            string value = driver.FindElement(lblEngAR).Text;
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            return value;
        }
        //Valdiate Engagement Invoice report
        public string ValidateEngInvoiceReport()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngInvoice, 140);
            driver.FindElement(lnkEngInvoice).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(4000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//div/iframe")));
            WebDriverWaits.WaitUntilEleVisible(driver, lblEngAR, 240);
            string value = driver.FindElement(lblEngAR).Text;
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            return value;
        }

        //Valdiate Engagement Working Group List
        public string ValidateEngWorkingGroupReport()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngWorking, 140);
            driver.FindElement(lnkEngWorking).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblEngWorking, 240);
            string value = driver.FindElement(lblEngWorking).Text;
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            return value;
        }

        public string GetEngStartDateLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngImpDate, 20);
            driver.FindElement(tabEngImpDate).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtStartDate, 20);
            string txtDate = driver.FindElement(txtStartDate).Text;
            //Date is in DD/MM/YYY formate converting it into MM/DD/YYYY string originalDateText = "12/09/2023";
            string[] dateParts = txtDate.Split('/');
            string newDateText = $"{dateParts[1]}/{dateParts[0]}/{dateParts[2]}";
            return newDateText;
        }
        public string GetStageL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngStageL, 20);
            string stage = driver.FindElement(txtEngStageL).Text;
            return stage;
        }
        public void NavigateToAdministratorTabLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabInfo2ndL, 10);
            driver.FindElement(tabInfo2ndL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngAdministratorL, 20);
            driver.FindElement(tabEngAdministratorL).Click();
        }
        public string GetRecordTypeLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,600)");
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngRecordTypeL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtEngRecordTypeL));
            return driver.FindElement(txtEngRecordTypeL).Text;
        }       

        public string ValidateWomenLedFieldLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblWomenLedL, 20);
            return driver.FindElement(lblWomenLedL).Text;
        }
        public string GetValueWomenLedLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtValueWomenLedL, 20);
            return driver.FindElement(txtValueWomenLedL).Text;
        }

        public string GetEngDealTeammMemberLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeamL, 30);
            driver.FindElement(tabInternalTeamL).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='accessibility title']")));
            Thread.Sleep(2000);
            //driver.FindElement(btnModifyRolesL).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, txtInternalTeamMemberL, 20);
            string nameInternalTeamMember = driver.FindElement(txtInternalTeamMemberL).Text;
            driver.SwitchTo().DefaultContent();
            return nameInternalTeamMember;
        }
        public string GetEngExternalContactLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tabContactsL, 5);
                driver.FindElement(tabContactsL).Click();
            }
            catch
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tabContactsL2, 5);
                driver.FindElement(tabContactsL2).Click();
            }
            
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactNameL, 20);
            return driver.FindElement(txtContactNameL).Text;
        }
        public string GetWomenLedSectionNameLV(string recType)
        {
            if (recType.Equals("CF"))
            {
                return "Need toChange"; //secName;
            }
            else if (recType.Equals("FVA"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelAdminSectionLV);
                string secName = driver.FindElement(labelAdminSectionLV).Text;
                return secName;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelAdminSectionLV);
                string secName = driver.FindElement(labelAdminSectionLV).Text;
                return secName;
            }
        }
        public int GetInernalTeamMembersCountLV()
        {
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeamL, 30);
            driver.FindElement(tabInternalTeamL).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='accessibility title']")));
            Thread.Sleep(4000);
            driver.FindElement(btnModifyRolesL).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, internalTeamFrame, 20);
            driver.SwitchTo().Frame(driver.FindElement(internalTeamFrame));
            WebDriverWaits.WaitUntilEleVisible(driver, listInternalDealTeam, 20);
            int dealTeamCount = driver.FindElements(listInternalDealTeam).Count();
            driver.SwitchTo().DefaultContent();
            return dealTeamCount;
        }
        public bool IsButtonSharingDisplayedL()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnSharingL, 10);
                if (driver.FindElement(btnSharingL).Displayed)
                {
                    driver.FindElement(btnSharingL).Click();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButonL, 10);
                driver.FindElement(iconExpandMoreButonL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnMoreSharingL, 10);
                if (driver.FindElement(btnMoreSharingL).Displayed)
                {
                    driver.FindElement(btnMoreSharingL).Click();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool IsSharingGroupDisplayedLV(string value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            try
            {
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, btnEditSharingGroup, 10);
                    driver.FindElement(btnEditSharingGroup).Click();
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(tblSharingGroup));
                    WebDriverWaits.WaitUntilEleVisible(driver, tblSharingGroup, 10);
                    return driver.FindElement(_sharingGroup(value)).Displayed;
                }
                catch
                {
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(tblSharingGroup));
                    WebDriverWaits.WaitUntilEleVisible(driver, tblSharingGroup, 10);
                    return driver.FindElement(_sharingGroup(value)).Displayed;

                }
            }
            catch { return false; }
        }
        public void CloseSharingGroupPopupLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelSharingGroup, 10);
            driver.FindElement(btnCancelSharingGroup).Click();
        }
        public bool IsSharingUserDisplayedLV(string value)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tblSharingGroup, 10);
                return driver.FindElement(_sharingGroup(value)).Displayed;
            }
            catch
            {
                string lower = value.ToLower();
                string[] name = lower.Split(' ');
                string newName = name[0] + "." + name[1];
                By txtname = By.XPath($"//div[contains(@class,'recordsRecordShare')]//table//tbody//tr//lightning-base-formatted-text[contains(text(),'{newName}')]");
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, txtname, 10);
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(txtname));
                    return driver.FindElement(txtname).Displayed;
                }
                catch { return false; }
            }
        }
        public void ClickEngInfoTabLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabInfo, 10);
            driver.FindElement(tabInfo).Click();

        }
        public void ClickEngAdministrationTabLV()
        {
            WebDriverWaits.WaitUntilEleVisible(Driver, subTabAdmin, 10);
            driver.FindElement(subTabAdmin).Click();

        }
        public string GetLegalEntityLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngLegalEntityL, 10);
            return driver.FindElement(txtEngLegalEntityL).Text;
        }

        public string GetWomenLedLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtWomenLedL, 10);
            return driver.FindElement(txtWomenLedL).Text;
        }
        //Return to Engagement
        public void ClickReturnToEngagementLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamModifyPage));
            Thread.Sleep(2000);
            driver.FindElement(btnReturnToEngLightning).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 30);
        }
        public void ClickRelatedOpportunityLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkRelatedOppL, 30);
            driver.FindElement(linkRelatedOppL).Click();
            Thread.Sleep(10000);
        }
        public void UpdatePrimaryOfficeInlineLV(string value)
        {
            driver.Navigate().Refresh();
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, iconInlinePrimaryOfficeL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(iconInlinePrimaryOfficeL));
            driver.FindElement(iconInlinePrimaryOfficeL).Click();
            Thread.Sleep(2000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblExpense));
            driver.FindElement(comboPrimaryOfficeL).Click();
            By elePO = By.XPath($"//label[text()='Primary Office']/following::lightning-base-combobox-item//span[@title='{value}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(elePO));
            WebDriverWaits.WaitUntilEleVisible(driver, elePO, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elePO));
            driver.FindElement(elePO).Click();
            Thread.Sleep(2000);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(10000);
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
        }
        public void UpdateHLSectorIDLV(string sector)
        {
            driver.Navigate().Refresh();
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditL));
            driver.FindElement(btnEditL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerEditBox, 20);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClearHLSectionL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblEngDescL));
            driver.FindElement(btnClearHLSectionL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, inputHLSectorIDL, 20);
            driver.FindElement(inputHLSectorIDL).Click();
            driver.FindElement(inputHLSectorIDL).SendKeys(sector);
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, listHLSectorL, 20);
            driver.FindElement(listHLSectorL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 20);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(10000);
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
        }
        public void UpdateJobTypeLV(string jobType)
        {
            driver.Navigate().Refresh();
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditL));
            driver.FindElement(btnEditL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerEditBox, 20);
            WebDriverWaits.WaitUntilEleVisible(driver, btnJobTypeL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnJobTypeL));
            driver.FindElement(btnJobTypeL).Click();
            Thread.Sleep(2000);
            By eleJobType = By.XPath($"//label[text()='Job Type']/following::lightning-base-combobox-item//span[@title='{jobType}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleJobType, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleJobType));
            driver.FindElement(eleJobType).Click();
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(10000);
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
        }
        public void UpdateClientOwnershipLV(string client)
        {
            driver.Navigate().Refresh();
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditL));
            driver.FindElement(btnEditL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerEditBox, 20);
            WebDriverWaits.WaitUntilEleVisible(driver, comboClientOwnershipL, 10);
            driver.FindElement(comboClientOwnershipL).Click();
            Thread.Sleep(2000);
            By eleClientOwnership = By.XPath($"//label[text()='Client Ownership']/following::lightning-base-combobox-item//span[@title='{client}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleClientOwnership, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleClientOwnership));
            driver.FindElement(eleClientOwnership).Click();
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(10000);
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
        }
        public void UpdateRecordTypeLV(string JobType, string newLOBExl)
        {
            driver.Navigate().Refresh();
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(Driver, subTabAdmin, 10);
            driver.FindElement(subTabAdmin).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnChangeRecordTypeL, 20);            
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnChangeRecordTypeL));
            driver.FindElement(btnChangeRecordTypeL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerChangeRT, 20);
            driver.FindElement(_elmRecordType(JobType)).Click();
            driver.FindElement(btnChangeRTNextL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerEditBox, 20);
            driver.FindElement(btnLOBL).Click();
            By eleLOB = By.XPath($"//label[text()='Line of Business']/following::lightning-base-combobox-item//span[@title='{newLOBExl}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleLOB, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleLOB));
            driver.FindElement(eleLOB).Click();
            driver.FindElement(btnJobTypeL).Click();
            By eleJobType = By.XPath($"//label[text()='Job Type']/following::lightning-base-combobox-item//span[@title='{JobType}']");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, eleJobType, 5);
                CustomFunctions.MoveToElement(driver, driver.FindElement(eleJobType));
            }
            catch
            {
                driver.FindElement(btnJobTypeL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, eleJobType, 5);
                CustomFunctions.MoveToElement(driver, driver.FindElement(eleJobType));
            }
            driver.FindElement(eleJobType).Click();

            if (newLOBExl == "FVA")
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(txtEstFee));
                driver.FindElement(txtEstFee).SendKeys("10000");
            }

            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 20);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(15000);
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
        }
        public string GetClientOwnershipLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabInfo, 10);
            driver.FindElement(tabInfo).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valClientOwnershipL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valClientOwnershipL));
            string clientOwnership = driver.FindElement(valClientOwnershipL).Text;
            return clientOwnership;
        }

        public bool IsJobTypePresentInDropdownOppDetailPageLV(string JobType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 10);
            driver.FindElement(btnEditL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerEditBox, 20);
            WebDriverWaits.WaitUntilEleVisible(driver, btnJobTypeL, 10);
            driver.FindElement(btnJobTypeL).Click();
            bool isFound = false;
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(optionsJobTypeL);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            for (int row = 0; row < actualValue.Length; row++)
            {
                if (actualValue[row].Contains(JobType))
                {
                    isFound = true;
                    break;
                }
            }
            driver.FindElement(btnCancelL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            return isFound;
        }

        private By _quickLink(string linkText)
        {
            return By.XPath($"//flexipage-component2[contains(@data-component-id,'ListQuickLinks')]//a//slot[contains(text(),'{linkText}')]/../..");
        }

        public void ClickQuickLink(string linkName)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            WebDriverWaits.WaitUntilEleVisible(driver, _quickLink(linkName), 10);
            //CustomFunctions.MoveToElement(driver, driver.FindElement(_quickLink(linkName)));
            driver.FindElement(_quickLink(linkName)).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tableContractsL, 10);
            Thread.Sleep(10000);
        }
        public string GetExistingContractNameLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, rowContractL, 10);
                //CustomFunctions.MoveToElement(driver, driver.FindElement(rowContractL));
                string id = driver.FindElement(rowContractL).Text;
                return id;
            }
            catch (Exception)
            {
                return "Contract does not exist";
            }
        }

        //Get the type of Contract
        public string GetERPContractTypeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContractTypeL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPContractTypeL));
            string type = driver.FindElement(valERPContractTypeL).Text;
            return type;
        }

        //Get Contract ERP Business Unit
        public string GetContractERPBusinessUnitLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusUnitL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPBusUnitL));
            string unit = driver.FindElement(valERPBusUnitL).Text;
            return unit;
        }

        //Get Contract ERP Legal Entity Name
        public string GetContractERPLegalEntityNameLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPLegalEntityL));
            string entity = driver.FindElement(valERPLegalEntityL).Text;
            return entity;
        }

        //Get Contract ERP Bill Plan
        public string GetContractERPBillPlanLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBillPlanL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPBillPlanL));
            string plan = driver.FindElement(valERPBillPlanL).Text;
            return plan;
        }

        //Get Contract Bill To
        public string GetContractBillToLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valBillToL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valBillToL));
            string bill = driver.FindElement(valBillToL).GetAttribute("title");
            return bill;
        }

        //Get Contract Start Date
        public string GetContractStartDateLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStartDateL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valStartDateL));
            string date = driver.FindElement(valStartDateL).Text;
            return date;
        }

        //Get if Main Contract checkbox is checked
        public string GetIsMainContractStateLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valIsMainL, 10);
            //string main = driver.FindElement(valIsMainL).GetAttribute("title");
            CustomFunctions.MoveToElement(driver, driver.FindElement(valIsMainL));
            bool Enabled = driver.FindElement(valIsMainL).Selected;
            if (Enabled)
            {
                return "Is Main Contract checkbox is checked";
            }
            else
            {
                return "Is Main Contract checkbox is not checked";
            }
        }

        public void ClickEngagementContactList()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            WebDriverWaits.WaitUntilEleVisible(driver, tabSidePanelL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tabSidePanelL));
            driver.FindElement(tabSidePanelL).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkViewAllContactsL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lnkViewAllContactsL));
            //driver.FindElement(lnkViewAllContactsL).Click();
            IWebElement ViewAllContacts = driver.FindElement(lnkViewAllContactsL);
            js.ExecuteScript("arguments[0].click();", ViewAllContacts);
            WebDriverWaits.WaitUntilEleVisible(driver, tableContactsL, 10);
            Thread.Sleep(5000);
        }

        //Get Company name of contact
        public string GetCompanyNameOfContactLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompNameL, 10);
            string name = driver.FindElement(valCompNameL).GetAttribute("title");
            return name;
        }

        //Validate add counterparty functionality
        public string ValidateAddCounterpartyFunctionality()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterpartiesL, 150);
            driver.FindElement(btnAddCounterpartiesL).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkExistingCompanies, 150);
            driver.FindElement(lnkExistingCompanies).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewAllCompList, 150);
            driver.FindElement(btnViewAllCompList).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, radioCompName, 150);
            driver.FindElement(radioCompName).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 150);
            driver.FindElement(btnOK).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkCompany, 150);
            driver.FindElement(chkCompany).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterpartyTo, 150);
            driver.FindElement(btnAddCounterpartyTo).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackCounterparties, 150);
            driver.FindElement(btnBackCounterparties).Click();
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkCompCounterparty, 180);
            string name = driver.FindElement(lnkCompCounterparty).Text;
            return name;
        }
        //Validate search counterparty functionality
        public string ValidateSearchCounterpartyFunctionality()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchCounterparty, 150);
            driver.FindElement(txtSearchCounterparty).SendKeys("Skyhive");
            driver.FindElement(txtSearchCounterparty).Click();
            Thread.Sleep(8000);
            string name = driver.FindElement(lnkCompCounterparty).Text;
            return name;

        }
        //Search Contact using Name field
        public void SearchContactUsingName()
        {
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchBox, 180);
            driver.FindElement(txtSearchBox).SendKeys("Salmaan Jaffery");
            driver.FindElement(btnSearchContact).Click();
        }
        //Add Contact 
        public string AddContact()
        {
            Thread.Sleep(17000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkContact, 180);
            driver.FindElement(chkContact).Click();
            string name = driver.FindElement(valContact).Text;
            Thread.Sleep(4000);
            driver.FindElement(btnAddContact).Click();
            Thread.Sleep(8000);
            driver.FindElement(btnBackCounterparties).Click();
            Thread.Sleep(8000);
            return name;
        }
        //Validate contact on Counterparties page
        public string ValidateContactDetailsOnCounterpartiesPage()
        {
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            Thread.Sleep(4000);
            driver.Navigate().Refresh();
            Thread.Sleep(9000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 80);
            string name = driver.FindElement(lnkContacts).Text;
            return name;
        }

        By iconMoreActionComments = By.XPath("//article[@aria-label='Comments']//button/span[text()='Show more actions']/..");
        By lnkNew = By.XPath("//lightning-button-menu[contains(@class,'slds-is-open')]//div//a//span[text()='New']");
        By txtEngCommentsIDL = By.XPath("//h1//records-entity-label[text()='Engagement Comment']/../../..//lightning-formatted-text/../..//slot//lightning-formatted-text");

        public string GetEngagementCommentsID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngCommentsIDL, 20);
            return driver.FindElement(txtEngCommentsIDL).Text;
        }
        public void AddEngementCommentsLV(string commentType, string txtComments)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            
            WebDriverWaits.WaitUntilEleVisible(driver, tabCommentsL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tabCommentsL));
            driver.FindElement(tabCommentsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, iconMoreActionComments, 10);
            //js.ExecuteScript("arguments[0].click();", iconMoreActionComments);
            driver.FindElement(iconMoreActionComments).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkNew, 10);
            driver.FindElement(lnkNew).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, comboCommentTypeL, 10);
            driver.FindElement(comboCommentTypeL).Click();
            Thread.Sleep(2000);
            By eleType = By.XPath($"//label[text()='Comment Type']/..//lightning-base-combobox-item//span[@title='{commentType}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleType, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleType));
            driver.FindElement(eleType).Click();
            driver.FindElement(inputCommentL).SendKeys(txtComments);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(8000);
        }
        public void CickAddEngagementContactLV(string RecordType)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, _btnAddContactL(RecordType), 10);
            driver.FindElement(_btnAddContactL(RecordType)).Click();
        }
        public void CreateContactLV(string name, string party)
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactL, 20);
            driver.FindElement(txtContactL).SendKeys(name);
            Thread.Sleep(3000);
            try
            {
                By listContactOption = By.XPath($"//div[@role='listbox']//ul//li//a//div[2]//div[1][@title='{name}']");
                WebDriverWaits.WaitUntilEleVisible(driver, listContactOption, 5);
                driver.FindElement(listContactOption).Click();
            }
            catch (Exception ex)
            {
                By iconContactSearchItem = By.XPath("//div[contains(@class,'searchButton')]");
                WebDriverWaits.WaitUntilEleVisible(driver, iconContactSearchItem, 5);
                driver.FindElement(iconContactSearchItem).Click();
                By txtContact = By.XPath("//div[contains(@class,'gridInScroller')]//table//tbody//tr[1]//td[1]//a");
                WebDriverWaits.WaitUntilEleVisible(driver, txtContact, 20);
                driver.FindElement(txtContact).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnPartyL, 20);
            driver.FindElement(btnPartyL).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div[8]/div/ul/li/a[text()='" + party + "']")).Click();
            driver.FindElement(btnSaveContactL).Click();
            Thread.Sleep(5000);
        }

        public void ClickTabFSEngagementLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);
            try
            {
                try
                {
                    js.ExecuteScript("window.scrollTo(0,0)");
                    WebDriverWaits.WaitUntilEleVisible(driver, tabFSEngL, 5);
                    driver.FindElement(tabFSEngL).Click();
                }
                catch (Exception e)
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, iconHeaderMoreTabsL, 5);
                    driver.FindElement(iconHeaderMoreTabsL).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, lnkMoretabFSEngL, 5);
                    driver.FindElement(lnkMoretabFSEngL).Click();
                }
            }
            catch { }

            Thread.Sleep(10000);
        }

        public string CreateNewFSEngagementLV(string sponsor)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewFSEngL, 10);
            driver.FindElement(btnNewFSEngL).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, inputSponsorCompanyL, 10);
            driver.FindElement(inputSponsorCompanyL).Click();
            driver.FindElement(inputSponsorCompanyL).SendKeys(sponsor);
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, optionSponsorCompanyL, 10);
            driver.FindElement(optionSponsorCompanyL).Click();
            Thread.Sleep(2000);
            driver.FindElement(btnSaveDetailsL).Click();
            //Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtFSEngNameL, 10);
            return driver.FindElement(txtFSEngNameL).Text;
        }

        public string GetFSEngagementIDLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtFSEngIDL, 10);
            return driver.FindElement(txtFSEngIDL).Text;
        }
        By valSponsorCmpnyL = By.XPath("//table[@aria-label='FS Engagements']//tbody//tr[1]//lightning-primitive-cell-factory[@data-label='Sponsor Company']//a");
        public string GetFSEngSponsorCompanyLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSponsorCmpnyL, 10);
            return driver.FindElement(valSponsorCmpnyL).GetAttribute("title");
        }
        public void ClickEngContactTabLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngContactsL, 20);
            driver.FindElement(tabEngContactsL).Click();
            Thread.Sleep(5000);
        }
        By tabEngCommentsL = By.XPath("(//lightning-tab-bar/ul/li/a[text()='Comments'])[1]");
        public void ClickEngInfoCommentsTabLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabInfo, 10);
            driver.FindElement(tabInfo).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngCommentsL, 10);
            driver.FindElement(tabEngCommentsL).Click();
            Thread.Sleep(5000);
        }
        public string GetEngCommentPresentLV(string commentType)
        {            
            By txtComments = By.XPath($"//article[@aria-label='{commentType}']//table//td[@data-label='Comment']//lightning-base-formatted-text");
            WebDriverWaits.WaitUntilEleVisible(driver, txtComments, 10);
            return driver.FindElement(txtComments).Text.Trim();

        }
        public bool IsEngContactPresentLV(string contactNamePE)
        {
            bool result = false;            
            IList<IWebElement> engContacts = driver.FindElements(By.XPath("//table[@aria-label='Engagement Contacts']//tr//th//lightning-primitive-cell-factory//a[2]"));
            foreach(IWebElement engContact in engContacts)
            {
                result = false;
                if (engContact.Text == contactNamePE)
                {
                    result = true;
                    break;
                }
            }            
            return result;
        }
        
        public void ClickEngCommentsTabLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");            
            WebDriverWaits.WaitUntilEleVisible(driver, tabcommentsL, 20);
            driver.FindElement(tabcommentsL).Click();
            Thread.Sleep(5000);
        }
        public bool IsEngCommentsPresentLV(string txtPEComments)
        {
            bool isCommentFund = false;
            WebDriverWaits.WaitUntilEleVisible(driver, textEngCommentsL, 10);
            IList<IWebElement> engComments = driver.FindElements(textEngCommentsL);
            foreach(IWebElement engComment in engComments)
            {
                isCommentFund = false;             
                if (engComment.Text == txtPEComments)
                {
                    isCommentFund = true;
                    break;
                }                
            }
            return isCommentFund;
        }        
        public bool IsEngCounterparyCompaniesPresentLV(string addedCounterpartyCompany)
        {
            bool iscounterpartyfound = false;            
            IList<IWebElement> counterpartyName = driver.FindElements(counterpartyNameL);
            WebDriverWaits.WaitUntilEleVisible(driver, counterpartyNameL, 20);
            foreach (IWebElement counterparty in counterpartyName)
            {
                iscounterpartyfound = false;
                string availablecounterparty = counterparty.GetAttribute("data-cell-value");
                if (availablecounterparty == addedCounterpartyCompany)
                {
                   iscounterpartyfound = true;
                   break;
                }                
            }
            return iscounterpartyfound;
        }

        public void ClickTabEngFeeAndFincnciaLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);           
            js.ExecuteScript("window.scrollTo(0,0)");
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngFeeAndFinanciaL, 5);
            driver.FindElement(tabEngFeeAndFinanciaL).Click();
            Thread.Sleep(5000);
        }

        By txtEstTransMCapL = By.XPath("//span[contains(text(),'Transaction Size')]/../../..//lightning-formatted-text");
        By txtEbitdaL = By.XPath("//span[contains(text(),'EBITDA')]/../../..//lightning-formatted-text");
        By txtRetainerL = By.XPath("//span[contains(text(),'Retainer')]/../../..//lightning-formatted-text");
        By txtProgressMonthlyFeeL = By.XPath("//span[contains(text(),'Progress/Monthly Fee')]/../../..//lightning-formatted-text");
        By txtContingentFeeL = By.XPath("//span[contains(text(),'Contingent Fee')]/../../..//lightning-formatted-text");
        By txtTotalFeeL = By.XPath("//span[contains(text(),'Total Fee')]/../../..//lightning-formatted-text");
        By txtSSExpenseL = By.XPath("//span[contains(text(),'Shared Services Expense')]/../../..//lightning-formatted-text");
        By txtExpenseCapL = By.XPath("//span[contains(text(),'Expense Cap')]/../../..//lightning-formatted-text");
        By txtLegalCapL = By.XPath("//span[contains(text(),'Shared Services Expense')]/../../..//lightning-formatted-text");

        public string GetValEstTansacttionMarketCapLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEstTransMCapL, 10);
            return driver.FindElement(txtEstTransMCapL).Text;
        }
        public string GetValEbitdaLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEbitdaL, 10);
            return driver.FindElement(txtEbitdaL).Text;
        }
        public string GetValRetainerLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtRetainerL, 10);
            return driver.FindElement(txtRetainerL).Text;
        }
        public string GetValProgressMonthlyFeeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtProgressMonthlyFeeL, 10);
            return driver.FindElement(txtProgressMonthlyFeeL).Text;
        }
        public string GetValContingentFeeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtContingentFeeL, 10);
            return driver.FindElement(txtContingentFeeL).Text;
        }
        public string GetValTotalFeeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtTotalFeeL, 10);
            return driver.FindElement(txtTotalFeeL).Text;
        }
        public string GetValSSExpenseLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSSExpenseL, 10);
            return driver.FindElement(txtSSExpenseL).Text;
        }
        public string GetValExpenseCapLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtExpenseCapL, 10);
            return driver.FindElement(txtExpenseCapL).Text;
        }
        public string GetValLegalCapLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtLegalCapL, 10);
            return driver.FindElement(txtLegalCapL).Text;
        }
        //Validate New Opp Valuation Period button of an Opportunity converted to Engagement
        public string ValidateNewOppValPeriodButtonOfRelatedOpp(string user)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRelatedOppL, 120);
            driver.FindElement(valRelatedOppL).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnPortfolioValL, 160);
            driver.FindElement(btnPortfolioValL).Click();
            Thread.Sleep(6000);

            if (user.Contains("Karan Chopra"))
            {
                Thread.Sleep(5000);
                try
                {
                    //driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='accessibility title']")));
                    string valImage = driver.FindElement(btnNewOppValPeriodL).Displayed.ToString();
                    //driver.SwitchTo().DefaultContent();
                    return "New Opportunity Valuation Period button is displayed";
                }
                catch (Exception)
                {
                    return "New Opportunity Valuation Period button is not displayed";
                }
            }
            else
            {
                driver.SwitchTo().Frame(1);
                Thread.Sleep(5000);
                try
                {
                    string valImage = driver.FindElement(btnNewOppValPeriodL).Displayed.ToString();
                    driver.SwitchTo().DefaultContent();
                    return "New Opportunity Valuation Period button is displayed";
                }
                catch (Exception)
                {
                    driver.SwitchTo().DefaultContent();
                    return "New Opportunity Valuation Period button is not displayed";
                }
            }
        }
    }
}
