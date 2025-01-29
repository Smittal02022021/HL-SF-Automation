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
        By btnSave1 = By.XPath("(//button[@name='SaveEdit'])[1]");
        By btnSave2 = By.XPath("(//input[@value='Save'])[2]");
        By btnCancel1 = By.XPath("(//button[@name='CancelEdit'])[1]");
        By btnCancel2 = By.XPath("(//input[@value='Cancel'])[2]");

        By linkCompanyNameLookupIcon = By.XPath("//a[@title='Company Name Lookup (New Window)']/img");
        By selectSalutation = By.XPath("//button[@aria-label='Salutation']");
        By inputFirstName = By.XPath("//input[@name='firstName']");
        By inputMiddleName = By.XPath("//input[@name='middleName']");
        By inputLastName = By.XPath("//input[@name='lastName']");
        By inputTitle = By.XPath("//input[@name='Title']");
        By inputEmail = By.XPath("//input[@name='Email']");
        By inputPhone = By.XPath("//input[@name='Phone']");
        By inputMobilePhone = By.XPath("//input[@name='MobilePhone']");

        By inputCompanySearchBox = By.XPath("//input[@placeholder='Search Companies...']");
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

            /*
            try
            {
                driver.SwitchTo().Frame(0);
            }
            catch(Exception)
            {

            }
            */

            //Click lookup 
            //WebDriverWaits.WaitUntilEleVisible(driver, linkCompanyNameLookupIcon, 120);
            //CustomFunctions.ActionClicks(driver, linkCompanyNameLookupIcon, 20);

            // Switch to second window
            //CustomFunctions.SwitchToWindow(driver, 1);

            // Enter value in Company search box
            WebDriverWaits.WaitUntilEleVisible(driver, inputCompanySearchBox);
            driver.FindElement(inputCompanySearchBox).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 1));
            Thread.Sleep(2000);

            try
            {
                //Select the company
                driver.FindElement(By.XPath("(//lightning-base-combobox-item)[2]/span[2]")).Click();
            }
            catch(Exception)
            {

            }
            
            /*
            //Click on Go button
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo);
            driver.FindElement(btnGo).Click();

            // Select first option
            WebDriverWaits.WaitUntilEleVisible(driver, selFirstOption);
            CustomFunctions.ActionClicks(driver, selFirstOption);

            // Switch back to default window
            CustomFunctions.SwitchToWindow(driver, 0);
            driver.SwitchTo().Frame(0);
            */

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

        public void CreateNewContactMultipleRows(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(5000);

            /*
            try
            {
                driver.SwitchTo().Frame(0);
            }
            catch(Exception)
            {

            }
            */

            //Click lookup 
            //WebDriverWaits.WaitUntilEleVisible(driver, linkCompanyNameLookupIcon, 120);
            //CustomFunctions.ActionClicks(driver, linkCompanyNameLookupIcon, 20);

            // Switch to second window
            //CustomFunctions.SwitchToWindow(driver, 1);

            // Enter value in Company search box
            WebDriverWaits.WaitUntilEleVisible(driver, inputCompanySearchBox);
            driver.FindElement(inputCompanySearchBox).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1));
            Thread.Sleep(2000);

            try
            {
                //Select the company
                driver.FindElement(By.XPath("(//lightning-base-combobox-item)[2]/span[2]")).Click();
            }
            catch(Exception)
            {

            }

            /*
            //Click on Go button
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo);
            driver.FindElement(btnGo).Click();

            // Select first option
            WebDriverWaits.WaitUntilEleVisible(driver, selFirstOption);
            CustomFunctions.ActionClicks(driver, selFirstOption);

            // Switch back to default window
            CustomFunctions.SwitchToWindow(driver, 0);
            driver.SwitchTo().Frame(0);
            */

            //Enter first name
            WebDriverWaits.WaitUntilEleVisible(driver, inputFirstName, 40);
            driver.FindElement(inputFirstName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2));

            //Enter last name
            WebDriverWaits.WaitUntilEleVisible(driver, inputLastName, 40);
            driver.FindElement(inputLastName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 3));

            //Enter email
            WebDriverWaits.WaitUntilEleVisible(driver, inputEmail, 40);
            driver.FindElement(inputEmail).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 4));

            //Enter phone
            WebDriverWaits.WaitUntilEleVisible(driver, inputPhone, 40);
            driver.FindElement(inputPhone).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 5));

            // Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave1);
            driver.FindElement(btnSave1).Click();
            Thread.Sleep(5000);
        }

        // To identify required tags/mandatory fields in Contact Create page
        public IWebElement ContactInformationRequiredTag(string fieldName)
        {
            return driver.FindElement(By.XPath($"//label[contains(text(), '{fieldName}')]/abbr"));
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
            //driver.SwitchTo().Frame(0);
            Thread.Sleep(2000);

            return ContactInformationRequiredTag("Company Name").GetAttribute("title").Contains("required") &&
            ContactInformationRequiredTag("Last Name").GetAttribute("title").Contains("required");
        }

        public void SelectContactType(string type)
        {
            Thread.Sleep(5000);
            switch (type)
            {
                case "External Contact":
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

        public string GetMandatoryFieldErrMsgForCompanyField()
        {
            string msg = driver.FindElement(By.XPath("(//span[text()='Company Name'])[2]/..")).Text;
            return msg;
        }

        public string GetMandatoryFieldErrMsgForLastNameField()
        {
            string msg = driver.FindElement(By.XPath("(//span[text()='Last Name'])[1]/..")).Text;
            return msg;
        }
    }
}