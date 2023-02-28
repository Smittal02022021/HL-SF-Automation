using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages.Contact
{
    class LV_ContactDetailsPage : BaseClass
    {
        By btnEdit = By.XPath("//button[@name='Edit']");
        By btnAddRelationshipL = By.XPath("//button[text()='Add Relationship L']");
        By btnAddActivity = By.XPath("//button[text()='Add Activity']");
        By btnPrintableView = By.XPath("//button[text()='Printable View']");
        By lblActivityDetails = By.XPath("//div[@class='pbSubheader brandTertiaryBgr first tertiaryPalette']/h3");
        By tabActivity = By.XPath("//a[@data-label='Activity']");

        //Activity Details
        By lblGetPrimaryContactNameFromActivity = By.XPath("//tbody[@id='j_id0:j_id1:j_id2:j_id3:pbActivityLog:pbtActivities:tb']/tr[1]/td[5]/span/a");
        By linkEditActivity = By.XPath("//tbody[@id='j_id0:j_id1:j_id2:j_id3:pbActivityLog:pbtActivities:tb']/tr[1]/td[1]/span/a[2]");

        //Contact Informaction section
        By lblContactName = By.XPath("(//span[text()='Name'])[2]/../../div[2]/span/slot/lightning-formatted-name");

        public void CloseTab(string tabName)
        {
            driver.FindElement(By.XPath($"//button[@title='Close {tabName}']")).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyButtonsDisplayedAtTheTopForExternalContact()
        {
            bool result = false;
            if(driver.FindElement(btnEdit).Displayed && driver.FindElement(btnAddRelationshipL).Displayed && driver.FindElement(btnAddActivity).Displayed && driver.FindElement(btnPrintableView).Displayed)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyDetailsDisplayedAtTheTopBarForExternalContact()
        {
            bool result = false;
            int recordCount = driver.FindElements(By.XPath("//slot[@name='secondaryFields']/records-highlights-details-item")).Count;

            for (int i = 1; i <= recordCount; i++)
            {
                string val = driver.FindElement(By.XPath($"//slot[@name='secondaryFields']/records-highlights-details-item[{i}]/div/p[1]")).Text;
                if(val=="Company Name" || val=="Title" || val == "Email")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        public bool VerifyTabsDisplayedInRightSideForExternalContact(string file)
        {
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            int excelCount = ReadExcelData.GetRowCount(excelPath, "ExternalContactTabs");
            int tabCount = driver.FindElements(By.XPath("//ul[@role='tablist']/li")).Count;

            for (int i = 2; i <= excelCount; i++)
            {
                string excelTabName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExternalContactTabs", i, 1);
                for (int j=1; j<=tabCount; j++)
                {
                    string tabName = driver.FindElement(By.XPath($"//ul[@role='tablist']/li[{j}]/a")).Text;
                    if(tabName == excelTabName)
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

        public bool VerifyFlagContactAndCompanyDetailSectionsAreDisplayedInRightSideForExternalContact(string file)
        {
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            int excelCount = ReadExcelData.GetRowCount(excelPath, "ExternalContactSections");
            int tabCount = driver.FindElements(By.XPath("//flexipage-tab2[@id='tab-7']/slot/flexipage-component2")).Count;

            for (int i = 2; i <= excelCount; i++)
            {
                string excelSectionName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExternalContactSections", i, 1);
                for (int j = 1; j <= tabCount; j++)
                {
                    string sectionName = driver.FindElement(By.XPath($"//flexipage-tab2[@id='tab-7']/slot/flexipage-component2[{j}]/slot/flexipage-aura-wrapper/div/article/div/div/div/div[2]/h2/a")).Text;
                    if (sectionName == excelSectionName)
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

        public bool VerifyUserNavigatedToAddActivityPageForExternalContact()
        {
            bool result = false;

            WebDriverWaits.WaitUntilEleVisible(driver,btnAddActivity, 120);
            driver.FindElement(btnAddActivity).Click();
            Thread.Sleep(10000);

            driver.SwitchTo().Frame(0);

            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblActivityDetails);
            if(driver.FindElement(lblActivityDetails).Text=="Activity Details")
            {
                result = true;
            }
            return result;
        }

        public void NavigateToActivityTabInsideExternalContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabActivity, 60);
            driver.FindElement(tabActivity).Click();
            Thread.Sleep(5000);
        }

        public void NavigateToActivityTabInsideCFFinancialUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver,tabActivity,60);
            driver.FindElement(tabActivity).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyCreatedActivityDisplayedUnderExternalContact(string extName)
        {
            bool result = false;
            driver.SwitchTo().Frame(0);

            WebDriverWaits.WaitUntilEleVisible(driver,lblGetPrimaryContactNameFromActivity,60);
            string name = driver.FindElement(lblGetPrimaryContactNameFromActivity).Text;
            if(name==extName)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyCFFinancialUserIsAbleToEditActivity()
        {
            bool result = false;
            //driver.SwitchTo().Frame(0);

            WebDriverWaits.WaitUntilEleVisible(driver,linkEditActivity,60);
            if(driver.FindElement(linkEditActivity).Displayed)
            {
                driver.FindElement(linkEditActivity).Click();
                result = true;
            }
            return result;
        }

        public bool VerifyCreatedActivityDisplayedUnderCFFinancialUser(string extName)
        {
            bool result = false;
            driver.SwitchTo().Frame(0);

            WebDriverWaits.WaitUntilEleVisible(driver,lblGetPrimaryContactNameFromActivity,60);
            string name = driver.FindElement(lblGetPrimaryContactNameFromActivity).Text;
            if(name == extName)
            {
                result = true;
            }
            return result;
        }

        public string GetExternalContactName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver,lblContactName,60);
            return driver.FindElement(lblContactName).Text;
        }

    }
}