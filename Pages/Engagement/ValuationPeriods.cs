using Microsoft.Office.Interop.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using static SF_Automation.TestData.ReadJSONData;
using Actions = OpenQA.Selenium.Interactions.Actions;

namespace SF_Automation.Pages.Engagement
{
    class ValuationPeriods : BaseClass
    {
        By btnNewEngValPeriod = By.CssSelector("input[value='New Engagement Valuation Period']");
        By lblNewEngValPeriod = By.CssSelector("h2[class='pageDescription']");
        By txtName = By.CssSelector("input[name*='id30']");
        By lnkValDate = By.CssSelector("tbody > tr:nth-child(3) > td:nth-child(2) > div > span > span > a");
        By lnkClientFinalDeadLine = By.CssSelector("tbody > tr:nth-child(7) > td:nth-child(2) > div > span > span > a");
        By btnSave = By.CssSelector("input[value='Save']");
        By lblEngValPeriodDetail = By.CssSelector("h2[class='mainTitle']");
        By btnNewEngValPeriodPosition = By.CssSelector("input[value='New Eng Valuation Period Position']");
        By btnCompany = By.CssSelector("span[class='lookupInput']>input[id*='CompanyField']");
        By comboAssetClasses = By.CssSelector("select[name*='id66']");
        By comboPositionIG = By.CssSelector("select[name*='PositionIG']");
        By comboPositionSector = By.CssSelector("select[name*='PositionS']");
        By comboToolUtilized = By.CssSelector("select[name*=':AutomationToolUtilizedId']");
        By txtReportFee = By.CssSelector("input[name*='id68']");
        By btnImportPositions = By.CssSelector("input[value='Import Positions']");
        By valPeriod = By.CssSelector("td[id*=':j_id167'] > a");
        By btnAddTeamMember = By.CssSelector("input[value='Add New Team Member']");
        By btnSaveTeamMember = By.CssSelector("input[value='Save Team Members']");
        By btnEdit = By.CssSelector("input[value='Edit']");
        //By btnEditPositionL = By.XPath("//input[@name='pgid:frmId:pbid:BtnsId:OnEdit']");
        By comboStatus = By.CssSelector("select[name*='PositionStatusID']");
        By msgErrorBox = By.CssSelector("div[class='message errorM3']");
        By msgError1 = By.CssSelector("span[id *= 'j_id18'] > ul > li:nth-child(1)");
        By msgError2 = By.CssSelector("span[id *= 'j_id18'] > ul > li:nth-child(2)");
        By valStatus = By.CssSelector("span[id*='StatusId']");
        By valFeeCompleted = By.CssSelector("span[id*='id66']");
        By valFeeCompletedInProgressL = By.XPath("//th[contains(text(),' Fee Completed')]/ancestor::tr/td[1]/span");
        By valFeeCompletedL = By.XPath("//th[contains(text(),' Report')]/ancestor::tr/td[1]/span//input");

        By valRevenueMonth = By.CssSelector("span[id*='id82']");
        By valRevenueMonthL = By.XPath("//span[contains(text(),'Revenue Month')]/ancestor::tr/td[1]/span");

        By valCancelMonth = By.CssSelector("span[id*='id83']");
        By valCancelMonthL = By.XPath("//th[contains(text(),'Cancel Month')]/ancestor::tr/td[2]/span");
        By valRevenueYear = By.CssSelector("span[id*='id85']");
        By valRevenueYearL = By.XPath("//span[contains(text(),'Revenue Year')]/ancestor::tr/td[1]/span");
        By valCancelYear = By.CssSelector("span[id*='id85']");
        By valCancelYearL = By.XPath("//th[contains(text(),'Cancel Year')]/ancestor::tr/td[2]/span");
        By valCancelYear1 = By.XPath("//th[contains(text(),'Cancel Year')]/ancestor::tr/td[2]/span");
        By valCancelYear1L = By.CssSelector("//th[contains(text(),'Cancel Date')]/ancestor::tr/td[2]/span");

        By valCompletedDate = By.CssSelector("span[id*='id87']");
        By valCompletedDateL = By.XPath("//th[contains(text(),'Completed Date')]/ancestor::tr/td[1]/span");

        By valCancelDate = By.CssSelector("span[id*='id88']");
        By valCancelDateL = By.XPath("//th[contains(text(),'Cancel Date')]/ancestor::tr/td[2]/span");

        By btnBackToValuation = By.CssSelector("input[value='Back To Valuation Period']");
        By valPositionName = By.CssSelector("td[id*='id167']>a");
        By txtUpReportFee = By.CssSelector("input[name*='id41']");
        By btnVoidPosition = By.CssSelector("input[value='Void Position']");
        By msgCancel = By.CssSelector("div[id*='_id5']");
        By btnYes = By.CssSelector("input[value=' Yes ']");
        By linkDel = By.CssSelector("a[name*='id176']");
        By msgSuccess1 = By.CssSelector("div[id*='id8']");

