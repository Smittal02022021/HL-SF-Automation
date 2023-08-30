using Microsoft.Office.Interop.Excel;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SF_Automation.Pages
{
    class AddOpportunityPage : BaseClass
    {
        By txtOpportunityName = By.Id("Name");
        By txtClient = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000D7zoC']");
        By txtSubject = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000D80OZ']");
        By comboJobType = By.CssSelector("select[id*= 'hWW']");
        By comboIndustryGroup = By.CssSelector("select[name*= 'VT3']");
        By comboSector = By.CssSelector("select[name*='PI']");
        By comboAdditionalClient = By.CssSelector("select[name*='FmBza']");
        By comboAdditionalSubject = By.CssSelector("select[name*='Bzb']");
        By comboReferralType = By.CssSelector("select[name*='uS']");
        By comboNonPublicInfo = By.CssSelector("select[name*='Bzn']");
        By comboBeneficialOwner = By.CssSelector("select[name*='HERR2']");
        By comboPrimaryOffice = By.CssSelector("select[name*='VIA']");
        By txtLegalEntity = By.XPath("//span[@class='lookupInput']/input[@name='CF00N5A00000M0eg5']");
        By comboDisclosureStatus = By.CssSelector("select[name*='HaP']");
        By btnSave = By.CssSelector("input[value=' Save ']");
        By comboClientOwnership = By.CssSelector("select[name*='M0d2T']");
        By comboSubjectOwnership = By.CssSelector("select[name*='M0d2U']");
        By comboRecordType = By.CssSelector("select[name='00Ni000000D8hW2']");
        By txtTotalDebt = By.CssSelector("input[name*='DwfqW']");
        By comboEMEAInitiatives = By.CssSelector("span>select[name*='MR']");
        By txtFee = By.CssSelector("input[name*='FmBzg']");
        By labelWomenLed = By.CssSelector("div:nth-child(23) > table > tbody > tr:nth-child(4) > td:nth-child(3) > label");
        By labelWomenLedFVA = By.CssSelector("div:nth-child(25) > table > tbody > tr:nth-child(3) > td:nth-child(3) > label");
        By labelWomenLedFR = By.CssSelector("div:nth-child(21) > table > tbody > tr:nth-child(4) > td:nth-child(3) > label");
        By labelAdmSection = By.CssSelector("div[id='head_11_ep'] > h3");
        By labelAdmSectionFVA = By.CssSelector("div[id = 'head_12_ep'] > h3");
        By labelAdmSectionFR = By.CssSelector("div[id = 'head_10_ep'] > h3");
        By comboWomenLed = By.CssSelector("select[id*='NgW']>option");
        By msgFee = By.XPath("//*[@id='ep']/div[2]/div[17]/table/tbody/tr[3]/td[2]/div/div[2]");        
By comboStage = By.CssSelector("select[id*='00Ni000000D80OA']");       
By txtErrorMessages = By.CssSelector("div[id*='errorDiv_ep']");



        //Lightning
        By txtOpportunityNameL = By.XPath("//input[@name= 'Name']");
        By txtClientL = By.XPath("//label[text()='Client']/ancestor::lightning-grouped-combobox/div[1]/div/lightning-base-combobox/div/div/div/div/div/input");
        By txtSubjectL = By.XPath("//label[text()='Subject']/ancestor::lightning-grouped-combobox/div[1]/div/lightning-base-combobox/div/div/div/div/div/input");
        By btnJobTypeL = By.XPath("//button[@aria-label='Job Type, --None--']");
        By btnIGL = By.XPath("//button[@aria-label='Industry Group, --None--']");
        By comboSectorL = By.XPath("//button[@aria-label='Sector, --None--']");
        By comboPrimaryOfficeL = By.XPath("//button[@aria-label='Primary Office, --None--']");
        By txtLegalEntitiesL = By.XPath("//input[@placeholder='Search Legal Entities...']");
        By comboRefTypeL = By.XPath("//button[@aria-label='Referral Type, --None--']");
        By comboAddClientL = By.XPath("//button[@aria-label='Additional Client, --None--']");
        By comboAddSubjectL = By.XPath("//button[@aria-label='Additional Subject, --None--']");
        By comboBenOwnerL = By.XPath("//button[@aria-label='Beneficial Owner & Control Person form?, --None--']");
        By comboHLMaterialL = By.XPath("//button[@aria-label='Does HL Have Material Non-Public Info?, --None--']");
        By btnSaveL = By.XPath("//button[text()='Save']");
        By btnConfAgreeL = By.XPath("//button[@aria-label='Confidentiality Agreement, --None--']");
        By txtObjective = By.XPath("//label[text()='Objective']/ancestor::lightning-textarea/div[1]/textarea");
        By btnClose = By.XPath("//records-record-edit-error-header/lightning-button-icon/button/lightning-primitive-icon");
        By msgOppName = By.XPath("//label[text()='Opportunity Name']/ancestor::lightning-input/div[2]");
        By msgClient = By.XPath("//label[text()='Client']/ancestor::lightning-grouped-combobox/div[2]");
        By msgSubject =By.XPath("//label[text()='Subject']/ancestor::lightning-grouped-combobox/div[2]");
        By msgJobType= By.XPath("//label[text()='Job Type']/ancestor::lightning-combobox/div[2]");
        By msgIG = By.XPath("//label[text()='Industry Group']/ancestor::lightning-combobox/div[2]");
        By msgPrimaryOff = By.XPath("//label[text()='Primary Office']/ancestor::lightning-combobox/div[2]");
        By msgLegalEntity = By.XPath("//label[text()='Legal Entity']/ancestor::lightning-grouped-combobox/div[2]");
        By msgRefType = By.XPath("//label[text()='Referral Type']/ancestor::lightning-combobox/div[2]");
        By msgAddClient = By.XPath("//label[text()='Additional Client']/ancestor::lightning-combobox/div[2]");
        By msgAddSub = By.XPath("//label[text()='Additional Subject']/ancestor::lightning-combobox/div[2]");
        By msgBenOwner = By.XPath("//label[text()='Beneficial Owner & Control Person form?']/ancestor::lightning-combobox/div[2]");
        By msgDoesHL = By.XPath("//label[text()='Does HL Have Material Non-Public Info?']/ancestor::lightning-combobox/div[2]");
        By txtStaff = By.XPath("//input[@placeholder='Begin Typing Name...']");
        By titleHLIntTeam = By.XPath("//h1/b");
        By msgInitiator = By.XPath("//label[@class='warning']");
        By msgRolesL = By.XPath("//table/tbody/tr[1]/td[2]/div");
        By listStaff = By.XPath("/html/body/ul");
        By btnReturnToOppor = By.CssSelector("input[value='Return To Opportunity']");
        By checkInitiator = By.CssSelector("input[name*='internalTeam:j_id88:0:j_id90']");
        By checkEditIniatiator = By.CssSelector("input[name*='internalTeam:j_id39:0:j_id41:0']");
        By checkSeller = By.CssSelector("input[name*='internalTeam:j_id88:1:j_id90']");
        By btnSaveDealTeam = By.CssSelector("input[value='Save']");
        By tabInfo = By.XPath("//a[text()='Info']");
        By tabOpp = By.XPath("//span[text()='Opportunities']");
        By txtSearch = By.XPath("//input[@placeholder='Search this list...']");
        By btnRefresh = By.XPath("//button[@title='Refresh']");
        By btnRecentlyViewed = By.XPath("//div/div/div[2]/div/button");
        By btnOppNavigation = By.XPath("//div/button[@aria-label='Show Navigation Menu']");
        By valRec1st = By.XPath("//table/tbody/tr[1]/th/span/a");
        By valRec3rd = By.XPath("//table/tbody/tr[5]/th/span/a");
        By tabInternalTeamL = By.XPath("//lightning-tab-bar/ul/li/a[text()='Internal Team']");
        By btnModifyRolesL = By.XPath("//div[1]/table/tbody/tr/td[2]/a");
        By labelOpportunityEdit = By.CssSelector("h2[class='mainTitle']");
        By btnCancel = By.CssSelector("td[class='pbButton'] > input[value='Cancel']");
        By selectedLOBvalue = By.CssSelector("select[id='00Ni000000D8hW2']");       
        By comboLegalAdvisor = By.CssSelector("select[id*='00N5A00000M4yQB']");
By txtTotalAntRev = By.CssSelector("input[id*='00N6e00000H0zNU']");
        By comboSuccessProb = By.CssSelector("select[id*='00N5A00000M4yXq']");
        By txtEstTranscSize = By.CssSelector("input[id*='00Ni000000D80P4']");
        By txtRetainerFee = By.CssSelector("input[id*='00Ni000000DwTdF']");
        By txtMnthFee = By.CssSelector("input[id*='00Ni000000FmBzi']");
        By txtContingFee = By.CssSelector("input[id*='00Ni000000FkGE9']");
        By txtEstCloseDate = By.CssSelector("input[id*='00Ni000000FnLTw']");
        By comboReferType = By.CssSelector("select[id*='00Ni000000FF5uS']");
        By chkboxNBC = By.CssSelector("input[id*='00Ni000000FmBzh']");
        By chkboxByPassConflictCheck = By.CssSelector("input[id*='00N3100000Gb1CJ']");


        public string AddOpportunities(string type,string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Console.WriteLine("path:" + excelPath);

            //--------------------------Enter Opportunity details-----------------------------
            //Information Section           
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityName, 40);
            string valOpportunity = CustomFunctions.RandomValue();

            driver.FindElement(txtOpportunityName).SendKeys(valOpportunity);
            //driver.FindElement(txtClient).SendKeys(ReadJSONData.data.addOpportunityDetails.client);            
            driver.FindElement(txtClient).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 1));
            driver.FindElement(txtSubject).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType,80);
            driver.FindElement(comboJobType).SendKeys(type);
            driver.FindElement(comboIndustryGroup).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 4));
            driver.FindElement(comboSector).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 5));
            driver.FindElement(comboClientOwnership).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 18));
            driver.FindElement(comboSubjectOwnership).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 19));
            Console.WriteLine("Subject");
            //Additional Client/Subject
            driver.FindElement(comboAdditionalClient).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 6));
            driver.FindElement(comboAdditionalSubject).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 7));

            //Referral Information
            driver.FindElement(comboReferralType).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 8));

            //Compliance Section
            driver.FindElement(comboNonPublicInfo).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 9));
            driver.FindElement(comboBeneficialOwner).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 10));
            Console.WriteLine("owner");
            //Administration Section
            driver.FindElement(comboPrimaryOffice).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 11));
            driver.FindElement(txtLegalEntity).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 12));
            driver.FindElement(comboDisclosureStatus).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 13));

            if (driver.FindElement(comboRecordType).Text.Contains("FR"))
            {
                Console.WriteLine("in if");
                driver.FindElement(txtTotalDebt).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(comboEMEAInitiatives).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 29));
            }

           else if (driver.FindElement(comboRecordType).Text.Contains("FVA"))
            {
                Console.WriteLine("in else if");
                driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
            }
            else
            {
                Console.WriteLine("CF types ");
            }
            //Click Save button                           
            driver.FindElement(btnSave).Click();            
            return valOpportunity;
        }

        public string AddOpportunitiesForFVA(string type, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Console.WriteLine("path:" + excelPath);

            //--------------------------Enter Opportunity details-----------------------------
            //Information Section           
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityName, 40);
            string valOpportunity = CustomFunctions.RandomValue();

            driver.FindElement(txtOpportunityName).SendKeys(valOpportunity);
            //driver.FindElement(txtClient).SendKeys(ReadJSONData.data.addOpportunityDetails.client);            
            driver.FindElement(txtClient).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 1));
            driver.FindElement(txtSubject).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType, 80);
            driver.FindElement(comboJobType).SendKeys(type);
            driver.FindElement(comboIndustryGroup).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 4));
            driver.FindElement(comboSector).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 5));
            driver.FindElement(comboClientOwnership).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 18));
            driver.FindElement(comboSubjectOwnership).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 19));
            Console.WriteLine("Subject");
            //Additional Client/Subject
            driver.FindElement(comboAdditionalClient).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 6));
            driver.FindElement(comboAdditionalSubject).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 7));

            //Referral Information
            driver.FindElement(comboReferralType).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 8));

            //Compliance Section
            driver.FindElement(comboNonPublicInfo).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 9));
            driver.FindElement(comboBeneficialOwner).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 10));
            Console.WriteLine("owner");
            //Administration Section
            driver.FindElement(comboPrimaryOffice).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 11));
            driver.FindElement(txtLegalEntity).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 12));
            driver.FindElement(comboDisclosureStatus).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 13));

            if (driver.FindElement(comboRecordType).Text.Contains("FR"))
            {
                Console.WriteLine("in if");
                driver.FindElement(txtTotalDebt).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(comboEMEAInitiatives).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 29));
            }

            else if (driver.FindElement(comboRecordType).Text.Contains("FVA"))
            {
                Console.WriteLine("in else if");
                driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
            }
            else
            {
                Console.WriteLine("CF types ");
            }
            //Click Save button
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgFee, 80);
            string message = driver.FindElement(msgFee).Text;
            driver.FindElement(txtFee).Clear();
            driver.FindElement(txtFee).SendKeys("10000");
            driver.FindElement(btnSave).Click();
            return message;
        }

        //Validate Women Led field
        public string ValidateWomenLedField(string recType)
        {
            if (recType.Equals("CF"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenLed);
                string fieldName = driver.FindElement(labelWomenLed).Text;
                return fieldName;
            }
            else if(recType.Equals("FVA"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenLedFVA);
                string fieldName = driver.FindElement(labelWomenLedFVA).Text;
                return fieldName;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenLedFR);
                string fieldName = driver.FindElement(labelWomenLedFR).Text;
                return fieldName;
            }
        }

        //Get Administration section
        public string GetAdminSectionName(string recType)
        {
            if (recType.Equals("CF"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelAdmSection);
                string secName = driver.FindElement(labelAdmSection).Text;
                return secName;
            }
            else if (recType.Equals("FVA"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelAdmSectionFVA);
                string secName = driver.FindElement(labelAdmSectionFVA).Text;
                return secName;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelAdmSectionFR);
                string secName = driver.FindElement(labelAdmSectionFR).Text;
                return secName;
            }
        }

        //Validate the values of Women Led field

        public bool VerifyWomenLedValues()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(comboWomenLed);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            //string[] expectedValue = {"CF", "Conflicts Check", "FAS","FR", "HL Internal Opportunity", "OPP DEL","SC"};
            string[] expectedValue = { "--None--", "Yes", "No"};
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

        public string AddOpportunitiesLightning(string type, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Console.WriteLine("path:" + excelPath);

            //--------------------------Enter Opportunity details-----------------------------
            //Information Section           
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityNameL, 240);
            string valOpportunity = CustomFunctions.RandomValue();

            driver.FindElement(txtOpportunityNameL).SendKeys(valOpportunity);                      
            driver.FindElement(txtClientL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 1));
            driver.FindElement(txtClientL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//lightning-grouped-combobox/div[1]/div/lightning-base-combobox/div/div/div[2]/ul")).Click();
            driver.FindElement(txtSubjectL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 2));
            driver.FindElement(txtSubjectL).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//flexipage-field[3]/slot/record_flexipage-record-field/div/div/slot/records-record-layout-lookup/lightning-lookup/lightning-lookup-desktop/lightning-grouped-combobox/div[1]/div/lightning-base-combobox/div/div/div[2]/ul")).Click();
            Thread.Sleep(4000);

            //Select Job Type
            WebDriverWaits.WaitUntilEleVisible(driver, btnJobTypeL, 80);
            driver.FindElement(btnJobTypeL).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//lightning-base-combobox-item/span[2]/span[text()='"+type+"']")).Click();

            //Enter objective
            driver.FindElement(txtObjective).SendKeys("Testing");

            //Select IG
            Thread.Sleep(3000);
            //IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            //js.ExecuteScript("arguments[0].click();", driver.FindElement(btnIGL));            
            string valIG = ReadExcelData.ReadData(excelPath, "AddOpportunity", 4);
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnIGL, 180);
            driver.FindElement(btnIGL).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//lightning-base-combobox-item/span[2]/span[text()='" + valIG + "']")).Click();

           
            //Select sector
            string valSector = ReadExcelData.ReadData(excelPath, "AddOpportunity", 5);
            driver.FindElement(comboSectorL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 5));
            driver.FindElement(By.XPath("//lightning-base-combobox-item/span[2]/span[text()='" + valSector + "']")).Click();

            //Select Primary Office
            string valPO = ReadExcelData.ReadData(excelPath, "AddOpportunity", 11);
            driver.FindElement(comboPrimaryOfficeL).SendKeys(valPO);
            driver.FindElement(By.XPath("//lightning-base-combobox-item/span[2]/span[text()='" + valPO + "']")).Click();

            //Select Legal Entity
            string valEntity = ReadExcelData.ReadData(excelPath, "AddOpportunity", 12);
            driver.FindElement(txtLegalEntitiesL).SendKeys(valEntity);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/slot/records-record-layout-lookup/lightning-lookup/lightning-lookup-desktop/lightning-grouped-combobox/div[1]/div/lightning-base-combobox/div/div/div[2]/ul")).Click();

            //Select Referral Type  
            string valRefType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 8);
            driver.FindElement(comboRefTypeL).SendKeys(valRefType);
            driver.FindElement(By.XPath("//lightning-base-combobox-item/span[2]/span[text()='" + valRefType + "']")).Click();

            //Select Additional Client
            driver.FindElement(comboAddClientL).SendKeys("No");
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//lightning-base-combobox-item/span[2]/span[text()='No']")).Click();

            //Select Additional Subject
            driver.FindElement(comboAddSubjectL).SendKeys("No");
            Thread.Sleep(7000);
            driver.FindElement(By.XPath("//flexipage-column2[2]/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/div[1]/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[3]/span[2]/span")).Click();

            //Select Beneficial Owner
            string valBenOwner = ReadExcelData.ReadData(excelPath, "AddOpportunity", 10);
            driver.FindElement(comboBenOwnerL).SendKeys(valBenOwner);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//flexipage-component2[10]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/div[1]/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[3]/span[2]/span[text()='" + valBenOwner + "']")).Click();

            //Select Does HL have material            
            driver.FindElement(comboHLMaterialL).SendKeys(valBenOwner);
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//flexipage-component2[10]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/div[1]/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();

            //Select Conf Agreement
            string valConf = ReadExcelData.ReadData(excelPath, "AddOpportunity", 23);
            Thread.Sleep(4000);
            //driver.FindElement(btnConfAgreeL).Click();
            driver.FindElement(btnConfAgreeL).SendKeys(valConf);
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//lightning-base-combobox-item/span[2]/span[text()='" + valConf + "']")).Click();

            //Click Save button                           
            driver.FindElement(btnSaveL).Click();

            //
            return valOpportunity;
        }
        //Validate mandatory field validations
        public string ValidateMandatoryFieldsValidations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 100);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose, 80);
            driver.FindElement(btnClose).Click();
            string validation = driver.FindElement(msgOppName).Text;
            return validation;
        }
        //Validate mandatory validation of Client
        public string ValidateMandatoryValidationOfClient ()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, msgClient, 80);           
            string validation = driver.FindElement(msgClient).Text;
            return validation;
        }

        //Validate mandatory validation of Subject
        public string ValidateMandatoryValidationOfSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgSubject, 80);
            string validation = driver.FindElement(msgSubject).Text;
            return validation;
        }
        //Validate mandatory validation of Job Type
        public string ValidateMandatoryValidationOfJobType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgJobType, 80);
            string validation = driver.FindElement(msgJobType).Text;
            return validation;
        }

        //Validate mandatory validation of Industry Group
        public string ValidateMandatoryValidationOfIG()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgIG, 80);
            string validation = driver.FindElement(msgIG).Text;
            return validation;
        }

        //Validate mandatory validation of Primary Office
        public string ValidateMandatoryValidationOfPrimaryOffice()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgPrimaryOff, 80);
            string validation = driver.FindElement(msgPrimaryOff).Text;
            return validation;
        }

        //Validate mandatory validation of Legal Entity
        public string ValidateMandatoryValidationOfLegalEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgLegalEntity, 80);
            string validation = driver.FindElement(msgLegalEntity).Text;
            return validation;
        }

        //Validate mandatory validation of Referral Type
        public string ValidateMandatoryValidationOfRefType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgRefType, 80);
            string validation = driver.FindElement(msgRefType).Text;
            return validation;
        }

        //Validate mandatory validation of Additional Client
        public string ValidateMandatoryValidationOfAdditionalClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgAddClient, 80);
            string validation = driver.FindElement(msgAddClient).Text;
            return validation;
        }

        //Validate mandatory validation of Additional Subject
        public string ValidateMandatoryValidationOfAdditionalSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgAddSub, 80);
            string validation = driver.FindElement(msgAddSub).Text;
            return validation;
        }

        //Validate mandatory validation of Beneficial Owner & Control Person form?
        public string ValidateMandatoryValidationOfBeneficialOwner()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgBenOwner, 80);
            string validation = driver.FindElement(msgBenOwner).Text;
            return validation;
        }

        //Validate mandatory validation of Does HL Have Material Non-Public Info?
        public string ValidateMandatoryValidationOfDoesHL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgDoesHL, 80);
            string validation = driver.FindElement(msgDoesHL).Text;
            return validation;
        }

        //Validate HL Internal Team title
        public string ValidateHLInternalTeamPage()
        {
            Thread.Sleep(8000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//div[1]/div/div/div/force-aloha-page/div/iframe")));
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, titleHLIntTeam, 80);
            string title = driver.FindElement(titleHLIntTeam).Text;
            return title;
        }

        //Validate Initiator message
        public string ValidateInitiatorMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgInitiator, 80);
            string title = driver.FindElement(msgInitiator).Text;
            return title;
        }

        //Validate Roles message
        public string ValidateRolesValidation(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            Console.WriteLine("Entered staff function");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valStaff = ReadExcelData.ReadData(excelPath, "AddOpportunity", 14);
            Console.WriteLine("Before entering Staff");
            
            WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 120);
            driver.FindElement(txtStaff).SendKeys(valStaff);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valStaff);
            Thread.Sleep(2000);
            driver.FindElement(btnSaveDealTeam).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgRolesL, 80);
            string title = driver.FindElement(msgRolesL).Text;
            return title;
        }

        //Validate User is redirected to Internal team page if Initiator is not selected
        public string ValidateUserIsRedirectedToHLInternalPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkSeller, 240);
            driver.FindElement(checkSeller).Click();
            driver.FindElement(btnSaveDealTeam).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, tabOpp, 260);
            driver.FindElement(tabOpp).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, valRec1st, 240);
            driver.FindElement(valRec1st).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//div[1]/div/div/div/force-aloha-page/div/iframe")));
             WebDriverWaits.WaitUntilEleVisible(driver, titleHLIntTeam, 250);
            string title = driver.FindElement(titleHLIntTeam).Text;
            return title;
        }

        //Validate User is redirected to Internal team page if Initiator is not selected
        public string ValidateUserIsRedirectedToHLInternalPageWhenOppOpenedFromGlobalSearch(string name)
        {
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, tabOpp, 260);
            driver.FindElement(tabOpp).Click();
            Thread.Sleep(3000);           
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearch, 150);
            driver.FindElement(txtSearch).SendKeys(name);
            Thread.Sleep(5000);
            driver.FindElement(btnRefresh).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valRec1st, 240);
            driver.FindElement(valRec1st).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//div[1]/div/div/div/force-aloha-page/div/iframe")));

            WebDriverWaits.WaitUntilEleVisible(driver, titleHLIntTeam, 240);
            string title = driver.FindElement(titleHLIntTeam).Text;
            return title;
        }

        //Validate User is redirected to Internal team page if Initiator is not selected
        public string ValidateUserIsRedirectedToHLInternalPageFromMyActiveOpp()
        {           
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, tabOpp, 260);
            driver.FindElement(tabOpp).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnRecentlyViewed, 350);
            driver.FindElement(btnRecentlyViewed).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div[1]/div/ul/li[5]")).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valRec3rd, 240);
            driver.FindElement(valRec3rd).Click();
            Thread.Sleep(8000);            
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//div[1]/div/div/div/force-aloha-page/div/iframe")));
            WebDriverWaits.WaitUntilEleVisible(driver, titleHLIntTeam, 250);
            string title = driver.FindElement(titleHLIntTeam).Text.Substring(17,16);
            return title;
        }


        //Validate Return To Opp button is dislayed if Initiator is selected
        public string ValidateReturnToOppButtonWhenInitiatorIsSelected()
        {           
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, checkEditIniatiator, 350);
            driver.FindElement(checkEditIniatiator).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDealTeam, 350);
            driver.FindElement(btnSaveDealTeam).Click();
            Thread.Sleep(6000);           
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOppor, 250);
            string title = driver.FindElement(btnReturnToOppor).GetAttribute("value");
            return title;
        }

        //Validate User is not redirected to Internal team page if Initiator is selected
        public string ValidatePageWhenInitiatorRoleIsSelected()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, tabOpp, 260);
            driver.FindElement(tabOpp).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnRecentlyViewed, 350);
            driver.FindElement(btnRecentlyViewed).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div[1]/div/ul/li[9]")).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valRec1st, 240);
            driver.FindElement(valRec1st).Click();
            Thread.Sleep(8000);
            //driver.SwitchTo().Frame(driver.FindElement(By.XPath("//div[1]/div/div/div/force-aloha-page/div/iframe")));
            WebDriverWaits.WaitUntilEleVisible(driver, tabInfo, 250);
            string title = driver.FindElement(tabInfo).Text;
            Thread.Sleep(4000);                     
             return title;
        }

        ////Validate User is redirected to Internal team page if Initiator is not selected
        //public string ValidateUserIsRedirectedToHLInternalPageWhenInitiatorIsSelected()
        //{
        //    WebDriverWaits.WaitUntilEleVisible(driver, btnOppNavigation, 260);
        //    driver.FindElement(btnOppNavigation).Click();
        //    Thread.Sleep(3000);            
        //    driver.FindElement(By.XPath("//span[text()='Companies']")).Click();
        //    Thread.Sleep(4000);
        //    WebDriverWaits.WaitUntilEleVisible(driver, btnNew, 240);
        //    driver.FindElement(valRec1st).Click();
        //    Thread.Sleep(8000);
        //    driver.SwitchTo().Frame(driver.FindElement(By.XPath("//div[1]/div/div/div/force-aloha-page/div/iframe")));
        //    WebDriverWaits.WaitUntilEleVisible(driver, titleHLIntTeam, 250);
        //    string title = driver.FindElement(titleHLIntTeam).Text;
        //    return title;
        //}

        //To enter team member details
        public string EnterStaffDetailsL(string file)
        {
            Thread.Sleep(8000);
            ReadJSONData.Generate("Admin_Data.json");
            Console.WriteLine("Entered staff function");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valStaff = ReadExcelData.ReadData(excelPath, "AddOpportunity", 14);
            Console.WriteLine("Before entering Staff");           
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//div[1]/div/div/div/force-aloha-page/div/iframe")));
            Thread.Sleep(7000); 
            WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 120);
            driver.FindElement(txtStaff).SendKeys(valStaff);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valStaff);
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 240);
            driver.FindElement(checkInitiator).Click();
            driver.FindElement(btnSaveDealTeam).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOppor);
            driver.FindElement(btnReturnToOppor).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabInfo);
            string name = driver.FindElement(tabInfo).Text;
            return name;
        }

