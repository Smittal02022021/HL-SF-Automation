using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SF_Automation.Pages.Engagement
{
    class ValuationPeriods : BaseClass
    {
        By btnNewEngValPeriod = By.CssSelector("input[value='New Engagement Valuation Period']");
        By lblNewEngValPeriod = By.CssSelector("h2[class='pageDescription']");
        By txtName = By.CssSelector("input[name*='id30']");
        By lnkValDate = By.CssSelector("tbody > tr:nth-child(3) > td:nth-child(2) > div > span > span > a");
        By lnkClientFinalDeadLine = By.CssSelector("tbody > tr:nth-child(7) > td:nth-child(2) > div > span > span > a");
        By btnSave = By.CssSelector("input[value='Save']");
        By lblEngValPeriodDetail = By.CssSelector("h2[class='mainTitle']");
        By btnNewEngValPeriodPosition = By.CssSelector("input[value='New Eng Valuation Period Position']");
        By btnCompany = By.CssSelector("span[class='lookupInput']>input[id*='CompanyField']");
        By comboAssetClasses = By.CssSelector("select[name*='id66']");
        By comboPositionIG = By.CssSelector("select[name*='PositionIG']");
        By comboPositionSector = By.CssSelector("select[name*='PositionS']");
        By comboToolUtilized = By.CssSelector("select[name*=':AutomationToolUtilizedId']");
        By txtReportFee = By.CssSelector("input[name*='id68']");
        By btnImportPositions = By.CssSelector("input[value='Import Positions']");
        By valPeriod = By.CssSelector("td[id*=':j_id167'] > a");
        By btnAddTeamMember = By.CssSelector("input[value='Add New Team Member']");
        By btnSaveTeamMember = By.CssSelector("input[value='Save Team Members']");
        By btnEdit = By.CssSelector("input[value='Edit']");
        By comboStatus = By.CssSelector("select[name*='PositionStatusID']");
        By msgErrorBox = By.CssSelector("div[class='message errorM3']");
        By msgError1 = By.CssSelector("span[id *= 'j_id18'] > ul > li:nth-child(1)");
        By msgError2 = By.CssSelector("span[id *= 'j_id18'] > ul > li:nth-child(2)");
        By valStatus = By.CssSelector("span[id*='StatusId']");
        By valFeeCompleted = By.CssSelector("span[id*='id66']");
        By valRevenueMonth = By.CssSelector("span[id*='id82']");
        By valCancelMonth = By.CssSelector("span[id*='id83']");
        By valRevenueYear = By.CssSelector("span[id*='id85']");
        By valCancelYear = By.CssSelector("span[id*='id85']");
        By valCancelYear1 = By.CssSelector("span[id*='id86']");
        By valCompletedDate = By.CssSelector("span[id*='id87']");
        By valCancelDate = By.CssSelector("span[id*='id88']");
        By btnBackToValuation = By.CssSelector("input[value='Back To Valuation Period']");
        By valPositionName = By.CssSelector("td[id*='id167']>a");
        By txtUpReportFee = By.CssSelector("input[name*='id38']");
        By btnVoidPosition = By.CssSelector("input[value='Void Position']");
        By msgCancel = By.CssSelector("div[id*='_id5']");
        By btnYes = By.CssSelector("input[value=' Yes ']");
        By linkDel = By.CssSelector("a[name*='id176']");
        By msgSuccess1 = By.CssSelector("div[id*='id8']");


        By btnEditL = By.XPath("//input[@value='Edit']");
        By txtNameL = By.XPath("//label[text()='Name']/ancestor::tr/td//input");
        By btnSaveL = By.XPath("//input[@value='Save']");
        By msgMandatoryFieldL = By.XPath("//div[contains(@class,'messageText')]");
        By btnCancelL = By.XPath("//input[@value='Cancel']");
        By titleEngValPeriodL = By.XPath("//h1[contains(text(),' Valuation Period')]");
        By valClientFinalL = By.XPath("//tr[9]/td[1]//a");
        By updClientFinalL = By.XPath("//th[text()='Client Final Deadline']/ancestor::tr/td[2]/span/span/span");
        By recPeriodAllocationL = By.XPath("//form/div[2]//tr[2]");
        By btnImportPositionsL = By.XPath("//input[@value='Import Positions']");
        By btnExistingImports = By.XPath("//td[1]/span[1]/input[contains(@value,'Valuation Period')]");
        By btnExistingValPeriodL = By.XPath("//input[@type='radio']");
        By btnSearchValPeriodPosL = By.XPath("//input[@value='Search Valuation Period for Positions']");
        By lblImportL = By.XPath("//label[contains(text(),'Positions')]");
        By chkPositionNameL = By.XPath("//input[contains(@name,'myCheckbox')]");
        By btnSaveImportL = By.XPath("//input[@value='Save']");
        By msgAutomationToolL = By.XPath("//div[@class='messageText']");
        By btnAutomationToolL = By.XPath("//input[@value='Update Automation Tool Usage']");
        By lblColumnsL = By.XPath("//td[1]/table//th/div");
        By comboUtilizedL = By.XPath("//select[contains(@name,'AutomationToolUtilizedId')]");
        By comboReasonL = By.XPath("//select[contains(@name,'ReasonFieldId')]");
        By btnSaveAutomation = By.XPath("//div[3]/table//td[2]/input[1]");
        By titleRelatedPositionsL = By.XPath("//h3[text()='Related Positions']");
        By btnSaveAndBackL = By.XPath("//input[@value='Save & Back To Valuation Period']");
        By valImportedPositionL = By.XPath("//div[2]//tr[2]/td[2]/a");
        By btnCloseTabL = By.XPath("//li[4]/div[2]/button");
        By radioImportWithoutL = By.XPath("//input[@value='Import Positions Without Team Members']");
        By titleEngValPeriodEditL = By.XPath("//h1[text()='Engagement Valuation Period Edit']");
        By lblEngValPeriodEditL = By.XPath("//span[text()='*']/ancestor::label");
        By btnEngValPeriodEditL = By.XPath("//td[@class='pbButton ']/input");
        By msgEngValPeriodEditL = By.XPath("//tr[2]//li");
        By titleValPeriodsL = By.XPath("//b[contains(text(),' Valuation Period')]");

        string dir = @"C:\Users\SGoyal0427\source\repos\SF_Automation\TestData\";

        //To Click New Engagement Valuation Period button
        public string ClickEngValuationPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewEngValPeriod, 60);
            driver.FindElement(btnNewEngValPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblNewEngValPeriod, 60);
            string title = driver.FindElement(lblNewEngValPeriod).Text;
            return title;
        }

        //Enter Engagement Valuation Period details and save it.
        public string EnterAndSaveEngValuationDetails()
        {
            string Name = "VP:" + CustomFunctions.RandomValue();
            driver.FindElement(txtName).SendKeys(Name);
            driver.FindElement(lnkValDate).Click();
            driver.FindElement(lnkClientFinalDeadLine).Click();
            driver.FindElement(btnSave).Click();
            return Name;
        }

        //Get title of Engagement Valuation Period Detail page
        public string GetEngValPeriodDetailTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnImportPositions, 60);
            string title = driver.FindElement(lblEngValPeriodDetail).Text;
            return title;
        }

        //Enter Eng Valuation Period Position details
        public string EnterPeriodPositionDetails(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewEngValPeriodPosition, 60);
            driver.FindElement(btnNewEngValPeriodPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 60);
            driver.FindElement(btnCompany).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 4));
            driver.FindElement(comboAssetClasses).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 5));
            driver.FindElement(comboPositionIG).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 8));
            driver.FindElement(comboPositionSector).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 9));
            driver.FindElement(comboToolUtilized).SendKeys("Yes");
            //driver.FindElement(txtReportFee).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 7));
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblEngValPeriodDetail, 60);
            string value = driver.FindElement(valPeriod).Text;
            return value;
        }

        //Click on added Engagement Valuation Period Position
        public string ClickAddedValPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPeriod, 100);
            driver.FindElement(valPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblEngValPeriodDetail, 100);
            string title = driver.FindElement(lblEngValPeriodDetail).Text;
            return title;
        }

        //To add team members and save it. 
        public void ClickPositionAndSaveTeamMembers()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddTeamMember, 80);
            driver.FindElement(btnAddTeamMember).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveTeamMember, 80);
            driver.FindElement(btnSaveTeamMember).Click();
            Thread.Sleep(3000);
        }

        //To update the status of Position
        public string UpdateStatusAndSave()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboStatus).SendKeys("Completed, Generate Accrual");
            driver.FindElement(btnSave).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgErrorBox, 90);
            string error1 = driver.FindElement(msgError1).Text;
            return error1;
        }

        //To fetch 2nd error message
        public string GetErrorMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgErrorBox, 90);
            string error2 = driver.FindElement(msgError2).Text;
            return error2;
        }
        //To get Status of Position
        public string GetPositionStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStatus, 90);
            string status = driver.FindElement(valStatus).Text;
            return status;
        }
        //To get Fee Completed of Position
        public string GetFeeCompleted()
        {
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, valFeeCompleted, 190);
            string feeCompleted = driver.FindElement(valFeeCompleted).Text;
            return feeCompleted;
        }
        //To get Revenue Month of Position
        public string GetRevenueMonth()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRevenueMonth, 90);
            string revenueMonth = driver.FindElement(valRevenueMonth).Text;
            return revenueMonth;
        }
        //To get Cancel Month of Position
        public string GetCancelMonth()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCancelMonth, 90);
            string cancelMonth = driver.FindElement(valCancelMonth).Text;
            return cancelMonth;
        }
        //To get Revenue Year of Position
        public string GetRevenueYear()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRevenueYear, 190);
            string revenueYear = driver.FindElement(valRevenueYear).Text;
            return revenueYear;
        }
        //To get Cancel Year of Position
        public string GetCancelYear()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCancelYear, 90);
            string cancelYear = driver.FindElement(valCancelYear).Text;
            return cancelYear;
        }
        //To get Cancel Year of Position
        public string GetCancelYearWithDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCancelYear1, 90);
            string cancelYear = driver.FindElement(valCancelYear1).Text;
            return cancelYear;
        }
        //To get Completed Date of Position
        public string GetCompletedDate()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valCompletedDate, 90);
            string compDate = driver.FindElement(valCompletedDate).Text;
            return compDate;
        }
        //To get Cancel Date of Position
        public string GetCancelDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCancelDate, 90);
            string cancelDate = driver.FindElement(valCancelDate).Text;
            return cancelDate;
        }
        //To  update Status and Report Fee of existing Position
        public string UpdateStatusAndReportFee(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToValuation, 60);
            driver.FindElement(btnBackToValuation).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valPositionName, 90);
            driver.FindElement(valPositionName).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 60);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboStatus, 60);
            driver.FindElement(comboStatus).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 14));
            driver.FindElement(txtUpReportFee).Clear();
            driver.FindElement(txtUpReportFee).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 11));
            Thread.Sleep(4000);
            driver.FindElement(btnSave).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess1, 100);
            string msg = driver.FindElement(msgSuccess1).Text;
            return msg;

        }

        //Click Void Position and fetch cancellation message
        public string ClickVoidPositionAndGetMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnVoidPosition);
            driver.FindElement(btnVoidPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgCancel, 70);
            string message = driver.FindElement(msgCancel).Text;
            driver.FindElement(btnYes).Click();
            return message;
        }

        //To delete the position  
        public void DeletePosition()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToValuation, 70);
            driver.FindElement(btnBackToValuation).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkDel, 60);
            driver.FindElement(linkDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        //Validate mandatory validation of Valuation Period
        public string ValidateMandatoryMessageOfValuationPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 120);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);                       
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(4000);           
            string value = driver.FindElement(msgMandatoryFieldL).Text;
            return value;
        }

        //Validate Opportunity details page is displayed upon clicking cancel button
        public string ValidateEngValDetailsPageUponClickingCancelButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelL, 120);
            driver.FindElement(btnCancelL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            string tab = driver.FindElement(titleEngValPeriodL).Text;            
            return tab;
        }

        //Validate FVA use can edit Valuation Period
        public string EditFunctionalityOfValuationPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 120);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            driver.FindElement(valClientFinalL).Click();
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            string value = driver.FindElement(updClientFinalL).Text;
            return value;
        }

        //Validate added Period allocation record after updating Client Final Deadline
        public string ValidatePeriodAllocationRecordAfterUpdatingClientFinalDeadline()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, recPeriodAllocationL, 140);           
            string value = driver.FindElement(recPeriodAllocationL).Displayed.ToString();
            return value;
        }

        //Validate Import Positions page buttons
        public bool ValidateButtonsOnImportPositions()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnImportPositionsL, 140);
            driver.FindElement(btnImportPositionsL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
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
            driver.SwitchTo().Frame(1);
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
        //Validate Automation Tool Usage mandatory field validation
        public string ValidateAutomationToolMandatoryFieldMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chkPositionNameL, 120);
            driver.FindElement(chkPositionNameL).Click();
            driver.FindElement(btnSaveImportL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            string row = driver.FindElement(msgAutomationToolL).Text;
            return row;
        }

        //Validate Automation tool fields
        public bool ValidateColumnsOnAutomationToolPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAutomationToolL, 140);
            driver.FindElement(btnAutomationToolL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblColumnsL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "POSITION NAME", "AUTOMATION TOOL UTILIZED", "REASON", "COMMENTS" };
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

        //Validate that clicking the save button, the user is redirected to the related positions list to import positions.
        public string ValidatePositionsListPageUponClickingSaveButtonOnAutomationToolPage()
        {            
            Thread.Sleep(5000);
            driver.FindElement(comboUtilizedL).SendKeys("Yes");
            //driver.FindElement(comboReasonL).SendKeys("Historical");
            driver.FindElement(btnSaveAutomation).Click();
            Thread.Sleep(7000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            string row = driver.FindElement(titleRelatedPositionsL).Text;
            return row;
        }

        //Validate that Import with team member 
        public string ValidateImportWithTeamMember()
        {
            Thread.Sleep(5000);           
            driver.FindElement(btnSaveAndBackL).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(6000);
            string row = driver.FindElement(valImportedPositionL).Text;
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(4000);
            driver.FindElement(btnCloseTabL).Click();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            return row;
        }

        //Validate that Import without team member 
        public string ValidateImportWithoutTeamMember()
        {
            driver.FindElement(btnExistingValPeriodL).Click();
            driver.FindElement(btnSearchValPeriodPosL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(6000);
            driver.FindElement(radioImportWithoutL).Click();
            driver.FindElement(chkPositionNameL).Click();            
            driver.FindElement(btnSaveAndBackL).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            driver.FindElement(btnAutomationToolL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            driver.FindElement(comboUtilizedL).SendKeys("Yes");            
            driver.FindElement(btnSaveAutomation).Click();
            Thread.Sleep(7000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            driver.FindElement(btnSaveAndBackL).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            string row = driver.FindElement(valImportedPositionL).Text;
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(4000);
            driver.FindElement(btnCloseTabL).Click();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);                      
            return row;
        }

        public string ValidatePageAfterClickingNewEngValPeriodButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewEngValPeriod, 140);
            driver.FindElement(btnNewEngValPeriod).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            string title = driver.FindElement(titleEngValPeriodEditL).Text;
            return title;
        }

        public bool ValidateMandatoryFieldsOnEngValPeriodEdit()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblEngValPeriodEditL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "*\r\nName", "*\r\nValuation Date", "*\r\nClient Final Deadline" };
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

        public bool ValidateButtonsOnEngValPeriodEdit()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(btnEngValPeriodEditL);
            var actualValue = valRecordTypes.Select(x => x.GetAttribute("value")).ToArray();
            string[] expectedValue = { "Save", "Cancel" };
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

        public bool ValidateMandatoryValidationsUponClickingSaveOnEngValPeriodEdit()
        {
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(msgEngValPeriodEditL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Name: You must enter a value", "Valuation Date: You must enter a value", "Client Final Deadline: You must enter a value" };
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

        public string ValidatePageAfterClickingCancelButtonOnEngValPeriodEditPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelL, 140);
            driver.FindElement(btnCancelL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            string title = driver.FindElement(btnNewEngValPeriod).GetAttribute("value");
            return title;
        }
    }
}


