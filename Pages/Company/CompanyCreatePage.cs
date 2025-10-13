using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System.Threading;

namespace SF_Automation.Pages.Company
{
    class CompanyCreatePage : BaseClass
    {
        By lnkCompanies = By.CssSelector("a[title*='Companies Tab']");
        By btnAddCompany = By.CssSelector("td[class='pbButton center'] > input[value='Add Company']");
        By headingCompanyCreate = By.CssSelector("h2[class='mainTitle']");
        By selectedCompanyType = By.CssSelector("select[id*='j_id77'] > option[selected='selected']");
        //By txtCompanyName = By.CssSelector("input[name='Name']");// input[id*='AccountName']");
        By txtCompanyName = By.XPath("//input[@name='Name']");
        //By comboCompanyCountry = By.CssSelector("input[name='country']");// select[id*='AccountCountry']");
        By comboCompanyCountry = By.XPath("//input[@name='country']");
        //By txtCompanyStreet = By.CssSelector("textarea[id*='j_id79']");
        By txtCompanyStreet = By.XPath("//label[text()='Street']/..//textarea");
        //By txtCompanyCity = By.CssSelector("input[name='city']");
        By txtCompanyCity = By.XPath("//input[@name='city']");// input[id*='AccountCity']");
        //By comboCompanyState = By.CssSelector("select[id*='AccountState']");
        By comboCompanyState = By.XPath("//input[@name='province']");
        By txtCompanyPostalCode = By.XPath("//input[@name='postalCode']");// input[id*='AccountPostalCode']");
        By btnSave = By.CssSelector("td[class='pbButtonb '] > input[value='Save']");
        By btnSaveIgnoreAlert = By.CssSelector("td[class='pbButton '] > input[value='Save(Ignore Alert)']");
        By btnCancelAlert = By.CssSelector("td[class='pbButton '] > input[value='Cancel']");
        By errmsgCmpanyName = By.CssSelector("div[class='errorMsg']");
        By errmsgCompanyNameL = By.XPath("//div[@class='fieldLevelErrors']//li//a");//div[@class='message errorM3']//td//div");

