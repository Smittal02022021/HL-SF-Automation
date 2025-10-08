using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SF_Automation.Pages.Company;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace SF_Automation.Pages.Companies
{
    class CompanyDetailsPage : BaseClass
    {
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanySelectRecordPage conSelectRecord = new CompanySelectRecordPage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        By linkCoverageTeam = By.CssSelector("a[id*='bV0_link']>span");
        By linkOfficerName = By.CssSelector("div[id*='V0_body'] > table > tbody > tr:nth-child(2) > th > a");
        By titleCoverageTeam = By.XPath("//div[@id='ep']//h2[@class='mainTitle']");
        By titlePage = By.CssSelector("h1[class='pageType']");
        By linkCompany = By.CssSelector("a[id*='D7bV0']");
        By linkCompanyList = By.CssSelector("a[id*='13ee_link']>span");
        By linkCompanyMember = By.CssSelector("div[id*='13ee_body'] > table > tbody > tr.dataRow.even.last.first > th > a");
        By linkCampaign = By.CssSelector("a[title='Campaigns Tab']");
        By btnGo = By.CssSelector("input[title='Go!']");
        By linkCampaignName = By.CssSelector("div[id*='nYp_CAMPAIGN_NAME']");
        By comboFlagReason = By.XPath("//label[text()='Flag Reason']/following::td/span/select");
        By txtFlagReasonComment = By.XPath("(//label[text()='Flag Reason Comment']/following::td/textarea)[1]");



        By companyDetailHeading = By.XPath("//h2[contains(text(),'Company Detail')]");
        By btnNewContact = By.CssSelector("td[class='pbButton'] > input[value='New Contact']");
        By btnAddCFOpportunity = By.XPath("//a[contains(text(),'Add CF Opportunity')]");
        By btnNewCFOpportunity = By.CssSelector("input[value='New CF Opportunity']");
        By btnAddFASOpportunity = By.XPath("//a[contains(text(),'Add FAS Opportunity')]");
        By btnNewFASOpportunity = By.CssSelector("input[value='New FAS Opportunity']");
        By btnNewCoverageTeam = By.CssSelector("input[value='New Coverage Team']");
        By coverageTeamEditHeading = By.CssSelector("h2[class='mainTitle']");
        By txtCompanyName = By.CssSelector("input[id='CF00Ni000000D7bV0']");
        By btnSaveCoverageTeam = By.CssSelector("td[id='topButtonRow'] > input[value=' Save ']");
        By toolTipPriorityHelp = By.CssSelector("span[id*='00N3100000GuI2p-_help'] > img");
        By toolTipPriorityText = By.CssSelector("span[id*='00N3100000GuI2p-_help'] > script");
        By btnDeleteCompany = By.CssSelector("td[id*='topButtonRow'] > input[value='Delete']");
        By valCompanyName = By.CssSelector("div[id*='acc2j_id0']");
        By valState = By.CssSelector("div[id*='00Ni000000DvFsEj']");
        By valCompanyType = By.CssSelector("div[id*='RecordTypej']");
        By valAddress = By.CssSelector("div[id*='acc17j']");
        By btnEdit = By.CssSelector("td[id*='topButtonRow'] >input[value*='Edit']");
        By valCompanySubType = By.XPath("//*[text()='Company Information']/.. //following-sibling::div//*[text()='Company Sub Type']/..//div[@id='acc6j_id0_j_id1_ileinner']");
        By valOwnership = By.XPath("//*[text()='Company Information']/.. //following-sibling::div//*[text()='Ownership']/..//div[@id='acc14j_id0_j_id1_ileinner']");
        By valParentCompany = By.XPath("//*[text()='Company Information']/..//following-sibling::div//*[text()='Parent Company']/../..//div[@id='acc3j_id0_j_id1_ileinner']");
        By valIndustryFocus = By.XPath("//*[text()='Investment Preferences']/..//following-sibling::div//*[text()='Industry Focus']/..//div[@id='00Ni000000D9WGgj_id0_j_id1_ileinner']");
        By valGeographicalFocus = By.XPath("//*[text()='Investment Preferences']/..//following-sibling::div//*[text()='Geographical Focus']/..//div[@id='00Ni000000D9WG2j_id0_j_id1_ileinner']");
        By valDealPreference = By.XPath("//*[text()='Investment Preferences']/..//following-sibling::div//*[text()='Deal Preferences']/..//div[@id='00Ni000000DvG7nj_id0_j_id1_ileinner']");
        By valDescription = By.XPath("//*[text()='Description Information']/..//following-sibling::div//*[text()='Description']/..//div[@id='acc20j_id0_j_id1_ileinner']");
        By valCapIQCompany = By.XPath("//*[text()='CapIQ Information']/..//following-sibling::div//*[text()='CapIQ Company']/../..//div[@id='CF00Ni000000DvFoMj_id0_j_id1_ileinner']");
        By errPage = By.CssSelector("span[id='theErrorPage:theError']");
        By valcompanyLocation = By.CssSelector("div[id*='00Ni000000DvFsEj']");
        By btnEditCompanyDetail = By.CssSelector("td[id='topButtonRowj_id0_j_id1'] > input[name='edit']");
        By valcompanyPhoneNumber = By.CssSelector("div[id*='acc10j_id0']");
        By valCompanyDescription = By.CssSelector("div[id*='acc20j_id0']");
        By valCompanyAddress = By.CssSelector("div[id*='acc17j_id0']");
        By chkBoxHQ = By.CssSelector("img[id*= '00N3100000GyJM5']");
        By linkChangeCompanyType = By.CssSelector("div[id*='RecordType'] > a");
        By btnSaveCompanyEdit = By.CssSelector("td[id='bottomButtonRow'] > input[value=' Save ']");
        By valCreatedBy = By.CssSelector("div[id*='CreatedBy'] > a");
        By valLabelGroupByDupRecord = By.CssSelector("tr[class='breakRowClass0 breakRowClass0Top'] > td:nth-child(2) > strong:nth-child(1)");
        By btnNewCompanyFinancial = By.CssSelector("input[value='New Company Financial']");
        By valCompanyFinancialYear = By.CssSelector("div[id*='ke_body'] > table > tbody > tr:nth-child(2) > th");
        By valAsOfDateInCompanyFinancial = By.CssSelector("div[id*='ke_body'] > table > tbody > tr:nth-child(2) > td:nth-child(3)");
        By valFinancialsYearAnnualFinancial = By.XPath("//*[text()='Annual Financials']/..//following-sibling::div//*[text()='Financials Year']/../td[2]");
        By valFinancialsDateAnnualFinancial = By.XPath("//*[text()='Annual Financials']/..//following-sibling::div//*[text()='Financials As of Date']/../td[2]");
        By linkDelCompanyFinancial = By.XPath("//*[text()='Company Financials']/../../../../../following-sibling::div/table/tbody/tr[2]/td/a[2]");
        By linkDelContact = By.XPath("//*[text()='Contacts']/../../../../../following-sibling::div/table/tbody/tr[2]/td/a[2]");
        By valNoRecordsCompanyFinancial = By.XPath("//*[text()='Company Financials']/../../../../../following-sibling::div/table/tbody/tr/th");
        By linkDelCoverageTeam = By.XPath("//*[text()='Coverage Team']/../../../../../../div[@class='pbBody']/table/tbody/tr/td[1]/a[2]");
        By linkCoverageTeamOfficer = By.XPath("//*[text()='Coverage Team']/../../../../../../div[@class='pbBody']/table/tbody/tr/th/a");
        By valCoverageLevel = By.XPath("//*[text()='Coverage Team']/../../../../../../div[@class='pbBody']/table/tbody/tr[2]/td[4]");
        By valCoverageType = By.XPath("//*[text()='Coverage Team']/../../../../../../div[@class='pbBody']/table/tbody/tr[2]/td[6]");
        By btnMergeContacts = By.CssSelector("input[name='merge']");
        By valContactName = By.XPath("//*[text()='Contacts']/../../../../../../div[2]/table/tbody/tr[2]/th/a");
        By valSecondContactName = By.XPath("//*[text()='Contacts']/../../../../../../div[2]/table/tbody/tr[3]/th/a");
        By valThirdContactName = By.XPath("//*[text()='Contacts']/../../../../../../div[2]/table/tbody/tr[4]/th/a");

        By valContactEmail = By.XPath("//*[text()='Contacts']/../../../../../../div[2]/table/tbody/tr[2]/td[3]/a");
        By valContactPhone = By.XPath("//*[text()='Contacts']/../../../../../../div[2]/table/tbody/tr[2]/td[4]");
        By linkCoverageOfficer = By.XPath("//*[text()='Officer Name']/../../tr/th/a[text()='Jacklyn Robinson']");


        // By valERPSubmittedToSync = By.CssSelector("div[id*='eayj']");
        By valERPSubmittedToSync = By.XPath("//td[@id='00N5A00000M0eayj_id0_j_id1_ilecell']/div");

        By valERPLastIntegrationResponseDate = By.XPath("//*[@id='00N5A00000M0eapj_id0_j_id1_ilecell']/div");
        // By valERPLastIntegrationResponseDate = By.CssSelector("div[id*='M0eapj']");

        By valERPLastIntegrationStatus = By.XPath("//*[@id='00N5A00000M0eaqj_id0_j_id1_ilecell']/div");
        //By valERPLastIntegrationStatus = By.CssSelector("div[id*='M0eaqj']");

        By valERPLastIntegrationErrorCode = By.XPath("//*[@id='00N5A00000M0eanj_id0_j_id1_ilecell']/div");
        //By valERPLastIntegrationErrorCode = By.CssSelector("div[id*='M0eanj']");
        By valERPLastIntegrationErrorDescription = By.XPath("//*[@id='00N5A00000M0eaoj_id0_j_id1_ilecell']/div");
        //By valERPLastIntegrationErrorDescription = By.CssSelector("div[id*='M0eaoj']");
        By valClientNumber = By.CssSelector("div[id*='DxJbdj']");
        By valPrimaryContactAvailable = By.CssSelector("div[id*='RelatedContactList_body'] > table > tbody > tr:nth-child(1) > th");
        By valERPAccountID = By.CssSelector("div[id*='M0eaUj']");
        By valERPOrgPartyId = By.CssSelector("div[id*='M0earj']");
        By valERPBillToAddressId = By.CssSelector("div[id*='M0eaWj']");
        By valERPBillToLocationId = By.CssSelector("div[id*='M0eaXj']");
        By valERPBillToLocation = By.CssSelector("div[id*='M5Dqsj']");
        By valERPBillToSiteId = By.CssSelector("div[id*='M0eaYj']");
        By valERPShipToAddressId = By.CssSelector("div[id*='M0eavj']");
        By valERPShipToLocationId = By.CssSelector("div[id*='M0eawj']");
        By valERPShipToSiteId = By.CssSelector("div[id*='M0eaxj']");
        By valERPBillToAddressFlag = By.CssSelector("div[id*='M0eaVj']");
        By valERPShipToAddressFlag = By.CssSelector("div[id*='M0eauj']");
        By valERPContactFirstName = By.CssSelector("div[id*='M0eaaj']");
        By valERPContactLastName = By.CssSelector("div[id*='M0eadj']");
        By valERPContactEmail = By.CssSelector("div[id*='M0eaZj']");
        By valERPContactPhone = By.CssSelector("div[id*='M0eaej']");
        By valERPContactId = By.CssSelector("div[id*='M0eacj']");
        By valERPPersonPartyId = By.CssSelector("div[id*='M0eatj']");
        By valERPContactPointEmailId = By.CssSelector("div[id*='M0eagj']");
        By valERPContactPointPhoneId = By.CssSelector("div[id*='M0eaij']");
        By valERPContactPointRelationshipId = By.CssSelector("div[id*='M0eajj']");
        By valERPAccountDescription = By.CssSelector("div[id*='M0eaTj']");
        By valERPCustomerType = By.CssSelector("div[id*='M0ealj']");
        By valERPCustomerTypeDesc = By.CssSelector("div[id*='M0eakj']");
        By linkContractName = By.CssSelector("th[class*='dataCell'] a");
        By comboStateProvince = By.CssSelector("select[id='acc17state']");
        By txtShippingProvince = By.CssSelector("input[id*='M0eb2']");
        By linkContactEdit = By.CssSelector("div[id*='RelatedContactList_body'] > table > tbody >tr:nth-child(2) > td:nth-child(1) > a");
        By txtContactEmail = By.CssSelector("input[id='con15']");
        By txtContactPhone = By.CssSelector("input[id='con10']");
        By btnContactSave = By.CssSelector("td[id='topButtonRow'] input[value*=' Save ']");
        By valERPContactFlag = By.CssSelector("td[id*='M0eabj']");
        By valERPContactPointEmailFlag = By.CssSelector("td[id*='M0eafj']");
        By valERPContactPointPhoneFlag = By.CssSelector("td[id*='M0eahj']");
        By linkPrimaryBillingContact = By.CssSelector("div[id*='M0eazj'] > a");
        By linkCoverageSector = By.CssSelector("table[class='list'] > tbody > tr:nth-child(2) > th > a");
        By btnDeleteCoverageSector = By.CssSelector("input[value='Delete']");
        By changelinkCompanyRecordType = By.CssSelector("div[id*='RecordTypej']>a");

        By txtCurrentRecordType = By.CssSelector("table[class='detailList']>tbody>tr:nth-of-type(2)>td:nth-of-type(2)");
        By drpdwnCompanyRecordType = By.CssSelector("select[id='p3']");

        By btnContinue = By.CssSelector("input[title='Continue']");
        By drpdownOfficeCode = By.CssSelector("select[id*='00Fjq9q']");

        By btnNewCompanySector = By.XPath("//input[@value='New Company Sector & Investment Preference']");
        By shwAllTab = By.CssSelector("li[id='AllTab_Tab'] > a > img");
        By imgCoverageSectorDependencies = By.CssSelector("img[alt = 'Coverage Sector Dependencies']");
        By imgCompanySectorDependencyLookUp = By.XPath("//img[@alt='Sector Categorization Lookup (New Window)']");
        By txtSearchBox = By.XPath("//input[@title='Go!']/preceding::input[1]");
        By linkCoverageSectorDependencyName = By.XPath("//a[@href='#']");
        By btnSaveCompanySector = By.XPath("(//input[@title='Save'])[1]");
        By btnDeleteCompanySector = By.XPath("(//input[@title='Delete'])[1]");
        By valCompanySectorName = By.XPath("//td[contains(text(),'Company Sector')]/following::div[1]");
        By linkCompanyName = By.XPath("(//td[contains(text(),'Company')])[2]/../td[2]/div/a");
        By linkSectorName = By.XPath("//table/tbody/tr[2]/th/a");
        By linkCompanySector = By.XPath("//span[@class='count'][contains(text(),'0')]/preceding::span[contains(text(),'Company Sectors')]");
        By linkShowFilters = By.XPath("//a[contains(text(),'Show Filters')]");
        By inputCoverageType = By.XPath("//input[@id='00N6e00000MRMtkEAHCoverage_Sector_Dependency__c']");
        By inputPrimarySector = By.XPath("//input[@id='00N6e00000MRMtlEAHCoverage_Sector_Dependency__c']");
        By inputSecondarySector = By.XPath("//input[@id='00N6e00000MRMtmEAHCoverage_Sector_Dependency__c']");
        By inputTertiarySector = By.XPath("//input[@id='00N6e00000MRMtnEAHCoverage_Sector_Dependency__c']");
        By btnApplyFilters = By.XPath("//input[@title='Apply Filters']");
        By btnEditCompCoverageSector = By.XPath("//input[@title='Edit']");
        By txtCompanyType = By.XPath("//span[contains(@class,'field-label')][normalize-space()='Company Type']/ancestor::dl//dd//div[contains(@class,'recordTypeName')]"); //span[@class='test-id__field-label'][normalize-space()='Company Type']/parent::div/following-sibling::div/span/slot/records-record-type//div[contains(@class,'recordTypeName')]/span");//p[@title='Company Type']//following::p//span");

        By drpdownInvestmentPref = By.XPath("//label[text()='Investment Preference / Operating Sector']/../../td[2]/div/span/select");

        By searchOpportunitiesL = By.XPath("//flexipage-component2[@data-component-id='c_sL_HierarchyViewer']//input[contains(@placeholder,'Search')]");
        By searchEngagementsL = By.XPath("//flexipage-component2[@data-component-id='c_sL_HierarchyViewer2']//input[contains(@placeholder,'Search')]");

        By linkViewAllOppL = By.XPath("//flexipage-component2[@data-component-id='c_sL_HierarchyViewer']//div[contains(@class,'card__footer')]//slot[@name='footer']//a[text()='View All']");
        By btnCloseViewAllPopup = By.XPath("//div[@class='slds-modal__container']//button[contains(@class,'slds-modal__close')]");
        By linkViewAllEngL = By.XPath("//flexipage-component2[@data-component-id='c_sL_HierarchyViewer2']//div[contains(@class,'card__footer')]//slot[@name='footer']//a[text()='View All']");
        By searchViewAllOppEngL = By.XPath("//div[@class='slds-modal__container']//input[contains(@placeholder,'Search')]");

        By headerNestedListL = By.XPath("//article[contains(@class,'NestedTables_CardChild')]//h2[contains(@class,'container')]//span");
        By txtCompanyHLRelationshipContactL = By.XPath("//article//div[contains(@class,'otherBrowser')]//table//tr[1]//td[@data-label='HL Contact']//a");
        By txtCompanyHLRelationshipCoverageOfficerL = By.XPath("//flexipage-component2[contains(@data-component-id,'NestedTables2')]//table//tr[1]//td[@data-label='Officer Name']//a");//div[contains(@class,'NestedTables')]//table//tr[1]//td[@data-label='Officer Name']//a
        By txtCoverageTeamCompanyNameL = By.XPath("(//div[contains(@class,'page-header')]//h1//slot[@name='primaryField']//a//span//slot)[2]"); //div[contains(@class,'page-header')]//h1//slot[@name='primaryField']//a//span");
        By txtCoverageTeamOfficerNameL = By.XPath("//p[@title='Officer Name']//following::p//span//a");
        By txtCompanyDetailCoverageTypeL = By.XPath("//article//div[contains(@class,'otherBrowser')]//table//tbody/tr[1]//td[1]//span");
        By panelCoverageTypeL = By.XPath("//dt[text()='Coverage Type:']//following-sibling::dd[1]//span");
        By panelTabCoverageSectors = By.XPath("//flexipage-component2[@slot='sidebar']//ul[@role='tablist']//li/a[@data-label='Coverage Sectors']");
        By buttonCloseCoverageTab = By.XPath("//button[contains(@title,'Close C-')]");
        By alertDuplicate = By.XPath("//div[@role='alertdialog']//button[@title='Close']");

        By comboIndustryType = By.CssSelector("select[id*='FD7Vf']");
        By comboIndustryTypeOptions = By.CssSelector("select[id*='FD7Vf'] option");
        By btnCancel = By.CssSelector("input[name='cancel']");
        By comboType = By.CssSelector("select[id*='FjXsE']");
        By comboTypeOption = By.CssSelector("select[id*='FjXsE'] option");
        By txtDetailHeadingL = By.XPath("//h1//lightning-formatted-text");
        By valCompanyAddressL = By.XPath("//slot[@name='header']//lightning-formatted-address");
        By valCompanyTypeL = By.XPath("//div[contains(@class,'recordTypeName')]//span");
        By txtDescL = By.XPath("//span[text()='Description']/../../..//lightning-formatted-text");
        By txtCmpnySubTypeL = By.XPath("//span[text()='Company Sub Type']/../../..//lightning-formatted-text");
        By txtOwnershipL = By.XPath("//span[text()='Ownership']/../../..//lightning-formatted-text");
        By txtIGL = By.XPath("//span[text()='Industry Group']/../../..//lightning-formatted-text");
        By txtDscL = By.XPath("//div//span[text()='Description']/../../..//lightning-formatted-text");
        By btnAddDeleteL = By.XPath("//div//li[contains(@data-target-selection-name,'Button.Address__c.Delete')]");
        By btnDeleteL = By.XPath("//button[@name='Delete']");
        By iconExpandMoreButonL = By.XPath("//lightning-button-menu//button[contains(@class,'slds-button_icon-border-filled')]");
        By linkDeleteL = By.XPath("//a/span[contains(text(),'Delete')]");
        By btnConfirmDeleteL = By.XPath("//button[@title='Delete']");
        By btnRelatedNewContactL = By.XPath("//button[text()='New']");
        By headertxtNewContactL = By.XPath("//div[@class='forceChangeRecordType']//h2");
        By tabContactsL = By.XPath("//lightning-tab-bar/ul/li/a[text()='Contacts']");
        By tabInfoL = By.XPath("//a[text()='Info']");
        By tabContactL = By.XPath("//a[text()='Contacts']");
        By tabCoverageL = By.XPath("//a[text()='Coverage']");
        By tabFinancialsL = By.XPath("//a[text()='Financials']");
        By txtsectionNameL = By.XPath("//h3//button/span"); //h3//span[@class='slds-truncate']");
        By btnInlineEditPhoneL = By.XPath("//button[@title='Edit Phone']");
        By inputPhoneL = By.XPath("//input[@name='Phone']");
        //By btnCancelDetailsL = By.XPath("//button[@name='CancelEdit']");
        By btnSaveDetailsL = By.XPath("//button[@name='SaveEdit']");
        By btnEditTopPanelL = By.XPath("//button[@name='Edit']");
        By txtPhoneNumberL = By.XPath("//span[text()='Phone']/../../..//lightning-formatted-phone//a");
        By txtClientNumberL = By.XPath("//span[text()='Client Number']/../../..//lightning-formatted-text");
        By txtParentCompanyL = By.XPath("//span[text()='Parent Company']/../../..//a//slot//slot");

        By txtTradeNameL = By.XPath("//span[text()='Trade Name']/../../..//lightning-formatted-text");
        By btnInlineChngTypeL = By.XPath("//button[@title='Change Record Type']");
        By btnNextChangeRecordTypeL = By.XPath("//div[contains(@class,'ChangeRecordTypeFooter')]//button[2]");
        By headrAnnFinL = By.XPath("//*[text()='Annual Financials']");
        By comboSectorL = By.XPath("//label[text()='Sector']/parent::div//button");
        By inputClientNumberL = By.XPath("//label[text()='Client Number']/..//input");
        By inputParentCompanyL = By.XPath("//label[text()='Parent Company']/..//input");
        By frameReportL = By.XPath("//iframe[@title='Report Viewer']");
        By tabBML = By.XPath("//h2/..//ul//li[@title='Board Members']//a");
        By tabOppL = By.XPath("//h2/..//ul//li[@title='Opportunities']//a");
        By headerBML = By.XPath("//article[@aria-label='Board Members']");
        By btnNewBML = By.XPath("//article[@aria-label='Board Members']//button[@name='New']");
        By txtAffCompanyNameL = By.XPath("//label[text()='Company']/..//input");
        By valAffStatusL = By.XPath("//label[text()='Status']/..//button");
        By headerNewAffL = By.XPath("//h2[text()='New Affiliation']");
        By txtReqFields = By.XPath("//div[@class='fieldLevelErrors']//li//a");
        By inputAffContactL = By.XPath("//input[@placeholder='Search Contacts...']");
        By btnAffTypeL = By.XPath("//button[@aria-label='Type']");
        By txtAffTypeL = By.XPath("//span[text()='Type']/../../..//lightning-formatted-text");
        By txtNumberRelationshipL = By.XPath("//records-entity-label[text()='Affiliation']/../../..//slot//lightning-formatted-text");
        By recordBoardMemberL = By.XPath("//table[@aria-label='Board Members']//tbody//tr[1]//th//lightning-formatted-rich-text//a[2]");
        By recordStatusTypeL = By.XPath("//table[@aria-label='Board Members']//tbody//tr[1]//td[@data-label='Affiliation: Status - Type']//lightning-formatted-rich-text//span");
        By recordAffNotesL = By.XPath("//table[@aria-label='Board Members']//tbody//tr[1]//td[@data-label='Affiliation: Notes']//lightning-base-formatted-text");
        By inputAffNotesL = By.XPath("//label[text()='Notes']/..//textarea");
        By btnPrintableViewL = By.XPath("//button[text()='Printable View']");
        By linkPrintableViewL = By.XPath("//a/span[contains(text(),'Printable View')]");
        By linkPrintPageL = By.XPath("//div[@class='printHeader']//a[text()='Print This Page']");

        By btnNewContactL = By.XPath("//button[text()='New Contact']");
        By inputFirstNameL = By.XPath("//input[contains(@class,'firstName')]");
        By inputLastNameL = By.XPath("//input[contains(@class,'lastName')]");
        By btnContactContactL = By.XPath("//footer//button[1]");
        By btnSaveContactL = By.XPath("//footer//button[2]");
        By msgDuplicate = By.XPath("//div[contains(@class,'dedupeToast')]");
        By btnContactNewL = By.XPath("//button[text()='New']");
        By btnNexRecordTypetL = By.XPath("//div[@class='slds-modal__footer']//button[text()='Next']");
        By inputNewContactFirstNameL = By.XPath("//input[contains(@name,'firstName')]");
        By inputNewContactLastNameL = By.XPath("//input[contains(@name,'lastName')]");
        By toastMsgPopup = By.XPath("//span[contains(@class,'toastMessage')]");
        By toastMsgCloseIcon = By.XPath("//button[@title='Close']");
        By tableFullNameL = By.XPath("//table//tr//td[@data-label='Full Name']//a");
        By iconContactActionsMoreL = By.XPath("//button[contains(@class,'button slds-button_icon-border')]");//button[@aria-haspopup='true'][contains(@class,'button slds-button_icon-border')]");
        By lnkContactActionsMoreL = By.XPath("//ul//a[@role='menuitem']//span[text()='Edit']");
        By txtContactPhoneNumberL = By.XPath("//table//td[@data-label='Business Phone']/span");
        By btnAddRelationshipL = By.XPath("//button[contains(@name,'Contact.Add_Relationship')]");
        By inputHLContactL = By.XPath("//input[@title='Search Contacts']");
        By linkViewDetailsRelationshipL = By.XPath("//tr[contains(@class,'NestedTables')]//table//td[@data-label='View Details']//a");
        By txtHLRelationshipContactL = By.XPath("//span[text()='HL Contact']/../../..//a//span//slot//span//slot");

        By btnViewContactRelationshipsL = By.XPath("//button[text()='View Contacts Relationships']");
        By txtHeaderReportL = By.XPath("//div[contains(@class,'reportView')]//div[@title='Total Records']");
        By txtHLRelationContactL = By.XPath("//div[contains(@class,'reportView')]//table//tr[2]//td//a");

        By iconBMShowMoreL = By.XPath("//lightning-button-menu//button[contains(@class,'slds-button_icon-border')]");
        By lnkBMEditL = By.XPath("//div[@title='Edit']/..");
        By lnkBMDeleteL = By.XPath("//div[@title='Delete']/..");

        By btnNewSponsorCoverageL = By.XPath("//h2//span[text()='Sponsor Coverage']//ancestor::article//button[text()='New']");
        By btnNewIndustryCoverageL = By.XPath("//h2//span[text()='Industry Coverage']//ancestor::article//button[text()='New']");
        By txtICofficerNameL = By.XPath("//h2//span[text()='Industry Coverage']//ancestor::article//table//tbody//tr//td[@data-label='Officer Name']//a");
        By txtSCOfficerNameL = By.XPath("//h2//span[text()='Sponsor Coverage']//ancestor::article//table//tbody//tr//td[@data-label='Officer Name']//a");
        By iconICExpandMoreButonL = By.XPath("//h2//span[text()='Industry Coverage']//ancestor::article//table//button[contains(@class,'slds-button_icon-border')]");
        By iconSCExpandMoreButonL = By.XPath("//h2//span[text()='Sponsor Coverage']//ancestor::article//table//button[contains(@class,'slds-button_icon-border')]");

        By linkEditL = By.XPath("//a/span[contains(text(),'Edit')]");
        By txtICTierL = By.XPath("//h2//span[text()='Industry Coverage']//ancestor::article//table//tbody//td[@data-label='Tier']/span");
        By txtSCTierL = By.XPath("//h2//span[text()='Sponsor Coverage']//ancestor::article//table//tbody//td[@data-label='Tier']/span");

        By tableFinancialsL = By.XPath("//table[@aria-label='HL Company Financials']//tbody//tr");
        By tableActivityL = By.XPath("//span[@title='Type']//ancestor::table/tbody//tr");
        By btnNewHLCompanyFinancialsL = By.XPath("//h2//span[@title='HL Company Financials']//ancestor::article//button[text()='New']");
        By comboYearL = By.XPath("//label[text()='Year']/..//button");
        By comboSourceL = By.XPath("//label[text()='Source']/..//button");
        By comboDataSourceL = By.XPath("//label[text()='Data Source']/..//button");
        By inputRevenueL = By.XPath("//label[text()='Revenue']/..//input");
        By inputEBITDAL = By.XPath("//label[text()='EBITDA']/..//input");
        By inputAsOfDate = By.XPath("//label[text()='As of Date']/..//input");
        By btnAddFilesL = By.XPath("//article[@aria-label='Files']//div[@title='Add Files']");
        By inputUploadFilesL = By.XPath("//article[@aria-label='Files']//label//span[text()='Upload Files']");
        By lnkVIewAllUploadedFilesL = By.XPath("//article[@aria-label='Files']//span[@class='view-all-label']/../..");
        // By lnkFileMoreActionsL=By.XPath(//table//span[text()='dashboard']//ancestor::tr//td//a[@role='button']);
        By lnkEditL = By.XPath("//div[contains(@class,'uiMenuList--default visible positioned')]//div[@title='Edit']/..");
        By lnkDeleteL = By.XPath("//div[contains(@class,'uiMenuList--default visible positioned')]//div[@title='Delete']/..");
        By txtHLFinNameL = By.XPath("//span[text()='HL Financial Name']/../../..//lightning-formatted-text");
        By btnNewCompanySectorsL = By.XPath("//article[@aria-label='Company Sectors']//button[text()='New']");
        By btnNewAddLocationL = By.XPath("//article[@aria-label='Address']//button[text()='New']");
        By btnNewInvestmentPreferencesL = By.XPath("//article[@aria-label='Investment Preferences']//button[text()='New']");
        By inputsectorCategL = By.XPath("//label[text()='Sector Categorization']/..//input");
        By txtCompanySectorL = By.XPath("//span[text()='Company Sector']/../../..//lightning-formatted-text");
        By btnClearSectorCategL = By.XPath("//label[text()='Sector Categorization']/..//button[@title='Clear Selection']");
        By comboAddressTypeL = By.XPath("//label[text()='Address Type']/..//button");
        By iconHeaderMoreTabsL = By.XPath("//lightning-tab-bar/ul/li/lightning-button-menu/button[@title='More Tabs']");
        By btnNewFSFundsL = By.XPath("//article[@aria-label='FS Funds']//div//button[@name='New']");
        By valCompanyNameL = By.XPath("//label[text()='Company']/..//input");
        By btnNewFinancialsL = By.XPath("//article[@aria-label='HL Company Financials']//div//button[@name='New']");
        By btnNewFinancialsSPL = By.XPath("//article[contains(@aria-label,'Current')]//div//button[@name='New']");
        By txtSPNameL = By.XPath("//span[text()='Investment List Number']/../../..//lightning-formatted-text");
        By btnInlineFlagReasonL = By.XPath("//button[@title='Edit Flag Reason']");
        By btnFlagReasonL = By.XPath("//button[@aria-label='Flag Reason']");
        By inputFlagReasonCommentL = By.XPath("//label[text()='Flag Reason Comment']/..//textarea");
        By txtFlagReasonL = By.XPath("//span[text()='Flag Reason']/../../..//lightning-formatted-text");
        By txtFlagReasonCommentL = By.XPath("//span[text()='Flag Reason Comment']/../../..//lightning-formatted-text");
        By btnEditL = By.XPath("//button[@name='Edit']");
        By comboIndustryTypeL = By.XPath("//label[text()='Industry Group']/..//button");
        By comboIndustryTypeOptionsL = By.XPath("//label[text()='Industry Group']/..//lightning-base-combobox-item//span[2]/span");
        By btnCancelL = By.XPath("//button[@name='CancelEdit']");
        By iframeCompanyForm = By.XPath("//iframe[contains(@name,'vfFrame')]");
        By btnNewCoverageTeamL = By.XPath("//span[contains(text(),'Sponsor Coverage')]//ancestor::header//button");//span[contains(text(),'Sponsor Coverage')]//ancestor::h2/..//following-sibling::div//button[text()='New']");
        By btnDialogNextL = By.XPath("//div[@role='dialog']//button[text()='Next']");
        By comboTypeL = By.XPath("//label[text()='Type']/..//button");
        By comboTypeOptionsL = By.XPath("//label[text()='Type']/..//lightning-base-combobox-item//span[2]/span");

        By btnEditInvestmentL = By.XPath("//records-entity-label[text()='Investment List']//ancestor::h1/../../..//button[@name='Edit']");
        By btnDeleteInvestmentL = By.XPath("//records-entity-label[text()='Investment List']//ancestor::h1/../../..//button[@name='Delete']");
        By inputInvestmentAmountL = By.XPath("//label[text()='Amount of Investment']/..//input");
        By btnInvestStatusL = By.XPath("//button[@aria-label='Status']");
        By btnActivitiesReportL = By.XPath("//button[text()='Activities Report']");
        By iframeTableCompanyActivityL = By.XPath("//iframe[@title='Report Viewer']");
        By headerPageL = By.XPath("//div[contains(@class,'reportView')]//h1");
        By tableCompaiesActivities = By.XPath("//table[contains(@class,'full-table')]");
        By tableActivityCompanyName = By.XPath("//table[contains(@class,'full-table')]//tbody//tr[2]//td[6]//span/a");
        By btnNewIPL = By.XPath("//h1[@title='Investment Preferences']/../../../../..//ul//button[@name='New']");
        By btnDialogNewIPL = By.XPath("//h2//a[@title='Investment Preferences']/../../../../..//ul//a[@title='New']");
        By btnNextL = By.XPath("//div[contains(@class,'ChangeRecordTypeFooter')]//button/span[text()='Next']");
        By btnDeleteIPL = By.XPath("//ul//button[@name='Delete']");
        By txtIPNameL = By.XPath("//h1//records-entity-label[text()='Investment Preference']/../../..//lightning-formatted-text");
        By txtIPContactNumberL = By.XPath("//h1//records-entity-label[text()='Investment Preference Contact']/../../..//lightning-formatted-text");
        By txtIPTypeNumberL = By.XPath("//h1//records-entity-label[text()='Investment Preference Investment Type']/../../..//lightning-formatted-text");
        By txtIPInvestorNumberL = By.XPath("//h1//records-entity-label[text()='Investment Preference Investor Type']/../../..//lightning-formatted-text");
        By txtIPUseOfProceedsNumberL = By.XPath("//h1//records-entity-label[text()='Investment Preference Use of Proceeds']/../../..//lightning-formatted-text");
        By txtIPOwnershipNumberL = By.XPath("//h1//records-entity-label[text()='Investment Preference Ownership']/../../..//lightning-formatted-text");
        By txtIPSectorL = By.XPath("//h1//records-entity-label[text()='Investment Preference Sector']/../../..//lightning-formatted-text");
        By txtsectorTypeL = By.XPath("//span[text()='Type']/../..//lightning-formatted-text");
        By btnIvestorTypeL = By.XPath("//label[text()='Investor Type']/..//button");
        By btnUseOfProceedsL = By.XPath("//label[text()='Use of Proceeds']/..//button");
        By btnInvestmentTypeL = By.XPath("//label[text()='Investment Type']/..//button");
        By btnOwnershipL = By.XPath("//label[text()='Ownership']/..//button");
        By comboOwnershipOptionsL = By.XPath("//label[text()='Ownership']/..//lightning-base-combobox-item//span/span");
        By comboIvestorTypeOptionsL = By.XPath("//label[text()='Investor Type']/..//lightning-base-combobox-item//span/span");
        By comboUseOfProceedsOptionsL = By.XPath("//label[text()='Use of Proceeds']/..//lightning-base-combobox-item//span/span");
        By comboInvestmentTypeOptionsL = By.XPath("//label[text()='Investment Type']/..//lightning-base-combobox-item//span/span");
        By inputSectorL = By.XPath("//label[text()='Sector']/..//input");
        By inputIPContactL = By.XPath("//label[text()='Contact']/..//input");
        By inputIPContactRoleL = By.XPath("//label[text()='Role']/..//input");
        By txtIPContactL = By.XPath("//h2//span[text()='Investment Preference Contacts']//ancestor::article//h3//lightning-formatted-rich-text/span");
        By txtIPSectorSidePanelL = By.XPath("//h2//span[text()='Investment Preference Sectors']//ancestor::article//h3//span//span//span");
        By txtIPInvestmentTypePanelL = By.XPath("//h2//span[text()='Investment Preference Investment Type']//ancestor::article//lst-formatted-text/span");
        By txtIPInvestorTypePanelL = By.XPath("//h2//span[text()='Investment Preference Investor Type']//ancestor::article//lst-formatted-text/span");
        By txtIPOwnershipPanelL = By.XPath("//h2//span[text()='Investment Preference Ownership']//ancestor::article//lst-formatted-text/span");
        By txtIPUseOfProceedsPanelL = By.XPath("//h2//span[text()='Investment Preference Use of Proceeds']//ancestor::article//lst-formatted-text/span");

        private By _DetailPageQuickLink(string name)
        {
            return By.XPath($"//div[@class='listHoverLinks']//a//span[text()='{name}']");
        }

        private By _homePageTab(string name)
        {
            return By.XPath($"//lightning-tabset[@class='flexipage-tabset']//a[contains(@data-label,'{name}')]");
        }

        private By _tabHeader(string name)
        {
            return By.XPath($"//div[@class='slds-card__header slds-grid']//h2//span[contains(text(),'{name}')]");
        }

        private By _CompanyDetailPageQuickLink(string name)
        {
            return By.XPath($"//flexipage-component2//li[contains(@class,'relatedListQuickLink')]//a[contains(@href,'{name}')]");
        }

        private By _btnAddOpportunity(string valLOB)
        {
            return By.XPath($"//li//button[text()='Add {valLOB} Opportunity']");
        }

        private By _inlineRadioRecordType(string name)
        {
            return By.XPath($"//label//div[contains(@class,'changeRecordTypeOption')]//span[text()='{name}']//ancestor::label/div/span");
        }
        private By _btnRadioIPRecordType(string type)
        {
            return By.XPath($"//h2[text()='New Investment Preference']/..//label//span[text()='{type}']");
        }
        private By _IPSidePanelLV(string name)
        {
            return By.XPath($"//h2//span[text()='{name}']");
        }
        private By _IPSidePanelActionIconLV(string name)
        {
            return By.XPath($"//h2//span[text()='{name}']//ancestor::div[contains(@class,'firstHeaderRow')]/..//button");
        }
        private By _sidePanelActionLV(string action)
        {
            return By.XPath($"//lightning-button-menu[contains(@class,'slds-is-open')]//a/span[text()='{action}']");
        }

        public bool IsIPContactInputFieldDisplayedLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, inputIPContactL, 10);
                return driver.FindElement(inputIPContactL).Displayed;
            }
            catch(NoSuchElementException)
            {
                return false;

            }
        }
        public bool IsSectorInputFieldDisplayedLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, inputSectorL, 10);
                return driver.FindElement(inputSectorL).Displayed;
            }
            catch(NoSuchElementException)
            {
                return false;
            }
        }
        public void AddInvestmentPreferenceContactLV(string name, string role)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, inputIPContactL, 10);
            driver.FindElement(inputIPContactL).SendKeys(name);
            By contactOption = By.XPath($"//div[@role='listbox']//ul//li//lightning-base-combobox-formatted-text[@title='{name}']");
            WebDriverWaits.WaitUntilEleVisible(driver, contactOption, 10);
            driver.FindElement(contactOption).Click();
            driver.FindElement(inputIPContactRoleL).SendKeys(role);
            driver.FindElement(btnSaveDetailsL).Click();
        }
        public void DeleteIPLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteIPL, 10);
            driver.FindElement(btnDeleteIPL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmDeleteL, 10);
            driver.FindElement(btnConfirmDeleteL).Click();
        }
        public void AddInvestmentPreferenceSectorLV(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, inputSectorL, 10);
            driver.FindElement(inputSectorL).SendKeys(value);
            By listSectorOption = By.XPath($"//div[@role='listbox']//ul//li//lightning-base-combobox-formatted-text[@title='{value}']");
            WebDriverWaits.WaitUntilEleVisible(driver, listSectorOption, 5);
            driver.FindElement(listSectorOption).Click();
            driver.FindElement(btnSaveDetailsL).Click();
        }
        public bool AreAllOwnershipFoundLV(string fileName)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + fileName;
            bool isEqual = false;
            int rowColumn, index;
            WebDriverWaits.WaitUntilEleVisible(driver, btnOwnershipL, 10);
            driver.FindElement(btnOwnershipL).Click();
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboOwnershipOptionsL);
            var actualValue = valTypes.Select(x => x.GetAttribute("title")).ToArray();
            rowColumn = ReadExcelData.GetRowCount(excelPath, "Ownership");
            string[] expectedValue = new string[rowColumn - 1];
            for(int row = 2; row <= rowColumn; row++)
            {
                index = row - 2;
                string typeName = actualValue[index];
                string valueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Ownership", row, 1);
                //expectedValue[index] = ReadExcelData.ReadDataMultipleRows(excelPath, "InvestorTypes", row, 1);

                if(valueExl == "None")
                {
                    valueExl = "--" + valueExl + "--";
                }
                expectedValue[index] = valueExl;
                string expectedval = expectedValue[index];
            }
            isEqual = actualValue.SequenceEqual(expectedValue);
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("arguments[0].click();", driver.FindElement(btnCancelL));
            return isEqual;
        }

        public bool AreAllInvestorTypeFoundLV(string fileName)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + fileName;
            bool isEqual = false;
            int rowColumn, index;
            WebDriverWaits.WaitUntilEleVisible(driver, btnIvestorTypeL, 10);
            driver.FindElement(btnIvestorTypeL).Click();
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboIvestorTypeOptionsL);
            var actualValue = valTypes.Select(x => x.GetAttribute("title")).ToArray();
            rowColumn = ReadExcelData.GetRowCount(excelPath, "InvestorTypes");
            string[] expectedValue = new string[rowColumn - 1];
            for(int row = 2; row <= rowColumn; row++)
            {
                index = row - 2;
                string typeName = actualValue[index];
                string valueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "InvestorTypes", row, 1);
                //expectedValue[index] = ReadExcelData.ReadDataMultipleRows(excelPath, "InvestorTypes", row, 1);

                if(valueExl == "None")
                {
                    valueExl = "--" + valueExl + "--";
                }
                expectedValue[index] = valueExl;
                string expectedval = expectedValue[index];
            }
            isEqual = actualValue.SequenceEqual(expectedValue);
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("arguments[0].click();", driver.FindElement(btnCancelL));
            return isEqual;
        }
        public bool AreAllUseOfProceedsFoundLV(string fileName)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + fileName;
            bool isEqual = false;
            int rowColumn, index;
            WebDriverWaits.WaitUntilEleVisible(driver, btnUseOfProceedsL, 10);
            driver.FindElement(btnUseOfProceedsL).Click();
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboUseOfProceedsOptionsL);
            var actualValue = valTypes.Select(x => x.GetAttribute("title")).ToArray();
            rowColumn = ReadExcelData.GetRowCount(excelPath, "UseofProceeds");
            string[] expectedValue = new string[rowColumn - 1];
            for(int row = 2; row <= rowColumn; row++)
            {
                index = row - 2;
                string typeName = actualValue[index];
                string valueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "UseofProceeds", row, 1);
                //expectedValue[index] = ReadExcelData.ReadDataMultipleRows(excelPath, "InvestorTypes", row, 1);

                if(valueExl == "None")
                {
                    valueExl = "--" + valueExl + "--";
                }
                expectedValue[index] = valueExl;
                string expectedval = expectedValue[index];
            }
            isEqual = actualValue.SequenceEqual(expectedValue);
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelL, 10);
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("arguments[0].click();", driver.FindElement(btnCancelL));

            return isEqual;
        }
        public void CreateNewIPInvestmentType(string type)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnInvestmentTypeL, 10);
            driver.FindElement(btnInvestmentTypeL).Click();
            By elmOption = By.XPath($"//label[text()='Investment Type']/..//lightning-base-combobox-item//span/span[@title='{type}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmOption, 10);
            driver.FindElement(elmOption).Click();
            driver.FindElement(btnSaveDetailsL).Click();

        }
        public void CreateNewIPInvestorType(string type)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnIvestorTypeL, 10);
            driver.FindElement(btnIvestorTypeL).Click();
            By elmOption = By.XPath($"//label[text()='Investor Type']/..//lightning-base-combobox-item//span/span[@title='{type}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmOption, 10);
            driver.FindElement(elmOption).Click();
            driver.FindElement(btnSaveDetailsL).Click();
        }
        public void CreateNewIPUseOfProceeds(string proceeds)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnUseOfProceedsL, 10);
            driver.FindElement(btnUseOfProceedsL).Click();
            By elmOption = By.XPath($"//label[text()='Use of Proceeds']/..//lightning-base-combobox-item//span/span[@title='{proceeds}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmOption, 10);
            driver.FindElement(elmOption).Click();
            driver.FindElement(btnSaveDetailsL).Click();
        }
        public void CreateNewIPOwnership(string ownership)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnOwnershipL, 10);
            driver.FindElement(btnOwnershipL).Click();
            By elmOption = By.XPath($"//label[text()='Ownership']/..//lightning-base-combobox-item//span/span[@title='{ownership}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmOption, 10);
            driver.FindElement(elmOption).Click();
            driver.FindElement(btnSaveDetailsL).Click();
        }
        public bool AreAllInvestmentTypesFoundLV(string fileName)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + fileName;
            bool isEqual = false;
            int rowColumn, index;
            WebDriverWaits.WaitUntilEleVisible(driver, btnInvestmentTypeL, 10);
            driver.FindElement(btnInvestmentTypeL).Click();
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboInvestmentTypeOptionsL);
            var actualValue = valTypes.Select(x => x.GetAttribute("title")).ToArray();
            rowColumn = ReadExcelData.GetRowCount(excelPath, "InvestmentTypes");
            string[] expectedValue = new string[rowColumn - 1];
            for(int row = 2; row <= rowColumn; row++)
            {
                index = row - 2;
                string typeName = actualValue[index];
                string valueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "InvestmentTypes", row, 1);
                //expectedValue[index] = ReadExcelData.ReadDataMultipleRows(excelPath, "InvestorTypes", row, 1);

                if(valueExl == "None")
                {
                    valueExl = "--" + valueExl + "--";
                }
                expectedValue[index] = valueExl;
                string expectedval = expectedValue[index];
            }
            isEqual = actualValue.SequenceEqual(expectedValue);
            //WebDriverWaits.WaitUntilEleVisible(driver, btnCancelL, 10);
            //driver.FindElement(btnCancelL).Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("arguments[0].click();", driver.FindElement(btnCancelL));

            return isEqual;
        }
        public void ClickSidePanelActionIconLV(string sectionName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _IPSidePanelActionIconLV(sectionName), 10);
            driver.FindElement(_IPSidePanelActionIconLV(sectionName)).Click();
        }

        public void ClickSidePanelActionLV(string action)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _sidePanelActionLV(action), 10);
            driver.FindElement(_sidePanelActionLV(action)).Click();
        }
        public void SelectIPTypeLV(string type)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _btnRadioIPRecordType(type), 10);
            driver.FindElement(_btnRadioIPRecordType(type)).Click();
            driver.FindElement(btnNextL).Click();
        }
        public void ClickNewIPButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDialogNewIPL, 10);
            driver.FindElement(btnDialogNewIPL).Click();
        }

        public void ClickSaveIPDefaultDetailsLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 10);
            driver.FindElement(btnSaveDetailsL).Click();
        }

        public string GetIPNameLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtIPNameL, 10);
            return driver.FindElement(txtIPNameL).Text;
        }
        public string GetIPTypeNumberLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtIPTypeNumberL, 10);
            return driver.FindElement(txtIPTypeNumberL).Text;
        }
        public string GetIPInvestorNumberLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtIPInvestorNumberL, 10);
            return driver.FindElement(txtIPInvestorNumberL).Text;
        }
        public string GetIPUseOfProceedsNumberLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtIPUseOfProceedsNumberL, 10);
            return driver.FindElement(txtIPUseOfProceedsNumberL).Text;
        }
        public string GetIPOwnershipNumberLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtIPOwnershipNumberL, 10);
            return driver.FindElement(txtIPOwnershipNumberL).Text;
        }
        public string GetIPContactNumberLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtIPContactNumberL, 10);
            return driver.FindElement(txtIPContactNumberL).Text;
        }
        public string GetIPInvestmentTypeSidePanelLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtIPInvestmentTypePanelL, 10);
            return driver.FindElement(txtIPInvestmentTypePanelL).Text;
        }
        public string GetIPInvestorTypeSidePanelLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtIPInvestorTypePanelL, 10);
            return driver.FindElement(txtIPInvestorTypePanelL).Text;
        }
        public string GetIPUseOfProceedsSidePanelLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtIPUseOfProceedsPanelL, 10);
            return driver.FindElement(txtIPUseOfProceedsPanelL).Text;
        }
        public string GetIPOwnershipSidePanelLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtIPOwnershipPanelL, 10);
            return driver.FindElement(txtIPOwnershipPanelL).Text;
        }
        public string GetIPContactNameLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtIPContactL, 10);
            string lastName = driver.FindElement(txtIPContactL).Text.Split(',')[0].Trim();
            string firstName = driver.FindElement(txtIPContactL).Text.Split(',')[1].Trim();
            return firstName + " " + lastName;

        }
        public string GetIPSectorLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtIPSectorL, 10);
            return driver.FindElement(txtIPSectorL).Text;
        }
        public string GetIPSectorSidePaneIDLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtIPSectorSidePanelL, 10);
            return driver.FindElement(txtIPSectorSidePanelL).Text;

        }
        public string GetIPSectorTypeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtsectorTypeL, 10);
            return driver.FindElement(txtsectorTypeL).Text;
        }

        public bool IsOpportunityTabDisplayedLV()
        {
            driver.SwitchTo().DefaultContent();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tabOppL, 10);
                return driver.FindElement(tabOppL).Displayed;
            }
            catch { return false; }

        }
        public void ClickOpportunitiesTabLV()
        {
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, tabOppL, 10);
            driver.FindElement(tabOppL).Click();
        }
        public bool IsOpportunityDisplayed(string oppName)
        {
            By elmOppName = By.XPath($"//table//th[@data-label='Opportunity Name']//a[text()='{oppName}']");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, elmOppName, 10);
                return driver.FindElement(elmOppName).Displayed;
            }
            catch { return false; }
        }

        public bool IsPrintThisPageLinkDisplayedLV()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkPrintPageL, 10);
            return driver.FindElement(linkPrintPageL).Displayed;
        }
        public bool IsPrintableViewButtonDisplayed()
        {
            try
            {
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnPrintableViewL, 5);
                return driver.FindElement(btnPrintableViewL).Displayed;
            }
            catch(Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButonL, 5);
                driver.FindElement(iconExpandMoreButonL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, linkPrintableViewL, 5);
                return driver.FindElement(linkPrintableViewL).Displayed;
            }
        }
        public void ClickPritableViewLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            try
            {
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnPrintableViewL, 5);
                driver.FindElement(btnPrintableViewL).Click();
            }
            catch(Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButonL, 10);
                driver.FindElement(iconExpandMoreButonL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, linkPrintableViewL, 10);
                driver.FindElement(linkPrintableViewL).Click();
            }
            Thread.Sleep(2000);
        }
        public string GetBoardMemberAffiliationNotesLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, recordAffNotesL, 10);
            return driver.FindElement(recordAffNotesL).Text;
        }

        public void UpdateAffiliationNotesLV(string notes)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, inputAffNotesL, 10);
            driver.FindElement(inputAffNotesL).SendKeys(notes);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 10);
            driver.FindElement(btnSaveDetailsL).Click();
        }

        public string GetBoardMemberAffiliationNameLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, recordBoardMemberL, 10);
            return driver.FindElement(recordBoardMemberL).Text;
        }
        public string GetBoardMemberAffiliationTypeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, recordStatusTypeL, 10);
            return driver.FindElement(recordStatusTypeL).Text;
        }

        public string GetAffiliationNumberLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtNumberRelationshipL, 10);
            return driver.FindElement(txtNumberRelationshipL).Text;
        }
        public string GetAffiliationTypeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtAffTypeL, 10);
            return driver.FindElement(txtAffTypeL).Text;
        }

        public string CreateNewAffiliationLV(string name, string type)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, inputAffContactL, 10);
            driver.FindElement(inputAffContactL).SendKeys(name);
            Thread.Sleep(2000);
            By eleOptionName = By.XPath($"//input[@placeholder='Search Contacts...']/../../../..//ul//lightning-base-combobox-formatted-text[@title='{name}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleOptionName, 10);
            driver.FindElement(eleOptionName).Click();
            driver.FindElement(btnAffTypeL).Click();
            Thread.Sleep(2000);
            By eleOptionType = By.XPath($"//button[@aria-label='Type']/../..//lightning-base-combobox-item[@data-value='{type}']");
            driver.FindElement(eleOptionType).Click();
            Thread.Sleep(2000);
            driver.FindElement(btnSaveDetailsL).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            return toasMsg;
        }

        public bool IsBoardMembersHeaderDisplayedLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, headerBML, 10);
                return driver.FindElement(headerBML).Displayed;
            }
            catch { return false; }
        }
        public void ClickSaveNewAffiliationButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 10);
            driver.FindElement(btnSaveDetailsL).Click();
        }
        public void ClickCancelNewAffiliationButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelL, 10);
            driver.FindElement(btnCancelL).Click();
        }
        public string GetNewAffiliationReqFieldsLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtReqFields, 10);
            IList<IWebElement> fieldLevelErrors = driver.FindElements(txtReqFields);
            string formatedReqFieldLabels = "";
            foreach(IWebElement txtFieldLevelError in fieldLevelErrors)
            {
                string fieldLevelError = txtFieldLevelError.Text;
                string formatedfieldLevelLabels = Regex.Replace(fieldLevelError, @"\t|\n|\r", "");
                formatedReqFieldLabels = formatedReqFieldLabels + formatedfieldLevelLabels;
            }
            //driver.FindElement(iconCloseErrorL).Click();
            Thread.Sleep(2000);
            return formatedReqFieldLabels;
        }

        public void ClickBoardMembersTabLV()
        {
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, tabBML, 10);
            driver.FindElement(tabBML).Click();
        }
        public void ClickNewBoardMemberButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewBML, 10);
            driver.FindElement(btnNewBML).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerNewAffL, 20);
        }

        public string GetPrefilledAffCompanyNameLV()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtAffCompanyNameL, 10);
            string prefilledClientName = driver.FindElement(txtAffCompanyNameL).GetAttribute("data-value");
            return prefilledClientName;
        }
        public string GetPrefilledAffStatusLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAffStatusL, 10);
            string prefilledClientName = driver.FindElement(valAffStatusL).GetAttribute("data-value");
            return prefilledClientName;
        }

        public string GetClientNumberLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtClientNumberL, 20);
            return driver.FindElement(txtClientNumberL).Text;
        }
        public string GetParentCompanyLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtParentCompanyL, 20);
            return driver.FindElement(txtParentCompanyL).Text;
        }

        public void UpdateClientNumberAndParentCompanyLV(string clientNumber, string parentCompany)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, inputClientNumberL, 20);
            driver.FindElement(inputClientNumberL).SendKeys(clientNumber);
            driver.FindElement(inputParentCompanyL).Click();
            driver.FindElement(inputParentCompanyL).SendKeys(parentCompany);
            By elePC = By.XPath($"//label[text()='Parent Company']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{parentCompany}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elePC, 20);
            driver.FindElement(elePC).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 20);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(8000);
        }

        public void ClickNewButtonCompanyFinancialsLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewHLCompanyFinancialsL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(btnNewHLCompanyFinancialsL));
        }


        public string GetHLFinancialNameLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtHLFinNameL, 10);
            return driver.FindElement(txtHLFinNameL).Text;
        }
        public string EditHLFinancialRecordLV(string financialName, string revenue)
        {
            By elmFinancialNameMoreActionIcon = By.XPath($"//table[@aria-label='HL Company Financials']//tbody//th//a//span/slot/span[text()='{financialName}']//ancestor::tr//button[contains(@class,'slds-button_icon-border')]");
            WebDriverWaits.WaitUntilEleVisible(driver, elmFinancialNameMoreActionIcon, 10);
            driver.FindElement(elmFinancialNameMoreActionIcon).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditL, 5);
            driver.FindElement(lnkEditL).Click();
            string latestDate = DateTime.Today.AddDays(1).ToString("M/d/yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, inputAsOfDate, 10);
            driver.FindElement(inputAsOfDate).Clear();
            driver.FindElement(inputAsOfDate).SendKeys(latestDate);
            WebDriverWaits.WaitUntilEleVisible(driver, inputRevenueL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(inputRevenueL));
            driver.FindElement(inputRevenueL).Clear();
            driver.FindElement(inputRevenueL).SendKeys(revenue);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 10);
            driver.FindElement(btnSaveDetailsL).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            return latestDate;
        }
        public string DeleteHLFinancialRecordLV(string tabname, string financialName)
        {
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, _homePageTab(tabname), 10);
            driver.FindElement(_homePageTab(tabname)).Click();
            Thread.Sleep(2000);
            By elmFinancialNameMoreActionIcon = By.XPath($"//table[@aria-label='HL Company Financials']//tbody//th//a//span/slot/span[text()='{financialName}']//ancestor::tr//button[contains(@class,'slds-button_icon-border')]");
            WebDriverWaits.WaitUntilEleVisible(driver, elmFinancialNameMoreActionIcon, 10);
            driver.FindElement(elmFinancialNameMoreActionIcon).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDeleteL, 5);
            driver.FindElement(lnkDeleteL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmDeleteL, 5);
            driver.FindElement(btnConfirmDeleteL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            return toasMsg;
        }
        public string GetHLFinancialRecordRevenueLV(string financialName)
        {
            By elmFinancialRevenue = By.XPath($"//table[@aria-label='HL Company Financials']//tbody//th//a//span/slot/span[text()='{financialName}']//ancestor::tr//td[@data-label='Revenue']//div//span");
            WebDriverWaits.WaitUntilEleVisible(driver, elmFinancialRevenue, 10);
            return driver.FindElement(elmFinancialRevenue).Text.Split(' ')[1].Trim();
        }
        public string GetHLFinancialRecordAsOfDateLV(string financialName)
        {
            By elmFinancialRevenue = By.XPath($"//table[@aria-label='HL Company Financials']//tbody//th//a//span/slot/span[text()='{financialName}']//ancestor::tr//td[@data-label='As of Date']//div//lightning-formatted-date-time");
            WebDriverWaits.WaitUntilEleVisible(driver, elmFinancialRevenue, 10);
            return driver.FindElement(elmFinancialRevenue).Text.Trim();
        }
        public string AddNewHLCompanyFinancialLV(string year, string source, string revenue, string EBITDA, string dataSource)
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboYearL, 5);
            driver.FindElement(comboYearL).Click();
            Thread.Sleep(2000);
            By elmYear = By.XPath($"//label[text()='Year']/..//lightning-base-combobox-item//span[text()='{year}']");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, elmYear, 5);
            }
            catch
            {
                driver.FindElement(comboYearL).Click();
                elmYear = By.XPath($"//label[text()='Year']/..//lightning-base-combobox-item//span[text()='{year}']");
                WebDriverWaits.WaitUntilEleVisible(driver, elmYear, 5);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, elmYear, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmYear));
            driver.FindElement(elmYear).Click();
            Thread.Sleep(2000);
            driver.FindElement(comboSourceL).Click();
            By elmSource = By.XPath($"//label[text()='Source']/..//lightning-base-combobox-item//span[text()='{source}']");
            Thread.Sleep(2000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, elmSource, 5);
            }
            catch
            {
                driver.FindElement(comboSourceL).Click();
                elmSource = By.XPath($"//label[text()='Source']/..//lightning-base-combobox-item//span[text()='{source}']");
                WebDriverWaits.WaitUntilEleVisible(driver, elmSource, 5);
            }
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmSource));
            driver.FindElement(elmSource).Click();

            string todayDate = DateTime.Today.AddDays(0).ToString("MM/dd/yyyy");
            driver.FindElement(inputAsOfDate).SendKeys(todayDate);

            CustomFunctions.MoveToElement(driver, driver.FindElement(inputRevenueL));
            driver.FindElement(inputRevenueL).SendKeys(revenue);

            CustomFunctions.MoveToElement(driver, driver.FindElement(inputEBITDAL));
            driver.FindElement(inputEBITDAL).SendKeys(EBITDA);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboDataSourceL));
            WebDriverWaits.WaitUntilEleVisible(driver, comboDataSourceL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(comboDataSourceL));
            //driver.FindElement(comboDataSourceL).Click();

            By elmDataSource = By.XPath($"//label[text()='Data Source']/..//lightning-base-combobox-item//span[text()='{dataSource}']");
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, elmDataSource, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmDataSource));
            js.ExecuteScript("arguments[0].click();", driver.FindElement(elmDataSource));
            //driver.FindElement(elmDataSource).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 10);
            driver.FindElement(btnSaveDetailsL).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            return toasMsg;
        }
        public string UpdateHLFinancialRevenueLV(string revenue)
        {
            CustomFunctions.MoveToElement(driver, driver.FindElement(inputRevenueL));
            driver.FindElement(inputRevenueL).Clear();
            driver.FindElement(inputRevenueL).SendKeys(revenue);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 10);
            driver.FindElement(btnSaveDetailsL).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            return toasMsg;
        }
        public bool IsNewHLCompanyFinancialsDisplayedLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewHLCompanyFinancialsL, 10);
            return driver.FindElement(btnNewHLCompanyFinancialsL).Displayed;
        }
        public bool IsCompanyFiancialRecordsFoundLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tableFinancialsL, 10);
            int recordCount = driver.FindElements(tableFinancialsL).Count;
            if(recordCount >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsCompanyActicityRecordsFoundLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tableActivityL, 10);
            int recordCount = driver.FindElements(tableActivityL).Count;
            if(recordCount >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsCompanySectorDisplayedLV(string companySector)
        {
            By txtCompanySectorL = By.XPath($"//table//th[@data-label='Company Sector']//a//span/slot/span[text()='{companySector}']");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanySectorL, 20);
                return driver.FindElement(txtCompanySectorL).Displayed;
            }
            catch { return false; }
        }
        public string GetCompanySectorLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanySectorL, 20);
            return driver.FindElement(txtCompanySectorL).Text;
        }

        public string DeleteCompanySectorRecordLV(string tabName, string companySector)
        {
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, _homePageTab(tabName), 10);
            driver.FindElement(_homePageTab(tabName)).Click();
            Thread.Sleep(2000);
            By elmCompanySectorsMoreActionIcon = By.XPath($"//table//th[@data-label='Company Sector']//a//span/slot/span[text()='{companySector}']//ancestor::tr//td//button[contains(@class,'button_icon-border')]");
            WebDriverWaits.WaitUntilEleVisible(driver, elmCompanySectorsMoreActionIcon, 10);
            driver.FindElement(elmCompanySectorsMoreActionIcon).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDeleteL, 5);
            driver.FindElement(lnkDeleteL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmDeleteL, 5);
            driver.FindElement(btnConfirmDeleteL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            return toasMsg;
        }

        public string UpdateCompanySectorRecordLV(string companySector, string newSector)
        {
            By elmCompanySectorsMoreActionIcon = By.XPath($"//table//th[@data-label='Company Sector']//a//span/slot/span[text()='{companySector}']//ancestor::tr//td//button[contains(@class,'button_icon-border')]");
            WebDriverWaits.WaitUntilEleVisible(driver, elmCompanySectorsMoreActionIcon, 10);
            driver.FindElement(elmCompanySectorsMoreActionIcon).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditL, 5);
            driver.FindElement(lnkEditL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnClearSectorCategL, 5);
            driver.FindElement(btnClearSectorCategL).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, inputsectorCategL, 10);
            driver.FindElement(inputsectorCategL).SendKeys(newSector);
            By eleSector = By.XPath($"//label[text()='Sector Categorization']/..//lightning-base-combobox-formatted-text[@title='{newSector}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleSector, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleSector));
            driver.FindElement(eleSector).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            return toasMsg;

        }
        public string AddNewCompanySectorLV(string sector)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCompanySectorsL, 10);
            driver.FindElement(btnNewCompanySectorsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, inputsectorCategL, 10);
            driver.FindElement(inputsectorCategL).SendKeys(sector);
            By eleSector = By.XPath($"//label[text()='Sector Categorization']/..//lightning-base-combobox-formatted-text[@title='{sector}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleSector, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleSector));
            driver.FindElement(eleSector).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            return toasMsg;
        }

        By addLocationRecordL = By.XPath("//table[@aria-label='Address']//tbody//tr[1]//slot//slot");
        By txtAddLocationL = By.XPath("//records-entity-label[text()='Address']/../../..//lightning-formatted-text");
        By btnEditAddLocationL = By.XPath("(//button[@name='Edit'])[2]");
        public string UpdateAddLocationLV(string addressType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditAddLocationL, 5);
            driver.FindElement(btnEditAddLocationL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboAddressTypeL, 10);
            driver.FindElement(comboAddressTypeL).Click();
            Thread.Sleep(1000);
            By eleAddType = By.XPath($"//label[text()='Address Type']/..//lightning-base-combobox-item//span[@title='{addressType}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleAddType, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleAddType));
            driver.FindElement(eleAddType).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            return toasMsg;
        }
        public void DeleteAddLocationLV()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddDeleteL, 5);
            driver.FindElement(btnAddDeleteL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmDeleteL, 10);
            driver.FindElement(btnConfirmDeleteL).Click();
            Thread.Sleep(3000);
        }
        public void SelectAddLocationRecordLV(string addLocationID)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            By elmAddLocationRecord = By.XPath($"//table[@aria-label='Address']//tbody//tr//slot//slot/span[text()='{addLocationID}']//ancestor::a");
            WebDriverWaits.WaitUntilEleVisible(driver, elmAddLocationRecord, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmAddLocationRecord));
            js.ExecuteScript("arguments[0].click();", driver.FindElement(elmAddLocationRecord));
            //driver.FindElement(elmAddLocationRecord).Click();
        }
        public bool IsAddLocationRecordDisplayedLV(string addLocationID)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, addLocationRecordL, 10);
            By elmAddLocationRecord = By.XPath($"//table[@aria-label='Address']//tbody//tr//slot//slot/span[text()='{addLocationID}']//ancestor::a");

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, elmAddLocationRecord, 5);
                CustomFunctions.MoveToElement(driver, driver.FindElement(elmAddLocationRecord));
                return driver.FindElement(elmAddLocationRecord).Displayed;
            }
            catch { return false; }
        }
        public string GetAddLocationIDLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtAddLocationL, 10);
            return driver.FindElement(txtAddLocationL).Text;
        }
        By inputAddressL = By.XPath("(//label[text()='Address Search']/..//input)[1]");
        By comboAddressL = By.XPath("(//label[text()='Address Search']/..//input/../../../..)//lightning-base-combobox-item[1]");
        public string AddNewCompanyLocationLV(string addressType, string address)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewAddLocationL, 10);
            driver.FindElement(btnNewAddLocationL).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, comboAddressTypeL, 10);
            driver.FindElement(comboAddressTypeL).Click();
            By eleAddType = By.XPath($"//label[text()='Address Type']/..//lightning-base-combobox-item//span[@title='{addressType}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleAddType, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleAddType));
            Thread.Sleep(2000);
            driver.FindElement(eleAddType).Click();
            CustomFunctions.MoveToElement(driver, driver.FindElement(inputAddressL));
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, inputAddressL, 10);
            driver.FindElement(inputAddressL).SendKeys(address);
            WebDriverWaits.WaitUntilEleVisible(driver, comboAddressL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboAddressL));
            WebDriverWaits.WaitUntilEleVisible(driver, comboAddressL, 10);
            driver.FindElement(comboAddressL).Click();
            Thread.Sleep(2000);
            driver.FindElement(btnSaveDetailsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            //Thread.Sleep(2000);
            return toasMsg;
        }

        public bool IsLocationAddressRecordPresentLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, addLocationRecordL, 10);
                return driver.FindElement(addLocationRecordL).Displayed;
            }
            catch { return false; }
        }

        public bool IsCompanyAddLocationNewButtonDisplayedLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnNewAddLocationL, 10);
                return driver.FindElement(btnNewAddLocationL).Displayed;
            }
            catch { return false; }
        }
        public bool IsCompanySectorsNewButtonDisplayedLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnNewCompanySectorsL, 10);
                return driver.FindElement(btnNewCompanySectorsL).Displayed;
            }
            catch { return false; }
        }
        public bool IsInvestmentPreferencesNewButtonDisplayedLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnNewInvestmentPreferencesL, 10);
                return driver.FindElement(btnNewInvestmentPreferencesL).Displayed;
            }
            catch { return false; }
        }
        public bool IsSubTabDisplayedLV(string subTabName)
        {
            By eleSubTab = By.XPath($"//span[@class='gridLayoutWidget text'][text()='{subTabName}']");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, eleSubTab, 10);
                return driver.FindElement(eleSubTab).Displayed;
            }
            catch { return false; }
        }
        public void ClickSubTabLV(string subTabName)
        {
            By eleSubTab = By.XPath($"//span[@class='gridLayoutWidget text'][text()='{subTabName}']/..");
            WebDriverWaits.WaitUntilEleVisible(driver, eleSubTab, 10);
            driver.FindElement(eleSubTab).Click();
        }
        public void ClickInvestmentPrefrenceNewButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewInvestmentPreferencesL, 10);
            driver.FindElement(btnNewInvestmentPreferencesL).Click();
        }
        By txtRecordTypeL = By.XPath("//div[@class='changeRecordTypeCenter']//label//span[2]");
        By btnCancelRecordTypeL = By.XPath("//div[contains(@class,'ChangeRecordTypeFooter')]//button[1]");

        public bool AreInvestmentPreferenceTypesPresentLV(string file)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtRecordTypeL, 10);
            IList<IWebElement> listRecordTypes = driver.FindElements(txtRecordTypeL);
            bool recordTypeFound = false;
            int rowOpp = ReadExcelData.GetRowCount(excelPath, "IPTypes");
            foreach(IWebElement txtRecordType in listRecordTypes)
            {
                recordTypeFound = false;
                string actualRecordType = txtRecordType.Text.Trim();
                for(int row = 2; row <= rowOpp; row++)
                {
                    string valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "IPTypes", row, 1);
                    if(actualRecordType == valRecordTypeExl)
                    {
                        recordTypeFound = true;
                        break;
                    }
                }
            }
            driver.FindElement(btnCancelRecordTypeL).Click();
            return recordTypeFound;
        }
        public void ClickIndustryCoverageTamMemberLV(string officeName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtICofficerNameL, 10);
            IReadOnlyCollection<IWebElement> valRecords = driver.FindElements(txtICofficerNameL);
            var actualValue = valRecords.Select(x => x.Text).ToArray();
            foreach(IWebElement record in valRecords)
            {
                if(record.Text == officeName)
                {
                    record.Click();
                    Thread.Sleep(5000);
                    break;
                }
            }
        }
        public void ClickSponsorCoverageTamMemberLV(string officeName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSCOfficerNameL, 10);
            IReadOnlyCollection<IWebElement> valRecords = driver.FindElements(txtSCOfficerNameL);
            var actualValue = valRecords.Select(x => x.Text).ToArray();
            foreach(IWebElement record in valRecords)
            {
                if(record.Text == officeName)
                {
                    record.Click();
                    Thread.Sleep(5000);
                    break;
                }
            }
        }

        public string GetIndustryCoverageTeamMemberTierLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtICTierL, 10);
            return driver.FindElement(txtICTierL).Text;
        }

        public string GetSponsorCoverageTeamMemberTierLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSCTierL, 10);
            return driver.FindElement(txtSCTierL).Text;
        }


        public void ClickEditLinkICRecordLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            WebDriverWaits.WaitUntilEleVisible(driver, iconICExpandMoreButonL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(iconICExpandMoreButonL));

            //driver.FindElement(iconICExpandMoreButonL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkEditL, 5);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(linkEditL));

            //driver.FindElement(linkEditL).Click();
        }
        public void ClickEditLinkSCRecordLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            WebDriverWaits.WaitUntilEleVisible(driver, iconSCExpandMoreButonL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(iconSCExpandMoreButonL));

            //driver.FindElement(iconICExpandMoreButonL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkEditL, 5);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(linkEditL));

            //driver.FindElement(linkEditL).Click();
        }

        public bool IsIndustryCoverageTamMemberDisplayedRelatedTabListLV(string officeName)
        {
            bool isFound = false;
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtICofficerNameL, 10);
                IReadOnlyCollection<IWebElement> valRecords = driver.FindElements(txtICofficerNameL);
                var actualValue = valRecords.Select(x => x.Text).ToArray();
                for(int row = 0; row <= actualValue.Length; row++)
                {
                    if(actualValue[row] == officeName)
                    {
                        isFound = true;
                        break;
                    }
                }
                return isFound;
            }
            catch { return isFound; }

        }
        public bool IsSponsorCoverageTamMemberDisplayedRelatedTabListLV(string officeName)
        {
            bool isFound = false;
            WebDriverWaits.WaitUntilEleVisible(driver, txtSCOfficerNameL, 10);
            IReadOnlyCollection<IWebElement> valRecords = driver.FindElements(txtSCOfficerNameL);
            var actualValue = valRecords.Select(x => x.Text).ToArray();
            for(int row = 0; row <= actualValue.Length; row++)
            {
                if(actualValue[row] == officeName)
                {
                    isFound = true;
                    break;
                }
            }
            return isFound;

        }

        public void ClickSaveNewCoverageTeamButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 10);
            driver.FindElement(btnSaveDetailsL).Click();
        }

        public void ClickNewButtonSponsorCoverageDisplayedLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewSponsorCoverageL, 10);
            driver.FindElement(btnNewSponsorCoverageL).Click();
        }
        public bool IsNewButtonSponsorCoverageDisplayedLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewSponsorCoverageL, 10);
            return driver.FindElement(btnNewSponsorCoverageL).Displayed;
        }
        public bool IsNewButtonIndustryCoverageDisplayedLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewIndustryCoverageL, 10);
            return driver.FindElement(btnNewIndustryCoverageL).Displayed;
        }
        public bool IsAddFilesButtonDisplayedLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnAddFilesL, 5);
                return driver.FindElement(btnAddFilesL).Displayed;
            }
            catch { return false; }
        }

        public bool IsNewFSFundsButtonDisplayedLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnNewFSFundsL, 5);
                return driver.FindElement(btnNewFSFundsL).Displayed;
            }
            catch { return false; }
        }

        public bool IsNewFinancialSPButtonDisplayedLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnNewFinancialsSPL, 5);
                return driver.FindElement(btnNewFinancialsSPL).Displayed;
            }
            catch { return false; }
        }
        public void ClickFinancialSponsorsNewButtonDisplayedLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewFinancialsSPL, 5);
            driver.FindElement(btnNewFinancialsSPL).Click();
        }


        public void AddNewCompanyFinancialsSponsorsLV(string status)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnInvestStatusL, 10);
            driver.FindElement(btnInvestStatusL).Click();
            By optionsStatus = By.XPath($"//button[@aria-label='Status']/../..//lightning-base-combobox-item//span[@title='{status}']");
            WebDriverWaits.WaitUntilEleVisible(driver, optionsStatus, 10);
            driver.FindElement(optionsStatus).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 10);
            driver.FindElement(btnSaveDetailsL).Click();
        }

        public string GetFinancialSponsorsNameLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSPNameL, 5);
            return driver.FindElement(txtSPNameL).Text;
        }

        public bool IsFinancialSPRecordDisplayedLV(string investmentNumber)
        {
            Thread.Sleep(5000);
            By fundRecord = By.XPath($"//table//tbody//th[@data-label='Investment List Number']//a//span[text()='{investmentNumber}']");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, fundRecord, 10);
                return driver.FindElement(fundRecord).Displayed;
            }
            catch { return false; }
        }
        public string GetFinancialSPSectionLV(string investmentNumber)
        {
            By sectionRecord = By.XPath($"//table//tbody//th[@data-label='Investment List Number']//a//span[text()='{investmentNumber}']//ancestor::article//h2//span");
            WebDriverWaits.WaitUntilEleVisible(driver, sectionRecord, 10);
            return driver.FindElement(sectionRecord).GetAttribute("title");

        }

        public void EditFinancialSPRecordLV(string investmentNumber, string InvestmentAmount)
        {
            By fundRecord = By.XPath($"//table//tbody//th[@data-label='Investment List Number']//a//span[text()='{investmentNumber}']");
            WebDriverWaits.WaitUntilEleVisible(driver, fundRecord, 10);
            driver.FindElement(fundRecord).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditInvestmentL, 10);
            driver.FindElement(btnEditInvestmentL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, inputInvestmentAmountL, 10);
            driver.FindElement(inputInvestmentAmountL).SendKeys(InvestmentAmount);
            Thread.Sleep(2000);
            driver.FindElement(btnSaveDetailsL).Click();
        }
        public void ClickInvestmentNumberLV(string investmentNumber)
        {
            By fundRecord = By.XPath($"//table//tbody//th[@data-label='Investment List Number']//a//span[text()='{investmentNumber}']");
            WebDriverWaits.WaitUntilEleVisible(driver, fundRecord, 10);
            driver.FindElement(fundRecord).Click();
            Thread.Sleep(5000);
        }

        public bool IsEditButtonDisplayedLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEditInvestmentL, 5);
                return driver.FindElement(btnEditInvestmentL).Displayed;
            }
            catch
            {
                return false;
            }
        }
        public bool IsDeleteButtonDisplayedLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteInvestmentL, 5);
                return driver.FindElement(btnDeleteInvestmentL).Displayed;
            }
            catch
            {
                return false;
            }
        }
        public void DeleteInvestmentLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteInvestmentL, 5);
            driver.FindElement(btnDeleteInvestmentL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmDeleteL, 5);
            driver.FindElement(btnConfirmDeleteL).Click();
        }

        public void ClickFSFundsNewButtonDisplayedLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewFSFundsL, 5);
            driver.FindElement(btnNewFSFundsL).Click();
        }
        public string GetNewFundsCompanyNameLV()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyNameL, 5);
            return driver.FindElement(valCompanyNameL).GetAttribute("data-value");
        }
        By inputDryPowderL = By.XPath("//label[text()='Dry Powder %']/..//input");
        By txtNameFSFundsL = By.XPath("//span[text()='FS Fund Name']/../../..//lightning-formatted-text");
        public void AddNewCompanyFSFundsLV(string fileName)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + fileName;
            string dryPowderExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TabName", 2, 2);
            WebDriverWaits.WaitUntilEleVisible(driver, inputDryPowderL, 5);
            driver.FindElement(inputDryPowderL).SendKeys(dryPowderExl);
            driver.FindElement(btnSaveDetailsL).Click();
        }




        public void EditCompanyFSFundsLV(string fileName)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + fileName;
            WebDriverWaits.WaitUntilEleVisible(driver, inputDryPowderL, 5);
            driver.FindElement(inputDryPowderL).Clear();
            string dryPowderExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TabName", 2, 3);
            driver.FindElement(inputDryPowderL).SendKeys(dryPowderExl);
            driver.FindElement(btnSaveDetailsL).Click();
        }

        public string GetFSFundsNameLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtNameFSFundsL, 10);
            return driver.FindElement(txtNameFSFundsL).Text;
        }
        public bool IsFSFundRecordDisplayedLV(string fundName)
        {
            By fundRecord = By.XPath($"//table[@aria-label='FS Funds']//tbody//th[@data-label='FS Fund Name']//a//span[text()='{fundName}']");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, fundRecord, 5);
                return driver.FindElement(fundRecord).Displayed;
            }
            catch { return false; }
        }
        public void FundsRecordMoreActionsLV(string fileName, string action)
        {

            WebDriverWaits.WaitUntilEleVisible(driver, _btnRecordMoreActionsL(fileName), 10);
            driver.FindElement(_btnRecordMoreActionsL(fileName)).Click();
            //Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, _lnkRecordMoreActionsOptionL(action), 10);
            driver.FindElement(_lnkRecordMoreActionsOptionL(action)).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmDeleteL, 5);
                driver.FindElement(btnConfirmDeleteL).Click();
            }
            catch
            {
                Thread.Sleep(5000);
            }
        }
        public bool IsUploadFilesButtonDisplayedLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, inputUploadFilesL, 5);
                return driver.FindElement(inputUploadFilesL).Displayed;
            }
            catch { return false; }
        }
        public bool IsUploadedFileDisplayedLV(string fileName)
        {
            string file = fileName.Split('.')[0].Trim();

            By txtFileNameL = By.XPath($"//article[@aria-label='Files']//span[@title='{file}']");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtFileNameL, 10);
                return driver.FindElement(txtFileNameL).Displayed;
            }
            catch { return false; }
        }

        public void ClickViewAllUploadedFilesLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkVIewAllUploadedFilesL, 10);
            driver.FindElement(lnkVIewAllUploadedFilesL).Click();
            Thread.Sleep(5000);
        }
        private By _lnkBtnRecordMoreActionsL(string fileName)
        {
            return By.XPath($"//table//span[text()='{fileName}']//ancestor::tr//td//a[@role='button']");
        }
        private By _lnkRecordMoreActionsOptionL(string option)
        {
            return By.XPath($"//div[contains(@class,'visible positioned')]//li//a[@title='{option}']");
        }

        private By _btnRecordMoreActionsL(string name)
        {
            return By.XPath($"//table//span[text()='{name}']//ancestor::tr//td//button");
        }

        public void UploadedFileMoreActionsLV(string fileName, string action)
        {
            string file = fileName.Split('.')[0].Trim();
            WebDriverWaits.WaitUntilEleVisible(driver, _lnkBtnRecordMoreActionsL(file), 10);
            driver.FindElement(_lnkBtnRecordMoreActionsL(file)).Click();
            //Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, _lnkRecordMoreActionsOptionL(action), 10);
            driver.FindElement(_lnkRecordMoreActionsOptionL(action)).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmDeleteL, 5);
                driver.FindElement(btnConfirmDeleteL).Click();
            }
            catch
            {
                Thread.Sleep(5000);
            }
        }

        public void ClickCoverageTabLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabCoverageL, 10);
            driver.FindElement(tabCoverageL).Click();
            Thread.Sleep(5000);
        }
        public bool IsCoverageTabDisplayedLV()
        {
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, tabCoverageL, 10);
            return driver.FindElement(tabCoverageL).Displayed;
        }

        public void DeleteBoardMemberRecordLinkLV()
        {
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabBML, 10);
            driver.FindElement(tabBML).Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            WebDriverWaits.WaitUntilEleVisible(driver, iconBMShowMoreL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(iconBMShowMoreL));
            //driver.FindElement(iconShowMoreL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkBMDeleteL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(lnkBMDeleteL));
            //driver.FindElement(lnkEditL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmDeleteL, 20);
            driver.FindElement(btnConfirmDeleteL).Click();
            Thread.Sleep(3000);
        }
        public void ClickEditBoardMemberLinkLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            WebDriverWaits.WaitUntilEleVisible(driver, iconBMShowMoreL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(iconBMShowMoreL));
            //driver.FindElement(iconShowMoreL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkBMEditL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(lnkBMEditL));
            //driver.FindElement(lnkEditL).Click();
        }
        public void ClickViewContactRelationshipLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewContactRelationshipsL, 10);
            driver.FindElement(btnViewContactRelationshipsL).Click();
        }
        public bool IsContactRelationshipReportPageDisplayedLV()
        {
            Thread.Sleep(10000);
            driver.SwitchTo().Frame(driver.FindElement(frameReportL));
            WebDriverWaits.WaitUntilEleVisible(driver, txtHeaderReportL, 10);
            bool displayed = driver.FindElement(txtHeaderReportL).Displayed;
            driver.SwitchTo().DefaultContent();
            return displayed;
        }
        public string GetHLContactLV()
        {

            driver.SwitchTo().Frame(driver.FindElement(frameReportL));
            WebDriverWaits.WaitUntilEleVisible(driver, txtHLRelationContactL, 15);
            string name = driver.FindElement(txtHLRelationContactL).Text;
            driver.SwitchTo().DefaultContent();
            return name;
        }

        public string GetHLRelationshipContactLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtHLRelationshipContactL, 10);
            return driver.FindElement(txtHLRelationshipContactL).Text.Trim();
        }
        public void ClickViewNestedRContactLV(string contactName)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            WebDriverWaits.WaitUntilEleVisible(driver, _btnViewNestedRContactL(contactName), 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(_btnViewNestedRContactL(contactName)));
            //driver.FindElement(_btnViewNestedRContactL(contactName)).Click();
        }
        public void ClickViewDetailsRelationshipContactLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkViewDetailsRelationshipL, 10);
            driver.FindElement(linkViewDetailsRelationshipL).Click();
        }
        public string AddRelationshipLV(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, inputHLContactL, 10);
            driver.FindElement(inputHLContactL).SendKeys(name);
            Thread.Sleep(3000);
            try
            {
                By eleOptionName = By.XPath($"//input[@title='Search Contacts']/..//li//a//div[@title='{name}']");
                WebDriverWaits.WaitUntilEleVisible(driver, eleOptionName, 5);
                driver.FindElement(eleOptionName).Click();

            }
            catch(Exception ex)
            {
                By iconContactSearchItem = By.XPath("//div[contains(@class,'searchButton')]");
                WebDriverWaits.WaitUntilEleVisible(driver, iconContactSearchItem, 5);
                driver.FindElement(iconContactSearchItem).Click();
                By txtContact = By.XPath("//div[contains(@class,'gridInScroller')]//table//tbody//tr[1]//td[1]//a");
                WebDriverWaits.WaitUntilEleVisible(driver, txtContact, 20);
                driver.FindElement(txtContact).Click();
            }


            driver.FindElement(btnSaveContactL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            return toasMsg;
        }
        public void ClickAddRelationshipButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddRelationshipL, 10);
            driver.FindElement(btnAddRelationshipL).Click();
        }
        public void ClickContactnameInRelatedTabListLV(string contactFullName)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            By elmContatFullName = By.XPath($"//table//tr//td[@data-label='Full Name']/a[text()='{contactFullName}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmContatFullName, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(elmContatFullName));

            //driver.FindElement(elmContatFullName).Click();
        }
        public string CreateNewContactLV(string valFirstName, string valLastName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, inputFirstNameL, 10);
            driver.FindElement(inputFirstNameL).SendKeys(valFirstName);
            WebDriverWaits.WaitUntilEleVisible(driver, inputLastNameL, 10);
            driver.FindElement(inputLastNameL).SendKeys(valLastName);
            driver.FindElement(btnSaveContactL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 5);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            return toasMsg;
        }

        public string GetContactPhoneNumberInRelatedTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactPhoneNumberL, 10);
            return driver.FindElement(txtContactPhoneNumberL).Text;
        }
        public void UpdateContactPhoneNumberLV(string valPhoneNumber)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, inputPhoneL, 10);
            driver.FindElement(inputPhoneL).SendKeys(valPhoneNumber);
            Thread.Sleep(200);
            driver.FindElement(btnSaveDetailsL).Click();
        }
        public void ClickEditContactContactTabLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            WebDriverWaits.WaitUntilEleVisible(driver, iconContactActionsMoreL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(iconContactActionsMoreL));
            //driver.FindElement(iconContactActionsMoreL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContactActionsMoreL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(lnkContactActionsMoreL));
            //driver.FindElement(lnkContactActionsMoreL).Click();            
        }


        public bool IsContactPresentInRelatedTabListLV(string contactFullName)
        {
            bool isFound = false;
            WebDriverWaits.WaitUntilEleVisible(driver, tableFullNameL, 20);
            IReadOnlyCollection<IWebElement> valRecords = driver.FindElements(tableFullNameL);
            var actualValue = valRecords.Select(x => x.Text).ToArray();
            for(int row = 0; row <= actualValue.Length; row++)
            {
                if(actualValue[row] == contactFullName)
                {
                    isFound = true;
                    break;
                }
            }
            return isFound;

        }
        public string CreateContactLV(string valFirstName, string valLastName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, inputNewContactFirstNameL, 10);
            driver.FindElement(inputNewContactFirstNameL).SendKeys(valFirstName);
            WebDriverWaits.WaitUntilEleVisible(driver, inputNewContactLastNameL, 10);
            driver.FindElement(inputNewContactLastNameL).SendKeys(valLastName);
            driver.FindElement(btnSaveDetailsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 5);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            //driver.FindElement(toastMsgCloseIcon).Click();
            return toasMsg;
        }

        public bool GetIGSectorValidationLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, headerEditBoxL, 10);
            WebDriverWaits.WaitUntilEleVisible(driver, comboIGL, 10);
            driver.FindElement(comboIGL).Click();
            Thread.Sleep(3000);
            By eleIG = By.XPath("//label[text()='Industry Group']/following::lightning-base-combobox-item//span[@title='BUS - Business Services']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleIG));
            driver.FindElement(eleIG).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtValidationSectorL, 10);
                bool isValidation = driver.FindElement(txtValidationSectorL).Displayed;
                driver.FindElement(btnCancelL).Click();
                return isValidation;
            }
            catch
            {
                return false;
            }
        }

        public void ClickNextButtonRecordTypeLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNexRecordTypetL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(btnNexRecordTypetL));
        }

        public void ClickNewButtonContactTabLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnContactNewL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(btnContactNewL));
        }
        public bool IsContactTabDisplayedLV()
        {
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, tabContactL, 10);
            return driver.FindElement(tabContactL).Displayed;
        }
        public void ClickContactTabLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabContactL, 10);
            driver.FindElement(tabContactL).Click();
            Thread.Sleep(5000);
        }
        public bool IsNewButtonContactDisplayedLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewContactL, 10);
            return driver.FindElement(btnNewContactL).Displayed;
        }

        public void ClickNewContactLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewContactL, 10);
            driver.FindElement(btnNewContactL).Click();
        }
        public bool ValidateDuplicateAlertForCreateContactLV(string fileName)
        {
            bool alertFound = false;
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + fileName;
            string valFirstName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 1);
            string valLastName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 2);
            WebDriverWaits.WaitUntilEleVisible(driver, inputFirstNameL, 10);
            driver.FindElement(inputFirstNameL).SendKeys(valFirstName);
            WebDriverWaits.WaitUntilEleVisible(driver, inputLastNameL, 10);
            driver.FindElement(inputLastNameL).SendKeys(valLastName);
            driver.FindElement(btnSaveContactL).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, msgDuplicate, 10);
                driver.FindElement(btnContactContactL).Click();
                alertFound = true;
            }
            catch
            {
                alertFound = false;
            }
            return alertFound;
        }
        public void ClickInlineChangeRecordTypeButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnInlineChngTypeL, 10);
            driver.FindElement(btnInlineChngTypeL).Click();
        }

        public void ChangeCompanyRadioTypeLV(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _inlineRadioRecordType(name), 20);
            driver.FindElement(_inlineRadioRecordType(name)).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnNextChangeRecordTypeL, 5);
            driver.FindElement(btnNextChangeRecordTypeL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 20);
            driver.FindElement(btnSaveDetailsL).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtValidationSectorL, 5);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSectorL, 5);
                CustomFunctions.MoveToElement(driver, driver.FindElement(headrAnnFinL));
                driver.FindElement(comboSectorL).Click();
                By eleSector = By.XPath("//label[text()='Sector']/following::lightning-base-combobox-item//span[@title='Cybersecurity Services']");
                CustomFunctions.MoveToElement(driver, driver.FindElement(eleSector));
                driver.FindElement(eleSector).Click();
                driver.FindElement(btnSaveDetailsL).Click();
            }
            catch
            {

            }

            Thread.Sleep(10000);
        }

        public void ClickInfoTabLV()
        {
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, tabInfoL, 20);
            driver.FindElement(tabInfoL).Click();
        }

        public bool AreAllSectionsDisplayedLV(string recordType, string fileName)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + fileName;
            bool isEqual = false;
            int countColumn, index;
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(txtsectionNameL);
            var actualValue = valTypes.Select(x => x.GetAttribute("title")).ToArray();

            if(recordType == "Operating Company")
            {
                countColumn = ReadExcelData.GetRowCount(excelPath, "OC_Sections");
                string[] expectedValue = new string[countColumn - 1];
                for(int row = 2; row <= countColumn; row++)
                {
                    index = row - 2;
                    //string sectionName = actualValue[index];
                    expectedValue[index] = ReadExcelData.ReadDataMultipleRows(excelPath, "OC_Sections", row, 1);
                }
                isEqual = actualValue.SequenceEqual(expectedValue);
            }
            else
            {
                countColumn = ReadExcelData.GetRowCount(excelPath, "CP_Sections");
                string[] expectedValue = new string[countColumn - 1];
                for(int row = 2; row <= countColumn; row++)
                {
                    index = row - 2;
                    //string sectionName = actualValue[index];
                    expectedValue[index] = ReadExcelData.ReadDataMultipleRows(excelPath, "CP_Sections", row, 1);
                }
                isEqual = actualValue.SequenceEqual(expectedValue);
            }
            return isEqual;
        }

        public void ClickInEditInlinePhoneNumberLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnInlineEditPhoneL, 10);
            driver.FindElement(btnInlineEditPhoneL).Click();
            Thread.Sleep(2000);
        }


        public bool IsPhoneEditableInlineLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, inputPhoneL, 10);
                bool editPhoneDisplayed = driver.FindElement(inputPhoneL).Displayed;
                driver.FindElement(btnCancelL).Click();
                return editPhoneDisplayed;
            }
            catch { return false; }
        }

        public void UpdatePhoneNumberInlineEditLV(string phoneNumber)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnInlineEditPhoneL, 10);
            driver.FindElement(btnInlineEditPhoneL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, inputPhoneL, 10);
            driver.FindElement(inputPhoneL).SendKeys(phoneNumber);
            Thread.Sleep(2000);
            driver.FindElement(btnSaveDetailsL).Click();
            //Thread.Sleep(8000);
        }

        public string GetCompanyPhoneNumberLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtPhoneNumberL, 10);
            return driver.FindElement(txtPhoneNumberL).Text;
        }
        public string GetCompanyTradeNameLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtTradeNameL, 10);
            return driver.FindElement(txtTradeNameL).Text;
        }

        By headerEditBoxL = By.XPath("//h2[contains(text(),'Edit')]");
        By inputTradeNameL = By.XPath("//input[contains(@name,'Trade_Name')]");
        public void EditCompanyPhoneNumberLV(string phoneNumber)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditTopPanelL, 10);
            driver.FindElement(btnEditTopPanelL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerEditBoxL, 10);
            WebDriverWaits.WaitUntilEleVisible(driver, inputPhoneL, 10);
            driver.FindElement(inputPhoneL).Clear();
            Thread.Sleep(2000);
            driver.FindElement(inputPhoneL).SendKeys(phoneNumber);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(5000);
        }

        public void ClickEditCompanyButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditTopPanelL, 10);
            driver.FindElement(btnEditTopPanelL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerEditBoxL, 10);
        }
        By comboIGL = By.XPath("//label[text()='Industry Group']/parent::div//button");
        By txtValidationSectorL = By.XPath("//div[@class='fieldLevelErrors']//ul/li/a[text()='Sector']");
        By txtValidationNameL = By.XPath("//div[@class='fieldLevelErrors']//ul/li/a[text()='Name']");
        public bool IsContactNameValidationDisplayedLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 10);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(2000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtValidationNameL, 10);
                bool isValidation = driver.FindElement(txtValidationNameL).Displayed;
                driver.FindElement(btnCancelL).Click();
                return isValidation;
            }
            catch
            {
                try
                {
                    driver.FindElement(btnCancelL).Click();
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public void ClickAddOpportunityButtonLV(string valLOB)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _btnAddOpportunity(valLOB), 10);
            driver.FindElement(_btnAddOpportunity(valLOB)).Click();

        }

        public void ClickQuickLinkLV(string linkTxt, string companyType)
        {
            if(companyType == "Houlihan Company")
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tabContactsL, 10);
                driver.FindElement(tabContactsL).Click();
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, _CompanyDetailPageQuickLink(linkTxt), 10);
                driver.FindElement(_CompanyDetailPageQuickLink(linkTxt)).Click();
            }
        }

        public void ClickRelatedNewContactButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnRelatedNewContactL, 10);
            driver.FindElement(btnRelatedNewContactL).Click();
        }

        public string GetNewContactDialogHeaderLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, headertxtNewContactL, 10);
            return driver.FindElement(headertxtNewContactL).Text.Trim();
        }

        public void DeleteCompanyLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            try
            {
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteL, 5);
                driver.FindElement(btnDeleteL).Click();
            }
            catch(Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButonL, 10);
                driver.FindElement(iconExpandMoreButonL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, linkDeleteL, 10);
                driver.FindElement(linkDeleteL).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmDeleteL, 20);
            driver.FindElement(btnConfirmDeleteL).Click();
            Thread.Sleep(3000);
        }

        //Function to get Description
        public string GetDescriptionValueLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,600)");
            WebDriverWaits.WaitUntilEleVisible(driver, txtDscL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtDscL));
            string descriptionValueFromDetail = driver.FindElement(txtDscL).Text;
            return descriptionValueFromDetail;
        }

        //Function to get IG
        public string GetIndustryFocusLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtIGL, 20);
            string IndustryFocusValueFromDetail = driver.FindElement(txtIGL).Text;
            return IndustryFocusValueFromDetail;
        }

        //Function to get company Ownership type
        public string GetOwnershipLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtOwnershipL, 20);
            string OwnershipValueFromDetail = driver.FindElement(txtOwnershipL).Text;
            return OwnershipValueFromDetail;
        }

        //Function to get company sub type
        public string GetCompanySubTypeLV()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, txtCmpnySubTypeL, 20);
            string CompanySubTypeFromDetail = driver.FindElement(txtCmpnySubTypeL).Text;
            return CompanySubTypeFromDetail;
        }

        public void ClickEditButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL);
            driver.FindElement(btnEditL).Click();
        }

        public string GetCompanyTypeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyTypeL, 20);
            string CompanyTypeFromDetail = driver.FindElement(valCompanyTypeL).Text.Trim();
            return CompanyTypeFromDetail;
        }

        public string GetCompanyDescriptionLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtDescL, 20);
            string companyDescription = driver.FindElement(txtDescL).Text;
            return companyDescription;
        }

        public string GetCompanyNameHeaderLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtDetailHeadingL, 20);
            string headingCompanyDetail = driver.FindElement(txtDetailHeadingL).Text;
            return headingCompanyDetail;
        }

        public string GetCompanyCompleteAddressLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyAddressL, 20);
            string companyAddress = driver.FindElement(valCompanyAddressL).Text.Replace("\r\n", " ");
            return companyAddress;
        }

        public string GetCompanyDetailsHeadingLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtDetailHeadingL, 20);
            string headingCompanyDetail = driver.FindElement(txtDetailHeadingL).Text;
            return headingCompanyDetail;
        }

        public bool VerifyIfCompanySectorQuickLinkIsDisplayed()
        {
            bool result = false;
            if(driver.FindElement(linkCompanySector).Displayed)
            {
                result = true;
            }
            return result;
        }

        public void ClickNewCompanySectorButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCompanySector, 120);
            driver.FindElement(btnNewCompanySector).Click();
            Thread.Sleep(2000);
        }

        public void SelectCoverageSectorDependency(string covSectorDependencyName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, drpdownInvestmentPref, 120);
            driver.FindElement(drpdownInvestmentPref).SendKeys("Operating Sector");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, imgCompanySectorDependencyLookUp, 120);
            driver.FindElement(imgCompanySectorDependencyLookUp).Click();
            Thread.Sleep(2000);

            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);

            //Enter search frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("searchFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='searchFrame']")));
            Thread.Sleep(2000);

            //Enter dependency name
            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys(covSectorDependencyName);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(2000);

            driver.SwitchTo().DefaultContent();

            //Enter results frame & click on the result
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("resultsFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='resultsFrame']")));
            Thread.Sleep(2000);
            driver.FindElement(linkCoverageSectorDependencyName).Click();
            Thread.Sleep(4000);

            //Switch back to original window
            CustomFunctions.SwitchToWindow(driver, 0);
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
            WebDriverWaits.WaitUntilEleVisible(driver, imgCompanySectorDependencyLookUp, 120);
            driver.FindElement(imgCompanySectorDependencyLookUp).Click();
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

            //Click on Show Filters link
            //driver.FindElement(linkShowFilters).Click();

            //Enter filter values
            driver.FindElement(inputCoverageType).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 1));
            driver.FindElement(inputPrimarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 2));
            driver.FindElement(inputSecondarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 3));
            driver.FindElement(inputTertiarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 4));

            //Click on Apply filters button
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(2000);

            if(driver.FindElement(linkCoverageSectorDependencyName).Text == covSectorDependencyName)
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

        public void SaveNewCompanySectorDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveCompanySector, 120);
            driver.FindElement(btnSaveCompanySector).Click();
            Thread.Sleep(2000);
        }

        public string GetCompanySectorName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanySectorName, 120);
            string name = driver.FindElement(valCompanySectorName).Text;
            return name;
        }

        public void NavigateToCompanyDetailPageFromCompanySectorDetailPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCompanyName, 120);
            driver.FindElement(linkCompanyName).Click();
            Thread.Sleep(2000);
        }

        public void NavigateToCoverageSectorDependenciesPage()
        {
            driver.FindElement(shwAllTab).Click();
            Thread.Sleep(2000);
            driver.FindElement(imgCoverageSectorDependencies).Click();
            Thread.Sleep(2000);
        }

        public bool VerifyCompanySectorAddedToCompanyOrNot(string sectorName)
        {
            Thread.Sleep(5000);
            bool result = false;
            if(driver.FindElement(linkSectorName).Text == sectorName)
            {
                result = true;
            }
            return result;
        }

        public void DeleteCompanySector()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkSectorName, 120);
            driver.FindElement(linkSectorName).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCompanySector, 120);
            driver.FindElement(btnDeleteCompanySector).Click();
            Thread.Sleep(2000);

            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(3000);
        }

        //To click Coverage Team link 
        public string ClickCoverageTeam()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCoverageTeam, 120);
            driver.FindElement(linkCoverageTeam).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkOfficerName, 120);
            driver.FindElement(linkOfficerName).Click();
            string title = driver.FindElement(titlePage).Text;
            return title;
        }

        //To click Company Member List
        public string ClickCompanyList()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCompany, 120);
            driver.FindElement(linkCompany).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkCompanyList, 120);
            driver.FindElement(linkCompanyList).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkCompanyMember, 120);
            driver.FindElement(linkCompanyMember).Click();
            string title = driver.FindElement(titleCoverageTeam).Text;
            return title;
        }

        //To click Campaigns Tab
        public string ClickCampaignsTab()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, linkCampaign, 120);
            driver.FindElement(linkCampaign).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo, 90);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkCampaignName, 120);
            driver.FindElement(linkCampaignName).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleCoverageTeam, 120);
            string title = driver.FindElement(titleCoverageTeam).Text;
            return title;
        }

        public int GetSizeOfCompanyFinancialList()
        {
            IList<IWebElement> companyFinancialList = driver.FindElements(By.XPath("//*[text()='Company Financials']/../../../../../following-sibling::div/table/tbody/tr"));
            return companyFinancialList.Count;
        }

        //Get heading of company details page
        public string GetCompanyDetailsHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, companyDetailHeading, 60);
            string headingCompanyDetail = driver.FindElement(companyDetailHeading).Text;
            return headingCompanyDetail;
        }

        //Get contact name under contacts section
        public string GetContactNameUnderContactsSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContactName, 60);
            string ContactName = driver.FindElement(valContactName).Text;
            return ContactName;
        }

        public string GetSecondContactNameUnderContactsSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSecondContactName, 60);
            string ContactName = driver.FindElement(valSecondContactName).Text;
            return ContactName;
        }

        public string GetThirdContactNameUnderContactsSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valThirdContactName, 60);
            string ContactName = driver.FindElement(valThirdContactName).Text;
            return ContactName;
        }

        //Get heading of company details page
        public string GetContactEmailUnderContactsSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContactEmail, 60);
            string ContactEmail = driver.FindElement(valContactEmail).Text;
            return ContactEmail;
        }

        //Get heading of company details page
        public string GetContactPhoneUnderContactsSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContactPhone, 60);
            string ContactPhone = driver.FindElement(valContactPhone).Text;
            return ContactPhone;
        }

        //Click new contact button
        public void ClickNewContactButton()
        {
            //CustomFunctions.ActionClicks(driver, btnNewContact, 20);

            WebDriverWaits.WaitUntilEleVisible(driver, btnNewContact, 120);
            driver.FindElement(btnNewContact).Click();
        }

        // Click opportunity button
        public void ClickOpportunityButton(string file, string CompanyType, string new_value, string add_value)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            if(CompanyType.Equals("Houlihan Company"))//For Houlihan Lockey company
            {
                driver.FindElement(By.CssSelector($"input[value = '{new_value}']")).Click();
            }
            // For other companies
            else
            {
                Thread.Sleep(2000);
                driver.SwitchTo().Frame("066i0000004ZGBw");
                driver.FindElement(By.XPath($"//a[contains(text(),'{add_value}')]")).Click();
                driver.SwitchTo().DefaultContent();
            }
        }

        // Get Account Description value
        public string ERPAccountDescription(string companyRecordType)
        {
            if(companyRecordType.Equals("Capital Provider") || companyRecordType.Contains("Operating Company"))
            {
                return "External Customer";
            }
            else
            {
                return "HL Account";
            }
        }

        public string ERPCustomerType(string companyRecordType)
        {
            if(companyRecordType.Equals("Capital Provider") || companyRecordType.Contains("Operating Company"))
            {
                return "R";
            }
            else
            {
                return "I";
            }
        }

        public string ERPCustomerTypeDescription(string companyRecordType)
        {
            if(companyRecordType.Equals("Capital Provider") || companyRecordType.Contains("Operating Company"))
            {
                return "External";
            }
            else
            {
                return "Internal";
            }
        }

        //Select coverage team officer
        public void SelectCoverageTeamOfficer()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCoverageOfficer, 120);
            driver.FindElement(linkCoverageOfficer).Click();
        }

        // Click new coverage team button
        public void ClickNewCoverageTeam()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCoverageTeam, 120);
            driver.FindElement(btnNewCoverageTeam).Click();
        }

        //Click on new company financial button
        public void ClickNewCompanyFinancial(string file, string CompanyType)
        {
            if(CustomFunctions.IsElementPresent(driver, errPage))
            {
                companyHome.SearchCompany(file, CompanyType);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCompanyFinancial, 120);
            driver.FindElement(btnNewCompanyFinancial).Click();
        }

        //Get coverage team edit page title
        public string GetCoverageTeamEditPageHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, coverageTeamEditHeading, 60);
            string headingCoverageTeamEdit = driver.FindElement(coverageTeamEditHeading).Text;
            return headingCoverageTeamEdit;
        }

        // To identify required tags/mandatory fields in Coverage team edit page
        public IWebElement CoverageTeamEditRequiredTag(string tagName, string fieldName)
        {
            return driver.FindElement(By.XPath($"//{tagName}[@id='{fieldName}']/../..//div"));
            //input[@id='CF00Ni000000D7bV0']/../../div
        }

        // Validate mandatory fields on FS and Standard coverage team page
        public bool ValidateMandatoryFields()
        {
            return CoverageTeamEditRequiredTag("input", "CF00Ni000000D7bV0").GetAttribute("class").Contains("requiredBlock") &&
            CoverageTeamEditRequiredTag("input", "CF00Ni000000D7ba0").GetAttribute("class").Contains("requiredBlock") &&
            CoverageTeamEditRequiredTag("select", "00Ni000000D7bha").GetAttribute("class").Contains("requiredBlock") &&
            CoverageTeamEditRequiredTag("select", "00Ni000000FjXsE").GetAttribute("class").Contains("requiredBlock") &&
             CoverageTeamEditRequiredTag("select", "00Ni000000FjXsJ").GetAttribute("class").Contains("requiredBlock");
        }

        //Clear text of company on coverage team edit page
        public void ClearTextCompanyName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).Clear();
        }

        // Click save button on coverage team edit page
        public void ClickSaveButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveCoverageTeam);
            driver.FindElement(btnSaveCoverageTeam).Click();
        }

        //Validate  priority help text
        public string GetPriorityHelpText()
        {
            IWebElement toolTipText = driver.FindElement(toolTipPriorityText);
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            string htmlCode = (string) js.ExecuteScript("return arguments[0].innerHTML;", toolTipText);
            string toolTipPriorityTxt = htmlCode.Split(',')[1].Trim();
            return toolTipPriorityTxt;
        }

        public bool ValidatePriorityHelpObject()
        {
            bool available = CustomFunctions.IsElementPresent(driver, toolTipPriorityHelp);
            return available;
        }

        public void DeleteSalesforceCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCompany, 120);
            driver.FindElement(btnDeleteCompany).Click();

            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        public void DeleteCompany(string file, string CompanyType)
        {
            companyHome.SearchCompany(file, CompanyType);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCompany, 120);
            driver.FindElement(btnDeleteCompany).Click();

            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        public void DeleteCompanyContract(string file, string CompanyType)
        {
            if(CustomFunctions.IsElementPresent(driver, errPage))
            {
                companyHome.SearchCompany(file, CompanyType);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCompany, 120);
            driver.FindElement(btnDeleteCompany).Click();

            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkContractName, 60);
            driver.FindElement(linkContractName).Click();

            CustomFunctions.ActionClicks(driver, btnDeleteCompany);
            Thread.Sleep(3000);
            alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        public string GetCompanyName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyName, 60);
            string CompanyNameFromDetail = driver.FindElement(valCompanyName).Text.Split('[')[0].Trim();
            return CompanyNameFromDetail;
        }

        public string GetCompanyStateProvince()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valState, 60);
            string headingCompanyDetail = driver.FindElement(valState).Text;
            return headingCompanyDetail;
        }

        public string GetCompanyType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyType, 60);
            string CompanyTypeFromDetail = driver.FindElement(valCompanyType).Text.Split('[')[0].Trim();
            return CompanyTypeFromDetail;
        }

        public string GetCompanyAddress()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAddress, 60);
            string CompanyAddressFromDetail = driver.FindElement(valAddress).Text;
            return CompanyAddressFromDetail;
        }

        public void ClickEditButton(string file, string CompanyType)
        {
            if(CustomFunctions.IsElementPresent(driver, errPage))
            {
                companyHome.SearchCompany(file, CompanyType);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit);
            driver.FindElement(btnEdit).Click();
        }

        public void EditCompanyAddress(string file, string CompanyType)
        {
            if(CustomFunctions.IsElementPresent(driver, errPage))
            {
                companyHome.SearchCompany(file, CompanyType);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit);
            driver.FindElement(btnEdit).Click();
            //IList<IWebElement> rows = driver.FindElements(comboStateProvince);
            SelectElement element = new SelectElement(driver.FindElement(comboStateProvince));
            IList<IWebElement> options = element.Options;
            for(int i = 1; i <= options.Count; i++)
            {
                IWebElement defaultSelected = element.SelectedOption;
                element.SelectByIndex(i);

                IWebElement updateValue = element.SelectedOption;
                if(updateValue.Text.Equals(defaultSelected.Text))
                {
                    element.SelectByIndex(i + 1);
                    break;
                }
                break;
            }
            //if (CompanyType.Equals("Operating Company"))
            //{
            string valShippingProvince = CustomFunctions.RandomValue();
            driver.FindElement(txtShippingProvince).Clear();
            driver.FindElement(txtShippingProvince).SendKeys(valShippingProvince);
            //}

            driver.FindElement(btnSaveCompanyEdit).Click();
        }

        public void EditCompanyContact(string email, string phone, int row)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkContactEdit);
            driver.FindElement(linkContactEdit).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, txtContactEmail, 120);

            driver.FindElement(txtContactEmail).Clear();
            Thread.Sleep(2000);
            driver.FindElement(txtContactEmail).SendKeys(email);
            //Enter phone
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactPhone, 40);
            driver.FindElement(txtContactPhone).Clear();
            driver.FindElement(txtContactPhone).SendKeys(phone);

            // Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnContactSave);
            driver.FindElement(btnContactSave).Click();
        }

        //Function to get company sub type
        public string GetCompanySubType()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, valCompanySubType, 60);
            string CompanySubTypeFromDetail = driver.FindElement(valCompanySubType).Text;
            return CompanySubTypeFromDetail;
        }

        public string GetOwnership()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valOwnership, 60);
            string OwnershipValueFromDetail = driver.FindElement(valOwnership).Text;
            return OwnershipValueFromDetail;
        }

        public string GetParentCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valParentCompany, 60);
            string ParentCompanyValueFromDetail = driver.FindElement(valParentCompany).Text;
            return ParentCompanyValueFromDetail;
        }

        public string GetIndustryFocus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valIndustryFocus, 60);
            string IndustryFocusValueFromDetail = driver.FindElement(valIndustryFocus).Text;
            return IndustryFocusValueFromDetail;
        }

        public string GetGeographicalFocus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valGeographicalFocus, 60);
            string GeographicalFocusValueFromDetail = driver.FindElement(valGeographicalFocus).Text;
            return GeographicalFocusValueFromDetail;
        }


        public string GetDealPreference()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valDealPreference, 60);
            string DealPreferenceValueFromDetail = driver.FindElement(valDealPreference).Text;
            return DealPreferenceValueFromDetail;
        }

        public string GetDescriptionValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valDescription, 60);
            string DescriptionValueFromDetail = driver.FindElement(valDescription).Text;
            return DescriptionValueFromDetail;
        }

        // Get CapIQ company value
        public string GetCapIQCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCapIQCompany, 60);
            string CapIQCompanyValueFromDetail = driver.FindElement(valCapIQCompany).Text;
            return CapIQCompanyValueFromDetail;
        }

        //Get heading of company details page
        public string GetCompanyLocation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valcompanyLocation, 60);
            string companyLocation = driver.FindElement(valcompanyLocation).Text;
            return companyLocation;
        }

        // Get company phone number
        public string GetCompanyPhoneNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valcompanyPhoneNumber, 60);
            string companyPhoneNumber = driver.FindElement(valcompanyPhoneNumber).Text;
            return companyPhoneNumber;
        }

        //Get company description

        public string GetCompanyDescription()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyDescription, 60);
            string companyDescription = driver.FindElement(valCompanyDescription).Text;
            return companyDescription;
        }

        //Function to get company complete address
        public string GetCompanyCompleteAddress()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyAddress, 60);
            string companyAddress = driver.FindElement(valCompanyAddress).Text;
            return companyAddress;
        }

        public string GetHQCheckBoxValue()
        {
            string title = driver.FindElement(chkBoxHQ).GetAttribute("title");
            return title;
        }

        public void ClickChangeCompanyTypeLink()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkChangeCompanyType);
            driver.FindElement(linkChangeCompanyType).Click();
        }

        public string ChangeCompanyRecordType(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string companyType = GetCompanyType();
            WebDriverWaits.WaitUntilEleVisible(driver, linkChangeCompanyType);
            driver.FindElement(linkChangeCompanyType).Click();
            if(companyType != (ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1)))
            {
                string recordType1 = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1);
                conSelectRecord.SelectCompanyRecordType(file, recordType1);
            }
            else
            {
                string recordType2 = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 3, 1);
                conSelectRecord.SelectCompanyRecordType(file, recordType2);
            }
            string companyTypeOnCompanyEditPage = companyEdit.GetCompanyType();

            IWebElement officecodeDropdown = driver.FindElement(drpdownOfficeCode);
            SelectElement select1 = new SelectElement(officecodeDropdown);
            select1.SelectByValue("BE");

            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveCompanyEdit);
            driver.FindElement(btnSaveCompanyEdit).Click();

            return companyTypeOnCompanyEditPage;
        }

        public string GetCreatedBy()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCreatedBy, 60);
            string CreatedByName = driver.FindElement(valCreatedBy).Text.Split(',')[0].Trim();
            return CreatedByName;
        }

        public string GetCompanyFinancialYear(int rows)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"div[id*='ke_body'] > table > tbody > tr:nth-child({rows}) > td:nth-child(3)"), 60);
            string CompanyFinancialYear = driver.FindElement(By.CssSelector($"div[id*='ke_body'] > table > tbody > tr:nth-child({rows}) > td:nth-child(3)")).Text;
            return CompanyFinancialYear;
        }


        public string GetAsOfYearFinancialYear(int rows)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"div[id*='ke_body'] > table > tbody > tr:nth-child({rows}) > td:nth-child(4)"), 60);
            string CompanyFinancialAsOfDate = driver.FindElement(By.CssSelector($"div[id*='ke_body'] > table > tbody > tr:nth-child({rows}) > td:nth-child(4)")).Text;
            return CompanyFinancialAsOfDate;

        }

        public string GetFinancialsYearAnnualFinancial()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valFinancialsYearAnnualFinancial, 60);
            string FinancialsYearAnnualFinancial = driver.FindElement(valFinancialsYearAnnualFinancial).Text;
            return FinancialsYearAnnualFinancial;
        }

        public string GetFinancialsDateAnnualFinancial()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valFinancialsDateAnnualFinancial, 60);
            string FinancialsDateAnnualFinancial = driver.FindElement(valFinancialsDateAnnualFinancial).Text;
            return FinancialsDateAnnualFinancial;
        }
        public string GetNoRecordTextCompanyFinancial()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNoRecordsCompanyFinancial, 60);
            string NoRecordsCompanyFinancial = driver.FindElement(valNoRecordsCompanyFinancial).Text;
            return NoRecordsCompanyFinancial;
        }

        public string GetERPBillToAddressFlag()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBillToAddressFlag, 60);
            string valueERPBillToAddressFlag = driver.FindElement(valERPBillToAddressFlag).Text;
            return valueERPBillToAddressFlag;
        }

        public string GetERPShipToAddressFlag()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPShipToAddressFlag, 60);
            string valueERPShipToAddressFlag = driver.FindElement(valERPShipToAddressFlag).Text;
            return valueERPShipToAddressFlag;
        }

        public void DeleteCompanyFinancialRecord(string file, string CompanyType)
        {
            if(CustomFunctions.IsElementPresent(driver, errPage))
            {
                companyHome.SearchCompany(file, CompanyType);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, linkDelCompanyFinancial);
            if(CustomFunctions.IsElementPresent(driver, linkDelCompanyFinancial))
            {
                driver.FindElement(linkDelCompanyFinancial).Click();

                Thread.Sleep(2000);
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("All records are deleted");
            }
        }

        public void DeleteContactRecord()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, linkDelContact);
            if(CustomFunctions.IsElementPresent(driver, linkDelContact))
            {
                driver.FindElement(linkDelContact).Click();

                Thread.Sleep(2000);
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("All records are deleted");
            }
        }

        public void DeleteCoverageTeamRecord(string file, string CompanyType)
        {
            if(CustomFunctions.IsElementPresent(driver, errPage))
            {
                companyHome.SearchCompany(file, CompanyType);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, linkDelCoverageTeam);
            driver.FindElement(linkDelCoverageTeam).Click();

            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        public void DeleteCoverageSector(string file, string CompanyType)
        {
            if(CustomFunctions.IsElementPresent(driver, errPage))
            {
                companyHome.SearchCompany(file, CompanyType);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, linkCoverageSector);
            driver.FindElement(linkCoverageSector).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCoverageSector);
            driver.FindElement(btnDeleteCoverageSector).Click();

            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        public string GetCoverageTeamOfficer()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCoverageTeamOfficer, 60);
            string CoverageTeamOfficer = driver.FindElement(linkCoverageTeamOfficer).Text;
            return CoverageTeamOfficer;
        }

        public string GetCoverageLevel()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCoverageLevel, 60);
            string CoverageLevel = driver.FindElement(valCoverageLevel).Text;
            return CoverageLevel;
        }

        public string GetCoverageType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCoverageType, 60);
            string CoverageType = driver.FindElement(valCoverageType).Text;
            return CoverageType;
        }



        public string GetValERPSubmittedToSync()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPSubmittedToSync, 120);
            //string valueERPSubmittedToSync = driver.FindElement(valERPSubmittedToSync).Text;

            string valueERPSubmittedToSync = driver.FindElement(valERPSubmittedToSync).GetAttribute("value");
            return valueERPSubmittedToSync;
        }


        public string GetValERPLastIntegrationResponseDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntegrationResponseDate, 60);
            //string valueERPLastIntegrationResponseDate = driver.FindElement(valERPLastIntegrationResponseDate).Text;
            string valueERPLastIntegrationResponseDate = driver.FindElement(valERPLastIntegrationResponseDate).GetAttribute("value");
            return valueERPLastIntegrationResponseDate;
        }

        public string GetERPAccountDescription()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPAccountDescription, 60);
            string valueERPAccountDescription = driver.FindElement(valERPAccountDescription).Text;
            return valueERPAccountDescription;
        }

        public string GetValERPLastIntegrationStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntegrationStatus, 60);
            string valueERPLastIntegrationStatus = driver.FindElement(valERPLastIntegrationStatus).Text;
            return valueERPLastIntegrationStatus;
        }

        public string GetValERPLastIntegrationErrorCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntegrationErrorCode, 60);
            string valueERPLastIntegrationErrorCode = driver.FindElement(valERPLastIntegrationErrorCode).Text;
            return valueERPLastIntegrationErrorCode;
        }

        public string GetValERPLastIntegrationErrorDescription()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntegrationErrorDescription, 60);
            string valueERPLastIntegrationErrorDescription = driver.FindElement(valERPLastIntegrationErrorDescription).Text;
            return valueERPLastIntegrationErrorDescription;
        }

        public bool IsClientNumberPresent()
        {
            return CustomFunctions.isTextPresent(driver, driver.FindElement(valClientNumber));
        }

        public string GetClientNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valClientNumber, 60);
            string valueClientNumber = driver.FindElement(valClientNumber).Text;
            return valueClientNumber;
        }

        public string GetContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPrimaryContactAvailable, 60);
            string valuePrimaryContactAvailable = driver.FindElement(valPrimaryContactAvailable).Text;
            return valuePrimaryContactAvailable;
        }

        public string CheckERPOracleDetailsExists()
        {
            try
            {
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPAccountID));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPOrgPartyId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPBillToAddressId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPBillToLocationId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPBillToLocation));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPBillToSiteId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPShipToAddressId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPShipToLocationId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPShipToSiteId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPContactId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPPersonPartyId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPContactPointEmailId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPContactPointPhoneId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPContactPointRelationshipId));
                return "ERP Oracle details exists";
            }
            catch
            {
                return "ERP Oracle Detail does not exists";
            }
        }

        public string GetERPContactFirstName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContactFirstName, 60);
            string valueFirstName = driver.FindElement(valERPContactFirstName).Text;
            return valueFirstName;
        }

        public string GetERPContactLastName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContactLastName, 60);
            string valueLastName = driver.FindElement(valERPContactLastName).Text;
            return valueLastName;
        }

        public string GetERPContactEmail()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContactEmail, 120);
            string valueERPContactEmail = driver.FindElement(valERPContactEmail).Text;
            return valueERPContactEmail;
        }

        public string GetERPContactPhone()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContactPhone, 60);
            string valueERPContactPhone = driver.FindElement(valERPContactPhone).Text;
            return valueERPContactPhone;
        }

        public string GetERPCustomerType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPCustomerType, 60);
            string valueERPCustomerType = driver.FindElement(valERPCustomerType).Text;
            return valueERPCustomerType;
        }

        public string GetERPCustomerTypeDescription()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPCustomerTypeDesc, 60);
            string valueERPCustomerTypeDesc = driver.FindElement(valERPCustomerTypeDesc).Text;
            return valueERPCustomerTypeDesc;
        }

        public string GetERPContactFlag()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContactFlag, 60);
            string valueERPContactFlag = driver.FindElement(valERPContactFlag).Text;
            return valueERPContactFlag;
        }

        public string GetERPContactPointEmailFlag()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContactPointEmailFlag, 60);
            string valueERPContactPointEmailFlag = driver.FindElement(valERPContactPointEmailFlag).Text;
            return valueERPContactPointEmailFlag;
        }

        public string GetERPContactPointPhoneFlag()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContactPointPhoneFlag, 60);
            string valueERPContactPointPhoneFlag = driver.FindElement(valERPContactPointPhoneFlag).Text;
            return valueERPContactPointPhoneFlag;
        }

        public string GetERPOrgPartyId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPOrgPartyId, 60);
            string valueERPOrgPartyId = driver.FindElement(valERPOrgPartyId).Text;
            return valueERPOrgPartyId;
        }

        public string GetERPBillToAddressId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBillToAddressId, 60);
            string valueERPBillToAddressId = driver.FindElement(valERPBillToAddressId).Text;
            return valueERPBillToAddressId;
        }

        public string GetERPContactId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContactId, 60);
            string valueERPContactId = driver.FindElement(valERPContactId).Text;
            return valueERPContactId;
        }

        public string GetERPShipToAddressId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPShipToAddressId, 60);
            string valueERPShipToAddressId = driver.FindElement(valERPShipToAddressId).Text;
            return valueERPShipToAddressId;
        }

        public void ClickPrimaryBillingContact(int row)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkPrimaryBillingContact, 120);
            driver.FindElement(linkPrimaryBillingContact).SendKeys(Keys.Control + Keys.Return);
            if(row.Equals(2))
            {
                CustomFunctions.SwitchToWindow(driver, 3);
            }
            else
            {
                CustomFunctions.SwitchToWindow(driver, 6);
            }
        }

        public void ClickPrimaryBillingContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkPrimaryBillingContact, 120);
            driver.FindElement(linkPrimaryBillingContact).SendKeys(Keys.Control + Keys.Return);
            CustomFunctions.SwitchToWindow(driver, 3);
        }

        //Function is to edit company record type
        public void EditCompanyRecordType(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            int CompanyRecordList = ReadExcelData.GetRowCount(excelPath, "CompanyRecordTypes");

            for(int row = 2; row <= CompanyRecordList; row++)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, changelinkCompanyRecordType, 120);
                driver.FindElement(changelinkCompanyRecordType).Click();
                IWebElement recordDropdown = driver.FindElement(drpdwnCompanyRecordType);
                SelectElement select = new SelectElement(recordDropdown);

                IWebElement currentRecordType = driver.FindElement(txtCurrentRecordType);

                string companyRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyRecordTypes", row, 3);
                Console.WriteLine(currentRecordType.Text);
                Console.WriteLine(companyRecordTypeExl);

                IList<IWebElement> options = select.Options;
                IWebElement companyRecordTypeOption = options[row - 2];
                companyRecordTypeOption.Click();
                driver.FindElement(btnContinue).Click();

                WebDriverWaits.WaitUntilEleVisible(driver, comboFlagReason, 120);
                driver.FindElement(comboFlagReason).SendKeys("Company Renamed");
                WebDriverWaits.WaitUntilEleVisible(driver, txtFlagReasonComment, 120);
                driver.FindElement(txtFlagReasonComment).SendKeys("Test");

                WebDriverWaits.WaitUntilEleVisible(driver, btnSaveCoverageTeam, 120);

                if((companyRecordTypeExl).Equals("Conflicts Check LDCCR"))
                {
                    IWebElement officecodeDropdown = driver.FindElement(drpdownOfficeCode);
                    SelectElement select1 = new SelectElement(officecodeDropdown);
                    select1.SelectByValue("BE");
                }
                driver.FindElement(btnSaveCoverageTeam).Click();
            }

        }

        // Click on tab from Company Detail page 
        //public bool IsCompanyDetailPageTabPresentLV(string tabname)
        //{
        //    try
        //    {
        //        WebDriverWaits.WaitUntilEleVisible(driver, _homePageTab(tabname), 30);
        //        return driver.FindElement(_homePageTab(tabname)).Displayed;
        //    }
        //    catch { return false; }
        //}

        public bool IsCompanyDetailPageTabPresentLV(string tabname)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);
            bool tabFound = false;
            try
            {
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, _homePageTab(tabname), 5);
                    return driver.FindElement(_homePageTab(tabname)).Displayed;
                }
                catch(Exception e)
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, iconHeaderMoreTabsL, 5);
                    driver.FindElement(iconHeaderMoreTabsL).Click();
                    By lnkFlag = By.XPath($"//lightning-tab-bar/ul/li/lightning-button-menu//a/span[text()='{tabname}']");
                    WebDriverWaits.WaitUntilEleVisible(driver, lnkFlag, 5);
                    tabFound = driver.FindElement(lnkFlag).Displayed;
                    driver.FindElement(iconHeaderMoreTabsL).Click();
                    return tabFound;
                }
            }
            catch { return false; }
        }
        public bool ClickCompanyDetailPageTabLV(string tabname)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, _homePageTab(tabname), 5);
                driver.FindElement(_homePageTab(tabname)).Click();
            }
            catch(Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconHeaderMoreTabsL, 5);
                driver.FindElement(iconHeaderMoreTabsL).Click();
                By lnkFlag = By.XPath($"//lightning-tab-bar/ul/li/lightning-button-menu//a/span[text()='{tabname}']");
                WebDriverWaits.WaitUntilEleVisible(driver, lnkFlag, 5);
                driver.FindElement(lnkFlag).Click();
                Thread.Sleep(5000);
            }
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, _tabHeader(tabname), 10);
                return driver.FindElement(_tabHeader(tabname)).Displayed;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public string GetFlagReasonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtFlagReasonL, 10);
            return driver.FindElement(txtFlagReasonL).Text;
        }
        public string GetFlagReasonCommentLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtFlagReasonCommentL, 10);
            return driver.FindElement(txtFlagReasonCommentL).Text;
        }
        public void EditCompanyFlagLV(string flagReason, string flagReasonComment)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnInlineFlagReasonL, 10);
            driver.FindElement(btnInlineFlagReasonL).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnFlagReasonL, 5);
            driver.FindElement(btnFlagReasonL).Click();
            By comboFlagReason = By.XPath($"//div//lightning-base-combobox-item[@data-value='{flagReason}']");
            WebDriverWaits.WaitUntilEleVisible(driver, comboFlagReason, 5);
            driver.FindElement(comboFlagReason).Click();
            driver.FindElement(inputFlagReasonCommentL).Clear();
            driver.FindElement(inputFlagReasonCommentL).SendKeys(flagReasonComment);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(5000);
        }

        public void ClickActivitiesReportButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitiesReportL);
            driver.FindElement(btnActivitiesReportL).Click();
        }
        public string GetPageHeaderLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iframeTableCompanyActivityL, 20);
            driver.SwitchTo().Frame(driver.FindElement(iframeTableCompanyActivityL));
            WebDriverWaits.WaitUntilEleVisible(driver, headerPageL, 20);
            return driver.FindElement(headerPageL).Text;
        }
        public string GetActivityCompanyNameLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tableActivityCompanyName, 20);
            string companyName = driver.FindElement(tableActivityCompanyName).Text;
            driver.SwitchTo().DefaultContent();
            return companyName;
        }
        //public bool ClickCompanyDetailPageTabLV(string tabname)
        //{
        //    WebDriverWaits.WaitUntilEleVisible(driver, _homePageTab(tabname), 10);
        //    driver.FindElement(_homePageTab(tabname)).Click();
        //    try
        //    {
        //        WebDriverWaits.WaitUntilEleVisible(driver, _tabHeader(tabname), 10);
        //        return driver.FindElement(_tabHeader(tabname)).Displayed;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}

        public bool IsOpportunitiesSearchBoxL()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, searchOpportunitiesL, 30);
                return driver.FindElement(searchOpportunitiesL).Displayed;
            }
            catch { return false; }
        }

        public bool IsOpportunitiesFoundByNameLV(string name)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor) driver;
            By recordByName = By.XPath($"//lightning-datatable//table//tbody//tr//a[text()='{name}']");
            WebDriverWaits.WaitUntilEleVisible(driver, searchOpportunitiesL, 10);
            jse.ExecuteScript("arguments[0].value='" + name + "';", driver.FindElement(searchOpportunitiesL));
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, recordByName, 10);
                return driver.FindElement(recordByName).Displayed;
            }
            catch { return false; }
        }

        public bool IsOppoortunitiesFoundByNumberLV(string number)
        {
            By recordByNumber = By.XPath($"//lightning-datatable//table//tbody//tr//td[@data-label='Opportunity Number']//lightning-base-formatted-text[text()='{number}']");
            WebDriverWaits.WaitUntilEleVisible(driver, searchOpportunitiesL, 10);
            driver.FindElement(searchOpportunitiesL).Clear();
            driver.FindElement(searchOpportunitiesL).SendKeys(number);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, recordByNumber, 10);
                return driver.FindElement(recordByNumber).Displayed;
            }
            catch { return false; }
        }

        public bool IsOppoortunitiesFoundByStageLV(string stage)
        {
            By recordByStage = By.XPath($"//lightning-datatable//table//tbody//tr//td[contains(@data-label,'Priority')]//lightning-base-formatted-text[text()='{stage}']");
            WebDriverWaits.WaitUntilEleVisible(driver, searchOpportunitiesL, 10);
            driver.FindElement(searchOpportunitiesL).Clear();
            driver.FindElement(searchOpportunitiesL).SendKeys(stage);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, recordByStage, 10);
                return driver.FindElement(recordByStage).Displayed;
            }
            catch { return false; }
        }

        public bool IsEngagementSearchBoxLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, searchEngagementsL, 30);
                return driver.FindElement(searchEngagementsL).Displayed;
            }
            catch { return false; }
        }

        public bool IsEngagementFoundByNameLV(string name)
        {
            By recordByName = By.XPath($"//lightning-datatable//table//tbody//tr//a[text()='{name}']");
            WebDriverWaits.WaitUntilEleVisible(driver, searchEngagementsL, 10);
            driver.FindElement(searchEngagementsL).SendKeys(name);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, recordByName, 10);
                return driver.FindElement(recordByName).Displayed;
            }
            catch { return false; }
        }

        public bool IsEngagementFoundByNumberLV(string number)
        {
            By recordByNumber = By.XPath($"//lightning-datatable//table//tbody//tr//td[@data-label='Engagement Number']//lightning-base-formatted-text[text()='{number}']");
            WebDriverWaits.WaitUntilEleVisible(driver, searchEngagementsL, 10);
            driver.FindElement(searchEngagementsL).Clear();
            driver.FindElement(searchEngagementsL).SendKeys(number);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, recordByNumber, 10);
                return driver.FindElement(recordByNumber).Displayed;
            }
            catch { return false; }
        }


        public bool IsEngagementFoundByStageLV(string stage)

        {
            By recordByStage = By.XPath($"//lightning-datatable//table//tbody//tr//td[contains(@data-label,'Stage')]//lightning-base-formatted-text[text()='{stage}']");
            WebDriverWaits.WaitUntilEleVisible(driver, searchEngagementsL, 10);
            driver.FindElement(searchEngagementsL).Clear();
            driver.FindElement(searchEngagementsL).SendKeys(stage);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, recordByStage, 10);
                return driver.FindElement(recordByStage).Displayed;
            }
            catch { return false; }
        }

        public void CloseCompanyTabLV(string name)
        {
            By buttonCloseTab = By.XPath($"//button[contains(@title,'Close {name}')]");

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, alertDuplicate, 5);
                driver.FindElement(alertDuplicate).Click();
                driver.FindElement(buttonCloseTab).Click();
                Thread.Sleep(4000);
                driver.Navigate().Refresh();
                Thread.Sleep(4000);
            }
            catch(Exception e)
            {
                driver.FindElement(buttonCloseTab).Click();
                Thread.Sleep(4000);
                driver.Navigate().Refresh();
                Thread.Sleep(4000);

            }

        }
        public void ClickViewAllOpportunities()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkViewAllOppL, 10);
            driver.FindElement(linkViewAllOppL).Click();
        }
        public void ClickViewAllEngagements()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkViewAllEngL, 10);
            driver.FindElement(linkViewAllEngL).Click();
        }
        public void CloseViewAllPopup()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCloseViewAllPopup, 10);
            driver.FindElement(btnCloseViewAllPopup).Click();
        }

        public bool IsOppEngFoundByNameOnViewAllLV(string name)
        {
            try
            {
                By recordByName = By.XPath($"//div[contains(@class,'slds-modal__content')]//lightning-datatable//table//tbody//tr//a[text()='{name}']");
                WebDriverWaits.WaitUntilEleVisible(driver, searchViewAllOppEngL, 10);
                driver.FindElement(searchViewAllOppEngL).Clear();
                driver.FindElement(searchViewAllOppEngL).SendKeys(name);
                WebDriverWaits.WaitUntilEleVisible(driver, recordByName, 10);
                return driver.FindElement(recordByName).Displayed;
            }

            catch { return false; }

        }

        public bool IsOpportunitiesFoundByNumberOnViewAllLV(string number)
        {
            try
            {
                By recordByNumber = By.XPath($"//div[contains(@class,'slds-modal__content')]//lightning-datatable//table//tbody//tr//td[@data-label='Opportunity Number']//lightning-base-formatted-text[text()='{number}']");
                WebDriverWaits.WaitUntilEleVisible(driver, searchViewAllOppEngL, 10);
                driver.FindElement(searchViewAllOppEngL).Clear();
                driver.FindElement(searchViewAllOppEngL).SendKeys(number);
                WebDriverWaits.WaitUntilEleVisible(driver, recordByNumber, 10);
                return driver.FindElement(recordByNumber).Displayed;
            }
            catch { return false; }
        }

        public bool IsEngagementsFoundByNumberOnViewAllLV(string number)
        {
            try
            {
                By recordByNumber = By.XPath($"//div[contains(@class,'slds-modal__content')]//lightning-datatable//table//tbody//tr//td[@data-label='Engagement Number']//lightning-base-formatted-text[text()='{number}']");
                WebDriverWaits.WaitUntilEleVisible(driver, searchViewAllOppEngL, 10);
                driver.FindElement(searchViewAllOppEngL).Clear();
                driver.FindElement(searchViewAllOppEngL).SendKeys(number);
                WebDriverWaits.WaitUntilEleVisible(driver, recordByNumber, 10);
                return driver.FindElement(recordByNumber).Displayed;
            }
            catch { return false; }
        }

        public bool IsOppEngFoundByStageOnViewAllLV(string stage)
        {
            try
            {
                By recordByStage = By.XPath($"//div[contains(@class,'slds-modal__content')]//lightning-datatable//table//tbody//tr//td[contains(@data-label,'Stage')]//lightning-base-formatted-text[text()='{stage}']");
                WebDriverWaits.WaitUntilEleVisible(driver, searchViewAllOppEngL, 10);
                driver.FindElement(searchViewAllOppEngL).Clear();
                driver.FindElement(searchViewAllOppEngL).SendKeys(stage);
                WebDriverWaits.WaitUntilEleVisible(driver, recordByStage, 10);
                return driver.FindElement(recordByStage).Displayed;

            }

            catch { return false; }

        }

        public void CloseCoverageTeamDetailPageLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, buttonCloseCoverageTab, 20);
            driver.FindElement(buttonCloseCoverageTab).Click();
            Thread.Sleep(5000);
            driver.Navigate().Refresh();
            Thread.Sleep(5000);
        }
        public bool IsContactNestedListHLRelationshipLV(string contactName)
        {
            try
            {
                By btnNestedHLRelationship = By.XPath($"//a[contains(@class,'NestedTables')][text()='{contactName}']//parent::td//preceding-sibling::td//button");
                WebDriverWaits.WaitUntilEleVisible(driver, btnNestedHLRelationship, 20);
                return driver.FindElement(btnNestedHLRelationship).Displayed;
            }
            catch { return false; }
        }
        public bool IsCoverageNestedListOfficerLV(string contactName)
        {
            try
            {
                By btnNestedHLRelationship = By.XPath($"//article//div[contains(@class,'NestedTables')]//table//tr//td[@data-label='Officer Name']//a[text()='{contactName}']//ancestor::td//preceding-sibling::td//button");
                WebDriverWaits.WaitUntilEleVisible(driver, btnNestedHLRelationship, 20);
                return driver.FindElement(btnNestedHLRelationship).Displayed;
            }
            catch { return false; }
        }
        public string ClickContactNestedListHLRelationshipLV(string contactName)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor) driver;
            By btnNestedHLRelationship = By.XPath($"//a[contains(@class,'NestedTables')][text()='{contactName}']//parent::td//preceding-sibling::td//button");
            WebDriverWaits.WaitUntilEleVisible(driver, btnNestedHLRelationship, 20);
            //driver.FindElement(btnNestedHLRelationship).Click();
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnNestedHLRelationship));

            WebDriverWaits.WaitUntilEleVisible(driver, headerNestedListL, 20);
            return driver.FindElement(headerNestedListL).Text;
        }



        public string ClickCoverageNestedListLV(string contactName)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor) driver;
            By btnNestedHLRelationshipL = By.XPath($"//article//div[contains(@class,'NestedTables')]//table//tr//td[@data-label='Officer Name']//a[text()='{contactName}']//ancestor::td//preceding-sibling::td//button");
            //WebDriverWaits.WaitUntilEleVisible(driver, btnNestedHLRelationshipL, 20);
            //driver.FindElement(btnNestedHLRelationship).Click();
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnNestedHLRelationshipL));



            WebDriverWaits.WaitUntilEleVisible(driver, headerNestedListL, 20);
            return driver.FindElement(headerNestedListL).Text;
        }

        public string GetCompanyHLRelationshipContactLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyHLRelationshipContactL, 20);
            return driver.FindElement(txtCompanyHLRelationshipContactL).Text;
        }

        public string GetCompanyHLRelationshipOfficerNameL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyHLRelationshipCoverageOfficerL, 20);
            return driver.FindElement(txtCompanyHLRelationshipCoverageOfficerL).Text;
        }
        public void ClickCompanyNestedContactLV(string contactName)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor) driver;
            By linkNestedHLRelationshipL = By.XPath($"//a[contains(@class,'NestedTables')][text()='{contactName}']");
            //driver.FindElement(linkNestedHLRelationship ).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkNestedHLRelationshipL, 20);
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(linkNestedHLRelationshipL));
        }
        public void ClickNestedCoverageTeamOfficerLV(string officerName)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor) driver;
            By btnNestedHLRelationshipL = By.XPath($"//article//div[contains(@class,'NestedTables')]//table//tr//td[@data-label='Officer Name']//a[text()='{officerName}']");
            // WebDriverWaits.WaitUntilEleVisible(driver, btnNestedHLRelationshipL, 20);
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnNestedHLRelationshipL));
        }

        public bool IsCoverageTeamDetailsPageDisplayedLV(string value)
        {
            By titleCoverageteamL = By.XPath($"//div[contains(@class,'page-header')]//h1//div[contains(@class,'NameTitle')]//records-entity-label[contains(text(),'{value}')]");//div[contains(@class,'page-header')]//h1//div[contains(@class,'NameTitle')][contains(text(),'{value}')]");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, titleCoverageteamL, 20);
                return driver.FindElement(titleCoverageteamL).Displayed;
            }
            catch { return false; }
        }

        public string GetCoverageTeamCompanyNameLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCoverageTeamCompanyNameL, 20);
            return driver.FindElement(txtCoverageTeamCompanyNameL).Text;
        }

        public string GetCoverageOfficerNameLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCoverageTeamOfficerNameL, 20);
            return driver.FindElement(txtCoverageTeamOfficerNameL).Text;
        }

        public string GetCompanyOfficeNameCoverageTypeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyDetailCoverageTypeL, 20);
            return driver.FindElement(txtCompanyDetailCoverageTypeL).Text;
        }

        public string GetOfficerCoverageTypeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, panelTabCoverageSectors, 20);
            driver.FindElement(panelTabCoverageSectors).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, panelCoverageTypeL, 10);
            return driver.FindElement(panelCoverageTypeL).Text;
        }

        public bool IsIndustryTypePresent(string industryType)
        {
            bool isFound = false;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboIndustryType);
            driver.FindElement(comboIndustryType).Click();
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboIndustryTypeOptions);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            for(int row = 0; row <= actualValue.Length; row++)
            {
                if(actualValue[row].Contains(industryType))
                {
                    isFound = true;
                    driver.FindElement(btnCancel).Click();
                    break;
                }
            }
            return isFound;
        }

        public void ClickDetailPageQuickLink(string linkName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _DetailPageQuickLink(linkName));
            driver.FindElement(_DetailPageQuickLink(linkName)).Click();
        }

        public void MouseHoverDetailPageQuickLink(string linkName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _CompanyDetailPageQuickLink(linkName), 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(_CompanyDetailPageQuickLink(linkName)));
            //driver.FindElement(_CompanyDetailPageQuickLink(linkName)).Click();
        }

        public void ClickNewCoverageTeamButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCoverageTeam);
            driver.FindElement(btnNewCoverageTeam).Click();
        }

        public bool IsIndustryTypePresentonCoverageTeam(string industryType)
        {
            bool isFound = false;
            WebDriverWaits.WaitUntilEleVisible(driver, comboType);
            driver.FindElement(comboType).Click();
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboTypeOption);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            for(int row = 0; row <= actualValue.Length; row++)
            {
                if(actualValue[row].Contains(industryType))
                {
                    isFound = true;
                    driver.FindElement(btnCancel).Click();
                    break;
                }
            }
            return isFound;
        }

        private By _btnViewNestedRContactL(string contactName)
        {
            return By.XPath($"//a[contains(@class,'NestedTables')][text()='{contactName}']//parent::td//preceding-sibling::td//button");
        }

        public bool IsIndustryTypePresentonCoverageTeamLV(string industryType)
        {
            bool isFound = false;
            WebDriverWaits.WaitUntilEleVisible(driver, comboTypeL);
            driver.FindElement(comboTypeL).Click();
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboTypeOptionsL);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            for(int row = 0; row <= actualValue.Length; row++)
            {
                //Thread.Sleep(1000);
                if(actualValue[row].Contains(industryType))
                {
                    isFound = true;
                    //driver.FindElement(comboTypeL).Click();
                    //driver.FindElement(btnCancelL).Click();
                    break;
                }
            }
            return isFound;
        }
        public bool IsIndustryTypePresentLV(string industryType)
        {
            bool isFound = false;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL);
            driver.FindElement(btnEditL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboIndustryTypeL, 20);
            driver.FindElement(comboIndustryTypeL).Click();
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboIndustryTypeOptionsL);
            if(valTypes.Count == 0)
            {
                Thread.Sleep(2000);
                driver.FindElement(comboIndustryTypeL).Click();
                valTypes = driver.FindElements(comboIndustryTypeOptionsL);
            }
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            for(int row = 0; row <= actualValue.Length; row++)
            {
                //CustomFunctions.MoveToElement(actualValue[row])
                if(actualValue[row].Contains(industryType))
                {
                    isFound = true;
                    driver.FindElement(comboIndustryTypeL).Click();
                    Thread.Sleep(2000);
                    driver.FindElement(btnCancelL).Click();
                    break;
                }
            }
            return isFound;
        }
        public void ClickNewCoverageTeamButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCoverageTeamL, 20);
            driver.FindElement(btnNewCoverageTeamL).Click();
        }
        public void ClickNewCoverageTeamDefaultRTButtonLV()
        {
            Actions actions = new Actions(driver);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCoverageTeamL, 20);
            actions.MoveToElement(driver.FindElement(btnNewCoverageTeamL)).Click().Perform();
            WebDriverWaits.WaitUntilEleVisible(driver, btnDialogNextL, 10);
            Thread.Sleep(2000);
            actions.MoveToElement(driver.FindElement(btnDialogNextL)).Click().Perform();
        }
        public void DeleteCompanyNew(string CompanyType)
        {
            companyHome.SearchCompanyNew(CompanyType);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCompany, 20);
            driver.FindElement(btnDeleteCompany).Click();
            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }
    }
}