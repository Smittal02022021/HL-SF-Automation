using OpenQA.Selenium;
using SF_Automation.UtilityFunctions;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages.Companies
{
    class LVTearsheetPage : BaseClass
    {
        By inputSearchCompany = By.XPath("//input[@placeholder='Search Company..']");
        By lblCompany = By.XPath("//table[@class='slds-table slds-table_cell-buffer slds-table_bordered']/tr[1]/div");
        By lblCompanyName = By.XPath("(//div[@class='ql-editor']/p/strong)[1]");
        By lblHQCompanyName = By.XPath("(//div[@class='ql-editor']/p/strong)[2]");
        By btnClearSearchField = By.XPath("//button[@title='Clear']");

        public bool VerifyTearsheetSearchPageHaveCompanySearchField()
        {
            bool result = false;

            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, inputSearchCompany, 120);
            if(driver.FindElement(inputSearchCompany).Displayed)
            {
                result = true;
            }

            return result;
        }

        public bool VerifyCompanyDropdownIsPopulatedWhenUserEntersCompanyNameInCompanySearchField(string company)
        {
            bool result = false;

            WebDriverWaits.WaitUntilEleVisible(driver, inputSearchCompany, 120);
            driver.FindElement(inputSearchCompany).SendKeys(company);
            Thread.Sleep(10000);

            IList<IWebElement> elements = driver.FindElements(By.XPath("//table[@class='slds-table slds-table_cell-buffer slds-table_bordered']/tr"));
            int size = elements.Count;

            for (int items = 1; items <= size; items++)
            {
                By comName = By.XPath($"//table[@class='slds-table slds-table_cell-buffer slds-table_bordered']/tr[{items}]/div/span");
                string itemName = driver.FindElement(comName).Text;

                if (itemName.Contains(company))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public bool VerifyClickingOnXSignClearsOutSearchField(string company)
        {
            bool result = false;

            WebDriverWaits.WaitUntilEleVisible(driver, inputSearchCompany, 120);
            driver.FindElement(inputSearchCompany).SendKeys(company);
            Thread.Sleep(10000);

            WebDriverWaits.WaitUntilClickable(driver, btnClearSearchField, 120);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebElement ele = (WebElement)driver.FindElement(btnClearSearchField);
            js.ExecuteScript("arguments[0].click()", ele);

            Thread.Sleep(5000);

            string textValue = driver.FindElement(inputSearchCompany).GetAttribute("value");
            if(textValue=="")
            {
                result = true;
            }

            return result;
        }

        public bool VerifyHQCompanyHaveHQFlagInSearch()
        {
            bool result = false;

            IList<IWebElement> elements = driver.FindElements(By.XPath("//table[@class='slds-table slds-table_cell-buffer slds-table_bordered']/tr"));
            int size = elements.Count;

            for (int items = 1; items <= size; items++)
            {
                By comName = By.XPath($"//table[@class='slds-table slds-table_cell-buffer slds-table_bordered']/tr[{items}]/div");
                string itemName = driver.FindElement(comName).Text;

                if (itemName.Contains("HQ"))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public bool VerifyCompanyTearsheetOpensUpUponSelectingCompanyFromDropdown(string company)
        {
            bool result = false;

            driver.Navigate().Refresh();
            Thread.Sleep(10000);

            WebDriverWaits.WaitUntilEleVisible(driver, inputSearchCompany, 120);
            driver.FindElement(inputSearchCompany).SendKeys(company);
            Thread.Sleep(10000);

            WebDriverWaits.WaitUntilClickable(driver, lblCompany, 120);
            CustomFunctions.MouseOver(driver, lblCompany, 120);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebElement ele = (WebElement)driver.FindElement(lblCompany);
            js.ExecuteScript("arguments[0].click()", ele);

            Thread.Sleep(15000);

            driver.SwitchTo().Frame("asset-0FK6e000000J8LsGAK");
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblCompanyName, 120);
            if (driver.FindElement(lblCompanyName).Text.Contains(company))
            {
                result = true;
            }

            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
            return result;
        }

    }
}

