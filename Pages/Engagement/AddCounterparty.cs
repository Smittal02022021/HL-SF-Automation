using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
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
        By btnBack = By.Id("back_btn");
        By lnkDetails = By.CssSelector(".view_record__c > a");
        By valFirstName = By.XPath("//table/tbody/tr[2]/td[2]/a");
        By valLastName = By.XPath("//table/tbody/tr[2]/td[3]/a");
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
        By lnkContacts = By.XPath("//div[text()='Contacts']");
        By valAddedContact = By.XPath("//slot/lightning-layout/slot/lightning-layout-item/slot/div/table/tbody/tr/td[1]/div/div[2]/div[3]/div/section/div/p[1]");
        By btnView = By.XPath("//button[@data-value='Buyside Stages']");
        By btnViewSellside = By.XPath("//button[@data-value='Sellside Stages']");
        By valUpdView = By.XPath("//lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[1]/span[2]/span");
        By selectedView = By.XPath("//lightning-combobox/div/lightning-base-combobox/div/div[1]/button/span");
        By valBuysideView = By.XPath("//lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[4]/span[2]/span");
        By tblCounterparty = By.XPath("//c-counter-party-edit/div/div/lightning-layout/slot/lightning-layout-item[3]/slot/div/div");
        By chkCounterparty = By.XPath("//lightning-layout/slot/lightning-layout-item[2]/slot/lightning-layout/slot/lightning-layout-item/slot/div/table/tbody/tr/td[1]/div/div[1]/lightning-input/div/span/label/span[1]");
        By btnEmail = By.XPath("//button[text()='Email']");
        By titleConfirmEmails = By.XPath("//h2[text()='Confirm emails']");
        By lblMilestone = By.XPath("//label[text()='Milestone']");
        By btnMilestone = By.XPath("//c-counter-party-edit/section/div/div/lightning-combobox/div/lightning-base-combobox/div/div[1]/button");
        By valMilestone = By.XPath("//c-counter-party-edit/section/div/div/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span");
        By lblTemplate = By.XPath("//label[text()='Template']");
        By btnTemplate = By.XPath("//c-counter-party-edit/section/div/div/div[1]/lightning-combobox/div/lightning-base-combobox/div/div[1]/button");
        By valTemplate = By.XPath("//c-counter-party-edit/section/div/div/div[1]/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span");
        By btnCancelConfirm = By.XPath("//c-counter-party-edit/section/div/footer/lightning-button[2]/button");
        By btnSaveCounterparty = By.XPath("//c-counter-party-edit/div/div/lightning-layout/slot/lightning-layout-item[1]/slot/div/div[4]/lightning-button-group/slot/lightning-button[1]/button");
        By btnCancelCounterparty = By.XPath("//c-counter-party-edit/div/div/lightning-layout/slot/lightning-layout-item[1]/slot/div/div[4]/lightning-button-group/slot/lightning-button[3]/button");
        By btnDeleteCounterparty = By.XPath("//c-counter-party-edit/div/div/lightning-layout/slot/lightning-layout-item[1]/slot/div/div[4]/lightning-button-group/slot/lightning-button[2]/button");
        By btnAddCounterpartiesL = By.XPath("//c-counter-party-edit/div/div/lightning-layout/slot/lightning-layout-item[1]/slot/div/div[4]/lightning-button-group/slot/lightning-button[4]/button");
        By btnEmailCounterparty = By.XPath("//c-counter-party-edit/div/div/lightning-layout/slot/lightning-layout-item[1]/slot/div/div[4]/lightning-button-group/slot/lightning-button[8]/button");
        By btnViewAllCounterparty = By.XPath("//c-counter-party-edit/div/div/lightning-layout/slot/lightning-layout-item[1]/slot/div/div[4]/lightning-button-group/slot/lightning-button[9]/button");
        By valExistingComp = By.XPath("//table/tbody/tr/td[1]/div/div[2]/div[1]");
        By btnAddCounterparty = By.XPath("//button[text()='Add Counterparties']");
        By lblCounterparties = By.XPath("//header/div[1]/h2/span[text()='Counterparties']");
        By lblExistingEngagement = By.XPath("//span[@title='Get Companies from existing Engagement']");
        By txtSearchEng = By.XPath("//input[@placeholder='Search Engagement here...']");
        By lblExistingCompanyList = By.XPath("//span[@title='Get Companies from existing Company List']");
        By txtSearchCompanyList = By.XPath("//input[@placeholder='Search Company List here...']");
        By btnBackCounterparties = By.XPath("//button[text()='Back']");
        By lblView = By.XPath("//label[text()='View']");
        By txtCompanyList = By.XPath("//label[text()='Company List']/following::input[1]");
        By btnViewAllCompList = By.XPath("//button[text()='View All Company List']");
        By titleCompanyList = By.XPath("//h2[text()='Company List']");
        By radioCompName = By.XPath("//table/tbody/tr[1]/td[1]/div/input");
        By btnOK = By.XPath("//button[@title='OK']");
        By chkCompany = By.XPath("//table/tbody/tr[1]/td[1]/lightning-primitive-cell-checkbox/span/label/span[1]");
        By btnAddCounterpartyTo = By.XPath("//button[text()='Add Counterparty to Project Astro']");
        By tblCompanies = By.XPath("//table/tbody/tr[2]/td[1]/div/div[2]/div[1]");
        By comboType = By.XPath("//button[@name='Type__c']/span");
        By comboTier = By.XPath("//button[@name='Tier__c']/span");
        By valSelectType = By.XPath("//lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span");
        By valSelectTier = By.XPath("//lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span");
        By chkCounterCompany = By.XPath("//tr/td[1]/div/div[1]/lightning-input/div/span/label/span[1]");
        By chk2ndCounterCompany = By.XPath("//tr[2]/td[1]/div/div[1]/lightning-input/div/span/label/span[1]");
        By combo2ndType = By.XPath("//tr[2]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[1]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[1]/button/span");
        By combo2ndTier = By.XPath("//tr[2]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[1]/button");
        By valSelect2ndType = By.XPath("//tr[2]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[1]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[3]/span[2]/span");
        By valSelect2ndTier = By.XPath("//tr[2]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[3]/span[2]/span");
        By msgSelectRecord = By.XPath("//span[text()='Please select at least one row to delete.']");

        By msgDeleteRecord = By.XPath("//div[text()='Are you sure you want to delete the selected rows?']");
        By btnDeleteConfirm = By.XPath("//c-counter-party-edit/section/div/footer/lightning-button[2]/button");
        By valCounterpartyName = By.XPath("//section[3]/div/div[2]/div[1]/div[1]/div/div/div/div/div[2]/div/div[1]/div[2]/div[2]/div[1]/div/div/table/tbody/tr/th/span/a");
        By lblCounterpartyName = By.XPath("//span[@title='Counterparty Name']");
        By lblStatus = By.XPath("//span[@title='Status']");
        By lblDateOfLast = By.XPath("//span[@title='Date of Last Status Change']");
        By btnNew = By.XPath("//div[@title='New']");
        By lnkShowMore = By.XPath("//table/tbody/tr/td[4]/span/div/a/span/span[1]");
        By lnkEdit = By.XPath("//a[@data-target-selection-name='sfdc:StandardButton.Engagement_Counterparty__c.Edit']");
        By lnkDelete = By.XPath("//a[@data-target-selection-name='sfdc:StandardButton.Engagement_Counterparty__c.Delete']");
        By txtSearchBox = By.XPath("//lightning-layout-item[1]/slot/p[1]/lightning-input/div/input");
        By btnSearchContact = By.XPath("//button[@title='Search']");
        By btnEditBids = By.XPath("//button[text()='Edit Bids']");
        By btnNewBidRound = By.XPath("//button[text()='New Bid Round']");
        By btnSelectNewRound = By.XPath("//button[@aria-label='Select New Round, Select New Round']");
        By valSelectNewRound = By.XPath("//lightning-base-combobox-item[@data-value='First']/span[2]/span");
        By lblCompName = By.XPath("//span[@title='Company Name']");
        By lblMinBid = By.XPath("//span[@title='Min Bid']");
        By lblMaxBid = By.XPath("//span[@title='Max Bid']");
        By lblEquity = By.XPath("//span[@title='Equity %']");
        By lblDebt = By.XPath("//span[@title='Debt %']");
        By lblBidDate = By.XPath("//span[@title='Bid Date']");
        By lblComments = By.XPath("//tr/th[8]/lightning-primitive-header-factory/div/span/span");
        By lnkEquity = By.XPath("//tr/td[4]/lightning-primitive-cell-factory/span/button");
        By lnkMinBid = By.XPath("//tr/td[2]/lightning-primitive-cell-factory/span/button");
        By lnkMaxBid = By.XPath("//tr/td[3]/lightning-primitive-cell-factory/span/button");
        By lnkDebt = By.XPath("//tr/td[5]/lightning-primitive-cell-factory/span/button");
        By lnkBidDate = By.XPath("//tr/td[6]/lightning-primitive-cell-factory/span/button");
        By lnkComments = By.XPath("//tr/td[7]/lightning-primitive-cell-factory/span/button");
        By txtEquity = By.XPath("//input[@name='dt-inline-edit-number']");
        By txtMinBid = By.XPath("//input[@name='dt-inline-edit-currency']");
        By txtBidDate = By.XPath("//input[@name='dt-inline-edit-dateLocal']");
        By txtComments = By.XPath("//input[@name='dt-inline-edit-text']");
        By btnSaveBid = By.XPath("//lightning-primitive-datatable-status-bar/div/div/button[2]");
        By btnAddCounterpartyL = By.XPath("//button[text()='Add Counterparty']");
        By valAddedCompany = By.XPath("//records-record-layout-row[2]/slot/records-record-layout-item[1]/div/div/div[2]/span/slot[1]/force-lookup/div/records-hoverable-link/div/a/slot/slot/span");
        By chkMassCheckbox = By.XPath("//div[1]/div/div/lightning-input/div/span/label/span[1]");
        By btn1stType = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[1]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[1]/button");
        By btn1stTier = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[1]/button");
        By btn2ndType = By.XPath("//tr[2]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[1]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[1]/button");
        By btn2ndTier = By.XPath("//tr[2]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[1]/button");
        By txtDeclined = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[3]/slot/div/lightning-input-field/lightning-input/lightning-datepicker/div[1]/div/input");
        By txtInitialContact = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[4]/slot/div/lightning-input-field/lightning-input/lightning-datepicker/div[1]/div/input");
        By txtSentTeaser = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[5]/slot/div/lightning-input-field/lightning-input/lightning-datepicker/div[1]/div/input");
        By txtMarkupSent = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[6]/slot/div/lightning-input-field/lightning-input/lightning-datepicker/div[1]/div/input");
        By txtMarkupReceived = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[7]/slot/div/lightning-input-field/lightning-input/lightning-datepicker/div[1]/div/input");
        By txtExecutedCA = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[8]/slot/div/lightning-input-field/lightning-input/lightning-datepicker/div[1]/div/input");
        By txtReceivedBook = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[9]/slot/div/lightning-input-field/lightning-input/lightning-datepicker/div[1]/div/input");
        By txtProposal = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[10]/slot/div/lightning-input-field/lightning-input/lightning-datepicker/div[1]/div/input");
        By txtMetWith = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[11]/slot/div/lightning-input-field/lightning-input/lightning-datepicker/div[1]/div/input");
        By txtLetter = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[12]/slot/div/lightning-input-field/lightning-input/lightning-datepicker/div[1]/div/input");
        By txtCommentsL = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[13]/slot/div/lightning-input-field/lightning-textarea/div/textarea");
        By btnSaveMass = By.XPath("//slot/div/div[2]/lightning-button/button");
        By valType1 = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[1]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[1]/button/span");
        By valTier1 = By.XPath("//tr[1]/td[2]/lightning-record-edit-form/lightning-record-edit-form-edit/form/slot/slot/lightning-layout/slot/lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[1]/button/span");

        //To Click Counterparties button
        public string ClickAddCounterpartiesbutton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterparties, 60);
            driver.FindElement(btnAddCounterparties).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titlePage, 60);
            string title = driver.FindElement(titlePage).Text;
            return title;
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
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
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchBox, 180);
            driver.FindElement(txtSearchBox).SendKeys("Jaffery Malik");
            driver.FindElement(btnSearchContact).Click();
        }



        //Add Contact 
        public string AddContact()
        {
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkContact, 180);
            driver.FindElement(chkContact).Click();
            string name = driver.FindElement(valContact).Text;
            Thread.Sleep(4000);
            driver.FindElement(btnAddContact).Click();
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabCounterpartyEditor, 80);
            driver.FindElement(tabCounterpartyEditor).Click();
            return name;
        }

        //Validate added Contact 
        public string ValidateAddedContact()
        {
            Thread.Sleep(15000);
            var element = driver.FindElement(lnkContacts);
            Actions action = new Actions(driver);
            action.MoveToElement(element);
            action.Perform();
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedContact, 80);
            string name = driver.FindElement(valAddedContact).Text;
            return name.Substring(0, 13);
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

        //Set the default view
        public string RevertDefaultView()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewSellside, 180);
            driver.FindElement(btnViewSellside).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valBuysideView, 90);
            driver.FindElement(valBuysideView).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, selectedView, 80);
            string name = driver.FindElement(selectedView).Text;
            return name;
        }

        //Validate the displayed records
        public string ValidateCounterpartyRecords()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tblCounterparty, 180);
            string value = driver.FindElement(tblCounterparty).Displayed.ToString();
            Console.WriteLine(value);
            if (value.Equals("True"))
            {
                string message = driver.FindElement(tblCounterparty).Text;
                return message;
            }
            else
            {
                return "Counterparty records are displayed";
            }
        }


        //Select the counterparty and click the email button
        public string SelectCounterpartyAndClickEmailButton()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkCounterparty, 180);
            driver.FindElement(chkCounterparty).Click();
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
            driver.FindElement(btnCancelConfirm).Click();
            return isSame;
        }

        //Validate Save button
        public string ValidateSaveButton()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveCounterparty, 150);
            string value = driver.FindElement(btnSaveCounterparty).Text;
            return value;
        }

        //Validate Delete button
        public string ValidateDeleteButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCounterparty, 150);
            string value = driver.FindElement(btnDeleteCounterparty).Text;
            return value;
        }

        //Validate Cancel button
        public string ValidateCancelButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelCounterparty, 150);
            string value = driver.FindElement(btnCancelCounterparty).Text;
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

        //Get the value of existing company
        public string GetExistingCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valExistingComp, 150);
            string value = driver.FindElement(valExistingComp).Text;
            return value;
        }

        //Get the default value of Type dropdown
        public string GetDefaultValueOfType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboType, 150);
            string value = driver.FindElement(comboType).Text;
            return value;
        }

        //Get the default value of Tier dropdown
        public string GetDefaultValueOfTier()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboTier, 150);
            string value = driver.FindElement(comboTier).Text;
            return value;
        }

        //Click View all button and validate the counterparties
        public string ClickViewAllAndValidateCounterpartiesName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewAllCounterparty, 150);
            driver.FindElement(btnViewAllCounterparty).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, valCounterpartyName, 150);
            string value = driver.FindElement(valCounterpartyName).Text;
            return value;

        }

        //Validate Counterparty Name Column
        public string ValidateCounterpartyNameColumn()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCounterpartyName, 150);
            string value = driver.FindElement(lblCounterpartyName).Text;
            return value;
        }

        //Validate Status Column
        public string ValidateStatusColumn()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblStatus, 150);
            string value = driver.FindElement(lblStatus).Text;
            return value;
        }

        //Validate Date of Last Status Change Column
        public string ValidateDateOfLastStatusChangeColumn()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblDateOfLast, 150);
            string value = driver.FindElement(lblDateOfLast).Text;
            return value;
        }

        //Validate New button
        public string ValidateNewButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNew, 150);
            string value = driver.FindElement(btnNew).Text;
            return value;
        }

        //Validate Edit option
        public string ValidateEditOption()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkShowMore, 180);
            driver.FindElement(lnkShowMore).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(lnkEdit).Text;
            return value;
        }

        //Validate Delete option
        public string ValidateDeleteOption()
        {
            string value = driver.FindElement(lnkDelete).Text;
            driver.FindElement(tabCounterpartyEditor).Click();
            return value;
        }

        //Update the value of Type
        public string SelectTypeTierAndClickCancel()
        {
            driver.FindElement(comboType).Click();
            var element = driver.FindElement(By.XPath("//lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span"));
            Actions action = new Actions(driver);
            action.MoveToElement(element);
            action.Perform();
            driver.FindElement(valSelectType).Click();
            Thread.Sleep(3000);
            driver.FindElement(comboTier).Click();
            var element1 = driver.FindElement(By.XPath("//lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span"));
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

        //Update the value of Type and Tier and click Save
        public string UpdateTypeTierAndClickSave()
        {
            driver.FindElement(comboType).Click();
            var element = driver.FindElement(By.XPath("//lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span"));
            Actions action = new Actions(driver);
            action.MoveToElement(element);
            action.Perform();
            driver.FindElement(valSelectType).Click();
            Thread.Sleep(3000);
            driver.FindElement(comboTier).Click();
            var element1 = driver.FindElement(By.XPath("//lightning-layout-item[2]/slot/div/lightning-input-field/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span"));
            Actions action1 = new Actions(driver);
            action1.MoveToElement(element1);
            action1.Perform();
            driver.FindElement(valSelectTier).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnSaveCounterparty).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboType, 150);
            string value = driver.FindElement(comboType).Text;
            return value;
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

        //Select any record and then click Delete button
        public string SelectAnyRecordAndClickDelete()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chk2ndCounterCompany, 150);
            driver.FindElement(chk2ndCounterCompany).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCounterparty, 150);
            driver.FindElement(btnDeleteCounterparty).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgDeleteRecord, 250);
            string text = driver.FindElement(msgDeleteRecord).Text;
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteConfirm, 250);
            driver.FindElement(btnDeleteConfirm).Click();
            return text;
        }
        //Search for a company and validate Company List
        public string EnterCompanyAndValidateThePage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyList, 150);
            driver.FindElement(txtCompanyList).SendKeys("etsy");
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewAllCompList, 150);
            driver.FindElement(btnViewAllCompList).Click();
            Thread.Sleep(4000);
            string title = driver.FindElement(titleCompanyList).Text;
            return title;
        }

        //Select the company
        public string SelectAndAddCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, radioCompName, 150);
            driver.FindElement(radioCompName).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 150);
            driver.FindElement(btnOK).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkCompany, 150);
            driver.FindElement(chkCompany).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterpartyTo, 150);
            driver.FindElement(btnAddCounterpartyTo).Click();
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackCounterparties, 150);
            driver.FindElement(btnBackCounterparties).Click();
            Thread.Sleep(7000);
            string name = driver.FindElement(tblCompanies).Text;
            return name;
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
            catch (Exception e)
            {
                return "2nd company does not exist";
            }
        }

        //Click Edit Bids button
        public void ClickEditBidsButton()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditBids, 250);
            driver.FindElement(btnEditBids).Click();
        }

        //Click New Bid Round button
        public void ClickNewBidRoundAndSelectFirstRound()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewBidRound, 150);
            driver.FindElement(btnNewBidRound).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSelectNewRound, 250);
            driver.FindElement(btnSelectNewRound).Click();
            driver.FindElement(valSelectNewRound).Click();
        }

        //Validate Company Name Column
        public string ValidateCompanyName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCompName, 150);
            string name = driver.FindElement(lblCompName).Text;
            return name;
        }

        //Validate Min Bid Column
        public string ValidateMinBid()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblMinBid, 150);
            string name = driver.FindElement(lblMinBid).Text;
            return name;
        }

        //Validate Max Bid Column
        public string ValidateMaxBid()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblMaxBid, 150);
            string name = driver.FindElement(lblMaxBid).Text;
            return name;
        }

        //Validate Equity Column
        public string ValidateEquity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblEquity, 150);
            string name = driver.FindElement(lblEquity).Text;
            return name;
        }

        //Validate Debt Column
        public string ValidateDebt()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblDebt, 150);
            string name = driver.FindElement(lblDebt).Text;
            return name;
        }

        //Validate Bid Date Column
        public string ValidateBidDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblBidDate, 150);
            string name = driver.FindElement(lblBidDate).Text;
            return name;
        }

        //Validate Comments Column
        public string ValidateComments()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblComments, 150);
            string name = driver.FindElement(lblComments).Text;
            return name;
        }

        //Enter the details of the columns and save it
        public void SaveAllDetailsOfBid()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtMinBid, 250);
            driver.FindElement(txtMinBid).SendKeys("10");
            driver.FindElement(lblMinBid).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkMaxBid, 250);
            driver.FindElement(lnkMaxBid).Click();
            driver.FindElement(txtMinBid).SendKeys("10");
            driver.FindElement(lblMaxBid).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEquity, 250);
            driver.FindElement(lnkEquity).Click();
            driver.FindElement(txtEquity).SendKeys("10");
            driver.FindElement(lblEquity).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDebt, 250);
            driver.FindElement(lnkDebt).Click();
            driver.FindElement(txtEquity).SendKeys("10");
            driver.FindElement(lblEquity).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkBidDate, 250);
            driver.FindElement(lnkBidDate).Click();
            driver.FindElement(txtBidDate).SendKeys("30-Nov-2022");
            driver.FindElement(lblEquity).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkComments, 250);
            driver.FindElement(lnkComments).Click();
            driver.FindElement(txtComments).SendKeys("Testing");
            driver.FindElement(lblEquity).Click();
            driver.FindElement(btnSaveBid).Click();
        }

        //Check Mass checkbox
        public void ClickMassCheckbox()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkMassCheckbox, 250);
            driver.FindElement(chkMassCheckbox).Click();
        }

        //Enter all the values for available columns and click Cancel button
        public string ValidateCancelFunctonalityAfterEnteringValuesForAllColumns(string date)
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
            Thread.Sleep(3000);
            driver.FindElement(txtDeclined).SendKeys(date);
            driver.FindElement(txtInitialContact).SendKeys(date);
            driver.FindElement(txtSentTeaser).SendKeys(date);
            driver.FindElement(txtMarkupSent).SendKeys(date);
            driver.FindElement(txtMarkupReceived).SendKeys(date);
            driver.FindElement(txtExecutedCA).SendKeys(date);
            driver.FindElement(txtReceivedBook).SendKeys(date);
            driver.FindElement(txtProposal).SendKeys(date);
            Thread.Sleep(3000);
            driver.FindElement(txtMetWith).SendKeys(date);
            driver.FindElement(txtLetter).SendKeys(date);
            driver.FindElement(txtCommentsL).SendKeys(date);
            driver.FindElement(btnCancelCounterparty).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valType1, 90);
            string value = driver.FindElement(valType1).Text;
            return value;
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
            Thread.Sleep(3000);
            driver.FindElement(txtDeclined).SendKeys(date);
            driver.FindElement(txtInitialContact).SendKeys(date);
            driver.FindElement(txtSentTeaser).SendKeys(date);
            driver.FindElement(txtMarkupSent).SendKeys(date);
            driver.FindElement(txtMarkupReceived).SendKeys(date);
            driver.FindElement(txtExecutedCA).SendKeys(date);
            driver.FindElement(txtReceivedBook).SendKeys(date);
            driver.FindElement(txtProposal).SendKeys(date);
            Thread.Sleep(3000);
            driver.FindElement(txtMetWith).SendKeys(date);
            driver.FindElement(txtLetter).SendKeys(date);
            driver.FindElement(txtCommentsL).SendKeys(date);
            driver.FindElement(btnSaveCounterparty).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valType1, 90);
            string value = driver.FindElement(valType1).Text;
            return value;
        }

        //Enter all the values for available columns and click Save button
        public void ValidateDeleteFunctonalityAfterEnteringValuesForAllColumns()
        {
            Thread.Sleep(5000);
            driver.FindElement(btnDeleteCounterparty).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteConfirm, 90);
            driver.FindElement(btnDeleteConfirm).Click();
        }
    }
}
