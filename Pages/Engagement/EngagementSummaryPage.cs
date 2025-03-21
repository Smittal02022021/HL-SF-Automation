﻿using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using MongoDB.Bson.Serialization.Conventions;
using System.Data;
using System.Transactions;
using System.Web;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace SF_Automation.Pages.Engagement
{
    class EngagementSummaryPage : BaseClass
    {
        By lblTransType = By.CssSelector("div[id*='id37'] > div[class='pbBody'] > table > tbody > tr:nth-child(1) > td:nth-child(1) > label");
        By lblTransTypeL = By.XPath("//label[text()='Transaction Type']");
        By lblCurrencyFinL = By.XPath("//label[text()='Currency Financials are reported in']");
        By lblProjectionsL = By.XPath("//label[text()='Projections are as of: ']");
        By lblLTMProjectionsL = By.XPath("//label[text()='LTM Projections are as of: ']");
        By lblRevenueL = By.XPath("//label[contains(text(),'Revenue')]");
        By lblEBITDAL = By.XPath("//label[contains(text(),'EBITDA')]");
        By lblCapexL = By.XPath("//label[contains(text(),'Capex')]");
        By lblDMAFieldsL = By.XPath("//table/thead/tr/th/span");
        By lblAddDistressedL = By.XPath("//slot/div//lightning-input-field//div[1]/label");

        By lblHLFinTable = By.XPath("//thead/tr[1]/th/span");
        By lblTotalFinAmt = By.XPath("//label[text()='Total Financing Amount']");
        By lblFinDescL = By.XPath("//label[text()='Financing Description']");
        By lblFinancingTypeL = By.XPath("//label[text()='Financing Type']");
        By lblOtherL = By.XPath("//label[text()='Notes']");
        By lblCurrencyAddDebtL = By.XPath("//label[text()='Currency']");
        By btnCurrencyL = By.XPath("//button[@name='CurrencyIsoCode']");
        By lblFinAmountL = By.XPath("//label[text()='Financing Amount (MM)']");
        By btnFinTypeL = By.XPath("//button[@name='Financing_Type__c']");
        By valFinTypesL = By.XPath("//button[@name='Financing_Type__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span");
        By btnSecTypeL = By.XPath("//button[@name='Security_Type__c']");
        By valSecTypesL = By.XPath("//button[@name='Security_Type__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span");
        By valDebtCurrencyL = By.XPath("//button[@name='CurrencyIsoCode']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span");
        By txtOtherL = By.XPath("//textarea[@name='Notes__c']");
        By txtFinAmtL = By.XPath("//input[@name='Financing_Amount__c']");
        By msgOtherL = By.XPath("//div[text()='Notes for Other Financing Types can only be saved if \"Other\" is selected from the drop-down.']");
        By valFinTypeL = By.XPath("//table/tbody/tr/th/div");
        By lblPreTransHeaders = By.XPath("//h3/span");
        By lblPreTransGrid = By.XPath("//c-engagement-fr-summary-pre-tran-info/div[1]/div[3]/div/div/table/thead/tr/th/span");
        By lblPostTransGrid = By.XPath("//c-engagement-fr-summary-post-tran-info/div[1]/div[3]/div/div/table/thead/tr/th/span");
        By lblPreReorg = By.XPath("//label[text()='Pre Reorganization Constituent Debt']");
        By lblPreReorgTotal = By.XPath("//label[text()='Pre Reorganized Total Debt']");
        By lblPostRestr = By.XPath("//label[text()='Post-Restructuring Total Debt (MM)']");
        By lblPostRestrComp = By.XPath("//label[text()='Net Debt of the Restructured Company (MM)']");
        By lblClosingStockL = By.XPath("//label[text()='Closing Stock Price (first full closing day post-restructuring)']");
        By lblPostTransOppL = By.XPath("//div[text()='Post-Transaction Opportunities']");
        By lblPostTransOppValuesL = By.XPath("//span[text()='Available']/ancestor::div[1]/div/ul/li/div/span/span");
        By lblPostTransOppNotesL = By.XPath("//label[text()='Post-Transaction Opportunity Notes']");
        By lblPostTransStaffRolesL = By.XPath("//span[text()='Post-Transaction Staff Roles']");
        By lblPostTransKeyExtContactL = By.XPath("//span[text()='Post-Transaction Key External Contact']");
        By lblPostTransStaffRolesHeaders = By.XPath("//span[text()='Post-Transaction Staff Roles']/ancestor::div[1]/div[1]/table/thead/tr/th/span");
        By lblPostTransKeyContacts = By.XPath("//span[text()='Post-Transaction Key External Contact']/ancestor::div[1]/div[1]/table/thead/tr/th/span");


        By valOtherL = By.XPath("//table/tbody/tr/td[1]/div");
        By btnAddEquityHolderL = By.XPath("//button[text()='Add Equity Holder']");
        By msgDupClientL = By.XPath("//div[contains(text(),'Company Name')]");
        By msgMandatoryKeyCredL = By.XPath("//label[text()='Client/Subject']/ancestor::lightning-grouped-combobox/div[2]");
        By txtPerOwnershipL = By.XPath("//input[@name='Percent_Ownership__c']");
        By btnAddBoardMemberL = By.XPath("//button[text()='Add Board Member']");
        By btnAddDebtStrL = By.XPath("//button[text()='Add Debt Structure']");

        By btnClientSubL = By.XPath("//input[@placeholder='Search Companies...']");
        By btnSecurityTypeL = By.XPath("//button[@name='Security_Type__c']");
        By txtInterestL = By.XPath("//input[@name='Interest__c']");
        By txtAmoritizationL = By.XPath("//input[@name='Amoritization__c']");
        By txtMandatoryL = By.XPath("//input[@name='Mandatory_Prepayments_ECF_Sweep__c']");
        By txtFeesL = By.XPath("//input[@name='Fees_Expenses__c']");
        By txtMaturityL = By.XPath("//input[@name='Maturity_Date__c']");
        By txtOIDL = By.XPath("//input[@name='OID_Percent__c']");
        By txtCallProvisionsL = By.XPath("//input[@name='Call_Provisions_Prepayment_Premiums__c']");
        By txtCovenantsL = By.XPath("//input[@name='Covenants__c']");
        By txtFacilityL = By.XPath("//input[@name='Facility_Balance_MM__c']");

        By rowAddDebtStructureL = By.XPath("//c-engagement-fr-summary-pre-tran-info/div[1]/div[3]/div/div/table/tbody/tr");
        By rowAddDebtStructurePostL = By.XPath("//c-engagement-fr-summary-post-tran-info/div[1]/div[3]/div/div/table/tbody/tr");

        By rowAddDebtStrcutureL2nd = By.XPath("//c-engagement-fr-summary-pre-tran-info/div[1]/div[3]/div/div/table/tbody/tr[2]");
        By rowAddDebtStrcutureL2ndPost = By.XPath("//c-engagement-fr-summary-post-tran-info/div[1]/div[3]/div/div/table/tbody/tr[2]");

        By rowAddKeyCredL = By.XPath("//slot/div/div[1]/lightning-input-field/lightning-lookup/lightning-lookup-desktop/lightning-grouped-combobox/div/div/lightning-base-combobox/div/div/div[1]/div/div/input");
        By valSecurityTypeL = By.XPath("//c-engagement-fr-summary-pre-tran-info/div[1]/div[3]/div/div/table/tbody/tr[2]/th");
        By valSecurityTypePostL = By.XPath("//c-engagement-fr-summary-post-tran-info/div[1]/div[3]/div/div/table/tbody/tr[2]/th");

        By rowAddEquityHolderL = By.XPath("//span[text()='Equity Holder']/ancestor::table/tbody/tr");
        By rowAddEquityHolderPostL = By.XPath("//c-engagement-fr-summary-post-tran-info/div[1]/div/div/div/table/tbody/tr");

        By rowAddBoardMemberL = By.XPath("//c-engagement-fr-summary-pre-tran-info/div[1]/div[2]/div/div/table/tbody/tr");
        By rowAddBoardMemberPostL = By.XPath("//c-engagement-fr-summary-post-tran-info/div[1]/div[2]/div/div/table/tbody/tr");

        By rowAddBoardMember2ndL = By.XPath("//c-engagement-fr-summary-pre-tran-info/div[1]/div[2]/div/div/table/tbody/tr[2]");
        By rowAddBoardMember2ndPostL = By.XPath("//c-engagement-fr-summary-post-tran-info/div[1]/div[2]/div/div/table/tbody/tr[2]");

        By msgDupContactL = By.XPath("//div[text()='Duplicate record detected.']");
        By msgDupKeyContactL = By.XPath("//div[text()='This person has already been added as an Engagement Contact. Please go to their Engagement Contact record and click the “Key External Contact” checkbox to add them to this list.']");
        By comboSecTypesL = By.XPath("//button[@name='Security_Type__c']/ancestor::div[2]/div[2]/lightning-base-combobox-item/span[2]/span");

        By lblClientSubL = By.XPath("//label[text()='Client/Subject']");
        By lblPercentOwnershipL = By.XPath("//label[text()='Percent Ownership']");
        By msgFinType = By.XPath("//label[text()='Financing Type']/ancestor::div[1]/div[text()='Complete this field.']");
        By msgSecType = By.XPath("//label[text()='Security Type']/ancestor::div[1]/div[text()='Complete this field.']");
        By lblContacts = By.XPath("//label[text()='Contact (External)']");
        By lblMaturityDate = By.XPath("//lightning-datepicker/div/label");
        By lblSecurityTypeL = By.XPath("//label[text()='Security Type']");
        By chk1stHLRel = By.XPath("//tr[1]/td[2]/div/lightning-input/lightning-primitive-input-checkbox");
        By chk2ndHLRel = By.XPath("//tr[2]/td[2]/div/lightning-input/lightning-primitive-input-checkbox");

        By btnAddDistressedL = By.XPath("//button[text()='Add Distressed M&A Information']");
        By tabDMAL = By.XPath("//li/a[text()='DM&A Info']");
        By btnAddHLFinL = By.XPath("//button[text()='Add HL Financing']");
        By lblPostTxnStatusL = By.XPath("//label[text()='Post Transaction Status']");
        By lblCompDescL = By.XPath("//label[text()='Company Description']");
        By lblClientDescL = By.XPath("//label[text()='Client Description']");
        By lblReTxnL = By.XPath("//label[text()='Restructuring Transaction Description']");
        By valTxnType = By.XPath("//label[text()='Transaction Type']/ancestor::div[@class='slds-col slds-size_1-of-2']/lightning-output-field/div/lightning-formatted-text");
        By txtTxnType = By.XPath("//label[text()='Transaction Type']/ancestor::div[@class='slds-col slds-size_1-of-2']/lightning-output-field/div/lightning-formatted-text");
        By txtRevenue = By.XPath("//label[text()='Revenue FY-1 (MM)']/ancestor::div[@part='input-text']/div");
        By txtEBITDA = By.XPath("//label[text()='EBITDA FY-1 (MM)']/ancestor::div[@part='input-text']/div");
        By txtCapex = By.XPath("//label[text()='Capex FY-1 (MM)']/ancestor::div[@part='input-text']/div");
        By valCurrency = By.XPath("//button[@name='CurrencyIsoCode']");
        By valCurrencyNone = By.XPath("//lightning-base-combobox-item/span/span[@title='--None--']");
        By btnProjections = By.XPath("//input[@name='Projections_Last_Updated__c']");
        By imgProjCalendar = By.XPath("//label[text()='Projections are as of: ']/ancestor::div[1]/lightning-input-field/lightning-input/lightning-datepicker/div/div/lightning-calendar");
        By btnLTMProjections = By.XPath("//input[@name='LTM_Projections_Last_Updated__c']");
        By imgLTMProjCalendar = By.XPath("//label[text()='LTM Projections are as of: ']/ancestor::div[1]/lightning-input-field/lightning-input/lightning-datepicker/div/div/lightning-calendar");
        By txtRevFYM1 = By.XPath("//input[@name='Revenue_FY_minus1_MM__c']");
        By txtRevFY = By.XPath("//input[@name='Revenue_FY_MM__c']");
        By txtRevLTM = By.XPath("//input[@name='Revenue_LTM_MM__c']");
        By txtRevFYA1 = By.XPath("//input[@name='Revenue_FY_1_MM__c']");
        By txtRevFYA2 = By.XPath("//input[@name='Revenue_FY_2_MM__c']");
        By txtRevFYA3 = By.XPath("//input[@name='Revenue_FY_3_MM__c']");
        By txtRevFYA4 = By.XPath("//input[@name='Revenue_FY_4_MM__c']");
        By txtRevFYA5 = By.XPath("//input[@name='Revenue_FY_5_MM__c']");
        By txtEBITDAFYM1 = By.XPath("//input[@name='EBITDA_FY_minus1_MM__c']");
        By txtEBITDAFY = By.XPath("//input[@name='EBITDA_FY_MM__c']");
        By txtEBITDALTM = By.XPath("//input[@name='EBITDA_LTM_MM__c']");
        By txtEBITDAFYA1 = By.XPath("//input[@name='EBITDA_FY_1_MM__c']");
        By txtEBITDAFYA2 = By.XPath("//input[@name='EBITDA_FY_2_MM__c']");
        By txtEBITDAFYA3 = By.XPath("//input[@name='EBITDA_FY_3_MM__c']");
        By txtEBITDAFYA4 = By.XPath("//input[@name='EBITDA_FY_4_MM__c']");
        By txtEBITDAFYA5 = By.XPath("//input[@name='EBITDA_FY_5_MM__c']");
        By txtCapexFYM1 = By.XPath("//input[@name='Capex_FY_minus1_MM__c']");
        By txtCapexLTM = By.XPath("//input[@name='Capex_LTM_MM__c']");
        By txtCapexFY = By.XPath("//input[@name='Capex_FY_MM__c']");
        By txtCapexFYA1 = By.XPath("//input[@name='Capex_FY_1_MM__c']");
        By txtCapexFYA2 = By.XPath("//input[@name='Capex_FY_2_MM__c']");
        By txtCapexFYA3 = By.XPath("//input[@name='Capex_FY_3_MM__c']");
        By txtCapexFYA4 = By.XPath("//input[@name='Capex_FY_4_MM__c']");
        By txtCapexFYA5 = By.XPath("//input[@name='Capex_FY_5_MM__c']");
        By btnSaveFinancials = By.XPath("//c-engagement-fr-summary-fin-proj/div/lightning-button/button[text()='Save']");
        By msgSaveFinancials = By.XPath("//span[text()='Record saved']");
        By msgCurrencyFinancials = By.XPath("//div[text()='Complete this field.']");
        By msgAssetSold = By.XPath("//label[text()='Asset Sold']/ancestor::lightning-primitive-input-simple/div[2]");
        By btnSaveAddDistressedL = By.XPath("//form/slot/slot/div[2]/lightning-button[2]/button");
        By btnCancel = By.XPath("//button[text()='Cancel']");
        By btnCancelKeyCredL = By.XPath("//div[2]/button[text()='Cancel']");
        By tabHLFinancingL = By.XPath("//li/a[text()='HL Financing']");
        By tabPreTransL = By.XPath("//li/a[text()='Pre-Transaction Info']");
        By tabPostTransL = By.XPath("//li/a[text()='Post-Transaction Info']");
        By txtAssetSoldL = By.XPath("//input[@name='Name']");
        By txtDateOfSoldL = By.XPath("//input[@name='Date_of_Sale__c']");
        By txtMinOverbidL = By.XPath("//input[@name='Minimum_Overbid__c']");
        By txtIncreOverBidL = By.XPath("//input[@name='Incremental_Overbid__c']");
        By txtBreakUPFeeL = By.XPath("//input[@name='Break_Up_Fee__c']");
        By rowAddDistressedL = By.XPath("//table[@class='slds-table slds-table_bordered slds-table_fixed-layout slds-table_resizable-cols']/tbody/tr");
        By rowAddHLFinL = By.XPath("//c-engagement-fr-summary-hl-financing/div[1]/div/div/div/table/tbody/tr");
        By btnEditDistressed = By.XPath("//button[@title='Edit']");
        By btnEditDebtL = By.XPath("//c-engagement-fr-summary-pre-tran-info/div[1]/div[3]/div/div/table/tbody/tr[1]/td[12]/span/div/lightning-button-icon/button");
        By btnEdit2ndDebtL = By.XPath("//c-engagement-fr-summary-pre-tran-info/div[1]/div[3]/div/div/table/tbody/tr[2]/td[12]/span/div/lightning-button-icon/button");
        By btnEditDebtLPost = By.XPath("//c-engagement-fr-summary-post-tran-info/div[1]/div[3]/div/div/table/tbody/tr[1]/td[12]/span/div/lightning-button-icon/button");
        By btnEdit2ndDebtLPost = By.XPath("//c-engagement-fr-summary-post-tran-info/div[1]/div[3]/div/div/table/tbody/tr[2]/td[12]/span/div/lightning-button-icon/button");

        By btnKeyCredL = By.XPath("//button[text()='New Key Creditor']");

        By valAssetSold = By.XPath("//table[@class='slds-table slds-table_bordered slds-table_fixed-layout slds-table_resizable-cols']/tbody/tr/th/div/div");
        By btnDeleteDistressed = By.XPath("//button[@title='Delete']");
        By btnDeleteBoard = By.XPath("//tr[2]/td[3]/span/div/lightning-button-icon/button[@title='Delete']");
        By btnDeleteDebtL = By.XPath("//c-engagement-fr-summary-pre-tran-info/div[1]/div[3]/div/div/table/tbody/tr[1]/td[13]/span/div/lightning-button-icon/button");
        By lnkEquityHolder = By.XPath("//c-engagement-fr-summary-pre-tran-info/div[1]/div/div/div/table/tbody/tr/th/div/a");
        By lnkEquityHolderPost = By.XPath("//c-engagement-fr-summary-post-tran-info/div[1]/div/div/div/table/tbody/tr/th/div/a");
        By btnDeleteDebtLPost = By.XPath("//c-engagement-fr-summary-post-tran-info/div[1]/div[3]/div/div/table/tbody/tr[1]/td[13]/span/div/lightning-button-icon/button");
        By btnDeleteRole = By.XPath("//tr[1]/td[2]/span/div/lightning-button-icon/button[@title='Delete']");
        By tabEngSummary = By.XPath("//ul[2]/li[3]/a/span[2]");
        By tabHLPostTransL = By.XPath("//li/a[text()='HL Post-Transaction Opportunities']");


        By msgClientSubL = By.XPath("//label[text()='Client/Subject']/ancestor::lightning-grouped-combobox/div[2]");
        By msgContactsL = By.XPath("//label[text()='Contact (External)']/ancestor::lightning-grouped-combobox/div[2]");
        By msgSecurityTypeL = By.XPath("//label[text()='Security Type']/ancestor::div[1]/div[2]");
        By btnOK = By.XPath("//button[text()='OK']");
        By btnSaveAddHL = By.XPath("//div[2]/lightning-button[2]/button");
        By btnSaveKeyCredL = By.XPath("//div[3]/lightning-button[2]/button");
        By btnSaveAndKeyCred = By.XPath("//button[text()='Save and add Key Creditor']");
        By valPerOwnerL = By.XPath("//table/tbody/tr/td/div/lightning-formatted-number");
        By msgFinTypeL = By.XPath("//label[text()='Financing Type']/ancestor::div[1]/div[2]");
        By msgSecTypeL = By.XPath("//label[text()='Security Type']/ancestor::div[1]/div[2]");
        By txtTotalFinAmt = By.XPath("//input[@name='Total_Financing_Amount__c']");
        By btnSaveHLFin = By.XPath("//c-engagement-fr-summary-hl-financing/div/lightning-button/button[text()='Save']");
        By msgSave = By.XPath("//span[text()='Record saved']");
        By txtFinDesc = By.XPath("//textarea[@name='Financing_Description__c']");
        By txtContactL = By.XPath("//input[@placeholder=\"Search Contacts...\"]");
        By txtCompaniesL = By.XPath("//input[@placeholder=\"Search Companies...\"]");
        By comboContactL = By.XPath("//lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item/span[2]/span/span");
        By txtLoanAmountL = By.XPath("//input[@name='Loan_Amount__c']");

        By btnAddStaffRoleL = By.XPath("//button[text()='Add Staff Role']");
        By btnAddKeyExtExternalL = By.XPath("//button[text()='Add Key External Contact']");
        By btnPostTransOppReport = By.XPath("//button[text()='Post-Transaction Opportunity Report']");
        By btnSubmitEngSummary = By.XPath("//button[text()='Submit Engagement Summary']");
        By btnSendBTPEmail = By.XPath("//button[text()='Send BTP Email']");
        By lblSendEmailL = By.XPath("//h2[@class='mainTitle']");
        By lblReport = By.XPath("//div[@class='brandingText']");
        By lblContactL = By.XPath("//label[text()='Contact (Internal)']");
        By lblContactExtL = By.XPath("//label[text()='Contact (External)']");
        By lblRole = By.XPath("//label[text()='Role']");
        By msgContactL = By.XPath("//label[text()='Contact (Internal)']/ancestor::lightning-grouped-combobox/div[2]");
        By msgContactExtL = By.XPath("//label[text()='Contact (External)']/ancestor::lightning-grouped-combobox/div[2]");

        By msgRoleL = By.XPath("//label[text()='Role']/ancestor::lightning-combobox/div/div[2]");

        By txtContactHLPostL = By.XPath("//input[@placeholder='Search Contacts...']");
        By lnkContactHLPostL = By.XPath("//div[1]/div/lightning-base-combobox/div/div/div[2]/ul/li");
        By btnRoleL = By.XPath("//button[contains(@aria-label,'Role')]");
        By valRoleL = By.XPath("//lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[2]/span[2]/span");
        By rowStaffRoleL = By.XPath("//lightning-record-edit-form-edit/form/slot/slot/div/div[3]/div/div/table/tbody/tr");
        By rowKeyContactL = By.XPath("//lightning-record-edit-form-edit/form/slot/slot/div/div[4]/div/div/table/tbody/tr");
        By rowKeyExtL = By.XPath("//lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/div/div[4]/div/div/table/tbody/tr");
        By valAddedRoleL = By.XPath("//table/tbody/tr/td[1]/div");

        By valPostTxnStatus = By.XPath("//label[text()='Post Transaction Status']/ancestor::div[@class='slds-col slds-size_1-of-2']/lightning-output-field/div/lightning-formatted-text");
        By valClientDesc = By.XPath("//label[text()='Client Description']/ancestor::div[@class='slds-col slds-size_1-of-2']/lightning-output-field/div/lightning-formatted-text");
        By valCompDesc = By.XPath("//label[text()='Company Description']/ancestor::div[@class='slds-col slds-size_1-of-2']/lightning-output-field/div/lightning-formatted-text");
        By valReTxnDesc = By.XPath("//label[text()='Restructuring Transaction Description']/ancestor::div[@class='slds-col slds-size_1-of-2']/lightning-output-field/div/lightning-formatted-text");
        By msgReqInfo = By.XPath("//div[text()='*Please fill out the required information in the Engagement and Business Information Section of the Engagement']");
        By comboTxnTypes = By.CssSelector("select[name*='j_id41']>option");
        By lblClientDesc = By.CssSelector("div[id*='id37'] > div[class='pbBody'] > table > tbody > tr:nth-child(1) > td:nth-child(3) > label");
        By lblPostTxnStatus = By.CssSelector("div[id*='id37'] > div[class='pbBody'] > table > tbody > tr:nth-child(2) > td:nth-child(1) > label");
        By comboPostTxnStatus = By.CssSelector("select[name*='j_id37:j_id49']>option");
        By lblCompDesc = By.CssSelector("div[id*='id37'] > div[class='pbBody'] > table > tbody > tr:nth-child(3) > td:nth-child(1) > label");
        By lblTxnDesc = By.CssSelector("div[id*='id37'] > div[class='pbBody'] > table > tbody > tr:nth-child(3) > td:nth-child(2) > label");
        By lblMessage = By.CssSelector("div[id*='id37'] > div[class='pbBody'] > table > tbody > tr:nth-child(6) > td > label");
        By tabFinancials = By.CssSelector("td[id*='FinancialsProjections_lbl']");
        By headerFYFinancials = By.CssSelector("table[id*='j_id60:j_id61']>thead>tr[class='headerRow']>th[class='headerRow right']");
        By lblCurrenyFinancials = By.CssSelector("div[id*='j_id60'] > div.pbBody > table > tbody > tr:nth-child(1) > td:nth-child(1) > label");
        By comboCurrencyTypes = By.CssSelector("select[name*='j_id60:j_id89']>option");
        By lblProjections = By.CssSelector("div[id*='j_id60'] > div.pbBody > table > tbody > tr:nth-child(2) > td:nth-child(1) > label");
        By lblLTMProjections = By.CssSelector("div[id*='j_id60'] > div.pbBody > table > tbody > tr:nth-child(3) > td:nth-child(1) > label");
        By rowFinancials = By.CssSelector("tbody[id*='j_id28:j_id60:j_id61:tb']>tr>td:nth-child(2)");
        By tabDistressedMA = By.CssSelector("td[id*='tabDistressedMAInformation_lbl']");

        By headerDistressed = By.CssSelector("table[id*=':salesTransactions']>thead>tr>th>div>label");
        By tabHLFinancing = By.CssSelector("td[id*='tabHLFinancing_lbl']");
        By headerHLFinancing = By.CssSelector("table[id*='financing']>thead>tr>th>div>label");
        By lblTotalFinancing = By.CssSelector("div[id*='j_id28:j_id133'] > div.pbBody > table>tbody>tr:nth-child(1)>td:nth-child(1)>label");
        By lblFinDesc = By.CssSelector("div[id*='j_id28:j_id133'] > div.pbBody > table>tbody>tr:nth-child(2)>td:nth-child(1)>label");
        By btnAddHLFin = By.CssSelector("input[value='Add HL Financing']");
        By titleInsertNewHL = By.CssSelector("span[class*='ui-dialog-title']");
        By lblFinTypes = By.CssSelector("div[id*='j_id492']>div>table>tbody>tr:nth-child(1)>td>label");
        By comboFinTypes = By.CssSelector("div[id*='j_id492']>div>table>tbody>tr:nth-child(1)>td:nth-child(2)>select>option");
        By lblOther = By.CssSelector("div[id*='j_id492']>div>table>tbody>tr:nth-child(2)>td>label");
        By lblSecurityType = By.CssSelector("div[id*='j_id492']>div>table>tbody>tr:nth-child(3)>td>label");
        By comboSecTypes = By.CssSelector("div[id*='j_id492']>div>table>tbody>tr:nth-child(3)>td:nth-child(2)>select>option");
        By lblFinAmount = By.CssSelector("div[id*='j_id492']>div>table>tbody>tr:nth-child(4)>td>label");
        By btnClose = By.CssSelector("button[title='Close']");
        By tabPreTrans = By.CssSelector("td[id*='tabPreTransactionInformation_lbl']");
        By lblPreTransSec = By.CssSelector("div[id*='id162'] > div.pbBody > table > tbody > tr:nth-child(1) > td:nth-child(1) > label");
        By lblEquityHolder = By.CssSelector("table[id*=':preTransactionEquityHolders']>thead>tr>th>div[id*='j_id178header:sortDiv']");
        By lblOwnership = By.CssSelector("table[id *= ':preTransactionEquityHolders']>thead>tr>th>div[id *= 'j_id182header:sortDiv']");
        By lblPreTransMembersSec = By.CssSelector("div[id*='id162'] > div.pbBody > table > tbody > tr:nth-child(1) > td:nth-child(2) > label");
        By lblBoardMember = By.CssSelector("table[id*=':preTransactionBoardMembers']>thead>tr>th>div[id*='j_id191header:sortDiv']");
        By lblCompany = By.CssSelector("table[id*=':preTransactionBoardMembers']>thead>tr>th>div[id*='j_id195header:sortDiv']");
        By lblRelationship = By.CssSelector("table[id*=':preTransactionBoardMembers']>thead>tr>th>div[id*='j_id199header:sortDiv']");
        By btnAddDebtStructure = By.CssSelector("input[id='newPreTransactionDebtStructure']");
        By lblSecurityTypes = By.CssSelector("div[id*='id1:j_id28'] > div.pbBody > table > tbody > tr:nth-child(1) > td:nth-child(1) > label");
        By lblCurrency = By.CssSelector("div[id*='id1:j_id28'] > div.pbBody > table > tbody > tr:nth-child(12) > td:nth-child(1) > label");
        By comboSecType = By.CssSelector("select[id*='j_id0:j_id1:j_id28:j_id32']>option");
        By comboDebtCurrency = By.CssSelector("select[id*='j_id0:j_id1:j_id28:j_id72']>option");
        By comboDebtCurrencyL = By.XPath("//button[@name='CurrencyIsoCode']");

        By valAvailableL = By.XPath("//span[@title='M&A - Buyside']");
        By btnChosenL = By.XPath("//button[@title='Move to Chosen']");
        By btnAvailableL = By.XPath("//button[@title='Move to Available']");
        By btnSaveHLPost = By.XPath("//c-engagement-fr-summary-post-tran-opp/div/lightning-button/button");
        By valChosenL = By.XPath("//span[text()='Chosen']/ancestor::div[1]/div[1]/ul/li/div/span");
        By txtOppNoteL = By.XPath("//textarea[@name='Post_Transaction_Opportunity_Notes__c']");
        By msgSuccessHLPost = By.XPath("//div/span[text()='Record saved']");

        By btnWinClose = By.CssSelector("button[title='Close']");
        By lblPreTransDebtSec = By.CssSelector("div[id*='id162'] > div.pbBody > table > tbody > tr:nth-child(3) > td:nth-child(1) > label");
        By headerPreTransDebt = By.CssSelector("table[id*=':preTransactionDebtStructures']>thead>tr>th>div");
        By lblPreReorgTotalDebt = By.CssSelector(" div[id*='j_id28:j_id162']> div.pbBody > table > tbody > tr:nth-child(5) > td > table > tbody > tr > td:nth-child(3) > label");
        By lblPreReorgConstDebt = By.CssSelector(" div[id*='j_id28:j_id162']> div.pbBody > table > tbody > tr:nth-child(5) > td > table > tbody > tr > td:nth-child(1) > label");
        By tabPostTrans = By.CssSelector("td[id*='tabPostTransactionInformation_lbl']");
        By lblPostTransSec = By.CssSelector("div[id*='postTransactionInformation'] > div.pbBody > table > tbody > tr:nth-child(1) > td:nth-child(1) > label");
        By headerEquityHolders = By.CssSelector("table[id*='postTransactionEquityHolders']>thead>tr>th");
        By lblPostTransMembersSec = By.CssSelector("div[id*='postTransactionInformation'] > div.pbBody > table > tbody > tr:nth-child(1) > td:nth-child(2) > label");
        By headerBoardMembers = By.CssSelector("table[id*='postTransactionBoardMembers']>thead>tr>th");
        By btnPostTransAddDebt = By.CssSelector("input[id='newPostTransactionDebtStructure']");
        By lblPostTransDebtSec = By.CssSelector("div[id*='j_id28:postTransactionInformation'] > div.pbBody > table > tbody > tr:nth-child(3) > td:nth-child(1) ");
        By headerPostTransDebt = By.CssSelector("table[id*=':postTransactionInformation:j']>thead>tr>th>div");
        By lblPostTotalDebt = By.CssSelector("div[id*='postTransactionInformation'] > div[class='pbBody'] > table > tbody > tr:nth-child(5) > td > table > tbody > tr > td:nth-child(1) > label");
        By lblNetDebt = By.CssSelector("div[id*='postTransactionInformation'] > div[class='pbBody'] > table > tbody > tr:nth-child(5) > td > table > tbody > tr > td:nth-child(3) > label");
        By lblClosingStock = By.CssSelector("div[id*='postTransactionInformation'] > div[class='pbBody'] > table > tbody > tr:nth-child(5) > td > table > tbody > tr > td:nth-child(5) > label");
        By tabHLPostTrans = By.CssSelector("td[id*='tabHLPostTransactionOpportunities_lbl']");
        By comboAvail = By.CssSelector("select[title*='Opportunities - Available']>optgroup>option");
        By btnAddStaffRole = By.CssSelector("input[value='Add Staff Role']");
        By lblClassification = By.CssSelector("span[id*='staffRoleClassification']>label");
        By comboClassification = By.CssSelector("select[name*='mainForm:dummyStaffRole']>option");
        By lblStaffRolesSec = By.CssSelector("div[id*='j_id336'] > div.pbBody > table > tbody > tr:nth-child(1) > td:nth-child(2) > label");
        By headerStaffRoles = By.CssSelector("tbody > tr:nth-child(1) > td:nth-child(2) > span>table>thead>tr>th>div");
        By lblExtContactSec = By.CssSelector("div[id*='j_id336'] > div.pbBody > table > tbody > tr:nth-child(1) > td:nth-child(3) > label");
        By headerExtContact = By.CssSelector("tbody > tr:nth-child(1) > td:nth-child(3) > span>table>thead>tr>th>div");
        By lblNotesSec = By.CssSelector("div[id*='j_id336'] > div.pbBody > table > tbody > tr:nth-child(2) > td > label");
        By btnAddDistressed = By.CssSelector("input[value='Add Distressed M&A Information']");
        By txtAssetSale = By.CssSelector("input[name*='saleTransactionName']");
        By lnkDateOfSale = By.CssSelector("div[id*='j_id34'] > div > table > tbody > tr:nth-child(2) > td > span > span > a");
        By txtMinOverbid = By.CssSelector("input[name*='j_id0:j_id5:pbMain:j_id34:j_id36']");
        By txtIncOverbid = By.CssSelector("input[name*='j_id0:j_id5:pbMain:j_id34:j_id37']");
        By txtBreakUpFee = By.CssSelector("input[name*='j_id0:j_id5:pbMain:j_id34:j_id38']");
        By txtDeposit = By.CssSelector("input[name*='j_id0:j_id5:pbMain:j_id34:j_id39']");
        By txtCashComp = By.CssSelector("input[name*='j_id0:j_id5:pbMain:j_id34:j_id41']");
        By txtStockComp = By.CssSelector("input[name*='j_id0:j_id5:pbMain:j_id34:j_id42']");
        By txtLiability = By.CssSelector("input[name*='j_id0:j_id5:pbMain:j_id34:j_id43']");
        By txtClaimConv = By.CssSelector("input[name*='j_id0:j_id5:pbMain:j_id34:j_id44']");
        By btnSave = By.CssSelector("input[name*='2:bottom:j_id33']");
        By lnkEdit = By.CssSelector("td[id*='salesTransactions:0:j_id104']>a");
        By valMARec = By.CssSelector("tbody[id*='salesTransactions:tb']>tr>td>span");
        By valMinOverbid = By.CssSelector("tbody[id*='salesTransactions:tb']>tr>td:nth-child(5)>span");
        By valCashComp = By.CssSelector("tbody[id*='salesTransactions:tb']>tr>td:nth-child(9)>span");
        By lnkDel = By.CssSelector("td[id*='salesTransactions:0:j_id107']>a");
        By txtMessage = By.CssSelector("div[class='pbBody']>span[id*='j_id101:panSalesTransactions']>label");
        By comboFinancingType = By.CssSelector("select[name*='j_id492:addFinancingType']");
        By txtOther = By.CssSelector("textarea[name*='addFinancingOther']");
        By comboSecurityType = By.CssSelector("div[id*='j_id492']>div>table>tbody>tr:nth-child(3)>td:nth-child(2)>select");
        By txtFinAmmount = By.CssSelector("input[name*='j_id506']");
        By btnHLFinSave = By.CssSelector("input[name*='j_id508']");
        By valHLFinRec = By.CssSelector("tbody[id*='j_id28:j_id133:financing:tb']>tr>td>span");
        By lnkHLFinEdit = By.CssSelector("tbody[id*='j_id133:financing:tb']>tr>td>a[id*='editFinancing']");
        By valFinancingType = By.CssSelector("tbody[id*='financing:tb']>tr>td:nth-child(3)>span");
        By lnkHLFinDel = By.CssSelector("td[id*='financing:0:j_id142']>a");
        By msgHLFin = By.CssSelector("div[class='pbBody']>span[id*='j_id133:panFinancing']>label");
        By comboFinTypeEdit = By.CssSelector("select[name*=':editFinancingType']");
        By btnSaveFinEdit = By.CssSelector("input[name*='3:j_id34']");
        By lnkEditRevenue = By.CssSelector("a[id='editRevenueFinancials']");
        By txtFY1 = By.CssSelector("input[name*='j_id369']");
        By txtFY = By.CssSelector("input[name*='j_id374']");
        By txtLTM = By.CssSelector("input[name*='j_id379']");
        By txtFYPlus1 = By.CssSelector("input[name*='j_id384']");
        By txtFYPlus2 = By.CssSelector("input[name*='j_id389']");
        By txtFYPlus3 = By.CssSelector("input[name*='j_id394']");
        By txtFYPlus4 = By.CssSelector("input[name*='j_id399']");
        By txtFYPlus5 = By.CssSelector("input[name*='j_id404']");
        By btnSaveRevenue = By.CssSelector("input[name*='j_id406']");
        By rowValRevenue = By.CssSelector("tbody[id*='j_id61:tb']>tr:nth-child(1)>td[class='dataCell  right']");
        By lnkEditEBITDA = By.CssSelector("a[id = 'editEBITDAFinancials']");
        By txtFY1EBITDA = By.CssSelector("input[name*='j_id453']");
        By txtFYEBITDA = By.CssSelector("input[name*='j_id458']");
        By txtLTMEBITDA = By.CssSelector("input[name*='j_id463']");
        By txtFYPlus1EBITDA = By.CssSelector("input[name*='j_id468']");
        By txtFYPlus2EBITDA = By.CssSelector("input[name*='j_id473']");
        By txtFYPlus3EBITDA = By.CssSelector("input[name*='j_id478']");
        By txtFYPlus4EBITDA = By.CssSelector("input[name*='j_id483']");
        By txtFYPlus5EBITDA = By.CssSelector("input[name*='j_id488']");
        By btnSaveEBITDA = By.CssSelector("input[name*='j_id490']");
        By rowValEBITDA = By.CssSelector("tbody[id*='j_id61:tb']>tr:nth-child(2)>td[class='dataCell  right']");
        By lnkEditCapex = By.CssSelector("a[id = 'editCapexFinancials']");
        By txtFY1Capex = By.CssSelector("input[name*='j_id411']");
        By txtFYCapex = By.CssSelector("input[name*='j_id416']");
        By txtLTMCapex = By.CssSelector("input[name*='j_id421']");
        By txtFYPlus1Capex = By.CssSelector("input[name*='j_id426']");
        By txtFYPlus2Capex = By.CssSelector("input[name*='j_id431']");
        By txtFYPlus3Capex = By.CssSelector("input[name*='j_id436']");
        By txtFYPlus4Capex = By.CssSelector("input[name*='j_id441']");
        By txtFYPlus5Capex = By.CssSelector("input[name*='j_id446']");
        By btnSaveCapex = By.CssSelector("input[name*='j_id448']");
        By rowValCapex = By.CssSelector("tbody[id*='j_id61:tb']>tr:nth-child(3)>td[class='dataCell  right']");
        By btnAddEquityHolder = By.CssSelector("input[id='newPreTransactionEquityHolder']");
        By txtEquitySearch = By.CssSelector("input[name*='txtSearch']");
        By btnGo = By.CssSelector("input[value='Go']");
        By checkRow = By.CssSelector("input[name*='tblResults:0:j_id50']");
        By btnAddSelected = By.CssSelector("input[value='Add Selected']");
        By msgSuccess = By.CssSelector("div[id*='4:j_id16']");

        By btnEquityClose = By.CssSelector("span[class*='closethick']");
        By valEquityHolder = By.CssSelector("tbody[id*='preTransactionEquityHolders:tb']>tr>td:nth-child(4)");
        By lnkEquityEdit = By.CssSelector("a[id*='editPre']");
        By txtOwnership = By.CssSelector("input[name*='ownershipPercent']");
        By valOwnership = By.CssSelector("td[id*='preTransactionEquityHolders:0:j_id182']");
        By btnEquitySave = By.CssSelector("input[name*='pbMainEdit:j_id41:j_id42']");
        By lnkEquityCopy = By.CssSelector("a[id*='j_id174']");
        By rowEquityHoldersPost = By.CssSelector("tbody[id*='postTransactionEquityHolders:tb']>tr>td");
        By lnkEquityDel = By.CssSelector("td[id*='j_id175']>a");
        By msgEquityDelete = By.CssSelector("span[id*='PreTransactionEquityHolders']>label");
        By btnAddBoardMember = By.CssSelector("input[id='newPreTransactionBoardMember']");
        By checkRowBoard = By.CssSelector("input[name*='tblResults:0:j_id42']");
        By msgSuccessBoard = By.CssSelector("div[id*='10:j_id12']");
        By valBoardMember = By.CssSelector("tbody[id*='preTransactionBoardMembers:tb']>tr>td:nth-child(3)");
        By lnkBoardCopy = By.CssSelector("a[id*='j_id187']");
        By rowBoardMemberPost = By.CssSelector("tbody[id*='postTransactionBoardMembers:tb']>tr>td");
        By lnkBoardDel = By.CssSelector("td[id*='j_id188']>a");
        By msgBoardDelete = By.CssSelector("span[id*='panPreTransactionBoardMembers']>label");
        By msgSaveRec = By.CssSelector("div[id*='1:j_id28']>div[class='pbBody']>table>tbody>tr:nth-child(15)>td>label");
        By comboSecurityTypeDebt = By.CssSelector("select[id*='id32']");
        By txtMaturityDate = By.CssSelector("div[id*='1:j_id28']>div[class='pbBody']>table>tbody>tr:nth-child(2)>td:nth-child(2)>span>span>a");
        By txtInterest = By.CssSelector("input[id*='id40']");
        By txtOID = By.CssSelector("input[id*='id44']");
        By txtAmortization = By.CssSelector("input[id*='id48']");
        By txtCallProvisions = By.CssSelector("input[id*='id52']");
        By txtMandatory = By.CssSelector("input[id*='id56']");
        By txtCovenants = By.CssSelector("textarea[name*='id60']");
        By txtFees = By.CssSelector("input[id*='id64']");
        By txtFacility = By.CssSelector("input[id*='id68']");
        By btnSaveDebt = By.CssSelector("input[name*='id74']");
        By msgSuccessDebt = By.CssSelector("td[class='messageCell']>div");
        By rowDebtStrPre = By.CssSelector("tbody[id*='preTransactionDebtStructures:tb']>tr>td>span");
        By valDebtSecurityType = By.CssSelector("td[id*='preTransactionDebtStructures:0:j_id212']");
        By lnkDebtDel = By.CssSelector("td[id*='j_id209']>a");
        By lnkDebtDelL = By.XPath("//flexipage-tab2[5]//td[13]//lightning-button-icon");
        By lnkPostDebtDelL = By.XPath("//flexipage-tab2[6]//td[13]//lightning-button-icon/button");
        By msgDebtDelete = By.CssSelector("span[id*='panPreTransactionDebtStructures']>label");
        By btnNewLender = By.CssSelector("input[value='New Key Creditor']");
        By txtLenderAmt = By.CssSelector("input[name*=':0:j_id54']");
        By rowLender = By.CssSelector("tbody[id*='id81:tb']>tr>td>span");
        By rowLenderPostDel = By.CssSelector("tbody[id*='id81:tb']>tr");
        By lnkLenderEdit = By.CssSelector("a[id*='editLender']");
        By txtLoanAmt = By.CssSelector("input[name*='loanAmount']");
        By btnSaveLender = By.CssSelector("input[name*='41:j_id42']");
        By valLoanAmt = By.CssSelector("tbody[id*='id81:tb']>tr>td:nth-child(3)>span");
        By lnkLenderDelete = By.CssSelector("a[id*='deleteLender']");
        By btnPostAddEquity = By.CssSelector("input[id='newPostTransactionEquityHolder']");
        By valPostEquityHolder = By.CssSelector("tbody[id*='postTransactionEquityHolders:tb']>tr>td:nth-child(3)");
        By lnkPostEquityEdit = By.CssSelector("a[id*='editPost']");
        By valPostOwnership = By.CssSelector("td[id*='postTransactionEquityHolders:0:j_id266']");
        By lnkPostEquityDel = By.CssSelector("td[id*='j_id259']>a");
        By msgPostEquityDelete = By.CssSelector("span[id*='PostTransactionEquityHolders']>label");
        By btnPostAddBoardMember = By.CssSelector("input[id='newPostTransactionBoardMember']");
        By valPostBoardMember = By.CssSelector("tbody[id*='postTransactionBoardMembers:tb']>tr>td:nth-child(2)");
        By lnkPostBoardDel = By.CssSelector("td[id*='j_id270']>a");
        By msgPostBoardDelete = By.CssSelector("span[id*='panPostTransactionBoardMembers']>label");
        By btnPostAddDebtStructure = By.CssSelector("input[id='newPostTransactionDebtStructure']");
        By rowPostDebtStrPre = By.CssSelector("tbody[id*='postTransactionInformation:j_id288:tb']>tr>td>span");
        By valPostDebtSecurityType = By.CssSelector("td[id*='j_id288:0:j_id300']");
        By lnkPostDebtDel = By.CssSelector("td[id*='j_id292']>a");
        By msgPostDebtDelete = By.CssSelector("span[id*='panPostTransactionDebtStructures']>label");
        By checkStaffRole = By.CssSelector("input[name*='tblResults:0:j_id42']");
        By valStaffRoles = By.CssSelector("tbody[id*='postTransactionStaffRoles:tb']>tr>td>span");
        By lnkStaffRoleDel = By.CssSelector("tbody[id*='postTransactionStaffRoles:tb']>tr>td>a");
        By msgStaffRoleDel = By.CssSelector("tbody>tr>td:nth-child(2)>span[id*='panPostTransactionStaffRoles']>label");
        By txtClassification = By.CssSelector("select[name*='mainForm:dummyStaffRole']");
        By msgSuccessStaff = By.CssSelector("div[id*='10:j_id12']");
        By btnReturnToEngagement = By.CssSelector("input[value='Return to Engagement']");

        By lblAddDebtTxtFields = By.XPath("//lightning-input-field/lightning-input/lightning-primitive-input-simple/div/label");
        By txtPreReorgConstituentDebt = By.XPath("//input[@name='Pre_Reorgnization_Constituent_Debt__c']");
        By txtPostRestrDebt = By.XPath("//input[@name='Post_Reorg_Total_Debt__c']");

        By txtPreReorgTotalDebt = By.XPath("//input[@name='Pre_Reorganized_Total_Debt__c']");
        By txtPostReorgTotalDebt = By.XPath("//input[@name='Post_Reorg_Net_Debt__c']");
        By txtPostClosingStocking = By.XPath("//input[@name='Post_Reorg_Stock_Price__c']");

        By btnSavePreTransInfo = By.XPath("//div[2]/lightning-button/button");
        By msgSuccessPreReorg = By.XPath("//div[2]/span[text()='Record saved']");
        By lnkBoardMember = By.XPath("//div[2]/div/div/table/tbody/tr/th/div/a");
        By lnkBoardCompany = By.XPath("//div[2]/div/div/table/tbody/tr/td/div/a");
        By lnkKeyCred = By.XPath("//div[3]/div/div/table/tbody/tr/td/div/a");
        By tabEngDetailsL = By.XPath("//ul[@class='tabBarItems slds-tabs--default__nav']/li[2]/a/span[2]");
        By tabClosingInfo = By.XPath("//a[text()='Closing Info']");
        By valSendBTPEmail = By.XPath("//span[text()='Sent BTP Email (User)']/ancestor::div[2]/div[2]/span/slot/lightning-formatted-text");
        By tabClientSubL = By.XPath("//a[text()='Client/Subject & Referral']");
        By rowAddedEquityInAdd = By.XPath("//lst-formatted-text/span[@title='Equity Holder']/ancestor::td/ancestor::tr/th[1]//a");
        By valAddedEquityType = By.XPath("//lst-formatted-text/span[@title='Equity Holder']");
        By valAddedEquityRole = By.XPath("//lst-formatted-text/span[@title='Equity Holder']/ancestor::td/ancestor::tr/td[7]//span");
        By valAddedEquityRolePost = By.XPath("//lst-formatted-text/span[@title='Equity Holder']/ancestor::td/ancestor::tr/td[7]//span[text()='Post-Transaction']");

        By tabEngContactsL = By.XPath("//a[text()='Contacts']");
        By valAddedMemberType = By.XPath("//tr[1]/td[3]/lightning-primitive-cell-factory/span");
        By valAddedMemberRole = By.XPath("//span[@title='Pre-Transaction Board Member']");
        By valAddedMemberRolePost = By.XPath("//span[@title='Post-Transaction Board Member']");
        By valAddedMember = By.XPath("//tr[1]/th/lightning-primitive-cell-factory//span/a[2]");
        By valAddedStaffRole = By.XPath("//tr[1]/td[4]/lightning-primitive-cell-factory/span");

        string dir = @"C:\Users\VKumar0427\source\repos\SF_Automation\TestData\";

        //Get label i.e. Transaction Type 
        public string GetLabelTransactionType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblTransType, 90);
            string txtTransType = driver.FindElement(lblTransType).Text;
            return txtTransType;
        }
        //Compare values of Transaction Type
        public bool VerifyTxnTypeValues()
        {
            IReadOnlyCollection<IWebElement> valTxnTypes = driver.FindElements(comboTxnTypes);
            var actualValue = valTxnTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Change of Control", "Debt for Cash", "Debt for Debt", "Debt for Equity", "Reinstatement", "Sale" };
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

        //Get label i.e. Client Description
        public string GetLabelClientDescription()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblClientDesc, 90);
            string txtClientDesc = driver.FindElement(lblClientDesc).Text;
            return txtClientDesc;
        }

        //Get label i.e. Post Transaction Status

        public string GetLabelPostTxnStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostTxnStatus, 90);
            string txtPostTxnStatus = driver.FindElement(lblPostTxnStatus).Text;
            return txtPostTxnStatus;
        }

        //Compare values of Post Transaction Status
        public bool VerifyPostTxnStatusValues()
        {
            IReadOnlyCollection<IWebElement> valTxnStatus = driver.FindElements(comboPostTxnStatus);
            var actualValue = valTxnStatus.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Reorganized - Public", "Reorganized - Private", "Liquidated", "Acquired", "Other", "N/A" };
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
        //Get label i.e. Company Description  
        public string GetLabelCompanyDescription()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCompDesc, 90);
            string txtCompDesc = driver.FindElement(lblCompDesc).Text;
            return txtCompDesc;
        }

        //Get label i.e. Restructuring Transaction Description
        public string GetLabelTxnDescription()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblTxnDesc, 90);
            string txtCompDesc = driver.FindElement(lblTxnDesc).Text;
            return txtCompDesc;
        }

        //Get message displayed at bottom
        public string GetMessageDisplayed()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblMessage, 90);
            string txtMessage = driver.FindElement(lblMessage).Text;
            return txtMessage;
        }

        //To Click Financials/Projections tab
        public void ClickFinancialsTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabFinancials, 90);
            driver.FindElement(tabFinancials).Click();
        }

        //To Validate FY header row of Financials/Projections tab
        public bool VerifyFYHeaderRow()
        {
            IReadOnlyCollection<IWebElement> valTxnStatus = driver.FindElements(headerFYFinancials);
            var actualValue = valTxnStatus.Select(x => x.Text).ToArray();
            string[] expectedValue = { "FY-1", "FY", "LTM", "FY+1", "FY+2", "FY+3", "FY+4", "FY+5" };
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

        //Get label i.e. Currency Financials are reported in

        public string ValidateLabelCurrenyFin()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCurrenyFinancials, 90);
            string txtCurrencyFin = driver.FindElement(lblCurrenyFinancials).Text;
            return txtCurrencyFin;
        }

        //Compare values of Currency Financials
        public bool VerifyCurrencyFinancialsValues()
        {
            IReadOnlyCollection<IWebElement> valCurTypes = driver.FindElements(comboCurrencyTypes);
            var actualValue = valCurTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Australian Dollar", "Brazilian Real", "British Pound", "Canadian Dollar", "Chinese Yuan", "Czech Koruna", "Danish Krone", "Euro", "Hong Kong Dollar", "Indian Rupee", "Israeli Shekel", "Japanese Yen", "Norwegian Krone", "Saudi Arabian Riyal", "Singapore Dollar", "Swedish Krona", "Swiss Franc", "Taiwan Dollar", "U.S. Dollar", "UAE Dirham", "Vietnam Dong" };
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

        //Get label i.e. Projections are as of

        public string ValidateLabelProjections()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblProjections, 90);
            string txtProjections = driver.FindElement(lblProjections).Text;
            return txtProjections;
        }

        //Get label i.e. LTM Projections are as of

        public string ValidateLabelLTMProjections()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblLTMProjections, 90);
            string txtLTMProjections = driver.FindElement(lblLTMProjections).Text;
            return txtLTMProjections;
        }
        //Compare row values of Financials/Projections
        public bool ValidateFinancialsRows()
        {
            IReadOnlyCollection<IWebElement> valRows = driver.FindElements(rowFinancials);
            var actualValue = valRows.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Revenue", "EBITDA", "Capex" };
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

        //To Click Distressed M&A Information tab        
        public void ClickDistressedMAInfoTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabDistressedMA, 90);
            driver.FindElement(tabDistressedMA).Click();
        }

        //To Validate header row of Distressed M&A Information tab        
        public bool VerifyDistressedHeaderValues()
        {
            IReadOnlyCollection<IWebElement> valHeader = driver.FindElements(headerDistressed);
            var actualValue = valHeader.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Asset Sold", "Date of Sale", "Minimum Overbid (MM)", "Incremental Overbid (MM)", "Break Up Fee (MM)", "Deposit (MM)", "Cash Component (MM)", "Stock Component (MM)", "Liability Assumed (MM)", "Claim Conversion (MM)", "Total Sales Price (MM)" };
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

        //To Click HL Financing tab        
        public void ClickHLFinancingTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabHLFinancing, 90);
            driver.FindElement(tabHLFinancing).Click();
        }
        //To validate HL Financing button
        public string ValidateAddHLFinancing()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddHLFin, 90);
            string btnAddHL = driver.FindElement(btnAddHLFin).Displayed.ToString();
            Console.WriteLine("btnAddHL: " + btnAddHL);
            if (btnAddHL.Equals("True"))
            {
                return "Add HL Financing is displayed";
            }
            else
            {
                return "Add HL Financing is not displayed";
            }
        }

        //To Click Add HL Financing button        
        public string ClickAddHLFinancing()
        {
            driver.FindElement(btnAddHLFin).Click();
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //Validate label i.e. Financing Types  
        public string ValidateLabelFinType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblFinTypes, 90);
            string txtFinTypes = driver.FindElement(lblFinTypes).Text;
            return txtFinTypes;
        }

        //To Validate values of Financing Types combo        
        public bool VerifyFinancingTypesValues()
        {
            IReadOnlyCollection<IWebElement> valFinTypes = driver.FindElements(comboFinTypes);
            var actualValue = valFinTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Acquisition", "Credit Facility", "DIP", "Equity", "Exit Facility", "Mezzanine", "New Equity Financing", "Refinancing", "Revolver", "Rollover", "Term Loan", "Other" };
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

        //Validate label i.e. Other
        public string ValidateLabelOther()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblOther, 90);
            string txtOther = driver.FindElement(lblOther).Text;
            return txtOther;
        }
        //Validate label i.e. Security Type
        public string ValidateLabelSecurityType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblSecurityType, 90);
            string txtSecType = driver.FindElement(lblSecurityType).Text;
            return txtSecType;
        }

        //To Validate values of Security Types combo        
        public bool VerifySecurityTypesValues()
        {
            IReadOnlyCollection<IWebElement> valSecTypes = driver.FindElements(comboSecTypes);
            var actualValue = valSecTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Bank Debt (First Lien) - Revolver", "Bank Debt (First Lien) - Term Loan A", "Bank Debt (First Lien) - Term Loan B", "Bank Debt (First Lien) - Synthetic LC Facility", "Bank Debt (Second Lien)", "Senior Structured Notes", "Capital Leases", "Other Secured Debt", "Mezzanine Debt", "Senior Notes (Unsecured)", "Senior Subordinated Notes (Unsecured)", "Other Unsecured Debt", "Common Equity", "Preferred Equity" };
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

        //Validate label i.e. Security Type
        public string ValidateLabelFinAmount()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblFinAmount, 90);
            string txtFinAmt = driver.FindElement(lblFinAmount).Text;
            driver.FindElement(btnClose).Click();
            return txtFinAmt;
        }

        //To Validate header row of HL Financing tab        
        public bool VerifyHLFinancingHeaderValues()
        {
            IReadOnlyCollection<IWebElement> valHeader = driver.FindElements(headerHLFinancing);
            var actualValue = valHeader.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Financing Type", "Other", "Security Type", "Financing Amount (MM)" };
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
        //Get label i.e. Total Financing Amount

        public string ValidateLabelTotalFinAmount()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblTotalFinancing, 90);
            string txtFinAmt = driver.FindElement(lblTotalFinancing).Text;
            return txtFinAmt;
        }

        //Get label i.e. Financing Description
        public string ValidateLabelFinDesc()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblFinDesc, 90);
            string txtFinDesc = driver.FindElement(lblFinDesc).Text;
            return txtFinDesc;
        }

        //To Click Pre-Transaction Information tab        
        public void ClickPreTransInfoTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabPreTrans, 90);
            driver.FindElement(tabPreTrans).Click();
        }

        //To validate Pre-Transaction Equity Holders section        
        public string ValidatePreTransEquitySection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPreTransSec, 90);
            string sectionName = driver.FindElement(lblPreTransSec).Text;
            return sectionName;
        }

        //To validate Equity Holder Header      
        public string ValidateEquityHolderHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblEquityHolder, 90);
            string headerName = driver.FindElement(lblEquityHolder).Text;
            return headerName;
        }

        //To validate Percent Ownership Header      
        public string ValidateOwnershipHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblOwnership, 90);
            string headerName = driver.FindElement(lblOwnership).Text;
            return headerName;
        }

        //To validate Pre-Transaction Board Members section        
        public string ValidatePreTransMembersSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPreTransMembersSec, 90);
            string sectionName = driver.FindElement(lblPreTransMembersSec).Text;
            return sectionName;
        }

        //To validate Board Member Header
        public string ValidateMemberHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblBoardMember, 90);
            string headerName = driver.FindElement(lblBoardMember).Text;
            return headerName;
        }

        //To validate Company Header
        public string ValidateCompanyHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCompany, 90);
            string headerName = driver.FindElement(lblCompany).Text;
            return headerName;
        }

        //To validate HL Relationship Header
        public string ValidateHLRelationshipHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRelationship, 90);
            string headerName = driver.FindElement(lblRelationship).Text;
            return headerName;
        }

        //To validate Add Debt Structure button        
        public string ValidateAddDebtStructureButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddDebtStructure, 90);
            string btnAddHL = driver.FindElement(btnAddDebtStructure).Displayed.ToString();
            Console.WriteLine("btnAddDebtStructure: " + btnAddDebtStructure);
            if (btnAddHL.Equals("True"))
            {
                return "Add Debt Structure button is displayed";
            }
            else
            {
                return "Add HL Financing button is not displayed";
            }
        }

        //To Click Add Debt Structure button        
        public string ValidateAddDebtTitle()
        {
            driver.FindElement(btnAddDebtStructure).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 80);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //Validate label Security Type in Add New Pre-Transaction Debt Structure window       
        public string ValidateLabelSecurityTypes()
        {
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, lblSecurityTypes, 60);
            driver.FindElement(lblSecurityTypes).Click();
            string txtSecurityTypes = driver.FindElement(lblSecurityTypes).Text;
            return txtSecurityTypes;
        }

        //To Validate values of Security Types combo    
        public bool VerifySecurityTypeValues()
        {
            IReadOnlyCollection<IWebElement> valType = driver.FindElements(comboSecType);
            var actualValue = valType.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Bank Debt (First Lien) - Revolver", "Bank Debt (First Lien) - Term Loan A", "Bank Debt (First Lien) - Term Loan B", "Bank Debt (First Lien) - Synthetic LC Facility", "ABL/ABS Facility - First Lien", "LC Facility", "Bank Debt (Second Lien)", "ABL/ABS Facility - Second Lien", "Convertible Notes (Secured)", "Senior Secured Notes", "Capital Leases", "Other Secured Debt", "Mezzanine Debt", "Senior Notes (Unsecured)", "Senior Subordinated Notes (Unsecured)", "Substantially All Assets", "Convertible Notes (Unsecured)", "Other Unsecured Debt" };
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


        //Validate label Debt Currency in Add New Pre-Transaction Debt Structure window       
        public string ValidateLabelCurrency()
        {
            driver.FindElement(lblCurrency).Click();
            string txtCurrency = driver.FindElement(lblCurrency).Text;
            return txtCurrency;
        }

        //To Validate values of Debt Currency combo    
        public bool VerifyCurrencyValues()
        {
            IReadOnlyCollection<IWebElement> valCurrencies = driver.FindElements(comboDebtCurrency);
            var actualValue = valCurrencies.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Australian Dollar", "Brazilian Real", "British Pound", "Canadian Dollar", "Chinese Yuan", "Czech Koruna", "Danish Krone", "Euro", "Hong Kong Dollar", "Indian Rupee", "Israeli Shekel", "Japanese Yen", "Norwegian Krone", "Saudi Arabian Riyal", "Singapore Dollar", "Swedish Krona", "Swiss Franc", "Taiwan Dollar", "U.S. Dollar", "UAE Dirham", "Vietnam Dong" };
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

        //To Validate values of Debt Currency combo    
        public bool VerifyCurrencyValuesL()
        {
            Thread.Sleep(7000);
            driver.FindElement(comboDebtCurrencyL).Click();
            driver.FindElement(comboDebtCurrencyL).Click();
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valCurrencies = driver.FindElements(valDebtCurrencyL);

            var actualValue = valCurrencies.Select(x => x.Text).ToArray();


            Console.WriteLine(actualValue[2]);
            Console.WriteLine(actualValue[3]);
            Console.WriteLine(actualValue[4]);
            Console.WriteLine(actualValue[5]);
            Console.WriteLine(actualValue[6]);
            Console.WriteLine(actualValue[7]);
            Console.WriteLine(actualValue[8]);
            Console.WriteLine(actualValue[9]);
            Console.WriteLine(actualValue[10]);
            Console.WriteLine(actualValue[11]);
            Console.WriteLine(actualValue[12]);
            string[] expectedValue = { "--None--", "AED - UAE Dirham", "AUD - Australian Dollar", "BRL - Brazilian Real", "CAD - Canadian Dollar", "CHF - Swiss Franc", "CNY - Chinese Yuan", "CZK - Czech Koruna", "DKK - Danish Krone", "EUR - Euro", "GBP - British Pound", "HKD - Hong Kong Dollar", "ILS - Israeli Shekel", "INR - Indian Rupee", "JPY - Japanese Yen", "MYR - Malaysian Ringgit", "NOK - Norwegian Krone", "QAR - Qatar Rial", "SAR - Saudi Arabian Riyal", "SEK - Swedish Krona", "SGD - Singapore Dollar", "TWD - Taiwan Dollar", "USD - U.S. Dollar", "VND - Vietnam Dong" };
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
            Thread.Sleep(4000);
            driver.FindElement(btnSaveAddHL).Click();
            return isSame;
        }

        //To close the window
        public void CloseDebtStructureWindow()
        {
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnWinClose).Click();
        }

        //To validate Pre-Transaction Debt (MM) section
        public string ValidatePreTransDebtSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPreTransDebtSec, 90);
            string sectionName = driver.FindElement(lblPreTransDebtSec).Text;
            return sectionName;
        }

        //To Validate header row of Pre-Transaction Debt(MM) tab        
        public bool VerifyPreTransDebtHeaderValues()
        {
            IReadOnlyCollection<IWebElement> valHeader = driver.FindElements(headerPreTransDebt);
            var actualValue = valHeader.Select(x => x.Text).ToArray();
            string[] expectedValue = { "", "", "Security Type", "Key Creditors", "Constituent Debt", "Facility Balance (MM)", "Maturity Date", "Interest", "OID Percent", "Amortization", "Call Provisions / Prepayment Premiums", "Mandatory Prepayments / ECF Sweep", "Covenants", "Fees & Expenses" };
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

        //To validate field Pre Reorganization Constituent Debt under Pre-Transaction Debt(MM) section
        public string ValidateLabelPreReorgConstDebt()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPreReorgConstDebt, 90);
            string txtTotalDebt = driver.FindElement(lblPreReorgConstDebt).Text;
            return txtTotalDebt;
        }

        //To validate field Pre Reorganized Total Debt under Pre-Transaction Debt (MM) section
        public string ValidateLabelPreReorgTotal()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPreReorgTotalDebt, 90);
            string txtTotalDebt = driver.FindElement(lblPreReorgTotalDebt).Text;
            return txtTotalDebt;
        }

        //To Click Post-Transaction Information tab        
        public void ClickPostTransInfoTab()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabPostTrans, 90);
            driver.FindElement(tabPostTrans).Click();
        }

        //To validate Post-Transaction Equity Holders section        
        public string ValidatePostTransEquitySection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostTransSec, 90);
            string sectionName = driver.FindElement(lblPostTransSec).Text;
            return sectionName;
        }

        //To validate headers of Post-Transaction Equity Holders section 
        public bool VerifyPostTransEquityHoldersHeader()
        {
            IReadOnlyCollection<IWebElement> valHeader = driver.FindElements(headerEquityHolders);
            var actualValue = valHeader.Select(x => x.Text).ToArray();
            string[] expectedValue = { "", "", "Equity Holder", "Percent Ownership" };
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
        //To validate Post-Transaction Board Members section        
        public string ValidatePostTransMembersSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostTransMembersSec, 90);
            string sectionName = driver.FindElement(lblPostTransMembersSec).Text;
            return sectionName;
        }

        //To validate headers of Post-Transaction Board Members section 
        public bool VerifyPostTransBoardMembersHeader()
        {
            IReadOnlyCollection<IWebElement> valHeader = driver.FindElements(headerBoardMembers);
            var actualValue = valHeader.Select(x => x.Text).ToArray();
            string[] expectedValue = { "", "Board Member", "Company", "Has HL Relationship" };
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

        //To validate Add Debt Structure button        
        public string ValidatePostTransAddDebtButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnPostTransAddDebt, 90);
            string btnAddHL = driver.FindElement(btnPostTransAddDebt).Displayed.ToString();
            Console.WriteLine("btnPostTransAddDebt: " + btnPostTransAddDebt);
            if (btnAddHL.Equals("True"))
            {
                return "Add Debt Structure button is displayed";
            }
            else
            {
                return "Add HL Financing button is not displayed";
            }
        }

        //To Click Add Debt Structure button        
        public string ValidatePostTransAddDebtTitle()
        {
            driver.FindElement(btnPostTransAddDebt).Click();
            Thread.Sleep(4000);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //To validate Post-Transaction Debt (MM) section
        public string ValidatePostTransDebtSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostTransDebtSec, 90);
            string sectionName = driver.FindElement(lblPostTransDebtSec).Text;
            return sectionName;
        }

        //To Validate header row of Post-Transaction Debt(MM) tab        
        public bool VerifyPostTransDebtHeaderValues()
        {
            IReadOnlyCollection<IWebElement> valHeader = driver.FindElements(headerPostTransDebt);
            var actualValue = valHeader.Select(x => x.Text).ToArray();
            string[] expectedValue = { "", "", "Key Creditors", "Security Type", "Constituent Debt", "Facility Balance (MM)", "Maturity Date", "Interest", "OID Percent", "Amortization", "Call Provisions / Prepayment Premiums", "Mandatory Prepayments / ECF Sweep", "Covenants", "Fees & Expenses" };
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
        //To validate field Post-Restructuring Total Debt (MM) under Post-Transaction Debt (MM) section
        public string ValidateLabelPostRestrucTotalDebt()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostTotalDebt, 90);
            string txtTotalDebt = driver.FindElement(lblPostTotalDebt).Text;
            return txtTotalDebt;
        }
        //To validate field Net Debt of the Restructured Company (MM) under Post-Transaction Debt (MM) section
        public string ValidateLabelNetDebtRestrComp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblNetDebt, 90);
            string txtNetDebt = driver.FindElement(lblNetDebt).Text;
            return txtNetDebt;
        }

        //To validate field Closing Stock Price under Post-Transaction Debt (MM) section
        public string ValidateLabelClosingStockPrice()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblClosingStock, 90);
            string txtStockPrice = driver.FindElement(lblClosingStock).Text;
            return txtStockPrice;
        }

        //To Click HL Post-Transaction Opportunities tab        
        public void ClickHLPostTransOppTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabHLPostTrans, 90);
            driver.FindElement(tabHLPostTrans).Click();
        }

        //To validate available values from the list 
        public bool VerifyAvailableValues()
        {
            IReadOnlyCollection<IWebElement> valAval = driver.FindElements(comboAvail);
            var actualValue = valAval.Select(x => x.Text).ToArray();
            string[] expectedValue = { "M&A - Buyside", "M&A - Sellside", "Restructuring", "Valuation", "Financing", "Fairness/Solvency" };
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

        //Validate Add Post-Transaction Staff Role window        
        public string ValidateAddPostTransStaffRoleTitle()
        {
            driver.FindElement(btnAddStaffRole).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 60);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //Validate label Classification         
        public string ValidateLabelClassification()
        {
            driver.SwitchTo().Frame(1);
            string txtClassification = driver.FindElement(lblClassification).Text;
            return txtClassification;
        }

        //To Validate values of Classification combo    
        public bool VerifyClassificationValues()
        {
            IReadOnlyCollection<IWebElement> valClassi = driver.FindElements(comboClassification);
            var actualValue = valClassi.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Financing", "FR", "FSG", "FVA", "Industry" };
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
        //To validate Post-Transaction Staff Roles section        
        public string ValidatePostTransStaffRolesSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblStaffRolesSec, 90);
            string sectionName = driver.FindElement(lblStaffRolesSec).Text;
            return sectionName;
        }

        //To Validate header values of Post-Transaction Staff Roles section   
        public bool VerifyHeaderValuesOfStaffRoles()
        {
            IReadOnlyCollection<IWebElement> valRoles = driver.FindElements(headerStaffRoles);
            var actualValue = valRoles.Select(x => x.Text).ToArray();
            string[] expectedValue = { "", "Name", "Relationship" };
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

        //To validate Post-Transaction Key External Contact section        
        public string ValidatePostTransExtContactSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblExtContactSec, 90);
            string sectionName = driver.FindElement(lblExtContactSec).Text;
            return sectionName;
        }

        //To Validate header values of Post-Transaction Key External Contact section    
        public bool VerifyHeaderValuesOfExtContact()
        {
            IReadOnlyCollection<IWebElement> valContact = driver.FindElements(headerExtContact);
            var actualValue = valContact.Select(x => x.Text).ToArray();
            string[] expectedValue = { "", "Name", "Relationship" };
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

        //To validate Notes section        
        public string ValidateNotesSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblNotesSec, 90);
            string sectionName = driver.FindElement(lblNotesSec).Text;
            return sectionName;
        }

        //To Validate Add Distressed M&A Information window        
        public string ValidateDistressedMAInfoWindow()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddDistressed, 90);
            driver.FindElement(btnAddDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 90);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //To save details of Add Distressed M&A Information         
        public void SaveDistressedMAInfoDetails(string file)
        {
            string excelPath = dir + file;
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, txtAssetSale, 90);
            driver.FindElement(txtAssetSale).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 1));
            driver.FindElement(lnkDateOfSale).Click();
            driver.FindElement(txtMinOverbid).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 2));
            driver.FindElement(txtIncOverbid).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 3));
            driver.FindElement(txtBreakUpFee).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 4));
            driver.FindElement(txtDeposit).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 5));
            driver.FindElement(txtCashComp).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 6));
            driver.FindElement(txtStockComp).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 7));
            driver.FindElement(txtLiability).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 8));
            driver.FindElement(txtClaimConv).SendKeys(ReadExcelData.ReadData(excelPath, "DistressedInfo", 9));
            driver.FindElement(btnSave).Click();
        }

        //To Validate values of added Distressed M&A record  
        public bool ValidateDistressedMARecord()
        {
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, valMARec, 80);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(valMARec);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string value2 = DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine("Date: " + value2);
            string[] expectedValue = { "Test Asset", value2, "USD 10.00", "USD 10.00", "USD 10.00", "USD 10.00", "USD 10.00", "USD 10.00", "USD 10.00", "USD 10.00", "40.00" };
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

        //To update existing Distressed M&A Information record
        public void UpdateDistressedMARecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEdit, 90);
            driver.FindElement(lnkEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 90);
            driver.SwitchTo().Frame(1);
            driver.FindElement(txtMinOverbid).Clear();
            driver.FindElement(txtMinOverbid).SendKeys("20");
            driver.FindElement(txtCashComp).Clear();
            driver.FindElement(txtCashComp).SendKeys("20");
            driver.FindElement(btnSave).Click();
        }

        //To get value of Minimum Overload (MM) column
        public string GetMinOverload()
        {
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, valMARec, 80);
            Thread.Sleep(3000);
            string value = driver.FindElement(valMinOverbid).Text;
            return value;
        }

        //To get value of Cash Component (MM) column
        public string GetCashComp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCashComp, 70);
            Thread.Sleep(3000);
            string value = driver.FindElement(valCashComp).Text;
            return value;
        }

        //To delete the added record and validate the displayed message
        public string DeleteExistingRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDel, 70);
            driver.FindElement(lnkDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, txtMessage, 60);
            string message = driver.FindElement(txtMessage).Text;
            return message;
        }

        //To validate if Other Text box is disabled
        public string ValidateOtherIsDisabledForAllFinTypesExceptOther(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, comboFinancingType, 90);
            driver.FindElement(comboFinancingType).SendKeys(ReadExcelData.ReadData(excelPath, "HLFin", 1));
            string value = driver.FindElement(txtOther).Enabled.ToString();
            return value;
        }

        //To validate if Other Text box is enabled   
        public string ValidateOtherIsEnabledWhenFinTypeIsOther(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, comboFinancingType, 90);
            Thread.Sleep(2000);
            driver.FindElement(comboFinancingType).SendKeys(ReadExcelData.ReadData(excelPath, "HLFin", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, txtOther, 90);
            string value = driver.FindElement(txtOther).Enabled.ToString();
            return value;
        }

        //To save HL Financing record
        public void SaveHLFinancingDetails(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, comboFinancingType, 90);
            driver.FindElement(txtOther).SendKeys("Testing");
            driver.FindElement(comboSecurityType).SendKeys(ReadExcelData.ReadData(excelPath, "HLFin", 3));
            driver.FindElement(txtFinAmmount).SendKeys(ReadExcelData.ReadData(excelPath, "HLFin", 4));
            driver.FindElement(btnHLFinSave).Click();
        }

        //To Validate values of added HL Financing record  
        public bool ValidateHLFinancingRecord(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, valHLFinRec, 80);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(valHLFinRec);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string valFinType = ReadExcelData.ReadData(excelPath, "HLFin", 2);
            string valSecType = ReadExcelData.ReadData(excelPath, "HLFin", 3);
            string[] expectedValue = { valFinType, "Testing", valSecType, "USD 10.00" };
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

        //To update existing Hl Financing record
        public void UpdateHLFinancingRecord(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkHLFinEdit, 90);
            driver.FindElement(lnkHLFinEdit).Click();
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, comboFinTypeEdit, 90);
            driver.FindElement(comboFinTypeEdit).SendKeys(ReadExcelData.ReadData(excelPath, "HLFin", 1));
            driver.FindElement(btnSaveFinEdit).Click();
        }

        //To get value of Financing Type column
        public string GetFinancingType()
        {
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, valFinancingType, 80);
            Thread.Sleep(2000);
            string value = driver.FindElement(valFinancingType).Text;
            return value;
        }

        //To delete the added HL Financing record and validate the displayed message
        public string DeleteExistingHLFinRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkHLFinDel, 70);
            driver.FindElement(lnkHLFinDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgHLFin, 60);
            string message = driver.FindElement(msgHLFin).Text;
            return message;
        }

        //To validate Revenue Financials window
        public string ValidateRevenueFinancialsWindow()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditRevenue, 80);
            driver.FindElement(lnkEditRevenue).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 70);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //To save details of Revenue Financials 
        public void SaveRevenueFinancialsDetails(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtFY1, 80);
            driver.FindElement(txtFY1).Clear();
            driver.FindElement(txtFY1).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 1));
            driver.FindElement(txtFY).Clear();
            driver.FindElement(txtFY).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 2));
            driver.FindElement(txtLTM).Clear();
            driver.FindElement(txtLTM).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 3));
            driver.FindElement(txtFYPlus1).Clear();
            driver.FindElement(txtFYPlus1).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 4));
            driver.FindElement(txtFYPlus2).Clear();
            driver.FindElement(txtFYPlus2).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 5));
            driver.FindElement(txtFYPlus3).Clear();
            driver.FindElement(txtFYPlus3).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 6));
            driver.FindElement(txtFYPlus4).Clear();
            driver.FindElement(txtFYPlus4).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 7));
            driver.FindElement(txtFYPlus5).Clear();
            driver.FindElement(txtFYPlus5).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 8));
            driver.FindElement(btnSaveRevenue).Click();
        }

        //To Validate values of added Revenue Financials details
        public bool ValidateRevenueFinDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowValRevenue, 80);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(rowValRevenue);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string[] expectedValue = { "10.00", "10.00", "10.00", "10.00", "10.00", "10.00", "10.00", "10.00" };
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

        //To validate EBITDA Financials window
        public string ValidateEBITDAFinancialsWindow()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditEBITDA, 80);
            driver.FindElement(lnkEditEBITDA).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 70);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //To save details of EBITDA Financials 
        public void SaveEBITDAFinancialsDetails(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtFY1EBITDA, 80);
            driver.FindElement(txtFY1EBITDA).Clear();
            driver.FindElement(txtFY1EBITDA).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 1));
            driver.FindElement(txtFYEBITDA).Clear();
            driver.FindElement(txtFYEBITDA).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 2));
            driver.FindElement(txtLTMEBITDA).Clear();
            driver.FindElement(txtLTMEBITDA).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 3));
            driver.FindElement(txtFYPlus1EBITDA).Clear();
            driver.FindElement(txtFYPlus1EBITDA).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 4));
            driver.FindElement(txtFYPlus2EBITDA).Clear();
            driver.FindElement(txtFYPlus2EBITDA).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 5));
            driver.FindElement(txtFYPlus3EBITDA).Clear();
            driver.FindElement(txtFYPlus3EBITDA).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 6));
            driver.FindElement(txtFYPlus4EBITDA).Clear();
            driver.FindElement(txtFYPlus4EBITDA).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 7));
            driver.FindElement(txtFYPlus5EBITDA).Clear();
            driver.FindElement(txtFYPlus5EBITDA).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 8));
            driver.FindElement(btnSaveEBITDA).Click();
        }

        //To Validate values of added EBITDA Financials details
        public bool ValidateEBITDAFinDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowValEBITDA, 80);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(rowValEBITDA);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string[] expectedValue = { "10.00", "10.00", "10.00", "10.00", "10.00", "10.00", "10.00", "10.00" };
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

        //To validate Capex Financials window
        public string ValidateCapexFinancialsWindow()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditCapex, 80);
            driver.FindElement(lnkEditCapex).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 70);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //To save details of Capex Financials 
        public void SaveCapexFinancialsDetails(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtFY1Capex, 80);
            driver.FindElement(txtFY1Capex).Clear();
            driver.FindElement(txtFY1Capex).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 1));
            driver.FindElement(txtFYCapex).Clear();
            driver.FindElement(txtFYCapex).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 2));
            driver.FindElement(txtLTMCapex).Clear();
            driver.FindElement(txtLTMCapex).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 3));
            driver.FindElement(txtFYPlus1Capex).Clear();
            driver.FindElement(txtFYPlus1Capex).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 4));
            driver.FindElement(txtFYPlus2Capex).Clear();
            driver.FindElement(txtFYPlus2Capex).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 5));
            driver.FindElement(txtFYPlus3Capex).Clear();
            driver.FindElement(txtFYPlus3Capex).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 6));
            driver.FindElement(txtFYPlus4Capex).Clear();
            driver.FindElement(txtFYPlus4Capex).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 7));
            driver.FindElement(txtFYPlus5Capex).Clear();
            driver.FindElement(txtFYPlus5Capex).SendKeys(ReadExcelData.ReadData(excelPath, "Financials", 8));
            driver.FindElement(btnSaveCapex).Click();
        }

        //To Validate values of added Capex Financials details
        public bool ValidateCapexFinDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowValCapex, 80);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(rowValCapex);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string[] expectedValue = { "10.00", "10.00", "10.00", "10.00", "10.00", "10.00", "10.00", "10.00" };
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
        //To validate Add Equity Holder button        
        public string ValidateAddEquityHolderButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddEquityHolder, 90);
            string btnAddEquity = driver.FindElement(btnAddEquityHolder).Displayed.ToString();
            Console.WriteLine("btnAddEquity: " + btnAddEquity);
            if (btnAddEquity.Equals("True"))
            {
                return "Add Equity Holder button is displayed";
            }
            else
            {
                return "Add Equity Holder button is not displayed";
            }
        }

        //To Click Add Equity Holder button        
        public string ValidateAddEquityTitle()
        {
            driver.FindElement(btnAddEquityHolder).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 90);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //To Add Equity Holder details        
        public string SaveEquityHolderDetails()
        {
            driver.SwitchTo().Frame(1);
            driver.FindElement(txtEquitySearch).SendKeys("Techno Coatings");
            driver.FindElement(btnGo).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkRow, 90);
            driver.FindElement(checkRow).Click();
            driver.FindElement(btnAddSelected).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 120);
            string message = driver.FindElement(msgSuccess).Text;
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnEquityClose).Click();
            return message;
        }

        //To validate value of added Equity Holder 
        public string ValidateAddedEquityHolderValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEquityHolder, 90);
            string valEquity = driver.FindElement(valEquityHolder).Text;
            return valEquity;
        }

        //To update added Equity Holder values
        public string UpdateAndValidateEquityHolderValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEquityEdit, 90);
            driver.FindElement(lnkEquityEdit).Click();
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, txtOwnership, 90);
            driver.FindElement(txtOwnership).Clear();
            driver.FindElement(txtOwnership).SendKeys("10.00");
            driver.FindElement(btnEquitySave).Click();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, valOwnership, 120);
            Thread.Sleep(5000);
            string valOwner = driver.FindElement(valOwnership).Text;
            return valOwner;
        }

        //To copy added Equity Holder record
        public void CopyEquityHolderRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEquityCopy, 100);
            driver.FindElement(lnkEquityCopy).Click();
            Thread.Sleep(4000);
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
        public bool ValidateCopiedEquityHolderValues()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowEquityHoldersPost, 80);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(rowEquityHoldersPost);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Edit", "Del", "Techno Coatings, Inc.", "10.00%" };
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

        //To delete added Equity Holder record
        public string DeleteAndValidateEquityHolderRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEquityDel, 90);
            driver.FindElement(lnkEquityDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgEquityDelete, 90);
            string message = driver.FindElement(msgEquityDelete).Text;
            return message;
        }
        //To delete added Equity Holder record in Post Transaction information tab
        public string DeleteAndValidatePostEquityHolderRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPostEquityDel, 90);
            driver.FindElement(lnkPostEquityDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgPostEquityDelete, 90);
            string message = driver.FindElement(msgPostEquityDelete).Text;
            return message;
        }


        //To validate Add Board Member button        
        public string ValidateAddBoardMemberButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddBoardMember, 90);
            string btnAddBoard = driver.FindElement(btnAddBoardMember).Displayed.ToString();
            Console.WriteLine("btnAddBoard: " + btnAddBoard);
            if (btnAddBoard.Equals("True"))
            {
                return "Add Board Member button is displayed";
            }
            else
            {
                return "Add Board Member button is not displayed";
            }
        }

        //To Click Add Board Member button        
        public string ValidateAddBoardMemberTitle()
        {
            driver.FindElement(btnAddBoardMember).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 90);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //To Add Board Member details        
        public string SaveBoardMemberDetails()
        {
            driver.SwitchTo().Frame(1);
            //driver.FindElement(txtEquitySearch).SendKeys("Adam Daland");
            driver.FindElement(txtEquitySearch).SendKeys("Adam B. Davis");
            driver.FindElement(btnGo).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkRowBoard, 120);
            driver.FindElement(checkRowBoard).Click();
            driver.FindElement(btnAddSelected).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessBoard, 120);
            string message = driver.FindElement(msgSuccessBoard).Text;
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnEquityClose).Click();
            return message;
        }

        //To validate value of added Board Member in Pre-Transaction Board Members section
        public string ValidateAddedBoardMember()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valBoardMember, 90);
            string valMember = driver.FindElement(valBoardMember).Text;
            return valMember;
        }

        //To copy added Board Member detail
        public void CopyBoardMemberRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkBoardCopy, 90);
            driver.FindElement(lnkBoardCopy).Click();
            Thread.Sleep(2000);
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

        //To Validate values of copied Board Member in Post Transaction Information tab
        public bool ValidateCopiedBoardMemberValues()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowBoardMemberPost, 80);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(rowBoardMemberPost);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Del", "Adam B. Davis", "Wells Fargo", "" };
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

        //To delete added Board Member record
        public string DeleteAndValidateBoardMemberRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkBoardDel, 90);
            driver.FindElement(lnkBoardDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgBoardDelete, 90);
            string message = driver.FindElement(msgBoardDelete).Text;
            return message;
        }

        //To delete added Board Member record in Post-Transaction Board Members section
        public string DeleteAndValidatePostBoardMemberRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPostBoardDel, 90);
            driver.FindElement(lnkPostBoardDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgPostBoardDelete, 90);
            string message = driver.FindElement(msgPostBoardDelete).Text;
            return message;
        }
        //To validate message "*Save Record to Add Lenders"       
        public string ValidateSaveRecordMessage()
        {
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, msgSaveRec, 100);
            string message = driver.FindElement(msgSaveRec).Text;
            return message;
        }

        //To Add Debt Structure details        
        public string SaveDebtStructureDetails(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, comboSecurityTypeDebt, 180);
            driver.FindElement(comboSecurityTypeDebt).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 1));
            driver.FindElement(txtMaturityDate).Click();
            driver.FindElement(txtInterest).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 2));
            driver.FindElement(txtOID).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 3));
            driver.FindElement(txtAmortization).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 4));
            driver.FindElement(txtCallProvisions).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 5));
            driver.FindElement(txtMandatory).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 6));
            driver.FindElement(txtCovenants).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 7));
            driver.FindElement(txtFees).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 8));
            driver.FindElement(txtFacility).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 9));
            driver.FindElement(btnSaveDebt).Click();
            string message = driver.FindElement(msgSuccessDebt).Text;
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnEquityClose).Click();
            return message;
        }

        //To Validate values of added Debt Structure in Pre Transaction Information tab
        public bool ValidateDebtStructureValues()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowDebtStrPre, 140);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(rowDebtStrPre);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string value2 = DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine(value2);
            string[] expectedValue = { "LC Facility", "", "USD 10.00", value2, "10.00", "10.000%", "USD 10.00", "USD 10.00", "USD 10.00", "10.00", "USD 10.00" };
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
        //To update added Debt Structure values
        public string UpdateAndValidateDebtStructureValue(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEquityEdit, 90);
            driver.FindElement(lnkEquityEdit).Click();
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, comboSecurityTypeDebt, 90);
            driver.FindElement(comboSecurityTypeDebt).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 10));
            driver.FindElement(btnSaveDebt).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
            driver.FindElement(btnEquityClose).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valDebtSecurityType, 90);
            Thread.Sleep(3000);
            string valSecurityType = driver.FindElement(valDebtSecurityType).Text;
            return valSecurityType;
        }

        //To delete added Debt Structure record
        public string DeleteAndValidateDebtStructureRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDebtDel, 100);
            driver.FindElement(lnkDebtDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgDebtDelete, 110);
            string message = driver.FindElement(msgDebtDelete).Text;
            return message;
        }

        //To delete added Debt Structure record
        public string DeleteAndValidatePreDebtStructureRecordL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDebtDelL, 100);
            driver.FindElement(lnkDebtDelL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 120);
            driver.FindElement(btnOK).Click();
            Thread.Sleep(4000);
            try
            {
                if (driver.FindElement(lnkDebtDelL).Displayed)
                {
                    return "Debt Structure still exists";
                }
                else
                {
                    return "No records to display";
                }
            }
            catch (Exception)
            {
                return "No records to display";
            }
        }
        //To delete added Debt Structure record
        public string DeleteAndValidatePostDebtStructureRecordL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPostDebtDelL, 100);
            driver.FindElement(lnkPostDebtDelL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 120);
            driver.FindElement(btnOK).Click();
            Thread.Sleep(4000);
            try
            {
                if (driver.FindElement(lnkDebtDelL).Displayed)
                {
                    return "Debt Structure still exists";
                }
                else
                {
                    return "No records to display";
                }
            }
            catch (Exception)
            {
                return "No records to display";
            }
        }

        //To add lender details
        public string AddLenderDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEquityEdit, 90);
            driver.FindElement(lnkEquityEdit).Click();
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewLender, 90);
            driver.FindElement(btnNewLender).Click();
            driver.SwitchTo().Frame(0);
            WebDriverWaits.WaitUntilEleVisible(driver, txtEquitySearch, 90);
            driver.FindElement(txtEquitySearch).SendKeys("ABC");
            driver.FindElement(btnGo).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkRow, 90);
            driver.FindElement(checkRow).Click();
            driver.FindElement(txtLenderAmt).SendKeys("10");
            driver.FindElement(btnAddSelected).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 150);
            string message = driver.FindElement(msgSuccess).Text;
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            driver.FindElement(btnEquityClose).Click();
            return message;
        }

        //Validate added lender details
        public bool ValidateLenderValues()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowLender, 100);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(rowLender);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string[] expectedValue = { "ABC", "10.00" };
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

        //To update lender details
        public string EditLenderDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkLenderEdit, 120);
            driver.FindElement(lnkLenderEdit).Click();
            driver.SwitchTo().Frame(0);
            WebDriverWaits.WaitUntilEleVisible(driver, txtLoanAmt, 90);
            driver.FindElement(txtLoanAmt).Clear();
            driver.FindElement(txtLoanAmt).SendKeys("20");
            driver.FindElement(btnSaveLender).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessDebt, 90);
            string message = driver.FindElement(msgSuccessDebt).Text;
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            driver.FindElement(btnEquityClose).Click();
            return message;
        }

        //To get updated value of Loan Amount
        public string GetLoanAmount()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLoanAmt, 90);
            Thread.Sleep(3000);
            string Amount = driver.FindElement(valLoanAmt).Text;
            return Amount;
        }

        //To delete lender details
        public string DeleteLenderDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkLenderDelete, 90);
            driver.FindElement(lnkLenderDelete).Click();
            Thread.Sleep(4000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(5000);

            try
            {
                string rowDisplayed = driver.FindElement(rowLenderPostDel).Displayed.ToString();
                Console.WriteLine("rowDisplayed :" + rowDisplayed);
                return rowDisplayed;
            }
            catch (Exception)
            {
                return "No row displayed";
            }
        }

        //To close lender page
        public void CloseLenderDetailsPage()
        {
            Thread.Sleep(8000);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEquityClose, 300);
            driver.FindElement(btnEquityClose).Click();
        }

        //To Click Add Equity Holder button in Post- Transaction Information tab       
        public string ValidatePostAddEquityTitle()
        {
            Thread.Sleep(5000);
            driver.FindElement(btnPostAddEquity).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 90);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }
        //To validate value of added Equity Holder in Post- Transaction Information tab
        public string ValidatePostAddedEquityHolderValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPostEquityHolder, 90);
            string valEquity = driver.FindElement(valPostEquityHolder).Text;
            return valEquity;
        }

        //To update added Equity Holder values in Post Transaction Information tab
        public string UpdateAndValidatePostEquityHolderValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPostEquityEdit, 90);
            driver.FindElement(lnkPostEquityEdit).Click();
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, txtOwnership, 90);
            driver.FindElement(txtOwnership).Clear();
            driver.FindElement(txtOwnership).SendKeys("10.00");
            Thread.Sleep(3000);
            driver.FindElement(btnEquitySave).Click();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, valPostOwnership, 90);
            Thread.Sleep(5000);
            string valOwner = driver.FindElement(valPostOwnership).Text;
            return valOwner;
        }

        //To Click Add Board Member button in Post- Transaction Information tab       
        public string ValidatePostAddBoardMemberTitle()
        {
            driver.FindElement(btnPostAddBoardMember).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 90);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }
        //To validate value of added Board Member in Post-Transaction Board Members section
        public string ValidatePostAddedBoardMember()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPostBoardMember, 90);
            string valMember = driver.FindElement(valPostBoardMember).Text;
            return valMember;
        }

        //To Click Add Debt Structure button        
        public string ValidatePostAddDebtTitle()
        {
            driver.FindElement(btnPostAddDebtStructure).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleInsertNewHL, 90);
            string title = driver.FindElement(titleInsertNewHL).Text;
            return title;
        }

        //To Validate values of added Debt Structure in Post Transaction Information tab
        public bool ValidatePostDebtStructureValues()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowPostDebtStrPre, 100);
            IReadOnlyCollection<IWebElement> valRow = driver.FindElements(rowPostDebtStrPre);
            var actualValue = valRow.Select(x => x.Text).ToArray();
            string value2 = DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture);
            string[] expectedValue = { "LC Facility", "", "USD 10.00", value2, "10.00", "10.000%", "USD 10.00", "USD 10.00", "USD 10.00", "10.00", "USD 10.00" };
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

        //To update added Debt Structure values in Post Transaction Information tab
        public string UpdateAndValidatePostDebtStructureValue(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPostEquityEdit, 90);
            driver.FindElement(lnkPostEquityEdit).Click();
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, comboSecurityTypeDebt, 90);
            driver.FindElement(comboSecurityTypeDebt).SendKeys(ReadExcelData.ReadData(excelPath, "Debt", 10));
            driver.FindElement(btnSaveDebt).Click();
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnEquityClose).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valPostDebtSecurityType, 90);
            Thread.Sleep(3000);
            string valSecurityType = driver.FindElement(valPostDebtSecurityType).Text;
            return valSecurityType;
        }

        //To delete added Debt Structure record in Post Transaction Information    
        public string DeleteAndValidatePostDebtStructureRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPostDebtDel, 90);
            driver.FindElement(lnkPostDebtDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgPostDebtDelete, 90);
            string message = driver.FindElement(msgPostDebtDelete).Text;
            return message;
        }

        //To add lender details in Post Transaction Information
        public string AddLenderDetailsPostTrans()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPostEquityEdit, 90);
            driver.FindElement(lnkPostEquityEdit).Click();
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewLender, 90);
            driver.FindElement(btnNewLender).Click();
            driver.SwitchTo().Frame(0);
            WebDriverWaits.WaitUntilEleVisible(driver, txtEquitySearch, 90);
            driver.FindElement(txtEquitySearch).SendKeys("ABC");
            driver.FindElement(btnGo).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkRow, 90);
            driver.FindElement(checkRow).Click();
            driver.FindElement(txtLenderAmt).SendKeys("10");
            driver.FindElement(btnAddSelected).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 150);
            string message = driver.FindElement(msgSuccess).Text;
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            driver.FindElement(btnEquityClose).Click();
            return message;
        }

        //To delete added Debt Structure record
        public string DeleteAndValidateDebtStructureRecordPostTrans()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPostDebtDel, 90);
            driver.FindElement(lnkPostDebtDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgPostDebtDelete, 90);
            string message = driver.FindElement(msgPostDebtDelete).Text;
            return message;
        }

        //To save Staff Role       
        public string SaveStaffRoleValues()
        {
            driver.SwitchTo().Frame(1);
            driver.FindElement(txtClassification).SendKeys("Financing");
            driver.FindElement(txtEquitySearch).SendKeys("Sonika Goyal");
            driver.FindElement(btnGo).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkStaffRole, 110);
            driver.FindElement(checkStaffRole).Click();
            driver.FindElement(btnAddSelected).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessStaff, 120);
            string message = driver.FindElement(msgSuccessStaff).Text;
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnEquityClose).Click();
            return message;
        }

        //To Validate added values of Post-Transaction Staff Roles section   
        public bool VerifyAddedValuesOfStaffRoles()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStaffRoles, 100);
            IReadOnlyCollection<IWebElement> valRoles = driver.FindElements(valStaffRoles);
            var actualValue = valRoles.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Sonika Goyal", "Financing" };
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

        //To delete added Staff Role record
        public string DeleteAndValidateStaffRoleRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkStaffRoleDel, 90);
            driver.FindElement(lnkStaffRoleDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgStaffRoleDel, 90);
            string message = driver.FindElement(msgStaffRoleDel).Text;
            return message;
        }

        //Click on Return to Engagement
        public void ClickReturnToEngagement()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToEngagement, 150);
            driver.FindElement(btnReturnToEngagement).Click();
        }
        public string ValidateFieldTransactionTypeL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblTransTypeL, 90);
            string txtTransType = driver.FindElement(lblTransTypeL).Text;
            return txtTransType;
        }

        public string ValidateFieldPostTransactionStatusL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostTxnStatusL, 90);
            string value = driver.FindElement(lblPostTxnStatusL).Text;
            return value;
        }

        public string ValidateFieldCompDescL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCompDescL, 90);
            string value = driver.FindElement(lblCompDescL).Text;
            return value;
        }

        public string ValidateFieldClientDescL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblClientDescL, 90);
            string value = driver.FindElement(lblClientDescL).Text;
            return value;
        }

        public string ValidateFieldRestructuringTxnDescL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblReTxnL, 90);
            string value = driver.FindElement(lblReTxnL).Text;
            return value;
        }

        //Get the value of Transaction Type
        public string GetValueOfTransactionTypeInFREngL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTxnType, 90);
            string value = driver.FindElement(valTxnType).Text;
            return value;
        }

        //Validate if Transaction Type is editable or not
        public string ValidateTransactionTypeIfEditable()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtTxnType, 90);
            string value = driver.FindElement(txtTxnType).GetAttribute("class");
            return value;
        }
        //Get the value of Transaction Type
        public string GetValueOfPostTransactionStatusInFREngL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPostTxnStatus, 90);
            string value = driver.FindElement(valPostTxnStatus).Text;
            return value;
        }

        //Get the value of Client Description
        public string GetValueOfClientDescInFREngL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valClientDesc, 90);
            string value = driver.FindElement(valClientDesc).Text;
            return value;
        }
        //Get the value of Company Description
        public string GetValueOfCompDescInFREngL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompDesc, 90);
            string value = driver.FindElement(valCompDesc).Text;
            return value;
        }

        //Get the value of Restructuring Transaction Description
        public string GetValueOfReTxnDescInFREngL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valReTxnDesc, 90);
            string value = driver.FindElement(valReTxnDesc).Text;
            return value;
        }

        //Get the note displaye at the bottom of Engagement Info tab
        public string GetNoteDisplayedOnEngInfoL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgReqInfo, 90);
            string value = driver.FindElement(msgReqInfo).Text;
            return value;
        }

        public string ValidateFieldCurrencyFinancialsAreReportedInL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCurrencyFinL, 90);
            string txtCurrency = driver.FindElement(lblCurrencyFinL).Text;
            return txtCurrency;
        }

        public string ValidateFieldProjectionsAreAsOfL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblProjectionsL, 90);
            string txtProj = driver.FindElement(lblProjectionsL).Text;
            return txtProj;
        }
        public string ValidateFieldLTMProjectionsAreAsOfL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblLTMProjectionsL, 90);
            string txtLTMProj = driver.FindElement(lblLTMProjectionsL).Text;
            return txtLTMProj;
        }

        public bool VerifyRevenueFieldsL()
        {
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(lblRevenueL);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            // string[] expectedValues = {"Record Type Name Description", "CF Corporate Finance", "Conflicts Check  ", "FAS Financial Advisory Services", "FR Financial Restructuring", "HL Internal Opportunity This record type is used for ERP \"Recommended VAT Treatment\"", "OPP DEL  ", "SC Strategic Consulting"};
            string[] expectedValues = { "Revenue FY-1 (MM)", "Revenue FY (MM)", "Revenue LTM (MM)", "Revenue FY+1 (MM)", "Revenue FY+2 (MM)", "Revenue FY+3 (MM)", "Revenue FY+4 (MM)", "Revenue FY+5 (MM)" };
            bool isTrue = true;

            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }

        public bool VerifyEBITDAFieldsL()
        {
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(lblEBITDAL);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            // string[] expectedValues = {"Record Type Name Description", "CF Corporate Finance", "Conflicts Check  ", "FAS Financial Advisory Services", "FR Financial Restructuring", "HL Internal Opportunity This record type is used for ERP \"Recommended VAT Treatment\"", "OPP DEL  ", "SC Strategic Consulting"};
            string[] expectedValues = { "EBITDA FY-1 (MM)", "EBITDA FY (MM)", "EBITDA LTM (MM)", "EBITDA FY+1 (MM)", "EBITDA FY+2 (MM)", "EBITDA FY+3 (MM)", "EBITDA FY+4 (MM)", "EBITDA FY+5 (MM)" };
            bool isTrue = true;

            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }

        public bool VerifyCapexFieldsL()
        {
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(lblCapexL);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            // string[] expectedValues = {"Record Type Name Description", "CF Corporate Finance", "Conflicts Check  ", "FAS Financial Advisory Services", "FR Financial Restructuring", "HL Internal Opportunity This record type is used for ERP \"Recommended VAT Treatment\"", "OPP DEL  ", "SC Strategic Consulting"};
            string[] expectedValues = { "Capex FY-1 (MM)", "Capex FY (MM)", "Capex LTM (MM)", "Capex FY+1 (MM)", "Capex FY+2 (MM)", "Capex FY+3 (MM)", "Capex FY+4 (MM)", "Capex FY+5 (MM)" };
            bool isTrue = true;

            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }

        //Validate if Revenue are editable or not
        public string ValidateIfRevenueFieldsAreEditable()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtRevenue, 90);
            driver.FindElement(txtRevenue).Click();
            string value = driver.FindElement(txtRevenue).Enabled.ToString();
            return value;
        }

        //Validate if EBITDA are editable or not
        public string ValidateIfEBITDAFieldsAreEditable()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEBITDA, 90);
            driver.FindElement(txtEBITDA).Click();
            string value = driver.FindElement(txtEBITDA).Enabled.ToString();
            return value;
        }

        //Validate if Capex are editable or not
        public string ValidateIfCapexFieldsAreEditable()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCapex, 90);
            driver.FindElement(txtCapex).Click();
            string value = driver.FindElement(txtCapex).Enabled.ToString();
            return value;
        }

        //Get the currency
        public string GetCurrencyValueOfFinancialsTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCurrency, 90);
            string value = driver.FindElement(valCurrency).GetAttribute("data-value");
            return value;
        }

        //Enter the value in Projections are as of
        public string ValidateProjectionsCalendar()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnProjections, 90);
            driver.FindElement(btnProjections).Click();
            Thread.Sleep(10000);
            string value = driver.FindElement(imgProjCalendar).Enabled.ToString();
            Console.WriteLine("value: " + value);
            if (value.Equals("True"))
            {
                return "Projections Calendar got displayed";
            }
            else
            {
                return "Projections Calendar did not get displayed";
            }
        }

        //Enter the value in LTMvProjections are as of
        public string ValidateLTMProjectionsCalendar()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnLTMProjections, 90);
            driver.FindElement(btnLTMProjections).Click();
            Thread.Sleep(10000);
            string value = driver.FindElement(imgLTMProjCalendar).Enabled.ToString();
            Console.WriteLine("value: " + value);
            if (value.Equals("True"))
            {
                return "LTM Projections Calendar got displayed";
            }
            else
            {
                return "LTM Projections Calendar did not get displayed";
            }
        }

        //Validate save functionality of Financials tab
        public string ValidateSaveFunctionalityOfFinancialsTab()
        {
            driver.FindElement(txtRevFYM1).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnProjections).Clear();
            driver.FindElement(btnProjections).SendKeys("Sep 5, 2023");
            WebDriverWaits.WaitUntilEleVisible(driver, btnLTMProjections, 100);
            driver.FindElement(btnLTMProjections).Clear();
            driver.FindElement(btnLTMProjections).SendKeys("Sep 5, 2023");
            driver.FindElement(txtRevFYM1).Clear();
            driver.FindElement(txtRevFYM1).SendKeys("10");
            driver.FindElement(txtRevFY).Clear();
            driver.FindElement(txtRevFY).SendKeys("10");
            driver.FindElement(txtRevLTM).Clear();
            driver.FindElement(txtRevLTM).SendKeys("10");
            driver.FindElement(txtRevFYA1).Clear();
            driver.FindElement(txtRevFYA1).SendKeys("10");
            driver.FindElement(txtRevFYA2).Clear();
            driver.FindElement(txtRevFYA2).SendKeys("10");
            driver.FindElement(txtRevFYA3).Clear();
            driver.FindElement(txtRevFYA3).SendKeys("10");
            driver.FindElement(txtRevFYA4).Clear();
            driver.FindElement(txtRevFYA4).SendKeys("10");
            driver.FindElement(txtRevFYA5).Clear();
            driver.FindElement(txtRevFYA5).SendKeys("10");

            driver.FindElement(txtEBITDAFYM1).Clear();
            driver.FindElement(txtEBITDAFYM1).SendKeys("10");
            driver.FindElement(txtEBITDAFY).Clear();
            driver.FindElement(txtEBITDAFY).SendKeys("10");
            driver.FindElement(txtEBITDALTM).Clear();
            driver.FindElement(txtEBITDALTM).SendKeys("10");
            driver.FindElement(txtEBITDAFYA1).Clear();
            driver.FindElement(txtEBITDAFYA1).SendKeys("10");
            driver.FindElement(txtEBITDAFYA2).Clear();
            driver.FindElement(txtEBITDAFYA2).SendKeys("10");
            driver.FindElement(txtEBITDAFYA3).Clear();
            driver.FindElement(txtEBITDAFYA3).SendKeys("10");
            driver.FindElement(txtEBITDAFYA4).Clear();
            driver.FindElement(txtEBITDAFYA4).SendKeys("10");
            driver.FindElement(txtEBITDAFYA5).Clear();
            driver.FindElement(txtEBITDAFYA5).SendKeys("10");

            driver.FindElement(txtCapexFYM1).Clear();
            driver.FindElement(txtCapexFYM1).SendKeys("10");
            driver.FindElement(txtCapexFY).Clear();
            driver.FindElement(txtCapexFY).SendKeys("10");
            driver.FindElement(txtCapexLTM).Clear();
            driver.FindElement(txtCapexLTM).SendKeys("10");
            driver.FindElement(txtCapexFYA1).Clear();
            driver.FindElement(txtCapexFYA1).SendKeys("10");
            driver.FindElement(txtCapexFYA2).Clear();
            driver.FindElement(txtCapexFYA2).SendKeys("10");
            driver.FindElement(txtCapexFYA3).Clear();
            driver.FindElement(txtCapexFYA3).SendKeys("10");
            driver.FindElement(txtCapexFYA4).Clear();
            driver.FindElement(txtCapexFYA4).SendKeys("10");
            driver.FindElement(txtCapexFYA5).Clear();
            driver.FindElement(txtCapexFYA5).SendKeys("10");

            driver.FindElement(btnSaveFinancials).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSaveFinancials, 90);
            string message = driver.FindElement(msgSaveFinancials).Text;
            return message;
        }

        //Validate save functionality of Financials tab
        public string ValidateUpdateFunctionalityOfFinancialsTab()
        {
            driver.FindElement(txtRevFYM1).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtRevFYM1).Clear();
            driver.FindElement(txtRevFYM1).SendKeys("20");
            driver.FindElement(txtRevFY).Clear();
            driver.FindElement(txtRevFY).SendKeys("20");
            driver.FindElement(txtRevLTM).Clear();
            driver.FindElement(txtRevLTM).SendKeys("20");
            driver.FindElement(txtRevFYA1).Clear();
            driver.FindElement(txtRevFYA1).SendKeys("20");
            driver.FindElement(txtRevFYA2).Clear();
            driver.FindElement(txtRevFYA2).SendKeys("20");
            driver.FindElement(txtRevFYA3).Clear();
            driver.FindElement(txtRevFYA3).SendKeys("20");
            driver.FindElement(txtRevFYA4).Clear();
            driver.FindElement(txtRevFYA4).SendKeys("20");
            driver.FindElement(txtRevFYA5).Clear();
            driver.FindElement(txtRevFYA5).SendKeys("20");

            driver.FindElement(txtEBITDAFYM1).Clear();
            driver.FindElement(txtEBITDAFYM1).SendKeys("20");
            driver.FindElement(txtEBITDAFY).Clear();
            driver.FindElement(txtEBITDAFY).SendKeys("20");
            driver.FindElement(txtEBITDALTM).Clear();
            driver.FindElement(txtEBITDALTM).SendKeys("20");
            driver.FindElement(txtEBITDAFYA1).Clear();
            driver.FindElement(txtEBITDAFYA1).SendKeys("20");
            driver.FindElement(txtEBITDAFYA2).Clear();
            driver.FindElement(txtEBITDAFYA2).SendKeys("20");
            driver.FindElement(txtEBITDAFYA3).Clear();
            driver.FindElement(txtEBITDAFYA3).SendKeys("20");
            driver.FindElement(txtEBITDAFYA4).Clear();
            driver.FindElement(txtEBITDAFYA4).SendKeys("20");
            driver.FindElement(txtEBITDAFYA5).Clear();
            driver.FindElement(txtEBITDAFYA5).SendKeys("20");

            driver.FindElement(txtCapexFYM1).Clear();
            driver.FindElement(txtCapexFYM1).SendKeys("20");
            driver.FindElement(txtCapexFY).Clear();
            driver.FindElement(txtCapexFY).SendKeys("20");
            driver.FindElement(txtCapexLTM).Clear();
            driver.FindElement(txtCapexLTM).SendKeys("20");
            driver.FindElement(txtCapexFYA1).Clear();
            driver.FindElement(txtCapexFYA1).SendKeys("20");
            driver.FindElement(txtCapexFYA2).Clear();
            driver.FindElement(txtCapexFYA2).SendKeys("20");
            driver.FindElement(txtCapexFYA3).Clear();
            driver.FindElement(txtCapexFYA3).SendKeys("20");
            driver.FindElement(txtCapexFYA4).Clear();
            driver.FindElement(txtCapexFYA4).SendKeys("20");
            driver.FindElement(txtCapexFYA5).Clear();
            driver.FindElement(txtCapexFYA5).SendKeys("20");

            driver.FindElement(btnSaveFinancials).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSaveFinancials, 90);
            string message = driver.FindElement(msgSaveFinancials).Text;
            return message;
        }

        //Validate Currency validation on Financials tab
        public string ValidateCurrencyFinancialsValidation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCurrency, 110);
            driver.FindElement(valCurrency).Click();
            Thread.Sleep(3000);
            var element = driver.FindElement(valCurrencyNone);
            Actions action = new Actions(driver);
            action.MoveToElement(element);
            action.Perform();
            driver.FindElement(valCurrencyNone).Click();
            driver.FindElement(btnSaveFinancials).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgCurrencyFinancials, 90);
            string message = driver.FindElement(msgCurrencyFinancials).Text;
            return message;
        }

        //Validate DM&A Info fields 
        public bool VerifyDMAndAInfoFieldsL()
        {
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(lblDMAFieldsL);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            // string[] expectedValues = {"Record Type Name Description", "CF Corporate Finance", "Conflicts Check  ", "FAS Financial Advisory Services", "FR Financial Restructuring", "HL Internal Opportunity This record type is used for ERP \"Recommended VAT Treatment\"", "OPP DEL  ", "SC Strategic Consulting"};
            string[] expectedValues = { "Asset Sold", "Date of Sale", "Minimum Overbid (MM)", "Incremental Overbid (MM)", "Break up fee (MM)", "Deposit (MM)", "Cash Component (MM)", "Stock Component (MM)", "Liability Assumed (MM)", "Claim Conversion (MM)", "Total Sales Price (MM)" };
            bool isTrue = true;

            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }


        //Validate fields on Add Distressed M & A Info
        public bool VerifyAddDistressedFieldsL()
        {
            driver.FindElement(btnAddDistressedL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblAddDistressedL);
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(lblAddDistressedL);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "*Asset Sold", "Currency", "Date of Sale", "Minimum Overbid (MM)", "Incremental Overbid (MM)", "Break Up Fee (MM)", "Deposit (MM)", "Cash Component (MM)", "Stock Component (MM)", "Liability Assumed (MM)", "Claim Conversion (MM)" };
            bool isTrue = true;
            Console.WriteLine(actualNamesAndDesc[0]);
            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }

        //Validate HL Financing fields 
        public bool VerifyHLFinancingTableFieldsL()
        {
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(lblHLFinTable);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "Financing Type", "Other", "Security Type", "Financing Amount (MM)" };
            bool isTrue = true;

            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }

        public string ValidateLabelTotalFinancingAmount()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblTotalFinAmt, 120);
            string value = driver.FindElement(lblTotalFinAmt).Text;
            return value;
        }

        public string ValidateLabelFinancingDesc()
        {
            string value = driver.FindElement(lblFinDescL).Text;
            return value;
        }

        //Validate mandatory validation for Asset Sold
        public string ValidateMandatoryMessageForAssetSoldField()
        {
            driver.FindElement(btnSaveAddDistressedL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgAssetSold);
            string message = driver.FindElement(msgAssetSold).Text;
            return message;

        }

        //Validate page after clicking Cancel button on Add Distressed M&A Info Page
        public string ValidatePageAfterClickingCancelOnAddDistressedMAInfoPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 150);
            driver.FindElement(btnCancel).Click();
            string name = driver.FindElement(tabDMAL).Text;
            return name;
        }


        //Validate Financing Type
        public string VerifyFinancingTypeFieldL()
        {
            driver.FindElement(btnAddHLFinL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblFinancingTypeL, 180);
            string name = driver.FindElement(lblFinancingTypeL).Text;
            return name;
        }

        //Validate Security Type
        public string VerifySecurityTypeFieldL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblSecurityTypeL, 180);
            string name = driver.FindElement(lblSecurityTypeL).Text;
            return name;
        }

        //Validate Financing Amount
        public string VerifyFinancingAmountFieldL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblFinAmountL, 180);
            string name = driver.FindElement(lblFinAmountL).Text;
            return name;
        }

        //Validate Other
        public string VerifyOtherFieldL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblOtherL, 180);
            string name = driver.FindElement(lblOtherL).Text;
            return name;
        }

        //Validate Financing Type values
        public bool VerifyFinancingTypeValuesL()
        {
            driver.FindElement(btnFinTypeL).Click();
            Thread.Sleep(3000);
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(valFinTypesL);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "--None--", "Acquisition", "Credit Facility", "DIP", "Equity", "Exit Facility", "Mezzanine", "New Equity Financing", "Refinancing", "Revolver", "Rollover", "Term Loan", "Other" };
            bool isTrue = true;

            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            driver.FindElement(btnFinTypeL).Click();
            return isTrue;
        }

        //Validate Security Type values
        public bool VerifySecurityTypeValuesL()
        {
            driver.FindElement(btnSecTypeL).Click();
            Thread.Sleep(3000);
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(valSecTypesL);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "--None--", "Bank Debt (First Lien) - Revolver", "Bank Debt (First Lien) - Term Loan A", "Bank Debt (First Lien) - Term Loan B", "Bank Debt (First Lien) - Synthetic LC Facility", "Bank Debt (Second Lien)", "Senior Structured Notes", "Capital Leases", "Other Secured Debt", "Mezzanine Debt", "Senior Notes (Unsecured)", "Senior Subordinated Notes (Unsecured)", "Other Unsecured Debt", "Common Equity", "Preferred Equity" };
            Console.WriteLine("1st:" + actualNamesAndDesc[0]);
            Console.WriteLine("1st:" + actualNamesAndDesc[1]);
            Console.WriteLine(actualNamesAndDesc[2]);
            Console.WriteLine(actualNamesAndDesc[3]);
            Console.WriteLine(actualNamesAndDesc[4]);
            Console.WriteLine(actualNamesAndDesc[5]);
            Console.WriteLine(actualNamesAndDesc[6]);
            Console.WriteLine(actualNamesAndDesc[7]);
            Console.WriteLine(actualNamesAndDesc[8]);
            Console.WriteLine(actualNamesAndDesc[9]);
            Console.WriteLine(actualNamesAndDesc[10]);
            Console.WriteLine(actualNamesAndDesc[11]);
            Console.WriteLine(actualNamesAndDesc[12]);

            bool isTrue = true;

            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            driver.FindElement(btnSecTypeL).Click();
            return isTrue;
        }

        //Validate Security Type values
        public bool VerifySecurityTypeDebtValuesL()
        {
            driver.FindElement(btnSecTypeL).Click();
            Thread.Sleep(3000);
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(valSecTypesL);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "--None--", "Bank Debt (First Lien) - Revolver", "Bank Debt (First Lien) - Term Loan A", "Bank Debt (First Lien) - Term Loan B", "Bank Debt (First Lien) - Synthetic LC Facility", "ABL/ABS Facility - First Lien", "LC Facility", "Bank Debt (Second Lien)", "ABL/ABS Facility - Second Lien", "Convertible Notes (Secured)", "Senior Secured Notes", "Capital Leases", "Other Secured Debt", "Mezzanine Debt", "Senior Notes (Unsecured)", "Senior Subordinated Notes (Unsecured)", "Substantially All Assets", "Convertible Notes (Unsecured)", "Other Unsecured Debt" };
            Console.WriteLine("1st:" + actualNamesAndDesc[0]);
            Console.WriteLine("1st:" + actualNamesAndDesc[1]);
            Console.WriteLine(actualNamesAndDesc[2]);
            Console.WriteLine(actualNamesAndDesc[3]);
            Console.WriteLine(actualNamesAndDesc[4]);
            Console.WriteLine(actualNamesAndDesc[5]);
            Console.WriteLine(actualNamesAndDesc[6]);
            Console.WriteLine(actualNamesAndDesc[7]);
            Console.WriteLine(actualNamesAndDesc[8]);
            Console.WriteLine(actualNamesAndDesc[9]);
            Console.WriteLine(actualNamesAndDesc[10]);
            Console.WriteLine(actualNamesAndDesc[11]);
            Console.WriteLine(actualNamesAndDesc[12]);

            bool isTrue = true;

            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            Thread.Sleep(4000);
            driver.FindElement(btnSecTypeL).Click();
            return isTrue;
        }
        //Validate save functionality of Add Distressed M&A Info Page
        public string ValidateSaveFunctionalityOfAddDistressedMAInfo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddDistressedL, 120);
            driver.FindElement(btnAddDistressedL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtAssetSoldL, 120);
            driver.FindElement(txtAssetSoldL).SendKeys("10");
            WebDriverWaits.WaitUntilEleVisible(driver, txtDateOfSoldL, 120);
            driver.FindElement(txtDateOfSoldL).SendKeys("Sep 13, 2023");
            driver.FindElement(txtMinOverbidL).SendKeys("10");
            driver.FindElement(txtIncreOverBidL).SendKeys("10");
            driver.FindElement(txtBreakUPFeeL).SendKeys("10");

            driver.FindElement(btnSaveAddDistressedL).Click();
            Thread.Sleep(6000);
            string row = driver.FindElement(rowAddDistressedL).Displayed.ToString();
            return row;
        }

        //Validate edit functionality of Add Distressed M&A Info Page
        public string ValidateEditFunctionalityOfAddDistressedMAInfo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditDistressed, 120);
            driver.FindElement(btnEditDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtAssetSoldL, 120);
            driver.FindElement(txtAssetSoldL).Clear();
            driver.FindElement(txtAssetSoldL).SendKeys("20");
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveAddDistressedL, 120);
            driver.FindElement(btnSaveAddDistressedL).Click();
            driver.FindElement(btnSaveAddDistressedL).Click();
            Thread.Sleep(6000);
            string value = driver.FindElement(valAssetSold).Text;
            return value;
        }


        //Validate cancel functionality of Add Distressed M&A Info Page
        public string ValidateCancelFunctionalityOfAddDistressedMAInfo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteDistressed, 120);
            driver.FindElement(btnDeleteDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(4000);
            try
            {
                string value = driver.FindElement(rowAddDistressedL).Displayed.ToString();
                return "Record is still displayed";
            }
            catch (Exception)
            {
                return "Record is not displayed";
            }
        }
        //Validate delete functionality of Add Distressed M&A Info Page
        public string ValidateDeleteFunctionalityOfAddDistressedMAInfo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteDistressed, 120);
            driver.FindElement(btnDeleteDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 120);
            driver.FindElement(btnOK).Click();
            Thread.Sleep(4000);
            try
            {
                string value = driver.FindElement(rowAddDistressedL).Displayed.ToString();
                return "Record is displayed";
            }
            catch (Exception)
            {
                return "Record is not displayed";
            }
        }

        //Validate cancel button
        public string ValidateCancelButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            string name = driver.FindElement(btnCancel).Text;
            return name;
        }

        //Validate save button
        public string ValidateSaveButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveAddHL, 120);
            string name = driver.FindElement(btnSaveAddHL).Text;
            driver.FindElement(btnSaveAddHL).Click();
            return name;
        }

        //Validate save and Key creditor button
        public string ValidateSaveAndKeyCreditorButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveAndKeyCred, 120);
            string name = driver.FindElement(btnSaveAndKeyCred).Text;
            return name;
        }



        //Validate the error message for Financing type
        public string ValidateErrorMessageForFinancingType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgFinTypeL, 120);
            string name = driver.FindElement(msgFinTypeL).Text;
            return name;
        }

        //Validate the error message for Security type
        public string ValidateErrorMessageForSecurityType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgSecTypeL, 120);
            string name = driver.FindElement(msgSecTypeL).Text;
            return name;
        }

        //Validate cancel button's functionality
        public string ValidateCancelButtonFunctionality()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            driver.FindElement(btnCancel).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabHLFinancingL, 120);
            string name = driver.FindElement(tabHLFinancingL).Text;
            return name;
        }

        //Validate save functionality of Add HL Financing Page
        public string ValidateSaveFunctionalityOfAddHLFinancing()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddHLFinL, 120);
            driver.FindElement(btnAddHLFinL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnFinTypeL, 120);
            driver.FindElement(btnFinTypeL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div[1]/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSecTypeL, 120);
            driver.FindElement(btnSecTypeL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div[3]/lightning-input-field/lightning-picklist/lightning-combobox/div/div[1]/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            driver.FindElement(txtFinAmtL).SendKeys("10");
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(6000);
            string row = driver.FindElement(rowAddHLFinL).Displayed.ToString();
            return row;
        }

        //Update the value of Financing Amount field
        public string UpdateTotalFinancingAmountValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtTotalFinAmt, 120);
            driver.FindElement(txtTotalFinAmt).SendKeys("15");
            driver.FindElement(btnSaveHLFin).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgSave, 120);
            string message = driver.FindElement(msgSave).Text;
            return message;
        }

        //Update the value of Financing Description field
        public string UpdateFinancingDescValue()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtFinDesc, 120);
            driver.FindElement(txtFinDesc).Clear();
            driver.FindElement(txtFinDesc).SendKeys("Testing");
            driver.FindElement(btnSaveHLFin).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgSave, 120);
            string message = driver.FindElement(msgSave).Text;
            return message;
        }

        //Validate validation for Other field
        public string ValidateErrorMessageOfOtherField()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddHLFinL, 120);
            driver.FindElement(btnAddHLFinL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnFinTypeL, 120);
            driver.FindElement(btnFinTypeL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div[1]/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSecTypeL, 120);
            driver.FindElement(btnSecTypeL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div[3]/lightning-input-field/lightning-picklist/lightning-combobox/div/div[1]/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            driver.FindElement(txtOtherL).SendKeys("Testing");
            driver.FindElement(txtFinAmtL).SendKeys("10");
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(4000);
            string message = driver.FindElement(msgOtherL).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            driver.FindElement(btnCancel).Click();
            return message;
        }

        //Get the value of Financing Type before update
        public string GetFinTypeBeforeUpdate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valFinTypeL, 120);
            string value = driver.FindElement(valFinTypeL).Text;
            return value;
        }

        //Validate edit functionality of Add HL Financing Page
        public string ValidateEditFunctionalityOfAddHLFinancing()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditDistressed, 120);
            driver.FindElement(btnEditDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnFinTypeL, 120);
            driver.FindElement(btnFinTypeL).Click();
            driver.FindElement(By.XPath("//div[1]/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[3]/span[2]/span")).Click();
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(6000);
            string row = driver.FindElement(valFinTypeL).Displayed.ToString();
            return row;
        }

        //Validate validation for Other field
        public string ValidateErrorMessageOfOtherFieldWhileEdit()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditDistressed, 120);
            driver.FindElement(btnEditDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtOtherL, 180);
            driver.FindElement(txtOtherL).SendKeys("Testing");
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(4000);
            string message = driver.FindElement(msgOtherL).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            driver.FindElement(btnCancel).Click();
            return message;
        }

        //Validate functionality for Other field
        public string ValidateFunctionalityOfOtherField()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditDistressed, 120);
            driver.FindElement(btnEditDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnFinTypeL, 120);
            driver.FindElement(btnFinTypeL).Click();
            Thread.Sleep(4000);
            var element = driver.FindElement((By.XPath("//div[1]/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[13]/span[2]/span")));
            Actions action = new Actions(driver);
            action.MoveToElement(element);
            action.Perform();
            driver.FindElement((By.XPath("//div[1]/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[13]/span[2]/span"))).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, txtOtherL, 180);
            driver.FindElement(txtOtherL).SendKeys("Testing");
            Thread.Sleep(4000);
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(valOtherL).Text;
            return value;
        }

        //Validate mandatory field validation for Financing Type
        public string ValidateMandatoryFieldValidationForFinType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditDistressed, 120);
            driver.FindElement(btnEditDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnFinTypeL, 120);
            driver.FindElement(btnFinTypeL).Click();
            Thread.Sleep(4000);
            var element = driver.FindElement((By.XPath("//div[1]/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[1]/span[2]/span")));
            Actions action = new Actions(driver);
            action.MoveToElement(element);
            action.Perform();
            driver.FindElement((By.XPath("//div[1]/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[1]/span[2]/span"))).Click();
            driver.FindElement(btnSecTypeL).Click();
            driver.FindElement(By.XPath("//div[3]/lightning-input-field/lightning-picklist/lightning-combobox/div/div[1]/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[1]/span[2]/span")).Click();

            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(msgFinType).Text;
            return value;
        }
        //Validate mandatory field validation for Security Type
        public string ValidateMandatoryFieldValidationForSecType()
        {
            string value = driver.FindElement(msgSecType).Text;
            return value;
        }

        //Validate Cancel functionality of Add HL Financing Page
        public string ValidateCancelFunctionalityOfHLFinancing()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteDistressed, 120);
            driver.FindElement(btnDeleteDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 130);
            driver.FindElement(btnCancel).Click();
            string row = driver.FindElement(valFinTypeL).Displayed.ToString();
            if (row.Equals("True"))
            {
                return "Record is not deleted";
            }
            else
            {
                return "Record is deleted";
            }
        }

        //Validate Delete functionality of Add HL Financing Page
        public string ValidateDeleteFunctionalityOfHLFinancing()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteDistressed, 120);
            driver.FindElement(btnDeleteDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 130);
            driver.FindElement(btnOK).Click();
            Thread.Sleep(4000);
            try
            {
                string row = driver.FindElement(valFinTypeL).Displayed.ToString();
                return "Record is not deleted";
            }
            catch (Exception)
            {
                return "Record is deleted";
            }

        }

        //Validate Pre Transaction Headers 
        public bool VerifyPreTransactionHeadersL()
        {
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(lblPreTransHeaders);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "Pre-Transaction Equity Holders", "Pre-Transaction Board Members", "Pre-Transaction Debt (MM)" };
            bool isTrue = true;

            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }

        //Validate Post Transaction Headers 
        public bool VerifyPostTransactionHeadersL()
        {
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(lblPreTransHeaders);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "Post-Transaction Equity Holders", "Post-Transaction Board Members", "Post-Transaction Debt (MM)" };
            bool isTrue = true;

            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }

        //Validate Post Transaction Opportunities label
        public string ValidateLabelPostTransOpp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostTransOppL, 120);
            string value = driver.FindElement(lblPostTransOppL).Text;
            return value;
        }


        //Validate HL Post Transaction Opportunities Values 
        public bool VerifyHLPostTransactionOppValuesL()
        {
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(lblPostTransOppValuesL);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "M&A - Buyside", "M&A - Sellside", "Restructuring", "Valuation", "Financing", "Fairness/Solvency" };
            bool isTrue = true;

            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }


        //Validate headers of Post Transaction Staff Roles table
        public bool VerifyPostTransactionStaffRolesHeadersL()
        {
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(lblPostTransStaffRolesHeaders);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "Name", "Relationship" };
            bool isTrue = true;

            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }

        //Validate headers of Post-Transaction Key External Contact

        public bool VerifyPostTransactionKeyContactL()
        {
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(lblPostTransKeyContacts);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "Name", "Relationship" };
            bool isTrue = true;

            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }

        //Validate Post Transaction Opportunities Notes
        public string ValidateLabelPostTransOppNotes()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostTransOppNotesL, 120);
            string value = driver.FindElement(lblPostTransOppNotesL).Text;
            return value;
        }

        //Validate Post Transaction Staff Roles
        public string ValidateLabelPostTransStaffRoles()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostTransStaffRolesL, 120);
            string value = driver.FindElement(lblPostTransStaffRolesL).Text;
            return value;
        }

        //Validate Post-Transaction Key External Contact
        public string ValidateLabelPostTransKeyExternalContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostTransKeyExtContactL, 120);
            string value = driver.FindElement(lblPostTransKeyExtContactL).Text;
            return value;
        }

        //Validate Contact internal field on Add Staff role
        public string ValidateContactField()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddStaffRoleL, 190);
            driver.FindElement(btnAddStaffRoleL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblContactL, 120);
            string value = driver.FindElement(lblContactL).Text;
            return value;
        }

        //Validate Contact internal field on Add Key External Contact
        public string ValidateContactFieldOnAddKeyExternalContact()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddKeyExtExternalL, 190);
            driver.FindElement(btnAddKeyExtExternalL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblContactExtL, 120);
            string value = driver.FindElement(lblContactExtL).Text;
            return value;
        }

        //Validate report on clicking Post-Transaction Opportunity Report
        public string ValidateReportAfterClickingPostTransOppReport()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnPostTransOppReport, 190);
            driver.FindElement(btnPostTransOppReport).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(4000);
            string value = driver.FindElement(By.XPath("//h1")).Text;
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            return value;
        }

        //Validate page on clicking Submit Engagement Summary
        public string ValidatePageAfterClickingSubmitEngSummary()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitEngSummary, 190);
            driver.FindElement(btnSubmitEngSummary).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(4000);
            string value = driver.FindElement(lblSendEmailL).Text;
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            return value;
        }

        //Validate message on clicking Send BTP Email
        public string ValidateMessageAfterClickingSendBTPEmail()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSendBTPEmail, 190);
            driver.FindElement(btnSendBTPEmail).Click();
            Thread.Sleep(3000);
            driver.FindElement(tabEngDetailsL).Click();
            Thread.Sleep(3000);
            driver.FindElement(tabClosingInfo).Click();
            string value = driver.FindElement(valSendBTPEmail).Text;

            return value;
        }

        //Validate Role Field
        public string ValidateRoleField()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblRole, 120);
            string value = driver.FindElement(lblRole).Text;
            return value;
        }

        //Validate Post Transaction Grid Headers 
        public bool VerifyPostTransactionGridL()
        {
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(lblPostTransGrid);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "Security Type", "Key Creditors", "Constituent Debt", "Facility Balance (MM)", "Maturity Date", "Interest", "OID Percent", "Amortization", "Call Provisions / Prepayment Premiums", "Mandatory Prepayments / ECF Sweep", "Covenants", "Fees & Expenses" };
            bool isTrue = true;

            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }

        //Validate Pre Transaction Grid Headers 
        public bool VerifyPreTransactionGridL()
        {
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(lblPreTransGrid);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "Security Type", "Key Creditors", "Constituent Debt", "Facility Balance (MM)", "Maturity Date", "Interest", "OID Percent", "Amortization", "Call Provisions / Prepayment Premiums", "Mandatory Prepayments / ECF Sweep", "Covenants", "Fees & Expenses" };
            bool isTrue = true;

            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }

        public string ValidateLabelPreReOrg()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPreReorg, 120);
            string value = driver.FindElement(lblPreReorg).Text;
            return value;
        }

        public string ValidateLabelPreReOrgTotalDebt()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPreReorgTotal, 120);
            string value = driver.FindElement(lblPreReorgTotal).Text;
            return value;
        }

        public string ValidateLabelPostReStr()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostRestr, 120);
            string value = driver.FindElement(lblPostRestr).Text;
            return value;
        }

        public string ValidateLabelPostReStrCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostRestrComp, 120);
            string value = driver.FindElement(lblPostRestrComp).Text;
            return value;
        }

        public string ValidateLabelClosingStock()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblClosingStockL, 120);
            string value = driver.FindElement(lblClosingStockL).Text;
            return value;
        }

        //Validate Client/Subject
        public string VerifyClientSubjectFieldL()
        {
            driver.FindElement(btnAddEquityHolderL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblClientSubL, 180);
            string name = driver.FindElement(lblClientSubL).Text;
            return name;
        }

        //Validate Percent Ownership
        public string VerifyPercentOwnershipFieldL()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, lblPercentOwnershipL, 180);
            string name = driver.FindElement(lblPercentOwnershipL).Text;
            return name;
        }

        //Validate cancel button's functionality
        public string ValidateCancelButtonFunctionalityOfPreTrans()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            driver.FindElement(btnCancel).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabPreTransL, 120);
            string name = driver.FindElement(tabPreTransL).Text;
            return name;
        }

        //Validate cancel button's functionality
        public string ValidateCancelButtonFunctionalityOfPostTrans()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            driver.FindElement(btnCancel).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabPostTransL, 120);
            string name = driver.FindElement(tabPostTransL).Text;
            return name;
        }

        //Validate cancel button's functionality
        public string ValidateCancelButtonFunctionalityOfHLPostTrans()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            driver.FindElement(btnCancel).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblPostTransOppL, 120);
            string name = driver.FindElement(lblPostTransOppL).Text;
            return name;
        }

        //Validate save button's functionality
        public string ValidateSaveButtonFunctionalityOfHLPostTrans()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddStaffRoleL, 120);
            driver.FindElement(btnAddStaffRoleL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactHLPostL, 120);
            driver.FindElement(txtContactHLPostL).SendKeys("Sonika Goyal");
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContactHLPostL, 150);
            driver.FindElement(lnkContactHLPostL).Click();
            driver.FindElement(btnRoleL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valRoleL, 160);
            driver.FindElement(valRoleL).Click();
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(5000);
            string row = driver.FindElement(rowStaffRoleL).Displayed.ToString();
            return row;
        }

        //Validate save button's functionality
        public string ValidateSaveButtonFunctionalityOfKeyExtContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddKeyExtExternalL, 120);
            driver.FindElement(btnAddKeyExtExternalL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactHLPostL, 120);
            driver.FindElement(txtContactHLPostL).SendKeys("Chris so");
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContactHLPostL, 150);
            driver.FindElement(lnkContactHLPostL).Click();
            driver.FindElement(btnRoleL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valRoleL, 160);
            driver.FindElement(valRoleL).Click();
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(5000);
            string row = driver.FindElement(rowKeyExtL).Displayed.ToString();
            return row;
        }

        //Get the value of role of added contact
        public string GetRoleOfAddedContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedRoleL, 160);
            string value = driver.FindElement(valAddedRoleL).Text;
            return value;
        }
        //Validate error message if user tries to add already-added Contact.
        public string ValidateErrorMessageWhileAddingExistingStaffContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddStaffRoleL, 120);
            driver.FindElement(btnAddStaffRoleL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactHLPostL, 120);
            driver.FindElement(txtContactHLPostL).SendKeys("Sonika Goyal");
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContactHLPostL, 150);
            driver.FindElement(lnkContactHLPostL).Click();
            driver.FindElement(btnRoleL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valRoleL, 160);
            driver.FindElement(valRoleL).Click();
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(5000);
            string row = driver.FindElement(msgDupContactL).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngDetailsL, 100);
            driver.FindElement(tabEngDetailsL).Click();
            driver.Navigate().Refresh();
            return row;
        }

        //Validate error message if user tries to add already-added Contact.
        public string ValidateErrorMessageWhileAddingExistingKeyContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddKeyExtExternalL, 120);
            driver.FindElement(btnAddKeyExtExternalL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactHLPostL, 120);
            driver.FindElement(txtContactHLPostL).SendKeys("Chris so");
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContactHLPostL, 150);
            driver.FindElement(lnkContactHLPostL).Click();
            driver.FindElement(btnRoleL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valRoleL, 160);
            driver.FindElement(valRoleL).Click();
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(5000);
            string row = driver.FindElement(msgDupKeyContactL).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            driver.FindElement(btnCancel).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngDetailsL, 100);
            driver.FindElement(tabEngDetailsL).Click();
            driver.Navigate().Refresh();
            Thread.Sleep(4000);
            return row;
        }

        //Validate cancel button's functionality
        public string ValidateSaveFunctionalityOfHLPostTransOpp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAvailableL, 120);
            driver.FindElement(valAvailableL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnChosenL, 130);
            driver.FindElement(btnChosenL).Click();
            driver.FindElement(btnSaveHLPost).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valChosenL, 140);
            driver.FindElement(valChosenL).Click();
            string value = driver.FindElement(valChosenL).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, btnAvailableL, 150);
            driver.FindElement(btnAvailableL).Click();
            driver.FindElement(btnSaveHLPost).Click();
            return value;
        }



        //Validate save functionality of Add Equity Holder Page
        public string ValidateSaveFunctionalityOfAddEquityHolder()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddEquityHolderL, 120);
            driver.FindElement(btnAddEquityHolderL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnClientSubL, 120);
            driver.FindElement(btnClientSubL).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnClientSubL).SendKeys("Dina");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div/ul[@role='group']/li[2]")).Click();
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(6000);
            string row = driver.FindElement(rowAddEquityHolderL).Displayed.ToString();
            return row;
        }

        //Validate save functionality of Add Equity Holder Page
        public string ValidateSaveFunctionalityOfAddEquityHolderPostTrans()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddEquityHolderL, 120);
            driver.FindElement(btnAddEquityHolderL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnClientSubL, 120);
            driver.FindElement(btnClientSubL).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnClientSubL).SendKeys("Dina");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div/ul[@role='group']/li[2]")).Click();
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(6000);
            string row = driver.FindElement(rowAddEquityHolderPostL).Displayed.ToString();
            return row;
        }

        //Validate save functionality of Add Equity Holder Page
        public string ValidateSaveFunctionalityOfPostTransOppNotes()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,-400)");
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtOppNoteL, 120);
            driver.FindElement(txtOppNoteL).Clear();
            driver.FindElement(txtOppNoteL).SendKeys("Testing");
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveHLPost, 120);
            driver.FindElement(btnSaveHLPost).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessHLPost, 100);
            string message = driver.FindElement(msgSuccessHLPost).Text;
            return message;

        }


        //Validate when same client is added in Add Equity Holder Page
        public string ValidateIfSameClientIsSelectedInAddEquityHolder()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddEquityHolderL, 120);
            driver.FindElement(btnAddEquityHolderL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnClientSubL, 120);
            driver.FindElement(btnClientSubL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div/ul[@aria-label='Recent Companies']/li[2]")).Click();
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(6000);
            string message = driver.FindElement(msgDupClientL).Text;
            return message;
        }

        //Validate save functionality of Add Debt Structure  Page
        public string ValidateSaveFunctionalityOfAddDebtStructure()
        {
            driver.FindElement(btnAddDebtStrL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSecurityTypeL, 120);
            driver.FindElement(btnSecurityTypeL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div/lightning-base-combobox-item[2]/span[2]/span")).Click();

            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(6000);
            string row = driver.FindElement(rowAddDebtStructureL).Displayed.ToString();
            return row;
        }

        //Validate save functionality of Add Debt Structure  Page
        public string ValidateSaveFunctionalityOfAddDebtStructurePostTrans()
        {
            driver.FindElement(btnAddDebtStrL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSecurityTypeL, 120);
            driver.FindElement(btnSecurityTypeL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div/lightning-base-combobox-item[2]/span[2]/span")).Click();

            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(6000);
            string row = driver.FindElement(rowAddDebtStructurePostL).Displayed.ToString();
            return row;
        }

        //Validate save functionality of Add Debt Structure Page by adding all values
        public bool ValidateSaveFunctionalityOfAddDebtStructureByAddingAllValues()
        {
            driver.FindElement(btnAddDebtStrL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSecurityTypeL, 120);
            driver.FindElement(btnSecurityTypeL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div/lightning-base-combobox-item[2]/span[2]/span")).Click();
            driver.FindElement(txtInterestL).SendKeys("Testing");
            driver.FindElement(txtAmoritizationL).SendKeys("10");
            driver.FindElement(txtMandatoryL).SendKeys("10");
            driver.FindElement(txtFeesL).SendKeys("10");

            driver.FindElement(txtMaturityL).SendKeys("Oct 2, 2023");
            driver.FindElement(txtOIDL).SendKeys("5");
            driver.FindElement(txtCallProvisionsL).SendKeys("5");
            driver.FindElement(txtCovenantsL).SendKeys("Testing");
            driver.FindElement(txtFacilityL).SendKeys("5");
            driver.FindElement(btnSaveAndKeyCred).Click();
            Thread.Sleep(6000);

            driver.FindElement(txtCompaniesL).SendKeys("adobe oil & gas");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//lightning-base-combobox/div/div/div[2]/ul/li/lightning-base-combobox-item")).Click();
            driver.FindElement(txtLoanAmountL).SendKeys("10");
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(5000);
            bool row = driver.FindElement(rowAddKeyCredL).Displayed;
            return row;
        }

        //Validate save functionality of Add Debt Structure Page by adding all values
        public string ValidateSaveFunctionalityOfAddDebtStructureByAddingAllValuesWithoutClickingAddDebt()
        {
            // driver.FindElement(btnAddDebtStrL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSecurityTypeL, 120);
            driver.FindElement(btnSecurityTypeL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div/lightning-base-combobox-item[2]/span[2]/span")).Click();
            driver.FindElement(txtInterestL).SendKeys("Testing");
            driver.FindElement(txtAmoritizationL).SendKeys("10");
            driver.FindElement(txtMandatoryL).SendKeys("10");
            driver.FindElement(txtFeesL).SendKeys("10");

            driver.FindElement(txtMaturityL).SendKeys("Oct 2, 2023");
            driver.FindElement(txtOIDL).SendKeys("5");
            driver.FindElement(txtCallProvisionsL).SendKeys("5");
            driver.FindElement(txtCovenantsL).SendKeys("Testing");
            driver.FindElement(txtFacilityL).SendKeys("5");
            driver.FindElement(btnSaveAndKeyCred).Click();
            Thread.Sleep(6000);

            driver.FindElement(txtCompaniesL).SendKeys("adobe oil & gas");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//lightning-base-combobox/div/div/div[2]/ul/li/lightning-base-combobox-item")).Click();
            driver.FindElement(txtLoanAmountL).SendKeys("10");
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(5000);
            bool row = driver.FindElement(rowAddKeyCredL).Displayed;
            return "Saved details";
        }

        //Validate edit functionality of Debt Structure 
        public string ValidateEditFunctionalityOfAddedDebtStructure()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSecurityTypeL, 120);
            driver.FindElement(btnSecurityTypeL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div/lightning-base-combobox-item[4]/span[2]/span")).Click();
            driver.FindElement(txtInterestL).SendKeys("Updated Value");
            driver.FindElement(txtAmoritizationL).SendKeys("5");
            driver.FindElement(txtMandatoryL).SendKeys("5");
            driver.FindElement(txtFeesL).SendKeys("5");

            driver.FindElement(txtMaturityL).SendKeys("Oct 2, 2023");
            driver.FindElement(txtOIDL).SendKeys("5");
            driver.FindElement(txtCallProvisionsL).SendKeys("5");
            driver.FindElement(txtCovenantsL).SendKeys("Testing");
            driver.FindElement(txtFacilityL).SendKeys("5");

            driver.FindElement(txtLoanAmountL).Clear();
            driver.FindElement(txtLoanAmountL).SendKeys("5");
            driver.FindElement(btnSaveKeyCredL).Click();
            Thread.Sleep(5000);
            string row = driver.FindElement(valSecurityTypeL).Text;
            return row;
        }

        //Validate edit functionality of Debt Structure 
        public string ValidateEditFunctionalityOfAddedDebtStructurePost()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSecurityTypeL, 120);
            driver.FindElement(btnSecurityTypeL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div/lightning-base-combobox-item[4]/span[2]/span")).Click();
            driver.FindElement(txtInterestL).SendKeys("Updated Value");
            driver.FindElement(txtAmoritizationL).SendKeys("5");
            driver.FindElement(txtMandatoryL).SendKeys("5");
            driver.FindElement(txtFeesL).SendKeys("5");

            driver.FindElement(txtMaturityL).SendKeys("Oct 2, 2023");
            driver.FindElement(txtOIDL).SendKeys("5");
            driver.FindElement(txtCallProvisionsL).SendKeys("5");
            driver.FindElement(txtCovenantsL).SendKeys("Testing");
            driver.FindElement(txtFacilityL).SendKeys("5");

            driver.FindElement(txtLoanAmountL).Clear();
            driver.FindElement(txtLoanAmountL).SendKeys("5");
            driver.FindElement(btnSaveKeyCredL).Click();
            Thread.Sleep(5000);
            string row = driver.FindElement(valSecurityTypePostL).Text;
            return row;
        }
        //Validate added debt structure 
        public bool ValidateDebtStrcWithKeyCred()
        {
            Thread.Sleep(5000);
            driver.FindElement(btnSaveKeyCredL).Click();
            Thread.Sleep(4000);
            bool row = driver.FindElement(rowAddDebtStrcutureL2nd).Displayed;
            return row;
        }

        //Validate added debt structure 
        public bool ValidateDebtStrcWithKeyCredPost()
        {
            Thread.Sleep(5000);
            driver.FindElement(btnSaveKeyCredL).Click();
            Thread.Sleep(6000);
            bool row = driver.FindElement(rowAddDebtStrcutureL2ndPost).Displayed;
            return row;
        }




        //Validate edit functionality of Add Equity Holder Page
        public string ValidateEditFunctionalityOfAddEquityHolder()
        {
            //driver.FindElement(btnCancel).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditDistressed, 120);
            driver.FindElement(btnEditDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtPerOwnershipL, 120);
            driver.FindElement(txtPerOwnershipL).SendKeys("10");
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(6000);
            string value = driver.FindElement(valPerOwnerL).Text;
            return value;
        }

        //Validate edit functionality of Add Equity Holder Page
        public bool ValidateEditFunctionalityOfDebtStruc()
        {
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditDebtL, 120);
            driver.FindElement(btnEditDebtL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnKeyCredL, 120);
            driver.FindElement(btnKeyCredL).Click();
            driver.FindElement(txtCompaniesL).SendKeys("Dina's Test Company");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//lightning-base-combobox/div/div/div[2]/ul/li/lightning-base-combobox-item")).Click();
            driver.FindElement(txtLoanAmountL).SendKeys("10");
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(5000);
            bool row = driver.FindElement(rowAddKeyCredL).Displayed;
            Thread.Sleep(5000);
            driver.FindElement(btnSaveKeyCredL).Click();
            return row;

        }
        //Validate edit functionality of Add Equity Holder Page
        public bool ValidateEditFunctionalityOfDebtStrucPost()
        {
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditDebtLPost, 120);
            driver.FindElement(btnEditDebtLPost).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnKeyCredL, 120);
            driver.FindElement(btnKeyCredL).Click();
            driver.FindElement(txtCompaniesL).SendKeys("Dina's Test Company");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//lightning-base-combobox/div/div/div[2]/ul/li/lightning-base-combobox-item")).Click();
            driver.FindElement(txtLoanAmountL).SendKeys("10");
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(5000);
            bool row = driver.FindElement(rowAddKeyCredL).Displayed;
            Thread.Sleep(5000);
            driver.FindElement(btnSaveKeyCredL).Click();
            return row;

        }
        //Validate duplicate error message while adding same company in Key Creditor
        public string ValidateErrorMessageWhileAddingSameClientSubjectInKeyCred()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditDebtL, 120);
            driver.FindElement(btnEditDebtL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnKeyCredL, 120);
            driver.FindElement(btnKeyCredL).Click();
            driver.FindElement(txtCompaniesL).SendKeys("Dina's Test Company");
            Thread.Sleep(8000);
            driver.FindElement(By.XPath("//lightning-base-combobox/div/div/div[2]/ul/li/lightning-base-combobox-item")).Click();
            driver.FindElement(txtLoanAmountL).SendKeys("10");
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(5000);
            string row = driver.FindElement(msgDupClientL).Text;
            driver.FindElement(btnCancel).Click();
            return row;

        }
        //Validate duplicate error message while adding same company in Key Creditor
        public string ValidateErrorMessageWhileAddingSameClientSubjectInKeyCredPost()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditDebtLPost, 150);
            driver.FindElement(btnEditDebtLPost).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnKeyCredL, 140);
            driver.FindElement(btnKeyCredL).Click();
            driver.FindElement(txtCompaniesL).SendKeys("Dina's Test Company");
            Thread.Sleep(8000);
            driver.FindElement(By.XPath("//lightning-base-combobox/div/div/div[2]/ul/li/lightning-base-combobox-item")).Click();
            driver.FindElement(txtLoanAmountL).SendKeys("10");
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(5000);
            string row = driver.FindElement(msgDupClientL).Text;
            driver.FindElement(btnCancel).Click();
            return row;

        }

        //Validate mandatory error message while adding same company in Key Creditor without entering details
        public string ValidateErrorMessageWhileSavingKeyCredWithoutClientSubject()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnKeyCredL, 120);
            driver.FindElement(btnKeyCredL).Click();
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(5000);
            string row = driver.FindElement(msgMandatoryKeyCredL).Text;
            driver.FindElement(btnCancel).Click();
            return row;
        }

        //Validate mandatory error message after removing mandatory fields of Debt Structure
        public string ValidateErrorMessageAfterRemovingMandatoryFieldsOfDebtStructure()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 130);
            driver.FindElement(btnCancel).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit2ndDebtL, 170);
            driver.FindElement(btnEdit2ndDebtL).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnSecurityTypeL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div/lightning-base-combobox-item[1]/span[2]/span")).Click();
            driver.FindElement(btnSaveKeyCredL).Click();
            Thread.Sleep(5000);
            string row = driver.FindElement(msgCurrencyFinancials).Text;
            driver.FindElement(btnCancel).Click();
            return row;
        }

        //Validate mandatory error message after removing mandatory fields of Debt Structure
        public string ValidateErrorMessageAfterRemovingMandatoryFieldsOfDebtStructurePost()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 130);
            driver.FindElement(btnCancel).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit2ndDebtLPost, 170);
            driver.FindElement(btnEdit2ndDebtLPost).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnSecurityTypeL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div/lightning-base-combobox-item[1]/span[2]/span")).Click();
            driver.FindElement(btnSaveKeyCredL).Click();
            Thread.Sleep(5000);
            string row = driver.FindElement(msgCurrencyFinancials).Text;
            driver.FindElement(btnCancel).Click();
            return row;
        }

        //Validate cancel functionality of Key Creditor in Debt Structure
        public bool ValidateCancelFunctionalityOfKeyCred()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditDebtL, 120);
            driver.FindElement(btnEditDebtL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteDistressed, 130);
            driver.FindElement(btnDeleteDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelKeyCredL, 140);
            driver.FindElement(btnCancelKeyCredL).Click();
            Thread.Sleep(5000);
            bool row = driver.FindElement(rowAddKeyCredL).Displayed;
            return row;

        }
        //Validate cancel functionality of Key Creditor in Debt Structure
        public bool ValidateCancelFunctionalityOfKeyCredPost()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditDebtLPost, 120);
            driver.FindElement(btnEditDebtLPost).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteDistressed, 130);
            driver.FindElement(btnDeleteDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelKeyCredL, 140);
            driver.FindElement(btnCancelKeyCredL).Click();
            Thread.Sleep(5000);
            bool row = driver.FindElement(rowAddKeyCredL).Displayed;
            return row;

        }

        //Validate delete functionality of Key Creditor in Debt Structure
        public string ValidateDeleteFunctionalityOfKeyCred()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteDistressed, 130);
            driver.FindElement(btnDeleteDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 140);
            driver.FindElement(btnOK).Click();
            Thread.Sleep(8000);
            try
            {
                string row = driver.FindElement(rowAddKeyCredL).Displayed.ToString();
                return row;
            }
            catch (Exception e)
            {
                return "Row is deleted";
            }

        }
        //Validate hyperlink of Added Equity Holder 
        public string ValidateHyperlinkOfAddedEquityHolder()
        {
            Thread.Sleep(5000);
            string value = driver.FindElement(lnkEquityHolder).GetAttribute("target");
            return value;
        }

        //Validate hyperlink of Added Equity Holder in Post Trans
        public string ValidateHyperlinkOfAddedEquityHolderPost()
        {
            Thread.Sleep(5000);
            string value = driver.FindElement(lnkEquityHolderPost).GetAttribute("target");
            return value;
        }

        //Get the value of Percent Ownership  before update
        public string GetPerOwnershipBeforeUpdate()
        {
            Thread.Sleep(4000);
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(6000);
            string value = driver.FindElement(valPerOwnerL).Text;
            return value;
        }

        //Validate the error message for Financing type
        public string ValidateErrorMessageForClientSubject()
        {
            driver.FindElement(btnSaveAddHL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgClientSubL, 130);
            string name = driver.FindElement(msgClientSubL).Text;
            return name;
        }

        //Validate the error message for Contact
        public string ValidateErrorMessageForContactInt()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgContactL, 130);
            string name = driver.FindElement(msgContactL).Text;
            return name;
        }

        //Validate the error message for Contact
        public string ValidateErrorMessageForContactExt()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgContactExtL, 130);
            string name = driver.FindElement(msgContactExtL).Text;
            return name;
        }

        //Validate the error message for Role
        public string ValidateErrorMessageForRole()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgRoleL, 130);
            string name = driver.FindElement(msgRoleL).Text;
            return name;
        }
        //Validate Cancel functionality of Add Equity Holder Page
        public string ValidateCancelFunctionalityOfEquityHolder()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteDistressed, 120);
            driver.FindElement(btnDeleteDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 130);
            driver.FindElement(btnCancel).Click();
            string row = driver.FindElement(valFinTypeL).Displayed.ToString();
            if (row.Equals("True"))
            {
                return "Record is not deleted";
            }
            else
            {
                return "Record is deleted";
            }
        }

        //Validate Cancel functionality of Add Board Member Page
        public string ValidateCancelFunctionalityOfAddedStaffRole()
        {

            driver.FindElement(tabEngSummary).Click();
            Thread.Sleep(4000);
            driver.FindElement(tabHLPostTransL).Click();
            //Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteRole, 120);
            driver.FindElement(btnDeleteRole).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 130);
            driver.FindElement(btnCancel).Click();
            string row = driver.FindElement(rowStaffRoleL).Displayed.ToString();
            if (row.Equals("True"))
            {
                return "Record is not deleted";
            }
            else
            {
                return "Record is deleted";
            }
        }

        //Validate Cancel functionality of Key Contact
        public string ValidateCancelFunctionalityOfAddedKeyContact()
        {
            driver.FindElement(tabEngSummary).Click();
            Thread.Sleep(4000);
            driver.FindElement(tabHLPostTransL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteRole, 120);
            driver.FindElement(btnDeleteRole).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 130);
            driver.FindElement(btnCancel).Click();
            string row = driver.FindElement(rowKeyContactL).Displayed.ToString();
            if (row.Equals("True"))
            {
                return "Record is not deleted";
            }
            else
            {
                return "Record is deleted";
            }
        }

        //Validate Cancel functionality of Add Staff Role Page
        public string ValidateCancelFunctionalityOfAddBoardMember()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteBoard, 120);
            driver.FindElement(btnDeleteBoard).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 130);
            driver.FindElement(btnCancel).Click();
            string row = driver.FindElement(rowAddBoardMember2ndL).Displayed.ToString();
            if (row.Equals("True"))
            {
                return "Record is not deleted";
            }
            else
            {
                return "Record is deleted";
            }
        }
        //Validate Cancel functionality of Add Board Member Page
        public string ValidateCancelFunctionalityOfAddBoardMemberPostTrans()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteBoard, 120);
            driver.FindElement(btnDeleteBoard).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 130);
            driver.FindElement(btnCancel).Click();
            string row = driver.FindElement(rowAddBoardMember2ndPostL).Displayed.ToString();
            if (row.Equals("True"))
            {
                return "Record is not deleted";
            }
            else
            {
                return "Record is deleted";
            }
        }
        //Validate Cancel functionality of Debt Structure
        public string ValidateCancelFunctionalityOfDebtStructure()
        {
            Thread.Sleep(4000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteDebtL, 120);
            driver.FindElement(btnDeleteDebtL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 140);
            driver.FindElement(btnCancel).Click();
            string row = driver.FindElement(rowAddDebtStrcutureL2nd).Displayed.ToString();
            if (row.Equals("True"))
            {
                return "Record is not deleted";
            }
            else
            {
                return "Record is deleted";
            }
        }

        //Validate Cancel functionality of Debt Structure
        public string ValidateCancelFunctionalityOfDebtStructurePost()
        {
            Thread.Sleep(4000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteDebtLPost, 120);
            driver.FindElement(btnDeleteDebtLPost).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 140);
            driver.FindElement(btnCancel).Click();
            string row = driver.FindElement(rowAddDebtStrcutureL2ndPost).Displayed.ToString();
            if (row.Equals("True"))
            {
                return "Record is not deleted";
            }
            else
            {
                return "Record is deleted";
            }
        }
        //Validate Delete functionality of Added Equity Holder Page
        public string ValidateDeleteFunctionalityOfEquityHolder()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteDistressed, 120);
            driver.FindElement(btnDeleteDistressed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 130);
            driver.FindElement(btnOK).Click();
            Thread.Sleep(4000);
            try
            {
                string row = driver.FindElement(valEquityHolder).Displayed.ToString();
                return "Record is not deleted";
            }
            catch (Exception)
            {
                return "Record is deleted";
            }

        }

        //Validate Delete functionality of Added Board Member Page
        public string ValidateDeleteFunctionalityOfBoardMember()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteBoard, 120);
            driver.FindElement(btnDeleteBoard).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 130);
            driver.FindElement(btnOK).Click();
            Thread.Sleep(4000);
            try
            {
                string row = driver.FindElement(rowAddBoardMember2ndL).Displayed.ToString();
                return "Record is not deleted";
            }
            catch (Exception)
            {
                return "Record is deleted";
            }

        }

        //Validate Delete functionality of Added Board Member Page
        public string ValidateDeleteFunctionalityOfBoardMemberPostTrans()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteBoard, 120);
            driver.FindElement(btnDeleteBoard).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 130);
            driver.FindElement(btnOK).Click();
            Thread.Sleep(4000);
            try
            {
                string row = driver.FindElement(rowAddBoardMember2ndPostL).Displayed.ToString();
                return "Record is not deleted";
            }
            catch (Exception)
            {
                return "Record is deleted";
            }

        }
        //Validate Delete functionality of Added Debt Structure Page
        public string ValidateDeleteFunctionalityOfDebtStructure()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteDebtL, 120);
            driver.FindElement(btnDeleteDebtL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 130);
            driver.FindElement(btnOK).Click();
            Thread.Sleep(4000);
            try
            {
                string row = driver.FindElement(rowAddDebtStrcutureL2nd).Displayed.ToString();
                return "Record is not deleted";
            }
            catch (Exception)
            {
                return "Record is deleted";
            }

        }

        //Validate Delete functionality of Added Debt Structure Page
        public string ValidateDeleteFunctionalityOfDebtStructurePost()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteDebtLPost, 120);
            driver.FindElement(btnDeleteDebtLPost).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 130);
            driver.FindElement(btnOK).Click();
            Thread.Sleep(4000);
            try
            {
                string row = driver.FindElement(rowAddDebtStrcutureL2ndPost).Displayed.ToString();
                return "Record is not deleted";
            }
            catch (Exception)
            {
                return "Record is deleted";
            }

        }
        //Validate Delete functionality of Added Staff Role
        public string ValidateDeleteFunctionalityOfStaffRole()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteRole, 120);
            driver.FindElement(btnDeleteRole).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 130);
            driver.FindElement(btnOK).Click();
            Thread.Sleep(4000);
            try
            {
                string row = driver.FindElement(rowStaffRoleL).Displayed.ToString();
                return "Record is not deleted";
            }
            catch (Exception)
            {
                return "Record is deleted";
            }

        }

        //Validate Delete functionality of Added Key Contact
        public string ValidateDeleteFunctionalityOfKeyContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteRole, 120);
            driver.FindElement(btnDeleteRole).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 130);
            driver.FindElement(btnOK).Click();
            Thread.Sleep(4000);
            try
            {
                string row = driver.FindElement(rowKeyContactL).Displayed.ToString();
                return "Record is not deleted";
            }
            catch (Exception)
            {
                return "Record is deleted";
            }

        }
        //Validate Contact field on click of Add Board Member
        public string VerifyContactsFieldL()
        {
            driver.FindElement(btnAddBoardMemberL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblContacts, 180);
            string name = driver.FindElement(lblContacts).Text;
            return name;
        }

        //Validate Maturity Date on Add Debt Structure 
        public string VerifyMaturityDateFieldL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblMaturityDate, 180);
            string name = driver.FindElement(lblMaturityDate).Text;
            return name;
        }


        //Validate Security Type on Add Debt Structure 
        public string VerifySecurityTypeFieldOfAddDebtL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblSecurityTypeL, 180);
            string name = driver.FindElement(lblSecurityTypeL).Text;
            return name;
        }

        //Validate Currency on Add Debt Structure 
        public string VerifyCurrencyFieldOfAddDebtL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCurrencyAddDebtL, 180);
            string name = driver.FindElement(lblCurrencyAddDebtL).Text;
            //driver.FindElement(btnCurrencyL).Click();                
            return name;
        }

        //Validate the error message for Contacts
        public string ValidateErrorMessageForContact()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgContactsL, 130);
            string name = driver.FindElement(msgContactsL).Text;
            return name;
        }

        //Validate the error message for Secutiry Type
        public string ValidateErrorMessageForSecuirtyType()
        {
            Thread.Sleep(6000);
            driver.FindElement(btnSaveAddHL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSecurityTypeL, 130);
            string name = driver.FindElement(msgSecurityTypeL).Text;
            return name;
        }

        //Validate that inactive contact can not be searched
        public string ValidateSearchWithInactiveContact()
        {
            driver.FindElement(btnAddBoardMemberL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactL, 130);
            driver.FindElement(txtContactL).SendKeys("Sue Chu");
            Thread.Sleep(6000);
            string value = driver.FindElement(comboContactL).Text;
            return value;
        }

        //Validate save functionality of Add Board Member Page
        public string ValidateSaveFunctionalityOfAddBoardMemberWithHLRel()
        {
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnAddBoardMemberL).Click();
            driver.FindElement(txtContactL).SendKeys("Anne Poetschke");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div/ul/li/lightning-base-combobox-item/span[2]/span")).Click();
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(6000);
            string row = driver.FindElement(rowAddBoardMemberL).Displayed.ToString();
            return row;
        }

        //Validate save functionality of Add Board Member Page
        public string ValidateSaveFunctionalityOfAddBoardMemberWithHLRelPostTrans()
        {
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnAddBoardMemberL).Click();
            driver.FindElement(txtContactL).SendKeys("Anne Poetschke");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div/ul/li/lightning-base-combobox-item/span[2]/span")).Click();
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(6000);
            string row = driver.FindElement(rowAddBoardMemberPostL).Displayed.ToString();
            return row;
        }

        public string ValidateSaveFunctionalityOfAddBoardMemberWithoutHLRel()
        {
            Thread.Sleep(4000);
            driver.FindElement(btnAddBoardMemberL).Click();
            driver.FindElement(txtContactL).SendKeys("Sonika Mathur");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div/ul/li/lightning-base-combobox-item/span[2]/span")).Click();
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(6000);
            string row = driver.FindElement(rowAddBoardMember2ndL).Displayed.ToString();
            return row;
        }

        public string ValidateSaveFunctionalityOfAddBoardMemberWithoutHLRelPostTrans()
        {
            Thread.Sleep(4000);
            driver.FindElement(btnAddBoardMemberL).Click();
            driver.FindElement(txtContactL).SendKeys("Sonika Mathur");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div/ul/li/lightning-base-combobox-item/span[2]/span")).Click();
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(6000);
            string row = driver.FindElement(rowAddBoardMember2ndPostL).Displayed.ToString();
            return row;
        }

        //Validate "Has HL Relationship" checkbox is checked 
        public string Validate1stHLRelationshipCheckbox()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chk1stHLRel, 130);
            string value = driver.FindElement(chk1stHLRel).GetAttribute("checked");
            return value;

        }

        //Validate "Has HL Relationship" checkbox is checked 
        public string Validate2ndHLRelationshipCheckbox()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chk1stHLRel, 130);
            string value = driver.FindElement(chk1stHLRel).GetAttribute("checked");
            return value;

        }
        //Validate error message when same contact is added again
        public string ValidateErrorMessageUponAddingDuplicateContact()
        {
            Thread.Sleep(4000);
            driver.FindElement(btnAddBoardMemberL).Click();
            driver.FindElement(txtContactL).SendKeys("Sonika Mathur");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div/ul/li/lightning-base-combobox-item/span[2]/span")).Click();
            driver.FindElement(btnSaveAddHL).Click();
            Thread.Sleep(6000);
            string row = driver.FindElement(msgDupContactL).Text;
            Thread.Sleep(4000);
            driver.FindElement(btnCancel).Click();
            return row;
        }

        //Save Pre Reorganization details
        public string SavePreOrganizedDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtPreReorgConstituentDebt, 100);
            driver.FindElement(txtPreReorgConstituentDebt).Clear();
            driver.FindElement(txtPreReorgConstituentDebt).SendKeys("5");
            driver.FindElement(txtPreReorgTotalDebt).Clear();
            driver.FindElement(txtPreReorgTotalDebt).SendKeys("2");
            driver.FindElement(btnSavePreTransInfo).Click();
            Thread.Sleep(4000);
            string message = driver.FindElement(msgSuccessPreReorg).Text;
            return message;
        }

        //Save Pre Reorganization details
        public string SavePostOrganizedDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtPostRestrDebt, 100);
            driver.FindElement(txtPostRestrDebt).Clear();
            driver.FindElement(txtPostRestrDebt).SendKeys("5");
            driver.FindElement(txtPostReorgTotalDebt).Clear();
            driver.FindElement(txtPostReorgTotalDebt).SendKeys("2");
            driver.FindElement(txtPostClosingStocking).Clear();
            driver.FindElement(txtPostClosingStocking).SendKeys("2");
            driver.FindElement(btnSavePreTransInfo).Click();
            Thread.Sleep(4000);
            string message = driver.FindElement(msgSuccessPreReorg).Text;
            return message;
        }

        //Validate added Board Member is displayed with Hyperlink
        public string ValidateBoardMemberIsDisplayedWithHyperlink()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkBoardMember);
            string value = driver.FindElement(lnkBoardMember).GetAttribute("target");
            return value;
        }

        //Validate added Board Member company is displayed with Hyperlink
        public string ValidateBoardMemberCompanyIsDisplayedWithHyperlink()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkBoardCompany);
            string value = driver.FindElement(lnkBoardCompany).GetAttribute("target");
            return value;
        }
        //Validate added Key Creditor is displayed with Hyperlink
        public string ValidateKeyCredIsDisplayedWithHyperlink()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkKeyCred);
            string value = driver.FindElement(lnkKeyCred).GetAttribute("target");
            return value;
        }

        //Validate that added Equity Holder company in Pre Transaction is displayed in Additional Client subject section of Engagement
        public string ValidateAddedEquityHolderIsDisplayedInAdditionalClientSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngDetailsL, 100);
            driver.FindElement(tabEngDetailsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabClientSubL, 140);
            driver.FindElement(tabClientSubL).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(rowAddedEquityInAdd).Displayed.ToString();
            return value;
        }


        //Validate that added Board Member in Pre Transaction is displayed in Engagement Contacts section
        public string ValidateAddedBoardMemberIsDisplayedInEngagementContacts()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, tabEngDetailsL, 100);
            //driver.FindElement(tabEngDetailsL).Click();
            //driver.Navigate().Refresh();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngContactsL, 140);
            driver.FindElement(tabEngContactsL).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(valAddedMember).Text;
            return value;
        }

        //Fetch the type of added board member in Engagement Contacts  
        public string GetTypeOfAddedBoardMemberInAdditionalClientSubject()
        {
            string value = driver.FindElement(valAddedMemberType).Text;
            return value;
        }

        //Fetch the role of added board member in Engagement Contacts
        public string GetRoleOfAddedBoardMemberInAdditionalClientSubject()
        {
            string value = driver.FindElement(valAddedMemberRole).Text;
            return value;
        }

        //Fetch the role of added staff contact member in Engagement Contacts
        public string GetRoleOfAddedStaffInEngContacts()
        {
            string value = driver.FindElement(valAddedStaffRole).Text;
            return value;
        }
        //Fetch the role of added board member in Engagement Contacts
        public string GetRoleOfAddedBoardMemberInAdditionalClientSubjectPost()
        {
            string value = driver.FindElement(valAddedMemberRolePost).Text;
            return value;
        }

        //Fetch the type of added Equity Holder company in Additional Client subject section of Engagement
        public string GetTypeOfAddedEquityHolderInAdditionalClientSubject()
        {
            string value = driver.FindElement(valAddedEquityType).Text;
            return value;
        }

        //Fetch the role of added Equity Holder company in Additional Client subject section of Engagement
        public string GetRoleOfAddedEquityHolderInAdditionalClientSubject()
        {
            string value = driver.FindElement(valAddedEquityRole).Text;
            return value;
        }

        //Fetch the role of added Equity Holder company in Additional Client subject section of Engagement
        public string GetRoleOfAddedEquityHolderInAdditionalClientSubjectPost()
        {
            string value = driver.FindElement(valAddedEquityRolePost).Text;
            return value;
        }

        //Validate Contact field on click of Add Board Member
        public bool VerifyTextFieldsOfAddDebtStructureL()
        {
            driver.FindElement(btnAddDebtStrL).Click();

            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valHeader = driver.FindElements(lblAddDebtTxtFields);
            var actualValue = valHeader.Select(x => x.Text).ToArray();
            Console.WriteLine(actualValue);
            string[] expectedValue = { "Interest", "OID Percent", "Amortization", "Call Provisions / Prepayment Premiums", "Mandatory Prepayments / ECF Sweep", "Covenants", "Fees & Expenses", "Facility Balance (MM)" };
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
}