// Clear mandatory values on add opprtunity page
        public void ClearMandatoryValuesOnAddOpportunity()
        {
            driver.FindElement(txtOpportunityName).Clear();
            driver.FindElement(txtClient).Clear();
            driver.FindElement(txtSubject).Clear();
        }
        // Get edit opportunity page heading
        public string GetEditOpportunityPageHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, labelOpportunityEdit, 60);
            string headingEditOpportunity = driver.FindElement(labelOpportunityEdit).Text;
            return headingEditOpportunity;
        }         // Get prefilled opportunity name
        public string GetPrefilledOpportunityName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityName, 60);
            string prefilledOpportunityName = driver.FindElement(txtOpportunityName).GetAttribute("value");
            return prefilledOpportunityName;
        }         // Get prefilled client name
        public string GetPrefilledClientName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtClient, 60);
            string prefilledClientName = driver.FindElement(txtClient).GetAttribute("value");
            return prefilledClientName;
        }         // Get prefilled opportunity subject
        public string GetPrefilledOpportunitySubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSubject, 60);
            string prefilledSubject = driver.FindElement(txtSubject).GetAttribute("value");
            return prefilledSubject;
        }         // Get prefilled line of business
        public string GetPrefilledLineOfBusiness()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, selectedLOBvalue, 60);
            string prefilledLOB = driver.FindElement(selectedLOBvalue).Text;
            return prefilledLOB;
        }
        //Click cancel button
        public void ClickCancelButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel);
            driver.FindElement(btnCancel).Click();
        }

   
