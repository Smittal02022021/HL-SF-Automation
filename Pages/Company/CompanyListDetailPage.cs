using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System.Threading;

namespace SF_Automation.Pages.Company
{
    class CompanyListDetailPage : BaseClass
    {
        By valCompanyName = By.CssSelector("div[id='Name_ileinner']");
        By btnDeleteCompanyList = By.CssSelector("td[id='topButtonRow'] > input[name='del']");

        
        public string GetCompanyName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyName, 60);
            string companyName = driver.FindElement(valCompanyName).Text;
            return companyName;
        }

        public void DeleteCompanyList()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCompanyList, 120);
            driver.FindElement(btnDeleteCompanyList).Click();

            Thread.Sleep(1000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(1000);
        }
    }
}
