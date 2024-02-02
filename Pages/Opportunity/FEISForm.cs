
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;

namespace SF_Automation.Pages.Opportunity
{
    class FEISForm : BaseClass
    {
        By OppName = By.CssSelector("span[id*='id44']");
        By clientComp = By.CssSelector("span[id*='id46']");
        By subjectComp = By.CssSelector("span[id*='id47']");
        By jobType = By.CssSelector("span[id*='id45']");
        By btnSubmit = By.CssSelector("input[id*='btnSubmit']");
        By errorList = By.CssSelector("span[id*='j_id18']>ul");
        By btnCancel = By.CssSelector("input[value='Cancel Submission']");
        By checkToggleTabs = By.Id("toggleTabs");
        By tabList = By.Id("tabsList");
        By txtAmountPaidOnDelivery = By.CssSelector("input[id*='id55']");
        By txtIncrementalFee = By.CssSelector("textarea[name*='id57']");
        By txtTranOverview = By.CssSelector("textarea[id*='descriptionOfTransaction']");
        By comboTransactionType = By.CssSelector("select[id*='TransactionType']");
        By comboLegalStructure = By.CssSelector("select[id*='LegalStructure']");
        By txtTransactionSize = By.CssSelector("input[id*='TransSize']");
        By comboFormOfConsideration = By.CssSelector("select[id*='FormConsider_unselected']> optgroup > option[value='0']");
        By btnFormRightArrow = By.CssSelector("img[id*='FormConsider_right_arrow']");
        By comboAffiliatedParties = By.CssSelector("select[id*='affiliatedParties']");
        By comboPubliclyDisclosed = By.CssSelector("select[id*='isPubliclyDisclosed']");
        By comboRelativeFairness = By.CssSelector("select[id*='fairnessRelativeFairness']");
        By comboFairnessOfTransaction = By.CssSelector("select[id*='fairnessFairnessOrTerms']");
        By comboFairnessConclusion = By.CssSelector("select[id*='fairnessMultipleConclusions']");
        By comboClientCommittee = By.CssSelector("select[id*='fairnessCommitteeOrTrustee']");
        By comboUnusualAttribute = By.CssSelector("select[id*='fairnessUnusualOpinion']");
        By comboRelationshipQuestion1 = By.CssSelector("select[id*='Conflicts3a']");
        By comboRelationshipQuestion2 = By.CssSelector("select[id*='Conflicts35a']");
        By comboRelationshipQuestion3 = By.CssSelector("select[id*='Conflicts4a']");
        By comboRelationshipQuestion4 = By.CssSelector("select[id*='Conflicts5a']");
        By comboOtherOpinion = By.CssSelector("select[id*='shareholderVote']");
        By comboSpecialCommittee = By.CssSelector("select[id*='id306']");
        By titleEmailPage = By.CssSelector("div.pbSubheader.brandTertiaryBgr.tertiaryPalette > h3");
        By valEmailOppName = By.CssSelector("body[id*='Body_rta_body'] > span:nth-child(9) > span");
        By btnCancelEmail = By.CssSelector("input[value='Cancel']");
        By btnReturntoOpp = By.CssSelector("input[value*='Return to Opportunity']");
        By lblDefaultTabL = By.XPath("//lightning-tab-bar/ul/li/a[text()='Opportunity Overview']");
        By msgFEISFormL = By.XPath("//span[@title='Please check this box and press Save to ensure all required fields are completed.']");
        By valOppNameL = By.XPath("//span[text()='Related Opportunity']/ancestor::dt/following::dd[1]//a/slot/slot/span");
        By valJobTypeL = By.XPath("//span[text()='Job Type']/ancestor::dl/dd//records-formula-output/slot/lightning-formatted-text");
        By valClientL = By.XPath("//span[text()='Client Company']/ancestor::dl/dd//records-formula-output/slot/lightning-formatted-text");
        By valSubjectL = By.XPath("//span[text()='Subject Company']/ancestor::dl/dd//records-formula-output/slot/lightning-formatted-text");
        By valRefTypeL = By.XPath("//span[text()='Referral Type']/ancestor::dl/dd//records-formula-output/slot/lightning-formatted-text");
        By lnkRelOppL = By.XPath("//button[@title='Edit Related Opportunity']");
        By btnSaveL = By.XPath("//button[@name='SaveEdit']");
        By msgMandatoryFields = By.XPath("//ul[@class='errorsList slds-list_dotted slds-m-left_medium']/li/a");
        By btnCloseL = By.XPath("//button[@title='Close error dialog']");
        By tabTransInfoL = By.XPath("//li[@title='Transaction Information']");
        By btnTransType = By.XPath("//label[text()='Transaction Type']/ancestor::div[1]/div//button");
        By valTransType = By.XPath("//lightning-base-combobox-item/span/span[text()='Other']");
        By lblDescribeOther = By.XPath("//label[text()='Describe Other Transaction Type']");
        By btnLegalStrL = By.XPath("//label[text()='Legal Structure']/ancestor::div[1]/div//button");
        By lblOtherLegalL = By.XPath("//label[text()='FEIS - Other Legal Structure Desc']");
        By valFormL = By.XPath("//li[3]/div/span/span");
        By btnChosenL = By.XPath("//div[4]/lightning-button-icon[1]/button");
        By lblOtherFormL = By.XPath("//label[text()='FEIS - Other Forms of Consideration Desc']");
        By btnOpinionParties = By.XPath("//label[text()='Opinion Parties Affiliated']/ancestor::div[1]/div//button");
        By lblOpinionPartiesL = By.XPath("//label[text()='Opinion Affiliated Parties Summary']");
        By tabOwnershipL = By.XPath("//lightning-tab-bar/ul/li/a[text()='Ownership & Advisors']");
        By msgOwnershipL = By.XPath("//span[text()='To Add Target/Subject and/or Counterparty(ies), please use the buttons on the top right of this form.']");
        By btnAddTargetL = By.XPath("//button[text()='Add Target/Subject']");
        By lblCompanyL = By.XPath("//span[text()='*']/ancestor::label");
        By txtCompanyL = By.XPath("//label[text()='Company']/ancestor::tr/td/div[1]/span/input");
        By btnSaveRecord = By.XPath("//td[@class='pbButton center']/input[@value='Save Record']");
        By txtSuccess = By.XPath("//h4[text()='Success:']");
        By btnPEFirm = By.XPath("//label[text()='Does any PE Firm individually own 10% or more of the equity?']/ancestor::tr/td[2]/select");
        By btnAddPEFirm = By.XPath("//input[@value='Add PE Firm']");
        By txtSearchCompL = By.XPath("//input[contains(@name,'txtSearch')]");
        By btnGo = By.XPath("//input[@value='Go']");
        By chkCompany = By.XPath("//input[contains(@name,':tblResults:0:j_id50')]");
        By txtOwnershipPer = By.XPath("//input[contains(@name,':tblResults:0:j_id54')]");
        By btnAddSelected = By.XPath("//input[contains(@value,'Add Selected')]");
        By btnClose = By.XPath("//button[@title='Close']");
        By lnkEditPEFirm = By.XPath("//span[contains(@id,'panPEFirms')]/table/tbody/tr/td[1]/a[1]");
        By lnkDelPEFirm = By.XPath("//span[contains(@id,'panPEFirms')]/table/tbody/tr/td[1]/a[2]");
        By valAddedPEFirm = By.XPath("//span[contains(@id,'panPEFirms')]/table/tbody/tr/td[2]/span");
        By valAddedOwnership = By.XPath("//span[contains(@id,'panPEFirms')]/table/tbody/tr/td[3]/span");
        By txtEditOwnership = By.XPath("//input[contains(@id,'Percent')]");
        By btnSavePEFirm = By.XPath("//input[@value='Save']");
        By tblTargetCompL = By.XPath("//tbody/tr");
        By btnEditTargetComp = By.XPath("//table/tbody/tr/td[8]//button");
        By lnkEdit = By.XPath("//a/span[text()='Edit']");
        By lnkDelete = By.XPath("//a/span[text()='Delete']");
        By txtPrivateEquity = By.XPath("//label[text()='Private Equity (%)']/ancestor::tr[1]/td[1]/input");
        By valPrivateEquity = By.XPath("//tbody/tr/td[3]");
        By btnOK = By.XPath("//button[text()='OK']");
        By btnAddCounterpartyL = By.XPath("//button[text()='Add Counterparty']");


