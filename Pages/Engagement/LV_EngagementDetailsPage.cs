using OpenQA.Selenium;
using SF_Automation.UtilityFunctions;
using System;
using System.Threading;

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

        By btnMoreAdmin = By.XPath("(//button[@title='More Tabs'])[3]");
        By btnDeleteActivity = By.XPath("//button[@title='Delete']");

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
                        Thread.Sleep(3000);
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

        public bool VerifyClickingOnTheEngagementNameUnderAssociatedEngagementsSectionTakesUserToEngagementDetailsPage()
        {
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

                if(driver.FindElement(txtEngagementName).Text == engagementNames[i])
                {
                    result = true;
                    Thread.Sleep(5000);
                    driver.FindElement(By.XPath("(//button[contains(@title,'| Engagement')])[2]")).Click();
                    Thread.Sleep(3000);
                    j++;
                    continue;
                }
                else
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public bool VerifyClickingOnTheEngagementNameUnderReferralsSectionTakesUserToEngagementDetailsPage()
        {
            bool result = false;

            //Get the no. of Engagements under Referrals section
            int noOfEngagements = driver.FindElements(By.XPath("//b[text()='Referrals ']/following::div/dl/dt/p[text()='Name: ']")).Count;

            //Get the name of each Engagement and store in an array
            String[] engagementNames = new String[noOfEngagements];
            int j = 1;

            for(int i = 0; i <= noOfEngagements - 1; i++)
            {
                engagementNames[i] = driver.FindElement(By.XPath($"(//b[text()='Referrals ']/following::div)[{j}]/dl/dd/p/button")).Text;
                driver.FindElement(By.XPath($"(//b[text()='Referrals ']/following::div)[{j}]/dl/dd/p/button")).Click();

                WebDriverWaits.WaitUntilEleVisible(driver, txtEngagementName, 120);

                if(driver.FindElement(txtEngagementName).Text == engagementNames[i])
                {
                    result = true;
                    Thread.Sleep(3000);
                    driver.FindElement(By.XPath("(//button[contains(@title,'| Engagement')])[2]")).Click();
                    Thread.Sleep(3000);
                    j++;
                    continue;
                }
                else
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        public bool VerifyActivityIsLinkedToEngagement(string sub)
        {
            bool result = false;
            driver.FindElement(btnMoreAdmin).Click();
            Thread.Sleep(3000);
            try
            {
                driver.FindElement(By.XPath("//span[text()='Activity']/..")).Click();
                Thread.Sleep(5000);
            }
            catch(Exception)
            {

            }

            Thread.Sleep(5000);
            if(driver.FindElement(By.XPath($"(//a[text()='{sub}'])[2]")).Displayed)
            {
                result = true;
            }
            else if(driver.FindElement(By.XPath($"(//a[text()='{sub}'])[3]")).Displayed)
            {
                result = true;
            }
            else if(driver.FindElement(By.XPath($"(//a[text()='{sub}'])[4]")).Displayed)
            {
                result = true;
            }
            return result;
        }

        public void ViewActivityFromList(string name)
        {
            try
            {
                Thread.Sleep(2000);
                CustomFunctions.ActionClick(driver, driver.FindElement(By.XPath($"(//a[text()='{name}'])[3]")), 60);
                Thread.Sleep(3000);
            }
            catch(Exception)
            {
                Thread.Sleep(2000);
                CustomFunctions.ActionClick(driver, driver.FindElement(By.XPath($"(//a[text()='{name}'])[4]")), 60);
                Thread.Sleep(3000);
            }
        }

        public void DeleteActivity()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteActivity, 60);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnDeleteActivity));
            driver.FindElement(btnDeleteActivity).Click();
            Thread.Sleep(2000);
        }

    }
}

