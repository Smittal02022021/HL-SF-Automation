using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SF_Automation.Pages.Opportunity
{
    class OppValuationPeriods : BaseClass
    {
        By btnNewOppValPeriod = By.CssSelector("input[value='New Opportunity Valuation Period']");
        By lblNewOppValPeriod = By.CssSelector("h2[class='pageDescription']");
        By txtName = By.CssSelector("input[name*='id30']");
        By lnkValDate = By.CssSelector("tbody > tr:nth-child(3) > td:nth-child(2) > div > span > span > a");
        By btnSave = By.CssSelector("input[value='Save']");
        By lblOppValPeriodDetail1 = By.CssSelector("h2[class='pageDescription']");
        By lblOppValPeriodDetail = By.CssSelector("h2[class='mainTitle']");
        By btnNewOppValPeriodPosition = By.CssSelector("input[value='New Opp Valuation Period Position']");
        By btnCompany = By.CssSelector("span[class='lookupInput']>input[id*='CompanyField']");
        By comboAssetClasses = By.CssSelector("select[name*='id79']");
        By comboUpdAssetClasses = By.CssSelector("select[name*='id38']");
        By comboPositionIG = By.CssSelector("select[name*='PositionIG']");
        By comboPositionSector = By.CssSelector("select[name*='PositionS']");
        By btnCancel = By.CssSelector("input[value='Cancel']");
        By msgPeriodSection = By.CssSelector("span[id*='RelatedRecordPanelId']>label");
        By msgError = By.CssSelector("div[class='message errorM3']>table>tbody>tr>td[class='messageCell']>div");
        By btnImportPositions = By.CssSelector("input[value='Import Positions']");
        By lnkEdit = By.CssSelector("tbody[id*='j_id94:pbtableId2:tb']>tr>td>a");
        By valName = By.CssSelector("td[id*='id134']>a");
        By valComp = By.CssSelector("td[id*='id137']>span>a");
        By valIG = By.CssSelector("td[id*='id138']>span");
        By valAsset = By.CssSelector("td[id*='id142']>span");
        By valFee = By.CssSelector("td[id*='id143']>span");
        By valStatus = By.CssSelector("td[id*='id144']>span");
        By lblValPeriodPosition = By.CssSelector("h1[class='pageType']");
        By btnBackToValPeriod = By.CssSelector("input[name*='BtnsId:Onbacktovp']");
        By lnkEditPosition = By.CssSelector("a[name*='id128']>font");
        By lnkDel = By.CssSelector("a[name*='id132']>font");
        By btnAddTeamMember = By.CssSelector("input[value='Add New Team Member']");
        By btnSaveTeamMember = By.CssSelector("input[value='Save Team Members']");
        By btnBackToOppValPeriodList = By.CssSelector("input[value='Back To Opp Valuation Period List']");
        By radioValPeriod = By.CssSelector("input[name*='myselection']");
        By btnSearchValPeriodPosition = By.CssSelector("input[value='Search Valuation Period for Positions']");
        By radioImportPositionsWithoutTM = By.CssSelector("input[value='Import Positions Without Team Members']");
        By checkPosition = By.CssSelector("input[id*='myCheckbox']");
        By btnSaveAndBack = By.CssSelector("input[id*='139']");
        By msgTeamMembers = By.CssSelector("div[class='messageText']");
        By valPeriod = By.CssSelector("td[id*=':j_id82'] > a");
        By valStaff1st = By.CssSelector("td[id*='id174']>select>option");
        By valStaff = By.CssSelector("td[id*='id174']");
        By valRole = By.CssSelector("td[id*='id179']");
        By btnBack = By.CssSelector("input[value*='Back']");
        By msgNoValPeriod = By.CssSelector("div[id*='id40']");
        By btnBackToOpp = By.CssSelector("input[value*='Back To Opportunity']");
        By titleOppDetails = By.CssSelector("div[id*='id55'] > div.pbHeader > table > tbody > tr > td.pbTitle > h2");
        By titleValPeriods = By.CssSelector("div>font>b");
        By txtVPName = By.CssSelector("td[id *= 'id82'] > a");
        By valVPDate = By.CssSelector("td[id*='id87']>span");
        By btnPortfolioVL = By.XPath("//button[text()='Portfolio Valuation']");

        By btnNewOppValPeriodL = By.XPath("//input[@value='New Opportunity Valuation Period']");
        By lblValuationFieldsL = By.XPath("//tbody/tr/th/label");
        By lblValuationButtonsL = By.XPath("//tbody/tr/td[2]/input");
        By btnSaveL = By.XPath("//input[@value='Save']");
        By msgMandatoryValL = By.XPath("//tbody/tr/td//li");
        By btnCancelL = By.XPath("//input[@value='Cancel']");
        By btnDeleteL = By.XPath("//input[@value='Delete']");
        By btnBackToOppL = By.XPath("//input[@value='Back To Opportunity']");


        By msgNoValL = By.XPath("//div[text()='Currently there are no valuation periods for this Opportunity. To proceed, please create a new valuation period.']");
        By txtNameL = By.XPath("//input[contains(@id,'id33')]");
        By txtName2L = By.XPath("//input[contains(@id,'id30')]");

        By txtEditNameL = By.XPath("//label[text()='Position Name']/ancestor::tr/td[1]//input");
        By lnkValDateL = By.XPath("//tr[3]/td[1]/div//a");
        By valNameL = By.XPath("//th[text()='Name']/ancestor::tr/td/span/span/span");
        By secPeriodDetailL = By.XPath("//div[contains(@title,'Hide Section')]/img");
        By btnPeriodDetailL = By.XPath("//td//input[@type='submit']");
        By mainSecPeriodDetailL = By.XPath("//td/h2");
        By btnBackValPeriodListL = By.XPath("//input[@value='Back To Opp Valuation Period List']");
        By titleValPeriodsL = By.XPath("//div[1]/font/b");
        By lnkEditL = By.XPath("//tr[1]/td[1]/a");
        By valAddedValPeriodL = By.XPath("//div[1]/table/tbody/tr[2]/td[2]/a");
        By btnCloseTabL = By.XPath("//ul[2]/li[4]/div[2]/button");
        By valReportsL = By.XPath("//div[1]/following::h3[text()='Report Section']/ancestor::div//div[2]/table/tbody/tr/td/a");
        By valUpdatedPeriodL = By.XPath("//tr/td[2]/a[1]");
        By titlePeriodDetailL = By.XPath("//h2[text()='Opportunity Valuation Period Detail']");
        By titleOppValPeriodL = By.XPath("//h1[text()='Opportunity Valuation Period']");
        By btnImportPositionL = By.XPath("//input[@value='Import Positions']");
        By msgImportPositionL = By.XPath("//div[text()='There is no valuation period available to import position.']");
        By btnBackL = By.XPath("//input[@value='Back']");
        By btnNewPeriodPositionL = By.XPath("//input[@value='New Opp Valuation Period Position']");
        By titlePeriodPositionL = By.XPath("//h2[text()=' New Opp Valuation Period Position']");
        By titlePositionDetailL = By.XPath("//h2[text()='Opportunity Valuation Position Detail']");
        By lblPeriodPositionL = By.XPath("//tr/th/label");
        By txtCompanyL = By.XPath("//span/input[contains(@id,'AccountSectionItem:CompanyField')]");
        By btnAssetClassL = By.XPath("//select[contains(@id,'id79')]");
        By btnIGL = By.XPath("//select[contains(@id,'PositionIG')]");
        By btnPositonSectorL = By.XPath("//select[contains(@id,'PositionS')]");
        By valAddedPositionL = By.XPath("//span/table/tbody/tr[1]/td[2]/a[1]");
        By btnBackToValPeriodL = By.XPath("//input[@value='Back To Valuation Period']");
        By btnEditL = By.XPath("//input[@value='Back To Valuation Period']/ancestor::td/input[@value='Edit']");
        By valUpdPositionL = By.XPath("//th[text()='Position Name']/ancestor::tr/td/span[1]/span");
        By secOppValTeamMemL = By.XPath("//b[text()='Opp Valuation Period Team Members']");
        By msgTeamMemL = By.XPath("//tr[1]/td[2]/div");
        By btnAddTeamMemL = By.XPath("//input[@value='Add New Team Member']");
        By colTeamMemL = By.XPath("//span/div[2]/div//tr/th/div");
        By btnSaveTeamMemL = By.XPath("//input[@value='Save Team Members']");
        By lnkDeleteTeamL = By.XPath("//a[text()='Delete']");
        By btnRoleL = By.XPath("//select[contains(@name,'id180')]");
        By btnRoleEngL = By.XPath("//select[contains(@name,'id216')]");
        By valStaffL = By.XPath("//span/div[2]/div//td[2]");
        By valRoleL = By.XPath("//span/div[2]//td[3]/div/select/option[@selected='selected']");
        By tabHLRelatedL = By.XPath("//span[@title='HL_Related_PortfolioValuations']");
        By btnExistingImports = By.XPath("//td[1]/span/input[contains(@value,'Valuation Period')]");
        By btnExistingValPeriodL = By.XPath("//input[@type='radio']");
        By btnExistingValPeriod2ndL = By.XPath("//tr[2]/td[1]/input");
        By btnSearchValPeriodPosL = By.XPath("//input[@value='Search Valuation Period for Positions']");
        By lblImportL = By.XPath("//label[contains(text(),'Import Positions')]");
        By btnImportBottomL = By.XPath("//span[contains(@id,'RelatedRecordPanelId')]/input[contains(@name,'RelatedRecordPanelId')]");
        By chkPositionNameL = By.XPath("//input[contains(@name,'myCheckbox')]");
        By btnSaveAndBackL = By.XPath("//input[contains(@value,'Save & Back ')]");
        By valAddedPositionWithTeam = By.XPath("//table[contains(@id,'pbtableId2')]//tbody/tr/td[2]/a");
        By chkImportWithoutTeamL = By.XPath("//input[@value='Import Positions Without Team Members']");
        By lnkEditPeriodPositionL = By.XPath("//table[contains(@id,'pbtableId2')]//tbody/tr/td[1]//font");
        By valStatusPeriodPosL = By.XPath("//th[text()='Status']/ancestor::tr[1]/td[2]/span");
        By tabPeriodPositionL = By.XPath("//ul[2]/li[4]/a/span[2]");
        By lnkDelPositionL = By.XPath("//tr[1]/td[1]/span/a/font");



        public string ClickOppValuationPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewOppValPeriod, 60);
            driver.FindElement(btnNewOppValPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblNewOppValPeriod, 60);
            string title = driver.FindElement(lblNewOppValPeriod).Text;
            return title;
        }

        //Get No Valuation Periods message
        public string GetNoValuationPeriods()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgNoValPeriod, 70);
            string message = driver.FindElement(msgNoValPeriod).Text;
            return message;
        }

        //Validate Back To Opportunity button 
        public string ValidateBackToOpportunityButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToOpp, 120);
            string value = driver.FindElement(btnBackToOpp).Displayed.ToString();
            Console.WriteLine(value);
            if (value.Equals("True"))
            {
                return "Back To Opportunity";
            }
            else
            {
                return "No Back To Opportunity";
            }
        }

        //Click Back to Opp button and get title of page
        public string ClickBackToOppAndValidatePageTitle()
        {
            driver.FindElement(btnBackToOpp).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleOppDetails, 70);
            string title = driver.FindElement(titleOppDetails).Text;
            return title;
        }

        //Enter all details and save it.
        public string EnterAndSaveOppValuationDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string Name = CustomFunctions.RandomValue();
            driver.FindElement(txtName).SendKeys(Name);
            //driver.FindElement(comboFrequency).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 2));
            //driver.FindElement(txtSummary).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 3));
            driver.FindElement(lnkValDate).Click();
            driver.FindElement(btnSave).Click();
            return Name;
        }

        public string GetOppValPeriodDetailTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnImportPositions, 60);
            string title = driver.FindElement(lblOppValPeriodDetail1).Text;
            return title;
        }

        //Click on Back To Opp Valuation Period List button and validate title of Page
        public string ClickBackToOppValAndGetTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToOppValPeriodList, 60);
            driver.FindElement(btnBackToOppValPeriodList).Click();
            string title = driver.FindElement(titleValPeriods).Text;
            return title;
        }

        //Get added VP Name
        public string GetVPName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtVPName, 60);
            string value = driver.FindElement(txtVPName).Text;
            return value;
        }


        //Get added Valuation Date
        public string GetValuationDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valVPDate, 60);
            string value = driver.FindElement(valVPDate).Text;
            return value;
        }

        //Click on added added Valuation Period
        public void ClickOnAddedValuation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtVPName, 60);
            driver.FindElement(txtVPName).Click();
        }

        //Enter Opp Valuation Period Position details and click cancel button
        public string EnterPeriodPositionDetailsAndClickCancel(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewOppValPeriodPosition, 60);
            driver.FindElement(btnNewOppValPeriodPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 60);
            driver.FindElement(btnCompany).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 4));
            driver.FindElement(comboAssetClasses).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 5));
            //driver.FindElement(txtNotes).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 6));
            //driver.FindElement(txtReportFee).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 7));            
            driver.FindElement(btnCancel).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewOppValPeriodPosition, 60);
            string value = driver.FindElement(msgPeriodSection).Text.ToString();
            return value;
        }

        //Validate error message when Asset Classes is selected as Other
        public string ValidateErrorMessage(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewOppValPeriodPosition, 60);
            driver.FindElement(btnNewOppValPeriodPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 60);
            driver.FindElement(btnCompany).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 4));
            driver.FindElement(comboAssetClasses).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 5));
            driver.FindElement(comboPositionIG).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 8));
            driver.FindElement(comboPositionSector).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 9));
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgError, 80);
            string message = driver.FindElement(msgError).Text.ToString();
            return message;
        }

        //Enter all Opp Valuation Period Position details
        public bool EnterValPeriodPositionDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            //driver.FindElement(txtNotes).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 6));
            //driver.FindElement(txtReportFee).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 7));
            driver.FindElement(comboAssetClasses).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 13));
            driver.FindElement(comboPositionIG).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 8));
            driver.FindElement(comboPositionSector).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 9));
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewOppValPeriodPosition, 60);
            bool value = driver.FindElement(lnkEdit).Displayed;
            return value;
        }

        //Get value of Name 
        public string GetName()
        {
            string name = driver.FindElement(valName).Text.ToString();
            return name;
        }
        //Get value of Company 
        public string GetCompanyName()
        {
            string compName = driver.FindElement(valComp).Text.ToString();
            return compName;
        }
        //Get value of IG 
        public string GetIG()
        {
            string industryGroup = driver.FindElement(valIG).Text.ToString();
            return industryGroup;
        }
        //Get value of Assert 
        public string GetAsset()
        {
            string asset = driver.FindElement(valAsset).Text.ToString();
            return asset;
        }
        //Get value of Report Fee 
        public string GetFee()
        {
            string fee = driver.FindElement(valFee).Text.ToString();
            return fee;
        }
        //Get value of Status
        public string GetStatus()
        {
            string status = driver.FindElement(valStatus).Text.ToString();
            return status;
        }
        //Validate Opportunity Valuation Position Detail page title
        public string ValidateOppValPositionDetailTitle()
        {
            driver.FindElement(valName).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblValPeriodPosition, 60);
            string titleValPosition = driver.FindElement(lblValPeriodPosition).Text.ToString();
            return titleValPosition;
        }
        //Validate Opportunity Valuation Position Detail page title
        public string ValidateOppValPeriodDetailTitle()
        {
            driver.FindElement(btnBackToValPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblOppValPeriodDetail, 60);
            string titlePeriodDetail = driver.FindElement(lblOppValPeriodDetail).Text.ToString();
            return titlePeriodDetail;
        }

        //To Update Opp Valuation Period Position details
        public void UpdateOppValPeriodPositionDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(lnkEditPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 60);
            driver.FindElement(comboUpdAssetClasses).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 10));
            //driver.FindElement(txtUpdReportFee).Clear();
            //driver.FindElement(txtUpdReportFee).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 11));
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToValPeriod, 60);
            driver.FindElement(btnBackToValPeriod).Click();
        }
        //Click delete link and validate record in Opp Valuation Period Position
        public string ValidateRecAfterDelete()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEdit, 60);
            try
            {
                string lnkDisplayed = driver.FindElement(lnkDel).Displayed.ToString();
                Console.WriteLine("Delete link is displayed");
                driver.FindElement(lnkDel).Click();
                Thread.Sleep(2000);
                driver.SwitchTo().Alert().Accept();
                WebDriverWaits.WaitUntilEleVisible(driver, msgPeriodSection, 80);
                string value = driver.FindElement(msgPeriodSection).Text.ToString();
                return value;
            }
            catch (Exception)
            {
                return "Delete link is not displayed";
            }
        }
        //Click on added Valuation Period
        public void ClickAddedValPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPeriod, 80);
            driver.FindElement(valPeriod).Click();
        }

        //To add team members and save it. 
        public void ClickPositionAndSaveTeamMembers()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valName, 60);
            driver.FindElement(valName).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddTeamMember, 80);
            driver.FindElement(btnAddTeamMember).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveTeamMember, 80);
            driver.FindElement(btnSaveTeamMember).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnBackToValPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToOppValPeriodList, 80);
            driver.FindElement(btnBackToOppValPeriodList).Click();
        }
        //To get Staff Name 
        public string GetStaffDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valName, 60);
            driver.FindElement(valName).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddTeamMember, 80);
            driver.FindElement(btnAddTeamMember).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valStaff1st, 80);
            string valStaffName = driver.FindElement(valStaff1st).Text;
            return valStaffName;
        }
        //To get Staff Name 
        public string ValidateStaffName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStaff, 80);
            string valStaffName = driver.FindElement(valStaff).Text;
            return valStaffName;
        }

        //To get role. 
        public string GetRole()
        {
            string valueRole = driver.FindElement(valRole).Text;
            return valueRole;
        }
        //To save team members
        public void SaveTeamMembers()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveTeamMember, 80);
            driver.FindElement(btnSaveTeamMember).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnBackToValPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToOppValPeriodList, 80);
            driver.FindElement(btnBackToOppValPeriodList).Click();
        }
        //To import positions without team members
        public string ImportPositionsWithoutTM()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnImportPositions, 100);
            driver.FindElement(btnImportPositions).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, radioValPeriod, 60);
            driver.FindElement(radioValPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearchValPeriodPosition, 60);
            driver.FindElement(btnSearchValPeriodPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, radioImportPositionsWithoutTM, 60);
            driver.FindElement(radioImportPositionsWithoutTM).Click();
            driver.FindElement(btnSave).Click();
            //Accept the alert pop up
            Thread.Sleep(2000);
            driver.SwitchTo().Alert().Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, checkPosition, 60);
            driver.FindElement(checkPosition).Click();
            driver.FindElement(btnSaveAndBack).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblOppValPeriodDetail, 80);
            string title = driver.FindElement(lblOppValPeriodDetail).Text;
            return title;
        }
        //To import positions without team members
        public string ImportPositionsWithTM()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnImportPositions, 100);
            driver.FindElement(btnImportPositions).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, radioValPeriod, 60);
            driver.FindElement(radioValPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearchValPeriodPosition, 60);
            driver.FindElement(btnSearchValPeriodPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 60);
            driver.FindElement(btnSave).Click();
            //Accept the alert pop up
            Thread.Sleep(2000);
            driver.SwitchTo().Alert().Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, checkPosition, 60);
            driver.FindElement(checkPosition).Click();
            driver.FindElement(btnSaveAndBack).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblOppValPeriodDetail, 80);
            string title = driver.FindElement(lblOppValPeriodDetail).Text;
            return title;
        }

        //To get message of Opp Valuation Period Team Members
        public string GetMessageTeamMembers()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgTeamMembers, 60);
            string message = driver.FindElement(msgTeamMembers).Text;
            return message;
        }

        //Click on Import Positions
        public void ClickImportPositionsButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnImportPositions);
            driver.FindElement(btnImportPositions).Click();
        }

        //To get No Valuation period message
        public string GetMessageNoValuationPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgTeamMembers, 80);
            string message = driver.FindElement(msgTeamMembers).Text;
            driver.FindElement(btnBack).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToOppValPeriodList, 80);
            driver.FindElement(btnBackToOppValPeriodList).Click();
            return message;
        }

        //To get No Valuation period message
        public string ValidateAlertMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearchValPeriodPosition, 80);
            driver.FindElement(btnSearchValPeriodPosition).Click();
            try
            {
                Thread.Sleep(2000);
                driver.SwitchTo().Alert().Accept();
                return "Please select an Valuation Period";
            }
            catch (Exception)
            {
                return "No message";
            }
        }

        //To get No Valuation period message
        public string ValidateNoPositionAvailableMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, radioValPeriod, 80);
            driver.FindElement(radioValPeriod).Click();
            driver.FindElement(btnSearchValPeriodPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgTeamMembers, 80);
            string message = driver.FindElement(msgTeamMembers).Text;
            driver.FindElement(btnBack).Click();
            return message;
        }

        //Lightning

        //Click New Opp Valuation Period and validate the fields
        public bool ClickOppValuationAndValidateFields()
        {
            Thread.Sleep(4000);
            By lnkMoreL = By.XPath("(//lightning-button-menu//button[contains(@class,'slds-button_icon-border-filled')])[1]");

            WebDriverWaits.WaitUntilEleVisible(driver, lnkMoreL, 80);
            driver.FindElement(lnkMoreL).Click();
            Thread.Sleep(5000);
            By btnPortfolioVCAOL = By.XPath("//span[text()='Portfolio Valuation']");

            WebDriverWaits.WaitUntilEleVisible(driver, btnPortfolioVCAOL, 120);
            driver.FindElement(btnPortfolioVCAOL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().Frame(0);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewOppValPeriodL, 120);
            driver.FindElement(btnNewOppValPeriodL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='accessibility title']")));
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblValuationFieldsL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            Console.WriteLine(actualValue[0]);

            string[] expectedValue = { "*\r\nName", "Frequency", "Summary", "Month/Quarter", "*\r\nValuation Date" };
            // string[] expectedValue = { "Name", "Summary", "Valuation Date" };
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


        //Click New Opp Valuation Period and validate button
        public bool ValidateButtonsOnValuationPeriod()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblValuationButtonsL);
            var actualValue = valRecordTypes.Select(x => x.GetAttribute("value")).ToArray();
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);
            string[] expectedValue = { "Save", "Cancel" };
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

        //Click on Save on Valuation Period page
        public bool ValidateMandatoryFieldValidationsOnClickOfSaveButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 120);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(msgMandatoryValL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Name: You must enter a value", "Valuation Date: You must enter a value" };
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

        //Validate Opportunity details page is displayed upon clicking cancel button
        public string ValidateOppDetailsPageUponClickingCancelButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelL, 120);
            driver.FindElement(btnCancelL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='accessibility title']")));
            Thread.Sleep(4000);
            string tab = driver.FindElement(msgNoValL).Text;
            driver.FindElement(btnNewOppValPeriodL).Click();
            return tab;
        }

        //Enter all details and save it.
        public string EnterAndSaveOppValuationPeriodDetailsL(string name)
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);

            driver.FindElement(txtName2L).SendKeys(name);
            driver.FindElement(lnkValDateL).Click();
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='accessibility title']")));
            Thread.Sleep(5000);
            string value = driver.FindElement(valNameL).Text;
            return value;
        }

        //Enter all details and save it.
        public string EnterAndSaveOppValuationPeriodDetailsL2nd(string name)
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);

            driver.FindElement(txtNameL).SendKeys(name);
            driver.FindElement(lnkValDateL).Click();
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='accessibility title']")));
            Thread.Sleep(5000);
            string value = driver.FindElement(valNameL).Text;
            return value;
        }

        //Validate sections of Opportunity Valuation Period Detail
        public bool ValidateSectionsOfOppValuationPeriodDetail()
        {
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(secPeriodDetailL);
            var actualValue = valRecordTypes.Select(x => x.GetAttribute("name")).ToArray();
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);
            Console.WriteLine(actualValue[2]);
            string[] expectedValue = { "Opportunity Information", "Valuation Information", "Report Section", "System Information" };
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

        //Validate buttons of Opportunity Valuation Period Detail
        public bool ValidateButtonsOfOppValuationPeriodDetail()
        {
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(btnPeriodDetailL);
            var actualValue = valRecordTypes.Select(x => x.GetAttribute("value")).ToArray();
            string[] expectedValue = { "Edit", "Import Positions", "Back To Opp Valuation Period List", "New Opp Valuation Period Position" };
            Console.WriteLine(actualValue[0]);
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

        //Validate main sections of Opportunity Valuation Period Detail
        public bool ValidateMainSectionsOfOppValuationPeriodDetail()
        {
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(mainSecPeriodDetailL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Opportunity Valuation Period Detail", "Opp Valuation Period Positions" };
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

        //Validate Opportunity details page upon clicking Back To Opp Valuation Period List button
        public string ValidateOppDetailsPageUponClickOfBackToOppButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackValPeriodListL, 120);
            driver.FindElement(btnBackValPeriodListL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(4000);
            string tab = driver.FindElement(titleValPeriodsL).Text;
            return tab;
        }

        //Validate edit functionality of Valuation Period
        public string EditFunctionalityOfValuationPeriod(String name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditL, 120);
            driver.FindElement(lnkEditL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(4000);
            driver.FindElement(txtNameL).Clear();
            driver.FindElement(txtNameL).SendKeys(name);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(4000);
            string value = driver.FindElement(valUpdatedPeriodL).Text;
            return value;
        }

        //Validate Opportunity Valuation Period Detail page is displayed upon clicking Valuation Period Name button
        public string ValidateOppValPeriodDetailPageUponClickOfValPeriodNameLink()
        {
            Thread.Sleep(4000);
            driver.FindElement(valUpdatedPeriodL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(7000);
            string page = driver.FindElement(titleOppValPeriodL).Text;
            return page;
        }

        //Validate error message after clicking on Import Position button
        public string ValidateMessageWhileClickingOnImportButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnImportPositionL, 120);
            driver.FindElement(btnImportPositionL).Click();
            Thread.Sleep(3000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            string value = driver.FindElement(msgImportPositionL).Text;
            return value;
        }

        public void ClickImportButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnImportPositionL, 120);
            driver.FindElement(btnImportPositionL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(4000);
        }

        //Validate error message after clicking on Import Position button
        public string ValidatePeriodPositionPageWhileClickingNewOppValPeriodPositionButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackL, 120);
            driver.FindElement(btnBackL).Click();
            Thread.Sleep(3000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            driver.FindElement(btnNewPeriodPositionL).Click();
            Thread.Sleep(3000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            string value = driver.FindElement(titlePeriodPositionL).Text;
            return value;
        }

        public void ClickNewPeriodPositionButtonL()
        {
            Thread.Sleep(3000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewPeriodPositionL, 120);
            driver.FindElement(btnNewPeriodPositionL).Click();
        }

        public void ClickNewPeriodPositionButtonWithoutFrameL()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewPeriodPositionL, 120);
            driver.FindElement(btnNewPeriodPositionL).Click();
        }
        //Validate fields of Opportunity Valuation Period Position
        public bool ValidateFieldsOfOppValuationPeriodPositionL()
        {
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblPeriodPositionL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Company", "*\r\nAsset Classes", "Company Industry Group", "Company Sector", "Report Fee", "Position Notes" };
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);
            Console.WriteLine(actualValue[2]);
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

        //Click on Save on Valuation Period page
        public bool ValidateMessageWhileClickingSaveButtonOnPeriodPosition()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 120);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(msgMandatoryValL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Company: You must enter a value", "Asset Classes: You must enter a value" };
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

        //Validate Opportunity Valuation Period details page is displayed upon clicking cancel button
        public string ValidateOppValPeriodDetailsPageUponClickingCancelButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelL, 120);
            driver.FindElement(btnCancelL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            string tab = driver.FindElement(titlePeriodDetailL).Text;
            driver.FindElement(btnNewPeriodPositionL).Click();
            return tab;
        }

        //Enter all details and save it.
        public string EnterAndSaveOppValuationPeriodPositionDetailsL(string name)
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            driver.FindElement(txtCompanyL).SendKeys(name);
            Thread.Sleep(6000);
            driver.FindElement(btnIGL).SendKeys("BUS - Business Services");
            Thread.Sleep(4000);
            driver.FindElement(btnAssetClassL).SendKeys("ABL");
            Thread.Sleep(4000);
            driver.FindElement(btnPositonSectorL).SendKeys("Cloud & Enterprise Consulting");
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            string value = driver.FindElement(valAddedPositionL).Text;
            return value;

        }

        //Enter all details and save 1st Period Position.
        public string EnterAndSave1stOppValuationPeriodPositionDetailsLWithDiffFrame(string name)
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            driver.FindElement(txtCompanyL).SendKeys(name);
            Thread.Sleep(6000);
            driver.FindElement(btnIGL).SendKeys("BUS - Business Services");
            Thread.Sleep(4000);
            driver.FindElement(btnAssetClassL).SendKeys("ABL");
            Thread.Sleep(4000);
            driver.FindElement(btnPositonSectorL).SendKeys("Cloud & Enterprise Consulting");
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            string value = driver.FindElement(valAddedPositionL).Text;
            return value;

        }

        //Enter all details and save it.
        public string EnterAndSaveOppValuationPeriodPositionDetailsLWithDiffFrame(string name)
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            driver.FindElement(txtCompanyL).SendKeys(name);
            Thread.Sleep(6000);
            driver.FindElement(btnIGL).SendKeys("BUS - Business Services");
            Thread.Sleep(4000);
            driver.FindElement(btnAssetClassL).SendKeys("ABL");
            Thread.Sleep(4000);
            driver.FindElement(btnPositonSectorL).SendKeys("Cloud & Enterprise Consulting");
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            string value = driver.FindElement(valAddedPositionL).Text;
            driver.SwitchTo().DefaultContent();
            return value;

        }

        //Validate  Opp Valuation Period Position details page is displayed upon clicking added Opp Valuation Period Position  
        public string ValidateOppValPeriodPositionPageUponClickingAddedPeriodPosition()
        {

            driver.FindElement(valAddedPositionL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(2);
            Thread.Sleep(4000);
            string tab = driver.FindElement(titlePositionDetailL).Text;
            driver.FindElement(titlePositionDetailL).Click();
            return tab;
        }

        //Validate Opportunity Valuation Period detail page is displayed upon clicking Back to Valuation button on Opp Valuation Period Position
        public string ValidateOppValPeriodDetailsPageUponClickingBackToValuationButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToValPeriodL, 120);
            driver.FindElement(btnBackToValPeriodL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            string tab = driver.FindElement(titlePeriodDetailL).Text;
            return tab;
        }

        //Validate edit functionality of Period Position
        public string EditFunctionalityOfPeriodPosition(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedPositionL, 120);
            driver.FindElement(valAddedPositionL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(2);
            Thread.Sleep(5000);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(2);
            Thread.Sleep(5000);
            driver.FindElement(txtEditNameL).Clear();
            driver.FindElement(txtEditNameL).SendKeys(name);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(2);
            Thread.Sleep(6000);
            string value = driver.FindElement(valUpdPositionL).Text;
            return value;
        }

        //Validate edit functionality of Period Position
        public string ValidateStatusOfPeriodPositionWithCAO()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 120);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(2);
            Thread.Sleep(5000);
            string value = driver.FindElement(valStatusPeriodPosL).Text;
            return value;
        }

        //Validate Opp Valuation Team member
        public string ValidateSecOppValTeamMember()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secOppValTeamMemL, 60);
            string section = driver.FindElement(secOppValTeamMemL).Text;
            return section;
        }

        //Validate add Team member message
        public string ValidateAddTeamMemberMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgTeamMemL, 60);
            string section = driver.FindElement(msgTeamMemL).Text;
            return section;
        }

        //Validate button Add New Team member
        public string ValidateAddNewTeamMemberButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddTeamMemL, 60);
            string section = driver.FindElement(btnAddTeamMemL).GetAttribute("value");
            return section;
        }


        //Validate button Add New Team member
        public bool ValidateTeamMemberColumns()
        {
            driver.FindElement(btnAddTeamMemL).Click();
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(colTeamMemL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "#", "STAFF", "ROLE", "STATUS", "ACTION" };
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);
            Console.WriteLine(actualValue[2]);
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

        //Validate button Save New Team member
        public string ValidateSaveTeamMemberButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveTeamMemL, 60);
            string section = driver.FindElement(btnSaveTeamMemL).GetAttribute("value");
            return section;
        }

        //Validate Delete link corresponding to added Team member
        public string ValidateDeleteLinkTeamMember()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDeleteTeamL, 60);
            string section = driver.FindElement(lnkDeleteTeamL).Text;
            return section;
        }
        //Save Team members and validate it
        public string SaveTeamMembersAndValidate()
        {
            driver.FindElement(btnRoleL).SendKeys("Associate");
            Thread.Sleep(4000);
            driver.FindElement(btnSaveTeamMemL).Click();
            Thread.Sleep(7000);
            string name = driver.FindElement(valStaffL).Text;
            return name;
        }

        //Save Team members and validate it
        public string SaveTeamMembersAndValidateEng()
        {
            driver.FindElement(btnRoleEngL).SendKeys("Associate");
            Thread.Sleep(4000);
            driver.FindElement(btnSaveTeamMemL).Click();
            Thread.Sleep(8000);
            string name = driver.FindElement(valStaffL).Text;
            return name;
        }

        //Get the value of Role
        public string GetSavedRoleOfStaff()
        {
            string name = driver.FindElement(valRoleL).Text;
            return name;
        }

        //Validate cancel functionality of Team members
        public string ValidateCancelFunctionalityOfTeamMembers()
        {
            driver.FindElement(btnAddTeamMemL).Click();
            Thread.Sleep(5000);
            driver.FindElement(lnkDeleteTeamL).Click();
            Thread.Sleep(3000);
            driver.SwitchTo().Alert().Dismiss();
            string name = driver.FindElement(lnkDeleteTeamL).Text;
            return name;
        }

        //Validate delete functionality of Team members
        public string ValidateDeleteFunctionalityOfTeamMembers()
        {
            driver.FindElement(lnkDeleteTeamL).Click();
            Thread.Sleep(3000);
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(5000);
            try
            {
                string name = driver.FindElement(lnkDeleteTeamL).Displayed.ToString();
                return name;
            }
            catch (Exception)
            {
                return "Team member is deleted";
            }
        }

        public void ClickHLRelatedTab()
        {
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(5000);
            driver.FindElement(tabHLRelatedL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            driver.FindElement(btnNewOppValPeriodL).Click();
        }

        public void ClickBackToOppValPeriodList()
        {
            Thread.Sleep(5000);
            driver.FindElement(btnBackToOppValPeriodList).Click();
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            driver.FindElement(btnNewOppValPeriodL).Click();

        }

        //Validate buttons of existing Valuation Period 
        public bool ValidateButtonsOfExistingValPeriodL()
        {
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(btnExistingImports);
            var actualValue = valRecordTypes.Select(x => x.GetAttribute("value")).ToArray();
            string[] expectedValue = { "Search Valuation Period for Positions", "Back To Valuation Period" };
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);

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

        //Validate the displayed buttons after clicking on Search Valuation Period for Positions
        public bool ValidateDisplayedImportButtonsUponClickingSearchValPeriod()
        {
            driver.FindElement(btnExistingValPeriodL).Click();
            driver.FindElement(btnSearchValPeriodPosL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblImportL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Import Positions Without Team Members", "Import Positions With Team Members" };
            Console.WriteLine(actualValue[0]);
            //Console.WriteLine(actualValue[1]);

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

        //Validate the displayed buttons after clicking on Search Valuation Period for Positions
        public bool ValidateDisplayedBottomButtonsUponClickingSearchValPeriod()
        {
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(btnImportBottomL);
            var actualValue = valRecordTypes.Select(x => x.GetAttribute("value")).ToArray();
            string[] expectedValue = { "Save", "Save & Back To Valuation Period", "Back", "Cancel" };
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);

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

        //Validate import with team members
        public string ValidateImportWithTeamMembers()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chkPositionNameL, 120);
            driver.FindElement(chkPositionNameL).Click();
            driver.FindElement(btnSaveAndBackL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(4000);
            string row = driver.FindElement(valAddedPositionWithTeam).Text;
            return row;
        }

        //Validate import without team members
        public string ValidateImportWithoutTeamMembers()
        {
            driver.FindElement(btnExistingValPeriod2ndL).Click();
            driver.FindElement(btnSearchValPeriodPosL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(4000);

            WebDriverWaits.WaitUntilEleVisible(driver, chkImportWithoutTeamL, 120);
            driver.FindElement(chkImportWithoutTeamL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkPositionNameL, 120);
            driver.FindElement(chkPositionNameL).Click();
            driver.FindElement(btnSaveAndBackL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(4000);
            string row = driver.FindElement(valAddedPositionWithTeam).Text;
            return row;
        }

        //Validate edit functionality of Period Position for deal team member
        public string ValidateEditFunctionalityOfPeriodPositionWithDealTeamMember()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditPeriodPositionL, 120);
            driver.FindElement(lnkEditPeriodPositionL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(4000);
            string row = driver.FindElement(valStatusPeriodPosL).Text;
            return row;

        }

        //Validate cancel functionality of Period Position
        public string ValidateWhenNoIsSelectedUponClickingDeleteButton()
        {
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            //driver.SwitchTo().Frame(0);
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabPeriodPositionL, 160);
            driver.FindElement(tabPeriodPositionL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDelPositionL, 170);
            driver.FindElement(lnkDelPositionL).Click();
            Thread.Sleep(4000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Dismiss();
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedPositionL, 180);
            string value = driver.FindElement(valAddedPositionL).Text;
            return value;
        }

        //Validate delete functionality of Period Position
        public string ValidateWhenYesIsSelectedUponClickingDeleteButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDelPositionL, 160);
            driver.FindElement(lnkDelPositionL).Click();
            Thread.Sleep(4000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedPositionL, 190);
            string value = driver.FindElement(valAddedPositionL).Text;
            return value;
        }

        //Validate delete functionality of Opp Valuation Period 
        public string ValidateDeleteFunctionalityOfOppValPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteL, 160);
            driver.FindElement(btnDeleteL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToOppL, 190);
            string value = driver.FindElement(btnBackToOppL).GetAttribute("value");
            return value;
        }

        //Validate report section of Opp Valuation Period 
        public bool ValidateReportSectionOfOppValPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedValPeriodL, 160);
            driver.FindElement(valAddedValPeriodL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(4000);
            driver.FindElement(btnCloseTabL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(valReportsL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "All Positions and Team Members", "All Positions by Team Members", "Valuation Portfolio Report Fees" };
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);

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

        //Validate delete functionality of Valuation Period for deal team member
        public string ValidateDeleteFunctionalityOfValPeriodWithDealTeamMember()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelL, 120);
            driver.FindElement(btnCancelL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(4000);
            try
            {
                string value = driver.FindElement(btnDeleteL).Text;
                return value;
            }
            catch (Exception)
            {
                return "Delete button is not displayed";
            }

        }

        //Validate delete functionality of Period Position for deal team member
        public string ValidateDeleteFunctionalityOfPeriodPositionWithDealTeamMember()
        {
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(4000);
            driver.FindElement(tabPeriodPositionL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            try
            {
                string value = driver.FindElement(btnDeleteL).Text;
                Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                Thread.Sleep(4000);
                return value;
            }
            catch (Exception)
            {
                Thread.Sleep(4000);
                driver.SwitchTo().DefaultContent();
                Thread.Sleep(4000);
                return "Delete button is not displayed";
            }

        }
    }
}