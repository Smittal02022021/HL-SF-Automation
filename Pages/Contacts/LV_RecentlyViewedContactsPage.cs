using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages.Contact
{
    class LV_RecentlyViewedContactsPage : BaseClass
    {
        By btnNewContact = By.XPath("//a[@title='New']");
        By inputSearchContact = By.XPath("//input[@placeholder='Search this list...']");
        By lblTabTitle = By.XPath("(//span[@class='title slds-truncate'])[1]");
        By btnSelectListView = By.XPath("//button[@title='Select a List View: Contacts']");
        By linkContactsTab = By.XPath("//a[@title='Contacts']");
        By nextButton = By.XPath("//span[text()='Next']/..");
                
        public void NavigateToCreateNewContactPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewContact, 120);
            driver.FindElement(btnNewContact).Click();
            Thread.Sleep(8000);
            string tabName = driver.FindElement(lblTabTitle).Text;
            Assert.IsTrue(tabName == "New Contact");
        }

        public void NavigateToContactTypeSelectionPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewContact, 120);
            driver.FindElement(btnNewContact).Click();
            Thread.Sleep(3000);
        }

        public void ClickNextButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, nextButton, 120);
            driver.FindElement(nextButton).Click();
            Thread.Sleep(3000);
        }

        public void SelectContactType(string type)
        {
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"(//span[text()='{type}']/../span)[1]")).Click();
            Thread.Sleep(2000);
            ClickNextButton();
        }

        public void ChangeContactListView(string viewName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSelectListView, 120);
            driver.FindElement(btnSelectListView).Click();
            Thread.Sleep(5000);

            int recordCount = driver.FindElements(By.XPath("//ul[@aria-label='Contacts | List Views']/li")).Count;

            for (int j = 2; j <= recordCount; j++)
            {
                string sfListViewValue = driver.FindElement(By.XPath($"//ul[@aria-label='Contacts | List Views']/li[{j}]/a/span")).Text;
                if (viewName == sfListViewValue)
                {
                    driver.FindElement(By.XPath($"//ul[@aria-label='Contacts | List Views']/li[{j}]/a")).Click();
                    Thread.Sleep(10000);
                    break;
                }
            }

        }

        public void SearchAndNavigateToContactDetailFromRecentlyViewedContactsListBasedOnView(string contactName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, inputSearchContact, 120);
            driver.FindElement(inputSearchContact).Clear();
            driver.FindElement(inputSearchContact).SendKeys(Keys.Enter);
            Thread.Sleep(5000);
            driver.FindElement(inputSearchContact).SendKeys(contactName);
            driver.FindElement(inputSearchContact).SendKeys(Keys.Enter);
            Thread.Sleep(5000);

            //Get no of records
            int recordCount = driver.FindElements(By.XPath("//table/tbody/tr")).Count;

            for (int i = 1; i <= recordCount; i++)
            {
                string name = driver.FindElement(By.XPath($"(//table/tbody/tr)[{i}]/th/span/a")).Text;

                if (name == contactName)
                {
                    driver.FindElement(By.XPath($"(//table/tbody/tr)[{i}]/th/span/a")).Click();
                    Thread.Sleep(10000);
                    WebDriverWaits.WaitUntilEleVisible(driver,lblTabTitle,120);
                    string tabName = driver.FindElement(lblTabTitle).Text;
                    Assert.IsTrue(tabName.Contains(contactName));
                    break;
                }
            }
        }

        public bool ValidateDiffListViewOptions(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool result = false;

            WebDriverWaits.WaitUntilEleVisible(driver, btnSelectListView, 120);
            driver.FindElement(btnSelectListView).Click();
            Thread.Sleep(3000);

            int recordCount = driver.FindElements(By.XPath("//ul[@aria-label='Contacts | List Views']/li")).Count;
            int excelCount = ReadExcelData.GetRowCount(excelPath, "RecentlyViewedListView");

            for (int i = 2; i <= excelCount; i++)
            {
                string exlListViewValue = ReadExcelData.ReadDataMultipleRows(excelPath, "RecentlyViewedListView", i, 1);

                for (int j = 2; j<=recordCount; j++)
                {
                    string sfListViewValue = driver.FindElement(By.XPath($"//ul[@aria-label='Contacts | List Views']/li[{j}]/a/span")).Text;
                    if(exlListViewValue == sfListViewValue)
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

            driver.FindElement(btnSelectListView).Click();
            Thread.Sleep(3000);
            return result;
        }

        public void CloseTab(string tabName)
        {
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//button[contains(@title,'Close {tabName}')]")).Click();
        }
    }
}