public string AddOpportunities(string type, string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Console.WriteLine("path:" + excelPath);             //--------------------------Enter Opportunity details-----------------------------
            //Information Section           
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityName, 40);
            string valOpportunity = CustomFunctions.RandomValue();// "abc" +CustomFunctions.RandomValue();             driver.FindElement(txtOpportunityName).SendKeys(valOpportunity);
            //driver.FindElement(txtClient).SendKeys(ReadJSONData.data.addOpportunityDetails.client);            
            driver.FindElement(txtClient).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 1));
            driver.FindElement(txtSubject).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 2));
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType);
            driver.FindElement(comboJobType).SendKeys(type);
            driver.FindElement(comboIndustryGroup).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 4));
            driver.FindElement(comboSector).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 5));
            driver.FindElement(comboClientOwnership).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 18));
            driver.FindElement(comboSubjectOwnership).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 19));             //Additional Client/Subject
            driver.FindElement(comboAdditionalClient).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 6));
            driver.FindElement(comboAdditionalSubject).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 7));             //Referral Information
            driver.FindElement(comboReferralType).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 8));             //Compliance Section
            driver.FindElement(comboNonPublicInfo).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 9));
            driver.FindElement(comboBeneficialOwner).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 10));             //Administration Section
            driver.FindElement(comboPrimaryOffice).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 11));
            driver.FindElement(txtLegalEntity).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 12));
            driver.FindElement(comboDisclosureStatus).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 13)); if (driver.FindElement(comboRecordType).Text.Contains("FR"))
            {
                driver.FindElement(txtTotalDebt).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 17));
                driver.FindElement(comboEMEAInitiatives).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 29));
                driver.FindElement(comboLegalAdvisor).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 31));
            }
            else if (driver.FindElement(comboRecordType).Text.Contains("FVA"))
            {
                driver.FindElement(txtFee).SendKeys("20000");
                driver.FindElement(txtTotalAntRev).SendKeys("30000");
            }
            else
            {
                Console.WriteLine("CF or SC types ");
            }
            //Click Save button
            driver.FindElement(btnSave).Click();
            return valOpportunity;
        }
        public string AddOpportunitiesWithStageVerballyEngaged(string type, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Console.WriteLine("path:" + excelPath);             //--------------------------Enter Opportunity details-----------------------------
            //Information Section           
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityName, 40);
            string valOpportunity = CustomFunctions.RandomValue(); driver.FindElement(txtOpportunityName).SendKeys(valOpportunity);
            //driver.FindElement(txtClient).SendKeys(ReadJSONData.data.addOpportunityDetails.client);            
            driver.FindElement(txtClient).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 1));
            driver.FindElement(txtSubject).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType, 80);
            driver.FindElement(comboJobType).SendKeys(type);
            driver.FindElement(comboIndustryGroup).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 4));
            driver.FindElement(comboSector).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 5));
            driver.FindElement(comboClientOwnership).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 18));
            driver.FindElement(comboSubjectOwnership).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 19));
            Console.WriteLine("Subject");
            driver.FindElement(comboStage).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 29));             //Additional Client/Subject
            driver.FindElement(comboAdditionalClient).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 6));
            driver.FindElement(comboAdditionalSubject).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 7));             //Referral Information
            driver.FindElement(comboReferralType).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 8));             //Compliance Section
            driver.FindElement(comboNonPublicInfo).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 9));
            driver.FindElement(comboBeneficialOwner).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 10));
            Console.WriteLine("owner");             //Administration Section
            driver.FindElement(comboPrimaryOffice).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 11));
            driver.FindElement(txtLegalEntity).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 32));
            driver.FindElement(comboDisclosureStatus).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 13));             //Click Save button
            driver.FindElement(btnSave).Click(); return valOpportunity;
        }

        public bool ValidateIfTailExpiresFieldIsRequiredOrNot()
        {
            if (driver.FindElement(txtErrorMessages).Displayed)
            {
                string errorMessages = driver.FindElement(txtErrorMessages).Text;
                if (errorMessages.Contains("Tail Expires"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public void ChangeLegalEntityFieldToHLCapitalToCreateOpportunityWithStageVerballyEngaged(string type, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;             //--------------------------Change Legal Entity to HL Capital-----------------------------             //Admministration Section
            driver.FindElement(txtLegalEntity).Clear();
            Thread.Sleep(2000);
            driver.FindElement(txtLegalEntity).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 12));             //Referral Information Section
            driver.FindElement(comboReferralType).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 8));             //Click Save button
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
        }

