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
        By imgCompany = By.XPath("//div[1]/records-highlights-icon/force-record-avatar/span/img[@title='Company']");

        By linkShowAdvanceSearch = By.CssSelector(".link-options");
        By comboIndustryType = By.CssSelector("select[name*='industryGroupSearch']");
        By btnSearch = By.CssSelector("input[name*='btnSearch']");
        By comboRecordType = By.XPath("//select[@name='p3']");
        By btnContinue = By.CssSelector("input[value='Continue']");
        By txtNewCompanyName = By.CssSelector("input[name*='AccountName']");
        By btnSave = By.CssSelector("input[value='Save']");

        string dir = @"C:\Users\vkumar0427\source\repos\SF_Automation\TestData\";

        // To Search Company
        public string SearchCompany(string file, string CompanyType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkCompanies, 120);
            driver.FindElement(lnkCompanies).Click();
            string excelPath = dir + file;
            if (CompanyType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1)))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 2));
            }
            else if (CompanyType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 3, 1)))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 3, 2));
            }
            else if (CompanyType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 4, 1)))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
                driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 4, 2));
            }
            // 4th Company Type
            else if (CompanyType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 5, 1)))
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
            catch (Exception)
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
            catch (Exception)
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

            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanysearchL, 10);
            driver.FindElement(txtCompanysearchL).SendKeys(value);
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



        public string SearchCompanyWithIndustryType(string industryType)

        {

            By matchedmyCompany = By.XPath($"//table[contains(@id,'myCompanies')]//tbody//td//span[contains(text(),'{industryType}')]");



            WebDriverWaits.WaitUntilEleVisible(driver, comboIndustryType);

            driver.FindElement(comboIndustryType).SendKeys(industryType);

            driver.FindElement(btnSearch).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 80);

            Thread.Sleep(6000);

            try

            {

                string result = driver.FindElement(matchedmyCompany).Displayed.ToString();

                Console.WriteLine("Search Results :" + result);

                return "Record found";

            }

            catch (Exception)

            {

                return "No record found";

            }

        }



        public void CreateCompany(string recordType)

        {

            //WebDriverWaits.WaitUntilEleVisible(driver, lnkCompanies);

            //driver.FindElement(lnkCompanies).Click();

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
    }
}
