using AventStack.ExtentReports.Reporter.Configuration;
using OpenQA.Selenium;
using RazorEngine.Compilation.ImpromptuInterface;
using RazorEngine.Compilation.ImpromptuInterface.Optimization;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SalesForce_Project.Pages
{
    class ParentProject : BaseClass
    {

        By btnNew = By.XPath("//div[@title='New']");
        By titleProject = By.XPath("//h2[text()='New Project Billing']");
        By msgProject = By.XPath("//flexipage-field//div[contains(text(),'Complete')]");
        By btnSave = By.XPath("//button[@name='SaveEdit']");
        By btnClose = By.XPath("//records-record-edit-error-header//button/lightning-primitive-icon");
        By txtName = By.XPath("//input[@name='Name']");
        By txtBillTo = By.XPath("//input[@placeholder='Search Companies...']");
        By txtBillToContact = By.XPath("//input[@placeholder='Search Contacts...']");
        By btnInvoice = By.XPath("//button[@aria-label='Invoice Type']");

        By valAddedProject = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Parent_Project__c.Name']//span//lightning-formatted-text");
        By btnEditParentProject = By.XPath("//flexipage-tab2[1]/slot/flexipage-component2[1]//flexipage-column2[2]/div/slot//div/button[@title='Edit Parent Project']");
        By txtParentProject = By.XPath("//input[@placeholder='Search Project Billings...']");
        By btnClearParentProject = By.XPath("//label[text()='Parent Project']/ancestor::lightning-grouped-combobox//button[@title='Clear Selection']");
        By valParentProjectEng = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.Parent_Project__c']//records-hoverable-link//span//span/slot");
        By lnkParentProjectEng = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.Parent_Project__c']//records-hoverable-link");
        By tabParentProject = By.XPath("//div[2]/div/div/ul[2]/li[2]/a/span[1]");
        By recParentProjcet = By.XPath("//tbody/tr[1]/th//a");
        By valAssociatedEng = By.XPath("//span[text()='Engagement Name']/ancestor::table//tr[1]/th//lst-output-lookup//a//slot//slot/span");
        By valProjectClient = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Parent_Project__c.Client__c']//lightning-formatted-text");
        By valProjectLOB = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Parent_Project__c.Line_of_Business__c']//lightning-formatted-text");
        By valProjectCurrency = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Parent_Project__c.CurrencyIsoCode']//lightning-formatted-text");
        By valLegalEntity = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Parent_Project__c.ERP_Legal_Entity__c']//lightning-formatted-text");
        By valParentProgContract = By.XPath("//th[@data-label='Contract Name']//records-hoverable-link//span//span/slot");
        By valContractNumber = By.XPath("//td[@data-label='Contract Number']//lst-formatted-text/span");
        By lnkContract = By.XPath("//th[@data-label='Contract Name']//records-hoverable-link");
        By valTotalFee = By.XPath("//span[text()='Total Fee']/ancestor::div[2]//lightning-formatted-text");
        By valFundingAmount = By.XPath("//span[text()='Funding Amount']/ancestor::div[2]/div[2]//lightning-formatted-number");
        By txtSearchProject = By.XPath("//input[@placeholder='Search this list...']");
        By btnRefresh = By.XPath("//button[@name='refreshButton']");
        By valSearchedProjectName = By.XPath("//table/tbody/tr[1]/th/span//a");
        By lnkEng1 = By.XPath("//span[@title='Engagement Name']/ancestor::table//tbody/tr[1]/th[1]//records-hoverable-link");
        By lnkEng2 = By.XPath("//span[@title='Engagement Name']/ancestor::table//tbody/tr[2]/th[1]//records-hoverable-link");
        By tabFees = By.XPath("//a[text()='Fees & Financials']");
        By valExpEng1 = By.XPath("//span[text()='Expense Cap']/ancestor::div[2]/div[2]//lightning-formatted-text");
        By btnCloseEng1 = By.XPath("//button[contains(@title,'Close O')]");

        By tabRelated = By.XPath("//a[text()='Related']");
        By lblRelatedSections = By.XPath("//h2[@class='header-title-container']/span");
        By lblBillingRequest = By.XPath("//span[@title='Billing Requests']");
        By tabParentProj = By.XPath("//span[@title='Parent Project  c']");
        By btnEditParentProj = By.XPath("//records-entity-label[text()='Project Billing']/ancestor::records-highlights2//div[@class='slds-grid primaryFieldRow']/div[3]//button[text()='Edit']");
        By txtEditParentProjName = By.XPath("//input[@name='Name']");
        By valUpdatedParentProjName = By.XPath("//records-entity-label[text()='Project Billing']/ancestor::records-highlights2//h1/slot/lightning-formatted-text");
        By btnDeleteParentProj = By.XPath("//records-entity-label[text()='Project Billing']/ancestor::records-highlights2//div[@class='slds-grid primaryFieldRow']/div[3]//button[text()='Delete']");
        By tabProject = By.XPath("//a[@title='Project Billings Tab']");
        By lnkProjName = By.XPath("//div[2]//td[2]/div[3]//tr[2]/th/a");
        By btnDeleteProjAdmin = By.XPath("//div[4]/div[1]//input[@title='Delete']");
        By lnkBillingReq = By.XPath("//slot[contains(text(),'Billing Requests')]/ancestor::a");
        By titleBillingReq = By.XPath("//h1[contains(text(),'Billing ')]");
        By btnCloseBillingReq = By.XPath("//button[@title='Close Billing Requests']");
        By btnNewBillingReq = By.XPath("//li[@data-target-selection-name='sfdc:StandardButton.Billing_Request__c.New']//button[@name='New']");
        By msgBillingReq = By.XPath("//div[text()='Complete this field.']");
        By msgBillingEvent = By.XPath("//div[contains(text(),'Complete ')]");
        By chkAccInvoice = By.XPath("//span[text()='Accounting Send Final Invoice']/ancestor::lightning-primitive-input-checkbox/div/span/input");
        By btnPreference1 = By.XPath("//button[@aria-label='Client Out-of-Pocket Charges Preference']");
        By btnPreference2 = By.XPath("//button[@aria-label='Client Fees Charges Preference']");
        By txtAccounting = By.XPath("//input[@placeholder='Search Contacts...']");
        By chkInvoice = By.XPath("//input[@name='Accounting_Send_Final_Invoice__c']");
        By msgPrincipal = By.XPath("//li[text()='Please select either \"Accounting Send Final Invoice\" or \"Principal/Manager\".']");
        By valBillingRequest = By.XPath("//records-entity-label[text()='Billing Request']/ancestor::h1/slot/lightning-formatted-text");
        By valBillingRequest2nd = By.XPath("//section[4]//flexipage-component2[1]//div[2]/h1//lightning-formatted-text");
        By lblHeaderRow = By.XPath("//slot[@class='slds-grid slds-page-header__detail-row']//p[1]");
        By secBillingReq = By.XPath("//span[@title='Approval History']/ancestor::div[5]//flexipage-component2//article//h2/a/span[1]");
        By btnEditbillingReq = By.XPath("//records-entity-label[text()='Billing Request']/ancestor::records-highlights2//div[@class='slds-grid primaryFieldRow']/div[3]//button[text()='Edit']");
        By txtEditBRComments = By.XPath("//label[text()='Comments']/ancestor::lightning-textarea//textarea");
        By btnEditBillingEvent = By.XPath("//records-entity-label[text()='ERP Revenue Billing Event']/ancestor::records-highlights2//div[@class='slds-grid primaryFieldRow']/div[3]//button[text()='Edit']");
        By txtComments = By.XPath("//textarea");
        By valComments = By.XPath("//span[text()='Comments']/ancestor::div[2]//span/slot/lightning-formatted-text");
        By valStatusBillingReq = By.XPath("//span[text()='Status']/ancestor::div[2]//span/slot/lightning-formatted-text");
        By subtabParentProj = By.XPath("//div[@class='tabBarContainer']//span[@title='Parent Project  c']");
        By btnNewFile = By.XPath("//section[4]//flexipage-component2[4]//div[3]//li//button");
        By txtEngagement = By.XPath("//input[@placeholder='Search Engagements...']");
        By btnFeeType = By.XPath("//button[@aria-label='Fee Type']");
        By txtFeeDescription = By.XPath("//textarea");
        By txtFeeAmount = By.XPath("//input[@name='Fee_Amount__c']");
        By valFeeType = By.XPath("//span[text()='Fee Type']/ancestor::div[2]//tr[1]/td[6]//lst-formatted-text/span");
        By subTabBillingReq = By.XPath("//li[5]//span[@title='Billing Request  c']");
        By subTabFeeToBill = By.XPath("//li[6]//span[@title='Fee To Bill  c']");
        By valFeeTypeBillingReq = By.XPath("//span[@title='Fees To Bill']/ancestor::article//table/tbody/tr[2]/td[6]//span//span");
        By valFeeTypeBillingReq2ndEng = By.XPath("//span[@title='Fees To Bill']/ancestor::article//table/tbody/tr[6]/td[6]//span//span");

        By valFeeAmtBillingReq = By.XPath("//span[@title='Fees To Bill']/ancestor::article//table/tbody//tr[7]/td[8]//span//span");
        // By lnkViewAll = By.XPath("//span[@title='Fees To Bill']/ancestor::article//table/tbody//td[6]//span//span/ancestor::lst-related-list-view-manager/a");
        //By btnAction = By.XPath("//table//td[10]//lightning-button-menu/button");
        By btnEditFee = By.XPath("//records-entity-label[text()='Fee To Bill']/ancestor::div[@class='slds-grid primaryFieldRow']//button");
        By valExpenseType = By.XPath("//span[@title='Expenses To Bill']/ancestor::article//table/tbody//tr[1]/td[5]//span//span");
        By btn1stBillingReq = By.XPath("//span[@title='Billing Request  c']");
        By valTotalExpense = By.XPath("//p[text()='Total Expense Amount']/ancestor::div[1]/p[2]//lightning-formatted-text");
        By checkSelectExp = By.XPath("//table[@aria-label='Expenses To Bill']/tbody/tr/td[2]//span/label/span[1]");
        By btnUpdateToBill = By.XPath("//button[text()='Update To Bill']");
        By checkToBill = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Expenses_To_Bill__c.To_Bill__c']//input");
        By btnSaveUpdateToBill = By.XPath("//footer/button[2]");
        By btnClose2ndBillingReq = By.XPath("//ul[2]/li[3]/div[2]/button");
        By btnClose1stBillingReq = By.XPath("//ul[2]/li[4]/div[2]/button");
        By btnAddExpToBill = By.XPath("//button[@name='Billing_Request__c.Add_Expenses_To_Bill']");
        By btnSelectEng = By.XPath("//button[@aria-label='Select Engagement']");
        By btnSelectPV = By.XPath("//button[@aria-label='Select PV']");
        By lnkExpName = By.XPath("//table[@aria-label='Expenses To Bill']/tbody/tr[1]/th//records-hoverable-link");
        By lnkPVPositionName = By.XPath("//table[@aria-label='PV Positions To Bill']/tbody/tr[1]/th//records-hoverable-link");
        By tabParentProjAdmin = By.XPath("//a[text()='Project Billings']");
        By lnkParentProjAdmin = By.XPath("//a[text()='Combo O’Connor Global']");
        By lnkBillingReqAdmin = By.XPath("//h3[text()='Billing Requests']//ancestor::div[2]//tr[2]/th/a");
        By lnkDelExpToBillAdmin = By.XPath("//h3[text()='Expenses To Bill']//ancestor::div[2]//tr[2]/td/a[contains(@title,'Delete - Record 1 ')]");
        By lnkDelPVToBillAdmin = By.XPath("//h3[text()='PV Positions To Bill']//ancestor::div[2]//tr[2]/td/a[contains(@title,'Delete - Record 1 ')]");
        By chkExpID = By.XPath("//tr[1]/td[1]/lightning-primitive-cell-checkbox//label/span[1]");
        By chkEXpID2 = By.XPath("//tr[2]/td[1]/lightning-primitive-cell-checkbox//label/span[1]");
        By btnSaveAddExp = By.XPath("//button[text()='Save']");
        By msgAddExp = By.XPath("//span[text()='records created successfully']");
        By btnAddPVPositions = By.XPath("//button[text()='Add PV Positions']");
        By valReportFeePV1 = By.XPath("//table[@aria-label='PV Positions To Bill']/tbody//tr[1]/td[8]//div//span");
        By valReportFeePV2 = By.XPath("//table[@aria-label='PV Positions To Bill']/tbody//tr[2]/td[8]//div//span");
        By checkSelectPV = By.XPath("//table[@aria-label='PV Positions To Bill']/tbody/tr/td[2]//span/label/span[1]");
        By btnUpateToBillPV = By.XPath("//li[@data-target-selection-name='sfdc:QuickAction.PV_Positions_To_Bill__c.Update_To_Bill']//button");
        By msgSuccess = By.XPath("//span[text()='Records updated Successfully']");
        By btnNewFee = By.XPath("//li[@data-target-selection-name='sfdc:StandardButton.Fee_To_Bill__c.New']//button[@name='New']");
        By valTotalFeeToBill = By.XPath("//p[text()='Total Fees To Bill']/ancestor::div[1]/p[2]//lightning-formatted-text");
        By tabAccounting = By.XPath("//a[text()='Accounting']");
        By tabDetails = By.XPath("//a[text()='Accounting']/ancestor::ul/li[1]/a");
        By btnMoreBillingEvent = By.XPath("//span[text()='ERP Revenue Billing Events']/ancestor::lst-common-list-internal//a[@role='button']");
        By valExpCap = By.XPath("//p[text()='Expense Cap']/ancestor::div[1]/p[2]//lightning-formatted-text");

        By secERPRevenue = By.XPath("//a/span[text()='ERP Revenue Billing Events']");
        By btnPVPositions = By.XPath("//records-entity-label[text()='PV Positions To Bill']/ancestor::div[@class='slds-grid primaryFieldRow']//button");
        By btnSubmitToBiller = By.XPath("//button[@name='Billing_Request__c.Submit_to_Biller']");
        By btnSubmit = By.XPath("//button[text()='Submit']");
        By valSubmitterEmail = By.XPath("//span[text()='Submitter Email']/ancestor::div[1]/following::lightning-formatted-email/a");
        By valAccDistriList = By.XPath("//span[text()='Accounting Distribution List']/ancestor::div[2]//records-hoverable-link//a/span//span/slot/span");
        By btnSharing = By.XPath("//button[text()='Sharing']");
        By txtSearchUser = By.XPath("//input[@title='Search User']");
        By btnAccessLevel = By.XPath("//button[@data-value='Read Only']");
        By btnShowBillingEvent = By.XPath("//span[text()='ERP Revenue Billing Events']/ancestor::div[4]/div[3]//button");
        By lnkNewBillingEvent = By.XPath("//span[text()='New Billing Event']");
        By titleBillingEvent = By.XPath("//h2[text()='New ERP Revenue Billing Event']");
        By txtContract = By.XPath("//input[@placeholder='Search Contract...']");
        By txtEventAmount = By.XPath("//input[@name='Event_Amount__c']");
        By btnEventType = By.XPath("//label[text()='Event Type']/ancestor::div[1]//button");
        By txtCompletionDate = By.XPath("//input[@name='Completion_Date__c']");
        By valBillingEvent = By.XPath("//dt[text()='Event Type:']/ancestor::article/div[1]//records-hoverable-link//a//slot[1]//slot/span");
        By valBillingEventAccounting = By.XPath("//dt[text()='Event Type:']/ancestor::div[2]/div[1]//records-hoverable-link//a/span//span[1]//span");
        By msgParentContract = By.XPath("//li[text()='Billing events cannot be created on parent contract.']");
        By btnClearSelection = By.XPath("//label[text()='Contract']/ancestor::lightning-grouped-combobox//button[@title='Clear Selection']");
        By msgICOContract = By.XPath("//div[text()='Billing Event cannot be created on ICO contracts']");
        By valTotalEventAmt = By.XPath("//dt[text()='Event Amount:']/ancestor::article/div[1]//records-hoverable-link//a//slot[1]//slot/span");
        By tabBillingReq = By.XPath("//ul[2]/li[4]");
        By lblStatus = By.XPath("//label[text()='Status']");
        By btnStatus = By.XPath("//label[text()='Status']/ancestor::div[1]//button");
        By msgTotalFee = By.XPath("//li[text()='Validation total fee to bill should equal to total event amount']");
        By btnClosePopUp = By.XPath("//button[@title='Cancel and close']");
        By tabBillingEvent = By.XPath("//ul[2]//li[5]");
        By valERPStatus = By.XPath("//dt[text()='ERP Last Integration Status:']/../dd[2]//lightning-formatted-number");
        By btnGenerateInvoice = By.XPath("//button[text()='Generate Invoice']");
        By btnResubmit = By.XPath("//button[text()='Resubmit']");
        By btnSaveInvoice = By.XPath("//button[@name='Save']");
        By msgInvoice = By.XPath("//h2[text()='Generate Invoice']/ancestor::lightning-quick-action-panel//lightning-messages/..//lightning-input//div[2]/span");



        public string ClickNewButton()
        {
            Thread.Sleep(5000);
            driver.FindElement(btnNew).Click();
            Thread.Sleep(4000);
            //driver.SwitchTo().Frame(0);
            WebDriverWaits.WaitUntilEleVisible(driver, titleProject);
            string title = driver.FindElement(titleProject).Text;
            return title;
        }
        public bool VerifyParentProjectMandatoryValdiations()
        {
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose);
            driver.FindElement(btnClose).Click();
            Thread.Sleep(7000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(msgProject);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);
            string[] expectedValue = { "Parent Project Name\r\nComplete this field.", "Invoice Type\r\nComplete this field.", "Bill To\r\nComplete this field.", "Bill To Contact\r\nComplete this field." };
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

        public string CreateNewParentProject(string value)
        {
            driver.FindElement(txtName).SendKeys(value);
            driver.FindElement(txtBillTo).Click();
            driver.FindElement(txtBillTo).SendKeys("Tec");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//lightning-grouped-combobox//lightning-base-combobox//div[2]/ul/li[2]")).Click();
            driver.FindElement(txtBillToContact).Click();
            driver.FindElement(txtBillToContact).SendKeys("Tec");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//label[text()='Bill To Contact']/ancestor::lightning-grouped-combobox//ul/li[2]/lightning-base-combobox-item")).Click();

            driver.FindElement(btnInvoice).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//label[text()='Invoice Type']/ancestor::div[1]/div[1]//div[2]/lightning-base-combobox-item[3]//span[2]/span")).Click();
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedProject);
            string project = driver.FindElement(valAddedProject).Text;
            return project;
        }

        //Associate Parent project to an Engagement
        public string AssociateParentProjectToEng(string name)
        {
            Thread.Sleep(5000);
            driver.FindElement(btnEditParentProject).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnClearParentProject);
            driver.FindElement(btnClearParentProject).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtParentProject, 100);
            driver.FindElement(txtParentProject).SendKeys(name);
            Thread.Sleep(7000);
            try
            {
                driver.FindElement(By.XPath("//div[1]//lightning-base-combobox/div//div[2]/ul/li/lightning-base-combobox-item")).Click();
                //driver.FindElement(By.XPath("//flexipage-field[12]/slot//div[1]/div/lightning-base-combobox//div[2]/ul/li")).Click();

            }
            catch
            {
                driver.FindElement(By.XPath("//flexipage-field[13]//div[1]//lightning-base-combobox/div//div[2]/ul/li[1]")).Click();

            }
            Thread.Sleep(7000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(8000);
            string project = driver.FindElement(valParentProjectEng).Text;
            return project;
        }

        public string GetParentContract()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valParentProgContract);
            string contract = driver.FindElement(valParentProgContract).Text;
            return contract;
        }

        public string GetParentContractNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContractNumber);
            string number = driver.FindElement(valContractNumber).Text;
            return number;
        }

        //Associate Parent project to an Engagement
        public string ValidateAssociatedEngToParentProject()
        {

            driver.FindElement(recParentProjcet).Click();
            //Sdriver.Navigate().Refresh();
            Thread.Sleep(8000);
            //WebDriverWaits.WaitUntilEleVisible(driver, valAssociatedEng);
            string eng = driver.FindElement(valAssociatedEng).Text;
            return eng;
        }

        //Associate Parent project to an Engagement
        public string ValidateAssociated2ndEngToParentProject()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,200)");
            Thread.Sleep(4000);
            driver.FindElement(lnkParentProjectEng).Click();
            //driver.Navigate().Refresh();
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, valAssociatedEng, 135);
            string eng = driver.FindElement(valAssociatedEng).Text;
            return eng;
        }

        public string GetContractTotalFee()
        {
            driver.FindElement(lnkContract).Click();
            //driver.Navigate().Refresh();
            Thread.Sleep(12000);
            string fee = driver.FindElement(valTotalFee).Text;
            return fee;

        }

        public string GetContractFundingAmount()
        {

            string fee = driver.FindElement(valFundingAmount).Text;
            return fee;

        }

        public string GetClientCompanyL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valProjectClient);
            string LOB = driver.FindElement(valProjectClient).Text;
            return LOB;
        }

        public string GetLOBL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valProjectLOB);
            string LOB = driver.FindElement(valProjectLOB).Text;
            return LOB;
        }

        public string GetCurrencyL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valProjectCurrency);
            string LOB = driver.FindElement(valProjectCurrency).Text;
            return LOB;
        }

        public string GetLegalEntityL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLegalEntity);
            string LOB = driver.FindElement(valLegalEntity).Text;
            return LOB;
        }

        //Validate if Search functionality is working as expected
        public string ValidateSearchFunctionalityOfParentProject(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchProject, 150);
            driver.FindElement(txtSearchProject).SendKeys(name);
            Thread.Sleep(5000);
            driver.FindElement(btnRefresh).Click();
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, valSearchedProjectName, 190);
            string project = driver.FindElement(valSearchedProjectName).Text;
            driver.FindElement(valSearchedProjectName).Click();
            return project;
        }

        public string GetExpenseCapOfEngagement1()
        {
            Thread.Sleep(4000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,650)");
            Thread.Sleep(6000);
            driver.FindElement(lnkEng1).Click();
            Thread.Sleep(5000);
            driver.FindElement(tabFees).Click();
            Thread.Sleep(4000);
            string project = driver.FindElement(valExpEng1).Text;
            driver.FindElement(btnCloseEng1).Click();
            return project;
        }

        public string GetExpenseCapOfEngagement2()
        {
            Thread.Sleep(4000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,650)");
            driver.FindElement(lnkEng2).Click();
            Thread.Sleep(5000);
            driver.FindElement(tabFees).Click();
            Thread.Sleep(4000);
            string project = driver.FindElement(valExpEng1).Text;
            driver.FindElement(btnCloseEng1).Click();
            Thread.Sleep(4000);
            js.ExecuteScript("window.scrollTo(0,-850)");
            Thread.Sleep(4000);
            return project;
        }


        //Validate Related tabs section
        public bool VerifyRelatedTabSections()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;

            Thread.Sleep(4000);
            js.ExecuteScript("window.scrollTo(0,350)");
            Thread.Sleep(5000);
            driver.FindElement(tabParentProj).Click();
            Thread.Sleep(4000);
            js.ExecuteScript("window.scrollTo(0,-150)");
            driver.FindElement(tabRelated).Click();
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblRelatedSections);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            Console.WriteLine("actualValue: " + actualValue[1]);
            //string[] expectedValue = {"CF", "Conflicts Check", "FAS","FR", "HL Internal Opportunity", "OPP DEL","SC"};
            string[] expectedValue = { "Engagements and Internal Teams (2)", "Revenue Accruals (2)", "Expenses (2)", "ERP Invoice Details (2)", "ERP Receipt Detail (2)", "ERP Adjustment Details (2)" };
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

        public string ValidateBillingRequestSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblBillingRequest);
            string billing = driver.FindElement(lblBillingRequest).Text;
            return billing;
        }

        //Validate the Edit functionality of Parent Project
        public string ValidateEditFunctionalityOfParentProject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditParentProj, 90);
            driver.FindElement(btnEditParentProj).Click();
            Thread.Sleep(5000);
            driver.FindElement(txtEditParentProjName).Clear();
            driver.FindElement(txtEditParentProjName).SendKeys("Updated Project");
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(valUpdatedParentProjName).Text;
            return value;
        }

        public string ValidateDeleteParenttProjectButton()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 60);
            Thread.Sleep(4000);
            try
            {
                string value = driver.FindElement(btnDeleteParentProj).Displayed.ToString();
                Console.WriteLine(value);
                if (value.Equals("True"))
                {
                    return "Delete button is displayed";
                }
                else
                {
                    return "Delete button is not displayed";
                }
            }
            catch (Exception)
            {
                return "Delete button is not displayed";
            }
        }


        public string ValidateDeleteParenttProjectButtonForAdmin()
        {
            Thread.Sleep(6000);
            driver.FindElement(tabProject).Click();
            Thread.Sleep(4000);
            driver.FindElement(lnkProjName).Click();

            try
            {
                string value = driver.FindElement(btnDeleteProjAdmin).Displayed.ToString();
                Console.WriteLine(value);
                if (value.Equals("True"))
                {
                    return "Delete button is displayed";
                }
                else
                {
                    return "Delete button is not displayed";
                }
            }
            catch (Exception)
            {
                return "Delete button is not displayed";
            }
        }

        public string ValidateBillingRequestLink()
        {
            Thread.Sleep(5000);
            driver.FindElement(lnkBillingReq).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(titleBillingReq).Text;
            //driver.FindElement(btnCloseBillingReq).Click();
            return value;
        }

        //Validate Billing Request validations
        public bool ValidateBillingRequestValidations()
        {
            Thread.Sleep(4000);
            driver.FindElement(btnNewBillingReq).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnClose).Click();
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(msgBillingReq);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            Console.WriteLine("actualValue: " + actualValue[0]);
            Console.WriteLine("actualValue: " + actualValue[1]);
            Console.WriteLine("actualValue: " + actualValue[2]);
            //string[] expectedValue = {"CF", "Conflicts Check", "FAS","FR", "HL Internal Opportunity", "OPP DEL","SC"};
            string[] expectedValue = { "Client Out-of-Pocket Charges Preference\r\nComplete this field.", "Client Fees Charges Preference\r\nComplete this field.", "Accounting Distribution List\r\nComplete this field." };
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

        public string ValidateDefaultCheckOfAccountingInvoice()
        {
            try
            {
                if (driver.FindElement(chkAccInvoice).Selected)
                {
                    return "Accounting Send Final Invoice is checked";
                }
                else
                {
                    return "Accounting Send Final Invoice is not-checked";
                }
            }

            catch
            {
                return "Accounting Send Final Invoic is not checked";
            }
        }
        public string SaveAllMandatoryFieldsOfBillingRequest()
        {
            driver.FindElement(btnPreference1).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//button[@aria-label='Client Out-of-Pocket Charges Preference']/ancestor::div[2]/div[2]/lightning-base-combobox-item[2]")).Click();
            driver.FindElement(btnPreference2).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//button[@aria-label='Client Fees Charges Preference']/ancestor::div[2]/div[2]/lightning-base-combobox-item[2]")).Click();
            driver.FindElement(txtAccounting).SendKeys("FVA");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//input[@placeholder='Search Contacts...']/ancestor::div[4]/div[2]//li[1]")).Click();
            driver.FindElement(chkInvoice).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
            string message = driver.FindElement(msgPrincipal).Text;
            driver.FindElement(btnClose).Click();
            Thread.Sleep(6000);
            return message;
        }

        public string SelectFinalInvoice()
        {
            driver.FindElement(chkInvoice).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(7000);
            string request = driver.FindElement(valBillingRequest).Text;
            return request;
        }

        //Validate Header row of Billing Request 
        public bool ValidateBillingRequestHeaders()
        {
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblHeaderRow);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            Console.WriteLine("actualValue: " + actualValue[0]);
            Console.WriteLine("actualValue: " + actualValue[1]);
            Console.WriteLine("actualValue: " + actualValue[2]);
            //string[] expectedValue = {"CF", "Conflicts Check", "FAS","FR", "HL Internal Opportunity", "OPP DEL","SC"};
            string[] expectedValue = { "Parent Project", "Total Expense Amount", "Expense Cap", "Total Suggested Fees To Bill", "Total Fees To Bill", "Total Event Amount" };
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

        //Validate links of Header row of Billing Request 
        public bool ValidateBillingRequestHeaderLinks()
        {
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(secBillingReq);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            Console.WriteLine("actualValue: " + actualValue[0]);
            Console.WriteLine("actualValue: " + actualValue[1]);
            Console.WriteLine("actualValue: " + actualValue[2]);
            Console.WriteLine("actualValue: " + actualValue[3]);
            //Console.WriteLine("actualValue: " + actualValue[4]);
            string[] expectedValue = { "Approval History", "Files", "Fees To Bill", "ERP Draft Invoices" };
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


        public string ValidateEditFunctionalityOfBillingRequest()
        {
            driver.FindElement(btnEditbillingReq).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtComments).SendKeys("Testing");
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            string comments = driver.FindElement(valComments).Text;
            return comments;
        }


        public string GetStatusOfBillingRequest()
        {
            Thread.Sleep(5000);
            string status = driver.FindElement(valStatusBillingReq).Text;
            return status;
        }

        public string Save2ndBillingRequest()
        {
            driver.FindElement(subtabParentProj).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkBillingReq, 100);
            driver.FindElement(lnkBillingReq).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewBillingReq, 100);
            driver.FindElement(btnNewBillingReq).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnPreference1).Click();
            driver.FindElement(By.XPath("//button[@aria-label='Client Out-of-Pocket Charges Preference']/ancestor::div[2]/div[2]/lightning-base-combobox-item[2]")).Click();
            driver.FindElement(btnPreference2).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//button[@aria-label='Client Fees Charges Preference']/ancestor::div[2]/div[2]/lightning-base-combobox-item[2]")).Click();
            driver.FindElement(txtAccounting).SendKeys("FVA");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//input[@placeholder='Search Contacts...']/ancestor::div[4]/div[2]//li[1]")).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
            string request = driver.FindElement(valBillingRequest2nd).Text;
            return request;
        }

        public string ValidateNewButtonInFeesToBillSection()
        {
            Thread.Sleep(4000);
            string value = driver.FindElement(btnNewFile).Text;
            return value;
        }

        public void clickNewFeeesButton()
        {
            Thread.Sleep(5000);
            driver.FindElement(subTabBillingReq).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnNewFile).Click();
            Thread.Sleep(4000);
        }

        //Validate New Fees To Bill validations
        public bool ValidateNewFeesToBillValidations()
        {
            Thread.Sleep(4000);
            driver.FindElement(btnNewFile).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnClose).Click();
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(msgBillingReq);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            Console.WriteLine("actualValue: " + actualValue[0]);
            Console.WriteLine("actualValue: " + actualValue[1]);
            Console.WriteLine("actualValue: " + actualValue[2]);
            string[] expectedValue = { "Engagement\r\nComplete this field.", "Fee Type\r\nComplete this field.", "Complete this field.", "Fee Amount\r\nComplete this field." };
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

        public string SaveFeeToBill(string eng, string feeType, string amount)
        {
            driver.FindElement(txtEngagement).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtEngagement).SendKeys("O'Connor - PV");
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("//lightning-base-combobox-formatted-text[@title='" + eng + "']")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnFeeType, 100);
            driver.FindElement(btnFeeType).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//flexipage-column2[1]//lightning-picklist/lightning-combobox//span[@title='" + feeType + "']")).Click();
            //driver.FindElement(By.XPath("//span[@title='"+feeType+"']")).Click();
            driver.FindElement(txtFeeDescription).SendKeys("Testing Fee");
            driver.FindElement(txtFeeAmount).SendKeys(amount);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(6000);
            string request = driver.FindElement(By.XPath("//span[text()='Fee Type']/ancestor::div[2]//tr/td[6]//lst-formatted-text/span[text()='" + feeType + "']")).Text;
            if (feeType.Equals("Retainer Fee") || feeType.Equals("Contingent Fee") || feeType.Equals("Monthly Fee"))
            {

                driver.FindElement(By.XPath("//span[@title='Fee To Bill  c']/ancestor::li[1]/div[2]//button")).Click();
                return request;
            }
            else
            {
                return request;
            }

        }
        public string GetAddedFeevalue(string feeType)
        {
            string request = driver.FindElement(By.XPath("//span[text()='Fee Type']/ancestor::div[2]//span[text()='" + feeType + "']/ancestor::tr/td[8]")).Text;
            return request;
        }

        public string GetGeneratedAdminFeevalue(string feeType)
        {
            string request = driver.FindElement(By.XPath("//span[text()='Fee Type']/ancestor::div[2]//span[text()='" + feeType + "']/ancestor::tr/td[7]")).Text;
            return request;
        }

        public string GetGenerated2ndAdminFeevalue(string feeType)
        {
            string request = driver.FindElement(By.XPath("//span[text()='Fee Type']/ancestor::div[2]//tr[6]/td[7]")).Text;
            return request;
        }

        public string ValidateAddedFeeInBillingRequest()
        {
            driver.FindElement(subTabBillingReq).Click();
            Thread.Sleep(5000);
            string request = driver.FindElement(valFeeTypeBillingReq).Text;
            return request;
        }

        public string ValidateAddedFeeInBillingRequest2ndEng()
        {
            driver.FindElement(subTabBillingReq).Click();
            Thread.Sleep(5000);
            string request = driver.FindElement(valFeeTypeBillingReq2ndEng).Text;
            return request;
        }
        public string ValidateDeleteFunctionalityOfAddedFeeInBillingRequest()
        {
            driver.FindElement(subTabFeeToBill).Click();
            Thread.Sleep(5000);
            string delete = driver.FindElement(btnEditFee).GetAttribute("name");
            driver.FindElement(By.XPath("//span[@title='Fee To Bill  c']/ancestor::li[1]/div[2]//button")).Click();
            return delete;

        }
        public string ValidateEditFunctionalityOfAddedFeeInBillingRequest()
        {
            driver.FindElement(subTabFeeToBill).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnEditFee).Click();
            Thread.Sleep(5000);
            driver.FindElement(txtFeeAmount).Clear();
            driver.FindElement(txtFeeAmount).SendKeys("20");
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
            driver.FindElement(subTabBillingReq).Click();
            Thread.Sleep(5000);
            string request = driver.FindElement(valFeeAmtBillingReq).Text;
            return request;
        }


        public string ValidateDeleteFunctionalityOfBillingRequest()
        {

            driver.FindElement(subTabFeeToBill).Click();
            Thread.Sleep(5000);
            string delete = driver.FindElement(btnEditFee).GetAttribute("name");
            return delete;

        }
        public string GetExpenseTypeOfBillingRequest()
        {
            Thread.Sleep(5000);
            driver.FindElement(btnClose1stBillingReq).Click();
            Thread.Sleep(3000);
            string value = driver.FindElement(valExpenseType).Text;
            return value;
        }

        public string GetTotalExpenseOfBillingRequest()
        {
            Thread.Sleep(5000);
            driver.FindElement(btn1stBillingReq).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(valTotalExpense).Text;
            return value;

        }

        public string ValidateExcludeExpenseFunctionalityOfBillingRequest()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,950)");
            Thread.Sleep(6000);
            driver.FindElement(checkSelectExp).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnUpdateToBill).Click();
            Thread.Sleep(6000);
            driver.FindElement(checkToBill).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnSaveUpdateToBill).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(valTotalExpense).Text;
            return value;
        }

        public string ValidateIncludeExpenseFunctionalityOfBillingRequest()
        {
            Thread.Sleep(5000);
            driver.FindElement(btnUpdateToBill).Click();
            Thread.Sleep(6000);
            driver.FindElement(checkToBill).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnSaveUpdateToBill).Click();
            Thread.Sleep(6000);
            string value = driver.FindElement(valTotalExpense).Text;
            return value;
        }

        public string ValidateDeleteFunctionalityOfExpenseToBill()
        {
            Thread.Sleep(7000);
            driver.FindElement(lnkExpName).Click();
            Thread.Sleep(5000);
            string delete = driver.FindElement(btnEditFee).GetAttribute("name");
            return delete;
        }



        public string ValidateDeleteFunctionalityOfExpenseToBillWithAdmin()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabParentProjAdmin, 90);
            driver.FindElement(tabParentProjAdmin).Click();
            driver.FindElement(lnkParentProjAdmin).Click();
            Thread.Sleep(4000);
            driver.FindElement(lnkBillingReqAdmin).Click();
            Thread.Sleep(4000);
            driver.FindElement(lnkDelExpToBillAdmin).Click();
            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
            return "Expense deleted successfully";
        }

        public string ValidateDeleteFunctionalityOfPVPositionsToBillWithAdmin()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabParentProjAdmin, 90);
            driver.FindElement(tabParentProjAdmin).Click();
            driver.FindElement(lnkParentProjAdmin).Click();
            Thread.Sleep(4000);
            driver.FindElement(lnkBillingReqAdmin).Click();
            Thread.Sleep(4000);
            driver.FindElement(lnkDelPVToBillAdmin).Click();
            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
            return "PV Position To Bill deleted successfully";
        }

        public void ValidateAddExpenseToBillFunctionality()
        {
            driver.FindElement(By.XPath("//table[@aria-label='Billing Requests']/tbody/tr[1]/th[1]//records-hoverable-link")).Click();
            Thread.Sleep(6000);
            //driver.FindElement(btnAddExpToBill).Click();
            //Thread.Sleep(4000);
            //driver.FindElement(btnSelectEng).Click();
            //Thread.Sleep(5000);
            //driver.FindElement(By.XPath("//lightning-base-combobox-item[2]//span[2]/span")).Click();
            //Thread.Sleep(5000);
            //driver.FindElement(chkExpID).Click();
            //Thread.Sleep(5000);
            //driver.FindElement(btnSaveAddExp).Click();
            //Thread.Sleep(5000);
            //string value = driver.FindElement(msgAddExp).Text;
            //return value;
        }

        public string ValidateAddPVPositions()
        {
            Thread.Sleep(7000);
            string value = driver.FindElement(btnAddPVPositions).Text;
            //driver.FindElement(btnCloseBillingReq).Click();
            return value;
        }

        public string ValidateAddPVPositionsFunctionality()
        {
            driver.FindElement(btnAddPVPositions).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnSelectEng).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//lightning-base-combobox-item[2]//span[2]/span")).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnSelectPV).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//button[@aria-label='Select PV']/ancestor::div[2]/div[2]/lightning-base-combobox-item[1]/span[2]/span")).Click();
            Thread.Sleep(5000);
            driver.FindElement(chkExpID).Click();
            driver.FindElement(chkEXpID2).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnSaveAddExp).Click();
            driver.Navigate().Refresh();
            Thread.Sleep(4000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,750)");
            WebDriverWaits.WaitUntilEleVisible(driver, valReportFeePV1, 260);
            string value = driver.FindElement(valReportFeePV1).Text;
            return value;
        }

        public string GetReportFeeOf2ndPVPosition()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, valReportFeePV2, 210);
            string value = driver.FindElement(valReportFeePV2).Text;
            return value;
        }

        public string ValidateIncludePVPositionFunctionalityOfBillingRequest()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,350)");
            Thread.Sleep(6000);
            driver.FindElement(checkSelectPV).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnUpateToBillPV).Click();
            Thread.Sleep(6000);
            //driver.FindElement(checkToBill).Click();
            //Thread.Sleep(5000);
            driver.FindElement(btnSaveUpdateToBill).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(msgSuccess).Text;
            return value;
        }

        public string ValidateAggregateOfReportFeeFunctionality(string fee)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,-550)");
            Thread.Sleep(5000);
            driver.FindElement(btnNewFee).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtEngagement).Click();
            driver.FindElement(txtEngagement).SendKeys("Connor");
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//ul/li[1]/lightning-base-combobox-item//span[2]/span[2]/lightning-base-combobox-formatted-text")).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnFeeType).Click();
            driver.FindElement(By.XPath("//span[text()='Admin Fee']/ancestor::div[1]/lightning-base-combobox-item[8]//span[2]/span")).Click();
            driver.FindElement(txtFeeDescription).SendKeys("Report Fee");
            driver.FindElement(txtFeeAmount).SendKeys(fee);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//a/span[contains(text(),'BR')]")).Click();
            Thread.Sleep(5000);
            string request = driver.FindElement(valTotalFeeToBill).Text;
            return request.Replace(",", "");
        }

        public string ValidateExpenseCapFee()
        {
            string request = driver.FindElement(valExpCap).Text;
            return request.Substring(4, 2);
        }
        public string ValidateBillingEventAccessInAccountingTab()
        {
            driver.FindElement(tabAccounting).Click();
            try
            {
                Thread.Sleep(5000);
                string value = driver.FindElement(secERPRevenue).Text;
                return value;
            }
            catch (Exception)
            {
                return "No access to create a Billing Event";
            }
        }


        //Validate Header row of Billing Request 
        public bool ValidateDeleteFunctionalityOfPVPositionsToBill()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,850)");
            Thread.Sleep(7000);
            driver.FindElement(lnkPVPositionName).Click();
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(btnPVPositions);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            Console.WriteLine("actualValue: " + actualValue[0]);
            Console.WriteLine("actualValue: " + actualValue[1]);
            string[] expectedValue = { "Edit", "Printable View" };
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

        public string ValidateSubmitToBillerFunctionality()
        {
            driver.FindElement(btnSubmitToBiller).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmit, 150);
            driver.FindElement(btnSubmit).Click();
            Thread.Sleep(6000);
            driver.FindElement(tabDetails).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valSubmitterEmail, 150);
            string email = driver.FindElement(valSubmitterEmail).Text;
            return email;
        }
        public string ValidateEmailNotificationFunctionality()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAccDistriList, 120);
            string list = driver.FindElement(valAccDistriList).Text;
            return list;
        }

        public void ValidateSharingFunctionalityOfBillingRequest()
        {
            driver.FindElement(btnSharing).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchUser, 120);
            driver.FindElement(txtSearchUser).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtSearchUser).SendKeys("Hugh Nelson");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//a[@role='option']//div[2]/div[@title='Hugh Nelson']")).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnAccessLevel).Click();
            driver.FindElement(By.XPath("//button[@data-value='Read Only']/ancestor::div[2]/div[2]/lightning-base-combobox-item[3]/span[2]/span")).Click();
            driver.FindElement(btnSaveAddExp).Click();
        }

        //Check if deal team member can access Billing Request
        public void ClickCreatedBillingRequest()
        {
            driver.FindElement(By.XPath("//table[@aria-label='Billing Requests']/tbody/tr[1]/th[1]//records-hoverable-link")).Click();

        }

        public string ValidateAccessToAddBillingEventFunctionality()
        {
            driver.FindElement(By.XPath("//table[@aria-label='Billing Requests']/tbody/tr[1]/th[1]//records-hoverable-link")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabAccounting, 120);
            driver.FindElement(tabAccounting).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnShowBillingEvent).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkNewBillingEvent, 130);
            driver.FindElement(lnkNewBillingEvent).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleBillingEvent, 140);
            string value = driver.FindElement(titleBillingEvent).Text;
            return value;
        }

        //Validate Billing Event validations
        public bool ValidateBillingEventValidations()
        {
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnClose).Click();
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(msgBillingEvent);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            Console.WriteLine("actualValue: " + actualValue[0]);
            Console.WriteLine("actualValue: " + actualValue[1]);
            Console.WriteLine("actualValue: " + actualValue[2]);
            //string[] expectedValue = {"CF", "Conflicts Check", "FAS","FR", "HL Internal Opportunity", "OPP DEL","SC"};
            string[] expectedValue = { "Engagement\r\nComplete this field.", "Contract\r\nComplete this field.", "Event Type\r\nComplete this field.", "Completion Date\r\nComplete this field with format 12/31/2024.", "Event Amount\r\nComplete this field." };
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

        public string SaveBillingEventDetails()
        {
            driver.FindElement(txtEngagement).SendKeys("O'Connor - PV");
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("//lightning-base-combobox-formatted-text[@title='100328']")).Click();

            driver.FindElement(txtContract).SendKeys("O'Connor - PV");
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("//flexipage-field[5]//li[1]/lightning-base-combobox-item")).Click();
            driver.FindElement(txtEventAmount).SendKeys("10");
            driver.FindElement(btnEventType).Click();
            driver.FindElement(By.XPath("//label[text()='Event Type']/ancestor::div[1]//button/ancestor::div[2]/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            driver.FindElement(txtCompletionDate).SendKeys("2/6/2025");
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(valBillingEvent).Text;
            return value;
        }

        public string GetTotalEventAmount()
        {
            string value = driver.FindElement(valTotalEventAmt).Text;
            return value;

        }

        public string ValidateCreatedBillingEventInAccountingTab()
        {
            driver.FindElement(By.XPath("//span[@title='Billing Request  c']")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabAccounting, 140);
            driver.FindElement(tabAccounting).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valBillingEventAccounting, 140);
            string value = driver.FindElement(valBillingEventAccounting).Text;
            return value;
        }

        public string ValidateBillingEventOnParentContractFunctionality()
        {
            driver.FindElement(btnShowBillingEvent).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkNewBillingEvent, 130);
            driver.FindElement(lnkNewBillingEvent).Click();
            Thread.Sleep(5000);
            driver.FindElement(txtEngagement).SendKeys("O'Connor - PV");
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("//lightning-base-combobox-formatted-text[@title='100328']")).Click();

            driver.FindElement(txtContract).SendKeys("O'Connor - PV");
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("//flexipage-field[5]//li[2]/lightning-base-combobox-item")).Click();
            driver.FindElement(txtEventAmount).SendKeys("10");
            driver.FindElement(btnEventType).Click();
            driver.FindElement(By.XPath("//label[text()='Event Type']/ancestor::div[1]//button/ancestor::div[2]/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            driver.FindElement(txtCompletionDate).SendKeys("2/6/2025");
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(msgParentContract).Text;
            driver.FindElement(btnClose).Click();
            Thread.Sleep(5000);
            return value;
        }

        public string ValidateBillingEventOnICOContractFunctionality()
        {
            driver.FindElement(btnClearSelection).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtContract).SendKeys("ICO - 001 - 100328");
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("//flexipage-field[5]//lightning-base-combobox//div[2]/ul/li")).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnClose).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(msgICOContract).Text;
            return value;
        }

        public string ValidateTotalFeesToBillValidation()
        {
            driver.FindElement(tabBillingReq).Click();
            Thread.Sleep(6000);
            driver.FindElement(btnEditbillingReq).Click();
            Thread.Sleep(6000);
            driver.FindElement(txtEditBRComments).SendKeys("Testing");
            By btnStatus = By.XPath("//label[text()='Status']/ancestor::div[1]//button");
            By lblOracleERP = By.XPath("//h2[contains(text(),'Edit BR')]/../..//slot[@name='main']//h3/span[text()='Oracle ERP Information']");

            CustomFunctions.MoveToElement(driver, driver.FindElement(lblOracleERP));
            Thread.Sleep(5000);
            driver.FindElement(btnStatus).Click();
            driver.FindElement(btnStatus).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//label[text()='Status']/ancestor::div[1]//lightning-base-combobox/div//div[2]/lightning-base-combobox-item//span[2]/span[text()='Sent to ERP']")).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
            string message = driver.FindElement(msgTotalFee).Text;
            driver.FindElement(btnClose).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnClosePopUp).Click();
            return message;
        }

        public string UpdateEventAmountAndValidateStatusOfBillingReq()
        {
            driver.FindElement(tabBillingEvent).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditBillingEvent, 130);
            driver.FindElement(btnEditBillingEvent).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtEventAmount).Clear();
            driver.FindElement(txtEventAmount).SendKeys("30000");
            driver.FindElement(btnSave).Click();

            driver.FindElement(tabBillingReq).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditbillingReq, 130);
            driver.FindElement(btnEditbillingReq).Click();
            Thread.Sleep(6000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnStatus));
            Thread.Sleep(5000);
            driver.FindElement(btnStatus).Click();
            driver.FindElement(By.XPath("//label[text()='Status']/ancestor::div[1]//lightning-base-combobox/div//div[2]/lightning-base-combobox-item//span[2]/span[text()='Sent to ERP']")).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(valStatusBillingReq).Text;
            return value;
        }

        public string GetUpdatedCommentOfBillingRequest()
        {
            driver.Navigate().Refresh();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valComments, 130);
            string value = driver.FindElement(valComments).Text;
            return value;
        }
        public string ValidateERPLastIntStatus()
        {
            //driver.Navigate().Refresh();
            Thread.Sleep(4000);
            driver.FindElement(tabAccounting).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valERPStatus, 130);
            string value = driver.FindElement(valERPStatus).Text;
            return value;
        }
        public string ValidateVisibilityOfGenerateInvoiceOrResubmitButton()
        {
            Thread.Sleep(4000);
            try
            {
                string value = driver.FindElement(btnGenerateInvoice).Displayed.ToString();
                string invoice = driver.FindElement(btnGenerateInvoice).Text;
                return invoice;
            }
            catch (Exception)
            {
                string value = driver.FindElement(btnResubmit).Displayed.ToString();
                string resubmit = driver.FindElement(btnResubmit).Text;
                return resubmit;
            }

        }
        //Validate Billing Event validations
        public bool ValidateGenerateInvoiceValidations()
        {
            driver.FindElement(btnSaveInvoice).Click();
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(msgInvoice);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            Console.WriteLine("actualValue: " + actualValue[0]);
            Console.WriteLine("actualValue: " + actualValue[1]);
            string[] expectedValue = { "Bill Through Date\r\nComplete this field with format Dec 31, 2024.", "Invoice Date\r\nComplete this field with format Dec 31, 2024." };
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
