using OpenQA.Selenium;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SF_Automation.Pages.Opportunity
{
    class AddOppCounterparty : BaseClass
    {
        By btnEdit = By.CssSelector("input[value=' Edit ']");
        By btnCounterparties = By.CssSelector(".pbButton > input[title = 'Counterparties']");
        By btnAddCounterparties = By.CssSelector(".pbButton >table>tbody>tr>td> input[value='Add Counterparties']");
        By btnDel = By.Id("sf_filter_remove_btn_1");
        By btnAddRow = By.Id("sf_filter_add_btn_and");
        By comboCity = By.Id("sf_filter_field_1");
        By txtCityName = By.Id("sf_value_1");
        By btnSearch = By.CssSelector("td>#search_btn");
        By checkCity = By.Id("thePage:theForm3:pbResult:tickTable:0:myCheckbox");
        By btnAddRec = By.Id("add_btn");
        By msgSuccess = By.CssSelector("td.messageCell>div");
        By btnBack = By.Id("back_btn");
        By lnkDetails = By.CssSelector(".view_record__c > a");
        By btnAddOppCounterPartyÇontact = By.CssSelector("input[value='New Opportunity Counterparty Contact']");
        By btnSave = By.Name("save");
        By txtContact = By.CssSelector("span[class='lookupInput']>input[name*='0D738V']");
        By listContact = By.XPath("/tbody/tr");
        By rowCPContact = By.CssSelector("table > tbody > tr.dataRow.even.last.first");
        By linkOpp = By.XPath("//td[text() = 'Opportunity']/following-sibling::td/child::div/child::a");
        By titlePage = By.CssSelector("h1[class='pageType']");
        By lblFilter = By.CssSelector("h3[class*='active ui-corner-top']>a");
        By lblExistingOpp = By.CssSelector("h3:nth-child(3) > a");
        By lblExistingCompany = By.CssSelector("h3:nth-child(5) > a");
        By lblOpp = By.CssSelector("div[class*='bottom ui-accordion-content-active']>div>table>tbody>tr>td>b");
        By txtLookUp = By.CssSelector("span[class='lookupInput']>input[id*='theForm:j_id65:1:j_id67']");
        By btnCancelBack = By.CssSelector("input[name*='id64']");
        By txtStaff = By.CssSelector("input[placeholder*='Begin Typing Name']");
        By listStaff = By.XPath("/html/body/ul");
        By chkInitiator = By.CssSelector("input[name*='0:j_id75']");
        By btnReturnToOpp = By.CssSelector("input[value*='Return To Opportunity']");
        By chkInternalTeamPrompt = By.CssSelector("input[name*='FnLTz']");
        By btnSaveRoles = By.CssSelector("input[value='Save']");
        By txtOpp = By.CssSelector("span>input[name*='id65:0:j_id67']");
        By btnSearchOpp = By.CssSelector("td>#search_btn2");
        By checkRow = By.CssSelector("#dtable > div.fix-column > div.tbody > div > div > div:nth-child(1) > input.targetCheck");
        By btnDelete = By.CssSelector("input[value='Delete']");
        By msgText = By.CssSelector("span[id*=':f']> div");

        //Lightning
        By btnViewCounterpartiesL = By.XPath("//button[text()='View Counterparties']");
        By tabCounterpartyEditorL = By.XPath("//span[text()='Counterparty Editor']");
        By btnAddCounterpartiesL = By.XPath("//button[text()='Add Counterparties']");
        By btnAddCounterpartyL = By.XPath("//button[text()='Add Counterparty']");
        By lblNewOppCounterparty = By.XPath("//h2[text()='New Opportunity Counterparty']");
        By txtCompanyL = By.XPath("//label[text()='Company']/following::div[1]/div/lightning-base-combobox/div[1]/div/input");
        By comboType = By.XPath("//button[@aria-label='Type, --None--']");
        By btnSaveCounterpartyL = By.XPath("//button[@name='SaveEdit']");
        By btnNewOppCounterpartyContactL = By.XPath("//button[text()='New Opportunity Counterparty Contact']");
        By chkNameL = By.XPath("//tr/td[1]/lightning-primitive-cell-checkbox/span/label/span[1]");
        By btnAddContactL = By.XPath("//button[text()='Add Contact']");
        By btnBackL = By.XPath("//c-opportunity-counter-party-contact/lightning-card/article/div[1]/header/div[2]/slot/lightning-button/button");
        By lnkShowMore = By.XPath("//div/div[2]/div[2]/slot/flexipage-component2[2]/slot/lst-related-list-single-container/laf-progressive-container/slot/lst-related-list-single-app-builder-mapper/article/lst-related-list-view-manager/lst-common-list-internal/lst-list-view-manager-header/div/div[1]/div[3]/div/runtime_platform_actions-actions-ribbon/ul/li/lightning-button-menu/button/lightning-primitive-icon");
        By lnkNewOppCounterpartyComment = By.XPath("//span[text()='New Opportunity Counterparty Comment']");
        By txtRelatedOppL = By.XPath("//input[@placeholder='Search Opportunity Counterparties...']");
        By txtCommentsL = By.XPath("//slot/records-record-layout-text-area/lightning-textarea/div/textarea");
        By btnSaveCommentL = By.XPath("//li[3]/runtime_platform_actions-action-renderer/runtime_platform_actions-executor-lwc-headless/slot[1]/slot/lightning-button/button");
        By tabOppNumL = By.XPath("//section[1]/div/div/div/div/div/ul[2]/li[2]/a/span[2]");
        By lnkDetailsL = By.XPath("//lightning-layout-item/slot/div/table/tbody/tr/td[1]/div/div[1]/lightning-formatted-rich-text/span/a");
        By valCommentL = By.XPath("//h3/lst-template-list-field/lightning-base-formatted-text");
        By valOppCreator = By.XPath("//formula-output-formula-html/lightning-formatted-rich-text/span");
        By tabCounterparty = By.XPath("//span[normalize-space()='Counterparty Editor']");       
        By linkCompany = By.XPath("(//div[contains(@class, 'slds-scrollable_y')]//table/tbody/tr/th)[1]//a");
        By companyFromCompanyList = By.XPath("(//div[contains(@class, 'slds-scrollable_y')]//table/tbody/tr/th)[1]");


        private By _counterpartyButtons(string buttonName)
        {
            return By.XPath($"//button[contains(text(),'{buttonName}')]");
        }
        private By _subFilterEle(string sectionName)
        {
            return By.XPath($"//label[text()='{sectionName}']/following::div[3]/lightning-input/div/input");
        }
        private By _counterpartyCompanyEle(string companyName)
        {
            return By.XPath($"//div[@title='{companyName}']");
        }
        private By _comboTypeCounterpartyOptionEle(string value)
        {
            return By.XPath($"//span[@title='{value}']");
        }
        private By _closeCurrentTabEle(string tabText)
        {
            return By.XPath($"//button[@title='{tabText}']");
        }
        By fitersSectionsCounterparties = By.XPath("//h3[@class='slds-accordion__summary-heading']/button/span[@class='slds-accordion__summary-content']");
        By optionCompanyListElement = By.XPath("(//div[contains(@class,'slds-dropdown-trigger slds-dropdown-trigger_click slds-is-open')]//div[@role='option'])[1]");
        By buttonSearch = By.XPath("//section[@class='slds-accordion__section slds-is-open']//button[@title='Search'][normalize-space()='Search']");
        By hyperlinkedCompanies = By.XPath("//lightning-datatable//table[contains(@role,'grid')]//a[contains(@href,'/lightning/r')]");
        By checkBoxSelectItem = By.XPath("(//div[contains(@class, 'slds-scrollable_y')]//table/tbody/tr/td)[1]");
        By buttonAddCunterpartyToOpportunity = By.XPath("//button[@title='counterparty']");
        By msgLVPopup = By.CssSelector("span.toastMessage.forceActionsText");
        By listViewCounterparties = By.XPath("//label[contains(text(),'View')]");
        By buttonConfirmDelete = By.XPath("//footer//lightning-button[contains(@class,'confirmButton')]");
        By searchCompany = By.XPath("//input[@placeholder='Search Companies...']");
        By comboResultCompany = By.XPath("(//ul[@role='group']//li)[1]");
        By comboTypeCounterparty = By.XPath("(//button[contains(@aria-label,'Type')])[2]");
        By buttonSaveCounterparty = By.XPath("//button[@name='SaveEdit']");


        //To validate Counterparties button
        public string ValidateAndAddCounterparties()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            Thread.Sleep(4000);
            try
            {
                string value = driver.FindElement(btnCounterparties).Displayed.ToString();
                Console.WriteLine(value);
                driver.FindElement(btnCounterparties).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterparties, 60);
                driver.FindElement(btnAddCounterparties).Click();
                //if (driver.FindElement(btnDel).Displayed)
                //{
                //    driver.FindElement(btnDel).Click();
                //}
                //else
                //{
                //    Console.WriteLine("Del button not displayed");
                //}
                WebDriverWaits.WaitUntilEleVisible(driver, btnAddRow, 80);
                driver.FindElement(btnAddRow).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, comboCity, 90);
                driver.FindElement(comboCity).SendKeys("City");
                driver.FindElement(txtCityName).SendKeys("China");
                Thread.Sleep(5000);
                driver.FindElement(btnSearch).Click();
                Thread.Sleep(7000);
                //driver.FindElement(btnSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, checkCity, 100);
                Thread.Sleep(3000);
                driver.FindElement(checkCity).Click();
                driver.FindElement(btnAddRec).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 150);
                Thread.Sleep(3000);
                string message = driver.FindElement(msgSuccess).Text;
                driver.FindElement(btnBack).Click();
                return message;
            }
            catch (Exception)
            {
                driver.FindElement(btnSearch).Click();
                Thread.Sleep(4000);
                //driver.FindElement(btnSearch).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, checkCity, 100);
                Thread.Sleep(3000);
                driver.FindElement(checkCity).Click();
                driver.FindElement(btnAddRec).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 150);
                Thread.Sleep(3000);
                string message = driver.FindElement(msgSuccess).Text;
                driver.FindElement(btnBack).Click();
                return message;
            }
        }

        public void AddCounterpartyContact(string Name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDetails, 60);
            driver.FindElement(lnkDetails).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddOppCounterPartyÇontact, 80);
            driver.FindElement(btnAddOppCounterPartyÇontact).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave, 80);
            driver.FindElement(txtContact).SendKeys(Name);
            Thread.Sleep(3000);
            CustomFunctions.SelectValueWithoutSelect(driver, listContact, Name);
            driver.FindElement(btnSave).Click();
        }
        public string ValidateCPContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowCPContact, 80);
            string value = driver.FindElement(rowCPContact).Displayed.ToString();
            Console.WriteLine(value);
            if (value.Equals("True"))
            {
                return "CounterParty Contact added";
            }
            else
            {
                return "CounterParty Contact not added";
            }
        }
        public void ClickOpp()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkOpp, 60);
            driver.FindElement(linkOpp).Click();
        }
        //To validate Add Counterparites button is displayed
        public string ValidateAddCounterpartiesAndGetPageHeader(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelBack, 80);
            string excelPath = dir + file;
            string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
            Console.WriteLine(valUser);
            try
            {
                string value = driver.FindElement(btnAddCounterparties).Displayed.ToString();
                Console.WriteLine(value);
                driver.FindElement(btnAddCounterparties).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, titlePage, 60);
                string pageTitle = driver.FindElement(titlePage).Text;
                return pageTitle;
            }
            catch (Exception)
            {
                driver.FindElement(btnCancelBack).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
                driver.FindElement(btnEdit).Click();
                driver.FindElement(chkInternalTeamPrompt).Click();
                driver.FindElement(btnSave).Click();

                //Enter logged in user and assign Initiator role             
                WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 80);
                driver.FindElement(txtStaff).SendKeys(valUser);
                Thread.Sleep(3000);
                CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valUser);
                WebDriverWaits.WaitUntilEleVisible(driver, chkInitiator, 70);
                driver.FindElement(chkInitiator).Click();
                driver.FindElement(btnSaveRoles).Click();
                driver.FindElement(btnReturnToOpp).Click();

                //Click Counterparties and check for Add Counterparty button
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
                driver.FindElement(btnCounterparties).Click();
                driver.FindElement(btnAddCounterparties).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, titlePage, 60);
                string pageTitle = driver.FindElement(titlePage).Text;
                return pageTitle;
            }
        }
        //Validate Filter section
        public string ValidateFilterSection()
        {
            string section1 = driver.FindElement(lblFilter).Text;
            return section1;
        }
        //Validate existing Opportunity Section
        public string ValidateExistingOpportunitySection()
        {
            string section2 = driver.FindElement(lblExistingOpp).Text;
            return section2;
        }
        //Validate existing Company Section
        public string ValidateExistingCompanySection()
        {
            string section3 = driver.FindElement(lblExistingCompany).Text;
            return section3;
        }
        //Validate fields of existing Opportunity Section
        public string ValidateFieldsOfExistingOppSection()
        {
            driver.FindElement(lblExistingOpp).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblOpp, 60);
            string fieldOpp = driver.FindElement(lblOpp).Text;
            return fieldOpp;
        }
        //Validate fields of existing Company List Section
        public string ValidateFieldsOfExistingCompanySection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lblExistingCompany, 100);
            Thread.Sleep(3000);
            driver.FindElement(lblExistingCompany).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtLookUp, 100);
            string fieldOpp = driver.FindElement(txtLookUp).Displayed.ToString();
            return fieldOpp;
        }
        //Validate page after clicking back button
        public string ClickBackAndGetTitle()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnBack, 60);
            driver.FindElement(btnBack).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titlePage, 60);
            string title = driver.FindElement(titlePage).Text;
            return title;
        }

        //Add company from existing opportunity
        public string AddCompanyFromExistingOpp(string Name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpp, 80);
            driver.FindElement(txtOpp).SendKeys(Name);
            driver.FindElement(btnSearchOpp).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkCity, 80);
            driver.FindElement(checkCity).Click();
            driver.FindElement(btnAddRec).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 80);
            string message = driver.FindElement(msgSuccess).Text;
            return message;
        }

        //Validate if company get added
        public string ValidateAddedCompanyExists()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkRow, 80);
            string value = driver.FindElement(checkRow).Displayed.ToString();
            return value;
        }

        //Delete the added company
        public string DeleteAddedCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkRow, 140);
            driver.FindElement(checkRow).Click();
            driver.FindElement(btnDelete).Click();
            Thread.Sleep(3000);
            string text = driver.FindElement(msgText).Text;
            return text;
        }

        //Click Add Counterparties
        public void AddCounterparties()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterparties);
            driver.FindElement(btnAddCounterparties).Click();
        }

        //Lightning code--------------------------------
        //Click View Counterparties button
        public string ClickViewCounterparties()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewCounterpartiesL);
            driver.FindElement(btnViewCounterpartiesL).Click();
            Thread.Sleep(4000);
            string name = driver.FindElement(tabCounterpartyEditorL).Text;
            return name;
        }

        //Click Details link
        public void ClickDetailsLinkL()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDetailsL);
            driver.FindElement(lnkDetailsL).Click();
            Thread.Sleep(4000);
        }

        //Get added Counterparty Comment
        public string GetAddedCommentL()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valCommentL);
            string comment = driver.FindElement(valCommentL).Text;
            Thread.Sleep(4000);
            return comment;
        }

        //Get creator of added Counterparty Comment
        public string GetCreatorOfAddedCommentL()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valOppCreator);
            string creator = driver.FindElement(valOppCreator).Text;
            Thread.Sleep(4000);
            return creator;
        }

        //Click on Add Counterparties and validate the page
        public string ClickAddCounterpartiesAndValidatePage()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterpartiesL, 250);
            driver.FindElement(btnAddCounterpartiesL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCounterpartyL, 250);
            driver.FindElement(btnAddCounterpartyL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblNewOppCounterparty, 250);
            string title = driver.FindElement(lblNewOppCounterparty).Text;
            return title;
        }

        //Add Counterparty 
        public void AddCounterpartyInOpportunityL(string name, string type)
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtCompanyL, 250);
            driver.FindElement(txtCompanyL).SendKeys(name);
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("//lightning-base-combobox-item/span[2]/span[1]/lightning-base-combobox-formatted-text")).Click();

            //Select Type            
            driver.FindElement(comboType).SendKeys(type);
            driver.FindElement(By.XPath("//lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span[text()='" + type + "']")).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnSaveCounterpartyL).Click();
        }

        //Add Counterparty Contact
        public void AddCounterpartyContactInOpportunityL()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewOppCounterpartyContactL, 250);
            driver.FindElement(btnNewOppCounterpartyContactL).Click();
            Thread.Sleep(6000);

            //Select Name            
            driver.FindElement(chkNameL).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnAddContactL).Click();
            Thread.Sleep(3000);
            driver.FindElement(btnBackL).Click();
        }

        //Add Counterparty Comment
        public void AddCounterpartyCommentL()
        {
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkShowMore, 250);
            driver.FindElement(lnkShowMore).Click();
            Thread.Sleep(3000);
            driver.FindElement(lnkNewOppCounterpartyComment).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtRelatedOppL, 300);
            driver.FindElement(txtRelatedOppL).SendKeys("8K Miles Software Services Limited");
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("//div/div[2]/ul/li/lightning-base-combobox-item/span[1]/lightning-icon/lightning-primitive-icon")).Click();
            Thread.Sleep(3000);
            driver.FindElement(txtCommentsL).SendKeys("Testing");
            driver.FindElement(btnSaveCommentL).Click();
            Thread.Sleep(4000);

        }



        //Click Opportunity Tab
        public void ClickOpportunityTab()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabOppNumL, 250);
            driver.FindElement(tabOppNumL).Click();
        }
    
