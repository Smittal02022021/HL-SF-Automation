using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Threading;

namespace SF_Automation.Pages.Activities
{
    class LV_ActivityDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();

        By successMsg = By.XPath("//span[text()='Record saved!']");
        By btnCancelActivity = By.XPath("//button[@title='Cancel']");
        By btnSaveActivity = By.XPath("//button[@title='Save']");
        By btnEditActivity = By.XPath("//button[@title='Edit']");
        By btnDeleteActivity = By.XPath("//button[@title='Delete']");
        By btnSendNotification = By.XPath("//button[@title='Send Notification']");
        By txtEmailId = By.XPath("//div[@class='slds-p-top_small']/input");
        By btnSendEmail = By.XPath("//button[text()='Send Email']");

        By dropdownFollowupType = By.XPath("//button[contains(@aria-label,'Follow-up Type')]");
        By txtFollowupStartDate = By.XPath("(//input[contains(@name,'startDate')])[1]");
        By txtFollowupEndDate = By.XPath("(//input[contains(@name,'endDate')])[1]");
        By btnCreateFollowUp = By.XPath("//button[text()='Create Follow-up']");
        By txtAreaFollowupDescription = By.XPath("(//textarea[contains(@name,'description')])[1]");
        By btnSaveFollowup = By.XPath("(//button[@title='Save'])[1]");

        By txtSubject = By.XPath("//input[@name='subject']");
        By txtDate = By.XPath("(//input[@name='startDateTime'])[1]");
        By drpdownIndustryGroup = By.XPath("//button[@name='industryGroup']");
        By drpdownProductType = By.XPath("//button[@name='productType']");
        By txtareaDescription = By.XPath("//textarea[@name='description']");
        By txtareaHLInternalMeetingNotes = By.XPath("//textarea[@name='hlCallNotes']");
        By txtExternalAttendee = By.XPath("//input[@placeholder='Lookup Contact...']");
        By txtHLAttendee = By.XPath("//input[@placeholder='Lookup Employees...']");
        By txtCompanyDiscussed = By.XPath("//input[@placeholder='Lookup Company...']");
        By txtOpportunitiesDiscussed = By.XPath("//input[@placeholder='Lookup Opportunities...']");
        By txtEngagementsDiscussed = By.XPath("//input[@placeholder='Lookup Engagements...']");
        By txtCampaignsDiscussed = By.XPath("//input[@placeholder='Lookup Campaigns...']");

        private By _comboDropdown(string value)
        {
            return By.XPath($"//lightning-base-combobox-item[@data-value='{value}']");
        }

        private By _btnActivityDetailPage(string btnName)
        {
            return By.XPath($"//button[text()='{btnName}']");
        }

        public string GetActivitySubjectFromList()
        {
            Thread.Sleep(5000);
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,2500)");
            Thread.Sleep(2000);

