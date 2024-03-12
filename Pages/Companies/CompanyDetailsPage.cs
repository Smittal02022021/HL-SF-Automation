﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SF_Automation.Pages.Company;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SF_Automation.Pages.Companies
{
    class CompanyDetailsPage : BaseClass
    {
        By linkCoverageTeam = By.CssSelector("a[id*='bV0_link']>span");
        By linkOfficerName = By.CssSelector("div[id*='V0_body'] > table > tbody > tr:nth-child(2) > th > a");
        By titleCoverageTeam = By.XPath("//div[@id='ep']//h2[@class='mainTitle']");
        By titlePage = By.CssSelector("h1[class='pageType']");
        By linkCompany = By.CssSelector("a[id*='D7bV0']");
        By linkCompanyList = By.CssSelector("a[id*='13ee_link']>span");
        By linkCompanyMember = By.CssSelector("div[id*='13ee_body'] > table > tbody > tr.dataRow.even.last.first > th > a");
        By linkCampaign = By.CssSelector("a[title='Campaigns Tab']");
        By btnGo = By.XPath("//input[@value=' Go! ']");
        By linkCampaignName = By.CssSelector("div[id*='nYp_CAMPAIGN_NAME']");
        By comboFlagReason = By.XPath("//label[text()='Flag Reason']/following::td/span/select");
        By txtFlagReasonComment = By.XPath("(//label[text()='Flag Reason Comment']/following::td/textarea)[1]");


        CompanyHomePage companyHome = new CompanyHomePage();
        CompanySelectRecordPage conSelectRecord = new CompanySelectRecordPage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        By companyDetailHeading = By.XPath("//h2[contains(text(),'Company Detail')]");
        By btnNewContact = By.CssSelector("td[class='pbButton'] > input[value='New Contact']");
        By btnAddCFOpportunity = By.XPath("//a[contains(text(),'Add CF Opportunity')]");
        By btnNewCFOpportunity = By.CssSelector("input[value='New CF Opportunity']");
        By btnAddFASOpportunity = By.XPath("//a[contains(text(),'Add FAS Opportunity')]");
        By btnNewFASOpportunity = By.CssSelector("input[value='New FAS Opportunity']");
        By btnNewCoverageTeam = By.CssSelector("input[value='New Coverage Team']");
        By coverageTeamEditHeading = By.CssSelector("h2[class='mainTitle']");
        By txtCompanyName = By.CssSelector("input[id='CF00Ni000000D7bV0']");
        By btnSaveCoverageTeam = By.CssSelector("td[id='topButtonRow'] > input[value=' Save ']");
        By toolTipPriorityHelp = By.CssSelector("span[id*='00N3100000GuI2p-_help'] > img");
        By toolTipPriorityText = By.CssSelector("span[id*='00N3100000GuI2p-_help'] > script");
        By btnDeleteCompany = By.CssSelector("td[id*='topButtonRow'] > input[value='Delete']");
        By valCompanyName = By.CssSelector("div[id*='acc2j_id0']");
        By valState = By.CssSelector("div[id*='00Ni000000DvFsEj']");
        By valCompanyType = By.CssSelector("div[id*='RecordTypej']");
        By valAddress = By.CssSelector("div[id*='acc17j']");
        By btnEdit = By.CssSelector("td[id*='topButtonRow'] >input[value*='Edit']");
        By valCompanySubType = By.XPath("//*[text()='Company Information']/.. //following-sibling::div//*[text()='Company Sub Type']/..//div[@id='acc6j_id0_j_id1_ileinner']");
        By valOwnership = By.XPath("//*[text()='Company Information']/.. //following-sibling::div//*[text()='Ownership']/..//div[@id='acc14j_id0_j_id1_ileinner']");
        By valParentCompany = By.XPath("//*[text()='Company Information']/..//following-sibling::div//*[text()='Parent Company']/../..//div[@id='acc3j_id0_j_id1_ileinner']");
        By valIndustryFocus = By.XPath("//*[text()='Investment Preferences']/..//following-sibling::div//*[text()='Industry Focus']/..//div[@id='00Ni000000D9WGgj_id0_j_id1_ileinner']");
        By valGeographicalFocus = By.XPath("//*[text()='Investment Preferences']/..//following-sibling::div//*[text()='Geographical Focus']/..//div[@id='00Ni000000D9WG2j_id0_j_id1_ileinner']");
        By valDealPreference = By.XPath("//*[text()='Investment Preferences']/..//following-sibling::div//*[text()='Deal Preferences']/..//div[@id='00Ni000000DvG7nj_id0_j_id1_ileinner']");
        By valDescription = By.XPath("//*[text()='Description Information']/..//following-sibling::div//*[text()='Description']/..//div[@id='acc20j_id0_j_id1_ileinner']");
        By valCapIQCompany = By.XPath("//*[text()='CapIQ Information']/..//following-sibling::div//*[text()='CapIQ Company']/../..//div[@id='CF00Ni000000DvFoMj_id0_j_id1_ileinner']");
        By errPage = By.CssSelector("span[id='theErrorPage:theError']");
        By valcompanyLocation = By.CssSelector("div[id*='00Ni000000DvFsEj']");
        By btnEditCompanyDetail = By.CssSelector("td[id='topButtonRowj_id0_j_id1'] > input[name='edit']");
        By valcompanyPhoneNumber = By.CssSelector("div[id*='acc10j_id0']");
        By valCompanyDescription = By.CssSelector("div[id*='acc20j_id0']");
        By valCompanyAddress = By.CssSelector("div[id*='acc17j_id0']");
        By chkBoxHQ = By.CssSelector("img[id*= '00N3100000GyJM5']");
        By linkChangeCompanyType = By.CssSelector("div[id*='RecordType'] > a");
        By btnSaveCompanyEdit = By.CssSelector("td[id='bottomButtonRow'] > input[value=' Save ']");
        By valCreatedBy = By.CssSelector("div[id*='CreatedBy'] > a");
        By valLabelGroupByDupRecord = By.CssSelector("tr[class='breakRowClass0 breakRowClass0Top'] > td:nth-child(2) > strong:nth-child(1)");
        By btnNewCompanyFinancial = By.CssSelector("input[value='New Company Financial']");
        By valCompanyFinancialYear = By.CssSelector("div[id*='ke_body'] > table > tbody > tr:nth-child(2) > th");
        By valAsOfDateInCompanyFinancial = By.CssSelector("div[id*='ke_body'] > table > tbody > tr:nth-child(2) > td:nth-child(3)");
        By valFinancialsYearAnnualFinancial = By.XPath("//*[text()='Annual Financials']/..//following-sibling::div//*[text()='Financials Year']/../td[2]");
        By valFinancialsDateAnnualFinancial = By.XPath("//*[text()='Annual Financials']/..//following-sibling::div//*[text()='Financials As of Date']/../td[2]");
        By linkDelCompanyFinancial = By.XPath("//*[text()='Company Financials']/../../../../../following-sibling::div/table/tbody/tr[2]/td/a[2]");
        By linkDelContact = By.XPath("//*[text()='Contacts']/../../../../../following-sibling::div/table/tbody/tr[2]/td/a[2]");
        By valNoRecordsCompanyFinancial = By.XPath("//*[text()='Company Financials']/../../../../../following-sibling::div/table/tbody/tr/th");
        By linkDelCoverageTeam = By.XPath("//*[text()='Coverage Team']/../../../../../../div[@class='pbBody']/table/tbody/tr/td[1]/a[2]");
        By linkCoverageTeamOfficer = By.XPath("//*[text()='Coverage Team']/../../../../../../div[@class='pbBody']/table/tbody/tr/th/a");
        By valCoverageLevel = By.XPath("//*[text()='Coverage Team']/../../../../../../div[@class='pbBody']/table/tbody/tr[2]/td[4]");
        By valCoverageType = By.XPath("//*[text()='Coverage Team']/../../../../../../div[@class='pbBody']/table/tbody/tr[2]/td[6]");
        By btnMergeContacts = By.CssSelector("input[name='merge']");
        By valContactName = By.XPath("//*[text()='Contacts']/../../../../../../div[2]/table/tbody/tr[2]/th/a");
        By valSecondContactName = By.XPath("//*[text()='Contacts']/../../../../../../div[2]/table/tbody/tr[3]/th/a");
        By valThirdContactName = By.XPath("//*[text()='Contacts']/../../../../../../div[2]/table/tbody/tr[4]/th/a");

        By valContactEmail = By.XPath("//*[text()='Contacts']/../../../../../../div[2]/table/tbody/tr[2]/td[3]/a");
        By valContactPhone = By.XPath("//*[text()='Contacts']/../../../../../../div[2]/table/tbody/tr[2]/td[4]");
        By linkCoverageOfficer = By.XPath("//*[text()='Officer Name']/../../tr/th/a[text()='Jacklyn Robinson']");
        
       
        // By valERPSubmittedToSync = By.CssSelector("div[id*='eayj']");
        By valERPSubmittedToSync = By.XPath("//td[@id='00N5A00000M0eayj_id0_j_id1_ilecell']/div");

        By valERPLastIntegrationResponseDate = By.XPath("//*[@id='00N5A00000M0eapj_id0_j_id1_ilecell']/div");
        // By valERPLastIntegrationResponseDate = By.CssSelector("div[id*='M0eapj']");

        By valERPLastIntegrationStatus = By.XPath("//*[@id='00N5A00000M0eaqj_id0_j_id1_ilecell']/div");
        //By valERPLastIntegrationStatus = By.CssSelector("div[id*='M0eaqj']");

        By valERPLastIntegrationErrorCode = By.XPath("//*[@id='00N5A00000M0eanj_id0_j_id1_ilecell']/div");
        //By valERPLastIntegrationErrorCode = By.CssSelector("div[id*='M0eanj']");
        By valERPLastIntegrationErrorDescription = By.XPath("//*[@id='00N5A00000M0eaoj_id0_j_id1_ilecell']/div");
        //By valERPLastIntegrationErrorDescription = By.CssSelector("div[id*='M0eaoj']");
        By valClientNumber = By.CssSelector("div[id*='DxJbdj']");
        By valPrimaryContactAvailable = By.CssSelector("div[id*='RelatedContactList_body'] > table > tbody > tr:nth-child(1) > th");
        By valERPAccountID = By.CssSelector("div[id*='M0eaUj']");
        By valERPOrgPartyId = By.CssSelector("div[id*='M0earj']");
        By valERPBillToAddressId = By.CssSelector("div[id*='M0eaWj']");
        By valERPBillToLocationId = By.CssSelector("div[id*='M0eaXj']");
        By valERPBillToLocation = By.CssSelector("div[id*='M5Dqsj']");
        By valERPBillToSiteId = By.CssSelector("div[id*='M0eaYj']");
        By valERPShipToAddressId = By.CssSelector("div[id*='M0eavj']");
        By valERPShipToLocationId = By.CssSelector("div[id*='M0eawj']");
        By valERPShipToSiteId = By.CssSelector("div[id*='M0eaxj']");
        By valERPBillToAddressFlag = By.CssSelector("div[id*='M0eaVj']");
        By valERPShipToAddressFlag = By.CssSelector("div[id*='M0eauj']");
        By valERPContactFirstName = By.CssSelector("div[id*='M0eaaj']");
        By valERPContactLastName = By.CssSelector("div[id*='M0eadj']");
        By valERPContactEmail = By.CssSelector("div[id*='M0eaZj']");
        By valERPContactPhone = By.CssSelector("div[id*='M0eaej']");
        By valERPContactId = By.CssSelector("div[id*='M0eacj']");
        By valERPPersonPartyId = By.CssSelector("div[id*='M0eatj']");
        By valERPContactPointEmailId = By.CssSelector("div[id*='M0eagj']");
        By valERPContactPointPhoneId = By.CssSelector("div[id*='M0eaij']");
        By valERPContactPointRelationshipId = By.CssSelector("div[id*='M0eajj']");
        By valERPAccountDescription = By.CssSelector("div[id*='M0eaTj']");
        By valERPCustomerType = By.CssSelector("div[id*='M0ealj']");
        By valERPCustomerTypeDesc = By.CssSelector("div[id*='M0eakj']");
        By linkContractName = By.CssSelector("th[class*='dataCell'] a");
        By comboStateProvince = By.CssSelector("select[id='acc17state']");
        By txtShippingProvince = By.CssSelector("input[id*='M0eb2']");
        By linkContactEdit = By.CssSelector("div[id*='RelatedContactList_body'] > table > tbody >tr:nth-child(2) > td:nth-child(1) > a");
        By txtContactEmail = By.CssSelector("input[id='con15']");
        By txtContactPhone = By.CssSelector("input[id='con10']");
        By btnContactSave = By.CssSelector("td[id='topButtonRow'] input[value*=' Save ']");
        By valERPContactFlag = By.CssSelector("td[id*='M0eabj']");
        By valERPContactPointEmailFlag = By.CssSelector("td[id*='M0eafj']");
        By valERPContactPointPhoneFlag = By.CssSelector("td[id*='M0eahj']");
        By linkPrimaryBillingContact = By.CssSelector("div[id*='M0eazj'] > a");
        By linkCoverageSector = By.CssSelector("table[class='list'] > tbody > tr:nth-child(2) > th > a");
        By btnDeleteCoverageSector = By.CssSelector("input[value='Delete']");
        By changelinkCompanyRecordType = By.CssSelector("div[id*='RecordTypej']>a");

        By txtCurrentRecordType = By.CssSelector("table[class='detailList']>tbody>tr:nth-of-type(2)>td:nth-of-type(2)");
        By drpdwnCompanyRecordType = By.CssSelector("select[id='p3']");

        By btnContinue = By.CssSelector("input[title='Continue']");
        By drpdownOfficeCode = By.CssSelector("select[id*='00Fjq9q']");

        By btnNewCompanySector = By.XPath("//input[@value='New Company Sector & Investment Preference']");
        By shwAllTab = By.CssSelector("li[id='AllTab_Tab'] > a > img");
        By imgCoverageSectorDependencies = By.CssSelector("img[alt = 'Coverage Sector Dependencies']");
        By imgCompanySectorDependencyLookUp = By.XPath("//img[@alt='Sector Categorization Lookup (New Window)']");
        By txtSearchBox = By.XPath("//input[@title='Go!']/preceding::input[1]");
        By linkCoverageSectorDependencyName = By.XPath("//a[@href='#']");
        By btnSaveCompanySector = By.XPath("(//input[@title='Save'])[1]");
        By btnDeleteCompanySector = By.XPath("(//input[@title='Delete'])[1]");
        By valCompanySectorName = By.XPath("//td[contains(text(),'Company Sector')]/following::div[1]");
        By linkCompanyName = By.XPath("(//td[contains(text(),'Company')])[2]/../td[2]/div/a");
        By linkSectorName = By.XPath("//table/tbody/tr[2]/th/a");
        By linkCompanySector = By.XPath("//span[@class='count'][contains(text(),'0')]/preceding::span[contains(text(),'Company Sectors')]");
        By linkShowFilters = By.XPath("//a[contains(text(),'Show Filters')]");
        By inputCoverageType = By.XPath("//input[@id='00N6e00000MRMtkEAHCoverage_Sector_Dependency__c']");
        By inputPrimarySector = By.XPath("//input[@id='00N6e00000MRMtlEAHCoverage_Sector_Dependency__c']");
        By inputSecondarySector = By.XPath("//input[@id='00N6e00000MRMtmEAHCoverage_Sector_Dependency__c']");
        By inputTertiarySector = By.XPath("//input[@id='00N6e00000MRMtnEAHCoverage_Sector_Dependency__c']");
        By btnApplyFilters = By.XPath("//input[@title='Apply Filters']");
        By btnEditCompCoverageSector = By.XPath("//input[@title='Edit']");
        By txtCompanyType = By.XPath("//p[@title='Company Type']//following::p//span");

        By drpdownInvestmentPref = By.XPath("//label[text()='Investment Preference / Operating Sector']/../../td[2]/div/span/select");

        By searchOpportunitiesL = By.XPath("//flexipage-component2[@data-component-id='c_sL_HierarchyViewer']//input[contains(@placeholder,'Search')]");
        By searchEngagementsL = By.XPath("//flexipage-component2[@data-component-id='c_sL_HierarchyViewer2']//input[contains(@placeholder,'Search')]");

        By linkViewAllOppL = By.XPath("//flexipage-component2[@data-component-id='c_sL_HierarchyViewer']//div[contains(@class,'card__footer')]//slot[@name='footer']//a[text()='View All']");
        By btnCloseViewAllPopup = By.XPath("//div[@class='slds-modal__container']//button[contains(@class,'slds-modal__close')]");
        By linkViewAllEngL = By.XPath("//flexipage-component2[@data-component-id='c_sL_HierarchyViewer2']//div[contains(@class,'card__footer')]//slot[@name='footer']//a[text()='View All']");
        By searchViewAllOppEngL = By.XPath("//div[@class='slds-modal__container']//input[contains(@placeholder,'Search')]");

        By headerNestedListL = By.XPath("//article[contains(@class,'NestedTables_CardChild')]//h2[contains(@class,'container')]//span");
        By txtCompanyHLRelationshipContactL = By.XPath("//article//div[contains(@class,'otherBrowser')]//table//tr[1]//td[@data-label='HL Contact']//a");
        By txtCompanyHLRelationshipCoverageOfficerL = By.XPath("//flexipage-component2[contains(@data-component-id,'NestedTables2')]//table//tr[1]//td[@data-label='Officer Name']//a");//div[contains(@class,'NestedTables')]//table//tr[1]//td[@data-label='Officer Name']//a
        By txtCoverageTeamCompanyNameL = By.XPath("//div[contains(@class,'page-header')]//h1//slot[@name='primaryField']//a//span");
        By txtCoverageTeamOfficerNameL = By.XPath("//p[@title='Officer Name']//following::p//span//a");
        By txtCompanyDetailCoverageTypeL = By.XPath("//article//div[contains(@class,'otherBrowser')]//table//tbody/tr[1]//td[1]//span");
        By panelCoverageTypeL = By.XPath("//dt[text()='Coverage Type:']//following-sibling::dd[1]//span");
        By panelCoverageSector = By.XPath("//ul[@role='tablist']//li[contains(@title,'Coverage Sectors')]//a");
        By buttonCloseCoverageTab = By.XPath("//button[contains(@title,'Close C-')]");
        By alertDuplicate = By.XPath("//div[@role='alertdialog']//button[@title='Close']");

        By comboIndustryType = By.CssSelector("select[id*='FD7Vf']");
        By comboIndustryTypeOptions = By.CssSelector("select[id*='FD7Vf'] option");
        By btnCancel = By.CssSelector("input[name='cancel']");
        By comboType = By.CssSelector("select[id*='FjXsE']");
        By comboTypeOption = By.CssSelector("select[id*='FjXsE'] option");

        //By txtSearchBox = By.XPath("//div[contains(@class,'forceSearchAssistant')]//button[contains(@aria-label,'Search')]");
        
        private By _DetailPageQuickLink(string name)
        {
            return By.XPath($"//div[@class='listHoverLinks']//a//span[text()='{name}']");
        }

        private By _homePageTab(string name)
        {
            return By.XPath($"//lightning-tabset[@class='flexipage-tabset']//a[contains(@data-label,'{name}')]");
        }
        private By _tabHeader(string name)
        {
            return By.XPath($"//div[@class='slds-card__header slds-grid']//h2//span[contains(text(),'{name}')]");
        }
        public bool VerifyIfCompanySectorQuickLinkIsDisplayed()
        {
            bool result = false;
            if (driver.FindElement(linkCompanySector).Displayed)
            {
                result = true;
            }
            return result;
        }

        public void ClickNewCompanySectorButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCompanySector, 120);
            driver.FindElement(btnNewCompanySector).Click();
            Thread.Sleep(2000);
        }

        public void SelectCoverageSectorDependency(string covSectorDependencyName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver,drpdownInvestmentPref,120);
            driver.FindElement(drpdownInvestmentPref).SendKeys("Operating Sector");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, imgCompanySectorDependencyLookUp, 120);
            driver.FindElement(imgCompanySectorDependencyLookUp).Click();
            Thread.Sleep(2000);

            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);

            //Enter search frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("searchFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='searchFrame']")));
            Thread.Sleep(2000);

            //Enter dependency name
            driver.FindElement(txtSearchBox).Clear();
            driver.FindElement(txtSearchBox).SendKeys(covSectorDependencyName);
            Thread.Sleep(2000);

            CustomFunctions.MoveToElement(driver, driver.FindElement(btnGo));
            driver.FindElement(btnGo).Click();

            Thread.Sleep(4000);

            driver.SwitchTo().DefaultContent();

            //Enter results frame & click on the result
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("resultsFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='resultsFrame']")));
            Thread.Sleep(2000);
            driver.FindElement(linkCoverageSectorDependencyName).Click();
            Thread.Sleep(4000);

            //Switch back to original window
            CustomFunctions.SwitchToWindow(driver, 0);
        }

        public bool VerifyFiltersFunctionalityOnCoverageSectorDependencyPopUp(string file, string covSectorDependencyName)
        {
            bool result = false;

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            //Click Edit button on Company Sector detail page 
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditCompCoverageSector, 120);
            driver.FindElement(btnEditCompCoverageSector).Click();
            Thread.Sleep(2000);

            //Click on Coverage Sector Dependency LookUp icon
            WebDriverWaits.WaitUntilEleVisible(driver, imgCompanySectorDependencyLookUp, 120);
            driver.FindElement(imgCompanySectorDependencyLookUp).Click();
            Thread.Sleep(2000);

            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);

            //Enter search frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("searchFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='searchFrame']")));
            Thread.Sleep(2000);

            //Clear Search box
            driver.FindElement(txtSearchBox).Clear();

            driver.SwitchTo().DefaultContent();

            //Enter results frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("resultsFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='resultsFrame']")));
            Thread.Sleep(2000);

            //Click on Show Filters link
            driver.FindElement(linkShowFilters).Click();

            //Enter filter values
            driver.FindElement(inputCoverageType).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 1));
            driver.FindElement(inputPrimarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 2));
            driver.FindElement(inputSecondarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 3));
            driver.FindElement(inputTertiarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 4));

            //Click on Apply filters button
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(2000);

            if(driver.FindElement(linkCoverageSectorDependencyName).Text == covSectorDependencyName)
            {
                //Select the desired dependency name from the result
                driver.FindElement(linkCoverageSectorDependencyName).Click();
                Thread.Sleep(4000);

                //Switch back to original window
                CustomFunctions.SwitchToWindow(driver, 0);

                result = true;
            }
            return result;
        }

        public void SaveNewCompanySectorDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveCompanySector, 120);
            driver.FindElement(btnSaveCompanySector).Click();
            Thread.Sleep(2000);
        }

        public string GetCompanySectorName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanySectorName, 120);
            string name = driver.FindElement(valCompanySectorName).Text;
            return name;
        }

        public void NavigateToCompanyDetailPageFromCompanySectorDetailPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCompanyName, 120);
            driver.FindElement(linkCompanyName).Click();
            Thread.Sleep(2000);
        }

        public void NavigateToCoverageSectorDependenciesPage()
        {
            driver.FindElement(shwAllTab).Click();
            Thread.Sleep(2000);
            driver.FindElement(imgCoverageSectorDependencies).Click();
            Thread.Sleep(2000);
        }

        public bool VerifyCompanySectorAddedToCompanyOrNot(string sectorName)
        {
            Thread.Sleep(5000);
            bool result = false;
            if(driver.FindElement(linkSectorName).Text == sectorName)
            {
                result = true;
            }
            return result;
        }

        public void DeleteCompanySector()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkSectorName, 120);
            driver.FindElement(linkSectorName).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCompanySector, 120);
            driver.FindElement(btnDeleteCompanySector).Click();
            Thread.Sleep(2000);

            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(3000);
        }

        //To click Coverage Team link 
        public string ClickCoverageTeam()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCoverageTeam, 120);
            driver.FindElement(linkCoverageTeam).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkOfficerName, 120);
            driver.FindElement(linkOfficerName).Click();
            string title = driver.FindElement(titlePage).Text;            
            return title;
        }

        //To click Company Member List
        public string ClickCompanyList()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCompany, 120);
            driver.FindElement(linkCompany).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkCompanyList, 120);
            driver.FindElement(linkCompanyList).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkCompanyMember, 120);
            driver.FindElement(linkCompanyMember).Click();
            string title = driver.FindElement(titleCoverageTeam).Text;
            return title;
        }

        //To click Campaigns Tab
        public string ClickCampaignsTab()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, linkCampaign, 120);
            driver.FindElement(linkCampaign).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnGo, 90);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkCampaignName, 120);
            driver.FindElement(linkCampaignName).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleCoverageTeam, 120);
            string title = driver.FindElement(titleCoverageTeam).Text;
            return title;
        }

        public int GetSizeOfCompanyFinancialList()
        {
            IList<IWebElement> companyFinancialList = driver.FindElements(By.XPath("//*[text()='Company Financials']/../../../../../following-sibling::div/table/tbody/tr"));
            return companyFinancialList.Count;
        }

        //Get heading of company details page
        public string GetCompanyDetailsHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, companyDetailHeading, 60);
            string headingCompanyDetail = driver.FindElement(companyDetailHeading).Text;
            return headingCompanyDetail;
        }

        //Get contact name under contacts section
        public string GetContactNameUnderContactsSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContactName, 60);
            string ContactName = driver.FindElement(valContactName).Text;
            return ContactName;
        }

        public string GetSecondContactNameUnderContactsSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSecondContactName, 60);
            string ContactName = driver.FindElement(valSecondContactName).Text;
            return ContactName;
        }

        public string GetThirdContactNameUnderContactsSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valThirdContactName, 60);
            string ContactName = driver.FindElement(valThirdContactName).Text;
            return ContactName;
        }

        //Get heading of company details page
        public string GetContactEmailUnderContactsSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContactEmail, 60);
            string ContactEmail = driver.FindElement(valContactEmail).Text;
            return ContactEmail;
        }

        //Get heading of company details page
        public string GetContactPhoneUnderContactsSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContactPhone, 60);
            string ContactPhone = driver.FindElement(valContactPhone).Text;
            return ContactPhone;
        }

        //Click new contact button
        public void ClickNewContactButton()
        {
            //CustomFunctions.ActionClicks(driver, btnNewContact, 20);

            WebDriverWaits.WaitUntilEleVisible(driver, btnNewContact, 120);
            driver.FindElement(btnNewContact).Click();
        }

        // Click opportunity button
        public void ClickOpportunityButton(string file, string CompanyType, string new_value, string add_value)
        {

            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            if (CompanyType.Equals("Houlihan Company"))//For Houlihan Lockey company
            {
                driver.FindElement(By.CssSelector($"input[value = '{new_value}']")).Click();
            }
            // For other companies
            else
            {
                Thread.Sleep(2000);
                driver.SwitchTo().Frame("066i0000004ZGBw");
                driver.FindElement(By.XPath($"//a[contains(text(),'{add_value}')]")).Click();
                driver.SwitchTo().DefaultContent();
            }
        }

        // Get Account Description value
        public string ERPAccountDescription(string companyRecordType)
        {
            if (companyRecordType.Equals("Capital Provider") || companyRecordType.Contains("Operating Company"))
            {
                return "External Customer";
            }
            else
            {
                return "HL Account";
            }
        }

        public string ERPCustomerType(string companyRecordType)
        {
            if (companyRecordType.Equals("Capital Provider") || companyRecordType.Contains("Operating Company"))
            {
                return "R";
            }
            else
            {
                return "I";
            }
        }

        public string ERPCustomerTypeDescription(string companyRecordType)
        {
            if (companyRecordType.Equals("Capital Provider") || companyRecordType.Contains("Operating Company"))
            {
                return "External";
            }
            else
            {
                return "Internal";
            }
        }

        //Select coverage team officer
        public void SelectCoverageTeamOfficer()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCoverageOfficer, 120);
            driver.FindElement(linkCoverageOfficer).Click();
        }

        // Click new coverage team button
        public void ClickNewCoverageTeam()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCoverageTeam, 120);
            driver.FindElement(btnNewCoverageTeam).Click();
        }

        //Click on new company financial button
        public void ClickNewCompanyFinancial(string file, string CompanyType)
        {
            if (CustomFunctions.IsElementPresent(driver, errPage))
            {
                companyHome.SearchCompany(file, CompanyType);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCompanyFinancial, 120);
            driver.FindElement(btnNewCompanyFinancial).Click();
        }

        //Get coverage team edit page title
        public string GetCoverageTeamEditPageHeading()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, coverageTeamEditHeading, 60);
            string headingCoverageTeamEdit = driver.FindElement(coverageTeamEditHeading).Text;
            return headingCoverageTeamEdit;
        }

        // To identify required tags/mandatory fields in Coverage team edit page
        public IWebElement CoverageTeamEditRequiredTag(string tagName, string fieldName)
        {
            return driver.FindElement(By.XPath($"//{tagName}[@id='{fieldName}']/../..//div"));
            //input[@id='CF00Ni000000D7bV0']/../../div
        }

        // Validate mandatory fields on FS and Standard coverage team page
        public bool ValidateMandatoryFields()
        {
            return CoverageTeamEditRequiredTag("input", "CF00Ni000000D7bV0").GetAttribute("class").Contains("requiredBlock") &&
            CoverageTeamEditRequiredTag("input", "CF00Ni000000D7ba0").GetAttribute("class").Contains("requiredBlock") &&
            CoverageTeamEditRequiredTag("select", "00Ni000000D7bha").GetAttribute("class").Contains("requiredBlock") &&
            CoverageTeamEditRequiredTag("select", "00Ni000000FjXsE").GetAttribute("class").Contains("requiredBlock") &&
             CoverageTeamEditRequiredTag("select", "00Ni000000FjXsJ").GetAttribute("class").Contains("requiredBlock");
        }

        //Clear text of company on coverage team edit page
        public void ClearTextCompanyName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyName);
            driver.FindElement(txtCompanyName).Clear();
        }

        // Click save button on coverage team edit page
        public void ClickSaveButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveCoverageTeam);
            driver.FindElement(btnSaveCoverageTeam).Click();
        }

        //Validate  priority help text
        public string GetPriorityHelpText()
        {
            IWebElement toolTipText = driver.FindElement(toolTipPriorityText);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            string htmlCode = (string)js.ExecuteScript("return arguments[0].innerHTML;", toolTipText);
            string toolTipPriorityTxt = htmlCode.Split(',')[1].Trim();
            return toolTipPriorityTxt;
        }

        public bool ValidatePriorityHelpObject()
        {
            bool available = CustomFunctions.IsElementPresent(driver, toolTipPriorityHelp);
            return available;
        }

        public void DeleteSalesforceCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCompany, 120);
            driver.FindElement(btnDeleteCompany).Click();

            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        public void DeleteCompany(string file, string CompanyType)
        {
            companyHome.SearchCompany(file, CompanyType);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCompany, 120);
            driver.FindElement(btnDeleteCompany).Click();

            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        public void DeleteCompanyContract(string file, string CompanyType)
        {
            if (CustomFunctions.IsElementPresent(driver, errPage))
            {
                companyHome.SearchCompany(file, CompanyType);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCompany, 120);
            driver.FindElement(btnDeleteCompany).Click();

            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkContractName, 60);
            driver.FindElement(linkContractName).Click();

            CustomFunctions.ActionClicks(driver, btnDeleteCompany);
            Thread.Sleep(3000);
            alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        public string GetCompanyName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyName, 60);
            string CompanyNameFromDetail = driver.FindElement(valCompanyName).Text.Split('[')[0].Trim();
            return CompanyNameFromDetail;
        }

        public string GetCompanyStateProvince()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valState, 60);
            string headingCompanyDetail = driver.FindElement(valState).Text;
            return headingCompanyDetail;
        }

        public string GetCompanyType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyType, 60);
            string CompanyTypeFromDetail = driver.FindElement(valCompanyType).Text.Split('[')[0].Trim();
            return CompanyTypeFromDetail;
        }

        public string GetCompanyAddress()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAddress, 60);
            string CompanyAddressFromDetail = driver.FindElement(valAddress).Text;
            return CompanyAddressFromDetail;
        }

        public void ClickEditButton(string file, string CompanyType)
        {
            if (CustomFunctions.IsElementPresent(driver, errPage))
            {
                companyHome.SearchCompany(file, CompanyType);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit);
            driver.FindElement(btnEdit).Click();
        }

        public void EditCompanyAddress(string file, string CompanyType)
        {
            if (CustomFunctions.IsElementPresent(driver, errPage))
            {
                companyHome.SearchCompany(file, CompanyType);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit);
            driver.FindElement(btnEdit).Click();
            //IList<IWebElement> rows = driver.FindElements(comboStateProvince);
            SelectElement element = new SelectElement(driver.FindElement(comboStateProvince));
            IList<IWebElement> options = element.Options;
            for (int i = 1; i <= options.Count; i++)
            {
                IWebElement defaultSelected = element.SelectedOption;
                element.SelectByIndex(i);

                IWebElement updateValue = element.SelectedOption;
                if (updateValue.Text.Equals(defaultSelected.Text))
                {
                    element.SelectByIndex(i + 1);
                    break;
                }
                break;
            }
            //if (CompanyType.Equals("Operating Company"))
            //{
            string valShippingProvince = CustomFunctions.RandomValue();
            driver.FindElement(txtShippingProvince).Clear();
            driver.FindElement(txtShippingProvince).SendKeys(valShippingProvince);
            //}

            driver.FindElement(btnSaveCompanyEdit).Click();
        }

        public void EditCompanyContact(string email, string phone, int row)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkContactEdit);
            driver.FindElement(linkContactEdit).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, txtContactEmail, 120);

            driver.FindElement(txtContactEmail).Clear();
            Thread.Sleep(2000);
            driver.FindElement(txtContactEmail).SendKeys(email);
            //Enter phone
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactPhone, 40);
            driver.FindElement(txtContactPhone).Clear();
            driver.FindElement(txtContactPhone).SendKeys(phone);

            // Click save button
            WebDriverWaits.WaitUntilEleVisible(driver, btnContactSave);
            driver.FindElement(btnContactSave).Click();
        }

        //Function to get company sub type
        public string GetCompanySubType()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, valCompanySubType, 60);
            string CompanySubTypeFromDetail = driver.FindElement(valCompanySubType).Text;
            return CompanySubTypeFromDetail;
        }

        public string GetOwnership()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valOwnership, 60);
            string OwnershipValueFromDetail = driver.FindElement(valOwnership).Text;
            return OwnershipValueFromDetail;
        }

        public string GetParentCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valParentCompany, 60);
            string ParentCompanyValueFromDetail = driver.FindElement(valParentCompany).Text;
            return ParentCompanyValueFromDetail;
        }

        public string GetIndustryFocus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valIndustryFocus, 60);
            string IndustryFocusValueFromDetail = driver.FindElement(valIndustryFocus).Text;
            return IndustryFocusValueFromDetail;
        }

        public string GetGeographicalFocus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valGeographicalFocus, 60);
            string GeographicalFocusValueFromDetail = driver.FindElement(valGeographicalFocus).Text;
            return GeographicalFocusValueFromDetail;
        }


        public string GetDealPreference()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valDealPreference, 60);
            string DealPreferenceValueFromDetail = driver.FindElement(valDealPreference).Text;
            return DealPreferenceValueFromDetail;
        }

        public string GetDescriptionValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valDescription, 60);
            string DescriptionValueFromDetail = driver.FindElement(valDescription).Text;
            return DescriptionValueFromDetail;
        }

        // Get CapIQ company value
        public string GetCapIQCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCapIQCompany, 60);
            string CapIQCompanyValueFromDetail = driver.FindElement(valCapIQCompany).Text;
            return CapIQCompanyValueFromDetail;
        }

        //Get heading of company details page
        public string GetCompanyLocation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valcompanyLocation, 60);
            string companyLocation = driver.FindElement(valcompanyLocation).Text;
            return companyLocation;
        }

        // Get company phone number
        public string GetCompanyPhoneNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valcompanyPhoneNumber, 60);
            string companyPhoneNumber = driver.FindElement(valcompanyPhoneNumber).Text;
            return companyPhoneNumber;
        }

        //Get company description
        public string GetCompanyDescription()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyDescription, 60);
            string companyDescription = driver.FindElement(valCompanyDescription).Text;
            return companyDescription;
        }

        //Function to get company complete address
        public string GetCompanyCompleteAddress()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompanyAddress, 60);
            string companyAddress = driver.FindElement(valCompanyAddress).Text;
            return companyAddress;
        }

        public string GetHQCheckBoxValue()
        {
            string title = driver.FindElement(chkBoxHQ).GetAttribute("title");
            return title;
        }

        public void ClickChangeCompanyTypeLink()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkChangeCompanyType);
            driver.FindElement(linkChangeCompanyType).Click();
        }

        public string ChangeCompanyRecordType(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string companyType = GetCompanyType();
            WebDriverWaits.WaitUntilEleVisible(driver, linkChangeCompanyType);
            driver.FindElement(linkChangeCompanyType).Click();
            if (companyType != (ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1)))
            {
                string recordType1 = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1);
                conSelectRecord.SelectCompanyRecordType(file, recordType1);
            }
            else
            {
                string recordType2 = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 3, 1);
                conSelectRecord.SelectCompanyRecordType(file, recordType2);
            }
            string companyTypeOnCompanyEditPage = companyEdit.GetCompanyType();

            IWebElement officecodeDropdown = driver.FindElement(drpdownOfficeCode);
            SelectElement select1 = new SelectElement(officecodeDropdown);
            select1.SelectByValue("BE");

            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveCompanyEdit);
            driver.FindElement(btnSaveCompanyEdit).Click();

            return companyTypeOnCompanyEditPage;
        }

        public string GetCreatedBy()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCreatedBy, 60);
            string CreatedByName = driver.FindElement(valCreatedBy).Text.Split(',')[0].Trim();
            return CreatedByName;
        }

        public string GetCompanyFinancialYear(int rows)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"div[id*='ke_body'] > table > tbody > tr:nth-child({rows}) > td:nth-child(3)"), 60);
            string CompanyFinancialYear = driver.FindElement(By.CssSelector($"div[id*='ke_body'] > table > tbody > tr:nth-child({rows}) > td:nth-child(3)")).Text;
            return CompanyFinancialYear;
        }


        public string GetAsOfYearFinancialYear(int rows)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, By.CssSelector($"div[id*='ke_body'] > table > tbody > tr:nth-child({rows}) > td:nth-child(4)"), 60);
            string CompanyFinancialAsOfDate = driver.FindElement(By.CssSelector($"div[id*='ke_body'] > table > tbody > tr:nth-child({rows}) > td:nth-child(4)")).Text;
            return CompanyFinancialAsOfDate;

        }

        public string GetFinancialsYearAnnualFinancial()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valFinancialsYearAnnualFinancial, 60);
            string FinancialsYearAnnualFinancial = driver.FindElement(valFinancialsYearAnnualFinancial).Text;
            return FinancialsYearAnnualFinancial;
        }

        public string GetFinancialsDateAnnualFinancial()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valFinancialsDateAnnualFinancial, 60);
            string FinancialsDateAnnualFinancial = driver.FindElement(valFinancialsDateAnnualFinancial).Text;
            return FinancialsDateAnnualFinancial;
        }
        public string GetNoRecordTextCompanyFinancial()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNoRecordsCompanyFinancial, 60);
            string NoRecordsCompanyFinancial = driver.FindElement(valNoRecordsCompanyFinancial).Text;
            return NoRecordsCompanyFinancial;
        }

        public string GetERPBillToAddressFlag()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBillToAddressFlag, 60);
            string valueERPBillToAddressFlag = driver.FindElement(valERPBillToAddressFlag).Text;
            return valueERPBillToAddressFlag;
        }

        public string GetERPShipToAddressFlag()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPShipToAddressFlag, 60);
            string valueERPShipToAddressFlag = driver.FindElement(valERPShipToAddressFlag).Text;
            return valueERPShipToAddressFlag;
        }

        public void DeleteCompanyFinancialRecord(string file, string CompanyType)
        {
            if (CustomFunctions.IsElementPresent(driver, errPage))
            {
                companyHome.SearchCompany(file, CompanyType);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, linkDelCompanyFinancial);
            if (CustomFunctions.IsElementPresent(driver, linkDelCompanyFinancial))
            {
                driver.FindElement(linkDelCompanyFinancial).Click();

                Thread.Sleep(2000);
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("All records are deleted");
            }
        }

        public void DeleteContactRecord()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, linkDelContact);
            if (CustomFunctions.IsElementPresent(driver, linkDelContact))
            {
                driver.FindElement(linkDelContact).Click();

                Thread.Sleep(2000);
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("All records are deleted");
            }
        }

        public void DeleteCoverageTeamRecord(string file, string CompanyType)
        {
            if (CustomFunctions.IsElementPresent(driver, errPage))
            {
                companyHome.SearchCompany(file, CompanyType);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, linkDelCoverageTeam);
            driver.FindElement(linkDelCoverageTeam).Click();

            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        public void DeleteCoverageSector(string file, string CompanyType)
        {
            if (CustomFunctions.IsElementPresent(driver, errPage))
            {
                companyHome.SearchCompany(file, CompanyType);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, linkCoverageSector);
            driver.FindElement(linkCoverageSector).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteCoverageSector);
            driver.FindElement(btnDeleteCoverageSector).Click();

            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        public string GetCoverageTeamOfficer()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCoverageTeamOfficer, 60);
            string CoverageTeamOfficer = driver.FindElement(linkCoverageTeamOfficer).Text;
            return CoverageTeamOfficer;
        }

        public string GetCoverageLevel()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCoverageLevel, 60);
            string CoverageLevel = driver.FindElement(valCoverageLevel).Text;
            return CoverageLevel;
        }

        public string GetCoverageType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCoverageType, 60);
            string CoverageType = driver.FindElement(valCoverageType).Text;
            return CoverageType;
        }
        
       
   
        public string GetValERPSubmittedToSync()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPSubmittedToSync, 120);
            //string valueERPSubmittedToSync = driver.FindElement(valERPSubmittedToSync).Text;

            string valueERPSubmittedToSync = driver.FindElement(valERPSubmittedToSync).GetAttribute("value");
            return valueERPSubmittedToSync;
        }


        public string GetValERPLastIntegrationResponseDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntegrationResponseDate, 60);
            //string valueERPLastIntegrationResponseDate = driver.FindElement(valERPLastIntegrationResponseDate).Text;
            string valueERPLastIntegrationResponseDate = driver.FindElement(valERPLastIntegrationResponseDate).GetAttribute("value");
            return valueERPLastIntegrationResponseDate;
        }

        public string GetERPAccountDescription()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPAccountDescription, 60);
            string valueERPAccountDescription = driver.FindElement(valERPAccountDescription).Text;
            return valueERPAccountDescription;
        }

        public string GetValERPLastIntegrationStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntegrationStatus, 60);
            string valueERPLastIntegrationStatus = driver.FindElement(valERPLastIntegrationStatus).Text;
            return valueERPLastIntegrationStatus;
        }

        public string GetValERPLastIntegrationErrorCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntegrationErrorCode, 60);
            string valueERPLastIntegrationErrorCode = driver.FindElement(valERPLastIntegrationErrorCode).Text;
            return valueERPLastIntegrationErrorCode;
        }

        public string GetValERPLastIntegrationErrorDescription()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntegrationErrorDescription, 60);
            string valueERPLastIntegrationErrorDescription = driver.FindElement(valERPLastIntegrationErrorDescription).Text;
            return valueERPLastIntegrationErrorDescription;
        }

        public bool IsClientNumberPresent()
        {
            return CustomFunctions.isTextPresent(driver, driver.FindElement(valClientNumber));
        }

        public string GetClientNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valClientNumber, 60);
            string valueClientNumber = driver.FindElement(valClientNumber).Text;
            return valueClientNumber;
        }

        public string GetContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPrimaryContactAvailable, 60);
            string valuePrimaryContactAvailable = driver.FindElement(valPrimaryContactAvailable).Text;
            return valuePrimaryContactAvailable;
        }

        public string CheckERPOracleDetailsExists()
        {
            try
            {
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPAccountID));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPOrgPartyId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPBillToAddressId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPBillToLocationId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPBillToLocation));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPBillToSiteId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPShipToAddressId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPShipToLocationId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPShipToSiteId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPContactId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPPersonPartyId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPContactPointEmailId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPContactPointPhoneId));
                CustomFunctions.isTextPresent(driver, driver.FindElement(valERPContactPointRelationshipId));
                return "ERP Oracle details exists";
            }
            catch
            {
                return "ERP Oracle Detail does not exists";
            }
        }

        public string GetERPContactFirstName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContactFirstName, 60);
            string valueFirstName = driver.FindElement(valERPContactFirstName).Text;
            return valueFirstName;
        }

        public string GetERPContactLastName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContactLastName, 60);
            string valueLastName = driver.FindElement(valERPContactLastName).Text;
            return valueLastName;
        }

        public string GetERPContactEmail()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContactEmail, 120);
            string valueERPContactEmail = driver.FindElement(valERPContactEmail).Text;
            return valueERPContactEmail;
        }

        public string GetERPContactPhone()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContactPhone, 60);
            string valueERPContactPhone = driver.FindElement(valERPContactPhone).Text;
            return valueERPContactPhone;
        }

        public string GetERPCustomerType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPCustomerType, 60);
            string valueERPCustomerType = driver.FindElement(valERPCustomerType).Text;
            return valueERPCustomerType;
        }

        public string GetERPCustomerTypeDescription()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPCustomerTypeDesc, 60);
            string valueERPCustomerTypeDesc = driver.FindElement(valERPCustomerTypeDesc).Text;
            return valueERPCustomerTypeDesc;
        }

        public string GetERPContactFlag()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContactFlag, 60);
            string valueERPContactFlag = driver.FindElement(valERPContactFlag).Text;
            return valueERPContactFlag;
        }

        public string GetERPContactPointEmailFlag()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContactPointEmailFlag, 60);
            string valueERPContactPointEmailFlag = driver.FindElement(valERPContactPointEmailFlag).Text;
            return valueERPContactPointEmailFlag;
        }

        public string GetERPContactPointPhoneFlag()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContactPointPhoneFlag, 60);
            string valueERPContactPointPhoneFlag = driver.FindElement(valERPContactPointPhoneFlag).Text;
            return valueERPContactPointPhoneFlag;
        }

        public string GetERPOrgPartyId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPOrgPartyId, 60);
            string valueERPOrgPartyId = driver.FindElement(valERPOrgPartyId).Text;
            return valueERPOrgPartyId;
        }

        public string GetERPBillToAddressId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPBillToAddressId, 60);
            string valueERPBillToAddressId = driver.FindElement(valERPBillToAddressId).Text;
            return valueERPBillToAddressId;
        }

        public string GetERPContactId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContactId, 60);
            string valueERPContactId = driver.FindElement(valERPContactId).Text;
            return valueERPContactId;
        }

        public string GetERPShipToAddressId()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPShipToAddressId, 60);
            string valueERPShipToAddressId = driver.FindElement(valERPShipToAddressId).Text;
            return valueERPShipToAddressId;
        }

        public void ClickPrimaryBillingContact(int row)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkPrimaryBillingContact, 120);
            driver.FindElement(linkPrimaryBillingContact).SendKeys(Keys.Control + Keys.Return);
            if (row.Equals(2))
            {
                CustomFunctions.SwitchToWindow(driver, 3);
            }
            else
            {
                CustomFunctions.SwitchToWindow(driver, 6);
            }
        }

        public void ClickPrimaryBillingContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkPrimaryBillingContact, 120);
            driver.FindElement(linkPrimaryBillingContact).SendKeys(Keys.Control + Keys.Return);
            CustomFunctions.SwitchToWindow(driver, 3);
        }

        //Function is to edit company record type
        public void EditCompanyRecordType(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;

            int CompanyRecordList = ReadExcelData.GetRowCount(excelPath, "CompanyRecordTypes");

            for (int row = 2; row <= CompanyRecordList; row++)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, changelinkCompanyRecordType, 120);
                driver.FindElement(changelinkCompanyRecordType).Click();
                IWebElement recordDropdown = driver.FindElement(drpdwnCompanyRecordType);
                SelectElement select = new SelectElement(recordDropdown);

                IWebElement currentRecordType = driver.FindElement(txtCurrentRecordType);

                string companyRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyRecordTypes", row, 3);
                Console.WriteLine(currentRecordType.Text);
                Console.WriteLine(companyRecordTypeExl);
                
                IList<IWebElement> options = select.Options;
                IWebElement companyRecordTypeOption = options[row - 2];
                companyRecordTypeOption.Click();
                driver.FindElement(btnContinue).Click();

                WebDriverWaits.WaitUntilEleVisible(driver, comboFlagReason, 120);
                driver.FindElement(comboFlagReason).SendKeys("Company Renamed");
                WebDriverWaits.WaitUntilEleVisible(driver, txtFlagReasonComment, 120);
                driver.FindElement(txtFlagReasonComment).SendKeys("Test");

                WebDriverWaits.WaitUntilEleVisible(driver, btnSaveCoverageTeam, 120);

                if ((companyRecordTypeExl).Equals("Conflicts Check LDCCR"))
                {
                    IWebElement officecodeDropdown = driver.FindElement(drpdownOfficeCode);
                    SelectElement select1 = new SelectElement(officecodeDropdown);
                    select1.SelectByValue("BE");
                }
                driver.FindElement(btnSaveCoverageTeam).Click();
            }

        }

        public string GetCompanyTypeL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyType, 30);
            return driver.FindElement(txtCompanyType).Text;
        }
        
        // Click on tab from Company Detail page 

        public bool ClickCompanyDetailPageTabL(string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _homePageTab(value), 30);
            driver.FindElement(_homePageTab(value)).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, _tabHeader(value), 30);
                return driver.FindElement(_tabHeader(value)).Displayed;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public bool IsOpportunitiesSearchBoxL()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, searchOpportunitiesL, 30);
                return driver.FindElement(searchOpportunitiesL).Displayed;
            }
            catch { return false; }
        }

        public bool IsOppoortunitiesFoundByNameL(string name)
        {
            By recordByName = By.XPath($"//lightning-datatable//table//tbody//tr//a[text()='{name}']");
            WebDriverWaits.WaitUntilEleVisible(driver, searchOpportunitiesL, 10);
            driver.FindElement(searchOpportunitiesL).SendKeys(name);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, recordByName, 10);
                return driver.FindElement(recordByName).Displayed;
            }
            catch { return false; }
        }

        public bool IsOppoortunitiesFoundByNumberL(string number)
        {
            By recordByNumber = By.XPath($"//lightning-datatable//table//tbody//tr//td[@data-label='Opportunity Number']//lightning-base-formatted-text[text()='{number}']");
            WebDriverWaits.WaitUntilEleVisible(driver, searchOpportunitiesL, 10);
            driver.FindElement(searchOpportunitiesL).Clear();
            driver.FindElement(searchOpportunitiesL).SendKeys(number);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, recordByNumber, 10);
                return driver.FindElement(recordByNumber).Displayed;
            }
            catch { return false; }
        }

        public bool IsOppoortunitiesFoundByStageL(string stage)
        {
            By recordByStage = By.XPath($"//lightning-datatable//table//tbody//tr//td[contains(@data-label,'Priority')]//lightning-base-formatted-text[text()='{stage}']");
            WebDriverWaits.WaitUntilEleVisible(driver, searchOpportunitiesL, 10);
            driver.FindElement(searchOpportunitiesL).Clear();
            driver.FindElement(searchOpportunitiesL).SendKeys(stage);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, recordByStage, 10);
                return driver.FindElement(recordByStage).Displayed;
            }
            catch { return false; }
        }

        public bool IsEngagementSearchBoxL()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, searchEngagementsL, 30);
                return driver.FindElement(searchEngagementsL).Displayed;
            }
            catch { return false; }
        }

        public bool IsEngagementFoundByNameL(string name)
        {
            By recordByName = By.XPath($"//lightning-datatable//table//tbody//tr//a[text()='{name}']");
            WebDriverWaits.WaitUntilEleVisible(driver, searchEngagementsL, 10);
            driver.FindElement(searchEngagementsL).SendKeys(name);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, recordByName, 10);
                return driver.FindElement(recordByName).Displayed;
            }
            catch { return false; }
        }

        public bool IsEngagementFoundByNumberL(string number)
        {
            By recordByNumber = By.XPath($"//lightning-datatable//table//tbody//tr//td[@data-label='Engagement Number']//lightning-base-formatted-text[text()='{number}']");
            WebDriverWaits.WaitUntilEleVisible(driver, searchEngagementsL, 10);
            driver.FindElement(searchEngagementsL).Clear();
            driver.FindElement(searchEngagementsL).SendKeys(number);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, recordByNumber, 10);
                return driver.FindElement(recordByNumber).Displayed;
            }
            catch { return false; }
        }


        public bool IsEngagementFoundByStageL(string stage)

        {
            By recordByStage = By.XPath($"//lightning-datatable//table//tbody//tr//td[contains(@data-label,'Stage')]//lightning-base-formatted-text[text()='{stage}']");
            WebDriverWaits.WaitUntilEleVisible(driver, searchEngagementsL, 10);
            driver.FindElement(searchEngagementsL).Clear();
            driver.FindElement(searchEngagementsL).SendKeys(stage);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, recordByStage, 10);
                return driver.FindElement(recordByStage).Displayed;
            }
            catch { return false; }
        }

        public void CloseCompanyTabL(string name)
         {
            By buttonCloseTab = By.XPath($"//button[contains(@title,'Close {name}')]");            

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, alertDuplicate, 5);
                driver.FindElement(alertDuplicate).Click();
                driver.FindElement(buttonCloseTab).Click();
                Thread.Sleep(4000);
                driver.Navigate().Refresh();
                Thread.Sleep(4000);
            }
            catch (Exception e)
            {
                driver.FindElement(buttonCloseTab).Click();
                Thread.Sleep(4000);
                driver.Navigate().Refresh();
                Thread.Sleep(4000);

            }

        }
        public void ClickViewAllOpportunities()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkViewAllOppL, 10);
            driver.FindElement(linkViewAllOppL).Click();
        }
        public void ClickViewAllEngagements()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkViewAllEngL, 10);
            driver.FindElement(linkViewAllEngL).Click();
        }
        public void CloseViewAllPopup()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCloseViewAllPopup, 10);
            driver.FindElement(btnCloseViewAllPopup).Click();
        }
       
        public bool IsOppEngFoundByNameOnViewAllL(string name)
        {
            try
            {
                By recordByName = By.XPath($"//div[contains(@class,'slds-modal__content')]//lightning-datatable//table//tbody//tr//a[text()='{name}']");
                WebDriverWaits.WaitUntilEleVisible(driver, searchViewAllOppEngL, 10);
                driver.FindElement(searchViewAllOppEngL).Clear();
                driver.FindElement(searchViewAllOppEngL).SendKeys(name);
                WebDriverWaits.WaitUntilEleVisible(driver, recordByName, 10);
                return driver.FindElement(recordByName).Displayed;
            }

            catch { return false; }

        }

