using AventStack.ExtentReports;
using OpenQA.Selenium;
﻿using Microsoft.Office.Interop.Excel;
using MongoDB.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V109.Page;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading;
using System.Web;

namespace SF_Automation.Pages.Companies
{
    class LV_CompanyDetailsPage : BaseClass
    {
        //General
        By txtCompanyName = By.XPath("//div[text()='Company']/following::slot/sfa-output-name-with-hierarchy-icon-account/sfa-output-name-with-hierarchy-icon-wrapper/force-aura-action-wrapper/div/div/lightning-formatted-text");
        
        //Coverage Tab
        By lblSponsorCoverage = By.XPath("(//h2[@id='header'])[1]/span");

        //Activity Tab
        By lblAddNewActivity = By.XPath("//h1[text()='Add New Activity']");
        By btnAddActivity = By.XPath("//button[text()='Add Activity']");
        By txtSubject = By.XPath("//input[@name='Subject']");
        By txtDate = By.XPath("//input[@name='Date']");
        By drpdownIndustryGroup = By.XPath("//button[@name='IndustryGroup']");
        By drpdownProductType = By.XPath("//button[@name='ProductType']");
        By txtareaDescription = By.XPath("//textarea[@name='Description']");
        By txtareaHLInternalMeetingNotes = By.XPath("//textarea[@name='HLInternalNotes']");
        By txtExternalAttendee = By.XPath("//input[@placeholder='Lookup Contact...']");
        By txtHLAttendee = By.XPath("//input[@placeholder='Lookup Employees...']");
        By txtCompanyDiscussed = By.XPath("//input[@placeholder='Lookup Company...']");
        By txtOpportunitiesDiscussed = By.XPath("//input[@placeholder='Lookup Opportunities...']");
        By txtEngagementsDiscussed = By.XPath("//input[@placeholder='Lookup Engagements...']");
        By txtCampaignsDiscussed = By.XPath("//input[@placeholder='Lookup Campaigns...']");

        By btnSave = By.XPath("(//button[@title='Save'])[2]");
        By btnCancel = By.XPath("(//button[@title='Cancel'])[2]");

        By btnsearchL = By.XPath("//button[@aria-label='Search']");
        By txtsearchL = By.XPath("//input[contains(@placeholder,'Search Companies ')]");
        By imgCompany = By.XPath("//div[1]/records-highlights-icon/force-record-avatar/span/img[@title='Company']");
        By chart = By.CssSelector("canvas[class='chart']");
        By btnAddActivity1 = By.XPath($"//header//button[text()='Add Activity']");
        By btnCreateNewTask = By.XPath("//button[text()='Create New Task']");
        By txtFollowupDate = By.XPath("//input[contains(@name,'Followup_Start_Date')]");
        By dropdownFollowupType = By.XPath("//button[contains(@aria-label,'Follow-up Type')]");
        By dropdownFolloupFrom = By.XPath("//button[contains(@name,'Followup_Start_Time')]");// [contains(@aria-label,'From')]");
        By dropdownFolloupTo = By.XPath("//button[contains(@name,'Followup_End_Time')]");// [contains(@aria-label,'To')]");
        By txtAreaFollowuoComments = By.XPath("//textarea[contains(@name,'Followup_Comments')]");
        By linkPrimayContact = By.XPath("//table//tbody//tr[1]//td[@data-label='Primary Contact']//a");
        By pageHeaderContactpage = By.XPath("//h1//div[text()='Contact']");
        By btnActivitiesRow = By.XPath("//table//tbody//tr[1]//td[7]//button");
        By btnActivitiesRowAction = By.XPath("//table//tbody//tr[1]//td[7]//button//following-sibling::div//span");
        By btnViewActivityDetails = By.XPath("//table//tbody//tr[1]//td[7]//button//following-sibling::div//span[text()='View']");
        By headerActivityDetailsPage = By.XPath("//h1[text()='Activity Details']");
        By btnRefereshActivitiesList = By.XPath("//button[@title='Refresh Activities']");
        By btnDelete = By.XPath("//header//button[text()='Delete']");
        By txtSubjectActivityList = By.XPath("//table//tbody//tr[1]//td[@data-label='Subject']//lightning-base-formatted-text");
        By chckNoExternalContact = By.XPath("//label/span[text()='No External Contact']/preceding-sibling::span");
        By msgLVPopup = By.CssSelector("span.toastMessage.forceActionsText");
        By btnLVPopupClose = By.XPath("//button[contains(@class,'toastClose')]");

