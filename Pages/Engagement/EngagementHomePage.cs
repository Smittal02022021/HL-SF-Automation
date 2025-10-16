using OpenQA.Selenium;
using SF_Automation.UtilityFunctions;
using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Net.PeerToPeer;
using System.Security.Cryptography;
using System.Linq.Expressions;

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
        By btnEngNum = By.XPath("//button[contains(@aria-label,'Search')]");
        By btnEngNumNotBlank = By.XPath("//header/div[2]/div[2]/div/div/button");
        By txtEngNumLightning = By.XPath("//input[contains(@placeholder,'Search Engagements and more...')]");
        //By txtEngNumLightningL = By.XPath("//input[contains(@placeholder,'Search...')]");
        By txtEngNumLightningL = By.XPath("//div[1]/div/div[1]/lightning-input//div[1]//input");
        By lnkEngLightning = By.XPath("//span/img[@title='Engagement']");
        By valEngName = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Engagement__c.Name']//lightning-formatted-text");
        By btnOppNumL = By.XPath("//button[@aria-label='Search']");
        By txtOppNumLCAO = By.XPath("//input[@placeholder='Search Engagements and more...']");
        By imgOppL = By.XPath("//div[1]/records-highlights-icon/force-record-avatar/span/img[@title='Engagement']");

        By searchEngBox = By.XPath("//lightning-input[@class='slds-form-element']");
        By selectEng = By.CssSelector("table[class*='slds-table'] tbody tr th a");
        By txtEngagementName = By.CssSelector("input[name*='nameSearch']");
        By btnNavigationMenu = By.XPath("//button[@title='Show Navigation Menu']");
        By tagEngagements = By.XPath("//a[@data-label='Engagements']");
        By lnkRecentlyViewed = By.XPath("//h1/span[2]");
        By tblEngagements = By.XPath("//section//table[@aria-label='Recently Viewed']");
        By btnRecentlyViewed = By.XPath("//button[@title='Select a List View: Engagements']");
        By valRecentlyViewed = By.XPath("//div[2]/div/div/div[1]/div/div/div/div/div[1]/div/ul/li/a/span");
        By txtSearchEng = By.XPath("//input[@name='Engagement-search-input']");
        By btnRefresh = By.XPath("//button[@name='refreshButton']");
        By btnSearchL = By.XPath("//button[@aria-label='Search']");
        By valSearchedEng = By.XPath("//tr/th/span//lst-output-lookup/force-lookup/div");
        By valSearchedEng1 = By.XPath("//tr/th//div/a");
        By valSearchedEngName = By.XPath("//table/tbody/tr[1]/th/span//a");
        By tabEngL = By.XPath("//table/tbody/tr/th/span//a");
        By tabEngagementL = By.XPath("//a/span[text()='Engagements']");
        By btnCloseTab = By.XPath("//ul[2]/li[2]/div[2]/button");
        By titleEngDetailsL = By.XPath("//flexipage-tab2[1]/slot//lightning-tab-bar/ul/li[1]/a");
        ////button[contains(@title,'Close 21132025131338 | Opportunity')]

        By linkShowAdvanceSearch = By.CssSelector(".link-options");
        By btnEngsearchL = By.XPath("//button[@aria-label='Search']");
        By txtEngsearchL = By.XPath("//input[contains(@placeholder,'Search Engagements')]");
        By imgEng = By.XPath("//div[1]/records-highlights-icon/force-record-avatar/span/img[@title='Engagement']");
        By comboIndustryType = By.CssSelector("select[name*='industryGroupSearch']");
        By tabEngagement = By.CssSelector("a[title*='Engagements Tab']");
        By txtSearchBox = By.XPath("//input[@placeholder='Search this list...']");
        By eleItem = By.XPath("//table/tbody//td[5]/span//span");
        By iconClearSearch = By.XPath("//button[@data-element-id='searchClear']");
        By inputAdminGlobalSearchL = By.XPath("//input[contains(@placeholder,'and more...')]");
        By inputGlobalSearchL = By.XPath("//button[@aria-label='Search']");
        By btnEngNumL = By.XPath("//button[@aria-label='Search']");
        By inputEngNumL = By.XPath("//button[contains(@aria-label,'Search')]");
        By txtEngNumLCAO = By.XPath("//input[@placeholder='Search...']");
        By imgEngL = By.XPath("//div[1]/records-highlights-icon/force-record-avatar/span/img[@title='Engagement']");
        By txtEngL = By.XPath("//input[@placeholder='Search...']");
        By columnJobTypeL = By.XPath("//div[contains(@aria-label,'Engagements')]//table//tbody/tr[1]//td[4]/span/span");
        By searchOppBox = By.XPath("//lightning-input[@class='slds-form-element']");
        private By _lnkSearchedEngL(string name)
        {
            return By.XPath($"//h2//a[text()='Engagements']/../../../../..//table//tbody//th[1]//a[@title='{name}']"); //div[@aria-label='Engagements||List View']//table//tbody//th[1]//a[@title='{name}']");
        }

        public string GlobalSearchEngagementInLightningView(string engName)
        {
            Thread.Sleep(6000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconClearSearch, 5);
                driver.FindElement(iconClearSearch).Click();
            }
            catch { }
            WebDriverWaits.WaitUntilEleVisible(driver, inputGlobalSearchL, 10);
            driver.FindElement(inputGlobalSearchL).Click();
            Thread.Sleep(4000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtEngL, 10);
                driver.FindElement(txtEngL).SendKeys(engName);

            }
            catch
            {
                WebDriverWaits.WaitUntilEleVisible(driver, inputAdminGlobalSearchL, 10);
                driver.FindElement(inputAdminGlobalSearchL).SendKeys(engName);
            }

            try
            {
                driver.FindElement(txtEngL).SendKeys(Keys.Enter);
                Thread.Sleep(6000);
                WebDriverWaits.WaitUntilEleVisible(driver, _lnkSearchedEngL(engName), 20);
                driver.FindElement(_lnkSearchedEngL(engName)).Click();
                Thread.Sleep(8000);
                return "Record found";
            }
            catch { return "No record found"; }

        }
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
            Thread.Sleep(5000);
            try
            {
                if (jobType.Equals("Sellside") || jobType.Equals("Buyside") || jobType.Equals("Debt Capital Markets") || jobType.Equals("Equity Capital Markets") || jobType.Equals("FA - Portfolio-Valuation") || jobType.Equals("FA - Portfolio-Advis/Consulting") || jobType.Equals("Creditor Advisors") || jobType.Equals("Debtor Advisors") || jobType.Equals("Discretionary Advisory") || jobType.Equals("General Financial Advisory") || jobType.Equals("DRC - Exp Wit-Litigation") || jobType.Equals("TAS - Due Diligence-Buyside"))
                {
                    Thread.Sleep(5000);
                    driver.FindElement(btnEngNum).Click();
                    Thread.Sleep(7000);
                    driver.FindElement(txtEngNumLightning).Clear();
                    driver.FindElement(txtEngNumLightning).SendKeys(name);
                    Thread.Sleep(5000);
                    driver.FindElement(txtEngNumLightning).Clear();
                    driver.FindElement(txtEngNumLightning).SendKeys(name);
                    Thread.Sleep(7000);
                    WebDriverWaits.WaitUntilEleVisible(driver, lnkEngLightning, 310);
                    driver.FindElement(lnkEngLightning).Click();
                }
                else
                {
                    driver.Navigate().Refresh();
                    Thread.Sleep(6000);
                    driver.FindElement(btnEngNum).Click();
                    Thread.Sleep(5000);
                    driver.FindElement(txtEngNumLightningL).Clear();
                    driver.FindElement(txtEngNumLightningL).SendKeys(name);
                    Thread.Sleep(5000);
                    driver.FindElement(txtEngNumLightningL).Clear();
                    driver.FindElement(txtEngNumLightningL).SendKeys(name);
                    Thread.Sleep(7000);
                    // WebDriverWaits.WaitUntilEleVisible(driver, lnkEngLightning, 380);
                    driver.FindElement(lnkEngLightning).Click();
                }
            }
            catch (Exception)
            {
                driver.Navigate().Refresh();
                Thread.Sleep(6000);
                driver.FindElement(btnEngNum).Click();
                Thread.Sleep(5000);
                driver.FindElement(txtEngNumLightningL).Clear();
                driver.FindElement(txtEngNumLightningL).SendKeys(name);
                Thread.Sleep(5000);
                driver.FindElement(txtEngNumLightningL).Clear();
                driver.FindElement(txtEngNumLightningL).SendKeys(name);
                Thread.Sleep(7000);
                // WebDriverWaits.WaitUntilEleVisible(driver, lnkEngLightning, 380);
                driver.FindElement(lnkEngLightning).Click();
            }
            Thread.Sleep(10000);
            //WebDriverWaits.WaitUntilEleVisible(driver, valEngName, 350);
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
        public string SearchEngagement(string engName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagements, 20);
            Thread.Sleep(3000);
            driver.FindElement(lnkEngagements).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngagementName);
            driver.FindElement(txtEngagementName).SendKeys(engName);
            driver.FindElement(btnSearch).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 120);
                Thread.Sleep(6000);
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResult).Click();
                return "Record found";
            }
            catch (Exception)
            {
                driver.Navigate().Refresh();
                WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagements, 150);
                Thread.Sleep(3000);
                driver.FindElement(lnkEngagements).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, txtEngagementName);
                driver.FindElement(txtEngagementName).SendKeys(engName);
                driver.FindElement(btnSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 120);
                Thread.Sleep(6000);
                try
                {
                    string result = driver.FindElement(matchedResult).Displayed.ToString();
                    Console.WriteLine("result");
                    driver.FindElement(matchedResult).Click();
                    return "Record found";
                }
                catch
                {
                    return "No record found";
                }
            }
        }
        //To Search Eng with Eng Name in Lighting
        public void SearchMyEngagementInLightning(string value, string user)
        {
            Thread.Sleep(6000);
            if (user.Equals("James Craven"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEngsearchL, 20);
                driver.FindElement(btnEngsearchL).Click();
                Thread.Sleep(4000);
            }
            else
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEngsearchL, 20);
                driver.FindElement(btnEngsearchL).Click();
                Thread.Sleep(4000);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngsearchL, 10);
            driver.FindElement(txtEngsearchL).SendKeys(value);
            Thread.Sleep(6000);
            driver.FindElement(imgEng).Click();
            Thread.Sleep(2000);
        }
        public string SearchEngagementsWithIndustryType(string industryType)
        {
            By matchedEngagement = By.XPath($"//table[contains(@id,'myEngagements')]//tbody//td//span[contains(text(),'{industryType}')]");

            WebDriverWaits.WaitUntilEleVisible(driver, comboIndustryType);
            driver.FindElement(comboIndustryType).SendKeys(industryType);
            driver.FindElement(btnSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(6000);
            try
            {
                string result = driver.FindElement(matchedEngagement).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                return "Record found";
            }
            catch (Exception)
            {
                return "No record found";
            }
        }
        public string SearchEngagemenstWithJobType(string jobType)
        {
            By matchedOpportunity = By.XPath($"//table[contains(@id,'myEngagements')]//tbody//td//span[contains(text(),'{jobType}')]");
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType);
            driver.FindElement(comboJobType).SendKeys(jobType);
            driver.FindElement(btnSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(6000);
            try
            {
                string result = driver.FindElement(matchedOpportunity).Displayed.ToString();
                return "Record found";
            }
            catch (Exception)
            {
                return "No record found";
            }
        }
        public bool SearchRecentEngagementLV(string oppName)
        {
            driver.FindElement(txtSearchBox).SendKeys(oppName);
            driver.FindElement(txtSearchBox).SendKeys(Keys.Enter);
            Thread.Sleep(4000);
            try
            {
                return driver.FindElement(eleItem).Displayed;
            }
            catch (Exception) { return false; }
        }
        public string GetSearchedEngJobType()
        {
            return driver.FindElement(eleItem).Text;
        }
        public void ClickEngagementTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngagement, 80);
            driver.FindElement(tabEngagement).Click();
        }

        public string ValidateEngUnderHLBanker()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNavigationMenu, 350);
            driver.FindElement(btnNavigationMenu).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tagEngagements, 370);
            string value = driver.FindElement(tagEngagements).Text;
            return value;
        }

        public void SelectEngUnderHLBanker()
        {
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNavigationMenu, 350);
            driver.FindElement(btnNavigationMenu).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tagEngagements, 370);
            driver.FindElement(tagEngagements).Click();
        }

        public void SelectDirectEngUnderHLBanker()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNavigationMenu, 350);
            driver.FindElement(btnNavigationMenu).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, tagEngagements, 370);
            driver.FindElement(tagEngagements).Click();
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
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnRecentlyViewed, 350);
            driver.FindElement(btnRecentlyViewed).Click();
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(valRecentlyViewed);
            Thread.Sleep(3000);
            string[] actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "Recently Viewed", "(Pinned list)", "All Engagements", "FAS Engagements", "FR Engagements - All Active", "FR Engagements - All Holds", "FVA Engagements - All Active", "My Active Engagements", "My Closed Deal Process to Review", "My Closed Engagements", "My Dead Engagements", "My FY24 Closed Deals", "My Hold Engagements" };
            bool isTrue = true;
            Console.WriteLine("1st:" + actualNamesAndDesc[0]);
            Console.WriteLine("1st:" + actualNamesAndDesc[1]);
            Console.WriteLine(actualNamesAndDesc[2]);
            Console.WriteLine(actualNamesAndDesc[3]);
            Console.WriteLine(actualNamesAndDesc[4]);
            Console.WriteLine(actualNamesAndDesc[5]);
            Console.WriteLine(actualNamesAndDesc[6]);
            Console.WriteLine(actualNamesAndDesc[7]);
            Console.WriteLine(actualNamesAndDesc[8]);
            Console.WriteLine(actualNamesAndDesc[9]);
            Console.WriteLine(actualNamesAndDesc[10]);
            Console.WriteLine(actualNamesAndDesc[11]);


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
            WebDriverWaits.WaitUntilEleVisible(driver, valSearchedEngName, 190);
            string opp = driver.FindElement(valSearchedEngName).Text;
            return opp;

        }
        //Validate if Search functionality is working as expected
        public string ValidateSearchFunctionalityOfEngagement(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchEng, 150);
            driver.FindElement(txtSearchEng).SendKeys(name);
            Thread.Sleep(5000);
            driver.FindElement(btnRefresh).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnRefresh).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valSearchedEng1, 190);
            string opp = driver.FindElement(valSearchedEng1).GetAttribute("title");
            driver.FindElement(valSearchedEng1).Click();
            return opp;

        }

        //Validate if Search functionality is working as expected
        public string ValidateSearchFunctionalityOfEngagementsByJobType(string name)
        {
            driver.FindElement(btnNavigationMenu).Click();
            driver.FindElement(tagEngagements).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchEng, 150);
            driver.FindElement(txtSearchEng).Clear();
            driver.FindElement(txtSearchEng).SendKeys(name);
            Thread.Sleep(5000);
            driver.FindElement(btnRefresh).Click();
            Thread.Sleep(6000);
            string opp = driver.FindElement(valSearchedEngName).Text;
            driver.FindElement(valSearchedEngName).Click();
            return opp;
        }
        //Validate if Search functionality is working as expected
        public void ValidateSearchFunctionalityOfEngagementsForAdmin(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchEng, 150);
            driver.FindElement(txtSearchEng).SendKeys(name);
            Thread.Sleep(5000);
            driver.FindElement(btnRefresh).Click();
            Thread.Sleep(3000);
        }

        //Validate Engagement details page upon clicking Engagement Name
        public string ClickEngNumAndValidateThePage()
        {
            //driver.FindElement(btnCloseTab).Click();
            //Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, titleEngDetailsL, 170);
            string title = driver.FindElement(titleEngDetailsL).Text;
            return title;
        }

        //Validate Engagement details page upon clicking Engagement Name
        public string ValidateEngDetailsPage()
        {
            driver.FindElement(btnCloseTab).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, titleEngDetailsL, 170);
            string title = driver.FindElement(titleEngDetailsL).Text;
            return title;
        }

        public void ClickEngNumber()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngL, 160);
            driver.FindElement(tabEngL).Click();
            Thread.Sleep(3000);
        }

        public void ClickEngTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCloseTab, 190);
            driver.FindElement(btnCloseTab).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngagementL, 180);
            driver.FindElement(tabEngagementL).Click();
            Thread.Sleep(3000);
        }
        public string SearchEngagementInLightningView(string value)
        {
            Thread.Sleep(6000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconClearSearch, 5);
                driver.FindElement(iconClearSearch).Click();
            }
            catch { }
            WebDriverWaits.WaitUntilEleVisible(driver, btnEngNumL, 20);
            driver.FindElement(btnEngNumL).Click();
            Thread.Sleep(4000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtEngNumLCAO, 20);
                driver.FindElement(txtEngNumLCAO).SendKeys(value);
            }
            catch
            {
                WebDriverWaits.WaitUntilEleVisible(driver, inputAdminGlobalSearchL, 10);
                driver.FindElement(inputAdminGlobalSearchL).SendKeys(value);
            }

            Thread.Sleep(6000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, imgEngL, 20);
                driver.FindElement(imgEngL).Click();
                Thread.Sleep(8000);
                return "Record found";
            }
            catch { return "No record found"; }
        }
        public string UpdateEngAndSearchLV(string engName)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconClearSearch, 5);
                driver.FindElement(iconClearSearch).Click();
            }
            catch { }
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEngNumL, 20);
                driver.FindElement(txtEngNumLCAO).Clear();
                driver.FindElement(txtEngNumLCAO).SendKeys(engName);
                Thread.Sleep(6000);
            }
            catch
            {
                WebDriverWaits.WaitUntilEleVisible(driver, inputAdminGlobalSearchL, 5);
                driver.FindElement(inputAdminGlobalSearchL).Click();
                driver.FindElement(inputAdminGlobalSearchL).Clear();
                driver.FindElement(inputAdminGlobalSearchL).SendKeys(engName);
                Thread.Sleep(6000);
            }
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, imgEngL, 20);
                driver.FindElement(imgEngL).Click();
                Thread.Sleep(8000);
                return "Record found";
            }
            catch { return "No record found"; }
        }
        public bool IsEngagementWithJobTypeFoundLV(string engName, string jobType)
        {
            Thread.Sleep(6000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, inputEngNumL, 5);
                driver.FindElement(inputEngNumL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, iconClearSearch, 5);
                driver.FindElement(iconClearSearch).Click();
            }
            catch { }
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtEngNumLCAO, 5);
                //driver.FindElement(inputEngNumL).Click();
                //driver.FindElement(inputEngNumL).Clear();
                driver.FindElement(txtEngNumLCAO).SendKeys(engName);
                driver.FindElement(txtEngNumLCAO).SendKeys(Keys.Enter);
            }
            catch
            {
                WebDriverWaits.WaitUntilEleVisible(driver, inputAdminGlobalSearchL, 5);
                driver.FindElement(inputAdminGlobalSearchL).Click();
                driver.FindElement(inputAdminGlobalSearchL).Clear();
                driver.FindElement(inputAdminGlobalSearchL).SendKeys(engName);
                driver.FindElement(txtEngNumLCAO).SendKeys(Keys.Enter);
            }
            Thread.Sleep(6000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, columnJobTypeL, 10);
                string txtJobType = driver.FindElement(columnJobTypeL).Text;
                if (txtJobType == jobType)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch { return false; }
        }
    }
}