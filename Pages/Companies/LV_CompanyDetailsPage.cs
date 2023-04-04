using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages.Companies
{
    class LV_CompanyDetailsPage : BaseClass
    {
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
        By btnSave = By.XPath("(//button[@title='Save'])[2]");
        By btnCancel = By.XPath("(//button[@title='Cancel'])[2]");

        public void NavigateToAParticularTab(string tabName)
        {
            Thread.Sleep(5000);
            IList<IWebElement> elements = driver.FindElements(By.XPath("//ul[@role='tablist']/li"));
            int size = elements.Count;

            for(int items = 1;items <= size;items++)
            {
                By linkTab = By.XPath($"//ul[@role='tablist']/li[{items}]/a");

                WebDriverWaits.WaitUntilEleVisible(driver,linkTab,120);
                string tab = driver.FindElement(linkTab).Text;

                if(tab == tabName)
                {
                    driver.FindElement(linkTab).Click();
                    Thread.Sleep(3000);
                    break;
                }
            }
        }

        public bool VerifyCoverageTabIsOpened()
        {
            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver,lblSponsorCoverage,120);
            string heading2 = driver.FindElement(lblSponsorCoverage).Text;
            if(heading2.Contains("Sponsor Coverage"))
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

            for(int rows = 1;rows <= size;rows++)
            {
                By userNameLink = By.XPath($"(//table[contains(@class,'slds-table slds-table--bordered')])[2]/tbody/tr[{rows}]/td[2]/div/lightning-formatted-rich-text/span/a");

                WebDriverWaits.WaitUntilEleVisible(driver,userNameLink,120);
                string userText = driver.FindElement(userNameLink).Text;

                if(userText == userName)
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
            WebDriverWaits.WaitUntilEleVisible(driver,btnAddActivity,120);
            if(driver.FindElement(btnAddActivity).Displayed)
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

            string type = ReadExcelData.ReadData(excelPath,"Activity",1);
            string subject = ReadExcelData.ReadData(excelPath,"Activity",2);
            string industryGroup = ReadExcelData.ReadData(excelPath,"Activity",3);
            string productType = ReadExcelData.ReadData(excelPath,"Activity",4);
            string description = ReadExcelData.ReadData(excelPath,"Activity",5);
            string meetingNotes = ReadExcelData.ReadData(excelPath,"Activity",6);
            string extAttendee = ReadExcelData.ReadData(excelPath,"Activity",7);

            //Click on Add Activity button
            WebDriverWaits.WaitUntilEleVisible(driver,btnAddActivity,120);
            driver.FindElement(btnAddActivity).Click();
            WebDriverWaits.WaitUntilEleVisible(driver,lblAddNewActivity,120);

            //Enter Activity details
            driver.FindElement(By.XPath($"//input[@value='{type}']")).Click();
            driver.FindElement(txtSubject).SendKeys(subject);

            DateTime currentDate = DateTime.Today;
            DateTime setDate = currentDate.AddDays(-3);
            driver.FindElement(txtDate).SendKeys(setDate.ToString());

            driver.FindElement(drpdownIndustryGroup).SendKeys(industryGroup);
            driver.FindElement(drpdownProductType).SendKeys(productType);
            driver.FindElement(txtareaDescription).SendKeys(description);
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(meetingNotes);

            //Enter External Attendee
            driver.FindElement(txtExternalAttendee).SendKeys(extAttendee);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//div[@data-name='{extAttendee}']")).Click();

            //Click Save
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
        }
    }
}

