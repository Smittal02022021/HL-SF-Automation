using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_2_TMTT0048664_VerifyWaiveRFOFieldInExistingOpportunityEngagement:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTC0036135 = "LV_TMTT0048664_VerifyWaiveRFOFieldInOpportunityEngagementCF";
        private bool isWaiveRFOFieldEditable = false;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0119861	Verify that "Waive RFO" field is added in "Fees and Financials" tab-> "Estimated Fees" section in an existing Opportunity page. 
        //TMTI0119883 Verify that Financial user cannot edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in an existing Opportunity page.
        //TMTI0119871 Verify that CAO can edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in an existing Opportunity page. 
        //TMTI0119878 Verify that System Admin can edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in an existing Opportunity page
        //TMTI0119899 Verify that Financial user cannot edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in an existing  Engagement
        //TMTI0119894 Verify that CAO can edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in existing Engagement. 
        //TMTI0119897 Verify that System Admin can edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in an existing  Engagement
        //TMTI0119909 Verify that the "Waive RFO" field value is not added in "Fees and Financials"section for FR and FVA LoBs
        

        [Test]
        public void VerifyWaiveRFOFieldInCFExistingOpportunityEngagement()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0036135;
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
                
                int rowOpp = ReadExcelData.GetRowCount(excelPath, "ExistingOpp");
                for (int row = 2; row <= rowOpp; row++)
                {
                    string stdUser = ReadExcelData.ReadData(excelPath, "Users", 1);
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
                    string opportunityName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExistingOpp", row, 2);
                    opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Info", "Opportunity Name: " + opportunityName + " found and selected");

                    //TMTI0119861	Verify that "Waive RFO" field is added in "Fees and Financials" tab-> "Estimated Fees" section in an existing Opportunity page. 

                    opportunityDetails.ClickTabOppFeeAndFincnciaLV();
                    bool isWaivRFODisplayed= randomPages.IsWaiveRFOFieldDisplayedLV();
                    Assert.IsTrue(isWaivRFODisplayed, "Verify that 'Waive RFO' field is added in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Opportunity page for CF Financial User");
                    extentReports.CreateStepLogs("Passed", "'Waive RFO' field is added in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Opportunity page for CF Financial User");

                    //TMTI0119883	Verify that Financial user cannot edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in an existing Opportunity page. 
                    //On Detail Page                    
                    isWaiveRFOFieldEditable = randomPages.IsWaveRFPInlineEditableLV();
                    Assert.IsFalse(isWaiveRFOFieldEditable, "Verify that CF Financial user cannot Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Opportunity page");
                    extentReports.CreateStepLogs("Passed", "CF Financial User cannot Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Opportunity page");
                    opportunityDetails.ClickTabInfoLV();

                    //On Edit page
                    opportunityDetails.ClickEditOpportunityLV();
                    isWaiveRFOFieldEditable = randomPages.IsWaiveRFOFieldEditableLV();
                    Assert.IsFalse(isWaiveRFOFieldEditable, "Verify that Financial user cannot edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Opportunity page");
                    extentReports.CreateStepLogs("Passed", "CF Financial User cannot edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Opportunity page");
                    randomPages.CancelEditFormLV();
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", stdUser + " CF Financial User logged out ");

                    //CAO Login
                    string userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                    homePage.SearchUserByGlobalSearchN(userCAOExl);
                    extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    extentReports.CreateStepLogs("Passed", "User: " + userCAOExl + " logged in on Lightning View");
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");
                    
                    //TMTI0119871 Verify that CAO can edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in an existing Opportunity page. 
                    opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Info", "Opportunity Name: " + opportunityName + " found and selected");

                    //on Detail Page
                    opportunityDetails.ClickTabOppFeeAndFincnciaLV();
                    isWaiveRFOFieldEditable = randomPages.IsWaveRFPInlineEditableLV();
                    Assert.IsTrue(isWaiveRFOFieldEditable, "Verify that CAO user can Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Opportunity page");
                    extentReports.CreateStepLogs("Passed", "CAO User can Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Opportunity page");
                    opportunityDetails.ClickTabInfoLV();

                    //On Edit form 
                    opportunityDetails.ClickEditOpportunityLV();
                    isWaiveRFOFieldEditable = randomPages.IsWaiveRFOFieldEditableLV();
                    Assert.IsTrue(isWaiveRFOFieldEditable, "Verify that CAO can edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Opportunity page");
                    extentReports.CreateStepLogs("Passed", "CAO can edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Opportunity page");
                    randomPages.CancelEditFormLV();
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", userCAOExl + " CAO User logged out ");

                    //System Admin
                    string adminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 4, 1);
                    homePage.SearchUserByGlobalSearchN(adminUser);
                    extentReports.CreateStepLogs("Info", "Admin User: " + adminUser + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "Admin User: " + adminUser + " logged in on Lightning View");
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "Admin User is on " + moduleNameExl + " Page ");

                    //TMTI0119878	Verify that System Admin can edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in an existing Opportunity page
                    //Search for opportunity
                    opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Info", "Opportunity Name: " + opportunityName + " found and selected");
                    //on Detail Page
                    opportunityDetails.ClickTabOppFeeAndFincnciaLV();
                    isWaiveRFOFieldEditable = randomPages.IsWaveRFPInlineEditableLV();
                    Assert.IsTrue(isWaiveRFOFieldEditable, "Verify that Admin user can Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Opportunity page");
                    extentReports.CreateStepLogs("Passed", "Admin User can Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Opportunity page");
                    opportunityDetails.ClickTabInfoLV();

                    //On Edit form 
                    opportunityDetails.ClickEditOpportunityLV();
                    isWaiveRFOFieldEditable = randomPages.IsWaiveRFOFieldEditableLV();
                    Assert.IsTrue(isWaiveRFOFieldEditable, "Verify that Admin can edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Opportunity page");
                    extentReports.CreateStepLogs("Passed", "Admin can edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Opportunity page");
                    randomPages.CancelEditFormLV();
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", adminUser + " System Administrator logged out ");

                }

                int rowEng = ReadExcelData.GetRowCount(excelPath, "ExistingEng");
                for (int row = 2; row <= rowEng; row++)
                {
                    string stdUser = ReadExcelData.ReadData(excelPath, "Users", 1);
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
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");
                    string engagementName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExistingEng", rowEng, 2);
                    engagementHome.GlobalSearchEngagementInLightningView(engagementName);
                    extentReports.CreateStepLogs("Info", "Engagement Name: " + engagementName + " found and selected");

                    engagementDetails.ClickTabEngFeeAndFincnciaLV();
                    //bool isWaivRFODisplayed = randomPages.IsWaiveRFOFieldDisplayedLV();
                    //Assert.IsTrue(isWaivRFODisplayed, "Verify that 'Waive RFO' field is added in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Engagement page for CF Financial User");
                    //extentReports.CreateStepLogs("Passed", "'Waive RFO' field is added in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Engagement page for CF Financial User");

                    //TMTI0119899	Verify that Financial user cannot edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in an existing  Engagement
                    //On Detail Page                    
                    isWaiveRFOFieldEditable = randomPages.IsWaveRFPInlineEditableLV();
                    Assert.IsFalse(isWaiveRFOFieldEditable, "Verify that CF Financial user cannot Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Engagement page");
                    extentReports.CreateStepLogs("Passed", "CF Financial User cannot Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Engagement page");
                    engagementDetails.ClickTabInfoLV();

                    //On Edit page
                    engagementDetails.ClickEditEngagementLV();
                    isWaiveRFOFieldEditable = randomPages.IsWaiveRFOFieldEditableLV();
                    Assert.IsFalse(isWaiveRFOFieldEditable, "Verify that Financial user cannot edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Engagement page");
                    extentReports.CreateStepLogs("Passed", "CF Financial User cannot edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Engagement page");
                    randomPages.CancelEditFormLV();
                    randomPages.CloseActiveTab(engagementName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", stdUser + " CF Financial User logged out ");

                    //CAO Login
                    string userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                    homePage.SearchUserByGlobalSearchN(userCAOExl);
                    extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    extentReports.CreateStepLogs("Passed", "User: " + userCAOExl + " logged in on Lightning View");
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                    //TMTI0119894	Verify that CAO can edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in existing  Engagement. 
                    engagementHome.GlobalSearchEngagementInLightningView(engagementName);
                    extentReports.CreateStepLogs("Info", "Opportunity Name: " + engagementName + " found and selected");

                    //on Detail Page
                    engagementDetails.ClickTabEngFeeAndFincnciaLV();
                    isWaiveRFOFieldEditable = randomPages.IsWaveRFPInlineEditableLV();
                    Assert.IsTrue(isWaiveRFOFieldEditable, "Verify that CAO user can Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Engagement page");
                    extentReports.CreateStepLogs("Passed", "CAO User can Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Engagement page");
                    engagementDetails.ClickTabInfoLV();

                    //On Edit form 
                    engagementDetails.ClickEditEngagementLV();
                    isWaiveRFOFieldEditable = randomPages.IsWaiveRFOFieldEditableLV();
                    Assert.IsTrue(isWaiveRFOFieldEditable, "Verify that CAO can edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Engagement page");
                    extentReports.CreateStepLogs("Passed", "CAO can edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Engagement page");
                    randomPages.CancelEditFormLV();
                    randomPages.CloseActiveTab(engagementName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", userCAOExl + " CAO User logged out ");

                    //System Admin
                    string adminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 4, 1);
                    homePage.SearchUserByGlobalSearchN(adminUser);
                    extentReports.CreateStepLogs("Info", "Admin User: " + adminUser + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "Admin User: " + adminUser + " logged in on Lightning View");
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "Admin User is on " + moduleNameExl + " Page ");

                    //TMTI0119897	Verify that System Admin can edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in an existing  Engagement
                    //Search for opportunity
                    engagementHome.GlobalSearchEngagementInLightningView(engagementName);
                    extentReports.CreateStepLogs("Info", "Opportunity Name: " + engagementName + " found and selected");
                    //on Detail Page
                    engagementDetails.ClickTabEngFeeAndFincnciaLV();
                    isWaiveRFOFieldEditable = randomPages.IsWaveRFPInlineEditableLV();
                    Assert.IsTrue(isWaiveRFOFieldEditable, "Verify that Admin user can Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Engagement page");
                    extentReports.CreateStepLogs("Passed", "Admin User can Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Engagement page");
                    engagementDetails.ClickTabInfoLV();

                    //On Edit form 
                    engagementDetails.ClickEditEngagementLV();
                    isWaiveRFOFieldEditable = randomPages.IsWaiveRFOFieldEditableLV();
                    Assert.IsTrue(isWaiveRFOFieldEditable, "Verify that Admin can edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Engagement page");
                    extentReports.CreateStepLogs("Passed", "Admin can edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in an existing Engagement page");
                    randomPages.CancelEditFormLV();
                    randomPages.CloseActiveTab(engagementName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "System Administrator:"+adminUser + " System Administrator logged out ");

                }

                int rowOppExist = ReadExcelData.GetRowCount(excelPath, "ExistingNonCFOpp");
                for (int row = 2; row <= rowOppExist; row++)
                {
                    string stdUser = ReadExcelData.ReadDataMultipleRows(excelPath, "NonCFUser", row, 1);
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
                    string opportunityName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExistingNonCFOpp", row, 2);
                    string oppLOB = ReadExcelData.ReadDataMultipleRows(excelPath, "ExistingNonCFOpp", row, 3);
                    opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Info", "Opportunity Name: " + opportunityName + " found and selected");

                    //TMTI0119909	Verify that the "Waive RFO" field value is not added in "Fees and Financials"section for FR and FVA LoBs

                    opportunityDetails.ClickTabOppFeeAndFincnciaLV();
                    bool isWaivRFODisplayed = randomPages.IsWaiveRFOFieldDisplayedLV();
                    Assert.IsFalse(isWaivRFODisplayed, "Verify that the 'Waive RFO' field value is not added in 'Fees and Financials' section for LOB: "+ oppLOB);
                    extentReports.CreateStepLogs("Passed", "'Waive RFO' field value is not added in 'Fees and Financials' section for LOB: " + oppLOB);
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "Standard user:" + stdUser + " logged out ");
                }
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                driver.Quit();
            }
        }
    }
}