using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium.Interactions;
using System.Linq;
using System.Web;
using MongoDB.Driver;

namespace SF_Automation.Pages.TimeRecordManager
{
    class TimeRecordManagerEntryPage : BaseClass
    {

        ExtentReport extentReports = new ExtentReport();


        By tabStaffTimeSheet = By.CssSelector("li[id*='staff'] > a");
        By tabWeeklyEntryMatrix = By.CssSelector("li[id*='mass'] > a");
        By tabSummaryLogs = By.CssSelector("li[id*='summary'] > a");
        By tabDetailLogs = By.CssSelector("li[id*='view'] > a");
        By tabWeeklyOverview = By.CssSelector("li[title*='Weekly Overview'] > a");
        By comboSelectProject = By.CssSelector("select[class='slds-select']");
        By comboSelectProjectN = By.XPath("//input[contains(@placeholder,'Type to filter projects')]");
        By txtEnterSundayTime = By.CssSelector("table > tr > td:nth-child(2) > div[class='activityRecordEntry'] > div > div > div > input");
        By comboSelectActivity = By.CssSelector("table > tr > td:nth-child(2) > div[class='activityRecordEntry'] > div > div:nth-child(2) > div > select");
        By comboLogActivity = By.CssSelector("div[class*='medium'] > select[class*='uiInput--select']");
        By btnClearTimeEntry = By.CssSelector("div[data-key*='a4C'] button[class*='slds-button']");
        By valTimeRecordManagerTitle = By.CssSelector("div[class='slds-text-heading--medium']");
        By comboDefaultSelectProject = By.CssSelector("select[class='slds-select'] > option:nth-child(1)");
        By txtSummaryLogsAddRecordDate = By.CssSelector("input[class*='date input']");
        By txtEnterSummaryLogEntryTime = By.CssSelector("input[class*='uiInput--input']");
        By linkSalesforceAdministrator = By.XPath("//*[text()='Salesforce Administrator']/../../p[@data-aura-rendered-by='983:78;a']/a");
        By valSelectedStaffTitle = By.CssSelector("div[class='timeSheet'] > div[class*='heading']");
        By btnAdd = By.CssSelector("span[dir='ltr']");
        By valProjectOrEngagement = By.CssSelector("tr[class*='parent'] > td:nth-child(2)");
        By valActivity = By.CssSelector("tr[class*='parent'] > td:nth-child(3)");
        By valActivityInDetailLogs = By.CssSelector("tr[class*='parent'] > td:nth-child(3) > select > option[selected='selected']");
        By valDefaultDollar = By.CssSelector("tr[class*='parent'] > td:nth-child(5) > span:nth-child(2)");
        By valDefaultRateDetailLogs = By.CssSelector("tr[class*='parent'] > td:nth-child(5) > div > input");
        By valEnteredHours = By.CssSelector("tr[class*='parent'] > td:nth-child(4) > span");
        By valEnteredHoursInDetailLogs = By.CssSelector("tr[class*='parent'] > td:nth-child(4) > div > input");
        By valTotalAmount = By.CssSelector("tr[class*='parent'] > td:nth-child(6) > span:nth-child(2)");
        By btnCross = By.CssSelector("div[data-key*='a4C'] button[class*='slds-button']");
        By btnCrossDeleteRecord = By.CssSelector("td[class*='slds-cell-shrink'] > button[class*='slds-button']");
        By tableSummaryLog = By.CssSelector("table[class='slds-table'] > tbody");
        By tableAddedProject = By.CssSelector("table[class*='slds-table'] > tbody");
        By comboBoxProjectInLog = By.CssSelector("select[class='slds-select'] > option");
        By txtWeeklyEntry = By.XPath("//div[starts-with(@data-key,'a4C')]//parent::div/div/input");
        By drpdownFuturePeriod = By.XPath("//div[@data-aura-class='cTimeRecordPeriodPicker']/select/option[@selected='selected']//preceding::option");////div[@data-aura-class='cHL_LightningComponent cTimeRecordPeriodPicker']/select/option[@selected='selected']//preceding::option
        By txtCurrentTimePeriod = By.CssSelector("div[class*='cTimeRecordPeriodPicker']");
        By drpdwnSelectPreSetTemplate = By.CssSelector("select[class*='slds-input']");
        By EnterDateSummaryLog = By.CssSelector("input[class*='date input']");
        By EnterHoursSummaryLog = By.CssSelector("input[placeholder='hrs']");
        By AddBtnSummaryLog = By.CssSelector("span[class*='bBody']");
        By TxtSuccessMsg = By.XPath("//h4[text()='Success -']");

