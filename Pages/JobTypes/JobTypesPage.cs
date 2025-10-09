
using OpenQA.Selenium;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SalesForce_Project.Pages.JobTypes
{
    class JobTypesPage : BaseClass
    {
        By searchBoxL = By.XPath("//input[contains(@aria-label,'Search this list')]");
        By btnClearSearchL = By.XPath("//button[@data-element-id='searchClear']");
        //By txtJobCodeL = By.XPath("//table//td[@data-label='Job Code']//span//span");
        //By txtProductLineL = By.XPath("//table//td[@data-label='Product Line']//span//span");
        By txtJobTypeNameL = By.XPath("//span[text()='JOB TYPE Name']/../..//lightning-formatted-text");
        By txtJobCodeL = By.XPath("//span[text()='Job Code']/../..//lightning-formatted-text");
        By txtProductTypeCodeL = By.XPath("//span[text()='Product Type Code']/../..//lightning-formatted-text");
        By txtProductTypeL = By.XPath("//span[text()='Product Type']/../..//lightning-formatted-text");
        By txtProductLineReportingL = By.XPath("//span[text()='Product Line Reporting']/../..//lightning-formatted-text");
        By txtProductLineL = By.XPath("//span[text()='Product Line']/../..//lightning-formatted-text");
        By txtProductTypeReportingL = By.XPath("//span[text()='Product Type Reporting']/../..//lightning-formatted-text");
        By txtEngagementDefaultStageL = By.XPath("//span[text()='Engagement Default Stage']/../..//lightning-formatted-text");
        By txtEngagementRecordTypeL = By.XPath("//span[text()='Engagement Record Type']/../..//lightning-formatted-text");
        By txtPrimaryLineBusinessL = By.XPath("//span[text()='Primary Line of Business']/../..//lightning-formatted-text");
        By chkRequireMAClosedWithL = By.XPath("//Input[contains(@name,'Require_MA_Closed_With')]");
        By chkRequireCapsourceL = By.XPath("//Input[contains(@name,'Require_Capsource')]");
        By lnkReqJobTypeNameL = By.XPath("//td[@data-label='Required Job Type Name']//a//span");
        By chkIsActiveL = By.XPath("//div[contains(@class,'RecordHomeTemplate')]//div//Input[contains(@name,'Is_Active')]");
        By lnkViewAllL = By.XPath("//a//span[text()='View All']/..");
        By tabCodeInfoL = By.XPath("//li[@title='Code Information']/a");

        private By _eleJobType(string name)
        {
            return By.XPath($"//div[contains(@class,'listViewContainer')]//table//tbody//th//a[@title='{name}']");
        }
        private By _txtPageHeader(string itemName)
        {
            return By.XPath($"//h1//slot//lightning-formatted-text[text()='{itemName}']");
        }

        public void SearchJobtypeLV(string jobTypeName)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnClearSearchL, 5);
                driver.FindElement(btnClearSearchL).Click();
            }
            catch { }
            WebDriverWaits.WaitUntilEleVisible(driver, searchBoxL, 5);
            driver.FindElement(searchBoxL).SendKeys(jobTypeName);
            driver.FindElement(searchBoxL).SendKeys(Keys.Enter);
            Thread.Sleep(5000);
        }

        public bool IsJobTypeDisplayedLV(string jobTypeName)
        {
            By elmItem = By.XPath($"//table//tr/th//a//span[text()='{jobTypeName}']");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, elmItem, 5);
                return driver.FindElement(elmItem).Displayed;
            }
            catch { return false; }
        }
        public void SelectJobTypeLV(string name)
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, _eleJobType(name), 10);
            driver.FindElement(_eleJobType(name)).Click();
            Thread.Sleep(5000);
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

        public string GetJobTypeNameLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtJobTypeNameL, 5);
                return driver.FindElement(txtJobTypeNameL).Text;
            }
            
            catch { return "Error"; }
        }
        public string GetJobCodeLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtJobCodeL, 5);
                return driver.FindElement(txtJobCodeL).Text;
            }
            
            catch { return "Error"; }
        }
        public string GetProductTypeCodeLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtProductTypeCodeL, 5);
                return driver.FindElement(txtProductTypeCodeL).Text;
            }
            catch { return "Error"; }
        }
        public string GetProductTypeLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtProductTypeL, 5);
                return driver.FindElement(txtProductTypeL).Text;
            }
            
            catch { return "Error"; }
        }
        public string GetProductLineReportingLV()
        {
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(txtProductLineReportingL));
                WebDriverWaits.WaitUntilEleVisible(driver, txtProductLineReportingL, 5);
                return driver.FindElement(txtProductLineReportingL).Text;
            }
            catch { return "Error"; }
        }
        public string GetProductLineLV()
        {
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(txtProductLineL));
                WebDriverWaits.WaitUntilEleVisible(driver, txtProductLineL, 5);
                return driver.FindElement(txtProductLineL).Text;
            }
            catch { return "Error"; }
        }
        public string GetProductTypeReportingLV()
        {
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(txtProductTypeReportingL));
                WebDriverWaits.WaitUntilEleVisible(driver, txtProductTypeReportingL, 5);
                return driver.FindElement(txtProductTypeReportingL).Text;
            }
            catch { return "Error"; }
        }

        public void ClickTabCodeInformationLV()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("window.scrollTo(0,0)");
            WebDriverWaits.WaitUntilEleVisible(driver, tabCodeInfoL, 5);
            driver.FindElement(tabCodeInfoL).Click();
            Thread.Sleep(2000);
        }
        public string GetEngagementDetailtStageLV()
        {
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(txtEngagementDefaultStageL));
                WebDriverWaits.WaitUntilEleVisible(driver, txtEngagementDefaultStageL, 5);
                return driver.FindElement(txtEngagementDefaultStageL).Text;
            }
            catch { return "Error"; }
        }
        public string GetEngagementRecordTypeLV()
        {
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(txtEngagementRecordTypeL));
                WebDriverWaits.WaitUntilEleVisible(driver, txtEngagementRecordTypeL, 5);
                return driver.FindElement(txtEngagementRecordTypeL).Text;
            }
            catch { return "Error"; }
        }
        public string GetPrimaryLineBusinessLV()
        {
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(txtPrimaryLineBusinessL));
                WebDriverWaits.WaitUntilEleVisible(driver, txtPrimaryLineBusinessL, 5);
                return driver.FindElement(txtPrimaryLineBusinessL).Text;
            }
            catch { return "Error"; }
        }
        public string GetRequireMAClosedWithLV()
        {
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(chkRequireMAClosedWithL));
                WebDriverWaits.WaitUntilEleVisible(driver, chkRequireMAClosedWithL, 5);
                if (driver.FindElement(chkRequireMAClosedWithL).Selected)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            catch { return "Error"; }
        }
        public string GetRequireCapsourceLV()
        {
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(chkRequireCapsourceL));
                WebDriverWaits.WaitUntilEleVisible(driver, chkRequireCapsourceL, 5);
                if( driver.FindElement(chkRequireCapsourceL).Selected)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            catch { return "Error"; }
        }

        public string IsRequiredItemDisplayedLV(string expectedItem)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("window.scrollTo(0,5000)");
            Thread.Sleep(2000);
            IReadOnlyCollection<IWebElement> valReqJobTypes = driver.FindElements(lnkReqJobTypeNameL);
            var actualValue = valReqJobTypes.Select(x => x.Text).ToArray();
            string isFound = "False";
            foreach (IWebElement valReqJobType in valReqJobTypes)
            {
                string value = valReqJobType.Text;
                if (valReqJobType.Text== expectedItem)
                {
                    isFound= "True";
                    break;
                }
            }
            return isFound;
        }
        public string IsJobTypeActiveLV()
        {
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(chkIsActiveL));
                WebDriverWaits.WaitUntilEleVisible(driver, chkIsActiveL, 5);
                if (driver.FindElement(chkIsActiveL).Selected)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            catch { return "Error"; }
        }
    }
}
