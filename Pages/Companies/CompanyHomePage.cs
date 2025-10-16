using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SF_Automation.Pages
{
    class CompanyHomePage : BaseClass
    {
        By lnkCompanies = By.CssSelector("a[title*='Companies Tab']");
        By txtCompanyName = By.CssSelector("input[id*='j_id37:nameSearch']");
        By btnCompanySearch = By.CssSelector("div[class='searchButtonPanel'] > center > input[value='Search']");
        By tblResults = By.CssSelector("table[id*='pbtCompanies']");
        By matchedResult = By.CssSelector("td[id*=':pbtCompanies:0:j_id68'] a");

        By CompanyHomePageHeading = By.CssSelector("h2[class='pageDescription']");
        By btnAddCompany = By.CssSelector("td[class='pbButton center'] > input[value='Add Company']");
        By errPage = By.CssSelector("span[id='theErrorPage:theError']");

        By btnCompanysearchL = By.XPath("//button[@aria-label='Search']");
        By txtCompanysearchL = By.XPath("//input[contains(@placeholder,'Search Companies')]");
        By txtCompanysearchL2 = By.XPath("//input[@placeholder='Search...']");
        By imgCompany = By.XPath("//div[1]/records-highlights-icon/force-record-avatar/span/img[@title='Company']");

        By linkShowAdvanceSearch = By.CssSelector(".link-options");
        By comboIndustryType = By.CssSelector("select[name*='industryGroupSearch']");
        By btnSearch = By.CssSelector("input[name*='btnSearch']");
        By comboRecordType = By.XPath("//select[@name='p3']");
        By btnContinue = By.CssSelector("input[value='Continue']");
        By txtNewCompanyName = By.CssSelector("input[name*='AccountName']");
        By btnSave = By.CssSelector("input[value='Save']");

        string dir = @"C:\Users\SMittal0207\source\repos\SF_Automation\TestData\";
        By inputSearchL = By.XPath("//input[contains(@placeholder,'Search...')]");
        By inputGlobalSearchL = By.XPath("//button[@aria-label='Search']");
        By inputGlobalSearchL2 = By.XPath("//button[contains(@aria-label,'Search: ')]");
        By imgCompanyL = By.XPath("//div[1]/records-highlights-icon/force-record-avatar/span/img[@title='Company']");
        By inputAdminGlobalSearchL = By.XPath("//input[contains(@placeholder,'and more...')]");
        By txtDefaultSelectedViewL = By.XPath("//h1//span[contains(@class,'lineHeight')]");//h1//span[contains(@class,'selectedListView ')]");
        By iconListViewPickerL = By.XPath("//div[contains(@class,'name-switcher')]//button[contains(@title,'Select a List View')]");
        By listViewsL = By.XPath("//ul[@aria-label='Recent List Views']/..//li//span/span");//div[contains(@class,'AutocompleteMenuList')]//li//span[contains(@class,'AutocompleteOptionText')]");
        By txtSearchBoxL = By.XPath("//input[@placeholder='Search this list...']");
        By btnClearSearchBoxL = By.XPath("//input[@placeholder='Search this list...']/..//button");
        By eleItemL = By.XPath("//table/tbody//tr[1]//th/span//a//span");
        By imgCompL = By.XPath("//div[1]/records-highlights-icon/force-record-avatar/span/img[@title='Company']");
        By popDuplicateL = By.XPath("//span[contains(@class,'toastMessage')]");
        By iconClearSearch = By.XPath("//button[@data-element-id='searchClear']");
        By btnDeleteL = By.XPath("//button[contains(text(),'Delete')]");
        By iconExpandMoreButonL = By.XPath("(//lightning-button-menu//button[contains(@class,'slds-button_icon-border-filled')])[1]");
        By btnMoreDeleteL = By.XPath("//span[contains(text(),'Delete')]");
        By btnConfirmDelete = By.XPath("//div[@role='dialog']//button[@title='Delete']");
        By btnNewCompanyL = By.XPath("//ul//li//a[@title='New']");
        By btnNextL = By.XPath("//div[contains(@class,'ChangeRecordTypeFooter')]//button/span[text()='Next']");

        private By _btnRadioRecordType(string type)
        {
            return By.XPath($"//h2[text()='New Company']/..//label//span[text()='{type}']");
        }

        private By _lnkSearchedCompanyL(string name)
        {
            return By.XPath($"//div[@aria-label='Companies||List View']//table//tbody//th[1]//a[@title='{name}']");
        }

        private By _btnCompanyHomePage(string name)
        {
            return By.XPath($"//ul//a[@title='{name}']");
        }

        public bool IsSearchRecentOptionDisplayedLV()
        {
            driver.SwitchTo().DefaultContent();
            //Thread.Sleep(4000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSearchBoxL, 5);
                return driver.FindElement(txtSearchBoxL).Displayed;
            }
            catch { return false; }
        }

        public string GetCompanyFromDislayedListLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, eleItemL, 10);
            return driver.FindElement(eleItemL).Text;
        }

        public void ClearRecentSearchAreaLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, eleItemL, 10);
            driver.FindElement(btnClearSearchBoxL).Click();
        }

        public bool SearchRecentCompanyLV(string oppName)
        {
            driver.FindElement(txtSearchBoxL).SendKeys(oppName);
            driver.FindElement(txtSearchBoxL).SendKeys(Keys.Enter);
            //Thread.Sleep(4000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, eleItemL, 10);
                bool found = driver.FindElement(eleItemL).Displayed;
                return found;
            }
            catch(Exception)
            {
                driver.FindElement(btnClearSearchBoxL).Click();
                return false;
            }
        }

        public bool AreViewOptionsDisplayedLV(string fileName)
        {
            driver.FindElement(iconListViewPickerL).Click();
            Thread.Sleep(2000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + fileName;
            bool isFound = false;
            IReadOnlyCollection<IWebElement> valViews = driver.FindElements(listViewsL);
            var actualViews = valViews.Select(x => x.Text).ToArray();
            int viewsCount = actualViews.Length;
            int countViews = ReadExcelData.GetRowCount(excelPath, "Views");
            for(int viewRow = 2; viewRow <= countViews; viewRow++)
            {
                string expectedViewName = ReadExcelData.ReadDataMultipleRows(excelPath, "Views", viewRow, 1);
                for(int row = 0; row < viewsCount; row++)
                {
                    isFound = false;
                    string actualViewName = actualViews[row];
                    if(actualViewName.Contains(expectedViewName))
                    {
                        isFound = true;
                        break;
                    }
                }
            }
            driver.FindElement(iconListViewPickerL).Click();
            return isFound;
        }

        public void ClickIconViewsOptionLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconListViewPickerL, 10);
            driver.FindElement(iconListViewPickerL).Click();
        }

        public string GetDefaultSelectedViewLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtDefaultSelectedViewL, 10);
            return driver.FindElement(txtDefaultSelectedViewL).Text;
        }

        public void ClickButtonCompanyHomePageLV(string btnName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _btnCompanyHomePage(btnName), 20);
            driver.FindElement(_btnCompanyHomePage(btnName)).Click();
            Thread.Sleep(5000);
        }

        public string GlobalSearchCompanyInLightningView(string companyName)
        {
            Thread.Sleep(6000); try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, inputGlobalSearchL, 5);
                driver.FindElement(inputGlobalSearchL).Click();
            }
            catch
            {
                WebDriverWaits.WaitUntilEleVisible(driver, inputGlobalSearchL2, 5);
                driver.FindElement(inputGlobalSearchL2).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, iconClearSearch, 5);
                driver.FindElement(iconClearSearch).Click();
            }

            Thread.Sleep(4000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, inputSearchL, 5);
                driver.FindElement(inputSearchL).SendKeys(companyName);
            }
            catch
            {
                WebDriverWaits.WaitUntilEleVisible(driver, inputAdminGlobalSearchL, 5);
                driver.FindElement(inputAdminGlobalSearchL).SendKeys(companyName);
            }
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, imgCompL, 5);
                driver.FindElement(imgCompL).Click();
                try
                {
                    By closePopDuplicateL = By.XPath("//span[contains(@class,'toastMessage')]/../../../../..//button");
                    WebDriverWaits.WaitUntilEleVisible(driver, popDuplicateL, 5);
                    if(driver.FindElement(popDuplicateL).Text.Contains("duplicates exist"))
                    {
                        driver.FindElement(closePopDuplicateL).Click();
                    }
                }
                catch { }
                //return "Record found";
                Thread.Sleep(6000);
                return "Record found";
            }
            catch
            {
                driver.FindElement(inputAdminGlobalSearchL).SendKeys(Keys.Enter);
                Thread.Sleep(6000);
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, _lnkSearchedCompanyL(companyName), 20);
                    driver.FindElement(_lnkSearchedCompanyL(companyName)).Click();
                    Thread.Sleep(8000);
                    try
                    {

                        By closePopDuplicateL = By.XPath("//span[contains(@class,'toastMessage')]/../../../../..//button");
                        WebDriverWaits.WaitUntilEleVisible(driver, popDuplicateL, 2);
                        if(driver.FindElement(popDuplicateL).Text.Contains("duplicates exist"))
                        {
                            driver.FindElement(closePopDuplicateL).Click();
                        }

                    }
                    catch { }
                    return "Record found";
                }
                catch { return "No record found"; }
            }

        }

        // To Search Company
        public string SearchCompany(string file, string CompanyType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkCompanies, 120);
            driver.FindElement(lnkCompanies).Click();
            string excelPath = dir + file;
            if(CompanyType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1)))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 2));
            }
            else if(CompanyType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 3, 1)))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 3, 2));
            }
            else if(CompanyType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 4, 1)))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 4, 2));
            }
            // 4th Company Type
            else if(CompanyType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 5, 1)))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 5, 2));
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnCompanySearch);
            Thread.Sleep(2000);
            driver.FindElement(btnCompanySearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(6000);
            try
            {
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResult).Click();
                return "Record found";
            }
            catch(Exception)
            {
                return "No record found";
            }
        }

        // Click add company button
        public void ClickAddCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkCompanies);
            driver.FindElement(lnkCompanies).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCompany, 120);
            driver.FindElement(btnAddCompany).Click();
        }

        // Search Company from Tableau downloaded sheet
        public string SearchTableauCompany(string file, int row1)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkCompanies, 120);
            driver.FindElement(lnkCompanies).Click();

            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Sheet 1", row1, 4));

            WebDriverWaits.WaitUntilEleVisible(driver, btnCompanySearch);
            Thread.Sleep(2000);
            driver.FindElement(btnCompanySearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(6000);
            try
            {
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                Console.WriteLine("Search Results :" + result);
                driver.FindElement(matchedResult).Click();
                WebDriverWaits.WaitForPageToLoad(driver, 60);
                //return result;
            }
            catch(Exception)
            {
                //return "No record found";
            }
            return ReadExcelData.ReadDataMultipleRows(excelPath, "Sheet 1", row1, 4);
        }

        //To Search Company with Company Name in Lighting
        public void SearchCompanyInLightning(string value)
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnCompanysearchL, 20);
            driver.FindElement(btnCompanysearchL).Click();
            Thread.Sleep(4000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanysearchL2, 10);
                driver.FindElement(txtCompanysearchL2).SendKeys(value);
            }
            catch
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanysearchL, 10);
                driver.FindElement(txtCompanysearchL).SendKeys(value);
            }

            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, imgCompany, 10);
            driver.FindElement(imgCompany).Click();
            Thread.Sleep(6000);
        }

        public void ClickCompaniesTabAdvanceSearch()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkCompanies);
            driver.FindElement(lnkCompanies).Click();
            driver.FindElement(linkShowAdvanceSearch).Click();
        }

        By imgLeader = By.XPath("//span[contains(@id,\"loadingStatus.start\")]");
        public string SearchCompanyWithIndustryType(string industryType)
        {
            By matchedmyCompany = By.XPath($"//table[contains(@id,'myCompanies')]//tbody//td//span[contains(text(),'{industryType}')]");
            WebDriverWaits.WaitUntilEleVisible(driver, comboIndustryType);
            driver.FindElement(comboIndustryType).SendKeys(industryType);
            driver.FindElement(btnSearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 20);
            //Thread.Sleep(10000);
            WebDriverWaits.WaitTillElementVisible(driver, imgLeader);
            try
            {
                string result = driver.FindElement(matchedmyCompany).Displayed.ToString();
                return "Record found";
            }
            catch(Exception)
            {
                return "No record found";
            }
        }

        public void CreateCompany(string recordType)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnContinue, 10);
            driver.FindElement(comboRecordType).SendKeys(recordType);
            driver.FindElement(btnContinue).Submit();
            string valCompanyName = "TestCompany_" + (CustomFunctions.RandomValue());
            WebDriverWaits.WaitUntilEleVisible(driver, txtNewCompanyName, 10);
            driver.FindElement(txtNewCompanyName).SendKeys(valCompanyName);
            driver.FindElement(btnSave).Click();
        }

        public void ClickSaveCompany()
        {
            string valCompanyName = "TestCompany_" + (CustomFunctions.RandomValue());
            WebDriverWaits.WaitUntilEleVisible(driver, txtNewCompanyName, 10);
            driver.FindElement(txtNewCompanyName).SendKeys(valCompanyName);
            driver.FindElement(btnSave).Click();
        }

        public void ClickNewCompanyButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCompanyL, 10);
            driver.FindElement(btnNewCompanyL).Click();
        }
        public void SelectCompanyTypeLV(string recordType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _btnRadioRecordType(recordType), 10);
            driver.FindElement(_btnRadioRecordType(recordType)).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnNextL, 10);
            driver.FindElement(btnNextL).Click();
        }

        public string SearchCompanyNew(string companyName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkCompanies, 20);
            driver.FindElement(lnkCompanies).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).SendKeys(companyName);
            WebDriverWaits.WaitUntilEleVisible(driver, btnCompanySearch);
            Thread.Sleep(2000);
            driver.FindElement(btnCompanySearch).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);
            Thread.Sleep(6000);
            try
            {
                string result = driver.FindElement(matchedResult).Displayed.ToString();
                driver.FindElement(matchedResult).Click();
                return "Record found";
            }
            catch(Exception)
            {
                return "No record found";
            }
        }

        public void DeleteCompanyLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);
            try
            {
                js.ExecuteScript("window.scrollTo(0,0)");
                WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteL, 10);
                driver.FindElement(btnDeleteL).Click();
            }
            catch(Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButonL, 10);
                driver.FindElement(iconExpandMoreButonL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnMoreDeleteL, 10);
                driver.FindElement(btnMoreDeleteL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmDelete, 10);
                driver.FindElement(btnConfirmDelete).Click();
            }

        }

        public void ClickNewButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCompanyL, 60);
            driver.FindElement(btnNewCompanyL).Click();
            Thread.Sleep(3000);
        }
    }
}