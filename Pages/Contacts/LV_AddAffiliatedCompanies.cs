using NUnit.Framework;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Security.Policy;
using System.Threading;

namespace SF_Automation.Pages.Contact
{
    class LV_AddAffiliatedCompanies : BaseClass
    {
        By headingNewAffiliate = By.CssSelector("h2[class='pageDescription']");
        By btnSave = By.XPath("//button[@name='SaveEdit']");
        By btnCancel = By.CssSelector("td[id='topButtonRow'] > input[value='Cancel']");
        By valPageLevelError = By.CssSelector("div[id='errorDiv_ep']");
        By txtCompanyName = By.XPath("//input[@placeholder='Search Companies...']");
        By txtContactName = By.CssSelector("input[id='CF00Ni000000D735q']");
        By comboAffiliationStatus = By.CssSelector("select[id='00Ni000000D737m']");
        By comboAffiliationType = By.XPath("//button[@data-value='--None--']");
        By affiliationNotes = By.XPath("//label[text()='Notes']/following::textarea");
        By linkEditAffiliationCompany = By.CssSelector("div[id*='00Ni000000D735q_body'] > table  > tbody > tr:nth-child(2) > td[class='actionColumn'] > a:nth-child(1)");
        By btnSaveAndNew = By.CssSelector("td[id='topButtonRow'] > input[name='save_new']");

        By valAffiliationCompanyName = By.XPath("(//span[text()='Company']/following::div//slot//slot)[2]/span");
        By valAffiliationCompanyStatus = By.XPath("((//span[text()='Status'])[4]/following::div//slot/lightning-formatted-text)[1]");
        By valAffiliationCompanyType = By.XPath("((//span[text()='Status'])[4]/following::div//slot/lightning-formatted-text)[2]");
        By linkDeleteAffiliationCompany = By.XPath("(//button[text()='Delete'])[2]");
        By buttonDelete = By.XPath("//button[@title='Delete']");

        //FUnction to get new affiliation heading
        public string GetNewAffliationCompaniesHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, headingNewAffiliate, 60);
            string headingNewAffliation = driver.FindElement(headingNewAffiliate).Text;
            return headingNewAffliation;
        }

        // To identify required tags/mandatory fields in Contact Create page
        public IWebElement AddAffilationRequiredTag(string tagName,string fieldName)
        {
            IWebElement element = driver.FindElement(By.XPath($"//{tagName}[@id='{fieldName}']/../..//div"));
            return element;// driver.FindElement(By.XPath($"//input[@id='{fieldName}')]/../..//div"));
        }

        // Validate mandatory fields on new affilation companies page
        public bool ValidateMandatoryFields()
        {
            return AddAffilationRequiredTag("input","CF00Ni000000D735b").GetAttribute("class").Contains("requiredBlock") &&
            AddAffilationRequiredTag("input","CF00Ni000000D735q").GetAttribute("class").Contains("requiredBlock") &&
            AddAffilationRequiredTag("select", "00Ni000000D737m").GetAttribute("class").Contains("requiredBlock") &&
            AddAffilationRequiredTag("select", "00Ni000000D737h").GetAttribute("class").Contains("requiredBlock");
        }

        public void ClickSaveButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
        }

        public void ClickSaveAndNewButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveAndNew);
            driver.FindElement(btnSaveAndNew).Click();
        }

        public bool GetPageLevelError()
        {
            Thread.Sleep(5000);
            bool result = false;

            string pageLevelError1 = driver.FindElement(By.XPath("//span[text()='Company']/..")).Text;
            string pageLevelError2;
            if(driver.FindElement(By.XPath("(//span[text()='Type']/..)[1]")).Displayed)
            {
                pageLevelError2 = driver.FindElement(By.XPath("(//span[text()='Type']/..)[1]")).Text;
            }
            else
            {
                pageLevelError2 = driver.FindElement(By.XPath("(//span[text()='Type']/..)[2]")).Text;
            }

            if(pageLevelError1.Contains("Complete this field.") && pageLevelError2.Contains("Complete this field."))
            {
                result = true;
            }
            return result;
        }

        public void EnterNewAffilationCompaniesDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName, 40);
            driver.FindElement(txtCompanyName).Clear();
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "AffiliatedCompany", 1));
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//strong[text()='StandardTestCompany']/../../../..")).Click();
            
            WebDriverWaits.WaitUntilEleVisible(driver, comboAffiliationType, 40);
            driver.FindElement(comboAffiliationType).SendKeys(ReadExcelData.ReadData(excelPath, "AffiliatedCompany", 4));
            driver.FindElement(comboAffiliationType).SendKeys(Keys.Enter);

            WebDriverWaits.WaitUntilEleVisible(driver, affiliationNotes, 40);
            driver.FindElement(affiliationNotes).SendKeys(ReadExcelData.ReadData(excelPath, "AffiliatedCompany", 5));
        }

        public void EditNewAffilationCompaniesDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, linkEditAffiliationCompany, 40);
            driver.FindElement(linkEditAffiliationCompany).Click();
            
            WebDriverWaits.WaitUntilEleVisible(driver, comboAffiliationStatus, 40);
            driver.FindElement(comboAffiliationStatus).SendKeys(ReadExcelData.ReadData(excelPath, "EditAffiliatedCompany", 1));
            WebDriverWaits.WaitUntilEleVisible(driver, comboAffiliationType, 40);
            driver.FindElement(comboAffiliationType).SendKeys(ReadExcelData.ReadData(excelPath, "EditAffiliatedCompany", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, affiliationNotes, 40);
            driver.FindElement(affiliationNotes).Clear();
            driver.FindElement(affiliationNotes).SendKeys(ReadExcelData.ReadData(excelPath, "EditAffiliatedCompany", 3));
        }

        public void ClickCancelButton()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel);
            driver.FindElement(btnCancel).Click();
        }

        public string GetCompanyNameText()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName, 40);
            return driver.FindElement(txtCompanyName).Text;
        }

        public string GetContactNameText()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, txtContactName, 40);
            return driver.FindElement(txtContactName).Text;
        }

        public string GetAffiliationID()
        {
            Thread.Sleep(1000);
            string result = driver.FindElement(By.XPath("//lightning-formatted-text[contains(text(),'A-')]")).Text;
            return result;
        }

        //Function to get affiliation Company name
        public string GetAffiliationCompanyName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAffiliationCompanyName, 60);
            string affiliationCompanyName = driver.FindElement(valAffiliationCompanyName).Text;
            return affiliationCompanyName;
        }

        // Function to get affiliation company status
        public string GetAffiliationCompanyStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAffiliationCompanyStatus, 60);
            string affiliationCompanyStatus = driver.FindElement(valAffiliationCompanyStatus).Text;
            return affiliationCompanyStatus;
        }

        // Function to get affiliation company type
        public string GetAffiliationCompanyType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAffiliationCompanyType, 60);
            string affiliationCompanyType = driver.FindElement(valAffiliationCompanyType).Text;
            return affiliationCompanyType;
        }

        // Function to delete affiliated companies
        public void DeleteAffiliatedCompanies()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkDeleteAffiliationCompany);
            driver.FindElement(linkDeleteAffiliationCompany).Click();

            Thread.Sleep(3000);
            driver.FindElement(buttonDelete).Click();
            Thread.Sleep(3000);
        }
    }
}
