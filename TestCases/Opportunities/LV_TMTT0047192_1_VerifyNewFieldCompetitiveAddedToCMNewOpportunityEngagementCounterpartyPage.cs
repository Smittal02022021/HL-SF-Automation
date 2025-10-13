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

namespace SF_Automation.TestCases.OpportunitiesConversion
{
    class LV_TMTT0047192_1_VerifyNewFieldCompetitiveAddedToCMNewOpportunityEngagementCounterpartyPage:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementHomePage engagementHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        AddOppCounterparty addCounterparty = new AddOppCounterparty();
        LVHomePage homePageLV = new LVHomePage();        
        HomeMainPage homePage = new HomeMainPage();
        RandomPages randomPages = new RandomPages();

        public static string TMTI0063910 = "LV_TMTT0047192_VerifyNewFieldCompetitiveAddedToCMNewOpportunityEngagementCounterpartyPage";
        private string popupMessage;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0115605 Verify that new checkbox field with field name "Competitive" is seen in Opportunity Counterparty in an opportunity.
        //TMTI0115468 Verify that new checkbox field with name "Competitive" is dislaying in Opportunity Counterparty printable view page.
        //TMTI0115470 Verify that "Competitive" checkbox value set in Opportunity Counterparties page is seen mapped on converting an opportunity to engagement.
        //TMTI0115611 Verify that new checkbox field with field name "Competitive" is seen in Engagement Counterparty on converting an opportunity to an Engagement and in Engagement Counterparty printable view page.
        //TMTI0115619 Verify that new checkbox "Competitive" field is seen in Engagement Counterparty on accessing from Counterparty dashboard for an existing Engagement having counterparties added.
        

