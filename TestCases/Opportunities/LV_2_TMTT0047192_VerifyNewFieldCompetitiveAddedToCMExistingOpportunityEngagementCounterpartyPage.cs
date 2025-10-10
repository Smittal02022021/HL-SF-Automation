using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Linq;

namespace SF_Automation.TestCases.OpportunitiesCounterparty
{
    class LV_2_TMTT0047192_VerifyNewFieldCompetitiveAddedToCMExistingOpportunityEngagementCounterpartyPage: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        AddOppCounterparty addCounterparty = new AddOppCounterparty();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();
        RandomPages randomPages = new RandomPages();

        public static string TMTT0047192 = "LV_TMTT0047192_VerifyNewFieldCompetitiveAddedToCMNewOpportunityEngagementCounterpartyPage";
        private string counterpartyCompanyNameExl;
        private string counterpartyButtonNameExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0115605 Verify that new checkbox field with field name "Competitive" is seen in Opportunity Counterparty in an existing  opportunity.
        //TMTI0115468 Verify that new checkbox field with name "Competitive" is dislaying in existing Opportunity Counterparty printable view page.
        //TMTI0115611 Verify that new checkbox field with field name "Competitive" is seen in Engagement Counterparty on converting an opportunity to an existing Engagement Counterparty printable view page.
        //TMTI0115619 Verify that new checkbox "Competitive" field is seen in Engagement Counterparty on accessing from Counterparty dashboard for an existing Engagement having counterparties added.

        [Test]
        public void VerifyNewFieldCompetitiveAddedToCMExistingOpportunityEngagementCounterpartyPageLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + TMTT0047192;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");
                //Login as Standard User profile and validate the user
                string stdUser = ReadExcelData.ReadData(excelPath, "StandardUsers", 1);
                homePage.SearchUserByGlobalSearchN(stdUser);
                extentReports.CreateStepLogs("Info", "User: " + stdUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(stdUser), true);
                extentReports.CreateStepLogs("Passed", "User: " + stdUser + " logged in on Lightning View");
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateLog("User is on " + moduleNameExl + " Page ");
                int rowOpp = ReadExcelData.GetRowCount(excelPath, "ExistingOpp");
                for (int row = 2; row <= rowOpp; row++)
                {
                    //TMTI0115605	Verify that new checkbox field with field name "Competitive" is seen in Opportunity Counterparty in an Existing opportunity.

                    string opportunityName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExistingOpp", row, 2);
                    opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Info", "Existing Opportunity: " + opportunityName + " found and selected");
                                        
                    opportunityDetails.ClickViewCounterpartyButtonOpportunityPageL();
                    counterpartyCompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 1);
                    addCounterparty.ClickCounterpartyCompanyLink(counterpartyCompanyNameExl);
                    CustomFunctions.SwitchToWindow(driver, 1);
                    extentReports.CreateLog("User Clicked on Company name from Counterparties List and switched to New Tab ");
                    Assert.IsTrue(addCounterparty.IsCPDetailpageCompetitiveCheckboxDisplayedLV(), "Verify that new checkbox field with field name 'Competitive' is seen in Opportunity Counterparty in an Existing opportunity. ");
                    extentReports.CreateStepLogs("Passed", "New checkbox field with field name 'Competitive' is seen in Opportunity Counterparty in an Existing opportunity. ");

