using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Threading;

namespace SF_Automation.Pages.Opportunity
{
    class NBCForm : BaseClass
    {
        By OppName = By.CssSelector("span[id*='id40']");
        By clientComp = By.CssSelector("span[id*='id44']");
        By subjectComp = By.CssSelector("span[id*='id47']");
        By jobType = By.CssSelector("span[id*='id60']");
        By btnSubmit = By.CssSelector("input[id*='j_id33:btnSubmitForReview']");
        By errorList = By.CssSelector("#j_id0\\:NBCForm\\:j_id2\\:j_id3\\:j_id4\\:0\\:j_id5\\:j_id6\\:j_id18 > ul");
        By btnCancel = By.CssSelector("input[value='Cancel Submission']");
        By checkToggleTabs = By.Id("toggleTabs");
        By tabList = By.Id("tabsList");
        By comboFinancialOpinion = By.CssSelector("select[id*='MajoritySale']");
        By checkConfirm = By.CssSelector("input[id*='HeadApproval']");
        By txtTranOverview = By.CssSelector("textarea[name*='id78']");
        By txtHLCompPG = By.CssSelector("textarea[name*='id75']");
        By txtCurrentStatus = By.CssSelector("textarea[name*='id80']");
        By txtCompDesc = By.CssSelector("textarea[name*='id82']");
        By comboCrossBorder = By.CssSelector("select[name*='InternationalAngle']");
        By comboAsiaAngle = By.CssSelector("select[name*='AsiaAngle']");
        By comboRealEstate = By.CssSelector("select[name*='RealEstateAngle']");
        By txtOwnershipStr = By.CssSelector("textarea[name*='id104']");
        By txtTotalDebt = By.CssSelector("input[name*='TotalDebt']");
        By comboAudit = By.CssSelector("select[name*='FinAudit01']");
        By txtEstVal = By.CssSelector("input[name*='estValu']");
        By txtValExp = By.CssSelector("textarea[name *= 'id153']");
        By txtRiskFactors = By.CssSelector("textarea[name*='id157']");
        By txtEstFee = By.CssSelector("input[name*='estMinFee']");
        By txtFeeStr = By.CssSelector("textarea[name*= 'id247']");
        By comboLockUps = By.CssSelector("select[name*= 'id251']");
        By comboReferralFee = By.CssSelector("select[name*= 'id256']");
        By comboPitch = By.CssSelector("select[name*= 'Pitch00']");
        By comboClient = By.CssSelector("select[id='j_id0:NBCForm:j_id31:j_id260:Exist']");
        By txtHLComp = By.CssSelector("textarea[name*= 'id273']");
        By comboExistingRel = By.CssSelector("select[name*= 'ExistingRel']");
        By comboTASAssist = By.CssSelector("select[name*= 'TAS00']");
        By txtOutsideCouncil = By.CssSelector("textarea[name*= 'OutsideCouncil']");
        By comboCapMkt = By.CssSelector("select[name*= 'id311']");
        By txtSum = By.CssSelector("textarea[name*= 'id314']");
        By comboFairness = By.CssSelector("select[name*= 'Fairness']");
        By comboResList = By.CssSelector("select[name*= 'RestrictedList']");
        By comboCCStatus = By.CssSelector("select[name*= 'Conflicts2a']");
        By comboCCStatus1 = By.CssSelector("select[name*= 'Conflicts3a']");
        By comboCCStatus2 = By.CssSelector("select[name*= 'Conflicts35a']");
        By comboCCStatus3 = By.CssSelector("select[name*= 'Conflicts4a']");
        By comboCCStatus4 = By.CssSelector("select[name*= 'Conflicts5a']");
        By titleEmailPage = By.CssSelector("h2[class='mainTitle']");
        By valEmailOppName = By.CssSelector("body[id*='Body_rta_body'] > span:nth-child(10) > span");
        By btnCancelEmail = By.CssSelector("input[value='Cancel']");
        By btnReturntoOpp = By.CssSelector("input[value*='Return to Opportunity']");
        By btnReturntoOppCFUser = By.CssSelector("span[id*=':j_id34'] > a");
        By statusCC = By.CssSelector("div[id*='tabs-6']>div>div[class='pbSubsection']>table>tbody>tr:nth-child(4)>td>span>table>tbody>tr>td:nth-child(2)>span");
        By valFinOption = By.CssSelector("select[id*='MajoritySale']>option[selected='selected']");
        By btnSave = By.CssSelector("input[value = 'Save NBC']");
        By txtComment = By.CssSelector("span[id*='MajoritySale01']");
        By tblSuggestedFee = By.CssSelector("span[id*='j_id163'] >table");
        By btnEUOverride = By.CssSelector("span[id*='1:j_id33'] > input[id*='euOverride']");
        By txtEUComment = By.CssSelector("tr[id *= 'MajoritySale02'] > td");
        By tblEUFee = By.CssSelector("span[id*='j_id218'] > table");
        By lblReview = By.CssSelector("div[id*='tabs-7']>div>div>h3");
        By comboGrade = By.CssSelector("select[id*='reviewGrade']");
        By valGrade = By.CssSelector("select[id*='reviewGrade']>option[selected='selected']");
        By txtNotes = By.CssSelector("textarea[id*='reviewNotes']");
        By txtDateSubmitted = By.CssSelector("input[id*='dateSubmit']");
        By txtReason = By.CssSelector("textarea[id*='reasonWonLost']");
        By txtFeeDiff = By.CssSelector("textarea[id*='feeDiff']");
        By btnPDFView = By.XPath("//a[contains(text(),'PDF View')]");
        By btnAttachFile = By.CssSelector("button[type='button']");
        By btnAddFinancials = By.CssSelector("input[id*='newFinancials']");
        //Validate Opp Name
        public string ValidateOppName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, OppName, 50);
            string valOpp = driver.FindElement(OppName).Text;
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
        //Validate JobType
        public string ValidateJobType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, jobType);
            string valJobType = driver.FindElement(jobType).Text;
            return valJobType;
        }

        //Fetch validations for mandatory fields
        public string GetFieldsValidations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmit, 80);
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
            WebDriverWaits.WaitUntilEleVisible(driver, comboFinancialOpinion, 90);
            driver.FindElement(comboFinancialOpinion).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 2));
            driver.FindElement(checkConfirm).Click();

            //Overview and Financials
            driver.FindElement(txtTranOverview).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 3));
            driver.FindElement(txtCurrentStatus).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 4));
            driver.FindElement(txtCompDesc).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 5));
            driver.FindElement(comboCrossBorder).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 6));
            driver.FindElement(comboAsiaAngle).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 7));
            driver.FindElement(comboRealEstate).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 7));
            driver.FindElement(txtOwnershipStr).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 8));
            driver.FindElement(txtTotalDebt).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 9));
            driver.FindElement(comboAudit).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 10));
            driver.FindElement(txtEstVal).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 11));
            driver.FindElement(txtValExp).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 12));
            driver.FindElement(txtRiskFactors).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 13));

            //Fees
            driver.FindElement(txtEstFee).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 14));
            driver.FindElement(txtFeeStr).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 15));
            driver.FindElement(comboLockUps).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 16));
            driver.FindElement(comboReferralFee).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 17));

            //Pre-Pitch
            driver.FindElement(comboPitch).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 18));
            driver.FindElement(comboClient).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 19));
            driver.FindElement(txtHLComp).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 20));
            driver.FindElement(comboExistingRel).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 21));
            driver.FindElement(comboTASAssist).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 22));
            driver.FindElement(txtOutsideCouncil).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 23));

            //Financing Checklist            
            driver.FindElement(comboCapMkt).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 24));
            driver.FindElement(txtSum).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 25));

            //Fairness Checklist
            driver.FindElement(comboFairness).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 26));
            Console.WriteLine("comboFairness");

            //Administrative
            driver.FindElement(comboResList).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 27));
            //driver.FindElement(comboCCStatus).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 28));
            driver.FindElement(comboCCStatus1).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 28));
            driver.FindElement(comboCCStatus2).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 29));
            driver.FindElement(comboCCStatus3).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 30));
            driver.FindElement(comboCCStatus4).SendKeys(ReadExcelData.ReadData(excelPath, "NBCForm", 31));

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

        //Validate Add Financials button
        public string ValidateAddFinancialsButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddFinancials);
            string value = driver.FindElement(btnAddFinancials).Enabled.ToString();
            return value;
        }
    }
}


