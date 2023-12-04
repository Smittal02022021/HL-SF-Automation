using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Security.Cryptography;
using AventStack.ExtentReports.Gherkin.Model;

namespace SalesForce_Project.TestCases.Opportunities
{
    class LV_TMTT0036859_VerifyTheFunctionalityOfDNDEngagementSharingFeatureForCFLOBOpportunityAndEngagementOnLightningView:BaseClass
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
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTMTT0036859 = "TMTT0036859_VerifyTheFunctionalityOfDNDEngagementSharingFeatureForCFLOBOpportunityAndEngagementOnLightningView";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]

        public void VerifySharingFeatureForCFDNDBOpportunityAndEngagementOnLightningView()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0036859;

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
                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                    extentReports.CreateStepLogs("Info", "Creating Opportunity for : " + valJobType + " ");

                    //Login as Standard User profile and validate the user
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "User", 2,1);
                    usersLogin.SearchUserAndLogin(valUser);
                    login.SwitchToClassicView();
                    string stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateStepLogs("Pass", "Standard User: " + stdUser + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateLog("User: " + stdUser + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Pass", appName + " App is selected from App Launcher ");
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Validating Title of New Opportunity Page
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                    extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");
                    extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType);
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTMTT0036859);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0036859);
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
                    addOpportunityContact.CreateContactL2(fileTMTT0036859);
                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForCFConversionLV2(fileTMTT0036859);//udated Move to element
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0036859);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickRetutnToOpportunityLV();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                    //Add Multipe users with Different Role on created opportunity
                    displayedTab = addOpportunity.EnterMembersToDealTeamL(fileTMTT0036859);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Pass", "More Users are added in Internal Team ");

                    //Validate the DND On/Off button for Std User
                    bool isButtonDisplayed = opportunityDetails.IsButtonDNDOnOffDisplayed();
                    Assert.IsFalse(isButtonDisplayed);
                    extentReports.CreateStepLogs("Pass", "DND On/Off button is not displayed for Standard User: " + stdUser);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Pass", "User: " + stdUser + "switched to Classic and Loggout ");

                    //////////////////Verify Sharing Button button is not availalbe for CAO User but visible for Admin////////////////////// 
                    extentReports.CreateStepLogs("Pass", "Verify Sharing Button/Option is available for System Admin only(Instead od CAO User) ");
                    //Login as System Admin user 
                    usersLogin.SearchUserAndLogin("Indrajeet Singh");
                    login.SwitchToClassicView();
                    string adminUser = login.ValidateUser();
                    Assert.AreEqual(adminUser.Contains("Indrajeet Singh"), true);
                    extentReports.CreateStepLogs("Pass", "System Admin User: " + adminUser + " User logged in ");

                    login.SwitchToClassicView();
                    opportunityHome.SearchOpportunity(opportunityName);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(fileTMTT0036859);
                    if (valJobType.Equals("Buyside") || valJobType.Equals("Sellside"))
                    {
                        opportunityDetails.UpdateNBCApproval();
                        extentReports.CreateLog("Conflict Check and NBC fields are updated ");
                    }
                    else
                    {
                        extentReports.CreateLog("Conflict Check fields are updated ");
                    }
                    /////////////////////////////////////////////////////////////////////
                    //TMTI0088201 Verify the functionality of internal sharing for DND and Non DND Opportunity
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Pass", "System Admin Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Pass", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    //Validate the DND On/Off button for Admin User Only
                    isButtonDisplayed = opportunityDetails.IsButtonDNDOnOffDisplayed();
                    Assert.IsTrue(isButtonDisplayed);
                    extentReports.CreateStepLogs("Pass", "DND On/Off button is displayed for System Admin ");
                    //Validate Only System admin can see the Sharing button
                    bool isBtnDisplayed = opportunityDetails.IsButtonSharingDisplayedL();
                    Assert.IsTrue(isBtnDisplayed);
                    extentReports.CreateStepLogs("Pass", "Sharing Button Found and Clicked by System Admin" );
                    //Verify group "All Internal Users" on click Sharing button
                    bool isGroupDisplayed= opportunityDetails.IsSharingUserGroupDisplayedLV(ReadExcelData.ReadDataMultipleRows(excelPath, "SharingGroup", 2, 1));
                    Assert.IsTrue(isGroupDisplayed);
                    extentReports.CreateStepLogs("Pass", "Sharing User group Found is displayed for System Admin");
                    //Verify group Type "Entire Organization" on click Sharing button
                    bool isGroupTypeDisplayed = opportunityDetails.IsSharingUserGroupDisplayedLV(ReadExcelData.ReadDataMultipleRows(excelPath, "SharingGroup", 2, 2));
                    Assert.IsTrue(isGroupTypeDisplayed);
                    extentReports.CreateStepLogs("Pass", "Sharing User group type Found is displayed for System Admin");
                    bool isUserDisplayed = opportunityDetails.IsSharingUserDisplayedLV("James Craven");
                    Assert.IsTrue(isUserDisplayed);
                    extentReports.CreateStepLogs("Pass", "Deal Team Member Found on Sharing Group Pop-up");
                    //Close Sharing Group Popup 
                    opportunityDetails.CloseSharingGroupPopupLV();
                    extentReports.CreateStepLogs("Info", "Sharing Group Popup Closed");
                    login.SwitchToClassicView();                    
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", "System Admin Logged Out");

                    extentReports.CreateStepLogs("Info", "Verify DND On/Off button with CAO User ");
                    //Validate the DND On/Off buton with CAO User
                    //Login as CAO user
                    string caoUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "User", 3, 1);
                    usersLogin.SearchUserAndLogin(caoUserExl);
                    login.SwitchToClassicView();

                    string caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(caoUserExl), true);
                    extentReports.CreateStepLogs("Pass", "CAO User: " + caoUser + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Pass", "CAO User Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Pass", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);                    
                    isButtonDisplayed = opportunityDetails.IsButtonDNDOnOffDisplayed();
                    Assert.IsTrue(isButtonDisplayed);
                    extentReports.CreateStepLogs("Pass", "DND On/Off button is displayed for CAO user:  : " + caoUser);
                    opportunityDetails.ClickDNDOnOffButtonL();
                    extentReports.CreateStepLogs("Info", "CAO User: "+ caoUser + "Clicked on DND On/Off Button ");
                    string txtMessage = randomPages.GetLVMessagePopup();
                    extentReports.CreateStepLogs("Pass", txtMessage);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Pass", "User: " + caoUser + "switched to Classic and Loggout ");

                    /////////////////////////////////////////////////////////////////////////////
                    extentReports.CreateStepLogs("Info", "Login and Approve the DND Request with DND Approval Q user ");
                    //Login as user from group DND Approval Q
                    string userDNDApproverExl = ReadExcelData.ReadDataMultipleRows(excelPath, "User", 4, 1);
                    usersLogin.SearchUserAndLogin(userDNDApproverExl);
                    login.SwitchToClassicView();
                    string userApproverDND = login.ValidateUser();
                    Assert.AreEqual(userApproverDND.Contains(userDNDApproverExl), true);
                    extentReports.CreateStepLogs("Pass", "DND Approver User: " + userApproverDND + "  logged in ");

                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "DND Approver User: " + userApproverDND + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    //Approve the Opportunity 
                    string status = opportunityDetails.ClickApproveButtonL();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Pass", "Opportunity " + status + " ");
                    opportunityDetails.CloseApprovalHistoryTabL();
                    //Get OppName after DND approved
                    string dndOppName = opportunityDetails.GetOpportunityNameL();
                    extentReports.CreateStepLogs("Pass", opportunityDetails.ValidateOpportunityNameL(dndOppName));
                    randomPages.CloseActiveTab(dndOppName);
                    //Search for DND Approved opportunity with new name
                    string updatedOpp = opportunityHome.UpdateOppAndSearchLV(dndOppName);
                    Assert.AreEqual("Record found", updatedOpp);
                    extentReports.CreateStepLogs("Pass", updatedOpp + " with DND Opportunity Name: " + dndOppName);
                    randomPages.CloseActiveTab(dndOppName);  
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Pass", "DND Approver User:: " + userApproverDND + "switched to Classic and Loggout ");

                    /////////////////////////////////////////////////////////////////////////////////
                    //Verify Admin user can search the DND opp with DND-name only and check the Sharing button
                    extentReports.CreateStepLogs("Pass", "Verify Admin user can search the DND opportunity with DND-name only and check the Sharing button");
                    usersLogin.SearchUserAndLogin("Indrajeet Singh");
                    login.SwitchToClassicView();
                    adminUser = login.ValidateUser();
                    Assert.AreEqual(adminUser.Contains("Indrajeet Singh"), true);
                    extentReports.CreateStepLogs("Pass", "System Admin User: " + adminUser + "  logged in ");

                    extentReports.CreateStepLogs("Info", "Verify Sharing Button button is not availalbe for System Admin ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Pass", "System Admin Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Pass", "User is on " + moduleNameExl + " Page ");
                    //Search for DND Approved opportunity with new name
                    updatedOpp = opportunityHome.UpdateOppAndSearchLV(dndOppName);
                    Assert.AreEqual("Record found", updatedOpp);
                    extentReports.CreateStepLogs("Pass", updatedOpp + " with DND Opportunity Name: " + dndOppName);

                    //Sharing button is available for System Admin Only
                    //Verify group "All Internal Users" on click Sharing button
                    //Validate Only System admin can see the Sharing button
                    isBtnDisplayed = opportunityDetails.IsButtonSharingDisplayedL();
                    Assert.IsTrue(isBtnDisplayed);
                    extentReports.CreateStepLogs("Pass", "Sharing Button Found and Clicked for System Admin");
                    isGroupDisplayed = opportunityDetails.IsSharingUserGroupDisplayedLV(ReadExcelData.ReadDataMultipleRows(excelPath, "SharingGroup", 2, 1));
                    Assert.IsFalse(isGroupDisplayed);
                    extentReports.CreateStepLogs("Pass", "Sharing User group not Found for search Opportunity ");
                    //Verify group Type "Entire Organization" on click Sharing button
                    bool isGroupTypeDisplayed1 = opportunityDetails.IsSharingUserGroupDisplayedLV(ReadExcelData.ReadDataMultipleRows(excelPath, "SharingGroup", 2, 2));
                    Assert.IsFalse(isGroupTypeDisplayed1);
                    extentReports.CreateStepLogs("Pass", "Sharing User group Type not Found for search Opportunity");
                    isUserDisplayed = opportunityDetails.IsSharingUserDisplayedLV("James Craven");
                    Assert.IsTrue(isUserDisplayed);
                    extentReports.CreateStepLogs("Pass", "Sharing User not Found for search Opportunity");
                    //Close Sharing Group Popup 
                    opportunityDetails.CloseSharingGroupPopupLV();
                    extentReports.CreateStepLogs("Info", "Sharing Group Popup Closed");
                    randomPages.CloseActiveTab(dndOppName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();

                    //Verify Deal Team Member have access to DND opp
                    //16. Again, login as added deal user and verify that these users has access to DND opportunity.
                    extentReports.CreateStepLogs("Info", "Verify Deal Team Member have access to DND opp with DND-name only");
                    string dealTeamMember = ReadExcelData.ReadDataMultipleRows(excelPath, "InternalTeams", 2, 1);
                    usersLogin.SearchUserAndLogin(dealTeamMember);
                    login.SwitchToClassicView();
                    string user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(dealTeamMember), true);
                    extentReports.CreateStepLogs("Pass", "Deal Team Member: " + user + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "Deal Team Member: " + user + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View                     
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "Deal Team Member is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    Assert.AreEqual("No record found", opportunityHome.SearchOpportunitiesInLightningView(opportunityName));
                    extentReports.CreateStepLogs("Pass", "Opportunity with old opportunity name after DND Approval is not found ");
                    //Search for DND Approved opportunity with new name
                    updatedOpp = opportunityHome.UpdateOppAndSearchLV(dndOppName);
                    Assert.AreEqual("Record found", updatedOpp);
                    extentReports.CreateStepLogs("Pass", " Opportunity found with DND Opportunity Name: " + dndOppName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    //8. Now login as any other deal user and verify that deal user has access to DND opportunity.
                    //Verify Login as CAO user can search the DND opp with DND-name only 
                    extentReports.CreateStepLogs("Info", "Verify CAO user can search the DND opp with DND-name only");                    
                    usersLogin.SearchUserAndLogin(caoUserExl);
                    login.SwitchToClassicView();
                    caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(caoUserExl), true);
                    extentReports.CreateStepLogs("Pass", "CAO User: " + caoUser + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "CAO User: " + caoUser + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View                     
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    string result = opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    Assert.AreEqual("No record found", result);
                    extentReports.CreateStepLogs("Pass", result + " with old opportunity name after DND Approval as expected");
                    //Search for DND Approved opportunity with new name
                    updatedOpp = opportunityHome.UpdateOppAndSearchLV(dndOppName);
                    Assert.AreEqual("Record found", updatedOpp);
                    extentReports.CreateStepLogs("Pass", updatedOpp + " with DND Opportunity Name: " + dndOppName);

                    //11. Login as any other user who is not part of deal and verify that he can't access the DND Opportunity.
                    //////////////////////////////////////
                    //12. Now again login as CAO and remove few of the deal users from DND opportunity.
                    extentReports.CreateStepLogs("Info", "CAO User Removing James Craven from Internal Deal Team ");
                    string msg= opportunityDetails.RemoveUserFromITTeamLV(dealTeamMember);
                    Assert.AreEqual(msg, "Success:Staff Roles Updated.");
                    extentReports.CreateStepLogs("Pass", dealTeamMember+" Removed from Internal Deal Team ");
                    randomPages.CloseActiveTab(dndOppName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();

                    //////////////////////////////////////////////////////////////////////////////
                    //13.Login as removed deal user and check that removed user is no longer access to DND opportunity.
                    extentReports.CreateStepLogs("Info", "Verify Login as removed deal user and check that user is no longer access to DND opportunity ");
                    usersLogin.SearchUserAndLogin(dealTeamMember);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(dealTeamMember), true);
                    extentReports.CreateStepLogs("Pass", "Deal Team Member: " + user + " Internal Deal Team User logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "Deal Team Member: " + user + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View                     
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    result = opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    Assert.AreEqual("No record found", result);
                    extentReports.CreateStepLogs("Pass", result + " with old opportunity name after DND Approval");
                    //Search for DND Approved opportunity with new name
                    updatedOpp = opportunityHome.UpdateOppAndSearchLV(dndOppName);
                    Assert.AreEqual("No record found", updatedOpp);
                    extentReports.CreateStepLogs("Pass", updatedOpp + " with DND opportunity name after DND Approval" );
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();

                    //15.Now navigate to internal team section and add removed deal users once again. (This time change the role of these users compared to last role)
                    //17. Again, login as CAO/SA and verify that readded deal users are appearing in the Sharing section.//
                    //Verify Admin user can search the DND opp with DND-name only and check the Sharing button
                    extentReports.CreateStepLogs("Pass", "Verify Admin user can search the DND opp with DND-name only and check the Sharing button and removed Deal Team Member ");
                    usersLogin.SearchUserAndLogin("Indrajeet Singh");
                    login.SwitchToClassicView();
                    adminUser = login.ValidateUser();
                    Assert.AreEqual(adminUser.Contains("Indrajeet Singh"), true);
                    extentReports.CreateStepLogs("Pass", "System Admin User: " + adminUser + "  User logged in ");                    
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Pass", "System Admin Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Pass", "User is on " + moduleNameExl + " Page ");
                    //Search for DND Approved opportunity with new name
                    updatedOpp = opportunityHome.UpdateOppAndSearchLV(dndOppName);
                    Assert.AreEqual("Record found", updatedOpp);
                    extentReports.CreateStepLogs("Pass", updatedOpp + " with DND Opportunity Name: " + dndOppName);
                    //Sharing button is available for System Admin Only
                    //Verify group "All Internal Users" on click Sharing button
                    //Validate Only System admin can see the Sharing button
                    isBtnDisplayed = opportunityDetails.IsButtonSharingDisplayedL();
                    Assert.IsTrue(isBtnDisplayed);
                    extentReports.CreateStepLogs("Pass", "Sharing Button Found and Clicked for System Admin");
                    isGroupDisplayed = opportunityDetails.IsSharingUserGroupDisplayedLV(ReadExcelData.ReadDataMultipleRows(excelPath, "SharingGroup", 2, 1));
                    Assert.IsFalse(isGroupDisplayed);
                    extentReports.CreateStepLogs("Pass", "Sharing User group not Found for search Opportunity ");
                    //Verify group Type "Entire Organization" on click Sharing button
                    isGroupTypeDisplayed = opportunityDetails.IsSharingUserGroupDisplayedLV(ReadExcelData.ReadDataMultipleRows(excelPath, "SharingGroup", 2, 2));
                    Assert.IsFalse(isGroupTypeDisplayed);
                    extentReports.CreateStepLogs("Pass", "Sharing User group Type not Found for search Opportunity");
                    //Verify Removed Deal Team is not found on Sharing Pop-Up
                    isUserDisplayed = opportunityDetails.IsSharingUserDisplayedLV("James Craven");
                    Assert.IsFalse(isUserDisplayed);
                    extentReports.CreateStepLogs("Pass", "Removed Deal Team Member is not Found for search Opportunity");
                    //Close Sharing Group Popup 
                    opportunityDetails.CloseSharingGroupPopupLV();
                    extentReports.CreateStepLogs("Info", "Sharing Group Popup Closed");
                    randomPages.CloseActiveTab(dndOppName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();

                    
                     //7. Convert the DND opportunity into Engagement.
                     extentReports.CreateStepLogs("Info", "Verify Standard user can search the DND opp with DND-name and Request the DND opportunity for Engagement");                    
                    usersLogin.SearchUserAndLogin(valUser);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(valUser), true);
                    extentReports.CreateStepLogs("Pass", "Standard User: " + user + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "Standard User: " + user + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View                     
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for DND Approved opportunity with new name
                    updatedOpp = opportunityHome.UpdateOppAndSearchLV(dndOppName);
                    Assert.AreEqual("Record found", updatedOpp);
                    extentReports.CreateStepLogs("Pass", updatedOpp + " with DND Opportunity Name: " + dndOppName);
                    opportunityDetails.ClickRequestToEngL();
                    //Submit Request To Engagement Conversion 
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");
                    login.SwitchToClassicView();
                    //Log out of Standard User
                    usersLogin.UserLogOut();


                    //Search and Approve the DND Opp
                    usersLogin.SearchUserAndLogin(caoUserExl);
                    login.SwitchToClassicView();
                    caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(caoUserExl), true);
                    extentReports.CreateStepLogs("Pass", "CAO User: " + caoUser + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Pass", "CAO User Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Pass", "User is on " + moduleNameExl + " Page ");
                    //Search for DND Approved opportunity with new name
                    updatedOpp = opportunityHome.UpdateOppAndSearchLV(dndOppName);
                    Assert.AreEqual("Record found", updatedOpp);
                    extentReports.CreateStepLogs("Pass", updatedOpp + " with DND Opportunity Name: " + dndOppName);
                    //Approve the Opportunity 
                    status = opportunityDetails.ClickApproveButtonL();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateLog("Opportunity " + status + " ");
                    opportunityDetails.CloseApprovalHistoryTabL();

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEngagementL2();
                    extentReports.CreateStepLogs("Info","Opportunity Converted into Engagement ");
                    //Validate the Engagement name in Engagement details page
                    string engNumber = engagementDetails.GetEngagementNumberL();
                    extentReports.CreateStepLogs("Info","Number of Engagement : " + engNumber + " is Same as DND Opportunity name ");
                    string dndEngName = engagementDetails.GetEngagementNameL();
                    extentReports.CreateStepLogs("Info","Number of Engagement : " + engNumber + " is Same as DND Opportunity name ");
                    Assert.AreEqual(dndOppName, dndEngName);
                    extentReports.CreateStepLogs("Pass","Name of Engagement : " + dndEngName + " is Same as Opportunity Name : "+ dndOppName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();

                    ///////////////////////Actions of Engagement page////////////////////////
                    ///TMTI0088202	Verify the functionality of internal sharing for DND Engagements

                    //Verify Deal Team Member can access the DND Eng
                    extentReports.CreateStepLogs("Info", "Verify Deal Team Member have access to DND Eng with DND-name only");
                    string engDealTeamMember = ReadExcelData.ReadDataMultipleRows(excelPath, "InternalTeams", 3, 1);
                    usersLogin.SearchUserAndLogin(engDealTeamMember);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(engDealTeamMember), true);
                    extentReports.CreateStepLogs("Pass", "Deal Team Member: " + user + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "Deal Team Member: " + user + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View                     
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule("Engagements");// moduleNameExl
                    extentReports.CreateStepLogs("Info", "Deal Team Member is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    Assert.AreEqual("No record found", engagementHome.SearchEngagementInLightningView(opportunityName));
                    extentReports.CreateStepLogs("Pass", "Engagement with old opportunity name after DND Approval is not found ");
                    //Search for DND Approved opportunity with new name
                    string updatedEng = engagementHome.UpdateEngAndSearchLV(dndEngName);
                    Assert.AreEqual("Record found", updatedEng);
                    extentReports.CreateStepLogs("Pass", " Engagement found with DND Opportunity Name: " + dndEngName);
                    randomPages.CloseActiveTab(dndOppName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();

                    //Verify Non-Deal Team Member can access the DND Eng
                    extentReports.CreateStepLogs("Info", "Verify Non-Deal Team Member don't have access to DND Eng with DND-name");
                    string nonDealTeamMember = ReadExcelData.ReadDataMultipleRows(excelPath, "InternalTeams", 2, 1);
                    usersLogin.SearchUserAndLogin(nonDealTeamMember);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(nonDealTeamMember), true);
                    extentReports.CreateStepLogs("Pass", "Deal Team Member: " + user + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "Deal Team Member: " + user + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View                     
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule("Engagements");// moduleNameExl
                    extentReports.CreateStepLogs("Info", "Deal Team Member is on " + moduleNameExl + " Page ");
                    Assert.AreEqual("No record found", engagementHome.SearchEngagementInLightningView(opportunityName));
                    extentReports.CreateStepLogs("Pass", "Engagement with old opportunity name after DND Approval is not found ");
                    //Search for DND Approved opportunity with new name
                    updatedEng = engagementHome.UpdateEngAndSearchLV(dndEngName);
                    Assert.AreEqual("No record found", updatedEng);
                    extentReports.CreateStepLogs("Pass", " Engagement Not found with DND Opportunity Name: " + dndEngName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();


                    //Verify Admin user can search the DND opp with DND-name only and check the Sharing button
                    extentReports.CreateStepLogs("Pass", "Verify Admin user can search the DND opportunity with DND-name only and check the Sharing button");
                    usersLogin.SearchUserAndLogin("Indrajeet Singh");
                    login.SwitchToClassicView();
                    adminUser = login.ValidateUser();
                    Assert.AreEqual(adminUser.Contains("Indrajeet Singh"), true);
                    extentReports.CreateStepLogs("Pass", "System Admin User: " + adminUser + "  logged in ");

                    extentReports.CreateStepLogs("Info", "Verify Sharing Button button is not availalbe for System Admin ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Pass", "System Admin Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule("Engagements");// moduleNameExl
                    extentReports.CreateStepLogs("Pass", "User is on " + moduleNameExl + " Page ");
                    //Search for DND Approved opportunity with new name
                    updatedOpp = engagementHome.SearchEngagementInLightningView(dndEngName);//UpdateEngAndSearchLV(dndEngName);
                    Assert.AreEqual("Record found", updatedOpp);
                    extentReports.CreateStepLogs("Pass", updatedOpp + " with DND Engagements Name: " + dndOppName);

                    //Sharing button is available for System Admin Only
                    //Verify group "All Internal Users" on click Sharing button
                    //Validate Only System admin can see the Sharing button
                    isBtnDisplayed = opportunityDetails.IsButtonSharingDisplayedL();
                    Assert.IsTrue(isBtnDisplayed);
                    extentReports.CreateStepLogs("Pass", "Sharing Button Found and Clicked for System Admin");
                    isGroupDisplayed = engagementDetails.IsSharingUserGroupDisplayedLV(ReadExcelData.ReadDataMultipleRows(excelPath, "SharingGroup", 2, 1));
                    Assert.IsFalse(isGroupDisplayed);
                    extentReports.CreateStepLogs("Pass", "Sharing User group not Found for search Engagements ");
                    //Verify group Type "Entire Organization" on click Sharing button
                    isGroupTypeDisplayed = engagementDetails.IsSharingUserGroupDisplayedLV(ReadExcelData.ReadDataMultipleRows(excelPath, "SharingGroup", 2, 2));
                    Assert.IsFalse(isGroupTypeDisplayed);
                    extentReports.CreateStepLogs("Pass", "Sharing User group Type not Found for search Engagements");
                    isUserDisplayed = engagementDetails.IsSharingUserDisplayedLV(nonDealTeamMember);
                    Assert.IsFalse(isUserDisplayed);
                    extentReports.CreateStepLogs("Pass", "Sharing User not Found for search Engagements");
                    isUserDisplayed = engagementDetails.IsSharingUserDisplayedLV(engDealTeamMember);
                    Assert.IsTrue(isUserDisplayed);
                    extentReports.CreateStepLogs("Pass", "Sharing User Found for search Engagements");

                    //Close Sharing Group Popup 
                    engagementDetails.CloseSharingGroupPopupLV();
                    extentReports.CreateStepLogs("Info", "Sharing Group Popup Closed");
                    randomPages.CloseActiveTab(dndOppName);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();



                    /*
                        8. Now login as any other deal user and verify that deal user has access to DND Engagement.
                        9. Again, login as System admin/CAO and check the sharing for DND Engagement.
                        10. Verify that "All Internal users" group is removed from DND Engagement.
                        11. Login as any other user who is not part of deal and verify that he can't access the DND Engagement.
                        12. Now again login as CAO and remove few of the deal users from DND Engagement.
                        13. Login as removed deal user and check that removed user is no longer access to DND Engagement.
                        14. Again, login as CAO/SA and check the sharing that removed deal user is also removed from Sharing.
                        15. Now navigate to internal team section and add removed deal users once again. (This time change the role of these users compared to last role)
                    */
                }
            }
            catch(Exception e)
            {
                extentReports.CreateLog(e.Message);
                login.SwitchToClassicView();
                driver.Quit();
            }
        }
    }
}
