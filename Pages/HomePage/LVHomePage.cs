using NUnit.Framework;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
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

        By lblTabHeading = By.XPath("//a[@class='slds-context-bar__label-action slds-p-left--xx-small']/span");
        By lblTabTitle = By.XPath("(//span[@class='title slds-truncate'])[1]");

        //HomePage Dashboard
        By homePageH1Heading = By.XPath("//div[@class='dash-title']/div/div/h1");
        By activitiesFilter = By.XPath("//span[text()='Activities']");
        By myCoverageTab = By.XPath("//span[text()='My Coverage']");
        By dropdownStartDateFilter = By.XPath("(//div[@class='selected-values'])[3]");
        By lblNoRecords = By.XPath("//span[text()='No results found']");

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
        By linkContactsInSearchAllDropDown = By.XPath("//ul[@aria-label='Suggested For You']/li[8]/lightning-base-combobox-item/span[2]/span");
        By linkCompaniesInSearchAllDropDown = By.XPath("//ul[@aria-label='Suggested For You']/li[7]/lightning-base-combobox-item/span[2]/span");


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

        public void SearchContactFromMainSearch(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnMainSearch, 120);
            driver.FindElement(btnMainSearch).Click();
            Thread.Sleep(3000);

            driver.FindElement(dropdownSearchAll).Click();
            WebDriverWaits.WaitUntilEleVisible(driver,linkContactsInSearchAllDropDown,120);
            driver.FindElement(linkContactsInSearchAllDropDown).Click();
            Thread.Sleep(5000);

            driver.FindElement(txtMainSearch).SendKeys(name);
            driver.FindElement(txtMainSearch).SendKeys(Keys.Enter);
            Thread.Sleep(5000);

            driver.FindElement(By.XPath($"//a[@title='{name}']")).Click();
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

        public void NavigateToHomePageTabFromHLBankerDropdown()
        {
            WebDriverWaits.WaitUntilEleVisible(driver,menuNavigation,120);
            driver.FindElement(menuNavigation).Click();
            Thread.Sleep(5000);

            IList<IWebElement> elements = driver.FindElements(By.XPath("//ul[@aria-label='Navigation Menu']/li"));
            int size = elements.Count;

            for(int items = 1;items <= size;items++)
            {
                By linkHome = By.XPath($"//ul[@aria-label='Navigation Menu']/li[{items}]/div/a");

                WebDriverWaits.WaitUntilEleVisible(driver,linkHome,120);
                string itemName = driver.FindElement(linkHome).GetAttribute("data-label");

                if(itemName == "Home")
                {
                    driver.FindElement(linkHome).Click();
                    Thread.Sleep(10000);
                    WebDriverWaits.WaitUntilEleVisible(driver,homePageH1Heading,120);
                    string heading = driver.FindElement(homePageH1Heading).Text;
                    Assert.IsTrue(heading == "My Homepage");
                    break;
                }
            }
        }

        public bool VerifyIfActivitiesFilterGridIsAvailableNextToEngAndOppFilters()
        {
            bool result = false;
            if(driver.FindElement(activitiesFilter).Displayed)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyIfUserCanSeeMyCoverageTabUnderActivitiesFilter()
        {
            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver,activitiesFilter,120);
            driver.FindElement(activitiesFilter).Click();
            Thread.Sleep(5000);

            if(driver.FindElement(myCoverageTab).Displayed)
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
            WebDriverWaits.WaitUntilEleVisible(driver,dropdownStartDateFilter,120);
            if(driver.FindElement(dropdownStartDateFilter).Displayed)
            {
                driver.FindElement(dropdownStartDateFilter).Click();
                Thread.Sleep(3000);
                int recordCount = driver.FindElements(By.XPath("//div[@class='css-12liw54']/div/div[3]/div/div")).Count;
                int excelCount = ReadExcelData.GetRowCount(excelPath,"StartDateFilterOptions");

                for(int i = 2;i <= excelCount;i++)
                {
                    string exlListViewValue = ReadExcelData.ReadDataMultipleRows(excelPath,"StartDateFilterOptions",i,1);

                    for(int j = 1;j <= recordCount;j++)
                    {
                        string sfListViewValue = driver.FindElement(By.XPath($"//div[@class='css-12liw54']/div/div[3]/div/div[{j}]/div[2]/div/div")).Text;
                        if(exlListViewValue == sfListViewValue)
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
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyDefaultValueSelectedInActivityStartDateFilter()
        {
            Thread.Sleep(5000);
            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver,dropdownStartDateFilter,120);
            if(driver.FindElement(dropdownStartDateFilter).Text == "Last 7 Days")
            {
                result = true;
            }
            return result;
        }

        public bool VerifyFunctionalityOfActivityStartDateGridFilterOnMyCoverageDashboard()
        {
            bool result = false;

            WebDriverWaits.WaitUntilEleVisible(driver,dropdownStartDateFilter,120);
            driver.FindElement(dropdownStartDateFilter).Click();
            Thread.Sleep(5000);

            //Get filter count
            int filterCount = driver.FindElements(By.XPath("//div[@class='css-12liw54']/div/div[3]/div/div")).Count;
            DateTime currentDate = DateTime.Today;

            for(int i = 1;i <= filterCount;i++)
            {
                driver.FindElement(By.XPath($"//div[@class='css-12liw54']/div/div[3]/div/div[{i}]/div[1]/div/input")).Click();
                Thread.Sleep(3000);

                //Get selected Filter value
                string selectedFilterValue = driver.FindElement(dropdownStartDateFilter).Text;

                switch(selectedFilterValue)
                {
                    case "Last 7 Days":
                        DateTime setDate = currentDate.AddDays(-7);

                        bool recPresent = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div"));
                                if(elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if(dateTime < currentDate && dateTime >= setDate)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Last 30 Days":
                        DateTime setDate1 = currentDate.AddDays(-30);

                        bool recPresent1 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent1==false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div"));
                                if(elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if(dateTime < currentDate && dateTime >= setDate1)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Last 3 Months":
                        DateTime setDate2 = currentDate.AddMonths(-3);

                        bool recPresent2 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent2 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/td[3]/div/div"));
                                if(elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/td[3]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if(dateTime < currentDate && dateTime >= setDate2)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Last 6 Months":
                        DateTime setDate3 = currentDate.AddMonths(-6);

                        bool recPresent3 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent3 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div"));
                                if(elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if(dateTime < currentDate && dateTime >= setDate3)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Last 12 Months":
                        DateTime setDate4 = currentDate.AddMonths(-12);

                        bool recPresent4 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent4 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div"));
                                if(elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if(dateTime < currentDate && dateTime >= setDate4)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Next 7 Days":
                        DateTime setDate5 = currentDate.AddDays(7);

                        bool recPresent5 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent5 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div"));
                                if(elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if(dateTime > currentDate && dateTime <= setDate5)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Next 30 Days":
                        DateTime setDate6 = currentDate.AddDays(30);

                        bool recPresent6 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent6 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div"));
                                if(elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if(dateTime > currentDate && dateTime <= setDate6)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Next 3 Months":
                        DateTime setDate7 = currentDate.AddMonths(3);

                        bool recPresent7 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent7 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div"));
                                if(elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if(dateTime > currentDate && dateTime <= setDate7)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Next 6 Months":
                        DateTime setDate8 = currentDate.AddMonths(6);

                        bool recPresent8 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent8 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div"));
                                if(elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if(dateTime > currentDate && dateTime <= setDate8)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                    case "Next 12 Months":
                        DateTime setDate9 = currentDate.AddMonths(12);

                        bool recPresent9 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent9 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div"));
                                if(elePresent == true)
                                {
                                    string activityDate = driver.FindElement(By.XPath($"//table[@class='data-grid-table data-grid-full-table']/tbody/tr[{j}]/th[2]/div/div")).Text;
                                    DateTime dateTime = DateTime.Parse(activityDate);
                                    if(dateTime > currentDate && dateTime <= setDate9)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        break;
                }

                WebDriverWaits.WaitUntilEleVisible(driver,dropdownStartDateFilter,120);
                driver.FindElement(dropdownStartDateFilter).Click();
                Thread.Sleep(5000);
            }

            driver.FindElement(dropdownStartDateFilter).Click();
            Thread.Sleep(5000);
            return result;
        }

        public bool VerifyAvailabilityOfKPIMetricesOnMyCoverageDashboard(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;
            int excelCount = ReadExcelData.GetRowCount(excelPath,"KPIMetrics");
            string lblKPI1 = driver.FindElement(lblKPITotal).Text;
            string lblKPI2 = driver.FindElement(lblKPIMeetings).Text;
            string lblKPI3 = driver.FindElement(lblKPICalls).Text;
            string lblKPI4 = driver.FindElement(lblKPIEmailsTasks).Text;
            string lblKPI5 = driver.FindElement(lblKPIOthers).Text;
            string lblKPI6 = driver.FindElement(lblKPIMissingNotes).Text;

            for(int i = 2;i <= excelCount;i++)
            {
                string exlKPIValue = ReadExcelData.ReadDataMultipleRows(excelPath,"KPIMetrics",i,1);
                if(exlKPIValue==lblKPI1 || exlKPIValue == lblKPI2 || exlKPIValue == lblKPI3 || exlKPIValue == lblKPI4 || exlKPIValue == lblKPI5 || exlKPIValue == lblKPI6)
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
            int filterCount = driver.FindElements(By.XPath("//div[@class='css-12liw54']/div/div[3]/div/div")).Count;

            for(int i = 1; i <= filterCount; i++)
            {
                driver.FindElement(By.XPath($"//div[@class='css-12liw54']/div/div[3]/div/div[{i}]/div[1]/div/input")).Click();
                Thread.Sleep(3000);

                //Get selected Filter value
                string selectedFilterValue = driver.FindElement(dropdownStartDateFilter).Text;

                switch(selectedFilterValue)
                {
                    case "Last 7 Days":
                        bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if(recPresent == true)
                        {
                            driver.FindElement(dropdownStartDateFilter).Click();
                            continue;
                        }
                        break;
                    case "Last 30 Days":
                        bool recPresent1 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if(recPresent1 == true)
                        {
                            driver.FindElement(dropdownStartDateFilter).Click();
                            continue;
                        }
                        break;
                    case "Last 3 Months":
                        bool recPresent2 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if(recPresent2 == true)
                        {
                            driver.FindElement(dropdownStartDateFilter).Click();
                            continue;
                        }
                        break;
                    case "Last 6 Months":
                        bool recPresent3 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if(recPresent3 == true)
                        {
                            driver.FindElement(dropdownStartDateFilter).Click();
                            continue;
                        }
                        break;
                    case "Last 12 Months":
                        bool recPresent4 = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                        if(recPresent4 == true)
                        {
                            driver.FindElement(dropdownStartDateFilter).Click();
                            continue;
                        }
                        break;
                }
                break;
            }

            int excelCount = ReadExcelData.GetRowCount(excelPath,"ActivityColumns");
            int recordCount = driver.FindElements(By.XPath("(//table[@role='grid'])[2]/tbody/tr[1]/th")).Count;

            for(int i = 2;i <= excelCount;i++)
            {
                string exlColValue = ReadExcelData.ReadDataMultipleRows(excelPath,"ActivityColumns",i,1);

                for(int j = 1;j <= recordCount;j++)
                {
                    string sfColValue = driver.FindElement(By.XPath($"(//table[@role='grid'])[2]/tbody/tr[1]/th[{j}]/div/div/div/button/span/span")).Text;
                    if(exlColValue == sfColValue)
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

            WebDriverWaits.WaitUntilEleVisible(driver,dropdownStartDateFilter,120);
            driver.FindElement(dropdownStartDateFilter).Click();
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//div[@class='css-12liw54']/div/div[3]/div/div[3]/div[1]/div/input")).Click();
            Thread.Sleep(3000);

            //Get total no. of KPI
            int excelCount = ReadExcelData.GetRowCount(excelPath,"KPIMetrics");

            for(int i = 2;i <= excelCount;i++)
            {
                string exlKPIValue = ReadExcelData.ReadDataMultipleRows(excelPath,"KPIMetrics",i,1);
                switch(exlKPIValue)
                {
                    case "Total":
                        string lblKPI1Count = driver.FindElement(lblTotalRecords).Text;

                        //Get the no. of rows in table
                        string noOfRows = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                        int rowCount = Convert.ToInt32(noOfRows)-1;
                        string totalRows = Convert.ToString(rowCount);
                        if(lblKPI1Count == totalRows)
                        {
                            result1 = true;
                            break;
                        }
                        break;
                    case "Meetings":
                        string lblKPI2Count = driver.FindElement(lblMeetingRecords).Text;

                        driver.FindElement(linkKPIMeetingsViewDetails).Click();
                        Thread.Sleep(3000);

                        //Get the no. of rows in table
                        string noOfRows1 = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                        int rowCount1 = Convert.ToInt32(noOfRows1) - 1;
                        string totalRows1 = Convert.ToString(rowCount1);
                        if(lblKPI2Count == totalRows1)
                        {
                            result1 = true;
                            break;
                        }
                        break;
                    case "Calls":
                        string lblKPI3Count = driver.FindElement(lblCallRecords).Text;

                        driver.FindElement(linkKPICallsViewDetails).Click();
                        Thread.Sleep(3000);

                        //Get the no. of rows in table
                        string noOfRows2 = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                        int rowCount2 = Convert.ToInt32(noOfRows2) - 1;
                        string totalRows2 = Convert.ToString(rowCount2);
                        if(lblKPI3Count == totalRows2)
                        {
                            result1 = true;
                            break;
                        }
                        break;
                    case "Emails/Tasks":
                        string lblKPI4Count = driver.FindElement(lblEmailRecords).Text;

                        driver.FindElement(linkKPIEmailsViewDetails).Click();
                        Thread.Sleep(3000);

                        //Get the no. of rows in table
                        string noOfRows3 = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                        int rowCount3 = Convert.ToInt32(noOfRows3) - 1;
                        string totalRows3 = Convert.ToString(rowCount3);
                        if(lblKPI4Count == totalRows3)
                        {
                            result1 = true;
                            break;
                        }
                        break;
                    case "Others":
                        string lblKPI5Count = driver.FindElement(lblOtherRecords).Text;

                        driver.FindElement(linkKPIOthersViewDetails).Click();
                        Thread.Sleep(3000);

                        //Get the no. of rows in table
                        string noOfRows4 = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                        int rowCount4 = Convert.ToInt32(noOfRows4) - 1;
                        string totalRows4 = Convert.ToString(rowCount4);
                        if(lblKPI5Count == totalRows4)
                        {
                            result1 = true;
                            break;
                        }
                        break;
                    case "Missing Notes":
                        string lblKPI6Count = driver.FindElement(lblMissingNoteRecords).Text;

                        driver.FindElement(linkKPIMissingNotesViewDetails).Click();
                        Thread.Sleep(3000);

                        //Get the no. of rows in table
                        string noOfRows5 = driver.FindElement(By.XPath("//table[@class='data-grid-table data-grid-full-table']")).GetAttribute("aria-rowcount");
                        int rowCount5 = Convert.ToInt32(noOfRows5) - 1;
                        string totalRows5 = Convert.ToString(rowCount5);
                        if(lblKPI6Count == totalRows5)
                        {
                            result1 = true;
                            break;
                        }
                        break;
                }
            }
            if(result1==true || result2==true && result3 == true && result4 == true && result5 == true && result6 == true)
            {
                overallResult = true;
            }
            return overallResult;
        }

        public void NavigateToAnItemFromHLBankerDropdown(string item)
        {
            WebDriverWaits.WaitUntilEleVisible(driver,menuNavigation,120);
            driver.FindElement(menuNavigation).Click();
            Thread.Sleep(5000);

            IList<IWebElement> elements = driver.FindElements(By.XPath("//ul[@aria-label='Navigation Menu']/li"));
            int size = elements.Count;

            for(int items = 1;items <= size;items++)
            {
                By linkHome = By.XPath($"//ul[@aria-label='Navigation Menu']/li[{items}]/div/a");

                WebDriverWaits.WaitUntilEleVisible(driver,linkHome,120);
                string itemName = driver.FindElement(linkHome).GetAttribute("data-label");

                if(itemName == item)
                {
                    driver.FindElement(linkHome).Click();
                    Thread.Sleep(10000);
                    break;
                }
            }
        }

    }
}