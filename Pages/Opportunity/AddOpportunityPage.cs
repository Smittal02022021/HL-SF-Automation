using Microsoft.Office.Interop.Excel;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static System.Collections.Specialized.BitVector32;

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

        By labelWomenLed = By.CssSelector("div:nth-child(25) > table > tbody > tr:nth-child(4) > td:nth-child(3) > span > label");// div:nth-child(27) > table > tbody > tr:nth-child(3) > td:nth-child(3) > span > label"); // div:nth-child(25) > table > tbody > tr:nth-child(4) > td:nth-child(3) > span");
        By labelWomenLedFVA = By.CssSelector("div:nth-child(27) > table > tbody > tr:nth-child(3) > td:nth-child(3) > span > label");//25
        By labelWomenLedFR = By.CssSelector("div:nth-child(21) > table > tbody > tr:nth-child(4) > td:nth-child(3) > span > label");//21 >label
        By labelAdmSection = By.CssSelector("div[id*= 'head_12_ep'] > h3");
        By labelAdmSectionFVA = By.CssSelector("div[id*= 'head_13_ep'] > h3");
        By labelAdmSectionFR = By.CssSelector("div[id = 'head_10_ep'] > h3");
        By comboWomenLed = By.CssSelector("select[id*='NgW']>option");
        By msgFee = By.XPath("//*[@id='ep']/div[2]/div[19]/table/tbody/tr[3]/td[2]/div/div[2]");
        By comboStage = By.CssSelector("select[id*='00Ni000000D80OA']");
        By txtErrorMessages = By.CssSelector("div[id*='errorDiv_ep']");



        //Lightning
        By txtOpportunityNameL = By.XPath("//input[@name= 'Name']");
        By txtClientL = By.XPath("//label[text()='Client']/following::div[1]/div/lightning-base-combobox//input");
        By txtSubjectL = By.XPath("//label[text()='Subject']/following::div[1]/div/lightning-base-combobox//input");
        By btnJobTypeL = By.XPath("//label[text()='Job Type']/parent::div//button"); //button[@aria-label='Job Type, --None--']");
        By btnIGL = By.XPath("//label[text()='Industry Group']/parent::div//button");//button[@aria-label='Industry Group, --None--']");
        By comboSectorL = By.XPath("//label[text()='Sector']/parent::div//button");//button[@aria-label='Sector, --None--']");
        By comboPrimaryOfficeL = By.XPath("//label[text()='Primary Office']/parent::div//button");//button[@aria-label='Primary Office, --None--']");
        By txtLegalEntitiesL = By.XPath("//input[@placeholder='Search Legal Entities...']");
        By comboRefTypeL = By.XPath("//button[contains(@aria-label,'Referral Type')]");
        By comboAddClientL = By.XPath("//button[contains(@aria-label,'Additional Client')]");//button[@aria-label='Additional Client, --None--']");
        By comboAddSubjectL = By.XPath("//button[contains(@aria-label,'Additional Subject')]");//button[@aria-label='Additional Subject, --None--']");
        By comboBenOwnerL = By.XPath("//button[contains(@aria-label,'Beneficial Owner & Control Person form?')]");//button[@aria-label='Beneficial Owner & Control Person form?, --None--']");
        By comboHLMaterialL = By.XPath("//button[contains(@aria-label,'Does HL Have Material Non-Public Info?')]");//button[@aria-label='Does HL Have Material Non-Public Info?, --None--']");
        By btnSaveL = By.XPath("//button[text()='Save']");
        By btnNewOppL = By.XPath("//div[contains(@class,'lvmForceActionsContainer')]//a[@title='New']");
        By txtStaff = By.XPath("//input[@placeholder='Begin Typing Name...']");


        By tabInfo = By.XPath("//a[text()='Info']");

        By comboConfAggL = By.XPath("//label[text()='Confidentiality Agreement']/parent::div//button");//button[@aria-label='Confidentiality Agreement, --None--']");
        By lblCAComments = By.XPath("//label[text()='CA Comments']");
        By lblWomenLed = By.XPath("//label[text()='Women Led']");
        By lblCongAgg = By.XPath("//label[text()='Confidentiality Agreement']");
        By txtSICL = By.XPath("//input[@placeholder='Search SIC Codes...']");
        By lblIndLangs = By.XPath("//label[text()='Indemnification Language']");
        By txtOppDescL2 = By.XPath("//label[text()='Opportunity Description']");

        By btnConfAgreeL = By.XPath("//label[text()='Confidentiality Agreement']/parent::div//button");//button[@aria-label='Confidentiality Agreement, --None--']");
        By txtObjective = By.XPath("//label[text()='Objective']/ancestor::lightning-textarea/div[1]/textarea");
        By btnClose = By.XPath("//records-record-edit-error-header/lightning-button-icon/button/lightning-primitive-icon");
        By msgOppName = By.XPath("//label[text()='Opportunity Name']/ancestor::lightning-primitive-input-simple/div[2]");
        By msgClient = By.XPath("//label[text()='Client']/ancestor::lightning-grouped-combobox/div[2]");
        By msgSubject = By.XPath("//label[text()='Subject']/ancestor::lightning-grouped-combobox/div[2]");
        By msgJobType = By.XPath("//label[text()='Job Type']/ancestor::lightning-combobox/div/div[2]");
        By msgIG = By.XPath("//label[text()='Industry Group']/ancestor::lightning-combobox/div/div[2]");
        By msgPrimaryOff = By.XPath("//label[text()='Primary Office']/ancestor::lightning-combobox/div/div[2]");
        By msgLegalEntity = By.XPath("//label[text()='Legal Entity']/ancestor::lightning-grouped-combobox/div[2]");
        By msgRefType = By.XPath("//label[text()='Referral Type']/ancestor::lightning-combobox/div/div[2]");
        By msgAddClient = By.XPath("//label[text()='Additional Client']/ancestor::lightning-combobox/div/div[2]");
        By msgAddSub = By.XPath("//label[text()='Additional Subject']/ancestor::lightning-combobox/div/div[2]");
        By msgBenOwner = By.XPath("//label[text()='Beneficial Owner & Control Person form?']/ancestor::lightning-combobox/div/div[2]");
        By msgDoesHL = By.XPath("//label[text()='Does HL Have Material Non-Public Info?']/ancestor::lightning-combobox/div/div[2]");

        By titleHLIntTeam = By.XPath("//h1/b");
        By msgInitiator = By.XPath("//label[@class='warning']");
        By msgRolesL = By.XPath("//table/tbody/tr[1]/td[2]/div");
        By listStaff = By.XPath("/html/body/ul");
        By btnReturnToOppor = By.CssSelector("input[value='Return To Opportunity']");
        By checkInitiator = By.CssSelector("input[name*='internalTeam:j_id89:0:j_id91']");
        By checkEditIniatiator = By.CssSelector("input[name*='internalTeam:j_id39:0:j_id41:0']");
        By checkSeller = By.CssSelector("input[name*='internalTeam:j_id89:1:j_id91']");
        By btnSaveDealTeam = By.CssSelector("input[value='Save']");

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
        By txtEstFee = By.XPath("//input[@name='Fee__c']");
        By checkSeller1 = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[3]/input");
        By checkPrincipal = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[4]/input");
        By checkManager = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[5]/input");
        By checkAssociate = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[6]/input");
        By checkAnalyst = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[7]/input");
        By checkSpecialty = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[8]/input");
        By checkIntern = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[11]/input");
        By msgHLIntTeam = By.CssSelector("div[id*='pgfrmId:internalTeam:j']");
        By frameInternalTeamTab = By.XPath("//iframe[@title='accessibility title']");
        By txtTotalDebtL = By.XPath("//input[@name='Total_Debt_MM__c']");
        By labelWomenLedLV = By.XPath("//flexipage-field[@data-field-id='RecordWomen_Led__cField']//label");
        By labelESGLV = By.XPath("//flexipage-field[contains(@data-field-id,'ESG')]//label");
        By labelAdmSectionLV = By.XPath("//flexipage-component2[@data-component-id='flexipage_fieldSection3']//h3//span");
        By labelWomenLedSectionLV = By.XPath("//flexipage-component2[@data-component-id='flexipage_fieldSection3']//h3//span");

        public string AddOpportunities(string type, string file)
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
                driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
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
            return valOpportunity;
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
            else if (recType.Equals("FVA"))
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
            string[] expectedValue = { "--None--", "Yes", "No" };
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


        //Add Opportunity via Lightning screen
        public string AddOpportunitiesLightning(string type, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Console.WriteLine("path:" + excelPath);
            Thread.Sleep(5000);
            //--------------------------Enter Opportunity details-----------------------------
            //Information Section           
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityNameL, 240);
            string valOpportunity = CustomFunctions.RandomValue();



            driver.FindElement(txtOpportunityNameL).SendKeys(valOpportunity);
            driver.FindElement(txtClientL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 1));
            WebDriverWaits.WaitUntilEleVisible(driver, txtClientL, 20);
            driver.FindElement(txtClientL).Click();



            //By comboDropdownResult = By.XPath("(//ul[@role='group']//li)[1]");
            //driver.FindElement(comboDropdownResult).Click();            
            Thread.Sleep(5000);
            By eleClient = By.XPath("//lightning-base-combobox/div/div/div[2]/ul/li[1]/lightning-base-combobox-item/span[2]/span/lightning-base-combobox-formatted-text");
            WebDriverWaits.WaitUntilEleVisible(driver, eleClient, 20);
            driver.FindElement(eleClient).Click();



            driver.FindElement(txtSubjectL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, txtSubjectL, 20);
            driver.FindElement(txtSubjectL).Click();
            Thread.Sleep(5000);
            By eleSubject = By.XPath("//flexipage-field[3]//lightning-base-combobox/div/div/div[2]/ul/li[1]/lightning-base-combobox-item/span[2]/span/lightning-base-combobox-formatted-text");//flexipage-field[3]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-lookup/lightning-lookup/lightning-lookup-desktop/lightning-grouped-combobox/div[1]/div/lightning-base-combobox/div/div/div[2]/ul/li[1]/lightning-base-combobox-item/span[2]/lightning-base-combobox-formatted-text");
            WebDriverWaits.WaitUntilEleVisible(driver, eleSubject, 20);
            driver.FindElement(eleSubject).Click();
            Thread.Sleep(5000);



            //Select IG
            string valIG = ReadExcelData.ReadData(excelPath, "AddOpportunity", 4);
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnIGL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnIGL));
            driver.FindElement(btnIGL).Click();
            Thread.Sleep(3000);
            By eleIG = By.XPath("//flexipage-field[6]/slot/record_flexipage-record-field/div/div/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div[1]//lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valIG + "']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleIG, 20);
            driver.FindElement(eleIG).Click();



            //Select Job Type
            WebDriverWaits.WaitUntilEleVisible(driver, btnJobTypeL, 80);
            driver.FindElement(btnJobTypeL).Click();
            Thread.Sleep(3000);
            By eleJobType = By.XPath("//flexipage-field[5]/slot/record_flexipage-record-field/div/div/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div[1]//lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + type + "']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleJobType));
            WebDriverWaits.WaitUntilEleVisible(driver, eleJobType, 20);
            driver.FindElement(eleJobType).Click();



            //Select sector
            string valSector = ReadExcelData.ReadData(excelPath, "AddOpportunity", 5);
            driver.FindElement(comboSectorL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 5));
            By eleSector = By.XPath("//flexipage-field[7]/slot/record_flexipage-record-field/div/div/slot/records-record-picklist/records-form-picklist//lightning-combobox/div[1]//lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valSector + "']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleSector, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleSector));
            driver.FindElement(eleSector).Click();



            //Select Primary Office
            string valPO = ReadExcelData.ReadData(excelPath, "AddOpportunity", 11);
            driver.FindElement(comboPrimaryOfficeL).SendKeys(valPO);
            By elePO = By.XPath("//flexipage-field[1]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valPO + "']");
            WebDriverWaits.WaitUntilEleVisible(driver, elePO, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elePO));
            driver.FindElement(elePO).Click();



            //Select Legal Entity
            string valEntity = ReadExcelData.ReadData(excelPath, "AddOpportunity", 12);
            driver.FindElement(txtLegalEntitiesL).SendKeys(valEntity);
            Thread.Sleep(3000);
            By eleLegalEntity = By.XPath("//flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-lookup/lightning-lookup/lightning-lookup-desktop/lightning-grouped-combobox/div[1]/div/lightning-base-combobox/div/div[2]");
            WebDriverWaits.WaitUntilEleVisible(driver, eleLegalEntity, 80);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleLegalEntity));
            driver.FindElement(eleLegalEntity).Click();



            //Select Referral Type  
            string valRefType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 8);
            driver.FindElement(comboRefTypeL).SendKeys(valRefType);
            By eleReferralType = By.XPath("//flexipage-component2[8]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valRefType + "']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleReferralType, 80);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleReferralType));
            driver.FindElement(eleReferralType).Click();



            //Select Additional Client
            driver.FindElement(comboAddClientL).SendKeys("No");
            driver.FindElement(By.XPath("//flexipage-component2[9]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='No']")).Click();



            //Select Additional Subject
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboAddSubjectL));
            driver.FindElement(comboAddSubjectL).SendKeys("No");
            driver.FindElement(By.XPath("//flexipage-component2[9]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='No']")).Click();



            //Select Beneficial Owner
            string valBenOwner = ReadExcelData.ReadData(excelPath, "AddOpportunity", 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboBenOwnerL));
            driver.FindElement(comboBenOwnerL).SendKeys(valBenOwner);
            driver.FindElement(By.XPath("//flexipage-component2[10]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valBenOwner + "']")).Click();



            //Select Does HL have material
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboHLMaterialL));
            driver.FindElement(comboHLMaterialL).SendKeys(valBenOwner);
            driver.FindElement(By.XPath("//flexipage-component2[10]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div[1]/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valBenOwner + "']")).Click();



            //Select Conf Agreement
            string valConf = ReadExcelData.ReadData(excelPath, "AddOpportunity", 23);
            Thread.Sleep(4000);
            //driver.FindElement(btnConfAgreeL).Click();
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnConfAgreeL));
            driver.FindElement(btnConfAgreeL).SendKeys(valConf);
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valConf + "']")).Click();



            //Click Save button
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveL));
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            //
            return valOpportunity;
        }


        public string AddOpportunitiesLightningV2(string type, string file)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Console.WriteLine("path:" + excelPath);
            Thread.Sleep(5000);
            string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
            //--------------------------Enter Opportunity details-----------------------------
            //Information Section           
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityNameL, 240);
            string valOpportunity = CustomFunctions.RandomValue();

            driver.FindElement(txtOpportunityNameL).SendKeys(valOpportunity);
            string valClient = ReadExcelData.ReadData(excelPath, "AddOpportunity", 1);
            driver.FindElement(txtClientL).SendKeys(valClient);
            Thread.Sleep(5000);
            By eleClient = By.XPath($"//label[text()='Client']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{valClient}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleClient, 20);
            driver.FindElement(eleClient).Click();

            string valSubject = ReadExcelData.ReadData(excelPath, "AddOpportunity", 2);
            driver.FindElement(txtSubjectL).SendKeys(valSubject);
            Thread.Sleep(5000);
            By eleSubject = By.XPath($"//label[text()='Subject']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{valSubject}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleSubject, 20);
            driver.FindElement(eleSubject).Click();
            Thread.Sleep(5000);

            //Select IG
            string valIG = ReadExcelData.ReadData(excelPath, "AddOpportunity", 4);
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnIGL, 20);
            driver.FindElement(btnIGL).Click();
            Thread.Sleep(3000);
            By eleIG = By.XPath($"//label[text()='Industry Group']/following::lightning-base-combobox-item//span[@title='{valIG}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleIG, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleIG));
            driver.FindElement(eleIG).Click();

            //Select Job Type
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtOppDescL2));
            WebDriverWaits.WaitUntilEleVisible(driver, btnJobTypeL, 80);
            driver.FindElement(btnJobTypeL).Click();
            Thread.Sleep(3000);
            By eleJobType = By.XPath($"//label[text()='Job Type']/following::lightning-base-combobox-item//span[@title='{type}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleJobType, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleJobType));
            driver.FindElement(eleJobType).Click();

            //Select sector
            string valSector = ReadExcelData.ReadData(excelPath, "AddOpportunity", 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSICL));
            driver.FindElement(comboSectorL).Click();
            By eleSector = By.XPath($"//label[text()='Sector']/following::lightning-base-combobox-item//span[@title='{valSector}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleSector));
            WebDriverWaits.WaitUntilEleVisible(driver, eleSector, 20);
            driver.FindElement(eleSector).Click();

            //Select Primary Office
            string valPO = ReadExcelData.ReadData(excelPath, "AddOpportunity", 11);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblWomenLed));
            driver.FindElement(comboPrimaryOfficeL).Click();
            By elePO = By.XPath($"//label[text()='Primary Office']/following::lightning-base-combobox-item//span[@title='{valPO}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(elePO));
            WebDriverWaits.WaitUntilEleVisible(driver, elePO, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elePO));
            driver.FindElement(elePO).Click();

            //Select Legal Entity
            string valEntity = ReadExcelData.ReadData(excelPath, "AddOpportunity", 12);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtLegalEntitiesL));
            driver.FindElement(txtLegalEntitiesL).SendKeys(valEntity);
            Thread.Sleep(3000);
            By eleLegalEntity = By.XPath($"//label[text()='Legal Entity']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{valEntity}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleLegalEntity, 80);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleLegalEntity));
            driver.FindElement(eleLegalEntity).Click();

            if (valRecordType == "FVA")
            {
                string valFee = ReadExcelData.ReadData(excelPath, "AddOpportunity", 28);
                CustomFunctions.MoveToElement(driver, driver.FindElement(comboRefTypeL));
                driver.FindElement(txtEstFee).SendKeys(valFee);
            }
            //Select Referral Type //Need to move in UpdteReq function  
            string valRefType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 8);
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboAddClientL));
            if (valRecordType == "CF" || valRecordType == "FVA")
            {
                driver.FindElement(comboRefTypeL).Click();
                By eleReferralType = By.XPath($"//label[text()='Referral Type']/following::lightning-base-combobox-item//span[@title='{valRefType}']");
                WebDriverWaits.WaitUntilEleVisible(driver, eleReferralType, 80);
                CustomFunctions.MoveToElement(driver, driver.FindElement(eleReferralType));
                driver.FindElement(eleReferralType).Click();
            }

            By txtTotalDebt = By.XPath("//input[@name='Total_Debt_MM__c']");
            if (valRecordType == "FR")
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(txtTotalDebt));
                driver.FindElement(txtTotalDebt).SendKeys("10");
            }
            //Select Additional Client
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboBenOwnerL));
            //driver.FindElement(comboAddClientL).Click();
            Thread.Sleep(1000);
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(comboAddClientL));
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//label[text()='Additional Client']/following::lightning-base-combobox-item//span[@title='No']")).Click();

            //Select Additional Subject
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboAddSubjectL));
            Thread.Sleep(1000);
            driver.FindElement(comboAddSubjectL).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//label[text()='Additional Subject']/following::lightning-base-combobox-item//span[@title='No']")).Click();

            //Select Beneficial Owner
            string valBenOwner = ReadExcelData.ReadData(excelPath, "AddOpportunity", 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblCongAgg));
            Thread.Sleep(1000);
            driver.FindElement(comboBenOwnerL).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath($"//label[text()='Beneficial Owner & Control Person form?']/following::lightning-base-combobox-item//span[@title='{valBenOwner}']")).Click();

            //Select Does HL have material
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboHLMaterialL));
            Thread.Sleep(1000);
            driver.FindElement(comboHLMaterialL).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//label[text()='Does HL Have Material Non-Public Info?']/following::lightning-base-combobox-item//span[@title='No']")).Click();

            //Select Conf Agreement//Need to move in UpdteReq function  
            //string valConf = ReadExcelData.ReadData(excelPath, "AddOpportunity", 23);
            //CustomFunctions.MoveToElement(driver, driver.FindElement(lblCAComments));
            //Thread.Sleep(1000);
            //driver.FindElement(comboConfAggL).Click();
            //Thread.Sleep(1000);
            //driver.FindElement(By.XPath($"//label[text()='Confidentiality Agreement']/following::lightning-base-combobox-item//span[@title='{valConf}']")).Click();

            //Click Save button                           
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
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
        public string ValidateMandatoryValidationOfClient()
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
            WebDriverWaits.WaitUntilEleVisible(driver, titleHLIntTeam, 280);
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
            string title = driver.FindElement(titleHLIntTeam).Text.Substring(17, 16);
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
            Thread.Sleep(10000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valStaff = ReadExcelData.ReadData(excelPath, "AddOpportunity", 14);
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
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabInfo, 190);
            string name = driver.FindElement(tabInfo).Text;
            return name;
        }

        public string EnterMembersToDealTeamL(string file)
        {
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeamL, 20);
            driver.FindElement(tabInternalTeamL).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, frameInternalTeamTab, 20);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamTab));
            Thread.Sleep(2000);
            driver.FindElement(btnModifyRolesL).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(5000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            int rowCount = ReadExcelData.GetRowCount(excelPath, "InternalTeams");
            By internalTeamFrame = By.XPath("//iframe[contains(@src,'InternalTeamModifyView')]");
            WebDriverWaits.WaitUntilEleVisible(driver, internalTeamFrame, 20);
            driver.SwitchTo().Frame(driver.FindElement(internalTeamFrame)); ;
            Thread.Sleep(5000);
            for (int row = 2; row <= rowCount; row++)
            {
                string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "InternalTeams", row, 1);
                WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 10);
                driver.FindElement(txtStaff).SendKeys(valStaff);
                Thread.Sleep(5000);

                By staff = By.XPath($"(/html/body/ul/li)[{row - 1}]/a");
                CustomFunctions.SelectValueWithoutSelect(driver, staff, valStaff);
                Thread.Sleep(2000);

                switch (row)
                {
                    case 2:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkSeller1, 20);
                        driver.FindElement(checkSeller1).Click();
                        break;
                    case 3:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkPrincipal, 20);
                        driver.FindElement(checkPrincipal).Click();
                        break;
                    case 4:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkManager, 20);
                        driver.FindElement(checkManager).Click();
                        break;
                    case 5:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkAssociate, 20);
                        driver.FindElement(checkAssociate).Click();
                        break;
                    case 6:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkAnalyst, 20);
                        driver.FindElement(checkAnalyst).Click();
                        break;
                        // case 7:
                        //     WebDriverWaits.WaitUntilEleVisible(driver, checkIntern, 240);
                        ///    driver.FindElement(checkIntern).Click();
                        //     break;
                }
                driver.FindElement(btnSaveDealTeam).Click();
                Thread.Sleep(8000);
                WebDriverWaits.WaitUntilEleVisible(driver, msgHLIntTeam, 90);
                WebDriverWaits.WaitForPageToLoad(driver, 20);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOppor);
            driver.FindElement(btnReturnToOppor).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabInfo, 20);
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
            Console.WriteLine("path:" + excelPath);
            //--------------------------Enter Opportunity details-----------------------------
            //Information Section           
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityName, 40);
            string valOpportunity = CustomFunctions.RandomValue();// "abc" +CustomFunctions.RandomValue();            
            driver.FindElement(txtOpportunityName).SendKeys(valOpportunity);
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
            Console.WriteLine("path:" + excelPath);
            //--------------------------Enter Opportunity details-----------------------------
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
            string excelPath = dir + file;
            //--------------------------Change Legal Entity to HL Capital-----------------------------             
            //Admministration Section
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
            string excelPath = dir + file;
            //--------------------------Enter Opportunity details-----------------------------
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
            string excelPath = dir + file;
            //--------------------------Update Missing Opportunity details-----------------------------
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



        //Get Label WomenLed
        public string ValidateWomenLedFieldLV(string recType)
        {
            if (recType.Equals("CF"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenLedLV);
                CustomFunctions.MoveToElement(driver, driver.FindElement(labelESGLV));
                string fieldName = driver.FindElement(labelWomenLedLV).Text;
                return fieldName;
            }
            else if (recType.Equals("FVA"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenLedFVA);
                CustomFunctions.MoveToElement(driver, driver.FindElement(labelESGLV));
                string fieldName = driver.FindElement(labelWomenLedFVA).Text;
                return fieldName;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenLedLV);
                CustomFunctions.MoveToElement(driver, driver.FindElement(labelESGLV));
                string fieldName = driver.FindElement(labelWomenLedLV).Text;
                return fieldName;
            }
        }
        //Get Administration section

        public string GetAdminSectionNameLV(string recType)
        {
            if (recType.Equals("CF"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelAdmSectionLV);
                CustomFunctions.MoveToElement(driver, driver.FindElement(labelWomenLedLV));
                string secName = driver.FindElement(labelAdmSectionLV).Text;
                return secName;
            }
            else if (recType.Equals("FVA"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelAdmSectionFVA);
                CustomFunctions.MoveToElement(driver, driver.FindElement(labelWomenLedFVA));
                string secName = driver.FindElement(labelAdmSectionFVA).Text;
                return secName;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelAdmSectionFR);
                CustomFunctions.MoveToElement(driver, driver.FindElement(labelWomenLedFR));
                string secName = driver.FindElement(labelAdmSectionFR).Text;
                return secName;
            }

        }
        //Get WomenLed section

        public string GetWomenLedSectionNameLV(string recType)
        {
            if (recType.Equals("CF"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenLedSectionLV);
                CustomFunctions.MoveToElement(driver, driver.FindElement(labelWomenLedLV));
                string secName = driver.FindElement(labelWomenLedSectionLV).Text;
                return secName;
            }
            else if (recType.Equals("FVA"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenLedSectionLV);
                CustomFunctions.MoveToElement(driver, driver.FindElement(labelWomenLedLV));
                string secName = driver.FindElement(labelWomenLedSectionLV).Text;
                return secName;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelWomenLedSectionLV);
                CustomFunctions.MoveToElement(driver, driver.FindElement(labelWomenLedLV));
                string secName = driver.FindElement(labelWomenLedSectionLV).Text;
                return secName;
            }
        }

        public string AddOpportunitiesLightningV3(string valRecordType, string type, string file)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(5000);
            //string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
            //--------------------------Enter Opportunity details-----------------------------
            //Information Section           
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityNameL, 240);
            string valOpportunity = CustomFunctions.RandomValue();

            driver.FindElement(txtOpportunityNameL).SendKeys(valOpportunity);
            string valClient = ReadExcelData.ReadData(excelPath, "AddOpportunity", 1);
            driver.FindElement(txtClientL).SendKeys(valClient);
            Thread.Sleep(5000);
            By eleClient = By.XPath($"//label[text()='Client']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{valClient}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleClient, 20);
            driver.FindElement(eleClient).Click();

            string valSubject = ReadExcelData.ReadData(excelPath, "AddOpportunity", 2);
            driver.FindElement(txtSubjectL).SendKeys(valSubject);
            Thread.Sleep(5000);
            By eleSubject = By.XPath($"//label[text()='Subject']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{valSubject}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleSubject, 20);
            driver.FindElement(eleSubject).Click();
            Thread.Sleep(5000);

            //Select IG
            string valIG = ReadExcelData.ReadData(excelPath, "AddOpportunity", 4);
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnIGL, 20);
            driver.FindElement(btnIGL).Click();
            Thread.Sleep(3000);
            By eleIG = By.XPath($"//label[text()='Industry Group']/following::lightning-base-combobox-item//span[@title='{valIG}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleIG, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleIG));
            driver.FindElement(eleIG).Click();

            //Select Job Type
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtOppDescL2));
            WebDriverWaits.WaitUntilEleVisible(driver, btnJobTypeL, 80);
            driver.FindElement(btnJobTypeL).Click();
            Thread.Sleep(3000);
            By eleJobType = By.XPath($"//label[text()='Job Type']/following::lightning-base-combobox-item//span[@title='{type}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleJobType, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleJobType));
            driver.FindElement(eleJobType).Click();

            //Select sector
            string valSector = ReadExcelData.ReadData(excelPath, "AddOpportunity", 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSICL));
            driver.FindElement(comboSectorL).Click();
            By eleSector = By.XPath($"//label[text()='Sector']/following::lightning-base-combobox-item//span[@title='{valSector}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleSector));
            WebDriverWaits.WaitUntilEleVisible(driver, eleSector, 20);
            driver.FindElement(eleSector).Click();

            //Select Primary Office
            string valPO = ReadExcelData.ReadData(excelPath, "AddOpportunity", 11);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblWomenLed));
            driver.FindElement(comboPrimaryOfficeL).Click();
            By elePO = By.XPath($"//label[text()='Primary Office']/following::lightning-base-combobox-item//span[@title='{valPO}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(elePO));
            WebDriverWaits.WaitUntilEleVisible(driver, elePO, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elePO));
            driver.FindElement(elePO).Click();

            //Select Legal Entity
            string valEntity = ReadExcelData.ReadData(excelPath, "AddOpportunity", 12);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtLegalEntitiesL));
            driver.FindElement(txtLegalEntitiesL).SendKeys(valEntity);
            Thread.Sleep(3000);
            By eleLegalEntity = By.XPath($"//label[text()='Legal Entity']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{valEntity}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleLegalEntity, 80);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleLegalEntity));
            driver.FindElement(eleLegalEntity).Click();

            if (valRecordType == "FVA")
            {
                string valFee = ReadExcelData.ReadData(excelPath, "AddOpportunity", 28);
                CustomFunctions.MoveToElement(driver, driver.FindElement(comboRefTypeL));
                driver.FindElement(txtEstFee).SendKeys(valFee);
            }
            //Select Referral Type //Need to move in UpdteReq function  
            string valRefType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 8);
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboAddClientL));
            Thread.Sleep(2000);
            if (valRecordType == "CF" || valRecordType == "FVA")
            {
                driver.FindElement(comboRefTypeL).Click();
                By eleReferralType = By.XPath($"//label[text()='Referral Type']/following::lightning-base-combobox-item//span[@title='{valRefType}']");
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, eleReferralType, 5);
                    CustomFunctions.MoveToElement(driver, driver.FindElement(eleReferralType));
                }
                catch
                {
                    driver.FindElement(comboRefTypeL).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, eleReferralType, 5);
                    CustomFunctions.MoveToElement(driver, driver.FindElement(eleReferralType));
                }

                driver.FindElement(eleReferralType).Click();
            }

            if (valRecordType == "FR")
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(txtTotalDebtL));
                driver.FindElement(txtTotalDebtL).SendKeys("10");
            }
            //Select Additional Client
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboBenOwnerL));
            //driver.FindElement(comboAddClientL).Click();
            Thread.Sleep(1000);
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(comboAddClientL));
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//label[text()='Additional Client']/following::lightning-base-combobox-item//span[@title='No']")).Click();

            //Select Additional Subject
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboAddSubjectL));
            Thread.Sleep(1000);
            driver.FindElement(comboAddSubjectL).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//label[text()='Additional Subject']/following::lightning-base-combobox-item//span[@title='No']")).Click();

            //Select Beneficial Owner
            string valBenOwner = ReadExcelData.ReadData(excelPath, "AddOpportunity", 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblCongAgg));
            Thread.Sleep(1000);
            driver.FindElement(comboBenOwnerL).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath($"//label[text()='Beneficial Owner & Control Person form?']/following::lightning-base-combobox-item//span[@title='{valBenOwner}']")).Click();

            //Select Does HL have material
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboHLMaterialL));
            Thread.Sleep(1000);
            driver.FindElement(comboHLMaterialL).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//label[text()='Does HL Have Material Non-Public Info?']/following::lightning-base-combobox-item//span[@title='No']")).Click();

            //Click Save button                           
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            return valOpportunity;

        }
    }
}

