﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        By valCoverageSectorDependencyName = By.XPath("//td[contains(text(),'Coverage Sector Dependency Name')]/following::div");

        public void DeleteCoverageSectorDependency()
        {
            driver.FindElement(btnDelete).Click();
            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        public string GetCoverageSectorDependencyName()
        {
            string name = driver.FindElement(valCoverageSectorDependencyName).Text;
            return name;
        }
    }
}
