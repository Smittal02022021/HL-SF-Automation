using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Threading;

namespace SF_Automation.Pages.Company
{
    class CompanyEditPage : BaseClass
    {
        By comboCompanySubType = By.CssSelector("select[id='acc6']");
        By comboOwnership = By.CssSelector("select[id='acc14']");
        By txtParentCompany = By.CssSelector("input[id='acc3']");
        By selAvailableIndustryFocus = By.CssSelector("select[id*='00Ni000000D9WGg_unselected'] > optgroup > option:nth-child(1)");
        By btnArrowRightIndustryFocus = By.CssSelector("img[id='00Ni000000D9WGg_right_arrow']");
        By selAvailableDealPreference = By.CssSelector("select[id*='00Ni000000D9WG2_unselected'] > optgroup > option:nth-child(1)");
        By btnArrowRightDealPreference = By.CssSelector("img[id='00Ni000000D9WG2_right_arrow']");
        By selAvailableGeographicalFocus = By.CssSelector("select[id='00Ni000000DvG7n_unselected'] > optgroup > option:nth-child(1)");
        By btnArrowRightGeographicalFocus = By.CssSelector("img[id='00Ni000000DvG7n_right_arrow']");
        By txtDescription = By.CssSelector("textarea[id='acc20']");
        By txtCapIQCompany = By.CssSelector("input[id='CF00Ni000000DvFoM']");
        By btnSave = By.CssSelector("td[id='bottomButtonRow'] > input[value=' Save ']");
        By valEditPageHeading = By.CssSelector("h2[class='mainTitle']");
        By btnEditCompanyDetail = By.CssSelector("td[id='topButtonRowj_id0_j_id1'] > input[name='edit']");
        By txtPhone = By.CssSelector("input[id='acc10']");
        By txtStreet = By.CssSelector("textarea[id='acc17street']");
        By txtCity = By.CssSelector("input[id='acc17city']");
        By comboState = By.CssSelector("select[id='acc17state']");
        By txtZipPostal = By.CssSelector("input[id='acc17zip']");
        By txtCountry = By.CssSelector("select[id='acc17country']");
        By btnEdit = By.CssSelector("td[id*='topButtonRow'] >input[value=' Edit ']");
        By chkBoxHQ = By.CssSelector("input[id='00N3100000GyJM5']");
        By valErrorMsgHQNotTrue = By.CssSelector("td[class='dataCol col02'] > div[class='errorMsg']");
        By valCompanyType = By.XPath("//span[@id='Account.RecordType-_help']/following::td[1]");
        By errMsgChangeCompanyType = By.CssSelector("div[id='errorDiv_ep']");
        By valCompanyName = By.CssSelector("input[id='acc2']");
        By linkDateERPSubmittedToSync = By.XPath("//*[text()='ERP Submitted To Sync']/../../../td[2]/span/span/a");

        By btnEditL = By.XPath("//button[@name='Edit']");
        By comboCountryL = By.XPath("//label[text()='Country']/..//input");
        By inputStreetL = By.XPath("//label[text()='Street']/..//textarea");
        By inputCityL = By.XPath("//label[text()='City']/..//input");
        By comboStateL = By.XPath("//label[text()='State/Province']/..//input");
        By inputPostalCodeL = By.XPath("//label[text()='Zip/Postal Code']/..//input");
        By inputDescL = By.XPath("//label[text()='Description']/..//textarea");
        By btnSaveL = By.XPath("//button[@name='SaveEdit']");
        By headerCDataL = By.XPath("//h3/span[@title='Connected Data']");
        By valEditPageHeadingL = By.XPath("//div[contains(@class,'header-container')]//h2[contains(@class,'header')]");

        By comboCmpnySubTypeL=By.XPath("//button[@aria-label='Company Sub Type']");
        By comboOwnershipL = By.XPath("//button[@aria-label='Ownership']");
        By comboIGL = By.XPath("//button[@aria-label='Industry Group']");
        By comboSectorL = By.XPath("//button[@aria-label='Sector']");
        By headingAFL = By.XPath("(//h3//span[@title='Annual Financials'])[2]");
        By inputDscL = By.XPath("//label[text()='Description']/..//textarea");

