using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System.Linq;
using System.Threading;

namespace SF_Automation.Pages.Contact
{
    class CoverageTeamDetail : BaseClass
    {
        By coverageTeamDetailHeading = By.CssSelector("h2[class='mainTitle']");
        By offsiteTemplateEditHeading = By.CssSelector("h2[class='mainTitle']");
        By btnNewOffsiteTemplate = By.CssSelector("input[value='New Offsite Template']");
        By txtCurrentYearStrategy = By.CssSelector("textarea[id*='GbCS3']");
        By txtPotentialRevenue = By.CssSelector("input[id*='GbCSD']");
        By txtRevenueComment = By.CssSelector("textarea[id*='GbCSF']");
        By txtNumberOfDeals = By.CssSelector("textarea[id*='GbCSB']");
        By txtRelationshipMetricsComments = By.CssSelector("textarea[id*='GbCSE']");
        By txtFaceToFace = By.CssSelector("input[id*='GbCS7']");
        By txtTimeSpent = By.CssSelector("input[id*='GbCSK']");
        By txtOtherProposal = By.CssSelector("input[id*='GbCSC']");
        By txtFundName = By.CssSelector("input[id*='GbCS8']");
        By btnCancel = By.CssSelector("td[id='bottomButtonRow'] > input[value='Cancel']");
        By btnSave = By.CssSelector("td[id='bottomButtonRow'] > input[value=' Save ']");
        By OffsiteTemplateRecords = By.CssSelector("div[id*='offsiteTemplateList_body'] > table > tbody > tr:nth-child(2)");
        By offsiteTemplateDetailHeading = By.CssSelector("h2[class='mainTitle']");
        By valCurrentYearStrategy = By.CssSelector("div[id*='00N3100000GbCS3']");
        By valRevenueComments = By.CssSelector("div[id*='00N3100000GbCSF']");
        By valPotentialRevenue = By.CssSelector("div[id*='GbCSD']");
        By valRelationshipMetricsComments = By.CssSelector("div[id*='GbCSE']");
        By valFundName = By.CssSelector("div[id*='GbCS8']");
        By lnkDelete = By.CssSelector("td[class='actionColumn'] > a:nth-child(2)");
        By idCoverageL = By.XPath("//p[@title='Coverage #']/..//lightning-formatted-text");
        By txtCoverageTeamStatusL = By.XPath("//span[text()='Coverage Team Status']/../../..//lightning-formatted-text");
        By inputCoverageTeamCommentsL = By.XPath("//label[text()='Comment']/..//textarea");
        By btnSaveCommentsL = By.XPath("//div[contains(@class,'comment')]//button[@name='save']");
        By txtCoverageTeamCommentsL = By.XPath("//dt[text()='Comment:']/..//lightning-base-formatted-text");
        By iconShowMoreActionsL = By.XPath("//article[@aria-label='Coverage Team Comments']//article//button[contains(@class,'slds-button_icon-border')]");
        By iconShowMoreActionCSectorL = By.XPath("//article[@aria-label='Coverage Sectors']//button[contains(@class,'slds-button_icon-border')]");
        By iconShowMoreActionCContactsL = By.XPath("//article[@aria-label='Coverage Contacts']//button[contains(@class,'slds-button_icon-border')]");
        By iconShowMoreActionsCContactL = By.XPath("//article[@aria-label='Coverage Contacts']//div//article//button[contains(@class,'slds-button_icon-border')]");
        //By lnkEditL = By.XPath("//div[@title='Edit']/..");
        //By lnkDeleteL = By.XPath("//div[@title='Delete']/..");
        By btnDeleteConfimL = By.XPath("//h1[text()='Delete Coverage Team Comment']/../..//button[@title='Delete']");
        By inputEditCovCommentsL = By.XPath("//h2[contains(text(),'Edit')]/../..//label[text()='Comment']/..//textarea");
        By btnDeleteCoverageTeamL = By.XPath("//h1//records-entity-label[text()='Coverage Team']//ancestor::div[contains(@class,'primaryFieldRow')]//li//button[text()='Delete']");
        By btnConfirmDeleteL = By.XPath("//button[@title='Delete']");
        By panelCoverageSectorsL = By.XPath("//ul[@role='tablist']//li[contains(@title,'Coverage Sectors')]//a");
        By panelCoverageContactsL = By.XPath("//ul[@role='tablist']//li[contains(@title,'Coverage Contacts')]//a");
        By btnNewCoverageContactsL = By.XPath("//article[@aria-label='Coverage Contacts']//button[contains(@class,'slds-button_icon-border')]/..//a//span[text()='Add Coverage Contact']"); 
        By btnSaveDetailsL = By.XPath("//button[@name='SaveEdit']");
        By btnNewCoverageSectionL = By.XPath("//article[@aria-label='Coverage Sectors']//button[contains(@class,'slds-button_icon-border')]/..//a//span[text()='New']");
        By inputCoverageSectorDepL = By.XPath("//label[text()='Coverage Sector Dependency']/..//input");
        By inputCoverageContactsL = By.XPath("//label[text()='Coverage Contact']/..//input");
        By inputCoverageTeamMemberL = By.XPath("//label[text()='Coverage Team Member']/..//input");
        By comboCoverageContactFocusL = By.XPath("//label[text()='Focus']/..//button");
        By chkIsMainL = By.XPath("//input[@name='IsMain__c']/..");//span[text()='Is Main']/../../div");
        By chkboxIsMainL = By.XPath("//input[@name='IsMain__c']"); //span[text()='Is Main']/../../div//input");
        By toastMsgPopup = By.XPath("//span[contains(@class,'toastMessage')]");
        By txtSectorDepCmpNameL = By.XPath("//records-entity-label[text()='Coverage Sector']/../../..//lightning-formatted-text");
        By txtCoverageContactIDL= By.XPath("//records-entity-label[text()='Coverage Contact']/../../..//lightning-formatted-text");
        By iconMoreIconsectorsL = By.XPath("//article[@aria-label='Coverage Sectors']//div//article//li//a[@title='Show one more action']");
        By lnkCoverageSectorCompanyNameL = By.XPath("//article[@aria-label='Coverage Sectors']//article//h3//a");
        By lnkEditL = By.XPath("//div[contains(@class,'uiMenuList--default visible positioned')]//div[@title='Edit']/..");
        By lnkDeleteL= By.XPath("//div[contains(@class,'uiMenuList--default visible positioned')]//div[@title='Delete']/..");
        By iconClearSecDepL = By.XPath("//label[text()='Coverage Sector Dependency']/..//button[@title='Clear Selection']");
        By tabFincncialL = By.XPath("//ul//li//a[@data-label='Financials']");
        By comboYearL = By.XPath("//label[text()='Year']/..//button");
        public void ClickCoverageTeamFinancialTabLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabFincncialL, 10);
            driver.FindElement(tabFincncialL).Click();
        }
        public void AddCompanyFinancialsLV(string year)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboYearL, 10);
            driver.FindElement(comboYearL).Click();
            By elmYear = By.XPath($"//label[text()='Year']/..//lightning-base-combobox-item//span[@title='{year}']");
            WebDriverWaits.WaitUntilEleVisible(driver, elmYear, 10);
            driver.FindElement(elmYear).Click();
        }
        public void DeleteCoverageSector(string coverageID)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,800)");
            By elmIconMoreAction = By.XPath($"//article[@aria-label='Coverage Sectors']//article[@aria-label='{coverageID}']//slot[@name='rowLevelActions']//lightning-button-menu");
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, elmIconMoreAction, 5);
            driver.FindElement(elmIconMoreAction).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDeleteL, 5);
            driver.FindElement(lnkDeleteL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmDeleteL, 10);
            driver.FindElement(btnConfirmDeleteL).Click();
            Thread.Sleep(3000);
        }
        public string UpdateCoverageSectorDependenciesLV(string coverageID, string secDependency)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,400)");
            By elmIconMoreAction = By.XPath($"//article[@aria-label='Coverage Sectors']//article[@aria-label='{coverageID}']//slot[@name='rowLevelActions']//lightning-button-menu");
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, elmIconMoreAction, 5);
            driver.FindElement(elmIconMoreAction).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditL, 5);
            driver.FindElement(lnkEditL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, iconClearSecDepL, 5);
            driver.FindElement(iconClearSecDepL).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, inputCoverageSectorDepL, 5);
            driver.FindElement(inputCoverageSectorDepL).SendKeys(secDependency);
            By elmDependency = By.XPath($"//label[text()='Coverage Sector Dependency']/..//lightning-base-combobox-formatted-text[@title='{secDependency}']");
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, elmDependency, 10);
            driver.FindElement(elmDependency).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 5);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            return toasMsg;
        }

        public void DeleteCoverageSectorLV()
        {

        }
        public string GetCoverageSectorDependencyNameLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSectorDepCmpNameL, 10);
            return driver.FindElement(txtSectorDepCmpNameL).Text;
        }
        public string GetCoverageContactIDLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCoverageContactIDL, 10);
            return driver.FindElement(txtCoverageContactIDL).Text;
        }
        public string AddNewCoverageSectorLV(string secDependency)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chkIsMainL, 5); 
            driver.FindElement(chkboxIsMainL).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, inputCoverageSectorDepL, 5);
            driver.FindElement(inputCoverageSectorDepL).SendKeys(secDependency);
            By elmDependency = By.XPath($"//label[text()='Coverage Sector Dependency']/..//lightning-base-combobox-formatted-text[@title='{secDependency}']");
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, elmDependency, 10);
            driver.FindElement(elmDependency).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 5);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
             return toasMsg;
        }
        public string AddNewCoverageContactsLV(string contact, string contactFocus, string coverageID)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWaits.WaitUntilEleVisible(driver, inputCoverageContactsL, 5);
            driver.FindElement(inputCoverageContactsL).SendKeys(contact);
            By elmContact = By.XPath($"//label[text()='Coverage Contact']/..//lightning-base-combobox-formatted-text[@title='{contact}']");
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, elmContact, 5);
            driver.FindElement(elmContact).Click();
            driver.FindElement(comboCoverageContactFocusL).Click();
            By elmContactFocus = By.XPath($"//label[text()='Focus']/..//lightning-base-combobox-item//span[@title='{contactFocus}']");
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, elmContactFocus, 5);
            driver.FindElement(elmContactFocus).Click();
            Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo(0,500)");
            driver.FindElement(inputCoverageTeamMemberL).SendKeys(coverageID);
            By elmTeam = By.XPath($"//label[text()='Coverage Team Member']/..//ul//li//lightning-base-combobox-formatted-text[@title='{coverageID}']");
            //By elmTeam = By.XPath("//label[text()='Coverage Team Member']/..//li[2]/lightning-base-combobox-item");
            Thread.Sleep(2000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, elmTeam, 5);
            }
            catch
            {
                driver.FindElement(inputCoverageTeamMemberL).Click();
                driver.FindElement(inputCoverageTeamMemberL).SendKeys(Keys.Backspace);
                //driver.FindElement(inputCoverageTeamMemberL).SendKeys(coverageID);
                WebDriverWaits.WaitUntilEleVisible(driver, elmTeam, 5);
            }
            
            driver.FindElement(elmTeam).Click();

            driver.FindElement(btnSaveDetailsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 5);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(2000);
            return toasMsg;
        }
        public void ClickNewCoverageSectorsSectionButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconShowMoreActionCSectorL, 5);
            driver.FindElement(iconShowMoreActionCSectorL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCoverageSectionL, 5);
            driver.FindElement(btnNewCoverageSectionL).Click();
        }
        public void ClickNewCoverageContactsSectionButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, iconShowMoreActionCContactsL, 5);
            driver.FindElement(iconShowMoreActionCContactsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCoverageContactsL, 5);
            driver.FindElement(btnNewCoverageContactsL).Click();
        }
        public void ClickCoverageSectorPanelLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, panelCoverageSectorsL, 10);
            driver.FindElement(panelCoverageSectorsL).Click();
        }
        public void ClickCoverageContactsPanelLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, panelCoverageContactsL, 10);
            driver.FindElement(panelCoverageContactsL).Click();
        }
        public bool IsDeleteButtonDisplayedLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(3000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCoverageTeamL, 5);
                return true;
            }
            catch { return false; }
        }
        public void DeleteCoverageTeamLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCoverageTeamL, 5);
            driver.FindElement(btnDeleteCoverageTeamL).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmDeleteL, 20);
            driver.FindElement(btnConfirmDeleteL).Click();
            Thread.Sleep(3000);
        }
        public void DeleteCoverageCommentsLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWaits.WaitUntilEleVisible(driver, iconShowMoreActionsL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(iconShowMoreActionsL));
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDeleteL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(lnkDeleteL));
            WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmDeleteL, 10);
            driver.FindElement(btnConfirmDeleteL).Click();
        }
        public void DeleteCoverageContactLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWaits.WaitUntilEleVisible(driver, iconShowMoreActionsCContactL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(iconShowMoreActionsCContactL));
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDeleteL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(lnkDeleteL));
            WebDriverWaits.WaitUntilEleVisible(driver, btnConfirmDeleteL, 10);
            driver.FindElement(btnConfirmDeleteL).Click();
        }

        public string UpdateCoverageContactLV(string focus)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWaits.WaitUntilEleVisible(driver, iconShowMoreActionsCContactL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(iconShowMoreActionsCContactL));
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditL, 5);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(lnkEditL));
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboCoverageContactFocusL, 10);
            driver.FindElement(comboCoverageContactFocusL).Click();
            By elmContactFocus = By.XPath($"//label[text()='Focus']/..//lightning-base-combobox-item//span[@title='{focus}']");
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, elmContactFocus, 5);
            driver.FindElement(elmContactFocus).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 5);
            string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(8000);
            return toasMsg;
        }

        public void UpdateCoverageCommentsLV(string coverageComments)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWaits.WaitUntilEleVisible(driver, iconShowMoreActionsL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(iconShowMoreActionsL));
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditL, 10);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(lnkEditL));
            WebDriverWaits.WaitUntilEleVisible(driver, inputEditCovCommentsL, 10);
            driver.FindElement(inputEditCovCommentsL).Clear();
            driver.FindElement(inputEditCovCommentsL).SendKeys(coverageComments);
            Thread.Sleep(2000);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(8000);
        }

        public string GetCoverageTeamCommentsLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCoverageTeamCommentsL, 10);
            return driver.FindElement(txtCoverageTeamCommentsL).Text;
        }
        public void SaveCoverageTeamCommentsLV(string coverageComments)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, inputCoverageTeamCommentsL, 10);
            driver.FindElement(inputCoverageTeamCommentsL).SendKeys(coverageComments);
            driver.FindElement(btnSaveCommentsL).Click();
            //WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 5);
            //string toasMsg = driver.FindElement(toastMsgPopup).Text;
            Thread.Sleep(8000);
            //return toasMsg;
        }

        public string GetCoverageTeamIDLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, idCoverageL, 10);
            return driver.FindElement(idCoverageL).Text;
        }
        public string GetCoverageTeamStatusLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCoverageTeamStatusL, 10);
            return driver.FindElement(txtCoverageTeamStatusL).Text;
        }
        //Get heading of coverage team details page
        public string GetCoverageTeamDetailsHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, coverageTeamDetailHeading, 60);
            string headingCoverageTeamDetail = driver.FindElement(coverageTeamDetailHeading).Text;
            return headingCoverageTeamDetail;
        }
        //Click on new offsite template button
        public void ClickNewOffsiteTemplateButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewOffsiteTemplate);
            driver.FindElement(btnNewOffsiteTemplate).Click();
        }

        //Get heading of coverage team details page
        public string GetOffsiteTemplateHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, offsiteTemplateEditHeading, 60);
            string headingOffsiteTemplateEdit = driver.FindElement(offsiteTemplateEditHeading).Text;
            return headingOffsiteTemplateEdit;
        }

        //Enter offsite template details function
        public void EnterOffsiteTemplateDetails(string file)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtCurrentYearStrategy, 40);
            driver.FindElement(txtCurrentYearStrategy).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 3));

            WebDriverWaits.WaitUntilEleVisible(driver, txtPotentialRevenue, 40);
            driver.FindElement(txtPotentialRevenue).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 4));

            WebDriverWaits.WaitUntilEleVisible(driver, txtRevenueComment, 40);
            driver.FindElement(txtRevenueComment).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 5));

            WebDriverWaits.WaitUntilEleVisible(driver, txtNumberOfDeals, 40);
            driver.FindElement(txtNumberOfDeals).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 6));

            WebDriverWaits.WaitUntilEleVisible(driver, txtRelationshipMetricsComments, 40);
            driver.FindElement(txtRelationshipMetricsComments).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 7));

            WebDriverWaits.WaitUntilEleVisible(driver, txtFaceToFace, 40);
            driver.FindElement(txtFaceToFace).Clear();
            driver.FindElement(txtFaceToFace).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 8));

            WebDriverWaits.WaitUntilEleVisible(driver, txtTimeSpent, 40);
            driver.FindElement(txtTimeSpent).Clear();
            driver.FindElement(txtTimeSpent).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 9));

            WebDriverWaits.WaitUntilEleVisible(driver, txtOtherProposal, 40);
            driver.FindElement(txtOtherProposal).Clear();
            driver.FindElement(txtOtherProposal).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 10));

            WebDriverWaits.WaitUntilEleVisible(driver, txtFundName, 40);
            driver.FindElement(txtFundName).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageTeam", 11));
        }

        // Click cancel button function
        public void ClickCancel()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancel);
            driver.FindElement(btnCancel).Click();
        }

        public bool ValidateOffsiteTemplateCreation()
        {
            return CustomFunctions.IsElementPresent(driver, OffsiteTemplateRecords);
        }

        // Click save button function
        public void ClickSave()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }

        // Get heading of offsite template detail page
        public string GetOffsiteTemplateDetailHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, offsiteTemplateDetailHeading, 60);
            string headingOffsiteTemplateDetail = driver.FindElement(offsiteTemplateDetailHeading).Text;
            return headingOffsiteTemplateDetail;
        }

        //Get value of current year strategy from offsite detail page
        public string GetCurrentYearStrategyValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCurrentYearStrategy, 60);
            string valueCurrentYearStr = driver.FindElement(valCurrentYearStrategy).Text;
            return valueCurrentYearStr;
        }

        //Get value of current year strategy from offsite detail page
        public string GetRevenueCommentValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRevenueComments, 60);
            string valueRevenueComments = driver.FindElement(valRevenueComments).Text;
            return valueRevenueComments;
        }

        //Function to get relationship metrics comments
        public string GetRelationshipMetricsComments()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRelationshipMetricsComments, 60);
            string relationshipMetricsComments = driver.FindElement(valRelationshipMetricsComments).Text;
            return relationshipMetricsComments;
        }

        //Function to get fund name
        public string GetFundName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valFundName, 60);
            string valueFundName = driver.FindElement(valFundName).Text;
            return valueFundName;
        }

        //Delete offsite templates
        public void DeleteOffsiteTemplate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDelete);
            driver.FindElement(lnkDelete).Click();
            Thread.Sleep(1000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(1000);
        }
    }
}
