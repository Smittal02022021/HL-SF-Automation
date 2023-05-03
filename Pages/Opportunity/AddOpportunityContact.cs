using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System.Threading;
using System;

namespace SF_Automation.Pages.Opportunity
{
    class AddOpportunityContact : BaseClass
    {
        By btnAddOppContact = By.CssSelector("input[name='new_external_team']");
        By btnSave = By.CssSelector("input[name='save']");
        By txtContact = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000D7Ns3']");
        By comboRole = By.CssSelector("select[name*='D7OOx']");
        By comboType = By.CssSelector("select[name*='D7OAq']");
        By checkPrimaryContact = By.CssSelector("input[name*='D7Nro']");
        By comboParty = By.CssSelector("select[name*='M0eMp']");
        By checkAckBillingContact = By.CssSelector("input[name*='M0jSN']");
        By checkBillingContact = By.CssSelector("input[name*='Gz3dL']");

        //Lightning--
        By btnPartyL = By.XPath("//div[4]/div[1]/div/div/div/div/div[1]/div/div/a");
        By chkBillingContactL = By.XPath("//span[text()='Billing Contact']/following::input[1]");
        By chkAckBillingContactL = By.XPath("//span[text()='Acknowledge Billing Contact']/following::input[1]");
        By chkPrimaryContactL = By.XPath("//span[text()='Primary Contact']/following::input[1]");
        By txtContactL = By.XPath("//input[@title='Search Contacts']");
        By imgContactL = By.XPath("//div[2]/ul/li[26]/a/div[1]/span/img");
        By btnSaveL = By.XPath("//div/footer/button[2]/span");
        By tabRelated = By.XPath("//a[text()='Related']");
        By valAddedContact = By.XPath("//formula-output-formula-html/lightning-formatted-rich-text/span/a[2]");
        By msgContact = By.XPath("//section/div/div/div/div/div/div[1]/div[1]/div/div/ul/li");
        By msgParty = By.XPath("//section/div/section/div/div/div/div/div/div[4]/div[1]/div/div/ul/li");
        By btnCancelContact = By.XPath("//footer/button[1]/span");
        By valContactNum = By.XPath("//flexipage-component2[2]/slot/flexipage-tabset2/div/lightning-tabset/div/slot/slot/flexipage-tab2[2]/slot/flexipage-component2[2]/slot/lst-dynamic-related-list/article/laf-progressive-container/slot/lst-dynamic-related-list-with-user-prefs/lst-related-list-view-manager/lst-common-list-internal/lst-list-view-manager-header/div/div[1]/div[1]/div/div/h2/a/span[2]");

        By btnAddCFContactL = By.XPath("//button[contains(@name,'Add_CF_Opportunity_Contact')]");
        //string dir = @"C:\Users\vkumar0427\source\repos\SF_Automation\TestData\";

        public void CreateContact(string file, string contact, string valRecType, string valType)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, btnAddOppContact, 70);
            driver.FindElement(btnAddOppContact).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 80);

            driver.FindElement(txtContact).SendKeys(contact);
            driver.FindElement(comboRole).SendKeys(ReadExcelData.ReadData(excelPath, "AddContact", 2));
            driver.FindElement(comboType).SendKeys(valType);
            driver.FindElement(checkPrimaryContact).Click();
            if (valRecType.Equals("CF"))
            {
                driver.FindElement(comboParty).SendKeys(ReadExcelData.ReadData(excelPath, "AddContact", 3));
            }
            driver.FindElement(checkAckBillingContact).Click();
            driver.FindElement(checkBillingContact).Click();
            driver.FindElement(btnSave).Click();
        }
        public void CreateContact(string file, string contact, string valRecType, string valType, int rowNumber)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, btnAddOppContact, 50);
            driver.FindElement(btnAddOppContact).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 80);

            driver.FindElement(txtContact).SendKeys(contact);
            driver.FindElement(comboRole).SendKeys(ReadExcelData.ReadData(excelPath, "AddContact", 2));
            driver.FindElement(comboType).SendKeys(valType);

            string Type = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowNumber, 4);
            if (Type.Equals("Client"))
            {
                driver.FindElement(checkPrimaryContact).Click();
            }
            if (valRecType.Equals("CF"))
            {
                driver.FindElement(comboParty).SendKeys(ReadExcelData.ReadData(excelPath, "AddContact", 3));
            }
            if (Type.Equals("External"))
            {
                driver.FindElement(checkPrimaryContact).Click();
                driver.FindElement(checkAckBillingContact).Click();
                driver.FindElement(checkBillingContact).Click();
            }
            driver.FindElement(btnSave).Click();
        }
        public void CickAddCFOpportunityContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCFContactL, 20);
            driver.FindElement(btnAddCFContactL).Click();
        }
        public void CreateContactL2(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string name = ReadExcelData.ReadData(excelPath, "AddContact", 1);
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactL, 20);
            driver.FindElement(txtContactL).SendKeys(name);
            Thread.Sleep(3000);
            try
            {
                By listContactOption = By.XPath($"//div[@role='listbox']//ul//li//a//div[2]//div[1][@title='{name}']");
                WebDriverWaits.WaitUntilEleVisible(driver, listContactOption, 20);
                driver.FindElement(listContactOption).Click();

            }
            catch (Exception ex)
            {
                By iconContactSearchItem = By.XPath("//div[contains(@class,'searchButton')]");
                WebDriverWaits.WaitUntilEleVisible(driver, iconContactSearchItem, 5);
                driver.FindElement(iconContactSearchItem).Click();
                By txtContact = By.XPath("//div[contains(@class,'gridInScroller')]//table//tbody//tr[1]//td[1]//a");
                WebDriverWaits.WaitUntilEleVisible(driver, txtContact, 20);
                driver.FindElement(txtContact).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnPartyL, 20);
            driver.FindElement(btnPartyL).Click();
            Thread.Sleep(3000);
            string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
            driver.FindElement(By.XPath("//div[8]/div/ul/li/a[text()='" + party + "']")).Click();
            driver.FindElement(chkBillingContactL).Click();
            driver.FindElement(chkAckBillingContactL).Click();
            driver.FindElement(chkPrimaryContactL).Click();
            driver.FindElement(btnSaveL).Click();
        }
    }
}


