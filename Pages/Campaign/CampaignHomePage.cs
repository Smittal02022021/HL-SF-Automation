﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages
{
    class CampaignHomePage : BaseClass
    {
        By linkCampaigns = By.XPath("//a[@title='Campaigns Tab']");
        By btnNew = By.XPath("//a[@title='New']");
        By dropdownCampaignRecordType = By.XPath("//label[text()='Select campaign record type']/following::td/div/select");
        By btnContinue = By.XPath("//input[@title='Continue']");
        By linkCampDetail = By.XPath("//a[text()='Test Parent Campaign']");

        public void NavigateToNewCampaignPage()
        {
            Thread.Sleep(2000);
            driver.FindElement(btnNew).Click();
            Thread.Sleep(2000);
        }

        public void ClickCampaignTab()
        {
            driver.FindElement(linkCampaigns).Click();
            Thread.Sleep(2000);
        }

        public void NavigateToCampaignDetailPage(string file)
        {
            Thread.Sleep(5000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            if(ReadExcelData.ReadData(excelPath, "Campaign", 2) == driver.FindElement(linkCampDetail).Text)
            {
                driver.FindElement(linkCampDetail).Click();
                Thread.Sleep(5000);
            }
        }
    }
}
