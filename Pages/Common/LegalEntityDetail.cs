using OpenQA.Selenium;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SF_Automation.Pages.Common
{
    class LegalEntityDetail : BaseClass
    {
       
        By valERPLegalEntityId = By.CssSelector("div[id*='ef9']");
        By valERPBusinessUnitId	= By.CssSelector("div[id*='ef4']");
        By valTemplateNumber = By.CssSelector("div[id*='efB']");
        By valERPBusinessUnit = By.CssSelector("div[id*='ef5']");
        By valERPEntityCode = By.CssSelector("div[id*='ef6']");
        By valERPLegislationCode = By.CssSelector("div[id*='efA']");

        By valTemplateNumberL = By.XPath("//records-record-layout-item[contains(@field-label,'Template number')]//lightning-formatted-text");
        By valERPBusinessUnitIdL = By.XPath("//records-record-layout-item[@field-label='ERP Business Unit Id']//lightning-formatted-text");
        By valERPBusinessUnitL = By.XPath("//records-record-layout-item[@field-label='ERP Business Unit']//lightning-formatted-text");
        By valERPLegalEntityIdL = By.XPath("//records-record-layout-item[@field-label='ERP Legal Entity Id']//lightning-formatted-text");
        By valERPEntityCodeL = By.XPath("//records-record-layout-item[@field-label='ERP Entity Code']//lightning-formatted-text");
        By valERPLegislationCodeL = By.XPath("//records-record-layout-item[@field-label='ERP Legislation Code']//lightning-formatted-text");

        //Get ERP Legal Entity ID 
        public string GetERPLegalEntityID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityId, 80);
            string id = driver.FindElement(valERPLegalEntityId).Text;
            return id;
        }

        //Get ERP Business Unit ID 
        public string GetERPBusinessUnitID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusinessUnitId, 80);
            string id = driver.FindElement(valERPBusinessUnitId).Text;
            return id;
        }

        //Get ERP Template number
        public string GetERPTemplateNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTemplateNumber, 80);
            string number = driver.FindElement(valTemplateNumber).Text;
            return number;
        }

        //Get ERP Business Unit
        public string GetERPBusinessUnit()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusinessUnit, 80);
            string unit = driver.FindElement(valERPBusinessUnit).Text;
            return unit;
        }

        //Get ERP Entity Code
        public string GetERPEntityCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPEntityCode, 80);
            string code = driver.FindElement(valERPEntityCode).Text;
            return code;
        }

        //Get ERP Legislation Code
        public string GetERPLegislationCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegislationCode, 80);
            string code = driver.FindElement(valERPLegislationCode).Text;
            return code;
        }

        public string GetERPTemplateNumberLV()

        {

            WebDriverWaits.WaitUntilEleVisible(driver, valTemplateNumberL, 10);

            CustomFunctions.MoveToElement(driver, driver.FindElement(valTemplateNumberL));

            string number = driver.FindElement(valTemplateNumberL).Text;

            return number;

        }

        public string GetERPBusinessUnitIDLV()

        {

            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusinessUnitIdL, 10);

            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPBusinessUnitIdL));

            string id = driver.FindElement(valERPBusinessUnitIdL).Text;

            return id;

        }

        public string GetERPBusinessUnitLV()

        {

            WebDriverWaits.WaitUntilEleVisible(driver, valERPBusinessUnitL, 10);

            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPBusinessUnitL));

            string unit = driver.FindElement(valERPBusinessUnitL).Text;

            return unit;

        }

        public string GetERPLegalEntityIDLV()

        {

            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityIdL, 10);

            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPLegalEntityIdL));

            string id = driver.FindElement(valERPLegalEntityIdL).Text;

            return id;

        }

        public string GetERPEntityCodeLV()

        {

            WebDriverWaits.WaitUntilEleVisible(driver, valERPEntityCodeL, 10);

            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPEntityCodeL));

            string code = driver.FindElement(valERPEntityCodeL).Text;

            return code;

        }

        public string GetERPLegislationCodeLV()

        {

            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegislationCodeL, 10);

            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPLegislationCodeL));

            string code = driver.FindElement(valERPLegislationCodeL).Text;

            return code;

        }

    }
}
