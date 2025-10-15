using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Linq;
using System.Threading;

namespace SF_Automation.Pages
{
    class LoginPage : BaseClass
    {
        By txtUserName = By.Id("username");
        By txtPassWord = By.Id("password");
        By btnLogin = By.Id("Login");
        By loggedUser = By.XPath("//span[@id='userNavLabel']");
        By loggedUserLightningView = By.XPath("//header[@id='oneHeader']/div/div//a");
        By imgProfile = By.CssSelector("div[class*='profileTrigger ']>span[class='uiImage']");
        By lnkSwitchToClassic = By.XPath("//a[text()='Switch to Salesforce Classic']");
        By userIcon = By.CssSelector("div[class*='profileTrigger'] > span[class='uiImage']");
        By linkSalesforceClassic = By.XPath("//a[normalize-space()='Switch to Salesforce Classic']");
        By linkSwitchtoLightningExperience = By.CssSelector(".switch-to-lightning");
        By valUser = By.XPath("//span[contains(text(),'Logged in')]");
        By btnVerifyIdentity = By.XPath("//input[@title='Verify']");

        Outlook outlook = new Outlook();

        public void SwitchToLightningExperience()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, linkSwitchtoLightningExperience, 20);
                IWebElement linkSwitchtoLightning = driver.FindElement(linkSwitchtoLightningExperience);
                if (linkSwitchtoLightning.Displayed)
                {
                    linkSwitchtoLightning.Click();
                }
            }
            catch (Exception e)
            {
            }
        }

        public string ValidateUserLightningView()
        {
            //Thread.Sleep(5000);
            driver.SwitchTo().Window(driver.WindowHandles[0]);
            WebDriverWaits.WaitUntilEleVisible(driver, loggedUserLightningView, 20);
            string loggedUserName = driver.FindElement(loggedUserLightningView).Text;
            return loggedUserName;
        }

        public string ValidateUserLightningCAO()
        {
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, valUser, 350);
            IWebElement loggedUserName = driver.FindElement(valUser);
            return loggedUserName.Text.Substring(13, 10);
        }
        public void LoginAsExpenseRequestApprover(string file, int row)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            driver.FindElement(txtUserName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Approver", row, 1));
            driver.FindElement(txtPassWord).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Approver", row, 2));
            driver.FindElement(btnLogin).Click();

        }
        public void LoginAsFirstLevelExpenseRequest(string file, int row)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(txtUserName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "FirstLevelApprover", row, 1));
            driver.FindElement(txtPassWord).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "FirstLevelApprover", row, 2));
            driver.FindElement(btnLogin).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnVerifyIdentity, 5);
                Thread.Sleep(5000);
                outlook.SelectVerifyIdentityEmail();
            }
            catch
            {
                // No Need to Verify your identity in Salesforce
            }
        }
        public void SwitchToClassicView()
        {
            string url = driver.Url;
            if (url.Contains(".com/lightning") || url.Contains(".lightning"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, imgProfile, 20);
                driver.FindElement(imgProfile).Click();
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, lnkSwitchToClassic, 20);
                driver.FindElement(lnkSwitchToClassic).Click();
                Thread.Sleep(2000);
            }
        }
        public void LoginApplication()
        {
            driver.FindElement(txtUserName).SendKeys(ReadJSONData.data.authentication.username);
            Console.WriteLine(ReadJSONData.data.authentication.username);
            driver.FindElement(txtPassWord).SendKeys(ReadJSONData.data.authentication.password);
            driver.FindElement(btnLogin).Click();

            Thread.Sleep(2000);
            string url = driver.Url;
            try
            {
                if (url.Contains("lightning.force.com/lightning/n/DNBoptimizer__Data_Stewardship1"))
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, imgProfile, 150);
                    driver.FindElement(imgProfile).Click();

                    WebDriverWaits.WaitUntilEleVisible(driver, lnkSwitchToClassic, 120);
                    driver.FindElement(lnkSwitchToClassic).Click();
                }
                else
                {
                    Console.WriteLine("No switch required ");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("No switch required ");
            }
        }
        public string ValidateUser()
        {
            Thread.Sleep(10000);
            //driver.SwitchTo().Window(driver.WindowHandles.Last());
            //WebDriverWaits.WaitUntilEleVisible(driver,loggedUser,190);
            IWebElement loggedUserName = driver.FindElement(loggedUser);
            return loggedUserName.Text;
        }

        public bool ValidateUserLightningView(string file, int userRow)
        {
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, loggedUserLightningView, 360);
            IWebElement loggedUserName = driver.FindElement(loggedUserLightningView);
            if (loggedUserName.Text.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", userRow, 1)))
            {
                result = true;
            }
            return result;
        }

        public void HandleSalesforceLightningPage()
        {
            WebDriverWaits.WaitForPageToLoad(driver, 15);
            Thread.Sleep(10000);
            string url = driver.Url;
            if (url.Contains("lightning.force.com/lightning/n/DNBoptimizer__Data_Stewardship1"))
            {
                //Thread.Sleep(15000);
                WebDriverWaits.WaitUntilEleVisible(driver, userIcon, 40);
                driver.FindElement(userIcon).Click();

                WebDriverWaits.WaitUntilEleVisible(driver, linkSalesforceClassic, 40);
                driver.FindElement(linkSalesforceClassic).Click();
            }
        }

        public void LoginAsExpenseRequestApprover(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtUserName, 40);
            driver.FindElement(txtUserName).SendKeys(ReadExcelData.ReadData(excelPath, "Approver", 1));
            driver.FindElement(txtPassWord).SendKeys(ReadExcelData.ReadData(excelPath, "Approver", 2));
            driver.FindElement(btnLogin).Click();
            Thread.Sleep(10000);
        }

        public void LoginAsFirstLevelExpenseRequest(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(txtUserName).SendKeys(ReadExcelData.ReadData(excelPath, "FirstLevelApprover", 1));
            driver.FindElement(txtPassWord).SendKeys(ReadExcelData.ReadData(excelPath, "FirstLevelApprover", 2));
            driver.FindElement(btnLogin).Click();
        }

        public string ValidateUserLightning()
        {

            Thread.Sleep(7000);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            WebDriverWaits.WaitUntilEleVisible(driver, valUser, 350);
            IWebElement loggedUserName = driver.FindElement(valUser);
            return loggedUserName.Text;
            //return loggedUserName.Text.Substring(13, 12);
        }

        public string ValidateUserLightningExcep()
        {

            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, valUser, 380);
            IWebElement loggedUserName = driver.FindElement(valUser);
            return loggedUserName.Text;
            //return loggedUserName.Text.Substring(13, 12);
        }

        public string ValidateFRUserLightning()
        {
            Thread.Sleep(7000);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            WebDriverWaits.WaitUntilEleVisible(driver, valUser, 350);
            IWebElement loggedUserName = driver.FindElement(valUser);
            return loggedUserName.Text.Substring(13, 13);
        }

        public void LoginAsExpenseRequestApproverV(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, txtUserName, 10);
            driver.FindElement(txtUserName).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Approver", row, 1));
            driver.FindElement(txtPassWord).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "Approver", row, 2));
            driver.FindElement(btnLogin).Click();
            Thread.Sleep(10000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnVerifyIdentity, 5);
                Thread.Sleep(5000);
                outlook.SelectVerifyIdentityEmail();
            }
            catch
            {
                // No Need to Verify your identity in Salesforce
            }
        }

        public string ValidateUserLightningCAO2nd()
        {
            Thread.Sleep(7000);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            WebDriverWaits.WaitUntilEleVisible(driver, valUser, 350);
            IWebElement loggedUserName = driver.FindElement(valUser);
            return loggedUserName.Text.Substring(13, 13);
        }

        public string ValidateUserLightningCAO3rd()
        {
            Thread.Sleep(7000);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            WebDriverWaits.WaitUntilEleVisible(driver, valUser, 350);
            IWebElement loggedUserName = driver.FindElement(valUser);
            return loggedUserName.Text.Substring(13, 12);
        }
    }
}
