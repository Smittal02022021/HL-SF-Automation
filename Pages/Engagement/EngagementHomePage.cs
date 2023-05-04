using OpenQA.Selenium;
using SF_Automation.UtilityFunctions;
using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

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
        By btnNavigationMenu = By.XPath("//button[@title='Show Navigation Menu']");
        By tagEngagements = By.XPath("//div/ul/li[3]/div/a/span[2]/span");        
        By lnkRecentlyViewed = By.XPath("//h1/span[2]");
        By tblEngagements = By.XPath("//div[1]/div/div/table");
        By btnRecentlyViewed = By.XPath("//div/div/div[2]/div/button");
        By valRecentlyViewed = By.XPath("//div[2]/div/div/div[1]/div/div/div/div/div[1]/div/ul/li/a/span");
        By txtSearchEng = By.XPath("//input[@name='Engagement__c-search-input']");
        By btnRefresh = By.XPath("//button[@title='Refresh']");
        By valSearchedEng = By.XPath("//table/tbody/tr/td[2]/span/span");
        By valSearchedEngName = By.XPath("//table/tbody/tr/th/span/a");
        By titleEngDetails = By.XPath("//forcegenerated-adg-rollup_component___force-generated__flexipage_-record-page___-engagement_-record_-page_-h-l-banker_-c-f___-engagement__c___-v-i-e-w/forcegenerated-flexipage_engagement_record_page_hlbanker_cf_engagement__c__view_js/record_flexipage-desktop-record-page-decorator/div[1]/records-record-layout-event-broker/slot/slot/flexipage-record-home-template-desktop2/div/div[2]/div[1]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/slot/slot/flexipage-tab2[1]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/lightning-tab-bar/ul/li[1]/a");
        By tabEngL = By.XPath("//table/tbody/tr/th/span/a");
        

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
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngageName,100);
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
           
            catch(Exception)
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
            if (jobType.Equals("Sellside")|| jobType.Equals("Buyside") || jobType.Equals("Debt Capital Markets"))
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

        public string ValidateEngUnderHLBanker()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNavigationMenu, 350);
            driver.FindElement(btnNavigationMenu).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tagEngagements, 350);
            string value = driver.FindElement(tagEngagements).Text;
            return value;
        }

        //Validate Recently Viewed is displayed upon selecting Engagements
        public string ValidateRecentViewedUponSelectingEngagements()
        {
            driver.FindElement(tagEngagements).Click();
            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRecentlyViewed, 350);
            string value = driver.FindElement(lnkRecentlyViewed).Text;
            return value;
        }

        //Validate if recently viewed Engagements are displayed or not
        public string ValidateIfRecentlyViewedEngagementsAreDisplayed()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tblEngagements, 100);
            string value = driver.FindElement(tblEngagements).Displayed.ToString();
            return value;
        }
        //Validate Recently Viewed values
        public bool ValidateRecentlyViewedValues()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnRecentlyViewed, 350);
            driver.FindElement(btnRecentlyViewed).Click();
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(valRecentlyViewed);
            Thread.Sleep(3000);
            string[] actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "All Active CF Engagements", "All Active Engagements","All Engagements", "FAS Engagements", "My Active Engagements","My Closed Deal Process to Review", "My Closed Engagements", "My Dead/Hold Engagements",  "Recently Viewed", "(Pinned list)" };
            bool isTrue = true;

            Console.WriteLine(actualNamesAndDesc[5]);
            Console.WriteLine(actualNamesAndDesc[6]);

            if (expectedValues.Length != actualNamesAndDesc.Length)
            {
                return !isTrue;
            }
            for (int recType = 0; recType < expectedValues.Length; recType++)
            {
                if (!expectedValues[recType].Equals(actualNamesAndDesc[recType]))
                {
                    isTrue = false;
                    break;
                }
            }

            driver.FindElement(lnkRecentlyViewed).Click();
            return isTrue;
        }

        //Validate if Search functionality is available
        public string ValidateSearchFunctionalityIsAvailable()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchEng, 150);
            string name = driver.FindElement(txtSearchEng).Displayed.ToString();
            return name;
        }

        //Validate if Search functionality is working as expected
        public string ValidateSearchFunctionalityOfEngagements(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchEng, 150);
            driver.FindElement(txtSearchEng).SendKeys(name);
            Thread.Sleep(5000);
            driver.FindElement(btnRefresh).Click();
            Thread.Sleep(6000);
            string opp = driver.FindElement(valSearchedEng).Text;
            return opp;
        }

        //Validate Engagement details page upon clicking Engagement Name
        public string ClickEngNumAndValidateThePage()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, titleEngDetails, 170);
            string title = driver.FindElement(titleEngDetails).Text;
            return title;
        }

        public void ClickEngNumber()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngL, 160);
            driver.FindElement(tabEngL).Click();
            Thread.Sleep(3000);
        }

    }
}


