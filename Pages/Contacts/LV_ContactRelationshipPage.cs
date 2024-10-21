using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SF_Automation.TestCases.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages.Contact
{
    class LV_ContactRelationshipPage : BaseClass
    {
        //General elements
        By pageHeading = By.XPath("(//h1/div[@class='entityNameTitle slds-line-height--reset'])[2]");

        //Information Section
        By txtHLContact = By.XPath("(//a[@class='flex-wrap-ie11'])[1]/slot/slot/span");
        By txtExternalContact = By.XPath("(//a[@class='flex-wrap-ie11'])[2]/slot/slot/span");
        By txtRelationshipNumber = By.XPath("//lightning-formatted-text[contains(text(),'R-')]");
        By btnDelete = By.XPath("//button[@name='Delete']");
        By btnDeleteRelationship = By.XPath("//button[@title='Delete']");

        public void CloseTab(string tabName)
        {
            Thread.Sleep(5000);
            driver.FindElement(By.XPath($"//button[contains(@title,'Close {tabName}')]")).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyClickingOnTheContactNameTakesTheUserToItsRelationshipDetailPage(string extContactName)
        {
            bool result = false;

            //Get the no. of contacts under Top Relationship section
            int noOfContacts = driver.FindElements(By.XPath("//b[text()='Top Relationships']/following::div/dl/dt/p[text()='Contact Name: ']")).Count;

            //Get the name of each contact and store in an array
            String[] contactNames = new String[noOfContacts];
            for(int i = 0; i < noOfContacts; i++)
            {
                try
                {
                    contactNames[i] = driver.FindElement(By.XPath($"((//b[text()='Top Relationships']/following::div/dl/dt/p[text()='Contact Name: '])[{i + 1}]/following::dd/p/button/b)[1]")).Text;
                    driver.FindElement(By.XPath($"((//b[text()='Top Relationships']/following::div/dl/dt/p[text()='Contact Name: '])[{i + 1}]/following::dd/p/button)[1]")).Click();
                    Thread.Sleep(5000);

                    WebDriverWaits.WaitUntilEleVisible(driver, txtHLContact, 120);
                    if(driver.FindElement(pageHeading).Text=="Relationship" && driver.FindElement(txtHLContact).Text == contactNames[i] && driver.FindElement(txtExternalContact).Text == extContactName) 
                    {
                        result = true;
                        string relationshipNo = driver.FindElement(txtRelationshipNumber).Text;
                        driver.FindElement(By.XPath($"//button[contains(@title,'Close {relationshipNo}')]")).Click();
                        Thread.Sleep(3000);
                        continue;
                    }
                    else
                    {
                        result = false;
                        string relationshipNo = driver.FindElement(txtRelationshipNumber).Text;
                        driver.FindElement(By.XPath($"//button[contains(@title,'Close {relationshipNo}')]")).Click();
                        Thread.Sleep(3000);
                        break;
                    }
                }
                catch(Exception)
                {
                    contactNames[i] = driver.FindElement(By.XPath($"((//b[text()='Top Relationships']/following::div/dl/dt/p[text()='Contact Name: '])[{i + 1}]/following::dd/p/button)[1]")).Text;
                    driver.FindElement(By.XPath($"((//b[text()='Top Relationships']/following::div/dl/dt/p[text()='Contact Name: '])[{i + 1}]/following::dd/p/button)[1]")).Click();
                    Thread.Sleep(5000);

                    WebDriverWaits.WaitUntilEleVisible(driver, txtHLContact, 120);

                    if(driver.FindElement(pageHeading).Text == "Relationship" && driver.FindElement(txtHLContact).Text == contactNames[i] && driver.FindElement(txtExternalContact).Text == extContactName)
                    {
                        result = true;
                        string relationshipNo = driver.FindElement(txtRelationshipNumber).Text;
                        driver.FindElement(By.XPath($"//button[contains(@title,'Close {relationshipNo}')]")).Click();
                        Thread.Sleep(3000);
                        continue;
                    }
                    else
                    {
                        result = false;
                        string relationshipNo = driver.FindElement(txtRelationshipNumber).Text;
                        driver.FindElement(By.XPath($"//button[contains(@title,'Close {relationshipNo}')]")).Click();
                        Thread.Sleep(3000);
                        break;
                    }
                }
            }

            return result;
        }

        public bool VerifyHLRelationshipIsLinkedToExternContact(string contact)
        {
            bool result = false;
            Thread.Sleep(2000);
            if(contact==driver.FindElement(By.XPath("((//tbody)[2]/tr/td)[2]//a")).GetAttribute("Title"))
            {
                result = true;
            }
            return result;
        }

        public void NavigateToRelationshipDetailPage()
        {
            driver.FindElement(By.XPath("(//tbody)[2]/tr//th//a")).Click();
            Thread.Sleep(2000);
        }

        public bool VerifyRelationshipDetails(string contact)
        {
            bool result = false;
            Thread.Sleep(3000);
            if(driver.FindElement(By.XPath("//a[@data-proxy-id='aura-pos-lib-1']//slot//slot")).Text == contact)
            {
                result = true;
            }
            return result;
        }

        public void DeleteRelationship()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDelete, 120);
            driver.FindElement(btnDelete).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteRelationship, 120);
            driver.FindElement(btnDeleteRelationship).Click();
            Thread.Sleep(2000);
        }
    }
}