using Microsoft.AspNetCore.Http;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V136.DOM;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Globalization;
using System.Net.PeerToPeer;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace SF_Automation.Pages.Companies
{
    class LV_EngagementDetailsPage : BaseClass
    {
        //General
        By txtEngagementName = By.XPath("//h1/slot/lightning-formatted-text");
        By btnCancelEditFormL = By.XPath("//button[@name='CancelEdit']");
        By btnSaveDetailsL = By.XPath("//button[@name='SaveEdit']");
        By txtEngNumberL = By.XPath("//span[contains(@class,'field-label')][normalize-space()='Engagement Number']/following::div[2]//lightning-formatted-text");//::dl//dd//lightning-formatted-text");//span[contains(@class,'field-label')][normalize-space()='Engagement Number']/parent::div/following-sibling::div//lightning-formatted-text");
        By txtEngNameL = By.XPath("(//span[contains(@class,'field-label')][normalize-space()='Engagement Name']/following::div/span//lightning-formatted-text)[1]");//::dl//dd//lightning-formatted-text");//span[@class='test-id__field-label'][normalize-space()='Engagement Name']/parent::div/following-sibling::div//lightning-formatted-text");

        //Tabs
        By linkEngagementContacts = By.XPath("//a[@data-label='Eng Contacts']");

        //Elements under Engagement Contacts Tab
        By linkViewAllEngContacts = By.XPath("//span[@title='Engagement Contacts']/parent::a");
        By btnCloseEngagementContacts = By.XPath("//button[@title='Close Engagement Contacts']");

        By btnMoreAdmin = By.XPath("(//button[@title='More Tabs'])[3]");
        By btnDeleteActivity = By.XPath("//button[@title='Delete']");

        //Potential Round Trip - Sahil
        By btnEditPotentialRoundTrip = By.XPath("//button[@title='Edit Engagement is a potential round trip']");
        By btnEditEngagementStage = By.XPath("//button[@title='Edit Stage']");
        By txtCloseDate = By.XPath("//input[@name='Close_Date__c']");
        By btnReminderClose = By.XPath("//button[text()='Close']");
        By warningMsgModal = By.XPath("(//div[@part='modal-body']//h2[contains(text(),'A Subject is')])[2]");
        By warningMsgModal1 = By.XPath("(//div[@part='modal-body']//h2[contains(text(),'A Buyer is')])[1]");
        By warningMsgModal2 = By.XPath("(//div[@part='modal-body']//h2)[1]");
        By warningMsgModal3 = By.XPath("(//div[@part='modal-body']//h2[contains(text(),'Companies')])[2]");

        By lblPotentialRoundTrip = By.XPath("//span[text()='Edit Potential Round Trip']/../..//lightning-formatted-text");
        By lblRoundTripEngagement = By.XPath("(//span[text()='Edit Round Trip Engagement']/../../span//records-hoverable-link//a//span)[3]");
        By lblPotentialRoundTripModifiedDate = By.XPath("(//span[text()='Potential Round Trip Last Modified Date']/following::div)[1]//slot/lightning-formatted-text");
        By lblRoundTripComment = By.XPath("(//span[text()='Round Trip Comment']/following::div)[1]//slot/lightning-formatted-text");

        //*********************************************************************************************************


        public bool VerifyAssociatedEngagementsSectionOnContactDetailsPageDisplaysEngagementsWhereTheExternalContactIsAnEngagementContact(string exlEngContact)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            bool result = false;

            //Get the no. of Engagements under Associated Engagements section
            int noOfEngagements = driver.FindElements(By.XPath("//b[text()='Associated Engagements ']/../../div/dl")).Count;

            //Get the name of each Engagement and store in an array
            String[] engagementNames = new String[noOfEngagements];
            int j = 1;

            for (int i = 0; i <= noOfEngagements - 1; i++)
            {
                engagementNames[i] = driver.FindElement(By.XPath($"(//b[text()='Associated Engagements ']/../../div/dl)[{j}]/dd/p/button")).Text;
                driver.FindElement(By.XPath($"(//b[text()='Associated Engagements ']/../../div/dl)[{j}]/dd/p/button")).Click();

                WebDriverWaits.WaitUntilEleVisible(driver, txtEngagementName, 120);

                try
                {
                    if (driver.FindElement(txtEngagementName).Text == engagementNames[i])
                    {
                        WebDriverWaits.WaitUntilEleVisible(driver, linkEngagementContacts, 120);
                        driver.FindElement(linkEngagementContacts).Click();
                        Thread.Sleep(3000);
                        WebDriverWaits.WaitUntilEleVisible(driver, linkViewAllEngContacts, 120);
                        driver.FindElement(linkViewAllEngContacts).Click();

                        Thread.Sleep(5000);

                        //Get the total no of contacts
                        int totalNoOfEngagementContacts = driver.FindElements(By.XPath("//table[@aria-label='Engagement Contacts']/tbody/tr")).Count;

                        for (int row = 1; row <= totalNoOfEngagementContacts; row++)
                        {
                            //Get the eng contact name from each row
                            string engContactName = driver.FindElement(By.XPath($"(//table[@aria-label='Engagement Contacts']/tbody/tr)[{row}]/th/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/formula-output-formula-html/lightning-formatted-rich-text/span/a[2]")).Text;

                            if (engContactName == exlEngContact)
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
                catch (Exception)
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

            for (int i = 0; i <= noOfEngagements - 1; i++)
            {
                engagementNames[i] = driver.FindElement(By.XPath($"(//b[text()='Associated Engagements ']/../../div/dl)[{j}]/dd/p/button")).Text;
                driver.FindElement(By.XPath($"(//b[text()='Associated Engagements ']/../../div/dl)[{j}]/dd/p/button")).Click();

                WebDriverWaits.WaitUntilEleVisible(driver, txtEngagementName, 120);

                if (driver.FindElement(txtEngagementName).Text == engagementNames[i])
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

            for (int i = 0; i <= noOfEngagements - 1; i++)
            {
                engagementNames[i] = driver.FindElement(By.XPath($"(//b[text()='Referrals ']/following::div)[{j}]/dl/dd/p/button")).Text;
                driver.FindElement(By.XPath($"(//b[text()='Referrals ']/following::div)[{j}]/dl/dd/p/button")).Click();

                WebDriverWaits.WaitUntilEleVisible(driver, txtEngagementName, 120);

                if (driver.FindElement(txtEngagementName).Text == engagementNames[i])
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
            catch (Exception)
            {

            }

            Thread.Sleep(5000);
            try
            {
                if (driver.FindElement(By.XPath("((//slot[@name='customdatatypes'])[3]/..//table//tbody//tr)[1]/td[4]//a")).Displayed)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        public void ViewActivityFromList(string name)
        {
            try
            {
                Thread.Sleep(2000);
                CustomFunctions.ActionClick(driver, driver.FindElement(By.XPath($"((//slot[@name='customdatatypes'])[3]/..//table//tbody//tr)[1]/td[4]//a[text()='{name}']")), 60);
                Thread.Sleep(3000);
            }
            catch (Exception)
            {

            }
        }

        public void DeleteActivity()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteActivity, 60);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnDeleteActivity));
            driver.FindElement(btnDeleteActivity).Click();
            Thread.Sleep(2000);
        }

        public string GetEngagementNumberL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngNumberL, 30);
            return driver.FindElement(txtEngNumberL).Text;
        }

        public string GetEngagementNameL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngNameL, 30);
            return driver.FindElement(txtEngNameL).Text;
        }

        ////////////Round Trip Functions
        ///
        ///////////////////////

        public bool VerifyIfEngagementIsAPotentialRoundTripIsAddedAndItIsNoneByDefault()
        {
            bool result = false;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnEditPotentialRoundTrip, 60);
            if (driver.FindElement(btnEditPotentialRoundTrip).Displayed == true)
            {
                driver.FindElement(btnEditPotentialRoundTrip).Click();
                Thread.Sleep(3000);
                if (driver.FindElement(By.XPath("//button[@aria-label='Engagement is a potential round trip']/span")).Text == "--None--")
                {
                    result = true;
                    driver.FindElement(btnCancelEditFormL).Click();
                    Thread.Sleep(2000);
                }
            }
            return result;
        }

        public bool VerifyPotentialRoundTripPicklistValues(string file)
        {
            bool result = false;
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(2000);

            driver.FindElement(btnEditPotentialRoundTrip).Click();
            Thread.Sleep(3000);

            int excelCount = ReadExcelData.GetRowCount(excelPath, "PicklistValues");

            driver.FindElement(By.XPath("//button[@aria-label='Engagement is a potential round trip']")).Click();
            int valueCount = driver.FindElements(By.XPath("//div[@aria-label='Engagement is a potential round trip']//lightning-base-combobox-item//span[2]/span")).Count;

            for (int i = 2; i <= excelCount; i++)
            {
                string excelValue = ReadExcelData.ReadDataMultipleRows(excelPath, "PicklistValues", i, 1);

                for (int j = 1; j <= valueCount; j++)
                {
                    string picklistValue = driver.FindElement(By.XPath($"(//div[@aria-label='Engagement is a potential round trip']//lightning-base-combobox-item//span[2]/span)[{j}]")).Text;
                    if (excelValue == picklistValue)
                    {
                        if (i == excelCount)
                        {
                            result = true;
                            driver.FindElement(btnCancelEditFormL).Click();
                            Thread.Sleep(2000);
                            break;
                        }
                        break;
                    }

                }
            }

            return result;
        }

        public bool VerifyHoverIconDescriptionForEngagementIsAPotentialRoundTripField(string icon)
        {
            bool result = false;
            Thread.Sleep(2000);

            if (driver.FindElement(By.XPath("(//span[text()='Help Engagement is a potential round trip']/following::span)[1]")).Text == icon)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyIfCompaniesClosedWithIsMissing()
        {
            bool result = false;

            //Navigate to Closing Info tab
            driver.FindElement(By.XPath("//a[@data-label='Closing Info']")).Click();
            Thread.Sleep(5000);

            //Verify if count is 0
            if (driver.FindElement(By.XPath("(//span[@title='Counterparties Closed With']/../span)[2]")).Text == "(0)")
            {
                result = true;
            }

            return result;
        }

        public void NavigateToClosingInfoTab()
        {
            //Navigate to Closing Info tab
            Thread.Sleep(5000);

            driver.FindElement(By.XPath("//a[@data-label='Closing Info']")).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyConclusionDateFieldIsNotDisplayed()
        {
            bool result = false;

            if (CustomFunctions.IsElementPresent(driver, By.XPath("//span[text()='Conclusion Date']")) == false)
            {
                result = true;
            }

            return result;
        }

        public bool NavigateToEngagementSummaryReportPage()
        {
            bool result = false;

            //Click on More Tabs
            driver.FindElement(By.XPath("//span[text()='Show more actions']/..")).Click();
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//span[text()='Engagement Summary (CF)']/..")).Click();
            Thread.Sleep(10000);

            if (CustomFunctions.IsElementPresent(driver, By.XPath("//button[@title='Summary Report']")) == true)
            {
                result = true;
            }

            return result;
        }

        public bool VerifyCloseDateIsDisplayed()
        {
            bool result = false;

            if (CustomFunctions.IsElementPresent(driver, By.XPath("//div[@title='Close Date']")) == true)
            {
                result = true;
            }

            return result;
        }

        public string GetCloseDateValue()
        {
            string closeDate = string.Empty;
            if (CustomFunctions.IsElementPresent(driver, By.XPath("(//div[@title='Close Date']/following::div)[1]")) == true)
            {
                closeDate = driver.FindElement(By.XPath("(//div[@title='Close Date']/following::div)[1]")).Text;
            }
            return closeDate;
        }

        public bool VerifyCloseDateIsSameUnderEngagementTimelinesSection(string engCloseDate)
        {
            bool result = false;

            driver.FindElement(By.XPath("//span[text()='Engagement Timeline']/..")).Click();
            Thread.Sleep(5000);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(5000);

            //Get the Close Date under Engagement Timelines section
            string closeDateUnderEngagementTimelines = driver.FindElement(By.XPath("(//span[text()='Close Date'])[4]/following::div/lightning-formatted-text")).Text;
            if (engCloseDate == closeDateUnderEngagementTimelines)
            {
                result = true;
            }
            return result;
        }

        public string GetClosedWeeksFromDateEngagedValue()
        {
            string closedWeek = string.Empty;
            if (CustomFunctions.IsElementPresent(driver, By.XPath("(//span[text()='Closed - Weeks From Date Engaged']/following::lightning-formatted-number)[1]")) == true)
            {
                closedWeek = driver.FindElement(By.XPath("(//span[text()='Closed - Weeks From Date Engaged']/following::lightning-formatted-number)[1]")).Text;
            }
            return closedWeek;
        }

        public bool VerifyClosedWeeksFromDateEngagedValueIsAsPerFormula(string closedWeeks, string closeD, string dateEngaged)
        {
            bool result = false;

            DateTime closedDate = DateTime.Parse(closeD);
            DateTime dateEng = DateTime.Parse(dateEngaged);

            //Applying the conversion formula
            TimeSpan diff = closedDate - dateEng;
            int weeks = (int)(diff.TotalDays / 7);

            //Converting closed weeks string into integer
            int number = Convert.ToInt32(Convert.ToDouble(closedWeeks));

            if (weeks.Equals(number))
            {
                result = true;
            }
            return result;
        }

        public string GetDateEngagedValue()
        {
            string dateEngaged = string.Empty;
            if (CustomFunctions.IsElementPresent(driver, By.XPath("(//span[text()='Date Engaged']/following::lightning-formatted-text[@class='slds-border_bottom slds-form-element__static'])[3]")) == true)
            {
                dateEngaged = driver.FindElement(By.XPath("(//span[text()='Date Engaged']/following::lightning-formatted-text[@class='slds-border_bottom slds-form-element__static'])[3]")).Text;
            }
            return dateEngaged;
        }

        public bool VerifyIfCompaniesClosedWithIsPresent()
        {
            bool result = false;

            //Navigate to Closing Info tab
            driver.FindElement(By.XPath("//a[@data-label='Closing Info']")).Click();
            Thread.Sleep(5000);

            //Verify if count is 0
            if (driver.FindElement(By.XPath("(//span[@title='Counterparties Closed With']/../span)[2]")).Text == "(1)")
            {
                result = true;
            }

            return result;
        }

        public void ChangeEngagementStageToClosed()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,1000)");
            Thread.Sleep(3000);

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEditEngagementStage, 60);
                driver.FindElement(btnEditEngagementStage).Click();
                Thread.Sleep(3000);

                driver.FindElement(By.XPath("//button[@aria-label='Stage']")).Click();
                Thread.Sleep(2000);

                int valueCount = driver.FindElements(By.XPath("//div[@aria-label='Stage']//lightning-base-combobox-item//span[2]/span")).Count;

                for (int i = 1; i <= valueCount; i++)
                {
                    string picklistValue = driver.FindElement(By.XPath($"(//div[@aria-label='Stage']//lightning-base-combobox-item//span[2]/span)[{i}]")).Text;
                    if (picklistValue == "Closed")
                    {
                        driver.FindElement(By.XPath($"(//div[@aria-label='Stage']//lightning-base-combobox-item//span[2]/span)[{i}]")).Click();
                        Thread.Sleep(2000);

                        //Enter Close Date
                        driver.FindElement(txtCloseDate).SendKeys(DateTime.Now.Date.ToString("MM/dd/yyyy").Replace("-", "/"));
                        Thread.Sleep(2000);

                        //Click Save
                        driver.FindElement(btnSaveDetailsL).Click();
                        Thread.Sleep(2000);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Thread.Sleep(2000);
            }
        }

        public void CloseEstimatedRevenueDateReminderPopup()
        {
            Thread.Sleep(10000);
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                WebDriverWaits.WaitUntilEleVisible(driver, btnReminderClose, 60);
                jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnReminderClose));
                jse.ExecuteScript("window.scrollTo(0,0)");
            }
            catch (Exception)
            {
                Thread.Sleep(2000);
            }
            Thread.Sleep(3000);
        }

        public string GetEngagementStage()
        {
            Thread.Sleep(2000);
            string stageName = driver.FindElement(By.XPath("((//span[text()='Stage'][@class='test-id__field-label'])[1]/following::div//lightning-formatted-text)[1]")).Text;
            return stageName;
        }

        public void SelectValueInPotentialRoundTripField(string value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,1000)");
            Thread.Sleep(3000);

            driver.FindElement(btnEditPotentialRoundTrip).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//button[@aria-label='Engagement is a potential round trip']")).Click();
            int valueCount = driver.FindElements(By.XPath("//div[@aria-label='Engagement is a potential round trip']//lightning-base-combobox-item//span[2]/span")).Count;

            for (int j = 1; j <= valueCount; j++)
            {
                string picklistValue = driver.FindElement(By.XPath($"(//div[@aria-label='Engagement is a potential round trip']//lightning-base-combobox-item//span[2]/span)[{j}]")).Text;
                if (value == picklistValue)
                {
                    driver.FindElement(By.XPath($"(//div[@aria-label='Engagement is a potential round trip']//lightning-base-combobox-item//span[2]/span)[{j}]")).Click();
                    Thread.Sleep(2000);

                    //Click Save
                    driver.FindElement(btnSaveDetailsL).Click();
                    Thread.Sleep(2000);
                    break;
                }
            }
        }

        public bool VerifyNoWarningMsgIsDisplayed()
        {
            bool result = false;

            Thread.Sleep(5000);
            if (CustomFunctions.IsElementPresent(driver, warningMsgModal) == false)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyWarningMsgIsDisplayed()
        {
            bool result = false;

            Thread.Sleep(5000);
            if (CustomFunctions.IsElementPresent(driver, warningMsgModal) == true)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyWarningMsgIsDisplayedUponMissingCompaniesClosedWith()
        {
            bool result = false;

            Thread.Sleep(5000);
            if (CustomFunctions.IsElementPresent(driver, warningMsgModal3) == true)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyNeitherBuyerNorSubjectWarningMsgIsDisplayed()
        {
            bool result = false;

            Thread.Sleep(5000);
            if (CustomFunctions.IsElementPresent(driver, warningMsgModal2) == true)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyBuyerWarningMsgIsDisplayed()
        {
            bool result = false;

            try
            {
                Thread.Sleep(5000);
                if (CustomFunctions.IsElementPresent(driver, warningMsgModal1) == true)
                {
                    result = true;
                }
            }
            catch (Exception)
            {

            }
            return result;
        }

        public bool VerifyWarningMsg(string message)
        {
            bool result = false;
            if (driver.FindElement(warningMsgModal).Text == message)
            {
                result = true;
            }

            CustomFunctions.PageReload(driver);
            Thread.Sleep(10000);

            return result;
        }

        public bool VerifyWarningMsgUponMissingCompaniesClosedWith(string message)
        {
            bool result = false;
            if (driver.FindElement(warningMsgModal3).Text == message)
            {
                result = true;
            }

            CustomFunctions.PageReload(driver);
            Thread.Sleep(10000);

            return result;
        }

        public bool VerifBuyerWarningMsg(string message)
        {
            bool result = false;
            if (driver.FindElement(warningMsgModal1).Text == message)
            {
                result = true;
            }

            CustomFunctions.PageReload(driver);
            Thread.Sleep(10000);

            return result;
        }

        public bool VerifyNeitherSubjectNorBuyerWarningMsg(string message)
        {
            bool result = false;
            if (driver.FindElement(warningMsgModal2).Text == message)
            {
                result = true;
            }

            CustomFunctions.PageReload(driver);
            Thread.Sleep(10000);

            return result;
        }

        public string GetSubjectCompanyType(string compName)
        {
            Thread.Sleep(3000);
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath("(//p[text()='Subject']/following::p//a)[3]")));
            }
            catch (Exception)
            {

            }
            Thread.Sleep(8000);

            string companyType = driver.FindElement(By.XPath("((//span[text()='Company Type'])[1]/following::div//span)[3]//div/div/span")).Text;

            //Close the warning message if any
            try
            {
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//button[contains(@class,'toastClose')]")).Click();
                Thread.Sleep(2000);

                //Close the tab
                driver.FindElement(By.XPath($"//button[contains(@title, 'Close {compName}')]")).Click();
                Thread.Sleep(2000);
            }
            catch (Exception)
            {
                //Close the tab
                driver.FindElement(By.XPath($"//button[contains(@title, 'Close {compName}')]")).Click();
                Thread.Sleep(2000);
            }

            return companyType;
        }

        public string GetClientOwnership(string compName)
        {
            Thread.Sleep(3000);
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath("(//p[text()='Client']/following::p//a)[3]")));
            }
            catch (Exception)
            {

            }
            Thread.Sleep(8000);

            string clientOwnership = driver.FindElement(By.XPath("((//span[text()='Ownership'])[1]/following::div//lightning-formatted-text)[1]")).Text;

            //Close the warning message if any
            try
            {
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//button[contains(@class,'toastClose')]")).Click();
                Thread.Sleep(2000);

                //Close the tab
                driver.FindElement(By.XPath($"//button[contains(@title, 'Close {compName}')]")).Click();
                Thread.Sleep(2000);
            }
            catch (Exception)
            {
                //Close the tab
                driver.FindElement(By.XPath($"//button[contains(@title, 'Close {compName}')]")).Click();
                Thread.Sleep(2000);
            }

            return clientOwnership;
        }

        public string GetCounterpartyCompanyOwnership(string compName)
        {
            Thread.Sleep(3000);
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath($"//lightning-primitive-cell-factory[@data-label='Company']//a[@title='{compName}']")));
            }
            catch (Exception)
            {

            }
            Thread.Sleep(8000);

            string ownership = driver.FindElement(By.XPath("((//span[text()='Ownership'])[1]/following::div//lightning-formatted-text)[1]")).Text;

            //Close the warning message if any
            try
            {
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//button[contains(@class,'toastClose')]")).Click();
                Thread.Sleep(2000);

                //Close the tab
                driver.FindElement(By.XPath($"//button[contains(@title, 'Close {compName}')]")).Click();
                Thread.Sleep(2000);
            }
            catch (Exception)
            {
                //Close the tab
                driver.FindElement(By.XPath($"//button[contains(@title, 'Close {compName}')]")).Click();
                Thread.Sleep(2000);
            }

            return ownership;
        }

        public string GetCounterpartyCompanyType(string compName)
        {
            Thread.Sleep(3000);
            try
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath($"//lightning-primitive-cell-factory[@data-label='Company']//a[@title='{compName}']")));
            }
            catch (Exception)
            {

            }
            Thread.Sleep(8000);

            string compType = driver.FindElement(By.XPath("((//span[text()='Company Type'])[1]/following::div//span)[3]//div/div/span")).Text;

            //Close the warning message if any
            try
            {
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//button[contains(@class,'toastClose')]")).Click();
                Thread.Sleep(2000);

                //Close the tab
                driver.FindElement(By.XPath($"//button[contains(@title, 'Close {compName}')]")).Click();
                Thread.Sleep(2000);
            }
            catch (Exception)
            {
                //Close the tab
                driver.FindElement(By.XPath($"//button[contains(@title, 'Close {compName}')]")).Click();
                Thread.Sleep(2000);
            }

            return compType;
        }



        public bool VerifyUpdatesOnSubjectCompany(string engName, string roundTrip, string comment, string compName)
        {
            bool result = false;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(3000);

            //Navigate to company detail page
            js.ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath("(//p[text()='Subject']/following::p//a)[3]")));
            Thread.Sleep(10000);

            js.ExecuteScript("window.scrollTo(0,1000)");
            Thread.Sleep(5000);

            string potentialRoundTripValue = driver.FindElement(lblPotentialRoundTrip).Text;
            string roundTripEngagementValue = driver.FindElement(lblRoundTripEngagement).Text;
            string roundTripModifiedDateValue = driver.FindElement(lblPotentialRoundTripModifiedDate).Text;
            string roundTripCommentValue = driver.FindElement(lblRoundTripComment).Text;

            string currentDate = DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture).Replace('-', '/');

            try
            {
                DateTime parsedDate = DateTime.ParseExact(currentDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                // Convert it to M/d/yyyy (removes leading zeros)
                string newFormat = parsedDate.ToString("M/d/yyyy").Replace('-', '/');
                if (potentialRoundTripValue == roundTrip && roundTripEngagementValue == engName && roundTripCommentValue == comment && roundTripModifiedDateValue.Contains(newFormat))
                {
                    result = true;
                }
            }
            catch (Exception)
            {

            }

            if (potentialRoundTripValue == roundTrip && roundTripEngagementValue == engName && roundTripCommentValue == comment && roundTripModifiedDateValue.Contains(currentDate))
            {
                result = true;
            }

            return result;
        }

        public bool VerifyUpdatesOnClientCompany(string engName, string roundTrip, string comment, string compName)
        {
            bool result = false;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(3000);

            //Navigate to company detail page
            js.ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath("(//p[text()='Client']/following::p//a)[3]")));
            Thread.Sleep(10000);

            js.ExecuteScript("window.scrollTo(0,1000)");
            Thread.Sleep(5000);

            string potentialRoundTripValue = driver.FindElement(lblPotentialRoundTrip).Text;
            string roundTripEngagementValue = driver.FindElement(lblRoundTripEngagement).Text;
            string roundTripModifiedDateValue = driver.FindElement(lblPotentialRoundTripModifiedDate).Text;
            string roundTripCommentValue = driver.FindElement(lblRoundTripComment).Text;

            string currentDate = DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture).Replace('-', '/');

            try
            {
                DateTime parsedDate = DateTime.ParseExact(currentDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                // Convert it to M/d/yyyy (removes leading zeros)
                string newFormat = parsedDate.ToString("M/d/yyyy").Replace('-', '/');
                if (potentialRoundTripValue == roundTrip && roundTripEngagementValue == engName && roundTripCommentValue == comment && roundTripModifiedDateValue.Contains(newFormat))
                {
                    result = true;
                }
            }
            catch (Exception)
            {

            }

            if (potentialRoundTripValue == roundTrip && roundTripEngagementValue == engName && roundTripCommentValue == comment && roundTripModifiedDateValue.Contains(currentDate))
            {
                result = true;
            }

            return result;
        }

        public bool VerifyUpdatesOnCompanyClosedWithBuyerCompany(string engName, string roundTrip, string comment, string compName)
        {
            bool result = false;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(3000);

            //Navigate to Closing Info tab
            driver.FindElement(By.XPath("//a[@data-label='Closing Info']")).Click();
            Thread.Sleep(5000);

            //Navigate to company detail page
            js.ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath($"//lightning-primitive-cell-factory[@data-label='Company']//a[@title='{compName}']")));
            Thread.Sleep(10000);

            js.ExecuteScript("window.scrollTo(0,1000)");
            Thread.Sleep(5000);

            string potentialRoundTripValue = driver.FindElement(lblPotentialRoundTrip).Text;
            string roundTripEngagementValue = driver.FindElement(lblRoundTripEngagement).Text;
            string roundTripModifiedDateValue = driver.FindElement(lblPotentialRoundTripModifiedDate).Text;
            string roundTripCommentValue = driver.FindElement(lblRoundTripComment).Text;

            string currentDate = DateTime.Now.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture).Replace('-', '/');

            try
            {
                DateTime parsedDate = DateTime.ParseExact(currentDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

                // Convert it to M/d/yyyy (removes leading zeros)
                string newFormat = parsedDate.ToString("M/d/yyyy").Replace('-', '/');
                if (potentialRoundTripValue == roundTrip && roundTripEngagementValue == engName && roundTripCommentValue == comment && roundTripModifiedDateValue.Contains(newFormat))
                {
                    result = true;
                }
            }
            catch (Exception)
            {

            }

            if (potentialRoundTripValue == roundTrip && roundTripEngagementValue == engName && roundTripCommentValue == comment && roundTripModifiedDateValue.Contains(currentDate))
            {
                result = true;
            }

            return result;
        }


        public void ClickViewCounterpartiesButton()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(3000);

            //Click View Counterparties button
            driver.FindElement(By.XPath("//button[@name='Engagement__c.ViewCounterparties']")).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyViewCounterpartiesPageIsDisplayed()
        {
            bool result = false;
            Thread.Sleep(5000);

            //Check if the page title is displayed
            if (driver.Title.Contains("Counterparty Editor | Salesforce"))
            {
                result = true;
            }
            return result;
        }
    }
}

