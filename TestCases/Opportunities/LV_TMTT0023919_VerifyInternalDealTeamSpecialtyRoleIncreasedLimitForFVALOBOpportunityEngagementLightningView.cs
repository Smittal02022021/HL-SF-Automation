using AventStack.ExtentReports.Gherkin.Model;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Data;

namespace SF_Automation.TestCases.OpportunitiesInternalTeam
{
    class LV_TMTT0023919_VerifyInternalDealTeamSpecialtyRoleIncreasedLimitForFVALOBOpportunityEngagementLightningView:BaseClass
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
        HomeMainPage homePage = new HomeMainPage();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTI0055011 = "LV_TMTI0055011_VerifyInternalDealTeamSpecialtyRoleIncreasedLimitForFVALOBOpportunityEngagement";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyDealTeamSpecialtyRoleOnFVAOppEngManagerPageLV()
        {
            {
                try
                {
                    //Get path of Test data file
                    string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0055011;

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
                        string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                        //Login as Standard User profile and validate the user
                        string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                        homePage.SearchUserByGlobalSearchN(valUser);
                        extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                        //Login user
                        usersLogin.LoginAsSelectedUser();
                        login.SwitchToClassicView();

                        string stdUser = login.ValidateUser();
                        Assert.AreEqual(stdUser.Contains(valUser), true);
                        extentReports.CreateLog("User: " + stdUser + " logged in ");

                        login.SwitchToLightningExperience();
                        extentReports.CreateLog("User: " + stdUser + " Switched to Lightning View ");

                        string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                        homePageLV.SelectAppLV(appNameExl);
                        string appName = homePageLV.GetAppName();
                        Assert.AreEqual(appNameExl, appName);
                        extentReports.CreateLog(appName + " App is selected from App Launcher ");

                        string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                        homePageLV.SelectModule(moduleNameExl);
                        extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                        //Validating Title of New Opportunity Page
                        string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                        Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                        extentReports.CreateLog(driver.Title + " is displayed ");

                        string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType,valJobType, fileTMTI0055011);
                        extentReports.CreateLog("Opportunity : " + opportunityName + " is created ");

                        //Call function to enter Internal Team details and validate Opportunity detail page
                        string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTI0055011);
                        Assert.AreEqual(displayedTab, "Info");
                        extentReports.CreateLog("User is on Opportunity detail " + displayedTab + " tab ");
                        randomPages.CloseActiveTab("Internal Team");
                        //Validating Opportunity with new opportunity number details  
                        string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                        Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                        extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                        //Create External Primary Contact         
                        string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                        string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                        addOpportunityContact.CickAddFVAOpportunityContact();
                        addOpportunityContact.CreateContactL2(fileTMTI0055011);
                        extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                        //Update required Opportunity fields for conversion and Internal team details
                        opportunityDetails.UpdateReqFieldsForFVAConversionLV(fileTMTI0055011);
                        extentReports.CreateLog("Opportunity Required Fields for Converting into Engagement are Filled ");
                        login.SwitchToClassicView();
                        extentReports.CreateLog(stdUser + " Standard User Switched to Classic View ");
                        //Logout of user and validate Admin login
                        usersLogin.UserLogOut();
                        extentReports.CreateLog(stdUser + " Standard User logged out ");

                        extentReports.CreateLog("Admin is Performing Required Actions ");

                        string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 3);
                        extentReports.CreateStepLogs("Info", "System Admin User: " + adminUserExl + " Updating the Required details ");
                       
