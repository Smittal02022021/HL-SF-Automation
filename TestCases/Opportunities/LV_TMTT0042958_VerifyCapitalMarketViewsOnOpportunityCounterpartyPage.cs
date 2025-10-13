using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SF_Automation.TestCases.OpportunitiesCounterparty
{
    class LV_TMTT0042958_VerifyCapitalMarketViewsOnOpportunityCounterpartyPage : BaseClass
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
        public static string fileTMTT0042958 = "LV_TMTT0042958_VerifyCapitalMarketViewsOnOpportunityCounterpartyPage";
        private string selectedCompany;
        private string appNameExl;
        private string appName;
        private string OppNameExl;
        private string OppJobTypeExl;
        private string moduleNameExl;
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
        public void CapitalMarketViewsOnOpportunityCounterpartyLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0042958;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();

                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");

                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                int OppRowExl = ReadExcelData.GetRowCount(excelPath, "OpportunityWithJobType");
                for (int OppRowCount = 2; OppRowCount <= OppRowExl; OppRowCount++)
                {
                    moduleNameExl = ReadExcelData.ReadData(excelPath, "ModuleName", 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", moduleNameExl + " Module is selected on HL Banker ");

                    OppNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OpportunityWithJobType", OppRowCount, 1);
                    OppJobTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OpportunityWithJobType", OppRowCount, 2);
                    opportunityHome.SearchOpportunitiesInLightningView(OppNameExl);
                    extentReports.CreateStepLogs("Passed", "Opportunity: "+OppNameExl + " found and selected");

                    string jobTypeOppDetailPage = opportunityDetails.GetOpportunityJobTypeL();
                    Assert.AreEqual(jobTypeOppDetailPage, OppJobTypeExl);
                    extentReports.CreateStepLogs("Passed", "Opportunity with Job type: " + jobTypeOppDetailPage + ", is searched and selected ");

                    Assert.IsTrue(opportunityDetails.IsViewCounterpartyButtonOpportunityPageL());
                    extentReports.CreateStepLogs("Passed", "View Counterparty Button is available for opportunity with with Job type: " + jobTypeOppDetailPage + " ");
                    opportunityDetails.ClickViewCounterpartyButtonOpportunityPageL();

                    //TMTI0105437	Verify the availability of Capital Market Shortened View on View Counterparty screen for Debt and Equity Capital Market Opportunities
                    Assert.IsTrue(addCounterparty.IsJobTypeViewDisplayedOnCounterpartyView(fileTMTT0042958), "Verify that the counterparties list will be viewed on the basis of the Job Type ");
                    extentReports.CreateStepLogs("Passed", "Counterparties View List contains the updated List View for Job Type: " + OppJobTypeExl);

                    //Adding Multiple CP 
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
                        extentReports.CreateStepLogs("Passed", popupMessage + " message Displayed and company " + selectedCompany + " is added in counterparty list ");

                        //Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page
                        addCounterparty.ButtonClick("Back");
                        extentReports.CreateStepLogs("Info", "Clicked on Back button ");
                        Assert.IsTrue(addCounterparty.IsCounterpartiesListDisplayed(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                        Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(selectedCompany), "Verify added Company: " + selectedCompany + " is under Counterparties List ");
                        extentReports.CreateStepLogs("Passed", "User return to Counterparties List Page ");
                        extentReports.CreateStepLogs("Passed", selectedCompany + " Company is added and displayed into Counterparties List ");
                    }
                    //TMTI0105420	Verify that added Counterparties shows new view columns on View Counterparty screen
                    //Get ColumnName of Default View
                    Assert.IsTrue(addCounterparty.AreCounterpartyListShortenedViewColumnDisplayedLV(fileTMTT0042958), "Verify that added Counterparties shows new columns on View Counterparty screen for Shortened View");
                    extentReports.CreateStepLogs("Passed", "Added Counterparties shows new columns on View Counterparty screen for Shortened View");

                    //TMTI0105427 Verify that existing "Capital Market Stages" view is rename to "Capital Markets Extended View" and no impact on existing functionality
                    // Get Count for default view 
                    int defaultViewCPCount = addCounterparty.GetCounterpartiesCountFromCounterpartyEditorPage();
                    string valCPView = ReadExcelData.ReadDataMultipleRows(excelPath, "CPView", 3, 1);
                    addCounterparty.SelectCounterpartyListViewLV(valCPView);
                    extentReports.CreateStepLogs("Passed", "Counterparty List View changed to "+ valCPView);

                    int updatedViewCPCount = addCounterparty.GetCounterpartiesCountFromCounterpartyEditorPage();
                    Assert.AreEqual(updatedViewCPCount, defaultViewCPCount,"Verify View change is not impacting the available CP in List ");
                    extentReports.CreateStepLogs("Passed", "View change is not impacting the available CP in List ");

                    //Get ColumnName of Extended View
                    Assert.IsTrue(addCounterparty.AreCounterpartyListExtendedViewColumnDisplayedLV(fileTMTT0042958), "Verify that added Counterparties shows new columns on View Counterparty screen for Extended View");
                    extentReports.CreateStepLogs("Passed", "Added Counterparties shows new columns on View Counterparty screen for Extended View");
                    /////////////////////////////////////
                    ///

                    addCounterparty.ClickAllCheckboxCounterpartyCompany();
                    addCounterparty.ClickDeleteCounterpartyButton();
                    addCounterparty.ButtonConfirmDeleteCounterparty();
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.AreEqual(popupMessage, "Records deleted successfully", "Verify the Success Message if Counterparty is Deleted ");
                    extentReports.CreateStepLogs("Passed", popupMessage + " : Message Displayed and counterparties is deleted from list ");

                    randomPages.CloseActiveTab("Counterparty Editor");
                    randomPages.CloseActiveTab(OppNameExl);

                    extentReports.CreateStepLogs("Info", "Selected Company: " + OppNameExl + " tab is closed ");
                    randomPages.ReloadPage();

                }
                homePageLV.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", valUser + " logged out ");
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch(Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                homePageLV.UserLogoutFromSFLightningView();
                login.SwitchToLightningExperience();
                homePageLV.SelectAppLV(appNameExl);
                homePageLV.SelectModule(moduleNameExl);
                opportunityHome.SearchOpportunitiesInLightningView(OppNameExl);
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
