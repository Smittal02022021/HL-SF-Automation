using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages
{
    class CampaignDetailPage : BaseClass
    {
        //Buttons
        By linkRemoveContact = By.XPath("//a[@title='Remove - Record 1 - Contact']");
        By btnDelete = By.XPath("//a[@title='Delete']");
        By btnDelete1 = By.XPath("//button[@title='Delete']");

        //Tabs
        By tabCampaignMembers = By.XPath("(//span[text()='Campaign Members']/..)[2]");
        By tabActivity = By.XPath("(//span[text()='Activity']/..)[2]");

        private By _txtPageHeader(string item)
        {
            return By.XPath($"//div[contains(@class,'testonly-outputNameWithHierarchyIcon')]//lightning-formatted-text[text()='{item}']");
        }

        private By _txtActivitySubject(string value)
        {
            return By.XPath($"//div[contains(@class,'timelineGridItemLeft')]//a[@title='{value}']");
        }

        public void DeleteContactFromCampaignHistory()
        {
            driver.FindElement(linkRemoveContact).Click();
            Thread.Sleep(5000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(5000);
        }

        public void DeleteCampaign()
        {
            Thread.Sleep(8000);
            //CustomFunctions.MoveToElement(driver, driver.FindElement(btnDelete));
            driver.FindElement(btnDelete).Click();
            Thread.Sleep(2000);
            driver.FindElement(btnDelete1).Click();
            Thread.Sleep(2000);
        }
       
        public bool IsPageHeaderDisplayedLV(string item)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, _txtPageHeader(item), 20);
                return driver.FindElement(_txtPageHeader(item)).Displayed;
            }
            catch { return false; }
        }
        
        public bool IsLinkedActivityDisplayed(string activity)
        {
            Thread.Sleep(5000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, _txtActivitySubject(activity), 20);
                return driver.FindElement(_txtActivitySubject(activity)).Displayed;
            }
            catch { return false; }
        }

        public void ClickActivityTab()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabActivity, 20);
            driver.FindElement(tabActivity).Click();
            Thread.Sleep(5000);
        }

    }
}