                    //TMTI0115468	Verify that new checkbox field with name "Competitive" is dislaying in Opportunity Counterparty printable view page.
                    counterpartyButtonNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartiesButton", 4, 1);
                    addCounterparty.ButtonClick(counterpartyButtonNameExl);
                    driver.Close();
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    Assert.IsTrue(addCounterparty.IsPVCompetitiveCheckBoxDisplayedLV(), "Verify that new checkbox field with name 'Competitive' is dislaying in Opportunity Counterparty Printable View page");
                    extentReports.CreateStepLogs("Passed", "New checkbox field with field name 'Competitive' Displayed on Opportunity Counterparty Printable View page");
                    driver.Close();
                    CustomFunctions.SwitchToWindow(driver, 0);
                    randomPages.CloseActiveTab("Counterparty Editor");
                    randomPages.CloseActiveTab(opportunityName);
                    extentReports.CreateStepLogs("Info", "Opportunity Name: " + opportunityName + " Closed");
                }
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "CF Financial user:" + stdUser + " logged out ");


                string userCAO = ReadExcelData.ReadData(excelPath, "CAOUsers", 1);
                homePage.SearchUserByGlobalSearchN(userCAO);
                extentReports.CreateStepLogs("Info", "User: " + userCAO + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(userCAO), true);
                extentReports.CreateStepLogs("Passed", "User: " + userCAO + " logged in on Lightning View");
                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateLog("User is on " + moduleNameExl + " Page ");                 

                int rowEng = ReadExcelData.GetRowCount(excelPath, "ExistingEng");
                for (int row = 2; row <= rowEng; row++)
                {
                    string engagementName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExistingEng", rowEng, 2);
                    engagementHome.GlobalSearchEngagementInLightningView(engagementName);
                    extentReports.CreateStepLogs("Info", "Existing Engagement: " + engagementName + " found and selected");
                    engagementDetails.ClickViewCounterpartyButtonEngagementPageL();

                    addCounterparty.ClickCounterpartyCompanyLink(counterpartyCompanyNameExl);
                    CustomFunctions.SwitchToWindow(driver, 1);
                    extentReports.CreateLog("User Clicked on Company name from Counterparties List and switched to New Tab ");
                    Assert.IsTrue(addCounterparty.IsCPDetailpageCompetitiveCheckboxDisplayedLV(), "Verify that 'Competitive' checkbox is Added in Existing Engagement Counterparties page. ");
                    extentReports.CreateStepLogs("Passed", "'Competitive' checkbox is Added in Existing Engagement Counterparties page.");

                    //TMTI0115611	Verify that new checkbox field with field name "Competitive" is seen in Engagement Counterparty on converting an opportunity to an Engagement and in Engagement Counterparty printable view page. 
                    addCounterparty.ButtonClick(counterpartyButtonNameExl);
                    driver.Close();
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    Assert.IsTrue(addCounterparty.IsPVCompetitiveCheckBoxDisplayedLV(), "Verify that new checkbox field with name 'Competitive' is Displayed in Existing Engagement Counterparty Printable View page");
                    extentReports.CreateStepLogs("Passed", "New checkbox field with field name 'Competitive' Displayed on Existing Engagement Counterparty Printable View page");
                    driver.Close();
                    CustomFunctions.SwitchToWindow(driver, 0);
                    randomPages.CloseActiveTab("Counterparty Editor");

                    //TMTI0115619	Verify that new checkbox "Competitive" field is seen in Engagement Counterparty on accessing from Counterparty dashboard for an existing Engagement having counterparties added. 
                    engagementDetails.ClickCounterpartyDashboardLV();
                    engagementDetails.ClickCPDashboardCounterpartyLV(counterpartyCompanyNameExl);
                    extentReports.CreateStepLogs("Info", "User Clicked on Engagement Dashboard tab ");
                    Assert.IsTrue(addCounterparty.IsCPDetailpageCompetitiveCheckboxDisplayedLV(), "Verify that new checkbox 'Competitive' field is seen in Engagement Counterparty on accessing from Counterparty dashboard for an Existing Engagement Page");
                    extentReports.CreateStepLogs("Passed", "New checkbox 'Competitive' field is seen in Engagement Counterparty on accessing from Counterparty dashboard for an Existing Engagement Page");
                    driver.Close();
                    CustomFunctions.SwitchToWindow(driver, 0);
                    randomPages.CloseActiveTab(engagementName);
                    randomPages.CloseActiveTab(engagementName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "CAO User: " + userCAO + " logged out ");
                }


                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Succesfully!");

            }


            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
