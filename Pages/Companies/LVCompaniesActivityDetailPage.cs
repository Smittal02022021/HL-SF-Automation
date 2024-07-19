using AventStack.ExtentReports.Utils;
using Microsoft.Office.Interop.Excel;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static NUnit.Framework.Internal.OSPlatform;

namespace SF_Automation.Pages.Companies
{   
    class LVCompaniesActivityDetailPage: BaseClass
    {
        By chartActivities = By.XPath("//div[contains(@class,'chart-widget-container')]");
        By btnAddActivity = By.XPath("//header//button[text()='Add Activity']");
        By btnSaveActivity = By.XPath("//button[text()='Save']");
        By msgLVPopup = By.CssSelector("span.toastMessage.forceActionsText");
        By reqFieldErrMsg = By.XPath("//label[text()='Subject']/following::div[2]");
        By btnLVPopupClose = By.XPath("//button[contains(@class,'toastClose')]");
        By tableActivities = By.XPath("//div[contains(@class,'table_header')]//table");
        By txtDefaultHLAttandee = By.XPath("//c-s-l_-lwc-multi-lookup[contains(@class,'lookupForHLAttendee')]//lightning-pill//span[contains(@class,'pill__label')]");
        By txtDefaultCompanyDiscussed = By.XPath("//c-s-l_-lwc-multi-lookup[contains(@class,'lookupForAccount')]//lightning-pill//span[contains(@class,'pill__label')]");
        By btnCreateNewTask = By.XPath("//button[text()='Create New Task']");
        By txtDate = By.XPath("//input[contains(@name,'Followup_Start_Date')]");
        By dropdownFollowupType = By.XPath("//button[contains(@aria-label,'Follow-up Type')]");
        By dropdownFolloupFrom = By.XPath("//button[contains(@name,'Followup_Start_Time')][contains(@aria-label,'From')]");
        By dropdownFolloupTo = By.XPath("//button[contains(@name,'Followup_End_Time')][contains(@aria-label,'To')]");
        By txtAreaFollowuoComments = By.XPath("//textarea[contains(@name,'Followup_Comments')]");
        By btnSave = By.XPath("(//button[@title='Save'])[2]");
        By btnActivityDetailPage = By.XPath("//article[contains(@class,'narrow')]//header//slot[@name='actions']//button");
        By btnActivitydetailFileUpload = By.XPath("//lightning-primitive-file-droppable-zone//span[text()='Upload Files']");
        By duelListBox = By.XPath("(//div[contains(@class,'dueling-list')]//ul)[1]");
        By txtSubject = By.XPath("//input[@name = 'Subject']");
        By txtFollowupDate = By.XPath("//input[contains(@name,'Followup_Start_Date')]");
        By headerFollowup= By.XPath("//h2//span[@title='Schedule Followup']");
        By chkPrivate = By.XPath("//span[text()='Private']//parent::label");

        By btnEdit = By.XPath("(//button[text()='Edit'])[2]");

        //By btnDialogDone = By.XPath("//div[@role='dialog']//div[contains(@class,'modal-footer')]//button");
        By txtUploadedFile = By.XPath("//lightning-file-upload//following-sibling::lightning-datatable//table//td[@data-label='File Name']//a");
        By iFrameExternal = By.XPath("//iframe[@title='External Web Page']");
        By iFrameEmailTemplate = By.XPath("//iframe[contains(@title,'Rich Text Editor')]");
        By txtEmailTemplate = By.XPath("//body[contains(@id,'SendEmail')]");

        private By _btnActivityStartDate(string btnName)
        {
            return By.XPath($"//div[contains(text(),'{btnName}')]//ancestor::button");
        }
        private By _btnActivityDetailPage(string btnName)
        {
            return By.XPath($"//button[text()='{btnName}']");
        }
        private By _comboDropdown(string value)
        {
            return (By.XPath($"//lightning-base-combobox-item[@data-value='{value}']"));
        }
        private By _linkDiscussedItem(string item)
        {
            return By.XPath($"//table//tbody//a[text()='{item}']");
        }
        
