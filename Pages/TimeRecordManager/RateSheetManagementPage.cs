using AventStack.ExtentReports;
using OpenQA.Selenium;
using SF_Automation.Pages.Common;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.PortableExecutable;
using System.Threading;

namespace SF_Automation.Pages.TimeRecordManager
{
    class RateSheetManagementPage : BaseClass
    {
        RandomPages randomPages = new RandomPages();
        By shwAllTab = By.CssSelector("li[id='AllTab_Tab'] > a > img");
        By imgTitleRateSheet = By.CssSelector("img[alt = 'Title Rate Sheets']");
        By btnGo = By.CssSelector("input[title='Go!']");
        By tableRateSheet = By.XPath("//table[@class='x-grid3-row-table']/tbody/tr");
        By linkFilterByLetterT = By.XPath("//a[@class='listItem']/span[text()='T']");
        By tabRateSheetManagement = By.CssSelector("li[id*='ratesheet'] > a");
      
        By comboEngagement = By.CssSelector("select[class*='engagementPicker']");
        By comboEngagementOptions = By.CssSelector("select[class*='engagementPicker'] > option[value*='16H9zBAAS']");
        By comboSelectRateSheet = By.XPath("//div[contains(text(),'Add Record')]/following::div/div/div/select");
        By txtRateSheetFromDate = By.CssSelector("div[class='slds-card__body'] > div[class='slds-grid'] > div:nth-child(2) > div > input");
        By txtRateSheetToDate = By.CssSelector("div[class='slds-card__body'] > div[class='slds-grid'] > div:nth-child(3) > div > input");
        By btnAddRateSheet = By.XPath("//span[text()='Add']");
        By valRateSheetRecord = By.CssSelector("div[class*='slds-table'] > tr > td:nth-child(1)");
        By btnCrossDeleteRecord = By.CssSelector("td[class*='slds-cell-shrink'] > button[class*='slds-button']");
        By tabBillingPreparation = By.CssSelector("li[id*='billing'] > a");
        By txtBillingAmt = By.CssSelector("#tab-billing > div > div.slds-table.slds-table--striped > tr > td:nth-child(5) > span.uiOutputNumber");
        By valDetailList = By.XPath("//*/td[contains(text(),'Intern/Financial')]");


        By sheetInitials = By.XPath("//div[@class='rolodex']/a/span");
        By rateSheetList = By.XPath("//div[contains(@class, ' x-panel x-grid-panel')]//tr//td[5]");
        By titleOnSheetDetail = By.XPath("//table[@class='detailList']//tr//td[@class='labelCol']");
        By rateOnSheetDetail = By.XPath("//table[@class='detailList']//tr//td[contains(@class,'dataCol')]//div");
        By viewGoButton = By.Name("go");
        By nameRateSheetDetailPage = By.CssSelector(".content > h2");
        By comboSelectRateSheet1 = By.XPath("(//div[contains(text(),'Add Record')]/following::div/div/div/select)[3]");
        By btnAddRateSheet1 = By.XPath("(//span[text()='Add'])[2]");
        By tabStaffTimeSheet = By.CssSelector("li[id*='staff'] > a");
        By frameTimeRecordPage = By.XPath("//iframe[@title='accessibility title']");
        By rowRateSheet = By.CssSelector("div[class*='slds-table'] > tr");
        By rateSheetNameList = By.XPath("//div[contains(@class, ' x-panel x-grid-panel')]//tr//td[5]//a");
        By comboSelectRateSheet3 = By.XPath("//div[contains(text(),'Add Record')]/following::div/div/div/select");
        By txtPageHeader = By.XPath("//h1//lightning-formatted-text");
        By chkboxBilling = By.XPath("//div[contains(@class,'TimeRecordManager')]//div[@id='tab-billing']//tr[1]//input");
        By btnSendNotification = By.XPath("//div[contains(@class,'TimeRecordManager')]//div[@id=\"tab-billing\"]//button");

        private By _nameRateSheet(string name)
        {
            return By.XPath($"//table//tbody//td//a[@title='{name}']");
        }
        private By _nameRateSheetRecent(string name)
        {
            return By.XPath($"//table//tbody//th//a[@title='{name}']");
        }
        

        //private By rateSheetName(String rateSheetname)
        //{
        //    return By.XPath($"//a/span[text()={rateSheetname}]");
        //}     

        public string GetRateSheetDetailPage()
        {

            return driver.FindElement(nameRateSheetDetailPage).Text;
        }

