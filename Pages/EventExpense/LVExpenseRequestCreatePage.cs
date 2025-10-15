using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Linq;
using System.Threading;

namespace SF_Automation.Pages.EventExpense
{
    class LVExpenseRequestCreatePage : BaseClass
    {
        //Sahil Locators
        By dropdownLOB = By.XPath("(//label[text()='LOB']/following::div[4]/button)[1]");
        By buttonCreateNewExpenseForm = By.XPath("//button[@title='Create New Expense Form']");

        By txtRequestor = By.XPath("(//label[text()='Requestor']/following::div/input)[1]");
        By selectRequestor = By.XPath("(//a[@data-refid='recordId'])[1]");
        By selectButton = By.XPath("//button[text()='Select']");
        By lblRequestorErr = By.XPath("(//label[text()='Requestor']/following::div/input)[1]/following::div[3]");

        By txtEventContact = By.XPath("//label[text()='Event Contact']/following::div[8]/input");
        By selectEventContact = By.XPath("(//a[@data-refid='recordId'])[1]");
        By lblEventContactErr = By.XPath("//label[text()='Event Contact']/following::div[8]/input/following::div[3]");

        By txtEventName = By.XPath("//input[@placeholder='Search Opportunities...']");
        By selectEventName = By.XPath("(//a[@data-refid='recordId'])[1]");
        By lblEventErr = By.XPath("//input[@placeholder='Search Opportunities...']/following::div[3]");

        By txtStartDate = By.XPath("//input[@name='Start_Date__c']");
        By lblStartDateErr = By.XPath("//input[@name='Start_Date__c']/following::div[1]");

        By comboNoOfGuest = By.XPath("(//label[text()='Number of guests']/following::div/button)[1]");
        By lblNoOfGuestsErr = By.XPath("(//label[text()='Number of guests']/following::div/button)[1]/following::div[3]");

        By txtExpectedAirFareCost = By.XPath("//input[@name='Expected_Airfare_Cost__c']");
        By lblExpectedAirFareCostErr = By.XPath("//input[@name='Expected_Airfare_Cost__c']/following::div[1]");

        By txtExpectedRegistrationFeeCost = By.XPath("//input[@name='Expected_Registration_Fee__c']");
        By lblExpectedRegistrationFeeCostErr = By.XPath("//input[@name='Expected_Registration_Fee__c']/following::div[1]");

        By txtExpectedLodgingCost = By.XPath("//input[@name='Expected_Lodging_Cost__c']");
        By lblExpectedLodgingCostErr = By.XPath("//input[@name='Expected_Lodging_Cost__c']/following::div[1]");

        By txtExpectedFnBCost = By.XPath("//input[@name='Expected_F_B_cost__c']");
        By lblExpectedFnBCostErr = By.XPath("//input[@name='Expected_F_B_cost__c']/following::div[1]");

        By txtOtherCost = By.XPath("//input[@name='Any_additional_cost_Specify__c']");
        By lblOtherCostErr = By.XPath("//input[@name='Any_additional_cost_Specify__c']/following::div[1]");

        By txtDescOtherCost = By.XPath("//input[@name='Specify__c']");
        By btnSave = By.XPath("//button[@type='submit']");
        By btnCancel = By.XPath("//button[text()='Cancel']");
        By lblErrMsg = By.XPath("//span[@class='toastMessage forceActionsText']");

        string dir = @"C:\Users\VKumar0427\source\repos\SF_Automation\TestData\";

