using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using NUnit.Framework;
using SF_Automation.TestData;
using System;

namespace SF_Automation.TestCases.OpportunitiesDND
{
    class LV_TMTT0037504_VerifyTheTASDNDFunctionalityforFVAOpportunityAndEngagementOnSFLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTMTT0037504 = "LV_TMTT0037504_VerifyTheTASDNDFunctionalityforFVAOpportunityAndEngagementOnSFLightningView";
        private string appNameExl;
        private string appName;
        private string moduleNameExl;
        private string pageTitle;
        private string user;
        private string displayedTab;
        private string opportunityNumber;
        private string adminUserExl;
        private string opportunityName;
        private string dealMemberNameExl;
        private string dealMemberLOBExl;
        private string dealMemberRoleExl;
        private string dealMemberGroupName;
        private string result;
        private string txtMessage;
        private string dndOppName;
        string chkTASDNDStatus;

        //Note:: User with Role Principle must be part of TAS Officer Group to access the TAS DND button and OPportunity
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyTheTASDNDFunctionalityforFVAOpportunityAndEngagementOnSFLightningView()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0037504;
                extentReports.CreateStepLogs("Info", "Verify Functionality of Opportunity to Engagement conversion for LOB:FVA On LightningView");
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                int rowOpp = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                for (int row = 2; row <= rowOpp; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 25);
                    extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType + " ");
                    //Login as Standard User profile and validate the user
                    string stdUserExl = ReadExcelData.ReadData(excelPath, "StandardUser", 1);
                    homePage.SearchUserByGlobalSearchN(stdUserExl);
                    extentReports.CreateStepLogs("Info", "User: " + stdUserExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(stdUserExl), true);
                    extentReports.CreateStepLogs("Passed", "Standard User: " + stdUserExl + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateLog("User: " + stdUserExl + " Switched to Lightning View ");
                    
                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Validating Title of New Opportunity Page
                    pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                    extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                    extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType);
                    opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileTMTT0037504);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");
                    //Call function to enter Internal Team details and validate Opportunity detail page
                    displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0037504);
                    Assert.AreEqual(displayedTab, "Info");
                    randomPages.CloseActiveTab("Internal Team");
                    extentReports.CreateStepLogs("Passed", "User is on Opportunity detail " + displayedTab + " tab ");
                    //Validating Opportunity details  
                    opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                    extentReports.CreateStepLogs("Passed", "Opportunity with number : " + opportunityNumber + " is created ");
                    //Create External Primary Contact      
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    addOpportunityContact.CickAddOpportunityContactLV();
                    addOpportunityContact.CreateContactL2(fileTMTT0037504);
                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");
                    //Update required Opportunity fields for conversion and Internal team details  
                    opportunityDetails.UpdateReqFieldsForFVAConversionLV(fileTMTT0037504);
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    if (valJobType.Contains("TAS"))
                    {
                        opportunityDetails.UpdateTASServicesLV();
                        extentReports.CreateStepLogs("Info", "TAS Services field is updated");
                    }
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    //TMTI0090536 Verify that the TAS DND button is not available for FVA Deal team members having non-Principal roles
                    extentReports.CreateStepLogs("Info", "Verify that the TAS DND button is not available for FVA Deal team members: "+user + " having non-Principal roles " );
                    Assert.IsFalse(opportunityDetails.IsBtnTASDNDDisplayedLV(), "Verify that the TAS DND button is not available for FVA Deal team members: " + user + " having non-Principal roles ");
                    extentReports.CreateStepLogs("Passed", "TAS DND(On/Off) button is not available for FVA Deal team members: "+ user + " having non-Principal roles");
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", "User: " + user + " logged out");
                    ///////////////////////////////////////////////////
                    //Login as System Admin user 
                    adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 3, 1);
                    extentReports.CreateStepLogs("Info", "System Admin User: " + adminUserExl + " Updating the Required details ");
                    homePage.SearchUserByGlobalSearchN(adminUserExl);
                    extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(adminUserExl), true);
                    extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUserExl + " User logged in ");
                    login.SwitchToClassicView();
                    opportunityHome.SearchOpportunity(opportunityName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");                     
                    opportunityDetails.UpdateOutcomeDetails(fileTMTT0037504);
                    extentReports.CreateStepLogs("Info", "Conflict Check fields are updated");
                    /////////////////////////////////////////////////////////////////////
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "System Admin Switched to Lightning View ");
                    
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0037504);
                    extentReports.CreateStepLogs("Info", "User with all required roles for requesting Engagement is added  ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab("Internal Team");
                    //////Standard User don't have permission to modify the Internal team so System Admin is modifying the members/roles////////
                    opportunityDetails.AddOppMultipleDealTeamMembersLV(fileTMTT0037504);
                    extentReports.CreateStepLogs("Info", "Addition Opportunity Internal Team members are added with differnt roles ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab("Internal Team");
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", "User: " + user + " logged out");
                    ////////////////////////////////////////////////////////////////////////                
                    //TMTI0090527 Verify that the TAS DND button is not available for CF Deal team members having a Non-Principal role
                    //TMTI0090528 Verify that the TAS DND button is not available for FR Deal team members having non-Principal roles
                    //TMTI0090530 Verify that the TAS DND button is not accessible to CF Deal team members having a Principal role
                    //TMTI0090531 Verify that the TAS DND button is not accessible to FR Deal team members having a Principal role
                    //TMTI0090529 Verify that the TAS DND button is accessible for FVA Deal team members having Principal roles
                    int rowMembers = ReadExcelData.GetRowCount(excelPath, "OppDealTeamMembers");
                    for (int memberRow = 3; memberRow <= rowMembers; memberRow++)
                    {
                        dealMemberNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", memberRow, 1);
                        dealMemberLOBExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", memberRow, 3);
                        dealMemberRoleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", memberRow, 4);
                        dealMemberGroupName= ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", memberRow, 5);
                        extentReports.CreateStepLogs("Info", "Member Name: " + dealMemberNameExl + ", Member LOB: " + dealMemberLOBExl + ", Member Role: " + dealMemberRoleExl+ ", Member Group Name: "+ dealMemberGroupName);
                        extentReports.CreateStepLogs("Info", "Verify the TAS DND button availablility Deal team members: " + dealMemberNameExl + " of LOB: " + dealMemberLOBExl+ " with "+ dealMemberRoleExl+" role on Opportunity Detail page");
                        homePage.SearchUserByGlobalSearchN(dealMemberNameExl);
                        extentReports.CreateStepLogs("Info", "User: " + dealMemberNameExl + " details are displayed. ");
                        //Login user
                        usersLogin.LoginAsSelectedUser();
                        login.SwitchToLightningExperience();
                        //Go to Opportunity module in Lightning View                     
                        homePageLV.SelectAppLV(appNameExl);
                        Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                        extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                        homePageLV.SelectModule(moduleNameExl);
                        extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                        //Search for opportunity
                        result = opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                        Assert.AreEqual("Record found", result);
                        extentReports.CreateStepLogs("Passed", result + " and selected");
                        //Verify TAS DND field is present
                        Assert.IsTrue(opportunityDetails.IsTASDNDFieldDisplayedLV(),"Verify TAS DND field is Present on Opportunity Detail page");
                        extentReports.CreateStepLogs("Passed", "TAS DND field is Present on Opportunity Detail page");
                        //Verify the default status of TAS DND field  bool Enabled = driver.FindElement(chkboxPrimaryL).Enabled;
                        chkTASDNDStatus= opportunityDetails.GetTASDNDCheckBoxStatusLV();
                        Assert.AreEqual(chkTASDNDStatus, "Not Checked");
                        extentReports.CreateStepLogs("Passed", "Default TAS DND Checkbox is "+chkTASDNDStatus);
                        if (dealMemberLOBExl=="FVA" && (dealMemberRoleExl== "PRINCIPAL"|| dealMemberRoleExl== "Principal") && dealMemberGroupName== "TAS Officer")
                        {
                            Assert.IsTrue(opportunityDetails.IsBtnTASDNDDisplayedLV(), "Verify DND button availablity is TRUE for Deal team members: " + dealMemberNameExl + " from LOB" + dealMemberLOBExl + " with " + dealMemberRoleExl + " role");
                            extentReports.CreateStepLogs("Passed", "TAS DND button is availablity is True for "+ dealMemberLOBExl + " Deal team members: " + dealMemberNameExl + " with " + dealMemberRoleExl+ " role");
                            //TMTI0090537 Verify that the FVA User having a Principal role is able to click on the TAS DND button and access the TAS DND Opportunity.
                            opportunityDetails.ClickBtnTASDNDLV();
                            extentReports.CreateStepLogs("Passed", "TAS DND button is Clicked and Confirmed ");
                            //TMTI0090542 Verify that once the opportunity is converted into a TAS DND opportunity, the TAS DND checkbox will be checked.
                            chkTASDNDStatus = opportunityDetails.GetTASDNDCheckBoxStatusLV();
                            Assert.AreEqual(chkTASDNDStatus, "Checked");
                            extentReports.CreateStepLogs("Passed", "TAS DND Checkbox is " + chkTASDNDStatus+" for TAS DND(On) Confirmed Opportunity");
                            //TMTI0090546 Verify that once opportunity is released from TAS DND, TAS DND checkbox will be un - checked.
                            opportunityDetails.ClickBtnTASDNDLV();
                            extentReports.CreateStepLogs("Passed", "TAS DND button is Clicked and Confirmed "); 
                            chkTASDNDStatus = opportunityDetails.GetTASDNDCheckBoxStatusLV();
                            Assert.AreEqual(chkTASDNDStatus, "Not Checked");
                            extentReports.CreateStepLogs("Passed", "TAS DND Checkbox is " + chkTASDNDStatus + " for TAS DND(Off) Confirmed Opportunity");
                            opportunityDetails.ClickBtnTASDNDLV();
                            extentReports.CreateStepLogs("Passed", "TAS DND button is Clicked and Confirmed ");
                            chkTASDNDStatus = opportunityDetails.GetTASDNDCheckBoxStatusLV();
                            Assert.AreEqual(chkTASDNDStatus, "Checked");
                            extentReports.CreateStepLogs("Passed", "TAS DND Checkbox is " + chkTASDNDStatus + " for TAS DND(On) Confirmed Opportunity");
                            //Get OppName after DND approved
                            dndOppName = opportunityDetails.GetOpportunityNameL();                            
                        }
                        else
                        {
                            Assert.IsFalse(opportunityDetails.IsBtnTASDNDDisplayedLV(), "Verify DND button availablity is FALSE for " + dealMemberLOBExl + " Deal team members: " + dealMemberNameExl + " with " + dealMemberRoleExl + " role");
                            extentReports.CreateStepLogs("Passed", "TAS DND button is is availablity is False for " + dealMemberLOBExl + " Deal team members: " + dealMemberNameExl + " with " + dealMemberRoleExl + " role");
                        }
                        usersLogin.ClickLogoutFromLightningView();
                        extentReports.CreateStepLogs("Info", "User: " + dealMemberNameExl + " logged out");
                    }
                    ////////////////////////////////////////////////////////////////
                    //TMTI0090532 Verify that FVA User having Non-Principal role is able to Access TAS DND Opportunity.
                    //TMTI0090547 Verify that the FVA Deal team member having a non-Principal role can access the TAS DND Opportunity. (Before Conversion)
                    //TMTI0090533 Verify that the CF Deal team member having a non - Principal role can access the TAS DND Opportunity(Before Conversion)
                    //TMTI0090539 Verify that the FR Deal team member having a non - Principal role can access the TAS DND Opportunity(Before Conversion)
                    //TMTI0090544 Verify that the FVA Deal team member having a Principal role can access the TAS DND Opportunity.  (Before Conversion)
                    //TMTI0090548 Verify that the CF Deal team member having a Principal role can access the TAS DND Opportunity(Before Conversion)
                    //TMTI0090534 Verify that the FR Deal team member having a Principal role can access the TAS DND Opportunity(Before Conversion)
                    extentReports.CreateStepLogs("Info", "Verify that FVA User: "+stdUserExl+" with  Non - Principal role is able to Access TAS DND Opportunity");
                    if (opportunityName != dndOppName)
                    {
                        extentReports.CreateStepLogs("Info", "TAS DND field is "+ chkTASDNDStatus+ " and Opportunity name is changed to " + dndOppName);
                        int rowDealMembers = ReadExcelData.GetRowCount(excelPath, "OppDealTeamMembers");
                        for(int memberRow=2; memberRow<=rowDealMembers; memberRow++)
                        {
                            dealMemberNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", memberRow, 1);
                            dealMemberLOBExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", memberRow, 3);
                            dealMemberRoleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", memberRow, 4);
                            dealMemberGroupName = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", memberRow, 5);
                            extentReports.CreateStepLogs("Info", "Member Name: " + dealMemberNameExl + ", Member LOB: " + dealMemberLOBExl + ", Member Role: " + dealMemberRoleExl + ", Member Group Name: " + dealMemberGroupName);
                            extentReports.CreateStepLogs("Info", "Verify that Deal team members: " + dealMemberNameExl + "of LOB: " + dealMemberLOBExl + " with " + dealMemberRoleExl + " role can access the TAS DND Opportunity");
                            
                            homePage.SearchUserByGlobalSearchN(dealMemberNameExl);
                            extentReports.CreateStepLogs("Info", "User: " + dealMemberNameExl + " details are displayed. ");
                            //Login user
                            usersLogin.LoginAsSelectedUser();
                            login.SwitchToLightningExperience();
                            //Go to Opportunity module in Lightning View                     
                            homePageLV.SelectAppLV(appNameExl);
                            Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                            extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                            homePageLV.SelectModule(moduleNameExl);
                            extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                            //Search for opportunity
                            result = opportunityHome.SearchOpportunitiesInLightningView(dndOppName);
                            Assert.AreEqual("Record found", result);
                            extentReports.CreateStepLogs("Passed", "Opp Name: "+dndOppName+" is "+ result + " and selected");
                            randomPages.CloseActiveTab(dndOppName);
                            usersLogin.ClickLogoutFromLightningView();
                            extentReports.CreateStepLogs("Info", "User: " + dealMemberNameExl + " logged out");
                        }   
                    }
                    ///////////////////////////////
                    //TMTI0090538 Verify that the FVA CAO can access the TAS DND Button on the TAS Opportunity.
                    //TMTI0090543 Verify the FVA CAO can access the TAS DND Opportunity.
                    //Login as FVA- CAO user 
                    string caoUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 2, 1);
                    extentReports.CreateStepLogs("Info", "Verify FVA CAO User: " + caoUserExl + " can access TAS DND Opportinity and TAS DND button ");
                    
                    homePage.SearchUserByGlobalSearchN(caoUserExl);
                    extentReports.CreateStepLogs("Info", "User: " + caoUserExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    //Go to Opportunity module in Lightning View                     
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");                    
                    //TMTI0090538 Verify that the FVA CAO can access the TAS DND Button on the TAS Opportunity.                    
                    result = opportunityHome.SearchOpportunitiesInLightningView(dndOppName);
                    Assert.AreEqual("Record found", result,"Verify FVA CAO user: "+ caoUserExl + " can access the TAS DND opportuity");
                    extentReports.CreateStepLogs("Passed", result + " with opportunity name: " + dndOppName + " and selected");                    
                    //TMTI0090543 Verify the FVA CAO can access the TAS DND Opportunity.                    
                    //Issue : ISU0011573: FVA CAO user not able to access the  TAS DND button on opportunity with  TAS- Job Type
                    Assert.IsTrue(opportunityDetails.IsBtnTASDNDDisplayedLV(), "Verify TAS DND button availablity is TRUE for FVA CAO user: " + caoUserExl );
                    extentReports.CreateStepLogs("Passed", "TAS DND button availablity is TRUE for FVA CAO user: " + caoUserExl);
                    //CAO Add new deal team member in the Principal role and who is not a member of the TAS Officer 
                    opportunityDetails.AddOppMultipleDealTeamMembersLV2(fileTMTT0037504);
                    extentReports.CreateStepLogs("Info", "CAO User adding New Deal team members :  " + dealMemberNameExl + " with LOB: " + dealMemberLOBExl + " of Role: " + dealMemberRoleExl + " from Group: " + dealMemberGroupName);
                    opportunityDetails.ClickReturnToOpportunityLV();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab("Internal Team");
                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "User: " + dealMemberNameExl + " logged out");
                    //TMTI0090540 Verify that new deal team members in the Principal role and members of the TAS Officer have access to TAS DND Opportunity. (Before Conversion)
                    //TMTI0090545 Verify that the new deal team member in the Principal role and who is not a member of the TAS Officer has access to TAS DND Opportunity and cannot click the TAS DND button. (Before Conversion)
                    int rowCount = ReadExcelData.GetRowCount(excelPath, "NewDealTeamMembers");
                    for (int newMemberRow = 2; newMemberRow <= rowCount; newMemberRow++)
                    {
                        dealMemberNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewDealTeamMembers", newMemberRow, 1);
                        dealMemberLOBExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewDealTeamMembers", newMemberRow, 3);
                        dealMemberRoleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewDealTeamMembers", newMemberRow, 4);
                        dealMemberGroupName = ReadExcelData.ReadDataMultipleRows(excelPath, "NewDealTeamMembers", newMemberRow, 5);
                        extentReports.CreateStepLogs("Info", "Verify the Access to DND opportunity and TAS DND button for Deal Team Member Name: " + dealMemberNameExl + ", of LOB: " + dealMemberLOBExl + ", with Role: " + dealMemberRoleExl + ", from Group Name: " + dealMemberGroupName);

                        homePage.SearchUserByGlobalSearchN(dealMemberNameExl);
                        extentReports.CreateStepLogs("Info", "User: " + dealMemberNameExl + " details are displayed. ");
                        //Login user
                        usersLogin.LoginAsSelectedUser();
                        login.SwitchToLightningExperience();                    
                        homePageLV.SelectAppLV(appNameExl);
                        Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                        extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                        homePageLV.SelectModule(moduleNameExl);
                        extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");                     
                        result = opportunityHome.SearchOpportunitiesInLightningView(dndOppName);
                        Assert.AreEqual("Record found", result, "Verify Access for TAS DND opportunity for Deal Team user: " + dealMemberNameExl + ", from Group Name: " + dealMemberGroupName);
                        extentReports.CreateStepLogs("Passed", result + " with opportunity name: "+ dndOppName + " and selected");
                        if (dealMemberLOBExl == "FVA" && (dealMemberRoleExl == "PRINCIPAL" || dealMemberRoleExl == "Principal") && dealMemberGroupName == "TAS Officer")
                        {
                            Assert.IsTrue(opportunityDetails.IsBtnTASDNDDisplayedLV(), "Verify TAS DND button availablity is TRUE for user: " + dealMemberNameExl);
                            extentReports.CreateStepLogs("Passed", "TAS DND button availablity is TRUE for FVA CAO user: " + dealMemberNameExl);
                        }
                        else
                        {
                            Assert.IsTrue(opportunityDetails.IsBtnTASDNDDisplayedLV(), "Verify TAS DND button availablity is TRUE for user: " + dealMemberNameExl);
                            extentReports.CreateStepLogs("Passed", "TAS DND button availablity is TRUE for FVA New Member user: " + dealMemberNameExl);
                            // click Validation
                            txtMessage= opportunityDetails.ClickBtnTASDNDAndGetValidationLV();
                            extentReports.CreateStepLogs("Info", "User: " + dealMemberNameExl + " Clicked on TAS DND(on/Off) button");
                            Assert.AreEqual("You do not have access to TAS DND", txtMessage);                            
                            extentReports.CreateStepLogs("Passed", "Validation message: "+txtMessage +" for user: " + dealMemberNameExl+" is displayed");
                           
                            opportunityDetails.ClickRequestToEngL();
                            //Submit Request To Engagement Conversion 
                            string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                            Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                            extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");
                        }                        

                        usersLogin.ClickLogoutFromLightningView();
                        extentReports.CreateStepLogs("Info", "User: " + dealMemberNameExl + " logged out");
                    }
                    
                    extentReports.CreateStepLogs("Info", "Verify FVA CAO User: " + caoUserExl + " can access TAS DND Opportinity and TAS DND button ");
                    homePage.SearchUserByGlobalSearchN(caoUserExl);
                    extentReports.CreateStepLogs("Info", "User: " + caoUserExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    //Go to Opportunity module in Lightning View                     
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //TMTI0090538 Verify that the FVA CAO can access the TAS DND Button on the TAS Opportunity.                    
                    result = opportunityHome.SearchOpportunitiesInLightningView(dndOppName);
                    Assert.AreEqual("Record found", result, "Verify FVA CAO user: " + caoUserExl + " can access the TAS DND opportuity");
                    extentReports.CreateStepLogs("Passed", result + " with opportunity name: " + dndOppName + " and selected");

                    //Approve the Opportunity 
                    string status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Pass", "Opportunity " + status + " and ready for conversion ");
                    opportunityDetails.CloseApprovalHistoryTabL();

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEngagementL2();
                    extentReports.CreateStepLogs("Info", "Opportunity: " + opportunityName + " Converted into Engagement ");

                    //Validate the Engagement name in Engagement details page
                    string engNumber = engagementDetails.GetEngagementNumberL();
                    Assert.AreEqual(opportunityNumber, engNumber);
                    extentReports.CreateStepLogs("Info", "Number of Engagement : " + engNumber + " is Same as Opportunity number: "+ engNumber);
                    string engName = engagementDetails.GetEngagementNameL();
                    Assert.AreEqual(dndOppName, engName);
                    extentReports.CreateStepLogs("Passed", "Name of Engagement : " + engName + " is Same as Opportunity Name: " + opportunityName);
                                         
                    //TMTI0090549	Verify the FVA CAO can access engaged TAS DND Opportunity and can release the TAS DND using the TAS DND button. (After Conversion)
                    extentReports.CreateStepLogs("Info", "Verify the FVA CAO User: "+ caoUserExl+" can access Engaged TAS DND Opportunity and can release the TAS DND using the TAS DND button. (After Conversion)");
                    engagementDetails.ClickRelatedOpportunityLV();
                    opportunityDetails.CloseConversionPopup();
                    Assert.AreEqual("Engaged",opportunityDetails.GetOppStage());
                    extentReports.CreateStepLogs("Passed", "Engaged TAS DND Opportunity : "+ dndOppName+" is accessible for FVA CAO User: "+ caoUserExl);
                    //Remove deal team member
                    Assert.IsTrue(opportunityDetails.IsBtnTASDNDDisplayedLV(), "Verify TAS DND button availablity is TRUE for FVA CAO user: " + caoUserExl);
                    opportunityDetails.ClickBtnTASDNDLV();
                    extentReports.CreateStepLogs("Passed", "TAS DND button is Clicked and Confirmed ");
                    chkTASDNDStatus = opportunityDetails.GetTASDNDCheckBoxStatusLV();
                    Assert.AreEqual(chkTASDNDStatus, "Not Checked");
                    extentReports.CreateStepLogs("Passed", "TAS DND Checkbox is " + chkTASDNDStatus + " for TAS DND(Off) Confirmed Opportunity");

                    //opportunityDetails.ClickBtnTASDNDLV();
                    //extentReports.CreateStepLogs("Passed", "TAS DND button is Clicked and Confirmed ");
                    //chkTASDNDStatus = opportunityDetails.GetTASDNDCheckBoxStatusLV();
                    //Assert.AreEqual(chkTASDNDStatus, "Checked");
                    //dndOppName = opportunityDetails.GetOpportunityNameL();

                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "FVA CAO user: " + caoUserExl + " logged out");
                    ///////////////////////////////////////////////////////////////////
                    //TMTI0090535 Verify that the TAS DND button is accessible to FVA Deal team members having a Principal role in Engaged Opportunity.
                    dealMemberNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", 7, 1);
                    dealMemberLOBExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", 7, 3);
                    dealMemberRoleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", 7, 4);
                    dealMemberGroupName = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", 7, 5);
                    extentReports.CreateStepLogs("Info", "Verify TAS DND button is accessible to FVA Deal team Member Name: " + dealMemberNameExl + ", of LOB: " + dealMemberLOBExl + ", with Role: " + dealMemberRoleExl + ", from Group Name: " + dealMemberGroupName+ "On Engaged Opportunity");
                    
                   // usersLogin.SearchUserAndLogin(dealMemberNameExl);
                    homePage.SearchUserByGlobalSearchN(dealMemberNameExl);
                    extentReports.CreateStepLogs("Info", "User: " + dealMemberNameExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    //Go to Opportunity module in Lightning View                     
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    result = opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    Assert.AreEqual("Record found", result);
                    extentReports.CreateStepLogs("Passed", result + " and selected");
                    if (dealMemberLOBExl == "FVA" && (dealMemberRoleExl == "PRINCIPAL" || dealMemberRoleExl == "Principal") && dealMemberGroupName == "TAS Officer")
                    {
                        Assert.IsTrue(opportunityDetails.IsBtnTASDNDDisplayedLV(), "Verify DND button availablity is TRUE for Deal team members: " + dealMemberNameExl + " from LOB" + dealMemberLOBExl + " with " + dealMemberRoleExl + " role");
                        extentReports.CreateStepLogs("Passed", "TAS DND button is availablity is True for " + dealMemberLOBExl + " Deal team members: " + dealMemberNameExl + " with " + dealMemberRoleExl + " role");
                        opportunityDetails.ClickBtnTASDNDLV();
                        extentReports.CreateStepLogs("Passed", "TAS DND button is Clicked and Confirmed ");
                        chkTASDNDStatus = opportunityDetails.GetTASDNDCheckBoxStatusLV();
                        Assert.AreEqual(chkTASDNDStatus, "Checked");
                        extentReports.CreateStepLogs("Passed", "TAS DND Checkbox is " + chkTASDNDStatus + " for TAS DND(On) Confirmed Opportunity");
                        dndOppName = opportunityDetails.GetOpportunityNameL();
                    }
                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "FVA user: " + dealMemberNameExl + " logged out");

                    //TMTI0090541	Verify existing deal team members and new deal team members have access to DND Opportunity after adding new deal team members.
                    extentReports.CreateStepLogs("Info", "Verify existing deal team members and new deal team members have access to DND Opportunity after adding new deal team members.");
                    if (opportunityName != dndOppName)
                    {
                        //extentReports.CreateStepLogs("Info", "TAS DND field is " + chkTASDNDStatus + " and Opportunity name is changed to " + dndOppName);
                        dealMemberNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewDealTeamMembers", 2, 1);
                        dealMemberLOBExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewDealTeamMembers", 2, 3);
                        dealMemberRoleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewDealTeamMembers", 2, 4);
                        dealMemberGroupName = ReadExcelData.ReadDataMultipleRows(excelPath, "NewDealTeamMembers", 2, 5);
                        extentReports.CreateStepLogs("Info", "Member Name: " + dealMemberNameExl + ", Member LOB: " + dealMemberLOBExl + ", Member Role: " + dealMemberRoleExl + ", Member Group Name: " + dealMemberGroupName);
                        extentReports.CreateStepLogs("Info", "Verify that New Deal team members: " + dealMemberNameExl + "of LOB: " + dealMemberLOBExl + " with " + dealMemberRoleExl + " role can access the TAS DND Opportunity");
                        homePage.SearchUserByGlobalSearchN(dealMemberNameExl);
                        extentReports.CreateStepLogs("Info", "User: " + dealMemberNameExl + " details are displayed. ");
                        //Login user
                        usersLogin.LoginAsSelectedUser();
                        login.SwitchToLightningExperience();
                        //Go to Opportunity module in Lightning View                     
                        homePageLV.SelectAppLV(appNameExl);
                        Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                        extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                        homePageLV.SelectModule(moduleNameExl);
                        extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                        //Search for opportunity
                        result = opportunityHome.SearchOpportunitiesInLightningView(dndOppName);
                        Assert.AreEqual("Record found", result);
                        extentReports.CreateStepLogs("Passed", "Opp Name: " + dndOppName + " is " + result + " and selected");
                        randomPages.CloseActiveTab(dndOppName);
                        usersLogin.ClickLogoutFromLightningView();
                        extentReports.CreateStepLogs("Info", "User: " + dealMemberNameExl + " logged out");                       
                    }
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Browser Closed");
                }
            }
            catch(Exception ex)
            {
                extentReports.CreateLog(ex.Message);
                login.SwitchToClassicView();
                driver.Quit();
            }
        }
    }
}
