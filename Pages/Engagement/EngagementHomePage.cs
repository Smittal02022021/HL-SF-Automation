using OpenQA.Selenium;
using SF_Automation.UtilityFunctions;
using System;
using System.Threading;

namespace SF_Automation.Pages
{
    class EngagementHomePage : BaseClass
    {
        By lnkEngagements = By.CssSelector("a[title*='Engagements Tab']");
        By linkAdvancedSearch = By.CssSelector("span[id='searchLabel']");
        By btnSearch = By.CssSelector("input[name*='btnSearch']");
        By tblResults = By.CssSelector("table[id*='pbtEngagements']");
        By matchedResult = By.XPath("//*[contains(@id,':pbtEngagements:0:j_id57')]/a");
        By comboJobType = By.CssSelector("select[name*='jobTypeSearch']");
        By comboLOB = By.CssSelector("select[name*='obSearch']");
        By comboStage = By.CssSelector("select[name*='stageSearch']");
        By lnkEngageManager = By.XPath("//a[contains(text(),'Engagement Manager')]");
        By titleEngageManager = By.CssSelector("label[id*='PageTitle']");
        By txtEngageName = By.CssSelector("input[name*='nameSearch']");
        By txtEngageNum = By.CssSelector("input[name*='NumberSearch']");
        By txtErrorMessage = By.CssSelector("span[id*=':theError']");

        //Lightning  
        By btnEngNum = By.XPath("//button[@aria-label='Search']");
        By btnEngNumNotBlank = By.XPath("//section/header/div[2]/div[2]/div/button");
        By txtEngNumLightning = By.XPath("//input[@placeholder='Search Engagements and more...']");
        By lnkEngLightning = By.XPath("//search_dialog-instant-result-item[1]/div[1]/div[2]/div/lightning-formatted-rich-text/span");
        By valEngName = By.XPath("//h1/slot/lightning-formatted-text");
        By btnOppNumL = By.XPath("//button[@aria-label='Search']");
        By txtOppNumLCAO = By.XPath("//input[@placeholder='Search Engagements and more...']");
        By imgOppL = By.XPath("//div[1]/records-highlights-icon/force-record-avatar/span/img[@title='Engagement']");

        By searchEngBox = By.XPath("//lightning-input[@class='slds-form-element']");
        By selectEng = By.CssSelector("table[class*='slds-table'] tbody tr th a");

        By linkShowAdvanceSearch = By.CssSelector(".link-options");