        [Test]
        public void VerifyNewFieldCompetitiveAddedToCMNewOpportunityEngagementCounterpartyPageLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + TMTI0063910;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowOpp = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                for (int row = 2; row <= rowOpp; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    //Login as Standard User profile and validate the user
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "StandardUsers", row, 1);
                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                    homePage.SearchUserByGlobalSearchN(valUser);
                    extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");

                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    //Validating Title of New Opportunity Page
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectCFOpp();
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                    extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, TMTI0063910);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(TMTI0063910);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Passed", "User is on Opportunity detail " + displayedTab + " tab ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                    extentReports.CreateStepLogs("Passed", "Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact      
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    addOpportunityContact.CickAddCFOpportunityContact();
                    addOpportunityContact.CreateContactL2(TMTI0063910, valRecordType);
                    extentReports.CreateStepLogs("Passed", valContactType + " is added as " + valContactType + "opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForCFConversionLV2(TMTI0063910, valJobType);
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(TMTI0063910);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityL();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab("Internal Team");
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", valUser + " Standard User logged out ");

                    //System Admin Performin required actions
                    string adminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 3, 1);
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
                    //Search for created opportunity
                    opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Info", "Admin is Performing Required Actions ");
                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeNBCApproveDetailsLV(valJobType);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", adminUser + " System Administrator logged out ");


                    //Login as CF FIn user to request opp to convert into eng.
                    homePage.SearchUserByGlobalSearchN(valUser);
                    extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");

                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);

                    opportunityDetails.ClickViewCounterpartyButtonOpportunityPageL();
                    string counterpartyCompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 1);
                    string counterpartyTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 2);

                    extentReports.CreateStepLogs("Info", "Adding Counterparty through Add Counterparty button");
                    addCounterparty.ClickAddCounterpartiesButtonLV();
                    string counterpartyButtonNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartiesButton", 2, 1);
                    addCounterparty.ButtonClick(counterpartyButtonNameExl);
                    extentReports.CreateStepLogs("Info", "Verifying the functionality of adding Counterparties Company from Add Counterparty button ");

                    //TMTI0115605	Verify that new checkbox field with field name "Competitive" is seen in Opportunity Counterparty in an opportunity.
                    //6. Verify the "Competitive" field displayed under section "Information" section.
                    Assert.IsTrue(addCounterparty.IsCompetitiveCheckboxDisplayedLV(), "Verify that new checkbox field with field name 'Competitive' is seen in Opportunity Counterparty in an opportunity.");
                    extentReports.CreateStepLogs("Passed", "New checkbox field with field name 'Competitive' is seen in Opportunity Counterparty in an opportunity.");

                    addCounterparty.UpdateCompetitiveCheckboxLV();
                    string chkboxStatus= addCounterparty.GetCompetitiveCheckboxStatusLV();
                    extentReports.CreateStepLogs("Info", "new checkbox field with field name 'Competitive' is "+ chkboxStatus);

                    addCounterparty.AddNewOpportunityCounterparty(counterpartyCompanyNameExl, counterpartyTypeExl);
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.IsTrue(popupMessage.Contains(counterpartyCompanyNameExl), "Verify the Added Counterparty name is displayed in Popup message ");
                    extentReports.CreateStepLogs("Passed", popupMessage + " message Displayed and company " + counterpartyCompanyNameExl + " is added in counterparty list ");

                    Assert.IsTrue(addCounterparty.IsCPDetailpageCompetitiveCheckboxDisplayedLV(), "Verify that new checkbox field with field name 'Competitive' is seen in Opportunity Counterparty in an opportunity. ");
                    extentReports.CreateStepLogs("Passed", "New checkbox field with field name 'Competitive' Displayed on Opportunity Counterparty Detail page");

                    //TMTI0115468	Verify that new checkbox field with name "Competitive" is dislaying in Opportunity Counterparty Printable View page.

                    counterpartyButtonNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartiesButton", 4, 1);
                    addCounterparty.ButtonClick(counterpartyButtonNameExl);
                    //get the status of checkbox from PV
                    Assert.IsTrue(addCounterparty.IsPVCompetitiveCheckBoxDisplayedLV(), "Verify that new checkbox field with name 'Competitive' is dislaying in Opportunity Counterparty Printable View page");
                    extentReports.CreateStepLogs("Passed", "New checkbox field with field name 'Competitive' Displayed on Opportunity Counterparty Printable View page");

                    Assert.AreEqual(addCounterparty.GetPVCompetitiveCheckBoxStatusLV(), chkboxStatus);
                    extentReports.CreateStepLogs("Passed", "New checkbox field with field name 'Competitive' Status is '"+ chkboxStatus+"' same as saved while adding new counterparty");
                    driver.Close();
                    CustomFunctions.SwitchToWindow(driver, 0);

                    addCounterparty.CloseOppCounterpartyPage(counterpartyCompanyNameExl);
                    addCounterparty.ButtonClick("Back");
                    extentReports.CreateStepLogs("Info", "Clicked on Back button");
                    randomPages.CloseActiveTab("Counterparty Editor");

                    //Requesting for engagement and validate the success message
                    opportunityDetails.ClickRequestToEngL();

                    //Submit Request To Engagement Conversion 
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", valUser + " Standard User logged out ");
                    
                    
                    //Login as CAO user to approve the Opportunity
                    string userCAO = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 2, 1);
                    homePage.SearchUserByGlobalSearchN(userCAO);
                    extentReports.CreateStepLogs("Info", "CAO User: " + userCAO + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(userCAO), true);
                    extentReports.CreateStepLogs("Passed", "CAO User: " + userCAO + " logged in on Lightning View");

                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                    //Approve the Opportunity 
                    string status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Passed", "Opportunity is " + status + " by CAO User ");
                    opportunityDetails.CloseApprovalHistoryTabL();

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEngagementL();
                    extentReports.CreateStepLogs("Info", "Opportunity Converted into Engagement ");
                    //Validate the Engagement name in Engagement details page
                    string engagementNumber = engagementDetails.GetEngagementNumberL();
                    string engagementName = engagementDetails.GetEngagementNameL();
                    Assert.AreEqual(opportunityName, engagementName);
                    extentReports.CreateStepLogs("Passed", "Name of Engagement : " + engagementName + " is Same as Opportunity name ");
                    randomPages.CloseActiveTab(opportunityName);
                    
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    engagementHome.GlobalSearchEngagementInLightningView(engagementName);
                    extentReports.CreateStepLogs("Info", "Engagement Name: " + engagementName + " found and selected");

                    //TMTI0115470	Verify that "Competitive" checkbox value set in Opportunity Counterparties page is seen mapped on converting an opportunity to engagement.
                    engagementDetails.ClickViewCounterpartyButtonEngagementPageL();                    
                    addCounterparty.ClickCounterpartyCompanyLink(counterpartyCompanyNameExl);
                    CustomFunctions.SwitchToWindow(driver, 1);
                    extentReports.CreateLog("User Clicked on Company name from Counterparties List and switched to New Tab ");
                    Assert.IsTrue(addCounterparty.IsCPDetailpageCompetitiveCheckboxDisplayedLV(), "Verify that 'Competitive' checkbox value set in Opportunity Counterparties page is seen mapped on converting an opportunity to engagement. ");
                    extentReports.CreateStepLogs("Passed", "New checkbox field with field name 'Competitive' Mapped on converted Engagement Counterparty Detail page");

                    //TMTI0115611	Verify that new checkbox field with field name "Competitive" is seen in Engagement Counterparty on converting an opportunity to an Engagement and in Engagement Counterparty printable view page. 
                    addCounterparty.ButtonClick(counterpartyButtonNameExl);
                    driver.Close();
                    driver.SwitchTo().Window(driver.WindowHandles.Last());
                    Assert.IsTrue(addCounterparty.IsPVCompetitiveCheckBoxDisplayedLV(), "Verify that new checkbox field with name 'Competitive' is dislaying in Opportunity Counterparty Printable View page");
                    extentReports.CreateStepLogs("Passed", "New checkbox field with field name 'Competitive' Displayed on Opportunity Counterparty Printable View page");

                    Assert.AreEqual(addCounterparty.GetPVCompetitiveCheckBoxStatusLV(), chkboxStatus);
                    extentReports.CreateStepLogs("Passed", "New checkbox field with field name 'Competitive' Status is '" + chkboxStatus + "' same as saved while adding new counterparty");
                    driver.Close();
                    CustomFunctions.SwitchToWindow(driver, 0);
                    randomPages.CloseActiveTab("Counterparty Editor");
                    randomPages.CloseActiveTab(engagementName);

                    //TMTI0115619	Verify that new checkbox "Competitive" field is seen in Engagement Counterparty on accessing from Counterparty dashboard for an existing Engagement having counterparties added. 

                    engagementDetails.ClickCounterpartyDashboardLV();
                    engagementDetails.ClickCPDashboardCounterpartyLV(counterpartyCompanyNameExl);                    
                    extentReports.CreateLog("User Clicked on Engagement Dashboard tab ");
                    Assert.IsTrue(addCounterparty.IsCPDetailpageCompetitiveCheckboxDisplayedLV(), "Verify that new checkbox 'Competitive' field is seen in Engagement Counterparty on accessing from Counterparty dashboard for an Engagement Page");
                    extentReports.CreateStepLogs("Passed", "New checkbox 'Competitive' field is seen in Engagement Counterparty on accessing from Counterparty dashboard for an Engagement Page");
                    driver.Close();
                    CustomFunctions.SwitchToWindow(driver, 0);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "CAO User: "+ userCAO + " logged out ");
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Browser Closed Succesfully!");
                }
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
