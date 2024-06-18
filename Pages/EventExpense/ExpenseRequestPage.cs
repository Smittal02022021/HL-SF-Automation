using Microsoft.Office.Interop.Excel;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;

namespace SF_Automation.Pages
{
    class ExpenseRequestPage : BaseClass
    {       

        By lnkExpenseRequest = By.CssSelector("a[title='Expense Request Tab']");
        By btnCreateNewExpenseForm = By.CssSelector("input[value*='Create New Expense Form']");
        By msgLOB = By.CssSelector("div[id*='id33:j_id35']");
        By comboLOB = By.CssSelector("select[id*='j_id60:lob']");
        By comboEventType = By.CssSelector("select[id*='j_id65:j_id68']");
        By btnSave =By.CssSelector("input[name*='j_id61']");    
        By msgRequestor = By.CssSelector("div[id*='j_id40']");
        By txtRequestor = By.CssSelector("div[class='requiredInput'] >input[id*='id64']");
        By lblRequestorInfo = By.CssSelector("div[class*='first tertiaryPalette']>h3");
        By btnReturnToExpense = By.CssSelector("input[value*='Back To Expense Request List']");
        By lblNewExpRequest = By.XPath("//h2[text() = 'New Expense Request']");
        By lnkEdit = By.CssSelector("tbody[id*='j_id0'] >tr:first-child >td[id*='id129'] >a");
        By btnCancel = By.CssSelector("input[value*='Cancel']");
        By btnSubmitForApproval = By.CssSelector("input[value*='Submit for Approval']");
        By msgErrorList = By.CssSelector("div[class*='message errorM3']>table>tbody>tr:nth-child(2)>td>span>ul");
        By comboProductType = By.CssSelector("select[id*='j_id67']");
        By txtEventContact = By.CssSelector("input[id='j_id0:form1:pb1:j_id63:j_id66']");
        By txtEventName = By.CssSelector("input[id*='j_id72']");
        By txtCity = By.CssSelector("input[id*='j_id76']");
        By valStartDate = By.CssSelector("div[class='pbSubsection']>table>tbody>tr:nth-child(2)>td>span>span[class='dateFormat']>a");
        By comboEventFormat = By.CssSelector("select[id*='formatid']");
        By comboNumberOfGuests = By.CssSelector("select[id*='j_id97']");
        By txtExpectedTravelCost = By.CssSelector("input[id*='j_id196:etc']");
        By txtOtherCost = By.CssSelector("input[id*='j_id196:aacs']");
        By txtExpectedFBCost = By.CssSelector("input[id*='j_id196:efbc']");
        By txtDescOfOtherCost = By.CssSelector("input[id*='specifyFieldId']");
        By txtHLIntOppName = By.CssSelector("input[id='j_id0:form1:pb1:j_id196:j_id205']");
        By txtListofTeamMembers = By.CssSelector("span + input[id*='j_id218:inputContactId3']");
        By valStatus = By.CssSelector("span[id*='id73']");
        By comboMarketingSupport = By.CssSelector("select[name*='Marketingsupport']");
        By txtDescMarketing = By.CssSelector("input[name*='Marketingsupport']");
        By btnEdit = By.CssSelector("input[value='Edit']");
        By txtEndDate = By.CssSelector("input[name*='id80']");
        By valEndDate = By.CssSelector("div[class='pbSubsection']>table>tbody>tr:nth-child(3)>td>span>span[class='dateFormat']>a");
        By msgOtherCost = By.CssSelector("div[id*='id39']");
        By txtEvalDate = By.CssSelector("input[id*='id192']");
        By valEvalDate = By.CssSelector("div[class='pbSubsection']>table>tbody>tr:nth-child(1)>td>span>span[class='dateFormat']>a");

        string dir = @"C:\Users\vkumar0427\source\repos\SF_Automation\TestData\";

