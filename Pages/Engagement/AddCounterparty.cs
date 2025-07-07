//using Microsoft.Office.Interop.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Permissions;
using System.Threading;

namespace SF_Automation.Pages.Engagement
{
    class AddCounterparty : BaseClass
    {
        By btnAddCounterparties = By.CssSelector(".pbButton >table>tbody>tr>td> input[value='Add Counterparties']");
        By btnDel = By.Id("sf_filter_remove_btn_1");
        By btnAddRow = By.Id("sf_filter_add_btn_and");
        By comboCity = By.Id("sf_filter_field_1");
        By txtCityName = By.Id("sf_value_1");
        By btnSearch = By.CssSelector("td>#search_btn");
        By checkCity = By.Id("thePage:theForm3:pbResult:tickTable:0:myCheckbox");
        By btnAddRec = By.CssSelector("input[value*='Add Selected Records To ']");
        By msgSuccess = By.CssSelector("div[class*='messageText']");
        By msgSuccessContact = By.CssSelector("div[id*='j_id8:j_id10']");
        //By btnBack = By.Id("back_btn");
        By btnBack = By.Id("back_btn");
        By lnkDetails = By.CssSelector(".view_record__c > a");
        By btnPrintableView = By.XPath("//button[text()='Printable View']");
        By valFirstName = By.XPath("//th[text()='First Name']/ancestor::tr/td");
        By valLastName = By.XPath("//th[text()='Last Name']/ancestor::tr/td");
        By valCounterparty1stName = By.XPath("//dt[text()='First Name:']/ancestor::dl/dd[1]//span");
        By valCounterparty2ndName = By.XPath("//dt[text()='Last Name:']/ancestor::dl/dd[1]//span");

        By valContactFirstName = By.XPath("//section/div/div/p[1]");
        By valContactLastName = By.XPath("//section/div/div/p[2]");
        //By valLastName = By.XPath("//dt[text()='First Name:']/ancestor::dl[1]/dd[2]/lst-template-list-field/lst-formatted-text");
        By btnAddEngCounterPartyÇontact = By.CssSelector("input[value='New Engagement Counterparty Contact']");
        By checkName = By.CssSelector("tbody[id*='pbtableId2:tb'] > tr:nth-child(1) > td:nth-child(1)");
        By btnSave = By.CssSelector("input[value='Save']");
        By titleCPSearch = By.CssSelector("div[class*='pbSubheader']>h3");
        By btnTableBack = By.CssSelector("input[name*='PanelId:j_id102']");
        By lnkEngagement = By.CssSelector("a[id*='lookupa']");
        By btnCounterParties = By.CssSelector("td[id*='topButtonRow'] > input[value='Counterparties']");
        By checkRec = By.XPath("//*[@id='dtable']/div[1]/div[2]/div/div/div[1]/input[1]");
        By titlePage = By.CssSelector("h1[class='pageType']");
        By btnDelete = By.CssSelector("input[value='Delete']");
        By msgText = By.CssSelector("span[id*=':f']> div");
        By txtEngage = By.CssSelector("span>input[name*='id65:0:j_id67']");
        By btnSearchEng = By.CssSelector("td>#search_btn2");
        By checkRow = By.CssSelector("#dtable > div.fix-column > div.tbody > div > div > div:nth-child(1) > input.targetCheck");
        By lblExistingEng = By.CssSelector("h3:nth-child(3) > a");
        By btnNewBid = By.CssSelector("input[value='New Bid']");
        By lnkDate = By.CssSelector("span.dateFormat > a");
        By btnCancel = By.CssSelector("td#topButtonRow > input[value='Cancel']");
        By msgNoRec = By.CssSelector("div[id*='3_body'] > table >tbody > tr >th");
        By btnBidSave = By.CssSelector("input[value=' Save ']");
        By valBidDate = By.CssSelector("td[class*='DateElement']");
        By lnkBidEdit = By.XPath("//a[text()='Edit']");
        By txtDate = By.CssSelector("input[id*='FlXO1']");
        By lnkBidDel = By.XPath("//a[text()='Del']");
        //Lightning
        By chkContact = By.XPath("//tr[1]/td[1]/lightning-primitive-cell-checkbox/span/label/span[1]");
        By valContact = By.XPath("//tr[1]/th/lightning-primitive-cell-factory/span/div/lightning-formatted-url/a");
        By btnAddContact = By.XPath("//button[@title='counterparty']");
        By tabCounterpartyEditor = By.XPath("//span[text()='Counterparty Editor']");
        By lnk2ndCompCounterparty = By.XPath("//tr/th/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/c-s-l-company-link-column/lightning-layout/slot/lightning-layout-item[2]/slot/lightning-formatted-url");
        By valMinRoundBid = By.XPath("//dt[text()='Round Minimum (MM):']/ancestor::dl/dd[2]/lst-template-list-field/lst-formatted-text/span");
        By valMaxRoundBid = By.XPath("//dt[text()='Round Maximum (MM):']/ancestor::dl/dd[3]/lst-template-list-field/lst-formatted-text/span");
        By btnEngCounterpartyContact = By.XPath("//button[@name='Engagement_Counterparty__c.New_Engagement_Counterparty_Contact']");

        By lnkContacts = By.XPath("//c-s-l-company-link-column/lightning-layout/slot/lightning-layout-item[2]/slot/div/p");
        By lnkCompCounterparty = By.XPath("//a[@title='SkyHive']");
        By txtSearchCounterparty = By.XPath("//input[@placeholder='Search']");
        By txtCPComments = By.XPath("//textarea[@name='Comment__c']");
        By btnSaveCPComment = By.XPath("//button[@type='submit']");
        By valAddedCPComment = By.XPath("//h3/lst-template-list-field/lightning-base-formatted-text");
        By valCPCommentType = By.XPath("//dt[text()='Comment Type:']/ancestor::dl/dd[1]/lst-template-list-field/lst-formatted-text");
        By valCPCommentCreator = By.XPath("//dt[text()='Creator:']/ancestor::dl/dd[2]/lst-template-list-field/formula-output-formula-html/lightning-formatted-rich-text/span");
        By valCPCommentCreatedDate = By.XPath("//dt[text()='Created Date:']/ancestor::dl/dd[3]/lst-template-list-field/lightning-formatted-date-time");
        By btnEditCounterpartyComment = By.XPath("//lst-dynamic-related-list-with-user-prefs/lst-related-list-view-manager/lst-common-list-internal/div/div/lst-primary-display-manager/div/lst-primary-display/lst-primary-display-card/lst-customized-template-list/div/lst-template-list-item-factory/lst-related-preview-card/article/slot/lst-template-list-field/lst-list-view-row-level-action/lightning-button-menu/button");
        By lnkDeleteCounterpartyComment = By.XPath("//li[2]/a[@title='Delete']");
        By btnDeleteCounterpartyComment = By.XPath("//span[text()='Delete']");
        By valPostDeleteCounterpartyComment = By.XPath("//span[@title='Engagement Counterparty Comments']/ancestor::a/span[@title='(0)']");
        By btnView = By.XPath("//button[@data-value='Buyside Stages']");
        By btnViewSellside = By.XPath("//button[@data-value='Sellside Stages']");
        By valUpdView = By.XPath("//lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[1]/span[2]/span");
        By selectedView = By.XPath("//div[2]/lightning-combobox/div/lightning-base-combobox/div/div[1]/button/span");
        By valBuysideView = By.XPath("//lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[4]/span[2]/span");
        By tblCounterparty = By.XPath("//lightning-datatable//div/table/tbody");
        By chkCounterparty = By.XPath("//tr[1]/td[2]/lightning-primitive-cell-checkbox/span/label/span[1]");
        By chkCounterparty2nd = By.XPath("//tr[2]/td[2]/lightning-primitive-cell-checkbox/span/label/span[1]");
        By btnEmail = By.XPath("//button[text()='Email']");
        By btnNewViewAll = By.XPath("//button[@name='New']");
        By titleConfirmEmails = By.XPath("//h2[text()='Confirm emails']");
        By lblMilestone = By.XPath("//label[text()='Milestone']");
        By btnMilestone = By.XPath("//label[text()='Milestone']/ancestor::div[1]/div[1]/lightning-base-combobox/div/div/div[1]/button");
        By valMilestone = By.XPath("//lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item/span[2]/span");
        By lblTemplate = By.XPath("//label[text()='Template']");
        By btnTemplate = By.XPath("//label[text()='Template']/ancestor::div[1]/div[1]/lightning-base-combobox/div/div/div[1]/button");
        By valTemplate = By.XPath("//div/div/div[1]/lightning-combobox/div/div/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item/span[2]/span");
        By btnContactEmail = By.XPath("//span[@title='SkyHive']/ancestor::button");
        By valEmailId = By.XPath("//c-email-message-input/div[1]/div[1]/div/lightning-pill/span/span");
        By valComp = By.XPath("//h2/button/span[@title='SkyHive']");

