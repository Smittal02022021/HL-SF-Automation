using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages.GiftLog
{
    class GiftRequestEditPage : BaseClass
    {
        By lnkApproveGifts = By.CssSelector("a[title*='Approve Gifts']");
        By valGiftRequestEditTitle = By.CssSelector("h2[class='mainTitle']");
        By txtGiftName = By.CssSelector("input[id*='j_id48:j_id49']");
        By comboGiftType = By.CssSelector("select[id*='j_id48:j_id50']");
        By txtVendor = By.CssSelector("input[id*='j_id48:j_id54']");
        By comboCurrency = By.CssSelector("select[id*='j_id48:j_id55']");
        By comboHlRelationship = By.CssSelector("select[id*='j_id48:j_id56']");
        By txtReasonForGift = By.CssSelector("textarea[id*='j_id48:j_id57']");
        By txtGiftValue = By.CssSelector("input[id*='j_id48:j_id58']");
        By txtGiftValueAfterGiftApprove = By.CssSelector("input[id*='j_id33:j_id44']");
        By comboApproved = By.CssSelector("select[id*='j_id33:j_id46']");
        By valApproved = By.CssSelector("select[id*='j_id33:j_id46'] > option[selected = 'selected']");
        By txtApprovalComments = By.CssSelector("textarea[id*='j_id33:j_id47']");
        By btnSave = By.CssSelector("td[class='pbButton '] > input[value='Save']");
        By msgSuccess = By.CssSelector("div[id*='j_id5:j_id7']");
        By btnCancel = By.CssSelector("td[class='pbButton '] > input[value='Cancel']");
        By btnNewGiftRequest = By.CssSelector("td[class='pbButton '] > input[value='New Gift Request']");
        By txtRecipientName = By.CssSelector("span[id*='j_id48:j_id51']");
        By txtDesiredDate = By.CssSelector("input[id*='j_id48:j_id59']");
        By txtGiftValueAfterGiftApproveL = By.CssSelector("input[id*='j_id33:j_id44']");// input[id*='j_id48:j_id58']");

        public void ClickNewGiftRequestLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewGiftRequest, 20);
            driver.FindElement(btnNewGiftRequest).Click();
            Thread.Sleep(5000);
        }

        public string GetGiftSelectedViewLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valApproved, 10);
            string giftRequestDetail = driver.FindElement(valApproved).Text;
            return giftRequestDetail;
        }
        public string IsGiftNameEditableLV()
        {
            return CustomFunctions.IsElementEditable(driver, txtGiftName);
        }

        public string IsGiftTypeEditableLV()
        {
            return CustomFunctions.IsElementEditable(driver, comboGiftType);
        }

        public string IsCurrencyEditableLV()
        {
            return CustomFunctions.IsElementEditable(driver, comboCurrency);
        }

        public string IsHLRelationshipEditableLV()
        {
            return CustomFunctions.IsElementEditable(driver, comboHlRelationship);
        }

        public string IsGiftValueAfterGiftApproveEditableLV()
        {
            return CustomFunctions.IsElementEditable(driver, txtGiftValueAfterGiftApproveL);
        }

        public string IsApporvedDropDownEditableLV()
        {
            return CustomFunctions.IsElementEditable(driver, comboApproved);
        }

        public string IsApporvalCommentsEditableLV()
        {
            return CustomFunctions.IsElementEditable(driver, txtApprovalComments);
        }

        public string IsVendorEditableLV()
        {
            return CustomFunctions.IsElementEditable(driver, txtVendor);
        }

        public string IsGiftValueEditableLV()
        {
            return CustomFunctions.IsElementEditable(driver, txtGiftValue);
        }

        public string IsReasonForGiftEditableLV()
        {
            return CustomFunctions.IsElementEditable(driver, txtReasonForGift);
        }

        public string IsDesiredDateEditableLV()
        {
            return CustomFunctions.IsElementEditable(driver, txtDesiredDate);
        }

        public void ClickCancelButtonLV()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(5000);
        }
        public string GetSuccessGiftUpdateMessageLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 20);
            string successMessage = driver.FindElement(msgSuccess).Text.Trim().Replace("\r\n", " ");
            return successMessage;
        }
        public string EnterDetailsGiftEditRequestLV(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            //Enter value of gift name
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftName);
            driver.FindElement(txtGiftName).Clear();
            string valGiftName = "ANewGiftName_"+CustomFunctions.RandomValue();
            driver.FindElement(txtGiftName).SendKeys(valGiftName);

            // Enter value in gift type
            WebDriverWaits.WaitUntilEleVisible(driver, comboGiftType);
            driver.FindElement(comboGiftType).SendKeys(ReadExcelData.ReadData(excelPath, "GiftEdit", 1));

            //Enter vendor details
            WebDriverWaits.WaitUntilEleVisible(driver, txtVendor);
            driver.FindElement(txtVendor).Clear();
            driver.FindElement(txtVendor).SendKeys(ReadExcelData.ReadData(excelPath, "GiftEdit", 2));

            //Enter currency 
            WebDriverWaits.WaitUntilEleVisible(driver, comboCurrency);
            driver.FindElement(comboCurrency).SendKeys(ReadExcelData.ReadData(excelPath, "GiftEdit", 3));

            // Enter HL Relationship
            WebDriverWaits.WaitUntilEleVisible(driver, comboHlRelationship);
            driver.FindElement(comboHlRelationship).SendKeys(ReadExcelData.ReadData(excelPath, "GiftEdit", 4));

            //Enter reason for gift
            WebDriverWaits.WaitUntilEleVisible(driver, txtReasonForGift);
            driver.FindElement(txtReasonForGift).Clear();
            driver.FindElement(txtReasonForGift).SendKeys(ReadExcelData.ReadData(excelPath, "GiftEdit", 5));
            // Enter gift value
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftValue);
            driver.FindElement(txtGiftValue).Clear();
            driver.FindElement(txtGiftValue).SendKeys(ReadExcelData.ReadData(excelPath, "GiftEdit", 6));

            //Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 120);
            driver.FindElement(btnSave).Click();
            return valGiftName;
        }
        public bool ValidateMandatoryFieldsLV()
        {
            return GiftRequestEditRequiredTag("input", "j_id0:j_id27:j_id28:j_id48:j_id49").GetAttribute("class").Contains("requiredBlock") &&
            GiftRequestEditRequiredTag("textarea", "j_id0:j_id27:j_id28:j_id48:j_id57").GetAttribute("class").Contains("requiredBlock") &&
            GiftRequestEditRequiredTagForDesireDate("input", "j_id0:j_id27:j_id28:j_id48:j_id59").GetAttribute("class").Contains("requiredBlock") &&
            GiftRequestEditRequiredTag("select", "j_id0:j_id27:j_id28:j_id48:j_id50").GetAttribute("class").Contains("requiredBlock") &&
            GiftRequestEditRequiredTag("input", "j_id0:j_id27:j_id28:j_id48:j_id54").GetAttribute("class").Contains("requiredBlock") &&
            GiftRequestEditRequiredTag("select", "j_id0:j_id27:j_id28:j_id48:j_id56").GetAttribute("class").Contains("requiredBlock") &&
            GiftRequestEditRequiredTag("input", "j_id0:j_id27:j_id28:j_id48:j_id58").GetAttribute("class").Contains("requiredBlock");
        }

        By frameEditGiftRequestL = By.XPath("(//iframe[@title='accessibility title'])[2]");
        public string GetGiftRequestEditTitleLV()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameEditGiftRequestL));
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftRequestEditTitle, 20);
            string giftRequestDetail = driver.FindElement(valGiftRequestEditTitle).Text;
            return giftRequestDetail;
        }

        public string GetGiftRequestEditTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valGiftRequestEditTitle, 60);
            string giftRequestDetail = driver.FindElement(valGiftRequestEditTitle).Text;
            return giftRequestDetail;
        }

        public IWebElement GiftRequestEditRequiredTag(string tagName, string fieldName)
        {
            return driver.FindElement(By.XPath($"//{tagName}[@id='{fieldName}']/..//div"));
        }

        public IWebElement GiftRequestEditRequiredTagForDesireDate(string tagName, string fieldName)
        {
            return driver.FindElement(By.XPath($"//{tagName}[@id='{fieldName}']/../..//div"));
        }

        // Validate mandatory fields on Gift Request Edit page
        public bool ValidateMandatoryFields()
        {
            return GiftRequestEditRequiredTag("input", "j_id0:j_id27:j_id28:j_id48:j_id49").GetAttribute("class").Contains("requiredBlock") &&
            GiftRequestEditRequiredTag("textarea", "j_id0:j_id27:j_id28:j_id48:j_id57").GetAttribute("class").Contains("requiredBlock") &&
            GiftRequestEditRequiredTagForDesireDate("input", "j_id0:j_id27:j_id28:j_id48:j_id59").GetAttribute("class").Contains("requiredBlock") &&
            GiftRequestEditRequiredTag("select", "j_id0:j_id27:j_id28:j_id48:j_id50").GetAttribute("class").Contains("requiredBlock") &&
            GiftRequestEditRequiredTag("input", "j_id0:j_id27:j_id28:j_id48:j_id54").GetAttribute("class").Contains("requiredBlock") &&
            GiftRequestEditRequiredTag("select", "j_id0:j_id27:j_id28:j_id48:j_id56").GetAttribute("class").Contains("requiredBlock") &&
            GiftRequestEditRequiredTag("input", "j_id0:j_id27:j_id28:j_id48:j_id58").GetAttribute("class").Contains("requiredBlock");
        }

        public string EnterDetailsGiftEditRequest(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;

            //Enter value of gift name
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftName);
            driver.FindElement(txtGiftName).Clear();
            string valGiftName = CustomFunctions.RandomValue();
            driver.FindElement(txtGiftName).SendKeys(valGiftName);

            // Enter value in gift type
            WebDriverWaits.WaitUntilEleVisible(driver, comboGiftType);
            driver.FindElement(comboGiftType).SendKeys(ReadExcelData.ReadData(excelPath, "GiftEdit", 1));


            //Enter vendor details
            WebDriverWaits.WaitUntilEleVisible(driver, txtVendor);
            driver.FindElement(txtVendor).Clear();
            driver.FindElement(txtVendor).SendKeys(ReadExcelData.ReadData(excelPath, "GiftEdit", 2));

            //Enter currency 
            WebDriverWaits.WaitUntilEleVisible(driver, comboCurrency);
            driver.FindElement(comboCurrency).SendKeys(ReadExcelData.ReadData(excelPath, "GiftEdit", 3));

            // Enter HL Relationship
            WebDriverWaits.WaitUntilEleVisible(driver, comboHlRelationship);
            driver.FindElement(comboHlRelationship).SendKeys(ReadExcelData.ReadData(excelPath, "GiftEdit", 4));

            //Enter reason for gift
            WebDriverWaits.WaitUntilEleVisible(driver, txtReasonForGift);
            driver.FindElement(txtReasonForGift).Clear();
            driver.FindElement(txtReasonForGift).SendKeys(ReadExcelData.ReadData(excelPath, "GiftEdit", 5));
            // Enter gift value
            WebDriverWaits.WaitUntilEleVisible(driver, txtGiftValue);
            driver.FindElement(txtGiftValue).Clear();
            driver.FindElement(txtGiftValue).SendKeys(ReadExcelData.ReadData(excelPath, "GiftEdit", 6));

            //Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 120);
            driver.FindElement(btnSave).Click();

            return valGiftName;
        }

        public string GetSuccessGiftUpdateMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 60);
            string successMessage = driver.FindElement(msgSuccess).Text;
            return successMessage;
        }

        public string GetGiftSelectedView()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valApproved, 60);
            string giftRequestDetail = driver.FindElement(valApproved).Text;
            return giftRequestDetail;
        }

        public void ClickCancelButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 120);
            driver.FindElement(btnCancel).Click();
        }

        public void ClickNewGiftRequest()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewGiftRequest, 120);
            driver.FindElement(btnNewGiftRequest).Click();
        }

        public string VerifyGiftNameEditable()
        {
            return CustomFunctions.IsElementEditable(driver, txtGiftName);
        }

        public string VerifyGiftTypeEditable()
        {
            return CustomFunctions.IsElementEditable(driver, comboGiftType);
        }

        public string VerifyCurrencyEditable()
        {
            return CustomFunctions.IsElementEditable(driver, comboCurrency);
        }

        public string VerifyHLRelationshipEditable()
        {
            return CustomFunctions.IsElementEditable(driver, comboHlRelationship);
        }

        public string VerifyGiftValueAfterGiftApproveEditable()
        {
            return CustomFunctions.IsElementEditable(driver, txtGiftValueAfterGiftApprove);
        }

        public string VerifyApporvedDropDownEditable()
        {
            return CustomFunctions.IsElementEditable(driver, comboApproved);
        }

        public string VerifyApporvalCommentsEditable()
        {
            return CustomFunctions.IsElementEditable(driver, txtApprovalComments);
        }

        public string VerifyVendorEditable()
        {
            return CustomFunctions.IsElementEditable(driver, txtVendor);
        }

        public string VerifyGiftValueEditable()
        {
            return CustomFunctions.IsElementEditable(driver, txtGiftValue);
        }

        public string VerifyReasonForGiftEditable()
        {
            return CustomFunctions.IsElementEditable(driver, txtReasonForGift);
        }

        public string VerifyDesiredDateEditable()
        {
            return CustomFunctions.IsElementEditable(driver, txtDesiredDate);
        }
    }
}