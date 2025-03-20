using AventStack.ExtentReports.Gherkin.Model;
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
        By txtEndDate = By.XPath("(//input[@name='endDateTime'])[1]");
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

        By btnCreateNewCompany = By.XPath("//button[text()='Create New Company']");
        By dropdownCompanyType = By.XPath("//label[text()='Company Type']/following::div//button[@aria-label='Company Type']");
        By txtCompanyName = By.XPath("//label[text()='Company Name']/following::div//input[@name='Name']");
        By txtCompanyCountry = By.XPath("//input[@name='country']/../..");

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

        public void CreateNewActivityWithCompanyDiscussed(string file)
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
            string companyDiscussed = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 7);

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

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            driver.FindElement(txtCompanyDiscussed).SendKeys(companyDiscussed);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//div[@data-name='{companyDiscussed}']")).Click();
            Thread.Sleep(2000);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            driver.FindElement(btnSave).Click();
            Thread.Sleep(40000);
        }

        public void CreateNewActivityWithOpportunityDiscussed(string file)
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
            string companyDiscussed = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 7);
            string oppDiscussed = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 8);

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

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            driver.FindElement(txtCompanyDiscussed).SendKeys(companyDiscussed);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//div[@data-name='{companyDiscussed}']")).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtOpportunitiesDiscussed).SendKeys(oppDiscussed);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"(//div[@data-name='{oppDiscussed}'])[1]")).Click();
            Thread.Sleep(2000);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
        }

        public void CreateNewActivityWithEngagementDiscussed(string file, string engName, string oppName)
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
            string companyDiscussed = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 7);
            string oppDiscussed = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 8);

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

            driver.FindElement(txtareaDescription).SendKeys(description);
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(meetingNotes);
            Thread.Sleep(2000);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,2500)");
            Thread.Sleep(2000);

            driver.FindElement(txtCompanyDiscussed).SendKeys(companyDiscussed);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//div[@data-name='{companyDiscussed}']")).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtOpportunitiesDiscussed).SendKeys(oppName);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"(//div[@data-name='{oppName}'])[1]")).Click();
            Thread.Sleep(2000);

            js.ExecuteScript("window.scrollTo(0,3000)");
            Thread.Sleep(2000);

            driver.FindElement(txtEngagementsDiscussed).SendKeys(engName);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"(//div[@data-name='{engName}'])[2]")).Click();
            Thread.Sleep(2000);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
        }

        public void CreateNewActivityWithCampaign(string file)
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
            string companyDiscussed = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 7);
            string oppDiscussed = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 8);

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

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            driver.FindElement(txtCompanyDiscussed).SendKeys(companyDiscussed);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//div[@data-name='{companyDiscussed}']")).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtOpportunitiesDiscussed).SendKeys(oppDiscussed);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"(//div[@data-name='{oppDiscussed}'])[1]")).Click();
            Thread.Sleep(2000);

            string campaign = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 9);

            js.ExecuteScript("window.scrollTo(0,2500)");
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//span[text()='Related Campaigns']/..")).Click();
            driver.FindElement(txtCampaignsDiscussed).SendKeys(campaign);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"(//div[@data-name='{campaign}'])[1]")).Click();
            Thread.Sleep(2000);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            driver.FindElement(btnSave).Click();
            Thread.Sleep(4000);
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

        public void CreateMultipleActivityFromContactActivityPage(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string type = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 1);
            string subject = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 2);
            string industryGroup = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 3);
            string productType = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 4);
            string description = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 5);
            string meetingNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 6);

            //Click on Add Activity button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnAddActivity));
            driver.FindElement(btnAddActivity).Click();
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblAddNewActivity, 60);

            //Enter Activity details
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(2);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("MMM d, yyyy"));
            Thread.Sleep(2000);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,500)");
            Thread.Sleep(3000);

            driver.FindElement(drpdownIndustryGroup).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{industryGroup}']")).Click();
            Thread.Sleep(3000);

            driver.FindElement(drpdownProductType).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{productType}']")).Click();
            Thread.Sleep(5000);
            driver.FindElement(txtareaDescription).SendKeys(description);
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(meetingNotes);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            Thread.Sleep(2000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
        }

        public void CreateMultipleActivityForHLEmployee(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string type = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 1);
            string subject = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 2);
            string industryGroup = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 3);
            string productType = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 4);
            string description = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 5);
            string meetingNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 6);
            string extAttendee = ReadExcelData.ReadData(excelPath, "Activity", 7);

            //Click on Add Activity button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnAddActivity));
            driver.FindElement(btnAddActivity).Click();
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblAddNewActivity, 60);

            //Enter Activity details
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(2);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("MMM d, yyyy"));
            Thread.Sleep(2000);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,500)");
            Thread.Sleep(3000);

            driver.FindElement(drpdownIndustryGroup).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{industryGroup}']")).Click();
            Thread.Sleep(3000);

            driver.FindElement(drpdownProductType).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{productType}']")).Click();
            Thread.Sleep(5000);
            driver.FindElement(txtareaDescription).SendKeys(description);
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(meetingNotes);

            //Add External Attendee
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtExternalAttendee));
            driver.FindElement(txtExternalAttendee).SendKeys(extAttendee);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//div[@data-name='{extAttendee}']")).Click();
            Thread.Sleep(2000);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            Thread.Sleep(2000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
        }

        public void CreateNewActivityWithMultipleAttendees(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string type = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 1);
            string subject = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 2);
            string industryGroup = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 3);
            string productType = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 4);
            string description = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 5);
            string meetingNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 6);
            string additionalExtAttendee = ReadExcelData.ReadData(excelPath, "MoreAttendees", 1);
            string additionalHLAttendee = ReadExcelData.ReadData(excelPath, "MoreAttendees", 2);

            //Click on Add Activity button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnAddActivity));
            driver.FindElement(btnAddActivity).Click();
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblAddNewActivity, 60);

            //Enter Activity details
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(2);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("MMM d, yyyy"));
            Thread.Sleep(2000);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,500)");
            Thread.Sleep(3000);

            driver.FindElement(drpdownIndustryGroup).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{industryGroup}']")).Click();
            Thread.Sleep(3000);

            driver.FindElement(drpdownProductType).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{productType}']")).Click();
            Thread.Sleep(5000);
            driver.FindElement(txtareaDescription).SendKeys(description);
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(meetingNotes);

            //Add new External Attendee
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtExternalAttendee));
            driver.FindElement(txtExternalAttendee).SendKeys(additionalExtAttendee);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//div[@data-name='{additionalExtAttendee}']")).Click();
            Thread.Sleep(2000);

            //Update HL Attendee
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtHLAttendee));
            driver.FindElement(txtHLAttendee).SendKeys(additionalHLAttendee);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//div[@data-name='{additionalHLAttendee}']")).Click();
            Thread.Sleep(2000);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            Thread.Sleep(2000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
        }

        public void CreateNewActivityWithDelegates(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string type = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 1);
            string subject = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 2);
            string industryGroup = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 3);
            string productType = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 4);
            string description = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 5);
            string meetingNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 6);
            string additionalExtAttendee = ReadExcelData.ReadData(excelPath, "MoreAttendees", 1);
            string additionalHLAttendee = ReadExcelData.ReadData(excelPath, "MoreAttendees", 2);

            //Click on Add Activity button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnAddActivity));
            driver.FindElement(btnAddActivity).Click();
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblAddNewActivity, 60);

            //Enter Activity details
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(2);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("MMM d, yyyy"));
            Thread.Sleep(2000);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,500)");
            Thread.Sleep(2000);

            driver.FindElement(drpdownIndustryGroup).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{industryGroup}']")).Click();
            Thread.Sleep(3000);

            driver.FindElement(drpdownProductType).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{productType}']")).Click();
            Thread.Sleep(5000);
            driver.FindElement(txtareaDescription).SendKeys(description);
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(meetingNotes);

            //Add new External Attendee
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtExternalAttendee));
            driver.FindElement(txtExternalAttendee).SendKeys(additionalExtAttendee);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//div[@data-name='{additionalExtAttendee}']")).Click();
            Thread.Sleep(2000);

            //Update HL Attendee
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtHLAttendee));
            driver.FindElement(txtHLAttendee).SendKeys(additionalHLAttendee);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//div[@data-name='{additionalHLAttendee}']")).Click();
            Thread.Sleep(2000);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            Thread.Sleep(2000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
        }

        public void CreateNewActivityWithDelegatesAndPrimaryBanker(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string type = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 1);
            string subject = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 2);
            string industryGroup = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 3);
            string productType = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 4);
            string description = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 5);
            string meetingNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 6);
            string additionalExtAttendee = ReadExcelData.ReadData(excelPath, "MoreAttendees", 1);
            string additionalHLAttendee = ReadExcelData.ReadData(excelPath, "MoreAttendees", 2);

            //Click on Add Activity button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnAddActivity));
            driver.FindElement(btnAddActivity).Click();
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblAddNewActivity, 60);

            //Enter Activity details
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(2);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("MMM d, yyyy"));
            Thread.Sleep(2000);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,500)");
            Thread.Sleep(2000);

            driver.FindElement(drpdownIndustryGroup).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{industryGroup}']")).Click();
            Thread.Sleep(3000);

            driver.FindElement(drpdownProductType).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{productType}']")).Click();
            Thread.Sleep(5000);
            driver.FindElement(txtareaDescription).SendKeys(description);
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(meetingNotes);

            //Add new External Attendee
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtExternalAttendee));
            driver.FindElement(txtExternalAttendee).SendKeys(additionalExtAttendee);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//div[@data-name='{additionalExtAttendee}']")).Click();
            Thread.Sleep(2000);

            //Update HL Attendee
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtHLAttendee));
            driver.FindElement(txtHLAttendee).SendKeys(additionalHLAttendee);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//div[@data-name='{additionalHLAttendee}']")).Click();
            Thread.Sleep(2000);

            //Make Banker as Primary
            try
            {
                driver.FindElement(By.XPath($"(//h2/span/a[text()='{additionalHLAttendee}']/../../..//span[text()='Primary']/../span)[1]")).Click();
                Thread.Sleep(2000);
            }
            catch(Exception)
            {

            }

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            Thread.Sleep(2000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
        }

        public void CreateActivityFromContactActivityPage(string file)
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

            //Click on Add Activity button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnAddActivity));
            driver.FindElement(btnAddActivity).Click();
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblAddNewActivity, 60);

            //Enter Activity details
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(2);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("MMM d, yyyy"));
            Thread.Sleep(2000);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,500)");
            Thread.Sleep(3000);

            driver.FindElement(drpdownIndustryGroup).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{industryGroup}']")).Click();
            Thread.Sleep(3000);

            driver.FindElement(drpdownProductType).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{productType}']")).Click();
            Thread.Sleep(5000);
            driver.FindElement(txtareaDescription).SendKeys(description);
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(meetingNotes);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            Thread.Sleep(2000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
        }

        public void AddAnActivityWithNewCompany(string file)
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
            string companyType = ReadExcelData.ReadData(excelPath, "Company", 1);
            string companyName = ReadExcelData.ReadData(excelPath, "Company", 2);
            string companyCountry = ReadExcelData.ReadData(excelPath, "Company", 3);

            //Click on Add Activity button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnAddActivity));
            driver.FindElement(btnAddActivity).Click();
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblAddNewActivity, 60);

            //Enter Activity details
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(2);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("MMM d, yyyy"));
            Thread.Sleep(2000);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,500)");
            Thread.Sleep(3000);

            driver.FindElement(drpdownIndustryGroup).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{industryGroup}']")).Click();
            Thread.Sleep(3000);

            driver.FindElement(drpdownProductType).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{productType}']")).Click();
            Thread.Sleep(5000);
            driver.FindElement(txtareaDescription).SendKeys(description);
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(meetingNotes);

            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            //Click Create New Company button
            WebDriverWaits.WaitUntilEleVisible(driver, btnCreateNewCompany, 60);
            driver.FindElement(btnCreateNewCompany).Click();
            Thread.Sleep(5000);

            //Enter Company Type
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownCompanyType, 60);
            driver.FindElement(dropdownCompanyType).Click();
            Thread.Sleep(1000);
            driver.FindElement(dropdownCompanyType).SendKeys(companyType);
            driver.FindElement(dropdownCompanyType).SendKeys(Keys.Enter);
            Thread.Sleep(3000);

            //Entr Company Name
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName, 60);
            driver.FindElement(txtCompanyName).SendKeys(companyName);
            Thread.Sleep(2000);

            //Enter Company Location
            driver.FindElement(txtCompanyCountry).Click();
            Thread.Sleep(1000);
            driver.FindElement(txtCompanyCountry).SendKeys(companyCountry);
            driver.FindElement(txtCompanyCountry).SendKeys(Keys.Enter);
            Thread.Sleep(3000);

            //Click Save on company page
            driver.FindElement(By.XPath("(//button[text()='Save'])[2]")).Click();
            Thread.Sleep(2000);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            Thread.Sleep(2000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyValidationMessageWithoutExternalAttendeeAndCompany(string file, string contact)
        {
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string type = ReadExcelData.ReadData(excelPath, "Activity", 1);
            string subject = ReadExcelData.ReadData(excelPath, "Activity", 2);
            string description = ReadExcelData.ReadData(excelPath, "Activity", 5);
            string meetingNotes = ReadExcelData.ReadData(excelPath, "Activity", 6);
            string msgExcel = ReadExcelData.ReadData(excelPath, "Validations", 1);

            //Enter Activity details
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(2);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("MMM d, yyyy"));
            Thread.Sleep(2000);

            driver.FindElement(txtareaDescription).SendKeys(description);
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(meetingNotes);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,1000)");
            Thread.Sleep(2000);

            //Remove External Attendee
            driver.FindElement(By.XPath($"//button[@title='Remove {contact}']")).Click();
            Thread.Sleep(2000);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);

            if(msgExcel == driver.FindElement(By.XPath("//div[@class='slds-text-heading_large slds-text-color_destructive']")).Text)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyValidationsOnClickingSaveWithoutFillingInMandatoryFields(string file)
        {
            bool result = false;
            Thread.Sleep(3000);

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string subExcel = ReadExcelData.ReadData(excelPath, "Validations", 2);
            string dateExcel = ReadExcelData.ReadData(excelPath, "Validations", 3);

            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtEndDate).Clear();
            Thread.Sleep(3000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);

            string msg1 = driver.FindElement(By.XPath("(//span[text()='Subject'])[2]/..")).Text;
            string msg2 = driver.FindElement(By.XPath("(//div[@data-error-message])[3]")).Text;
            string msg3 = driver.FindElement(By.XPath("(//div[@data-error-message])[6]")).Text;

            if(msg1.Contains(subExcel) && msg2.Contains("Complete this field with format") && msg3.Contains("Complete this field with format"))
            {
                result = true;
            }
            return result;
        }

        public bool VerifyValidationMessageThatAppearsIfEndDateIsLessThanFromDate(string file)
        {
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string type = ReadExcelData.ReadData(excelPath, "Activity", 1);
            string subject = ReadExcelData.ReadData(excelPath, "Activity", 2);
            string msg1 = ReadExcelData.ReadData(excelPath, "Validations", 4);
            string msg2 = ReadExcelData.ReadData(excelPath, "Validations", 5);

            //Enter Activity details
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).Clear();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(-2);
            driver.FindElement(txtEndDate).Clear();
            driver.FindElement(txtEndDate).SendKeys(setDate.ToString("MMM d, yyyy"));
            Thread.Sleep(2000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);

            if(msg1 == driver.FindElement(By.XPath("(//ul[@class='slds-list_dotted']/li)[1]/div")).Text && msg2 == driver.FindElement(By.XPath("(//ul[@class='slds-list_dotted']/li)[2]/div")).Text)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyValidationMessageIfBankerTriesToSaveActivityWithoutHLAttendee(string file, string hlAttendee)
        {
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string type = ReadExcelData.ReadData(excelPath, "Activity", 1);
            string subject = ReadExcelData.ReadData(excelPath, "Activity", 2);
            string description = ReadExcelData.ReadData(excelPath, "Activity", 5);
            string meetingNotes = ReadExcelData.ReadData(excelPath, "Activity", 6);
            string msgExcel = ReadExcelData.ReadData(excelPath, "Validations", 6);

            //Enter Activity details
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(2);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("MMM d, yyyy"));
            Thread.Sleep(2000);

            driver.FindElement(txtareaDescription).SendKeys(description);
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(meetingNotes);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,1000)");
            Thread.Sleep(2000);

            //Remove HL Attendee
            driver.FindElement(By.XPath($"//button[@title='Remove {hlAttendee}']")).Click();
            Thread.Sleep(2000);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);

            if(msgExcel == driver.FindElement(By.XPath("//div[@class='slds-text-heading_large slds-text-color_destructive']")).Text)
            {
                result = true;
            }
            return result;
        }
    }
}