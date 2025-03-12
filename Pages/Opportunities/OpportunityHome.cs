using OpenQA.Selenium;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SF_Automation.Pages
{
    class OpportunityHomePage : BaseClass
    {
        By lnkOpportunities = By.CssSelector("a[title*='Opportunities Tab']");
        By btnAddOpportunity = By.CssSelector("input[value='Add Opportunity']");
        By comboRecordTypes = By.XPath("//select[@name='p3']/option");
        By tblRecordTypes = By.XPath("//table[@class='infoTable recordTypeInfo']//tbody/tr");
        By linkAdvancedSearch = By.CssSelector("span[id='searchLabel']");
        By txtOpportunityName = By.CssSelector("input[name*='pbAdvancedSearchContent:nameSearch']");
        By btnSearch = By.CssSelector("input[name*='btnSearch']");
        By tblResults = By.CssSelector("table[id*='pbtOpportunities']");
        By matchedResult = By.XPath("//*[contains(@id,':pbtOpportunities:0:j_id59')]/a");
        By btnContinue = By.CssSelector("input[value='Continue']");
        By comboRecordType = By.XPath("//select[@name='p3']");
        By comboJobType = By.CssSelector("select[name*='jobTypeSearch']");
        By comboStage = By.CssSelector("select[name*='stageSearch']");
        By linkOppManager = By.XPath("//a[contains(text(),'Opportunity Manager')]");
        By titleOppManager = By.CssSelector("label[id*='PageTitle']");
        By radioMyOpp = By.CssSelector("input[id*='myOpportunities:pbMain:j_id34:j_id35:1']");
        By lnkOppSelected = By.CssSelector("a[title = 'Opportunities Tab - Selected']");
        By btnReturnToOpp = By.XPath("//div/div[1]/table/tbody/tr/td[2]/span/input[2]");

        By btnNew = By.XPath("//div[@title='New']");
        By btnNext = By.XPath("//span[text()='Next']");
        By valTitle = By.XPath("//h2[contains(text(),'New Opportunity')]");
        By lnkOppL = By.XPath("//table/tbody/tr[1]/th/span/a");
        By btnOppNumL = By.XPath("//button[@aria-label='Search']");
        //By btnOppNumLCAO = By.XPath("//header/div[2]/div[2]/div/button/text()");
        By txtOppNumL = By.XPath("//input[@placeholder='Search...']");
        By txtOppNumLCAO = By.XPath("//input[@placeholder='Search...']");
        By txtOppAndMore = By.XPath("//input[@placeholder='Search Opportunities and more...']");
        By imgOppL = By.XPath("//div[1]/records-highlights-icon/force-record-avatar/span/img[@title='Opportunity']");
        By btnNavigationMenu = By.XPath("//button[@title='Show Navigation Menu']");
        By tagOpportunities = By.XPath("//div/ul/li[6]/div/a/span[2]/span");
        By tagParentProject = By.XPath("//div/ul/li/div/a/span[2]/span[text()='Parent Projects']");
        By tagOpportunitiesCAO = By.XPath("//div/ul/li[4]/div/a/span[2]/span");
        By lnkRecentlyViewed = By.XPath("//h1/span[2]");
        By btnRecentlyViewed = By.XPath("//div/div/div[2]/div/button");
        By valRecentlyViewed = By.XPath("//div[2]/div/div/div[1]/div/div/div/div/div[1]/div/ul/li/a/span");
        By tblOpportunities = By.XPath("//div/table/tbody");
        By txtSearchOpp = By.XPath("//input[@name='Opportunity__c-search-input']");
        By btnRefresh = By.XPath("//button[@title='Refresh']");
        By valSearchedOpp = By.XPath("//table/tbody/tr/td[2]/span//span");
        By valLOBs = By.XPath("//fieldset/div/label/span[2]");
        By searchOppBox = By.XPath("//lightning-input[@class='slds-form-element']");
        By selectOpp = By.CssSelector("table[class*='slds-table'] tbody tr th a");
        By tabOpportunityL = By.XPath("//a/span[text()='Opportunities']");
        By valRec1st = By.XPath("//table/tbody/tr[1]/th/span//a");
        By btnCloseOpp = By.XPath("//ul[2]/li[2]/div[2]/button");
        By tab1stOpportunityL = By.XPath("//div[2]/div/div/ul[2]/li[2]/a/span[2]");
        By linkShowAdvanceSearch = By.CssSelector(".link-options");
        By comboJobTypes = By.CssSelector("select[name*='jobTypeSearch'] option");
        By comboIndustryType = By.CssSelector("select[name*='industryGroupSearch']");
        By comboIndustryTypeOptions = By.CssSelector("select[name*='industryGroupSearch'] option");
        By linkEngManager = By.XPath("//a[contains(text(),'Engagement Manager')]");
        By btnOppsearchL = By.XPath("//button[@aria-label='Search']");
        By txtOppsearchL = By.XPath("//input[contains(@placeholder,'Search Opportunities')]");
        By imgOpp = By.XPath("//div[1]/records-highlights-icon/force-record-avatar/span/img[@title='Opportunity']");
        By txtSearchBox = By.XPath("//input[@placeholder='Search this list...']");
        By eleItem = By.XPath("//table/tbody//td[4]/span/span");
        By lblRecordTypeL = By.XPath("//div[contains(@class,'RecordTypeTopDownOneColumn')]//label//span[contains(@class,'radio--label')]");
        By descRecordTypeL = By.XPath("//div[contains(@class,'RecordTypeTopDownOneColumn')]//label//div[contains(@class,'ItemDescription')]");
        By inputGlobalSearchL = By.XPath("//button[@aria-label='Search']");
        By inputAdminGlobalSearchL = By.XPath("//input[contains(@placeholder,'and more...')]");
        By txtTitle = By.XPath("//div[@class='forceChangeRecordType']//h2");
        By resultTable = By.XPath("//table/tbody//tr//th//a");
        By iconClearSearch = By.XPath("//button[@data-element-id='searchClear']");
        private By _lnkSearchedOppL(string name)
        {
            return By.XPath($"//div[@aria-label='Opportunities||List View']//table//tbody//th[1]//a[@title='{name}']");
        }
        private By _eleOppRecordType(string type)
        {
            return By.XPath($"//div[@class='changeRecordTypeRow']//span[text()='{type}']");
        }

        public void SearchOpportunityInLightning(string value)
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnOppsearchL, 5);
            driver.FindElement(btnOppsearchL).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, txtOppsearchL, 10);
            driver.FindElement(txtOppsearchL).SendKeys(value);
            WebDriverWaits.WaitUntilEleVisible(driver, imgOpp);
            Thread.Sleep(4000);
            driver.FindElement(imgOpp).Click();
            Thread.Sleep(6000);

        }
        public void ClickOpportunityTabAdvanceSearch()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkOpportunities);
            driver.FindElement(lnkOpportunities).Click();
            driver.FindElement(linkShowAdvanceSearch).Click();
        }
        public bool IsJobTypePresentInDropdownHomePage(string jobType)
        {
            bool isFound = false;
            driver.FindElement(comboJobType).Click();
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboJobTypes);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            for (int row = 0; row <= actualValue.Length; row++)
            {
                if (actualValue[row].Contains(jobType))
                {
                    isFound = true;
                    break;
                }
            }
            return isFound;
        }
        public void SelectOpportunity(String OppNumber)
        {
            driver.FindElement(searchOppBox).SendKeys(OppNumber);
            driver.FindElement(searchOppBox).SendKeys(Keys.Enter);
            Thread.Sleep(4000);
            driver.FindElement(selectOpp).Click();
            Thread.Sleep(4000);
        }

        public void ClickOpportunity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkOpportunities);
            driver.FindElement(lnkOpportunities).Click();

            //Calling wait function-- Add Opportunity button        
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddOpportunity);
            driver.FindElement(btnAddOpportunity).Click();
        }
        public void SelectLOBAndClickContinue(string name)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnContinue, 110);
            driver.FindElement(comboRecordType).SendKeys(name);
            WebDriverWaits.WaitUntilEleVisible(driver, btnContinue, 110);
            driver.FindElement(btnContinue).Submit();
        }

        public string SearchOpportunity(string oppName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkOpportunities, 150);
            Thread.Sleep(3000);
            driver.FindElement(lnkOpportunities).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkAdvancedSearch);
            driver.FindElement(linkAdvancedSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityName);
            driver.FindElement(txtOpportunityName).SendKeys(oppName);
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
                WebDriverWaits.WaitUntilEleVisible(driver, lnkOpportunities, 150);
                Thread.Sleep(3000);
                driver.FindElement(lnkOpportunities).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, linkAdvancedSearch);
                driver.FindElement(linkAdvancedSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityName);
                driver.FindElement(txtOpportunityName).SendKeys(oppName);
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


        //To Search Opportunity with Job Type and Stage
        public string SearchOpportunityWithJobTypeAndStge(string jobType, string stage)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkOpportunities, 90);
            driver.FindElement(lnkOpportunities).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkAdvancedSearch);
            driver.FindElement(linkAdvancedSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType);
            driver.FindElement(comboJobType).SendKeys(jobType);
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

        public string UpdateOppAndSearch(string oppName)
        {
            driver.FindElement(txtOpportunityName).Clear();
            driver.FindElement(txtOpportunityName).SendKeys(oppName);
            driver.FindElement(btnSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(6000);
            try
            {
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                return "Record found";
            }
            catch (Exception)
            {
                return "No record found";
            }
        }
        public bool VerifyRecordTypesLV()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblRecordTypeL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "CF", "FR", "FVA", "SC" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public bool VerifyRecordTypesDescLV()
        {
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(descRecordTypeL);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            string[] expectedValues = { "Corporate Finance", "Financial Restructuring", "Financial and Valuation Advisory", "Strategic Consulting" };
            bool isTrue = true;

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
            return isTrue;
        }
        public bool VerifyRecordTypes()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(comboRecordTypes);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            //string[] expectedValue = {"CF", "Conflicts Check", "FAS","FR", "HL Internal Opportunity", "OPP DEL","SC"};
            string[] expectedValue = { "CF", "FR", "FVA", "SC" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public bool VerifyNamesAndDesc()
        {
            IReadOnlyCollection<IWebElement> valNamesAndDesc = driver.FindElements(tblRecordTypes);
            var actualNamesAndDesc = valNamesAndDesc.Select(x => x.Text).ToArray();
            // string[] expectedValues = {"Record Type Name Description", "CF Corporate Finance", "Conflicts Check  ", "FAS Financial Advisory Services", "FR Financial Restructuring", "HL Internal Opportunity This record type is used for ERP \"Recommended VAT Treatment\"", "OPP DEL  ", "SC Strategic Consulting"};
            string[] expectedValues = { "Record Type Name Description", "CF Corporate Finance", "FR Financial Restructuring", "FVA Financial and Valuation Advisory", "SC Strategic Consulting" };
            bool isTrue = true;

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
            return isTrue;
        }
        //To click on Opportunity Manager Link
        public string ClickOppManager()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkOppManager, 60);
            driver.FindElement(linkOppManager).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleOppManager, 60);
            string title = driver.FindElement(titleOppManager).Text;
            return title;
        }
        //To click Engagement Manager Link
        public string ClickEngManager()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, linkEngManager, 60);
            driver.FindElement(linkEngManager).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleOppManager, 60);
            string title = driver.FindElement(titleOppManager).Text;
            return title;

        }

        //To Search Opportunity with Opportunity Name
        public string SearchMyOpportunities(string oppName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkOppSelected, 100);
            driver.FindElement(lnkOppSelected).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, radioMyOpp, 80);
            driver.FindElement(radioMyOpp).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkAdvancedSearch);
            driver.FindElement(linkAdvancedSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityName);
            driver.FindElement(txtOpportunityName).SendKeys(oppName);
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
        //Validate Opportunity is present under HL Banker
        public string ValidateOppUnderHLBanker()
        {

            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNavigationMenu, 250);
            Console.WriteLine("about to click navigation");
            driver.FindElement(btnNavigationMenu).Click();
            Console.WriteLine("Clicked navigation");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tagOpportunities, 350);
            string value = driver.FindElement(tagOpportunities).Text;
            return value;
        }

        //Validate Parent Project is present under HL Banker
        public string ValidateParentProjectUnderHLBanker()
        {

            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNavigationMenu, 250);
            Console.WriteLine("about to click navigation");
            driver.FindElement(btnNavigationMenu).Click();
            Console.WriteLine("Clicked navigation");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tagParentProject, 350);
            string value = driver.FindElement(tagParentProject).Text;
            driver.FindElement(tagParentProject).Click();
            return value;
        }

        //Select Opportunity is present under HL Banker
        public string ClickOppUnderHLBanker()
        {

            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNavigationMenu, 250);
            Console.WriteLine("about to click navigation");
            driver.FindElement(btnNavigationMenu).Click();
            Console.WriteLine("Clicked navigation");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tagOpportunities, 350);
            string value = driver.FindElement(tagOpportunities).Text;
            driver.FindElement(tagOpportunities).Click();
            return value;
        }

        //Select Opportunity is present under HL Banker
        public string ClickOppUnderHLBankerCAO()
        {

            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNavigationMenu, 250);
            Console.WriteLine("about to click navigation");
            driver.FindElement(btnNavigationMenu).Click();
            Console.WriteLine("Clicked navigation");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tagOpportunitiesCAO, 350);
            string value = driver.FindElement(tagOpportunitiesCAO).Text;
            driver.FindElement(tagOpportunitiesCAO).Click();
            return value;
        }

        //Validate Recently Viewed is displayed upon selecting Opportunities
        public string ValidateRecentViewedUponSelectingOpportunities()
        {
            driver.FindElement(tagOpportunities).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRecentlyViewed, 350);
            string value = driver.FindElement(lnkRecentlyViewed).Text;
            return value;
        }

        //Validate if recently viewed opportunities are displayed or not
        public string ValidateIfRecentlyViewedOpportunitiesAreDisplayed()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tblOpportunities, 100);
            string value = driver.FindElement(tblOpportunities).Displayed.ToString();
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
            string[] expectedValues = { "CF Opportunity Confidence", "FR Opportunities - All Active", "FR Opportunities - All Holds", "GCA Migration Tracy View", "Latest Opportunities", "My Active Opportunities", "My Dead Opportunities", "My Engaged Opportunities", "My Hold Opportunities", "Recently Viewed", "(Pinned list)" };
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
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchOpp, 150);
            string name = driver.FindElement(txtSearchOpp).Displayed.ToString();
            return name;
        }

        public void ClickOpportunityTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabOpportunityL, 150);
            driver.FindElement(tabOpportunityL).Click();
        }

        public void Click1stOpportunityTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tab1stOpportunityL, 150);
            driver.FindElement(tab1stOpportunityL).Click();
        }


        //Validate if Search functionality is working as expected
        public string ValidateSearchFunctionalityOfOpportunities(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchOpp, 150);
            driver.FindElement(txtSearchOpp).SendKeys(name);
            Thread.Sleep(5000);
            driver.FindElement(btnRefresh).Click();
            Thread.Sleep(5000);
            string opp = driver.FindElement(valSearchedOpp).GetAttribute("title");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valRec1st, 240);
                driver.FindElement(valRec1st).Click();
                driver.FindElement(btnCloseOpp).Click();

                return opp;
            }
            catch (Exception)
            {
                return opp;
            }
        }

        //Click on New button
        public string ClickNewOppButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNew, 20);
            driver.FindElement(btnNew).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtTitle, 20);
            string title = driver.FindElement(txtTitle).Text;
            return title;
        }
        public string ClickNewButtonAndSelectCFOpp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNew, 350);
            driver.FindElement(btnNew).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnNext, 100);
            driver.FindElement(btnNext).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valTitle, 300);
            string title = driver.FindElement(valTitle).Text;
            return title;
        }
        
        public string ClickNewButtonAndSelectOppRecordTypeLV(string type)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNew, 120);
            driver.FindElement(btnNew).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, _eleOppRecordType(type), 20);
            driver.FindElement(_eleOppRecordType(type)).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnNext, 20);
            driver.FindElement(btnNext).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valTitle, 20);
            string title = driver.FindElement(valTitle).Text;
            return title;
        }

        //Click on New button and validate Choose LOB screen
        public bool ValidateChooseLOBPostClickingNewButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNew, 350);
            driver.FindElement(btnNew).Click();
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(valLOBs);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            //string[] expectedValue = {"CF", "Conflicts Check", "FAS","FR", "HL Internal Opportunity", "OPP DEL","SC"};
            string[] expectedValue = { "CF", "FR", "FVA", "SC" };
            Console.WriteLine(actualValue[1]);
            Console.WriteLine(actualValue[2]);

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //Click Next button
        public string ClickNextAndValidatePage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNext, 350);
            driver.FindElement(btnNext).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valTitle, 350);
            string title = driver.FindElement(valTitle).Text;
            return title;
        }

        //Click Opportunity 
        public void ClickOpportunityL()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkOppL, 100);
            driver.FindElement(lnkOppL).Click();
        }

        //To Search Opportunity with Opportunity Name in Lighting
        public void SearchMyOpportunitiesInLightning(string value, string user)
        {
            try
            {
                Thread.Sleep(5000);
                if (user.Equals("James Craven") || user.Equals("Mark Fisher") || user.Equals("Brian Miller"))
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, btnOppNumL, 220);
                    driver.FindElement(btnOppNumL).Click();
                    Thread.Sleep(4000);
                    WebDriverWaits.WaitUntilEleVisible(driver, txtOppNumLCAO, 100);
                    driver.FindElement(txtOppNumLCAO).SendKeys(value);
                    Thread.Sleep(6000);
                    driver.FindElement(imgOppL).Click();
                    Thread.Sleep(6000);
                }
                else if (user.Equals("Derek Janisch"))
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, btnOppNumL, 250);
                    driver.FindElement(btnOppNumL).Click();
                    Thread.Sleep(4000);
                    WebDriverWaits.WaitUntilEleVisible(driver, txtOppAndMore, 100);
                    driver.FindElement(txtOppAndMore).SendKeys(value);
                    Thread.Sleep(7000);
                    driver.FindElement(imgOppL).Click();
                    Thread.Sleep(7000);
                }
                else
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, btnOppNumL, 150);
                    driver.FindElement(btnOppNumL).Click();
                    Thread.Sleep(4000);
                    WebDriverWaits.WaitUntilEleVisible(driver, txtOppNumLCAO, 100);
                    driver.FindElement(txtOppNumLCAO).SendKeys(value);
                    Thread.Sleep(6000);
                    driver.FindElement(imgOppL).Click();
                    Thread.Sleep(6000);
                }

                //WebDriverWaits.WaitUntilEleVisible(driver, imgOppL, 170);
                //driver.FindElement(imgOppL).Click();
                //Thread.Sleep(8000);
            }
            catch (Exception)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtOppAndMore, 100);
                driver.FindElement(txtOppAndMore).SendKeys(value);
                Thread.Sleep(8000);
                driver.FindElement(imgOppL).Click();
                Thread.Sleep(7000);
            }

        }

        public bool IsIndustryTypePresentInDropdownHomePage(string industryType)
        {
            bool isFound = false;
            driver.FindElement(comboIndustryType).Click();
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboIndustryTypeOptions);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            for (int row = 0; row <= actualValue.Length; row++)
            {
                if (actualValue[row].Contains(industryType))
                {
                    isFound = true;
                    break;
                }
            }
            return isFound;
        }
        public string SearchOpportunityWithIndustryType(string industryType)
        {
            By matchedOpportunity = By.XPath($"//table[contains(@id,'myOpportunities')]//tbody//td//span[contains(text(),'{industryType}')]");
            WebDriverWaits.WaitUntilEleVisible(driver, comboIndustryType);
            driver.FindElement(comboIndustryType).SendKeys(industryType);
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
        public string SearchOpportunityWithJobType(string jobType)
        {
            By matchedOpportunity = By.XPath($"//table[contains(@id,'myOpportunities')]//tbody//td//span[contains(text(),'{jobType}')]");
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
        public bool SearchRecentOpportunityLV(string oppName)
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
        public string GetSearchedOppJobType()
        {
            return driver.FindElement(eleItem).Text;
        }        

        //To Search Opportunity with Opportunity Name
        public string SearchMyOpportunitiesLV(string oppName)
        {
            driver.FindElement(txtSearchBox).SendKeys(oppName);
            driver.FindElement(txtSearchBox).SendKeys(Keys.Enter);
            Thread.Sleep(5000);
            By expectedResult = By.XPath($"//table/tbody//tr//th//a[@title='{oppName}']");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, resultTable, 20);
                bool resultDisplayed = driver.FindElement(expectedResult).Displayed;
                if (resultDisplayed)
                {
                    driver.FindElement(expectedResult).Click();
                    return "Record found";
                }
                else
                {
                    return "No record found";
                }
            }
            catch (Exception)
            {
                return "No record found";
            }
        }
        public string GlobalSearchOpportunityInLightningView(string OppName)
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, inputGlobalSearchL, 10);
            driver.FindElement(inputGlobalSearchL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtOppNumL, 10);
            driver.FindElement(txtOppNumL).SendKeys(OppName);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, imgOppL, 10);
                driver.FindElement(imgOppL).Click();
                Thread.Sleep(8000);
                return "Record found";
            }
            catch
            {
                try
                {
                    driver.FindElement(txtOppNumL).SendKeys(Keys.Enter);//inputAdminGlobalSearchL
                    Thread.Sleep(6000);
                    WebDriverWaits.WaitUntilEleVisible(driver, _lnkSearchedOppL(OppName), 20);
                    driver.FindElement(_lnkSearchedOppL(OppName)).Click();
                    Thread.Sleep(8000);
                    return "Record found";
                }
                catch { return "No record found"; }
            }
        }

        public string SearchOpportunitiesInLightningView(string value)
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnOppNumL, 10);
            driver.FindElement(btnOppNumL).Click();
            Thread.Sleep(4000);

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtOppNumLCAO, 10);
                driver.FindElement(txtOppNumLCAO).SendKeys(value);

            }
            catch
            {
                WebDriverWaits.WaitUntilEleVisible(driver, inputAdminGlobalSearchL, 10);
                driver.FindElement(inputAdminGlobalSearchL).SendKeys(value);
            }
            Thread.Sleep(6000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, imgOppL, 20);
                driver.FindElement(imgOppL).Click();
                Thread.Sleep(8000);
                return "Record found";
            }
            catch { return "No record found"; }
        }

        public string UpdateOppAndSearchLV(string oppName)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconClearSearch, 5);
                driver.FindElement(iconClearSearch).Click();
            }
            catch { }
            try
            {
                try
                {

                    WebDriverWaits.WaitUntilEleVisible(driver, btnOppNumL, 10);
                    //driver.FindElement(btnOppNumL).Clear();
                    driver.FindElement(btnOppNumL).Click();
                    driver.FindElement(txtOppNumLCAO).Clear();
                    driver.FindElement(txtOppNumLCAO).SendKeys(oppName);
                    Thread.Sleep(6000);
                }
                catch
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, inputAdminGlobalSearchL, 10);
                    //driver.FindElement(btnOppNumL).Clear();
                    driver.FindElement(inputAdminGlobalSearchL).Click();
                    driver.FindElement(inputAdminGlobalSearchL).Clear();
                    driver.FindElement(inputAdminGlobalSearchL).SendKeys(oppName);
                    Thread.Sleep(6000);

                }
            }
            catch
            {
                driver.FindElement(txtOppNumLCAO).Clear();
                driver.FindElement(txtOppNumLCAO).SendKeys(oppName);
                Thread.Sleep(6000);
            }
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, imgOppL, 10);
                driver.FindElement(imgOppL).Click();
                Thread.Sleep(8000);
                return "Record found";
            }
            catch { return "No record found"; }
        }
    }
}