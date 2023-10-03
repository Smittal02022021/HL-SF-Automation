using OpenQA.Selenium;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;


namespace SF_Automation.Pages.Common
{
    class RandomPages : BaseClass
    {
        By linkExpenseRequest = By.XPath("//a[text() = 'Expense Request']");
        By linkExpenseNumber = By.CssSelector("tbody[id*='pbtableId1:tb']>tr>td[id*='j_id142'] > a");
        By titleNewExpense = By.XPath("//h2[text() = 'New Expense Request']");
        By btnClone = By.CssSelector("input[value='Clone']");
        By linkDBCompanyRecords = By.XPath("//a[text() = 'D&B Company Records']");
        By btnAllList = By.Id("AllTab_Tab");
        By linkRowSelection = By.CssSelector("div[id*='FaE_Name'] > a");
        By linkDBContactRecords = By.XPath("//a[text() = 'D&B Contact Records']");
        By linkRowSelectionDB = By.CssSelector("div[id*='xdu_Name'] > a");
        By btnGo = By.CssSelector("input[title='Go!']");
        By titleCoverageTeam = By.CssSelector("h2[class='mainTitle']");
        By linkDBContactRec = By.CssSelector("div[id*='p_Name'] > a");
        By linkLegalEntities = By.XPath("//a[text() = 'Legal Entities']");
        By linkLegalEntityName = By.XPath("//th/a[text() = 'HL Capital, Inc.']");
        By linkJobTypes = By.XPath("//a[text() = 'Job Types']");
        By valProductLine = By.CssSelector("table > tbody > tr:nth-child(21) > td:nth-child(2)");
        By valProductTypeCode = By.CssSelector("table > tbody > tr:nth-child(22) > td:nth-child(2)");
        By valJobTypeNames = By.CssSelector("div[id*='_Name']>a>span");
        By valJobTypes = By.CssSelector("div[class*='-row']>table>tbody>tr>td:nth-child(3)");
        By valProdLines = By.CssSelector("div[class*='-row']>table>tbody>tr>td:nth-child(4)");
        By valBlank = By.CssSelector("div[id*='wN2_00Ni000000G8Xmo']");

        By txtPageCountString = By.XPath("//div[@class='paginator']//td[2]//span//i");
        By linkNext = By.XPath("//div[@class='paginator']//a[contains(text(),'Next')]");
        By loader = By.XPath("//b[contains(text(),'Loading')]");
        By shwAllTab = By.CssSelector("li[id='AllTab_Tab'] > a > img");
        By comboViewJobType = By.CssSelector("select[name*='fcf']");
        By comboJobType = By.CssSelector("select[name*='jobTypeSearch']");
        By txtPageTitle = By.XPath("//h1[@class='pageType']");
        By tabReports = By.XPath("//a[@title='Reports Tab']");
        By btnNewReport = By.XPath("//Input[@title='New Report...']");
        By searchBox = By.XPath("//input[@id='quickFindInput']");
        By btnCreateReport = By.CssSelector("input[value='Create']");
        By btnAddFilter = By.XPath("//button[normalize-space()='Add']");
        By searchField = By.XPath("//div[@class='cfPanel_fieldFilterSection']/div/div[2]/div[2]/div/form/div/div[2]/input[2]");
        By txtReportsPageHeader = By.XPath("//h1[@class='pageType noSecondHeader']");
        By txtReportHeader = By.XPath("//h1[@class='pageType']");
        By txtSearchValue = By.XPath("//input[@name='pv']");
        By listOptions = By.XPath("//div[@class='lookup']//label");
        By iconLookup = By.XPath("//a[@class='rb_lookupIcon']");
        By sectionFilter = By.XPath("//div[contains(@class,'filterItemContainer')]");
        By optionsFilterFields = By.XPath("//div[contains(@class,'filterItemContainer')]//div[2]//form//div//div[2]//img");
        By filterList = By.XPath("//div[contains(@class,'combo-list pc-list')]//div//div");
        By btnCloseReport = By.XPath("//table[@id='closeBtn']//button");
        By btnConfirmClose = By.XPath("//div[contains(@class,'window-footer')]//button[text() = 'Close']");

