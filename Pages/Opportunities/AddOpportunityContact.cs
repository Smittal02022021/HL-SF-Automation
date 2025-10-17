using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System.Threading;
using System;

namespace SF_Automation.Pages.Opportunity
{
    class AddOpportunityContact : BaseClass
    {
        By btnAddOppContact = By.CssSelector("input[name='new_external_team']");
        By btnSave = By.CssSelector("input[name='save']");
        By txtContact = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000D7Ns3']");
        By comboRole = By.CssSelector("select[name*='D7OOx']");
        By comboPartyL = By.XPath("//div[8]/div/ul/li/a");
        By comboType = By.CssSelector("select[name*='D7OAq']");
        By checkPrimaryContact = By.CssSelector("input[name*='D7Nro']");
        By comboParty = By.CssSelector("select[name*='M0eMp']");
        By checkAckBillingContact = By.CssSelector("input[name*='M0jSN']");
        By checkBillingContact = By.CssSelector("input[name*='Gz3dL']");
        By btnPartyL = By.XPath("//span[text()='Party']/ancestor::div[2]//a");
        By btnPartyCFL = By.XPath("//span[text()='Party']/ancestor::div[2]//a");

        By chkAckBillingContactL = By.XPath("//span[text()='Acknowledge Billing Contact']/following::input[1]");
        By chkPrimaryContactL = By.XPath("//span[text()='Primary Contact']/following::input[1]");
        By txtContactL = By.XPath("//input[@title='Search Contacts']");

        By imgContactOppL = By.XPath("//div[@title='Chris Lord']");
        By btnSaveL = By.XPath("//div/footer/button[2]/span");
        By tabRelated = By.XPath("//a[text()='Comments']");
        By tabContactsL = By.XPath("//a[text()='Contacts']");
        By valAddedContact = By.XPath("//tbody/tr/th//span/a[2]");
        By msgParty = By.XPath("//span[text()='Party']/ancestor::div[2]/ul/li");

        By btnCancelContact = By.XPath("//footer/button[1]/span");

        //Lightning--

        By chkBillingContactL = By.XPath("//span[text()='Billing Contact']/following::input[1]");

        //By imgContactL = By.XPath("//div[2]/ul/li[26]/a/div[1]/span/img");
        By imgContactL = By.XPath("//div[2]/ul/li[9]/a/div[1]/span/img");
        By msgContact = By.XPath("//span[text()='Contact']/ancestor::div[2]/ul/li");

        By valContactNum = By.XPath("//flexipage-component2[2]/slot/flexipage-tabset2/div/lightning-tabset/div/slot/slot/flexipage-tab2[2]/slot/flexipage-component2[2]/slot/lst-dynamic-related-list/article/laf-progressive-container/slot/lst-dynamic-related-list-with-user-prefs/lst-related-list-view-manager/lst-common-list-internal/lst-list-view-manager-header/div/div[1]/div[1]/div/div/h2/a/span[2]");

        By dropdownContactType = By.XPath("//span[text()='Type']/../..//a");
        By btnAddCFContactL = By.XPath("//button[contains(@name,'Add_CF_Opportunity_Contact')]");
        By btnAddFRContactL = By.XPath("//button[contains(@name,'Add_FR_Opportunity_Contact')]");//can be modified with above 
        By btnAddFVAContactL = By.XPath("//button[contains(@name,'Add_FVA_Opportunity_Contact')]");
        By btnAddOppContactL = By.XPath("//button[contains(@name,'Opportunity_Contact')]");


        public void CreateClientContactL(string nameContact, string partyContact, string typeContact)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            // string name = ReadExcelData.ReadData(excelPath, "AddContact", 1);//prm
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactL, 20);
            driver.FindElement(txtContactL).SendKeys(nameContact);
            Thread.Sleep(3000);
            try
            {
                By listContactOption = By.XPath($"//div[@role='listbox']//ul//li//a//div[2]//div[1][@title='{nameContact}']");
                WebDriverWaits.WaitUntilEleVisible(driver, listContactOption, 20);
                driver.FindElement(listContactOption).Click();
            }
            catch (Exception ex)
            {
                By iconContactSearchItem = By.XPath("//div[contains(@class,'searchButton')]");
                WebDriverWaits.WaitUntilEleVisible(driver, iconContactSearchItem, 5);
                driver.FindElement(iconContactSearchItem).Click();
                By txtContact = By.XPath("//div[contains(@class,'gridInScroller')]//table//tbody//tr[1]//td[1]//a");
                WebDriverWaits.WaitUntilEleVisible(driver, txtContact, 20);
                driver.FindElement(txtContact).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownContactType, 20);
            driver.FindElement(dropdownContactType).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//div[8]/div/ul/li/a[text()='{typeContact}']")).Click();//prm

