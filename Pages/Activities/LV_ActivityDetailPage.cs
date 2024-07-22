using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages.Activities
{
    class LV_ActivityDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();

        By btnCancelActivity = By.XPath("//button[@title='Cancel']");
        By btnEditActivity = By.XPath("//button[@title='Edit']");
        By btnDeleteActivity = By.XPath("//button[@title='Delete']");
        By btnSendNotificationActivity = By.XPath("//button[@title='Edit']");

        public string GetActivitySubjectFromList()
        {
            string subjectName = driver.FindElement(By.XPath("(//td[@data-label='Subject']//a)[1]")).Text;
            return subjectName;
        }

        public void ViewActivityFromList()
        {
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//td[@data-label='Subject']//a)[1]")).Click();

        }

        public void DeleteActivity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteActivity, 30);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnDeleteActivity));
            driver.FindElement(btnDeleteActivity).Click();
            Thread.Sleep(5000);
        }
    }

}