        By btnEditL = By.XPath("//input[@value='Edit']");
        By txtNameL = By.XPath("//label[text()='Name']/ancestor::tr/td//input");
        By btnSaveL = By.XPath("//input[@value='Save']");
        By msgMandatoryFieldL = By.XPath("//div[contains(@class,'messageText')]");
        By btnCancelL = By.XPath("//input[@value='Cancel']");
        By titleEngValPeriodL = By.XPath("//h1[contains(text(),' Valuation Period')]");
        By valClientFinalL = By.XPath("//tr[9]/td[1]//a");
        By updClientFinalL = By.XPath("//th[text()='Client Final Deadline']/ancestor::tr/td[2]/span/span/span");
        By recPeriodAllocationL = By.XPath("//form/div[2]//tr[2]");
        By btnImportPositionsL = By.XPath("//input[@value='Import Positions']");
        By btnExistingImports = By.XPath("//td[1]/span[1]/input[contains(@value,'Valuation Period')]");
        By btnExistingValPeriodL = By.XPath("//input[@type='radio']");
        By btnSearchValPeriodPosL = By.XPath("//input[@value='Search Valuation Period for Positions']");
        By lblImportL = By.XPath("//label[contains(text(),'Positions')]");
        By chkPositionNameL = By.XPath("//input[contains(@name,'myCheckbox')]");
        By btnSaveImportL = By.XPath("//input[@value='Save']");
        By msgAutomationToolL = By.XPath("//div[@class='messageText']");
        By btnAutomationToolL = By.XPath("//input[@value='Update Automation Tool Usage']");
        By lblColumnsL = By.XPath("//td[1]/table//th/div");
        By comboUtilizedL = By.XPath("//select[contains(@name,'AutomationToolUtilizedId')]");
        By comboReasonL = By.XPath("//select[contains(@name,'ReasonFieldId')]");
        By btnSaveAutomation = By.XPath("//div[3]/table//td[2]/input[1]");
        By titleRelatedPositionsL = By.XPath("//h3[text()='Related Positions']");
        By btnSaveAndBackL = By.XPath("//input[@value='Save & Back To Valuation Period']");
        By valImportedPositionL = By.XPath("//div[2]//tr[2]/td[2]/a");
        By valImportedPosition2L = By.XPath("//div[2]//tr[1]/td[2]/a");
        By btnCloseTabL = By.XPath("//li[4]/div[2]/button");
        By radioImportWithoutL = By.XPath("//input[@value='Import Positions Without Team Members']");
        By titleEngValPeriodEditL = By.XPath("//h1[text()='Engagement Valuation Period Edit']");
        By lblEngValPeriodEditL = By.XPath("//span[text()='*']/ancestor::label");
        By btnEngValPeriodEditL = By.XPath("//td[@class='pbButton ']/input");
        By msgEngValPeriodEditL = By.XPath("//tr[2]//li");
        By titleValPeriodsL = By.XPath("//b[contains(text(),' Valuation Period')]");
        By btnNewEngValPeriodL = By.XPath("//input[@value='New Engagement Valuation Period']");
        By lnkValDateL = By.XPath("//tr[3]/td[1]/div//a");
        By lnkClientDeadlineL = By.XPath("//tr[7]/td[1]/div//a");
        By valNameL = By.XPath("//th[text()='Name']/ancestor::tr/td/span/span/span");
        By btnNewEngPeriodPositionL = By.XPath("//input[@value='New Eng Valuation Period Position']");
        By msgMandatoryValL = By.XPath("//tbody/tr/td//li");
        By txtCompanyL = By.XPath("//span/input[contains(@id,'AccountSectionItem:CompanyField')]");
        By btnAssetClassL = By.XPath("//select[contains(@id,'id66')]");
        By btnIGL = By.XPath("//select[contains(@id,'PositionIG')]");
        By btnPositonSectorL = By.XPath("//select[contains(@id,'PositionS')]");
        By btnAutomationToolPositionL = By.XPath("//select[contains(@id,'AutomationToolUtilizedId')]");
        By valAddedPositionL = By.XPath("//span/table/tbody/tr[1]/td[2]/a[1]");
        By btnEditPositionL = By.XPath("//input[@value='Back To Valuation Period']/ancestor::td/input[@value='Edit']");
        By txtEditNameL = By.XPath("//label[text()='Position Name']/ancestor::tr/td[1]//input");
        By valUpdPositionL = By.XPath("//th[text()='Position Name']/ancestor::tr/td[1]/span[1]/span/span");
        By secEngValTeamMemL = By.XPath("//b[text()='Eng Valuation Period Team Members']");
        By btnAddTeamMemL = By.XPath("//input[@value='Add New Team Member']");
        By colTeamMemL = By.XPath("//span/div[2]/div//tr/th/div");
        By btnSaveTeamMemL = By.XPath("//input[@value='Save Team Members']");
        By lnkDeleteTeamL = By.XPath("//a[text()='Delete']");
        By btnVoidPositionL = By.XPath("//input[@value='Void Position']");
        By msgConfirmationVoidL = By.XPath("//tbody//div[contains(text(),'Are you')]");
        By btnVoidPositionsL = By.XPath("//tbody/tr[2]/td/input[@type='submit']");
        By btnNoVoidL = By.XPath("//input[@value=' No ']");
        By btnYesVoidL = By.XPath("//input[@value=' Yes ']");
        By valStatusPeriodPositionL = By.XPath("//th[text()='Status']/ancestor::tr/td[2]/span");
        By valEngValPeriodL = By.XPath("//th[text()='Engagement Valuation Period']/ancestor::tr/td[1]/span");
        By btnUpdateAutoToolUsageL = By.XPath("//input[@value='Update Automation Tool Usage']");
        By btnCancelAutoToolL = By.XPath("//div[@class='pbBottomButtons']//input[@value='Cancel']");
        By titleEngValPeriodDetailL = By.XPath("//h2[text()='Engagement Valuation Period Detail']");
        By tabEngValPeriodL = By.XPath("//ul[2]/li[3]/a/span[2]");
        By btnCloseEngValPeriodL = By.XPath("//ul[2]/li[3]/div[2]/button");
        By valEngValAllocationL = By.XPath("//th[text()='Engagement Valuation Period']/ancestor::tr[1]/td[1]/span/a");
        By btnEngValPeriodAllocationL = By.XPath("//input[@value='New Eng Valuation Period Allocation']");
        By titleEngValPeriodAllocationL = By.XPath("//h2[text()='New Eng Valuation Period Allocation']");
        By txtWeekStartingL = By.XPath("//label[text()='Week Starting']/ancestor::tr/td[1]//span/input");
        By txtWeekEndingL = By.XPath("//label[text()='Week Ending']/ancestor::tr/td[1]//span/input");
        By btnSaveAllocationL = By.XPath("//div[3]/table/tbody/tr/td[2]/input[1]");
        By btnCancelAllocationL = By.XPath("//div[3]/table/tbody/tr/td[2]/input[2]");
        By addedAllocationL = By.XPath("//tbody/tr[3]/th/a[contains(text(),'VPA-')]");
        By lnkWeekStartingL = By.XPath("//label[text()='Week Starting']/ancestor::tr/td[1]//span/a");
        By lnkWeekEndingL = By.XPath("//label[text()='Week Ending']/ancestor::tr/td[1]//span/a");
        By msgDupAllocationL = By.XPath("//div[text()='Duplicate Record/s Exists']");
        By lnkEditAllocationL = By.XPath("//tbody/tr[3]/td[1]/a");
        By txtAnalystAllocationL = By.XPath("//slot/records-record-layout-row[2]//lightning-primitive-input-simple//input");
        By btnSaveUpdAllL = By.XPath("//button[@name='SaveEdit']");
        By tabPeriodPositionL = By.XPath("//a/span[text()='XYZ']");
        By valAnalystAllocationL = By.XPath("//div/form//tr[3]/td[4]");
        By btnMassEditL = By.XPath("//input[@title='Mass Edit']");
        By btnMassEditButtonsL = By.XPath("//span[text()='Eng Valuations Period Allocations ']/ancestor::div[3]//following::div//button[contains(text(),'Eng')]");
        By btnSelectAllL = By.XPath("//span[text()='Select All']/ancestor::label[1]/span[1]");
        By btnInlineEditL = By.XPath("//tr[1]/td[5]//span//button[@data-navigation='enable']");
        By btnUpdateL = By.XPath("//form//div/span/label/span[1]");
        By btnApplyL = By.XPath("//button[text()='Apply']");
        By txtAnalystL = By.XPath("//input[@name='dt-inline-edit-text']");
        By btnSaveMassEditL = By.XPath("//button[text()='Save']");
        By valTotalAllocationL = By.XPath("//div[2]/span/b");
        By btnBackToEngValPeriodL = By.XPath("//button[text()='Back to Engagement Valuation Period']");
        By btnBillingReqL = By.XPath("//input[@value='Billing Request']");
        By btnBillingReqButtonsL = By.XPath("//div[@class='ui-dialog-content ui-widget-content']//label");
        By btnTotalReportFeeL = By.XPath("//input[@value='TotalReportFee']");
        By btnIndivReportFeeL = By.XPath("//input[@value='IndividualReportFee']");
        By btnSaveBillingL = By.XPath("//div[4]/div[2]/input[1]");
        By btnSendEmailL = By.XPath("//div[1]/table/tbody/tr/td[2]/input[1]");
        By txtTo = By.XPath("//div[1]/div/table/tbody/tr/td/span/input[2]");
        By valTo = By.XPath("/html/body/ul[1]/li/a");
        By delAllocationL = By.XPath("//a[contains(@title,'Delete - Record 1 - VPA-')]");
        By valAllocationL = By.XPath("//a[contains(@title,'Delete - Record 1 - VPA-')]/ancestor::tr[1]/th/a");
        By delPositionL = By.XPath("//tr/td[1]/span/a/font");

