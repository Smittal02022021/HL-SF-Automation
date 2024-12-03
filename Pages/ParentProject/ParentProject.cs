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
        By lnkParentProjectEng = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.Parent_Project__c']//dd//records-hoverable-link");
        By tabParentProject = By.XPath("//div[2]/div/div[@role='tablist']/ul[@role='presentation']/li[2]/a/span[2]");
        By valAssociatedEng = By.XPath("//table[@aria-label='Engagements']//tbody/tr[1]/th//a/span//span/slot");
        By valProjectClient = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Parent_Project__c.Client__c']//dd//lightning-formatted-text");
        By valProjectLOB = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Parent_Project__c.Line_of_Business__c']//dd//lightning-formatted-text");
        By valProjectCurrency = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Parent_Project__c.CurrencyIsoCode']//dd//lightning-formatted-text");
        By valLegalEntity = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Parent_Project__c.ERP_Legal_Entity__c']//dd//lightning-formatted-text");
        By valParentProgContract = By.XPath("//th[@data-label='Contract Name']//records-hoverable-link//span//span/slot");
        By valContractNumber = By.XPath("//td[@data-label='Contract Number']//lst-formatted-text/span");
        By lnkContract = By.XPath("//th[@data-label='Contract Name']//records-hoverable-link");
        By valTotalFee = By.XPath("//span[text()='Total Fee']/ancestor::div[2]/dd//lightning-formatted-text");
        By valFundingAmount = By.XPath("//span[text()='Funding Amount']/ancestor::div[2]/dd//span");
        By txtSearchProject = By.XPath("//input[@placeholder='Search this list...']");
        By btnRefresh = By.XPath("//button[@name='refreshButton']");
        By valSearchedProjectName = By.XPath("//table/tbody/tr[1]/th/span/a");
        By tabRelated = By.XPath("//a[text()='Related']");
        By lblRelatedSections = By.XPath("//h2[@class='header-title-container']/span");
        By lblBillingRequest = By.XPath("//span[@title='Billing Requests']");
        By tabParentProj = By.XPath("//span[@title='Parent Project  c']");
        By btnEditParentProj = By.XPath("//records-entity-label[text()='Parent Project']/ancestor::records-highlights2//div[@class='slds-grid primaryFieldRow']/div[3]//button[text()='Edit']");
        By txtEditParentProjName = By.XPath("//input[@name='Name']");
        By valUpdatedParentProjName = By.XPath("//records-entity-label[text()='Parent Project']/ancestor::records-highlights2//h1/slot/lightning-formatted-text");
        By btnDeleteParentProj = By.XPath("//records-entity-label[text()='Parent Project']/ancestor::records-highlights2//div[@class='slds-grid primaryFieldRow']/div[3]//button[text()='Delete']");
        By tabProject = By.XPath("//a[@title='Parent Projects Tab']");
        By lnkProjName = By.XPath("//div[2]//td[2]/div[3]//tr[2]/th/a");
        By btnDeleteProjAdmin = By.XPath("//div[4]/div[1]//input[@title='Delete']");
        By lnkBillingReq = By.XPath("//slot[contains(text(),'Billing ')]/ancestor::a");
        By titleBillingReq = By.XPath("//h1[contains(text(),'Billing ')]");
        By btnCloseBillingReq = By.XPath("//button[@title='Close Billing Requests']");
        By btnNewBillingReq = By.XPath("//button[@name='New']");
        By msgBillingReq = By.XPath("//div[text()='Complete this field.']");
        
        
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
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,150)");
            Thread.Sleep(4000);
            driver.FindElement(lnkParentProjectEng).Click();
            //driver.Navigate().Refresh();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, valAssociatedEng,120);
            string eng = driver.FindElement(valAssociatedEng).Text;
            return eng;
        }

        public string GetContractTotalFee()
        {
            driver.FindElement(lnkContract).Click();
            //driver.Navigate().Refresh();
            Thread.Sleep(6000);
            string fee = driver.FindElement(valTotalFee).Text;
            return fee;

        }

        public string GetContractFundingAmount()
        {
            
            string fee = driver.FindElement(valFundingAmount).Text;
            return fee;

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

        //Validate if Search functionality is working as expected
        public string ValidateSearchFunctionalityOfParentProject(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchProject, 150);
            driver.FindElement(txtSearchProject).SendKeys(name);
            Thread.Sleep(5000);
            driver.FindElement(btnRefresh).Click();
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, valSearchedProjectName, 190);
            string project = driver.FindElement(valSearchedProjectName).Text;
            driver.FindElement(valSearchedProjectName).Click();
            return project;
        }

        //Validate Related tabs section
        public bool VerifyRelatedTabSections()
        {
            Thread.Sleep(4000);
            driver.FindElement(tabParentProj).Click();
            Thread.Sleep(4000);
            driver.FindElement(tabRelated).Click();
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblRelatedSections);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            Console.WriteLine("actualValue: " + actualValue[1]);
            //string[] expectedValue = {"CF", "Conflicts Check", "FAS","FR", "HL Internal Opportunity", "OPP DEL","SC"};
            string[] expectedValue = { "Engagements and Internal Teams (2)", "Revenue Accruals (2)", "Expenses (2)", "ERP Invoice Details (2)", "ERP Receipt Detail (2)", "ERP Adjustment Details (2)" };
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

        public string ValidateBillingRequestSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblBillingRequest);
            string billing = driver.FindElement(lblBillingRequest).Text;
            return billing;
        }

        //Validate the Edit functionality of Parent Project
        public string ValidateEditFunctionalityOfParentProject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditParentProj,90);
            driver.FindElement(btnEditParentProj).Click();
            Thread.Sleep(5000);
            driver.FindElement(txtEditParentProjName).Clear();
            driver.FindElement(txtEditParentProjName).SendKeys("Updated Project");
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(valUpdatedParentProjName).Text;
            return value;            
        }

        public string ValidateDeleteParenttProjectButton()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 60);
            Thread.Sleep(4000);
            try
            {
                string value = driver.FindElement(btnDeleteParentProj).Displayed.ToString();
                Console.WriteLine(value);
                if (value.Equals("True"))
                {
                    return "Delete button is displayed";
                }
                else
                {
                    return "Delete button is not displayed";
                }
            }
            catch (Exception)
            {
                return "Delete button is not displayed";
            }
        }


        public string ValidateDeleteParenttProjectButtonForAdmin()
        {
            Thread.Sleep(6000);
            driver.FindElement(tabProject).Click();
            Thread.Sleep(4000);
            driver.FindElement(lnkProjName).Click();

            try
            {
                string value = driver.FindElement(btnDeleteProjAdmin).Displayed.ToString();
                Console.WriteLine(value);
                if (value.Equals("True"))
                {
                    return "Delete button is displayed";
                }
                else
                {
                    return "Delete button is not displayed";
                }
            }
            catch (Exception)
            {
                return "Delete button is not displayed";
            }
        }

        public string ValidateBillingRequestLink()
        {
            Thread.Sleep(5000);
            driver.FindElement(lnkBillingReq).Click();
            Thread.Sleep(5000);
            string value = driver.FindElement(titleBillingReq).Text;
            //driver.FindElement(btnCloseBillingReq).Click();
            return value;
        }

        //Validate Billing Request validations
        public bool ValidateBillingRequestValidations()
        {
            Thread.Sleep(4000);
            driver.FindElement(btnNewBillingReq).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnClose).Click();
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(msgBillingReq);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            Console.WriteLine("actualValue: " + actualValue[1]);
            //string[] expectedValue = {"CF", "Conflicts Check", "FAS","FR", "HL Internal Opportunity", "OPP DEL","SC"};
            string[] expectedValue = { "Complete this field.", "Complete this field.", "Complete this field." };
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
}
