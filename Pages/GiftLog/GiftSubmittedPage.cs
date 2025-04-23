using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages.GiftLog
{
    class GiftSubmittedPage : BaseClass
    {
        By shwAllTab = By.CssSelector("li[id='AllTab_Tab'] > a > img");
        By linkGiftsSubmitted = By.XPath("//img[@alt='Gifts Submitted']/..");
        By GiftDescColLength = By.CssSelector("table[id='j_id0:theSubmitterForm:j_id34:table'] > tbody > tr");
        By valGiftRequestDetail = By.CssSelector("h2[class='mainTitle']");
        By btnEdit = By.CssSelector("td[class='pbButton'] > input[name='edit']");
        By tableList = By.CssSelector("table[id='j_id0:theSubmitterForm:j_id34:table'] > tbody");
        By valGiftType = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(2) > td:nth-child(2)");
        By valHLRelationship = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(8) > td:nth-child(2)");
        By valGiftValue = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(6) > td:nth-child(2)");
        By valEditedReason = By.CssSelector("table[class='detailList'] >tbody > tr:nth-child(9) > td:nth-child(2)");
        By btnDelete = By.CssSelector("td[id='topButtonRow'] > input[value='Delete']");

        By comboApprovedStatus = By.XPath("//select[@id='j_id0:theSubmitterForm:j_id27:searchOptions']");
        By btnGo = By.XPath("//input[@value='Go']");

        By frameGiftSubmittedL = By.XPath("//iframe[@title='accessibility title']");



        public bool ValidateIfGiftDescIsClickableLV(string giftName)
        {
            IList<IWebElement> element = driver.FindElements(GiftDescColLength);
            int totalRows = element.Count;
            bool result = false;

            for(int i = 1; i <= totalRows; i++)
            {
                By giftDesc = By.CssSelector($"table[id='j_id0:theSubmitterForm:j_id34:table'] > tbody > tr:nth-child({i}) > td:nth-child(1) > span");
                IWebElement descGiftWebElement = driver.FindElement(giftDesc);
                string descGift = descGiftWebElement.Text;
                if(descGift.Equals(giftName))
                {
                    try
                    {
                        By linkGift = By.CssSelector($"table[id='j_id0:theSubmitterForm:j_id34:table'] > tbody > tr:nth-child({i}) > td:nth-child(1) > a");
                        WebDriverWaits.WaitUntilEleVisible(driver, linkGift, 5);
                        result = true;
                        break;
                    }
                    catch(Exception)
                    {
                        break;
                    }
                }
            }
            return result;
        }
        public void SearchByOnlyStatusLV(string status)
        {

            WebDriverWaits.WaitUntilEleVisible(driver, comboApprovedStatus);
            driver.FindElement(comboApprovedStatus).SendKeys(status);
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(3000);
        }
        public bool IsGiftLogPresentInSubmittedListLV(string giftName)
        {
            bool recordFound = false;
            IList<IWebElement> element = driver.FindElements(GiftDescColLength);
            int totalRows = element.Count;

            for(int i = 1; i <= totalRows; i++)
            {
                By linkGiftDesc = By.CssSelector($"table[id='j_id0:theSubmitterForm:j_id34:table'] > tbody > tr:nth-child({i}) > td:nth-child(1) > a");
                IWebElement descGiftWebElement = driver.FindElement(linkGiftDesc);
                string descGift = descGiftWebElement.Text;
                if(descGift.Equals(giftName))
                {
                    recordFound = true;
                    Thread.Sleep(3000);
                    break;
                }
                else
                {
                    recordFound = false;
                }
            }
            return recordFound;
        }
        public void DeleteGiftSubmittedLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDelete, 20);
            driver.FindElement(btnDelete).Click();
            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(8000);
        }

        public string GetGiftReasonUpdatedValueLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEditedReason, 20);
            string giftReasonInDetail = driver.FindElement(valEditedReason).Text;
            return giftReasonInDetail;
        }
        public string GetGiftValueUpdatedLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftValue, 20);
            string giftValueInDetail = driver.FindElement(valGiftValue).Text.Split('(')[0].Trim();
            return giftValueInDetail;
        }
        public string GetHLRelationshipUpdatedValueLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valHLRelationship, 20);
            string hlRelationshipInDetail = driver.FindElement(valHLRelationship).Text;
            return hlRelationshipInDetail;
        }
        public string GetGiftTypeUpdatedValueLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftType, 20);
            string giftTypeInDetail = driver.FindElement(valGiftType).Text;
            return giftTypeInDetail;
        }
        public bool IsTableListPresentLV()
        {
            Thread.Sleep(10000);
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, frameGiftSubmittedL, 20);
            driver.SwitchTo().Frame(driver.FindElement(frameGiftSubmittedL));
            return CustomFunctions.IsElementPresent(driver, tableList);
        }
        public void ClickEditButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 20);
            IJavaScriptExecutor jse = (IJavaScriptExecutor) driver;
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnEdit));
            Thread.Sleep(5000);
        }
        public string GetGiftSubmittedDetailTitleLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftRequestDetail, 20);
            string giftRequestDetail = driver.FindElement(valGiftRequestDetail).Text;
            return giftRequestDetail;
        }
        public void CompareAndClickGiftDescWithGiftNameLV(string giftName)
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, frameGiftSubmittedL, 20);
            driver.SwitchTo().Frame(driver.FindElement(frameGiftSubmittedL));
            Thread.Sleep(1000);
            IList<IWebElement> element = driver.FindElements(GiftDescColLength);
            int totalRows = element.Count;
            for(int i = 1; i <= totalRows; i++)
            {
                By linkGiftDesc = By.CssSelector($"table[id='j_id0:theSubmitterForm:j_id34:table'] > tbody > tr:nth-child({i}) > td:nth-child(1) > a");
                IWebElement descGiftWebElement = driver.FindElement(linkGiftDesc);
                CustomFunctions.MoveToElement(driver, driver.FindElement(linkGiftDesc));
                Thread.Sleep(500);
                string descGift = descGiftWebElement.Text;
                if(descGift.Equals(giftName))
                {
                    //Console.WriteLine("Gift Description and Gift Name Matches");
                    descGiftWebElement.Click();
                    Thread.Sleep(3000);
                    break;
                }
                else
                {
                    //Console.WriteLine("Gift Description is not available in the list");
                }
            }
        }

        // Navigate to gift submitted page
        public void GoToGiftsSubmittedPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, shwAllTab, 120);
            IJavaScriptExecutor jse = (IJavaScriptExecutor) driver;
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(shwAllTab));
            //driver.FindElement(shwAllTab).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, linkGiftsSubmitted, 120);
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(linkGiftsSubmitted));
            //driver.FindElement(linkGiftsSubmitted).Click();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Thread.Sleep(3000);
        }

        public void CompareAndClickGiftDescWithGiftName(string giftName)
        {
            IList<IWebElement> element = driver.FindElements(GiftDescColLength);
            int totalRows = element.Count;

            for(int i = 1; i <= totalRows; i++)
            {
                By linkGiftDesc = By.CssSelector($"table[id='j_id0:theSubmitterForm:j_id34:table'] > tbody > tr:nth-child({i}) > td:nth-child(1) > a");
                IWebElement descGiftWebElement = driver.FindElement(linkGiftDesc);

                string descGift = descGiftWebElement.Text;
                if(descGift.Equals(giftName))
                {
                    Console.WriteLine("Gift Description and Gift Name Matches");
                    descGiftWebElement.Click();
                    Thread.Sleep(3000);
                    break;
                }
                else
                {
                    Console.WriteLine("Gift Description is not available in the list");
                }
            }
        }

        public string GetGiftRequestDetailTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftRequestDetail, 60);
            string giftRequestDetail = driver.FindElement(valGiftRequestDetail).Text;
            return giftRequestDetail;
        }

        public void ClickEditButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            IJavaScriptExecutor jse = (IJavaScriptExecutor) driver;
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnEdit));

            //  driver.FindElement(btnEdit).Click();
        }

        public bool tableListPresent()
        {
            return CustomFunctions.IsElementPresent(driver, tableList);
        }

        public string GetGiftTypeUpdatedValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftType, 60);
            string giftTypeInDetail = driver.FindElement(valGiftType).Text;
            return giftTypeInDetail;
        }

        public string GetHLRelationshipUpdatedValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valHLRelationship, 60);
            string hlRelationshipInDetail = driver.FindElement(valHLRelationship).Text;
            return hlRelationshipInDetail;
        }

        public string GetGiftValueUpdated()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftValue, 60);
            string giftValueInDetail = driver.FindElement(valGiftValue).Text.Split('(')[0].Trim();
            return giftValueInDetail;
        }

        public string GetGiftReasonUpdatedValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEditedReason, 60);
            string giftReasonInDetail = driver.FindElement(valEditedReason).Text;
            return giftReasonInDetail;
        }

        public void DeleteGiftSubmitted()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDelete, 120);
            driver.FindElement(btnDelete).Click();
            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        public void SearchByOnlyStatus(string file, string status)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            Thread.Sleep(3000);
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, comboApprovedStatus);
            driver.FindElement(comboApprovedStatus).SendKeys(status);

            WebDriverWaits.WaitUntilEleVisible(driver, btnGo);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(2000);
        }

        public bool ValidateIfGiftDescIsClickable(string giftName)
        {
            IList<IWebElement> element = driver.FindElements(GiftDescColLength);
            int totalRows = element.Count;

            bool result = false;

            for(int i = 1; i <= totalRows; i++)
            {
                By linkGiftDesc = By.CssSelector($"table[id='j_id0:theSubmitterForm:j_id34:table'] > tbody > tr:nth-child({i}) > td:nth-child(1) > span");
                IWebElement descGiftWebElement = driver.FindElement(linkGiftDesc);

                string descGift = descGiftWebElement.Text;
                if(descGift.Equals(giftName))
                {
                    try
                    {
                        WebDriverWaits.WaitUntilClickable(driver, linkGiftDesc, 5);
                    }
                    catch(Exception)
                    {
                        break;
                    }
                }
            }
            return result;
        }
    }
}