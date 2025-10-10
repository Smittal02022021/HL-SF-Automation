using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.OpportunitiesDND
{
    class LV_CF_TMTT0049771_5_VerifyMACSDNDOpportunityConvertedIntoVEThanConvertedFEEngagementWithRevenueAccrualAddedOnEngagement:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTT0049771 = "LV_5_CF_TMTT0049771_VerifyMACSDNDOpportunityConvertedIntoVEThanConvertedFEEngagementWithRevenueAccrualAddedOnEngagement";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0123104	Verify that the M&A opportunity is DND approved and is converted into verbally engaged and converted into fully engaged engagement and revenue accrual is added on to the engagement

        [Test]
        public void VerifyMACSOpportunityDNDApprovedAndConvertedIntoEngagementLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0049771;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", login.ValidateUser() + " is able to login ");

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");

                for (int row = 2; row <= rowJobType; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                    extentReports.CreateStepLogs("Info", "Creating Opportunity with Job Type: " + valJobType);
                    //Login as Standard User profile and validate the user
                    string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StandardUsers", row, 1);
                    //Login as Standard User profile and validate the user
                    homePage.SearchUserByGlobalSearchN(userExl);
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(userExl), true);
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " logged in on Lightning View");

                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    //Validating Title of New Opportunity Page
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                    extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileTMTT0049771);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0049771);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Info", "User is on Opportunity detail " + displayedTab + " tab ");

                    //Validating Opportunity details  
                    string oppName = opportunityDetails.GetOpportunityNameL();
                    string oppNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(oppNumber);
                    extentReports.CreateStepLogs("Passed", "Opportunity with number : " + oppNumber + " is created ");

                    //Create External Primary Contact      
                    //string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    //string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                    //string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);

                    //addOpportunityContact.CickAddCFOpportunityContact();
                    //addOpportunityContact.CreateContactL2(fileTMTT0049771, valRecordType);
                    //extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0049771);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    randomPages.CloseActiveTab("Internal Team");
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected");

                    //3. Navigate to the "Oracle ERP" tab and verify the "Product Type", "ERP Product Type Code" field value. 
                    string productType = randomPages.GetERPProductTypeLV();
                    string prodType = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 1);
                    Assert.AreEqual(prodType, productType, "Verify the Product Type in Oracle ERP Information Opportunity details page for the opportunity having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type  " + productType + " in ERP section for Job Type: " + valJobType);

                    string prodTypeCodeERP = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 2);
                    string productTypeCodeERP = randomPages.GetERPProductTypeCodeLV();
                    Assert.AreEqual(prodTypeCodeERP, productTypeCodeERP, "Verify the Product code in Oracle ERP Information Opportunity details page for the opportunity having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type Code: " + productTypeCodeERP + " in ERP section for Job Type: " + valJobType);

                    opportunityDetails.EnterVerballyEngagedRequiredFieldsLV(valJobType, fileTMTT0049771);
                    extentReports.CreateStepLogs("Info", "Entered All Field level Required values for Verbally Engaged Request");

                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "CF Financial User: " + userExl + " Logged out ");

                    //System Admin Performin required actions
                    string adminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 4, 1);
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
                    opportunityDetails.UpdateOutcomeNBCApproveDetailsLV(valJobType);
                    extentReports.CreateStepLogs("Info", "Conflict Check and NBC details are provided");

                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUser + " logged out");


                    //Login as CAO user 
                    string userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", row, 1);
                    homePage.SearchUserByGlobalSearchN(userCAOExl);
                    extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    extentReports.CreateLog("User: " + userCAOExl + " logged in on Lightning View");

                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                    //Validate he DND On/Off buton 
                    bool isButtonDisplayed = opportunityDetails.IsButtonDNDOnOffDisplayedLV();
                    Assert.IsTrue(isButtonDisplayed);
                    extentReports.CreateStepLogs("Pass", "DND On/Off button is displayed for user:  with number : " + userCAOExl);
                    opportunityDetails.ClickDNDOnOffButtonLV();
                    extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " Clicked on DND On/Off Button ");
                    string txtMessage = randomPages.GetLVMessagePopup();
                    extentReports.CreateStepLogs("Pass", txtMessage);
                    randomPages.ReloadPage();
                    //Get DND Status
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Submitted the DND Request");

                    /* /Approval History Details: 
                    DND Approval
                    •	Status: Pending
                    •	Assigned To: DND Approval Q
                    •	Actual Approver: DND Approval Q
                    */

                    string historyStatus = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 8);
                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Status on DND On Requested Opportunity");
                    extentReports.CreateStepLogs("Passed", "CAO User Requested Opportunity DND On Status: " + historyStatus);

                    string historyExpectedAssignTo = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 9);
                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name DND On Requested Opportunity as CAO user");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " DND On Requested Opportunity as CAO user");

                    string actualApprover = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 10);
                    Assert.AreEqual(actualApprover, randomPages.GetHistoryActualApproverLV(), "Verify the Actual Approver Group Name DND On Requested Opportunity as CAO user");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + actualApprover + "DND On Requested Opportunity as CAO user");

                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Pass", "CAO User: " + userCAOExl + " Loggout ");

                    //Login as user from group DND Approval Q
                    string userDNDApproverExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DNDUsers", row, 1);
                    homePage.SearchUserByGlobalSearchN(userDNDApproverExl);
                    extentReports.CreateStepLogs("Info", "DND Approver User: " + userDNDApproverExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string userDND = login.ValidateUserLightningView();
                    Assert.AreEqual(userDND.Contains(userDNDApproverExl), true);
                    extentReports.CreateLog("DND Approver User: " + userDNDApproverExl + " logged in on Lightning View");

                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    //Approve the Opportunity 
                    string status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Pass", "Opportunity DND Request: " + status + " ");
                    randomPages.ReloadPage();
                    opportunityDetails.CloseApprovalHistoryTabL();
                    //Get OppName after DND approved
                    string approvedDNDOppName = opportunityDetails.GetOpportunityNameL();
                    extentReports.CreateStepLogs("Pass", opportunityDetails.ValidateOpportunityNameL(approvedDNDOppName));
                    //Get DND Status
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Approving the DND Request");

                    /*Approval History:
                    DND Approval
                    •	Status: Approved
                    •	Assigned To: DND Approval Q
                    •	Actual Approver: Justus Seyler

                    */
                    historyStatus = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 5);
                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Status on DND On Approved Opportunity as DND Approver User");
                    extentReports.CreateStepLogs("Passed", "Status: " + historyStatus + " on DND On Approved Opportunity as DND Approver User");

                    historyExpectedAssignTo = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 9);
                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name on DND On Approved Opportunity as DND Approver User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " on DND On Approved Opportunity as DND Approver User");

                    Assert.AreEqual(userDNDApproverExl, randomPages.GetHistoryActualApprovedLV(), "Verify the Actual Approver Group Name on DND On Approved Opportunity as DND Approver User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + actualApprover + "on DND On Approved Opportunity as DND Approver User");

                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Pass", "DND Approver User: " + userDNDApproverExl + " Loggout ");

                    //8. Login as User: CF Financial and check the Opportunity name, DND status and Approval History.
                    homePage.SearchUserByGlobalSearchN(userExl);
                    extentReports.CreateStepLogs("Info", "CF Financial User: " + userExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(userExl), true);
                    extentReports.CreateLog("User: " + userExl + " logged in on Lightning View");

                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(approvedDNDOppName);
                    //Get DND Status
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Approved the DND On Request AS CF Financial User");

                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Status on DND On Approved Opportunity as CF Financial User");
                    extentReports.CreateStepLogs("Passed", "Status: " + historyStatus + " on on DND On Approved Opportunity as CF Financial User");

                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name on DND On Approved Opportunity as CF Financial User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " on on DND On Approved Opportunity as CF Financial User");

                    Assert.AreEqual(userDNDApproverExl, randomPages.GetHistoryActualApprovedLV(), "Verify the Actual Approver Group Name on DND On Approved Opportunity as CF Financial User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + actualApprover + "on DND On Approved Opportunity as CF Financial User");

                    string historyComments = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 6);
                    Assert.AreEqual(historyComments, randomPages.GetHistoryCommentsLV(), "Verify the Comments on DND On Approved Opportunity as CF Financial User");
                    extentReports.CreateStepLogs("Passed", "Comments on DND On Approved Opportunity: " + historyComments);

                    randomPages.CloseActiveTab(approvedDNDOppName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Pass", "CF Financial User: " + userExl + " Loggout ");

                    //9. Login to CAO : Brian Miller and release the DND request by clicking “DND ON/Off” button. 
                    //DND Status: Removal Requested           

                    homePage.SearchUserByGlobalSearchN(userCAOExl);
                    extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    extentReports.CreateLog("User: " + userCAOExl + " logged in on Lightning View");

                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(approvedDNDOppName);

                    opportunityDetails.ClickDNDOnOffButtonLV();
                    extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " Clicked on DND On/Off Button ");
                    txtMessage = randomPages.GetLVMessagePopup();
                    extentReports.CreateStepLogs("Pass", txtMessage);
                    randomPages.ReloadPage();
                    //Get DND Status
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Submitting the DND Release Request");

                    /* Approval History : 
                    DND Release Approval
                    •	Status: Pending
                    •	Assigned To: DND Approval Q
                    •	Actual Approver: DND Approval Q

                    */
                    historyStatus = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 8);
                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Status on DND Off Requested Opportunity as CAO User");
                    extentReports.CreateStepLogs("Passed", "Status: " + historyStatus + " DND Off Requested Opportunity as CAO User");

                    historyExpectedAssignTo = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 9);
                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name DND Off Requested Opportunity as CAO User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " DND Off Requested Opportunity as CAO User");

                    actualApprover = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 10);
                    Assert.AreEqual(actualApprover, randomPages.GetHistoryActualApproverLV(), "Verify the Actual Approver Group Name DND Off Requested Opportunity as CAO User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + actualApprover + "DND Off Requested Opportunity as CAO User");

                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Pass", "CAO User: " + userCAOExl + " Loggout ");

                    //10.Login as DND Approval Q member – Justus Seyler and load the Opportunity.

                    homePage.SearchUserByGlobalSearchN(userDNDApproverExl);
                    extentReports.CreateStepLogs("Info", "DND Approver User: " + userDNDApproverExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    userDND = login.ValidateUserLightningView();
                    Assert.AreEqual(userDND.Contains(userDNDApproverExl), true);
                    extentReports.CreateLog("DND Approver User: " + userDNDApproverExl + " logged in on Lightning View");

                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "DND Approver User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(approvedDNDOppName);
                    //Approve the Opportunity 
                    status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Pass", "Opportunity DND Request: " + status + " ");
                    randomPages.ReloadPage();
                    opportunityDetails.CloseApprovalHistoryTabL();
                    //Get OppName after DND Off approved
                    approvedDNDOppName = opportunityDetails.GetOpportunityNameL();
                    Assert.AreEqual(opportunityName, approvedDNDOppName);
                    extentReports.CreateStepLogs("Pass", "Opportunity DND Off and Opportunity name is same as original name: " + approvedDNDOppName + " ");
                    //Get DND Status
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Approving the DND Release Request as DND Approver");

                    // Approve the DND release request and check the Opportunity name, DND status, Approval History details.
                    /*
                    DND Status: RELEASED
                        Approval History:
                    DND Release Approval
                    o   Status: Approved
                    o   Assigned To: DND Approval Q
                    o   Actual Approver: Justus Seyler
                    o Comments: DND Release request approved
                    */

                    historyStatus = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 5);
                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Status on DND Release Approved Opportunity as DND Approver User");
                    extentReports.CreateStepLogs("Passed", "Status: " + historyStatus + " on DND Release Approved Opportunity as DND Approver User");

                    historyExpectedAssignTo = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 9);
                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name on DND Release Approved Opportunity as DND Approver User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " on DND Release Approved Opportunity as DND Approver User");

                    Assert.AreEqual(userDNDApproverExl, randomPages.GetHistoryActualApprovedLV(), "Verify the Actual Approver Group Name on DND Release Approved Opportunity as DND Approver User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + actualApprover + "on DND Release Approved Opportunity as DND Approver User");

                    Assert.AreEqual(historyComments, randomPages.GetHistoryCommentsLV(), "Verify the Comments on DND Release Approved Opportunity as DND Approver User");
                    extentReports.CreateStepLogs("Passed", "Comments on DND Release Approved Opportunity: " + historyComments);

                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Pass", "DND Approver User: " + userDNDApproverExl + " Loggout ");

                    //11. Login to user: Scott Alford and check the Opportunity name, DND status and Approval History. 

                    homePage.SearchUserByGlobalSearchN(userExl);
                    extentReports.CreateStepLogs("Info", "CF Financial User: " + userExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(userExl), true);
                    extentReports.CreateLog("CF Financial User: " + userExl + " logged in on Lightning View");

                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "CF Financial User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    //Get DND Status
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Approving the DND Release Request as CF Financial user");

                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Status on DND Release Approved Opportunity as CF Financial User");
                    extentReports.CreateStepLogs("Passed", "Status: " + historyStatus + " on DND Release Approved Opportunity as CF Financial User");

                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name on DND Release Approved Opportunity as CF Financial User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " on DND Release Approved Opportunity as CF Financial User");

                    Assert.AreEqual(userDNDApproverExl, randomPages.GetHistoryActualApprovedLV(), "Verify the Actual Approver Group Name on DND Release Approved Opportunity as CF Financial User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + actualApprover + "on DND Release Approved Opportunity as CF Financial User");

                    Assert.AreEqual(historyComments, randomPages.GetHistoryCommentsLV(), "Verify the Comments on DND Release Approved Opportunity as CF Financial User");
                    extentReports.CreateStepLogs("Passed", "Comments on DND Release Approved Opportunity: " + historyComments);

                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Pass", "CF Financial User: " + userExl + " Loggout ");

                    //12.Login to CAO: Brian Miller and request the DND by clicking “DND ON/ Off” button.
                    homePage.SearchUserByGlobalSearchN(userCAOExl);
                    extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    extentReports.CreateLog("User: " + userCAOExl + " logged in on Lightning View");

                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                    opportunityDetails.ClickDNDOnOffButtonLV();
                    extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " Clicked on DND On/Off Button ");
                    txtMessage = randomPages.GetLVMessagePopup();
                    extentReports.CreateStepLogs("Pass", txtMessage);
                    randomPages.ReloadPage();
                    //Get DND Status
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Submitting the DND On Request As CAO User");

                    historyStatus = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 8);
                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Status on DND On Requested Opportunity");
                    extentReports.CreateStepLogs("Passed", "CAO User Requested Opportunity DND On Status: " + historyStatus);

                    //string historyExpectedAssignTo = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 9);
                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name DND On Requested Opportunity as CAO user");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " DND On Requested Opportunity as CAO user");

                    //string actualApprover = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 10);
                    Assert.AreEqual(actualApprover, randomPages.GetHistoryActualApproverLV(), "Verify the Actual Approver Group Name DND On Requested Opportunity as CAO user");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + actualApprover + "DND On Requested Opportunity as CAO user");


                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Pass", "CAO User: " + userCAOExl + " Loggout ");

                    //13. Login as DND Approval Q member – Justus Seyler and load the Opportunity.
                    /*Approve the DND request and check the Opportunity name, DND status, Approval History details.                    
                    DND Status : APPROVED  
                     */
                    homePage.SearchUserByGlobalSearchN(userDNDApproverExl);
                    extentReports.CreateStepLogs("Info", "DND Approver User: " + userDNDApproverExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    userDND = login.ValidateUserLightningView();
                    Assert.AreEqual(userDND.Contains(userDNDApproverExl), true);
                    extentReports.CreateLog("DND Approver User: " + userDNDApproverExl + " logged in on Lightning View");

                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "DND Approver User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    //Approve the Opportunity 
                    status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Pass", "Opportunity DND Request: " + status + " ");
                    randomPages.ReloadPage();
                    opportunityDetails.CloseApprovalHistoryTabL();

                    //Get OppName after DND approved
                    approvedDNDOppName = opportunityDetails.GetOpportunityNameL();
                    extentReports.CreateStepLogs("Pass", opportunityDetails.ValidateOpportunityNameL(approvedDNDOppName));
                    //Get DND Status
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Approving the DND On Request as DND Approver");

                    /* Approval History:
                                    DND Approval
                    •	Status: Approved
                    •	Assigned To: DND Approval Q
                    •	Actual Approver: Justus Seyler
                    */

                    historyStatus = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 5);
                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Status on DND On Approved Opportunity as CAO User");
                    extentReports.CreateStepLogs("Passed", "Status: " + historyStatus + " on DND On Approved Opportunity as CAO User");

                    historyExpectedAssignTo = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 9);
                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name on DND On Approved Opportunity as CAO User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " on DND On Approved Opportunity as CAO User");

                    Assert.AreEqual(userDNDApproverExl, randomPages.GetHistoryActualApprovedLV(), "Verify the Actual Approver Group Name on DND On Approved Opportunity as CAO User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + actualApprover + "on DND On Approved Opportunity as CAO User");

                    Assert.AreEqual(historyComments, randomPages.GetHistoryCommentsLV(), "Verify the Comments on DND On Approved Opportunity as as CAO User");
                    extentReports.CreateStepLogs("Passed", "Comments on DND Approved Opportunity: " + historyComments);

                    randomPages.CloseActiveTab(approvedDNDOppName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Pass", "DND Approver User: " + userDNDApproverExl + " Loggout ");

                    //14. Login as User:  Scott Alford and check the opportunity name, DND status and Approval History details.
                    homePage.SearchUserByGlobalSearchN(userExl);
                    extentReports.CreateStepLogs("Info", "CF Financial User: " + userExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(userExl), true);
                    extentReports.CreateLog("User: " + userExl + " logged in on Lightning View");

                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(approvedDNDOppName);
                    //Get DND Status
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Approving the DND Request as CF FInancial User");

                    //14. Login as User:  Denis Collins and convert the opportunity to partial engagement by changing the stage to “Verbally engaged”.
                    opportunityDetails.ClickTabInfoLV();
                    string stage = opportunityDetails.GetStageLV();
                    string stageExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 31);
                    opportunityDetails.EditOpportunityStageLV(stageExl);
                    string updatedStage = opportunityDetails.GetStageLV();
                    Assert.AreEqual(updatedStage, stageExl);
                    extentReports.CreateStepLogs("Passed", "Opportunity Stage is updated from " + stage + " to " + updatedStage);
                    Assert.IsTrue(randomPages.GetVerballyEngCheckboxStatusLV(), "Verify Verbally Engaged checkbox is Checked after stage change of the Opportunity to Verbally Engaged");
                    extentReports.CreateStepLogs("Passed", "Verbally Engaged checkbox is Checked after stage change of the Opportunity to Verbally Engaged");

                    randomPages.CloseActiveTab(approvedDNDOppName);
                    extentReports.CreateStepLogs("Info", updatedStage + " opportunity  is closed");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "CF Financial User is on Partial Engaged " + moduleNameExl);
                    CustomFunctions.PageReload(driver);
                    engagementHome.GlobalSearchEngagementInLightningView(approvedDNDOppName);
                    //extentReports.CreateStepLogs("Info", " User is on " + updatedStage + " Engagement page");

                    //Get DND Status
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " On DND Verbally Engaged Engagement as CF FInancial User");

                    //15.Click on the partial engagement link and load the partial engagement
                    Assert.IsTrue(randomPages.GetVerballyEngCheckboxStatusLV(), "Verify Verbally Engaged checkbox is Checked on Verbally Engaged Engagement");
                    extentReports.CreateStepLogs("Passed", "Verbally Engaged checkbox is Checked on Verbally Engaged Engagement");


                    //16. Navigate to Administration tab and check the record type
                    engagementDetails.ClickEngAdministrationTabLV();
                    string engRecordType = engagementDetails.GetRecordTypeLV();
                    string recordTypeExpected = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 2);
                    Assert.AreEqual(recordTypeExpected, engRecordType);
                    extentReports.CreateStepLogs("Passed", "Value of Record type is : " + engRecordType + " for Job Type " + valJobType + " ");

                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected");

                    //17.Navigate to the "Oracle ERP" tab and verify the "Product Type", "ERP Product Type Code" field value. productType = randomPages.GetERPProductTypeLV();
                    Assert.AreEqual(prodType, productType, "Verify the Product Type in Oracle ERP Information Opportunity details page for the opportunity having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type  " + productType + " in ERP section for Job Type: " + valJobType);

                    productTypeCodeERP = randomPages.GetERPProductTypeCodeLV();
                    Assert.AreEqual(prodTypeCodeERP, productTypeCodeERP, "Verify the Product code in Oracle ERP Information Opportunity details page for the opportunity having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type Code: " + productTypeCodeERP + " in ERP section for Job Type: " + valJobType);

                    //18. Click on “Request Full Engagement” button and convert the partial engaged engagement to Full Engagement.
                    engagementDetails.ClickRequestFullEngagementLV();
                    engagementDetails.EnterRequestFullEngagementReqValuesLV();
                    extentReports.CreateStepLogs("Info", "Required Fields for Request Full Engagement are entered");
                    extentReports.CreateStepLogs("Info", randomPages.ClickInterruptionOKButtonLV());

                    //CF Financial user add Engagement Contact
                    engagementDetails.CickAddEngagementContactLV(valRecordType);
                    string billingContactNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", 3, 1);
                    string contactPartyExl = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", 3, 3);
                    engagementDetails.CreateBillingContactLV(billingContactNameExl, contactPartyExl);
                    string popupMessage = randomPages.GetLVMessagePopup();
                    extentReports.CreateStepLogs("Passed", billingContactNameExl + " Primary, Billing Contact added on Verbally Engaged Engagement page(Required for Full Engagement Request)");

                    engagementDetails.ClickRequestFullEngagementLV();
                    extentReports.CreateStepLogs("Info", "Click on Request Full Engagement button and Fill are required fields");
                    //*******Don't have clarify of the Engagement Information pop-up******
                    engagementDetails.ClickSaveEngagementInformationPopup();
                    extentReports.CreateStepLogs("Info", randomPages.ClickInterruptionOKButtonLV());
                    randomPages.ReloadPage();

                    //19.Check the details in Approval history section.
                    /*MAEngagementReview
                    •	Status: Pending
                    •	Assigned To: Conversion CF MA
                    •	Actual Approver: Conversion CF MA
                    */
                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected");
                    historyStatus = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 8);
                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Status on Requested Full engaged Engagement");
                    extentReports.CreateStepLogs("Passed", "Status: " + historyStatus + " on Requested Full engaged Engagement");

                    historyExpectedAssignTo = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 3);
                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name on Requested Full engaged Engagement");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " on Requested Full engaged Engagement");

                    actualApprover = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 4);
                    Assert.AreEqual(actualApprover, randomPages.GetHistoryActualApproverLV(), "Verify the Actual Approver Group Name on Requested Full engaged Engagement");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + actualApprover + "on Requested Full engaged Engagement");

                    randomPages.CloseActiveTab(approvedDNDOppName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Pass", "CF Financial User: " + userExl + " Loggout ");

                    homePage.SearchUserByGlobalSearchN(userCAOExl);
                    extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    extentReports.CreateStepLogs("Passed", "User: " + userCAOExl + " logged in on Lightning View");
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity &Approve the Opportunity 
                    engagementHome.GlobalSearchEngagementInLightningView(approvedDNDOppName);

                    //20.Login as CS CAO – Jack Truett and load the partial engagement – .
                    //check the details in Approval history section.
                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Assign To for Approval Group Name Before approval as CAO User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyStatus + " Before approval as CAO User");

                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name Before approval as CAO User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " Before approval as CAO User");

                    Assert.AreEqual(actualApprover, randomPages.GetHistoryActualApproverLV(), "Verify the Actual Approver Group Name Before approval as CAO User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + actualApprover + " Before approval as CAO User");

                    //21. Approve the Full Engagement request 
                    /*
                    • Status: Approved
                    • Assigned To: Conversion CF MA/CS
                    •  Actual Approver: Brian Miller
                    • Comments: Engagement Conversion request Approved
                    */

                    status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Passed", "DND Engagement  " + status+" as Full Engagement"); ;
                    randomPages.ReloadPage();
                    randomPages.CloseActiveTab("Approval History");

                    historyStatus = historyStatus = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 5);
                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Assign To for Approval Group Name after approval on Verbally Engaged Engagement as CAO User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyStatus + " after approval on Verbally Engaged Engagement as CAO User");

                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name after approval on Verbally Engaged Engagement as CAO User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " after approval on Verbally Engaged Engagementas CAO User");

                    Assert.AreEqual(userCAOExl, randomPages.GetHistoryActualApprovedLV(), "Verify the Actual Approver Group Name after approval on Verbally Engaged Engagementas CAO User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + userCAOExl + "after approval on Verbally Engaged Engagementas CAO User");

                    Assert.AreEqual(historyComments, randomPages.GetHistoryCommentsLV(), "Verify the Actual Approver Group Name on Verbally Engaged Engagement");
                    extentReports.CreateStepLogs("Passed", "Affter Approved Comments: " + historyComments);

                    //22.Navigate to Administration tab and check the Record type.
                    //Validate the value of Record Type in Engagement details page
                    engagementDetails.ClickEngAdministrationTabLV();
                    engRecordType = engagementDetails.GetRecordTypeLV();
                    recordTypeExpected = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 2);
                    Assert.AreEqual(recordTypeExpected, engRecordType);
                    extentReports.CreateStepLogs("Passed", "Value of Record type is : " + engRecordType + " for Job Type " + valJobType + " ");

                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected");

                    //23. Navigate to the "Oracle ERP" tab and verify the "Product Type", "ERP Product Type Code" field value
                    productType = randomPages.GetERPProductTypeLV();
                    //prodType = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 1);
                    Assert.AreEqual(prodType, productType, "Verify the Product Type in Oracle ERP Information Engagement details page for the Engagement having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type  " + productType + " in ERP section for Job Type: " + valJobType);

                    //prodTypeCodeERP = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 2);
                    productTypeCodeERP = randomPages.GetERPProductTypeCodeLV();
                    Assert.AreEqual(prodTypeCodeERP, productTypeCodeERP, "Verify the Product code in Oracle ERP Information Engagement details page for the Engagement having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type Code: " + productTypeCodeERP + " in ERP section for Job Type: " + valJobType);


                    //24.Navigate to Revenue tab and check the revenue accrual.
                    string feesPA = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 7);
                    engagementDetails.AddAccrualLV(feesPA);
                    extentReports.CreateStepLogs("Passed", randomPages.GetLVMessagePopup());

                    //Get Estimated fees
                    string historyNewValue = engagementDetails.GetHistoryNewValueLV();
                    Assert.IsTrue(historyNewValue.Contains(feesPA), "Verify the Period Accrued Fees is saved and displayed in Engegement History section");
                    extentReports.CreateStepLogs("Passed", "Period Accrued Fees is saved and displayed in Engegement History section");

                    string revAccruTotalestimatedfee = engagementDetails.GetRevenueAccruTotalEstimatedFeeLV();
                    Assert.IsTrue(revAccruTotalestimatedfee.Contains(historyNewValue), "Verify the Total Estimated fees in Revenue Accrual section is matching the saved fees in Engegement History section");
                    extentReports.CreateStepLogs("Passed", "Total Estimated fees in Revenue Accrual section is matching the saved fees in Engegement History section");


                    //

                    randomPages.CloseActiveTab(approvedDNDOppName);
                    randomPages.CloseActiveTab(approvedDNDOppName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " Logged out ");
                }

                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Pass", "Browser Closed Successfully");
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