public Boolean IsCounterpartyButtonsDisplayed(string buttonName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _counterpartyButtons(buttonName), 60);
            return driver.FindElement(_counterpartyButtons(buttonName)).Displayed;
        }

        public void ButtonClickAddCounterparties()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _counterpartyButtons("Add Counterparties"), 60);
            driver.FindElement(_counterpartyButtons("Add Counterparties")).Click();
            Thread.Sleep(4000);
        }

        public bool IsExpectedFilterNameavailable(string expectedSection)
        {
            System.Collections.Generic.IList<IWebElement> filtersSection = driver.FindElements(fitersSectionsCounterparties);
            bool isFilternameAvaialable = false;
            for (int count = 0; count < filtersSection.Count; count++)
            {
                string section = filtersSection[count].Text;
                if (expectedSection.Equals(section))
                {
                    isFilternameAvaialable = true;
                    break;
                }
            }
            return isFilternameAvaialable;
        }

        public void SearchCounterparties(string filterSection, string subFilter, string value)
        {
            IList<IWebElement> filtersSection = driver.FindElements(fitersSectionsCounterparties);
            IWebElement subFilterOption = driver.FindElement(_subFilterEle(subFilter));
            for (int count = 0; count < filtersSection.Count; count++)
            {
                filtersSection[count].Click();
                string section = filtersSection[count].Text;
                if (filterSection.Equals(section))
                {
                    if (subFilterOption.Displayed)
                        subFilterOption.Click();
                    subFilterOption.SendKeys(value);
                    Thread.Sleep(2000);
                    driver.FindElement(optionCompanyListElement).Click();
                    break;
                }
            }
            Thread.Sleep(2000);
            driver.FindElement(buttonSearch).Click();
        }
       
