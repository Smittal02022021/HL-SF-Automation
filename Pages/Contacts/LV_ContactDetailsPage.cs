using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages.Contact
{
    class LV_ContactDetailsPage : BaseClass
    {
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
        By lblContactName = By.XPath("(//span[text()='Name'])[2]/../../div[2]/span/slot/lightning-formatted-name");

        public void CloseTab(string tabName)
        {
            driver.FindElement(By.XPath($"//button[@title='Close {tabName}']")).Click();
            Thread.Sleep(5000);
        }

        public bool VerifyButtonsDisplayedAtTheTopOfExternalContactDetailsPageForCFFinancialUser()
        {
            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver,btnEdit,120);
            if(driver.FindElement(btnEdit).Displayed && driver.FindElement(btnAddRelationshipL).Displayed && driver.FindElement(btnAddActivity).Displayed && driver.FindElement(btnPrintableView).Displayed)
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
            int tabCount = driver.FindElements(By.XPath("//flexipage-tab2[@id='tab-7']/slot/flexipage-component2")).Count;

            for (int i = 2; i <= excelCount; i++)
            {
                string excelSectionName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExternalContactSections", i, 1);
                for (int j = 1; j <= tabCount; j++)
                {
                    string sectionName = driver.FindElement(By.XPath($"//flexipage-tab2[@id='tab-7']/slot/flexipage-component2[{j}]/slot/flexipage-aura-wrapper/div/article/div/div/div/div[2]/h2/a")).Text;
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
    }
}