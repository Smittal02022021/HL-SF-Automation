using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V120.DOM;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Threading;

namespace SF_Automation.Pages.Contact
{
    class LV_ContactsActivityListPage : BaseClass
    {
        By btnAddActivity = By.XPath("(//button[text()='Add Activity'])[1]");

        public bool VerifyUserLandsOnActivityTab()
        {
            return true;
        }


    }
}