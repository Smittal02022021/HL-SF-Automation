using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections;
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
        By btnDeleteContact = By.XPath("//div[contains(text(),'Are you sure you want')]/following::button[@title='Delete']");

        //Contact Information Section Elements
        By btnEditName = By.XPath("//button[@title='Edit Name']");
        By txtLastName = By.XPath("(//label[text()='Last Name']/following::div/input)[1]");
        By lblContactName = By.XPath("(//span[text()='Name']/following::lightning-formatted-name)[2]");
        By lblCompanyName = By.XPath("((//span[text()='Company Name'])[3]/following::div//records-hoverable-link//a//slot)[2]/span");
        By lblEmail = By.XPath("((//span[text()='Email'])[2]/following::dd//span/slot//a)[1]");
        By lblPhoneNo = By.XPath("((//span[text()='Phone'])[2]/following::dd//span/slot//a)[1]");
        By associatedEngagementsIcon = By.XPath("(//lightning-icon[@icon-name='utility:new_window'])[1]");
        By txtCloseDate = By.XPath("((//span[text()='Close Date'])[2]/following::div/span)[1]/slot/lightning-formatted-text");
        By valContactMailingAddress = By.XPath("(//span[text()='Mailing Address']/following::dd//a)[1]");
        By valContactStatus = By.XPath("//lightning-formatted-text[text()='Active']");
        By valContactOffice = By.XPath("(//span[text()='Title']/following::dd//lightning-formatted-text)[2]");
        By valContactTitle = By.XPath("(//span[text()='Title']/following::dd//lightning-formatted-text)[4]");
        By valContactDepartment = By.XPath("(//span[text()='Title']/following::dd//lightning-formatted-text)[6]");

        //Assistant Information Section Elements
        By lblAssistant = By.XPath("(//span[text()='Assistant']/following::lightning-formatted-text)[1]");
        By lblAssistantPhone = By.XPath("(//span[text()='Assistant Phone']/following::a)[1]");
        By lblAssistantEmail = By.XPath("(//span[text()='Assistant Email']/following::a)[1]");

        //System Information Section Elements
        By btnEditContactCurrency = By.XPath("//button[@title='Edit Contact Currency']");
        By dropdownContactCurrency = By.XPath("//button[@aria-label='Contact Currency']");
        By contactRecordType = By.XPath("//div[@class='recordTypeName slds-grow slds-truncate']/span");

        //Buttons for CF Financial User
        By btnEdit = By.XPath("//button[@name='Edit']");
        By btnAddRelationshipL = By.XPath("//button[text()='Add Relationship L']");
        By btnAddActivity = By.XPath("//button[text()='Add Activity']");
        By btnPrintableView = By.XPath("//button[text()='Printable View']");
        By lblActivityDetails = By.XPath("//div[@class='pbSubheader brandTertiaryBgr first tertiaryPalette']/h3");

        //Buttons for System Admin User
        By btnAddRelationship = By.XPath("//button[text()='Add Relationship L']");
        By btnTearsheet = By.XPath("//button[text()='Tearsheet']");
        By btnContactReportsM = By.XPath("//button[text()='Contact Reports M']");
        By btnDelete = By.XPath("//button[text()='Delete']");
        By btnSubmitForApproval = By.XPath("//button[text()='Submit for Approval']");

        //Quick Links
        By quickLinksShowAll = By.XPath("(//a[contains(text(),'Show All')])[1]");
        By linkHLRelationships = By.XPath("((//*[text()='Related List Quick Links'])[2]/following::ul/li//a)[1]");
        By linkIndustryFocus = By.XPath("(//span[contains(text(),'Industry Focus')]/../..)[1]");
        By linkOpportunityContacts = By.XPath("(//span[contains(text(),'Opportunity Contacts')]/../..)[1]");
        By linkEngagementContacts = By.XPath("(//span[contains(text(),'Engagement Contacts')]/../..)[1]");
        By linkEngagementsShown = By.XPath("(//span[contains(text(),'Engagements Shown')]/../..)[1]");
        By linkAffiliatedCompanies = By.XPath("(//span[contains(text(),'Affiliated Companies')]/../..)[1]/../a");
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
        By dropdownStrengthRating = By.XPath("(//a[@class='select'])[1]");
        By dropdownType = By.XPath("(//a[@class='select'])[2]");
        By txtPersonalNote = By.XPath("(//span[text()='Personal Note']/following::textarea)[1]");
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
        By btnSave = By.XPath("(//span[contains(text(),'Save')])[3]/..");
        By btnSaveEdit = By.XPath("//button[@name='SaveEdit']");
        By btnCancel = By.XPath("(//span[contains(text(),'Cancel')])[3]/..");
        By btnCancelEdit = By.XPath("//button[@name='CancelEdit']");

        //Tabs for CF Financial/System Admin User
        By tabInfo = By.XPath("//a[@data-label='Info']");
        By tabPitchBook = By.XPath("//a[@data-label='PitchBook']");
        By tabRelationships = By.XPath("//a[@data-label='Relationships']");
        By tabCoverage = By.XPath("//a[@data-label='Coverage']");
        By tabActivity = By.XPath("//a[@data-label='Activity']");
        By tabCampaignHistory = By.XPath("//a[@data-label='Campaign History']");
        By tabHistory = By.XPath("//a[@data-label='History']");
        By tabDeals = By.XPath("//a[@data-label='Deals']");
        By tabSummary = By.XPath("//a[@data-label='Summary']");
        By tabMarketing = By.XPath("//a[@data-label='Marketing']");

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

        //HL Employee buttons
        By lobButton = By.XPath("(//label[text()='Line of Business'])[2]/../div//button");
        By lblIndustryGroup = By.XPath("//label[text()='Industry Group']");

        //Edit HL Contact elements
        By btnSaveOnEdit = By.XPath("(//button[contains(text(),'Save')])[2]");
        By btnCancelOnEdit = By.XPath("//button[@name='CancelEdit']");
        By inputCompanyName = By.XPath("//input[@placeholder='Search Companies...']");
        By txtErrorPopup = By.XPath("//div[@class='genericNotification']/../ul/li");
        By btnCloseErrorPopup = By.XPath("//button[@title='Close error dialog']");
        By inputAssistantName = By.XPath("//input[@name='AssistantName']");
        By inputAssistantPhone = By.XPath("//input[@name='AssistantPhone']");
        By inputAssistantEmail = By.XPath("//input[@name='Assistant_Email__c']");
        By dropdownDealAnnouncements = By.XPath("//button[@aria-label='Deal Announcements']");
        By dropdownEventConferences = By.XPath("//button[@aria-label='Events/Conferences']");
        By dropdownGeneralAnnouncements = By.XPath("//button[@aria-label='General Announcements']");
        By dropdownInsightsContent = By.XPath("//button[@aria-label='Insights/Content']");
        By inputMergeGroup = By.XPath("//input[@name='Merge_Group__c']");
        By checkboxCopyFromContactDetail = By.XPath("(//input[@name='Copy_From_Contact_Detail__c'])[2]");
        By inputBadgeFirstName = By.XPath("//input[@name='Badge_First_Name__c']");
        By inputBadgeLastName = By.XPath("//input[@name='Badge_Last_Name__c']");
        By inputBadgeCompanyName = By.XPath("//input[@name='Badge_Company__c']");
        By inputAccEngScore = By.XPath("//label[text()='Account Engagement Score']/following::div/input");

        //Marketing Tab Elements
        By lblDealAnnouncement = By.XPath("(//span[text()='Deal Announcements']/following::lightning-formatted-text)[1]");
        By lblEventsConferences = By.XPath("(//span[text()='Events/Conferences']/following::lightning-formatted-text)[1]");
        By lblGeneralAnnouncements = By.XPath("(//span[text()='General Announcements']/following::lightning-formatted-text)[1]");
        By lblInsightsContent = By.XPath("(//span[text()='Insights/Content']/following::lightning-formatted-text)[1]");
        By lblDealAnnouncementChangeDate = By.XPath("(//span[text()='Deal Announcements Change Date']/following::lightning-formatted-text)[1]");
        By lblEventsConferencesChangeDate = By.XPath("(//span[text()='Events/Conference Change Date']/following::lightning-formatted-text)[1]");
        By lblGeneralAnnouncementsChangeDate = By.XPath("(//span[text()='General Announcements Change Date']/following::lightning-formatted-text)[1]");
        By lblInsightsContentChangeDate = By.XPath("(//span[text()='Insights/Content Change Date']/following::lightning-formatted-text)[1]");
        By lblBadgeFirstName = By.XPath("(//span[text()='Badge First Name']/following::lightning-formatted-text)[1]");
        By lblBadgeLastName = By.XPath("(//span[text()='Badge Last Name']/following::lightning-formatted-text)[1]");
        By lblBadgeCompanyName = By.XPath("(//span[text()='Badge Company']/following::lightning-formatted-text)[1]");
        By lblBadgeFullName = By.XPath("(//span[text()='Badge Full Name']/following::lightning-formatted-text)[1]");

        By chkboxCopyFromContactDetail = By.XPath("(//input[@name='Copy_From_Contact_Detail__c'])[1]");
        By editCopyFromContactDetail = By.XPath("(//button[@title='Edit Copy From Contact Detail'])[1]");
        By chkboxPardotOptOut = By.XPath("(//input[@name='Pardot_Opt_Out__c'])[1]");
        By chkboxPardotDoNotEmail = By.XPath("(//input[@name='HasOptedOutOfEmail'])[1]");

        By txtDealAnnouncementChangeDate = By.XPath("(//input[@name='Deal_Announcements_Change_Date__c'])[1]");
        By txtEventsConferencesChangeDate = By.XPath("(//input[@name='Events_Conference_Change_Date__c'])[1]");
        By txtGeneralAnnouncementsChangeDate = By.XPath("(//input[@name='General_Announcements_Change_Date__c'])[1]");
        By txtInsightsContentChangeDate = By.XPath("(//input[@name='Insights_Content_Change_Date__c'])[1]");

        //Campaign History Tab Elements
        By btnAddToCampaign = By.XPath("(//a[@title='Add to Campaign'])[2]");
        By txtSearchCampaign = By.XPath("//input[@title='Search Campaigns']");
        By btnNext = By.XPath("//button[text()='Next']");

        public void ClickEditContactButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
        }

        public void DeleteContact()
        {
            //Scroll to the top of the page
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0, 0)");

            WebDriverWaits.WaitUntilClickable(driver, btnDelete, 120);

            driver.FindElement(btnDelete).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnDeleteContact).Click();
            Thread.Sleep(3000);
        }

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

            string msg = driver.FindElement(By.XPath("(//label[text()='Industry Group']/../div)[2]")).Text;
            if(msg.Contains("Industry Group must be selected when LOB is CF"))
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

        public string GetCompanyName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblCompanyName, 60);
            string companyName = driver.FindElement(lblCompanyName).Text;
            return companyName;
        }

        public string GetFirstAndLastName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblContactName, 60);
            string contactName = driver.FindElement(lblContactName).Text;
            return contactName;
        }

        public string GetContactCompleteAddress()
        {
            Thread.Sleep(2000);
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,700)");
            Thread.Sleep(5000);

            WebDriverWaits.WaitUntilEleVisible(driver, valContactMailingAddress, 60);
            string contactAddress = driver.FindElement(By.XPath("(//span[text()='Mailing Address']/following::dd//a/div)[1]")).Text + "\r\n"+ driver.FindElement(By.XPath("(//span[text()='Mailing Address']/following::dd//a/div)[2]")).Text;
            return contactAddress;
        }

        public string GetContactStatus()
        {
            Thread.Sleep(2000);
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,2000)");
            Thread.Sleep(5000);

            string contactStatus = driver.FindElement(valContactStatus).Text;
            return contactStatus;
        }

        public string GetContactOffice()
        {
            Thread.Sleep(2000);
            Actions action = new Actions(driver);
            var element = driver.FindElement(valContactOffice);
            action.MoveToElement(element);
            Thread.Sleep(2000);

            string contactOffice = driver.FindElement(valContactOffice).Text;
            return contactOffice;
        }

        public string GetContactTitle(string file, int row)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(3000);

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string contactTitle="";
            if(ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", row, 1).Contains("Houlihan Employee"))
            {
                contactTitle = driver.FindElement(By.XPath("(//span[text()='Title']/following::dd//lightning-formatted-text)[5]")).Text;
            }
            else
            {
                contactTitle = driver.FindElement(valContactTitle).Text;
            }
            return contactTitle;
        }

        public string GetContactDepartment(string file, int row)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(3000);

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string contactDept = "";
            if(ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", row, 1).Contains("Houlihan Employee"))
            {
                contactDept = driver.FindElement(By.XPath("(//span[text()='Title']/following::dd//lightning-formatted-text)[17]")).Text;
            }
            else
            {
                contactDept = driver.FindElement(valContactDepartment).Text;
            }
            return contactDept;
        }

        public string GetContactRecordTypeValue()
        {
            //Scroll to the bottom of the page
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0, 2500)");

            WebDriverWaits.WaitUntilEleVisible(driver, contactRecordType, 120);
            CustomFunctions.MoveToElement(driver, driver.FindElement(contactRecordType));

            string valContactRecord = driver.FindElement(contactRecordType).Text;
            return valContactRecord;
        }

        public bool VerifyTabsDisplayedOnExternalContactDetailPageForCFFinancialUser()
        {
            //Scroll to the Top of the page
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");

            bool result = false;
            if(driver.FindElement(tabInfo).Displayed && driver.FindElement(tabPitchBook).Displayed && driver.FindElement(tabRelationships).Displayed && driver.FindElement(tabCoverage).Displayed && driver.FindElement(tabActivity).Displayed && driver.FindElement(tabCampaignHistory).Displayed && driver.FindElement(tabHistory).Displayed && driver.FindElement(tabDeals).Displayed && driver.FindElement(tabSummary).Displayed)
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

            //Scroll to the bottom of the page
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0, 0)");
            Thread.Sleep(2000);

            //WebDriverWaits.WaitUntilEleVisible(driver, quickLinksShowAll, 120);
            //CustomFunctions.MoveToElement(driver, driver.FindElement(quickLinksShowAll));
            //driver.FindElement(quickLinksShowAll).Click();
            //Thread.Sleep(2000);

            //Get no of quick links
            int linkCount = driver.FindElements(By.XPath("(//*[text()='Related List Quick Links'])[1]/following::ul/li//slot")).Count;

            //Get the count from excel
            int excelLinkCount = ReadExcelData.GetRowCount(excelPath,"QuickLinks");

            for(int i = 2;i <= excelLinkCount;i++)
            {
                string excelLinkName = ReadExcelData.ReadDataMultipleRows(excelPath,"QuickLinks",i,1);
                for(int j=1;j<=linkCount;j++)
                {
                    string linkName = driver.FindElement(By.XPath($"((//*[text()='Related List Quick Links'])[1]/following::ul/li//slot)[{j}]")).Text;
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
            //Scroll to the bottom of the page
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            bool result = false;
            if(driver.FindElement(tabInfo).Displayed && driver.FindElement(tabPitchBook).Displayed && driver.FindElement(tabRelationships).Displayed && driver.FindElement(tabCoverage).Displayed && driver.FindElement(tabActivity).Displayed && driver.FindElement(tabCampaignHistory).Displayed && driver.FindElement(tabHistory).Displayed && driver.FindElement(tabDeals).Displayed && driver.FindElement(tabMarketing).Displayed && driver.FindElement(tabSummary).Displayed)
            {
                result = true;
            }
            return result;
        }

        public bool VerifyButtonsDisplayedAtTheTopOfExternalContactDetailsPageForSysAdminUser()
        {
            bool result = false;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            if(driver.FindElement(btnEdit).Displayed && driver.FindElement(btnAddRelationshipL).Displayed && driver.FindElement(btnPrintableView).Displayed && driver.FindElement(btnDelete).Displayed)
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
            Thread.Sleep(5000);
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

            if(driver.FindElement(By.XPath($"(//a[@title='{empName}'])[1]")).Displayed)
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

            Thread.Sleep(5000);
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

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
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//input[@placeholder='Search Companies...']/following::div[2]/ul/li/lightning-base-combobox-item")).Click();
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

            //Click on Cancel button
            driver.FindElement(btnCancelOnEdit).Click();
            Thread.Sleep(2000);

            return result;
        }

        public bool VerifyErrorMessageDisplayedWithNoLastName()
        {
            bool result = false;

            //Cick on Edit button
            WebDriverWaits.WaitUntilClickable(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilClickable(driver, btnCancelOnEdit, 120);

            //Remove the already associated last name
            driver.FindElement(By.XPath("//label[text()='Last Name']/following::div/input")).Clear();

            //Click on Save button
            driver.FindElement(btnSaveOnEdit).Click();

            //Get the error message
            WebDriverWaits.WaitUntilClickable(driver, btnCloseErrorPopup, 120);
            string errMsg = driver.FindElement(By.XPath("//label[text()='Last Name']/following::div[2]")).Text;

            if(errMsg.Contains("Complete this field."))
            {
                result = true;
            }

            //Click on Cancel button
            driver.FindElement(btnCancelOnEdit).Click();
            Thread.Sleep(2000);

            return result;
        }

        public bool VerifyErrorMessageDisplayedIfUserTriesToChangeContactCurrency()
        {
            bool result = false;

            // Scroll to the bottom of the page
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0, 3500)");

            WebDriverWaits.WaitUntilClickable(driver, btnEditContactCurrency, 120);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditContactCurrency));
            driver.FindElement(btnEditContactCurrency).Click();
            js.ExecuteScript("window.scrollTo(0, 3500)");

            WebDriverWaits.WaitUntilClickable(driver, btnSaveEdit, 120);
            driver.FindElement(dropdownContactCurrency).Click();
            Thread.Sleep(5000);

            driver.FindElement(dropdownContactCurrency).SendKeys(Keys.ArrowUp);
            driver.FindElement(dropdownContactCurrency).SendKeys(Keys.Enter);

            driver.FindElement(btnSaveEdit).Click();

            //Get the error message
            WebDriverWaits.WaitUntilClickable(driver, txtErrorPopup, 120);
            string errMsg = driver.FindElement(By.XPath("//button[@aria-label='Contact Currency']/following::div[3]")).Text;

            if(errMsg.Contains("Only system administrators can change employee currency"))
            {
                result = true;
            }

            //Click on Cancel button
            driver.FindElement(btnCancelEdit).Click();
            Thread.Sleep(2000);

            return result;
        }

        public bool VerifyErrorMessageDisplayedIfUserTriesToChangeLastNameOfHLContact()
        {
            bool result = false;

            // Scroll to the top of the page
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0, 0)");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilClickable(driver, btnEditName, 120);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditName));
            driver.FindElement(btnEditName).Click();
            Thread.Sleep(2000);

            driver.FindElement(txtLastName).Clear();
            driver.FindElement(txtLastName).SendKeys("testing");
            Thread.Sleep(2000);

            driver.FindElement(btnSaveEdit).Click();

            //Get the error message
            WebDriverWaits.WaitUntilClickable(driver, txtErrorPopup, 120);
            string errMsg = driver.FindElement(By.XPath("//label[text()='Last Name']/following::div[2]")).Text;

            if(errMsg.Contains("Only system administrators can change employee name and salutation"))
            {
                result = true;
            }

            //Click on Cancel button
            driver.FindElement(btnCancelEdit).Click();
            Thread.Sleep(2000);

            return result;
        }

        public bool VerifyUserCanEditAssistantNamePhoneAndEmail(string assName, string assPhn, string assEmail)
        {
            bool result = false;

            //Cick on Edit button
            WebDriverWaits.WaitUntilClickable(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilClickable(driver, btnCancelOnEdit, 120);

            //Enter Assistant Name, Phone and Email
            driver.FindElement(inputAssistantName).Clear();
            driver.FindElement(inputAssistantName).SendKeys(assName);
            Thread.Sleep(2000);

            driver.FindElement(inputAssistantPhone).Clear();
            driver.FindElement(inputAssistantPhone).SendKeys(assPhn);
            Thread.Sleep(2000);

            driver.FindElement(inputAssistantEmail).Clear();
            driver.FindElement(inputAssistantEmail).SendKeys(assEmail);
            Thread.Sleep(2000);

            //Click on Save button
            driver.FindElement(btnSaveOnEdit).Click();
            Thread.Sleep(3000);

            if(assName == driver.FindElement(lblAssistant).Text && assPhn == driver.FindElement(lblAssistantPhone).Text && assEmail == driver.FindElement(lblAssistantEmail).Text)
            {
                result = true;
            }

            return result;
        }

        public bool VerifyUserIsAbleToEditSubscriptionPreferenes(string dealAnn, string eventConference, string generalAnn, string insightsCon)
        {
            bool overallResult = false;
            bool result1 = false;
            bool result2 = false;

            //Click on Edit button
            WebDriverWaits.WaitUntilClickable(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(3000);

            //Select Deal Announcement
            CustomFunctions.MoveToElement(driver, driver.FindElement(dropdownInsightsContent));
            driver.FindElement(dropdownDealAnnouncements).Click();
            Thread.Sleep(2000);

            if(dealAnn == "Opt In")
            {
                driver.FindElement(By.XPath("//button[@aria-label='Deal Announcements']/following::div[2]/lightning-base-combobox-item[2]")).Click();
                Thread.Sleep(2000);
            }
            else if (dealAnn == "Opt Out")
            {
                driver.FindElement(By.XPath("//button[@aria-label='Deal Announcements']/following::div[2]/lightning-base-combobox-item[3]")).Click();
                Thread.Sleep(2000);
            }

            //Select Event Conference
            CustomFunctions.MoveToElement(driver, driver.FindElement(dropdownInsightsContent));
            driver.FindElement(dropdownEventConferences).Click();
            Thread.Sleep(2000);

            if(eventConference == "Opt In")
            {
                driver.FindElement(By.XPath("//button[@aria-label='Events/Conferences']/following::div[2]/lightning-base-combobox-item[2]")).Click();
                Thread.Sleep(2000);
            }
            else if(eventConference == "Opt Out")
            {
                driver.FindElement(By.XPath("//button[@aria-label='Events/Conferences']/following::div[2]/lightning-base-combobox-item[3]")).Click();
                Thread.Sleep(2000);
            }

            //Select General Announcement
            try
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(inputAccEngScore));
                driver.FindElement(dropdownGeneralAnnouncements).Click();
                Thread.Sleep(2000);
            }
            catch(Exception)
            {

            }

            if(generalAnn == "Opt In")
            {
                driver.FindElement(By.XPath("//button[@aria-label='General Announcements']/following::div[2]/lightning-base-combobox-item[2]")).Click();
                Thread.Sleep(2000);
            }
            else if(generalAnn == "Opt Out")
            {
                driver.FindElement(By.XPath("//button[@aria-label='General Announcements']/following::div[2]/lightning-base-combobox-item[3]")).Click();
                Thread.Sleep(2000);
            }

            //Select Insights Content
            driver.FindElement(dropdownInsightsContent).Click();
            Thread.Sleep(2000);

            if(insightsCon == "Opt In")
            {
                driver.FindElement(By.XPath("//button[@aria-label='Insights/Content']/following::div[2]/lightning-base-combobox-item[2]")).Click();
                Thread.Sleep(2000);
            }
            else if(insightsCon == "Opt Out")
            {
                driver.FindElement(By.XPath("//button[@aria-label='Insights/Content']/following::div[2]/lightning-base-combobox-item[3]")).Click();
                Thread.Sleep(2000);
            }

            //Click on Save button
            driver.FindElement(btnSaveOnEdit).Click();
            Thread.Sleep(5000);

            //Navigate to Marketing Tab
            WebDriverWaits.WaitUntilEleVisible(driver, tabMarketing, 120);
            driver.FindElement(tabMarketing).Click();
            Thread.Sleep(5000);

            if(dealAnn == driver.FindElement(lblDealAnnouncement).Text && eventConference == driver.FindElement(lblEventsConferences).Text && generalAnn == driver.FindElement(lblGeneralAnnouncements).Text && insightsCon == driver.FindElement(lblInsightsContent).Text)
            {
                result1 = true;
                string updatedDate = DateTime.Today.ToString("M/dd/yyyy").Replace('-', '/');
                if(driver.FindElement(lblDealAnnouncementChangeDate).Text == updatedDate && driver.FindElement(lblEventsConferencesChangeDate).Text == updatedDate && driver.FindElement(lblGeneralAnnouncementsChangeDate).Text == updatedDate && driver.FindElement(lblInsightsContentChangeDate).Text == updatedDate)
                {
                    result2 = true;
                }
            }

            if(result1 && result2 == true) 
            {
                overallResult = true;
            }

            return overallResult;
        }

        public bool VerifyUserIsAbleToEditEventBadges(string file)
        {
            Thread.Sleep(3000);
            bool overallResult = false;
            bool result1 = false;
            bool result2 = false;
            bool result3 = false;

            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            string companyName = ReadExcelData.ReadData(excelPath, "Contact", 1);
            string firstName = ReadExcelData.ReadData(excelPath, "Contact", 2);
            string lastName = ReadExcelData.ReadData(excelPath, "Contact", 3);
            string fullName = ReadExcelData.ReadData(excelPath, "Contact", 6);

            string badgeFirst = ReadExcelData.ReadData(excelPath, "EventBadges", 1);
            string badgeLast = ReadExcelData.ReadData(excelPath, "EventBadges", 2);
            string badgeCompany = ReadExcelData.ReadData(excelPath, "EventBadges", 3);
            string badgeFullName = badgeFirst + " " + badgeLast;

            //Cick on Edit button
            WebDriverWaits.WaitUntilClickable(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilClickable(driver, btnCancelOnEdit, 120);

            //Select Copy from Contact Detail checkbox
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownDealAnnouncements, 120);
            CustomFunctions.MoveToElement(driver, driver.FindElement(dropdownDealAnnouncements));
            driver.FindElement(checkboxCopyFromContactDetail).Click();
            Thread.Sleep(2000);

            //Click on Save button
            driver.FindElement(btnSaveOnEdit).Click();
            Thread.Sleep(5000);

            //Navigate to Marketing Tab
            WebDriverWaits.WaitUntilEleVisible(driver, tabMarketing, 120);
            driver.FindElement(tabMarketing).Click();
            Thread.Sleep(5000);

            if(companyName == driver.FindElement(lblBadgeCompanyName).Text && firstName == driver.FindElement(lblBadgeFirstName).Text && lastName == driver.FindElement(lblBadgeLastName).Text && fullName == driver.FindElement(lblBadgeFullName).Text)
            {
                result1 = true;
            }
            Thread.Sleep(5000);

            //Cick on Edit button
            WebDriverWaits.WaitUntilClickable(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(3000);

            //Uncheck Copy from Contact Detail checkbox
            WebDriverWaits.WaitUntilEleVisible(driver, dropdownDealAnnouncements, 120);
            CustomFunctions.MoveToElement(driver, driver.FindElement(dropdownDealAnnouncements));
            driver.FindElement(checkboxCopyFromContactDetail).Click();
            Thread.Sleep(2000);

            //Click on Save button
            driver.FindElement(btnSaveOnEdit).Click();
            Thread.Sleep(3000);

            if(driver.FindElement(lblBadgeCompanyName).Text == "" && driver.FindElement(lblBadgeFirstName).Text == "" && driver.FindElement(lblBadgeLastName).Text == "" && driver.FindElement(lblBadgeFullName).Text == "")
            {
                result2 = true;
            }
            Thread.Sleep(5000);

            //Cick on Edit button
            WebDriverWaits.WaitUntilClickable(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(3000);

            WebDriverWaits.WaitUntilEleVisible(driver, dropdownDealAnnouncements, 120);
            CustomFunctions.MoveToElement(driver, driver.FindElement(dropdownDealAnnouncements));
            Thread.Sleep(2000);

            driver.FindElement(inputBadgeFirstName).Clear();
            driver.FindElement(inputBadgeFirstName).Click();
            Thread.Sleep(2000);

            driver.FindElement(inputBadgeLastName).Clear();
            driver.FindElement(inputBadgeLastName).Click();
            Thread.Sleep(2000);

            driver.FindElement(inputBadgeCompanyName).Clear();
            driver.FindElement(inputBadgeCompanyName).Click();
            Thread.Sleep(2000);

            //Click on Save button
            driver.FindElement(btnSaveOnEdit).Click();
            Thread.Sleep(3000);

            if(badgeCompany == driver.FindElement(lblBadgeCompanyName).Text && badgeFirst == driver.FindElement(lblBadgeFirstName).Text && badgeLast == driver.FindElement(lblBadgeLastName).Text && badgeFullName == driver.FindElement(lblBadgeFullName).Text)
            {
                result3 = true;
            }

            if(result1 && result2 && result3 == true)
            {
                overallResult = true;
            }

            return overallResult;
        }

        public string GetContactDetailsHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactName, 60);
            string headingcontactDetail = driver.FindElement(txtContactName).Text;
            return headingcontactDetail;
        }

        public void CreateRelationship(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            WebDriverWaits.WaitUntilEleVisible(driver, btnAddRelationshipL);
            driver.FindElement(btnAddRelationshipL).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, txtHLContact);
            driver.FindElement(txtHLContact).SendKeys(ReadExcelData.ReadData(excelPath, "Relationship", 1));
            Thread.Sleep(2000);

            driver.FindElement(By.XPath("(//mark[text()='Houlihan'])[1]/../../../..")).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(5000);
        }

        public void NavigateToRelationshipTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabRelationships);
            driver.FindElement(tabRelationships).Click();
            Thread.Sleep(5000);
        }

        public void NavigateToDealsTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabDeals);
            driver.FindElement(tabDeals).Click();
            Thread.Sleep(5000);
        }

        public void NavigateToMarketingTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabMarketing);
            driver.FindElement(tabMarketing).Click();
            Thread.Sleep(5000);
        }

        public void NavigateToCampaignHistoryTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabCampaignHistory);
            driver.FindElement(tabCampaignHistory).Click();
            Thread.Sleep(3000);
        }

        public void UpdateSubscriptionPreferences()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, editCopyFromContactDetail, 120);
            driver.FindElement(editCopyFromContactDetail).Click();
            Thread.Sleep(2000);

            try
            {
                driver.FindElement(chkboxPardotOptOut).Click();
                Thread.Sleep(2000);

                driver.FindElement(chkboxPardotDoNotEmail).Click();
                Thread.Sleep(2000);

                string getDate = DateTime.Today.AddDays(0).ToString("MM/dd/yyyy").Replace('-', '/');
                driver.FindElement(txtDealAnnouncementChangeDate).SendKeys(getDate);
                Thread.Sleep(2000);

                driver.FindElement(txtEventsConferencesChangeDate).SendKeys(getDate);
                Thread.Sleep(2000);

                driver.FindElement(txtGeneralAnnouncementsChangeDate).SendKeys(getDate);
                Thread.Sleep(2000);

                driver.FindElement(txtInsightsContentChangeDate).SendKeys(getDate);
                Thread.Sleep(2000);
            }
            catch(Exception ex)
            {

            }

            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveEdit, 120);
            driver.FindElement(btnSaveEdit).Click();

            Thread.Sleep(4000);
        }

        public bool VerifySubscriptionPreferencesAreUpdated()
        {
            bool result = false;
            
            if(driver.FindElement(lblDealAnnouncementChangeDate).Text!="" && driver.FindElement(lblEventsConferencesChangeDate).Text != "" && driver.FindElement(lblGeneralAnnouncementsChangeDate).Text != "" && driver.FindElement(lblInsightsContentChangeDate).Text != "")
            {
                result = true;
            }
            return result;
        }

        public void ClickAddToCampaignButton()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnAddToCampaign);
                driver.FindElement(btnAddToCampaign).Click();
                Thread.Sleep(5000);
            }
            catch (Exception ex) { }
        }

        public void SearchAndSelectCampaignName(string name)
        {
            try
            {
                driver.FindElement(txtSearchCampaign).SendKeys(name);
                Thread.Sleep(5000);

                driver.FindElement(By.XPath($"//div[@title='{name}']")).Click();
                Thread.Sleep(2000);

                driver.FindElement(btnNext).Click();
                Thread.Sleep(2000);
            }
            catch(Exception ex) { }
        }

        public void NavigateToNewAffiliatedCompaniesPage()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkAffiliatedCompanies);

            CustomFunctions.MoveToElement(driver, driver.FindElement(linkAffiliatedCompanies));
            driver.FindElement(linkAffiliatedCompanies).Click();

            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//button[@name='New']")).Click();
            Thread.Sleep(5000);
        }

        public bool ValidateOffileFieldEditableForHCMUser()
        {
            bool result = false;
            int size = driver.FindElements(By.XPath("//span[text()='Office']/../../..//button")).Count();

            //string val = driver.FindElement(txtOffice).Text;
            if(size == 0)
            {
                result = true;
            }
            return result;
        }
    }
}