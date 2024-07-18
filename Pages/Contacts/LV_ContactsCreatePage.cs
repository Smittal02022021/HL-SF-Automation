using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Threading;

namespace SF_Automation.Pages.Contact
{
    class LV_ContactsCreatePage : BaseClass
    {
        By btnSave1 = By.XPath("(//input[@value='Save'])[1]");
        By btnSave2 = By.XPath("(//input[@value='Save'])[2]");
        By btnCancel1 = By.XPath("(//input[@value='Cancel'])[1]");
        By btnCancel2 = By.XPath("(//input[@value='Cancel'])[2]");

        By linkCompanyNameLookupIcon = By.XPath("//a[@title='Company Name Lookup (New Window)']/img");
        By selectSalutation = By.Id("contactNewPage:NewContactForm:pgBlock:pgBlockSectionAcctInfo:Salutation");
        By inputFirstName = By.Id("contactNewPage:NewContactForm:pgBlock:pgBlockSectionAcctInfo:FirstName");
        By inputMiddleName = By.Id("contactNewPage:NewContactForm:pgBlock:pgBlockSectionAcctInfo:MiddleName");
        By inputLastName = By.Id("contactNewPage:NewContactForm:pgBlock:pgBlockSectionAcctInfo:LastName");
        By inputTitle = By.Id("contactNewPage:NewContactForm:pgBlock:pgBlockSectionAcctInfo:Title");
        By inputEmail = By.Id("contactNewPage:NewContactForm:pgBlock:pgBlockSectionAcctInfo:Email");
        By inputPhone = By.Id("contactNewPage:NewContactForm:pgBlock:pgBlockSectionAcctInfo:Phone");
        By inputMobilePhone = By.Id("contactNewPage:NewContactForm:pgBlock:pgBlockSectionAcctInfo:MobilePhone");

        By inputCompanySearchBox = By.XPath("//input[@id='j_id0:j_id1:j_id2:formId:txtSearch']");
        By btnGo = By.XPath("//input[@id='j_id0:j_id1:j_id2:formId:btnGo']");
        By selFirstOption = By.CssSelector("td[id*='tblResults:0:j_id49'] > a");

        //New Contact Page - Select Contact Type
        By radioExternalContact = By.XPath("//span[text()='External Contact']/../input");
        By radioArchivedContact = By.XPath("//span[text()='Archived']/../input");
        By radioConflictsCheckLDCCRContact = By.XPath("//span[text()='Conflicts Check LDCCR']/../input");
        By radioDistributionListsContact = By.XPath("//span[text()='Distribution Lists']/../input");
        By radioHoulihanEmployeeContact = By.XPath("//span[text()='Houlihan Employee']/../input");
        By btnNext = By.XPath("//span[text()='Next']/..");
        By btnCancel = By.XPath("(//span[text()='Cancel']/..)[2]");

        public void CreateNewContact(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(5000);

            driver.SwitchTo().Frame(0);

            //Click lookup 
            WebDriverWaits.WaitUntilEleVisible(driver, linkCompanyNameLookupIcon, 120);
            CustomFunctions.ActionClicks(driver, linkCompanyNameLookupIcon, 20);

            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);

            // Enter value in search box
            WebDriverWaits.WaitUntilEleVisible(driver, inputCompanySearchBox);
            driver.FindElement(inputCompanySearchBox).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 1));

            //Click on Go button
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo);
            driver.FindElement(btnGo).Click();

            // Select first option
            WebDriverWaits.WaitUntilEleVisible(driver, selFirstOption);
            CustomFunctions.ActionClicks(driver, selFirstOption);

            // Switch back to default window
            CustomFunctions.SwitchToWindow(driver, 0);
            driver.SwitchTo().Frame(0);

            //Enter first name
            WebDriverWaits.WaitUntilEleVisible(driver, inputFirstName, 40);
            driver.FindElement(inputFirstName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 2));

            //Enter last name
            WebDriverWaits.WaitUntilEleVisible(driver, inputLastName, 40);
            driver.FindElement(inputLastName).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 3));

            //Enter email
            WebDriverWaits.WaitUntilEleVisible(driver, inputEmail, 40);
            driver.FindElement(inputEmail).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 4));

            //Enter phone
            WebDriverWaits.WaitUntilEleVisible(driver, inputPhone, 40);
            driver.FindElement(inputPhone).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 5));

            // Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave1);
            driver.FindElement(btnSave1).Click();
            Thread.Sleep(5000);
        }

        // To identify required tags/mandatory fields in Contact Create page
        public IWebElement ContactInformationRequiredTag(string fieldName)
        {
            return driver.FindElement(By.XPath($"//input[contains(@id, '{fieldName}')]/..//div"));
        }

        //To Click save button
        public void ClickSaveButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave1);
            driver.FindElement(btnSave1).Click();
            Thread.Sleep(5000);
        }

        //To Click Cancel button
        public void ClickCancelButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel1);
            driver.FindElement(btnCancel1).Click();
            Thread.Sleep(5000);

        }

        public bool ValidateMandatoryFields()
        {
            driver.SwitchTo().Frame(0);
            Thread.Sleep(2000);

            return ContactInformationRequiredTag("FirstName").GetAttribute("class").Contains("requiredBlock") &&
            ContactInformationRequiredTag("LastName").GetAttribute("class").Contains("requiredBlock") &&
            ContactInformationRequiredTag("Account").GetAttribute("class").Contains("requiredBlock");
        }

        public void SelectContactType(string type)
        {
            switch (type)
            {
                case "External Contact":
                    WebDriverWaits.WaitUntilEleVisible(driver, radioExternalContact, 20);
                    driver.FindElement(radioExternalContact).Click();
                    break;
                case "Archived":
                    WebDriverWaits.WaitUntilEleVisible(driver, radioArchivedContact, 20);
                    driver.FindElement(radioArchivedContact).Click();
                    break;
                case "Conflicts Check LDCCR":
                    WebDriverWaits.WaitUntilEleVisible(driver, radioConflictsCheckLDCCRContact, 20);
                    driver.FindElement(radioConflictsCheckLDCCRContact).Click();
                    break;
                case "Distribution Lists":
                    WebDriverWaits.WaitUntilEleVisible(driver, radioDistributionListsContact, 20);
                    driver.FindElement(radioDistributionListsContact).Click();
                    break;
                case "Houlihan Employee":
                    WebDriverWaits.WaitUntilEleVisible(driver, radioHoulihanEmployeeContact, 20);
                    driver.FindElement(radioHoulihanEmployeeContact).Click();
                    break;
            }

            //Click Next
            driver.FindElement(btnNext).Click();
            Thread.Sleep(3000);
        }
    }
}