        By btnCancelConfirm = By.XPath("//button[text()='Cancel']");
        By btnAddRemoveCounterparty = By.XPath("//lightning-layout-item[3]/slot/div/lightning-button-group/div/slot/lightning-button[1]/button");
        By btnSaveCounterparty = By.XPath("//button[@title='Save']");
        By btnBidTrackingReport = By.XPath("//lightning-layout-item[3]/slot/div/lightning-button-group/div/slot/lightning-button[4]/button");
        By btnEditBidsL = By.XPath("//lightning-layout-item[3]/slot/div/lightning-button-group/div/slot/lightning-button[5]/button");
        By btnImport = By.XPath("//lightning-layout-item[3]/slot/div/lightning-button-group/div/slot/lightning-button[6]/button");
        By btnExportData = By.XPath("//lightning-layout-item[3]/slot/div/lightning-button-group/div/slot/lightning-button[6]/button");
        By btnViewDefault = By.XPath("//button[@data-value='All']");
        By valView = By.XPath("//button[@data-value='Buyside Stages']/span");
        By btnEditViewAll = By.XPath("//tr[1]/td[5]/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/lst-list-view-row-level-action/lightning-button-menu/button");
        By lnkEditViewAll = By.XPath("//a[@title='Edit']");
        By lnkDeleteViewAll = By.XPath("//a[@title='Delete']");

        By btnCancelCounterparty = By.XPath("//lightning-layout-item[1]/slot/div/div[4]/lightning-button-group/div/slot/lightning-button[3]/button");
        By btnDeleteCounterparty = By.XPath("//lightning-layout-item[3]/slot/div/lightning-button-group/div/slot/lightning-button[2]/button");
        By btnAddCounterpartiesL = By.XPath("//lightning-layout-item[3]/slot/div/lightning-button-group/div/slot/lightning-button[3]/button");
        By lnkExistingCompanies = By.XPath("//span[text()='Get Companies from existing Company List']");

        By btnEmailCounterparty = By.XPath("//lightning-layout-item[3]/slot/div/lightning-button-group/div/slot/lightning-button[7]/button");
        By btnViewAllCounterparty = By.XPath("//lightning-layout-item[3]/slot/div/lightning-button-group/div/slot/lightning-button[8]/button");
        By titleCounterparty = By.XPath("//h1[@class='slds-page-header__title listViewTitle slds-truncate']");
        By valExistingComp = By.XPath("//table/tbody/tr/td[1]/div/div[2]/div[1]");
        By txtSearch = By.XPath("//input[@placeholder='Search']");
        By btnAddCounterparty = By.XPath("//button[text()='Add Counterparties']");
        By lblCounterparties = By.XPath("//header/div[1]/h2/span[text()='Counterparties']");
        By lblExistingEngagement = By.XPath("//span[@title='Get Companies from existing Engagement']");
        By txtSearchEng = By.XPath("//input[@placeholder='Search Engagement here...']");
        By lblExistingCompanyList = By.XPath("//span[@title='Get Companies from existing Company List']");
        By txtSearchCompanyList = By.XPath("//input[@placeholder='Search Company List here...']");
        By btnBackCounterparties = By.XPath("//button[text()='Back']");
        By titleEngCounterparties = By.XPath("//div[@class='entityNameTitle slds-line-height--reset']");
        By lnkAddedCounterparty = By.XPath("//tr[2]/th//lightning-layout-item[2]/slot/lightning-formatted-url");
        By lblView = By.XPath("//label[text()='View']");
        By txtCompanyList = By.XPath("//label[text()='Company List']/following::input[1]");
        By btnViewAllCompList = By.XPath("//button[text()='View All Company List']");
        By titleCompanyList = By.XPath("//h2[text()='Company List']");
        By radioCompName = By.XPath("//table/tbody/tr/td[2]/div[text()='2022 Summit Attendees']/ancestor::tr/td[1]");
        By btnOK = By.XPath("//button[@title='OK']");
        By chkCompany = By.XPath("//table/tbody/tr[5]/td[1]/lightning-primitive-cell-checkbox/span/label/span[1]");
        By btnAddCounterpartyTo = By.XPath("//button[text()='Add Counterparty to Project Astro']");
        By tblCompanies = By.XPath("//table/tbody/tr[2]/td[1]/div/div[2]/div[1]");
        By comboType = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[1]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[1]/button/span");
        By comboTier = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[1]/button/span");
        By valSelectType = By.XPath("//lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[3]/span[2]/span");
        By valReSelectType = By.XPath("//lightning-layout-item[1]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span");
        By val1stRowType = By.XPath("//lightning-layout-item[1]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[4]/span[2]/span");
        By valSelectTier = By.XPath("//lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[3]/span[2]/span");
        By valReSelectTier = By.XPath("//lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span");
        By val1stRowTier = By.XPath("//lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[4]/span[2]/span");
        By chkCounterCompany = By.XPath("//tr/td[1]/div/div[1]/lightning-input/div/span/label/span[1]");
        By chk2ndCounterCompany = By.XPath("//tr[2]/td[1]/div/div[1]/lightning-input/div/span/label/span[1]");
        By combo2ndType = By.XPath("//tr[2]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[1]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[1]/button/span");
        By combo2ndTier = By.XPath("//tr[2]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[1]/button");
        By valSelect2ndType = By.XPath("//tr[2]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[1]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[3]/span[2]/span");
        By valSelect2ndTier = By.XPath("//tr[2]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[3]/span[2]/span");
        By msgSelectRecord = By.XPath("//span[text()='Please select at least one row to delete.']");

