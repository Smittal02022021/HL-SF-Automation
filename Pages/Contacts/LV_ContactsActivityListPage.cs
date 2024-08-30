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
            Thread.Sleep(5000);
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
            Thread.Sleep(2000);
            CustomFunctions.ActionClick(driver, driver.FindElement(By.XPath($"(//a[text()='{name}'])[1]")), 60);
            Thread.Sleep(3000);
        }

        public bool VerifyUpdatedActivityIsDisplayedUnderActivitiesList(string file, int row)
        {
            Thread.Sleep(2000);
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string updatedSubject = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 1);
            string updatedIndGrp = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 2);
            string updatedPrdType = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 3);
            string updatedDesc = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 4);
            string updatedNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 5);
            string updatedExtAttendee = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 6);
            string updatedHLAttendee = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 7);

            return result;
        }
    }
}