        //Selecting the Title Rate Sheet Object
        public void SelectTitleRateSheetObject()
        {
            driver.FindElement(shwAllTab).Click();
            Thread.Sleep(2000);
            driver.FindElement(imgTitleRateSheet).Click();
            Thread.Sleep(2000);

        }
        public void SelectAllRateSheets()
        {
            Thread.Sleep(4000);
            bool goButton = driver.FindElement(viewGoButton).Displayed;
            if (goButton)
                driver.FindElement(viewGoButton).Click();
        }

        //Selecting the Rate Sheet initials
        public void SelectSheetIntials(string initialValue)
        {
            Thread.Sleep(4000);
            IList<IWebElement> initials = driver.FindElements(sheetInitials);
            for (int i = 0; i <= initials.Count; i++)
            {
                string initialsValue = initials[i].Text;
                if (initialsValue.Equals(initialValue))
                {
                    initials[i].Click();
                    Thread.Sleep(2000);
                    WebDriverWaits.WaitUntilEleVisible(driver, rateSheetList);
                    break;
                }

            }
        }

        //Selecting the Rate Sheet
        public void SelectRateSheet(string rateSheetname)
        {

            IList<IWebElement> rateSheets = driver.FindElements(rateSheetNameList);
            for (int i = 0; i <= rateSheets.Count; i++)
            {
                string rateSheetValue = rateSheets[i].Text;
                if (rateSheetValue.Equals(rateSheetname))
                {
                    rateSheets[i].Click();
                    Thread.Sleep(2000);
                    break;
                }
            }
        }

        //Get the Title and its respectice Rate on selected Rate sheet
        public bool IsRateAsPerTitle(string title, string rate)
        {
            IList<IWebElement> titles = driver.FindElements(titleOnSheetDetail);
            IList<IWebElement> rates = driver.FindElements(rateOnSheetDetail);
            bool IsRateAsPerTitleAvailable = false;
            for (int titleTableRow = 2; titleTableRow <= titles.Count; titleTableRow++)
            {
                string titleValue = titles[titleTableRow].Text;
                if (titleValue.Equals(title))
                {
                    string rateValue = rates[titleTableRow].Text;
                    IsRateAsPerTitleAvailable = rateValue.Contains(rate);
                    break;
                }
            }
            return IsRateAsPerTitleAvailable;
        }


