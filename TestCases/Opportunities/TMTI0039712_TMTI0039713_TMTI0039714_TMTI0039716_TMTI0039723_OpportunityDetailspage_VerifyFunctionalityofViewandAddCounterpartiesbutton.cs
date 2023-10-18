using AventStack.ExtentReports.Gherkin.Model;
using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunity
{
    class TMTI0039712_TMTI0039713_TMTI0039714_TMTI0039716_TMTI0039723_OpportunityDetailspage_VerifyFunctionalityofViewandAddCounterpartiesbutton  : BaseClass
    {

        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOppCounterparty addCounterparty = new AddOppCounterparty();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileTMTI0035056 = "TMTI0039712_OpportunityDetailspage_VerifyFunctionalityofViewandAddCounterpartiesbutton";
        private string selectedCompany;
        private string msgSuccess;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void LightniningCounterparties()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0035056;
                Console.WriteLine(excelPath);
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                // Calling Login function                
                login.LoginApplication();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                int rowUsers = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row<=rowUsers; row++)
                {
                    //Login again as CF Financial User
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    usersLogin.SearchCFUserAndLogin(valUser);
                    string cfUser = login.ValidateUser();
                    Assert.AreEqual(cfUser.Contains(valUser), true);
                    extentReports.CreateLog("User: " + cfUser + " logged in ");

                    //Switching to LightningView
                    login.SwitchToLightningExperience();
                    homePageLV.ClickAppLauncher();
                    //homePageLV.SelectHLBankerApp("Hl Banker");
                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName",1);
                    homePageLV.SelectApp(appNameExl);
                    string appName= homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");
                    //Go to Opportunities Module and Search for opportunity
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", row, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    string oppNumber = ReadExcelData.ReadDataMultipleRows(excelPath, "Opportunities", row, 1);
                    opportunityHome.SelectOpportunity(oppNumber);
                    //string opportunityName= opportunityDetails.GetOppurtunityName();

                    //Verify is View Counterparty Button is Displayed on selected Opportunity Detail page
                    Assert.IsTrue(opportunityDetails.IsViewCounterpartyButton());
                    extentReports.CreateLog("View Counterparty Button is displayed on Oppordunity Detail Page");
                    //Verify All available Buttons on View Counterparty Detail page
                    opportunityDetails.ClickOnViewCounterpartyButton();                    
                    Assert.IsTrue(addCounterparty.IsCounterpartyButtonsDisplayed("Save"),"Verifying the Save Button");
                    extentReports.CreateLog("Save Button is displayed on Oppordunity Detail Page");
                    Assert.IsTrue(addCounterparty.IsCounterpartyButtonsDisplayed("Delete"), "Verifying the Delete Button");
                    extentReports.CreateLog("Delete Button is displayed on Oppordunity Detail Page");
                    Assert.IsTrue(addCounterparty.IsCounterpartyButtonsDisplayed("Cancel"), "Verifying the Cancel Button");
                    extentReports.CreateLog("Cancel Button is displayed on Oppordunity Detail Page");
                    Assert.IsTrue(addCounterparty.IsCounterpartyButtonsDisplayed("Add Counterparties"), "Verifying the Add Counterparties Button");
                    extentReports.CreateLog("Add Counterparties Button is displayed on Oppordunity Detail Page");
                    Assert.IsTrue(addCounterparty.IsCounterpartyButtonsDisplayed("Import with Dataloader"), "Verifying the Import with Dataloader Button");
                    extentReports.CreateLog("Import with Dataloader Button is displayed on Oppordunity Detail Page");
                    Assert.IsTrue(addCounterparty.IsCounterpartyButtonsDisplayed("Email"), "Verifying the Email Button");
                    extentReports.CreateLog("Email Button is displayed on Oppordunity Detail Page");
                    Assert.IsTrue(addCounterparty.IsCounterpartyButtonsDisplayed("View All"), "Verifying the View All Button");
                    extentReports.CreateLog("View All Button is displayed on Oppordunity Detail Page");
                    
                    //Verify the number of filter section on Add Counterparties page
                    addCounterparty.ButtonClickAddCounterparties();
                    int filterSectionsExl = ReadExcelData.GetRowCount(excelPath, "FilterSections");
                    Assert.AreEqual(addCounterparty.GetCounterpartiesFiltersCount(), filterSectionsExl - 1, "Verifying the number of filter Sections on Add Counterparties page ");
                    extentReports.CreateLog(filterSectionsExl - 1 + " sectors are available on Add Counterparties page as Expected ");

                    //Verify the filter section on Add Counterparties page
                    extentReports.CreateLog("Verifying the available filters on Add Counterparties page ");
                    String SectorAvailable11Exl = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", 2, 1);
                    String SectorAvailable12Exl = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", 3, 1);
                    String SectorsOppSubFiltersExl= ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", 2, 2);
                    String SectorsCompanySubFiltersExl = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", 3, 2);
                    Assert.IsTrue(addCounterparty.IsExpectedFilterNameavailable(SectorAvailable11Exl), "Verifying the Names of filter Section on Add Counterparties page ");
                    extentReports.CreateLog(SectorAvailable11Exl + "- Sectors names is Available as expected on Add Counterparties page as Expected ");
                    Assert.IsTrue(addCounterparty.IsExpectedFilterNameavailable(SectorAvailable12Exl), "Verifying the Names of filter Section on Add Counterparties page ");
                    extentReports.CreateLog(SectorAvailable12Exl + "- Sectors names is Available as expected on Add Counterparties page as Expected ");                                      
                    Assert.IsTrue(addCounterparty.IsExpectedSubfilterAvailable(SectorAvailable11Exl, SectorsOppSubFiltersExl));
                    extentReports.CreateLog(SectorsOppSubFiltersExl + "- subfilter is Available as expected on Add Counterparties page as Expected ");
                    Assert.IsTrue(addCounterparty.IsExpectedSubfilterAvailable(SectorAvailable12Exl, SectorsCompanySubFiltersExl));
                    extentReports.CreateLog(SectorsCompanySubFiltersExl + "- subfilter is Available as expected on Add Counterparties page as Expected ");

                    int filtersCount = ReadExcelData.GetRowCount(excelPath, "FilterSections");
                    for (int optionRow = 2; optionRow <= filtersCount; optionRow++)
                    {
                        string filterSection = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", optionRow, 1);
                        string subFilterSection = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", optionRow, 2);
                        string filterValue = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", optionRow, 3);
                        extentReports.CreateLog("Verifying the functionality of adding Counterparties Company from " + filterSection + " ");
                        addCounterparty.SearchCounterparties(filterSection, subFilterSection, filterValue);

                        // Verify Company names have Hyperlinks                         
                        Assert.IsTrue(addCounterparty.ValidateCompanyListHyperlinked(), "Verify all Counterparties under Company List have Hyperlinks ");
                        extentReports.CreateLog("All Counterparties under Companies List have Hyperlink");
                        // Verify Click on hypelink opend new tab with comapny details
                        int tabCount = addCounterparty.VerifyTabCountOnClickCompanyLink();
                        Assert.AreEqual(tabCount, 2, "Verify New Tab is opened if use click on the Company name from Counterparties list");
                        extentReports.CreateLog("Click on Company Name Hyperlink opnes new tab");
                        //Verify New tab is Opened if user click on Compnay Name
                        //Assert.AreEqual(addCounterparty.GetNewTabPageTitle().Contains("Company List Member"), true);
                        // switchback to original tab
                        addCounterparty.SwitchBackToPreviousTab();
                        //GetCompanynamefrom Company List 
                        selectedCompany = addCounterparty.GetCompanyNameFromList();
                        // Checkbox of first company
                        addCounterparty.CheckBoxSelectRecord();
                        extentReports.CreateLog("Company selected from Company List ");
                        // Click on Add Counterparty oppname button
                        addCounterparty.ClickAddCompanyToCounterparty();
                        //Verify the Success Message
                        msgSuccess = addCounterparty.GetLVMessagePopup();
                        Assert.AreEqual(msgSuccess, "Selected Counterparty Records have been created.");
                        extentReports.CreateLog("Success: " + msgSuccess + " message Displayed ");
                        //Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page
                        addCounterparty.ButtonClick("Back");
                        extentReports.CreateLog("Clicked on Back button");
                        Assert.IsTrue(addCounterparty.VerifyUserIsOnCounterpartiesListPage(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                        extentReports.CreateLog("User return to Counterparties List Page");
                        Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(selectedCompany));
                        extentReports.CreateLog(selectedCompany + " Company is added and displayed into Counterparties List ");
                        //Deleted the Added Company  from list
                        addCounterparty.ClickCheckboxCounterpartyCompany(selectedCompany);
                        addCounterparty.ButtonClick("Delete");
                        addCounterparty.ButtonConfirmDeleteCounterparty();
                        extentReports.CreateLog("Delete button is clicked from Confirmation popup and records is deleted ");
                        addCounterparty.ButtonClickAddCounterparties();
                    }

                    extentReports.CreateLog("Verifying the functionality of adding Counterparties from Add Counterparty Button ");
                    String counterpartyCompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 1);
                    String counterpartyTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 2);
                    
                    addCounterparty.ButtonClick("Add Counterparty");
                    addCounterparty.AddNewOpportunityCounterparty(counterpartyCompanyNameExl, counterpartyTypeExl);
                    //msgSuccess = addCounterparty.GetLVMessagePopup();
                    //Assert.AreEqual(msgSuccess, "Selected Counterparty Records have been created.");
                    addCounterparty.CloseCurrentTab(counterpartyCompanyNameExl);
                    addCounterparty.ButtonClick("Back");
                    extentReports.CreateLog("Clicked on Back button");
                    Assert.IsTrue(addCounterparty.VerifyUserIsOnCounterpartiesListPage(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                    extentReports.CreateLog("User return to Counterparties List Page");
                    Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(counterpartyCompanyNameExl));
                    extentReports.CreateLog(counterpartyCompanyNameExl + " Company is added and displayed into Counterparties List ");
                    
                    //Adding Contact with email id in added Counterparty

                    //String counterpartyContactNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyContact", 2, 1);
                    //addCounterparty.ClickDetailsLink(counterpartyCompanyNameExl);
                    //addCounterparty.ButtonClick("New Opportunity Counterparty Contact");
                    //string contactNameResult= addCounterparty.GetContactSearched("Name", counterpartyContactNameExl);
                    //string ConpanyNameResult= addCounterparty.GetContactSearched("Company", "8K Miles");
                    //string cindustryNameResult= addCounterparty.GetContactSearched("Industry/Product Focus", "FIG");

                    //addCounterparty.CheckBoxSelectRecord();
                    //addCounterparty.ButtonClick("Add Contact");
                    //msgSuccess = addCounterparty.GetLVMessagePopup();
                    //Assert.AreEqual(msgSuccess, "Counterparty Contact(s) were created successfully");
                    //extentReports.CreateLog("New Opportunity Counterparty Contact is added ");
                    ////addCounterparty.ButtonClick("Back");
                    //addCounterparty.CloseCurrentContactTab("Close Opportunity Counterparty Contact");
                    //addCounterparty.CloseCurrentTab(counterpartyCompanyNameExl);
                    //CustomFunctions.pageReload();

                    //Assert.IsTrue(addCounterparty.IsContactAddedCounterpartyList(counterpartyCompanyNameExl),"Verify Contact is added under Company name in Counterparty Companies List");
                    //extentReports.CreateLog("Added Contact is available under Counterparty Record List ");

                    //---------------------------
                    ////Select the Counterparty and verify the functionality of Email Button 
                    //addCounterparty.ClickCheckboxCounterpartyCompany(selectedCompany);
                    //addCounterparty.ButtonClick("Email");
                    //Assert.AreEqual(addCounterparty.GetConfirmPopupHeader(), "Confirm emails", "Verifying the Email Confirmation Pop-Up");
                    //Assert.IsTrue(addCounterparty.ValidatePopupDropdownLabel("Milestone"), "Verifying the dropdown is available on Confirmation Pop-Up");
                    //Assert.IsTrue(addCounterparty.ValidatePopupDropdownLabel("Template"), "Verifying the dropdown is available on Confirmation Pop - Up");
                    //extentReports.CreateLog("Dropdown Labels are Validated on Confirm Email Popup ");
                    //addCounterparty.CloseConfirmPopup();
                    //extentReports.CreateLog("Confirm Email Popup is closed");

                    //Verify the Delete Button Functionality on Counterparties List
                    //addCounterparty.ClickCheckboxCounterpartyCompany(counterpartyCompanyNameExl);
                    addCounterparty.ButtonClick("Delete");
                    string msgWarning = addCounterparty.GetLVMessagePopup();
                    Assert.AreEqual(msgWarning, "Please select at least one row to delete.", "Verify the Warning Message if Counterparty is not selected while clicking on Delete Button");
                    extentReports.CreateLog("Warning: " + msgWarning + " message Displayed ");
                    addCounterparty.ClickCheckboxCounterpartyCompany(counterpartyCompanyNameExl);
                    addCounterparty.ButtonClick("Delete");
                    addCounterparty.ButtonConfirmDeleteCounterparty();
                    extentReports.CreateLog("Delete button is clicked from Confirmation popup and records is deleted ");
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateLog(cfUser + " logged out ");
                }
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                homePageLV.UserLogoutFromSFLightningView();
            }
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            usersLogin.UserLogOut();
            driver.Quit();
        }
    }
}