        //Vijay Locators
        By btnCreateNewExpenseFormLWC = By.XPath("//button[@title='Create New Expense Form']");
        By btnSaveLWC = By.XPath("//footer//button[text()='Save']");
        By btnSaveEditLWC = By.XPath("//button[@name='SaveEdit']");
        By btnNameLWC = By.XPath("//div[contains(@class,'footer')]//button");
        By msgLOBLWC = By.XPath("//label[text()='LOB']/abbr/../../div[2]/div[contains(@class,'help')]");
        By msgEventTypeLWC = By.XPath("//label[text()='Event Type']/abbr/../../div[2]/div[contains(@class,'help')]");
        By msgRequestorLWC = By.XPath("//label[text()='Requestor']/abbr/../../div[2][contains(@class,'help')]");
        By msgProductTypeLWC = By.XPath("//label[text()='Product Type']/abbr/../../div[2]/div[contains(@class,'help')]");
        By msgEventNameLWC = By.XPath("//label[text()='Event Name']/abbr/../../../div[2][contains(@class,'help')]");
        By msgCityLWC = By.XPath("//label[text()='City']/abbr/../../../div[2][contains(@class,'help')]");
        By msgETCostLWC = By.XPath("//label[text()='Expected Travel Cost']/abbr/../../../div[2][contains(@class,'help')]");
        By msgEFBCostLWC = By.XPath("//label[text()='Expected F&B Cost']/abbr/../../../div[2][contains(@class,'help')]");
        By msgOtherCostLWC = By.XPath("//label[text()='Other Cost']/abbr/../../../div[2][contains(@class,'help')]");
        By msgDescOtherCostLWC = By.XPath("//label[text()='Description of Other Cost']/abbr/../../../div[2][contains(@class,'help')]");
        By msgEventFormatLWC = By.XPath("//label[text()='Event Format']/abbr/../../../div//div[2]/div[contains(@class,'help')]");
        By comboLOBLWC = By.XPath("//label[text()='LOB']/abbr/../..//button");
        By comboEventTypeLWC = By.XPath("//label[text()='Event Type']/abbr/../..//button");
        By comboProductTypeLWC = By.XPath("//label[text()='Product Type']/abbr/../..//button");
        By comboEventFormatLWC = By.XPath("//label[text()='Event Format']/abbr/../..//button");
        By btnNumberofQuestsLWC = By.XPath("//label[text()='Number of guests']/..//button");
        By btnMarketingSupportLWC = By.XPath("//label[text()='Marketing support']/..//button");
        By inputRequestorLWC = By.XPath("//label[text()='Requestor']/..//input");
        By inputEventTypeLWC = By.XPath("//label[text()='Event Contact']/..//input");
        By inputEventNameLWC = By.XPath("//label[text()='Event Name']/..//input");
        By inputCityLWC = By.XPath("//label[text()='City']/..//input");
        By inputStartDateLWC = By.XPath("//label[text()='Start Date']/..//input");
        By inputEndDateLWC = By.XPath("//label[text()='End Date']/..//input");
        By inputETCostLWC = By.XPath("//label[text()='Expected Travel Cost']/..//input");
        By inputEFBCostLWC = By.XPath("//label[text()='Expected F&B Cost']/..//input");
        By inputOtherCostLWC = By.XPath("//label[text()='Other Cost']/..//input");
        By inputDescOtherCostLWC = By.XPath("//label[text()='Description of Other Cost']/..//input");
        By inputHLOppLWC = By.XPath("//label[text()='HL Internal Opportunity Name']/..//input");
        By inputTeamMemberLWC = By.XPath("//label[text()='List of Team Members']/..//input");
        By inputDscMarketingSupportLWC = By.XPath("//label[text()='Description of Marketing Support']/..//input");
        By headerEventTrackingLWC = By.XPath("//h3//span[@title='Event Tracking']");

