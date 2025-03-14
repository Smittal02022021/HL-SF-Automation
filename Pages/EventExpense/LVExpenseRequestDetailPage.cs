﻿using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;

namespace SF_Automation.Pages.EventExpense
{
    class LVExpenseRequestDetailPage : BaseClass
    {
        //Sahil Locators
        //***********************************************************************************

        //Approver Buttons & Labels
        By btnDeleteApprover = By.XPath("//button[contains(text(),'Delete')]");
        By btnCloneApprover = By.XPath("//button[contains(text(),'Clone')]");
        By btnRequestMoreInformationApprover = By.XPath("//button[contains(text(),'Request More Information')]");
        By btnEditApprover = By.XPath("//button[text()='Edit']");
        By btnApproveApprover = By.XPath("//button[contains(text(),'Approve')]");
        By btnRejectApprover = By.XPath("//button[contains(text(),'Reject')]");
        By lblApproverStatus = By.XPath("(//span[text()='Status']/following::div/span/slot/lightning-formatted-text)[1]");
        
        //Requestor Buttons
        By btnSubmitForApproval = By.XPath("//button[contains(text(),'Submit for Approval')]");
        By btnReqDelete = By.XPath("(//button[contains(text(),'Delete')])[2]");
        By btnOK = By.XPath("//button[text()='Ok']");
        By btnClone = By.XPath("//button[@name='Clone']");
        By btnEdit = By.XPath("//button[@name='Edit']");
        By btnReject = By.XPath("(//button[contains(text(),'Reject')])[2]");
        By btnRequestMoreInformation = By.XPath("//button[text()='Request More Information']");
        
