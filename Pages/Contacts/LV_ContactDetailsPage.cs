using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SF_Automation.TestCases.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace SF_Automation.Pages.Contact
{
    class LV_ContactDetailsPage : BaseClass
    {
        //General elements
        By txtContactName = By.XPath("//lightning-formatted-name[@slot='primaryField']");
        By btnCloseDuplicateCompanyAlertDialogBox = By.XPath("//button[@title='Close']");
        By linkImportantDates = By.XPath("//a[text()='Important Dates']");

        //Buttons for CF Financial User
        By btnEdit = By.XPath("//button[@name='Edit']");
        By btnAddRelationshipL = By.XPath("//button[text()='Add Relationship L']");
        By btnAddActivity = By.XPath("//button[text()='Add Activity']");
        By btnPrintableView = By.XPath("//button[text()='Printable View']");
        By lblActivityDetails = By.XPath("//div[@class='pbSubheader brandTertiaryBgr first tertiaryPalette']/h3");

        //Buttons for System Admin User
        By btnAddRelationship = By.XPath("//button[text()='Add Relationship']");
        By btnTearsheet = By.XPath("//button[text()='Tearsheet']");
        By btnContactReportsM = By.XPath("//button[text()='Contact Reports M']");
        By btnDelete = By.XPath("//button[text()='Delete']");
        By btnSubmitForApproval = By.XPath("//button[text()='Submit for Approval']");

        //Quick Links
        By linkHLRelationships = By.XPath("(//span[contains(text(),'HL Relationships')]/../..)[1]");
        By linkIndustryFocus = By.XPath("(//span[contains(text(),'Industry Focus')]/../..)[1]");
        By linkOpportunityContacts = By.XPath("(//span[contains(text(),'Opportunity Contacts')]/../..)[1]");
        By linkEngagementContacts = By.XPath("(//span[contains(text(),'Engagement Contacts')]/../..)[1]");
        By linkEngagementsShown = By.XPath("(//span[contains(text(),'Engagements Shown')]/../..)[1]");
        By linkAffiliatedCompanies = By.XPath("(//span[contains(text(),'Affiliated Companies')]/../..)[1]");
        By linkMemberships = By.XPath("(//span[contains(text(),'Memberships')]/../..)[1]");
        By linkContactSectors = By.XPath("(//span[contains(text(),'Contact Sectors')]/../..)[1]");
        By linkCampaignHistory = By.XPath("(//span[contains(text(),'Campaign History')]/../..)[1]");
        By linkContactEmailHistory = By.XPath("(//span[contains(text(),'Contact Email History')]/../..)[1]");
        By linkContactSources = By.XPath("(//span[contains(text(),'Contact Sources')]/../..)[1]");
        By linkDevelopmentLeads = By.XPath("(//span[contains(text(),'Development Leads')]/../..)[1]");
        By linkFiles = By.XPath("(//span[contains(text(),'Files')]/../..)[1]");
        By linkContactHistory = By.XPath("(//span[contains(text(),'Contact History')]/../..)[1]");

        //Add Relationship
        By txtLookupEmployee = By.XPath("//label[contains(text(),'Lookup')]/../input");
        By dropdownStrengthRating = By.XPath("//p[contains(text(),'Strength')]/../select");
        By dropdownType = By.XPath("//p[contains(text(),'Type')]/../select");
        By txtPersonalNote = By.XPath("//p[contains(text(),'Personal')]/../textarea");
        By txtOutlookCategories = By.XPath("//p[contains(text(),'Outlook')]/../textarea");
        By btnCreateRelationship = By.XPath("//span[contains(text(),'Create Relationship')]/..");
        By btnAddRelationshipPopupCancel = By.XPath("(//span[contains(text(),'Cancel')])[3]/..");

        //Add Relationship L
        By txtHLContact = By.XPath("//span[contains(text(),'HL Contact')]/../../div/div/div/div/input");
        By linkStrengthRating = By.XPath("//span[contains(text(),'Strength')]/../../div/div/div/div/a");
        By linkType = By.XPath("//span[contains(text(),'Type')]/../../div/div/div/div/a");
        By txtAreaPersonalNote = By.XPath("//span[contains(text(),'Personal Note')]/../../textarea");
        By txtAreaOutlookCategories = By.XPath("//span[contains(text(),'Outlook Categories')]/../../textarea");
        By txtOutlookCategoriesEdit = By.XPath("//label[contains(text(),'Outlook Categories')]/../div/textarea");
        By btnSave = By.XPath("(//span[contains(text(),'Save')])[4]/..");
        By btnSaveEdit = By.XPath("//button[@name='SaveEdit']");
        By btnCancel = By.XPath("(//span[contains(text(),'Cancel')])[3]/..");
        By btnCancelEdit = By.XPath("//button[@name='CancelEdit']");

        //Tabs for CF Financial User
        By tabInfo = By.XPath("//a[@data-label='Info']");
        By tabRelationships = By.XPath("//a[@data-label='Relationships']");
        By tabCoverage = By.XPath("//a[@data-label='Coverage']");
        By tabActivity = By.XPath("//a[@data-label='Activity']");
        By tabCampaignHistory = By.XPath("//a[@data-label='Campaign History']");
        By tabHistory = By.XPath("//a[@data-label='History']");

        //Tabs for System Admin User
        By tabDetails = By.XPath("//a[@data-label='Details']");
        By tabRelated = By.XPath("//a[@data-label='Related']");
        By tabNews = By.XPath("//a[@data-label='News']");

        //Relationship Details
        By linkViewDetails = By.XPath("//a[text()='View Details']");
        By btnEditRelationshipL = By.XPath("(//button[text()='Edit'])[2]");
        By btnDeleteEditRelationshipL = By.XPath("//button[text()='Delete']");

        //Activity Details
        By lblGetPrimaryContactNameFromActivity = By.XPath("//tbody[@id='j_id0:j_id1:j_id2:j_id3:pbActivityLog:pbtActivities:tb']/tr[1]/td[5]/span/a");
        By linkEditActivity = By.XPath("//tbody[@id='j_id0:j_id1:j_id2:j_id3:pbActivityLog:pbtActivities:tb']/tr[1]/td[1]/span/a[2]");

        //Contact Informaction section
        By lblContactName = By.XPath("(//span[text()='Name'])[2]/../../../dd/div/span/slot/lightning-formatted-name");
        By associatedEngagementsIcon = By.XPath("(//lightning-icon[@icon-name='utility:new_window'])[1]");
        By txtCloseDate = By.XPath("((//span[text()='Close Date'])[2]/following::div/span)[1]/slot/lightning-formatted-text");

        //HL Employee buttons
        By lobButton = By.XPath("(//label[text()='Line of Business'])[2]/../div//button");
        By lblIndustryGroup = By.XPath("//label[text()='Industry Group']");

        //Edit HL Contact elements
        By btnSaveOnEdit = By.XPath("(//button[contains(text(),'Save')])[2]");
        By btnCancelOnEdit = By.XPath("//button[@name='CancelEdit']");
        By inputCompanyName = By.XPath("//input[@placeholder='Search Companies...']");
        By txtErrorPopup = By.XPath("//div[@class='genericNotification']/../ul/li");

        public void CloseTab(string tabName)
        {
            Thread.Sleep(5000);
            driver.FindElement(By.XPath($"//button[contains(@title,'Close {tabName}')]")).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyButtonsDisplayedAtTheTopOfExternalContactDetailsPageForCFFinancialUser()
        {
            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver,btnEdit,120);
            if(driver.FindElement(btnEdit).Displayed && driver.FindElement(btnAddRelationshipL).Displayed && driver.FindElement(btnPrintableView).Displayed)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyDetailsDisplayedAtTheTopBarForExternalContact()
        {
            bool result = false;
            int recordCount = driver.FindElements(By.XPath("//slot[@name='secondaryFields']/records-highlights-details-item")).Count;

            for (int i = 1; i <= recordCount; i++)
            {
                string val = driver.FindElement(By.XPath($"//slot[@name='secondaryFields']/records-highlights-details-item[{i}]/div/p[1]")).Text;
                if(val=="Company Name" || val=="Title" || val == "Email")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        public bool VerifyTabsDisplayedInRightSideForExternalContact(string file)
        {
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            int excelCount = ReadExcelData.GetRowCount(excelPath, "ExternalContactTabs");
            int tabCount = driver.FindElements(By.XPath("//ul[@role='tablist']/li")).Count;

            for (int i = 2; i <= excelCount; i++)
            {
                string excelTabName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExternalContactTabs", i, 1);
                for (int j=1; j<=tabCount; j++)
                {
                    string tabName = driver.FindElement(By.XPath($"//ul[@role='tablist']/li[{j}]/a")).Text;
                    if(tabName == excelTabName)
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        result = false;
                    }
                }
                continue;
            }

            return result;
        }

        public bool VerifyFlagContactAndCompanyDetailSectionsAreDisplayedInRightSideForExternalContact(string file)
        {
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            int excelCount = ReadExcelData.GetRowCount(excelPath, "ExternalContactSections");
            int tabCount = driver.FindElements(By.XPath("(//flexipage-tab2[@class='slds-tabs_default__content slds-show'])[2]/slot/flexipage-component2")).Count;

            for (int i = 2; i <= excelCount; i++)
            {
                string excelSectionName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExternalContactSections", i, 1);
                for (int j = 1; j <= tabCount; j++)
                {
                    string sectionName;
                    if(j==1)
                    {
                        sectionName = driver.FindElement(By.XPath($"(((//flexipage-tab2[@class='slds-tabs_default__content slds-show'])[2]/slot/flexipage-component2)[{j}]//div[2]/lightning-formatted-text)[1]")).Text;
                    }
                    else
                    {
                        sectionName = driver.FindElement(By.XPath($"(((//flexipage-tab2[@class='slds-tabs_default__content slds-show'])[2]/slot/flexipage-component2)[{j}]//div[2]/a)[1]")).Text;
                    }
                    if (sectionName == excelSectionName)
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        result = false;
                    }
                }
                continue;
            }

            return result;
        }

        public bool VerifyIndustryGroupErrorMessageWhenLOBIsCF()
        {
            bool result = false;

            WebDriverWaits.WaitForPageToLoad(driver, 120);
            WebDriverWaits.WaitUntilClickable(driver, btnEdit, 120);

            driver.FindElement(btnEdit).Click();
            Thread.Sleep(8000);

            try
            {
                By elePO = By.XPath("(//button[@aria-label='Industry Group'])[2]");
                CustomFunctions.MoveToElement(driver, driver.FindElement(elePO));
                Thread.Sleep(3000);
            }
            catch(Exception)
            {
                By elePO = By.XPath("(//button[@aria-label='Industry Group'])[1]");
                CustomFunctions.MoveToElement(driver, driver.FindElement(elePO));
                Thread.Sleep(3000);
            }

            By elePO1 = By.XPath("(//label[text()='Line of Business'])[2]/..//lightning-base-combobox");
            driver.FindElement(elePO1).Click();
            Thread.Sleep(2000);

            By elePO2 = By.XPath("(//label[text()='Line of Business']/following::lightning-base-combobox-item)[2][@data-value='CF']");
            driver.FindElement(elePO2).Click();
            Thread.Sleep(2000);

            driver.FindElement(btnSaveOnEdit).Click();
            Thread.Sleep(5000);

            string msg = driver.FindElement(By.XPath("//label[text()='Industry Group']/../div[@aria-live='assertive']")).Text;
            if(msg == "Industry Group must be selected when LOB is CF")
            {
                result = true;
                driver.FindElement(btnCancelOnEdit).Click();
                Thread.Sleep(3000);
            }
            return result;
        }

        public bool VerifyUserNavigatedToAddActivityPageForExternalContact()
        {
            bool result = false;

            WebDriverWaits.WaitUntilEleVisible(driver,btnAddActivity, 120);
            driver.FindElement(btnAddActivity).Click();
            Thread.Sleep(10000);

            int framCount = driver.FindElements(By.XPath("//iframe")).Count;
            if(framCount>0)
            {
                driver.SwitchTo().Frame(framCount - 1);
            }

            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblActivityDetails);
            if(driver.FindElement(lblActivityDetails).Text=="Activity Details")
            {
                result = true;
            }
            return result;
        }

        public void NavigateToActivityTabInsideExternalContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabActivity, 60);
            driver.FindElement(tabActivity).Click();
            Thread.Sleep(5000);
        }

        public void NavigateToActivityTabInsideCFFinancialUser()
        {
            WebDriverWaits.WaitUntilEleVisible(driver,tabActivity,60);
            driver.FindElement(tabActivity).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyCreatedActivityDisplayedUnderExternalContact(string extName)
        {
            bool result = false;
            int framCount = driver.FindElements(By.XPath("//iframe")).Count;
            if(framCount > 0)
            {
                driver.SwitchTo().Frame(framCount - 1);
                Thread.Sleep(2000);
            }

            WebDriverWaits.WaitUntilEleVisible(driver,lblGetPrimaryContactNameFromActivity,60);
            string name = driver.FindElement(lblGetPrimaryContactNameFromActivity).Text;
            if(name==extName)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyCFFinancialUserIsAbleToEditActivity()
        {
            bool result = false;
            int framCount = driver.FindElements(By.XPath("//iframe")).Count;
            if(framCount > 0)
            {
                driver.SwitchTo().Frame(framCount - 1);
            }

            WebDriverWaits.WaitUntilEleVisible(driver,linkEditActivity,60);
            if(driver.FindElement(linkEditActivity).Displayed)
            {
                driver.FindElement(linkEditActivity).Click();
                result = true;
            }
            return result;
        }

        public bool VerifyCreatedActivityDisplayedUnderCFFinancialUser(string extName)
        {
            bool result = false;

            int framCount = driver.FindElements(By.XPath("//iframe")).Count;
            if(framCount > 0)
            {
                driver.SwitchTo().Frame(framCount - 1);
            }

            WebDriverWaits.WaitUntilEleVisible(driver,lblGetPrimaryContactNameFromActivity,60);
            string name = driver.FindElement(lblGetPrimaryContactNameFromActivity).Text;
            if(name == extName)
            {
                result = true;
            }
            return result;
        }

        public string GetExternalContactName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver,lblContactName,60);
            return driver.FindElement(lblContactName).Text;
        }

        public bool VerifyTabsDisplayedOnExternalContactDetailPageForCFFinancialUser()
        {
            bool result = false;
            if(driver.FindElement(tabInfo).Displayed && driver.FindElement(tabRelationships).Displayed && driver.FindElement(tabCoverage).Displayed && driver.FindElement(tabActivity).Displayed && driver.FindElement(tabCampaignHistory).Displayed && driver.FindElement(tabHistory).Displayed)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyQuickLinksOnContactDetailPageForSysAdminUser(string file)
        {
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(5000);

            //Get no of quick links
            int linkCount = driver.FindElements(By.XPath("(//ul[@class='slds-grid slds-wrap list'])[1]/li")).Count;

            //Get the count from excel
            int excelLinkCount = ReadExcelData.GetRowCount(excelPath,"QuickLinks");

            for(int i = 2;i <= excelLinkCount;i++)
            {
                string excelLinkName = ReadExcelData.ReadDataMultipleRows(excelPath,"QuickLinks",i,1);
                for(int j=1;j<=linkCount;j++)
                {
                    string linkName = driver.FindElement(By.XPath($"(//ul[@class='slds-grid slds-wrap list'])[1]/li[{j}]/lst-related-list-quick-link/div/div/records-hoverable-link/div/a/slot/span")).Text;
                    if(linkName.Contains(excelLinkName))
                    {
                        result = true;
                        break;
                    }
                }
                continue;
            }
            return result;
        }

        public bool VerifyTabsDisplayedOnExternalContactDetailPageForSysAdminUser()
        {
            bool result = false;
            if(driver.FindElement(tabDetails).Displayed && driver.FindElement(tabRelated).Displayed && driver.FindElement(tabNews).Displayed)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyButtonsDisplayedAtTheTopOfExternalContactDetailsPageForSysAdminUser()
        {
            bool result = false;
            if(driver.FindElement(btnAddRelationship).Displayed && driver.FindElement(btnTearsheet).Displayed && driver.FindElement(btnContactReportsM).Displayed && driver.FindElement(btnDelete).Displayed && driver.FindElement(btnSubmitForApproval).Displayed)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyIfClickingOnQuickLinksLeadsToRespectivePages(string file)
        {
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(5000);

            //Get no of quick links
            int linkCount = driver.FindElements(By.XPath("(//ul[@class='slds-grid slds-wrap list'])[1]/li")).Count;

            //Get the count from excel
            int excelLinkCount = ReadExcelData.GetRowCount(excelPath,"QuickLinks");

            for(int i = 2;i <= excelLinkCount;i++)
            {
                string excelLinkName = ReadExcelData.ReadDataMultipleRows(excelPath,"QuickLinks",i,1);
                for(int j = 1;j <= linkCount;j++)
                {
                    string linkName = driver.FindElement(By.XPath($"(//ul[@class='slds-grid slds-wrap list'])[1]/li[{j}]/lst-related-list-quick-link/div/div/records-hoverable-link/div/a/slot/span")).Text;
                    if(linkName.Contains(excelLinkName))
                    {
                        driver.FindElement(By.XPath($"(//span[contains(text(),'{excelLinkName}')]/../..)[1]")).Click();
                        Thread.Sleep(6000);
                        string pageHeading = driver.FindElement(By.XPath("//h1[@class='slds-page-header__title listViewTitle slds-truncate']")).Text;
                        if(linkName.Contains(pageHeading))
                        {
                            CloseTab(pageHeading);
                            result = true;
                            break;
                        }
                        else
                        {
                            //CloseTab(pageHeading);
                            result = false;
                        }
                    }
                }
                continue;
            }
            return result;
        }

        public void AddNewRelationshipUsingAddRelationshipButtonForSysAdmin(string file)
        {
            WebDriverWaits.WaitUntilEleVisible(driver,btnAddRelationship,120);
            driver.FindElement(btnAddRelationship).Click();
            Thread.Sleep(5000);

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string empName = ReadExcelData.ReadData(excelPath,"AddRelationship",1);
            driver.FindElement(txtLookupEmployee).SendKeys(empName);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//li[contains(text(),'{empName}')]")).Click();

            CustomFunctions.SelectByValue(driver,driver.FindElement(dropdownStrengthRating),ReadExcelData.ReadData(excelPath,"AddRelationship",2));
            CustomFunctions.SelectByValue(driver,driver.FindElement(dropdownType),ReadExcelData.ReadData(excelPath,"AddRelationship",3));
            driver.FindElement(txtPersonalNote).SendKeys(ReadExcelData.ReadData(excelPath,"AddRelationship",4));
            driver.FindElement(txtOutlookCategories).SendKeys(ReadExcelData.ReadData(excelPath,"AddRelationship",5));
            driver.FindElement(btnCreateRelationship).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyNewRelationshipVisibleUnderHLRelationshipSectionForSysAdmin(string file)
        {
            bool result = false;
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver,linkHLRelationships,120);
            driver.FindElement(linkHLRelationships).Click();
            Thread.Sleep(5000);
            string empName = ReadExcelData.ReadData(excelPath,"AddRelationship",1);

            if(driver.FindElement(By.XPath($"(//a[@title='{empName}'])[3]")).Displayed)
            {
                result = true;
            }
            return result;
        }

        public void AddNewRelationshipUsingAddRelationshipButtonForCFFinancialUser(string file)
        {
            WebDriverWaits.WaitUntilEleVisible(driver,btnAddRelationshipL,120);
            driver.FindElement(btnAddRelationshipL).Click();
            Thread.Sleep(5000);

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string empName = ReadExcelData.ReadData(excelPath,"AddRelationship",1);
            driver.FindElement(txtHLContact).SendKeys(empName);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//div[@title='{empName}']/..")).Click();

            driver.FindElement(txtAreaPersonalNote).SendKeys(ReadExcelData.ReadData(excelPath,"AddRelationship",4));
            driver.FindElement(txtAreaOutlookCategories).SendKeys(ReadExcelData.ReadData(excelPath,"AddRelationship",5));
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyNewRelationshipVisibleUnderHLRelationshipTabCFFinancialUser(string file)
        {
            bool result = false;
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver,tabRelationships,120);
            driver.FindElement(tabRelationships).Click();
            Thread.Sleep(10000);
            string empName = ReadExcelData.ReadData(excelPath,"AddRelationship",1);

            if(driver.FindElement(By.XPath($"(//a[@title='{empName}'])[2]")).Displayed)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyCFFinancialUserIsAbleToEditNewRelationship(string updatedText)
        {
            bool result = false;

            WebDriverWaits.WaitUntilEleVisible(driver,linkViewDetails,120);
            driver.FindElement(linkViewDetails).Click();
            WebDriverWaits.WaitUntilEleVisible(driver,btnEditRelationshipL,120);
            driver.FindElement(btnEditRelationshipL).Click();

            WebDriverWaits.WaitUntilEleVisible(driver,txtOutlookCategoriesEdit,120);
            driver.FindElement(txtOutlookCategoriesEdit).Clear();
            driver.FindElement(txtOutlookCategoriesEdit).SendKeys(updatedText);

            driver.FindElement(btnSaveEdit).Click();
            Thread.Sleep(5000);
            result = true;
            return result;
        }

        public bool VerifyUserLandedOnCorrectContactDetailsPage(string contactName)
        {
            bool result=false;

            WebDriverWaits.WaitUntilEleVisible(driver, txtContactName, 120);
            string name=driver.FindElement(txtContactName).Text;
            if(name.Contains(contactName))
            {
                result = true;
            }
            return result;
        }

        public bool VerifyTheAvailableSectionsUnderRelationshipTreeMapOnContactDetailsPage(string path)
        {
            bool result=false;

            Thread.Sleep(5000);
            int exlRowCount = ReadExcelData.GetRowCount(path, "Sections");
            for(int i = 1; i<=exlRowCount; i++)
            {
                string exlSectionName = ReadExcelData.ReadDataMultipleRows(path, "Sections", i, 1);
                string sfSectionName = driver.FindElement(By.XPath($"(//span[@class='slds-icon_container slds-m-right_small']/following::span/b)[{i}]")).Text;
                if(exlSectionName==sfSectionName)
                {
                    result=true;
                }
                else
                {
                    result=false;
                }
            }
            if(result==true)
            {
                Assert.IsTrue(driver.FindElement(By.XPath("//p[@title='Company Name']")).Displayed && driver.FindElement(By.XPath("//p[@title='Company Name']")).Text=="Company Name");
            }
            return result;
        }

        public bool VerifyTheAvailableFieldsUnderContactInformationSectionOnContactDetailsPage(string path)
        {
            bool result = false;

            int exlRowCount = ReadExcelData.GetRowCount(path, "ContactInfoFields");
            for(int i = 1; i <= exlRowCount; i++)
            {
                string exlFieldName = ReadExcelData.ReadDataMultipleRows(path, "ContactInfoFields", i, 1);
                string sfFieldName = driver.FindElement(By.XPath($"(//records-highlights-details-item[@slot='secondaryFields'])[{i}]/div/p[1]")).Text;
                if(exlFieldName == sfFieldName)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            if (result==true)
            {
                Assert.IsTrue(driver.FindElement(By.XPath("//button[@title='Show more Phone Numbers']/p")).Text == "Phone (2)");
            }
            return result;
        }

        public bool VerifyTheAvailableFieldsUnderTopRelationshipsSectionOnContactDetailsPage(string path)
        {
            bool result = false;

            int exlRowCount = ReadExcelData.GetRowCount(path, "TopRelationships");
            for(int i = 1; i <= exlRowCount; i++)
            {
                string exlFieldName = ReadExcelData.ReadDataMultipleRows(path, "TopRelationships", i, 1);
                string sfFieldName = driver.FindElement(By.XPath($"(//b[text()='Top Relationships']/following::div/dl/dt/p)[{i}]")).Text;
                if(exlFieldName == sfFieldName)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        public bool VerifyContactsUnderTopRelationshipSectionIsSortedCorrectly() 
        {
            bool result = false;

            //Get the no. of contacts under Top Relationship section
            int noOfContacts = driver.FindElements(By.XPath("//b[text()='Top Relationships']/following::div/dl/dt/p[text()='Contact Name: ']")).Count;

            //Get the name of each contact and store in an array
            String[] contactNames = new String[noOfContacts];
            for(int i=0; i< noOfContacts; i++)
            {
                try
                {
                    contactNames[i] = driver.FindElement(By.XPath($"((//b[text()='Top Relationships']/following::div/dl/dt/p[text()='Contact Name: '])[{i + 1}]/following::dd/p/button/b)[1]")).Text;
                }
                catch (Exception)
                {
                    contactNames[i] = driver.FindElement(By.XPath($"((//b[text()='Top Relationships']/following::div/dl/dt/p[text()='Contact Name: '])[{i + 1}]/following::dd/p/button)[1]")).Text;
                }
            }

            //Get star rating value for each contact and store in an array
            String[] starRatingValueForContact = new String[5];
            Int32[] totalStarRatingForContact = new int[noOfContacts];
            int totalStarRating;
            for(int i = 0; i < noOfContacts; i++)
            {
                int k = 0;
                for(int h = 1; h <= 5; h++)
                {
                    try
                    {
                        starRatingValueForContact[k] = driver.FindElement(By.XPath($"(//b[text()='{contactNames[i]}']/following::p[text()='Strength Rating: ']/following::dd/p/lightning-icon)[{h}]/span/lightning-primitive-icon/*")).GetAttribute("data-key");
                        k++;
                    }
                    catch(Exception)
                    {
                        starRatingValueForContact[k] = driver.FindElement(By.XPath($"(//button[text()='{contactNames[i]}']/following::p[text()='Strength Rating: ']/following::dd/p/lightning-icon)[{h}]/span/lightning-primitive-icon/*")).GetAttribute("data-key");
                        k++;
                    }
                }

                totalStarRating = 1;

                //Get star rating for each contact

                for(int p = 0; p < noOfContacts - 1; p++)
                {
                    if(starRatingValueForContact[0] == "favorite")
                    {
                        if(starRatingValueForContact[p] == starRatingValueForContact[p + 1])
                        {
                            totalStarRating++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        totalStarRating = 0;
                    }
                }
                totalStarRatingForContact[i] = totalStarRating;
            }

            //Get the last activity date of each contact and store in an array
            DateTime[] lastActivityDate = new DateTime[noOfContacts];
            String[] sfContactActivityDate = new String[noOfContacts];
            int j = 1;
            for(int i = 0; i < noOfContacts; i++)
            {
                sfContactActivityDate[i] = driver.FindElement(By.XPath($"(//b[text()='Top Relationships']/following::div/dl/dt/p[text()='Contact Name: '])[{j}]/following::dd[5]/p")).Text;
                lastActivityDate[i] = Convert.ToDateTime(sfContactActivityDate[i]);
                j++;
            }

            //Compare the star rating for each contact and verify sorting
            for(int finalCompare = 0; finalCompare < noOfContacts - 1; finalCompare++)
            {
                if(totalStarRatingForContact[finalCompare] < totalStarRatingForContact[finalCompare+1])
                {
                    result = false;
                    break;
                }
                else if(totalStarRatingForContact[finalCompare] > totalStarRatingForContact[finalCompare + 1])
                {
                    result = true;
                    continue;
                }
                else //If star ratings match then
                {
                    //compare last activity dates
                    if(lastActivityDate[finalCompare] < lastActivityDate[finalCompare + 1])
                    {
                        result = false;
                        break;
                    }
                    else if(lastActivityDate[finalCompare] > lastActivityDate[finalCompare + 1])
                    {
                        result = true;
                        continue;
                    }
                    else //If last activity date also matches then
                    {
                        result = true;
                        continue;
                    }
                }
            }

            return result;
        }

        public bool VerifyContactNameUnderTopRelationshipIsBoldIfStrengthRatingIsStrongAndActivityDateIsWithinLastMonth()
        {
            bool result = false;

            //Get the no. of contacts under Top Relationship section
            int noOfContacts = driver.FindElements(By.XPath("//b[text()='Top Relationships']/following::div/dl/dt/p[text()='Contact Name: ']")).Count;

            //Get the name of each contact and store in an array
            String[] contactNames = new String[noOfContacts];
            for(int i = 0; i < noOfContacts; i++)
            {
                try
                {
                    contactNames[i] = driver.FindElement(By.XPath($"((//b[text()='Top Relationships']/following::div/dl/dt/p[text()='Contact Name: '])[{i + 1}]/following::dd/p/button/b)[1]")).Text;
                }
                catch(Exception)
                {
                    contactNames[i] = driver.FindElement(By.XPath($"((//b[text()='Top Relationships']/following::div/dl/dt/p[text()='Contact Name: '])[{i + 1}]/following::dd/p/button)[1]")).Text;
                }
            }

            //Get star rating value for each contact and store in an array
            String[] starRatingValueForContact = new String[5];
            Int32[] totalStarRatingForContact = new int[noOfContacts];
            int totalStarRating;
            for(int i = 0; i < noOfContacts; i++)
            {
                int k = 0;
                for(int h = 1; h <= 5; h++)
                {
                    try
                    {
                        starRatingValueForContact[k] = driver.FindElement(By.XPath($"(//b[text()='{contactNames[i]}']/following::p[text()='Strength Rating: ']/following::dd/p/lightning-icon)[{h}]/span/lightning-primitive-icon/*")).GetAttribute("data-key");
                        k++;
                    }
                    catch(Exception)
                    {
                        starRatingValueForContact[k] = driver.FindElement(By.XPath($"(//button[text()='{contactNames[i]}']/following::p[text()='Strength Rating: ']/following::dd/p/lightning-icon)[{h}]/span/lightning-primitive-icon/*")).GetAttribute("data-key");
                        k++;
                    }
                }

                totalStarRating = 1;

                //Get star rating for each contact

                for(int p = 0; p < noOfContacts - 1; p++)
                {
                    if(starRatingValueForContact[0] == "favorite")
                    {
                        if(starRatingValueForContact[p] == starRatingValueForContact[p + 1])
                        {
                            totalStarRating++;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        totalStarRating = 0;
                    }
                }
                totalStarRatingForContact[i] = totalStarRating;
            }

            //Get the last activity date of each contact and store in an array
            DateTime[] lastActivityDate = new DateTime[noOfContacts];
            String[] sfContactActivityDate = new String[noOfContacts];
            int j = 1;
            for(int i = 0; i < noOfContacts; i++)
            {
                sfContactActivityDate[i] = driver.FindElement(By.XPath($"(//b[text()='Top Relationships']/following::div/dl/dt/p[text()='Contact Name: '])[{j}]/following::dd[5]/p")).Text;
                lastActivityDate[i] = Convert.ToDateTime(sfContactActivityDate[i]);
                j++;
            }

            //Validate the expected result of the function
            for(int i = 0; i < noOfContacts; i++)
            {
                if(totalStarRatingForContact[i] == 5 && lastActivityDate[i] < DateTime.Today && lastActivityDate[i] > DateTime.Today.AddMonths(-1))
                {
                    string tagName1 = driver.FindElement(By.XPath($"((//b[text()='Top Relationships']/following::div/dl/dt/p[text()='Contact Name: '])[{i + 1}]/following::dd/p/button/b)[1]")).TagName;
                    if(tagName1 == "b")
                    {
                        result = true;
                        continue;
                    }
                    else
                    {
                        result = false;
                        break;
                    }
                }
                else
                {
                    string tagName2 = driver.FindElement(By.XPath($"((//b[text()='Top Relationships']/following::div/dl/dt/p[text()='Contact Name: '])[{i + 1}]/following::dd/p/button)[1]")).TagName;
                    if(tagName2 != "b")
                    {
                        result = true;
                        continue;
                    }
                    else
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public bool VerifyFieldsDisplayedUnderAffiliatedCompaniesSection(string path)
        {
            bool result = false;

            int exlRowCount = ReadExcelData.GetRowCount(path, "AffiliatedCompanies");
            for(int i = 1; i <= exlRowCount; i++)
            {
                string exlFieldName = ReadExcelData.ReadDataMultipleRows(path, "AffiliatedCompanies", i, 1);
                string sfFieldName = driver.FindElement(By.XPath($"(//b[text()='Affiliated Companies ']/following::div/dl/dt/p)[{i}]")).Text;
                if(exlFieldName == sfFieldName)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        public bool VerifyAffiliationTypeIsDisplayedInBoldIfItIsInsideBoardMemberOrOutsideBoardMember()
        {
            bool result = false;

            //Get the no. of companies under Affiliated Companies section
            int noOfCompanies = driver.FindElements(By.XPath("//b[text()='Affiliated Companies ']/following::div/dl/dt/p[text()='Company Name: ']")).Count;

            //Validate the expected result of the function
            for(int i = 1; i <= noOfCompanies; i++)
            {
                //Get the typename
                string typeName = driver.FindElement(By.XPath($"((//b[text()='Affiliated Companies ']/following::div/dl/dt/p[text()='Type: '])[{i}]/following::dd/p)[1]")).Text;
                if(typeName == "Outside Board Member" || typeName == "Inside Board Member")
                {
                    string tagName = driver.FindElement(By.XPath($"((//b[text()='Affiliated Companies ']/following::div/dl/dt/p[text()='Type: '])[{i}]/following::dd/p/b)[1]")).TagName;
                    if(tagName == "b")
                    {
                        result = true;
                        continue;
                    }
                    else
                    {
                        result = false;
                        break;
                    }
                }
                else
                {
                    string tagName1 = driver.FindElement(By.XPath($"((//b[text()='Affiliated Companies ']/following::div/dl/dt/p[text()='Type: '])[{i}]/following::dd/p)[1]")).TagName;
                    if(tagName1 != "b")
                    {
                        result = true;
                        continue;
                    }
                    else
                    {
                        result = false;
                        break;
                    }
                }
            }
            
            return result;
        }

        public bool VerifyFieldsDisplayedUnderAssociatedEngagementsSection(string path)
        {
            bool result = false;

            int exlRowCount = ReadExcelData.GetRowCount(path, "AssociatedEngagements");
            for(int i = 1; i <= exlRowCount; i++)
            {
                string exlFieldName = ReadExcelData.ReadDataMultipleRows(path, "AssociatedEngagements", i, 1);
                string sfFieldName = driver.FindElement(By.XPath($"(//b[text()='Associated Engagements ']/following::div/dl/dt/p)[{i}]")).Text;
                if(exlFieldName == sfFieldName)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        public void CloseDuplicateCompanyAlertMessageDialogBox()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCloseDuplicateCompanyAlertDialogBox, 120);
            driver.FindElement(btnCloseDuplicateCompanyAlertDialogBox).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyIfThereAreBothActiveAndInactiveEngagementsThenOnlyActiveEngagementsAreDisplayedUnderAssociatedEngagementsSection()
        {
            bool result= false;

            //Get the no. of Engagements under Associated Engagements section
            int noOfEngagements = driver.FindElements(By.XPath("//b[text()='Associated Engagements ']/../../div/dl")).Count;

            //Get the name of each Engagement and store in an array
            String[] engagementNames = new String[noOfEngagements];
            int j = 1;

            for(int i = 0; i <= noOfEngagements - 1; i++)
            {
                engagementNames[i] = driver.FindElement(By.XPath($"(//b[text()='Associated Engagements ']/../../div/dl)[{j}]/dd/p/button")).Text;
                j++;
            }

            //Navigate to Total Engagements page
            WebDriverWaits.WaitUntilEleVisible(driver, associatedEngagementsIcon, 120);
            driver.FindElement(associatedEngagementsIcon).Click();
            Thread.Sleep(3000);

            //Get total no of engagements
            int totalAvailableEngagements = driver.FindElements(By.XPath("//table[@aria-label='Engagements']/tbody/tr")).Count;

            //Get the name of each Engagement and store in an array
            ArrayList activeEngagementNames = new ArrayList();

            for(int rowNum = 1; rowNum <= totalAvailableEngagements; rowNum++)
            {
                if(driver.FindElement(By.XPath($"(//table[@aria-label='Engagements']/tbody/tr)[{rowNum}]/td[6]/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/lst-formatted-text")).Text != "Closed")
                {
                    activeEngagementNames.Add(driver.FindElement(By.XPath($"(//table[@aria-label='Engagements']/tbody/tr)[{rowNum}]/th/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/force-lookup/div/records-hoverable-link/div/a/slot/slot/span")).Text);
                }
            }

            //Get the total no of active Engagements
            int totalActiveEngagements = activeEngagementNames.Count;
            string[] myArray = (string[]) activeEngagementNames.ToArray(typeof(string));

            DateTime[] estCloseDate = new DateTime[totalActiveEngagements];

            if(totalActiveEngagements > 5)
            {
                for(int k = 0; k < totalActiveEngagements; k++)
                {
                    //Open active Engagements
                    driver.FindElement(By.XPath($"//span[text()='{myArray[k]}']")).Click();

                    //Navigate to Important Dates tab
                    WebDriverWaits.WaitUntilEleVisible(driver, linkImportantDates, 120);
                    driver.FindElement(linkImportantDates).Click();
                    Thread.Sleep(5000);

                    //Get the Estimated Close Date for each Active Engagement
                    string abc = driver.FindElement(By.XPath("(//span[text()='Estimated Close Date']/following::div/span)[1]/slot/lightning-formatted-text")).Text;
                    if(abc != "")
                    {
                        estCloseDate[k] = DateTime.ParseExact(abc, "M/d/yyyy", null);
                    }

                    CloseTab(myArray[k]);
                    Thread.Sleep(2000);
                }

                //Get the latest 5 estimated close dates
                DateTime[] latestDates = estCloseDate.OrderByDescending(date => date).Take(5).ToArray();

                for(int m = 0; m < latestDates.Length; m++)
                {
                    for(int p = 0; p < totalActiveEngagements; p++)
                    {
                        if(m>0)
                        {
                            if(latestDates[m] == estCloseDate[m - 1])
                            {
                                for(int d = 0; d <= noOfEngagements; d++)
                                {
                                    if(myArray[m - 1] == engagementNames[d])
                                    {
                                        result = true;
                                        break;
                                    }
                                    else if(d == noOfEngagements)
                                    {
                                        result = false;
                                        break; ;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }
                            else if(latestDates[m] == estCloseDate[p])
                            {
                                for(int d = 0; d <= noOfEngagements; d++)
                                {
                                    if(myArray[p] == engagementNames[d])
                                    {
                                        result = true;
                                        break;
                                    }
                                    else if(d == noOfEngagements)
                                    {
                                        result = false;
                                        break; ;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                break;
                            }
                        }
                        else
                        {
                            if(latestDates[m] == estCloseDate[p])
                            {
                                for(int d = 0; d <= noOfEngagements; d++)
                                {
                                    if(myArray[p] == engagementNames[d])
                                    {
                                        result = true;
                                        break;
                                    }
                                    else if(d == noOfEngagements)
                                    {
                                        result = false;
                                        break; ;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                for(int x = 0; x <= totalActiveEngagements; x++)
                {
                    for(int y = 0; y <= noOfEngagements; y++)
                    {
                        if(myArray[x] == engagementNames[y])
                        {
                            result = true;
                            break;
                        }
                        else if(y == noOfEngagements)
                        {
                            result = false;
                            break; ;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            
            return result;
        }

        public bool VerifyIfThereAreNoActiveEngagementsThenLatest5ClosedEngagementsAreDisplayedUnderAssociatedEngagementsSection()
        {
            bool result = false;

            Thread.Sleep(8000);

            //Get the no. of Engagements under Associated Engagements section
            int noOfEngagements = driver.FindElements(By.XPath("//b[text()='Associated Engagements ']/../../div/dl")).Count;

            //Get the name of each Engagement and store in an array
            String[] engagementNames = new String[noOfEngagements];
            int j = 1;

            for(int i = 0; i <= noOfEngagements - 1; i++)
            {
                engagementNames[i] = driver.FindElement(By.XPath($"(//b[text()='Associated Engagements ']/../../div/dl)[{j}]/dd/p/button")).Text;
                j++;
            }

            //Navigate to Total Engagements page
            WebDriverWaits.WaitUntilEleVisible(driver, associatedEngagementsIcon, 120);
            driver.FindElement(associatedEngagementsIcon).Click();
            Thread.Sleep(3000);

            //Get total no of engagements
            int totalAvailableEngagements = driver.FindElements(By.XPath("//table[@aria-label='Engagements']/tbody/tr")).Count;

            //Get the name of each closed Engagement and store in an array
            ArrayList closeEngagementNames = new ArrayList();

            for(int rowNum = 1; rowNum <= totalAvailableEngagements; rowNum++)
            {
                if(driver.FindElement(By.XPath($"(//table[@aria-label='Engagements']/tbody/tr)[{rowNum}]/td[6]/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/lst-formatted-text")).Text == "Closed")
                {
                    closeEngagementNames.Add(driver.FindElement(By.XPath($"(//table[@aria-label='Engagements']/tbody/tr)[{rowNum}]/th/lightning-primitive-cell-factory/span/div/lightning-primitive-custom-cell/force-lookup/div/records-hoverable-link/div/a/slot/slot/span")).Text);
                }
            }

            //Get the total no of closed Engagements
            int totalClosedEngagements = closeEngagementNames.Count;
            string[] myArray = (string[]) closeEngagementNames.ToArray(typeof(string));

            DateTime[] closeDate = new DateTime[totalClosedEngagements];

            if(totalClosedEngagements > 5)
            {
                for(int k = 0; k < totalClosedEngagements; k++)
                {
                    //Open closed Engagements
                    driver.FindElement(By.XPath($"//span[text()='{myArray[k]}']")).Click();

                    //Navigate to Important Dates tab
                    WebDriverWaits.WaitUntilEleVisible(driver, linkImportantDates, 120);
                    driver.FindElement(linkImportantDates).Click();
                    Thread.Sleep(3000);

                    //Get the Close Date for each Closed Engagement
                    WebDriverWaits.WaitUntilEleVisible(driver, txtCloseDate, 120);
                    string closedDate = driver.FindElement(txtCloseDate).Text;
                    if(closedDate != "")
                    {
                        closeDate[k] = DateTime.ParseExact(closedDate, "M/d/yyyy", null);
                    }

                    CloseTab(myArray[k]);
                    Thread.Sleep(2000);
                }

                //Get the latest 5 close dates
                DateTime[] latestDates = closeDate.OrderByDescending(date => date).Take(5).ToArray();

                for(int m = 0; m < latestDates.Length; m++)
                {
                    for(int p = 0; p < totalClosedEngagements; p++)
                    {
                        if(m > 0)
                        {
                            if(latestDates[m] == closeDate[m - 1])
                            {
                                for(int d = 0; d <= noOfEngagements; d++)
                                {
                                    if(myArray[m - 1] == engagementNames[d])
                                    {
                                        result = true;
                                        break;
                                    }
                                    else if(d == noOfEngagements)
                                    {
                                        result = false;
                                        break; ;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }
                            else if(latestDates[m] == closeDate[p])
                            {
                                for(int d = 0; d <= noOfEngagements; d++)
                                {
                                    if(myArray[p] == engagementNames[d])
                                    {
                                        result = true;
                                        break;
                                    }
                                    else if(d == noOfEngagements)
                                    {
                                        result = false;
                                        break; ;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                break;
                            }
                        }
                        else
                        {
                            if(latestDates[m] == closeDate[p])
                            {
                                for(int d = 0; d <= noOfEngagements; d++)
                                {
                                    if(myArray[p] == engagementNames[d])
                                    {
                                        result = true;
                                        break;
                                    }
                                    else if(d == noOfEngagements)
                                    {
                                        result = false;
                                        break; ;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                for(int x = 0; x <= totalClosedEngagements; x++)
                {
                    for(int y = 0; y <= noOfEngagements; y++)
                    {
                        if(myArray[x] == engagementNames[y])
                        {
                            result = true;
                            break;
                        }
                        else if(y == noOfEngagements)
                        {
                            result = false;
                            break; ;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }

            return result;
        }

        public bool VerifyFieldsDisplayedUnderReferralsSection(string path)
        {
            bool result = false;

            int exlRowCount = ReadExcelData.GetRowCount(path, "Referrals");
            for(int i = 1; i <= exlRowCount; i++)
            {
                string exlFieldName = ReadExcelData.ReadDataMultipleRows(path, "Referrals", i, 1);
                string sfFieldName = driver.FindElement(By.XPath($"(//b[text()='Referrals ']/following::div/dl/dt/p)[{i}]")).Text;
                if(exlFieldName == sfFieldName)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            //Get total referrals count
            int totalReferrals = driver.FindElements(By.XPath("//b[text()='Referrals ']/following::div/dl/dt/p[text()='Status: ']")).Count;

            //Verify if status is Active then field displayed would be "Date Engaged" and If Status is Closed, then the field displayed would be "Closed Date"
            for(int j = 1; j <= totalReferrals; j++)
            {
                if(driver.FindElement(By.XPath($"(//b[text()='Referrals ']/following::div/dl/dt/p[text()='Status: '])[{j}]/following::dd/p")).Text != "Closed")
                {
                    string fieldName = driver.FindElement(By.XPath($"(//b[text()='Referrals ']/following::div/dl/dt/p)[{j * 5}]")).Text;
                    if(fieldName == "Date Engaged:")
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else
                {
                    string fieldName = driver.FindElement(By.XPath($"(//b[text()='Referrals ']/following::div/dl/dt/p)[{j * 5}]")).Text;
                    if(fieldName == "Closed Date:")
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        public bool VerifyErrorMessageDisplayedUponChangingCompanyNameForAContact(string newCompany)
        {
            bool result = false;

            //Cick on Edit button
            WebDriverWaits.WaitUntilClickable(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilClickable(driver, btnCancelOnEdit, 120);

            //Remove the already associated company
            driver.FindElement(By.XPath("//button[@title='Clear Selection']")).Click();

            //Enter new company
            WebDriverWaits.WaitUntilClickable(driver, inputCompanyName, 120);
            driver.FindElement(inputCompanyName).SendKeys(newCompany);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[contains(@title,'ActivityCompany')]/following::ul/li/lightning-base-combobox-item")).Click();
            Thread.Sleep(2000);

            //Click on Save button
            driver.FindElement(btnSaveOnEdit).Click();

            //Get the error message
            WebDriverWaits.WaitUntilClickable(driver, txtErrorPopup, 120);
            string errMsg = driver.FindElement(txtErrorPopup).Text;

            if(errMsg == "You do not have rights to move a Contact to another Company.")
            {
                result = true;
            }

            return result;
        }

    }
}