        By btnNew = By.XPath("//ul[contains(@class,'oneActionsRibbon')]//a[@title='New']");
        By btnNext=By.XPath("//div[contains(@class,'ChangeRecordTypeFooter')]//button//span[text()='Next']");
        By txtCompanyName = By.XPath("//form//input[contains(@name,'AccountName')]");
        By btnSaveCompany = By.XPath("//form//input[@value='Save']");

        By _radioRecordType(string recordType)
        {
            return By.XPath($"//div[contains(@class,'changeRecordType')]//span[text()='{recordType}']");
        }

        public void ClickNewCompanyButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNew, 20);
            driver.FindElement(btnNew).Click();
        }
        public void SelectRecordType(string recordType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _radioRecordType(recordType), 20);
            driver.FindElement(_radioRecordType(recordType)).Click();
            driver.FindElement(btnNext).Click();
            Thread.Sleep(5000);
        }
        public string SaveCompany()
        {
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='accessibility title']")));
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName, 20);
            string nameCompany = CustomFunctions.RandomValue();
            driver.FindElement(txtCompanyName).SendKeys(nameCompany);
            driver.FindElement(btnSaveCompany).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(5000);
            return nameCompany;
        }

        By tblResults= By.XPath("(//h2//span[text()='Company Activity']//ancestor::article//table)[2]");
        private By _matchedResult(string value)
        {
            return By.XPath($"(//h2//span[text()='Company Activity']//ancestor::article//table)[2]//tbody//tr//td[1]//lightning-base-formatted-text[text()='{value}']");
        }
         
        public bool SearchAcvtivity(string activity)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearch, 20);
            driver.FindElement(txtSearch).SendKeys(activity);

            WebDriverWaits.WaitUntilEleVisible(driver, tblResults, 20);
            Thread.Sleep(5000);
            try
            {
                return driver.FindElement(_matchedResult(activity)).Displayed;
            }
            catch { return false; }
        }

        private By _comboDropdown(string value)
        {
            return By.XPath($"//lightning-base-combobox-item[@data-value='{value}']");
        }
        private By _TabEle(string value)
        {
            return By.XPath($"//button[contains(@title,{value})]");
        }
        
        public void SearchCompanyInLightning(string value)
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnsearchL, 5);
            driver.FindElement(btnsearchL).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, txtsearchL, 10);
            driver.FindElement(txtsearchL).SendKeys(value);
            Thread.Sleep(4000);
            driver.FindElement(imgCompany).Click();
            Thread.Sleep(6000);
        }

        public bool IsTabAvailable(string tabName)
        {
            Thread.Sleep(5000);
            IList<IWebElement> elements = driver.FindElements(By.XPath("//ul[@role='tablist']/li"));
            int size = elements.Count;
            bool tabFound = false;
            for (int items = 1; items <= size; items++)
            {
                By linkTab = By.XPath($"//ul[@role='tablist']/li[{items}]/a");
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, linkTab, 20);
                    string tab = driver.FindElement(linkTab).Text;

                    if (tab == tabName)
                    {
                        tabFound = true;
                        break;
                    }
                }
                catch { tabFound = false; }

            }
            return tabFound;
        }
        public bool IsAddActivityButtonDisplayed()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity, 20);
                return driver.FindElement(btnAddActivity).Displayed;
            }
            catch { return false; }

        }
        public bool IsChartDisplayed()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, chart, 20);
                return driver.FindElement(chart).Displayed;
            } catch { return false; }
        }

        public void NavigateToAParticularTab(string tabName)
        {
            Thread.Sleep(5000);
            IList<IWebElement> elements = driver.FindElements(By.XPath("//ul[@role='tablist']/li"));
            int size = elements.Count;

            for (int items = 1; items <= size; items++)
            {
                By linkTab = By.XPath($"//ul[@role='tablist']/li[{items}]/a");

                WebDriverWaits.WaitUntilEleVisible(driver, linkTab, 120);
                string tab = driver.FindElement(linkTab).Text;

                if (tab == tabName)
                {
                    driver.FindElement(linkTab).Click();
                    Thread.Sleep(8000);
                    break;
                }
            }
        }

        public bool VerifyCoverageTabIsOpened()
        {
            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver, lblSponsorCoverage, 120);
            string heading2 = driver.FindElement(lblSponsorCoverage).Text;
            if (heading2.Contains("Sponsor Coverage"))
            {
                result = true;
            }
            return result;
        }

        public bool VerifyLoggedInUserHasIndustryCoverageForACompany(string userName)
        {
            bool result = false;

            IList<IWebElement> elements = driver.FindElements(By.XPath("(//table[contains(@class,'slds-table slds-table--bordered')])[2]/tbody/tr"));
            int size = elements.Count;

            for (int rows = 1; rows <= size; rows++)
            {
                By userNameLink = By.XPath($"(//table[contains(@class,'slds-table slds-table--bordered')])[2]/tbody/tr[{rows}]/td[2]/div/lightning-formatted-rich-text/span/a");

                WebDriverWaits.WaitUntilEleVisible(driver, userNameLink, 120);
                string userText = driver.FindElement(userNameLink).Text;

                if (userText == userName)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool VerifyActivityTabIsOpened()
        {
            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity, 120);
            if (driver.FindElement(btnAddActivity).Displayed)
            {
                result = true;
            }
            return result;
        }

        public void CreateNewActivityFromCompanyDetailPage(string file)
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

            //Click on Add Activity button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity, 120);
            driver.FindElement(btnAddActivity).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblAddNewActivity, 120);

            //Enter Activity details
            Thread.Sleep(3000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(-3);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("dd-MMM-yyyy"));

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

            //Click Save
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyClickingOnTheCompanyNameUnderAffiliatedCompaniesSectionTakesUserToCompanyDetailsPage()
        {
            bool result = false;

            //Get the no. of companies under Affiliated Companies section
            int noOfCompanies = driver.FindElements(By.XPath("//b[text()='Affiliated Companies ']/following::div/dl/dt/p[text()='Company Name: ']")).Count;

            //Get the name of each company and store in an array
            String[] companyNames = new String[noOfCompanies];
            int j = 1;

            for(int i = 0; i <= noOfCompanies - 1; i++)
            {
                companyNames[i] = driver.FindElement(By.XPath($"((//b[text()='Affiliated Companies ']/following::div/dl/dt/p[text()='Company Name: '])[{j}]/following::dd/p/button)[1]")).Text;
                driver.FindElement(By.XPath($"((//b[text()='Affiliated Companies ']/following::div/dl/dt/p[text()='Company Name: '])[{j}]/following::dd/p/button)[1]")).Click();

                WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName, 120);

                if(driver.FindElement(txtCompanyName).Text == companyNames[i])
                {
                    result = true;
                    Thread.Sleep(3000);
                    driver.FindElement(By.XPath("(//button[contains(@title,'| Company')])[2]")).Click();
                    Thread.Sleep(3000);
                    j++;
                    continue;
                }
                else
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
        public void CreateNewActivityFromCompanyDetailPage(string type, string subject, string industryGroup, string productType, string description, string meetingNotes, string extAttendee)
        {
            //Click on Add Activity button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity1, 20);
            driver.FindElement(btnAddActivity1).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblAddNewActivity, 20);

            //Enter Activity details
            Thread.Sleep(3000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(3);
            IWebElement txtDateField = driver.FindElement(txtDate);
            CustomFunctions.MoveToElement(driver, txtDateField);
            txtDateField.Clear();
            txtDateField.SendKeys(setDate.ToString("dd-MMM-yyyy"));

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
        }
        public void CreateNewActivityAndFollowupFromCompanyDetailPage(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string type = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 5, 1);
            string subject = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 5, 2);
            string industryGroup = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 5, 3);
            string productType = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 5, 4);
            string description = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 5, 5);
            string meetingNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 5, 6);
            string extAttendee = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 5, 7);

            string typeFollowup = ReadExcelData.ReadData(excelPath, "Followup", 1);
            string fromFollowup = ReadExcelData.ReadData(excelPath, "Followup", 2);
            string toFollowup = ReadExcelData.ReadData(excelPath, "Followup", 3);
            string commentsFollowup = ReadExcelData.ReadData(excelPath, "Followup", 4);

            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity1, 20);
            driver.FindElement(btnAddActivity1).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblAddNewActivity, 20);

            //Enter Activity details
            Thread.Sleep(3000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(4);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("dd-MMM-yyyy"));

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

            //Click on Create New Task button
            WebDriverWaits.WaitUntilEleVisible(driver, btnCreateNewTask, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnCreateNewTask));
            driver.FindElement(btnCreateNewTask).Click();
            Thread.Sleep(3000);

            //Enter Followup  details
            CustomFunctions.MoveToElement(driver, driver.FindElement(dropdownFollowupType));
            driver.FindElement(dropdownFollowupType).Click();
            Thread.Sleep(2000);
            driver.FindElement(_comboDropdown(typeFollowup)).Click();
            Thread.Sleep(2000);

            DateTime currentDate1 = DateTime.Today;
            DateTime setDate1 = currentDate1.AddDays(2);
            IWebElement followupDate = driver.FindElement(txtFollowupDate);
            CustomFunctions.MoveToElement(driver, followupDate);
            followupDate.Clear();
            followupDate.SendKeys(setDate1.ToString("dd-MMM-yyyy"));

            IWebElement followupFrom = driver.FindElement(dropdownFolloupFrom);
            CustomFunctions.MoveToElement(driver, followupFrom);
            followupFrom.Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//button[contains(@name,'Followup_Start_Time')]//parent::div//following-sibling::div//lightning-base-combobox-item[@data-value='{fromFollowup}']")).Click();

            IWebElement followupTo = driver.FindElement(dropdownFolloupTo);
            CustomFunctions.MoveToElement(driver, followupTo);
            followupTo.Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//button[contains(@name,'Followup_End_Time')]//parent::div//following-sibling::div//lightning-base-combobox-item[@data-value='{toFollowup}']")).Click();

            IWebElement areaFollowuoComments = driver.FindElement(txtAreaFollowuoComments);
            CustomFunctions.MoveToElement(driver, areaFollowuoComments);
            areaFollowuoComments.SendKeys(commentsFollowup);

            //Click Save
            driver.FindElement(btnSave).Click();
        }
        public string GetActivityPrimayContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkPrimayContact, 30);
            return driver.FindElement(linkPrimayContact).Text;
        }

        public void ClosePrimaryContactPage(string name)
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, _TabEle("'Close " + name + "'"), 30);
            IWebElement closeTabIcon = driver.FindElement(_TabEle("'Close " + name+"'"));
            closeTabIcon.Click();
            Thread.Sleep(2000);
        }
        public bool IsActivityPrimaryContactHyperlinked()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, linkPrimayContact, 30);
                return driver.FindElement(linkPrimayContact).Displayed;

            }
            catch { return false; }
        }

        public bool ClickActivityPrimaryContactHyperlink()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkPrimayContact, 30);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(linkPrimayContact));
            //driver.FindElement(linkPrimayContact).Click();
            Thread.Sleep(5000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, pageHeaderContactpage, 30);
                return driver.FindElement(pageHeaderContactpage).Displayed;

            } catch { return false; }
        }

        public bool VerifyAvailableColumnsOnCompaniesActivitiesListView(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            //Get columns count            
            int recordCount = driver.FindElements(By.XPath("//table//tr[@data-row-key-value='HEADER']//th//span[@class='slds-truncate']")).Count;
            int excelCount = ReadExcelData.GetRowCount(excelPath, "ActivityListColumns");

            for (int columnExl = 2; columnExl <= excelCount; columnExl++)
            {
                string expColValue = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityListColumns", columnExl, 1);

                for (int recordIndex = 1; recordIndex < recordCount; recordIndex++)
                {
                    string actualColValue = driver.FindElement(By.XPath($"//table//tr[@data-row-key-value='HEADER']//th[{recordIndex}]//span[@class='slds-truncate']")).Text;
                    if (expColValue == actualColValue)
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
        public bool VerifyAvailableActionsOnCompaniesActivitiesListView(string file)
        {           
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitiesRow, 30);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnActivitiesRow));
            driver.FindElement(btnActivitiesRow).Click();

            //Get columns count            
            int recordCount = driver.FindElements(btnActivitiesRowAction).Count;
            int excelCount = ReadExcelData.GetRowCount(excelPath, "ActivityListAction");

            for (int columnExl = 2; columnExl <= excelCount; columnExl++)
            {
                string expMenuValue = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityListAction", columnExl, 1);

                for (int recordIndex = 1; recordIndex <= recordCount; recordIndex++)
                {
                    string actualMenuValue = driver.FindElement(By.XPath($"(//table//tbody//tr[1]//td[7]//button//following-sibling::div//span)[{recordIndex}]")).Text;
                    if (expMenuValue == actualMenuValue)
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
        public bool VerifyAvailableActionsOnCompaniesActivitiesListViewLV(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool isSame = true;
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitiesRow, 30);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnActivitiesRow));
            driver.FindElement(btnActivitiesRow).Click();
            Thread.Sleep(2000);
            //Get columns count            
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(btnActivitiesRowAction);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            int excelCount = ReadExcelData.GetRowCount(excelPath, "ActivityListAction");
            string[] expectedValue = new string[excelCount];
            int expectedOptionsCount = excelCount - 1;
            if (expectedOptionsCount != actualValue.Length)
            {
                return !isSame;
            }
            int row = 2;
            for (int rec = 0; rec < expectedOptionsCount; rec++)
            {
                
                expectedValue[rec] = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityListAction",row , 1);
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
                row++;
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnRefereshActivitiesList, 30);
            driver.FindElement(btnRefereshActivitiesList).Click();
            Thread.Sleep(2000);
            return isSame;
        }
        public void RefreshActivitiesList()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnRefereshActivitiesList, 30);
            driver.FindElement(btnRefereshActivitiesList).Click();
            Thread.Sleep(4000);
        }
        public bool ClickActivityViewOption()
        {          
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitiesRow, 30);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnActivitiesRow));
            driver.FindElement(btnActivitiesRow).Click();
            
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewActivityDetails, 30);
            driver.FindElement(btnViewActivityDetails).Click();
            Thread.Sleep(5000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, headerActivityDetailsPage, 30);
                return driver.FindElement(headerActivityDetailsPage).Displayed;
            }
            catch { return false; }         
        }
        public void DeleteActivity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivitiesRow, 30);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnActivitiesRow));
            driver.FindElement(btnActivitiesRow).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewActivityDetails, 30);
            driver.FindElement(btnViewActivityDetails).Click();
            Thread.Sleep(5000);
            
            WebDriverWaits.WaitUntilEleVisible(driver, btnDelete, 30);
            driver.FindElement(btnDelete).Click();            
            WebDriverWaits.WaitUntilEleVisible(driver, btnRefereshActivitiesList, 30);
            driver.FindElement(btnRefereshActivitiesList).Click();
            Thread.Sleep(4000);
        }


        By btnActivityDetailsHeader = By.XPath("//c-s-l_-add-new-activity[contains(@class,'ActivityEventView')]//slot[@name='actions']//lightning-button//button");
        public bool ValidateActivityDetailsPageAvailableButton(string file)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnActivityDetailsHeader, 30);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;
            int recordCount = driver.FindElements(btnActivityDetailsHeader).Count;
            int excelCount = ReadExcelData.GetRowCount(excelPath, "ActivityListAction");

            for (int columnExl = 2; columnExl <= excelCount; columnExl++)
            {
                string expMenuValue = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityListAction", columnExl, 1);

                for (int recordIndex = 1; recordIndex <= recordCount; recordIndex++)
                {
                    string actualMenuValue = driver.FindElement(By.XPath($"(//table//tbody//tr[1]//td[7]//button//following-sibling::div//span)[{recordIndex}]")).Text;
                    if (expMenuValue == actualMenuValue)
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
        public string GetActivitySubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnRefereshActivitiesList, 30);
            driver.FindElement(btnRefereshActivitiesList).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtSubjectActivityList, 30);
            return driver.FindElement(txtSubjectActivityList).Text;
        }

        public void CreateNewActivityWithoutExternalContactFromCompanyDetailPage(string file)
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
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity1, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnAddActivity1));
            driver.FindElement(btnAddActivity1).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblAddNewActivity, 20);

            //Enter Activity details
            Thread.Sleep(3000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(5);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("dd-MMM-yyyy"));

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
            //driver.FindElement(txtExternalAttendee).SendKeys(extAttendee);
            //Thread.Sleep(5000);
            //driver.FindElement(By.XPath($"//div[@data-name='{extAttendee}']")).Click();

            //Click Save
            //driver.FindElement(btnSave).Click();
        }
        
        public void CheckNoExternalContactCheckbox()
        { 
            WebDriverWaits.WaitUntilEleVisible(driver, chckNoExternalContact, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(chckNoExternalContact));
            driver.FindElement(chckNoExternalContact).Click();

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
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity1, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnAddActivity1));
            driver.FindElement(btnAddActivity1).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblAddNewActivity, 20);

            //Enter Activity details
            Thread.Sleep(3000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(2);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("dd-MMM-yyyy"));

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

            //Click Save
            //driver.FindElement(btnSave).Click();
        }
        public void CreateNewActivitywithAllFieldsFromCompanyDetailPage(string file)
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
            string extAttendee= ReadExcelData.ReadData(excelPath, "Activity", 7);
            string opportunitiesDiscussed = ReadExcelData.ReadData(excelPath, "Activity", 9);
            string engagementsDiscussed = ReadExcelData.ReadData(excelPath, "Activity", 10);
            string campaignsDiscussed = ReadExcelData.ReadData(excelPath, "Activity", 11);

            //Click on Add Activity button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddActivity1, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnAddActivity1));
            driver.FindElement(btnAddActivity1).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblAddNewActivity, 20);

            //Enter Activity details
            Thread.Sleep(3000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//input[@value='{type}']/../label")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(6);
            driver.FindElement(txtDate).Clear();
            driver.FindElement(txtDate).SendKeys(setDate.ToString("dd-MMM-yyyy"));

            CustomFunctions.MoveToElement(driver, driver.FindElement(drpdownIndustryGroup));
            Thread.Sleep(2000);
            driver.FindElement(drpdownIndustryGroup).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//span[@title='{industryGroup}']/../..")).Click();
            Thread.Sleep(2000);

            driver.FindElement(drpdownProductType).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath($"//span[@title='{productType}']")).Click();
            Thread.Sleep(1000);

            //Enter External Attendee
            driver.FindElement(txtExternalAttendee).SendKeys(extAttendee);
            Thread.Sleep(5000);
            driver.FindElement(By.XPath($"//div[@data-name='{extAttendee}']")).Click();
            Thread.Sleep(1000);

            CustomFunctions.MoveToElement(driver, driver.FindElement(txtareaDescription));
            Thread.Sleep(2000);
            driver.FindElement(txtareaDescription).SendKeys(description);
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(meetingNotes);

            //OpportunitiesDiscussed
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtOpportunitiesDiscussed));
            Thread.Sleep(2000);
            driver.FindElement(txtOpportunitiesDiscussed).SendKeys(opportunitiesDiscussed);
            Thread.Sleep(2000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtOpportunitiesDiscussed));
            driver.FindElement(By.XPath($"//div[@data-name='{opportunitiesDiscussed}']")).Click();
            Thread.Sleep(1000);

            //EngagementsDiscussed
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtEngagementsDiscussed));
            Thread.Sleep(2000);
            driver.FindElement(txtEngagementsDiscussed).SendKeys(engagementsDiscussed);
            Thread.Sleep(2000);
            By engDiscussed = By.XPath($"//div[@data-name='{engagementsDiscussed}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(engDiscussed));
            driver.FindElement(engDiscussed).Click();
            Thread.Sleep(1000);

            //CampaignDiscussed
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtCampaignsDiscussed));
            Thread.Sleep(2000);
            driver.FindElement(txtCampaignsDiscussed).SendKeys(campaignsDiscussed);
            Thread.Sleep(2000);
            By campDiscussed = By.XPath($"//div[@data-name='{campaignsDiscussed}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(campDiscussed));
            driver.FindElement(campDiscussed).Click();
            Thread.Sleep(1000);
        }

        By _eleActivityListFields(string fieldName)
        {
            return By.XPath($"//h2//span[text()='Company Activity']//ancestor::article//table//tr[1]//td[@data-label='{fieldName}']//lightning-base-formatted-text");
        }  
        
        public string GetValueFromActivityList(string fieldName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _eleActivityListFields(fieldName), 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(_eleActivityListFields(fieldName)));
            return driver.FindElement(_eleActivityListFields(fieldName)).Text;
        }
        public void ViewActivity()
        {            
                WebDriverWaits.WaitUntilEleVisible(driver, btnActivitiesRow, 30);
                CustomFunctions.MoveToElement(driver, driver.FindElement(btnActivitiesRow));
                driver.FindElement(btnActivitiesRow).Click();

                WebDriverWaits.WaitUntilEleVisible(driver, btnViewActivityDetails, 30);
                driver.FindElement(btnViewActivityDetails).Click();              
        }

        /// <summary>
        /// ///////////
        /// </summary>
        By btnViewAllActivities = By.XPath("//h2//span[text()='Company Activity']//ancestor::article//div[@slot='footer']//button");
        By txtSearch = By.XPath("//h2//span[text()='Company Activity']//ancestor::article//input[@placeholder='Search...']");
        public void ClickViewAllActivities()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewAllActivities, 30);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnViewAllActivities));
            driver.FindElement(btnViewAllActivities).Click();
            Thread.Sleep(10000);
        }
        public bool IsCompanyActivityListNewTabDispayed()
        {
            string tabName = "Company Activity";
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, _TabEle("'Close " + tabName + "'"), 30);
                return driver.FindElement(_TabEle("'Close " + tabName + "'")).Displayed;
            }
            catch { return false; }            
        }
        public bool IsSearchBoxDisplayed()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtSearch, 30);
                return driver.FindElement(txtSearch).Displayed;
            }
            catch { return false; }
        }

    }
}

