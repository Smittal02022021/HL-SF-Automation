using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages.Company
{
    class CompanySelectRecordPage : BaseClass
    {
        By headingCompanyRecordType = By.CssSelector("h2[class='pageDescription']");
        By drpdwnSelectRecordType = By.CssSelector("select[id='p3']");
        By btnContinue = By.CssSelector("input[title='Continue']");
        By drpdwnCompanyRecordType = By.CssSelector("select[id='p3']");
        By btnCancel = By.CssSelector("input[title='Cancel']");
        By txtRecordTypeL = By.XPath("//div[@class='changeRecordTypeCenter']//label//span[2]");
        By txtRecordTypeDscL = By.XPath("//div[@class='changeRecordTypeCenter']//label//div");
        By btnRecordTypePageL = By.XPath("//div[@class='forceChangeRecordTypeFooter']//button/span[text()='Next']");

        private By _radioRecordType(string name)
        {
            return By.XPath($"//div[@class='changeRecordTypeCenter']//label//span[2][text()='{name}']/../span");
        }

        private By _btnRecordTypePage(string name)
        {
            return By.XPath($"//div[@class='forceChangeRecordTypeFooter']//button/span[text()='{name}']");
        }

        public void ClickButtonCompanyChangeRecordTypePageLV(string btnName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _btnRecordTypePage(btnName), 10);
            driver.FindElement(_btnRecordTypePage(btnName)).Click();
            Thread.Sleep(5000);
        }

        public bool IsButtonPresentOnRecordTypePageLV(string btnName)
        {
            return driver.FindElement(_btnRecordTypePage(btnName)).Displayed;
        }

        public void SelectCompanyRecordTypeAndClickNextLV(string type)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _radioRecordType(type), 20);
            driver.FindElement(_radioRecordType(type)).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnRecordTypePageL, 20);
            driver.FindElement(btnRecordTypePageL).Click();
        }

        public bool AreCompanyRecordTypesDescriptionDisplayedLV(string file)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtRecordTypeDscL, 10);
            IList<IWebElement> dscRecordTypes = driver.FindElements(txtRecordTypeDscL);
            bool recordTypeDscFound = false;
            int rowOpp = ReadExcelData.GetRowCount(excelPath, "CompanyRecordTypes");
            foreach(IWebElement txtFieldError in dscRecordTypes)
            {
                recordTypeDscFound = false;
                string actualRecordType = txtFieldError.Text.Trim();
                for(int row = 2; row <= rowOpp; row++)
                {
                    string valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyRecordTypes", row, 2);
                    if(actualRecordType == valRecordTypeExl)
                    {
                        recordTypeDscFound = true;
                        break;
                    }
                }
            }
            return recordTypeDscFound;
        }

        public bool AreCompanyRecordTypesDisplayedLV(string file)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtRecordTypeL, 10);
            IList<IWebElement> listRecordTypes = driver.FindElements(txtRecordTypeL);
            bool recordTypeFound = false;
            int rowOpp = ReadExcelData.GetRowCount(excelPath, "CompanyRecordTypes");
            foreach(IWebElement txtFieldError in listRecordTypes)
            {
                recordTypeFound = false;
                string actualRecordType = txtFieldError.Text.Trim();
                for(int row = 2; row <= rowOpp; row++)
                {
                    string valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyRecordTypes", row, 1);
                    if(actualRecordType == valRecordTypeExl)
                    {
                        recordTypeFound = true;
                        break;
                    }
                }
            }
            return recordTypeFound;
        }
        // Get company record type page heading
        public string GetCompanyRecordTypePageHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, headingCompanyRecordType, 60);
            string headingCompanyRecordTypePage = driver.FindElement(headingCompanyRecordType).Text;
            return headingCompanyRecordTypePage;
        }

        public void SelectCompanyRecordType(string file, string recordType)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            CustomFunctions.SelectByText(driver, driver.FindElement(drpdwnSelectRecordType), recordType);
            WebDriverWaits.WaitUntilEleVisible(driver, btnContinue);
            driver.FindElement(btnContinue).Click();
        }

        //Verify Company reocrd types and description
        public void VerifyCompanyRecordTypesandDesc(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IWebElement recordDropdown = driver.FindElement(drpdwnCompanyRecordType);
            SelectElement select = new SelectElement(recordDropdown);
            int CompanyRecordList = ReadExcelData.GetRowCount(excelPath, "CompanyRecordTypes");

            for(int row = 2; row <= CompanyRecordList; row++)
            {
                IList<IWebElement> options = select.Options;
                IWebElement companyRecordTypeOption = options[row - 2];
                string companyRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyRecordTypes", row, 1);

                IWebElement tableCompanyRecordType = driver.FindElement(By.CssSelector("table[class='infoTable recordTypeInfo']>tbody>tr:nth-of-type(" + row + ")>th"));
                Assert.AreEqual(companyRecordTypeOption.Text, companyRecordTypeExl);
                Assert.AreEqual(tableCompanyRecordType.Text, companyRecordTypeExl);

                IWebElement tableCompanyRecordDesc = driver.FindElement(By.CssSelector("table[class='infoTable recordTypeInfo']>tbody>tr:nth-of-type(" + row + ")>td"));
                string companyRecordDesExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyRecordTypes", row, 2);
                Assert.AreEqual(tableCompanyRecordDesc.Text, companyRecordDesExl);

            }
        }

        //Verify continue and cancel button on company record page
        public void VerifyContinueCancelBtnDisplay()
        {
            bool res = driver.FindElement(btnContinue).Displayed;
            Assert.IsTrue(res);
            bool res1 = driver.FindElement(btnCancel).Displayed;
            Assert.IsTrue(res1);
        }
    }
}
