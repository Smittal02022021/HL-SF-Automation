
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages.CoverageTeam
{
    class CoverageTeamPage: BaseClass
    {
        By btnNewL = By.XPath("//a[@title='New']");
        By headerNewFormL = By.XPath("//h2[contains(text(),'New Coverage Team')]");
        By btnTypeL = By.XPath("//label[text()='Type']/..//button");
        By optionsTypeL = By.XPath("//label[text()='Type']/..//button/../../..//lightning-base-combobox-item//span/span");
        By headerSectionL = By.XPath("//h3//span[@title='Section']");
        By btnCancelL = By.XPath("//button[@name='CancelEdit']");
        By listBoxExpectedProductsL = By.XPath("//div[text()='Expected Products']/..//ul//div[@role='option']/span/span");

        public void ClickNewCoverageTeamButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewL,10);
            driver.FindElement(btnNewL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerNewFormL, 10);  
        }
        public void CancelFormLV()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelL, 10);
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnCancelL));
        }
        public string IsExpectedProductsPresentLV(string product)
        {
            string typeFound = "Not found";
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            WebDriverWaits.WaitUntilEleVisible(driver, headerSectionL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(headerSectionL));           
            IReadOnlyCollection < IWebElement > valproducts = driver.FindElements(listBoxExpectedProductsL);
            foreach (IWebElement valproduct in valproducts)
            {
                CustomFunctions.MoveToElement(driver, valproduct);
                Thread.Sleep(2000);
                string availableType = valproduct.GetAttribute("title");
                if (availableType == product)
                {                        
                typeFound = "Found";
                break;
                } 
            }                
            return typeFound;
        }

        public string IsTypePresentLV(string typeOld)
        {
            string typeFound = "Not found";
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            WebDriverWaits.WaitUntilEleVisible(driver, headerSectionL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(headerSectionL));
            driver.FindElement(btnTypeL).Click();
            By optionsTypeL = By.XPath("//label[text()='Type']/..//button/../../..//lightning-base-combobox-item//span/span");
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(optionsTypeL);
            foreach (IWebElement valType in valTypes)
            {
                //CustomFunctions.MoveToElement(driver, valType);
                string availableType = valType.GetAttribute("title");
                if (availableType == typeOld)
                {
                    typeFound = "Found";
                    break;
                }
            }
            return typeFound;
        }
    }
}