        By msgDeleteRecord = By.XPath("//lightning-confirm/p[text()='Are you sure you want to delete the selected rows ?']");
        By btnDeleteConfirm = By.XPath("//section/div/div/button[text()='OK']");
        By msgDeleteFinal = By.XPath("//span[text()='Displaying 1 to 1 of 1 records. Page 1 of 1.']");
        By valCounterpartyName = By.XPath("//section[3]/div/div[2]/div[1]/div[1]/div/div/div/div/div[2]/div/div[1]/div[2]/div[2]/div[1]/div/div/table/tbody/tr/th/span/a");
        By lblCounterpartyName = By.XPath("//span[@title='Counterparty Name']");
        By lblStatus = By.XPath("//th[3]/div/a/span[@title='Status']");
        By lblDateOfLast = By.XPath("//span[@title='Date of Last Status Change']");
        By btnNew = By.XPath("//div[@title='New']");
        By lnkShowMore = By.XPath("//table/tbody/tr/td[4]/span/div/a/span/span[1]");
        By lnkEdit = By.XPath("//a[@data-target-selection-name='sfdc:StandardButton.Engagement_Counterparty__c.Edit']");
        By lnkDelete = By.XPath("//a[@data-target-selection-name='sfdc:StandardButton.Engagement_Counterparty__c.Delete']");
        By txtSearchBox = By.XPath("//p[1]/lightning-input/lightning-primitive-input-simple/div/div/input");
        By btnSearchContact = By.XPath("//button[@title='Search']");
        By btnEditBids = By.XPath("//button[text()='Edit Bids']");
        By btnNewBidRound = By.XPath("//button[text()='New Bid Round']");
        By btnSelectNewRound = By.XPath("//label[text()='Select New Round']/ancestor::div[1]/div/lightning-base-combobox/div/div/div[1]/button");
        By tabAddedBid = By.XPath("//a[text()='Round Second']");
        By txtMinBid = By.XPath("//input[@name='dt-inline-edit-currency']");
        By txtEquity = By.XPath("//input[@name='dt-inline-edit-number']");
        By txtBidDate = By.XPath("//input[@name='dt-inline-edit-dateLocal']");
        By txtComments = By.XPath("//input[@name='dt-inline-edit-text']");
        By lblMinBid = By.XPath("//span[@title='Min Bid']");
        By btnMaxBid = By.XPath("//lightning-datatable/div[2]/div/div/table/tbody/tr/td[3]/lightning-primitive-cell-factory/span/button");
        By btnEquity = By.XPath("//lightning-datatable/div[2]/div/div/table/tbody/tr/td[4]/lightning-primitive-cell-factory/span/button");
        By btnDebt = By.XPath("//lightning-datatable/div[2]/div/div/table/tbody/tr/td[5]/lightning-primitive-cell-factory/span/button");
        By btnDate = By.XPath("//lightning-datatable/div[2]/div/div/table/tbody/tr/td[6]/lightning-primitive-cell-factory/span/button");
        By btnComments = By.XPath("//lightning-datatable/div[2]/div/div/table/tbody/tr/td[7]/lightning-primitive-cell-factory/span/button");
        By valMinBid = By.XPath("//lightning-tab/slot/lightning-card/article/div[2]/slot/div/lightning-datatable/div[2]/div/div/table/tbody/tr[1]/td[2]/lightning-primitive-cell-factory/span/div/lightning-formatted-number");
        By valMaxBid = By.XPath("//tr[1]/td[3]/lightning-primitive-cell-factory/span/div/lightning-formatted-number");

        By btnSaveBid = By.XPath("//lightning-primitive-datatable-status-bar/div/div/button[2]");
        By btnAddCounterpartyL = By.XPath("//button[text()='Add Counterparty']");
        By valAddedCompany = By.XPath("//records-record-layout-row[2]/slot/records-record-layout-item[1]/div/div/div[2]/span/slot[1]/force-lookup/div/records-hoverable-link/div/a/slot/slot/span");
        By chkMassCheckbox = By.XPath("//lightning-primitive-header-factory/div/span/label/span[1]");
        By btn1stType = By.XPath("//tr[1]/td[3]/lightning-primitive-cell-factory/span/button");
        By btnApply = By.XPath("//button[text()='Apply']");
        By btn1stTier = By.XPath("//tr[1]/td[4]/lightning-primitive-cell-factory/span/button");
        By colDeclined = By.XPath("//c-s-l-custom-datatable-type/div[2]/div/div/table/tbody/tr[1]/td[5]");
        By btn1stDeclined = By.XPath("//tr[1]/td[5]/lightning-primitive-cell-factory/span/button");
        By txtDeclined = By.XPath("//input[@name='dt-inline-edit-dateLocal']");
        By lblDeclined = By.XPath("//a/span[text()='Declined / Passed']");
        By chkSelectItems = By.XPath("//form/lightning-input/lightning-primitive-input-checkbox/div/span/label/span[1]");
        By colInitial = By.XPath("//c-s-l-custom-datatable-type/div[2]/div/div/table/tbody/tr[1]/td[6]");
        By btn1stInitial = By.XPath("//tr[1]/td[6]/lightning-primitive-cell-factory/span/button");
        By colSent = By.XPath("//c-s-l-custom-datatable-type/div[2]/div/div/table/tbody/tr[1]/td[7]");
        By btnSent = By.XPath("//tr[1]/td[7]/lightning-primitive-cell-factory/span/button");
        By colMarkUp = By.XPath("//c-s-l-custom-datatable-type/div[2]/div/div/table/tbody/tr[1]/td[8]");
        By btnMarkUp = By.XPath("//tr[1]/td[8]/lightning-primitive-cell-factory/span/button");
        By colReceived = By.XPath("//c-s-l-custom-datatable-type/div[2]/div/div/table/tbody/tr[1]/td[9]");
        By btnReceived = By.XPath("//tr[1]/td[9]/lightning-primitive-cell-factory/span/button");
        By chk2ndCounterparty = By.XPath("//tr[2]/td[2]/lightning-primitive-cell-checkbox/span/label/span[1]");
        By val1stDeclined = By.XPath("//tr[1]/td[5]/lightning-primitive-cell-factory/span/div/lightning-formatted-date-time");
        By val2ndDeclined = By.XPath("//tr[2]/td[5]/lightning-primitive-cell-factory/span/div/lightning-formatted-date-time");
        By val1stInitial = By.XPath("//tr[1]/td[6]/lightning-primitive-cell-factory/span/div/lightning-formatted-date-time");
        By val2ndInitial = By.XPath("//tr[2]/td[6]/lightning-primitive-cell-factory/span/div/lightning-formatted-date-time");
        By val1stSent = By.XPath("//tr[1]/td[7]/lightning-primitive-cell-factory/span/div/lightning-formatted-date-time");
        By val2ndSent = By.XPath("//tr[2]/td[7]/lightning-primitive-cell-factory/span/div/lightning-formatted-date-time");
        By val1stMarkUpSent = By.XPath("//tr[1]/td[8]/lightning-primitive-cell-factory/span/div/lightning-formatted-date-time");
        By val2ndMarkUpSent = By.XPath("//tr[2]/td[8]/lightning-primitive-cell-factory/span/div/lightning-formatted-date-time");
        By val1stMarkUpRec = By.XPath("//tr[1]/td[9]/lightning-primitive-cell-factory/span/div/lightning-formatted-date-time");
        By val2ndMarkUpRec = By.XPath("//tr[2]/td[9]/lightning-primitive-cell-factory/span/div/lightning-formatted-date-time");

        By valTier1 = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[1]/button/span");
        By chk1stRow = By.XPath("//tr[2]/td[1]/div/div[1]/lightning-input/div/span/label/span[1]");
        By valTier2 = By.XPath("//tr[2]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[1]/button/span");
        By valType2 = By.XPath("//tr[2]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[1]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[1]/button/span");
        By val1stKPINo = By.XPath("//c-s-l-counterparty-data-table/div/div[2]/div/div/article/div/c-s-l-counterparty-total-tabs/div/lightning-layout/slot/lightning-layout-item[1]/slot/div/div[1]");
        By val1stKPIText = By.XPath("//c-s-l-counterparty-data-table/div/div[2]/div/div/article/div/c-s-l-counterparty-total-tabs/div/lightning-layout/slot/lightning-layout-item[1]/slot/div/div[2]");
        By val2ndKPINo = By.XPath("//c-s-l-counterparty-data-table/div/div[2]/div/div/article/div/c-s-l-counterparty-total-tabs/div/lightning-layout/slot/lightning-layout-item[2]/slot/div/div[1]");
        By val2ndKPIText = By.XPath("//c-s-l-counterparty-data-table/div/div[2]/div/div/article/div/c-s-l-counterparty-total-tabs/div/lightning-layout/slot/lightning-layout-item[2]/slot/div/div[2]");
        By val3rdKPINo = By.XPath("//c-s-l-counterparty-data-table/div/div[2]/div/div/article/div/c-s-l-counterparty-total-tabs/div/lightning-layout/slot/lightning-layout-item[3]/slot/div/div[1]");
        By val3rdKPIText = By.XPath("//c-s-l-counterparty-data-table/div/div[2]/div/div/article/div/c-s-l-counterparty-total-tabs/div/lightning-layout/slot/lightning-layout-item[3]/slot/div/div[2]");

        By searchCompany = By.XPath("//input[@placeholder='Search Companies...']");
        By comboResultCompany = By.XPath("(//ul[@role='group']//li)[1]");
        By comboTypeCounterparty = By.XPath("(//lightning-base-combobox//button[contains(@aria-label,'Type')])[2]");
        By buttonSaveL = By.XPath("//button[@name='SaveEdit']");
        By msgLVPopup = By.CssSelector("span.toastMessage.forceActionsText");