        By tabBetaSummaryLogs = By.CssSelector("li[title='Summary'] > a");
        By tabBetaDetailLogs = By.CssSelector("li[title='Details'] > a");
        By comboSelectProjectName = By.XPath("(//div[@role='listbox']//li)[1]//span//span");
        By optionProject = By.XPath("//div//label[text()='Select Project']//following::div//input");
        By option = By.XPath("//div//label[text()='Select Project']//following::div//input");
        By txtTimeClockRecorder = By.XPath("//*[contains(@title,'Time Clock Recorder')]");
        By comboValueLogActivity = By.XPath("(//div[@id='tab-view']//select[contains(@class,'uiInput--select')])[1]//option[@selected='selected']");
        By comboLogActivity1 = By.XPath("(//div[@id='tab-view']//select[contains(@class,'uiInput--select')])[1]");
        By txtEnterSummaryLogEntryTime1 = By.XPath("(//input[contains(@class,'uiInput--input')])[1]");
        By txtWeeklyOverview = By.XPath("//div[contains(@class,'StaffTimeSheet')]//span[text()='Weekly Overview']");
        By tableWeeklyOverview = By.XPath("//table[contains(@class,'slds-table')]//td[contains(@class,'cell-project')]");
        By imgSpinningLoader = By.XPath("//div[@class='loading']");
        By textSuccessMsg = By.XPath("//div[contains(@class,'uiMessage')]//h4//following-sibling::span");
        By txtTimeRecordUserName = By.XPath("//div[contains(@class,'TimeRecordManager')]//div[@class='timeSheet']/div[1]");
        By frameTimeRecordPage = By.XPath("//iframe[@title='accessibility title']"); //iframe[@title='Salesforce - Unlimited Edition']"); 
        By txtdefaultTimeRecordPeriod = By.XPath("//table//div[contains(@class,'TimeRecordPeriodPicker')]//select/option[1]");
        By txtSelectedStaffName = By.XPath("//div[@class='timeSheet']/div[contains(@class,'heading')]");
        By txtDetailLogsActualHours = By.CssSelector("tr[class*='parent'] > td:nth-child(4) > div > input");
        By txtSummaryLogsActualHours = By.CssSelector("tr[class*='parent'] > td:nth-child(4) > span:nth-child(1)");
        By txtTimeClockRecorderComments = By.XPath("//textarea[@placeholder='Enter Comments']");
        By txtLogsComments = By.XPath("//div[contains(@class,'AddTimeRecordRow')]//textarea");
        By comboOptionActivity = By.CssSelector("table > tr > td:nth-child(2) > div[class='activityRecordEntry'] > div > div:nth-child(2) > div > select > option");
        By comboOptionsLogsActivity = By.CssSelector("div[class*='medium'] > select[class*='uiInput--select'] > option");
        By txtTotalForcastedHoursL = By.XPath("//div[@class='staffTimeSheetWeeklyMassEdit']/table[2]//tr/td/div[contains(text(),'Forecasted')]/../..//td[2]/div");
        By txtdefaultTimeRecordPeriodL = By.XPath("//table//div[contains(@class,'TimeRecordPeriodPicker')]//select/option[@selected='selected']");
        By comboTimeRecordPeriodL = By.XPath("//table//div[contains(@class,'TimeRecordPeriodPicker')]//select");
        public void GoToWeeklyEntryMatrix()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabWeeklyEntryMatrix);
            driver.FindElement(tabWeeklyEntryMatrix).Click();
            Thread.Sleep(20000);
        }

        public void EnterWeeklyEntryMatrix(string selectProject, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            Thread.Sleep(6000);
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabWeeklyEntryMatrix);
            driver.FindElement(tabWeeklyEntryMatrix).Click();
            Thread.Sleep(2000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);                
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();                
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSundayTime);
            driver.FindElement(txtEnterSundayTime).SendKeys(ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 2));
            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectActivity);
            driver.FindElement(comboSelectActivity).SendKeys(ReadExcelData.ReadData(excelPath, "WeeklyEntryMatrix", 3));
        }
        public void GoToSummaryLogs()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabSummaryLogs);
            driver.FindElement(tabSummaryLogs).Click();
            Thread.Sleep(2000);
            driver.FindElement(tabSummaryLogs).Click();
            Thread.Sleep(2000);
        }

        public void GoToSummaryLog()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabSummaryLogs);
            driver.FindElement(tabSummaryLogs).Click();
            Thread.Sleep(5000);
        }

        public void EnterSummaryLogs(string selectProject, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, tabSummaryLogs);
            driver.FindElement(tabSummaryLogs).Click();
            Thread.Sleep(2000);

            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);                
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();                
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }

            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(ReadExcelData.ReadData(excelPath, "SummaryLogs", 2));

            WebDriverWaits.WaitUntilEleVisible(driver, comboLogActivity);
            driver.FindElement(comboLogActivity).SendKeys(ReadExcelData.ReadData(excelPath, "SummaryLogs", 3));
        }
        public void BetaUserEnterSummaryLogs(string selectProject, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, tabBetaSummaryLogs);
            driver.FindElement(tabBetaSummaryLogs).Click();
            Thread.Sleep(2000);

            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();                
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }

            WebDriverWaits.WaitUntilEleVisible(driver, comboLogActivity);
            driver.FindElement(comboLogActivity).SendKeys(ReadExcelData.ReadData(excelPath, "SummaryLogs", 3));

            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(ReadExcelData.ReadData(excelPath, "SummaryLogs", 2));
        }
        public void EnterSummaryLogs1(string selectProject, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, tabSummaryLogs);
            driver.FindElement(tabSummaryLogs).Click();
            Thread.Sleep(2000);

            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);

            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
            driver.FindElement(comboSelectProject).SendKeys(selectProject);

            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(ReadExcelData.ReadData(excelPath, "SummaryLogs", 2));

        }
        public void ClickAddButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdd);
            driver.FindElement(btnAdd).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, tableSummaryLog, 120);
        }

        public void GoToDetailLogs()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, tabDetailLogs);
            driver.FindElement(tabDetailLogs).Click();
            Thread.Sleep(5000);
        }
        public void BetaUserEnterDetailLogs(string selectProject, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, tabBetaDetailLogs);
            driver.FindElement(tabBetaDetailLogs).Click();
            Thread.Sleep(5000);

            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
               
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();                
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
           
            WebDriverWaits.WaitUntilEleVisible(driver, comboLogActivity);
            driver.FindElement(comboLogActivity).SendKeys(ReadExcelData.ReadData(excelPath, "DetailLogs", 3));

            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(ReadExcelData.ReadData(excelPath, "DetailLogs", 2));
        }

        public void EnterDetailLogs(string selectProject, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, tabDetailLogs);
            driver.FindElement(tabDetailLogs).Click();
            Thread.Sleep(5000);

            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                //extracode
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
                //
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }

            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(ReadExcelData.ReadData(excelPath, "DetailLogs", 2));

            WebDriverWaits.WaitUntilEleVisible(driver, comboLogActivity);
            driver.FindElement(comboLogActivity).SendKeys(ReadExcelData.ReadData(excelPath, "DetailLogs", 3));
        }

        public void EnterDetailLogs1(string selectProject, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, tabDetailLogs);
            driver.FindElement(tabDetailLogs).Click();
            Thread.Sleep(5000);

            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);

            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
            driver.FindElement(comboSelectProject).SendKeys(selectProject);

            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(ReadExcelData.ReadData(excelPath, "DetailLogs", 2));
        }

        public string GetSundayTimeEntry()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSundayTime, 120);
            string sundayTimeEntryValue = driver.FindElement(txtEnterSundayTime).GetAttribute("value");
            return sundayTimeEntryValue;
        }

        public string GetSummaryLogsTimeEntry()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime, 80);
            string SummaryLogEntryTime = driver.FindElement(txtEnterSummaryLogEntryTime).GetAttribute("value");
            return SummaryLogEntryTime;
        }
        public string GetDetailLogsTimeEntry()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime, 80);
            string DetailLogEntryTime = driver.FindElement(txtEnterSummaryLogEntryTime).GetAttribute("value");
            return DetailLogEntryTime;
        }
        public void ClearTimeRecord()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnClearTimeEntry);
            driver.FindElement(btnClearTimeEntry).Click();
            Thread.Sleep(5000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        public string GetPeopleOrUserName(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string peopleFromExcel = ReadExcelData.ReadData(excelPath, "Users", 2).Split('*')[0].Trim();
            return peopleFromExcel;
        }

        public string GetTimeRecordManagerTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTimeRecordManagerTitle, 80);
            string TimeRecordManagerTitle = driver.FindElement(valTimeRecordManagerTitle).Text.Split(':')[0].Trim();
            return TimeRecordManagerTitle;
        }

        public string GetDefaultSelectedProjectOption()
        {
            By dropDownSelectProject = By.XPath("//div//label[text()='Select Project']");
            WebDriverWaits.WaitUntilEleVisible(driver, dropDownSelectProject, 80);

            string DefaultSelectProject = driver.FindElement(optionProject).GetAttribute("placeholder");
            return DefaultSelectProject;
        }
        public string GetDefaultSelectedProjectOptionN()
        {
            By dropDownSelectProject = By.XPath("//div//label[text()='Select Project']");
            WebDriverWaits.WaitUntilEleVisible(driver, dropDownSelectProject, 80);
            string DefaultSelectProject = driver.FindElement(option).GetAttribute("placeholder");
            return DefaultSelectProject;
        }

        public void SelectStaffMember(string name)
        {
            Thread.Sleep(8000);
            driver.FindElement(By.XPath($"//a[text()='{name}']")).Click();
            Thread.Sleep(20000);
        }

        public string GetSelectedStaffMember()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSelectedStaffTitle, 80);
            string SelectedStaffTitle = driver.FindElement(valSelectedStaffTitle).Text;
            return SelectedStaffTitle;
        }

        public string GetProjectOrEngagementValue()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valProjectOrEngagement, 80);
            string ProjectOrEngagement = driver.FindElement(valProjectOrEngagement).Text;
            return ProjectOrEngagement;
        }

        public string GetSelectedActivity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valActivity, 80);
            string ActivityValue = driver.FindElement(valActivity).Text;
            return ActivityValue;
        }

        public string GetSelectedActivityOnDetailLog()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valActivityInDetailLogs, 80);
            string ActivityValueInDetail = driver.FindElement(valActivityInDetailLogs).Text;
            return ActivityValueInDetail;
        }

        public string GetDefaultRateForStaff()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultDollar, 80);
            string DefaultDollar = driver.FindElement(valDefaultDollar).Text;
            return DefaultDollar;
        }

        public string GetEnteredHoursInDetailLogs()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEnteredHoursInDetailLogs, 80);
            string enteredHours = driver.FindElement(valEnteredHoursInDetailLogs).GetAttribute("value");
            return enteredHours;
        }

        public string GetDefaultRateForStaffInDetailLogs()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultRateDetailLogs, 80);
            string DefaultDollar = driver.FindElement(valDefaultRateDetailLogs).GetAttribute("value");
            return DefaultDollar;
        }

        public string GetEnteredHoursInSummaryLog()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEnteredHours, 80);
            string EnteredHours = driver.FindElement(valEnteredHours).Text;
            return EnteredHours;
        }

        public double GetDefaultRateForStaffValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultDollar, 80);
            return double.Parse(driver.FindElement(valDefaultDollar).Text);
        }

        public double GetDefaultRateForStaffValueInDetailLogs()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultRateDetailLogs, 80);
            return double.Parse(driver.FindElement(valDefaultRateDetailLogs).GetAttribute("value"));
        }

        public double GetEnteredHoursInSummaryLogValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEnteredHours, 80);
            return double.Parse(driver.FindElement(valEnteredHours).Text);
        }

        public double GetEnteredHoursInDetailLogValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEnteredHoursInDetailLogs, 80);
            return double.Parse(driver.FindElement(valEnteredHoursInDetailLogs).GetAttribute("value"));
        }

        public void EditEnteredHoursInDetailLog(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            Thread.Sleep(6000);
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, valEnteredHoursInDetailLogs, 80);
            driver.FindElement(valEnteredHoursInDetailLogs).Clear();
            driver.FindElement(valEnteredHoursInDetailLogs).SendKeys(ReadExcelData.ReadData(excelPath, "Update_Hours", 1));
            Thread.Sleep(2000);
        }

        public double GetTotalAmount()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTotalAmount, 80);
            return double.Parse(driver.FindElement(valTotalAmount).Text);
        }

        //Delete time entry from weekly entry matrix
        public void DeleteTimeEntry()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabWeeklyEntryMatrix);
            driver.FindElement(tabWeeklyEntryMatrix).Click();
            Thread.Sleep(2000);
            IList<IWebElement> elements = driver.FindElements(btnCross);
            for (int i = 0; i < elements.Count; i++)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnCross);
                driver.FindElement(btnCross).Click();
                Thread.Sleep(5000);

                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(2000);
            }
        }

        public void RemoveRecordFromDetailLogs()
        {
            IList<IWebElement> elements = driver.FindElements(btnCrossDeleteRecord);
            for (int i = 0; i < elements.Count; i++)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnCrossDeleteRecord);
                driver.FindElement(btnCrossDeleteRecord).Click();
                Thread.Sleep(3000);

                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(5000);
            }
        }

        public bool VerifyProjectDisableAfterOppToEngagementConversion()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
            bool isDisabled = CustomFunctions.isAttributePresent(driver, driver.FindElement(comboSelectProject), "disabled");
            return isDisabled;
        }

        public string VerifyEngagementProjectExist(string engagementProject)
        {
            //IWebElement 
            IList<IWebElement> elements = driver.FindElements(comboSelectProject);

            if (elements[1].Text.Contains(engagementProject))
            {
                return "Project is present";
            }
            else
            {
                return "Project not present";
            }
        }

        public string VerifyEngagementProjectExistInLogs(string engagementProject)
        {
            //IWebElement 
            IWebElement element = driver.FindElement(comboSelectProject);

            if (element.Text.Contains(engagementProject))
            {
                return "Project is present";
            }
            else
            {
                return "Project not present";
            }
        }

        public void SelectProjectWeeklyEntryMatrix(string selectProject, string file)
        {
            Thread.Sleep(10000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                //extracode

                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
                //
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
            Thread.Sleep(3000);
        }

        //Get Border color from entered time entry
        public void GetBorderColorTimeEntry(string weekday)
        {

            Thread.Sleep(20000);

            switch (weekday)
            {
                case "Mon":

                    string color = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    Console.WriteLine(color);
                    Assert.AreEqual(color, "rgb(194, 57, 52)");
                    break;
                case "Tue":

                    string color1 = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    Console.WriteLine(color1);
                    Assert.AreEqual(color1, "rgb(194, 57, 52)");
                    break;
                case "Wed":

                    string color2 = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    Console.WriteLine(color2);
                    Assert.AreEqual(color2, "rgb(194, 57, 52)");
                    break;


                case "Thu":

                    string color3 = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    Console.WriteLine(color3);
                    Assert.AreEqual(color3, "rgb(194, 57, 52)");
                    break;
                case "Fri":

                    string color4 = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    Console.WriteLine(color4);
                    Assert.AreEqual(color4, "rgb(194, 57, 52)");
                    break;

                case "Sat":

                    string color5 = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    Console.WriteLine(color5);
                    Assert.AreEqual(color5, "rgb(194, 57, 52)");
                    break;
                case "Sun":

                    string color6 = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    Console.WriteLine(color6);
                    Assert.AreEqual(color6, "rgb(194, 57, 52)");
                    break;
            }
        }

        //Log Future Date Hours
        public string LogFutureDateHours(string file)
        {

            DateTime Time = DateTime.Now.AddDays(1);
            string format = "ddd";
            string week = Time.ToString(format);

            Console.WriteLine(Time.ToString(format));

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            Thread.Sleep(6000);
            string excelPath = dir + file;


            string txtHours = ReadExcelData.ReadData(excelPath, "Update_Hours", 1).ToString();
            Console.WriteLine(txtHours);


            switch (Time.ToString(format))
            {
                case "Mon":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div[2]/div/select")), 2);

                    break;

                case "Tue":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div[2]/div/select")), 2);

                    break;

                case "Wed":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div[2]/div/select")), 2);
                    break;


                case "Thu":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div[2]/div/select")), 2);

                    break;

                case "Fri":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div[2]/div/select")), 2);

                    break;


                case "Sat":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div[2]/div/select")), 2);

                    break;

                case "Sun":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div[2]/div/select")), 2);

                    break;
            }
            return week;

        }

        //Compare weekly time entry 
        public void CompareWeeklyTimeEntry(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, tabWeeklyEntryMatrix);
            driver.FindElement(tabWeeklyEntryMatrix).Click();
            Thread.Sleep(2000);
            IList<IWebElement> elements = driver.FindElements(btnCross);

            WebDriverWaits.WaitUntilEleVisible(driver, txtWeeklyEntry);
            string txt = driver.FindElement(txtWeeklyEntry).GetAttribute("value");
            string ExlTimer = ReadExcelData.ReadDataMultipleRows(excelPath, "Update_Timer", 2, 2).ToString();
            Assert.AreEqual(txt, ExlTimer);



        }

        public void SelectFutureTimePeriod()
        {

            driver.FindElement(txtCurrentTimePeriod).Click();
            driver.FindElement(drpdownFuturePeriod).Click();
        }

        //Verify activity drop down for future time period
        public void VerifyActivityDropDownForFuturePeriod(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, drpdwnSelectPreSetTemplate);
            IWebElement recordDropdown = driver.FindElement(drpdwnSelectPreSetTemplate);
            SelectElement select = new SelectElement(recordDropdown);
            int RowPreSetTemplateList = ReadExcelData.GetRowCount(excelPath, "Activity_List");

            for (int i = 2; i <= RowPreSetTemplateList; i++)
            {
                IList<IWebElement> options = select.Options;
                IWebElement PReSetTemplateOption = options[i - 2];
                string preSetListExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity_List", i, 1);
                Assert.AreEqual(PReSetTemplateOption.Text, preSetListExl);
            }
        }

        //Log Current Date Hours
        public string LogCurrentDateHours(string file)
        {
            DateTime Time = DateTime.Now.AddDays(0);
            string format = "ddd";
            string week = Time.ToString(format);

            Console.WriteLine(Time.ToString(format));

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            Thread.Sleep(6000);
            string excelPath = dir + file;

            string txtHours = ReadExcelData.ReadData(excelPath, "Update_Timer", 1).ToString();
            Console.WriteLine(txtHours);

            switch (Time.ToString(format))
            {
                case "Mon":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div[2]/div/select")), 1);//2
                    Thread.Sleep(5000);
                    break;

                case "Tue":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div[2]/div/select")), 1);//2
                    Thread.Sleep(5000);
                    break;

                case "Wed":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div[2]/div/select")), 1);//2
                    Thread.Sleep(5000);
                    break;


                case "Thu":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div[2]/div/select")), 1);//2
                    Thread.Sleep(5000);
                    break;

                case "Fri":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div[2]/div/select")), 1);//2
                    Thread.Sleep(5000);
                    break;


                case "Sat":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div[2]/div/select")), 1);//2
                    Thread.Sleep(5000);
                    break;

                case "Sun":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div[2]/div/select")), 1);//2
                    Thread.Sleep(5000);
                    break;
            }
            return week;
        }

        public void EnterSummaryLogHours(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string TodayDate = DateTime.Now.AddDays(0).ToString("MMM dd, yyyy");
            driver.FindElement(EnterDateSummaryLog).SendKeys(TodayDate);

            //WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
            //driver.FindElement(comboSelectProject).Click();
            //driver.FindElement(comboSelectProject).SendKeys(ReadExcelData.ReadData(excelPath, "Project_Title", 1));
            //extracode
            //WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
            //driver.FindElement(comboSelectProjectName).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                Thread.Sleep(2000);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(ReadExcelData.ReadData(excelPath, "Project_Title", 1));
                //extracode
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
                //
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(ReadExcelData.ReadData(excelPath, "Project_Title", 1));
            }
            WebDriverWaits.WaitUntilEleVisible(driver, comboLogActivity);
            driver.FindElement(comboLogActivity).SendKeys(ReadExcelData.ReadData(excelPath, "Project_Title", 2));
            driver.FindElement(EnterHoursSummaryLog).SendKeys(ReadExcelData.ReadData(excelPath, "Update_Hours", 2));
            driver.FindElement(AddBtnSummaryLog).Click();
            Thread.Sleep(5000);
        }

        public bool VerifySuccessMsgDisplay()
        {
            try
            {
                driver.FindElement(TxtSuccessMsg);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void ClickWeeklyEntryMatrixTab()
        {
        Retry: try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tabWeeklyEntryMatrix);
                driver.FindElement(tabWeeklyEntryMatrix).Click();
                CustomFunctions.MoveToElement(driver, driver.FindElement(comboSelectProjectN));
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                goto Retry;
            }
        }
        public void ClickTimeClockRecorderTab()
        {
            driver.FindElement(txtTimeClockRecorder).Click();
            Thread.Sleep(2000);
        }
        public void ClickSummaryLogsTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabSummaryLogs);
            driver.FindElement(tabSummaryLogs).Click();
            Thread.Sleep(2000);
        }
        public void ClickDetailLogsTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabDetailLogs);
            driver.FindElement(tabDetailLogs).Click();
            Thread.Sleep(2000);
        }



        public void ClickWeeklyOverviewTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtWeeklyOverview);
            driver.FindElement(txtWeeklyOverview).Click();
            Thread.Sleep(2000);
        }
        public bool IsProjectSelected(string value)
        {
            try
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(comboSelectProjectN));
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(value);
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool IsComboSelectProjectDisplayed()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                return driver.FindElement(comboSelectProject).Displayed;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public string SearchProjectandGetFullName(string value)
        {
            try
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(comboSelectProjectN));
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(value);
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                return driver.FindElement(comboSelectProjectName).Text;
            }
            catch (Exception e)
            {
                return "No Item Found";
            }
        }

        public bool IsActivityListDisplayed(string selectProject, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName, 5);
                driver.FindElement(comboSelectProjectName).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject, 5);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }


            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectActivity, 5);
                return driver.FindElement(comboSelectActivity).Displayed;
            }
            catch (Exception e) { return false; }
        }
        public string GetTimeRecordUserNameLV()
        {
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtTimeRecordUserName, 30);
            string name=  driver.FindElement(txtTimeRecordUserName).Text;
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
            return name;
        }
        
        public string  GetDefaultTimeRecordPeriodLV()
        {
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, txtdefaultTimeRecordPeriodL, 30);
            string defaultPeriod = driver.FindElement(txtdefaultTimeRecordPeriodL).Text;
            string startDate = defaultPeriod.Split(' ')[0];
            driver.SwitchTo().DefaultContent();
            return startDate;
        }       

        public void DeleteTimeEntryLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, tabWeeklyEntryMatrix);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tabWeeklyEntryMatrix));
            driver.FindElement(tabWeeklyEntryMatrix).Click();
            Thread.Sleep(5000);
            driver.FindElement(tabWeeklyEntryMatrix).Click();

            //WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(2000);
            IList<IWebElement> elements = driver.FindElements(btnCross);
            for (int i = 0; i < elements.Count; i++)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnCross);
                driver.FindElement(btnCross).Click();
                Thread.Sleep(5000);

                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
                Thread.Sleep(5000);
            }
            driver.SwitchTo().DefaultContent();
        }
        public void RemoveRecordFromDetailLogsLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            IList<IWebElement> elements = driver.FindElements(btnCrossDeleteRecord);
            for (int i = 0; i < elements.Count; i++)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnCrossDeleteRecord);
                driver.FindElement(btnCrossDeleteRecord).Click();
                Thread.Sleep(3000);

                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(5000);
            }
            driver.SwitchTo().DefaultContent();
        }
        public bool ClickDeleteAndCancel()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            bool entryStatus=true;
            WebDriverWaits.WaitUntilEleVisible(driver, tabWeeklyEntryMatrix);
            driver.FindElement(tabWeeklyEntryMatrix).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            IList<IWebElement> elements = driver.FindElements(btnCross);
            for (int i = 0; i < elements.Count; i++)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnCross);
                driver.FindElement(btnCross).Click();
                Thread.Sleep(3000);

                IAlert alert = driver.SwitchTo().Alert();
                alert.Dismiss();
                Thread.Sleep(5000);
                IList<IWebElement> btnCross1 = driver.FindElements(btnCross);
                if (elements.Count == btnCross1.Count)
                {
                    entryStatus= false;
                    break;
                }
                else
                {
                    entryStatus= true;
                    break;
                }
            }
            driver.SwitchTo().DefaultContent();
            return entryStatus;
        }
        public bool ClickDeleteAndOK()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            bool entryStatus = true;
            WebDriverWaits.WaitUntilEleVisible(driver, tabWeeklyEntryMatrix);
            driver.FindElement(tabWeeklyEntryMatrix).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            IList<IWebElement> elements = driver.FindElements(btnCross);
            for (int i = 0; i < elements.Count; i++)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnCross);
                driver.FindElement(btnCross).Click();
                Thread.Sleep(5000);

                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(5000);
                driver.FindElement(tabWeeklyEntryMatrix).Click();
                WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
                IList<IWebElement> btnCross1 = driver.FindElements(btnCross);
                if (elements.Count == btnCross1.Count)
                {
                    entryStatus = false;
                    break;
                }
                else
                {
                    entryStatus = true;
                    break;
                }
            }
            driver.SwitchTo().DefaultContent();
            return entryStatus;
        }
        public void GoToWeeklyEntryMatrixLV()
        {
            Thread.Sleep(5000);                       
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, tabWeeklyEntryMatrix);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tabWeeklyEntryMatrix));
            Thread.Sleep(5000);
            driver.FindElement(tabWeeklyEntryMatrix).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(10000);
            driver.FindElement(tabWeeklyEntryMatrix).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            driver.SwitchTo().DefaultContent();
        }
        public void GoToSummaryLogLV()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, tabSummaryLogs);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tabSummaryLogs));
            //Thread.Sleep(5000);
            driver.FindElement(tabSummaryLogs).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(5000);
            driver.FindElement(tabSummaryLogs).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(10000);
            driver.SwitchTo().DefaultContent();
        }
        public void GoToDetailLogsLV()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, tabDetailLogs);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tabDetailLogs));
            //Thread.Sleep(5000);
            driver.FindElement(tabDetailLogs).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(5000);
            driver.FindElement(tabDetailLogs).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(10000);
            driver.SwitchTo().DefaultContent();
        }
        public void GoToWeeklyOverviewLV()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, tabWeeklyOverview);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tabWeeklyOverview));
            //Thread.Sleep(5000);
            driver.FindElement(tabWeeklyOverview).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(5000);
            driver.FindElement(tabWeeklyOverview).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(10000);
            driver.SwitchTo().DefaultContent();
        }
        public string EnterSummaryLogsHoursLV(string selectProject, string activity, string hours)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN,20);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                //extracode
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName,20);
                driver.FindElement(comboSelectProjectName).Click();                
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject,20);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, comboLogActivity,20);
            driver.FindElement(comboLogActivity).SendKeys(activity);
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime,20);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(hours);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdd,20);
            driver.FindElement(btnAdd).Click();
            
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tableSummaryLog, 20);
                CustomFunctions.MoveToElement(driver, driver.FindElement(tableSummaryLog));
                WebDriverWaits.WaitUntilEleVisible(driver, textSuccessMsg);
                CustomFunctions.MoveToElement(driver, driver.FindElement(textSuccessMsg));
                string txtMsg = driver.FindElement(textSuccessMsg).Text.Trim();
                driver.SwitchTo().DefaultContent();
                Thread.Sleep(2000);
                return txtMsg;
            }
            catch{ return "Records is Not Added"; }
            
        }
        public void EnterHoursLogsActivityOptionsLV(string selectProject, string hours)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN,20);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                //extracode
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName,20);
                driver.FindElement(comboSelectProjectName).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime,20);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(hours);
            WebDriverWaits.WaitUntilEleVisible(driver, comboLogActivity,10);
            driver.FindElement(comboLogActivity).Click();
            //Thread.Sleep(2000);
            driver.SwitchTo().DefaultContent();
        }
        
        //Validate te ActivityList
        public bool ValidateActiviyListDropdownOptionsLV(string file,string groupName)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            //WebDriverWaits.WaitUntilEleVisible(driver, comboSelectActivity, 20);
            //driver.FindElement(comboSelectActivity).Click();
            IReadOnlyCollection<IWebElement> valActivityList = driver.FindElements(comboOptionActivity);
            int listCount= valActivityList.Count;
            var actualActivityValue = valActivityList.Select(x => x.Text).ToArray();
            string[] expectedActivityList = new string[listCount];
            int index;
            int ActivityListCount;
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            if(groupName== "Time Tracking Litigation")
            {
                ActivityListCount = ReadExcelData.GetRowCount(excelPath, "ActivityListTTLightning");
                for (int row = 2; row <= ActivityListCount; row++)
                {
                    index = row - 2;
                    expectedActivityList[index] = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityListTTLightning", row, 1);
                }
            }
            if (groupName == "Time Tracking Beta")
            {
                ActivityListCount = ReadExcelData.GetRowCount(excelPath, "ActivityListTTBeta");
                for (int row = 2; row <= ActivityListCount; row++)
                {
                    index = row - 2;
                    expectedActivityList[index] = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityListTTBeta", row, 1);
                }
            }
            bool equal = actualActivityValue.SequenceEqual(expectedActivityList);

            driver.SwitchTo().DefaultContent();
            return equal;
        }
        public bool ValidateActiviyListDropdownOptionsLogsPageLV(string file, string groupName)        
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage)); 
            IReadOnlyCollection<IWebElement> valActivityList = driver.FindElements(comboOptionsLogsActivity);
            int listCount = valActivityList.Count;
            var actualActivityValue = valActivityList.Select(x => x.Text).ToArray();
            string[] expectedActivityList = new string[listCount];
            int index;
            int ActivityListCount;
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            if (groupName == "Time Tracking Litigation")
            {
                ActivityListCount = ReadExcelData.GetRowCount(excelPath, "ActivityListTTLightning");
                for (int row = 2; row <= ActivityListCount; row++)
                {
                    index = row - 2;
                    expectedActivityList[index] = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityListTTLightning", row, 1);
                }
            }
            if (groupName == "Time Tracking Beta")
            {
                ActivityListCount = ReadExcelData.GetRowCount(excelPath, "ActivityListTTBeta");
                for (int row = 2; row <= ActivityListCount; row++)
                {
                    index = row - 2;
                    expectedActivityList[index] = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityListTTBeta", row, 1);
                }
            }
            bool equal = actualActivityValue.SequenceEqual(expectedActivityList);

            driver.SwitchTo().DefaultContent();
            return equal;
        }

        public string EnterSummaryLogsHoursTFRGroupLV(string selectProject, string hours)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                //extracode
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }            
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(hours);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdd);
            driver.FindElement(btnAdd).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tableSummaryLog, 60);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tableSummaryLog));
            WebDriverWaits.WaitUntilEleVisible(driver, textSuccessMsg);
            CustomFunctions.MoveToElement(driver, driver.FindElement(textSuccessMsg));
            string txtMsg = driver.FindElement(textSuccessMsg).Text.Trim();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
            return txtMsg;
        }
        public bool EnterSummaryLogsHoursValidateActivityListLV(string selectProject, string hours)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            bool IsActivityDisplayed = false;
            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                //extracode
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(hours);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectActivity, 5);
                IsActivityDisplayed = driver.FindElement(comboSelectActivity).Displayed;                            
            }
            catch (Exception e)
            {
                IsActivityDisplayed = false;
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdd);
            driver.FindElement(btnAdd).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tableSummaryLog, 60);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tableSummaryLog));
            WebDriverWaits.WaitUntilEleVisible(driver, textSuccessMsg);
            CustomFunctions.MoveToElement(driver, driver.FindElement(textSuccessMsg));            
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
            return IsActivityDisplayed;
        }
        public string EnterDetailLogsHoursLV(string selectProject, string activity, string hours)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN,20);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName,20);
                driver.FindElement(comboSelectProjectName).Click();                
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, comboLogActivity);
            driver.FindElement(comboLogActivity).SendKeys(activity);
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(hours);
                        
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdd);
            driver.FindElement(btnAdd).Click();
            Thread.Sleep(5000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tableSummaryLog,20);
                CustomFunctions.MoveToElement(driver, driver.FindElement(tableSummaryLog));
                WebDriverWaits.WaitUntilEleVisible(driver, textSuccessMsg,20);
                CustomFunctions.MoveToElement(driver, driver.FindElement(textSuccessMsg));
                string txtMsg = driver.FindElement(textSuccessMsg).Text.Trim();
                driver.SwitchTo().DefaultContent();
                Thread.Sleep(2000);
                return txtMsg;
            }
            catch { return "Records is Not Added"; }
            
        }        

        public bool EnterDetailLogsHoursValidateActivityListLV(string selectProject, string hours)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            bool IsActivityDisplayed = false;
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(hours);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectActivity, 5);
                IsActivityDisplayed = driver.FindElement(comboSelectActivity).Displayed;                
            }
            catch (Exception e)
            {                
                IsActivityDisplayed = false;
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdd);
            driver.FindElement(btnAdd).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tableSummaryLog, 60);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tableSummaryLog));
            WebDriverWaits.WaitUntilEleVisible(driver, textSuccessMsg);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
            return IsActivityDisplayed;
        }
        public string EnterDetailLogsHoursTFRGroupLV(string selectProject, string hours)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(hours);

            WebDriverWaits.WaitUntilEleVisible(driver, btnAdd);
            driver.FindElement(btnAdd).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tableSummaryLog);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tableSummaryLog));
            WebDriverWaits.WaitUntilEleVisible(driver, textSuccessMsg);
            CustomFunctions.MoveToElement(driver, driver.FindElement(textSuccessMsg));
            string txtMsg = driver.FindElement(textSuccessMsg).Text.Trim();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
            return txtMsg;
        }
        public string EnterWeeklyOverviewHoursLV(string selectProject, string activity, string hours)
        {
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSummaryLogsAddRecordDate));
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, comboLogActivity);
            driver.FindElement(comboLogActivity).SendKeys(activity);
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(hours);

            WebDriverWaits.WaitUntilEleVisible(driver, btnAdd);
            driver.FindElement(btnAdd).Click();            
            WebDriverWaits.WaitUntilEleVisible(driver, tableWeeklyOverview);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tableWeeklyOverview));
            WebDriverWaits.WaitUntilEleVisible(driver, textSuccessMsg);
            CustomFunctions.MoveToElement(driver, driver.FindElement(textSuccessMsg));
            string txtMsg = driver.FindElement(textSuccessMsg).Text.Trim();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
            return txtMsg;
        }
        public bool EnterWeeklyOverviewHoursValidateActivityListLV(string selectProject, string hours)
        {
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            bool IsActivityDisplayed = false;
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSummaryLogsAddRecordDate));
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(hours);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectActivity, 5);
                IsActivityDisplayed = driver.FindElement(comboSelectActivity).Displayed;                
            }
            catch (Exception e)
            {
                IsActivityDisplayed = false;
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdd);
            driver.FindElement(btnAdd).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tableWeeklyOverview);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tableWeeklyOverview));
            WebDriverWaits.WaitUntilEleVisible(driver, textSuccessMsg);
            CustomFunctions.MoveToElement(driver, driver.FindElement(textSuccessMsg));
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
            return IsActivityDisplayed;
        }
        public string EnterWeeklyOverviewHoursTFRGroupLV(string selectProject, string hours)
        {
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSummaryLogsAddRecordDate));
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(hours);

            WebDriverWaits.WaitUntilEleVisible(driver, btnAdd);
            driver.FindElement(btnAdd).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tableWeeklyOverview);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tableWeeklyOverview));
            WebDriverWaits.WaitUntilEleVisible(driver, textSuccessMsg);
            CustomFunctions.MoveToElement(driver, driver.FindElement(textSuccessMsg));
            string txtMsg = driver.FindElement(textSuccessMsg).Text.Trim();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
            return txtMsg;
        }
        public void UpdateDetailLogsHoursLV(string newActivity, string newHours)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime1);
            driver.FindElement(txtEnterSummaryLogEntryTime1).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime1).SendKeys(newHours);
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboLogActivity1);
            driver.FindElement(comboLogActivity1).SendKeys(newActivity);
            Thread.Sleep(2000);                                    
            driver.FindElement(tabSummaryLogs).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(2000);
            driver.FindElement(tabDetailLogs).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
        }
        public void UpdateDetailLogsHoursTFRGroupLV(string newHours)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));            
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime1);
            driver.FindElement(txtEnterSummaryLogEntryTime1).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime1).SendKeys(newHours);
            Thread.Sleep(2000);
            driver.FindElement(tabWeeklyEntryMatrix).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(2000);
            driver.FindElement(tabDetailLogs).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
        }
        public string GetDetailLogsActivity()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, comboValueLogActivity);
            string Activity = driver.FindElement(comboValueLogActivity).Text;
            driver.SwitchTo().DefaultContent();
            return Activity;
        }
        public string GetDetailLogsHours()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime1);
            string hours= driver.FindElement(txtEnterSummaryLogEntryTime1).GetAttribute("value");
            driver.SwitchTo().DefaultContent();
            return hours;
        }
        public void SelectProjectWeeklyEntryMatrixLV(string selectProject)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            Thread.Sleep(5000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);  
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(3000);
        }
        //Log Current Date Hours
        public string LogCurrentDateHoursLV(string txtHours)
        {
            DateTime Time = DateTime.Now.AddDays(0);
            string format = "ddd";
            string week = Time.ToString(format);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            
            switch (Time.ToString(format))
            {
                case "Mon":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div/div[1]/input[1]")).Clear();
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div[2]/div/select")), 1);//2
                    break;

                case "Tue":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div/div[1]/input[1]")).Clear();
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div[2]/div/select")), 1);//2
                    break;

                case "Wed":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div/div[1]/input[1]")).Clear();
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div[2]/div/select")), 1);//2
                    break;

                case "Thu":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div/div[1]/input[1]")).Clear();
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div[2]/div/select")), 1);//2
                    break;

                case "Fri":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div/div[1]/input[1]")).Clear();
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div[2]/div/select")), 1);//2
                    break;

                case "Sat":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div/div[1]/input[1]")).Clear();
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div[2]/div/select")), 1);//2
                    break;

                case "Sun":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div/div[1]/input[1]")).Clear();
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div[2]/div/select")), 1);//2
                    break;
            }
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            return week;
        }
        public string LogCurrentDateHoursforSpecialProjectLV(string hours)
        {
            DateTime Time = DateTime.Now.AddDays(0);
            string format = "ddd";
            string week = Time.ToString(format);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));

            switch (Time.ToString(format))
            {
                case "Mon":
                    By comboOptionMon = By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div/div[1]/input[1]");
                    driver.FindElement(comboOptionMon).SendKeys(hours);
                    driver.FindElement(comboOptionMon).SendKeys(Keys.Enter);
                    break;

                case "Tue":
                    By comboOptionTue = By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div/div[1]/input[1]");
                    driver.FindElement(comboOptionTue).SendKeys(hours);
                    driver.FindElement(comboOptionTue).SendKeys(Keys.Enter);
                    break;

                case "Wed":
                    By comboOptionWed = By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div/div[1]/input[1]");
                    driver.FindElement(comboOptionWed).SendKeys(hours);
                    driver.FindElement(comboOptionWed).SendKeys(Keys.Enter);
                    break;

                case "Thu":
                    By comboOptionThu = By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div/div[1]/input[1]");
                    driver.FindElement(comboOptionThu).SendKeys(hours);
                    driver.FindElement(comboOptionThu).SendKeys(Keys.Enter);
                    break;

                case "Fri":
                    By comboOptionFri = By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div/div[1]/input[1]");
                    driver.FindElement(comboOptionFri).SendKeys(hours);
                    driver.FindElement(comboOptionFri).SendKeys(Keys.Enter);
                    break;

                case "Sat":
                    By comboOptionSat = By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div/div[1]/input[1]");
                    driver.FindElement(comboOptionSat).SendKeys(hours);
                    driver.FindElement(comboOptionSat).SendKeys(Keys.Enter);
                    break;

                case "Sun":
                    By comboOptionSun = By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div/div[1]/input[1]");
                    driver.FindElement(comboOptionSun).SendKeys(hours);
                    driver.FindElement(comboOptionSun).SendKeys(Keys.Enter);
                    break;
            }
            Thread.Sleep(8000);
            driver.SwitchTo().DefaultContent();
            return week;
        }
        //Log Future Date Hours
        public string LogFutureDateHoursLV(string txtHours)
        {

            DateTime Time = DateTime.Now.AddDays(1);
            string format = "ddd";
            string week = Time.ToString(format);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            switch (Time.ToString(format))
            {
                case "Mon":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div[2]/div/select")), 2);

                    break;

                case "Tue":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div[2]/div/select")), 2);

                    break;

                case "Wed":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div[2]/div/select")), 2);
                    break;


                case "Thu":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div[2]/div/select")), 2);

                    break;

                case "Fri":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div[2]/div/select")), 2);

                    break;


                case "Sat":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div[2]/div/select")), 2);

                    break;

                case "Sun":

                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(1000);
                    CustomFunctions.SelectByIndex(driver, driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div[2]/div/select")), 2);

                    break;
            }
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            return week;

        }
        public string LogCurrentDateHoursActivityOptionsLV(string txtHours)
        {
            DateTime Time = DateTime.Now.AddDays(0);
            string format = "ddd";
            string week = Time.ToString(format);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));

            switch (Time.ToString(format))
            {
                case "Mon":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div[2]/div/select")).Click();
                    break;

                case "Tue":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div[2]/div/select")).Click();
                    break;

                case "Wed":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div[2]/div/select")).Click();
                    break;

                case "Thu":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div[2]/div/select")).Click();
                    break;

                case "Fri":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div[2]/div/select")).Click();
                    break;

                case "Sat":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div[2]/div/select")).Click();
                    break;

                case "Sun":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    Thread.Sleep(2000);
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div[2]/div/select")).Click();
                    break;
            }
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            return week;
        }
        public void SelectStaffMemberLV(string name)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            Thread.Sleep(2000);
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(10000);
            driver.FindElement(By.XPath($"//a[text()='{name}']")).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(10000);
        }
       
        public string GetSelectedStaffNameLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, txtSelectedStaffName);
            string name= driver.FindElement(txtSelectedStaffName).Text.Trim();
            driver.SwitchTo().DefaultContent();
            return name;
        }
        public void GoToStaffTimeSheetTabLV()
        {
            Thread.Sleep(20000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, tabStaffTimeSheet);
            driver.FindElement(tabStaffTimeSheet).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(10000);
            driver.SwitchTo().DefaultContent();
        }

        public double GetTotalAmountLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, valTotalAmount, 20);
            double billedAmount = double.Parse(driver.FindElement(valTotalAmount).Text);
            driver.SwitchTo().DefaultContent();
            return billedAmount;
        }
        public double GetEnteredHoursInSummaryLogValueLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, valEnteredHours, 20);
            double enteredHours= double.Parse(driver.FindElement(valEnteredHours).Text);
            driver.SwitchTo().DefaultContent();
            return enteredHours;
        }
        public bool IsProjectSelectedLV(string value)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            try
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(comboSelectProjectN));
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(value);
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
                driver.SwitchTo().DefaultContent();
                return true;
            }
            catch (Exception e)
            {
                driver.SwitchTo().DefaultContent();
                return false;
            }
        }
        public bool IsComboSelectProjectDisplayedLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                bool isDisplayed= driver.FindElement(comboSelectProject).Displayed;
                driver.SwitchTo().DefaultContent();
                return isDisplayed;
            }
            catch (Exception e)
            {
                driver.SwitchTo().DefaultContent();
                return false;
            }
        }
        public string SearchProjectandGetFullNameLV(string value)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            try
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(comboSelectProjectN));
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(value);
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                string name= driver.FindElement(comboSelectProjectName).Text;
                driver.SwitchTo().DefaultContent();
                return name;
            }
            catch (Exception e)
            {
                driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
                return "No Item Found";
            }
        }
        public bool IsActivityListDisplayedLV(string selectProject)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName, 5);
                driver.FindElement(comboSelectProjectName).Click();
                driver.SwitchTo().DefaultContent();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject, 5);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
                driver.SwitchTo().DefaultContent();
            }

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectActivity, 5);
                bool IsActivityDisplayed= driver.FindElement(comboSelectActivity).Displayed;
                driver.SwitchTo().DefaultContent();
                return IsActivityDisplayed;
            }
            catch (Exception e) 
            {
                driver.SwitchTo().DefaultContent(); 
                return false; 
            }
        }
        public bool IsActivityListDisplayedLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));   
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectActivity, 5);
                bool IsActivityDisplayed = driver.FindElement(comboSelectActivity).Displayed;
                driver.SwitchTo().DefaultContent();
                return IsActivityDisplayed;
            }
            catch (Exception e)
            {
                driver.SwitchTo().DefaultContent();
                return false;
            }
        }        
        public bool IsTimeClockRecorderCommentBoxDisplayedLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtTimeClockRecorderComments, 5);
                bool IsActivityDisplayed = driver.FindElement(txtTimeClockRecorderComments).Displayed;
                driver.SwitchTo().DefaultContent();
                return IsActivityDisplayed;
            }
            catch (Exception e)
            {
                driver.SwitchTo().DefaultContent();
                return false;
            }
        }
        public bool IsLogsCommentBoxDisplayedLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtLogsComments, 5);
                bool IsActivityDisplayed = driver.FindElement(txtLogsComments).Displayed;
                driver.SwitchTo().DefaultContent();
                return IsActivityDisplayed;
            }
            catch (Exception e)
            {
                driver.SwitchTo().DefaultContent();
                return false;
            }
        }
        public string GetTimeRecordManagerTitleLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, valTimeRecordManagerTitle, 80);
            string TimeRecordManagerTitle = driver.FindElement(valTimeRecordManagerTitle).Text.Split(':')[0].Trim();
            driver.SwitchTo().DefaultContent();
            return TimeRecordManagerTitle;
        }
        public void EnterWeeklyEntryMatrixLV(string selectProject, string txtHours, string txtActivity)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            Thread.Sleep(5000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSundayTime);
            driver.FindElement(txtEnterSundayTime).SendKeys(txtHours);
            WebDriverWaits.WaitUntilEleVisible(driver, comboSelectActivity);
            driver.FindElement(comboSelectActivity).SendKeys(txtActivity);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(5000);
        }
        public string GetSundayTimeEntryLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSundayTime, 20);
            string sundayTimeEntryValue = driver.FindElement(txtEnterSundayTime).GetAttribute("value");
            driver.SwitchTo().DefaultContent();
            return sundayTimeEntryValue;
        }
        public string GetDefaultSelectedProjectOptionLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            By dropDownSelectProject = By.XPath("//div//label[text()='Select Project']");
            WebDriverWaits.WaitUntilEleVisible(driver, dropDownSelectProject, 20);
            string DefaultSelectProject = driver.FindElement(option).GetAttribute("placeholder");
            driver.SwitchTo().DefaultContent();
            return DefaultSelectProject;
        }
       public string GetSummaryLogsTimeEntryLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsActualHours, 20);
            string SummaryLogEntryTime = driver.FindElement(txtSummaryLogsActualHours).Text;
            driver.SwitchTo().DefaultContent();
            return SummaryLogEntryTime;
        }        
        public string GetDetailLogsTimeEntryLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, txtDetailLogsActualHours, 20);
            string DetailLogEntryTime = driver.FindElement(txtDetailLogsActualHours).GetAttribute("value");
            driver.SwitchTo().DefaultContent();
            return DetailLogEntryTime;
        }
        public void SelectFutureTimePeriodLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            driver.FindElement(txtCurrentTimePeriod).Click();
            driver.FindElement(drpdownFuturePeriod).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(2000);
            driver.SwitchTo().DefaultContent();
        }
        public void VerifyActivityDropDownForFuturePeriodLV(string file)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, drpdwnSelectPreSetTemplate);
            IWebElement recordDropdown = driver.FindElement(drpdwnSelectPreSetTemplate);
            SelectElement select = new SelectElement(recordDropdown);
            int RowPreSetTemplateList = ReadExcelData.GetRowCount(excelPath, "Activity_List");

            for (int i = 2; i <= RowPreSetTemplateList; i++)
            {
                IList<IWebElement> options = select.Options;
                IWebElement PReSetTemplateOption = options[i - 2];
                string preSetListExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity_List", i, 1);
                Assert.AreEqual(PReSetTemplateOption.Text, preSetListExl);
            }
            driver.SwitchTo().DefaultContent();
        }
        public string LogCurrentDateHoursTFRGroupLV(string txtHours)
        {
            DateTime Time = DateTime.Now.AddDays(0);
            string format = "ddd";
            string week = Time.ToString(format);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));

            switch (Time.ToString(format))
            {
                case "Mon":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    break;
                case "Tue":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    break;
                case "Wed":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    break;
                case "Thu":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    break;
                case "Fri":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    break;
                case "Sat":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    break;
                case "Sun":
                    driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div/div[1]/input[1]")).SendKeys(txtHours);
                    break;
            }
            Thread.Sleep(5000);            
            driver.FindElement(tabSummaryLogs).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(2000);
            driver.FindElement(tabWeeklyEntryMatrix).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(2000);
            driver.SwitchTo().DefaultContent();
            return week;
        }
        public string GetProjectOrEngagementValueLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, valProjectOrEngagement, 20);
            string ProjectOrEngagement = driver.FindElement(valProjectOrEngagement).Text;
            driver.SwitchTo().DefaultContent();
            return ProjectOrEngagement;
        }
        public string GetSelectedActivityOnSummaryLogsLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, valActivity, 20);
            string ActivityValue = driver.FindElement(valActivity).Text;
            driver.SwitchTo().DefaultContent();
            return ActivityValue;
        }
        public string GetSelectedActivityOnDetailLogsLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, valActivityInDetailLogs, 20);
            string ActivityValueInDetail = driver.FindElement(valActivityInDetailLogs).Text;
            driver.SwitchTo().DefaultContent();
            return ActivityValueInDetail;
        }
        public string GetEnteredHoursInSummaryLogsLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, valEnteredHours, 20);
            string enteredHours = driver.FindElement(valEnteredHours).Text;
            driver.SwitchTo().DefaultContent();
            return enteredHours;
        }

        public string GetEnteredHoursInDetailLogsLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, valEnteredHoursInDetailLogs, 20);
            string enteredHours = driver.FindElement(valEnteredHoursInDetailLogs).GetAttribute("value");
            driver.SwitchTo().DefaultContent();
            return enteredHours;
        }
        By comboSelectProjectFR = By.XPath("//div[contains(@class,'AddTimeRecordRollupWeekRecordRow')]//select");
        By txtWkDay = By.XPath("//div[contains(@class,'AddTimeRecordRollupWeekRecordRow')]//input[@placeholder='Wk Day']");
        By txtWkEnd = By.XPath("//div[contains(@class,'AddTimeRecordRollupWeekRecordRow')]//input[@placeholder='Wk End']");
        public string EnterFRUserHoursLV(string projectName, string hoursWkDay, string hoursWkEnd)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));            
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectFR, 10);
                driver.FindElement(comboSelectProjectFR).SendKeys(projectName);
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN, 20);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(projectName);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName, 10);
                driver.FindElement(comboSelectProjectName).Click();

            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtWkDay);
            driver.FindElement(txtWkDay).Clear();
            driver.FindElement(txtWkDay).SendKeys(hoursWkDay);

            WebDriverWaits.WaitUntilEleVisible(driver, txtWkEnd);
            driver.FindElement(txtWkEnd).Clear();
            driver.FindElement(txtWkEnd).SendKeys(hoursWkEnd);


            WebDriverWaits.WaitUntilEleVisible(driver, btnAdd);
            driver.FindElement(btnAdd).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tableAddedProject,20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tableAddedProject));
            WebDriverWaits.WaitUntilEleVisible(driver, textSuccessMsg,20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(textSuccessMsg));
            string txtMsg = driver.FindElement(textSuccessMsg).Text.Trim();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
            return txtMsg;
        }
        public string GetDefaultRateForStaffLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultDollar, 80);
            string DefaultDollar = driver.FindElement(valDefaultDollar).Text;
            driver.SwitchTo().DefaultContent();
            return DefaultDollar;
        }
        public string GetBorderColorTimeEntryLV(string weekday)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            string borderColor="";

            switch (weekday)
            {
                case "Mon":

                    borderColor = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[3]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    //Console.WriteLine(color);
                    //Assert.AreEqual(color, "rgb(194, 57, 52)");
                    break;
                case "Tue":

                     borderColor = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[4]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    //Console.WriteLine(color1);
                    //Assert.AreEqual(color1, "rgb(194, 57, 52)");
                    break;
                case "Wed":

                    borderColor = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[5]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    //Console.WriteLine(color2);
                    //Assert.AreEqual(color2, "rgb(194, 57, 52)");
                    break;


                case "Thu":

                    borderColor = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[6]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    //Console.WriteLine(color3);
                    //Assert.AreEqual(color3, "rgb(194, 57, 52)");
                    break;
                case "Fri":

                    borderColor = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[7]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    //Console.WriteLine(color4);
                    //Assert.AreEqual(color4, "rgb(194, 57, 52)");
                    break;

                case "Sat":

                    borderColor = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[8]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    //Console.WriteLine(color5);
                    //Assert.AreEqual(color5, "rgb(194, 57, 52)");
                    break;
                case "Sun":

                    borderColor = driver.FindElement(By.XPath("//*[@class='staffTimeSheetWeeklyMassEdit']/div/table/tr[2]/td/div/div/table/tr[1]/td[2]/div[1]/div/div/div[1]/input[1]")).GetCssValue("border-color");
                    //Console.WriteLine(color6);
                    //Assert.AreEqual(color6, "rgb(194, 57, 52)");
                    break;
            }
            driver.SwitchTo().DefaultContent();
            if (borderColor == "rgb(194, 57, 52)")
                return "Red";
            else 
            return "Black";
        }
        public string GetWeekStartDateLV()
        {
            string weekStartDate="";            
            string weekday = System.DateTime.Now.ToString("ddd");
            driver.SwitchTo().DefaultContent();            
            switch (weekday)
            {
                case "Sun":
                   string weekStartDate1 = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
                    weekStartDate = weekStartDate1;
                    break;

                case "Mon":
                    string weekStartDate2 = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    weekStartDate = weekStartDate2;
                    break;

                case "Tue":
                    string weekStartDate3 = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
                    weekStartDate = weekStartDate3;
                    break;

                case "Wed":
                    string weekStartDate4 = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd");
                    weekStartDate = weekStartDate4;
                    break;

                case "Thu":
                    string weekStartDate5 = DateTime.Now.AddDays(-4).ToString("yyyy-MM-dd");
                    weekStartDate = weekStartDate5;
                    break;

                case "Fri":
                    string weekStartDate6 = DateTime.Now.AddDays(-5).ToString("yyyy-MM-dd");
                    weekStartDate = weekStartDate6;
                    break;

                case "Sat":
                    string weekStartDate7 = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
                    weekStartDate = weekStartDate7;
                    break;
                
            }
            return weekStartDate;
        }        
        public string GetTotalForcastedHoursLV()
        {
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, txtTotalForcastedHoursL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtTotalForcastedHoursL));
            string hours=  driver.FindElement(txtTotalForcastedHoursL).Text;
            driver.SwitchTo().DefaultContent();
            return hours;
        }
        public string EnterSummaryLogsHoursSpecialProjectLV(string selectProject, string hours)
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                //extracode
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
            }
            catch 
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(hours);                
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdd);
            driver.FindElement(btnAdd).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, tableSummaryLog, 60);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tableSummaryLog));
            WebDriverWaits.WaitUntilEleVisible(driver, textSuccessMsg);
            CustomFunctions.MoveToElement(driver, driver.FindElement(textSuccessMsg));
            string msgSuccess = driver.FindElement(textSuccessMsg).Text.Trim(); 
            driver.SwitchTo().DefaultContent();
            return msgSuccess;
            //Thread.Sleep(2000);  
        }

        public string EnterDetailLogsHoursSpecialProjectLV(string selectProject, string hours)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
            }
            catch
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(hours);
            
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdd);
            driver.FindElement(btnAdd).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tableSummaryLog, 60);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tableSummaryLog));
            WebDriverWaits.WaitUntilEleVisible(driver, textSuccessMsg);
            string msgSuccess= driver.FindElement(textSuccessMsg).Text.Trim(); 
            driver.SwitchTo().DefaultContent();
            return msgSuccess;
            //Thread.Sleep(2000);
        }

        public void UpdateDetailLogsHoursSpecialProjectLV(string newHours)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime1);
            driver.FindElement(txtEnterSummaryLogEntryTime1).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime1).SendKeys(newHours);
            Thread.Sleep(2000);
            driver.FindElement(tabSummaryLogs).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            Thread.Sleep(2000);
            driver.FindElement(tabDetailLogs).Click();
            WebDriverWaits.WaitTillElementVisible(driver, imgSpinningLoader);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
        }
        public string EnterWeeklyOverviewHoursSpecialProjectLV(string selectProject, string hours)
        {
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            string getDate = DateTime.Today.AddDays(0).ToString("dd MMM yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtSummaryLogsAddRecordDate);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSummaryLogsAddRecordDate));
            driver.FindElement(txtSummaryLogsAddRecordDate).Clear();
            driver.FindElement(txtSummaryLogsAddRecordDate).SendKeys(getDate);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(selectProject);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProject);
                driver.FindElement(comboSelectProject).SendKeys(selectProject);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtEnterSummaryLogEntryTime);
            driver.FindElement(txtEnterSummaryLogEntryTime).Clear();
            driver.FindElement(txtEnterSummaryLogEntryTime).SendKeys(hours);

            WebDriverWaits.WaitUntilEleVisible(driver, btnAdd);
            driver.FindElement(btnAdd).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tableWeeklyOverview);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tableWeeklyOverview));
            WebDriverWaits.WaitUntilEleVisible(driver, textSuccessMsg);
            CustomFunctions.MoveToElement(driver, driver.FindElement(textSuccessMsg));
            string txtMsg = driver.FindElement(textSuccessMsg).Text.Trim();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(2000);
            return txtMsg;
        }

        public string GetFutureTimeRecordPeriodLV()
        {
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, comboTimeRecordPeriodL, 10);
            driver.FindElement(comboTimeRecordPeriodL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtdefaultTimeRecordPeriod, 10);
            string defaultPeriod = driver.FindElement(txtdefaultTimeRecordPeriod).Text;
            string startDate = defaultPeriod.Split(' ')[0];
            driver.FindElement(comboTimeRecordPeriodL).Click();
            driver.SwitchTo().DefaultContent();
            return startDate;
        }

        public string GetFutureWeekStartDateLV(int weeks)
        {
            string weekStartDate = "";
            string weekday = System.DateTime.Now.ToString("ddd");
            driver.SwitchTo().DefaultContent();
            int expactedFutureDays = 7 * weeks;
            switch (weekday)
            {
                case "Sun":
                    string weekStartDate1 = DateTime.Now.AddDays(expactedFutureDays).ToString("yyyy-MM-dd");
                    weekStartDate = weekStartDate1;
                    break;

                case "Mon":
                    string weekStartDate2 = DateTime.Now.AddDays(expactedFutureDays-1).ToString("yyyy-MM-dd");
                    weekStartDate = weekStartDate2;
                    break;

                case "Tue":
                    string weekStartDate3 = DateTime.Now.AddDays(expactedFutureDays - 2).ToString("yyyy-MM-dd");
                    weekStartDate = weekStartDate3;
                    break;

                case "Wed":
                    string weekStartDate4 = DateTime.Now.AddDays(expactedFutureDays - 3).ToString("yyyy-MM-dd");
                    weekStartDate = weekStartDate4;
                    break;

                case "Thu":
                    string weekStartDate5 = DateTime.Now.AddDays(expactedFutureDays - 4).ToString("yyyy-MM-dd");
                    weekStartDate = weekStartDate5;
                    break;

                case "Fri":
                    string weekStartDate6 = DateTime.Now.AddDays(expactedFutureDays - 5).ToString("yyyy-MM-dd");
                    weekStartDate = weekStartDate6;
                    break;

                case "Sat":
                    string weekStartDate7 = DateTime.Now.AddDays(expactedFutureDays - 6).ToString("yyyy-MM-dd");
                    weekStartDate = weekStartDate7;
                    break;

            }
            return weekStartDate;
        }
    }
}
