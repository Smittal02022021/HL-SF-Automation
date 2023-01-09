using OpenQA.Selenium;
using SF_Automation.UtilityFunctions;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages.HomePage
{
    class LVHomePage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();

        By btnMenu = By.XPath("//button[@class='slds-button slds-show']");
        By lblHLBanker = By.XPath("//span[@title='HL Banker']");
        By btnMenu1 = By.XPath("(//span[@title='HL Banker']/../preceding::button)[8]");
        By txtSearchItems = By.XPath("//input[@placeholder='Search apps and items...']");
        By itemExpenseRequestLWC = By.XPath("(//h3[text()='Items']/following::div/*/span/p/b[text()='Expense Request(LWC)'])[1]");
        By linkLogout = By.XPath("//a[contains(text(),'Log out')]");
        By btnMainSearch = By.XPath("//button[@aria-label='Search']");
        By txtMainSearch = By.XPath("//input[@placeholder='Search...']");
        By userImage = By.XPath("(//span[@data-aura-class='uiImage'])[1]");
        By linkLogOut = By.XPath("//a[text()='Log Out']");
        By appLauncher = By.CssSelector("button[class*='slds-icon-waffle_container'] div.slds-icon-waffle");
        By appHeader = By.CssSelector("div.slds-context-bar__label-action .slds-truncate");
        By menuNavigation = By.CssSelector("button[title = 'Show Navigation Menu']");
        By avaiableModules = By.XPath("//div[@id='navMenuList']/div/ul/li/div/*/*/span");
        By lblTearsheetHeading = By.XPath("//h1");

        string dir = @"C:\Users\SMittal0207\source\repos\SF_Automation\TestData\";

        private By _appInAppLauncher(string appName)
        {
            return By.XPath($"//h3[text()='Apps']/following::div/*/span/p/b[text()='{appName}']");
        }

        public void SelectModule(string moduleName)
        {
            Thread.Sleep(2000);
            driver.FindElement(menuNavigation).Click();
            IList<IWebElement> modules = driver.FindElements(avaiableModules);
            for (int module = 0; module <= modules.Count; module++)
            {
                string moduleValue = modules[module].Text;
                if (moduleValue.Equals(moduleName))
                {
                    modules[module].Click();
                    Thread.Sleep(4000);
                    break;
                }
            }
        }

        public void ClickAppLauncher()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, appLauncher, 30);
            driver.FindElement(appLauncher).Click();
            Thread.Sleep(3000);
        }

        public void SelectApp(string appName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchItems, 30);
            driver.FindElement(txtSearchItems).SendKeys(appName);
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, _appInAppLauncher(appName), 60);
            driver.FindElement(_appInAppLauncher(appName)).Click();
            Thread.Sleep(3000);
        }

        public string GetAppName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, appHeader, 30);
            return driver.FindElement(appHeader).Text;
        }

        public void SearchText()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnMainSearch, 120);
            driver.FindElement(btnMainSearch).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtMainSearch).SendKeys("dummyText");
            Thread.Sleep(3000);
        }

        public void ClickExpenseRequestMenuButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnMenu, 140);
            driver.FindElement(btnMenu).Click();
            Thread.Sleep(3000);
        }

        public void ClickHomePageMenu()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnMenu1, 140);
            driver.FindElement(btnMenu1).Click();
            Thread.Sleep(3000);
        }

        public void SearchItemExpenseRequestLWC(string item)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchItems, 140);
            driver.FindElement(txtSearchItems).SendKeys(item);
            Thread.Sleep(2000);
            driver.FindElement(itemExpenseRequestLWC).Click();
            Thread.Sleep(3000);
        }

        public void UserLogoutFromSFLightningView()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkLogout, 140);
            driver.FindElement(linkLogout).Click();
            Thread.Sleep(4000);
        }

        public void LogoutFromSFLightningAsApprover()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, userImage, 120);
            driver.FindElement(userImage).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkLogOut, 120);
            driver.FindElement(linkLogOut).Click();
            Thread.Sleep(10000);
        }

        public bool VerifyUserNavigatedToTearsheetSearchCompanyPage()
        {
            bool result = false;

            WebDriverWaits.WaitUntilEleVisible(driver, lblTearsheetHeading, 120);
            Thread.Sleep(5000);

            if(driver.FindElement(lblTearsheetHeading).Text =="Tearsheet")
            {
                result = true;
            }
            return result;
        }

        public bool VerifyThereExistTearsheetAsANavigationalItemOnHLBanker()
        {
            bool result = false;

            WebDriverWaits.WaitUntilEleVisible(driver, menuNavigation, 120);
            driver.FindElement(menuNavigation).Click();
            Thread.Sleep(5000);

            IList<IWebElement> elements = driver.FindElements(By.XPath("//ul[@aria-label='Navigation Menu']/li"));
            int size = elements.Count;

            for (int items=1; items<=size; items++)
            {
                By linkTearsheet = By.XPath($"//ul[@aria-label='Navigation Menu']/li[{items}]/div/a");

                WebDriverWaits.WaitUntilEleVisible(driver, linkTearsheet, 120);
                string itemName = driver.FindElement(linkTearsheet).GetAttribute("data-label");

                if (itemName == "Tearsheet")
                {
                    driver.FindElement(linkTearsheet).Click();
                    Thread.Sleep(3000);

                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}