        public void EditCompanyDetailsLV(string file, string companyType)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            if (companyType== "Operating Company")
            {
                // Enter company sub type
                WebDriverWaits.WaitUntilEleVisible(driver, comboCmpnySubTypeL, 10);
                driver.FindElement(comboCmpnySubTypeL).Click();
                string valCmpnySubType= ReadExcelData.ReadData(excelPath, "Company", 9);
                By comboOptionCmpnySubType = By.XPath($"//lightning-base-combobox-item//span[2]/span[@title='{valCmpnySubType}']");
                CustomFunctions.MoveToElement(driver, driver.FindElement(comboOptionCmpnySubType));
                WebDriverWaits.WaitUntilEleVisible(driver, comboOptionCmpnySubType, 10);
                driver.FindElement(comboOptionCmpnySubType).Click();

                //Enter ownership detail
                WebDriverWaits.WaitUntilEleVisible(driver, comboOwnershipL, 40);
                driver.FindElement(comboOwnershipL).Click();
                string valOwnershipL = ReadExcelData.ReadData(excelPath, "Company", 10);
                By comboOptionOwnership = By.XPath($"//lightning-base-combobox-item//span[2]/span[@title='{valOwnershipL}']");
                CustomFunctions.MoveToElement(driver, driver.FindElement(comboOptionOwnership));
                WebDriverWaits.WaitUntilEleVisible(driver, comboOptionOwnership, 10);
                driver.FindElement(comboOptionOwnership).Click();

                //Enter Parent company
                //WebDriverWaits.WaitUntilEleVisible(driver, txtParentCompany, 40);
                //driver.FindElement(txtParentCompany).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 11));

                // Select value from industry focus
                WebDriverWaits.WaitUntilEleVisible(driver, comboIGL, 10);
                CustomFunctions.MoveToElement(driver, driver.FindElement(comboIGL));
                driver.FindElement(comboIGL).Click();
                string valIG = ReadExcelData.ReadData(excelPath, "Company", 15);
                By comboOptionIG = By.XPath($"//lightning-base-combobox-item//span[2]/span[@title='{valIG}']");
                CustomFunctions.MoveToElement(driver, driver.FindElement(comboOptionIG));
                WebDriverWaits.WaitUntilEleVisible(driver, comboOptionIG, 10);
                driver.FindElement(comboOptionIG).Click();

                //Select Sector
                WebDriverWaits.WaitUntilEleVisible(driver, comboSectorL, 10);
                CustomFunctions.MoveToElement(driver, driver.FindElement(headingAFL));
                driver.FindElement(comboSectorL).Click();
                string valSector = ReadExcelData.ReadData(excelPath, "Company", 16);
                By comboOptionSector = By.XPath($"//lightning-base-combobox-item//span[2]/span[@title='{valSector}']");
                CustomFunctions.MoveToElement(driver, driver.FindElement(comboOptionSector));
                WebDriverWaits.WaitUntilEleVisible(driver, comboOptionSector, 10);
                driver.FindElement(comboOptionSector).Click();

                // Select value from Deal Preference
                //WebDriverWaits.WaitUntilEleVisible(driver, selAvailableDealPreference, 40);
                //driver.FindElement(selAvailableDealPreference).Click();
                //WebDriverWaits.WaitUntilEleVisible(driver, btnArrowRightDealPreference, 40);
                //driver.FindElement(btnArrowRightDealPreference).Click();

                // Select value from Geographical Preference
                //WebDriverWaits.WaitUntilEleVisible(driver, selAvailableGeographicalFocus, 40);
                //driver.FindElement(selAvailableGeographicalFocus).Click();
                //WebDriverWaits.WaitUntilEleVisible(driver, btnArrowRightGeographicalFocus, 40);
                //driver.FindElement(btnArrowRightGeographicalFocus).Click();
            }
            //Enter Description
            string valDesc = ReadExcelData.ReadData(excelPath, "Company", 12);
            WebDriverWaits.WaitUntilEleVisible(driver, inputDscL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(inputDscL));
            driver.FindElement(inputDscL).SendKeys(valDesc);

