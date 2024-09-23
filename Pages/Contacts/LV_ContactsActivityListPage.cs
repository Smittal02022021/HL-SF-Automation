using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V120.DOM;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Threading;

namespace SF_Automation.Pages.Contact
{
    class LV_ContactsActivityListPage : BaseClass
    {
        By btnAddActivity = By.XPath("(//button[text()='Add Activity'])[1]");

        public bool VerifyUserLandsOnActivityTab()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity, 20);
                return driver.FindElement(btnAddActivity).Displayed;
            }
            catch { return false; }
        }

        public int GetActivityCount()
        {
            Thread.Sleep(3000);

            int totalNumberOfActivities;
            try
            {
                totalNumberOfActivities = driver.FindElements(By.XPath("(//table[contains(@class,'slds-table')])[3]//tbody/tr")).Count;
            }
            catch(Exception)
            {
                totalNumberOfActivities = 0;
            }

            return totalNumberOfActivities;
        }

        public bool VerifyCreatedActivityIsDisplayedUnderActivitiesList(int num)
        {
            Thread.Sleep(2000);
            bool result = false;

            //Get Activity List Count
            int activitiesAfterAdding = GetActivityCount();

            if(activitiesAfterAdding == num + 1)
            {
                result = true;
            }

            return result;
        }

        public void ViewActivityFromList(string name)
        {
            try
            {
                Thread.Sleep(2000);
                CustomFunctions.ActionClick(driver, driver.FindElement(By.XPath($"(//a[text()='{name}'])[1]")), 60);
                Thread.Sleep(3000);
            }
            catch(Exception) 
            {
                Thread.Sleep(2000);
                CustomFunctions.ActionClick(driver, driver.FindElement(By.XPath($"(//a[text()='{name}'])[2]")), 60);
                Thread.Sleep(3000);
            }
        }

        public string GetActivitySubjectName()
        {
            string subject = driver.FindElement(By.XPath("(//table[contains(@class,'slds-table')])[3]//tbody/tr[1]/td[4]//a")).Text;
            return subject;
        }

        public bool VerifyUpdatedActivityIsDisplayedUnderActivitiesList(string file, int row)
        {
            Thread.Sleep(2000);
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string updatedSubject = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 1);
            string sub = GetActivitySubjectName();

            if (updatedSubject == sub)
            {
                result = true;
            }

            return result;
        }

        public bool VerifyFollowupActivityIsDeleted(int num)
        {
            bool result = false;

            //Get Activity List Count
            int activitiesAfterDeleting = GetActivityCount();

            if(activitiesAfterDeleting == num-1)
            {
                result = true;
            }

            return result;
        }

        public bool VerifyFollowupActivityIsCreatedSuccessfully(string sub)
        {
            bool result = false;

            string followupActivitySubject = GetActivitySubjectName();

            if (followupActivitySubject == "Follow-up: " + sub)
            {
                result = true;
            }
            return result;
        }

        public string GetPrimaryHLAttendee()
        {
            string subject = driver.FindElement(By.XPath("(//table[contains(@class,'slds-table')])[3]//tbody/tr[1]/td[2]//a")).Text;
            Thread.Sleep(2000);
            return subject;
        }

        public string GetPrimaryExternalAttendee()
        {
            string subject = driver.FindElement(By.XPath("(//table[contains(@class,'slds-table')])[3]//tbody/tr[1]/td[3]//a")).Text;
            Thread.Sleep(2000);
            return subject;
        }

        public bool VerifyUpdatedPrimaryHLAndExternalAttendeesAreDisplayedOnActivityListPage(string prHL, string addHL, string prExt, string addExt)
        {
            bool result = false;

            if(prHL == addHL && prExt== addExt)
            {
                result = true;
            }
            return result;
        }

    }
}