        public bool IsActivitiesChartDisplayed()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, chartActivities, 20);
                return driver.FindElement(chartActivities).Displayed;                
            }catch { return false; }            
        }
        public string DefaultDateSelection(string btnName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _btnActivityStartDate(btnName), 20);
            return driver.FindElement(_btnActivityStartDate(btnName)).GetAttribute("aria-pressed").ToString();
        }
        public string ClickSaveButton(string btnName)
        {
            driver.FindElement(_btnActivityDetailPage(btnName)).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgLVPopup, 20);
            return driver.FindElement(msgLVPopup).Text;
        }
        public string GetRequiredFieldErrorMsg()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, reqFieldErrMsg, 20);
            string toastMsg= driver.FindElement(reqFieldErrMsg).Text;
            return toastMsg;
        }

        public string GetLVMessagePopup()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgLVPopup, 20);
            string toastMsg = driver.FindElement(msgLVPopup).Text;
            return toastMsg;
        }

        public void CloseTab(string tabName)
        {
            Thread.Sleep(5000);
            driver.FindElement(By.XPath($"//button[contains(@title,'Close {tabName}')]")).Click();
            Thread.Sleep(5000);
        }

        public void ClickActivityDetailPageButton(string btnName)
        {
            //IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            //jse.ExecuteScript("arguments[0].scrollIntoView();", _btnActivityDetailPage(btnName));
            CustomFunctions.MoveToElement(driver, driver.FindElement(_btnActivityDetailPage(btnName)));
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, _btnActivityDetailPage(btnName), 20);
            driver.FindElement(_btnActivityDetailPage(btnName)).Click();
        }

        public void ClickEditButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 20);
            driver.FindElement(btnEdit).Click();
        }

        public bool IsActivityListDisplayed()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tableActivities, 20);
                return driver.FindElement(tableActivities).Displayed;
            }catch { return false; }
        }
        public string GetDefaultPrimaryHlAttandeeHLAttandee()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtDefaultHLAttandee, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtDefaultHLAttandee));            
            return driver.FindElement(txtDefaultHLAttandee).Text;
        }
        public string GetDefaultCompaniesDiscussed()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtDefaultCompanyDiscussed, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtDefaultCompanyDiscussed));
            return driver.FindElement(txtDefaultCompanyDiscussed).Text;
        }

        public bool VerifyActivityDetailPageAvailableHeaderButtons(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            bool isSame = true;
            int row = 2;
            //WebDriverWaits.WaitUntilEleVisible(driver, btnActivitiesRow, 30);
            //CustomFunctions.MoveToElement(driver, driver.FindElement(btnActivitiesRow));
            //driver.FindElement(btnActivitiesRow).Click();
            //Thread.Sleep(2000);
            //Get columns count            
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(btnActivityDetailPage);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            int excelCount = ReadExcelData.GetRowCount(excelPath, "ActivityDetailButton");
            string[] expectedValue = new string[excelCount];
            int expectedOptionsCount = excelCount - 1;
            if (expectedOptionsCount != actualValue.Length)
            {
                return !isSame;
            }            
            for (int rec = 0; rec < expectedOptionsCount; rec++)
            {
                expectedValue[rec] = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityDetailButton", row, 1);
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
                row++;
            }
            return isSame;
        }
        public bool VerifyActivityDetailPageFileUploadButton()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnActivitydetailFileUpload, 10);
                CustomFunctions.MoveToElement(driver, driver.FindElement(btnActivitydetailFileUpload));
                return driver.FindElement(btnActivitydetailFileUpload).Displayed;
            }
            catch { return false; }            
        }
        public bool VerifyActivityDetailPageStatus()
        {
            try
            {
                return driver.FindElement(txtSubject).Enabled;
            }catch { return false; }   
        }
        public void UpdateActivity(string updateSubject)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSubject, 10);
            driver.FindElement(txtSubject).Clear();
            driver.FindElement(txtSubject).SendKeys(updateSubject);
            ////Click Save
            //driver.FindElement(btnSave).Click();
        }
        //Create Followup 
        public void CreateFollowup(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string typeFollowup = ReadExcelData.ReadData(excelPath, "Followup", 1);
            string fromFollowup = ReadExcelData.ReadData(excelPath, "Followup", 2);
            string toFollowup = ReadExcelData.ReadData(excelPath, "Followup", 3);
            string commentsFollowup = ReadExcelData.ReadData(excelPath, "Followup", 4);

            //Click on Create New Task button
            WebDriverWaits.WaitUntilEleVisible(driver, btnCreateNewTask, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnCreateNewTask));
            driver.FindElement(btnCreateNewTask).Click();
            Thread.Sleep(3000);

            //Enter Followup  details
            CustomFunctions.MoveToElement(driver, driver.FindElement(dropdownFollowupType));
            driver.FindElement(dropdownFollowupType).Click();
            Thread.Sleep(2000);
            driver.FindElement(_comboDropdown(typeFollowup)).Click();
            Thread.Sleep(2000);

            DateTime currentDate1 = DateTime.Today;
            DateTime setDate1 = currentDate1.AddDays(2);
            IWebElement followupDate = driver.FindElement(txtFollowupDate);
            CustomFunctions.MoveToElement(driver, followupDate);
            followupDate.Clear();
            followupDate.SendKeys(setDate1.ToString("dd-MMM-yyyy"));

            IWebElement followupFrom = driver.FindElement(dropdownFolloupFrom);
            CustomFunctions.MoveToElement(driver, followupFrom);
            followupFrom.Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//button[contains(@name,'Followup_Start_Time')]//parent::div//following-sibling::div//lightning-base-combobox-item[@data-value='{fromFollowup}']")).Click();

            IWebElement followupTo = driver.FindElement(dropdownFolloupTo);
            CustomFunctions.MoveToElement(driver, followupTo);
            followupTo.Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath($"//button[contains(@name,'Followup_End_Time')]//parent::div//following-sibling::div//lightning-base-combobox-item[@data-value='{toFollowup}']")).Click();

            IWebElement areaFollowuoComments = driver.FindElement(txtAreaFollowuoComments);
            CustomFunctions.MoveToElement(driver, areaFollowuoComments);
            areaFollowuoComments.SendKeys(commentsFollowup);
        }
        By chckPrimarySelection = By.XPath("//lightning-input[@data-class='primaryCheck']");
        public string GetPrimaryHlAttandeeHLAttandee()
        {
            string contactName="";
            int recordCount = driver.FindElements(chckPrimarySelection).Count;
            CustomFunctions.MoveToElement(driver, driver.FindElement(chckPrimarySelection));
            for (int recordIndex = 1; recordIndex <= recordCount; recordIndex++)
            {
                if(driver.FindElement(By.XPath($"(//lightning-input[@data-class='primaryCheck'])[{recordIndex}]//label//span[@part='indicator']")).Text.IsNullOrEmpty())
                {
                    contactName = driver.FindElement(By.XPath($"(//lightning-input[@data-class='primaryCheck'])[{recordIndex}]//ancestor::header//h2//span")).Text;
                    break;
                }
            }
            return contactName;
        }

        public void RemovePrimaryContact(string name)
        {
            driver.FindElement(By.XPath($"//lightning-pill//span[@title='{name}']//parent::span//lightning-button-icon//button")).Click();
        }
        public void ClickDicsussionItemName(string item)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            CustomFunctions.MoveToElement(driver, driver.FindElement(headerFollowup));
            Thread.Sleep(2000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(_linkDiscussedItem(item)));
            
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(_linkDiscussedItem(item)));
            //driver.FindElement(_linkDiscussedItem(item)).Click();
            Thread.Sleep(5000);                          
        }

        public void ClickPrivateCheckbox()
        {
            CustomFunctions.MoveToElement(driver, driver.FindElement(chkPrivate));
            driver.FindElement(chkPrivate).Click();
        }

        public string UploadFileAndValidate(string path)
        {
            //IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnActivitydetailFileUpload));
            Thread.Sleep(1000);
            CustomFunctions.FileUpload(driver, path);
            //Thread.Sleep(10000);
            //WebDriverWaits.WaitUntilEleVisible(driver, btnDialogDone, 30);
            //driver.FindElement(btnDialogDone).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, txtUploadedFile, 30);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtUploadedFile));
            return driver.FindElement(txtUploadedFile).Text;

        }
        public string GetEmailTemplate()
        {
            Thread.Sleep(10000);
            CustomFunctions.SwitchToWindow(driver, 1);
            driver.SwitchTo().Frame(driver.FindElement(iFrameExternal));
            Thread.Sleep(2000); // new change
            driver.SwitchTo().Frame(driver.FindElement(iFrameEmailTemplate));
            WebDriverWaits.WaitUntilEleVisible(driver, txtEmailTemplate, 30);
            string emailTemplate = driver.FindElement(txtEmailTemplate).Text;
            driver.SwitchTo().DefaultContent();
            CustomFunctions.CloseWindow(driver, 1);
            CustomFunctions.SwitchToWindow(driver, 0);
            return emailTemplate;
        }
        //public void CreateActivityFollowupCompanyDetailPage()
        //{
        //    //Click on Create New Task button
        //    WebDriverWaits.WaitUntilEleVisible(driver, btnCreateNewTask, 20);
        //    CustomFunctions.MoveToElement(driver, driver.FindElement(btnCreateNewTask));
        //    driver.FindElement(btnCreateNewTask).Click();
        //    Thread.Sleep(3000);

        //    //Enter Followup  details
        //    driver.FindElement(dropdownFollowupType).Click();
        //    Thread.Sleep(2000);
        //    driver.FindElement(_comboDropdown(typeFollowup)).Click();
        //    Thread.Sleep(2000);

        //    DateTime currentDate = DateTime.Today;
        //    DateTime setDate = currentDate.AddDays(2);
        //    driver.FindElement(txtDate).Clear();
        //    driver.FindElement(txtDate).SendKeys(setDate.ToString("dd-MMM-yyyy"));


        //    driver.FindElement(dropdownFolloupFrom).Click();
        //    Thread.Sleep(2000);
        //    driver.FindElement(_comboDropdown(fromFollowup)).Click();

        //    driver.FindElement(dropdownFolloupTo).Click();
        //    Thread.Sleep(2000);
        //    driver.FindElement(_comboDropdown(toFollowup)).Click();

        //    driver.FindElement(txtAreaFollowuoComments).SendKeys(commentsFollowup);


        //    //Click Save
        //    driver.FindElement(btnSave).Click();

        //}
    }
}