            WebDriverWaits.WaitUntilEleVisible(driver, btnPartyL, 20);
            driver.FindElement(btnPartyL).Click();
            Thread.Sleep(3000);
            //string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);//in prm
            driver.FindElement(By.XPath($"//div[9]/div/ul/li/a[text()='{partyContact}']")).Click();
            driver.FindElement(chkBillingContactL).Click();
            driver.FindElement(chkAckBillingContactL).Click();
            driver.FindElement(chkPrimaryContactL).Click();
            driver.FindElement(btnSaveL).Click();

            driver.FindElement(btnCancelContact).Click();
        }
        private By _btnAddContactL(string lob)
        {
            return By.XPath($"//button[contains(@name,'Add_{lob}_Opportunity_Contact')]");
        }
        public void ClickAddOpportunityContactLV(string recordType)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            WebDriverWaits.WaitUntilEleVisible(driver, _btnAddContactL(recordType), 10);
            driver.FindElement(_btnAddContactL(recordType)).Click();
        }
        public void CickAddOpportunityContact(string RecordType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _btnAddContactL(RecordType), 20);
            driver.FindElement(_btnAddContactL(RecordType)).Click();
        }
        public void CreateContact(string file, string contact, string valRecType, string valType)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, btnAddOppContact, 170);
            driver.FindElement(btnAddOppContact).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 80);

            driver.FindElement(txtContact).SendKeys(contact);
            driver.FindElement(comboRole).SendKeys(ReadExcelData.ReadData(excelPath, "AddContact", 2));
            driver.FindElement(comboType).SendKeys(valType);
            driver.FindElement(checkPrimaryContact).Click();
            if (valRecType.Equals("CF"))
            {
                driver.FindElement(comboParty).SendKeys(ReadExcelData.ReadData(excelPath, "AddContact", 3));
            }
            driver.FindElement(checkAckBillingContact).Click();
            driver.FindElement(checkBillingContact).Click();
            driver.FindElement(btnSave).Click();
        }
        public void CreateContact(string file, string contact, string valRecType, string valType, int rowNumber)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, btnAddOppContact, 50);
            driver.FindElement(btnAddOppContact).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 80);

            driver.FindElement(txtContact).SendKeys(contact);
            driver.FindElement(comboRole).SendKeys(ReadExcelData.ReadData(excelPath, "AddContact", 2));
            driver.FindElement(comboType).SendKeys(valType);

            string Type = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowNumber, 4);
            if (Type.Equals("Client"))
            {
                driver.FindElement(checkPrimaryContact).Click();
            }
            if (valRecType.Equals("CF"))
            {
                driver.FindElement(comboParty).SendKeys(ReadExcelData.ReadData(excelPath, "AddContact", 3));
            }
            if (Type.Equals("External"))
            {
                driver.FindElement(checkPrimaryContact).Click();
                driver.FindElement(checkAckBillingContact).Click();
                driver.FindElement(checkBillingContact).Click();
            }
            driver.FindElement(btnSave).Click();
        }
        public void CickAddCFOpportunityContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCFContactL, 20);
            driver.FindElement(btnAddCFContactL).Click();
        }

        public void CreateContactL2(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string name = ReadExcelData.ReadData(excelPath, "AddContact", 1);//prm
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactL, 20);
            driver.FindElement(txtContactL).SendKeys(name);
            Thread.Sleep(3000);
            try
            {
                By listContactOption = By.XPath($"//div[@role='listbox']//ul//li//a//div[2]//div[1][@title='{name}']");
                WebDriverWaits.WaitUntilEleVisible(driver, listContactOption, 5);
                driver.FindElement(listContactOption).Click();

            }
            catch (Exception ex)
            {
                By iconContactSearchItem = By.XPath("//div[contains(@class,'searchButton')]");
                WebDriverWaits.WaitUntilEleVisible(driver, iconContactSearchItem, 5);
                driver.FindElement(iconContactSearchItem).Click();
                By txtContact = By.XPath("//div[contains(@class,'gridInScroller')]//table//tbody//tr[1]//td[1]//a");
                WebDriverWaits.WaitUntilEleVisible(driver, txtContact, 20);
                driver.FindElement(txtContact).Click();
            }
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnPartyL, 20);
                driver.FindElement(btnPartyL).Click();
                Thread.Sleep(3000);
                string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);//in prm
                driver.FindElement(By.XPath("(//span[text()='Party']/following::div//a[text()='" + party + "'])[1]")).Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Party not found: " + ex.Message);
            }

            try
            {
                driver.FindElement(comboRole).SendKeys(ReadExcelData.ReadData(excelPath, "AddContact", 2));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Role not found: " + ex.Message);
            }

            driver.FindElement(chkBillingContactL).Click();
            driver.FindElement(chkAckBillingContactL).Click();
            driver.FindElement(chkPrimaryContactL).Click();
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
        }

        public void CreateContactL2(string file, string recordType)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string name = ReadExcelData.ReadData(excelPath, "AddContact", 1);//prm
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactL, 20);
            driver.FindElement(txtContactL).SendKeys(name);
            Thread.Sleep(3000);
            try
            {
                By listContactOption = By.XPath($"//div[@role='listbox']//ul//li//a//div[2]//div[1][@title='{name}']");
                WebDriverWaits.WaitUntilEleVisible(driver, listContactOption, 5);
                driver.FindElement(listContactOption).Click();

            }
            catch (Exception ex)
            {
                By iconContactSearchItem = By.XPath("//div[contains(@class,'searchButton')]");
                WebDriverWaits.WaitUntilEleVisible(driver, iconContactSearchItem, 5);
                driver.FindElement(iconContactSearchItem).Click();
                By txtContact = By.XPath("//div[contains(@class,'gridInScroller')]//table//tbody//tr[1]//td[1]//a");
                WebDriverWaits.WaitUntilEleVisible(driver, txtContact, 20);
                driver.FindElement(txtContact).Click();
            }
            if (recordType == "CF")
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnPartyL, 20);
                driver.FindElement(btnPartyL).Click();
                Thread.Sleep(3000);
                string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);//in prm
                driver.FindElement(By.XPath($"//ul//li//a[@title='{party}']")).Click(); //div[8]/div/ul/li/a[text()='" + party + "']")).Click();
            }
            driver.FindElement(chkBillingContactL).Click();
            driver.FindElement(chkAckBillingContactL).Click();
            driver.FindElement(chkPrimaryContactL).Click();
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
        }

        public void CreateContactL(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string name = ReadExcelData.ReadData(excelPath, "AddContact", 1);
            driver.FindElement(txtContactL).SendKeys(name);
            Thread.Sleep(4000);
            driver.FindElement(txtContactL).Clear();
            Thread.Sleep(5000);
            driver.FindElement(txtContactL).SendKeys(name);
            Thread.Sleep(8000);
            driver.FindElement(By.XPath("//div[@title='" + name + "']")).Click();
            driver.FindElement(btnPartyL).Click();
            Thread.Sleep(3000);
            string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
            driver.FindElement(By.XPath("//div[8]/div/ul/li/a[text()='" + party + "']")).Click();

            driver.FindElement(chkBillingContactL).Click();
            driver.FindElement(chkAckBillingContactL).Click();
            driver.FindElement(chkPrimaryContactL).Click();
            driver.FindElement(btnSaveL).Click();
        }
        public void CreateContactCFL(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string name = ReadExcelData.ReadData(excelPath, "AddContact", 1);
            driver.FindElement(txtContactL).SendKeys(name);
            Thread.Sleep(4000);
            driver.FindElement(txtContactL).Clear();
            Thread.Sleep(5000);
            driver.FindElement(txtContactL).SendKeys(name);
            Thread.Sleep(8000);
            driver.FindElement(By.XPath("//div[@title='" + name + "']")).Click();
            driver.FindElement(btnPartyCFL).Click();
            Thread.Sleep(3000);
            string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
            driver.FindElement(By.XPath("//div[8]/div/ul/li/a[text()='" + party + "']")).Click();

            driver.FindElement(chkBillingContactL).Click();
            driver.FindElement(chkAckBillingContactL).Click();
            driver.FindElement(chkPrimaryContactL).Click();
            driver.FindElement(btnSaveL).Click();
        }
        public void ValidateCancelFunctionalityOfContactL(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string name = ReadExcelData.ReadData(excelPath, "AddContact", 1);
            driver.FindElement(txtContactL).SendKeys(name);
            driver.FindElement(txtContactL).Clear();
            Thread.Sleep(5000);
            driver.FindElement(txtContactL).SendKeys(name);
            Thread.Sleep(8000);
            driver.FindElement(imgContactOppL).Click();
            driver.FindElement(btnPartyCFL).Click();
            Thread.Sleep(3000);
            string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
            driver.FindElement(By.XPath("//div[8]/div/ul/li/a[text()='" + party + "']")).Click();
            driver.FindElement(btnCancelContact).Click();
        }

        public void CreateClientContactLV(string nameContact, string partyContact, string typeContact)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            // string name = ReadExcelData.ReadData(excelPath, "AddContact", 1);//prm
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactL, 20);
            driver.FindElement(txtContactL).SendKeys(nameContact);
            Thread.Sleep(3000);
            try
            {
                By listContactOption = By.XPath($"//div[@role='listbox']//ul//li//a//div[2]//div[1][@title='{nameContact}']");
                WebDriverWaits.WaitUntilEleVisible(driver, listContactOption, 20);
                driver.FindElement(listContactOption).Click();
            }
            catch (Exception ex)
            {
                By iconContactSearchItem = By.XPath("//div[contains(@class,'searchButton')]");
                WebDriverWaits.WaitUntilEleVisible(driver, iconContactSearchItem, 5);
                driver.FindElement(iconContactSearchItem).Click();
                By txtContact = By.XPath("//div[contains(@class,'gridInScroller')]//table//tbody//tr[1]//td[1]//a");
                WebDriverWaits.WaitUntilEleVisible(driver, txtContact, 20);
                driver.FindElement(txtContact).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownContactType, 20);
            driver.FindElement(dropdownContactType).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//ul/li/a[text()='{typeContact}']")).Click();//prm

            WebDriverWaits.WaitUntilEleVisible(driver, btnPartyL, 20);
            driver.FindElement(btnPartyL).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//ul/li/a[text()='" + partyContact + "']")).Click();
            driver.FindElement(chkBillingContactL).Click();
            driver.FindElement(chkAckBillingContactL).Click();
            driver.FindElement(chkPrimaryContactL).Click();
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);

            //driver.FindElement(btnCancelContact).Click();
        }

        //Validate added Contact
        public string ValidateAddedContact()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabContactsL, 150);
            driver.FindElement(tabContactsL).Click();
            Thread.Sleep(8000);
            //driver.Navigate().Refresh();            
            try
            {
                string name = driver.FindElement(valAddedContact).Text;
                return name;
            }
            catch (Exception e)
            {
                return "No contact is added";
            }
        }

        //Validate added Contact
        public string ValidateAddedContactWhileCancel()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabRelated, 50);
            driver.FindElement(tabRelated).Click();
            Thread.Sleep(12000);
            string name = driver.FindElement(valContactNum).Text;
            return name;
        }

        //Get Validation of Contact 
        public string GetContactValidation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 150);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, msgContact, 50);
            string name = driver.FindElement(msgContact).Text;
            return name;
        }

        //Get Validation of Contact 
        public string GetPartyValidation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgParty, 50);
            string name = driver.FindElement(msgParty).Text;
            return name;
        }

        //Click on AddFROpportunityContact
        public void CickAddFROpportunityContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddFRContactL, 20);
            driver.FindElement(btnAddFRContactL).Click();
        }

        public void CickAddOpportunityContactLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddOppContactL, 20);
            driver.FindElement(btnAddOppContactL).Click();
        }

        public void CickAddFVAOpportunityContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddFVAContactL, 20);
            driver.FindElement(btnAddFVAContactL).Click();
        }

    }
}


