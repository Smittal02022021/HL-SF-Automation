
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace SF_Automation.Pages.TimeRecordManager
{
    class RefreshButtonFunctionality : BaseClass
    {

        ExtentReport extentReports = new ExtentReport();


        By txtTimeClockRecorder = By.XPath("//*[contains(@title,'Time Clock Recorder')]");
        By txtTimeClockRecorderTitle = By.XPath("//div[@class='slds-text-heading--medium']");
        By selectPrjctDropDown = By.XPath("//*[@class='slds-select']");
        By selectActivityDropDown = By.XPath("//select[@class='slds-input select uiInput uiInputSelect uiInput--default uiInput--select']");
        By btnStart = By.XPath("//button[text()='Start']");
        By btnRefresh = By.XPath("//button[text()='Refresh']");
        By txtClockTimerSecond = By.XPath("//div[@class='clock flip-clock-wrapper']/ul[5]/li[2]/a/div[1]/div[2]");
        By btnReset = By.XPath("//div[@id='existingRecordWarning']/p[4]/button[text()='Reset']");
        By btnResume = By.XPath("//button[text()='Resume']");
        By btnPause = By.XPath("//div[@class='slds-button-group slds-p-top--small']/button[2]");
        By txtTimer = By.XPath("//div[@disabled='disabled']/p/input");
        By btnUpdate = By.XPath("//div[@disabled='disabled']/p/button");
        By txtClockTimerHours = By.XPath("//div[@class='clock flip-clock-wrapper']/ul[2]/li[2]/a/div[1]/div[2]");
        By msgErrorStart = By.XPath("//div[@data-aura-class='uiMessage']/div/div[3]/span");
        By btnFinish = By.XPath("//button[text()='Finish']");        
        By comboSelectProjectN = By.XPath("//input[contains(@placeholder,'Type to filter projects')]");
        By comboSelectProjectName = By.XPath("(//div[@role='listbox']//li)[1]//span//span");
        By frameTimeRecordPage = By.XPath("//iframe[@title='accessibility title']");//iframe[@title='Salesforce - Unlimited Edition']");
        By imgSpinningLoader = By.XPath("//div[@class='loading']");
        By txtAddMinutes = By.XPath("//div[contains(@class,'TimeClockRecorder')]//p[contains(text(),'Add Minutes')]//input");
        By imgSpinner = By.XPath("//div[contains(@class,'spinner_container')]//div[@role='alert']");
        private By _btnTimeClockRecorder(string button)
        {
            return By.XPath($"//button[text()='{button}']");
        }
        // Go to TIme CLock Recorder Page
        public void GotoTimeClockRecorderPage()
        {
            Thread.Sleep(4000);
            //driver.Navigate().Refresh();
            //Thread.Sleep(12000);

            driver.FindElement(txtTimeClockRecorder).Click();
            Thread.Sleep(10000);
        }

        //Get the title of TIme Clock Recorder Page
        public string GetTitleTimeClockRecorderPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtTimeClockRecorderTitle, 80000);
            string TimeClockRecorderPageTitle = driver.FindElement(txtTimeClockRecorderTitle).Text;
            return TimeClockRecorderPageTitle;
        }

        //Select Project and Activity Drop Down
        public void SelectDropDownProjectandActivity(string excel)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(ReadExcelData.ReadData(excel, "Project_Title", 1));
                //extracode
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
                //
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, selectPrjctDropDown);
                CustomFunctions.SelectByText(driver, driver.FindElement(selectPrjctDropDown), ReadExcelData.ReadData(excel, "Project_Title", 1));

            }
            WebDriverWaits.WaitUntilEleVisible(driver, selectActivityDropDown, 80000);
            CustomFunctions.SelectByText(driver, driver.FindElement(selectActivityDropDown), ReadExcelData.ReadData(excel, "Project_Title", 2));
        }

        //Click Start Button

        public void ClickStartButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnStart);
            driver.FindElement(btnStart).Click();
          
        }

        //Click Refresh Button

        public void ClickRefreshButton()
        {
            Thread.Sleep(6000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnRefresh);
            driver.FindElement(btnRefresh).Click();
            Thread.Sleep(10000);
        }

        //Get seconds from Timer
        public string GetSecondsTimer1()
        {

            //Time Required to check second timer
            Thread.Sleep(12000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtClockTimerSecond);
          
            string GetSecondsTimer1 = driver.FindElement(txtClockTimerSecond).Text;
            return GetSecondsTimer1;
        }

        //Click Reset Button when Displayed

        public void ClickResetButton()
        {
            try
            {

                if ((driver.FindElement(btnReset)).Displayed)
                {
                    driver.FindElement(btnReset).Click();
                    Thread.Sleep(4000);
                }
            }
            catch (Exception)
            {

            }
        }

        //Click Resume Button
        public void ClickResumeButton()
        {           
            WebDriverWaits.WaitUntilEleVisible(driver, btnResume, 60000);
            driver.FindElement(btnResume).Click();
            Thread.Sleep(5000);
        }

        //Click Pause Button
        public void ClickPauseButton()
        {
            Thread.Sleep(8000);          
            driver.FindElement(btnPause).Click(); 
        }

        //Update Timer
        public void UpdateTimer(string excel)
        {
            
            WebDriverWaits.WaitUntilEleVisible(driver, txtTimer, 600000);
          
            string i=ReadExcelData.ReadData(excel, "Update_Timer", 1).ToString();
            driver.FindElement(txtTimer).SendKeys(i);
         
            WebDriverWaits.WaitUntilEleVisible(driver, btnUpdate);
            driver.FindElement(btnUpdate).Click();

            Thread.Sleep(50000);

        }

        //Get Hours from Timer
        public string GetHoursTimer1()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtClockTimerHours);
            string GetHoursTimer1 = driver.FindElement(txtClockTimerHours).Text;
            return GetHoursTimer1;
        }
        //Get Error Message when project is not Selected
        public string GetErrorMessageStart()
        {           
            var errormsg = driver.FindElement(msgErrorStart);
            var errmsg= errormsg.Text;
            return errmsg;
        }

        //Start Button is clickable or not after selecting project and acitvity drop down

        public static bool ButtonStartClickable() {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(200));
              
                //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Start']")));
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        //Check Refresh Button is not displaying
        public static bool HiddenRefreshButton()
        {
            try
            {
                if ((driver.FindElement(By.XPath("//button[text()='Refresh']"))) != null)
                {
                //    extentReports.CreateLog("Refresh button is displaying ");
                    
                }
                return false;
            }
            catch (Exception)

            {
              //  extentReports.CreateLog("Refresh button is hidden when project is not selected ");
                return true;
            }
        }

        //Click Finish Button
        public void ClickFinishButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtClockTimerHours,15000);
            Thread.Sleep(20000);
            driver.FindElement(btnFinish).Click();
            Thread.Sleep(2000);
        }
        // Go to TIme Clock Recorder Page
        public void GoToTimeClockRecorderPageLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            Thread.Sleep(10000);
            driver.FindElement(txtTimeClockRecorder).Click();
            //Thread.Sleep(500);
            //WebDriverWaits.WaitUntilEleVisible(driver, imgSpinningLoader);
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
        }       
        public string GetTitleTimeClockRecorderPageLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, txtTimeClockRecorderTitle, 60);
            string TimeClockRecorderPageTitle = driver.FindElement(txtTimeClockRecorderTitle).Text;
            driver.SwitchTo().DefaultContent();
            return TimeClockRecorderPageTitle;
        }
        public void ClickResetButtonLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            try
            {
                if ((driver.FindElement(btnReset)).Displayed)
                {
                    driver.FindElement(btnReset).Click();
                    Thread.Sleep(5000);
                }
            }
            catch (Exception)
            {

            }
            driver.SwitchTo().DefaultContent();
        }
        //Select Project and Activity Drop Down
        public void SelectDropDownProjectandActivityLV(string project, string activity)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(project);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();                
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, selectPrjctDropDown);
                CustomFunctions.SelectByText(driver, driver.FindElement(selectPrjctDropDown), project);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, selectActivityDropDown, 20);
            CustomFunctions.SelectByText(driver, driver.FindElement(selectActivityDropDown), activity);
            driver.SwitchTo().DefaultContent();
        }   
        public bool GetButtonStatusLV(string button)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, _btnTimeClockRecorder(button),20);
                bool btnstatus = driver.FindElement(_btnTimeClockRecorder(button)).Enabled;
                driver.SwitchTo().DefaultContent();
                return btnstatus;
            }
            catch { return false; }
            
        }
        public void ClickStartButtonLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, btnStart);
            driver.FindElement(btnStart).Click();
            Thread.Sleep(10000);
            driver.SwitchTo().DefaultContent();
        }        
        public void AddMinutesToTimerLV(string minutes)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, txtAddMinutes, 20);
            driver.FindElement(txtAddMinutes).SendKeys(minutes);
            driver.FindElement(_btnTimeClockRecorder("Update")).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
        }
        public void ClickFinishButtonLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, txtClockTimerHours, 20);
            //Thread.Sleep(20000);
            //driver.FindElement(btnFinish).Click();
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnFinish));            
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
        }
        public void ClickRefreshButtonLV()
        {
            Thread.Sleep(3000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, btnRefresh);
            driver.FindElement(btnRefresh).Click();
            //WebDriverWaits.WaitUntilEleVisible(driver, imgSpinner, 60);
            Thread.Sleep(10000);
            driver.SwitchTo().DefaultContent();
        }
        //Get seconds from Timer
        public string GetSecondsTimerLV()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, txtClockTimerSecond);
            string GetSecondsTimer = driver.FindElement(txtClockTimerSecond).Text;
            driver.SwitchTo().DefaultContent();
            return GetSecondsTimer;
        }
        //Get Hours from Timer
        public string GetHoursTimerLV()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, txtClockTimerHours);
            string GetHoursTimer = driver.FindElement(txtClockTimerHours).Text;
            driver.SwitchTo().DefaultContent();
            return GetHoursTimer;
        }
        //Click Resume Button
        public void ClickResumeButtonLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, btnResume, 60);
            driver.FindElement(btnResume).Click();
            Thread.Sleep(10000);
            driver.SwitchTo().DefaultContent();
        }
        
        //Click Pause Button
        public void ClickPauseButtonLV()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            driver.FindElement(btnPause).Click();
            //WebDriverWaits.WaitUntilEleVisible(driver, imgSpinner, 60);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(5000);
        }
        public void SelectDropDownProjectAndActivityOptionsLV(string project)
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectN);
                driver.FindElement(comboSelectProjectN).Click();
                driver.FindElement(comboSelectProjectN).SendKeys(project);
                WebDriverWaits.WaitUntilEleVisible(driver, comboSelectProjectName);
                driver.FindElement(comboSelectProjectName).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, selectPrjctDropDown);
                CustomFunctions.SelectByText(driver, driver.FindElement(selectPrjctDropDown), project);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, selectActivityDropDown, 20);
            driver.FindElement(selectActivityDropDown).Click();
            driver.SwitchTo().DefaultContent();
        }
        //Get Error Message when project is not Selected
        public string GetErrorMessageStartLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameTimeRecordPage));
            WebDriverWaits.WaitUntilEleVisible(driver, msgErrorStart,20);
            var errormsg = driver.FindElement(msgErrorStart);
            var errmsg = errormsg.Text;
            driver.SwitchTo().DefaultContent();
            return errmsg;
        }
    } 
}