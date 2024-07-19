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
    class LV_ActivitiesList : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();

        By lblAddNewActivity = By.XPath("//span[text()='Add New Activity']");
        By btnAddActivity = By.XPath("(//button[text()='Add Activity'])[1]");
        By btnRefereshActivitiesList = By.XPath("//button[@title='Refresh Activities']");

        public void ClickAddActivityBtn()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity);
            driver.FindElement(btnAddActivity).Click();
        }

        public void RefreshActivitiesList()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnRefereshActivitiesList, 30);
            driver.FindElement(btnRefereshActivitiesList).Click();
            Thread.Sleep(4000);
        }

        public void CloseTab(string tabName)
        {
            Thread.Sleep(5000);
            driver.FindElement(By.XPath($"//button[contains(@title,'Close {tabName}')]")).Click();
            Thread.Sleep(5000);
        }

        public int GetActivityCount()
        {
            Thread.Sleep(3000);
            int totalNumberOfActivities = driver.FindElements(By.XPath("//tr[@class='slds-hint-parent']")).Count;
            return totalNumberOfActivities;
        }

        public bool VerifyCreatedActivityIsDisplayedUnderActivitiesList(int num)
        {
            bool result = false;

            //Get Activity List Count
            int activitiesAfterAdding = GetActivityCount();

            if (activitiesAfterAdding == num+1)
            {
                result= true;
            }

            return result;
        }

    }

}