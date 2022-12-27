using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages.Contacts
{
    class LegalEntityDetailPage: BaseClass
    {
        By valERPBusinessUnit = By.CssSelector("div[id*='M0ef5']");
        By valERPBusinessUnitId = By.CssSelector("div[id*='M0ef4']");

        // Function to get business unit value
        public string GetERPBusinessUnit()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusinessUnit, 60);
            string ERPBusinessUnit = driver.FindElement(valERPBusinessUnit).Text;
            return ERPBusinessUnit;
        }

        // Function to get legal entity id
        public string GetERPBusinessUnitId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusinessUnitId, 60);
            string ERPBusinessUnitId = driver.FindElement(valERPBusinessUnitId).Text;
            return ERPBusinessUnitId;
        }
    }
}