public bool ValidateCompanyListHyperlinked()
        {
            Thread.Sleep(2000);
            IList<IWebElement> hyperlinkes = driver.FindElements(hyperlinkedCompanies);
            bool hyperlinkFound = true;
            for (int index = 0; index < hyperlinkes.Count; index++)
            {
                if (!(hyperlinkes[index].Displayed))
                {
                    hyperlinkFound = false;
                    break;
                }
            }
            return hyperlinkFound;
        }

        public int GetCounterpartiesFiltersCount()
        {
            driver.FindElement(tabCounterparty).Click();
            Thread.Sleep(4000);
            return driver.FindElements(fitersSectionsCounterparties).Count;
        }
       
        public bool IsExpectedSubfilterAvailable(string filterSection, string subFilter)
        {
            System.Collections.Generic.IList<IWebElement> filtersSection = driver.FindElements(fitersSectionsCounterparties);
            IWebElement subFilterOption = driver.FindElement(_subFilterEle(subFilter));
            bool isSubFilternameAvaialable = false;
            for (int count = 0; count < filtersSection.Count; count++)
            {
                filtersSection[count].Click();
                string section = filtersSection[count].Text;
                if (filterSection.Equals(section))
                {
                    if (subFilterOption.Displayed)
                        isSubFilternameAvaialable = true;
                    break;
                }
            }
            return isSubFilternameAvaialable;
        }
        public int VerifyTabCountOnClickCompanyLink()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkCompany, 30);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].click();", driver.FindElement(linkCompany));
            Thread.Sleep(5000);
            return driver.WindowHandles.Count;
        }



        public void SwitchBackToPreviousTab()
        {
            driver.SwitchTo().Window(driver.WindowHandles[1]);
            Thread.Sleep(2000);
            driver.Close();
            driver.SwitchTo().Window(driver.WindowHandles[0]);
        }

        public string GetCompanyNameFromList()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, companyFromCompanyList, 30);
            Thread.Sleep(2000);
            return driver.FindElement(companyFromCompanyList).Text;
        }

        public void CheckBoxSelectRecord()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkBoxSelectItem, 30);
            driver.FindElement(checkBoxSelectItem).Click();
        }



        public void ClickAddCompanyToCounterparty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, buttonAddCunterpartyToOpportunity, 30);
            driver.FindElement(buttonAddCunterpartyToOpportunity).Click();
            Thread.Sleep(2000);
        }
        public string GetLVMessagePopup()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgLVPopup, 30);
            return driver.FindElement(msgLVPopup).Text;
        }
        public void ButtonClick(string buttonName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, _counterpartyButtons(buttonName), 30);
            driver.FindElement(_counterpartyButtons(buttonName)).Click();
            Thread.Sleep(5000);
        }
        public bool VerifyUserIsOnCounterpartiesListPage()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, listViewCounterparties, 60);
            return driver.FindElement(listViewCounterparties).Displayed;
        }
        public bool IsCompanyInCounterpartyList(string companyName)
        {
            return driver.FindElement(_counterpartyCompanyEle(companyName)).Displayed;
        }
        public void ClickCheckboxCounterpartyCompany(string companyName)
        {
            driver.FindElement(_counterpartyCompanyEle(companyName)).FindElement(By.XPath("//ancestor::td//div/lightning-input[contains(@class,'rowCheckbox')]//label/span[@class='slds-checkbox_faux']")).Click();
            Thread.Sleep(2000);
        }
        public void ButtonConfirmDeleteCounterparty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, buttonConfirmDelete, 30);
            driver.FindElement(buttonConfirmDelete).Click();
            Thread.Sleep(4000);
        }

        public void AddNewOpportunityCounterparty(string companyName, string value)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, searchCompany, 30);
            IWebElement companySearch = driver.FindElement(searchCompany);
            companySearch.Click();
            companySearch.SendKeys(companyName);
            WebDriverWaits.WaitUntilEleVisible(driver, comboResultCompany, 10);
            driver.FindElement(comboResultCompany).Click();
            Thread.Sleep(2000);
            driver.FindElement(comboTypeCounterparty).Click();
            //Thread.Sleep(2000);
            driver.FindElement(_comboTypeCounterpartyOptionEle(value)).Click();
            Thread.Sleep(2000);
            driver.FindElement(buttonSaveCounterparty).Click();
            Thread.Sleep(5000);
        }
        public void CloseCurrentTab(string tabText)
        {
            Thread.Sleep(4000);
            IWebElement closeTabIcon = driver.FindElement(_closeCurrentTabEle("Close OC - " + tabText));
            closeTabIcon.Click();
            Thread.Sleep(2000);
        }
    }
}