        public void ClickEngagementTabAdvanceSearch()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagements);
            driver.FindElement(lnkEngagements).Click();
            driver.FindElement(linkShowAdvanceSearch).Click();
        }
        public void SelectEngagement(String engNumber)
        {
            driver.FindElement(searchEngBox).SendKeys(engNumber);
            driver.FindElement(searchEngBox).SendKeys(Keys.Enter);
            Thread.Sleep(4000);
            driver.FindElement(selectEng).Click();
            Thread.Sleep(4000);
        }
        //To Click on Engagement tab
        public void ClickEngagement()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagements);
            driver.FindElement(lnkEngagements).Click();
        }

        //To Search Engagement with Job Type
        public string SearchEngagementWithJobType(string jobType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagements, 120);
            driver.FindElement(lnkEngagements).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkAdvancedSearch);
            driver.FindElement(linkAdvancedSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType);
            driver.FindElement(comboJobType).SendKeys(jobType);
            driver.FindElement(btnSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(5000);
            try
            {
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResult).Click();
                return "Record found";
            }
            catch (Exception)
            {
                return "No record found";
            }
        }

        //To Search Engagement with LOB
        public string SearchEngagementWithLOB(string LOB, string stage)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagements, 90);
            driver.FindElement(lnkEngagements).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkAdvancedSearch);
            driver.FindElement(linkAdvancedSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboLOB);
            driver.FindElement(comboLOB).SendKeys(LOB);
            driver.FindElement(comboStage).SendKeys(stage);
            driver.FindElement(btnSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(6000);
            try
            {
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResult).Click();
                return "Record found";
            }
            catch (Exception)
            {
                return "No record found";
            }
        }

        //To click on Engagement Manager Link
        public string ClickEngageManager()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngageManager, 100);
            driver.FindElement(lnkEngageManager).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleEngageManager, 70);
            string title = driver.FindElement(titleEngageManager).Text;
            return title;
        }

        //To Search with Engagement Name
        public string SearchEngagementWithName(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagements, 150);
            driver.FindElement(lnkEngagements).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngageName, 100);
            driver.FindElement(txtEngageName).SendKeys(name);
            driver.FindElement(btnSearch).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
                Thread.Sleep(6000);
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResult).Click();
                Thread.Sleep(5000);
                return "Record found";
            }

            catch (Exception)
            {
                driver.Navigate().Refresh();
                WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagements, 110);
                driver.FindElement(lnkEngagements).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, txtEngageName, 80);
                driver.FindElement(txtEngageName).SendKeys(name);
                driver.FindElement(btnSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
                Thread.Sleep(6000);
                driver.FindElement(matchedResult).Click();
                return "Error page found or no record found";
            }
        }

        //To Search with Engagement Name
        public string SearchEngagementWithNumber(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagements, 150);
            driver.FindElement(lnkEngagements).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngageNum, 80);
            driver.FindElement(txtEngageNum).SendKeys(name);
            driver.FindElement(btnSearch).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
                Thread.Sleep(6000);
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResult).Click();
                Thread.Sleep(5000);
                return "Record found";
            }

            catch (Exception)
            {
                driver.Navigate().Refresh();
                WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagements, 110);
                driver.FindElement(lnkEngagements).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, txtEngageNum, 80);
                driver.FindElement(txtEngageNum).SendKeys(name);
                driver.FindElement(btnSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
                Thread.Sleep(6000);
                driver.FindElement(matchedResult).Click();
                return "Error page found or no record found";
            }
        }

        public void HandlePopUp(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagements, 150);
            driver.FindElement(lnkEngagements).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngageName, 80);
            driver.FindElement(txtEngageName).SendKeys(name);
            driver.FindElement(btnSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(6000);
            try
            {
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResult).Click();
                Thread.Sleep(10000);
            }
            catch (Exception)
            {

            }
            //InputSimulator sim = new InputSimulator();
            //sim.Keyboard.KeyPress(VirtualKeyCode.TAB);
            //sim.Keyboard.KeyPress(VirtualKeyCode.TAB);
            //sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            Thread.Sleep(5000);
        }

        //To Search with Engagement number on Lightning 
        public string SearchEngagementWithNumberOnLightning(string name, string jobType)
        {
            Thread.Sleep(6000);
            if (jobType.Equals("Sellside") || jobType.Equals("Buyside") || jobType.Equals("Debt Capital Markets"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEngNum, 150);
                driver.FindElement(btnEngNum).Click();
                Thread.Sleep(5000);
                WebDriverWaits.WaitUntilEleVisible(driver, txtEngNumLightning, 150);
                driver.FindElement(txtEngNumLightning).Clear();
                driver.FindElement(txtEngNumLightning).SendKeys(name);
                Thread.Sleep(4000);
                WebDriverWaits.WaitUntilEleVisible(driver, lnkEngLightning, 480);
                driver.FindElement(lnkEngLightning).Click();
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEngNumNotBlank, 180);
                driver.FindElement(btnEngNumNotBlank).Click();
                Thread.Sleep(5000);
                driver.FindElement(txtEngNumLightning).Clear();
                driver.FindElement(txtEngNumLightning).SendKeys(name);
                Thread.Sleep(5000);
                WebDriverWaits.WaitUntilEleVisible(driver, lnkEngLightning, 380);
                driver.FindElement(lnkEngLightning).Click();
            }
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valEngName, 350);
            string engName = driver.FindElement(valEngName).Text;
            return engName;
        }

        //To Search Opportunity with Opportunity Name in Lighting
        public void SearchMyEngInLightning(string value)
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnOppNumL, 250);
            driver.FindElement(btnOppNumL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtOppNumLCAO, 100);
            driver.FindElement(txtOppNumLCAO).SendKeys(value);
            Thread.Sleep(6000);
            driver.FindElement(imgOppL).Click();
            Thread.Sleep(2000);

        }
    }
}


