using Microsoft.Office.Interop.Excel;
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
        By iconListViewPicker = By.XPath("//div[contains(@class,'name-switcher')]//button[contains(@title,'Select a List View')]");//div[contains(@class,'ListViewPicker')]//button[contains(@title,'Select a List View')]");
        By msgLVPopup = By.CssSelector("span.toastMessage.forceActionsText");
        By dropdownCompaign = By.CssSelector("select[id='fcf']");
        By frameTimeRecordPage = By.XPath("//iframe[@title='accessibility title']");
        By imgSpinningLoader = By.XPath("//div[@class='loading']");
        By txtProductLineL = By.XPath("//div[contains(@data-target-selection-name,'Product_Line__c')]//lightning-formatted-text");
        By txtProductTypeCodeL = By.XPath("//div[contains(@data-target-selection-name,'Product_Type_Code__c')]//lightning-formatted-text");
        By valERPProductTypeL = By.XPath("//records-record-layout-item[@field-label='Product Type']//lightning-formatted-text");//div[@class='slds-form']//records-record-layout-item[@field-label='Product Type']//dd//lightning-formatted-text");
        By valERPProductTypCodeL = By.XPath("//records-record-layout-item[@field-label='ERP Product Type Code']//lightning-formatted-text");//div[@class='slds-form']//records-record-layout-item[@field-label='ERP Product Type Code']//dd//lightning-formatted-text");
        By checkERPUpdateDFFL = By.XPath("//records-record-layout-item[@field-label='ERP Update DFF']//input[@type='checkbox']");//div[@class='slds-form']//records-record-layout-item[@field-label='ERP Update DFF']//input[@type='checkbox']");
        By valERPSubmittedToSyncL = By.XPath("//records-record-layout-item[@field-label='ERP Submitted To Sync']//lightning-formatted-text");//div[@class='slds-form']//records-record-layout-item[@field-label='ERP Submitted To Sync']//dd//lightning-formatted-text");
        By valERPLastIntStatusL = By.XPath("//records-record-layout-item[@field-label='ERP Last Integration Status']//lightning-formatted-text");//records-record-layout-item[@field-label='ERP Last Integration Status']//dd//lightning-formatted-text");////div[@class='slds-form']//records-record-layout-item[@field-label='ERP Last Integration Status']//dd//lightning-formatted-text
        By valERPLastIntegrationResponseDateL = By.XPath("//records-record-layout-item[@field-label='ERP Last Integration Response Date']//lightning-formatted-text");//records-record-layout-item[@field-label='ERP Last Integration Response Date']//dd//lightning-formatted-text");////div[@class='slds-form']//records-record-layout-item[@field-label='ERP Last Integration Response Date']//dd//lightning-formatted-text
        By btnSaveL = By.XPath("//button[text()='Save']");

        By iconInlineEditERPSubmittedToSyncL = By.XPath("//records-record-layout-item[@field-label='ERP Submitted To Sync']//dd//button");
        By textDatePickerL = By.XPath("//records-record-layout-item[@field-label='ERP Submitted To Sync']//lightning-datepicker//input");
        By txtTimePickerL = By.XPath("//records-record-layout-item[@field-label='ERP Submitted To Sync']//lightning-timepicker//input");
        By valAdminPrimaryOfficeL = By.XPath("//h3//span[text()='Administration']//ancestor::h3/following-sibling::div//records-record-layout-item[@field-label='Primary Office']//dd//lightning-formatted-text");
        By txtHLSectorIDL = By.XPath("//flexipage-field[contains(@data-field-id,'Industry_Sector_cField')]//records-hoverable-link//a//span");
        By txtHLSectorComboL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordSector_Combo_cField')]//dd//lightning-formatted-text");
        By txtJobTypeL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordJob_Type')]//dd//lightning-formatted-text");
        By valRecordTypeL = By.XPath("//div[contains(@data-target-selection-name,'RecordType')]//dd//div[contains(@class,'recordTypeName')]/span");
        By valERPIDL = By.XPath("//records-record-layout-item[@field-label='ERP ID']//lightning-formatted-text");//div[@class='slds-form']//records-record-layout-item[@field-label='ERP ID']//dd//lightning-formatted-text");
        By valERPProjStatusCodeL = By.XPath("//records-record-layout-item[@field-label='ERP Project Status Code']//lightning-formatted-text");//div[@class='slds-form']//records-record-layout-item[@field-label='ERP Project Status Code']//dd//lightning-formatted-text");
        By valERPProjectNumberL = By.XPath("//records-record-layout-item[@field-label='ERP Project Number']//lightning-formatted-text");//div[@class='slds-form']//records-record-layout-item[@field-label='ERP Project Number']//dd//lightning-formatted-text");
        By valERPProjectNameL = By.XPath("//records-record-layout-item[@field-label='ERP Project Name']//lightning-formatted-text");//div[@class='slds-form']//records-record-layout-item[@field-label='ERP Project Name']//dd//lightning-formatted-text");
        By valERPLOBL = By.XPath("//records-record-layout-item[@field-label='ERP LOB']//lightning-formatted-text");//div[@class='slds-form']//records-record-layout-item[@field-label='ERP LOB']//dd//lightning-formatted-text");
        By valERPTemplateL = By.XPath("//records-record-layout-item[@field-label='ERP Template']//lightning-formatted-text");//div[@class='slds-form']//records-record-layout-item[@field-label='ERP Template']//dd//lightning-formatted-text");
        By valERPUnitL = By.XPath("//records-record-layout-item[@field-label='ERP Business Unit']//lightning-formatted-text");//div[@class='slds-form']//records-record-layout-item[@field-label='ERP Business Unit']//dd//lightning-formatted-text");
        By valERPUnitIDL = By.XPath("//records-record-layout-item[@field-label='ERP Business Unit Id']//lightning-formatted-text");//div[@class='slds-form']//records-record-layout-item[@field-label='ERP Business Unit Id']//dd//lightning-formatted-text");
        By valERPLegalEntityIDL = By.XPath("//records-record-layout-item[@field-label='ERP Legal Entity Id']//lightning-formatted-text");//div[@class='slds-form']//records-record-layout-item[@field-label='ERP Legal Entity Id']//dd//lightning-formatted-text");
        By valERPEntityCodeL = By.XPath("//records-record-layout-item[@field-label='ERP Entity Code']//lightning-formatted-text");//div[@class='slds-form']//records-record-layout-item[@field-label='ERP Entity Code']//dd//lightning-formatted-text");
        By valERPLegCodeL = By.XPath("//records-record-layout-item[@field-label='ERP Legislation Code']//lightning-formatted-text");
        By valERPIGL = By.XPath("//records-record-layout-item[contains(@field-label,'ERP Industry Group')]//lightning-formatted-text");
        By valHLEntityL = By.XPath("//records-record-layout-item[@field-label='HL Entity']//lightning-formatted-text");
        By valERPHLEntityL = By.XPath("//records-record-layout-item[@field-label='ERP HL Entity']//lightning-formatted-text");
        By valERPLegalEntityL = By.XPath("//records-record-layout-item[@field-label='ERP Legal Entity']//lightning-formatted-text");
        By valERPErrorL = By.XPath("//records-record-layout-item[contains(@field-label,'Error Description')]//lightning-formatted-text");
        By valERPEmailIDL = By.XPath("//records-record-layout-item[@field-label='ERP Principal Manager']//lightning-formatted-text");
        By valLOBL = By.XPath("//records-record-layout-item[@field-label='Line of Business']//lightning-formatted-text");
        By valJobCodeL = By.XPath("//records-record-layout-item[@field-label='Job Code']//lightning-formatted-text");
        By tabFullViewL = By.XPath("//lightning-tab-bar/ul/li/a[text()='Full View']");
        By tabMoreFullViewL = By.XPath("//lightning-tab-bar/ul/li/lightning-button-menu//a/span[text()='Full View']");
        By iconHeaderMoreTabsL = By.XPath("(//lightning-tab-bar/ul/li/lightning-button-menu/button[@title='More Tabs'])[1]");

        By chkVerballyEngL = By.XPath("//input[@name='Verbally_Engaged__c']");
        By toastMsgPopup = By.XPath("//span[contains(@class,'toastMessage')]");

        By lnkRefereshTabL = By.XPath("//ul//li[@title='Refresh Tab']/a");
        By tabOracleERPL = By.XPath("//lightning-tab-bar/ul/li/a[text()='Oracle ERP']");
        By tabMoreOracleERPL = By.XPath("//lightning-tab-bar/ul/li/lightning-button-menu//a/span[text()='Oracle ERP']");

        public void RefreshActiveTab(string name)
        {
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, _TabEle("'Actions for " + name + "'"), 10);
            IWebElement closeTabIcon = driver.FindElement(_TabEle("'Actions for " + name + "'"));
            closeTabIcon.Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRefereshTabL, 5);
            driver.FindElement(lnkRefereshTabL).Click();
            Thread.Sleep(8000);
        }

        private By _optionListView(string name)
        {
            return By.XPath($"//lightning-popup/section//span[text()='{name}']"); //div[contains(@class,'scroller')]//ul[contains(@aria-label,'List Views')]//li//a//span[text()='{name}']");
        }
        private By _optionListViewL(string name)
        {
            return By.XPath($"//div[contains(@class,'scroller')]//ul[contains(@aria-label,'List Views')]//li//a//span[text()='{name}']");
        }
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
                Thread.Sleep(4000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnNewReport, 20);
                driver.FindElement(btnNewReport).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, searchBox, 20);
                driver.FindElement(searchBox).SendKeys(reportName);
                Thread.Sleep(2000);
                IWebElement optionReport = driver.FindElement(_optionReports(reportName));
                CustomFunctions.MoveToElement(driver, optionReport);
                optionReport.Click();
                driver.FindElement(btnCreateReport).Click();
                Thread.Sleep(2000);
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
                Thread.Sleep(4000);
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
            string[] expectedValue = {"Activism Advisory", "Board Advisory Services (BAS)", "Buyside", "Buyside & Financing Advisory", "Collateral Valuation", "Compensation/Formula Analysis", "Consulting", "Corporate Alliances", "Creditor Advisors", "CVAS - Complex Securities", "CVAS - Forensic Services", "CVAS - FV Opinion", "CVAS - Goodwill or Asset Impairment", "CVAS - IP Valuation", "CVAS - Modeling", "CVAS - Pre-Acq Val'n Cons", "CVAS - Purchase Price Allocation", "CVAS - SFAS 123R/409A Stock, Option Valuation", "CVAS - Sovereign Advisory", "CVAS - Tangible Asset Valuation", "CVAS - Tax Valuation", "CVAS - Transfer Pricing", "Debt Advisory", "Debt Capital Markets", "Debtor Advisors", "Discretionary Advisory", "DM&A Buyside", "DM&A Sellside", "DRC - Ad Valorem Services", "DRC - Appointed Arbitrator/Mediator", "DRC - Contract Compliance", "DRC - Exp Cons-Arbitrat'n", "DRC - Exp Cons-Bankruptcy", "DRC - Exp Cons-Litigation", "DRC - Exp Cons-Mediation", "DRC - Exp Cons-Pre-Complt", "DRC - Exp Wit-Arbitration", "DRC - Exp Wit-Bankruptcy", "DRC - Exp Wit-Litigation", "DRC - Exp Wit-Mediation", "DRC - Exp Wit-Pre-Complnt", "DRC - Post Transaction Dispute", "Equity Advisors", "Equity Capital Markets", "ESOP Advisory - CVAS", "ESOP Advisory - DRC", "ESOP Advisory - Other", "ESOP Advisory - PV", "ESOP Advisory - TO", "ESOP Capital Partnership", "ESOP Corporate Finance", "ESOP Fairness", "ESOP Update", "Estate & Gift Tax", "FA - Fund Opinions-Fairness", "FA - Fund Opinions-Non-Fairness", "FA - Fund Opinions-Valuation", "FA - Portfolio - SPAC", "FA - Portfolio LIBOR Advisory", "FA - Portfolio-Advis/Consulting", "FA - Portfolio-Auto Loans", "FA - Portfolio-Auto Struct Prd", "FA - Portfolio-Auto Struct Prd/Consulting", "FA - Portfolio-BDC/Direct Lending", "FA - Portfolio-Deriv/Risk Mgmt", "FA - Portfolio-Diligence/Assets", "FA - Portfolio-Funds Transfer", "FA - Portfolio-GP interest", "FA - Portfolio-Outsourcing", "FA - Portfolio-Real Estate", "FA - Portfolio-Valuation", "Fairness", "FAS - Administrative", "Financing", "FMV Non-Transaction Based Opinion", "FMV Transaction Based Opinion", "General Financial Advisory", "Going Private", "Illiquid Financial Assets", "Income Deposit Securities", "InSource", "Lender Education", "Liability Management", "Liability Mgmt", "Litigation", "Merger", "Negotiated Fairness", "Partners", "PBAS", "Portfolio Acquisition", "Post Merger Integration", "Private Funds: GP Advisory", "Private Funds: GP Stake Sale", "Private Funds: Primary Advisory", "Private Funds: Secondary Advisory", "Real Estate Brokerage", "Regulator/Other", "Securities Design", "Sellside", "Solvency", "Sovereign Restructuring", "Special Committee Advisory", "Special Situations", "Special Situations Buyside", "Special Situations Sellside", "Strategic Alternatives Study", "Strategic Consulting", "Strategy", "Syndicated Finance", "T+IP - Damages", "T+IP - Expert Report", "Take Over Defense", "TAS - Accounting and Financial Reporting Advisory", "TAS - Due Diligence-Buyside", "TAS - Due Diligence-Sellside", "TAS - DVC Business Analytics", "TAS - DVC Decision Modeling","TAS - ESG Due Diligence & Analytics","TAS - Lender Services", "TAS - Tax", "TAS - Tech & Cyber", "Tech+IP - Buyside", "Tech+IP - Patent Acquisition Support", "Tech+IP - Patent Sales", "Tech+IP - Strategic Advisory", "Tech+IP - Tech+IP Sales", "Tech+IP - Valuation", "Valuation Advisory" };
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
            string[] expectedValue = { "Advisory", "Transaction Opinions", "Buyside", "Capital Markets", "Other", "Portfolio Valuation & Advisory", "Other", "Advisory", "Financial Restructuring - Creditor / Debtor", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "CVAS - Corporate Valuation Advisory Services", "Capital Markets", "Capital Markets", "Financial Restructuring - Creditor / Debtor", "Advisory", "Financial Restructuring - Other", "Financial Restructuring - Other", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Dispute", "Financial Restructuring - Other", "Capital Markets", "CVAS - Corporate Valuation Advisory Services", "Dispute", "Other", "Portfolio Valuation & Advisory", "Transaction Opinions", "Other", "Advisory", "Transaction Opinions", "Other", "Dispute", "Fund Opinions", "Fund Opinions", "Fund Opinions", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Portfolio Valuation & Advisory", "Transaction Opinions", " ", "Capital Markets", "Other", "Transaction Opinions", "Advisory", "Advisory", "Advisory", "Other", "Other", "Capital Markets", "Capital Markets", "Financial Restructuring - Other", "Dispute", "Sellside", "Advisory", "Capital Markets", "Financial Restructuring - Other", " ", "Advisory", "Private Funds Advisory", "Private Funds Advisory", "Private Funds Advisory", "Private Funds Advisory", "Advisory", "Financial Restructuring - Other", "Other", "Sellside", "Transaction Opinions", " ", "Advisory", "Advisory", "Buyside", "Sellside", "Advisory", "Strategic Consulting", "Advisory", "Capital Markets", "Tech+IP Advisory", "Tech+IP Advisory", "Advisory", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "TAS - Due Diligence Services", "Advisory", "Tech+IP Advisory", "Advisory", "Tech+IP Advisory", "Advisory", "Tech+IP Advisory", "Advisory" };

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
            try
            {
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, _TabEle("'Close " + name + "'"), 10);
                IWebElement closeTabIcon = driver.FindElement(_TabEle("'Close " + name + "'"));
                closeTabIcon.Click();
                Thread.Sleep(2000);
            }
            catch 
            { 
                //tab already closed
             }
            
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
        
        //To Search Opportunity with Opportunity Name
        public void SelectListView(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconListViewPicker, 20);
            driver.FindElement(iconListViewPicker).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, iconListViewPicker, 20);
            driver.FindElement(_optionListView(name)).Click();
            Thread.Sleep(10000);
        }
        public string GetLVMessagePopup()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, msgLVPopup, 30);
                return driver.FindElement(msgLVPopup).Text;
            }
            catch { return "No popup displayed"; }
        }
        
        public void WaitForPageLoaderLV()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            Thread.Sleep(2000);
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
        }
        public void SelectListViewLV(string name)
        {
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, iconListViewPicker, 20);
            driver.FindElement(iconListViewPicker).Click();
            Thread.Sleep(2000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, _optionListView(name), 10);
                driver.FindElement(_optionListView(name)).Click();
            }
            catch
            {
                driver.FindElement(_optionListViewL(name)).Click();
            }
            try
            {
                WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            }
            catch { Thread.Sleep(4000); }
            Thread.Sleep(4000);
        }

        private By _eleJobType(string name)
        {
            return By.XPath($"//div[contains(@class,'listViewContainer')]//table//tbody//th//a[@title='{name}']");
        }
        private By _eleLegalEntity(string name)
        {
            return By.XPath($"//div[contains(@class,'listViewContainer')]//table//tbody//td//a[@title='{name}']");
        }
        private By _quickEleLegalEntity(string name)
        {
            return By.XPath($"//div[contains(@class,'listViewContainer')]//table//tbody//th//a[@title='{name}']");
        }

        By txtPageHeader = By.XPath("//h1//lightning-formatted-text");
        public string SelectJobTypesLV(string name)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            Thread.Sleep(5000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, _eleJobType(name), 10);
            }
            catch
            {
                this.SelectListViewLV("All");
            ReTry: try
                {
                    
                    WebDriverWaits.WaitUntilEleVisible(driver, _eleJobType(name), 10);
                    CustomFunctions.MoveToElement(driver, driver.FindElement(_eleJobType(name)));
                }
                catch
                {
                    js.ExecuteScript("window.scrollTo(0,500)");
                    Thread.Sleep(2000);
                    goto ReTry;
                }
            }
            driver.FindElement(_eleJobType(name)).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtPageHeader, 20);
            string pageheader = driver.FindElement(txtPageHeader).Text;
            return pageheader;
        }
        public string SelectLegalEntityLV(string name)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            Thread.Sleep(5000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, _quickEleLegalEntity(name), 10);
                driver.FindElement(_quickEleLegalEntity(name)).Click();
            }
            catch
            {
            ReTry: try
                {
                    this.SelectListViewLV("All Legal Entities");
                    WebDriverWaits.WaitUntilEleVisible(driver, _eleLegalEntity(name), 10);
                    CustomFunctions.MoveToElement(driver, driver.FindElement(_eleLegalEntity(name)));
                    driver.FindElement(_eleLegalEntity(name)).Click();
                }
                catch
                {
                    js.ExecuteScript("window.scrollTo(0,800)");
                    Thread.Sleep(2000);
                    goto ReTry;
                }
            }              
            
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtPageHeader, 20);
            string pageheader = driver.FindElement(txtPageHeader).Text;
            return pageheader;
        }

       
        public string GetJobTypeProductLineLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtProductLineL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtProductLineL));
            return driver.FindElement(txtProductLineL).Text;
        }
        public string GetERPProductTypeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProductTypeL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPProductTypeL));
            string type = driver.FindElement(valERPProductTypeL).Text;
            return type;
        }
        public string GetERPProductTypeCodeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProductTypCodeL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPProductTypCodeL));
            string code = driver.FindElement(valERPProductTypCodeL).Text;
            return code;
        }
        public string GetJobTypeProductTypeCodeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtProductTypeCodeL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtProductTypeCodeL));
            return driver.FindElement(txtProductTypeCodeL).Text;
        }
        
        public void UpdateERPSyncManuallyInlineLV()
        {
            //Thread.Sleep(60000);
            DateTime Time = DateTime.Now;
            string syncDate = Time.ToString("MM/dd/yyyy");
            string syncTime = Time.ToString("hh:mm:ss tt");
            WebDriverWaits.WaitUntilEleVisible(driver, iconInlineEditERPSubmittedToSyncL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(iconInlineEditERPSubmittedToSyncL));
            driver.FindElement(iconInlineEditERPSubmittedToSyncL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, textDatePickerL, 20);
            driver.FindElement(textDatePickerL).Click();
            Thread.Sleep(2000);
            driver.FindElement(textDatePickerL).Clear();
            CustomFunctions.MoveToElement(driver, driver.FindElement(textDatePickerL));
            driver.FindElement(textDatePickerL).SendKeys(syncDate);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtTimePickerL));
            driver.FindElement(txtTimePickerL).Click();
            Thread.Sleep(2000);
            driver.FindElement(txtTimePickerL).Clear();
            driver.FindElement(txtTimePickerL).SendKeys(syncTime);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(10000);
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
        }
        public string GetERPSubmittedToSyncLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPSubmittedToSyncL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPSubmittedToSyncL));
            string syncDate = driver.FindElement(valERPSubmittedToSyncL).Text;
            return syncDate;
        }
        public string GetERPLastIntegrationResponseDateLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntegrationResponseDateL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPLastIntegrationResponseDateL));
            string date = driver.FindElement(valERPLastIntegrationResponseDateL).Text;
            return date;
        }
        public string GetERPLastIntegrationStatusLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntStatusL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPLastIntStatusL));
            string status = driver.FindElement(valERPLastIntStatusL).Text;
            return status;
        }       
        public string GetERPUpdateDFFCheckboxStatusLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkERPUpdateDFFL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(checkERPUpdateDFFL));
            bool Enabled = driver.FindElement(checkERPUpdateDFFL).Selected;
            if (Enabled)
            {
                return "Checkbox is checked";
            }
            else
            {
                return "Checkbox is not checked";
            }
        }
        public string GetPrimaryOfficeLV()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valAdminPrimaryOfficeL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valAdminPrimaryOfficeL));
            string value = driver.FindElement(valAdminPrimaryOfficeL).Text;
            return value;
        }
        public string GetHLSectorIDLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtHLSectorIDL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtHLSectorIDL));
            return driver.FindElement(txtHLSectorIDL).Text;
        }

        public string GetHLSectorComboLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtHLSectorComboL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtHLSectorComboL));
            return driver.FindElement(txtHLSectorComboL).Text;
        }
        public string GetJobTypeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtJobTypeL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtJobTypeL));
            return driver.FindElement(txtJobTypeL).Text;
        }
        public string GetRecordTypeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRecordTypeL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valRecordTypeL));
            return driver.FindElement(valRecordTypeL).Text;
        }        
        
        //Get ERP Principal Manager ID
        public string GetERPEmailIDLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPEmailIDL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPEmailIDL));
            string id = driver.FindElement(valERPEmailIDL).Text;
            return id;
        }
        public string GetERPIDLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPIDL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPIDL));
            string id = driver.FindElement(valERPIDL).Text;
            return id;
        }

        //Get ERP Project Status Code in ERP section
        public string GetERPProjStatusCodeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjStatusCodeL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPProjStatusCodeL));
            string code = driver.FindElement(valERPProjStatusCodeL).Text;
            return code;
        }

        //Get ERP Project Number in ERP section
        public string GetERPProjectNumberLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjectNumberL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPProjectNumberL));
            string Number = driver.FindElement(valERPProjectNumberL).Text;
            return Number;
        }

        //Get ERP Project Name in ERP section
        public string GetERPProjectNameLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjectNameL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPProjectNameL));
            string Number = driver.FindElement(valERPProjectNameL).Text;
            return Number;
        }

        //Get ERP LOB in ERP section
        public string GetERPLOBLV()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLOBL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPLOBL));
            string Number = driver.FindElement(valERPLOBL).Text;
            return Number;
        }
        public string GetLOBLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLOBL);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valLOBL));
            string LOB = driver.FindElement(valLOBL).Text;
            return LOB;
        }

        //Get ERP Template
        public string GetERPTemplateLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPTemplateL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPTemplateL));
            string number = driver.FindElement(valERPTemplateL).Text;
            return number;
        }

        //Get ERP Business Unit ID
        public string GetERPBusinessUnitIDLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPUnitIDL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPUnitIDL));
            string number = driver.FindElement(valERPUnitIDL).Text;
            return number;
        }

        //Get ERP Business Unit
        public string GetERPBusinessUnitLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPUnitL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPUnitL));
            string unit = driver.FindElement(valERPUnitL).Text;
            return unit;
        }

        //Get ERP Legal Entity ID
        public string GetERPLegalEntityIDLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityIDL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPLegalEntityIDL));
            string id = driver.FindElement(valERPLegalEntityIDL).Text;
            return id;
        }

        //Get ERP Entity Code
        public string GetERPEntityCodeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPEntityCodeL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPEntityCodeL));
            string code = driver.FindElement(valERPEntityCodeL).Text;
            return code;
        }

        //Get ERP Legislation Code
        public string GetERPLegCodeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegCodeL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPLegCodeL));
            string code = driver.FindElement(valERPLegCodeL).Text;
            return code;
        }

        //Get ERP Industry Group
        public string GetERPIndustryGroupLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPIGL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPIGL));
            string IG = driver.FindElement(valERPIGL).Text;
            return IG;
        }
        //Get HL Entity in ERP section
        public string GetHLEntityLV()
        {
            //driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valHLEntityL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valHLEntityL));
            string entity = driver.FindElement(valHLEntityL).Text;
            return entity;
        }

        //Get ERP HL Entity in ERP section
        public string GetERPHLEntityLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPHLEntityL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPHLEntityL));
            string ERPEntity = driver.FindElement(valERPHLEntityL).Text;
            return ERPEntity;
        }

        //Get ERP Legal Entity in ERP section
        public string GetERPLegalEntityLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPHLEntityL));
            string ERPEntity = driver.FindElement(valERPLegalEntityL).Text;
            return ERPEntity;
        }
        public string GetERPIntegrationErrorLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPErrorL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPErrorL));
            string error = driver.FindElement(valERPErrorL).Text;
            return error;
        }
        public void ReloadPage()
        {
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
        }

        public string GetJobCodeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valJobCodeL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valJobCodeL));
            string jobCode = driver.FindElement(valJobCodeL).Text;
            return jobCode;
        }
        public void DetailPageFullViewLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);
            try
            {
                js.ExecuteScript("window.scrollTo(0,0)");
                WebDriverWaits.WaitUntilEleVisible(driver, tabFullViewL, 10);
                driver.FindElement(tabFullViewL).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconHeaderMoreTabsL, 10);
                driver.FindElement(iconHeaderMoreTabsL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, tabMoreFullViewL, 10);
                driver.FindElement(tabMoreFullViewL).Click();
            }
            Thread.Sleep(10000);
        }

        public bool GetVerballyEngCheckboxStatusLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            WebDriverWaits.WaitUntilEleVisible(driver, chkVerballyEngL, 20);
            Thread.Sleep(2000);
            return driver.FindElement(chkVerballyEngL).Selected;
        }

        public string GetPopUpMessagelV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 10);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            return toasMsg;
        }

        public void ClickTabOracleERPLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);
            try
            {
                try
                {
                    js.ExecuteScript("window.scrollTo(0,0)");
                    WebDriverWaits.WaitUntilEleVisible(driver, tabOracleERPL, 5);
                    driver.FindElement(tabOracleERPL).Click();
                }
                catch(Exception e)
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, iconHeaderMoreTabsL, 5);
                    driver.FindElement(iconHeaderMoreTabsL).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, tabMoreOracleERPL, 5);
                    driver.FindElement(tabMoreOracleERPL).Click();
                }
            }
            catch { }
            Thread.Sleep(5000);
        }
    }
}
