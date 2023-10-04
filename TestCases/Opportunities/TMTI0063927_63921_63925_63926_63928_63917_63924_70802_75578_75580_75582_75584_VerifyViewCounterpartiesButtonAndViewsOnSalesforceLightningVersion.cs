using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class TMTI0063927_63921_63925_63926_63928_63917_63924_70802_75578_75580_75582_75584_VerifyViewCounterpartiesButtonAndViewsOnSalesforceLightningVersion  : BaseClass
    {

        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOppCounterparty addCounterparty = new AddOppCounterparty();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileTMTI0063927 = "TMTI0063927_VerifyViewCounterpartiesButtonAndViewsOnSalesforceLightningVersion";
        private string selectedCompany;
        private string appNameExl;
        private string appName;
        private string OppNameExl;
        private string OppJobTypeExl;
        private string moduleNameExl;
        private string nameOpportunityExl;
        private string filterSection;
        private string subFilterSection;
        private string filterValue;
        private string popupMessage;

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0063927;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");               
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
                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName",1);
                    homePageLV.SelectApp(appNameExl);
                    appName= homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");
                    //Go to Opportunities Module and Search for opportunity
                    moduleNameExl = ReadExcelData.ReadData(excelPath, "ModuleName", 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog(moduleNameExl + " Module is selected on HL Banker ");

                    //TMTI0063925-Verify the View Counterparties button is displayed for the Job Types: Buyside, Sellside, Debt and Equity capital market Job Types.
                    int OppRowExl = ReadExcelData.GetRowCount(excelPath, "OpportunityWithJobType");
                    for (int OppRowCount = 2; OppRowCount <= OppRowExl; OppRowCount++)
                    {
                        OppNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OpportunityWithJobType", OppRowCount, 1);
                        OppJobTypeExl= ReadExcelData.ReadDataMultipleRows(excelPath, "OpportunityWithJobType", OppRowCount, 2);
                        opportunityHome.SearchOpportunityInLightning(OppNameExl);

                        string jobTypeOppDetailPage= opportunityDetails.GetOpportunityJobTypeL();
                        Assert.AreEqual(jobTypeOppDetailPage, OppJobTypeExl);
                        extentReports.CreateLog("Opportunity with Job type: " + jobTypeOppDetailPage + ", is searched and selected ");
                        
                        Assert.IsTrue(opportunityDetails.IsViewCounterpartyButtonOpportunityPageL());
                        extentReports.CreateLog("View Counterparty Button is available for opportunity with with Job type: " + jobTypeOppDetailPage + " ");
                        opportunityDetails.ClickViewCounterpartyButtonOpportunityPageL();

                        //TMTI0063926-Verify that the counterparties list will be viewed on the basis of the Job Type
                        Assert.IsTrue(addCounterparty.IsJobTypeDisplayedOnCounterpartyView(OppJobTypeExl), "Verify that the counterparties list will be viewed on the basis of the Job Type ");
                        extentReports.CreateLog("Counterparties View List contains the the Job Type: "+ OppJobTypeExl+ " ");

                        opportunityDetails.CloseOpprtunityTabL(OppNameExl);
                        extentReports.CreateLog("Selected Company: " + OppNameExl + " tab is closed ");
                    }

                    nameOpportunityExl = ReadExcelData.ReadData(excelPath, "Opportunities", 1);
                    opportunityHome.SearchOpportunityInLightning(nameOpportunityExl);
                    extentReports.CreateLog("Opportunity : " + nameOpportunityExl + " is searched and selected ");

                    //Verify is View Counterparty Button is Displayed on selected Opportunity Detail page
                    Assert.IsTrue(opportunityDetails.IsViewCounterpartyButtonOpportunityPageL());
                    extentReports.CreateLog("View Counterparty Button is displayed on Oppordunity Detail Page");
                    
                    //Verify All available Buttons on View Counterparty Detail page
                    opportunityDetails.ClickViewCounterpartyButtonOpportunityPageL();

                    int rowButtons = ReadExcelData.GetRowCount(excelPath, "CounterpartiesPageButton");
                    for(int buttonRowCount=2; buttonRowCount<=rowButtons; buttonRowCount++)
                    {
                        string buttonNameExl= ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartiesPageButton", buttonRowCount, 1);
                        Assert.IsTrue(addCounterparty.IsCounterpartyButtonsDisplayed(buttonNameExl), "Verifying the "+ buttonNameExl+ " Button is available on Opportunity Counterparty List Page ");
                        extentReports.CreateLog(buttonNameExl+": Button is displayed on Opportunity Counterparty List Page ");
                    }

                    //Verify the number of filter section on Add Counterparties page
                    //TMTI0063928 Verify the UI placement and role of Add Counterparties button 
                    addCounterparty.ClickAddCounterpartiesButton();
                    int filterSectionsExl = ReadExcelData.GetRowCount(excelPath, "FilterSections");
                    Assert.AreEqual(addCounterparty.GetCounterpartiesFiltersCount(), filterSectionsExl - 1, "Verifying the number of filter Sections on Add Counterparties page ");
                    extentReports.CreateLog(filterSectionsExl - 1 + " sectors are available on Add Counterparties page as Expected ");
                    addCounterparty.ButtonClick("Back");
                    extentReports.CreateLog("Clicked on Back button ");

                    //TMTI0075578 Verify the funtionality of adding Counterparty through existing opportunity
                    //TMTI0075580 Verify the funtionality of adding Counterparty through existing Comapny list

                    int filtersCount = ReadExcelData.GetRowCount(excelPath, "FilterSections");
                    for (int optionRow = 2; optionRow <= filtersCount; optionRow++)
                    {
                        addCounterparty.ClickAddCounterpartiesButton();
                        filterSection = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", optionRow, 1);
                        subFilterSection = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", optionRow, 2);
                        filterValue = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", optionRow, 3);
                        extentReports.CreateLog("Verifying the functionality of adding Counterparties Company from " + filterSection + " ");
                        addCounterparty.SelectFilter(filterSection, subFilterSection);
                        addCounterparty.SearchCounterparties(subFilterSection, filterValue);

                        //Get Company name from Company List 
                        selectedCompany = addCounterparty.GetItemNameFromList();
                        
                        // Checkbox of first company
                        addCounterparty.CheckBoxSelectRecord();
                        extentReports.CreateLog(selectedCompany + " : Company selected from Company List ");
                        // Click on Add Counterparty oppname button
                        addCounterparty.ClickAddCounterpartyToOpportunity();

                        //Verify the Success Message
                        popupMessage = addCounterparty.GetLVMessagePopup();
                        Assert.AreEqual(popupMessage, "Selected Counterparty Records have been created.");
                        extentReports.CreateLog(popupMessage + " message Displayed and company "+ selectedCompany+" is added in counterparty list ");

                        //Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page
                        addCounterparty.ButtonClick("Back");
                        extentReports.CreateLog("Clicked on Back button ");
                        Assert.IsTrue(addCounterparty.VerifyUserIsOnCounterpartiesListPage(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                        Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(selectedCompany), "Verify added Company: " + selectedCompany + " is under Counterparties List ");
                        extentReports.CreateLog("User return to Counterparties List Page ");
                        extentReports.CreateLog(selectedCompany + " Company is added and displayed into Counterparties List ");
                    }

                    //TMTI0075584-Verify the funtionality of adding Counterparty through Add Counterparty button
                    string counterpartyCompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 1);
                    string counterpartyTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 2);

                    addCounterparty.ClickAddCounterpartiesButton();
                    addCounterparty.ButtonClick("Add Counterparty");                    
                    extentReports.CreateLog("Verifying the functionality of adding Counterparties Company from Add Counterparty button ");

                    addCounterparty.AddNewOpportunityCounterparty(counterpartyCompanyNameExl, counterpartyTypeExl);
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.IsTrue(popupMessage.Contains(counterpartyCompanyNameExl), "Verify the Added Counterparty name is displayed in Popup message ");
                    extentReports.CreateLog(popupMessage + " message Displayed and company " + counterpartyCompanyNameExl + " is added in counterparty list ");

                    addCounterparty.CloseCurrentTab(counterpartyCompanyNameExl);
                    addCounterparty.ButtonClick("Back");
                    extentReports.CreateLog("Clicked on Back button");
                    Assert.IsTrue(addCounterparty.VerifyUserIsOnCounterpartiesListPage(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                    Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(counterpartyCompanyNameExl), "Verify added Company: "+ counterpartyCompanyNameExl + " is under Counterparties List");
                    extentReports.CreateLog("User returned to Counterparties List Page");
                    extentReports.CreateLog(counterpartyCompanyNameExl + " Company is added and displayed into Counterparties List ");


                    // TMTI0075582-Verify the funtionality of adding Counterparty through "View All Company List" button
                    extentReports.CreateLog("Verifying the functionality of adding Counterparties Company from View All Company List button ");

                    addCounterparty.ClickAddCounterpartiesButton();
                    addCounterparty.SelectFilter(filterSection, subFilterSection);// parameters values are same as from previous iteration.
                    addCounterparty.ButtonClick("View All Company List");

                    addCounterparty.SelectCompanyFromAllCompanyList(filterValue);
                    extentReports.CreateLog(filterValue + " Company is Selected from Companies List ");
                   // selectedCompany = addCounterparty.GetItemNameFromList();

                    //Get Company name from Company List 
                    selectedCompany = addCounterparty.GetItemNameFromList();

                    // Checkbox of first company
                    addCounterparty.CheckBoxSelectRecord();
                    extentReports.CreateLog(selectedCompany + " : Company selected from Company List ");
                    // Click on Add Counterparty oppname button
                    addCounterparty.ClickAddCounterpartyToOpportunity();
                    //Verify the Success Message
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    extentReports.CreateLog(popupMessage + " message Displayed and company " + selectedCompany + " is added in counterparty list ");

                    addCounterparty.ButtonClick("Back");
                    extentReports.CreateLog("Clicked on Back button ");
                    Assert.IsTrue(addCounterparty.VerifyUserIsOnCounterpartiesListPage(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                    extentReports.CreateLog("User return to Counterparties List Page ");
                    Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(selectedCompany), "Verify added Company: " + selectedCompany + " is under Counterparties List ");
                    extentReports.CreateLog("User return to Counterparties List Page ");
                    extentReports.CreateLog(selectedCompany + " Company is added and displayed into Counterparties List ");

                    //TMTI0070802-Verification of Seacrh filter available on View Counterparty screen

                    addCounterparty.SearchCounterparty(selectedCompany);
                    Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(selectedCompany), "Verify added Company: " + selectedCompany + " is under Counterparties List while searching from Search Box ");
                    extentReports.CreateLog(selectedCompany + " Company is added and displayed into Counterparties List while searching from Search Box ");
                    addCounterparty.ClearSearchbox();


                    //TMTI0063924 Verify the functionality of View all button on Counterparties Editor Page

                    //GetCount of Added Counterparties 
                    int countCounterparties = addCounterparty.GetCounterpartiesCountFromCounterpartyEditorPage();
                    addCounterparty.ButtonClick("View All");
                    extentReports.CreateLog("User Clicked on View All button on Counterparties List Page ");
                    int countOpportunitiesCounterparties= addCounterparty.GetCounterpartiesCountFromOpportunityCounterpartiesPage();
                    Assert.AreEqual(countCounterparties, countOpportunitiesCounterparties);
                    extentReports.CreateLog("All: "+ countOpportunitiesCounterparties+" Opportunity Counterparties are displayed after click on View All button ");
                    addCounterparty.CloseOpportunityCounterpartiesTab();
              

                    //TMTI0063917 5_Verify the functionality of the Delete button displayed on the Counterparties editor's Page
                    //addCounterparty.ClickCheckboxCounterpartyCompany(counterpartyCompanyNameExl);
                    addCounterparty.ClickDeleteCounterpartyButton();
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.AreEqual(popupMessage, "Please select at least one row to delete.", "Verify the Warning Message if Counterparty is not selected while clicking on Delete Button");
                    extentReports.CreateLog(popupMessage + " : Message Displayed ");

                    addCounterparty.ClickAllCheckboxCounterpartyCompany();
                    addCounterparty.ClickDeleteCounterpartyButton();
                    addCounterparty.ButtonConfirmDeleteCounterparty();
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.AreEqual(popupMessage, "Records deleted successfully", "Verify the Success Message if Counterparty is Deleted ");
                    extentReports.CreateLog(popupMessage + " : Message Displayed and counterparties is deleted from list ");              

                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateLog(valUser + " logged out ");
                }
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                homePageLV.UserLogoutFromSFLightningView();
                login.SwitchToLightningExperience();
                homePageLV.ClickAppLauncher();
                homePageLV.SelectApp(appNameExl);
                homePageLV.SelectModule(moduleNameExl);
                opportunityHome.SearchOpportunityInLightning(nameOpportunityExl);
                opportunityDetails.ClickViewCounterpartyButtonOpportunityPageL();
                addCounterparty.ClickAllCheckboxCounterpartyCompany();
                addCounterparty.ClickDeleteCounterpartyButton();
                addCounterparty.ButtonConfirmDeleteCounterparty();
                ///popupMessage = addCounterparty.GetLVMessagePopup();
                //Assert.AreEqual(popupMessage, "Records deleted successfully", "Verify the Success Message if Counterparty is Deleted ");
                extentReports.CreateLog(popupMessage + " : Message Displayed and counterparty: " + selectedCompany + "is deleted from list ");

                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
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
