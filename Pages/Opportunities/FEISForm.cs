using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using AventStack.ExtentReports.Reporter.Configuration;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Builder;
using System.Security.Policy;

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
        By valEmailOppNameL = By.XPath("//body/p[6]/span");

        By btnCancelEmail = By.CssSelector("input[value='Cancel']");
        By btnReturntoOpp = By.CssSelector("input[value*='Return to Opportunity']");
        By lblDefaultTabL = By.XPath("//lightning-tab-bar/ul/li/a[text()='Opportunity Overview']");
        By msgFEISFormL = By.XPath("//span[@title='Please check this box and press Save to ensure all required fields are completed.']");
        By valOppNameL = By.XPath("//span[text()='Related Opportunity']/ancestor::dt/following::dd[1]//slot/span/slot");
        By valJobTypeL = By.XPath("//span[text()='Job Type']/ancestor::div[2]/dd//records-formula-output/slot/lightning-formatted-text");
        By valClientL = By.XPath("//span[text()='Client Company']/ancestor::div[2]/dd//records-formula-output/slot/lightning-formatted-text");
        By valSubjectL = By.XPath("//span[text()='Subject Company']/ancestor::div[2]/dd//records-formula-output/slot/lightning-formatted-text");
        By valRefTypeL = By.XPath("//span[text()='Referral Type']/ancestor::div[2]/dd//records-formula-output/slot/lightning-formatted-text");
        By lnkRelOppL = By.XPath("//button[@title='Edit Related Opportunity']");
        By btnSaveL = By.XPath("//button[@name='SaveEdit']");
        By btnCancelL = By.XPath("//button[@name='CancelEdit']");
        By msgMandatoryFields = By.XPath("//ul[@class='errorsList slds-list_dotted slds-m-left_medium']/li/a");
        By btnCloseL = By.XPath("//button[@title='Close error dialog']");
        By tabTransInfoL = By.XPath("//li[@title='Transaction Information']");
        By btnTransType = By.XPath("//label[text()='Transaction Type']/ancestor::div[1]/div//button");
        By valTransType = By.XPath("//lightning-base-combobox-item/span/span[text()='Other']");
        By txtEstTxnSize = By.XPath("//input[@name='Estimated_Transaction_Size_MM__c']");
        By lblDescribeOther = By.XPath("//label[text()='Describe Other Transaction Type']");
        By btnLegalStrL = By.XPath("//label[text()='Legal Structure']/ancestor::div[1]/div//button");
        By valAvailable = By.XPath("//span[text()='Cash']");
        By btnMove = By.XPath("//button[@title='Move selection to Chosen']");
        By lblOtherLegalL = By.XPath("//label[text()='FEIS - Other Legal Structure Desc']");
        By valFormL = By.XPath("//li[3]/div/span/span");
        By btnChosenL = By.XPath("//div[4]/lightning-button-icon[1]/button");
        By lblOtherFormL = By.XPath("//label[text()='FEIS - Other Forms of Consideration Desc']");
        By txtOtherFormL = By.XPath("//label[text()='FEIS - Other Forms of Consideration Desc']/ancestor::lightning-textarea//textarea");
        By btnOpinionParties = By.XPath("//label[text()='Opinion Parties Affiliated']/ancestor::div[1]/div//button");
        By lblOpinionPartiesL = By.XPath("//label[text()='Opinion Affiliated Parties Summary']");
        By txtOpinionParties = By.XPath("//label[text()='Opinion Affiliated Parties Summary']/ancestor::lightning-textarea//textarea");

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
        By tabForm = By.XPath("//lightning-tab-bar/ul/li/a[text()='Form of Opinion']");
        By lnk1stCheck = By.XPath("//flexipage-component2[2]/slot/flexipage-field-section2//div/div/dl/dd/div/button/span[1]");
        By chk1stCheck = By.XPath("//span[text()='﻿The consideration to be received by the Unaffiliated Stockholders in the Transaction is fair to them from a financial point of view.']/ancestor::div[1]/laf-progressive-container//input");
        By chk2ndCheck = By.XPath("//flexipage-field[@data-field-id='RecordFEIS_Opine_Option_2_cField']//following::input[@name='FEIS_Opine_Option_2__c']");
        By chk3rdCheck = By.XPath("//flexipage-field[@data-field-id='RecordFEIS_Opine_Option_3_cField']//following::input[@name='FEIS_Opine_Option_3__c']");
        By chk4thCheck = By.XPath("//flexipage-field[@data-field-id='RecordFEIS_Opine_Option_4_cField']//following::input[@name='FEIS_Opine_Option_4__c']");
        By chk5thCheck = By.XPath("//flexipage-field[@data-field-id='RecordFEIS_Opine_Option_5_cField']//following::input[@name='FEIS_Opine_Option_5__c']");
        By chk6thCheck = By.XPath("//flexipage-field[@data-field-id='RecordFEIS_Opine_Option_6_cField']//following::input[@name='FEIS_Opine_Option_6__c']");
        By txtOpinionNotes = By.XPath("//label[text()='Form of Opinion Notes']/ancestor::lightning-textarea//textarea");
        By lnkRelValidation = By.XPath("//li/a[text()='Yes/No']");
        By msgRelationshipQ = By.XPath("//div[text()='Complete this field.']");
        By tabRelationship = By.XPath("//li/a[text()='Relationship Questions']");
        By btn1stQues = By.XPath("//flexipage-tab2[6]/slot/flexipage-component2[3]/slot/flexipage-field-section2/div/div//div[1]/button");
        By btn2ndQues = By.XPath("//flexipage-tab2[6]/slot/flexipage-component2[5]/slot/flexipage-field-section2/div/div//div[1]/button");
        By btn3rdQues = By.XPath("//flexipage-tab2[6]/slot/flexipage-component2[7]/slot/flexipage-field-section2/div/div//div[1]/button");
        By btn4thQues = By.XPath("//flexipage-tab2[6]/slot/flexipage-component2[9]/slot/flexipage-field-section2/div/div//div[1]/button");
        By lblYes = By.XPath("//label[text()='If Yes, please explain']");
        By lnkFairnessQues = By.XPath("//a[text()='Fairness Opinion Publicly Disclosed']");
        By tabLegalReview = By.XPath("//a[text()='Legal Review Criteria']");
        By btnFairness1 = By.XPath("//label[text()='Fairness Opinion Publicly Disclosed']/ancestor::lightning-combobox//button");
        By btnFairness2 = By.XPath("//label[text()='Fairness Relative Fairness']/ancestor::lightning-combobox//button");
        By btnFairness3 = By.XPath("//label[text()='Fairness Fairness or Terms']/ancestor::lightning-combobox//button");
        By btnFairness4 = By.XPath("//label[text()='Fairness Committee or Trustee']/ancestor::lightning-combobox//button");
        By btnFairness5 = By.XPath("//label[text()='Fairness Unusual Opinion']/ancestor::lightning-combobox//button");
        By btnFairness6 = By.XPath("//label[text()='Fairness Multiple Conclusions']/ancestor::lightning-combobox//button");
        By lnkOpinionSpec = By.XPath("//a[text()='Opinion Special Committee']");
        By tabOtherOpinion = By.XPath("//a[text()='Other Opinion Information']");
        By msgOtherOpinion = By.XPath("//label[text()='Opinion Special Committee']/ancestor::div[1]//div[text()='Complete this field.']");
        By btnOpinionSpec = By.XPath("//label[text()='Opinion Special Committee']/ancestor::lightning-combobox//button");
        By btnShareholder = By.XPath("//label[contains(@id,'shareholderVoteTXT')]");
        By btnSaveOpinion = By.XPath("//input[@value='Save']");
        By msgMandatory = By.XPath("//ul[@class='errorsList slds-list_dotted slds-m-left_medium']/li/a");
        By lnkTxnType = By.XPath("//ul[@class='errorsList slds-list_dotted slds-m-left_medium']/li[1]/a");
        By lnkFormCheck = By.XPath("//button[@title='Edit Form Check (required to submit)']");
        By btnFormCheck = By.XPath("//input[@name='Submit_For_Review__c']");
        By btnSubmitFEIS = By.XPath("//button[text()='Submit FEIS (Part I) Form']");
        By lblSendEmail = By.XPath("//table/tbody/tr/td/h2[text()='Send Email']");
        By txtTo = By.XPath("//label[text()='To']/ancestor::tr[1]/td/div/span/input[1]");
        By btnSendEmail = By.XPath("//div[1]/table/tbody/tr/td[2]/input[1]");
        By msgPostSubmission = By.XPath("//div[@class='pageLevelErrors']/ul/li");
        By btnMore = By.XPath("//div/lightning-tab-bar/ul/li/lightning-button-menu/button[text()='More']");
        By btnMoreCAO = By.XPath("//li[7]/lightning-button-menu/button");
        By tabReview = By.XPath("//span[text()='Review']");
        By tabReviewCAO = By.XPath("//lightning-tab-bar/ul/li[8]/lightning-button-menu/button");
        By lblReviewed = By.XPath("//div/span[text()='Reviewed']");

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
            WebDriverWaits.WaitUntilEleVisible(driver, valOppNameL, 150);
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
            if(tabsPresent == true)
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

        public string GetOppNameL()
        {
            Thread.Sleep(6000);
            //WebDriverWaits.WaitUntilEleVisible(driver, valEmailOppNameL, 90);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[contains(@title,'Editor')]")));
            Thread.Sleep(4000);
            string emailSub = driver.FindElement(valEmailOppNameL).Text;
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
            IJavaScriptExecutor js = (IJavaScriptExecutor) Driver;
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
            Thread.Sleep(6000);
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
                Thread.Sleep(6000);
                WebDriverWaits.WaitUntilEleVisible(driver, tblTargetCompL, 40);
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

        //Validate Transaction info tab
        public string ValidateAdditionalFieldsOnTransInfo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabTransInfoL, 150);
            driver.FindElement(tabTransInfoL).Click();
            Thread.Sleep(5000);
            IJavaScriptExecutor js = (IJavaScriptExecutor) Driver;
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
            IJavaScriptExecutor js = (IJavaScriptExecutor) Driver;
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
            IJavaScriptExecutor js = (IJavaScriptExecutor) Driver;
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

        public void SaveAllMandatoryFieldsOfTransactionInfoTab()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) Driver;
            Thread.Sleep(5000);

            driver.FindElement(btnTransType).Click();
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("//label[text()='Transaction Type']/ancestor::div[1]/div//lightning-base-combobox-item/span[2]/span[text()='Buy Side']")).Click();
            Thread.Sleep(5000);
            driver.FindElement(txtEstTxnSize).SendKeys("10");
            js.ExecuteScript("window.scrollTo(0,250)");
            Thread.Sleep(5000);
            driver.FindElement(btnLegalStrL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//label[text()='Legal Structure']/ancestor::div[1]/div//lightning-base-combobox-item/span[2]/span[text()='Merger']")).Click();

            driver.FindElement(txtOtherFormL).SendKeys("Testing");
            Thread.Sleep(5000);
            js.ExecuteScript("window.scrollTo(0,700)");
            Thread.Sleep(6000);
            // WebDriverWaits.WaitUntilEleVisible(driver, btnOpinionParties, 160);
            driver.FindElement(txtOpinionParties).SendKeys("Testing");
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnCloseL, 150);
            driver.FindElement(btnCloseL).Click();

        }

        //Validate Select PE Firm
        public string ValidateSelectPEFirm()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyL, 150);
            driver.FindElement(txtCompanyL).SendKeys("TE Studio, Ltd.");
            Thread.Sleep(4000);
            //driver.FindElement(By.XPath("//table/tbody/tr[2]/td/div/strong")).Click();
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
            catch(Exception e)
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
            IJavaScriptExecutor js = (IJavaScriptExecutor) Driver;
            js.ExecuteScript("window.scrollTo(0,750)");
            WebDriverWaits.WaitUntilEleVisible(driver, tblTargetCompL, 180);
            string name = driver.FindElement(tblTargetCompL).Displayed.ToString();
            return name;
        }
        //Check all questions on Form Of Opinion tab and validate tab on clicking Save button
        public string ValidateRelatioshipValidationPostSavingCheckboxesOnFormOfOpinion()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) Driver;
            js.ExecuteScript("window.scrollTo(0,-750)");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabForm, 160);
            driver.FindElement(tabForm).Click();
            Thread.Sleep(5000);
            //WebDriverWaits.WaitUntilEleVisible(driver, lnk1stCheck, 150);
            //driver.FindElement(lnk1stCheck).Click();
            //Thread.Sleep(6000);            
            driver.FindElement(chk1stCheck).Click();
            Thread.Sleep(6000);

            js.ExecuteScript("window.scrollTo(0,550)");
            Thread.Sleep(5000);
            driver.FindElement(chk2ndCheck).Click();
            Thread.Sleep(4000);
            driver.FindElement(chk3rdCheck).Click();
            Thread.Sleep(4000);
            driver.FindElement(chk4thCheck).Click();
            Thread.Sleep(4000);
            driver.FindElement(chk5thCheck).Click();
            Thread.Sleep(4000);
            js.ExecuteScript("window.scrollTo(0,650)");
            Thread.Sleep(5000);
            driver.FindElement(chk6thCheck).Click();
            Thread.Sleep(4000);
            js.ExecuteScript("window.scrollTo(0,750)");
            Thread.Sleep(4000);
            driver.FindElement(txtOpinionNotes).SendKeys("Testing");
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRelValidation, 170);
            driver.FindElement(lnkRelValidation).Click();
            Thread.Sleep(4000);
            js.ExecuteScript("window.scrollTo(0,-650)");
            WebDriverWaits.WaitUntilEleVisible(driver, tabRelationship, 190);
            string message = driver.FindElement(tabRelationship).Text;
            return message;
        }

        //Validate displayed validation on Relationship Tab
        public bool VerifyAllRelatioshipQValidations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            driver.FindElement(btnSaveL).Click();
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(msgRelationshipQ);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "Yes/No\r\nComplete this field.", "Yes/No\r\nComplete this field.", "Yes/No\r\nComplete this field.", "Yes/No\r\nComplete this field." };
            bool isTrue = true;

            if(expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for(int recType = 0; recType < expectedValues.Length; recType++)
            {
                if(!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }

        //Select all questions
        public bool SaveAllQuestionsAsYesAndValidateDisplayedExpTextBox()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCloseL, 150);
            driver.FindElement(btnCloseL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btn1stQues, 170);
            driver.FindElement(btn1stQues).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//flexipage-component2[3]/slot/flexipage-field-section2/div/div//div[2]/lightning-base-combobox-item[3]/span[2]/span")).Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor) Driver;
            js.ExecuteScript("window.scrollTo(0,400)");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btn2ndQues, 150);
            driver.FindElement(btn2ndQues).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//flexipage-component2[5]/slot/flexipage-field-section2/div/div//div[2]/lightning-base-combobox-item[3]/span[2]/span")).Click();
            js.ExecuteScript("window.scrollTo(0,600)");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btn3rdQues, 150);
            driver.FindElement(btn3rdQues).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//flexipage-component2[7]/slot/flexipage-field-section2/div/div//div[2]/lightning-base-combobox-item[3]/span[2]/span")).Click();
            js.ExecuteScript("window.scrollTo(0,950)");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btn4thQues, 150);
            driver.FindElement(btn4thQues).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//flexipage-component2[9]/slot/flexipage-field-section2/div/div//div[2]/lightning-base-combobox-item[3]/span[2]/span")).Click();
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(lblYes);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "*If Yes, please explain", "*If Yes, please explain", "*If Yes, please explain", "*If Yes, please explain" };
            bool isTrue = true;

            if(expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for(int recType = 0; recType < expectedValues.Length; recType++)
            {
                if(!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }

        //Select all questions
        public bool SaveAllQuestionsAsNoAndValidateDisplayedExpTextBox()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) Driver;
            js.ExecuteScript("window.scrollTo(0,-1000)");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btn1stQues, 150);
            driver.FindElement(btn1stQues).Click();
            driver.FindElement(By.XPath("//flexipage-component2[3]/slot/flexipage-field-section2/div/div//div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();

            js.ExecuteScript("window.scrollTo(0,400)");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btn2ndQues, 150);
            driver.FindElement(btn2ndQues).Click();
            driver.FindElement(By.XPath("//flexipage-component2[5]/slot/flexipage-field-section2/div/div//div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            js.ExecuteScript("window.scrollTo(0,600)");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btn3rdQues, 150);
            driver.FindElement(btn3rdQues).Click();
            driver.FindElement(By.XPath("//flexipage-component2[7]/slot/flexipage-field-section2/div/div//div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            js.ExecuteScript("window.scrollTo(0,900)");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btn4thQues, 150);
            driver.FindElement(btn4thQues).Click();
            driver.FindElement(By.XPath("//flexipage-component2[9]/slot/flexipage-field-section2/div/div//div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(lblYes);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "If Yes, please explain", "If Yes, please explain", "If Yes, please explain", "If Yes, please explain" };
            bool isTrue = true;
            try
            {
                if(expectedValues.Length != actualNamesAndDesc.Length)
                {
                    return !isTrue;
                }
                for(int recType = 0; recType < expectedValues.Length; recType++)
                {
                    if(!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                    {
                        isTrue = false;
                        break;
                    }
                }
                return isTrue;
            }
            catch(Exception e)
            {
                return !isTrue;
            }
        }

        //Validate that no validation on Relationship Tab after saving No
        public bool VerifyNoRelatioshipQValidationsUponSelectingNo()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            //driver.FindElement(btnSaveL).Click();
            //bDriverWaits.WaitUntilEleVisible(driver, btnCloseL, 150);
            //driver.FindElement(btnCloseL).Click();
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(msgRelationshipQ);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "Complete this field.", "Complete this field.", "Complete this field.", "Complete this field." };
            bool isTrue = true;

            if(expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for(int recType = 0; recType < expectedValues.Length; recType++)
            {
                if(!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return !isTrue;
        }

        //Validate the Legal Review Criteria tab upon clicking Fairness questions
        public string ValidateLegalReviewCriteriaTabUponClickingFairnessQuestions()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkFairnessQues, 150);
            driver.FindElement(lnkFairnessQues).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabLegalReview, 150);
            string name = driver.FindElement(tabLegalReview).Text;
            return name;
        }

        //Validate displayed validation on Legal Review Tab
        public bool VerifyAllLegalReviewValidations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            driver.FindElement(btnSaveL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnCloseL, 150);
            driver.FindElement(btnCloseL).Click();
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(msgRelationshipQ);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "Fairness Opinion Publicly Disclosed\r\nComplete this field.", "Fairness Relative Fairness\r\nComplete this field.", "Fairness Fairness or Terms\r\nComplete this field.", "Fairness Committee or Trustee\r\nComplete this field.", "Fairness Unusual Opinion\r\nComplete this field." };
            bool isTrue = true;

            if(expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for(int recType = 0; recType < expectedValues.Length; recType++)
            {
                if(!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }

        //Validate that no validation on Legal Review Tab after saving values
        public bool VerifyNoFairnessValidationIsDisplayedUponSelectingValue()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) Driver;
            js.ExecuteScript("window.scrollTo(0,-1500)");
            Thread.Sleep(4000);
            driver.FindElement(btnFairness1).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//label[text()='Fairness Opinion Publicly Disclosed']/ancestor::lightning-combobox//div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            js.ExecuteScript("window.scrollTo(0,1550)");
            Thread.Sleep(6000);
            driver.FindElement(btnFairness2).Click();
            driver.FindElement(By.XPath("//label[text()='Fairness Relative Fairness']/ancestor::lightning-combobox//div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            js.ExecuteScript("window.scrollTo(0,1700)");
            Thread.Sleep(6000);
            driver.FindElement(btnFairness3).Click();
            driver.FindElement(By.XPath("//label[text()='Fairness Fairness or Terms']/ancestor::lightning-combobox//div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            js.ExecuteScript("window.scrollTo(0,2350)");
            Thread.Sleep(5000);
            driver.FindElement(btnFairness6).Click();
            driver.FindElement(By.XPath("//label[text()='Fairness Multiple Conclusions']/ancestor::lightning-combobox//div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            js.ExecuteScript("window.scrollTo(0,2550)");
            Thread.Sleep(5000);
            driver.FindElement(btnFairness4).Click();
            driver.FindElement(By.XPath("//label[text()='Fairness Committee or Trustee']/ancestor::lightning-combobox//div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            js.ExecuteScript("window.scrollTo(0,2750)");
            Thread.Sleep(5000);
            driver.FindElement(btnFairness5).Click();
            driver.FindElement(By.XPath("//label[text()='Fairness Unusual Opinion']/ancestor::lightning-combobox//div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            driver.FindElement(btnSaveL).Click();

            //WebDriverWaits.WaitUntilEleVisible(driver, btnCloseL, 150);
            //driver.FindElement(btnCloseL).Click();
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(msgRelationshipQ);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "Complete this field.", "Complete this field.", "Complete this field.", "Complete this field." };
            bool isTrue = true;

            if(expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for(int recType = 0; recType < expectedValues.Length; recType++)
            {
                if(!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return !isTrue;
        }

        //Validate Other Opinion Information tab after clicking error message
        public string ValidateOtherOpinionInfoTabUponClickingOpinionSpecialQuestion()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            //driver.FindElement(btnSaveL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkOpinionSpec, 170);
            driver.FindElement(lnkOpinionSpec).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabOtherOpinion, 180);
            string name = driver.FindElement(tabOtherOpinion).Text;
            return name;
        }

        //Validate displayed validation on Other Opinion Information tab
        public string VerifyAllOtherOpinionInfoValidations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            driver.FindElement(btnSaveL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnCloseL, 150);
            driver.FindElement(btnCloseL).Click();
            string message = driver.FindElement(msgOtherOpinion).Text;
            return message;
        }

        //Validate that no validation on Other Opinion Information Tab after saving values
        public string VerifyNoValidationIsDisplayedUponSelectingValueOnOtherOpinionInfoTab()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) Driver;
            Thread.Sleep(5000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//flexipage-component2[1]/slot//iframe")));
            Thread.Sleep(5000);
            driver.FindElement(btnShareholder).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//label[contains(@id,'shareholderVoteTXT')]/ancestor::div[1]//select/option[2]")).Click();
            Thread.Sleep(4000);
            //driver.FindElement(By.XPath("//input[contains(@name,'pbtShareholderCompanies:0')]")).Click();
            driver.FindElement(btnSaveOpinion).Click();
            Thread.Sleep(7000);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(4000);
            js.ExecuteScript("window.scrollTo(0,550)");
            Thread.Sleep(5000);
            driver.FindElement(btnOpinionSpec).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//label[text()='Opinion Special Committee']/ancestor::lightning-combobox//div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            //WebDriverWaits.WaitUntilEleVisible(driver, btnCloseL, 150);
            //driver.FindElement(btnCloseL).Click();
            try
            {
                string message = driver.FindElement(msgOtherOpinion).Text;
                return message;
            }
            catch(Exception e)
            {
                return "No validation is displayed";
            }
        }

        //Validate displayed validation on Legal Review Tab
        public bool VerifyAllPendingValidations()
        {

            IJavaScriptExecutor js = (IJavaScriptExecutor) Driver;
            js.ExecuteScript("window.scrollTo(0,-800)");
            Thread.Sleep(5000);
            //WebDriverWaits.WaitUntilEleVisible(driver, btnFormCheck, 170);
            driver.FindElement(btnFormCheck).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            driver.FindElement(btnSaveL).Click();

            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(msgMandatory);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "Estimated Transaction Size (MM)", "Form of Consideration", "Legal Structure", "Opinion Parties Affiliated", "Transaction Type" };
            bool isTrue = true;

            if(expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for(int recType = 0; recType < expectedValues.Length; recType++)
            {
                if(!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return isTrue;
        }

        //Enter all mandatory fields and Validate if any validations are displayed
        public bool SaveAllMandatoryFieldsAndValidateAnyValidations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            driver.FindElement(btnSaveL).Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor) Driver;

            WebDriverWaits.WaitUntilEleVisible(driver, lnkTxnType, 150);
            driver.FindElement(lnkTxnType).Click();
            driver.FindElement(btnTransType).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//label[text()='Transaction Type']/ancestor::div[1]/div//lightning-base-combobox-item/span[2]/span[text()='Buy Side']")).Click();
            driver.FindElement(txtEstTxnSize).SendKeys("10");
            driver.FindElement(btnLegalStrL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//label[text()='Legal Structure']/ancestor::div[1]/div//lightning-base-combobox-item/span[2]/span[text()='Merger']")).Click();

            driver.FindElement(valAvailable).Click();
            driver.FindElement(btnMove).Click();

            js.ExecuteScript("window.scrollTo(0,750)");
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnOpinionParties, 160);
            driver.FindElement(btnOpinionParties).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//flexipage-component2[2]/slot//flexipage-component2[4]//record_flexipage-record-field//lightning-base-combobox-item[3]/span[2]/span")).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            driver.FindElement(btnSaveL).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, lnkTxnType, 150);
            driver.FindElement(lnkTxnType).Click();
            Thread.Sleep(5000);

            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(msgMandatory);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "Transaction Type", "Legal Structure", "Estimated Transaction Size (MM)", "Form of Consideration" };
            bool isTrue = true;

            if(expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for(int recType = 0; recType < expectedValues.Length; recType++)
            {
                if(!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }
            return !isTrue;
        }

        //Validate Submit FEIS Form button
        public string ValidateSubmitFEISFormButton()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) Driver;
            js.ExecuteScript("window.scrollTo(0,-800)");
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkFormCheck, 170);
            driver.FindElement(lnkFormCheck).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnFormCheck).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitFEIS, 100);
            string value = driver.FindElement(btnSubmitFEIS).Text;
            return value;
        }

        //Validate email format after clicking FEIS Form button
        public string ValidateEmailFormatAfterClickingFEISFormButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitFEIS, 100);
            driver.FindElement(btnSubmitFEIS).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//div[1]/div/div/div/force-aloha-page/div/iframe")));
            Thread.Sleep(6000);
            string value = driver.FindElement(lblSendEmail).Text;
            return value;
        }

        //Validate that the user is able to send an email
        public string ValidateSendEmailFunctionality()
        {
            driver.SwitchTo().ParentFrame();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtTo, 100);
            driver.FindElement(txtTo).Clear();
            Thread.Sleep(5000);
            driver.FindElement(txtTo).SendKeys("Sonika Goyal");
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSendEmail, 150);
            driver.FindElement(btnSendEmail).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().DefaultContent();
            try
            {
                string button = driver.FindElement(btnSubmitFEIS).Displayed.ToString();
                return button;
            }
            catch(Exception)
            {
                return "Button is not displayed";
            }
        }

        //Validate of form is editable post submission
        public string ValidateIfFormIsEditablePostSubmission()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkFormCheck, 170);
            driver.FindElement(lnkFormCheck).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnFormCheck).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgPostSubmission, 100);
            string value = driver.FindElement(msgPostSubmission).Text;
            driver.FindElement(btnCloseL).Click();
            return value;
        }

        //Validate Review tab post submission of form
        public string ValidateReviewTabPostSubmissionFVA()
        {
            Thread.Sleep(4000);
            driver.FindElement(btnMore).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabReview, 100);
            driver.FindElement(tabReview).Click();

            Thread.Sleep(5000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnCancelL, 80);
                driver.FindElement(btnCancelL).Click();
                return "Review tab is not accessible";
            }
            catch(Exception)
            {
                string value = driver.FindElement(lblReviewed).Text;
                return value;
            }
        }
        //Validate Review tab post submission of form
        public string ValidateReviewTabPostSubmissionCAO()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnMoreCAO, 110);
            driver.FindElement(btnMoreCAO).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabReview, 100);
            driver.FindElement(tabReview).Click();

            Thread.Sleep(5000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnCancelL, 80);
                driver.FindElement(btnCancelL).Click();
                return "Review tab is not accessible";
            }
            catch(Exception)
            {
                string value = driver.FindElement(lblReviewed).Text;
                return value;
            }


        }

    }
}