        //Validate Opp Name
        public string ValidateOppName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, OppName, 50);
            string valOpp = driver.FindElement(OppName).Text;
            return valOpp;
        }

        //Validate Opp Name
        public string ValidateOppNameL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valOppNameL, 50);
            string valOpp = driver.FindElement(valOppNameL).Text;
            return valOpp;
        }

        //Validate Client Name
        public string ValidateClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, clientComp);
            string valclient = driver.FindElement(clientComp).Text;
            return valclient;
        }
        //Validate Subject Name
        public string ValidateSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, subjectComp);
            string valSubject = driver.FindElement(subjectComp).Text;
            return valSubject;
        }

        //Validate Client Name
        public string ValidateClientL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valClientL);
            string valclient = driver.FindElement(valClientL).Text;
            return valclient;
        }
        //Validate Subject Name
        public string ValidateSubjectL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSubjectL);
            string valSubject = driver.FindElement(valSubjectL).Text;
            return valSubject;
        }
        //Validate JobType
        public string ValidateJobType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, jobType);
            string valJobType = driver.FindElement(jobType).Text;
            return valJobType;
        }
        //Validate JobType
        public string ValidateJobTypeL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valJobTypeL);
            string valJobType = driver.FindElement(valJobTypeL).Text;
            return valJobType;
        }

        //Validate Ref Type
        public string ValidateRefTypeL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRefTypeL);
            string valRefType = driver.FindElement(valRefTypeL).Text;
            return valRefType;
        }

        //Fetch validations for mandatory fields
        public string GetFieldsValidations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmit, 60);
            driver.FindElement(txtTranOverview).Clear();
            driver.FindElement(btnSubmit).Click();
            Thread.Sleep(2000);
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

        public void EnterDetailsAndClickSubmit(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtAmountPaidOnDelivery, 90);
            driver.FindElement(txtTranOverview).Click();
            driver.SwitchTo().Alert().Accept();

            //General
            driver.FindElement(txtAmountPaidOnDelivery).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 2));
            driver.FindElement(txtIncrementalFee).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 3));

            //Background on Transaction
            driver.FindElement(txtTranOverview).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 4));
            driver.FindElement(comboTransactionType).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 5));
            driver.FindElement(comboLegalStructure).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 6));

            //Form and Amount of Consideration
            driver.FindElement(txtTransactionSize).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 7));
            driver.FindElement(comboFormOfConsideration).Click();
            driver.FindElement(btnFormRightArrow).Click();

            //Affiliated Parties Information
            driver.FindElement(comboAffiliatedParties).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 8));

            //Legal Review Criteria
            driver.FindElement(comboPubliclyDisclosed).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 9));
            driver.FindElement(comboRelativeFairness).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 10));
            driver.FindElement(comboFairnessOfTransaction).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 11));
            driver.FindElement(comboFairnessConclusion).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 12));
            driver.FindElement(comboClientCommittee).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 13));
            driver.FindElement(comboUnusualAttribute).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 14));

            //Relationship Questions
            driver.FindElement(comboRelationshipQuestion1).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 15));
            driver.FindElement(comboRelationshipQuestion2).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 16));
            driver.FindElement(comboRelationshipQuestion3).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 17));
            driver.FindElement(comboRelationshipQuestion4).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 18));

            //Other Opinion Information
            driver.FindElement(comboOtherOpinion).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 19));
            driver.FindElement(comboSpecialCommittee).SendKeys(ReadExcelData.ReadData(excelPath, "FEISForm", 20));

            driver.FindElement(btnSubmit).Click();
        }
        public string ValidateHeader()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, titleEmailPage, 170);
            string title = driver.FindElement(titleEmailPage).Text;
            Console.WriteLine(title);
            return title;
        }
        public string GetOppName()
        {
            driver.SwitchTo().Frame(0);
            WebDriverWaits.WaitUntilEleVisible(driver, valEmailOppName, 90);
            string emailSub = driver.FindElement(valEmailOppName).Text;
            Console.WriteLine(emailSub);
            driver.SwitchTo().DefaultContent();
            driver.FindElement(btnCancelEmail).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturntoOpp, 70);
            driver.FindElement(btnReturntoOpp).Click();
            return emailSub;
        }

        //---Lightning
        //Validate default tab displayed on FEIS form
        public string ValidateDefaultTabOfFEISForm()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblDefaultTabL, 150);
            string tab = driver.FindElement(lblDefaultTabL).Text;
            return tab;
        }
     
        public string ValidateInformativeMessageOnFEISForm()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgFEISFormL, 150);
            string message = driver.FindElement(msgFEISFormL).Text;
            return message;
        }

        public string ValidateInformativeMessageOnOwnershipTab()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,-900)");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabOwnershipL, 150);
            driver.FindElement(tabOwnershipL).Click();            
            WebDriverWaits.WaitUntilEleVisible(driver, msgOwnershipL, 170);
            string message = driver.FindElement(msgOwnershipL).Text;
            return message;
        }

        public string ValidateAddTargetButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddTargetL, 150);            
            string message = driver.FindElement(btnAddTargetL).Text;
            return message;
        }

        public string ValidateCompanyFieldOnAddTargetDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddTargetL, 150);
            driver.FindElement(btnAddTargetL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='accessibility title']")));
            WebDriverWaits.WaitUntilEleVisible(driver, lblCompanyL, 190);
            string name = driver.FindElement(lblCompanyL).Text;
            return name;            
        }

        public string SaveTargetCompanyDetails()
        {
           
            driver.FindElement(btnSaveRecord).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            WebDriverWaits.WaitUntilEleVisible(driver, tabOwnershipL, 150);
            driver.FindElement(tabOwnershipL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblTargetCompL, 140);
            string name = driver.FindElement(tblTargetCompL).Displayed.ToString();
            return name;
        }

        public string EditTargetCompanyDetails(string valOwnership)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditTargetComp, 160);
            driver.FindElement(btnEditTargetComp).Click();
            Thread.Sleep(4000);           
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEdit, 150);
            driver.FindElement(lnkEdit).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[contains(@name,'vfFrameId')]")));
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtPrivateEquity, 170);
            driver.FindElement(txtPrivateEquity).SendKeys(valOwnership);
            driver.FindElement(btnSaveRecord).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            WebDriverWaits.WaitUntilEleVisible(driver, tabOwnershipL, 150);
            driver.FindElement(tabOwnershipL).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valPrivateEquity, 140);
            string name = driver.FindElement(valPrivateEquity).Text;
            return name;            
        }


        public string DeleteTargetCompanyDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditTargetComp, 160);
            driver.FindElement(btnEditTargetComp).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDelete, 150);
            driver.FindElement(lnkDelete).Click();            
            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 150);
            driver.FindElement(btnOK).Click();
            try
            {
                Thread.Sleep(4000);
                WebDriverWaits.WaitUntilEleVisible(driver, tblTargetCompL, 140);
                string name = driver.FindElement(tblTargetCompL).Text;
                return name;
            }
            catch(Exception e)
            {                
                return "Record has been deleted";
            }           
        }

        public string ValidateAddCounterpartyButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterpartyL, 250);
            string message = driver.FindElement(btnAddCounterpartyL).Text;
            return message;
        }

        ////Validate all required validations
        //public string GetErrorMessagesOnFEISForm()
        //{
        //    WebDriverWaits.WaitUntilEleVisible(driver, lnkRelOppL, 150);
        //    driver.FindElement(lnkRelOppL).Click();
        //    WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
        //    driver.FindElement(btnSaveL).Click();
        //    Thread.Sleep(5000);
        //    string message = driver.FindElement(msgMandatoryFields).Text;
        //    driver.FindElement(btnCloseL).Click();
        //    return message;
        //}
        public string ValidateCompanyFieldOnAddCounterpartyDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterpartyL, 150);
            driver.FindElement(btnAddCounterpartyL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='accessibility title']")));
            WebDriverWaits.WaitUntilEleVisible(driver, lblCompanyL, 190);
            string name = driver.FindElement(lblCompanyL).Text;
            return name;
        }

        public bool GetErrorMessagesOnFEISForm()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRelOppL, 150);
            driver.FindElement(lnkRelOppL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(msgMandatoryFields);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            //string[] expectedValue = {"CF", "Conflicts Check", "FAS","FR", "HL Internal Opportunity", "OPP DEL","SC"};
            string[] expectedValue = { "Yes/No", "Estimated Transaction Size (MM)", "Fairness Committee or Trustee", "Fairness Fairness or Terms", "Fairness Opinion Publicly Disclosed", "Fairness Relative Fairness", "Fairness Unusual Opinion", "Form of Consideration", "Legal Structure", "Opinion Parties Affiliated", "Opinion Special Committee", "Transaction Type" };
            bool isSame = true;
            driver.FindElement(btnCloseL).Click();
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

        //Validate Transaction info tab
        public string ValidateAdditionalFieldsOnTransInfo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabTransInfoL, 150);
            driver.FindElement(tabTransInfoL).Click();
            Thread.Sleep(5000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,350)");
            WebDriverWaits.WaitUntilEleVisible(driver, btnTransType, 150);
            driver.FindElement(btnTransType).Click();
            Thread.Sleep(4000);          
            CustomFunctions.MoveToElement(driver, driver.FindElement(valTransType));
            driver.FindElement(valTransType).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblDescribeOther, 150);
            string value = driver.FindElement(lblDescribeOther).Text;
            return value;

        }
        //Validate Other Legal Structure field
        public string ValidateAdditionalOtherLegalField()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnLegalStrL, 150);
            driver.FindElement(btnLegalStrL).Click();
            Thread.Sleep(4000);           
            driver.FindElement(By.XPath("//flexipage-column2[2]/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/div[1]/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[7]/span[2]/span")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblOtherLegalL, 150);
            string value = driver.FindElement(lblOtherLegalL).Text;
            return value;

        }
        //Validate ther *Form of Consideration
        public string ValidateOtherFormofConsideration()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,400)");
            Thread.Sleep(4000);
            driver.FindElement(valFormL).Click();
            driver.FindElement(btnChosenL).Click();
            js.ExecuteScript("window.scrollTo(0,500)");
            Thread.Sleep(5000);           
            WebDriverWaits.WaitUntilEleVisible(driver, lblOtherFormL, 150);
            string value = driver.FindElement(lblOtherFormL).Text;
            return value;

        }

        //Validate ther *Form of Consideration
        public string ValidateAdditionalOpinionPartiesAffiliated()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,750)");
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnOpinionParties, 160);
            driver.FindElement(btnOpinionParties).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//flexipage-component2[2]/slot//flexipage-component2[4]//record_flexipage-record-field//lightning-base-combobox-item[3]/span[2]/span")).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblOpinionPartiesL, 170);
            string value = driver.FindElement(lblOpinionPartiesL).Text;
            return value;
        }

        //Validate Select PE Firm
        public string ValidateSelectPEFirm()    
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyL, 150);
            driver.FindElement(txtCompanyL).SendKeys("te");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//table/tbody/tr[2]/td/div/strong")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnPEFirm, 160);
            driver.FindElement(btnPEFirm).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//label[text()='Does any PE Firm individually own 10% or more of the equity?']/ancestor::tr/td[2]/select/option[text()='Yes']")).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddPEFirm, 170);
            string value = driver.FindElement(btnAddPEFirm).GetAttribute("value");
            return value;
        }

        //Validate search window appears on clicking Add PE Firm button
        public string ValidateSearchWindowUponClickingAddPEFirmButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddPEFirm, 150);
            driver.FindElement(btnAddPEFirm).Click();
            Thread.Sleep(4000);            
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@id='iframeContentId']")));
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchCompL, 160);
            string name = driver.FindElement(txtSearchCompL).Displayed.ToString();
            return name;
            
        }

        //Validate add company functionality
        public string ValidateAddCompanyDetails(string valComp, string valOwnership)
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchCompL, 160);
            driver.FindElement(txtSearchCompL).SendKeys(valComp);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkCompany, 170);
            driver.FindElement(chkCompany).Click();
            driver.FindElement(txtOwnershipPer).SendKeys(valOwnership);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddSelected, 180);
            driver.FindElement(btnAddSelected).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtSuccess, 190);
            string name = driver.FindElement(txtSuccess).Text;
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(4000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[contains(@name,'vfFrameId')]")));
            Thread.Sleep(6000);
            driver.FindElement(btnClose).Click();            
            return name;
        }

        //Validate links corresponding to added PE Firm record
        public string ValidateEditLinkOfAddedPEFirmDetail()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditPEFirm, 180);            
            string name = driver.FindElement(lnkEditPEFirm).Text;            
            return name;
        }

        //Validate links corresponding to added PE Firm record
        public string ValidateDelLinkOfAddedPEFirmDetail()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDelPEFirm, 160);
            string name = driver.FindElement(lnkDelPEFirm).Text;
            return name;
        }

        //Validate added PE Firm company
        public string ValidateAddedCompanyInPEFirmDetail()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedPEFirm, 160);
            string name = driver.FindElement(valAddedPEFirm).Text;
            return name;
        }

        //Validate links corresponding to added PE Firm record
        public string ValidateAddedOwnershipInAddedPEFirmDetail()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedOwnership, 160);
            string name = driver.FindElement(valAddedOwnership).Text;
            return name;
        }

        //Validate edit company details functionality
        public string ValidateEditCompanyDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditPEFirm, 160);
            driver.FindElement(lnkEditPEFirm).Click();           
            Thread.Sleep(4000);            
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@id='iframeContentId']")));
            WebDriverWaits.WaitUntilEleVisible(driver, txtEditOwnership, 170);
            driver.FindElement(txtEditOwnership).Clear();
            driver.FindElement(txtEditOwnership).SendKeys("5");            
            WebDriverWaits.WaitUntilEleVisible(driver, btnSavePEFirm, 190);
            driver.FindElement(btnSavePEFirm).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(4000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[contains(@name,'vfFrameId')]")));
            Thread.Sleep(6000);
            driver.FindElement(btnClose).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedOwnership, 160);
            string value = driver.FindElement(valAddedOwnership).Text;
            return value;
        }

       
        //Validate cancel company details functionality
        public string ValidateCancelCompanyDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDelPEFirm, 160);
            driver.FindElement(lnkDelPEFirm).Click();
            Thread.Sleep(4000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Dismiss();                  
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedOwnership, 160);
            string value = driver.FindElement(valAddedOwnership).Text;
            return value;
        }

        //Validate delete company details functionality
        public string ValidateDeleteCompanyDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDelPEFirm, 160);
            driver.FindElement(lnkDelPEFirm).Click();
            Thread.Sleep(4000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valAddedOwnership, 160);
                string value = driver.FindElement(valAddedOwnership).Text;
                return value;
            }
            catch (Exception e)
            {
                return "Added PE Firm record has been deleted";
            }
        }

        //Validate save functionality of Counterparty
        public string ValidateSaveFunctionalityOfCounterparty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyL, 150);
            driver.FindElement(txtCompanyL).SendKeys("te");
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//table/tbody/tr[2]/td/div/strong")).Click();            
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveRecord, 170);
            driver.FindElement(btnSaveRecord).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            WebDriverWaits.WaitUntilEleVisible(driver, tabOwnershipL, 150);
            driver.FindElement(tabOwnershipL).Click();
            Thread.Sleep(4000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,750)");
            WebDriverWaits.WaitUntilEleVisible(driver, tblTargetCompL, 180);
            string name = driver.FindElement(tblTargetCompL).Displayed.ToString();
            return name;
        }
    }
}




