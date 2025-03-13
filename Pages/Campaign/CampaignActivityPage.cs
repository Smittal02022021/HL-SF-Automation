using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages
{
    class CampaignActivityPage : BaseClass
    {
        By btnSave = By.XPath("//input[@title='Save']");
        By btnDeleteActivity = By.XPath("//button[@title='Delete']");

        public bool VerifyActivityIsLinkedToCampaign(string sub)
        {
            bool result = false;
            
            if(driver.FindElement(By.XPath($"(//a[text()='{sub}'])[2]")).Displayed)
            {
                result = true;
            }
            return result;
        }

        public void ViewActivityFromList(string name)
        {
            Thread.Sleep(2000);
            CustomFunctions.ActionClick(driver, driver.FindElement(By.XPath($"(//a[text()='{name}'])[2]")), 60);
            Thread.Sleep(3000);
        }

        public void DeleteActivity()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteActivity, 60);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnDeleteActivity));
            driver.FindElement(btnDeleteActivity).Click();
            Thread.Sleep(2000);
        }
    }
}
