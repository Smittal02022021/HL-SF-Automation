using AventStack.ExtentReports;
using OpenQA.Selenium;
using RazorEngine.Compilation.ImpromptuInterface;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;

namespace SF_Automation.Pages.Companies
{
    class LV_EngagementDetailsPage : BaseClass
    {
        //General
        By txtEngagementName = By.XPath("//h1/slot/lightning-formatted-text");

        //Tabs
        By linkEngagementContacts = By.XPath("//a[@data-label='Eng Contacts']");

        //Elements under Engagement Contacts Tab
        By linkViewAllEngContacts = By.XPath("//span[@title='Engagement Contacts']/parent::a");
        By btnCloseEngagementContacts = By.XPath("//button[@title='Close Engagement Contacts']");

        public bool VerifyAssociatedEngagementsSectionOnContactDetailsPageDisplaysEngagementsWhereTheExternalContactIsAnEngagementContact(string exlEngContact)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            bool result = false;

            //Get the no. of Engagements under Associated Engagements section
            int noOfEngagements = driver.FindElements(By.XPath("//b[text()='Associated Engagements ']/../../div/dl")).Count;

            //Get the name of each Engagement and store in an array
            String[] engagementNames = new String[noOfEngagements];
            int j = 1;

            for(int i = 0; i <= noOfEngagements - 1; i++)
            {
                engagementNames[i] = driver.FindElement(By.XPath($"(//b[text()='Associated Engagements ']/../../div/dl)[{j}]/dd/p/button")).Text;
                driver.FindElement(By.XPath($"(//b[text()='Associated Engagements ']/../../div/dl)[{j}]/dd/p/button")).Click();

                WebDriverWaits.WaitUntilEleVisible(driver, txtEngagementName, 120);

                try
                {
                    if(driver.FindElement(txtEngagementName).Text == engagementNames[i])
                    {
                        WebDriverWaits.WaitUntilEleVisible(driver, linkEngagementContacts, 120);
                        driver.FindElement(linkEngagementContacts).Click();
                        WebDriverWaits.WaitUntilEleVisible(driver, linkViewAllEngContacts, 120);
                        driver.FindElement(linkViewAllEngContacts).Click();

                        Thread.Sleep(5000);

                        //Get the total no of contacts
                        int totalNoOfEngagementContacts = driver.FindElements(By.XPath("//table[@aria-label='Engagement Contacts']/tbody/tr")).Count;

                        for(int row = 1; row <= totalNoOfEngagementContacts; row++)
                        {
                            //Get the eng contact name from each row
                            string engContactName = driver.FindElement(By.XPath($"(//table[@aria-label='Engagement Contacts']/tbody/tr)[{row}]/th/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/formula-output-formula-html/lightning-formatted-rich-text/span/a[2]")).Text;

                            if(engContactName==exlEngContact)
                            {
                                result = true;
                                driver.FindElement(btnCloseEngagementContacts).Click();
                                Thread.Sleep(2000);
                                break;
                            }
                            else
                            {
                                result = false;
                                continue;
                            }
                        }

                        driver.FindElement(By.XPath($"//span[contains(text(),'Close {engagementNames[i]}')]/..")).Click();
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        result = false;
                        break;
                    }
                    j++;
                }
                catch(Exception)
                {

                }
            }

            return result;
        }
    }
}

