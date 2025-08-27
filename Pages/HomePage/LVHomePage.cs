using NUnit.Framework;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

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
        By txtMainSearch1 = By.XPath("(//input[contains(@placeholder,'Search')])[2]");

        By userImage = By.XPath("(//span[@data-aura-class='uiImage'])[1]");
        By linkLogOut = By.XPath("//a[text()='Log Out']");
        By appLauncher = By.XPath("//button[@title='App Launcher']");
        By appHeader = By.XPath("//div/h1[contains(@class,'appName')]/span");
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
        By dropdownStartDateFilter1 = By.XPath("(//div[@class='selected-values'])[9]");

        By lblNoRecords = By.XPath("(//span[text()='No results found'])[2]");

        By lblKPITotal = By.XPath("//dd[text()='Total']");
        By lblTotalRecords = By.XPath("(//dd[text()='Total']/../../div/dt)[2]");

        By lblKPIMeetings = By.XPath("//dd[text()='Meetings']");
        By lblMeetingRecords = By.XPath("(//dd[text()='Meetings']/../../div/dt)[2]");
        By linkKPIMeetingsViewDetails = By.XPath("(//dd[text()='Meetings']/following::div/following::div[5]/span)[13]");

        By lblKPICalls = By.XPath("//dd[text()='Calls']");
        By lblCallRecords = By.XPath("(//dd[text()='Calls']/../../div/dt)[2]");
        By linkKPICallsViewDetails = By.XPath("(//dd[text()='Calls']/following::div/following::div[5]/span)[13]");

        By lblKPIEmailsTasks = By.XPath("//dd[text()='Emails/Tasks ']");
        By lblEmailRecords = By.XPath("(//dd[text()='Emails/Tasks ']/../../div/dt)[2]");
        By linkKPIEmailsViewDetails = By.XPath("(//dd[text()='Emails/Tasks ']/following::div/following::div[5]/span)[13]");

        By lblKPIOthers = By.XPath("//dd[text()='Others']");
        By lblOtherRecords = By.XPath("(//dd[text()='Others']/../../div/dt)[1]");
        By linkKPIOthersViewDetails = By.XPath("(//dd[text()='Others']/following::div/following::div[5]/span)[1]");

        By lblKPIMissingNotes = By.XPath("//dd[text()='Missing Notes']");
        By lblMissingNoteRecords = By.XPath("(//dd[text()='Missing Notes']/../../div/dt)[2]");
        By linkKPIMissingNotesViewDetails = By.XPath("(//dd[text()='Missing Notes']/following::div/following::div[5]/span)[13]");

        //General
        By linkSwitchToClassic = By.XPath("//a[text()='Switch to Salesforce Classic']");
        By dropdownSearchAll = By.XPath("//input[@data-value='Search: All']");
        By linkContactsInSearchAllDropDown = By.XPath("//lightning-base-combobox-item[@data-value='FILTER:Contact:Contacts']");
        By linkCompaniesInSearchAllDropDown = By.XPath("//lightning-base-combobox-item[@data-value='FILTER:Account:Companies']");
        By linkOpportunitiesInSearchAllDropDown = By.XPath("//lightning-base-combobox-item[@data-value='FILTER:Opportunity__c:Opportunities']");
        By linkEngagementInSearchAllDropdown = By.XPath("//lightning-base-combobox-item[@data-value='FILTER:Engagement__c:Engagements']");
        By linkPeople = By.XPath("//lightning-base-combobox-item[@data-value='FILTER:User:People']");
        By linkUserDetail = By.XPath("//a[@title='User Detail']");
        By btnLogin = By.CssSelector("input[title ='Login']");

        By pageHeaderEle = By.XPath("//lst-breadcrumbs//span");

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
            Thread.Sleep(4000);
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

            driver.FindElement(txtMainSearch).Clear();
            driver.FindElement(txtMainSearch).SendKeys(name);
            Thread.Sleep(3000);
            driver.FindElement(txtMainSearch).SendKeys(OpenQA.Selenium.Keys.Enter);
            Thread.Sleep(8000);

            WebDriverWaits.WaitForPageToLoad(driver, 120);

            try
            {
                driver.FindElement(By.XPath($"(//a[@title='{name}'])[2]")).Click();
                Thread.Sleep(5000);
            }
            catch(Exception)
            {
                driver.FindElement(By.XPath($"(//a[@title='{name}'])[1]")).Click();
                Thread.Sleep(5000);
            }
        }

        public void SearchHLEmpContactFromMainSearch(string name)
        {
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnMainSearch, 120);
            driver.FindElement(btnMainSearch).Click();
            Thread.Sleep(3000);

            driver.FindElement(dropdownSearchAll).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkContactsInSearchAllDropDown, 120);
            driver.FindElement(linkContactsInSearchAllDropDown).Click();
            Thread.Sleep(5000);

            driver.FindElement(txtMainSearch).Clear();
            driver.FindElement(txtMainSearch).SendKeys(name);
            Thread.Sleep(3000);
            driver.FindElement(txtMainSearch).SendKeys(OpenQA.Selenium.Keys.Enter);
            Thread.Sleep(8000);

            WebDriverWaits.WaitForPageToLoad(driver, 120);

            int rowCount = driver.FindElements(By.XPath("(//tbody)[2]/tr")).Count;

            for(int i=1; i<= rowCount; i++)
            {
                string compName = driver.FindElement(By.XPath($"(//tbody)[2]/tr[{i}]//td[2]//a")).GetAttribute("title");
                if(compName.Contains("Houlihan Lokey"))
                {
                    driver.FindElement(By.XPath($"(//tbody)[2]/tr[{i}]//th//a")).Click();
                    Thread.Sleep(5000);
                    break;
                }
            }
        }


        public void SearchUserFromMainSearch(string name)
        {
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnMainSearch, 120);
            driver.FindElement(btnMainSearch).Click();
            Thread.Sleep(5000);

            driver.FindElement(dropdownSearchAll).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkPeople, 120);
            driver.FindElement(linkPeople).Click();
            Thread.Sleep(5000);

            driver.FindElement(txtMainSearch).SendKeys(name);
            driver.FindElement(txtMainSearch).SendKeys(OpenQA.Selenium.Keys.Enter);
            Thread.Sleep(10000);

            try
            {
                driver.FindElement(By.XPath($"(//a[text()='{name}'])[1]")).Click();
                Thread.Sleep(5000);
            }
            catch(Exception)
            {
                driver.FindElement(By.XPath($"(//a[text()='{name}'])[2]")).Click();
                Thread.Sleep(5000);
            }
        }

        public void SearchUserFromMainSearch1(string name)
        {
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnMainSearch, 120);
            driver.FindElement(btnMainSearch).Click();
            Thread.Sleep(5000);

            driver.FindElement(dropdownSearchAll).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkPeople, 120);
            driver.FindElement(linkPeople).Click();
            Thread.Sleep(5000);

            driver.FindElement(txtMainSearch).SendKeys(name);
            Thread.Sleep(5000);

            try
            {
                driver.FindElement(By.XPath("(//div[@class='instant-results-list']/search_dialog-instant-result-item)[1]/div")).Click();
                Thread.Sleep(5000);
            }
            catch(Exception)
            {
                
            }
        }

        public void UserLogin()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkUserDetail, 120);
            driver.FindElement(linkUserDetail).Click();
            Thread.Sleep(30000);

            driver.SwitchTo().Frame(0);

            WebDriverWaits.WaitUntilEleVisible(driver, btnLogin);
            driver.FindElement(btnLogin).Click();
            Thread.Sleep(10000);

            driver.SwitchTo().DefaultContent();
        }

        public bool VerifyUserIsAbleToLogin(string userName)
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitForPageToLoad(driver, 120);

            bool result = false;
            string name = driver.FindElement(By.XPath("(//lightning-icon[@icon-name='utility:user']/following::span)[1]")).Text;
            if(name.Contains(userName))
            {
                result = true;
            }
            return result;
        }

        public bool VerifyBankerIsAbleToSearchActivityFromGlobalSearch(string actSub, string user, string addUser)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitForPageToLoad(driver, 120);

            bool result = false;
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnMainSearch, 120);
            driver.FindElement(btnMainSearch).Click();
            Thread.Sleep(3000);

            driver.FindElement(txtMainSearch1).Clear();
            driver.FindElement(txtMainSearch1).SendKeys(actSub);
            Thread.Sleep(3000);
            driver.FindElement(txtMainSearch1).SendKeys(OpenQA.Selenium.Keys.Enter);
            Thread.Sleep(3000);

            WebDriverWaits.WaitForPageToLoad(driver, 120);

            //Get total rows under events
            int totalRows = driver.FindElements(By.XPath("//a[text()='Events']/../../../../..//table//tr")).Count;

            for(int i=1; i<totalRows; i++)
            {
                string sub1 = driver.FindElement(By.XPath($"//a[text()='Events']/../../../../..//table//tr[{i}]/td[3]//a")).Text;
                string alias1 = driver.FindElement(By.XPath($"//a[text()='Events']/../../../../..//table//tr[{i}]/td[6]/span/span")).Text.Remove(0,1);

                if(sub1 == actSub && addUser.Contains(alias1))
                {
                    for(int j=2; j < totalRows; j++)
                    {
                        string sub2 = driver.FindElement(By.XPath($"//a[text()='Events']/../../../../..//table//tr[{j}]/td[3]//a")).Text;
                        string alias2 = driver.FindElement(By.XPath($"//a[text()='Events']/../../../../..//table//tr[{j}]/td[6]/span/span")).Text.Remove(0, 1);

                        if(sub2 == actSub && user.Contains(alias2))
                        {
                            result = true;
                            break;
                        }
                    }
                }
                break;
            }

            return result;
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
            driver.FindElement(txtMainSearch).SendKeys(OpenQA.Selenium.Keys.Enter);
            Thread.Sleep(5000);

            try
            {
                driver.FindElement(By.XPath($"(//a[@title='{name}'])[2]")).Click();
                Thread.Sleep(5000);
            }
            catch(Exception)
            {
                driver.FindElement(By.XPath($"(//a[@title='{name}'])[1]")).Click();
                Thread.Sleep(5000);
            }
        }

        public void SearchOpportunityFromMainSearch(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnMainSearch, 120);
            driver.FindElement(btnMainSearch).Click();
            Thread.Sleep(5000);

            driver.FindElement(dropdownSearchAll).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkOpportunitiesInSearchAllDropDown, 120);
            driver.FindElement(linkOpportunitiesInSearchAllDropDown).Click();
            Thread.Sleep(5000);

            driver.FindElement(txtMainSearch).SendKeys(name);
            driver.FindElement(txtMainSearch).SendKeys(OpenQA.Selenium.Keys.Enter);
            Thread.Sleep(5000);

            By ele = By.XPath($"(//a[@title='{name}'])[1]");
            WebDriverWaits.WaitUntilEleVisible(driver, ele, 60);

            driver.FindElement(By.XPath($"(//a[@title='{name}'])[1]")).Click();
            Thread.Sleep(5000);
        }

        public void SearchEngFromMainSearch(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnMainSearch, 120);
            driver.FindElement(btnMainSearch).Click();
            Thread.Sleep(5000);

            driver.FindElement(dropdownSearchAll).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkEngagementInSearchAllDropdown, 120);
            driver.FindElement(linkEngagementInSearchAllDropdown).Click();
            Thread.Sleep(5000);

            driver.FindElement(txtMainSearch).SendKeys(name);
            driver.FindElement(txtMainSearch).SendKeys(OpenQA.Selenium.Keys.Enter);
            Thread.Sleep(5000);

            By ele = By.XPath($"(//a[@title='{name}'])[1]");
            WebDriverWaits.WaitUntilEleVisible(driver, ele, 60);

            driver.FindElement(By.XPath($"(//a[@title='{name}'])[1]")).Click();
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

        public void SearchObjects(string item)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearchItems, 140);
            driver.FindElement(txtSearchItems).SendKeys(item);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"(//b[text()='{item}'])[1]")).Click();
            Thread.Sleep(3000);
        }

        public void UserLogoutFromSFLightningView()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, linkLogout, 120);
                driver.FindElement(linkLogout).Click();
                Thread.Sleep(4000);
            }
            catch (Exception ex)
            {
                driver.Navigate().Refresh();
                Thread.Sleep(4000);
                WebDriverWaits.WaitUntilEleVisible(driver, linkLogout, 120);
                driver.FindElement(linkLogout).Click();
                Thread.Sleep(4000);
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
                    driver.SwitchTo().Frame("asset-My_Homepage");
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
                int recordCount = driver.FindElements(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div")).Count;
                int excelCount = ReadExcelData.GetRowCount(excelPath,"StartDateFilterOptions");

                for(int i = 2;i <= excelCount;i++)
                {
                    string exlListViewValue = ReadExcelData.ReadDataMultipleRows(excelPath,"StartDateFilterOptions",i,1);

                    for(int j = 1;j <= recordCount;j++)
                    {
                        string sfListViewValue = driver.FindElement(By.XPath($"//div[@class='css-1vp5y9m']/div/div[3]/div/div[{j}]/div[2]/div/div")).Text;
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
            WebDriverWaits.WaitUntilEleVisible(driver,dropdownStartDateFilter,120);
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
            int filterCount = driver.FindElements(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div")).Count;
            DateTime currentDate = DateTime.Today;

            for(int i = 1;i <= filterCount;i++)
            {
                driver.FindElement(By.XPath($"//div[@class='css-1vp5y9m']/div/div[3]/div/div[{i}]/div[1]/div/input")).Click();
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
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if(elePresent == true)
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

                        bool recPresent1 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent1==false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if(elePresent == true)
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

                        bool recPresent2 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent2 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if(elePresent == true)
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

                        bool recPresent3 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent3 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if(elePresent == true)
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

                        bool recPresent4 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent4 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if(elePresent == true)
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

                        bool recPresent5 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent5 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if(elePresent == true)
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

                        bool recPresent6 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent6 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if(elePresent == true)
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

                        bool recPresent7 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent7 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if(elePresent == true)
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

                        bool recPresent8 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent8 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if(elePresent == true)
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

                        bool recPresent9 = CustomFunctions.IsElementPresent(driver,lblNoRecords);
                        if(recPresent9 == false)
                        {
                            //Get the no. of record in table
                            string noOfRows = driver.FindElement(By.XPath("(//table[@class='data-grid-table data-grid-full-table'])[2]")).GetAttribute("aria-rowcount");
                            int recordCount = Convert.ToInt32(noOfRows) - 1;

                            for(int j = 2;j <= recordCount;j++)
                            {
                                bool elePresent = CustomFunctions.IsElementPresent(driver,By.XPath($"(//table[@class='data-grid-table data-grid-full-table'])[2]/tbody/tr[{j}]/th[4]/div/div"));
                                if(elePresent == true)
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

                WebDriverWaits.WaitUntilEleVisible(driver,dropdownStartDateFilter,120);
                driver.FindElement(dropdownStartDateFilter).Click();
                Thread.Sleep(5000);
            }

            driver.FindElement(dropdownStartDateFilter).Click();
            Thread.Sleep(5000);

            if (result1 == true || result2 == true && result3 == true && result4 == true && result5 == true && result6 == true && result7 == true && result8 == true && result9 == true && result10 == true)
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
            int filterCount = driver.FindElements(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div")).Count;

            for(int i = 1; i <= filterCount; i++)
            {
                driver.FindElement(By.XPath($"//div[@class='css-1vp5y9m']/div/div[3]/div/div[{i}]/div[1]/div/input")).Click();
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
            int columnCount = driver.FindElements(By.XPath("(//table[@role='grid'])[5]/tbody/tr[1]/th")).Count;

            for(int i = 2;i <= excelCount;i++)
            {
                string exlColValue = ReadExcelData.ReadDataMultipleRows(excelPath,"ActivityColumns",i,1);

                for(int j = 1;j <= columnCount; j++)
                {
                    string sfColValue = driver.FindElement(By.XPath($"(//table[@role='grid'])[5]/tbody/tr[1]/th[{j}]/div/div/div/button/span/span")).Text;
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

            WebDriverWaits.WaitUntilEleVisible(driver,dropdownStartDateFilter,120);
            driver.FindElement(dropdownStartDateFilter).Click();
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//div[@class='css-1vp5y9m']/div/div[3]/div/div[3]/div[1]/div/input")).Click();
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

                        if(lblKPI2Count == "0")
                        {
                            Thread.Sleep(2000);
                            bool recPresent = CustomFunctions.IsElementPresent(driver, lblNoRecords);
                            if(recPresent==true)
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
            if(result1==true || result2==true && result3 == true && result4 == true && result5 == true && result6 == true)
            {
                overallResult = true;
            }
            return overallResult;
        }

        public void NavigateToAnItemFromHLBankerDropdown(string item)
        {
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver,menuNavigation,120);
            driver.FindElement(menuNavigation).Click();
            Thread.Sleep(5000);

            IList<IWebElement> elements = driver.FindElements(By.XPath("//ul[@aria-label='Navigation Menu']/li"));
            int size = elements.Count;

            for(int items = 1;items <= size;items++)
            {
                By itemLink = By.XPath($"//ul[@aria-label='Navigation Menu']/li[{items}]/div/a");

                WebDriverWaits.WaitUntilEleVisible(driver, itemLink, 120);
                string itemName = driver.FindElement(itemLink).GetAttribute("data-label");

                if(itemName == item)
                {
                    driver.FindElement(itemLink).Click();
                    Thread.Sleep(10000);
                    break;
                }
            }
        }

        public bool IsModulePageDisplayed(string moduleName)
        {
            return (driver.FindElement(pageHeaderEle).Text).Contains(moduleName);
        }

        public void SelectAppLV(string appName)
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, appHeader, 30);
            string defaultApp = driver.FindElement(appHeader).Text;
            if (defaultApp == appName)
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

        public void SelectAppLV1(string appName)
        {
            Thread.Sleep(5000);
            string defaultApp = driver.FindElement(By.XPath("//span[@title='Setup']")).Text;
            if(defaultApp == appName)
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
                Thread.Sleep(8000);
            }
        }

    }
}