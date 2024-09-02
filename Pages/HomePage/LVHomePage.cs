using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using RazorEngine.Compilation.ImpromptuInterface.Optimization;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static MongoDB.Driver.WriteConcern;

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
        By appLauncher = By.XPath("//button[@title='App Launcher']/div[@class='slds-icon-waffle']");// button[class*='slds-icon-waffle_container'] div.slds-icon-waffle");
        By appHeader = By.XPath("//div//h1[contains(@class,'appName')]/span");//CssSelector("div.slds-context-bar__label-action .slds-truncate");
        By menuNavigation = By.CssSelector("button[title = 'Show Navigation Menu']");
        By avaiableModules = By.XPath("//div[@id='navMenuList']/div/ul/li/div/*/*/span");
        By lblTearsheetHeading = By.XPath("//h1");

        By lblTabHeading = By.XPath("//a[@class='slds-context-bar__label-action slds-p-left--xx-small']/span");
        By lblTabTitle = By.XPath("(//span[@class='title slds-truncate'])[1]");

        //HomePage Dashboard
        By homePageH1Heading = By.XPath("//div[@class='dash-title']/div/div/h1");
        By activitiesFilter = By.XPath("//span[text()='Activities']");
        By myCoverageTab = By.XPath("//span[text()='My Coverage']");
        By dropdownStartDateFilter = By.XPath("(//div[@class='selected-values'])[6]");
        By lblNoRecords = By.XPath("//span[text()='No results found']");
        By dropdownStartDateFilter1 = By.XPath("(//div[@class='selected-values'])[9]");
        By lblKPITotal = By.XPath("//div[text()='Total']");
        By lblTotalRecords = By.XPath("//div[text()='Total']/../../div/div/div");

        By lblKPIMeetings = By.XPath("//div[text()='Meetings']");
        By lblMeetingRecords = By.XPath("//div[text()='Meetings']/../../div/div/div");
        By linkKPIMeetingsViewDetails = By.XPath("(//span[text()='View Details'])[1]/..");

        By lblKPICalls = By.XPath("//div[text()='Calls']");
        By lblCallRecords = By.XPath("//div[text()='Calls']/../../div/div/div");
        By linkKPICallsViewDetails = By.XPath("(//span[text()='View Details'])[2]/..");

        By lblKPIEmailsTasks = By.XPath("//div[text()='Emails/Tasks ']");
        By lblEmailRecords = By.XPath("//div[text()='Emails/Tasks ']/../../div/div/div");
        By linkKPIEmailsViewDetails = By.XPath("(//span[text()='View Details'])[3]/..");

        By lblKPIOthers = By.XPath("//div[text()='Others']");
        By lblOtherRecords = By.XPath("//div[text()='Others']/../../div/div/div");
        By linkKPIOthersViewDetails = By.XPath("(//span[text()='View Details'])[4]/..");

        By lblKPIMissingNotes = By.XPath("//div[text()='Missing Notes']");
        By lblMissingNoteRecords = By.XPath("//div[text()='Missing Notes']/../../div/div/div");
        By linkKPIMissingNotesViewDetails = By.XPath("(//span[text()='View Details'])[5]/..");

        //General
        By linkSwitchToClassic = By.XPath("//a[text()='Switch to Salesforce Classic']");
        By dropdownSearchAll = By.XPath("//input[@data-value='Search: All']");
        By linkContactsInSearchAllDropDown = By.XPath("//lightning-base-combobox-item[@data-value='FILTER:Contact:Contacts']");
        By linkCompaniesInSearchAllDropDown = By.XPath("//lightning-base-combobox-item[@data-value='FILTER:Account:Companies']");
        By imgSpinningLoader = By.XPath("//div[@class='loading']");
        By pageHeaderEle = By.XPath("//lst-breadcrumbs//span");
        By activitiesTab = By.XPath("//span[text()='Activities']");
        By dropdownActiviyFilter = By.XPath("(//div[@class='selected-values'])[1]");
        By dropdownActivityFilterOptions = By.XPath("//section[contains(@class,'dropdown-menu')]//div[@class='rows']//div[2]/div/div[@class='content string']");
        By myActivityTab = By.XPath("//span[text()='My Activity']");
        By dropdownActivityStartDateFilter = By.XPath("(//div[@class='selected-values'])[3]");
        By dropdownViewDetailsActivityStartDateFilter = By.XPath("(//div[@class='selected-values'])[6]");

        private By _appInAppLauncher(string appName)
        {
            return By.XPath($"//h3[text()='Apps']/following::div/*/span/p/b[text()='{appName}']");

        }

        //Activity Homepage
        By txtActionMenu = By.XPath("//div[contains(@class,'actions-menu-dropdown')]//ul//li//a//span[2]");
        By btnOpenRecord = By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[5]//th[7]//button[contains(@class,'actions-icon')]");//tr[2] 
        By linkOpenRecord = By.XPath("//div[contains(@class,'actions-menu-dropdown')]//ul//li//a[@role='menuitem']");
        By eleHomepageActivitySubject = By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[5]//th[7]//button[1]");//tr[2] 
        By eleDetailPageActivitySubject = By.XPath("//span[text()='Subject']//parent::div/div/div");
        By eleHomepageMeetingCallNotes = By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[5]//th[9]");//tr[2]     
        By btnHomePageActivityViewUpdateNotes = By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[5]//th[9]//button[contains(@class,'actions-icon')]");
        By txtboxNotes = By.XPath("//div[contains(@class,'container EDIT')]//section//textarea[@role='textbox']");
        By btnSaveNotes = By.XPath("//div[contains(@class,'modal-footer')]//button[contains(@class,'ShareButton')]");
        By msgLVPopup = By.CssSelector("span.toastMessage.forceActionsText");
        By frameActvityDashboard = By.XPath("//iframe[@title='Analytics']");
        By optionsActivityFilter = By.XPath("//div[@class='css-1mvcrrm']//div[contains(@class,'row searchableTable')]/div[2]/div/div");
        By elmTableColumns = By.XPath("//table[@role='grid']/tbody/tr[1]/th");
        private By _optionsActiveFilter(int index)
        {
            return By.XPath($"//div[@class='css-1mvcrrm']//div[{index}][contains(@class,'row searchableTable')]//input");
        }
        private By _columnActivityTable(int index)
        {
            return By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[1]/th[{index}]//button/span/span");
        }
        private By _listViewActivityFilter(int index)
        {
            return By.XPath($"//div[@class='css-1mvcrrm']//div[contains(@class,'row searchableTable')][{index}]/div[2]/div/div");
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
                    //try
                    //{
                    //    WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
                    //}
                    //catch { Thread.Sleep(5000); }
                    Thread.Sleep(5000);
                    break;
                }
            }
        }

        public void ClickAppLauncher()
        {
            Thread.Sleep(2000);
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
        public void SelectAppLV(string appName)
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, appHeader, 30);
            string defaultApp = driver.FindElement(appHeader).Text;
            if(defaultApp==appName)
            {
                //No need to select app again as desired aap is already selected
            }
            else
            {                
                WebDriverWaits.WaitUntilEleVisible(driver, appLauncher, 30);
                driver.FindElement(appLauncher).Click();
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, txtSearchItems, 30);
                driver.FindElement(txtSearchItems).SendKeys(appName);
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, _appInAppLauncher(appName), 60);
                driver.FindElement(_appInAppLauncher(appName)).Click();
                Thread.Sleep(3000);
            } 
        }

        public string GetAppName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, appHeader, 30);
            return driver.FindElement(appHeader).Text;
        }

        public void SearchContactFromMainSearch(string name)
        {
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnMainSearch, 120);
            driver.FindElement(btnMainSearch).Click();
            Thread.Sleep(3000);

            driver.FindElement(dropdownSearchAll).Click();
            WebDriverWaits.WaitUntilEleVisible(driver,linkContactsInSearchAllDropDown,120);
            driver.FindElement(linkContactsInSearchAllDropDown).Click();
            Thread.Sleep(5000);

            driver.FindElement(txtMainSearch).SendKeys(name);
            Thread.Sleep(3000);
            driver.FindElement(txtMainSearch).SendKeys(Keys.Enter);
            Thread.Sleep(8000);

            driver.FindElement(By.XPath($"(//a[@title='{name}'])[1]")).Click();
            Thread.Sleep(5000);
        }

        public void SearchCompanyFromMainSearch(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnMainSearch, 120);
            driver.FindElement(btnMainSearch).Click();
            Thread.Sleep(5000);

            driver.FindElement(dropdownSearchAll).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkCompaniesInSearchAllDropDown, 120);
            driver.FindElement(linkCompaniesInSearchAllDropDown).Click();
            Thread.Sleep(5000);

            driver.FindElement(txtMainSearch).SendKeys(name);
            driver.FindElement(txtMainSearch).SendKeys(Keys.Enter);
            Thread.Sleep(5000);

            driver.FindElement(By.XPath($"(//a[@title='{name}'])[2]")).Click();
            Thread.Sleep(5000);
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
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, linkLogout, 20);
                driver.FindElement(linkLogout).Click();
                Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                driver.Navigate().Refresh();
                Thread.Sleep(4000);
                WebDriverWaits.WaitUntilEleVisible(driver, linkLogout, 20);
                driver.FindElement(linkLogout).Click();
                Thread.Sleep(2000);
            }
        }

        public void LogoutFromSFLightningAsApprover()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, userImage, 120);
            driver.FindElement(userImage).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkLogOut, 120);
            driver.FindElement(linkLogOut).Click();
            Thread.Sleep(10000);
        }

        public void SwitchBackToClassicView()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver,userImage,120);
            driver.FindElement(userImage).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver,linkSwitchToClassic,120);
            driver.FindElement(linkSwitchToClassic).Click();
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

        public bool VerifyThereExistContactsOptionAsANavigationalItemOnHLBanker()
        {
            bool result = false;

            WebDriverWaits.WaitUntilEleVisible(driver, menuNavigation, 120);
            driver.FindElement(menuNavigation).Click();
            Thread.Sleep(5000);

            IList<IWebElement> elements = driver.FindElements(By.XPath("//ul[@aria-label='Navigation Menu']/li"));
            int size = elements.Count;

            for (int items = 1; items <= size; items++)
            {
                By linkContacts = By.XPath($"//ul[@aria-label='Navigation Menu']/li[{items}]/div/a");

                WebDriverWaits.WaitUntilEleVisible(driver, linkContacts, 120);
                string itemName = driver.FindElement(linkContacts).GetAttribute("data-label");

                if (itemName == "Contacts")
                {
                    driver.FindElement(linkContacts).Click();
                    Thread.Sleep(5000);

                    result = true;
                    break;
                }
            }

            return result;
        }

        public bool VerifyUserNavigatedToRecentlyViewedContactsPage()
        {
            bool result = false;

            WebDriverWaits.WaitUntilEleVisible(driver, lblTabHeading, 120);
            Thread.Sleep(5000);

            if (driver.FindElement(lblTabHeading).Text == "Contacts")
            {
                result = true;
            }
            return result;
        }           

        public bool IsActivitiesTabAvailable()
        {
            bool result = false;
            driver.SwitchTo().Frame("asset-My_Homepage");
            WebDriverWaits.WaitUntilEleVisible(driver, activitiesTab, 20);
            if (driver.FindElement(activitiesTab).Displayed)
            {
                result = true;
            }
            return result;
        }
        
        public bool AreFilterOptionsCorrectOnActivityDashboard(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            //Thread.Sleep(3000);
            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownActivityStartDateFilter, 20);
            if (driver.FindElement(dropdownActivityStartDateFilter).Displayed)
            {
                driver.FindElement(dropdownActivityStartDateFilter).Click();
                Thread.Sleep(2000);
                int recordCount = driver.FindElements(optionsActivityFilter).Count;
                int excelCount = ReadExcelData.GetRowCount(excelPath, "StartDateFilterOptions");

                for (int i = 2; i <= excelCount; i++)
                {
                    string exlListViewValue = ReadExcelData.ReadDataMultipleRows(excelPath, "StartDateFilterOptions", i, 1);

                    for (int j = 1; j <= recordCount; j++)
                    {
                        string sfListViewValue = driver.FindElement(_listViewActivityFilter(j)).Text;
                        if (exlListViewValue == sfListViewValue)
                        {
                            result = true;
                            break;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    continue;
                }
            }
            driver.FindElement(dropdownActivityStartDateFilter).Click();
            //Thread.Sleep(3000);
            return result;
        }
        public bool IsMyActivityTabDisplayedUnderActivities()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, activitiesTab, 20);
            driver.FindElement(activitiesTab).Click();
            Thread.Sleep(5000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, myActivityTab, 20);
                return driver.FindElement(myActivityTab).Displayed;
            }
            catch (Exception ex) { return false; }
        }
        
        public bool AreAvailableColumnsInActivitiesTableCorrectOnMyActivityDashboard(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            bool result = false;
            //Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownActivityStartDateFilter, 10);
            driver.FindElement(dropdownActivityStartDateFilter).Click();
            Thread.Sleep(2000);

            //Get filter count
            int filterCount = driver.FindElements(optionsActivityFilter).Count;

            for (int i = 1; i <= filterCount; i++)
            {
                driver.FindElement(_optionsActiveFilter(i)).Click();
                Thread.Sleep(5000);

                //Get selected Filter value
                string selectedFilterValue = driver.FindElement(dropdownActivityStartDateFilter).Text;

                switch (selectedFilterValue)
                {
                    case "Last 7 Days":
                        bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent == true)
                        {
                            driver.FindElement(dropdownActivityStartDateFilter).Click();
                            continue;
                        }
                        break;
                    case "Last 30 Days":
                        bool recPresent1 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent1 == true)
                        {
                            driver.FindElement(dropdownActivityStartDateFilter).Click();
                            continue;
                        }
                        break;
                    case "Last 3 Months":
                        bool recPresent2 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent2 == true)
                        {
                            driver.FindElement(dropdownActivityStartDateFilter).Click();
                            continue;
                        }
                        break;
                    case "Last 6 Months":
                        bool recPresent3 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent3 == true)
                        {
                            driver.FindElement(dropdownActivityStartDateFilter).Click();
                            continue;
                        }
                        break;
                    case "Last 12 Months":
                        bool recPresent4 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent4 == true)
                        {
                            driver.FindElement(dropdownActivityStartDateFilter).Click();
                            continue;
                        }
                        break;
                    //for System Admin Only 
                    case "Next 7 Days":
                        bool recPresent5 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent5 == true)
                        {
                            driver.FindElement(dropdownActivityStartDateFilter).Click();
                            continue;
                        }
                        break;
                    case "Next 30 Days":
                        bool recPresent6 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent6 == true)
                        {
                            driver.FindElement(dropdownActivityStartDateFilter).Click();
                            continue;
                        }
                        break;
                    case "Next 3 Months":
                        bool recPresent7 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent7 == true)
                        {
                            driver.FindElement(dropdownActivityStartDateFilter).Click();
                            continue;
                        }
                        break;
                    case "Next 6 Months":
                        bool recPresent8 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent8 == true)
                        {
                            driver.FindElement(dropdownActivityStartDateFilter).Click();
                            continue;
                        }
                        break;
                    case "Next 12 Months":
                        bool recPresent9 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent9 == true)
                        {
                            driver.FindElement(dropdownActivityStartDateFilter).Click();
                            continue;
                        }
                        break;
                }
                Thread.Sleep(3000);
                driver.FindElement(dropdownActivityStartDateFilter).Click();
                break;
            }
            driver.FindElement(dropdownActivityStartDateFilter).Click();
            int excelCount = ReadExcelData.GetRowCount(excelPath, "ActivityColumns");
            int recordCount = driver.FindElements(elmTableColumns).Count;

            for (int i = 2; i <= excelCount; i++)
            {
                string exlColValue = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityColumns", i, 1);
                //Thread.Sleep(2000);
                for (int j = 1; j <= recordCount; j++)
                {
                    string sfColValue = driver.FindElement(_columnActivityTable(j)).Text;                    
                    if (exlColValue == sfColValue)
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        result = false;
                    }
                }
                continue;
            }            
            return result;
        }

        private By _btnCloseActiveTab(string tabName)
        {
            return By.XPath($"//span[text()='My Activity - {tabName}, Close tab']/../../button"); 
            
        }
        By lblActivityKPITotal = By.XPath("//dd[text()='Total']");
        By lblActivityTotalRecords = By.XPath("//dd[text()='Total']//ancestor::dl//dt/div");

        By lblActivityKPIMeetings = By.XPath("//dd[text()='Meetings']");
        By lblActivityMeetingRecords = By.XPath("//dd[text()='Meetings']//ancestor::dl//dt/div");
        By lblActivityMeetingRecordsZero = By.XPath("//dd[text()='Meetings']//ancestor::dl//dt");
        By linkActivityKPIMeetingsViewDetails = By.XPath("(//dd[text()='Meetings']/following::div/following::div[5][@class='link']/span)[1]");

        By lblActivityKPICalls = By.XPath("//dd[text()='Calls']");
        By lblActivityCallRecords = By.XPath("//dd[text()='Calls']//ancestor::dl//dt/div");
        By lblActivityCallRecordsZero = By.XPath("//dd[text()='Calls']//ancestor::dl//dt");
        By linkActivityKPICallsViewDetails = By.XPath("(//dd[text()='Calls']/following::div/following::div[5][@class='link']/span)[1]");

        By lblActivityKPIEmailsTasks = By.XPath("//dd[text()='Emails/Tasks ']");
        By lblActivityEmailRecords = By.XPath("//dd[text()='Emails/Tasks ']//ancestor::dl//dt/div");
        By lblActivityEmailRecordsZero = By.XPath("//dd[text()='Emails/Tasks ']//ancestor::dl//dt");
        By linkActivityKPIEmailsViewDetails = By.XPath("(//dd[text()='Emails/Tasks ']/following::div/following::div[5][@class='link']/span)[1]");

        By lblActivityKPIOthers = By.XPath("//dd[text()='Other']");//Others
        By lblActivityOtherRecords = By.XPath("//dd[text()='Other']//ancestor::dl//dt/div");
        By lblActivityOtherRecordsZero = By.XPath("//dd[text()='Other']//ancestor::dl//dt");
        By linkActivityKPIOthersViewDetails = By.XPath("(//dd[text()='Other']/following::div/following::div[5][@class='link']/span)[1]");

        By lblActivityKPIMissingNotes = By.XPath("//dd[text()='Missing Notes']");
        By lblActivityMissingNoteRecords = By.XPath("//dd[text()='Missing Notes']//ancestor::dl//dt/div");
        By lblActivityMissingNoteRecordsZero = By.XPath("//dd[text()='Missing Notes']//ancestor::dl//dt");
        By linkActivityKPIMissingNotesViewDetails = By.XPath("(//dd[text()='Missing Notes']/following::div/following::div[5][@class='link']/span)[1]");

        private By _eleActivityFilterOption(int index)
        {
            return By.XPath($"//div[@class='css-1mvcrrm']/div/div/div[3]/div/div[{index}]/div[1]//input");

        }
        By tableActivities = By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]");
        private By _elmActivityStartDate(int rowIndex)
        {
            return By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{rowIndex}]/th[4]/div/div");
        }
        public bool IsKPIMetricesCorrectOnMyActivitiyDashboard(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;
            int excelCount = ReadExcelData.GetRowCount(excelPath, "KPIMetrics");
            string lblKPI1 = driver.FindElement(lblActivityKPITotal).Text;
            string lblKPI2 = driver.FindElement(lblActivityKPIMeetings).Text;
            string lblKPI3 = driver.FindElement(lblActivityKPICalls).Text;
            string lblKPI4 = driver.FindElement(lblActivityKPIEmailsTasks).Text;
            string lblKPI5 = driver.FindElement(lblActivityKPIOthers).Text;
            string lblKPI6 = driver.FindElement(lblActivityKPIMissingNotes).Text;

            for (int i = 2; i <= excelCount; i++)
            {
                string exlKPIValue = ReadExcelData.ReadDataMultipleRows(excelPath, "KPIMetrics", i, 1);
                if (exlKPIValue == lblKPI1 || exlKPIValue == lblKPI2 || exlKPIValue == lblKPI3 || exlKPIValue == lblKPI4 || exlKPIValue == lblKPI5 || exlKPIValue == lblKPI6)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool IsActivityListSortedWithSelectedActivityStartDateFilterOnMyActivityDashboard()
        {
            bool overallResult = false;
            bool result1 = false;
            bool result2 = false;
            bool result3 = false;
            bool result4 = false;
            bool result5 = false;
            bool result6 = false;
            bool result7 = false;
            bool result8 = false;
            bool result9 = false;
            bool result10 = false;

            WebDriverWaits.WaitUntilEleVisible(driver, dropdownActivityStartDateFilter, 20);
            driver.FindElement(dropdownActivityStartDateFilter).Click();
            //Thread.Sleep(2000);

            //Get filter count
            WebDriverWaits.WaitUntilEleVisible(driver, optionsActivityFilter, 10);
            int filterCount = driver.FindElements(optionsActivityFilter).Count;
            DateTime currentDate = DateTime.Today;
            string noOfRows;
            int recordCount;
            for (int i = 1; i <= filterCount; i++)
            {
                driver.FindElement(_eleActivityFilterOption(i)).Click();
                Thread.Sleep(5000);

                //Get selected Filter value
                WebDriverWaits.WaitUntilEleVisible(driver, dropdownActivityStartDateFilter, 10);
                string selectedFilterValue = driver.FindElement(dropdownActivityStartDateFilter).Text;

                switch (selectedFilterValue)
                {
                    case "Last 7 Days":
                        DateTime setDate = currentDate.AddDays(-7);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                //Get the no. of record in table
                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));

                                    if (elePresent == true)
                                    {
                                        WebDriverWaits.WaitUntilEleVisible(driver, _elmActivityStartDate(j), 10);
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime <= currentDate && dateTime >= setDate)
                                        {
                                            result1 = true;
                                        }
                                        else
                                        {
                                            result1 = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                result1 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result1 = true;
                        }
                        break;
                    case "Last 30 Days":
                        DateTime setDate1 = currentDate.AddDays(-30);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                //Get the no. of record in table
                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));
                                    if (elePresent == true)
                                    {
                                        WebDriverWaits.WaitUntilEleVisible(driver, _elmActivityStartDate(j), 10);
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime <= currentDate && dateTime >= setDate1)
                                        {
                                            result2 = true;
                                        }
                                        else
                                        {
                                            result2 = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                result2 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result2 = true;
                        }
                        break;
                    case "Last 3 Months":
                        DateTime setDate2 = currentDate.AddMonths(-3);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/td[4]/div/div"));
                                    if (elePresent == true)
                                    {
                                        WebDriverWaits.WaitUntilEleVisible(driver, _elmActivityStartDate(j), 10);
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/td[4]/div/div")).Text;
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime <= currentDate && dateTime >= setDate2)
                                        {
                                            result3 = true;
                                        }
                                        else
                                        {
                                            result3 = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                result3 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result3 = true;
                        }
                        break;
                    case "Last 6 Months":
                        DateTime setDate3 = currentDate.AddMonths(-6);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div"));
                                    if (elePresent == true)
                                    {
                                        WebDriverWaits.WaitUntilEleVisible(driver, _elmActivityStartDate(j), 10);
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div")).Text;
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime <= currentDate && dateTime >= setDate3)
                                        {
                                            result4 = true;
                                        }
                                        else
                                        {
                                            result4 = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                result4 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result4 = true;
                        }
                        break;
                    case "Last 12 Months":
                        DateTime setDate4 = currentDate.AddMonths(-12);
                        try
                        {

                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                //Get the no. of record in table
                                
                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div"));
                                    if (elePresent == true)
                                    {
                                        WebDriverWaits.WaitUntilEleVisible(driver, _elmActivityStartDate(j), 10);
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div")).Text;
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime <= currentDate && dateTime >= setDate4)
                                        {
                                            result5 = true;
                                        }
                                        else
                                        {
                                            result5 = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                result5 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result5 = true;
                        }
                        break;
                    case "Next 7 Days":
                        DateTime setDate5 = currentDate.AddDays(7);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                           if (recordCount > 1)
                            {
                                //Get the no. of record in table
                               
                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div"));
                                    if (elePresent == true)
                                    {
                                        WebDriverWaits.WaitUntilEleVisible(driver, _elmActivityStartDate(j), 10);
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div")).Text;
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime >= currentDate && dateTime <= setDate5)
                                        {
                                            result6 = true;
                                        }
                                        else
                                        {
                                            result6 = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                result6 = true;
                            }
                        }
                        catch 
                        { 
                            //No Record found fr the selected filter
                             result6 = true; 
                        }                        
                        break;
                    case "Next 30 Days":
                        DateTime setDate6 = currentDate.AddDays(30);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                           if (recordCount > 1)
                            {
                                //Get the no. of record in table
                               
                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div"));
                                    if (elePresent == true)
                                    {
                                        WebDriverWaits.WaitUntilEleVisible(driver, _elmActivityStartDate(j), 10);
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div")).Text;
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime >= currentDate && dateTime <= setDate6)
                                        {
                                            result7 = true;
                                        }
                                        else
                                        {
                                            result7 = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                result7 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result7 = true;
                        }
                        break;
                    case "Next 3 Months":
                        DateTime setDate7 = currentDate.AddMonths(3);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                //Get the no. of record in table
                               
                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div"));
                                    if (elePresent == true)
                                    {
                                        WebDriverWaits.WaitUntilEleVisible(driver, _elmActivityStartDate(j), 10);
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div")).Text;
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime >= currentDate && dateTime <= setDate7)
                                        {
                                            result8 = true;
                                        }
                                        else
                                        {
                                            result8 = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                result8 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result8 = true;
                        }
                        break;
                    case "Next 6 Months":
                        DateTime setDate8 = currentDate.AddMonths(6);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                //Get the no. of record in table
                             
                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));
                                    if (elePresent == true)
                                    {
                                        WebDriverWaits.WaitUntilEleVisible(driver, _elmActivityStartDate(j), 10);
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime >= currentDate && dateTime <= setDate8)
                                        {
                                            result9 = true;
                                        }
                                        else
                                        {
                                            result9 = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                result9 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result9 = true;
                        }
                        break;
                    case "Next 12 Months":
                        DateTime setDate9 = currentDate.AddMonths(12);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                //Get the no. of record in table
                              
                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));
                                    if (elePresent == true)
                                    {
                                        WebDriverWaits.WaitUntilEleVisible(driver, _elmActivityStartDate(j), 10);
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime >= currentDate && dateTime <= setDate9)
                                        {
                                            result10 = true;
                                        }
                                        else
                                        {
                                            result10 = false;
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                result10 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result10 = true;
                        }
                        break;
                }
                WebDriverWaits.WaitUntilEleVisible(driver, dropdownActivityStartDateFilter, 10);
                driver.FindElement(dropdownActivityStartDateFilter).Click();
                //Thread.Sleep(3000);
            }
            driver.FindElement(dropdownActivityStartDateFilter).Click();
            //Thread.Sleep(5000);
            if (result1 == true && result2 == true && result3 == true && result4 == true && result5 == true && result6 == true && result7 == true && result8 == true && result9 == true && result10 == true)
            {
                overallResult = true;
            }
            return overallResult;
        }

        public void NavigateToHomePageTabFromHLBankerDropdown()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, menuNavigation, 120);
            driver.FindElement(menuNavigation).Click();
            Thread.Sleep(5000);

            IList<IWebElement> elements = driver.FindElements(By.XPath("//ul[@aria-label='Navigation Menu']/li"));
            int size = elements.Count;

            for (int items = 1; items <= size; items++)
            {
                By linkHome = By.XPath($"//ul[@aria-label='Navigation Menu']/li[{items}]/div/a");

                WebDriverWaits.WaitUntilEleVisible(driver, linkHome, 120);
                string itemName = driver.FindElement(linkHome).GetAttribute("data-label");

                if (itemName == "Home")
                {
                    driver.FindElement(linkHome).Click();
                    Thread.Sleep(10000);
                    driver.SwitchTo().Frame("asset-My_Homepage");
                    WebDriverWaits.WaitUntilEleVisible(driver, homePageH1Heading, 120);
                    string heading = driver.FindElement(homePageH1Heading).Text;
                    Assert.IsTrue(heading == "My Homepage");
                    break;
                }
            }
        }

        public bool VerifyIfActivitiesFilterGridIsAvailableNextToEngAndOppFilters()
        {
            bool result = false;
            if (driver.FindElement(activitiesFilter).Displayed)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyIfUserCanSeeMyCoverageTabUnderActivitiesFilter()
        {
            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver, activitiesFilter, 120);
            driver.FindElement(activitiesFilter).Click();
            Thread.Sleep(5000);

            if (driver.FindElement(myCoverageTab).Displayed)
            {
                driver.FindElement(myCoverageTab).Click();
                result = true;
            }
            return result;
        }

        public bool VerifyAvailabilityOfStartDateFilterOnActivityDashboard(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter, 120);
            if (driver.FindElement(dropdownStartDateFilter).Displayed)
            {
                driver.FindElement(dropdownStartDateFilter).Click();
                Thread.Sleep(3000);
                int recordCount = driver.FindElements(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div")).Count;
                int excelCount = ReadExcelData.GetRowCount(excelPath, "StartDateFilterOptions");

                for (int i = 2; i <= excelCount; i++)
                {
                    string exlListViewValue = ReadExcelData.ReadDataMultipleRows(excelPath, "StartDateFilterOptions", i, 1);

                    for (int j = 1; j <= recordCount; j++)
                    {
                        string sfListViewValue = driver.FindElement(By.XPath($"//div[@class='css-1vp5y9m']/div/div[3]/div/div[{j}]/div[2]/div/div")).Text;
                        if (exlListViewValue == sfListViewValue)
                        {
                            result = true;
                            break;
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    continue;
                }
            }
            driver.FindElement(dropdownStartDateFilter).Click();
            //Thread.Sleep(3000);
            return result;
        }
        public bool VerifyDefaultValueSelectedInActivityStartDateFilter()
        {
            Thread.Sleep(5000);
            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter, 120);
            if (driver.FindElement(dropdownStartDateFilter).Text == "Last 7 Days")
            {
                result = true;
            }
            return result;
        }

        public string GetDefaultSelectedValueInActivityStartDateFilter()
        {
            //Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownActivityStartDateFilter, 10);
            return driver.FindElement(dropdownActivityStartDateFilter).Text;            
        }

        public bool VerifyFunctionalityOfActivityStartDateGridFilterOnMyCoverageDashboard()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter, 120);
            driver.FindElement(dropdownStartDateFilter).Click();
            Thread.Sleep(5000);

            bool overallResult = false;
            bool result1 = false;
            bool result2 = false;
            bool result3 = false;
            bool result4 = false;
            bool result5 = false;
            bool result6 = false;
            bool result7 = false;
            bool result8 = false;
            bool result9 = false;
            bool result10 = false;

            //Get filter count
            int filterCount = driver.FindElements(optionsActivityFilter).Count;
            DateTime currentDate = DateTime.Today;

            for (int i = 1; i <= filterCount; i++)
            {
                driver.FindElement(By.XPath($"//div[@class='css-1vp5y9m']/div/div[3]/div/div[{i}]/div[1]/div/input")).Click();
                Thread.Sleep(3000);

                //Get selected Filter value
                string selectedFilterValue = driver.FindElement(dropdownStartDateFilter).Text;

                switch (selectedFilterValue)
                {
                    case "Last 7 Days":
                        DateTime setDate = currentDate.AddDays(-7);

                        bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    try
                                    {
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime < currentDate && dateTime >= setDate)
                                        {
                                            result1 = true;
                                        }
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                            }
                        }
                        else
                        {
                            result1 = true;
                        }
                        break;
                    case "Last 30 Days":
                        DateTime setDate1 = currentDate.AddDays(-30);

                        bool recPresent1 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent1 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    try
                                    {
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime < currentDate && dateTime >= setDate1)
                                        {
                                            result2 = true;
                                        }
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                            }
                        }
                        else
                        {
                            result2 = true;
                        }
                        break;
                    case "Last 3 Months":
                        DateTime setDate2 = currentDate.AddMonths(-3);

                        bool recPresent2 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent2 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    try
                                    {
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime < currentDate && dateTime >= setDate2)
                                        {
                                            result3 = true;
                                        }
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                            }
                        }
                        else
                        {
                            result3 = true;
                        }
                        break;
                    case "Last 6 Months":
                        DateTime setDate3 = currentDate.AddMonths(-6);

                        bool recPresent3 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent3 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    try
                                    {
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime < currentDate && dateTime >= setDate3)
                                        {
                                            result4 = true;
                                        }
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                            }
                        }
                        else
                        {
                            result4 = true;
                        }
                        break;
                    case "Last 12 Months":
                        DateTime setDate4 = currentDate.AddMonths(-12);

                        bool recPresent4 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent4 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    try
                                    {
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime < currentDate && dateTime >= setDate4)
                                        {
                                            result5 = true;
                                        }
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                            }
                        }
                        else
                        {
                            result5 = true;
                        }
                        break;
                    case "Next 7 Days":
                        DateTime setDate5 = currentDate.AddDays(7);

                        bool recPresent5 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent5 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    try
                                    {
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime < currentDate && dateTime >= setDate5)
                                        {
                                            result6 = true;
                                        }
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                            }
                        }
                        else
                        {
                            result6 = true;
                        }
                        break;
                    case "Next 30 Days":
                        DateTime setDate6 = currentDate.AddDays(30);

                        bool recPresent6 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent6 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    try
                                    {
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime < currentDate && dateTime >= setDate6)
                                        {
                                            result7 = true;
                                        }
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                            }
                        }
                        else
                        {
                            result7 = true;
                        }
                        break;
                    case "Next 3 Months":
                        DateTime setDate7 = currentDate.AddMonths(3);

                        bool recPresent7 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent7 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    try
                                    {
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime < currentDate && dateTime >= setDate7)
                                        {
                                            result8 = true;
                                        }
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                            }
                        }
                        else
                        {
                            result8 = true;
                        }
                        break;
                    case "Next 6 Months":
                        DateTime setDate8 = currentDate.AddMonths(6);

                        bool recPresent8 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent8 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    try
                                    {
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime < currentDate && dateTime >= setDate8)
                                        {
                                            result9 = true;
                                        }
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                            }
                        }
                        else
                        {
                            result9 = true;
                        }
                        break;
                    case "Next 12 Months":
                        DateTime setDate9 = currentDate.AddMonths(12);

                        bool recPresent9 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent9 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    try
                                    {
                                        DateTime dateTime = DateTime.Parse(activityDate);
                                        if (dateTime < currentDate && dateTime >= setDate9)
                                        {
                                            result10 = true;
                                        }
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                            }
                        }
                        else
                        {
                            result10 = true;
                        }
                        break;
                }

                WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter, 120);
                driver.FindElement(dropdownStartDateFilter).Click();
                Thread.Sleep(5000);
            }

            driver.FindElement(dropdownStartDateFilter).Click();
            Thread.Sleep(5000);

            if (result1 == true && result2 == true && result3 == true && result4 == true && result5 == true && result6 == true && result7 == true && result8 == true && result9 == true && result10 == true)
            {
                overallResult = true;
            }
            return overallResult;
        }

        public bool VerifyAvailabilityOfKPIMetricesOnMyCoverageDashboard(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;
            int excelCount = ReadExcelData.GetRowCount(excelPath, "KPIMetrics");
            string lblKPI1 = driver.FindElement(lblKPITotal).Text;
            string lblKPI2 = driver.FindElement(lblKPIMeetings).Text;
            string lblKPI3 = driver.FindElement(lblKPICalls).Text;
            string lblKPI4 = driver.FindElement(lblKPIEmailsTasks).Text;
            string lblKPI5 = driver.FindElement(lblKPIOthers).Text;
            string lblKPI6 = driver.FindElement(lblKPIMissingNotes).Text;

            for (int i = 2; i <= excelCount; i++)
            {
                string exlKPIValue = ReadExcelData.ReadDataMultipleRows(excelPath, "KPIMetrics", i, 1);
                if (exlKPIValue == lblKPI1 || exlKPIValue == lblKPI2 || exlKPIValue == lblKPI3 || exlKPIValue == lblKPI4 || exlKPIValue == lblKPI5 || exlKPIValue == lblKPI6)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool VerifyAvailableColumnsInActivitiesTableOnMyCoverageDashboard(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter, 120);
            driver.FindElement(dropdownStartDateFilter).Click();
            Thread.Sleep(5000);

            //Get filter count
            int filterCount = driver.FindElements(optionsActivityFilter).Count;

            for (int i = 1; i <= filterCount; i++)
            {
                driver.FindElement(By.XPath($"//div[@class='css-1vp5y9m']/div/div[3]/div/div[{i}]/div[1]/div/input")).Click();
                Thread.Sleep(3000);

                //Get selected Filter value
                string selectedFilterValue = driver.FindElement(dropdownStartDateFilter).Text;

                switch (selectedFilterValue)
                {
                    case "Last 7 Days":
                        bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent == true)
                        {
                            driver.FindElement(dropdownStartDateFilter).Click();
                            continue;
                        }
                        break;
                    case "Last 30 Days":
                        bool recPresent1 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent1 == true)
                        {
                            driver.FindElement(dropdownStartDateFilter).Click();
                            continue;
                        }
                        break;
                    case "Last 3 Months":
                        bool recPresent2 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent2 == true)
                        {
                            driver.FindElement(dropdownStartDateFilter).Click();
                            continue;
                        }
                        break;
                    case "Last 6 Months":
                        bool recPresent3 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent3 == true)
                        {
                            driver.FindElement(dropdownStartDateFilter).Click();
                            continue;
                        }
                        break;
                    case "Last 12 Months":
                        bool recPresent4 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent4 == true)
                        {
                            driver.FindElement(dropdownStartDateFilter).Click();
                            continue;
                        }
                        break;
                        // next 7 to 12
                }
                break;
            }

            int excelCount = ReadExcelData.GetRowCount(excelPath, "ActivityColumns");
            int columnCount = driver.FindElements(By.XPath("(//table[@role='grid'])[5]/tbody/tr[1]/th")).Count;

            for (int i = 2; i <= excelCount; i++)
            {
                string exlColValue = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityColumns", i, 1);

                for (int j = 1; j <= columnCount; j++)
                {
                    string sfColValue = driver.FindElement(By.XPath($"(//table[@role='grid'])[5]/tbody/tr[1]/th[{j}]/div/div/div/button/span/span")).Text;
                    if (exlColValue == sfColValue)
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        result = false;
                    }
                }
                continue;
            }
            return result;
        }

        public string GetMeetingTypeActivitiesCount()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblMeetingRecords, 120);
            string meetingCount = driver.FindElement(lblMeetingRecords).Text;
            return meetingCount;
        }

        public bool VerifyFunctionalityOfKPIMetricesOnMyCoverageDashboard(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool overallResult = false;
            bool result1 = false;
            bool result2 = false;
            bool result3 = false;
            bool result4 = false;
            bool result5 = false;
            bool result6 = false;

            WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter, 20);
            driver.FindElement(dropdownStartDateFilter).Click();
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div[3]/div[1]/div/input")).Click();
            Thread.Sleep(3000);

            //Get total no. of KPI
            int excelCount = ReadExcelData.GetRowCount(excelPath, "KPIMetrics");

            for (int i = 2; i <= excelCount; i++)
            {
                string exlKPIValue = ReadExcelData.ReadDataMultipleRows(excelPath, "KPIMetrics", i, 1);
                switch (exlKPIValue)
                {
                    case "Total":
                        string lblKPI1Count = driver.FindElement(lblTotalRecords).Text;

                        if (lblKPI1Count == "0")
                        {
                            Thread.Sleep(2000);
                            bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                            if (recPresent == true)
                            {
                                result1 = true;
                            }
                        }
                        else
                        {
                            //Get the no. of rows in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int rowCount = Convert.ToInt32(noOfRows) - 1;
                            string totalRows = Convert.ToString(rowCount);
                            if (lblKPI1Count == totalRows)
                            {
                                result1 = true;
                            }
                        }

                        break;
                    case "Meetings":
                        string lblKPI2Count = driver.FindElement(lblMeetingRecords).Text;

                        driver.FindElement(linkKPIMeetingsViewDetails).Click();
                        Thread.Sleep(3000);

                        if (lblKPI2Count == "0")
                        {
                            Thread.Sleep(2000);
                            bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                            if (recPresent == true)
                            {
                                result2 = true;
                            }
                        }
                        else
                        {
                            //Select appropriate date filter value
                            WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter1, 120);
                            driver.FindElement(dropdownStartDateFilter1).Click();
                            Thread.Sleep(5000);

                            driver.FindElement(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div[3]/div[1]/div/input")).Click();
                            Thread.Sleep(3000);

                            //Get the no. of rows in table
                            string noOfRows1 = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[3]")).GetAttribute("aria-rowcount");
                            int rowCount1 = Convert.ToInt32(noOfRows1) - 1;
                            string totalRows1 = Convert.ToString(rowCount1);
                            if (lblKPI2Count == totalRows1)
                            {
                                result2 = true;
                            }
                        }

                        driver.FindElement(By.XPath("//span[text()='My Coverage - Meetings, Close tab']/..")).Click();
                        Thread.Sleep(2000);
                        break;
                    case "Calls":
                        string lblKPI3Count = driver.FindElement(lblCallRecords).Text;

                        driver.FindElement(linkKPICallsViewDetails).Click();
                        Thread.Sleep(3000);

                        if (lblKPI3Count == "0")
                        {
                            Thread.Sleep(2000);
                            bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                            if (recPresent == true)
                            {
                                result3 = true;
                            }
                        }
                        else
                        {
                            //Select appropriate date filter value
                            WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter1, 120);
                            driver.FindElement(dropdownStartDateFilter1).Click();
                            Thread.Sleep(5000);

                            driver.FindElement(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div[3]/div[1]/div/input")).Click();
                            Thread.Sleep(3000);

                            //Get the no. of rows in table
                            string noOfRows2 = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[3]")).GetAttribute("aria-rowcount");
                            int rowCount2 = Convert.ToInt32(noOfRows2) - 1;
                            string totalRows2 = Convert.ToString(rowCount2);
                            if (lblKPI3Count == totalRows2)
                            {
                                result3 = true;
                            }
                        }

                        driver.FindElement(By.XPath("//span[text()='My Coverage - Calls, Close tab']/..")).Click();
                        Thread.Sleep(2000);
                        break;
                    case "Emails/Tasks":
                        string lblKPI4Count = driver.FindElement(lblEmailRecords).Text;

                        driver.FindElement(linkKPIEmailsViewDetails).Click();
                        Thread.Sleep(3000);

                        if (lblKPI4Count == "0")
                        {
                            Thread.Sleep(2000);
                            bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                            if (recPresent == true)
                            {
                                result4 = true;
                            }
                        }
                        else
                        {
                            //Select appropriate date filter value
                            WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter1, 120);
                            driver.FindElement(dropdownStartDateFilter1).Click();
                            Thread.Sleep(5000);

                            driver.FindElement(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div[3]/div[1]/div/input")).Click();
                            Thread.Sleep(3000);

                            //Get the no. of rows in table
                            string noOfRows3 = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[3]")).GetAttribute("aria-rowcount");
                            int rowCount3 = Convert.ToInt32(noOfRows3) - 1;
                            string totalRows3 = Convert.ToString(rowCount3);
                            if (lblKPI4Count == totalRows3)
                            {
                                result4 = true;
                            }
                        }

                        driver.FindElement(By.XPath("//span[text()='My Coverage - Emails/Task, Close tab']/..")).Click();
                        Thread.Sleep(2000);
                        break;
                    case "Others":
                        string lblKPI5Count = driver.FindElement(lblOtherRecords).Text;

                        driver.FindElement(linkKPIOthersViewDetails).Click();
                        Thread.Sleep(3000);

                        if (lblKPI5Count == "0")
                        {
                            Thread.Sleep(2000);
                            bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                            if (recPresent == true)
                            {
                                result5 = true;
                            }
                        }
                        else
                        {
                            //Select appropriate date filter value
                            WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter1, 120);
                            driver.FindElement(dropdownStartDateFilter1).Click();
                            Thread.Sleep(5000);

                            driver.FindElement(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div[3]/div[1]/div/input")).Click();
                            Thread.Sleep(3000);

                            //Get the no. of rows in table
                            string noOfRows4 = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[3]")).GetAttribute("aria-rowcount");
                            int rowCount4 = Convert.ToInt32(noOfRows4) - 1;
                            string totalRows4 = Convert.ToString(rowCount4);
                            if (lblKPI5Count == totalRows4)
                            {
                                result5 = true;
                            }
                        }

                        driver.FindElement(By.XPath("//span[text()='My Coverage - Others, Close tab']/..")).Click();
                        Thread.Sleep(2000);
                        break;
                    case "Missing Notes":
                        string lblKPI6Count = driver.FindElement(lblMissingNoteRecords).Text;

                        driver.FindElement(linkKPIMissingNotesViewDetails).Click();
                        Thread.Sleep(3000);

                        if (lblKPI6Count == "0")
                        {
                            Thread.Sleep(2000);
                            bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                            if (recPresent == true)
                            {
                                result6 = true;
                            }
                        }
                        else
                        {
                            //Select appropriate date filter value
                            WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter1, 120);
                            driver.FindElement(dropdownStartDateFilter1).Click();
                            Thread.Sleep(5000);

                            driver.FindElement(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div[3]/div[1]/div/input")).Click();
                            Thread.Sleep(3000);

                            //Get the no. of rows in table
                            string noOfRows5 = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[3]")).GetAttribute("aria-rowcount");
                            int rowCount5 = Convert.ToInt32(noOfRows5) - 1;
                            string totalRows5 = Convert.ToString(rowCount5);
                            if (lblKPI6Count == totalRows5)
                            {
                                result6 = true;
                            }
                        }

                        driver.FindElement(By.XPath("//span[text()='My Coverage - No notes, Close tab']/..")).Click();
                        Thread.Sleep(2000);
                        break;
                }
            }
            if (result1 == true || result2 == true && result3 == true && result4 == true && result5 == true && result6 == true)
            {
                overallResult = true;
            }
            return overallResult;
        }

        public void NavigateToAnItemFromHLBankerDropdown(string item)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, menuNavigation, 120);
            driver.FindElement(menuNavigation).Click();
            Thread.Sleep(5000);

            IList<IWebElement> elements = driver.FindElements(By.XPath("//ul[@aria-label='Navigation Menu']/li"));
            int size = elements.Count;

            for (int items = 1; items <= size; items++)
            {
                By itemLink = By.XPath($"//ul[@aria-label='Navigation Menu']/li[{items}]/div/a");

                WebDriverWaits.WaitUntilEleVisible(driver, itemLink, 120);
                string itemName = driver.FindElement(itemLink).GetAttribute("data-label");

                if (itemName == item)
                {
                    driver.FindElement(itemLink).Click();
                    Thread.Sleep(10000);
                    break;
                }
            }
        }
        public bool VerifyFunctionalityOfActivityStartDateGridFilterOnMyActivityDashboard()
        {
            bool result = false;

            WebDriverWaits.WaitUntilEleVisible(driver, dropdownActivityStartDateFilter, 20);
            driver.FindElement(dropdownActivityStartDateFilter).Click();
            Thread.Sleep(5000);

            //Get filter count
            int filterCount = driver.FindElements(optionsActivityFilter).Count;
            DateTime currentDate = DateTime.Today;

            for (int i = 1; i <= filterCount; i++)
            {
                driver.FindElement(By.XPath($"//div[@class='css-1vp5y9m']/div/div[3]/div/div[{i}]/div[1]/div/input")).Click();
                Thread.Sleep(3000);

                //Get selected Filter value
                string selectedFilterValue = driver.FindElement(dropdownActivityStartDateFilter).Text;

                switch (selectedFilterValue)
                {
                    case "Last 7 Days":
                        DateTime setDate = currentDate.AddDays(-7);

                        bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if (dateTime < currentDate && dateTime >= setDate)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Last 30 Days":
                        DateTime setDate1 = currentDate.AddDays(-30);

                        bool recPresent1 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent1 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if (dateTime < currentDate && dateTime >= setDate1)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Last 3 Months":
                        DateTime setDate2 = currentDate.AddMonths(-3);

                        bool recPresent2 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent2 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/td[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/td[4]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if (dateTime < currentDate && dateTime >= setDate2)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Last 6 Months":
                        DateTime setDate3 = currentDate.AddMonths(-6);

                        bool recPresent3 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent3 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if (dateTime < currentDate && dateTime >= setDate3)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Last 12 Months":
                        DateTime setDate4 = currentDate.AddMonths(-12);

                        bool recPresent4 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent4 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if (dateTime < currentDate && dateTime >= setDate4)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Next 7 Days":
                        DateTime setDate5 = currentDate.AddDays(7);

                        bool recPresent5 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent5 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if (dateTime > currentDate && dateTime <= setDate5)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Next 30 Days":
                        DateTime setDate6 = currentDate.AddDays(30);

                        bool recPresent6 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent6 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if (dateTime > currentDate && dateTime <= setDate6)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Next 3 Months":
                        DateTime setDate7 = currentDate.AddMonths(3);

                        bool recPresent7 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent7 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if (dateTime > currentDate && dateTime <= setDate7)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Next 6 Months":
                        DateTime setDate8 = currentDate.AddMonths(6);

                        bool recPresent8 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent8 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if (dateTime > currentDate && dateTime <= setDate8)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Next 12 Months":
                        DateTime setDate9 = currentDate.AddMonths(12);

                        bool recPresent9 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if (recPresent9 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for (int j = 2; j <= recordCount; j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver, By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div"));
                                if (elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[4]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if (dateTime > currentDate && dateTime <= setDate9)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                }
                WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter, 120);
                driver.FindElement(dropdownStartDateFilter).Click();
                Thread.Sleep(5000);
            }
            driver.FindElement(dropdownStartDateFilter).Click();
            Thread.Sleep(5000);
            return result;
        }
        public bool VerifyFunctionalityOfKPIMetricesOnMyActivityDashboard(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool overallResult = false;
            bool result1 = false;
            bool result2 = false;
            bool result3 = false;
            bool result4 = false;
            bool result5 = false;
            bool result6 = false;

            WebDriverWaits.WaitUntilEleVisible(driver, dropdownActivityStartDateFilter, 20);
            driver.FindElement(dropdownActivityStartDateFilter).Click();
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div[3]/div[1]/div/input")).Click();
            Thread.Sleep(3000);

            //Get total no. of KPI
            int excelCount = ReadExcelData.GetRowCount(excelPath, "KPIMetrics");

            for (int i = 2; i <= excelCount; i++)
            {
                string exlKPIValue = ReadExcelData.ReadDataMultipleRows(excelPath, "KPIMetrics", i, 1);
                switch (exlKPIValue)
                {
                    case "Total":
                        string lblKPI1Count = driver.FindElement(lblTotalRecords).Text;

                        if (lblKPI1Count == "0")
                        {
                            Thread.Sleep(2000);
                            bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                            if (recPresent == true)
                            {
                                result1 = true;
                            }
                        }
                        else
                        {
                            //Get the no. of rows in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int rowCount = Convert.ToInt32(noOfRows) - 1;
                            string totalRows = Convert.ToString(rowCount);
                            if (lblKPI1Count == totalRows)
                            {
                                result1 = true;
                            }
                        }

                        break;
                    case "Meetings":
                        string lblKPI2Count = driver.FindElement(lblMeetingRecords).Text;

                        driver.FindElement(linkActivityKPIMeetingsViewDetails).Click();
                        Thread.Sleep(3000);

                        if (lblKPI2Count == "0")
                        {
                            Thread.Sleep(2000);
                            bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                            if (recPresent == true)
                            {
                                result2 = true;
                            }
                        }
                        else
                        {
                            //Select appropriate date filter value
                            WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter1, 120);
                            driver.FindElement(dropdownStartDateFilter1).Click();
                            Thread.Sleep(5000);

                            driver.FindElement(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div[3]/div[1]/div/input")).Click();
                            Thread.Sleep(3000);

                            //Get the no. of rows in table
                            string noOfRows1 = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[3]")).GetAttribute("aria-rowcount");
                            int rowCount1 = Convert.ToInt32(noOfRows1) - 1;
                            string totalRows1 = Convert.ToString(rowCount1);
                            if (lblKPI2Count == totalRows1)
                            {
                                result2 = true;
                            }
                        }

                        driver.FindElement(By.XPath("//span[text()='My Coverage - Meetings, Close tab']/..")).Click();
                        Thread.Sleep(2000);
                        break;
                    case "Calls":
                        string lblKPI3Count = driver.FindElement(lblCallRecords).Text;

                        driver.FindElement(linkActivityKPICallsViewDetails).Click();
                        Thread.Sleep(3000);

                        if (lblKPI3Count == "0")
                        {
                            Thread.Sleep(2000);
                            bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                            if (recPresent == true)
                            {
                                result3 = true;
                            }
                        }
                        else
                        {
                            //Select appropriate date filter value
                            WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter1, 120);
                            driver.FindElement(dropdownStartDateFilter1).Click();
                            Thread.Sleep(5000);

                            driver.FindElement(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div[3]/div[1]/div/input")).Click();
                            Thread.Sleep(3000);

                            //Get the no. of rows in table
                            string noOfRows2 = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[3]")).GetAttribute("aria-rowcount");
                            int rowCount2 = Convert.ToInt32(noOfRows2) - 1;
                            string totalRows2 = Convert.ToString(rowCount2);
                            if (lblKPI3Count == totalRows2)
                            {
                                result3 = true;
                            }
                        }

                        driver.FindElement(By.XPath("//span[text()='My Coverage - Calls, Close tab']/..")).Click();
                        Thread.Sleep(2000);
                        break;
                    case "Emails/Tasks":
                        string lblKPI4Count = driver.FindElement(lblEmailRecords).Text;

                        driver.FindElement(linkKPIEmailsViewDetails).Click();
                        Thread.Sleep(3000);

                        if (lblKPI4Count == "0")
                        {
                            Thread.Sleep(2000);
                            bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                            if (recPresent == true)
                            {
                                result4 = true;
                            }
                        }
                        else
                        {
                            //Select appropriate date filter value
                            WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter1, 120);
                            driver.FindElement(dropdownStartDateFilter1).Click();
                            Thread.Sleep(5000);

                            driver.FindElement(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div[3]/div[1]/div/input")).Click();
                            Thread.Sleep(3000);

                            //Get the no. of rows in table
                            string noOfRows3 = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[3]")).GetAttribute("aria-rowcount");
                            int rowCount3 = Convert.ToInt32(noOfRows3) - 1;
                            string totalRows3 = Convert.ToString(rowCount3);
                            if (lblKPI4Count == totalRows3)
                            {
                                result4 = true;
                            }
                        }

                        driver.FindElement(By.XPath("//span[text()='My Coverage - Emails/Task, Close tab']/..")).Click();
                        Thread.Sleep(2000);
                        break;
                    case "Others":
                        string lblKPI5Count = driver.FindElement(lblOtherRecords).Text;

                        driver.FindElement(linkKPIOthersViewDetails).Click();
                        Thread.Sleep(3000);

                        if (lblKPI5Count == "0")
                        {
                            Thread.Sleep(2000);
                            bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                            if (recPresent == true)
                            {
                                result5 = true;
                            }
                        }
                        else
                        {
                            //Select appropriate date filter value
                            WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter1, 120);
                            driver.FindElement(dropdownStartDateFilter1).Click();
                            Thread.Sleep(5000);

                            driver.FindElement(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div[3]/div[1]/div/input")).Click();
                            Thread.Sleep(3000);

                            //Get the no. of rows in table
                            string noOfRows4 = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[3]")).GetAttribute("aria-rowcount");
                            int rowCount4 = Convert.ToInt32(noOfRows4) - 1;
                            string totalRows4 = Convert.ToString(rowCount4);
                            if (lblKPI5Count == totalRows4)
                            {
                                result5 = true;
                            }
                        }

                        driver.FindElement(By.XPath("//span[text()='My Coverage - Others, Close tab']/..")).Click();
                        Thread.Sleep(2000);
                        break;
                    case "Missing Notes":
                        string lblKPI6Count = driver.FindElement(lblMissingNoteRecords).Text;

                        driver.FindElement(linkKPIMissingNotesViewDetails).Click();
                        Thread.Sleep(3000);

                        if (lblKPI6Count == "0")
                        {
                            Thread.Sleep(2000);
                            bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                            if (recPresent == true)
                            {
                                result6 = true;
                            }
                        }
                        else
                        {
                            //Select appropriate date filter value
                            WebDriverWaits.WaitUntilEleVisible(driver, dropdownStartDateFilter1, 120);
                            driver.FindElement(dropdownStartDateFilter1).Click();
                            Thread.Sleep(5000);

                            driver.FindElement(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div[3]/div[1]/div/input")).Click();
                            Thread.Sleep(3000);

                            //Get the no. of rows in table
                            string noOfRows5 = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[3]")).GetAttribute("aria-rowcount");
                            int rowCount5 = Convert.ToInt32(noOfRows5) - 1;
                            string totalRows5 = Convert.ToString(rowCount5);
                            if (lblKPI6Count == totalRows5)
                            {
                                result6 = true;
                            }
                        }

                        driver.FindElement(By.XPath("//span[text()='My Coverage - No notes, Close tab']/..")).Click();
                        Thread.Sleep(2000);
                        break;
                }
            }
            if (result1 == true || result2 == true && result3 == true && result4 == true && result5 == true && result6 == true)
            {
                overallResult = true;
            }
            return overallResult;
        }
        public bool AreKPIMetricesCorrectOnMyActivityDashboard(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool overallResult = false;
            bool result1 = false;
            bool result2 = false;
            bool result3 = false;
            bool result4 = false;
            bool result5 = false;
            bool result6 = false;

            WebDriverWaits.WaitUntilEleVisible(driver, dropdownActivityStartDateFilter, 10);
            driver.FindElement(dropdownActivityStartDateFilter).Click();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//div[@class='css-1mvcrrm']/div//div[3]/div/div[1]/div[1]/div/input")).Click();
            
            //Get total no. of KPI
            int excelCount = ReadExcelData.GetRowCount(excelPath, "KPIMetrics");
            for (int i = 2; i <= excelCount; i++)
            {
                string exlKPIValue = ReadExcelData.ReadDataMultipleRows(excelPath, "KPIMetrics", i, 1);                
                switch (exlKPIValue)
                {
                    case "Total":
                        string lblKPI1Count = driver.FindElement(lblActivityTotalRecords).Text;
                        //Get the no. of rows in table
                        string noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                        int rowCount = Convert.ToInt32(noOfRows);
                        int rowNoCount1=0;
                        if (lblKPI1Count == "0")
                        {
                            result1 = true;
                        }
                        else
                        {
                        Re_try:
                            try
                            {
                                rowNoCount1 = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{rowCount}]/th[1]/div/div")).Text);
                            }
                            catch
                            {
                                rowCount--;
                                if (rowCount <= 0)
                                    goto Exit1;
                                goto Re_try;
                            }
                        }
                    Exit1: string totalRows = Convert.ToString(rowNoCount1);
                        if (lblKPI1Count == totalRows)
                        {
                                result1 = true;
                        }                        
                        break;

                    case "Meetings":
                        string lblKPI2Count;
                        try
                        {
                            lblKPI2Count = driver.FindElement(lblActivityMeetingRecords).Text;
                        }
                        catch
                        {
                            lblKPI2Count = driver.FindElement(lblActivityMeetingRecordsZero).Text;
                        }
                         
                        driver.FindElement(linkActivityKPIMeetingsViewDetails).Click();
                        Thread.Sleep(5000);
                        WebDriverWaits.WaitUntilEleVisible(driver, dropdownViewDetailsActivityStartDateFilter, 10);
                        driver.FindElement(dropdownViewDetailsActivityStartDateFilter).Click();
                        Thread.Sleep(2000);
                        driver.FindElement(By.XPath("//div[@class='css-1mvcrrm']/div//div[3]/div/div[1]/div[1]/div/input")).Click();
                        //Get the no. of rows in table
                        noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                        rowCount = Convert.ToInt32(noOfRows);
                        int rowNoCount2 = 0;
                        if (lblKPI2Count == "0")
                        {
                           result2 = true;
                        }
                        else
                        {
                        Re_try:
                            try
                            {
                                rowNoCount2 = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[3]//tr[{rowCount}]/th[1]/div/div")).Text);
                            }
                            catch
                            {
                                rowCount--;
                                if (rowCount <= 0)
                                    goto Exit2;
                                goto Re_try;
                            }
                        Exit2: string totalRows2 = Convert.ToString(rowNoCount2);
                            if (lblKPI2Count == totalRows2)
                            {
                                result2 = true;
                            }
                        }
                        driver.FindElement(_btnCloseActiveTab("Meetings")).Click();
                        Thread.Sleep(2000);
                        break;

                    case "Calls":
                        string lblKPI3Count;
                        try
                        {
                            lblKPI3Count = driver.FindElement(lblActivityCallRecords).Text;
                        }
                        catch
                        {
                            lblKPI3Count = driver.FindElement(lblActivityCallRecordsZero).Text;
                        }
                        driver.FindElement(linkActivityKPICallsViewDetails).Click();
                        Thread.Sleep(3000);
                        WebDriverWaits.WaitUntilEleVisible(driver, dropdownViewDetailsActivityStartDateFilter, 10);
                        driver.FindElement(dropdownViewDetailsActivityStartDateFilter).Click();
                        Thread.Sleep(2000);
                        driver.FindElement(By.XPath("//div[@class='css-1mvcrrm']/div//div[3]/div/div[1]/div[1]/div/input")).Click();
                        //Get the no. of rows in table
                        Thread.Sleep(2000);
                        noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                        rowCount = Convert.ToInt32(noOfRows);
                        int rowNoCount3 = 0;
                        if (lblKPI3Count == "0")
                        {
                           result3 = true;
                        }
                        else
                        {
                        Re_try:
                            try
                            {
                                rowNoCount3 = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[3]//tr[{rowCount}]/th[1]/div/div")).Text);
                            }                            
                            catch
                            {
                                rowCount--;
                                if (rowCount <= 0)
                                    goto Exit3;
                                goto Re_try;
                            }
                        Exit3: string totalRows3 = Convert.ToString(rowNoCount3);
                            if (lblKPI3Count == totalRows3)
                            {
                                result3 = true;
                            }
                        }
                        driver.FindElement(_btnCloseActiveTab("Calls")).Click();
                        Thread.Sleep(2000);
                        break;

                    case "Emails/Tasks":
                        string lblKPI4Count;
                        try
                        {
                            lblKPI4Count = driver.FindElement(lblActivityEmailRecords).Text;
                        }
                        catch
                        {
                            lblKPI4Count = driver.FindElement(lblActivityEmailRecordsZero).Text;
                        }
                        driver.FindElement(linkActivityKPIEmailsViewDetails).Click();
                        Thread.Sleep(3000);
                        WebDriverWaits.WaitUntilEleVisible(driver, dropdownViewDetailsActivityStartDateFilter, 10);
                        driver.FindElement(dropdownViewDetailsActivityStartDateFilter).Click();
                        Thread.Sleep(2000);
                        driver.FindElement(By.XPath("//div[@class='css-1mvcrrm']/div//div[3]/div/div[1]/div[1]/div/input")).Click();
                        //Get the no. of rows in table
                        Thread.Sleep(2000);
                        noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                        rowCount = Convert.ToInt32(noOfRows);
                        int rowNoCount4 = 0;
                        if (lblKPI4Count == "0")
                        {
                           result4 = true;
                        }
                        else
                        {
                        Re_try:
                            try
                            {
                                rowNoCount4 = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[3]//tr[{rowCount}]/th[1]/div/div")).Text);
                            }
                            catch
                            {
                                rowCount--;
                                if (rowCount <= 0)
                                    goto Exit4;
                                goto Re_try;
                            }
                        Exit4: string totalRows3 = Convert.ToString(rowNoCount4);
                            if (lblKPI4Count == totalRows3)
                            {
                                result4 = true;
                            }
                        }
                        driver.FindElement(_btnCloseActiveTab("Emails/Tasks")).Click();
                        Thread.Sleep(2000);
                        break;

                    case "Other": //Others
                        string lblKPI5Count;
                        try
                        {
                            lblKPI5Count= driver.FindElement(lblActivityOtherRecords).Text;
                        }
                        catch
                        {
                            lblKPI5Count = driver.FindElement(lblActivityOtherRecordsZero).Text;
                        }
                        driver.FindElement(linkActivityKPIOthersViewDetails).Click();
                        Thread.Sleep(3000);
                        WebDriverWaits.WaitUntilEleVisible(driver, dropdownViewDetailsActivityStartDateFilter, 10);
                        driver.FindElement(dropdownViewDetailsActivityStartDateFilter).Click();
                        Thread.Sleep(2000);
                        driver.FindElement(By.XPath("//div[@class='css-1mvcrrm']/div//div[3]/div/div[1]/div[1]/div/input")).Click();
                        //Get the no. of rows in table
                        Thread.Sleep(2000);
                        noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                        rowCount = Convert.ToInt32(noOfRows);
                        int rowNoCount5 = 0;
                        if (lblKPI5Count == "0")
                        {
                            result5 = true;
                        }
                        else
                        {
                        Re_try:
                            try
                            {
                                rowNoCount5 = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[3]//tr[{rowCount}]/th[1]/div/div")).Text);
                            }
                            catch
                            {
                                rowCount--;
                                if (rowCount <= 0)
                                    goto Exit5;
                                goto Re_try;
                            }
                        Exit5: string totalRows4 = Convert.ToString(rowNoCount5);
                            if (lblKPI5Count == totalRows4)
                            {
                                result5 = true;
                            }
                        }
                        driver.FindElement(_btnCloseActiveTab("Others")).Click();
                        Thread.Sleep(2000);
                        break;

                    case "Missing Notes":
                        string lblKPI6Count;
                        try
                        {
                            lblKPI6Count = driver.FindElement(lblActivityMissingNoteRecords).Text;
                        }
                        catch
                        {
                            lblKPI6Count = driver.FindElement(lblActivityMissingNoteRecordsZero).Text;
                        }
                        driver.FindElement(linkActivityKPIMissingNotesViewDetails).Click();
                        Thread.Sleep(3000);
                        WebDriverWaits.WaitUntilEleVisible(driver, dropdownViewDetailsActivityStartDateFilter, 10);
                        driver.FindElement(dropdownViewDetailsActivityStartDateFilter).Click();
                        Thread.Sleep(2000);
                        driver.FindElement(By.XPath("//div[@class='css-1mvcrrm']/div//div[3]/div/div[1]/div[1]/div/input")).Click();
                        //Get the no. of rows in table
                        Thread.Sleep(2000);
                        noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                        rowCount = Convert.ToInt32(noOfRows);
                        int rowNoCount6 = 0;
                        if (lblKPI6Count == "0")
                        {
                           result6 = true;
                        }
                        else
                        {
                        Re_try:
                            try
                            {
                                rowNoCount6 = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[3]//tr[{rowCount}]/th[1]/div/div")).Text);
                            }
                            catch
                            {
                                rowCount--;
                                if (rowCount<=0)
                                    goto Exit6;
                                goto Re_try;
                            }
                            Exit6:  string totalRows5 = Convert.ToString(rowNoCount6);
                            if (lblKPI6Count == totalRows5)
                            {
                                result6 = true;
                            }
                        }
                        driver.FindElement(_btnCloseActiveTab("No Notes")).Click();
                        Thread.Sleep(2000);
                        break;
                }
            }
            if (result1 == true && result2 == true && result3 == true && result4 == true && result5 == true && result6 == true)
            {
                overallResult = true;
            }
            return overallResult;
        }

        
        public string UpdateActivityMeetingCallNotes()
        {
            CustomFunctions.SwitchToWindow(driver, 0);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameActvityDashboard));

            WebDriverWaits.WaitUntilEleVisible(driver, eleHomepageMeetingCallNotes, 10);
            CustomFunctions.MouseOver(driver, eleHomepageMeetingCallNotes);
            WebDriverWaits.WaitUntilEleVisible(driver, btnHomePageActivityViewUpdateNotes, 10);
            driver.FindElement(btnHomePageActivityViewUpdateNotes).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkOpenRecord, 10);
            driver.FindElement(linkOpenRecord).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, txtboxNotes, 10);
            string comments= driver.FindElement(txtboxNotes).GetAttribute("value");
            driver.FindElement(txtboxNotes).Clear();
            driver.FindElement(txtboxNotes).SendKeys("Automation New Test Comments");
            driver.FindElement(btnSaveNotes).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgLVPopup, 20);
            //driver.SwitchTo().DefaultContent();
            return driver.FindElement(msgLVPopup).Text;

        }
        public bool IsActivityOpenNewWindow()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, eleHomepageActivitySubject, 10);
            string homePageactivitySubject= driver.FindElement(eleHomepageActivitySubject).Text;

            WebDriverWaits.WaitUntilEleVisible(driver, linkOpenRecord, 10);
            driver.FindElement(linkOpenRecord).Click();
            Thread.Sleep(8000);
            // Switch to second window
            //CustomFunctions.SwitchToWindow(driver, 1);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, eleDetailPageActivitySubject, 10);
            string detailPageactivitySubject = driver.FindElement(eleDetailPageActivitySubject).Text;
            driver.SwitchTo().Window(driver.WindowHandles.First());
            CustomFunctions.CloseWindow(driver, 1);
            if (detailPageactivitySubject == homePageactivitySubject)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
        public string GetActionMenuText()
        {
            string actionMenu="";
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownActivityStartDateFilter, 10);
            driver.FindElement(dropdownActivityStartDateFilter).Click();
            Thread.Sleep(3000);

            //Get filter count
            int filterCount = driver.FindElements(optionsActivityFilter).Count;

            for (int i = 1; i <= filterCount; i++)
            {
                driver.FindElement(By.XPath($"//div[@class='css-1mvcrrm']//div[{i}][contains(@class,'row searchableTable')]//input")).Click();
                Thread.Sleep(5000);
                WebDriverWaits.WaitUntilEleVisible(driver, lblActivityTotalRecords, 10);
                string lblTotalKPICount = driver.FindElement(lblActivityTotalRecords).Text;
                if (lblTotalKPICount != "0")
                {
                    CustomFunctions.MouseOver(driver,eleHomepageActivitySubject);
                    WebDriverWaits.WaitUntilEleVisible(driver, btnOpenRecord, 10);
                    driver.FindElement(btnOpenRecord).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, txtActionMenu, 10);
                    actionMenu = driver.FindElement(txtActionMenu).Text;
                    break;
                }
                else
                {
                    driver.FindElement(dropdownActivityStartDateFilter).Click();
                    Thread.Sleep(3000);
                }
            }            
            return actionMenu;
        }
        public bool IsActivityListSortedChronologicalOnMyActivityDashboard()
        {
            bool overallResult = false;
            bool result1 = false;
            bool result2 = false;
            bool result3 = false;
            bool result4 = false;
            bool result5 = false;
            bool result6 = false;
            bool result7 = false;
            bool result8 = false;
            bool result9 = false;
            bool result10 = false;

            WebDriverWaits.WaitUntilEleVisible(driver, dropdownActivityStartDateFilter, 10);
            driver.FindElement(dropdownActivityStartDateFilter).Click();
            //Thread.Sleep(5000);

            //Get filter count
            WebDriverWaits.WaitUntilEleVisible(driver, optionsActivityFilter, 10);
            int filterCount = driver.FindElements(optionsActivityFilter).Count;
            DateTime currentDate = DateTime.Today;
            string noOfRows;
            int recordCount;
            for (int i = 1; i <= filterCount; i++)
            {
                driver.FindElement(_eleActivityFilterOption(i)).Click();
                //Thread.Sleep(3000);

                //Get selected Filter value
                WebDriverWaits.WaitUntilEleVisible(driver, dropdownActivityStartDateFilter, 10);
                string selectedFilterValue = driver.FindElement(dropdownActivityStartDateFilter).Text;

                switch (selectedFilterValue)
                {
                    case "Last 7 Days":
                        DateTime setDate = currentDate.AddDays(-7);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                //Get the no. of record in table
                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));

                                    if (elePresent == true)
                                    {
                                        DateTime dateTime1, dateTime2= setDate;
                                        int rowNoCount;
                                        try 
                                        {
                                            rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j}]/th[1]/div/div")).Text);
                                        }
                                        catch
                                        {
                                            continue;
                                        }                                        
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;
                                        dateTime1 = DateTime.Parse(activityDate);
                                        try
                                        {
                                            try
                                            {
                                                rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j+1}]/th[1]/div/div")).Text);
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                            activityDate = driver.FindElement(_elmActivityStartDate(j+1)).Text;
                                            dateTime2 = DateTime.Parse(activityDate);                                            
                                        }
                                        catch(NoSuchElementException)
                                        {                                            
                                            //Next Records not available                                            
                                        }
                                        if (dateTime1 >= dateTime2)
                                        {
                                            result1 = true;
                                        }
                                        else
                                        {
                                            result1 = false;
                                            break;
                                        }
                                    }                                    
                                }
                                if (recordCount == 2)
                                {
                                    result1 = true;
                                }
                            }                            
                            else
                            {
                                result1 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result1 = true;
                        }
                        break;
                    case "Last 30 Days":
                        DateTime setDate1 = currentDate.AddDays(-30);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                //Get the no. of record in table
                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));
                                    if (elePresent == true)
                                    {
                                        DateTime dateTime1, dateTime2 = setDate1;
                                        int rowNoCount;
                                        try
                                        {
                                            rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j}]/th[1]/div/div")).Text);
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;
                                        dateTime1 = DateTime.Parse(activityDate);
                                        try
                                        {
                                            try
                                            {
                                                rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j+1}]/th[1]/div/div")).Text);
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                            activityDate = driver.FindElement(_elmActivityStartDate(j+1)).Text;
                                            dateTime2 = DateTime.Parse(activityDate);
                                        }
                                        catch (NoSuchElementException)
                                        {
                                            //Next Records not available 
                                        }
                                        if (dateTime1 >= dateTime2)
                                        {
                                            result2 = true;
                                        }
                                        else
                                        {
                                            result2 = false;
                                            break;
                                        }
                                    }
                                }
                                if (recordCount == 2)
                                {
                                    result2 = true;
                                }
                            }
                            else
                            {
                                result2 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result2 = true;
                        }
                        break;
                    case "Last 3 Months":
                        DateTime setDate2 = currentDate.AddMonths(-3);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));
                                    if (elePresent == true)
                                    {
                                        DateTime dateTime1, dateTime2 = setDate2;
                                        int rowNoCount;
                                        try
                                        {
                                            rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j}]/th[1]/div/div")).Text);
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;
                                        dateTime1 = DateTime.Parse(activityDate);
                                        try
                                        {
                                            try
                                            {
                                                rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j + 1}]/th[1]/div/div")).Text);
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                            activityDate = driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j + 1}]/th[4]/div/div")).Text;
                                            dateTime2 = DateTime.Parse(activityDate);
                                        }
                                        catch (NoSuchElementException)
                                        {
                                            //Next Records not available 
                                        }
                                        if (dateTime1 >= dateTime2)
                                        {
                                            result3 = true;
                                        }
                                        else
                                        {
                                            result3 = false;
                                            break;
                                        }
                                    }
                                }
                                if (recordCount == 2)
                                {
                                    result3 = true;
                                }
                            }
                            else
                            {
                                result3 = true;
                            }
                        }
                        catch
                        {
                            //No Record found for the selected filter
                            result3 = true;
                        }
                        break;
                    case "Last 6 Months":
                        DateTime setDate3 = currentDate.AddMonths(-6);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));
                                    if (elePresent == true)
                                    {
                                        DateTime dateTime1, dateTime2 = setDate3;
                                        int rowNoCount;
                                        try
                                        {
                                            rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j}]/th[1]/div/div")).Text);
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;
                                        dateTime1 = DateTime.Parse(activityDate);
                                        try
                                        {
                                            try
                                            {
                                                rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j+1}]/th[1]/div/div")).Text);
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                            activityDate = driver.FindElement(_elmActivityStartDate(j+1)).Text;
                                            dateTime2 = DateTime.Parse(activityDate);
                                        }
                                        catch (NoSuchElementException)
                                        {
                                            //Next Records not available 
                                        }
                                        if (dateTime1 >= dateTime2)
                                        {
                                            result4 = true;
                                        }
                                        else
                                        {
                                            result4 = false;
                                            break;
                                        }
                                    }
                                }
                                if (recordCount == 2)
                                {
                                    result4 = true;
                                }
                            }
                            else
                            {
                                result4 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result4 = true;
                        }
                        break;
                    case "Last 12 Months":
                        DateTime setDate4 = currentDate.AddMonths(-12);
                        try
                        {
                            noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                //Get the no. of record in table

                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));
                                    if (elePresent == true)
                                    {
                                        DateTime dateTime1, dateTime2 = setDate4;
                                        int rowNoCount;
                                        try
                                        {
                                            rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j}]/th[1]/div/div")).Text);
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;
                                        dateTime1 = DateTime.Parse(activityDate);
                                        try
                                        {
                                            try
                                            {
                                                rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j+1}]/th[1]/div/div")).Text);
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                            activityDate = driver.FindElement(_elmActivityStartDate(j + 1)).Text;
                                            dateTime2 = DateTime.Parse(activityDate);
                                        }
                                        catch (NoSuchElementException)
                                        {
                                            //Next Records not available 
                                        }
                                        if (dateTime1 >= dateTime2)
                                        {
                                            result5 = true;
                                        }
                                        else
                                        {
                                            result5 = false;
                                            break;
                                        }
                                    }
                                }
                                if (recordCount == 2)
                                {
                                    result5 = true;
                                }
                            }
                            
                            else
                            {
                                result5 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result5 = true;
                        }
                        break;
                    case "Next 7 Days":
                        DateTime setDate5 = currentDate.AddDays(7);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                //Get the no. of record in table

                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));
                                    if (elePresent == true)
                                    {
                                        DateTime dateTime1, dateTime2 = setDate5;
                                        int rowNoCount;
                                        try
                                        {
                                            rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j}]/th[1]/div/div")).Text);
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;
                                        dateTime1 = DateTime.Parse(activityDate);
                                        try
                                        {
                                            try
                                            {
                                                rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j+1}]/th[1]/div/div")).Text);
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                            activityDate = driver.FindElement(_elmActivityStartDate(j + 1)).Text;
                                            dateTime2 = DateTime.Parse(activityDate);
                                        }
                                        catch (NoSuchElementException)
                                        {
                                            //Next Records not available 
                                        }
                                        if (dateTime1 >= dateTime2)
                                        {
                                            result6 = true;
                                        }
                                        else
                                        {
                                            result6 = false;
                                            break;
                                        }
                                    }
                                }
                                if (recordCount == 2)
                                {
                                    result6 = true;
                                }
                            }
                            
                            else
                            {
                                result6 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result6 = true;
                        }
                        break;
                    case "Next 30 Days":
                        DateTime setDate6 = currentDate.AddDays(30);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                //Get the no. of record in table

                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));
                                    if (elePresent == true)
                                    {
                                        DateTime dateTime1, dateTime2 = setDate6;
                                        int rowNoCount;
                                        try
                                        {
                                            rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j}]/th[1]/div/div")).Text);
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;
                                        dateTime1 = DateTime.Parse(activityDate);
                                        try
                                        {
                                            try
                                            {
                                                rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j+1}]/th[1]/div/div")).Text);
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                            activityDate = driver.FindElement(_elmActivityStartDate(j+1)).Text;
                                            dateTime2 = DateTime.Parse(activityDate);
                                        }
                                        catch (NoSuchElementException)
                                        {
                                            //Next Records not available 
                                        }
                                        if (dateTime1 >= dateTime2)
                                        {
                                            result7 = true;
                                        }
                                        else
                                        {
                                            result7 = false;
                                            break;
                                        }
                                    }
                                }
                                if (recordCount == 2)
                                {
                                    result7 = true;
                                }
                            }
                            
                            else
                            {
                                result7 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result7 = true;
                        }
                        break;
                    case "Next 3 Months":
                        DateTime setDate7 = currentDate.AddMonths(3);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                //Get the no. of record in table

                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));
                                    if (elePresent == true)
                                    {
                                        DateTime dateTime1, dateTime2 = setDate7;
                                        int rowNoCount;
                                        try
                                        {
                                            rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j}]/th[1]/div/div")).Text);
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;
                                        dateTime1 = DateTime.Parse(activityDate);
                                        try
                                        {
                                            try
                                            {
                                                rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j + 1}]/th[1]/div/div")).Text);
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                            activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;
                                            dateTime2 = DateTime.Parse(activityDate);
                                        }
                                        catch (NoSuchElementException)
                                        {
                                            //Next Records not available 
                                        }
                                        if (dateTime1 >= dateTime2)
                                        {
                                            result8 = true;
                                        }
                                        else
                                        {
                                            result8 = false;
                                            break;
                                        }
                                    }
                                }
                                if (recordCount == 2)
                                {
                                    result8 = true;
                                }
                            }
                            
                            else
                            {
                                result8 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result8 = true;
                        }
                        break;
                    case "Next 6 Months":
                        DateTime setDate8 = currentDate.AddMonths(6);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                //Get the no. of record in table

                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));
                                    if (elePresent == true)
                                    {
                                        DateTime dateTime1, dateTime2 = setDate8;
                                        int rowNoCount;
                                        try
                                        {
                                            rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j}]/th[1]/div/div")).Text);
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;
                                        dateTime1 = DateTime.Parse(activityDate);
                                        try
                                        {
                                            try
                                            {
                                                rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j+1}]/th[1]/div/div")).Text);
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                            activityDate = driver.FindElement(_elmActivityStartDate(j + 1)).Text;
                                            dateTime2 = DateTime.Parse(activityDate);
                                        }
                                        catch (NoSuchElementException)
                                        {
                                            //Next Records not available 
                                        }
                                        if (dateTime1 >= dateTime2|| recordCount==2)
                                        {
                                            result9 = true;
                                        }
                                        else
                                        {
                                            result9 = false;
                                            break;
                                        }
                                    }
                                }
                                if (recordCount == 2)
                                {
                                    result9 = true;
                                }
                            }                            
                            else
                            {
                                result9 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result9 = true;
                        }
                        break;
                    case "Next 12 Months":
                        DateTime setDate9 = currentDate.AddMonths(12);
                        try
                        {
                            noOfRows = driver.FindElement(tableActivities).GetAttribute("aria-rowcount");
                            recordCount = Convert.ToInt32(noOfRows);
                            if (recordCount > 1)
                            {
                                //Get the no. of record in table

                                for (int j = 2; j <= recordCount; j++)
                                {
                                    bool elePresent = CustomFunctions.IsElementPresent(driver, _elmActivityStartDate(j));
                                    if (elePresent == true)
                                    {
                                        DateTime dateTime1, dateTime2 = setDate9;
                                        int rowNoCount;
                                        try
                                        {
                                            rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j}]/th[1]/div/div")).Text);
                                        }
                                        catch
                                        {
                                            continue;
                                        }
                                        string activityDate = driver.FindElement(_elmActivityStartDate(j)).Text;
                                        dateTime1 = DateTime.Parse(activityDate);
                                        try
                                        {
                                            try
                                            {
                                                rowNoCount = Convert.ToInt32(driver.FindElement(By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]//tr[{j+1}]/th[1]/div/div")).Text);
                                            }
                                            catch
                                            {
                                                continue;
                                            }
                                            activityDate = driver.FindElement(_elmActivityStartDate(j+1)).Text;
                                            dateTime2 = DateTime.Parse(activityDate);
                                        }
                                        catch (NoSuchElementException)
                                        {
                                            //Next Records not available 
                                        }
                                        if (dateTime1 >= dateTime2)
                                        {
                                            result10 = true;
                                        }
                                        else
                                        {
                                            result10 = false;
                                            break;
                                        }
                                    }
                                }
                                if (recordCount == 2)
                                {
                                    result10 = true;
                                }
                            }                            
                            else
                            {
                                result10 = true;
                            }
                        }
                        catch
                        {
                            //No Record found fr the selected filter
                            result10 = true;
                        }
                        break;
                }
                WebDriverWaits.WaitUntilEleVisible(driver, dropdownActivityStartDateFilter, 10);
                driver.FindElement(dropdownActivityStartDateFilter).Click();
                Thread.Sleep(3000);
            }
            driver.FindElement(dropdownActivityStartDateFilter).Click();
            if (result1 == true && result2 == true && result3 == true && result4 == true && result5 == true && result6 == true && result7 == true && result8 == true && result9 == true && result10 == true)
            {
                overallResult = true;
            }
            return overallResult;
        }
        public bool IsModulePageDisplayed(string moduleName)
        {
            return (driver.FindElement(pageHeaderEle).Text).Contains(moduleName);
        }
    }
}