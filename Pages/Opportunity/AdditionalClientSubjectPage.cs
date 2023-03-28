using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
//using System.Windows.Forms;

namespace SF_Automation.Pages
{
    class AdditionalClientSubjectsPage : BaseClass
    {
        By btnContinue = By.CssSelector("input[value='Continue']");
        By winAdditionalClientSubject = By.CssSelector("span[class='ui-dialog-title']");
        By winAdditionalClient = By.Id("ui-id-2");
        By txtSearch = By.CssSelector("input[name*= 'txtSearch']");
        By btnGo = By.CssSelector("input[name*= 'btnGo']");
        By checkCompany = By.CssSelector("input[name*= 'id50']");
        By btnAddSelected = By.CssSelector("input[value= 'Add Selected']");
        By msgSuccess = By.XPath("//div[contains(text(),'Company Added To Additional Clients')]");
        By btnClose = By.XPath("//span[contains(text(),'Add Additional Client(s)')] /following-sibling::button");
        By btnSubClose = By.XPath("//span[contains(text(),'Add Additional Subject(s)')] /following-sibling::button");
        By msgSubject = By.XPath("//div[contains(text(),'Company Added To Additional Subjects')]");
        By additionalCompany = By.XPath("//*[contains(@id,'pbAdditionalClients')] / tr/td/span/a[contains(text(),'Del')]");
        By additionalSubject = By.XPath("//*[contains(@id,'pbAdditionalSubjects')] / tr/td/span/a[contains(text(),'Del')]");
        By comboClientInterest = By.CssSelector("select[name*='hasAdverseClients']");
        By btnSaveClose = By.CssSelector("input[value='Save & Close']");
        By btnAddClient = By.Id("newClient");
        By btnAddSubject = By.Id("newSubject");
        By titleHLTeam = By.CssSelector("h1 b");  //By.CssSelector("h2[class='mainTitle']");
        By txtStaff = By.CssSelector("input[placeholder*='Begin Typing Name']");
        By btnSave = By.CssSelector("input[value='Save']");
        By listStaff = By.XPath("/html/body/ul");
        By btnReturnToOppor = By.CssSelector("input[value='Return To Opportunity']");
        By linkCompanyName = By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[2]/th/a");
        By linkSubjectName = By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[3]/th/a");
        By txtCompanyType = By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[2]/td[2]");
        By txtCompanyRecType = By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[2]/td[3]");
        By txtSubjectType = By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[4]/td[2]");
        By txtSubjectRecType = By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[4]/td[3]");
        By txtComSubjectName = By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[4]/th/a");
        By btnAdditionalClientSubject = By.CssSelector("input[value*='New Opportunity Client/Subject']");
        By btnMassEditRecords = By.CssSelector("input[value*='Mass Edit Records']");
        By titleMassEditPage = By.XPath("//span[@class='slds-text-heading_small slds-truncate']");
        By btnBackToOpp = By.XPath("//div[1]/span/lightning-button/button");
        By titleOppDetails = By.CssSelector("div[id*='j_id55'] > div.pbHeader > table > tbody > tr > td.pbTitle > h2");
        By btnAdditionalClientSub = By.XPath("//div[2]/span/lightning-button/button");
        By btnEditMassEdit = By.XPath("//header/div[2]/slot/lightning-button/button");
        By txtRefresh = By.XPath("//div[2]/div[2]/span/p");
        By comboTypeMassEdit = By.XPath("//lightning-base-combobox-item[contains(@id,'button-16')]");
        By btnDeleteRecords = By.XPath("//div[3]/span/lightning-button/button");
        By txtAlertMessage = By.XPath("//slot/div/div/h2");
        By btnCloseError = By.XPath("//div/div/div/lightning-button-icon/button");
        By checkRecord1 = By.XPath("//tr[1]/td[1]/div/lightning-input/div/span/label/span[1]");
        By checkRecord = By.XPath("//tr[2]/td[1]/div/lightning-input/div/span/label/span[1]");
        By btnYes = By.XPath("//footer/lightning-button[1]/button");
        By txtType1 = By.XPath("//tr[1]/td[4]/div/lightning-formatted-text");
        By txtType = By.XPath("//tr[1]/td[4]/div/lightning-formatted-text");
        By titleAdditionalClient = By.CssSelector("h2[class='pageDescription']");
        By txtClientSubject = By.CssSelector("span>input[id*='CF00Ni000000D9DcG']");
        By txtClientSubjectEng = By.CssSelector("span>input[id*='D9Dbw']");
        By valNewClient = By.XPath("//div[@id='box-30']/div[1]/div/lightning-formatted-text");
        By valNewClientEng = By.XPath("//table/tbody/tr[5]/th/a");
        By valNewClientEngOther = By.XPath("//table/tbody/tr[4]/th/a");
        By valClientType = By.XPath("//tbody/tr[1]/td[4]/div/lightning-formatted-text");
        By valEngClientType = By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr[5]/td[2]");
        By valClientType1 = By.XPath("//tbody/tr[1]/td[3]/div/lightning-formatted-text");
        By valCompKeyCreditor = By.XPath("//div[@id='box-43']/div[1]/div/lightning-formatted-text");
        By valEngCompKeyCreditor = By.XPath("//table/tbody/tr[6]/th/a");
        By valTypeKeyCreditor = By.XPath("//tbody/tr[2]/td[4]/div/lightning-formatted-text");
        By valEngTypeKeyCreditor = By.XPath("//*[contains(@id,'DbX_body')]/table/tbody/tr[6]/td[2]");
        By btnSaveClientSubject = By.CssSelector("input[value=' Save ']");
        By valType = By.XPath("//button[contains(@id,'button-16')]");
        By valSelectedType = By.XPath("//lightning-base-combobox/div/div[@id='dropdown-element-16']/lightning-base-combobox-item[@aria-checked='true']");
        By colTableColumns = By.XPath("//table/thead/tr/td/div");
        By val2ndClient = By.XPath("//div[@id='box-43']/div[1]/div/lightning-formatted-text");
        By valOtherCred = By.XPath("//table/tbody[2]/tr[1]/td[1]/div/span");
        By val2ndType = By.XPath("//table/tbody[1]/tr[2]/td[4]/div/lightning-formatted-text");
        By val2ndTypeKey = By.XPath("//table/tbody[1]/tr[2]/td[3]/div/lightning-formatted-text");
        By btnSaveRecords = By.XPath("//header/div[2]/slot/lightning-button[1]/button");
        By btnCancelRecords = By.XPath("//header/div[2]/slot/lightning-button[2]/button");
        By txtClientHoldingsMM = By.XPath("//*[@id='input-135']");
        By txtClientHoldingsPer = By.XPath("//*[@id='input-136']");
        By txtClientHoldingsMMEng = By.XPath("//*[@id='input-125']");
        By txtClientHoldingsPerEng = By.XPath("//*[@id='input-126']");
        By valClientHoldingsPer = By.XPath("//table/tbody[1]/tr/td[7]/div/lightning-formatted-text");
        By valClientHoldingsPerEng = By.XPath("//table/tbody[1]/tr[1]/td[7]/div/lightning-formatted-text");
        By valClientHoldingsPerUpd = By.XPath("//div[4]/table/tbody[1]/tr[1]/td[7]/div/lightning-formatted-text");
        By msgSuccessSave = By.XPath("//slot/div[2]/div/h2");
        By msgSuccessSaveKey = By.XPath("//div[2]/slot/div[3]/div/h2");
        By msgSuccessSaveKeyEngage = By.XPath("//div[2]/slot/div[2]/div/h2");
        By txtClientHoldingsMM2nd = By.XPath("//*[@id='input-321']");
        By txtClientHoldingsPer2nd = By.XPath("//*[@id='input-237']");
        By txtClientHoldingsPer2ndEngage = By.XPath("//*[@id='input-113']");
        By txtClientHoldingsPercen2nd = By.XPath("//*[@id='input-322']");
        By txtClientHoldingsMM2ndEng = By.XPath("//*[@id='input-226']");
        By txtClientHoldingsMM2ndEngCred = By.XPath("//*[@id='input-191']");
        By txtClientHoldingsPer2ndEng = By.XPath("//*[@id='input-227']");
        By txtClientHoldingsPer2ndEngCred = By.XPath("//*[@id='input-192']");
        By txtClientHoldingsPer3rd = By.XPath("//*[@id='input-322']");
        By txtRevAllocationContra = By.XPath("//*[@id='input-165']");
        By txtRevAllocationCounterparty = By.XPath("//*[@id='input-230']");
        By txtRevAllocationContraEng = By.XPath("//*[@id='input-142']");
        By txtRevAllocationCounterpartyEng = By.XPath("//*[@id='input-207']");
        By txtRevAllocationEquityEng = By.XPath("//*[@id='input-220']");
        By valRevAllocationContra = By.XPath("//table/tbody[1]/tr/td[5]/div/lightning-formatted-text");
        By valRevAllocationContraEng = By.XPath("//table/tbody[2]/tr/td[4]/div/lightning-formatted-text");
        By txtRevAllocationContra2nd = By.XPath("//*[@id='input-188']");
        By txtKeyCreditorWeighting = By.XPath("//*[@id='input-176']");
        By txtKeyCreditorWeightingEng = By.XPath("//*[@id='input-179']");
        By valKeyCreditorWeighting = By.XPath("//tbody[1]/tr[1]/td[8]/div/lightning-formatted-text");
        By txtDebtHoldingsMM2nd = By.XPath("//*[@id='input-281']");
        By txtDebtHoldingsMM2ndEng = By.XPath("//*[@id='input-241']");
        By txtDebtHoldingsMM2ndEngCred = By.XPath("//*[@id='input-277']");
        By txtRevAllocationOther = By.XPath("//*[@id='input-178']");
        By txtRevAllocationOtherEng = By.XPath("//*[@id='input-168']");
        By txtRevAllocationOther2nd = By.XPath("//*[@id='input-201']");
        By txtRevAllocationOther2ndEng = By.XPath("//*[@id='input-191']");
        By txtRevAllocationPEFirm = By.XPath("//*[@id='input-191']");
        By txtRevAllocationPEFirmEng = By.XPath("//*[@id='input-194']");
        By txtRevAllocationPEFirm2nd = By.XPath("//*[@id='input-214']");
        By txtRevAllocationPEFirm2ndEng = By.XPath("//*[@id='input-217']");
        By txtRevAllocationSub = By.XPath("//*[@id='input-203']");
        By txtRevAllocationSubEng = By.XPath("//*[@id='input-180']");
        By txtRevAllocationSub2nd = By.XPath("//*[@id='input-247']");
        By txtRevAllocationSub2ndEng = By.XPath("//*[@id='input-224']");
        By txtRevAllocationCounter2ndEng = By.XPath("//*[@id='input-230']");
        By txtRevAllocationEquity2ndEng = By.XPath("//*[@id='input-243']");
        By valRevAllocationSub = By.XPath("//table/tbody[1]/tr/td[6]/div/lightning-formatted-text");
        By valRevAllocationSubEng = By.XPath("//table/tbody[2]/tr/td[5]/div/lightning-formatted-text");
        By comboKeyCreditorImpEng = By.XPath("//div[@id='dropdown-element-176']/lightning-base-combobox-item/span[2]/span");
        By comboKeyCreditorImp = By.XPath("//div[@id='dropdown-element-173']/lightning-base-combobox-item/span[2]/span");
        By comboRole = By.XPath("//*[@id='dropdown-element-132']/lightning-base-combobox-item/span[2]/span");
        By comboRoleEng = By.XPath("//*[@id='dropdown-element-122']/lightning-base-combobox-item/span[2]/span");
        By valKeyCreditorImpDefault = By.XPath("//div/button[@id='combobox-button-173']/span");
        By valKeyCreditorImpDefaultEng = By.XPath("//div/button[@id='combobox-button-176']/span");
        By btnKeyCreditor = By.XPath("//button[@id='combobox-button-173']");
        By btnKeyCreditorEng = By.XPath("//button[@id='combobox-button-176']");
        By btnRole = By.XPath("//button[@id='combobox-button-132']");
        By btnRoleEng = By.XPath("//button[@id='combobox-button-122']");
        By valTotalClientHoldings = By.XPath("//table/tbody[2]/tr[2]/td[6]/div/lightning-formatted-text");
        By txtClientHoldingPerEngage = By.XPath("//*[@id='input-198']");
        By btnCloseMessage = By.XPath("//div[2]/div/div/lightning-button-icon/button");
        By txtRevAllocationClient = By.XPath("//*[@id='input-246']");
        By txtRevAllocationClientEngage = By.XPath("//*[@id='input-122']");
        By valRevAllocation = By.XPath("//table/tbody[2]/tr[2]/td[11]/div/lightning-formatted-text");
        By txtKeyCreditorWeighting2nd = By.XPath("//*[@id='input-383']");
        By txtKeyCreditorWeighting2ndEngage = By.XPath("//*[@id='input-156']");
        By valTotalKeyCreditorWeighting = By.XPath("//table/tbody[2]/tr[2]/td[7]/div/lightning-formatted-text");
        By valColumnClientHoldings = By.XPath("//table/thead/tr/td[5]/div/div");
        By valColumnDebtHoldings = By.XPath("//table/thead/tr/td[7]/div/div");
        By valTotalDebtHoldingsMM = By.XPath("//table/tbody[2]/tr[2]/td[4]/div/lightning-formatted-text");
        By valOtherCreditorsDebtHoldingsMM = By.XPath("//table/tbody[2]/tr[1]/td[4]/div/lightning-formatted-text");
        By txtDebtHodlingsKeyCred1 = By.XPath("//*[@id='input-164']");
        By txtDebtHodlingsKeyCred1Eng = By.XPath("//*[@id='input-192']");
        By txtDebtHodlingsKeyCred2 = By.XPath("//*[@id='input-185']");
        By txtDebtHodlingsKeyCred12ndEdit = By.XPath("//*[@id='input-235']");
        By txtDebtHodlingsKeyCred12ndEditEng = By.XPath("//*[@id='input-230']");
        By txtDebtHodlingsKeyCred22ndEdit = By.XPath("//*[@id='input-256']");
        By txtDebtHodlingsKeyCred13rdEdit = By.XPath("//*[@id='input-304']");
        By txtDebtHodlingsKeyCred13rdEditEng = By.XPath("//*[@id='input-268']");
        By txtDebtHodlingsKeyCred23rdEdit = By.XPath("//*[@id='input-325']");
        By btnClientHoldingsHelpIcon = By.XPath("//div[2]/slot/div[3]/table/thead/tr/td[6]/div/div/a");
        By listGCAMember = By.XPath("//li[@class='ui-menu-item']/a/b/b[text()='Mark']");
        By checkInitiator = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[2]/input");
        By checkMarketing = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[3]/input");
        By checkSeller = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[4]/input");
        By checkPrincipal = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[5]/input");
        By checkManager = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[6]/input");
        By checkAssociate = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[7]/input");
        By checkAnalyst = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[8]/input");
        By checkSpecialty = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[9]/input");
        By checkPEHF = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[10]/input");
        By checkIntern1 = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[11]/input");
        By checkIntern = By.XPath("(//*[contains(text(),'Add New Team Member')]/following::td)[11]/following::tr/td[12]/input");
        By listStaff2 = By.XPath("(/html/body/ul/li)[2]/a");

