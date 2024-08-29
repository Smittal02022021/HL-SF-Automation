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
    class LV_AddActivity : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();

        //Add New Activity
        By lblAddNewActivity = By.XPath("//span[text()='Add New Activity']");
        By btnAddActivity = By.XPath("(//button[text()='Add Activity'])[1]");

        By btnCallType = By.XPath("//input[@value='Call']");
        By btnEmailType = By.XPath("//input[@value='Email']");
        By btnInternalType = By.XPath("//input[@value='Internal']");
        By btnMeetingType = By.XPath("//input[@value='Meeting']");
        By btnOtherType = By.XPath("//input[@value='Other']");

        By txtSubject = By.XPath("//input[@name='subject']");
        By txtDate = By.XPath("(//input[@name='startDateTime'])[1]");
        By drpdownIndustryGroup = By.XPath("//button[@name='industryGroup']");
        By drpdownProductType = By.XPath("//button[@name='productType']");
        By txtareaDescription = By.XPath("//textarea[@name='description']");
        By txtareaHLInternalMeetingNotes = By.XPath("//textarea[@name='hlCallNotes']");
        By txtExternalAttendee = By.XPath("//input[@placeholder='Lookup Contact...']");
        By txtHLAttendee = By.XPath("//input[@placeholder='Lookup Employees...']");
        By txtCompanyDiscussed = By.XPath("//input[@placeholder='Lookup Company...']");
        By txtOpportunitiesDiscussed = By.XPath("//input[@placeholder='Lookup Opportunities...']");
        By txtEngagementsDiscussed = By.XPath("//input[@placeholder='Lookup Engagements...']");
        By txtCampaignsDiscussed = By.XPath("//input[@placeholder='Lookup Campaigns...']");

        By btnSave = By.XPath("(//button[@title='Save'])[1]");
        By btnCancel = By.XPath("(//button[@title='Cancel'])[1]");

        By reqFieldErrMsg = By.XPath("//label[text()='Subject']/following::div[2]");
        By txtDefaultHLAttandee = By.XPath("//c-s-l_-lwc-multi-lookup[contains(@class,'lookupForHLAttendee')]//lightning-pill//span[contains(@class,'pill__label')]");
        By txtDefaultCompanyDiscussed = By.XPath("//c-s-l_-lwc-multi-lookup[contains(@class,'lookupForAccount')]//lightning-pill//span[contains(@class,'pill__label')]");

        public void ClickAddActivityBtn()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity);
            driver.FindElement(btnAddActivity).Click();
            Thread.Sleep(5000);
        }

        public void ClickSaveActivityBtn()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
        }

        public void ClickCancelActivityBtn()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel);
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(5000);
        }

        public string GetRequiredFieldErrorMsg()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, reqFieldErrMsg, 20);
            string toastMsg = driver.FindElement(reqFieldErrMsg).Text;
            return toastMsg;
        }

        public void CreateNewActivityAdditionalHLAttandeeFromCompanyDetailPage(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string type = ReadExcelData.ReadData(excelPath, "Activity", 1);
            string subject = ReadExcelData.ReadData(excelPath, "Activity", 2);
            string industryGroup = ReadExcelData.ReadData(excelPath, "Activity", 3);
            string productType = ReadExcelData.ReadData(excelPath, "Activity", 4);
            string description = ReadExcelData.ReadData(excelPath, "Activity", 5);
            string meetingNotes = ReadExcelData.ReadData(excelPath, "Activity", 6);
            string extAttendee = ReadExcelData.ReadData(excelPath, "Activity", 7);
            string addHLAttandee = ReadExcelData.ReadData(excelPath, "Activity", 8);

            //Click on Add Activity button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnAddActivity));
            driver.FindElement(btnAddActivity).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblAddNewActivity, 20);

            //Enter Activity details
            Thread.Sleep(3000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(2);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("MMM d, yyyy"));
            Thread.Sleep(2000);

            CustomFunctions.MoveToElement(driver, driver.FindElement(drpdownIndustryGroup));
            driver.FindElement(drpdownIndustryGroup).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//span[@title='{industryGroup}']/../..")).Click();
            Thread.Sleep(2000);

            driver.FindElement(drpdownProductType).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath($"//span[@title='{productType}']")).Click();
            Thread.Sleep(1000);
            driver.FindElement(txtareaDescription).SendKeys(description);
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(meetingNotes);

            //Enter External Attendee
            driver.FindElement(txtExternalAttendee).SendKeys(extAttendee);
            Thread.Sleep(5000);
            driver.FindElement(By.XPath($"//div[@data-name='{extAttendee}']")).Click();

            //Additional HL Attendee
            driver.FindElement(txtHLAttendee).SendKeys(addHLAttandee);
            Thread.Sleep(5000);
            driver.FindElement(By.XPath($"//div[@data-name='{addHLAttandee}']")).Click();
            Thread.Sleep(5000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            Thread.Sleep(2000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);
        }

        public void CreateNewActivity(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string type = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 1);
            string subject = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 2);
            string industryGroup = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 3);
            string productType = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 4);
            string description = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 5);
            string meetingNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 6);
            string extAttendee = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 7);

            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity, 20);
            driver.FindElement(btnAddActivity).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblAddNewActivity, 20);

            //Enter Activity details
            Thread.Sleep(3000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(4);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("MMM dd, yyyy"));

            CustomFunctions.MoveToElement(driver, driver.FindElement(drpdownIndustryGroup));
            driver.FindElement(drpdownIndustryGroup).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//span[@title='{industryGroup}']/../..")).Click();
            Thread.Sleep(2000);

            driver.FindElement(drpdownProductType).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath($"//span[@title='{productType}']")).Click();
            Thread.Sleep(1000);
            driver.FindElement(txtareaDescription).SendKeys(description);
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(meetingNotes);

            //Enter External Attendee
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtExternalAttendee));
            driver.FindElement(txtExternalAttendee).SendKeys(extAttendee);
            Thread.Sleep(5000);
            driver.FindElement(By.XPath($"//div[@data-name='{extAttendee}']")).Click();

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            driver.FindElement(btnSave).Click();
            Thread.Sleep(40000);
        }

        public void CloseTab(string tabName)
        {
            Thread.Sleep(5000);
            driver.FindElement(By.XPath($"//button[contains(@title,'Close {tabName}')]")).Click();
            Thread.Sleep(5000);
        }

        public string GetDefaultPrimaryHlAttandeeHLAttandee()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtDefaultHLAttandee, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtDefaultHLAttandee));
            return driver.FindElement(txtDefaultHLAttandee).Text;
        }

        public string GetDefaultCompaniesDiscussed()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtDefaultCompanyDiscussed, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtDefaultCompanyDiscussed));
            return driver.FindElement(txtDefaultCompanyDiscussed).Text;
        }

        public void ViewActivityFromList(string name)
        {
            Thread.Sleep(5000);
            CustomFunctions.ActionClick(driver, driver.FindElement(By.XPath($"//a[@title='{name}']")), 60);
            Thread.Sleep(5000);
        }

        public void CreateNewActivityFromContactActivityPage(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string type = ReadExcelData.ReadData(excelPath, "Activity", 1);
            string subject = ReadExcelData.ReadData(excelPath, "Activity", 2);
            string industryGroup = ReadExcelData.ReadData(excelPath, "Activity", 3);
            string productType = ReadExcelData.ReadData(excelPath, "Activity", 4);
            string description = ReadExcelData.ReadData(excelPath, "Activity", 5);
            string meetingNotes = ReadExcelData.ReadData(excelPath, "Activity", 6);
            string extAttendee = ReadExcelData.ReadData(excelPath, "Activity", 7);
            string addHLAttandee = ReadExcelData.ReadData(excelPath, "Activity", 8);

            //Click on Add Activity button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnAddActivity));
            driver.FindElement(btnAddActivity).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblAddNewActivity, 20);

            //Enter Activity details
            Thread.Sleep(3000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(2);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("MMM d, yyyy"));
            Thread.Sleep(2000);

            CustomFunctions.MoveToElement(driver, driver.FindElement(drpdownIndustryGroup));
            driver.FindElement(drpdownIndustryGroup).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//span[@title='{industryGroup}']/../..")).Click();
            Thread.Sleep(2000);

            driver.FindElement(drpdownProductType).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath($"//span[@title='{productType}']")).Click();
            Thread.Sleep(1000);
            driver.FindElement(txtareaDescription).SendKeys(description);
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(meetingNotes);

            //Enter External Attendee
            driver.FindElement(txtExternalAttendee).SendKeys(extAttendee);
            Thread.Sleep(5000);
            driver.FindElement(By.XPath($"//div[@data-name='{extAttendee}']")).Click();

            //Additional HL Attendee
            driver.FindElement(txtHLAttendee).SendKeys(addHLAttandee);
            Thread.Sleep(5000);
            driver.FindElement(By.XPath($"//div[@data-name='{addHLAttandee}']")).Click();
            Thread.Sleep(5000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            Thread.Sleep(2000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);
        }

    }

}