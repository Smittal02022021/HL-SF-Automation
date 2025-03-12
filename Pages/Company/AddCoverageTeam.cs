using OpenQA.Selenium;
using SF_Automation.TestCases.GiftLog;
using SF_Automation.Pages.Companies;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Web;

namespace SF_Automation.Pages.Company
{
    class AddCoverageTeam : BaseClass
    {
        By btnNewCoverageTeam = By.CssSelector("input[value='New Coverage Team']");
        By txtCompany = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(1) >td:nth-child(2) > div > span > input");
        By txtOfficer = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(2) >td:nth-child(2) > div > span > input");
        By comboCoverageLevel = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(3) >td:nth-child(2) > div > span > select");
        By comboType = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(4) >td:nth-child(2) > div > span > select");
        By comboPrimarySector = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(5) >td:nth-child(2) > span > span > select");
        By comboSecondarySector = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(6) >td:nth-child(2) > span > span > select");
        By comboTier = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(7) >td:nth-child(2) > div >  span > select");
        By btnSave = By.CssSelector("td[id='bottomButtonRow'] > input[name='save']");
        By btnEdit = By.CssSelector("div[id*='D7bV0_body']> table > tbody > tr > td:nth-child(1)");
        By btnNexRecordTypetL = By.XPath("//div[@class='slds-modal__footer']//button[text()='Next']");
        By btnSaveDetailsL = By.XPath("//button[@name='SaveEdit']");
        By btnNewSponsorCoverageL = By.XPath("//h2//span[text()='Sponsor Coverage']//ancestor::article//button[text()='New']");
        By btnNewIndustryCoverageL = By.XPath("//h2//span[text()='Industry Coverage']//ancestor::article//button[text()='New']");
        
        By txtReqFields = By.XPath("//div[@class='fieldLevelErrors']//li//a");
        By btnCancelL = By.XPath("//button[@name='CancelEdit']");
        By inputOfficeL = By.XPath("//label[text()='Officer']/..//input");
        By inputContactNameL = By.XPath("//input[@title='Search Contacts']/..//li//a//div[@title='{name}']");
        By comboCovegareTierL = By.XPath("//label[text()='Tier']/..//button");
        By comboCoverageLevelL = By.XPath("//label[text()='Coverage Level']/..//button");
        By comboCovegareTypeL = By.XPath("//label[text()='Type']/..//button");
        By toastMsgPopup = By.XPath("//span[contains(@class,'toastMessage')]");
        
             
        
