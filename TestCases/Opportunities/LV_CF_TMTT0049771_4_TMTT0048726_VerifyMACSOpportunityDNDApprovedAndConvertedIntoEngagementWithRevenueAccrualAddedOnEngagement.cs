using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.OpportunitiesDND
{
    class LV_CF_TMTT0049771_4_TMTT0048726_VerifyMACSOpportunityDNDApprovedAndConvertedIntoEngagementWithRevenueAccrualAddedOnEngagement: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTT0049771 = "LV_4_CF_TMTT0049771_VerifyMACSOpportunityDNDApprovedAndConvertedIntoEngagementwithRevenueAccrualAddedOnEngagement";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0123100	Verify that the M&A opportunity is DND approved and converted into an engagement and revenue accrual is added on to the engagement. 
        //TMTI0121887	Verify that the DND Opportunity is converted into a DND Engagement for the new Job Types

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
                    homePage.SearchUserByGlobalSearchN(userExl);
                    extentReports.CreateStepLogs("Info", "CF Financial User: " + userExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(userExl), true);
                    extentReports.CreateLog("User: " + userExl + " logged in on Lightning View");

                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    //Validating Title of New Opportunity Page
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB: " + valRecordType);
                    extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTMTT0049771);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0049771);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Pass", "User is on Opportunity detail " + displayedTab + " tab ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                    extentReports.CreateStepLogs("Pass", "Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact      
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);

                    addOpportunityContact.CickAddCFOpportunityContact();
                    addOpportunityContact.CreateContactL2(fileTMTT0049771, valRecordType);
                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");
                    opportunityDetails.UpdateReqFieldsForCFConversionLV2(fileTMTT0049771, valJobType);//udated Move to element
                    extentReports.CreateStepLogs("Info", "Location where Benefit was Provided value filled ");
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0049771);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    randomPages.CloseActiveTab("Internal Team");
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab(opportunityName);

                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Pass", "CF Financial User: " + userExl + " Loggout ");

                    string adminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 4, 1);
                    homePage.SearchUserByGlobalSearchN(adminUser);
                    extentReports.CreateStepLogs("Info", "Admin User: " + adminUser + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "Admin User: " + adminUser + " logged in on Lightning View");
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "Admin User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Info", "Admin is Performing Required Actions ");
                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeNBCApproveDetailsLV(valJobType);
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "System Administrator: "+adminUser + "  logged out ");


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
                    extentReports.CreateStepLogs("Pass", "DND Status: "+randomPages.GetDNDStatusLV()+" After Submitted the DND Request");

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
                    homePageLV.UserLogoutFromSFLightningView();
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
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Approving the DND On Request");
                    
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
                    homePageLV.UserLogoutFromSFLightningView();
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
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Approving the DND On Request");

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
                    homePageLV.UserLogoutFromSFLightningView();
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
                    homePageLV.UserLogoutFromSFLightningView();
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
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Approving the DND Release Request");

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
                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Status on DND Off Approved Opportunity as DND Approver User");
                    extentReports.CreateStepLogs("Passed", "Status: " + historyStatus + " on DND Off Approved Opportunity as DND Approver User");

                    historyExpectedAssignTo = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 9);
                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name on DND Off Approved Opportunity as DND Approver User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " on DND Off Approved Opportunity as DND Approver User");

                    Assert.AreEqual(userDNDApproverExl, randomPages.GetHistoryActualApprovedLV(), "Verify the Actual Approver Group Name on DND Off Approved Opportunity as DND Approver User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + actualApprover + "on DND Off Approved Opportunity as DND Approver User");
                    
                    Assert.AreEqual(historyComments, randomPages.GetHistoryCommentsLV(), "Verify the Comments on DND Off Approved Opportunity as DND Approver User");
                    extentReports.CreateStepLogs("Passed", "Comments on DND Off Approved Opportunity: " + historyComments);

                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.UserLogoutFromSFLightningView();
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
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Approving the DND Release Request");

                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Status on DND Off Approved Opportunity as CF Financial User");
                    extentReports.CreateStepLogs("Passed", "Status: " + historyStatus + " on DND Off Approved Opportunity as CF Financial User");

                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name on DND Off Approved Opportunity as CF Financial User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " on DND Off Approved Opportunity as CF Financial User");

                    Assert.AreEqual(userDNDApproverExl, randomPages.GetHistoryActualApprovedLV(), "Verify the Actual Approver Group Name on DND Approved Opportunity as CF Financial User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + actualApprover + "on DND Off Approved Opportunity as CF Financial User");
                                        
                    Assert.AreEqual(historyComments, randomPages.GetHistoryCommentsLV(), "Verify the Comments on DND Off Approved Opportunity as CF Financial User");
                    extentReports.CreateStepLogs("Passed", "Comments on DND Approved Opportunity: " + historyComments);

                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.UserLogoutFromSFLightningView();
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
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Submitting the DND On Request");

                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.UserLogoutFromSFLightningView();
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
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Approving the DND On Request");

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
                    homePageLV.UserLogoutFromSFLightningView();
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
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Approving the DND On Request");

                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Status on DND On Approved Opportunity as CF Financial User");
                    extentReports.CreateStepLogs("Passed", "Status: " + historyStatus + " on DND On Approved Opportunity as CF Financial User");

                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name on DND On Approved Opportunity as CF Financial User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " on  DND On Approved Opportunity as CF Financial User");

                    Assert.AreEqual(userDNDApproverExl, randomPages.GetHistoryActualApprovedLV(), "Verify the Actual Approver Group Name on DND On Approved Opportunity as CF Financial User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + actualApprover + "on DND On Approved Opportunity as CF Financial User");
                                        
                    Assert.AreEqual(historyComments, randomPages.GetHistoryCommentsLV(), "Verify the Comments on DND On Approved Opportunity as CF Financial User");
                    extentReports.CreateStepLogs("Passed", "Comments on DND On Approved Opportunity: " + historyComments);

                    //15. Request an engagement by clicking “Request Engagement” option
                    //Submit Request To Engagement Conversion
                    opportunityDetails.ClickRequestToEngL();
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");

                    /*Check Assigned To in Approval History.
                    •	Assigned To: Conversion CF MA/CA
                    •	Actual Approver: Conversion CF MA/CS
                    */
                    historyExpectedAssignTo = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 3);
                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name before approval as CF inancial User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " before approval as CF inancial User");

                    string historyExpectedActualApprover = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 4);
                    Assert.AreEqual(historyExpectedActualApprover, randomPages.GetHistoryActualApproverLV(), "Verify the Actual Approver Group Name before approval as CF inancial User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + historyExpectedActualApprover + " before approval as CF inancial User");

                    randomPages.CloseActiveTab(approvedDNDOppName);
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Pass", "CF Financial User: " + userExl + " Loggout ");

                    //16. Login as CAO Brian Miller and Approve the request and check the Approval History
                    
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
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity &Approve the Opportunity 
                    opportunityHome.GlobalSearchOpportunityInLightningView(approvedDNDOppName);
                    //Get DND Status
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Approving the DND On Request");

                    /*
                    • Status: Approved
                    • Assigned To: Conversion CF MA/CS
                    •  Actual Approver: Brian Miller
                    • Comments: Engagement Conversion request Approved
                    */

                    status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Passed", "Opportunity " + status); ;
                    randomPages.ReloadPage();
                    randomPages.CloseActiveTab("Approval History");

                    
                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Assign To for Approval Group Name after approval as CAO User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyStatus + " after approval as CAO User");

                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name after approval as CAO User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " after approval as CAO User");

                    Assert.AreEqual(userCAOExl, randomPages.GetHistoryActualApprovedLV(), "Verify the Actual Approver Group Name after approval as CAO User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + historyExpectedActualApprover + "after approval as CAO User");
                    
                    Assert.AreEqual(historyComments, randomPages.GetHistoryCommentsLV(), "Verify the Actual Approver Group Name");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + historyComments);

                    //17. Convert the DND Opportunity to Engagement by clicking “Convert to Engagement”.
                    //opportunityDetails.CheckConvertedToEng();

                    opportunityDetails.ClickConvertToEngagementL2();
                    extentReports.CreateStepLogs("Info", "Opportunity Converted into Engagement ");
                    //Validate the Engagement name in Engagement details page
                    string engagementNumber = engagementDetails.GetEngagementNumberL();
                    string engagementName = engagementDetails.GetEngagementNameL();
                    //Need to get Name of Opp and Eng
                    Assert.AreEqual(approvedDNDOppName, engagementName);
                    extentReports.CreateStepLogs("Passed", "Name of Engagement : " + engagementName + " is Same as Opportunity name ");

                    randomPages.CloseActiveTab(engagementName);
                    randomPages.CloseActiveTab(approvedDNDOppName);
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    engagementHome.GlobalSearchEngagementInLightningView(engagementName);
                    //Get DND Status
                    extentReports.CreateStepLogs("Pass", "DND Status: " + randomPages.GetDNDStatusLV() + " After Approving the DND On Request");


                    //TMTI0121887	Verify that the DND Opportunity is converted into a DND Engagement for the new Job Types
                    //get JobType on engement page
                    string engJobType = engagementDetails.GetJobTypeL();
                    Assert.AreEqual(valJobType, engJobType, "Verify that the DND Opportunity is converted into a DND Engagement for the new Job Types: "+ valJobType);
                    extentReports.CreateStepLogs("Passed", "DND Opportunity is converted into a DND Engagement with new Job Type: " + valJobType);


                    //Validate the value of Record Type in Engagement details page
                    engagementDetails.ClickEngAdministrationTabLV();
                    string engRecordType = engagementDetails.GetRecordTypeLV();
                    string recordTypeExpected = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 2);
                    Assert.AreEqual(recordTypeExpected, engRecordType);
                    extentReports.CreateStepLogs("Passed", "Value of Record type is : " + engRecordType + " for Job Type " + valJobType + " ");

                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected");

                    //12. Navigate to the "Oracle ERP" tab and verify the "Product Type", "ERP Product Type Code" field value
                    string productType = randomPages.GetERPProductTypeLV();
                    string prodType = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 1);
                    Assert.AreEqual(prodType, productType, "Verify the Product Type in Oracle ERP Information Engagement details page for the Engagement having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type  " + productType + " in ERP section for Job Type: " + valJobType);

                    string prodTypeCodeERP = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 2);
                    string productTypeCodeERP = randomPages.GetERPProductTypeCodeLV();
                    Assert.AreEqual(prodTypeCodeERP, productTypeCodeERP, "Verify the Product code in Oracle ERP Information Engagement details page for the Engagement having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type Code: " + productTypeCodeERP + " in ERP section for Job Type: " + valJobType);


                    //13. Navigate to Revenue tab and check the revenue Accruals. 
                    //14. Click on “Add Accrual” button and enter value in “Period Accrued Fees” field and click save. 
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

                    randomPages.CloseActiveTab(engagementName);
                    randomPages.CloseActiveTab(engagementName);
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