        By newCompanyPageFrameL = By.XPath("//iframe[@title='accessibility title']");
        By txtCreateCompanyHeaderL = By.XPath("//div[contains(@class,'header-container')]//h2");//div[@class='editPage']//h2");
        By optionCompanyTypeL = By.XPath("//span[text()='Company Type']/../../..//records-record-type");//table[@class='detailList']//label[text()='Company Type']//ancestor::tr/td//option[@selected='selected']");
        By btnSaveCompanyL = By.XPath("//button[@name='SaveEdit']");//div[@class='editPage']//table//input[@type='submit'][@value='Save']");
        By btnCancelCompanyL = By.XPath("//button[@name='CancelEdit']");//div[@class='editPage']//table//input[@type='submit'][@value='Cancel']");
        By btnNewCompanyL = By.XPath("//ul//li//a[@title='New']");
        By btnNextL = By.XPath("//div[contains(@class,'ChangeRecordTypeFooter')]//button/span[text()='Next']");
        By iframeCompanyForm = By.XPath("//iframe[contains(@name,'vfFrame')]");
        By txtNewCompanyName = By.XPath("//input[@name='Name']"); //By.CssSelector("input[name*='AccountName']");
        By btnSaveL = By.XPath("//button[@name='SaveEdit']");//By.XPath("//div[@class='pbBottomButtons']//input[@value='Save']");
        By txtCompanyNameL = By.XPath("//span[text()='Company Name']/../..//lightning-formatted-text"); //span[text()='Company Name']/../../..//dd//lightning-formatted-text");
        By popDuplicateL = By.XPath("//span[contains(@class,'toastMessage')]");
        private By _btnRadioRecordType(string type)
        {
            return By.XPath($"//h2[text()='New Company']/..//label//span[text()='{type}']");
        }
        public string CreateCompanyLV(string file, string recordType)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string country = ReadExcelData.ReadData(excelPath, "CompanyType", 2);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCompanyL, 10);
            driver.FindElement(btnNewCompanyL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, _btnRadioRecordType(recordType), 10);
            driver.FindElement(_btnRadioRecordType(recordType)).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnNextL, 10);
            driver.FindElement(btnNextL).Click();

            string valCompanyName = recordType + "_" + (CustomFunctions.RandomValue());
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iframeCompanyForm, 10);
                driver.SwitchTo().Frame(driver.FindElement(iframeCompanyForm));
            }
            catch
            {

            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtNewCompanyName, 10);
            driver.FindElement(txtNewCompanyName).SendKeys(valCompanyName);

            // Select country 
            WebDriverWaits.WaitUntilEleVisible(driver, comboCompanyCountry, 2);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtCompanyPostalCode));
            driver.FindElement(comboCompanyCountry).Click();
            By elmCountry = By.XPath($"//input[@name='country']/../../../..//lightning-base-combobox-item//span[@title='{country}']");

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, elmCountry, 2);
            }
            catch
            {
                driver.FindElement(comboCompanyCountry).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, elmCountry, 2);
            }
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmCountry));
            //Thread.Sleep(2000);
            driver.FindElement(elmCountry).Click();

            driver.FindElement(btnSaveL).Click();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyNameL, 10);
            return driver.FindElement(txtCompanyNameL).Text;
        }

        public string GetErrorsMessageCreateCoverageTeamPageLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, errmsgCompanyNameL, 10);
            return driver.FindElement(errmsgCompanyNameL).Text.Replace("\r\n", " ").Trim();
        }

        public bool ValidateDuplicateAlertForCreateNewCompanyLV(string file)
        {
            bool alertFound = false;
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            // Enter company name
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName, 40);
            string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 2);
            driver.FindElement(txtCompanyName).SendKeys(companyNameExl);
            // Select country 
            WebDriverWaits.WaitUntilEleVisible(driver, comboCompanyCountry, 40);
            driver.FindElement(comboCompanyCountry).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 3));
            //Enter street 
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyStreet, 40);
            driver.FindElement(txtCompanyStreet).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 4));

            // Select country 
            WebDriverWaits.WaitUntilEleVisible(driver, comboCompanyCountry, 2);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtCompanyPostalCode));
            driver.FindElement(comboCompanyCountry).Click();
            string country = ReadExcelData.ReadData(excelPath, "Company", 3);
            By elmCountry = By.XPath($"//input[@name='country']/../../../..//lightning-base-combobox-item//span[@title='{country}']");

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, elmCountry, 2);
            }
            catch
            {
                driver.FindElement(comboCompanyCountry).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, elmCountry, 2);
            }
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmCountry));
            //Thread.Sleep(2000);
            driver.FindElement(elmCountry).Click();
            // Enter city
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyCity, 40);
            driver.FindElement(txtCompanyCity).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 5));
            // Select state
            WebDriverWaits.WaitUntilEleVisible(driver, comboCompanyState, 40);
            driver.FindElement(comboCompanyState).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 6));
            // Enter postal code
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyPostalCode, 10);
            driver.FindElement(txtCompanyPostalCode).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 14));

            //Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveCompanyL);
            driver.FindElement(btnSaveCompanyL).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnSaveIgnoreAlert, 10);
                driver.FindElement(btnCancelAlert).Click();
                driver.SwitchTo().DefaultContent();
                alertFound = true;
            }
            catch
            {

            }
            return alertFound;
        }

        By comboOficeCodeL = By.XPath("//label[text()='Office Code']/..//button");
        public void CreateNewCompanyLV(string file, int companyRow)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            // Enter company name
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName, 20);
            string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", companyRow, 2);
            driver.FindElement(txtCompanyName).SendKeys(companyNameExl);
            try
            {
                // for Houlihan Lockey Office Code
                WebDriverWaits.WaitUntilEleVisible(driver, comboOficeCodeL, 2);
                driver.FindElement(comboOficeCodeL).Click();
                By elmOfcCode = By.XPath("//label[text()='Office Code']/..//lightning-base-combobox-item//span[text()='AM']");

                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, elmOfcCode, 2);
                }
                catch
                {
                    driver.FindElement(comboOficeCodeL).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, elmOfcCode, 2);
                }
                CustomFunctions.MoveToElement(driver, driver.FindElement(elmOfcCode));
                driver.FindElement(elmOfcCode).Click();
            }
            catch
            {
                //Nothing to do
            }
            // Select country 
            WebDriverWaits.WaitUntilEleVisible(driver, comboCompanyCountry, 2);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtCompanyPostalCode));
            driver.FindElement(comboCompanyCountry).Click();
            string country = ReadExcelData.ReadData(excelPath, "Company", 3);
            By elmCountry = By.XPath($"//input[@name='country']/../../../..//lightning-base-combobox-item//span[@title='{country}']");

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, elmCountry, 2);
            }
            catch
            {
                driver.FindElement(comboCompanyCountry).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, elmCountry, 2);
            }
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmCountry));
            //Thread.Sleep(2000);
            driver.FindElement(elmCountry).Click();
            //Enter street 
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyStreet, 5);
            driver.FindElement(txtCompanyStreet).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 4));
            // Enter city
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyCity, 5);
            driver.FindElement(txtCompanyCity).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 5));
            // Select state
            driver.FindElement(comboCompanyState).Click();
            string state = ReadExcelData.ReadData(excelPath, "Company", 6);
            By elmState = By.XPath($"//input[@name='province']/../../../..//lightning-base-combobox-item//span[@title='{state}']");

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, elmState, 5);
            }
            catch
            {
                driver.FindElement(comboCompanyState).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, elmState, 5);
            }
            CustomFunctions.MoveToElement(driver, driver.FindElement(elmState));
            Thread.Sleep(2000);
            driver.FindElement(elmState).Click();

            // Enter postal code
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyPostalCode, 5);
            driver.FindElement(txtCompanyPostalCode).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 14));

            //Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveCompanyL, 5);
            driver.FindElement(btnSaveCompanyL).Click();
            By popDupRecordL = By.XPath("//h2[text()='Similar Records Exist']");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, popDupRecordL, 5);
                WebDriverWaits.WaitUntilEleVisible(driver, btnSaveCompanyL, 5);
                driver.FindElement(btnSaveCompanyL).Click();
            }
            catch { }
            try
            {
                this.ClickSaveIgnoreAlertButtonLV();
                driver.SwitchTo().DefaultContent();
            }
            catch
            {
                driver.SwitchTo().DefaultContent();
            }

            try
            {

                By closePopDuplicateL = By.XPath("//span[contains(@class,'toastMessage')]/../../../../..//button");
                WebDriverWaits.WaitUntilEleVisible(driver, popDuplicateL, 2);
                if(driver.FindElement(popDuplicateL).Text.Contains("duplicates exist"))
                {
                    driver.FindElement(closePopDuplicateL).Click();
                }

            }
            catch { }
        }

        public void AddCompanyLV(string file, int companyRow)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            // Enter company name
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName, 40);
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", companyRow, 2));
            // Select country 
            WebDriverWaits.WaitUntilEleVisible(driver, comboCompanyCountry, 40);
            driver.FindElement(comboCompanyCountry).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 3));
            //Enter street 
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyStreet, 40);
            driver.FindElement(txtCompanyStreet).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 4));
            // Enter city
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyCity, 40);
            driver.FindElement(txtCompanyCity).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 5));
            // Select state
            WebDriverWaits.WaitUntilEleVisible(driver, comboCompanyState, 40);
            driver.FindElement(comboCompanyState).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 6));

            // Enter postal code
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyPostalCode, 40);
            driver.FindElement(txtCompanyPostalCode).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 14));


            //Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
            driver.SwitchTo().DefaultContent();
        }

        public string GetSelectedCompanyTypeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, optionCompanyTypeL, 10);
            string selectedCompanyTypeSelected = driver.FindElement(optionCompanyTypeL).Text;
            return selectedCompanyTypeSelected;
        }
        public string GetCreateCompanyPageHeaderLV()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, newCompanyPageFrameL, 20);
            //driver.SwitchTo().Frame(driver.FindElement(newCompanyPageFrameL));
            WebDriverWaits.WaitUntilEleVisible(driver, txtCreateCompanyHeaderL, 20);
            string headingCompanyCreatePage = driver.FindElement(txtCreateCompanyHeaderL).Text;
            return headingCompanyCreatePage;
        }
        public string GetErrorMessageCreateCompanyPageLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, errmsgCompanyNameL, 10);
            return driver.FindElement(errmsgCompanyNameL).Text.Replace("\r\n", " ").Trim();
        }
        public void ClickSaveNewCompanyButtonLV()
        {
            //driver.SwitchTo().Frame(driver.FindElement(newCompanyPageFrameL));
            driver.FindElement(btnSaveCompanyL).Click();
        }
        public void ClickCancelNewCompanyButtonLV()
        {
            //driver.SwitchTo().Frame(driver.FindElement(newCompanyPageFrameL));
            driver.FindElement(btnCancelCompanyL).Click();
        }

        public void ClickSaveIgnoreAlertButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveIgnoreAlert, 10);
            driver.FindElement(btnSaveIgnoreAlert).Click();
        }

        public string GetCreateCompanyPageHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, headingCompanyCreate, 60);
            string headingCompanyCreatePage = driver.FindElement(headingCompanyCreate).Text;
            return headingCompanyCreatePage;
        }

        public string GetSelectedCompanyType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, selectedCompanyType, 60);
            string selectedCompanyTypeSelected = driver.FindElement(selectedCompanyType).Text;
            return selectedCompanyTypeSelected;
        }

        public void AddCompany(string file, int companyRow)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            // Enter company name
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName, 40);
            driver.FindElement(txtCompanyName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", companyRow, 2));
            // Select country 
            WebDriverWaits.WaitUntilEleVisible(driver, comboCompanyCountry, 40);
            driver.FindElement(comboCompanyCountry).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 3));
            //Enter street 
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyStreet, 40);
            driver.FindElement(txtCompanyStreet).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 4));
            // Enter city
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyCity, 40);
            driver.FindElement(txtCompanyCity).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 5));
            // Select state
            WebDriverWaits.WaitUntilEleVisible(driver, comboCompanyState, 40);
            driver.FindElement(comboCompanyState).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 6));

            // Enter postal code
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyPostalCode, 40);
            driver.FindElement(txtCompanyPostalCode).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 14));


            //Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }

        public void ClickSaveIgnoreAlertButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveIgnoreAlert);
            driver.FindElement(btnSaveIgnoreAlert).Click();
        }

        public string errmsgCompanyName()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
            string errmsg = driver.FindElement(errmsgCmpanyName).Text;
            return errmsg;
        }
    }
}