public bool IsOpportunitiesFoundByNumberOnViewAllL(string number)
        {
            try

            {

                By recordByNumber = By.XPath($"//div[contains(@class,'slds-modal__content')]//lightning-datatable//table//tbody//tr//td[@data-label='Opportunity Number']//lightning-base-formatted-text[text()='{number}']");

                WebDriverWaits.WaitUntilEleVisible(driver, searchViewAllOppEngL, 10);

                driver.FindElement(searchViewAllOppEngL).Clear();

                driver.FindElement(searchViewAllOppEngL).SendKeys(number);

                WebDriverWaits.WaitUntilEleVisible(driver, recordByNumber, 10);

                return driver.FindElement(recordByNumber).Displayed;

            }

            catch { return false; }

        }

        public bool IsEngagementsFoundByNumberOnViewAllL(string number)
        {
            try
            {
                By recordByNumber = By.XPath($"//div[contains(@class,'slds-modal__content')]//lightning-datatable//table//tbody//tr//td[@data-label='Engagement Number']//lightning-base-formatted-text[text()='{number}']");
                WebDriverWaits.WaitUntilEleVisible(driver, searchViewAllOppEngL, 10);
                driver.FindElement(searchViewAllOppEngL).Clear();
                driver.FindElement(searchViewAllOppEngL).SendKeys(number);
                WebDriverWaits.WaitUntilEleVisible(driver, recordByNumber, 10);
                return driver.FindElement(recordByNumber).Displayed;
            }
            catch { return false; }
        }

        public bool IsOppEngFoundByStageOnViewAllL(string stage)
        {
            try
            {
                By recordByStage = By.XPath($"//div[contains(@class,'slds-modal__content')]//lightning-datatable//table//tbody//tr//td[contains(@data-label,'Stage')]//lightning-base-formatted-text[text()='{stage}']");
                WebDriverWaits.WaitUntilEleVisible(driver, searchViewAllOppEngL, 10);
                driver.FindElement(searchViewAllOppEngL).Clear();
                driver.FindElement(searchViewAllOppEngL).SendKeys(stage);
                WebDriverWaits.WaitUntilEleVisible(driver, recordByStage, 10);
                return driver.FindElement(recordByStage).Displayed;

            }

            catch { return false; }

        }      

        public void CloseCoverageTeamDetailPageL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, buttonCloseCoverageTab, 20);
            driver.FindElement(buttonCloseCoverageTab).Click();
            Thread.Sleep(5000);
            driver.Navigate().Refresh();
            Thread.Sleep(5000);
        }
        public bool IsContactNestedListHLRelationshipL(string contactName)
        {
            try
            {
                By btnNestedHLRelationship = By.XPath($"//a[contains(@class,'NestedTables')][text()='{contactName}']//parent::td//preceding-sibling::td//button");
                WebDriverWaits.WaitUntilEleVisible(driver, btnNestedHLRelationship, 20);
                return driver.FindElement(btnNestedHLRelationship).Displayed;
            }
            catch { return false; }
        }
        public bool IsCoverageNestedListOfficerL(string contactName)
        {
            try
            {
                By btnNestedHLRelationship = By.XPath($"//article//div[contains(@class,'NestedTables')]//table//tr//td[@data-label='Officer Name']//a[text()='{contactName}']//ancestor::td//preceding-sibling::td//button");
                WebDriverWaits.WaitUntilEleVisible(driver, btnNestedHLRelationship, 20);
                return driver.FindElement(btnNestedHLRelationship).Displayed;
            }
            catch { return false; }
        }
        public string ClickContactNestedListHLRelationshipL(string contactName)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            By btnNestedHLRelationship = By.XPath($"//a[contains(@class,'NestedTables')][text()='{contactName}']//parent::td//preceding-sibling::td//button");
            WebDriverWaits.WaitUntilEleVisible(driver, btnNestedHLRelationship, 20);
            //driver.FindElement(btnNestedHLRelationship).Click();
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnNestedHLRelationship));

            WebDriverWaits.WaitUntilEleVisible(driver, headerNestedListL, 20);
            return driver.FindElement(headerNestedListL).Text;
        }



        public string ClickCoverageNestedList(string contactName)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            By btnNestedHLRelationshipL = By.XPath($"//article//div[contains(@class,'NestedTables')]//table//tr//td[@data-label='Officer Name']//a[text()='{contactName}']//ancestor::td//preceding-sibling::td//button");
            //WebDriverWaits.WaitUntilEleVisible(driver, btnNestedHLRelationshipL, 20);
            //driver.FindElement(btnNestedHLRelationship).Click();
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnNestedHLRelationshipL));



            WebDriverWaits.WaitUntilEleVisible(driver, headerNestedListL, 20);
            return driver.FindElement(headerNestedListL).Text;
        }




        public string GetCompanyHLRelationshipContactL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyHLRelationshipContactL, 20);
            return driver.FindElement(txtCompanyHLRelationshipContactL).Text;
        }



        public string GetCompanyHLRelationshipOfficerNameL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyHLRelationshipCoverageOfficerL, 20);
            return driver.FindElement(txtCompanyHLRelationshipCoverageOfficerL).Text;
        }
        public void ClickCompanyNestedContactL(string contactName)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            By linkNestedHLRelationshipL = By.XPath($"//a[contains(@class,'NestedTables')][text()='{contactName}']");
            //driver.FindElement(linkNestedHLRelationship ).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkNestedHLRelationshipL, 20);
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(linkNestedHLRelationshipL));



        }
        public void ClickNestedCoverageTeamOfficerL(string officerName)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            By btnNestedHLRelationshipL = By.XPath($"//article//div[contains(@class,'NestedTables')]//table//tr//td[@data-label='Officer Name']//a[text()='{officerName}']");
            // WebDriverWaits.WaitUntilEleVisible(driver, btnNestedHLRelationshipL, 20);
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(btnNestedHLRelationshipL));



        }

        public bool IsCoverageTeamDetailsPageDisplayedL(string value)
        {
            By titleCoverageteamL = By.XPath($"//div[contains(@class,'page-header')]//h1//div[contains(@class,'NameTitle')][contains(text(),'{value}')]");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, titleCoverageteamL, 20);
                return driver.FindElement(titleCoverageteamL).Displayed;
            }
            catch { return false; }
        }

        public string GetCoverageTeamCompanyNameL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCoverageTeamCompanyNameL, 20);
            return driver.FindElement(txtCoverageTeamCompanyNameL).Text;
        }

        public string GetCoverageOfficerNameL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCoverageTeamOfficerNameL, 20);
            return driver.FindElement(txtCoverageTeamOfficerNameL).Text;
        }

        public string GetCompanyOfficeNameCoverageTypeL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyDetailCoverageTypeL, 20);
            return driver.FindElement(txtCompanyDetailCoverageTypeL).Text;
        }

        public void ClickCoverageSectorPanelL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, panelCoverageSector, 20);
            driver.FindElement(panelCoverageSector).Click();
        }

        public string GetOfficerCoverageTypeL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, panelCoverageTypeL, 20);
            return driver.FindElement(panelCoverageTypeL).Text;
        }
        
        public bool IsIndustryTypePresent(string industryType)
        {
            bool isFound = false;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit);
            driver.FindElement(btnEdit).Click();

            WebDriverWaits.WaitUntilEleVisible(driver, comboIndustryType);
            driver.FindElement(comboIndustryType).Click();

            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboIndustryTypeOptions);
                 var actualValue = valTypes.Select(x => x.Text).ToArray();

            for (int row = 0; row <= actualValue.Length; row++)

            {

                if (actualValue[row].Contains(industryType))

                {

                    isFound = true;

                    driver.FindElement(btnCancel).Click();

                    break;

                }

            }


            return isFound;

        }

        public void ClickDetailPageQuickLink(string linkName)

        {

            WebDriverWaits.WaitUntilEleVisible(driver, _DetailPageQuickLink(linkName));

            driver.FindElement(_DetailPageQuickLink(linkName)).Click();

        }


        public void ClickNewCoverageTeamButton()

        {

            WebDriverWaits.WaitUntilEleVisible(driver, btnNewCoverageTeam);

            driver.FindElement(btnNewCoverageTeam).Click();

        }





        public bool IsIndustryTypePresentonCoverageTeam(string industryType)

        {

            bool isFound = false;



            WebDriverWaits.WaitUntilEleVisible(driver, comboType);

            driver.FindElement(comboType).Click();

            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboTypeOption);

            var actualValue = valTypes.Select(x => x.Text).ToArray();

            for (int row = 0; row <= actualValue.Length; row++)

            {

                if (actualValue[row].Contains(industryType))

                {

                    isFound = true;

                    driver.FindElement(btnCancel).Click();

                    break;

                }

            }

            return isFound;

        }


    }
}

