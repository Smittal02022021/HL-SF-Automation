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
            Thread.Sleep(10000);
            bool result = false;

            //Get Activity List Count
            int activitiesAfterAdding = GetActivityCount();

            if (activitiesAfterAdding == num+1)
            {
                result= true;
            }

            return result;
        }

        public string GetActivityTypeFromList()
        {
            Thread.Sleep(5000);
            string typeName = driver.FindElement(By.XPath("(//td[@data-label='Type']//lightning-base-formatted-text)[1]")).Text;
            return typeName;
        }

        public string GetActivitySubjectFromList()
        {
            Thread.Sleep(5000);
            string subjectName = driver.FindElement(By.XPath("(//td[@data-label='Subject']//a)[1]")).Text;
            return subjectName;
        }

        public string GetActivityDescriptionFromList()
        {
            Thread.Sleep(5000);
            string descName = driver.FindElement(By.XPath("(//td[@data-label='Description']//lightning-base-formatted-text)[1]")).Text;
            return descName;
        }

        public string GetActivityMeetingNotesFromList()
        {
            Thread.Sleep(5000);
            string meetingNotesName = driver.FindElement(By.XPath("(//td[@data-label='Meeting/Call Notes']//a)[1]")).Text;
            return meetingNotesName;
        }

        public void ViewActivityFromList()
        {
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//td[@data-label='Subject']//a)[1]")).Click();

        }
    }

}