        //To Click Counterparties button
        public string ClickAddCounterpartiesbutton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterparties, 60);
            driver.FindElement(btnAddCounterparties).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titlePage, 60);
            string title = driver.FindElement(titlePage).Text;
            return title;
        }

        public void ClickAddCounterpartiesBtn()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterparty, 60);
            driver.FindElement(btnAddCounterparty).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterpartyL, 60);
        }

        public bool VerifyAddCounterpartiesPageIsDisplayed()
        {
            bool result = false;
            if(driver.FindElement(btnAddCounterpartyL).Displayed)
            {
                result = true;
            }
            return result;
        }

        private By _comboTypeCounterpartyOptionEle(string value)
        {
            return By.XPath($"//span[@title='{value}']");
        }

        public void AddNewCounterparty(string companyName, string value)
        {
            driver.FindElement(btnAddCounterpartyL).Click();
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, searchCompany, 30);
            IWebElement companySearch = driver.FindElement(searchCompany);
            companySearch.Click();
            companySearch.SendKeys(companyName);
            WebDriverWaits.WaitUntilEleVisible(driver, comboResultCompany, 10);
            driver.FindElement(comboResultCompany).Click();
            Thread.Sleep(5000);
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].click();", driver.FindElement(comboTypeCounterparty));

                //driver.FindElement(comboTypeCounterparty).Click();
                //Thread.Sleep(2000);
                driver.FindElement(_comboTypeCounterpartyOptionEle(value)).Click();
                Thread.Sleep(2000);
            }
            catch(Exception)
            {

            }
            
            driver.FindElement(buttonSaveL).Click();
            Thread.Sleep(5000);
        }

        public string GetLVMessagePopup()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgLVPopup, 30);
            return driver.FindElement(msgLVPopup).Text;
        }

        public void CloseTab(string tabName)
        {
            Thread.Sleep(5000);
            driver.FindElement(By.XPath($"//button[contains(@title,'Close {tabName}')]")).Click();
            Thread.Sleep(5000);
        }

        public void ClickEditBidsButtonAndNavigateToBidRoundsPage(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditBids, 30);
            driver.FindElement(btnEditBids).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnNewBidRound, 150);
            driver.FindElement(btnNewBidRound).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnSelectNewRound, 200);
            driver.FindElement(btnSelectNewRound).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{value}']")).Click();
            Thread.Sleep(3000);
        }

        //To validate Counterparties button
        public string AddCounterparties()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnDel, 90);
                driver.FindElement(btnDel).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnAddRow, 60);
                driver.FindElement(btnAddRow).Click();
                driver.FindElement(comboCity).SendKeys("Company Name");
                driver.FindElement(txtCityName).SendKeys("Test");
                driver.FindElement(btnSearch).Click();
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, checkCity, 120);
                driver.FindElement(checkCity).Click();
                driver.FindElement(btnAddRec).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 100);
                string message = driver.FindElement(msgSuccess).Text;
                driver.FindElement(btnBack).Click();
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, lnkDetails, 120);
                driver.FindElement(lnkDetails).Click();
                return message;
            }
            catch(Exception)
            {
                driver.FindElement(lnkDetails).Click();
                return "Record is already displayed";
            }
        }


        public void ClickDetailsLink()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDetails, 190);
            driver.FindElement(lnkDetails).Click();
        }

        public string ClickAddCounterPartyContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddEngCounterPartyÇontact, 80);
            driver.FindElement(btnAddEngCounterPartyÇontact).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            WebDriverWaits.WaitUntilEleVisible(driver, titleCPSearch, 120);
            string title = driver.FindElement(titleCPSearch).Text;
            return title;
        }

        // To validate cancel functionality of CP Contact
        public string ValidateCancelFunctionalityOFCPContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkName, 100);
            driver.FindElement(checkName).Click();
            driver.FindElement(btnSave).Click();
            driver.SwitchTo().Alert().Dismiss();
            try
            {
                string message = driver.FindElement(msgSuccess).Text;
                return message;
            }
            catch(Exception)
            {
                return "Record is not addded";
            }
        }

        //To validate Please select at least one contact before save Message
        public string ValidateErrorMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkName, 100);
            driver.FindElement(checkName).Click();
            driver.FindElement(btnSave).Click();
            driver.SwitchTo().Alert().Accept();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 80);
                string message = driver.FindElement(msgSuccess).Text.Replace("\r\n", " ");
                return message;
            }
            catch(Exception)
            {
                return "Message is not displayed";
            }
        }

        // To validate Save functionality of CP Contact
        public string SaveCounterpartyContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkName, 80);
            driver.FindElement(checkName).Click();
            driver.FindElement(btnSave).Click();
            driver.SwitchTo().Alert().Accept();
            try
            {
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessContact, 150);
                Thread.Sleep(4000);
                string message = driver.FindElement(msgSuccessContact).Text.Replace("\r\n", " ");
                WebDriverWaits.WaitUntilEleVisible(driver, btnTableBack, 100);
                driver.FindElement(btnTableBack).Click();
                return message;
            }
            catch(Exception)
            {
                return "Record is not addded";
            }
        }

        //Delete the added Counterparty
        public string DeleteAddedCP()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagement, 80);
            driver.FindElement(lnkEngagement).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnCounterParties, 80);
            driver.FindElement(btnCounterParties).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkRec, 80);
            driver.FindElement(checkRec).Click();
            driver.FindElement(btnDelete).Click();
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgText, 120);
            string message = driver.FindElement(msgText).Text;
            return message;
        }

        //Add company from existing Engagement
        public string AddCompanyFromExistingEng(string Name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblExistingEng, 80);
            driver.FindElement(lblExistingEng).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngage, 80);
            driver.FindElement(txtEngage).SendKeys(Name);
            driver.FindElement(btnSearchEng).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkCity, 80);
            driver.FindElement(checkCity).Click();
            driver.FindElement(btnAddRec).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 80);
            string message = driver.FindElement(msgSuccess).Text;
            return message;
        }

        //Validate page after clicking back button
        public string ClickBackAndGetTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBack, 60);
            driver.FindElement(btnBack).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titlePage, 60);
            string title = driver.FindElement(titlePage).Text;
            return title;
        }


        //Validate if company get added
        public string ValidateAddedCompanyExists()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkRow, 80);
            string value = driver.FindElement(checkRow).Displayed.ToString();
            return value;
        }

        //Delete the added company
        public string DeleteAddedCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkRow, 80);
            driver.FindElement(checkRow).Click();
            driver.FindElement(btnDelete).Click();
            Thread.Sleep(4000);
            string text = driver.FindElement(msgText).Text;
            return text;
        }

        //Click on New Bid and validate page 

        public string ClickNewBidAndValidatePage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewBid, 80);
            driver.FindElement(btnNewBid).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titlePage, 80);
            string text = driver.FindElement(titlePage).Text;
            return text;
        }

        // To validate cancel functionality of Bid
        public string ValidateCancelFunctionalityOFBid()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDate, 80);
            driver.FindElement(lnkDate).Click();
            driver.FindElement(btnCancel).Click();
            string page = driver.FindElement(titlePage).Text;
            return page;
        }

        // To validate No Records message
        public string ValidateNoRecordsMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgNoRec, 80);
            string message = driver.FindElement(msgNoRec).Text;
            return message;
        }

        //Save Bid details
        public string SaveBidDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDate, 80);
            driver.FindElement(lnkDate).Click();
            driver.FindElement(btnBidSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valBidDate, 100);
            string date = driver.FindElement(valBidDate).Text;
            return date;
        }

        // To validate edit functionality of Bid
        public string ValidateEditFunctionalityOFBid(string date)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkBidEdit, 80);
            driver.FindElement(lnkBidEdit).Click();
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(date);
            driver.FindElement(btnBidSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valBidDate, 100);
            string valDate = driver.FindElement(valBidDate).Text;
            return valDate;
        }

        // To validate delete functionality of Bid
        public string ValidateDeleteFunctionalityOFBid()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkBidDel, 80);
            driver.FindElement(lnkBidDel).Click();
            driver.SwitchTo().Alert().Accept();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, msgNoRec, 100);
            string message = driver.FindElement(msgNoRec).Text;
            return message;
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

        public string ValidateEngCounterpartiesPostClickingBackButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, titleEngCounterparties, 80);
            string name = driver.FindElement(titleEngCounterparties).Text;
            return name;
        }

        public void ClickAddedCounterparty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkAddedCounterparty, 150);
            driver.FindElement(lnkAddedCounterparty).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(7000);
        }

        //Get 1st Name
        public string Get1stName()
        {
            Thread.Sleep(10000);
            var element = driver.FindElement(lnkContacts);
            Actions action = new Actions(driver);
            action.MoveToElement(element);
            action.Perform();
            //driver.FindElement(btnPrintableView).Click();
            //Thread.Sleep(5000);
            // driver.SwitchTo().Window(driver.WindowHandles.Last());            
            WebDriverWaits.WaitUntilEleVisible(driver, valFirstName, 180);
            string name = driver.FindElement(valFirstName).Text;
            return name;
        }

        //Get 1st Name
        public string GetCounterparty1stName()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valCounterparty1stName, 120);
            string name = driver.FindElement(valCounterparty1stName).Text;
            return name;
        }

        public string GetCounterparty2ndName()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valCounterparty2ndName, 120);
            string name = driver.FindElement(valCounterparty2ndName).Text;
            return name;
        }

        //Get 2nd Name
        public string Get2ndName()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, valLastName, 80);
            string name = driver.FindElement(valLastName).Text;
            driver.SwitchTo().Window(driver.WindowHandles.First());
            Thread.Sleep(5000);
            return name;
        }

        //Get 1st Name
        public string GetContact1stName()
        {
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//li[3]//span[text()='Counterparty Editor']")).Click();
            Thread.Sleep(5000);
            var element = driver.FindElement(lnkContacts);
            Actions action = new Actions(driver);
            action.MoveToElement(element);
            action.Perform();
            WebDriverWaits.WaitUntilEleVisible(driver, valContactFirstName, 180);
            string name = driver.FindElement(valContactFirstName).Text;
            return name;
        }

        //Get 2nd Name
        public string GetContact2ndName()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, valContactLastName, 80);
            string name = driver.FindElement(valContactLastName).Text;
            // driver.SwitchTo().Window(driver.WindowHandles.First());
            Thread.Sleep(5000);
            return name;
        }
        //Validate contact on Counterparties page
        public string ValidateContactDetailsOnCounterpartiesPage()
        {
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            Thread.Sleep(4000);
            driver.Navigate().Refresh();
            Thread.Sleep(9000);
            //var element = driver.FindElement(lnkContacts);
            //Actions action = new Actions(driver);
            //action.MoveToElement(element);
            //action.Perform();
            //Thread.Sleep(5000);
            //IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            //js.ExecuteScript("window.scrollTo(0,100)");
            //Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkContacts, 80);
            string name = driver.FindElement(lnkContacts).Text;
            return name;
        }
        //Get added company of Ccounterparty
        public string GetCompanyOfCounterparty()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, lnkCompCounterparty, 80);
            string name = driver.FindElement(lnkCompCounterparty).Text;
            return name;
        }

        //Change the default view
        public string UpdateDefaultView()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnView, 80);
            driver.FindElement(btnView).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valUpdView, 80);
            driver.FindElement(valUpdView).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, selectedView, 80);
            string name = driver.FindElement(selectedView).Text;
            return name;
        }


        //Validate the displayed records
        public string ValidateCounterpartyRecords()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tblCounterparty, 180);
            string value = driver.FindElement(tblCounterparty).Displayed.ToString();
            Console.WriteLine(value);
            if(value.Equals("True"))
            {
                return "Counterparty records are displayed";
            }
            else
            {
                return "Counterparty records are not displayed";
            }
        }

        //Validate New  button on View all page
        public string ValidateViewAllNewButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewViewAll, 180);
            string name = driver.FindElement(btnNewViewAll).Text;
            return name;
        }

        //Validate the edit link on View All page
        public string ValidateEditLinkOnViewAll()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditViewAll, 180);
            driver.FindElement(btnEditViewAll).Click();
            Thread.Sleep(4000);
            string name = driver.FindElement(lnkEditViewAll).Text;
            return name;
        }

        //Validate the delete link on View All page
        public string ValidateDeleteLinkOnViewAll()
        {
            string name = driver.FindElement(lnkDeleteViewAll).Text;
            return name;
        }

        //Select the counterparty and click the email button
        public string SelectCounterpartyAndClickEmailButton()
        {
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkCounterparty2nd, 180);
            driver.FindElement(chkCounterparty2nd).Click();
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEmail, 80);
            driver.FindElement(btnEmail).Click();
            string name = driver.FindElement(titleConfirmEmails).Text;
            return name;
        }

        //Validate Milestone dropdown with its values
        public string ValidateMilestoneDropdown()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblMilestone, 80);
            string name = driver.FindElement(lblMilestone).Text;
            return name;
        }

        //Validate Milestone dropdown's values
        public bool ValidateMilestoneValues()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnMilestone, 80);
            driver.FindElement(btnMilestone).Click();
            Thread.Sleep(3000);
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(valMilestone);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "None", "Sent CA", "Sent Teaser" };
            bool isSame = true;

            if(expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for(int rec = 0; rec < expectedValue.Length; rec++)
            {
                if(!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //Validate Template dropdown 
        public string ValidateTemplateDropdown()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, titleConfirmEmails, 80);
            driver.FindElement(titleConfirmEmails).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblTemplate, 80);
            string name = driver.FindElement(lblTemplate).Text;
            return name;
        }

        //Validate Template dropdown's values
        public bool ValidateTemplateValues()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnTemplate, 100);
            driver.FindElement(btnTemplate).Click();
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(valTemplate);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Counterparty Email", "Counterparty Email Clone" };
            bool isSame = true;

            if(expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for(int rec = 0; rec < expectedValue.Length; rec++)
            {
                if(!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            // driver.FindElement(btnCancelConfirm).Click();
            return isSame;
        }

        //Select template and get email id
        public string ValidateEmailIdOnEmailTemplate()
        {
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//div/div/div[1]/lightning-combobox/div/div/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[1]/span[2]/span")).Click();
            Thread.Sleep(7000);
            driver.FindElement(btnContactEmail).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(valEmailId).Text;
            return value;
        }

        //Get Company of added counterparty
        public string GetCompanyOfAddedCounterparty()
        {
            string value = driver.FindElement(valComp).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelConfirm);
            driver.FindElement(btnCancelConfirm).Click();
            return value;
        }
        //Get View default value

        public string GetViewValue()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewDefault, 150);
            driver.FindElement(btnViewDefault).Click();
            driver.FindElement(By.XPath("//lightning-base-combobox-item//span[text()='Buyside Stages']")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valView, 150);
            string value = driver.FindElement(valView).Text.Substring(0, 7);
            return value;

        }
        //Validate Save button
        public string ValidateAddRemoveColumnsButton()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddRemoveCounterparty, 150);
            string value = driver.FindElement(btnAddRemoveCounterparty).Text;
            return value;
        }

        //Validate Delete button
        public string ValidateDeleteButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCounterparty, 150);
            string value = driver.FindElement(btnDeleteCounterparty).Text;
            return value;
        }

        //Validate View All functionality
        public string ValidateViewAllFunctionality()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewAllCounterparty, 150);
            driver.FindElement(btnViewAllCounterparty).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(titleCounterparty).Text;
            return value;
        }

        //Validate Bid Tracking Report button
        public string ValidateBidTrackingReportButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBidTrackingReport, 150);
            string value = driver.FindElement(btnBidTrackingReport).Text;
            return value;
        }

        //Validate Edit Bids button
        public string ValidateEditBidsButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditBidsL, 150);
            string value = driver.FindElement(btnEditBidsL).Text;
            return value;
        }

        //Validate Import with Dataloader button
        public string ValidateImportWithDataloaderButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnImport, 150);
            string value = driver.FindElement(btnImport).Text;
            return value;
        }

        //Validate Export Data button
        public string ValidateExportDataButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnExportData, 150);
            string value = driver.FindElement(btnExportData).Text;
            return value;
        }

        //Validate Add Counterparty button
        public string ValidateAddCounterpartyButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterpartiesL, 150);
            string value = driver.FindElement(btnAddCounterpartiesL).Text;
            return value;
        }

        //Validate Email button
        public string ValidateEmailButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEmailCounterparty, 150);
            string value = driver.FindElement(btnEmailCounterparty).Text;
            return value;
        }

        //Validate View All button
        public string ValidateViewAllButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewAllCounterparty, 150);
            string value = driver.FindElement(btnViewAllCounterparty).Text;
            return value;
        }

        //Validate Search button
        public string ValidateSearchTextBoxButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearch, 150);
            string value = driver.FindElement(txtSearch).GetAttribute("placeholder");
            return value;
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
            Thread.Sleep(9000);
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
            driver.FindElement(txtSearchCounterparty).SendKeys("SkyHive");
            driver.FindElement(txtSearchCounterparty).Click();
            Thread.Sleep(8000);
            string name = driver.FindElement(lnkCompCounterparty).Text;
            return name;

        }











        //Update the value of Type and Tier and click click
        public string UpdateTypeTierAndClickCancel()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chkCounterCompany, 150);
            driver.FindElement(chkCounterCompany).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboType, 180);
            driver.FindElement(comboType).Click();
            var element = driver.FindElement(By.XPath("//lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[3]/span[2]/span"));
            Actions action = new Actions(driver);
            action.MoveToElement(element);
            action.Perform();
            driver.FindElement(valSelectType).Click();
            Thread.Sleep(3000);
            driver.FindElement(comboTier).Click();
            var element1 = driver.FindElement(By.XPath("//lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[3]/span[2]/span"));
            Actions action1 = new Actions(driver);
            action1.MoveToElement(element1);
            action1.Perform();
            driver.FindElement(valSelectTier).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnCancelCounterparty).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboType, 150);
            string value = driver.FindElement(comboType).Text;
            return value;
        }

        //Update the value of 2nd Type and Tier and click Save
        public string UpdateTypeTierOf2ndCompanyAndClickSave()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chk2ndCounterCompany, 150);
            driver.FindElement(chk2ndCounterCompany).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, combo2ndType, 150);
            driver.FindElement(combo2ndType).Click();
            var element = driver.FindElement(By.XPath("//tr[2]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[1]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[3]/span[2]/span"));
            Actions action = new Actions(driver);
            action.MoveToElement(element);
            action.Perform();
            driver.FindElement(valSelect2ndType).Click();
            Thread.Sleep(3000);
            driver.FindElement(combo2ndTier).Click();
            var element1 = driver.FindElement(By.XPath("//tr[2]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[3]/span[2]/span"));
            Actions action1 = new Actions(driver);
            action1.MoveToElement(element1);
            action1.Perform();
            driver.FindElement(valSelect2ndTier).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnSaveCounterparty).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, combo2ndType, 150);
            string value = driver.FindElement(combo2ndType).Text;
            return value;
        }


        //Get the value of Tier dropdown for 2nd Counterparty
        public string GetValueOfTierOf2ndCounterparty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, combo2ndTier, 150);
            string value = driver.FindElement(combo2ndTier).Text;
            return value;
        }
        //Click on Add Counterparties and validate the page
        public string ClickAddCounterpartiesAndValidatePage()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterparty, 170);
            driver.FindElement(btnAddCounterparty).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblCounterparties, 250);
            string value = driver.FindElement(lblCounterparties).Text;
            return value;
        }

        //Click on Add Counterparty
        public void ClickAddCounterparty()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterpartyL, 150);
            driver.FindElement(btnAddCounterpartyL).Click();
            Thread.Sleep(4000);
        }

        //Validate added Company Name
        public string ValidateAddedCompany()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedCompany, 150);
            string value = driver.FindElement(valAddedCompany).Text;
            return value;
        }

        //Validate label Get Companies From Existing Engagement
        public string ValidateLabelGetCompaniesFromExistingEng()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblExistingEngagement, 150);
            string name = driver.FindElement(lblExistingEngagement).Text;
            return name;
        }

        //Validate Search Box
        public string ValidateSearchBox()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchEng, 150);
            string name = driver.FindElement(txtSearchEng).GetAttribute("type");
            return name;
        }

        //Validate label Get Companies From Existing Company List
        public string ValidateLabelGetCompaniesFromExistingCompanyList()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblExistingCompanyList, 150);
            string name = driver.FindElement(lblExistingCompanyList).Text;
            return name;
        }

        //Expand Get Companies From Existing Company List section and validate search box
        public string ExpandCompanyListAndValidateSearchBox()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblExistingCompanyList, 250);
            driver.FindElement(lblExistingCompanyList).Click();
            Thread.Sleep(5000);
            string name = driver.FindElement(txtSearchCompanyList).GetAttribute("type");
            return name;
        }

        //Click Back button and validate counterparties list
        public string ClickBackButtonAndValidatePage()
        {
            //Thread.Sleep(3000);
            //WebDriverWaits.WaitUntilEleVisible(driver,btnBackCounterparties,150);
            //driver.FindElement(btnBackCounterparties).Click();
            //Thread.Sleep(6000);
            string name = driver.FindElement(lblView).Text;
            return name;
        }

        //Validate the validation without selecting any records
        public string ValidateSelectAnyRecordValidation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chk2ndCounterCompany, 150);
            driver.FindElement(chk2ndCounterCompany).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnDeleteCounterparty).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCounterparty, 150);
            driver.FindElement(btnDeleteCounterparty).Click();
            string text = driver.FindElement(msgSelectRecord).Text;
            return text;
        }

        //Validate Engagement CP Comment
        public string ValidateEngCPCommentOnCounterpartyPage()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedCPComment, 90);
            string name = driver.FindElement(valAddedCPComment).Text;
            return name;
        }
        //Validate Engageemnt CP Comment
        public void ValidateEngCPComment()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabCounterpartyEditor, 120);
            driver.FindElement(tabCounterpartyEditor).Click();
            Thread.Sleep(6000);
            //driver.SwitchTo().Window(driver.WindowHandles.Last());
            //Thread.Sleep(7000);
            //driver.FindElement(txtCPComments).SendKeys("Testing");
            //driver.FindElement(btnSaveCPComment).Click();
            //Thread.Sleep(5000);
            //WebDriverWaits.WaitUntilEleVisible(driver, valAddedCPComment, 90);
            //string name = driver.FindElement(valAddedCPComment).Text;
            //return name;
        }
        //Validate Engageemnt CP Comment
        //public string ValidateEngCPComment()
        //{
        //    Thread.Sleep(5000);
        //    WebDriverWaits.WaitUntilEleVisible(driver, tabCounterpartyEditor, 90);
        //    driver.FindElement(tabCounterpartyEditor).Click();
        //    Thread.Sleep(6000);
        //    driver.SwitchTo().Window(driver.WindowHandles.Last());
        //    Thread.Sleep(7000);
        //    driver.FindElement(txtCPComments).SendKeys("Testing");
        //    driver.FindElement(btnSaveCPComment).Click();
        //    Thread.Sleep(5000);
        //    WebDriverWaits.WaitUntilEleVisible(driver, valAddedCPComment, 90);
        //    string name = driver.FindElement(valAddedCPComment).Text;
        //    return name;
        //}

        public string GetCPCommentType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCPCommentType, 90);
            string name = driver.FindElement(valCPCommentType).Text;
            return name;

        }

        public string GetCPCommentCreator()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCPCommentCreator, 90);
            string name = driver.FindElement(valCPCommentCreator).Text;
            return name;

        }

        public string GetCPCommentCreatedDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCPCommentCreatedDate, 90);
            string name = driver.FindElement(valCPCommentCreatedDate).Text;
            return name;

        }
        //Deleted added counterparty comment
        public string DeleteEngCounterpartyComment()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditCounterpartyComment);
            driver.FindElement(btnEditCounterpartyComment).Click();
            Thread.Sleep(4000);
            driver.FindElement(lnkDeleteCounterpartyComment).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCounterpartyComment, 120);
            driver.FindElement(btnDeleteCounterpartyComment).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(valPostDeleteCounterpartyComment).Text;
            return value;
        }
        //Delete Engagement Counterparty Contact
        public string SelectAnyRecordAndClickDelete()
        {
            Thread.Sleep(4000);
            driver.Navigate().Refresh();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkCounterparty2nd, 150);
            driver.FindElement(chkCounterparty2nd).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCounterparty, 150);
            driver.FindElement(btnDeleteCounterparty).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgDeleteRecord, 270);
            string text = driver.FindElement(msgDeleteRecord).Text;
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteConfirm, 250);
            driver.FindElement(btnDeleteConfirm).Click();

            return text;
        }

        //Validate if record has been deleted
        public string ValidateIfRecordIsDeleted()
        {
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgDeleteFinal, 150);
            string message = driver.FindElement(msgDeleteFinal).Text;
            return message;
        }

        //Validate if 2nd company still exists
        public string Validate2ndCompanyPostDeletion()
        {
            try
            {
                Thread.Sleep(5000);
                string name = driver.FindElement(tblCompanies).Text;
                return name;

            }
            catch(Exception)
            {
                return "2nd company does not exist";
            }
        }

        //Clik Edit bid button
        public string ClickEditBidAndValidateNewTab()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditBidsL, 150);
            driver.FindElement(btnEditBidsL).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewBidRound, 150);
            driver.FindElement(btnNewBidRound).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSelectNewRound, 200);
            driver.FindElement(btnSelectNewRound).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div/div[2]/lightning-base-combobox-item[@data-value='Second']")).Click();
            string tab = driver.FindElement(tabAddedBid).Text;
            //driver.Navigate().Refresh();
            return tab;
        }

        //Add Bid values
        public string SaveBidValues(string value)
        {
            Thread.Sleep(5000);
            driver.FindElement(txtMinBid).Clear();
            driver.FindElement(txtMinBid).SendKeys(value);
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblMinBid, 200);
            driver.FindElement(lblMinBid).Click();
            //Max Bid
            var elementMax = driver.FindElement(By.XPath("//td[@data-label='Max Bid']"));
            Actions actions = new Actions(driver);
            actions.MoveToElement(elementMax);
            actions.Perform();
            Thread.Sleep(4000);
            driver.FindElement(btnMaxBid).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtMinBid).Clear();
            driver.FindElement(txtMinBid).SendKeys(value);

            try
            {
                //Equity %
                var elementEquity = driver.FindElement(By.XPath("//td[@data-label='Equity %']"));
                actions.MoveToElement(elementEquity);
                actions.Perform();
                Thread.Sleep(3000);
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnEquity));
                //driver.FindElement(btnEquity).Click();
                Thread.Sleep(3000);
                driver.FindElement(txtEquity).Clear();
                driver.FindElement(txtEquity).SendKeys(value);
                driver.FindElement(lblMinBid).Click();
                //Debt %
                var elementDebt = driver.FindElement(By.XPath("//td[@data-label='Debt %']"));
                actions.MoveToElement(elementDebt);
                actions.Perform();
                Thread.Sleep(4000);
                driver.FindElement(btnDebt).Click();
                Thread.Sleep(4000);
                driver.FindElement(txtEquity).Clear();
                driver.FindElement(txtEquity).SendKeys(value);
                driver.FindElement(lblMinBid).Click();
                //Bid Date
                var elementDate = driver.FindElement(By.XPath("//td[@data-label='Bid Date']"));
                actions.MoveToElement(elementDate);
                actions.Perform();
                Thread.Sleep(4000);
                driver.FindElement(btnDate).Click();
                Thread.Sleep(4000);
                driver.FindElement(txtBidDate).Clear();
                driver.FindElement(txtBidDate).SendKeys(DateTime.Today.ToString("MMM dd, yyyy"));
                driver.FindElement(lblMinBid).Click();
                //Bid Comments
                var elementComments = driver.FindElement(By.XPath("//td[@data-label='Comments']"));
                actions.MoveToElement(elementComments);
                actions.Perform();
                Thread.Sleep(4000);
                driver.FindElement(btnComments).Click();
                Thread.Sleep(4000);
                driver.FindElement(txtComments).Clear();
                driver.FindElement(txtComments).SendKeys("Testing");
                driver.FindElement(lblMinBid).Click();
            }
            catch(Exception e)
            {
                Console.WriteLine("Element not found: " + e.Message);
            }

            //Save button
            driver.FindElement(btnSaveBid).Click();
            Thread.Sleep(10000);
            string bid = driver.FindElement(valMinBid).Text;
            return bid.Substring(1, 2);
        }

        //Get value of Max bid
        public string GetMaxBid()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valMaxBid, 80);
            string bid = driver.FindElement(valMaxBid).Text;
            return bid.Substring(1, 2);
        }

        //Validate added bid details on counterparty page
        public string ValidateMinBidDetailOnCounterpartiesPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabCounterpartyEditor, 80);
            Thread.Sleep(5000);
            driver.FindElement(tabCounterpartyEditor).Click();
            Thread.Sleep(5000);
            driver.FindElement(lnk2ndCompCounterparty).Click();
            Thread.Sleep(7000);
            driver.Navigate().Refresh();
            Console.WriteLine("About to fetch the element");
            //WebDriverWaits.WaitUntilEleVisible(driver, valMinRoundBid, 200);
            string bid = driver.FindElement(valMinRoundBid).Text;
            return bid;
        }

        //Validate added bid details on counterparty page
        public string ValidateMaxBidDetailOnCounterpartiesPage()
        {
            string bid = driver.FindElement(valMaxRoundBid).Text;
            return bid;
        }



        //Check Mass checkbox
        public void ClickMassCheckbox()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkMassCheckbox, 250);
            driver.FindElement(chkMassCheckbox).Click();
        }

        public string GetValueOfTier()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTier1, 90);
            string value = driver.FindElement(valTier1).Text;
            return value;
        }

        //Enter all the values for available columns and click Save button
        public string ValidateSaveFunctonalityAfterEnteringValuesForAllColumns(string date)
        {
            Thread.Sleep(5000);
            Actions actions = new Actions(driver);

            //Update Declined
            var elementDeclined = driver.FindElement(colDeclined);
            actions.MoveToElement(elementDeclined);
            actions.Perform();
            driver.FindElement(btn1stDeclined).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtDeclined).Clear();
            driver.FindElement(txtDeclined).SendKeys(date);
            driver.FindElement(chkSelectItems).Click();
            driver.FindElement(lblDeclined).Click();
            driver.FindElement(btnApply).Click();

            //Update Initial Contact
            var elementInitial = driver.FindElement(colInitial);
            actions.MoveToElement(elementInitial);
            actions.Perform();
            driver.FindElement(btn1stInitial).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtDeclined).Clear();
            driver.FindElement(txtDeclined).SendKeys(date);
            driver.FindElement(chkSelectItems).Click();
            driver.FindElement(lblDeclined).Click();
            driver.FindElement(btnApply).Click();

            //Update Sent Teaser
            var elementSent = driver.FindElement(colSent);
            actions.MoveToElement(elementSent);
            actions.Perform();
            driver.FindElement(btnSent).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtDeclined).Clear();
            driver.FindElement(txtDeclined).SendKeys(date);
            driver.FindElement(chkSelectItems).Click();
            driver.FindElement(lblDeclined).Click();
            driver.FindElement(btnApply).Click();

            //Update Markup Sent
            var elementMarkUp = driver.FindElement(colMarkUp);
            actions.MoveToElement(elementMarkUp);
            actions.Perform();
            driver.FindElement(btnMarkUp).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtDeclined).Clear();
            driver.FindElement(txtDeclined).SendKeys(date);
            driver.FindElement(chkSelectItems).Click();
            driver.FindElement(lblDeclined).Click();
            driver.FindElement(btnApply).Click();

            //Update Markup Received
            IJavaScriptExecutor js = (IJavaScriptExecutor) Driver;
            js.ExecuteScript("window.scrollBy(300,0)");
            var elementReceived = driver.FindElement(colReceived);
            actions.MoveToElement(elementReceived);
            actions.Perform();
            Thread.Sleep(4000);
            driver.FindElement(btnReceived).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtDeclined).Clear();
            driver.FindElement(txtDeclined).SendKeys(date);
            driver.FindElement(chkSelectItems).Click();
            driver.FindElement(lblDeclined).Click();
            driver.FindElement(btnApply).Click();
            Thread.Sleep(5000);

            driver.FindElement(btnSaveCounterparty).Click();
            Thread.Sleep(5000);
            driver.Navigate().Refresh();
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, val1stDeclined, 90);
            string value = driver.FindElement(val1stDeclined).Text;
            return value;
        }


        //Get Value of Declined of 2nd row as well
        public string GetDeclinedDateOf2ndCounterparty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, val2ndDeclined, 90);
            string value = driver.FindElement(val2ndDeclined).Text;
            return value;
        }

        //Get Value of Initial contact of 1st row as well
        public string GetInitialContactOf1stCounterparty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, val1stInitial, 90);
            string value = driver.FindElement(val1stInitial).Text;
            return value;
        }

        //Get Value of Initial contact of 2nd row as well
        public string GetInitialContactOf2ndCounterparty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, val2ndInitial, 90);
            string value = driver.FindElement(val2ndInitial).Text;
            return value;
        }

        //Get Value of Sent Teaser of 1st row as well
        public string GetSentTeaserOf1stCounterparty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, val1stSent, 90);
            string value = driver.FindElement(val1stSent).Text;
            return value;
        }

        //Get Value of Sent Teaser of 2nd row as well
        public string GetSentTeaserOf2ndCounterparty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, val2ndSent, 90);
            string value = driver.FindElement(val2ndSent).Text;
            return value;
        }

        //Get Value of Mark up sent of 1st row as well
        public string GetMarkUpSentOf1stCounterparty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, val1stMarkUpSent, 90);
            string value = driver.FindElement(val1stMarkUpSent).Text;
            return value;
        }

        //Get Value of Mark up sent of 2nd row as well
        public string GetMarkUpSentOf2ndCounterparty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, val2ndMarkUpSent, 90);
            string value = driver.FindElement(val2ndMarkUpSent).Text;
            return value;
        }

        //Get Value of Mark up received of 1st row as well
        public string GetMarkUpRecOf1stCounterparty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, val1stMarkUpRec, 90);
            string value = driver.FindElement(val1stMarkUpRec).Text;
            return value;
        }

        //Get Value of Mark up received of 2nd row as well
        public string GetMarkUpRecOf2ndCounterparty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, val2ndMarkUpRec, 90);
            string value = driver.FindElement(val2ndMarkUpRec).Text;
            return value;
        }

        //Enter all the values for available columns and click Save button
        public string SelectOnlyOneRowAndValidateSaveFunctionality(string date)
        {
            Thread.Sleep(5000);
            Actions actions = new Actions(driver);

            //Update Declined
            driver.FindElement(chk2ndCounterparty).Click();
            var elementDeclined = driver.FindElement(colDeclined);
            actions.MoveToElement(elementDeclined);
            actions.Perform();
            driver.FindElement(btn1stDeclined).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtDeclined).Clear();
            driver.FindElement(txtDeclined).SendKeys(date);
            driver.FindElement(lblDeclined).Click();

            //Update Initial Contact
            var elementInitial = driver.FindElement(colInitial);
            actions.MoveToElement(elementInitial);
            actions.Perform();
            driver.FindElement(btn1stInitial).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtDeclined).Clear();
            driver.FindElement(txtDeclined).SendKeys(date);
            driver.FindElement(lblDeclined).Click();


            driver.FindElement(btnSaveCounterparty).Click();
            Thread.Sleep(5000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, val1stDeclined, 90);
            string value = driver.FindElement(val1stDeclined).Text;
            return value;
        }
        //Reset Tyep and Tire columns and click Save button
        public void ResetTypeAndTireColumns()
        {
            Thread.Sleep(5000);
            driver.FindElement(btn1stType).Click();
            var element = driver.FindElement(By.XPath("//lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span"));
            Actions action = new Actions(driver);
            action.MoveToElement(element);
            action.Perform();
            Thread.Sleep(2000);
            driver.FindElement(valSelectType).Click();
            Thread.Sleep(3000);
            driver.FindElement(btn1stTier).Click();
            var element1 = driver.FindElement(By.XPath("//lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span"));
            Actions action1 = new Actions(driver);
            action1.MoveToElement(element1);
            action1.Perform();
            driver.FindElement(valSelectTier).Click();
            driver.FindElement(btnSaveCounterparty).Click();
            Thread.Sleep(4000);
        }

        //Select 2nd row
        public void SelectOnly1stRow()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chk1stRow, 90);
            driver.FindElement(chk1stRow).Click();
        }

        //Update only selected row
        public void UpdateOnlySelectedRow()
        {
            Thread.Sleep(5000);
            driver.FindElement(btn1stType).Click();
            var element = driver.FindElement(By.XPath("//lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[4]/span[2]/span"));
            Actions action = new Actions(driver);
            action.MoveToElement(element);
            action.Perform();
            Thread.Sleep(2000);
            driver.FindElement(val1stRowType).Click();
            Thread.Sleep(3000);
            driver.FindElement(btn1stTier).Click();
            var element1 = driver.FindElement(By.XPath("//lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[4]/span[2]/span"));
            Actions action1 = new Actions(driver);
            action1.MoveToElement(element1);
            action1.Perform();
            driver.FindElement(val1stRowTier).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnSaveCounterparty).Click();
            driver.Navigate().Refresh();
            //WebDriverWaits.WaitUntilEleVisible(driver, valType1, 90);
            //string value = driver.FindElement(valType1).Text;
            //return value;
        }

        //Get Tier of 2nd Row
        public string GetValueOf2ndRowTier()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTier2, 90);
            string value = driver.FindElement(valTier2).Text;
            return value;
        }
        //Get Type of 2nd Row
        public string GetValueOf2ndRowType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valType2, 90);
            string value = driver.FindElement(valType2).Text;
            return value;
        }

        //Validate Total KPI's count
        public string GetNumberOf1stKPI()
        {
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, val1stKPINo, 90);
            string value = driver.FindElement(val1stKPINo).Text;
            return value;
        }

        //Validate Total KPI
        public string GetTextOf1stKPI()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, val1stKPIText, 90);
            string value = driver.FindElement(val1stKPIText).Text;
            return value;
        }
        //Validate Initial Contact KPI's count
        public string GetNumberOf2ndKPI()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, val2ndKPINo, 90);
            string value = driver.FindElement(val2ndKPINo).Text;
            return value;
        }

        //Validate Initial Contact KPI
        public string GetTextOf2ndKPI()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, val2ndKPIText, 90);
            string value = driver.FindElement(val2ndKPIText).Text;
            return value;
        }
        //Validate Sent Teaser KPI's count
        public string GetNumberOf3rdKPI()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, val3rdKPINo, 90);
            string value = driver.FindElement(val3rdKPINo).Text;
            return value;
        }

        //Validate Sent Teaser KPI
        public string GetTextOf3rdKPI()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, val3rdKPIText, 90);
            string value = driver.FindElement(val3rdKPIText).Text;
            return value;
        }

        //Validate Sent Teaser KPI functionality
        public string ValidateKPIFunctionality()
        {
            driver.FindElement(val3rdKPINo).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgDeleteFinal, 150);
            string message = driver.FindElement(msgDeleteFinal).Text;
            return message;
        }

    }

}