        public void EnterMultipleStaffDetails(string file, int row, int row1, string recordType)
        {
            Thread.Sleep(3000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "DealTeamMembers", row, 1);
            string valStaff1 = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row1, 1);

            WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 120);
            driver.FindElement(txtStaff).SendKeys(valStaff);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valStaff);
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 240);
            driver.FindElement(checkInitiator).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);

            WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 120);
            driver.FindElement(txtStaff).SendKeys(valStaff1);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff2, valStaff1);
            Thread.Sleep(2000);

            if (row1 == 3 && recordType == "CF")
            {
                WebDriverWaits.WaitUntilEleVisible(driver, checkIntern1, 120);
                driver.FindElement(checkIntern1).Click();
                driver.FindElement(btnSave).Click();
                Thread.Sleep(4000);
            }
            else if (row1 == 3 && recordType == "FVA")
            {
                WebDriverWaits.WaitUntilEleVisible(driver, checkIntern, 120);
                driver.FindElement(checkIntern).Click();
                driver.FindElement(btnSave).Click();
                Thread.Sleep(4000);
            }
            else if (row1 == 3 && recordType == "FR")
            {
                WebDriverWaits.WaitUntilEleVisible(driver, checkIntern, 120);
                driver.FindElement(checkIntern).Click();
                driver.FindElement(btnSave).Click();
                Thread.Sleep(4000);
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 120);
                driver.FindElement(checkInitiator).Click();
                driver.FindElement(btnSave).Click();
                Thread.Sleep(4000);
            }

            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOppor);
            driver.FindElement(btnReturnToOppor).Click();
            Thread.Sleep(2000);
        }
        public void EnterMembersToDealTeam(string file)
        {
            Thread.Sleep(3000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            int rowCount = ReadExcelData.GetRowCount(excelPath, "RateSheetManagement");
            string valStaff = "";
            for (int row = 2; row <= rowCount; row++)
            {
                valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 2);
                WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 120);
                driver.FindElement(txtStaff).SendKeys(valStaff);
                Thread.Sleep(5000);

                By staff = By.XPath($"(/html/body/ul/li)[{row - 1}]/a");
                CustomFunctions.SelectValueWithoutSelect(driver, staff, valStaff);
                Thread.Sleep(2000);

                switch (row)
                {
                    case 2:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 240);
                        driver.FindElement(checkInitiator).Click();
                        WebDriverWaits.WaitUntilEleVisible(driver, checkPrincipal, 240);
                        driver.FindElement(checkPrincipal).Click();
                        break;
                    case 3:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkManager, 240);
                        driver.FindElement(checkManager).Click();
                        break;
                    case 4:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkSpecialty, 240);
                        driver.FindElement(checkSpecialty).Click();
                        break;
                    case 5:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkMarketing, 240);
                        driver.FindElement(checkMarketing).Click();
                        break;
                    case 6:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkSeller, 240);
                        driver.FindElement(checkSeller).Click();
                        break;
                    case 7:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkAssociate, 240);
                        driver.FindElement(checkAssociate).Click();
                        break;
                    case 8:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkAnalyst, 240);
                        driver.FindElement(checkAnalyst).Click();
                        break;
                    case 9:
                        WebDriverWaits.WaitUntilEleVisible(driver, checkIntern, 240);
                        driver.FindElement(checkIntern).Click();
                        break;
                }

                driver.FindElement(btnSave).Click();
                Thread.Sleep(10000);
                WebDriverWaits.WaitForPageToLoad(driver, 120);
            }

            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOppor);
            driver.FindElement(btnReturnToOppor).Click();
            Thread.Sleep(5000);
        }

        public void EnterStaffDetailsMultipleRows(string file, int row)
        {
            Thread.Sleep(3000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "DealTeamMembers", row, 1);
            WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 120);
            driver.FindElement(txtStaff).SendKeys(valStaff);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valStaff);
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 240);
            driver.FindElement(checkInitiator).Click();
            driver.FindElement(btnSave).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOppor);
            driver.FindElement(btnReturnToOppor).Click();
        }

        //To enter team member details
        public void EnterHLAndGCAStaffDetails(string file, int row)
        {
            Thread.Sleep(3000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            for (int col = 1; col <= 2; col++)
            {
                string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "DealTeamMembers", row, col);
                WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 120);
                driver.FindElement(txtStaff).SendKeys(valStaff);
                Thread.Sleep(5000);
                if (col == 1)
                {
                    CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valStaff);
                }
                else if (col == 2)
                {
                    CustomFunctions.SelectValueWithoutSelect(driver, listGCAMember, valStaff);
                }
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 240);
                driver.FindElement(checkInitiator).Click();
                driver.FindElement(btnSave).Click();
                Thread.Sleep(10000);
                WebDriverWaits.WaitForPageToLoad(driver, 120);
            }

            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOppor);
            driver.FindElement(btnReturnToOppor).Click();
        }

        public void ClickContinue()
        {
            //Calling wait function--Continue button     
            WebDriverWaits.WaitUntilEleVisible(driver, btnContinue);
            driver.FindElement(btnContinue).Submit();
        }

        public string ValidateAdditionalClientSubjectTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, winAdditionalClientSubject);
            IWebElement titleAdditionalClientSubject = driver.FindElement(winAdditionalClientSubject);
            return titleAdditionalClientSubject.Text;
        }
        public void ClickAddClient()
        {
            Thread.Sleep(3000);
            driver.SwitchTo().Frame(4);
            driver.FindElement(btnAddClient).Click();
        }

        public string ValidateAdditionalClientTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, winAdditionalClient, 40);
            IWebElement titleAdditionalClient = driver.FindElement(winAdditionalClient);
            return titleAdditionalClient.Text;
        }

        //To add additional Client
        public void AddAdditionalClient(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.SwitchTo().Frame(0);
            driver.FindElement(txtSearch).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 33));
            Thread.Sleep(2000);
            driver.FindElement(btnGo).Click();

            //Calling wait function -- to load search results
            WebDriverWaits.WaitUntilEleVisible(driver, checkCompany, 50);
            driver.FindElement(checkCompany).Click();
            driver.FindElement(btnAddSelected).Click();
        }

        //To validate message while adding additional Client
        public string ValidateMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess);
            return driver.FindElement(msgSuccess).Text;
        }

        //To validate additional Client added in Table
        public string ValidateTableDetails()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(4);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose);
            driver.FindElement(btnClose).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, additionalCompany, 50);
            if (driver.FindElement(additionalCompany).Displayed)
                return "True";
            else
                return "False";
        }

        //To close the popup
        public void CloseClientPopUp()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(4);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose);
            driver.FindElement(btnClose).Click();
        }

        //To add additional subject
        public void AddAdditionalSubject(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(btnAddSubject).Click();
            driver.SwitchTo().Frame(0);
            driver.FindElement(txtSearch).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 35));
            Thread.Sleep(2000);
            driver.FindElement(btnGo).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, checkCompany, 50);
            driver.FindElement(checkCompany).Click();
            driver.FindElement(btnAddSelected).Click();
        }
        //To validate message while adding additional subject
        public string ValidateSubjectMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgSubject);
            return driver.FindElement(msgSubject).Text;
        }

        //To close the popup
        public void CloseSubjectPopUp()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(4);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubClose);
            driver.FindElement(btnSubClose).Click();
        }

        //To validate additional Subject in Table
        public string ValidateSubjectTableDetails()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(4);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubClose);
            driver.FindElement(btnSubClose).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, additionalSubject, 50);
            if (driver.FindElement(additionalSubject).Displayed)
                return "True";
            else
                return "False";
        }
        //To select interests of client and Save value
        public void selectClientInterest(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, comboClientInterest);
            IWebElement comboInterest = driver.FindElement(comboClientInterest);
            CustomFunctions.SelectByValue(driver, comboInterest, ReadExcelData.ReadData(excelPath, "AddOpportunity", 34));
            Thread.Sleep(4000);
            driver.FindElement(btnSaveClose).Click();
        }

        //To validate HL Internal Page title
        public string ValidateInternalTeamTitle()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            WebDriverWaits.WaitUntilEleVisible(driver, titleHLTeam, 40);
            IWebElement titleHLInternalTeam = driver.FindElement(titleHLTeam);
            return titleHLInternalTeam.Text;
        }

        //To enter team member details
        public void EnterStaffDetails(string file, int row)
        {
            Thread.Sleep(3000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 14);
            driver.FindElement(txtStaff).SendKeys(valStaff);
            Thread.Sleep(3000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valStaff);
            WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 90);
            driver.FindElement(checkInitiator).Click();
            driver.FindElement(btnSave).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOppor);
            driver.FindElement(btnReturnToOppor).Click();
        }

        //To enter team member details
        public void EnterStaffDetails(string file)
        {
            Thread.Sleep(7000);
            ReadJSONData.Generate("Admin_Data.json");
            Console.WriteLine("Entered staff function");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valStaff = ReadExcelData.ReadData(excelPath, "AddOpportunity", 14);
            Console.WriteLine("Before entering Staff");
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 120);
            driver.FindElement(txtStaff).SendKeys(valStaff);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valStaff);
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 240);
            driver.FindElement(checkInitiator).Click();
            driver.FindElement(btnSave).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOppor);
            driver.FindElement(btnReturnToOppor).Click();
        }

        //To validate additional added client in Additional Clients/Subjects section
        public string ValidateAddedClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCompanyName, 50);
            Thread.Sleep(2000);
            string valCompany = driver.FindElement(linkCompanyName).Text;
            return valCompany;
        }

        //To validate additional added subject in additional Clients/Subjects section
        public string ValidateAddedSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkSubjectName, 50);
            Thread.Sleep(2000);
            string valSubject = driver.FindElement(linkSubjectName).Text;
            return valSubject;
        }

        public string ValidateAddedSubjectWithKeyCreditor()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtComSubjectName, 50);
                Thread.Sleep(2000);
                string valSubject = driver.FindElement(txtComSubjectName).Text;
                return valSubject;
            }
            catch (Exception e)
            {
                return "No new client exists";
            }
        }

        //To validate type of additional added client in Additional Clients/Subjects section
        public string ValidateTypeOfAddedClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyType, 50);
            Thread.Sleep(3000);
            string valCompany = driver.FindElement(txtCompanyType).Text;
            return valCompany;
        }

        //To validate rec type of additional added client in Additional Clients/Subjects section
        public string ValidateRecTypeOfAddedClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyRecType, 50);
            Thread.Sleep(2000);
            string valCompany = driver.FindElement(txtCompanyRecType).Text;
            return valCompany;
        }

        //To validate type of additional added subject in Additional Clients/Subjects section
        public string ValidateTypeOfAddedSubject()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSubjectType, 40);
                Thread.Sleep(2000);
                string valType = driver.FindElement(txtSubjectType).Text;
                return valType;
            }
            catch (Exception e)
            {
                return "No new type exists";
            }
        }

        //To validate rec type of additional added subject in Additional Clients/Subjects section
        public string ValidateRecTypeOfAddedSubject()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSubjectRecType, 40);
                Thread.Sleep(2000);
                string valType = driver.FindElement(txtSubjectRecType).Text;
                return valType;
            }
            catch (Exception e)
            {
                return "No new Rec type exists";
            }
        }

        //Validate Additional Clients/Subjects button
        public string ValidateAdditionalClientSubjectButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdditionalClientSub, 120);
            string name = driver.FindElement(btnAdditionalClientSub).Text;
            return name;
        }

        //Validate Delete Records button
        public string ValidateDeleteRecordsButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteRecords, 120);
            string name = driver.FindElement(btnDeleteRecords).Text;
            return name;
        }

        //Validate Edit button
        public string ValidateEditButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditMassEdit, 120);
            string name = driver.FindElement(btnEditMassEdit).Text;
            return name;
        }

        //Validate Refresh button
        public string ValidateRefreshButton()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtRefresh, 120);
            string name = driver.FindElement(txtRefresh).Text;
            return name;
        }

        //Validate all displayed Type dropdown values
        public bool VerifyTypes()
        {
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//button[contains(@id,'button-16')]")).Click();
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(comboTypeMassEdit);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "All", "Client", "Contra", "Key Creditor", "PE Firm", "Subject", "Other" };
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
        public string Get1stColumn()
        {
            Thread.Sleep(7000);
            string column1 = driver.FindElement(By.XPath("//table/thead/tr/td[1]/div")).Text;
            return column1;
        }

        public bool ValidateTableColumns()
        {
            Thread.Sleep(3000);
            IReadOnlyCollection<IWebElement> valColumns = driver.FindElements(colTableColumns);
            var actualValue = valColumns.Select(x => x.Text).ToArray();
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[1]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[2]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[3]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[4]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[5]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[6]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[7]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[8]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[9]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[10]/div")).Text + "col1");
            Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[11]/div")).Text + "col1");
            string[] expectedValue = { "Client/Subject  ", "Primary?  ", "Type  ", "Role", "Client Holdings (MM) - USD   ", "Client Holdings %  ", "Debt Holdings (MM) - USD   ", "Debt Holdings % Total Debt  ", "Key Creditor Importance  ", "Key Creditor Weighting %  ", "Revenue Allocation %  " };
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

        public bool ValidateTableColumnsForEachType(string name)
        {
            if (name.Equals("Client"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, colTableColumns, 100);
                IReadOnlyCollection<IWebElement> valColumns = driver.FindElements(colTableColumns);
                var actualValue = valColumns.Select(x => x.Text).ToArray();
                string[] expectedValue = { "Client/Subject  ", "Primary?  ", "Type  ", "Role", "Client Holdings (MM) - USD   ", "Client Holdings %  ", "Debt Holdings (MM) - USD   ", "Debt Holdings % Total Debt  ", "Key Creditor Importance  ", "Key Creditor Weighting %  ", "Revenue Allocation %  " };
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
            else if (name.Equals("Key Creditor"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, colTableColumns, 100);
                IReadOnlyCollection<IWebElement> valColumns = driver.FindElements(colTableColumns);
                var actualValue = valColumns.Select(x => x.Text).ToArray();
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[1]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[2]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[3]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[4]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[5]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[6]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[7]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[8]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[9]/div")).Text + "col1");
                string[] expectedValue = { "Client/Subject  ", "Type  ", "Role", "Debt Holdings (MM) - USD   ", "Debt Holdings % Total Debt  ", "Key Creditor Importance  ", "Key Creditor Weighting %  ", "Revenue Allocation %  ", "Notes" };
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
            else if (name.Equals("Subject"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, colTableColumns, 100);
                IReadOnlyCollection<IWebElement> valColumns = driver.FindElements(colTableColumns);
                var actualValue = valColumns.Select(x => x.Text).ToArray();
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[1]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[2]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[3]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[4]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[5]/div")).Text + "col1");

                string[] expectedValue = { "Client/Subject  ", "Primary?  ", "Type  ", "Role", "Revenue Allocation %  " };
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
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, colTableColumns, 100);
                IReadOnlyCollection<IWebElement> valColumns = driver.FindElements(colTableColumns);
                var actualValue = valColumns.Select(x => x.Text).ToArray();
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[1]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[2]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[3]/div")).Text + "col1");
                Console.WriteLine(driver.FindElement(By.XPath("//table/thead/tr/td[4]/div")).Text + "col1");
                string[] expectedValue = { "Client/Subject  ", "Type  ", "Role", "Revenue Allocation %  " };
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

        //Click Delete button without selecting records and validate error message
        public string ClickDeleteAndValidateErrorMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteRecords);
            driver.FindElement(btnDeleteRecords).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtAlertMessage, 100);
            string message = driver.FindElement(txtAlertMessage).Text;
            return message;
        }

        //Close the error message and validate if it is still displayed
        public string ClickCloseAndValidateErrorMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCloseError);
            driver.FindElement(btnCloseError).Click();
            try
            {
                string message = driver.FindElement(txtAlertMessage).Displayed.ToString();
                return message;
            }
            catch (Exception e)
            {
                return "No validate message is displayed";
            }
        }

        //Select a record and click Delete button 
        public string ValidateDeleteRecordsFunctionality()
        {
            Console.WriteLine("Entered into delete function");
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, checkRecord, 140);
            driver.FindElement(checkRecord).Click();
            driver.FindElement(btnDeleteRecords).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnYes, 100);
            driver.FindElement(btnYes).Click();
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtType, 130);
            Thread.Sleep(6000);
            string type = driver.FindElement(txtType).Text;
            if (type.Equals("Client"))
            {
                return "Record is deleted successfully";
            }
            else
            {
                return "Record still exists";
            }
        }

        //Select a record and click Delete button 
        public string ValidateDeleteRecordsFunctionalityOfMassEdit()
        {
            Console.WriteLine("Entered into delete function");
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, checkRecord1, 140);
            driver.FindElement(checkRecord1).Click();
            driver.FindElement(btnDeleteRecords).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnYes, 100);
            driver.FindElement(btnYes).Click();
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtType, 130);
            Thread.Sleep(6000);
            string type = driver.FindElement(txtType).Text;
            if (type.Equals("Key Creditor"))
            {
                return "Record is deleted successfully";
            }
            else
            {
                return "Record still exists";
            }
        }

        //Select a record and click Delete button 
        public string DeleteAddedRecordsAndValidate()
        {
            Console.WriteLine("Entered into delete function");
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, checkRecord, 140);
            driver.FindElement(checkRecord1).Click();
            driver.FindElement(checkRecord).Click();
            driver.FindElement(btnDeleteRecords).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnYes, 100);
            driver.FindElement(btnYes).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtType1, 150);
            string type = driver.FindElement(txtType1).Text;
            if (type.Equals("Subject"))
            {
                return "Records are deleted successfully";
            }
            else
            {
                return "Record still exists";
            }
        }

        //Click Additional Clients/Subjects button
        public string ClickAdditionalClientsSubjectsButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdditionalClientSub, 250);
            driver.FindElement(btnAdditionalClientSub).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, titleAdditionalClient, 120);
            string title = driver.FindElement(titleAdditionalClient).Text;
            return title;
        }

        // To validate save functionality of Additional client
        public string ValidateSaveFunctionalityOfAdditionalClient(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtClientSubject, 80);
            driver.FindElement(txtClientSubject).SendKeys(name);
            driver.FindElement(btnSaveClientSubject).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, valNewClient, 150);
            string value = driver.FindElement(valNewClient).Text;
            return value;
        }

        // To validate save functionality of Engagement - Additional client
        public string ValidateSaveFunctionalityOfEngAdditionalClient(string name, string type)
        {
            if (type.Equals("Creditor Advisors"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtClientSubjectEng, 80);
                driver.FindElement(txtClientSubjectEng).SendKeys(name);
                driver.FindElement(btnSaveClientSubject).Click();
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, valNewClientEng, 150);
                string value = driver.FindElement(valNewClientEng).Text;
                return value;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtClientSubjectEng, 80);
                driver.FindElement(txtClientSubjectEng).SendKeys(name);
                driver.FindElement(btnSaveClientSubject).Click();
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, valNewClientEngOther, 150);
                string value = driver.FindElement(valNewClientEngOther).Text;
                return value;
            }
        }

        //Get type of added additional client record
        public string GetTypeOfAdditionalClient(string name)
        {

            if (name.Equals("Client") || name.Equals("Subject"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valClientType, 100);
                string value = driver.FindElement(valClientType).Text;
                return value;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valClientType1, 100);
                string value = driver.FindElement(valClientType1).Text;
                return value;
            }
        }

        //Get type of added additional client record
        public string GetTypeOfAdditionalClientWithAllOption()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valClientType, 100);
            string value = driver.FindElement(valClientType).Text;
            return value;
        }

        //Get type of added Engagement additional client record
        public string GetTypeOfEngAdditionalClientWithAllOption()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEngClientType, 130);
            string value = driver.FindElement(valEngClientType).Text;
            return value;
        }

        //Validate the company name of Key Creditors 
        public string GetCompanyNameOfKeyCreditor()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valCompKeyCreditor, 40);
                string value = driver.FindElement(valCompKeyCreditor).Text;
                return value;
            }
            catch (Exception e)
            {
                return "No new client exists";
            }
        }
        //Validate the company name of Key Creditors of Engagement
        public string GetCompanyNameOfEngKeyCreditor()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valEngCompKeyCreditor, 40);
                string value = driver.FindElement(valEngCompKeyCreditor).Text;
                return value;
            }
            catch (Exception e)
            {
                return "No new client exists";
            }
        }
        //Validate the type of Key Creditors 
        public string GetTypeOfKeyCreditor()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valTypeKeyCreditor, 40);
                string value = driver.FindElement(valTypeKeyCreditor).Text;
                return value;
            }
            catch (Exception e)
            {
                return "Key Creditor";
            }
        }

        //Validate the type of Key Creditors 
        public string GetTypeOfEngKeyCreditor()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valEngTypeKeyCreditor, 40);
                string value = driver.FindElement(valEngTypeKeyCreditor).Text;
                return value;
            }
            catch (Exception e)
            {
                return "Key Creditor";
            }
        }

        //Validate additional Subject added from Additional Client/Subject Pop up
        public string ValidateAdditionalSubjectFromPopUp(string name)
        {
            Thread.Sleep(2000);
            string value = driver.FindElement(By.XPath("//div/div[1]/div/lightning-formatted-text[text()='" + name + "']")).Displayed.ToString();
            if (value.Equals("True"))
            {
                string type = driver.FindElement(By.XPath("//div/div[1]/div/lightning-formatted-text[text()='" + name + "']/ancestor::tr/td[4]/div/lightning-formatted-text")).Text;
                return type;
            }
            else
            {
                return "Not required value";
            }
        }



        //Select value from Type dropdown and validate the displayed values
        public string SelectValueFromTypeDropdown(string name)
        {
            Thread.Sleep(5000);
            driver.FindElement(valType).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div[@id='dropdown-element-16']/lightning-base-combobox-item/span[2]/span[text()='" + name + "']")).Click();
            Thread.Sleep(6000);
            //string value = driver.FindElement(valSelectedType).Text;
            string value = driver.FindElement(valSelectedType).GetAttribute("data-value");
            return value;
        }

        //Get the displayed Client/Subject
        public string GetClientSubjectValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNewClient, 150);
            string value = driver.FindElement(valNewClient).Text;
            return value;
        }

        //Get 2nd displayed Client/Subject
        public string Get2ndClientSubjectValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, val2ndClient, 150);
            string value = driver.FindElement(val2ndClient).Text;
            return value;
        }

        //Get Other Creditors
        public string GetOtherCreditorsValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valOtherCred, 150);
            string value = driver.FindElement(valOtherCred).Text;
            return value;
        }

        //Get 2nd type of added additional client record
        public string Get2ndTypeOfAdditionalClient(string type, string name)
        {

            if (name.Equals("Client") || name.Equals("Subject"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, val2ndType, 100);
                string value = driver.FindElement(val2ndType).Text;
                return value;
            }
            else if (type.Equals("Creditor Advisors") && name.Equals("Key Creditor"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, val2ndTypeKey, 100);
                string value = driver.FindElement(val2ndTypeKey).Text;
                return value;
            }
            else
            {
                return "No 2nd company exists";
            }
        }

        //Click Edit button and validate Save button
        public string ClickEditButtonAndValidateSaveButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditMassEdit, 120);
            driver.FindElement(btnEditMassEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveRecords, 110);
            string name = driver.FindElement(btnSaveRecords).Text;
            return name;
        }

        //Validate Cancel button
        public string ValidateCancelButton()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnCancelRecords, 50);
                string name = driver.FindElement(btnCancelRecords).Text;
                return name;
            }
            catch (Exception e)
            {
                return "Cancel button is not displayed";
            }
        }

        //Validate Cancel Functionalities
        public string ValidateCancelFunctionalityOfMassEdit(string value, string type)
        {
            if (type.Contains("Client"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtClientHoldingsMM, 140);
                driver.FindElement(txtClientHoldingsMM).Clear();
                driver.FindElement(txtClientHoldingsMM).SendKeys(value);
                driver.FindElement(txtClientHoldingsPer).Clear();
                driver.FindElement(txtClientHoldingsPer).SendKeys(value);
                driver.FindElement(btnCancelRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valClientHoldingsPer, 130);
                string client = driver.FindElement(valClientHoldingsPer).Text;
                return client;
            }
            else if (type.Contains("Key Creditor"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtKeyCreditorWeighting, 140);
                driver.FindElement(txtKeyCreditorWeighting).Clear();
                driver.FindElement(txtKeyCreditorWeighting).SendKeys(value);
                driver.FindElement(btnCancelRecords).Click();
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, valKeyCreditorWeighting, 130);
                string client = driver.FindElement(valKeyCreditorWeighting).Text;
                return client;
            }
            else if (type.Contains("Other"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationOther, 140);
                driver.FindElement(txtRevAllocationOther).Clear();
                driver.FindElement(txtRevAllocationOther).SendKeys(value);
                driver.FindElement(btnCancelRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valRevAllocationContra, 130);
                string client = driver.FindElement(valRevAllocationContra).Text;
                return client;
            }
            else if (type.Contains("PE Firm"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationPEFirm, 140);
                driver.FindElement(txtRevAllocationPEFirm).Clear();
                driver.FindElement(txtRevAllocationPEFirm).SendKeys(value);
                driver.FindElement(btnCancelRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valRevAllocationContra, 130);
                string client = driver.FindElement(valRevAllocationContra).Text;
                return client;
            }
            else if (type.Contains("Subject"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationSub, 140);
                driver.FindElement(txtRevAllocationSub).Clear();
                driver.FindElement(txtRevAllocationSub).SendKeys(value);
                driver.FindElement(btnCancelRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valRevAllocationSub, 130);
                string client = driver.FindElement(valRevAllocationSub).Text;
                return client;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationContra, 140);
                driver.FindElement(txtRevAllocationContra).Clear();
                driver.FindElement(txtRevAllocationContra).SendKeys(value);
                driver.FindElement(btnCancelRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valRevAllocationContra, 130);
                string client = driver.FindElement(valRevAllocationContra).Text;
                return client;
            }
        }

        //Validate Cancel Functionalities
        public string ValidateCancelFunctionalityOfMassEditOfEngagement(string value, string type)
        {
            if (type.Contains("Client"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtClientHoldingsMMEng, 140);
                driver.FindElement(txtClientHoldingsMMEng).Clear();
                driver.FindElement(txtClientHoldingsMMEng).SendKeys(value);
                driver.FindElement(txtClientHoldingsPerEng).Clear();
                driver.FindElement(txtClientHoldingsPerEng).SendKeys(value);
                driver.FindElement(btnCancelRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valClientHoldingsPerEng, 130);
                string client = driver.FindElement(valClientHoldingsPerEng).Text;
                return client;
            }
            else if (type.Contains("Key Creditor"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtKeyCreditorWeightingEng, 140);
                driver.FindElement(txtKeyCreditorWeightingEng).Clear();
                driver.FindElement(txtKeyCreditorWeightingEng).SendKeys(value);
                driver.FindElement(btnCancelRecords).Click();
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, valKeyCreditorWeighting, 130);
                string client = driver.FindElement(valKeyCreditorWeighting).Text;
                return client;
            }
            else if (type.Contains("Other"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationOtherEng, 140);
                driver.FindElement(txtRevAllocationOtherEng).Clear();
                driver.FindElement(txtRevAllocationOtherEng).SendKeys(value);
                driver.FindElement(btnCancelRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valRevAllocationContraEng, 130);
                string client = driver.FindElement(valRevAllocationContraEng).Text;
                return client;
            }
            else if (type.Contains("PE Firm"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationPEFirmEng, 140);
                driver.FindElement(txtRevAllocationPEFirmEng).Clear();
                driver.FindElement(txtRevAllocationPEFirmEng).SendKeys(value);
                driver.FindElement(btnCancelRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valRevAllocationContraEng, 130);
                string client = driver.FindElement(valRevAllocationContraEng).Text;
                return client;
            }
            else if (type.Contains("Subject"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationSubEng, 140);
                driver.FindElement(txtRevAllocationSubEng).Clear();
                driver.FindElement(txtRevAllocationSubEng).SendKeys(value);
                driver.FindElement(btnCancelRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valRevAllocationSubEng, 130);
                string client = driver.FindElement(valRevAllocationSubEng).Text;
                return client;
            }
            else if (type.Contains("Counterparty"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationCounterpartyEng, 140);
                driver.FindElement(txtRevAllocationCounterpartyEng).Clear();
                driver.FindElement(txtRevAllocationCounterpartyEng).SendKeys(value);
                driver.FindElement(btnCancelRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valRevAllocationContraEng, 130);
                string client = driver.FindElement(valRevAllocationContraEng).Text;
                return client;
            }
            else if (type.Contains("Equity Holder"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationEquityEng, 140);
                driver.FindElement(txtRevAllocationEquityEng).Clear();
                driver.FindElement(txtRevAllocationEquityEng).SendKeys(value);
                driver.FindElement(btnCancelRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valRevAllocationContraEng, 130);
                string client = driver.FindElement(valRevAllocationContraEng).Text;
                return client;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationContraEng, 140);
                driver.FindElement(txtRevAllocationContraEng).Clear();
                driver.FindElement(txtRevAllocationContraEng).SendKeys(value);
                driver.FindElement(btnCancelRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valRevAllocationContraEng, 130);
                string client = driver.FindElement(valRevAllocationContraEng).Text;
                return client;
            }
        }

        //Validate Save Functionalities
        public string ValidateSaveFunctionalityOfMassEdit(string value, string type)
        {
            if (type.Contains("Client"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtClientHoldingsMM2nd, 300);
                driver.FindElement(txtClientHoldingsMM2nd).Clear();
                driver.FindElement(txtClientHoldingsMM2nd).SendKeys(value);
                Thread.Sleep(4000);
                driver.FindElement(txtClientHoldingsPercen2nd).Clear();
                driver.FindElement(txtClientHoldingsPercen2nd).SendKeys(value);
                Thread.Sleep(4000);
                driver.FindElement(btnSaveRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 140);
                string client = driver.FindElement(msgSuccessSave).Text;
                return client;
            }
            else if (type.Contains("Key Creditor"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtDebtHoldingsMM2nd, 140);
                driver.FindElement(txtDebtHoldingsMM2nd).Clear();
                driver.FindElement(txtDebtHoldingsMM2nd).SendKeys(value);
                Thread.Sleep(3000);
                driver.FindElement(btnSaveRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 130);
                string client = driver.FindElement(msgSuccessSave).Text;
                return client;
            }
            else if (type.Contains("Other"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationOther2nd, 140);
                driver.FindElement(txtRevAllocationOther2nd).Clear();
                driver.FindElement(txtRevAllocationOther2nd).SendKeys(value);
                Thread.Sleep(6000);
                driver.FindElement(btnSaveRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 130);
                string client = driver.FindElement(msgSuccessSave).Text;
                return client;
            }
            else if (type.Contains("PE Firm"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationPEFirm2nd, 140);
                driver.FindElement(txtRevAllocationPEFirm2nd).Clear();
                driver.FindElement(txtRevAllocationPEFirm2nd).SendKeys(value);
                Thread.Sleep(4000);
                driver.FindElement(btnSaveRecords).Click();
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 130);
                string client = driver.FindElement(msgSuccessSave).Text;
                return client;
            }
            else if (type.Contains("Subject"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationSub2nd, 140);
                driver.FindElement(txtRevAllocationSub2nd).Clear();
                driver.FindElement(txtRevAllocationSub2nd).SendKeys(value);
                Thread.Sleep(4000);
                driver.FindElement(btnSaveRecords).Click();
                Thread.Sleep(6000);
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 130);
                string client = driver.FindElement(msgSuccessSave).Text;
                return client;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationContra2nd, 140);
                driver.FindElement(txtRevAllocationContra2nd).Clear();
                driver.FindElement(txtRevAllocationContra2nd).SendKeys(value);
                Thread.Sleep(43000);
                driver.FindElement(btnSaveRecords).Click();
                Thread.Sleep(4000);
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 130);
                string client = driver.FindElement(msgSuccessSave).Text;
                return client;
            }
        }

        //Validate Save Functionalities
        public string ValidateSaveFunctionalityOfMassEditOfEngagement(string value, string type)
        {
            if (type.Contains("Client") && value.Contains("Creditor Advisors"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtClientHoldingsMM2ndEngCred, 300);
                driver.FindElement(txtClientHoldingsMM2ndEngCred).Clear();
                driver.FindElement(txtClientHoldingsMM2ndEngCred).SendKeys("10");
                driver.FindElement(txtClientHoldingsPer2ndEngCred).Clear();
                driver.FindElement(txtClientHoldingsPer2ndEngCred).SendKeys("10");
                Thread.Sleep(4000);
                driver.FindElement(btnSaveRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 140);
                string client = driver.FindElement(msgSuccessSave).Text;
                return client;
            }
            else if (type.Contains("Client") && value.Contains("Debtor Advisors"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtClientHoldingsMM2ndEng, 300);
                driver.FindElement(txtClientHoldingsMM2ndEng).Clear();
                driver.FindElement(txtClientHoldingsMM2ndEng).SendKeys(value);
                driver.FindElement(txtClientHoldingsPer2ndEng).Clear();
                driver.FindElement(txtClientHoldingsPer2ndEng).SendKeys(value);
                Thread.Sleep(4000);
                driver.FindElement(btnSaveRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 140);
                string client = driver.FindElement(msgSuccessSave).Text;
                return client;
            }
            else if (type.Contains("Key Creditor") && value.Contains("Creditor Advisors"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtDebtHoldingsMM2ndEngCred, 140);
                driver.FindElement(txtDebtHoldingsMM2ndEngCred).Clear();
                driver.FindElement(txtDebtHoldingsMM2ndEngCred).SendKeys("10");
                Thread.Sleep(3000);
                driver.FindElement(btnSaveRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 130);
                string client = driver.FindElement(msgSuccessSave).Text;
                return client;
            }
            else if (type.Contains("Key Creditor") && value.Contains("Debtor Advisors"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtDebtHoldingsMM2ndEng, 140);
                driver.FindElement(txtDebtHoldingsMM2ndEng).Clear();
                driver.FindElement(txtDebtHoldingsMM2ndEng).SendKeys("10");
                Thread.Sleep(3000);
                driver.FindElement(btnSaveRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 130);
                string client = driver.FindElement(msgSuccessSave).Text;
                return client;
            }
            else if (type.Contains("Other"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationOther2ndEng, 140);
                driver.FindElement(txtRevAllocationOther2ndEng).Clear();
                driver.FindElement(txtRevAllocationOther2ndEng).SendKeys("10");
                driver.FindElement(btnSaveRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 130);
                string client = driver.FindElement(msgSuccessSave).Text;
                return client;
            }
            else if (type.Contains("PE Firm"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationPEFirm2ndEng, 140);
                driver.FindElement(txtRevAllocationPEFirm2ndEng).Clear();
                Thread.Sleep(4000);
                driver.FindElement(txtRevAllocationPEFirm2ndEng).SendKeys("10");
                Thread.Sleep(6000);
                driver.FindElement(btnSaveRecords).Click();
                Thread.Sleep(6000);
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 130);
                string client = driver.FindElement(msgSuccessSave).Text;
                return client;
            }
            else if (type.Contains("Subject"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationSub2ndEng, 140);
                driver.FindElement(txtRevAllocationSub2ndEng).Clear();
                driver.FindElement(txtRevAllocationSub2ndEng).SendKeys("10");
                driver.FindElement(btnSaveRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 130);
                string client = driver.FindElement(msgSuccessSave).Text;
                return client;
            }

            else if (type.Contains("Counterparty"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationCounter2ndEng, 140);
                driver.FindElement(txtRevAllocationCounter2ndEng).Clear();
                driver.FindElement(txtRevAllocationCounter2ndEng).SendKeys("10");
                driver.FindElement(btnSaveRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 130);
                string client = driver.FindElement(msgSuccessSave).Text;
                return client;
            }

            else if (type.Contains("Equity Holder"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationEquity2ndEng, 140);
                driver.FindElement(txtRevAllocationEquity2ndEng).Clear();
                driver.FindElement(txtRevAllocationEquity2ndEng).SendKeys("10");
                driver.FindElement(btnSaveRecords).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 130);
                string client = driver.FindElement(msgSuccessSave).Text;
                return client;
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationContra, 140);
                driver.FindElement(txtRevAllocationContra).Clear();
                driver.FindElement(txtRevAllocationContra).SendKeys("10");
                Thread.Sleep(43000);
                driver.FindElement(btnSaveRecords).Click();
                Thread.Sleep(4000);
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 130);
                string client = driver.FindElement(msgSuccessSave).Text;
                return client;
            }
        }

        //Validate ClientHoldingsPerValue
        public string ValidateUpdatedClientHoldingsPer(string type)
        {
            Thread.Sleep(3000);
            if (type.Contains("Client"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valClientHoldingsPerUpd, 170);
                string client = driver.FindElement(valClientHoldingsPerUpd).Text;
                return client;
            }
            else if (type.Contains("Key Creditor"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valKeyCreditorWeighting, 150);
                string client = driver.FindElement(valKeyCreditorWeighting).Text;
                return client;
            }
            else if (type.Contains("Subject"))
            {
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, valRevAllocationSub, 180);
                string client = driver.FindElement(valRevAllocationSub).Text;
                //driver.SwitchTo().DefaultContent(); for 8300
                return client;
            }
            else
            {
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, valRevAllocationContra, 160);
                string client = driver.FindElement(valRevAllocationContra).Text;
                return client;
            }
        }

        //Validate Key Creditor Importance Values
        public bool VerifyKeyCreditorImpValues()
        {
            driver.FindElement(btnKeyCreditor).Click();
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(comboKeyCreditorImp);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "High", "Medium", "Low" };
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

        //Validate Key Creditor Importance Values
        public bool VerifyKeyCreditorImpValuesInEngagement()
        {
            driver.FindElement(btnKeyCreditorEng).Click();
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(comboKeyCreditorImpEng);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "High", "Medium", "Low" };
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
        //Validate Role Values
        public bool VerifyRoleValues()
        {
            Console.WriteLine("In roles functions");
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnRole, 140);
            driver.FindElement(btnRole).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboRole, 160);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(comboRole);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Adverse", "Buyer Contact", "Buyer Financial Advisor", "Buyer Legal Counsel", "Equity Holder", "Financial Advisor", "Financial Advisor to Creditor", "Financial Advisor to Debtor", "Legal Advisor to Creditor", "Legal Advisor to Debtor", "Legal Counsel", "Lender", "Majority Owner", "Management Contact", "Minority Owner", "Other", "Post-Transaction", "Pre-Transaction", "Primary Contact", "Seller Legal Counsel", "Seller Majority Owner", "Seller Management Contact" };
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


        //Validate Role Values
        public bool VerifyRoleValuesInEngagement()
        {
            Console.WriteLine("In roles functions");
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnRoleEng, 140);
            driver.FindElement(btnRoleEng).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboRoleEng, 160);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(comboRoleEng);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "--None--", "Adverse", "Buyer Contact", "Buyer Financial Advisor", "Buyer Legal Counsel", "Equity Holder", "Financial Advisor", "Financial Advisor to Creditor", "Financial Advisor to Debtor", "Legal Advisor to Creditor", "Legal Advisor to Debtor", "Legal Counsel", "Lender", "Majority Owner", "Management Contact", "Minority Owner", "Other", "Post-Transaction", "Pre-Transaction", "Primary Contact", "Seller Legal Counsel", "Seller Majority Owner", "Seller Management Contact" };
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

        //Get default value of Key Creditor Importance
        public string GetDefaultValueOfKeyCreditorImportance()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, valKeyCreditorImpDefault, 190);
            string value = driver.FindElement(valKeyCreditorImpDefault).Text;
            return value;
        }

        //Get default value of Key Creditor Importance
        public string GetDefaultValueOfKeyCreditorImportanceInEngagement()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, valKeyCreditorImpDefaultEng, 190);
            string value = driver.FindElement(valKeyCreditorImpDefaultEng).Text;
            return value;
        }

        //Get Client Holdings % message
        public string ValidateErrorMessageUponEnteringClientHoldingsMoreThan100()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtClientHoldingsPer2nd, 160);
            driver.FindElement(txtClientHoldingsPer2nd).Clear();
            Console.WriteLine("220");
            driver.FindElement(txtClientHoldingsPer2nd).SendKeys("220");
            Thread.Sleep(4000);
            driver.FindElement(btnSaveRecords).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 140);
            Thread.Sleep(7000);
            string message = driver.FindElement(msgSuccessSave).Text;
            driver.FindElement(btnCloseMessage).Click();
            return message;
        }
        //Get Client Holdings % message
        public string ValidateErrorMessageUponEnteringClientHoldingsMoreThan100InEng()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtClientHoldingsPer2ndEngage, 160);
            driver.FindElement(txtClientHoldingsPer2ndEngage).Clear();
            Console.WriteLine("220");
            driver.FindElement(txtClientHoldingsPer2ndEngage).SendKeys("220");
            Thread.Sleep(6000);
            driver.FindElement(btnSaveRecords).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 140);
            Thread.Sleep(4000);
            string message = driver.FindElement(msgSuccessSave).Text;
            driver.FindElement(btnCloseMessage).Click();
            return message;
        }

        //Save Client Holding%
        public void SaveClientHoldings()
        {
            driver.FindElement(txtClientHoldingPerEngage).SendKeys("10");
            Thread.Sleep(5000);
            driver.FindElement(btnSaveRecords).Click();
        }


        //Validate color code of Total of Client Holdings % 
        public string GetColourCodeOfTotalOfClientHoldings()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valTotalClientHoldings, 200);
            string value = driver.FindElement(valTotalClientHoldings).GetAttribute("class");
            return value;
        }

        //Get Revenue  Holdings % message
        public string ValidateErrorMessageUponEnteringRevenueAllocationMoreThan100()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtClientHoldingsPer2nd, 160);
            driver.FindElement(txtClientHoldingsPer2nd).Clear();
            Thread.Sleep(2000);
            driver.FindElement(txtRevAllocationClient).Clear();
            Thread.Sleep(2000);
            driver.FindElement(txtRevAllocationClient).SendKeys("235");
            Thread.Sleep(5000);
            driver.FindElement(btnSaveRecords).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 140);
            string message = driver.FindElement(msgSuccessSave).Text;
            driver.FindElement(btnCloseMessage).Click();
            return message;
        }

        //Get Revenue  Holdings % message
        public string ValidateErrorMessageUponEnteringRevenueAllocationMoreThan100InEng()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtClientHoldingsPer2ndEngage, 160);
            driver.FindElement(txtClientHoldingsPer2ndEngage).Clear();
            Thread.Sleep(2000);
            driver.FindElement(txtRevAllocationClientEngage).Clear();
            Thread.Sleep(2000);
            driver.FindElement(txtRevAllocationClientEngage).SendKeys("235");
            Thread.Sleep(5000);
            driver.FindElement(btnSaveRecords).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSave, 140);
            string message = driver.FindElement(msgSuccessSave).Text;
            driver.FindElement(btnCloseMessage).Click();
            return message;
        }

        //Validate color code of Total of Revenue Allocation %
        public string GetColourCodePostSavingRevenueAllocationLessThan100()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationClient, 160);
            driver.FindElement(txtRevAllocationClient).Clear();
            Thread.Sleep(2000);
            driver.FindElement(txtRevAllocationClient).SendKeys("10");
            Thread.Sleep(3000);
            driver.FindElement(btnSaveRecords).Click();
            Thread.Sleep(6000);
            string colour = driver.FindElement(valRevAllocation).GetAttribute("class");
            return colour;
        }

        //Validate color code of Total of Revenue Allocation %
        public string GetColourCodePostSavingRevenueAllocationLessThan100InEng()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtRevAllocationClientEngage, 200);
            driver.FindElement(txtRevAllocationClientEngage).Clear();
            Thread.Sleep(2000);
            driver.FindElement(txtRevAllocationClientEngage).SendKeys("10");
            Thread.Sleep(3000);
            driver.FindElement(btnSaveRecords).Click();
            Thread.Sleep(8000);
            string colour = driver.FindElement(valRevAllocation).GetAttribute("class");
            return colour;
        }

        //Click Edit button
        public void ClickEditButton()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditMassEdit, 120);
            driver.FindElement(btnEditMassEdit).Click();
        }

        //Validate color code of Total of Key Creditor Weighting %
        public string GetColourCodePostSavingKeyCreditorWeightingLessThan100()
        {
            Thread.Sleep(6000);
            string colour = driver.FindElement(valTotalKeyCreditorWeighting).GetAttribute("class");
            return colour;
        }

        //Get Key Creditor Weighting % message
        public string ValidateErrorMessageUponEnteringKeyCreditorWeightingMoreThan100()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditMassEdit, 160);
            driver.FindElement(btnEditMassEdit).Click();
            driver.FindElement(txtKeyCreditorWeighting2nd).Clear();
            Console.WriteLine("220");
            Thread.Sleep(3000);
            driver.FindElement(txtKeyCreditorWeighting2nd).SendKeys("235");
            Thread.Sleep(4000);
            driver.FindElement(btnSaveRecords).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSaveKey, 140);
            string message = driver.FindElement(msgSuccessSaveKey).Text;
            //driver.FindElement(btnCloseMessage).Click();
            return message;
        }

        //Get Key Creditor Weighting % message
        public string ValidateErrorMessageUponEnteringKeyCreditorWeightingMoreThan100InEng()
        {
            Thread.Sleep(3000);
            driver.FindElement(txtKeyCreditorWeighting2ndEngage).Clear();
            driver.FindElement(txtKeyCreditorWeighting2ndEngage).SendKeys("235");
            Thread.Sleep(8000);
            driver.FindElement(btnSaveRecords).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccessSaveKeyEngage, 140);
            string message = driver.FindElement(msgSuccessSaveKeyEngage).Text;
            //driver.FindElement(btnCloseMessage).Click();
            return message;
        }

        //Save Key Creditor Weighting %
        public void SaveKeyCreditorWeighting()
        {
            Thread.Sleep(3000);
            driver.FindElement(txtKeyCreditorWeighting2ndEngage).Clear();
            driver.FindElement(txtKeyCreditorWeighting2ndEngage).SendKeys("10");
            Thread.Sleep(6000);
            driver.FindElement(btnSaveRecords).Click();
        }


        //Get column name of Client Holdings (MM)
        public string GetClientHoldingsMM()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, valColumnClientHoldings, 70);
            string colName = driver.FindElement(valColumnClientHoldings).Text;
            return colName;
        }

        //Get column name of Debt Holdings (MM)
        public string GetDebtHoldingsMM()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, valColumnDebtHoldings, 70);
            string colName = driver.FindElement(valColumnDebtHoldings).Text;
            return colName;
        }

        //Get Total Debt Holdings MM Value
        public string GetTotalDebtHoldingsMM()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, valTotalDebtHoldingsMM, 70);
            string debt = driver.FindElement(valTotalDebtHoldingsMM).Text;
            return debt;
        }

        //Clear all Debt Holdings
        public void ClearAllDebtHoldings()
        {
            Thread.Sleep(4000);
            driver.FindElement(txtDebtHodlingsKeyCred1).Clear();
            Thread.Sleep(2000);
            driver.FindElement(txtDebtHodlingsKeyCred2).Clear();
            Thread.Sleep(3000);
            driver.FindElement(btnSaveRecords).Click();
        }

        //Clear all Debt Holdings of Engagement
        public void ClearAllDebtHoldingsOfEngagement()
        {
            Thread.Sleep(4000);
            driver.FindElement(txtDebtHodlingsKeyCred1Eng).Clear();
            driver.FindElement(btnSaveRecords).Click();
        }

        //Get Other Creditors of Debt Holdings MM Value
        public string GetOtherCreditorsOfDebtHoldingsMM()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, valOtherCreditorsDebtHoldingsMM, 70);
            string creditors = driver.FindElement(valOtherCreditorsDebtHoldingsMM).Text;
            return creditors;
        }

        //Update  all key creditors
        public void UpdateAllDebtHoldings()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditMassEdit, 150);
            driver.FindElement(btnEditMassEdit).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtDebtHodlingsKeyCred12ndEdit).SendKeys("50");
            Thread.Sleep(3000);
            driver.FindElement(txtDebtHodlingsKeyCred22ndEdit).SendKeys("20");
            Thread.Sleep(3000);
            driver.FindElement(btnSaveRecords).Click();
        }

        //Update  all key creditors
        public void UpdateAllDebtHoldingsOfEngagement()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditMassEdit, 150);
            driver.FindElement(btnEditMassEdit).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtDebtHodlingsKeyCred12ndEditEng).SendKeys("55");
            Thread.Sleep(6000);
            driver.FindElement(btnSaveRecords).Click();
        }

        //Clear all Debt Holdings again
        public void ResetAllDebtHoldings()
        {
            driver.FindElement(btnEditMassEdit).Click();
            Thread.Sleep(2000);
            driver.FindElement(txtDebtHodlingsKeyCred13rdEdit).Clear();
            driver.FindElement(txtDebtHodlingsKeyCred23rdEdit).Clear();
            driver.FindElement(btnSaveRecords).Click();
            Thread.Sleep(3000);
            driver.SwitchTo().DefaultContent();
        }


        //Clear all Debt Holdings again
        public void ResetAllDebtHoldingsOfEngagement()
        {
            driver.FindElement(btnEditMassEdit).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtDebtHodlingsKeyCred13rdEditEng).Clear();
            driver.FindElement(btnSaveRecords).Click();
            Thread.Sleep(3000);
            // driver.SwitchTo().DefaultContent();
        }

        //Click to Back To Opportunity button
        public void ClickBackToOpportunityButton()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToOpp, 150);
            driver.FindElement(btnBackToOpp).Click();
        }

        //Get Help text displayed on Client Holdings%
        public string GetClientHoldingsHelpText()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnClientHoldingsHelpIcon, 150);
            driver.FindElement(btnClientHoldingsHelpIcon).Click();
            string text = driver.FindElement(btnClientHoldingsHelpIcon).GetAttribute("title");
            Console.WriteLine("text: " + text);
            //string actualText = text.Replace("\r\n", "").ToString();
            return text;
        }
    }
}