        //============================================================
        public void EnterRateSheet(string engagement, string rateSheet)
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabRateSheetManagement);
            driver.FindElement(tabRateSheetManagement).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboEngagement);
            //WebDriverWaits.WaitUntilEleVisible(driver, comboEngagementOptions, 220);
            
            driver.FindElement(comboEngagement).SendKeys(engagement);
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectRateSheet);
            driver.FindElement(comboSelectRateSheet).SendKeys(rateSheet);
            Thread.Sleep(2000);

            string getFromDate = DateTime.Now.ToString("MMM dd, yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtRateSheetFromDate);
            driver.FindElement(txtRateSheetFromDate).Clear();
            driver.FindElement(txtRateSheetFromDate).SendKeys(getFromDate);

            string getToDate = DateTime.Now.AddDays(+7).ToString("MMM dd, yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtRateSheetToDate);
            driver.FindElement(txtRateSheetToDate).Clear();
            driver.FindElement(txtRateSheetToDate).SendKeys(getToDate);

            WebDriverWaits.WaitUntilEleVisible(driver, btnAddRateSheet);
            driver.FindElement(btnAddRateSheet).Click();
            Thread.Sleep(5000);
        }

        //TO get selected rate sheet
        public string GetSelectedRateSheet()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valRateSheetRecord, 80);
            string selectedRateSheet = driver.FindElement(valRateSheetRecord).Text;
            return selectedRateSheet;
        }

        public void DeleteRateSheet(string engagementName)
        {
            try
            {
                Thread.Sleep(5000);
                WebDriverWaits.WaitUntilEleVisible(driver, tabRateSheetManagement);
                driver.FindElement(tabRateSheetManagement).Click();
                Thread.Sleep(10000);

                WebDriverWaits.WaitUntilEleVisible(driver, comboEngagement);
                driver.FindElement(comboEngagement).SendKeys(engagementName);
                Thread.Sleep(10000);

                WebDriverWaits.WaitUntilEleVisible(driver, btnCrossDeleteRecord);
                driver.FindElement(btnCrossDeleteRecord).Click();
                Thread.Sleep(5000);

                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(5000);
            }
            catch (Exception)
            {
                driver.FindElement(tabStaffTimeSheet).Click(); 
                Thread.Sleep(5000);
                WebDriverWaits.WaitUntilEleVisible(driver, tabRateSheetManagement);
                driver.FindElement(tabRateSheetManagement).Click();
                Thread.Sleep(10000);

                WebDriverWaits.WaitUntilEleVisible(driver, comboEngagement);
                driver.FindElement(comboEngagement).SendKeys(engagementName);
                Thread.Sleep(10000);

                WebDriverWaits.WaitUntilEleVisible(driver, btnCrossDeleteRecord);
                driver.FindElement(btnCrossDeleteRecord).Click();
                Thread.Sleep(5000);

                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(5000);
            }

        }
        public string GetBillingAmountFromBillingPreparationTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabBillingPreparation);
            driver.FindElement(tabBillingPreparation).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtBillingAmt, 120);
            string BillingAmt = driver.FindElement(txtBillingAmt).Text;
            return BillingAmt;
        }

        public string GetDefaultRateForAssociateAsPerRateSheet(string rateSheet)
        {
            driver.FindElement(shwAllTab).Click();
            Thread.Sleep(2000);
            driver.FindElement(imgTitleRateSheet).Click();
            Thread.Sleep(2000);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
            driver.FindElement(linkFilterByLetterT).Click();
            Thread.Sleep(2000);

            //Get Row Count
            IList<IWebElement> element = driver.FindElements(tableRateSheet);
            int rowCount = element.Count;
            string rate = "";
            for (int p = 1; p <= rowCount; p++)
            {
                By titleRateSheetName = By.CssSelector($"table[class='x-grid3-row-table'] > tbody > tr:nth-child({p}) > td:nth-child(3) > div > a > span");
                IWebElement rateSheetName = driver.FindElement(titleRateSheetName);

                string sheet = rateSheetName.Text;
                if (sheet.Equals(rateSheet))
                {
                    driver.FindElement(titleRateSheetName).Click();
                    Thread.Sleep(5000);

                    By associateRate = By.XPath("//*[contains(text(),'Associate Rate')]/following::td[1]");
                    IWebElement associateRatePerHour = driver.FindElement(associateRate);

                    rate = associateRatePerHour.Text.Split(' ')[1].Trim();
                    break;
                }
            }
            return rate;
        }
        public string GetDefaultRateForOutsourcedContractorAsPerRateSheet(string rateSheet)
        {
            driver.FindElement(shwAllTab).Click();
            Thread.Sleep(2000);
            driver.FindElement(imgTitleRateSheet).Click();
            Thread.Sleep(2000);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
            driver.FindElement(linkFilterByLetterT).Click();
            Thread.Sleep(2000);

            //Get Row Count
            IList<IWebElement> element = driver.FindElements(tableRateSheet);
            int rowCount = element.Count;
            string rate = "";
            for (int p = 1; p <= rowCount; p++)
            {
                By titleRateSheetName = By.CssSelector($"table[class='x-grid3-row-table'] > tbody > tr:nth-child({p}) > td:nth-child(3) > div > a > span");
                IWebElement rateSheetName = driver.FindElement(titleRateSheetName);

                string sheet = rateSheetName.Text;
                if (sheet.Equals(rateSheet))
                {
                    driver.FindElement(titleRateSheetName).Click();
                    Thread.Sleep(5000);

                    By outsourcedContractorRate = By.XPath("//*[contains(text(),'Outsourced Contractor Rate')]/following::td[1]");
                    IWebElement outsourcedContractorRatePerHour = driver.FindElement(outsourcedContractorRate);

                    rate = outsourcedContractorRatePerHour.Text.Split(' ')[1].Trim();
                    break;
                }
            }
            return rate;
        }
        public string GetDefaultRateForInternAndFinancialAnalyst(string rateSheet)
        {
            string rate = "";
            if (driver.Title.Equals("Title Rate Sheet: " + rateSheet + " ~ Salesforce - Unlimited Edition"))
            {
                string internRatePerHour = driver.FindElement(By.XPath("//*/td[contains(text(),'Intern/Financial')]/following::td")).Text;
                rate = internRatePerHour.Split(' ')[1].Trim();
            }
            return rate;
        }

        public double GetDefaultRateAsPerRole(string role)
        {
            string ratePerHour = driver.FindElement(By.XPath($"//*[text()='{role}']/following::td")).Text;
            double rate = Convert.ToDouble(ratePerHour.Split(' ')[1].Trim());
            return rate;
        }
        public void ClickNewTitleRateSheet(string rateSheet)
        {
            //Get Row Count
            IList<IWebElement> element = driver.FindElements(tableRateSheet);
            int rowCount = element.Count;
            for (int p = 1; p <= rowCount; p++)
            {
                By titleRateSheetName = By.CssSelector($"div[class='x-grid3-body'] > div:nth-child({p}) > table > tbody > tr > td:nth-child(5) > div > a > span");
                IWebElement rateSheetName = driver.FindElement(titleRateSheetName);

                string sheet = rateSheetName.Text;
                if (sheet.Equals(rateSheet))
                {
                    driver.FindElement(titleRateSheetName).Click();
                    break;
                }
            }
        }
        public void NavigateToTitleRateSheetsPage()
        {
            driver.FindElement(shwAllTab).Click();
            Thread.Sleep(2000);
            driver.FindElement(imgTitleRateSheet).Click();
            Thread.Sleep(2000);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
        }
        public bool VerifyNewTitle(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            WebDriverWaits.WaitUntilEleVisible(driver, valDetailList, 120);
            string listName = driver.FindElement(valDetailList).Text;
            if (ReadExcelData.ReadData(excelPath, "RateSheetManagement", 1).Equals(listName))
            {
                result = true;
            }

            return result;
        }
        public void EnterRateSheet1(string engagement, string rateSheet)
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabRateSheetManagement);
            driver.FindElement(tabRateSheetManagement).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboEngagement);
            //WebDriverWaits.WaitUntilEleVisible(driver, comboEngagementOptions, 220);

            driver.FindElement(comboEngagement).SendKeys(engagement);
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectRateSheet1);
            driver.FindElement(comboSelectRateSheet1).SendKeys(rateSheet);
            Thread.Sleep(2000);

            string getFromDate = DateTime.Now.ToString("MMM dd, yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtRateSheetFromDate);
            driver.FindElement(txtRateSheetFromDate).Clear();
            driver.FindElement(txtRateSheetFromDate).SendKeys(getFromDate);

            string getToDate = DateTime.Now.AddDays(+7).ToString("MMM dd, yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtRateSheetToDate);
            driver.FindElement(txtRateSheetToDate).Clear();
            driver.FindElement(txtRateSheetToDate).SendKeys(getToDate);

            WebDriverWaits.WaitUntilEleVisible(driver, btnAddRateSheet1);
            driver.FindElement(btnAddRateSheet1).Click();
            Thread.Sleep(5000);
        }
        
        public void EnterRateSheetLV(string engagement, string rateSheet)
        {
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, tabRateSheetManagement);
            driver.FindElement(tabRateSheetManagement).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabRateSheetManagement);
            driver.FindElement(tabRateSheetManagement).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboEngagement);
            driver.FindElement(comboEngagement).SendKeys(engagement);
            Thread.Sleep(5000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectRateSheet,10);
                driver.FindElement(comboSelectRateSheet).SendKeys(rateSheet);                
            }
            catch
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectRateSheet1,10);
                driver.FindElement(comboSelectRateSheet1).SendKeys(rateSheet);                
            }
            //Thread.Sleep(2000);
            string getFromDate = DateTime.Now.ToString("MMM dd, yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtRateSheetFromDate);
            driver.FindElement(txtRateSheetFromDate).Clear();
            driver.FindElement(txtRateSheetFromDate).SendKeys(getFromDate);

            string getToDate = DateTime.Now.AddDays(+7).ToString("MMM dd, yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtRateSheetToDate);
            driver.FindElement(txtRateSheetToDate).Clear();
            driver.FindElement(txtRateSheetToDate).SendKeys(getToDate);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnAddRateSheet, 5);
                driver.FindElement(btnAddRateSheet).Click();
            }
            catch
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnAddRateSheet1, 5);
                driver.FindElement(btnAddRateSheet1).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, rowRateSheet);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
        }
        public void DeleteRateSheetLV(string engagementName)
        {
            try
            {
                Thread.Sleep(5000);
                driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
                WebDriverWaits.WaitUntilEleVisible(driver, tabRateSheetManagement);
                driver.FindElement(tabRateSheetManagement).Click();
                Thread.Sleep(5000);
                WebDriverWaits.WaitUntilEleVisible(driver, tabRateSheetManagement);
                driver.FindElement(tabRateSheetManagement).Click();
                Thread.Sleep(10000);

                WebDriverWaits.WaitUntilEleVisible(driver, comboEngagement);
                driver.FindElement(comboEngagement).SendKeys(engagementName);
                Thread.Sleep(10000);

                WebDriverWaits.WaitUntilEleVisible(driver, btnCrossDeleteRecord);
                driver.FindElement(btnCrossDeleteRecord).Click();
                Thread.Sleep(5000);

                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(5000);
            }
            catch (Exception)
            {
                driver.FindElement(tabStaffTimeSheet).Click();
                Thread.Sleep(5000);
                WebDriverWaits.WaitUntilEleVisible(driver, tabRateSheetManagement);
                driver.FindElement(tabRateSheetManagement).Click();
                Thread.Sleep(10000);

                WebDriverWaits.WaitUntilEleVisible(driver, comboEngagement);
                try
                {
                    driver.FindElement(comboEngagement).SendKeys(engagementName);
                    Thread.Sleep(10000);
                    WebDriverWaits.WaitUntilEleVisible(driver, btnCrossDeleteRecord);
                    driver.FindElement(btnCrossDeleteRecord).Click();
                    Thread.Sleep(5000);
                    IAlert alert = driver.SwitchTo().Alert();
                    alert.Accept();
                    Thread.Sleep(5000);
                }
                catch 
                {
                    //Do Nothing as there is no Rate sheet added
                }                
            }
            driver.SwitchTo().DefaultContent();
        }
        public string GetSelectedRateSheetLV()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, valRateSheetRecord, 80);
            string selectedRateSheet = driver.FindElement(valRateSheetRecord).Text;
            driver.SwitchTo().DefaultContent();
            return selectedRateSheet;
        }

        public double GetBillingAmountFromBillingPreparationTabLV()
        {
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, tabBillingPreparation);
            driver.FindElement(tabBillingPreparation).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtBillingAmt, 120);
            string billingAmt = driver.FindElement(txtBillingAmt).Text;
            double billedAmount = Convert.ToDouble(billingAmt);
            //double titleRate = Convert.ToDouble(BillingAmt.Split('$')[1].Trim());
            driver.SwitchTo().DefaultContent();
            return billedAmount; 
        }
        public void GoToBillingPreparationTabLV()
        {
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, tabBillingPreparation);
            driver.FindElement(tabBillingPreparation).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkboxBilling, 20);
            driver.SwitchTo().DefaultContent();
        }
        public bool GetSendNotificatioButtonStatusLV()
        {
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, btnSendNotification, 20);
            bool btnStatus = driver.FindElement(btnSendNotification).Enabled;
            driver.SwitchTo().DefaultContent();
            return btnStatus;
        }
        public void SelectBillingPreparationRecordLV()
        {
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, chkboxBilling);
            driver.FindElement(chkboxBilling).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
        }

        private By nameRole(string role)
        {
            return By.XPath($"//div/span[text()='{role}']//ancestor::dt/following-sibling::dd//lightning-formatted-text");//*[text()='{role}']//ancestor::dl//dd//span//lightning-formatted-text");
        }
        public double GetDefaultRateAsPerRoleLV(string role)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, nameRole(role),20);
            string ratePerHour = driver.FindElement(nameRole(role)).Text;
            double rate = Convert.ToDouble(ratePerHour.Split(' ')[1].Trim());
            driver.SwitchTo().DefaultContent();
            return rate;
        }        

        public string SelectTitleRateSheetLV(string name)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            Thread.Sleep(5000); 
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, _nameRateSheetRecent(name), 10);
                driver.FindElement(_nameRateSheetRecent(name)).Click();
            }
            catch
            {
                randomPages.SelectListViewLV("All");
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, _nameRateSheet(name), 10);
                    driver.FindElement(_nameRateSheet(name)).Click();
                }
                catch
                {
                    js.ExecuteScript("window.scrollTo(0,550)");
                    WebDriverWaits.WaitUntilEleVisible(driver, _nameRateSheet(name), 10);
                    driver.FindElement(_nameRateSheet(name)).Click();
                }
            }            
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtPageHeader, 20);
            string pageheader= driver.FindElement(txtPageHeader).Text;
            return pageheader;
        }

    }
}
