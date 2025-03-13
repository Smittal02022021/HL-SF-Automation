using OpenQA.Selenium;
using SF_Automation.UtilityFunctions;
using System.Threading;

namespace SF_Automation.Pages
{
    class CoverageSectorDependenciesHomePage : BaseClass
    {
        By btnNew = By.XPath("//input[@title='New']");
        By linkCoverageSectorDependencyName = By.XPath("//th[contains(text(),'HL Sector ID')]/following::tr/th/a");

        public void ClickNewCoverageDependenciesButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNew, 120);
            driver.FindElement(btnNew).Click();
            Thread.Sleep(2000);
        }

        public void NavigateToCoverageSectorDependencyDetailPage(string covSectorDependencyName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCoverageSectorDependencyName, 120);
            driver.FindElement(linkCoverageSectorDependencyName).Click();
            Thread.Sleep(2000);
        }
    }
}
