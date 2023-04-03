using OpenQA.Selenium;
using SF_Automation.UtilityFunctions;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages.Companies
{
    class LV_CompanyDetailsPage : BaseClass
    {
        By lblSponsorCoverage = By.XPath("(//h2[@id='header'])[1]/span");
        By btnAddActivity = By.XPath("//button[text()='Add Activity']");
        
        public void NavigateToAParticularTab(string tabName)
        {
            Thread.Sleep(5000);
            IList<IWebElement> elements = driver.FindElements(By.XPath("//ul[@role='tablist']/li"));
            int size = elements.Count;

            for(int items = 1;items <= size;items++)
            {
                By linkTab = By.XPath($"//ul[@role='tablist']/li[{items}]/a");

                WebDriverWaits.WaitUntilEleVisible(driver,linkTab,120);
                string tab = driver.FindElement(linkTab).Text;

                if(tab == tabName)
                {
                    driver.FindElement(linkTab).Click();
                    Thread.Sleep(3000);
                    break;
                }
            }
        }

        public bool VerifyCoverageTabIsOpened()
        {
            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver,lblSponsorCoverage,120);
            string heading2 = driver.FindElement(lblSponsorCoverage).Text;
            if(heading2.Contains("Sponsor Coverage"))
            {
                result = true;
            }
            return result;
        }

        public bool VerifyLoggedInUserHasIndustryCoverageForACompany(string userName)
        {
            bool result = false;

            IList<IWebElement> elements = driver.FindElements(By.XPath("(//table[contains(@class,'slds-table slds-table--bordered')])[2]/tbody/tr"));
            int size = elements.Count;

            for(int rows = 1;rows <= size;rows++)
            {
                By userNameLink = By.XPath($"(//table[contains(@class,'slds-table slds-table--bordered')])[2]/tbody/tr[{rows}]/td[2]/div/lightning-formatted-rich-text/span/a");

                WebDriverWaits.WaitUntilEleVisible(driver,userNameLink,120);
                string userText = driver.FindElement(userNameLink).Text;

                if(userText == userName)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public bool VerifyActivityTabIsOpened()
        {
            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver,btnAddActivity,120);
            if(driver.FindElement(btnAddActivity).Displayed)
            {
                result = true;
            }
            return result;
        }
    }
}