        By comboIndustryTypeOpp = By.CssSelector("select[id*='pipelineManagerForm:industryGroupOptionsOpps']");
        By comboIndustryTypeOptionsOpp = By.CssSelector("select[id*='pipelineManagerForm:industryGroupOptionsOpps'] option");
        By comboIndustryTypeEng = By.CssSelector("select[id*='pipelineManagerForm:industryGroupOptionsEng']");
        By comboIndustryTypeOptionsEng = By.CssSelector("select[id*='pipelineManagerForm:industryGroupOptionsEng'] option");
        By btnApplyFilter = By.CssSelector("input[id*='ApplyFilters']");
        By comboJobTypeOptions = By.CssSelector("select[name*='jobTypeSearch'] option");
        By btnMoreTabs = By.XPath("(//ul[@role='tablist']//button[@title='More Tabs'])[2]");
        By linkActivity = By.XPath("//div[@role='menu']//lightning-menu-item//a//span[text()='Activity']");
        By tabActivity = By.XPath("//li[@title='Activity']//a[@id='flexipage_tab4__item']");
        private By _elmIGType(string industryType)
        {
            return By.XPath($"//div[contains(@id,'ManagerContainer')]//table//tbody//td[contains(@id,'Industry')]//span[contains(text(),'{industryType}')]");
        }
        private By _optionReports(string reportName)
        {
            return By.XPath($"//ul[contains(@class,'tree-lines')]//li//span[text()='{reportName}']");
        }
        private By _txtJobTypeMgrPage(string valJobType)
        {
            return By.XPath($"//td[contains(@id,'PIPELINE_Job_Type')]//span[contains(text(),'{valJobType}')]");
        }

        private By _txtJobTypeObjPage(string valJobType)
        {
            return By.XPath($"//table[contains(@class,'row-table')]//span[contains(text(),'{valJobType}')]");
        }

        private By _objJobTypeCode(string valJobCode)
        {
            return By.XPath($"//div[contains(text(),'{valJobCode}')]");
        }

        private By _TabEle(string value)
        {
            return By.XPath($"//button[contains(@title,{value})]");
        }
        private By _txtPageHeader(string itemName)
        {
            return By.XPath($"//h1//slot//lightning-formatted-text[text()='{itemName}']");
        }