                        homePage.SearchUserByGlobalSearchN(adminUserExl);
                        extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " details are displayed. ");
                        //Login user
                        usersLogin.LoginAsSelectedUser();
                        login.SwitchToClassicView();
                        string user = login.ValidateUser();
                        Assert.AreEqual(user.Contains(adminUserExl), true);
                        extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUserExl + " User logged in ");
                        opportunityHome.SearchOpportunity(opportunityName);
                        extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                        //update CC and NBC checkboxes 
                        opportunityDetails.UpdateOutcomeDetails(fileTMTI0055011);

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
                        opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTI0055011);
                        extentReports.CreateLog("Opportunity Internal Team Details are provided ");
                        opportunityDetails.ClickReturnToOpportunityLV();
                        extentReports.CreateLog("Return to Opportunity Detail page ");
                        randomPages.CloseActiveTab("Internal Team");
                        //TMTI0055011 Verify the Internal deal team "Specialty" role increased limit for FVA LOB Opportunity
                        //AddMultiple Staff to Internal Deal Team
                        int countDealTeamMember = opportunityDetails.AddOppMultipleDealTeamMembersLV(valRecordType, fileTMTI0055011);
                        extentReports.CreateLog(countDealTeamMember + " Internal Team Members with Role Specialty are added to Opportunity ");

                        string msgActualLimit = opportunityDetails.ValidateDealTeamMemberOverLimitLV();//extra +1
                        string exectedLimitMessage = ReadExcelData.ReadData(excelPath, "OverLimitMessage", 1);
                        Assert.AreEqual(msgActualLimit, exectedLimitMessage);
                        extentReports.CreateLog("Popup with Message: " + msgActualLimit + " is Displayed ");

                        //Get the line error message from internal staff page.
                        string txtLineErrorMessage = opportunityDetails.GetLineErrorMessageLV();
                        string maxMemberLimit = ReadExcelData.ReadData(excelPath, "OverLimitMessage", 2);
                        Assert.IsTrue(txtLineErrorMessage.Contains(maxMemberLimit));
                        extentReports.CreateLog("Line Message: " + txtLineErrorMessage + " is Displayed on header of Opportunity Internal Team Member page ");
                        extentReports.CreateLog("User returned to Opportunity Detail page ");

                        login.SwitchToClassicView();
                        extentReports.CreateLog(stdUser + " Standard User Switched to Classic View ");
                        //Logout of user and validate Admin login
                        usersLogin.UserLogOut();
                        extentReports.CreateLog(stdUser + " Standard User logged out ");
                        homePage.SearchUserByGlobalSearchN(valUser);
                        extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                        //Login user
                        usersLogin.LoginAsSelectedUser();
                        login.SwitchToClassicView();

                        stdUser = login.ValidateUser();
                        Assert.AreEqual(stdUser.Contains(valUser), true);
                        extentReports.CreateLog("User: " + stdUser + " Standard User logged in ");

                        login.SwitchToLightningExperience();
                        extentReports.CreateLog("User: " + stdUser + " Standard User Switched to Lightning View ");
                        homePageLV.SelectAppLV(appNameExl);
                        appName = homePageLV.GetAppName();
                        Assert.AreEqual(appNameExl, appName);
                        extentReports.CreateLog(appName + " App is selected from App Launcher ");
                        homePageLV.SelectModule(moduleNameExl);
                        extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                        //Search for created opportunity
                        opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                        //Requesting for engagement and validate the success message
                        opportunityDetails.ClickRequestToEngL();

                        //Submit Request To Engagement Conversion 
                        string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                        Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                        extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");
                        login.SwitchToClassicView();
                        //Log out of Standard User
                        usersLogin.UserLogOut();
                        //Login as CAO user to approve the Opportunity
                        string userCAOExl = ReadExcelData.ReadData(excelPath, "Users", 2);
                        homePage.SearchUserByGlobalSearchN(userCAOExl);
                        extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " details are displayed. ");
                        //Login user
                        usersLogin.LoginAsSelectedUser();
                        login.SwitchToClassicView();

                        string caoUser = login.ValidateUser();
                        Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                        extentReports.CreateLog("User: " + caoUser + " CAO User logged in ");

                        login.SwitchToLightningExperience();
                        extentReports.CreateLog("User: " + caoUser + " Switched to Lightning View ");

                        //Go to Opportunity module in Lightning View 
                        homePageLV.SelectAppLV(appNameExl);
                        appName = homePageLV.GetAppName();
                        Assert.AreEqual(appNameExl, appName);
                        extentReports.CreateLog(appName + " App is selected from App Launcher ");

                        homePageLV.SelectModule(moduleNameExl);
                        extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                        //Search for created opportunity
                        opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                        //Approve the Opportunity 
                        string status = opportunityDetails.ClickApproveButtonLV2();
                        Assert.AreEqual(status, "Approved");
                        extentReports.CreateLog("Opportunity " + status + " ");
                        opportunityDetails.CloseApprovalHistoryTabL();

                        //Calling function to convert to Engagement
                        opportunityDetails.ClickConvertToEngagementL2();
                        extentReports.CreateLog("Opportunity Converted into Engagement ");

                        //Validate the Engagement name in Engagement details page
                        string engagementNumber = engagementDetails.GetEngagementNumberL();
                        string engagementName = engagementDetails.GetEngagementNameL();

                        //Need to get Name of Opp and Eng
                        Assert.AreEqual(opportunityName, engagementName);
                        extentReports.CreateLog("Name of Engagement : " + engagementName + " is Same as Opportunity name ");

                        //TMTI0055013 Verify the Internal deal team "Specialty" role increased limit for FVA LOB Engagement
                        countDealTeamMember = engagementDetails.AddEngMultipleDealTeamMembersLV(valRecordType, fileTMTI0055011);
                        extentReports.CreateLog(countDealTeamMember + " Internal Team Members with Role Specialty are added to Engagement after conversion after Conversion ");

                        msgActualLimit = opportunityDetails.ValidateDealTeamMemberOverLimitLV();
                        exectedLimitMessage = ReadExcelData.ReadData(excelPath, "OverLimitMessage", 1);
                        Assert.AreEqual(msgActualLimit, exectedLimitMessage);
                        extentReports.CreateLog("Popup with Message: " + msgActualLimit + " is Displayed ");

                        //get the line error message from internal staff page.
                        txtLineErrorMessage = opportunityDetails.GetLineErrorMessageLV();
                        Assert.IsTrue(txtLineErrorMessage.Contains(maxMemberLimit));
                        extentReports.CreateLog("Line Message: " + txtLineErrorMessage + " is Displayed on header of Engagement Internal Team Member page ");

                        login.SwitchToClassicView();
                        usersLogin.UserLogOut();
                        extentReports.CreateLog("User: " + caoUser + " logged out ");
                    }
                    usersLogin.UserLogOut();
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Browser Closed");
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
}
