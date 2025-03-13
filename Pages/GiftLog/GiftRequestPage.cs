using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace SF_Automation.Pages.GiftLog
{
    class GiftRequestPage : BaseClass
    {
        
        By shwAllTab = By.CssSelector("li[id='AllTab_Tab'] > a > img");
        By linkGiftRequests = By.CssSelector("td[class*='dataCol Custom40Block'] > a > img[title='Gift Requests']");
        By valGiftRequestsTitle = By.CssSelector("h2[class='mainTitle']");
        By comboGiftType = By.CssSelector("select[id*='txtGiftType']");
        By txtGiftName = By.CssSelector("input[id*='txtGiftName']");
        By txtGiftValue = By.CssSelector("input[id*='txtGiftValue']");
        By comboHlRelationship = By.CssSelector("select[id*='txtHlRelationship']");
        By comboCurrency = By.CssSelector("select[id*='ddlCurrency']");
        By txtVendor = By.CssSelector("input[id*='txtVendor']");
        By txtReasonForGift = By.CssSelector("textarea[id*='txtReason']");
        By txtCompanyName = By.CssSelector("input[id*='searchTextAccount']");
        By txtContactName = By.CssSelector("input[id*='searchTextContact']");
        By btnSearch = By.CssSelector("input[name*='thePage:theForm:thePageBlock'][value='Search']");
        By valAvailableRecipientName = By.CssSelector("tbody[id*='j_id50:j_id60:table:tb']>tr:nth-child(1)>td:nth-child(2)>span");
        By valRecipientCompanyName = By.CssSelector("tbody[id*='j_id50:j_id60:table:tb']>tr:nth-child(1)>td:nth-child(3)>span");
        By chkBoxOfAvailableRecipient = By.CssSelector("input[name*='table:0:j_id62']");
        By btnAddRecipients = By.CssSelector("input[name*='j_id91:j_id93']");
        //  By valSelectedRecipientName = By.CssSelector("span[id*='table2:0:j_id165']");
        By valSelectedRecipientName = By.CssSelector("tbody[id*='j_id50:table2:tb']>tr>td:nth-child(2)>span");
        By valSelectedCompanyName = By.CssSelector("tbody[id*='j_id50:table2:tb']>tr>td:nth-child(3)>span");
        // By valSelectedCompanyName = By.CssSelector("span[id*='table2:0:j_id166']");
        By chkBoxOfSelectedRecipient = By.CssSelector("input[name*='table2:0:j_id77']");
        //By btnRemoveRecipients = By.CssSelector("input[name*='j_id93:j_id94']");
        By selectedRecipients = By.CssSelector("table[id*='j_id49:table2'] > tbody > tr");
        By btnCancel = By.CssSelector("td[class='pbButtonb '] > input[value='Cancel']");
        By btnSubmitGiftRequest = By.CssSelector("td[class='pbButtonb '] > input[value='Submit Gift Request']");
        By valCongratulationMsg = By.CssSelector("form[id='j_id0:j_id2'] > h1");
        By valRecipientNameGiftDetail = By.CssSelector("span[id*='table:0:j_id19']");
        By valGiftDescription = By.CssSelector("span[id*='table:0:j_id18']");
        By btnReturnToPreApprovalPage = By.CssSelector("input[name*='j_id4:j_id17']");
        By linkGiftLog = By.XPath("//a[normalize-space()='Gift Log']");
        By drpdwnArrow = By.CssSelector("div[id='tsid-arrow']");
        By linkHLForce = By.XPath("//a[normalize-space()='HL Force']");
        By valDrpDwnValue = By.CssSelector("span[id='tsidLabel']");
        By txtSubmittedFor = By.CssSelector("span[class='lookupInput'] >input[id*='txtSubmittedFor']");
        By labelNewGiftAmtYTD = By.CssSelector("div[id*='j_id82header:sortDiv']");
        By valNewGiftAmtYTD = By.CssSelector("td[id*='table2:0:j_id82']");
        By valNewGiftTotalNextYear = By.CssSelector("td[id*='table2:0:j_id86']");
        By valWarningMsgFirstLine = By.CssSelector("form[id='j_id0:j_id2'] > span:nth-child(3)");
        By valWarningMsgNextLine = By.CssSelector("form[id='j_id0:j_id2'] > span:nth-child(5)");
        By btnReviseRequest = By.CssSelector("td[class='pbButton '] > input[value='Revise Request']");
        By btnSubmitRequest = By.CssSelector("td[class='pbButton '] > input[value='Submit Request']");
        By valErrorMsgOnlyOneRecipient = By.CssSelector("div[id*='j_id6:j_id8']");
        By txtDesireDate = By.CssSelector("input[id*='txtDesiredDate']");
        By linkDesireDate = By.XPath("//span[@class='dateFormat']/a");
        By valErrorMsgDesireDate = By.CssSelector("td[class='messageCell'] > div");
        By chkDivideGiftValue = By.CssSelector("input[id*='isDistributed']");
        By chkFirstSelectedRecipient = By.CssSelector("input[name*='0:j_id62']");
        By btnRemoveRecipients = By.CssSelector("input[value*='Remove Recipients']");
        By btnRefresh = By.CssSelector("input[value='Refresh']");
        By clickLookupIcon = By.CssSelector("img.lookupIcon");
        By txtMsgFrame = By.CssSelector("div.messageText");
        By radioBtnName = By.CssSelector("input[value=SEARCH_NAME]");
        By radioBtnAllFields = By.CssSelector("input[value=SEARCH_ALL]");
        By txtSearchBox = By.CssSelector("input#lksrch");
        By btnGo = By.CssSelector("input[name=go]");
        By txtSearchResults = By.CssSelector("#Contact > div:nth-of-type(2) > div > div:nth-of-type(1) > table > tbody > tr:nth-of-type(1) > td:nth-of-type(1) > h3 > span");
        By searchFrame = By.CssSelector("frame#searchFrame");
        By resultFrame = By.CssSelector("frame#resultsFrame");
        By srchHoulihanEmployeeResult = By.CssSelector("tr.dataRow.even.first > th>a:nth-of-type(1)");
        By titleResult = By.CssSelector("tr.dataRow.even.first > td:nth-of-type(2)");
        By deptResult = By.CssSelector("tr.dataRow.even.first > td:nth-of-type(3)");
        //  By SrchCmpnyResults = By.XPath("//tr[@class='dataRow even  first']/td[3]/span");
        By selectCompanyName = By.CssSelector("td[class='data2Col  first '] > span > select");
        By selectContactName = By.CssSelector("td[class='data2Col '] > span > select");
        By helpTxtGiftType = By.CssSelector("div.pbBody > div:nth-of-type(1) > div:nth-of-type(2) > table > tbody > tr:nth-of-type(1) > th:nth-of-type(1) > span > script");
        By helpTxtSubmittedFor = By.CssSelector("div.pbBody > div:nth-of-type(1) > div:nth-of-type(2) > table > tbody > tr:nth-of-type(1) > th:nth-of-type(2) > span > script");
        By helpTxtGiftValue = By.CssSelector("div.pbBody > div:nth-of-type(2) > div:nth-of-type(2) > table > tbody > tr:nth-of-type(2) > th:nth-of-type(1) > span > script");
        By helpTxtDesiredDate = By.CssSelector("div.pbBody > div:nth-of-type(2) > div:nth-of-type(2) > table > tbody > tr:nth-of-type(2) > th:nth-of-type(2) > span > script");
        By helpTxtCurrency = By.CssSelector("div.pbBody > div:nth-of-type(2) > div:nth-of-type(2) > table > tbody > tr:nth-of-type(3) > th:nth-of-type(1) > span > script");
        By helpTxtVendor = By.CssSelector("div.pbBody > div:nth-of-type(2) > div:nth-of-type(2) > table > tbody > tr:nth-of-type(3) > th:nth-of-type(2) > span > script");
        By txtCurrentGiftAmtYTD = By.CssSelector("td[id*='table:0:j_id67']");
        By txtCurrentNextYearGiftAmt = By.CssSelector("td[id*='table:0:j_id71']");
        By selectCurrencyDrpDown = By.CssSelector("select[id*='j_id28:ddlCurrency']");
        By linkGiftRequestTab = By.XPath("//a[text()='Gift Requests']");

        ///LightningView

        By btnSubmitGiftRequestL = By.XPath("");
        By frameGiftRequestL = By.XPath("//iframe[@title='accessibility title']");
        By frameGiftRequestL2= By.XPath("(//iframe[@title='accessibility title'])[2]");
        By txtGiftTypeErrorL = By.XPath("//label[text()='Gift Type']/ancestor::tbody//td//div[@class='errorMsg']");
        By txtGiftNameErrorL = By.XPath("//h3[text()='Gift Details']/../..//tr[1]//td[1]//div[@class='errorMsg']");
        By txtHLRelationshipL = By.XPath("//h3[text()='Gift Details']/../..//tr[1]//td[2]//div[@class='errorMsg']");
        By txtGiftValueL= By.XPath("//h3[text()='Gift Details']/../..//tr[2]//td[1]//div[@class='errorMsg']");
        By txtVendorL = By.XPath("//h3[text()='Gift Details']/../..//tr[3]//td[2]//div[@class='errorMsg']");

        By listHaderErrors = By.CssSelector("div[class*='message errorM3']>table>tbody>tr:nth-child(2)>td>span>ul>li");

        By elmToolTipL = By.XPath("//div[@class='helpText']");

        private By _iconHelp(string label)
        {
            return By.XPath($"//label[text()='{label}']/../img");
        }

        public void SetDesiredDateToCurrentDateLV()
        {
            driver.FindElement(linkDesireDate).Click();
            Thread.Sleep(2000);
        }
        public string GetDollarValueTotalNextYearLV(int num)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"td[id*='{num}:j_id85']"), 60);
            string dollarValue = driver.FindElement(By.CssSelector($"td[id*='{num}:j_id85']")).Text.Split(' ')[1].Trim();
            return dollarValue;
        }

        public void RemoveSelectedRecipientLV()
        {
            driver.FindElement(chkFirstSelectedRecipient).Click();
            driver.FindElement(btnRemoveRecipients).Click();
        }
        public string GetDollarValueLV(int num)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"td[id*='{num}:j_id81']"), 60);
            string dollarValue = driver.FindElement(By.CssSelector($"td[id*='{num}:j_id81']")).Text.Split(' ')[1].Trim();
            return dollarValue;
        }
        public int GetSizeOfSelectedRecipientLV()
        {
            IList<IWebElement> selectedRecipient = driver.FindElements(By.CssSelector("tbody[id*='j_id49:table2:tb'] > tr"));
            return selectedRecipient.Count;
        }
        public void AddMultipleRecipientToSelectedRecipientsLV(int num)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"input[name*='{num}:j_id61']"), 60);
            driver.FindElement(By.CssSelector($"input[name*='{num}:j_id61']")).Click();
        }

        public int GetSizeOfAvailableRecipientLV()
        {
            IList<IWebElement> availableRecipient = driver.FindElements(By.CssSelector("tbody[id*='j_id59:table:tb'] > tr"));
            return availableRecipient.Count;
        }
        public string EnterGiftRequestDetailsLV(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;
            // Enter value in gift type
            WebDriverWaits.WaitUntilEleVisible(driver, comboGiftType);
            driver.FindElement(comboGiftType).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 1));

            WebDriverWaits.WaitUntilEleVisible(driver, txtSubmittedFor);
            driver.FindElement(txtSubmittedFor).Clear();
            driver.FindElement(txtSubmittedFor).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 2));

            //Enter value of gift name
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftName);
            driver.FindElement(txtGiftName).Clear();
            string valGiftName = CustomFunctions.RandomValue();
            driver.FindElement(txtGiftName).SendKeys(valGiftName);

            // Enter gift value
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftValue);
            driver.FindElement(txtGiftValue).Clear();
            driver.FindElement(txtGiftValue).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 3));

            // Enter HL Relationship
            WebDriverWaits.WaitUntilEleVisible(driver, comboHlRelationship);
            driver.FindElement(comboHlRelationship).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 4));

            WebDriverWaits.WaitUntilEleVisible(driver, comboCurrency);
            driver.FindElement(comboCurrency).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 5));

            //Enter vendor details
            WebDriverWaits.WaitUntilEleVisible(driver, txtVendor);
            driver.FindElement(txtVendor).Clear();
            driver.FindElement(txtVendor).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 6));

            //Click Divide Gift Value
            driver.FindElement(chkDivideGiftValue).Click();
            //Enter reason for gift
            WebDriverWaits.WaitUntilEleVisible(driver, txtReasonForGift);
            driver.FindElement(txtReasonForGift).Clear();
            driver.FindElement(txtReasonForGift).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 7));

            //Enter company name
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).Clear();
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 8));


            //Click search button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 120);
            driver.FindElement(btnSearch).Click();

            return valGiftName;
        }
        public string GetEditPageNewGiftRequestPageTitleLV()
        {
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, frameGiftRequestL2, 60);
            driver.SwitchTo().Frame(driver.FindElement(frameGiftRequestL2));
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftRequestsTitle, 60);
            string selectedCompanyTypeSelected = driver.FindElement(valGiftRequestsTitle).Text;
            return selectedCompanyTypeSelected;
        }
        public string GetGiftValueInGiftTotalNextYearLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNewGiftTotalNextYear, 20);
            string valueNewGiftTotalNextYear = driver.FindElement(valNewGiftTotalNextYear).Text.Split(' ')[1].Trim();
            return valueNewGiftTotalNextYear;
        }
        public string GetCurrentNextYearGiftAmtLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCurrentNextYearGiftAmt, 60);
            string CurrentNextYearGiftAmtText = driver.FindElement(txtCurrentNextYearGiftAmt).Text.Split(' ')[1].Trim();
            return CurrentNextYearGiftAmtText;
        }
        public string GetCurrentGiftAmtYTDLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCurrentGiftAmtYTD, 60);
            string CurrentGiftAmtYTDText = driver.FindElement(txtCurrentGiftAmtYTD).Text.Split(' ')[1].Trim(); ;
            return CurrentGiftAmtYTDText;
        }
        public bool IsReturnToPreApprovalPageVisibleLV()
        {
            return CustomFunctions.IsElementPresent(driver, btnReturnToPreApprovalPage);
        }
        public void SelectCurrencyDrpDownLV(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, selectCurrencyDrpDown, 20);
            CustomFunctions.SelectByText(driver, driver.FindElement(selectCurrencyDrpDown), value);
        }
        public void ClickSubmitRequestButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitRequest, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSubmitRequest));
            driver.FindElement(btnSubmitRequest).Click();
            Thread.Sleep(5000);
        }
        public void ClickAddRecipientLV()
        {
            //Click add recipients button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddRecipients, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnAddRecipients));
            driver.FindElement(btnAddRecipients).Click();
        }
        public void EnterGiftValueLV(string value)
        {          
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftValue, 20);
            driver.FindElement(txtGiftValue).Clear();
            driver.FindElement(txtGiftValue).SendKeys(value);
        }
        public bool IsSubmitGiftRequestButtonVisibleLV()
        {
            return CustomFunctions.IsElementPresent(driver, btnSubmitGiftRequest);
        }
        public void ClickReviseRequestButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReviseRequest, 20);
            driver.FindElement(btnReviseRequest).Click();
        }
        public bool IsSubmitRequestButtonVisibleLV()
        {
            return CustomFunctions.IsElementPresent(driver, btnSubmitRequest);
        }
        public bool IsReviseRequestButtonVisibleLV()
        {
            return CustomFunctions.IsElementPresent(driver, btnReviseRequest);
        }
        public string GetWarningMessageOnAmountLimitExceedLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valWarningMsgFirstLine, 30);
            string titleWarningMsg = driver.FindElement(valWarningMsgFirstLine).Text;

            WebDriverWaits.WaitUntilEleVisible(driver, valWarningMsgNextLine, 10);
            string textWarningMsg = driver.FindElement(valWarningMsgNextLine).Text;
            string textCompleteWarningMsg = titleWarningMsg + textWarningMsg;
            return textCompleteWarningMsg;
        }
        public string GetGiftValueColorInGiftAmtYTDLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNewGiftAmtYTD, 20);
            string colorOfValueNewGiftAmtYTD = driver.FindElement(valNewGiftAmtYTD).GetCssValue("color");
            if (colorOfValueNewGiftAmtYTD.Equals("rgba(255, 0, 0, 1)"))
            {
                return "Red";
            }
            if (colorOfValueNewGiftAmtYTD.Equals("rgba(0, 0, 0, 1)"))
            {
                return "Red";
            }
            else
            {
                return colorOfValueNewGiftAmtYTD;
            }
        }
        public string GetGiftCurrencyCodeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNewGiftAmtYTD, 20);
            string currencyNewGiftAmtYTD = driver.FindElement(valNewGiftAmtYTD).Text.Split(' ')[0].Trim();
            return currencyNewGiftAmtYTD;
        }
        public string GetGiftValueInGiftAmtYTDLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNewGiftAmtYTD, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valNewGiftAmtYTD));
            string valueNewGiftAmtYTD = driver.FindElement(valNewGiftAmtYTD).Text.Split(' ')[1].Trim();
            return valueNewGiftAmtYTD;
        }
        public string GetLabelNewGiftAmtYTDLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, labelNewGiftAmtYTD, 20);
            string lblNewGiftAmtYTD = driver.FindElement(labelNewGiftAmtYTD).Text;
            return lblNewGiftAmtYTD;
        }
        public string GetDesiredDateErrorMsgLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valErrorMsgDesireDate, 20);
            string ErrorMsgDesireDate = driver.FindElement(valErrorMsgDesireDate).Text.Replace("\r\n", " ").Trim();
            return ErrorMsgDesireDate;
        }
        public string EnterDesiredDateLV(int Days)
        {
            string getDate = DateTime.Today.AddDays(Days).ToString("dd/MM/yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtDesireDate);
            driver.FindElement(txtDesireDate).Clear();
            string newDate = getDate.Replace('-', '/');
            driver.FindElement(txtDesireDate).SendKeys(newDate);

            return newDate;
        }
        public string GetGiftDescriptionOnGiftRequestDetailLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftDescription, 20);
            string giftDescGiftDetail = driver.FindElement(valGiftDescription).Text;
            return giftDescGiftDetail;
        }
        public void ClickReturnToPreApprovalPageLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToPreApprovalPage, 20);
            driver.FindElement(btnReturnToPreApprovalPage).Click();
        }
        public string GetGiftRequestPageTitleAfterReturnToPreApprovalPageLV()
        {
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, frameGiftRequestL2, 20);
            driver.SwitchTo().Frame(driver.FindElement(frameGiftRequestL2));
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftRequestsTitle, 20);
            string selectedCompanyTypeSelected = driver.FindElement(valGiftRequestsTitle).Text;
            driver.SwitchTo().DefaultContent();
            return selectedCompanyTypeSelected;
        }

        public string GetRecipientNameOnGiftRequestDetailLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRecipientNameGiftDetail, 20);

            string RecipientNameGiftDetail = driver.FindElement(valRecipientNameGiftDetail).Text;
            return RecipientNameGiftDetail;
        }
        public void AddRecipientToSelectedRecipientsLV()
        {
            //Click check box
            WebDriverWaits.WaitUntilEleVisible(driver, chkBoxOfAvailableRecipient, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(chkBoxOfAvailableRecipient));
            Thread.Sleep(2000);
            driver.FindElement(chkBoxOfAvailableRecipient).Click();

            //Click add recipients button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddRecipients, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnAddRecipients));
            Thread.Sleep(2000);
            driver.FindElement(btnAddRecipients).Click();
        }
        public string GetSelectedRecipientNameLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSelectedRecipientName, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valSelectedRecipientName));
            string RecipientCompanyName = driver.FindElement(valSelectedRecipientName).Text;
            return RecipientCompanyName;
        }
        public string GetSelectedCompanyNameLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSelectedCompanyName, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valSelectedCompanyName));
            string RecipientCompanyName = driver.FindElement(valSelectedCompanyName).Text;
            return RecipientCompanyName;
        }
        public void RemoveRecipientFromSelectedRecipientsLV()
        {
            //Click check box
            WebDriverWaits.WaitUntilEleVisible(driver, chkBoxOfSelectedRecipient, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(chkBoxOfSelectedRecipient));
            driver.FindElement(chkBoxOfSelectedRecipient).Click();

            //Click add recipients button
            WebDriverWaits.WaitUntilEleVisible(driver, btnRemoveRecipients, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnRemoveRecipients));
            driver.FindElement(btnRemoveRecipients).Click();
        }
        public bool IsSelectedRecipientDisplayedLV()
        {
            return CustomFunctions.IsElementPresent(driver, selectedRecipients);
        }
        //Verify Company Name Combo Box
        public void SearchWithCompnyNameComboBoxLV(string value, string coompanyname)
        {
            CustomFunctions.SelectByText(driver, driver.FindElement(selectCompanyName), value);
            //Enter company name
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).Clear();
            driver.FindElement(txtCompanyName).SendKeys(coompanyname);
            //Click search button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 20);
            driver.FindElement(btnSearch).Click();
            CustomFunctions.SelectByIndex(driver, driver.FindElement(selectCompanyName), 0);
        }
        public string GetAvailableRecipientCompanyLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRecipientCompanyName, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valRecipientCompanyName));
            string RecipientCompanyName = driver.FindElement(valRecipientCompanyName).Text;
            return RecipientCompanyName;
        }
        //Verify Contact Name Combo Box
        public void SearchWithContactNameComboBoxLV(string value, string contactname)
        {
            CustomFunctions.SelectByText(driver, driver.FindElement(selectContactName), value);
            //Enter contact name
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactName);
            driver.FindElement(txtContactName).Clear();
            driver.FindElement(txtContactName).SendKeys(contactname);
            //Click search button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 20);
            driver.FindElement(btnSearch).Click();
        }
        public string GetAvailableRecipientNameLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAvailableRecipientName, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valAvailableRecipientName));
            string AvailableRecipientName = driver.FindElement(valAvailableRecipientName).Text;
            return AvailableRecipientName;
        }
        public void SearchWithCompanyContactNameComboBoxLV(string value, string val, string companyname, string contactname)
        {
            CustomFunctions.SelectByText(driver, driver.FindElement(selectContactName), value);
            CustomFunctions.SelectByText(driver, driver.FindElement(selectCompanyName), val);
            //Enter company name
            driver.FindElement(txtCompanyName).SendKeys(companyname);
            //Enter contact name
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactName);
            driver.FindElement(txtContactName).Clear();
            driver.FindElement(txtContactName).SendKeys(contactname);
            //Click search button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 120);
            driver.FindElement(btnSearch).Click();
            CustomFunctions.SelectByIndex(driver, driver.FindElement(selectContactName), 0);
        }
        public void ClearGiftRecipientsDetailsLV()
        {
            //Clear company name
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).Clear();
            Thread.Sleep(1000);
            //Clear contact name
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactName);
            driver.FindElement(txtContactName).Clear();
            Thread.Sleep(1000);
        }
        public bool ValidateFieldsUnderGiftBillingDetailsSectionLV()
        {            
            if (driver.FindElement(comboGiftType).Displayed && driver.FindElement(txtSubmittedFor).Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ValidateFieldsUnderGiftDetailsSectionLV()
        {
            if (driver.FindElement(txtGiftName).Displayed && driver.FindElement(txtGiftValue).Displayed && driver.FindElement(comboHlRelationship).Displayed && driver.FindElement(comboCurrency).Displayed && driver.FindElement(txtVendor).Displayed && driver.FindElement(txtReasonForGift).Displayed && driver.FindElement(txtDesireDate).Displayed && driver.FindElement(chkDivideGiftValue).Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateFieldsUnderGiftRecipientsSectionLV()
        {
            if (driver.FindElement(txtCompanyName).Displayed && driver.FindElement(txtContactName).Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ClickCancelButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnCancel));
            driver.FindElement(btnCancel).Click();
        }
        public string GetCongratulationsMsgLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCongratulationMsg, 60);
            string CongratulationMsg = driver.FindElement(valCongratulationMsg).GetAttribute("innerText").Trim();
            return CongratulationMsg;
        }
        public string SearchWithTitleLV(string title)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(searchFrame));
            driver.FindElement(radioBtnAllFields).Click();
            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys(title);
            driver.FindElement(btnGo).Click();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, resultFrame, 20);
            driver.SwitchTo().Frame(driver.FindElement(resultFrame));
            string txt = driver.FindElement(titleResult).Text.Trim();
            driver.SwitchTo().DefaultContent();
            return txt;
        }

        public string SearchWithDeptLV(string dept)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(searchFrame));
            driver.FindElement(radioBtnAllFields).Click();
            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys(dept);
            driver.FindElement(btnGo).Click();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, resultFrame, 20);
            driver.SwitchTo().Frame(driver.FindElement(resultFrame));
            string txt = driver.FindElement(deptResult).Text.Trim();
            driver.SwitchTo().DefaultContent();
            driver.Close();
            CustomFunctions.SwitchToWindow(driver, 0);

            return txt;

        }
        public string SearchHLEmployeeLV(string name)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(searchFrame));
            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys(name);
            driver.FindElement(btnGo).Click();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, resultFrame, 20);
            driver.SwitchTo().Frame(driver.FindElement(resultFrame));
            string txt = driver.FindElement(srchHoulihanEmployeeResult).Text.Trim();
            driver.SwitchTo().DefaultContent();
            return txt;

        }
        public string SearchExternalContctLV(string name)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(searchFrame));
            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys(name);
            driver.FindElement(btnGo).Click();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, resultFrame, 20);
            driver.SwitchTo().Frame(driver.FindElement(resultFrame));
            string txt = driver.FindElement(txtSearchResults).Text.Trim();
            driver.SwitchTo().DefaultContent();
            if (txt == "Contacts [0]")
                return "No records were found based on your criteria";
            else 
                return txt;            
        }
        public bool VerifyRadioBtnNameLV()
        {
            driver.SwitchTo().Frame(driver.FindElement(searchFrame));
            string str = driver.FindElement(radioBtnName).GetAttribute("checked");
            driver.SwitchTo().DefaultContent();
            if (str.Equals("true"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string GetLookupSubittedForHeaderLV()
        {
            driver.FindElement(clickLookupIcon).Click();
            CustomFunctions.SwitchToWindow(driver, 1);
            driver.SwitchTo().Frame(driver.FindElement(searchFrame));
            string textHeader = driver.FindElement(txtMsgFrame).Text.Replace("\r\n", " ").Trim();
            driver.SwitchTo().DefaultContent();
            return textHeader;
        }
        public string GetGiftRequestPageTitleLV()
        {
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, frameGiftRequestL, 60);
            driver.SwitchTo().Frame(driver.FindElement(frameGiftRequestL));
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftRequestsTitle, 20);
            string selectedCompanyTypeSelected = driver.FindElement(valGiftRequestsTitle).Text;
            return selectedCompanyTypeSelected;
        }
        public string GetHelpFieldTextLV(string label)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _iconHelp(label), 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(_iconHelp(label)));
            Thread.Sleep(1000);
            WebDriverWaits.WaitUntilEleVisible(driver, elmToolTipL, 15);
            return driver.FindElement(elmToolTipL).Text.Trim();
        }               

        public string GetRecipientErrorMessageLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valErrorMsgOnlyOneRecipient, 20);
            string ErrorMsgOnlyOneRecipient = driver.FindElement(valErrorMsgOnlyOneRecipient).Text.Trim().Replace("\r\n", " ");
            return ErrorMsgOnlyOneRecipient;
        }
        public string EnterDetailsGiftRequestLV(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            // Enter value in gift type
            WebDriverWaits.WaitUntilEleVisible(driver, comboGiftType);
            driver.FindElement(comboGiftType).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 1));

            WebDriverWaits.WaitUntilEleVisible(driver, txtSubmittedFor);
            driver.FindElement(txtSubmittedFor).Clear();
            driver.FindElement(txtSubmittedFor).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 2));

            //Enter value of gift name
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftName);
            driver.FindElement(txtGiftName).Clear();
            string valGiftName = "GiftName_"+CustomFunctions.RandomValue();
            driver.FindElement(txtGiftName).SendKeys(valGiftName);

            // Enter gift value
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftValue);
            driver.FindElement(txtGiftValue).Clear();
            driver.FindElement(txtGiftValue).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 3));

            // Enter HL Relationship
            WebDriverWaits.WaitUntilEleVisible(driver, comboHlRelationship);
            driver.FindElement(comboHlRelationship).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 4));

            WebDriverWaits.WaitUntilEleVisible(driver, comboCurrency);
            driver.FindElement(comboCurrency).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 5));

            //Enter vendor details
            WebDriverWaits.WaitUntilEleVisible(driver, txtVendor);
            driver.FindElement(txtVendor).Clear();
            driver.FindElement(txtVendor).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 6));

            //Enter reason for gift
            WebDriverWaits.WaitUntilEleVisible(driver, txtReasonForGift);
            driver.FindElement(txtReasonForGift).Clear();
            driver.FindElement(txtReasonForGift).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 7));

            //Enter company name
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSearch));
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).Clear();
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 8));

            //Enter contact name
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactName);
            driver.FindElement(txtContactName).Clear();
            driver.FindElement(txtContactName).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 9));

            //Click search button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 10);
            driver.FindElement(btnSearch).Click();
            return valGiftName;
        }
        public string AreRequiredFieldsValidationDisplayedLV(string file)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            //driver.FindElements(listHaderErrors);
            IList<IWebElement> fieldErrorsList = driver.FindElements(listHaderErrors);
            bool errorFound = false;
            int rowOpp = ReadExcelData.GetRowCount(excelPath, "GiftLogErrors");
            foreach (IWebElement txtFieldError in fieldErrorsList)
            {
                errorFound = false;
                string ActualError = txtFieldError.Text.Trim();
                for (int row = 2; row <= rowOpp; row++)
                {                    
                    string valErrorExl = ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLogErrors", row, 1);
                    if(ActualError== valErrorExl)
                    {
                        errorFound = true;
                        break;
                    }
                }
            }
            if (errorFound)
                return "All errors for required fields are correct";
            else
                return "Required fields errors are not correct";
        }

        public void ClickSubmitGiftRequestLV()
        {   
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSubmitGiftRequest));
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitGiftRequest, 20);
            driver.FindElement(btnSubmitGiftRequest).Click();
            Thread.Sleep(2000);
        }
        public string GetGiftTypeFieldValidationLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftTypeErrorL, 20);
            return driver.FindElement(txtGiftTypeErrorL).Text.Replace("\r\n", " ");
        }
        public string GetGiftNameFieldValidationLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftNameErrorL, 20);
            return driver.FindElement(txtGiftNameErrorL).Text.Replace("\r\n", " ");
        }
        public string GetHLRelationshipFieldValidationLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtHLRelationshipL, 20);
            return driver.FindElement(txtHLRelationshipL).Text.Replace("\r\n", " ");
        }

        public string GetGiftValueLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftValueL, 20);
            return driver.FindElement(txtGiftValueL).Text.Replace("\r\n", " ");
        }
        public string GetVendorLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtVendorL, 20);
            return driver.FindElement(txtVendorL).Text.Replace("\r\n", " ");
        }

        public void GoToGiftRequestPage()
        {

            string valueOfDropDwn = driver.FindElement(valDrpDwnValue).Text;
            if (valueOfDropDwn.Equals("HL Force"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, drpdwnArrow, 120);
                driver.FindElement(drpdwnArrow).Click();

                WebDriverWaits.WaitUntilEleVisible(driver, linkGiftLog, 120);
                driver.FindElement(linkGiftLog).Click();
            }
        }

        public void GoToGiftRequestTab()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, linkGiftRequestTab, 120);
            driver.FindElement(linkGiftRequestTab).Click();

            Thread.Sleep(2000);
        }

        public void SwitchFromGiftLogToHLForce()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, drpdwnArrow, 120);
            driver.FindElement(drpdwnArrow).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, linkHLForce, 120);
            driver.FindElement(linkHLForce).Click();
        }
        // Navigate to gift request page
        public void GoToGiftRequestsPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, shwAllTab, 120);
            driver.FindElement(shwAllTab).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, linkGiftRequests, 120);
            driver.FindElement(linkGiftRequests).Click();            
        }

        public string GetGiftRequestPageTitle()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftRequestsTitle, 60);
            string selectedCompanyTypeSelected = driver.FindElement(valGiftRequestsTitle).Text;
            return selectedCompanyTypeSelected;
        }

        public bool ValidateFieldsUnderGiftBillingDetailsSection()
        {
            if (driver.FindElement(comboGiftType).Displayed && driver.FindElement(txtSubmittedFor).Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ValidateFieldsUnderGiftDetailsSection()
        {
            if (driver.FindElement(txtGiftName).Displayed && driver.FindElement(txtGiftValue).Displayed && driver.FindElement(comboHlRelationship).Displayed && driver.FindElement(comboCurrency).Displayed && driver.FindElement(txtVendor).Displayed && driver.FindElement(txtReasonForGift).Displayed && driver.FindElement(txtDesireDate).Displayed && driver.FindElement(chkDivideGiftValue).Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateFieldsUnderGiftRecipientsSection()
        {
            if (driver.FindElement(txtCompanyName).Displayed && driver.FindElement(txtContactName).Displayed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string EnterDetailsGiftRequest(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;


            string excelPath = dir + file;
            // Enter value in gift type
            WebDriverWaits.WaitUntilEleVisible(driver, comboGiftType);
            driver.FindElement(comboGiftType).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 1));

            WebDriverWaits.WaitUntilEleVisible(driver, txtSubmittedFor);
            driver.FindElement(txtSubmittedFor).Clear();
            driver.FindElement(txtSubmittedFor).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 2));

            //Enter value of gift name
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftName);
            driver.FindElement(txtGiftName).Clear();
            string valGiftName = CustomFunctions.RandomValue();
            driver.FindElement(txtGiftName).SendKeys(valGiftName);

            // Enter gift value
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftValue);
            driver.FindElement(txtGiftValue).Clear();
            driver.FindElement(txtGiftValue).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 3));

            // Enter HL Relationship
            WebDriverWaits.WaitUntilEleVisible(driver, comboHlRelationship);
            driver.FindElement(comboHlRelationship).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 4));

            WebDriverWaits.WaitUntilEleVisible(driver, comboCurrency);
            driver.FindElement(comboCurrency).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 5));

            //Enter vendor details
            WebDriverWaits.WaitUntilEleVisible(driver, txtVendor);
            driver.FindElement(txtVendor).Clear();
            driver.FindElement(txtVendor).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 6));

            //Enter reason for gift
            WebDriverWaits.WaitUntilEleVisible(driver, txtReasonForGift);
            driver.FindElement(txtReasonForGift).Clear();
            driver.FindElement(txtReasonForGift).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 7));

            //Enter company name
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).Clear();
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 8));

            //Enter contact name
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactName);
            driver.FindElement(txtContactName).Clear();
            driver.FindElement(txtContactName).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 9));

            //Click search button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 120);
            driver.FindElement(btnSearch).Click();

            return valGiftName;
        }

        public string EnterGiftRequestDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;
            // Enter value in gift type
            WebDriverWaits.WaitUntilEleVisible(driver, comboGiftType);
            driver.FindElement(comboGiftType).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 1));

            WebDriverWaits.WaitUntilEleVisible(driver, txtSubmittedFor);
            driver.FindElement(txtSubmittedFor).Clear();
            driver.FindElement(txtSubmittedFor).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 2));

            //Enter value of gift name
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftName);
            driver.FindElement(txtGiftName).Clear();
            string valGiftName = CustomFunctions.RandomValue();
            driver.FindElement(txtGiftName).SendKeys(valGiftName);

            // Enter gift value
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftValue);
            driver.FindElement(txtGiftValue).Clear();
            driver.FindElement(txtGiftValue).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 3));

            // Enter HL Relationship
            WebDriverWaits.WaitUntilEleVisible(driver, comboHlRelationship);
            driver.FindElement(comboHlRelationship).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 4));

            WebDriverWaits.WaitUntilEleVisible(driver, comboCurrency);
            driver.FindElement(comboCurrency).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 5));

            //Enter vendor details
            WebDriverWaits.WaitUntilEleVisible(driver, txtVendor);
            driver.FindElement(txtVendor).Clear();
            driver.FindElement(txtVendor).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 6));

            //Click Divide Gift Value
            driver.FindElement(chkDivideGiftValue).Click();
            //Enter reason for gift
            WebDriverWaits.WaitUntilEleVisible(driver, txtReasonForGift);
            driver.FindElement(txtReasonForGift).Clear();
            driver.FindElement(txtReasonForGift).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 7));

            //Enter company name
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).Clear();
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadData(excelPath, "GiftLog", 8));


            //Click search button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 120);
            driver.FindElement(btnSearch).Click();

            return valGiftName;
        }

        public void SetDesiredDateToCurrentDate()
        {
            driver.FindElement(linkDesireDate).Click();
            Thread.Sleep(2000);
        }

        public string EnterDesiredDate(int Days)
        {
            string getDate = DateTime.Today.AddDays(Days).ToString("dd/MM/yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, txtDesireDate);
            driver.FindElement(txtDesireDate).Clear();
            string newDate = getDate.Replace('-', '/');
            driver.FindElement(txtDesireDate).SendKeys(newDate);

            return newDate;
        }

        public void ClearGiftRecipientsDetails()
        {
            //Clear company name
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).Clear();

            //Clear contact name
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactName);
            driver.FindElement(txtContactName).Clear();
        }

        public void ClickRefreshButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnRefresh);
            driver.FindElement(btnRefresh).Click();
            Thread.Sleep(5000);
        }

        public string GetAvailableRecipientName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAvailableRecipientName, 60);
            string AvailableRecipientName = driver.FindElement(valAvailableRecipientName).Text;
            return AvailableRecipientName;
        }

        public string GetAvailableRecipientCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRecipientCompanyName, 3000);
            string RecipientCompanyName = driver.FindElement(valRecipientCompanyName).Text;
            return RecipientCompanyName;
        }

        public string GetErrorMessageForSelectingAlteastOneRecipient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valErrorMsgOnlyOneRecipient, 60);
            string ErrorMsgOnlyOneRecipient = driver.FindElement(valErrorMsgOnlyOneRecipient).Text;
            return ErrorMsgOnlyOneRecipient;
        }

        public void AddRecipientToSelectedRecipients()
        {
            //Click check box
            WebDriverWaits.WaitUntilEleVisible(driver, chkBoxOfAvailableRecipient, 240);
            driver.FindElement(chkBoxOfAvailableRecipient).Click();

            //Click add recipients button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddRecipients, 120);
            driver.FindElement(btnAddRecipients).Click();
        }

        public void ClickAddRecipient()
        {
            //Click add recipients button
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddRecipients, 120);
            driver.FindElement(btnAddRecipients).Click();
        }

        public int GetSizeOfAvailableRecipient()
        {
            IList<IWebElement> availableRecipient = driver.FindElements(By.CssSelector("tbody[id*='j_id59:table:tb'] > tr"));
            return availableRecipient.Count;
        }

        public int GetSizeOfSelectedRecipient()
        {
            IList<IWebElement> selectedRecipient = driver.FindElements(By.CssSelector("tbody[id*='j_id49:table2:tb'] > tr"));
            return selectedRecipient.Count;
        }

        public string GetDollarValue(int num)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"td[id*='{num}:j_id81']"), 60);
            string dollarValue = driver.FindElement(By.CssSelector($"td[id*='{num}:j_id81']")).Text.Split(' ')[1].Trim();
            return dollarValue;
        }

        public string GetDollarValueTotalNextYear(int num)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"td[id*='{num}:j_id85']"), 60);
            string dollarValue = driver.FindElement(By.CssSelector($"td[id*='{num}:j_id85']")).Text.Split(' ')[1].Trim();
            return dollarValue;
        }

        public void AddMultipleRecipientToSelectedRecipients(int num)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"input[name*='{num}:j_id61']"), 60);
            driver.FindElement(By.CssSelector($"input[name*='{num}:j_id61']")).Click();
        }

        public string GetSelectedRecipientName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSelectedRecipientName, 150);
            string RecipientCompanyName = driver.FindElement(valSelectedRecipientName).Text;
            return RecipientCompanyName;
        }

        public void RemoveSelectedRecipient()
        {
            driver.FindElement(chkFirstSelectedRecipient).Click();
            driver.FindElement(btnRemoveRecipients).Click();
        }

        public string GetSelectedCompanyName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSelectedCompanyName, 60);
            string RecipientCompanyName = driver.FindElement(valSelectedCompanyName).Text;
            return RecipientCompanyName;
        }

        public void RemoveRecipientFromSelectedRecipients()
        {
            //Click check box
            WebDriverWaits.WaitUntilEleVisible(driver, chkBoxOfSelectedRecipient, 120);
            driver.FindElement(chkBoxOfSelectedRecipient).Click();

            //Click add recipients button
            WebDriverWaits.WaitUntilEleVisible(driver, btnRemoveRecipients, 120);
            driver.FindElement(btnRemoveRecipients).Click();
        }

        public bool IsSelectedRecipientDisplayed()
        {
            return CustomFunctions.IsElementPresent(driver, selectedRecipients);
        }

        public bool IsReviseRequestButtonVisible()
        {
            return CustomFunctions.IsElementPresent(driver, btnReviseRequest);
        }

        public bool IsSubmitGiftRequestButtonVisible()
        {
            return CustomFunctions.IsElementPresent(driver, btnSubmitGiftRequest);
        }

        public bool IsReturnToPreApprovalPageVisible()
        {
            return CustomFunctions.IsElementPresent(driver, btnReturnToPreApprovalPage);
        }

        public bool IsSubmitRequestButtonVisible()
        {
            return CustomFunctions.IsElementPresent(driver, btnSubmitRequest);
        }
        public void ClickCancelButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            driver.FindElement(btnCancel).Click();
        }

        public void ClickReviseRequestButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReviseRequest, 120);
            driver.FindElement(btnReviseRequest).Click();
        }

        public void ClickSubmitRequestButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitRequest, 120);
            driver.FindElement(btnSubmitRequest).Click();
            Thread.Sleep(5000);
        }

        public void ClickSubmitGiftRequest()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitGiftRequest, 120);
            driver.FindElement(btnSubmitGiftRequest).Click();
            Thread.Sleep(2000);
        }

        public string GetCongratulationsMsg()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCongratulationMsg, 60);
            string CongratulationMsg = driver.FindElement(valCongratulationMsg).GetAttribute("innerText");
            return CongratulationMsg;
        }

        public string GetRecipientNameOnGiftRequestDetail()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRecipientNameGiftDetail, 60);

            string RecipientNameGiftDetail = driver.FindElement(valRecipientNameGiftDetail).Text;
            return RecipientNameGiftDetail;
        }

        public string GetGiftDescriptionOnGiftRequestDetail()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftDescription, 60);
            string giftDescGiftDetail = driver.FindElement(valGiftDescription).Text;
            return giftDescGiftDetail;
        }

        public void ClickReturnToPreApprovalPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToPreApprovalPage, 120);
            driver.FindElement(btnReturnToPreApprovalPage).Click();
        }

        public string GetLabelNewGiftAmtYTD()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, labelNewGiftAmtYTD, 60);
            string lblNewGiftAmtYTD = driver.FindElement(labelNewGiftAmtYTD).Text;
            return lblNewGiftAmtYTD;
        }

        public string GetGiftCurrencyCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNewGiftAmtYTD, 60);
            string currencyNewGiftAmtYTD = driver.FindElement(valNewGiftAmtYTD).Text.Split(' ')[0].Trim();
            return currencyNewGiftAmtYTD;
        }

        public string GetGiftValueInGiftAmtYTD()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNewGiftAmtYTD, 60);
            string valueNewGiftAmtYTD = driver.FindElement(valNewGiftAmtYTD).Text.Split(' ')[1].Trim();
            return valueNewGiftAmtYTD;
        }

        public string GetGiftValueInGiftTotalNextYear()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, valNewGiftTotalNextYear, 60);
            //string test = driver.FindElement(valNewGiftTotalNextYear).Text;
            string valueNewGiftTotalNextYear = driver.FindElement(valNewGiftTotalNextYear).Text.Split(' ')[1].Trim();
            return valueNewGiftTotalNextYear;
        }

        public string GetGiftValueColorInGiftAmtYTD()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNewGiftAmtYTD, 60);
            string colorOfValueNewGiftAmtYTD = driver.FindElement(valNewGiftAmtYTD).GetCssValue("color");
            if (colorOfValueNewGiftAmtYTD.Equals("rgba(255, 0, 0, 1)"))
            {
                return "Red";
            }
            else
            {
                return colorOfValueNewGiftAmtYTD;
            }
        }

        public string GetWarningMessageOnAmountLimitExceed()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valWarningMsgFirstLine, 60);
            string WarningMsg1 = driver.FindElement(valWarningMsgFirstLine).Text;

            WebDriverWaits.WaitUntilEleVisible(driver, valWarningMsgNextLine, 60);
            string WarningMsg2 = driver.FindElement(valWarningMsgNextLine).Text;
            string WarningMsg = WarningMsg1 + WarningMsg2;
            return WarningMsg;
        }
        public bool VerifyWarningMessageDisplayed()
        {
            bool result = false;
            if (driver.FindElement(valWarningMsgFirstLine).Displayed && driver.FindElement(valWarningMsgNextLine).Displayed)
            {
                result = true;
            }
            return result;
        }

        public string GetDefaultSubmittedForUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSubmittedFor, 60);
            string DefaultSubmittedForUser = driver.FindElement(txtSubmittedFor).GetAttribute("value");
            return DefaultSubmittedForUser;
        }
        public string GetDesiredDateErrorMsg()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valErrorMsgDesireDate, 60);
            string ErrorMsgDesireDate = driver.FindElement(valErrorMsgDesireDate).Text;
            return ErrorMsgDesireDate;
        }
        public IWebElement ErrorFieldsGiftTypeValueAndVendor(IWebDriver driver, string name)
        {
            return driver.FindElement(By.XPath($"//*[text()='{name}']/../../../td/div/div[@class='errorMsg']"));
        }

        public IWebElement ErrorFieldsGiftNameAndHLRelationship(IWebDriver driver, string name, int td)
        {
            return driver.FindElement(By.XPath($"//*[text()='{name}']/../../td[{td}]/div/div[@class='errorMsg']"));
        }


        //Get Text from Lookup form Submitted for Look up on Pre-Approval Page

        public string TxtfromLookupSubittedFor()
        {
            driver.FindElement(clickLookupIcon).Click();
            //   Thread.Sleep(3000);
            CustomFunctions.SwitchToWindow(driver, 1);
            driver.SwitchTo().Frame(driver.FindElement(searchFrame));

            string browserTitle = driver.Title;

            String Txt = driver.FindElement(txtMsgFrame).Text;
            return Txt;
        }
        //Verify Radion Button is checked by default
        public bool VerifyRadioBtnName()
        {
            string str = driver.FindElement(radioBtnName).GetAttribute("checked");
            Console.WriteLine(str);
            if (str.Equals("true"))
            {

                return true;
            }
            else
            {
                return false;
            }

        }


        //Verify 0 results are displayed when user searched for external contacts
        public string SrchExternalContct()
        {
            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys("test external");
            driver.FindElement(btnGo).Click();
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(resultFrame));
            string txt = driver.FindElement(txtSearchResults).Text;
            return txt;
            //    try
            //    {
            //        if ((driver.FindElement(By.XPath("//span[contains(text(),'Contacts [0]"))) != null)
            //        {


            //        }
            //        return true;
            //    }
            //    catch (Exception e)

            //    {

            //        return false;
            //    }


            //}
        }
        //Verify appropriate results are dispalyed when user searched with partial name of Houlihan Employee
        public string SrchHoulihanEmployee()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(searchFrame));

            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys("oscar");

            driver.FindElement(btnGo).Click();

            driver.SwitchTo().DefaultContent();

            WebDriverWaits.WaitUntilEleVisible(driver, resultFrame, 60);
            driver.SwitchTo().Frame(driver.FindElement(resultFrame));
            string txt = driver.FindElement(srchHoulihanEmployeeResult).Text;
            return txt;

        }

        public string SrchTitle()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(searchFrame));
            driver.FindElement(radioBtnAllFields).Click();
            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys("Managing Director");

            driver.FindElement(btnGo).Click();

            driver.SwitchTo().DefaultContent();

            WebDriverWaits.WaitUntilEleVisible(driver, resultFrame, 60);
            driver.SwitchTo().Frame(driver.FindElement(resultFrame));
            string txt = driver.FindElement(titleResult).Text;
            return txt;
        }

        public string SrchDept()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(searchFrame));
            driver.FindElement(radioBtnAllFields).Click();
            driver.FindElement(txtSearchBox).Clear();

            driver.FindElement(txtSearchBox).SendKeys("FR");

            driver.FindElement(btnGo).Click();

            driver.SwitchTo().DefaultContent();

            WebDriverWaits.WaitUntilEleVisible(driver, resultFrame, 60);
            driver.SwitchTo().Frame(driver.FindElement(resultFrame));
            string txt = driver.FindElement(deptResult).Text;
            driver.SwitchTo().DefaultContent();
            CustomFunctions.SwitchToWindow(driver, 0);

            return txt;

        }
        //Verify Company Name Combo Box
        public void VerifyCompnyNameComboBox(string file, string value, string coompanyname)
        {
            CustomFunctions.SelectByText(driver, driver.FindElement(selectCompanyName), value);

            //Enter company name

            driver.FindElement(txtCompanyName).SendKeys(coompanyname);

            //Click search button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 120);
            driver.FindElement(btnSearch).Click();

            CustomFunctions.SelectByIndex(driver, driver.FindElement(selectCompanyName), 0);

        }
        //Verify Contact Name Combo Box
        public void VerifyContactNameComboBox(string file, string value, string contactname)
        {
            CustomFunctions.SelectByText(driver, driver.FindElement(selectContactName), value);

            //Enter contact name


            driver.FindElement(txtContactName).SendKeys(contactname);

            //Click search button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 120);
            driver.FindElement(btnSearch).Click();

        }
        //Verify combination of COmpany and Contact Combo Box
        public void VerifyCompanyContactNameComboBox(string file, string value, string val, string companyname, string contactname)
        {
            CustomFunctions.SelectByText(driver, driver.FindElement(selectContactName), value);
            CustomFunctions.SelectByText(driver, driver.FindElement(selectCompanyName), val);

            //Enter company name

            driver.FindElement(txtCompanyName).SendKeys(companyname);
            //Enter contact name

            driver.FindElement(txtContactName).SendKeys(contactname);

            //Click search button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSearch, 120);
            driver.FindElement(btnSearch).Click();

            CustomFunctions.SelectByIndex(driver, driver.FindElement(selectContactName), 0);
        }

        public string VerifyHelpTextGiftType()
        {
            string atr = driver.FindElement(helpTxtGiftType).GetAttribute("innerHTML");
            string text = atr.Split('\'')[3];

            return text;

        }
        public string VerifyHelpTextSubmittedFor()
        {
            string atr = driver.FindElement(helpTxtSubmittedFor).GetAttribute("innerHTML");
            string text = atr.Split('\'')[3];

            return text;

        }

        public string VerifyHelpTextGiftValue()
        {
            string atr = driver.FindElement(helpTxtGiftValue).GetAttribute("innerHTML");
            string text = atr.Split('\'')[3];

            return text;

        }

        public string VerifyHelpTextDesiredDate()
        {
            string atr = driver.FindElement(helpTxtDesiredDate).GetAttribute("innerHTML");
            string text = atr.Split('\'')[3];

            return text;

        }

        public string VerifyHelpTextCurrency()
        {
            string atr = driver.FindElement(helpTxtCurrency).GetAttribute("innerHTML");
            string text = atr.Split('\'')[3];

            return text;

        }
        public string VerifyHelpTextVendor()
        {
            string atr = driver.FindElement(helpTxtVendor).GetAttribute("innerHTML");
            string text = atr.Split('\'')[3];

            return text;

        }
        public void EnterGiftValue(string value)
        {
            // string excelPath = dir + file;
            // Enter gift value
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftValue, 120);
            driver.FindElement(txtGiftValue).Clear();
            driver.FindElement(txtGiftValue).SendKeys(value);
        }
        public string GetCurrentGiftAmtYTD()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCurrentGiftAmtYTD, 60);
            string CurrentGiftAmtYTDText = driver.FindElement(txtCurrentGiftAmtYTD).Text.Split(' ')[1].Trim(); ;
            return CurrentGiftAmtYTDText;
        }

        public string GetCurrentNextYearGiftAmt()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCurrentNextYearGiftAmt, 60);
            string CurrentNextYearGiftAmtText = driver.FindElement(txtCurrentNextYearGiftAmt).Text.Split(' ')[1].Trim();
            return CurrentNextYearGiftAmtText;
        }

        public void SelectCurrencyDrpDown(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, selectCurrencyDrpDown, 60);
            CustomFunctions.SelectByText(driver, driver.FindElement(selectCurrencyDrpDown), value);


        }
    }
}