        public void ClickExpenseRequest()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkExpenseRequest);
            driver.FindElement(lnkExpenseRequest).Click();

        }
        //To Click on Create New Expense Form button
        public void ClickCreateNewExpenseForm()
        {
            //Calling wait function-- Create New Expense Form
            WebDriverWaits.WaitUntilEleVisible(driver, btnCreateNewExpenseForm, 70);
            driver.FindElement(btnCreateNewExpenseForm).Click();
        }

        By btnCreateNewExpenseFormLWC = By.XPath("//button[@title='Create New Expense Form']");
        By btnSaveLWC = By.XPath("//footer//button[text()='Save']");
        By msgLOBLWC = By.XPath("//label[text()='LOB']/abbr/../../div[2][contains(@class,'help')]");
        By msgEventTypeLWC= By.XPath("//label[text()='Event Type']/abbr/../../div[2][contains(@class,'help')]");
        By msgRequestorLWC = By.XPath("//label[text()='Requestor']/abbr/../../div[2][contains(@class,'help')]");
        By msgProductTypeLWC = By.XPath("//label[text()='Product Type']/abbr/../../div[2][contains(@class,'help')]");
        By msgEventNameLWC = By.XPath("//label[text()='Event Name']/abbr/../../../div[2][contains(@class,'help')]");
        By msgCityLWC = By.XPath("//label[text()='City']/abbr/../../../div[2][contains(@class,'help')]");
        By msgETCostLWC = By.XPath("//label[text()='Expected Travel Cost']/abbr/../../../div[2][contains(@class,'help')]");
        By msgEFBCostLWC = By.XPath("//label[text()='Expected F&B Cost']/abbr/../../../div[2][contains(@class,'help')]");
        By msgOtherCostLWC = By.XPath("//label[text()='Other Cost']/abbr/../../../div[2][contains(@class,'help')]");
        By msgEventFormatLWC = By.XPath("//label[text()='Event Format']/abbr/../../../div//div[2][contains(@class,'help')]");

        By comboLOBLWC = By.XPath("//label[text()='LOB']/abbr/../..//button");
        By comboEventTypeLWC = By.XPath("//label[text()='Event Type']/abbr/../..//button");
        By comboProductTypeLWC = By.XPath("//label[text()='Product Type']/abbr/../..//button");
        By comboEventFormatLWC = By.XPath("//label[text()='Event Format']/abbr/../..//button");
        By inputRequestorLWC = By.XPath("//label[text()='Requestor']/..//input");
        By inputEventTypeLWC= By.XPath("//label[text()='Event Contact']/..//input");
        By inputEventNameLWC = By.XPath("//label[text()='Event Name']/..//input");
        By inputCityLWC = By.XPath("//label[text()='City']/..//input");
        By inputStartDateLWC = By.XPath("//label[text()='Start Date']/..//input");
        By inputETCostLWC = By.XPath("//label[text()='Expected Travel Cost']/..//input");
        By inputEFBCostLWC = By.XPath("//label[text()='Expected F&B Cost']/..//input");
        By inputOtherCostLWC = By.XPath("//label[text()='Other Cost']/..//input");
        By inputDescOtherCostLWC = By.XPath("//label[text()='Description of Other Cost']/..//input");
        By inputHLOppLWC = By.XPath("//label[text()='HL Internal Opportunity Name']/..//input");

        By headerPageLWC = By.XPath("//h1//records-entity-label");
        By elmEPNumberLWC = By.XPath("//div[contains(@data-target-selection-name,'Event_Expense__c.Name')]//lightning-formatted-text");
            
        public string getPageHeaderLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, headerPageLWC, 20);
            return driver.FindElement(headerPageLWC).Text.Trim();
        }
        public string GetRequestNumberLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, elmEPNumberLWC, 20);
            return driver.FindElement(elmEPNumberLWC).Text.Trim();
        }
        
        public void ClickCreateNewExpenseFormLWC()
        {
            //Calling wait function-- Create New Expense Form
            WebDriverWaits.WaitUntilEleVisible(driver, btnCreateNewExpenseFormLWC, 20);
            driver.FindElement(btnCreateNewExpenseFormLWC).Click();
        }
        //To validate LOB validation
        public string ValidateLOBMessageLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgLOBLWC, 10);
            string message = driver.FindElement(msgLOBLWC).Text;
            return message;
        }
        //To validate LOB validation
        
        public string ValidateEventTypeMessageLWC(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboLOBLWC, 5);
            driver.FindElement(comboLOBLWC).Click();
            By elmComboLOB= By.XPath($"//label[text()='LOB']/abbr/../..//lightning-base-combobox-item//span[@title='{value}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmComboLOB, 5);
            driver.FindElement(elmComboLOB).Click();
            driver.FindElement(btnCreateNewExpenseFormLWC).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, msgEventTypeLWC, 5);
            }
            catch 
            {
                driver.FindElement(btnCreateNewExpenseFormLWC).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, msgEventTypeLWC, 5);
            string message = driver.FindElement(msgEventTypeLWC).Text;
            return message;
        }
        public string ValidateRequestorMessageLWC(string type)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboEventTypeLWC, 5);
            driver.FindElement(comboEventTypeLWC).Click();
            //Thread.Sleep(2000);
            By elmProductType= By.XPath($"//label[text()='Event Type']/abbr/../..//lightning-base-combobox-item//span[@title='{type}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmProductType, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmProductType));
            driver.FindElement(elmProductType).Click();
            //Thread.Sleep(2000);
            driver.FindElement(btnCreateNewExpenseFormLWC).Click();
            
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnSaveLWC, 5);
                //CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveLWC));                
            }
            catch 
            {
                driver.FindElement(btnCreateNewExpenseFormLWC).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnSaveLWC, 5);
            }
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveLWC));
            //Thread.Sleep(2000);
            try
            {
                driver.FindElement(btnSaveLWC).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgRequestorLWC, 5);
            }
            catch
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveLWC));
                driver.FindElement(btnSaveLWC).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgRequestorLWC, 5);
            }            
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgRequestorLWC));
            string message = driver.FindElement(msgRequestorLWC).Text;
            return message;
        }
        public string ValidateProductTypeLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgProductTypeLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgProductTypeLWC));
            string message = driver.FindElement(msgProductTypeLWC).Text;
            return message;
        }
        public string ValidateEventNameLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgEventNameLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgEventNameLWC));
            string message = driver.FindElement(msgEventNameLWC).Text;
            return message;
        }
        public string ValidateCityLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgCityLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgCityLWC));
            string message = driver.FindElement(msgCityLWC).Text;
            return message;
        }
        public string ValidateETCostLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgETCostLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgETCostLWC));
            string message = driver.FindElement(msgETCostLWC).Text;
            return message;
        }

        public string ValidateEFBCostLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgEFBCostLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgEFBCostLWC));
            string message = driver.FindElement(msgEFBCostLWC).Text;
            return message;
        }
        
        public string ValidateOtherCostLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgOtherCostLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgOtherCostLWC));
            string message = driver.FindElement(msgOtherCostLWC).Text;
            return message;
        }
        public string ValidateEventFormatMessageLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgEventFormatLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgEventFormatLWC));
            string message = driver.FindElement(msgEventFormatLWC).Text;
            return message;
        }
        public string ValidateErrorHlopportunityLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgEventFormatLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgEventFormatLWC));
            string message = driver.FindElement(msgEventFormatLWC).Text;
            return message;
        }
        public void SaveExpenseRequestRequiredFieldsLWC(string nameRequestor, string nameEventContact, string nameProducType, string nameEvent, string nameCity,string ETCost,string EFBCost,  string OtherCost, string DescOtherCost)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, inputRequestorLWC, 5);
            driver.FindElement(inputRequestorLWC).SendKeys(nameRequestor);
            By elmRequestor= By.XPath($"//label[text()='Requestor']/..//li//lightning-base-combobox-formatted-text[@title='{nameRequestor}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmRequestor, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmRequestor));            
            driver.FindElement(elmRequestor).Click();

            driver.FindElement(inputEventTypeLWC).SendKeys(nameEventContact);
            By elmEventContact = By.XPath($"//label[text()='Event Contact']/..//li//lightning-base-combobox-formatted-text[@title='{nameEventContact}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmEventContact, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmEventContact));            
            driver.FindElement(elmEventContact).Click();

            driver.FindElement(comboProductTypeLWC).Click();
            By elmProductType=By.XPath($"//label[text()='Product Type']/abbr/../..//lightning-base-combobox-item//span[@title='{nameProducType}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmProductType, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmProductType));            
            driver.FindElement(elmProductType).Click();
            
            driver.FindElement(inputEventNameLWC).SendKeys(nameEvent);
            driver.FindElement(inputCityLWC).SendKeys(nameCity);
            string getDate = DateTime.Today.AddDays(0).ToString("MMM dd, yyyy");
            driver.FindElement(inputStartDateLWC).SendKeys(getDate);

            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveLWC));
            driver.FindElement(inputETCostLWC).SendKeys(ETCost);
            driver.FindElement(inputEFBCostLWC).SendKeys(EFBCost);
            driver.FindElement(inputOtherCostLWC).SendKeys(OtherCost);
            driver.FindElement(inputDescOtherCostLWC).SendKeys(DescOtherCost);

            driver.FindElement(btnSaveLWC).Click();
        }
        public void AssignHLOpportunityLWC(string nameOpp)
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, inputHLOppLWC, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(inputHLOppLWC));
            driver.FindElement(inputHLOppLWC).SendKeys(nameOpp);
            By elmHLOpp = By.XPath($"//label[text()='HL Internal Opportunity Name']//ancestor::lightning-layout-item//li//span[text()='{nameOpp}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmHLOpp, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmHLOpp));
            driver.FindElement(elmHLOpp).Click();
            driver.FindElement(btnSaveLWC).Click();
            
        }
        public void AssignEventFormatLWC(string eventFormat)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboEventFormatLWC, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboEventFormatLWC));
            driver.FindElement(comboEventFormatLWC).Click();
            By eleEventFormat = By.XPath($"//label[text()='Event Format']/abbr/../..//lightning-base-combobox-item//span[@title='{eventFormat}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleEventFormat, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleEventFormat));
            driver.FindElement(eleEventFormat).Click();
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveLWC));
            driver.FindElement(btnSaveLWC).Click();
        }
        public void SaveRequiredExpenseRequestFieldsLWC(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, inputRequestorLWC, 10);
            driver.FindElement(inputRequestorLWC).SendKeys(name);
            By elmRequestor = By.XPath($"//label[text()='Requestor']/..//lightning-base-combobox-item//span[contains(@title,'{name}')]");
            WebDriverWaits.WaitUntilEleVisible(driver, elmRequestor, 10);
            driver.FindElement(elmRequestor).Click();

        }

        //To validate LOB validation
        public string ValidateLOBMessage()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, msgLOB, 90);
            string message = driver.FindElement(msgLOB).Text.Replace("\r\n", " ");
            Console.WriteLine(message);
            return message;
        }

        //To validate Event Type validation
        public string ValidateEventTypeMessage(string value)
        {        
            WebDriverWaits.WaitUntilEleVisible(driver, comboLOB, 90);
            driver.FindElement(comboLOB).SendKeys(value);
            driver.FindElement(btnCreateNewExpenseForm).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgLOB, 90);
            string message = driver.FindElement(msgLOB).Text.Replace("\r\n", " ");
            return message;
        }

        //To validate Requestor/Delegate validation
        public string ValidateRequestorMessage(string type)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboEventType, 20);
            driver.FindElement(comboEventType).SendKeys(type);
            driver.FindElement(btnCreateNewExpenseForm).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 20);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgRequestor, 20);
            string message = driver.FindElement(msgRequestor).Text.Replace("\r\n", " ");
            return message;
        }

        //To save Requestor/Delegate details
        public string SaveRequestorDetails(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtRequestor, 90);
            driver.FindElement(txtRequestor).SendKeys(name);            
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblRequestorInfo, 80);
            string label = driver.FindElement(lblRequestorInfo).Text;
            return label;
        }      

        //To click Back To Expense Request List button 
        public string ClickBackAndValidatePage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToExpense, 90);
            driver.FindElement(btnReturnToExpense).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblNewExpRequest, 80);
            string label = driver.FindElement(lblNewExpRequest).Text;
            return label;
        }

        //Validate Edit functionality
        public string ValidateEditFeature()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEdit, 90);
            driver.FindElement(lnkEdit).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblRequestorInfo, 80);
            string label = driver.FindElement(lblRequestorInfo).Text;
            return label;
        }

        //Validate cancel functionality
        public string ValidateCancelFeature()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel, 90);
            driver.FindElement(btnCancel).Click();
            driver.SwitchTo().Alert().Dismiss();
            driver.FindElement(btnCancel).Click();
            driver.SwitchTo().Alert().Accept();            
            WebDriverWaits.WaitUntilEleVisible(driver, lblNewExpRequest, 80);
            string label = driver.FindElement(lblNewExpRequest).Text;
            return label;
        }

        //To click submit without entering all required fields
        public string ClickSubmitWithoutMandatoryFields()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEdit, 90);
            driver.FindElement(lnkEdit).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(2000);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitForApproval, 70);
            driver.FindElement(btnSubmitForApproval).Click();
            Thread.Sleep(6000);
            string errorList = driver.FindElement(msgErrorList).Text.Replace("\r\n", ", ").ToString();
            return errorList;
        }

        //To navigate back to Return to Expense page
        public void ClickReturnToExpense()
        {
            driver.FindElement(btnReturnToExpense).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEdit, 90);
            driver.FindElement(lnkEdit).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }


        //To save all details of Event Expense form
        public void SaveAllValuesOfEventExpense(string file)
        {
            string excelPath = dir + file;           
            Thread.Sleep(2000);
            driver.FindElement(comboProductType).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 4));
            string valEventContact = ReadExcelData.ReadData(excelPath, "EventExp", 5);
            driver.FindElement(txtEventContact).SendKeys(valEventContact);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithXpath(valEventContact);
            driver.FindElement(txtEventName).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 6));
            driver.FindElement(txtCity).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 7));
            driver.FindElement(valStartDate).Click();
            driver.FindElement(comboEventFormat).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 8));
            driver.FindElement(comboNumberOfGuests).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 9));
            driver.FindElement(txtExpectedTravelCost).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 10));
            driver.FindElement(txtOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 11));
            driver.FindElement(txtExpectedFBCost).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 12));
            driver.FindElement(txtDescOfOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 13));
            string valHLOppName = ReadExcelData.ReadData(excelPath, "EventExp", 14);
            driver.FindElement(txtHLIntOppName).SendKeys(valHLOppName);
            Thread.Sleep(3000);
            //CustomFunctions.SelectValueWithXpath(valHLOppName);
            string valTeamMembers = ReadExcelData.ReadData(excelPath, "EventExp", 15);
            Console.WriteLine(valTeamMembers);
            driver.FindElement(txtListofTeamMembers).SendKeys(valTeamMembers);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//a[contains(@class, 'ui-corner-all')]/ b")).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnSave).Click();
        }
        public void SubmitEventExpenseRequest()
        { 
            WebDriverWaits.WaitUntilEleVisible(driver, btnSubmitForApproval, 90);
            driver.FindElement(btnSubmitForApproval).Click();
            Thread.Sleep(6000);
        }

        //To get value of Status
        public string GetRequestStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStatus, 90);
            string status = driver.FindElement(valStatus).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToExpense, 90);
            driver.FindElement(btnReturnToExpense).Click();
            Thread.Sleep(6000);
            return status;
        }

        //To select Marketing Support 
        public string SelectMarketingSupport(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboMarketingSupport, 90);
            driver.FindElement(comboMarketingSupport).SendKeys(value);
            Thread.Sleep(5000);
            string val = driver.FindElement(txtDescMarketing).Enabled.ToString();
            return val;
        }

        //To enter the value of description of Marketing Support
        public void EnterMarketingSupportDesc()
        {
            driver.FindElement(txtDescMarketing).SendKeys("Test Description");
        }

        //To click on edit button
        public void ClickEditButton()
        {
            WebDriverWaits.WaitUntilClickable(driver, btnEdit);
            driver.FindElement(btnEdit).Click();
        }

        //To enter End Date value
        public void EnterEndDate(string value)
        {
            WebDriverWaits.WaitUntilClickable(driver, txtEndDate);
            driver.FindElement(txtEndDate).SendKeys(value);
            driver.FindElement(btnSave).Click();
        }

        //To enter End Date link
        public void ClickEndDateLink()
        {
            WebDriverWaits.WaitUntilClickable(driver, valEndDate);
            driver.FindElement(valEndDate).Click();
            driver.FindElement(btnSave).Click();
        }

        //To get End Date validations
        public string GetEndDateValidations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgOtherCost, 70);
            string validations = driver.FindElement(msgOtherCost).Text.Replace("\r\n", " ");
            return validations;
        }

        //Clear Other Cost and Description of Other Cost value
        public void ClearCostValuesAndSave()
        {
            WebDriverWaits.WaitUntilClickable(driver, btnEdit);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtOtherCost);
            driver.FindElement(txtOtherCost).Clear();
            driver.FindElement(txtDescOfOtherCost).Clear();
            driver.FindElement(btnSave).Click();
        }

        //To get Other cost validations
        public string GetOtherCostValidations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgOtherCost);
            string validation = driver.FindElement(msgOtherCost).Text.Replace("\r\n", " ");
            return validation;
        }

        //Enter Other cost value and Save
        public string EnterOtherCostAndSave(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtOtherCost);
            driver.FindElement(txtOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 11));
            driver.FindElement(btnSave).Click();
            driver.FindElement(btnSubmitForApproval).Click();
            Thread.Sleep(5000);
            string validation = driver.FindElement(msgOtherCost).Text.Replace("\r\n", " ");
            return validation;
        }

        //Enter Description of Other cost value and Save
        public string EnterDescofOtherCostAndSave(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtDescOfOtherCost);
            driver.FindElement(txtDescOfOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "EventExp", 13));
            driver.FindElement(txtEvalDate).Clear();
            driver.FindElement(txtEvalDate).SendKeys("11/11/2020");
            driver.FindElement(btnSave).Click();
            driver.FindElement(btnSubmitForApproval).Click();
            Thread.Sleep(4000);
            string errorList = driver.FindElement(msgErrorList).Text.Replace("\r\n", ", ").ToString();
            return errorList;
        }

        //Update the value of Evaluation date
        public void UpdateEvalDateAndSubmit()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, valEvalDate);            
            driver.FindElement(valEvalDate).Click();
            driver.FindElement(btnSave).Click();
            driver.FindElement(btnSubmitForApproval).Click();
            Thread.Sleep(5000);
        }
    }
}

