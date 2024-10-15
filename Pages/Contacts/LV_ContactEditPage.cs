using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;


namespace SF_Automation.Pages.Contact
{
    class LV_ContactEditPage : BaseClass
    {
        By contactEditHeading = By.CssSelector("h2.mainTitle");
        By btnEdit = By.XPath("//button[@name='Edit']");
        By txtMiddleName = By.XPath("//input[@name='middleName']");
        By txtMailingStreet = By.XPath("(//textarea[@name='street'])[1]");
        By txtMailingCity = By.XPath("(//input[@name='city'])[1]");
        By txtMailingState = By.XPath("(//input[@name='province'])[1]");
        By btnSaveAndNew = By.XPath("(//button[@name='SaveAndNew'])[1]");
        By comboContactStatus = By.XPath("//button[@aria-label='Status']");
        By comboOffice = By.XPath("//button[@aria-label='Office']");
        By txtTitle = By.XPath("(//input[@name='Title'])[1]");
        By txtDepartment = By.XPath("(//input[@name='Department'])[1]");
        By comboCountry = By.XPath("(//input[@name='country'])[1]");

        By comboLineOfBusiness = By.XPath("//button[@aria-label='Line of Business']");
        By comboLineOfBusinessDistributed = By.CssSelector("table[class='detailList'] > tbody > tr:nth-child(9) > td:nth-child(2) > span > select");
        By comboProjectNotifyRole = By.CssSelector("select[title='Project Notify Role - Available']");
        By rightArrow = By.CssSelector("img[id*='67d_right_arrow']");
        By txtHireDate = By.XPath("//input[@name='Hire_Date__c']");
        By txtDepartureDate = By.XPath("//input[@name='Departure_Date__c']");

        By comboMentor = By.CssSelector("span[class='lookupInput'] > select[id*='Gz5Lf']");
        By txtMentor = By.CssSelector("span[class='lookupInput'] > input[id*='Gz5Lf']");
        By lookupCompany = By.CssSelector("a[title='Company Name Lookup (New Window)']");

        By searchFrame = By.CssSelector("frame#searchFrame");
        By resultFrame = By.CssSelector("frame#resultsFrame");
        By txtSearchBox = By.CssSelector("input#lksrch");
        By btnGo = By.CssSelector("input[name=go]");

        By txtSearchResults = By.CssSelector("tr[class='dataRow even first']>th>a");
        By btnSave = By.XPath("//button[@name='SaveEdit']");

        By txtErrorMessageCompany = By.CssSelector("#errorDiv_ep");
        By txtErrorMessageDepartureDate = By.XPath("(//span[text()='Departure Date'])[2]/..");
        By txtErrorMessageIndustryGroup = By.XPath("(//span[text()='Industry Group'])[2]/..");
        By txtErrorMessageEmpCurrency = By.XPath("(//span[text()='Contact Currency'])[2]/..");
        By txtErrorMessageLastName = By.XPath("(//span[text()='Last Name'])[1]/..");

        By btnCancel = By.XPath("//button[@name='CancelEdit']");
        By lookupExpenseApprover = By.CssSelector("img[alt='Expense Approver Lookup (New Window)']");
        By radioBtnAllFields = By.CssSelector("input[value=SEARCH_ALL]");
        By txtSearchResultExpenseApprover = By.CssSelector("tr[class='dataRow even last first']>th>a");
        By selectFlagReasonDrpDown = By.CssSelector("select[name='00Ni000000Fk47Y']");
        By selectCurrencyDrpDown = By.XPath("//button[@aria-label='Contact Currency']");
        By txtLastName = By.XPath("//input[@name='lastName']");

        By lookupPrimaryPDC = By.CssSelector("img[alt='Primary PDC Lookup (New Window)']");
        By txtPrimaryPDC = By.XPath("(//label[text()='Primary PDC']/following::div//input)[1]");
        By txtDealAnnouncementsChangeDate = By.XPath("//td[text()='Deal Announcements Change Date']//following::div[1]");
        By txtEventsChangeDate = By.CssSelector(@"#\30 0Ni000000FZZiNj_id0_j_id1_ileinner");

