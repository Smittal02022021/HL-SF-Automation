using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.OpportunitiesCounterparty
{
    class LV_TMTT0027430_TMTT0027433_TMTT0027436_TMTT0027437_TMTT0027438_TMTT0027439_TMTT0030273_TMTT0032167_VerifyViewCounterpartiesButtonAndViewsOnSalesforceLightningVersion  : BaseClass
    {

        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOppCounterparty addCounterparty = new AddOppCounterparty();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();
        RandomPages randomPages = new RandomPages();

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
        private string counterpartyButtonNameExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0063926-Verify that the counterparties list will be viewed on the basis of the Job Type
        //TMTI0063928 Verify the UI placement and role of Add Counterparties button 
        //TMTI0075578 Verify the functionality of adding Counterparty through existing opportunity
        //TMTI0075580 Verify the functionality of adding Counterparty through existing Company list
        // TMTI0075582-Verify the functionality of adding Counterparty through "View All Company List" button
        //TMTI0075584-Verify the funtionality of adding Counterparty through Add Counterparty button
        //TMTI0070802-Verification of Search filter available on View Counterparty screen
        //TMTI0063924 Verify the functionality of View all button on Counterparties Editor Page
        //TMTI0063917 5_Verify the functionality of the Delete button displayed on the Counterparties editor's Page
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
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                int rowUsers = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row<=rowUsers; row++)
                {
                    //Login again as CF Financial User
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    homePage.SearchUserByGlobalSearchN(valUser);
                    extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                    usersLogin.LoginAsSelectedUser();

                    login.SwitchToLightningExperience();
                    string stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");                   

                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName",1);
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    
                    //TMTI0063925-Verify the View Counterparties button is displayed for the Job Types: Buyside, Sellside, Debt and Equity capital market Job Types.
                    int OppRowExl = ReadExcelData.GetRowCount(excelPath, "OpportunityWithJobType");
                    for (int OppRowCount = 2; OppRowCount <= OppRowExl; OppRowCount++)
                    {
                        moduleNameExl = ReadExcelData.ReadData(excelPath, "ModuleName", 1);
                        homePageLV.SelectModule(moduleNameExl);
                        extentReports.CreateStepLogs("Passed", moduleNameExl + " Module is selected on HL Banker ");

                        OppNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OpportunityWithJobType", OppRowCount, 1);
                        OppJobTypeExl= ReadExcelData.ReadDataMultipleRows(excelPath, "OpportunityWithJobType", OppRowCount, 2);
                        opportunityHome.SearchOpportunitiesInLightningView(OppNameExl);

                        string jobTypeOppDetailPage= opportunityDetails.GetOpportunityJobTypeL();
                        Assert.AreEqual(jobTypeOppDetailPage, OppJobTypeExl);
                        extentReports.CreateStepLogs("Passed", "Opportunity with Job type: " + jobTypeOppDetailPage + ", is searched and selected ");
                        
                        Assert.IsTrue(opportunityDetails.IsViewCounterpartyButtonOpportunityPageL());
                        extentReports.CreateStepLogs("Passed", "View Counterparty Button is available for opportunity with with Job type: " + jobTypeOppDetailPage + " ");
                        opportunityDetails.ClickViewCounterpartyButtonOpportunityPageL();

                        //TMTI0063926-Verify that the counterparties list will be viewed on the basis of the Job Type
                        Assert.IsTrue(addCounterparty.IsJobTypeDisplayedOnCounterpartyView(OppJobTypeExl), "Verify that the counterparties list will be viewed on the basis of the Job Type ");
                        extentReports.CreateStepLogs("Passed", "Counterparties View List contains the the Job Type: " + OppJobTypeExl+ " ");

                        randomPages.CloseActiveTab("Counterparty Editor");
                        randomPages.CloseActiveTab(OppNameExl);

                        extentReports.CreateStepLogs("Info", "Selected Company: " + OppNameExl + " tab is closed ");
                        randomPages.ReloadPage();
                    }

                    nameOpportunityExl = ReadExcelData.ReadData(excelPath, "Opportunities", 1);
                    opportunityHome.SearchOpportunitiesInLightningView(nameOpportunityExl);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + nameOpportunityExl + " is searched and selected ");

                    //Verify is View Counterparty Button is Displayed on selected Opportunity Detail page
                    Assert.IsTrue(opportunityDetails.IsViewCounterpartyButtonOpportunityPageL());
                    extentReports.CreateStepLogs("Passed", "View Counterparty Button is displayed on Oppordunity Detail Page");
                    
                    //Verify All available Buttons on View Counterparty Detail page
                    opportunityDetails.ClickViewCounterpartyButtonOpportunityPageL();

                    // buttons changed as discussed with QA
                    int rowButtons = ReadExcelData.GetRowCount(excelPath, "CounterpartiesPageButton");
                    for(int buttonRowCount=2; buttonRowCount<=rowButtons; buttonRowCount++)
                    {
                        
                        string buttonNameExl= ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartiesPageButton", buttonRowCount, 1);
                        Assert.IsTrue(addCounterparty.IsCounterpartyButtonsDisplayed(buttonNameExl), "Verifying the "+ buttonNameExl+ " Button is available on Opportunity Counterparty List Page ");
                        extentReports.CreateStepLogs("Passed", buttonNameExl +": Button is displayed on Opportunity Counterparty List Page ");
                    }

                    //Verify the number of filter section on Add Counterparties page
                    //TMTI0063928 Verify the UI placement and role of Add Counterparties button 
                    addCounterparty.ClickAddCounterpartiesButtonLV();
                    int filterSectionsExl = ReadExcelData.GetRowCount(excelPath, "FilterSections");
                    Assert.AreEqual(filterSectionsExl - 1, addCounterparty.GetCounterpartiesFiltersCount(), "Verifying the number of filter Sections on Add Counterparties page ");
                    extentReports.CreateStepLogs("Passed", filterSectionsExl - 1 + " sectors are available on Add Counterparties page as Expected ");
                    addCounterparty.ButtonClick("Back");
                    extentReports.CreateStepLogs("Info", "Clicked on Back button ");

                    //TMTI0075578 Verify the functionality of adding Counterparty through existing opportunity
                    //TMTI0075580 Verify the functionality of adding Counterparty through existing Comapny list
                    int filtersCount = ReadExcelData.GetRowCount(excelPath, "FilterSections");
                    for (int optionRow = 2; optionRow <= filtersCount; optionRow++)
                    {
                        addCounterparty.ClickAddCounterpartiesButtonLV();
                        filterSection = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", optionRow, 1);
                        subFilterSection = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", optionRow, 2);
                        filterValue = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", optionRow, 3);
                        extentReports.CreateStepLogs("Info", "Verifying the functionality of adding Counterparties Company from " + filterSection + " ");
                        addCounterparty.SelectFilterLV(filterSection, subFilterSection);
                        addCounterparty.SearchCounterpartiesLV(subFilterSection, filterValue);
                        //Get Company name from Company List 
                        selectedCompany = addCounterparty.GetCompanyNameFromListLV();                        
                        // Checkbox of first company
                        addCounterparty.SelectCompanyFromListLV();
                        extentReports.CreateStepLogs("Info", selectedCompany + " : Company selected from Company List ");
                        // Click on Add Counterparty oppname button
                        addCounterparty.ClickAddCounterpartyToOpportunity();

                        //Verify the Success Message
                        popupMessage = addCounterparty.GetLVMessagePopup();
                        Assert.AreEqual(popupMessage, "Selected Counterparty Records have been created.");
                        extentReports.CreateStepLogs("Passed", popupMessage + " message Displayed and company "+ selectedCompany+" is added in counterparty list ");

                        //Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page
                        addCounterparty.ButtonClick("Back");
                        extentReports.CreateStepLogs("Info", "Clicked on Back button ");
                        Assert.IsTrue(addCounterparty.IsCounterpartiesListDisplayed(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                        Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(selectedCompany), "Verify added Company: " + selectedCompany + " is under Counterparties List ");
                        extentReports.CreateStepLogs("Passed", "User return to Counterparties List Page ");
                        extentReports.CreateStepLogs("Passed", selectedCompany + " Company is added and displayed into Counterparties List ");
                    }

                    // TMTI0075582-Verify the functionality of adding Counterparty through "View All Company List" button
                    extentReports.CreateStepLogs("Info", "Verifying the functionality of adding Counterparties Company from View All Company List button ");
                    addCounterparty.ClickAddCounterpartiesButtonLV();
                    filterSection = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", 3, 1);
                    counterpartyButtonNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartiesButton", 3, 1);
                    
                    Assert.IsTrue(addCounterparty.IsButtonDisplayedViewAllCompanyListLV(filterSection, counterpartyButtonNameExl), "Verify View All Company List Button Displayed in filters sections");
                    extentReports.CreateStepLogs("Passed", " View All Company List Button Displayed");
                    
                    addCounterparty.ButtonClick(counterpartyButtonNameExl);
                    addCounterparty.SelectCompanyListCompanyLV();
                    //Get Company name from Company List 
                    selectedCompany = addCounterparty.GetCompanyNameFromListLV();
                    // Checkbox of first company
                    addCounterparty.SelectCompanyFromListLV();
                    extentReports.CreateStepLogs("Info", selectedCompany + " : Company selected from Company List ");
                    addCounterparty.ClickAddCounterpartyToOpportunity();                    
                    //Verify the Success Message
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.AreEqual(popupMessage, "Selected Counterparty Records have been created.");
                    extentReports.CreateStepLogs("Passed", popupMessage + " message Displayed and company " + selectedCompany + " is added in counterparty list ");

                    //Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page
                    addCounterparty.ButtonClick("Back");
                    extentReports.CreateLog("Clicked on Back button ");
                    Assert.IsTrue(addCounterparty.IsCounterpartiesListDisplayed(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                    Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(selectedCompany), "Verify added Company: " + selectedCompany + " is under Counterparties List ");
                    extentReports.CreateStepLogs("Passed", "User return to Counterparties List Page ");
                    extentReports.CreateStepLogs("Passed", selectedCompany + " Company is added and displayed into Counterparties List ");                    

                    //TMTI0075584-Verify the funtionality of adding Counterparty through Add Counterparty button
                    string counterpartyCompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 1);
                    string counterpartyTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 2);

                    addCounterparty.ClickAddCounterpartiesButtonLV();
                    counterpartyButtonNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartiesButton", 2, 1);
                    addCounterparty.ButtonClick(counterpartyButtonNameExl);
                    extentReports.CreateStepLogs("Info", "Verifying the functionality of adding Counterparties Company from Add Counterparty button ");

                    addCounterparty.AddNewOpportunityCounterparty(counterpartyCompanyNameExl, counterpartyTypeExl);
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.IsTrue(popupMessage.Contains(counterpartyCompanyNameExl), "Verify the Added Counterparty name is displayed in Popup message ");
                    extentReports.CreateStepLogs("Passed", popupMessage + " message Displayed and company " + counterpartyCompanyNameExl + " is added in counterparty list ");

                    addCounterparty.CloseOppCounterpartyPage(counterpartyCompanyNameExl);
                    addCounterparty.ButtonClick("Back");
                    extentReports.CreateLog("Clicked on Back button");
                    Assert.IsTrue(addCounterparty.IsCounterpartiesListDisplayed(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                    Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(counterpartyCompanyNameExl), "Verify added Company: "+ counterpartyCompanyNameExl + " is under Counterparties List");
                    extentReports.CreateStepLogs("Passed", "User returned to Counterparties List Page");
                    extentReports.CreateStepLogs("Passed", counterpartyCompanyNameExl + " Company is added and displayed into Counterparties List ");

                    //TMTI0070802-Verification of Search filter available on View Counterparty screen
                    //TMTI0105434	Verify the functionality of search filter available on View Counterparty page
                    addCounterparty.SearchCounterparty(selectedCompany);
                    Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(selectedCompany), "Verify added Company: " + selectedCompany + " is under Counterparties List while searching from Search Box ");
                    extentReports.CreateLog(selectedCompany + " Company is added and displayed into Counterparties List while searching from Search Box ");
                    addCounterparty.ClearSearchbox(); 

                    //TMTI0063924 Verify the functionality of View all button on Counterparties Editor Page
                    //GetCount of Added Counterparties 
                    int countCounterparties = addCounterparty.GetCounterpartiesCountFromCounterpartyEditorPage();
                    addCounterparty.ButtonClick("View All");
                    extentReports.CreateStepLogs("Info", "User Clicked on View All button on Counterparties List Page ");
                    int countOpportunitiesCounterparties= addCounterparty.GetCounterpartiesCountFromOpportunityCounterpartiesPage();
                    Assert.AreEqual(countCounterparties, countOpportunitiesCounterparties);
                    extentReports.CreateStepLogs("Passed", "All: " + countOpportunitiesCounterparties+" Opportunity Counterparties are displayed after click on View All button ");
                    addCounterparty.CloseOpportunityCounterpartiesTab();
              
                    //TMTI0063917 5_Verify the functionality of the Delete button displayed on the Counterparties editor's Page
                    addCounterparty.ClickDeleteCounterpartyButton();
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.AreEqual(popupMessage, "Please select at least one row to delete.", "Verify the Warning Message if Counterparty is not selected while clicking on Delete Button");
                    extentReports.CreateStepLogs("Passed", popupMessage + " : Message Displayed ");

                    addCounterparty.ClickAllCheckboxCounterpartyCompany();
                    addCounterparty.ClickDeleteCounterpartyButton();
                    addCounterparty.ButtonConfirmDeleteCounterparty();//need to change locator on delte popup 
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.AreEqual(popupMessage, "Records deleted successfully", "Verify the Success Message if Counterparty is Deleted ");
                    extentReports.CreateStepLogs("Passed", popupMessage + " : Message Displayed and counterparties is deleted from list ");
                    randomPages.CloseActiveTab(nameOpportunityExl);
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Info", valUser + " logged out ");
                }
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                homePageLV.UserLogoutFromSFLightningView();
                login.SwitchToLightningExperience();
                homePageLV.SelectAppLV(appNameExl);
                homePageLV.SelectModule(moduleNameExl);
                opportunityHome.SearchOpportunitiesInLightningView(nameOpportunityExl);
                opportunityDetails.ClickViewCounterpartyButtonOpportunityPageL();
                addCounterparty.ClickAllCheckboxCounterpartyCompany();
                addCounterparty.ClickDeleteCounterpartyButton();
                addCounterparty.ButtonConfirmDeleteCounterparty();
                extentReports.CreateStepLogs("Info", popupMessage + " : Message Displayed and counterparty: " + selectedCompany + "is deleted from list ");

                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();                
            }
        }        
    }
}
