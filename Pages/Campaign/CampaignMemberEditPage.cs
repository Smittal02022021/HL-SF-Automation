using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages
{
    class CampaignMemberEditPage : BaseClass
    {
        By btnSave = By.XPath("//button[@title='Save']");
        By lblErrorMsg = By.XPath("//a[@class='errorsListLink']");

        By selectResponseMethod = By.XPath("(//a[@class='select'])[1]");
        By selectStatus = By.XPath("(//a[@class='select'])[2]");
        By txtResponseComments = By.XPath("(//textarea)[1]");
        
        public string GetErrorMessage()
        {
            Thread.Sleep(2000);

            driver.FindElement(btnSave).Click();
            Thread.Sleep(2000);
            string error = driver.FindElement(lblErrorMsg).Text;
            return error;
        }

        public void AddCampaignMember(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string respMethod = ReadExcelData.ReadData(excelPath, "CampaignMember", 1);
            string status = ReadExcelData.ReadData(excelPath, "CampaignMember", 2);

            try
            {
                driver.FindElement(selectResponseMethod).Click();
                driver.FindElement(By.XPath($"//a[@title='{respMethod}']")).Click();
                Thread.Sleep(2000);

                driver.FindElement(selectStatus).Click();
                driver.FindElement(By.XPath($"//a[@title='{status}']")).Click();
                Thread.Sleep(2000);

                driver.FindElement(txtResponseComments).Clear();
                driver.FindElement(txtResponseComments).SendKeys(ReadExcelData.ReadData(excelPath, "CampaignMember", 3));
            }
            catch (Exception ex) { }

            driver.FindElement(btnSave).Click();
            Thread.Sleep(8000);
        }
    }
}
