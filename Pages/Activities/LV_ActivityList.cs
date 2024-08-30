using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
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
        By tableActivities = By.XPath("(//div[contains(@class,'table_header')]//table)[1]");
        By btnViewAll = By.XPath("//button[@title='View All']");
        By linkPrimayContact = By.XPath("(//table//tbody//tr[1]//td[@data-label='Primary Contact']//a)[1]");

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

            int totalNumberOfActivities;
            try
            {
                totalNumberOfActivities = driver.FindElements(By.XPath("(//tr[@class='slds-hint-parent'])[1]/../tr")).Count;
            }
            catch(Exception)
            {
                totalNumberOfActivities = 0;
            }
            
            return totalNumberOfActivities;
        }

        public bool VerifyCreatedActivityIsDisplayedUnderActivitiesList(int num)
        {
            Thread.Sleep(60000);
            bool result = false;

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,2500)");
            Thread.Sleep(2000);

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
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,2500)");
            Thread.Sleep(2000);

            string subjectName = driver.FindElement(By.XPath("(//td[@data-label='Subject']//a)[1]")).Text;
            return subjectName;
        }

        public string GetActivityDescriptionFromList()
        {
            Thread.Sleep(5000);
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,2500)");
            Thread.Sleep(2000);

            string descName = driver.FindElement(By.XPath("(//td[@data-label='Description']//lightning-base-formatted-text)[1]")).Text;
            return descName;
        }

        public string GetActivityMeetingNotesFromList()
        {
            Thread.Sleep(5000);
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,2500)");
            Thread.Sleep(2000);

            string meetingNotesName = driver.FindElement(By.XPath("(//td[@data-label='Meeting/Call Notes']//a)[1]")).Text;
            return meetingNotesName;
        }

        public void ViewActivityFromList(string name)
        {
            Thread.Sleep(5000);
            CustomFunctions.ActionClick(driver, driver.FindElement(By.XPath($"//a[contains(@title,'{name}')]")), 60);
            Thread.Sleep(5000);
        }

        public bool IsActivityListDisplayed()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tableActivities, 20);
                return driver.FindElement(tableActivities).Displayed;
            }
            catch { return false; }
        }

        public bool VerifyAvailableColumnsOnCompaniesActivitiesListView(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            //Get columns count            
            int recordCount = driver.FindElements(By.XPath("(//table)[2]//th[@role='columnheader']")).Count;
            int excelCount = ReadExcelData.GetRowCount(excelPath, "ActivityListColumns");

            for(int columnExl = 2; columnExl <= excelCount; columnExl++)
            {
                string expColValue = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityListColumns", columnExl, 1);

                for(int recordIndex = 1; recordIndex <= recordCount; recordIndex++)
                {
                    string actualColValue = driver.FindElement(By.XPath($"((//table)[2]//th[@role='columnheader'])[{recordIndex}]//span[@class='slds-truncate']")).Text;
                    if(expColValue == actualColValue)
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

    }

}