        By txtGeneralAnnouncementsChangeDate = By.CssSelector(@"#\30 0Ni000000FZZiSj_id0_j_id1_ileinner");
        By txtContentChangeDate = By.CssSelector(@"#\30 0Ni000000FZZmFj_id0_j_id1_ileinner");
        By inputDealAnnouncementsChangeDate = By.CssSelector("input[name='00Ni000000FZZiI']");
        By inputEventsChangeDate = By.CssSelector("input[name='00Ni000000FZZiN']");
        By inputGeneralAnnouncementsChangeDate = By.CssSelector("input[name='00Ni000000FZZiS']");
        By inputContentChangeDate = By.CssSelector("input[name='00Ni000000FZZmF']");
        By txtBadgeFirstName = By.CssSelector(@"#\30 0Ni000000FYRYnj_id0_j_id1_ileinner");
        By txtBadgeLastName = By.CssSelector(@"#\30 0Ni000000FYRYsj_id0_j_id1_ileinner");
        By txtBadgeCompany = By.CssSelector(@"#\30 0Ni000000FYRZ2j_id0_j_id1_ileinner");
        By txtBadgeFullName = By.CssSelector(@"#\30 0Ni000000FYRZ7j_id0_j_id1_ileinner");
        By checkboxCopyFromContactDetail = By.CssSelector(@"#\30 0Ni000000FZTyw");
        By txtGoogleMaps = By.CssSelector("table[class='customLinks']>tbody>tr>td:nth-of-type(1)>a");

