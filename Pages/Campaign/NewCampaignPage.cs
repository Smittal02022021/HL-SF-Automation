using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages
{
    class NewCampaignPage : BaseClass
    {
        By valCampRecordType = By.XPath("((//span[text()='Campaign Record Type'])[2]/following::dd//span)[2]");
        By txtCampaignName = By.XPath("((//span[text()='Campaign Name']/..)[2]/following::input)[1]");

        By selectLOB = By.XPath("((//div[text()='Lines of Business']/following::div)[1]//ul)[1]/li");
        By selectIndustryGroup = By.XPath("((//div[text()='Industry Groups']/following::div)[1]//ul)[1]/li");
        By selectHLSubGroup = By.XPath("((//div[text()='HL Sub-Group']/following::div)[1]//ul)[1]/li");

        By linkAddLOB = By.XPath("(//button[@title='Move to Chosen'])[1]");
        By linkAddIndGrp = By.XPath("(//button[@title='Move to Chosen'])[2]");
        By linkAddHLSubGrp = By.XPath("(//button[@title='Move to Chosen'])[3]");

        By checkboxActive = By.XPath("(//span[text()='Active']/following::input)[1]");
        By btnSave = By.XPath("//button[@title='Save']");
        By btnSaveNew = By.XPath("//button[@title='Save & New']");

        //LV Elements
        By btnCancel = By.XPath("//span[text()='Cancel']/..");
        By btnNext = By.XPath("//span[text()='Next']/..");
        By txtHeadingSelectCampaignType = By.XPath("//h2[text()='New Campaign']");

        public string GetCampaignRecordTypeValue()
        {
            string recordType = driver.FindElement(valCampRecordType).Text;
            return recordType;
        }

        public void CreateNewParentCampaign(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            Thread.Sleep(3000);

            //Add Campaign Name
            driver.FindElement(txtCampaignName).SendKeys(ReadExcelData.ReadData(excelPath, "Campaign", 2));

            //Get LOB options Count
            IList<IWebElement> elementLOB = driver.FindElements(selectLOB);
            int lobOptionsCount = elementLOB.Count;

            //select LOB
            for (int i = 1; i <= lobOptionsCount; i++)
            {
                if(driver.FindElement(By.XPath($"((//div[text()='Lines of Business']/following::div)[1]//ul)[1]/li[{i}]")).Text == ReadExcelData.ReadData(excelPath, "Campaign", 3))
                {
                    driver.FindElement(By.XPath($"((//div[text()='Lines of Business']/following::div)[1]//ul)[1]/li[{i}]")).Click();
                    driver.FindElement(linkAddLOB).Click();
                    break;
                }
            }

            //Get Industry Group options Count
            IList<IWebElement> elementIndGrp = driver.FindElements(selectIndustryGroup);
            int indGrpOptionsCount = elementIndGrp.Count;

            //select Industry Group
            for (int j = 1; j <= indGrpOptionsCount; j++)
            {
                if (driver.FindElement(By.XPath($"((//div[text()='Industry Groups']/following::div)[1]//ul)[1]/li[{j}]")).Text == ReadExcelData.ReadData(excelPath, "Campaign", 4))
                {
                    driver.FindElement(By.XPath($"((//div[text()='Industry Groups']/following::div)[1]//ul)[1]/li[{j}]")).Click();
                    CustomFunctions.MoveToElement(driver, driver.FindElement(linkAddIndGrp));
                    driver.FindElement(linkAddIndGrp).Click();
                    break;
                }
            }

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,500)");
            Thread.Sleep(2000);

            //Get HL Sub Group options Count
            IList<IWebElement> elementHLSubGrp = driver.FindElements(selectHLSubGroup);
            int hlSubGrpOptionsCount = elementHLSubGrp.Count;

            //select Industry Group
            for (int k = 1; k <= hlSubGrpOptionsCount; k++)
            {
                if (driver.FindElement(By.XPath($"((//div[text()='HL Sub-Group']/following::div)[1]//ul)[1]/li[{k}]")).Text == ReadExcelData.ReadData(excelPath, "Campaign", 5))
                {
                    driver.FindElement(By.XPath($"((//div[text()='HL Sub-Group']/following::div)[1]//ul)[1]/li[{k}]")).Click();
                    CustomFunctions.MoveToElement(driver, driver.FindElement(linkAddHLSubGrp));
                    driver.FindElement(linkAddHLSubGrp).Click();
                    break;
                }
            }

            driver.FindElement(checkboxActive).Click();
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
        }

        public void SelectCampaignType(string name)
        {
            try
            {
                Thread.Sleep(5000);
                driver.FindElement(By.XPath($"((//span[text()='{name}'])[2]/../span)[1]")).Click();
                Thread.Sleep(2000);

                driver.FindElement(btnNext).Click();
                Thread.Sleep(3000);
            }
            catch(Exception)
            {
                
            }
        }

        public bool VerifyUserLandedOnNewCampaignPage()
        {
            bool result = false;
            Thread.Sleep(2000);


            return result;
        }

        public bool VerifyIfNewCampaignIsCreatedSuccessfully(string name)
        {
            bool result = false;
            string type = GetCampaignRecordTypeValue();
            if(type==name)
            {
                result = true;
            }
            return result;
        }

        public void CloseTab(string tabName)
        {
            Thread.Sleep(5000);
            driver.FindElement(By.XPath($"//button[contains(@title,'Close {tabName}')]")).Click();
            Thread.Sleep(5000);
        }

    }
}