        public string UpdateCoverageTeamTierLV(string tier)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboCovegareTierL, 10);
            driver.FindElement(comboCovegareTierL).Click();
            By elmCovTier = By.XPath($"//label[text()='Tier']/..//lightning-base-combobox-item//span[@title='{tier}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmCovTier, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmCovTier));
            driver.FindElement(elmCovTier).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 10);
            driver.FindElement(btnSaveDetailsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 5);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(8000);
            return toasMsg;
        }

        //public string UpdateSponsorCoverageTeamTierLV(string tier)
        //{
        //    WebDriverWaits.WaitUntilEleVisible(driver, comboCovegareTierL, 10);
        //    driver.FindElement(comboCovegareTierL).Click();
        //    By elmCovTier = By.XPath($"//label[text()='Tier']/..//lightning-base-combobox-item//span[@title='{tier}']");
        //    WebDriverWaits.WaitUntilEleVisible(driver, elmCovTier, 10);
        //    CustomFunctions.MoveToElement(driver, driver.FindElement(elmCovTier));
        //    driver.FindElement(elmCovTier).Click();
        //    WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 10);
        //    driver.FindElement(btnSaveDetailsL).Click();
        //    WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 5);
        //    string toasMsg = driver.FindElement(toastMsgPopup).Text;
        //    Thread.Sleep(8000);
        //    return toasMsg;
        //}



        public string AddNewCoverageTeamLV(string officerName, string tier, string level, string type)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, inputOfficeL, 10);
            driver.FindElement(inputOfficeL).SendKeys(officerName);
            By elmOfficer = By.XPath($"//label[text()='Officer']/..//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{officerName}']");
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, elmOfficer, 10);
            driver.FindElement(elmOfficer).Click();

            driver.FindElement(comboCovegareTierL).Click();
            By elmCovTier= By.XPath($"//label[text()='Tier']/..//lightning-base-combobox-item//span[@title='{tier}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmCovTier, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmCovTier));
            driver.FindElement(elmCovTier).Click();

            driver.FindElement(comboCoverageLevelL).Click();
            By elmCovlevel = By.XPath($"//label[text()='Coverage Level']/..//lightning-base-combobox-item//span[@title='{level}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmCovlevel, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmCovlevel));
            driver.FindElement(elmCovlevel).Click();

            driver.FindElement(comboCovegareTypeL).Click();
            By elmCovType = By.XPath($"//label[text()='Type']/..//lightning-base-combobox-item//span[@title='{type}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmCovType, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmCovType));
            driver.FindElement(elmCovType).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 10);
            driver.FindElement(btnSaveDetailsL).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 5);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            return toasMsg;

        }
        public string GetNewCoverageTeamReqFieldsLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtReqFields, 10);
            IList<IWebElement> fieldLevelErrors = driver.FindElements(txtReqFields);
            string formatedReqFieldLabels = "";
            foreach (IWebElement txtFieldLevelError in fieldLevelErrors)
            {
                string fieldLevelError = txtFieldLevelError.Text;
                string formatedfieldLevelLabels = Regex.Replace(fieldLevelError, @"\t|\n|\r", "");
                formatedReqFieldLabels = formatedReqFieldLabels + formatedfieldLevelLabels;
            }
            //driver.FindElement(iconCloseErrorL).Click();
            Thread.Sleep(2000);
            return formatedReqFieldLabels;
        }
        public void ClickSaveNewCoverageTeamButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 10);
            driver.FindElement(btnSaveDetailsL).Click();
        }

        public void ClickNewButtonSponsorCoverageDisplayedLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewSponsorCoverageL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(btnNewSponsorCoverageL));
        }
        public void ClickCancelNewCoverageTeamButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelL, 10);
            driver.FindElement(btnCancelL).Click();
        }
        public bool IsNewButtonSponsorCoverageDisplayedLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewSponsorCoverageL, 10);
            return driver.FindElement(btnNewSponsorCoverageL).Displayed;
        }
        public bool IsNewButtonIndustryCoverageDisplayedLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewIndustryCoverageL, 10);
            return driver.FindElement(btnNewIndustryCoverageL).Displayed;
        }
       
        public void ClickNextButtonRecordTypeLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNexRecordTypetL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(btnNexRecordTypetL));
        }

        public void AddNewCoverageTeam(string file, int number)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            if (CustomFunctions.IsElementPresent(driver, btnNewCoverageTeam))
            {
                //Click new coverage team button
                WebDriverWaits.WaitUntilEleVisible(driver, btnNewCoverageTeam, 40);
                driver.FindElement(btnNewCoverageTeam).Click();
            }

            // Enter company
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompany, 40);
            driver.FindElement(txtCompany).SendKeys(ReadExcelData.ReadData(excelPath, "AddCoverageTeam",  1));
            Thread.Sleep(3000);

            // Enter officer name
            WebDriverWaits.WaitUntilEleVisible(driver, txtOfficer, 40);
            driver.FindElement(txtOfficer).SendKeys(ReadExcelData.ReadData(excelPath, "AddCoverageTeam", 2));

            // Enter coverage level
            WebDriverWaits.WaitUntilEleVisible(driver, comboCoverageLevel, 40);
            driver.FindElement(comboCoverageLevel).SendKeys(ReadExcelData.ReadData(excelPath, "AddCoverageTeam", 3));

            // Enter coverage type
            WebDriverWaits.WaitUntilEleVisible(driver, comboType, 40);
            driver.FindElement(comboType).SendKeys(ReadExcelData.ReadData(excelPath, "AddCoverageTeam", 4));

            // Enter Tier
            driver.FindElement(By.XPath("//select[@id='00Ni000000FjXsJ']")).SendKeys(ReadExcelData.ReadData(excelPath, "AddCoverageTeam", 7));

            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();
        }

        public void EditCoverageTeam(string file,int row)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            //Click new coverage team button
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 60);
            driver.FindElement(btnEdit).Click();
            
            // Enter coverage type
            WebDriverWaits.WaitUntilEleVisible(driver, comboType, 40);
            driver.FindElement(comboType).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "EditCoverageTeam", row, 1));

            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();
        }
    }
}
