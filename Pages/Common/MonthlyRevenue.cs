using OpenQA.Selenium;
using SF_Automation.UtilityFunctions;
using System.Threading;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;

namespace SF_Automation.Pages.Common
{
    class MonthlyRevenue : BaseClass
    {
        RandomPages randomPages = new RandomPages();

        By dropDownView = By.XPath("//select[@id='fcf']");
        By btnGo = By.XPath("//input[@title='Go!']");
        By colIsCurrent = By.XPath("//td[@class='x-grid3-hd x-grid3-cell x-grid3-td-00Ni000000FnLRS ASC']");
        By imgIsCurrent = By.XPath("//div[@class='x-grid3-cell-inner x-grid3-col-00Ni000000FnLRS']/img");
        By linkMonthlyRevenueControlName = By.XPath("//div[@id='a1r6e000004Sj9R_Name']/a/span");
        By LOBColLength = By.XPath("//div[@id='a1r6e000004Sj9R_00N3100000GbhhF_body']/table/tbody/tr");
        By lblLegacyPeriod = By.XPath("//td[text()='Legacy Period Accrued Fees']");
        By lnkBackToList = By.XPath("//a[text()='Back to List: Monthly Revenue Process Controls']");
        By lblLagacyPAFee = By.XPath("//records-record-layout-item[@field-label='Legacy Period Accrued Fees']");

        By lnkIsCurrentColumn = By.XPath("//th[@aria-label='Is Current']//a");
        By chkIsCurrent = By.XPath("//table//tbody//tr[1]//td[5]/span//input");//..//span[contains(@class,'slds-checkbox')]");////table//tbody//tr[1]//td[5]//img");//(//td[@data-label='Is Current'])[1]//input"); .GetAttribute("alt");
        By linkCurrentMonthRev = By.XPath("//table//tbody//tr[1]/th//a");// (//td[@data-label='Is Current'])[1]//ancestor::tr//th//a");

        By linkViewAllRevAccu = By.XPath("//article[@aria-label='Revenue Accruals']//a[contains(@class,'footer')]");

        By listRevAccu = By.XPath("(//table[@aria-label='Revenue Accruals'])[2]//tbody//tr");

        By valRVAccuNum = By.XPath("//records-record-layout-item[@field-label='Revenue Accrual #']//lightning-formatted-text");

        public void SelectCurrentMonthRevenuePageLV()
        {
            bool IsCurrentFound = false;
        IsCurrentStatus:
            while(IsCurrentFound == false)
            {
                //string chckboxStatus = driver.FindElement(chkIsCurrent).IsSelected; // GetAttribute("alt");
                bool chckboxStatus = driver.FindElement(chkIsCurrent).Selected;
                if(chckboxStatus)
                {
                    driver.FindElement(linkCurrentMonthRev).Click();
                    Thread.Sleep(5000);
                    IsCurrentFound = true;
                }
                else
                {
                    driver.FindElement(lnkIsCurrentColumn).Click();
                    Thread.Sleep(2000);
                    goto IsCurrentStatus;
                }
            }
        }


        public string SelectRevenueAccrualLV(string lob)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,5000)");
            Thread.Sleep(2000);
            string valRVAccu = "";
            CustomFunctions.MoveToElement(driver, driver.FindElement(linkViewAllRevAccu));
            Thread.Sleep(2000);
            //driver.FindElement(linkViewAllRevAccu).Click();
            js.ExecuteScript("arguments[0].click();", driver.FindElement(linkViewAllRevAccu));
            WebDriverWaits.WaitUntilEleVisible(driver, listRevAccu, 20);
            IList<IWebElement> element = driver.FindElements(listRevAccu);
            int totalRows = element.Count;
            for(int row = 1; row <= totalRows; row++)
            {
                By linkRevAccu = By.XPath($"(//table[@aria-label='Revenue Accruals'])[2]//tbody//tr[{row}]//th//a/../..");
                driver.FindElement(linkRevAccu).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valRVAccuNum, 20);
                valRVAccu = driver.FindElement(valRVAccuNum).Text;
                string RevAccuLOB = randomPages.GetLOBLV();
                if(RevAccuLOB == lob)
                {
                    break;
                }
                else
                {
                    By btnCloseRevAccu = By.XPath($"//button[contains(@title,'Close {valRVAccu} | Revenue Accrual')]");
                    driver.FindElement(btnCloseRevAccu).Click();
                    Thread.Sleep(2000);
                }
            }
            return valRVAccu;
        }

        public bool IsLegacyPeriodAccruedFeesExistLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lblLagacyPAFee, 10);
                CustomFunctions.MoveToElement(driver, driver.FindElement(lblLagacyPAFee));
                return driver.FindElement(lblLagacyPAFee).Displayed;
            }
            catch { return false; }
        }

        public void SelectMonthlyRevenueProcessControlView()
        {
            if(driver.FindElement(btnGo).Displayed)
            {
                driver.FindElement(dropDownView).SendKeys("All Monthly Revenue Process Controls");
                Thread.Sleep(2000);
            }
            else
            {
                driver.FindElement(dropDownView).SendKeys("All Monthly Revenue Process Controls");
                Thread.Sleep(2000);
            }
        }

        public void SortDataAndGetToCurrentMonthRevenuePage()
        {
            string attValue = driver.FindElement(imgIsCurrent).GetAttribute("alt");
            if(attValue == "Not Checked")
            {
                driver.FindElement(colIsCurrent).Click();
                Thread.Sleep(2000);
                driver.FindElement(linkMonthlyRevenueControlName).Click();
            }
            else
            {
                driver.FindElement(linkMonthlyRevenueControlName).Click();
                Thread.Sleep(2000);
            }
        }

        public void GetToRevenueAccrualsPage(string lob)
        {
            IList<IWebElement> element = driver.FindElements(LOBColLength);
            int totalRows = element.Count;
            for(int i = 2; i <= totalRows; i++)
            {
                By xyz = By.XPath($"//div[@id='a1r6e000004Sj9R_00N3100000GbhhF_body']/table/tbody/tr[{i}]/td[2]");
                IWebElement LOBElement = driver.FindElement(xyz);

                string lobName = LOBElement.Text;
                if(lobName.Equals(lob))
                {
                    Console.WriteLine("LOB Name Matches");
                    By linkAccrualNo = By.XPath($"//div[@id='a1r6e000004Sj9R_00N3100000GbhhF_body']/table/tbody/tr[{i}]/th/a");
                    IWebElement linkAccrualNoElement = driver.FindElement(linkAccrualNo);
                    Thread.Sleep(1000);
                    linkAccrualNoElement.Click();
                    Thread.Sleep(3000);
                    break;
                }
            }
        }

        public string ValidateIfLegacyPeriodAccruedFeesExist()
        {
            if(driver.FindElement(lblLegacyPeriod).Displayed)
            {
                return "Legacy field is displayed. ";
            }
            else
            {
                return "Legacy field is not displayed. ";
            }
        }

        public void ClickBackToRevenueMonthList()
        {
            driver.FindElement(lnkBackToList).Click();
            Thread.Sleep(2000);
        }
    }
}


