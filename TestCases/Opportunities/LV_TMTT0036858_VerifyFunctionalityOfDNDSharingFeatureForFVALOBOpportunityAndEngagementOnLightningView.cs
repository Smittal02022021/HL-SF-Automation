﻿using NUnit.Framework;
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
    class LV_TMTT0036858_VerifyFunctionalityOfDNDSharingFeatureForFVALOBOpportunityAndEngagementOnLightningView : BaseClass
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
        RandomPages randomPages = new RandomPages();

        public static string fileTMTT0036858 = "LV_TMTT0036858_VerifyFunctionalityOfDNDSharingFeatureForFVALOBOpportunityAndEngagementOnLightningView";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]

        public void VerifySharingFeatureForFVADNDBOpportunityAndEngagementOnLightningView()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0036858;
                extentReports.CreateStepLogs("Info", "Verify Functionality Of DND Sharing Feature For FVA Opportunity And Engagement On LightningView");
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowOpp = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                for (int row = 2; row <= rowOpp; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 25);

                    extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType + " ");
                    //Login as Standard User profile and validate the user
                    string stdUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StandardUser", row, 1);
                    usersLogin.SearchUserAndLogin(stdUserExl);
                    login.SwitchToClassicView();
                    string user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(stdUserExl), true);
                    extentReports.CreateStepLogs("Passed", "Standard User: " + stdUserExl + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateLog("User: " + stdUserExl + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
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
                    extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType);
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileTMTT0036858);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");
                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0036858);
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
                    addOpportunityContact.CickAddOpportunityContactLV();
                    addOpportunityContact.CreateContactL2(fileTMTT0036858);
                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForFVAConversionLV(fileTMTT0036858);
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");

                    //Validate the DND On/Off button for Std User
                    bool isButtonDisplayed = opportunityDetails.IsButtonDNDOnOffDisplayedLV();
                    Assert.IsFalse(isButtonDisplayed);
                    extentReports.CreateStepLogs("Passed", "DND On/Off button is not displayed for Standard User: " + stdUserExl);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Passed", "Standard User: " + stdUserExl + "switched to Classic and Loggout ");

                    //////////////////Verify Sharing Button button is not availalbe for CAO User but visible for Admin////////////////////// 
                    extentReports.CreateStepLogs("Passed", "Verify Sharing Button/Option is available for System Admin only(Instead od CAO User) ");
                    
                    //Login as System Admin user 
                    string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DNDApprover", 3, 1);
                    usersLogin.SearchUserAndLogin(adminUserExl);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(adminUserExl), true);
                    extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUserExl + " User logged in ");

                    login.SwitchToClassicView();
                    opportunityHome.SearchOpportunity(opportunityName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: "+ opportunityName+" found and selected ");
                    //update CC 
                    opportunityDetails.UpdateOutcomeDetails(fileTMTT0036858);
                    extentReports.CreateStepLogs("Info", "Conflict Check fields are updated");


                    /////////////////////////////////////////////////////////////////////
                    //TMTI0088199	Verify the functionality of internal sharing for DND and Non DND FVA Opportunity


                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "System Admin Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");

                    //////Standard User don't have permission to modify the Internal team so System Admin is modifying the roles////////
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0036858);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                    //Add Multipe users with Different Role on created opportunity
                    displayedTab = addOpportunity.EnterMembersToDealTeamL(fileTMTT0036858);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Passed", "More Users are added in Internal Team ");

                    //Validate the DND On/Off button for Admin User Only
                    isButtonDisplayed = opportunityDetails.IsButtonDNDOnOffDisplayedLV();
                    Assert.IsTrue(isButtonDisplayed);
                    extentReports.CreateStepLogs("Passed", "DND On/Off button is displayed for System Admin ");
                    //Validate Only System admin can see the Sharing button
                    bool isBtnDisplayed = opportunityDetails.IsButtonSharingDisplayedLV();
                    Assert.IsTrue(isBtnDisplayed);
                    extentReports.CreateStepLogs("Passed", "Sharing Button Found and Clicked by System Admin");
                    //Verify group "All Internal Users" on click Sharing button
                    bool isGroupDisplayed = opportunityDetails.IsSharingGroupDisplayedLV(ReadExcelData.ReadDataMultipleRows(excelPath, "SharingGroup", 2, 1));
                    Assert.IsTrue(isGroupDisplayed);
                    extentReports.CreateStepLogs("Passed", "Sharing User group Found is displayed for System Admin");
                    //Verify group Type "Entire Organization" on click Sharing button
                    bool isGroupTypeDisplayed = opportunityDetails.IsSharingGroupDisplayedLV(ReadExcelData.ReadDataMultipleRows(excelPath, "SharingGroup", 2, 2));
                    Assert.IsTrue(isGroupTypeDisplayed);
                    extentReports.CreateStepLogs("Passed", "Sharing User group type Found is displayed for System Admin");
                    string dealTeamMemberExl = ReadExcelData.ReadDataMultipleRows(excelPath, "InternalTeams", 2, 1);
                    bool isUserDisplayed = opportunityDetails.IsSharingUserDisplayedLV(dealTeamMemberExl);
                    Assert.IsTrue(isUserDisplayed);
                    extentReports.CreateStepLogs("Passed", "Deal Team Member Found on Sharing Group Pop-up");
                    //Close Sharing Group Popup 
                    opportunityDetails.CloseSharingGroupPopupLV();
                    extentReports.CreateStepLogs("Info", "Sharing Group Popup Closed");
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", "System Admin: "+adminUserExl+" Logged Out");

                    extentReports.CreateStepLogs("Info", "Verify DND On/Off button with CAO User ");
                    //Validate the DND On/Off buton with CAO User
                    //Login as CAO user
                    string caoUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", row, 1);
                    usersLogin.SearchUserAndLogin(caoUserExl);
                    login.SwitchToClassicView();

                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(caoUserExl), true);
                    extentReports.CreateStepLogs("Passed", "CAO User: " + caoUserExl + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "CAO User: " + caoUserExl + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                    isButtonDisplayed = opportunityDetails.IsButtonDNDOnOffDisplayedLV();
                    Assert.IsTrue(isButtonDisplayed);
                    extentReports.CreateStepLogs("Passed", "DND On/Off button is displayed for CAO user: " + caoUserExl);
                    opportunityDetails.ClickDNDOnOffButtonLV();
                    extentReports.CreateStepLogs("Info", "CAO User: " + caoUserExl + "Clicked on DND On/Off Button ");

                    /////////////////////////////
                    //no message for FR Opp 
                    //string txtMessage = randomPages.GetLVMessagePopup();
                    //extentReports.CreateStepLogs("Pass", txtMessage);
                    ///////////////////////////////

                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", "CAO User: " + caoUserExl + "switched to Classic and Loggout ");

                    /////////////////////////////////////////////////////////////////////////////
                    extentReports.CreateStepLogs("Info", "Login and Approve the DND Request with DND Approval Q user ");
                    //Login as user from group DND Approval Q
                    string userDNDApproverExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DNDApprover", 2, 1);//row
                    usersLogin.SearchUserAndLogin(userDNDApproverExl);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(userDNDApproverExl), true);
                    extentReports.CreateStepLogs("Passed", "DND Approver User: " + userDNDApproverExl + "  logged in ");

                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "DND Approver User: " + userDNDApproverExl + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                    //Approve the Opportunity 
                    string status = opportunityDetails.ClickApproveButtonL();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Passed", "Opportunity " + status + " ");
                    opportunityDetails.CloseApprovalHistoryTabL();
                    //Get OppName after DND approved
                    string dndOppName = opportunityDetails.GetOpportunityNameL();
                    extentReports.CreateStepLogs("Info", opportunityDetails.ValidateOpportunityNameL(dndOppName));
                    randomPages.CloseActiveTab(dndOppName);
                    //Search for DND Approved opportunity with new name
                    string result = opportunityHome.UpdateOppAndSearchLV(dndOppName);
                    Assert.AreEqual("Record found", result);
                    extentReports.CreateStepLogs("Passed", result + " with DND Opportunity Name: " + dndOppName);
                    randomPages.CloseActiveTab(dndOppName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", "DND Approver User:: " + userDNDApproverExl + "switched to Classic and Loggout ");

                    /////////////////////////////////////////////////////////////////////////////////
                    //Verify Admin user can search the DND opp with DND-name only and check the Sharing button
                    extentReports.CreateStepLogs("Info", "Verify Admin user( "+ adminUserExl+" ) can search the DND opportunity with DND-name only and check the Sharing button");
                    usersLogin.SearchUserAndLogin(adminUserExl);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(adminUserExl), true);
                    extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUserExl + "  logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "System Admin: "+ adminUserExl+" Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for DND Approved opportunity with new name
                    result = opportunityHome.UpdateOppAndSearchLV(dndOppName);
                    Assert.AreEqual("Record found", result);
                    extentReports.CreateStepLogs("Passed", result + " with DND Opportunity Name: " + dndOppName);

                    //Sharing button is available for System Admin Only
                    //Verify group "All Internal Users" on click Sharing button
                    //Validate Only System admin can see the Sharing button
                    isBtnDisplayed = opportunityDetails.IsButtonSharingDisplayedLV();
                    Assert.IsTrue(isBtnDisplayed);
                    extentReports.CreateStepLogs("Passed", "Sharing Button Found and Clicked for System Admin");
                    isGroupDisplayed = opportunityDetails.IsSharingGroupDisplayedLV(ReadExcelData.ReadDataMultipleRows(excelPath, "SharingGroup", 2, 1));
                    Assert.IsFalse(isGroupDisplayed);
                    extentReports.CreateStepLogs("Passed", "Sharing User group not Found for search Opportunity ");
                    //Verify group Type "Entire Organization" on click Sharing button
                    bool isGroupTypeDisplayed1 = opportunityDetails.IsSharingGroupDisplayedLV(ReadExcelData.ReadDataMultipleRows(excelPath, "SharingGroup", 2, 2));
                    Assert.IsFalse(isGroupTypeDisplayed1);
                    extentReports.CreateStepLogs("Passed", "Sharing User group Type not Found for search Opportunity");
                    isUserDisplayed = opportunityDetails.IsSharingUserDisplayedLV(dealTeamMemberExl);
                    Assert.IsTrue(isUserDisplayed);
                    extentReports.CreateStepLogs("Passed", "Sharing User Found for search Opportunity");
                    //Close Sharing Group Popup 
                    opportunityDetails.CloseSharingGroupPopupLV();
                    extentReports.CreateStepLogs("Info", "Sharing Group Popup Closed");
                    randomPages.CloseActiveTab(dndOppName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", "System Admin: " + adminUserExl + "switched to Classic and Loggout ");

                    //Verify Deal Team Member have access to DND opp
                    //16. Again, login as added deal user and verify that these users has access to DND opportunity.
                    extentReports.CreateStepLogs("Info", "Verify Deal Team Member have access to DND opp with DND-name only");
                    //string dealTeamMemberExl = ReadExcelData.ReadDataMultipleRows(excelPath, "InternalTeams", 2, 1);
                    usersLogin.SearchUserAndLogin(dealTeamMemberExl);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(dealTeamMemberExl), true);
                    extentReports.CreateStepLogs("Passed", "Deal Team Member: " + dealTeamMemberExl + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "Deal Team Member: " + dealTeamMemberExl + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View                     
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "Deal Team Member is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    Assert.AreEqual("No record found", opportunityHome.SearchOpportunitiesInLightningView(opportunityName));
                    extentReports.CreateStepLogs("Passed", "Opportunity with old opportunity name after DND Approval is not found ");
                    //Search for DND Approved opportunity with new name
                    result = opportunityHome.UpdateOppAndSearchLV(dndOppName);
                    Assert.AreEqual("Record found", result);
                    extentReports.CreateStepLogs("Passed", result + " with DND Opportunity Name: " + dndOppName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    //8. Now login as any other deal user and verify that deal user has access to DND opportunity.
                    //Verify Login as CAO user can search the DND opp with DND-name only 
                    extentReports.CreateStepLogs("Info", "Verify CAO user can search the DND opp with DND-name only");
                    usersLogin.SearchUserAndLogin(caoUserExl);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(caoUserExl), true);
                    extentReports.CreateStepLogs("Passed", "CAO User: " + caoUserExl + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "CAO User: " + caoUserExl + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View                     
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    result = opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    Assert.AreEqual("No record found", result);
                    extentReports.CreateStepLogs("Passed", result + " with old opportunity name after DND Approval as expected");
                    //Search for DND Approved opportunity with new name
                    result = opportunityHome.UpdateOppAndSearchLV(dndOppName);
                    Assert.AreEqual("Record found", result);
                    extentReports.CreateStepLogs("Passed", result + " with DND Opportunity Name: " + dndOppName);

                    //11. Login as any other user who is not part of deal and verify that he can't access the DND Opportunity.
                    //////////////////////////////////////
                    //12. Now again login as CAO and remove few of the deal users from DND opportunity.
                    extentReports.CreateStepLogs("Info", "CAO User Removing " +dealTeamMemberExl+ " from Internal Deal Team ");
                    string removedDealTeamMemberExl = ReadExcelData.ReadDataMultipleRows(excelPath, "InternalTeams", 2, 1);

                    string msg = opportunityDetails.RemoveUserFromITTeamLV(removedDealTeamMemberExl);
                    Assert.AreEqual(msg, "Success:Staff Roles Updated.");
                    extentReports.CreateStepLogs("Passed", removedDealTeamMemberExl + " Removed from Internal Deal Team ");
                    randomPages.CloseActiveTab(dndOppName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", "CAO User: " + caoUserExl + " switched to Classic and Loggout ");

                    //////////////////////////////////////////////////////////////////////////////
                    //13.Login as removed deal user and check that removed user is no longer access to DND opportunity.
                    extentReports.CreateStepLogs("Info", "Verify Login as removed deal user and check that user is no longer access to DND opportunity ");
                    usersLogin.SearchUserAndLogin(removedDealTeamMemberExl);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(removedDealTeamMemberExl), true);
                    extentReports.CreateStepLogs("Passed", "Deal Team Member: " + removedDealTeamMemberExl + " Internal Deal Team User logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "Deal Team Member: " + removedDealTeamMemberExl + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View                     
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    result = opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    Assert.AreEqual("No record found", result);
                    extentReports.CreateStepLogs("Passed", result + " with old opportunity name after DND Approval");
                    //Search for DND Approved opportunity with new name
                    result = opportunityHome.UpdateOppAndSearchLV(dndOppName);
                    Assert.AreEqual("No record found", result);
                    extentReports.CreateStepLogs("Passed", result + " with DND opportunity name after DND Approval");
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", "Removed Deal Team Member: " + removedDealTeamMemberExl + " switched to Classic and Loggout ");

                    //15.Now navigate to internal team section and add removed deal users once again. (This time change the role of these users compared to last role)
                    //17. Again, login as CAO/SA and verify that readded deal users are appearing in the Sharing section.//
                    //Verify Admin user can search the DND opp with DND-name only and check the Sharing button
                    extentReports.CreateStepLogs("Info", "Verify Admin user can search the DND opp with DND-name only and check the Sharing button and removed Deal Team Member ");
                    usersLogin.SearchUserAndLogin(adminUserExl);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(adminUserExl), true);
                    extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUserExl + "  User logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "System Admin Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for DND Approved opportunity with new name
                    result = opportunityHome.UpdateOppAndSearchLV(dndOppName);
                    Assert.AreEqual("Record found", result);
                    extentReports.CreateStepLogs("Passed", result + " with DND Opportunity Name: " + dndOppName);
                    //Sharing button is available for System Admin Only
                    //Verify group "All Internal Users" on click Sharing button
                    //Validate Only System admin can see the Sharing button
                    isBtnDisplayed = opportunityDetails.IsButtonSharingDisplayedLV();
                    Assert.IsTrue(isBtnDisplayed);
                    extentReports.CreateStepLogs("Passed", "Sharing Button Found and Clicked for System Admin");
                    isGroupDisplayed = opportunityDetails.IsSharingGroupDisplayedLV(ReadExcelData.ReadDataMultipleRows(excelPath, "SharingGroup", 2, 1));
                    Assert.IsFalse(isGroupDisplayed);
                    extentReports.CreateStepLogs("Passed", "Sharing User group not Found for search Opportunity ");
                    //Verify group Type "Entire Organization" on click Sharing button
                    isGroupTypeDisplayed = opportunityDetails.IsSharingGroupDisplayedLV(ReadExcelData.ReadDataMultipleRows(excelPath, "SharingGroup", 2, 2));
                    Assert.IsFalse(isGroupTypeDisplayed);
                    extentReports.CreateStepLogs("Passed", "Sharing User group Type not Found for search Opportunity");
                    //Verify Removed Deal Team is not found on Sharing Pop-Up
                    isUserDisplayed = opportunityDetails.IsSharingUserDisplayedLV(removedDealTeamMemberExl);
                    Assert.IsFalse(isUserDisplayed);
                    extentReports.CreateStepLogs("Passed", "Removed Deal Team Member is not Found for search Opportunity");
                    //Close Sharing Group Popup 
                    opportunityDetails.CloseSharingGroupPopupLV();
                    extentReports.CreateStepLogs("Info", "Sharing Group Popup Closed");
                    randomPages.CloseActiveTab(dndOppName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", "System Admin: " + adminUserExl + " switched to Classic and Loggout ");


                    //7. Convert the DND opportunity into Engagement.
                    extentReports.CreateStepLogs("Info", "Verify Standard user can search the DND opp with DND-name and Request the DND opportunity for Engagement");
                    usersLogin.SearchUserAndLogin(stdUserExl);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(stdUserExl), true);
                    extentReports.CreateStepLogs("Passed", "Standard User: " + stdUserExl + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "Standard User: " + stdUserExl + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View                     
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for DND Approved opportunity with new name
                    result = opportunityHome.UpdateOppAndSearchLV(dndOppName);
                    Assert.AreEqual("Record found", result);
                    extentReports.CreateStepLogs("Passed", result + " with DND Opportunity Name: " + dndOppName);
                    opportunityDetails.ClickRequestToEngL();
                    //Submit Request To Engagement Conversion 
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");
                    login.SwitchToClassicView();
                    //Log out of Standard User
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", "Standard User: " + stdUserExl + " switched to Classic and Loggout ");

                    extentReports.CreateStepLogs("Info", "CAO User: " + caoUserExl + " Approving the Request for Engagement and converting into Engagement ");
                    //Search and Approve the DND Opp
                    usersLogin.SearchUserAndLogin(caoUserExl);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(caoUserExl), true);
                    extentReports.CreateStepLogs("Passed", "CAO User: " + caoUserExl + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "CAO User: " + caoUserExl + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Pass", "User is on " + moduleNameExl + " Page ");
                    //Search for DND Approved opportunity with new name
                    result = opportunityHome.UpdateOppAndSearchLV(dndOppName);
                    Assert.AreEqual("Record found", result);
                    extentReports.CreateStepLogs("Passed", result + " with DND Opportunity Name: " + dndOppName);
                    //Approve the Opportunity 
                    status = opportunityDetails.ClickApproveButtonL();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateLog("Opportunity " + status + " ");
                    opportunityDetails.CloseApprovalHistoryTabL();

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEngagementL2();
                    extentReports.CreateStepLogs("Info", "Opportunity: "+ dndOppName+" Converted into Engagement ");
                    //Validate the Engagement name in Engagement details page
                    string engNumber = engagementDetails.GetEngagementNumberL();
                    extentReports.CreateStepLogs("Info", "Number of Engagement : " + engNumber + " is Same as DND Opportunity name ");
                    string dndEngName = engagementDetails.GetEngagementNameL();
                    extentReports.CreateStepLogs("Info", "Number of Engagement : " + engNumber + " is Same as DND Opportunity name ");
                    Assert.AreEqual(dndOppName, dndEngName);
                    extentReports.CreateStepLogs("Passed", "Name of Engagement : " + dndEngName + " is Same as Opportunity Name : " + dndOppName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", "CAO User: " + caoUserExl + " switched to Classic and Loggout ");

                    ///////////////////////Actions of Engagement page////////////////////////
                    //TMTI0088200	Verify the functionality of internal sharing for DND FVA Engagements


                    //Verify Deal Team Member can access the DND Eng
                    extentReports.CreateStepLogs("Info", "Verify Deal Team Member have access to DND Engagement with DND-name only");
                    string engDealTeamMember = ReadExcelData.ReadDataMultipleRows(excelPath, "InternalTeams", 3, 1);
                    usersLogin.SearchUserAndLogin(engDealTeamMember);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(engDealTeamMember), true);
                    extentReports.CreateStepLogs("Passed", "Deal Team Member: " + user + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "Deal Team Member: " + user + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View                     
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "Deal Team Member is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    Assert.AreEqual("No record found", engagementHome.SearchEngagementInLightningView(opportunityName));
                    extentReports.CreateStepLogs("Passed", "Engagement with old opportunity name after DND Approval is not found ");
                    //Search for DND Approved opportunity with new name
                    result = engagementHome.UpdateEngAndSearchLV(dndEngName);
                    Assert.AreEqual("Record found", result);
                    extentReports.CreateStepLogs("Passed", " Engagement found with DND Opportunity Name: " + dndEngName);
                    randomPages.CloseActiveTab(dndOppName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", "Deal Team Member: " + engDealTeamMember + " switched to Classic and Loggout ");

                    //Verify Non-Deal Team Member can access the DND Eng
                    extentReports.CreateStepLogs("Info", "Verify Non-Deal Team Member( "+ removedDealTeamMemberExl+" ) don't have access to DND Eng with DND-name");
                    usersLogin.SearchUserAndLogin(removedDealTeamMemberExl);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(removedDealTeamMemberExl), true);
                    extentReports.CreateStepLogs("Passed", "Non-Deal Team Member: " + user + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "Non-Deal Team Member: " + user + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View                     
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "Deal Team Member is on " + moduleNameExl + " Page ");
                    Assert.AreEqual("No record found", engagementHome.SearchEngagementInLightningView(opportunityName));
                    extentReports.CreateStepLogs("Passed", "Engagement with old opportunity name after DND Approval is not found ");
                    //Search for DND Approved opportunity with new name
                    result = engagementHome.UpdateEngAndSearchLV(dndEngName);
                    Assert.AreEqual("No record found", result);
                    extentReports.CreateStepLogs("Passed", " Engagement Not found with DND Opportunity Name: " + dndEngName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", "Removed Deal Team Member: " + removedDealTeamMemberExl + " switched to Classic and Loggout ");


                    //Verify Admin user can search the DND opp with DND-name only and check the Sharing button
                    extentReports.CreateStepLogs("Info", "Verify Admin user( "+ adminUserExl+" ) can search the DND Engagement with DND-name only and check the Sharing button");
                    usersLogin.SearchUserAndLogin(adminUserExl);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(adminUserExl), true);
                    extentReports.CreateStepLogs("Passed", "System Admin User: " + user + "  logged in ");

                    extentReports.CreateStepLogs("Info", "Verify Sharing Button button is not availalbe for System Admin ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "System Administrator: "+ user + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Pass", "User is on " + moduleNameExl + " Page ");
                    //Search for DND Approved opportunity with new name
                    result = engagementHome.SearchEngagementInLightningView(dndEngName);
                    Assert.AreEqual("Record found", result);
                    extentReports.CreateStepLogs("Passed", result + " with DND Engagements Name: " + dndOppName);

                    //Sharing button is available for System Admin Only
                    //Verify group "All Internal Users" on click Sharing button
                    //Validate Only System admin can see the Sharing button
                    isBtnDisplayed = opportunityDetails.IsButtonSharingDisplayedLV();
                    Assert.IsTrue(isBtnDisplayed);
                    extentReports.CreateStepLogs("Passed", "Sharing Button Found and Clicked for System Admin");
                    isGroupDisplayed = engagementDetails.IsSharingGroupDisplayedLV(ReadExcelData.ReadDataMultipleRows(excelPath, "SharingGroup", 2, 1));
                    Assert.IsFalse(isGroupDisplayed);
                    extentReports.CreateStepLogs("Passed", "Sharing User group not Found for search Engagements ");
                    //Verify group Type "Entire Organization" on click Sharing button
                    isGroupTypeDisplayed = engagementDetails.IsSharingGroupDisplayedLV(ReadExcelData.ReadDataMultipleRows(excelPath, "SharingGroup", 2, 2));
                    Assert.IsFalse(isGroupTypeDisplayed);
                    extentReports.CreateStepLogs("Passed", "Sharing User group Type not Found for search Engagements");
                    isUserDisplayed = engagementDetails.IsSharingUserDisplayedLV(removedDealTeamMemberExl);
                    Assert.IsFalse(isUserDisplayed);
                    extentReports.CreateStepLogs("Passed", "Sharing User not Found for search Engagements");
                    isUserDisplayed = engagementDetails.IsSharingUserDisplayedLV(engDealTeamMember);
                    Assert.IsTrue(isUserDisplayed);
                    extentReports.CreateStepLogs("Passed", "Sharing User Found for search Engagements");

                    //Close Sharing Group Popup 
                    engagementDetails.CloseSharingGroupPopupLV();
                    extentReports.CreateStepLogs("Info", "Sharing Group Popup Closed");
                    randomPages.CloseActiveTab(dndOppName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", "System Admin User: " + adminUserExl + ": Logged out");
                }
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