        public void SaveExpenseRequestSubmitForApprovalRequiredFieldsLWC(string eventType, string numberOfGuest, string teamMember)
        {
            switch(eventType)
            {
                case "ADM - Staff Entertainment":
                    CustomFunctions.MoveToElement(driver, driver.FindElement(headerEventTrackingLWC));
                    driver.FindElement(btnNumberofQuestsLWC).Click();

                    By elmOptionGuests = By.XPath($"//label[text()='Number of guests']/..//lightning-base-combobox-item//span[@title='{numberOfGuest}']");
                    WebDriverWaits.WaitUntilEleVisible(driver, elmOptionGuests, 10);
                    CustomFunctions.MoveToElement(driver, driver.FindElement(elmOptionGuests));
                    driver.FindElement(elmOptionGuests).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, btnSaveEditLWC, 10);
                    CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveEditLWC));
                    driver.FindElement(btnSaveEditLWC).Click();
                    //Thread.Sleep(3000);
                    break;
            }
            WebDriverWaits.WaitUntilEleVisible(driver, inputTeamMemberLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(inputTeamMemberLWC));
            Thread.Sleep(3000);
            driver.FindElement(inputTeamMemberLWC).Click();
            driver.FindElement(inputTeamMemberLWC).SendKeys(teamMember);
            By elmOptionMember = By.XPath($"//label[text()='List of Team Members']/..//li//span[text()='{teamMember}']/../..");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, elmOptionMember, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmOptionMember));
            driver.FindElement(elmOptionMember).Click();
            Thread.Sleep(3000);
            IAlert alert = driver.SwitchTo().Alert();
            Thread.Sleep(2000);
            alert.Accept();
            By elmInList = By.XPath($"//table//td//lightning-base-formatted-text[text()='{teamMember}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmInList, 10);
            Thread.Sleep(5000);
        }

        public string GetValidationsLWC(string bubbleMessage)
        {
            string formatedValidation = bubbleMessage.Replace("\r\n", " ");
            return formatedValidation;
        }

        public string GetButtonnameLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNameLWC, 20);
            return driver.FindElement(btnNameLWC).Text.Trim();
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
            string message = driver.FindElement(msgLOBLWC).Text.Replace("\r\n", " ");
            return message;
        }

        public bool IsLOBPresentLV(string valueLOB)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboLOBLWC, 5);
            driver.FindElement(comboLOBLWC).Click();

            try
            {
                By elmComboLOB = By.XPath($"//label[text()='LOB']//lightning-base-combobox-item//span[@title='{valueLOB}']");
                WebDriverWaits.WaitUntilEleVisible(driver, elmComboLOB, 5);
                bool isFound = driver.FindElement(elmComboLOB).Displayed;
                driver.FindElement(comboLOBLWC).Click();
                return isFound;
            }
            catch
            {
                //driver.FindElement(comboLOBLWC).Click(); 
                return false;
            }
        }

        //To validate LOB validation
        public string ValidateEventTypeMessageLWC(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboLOBLWC, 5);
            driver.FindElement(comboLOBLWC).Click();
            By elmComboLOB = By.XPath($"//label[text()='LOB']/abbr/../..//lightning-base-combobox-item//span[@title='{value}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmComboLOB, 5);
            driver.FindElement(elmComboLOB).Click();
            driver.FindElement(btnCreateNewExpenseFormLWC).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, msgEventTypeLWC, 5);
                driver.FindElement(btnCreateNewExpenseFormLWC).Click();
            }
            catch
            {
                driver.FindElement(btnCreateNewExpenseFormLWC).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, msgEventTypeLWC, 5);
            string message = driver.FindElement(msgEventTypeLWC).Text.Replace("\r\n", " ");
            return message;
        }

        public string ValidateRequestorMessageLWC(string type)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboEventTypeLWC, 5);
            driver.FindElement(comboEventTypeLWC).Click();
            //Thread.Sleep(2000);
            By elmEventType = By.XPath($"//label[text()='Event Type']/abbr/../..//lightning-base-combobox-item//span[@title='{type}']");

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, elmEventType, 5);
            }
            catch
            {
                driver.FindElement(comboEventTypeLWC).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, elmEventType, 5);
            }
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmEventType));
            driver.FindElement(elmEventType).Click();
            driver.FindElement(btnCreateNewExpenseFormLWC).Click();

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnSaveLWC, 5);
                //CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveLWC));                
            }
            catch
            {
                driver.FindElement(btnCreateNewExpenseFormLWC).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnSaveLWC, 10);
            }
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveLWC));
            //Thread.Sleep(2000);
            try
            {
                driver.FindElement(btnSaveLWC).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgRequestorLWC, 5);
                CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveLWC));
                driver.FindElement(btnSaveLWC).Click();
            }
            catch
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveLWC));
                driver.FindElement(btnSaveLWC).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgRequestorLWC, 5);
                CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveLWC));
                driver.FindElement(btnSaveLWC).Click();
            }
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgRequestorLWC));
            string message = driver.FindElement(msgRequestorLWC).Text.Replace("\r\n", " ");
            return message;
        }

        public string ValidateProductTypeLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgProductTypeLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgProductTypeLWC));
            string message = driver.FindElement(msgProductTypeLWC).Text.Replace("\r\n", " ");
            return message;
        }

        public string ValidateEventNameLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgEventNameLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgEventNameLWC));
            string message = driver.FindElement(msgEventNameLWC).Text.Replace("\r\n", " ");
            return message;
        }

        public string ValidateCityLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgCityLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgCityLWC));
            string message = driver.FindElement(msgCityLWC).Text.Replace("\r\n", " ");
            return message;
        }

        public string ValidateETCostLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgETCostLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgETCostLWC));
            string message = driver.FindElement(msgETCostLWC).Text.Replace("\r\n", " ");
            return message;
        }

        public string ValidateEFBCostLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgEFBCostLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgEFBCostLWC));
            string message = driver.FindElement(msgEFBCostLWC).Text.Replace("\r\n", " ");
            return message;
        }

        public string ValidateOtherCostLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgOtherCostLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgOtherCostLWC));
            string message = driver.FindElement(msgOtherCostLWC).Text.Replace("\r\n", " ");
            return message;
        }

        public string ValidateDscOtherCostLWC()
        {
            driver.FindElement(inputOtherCostLWC).SendKeys("10.00");
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveLWC));
            try
            {
                driver.FindElement(btnSaveLWC).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgDescOtherCostLWC, 5);
            }
            catch
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveLWC));
                driver.FindElement(btnSaveLWC).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, msgDescOtherCostLWC, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgDescOtherCostLWC));
            WebDriverWaits.WaitUntilEleVisible(driver, msgDescOtherCostLWC, 5);
            string message = driver.FindElement(msgDescOtherCostLWC).Text.Replace("\r\n", " ");
            return message;
        }

        public void SelectMarketingSupportLWC(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnMarketingSupportLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnMarketingSupportLWC));
            driver.FindElement(btnMarketingSupportLWC).Click();
            By elmMarketingSupport = By.XPath($"//label[text()='Marketing support']/..//lightning-base-combobox-item//span[@title='{value}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmMarketingSupport, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmMarketingSupport));
            driver.FindElement(elmMarketingSupport).Click();
        }

        public bool GetDescriptionMarketingSupportStateLWC()
        {
            CustomFunctions.MoveToElement(driver, driver.FindElement(inputDscMarketingSupportLWC));
            return driver.FindElement(inputDscMarketingSupportLWC).Enabled;
        }

        public string ValidateEventFormatMessageLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgEventFormatLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgEventFormatLWC));
            string message = driver.FindElement(msgEventFormatLWC).Text.Replace("\r\n", " ");
            return message;
        }

        public string ValidateErrorHlopportunityLWC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgEventFormatLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(msgEventFormatLWC));
            string message = driver.FindElement(msgEventFormatLWC).Text.Replace("\r\n", " ");
            return message;
        }

        public void SaveExpenseRequestRequiredFieldsLWC(string nameRequestor, string nameEventContact, string nameProducType, string nameEvent, string nameCity, string ETCost, string EFBCost, string OtherCost, string DescOtherCost)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, inputRequestorLWC, 5);
            driver.FindElement(inputRequestorLWC).SendKeys(nameRequestor);
            By elmRequestor = By.XPath($"//label[text()='Requestor']/..//li//lightning-base-combobox-formatted-text[@title='{nameRequestor}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmRequestor, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmRequestor));
            driver.FindElement(elmRequestor).Click();

            driver.FindElement(inputEventTypeLWC).SendKeys(nameEventContact);
            By elmEventContact = By.XPath($"//label[text()='Event Contact']/..//li//lightning-base-combobox-formatted-text[@title='{nameEventContact}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmEventContact, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmEventContact));
            driver.FindElement(elmEventContact).Click();

            driver.FindElement(comboProductTypeLWC).Click();
            By elmProductType = By.XPath($"//label[text()='Product Type']/abbr/../..//lightning-base-combobox-item//span[@title='{nameProducType}']");
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
            driver.FindElement(inputOtherCostLWC).Clear();
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
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, elmHLOpp, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmHLOpp));
            Thread.Sleep(2000);
            driver.FindElement(elmHLOpp).Click();
            Thread.Sleep(2000);
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

        public void SaveExpenseRequestRequiredFieldstoSubmitLWC(string valLOB, string eventType, string nameRequestor, string nameEventContact, string nameProducType, string nameEvent, string nameCity, string eventFormat, string numOfGuest, string ETCost, string EFBCost, string OtherCost, string DescOtherCost, string nameOpp, string teamMember)
        {
            //select LOB
            WebDriverWaits.WaitUntilEleVisible(driver, comboLOBLWC, 5);
            driver.FindElement(comboLOBLWC).Click();
            By elmComboLOB = By.XPath($"//label[text()='LOB']/abbr/../..//lightning-base-combobox-item//span[@title='{valLOB}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmComboLOB, 5);
            driver.FindElement(elmComboLOB).Click();

            //select Event Type
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboEventTypeLWC, 5);
                driver.FindElement(comboEventTypeLWC).Click();
                By elmEventType = By.XPath($"//label[text()='Event Type']/abbr/../..//lightning-base-combobox-item//span[@title='{eventType}']");
                WebDriverWaits.WaitUntilEleVisible(driver, elmEventType, 5);
                CustomFunctions.MoveToElement(driver, driver.FindElement(elmEventType));
                driver.FindElement(elmEventType).Click();
            }
            catch
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboEventTypeLWC, 5);
                driver.FindElement(comboEventTypeLWC).Click();
                By elmEventType = By.XPath($"//label[text()='Event Type']/abbr/../..//lightning-base-combobox-item//span[@title='{eventType}']");
                WebDriverWaits.WaitUntilEleVisible(driver, elmEventType, 5);
                CustomFunctions.MoveToElement(driver, driver.FindElement(elmEventType));
                driver.FindElement(elmEventType).Click();
            }


            //Click create new button
            this.ClickCreateNewExpenseFormLWC();

            //Requestor
            WebDriverWaits.WaitUntilEleVisible(driver, inputRequestorLWC, 5);
            driver.FindElement(inputRequestorLWC).SendKeys(nameRequestor);
            By elmRequestor = By.XPath($"//label[text()='Requestor']/..//li//lightning-base-combobox-formatted-text[@title='{nameRequestor}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmRequestor, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmRequestor));
            driver.FindElement(elmRequestor).Click();

            //Event Contact
            driver.FindElement(inputEventTypeLWC).SendKeys(nameEventContact);
            By elmEventContact = By.XPath($"//label[text()='Event Contact']/..//li//lightning-base-combobox-formatted-text[@title='{nameEventContact}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmEventContact, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmEventContact));
            driver.FindElement(elmEventContact).Click();

            //Product Type
            driver.FindElement(comboProductTypeLWC).Click();
            By elmProductType = By.XPath($"//label[text()='Product Type']/abbr/../..//lightning-base-combobox-item//span[@title='{nameProducType}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmProductType, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmProductType));
            driver.FindElement(elmProductType).Click();

            //Event Name
            driver.FindElement(inputEventNameLWC).SendKeys(nameEvent + "_" + CustomFunctions.RandomValue());
            driver.FindElement(inputCityLWC).SendKeys(nameCity);

            //Start &End Dates
            string startDate = DateTime.Today.AddDays(0).ToString("MMM dd, yyyy");
            driver.FindElement(inputStartDateLWC).SendKeys(startDate);
            string endDate = DateTime.Today.AddDays(3).ToString("MMM dd, yyyy");
            driver.FindElement(inputEndDateLWC).Clear();
            driver.FindElement(inputEndDateLWC).SendKeys(endDate);

            //Event Format
            WebDriverWaits.WaitUntilEleVisible(driver, comboEventFormatLWC, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboEventFormatLWC));
            driver.FindElement(comboEventFormatLWC).Click();
            By eleEventFormat = By.XPath($"//label[text()='Event Format']/abbr/../..//lightning-base-combobox-item//span[@title='{eventFormat}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleEventFormat, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleEventFormat));
            driver.FindElement(eleEventFormat).Click();

            //No. Of Guests
            CustomFunctions.MoveToElement(driver, driver.FindElement(inputHLOppLWC));
            driver.FindElement(btnNumberofQuestsLWC).Click();
            By elmOptionGuests = By.XPath($"//label[text()='Number of guests']/..//lightning-base-combobox-item//span[@title='{numOfGuest}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmOptionGuests, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmOptionGuests));
            driver.FindElement(elmOptionGuests).Click();

            //HL Internal Opportunity Name
            WebDriverWaits.WaitUntilEleVisible(driver, inputHLOppLWC, 5);
            CustomFunctions.MoveToElement(driver, driver.FindElement(inputHLOppLWC));
            driver.FindElement(inputHLOppLWC).SendKeys(nameOpp);
            By elmHLOpp = By.XPath($"//label[text()='HL Internal Opportunity Name']//ancestor::lightning-layout-item//li//span[text()='{nameOpp}']");
            //Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, elmHLOpp, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmHLOpp));
            driver.FindElement(elmHLOpp).Click();

            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveLWC));
            //Enter Costs
            driver.FindElement(inputETCostLWC).SendKeys(ETCost);
            driver.FindElement(inputEFBCostLWC).SendKeys(EFBCost);
            driver.FindElement(inputOtherCostLWC).Clear();
            driver.FindElement(inputOtherCostLWC).SendKeys(OtherCost);
            driver.FindElement(inputDescOtherCostLWC).SendKeys(DescOtherCost);

            //Add Team Member
            WebDriverWaits.WaitUntilEleVisible(driver, inputTeamMemberLWC, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(inputTeamMemberLWC));
            //Thread.Sleep(3000);
            driver.FindElement(inputTeamMemberLWC).Click();
            driver.FindElement(inputTeamMemberLWC).SendKeys(teamMember);
            By elmOptionMember = By.XPath($"//label[text()='List of Team Members']/..//li//span[text()='{teamMember}']/../..");
            //Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, elmOptionMember, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmOptionMember));
            driver.FindElement(elmOptionMember).Click();
            By elmInList = By.XPath($"//table//td//lightning-base-formatted-text[text()='{teamMember}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmInList, 10);

            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveLWC));
            driver.FindElement(btnSaveLWC).Click();
        }

        public void CreateNewExpenseRequestLWC(string LOB, string file, int userRow)
        {
            Thread.Sleep(3000);
            string excelPath = dir + file;

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(5000);

            //Select LOB
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownLOB, 120);
            driver.FindElement(dropdownLOB).SendKeys(LOB);
            //Thread.Sleep(2000);
            driver.FindElement(dropdownLOB).SendKeys(Keys.Enter);
            driver.FindElement(dropdownLOB).SendKeys(Keys.Enter);
            driver.FindElement(dropdownLOB).SendKeys(Keys.Enter);

            Thread.Sleep(5000);

            //Click on Create New Expense Form button
            driver.FindElement(buttonCreateNewExpenseForm).Click();
            Thread.Sleep(5000);

            //Fill all mandatory fields
            driver.FindElement(txtRequestor).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", userRow, 2));
            Thread.Sleep(5000);
            driver.FindElement(txtRequestor).SendKeys(Keys.ArrowDown);
            driver.FindElement(txtRequestor).SendKeys(Keys.Enter);

            WebDriverWaits.WaitUntilEleVisible(driver, selectRequestor, 120);
            driver.FindElement(selectRequestor).Click();
            //driver.FindElement(selectButton).Click();
            Thread.Sleep(3000);

            driver.FindElement(txtEventContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", userRow, 3));
            Thread.Sleep(5000);
            driver.FindElement(txtEventContact).SendKeys(Keys.ArrowDown);
            driver.FindElement(txtEventContact).SendKeys(Keys.Enter);
            WebDriverWaits.WaitUntilEleVisible(driver, selectEventContact, 120);
            driver.FindElement(selectEventContact).Click();
            //driver.FindElement(selectButton).Click();
            Thread.Sleep(3000);

            driver.FindElement(txtEventName).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 4));
            Thread.Sleep(5000);
            driver.FindElement(txtEventName).SendKeys(Keys.ArrowDown);
            driver.FindElement(txtEventName).SendKeys(Keys.Enter);
            WebDriverWaits.WaitUntilEleVisible(driver, selectEventName, 120);
            driver.FindElement(selectEventName).Click();
            //driver.FindElement(selectButton).Click();
            Thread.Sleep(3000);

            driver.FindElement(txtStartDate).SendKeys(DateTime.Today.ToString("MMM d, yyyy"));
            driver.FindElement(comboNoOfGuest).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 5));
            Thread.Sleep(5000);
            driver.FindElement(comboNoOfGuest).SendKeys(Keys.Enter);

            driver.FindElement(txtExpectedAirFareCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 6));
            driver.FindElement(txtExpectedRegistrationFeeCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 7));
            driver.FindElement(txtOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 8));
            driver.FindElement(txtExpectedLodgingCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 9));
            driver.FindElement(txtExpectedFnBCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 10));
            driver.FindElement(txtDescOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 11));

            //Click Save btton
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);
        }

        public void CreateNewExpenseRequestLWC2(string LOB, string file, int userRow)
        {
            Thread.Sleep(3000);
            string excelPath = dir + file;

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(5000);

            //Select LOB
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownLOB, 120);
            driver.FindElement(dropdownLOB).SendKeys(LOB);
            Thread.Sleep(10000);
            driver.FindElement(dropdownLOB).SendKeys(Keys.Enter);
            Thread.Sleep(5000);

            //Click on Create New Expense Form button
            driver.FindElement(buttonCreateNewExpenseForm).Click();
            Thread.Sleep(5000);

            //Fill all mandatory fields
            driver.FindElement(txtRequestor).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", userRow, 2));
            Thread.Sleep(5000);
            driver.FindElement(txtRequestor).SendKeys(Keys.ArrowDown);
            driver.FindElement(txtRequestor).SendKeys(Keys.Enter);
            WebDriverWaits.WaitUntilEleVisible(driver, selectRequestor, 120);
            driver.FindElement(selectRequestor).Click();
            //driver.FindElement(selectButton).Click();
            Thread.Sleep(3000);

            driver.FindElement(txtEventContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", userRow, 3));
            Thread.Sleep(5000);
            driver.FindElement(txtEventContact).SendKeys(Keys.ArrowDown);
            driver.FindElement(txtEventContact).SendKeys(Keys.Enter);
            WebDriverWaits.WaitUntilEleVisible(driver, selectEventContact, 120);
            driver.FindElement(selectEventContact).Click();
            //driver.FindElement(selectButton).Click();
            Thread.Sleep(3000);

            driver.FindElement(txtEventName).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 4));
            Thread.Sleep(5000);
            driver.FindElement(txtEventName).SendKeys(Keys.ArrowDown);
            driver.FindElement(txtEventName).SendKeys(Keys.Enter);
            WebDriverWaits.WaitUntilEleVisible(driver, selectEventName, 120);
            driver.FindElement(selectEventName).Click();
            //driver.FindElement(selectButton).Click();
            Thread.Sleep(3000);

            driver.FindElement(txtStartDate).SendKeys(DateTime.Today.ToString("MMM dd, yyyy"));
            driver.FindElement(comboNoOfGuest).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 5));
            Thread.Sleep(2000);
            driver.FindElement(comboNoOfGuest).SendKeys(Keys.Enter);
            Thread.Sleep(3000);

            driver.FindElement(txtExpectedAirFareCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 6));
            driver.FindElement(txtExpectedRegistrationFeeCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 7));
            driver.FindElement(txtOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 8));
            driver.FindElement(txtExpectedLodgingCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 9));
            driver.FindElement(txtExpectedFnBCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 10));
            driver.FindElement(txtDescOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 11));

            //Click Save btton
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(10000);
        }

        public bool VerifyRequiredFieldsOnCreateNewExpenseRequestPage(string LOB, string err)
        {
            Thread.Sleep(3000);

            bool result = false;

            //Click on Create New Expense Form button
            driver.FindElement(buttonCreateNewExpenseForm).Click();
            Thread.Sleep(5000);

            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));

            //Click Save button
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(2000);

            CustomFunctions.MoveToElement(driver, driver.FindElement(lblRequestorErr));

            //Verify all mandatory fields
            if(driver.FindElement(lblRequestorErr).Text.Contains(err) && driver.FindElement(lblEventContactErr).Text.Contains(err) && driver.FindElement(lblEventErr).Text.Contains(err) && driver.FindElement(lblStartDateErr).Text.Contains(err) && driver.FindElement(lblNoOfGuestsErr).Text.Contains(err))
            {
                if(driver.FindElement(lblExpectedAirFareCostErr).Text.Contains(err) && driver.FindElement(lblExpectedFnBCostErr).Text.Contains(err) && driver.FindElement(lblExpectedLodgingCostErr).Text.Contains(err) && driver.FindElement(lblExpectedRegistrationFeeCostErr).Text.Contains(err) && driver.FindElement(lblOtherCostErr).Text.Contains(err))
                {
                    result = true;
                }
            }

            return result;
        }

        public bool VerifyFunctionalityOfExpReqOnClickingCancelButton()
        {
            Thread.Sleep(3000);
            bool result = false;

            //Click Cancel button on Create Expense Request page
            driver.FindElement(btnCancel).Click();
            Thread.Sleep(3000);

            //Click Ok button on the alert box
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(5000);

            result = CustomFunctions.IsElementPresent(driver, buttonCreateNewExpenseForm);
            Thread.Sleep(5000);
            return result;
        }

        public string GetErrorMsgDisplayedIfLoggedUserIsNotSelectedAsREquestorOrDelegateOfRequestor(string LOB, string file, int userRow)
        {
            Thread.Sleep(3000);
            string excelPath = dir + file;

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            //Fill all mandatory fields
            driver.FindElement(txtRequestor).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", userRow, 14));
            Thread.Sleep(2000);
            driver.FindElement(txtRequestor).SendKeys(Keys.ArrowDown);
            driver.FindElement(txtRequestor).SendKeys(Keys.Enter);
            //WebDriverWaits.WaitUntilEleVisible(driver, selectRequestor, 120);
            //driver.FindElement(selectRequestor).Click();
            //driver.FindElement(selectButton).Click();
            Thread.Sleep(3000);

            CustomFunctions.MoveToElement(driver, driver.FindElement(txtEventContact));

            driver.FindElement(txtEventContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", userRow, 3));
            Thread.Sleep(2000);
            driver.FindElement(txtEventContact).SendKeys(Keys.ArrowDown);
            driver.FindElement(txtEventContact).SendKeys(Keys.Enter);
            WebDriverWaits.WaitUntilEleVisible(driver, selectEventContact, 120);
            driver.FindElement(selectEventContact).Click();
            //driver.FindElement(selectButton).Click();
            Thread.Sleep(3000);

            CustomFunctions.MoveToElement(driver, driver.FindElement(txtEventName));

            driver.FindElement(txtEventName).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 4));
            Thread.Sleep(5000);
            driver.FindElement(txtEventName).SendKeys(Keys.ArrowDown);
            driver.FindElement(txtEventName).SendKeys(Keys.Enter);
            WebDriverWaits.WaitUntilEleVisible(driver, selectEventName, 120);
            driver.FindElement(selectEventName).Click();
            //driver.FindElement(selectButton).Click();
            Thread.Sleep(3000);

            driver.FindElement(txtStartDate).SendKeys(DateTime.Today.ToString("MMM dd, yyyy"));
            driver.FindElement(comboNoOfGuest).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 5));
            Thread.Sleep(2000);

            driver.FindElement(comboNoOfGuest).SendKeys(Keys.Enter);
            driver.FindElement(txtExpectedAirFareCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 6));
            driver.FindElement(txtExpectedRegistrationFeeCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 7));
            driver.FindElement(txtOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 8));
            driver.FindElement(txtExpectedLodgingCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 9));
            driver.FindElement(txtExpectedFnBCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 10));
            driver.FindElement(txtDescOtherCost).SendKeys(ReadExcelData.ReadData(excelPath, "ExpenseRequest", 11));

            //Click Save btton
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSave));

            driver.FindElement(btnSave).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, lblErrMsg, 120);
            string err = driver.FindElement(lblErrMsg).Text;
            return err;
        }
    }
}