        string dir = @"C:\Users\VKumarl0427\source\repos\SF_Automation\TestData\";

        //To Click New Engagement Valuation Period button
        public string ClickEngValuationPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewEngValPeriod, 60);
            driver.FindElement(btnNewEngValPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblNewEngValPeriod, 60);
            string title = driver.FindElement(lblNewEngValPeriod).Text;
            return title;
        }
        //To Click New Engagement Valuation Period button
        public string ClickEngValuationPeriodL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewEngValPeriod, 60);
            driver.FindElement(btnNewEngValPeriod).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            string title = driver.FindElement(lblNewEngValPeriod).Text;
            return title;
        }

        //Enter Engagement Valuation Period details and save it.
        public string EnterAndSaveEngValuationDetails()
        {
            string Name = "VP:" + CustomFunctions.RandomValue();
            driver.FindElement(txtName).SendKeys(Name);
            driver.FindElement(lnkValDate).Click();
            driver.FindElement(lnkClientFinalDeadLine).Click();
            driver.FindElement(btnSave).Click();
            return Name;
        }

        //Get title of Engagement Valuation Period Detail page
        public string GetEngValPeriodDetailTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnImportPositions, 60);
            string title = driver.FindElement(lblEngValPeriodDetail).Text;
            return title;
        }

        //Enter Eng Valuation Period Position details
        public string EnterPeriodPositionDetails(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewEngValPeriodPosition, 60);
            driver.FindElement(btnNewEngValPeriodPosition).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 60);
            driver.FindElement(btnCompany).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 4));
            driver.FindElement(comboAssetClasses).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 5));
            driver.FindElement(comboPositionIG).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 8));
            driver.FindElement(comboPositionSector).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 9));
            driver.FindElement(comboToolUtilized).SendKeys("Yes");
            //driver.FindElement(txtReportFee).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 7));
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblEngValPeriodDetail, 60);
            string value = driver.FindElement(valPeriod).Text;
            return value;
        }

        //Click on added Engagement Valuation Period Position
        public string ClickAddedValPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPeriod, 100);
            driver.FindElement(valPeriod).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblEngValPeriodDetail, 100);
            string title = driver.FindElement(lblEngValPeriodDetail).Text;
            return title;
        }

        //Click on added Engagement Valuation Period Position
        public string ClickAddedValPeriodL()
        {
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(5000);
            driver.FindElement(tabEngValPeriodL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().Frame(0);
            Thread.Sleep(4000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,250)");
            Thread.Sleep(5000);
            driver.FindElement(valAddedPositionL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblEngValPeriodDetail, 100);
            string title = driver.FindElement(lblEngValPeriodDetail).Text;
            return title;
        }

        //To add team members and save it. 
        public void ClickPositionAndSaveTeamMembers()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            By eleJobType = By.XPath("//div[3]//tr[5]/th[1]");
            Thread.Sleep(4000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleJobType));
            Thread.Sleep(5000);
            driver.FindElement(btnAddTeamMember).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveTeamMember, 100);
            driver.FindElement(btnSaveTeamMember).Click();
            Thread.Sleep(3000);
        }

        //To update the status of Position
        public string UpdateStatusAndSave()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboStatus).SendKeys("Completed, Generate Accrual");
            driver.FindElement(btnSave).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgErrorBox, 90);
            string error1 = driver.FindElement(msgError1).Text;
            return error1;
        }

        //To update the status of Position
        public string UpdateStatusAndSaveL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditPositionL, 80);
            driver.FindElement(btnEditPositionL).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(4000);
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            driver.FindElement(comboStatus).SendKeys("Completed, Generate Accrual");
            driver.FindElement(btnSave).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgErrorBox, 90);
            string error1 = driver.FindElement(msgError1).Text;
            return error1;
        }

        //To fetch 2nd error message
        public string GetErrorMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgErrorBox, 90);
            string error2 = driver.FindElement(msgError2).Text;
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(4000);
            return error2;
        }
        //To get Status of Position
        public string GetPositionStatus()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, valStatus, 90);
            string status = driver.FindElement(valStatus).Text;
            return status;
        }

        //To get Status of Position
        public string GetPositionStatusL()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, valStatus, 90);
            string status = driver.FindElement(valStatus).Text;
            return status;
        }
        //To get Fee Completed of Position
        public string GetFeeCompleted()
        {
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, valFeeCompleted, 190);
            string feeCompleted = driver.FindElement(valFeeCompleted).Text;
            return feeCompleted;
        }

        //To get Fee Completed of Position
        public string GetFeeCompletedInProgressL()
        {
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, valFeeCompletedInProgressL, 190);
            string feeCompleted = driver.FindElement(valFeeCompletedInProgressL).Text;
            return feeCompleted;
        }

        //To get Fee Completed of Position
        public string GetFeeCompletedL()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, valFeeCompletedL, 190);
            string feeCompleted = driver.FindElement(valFeeCompletedL).GetAttribute("value");
            return feeCompleted;
        }
        //To get Revenue Month of Position
        public string GetRevenueMonth()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRevenueMonth, 90);
            string revenueMonth = driver.FindElement(valRevenueMonth).Text;
            return revenueMonth;
        }

        //To get Revenue Month of Position
        public string GetRevenueMonthL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRevenueMonthL, 90);
            string revenueMonth = driver.FindElement(valRevenueMonthL).Text;
            return revenueMonth;
        }
        //To get Cancel Month of Position
        public string GetCancelMonth()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCancelMonth, 90);
            string cancelMonth = driver.FindElement(valCancelMonth).Text;
            return cancelMonth;
        }

        //To get Cancel Month of Position
        public string GetCancelMonthL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCancelMonthL, 90);
            string cancelMonth = driver.FindElement(valCancelMonthL).Text;
            return cancelMonth;
        }
        //To get Revenue Year of Position
        public string GetRevenueYear()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRevenueYear, 190);
            string revenueYear = driver.FindElement(valRevenueYear).Text;
            return revenueYear;
        }
        //To get Revenue Year of Position
        public string GetRevenueYearL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRevenueYearL, 190);
            string revenueYear = driver.FindElement(valRevenueYearL).Text;
            return revenueYear;
        }
        //To get Cancel Year of Position
        public string GetCancelYear()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCancelYear, 90);
            string cancelYear = driver.FindElement(valCancelYear).Text;
            return cancelYear;
        }

        //To get Cancel Year of Position
        public string GetCancelYearL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCancelYearL, 90);
            string cancelYear = driver.FindElement(valCancelYearL).Text;
            return cancelYear;
        }
        //To get Cancel Year of Position
        public string GetCancelYearWithDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCancelYear1, 90);
            string cancelYear = driver.FindElement(valCancelYear1).Text;
            return cancelYear;
        }

        //To get Cancel Year of Position
        public string GetCancelYearWithDetailsL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCancelYear1, 90);
            string cancelYear = driver.FindElement(valCancelYear1).Text;
            return cancelYear;
        }
        //To get Completed Date of Position
        public string GetCompletedDate()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valCompletedDate, 90);
            string compDate = driver.FindElement(valCompletedDate).Text;
            return compDate;
        }

        //To get Completed Date of Position
        public string GetCompletedDateL()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valCompletedDateL, 90);
            string compDate = driver.FindElement(valCompletedDateL).Text;
            return compDate;
        }
        //To get Cancel Date of Position
        public string GetCancelDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCancelDate, 90);
            string cancelDate = driver.FindElement(valCancelDate).Text;
            return cancelDate;
        }

        //To get Cancel Date of Position
        public string GetCancelDateL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCancelDateL, 90);
            string cancelDate = driver.FindElement(valCancelDateL).Text;
            return cancelDate;
        }

        //To  update Status and Report Fee of existing Position
        public string UpdateStatusAndReportFee(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToValuation, 60);
            driver.FindElement(btnBackToValuation).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valPositionName, 90);
            driver.FindElement(valPositionName).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 60);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboStatus, 60);
            driver.FindElement(comboStatus).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 14));
            driver.FindElement(txtUpReportFee).Clear();
            driver.FindElement(txtUpReportFee).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 11));
            Thread.Sleep(4000);
            driver.FindElement(btnSave).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess1, 100);
            string msg = driver.FindElement(msgSuccess1).Text;
            return msg;

        }

        //To  update Status and Report Fee of existing Position
        public string UpdateStatusAndReportFeeL(string file)
        {
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditPositionL, 60);
            driver.FindElement(btnEditPositionL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboStatus, 60);
            driver.FindElement(comboStatus).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 14));
            driver.FindElement(txtUpReportFee).Clear();
            driver.FindElement(txtUpReportFee).SendKeys(ReadExcelData.ReadData(excelPath, "ValuationPeriod", 11));
            Thread.Sleep(4000);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(6000);
                driver.SwitchTo().DefaultContent();
                driver.SwitchTo().Frame(1);
                Thread.Sleep(8000);
                WebDriverWaits.WaitUntilEleVisible(driver, valStatus, 110);
                string msg = driver.FindElement(valStatus).Text;
                return msg;
            }
            catch (Exception)
            {
                driver.SwitchTo().DefaultContent();
                driver.SwitchTo().Frame(1);
                Thread.Sleep(8000);
                WebDriverWaits.WaitUntilEleVisible(driver, valStatus, 110);
                string msg = driver.FindElement(valStatus).Text;
                return msg;
            }

        }
        //Click Void Position and fetch cancellation message
        public string ClickVoidPositionAndGetMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnVoidPosition);
            driver.FindElement(btnVoidPosition).Click();
            Thread.Sleep(6000);
            //driver.SwitchTo().DefaultContent();
            //driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, msgCancel, 90);
            string message = driver.FindElement(msgCancel).Text;
            driver.FindElement(btnYes).Click();
            return message;
        }

        //Click Void Position and fetch cancellation message
        public string ClickVoidPositionAndGetMessageL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnVoidPosition);
            driver.FindElement(btnVoidPosition).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            WebDriverWaits.WaitUntilEleVisible(driver, msgCancel, 90);
            string message = driver.FindElement(msgCancel).Text;
            driver.FindElement(btnYes).Click();
            return message;
        }

        //To delete the position  
        public void DeletePosition()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToValuation, 70);
            driver.FindElement(btnBackToValuation).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkDel, 60);
            driver.FindElement(linkDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
        }

        //Validate mandatory validation of Valuation Period
        public string ValidateMandatoryMessageOfValuationPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 120);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(msgMandatoryFieldL).Text;
            return value;
        }

        //Validate Opportunity details page is displayed upon clicking cancel button
        public string ValidateEngValDetailsPageUponClickingCancelButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelL, 120);
            driver.FindElement(btnCancelL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            string tab = driver.FindElement(titleEngValPeriodL).Text;
            return tab;
        }

        //Validate FVA use can edit Valuation Period
        public string EditFunctionalityOfValuationPeriod()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 120);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            driver.FindElement(valClientFinalL).Click();
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            string value = driver.FindElement(updClientFinalL).Text;
            return value;
        }

        //Validate added Period allocation record after updating Client Final Deadline
        public string ValidatePeriodAllocationRecordAfterUpdatingClientFinalDeadline()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, recPeriodAllocationL, 140);
            string value = driver.FindElement(recPeriodAllocationL).Displayed.ToString();
            return value;
        }

        //Validate Import Positions page buttons
        public bool ValidateButtonsOnImportPositions()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnImportPositionsL, 140);
            driver.FindElement(btnImportPositionsL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(btnExistingImports);
            var actualValue = valRecordTypes.Select(x => x.GetAttribute("value")).ToArray();
            string[] expectedValue = { "Search Valuation Period for Positions", "Back To Valuation Period" };
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //Validate the displayed buttons after clicking on Search Valuation Period for Positions
        public bool ValidateDisplayedImportButtonsUponClickingSearchValPeriod()
        {
            driver.FindElement(btnExistingValPeriodL).Click();
            driver.FindElement(btnSearchValPeriodPosL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblImportL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Import Positions Without Team Members", "Import Positions With Team Members" };
            Console.WriteLine(actualValue[0]);
            //Console.WriteLine(actualValue[1]);

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        //Validate Automation Tool Usage mandatory field validation
        public string ValidateAutomationToolMandatoryFieldMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chkPositionNameL, 120);
            driver.FindElement(chkPositionNameL).Click();
            driver.FindElement(btnSaveImportL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            string row = driver.FindElement(msgAutomationToolL).Text;
            return row;
        }

        //Validate Automation tool fields
        public bool ValidateColumnsOnAutomationToolPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAutomationToolL, 140);
            driver.FindElement(btnAutomationToolL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblColumnsL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "POSITION NAME", "AUTOMATION TOOL UTILIZED", "REASON", "COMMENTS" };
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //Validate that clicking the save button, the user is redirected to the related positions list to import positions.
        public string ValidatePositionsListPageUponClickingSaveButtonOnAutomationToolPage()
        {
            Thread.Sleep(5000);
            driver.FindElement(comboUtilizedL).SendKeys("Yes");
            //driver.FindElement(comboReasonL).SendKeys("Historical");
            driver.FindElement(btnSaveAutomation).Click();
            Thread.Sleep(7000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            string row = driver.FindElement(titleRelatedPositionsL).Text;
            return row;
        }

        //Validate that Import with team member 
        public string ValidateImportWithTeamMember()
        {
            Thread.Sleep(5000);
            driver.FindElement(btnSaveAndBackL).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(6000);
            string row = driver.FindElement(valImportedPositionL).Text;
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(4000);
            driver.FindElement(btnCloseTabL).Click();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            return row;
        }

        //Validate that Import without team member 
        public string ValidateImportWithoutTeamMember()
        {
            driver.FindElement(btnExistingValPeriodL).Click();
            driver.FindElement(btnSearchValPeriodPosL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(6000);
            driver.FindElement(radioImportWithoutL).Click();
            driver.FindElement(chkPositionNameL).Click();
            driver.FindElement(btnSaveAndBackL).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            driver.FindElement(btnAutomationToolL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            driver.FindElement(comboUtilizedL).SendKeys("Yes");
            driver.FindElement(btnSaveAutomation).Click();
            Thread.Sleep(7000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            driver.FindElement(btnSaveAndBackL).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(4000);
            string row = driver.FindElement(valImportedPositionL).Text;
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(4000);
            driver.FindElement(btnCloseTabL).Click();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            return row;
        }

        public string ValidatePageAfterClickingNewEngValPeriodButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewEngValPeriod, 140);
            driver.FindElement(btnNewEngValPeriod).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            string title = driver.FindElement(titleEngValPeriodEditL).Text;
            return title;
        }

        public bool ValidateMandatoryFieldsOnEngValPeriodEdit()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(lblEngValPeriodEditL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "*\r\nName", "*\r\nValuation Date", "*\r\nClient Final Deadline" };
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public bool ValidateButtonsOnEngValPeriodEdit()
        {
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(btnEngValPeriodEditL);
            var actualValue = valRecordTypes.Select(x => x.GetAttribute("value")).ToArray();
            string[] expectedValue = { "Save", "Cancel" };
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public bool ValidateMandatoryValidationsUponClickingSaveOnEngValPeriodEdit()
        {
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(msgEngValPeriodEditL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Name: You must enter a value", "Valuation Date: You must enter a value", "Client Final Deadline: You must enter a value" };
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);

            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        public string ValidatePageAfterClickingCancelButtonOnEngValPeriodEditPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelL, 140);
            driver.FindElement(btnCancelL).Click();
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            string title = driver.FindElement(btnNewEngValPeriodL).GetAttribute("value");
            driver.FindElement(btnNewEngValPeriodL).Click();
            return title;
        }

        //Enter all details and save it.
        public string EnterAndSaveEngValuationPeriodDetailsL(string name)
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            driver.FindElement(txtNameL).SendKeys(name);
            driver.FindElement(lnkValDateL).Click();
            driver.FindElement(lnkClientDeadlineL).Click();
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(7000);
            string value = driver.FindElement(valNameL).Text;
            return value;
        }

        //Click on Save on Valuation Period page
        public bool ValidateMessageWhileClickingSaveButtonOnPeriodPosition()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewEngPeriodPositionL, 120);
            driver.FindElement(btnNewEngPeriodPositionL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 120);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(msgMandatoryValL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Company: You must enter a value", "Asset Classes: You must enter a value", "Automation Tool Utilized: You must enter a value" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }


        //Enter all details and save it.
        public string EnterAndSaveEngValuationPeriodPositionDetailsL(string name)
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            driver.FindElement(txtCompanyL).SendKeys(name);
            Thread.Sleep(6000);
            driver.FindElement(btnIGL).SendKeys("BUS - Business Services");
            Thread.Sleep(4000);
            driver.FindElement(btnAssetClassL).SendKeys("ABL");
            Thread.Sleep(4000);
            driver.FindElement(btnPositonSectorL).SendKeys("Cloud & Enterprise Consulting");
            Thread.Sleep(4000);
            driver.FindElement(btnAutomationToolPositionL).SendKeys("Yes");
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(8000);
            string value = driver.FindElement(valAddedPositionL).Text;
            driver.FindElement(valAddedPositionL).Click();
            return value;
        }

        //Validate edit functionality of Period Position
        public string EditFunctionalityOfPeriodPosition(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedPositionL, 120);
            driver.FindElement(valAddedPositionL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            driver.FindElement(btnEditPositionL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            driver.FindElement(txtEditNameL).Clear();
            driver.FindElement(txtEditNameL).SendKeys(name);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(6000);
            string value = driver.FindElement(valUpdPositionL).Text;
            return value;
        }

        //Validate Eng Valuation Team member
        public string ValidateSecEngValTeamMember()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secEngValTeamMemL, 60);
            string section = driver.FindElement(secEngValTeamMemL).Text;
            return section;
        }

        //Validate button Add New Team member
        public string ValidateAddNewTeamMemberButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddTeamMemL, 60);
            string section = driver.FindElement(btnAddTeamMemL).GetAttribute("value");
            return section;
        }

        //Validate button Add New Team member
        public bool ValidateTeamMemberColumns()
        {
            driver.FindElement(btnAddTeamMemL).Click();
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(colTeamMemL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "#", "STAFF", "ROLE", "START DATE", "END DATE", "STATUS", "ACTION" };
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);
            Console.WriteLine(actualValue[2]);
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }
        //Validate button Save New Team member
        public string ValidateSaveTeamMemberButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveTeamMemL, 60);
            string section = driver.FindElement(btnSaveTeamMemL).GetAttribute("value");
            return section;
        }

        //Validate Delete link corresponding to added Team member
        public string ValidateDeleteLinkTeamMember()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDeleteTeamL, 60);
            string section = driver.FindElement(lnkDeleteTeamL).Text;
            return section;
        }

        //Validate Void Position functionality 
        public string ValidateConfirmationMessageoAfterClickingVoidPositionOnPeriodPosition()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnVoidPositionL, 120);
            driver.FindElement(btnVoidPositionL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            string value = driver.FindElement(msgConfirmationVoidL).Text;
            return value;
        }

        //Validate button on Void Position
        public bool ValidateVoidPositionButtons()
        {

            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(btnVoidPositionsL);
            var actualValue = valRecordTypes.Select(x => x.GetAttribute("value")).ToArray();
            string[] expectedValue = { " Yes ", " No " };
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //Validate Void Position functionality after clicking No
        public string ValidateVoidPositionByClickingNo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNoVoidL, 120);
            driver.FindElement(btnNoVoidL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            string value = driver.FindElement(valStatusPeriodPositionL).Text;
            return value;
        }

        //Validate Void Position functionality after clicking Yes
        public string ValidateVoidPositionByClickingYes()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedPositionL, 120);
            driver.FindElement(valAddedPositionL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnVoidPositionL, 130);
            driver.FindElement(btnVoidPositionL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnYesVoidL, 130);
            driver.FindElement(btnYesVoidL).Click();
            Thread.Sleep(7000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            string value = driver.FindElement(valStatusPeriodPositionL).Text;
            return value;
        }

        //Validate cancel functionality of Update Automation Tool Usage button
        public string ValidateCancelFunctionalityOfUpdateAutomationToolUsage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valEngValPeriodL, 120);
            driver.FindElement(valEngValPeriodL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnUpdateAutoToolUsageL, 130);
            driver.FindElement(btnUpdateAutoToolUsageL).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelAutoToolL, 130);
            driver.FindElement(btnCancelAutoToolL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            string value = driver.FindElement(titleEngValPeriodDetailL).Text;
            return value;
        }

        //Validate accept functionality of Update Automation Tool Usage button
        public string ValidateAcceptFunctionalityOfUpdateAutomationToolUsage()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, valEngValPeriodL, 120);
            //driver.FindElement(valEngValPeriodL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnUpdateAutoToolUsageL, 130);
            driver.FindElement(btnUpdateAutoToolUsageL).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);

            driver.FindElement(comboUtilizedL).SendKeys("Yes");
            driver.FindElement(btnSaveAutomation).Click();
            Thread.Sleep(7000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(4000);
            string value = driver.FindElement(titleEngValPeriodDetailL).Text;
            return value;
        }

        //Validate functionality of New Eng Val Period Allocation button
        public string ValidateNewEngValPeriodAllocation()
        {
            Thread.Sleep(7000);
            driver.SwitchTo().DefaultContent();
            //WebDriverWaits.WaitUntilEleVisible(driver, tabEngValPeriodL, 120);
            driver.FindElement(tabEngValPeriodL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEngValPeriodAllocationL, 130);
            driver.FindElement(btnEngValPeriodAllocationL).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(2);
            Thread.Sleep(5000);
            string value = driver.FindElement(titleEngValPeriodAllocationL).Text;
            return value;
        }

        //Validate cancel functionality of Valuation Period Allocation button
        public string ValidateCancelFunctionalityOfEngValPeriodAllocation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelAutoToolL, 130);
            driver.FindElement(btnCancelAutoToolL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngValPeriodL, 120);
            driver.FindElement(tabEngValPeriodL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            string value = driver.FindElement(titleEngValPeriodDetailL).Text;
            return value;
        }

        //Validate save functionality of Valuation Period Allocation button
        public string ValidateSaveFunctionalityOfValuationPeriodAllocation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEngValPeriodAllocationL, 130);
            driver.FindElement(btnEngValPeriodAllocationL).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(2);
            Thread.Sleep(5000);
            driver.FindElement(txtWeekStartingL).SendKeys("21/4/2024");
            driver.FindElement(txtWeekEndingL).SendKeys("28/4/2024");
            Thread.Sleep(7000);
            driver.FindElement(btnSaveAllocationL).Click();
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(2);
            Thread.Sleep(10000);
            string value = driver.FindElement(addedAllocationL).Displayed.ToString();
            return value;
        }

        //Validate mandatory field validation of Valuation Period Allocation button
        public string ValidateDuplicateValidationOfValuationPeriodAllocation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEngValPeriodAllocationL, 130);
            driver.FindElement(btnEngValPeriodAllocationL).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(3);
            Thread.Sleep(5000);
            driver.FindElement(lnkWeekStartingL).Click();
            driver.FindElement(lnkWeekEndingL).Click();
            Thread.Sleep(7000);
            driver.FindElement(btnSaveAllocationL).Click();
            Thread.Sleep(4000);
            string value = driver.FindElement(msgDupAllocationL).Text;
            driver.FindElement(btnCancelAllocationL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(2);
            Thread.Sleep(5000);
            return value;
        }
        //Validate edit functionality of Valuation Period Allocation button
        public string ValidateEditFunctionalityOfValuationPeriodAllocation()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,450)");
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditAllocationL, 130);
            driver.FindElement(lnkEditAllocationL).Click();
            Thread.Sleep(7000);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(3000);
            driver.FindElement(txtAnalystAllocationL).Clear();
            driver.FindElement(txtAnalystAllocationL).SendKeys("10");
            driver.FindElement(btnSaveUpdAllL).Click();
            Thread.Sleep(6000);
            driver.FindElement(tabPeriodPositionL).Click();
            //driver.Navigate().Refresh();
            //Console.WriteLine("browser refreshed ");

            driver.FindElement(btnCloseEngValPeriodL).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            driver.FindElement(valEngValAllocationL).Click();

            Thread.Sleep(7000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            string value = driver.FindElement(valAnalystAllocationL).Text;
            return value;
        }

        //Validate button on Mass Edit window
        public bool ValidateMassEditButtons()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnMassEditL, 130);
            driver.FindElement(btnMassEditL).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(btnMassEditButtonsL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Back to Engagement", "Back to Engagement Valuation Period", "New Eng Valuation Period Allocation" };
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //Validate Inline edit functionality
        public string ValidateInLineEditFunctionalityOfPeriodAllocations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSelectAllL, 130);
            driver.FindElement(btnSelectAllL).Click();
            Thread.Sleep(6000);
            Thread.Sleep(5000);
            Actions actions = new Actions(driver);
            var analyst = driver.FindElement(btnInlineEditL);
            actions.MoveToElement(analyst);
            actions.Perform();
            driver.FindElement(btnInlineEditL).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtAnalystL).Clear();
            driver.FindElement(txtAnalystL).SendKeys("10");
            driver.FindElement(btnUpdateL).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnApplyL).Click();
            driver.FindElement(btnSaveMassEditL).Click();
            Thread.Sleep(6000);
            //driver.SwitchTo().DefaultContent(); 
            //driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            string totalAnalyst = driver.FindElement(valTotalAllocationL).Text;
            return totalAnalyst;
        }

        //Validate Back To Engagement functionality
        public string ValidateBackToEngagementFunctionality()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToEngValPeriodL, 130);
            driver.FindElement(btnBackToEngValPeriodL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);
            string title = driver.FindElement(titleEngValPeriodDetailL).Text;
            return title;
        }

        //Validate button on Billing Request window
        public bool ValidateBillingRequestButtons()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBillingReqL, 130);
            driver.FindElement(btnBillingReqL).Click();
            Thread.Sleep(6000);
            IReadOnlyCollection<IWebElement> valRecordTypes = driver.FindElements(btnBillingReqButtonsL);
            var actualValue = valRecordTypes.Select(x => x.Text).ToArray();
            string[] expectedValue = { "Total Report Fee", "Individual Report Fee" };
            Console.WriteLine(actualValue[0]);
            Console.WriteLine(actualValue[1]);
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //Validate Send Email Page upon saving Total Report Fee 
        public string ValidateSendEmailPageUponSavingTotalReportFee()
        {
            driver.FindElement(btnTotalReportFeeL).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnSaveBillingL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//div[1]/div/div/div/force-aloha-page/div/iframe")));
            Thread.Sleep(6000);
            string title = driver.FindElement(btnSendEmailL).GetAttribute("value");
            return title;
        }

        //Validate Send Email Page upon saving Indiv Report Fee 
        public string ValidateSendEmailPageUponSavingIndivReportFee()
        {
            driver.FindElement(btnIndivReportFeeL).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnSaveBillingL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//div[1]/div/div/div/force-aloha-page/div/iframe")));
            Thread.Sleep(6000);
            string title = driver.FindElement(btnSendEmailL).GetAttribute("value");
            return title;
        }


        //Validate that the user is able to send an email
        public string ValidateSendEmailFunctionality()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtTo, 100);
            driver.FindElement(txtTo).Clear();
            Thread.Sleep(5000);
            driver.FindElement(txtTo).SendKeys("Sonika Goyal");
            Thread.Sleep(4000);
            driver.FindElement(valTo).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSendEmailL, 150);
            driver.FindElement(btnSendEmailL).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(0);
            Thread.Sleep(5000);

            try
            {
                string button = driver.FindElement(titleEngValPeriodDetailL).Text;
                return button;
            }
            catch (Exception)
            {
                return "Button is not displayed";
            }
        }

        public void SwitchFrame()
        {
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(5000);
        }

        //Get Allocation
        public string GetAllocation(string name)
        {
            driver.FindElement(By.XPath("//a[text()='" + name + "']")).Click();
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            string value = driver.FindElement(valAllocationL).Text;
            return value;
        }

        //Validate delete functionality of allocation after clicking No on confirmation pop up
        public string ValidateDeleteFunctionalityOfPeriodAllocationAfterSelectingNo()
        {
            driver.FindElement(delAllocationL).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Dismiss();
            Thread.Sleep(6000);
            string allocation = driver.FindElement(valAllocationL).Text;
            return allocation;
        }

        //Validate delete functionality of allocation after accepting confirmation pop up
        public string ValidateDeleteFunctionalityOfPeriodAllocationAfterAccepting()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            string value = driver.FindElement(valAllocationL).Text;
            driver.FindElement(delAllocationL).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            string allocation = driver.FindElement(valAllocationL).Displayed.ToString();
            return allocation;
        }

        //Validate delete functionality of Period Position after clicking No on confirmation pop up
        public string ValidateDeleteFunctionalityOfPeriodPositionAfterSelectingNo()
        {
            Thread.Sleep(5000);
            driver.FindElement(delPositionL).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Dismiss();
            WebDriverWaits.WaitUntilEleVisible(driver, delPositionL, 190);
            string period = driver.FindElement(delPositionL).Displayed.ToString();
            return period;
        }

        //Validate delete functionality of Period Position after accepting on confirmation pop up
        public string ValidateDeleteFunctionalityOfPeriodPositionAfterAccepting()
        {
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            driver.FindElement(delPositionL).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(1);
            Thread.Sleep(5000);
            string period = driver.FindElement(delPositionL).Displayed.ToString();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(5000);
            return period;
        }
    }
}