public string AddOpportunitiesWithLegalEntityOtherThanHLCapital(string type, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;             //--------------------------Enter Opportunity details-----------------------------
            //Information Section           
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityName, 40);
            string valOpportunity = CustomFunctions.RandomValue(); driver.FindElement(txtOpportunityName).SendKeys(valOpportunity);
            driver.FindElement(txtClient).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 1));
            driver.FindElement(txtSubject).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType, 80);
            driver.FindElement(comboJobType).SendKeys(type);
            driver.FindElement(comboIndustryGroup).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 4));
            driver.FindElement(comboSector).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 5));
            driver.FindElement(comboClientOwnership).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 18));
            driver.FindElement(comboSubjectOwnership).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 19));
            Console.WriteLine("Subject");
            //Additional Client/Subject
            driver.FindElement(comboAdditionalClient).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 6));
            driver.FindElement(comboAdditionalSubject).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 7));             //Referral Information
            driver.FindElement(comboReferralType).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 8));             //Compliance Section
            driver.FindElement(comboNonPublicInfo).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 9));
            driver.FindElement(comboBeneficialOwner).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 10));
            Console.WriteLine("owner");
            //Administration Section
            driver.FindElement(comboPrimaryOffice).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 11));
            driver.FindElement(txtLegalEntity).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 32));
            driver.FindElement(comboDisclosureStatus).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 13)); if (driver.FindElement(comboRecordType).Text.Contains("FR"))
            {
                Console.WriteLine("in if");
                driver.FindElement(txtTotalDebt).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(comboEMEAInitiatives).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 29));
            }
            else if (driver.FindElement(comboRecordType).Text.Contains("FVA"))
            {
                Console.WriteLine("in else if");
                driver.FindElement(txtFee).SendKeys("10001");
            }
            else
            {
                Console.WriteLine("CF types ");
            }
            //Click Save button
            driver.FindElement(btnSave).Click();
            return valOpportunity;
        }

       
     public void UpdateMissingFieldsForOpportunityWithStageVerballyEngagedAndLegalEntityOtherThanHLCapital(string type, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;             //--------------------------Update Missing Opportunity details-----------------------------
            //Information Section             //driver.FindElement(comboSuccessProb).Click();
            Thread.Sleep(3000);
            driver.FindElement(comboSuccessProb).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));             //Estimated Financials Section
            driver.FindElement(txtEstTranscSize).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 31));             //Estimated Fees Section
            driver.FindElement(txtRetainerFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtMnthFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
            driver.FindElement(txtContingFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));             //Administration Section
            string getDate = DateTime.Today.AddDays(0).ToString("MM/dd/yyyy");
            driver.FindElement(txtEstCloseDate).SendKeys(getDate);             // Approve NBC Form
            driver.FindElement(chkboxNBC).Click();             // By Pass Conflict check
            driver.FindElement(chkboxByPassConflictCheck).Click();             //Click Save button
            driver.FindElement(btnSave).Click();
        }


    }

}