        //Requestor/Host Information
        By linkRequestor = By.XPath("(//span[text()='Requestor']/following::div/a/span/slot/span/slot)[1]");
        By lblStatus = By.XPath("(//span[text()='Status']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblStatusForApprover = By.XPath("(//span[text()='Status']/following::lightning-formatted-text)[1]");
        By lblCloneStatus = By.XPath("((//span[text()='Status'])[2]/following::div/span/slot/lightning-formatted-text)[1]");
        By lblTitle = By.XPath("(//span[text()='Title']/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By lblExpensePreapprovalNumber = By.XPath("(//span[text()='Expense Preapproval Number']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblCloneExpPreAppNum = By.XPath("((//span[text()='Expense Preapproval Number'])[2]/following::div/span/slot/lightning-formatted-text)[1]");
        By linkPrimaryEmail = By.XPath("(//span[text()='Primary Email']/following::div/span/slot/records-formula-output/slot/lightning-formatted-text/a)[1]");
        By linkEventContact = By.XPath("(//span[text()='Event Contact']/following::div/a/span/slot/span/slot)[1]");
        By lblIndustryGroup = By.XPath("(//span[text()='Industry Group']/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By lblPrimaryPhoneNumber = By.XPath("(//span[text()='Primary phone number']/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By lblOffice = By.XPath("(//span[text()='Office']/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");

        //Event Information
        By linkEvent = By.XPath("(//span[text()='Event']/following::records-hoverable-link/div/a/span/slot/span/slot)[1]");
        By lblLOB = By.XPath("(//span[text()='LOB']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblEventType = By.XPath("(//span[text()='Event Type']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblEventFormat = By.XPath("(//span[text()='Event Format']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblStartDate = By.XPath("(//span[text()='Start Date']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblInternalOppNumber = By.XPath("(//span[text()='Internal Opportunity Number']/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By lblClassification = By.XPath("(//span[text()='Classification']/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By lblCity = By.XPath("(//span[text()='City']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblEventLocation = By.XPath("(//span[text()='Event Location']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblNumberOfGuests = By.XPath("(//span[text()='Number of guests']/following::div/span/slot/lightning-formatted-text)[1]");

        //Attendee Budget Information
        By lblDescriptionOfOtherCost = By.XPath("(//span[text()='Description of Other Cost']/following::div/span/slot/lightning-formatted-text)[1]");
        By btnEditEventInfo = By.XPath("//button[@title='Edit Description of Other Cost']");
        By txtDescriptOfOtherCost = By.XPath("//label[text()='Description of Other Cost']/../div/input");
        By lblExpectedAirfareCost = By.XPath("(//span[text()='Expected Airfare Cost']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblExpectedRegistrationFee = By.XPath("(//span[text()='Expected Registration Fee']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblOtherCost = By.XPath("(//span[text()='Other Cost']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblExpectedLodgingCost = By.XPath("(//span[text()='Expected Lodging Cost']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblExpectedFBCost = By.XPath("(//span[text()='Expected F&B Cost']/following::div/span/slot/lightning-formatted-text)[1]");
        By lblTotalBudgetRequested = By.XPath("(//span[text()='Total Budget Requested']/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");

        //Additional Info
        By lblAdditionalInfoNotes = By.XPath("(//span[text()='Notes'])[1]/following::div/span/slot/lightning-formatted-text");

        //General
        By btnSave = By.XPath("//button[@name='SaveEdit']");
        By btnCancel = By.XPath("//button[text()='Cancel']");
        By btnCloneCancel = By.XPath("//button[@name='CancelEdit']");
        By btnCloneSave = By.XPath("//button[@name='SaveEdit']");
        By lblMandatoryFieldWarningMsg = By.XPath("//div/strong[text()='Review the following fields']/following::ul/li/a[text()='Description of Other Cost']");
        By h2NewExpenseRequest = By.XPath("//h2[text()='New HL_Expense Request']");

        //Clone Elements
        By txtRequestor1 = By.XPath("(//label[text()='Requestor']/following::div/input)[1]");
        By lblTitle1 = By.XPath("((//span[text()='Title'])[2]/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By linkPrimaryEmail1 = By.XPath("((//span[text()='Primary Email'])[2]/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By lblIndustryGroup1 = By.XPath("((//span[text()='Industry Group'])[2]/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By lblOffice1 = By.XPath("((//span[text()='Office'])[2]/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");
        By lblStatus1 = By.XPath("(//span[text()='Status']/following::div/span/slot/force-record-output-picklist)[1]");
        By txtEventContact1 = By.XPath("(//label[text()='Event Contact']/following::div/input)[1]");
        By lblPrimaryPhnNum1 = By.XPath("((//span[text()='Primary phone number'])[2]/following::div/span/slot/records-formula-output/slot/lightning-formatted-text)[1]");

        By txtEvent1 = By.XPath("(//label[text()='Event']/following::div/input)[1]");
        By lblLOB1 = By.XPath("((//span[text()='LOB'])[2]/following::div/span/slot/force-record-output-picklist)[1]");

        By txtNotes = By.XPath("//textarea");
        By lblApproverResponse = By.XPath("(//lst-formatted-text)[2]");
        By lblNotes = By.XPath("(//lightning-base-formatted-text)[1]");

        By txtAreaNotes = By.XPath("//label[text()='Notes']/following::div/textarea");
        By txtAreaNotes1 = By.XPath("//label[text()='Textarea field with a placeholder']/following::div/textarea");
        By lblApproverEditExpReqErrorMsg = By.XPath("//ul[@class='errorsList slds-list_dotted slds-m-left_medium']/li");
        //********************************************************************************************************************

        //Vijay Locators
        By totalBudgetRequestedLWC = By.XPath("(//span[contains(@class,'field-label')][text()='Total Budget Requested']/../../..//lightning-formatted-text)[2]");
        By btnApproveLWC = By.XPath("//button[text()='Approve']");
        By btnRejectLWC = By.XPath("//button[text()='Reject']");
        By btnSubmitForApprovalLWC = By.XPath("//button[text()='Submit for Approval']");
        By btnDeleteLWC = By.XPath("(//button[text()='Delete'])[1]");
        By btnReqDeleteLWC = By.XPath("(//button[text()='Delete'])[2]");
        By btnEditRequestorLWC = By.XPath("//button[@title='Edit Requestor']");
        By btnEditEndDateLWC = By.XPath("//button[@title='Edit End Date']");
        By inputEndDateLWC = By.XPath("//label[text()='End Date']/..//input");
        By btnSaveEditLWC = By.XPath("//button[@name='SaveEdit']");
        By valRequestorLWC = By.XPath("//span[contains(@class,'field-label')][text()='Requestor']/../../..//a/span/slot/span/slot");
        By valEventContactLWC = By.XPath("//span[contains(@class,'field-label')][text()='Event Contact']/../../..//a/span/slot/span/slot");
        By elmEPStatusLWC = By.XPath("//span[text()='Status']/../../..//lightning-formatted-text");
        By elmEPNumberLWC = By.XPath("//div[contains(@data-target-selection-name,'Event_Expense__c.Name')]//lightning-formatted-text");
        By valProductTypeLWC = By.XPath("//span[contains(@class,'field-label')][text()='Product Type']/../../..//lightning-formatted-text");
        By valEventNameLWC = By.XPath("//span[contains(@class,'field-label')][text()='Event Name']/../../..//lightning-formatted-text");
        By valEventCityLWC = By.XPath("//span[contains(@class,'field-label')][text()='City']/../../..//lightning-formatted-text");
        By valLOBLWC = By.XPath("//span[contains(@class,'field-label')][text()='LOB']/../../..//lightning-formatted-text");
        By valEventTypeLWC = By.XPath("//span[contains(@class,'field-label')][text()='Event Type']/../../..//lightning-formatted-text");
        By valEventFormatLWC = By.XPath("//span[contains(@class,'field-label')][text()='Event Format']/../../..//lightning-formatted-text");
        By btnInlineEditCityLWC = By.XPath("//button[@title='Edit City']");
        By inputCityLWC = By.XPath("//label[text()='City']/..//input");
        By txtCommentsL = By.XPath("//p//textarea");
        By btnRejectAcceptL = By.XPath("//p//button[@title='Primary action']");

        string dir = @"C:\Users\SMittal0207\source\repos\SF_Automation\TestData\";

        private By _btnEventExpenseRequestLWC(string btnName)
        {
            return By.XPath($"//ul//li//button[text()='{btnName}']");
        }

        public bool IsButtonDisplayedLWC(string btnName)
        {
            return driver.FindElement(_btnEventExpenseRequestLWC(btnName)).Displayed;
        }

        public decimal GetTotalBudgetRequestedLWC()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,800)");
            WebDriverWaits.WaitUntilEleVisible(driver, totalBudgetRequestedLWC, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(totalBudgetRequestedLWC));
            string totalBudget = driver.FindElement(totalBudgetRequestedLWC).Text.Split(' ')[1];// seperate the Currency and amount and get the actual amout
            return CustomFunctions.ReadDecimalValueFromString(totalBudget);
        }

        public void ClickApproveButtonLWC()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, _btnEventExpenseRequestLWC("Approve(LWC)"), 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(_btnEventExpenseRequestLWC("Approve(LWC)")));
            driver.FindElement(_btnEventExpenseRequestLWC("Approve(LWC)")).Click();
            //Thread.Sleep(8000);
            WebDriverWaits.WaitUntilAlertVisible(driver, 20);
            Thread.Sleep(1000);
            IAlert alert = driver.SwitchTo().Alert();
            Thread.Sleep(1000);
            alert.Accept();
        }

        public void EditExpenseRequestCityLWC(string cityName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnInlineEditCityLWC, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnInlineEditCityLWC));
            driver.FindElement(btnInlineEditCityLWC).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, inputCityLWC, 20);
            driver.FindElement(inputCityLWC).Clear();
            driver.FindElement(inputCityLWC).SendKeys(cityName);
            Thread.Sleep(2000);
            driver.FindElement(btnSaveEditLWC).Click();
            Thread.Sleep(6000);
        }

        public string GetExpenseRequestStatusLWC()
        {
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, elmEPStatusLWC, 30);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmEPStatusLWC));
            return driver.FindElement(elmEPStatusLWC).Text;
        }

        public void ClickRejectButtonLWC()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, _btnEventExpenseRequestLWC("Reject(LWC)"), 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(_btnEventExpenseRequestLWC("Reject(LWC)")));
            driver.FindElement(_btnEventExpenseRequestLWC("Reject(LWC)")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtCommentsL, 10);
            driver.FindElement(txtCommentsL).SendKeys(" Request Rejected Automation");
            WebDriverWaits.WaitUntilEleVisible(driver, btnRejectAcceptL, 10);
            driver.FindElement(btnRejectAcceptL).Click();
            Thread.Sleep(5000);
        }

        public void ClickEditExpenseRequestButtonLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditRequestorLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditRequestorLWC));
            driver.FindElement(btnEditRequestorLWC).Click();
        }

        public void EditExpenseRequestEndDateLWC(string date)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditEndDateLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditEndDateLWC));
            driver.FindElement(btnEditEndDateLWC).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, inputEndDateLWC, 10);
            driver.FindElement(inputEndDateLWC).Clear();
            driver.FindElement(inputEndDateLWC).SendKeys(date);
            driver.FindElement(btnSaveEditLWC).Click();

        }

        public bool VerifyIfExpensePreapprovalNumberIsDisplayed()
        {
            Thread.Sleep(10000);
            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver, lblExpensePreapprovalNumber, 120);
            if(driver.FindElement(lblExpensePreapprovalNumber).Displayed)
            {
                result = true;
            }
            return result;
        }

        public string GetExpensePreapprovalNumber()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblExpensePreapprovalNumber, 120);
            string expensePreapprovalNum = driver.FindElement(lblExpensePreapprovalNumber).Text;
            Thread.Sleep(3000);
            return expensePreapprovalNum;
        }

        public string GetExpenseRequestStatusLWC(int windowId)
        {
            CustomFunctions.SwitchToWindow(driver, windowId);
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, elmEPStatusLWC, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmEPStatusLWC));
            return driver.FindElement(elmEPStatusLWC).Text;
        }

        public string GetEventFormatLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEventFormatLWC, 60);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valEventFormatLWC));
            return driver.FindElement(valEventFormatLWC).Text;
        }

        public string GetEventTypeLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEventTypeLWC, 60);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valEventTypeLWC));
            return driver.FindElement(valEventTypeLWC).Text;
        }

        public string GetLOBLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLOBLWC, 60);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valLOBLWC));
            return driver.FindElement(valLOBLWC).Text;
        }

        public string GetEventCityLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEventCityLWC, 60);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valEventCityLWC));
            return driver.FindElement(valEventCityLWC).Text;
        }

        public string GetEventNameLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEventNameLWC, 60);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valEventNameLWC));
            return driver.FindElement(valEventNameLWC).Text;
        }

        public string GetProductTypeLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valProductTypeLWC, 60);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valProductTypeLWC));
            return driver.FindElement(valProductTypeLWC).Text;
        }

        public string GetEventContactLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEventContactLWC, 60);
            string eventContact = driver.FindElement(valEventContactLWC).Text;
            return eventContact;
        }

        public string GetCloneExpensePreapprovalNumber()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblCloneExpPreAppNum, 120);
            string expensePreapprovalNum = driver.FindElement(lblCloneExpPreAppNum).Text;
            Thread.Sleep(3000);
            return expensePreapprovalNum;
        }

        public string GetEventTypeInfo()
        {
            Thread.Sleep(3000);
            string eventTypeInfo = driver.FindElement(lblEventType).Text;
            Thread.Sleep(3000);
            return eventTypeInfo;
        }

        public string GetEventFormatInfo()
        {
            Thread.Sleep(3000);
            string eventFormatInfo = driver.FindElement(lblEventFormat).Text;
            Thread.Sleep(3000);
            return eventFormatInfo;
        }

        public string GetRequestNumberLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, elmEPNumberLWC, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmEPNumberLWC));
            return driver.FindElement(elmEPNumberLWC).Text.Trim();
        }

        public string GetRequestorLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRequestorLWC, 60);
            string requestorValue = driver.FindElement(valRequestorLWC).Text;
            return requestorValue;
        }

        public void ClickEventExpenseRequestButtonLWC(string btnName)
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, _btnEventExpenseRequestLWC(btnName), 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(_btnEventExpenseRequestLWC(btnName)));
            driver.FindElement(_btnEventExpenseRequestLWC(btnName)).Click();
            Thread.Sleep(5000);
        }

        public void ClickRequestMoreInformationButtonLWC()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, _btnEventExpenseRequestLWC("Request More Information"), 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(_btnEventExpenseRequestLWC("Request More Information")));
            driver.FindElement(_btnEventExpenseRequestLWC("Request More Information")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtCommentsL, 10);
            driver.FindElement(txtCommentsL).SendKeys(" Request More Information Automation");
            WebDriverWaits.WaitUntilEleVisible(driver, btnRejectAcceptL, 10);
            driver.FindElement(btnRejectAcceptL).Click();
            Thread.Sleep(5000);
        }

        public string GetEventStatusInfo()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblStatus, 120);
            string eventStatusInfo = driver.FindElement(lblStatus).Text;
            Thread.Sleep(3000);
            return eventStatusInfo;
        }

        public string GetEventStatusInfoForApprover()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblApproverStatus, 120);
            string eventStatusInfo = driver.FindElement(lblApproverStatus).Text;
            Thread.Sleep(3000);
            return eventStatusInfo;
        }

        public string GetCloneEventStatusInfo()
        {
            Thread.Sleep(3000);
            string eventStatusInfo = driver.FindElement(lblCloneStatus).Text;
            Thread.Sleep(3000);
            return eventStatusInfo;
        }

        public string GetDeletedStatusInfo()
        {
            Thread.Sleep(3000);
            string eventStatusInfo = driver.FindElement(lblStatus).Text;
            Thread.Sleep(3000);
            return eventStatusInfo;
        }

        public void EditEventInformation(string file, int userRow)
        {
            Thread.Sleep(5000);
            string excelPath = dir + file;

            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditEventInfo));

            WebDriverWaits.WaitUntilEleVisible(driver, btnEditEventInfo, 120);
            driver.FindElement(btnEditEventInfo).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtDescriptOfOtherCost).Clear();
            Thread.Sleep(2000);
            driver.FindElement(txtDescriptOfOtherCost).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", userRow, 15));

            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 120);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);
        }

        public string GetUpdatedDescriptionOfOtherCost()
        {
            Thread.Sleep(3000);

            CustomFunctions.MoveToElement(driver, driver.FindElement(lblDescriptionOfOtherCost));

            WebDriverWaits.WaitUntilEleVisible(driver, lblDescriptionOfOtherCost, 120);
            string updatedDescOfOtherCost = driver.FindElement(lblDescriptionOfOtherCost).Text;
            Thread.Sleep(3000);
            return updatedDescOfOtherCost;
        }

        public bool VerifyMandatoryFieldErrorMessageWhileTryingToEditEventInfo(string file, int userRow)
        {
            Thread.Sleep(3000);
            string excelPath = dir + file;

            Thread.Sleep(3000);

            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditEventInfo));

            WebDriverWaits.WaitUntilEleVisible(driver, btnEditEventInfo, 120);
            driver.FindElement(btnEditEventInfo).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtDescriptOfOtherCost).Clear();
            Thread.Sleep(2000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblMandatoryFieldWarningMsg, 120);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblMandatoryFieldWarningMsg));
            bool result = CustomFunctions.IsElementPresent(driver, lblMandatoryFieldWarningMsg);

            CustomFunctions.MoveToElement(driver, driver.FindElement(txtDescriptOfOtherCost));

            driver.FindElement(txtDescriptOfOtherCost).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", userRow, 15));
            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);
            return result;
        }

        public bool VerifyCloneButtonFunctionality()
        {
            Thread.Sleep(3000);
            bool result = false;

            CustomFunctions.MoveToElement(driver, driver.FindElement(lblTitle));

            //Get Expense Request details from the details page
            string requestor = driver.FindElement(linkRequestor).Text;
            string title = driver.FindElement(lblTitle).Text;
            string primaryEmail = driver.FindElement(linkPrimaryEmail).Text;
            string industryGroup = driver.FindElement(lblIndustryGroup).Text;
            string office = driver.FindElement(lblOffice).Text;
            string status = driver.FindElement(lblStatus).Text;
            string expensePreApprovalNum = driver.FindElement(lblExpensePreapprovalNumber).Text;
            string eventContact = driver.FindElement(linkEventContact).Text;
            string primaryPhnNum = driver.FindElement(lblPrimaryPhoneNumber).Text;

            string eventName = driver.FindElement(linkEvent).Text;
            string lob = driver.FindElement(lblLOB).Text;
            string eventType = driver.FindElement(lblEventType).Text;
            string eventFormat = driver.FindElement(lblEventFormat).Text;
            string startDate = driver.FindElement(lblStartDate).Text;
            string internalOppNum = driver.FindElement(lblInternalOppNumber).Text;
            string classfication = driver.FindElement(lblClassification).Text;
            string city = driver.FindElement(lblCity).Text;
            string eventLoc = driver.FindElement(lblEventLocation).Text;
            string numOfGuests = driver.FindElement(lblNumberOfGuests).Text;

            CustomFunctions.MoveToElement(driver, driver.FindElement(lblExpectedAirfareCost));

            string expAirfareCost = driver.FindElement(lblExpectedAirfareCost).Text;
            string expRegFee = driver.FindElement(lblExpectedRegistrationFee).Text;
            string otherCost = driver.FindElement(lblOtherCost).Text;
            string descOfOtherCost = driver.FindElement(lblDescriptionOfOtherCost).Text;
            string expLodgingCost = driver.FindElement(lblExpectedLodgingCost).Text;
            string expFBCost = driver.FindElement(lblExpectedFBCost).Text;
            string totalbudgetReq = driver.FindElement(lblTotalBudgetRequested).Text;

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            CustomFunctions.MoveToElement(driver, driver.FindElement(btnClone));

            driver.FindElement(btnClone).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, h2NewExpenseRequest, 120);

            string requestor1 = driver.FindElement(txtRequestor1).GetAttribute("data-value");
            string title1 = driver.FindElement(lblTitle1).Text;
            string primaryEmail1 = driver.FindElement(linkPrimaryEmail1).Text;
            string industryGroup1 = driver.FindElement(lblIndustryGroup1).Text;
            string office1 = driver.FindElement(lblOffice1).Text;
            string status1 = driver.FindElement(lblStatus1).Text;
            string eventContact1 = driver.FindElement(txtEventContact1).GetAttribute("data-value");
            string primaryPhnNum1 = driver.FindElement(lblPrimaryPhnNum1).Text;

            string event1 = driver.FindElement(txtEvent1).GetAttribute("data-value");
            string lob1 = driver.FindElement(lblLOB1).Text;

            if(CustomFunctions.IsElementPresent(driver, h2NewExpenseRequest) == true)
            {
                if(requestor == requestor1 && title == title1 && primaryEmail == primaryEmail1 && industryGroup == industryGroup1 && office == office1 && status == status1 && eventContact == eventContact1 && primaryPhnNum == primaryPhnNum1)
                {
                    if(eventName == event1 && lob == lob1)
                    {
                        result = true;
                    }
                }
            }

            WebDriverWaits.WaitUntilEleVisible(driver, btnCloneCancel, 120);
            driver.FindElement(btnCloneCancel).Click();
            Thread.Sleep(3000);

            return result;
        }

        public void CreateCloneExpenseRequest()
        {
            driver.FindElement(btnClone).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnCloneSave, 120);
            driver.FindElement(btnCloneSave).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyDeleteExpenseRequestFunctionalityAsApprover()
        {
            bool result = false;

            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteApprover, 120);
            driver.FindElement(btnDeleteApprover).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 120);
            driver.FindElement(txtNotes).SendKeys("Test");
            driver.FindElement(btnOK).Click();
            Thread.Sleep(10000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblStatus, 120);
            Thread.Sleep(3000);
            if(driver.FindElement(lblStatus).Text == "Deleted")
            {
                result = true;
            }

            return result;
        }

        public bool VerifyDeleteExpenseRequestFunctionality()
        {
            bool result = false;

            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteLWC, 120);
            driver.FindElement(btnDeleteLWC).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 120);
            driver.FindElement(txtNotes).SendKeys("Test");
            driver.FindElement(btnOK).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, lblStatus, 120);
            Thread.Sleep(3000);
            if(driver.FindElement(lblStatus).Text == "Deleted")
            {
                result = true;
            }

            return result;
        }

        public bool VerifyDeleteExpenseRequestFunctionalityAsRequestor()
        {
            bool result = false;

            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnReqDelete, 120);
            driver.FindElement(btnReqDelete).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnOK, 120);
            driver.FindElement(txtNotes).SendKeys("Test");
            driver.FindElement(btnOK).Click();

            WebDriverWaits.WaitForPageToLoad(driver, 120);
            WebDriverWaits.WaitUntilEleVisible(driver, lblStatus, 120);
            Thread.Sleep(3000);
            if(driver.FindElement(lblStatus).Text == "Deleted")
            {
                result = true;
            }

            return result;
        }

        public string GetApproverResponseFromApprovalHistorySection()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblApproverResponse, 120);
            string approverResp = driver.FindElement(lblApproverResponse).Text;

            return approverResp;
        }

        public string GetApproverResponseFromApprovalHistorySectionForApprover()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(5000);

            By lblApproverResponseForApprover = By.XPath("//table[@aria-label='Event Expense Approval History']/tbody/tr[1]/td[2]/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/lst-formatted-text");
            string approverResp = driver.FindElement(lblApproverResponseForApprover).Text;

            return approverResp;
        }

        public string GetNotesFromApprovalHistorySection()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblNotes, 120);
            string notes = driver.FindElement(lblNotes).Text;

            return notes;
        }

        public string GetNotesFromApprovalHistorySectionForApprover()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            CustomFunctions.MoveToElement(driver, driver.FindElement(By.XPath("//table[@aria-label='Event Expense Approval History']/tbody/tr")));

            IList<IWebElement> elements = driver.FindElements(By.XPath("//table[@aria-label='Event Expense Approval History']/tbody/tr"));
            int size = elements.Count;

            By lblAppNotes = By.XPath("//table[@aria-label='Event Expense Approval History']/tbody/tr[1]/td[3]/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/lightning-base-formatted-text");

            WebDriverWaits.WaitUntilEleVisible(driver, lblAppNotes, 120);
            string notes = driver.FindElement(lblAppNotes).Text;

            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            return notes;
        }

        public bool SubmitExpenseRequestForApproval()
        {
            bool result = false;

            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitForApproval, 120);
            //driver.FindElement(btnSubmitForApprovalLWC).Click();

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("arguments[0].click();", driver.FindElement(btnSubmitForApproval)); ;
            Thread.Sleep(15000);

            WebDriverWaits.WaitForPageToLoad(driver, 120);
            Thread.Sleep(15000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblStatus, 120);
            Thread.Sleep(3000);
            if(driver.FindElement(lblStatus).Text == "Waiting for Approval")
            {
                result = true;
            }

            return result;
        }

        public void EditExpenseRequestAsApprover(string notes)
        {
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnEditApprover, 120);
            driver.FindElement(btnEditApprover).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, txtAreaNotes, 120);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtAreaNotes));
            driver.FindElement(txtAreaNotes).Clear();
            driver.FindElement(txtAreaNotes).SendKeys(notes);

            Thread.Sleep(5000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);
        }

        public bool VerifyApproverIsNotAbleToEditExpenseRequest(string msg)
        {
            bool result = false;
            Thread.Sleep(5000);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, txtAreaNotes, 120);
            driver.FindElement(txtAreaNotes).Clear();
            driver.FindElement(txtAreaNotes).SendKeys("Test Notes");

            driver.FindElement(btnSave).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblApproverEditExpReqErrorMsg, 120);
            string errorMsg = driver.FindElement(lblApproverEditExpReqErrorMsg).Text;
            if(errorMsg == msg)
            {
                result = true;
            }

            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);

            return result;
        }

        public string GetApproverNotesDetailsUnderAdditionalInfo()
        {
            Thread.Sleep(5000);

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblAdditionalInfoNotes, 120);
            string approverNotes = driver.FindElement(lblAdditionalInfoNotes).Text;
            Thread.Sleep(3000);
            return approverNotes;
        }

        public bool VerifyNecessaryButtonsAreDisplayedWhenApproverLandsOnExpenseDetailPage()
        {
            bool result = false;
            Thread.Sleep(3000);
            if(driver.FindElement(btnEditApprover).Displayed && driver.FindElement(btnApproveApprover).Displayed && driver.FindElement(btnRejectApprover).Displayed && driver.FindElement(btnDeleteApprover).Displayed && driver.FindElement(btnRequestMoreInformationApprover).Displayed)
            {
                result = true;
            }
            return result;
        }

        public void RejectExpenseRequest(string notes)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnRejectApprover, 120);
            driver.FindElement(btnRejectApprover).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtAreaNotes, 120);
            driver.FindElement(txtAreaNotes).SendKeys(notes);
            Thread.Sleep(3000);
            driver.FindElement(btnReject).Click();
            Thread.Sleep(20000);
        }

        public void RequestForMoreInformation(string notes)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnRequestMoreInformationApprover, 120);
            driver.FindElement(btnRequestMoreInformationApprover).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtAreaNotes1, 120);
            driver.FindElement(txtAreaNotes1).SendKeys(notes);
            Thread.Sleep(3000);
            driver.FindElement(btnOK).Click();
            Thread.Sleep(20000);
        }

        public void ApproveExpenseRequest()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnApproveApprover, 120);
            driver.FindElement(btnApproveApprover).Click();
            Thread.Sleep(8000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(10000);

            Actions action1 = new Actions(driver);
            action1.SendKeys(Keys.LeftControl + "R").Build().Perform();
            Thread.Sleep(5000);
        }
    }
}