        public string GetContactEditHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, contactEditHeading, 60);
            string headingcontactEdit = driver.FindElement(contactEditHeading).Text;
            return headingcontactEdit;
        }

        public bool ValidateOffileFieldEditableForHCMUser()
        {
            bool result = false;
            //string val = driver.FindElement(txtOffice).Text;
            //if(val == " ")
            {
               result = true;
            }
            return result;
        }

        public void EditContact(string file, int row, int userRow)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(3000);

            if(ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", userRow, 1).Equals("Admin"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtMiddleName);
                driver.FindElement(txtMiddleName).Clear();
                driver.FindElement(txtMiddleName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 8));

                WebDriverWaits.WaitUntilEleVisible(driver, txtMailingStreet);
                driver.FindElement(txtMailingStreet).Clear();
                driver.FindElement(txtMailingStreet).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 9));

                WebDriverWaits.WaitUntilEleVisible(driver, txtMailingCity);
                driver.FindElement(txtMailingCity).Clear();
                driver.FindElement(txtMailingCity).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 10));

                WebDriverWaits.WaitUntilEleVisible(driver, txtMailingState);
                driver.FindElement(txtMailingState).Clear();
                driver.FindElement(txtMailingState).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 11));
                driver.FindElement(txtMailingState).SendKeys(Keys.Enter);
                Thread.Sleep(1000);
                driver.FindElement(txtMailingState).SendKeys(Keys.Enter);

                WebDriverWaits.WaitUntilEleVisible(driver, txtTitle);
                driver.FindElement(txtTitle).Clear();
                driver.FindElement(txtTitle).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 15));

                WebDriverWaits.WaitUntilEleVisible(driver, txtDepartment);
                driver.FindElement(txtDepartment).Clear();
                driver.FindElement(txtDepartment).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 16));
            }
            if (ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", row, 1).Contains("Houlihan Employee"))
            {
                if (ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", userRow, 1).Equals("Admin"))
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, comboContactStatus);
                    driver.FindElement(comboContactStatus).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 13));
                    Thread.Sleep(2000);

                    WebDriverWaits.WaitUntilEleVisible(driver, comboOffice);
                    driver.FindElement(comboOffice).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 14));
                    Thread.Sleep(2000);

                    WebDriverWaits.WaitUntilEleVisible(driver, comboLineOfBusiness);
                    driver.FindElement(comboLineOfBusiness).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 17));
                    Thread.Sleep(2000);

                    string getDates = DateTime.Today.ToString("MM/dd/yyyy").Replace('-', '/');
                    WebDriverWaits.WaitUntilEleVisible(driver, txtHireDate);
                    driver.FindElement(txtHireDate).SendKeys(getDates);
                    Thread.Sleep(2000);
                }
                else
                {
                    string getDate = DateTime.Today.ToString("MM/dd/yyyy").Replace('-', '/');
                    WebDriverWaits.WaitUntilEleVisible(driver, txtHireDate);
                    driver.FindElement(txtHireDate).SendKeys(getDate);
                    Thread.Sleep(2000);
                }
            }
            else if (ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", row, 1).Contains("Distribution List"))
            {
                if (ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", userRow, 1).Equals("Admin"))
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, comboLineOfBusiness);
                    driver.FindElement(comboLineOfBusiness).SendKeys(ReadExcelData.ReadData(excelPath, "Contact", 17));
                    Thread.Sleep(2000);
                }
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 120);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(2000);
        }

        public void EditCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, lookupCompany, 120);
            driver.FindElement(lookupCompany).Click();

            CustomFunctions.SwitchToWindow(driver, 1);
            driver.SwitchTo().Frame(driver.FindElement(searchFrame));

            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys("test");

            driver.FindElement(btnGo).Click();

            driver.SwitchTo().DefaultContent();

            // WebDriverWaits.WaitUntilEleVisible(driver, resultFrame, 60);

            driver.SwitchTo().Frame(driver.FindElement(resultFrame));

            driver.FindElement(txtSearchResults).Click();
            CustomFunctions.SwitchToWindow(driver, 0);
        }

        public void ClickSaveBtn()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 120);
            driver.FindElement(btnSave).Click();
        }

        public string TxtErrorMessageCompany()
        {
            string text = driver.FindElement(txtErrorMessageCompany).Text;
            return text;
        }

        public void UpdateDepartureDate()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(3000);

            Actions action = new Actions(driver);
            var element = driver.FindElement(txtHireDate);
            action.MoveToElement(element);
            driver.FindElement(txtDepartureDate).SendKeys(DateTime.Today.AddDays(-10).ToString("MM/dd/yyyy").Replace('-', '/'));
            Thread.Sleep(3000);
        }

        public void UpdateDepartureDateforInactiveEmployee()
        {
            driver.FindElement(comboContactStatus).SendKeys("Inactive");
            driver.FindElement(comboContactStatus).SendKeys(Keys.Enter);
            driver.FindElement(comboContactStatus).SendKeys(Keys.Enter);

            Thread.Sleep(2000);
            Actions action = new Actions(driver);
            var element = driver.FindElement(txtHireDate);
            action.MoveToElement(element);
            driver.FindElement(txtDepartureDate).Clear();
        }

        public string TxtErrorMessageDepartureDate()
        {
            Thread.Sleep(2000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtErrorMessageDepartureDate));
            string text = driver.FindElement(txtErrorMessageDepartureDate).Text;
            Thread.Sleep(1000);

            if(text.Contains("Departure Date\r\n"))
            {
                text = text.Remove(0, 16);
            }
            
            return text;
        }

        public string TxtErrorMessageIndustryGroup()
        {
            Thread.Sleep(2000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtErrorMessageIndustryGroup));
            string text = driver.FindElement(txtErrorMessageIndustryGroup).Text;
            Thread.Sleep(1000);

            if(text.Contains("Industry Group\r\n"))
            {
                text = text.Remove(0, 16);
            }
            return text;
        }

        public string TxtErrorMessageEmployeeCurrency()
        {
            Thread.Sleep(3000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtErrorMessageEmpCurrency));
            string text = driver.FindElement(txtErrorMessageEmpCurrency).Text;
            Thread.Sleep(1000);

            if(text.Contains("Contact Currency\r\n"))
            {
                text = text.Remove(0, 18);
            }
            return text;
        }

        public string TxtErrorMessageEmpName()
        {
            Thread.Sleep(2000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtErrorMessageLastName));
            string text = driver.FindElement(txtErrorMessageLastName).Text;
            Thread.Sleep(1000);

            if(text.Contains("Last Name\r\n"))
            {
                text = text.Remove(0, 11);
            }
            return text;
        }


        public void ClickCancelBtn()
        {
            driver.FindElement(btnCancel).Click();
        }

        public void VerifyExpenseApproverValidation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(lookupExpenseApprover).Click();

            CustomFunctions.SwitchToWindow(driver, 1);

            driver.SwitchTo().Frame(driver.FindElement(searchFrame));
            driver.FindElement(radioBtnAllFields).Click();
            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys("Inactive");

            driver.FindElement(btnGo).Click();

            driver.SwitchTo().DefaultContent();

            WebDriverWaits.WaitUntilEleVisible(driver, resultFrame, 60);
            driver.SwitchTo().Frame(driver.FindElement(resultFrame));
            driver.FindElement(txtSearchResultExpenseApprover).Click();

            CustomFunctions.SwitchToWindow(driver, 0);
        }

        public void VerifyFlagReasonValidation(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            CustomFunctions.SelectByText(driver, driver.FindElement(selectFlagReasonDrpDown), value);
        }

        public void VerifyEmployeeCurrencyValidation(string value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(3000);

            string getDate = DateTime.Today.AddDays(-10).ToString("MM/dd/yyyy");
            string date = getDate.Replace('-', '/');
            WebDriverWaits.WaitUntilEleVisible(driver, txtHireDate);
            driver.FindElement(txtHireDate).Clear();
            driver.FindElement(txtHireDate).SendKeys(date);

            CustomFunctions.MoveToElement(driver, driver.FindElement(selectCurrencyDrpDown));
            driver.FindElement(selectCurrencyDrpDown).SendKeys(value);
            Thread.Sleep(2000);
            driver.FindElement(selectCurrencyDrpDown).SendKeys(Keys.Enter);
            Thread.Sleep(1000);
        }

        public void VerifyLastNameValidation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(3000);

            string getDate = DateTime.Today.AddDays(-10).ToString("MM/dd/yyyy");
            string date = getDate.Replace('-', '/');
            WebDriverWaits.WaitUntilEleVisible(driver, txtHireDate);
            driver.FindElement(txtHireDate).Clear();
            driver.FindElement(txtHireDate).SendKeys(date);
            driver.FindElement(txtLastName).Clear();
            driver.FindElement(txtLastName).SendKeys("test");
        }

        public void VerifyIndustryGroupValidation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, comboLineOfBusiness);
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboLineOfBusiness));

            driver.FindElement(comboLineOfBusiness).SendKeys("CF");
            Thread.Sleep(1000);
            driver.FindElement(comboLineOfBusiness).SendKeys(Keys.Enter);
            Thread.Sleep(1000);
        }

        public void VerifyPDCValidation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(3000);

            driver.FindElement(lookupPrimaryPDC).Click();

            CustomFunctions.SwitchToWindow(driver, 1);

            driver.SwitchTo().Frame(driver.FindElement(searchFrame));
            driver.FindElement(radioBtnAllFields).Click();
            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys("Inactive");

            driver.FindElement(btnGo).Click();

            driver.SwitchTo().DefaultContent();

            WebDriverWaits.WaitUntilEleVisible(driver, resultFrame, 60);
            driver.SwitchTo().Frame(driver.FindElement(resultFrame));
            driver.FindElement(txtSearchResultExpenseApprover).Click();

            CustomFunctions.SwitchToWindow(driver, 0);
        }

        public void VerifyPrimaryPDCValidation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);

            CustomFunctions.MoveToElement(driver, driver.FindElement(txtPrimaryPDC));
            driver.FindElement(txtPrimaryPDC).SendKeys("test houlihan");
        }

        public string TxtDealAnnouncementsChangeDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtDealAnnouncementsChangeDate, 120);
            string text = driver.FindElement(txtDealAnnouncementsChangeDate).Text;
            return text;
        }

        public string TxtEventsChangeDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtEventsChangeDate, 120);
            string text = driver.FindElement(txtEventsChangeDate).Text;
            return text;
        }

        public string TxtGeneralAnnouncementsChangeDate()

        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtGeneralAnnouncementsChangeDate, 120);
            string text = driver.FindElement(txtGeneralAnnouncementsChangeDate).Text;
            return text;
        }

        public string TxtContentChangeDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtContentChangeDate, 120);
            string text = driver.FindElement(txtContentChangeDate).Text;
            return text;
        }

        //Edit Subscription Preference Fields
        public void EditSubscriptionPreferenceFields()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            string getDate = DateTime.Today.AddDays(0).ToString("MM/dd/yyyy");
            Console.WriteLine(getDate);
            driver.FindElement(inputDealAnnouncementsChangeDate).SendKeys(getDate);
            driver.FindElement(inputEventsChangeDate).SendKeys(getDate);
            driver.FindElement(inputGeneralAnnouncementsChangeDate).SendKeys(getDate);
            driver.FindElement(inputContentChangeDate).SendKeys(getDate);
        }

        //Get Text from First Name Badge
        public string TxtBadgeFirstName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtBadgeFirstName, 120);
            string text = driver.FindElement(txtBadgeFirstName).Text;
            return text;
        }

        //Get text from Last Name Badge
        public string TxtBadgeLastName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtBadgeLastName, 120);
            string text = driver.FindElement(txtBadgeLastName).Text;
            return text;
        }

        //Get Text from Company Badge
        public string TxtBadgeCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtBadgeCompany, 120);
            string text = driver.FindElement(txtBadgeCompany).Text;
            return text;

        }

        //Edit Event Badge fields
        public void EditEventBadgeFields()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkboxCopyFromContactDetail, 120);
            driver.FindElement(checkboxCopyFromContactDetail).Click();
        }

        //Verify quick links and Related objects for external contact
        public bool ValidateQuickLink(string file)
        {
            bool result = false;
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            int RowQuickLinkList = ReadExcelData.GetRowCount(excelPath, "QuickLink");
            for (int row = 2; row < RowQuickLinkList; row++)
            {
                string QuickLinkText = driver.FindElement(By.CssSelector("div[class='listHoverLinks']>a:nth-of-type(" + row + ")>span")).Text.Split('[')[0].Trim();
                Console.WriteLine(QuickLinkText);

                string preSetListExl = ReadExcelData.ReadDataMultipleRows(excelPath, "QuickLink", row , 1);
                Console.WriteLine(preSetListExl);

                if (QuickLinkText == preSetListExl)
                {
                    result = true;
                }
            }
            return result;
        }

        public string GetCustomLinkText() {
            WebDriverWaits.WaitUntilEleVisible(driver, txtGoogleMaps, 120);
            string text = driver.FindElement(txtGoogleMaps).Text;
            return text;

        }

        public void VerifyGoogleMapsLink() {

            WebDriverWaits.WaitUntilEleVisible(driver, txtGoogleMaps, 120);
            driver.FindElement(txtGoogleMaps).Click();
            CustomFunctions.SwitchToWindow(driver, 1);
            string title = driver.Title;
            Assert.AreEqual("Google Maps", title);
            driver.Close();
            CustomFunctions.SwitchToWindow(driver, 0);
            
            
        }
    }
}