            //Enter CapIQ company
            //WebDriverWaits.WaitUntilEleVisible(driver, txtCapIQCompany, 40);
            //driver.FindElement(txtCapIQCompany).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 13));

            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 10);
            driver.FindElement(btnSaveL).Click();
        }
        //Get heading of company edit page details 
        public string GetCompanyEditPageHeadingLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEditPageHeadingL, 20);
            string editPageHeading = driver.FindElement(valEditPageHeadingL).Text;
            return editPageHeading;
        }

        public void UpdateExistingCompanyDetailsLV(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL);
            driver.FindElement(btnEditL).Click();

            //Enter updated country
            WebDriverWaits.WaitUntilEleVisible(driver, comboCountryL, 20);
            IWebElement headerCData = driver.FindElement(headerCDataL);
            CustomFunctions.MoveToElement(driver, headerCData);
            driver.FindElement(comboCountryL).Click();
            string countryNameExl= ReadExcelData.ReadData(excelPath, "Company", 5);
            By eleComboCountryOption = By.XPath($"//lightning-base-combobox-item//span/span[@title='{countryNameExl}']");////label[text()='Country']/..
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleComboCountryOption));
            driver.FindElement(eleComboCountryOption).Click();

            //Enter updated street 
            WebDriverWaits.WaitUntilEleVisible(driver, inputStreetL, 5);
            driver.FindElement(inputStreetL).Clear();
            string txtStreetExl = ReadExcelData.ReadData(excelPath, "Company", 6);
            driver.FindElement(inputStreetL).SendKeys(txtStreetExl);

            //Enter updated city
            WebDriverWaits.WaitUntilEleVisible(driver, inputCityL, 5);
            driver.FindElement(inputCityL).Clear();
            string txtCityExl = ReadExcelData.ReadData(excelPath, "Company", 7);
            driver.FindElement(inputCityL).SendKeys(txtCityExl);

            //Enter updated state
            WebDriverWaits.WaitUntilEleVisible(driver, comboStateL, 5);
            driver.FindElement(comboStateL).Click();
            string stateNameExl = ReadExcelData.ReadData(excelPath, "Company", 8);
            By eleComboStateOption = By.XPath($"//lightning-base-combobox-item//span/span[@title='{stateNameExl}']");////label[text()='Country']/..
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleComboStateOption));
            //Thread.Sleep(2000);
            driver.FindElement(eleComboStateOption).Click();

            //Enter updated zipPostalCode
            WebDriverWaits.WaitUntilEleVisible(driver, inputPostalCodeL, 5);
            driver.FindElement(inputPostalCodeL).Clear();
            string txtPostalCodeExl = ReadExcelData.ReadData(excelPath, "Company", 9);
            driver.FindElement(inputPostalCodeL).SendKeys(txtPostalCodeExl);

            //Enter updated description
            CustomFunctions.MoveToElement(driver, driver.FindElement(inputDescL));
            WebDriverWaits.WaitUntilEleVisible(driver, inputDescL, 10);
            driver.FindElement(inputDescL).Clear();
            string descriptionExl = ReadExcelData.ReadData(excelPath, "Company", 10);
            driver.FindElement(inputDescL).SendKeys(descriptionExl);

            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 5);
            driver.FindElement(btnSaveL).Click();
        }

        public string GetCompanyNameEditPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyName, 60);
            string CompanyNameFromEditPage = driver.FindElement(valCompanyName).Text;
            return CompanyNameFromEditPage;
        }
        public void EditCompanyDetails(string file, string companyType)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            if (companyType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1)))
            {
                // Enter company sub type
                WebDriverWaits.WaitUntilEleVisible(driver, comboCompanySubType, 40);
                driver.FindElement(comboCompanySubType).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 9));

                //Enter ownership detail
                WebDriverWaits.WaitUntilEleVisible(driver, comboOwnership, 40);
                driver.FindElement(comboOwnership).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 10));

                //Enter Parent company
                //WebDriverWaits.WaitUntilEleVisible(driver, txtParentCompany, 40);
                //driver.FindElement(txtParentCompany).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 11));

                // Select value from industry focus
                WebDriverWaits.WaitUntilEleVisible(driver, selAvailableIndustryFocus, 40);
                driver.FindElement(selAvailableIndustryFocus).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnArrowRightIndustryFocus, 40);
                driver.FindElement(btnArrowRightIndustryFocus).Click();

                // Select value from Deal Preference
                WebDriverWaits.WaitUntilEleVisible(driver, selAvailableDealPreference, 40);
                driver.FindElement(selAvailableDealPreference).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnArrowRightDealPreference, 40);
                driver.FindElement(btnArrowRightDealPreference).Click();

                // Select value from Geographical Preference
                WebDriverWaits.WaitUntilEleVisible(driver, selAvailableGeographicalFocus, 40);
                driver.FindElement(selAvailableGeographicalFocus).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnArrowRightGeographicalFocus, 40);
                driver.FindElement(btnArrowRightGeographicalFocus).Click();
            }
            //Enter Description
            WebDriverWaits.WaitUntilEleVisible(driver, txtDescription, 40);
            driver.FindElement(txtDescription).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 12));

            //Enter CapIQ company
            //WebDriverWaits.WaitUntilEleVisible(driver, txtCapIQCompany, 40);
            //driver.FindElement(txtCapIQCompany).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 13));

            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();
        }

        //Get heading of company edit page details 
        public string GetCompanyEditPageHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEditPageHeading, 60);
            string editPageHeading = driver.FindElement(valEditPageHeading).Text;
            return editPageHeading;
        }

        //Edit company detail
        public void UpdateExistingCompanyDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditCompanyDetail);
            driver.FindElement(btnEditCompanyDetail).Click();

            //Enter updated country
            WebDriverWaits.WaitUntilEleVisible(driver, txtCountry, 40);
            driver.FindElement(txtCountry).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 5));

            //Enter updated street 
            WebDriverWaits.WaitUntilEleVisible(driver, txtStreet, 40);
            driver.FindElement(txtStreet).Clear();
            driver.FindElement(txtStreet).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 6));

            //Enter updated city
            WebDriverWaits.WaitUntilEleVisible(driver, txtCity, 40);
            driver.FindElement(txtCity).Clear();
            driver.FindElement(txtCity).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 7));

            //Enter updated state
            WebDriverWaits.WaitUntilEleVisible(driver, comboState, 40);
            driver.FindElement(comboState).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 8));

            //Enter updated zipPostalCode
            WebDriverWaits.WaitUntilEleVisible(driver, txtZipPostal, 40);
            driver.FindElement(txtZipPostal).Clear();
            driver.FindElement(txtZipPostal).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 9));

            //Enter updated description
            WebDriverWaits.WaitUntilEleVisible(driver, txtDescription, 40);
            driver.FindElement(txtDescription).Clear();
            driver.FindElement(txtDescription).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 10));

            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();
        }

        public void ValidateUpdatingParentCompanyOnly(string file)
        {
            //Click edit button
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 40);
            driver.FindElement(btnEdit).Click();
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string checkBoxValue = driver.FindElement(chkBoxHQ).GetAttribute("checked");
            if (checkBoxValue == null)
            {
                Console.WriteLine("HQ checkbox not checked ");
            }
            else if (checkBoxValue.Equals("true"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, chkBoxHQ, 40);
                driver.FindElement(chkBoxHQ).Click();
            }
            //Enter Parent company
            WebDriverWaits.WaitUntilEleVisible(driver, txtParentCompany, 40);
            driver.FindElement(txtParentCompany).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 4));

            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();
        }

        public void ValidateCheckingHQCheckBoxOnly(string file)
        {
            //Click edit button
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 40);
            driver.FindElement(btnEdit).Click();
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            //Clear Parent company
            WebDriverWaits.WaitUntilEleVisible(driver, txtParentCompany, 40);
            driver.FindElement(txtParentCompany).Clear();

            // Click Checkbox HQ
            string checkBoxValue = driver.FindElement(chkBoxHQ).GetAttribute("checked");
            if (checkBoxValue != ("true"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, chkBoxHQ, 40);
                driver.FindElement(chkBoxHQ).Click();
            }
            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();
        }

        public void ValidateErrorHQCanBeTrue(string file)
        {
            //Click edit button
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 40);
            driver.FindElement(btnEdit).Click();
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            // Click Checkbox HQ
            string checkBoxValue = driver.FindElement(chkBoxHQ).GetAttribute("checked");
            if (checkBoxValue != ("true"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, chkBoxHQ, 40);
                driver.FindElement(chkBoxHQ).Click();
            }
            //Add Parent company
            WebDriverWaits.WaitUntilEleVisible(driver, txtParentCompany, 40);
            driver.FindElement(txtParentCompany).Clear();
            driver.FindElement(txtParentCompany).SendKeys(ReadExcelData.ReadData(excelPath, "Company", 4));

            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();
        }

        //Error msg for HQ no true
        public string GetErrorMsgHQNoTrue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valErrorMsgHQNotTrue, 60);
            string errMsgHQNotTrue = driver.FindElement(valErrorMsgHQNotTrue).Text;
            return errMsgHQNotTrue;
        }

        //Error msg for company record type change
        public string GetErrorMsgForCompanyRecordTypeChange()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, errMsgChangeCompanyType, 60);
            string errMsgOnChangeCompanyType = driver.FindElement(errMsgChangeCompanyType).Text;
            return errMsgOnChangeCompanyType;
        }


        public string GetCompanyType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyType, 60);
            string companyType = driver.FindElement(valCompanyType).Text;
            return companyType;
        }
        public void UpdateERPSubmittedToSyncField()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkDateERPSubmittedToSync, 40);
            driver.FindElement(linkDateERPSubmittedToSync).Click();

            //Click Save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 40);
            driver.FindElement(btnSave).Click();
        }
    }
}
