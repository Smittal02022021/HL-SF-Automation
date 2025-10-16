using OpenQA.Selenium;
using SF_Automation.UtilityFunctions;
using System;
using System.Linq;
using System.Threading;

namespace SF_Automation.Pages.Common
{
    class UsersLogin : BaseClass
    {
        By linkSetUp = By.CssSelector("a[id='setupLink']");
        By linkManageUsers = By.CssSelector("[id='Users_font']");
        By linkUsers = By.Id("ManageUsers_font");
        By comboView = By.Id("fcf");
        //By linkLogin = By.CssSelector("a[title*='Login - Record 8 - Standard User, Verifaya']");
        By linkStdUser = By.XPath("//a[contains(text(),'User, SFStandard')]");
        By btnLogin = By.CssSelector("input[title ='Login']");
        By loggedUser = By.XPath("//span[@id='userNavLabel']");
        By linkLogOut = By.CssSelector("a[title='Logout']");
        By linkCAOUser = By.XPath("//a[contains(text(),'User, SFCAO')]");
        By linkFASCAOUser = By.XPath("//a[contains(text(),'User, SFFASCAO')]");
        By linkFRCAOUser = By.XPath("//a[contains(text(),'User, SFFRCAO')]");
        By tabHome = By.CssSelector("a[title*='Home Tab']");
        By linkStdFASUser = By.XPath("//a[contains(text(),'User, SFStdFAS')]");
        By linkStdFRUser = By.XPath("//a[contains(text(),'User, SFStdFR')]");
        By linkFRAccountingUser = By.XPath("//a[contains(text(),'User, SFFRAccounting')]");
        By linkcompUser = By.XPath("//a[contains(text(),'User, SFCompliance')]");
        By txtSearch = By.CssSelector("input[id*='phSearchInput']");
        By listUser = By.CssSelector("div[id*='phSearchInput']>div>ul>li>a>span[class='userMru']");//div[id*='phSearchInput']>div>ul>li
        By arrowMenu = By.CssSelector("a[title='User Action Menu']");
        By titleUserDetail = By.CssSelector("a[title='User Detail']");
        By dropDwnForUserDetail = By.CssSelector("a[id='moderatorMutton']");
        By optionUserDetail = By.CssSelector("a[id='USER_DETAIL']");
        By lnkUser = By.XPath("//img[@title='User']/following::strong");
        By imgProfile = By.CssSelector("div[class*='profileTrigger ']>span[class='uiImage']");
        By lnkSwitchToClassic = By.XPath("//a[text()='Switch to Salesforce Classic']");
        By imgUser = By.XPath("//span/div/span[@class='uiImage']");
        By lnkLogout = By.XPath("//div[2]/div/a[2]");
        By lnkLogoutL = By.XPath("//div/div[1]/a[text()='Log Out']");
        By lnkSwitchTo = By.XPath("//tr/td[3]/div/div[3]/div/a[1]");
        By linkLogoutLV = By.XPath("//header//div[@data-message-id='loginAsSystemMessage']//a[contains(text(),'Log out')]");
        By btnDiscard = By.XPath("//button[text()='Discard Changes']");

        public void SearchCFUserAndLogin(string name)
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtSearch, 150);
            driver.FindElement(txtSearch).SendKeys(name);
            Thread.Sleep(5000);
            // CustomFunctions.SelectValueWithoutSelect(driver, listUser, name);
            driver.FindElement(lnkUser).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, arrowMenu, 130);
            driver.FindElement(arrowMenu).Click();
            driver.FindElement(titleUserDetail).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnLogin, 100);
            driver.FindElement(btnLogin).Click();
            Thread.Sleep(2000);
            string url = driver.Url;
            try
            {
                if (url.Contains("lightning"))
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
        public void ClickManageUsers()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkSetUp);
            driver.FindElement(linkSetUp).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkManageUsers);
            driver.FindElement(linkManageUsers).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkUsers);
            driver.FindElement(linkUsers).Click();
            Thread.Sleep(4000);
            CustomFunctions.SelectValueWithoutSelect(driver, comboView, "Test Automation Users");
        }

        public void LoginAsStandardUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkStdUser, 80);
            driver.FindElement(linkStdUser).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnLogin);
            driver.FindElement(btnLogin).Click();
        }
        //To logout from a user
        public void UserLogOut()
        {
            Thread.Sleep(17000);
            //WebDriverWaits.WaitUntilEleVisible(driver, loggedUser, 140);
            driver.FindElement(loggedUser).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkLogOut, 160);
            driver.FindElement(linkLogOut).Click();
        }
        //To login with another user
        public void LoginAsCAOUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkUsers);
            driver.FindElement(linkUsers).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkCAOUser, 60);
            driver.FindElement(linkCAOUser).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnLogin);
            driver.FindElement(btnLogin).Click();
        }


        //Login as FAS CAO user
        public void LoginAsFASCAOUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkFASCAOUser);
            driver.FindElement(linkFASCAOUser).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnLogin);
            driver.FindElement(btnLogin).Click();
        }

        //Click on Home tab
        public void ClickHomeTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabHome, 120);
            driver.FindElement(tabHome).Click();
        }


        public void SearchUserAndLogin(string name)
        {
            Thread.Sleep(16000);
            //WebDriverWaits.WaitUntilEleVisible(driver, txtSearch, 180);
            driver.FindElement(txtSearch).SendKeys(name);
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkUser, 20);
            driver.FindElement(lnkUser).Click();
            //CustomFunctions.SelectValueWithoutSelect(driver, listUser, name);
            WebDriverWaits.WaitUntilEleVisible(driver, arrowMenu, 130);
            driver.FindElement(arrowMenu).Click();
            driver.FindElement(titleUserDetail).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnLogin, 100);
            driver.FindElement(btnLogin).Click();
        }
        //Login as Selected user from global search
        public void LoginAsSelectedUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, dropDwnForUserDetail);
            driver.FindElement(dropDwnForUserDetail).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, optionUserDetail);
            driver.FindElement(optionUserDetail).Click();
            Thread.Sleep(4000);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnLogin));

            //WebDriverWaits.WaitUntilEleVisible(driver, btnLogin);
            //driver.FindElement(btnLogin).Click();
            Thread.Sleep(2000);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(2000);
        }
        //--------------------

        public void LightningLogout()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, imgUser, 250);
            driver.FindElement(imgUser).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkLogout, 150);
            driver.FindElement(lnkLogout).Click();
        }

        //Log out of Lightning
        public void DiffLightningLogout()
        {
            Thread.Sleep(7000);
            //WebDriverWaits.WaitUntilEleVisible(driver, imgUser, 70);
            driver.FindElement(imgUser).Click();
            Thread.Sleep(7000);
            //WebDriverWaits.WaitUntilEleVisible(driver, lnkLogoutL, 10);
            driver.FindElement(lnkLogoutL).Click();
        }

        //Click on Switch To Lightning Experience link
        public void ClickSwitchToLightning()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkSwitchTo, 250);
            driver.FindElement(lnkSwitchTo).Click();
        }
        public void ClickLogoutFromLightningView()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkLogoutLV, 10);
            driver.FindElement(linkLogoutLV).Click();
        }

        public void DiscardChanges()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDiscard, 220);
            driver.FindElement(btnDiscard).Click();
        }
    }
}


