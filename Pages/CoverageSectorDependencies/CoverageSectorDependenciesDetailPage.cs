using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages
{
    class CoverageSectorDependenciesDetailPage : BaseClass
    {
        By btnDelete = By.XPath("//input[@title='Delete']");
        By hlSectorID = By.XPath("//div[@id='Name_ileinner']");

        public void DeleteCoverageSectorDependency()
        {
            driver.FindElement(btnDelete).Click();
            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        public string GetCoverageSectorID()
        {
            string sectorID = driver.FindElement(hlSectorID).Text;
            return sectorID;
        }
    }
}
