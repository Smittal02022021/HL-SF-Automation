using SF_Automation.UtilityFunctions;
using OpenQA.Selenium;
using SF_Automation.TestData;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using NUnit.Framework;
using SF_Automation.Pages.Common;

namespace SF_Automation.Pages.Contact
{
    class ContactSelectRecordPage : BaseClass
    {
        By drpdwnSelectRecordType = By.CssSelector("select[id='p3']");
        By btnContinue = By.CssSelector("input[title='Continue']");
        By txtTypeL = By.XPath("//div[@class='changeRecordTypeCenter']//label//span[2]");
        By btnCanelTypeL = By.XPath("//div[@class='forceChangeRecordTypeFooter']//button//span[text()='Cancel']");

        //To Select Houlihan Employee option
        public void SelectContactRecordType(string file, string contactType)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            CustomFunctions.SelectByText(driver, driver.FindElement(drpdwnSelectRecordType), contactType);
            WebDriverWaits.WaitUntilEleVisible(driver, btnContinue);
            driver.FindElement(btnContinue).Click();
        }

        //To Select Houlihan Employee option
        public void SelectContactRecordType(string file, int row)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            CustomFunctions.SelectByText(driver, driver.FindElement(drpdwnSelectRecordType), ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 4));
            
            WebDriverWaits.WaitUntilEleVisible(driver, btnContinue,1000);
            driver.FindElement(btnContinue).Click();
        }
        //To Click on Continue button
        public void ClickContinue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnContinue);
            driver.FindElement(btnContinue).Click();
        }

        // To validate the list of contact record type
        public void ValidateContactRecordType(string file, int rows)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            IWebElement recordDropdown = driver.FindElement(drpdwnSelectRecordType);
            SelectElement select = new SelectElement(recordDropdown);
            int RowContactRecordType = ReadExcelData.GetRowCount(excelPath, "Contact");
            string usersType = ReadExcelData.ReadDataMultipleRows(excelPath, "UsersType", rows, 1);
            if (usersType.Equals("Admin"))
            {
                for (int i = 2; i <= RowContactRecordType; i++)
                {
                    IList<IWebElement> options = select.Options;
                    IWebElement contactTypeOption = options[i - 2];
                    Assert.AreEqual(contactTypeOption.Text, ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", i, 1));
                }
            }
            else
            {
                for (int i = 4; i <= RowContactRecordType; i++)
                {
                    IList<IWebElement> options = select.Options;
                    IWebElement contactTypeOption = options[i - 4];
                    Assert.AreEqual(contactTypeOption.Text, ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", i, 1));
                }
            }

        }

        public void ClickCancelContactTypePageLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCanelTypeL, 10);
            driver.FindElement(btnCanelTypeL).Click();
        }

        public bool AreContactTypesDisplayedLV(string file)

        {

            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;

            js.ExecuteScript("window.scrollTo(0,0)");

            ReadJSONData.Generate("Admin_Data.json");

            string dir = ReadJSONData.data.filePaths.testData;

            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, txtTypeL, 10);

            IList<IWebElement> listRecordTypes = driver.FindElements(txtTypeL);

            bool recordTypeFound = false;

            int rowOpp = ReadExcelData.GetRowCount(excelPath, "ContactTypes");

            foreach(IWebElement txtFieldError in listRecordTypes)

            {

                recordTypeFound = false;

                string actualRecordType = txtFieldError.Text.Trim();

                for(int row = 2; row <= rowOpp; row++)

                {

                    string valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", row, 1);

                    if(actualRecordType == valRecordTypeExl)

                    {

                        recordTypeFound = true;

                        break;

                    }

                }

            }

            return recordTypeFound;

        }

    }
}