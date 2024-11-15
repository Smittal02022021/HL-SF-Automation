using OpenQA.Selenium;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SalesForce_Project.Pages
{
     class ParentProject : BaseClass
    {

        By btnNew = By.XPath("//div[@title='New']");
        By titleProject = By.XPath("//h2[text()='New Parent Project']");
        By msgProject = By.XPath("//flexipage-field//div[contains(text(),'Complete')]");
        By btnSave = By.XPath("//button[@name='SaveEdit']");
        By btnClose = By.XPath("//records-record-edit-error-header//button/lightning-primitive-icon");
        By txtName = By.XPath("//input[@name='Name']");
        By txtBillTo = By.XPath("//input[@placeholder='Search Companies...']");
        By valAddedProject = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Parent_Project__c.Name']//dd//span//lightning-formatted-text");
        By btnEditParentProject = By.XPath("//flexipage-tab2[1]/slot/flexipage-component2[1]//flexipage-column2[2]/div/slot/flexipage-field[13]//div/button");
        By txtParentProject = By.XPath("//input[@placeholder='Search Parent Projects...']");
        By btnClearParentProject = By.XPath("//label[text()='Parent Project']/ancestor::lightning-grouped-combobox//button[@title='Clear Selection']");
        By valParentProjectEng = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.Parent_Project__c']//dd//records-hoverable-link//span//span/slot");
        By tabParentProject = By.XPath("//div[2]/div/div[@role='tablist']/ul[@role='presentation']/li[2]/a/span[2]");
        By valAssociatedEng = By.XPath("//table[@aria-label='Engagements']//tbody//th//a/span//span/slot");
        By valProjectClient = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Parent_Project__c.Client__c']//dd//lightning-formatted-text");
        By valProjectLOB = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Parent_Project__c.Line_of_Business__c']//dd//lightning-formatted-text");
        By valProjectCurrency = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Parent_Project__c.CurrencyIsoCode']//dd//lightning-formatted-text");
        By valLegalEntity = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Parent_Project__c.ERP_Legal_Entity__c']//dd//lightning-formatted-text");
        By valParentProgContract = By.XPath("//th[@data-label='Contract Name']//records-hoverable-link//span//span/slot");
        By valContractNumber = By.XPath("//td[@data-label='Contract Number']//lst-formatted-text/span");

        public string ClickNewButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNew);
            driver.FindElement(btnNew).Click();
            Thread.Sleep(4000);
            //driver.SwitchTo().Frame(0);
            WebDriverWaits.WaitUntilEleVisible(driver, titleProject);
            string title=  driver.FindElement(titleProject).Text;
            return title;
        }
        public bool VerifyParentProjectMandatoryValdiations()
        {
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnClose);
            driver.FindElement(btnClose).Click();
            Thread.Sleep(7000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(msgProject);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);
            string[] expectedValue = { "Parent Project Name\r\nComplete this field.", "Bill To\r\nComplete this field." };
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

        public string CreateNewParentProject(string value)
        {            
            driver.FindElement(txtName).SendKeys(value);
            driver.FindElement(txtBillTo).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//lightning-grouped-combobox//lightning-base-combobox//div[2]/ul/li[2]")).Click();
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedProject);
            string project = driver.FindElement(valAddedProject).Text;
            return project;
        }

        //Associate Parent project to an Engagement
        public string AssociateParentProjectToEng(string name)
        {
            Thread.Sleep(5000);            
            driver.FindElement(btnEditParentProject).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnClearParentProject);
            driver.FindElement(btnClearParentProject).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtParentProject);
            driver.FindElement(txtParentProject).SendKeys(name);
            Thread.Sleep(7000);
            driver.FindElement(By.XPath("//flexipage-tab2[1]//flexipage-tab2[1]/slot/flexipage-component2[1]/slot//flexipage-column2[2]//flexipage-field[13]//div[2]/ul")).Click();
            Thread.Sleep(7000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(8000);
            string project = driver.FindElement(valParentProjectEng).Text;
            return project;
        }

        public string GetParentContract()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valParentProgContract);
            string contract = driver.FindElement(valParentProgContract).Text;
            return contract;
        }

        public string GetParentContractNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContractNumber);
            string number = driver.FindElement(valContractNumber).Text;
            return number;
        }

        //Associate Parent project to an Engagement
        public string ValidateAssociatedEngToParentProject()
        {          
            
            driver.FindElement(tabParentProject).Click();
            driver.Navigate().Refresh();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valAssociatedEng);            
            string eng = driver.FindElement(valAssociatedEng).Text;
            return eng;
        }

        //Associate Parent project to an Engagement
        public string ValidateAssociated2ndEngToParentProject()
        {

            driver.FindElement(tabParentProject).Click();
            //driver.Navigate().Refresh();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valAssociatedEng);
            string eng = driver.FindElement(valAssociatedEng).Text;
            return eng;
        }

        public string GetClientCompanyL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valProjectClient);
            string LOB = driver.FindElement(valProjectClient).Text;
            return LOB;
        }

        public string GetLOBL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valProjectLOB);
            string LOB = driver.FindElement(valProjectLOB).Text;
            return LOB;
        }

        public string GetCurrencyL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valProjectCurrency);
            string LOB = driver.FindElement(valProjectCurrency).Text;
            return LOB;
        }

        public string GetLegalEntityL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLegalEntity);
            string LOB = driver.FindElement(valLegalEntity).Text;
            return LOB;
        }

    }
}