            string subjectName = driver.FindElement(By.XPath("(//td[@data-label='Subject']//a)[1]")).Text;
            return subjectName;
        }

        public void ViewActivityFromList(string name)
        {
            Thread.Sleep(5000);
            CustomFunctions.ActionClick(driver, driver.FindElement(By.XPath($"//a[@title='{name}']")), 60);
            Thread.Sleep(5000);
        }

        public void DeleteActivity()
        {
            Thread.Sleep(5000);
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteActivity, 60);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnDeleteActivity));
            driver.FindElement(btnDeleteActivity).Click();
            Thread.Sleep(2000);
        }

        public void CloseTab(string tabName)
        {
            Thread.Sleep(5000);
            driver.FindElement(By.XPath($"//button[contains(@title,'Close {tabName}')]")).Click();
            Thread.Sleep(5000);
        }

        public void CreateFolloupActivity(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string typeFollowup = ReadExcelData.ReadData(excelPath, "Followup", 1);
            string commentsFollowup = ReadExcelData.ReadData(excelPath, "Followup", 2);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,2500)");
            Thread.Sleep(2000);

            //Enter Followup  details
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnCreateFollowUp));
            driver.FindElement(btnCreateFollowUp).Click();
            Thread.Sleep(2000);

            CustomFunctions.MoveToElement(driver, driver.FindElement(dropdownFollowupType));
            driver.FindElement(dropdownFollowupType).Click();
            Thread.Sleep(2000);
            driver.FindElement(_comboDropdown(typeFollowup)).Click();
            Thread.Sleep(2000);

            DateTime currentDate1 = DateTime.Today;
            DateTime setDate1 = currentDate1.AddDays(2);

            IWebElement followupStartDate = driver.FindElement(txtFollowupStartDate);
            CustomFunctions.MoveToElement(driver, followupStartDate);
            followupStartDate.Clear();
            followupStartDate.SendKeys(setDate1.ToString("MMM dd, yyyy"));
            Thread.Sleep(2000);

            IWebElement followupEndDate = driver.FindElement(txtFollowupEndDate);
            CustomFunctions.MoveToElement(driver, followupEndDate);
            followupEndDate.Clear();
            followupEndDate.SendKeys(setDate1.ToString("MMM dd, yyyy"));
            Thread.Sleep(2000);

            driver.FindElement(txtAreaFollowupDescription).SendKeys(commentsFollowup);
            Thread.Sleep(2000);

            //Click Save
            driver.FindElement(btnSaveFollowup).Click();
            Thread.Sleep(3000);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);
        }

        public void ClickActivityDetailPageButton(string name)
        {
            CustomFunctions.MoveToElement(driver, driver.FindElement(_btnActivityDetailPage(name)));
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, _btnActivityDetailPage(name), 20);
            driver.FindElement(_btnActivityDetailPage(name)).Click();
        }

        public void ClickEditActivityButton()
        {
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditActivity));
            WebDriverWaits.WaitUntilClickable(driver, btnEditActivity, 60);
            driver.FindElement(btnEditActivity).Click();
            Thread.Sleep(2000);
        }

        public void UpdateActivityByPrimaryHLAttendee(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;

            string updatedSubject = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 1);
            string updatedIndGrp = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 2);
            string updatedPrdType = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 3);
            string updatedDesc = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 4);
            string updatedNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 5);
            string updatedExtAttendee = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 6);
            string updatedHLAttendee = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 7);

            //Edit Subject details
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(txtSubject).Clear();
            driver.FindElement(txtSubject).SendKeys(updatedSubject);
            Thread.Sleep(2000);

            //Edit Description details
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtareaDescription));
            driver.FindElement(txtareaDescription).Clear();
            driver.FindElement(txtareaDescription).SendKeys(updatedDesc);
            Thread.Sleep(2000);

            //Edit Notes details
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtareaHLInternalMeetingNotes));
            driver.FindElement(txtareaHLInternalMeetingNotes).Clear();
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(updatedNotes);
            Thread.Sleep(2000);

            //Edit Industry Group
            CustomFunctions.MoveToElement(driver, driver.FindElement(drpdownIndustryGroup));
            driver.FindElement(drpdownIndustryGroup).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{updatedIndGrp}']")).Click();
            Thread.Sleep(2000);

            //Edit Product Group
            CustomFunctions.MoveToElement(driver, driver.FindElement(drpdownProductType));
            driver.FindElement(drpdownProductType).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{updatedPrdType}']")).Click();
            Thread.Sleep(2000);
            
            js.ExecuteScript("window.scrollTo(0,1000)");
            Thread.Sleep(2000);

            //Update External Attendee
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtExternalAttendee));
            driver.FindElement(txtExternalAttendee).SendKeys(updatedExtAttendee);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//div[@data-name='{updatedExtAttendee}']")).Click();
            Thread.Sleep(2000);
            
            //Update HL Attendee
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtHLAttendee));
            driver.FindElement(txtHLAttendee).SendKeys(updatedHLAttendee);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//div[@data-name='{updatedHLAttendee}']")).Click();
            Thread.Sleep(2000);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            driver.FindElement(btnSaveActivity).Click();
            Thread.Sleep(5000);
        }

        public void UpdateActivityByNonPrimaryHLAttendee(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string updatedSubject = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 1);
            string updatedIndGrp = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 2);
            string updatedPrdType = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 3);
            string updatedDesc = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 4);
            string updatedNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", row, 5);

            //Edit Subject details
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtSubject));
            driver.FindElement(txtSubject).Clear();
            driver.FindElement(txtSubject).SendKeys(updatedSubject);
            Thread.Sleep(1000);

            //Edit Description details
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtareaDescription));
            driver.FindElement(txtareaDescription).Clear();
            driver.FindElement(txtareaDescription).SendKeys(updatedDesc);
            Thread.Sleep(1000);

            //Edit Notes details
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtareaHLInternalMeetingNotes));
            driver.FindElement(txtareaHLInternalMeetingNotes).Clear();
            driver.FindElement(txtareaHLInternalMeetingNotes).SendKeys(updatedNotes);
            Thread.Sleep(1000);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,1800)");
            Thread.Sleep(2000);

            //Edit Industry Group
            CustomFunctions.MoveToElement(driver, driver.FindElement(drpdownIndustryGroup));
            driver.FindElement(drpdownIndustryGroup).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{updatedIndGrp}']")).Click();
            Thread.Sleep(2000);

            //Edit Product Group
            //CustomFunctions.MoveToElement(driver, driver.FindElement(drpdownProductType));
            driver.FindElement(drpdownProductType).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//lightning-base-combobox-item[@data-value='{updatedPrdType}']")).Click();
            Thread.Sleep(2000);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            driver.FindElement(btnSaveActivity).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyNonActivityUserCannotEditActivity()
        {
            bool result = false;
            Thread.Sleep(5000);

            if(CustomFunctions.IsElementPresent(driver, btnEditActivity) == false)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyBankerIsAbleToRemoveNonPrimaryHLAndExternalAttendees(string extAtt, string hlAtt)
        {
            bool result = false;

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,800)");
            Thread.Sleep(2000);

            //Remove Non Primary Ext Attendee
            driver.FindElement(By.XPath($"//span[text()='Remove {extAtt}']/..")).Click();
            Thread.Sleep(2000);

            //Remove Non Primary HL Attendee
            driver.FindElement(By.XPath($"//span[text()='Remove {hlAtt}']/..")).Click();
            Thread.Sleep(2000);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            driver.FindElement(btnSaveActivity).Click();
            Thread.Sleep(2000);

            if(driver.FindElement(successMsg).Displayed)
            {
                result = true;
            }

            return result;
        }

        public void UpdateHLAndExternalAttendeesAsPrimary()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,800)");
            Thread.Sleep(3000);

            //Update Primary Ext Attendee
            driver.FindElement(By.XPath("((//input[@part='checkbox'])[7]/following::span)[1]")).Click();
            Thread.Sleep(2000);

            //Remove Non Primary HL Attendee
            driver.FindElement(By.XPath("((//input[@part='checkbox'])[10]/following::span)[1]")).Click();
            Thread.Sleep(2000);

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Click Save
            driver.FindElement(btnSaveActivity).Click();
            Thread.Sleep(5000);
        }

        public void SendNotification(string file)
        {
            Thread.Sleep(2000);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string toEmail = ReadExcelData.ReadData(excelPath, "Notification", 1);

            driver.FindElement(btnSendNotification).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtEmailId).SendKeys(toEmail);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//span[text()='{toEmail}']/..")).Click();

            driver.FindElement(btnSendEmail).Click();
            Thread.Sleep(2000);
        }
    }
}