        public string ClickReportsTab()
        {
            try
            {
                driver.FindElement(tabReports).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, txtReportsPageHeader, 20);
                return driver.FindElement(txtReportsPageHeader).Text;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
        public string CreateNewReport(string reportName)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnNewReport, 20);
                driver.FindElement(btnNewReport).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, searchBox, 20);
                driver.FindElement(searchBox).SendKeys(reportName);
                Thread.Sleep(2000);
                IWebElement optionReport = driver.FindElement(_optionReports(reportName));
                CustomFunctions.MoveToElement(driver, optionReport);
                optionReport.Click();
                driver.FindElement(btnCreateReport).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, txtReportHeader, 20);
                return driver.FindElement(txtReportHeader).Text;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }
        public string AddFilter(string value)
        {
            try
            {
                Thread.Sleep(1000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnAddFilter, 20);
                driver.FindElement(btnAddFilter).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, sectionFilter, 20);

                WebDriverWaits.WaitUntilEleVisible(driver, optionsFilterFields, 10);
                driver.FindElement(optionsFilterFields).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, filterList, 5);
                IReadOnlyCollection<IWebElement> listOption = driver.FindElements(filterList);
                foreach (IWebElement element in listOption)
                {
                    CustomFunctions.MoveToElement(driver, element);
                    if (element.Text == value)
                    {
                        element.Click();
                        break;
                    }
                }
                return driver.FindElement(searchField).GetAttribute("value");
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public bool IsRecordAvailableOnReportsPage(string valJobType)
        {
            bool found = false;
            WebDriverWaits.WaitUntilEleVisible(driver, iconLookup, 10);
            driver.FindElement(iconLookup).Click();
            CustomFunctions.SwitchToWindow(driver, 1);
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(listOptions);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            string expectedValue = valJobType;
            for (int rec = 0; rec < actualValue.Length; rec++)
            {
                if (expectedValue.Equals(actualValue[rec]))
                {
                    found = true;
                    break;
                }
            }
            driver.Close();
            CustomFunctions.SwitchToWindow(driver, 0);
            return found;
        }

        public string CloseUnsavedReport()
        {
            try
            {
                driver.FindElement(btnCloseReport).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmClose, 10);
                driver.FindElement(btnConfirmClose).Click();
                Thread.Sleep(1000);
                WebDriverWaits.WaitUntilEleVisible(driver, txtReportsPageHeader, 20);
                return driver.FindElement(txtReportsPageHeader).Text;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        public string selectJobTypesObject(string viewOption)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, shwAllTab, 20);
            driver.FindElement(shwAllTab).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkJobTypes, 20);
            driver.FindElement(linkJobTypes).Click();
            string pageTitle = driver.FindElement(txtPageTitle).Text;
            driver.FindElement(comboViewJobType).SendKeys(viewOption);
            try
            {
                driver.FindElement(btnGo).Click();
            }
            catch (Exception e)
            {
                //do nothing
            }

            return pageTitle;
        }
        //Validate the Job Type in Displayed in List 
        public bool IsJobTypeVailableOnPage(string pageTitle, string valJobType)
        {
            //string paginationFooter = driver.FindElement(txtPageCountString).Text;
            //string lastSubstr = paginationFooter.Substring(paginationFooter.LastIndexOf(" ") + 1);
            //int pageCount = int.Parse(lastSubstr);
            bool isJobTypefound = false;
            IWebElement elmJobType;

        check: try
            {
                if (pageTitle == "Job Types")
                {
                    elmJobType = driver.FindElement(_txtJobTypeObjPage(valJobType));
                }
                else
                {
                    elmJobType = driver.FindElement(_txtJobTypeMgrPage(valJobType));
                }

                CustomFunctions.MoveToElement(driver, elmJobType);
                if (elmJobType.Displayed)
                {
                    isJobTypefound = true;
                    //break;
                }
            }
            catch (Exception e)
            {
                try
                {
                    if (driver.FindElement(linkNext).Enabled)
                    {
                        driver.FindElement(linkNext).Click();
                        WebDriverWaits.WaitTillElementVisible(driver, loader);
                        Thread.Sleep(1000);
                        goto check;
                    }
                }
                catch (Exception ee)
                {
                    isJobTypefound = false;
                }

            }
            //}
            return isJobTypefound;
        }

        public bool IsJobCodeAvailable(string valJobCode)
        {
            try
            {
                return driver.FindElement(_objJobTypeCode(valJobCode)).Displayed;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //To click Expense Request Tab
        public string ClickExpenseRequestTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkExpenseRequest, 120);
            driver.FindElement(linkExpenseRequest).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleNewExpense, 100);
            string title = driver.FindElement(titleNewExpense).Text;
            return title;
        }

        //To Click Expense Number
        public string ClickExpenseNumberandValidateCloneButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkExpenseNumber, 100);
            driver.FindElement(linkExpenseNumber).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(2000);
            try
            {
                string value = driver.FindElement(btnClone).Displayed.ToString();
                Console.WriteLine(value);
                if (value.Equals("True"))
                {
                    return "Clone button is displayed";
                }
                else
                {
                    return "Clone button is not displayed";
                }
            }
            catch (Exception)
            {
                return "Clone button is not displayed";
            }
        }

        //To close additional opened tab
        public void CloseAdditionalTab()
        {
            driver.Close();
            Thread.Sleep(2000);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }

        //To click D&B Company Records
        public string ClickDBCompanyRecords()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAllList, 90);
            driver.FindElement(btnAllList).Click();
            driver.FindElement(linkDBCompanyRecords).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo, 90);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkRowSelection, 90);
            driver.FindElement(linkRowSelection).Click();
            string title = driver.FindElement(titleCoverageTeam).Text;
            return title;
        }

        //To click D&B Contact Records
        public string ClickDBContactRecords()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAllList, 90);
            driver.FindElement(btnAllList).Click();
            driver.FindElement(linkDBContactRecords).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo, 100);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkDBContactRec, 110);
            driver.FindElement(linkDBContactRec).Click();
            string title = driver.FindElement(titleCoverageTeam).Text;
            return title;
        }

        //To click Legal Entities 
        public string ClickLegalEntities(string name)
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAllList, 150);
            driver.FindElement(btnAllList).Click();
            driver.FindElement(linkLegalEntities).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo, 120);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div/a/span[text() = '" + name + "']")).Click();
            string title = driver.FindElement(titleCoverageTeam).Text;
            return title;
        }

        //To Get Job Types 
        public string ClickJobTypes(string name)
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAllList, 90);
            driver.FindElement(btnAllList).Click();
            driver.FindElement(linkJobTypes).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo, 100);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div/a/span[text() = '" + name + "']")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleCoverageTeam, 110);
            string title = driver.FindElement(titleCoverageTeam).Text;
            return title;
        }





        //To get Job Types 
        public void GetJobTypes()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAllList, 90);
            driver.FindElement(btnAllList).Click();
            driver.FindElement(linkJobTypes).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo, 100);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
            IReadOnlyCollection<IWebElement> type = driver.FindElements(valJobTypes);
            //Console.WriteLine("NUMBER OF ROWS IN THIS TABLE = " + type.Count);
            int row_num = 1;
            foreach (IWebElement element in type)
            {
                Console.WriteLine(element.Text);
                row_num++;
            }
        }

        //To get Product Line 
        public void GetProductLines()
        {
            Thread.Sleep(3000);
            IReadOnlyCollection<IWebElement> type = driver.FindElements(valProdLines);
            Console.WriteLine("NUMBER OF ROWS IN THIS TABLE = " + type.Count);

            int row_num = 1;

            foreach (IWebElement element in type)
            {
                Console.WriteLine("row# " + row_num + element.Text);
                row_num++;
            }
        }



        public string GetBlankValue()
        {
            string value = driver.FindElement(valBlank).Text;
            return value;
        }




        //Get the value of Product Line
        public string GetProductLine()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valProductLine, 110);
            string value = driver.FindElement(valProductLine).Text;
            return value;
        }

        //Get the value of Product Type Code
        public string GetProductTypeCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valProductTypeCode, 110);
            string value = driver.FindElement(valProductTypeCode).Text;
            return value;
        }

        //To Validate Job Types 
        public bool ValidateJobTypes()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAllList, 90);
            driver.FindElement(btnAllList).Click();
            driver.FindElement(linkJobTypes).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo, 100);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
            IReadOnlyCollection<IWebElement> jobTypes = driver.FindElements(valJobTypes);
            var actualValue = jobTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Activism Advisory", "Board Advisory Services (BAS)", "Buyside", "Buyside & Financing Advisory", "Collateral Valuation", "Compensation/Formula Analysis", "Consulting", "Corporate Alliances", "Creditor Advisors", "CVAS - Complex Securities", "CVAS - Forensic Services", "CVAS - FV Opinion", "CVAS - Goodwill or Asset Impairment", "CVAS - Modeling", "CVAS - Pre-Acq Val'n Cons", "CVAS - Purchase Price Allocation", "CVAS - SFAS 123R/409A Stock, Option Valuation", "CVAS - Sovereign Advisory", "CVAS - Tangible Asset Valuation", "CVAS - Tax Valuation", "CVAS - Transfer Pricing", "Debt Advisory", "Debt Capital Markets", "Debtor Advisors", "Discretionary Advisory", "DM&A Buyside", "DM&A Sellside", "DRC - Ad Valorem Services", "DRC - Appointed Arbitrator/Mediator", "DRC - Contract Compliance", "DRC - Exp Cons-Arbitrat'n", "DRC - Exp Cons-Bankruptcy", "DRC - Exp Cons-Litigation", "DRC - Exp Cons-Mediation", "DRC - Exp Cons-Pre-Complt", "DRC - Exp Wit-Arbitration", "DRC - Exp Wit-Bankruptcy", "DRC - Exp Wit-Litigation", "DRC - Exp Wit-Mediation", "DRC - Exp Wit-Pre-Complnt", "DRC - Post Transaction Dispute", "Equity Advisors", "Equity Capital Markets", "ESOP Advisory - CVAS", "ESOP Advisory - DRC", "ESOP Advisory - Other", "ESOP Advisory - PV", "ESOP Advisory - TO", "ESOP Capital Partnership", "ESOP Corporate Finance", "ESOP Fairness", "ESOP Update", "Estate & Gift Tax", "FA - Fund Opinions-Fairness", "FA - Fund Opinions-Non-Fairness", "FA - Fund Opinions-Valuation", "FA - Portfolio - SPAC", "FA - Portfolio LIBOR Advisory", "FA - Portfolio-Advis/Consulting", "FA - Portfolio-Auto Loans", "FA - Portfolio-Auto Struct Prd", "FA - Portfolio-BDC/Direct Lending", "FA - Portfolio-Deriv/Risk Mgmt", "FA - Portfolio-Diligence/Assets", "FA - Portfolio-Funds Transfer", "FA - Portfolio-GP interest", "FA - Portfolio-Outsourcing", "FA - Portfolio-Real Estate", "FA - Portfolio-Valuation", "Fairness", "FAS - Administrative", "Financing", "FMV Non-Transaction Based Opinion", "FMV Transaction Based Opinion", "General Financial Advisory", "Going Private", "Illiquid Financial Assets", "Income Deposit Securities", "InSource", "Liability Management", "Liability Mgmt", "Litigation", "Merger", "Negotiated Fairness", "Partners", "PBAS", "Portfolio Acquisition", "Post Merger Integration", "Private Funds: GP Advisory", "Private Funds: GP Stake Sale", "Private Funds: Primary Advisory", "Private Funds: Secondary Advisory", "Real Estate Brokerage", "Regulator/Other", "Securities Design", "Sellside", "Solvency", "Sovereign Restructuring", "Special Committee Advisory", "Special Situations", "Special Situations Buyside", "Special Situations Sellside", "Strategic Alternatives Study", "Strategic Consulting", "Strategy", "Syndicated Finance", "T+IP - Damages", "T+IP - Expert Report", "Take Over Defense", "TAS - Accounting and Financial Reporting Advisory", "TAS - Due Diligence-Buyside", "TAS - Due Diligence-Sellside", "TAS - DVC Business Analytics", "TAS - DVC Decision Modeling", "TAS - Lender Services", "TAS - Tax", "TAS - Tech & Cyber", "Tech+IP - Buyside", "Tech+IP - Patent Acquisition Support", "Tech+IP - Patent Sales", "Tech+IP - Strategic Advisory", "Tech+IP - Tech+IP Sales", "Tech+IP - Valuation", "Valuation Advisory" };
            bool isSame = true;

            List<string> difference = actualValue.Except(expectedValue).ToList();
            foreach (var value in difference)
            {
                Console.WriteLine(value);
            }

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

        //

        //To Validate Job Types 
        public bool ValidateProductLines()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAllList, 90);
            driver.FindElement(btnAllList).Click();
            driver.FindElement(linkJobTypes).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo, 100);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
            IReadOnlyCollection<IWebElement> prodLines = driver.FindElements(valProdLines);
            var actualValue = prodLines.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Advisory", "Transaction Opinions", "Buyside", "Capital Markets", "Other", "Other", "Other", "Advisory", "Financial Restructuring - Creditor / Debtor", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "Capital Markets", "Capital Markets", "Financial Restructuring - Creditor / Debtor", "Advisory", "Financial Restructuring - Other", "Financial Restructuring - Other", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Financial Restructuring - Other", "Capital Markets", "CVAS - Corporate Valuation Advisory Services", "Dispute", "Other", "Portfolio Valuation & Advisory", "Transaction Opinions", "Other", "Advisory", "Transaction Opinions", "Other", "Dispute", "Fund Opinions", "Fund Opinions", "Fund Opinions", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Transaction Opinions", " ", "Capital Markets", "Other", "Transaction Opinions", "Advisory", "Advisory", "Advisory", "Other", "Other", "Capital Markets", "Financial Restructuring - Other", "Dispute", "Sellside", "Advisory", "Capital Markets", "Financial Restructuring - Other", " ", "Advisory", "Private Funds Advisory", "Private Funds Advisory", "Private Funds Advisory", "Private Funds Advisory", "Advisory", "Financial Restructuring - Other", "Other", "Sellside", "Transaction Opinions", " ", "Advisory", "Advisory", "Buyside", "Sellside", "Advisory", "Strategic Consulting", "Advisory", "Capital Markets", "Tech+IP Advisory", "Tech+IP Advisory", "Advisory", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "Advisory", "Tech+IP Advisory", "Advisory", "Tech+IP Advisory", "Advisory", "Tech+IP Advisory", "Advisory" };

            bool isSame = true;

            List<string> difference = actualValue.Except(expectedValue).ToList();
            //int row_num = 1;
            foreach (var value in difference)
            {
                Console.WriteLine(value);
            }

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
        public bool IsIndustryTypePresentInDropdownOppManager(string valIndustryGroup)
        {
            bool isFound = false;
            driver.FindElement(comboIndustryTypeOpp).Click();
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboIndustryTypeOptionsOpp);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            for (int row = 0; row <= actualValue.Length; row++)
            {
                if (actualValue[row].Contains(valIndustryGroup))
                {
                    isFound = true;
                    break;
                }
            }
            driver.FindElement(comboIndustryTypeOpp).Click();
            return isFound;
        }



        public bool IsIndustryTypePresentInDropdownEngManager(string valIndustryGroup)
        {
            bool isFound = false;
            driver.FindElement(comboIndustryTypeEng).Click();
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboIndustryTypeOptionsEng);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            for (int row = 0; row <= actualValue.Length; row++)
            {
                if (actualValue[row].Contains(valIndustryGroup))
                {
                    isFound = true;
                    break;
                }
            }
            driver.FindElement(comboIndustryTypeEng).Click();
            return isFound;
        }
        public bool IsOpportunityFoundWithIndustryType(string industryType)
        {
            bool isFound = false;
            WebDriverWaits.WaitUntilEleVisible(driver, comboIndustryTypeOpp);
            driver.FindElement(comboIndustryTypeOpp).SendKeys(industryType);
            driver.FindElement(btnApplyFilter).Click();
            Thread.Sleep(15000);
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(_elmIGType(industryType));
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
        public bool IsEngagementFoundWithIndustryType(string industryType)
        {
            bool isFound = false;
            WebDriverWaits.WaitUntilEleVisible(driver, comboIndustryTypeEng);
            driver.FindElement(comboIndustryTypeEng).SendKeys(industryType);
            driver.FindElement(btnApplyFilter).Click();
            Thread.Sleep(15000);
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(_elmIGType(industryType));
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



        By dropdownCompaign = By.CssSelector("select[id='fcf']");

        //Verify Industry Group in list 
        public bool IsIndustryGroupAvailableOnCampaignPage(string valCampaign, string valIndustryGroup)
        {
            By typeIG = By.XPath($"//div[@class='listBody']//table//tbody//td//div[text()='{valIndustryGroup}']");
            bool isIndustryGroupfound = false;
            IWebElement elmIGType;
            driver.FindElement(dropdownCompaign).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownCompaign);
            driver.FindElement(dropdownCompaign).SendKeys(valCampaign);
            driver.FindElement(dropdownCompaign).Click();
            try
            {
                driver.FindElement(btnGo).Click();
            }
            catch (Exception ex) { }
            Thread.Sleep(5000);
        check: try
            {
                elmIGType = driver.FindElement(typeIG);
                CustomFunctions.MoveToElement(driver, elmIGType);
                if (elmIGType.Displayed)
                {
                    isIndustryGroupfound = true;
                    //break;
                }
            }
            catch (Exception e)
            {
                if (driver.FindElement(linkNext).Enabled)
                {
                    driver.FindElement(linkNext).Click();
                    WebDriverWaits.WaitTillElementVisible(driver, loader);
                    Thread.Sleep(1000);
                    goto check;
                }
            }
            return isIndustryGroupfound;
        }
        public bool IsJobTypePresentInDropdownHomePage(string jobType)
        {
            bool isFound = false;
            driver.FindElement(comboJobType).Click();
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboJobTypeOptions);
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
        public void CloseActiveTab(string name)
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, _TabEle("'Close " + name + "'"), 30);
            IWebElement closeTabIcon = driver.FindElement(_TabEle("'Close " + name + "'"));
            closeTabIcon.Click();
            Thread.Sleep(2000);
        }
        public bool IsPageHeaderDisplayedLV(string item)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, _txtPageHeader(item), 20);
                return driver.FindElement(_txtPageHeader(item)).Displayed;
            }
            catch { return false; }
        }
        public void ClickActivityTab()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tabActivity, 10);
                driver.FindElement(tabActivity).Click();
                Thread.Sleep(5000);
            }
            catch (Exception e)
            {
                driver.FindElement(btnMoreTabs).Click();
                Thread.Sleep(2000);
                driver.FindElement(linkActivity).Click();
                Thread.Sleep(5000);
            }
        }
    }
}
