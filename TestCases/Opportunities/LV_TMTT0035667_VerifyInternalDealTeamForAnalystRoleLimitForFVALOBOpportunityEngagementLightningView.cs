using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.OpportunitiesInternalTeam
{
    internal class LV_TMTT0035667_VerifyInternalDealTeamForAnalystRoleLimitForFVALOBOpportunityEngagementLightningView:BaseClass
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

        public static string fileTMTT0035667 = "TMTT0035667_VerifyInternalDealTeamForAnalystRoleLimitForFVALOBOpportunityEngagement";
        string exectedMaxLimit;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]

        public void  VerifyDealTeamAnalystRoleOnCFOppEngPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0035667;
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
                    login.SwitchToLightningExperience();
                    string stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(stdUserExl), true);
                    extentReports.CreateLog("User: " + stdUserExl + " logged in on Lightning View");
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
                    extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType);
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileTMTT0035667);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");
                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0035667);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Passed", "User is on Opportunity detail " + displayedTab + " tab ");
                    randomPages.CloseActiveTab("Internal Team");
                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                    extentReports.CreateStepLogs("Passed", "Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact      
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    addOpportunityContact.CickAddOpportunityContactLV();
                    addOpportunityContact.CreateContactL2(fileTMTT0035667, valRecordType);
                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForFVAConversionLV(fileTMTT0035667);
                    if (valJobType.Contains("TAS"))
                    {
                        opportunityDetails.UpdateTASServicesLV();
                    }
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();

                    //Login as System Admin user 
                    string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 3, 1);
                    extentReports.CreateStepLogs("Info", "System Admin User: " + adminUserExl + " Updating the Required details ");

                    homePage.SearchUserByGlobalSearchN(adminUserExl);
                    extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();

                    login.SwitchToClassicView();
                    string userAdmin = login.ValidateUser();
                    Assert.AreEqual(userAdmin.Contains(adminUserExl), true);
                    extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUserExl + " User logged in ");

                    login.SwitchToClassicView();
                    opportunityHome.SearchOpportunity(opportunityName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                    //update CC 
                    opportunityDetails.UpdateOutcomeDetails(fileTMTT0035667);
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

                    //////Standard User don't have permission to modify the Internal team so System Admin is modifying the roles////////
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0035667);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab("Internal Team");
                    //TMTT0035667/TMTI0085044	Verify the Internal deal team "Analyst and Associate Roles" role increased limit for FVA LOB Opportunity                
                    //AddMultiple Staff for Specific Role                    
                    string  memberRole = ReadExcelData.ReadDataMultipleRows(excelPath, "Roles", row, 1);
                    exectedMaxLimit = ReadExcelData.ReadDataMultipleRows(excelPath, "OverLimitMessage", row, 2);
                    extentReports.CreateStepLogs("Info", "Verify the Internal deal team limit is increased for FVA LOB Opportunity of Role: " + memberRole + " ");

                    int countOppDealTeamMember = opportunityDetails.AddOppMultipleDealTeamMembersLV(valRecordType, memberRole, fileTMTT0035667);
                    Assert.AreEqual(exectedMaxLimit, countOppDealTeamMember.ToString());
                    extentReports.CreateStepLogs("Pass", countOppDealTeamMember + " Internal Team Members with Role:" + memberRole + " are added to Opportunity ");

                    string msgActualLimit = opportunityDetails.ValidateDealTeamMemberOverLimitLV();//extra +1//Function Updated 
                    string exectedLimitMessage = ReadExcelData.ReadDataMultipleRows(excelPath, "OverLimitMessage", 2, 1);
                    Assert.AreNotEqual(exectedLimitMessage, msgActualLimit);
                    extentReports.CreateStepLogs("Pass", msgActualLimit + " is Displayed after Adding " + countOppDealTeamMember + " deal team members");

                    //get the line error message from internal staff page.
                    string txtLineErrorMessage = opportunityDetails.GetLineErrorMessageLV();//Function Updated
                    string maxMemberLimit = ReadExcelData.ReadDataMultipleRows(excelPath, "OverLimitMessage", 2, 2);
                    Assert.IsFalse(txtLineErrorMessage.Contains(maxMemberLimit));
                    extentReports.CreateStepLogs("Pass", "Line Message: " + txtLineErrorMessage + " is Displayed on header of Opportunity Internal Team Member page ");
                    
                    //opportunityDetails.ClickReturnToOpportunityLV();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Passed", "Admin: " + adminUserExl + "switched to Classic and Loggout ");

                    //Submit Request to Convert opportunity into Engagement.
                    extentReports.CreateStepLogs("Info", "Submit Request to Convert opportunity into Engagement");
                    homePage.SearchUserByGlobalSearchN(stdUserExl);
                    extentReports.CreateStepLogs("Info", "User: " + stdUserExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(stdUserExl), true);
                    extentReports.CreateLog("User: " + stdUserExl + " logged in on Lightning View");

                    //Go to Opportunity module in Lightning View                     
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for DND Approved opportunity with new name
                    string result = opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    Assert.AreEqual("Record found", result);
                    extentReports.CreateStepLogs("Passed", result + " and selected");
                    opportunityDetails.ClickRequestToEngL();
                    //Submit Request To Engagement Conversion 
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");
                    //Log out of Standard User
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Info", "Standard User: " + stdUserExl + " switched to Classic and Loggout ");

                    //Approve and convert the Opporunity into Engagement
                    string caoUserExl = ReadExcelData.ReadData(excelPath, "CAOUser", 1);
                    extentReports.CreateStepLogs("Info", "CAO User: " + caoUserExl + " Approving the Request for Engagement and converting into Engagement ");
                    //Search and Approve the DND Opp
                    homePage.SearchUserByGlobalSearchN(caoUserExl);
                    extentReports.CreateStepLogs("Info", "User: " + caoUserExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(caoUserExl), true);
                    extentReports.CreateStepLogs("Info", "CAO User: " + caoUserExl + " Switched to Lightning View ");

                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Pass", "User is on " + moduleNameExl + " Page ");
                    //Search for DND Approved opportunity with new name
                    result = opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    Assert.AreEqual("Record found", result);
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
                    extentReports.CreateStepLogs("Info", "Number of Engagement : " + engNumber + " is Same as Opportunity number ");
                    string engName = engagementDetails.GetEngagementNameL();
                    Assert.AreEqual(opportunityName, engName);
                    extentReports.CreateStepLogs("Passed", "Name of Engagement : " + engName + " is Same as Opportunity Name : " + opportunityName);
                    
                    /////////////////////////////////////////
                    /////TMTI0085043   Verify the Internal deal team "Analyst and Associate Roles" role increased limit for FVA LOB Engagement
                   
                    int countEngDealTeamMember = engagementDetails.GetInernalTeamMembersCountLV();
                    Assert.AreEqual(exectedMaxLimit, (countEngDealTeamMember).ToString());
                    extentReports.CreateStepLogs("Pass", "Opportunity Deal Team Member : " + (countEngDealTeamMember - 1) + " are Present on Converted Engagement ");
                    engagementDetails.ClickReturnToEngagementLV();
                    extentReports.CreateStepLogs("Info", "Return to Engagement Detail page ");
                    /////////////////////////////////////////////////   
                    randomPages.CloseActiveTab("Internal Team");
                    randomPages.CloseActiveTab(engName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "System Administrator: " + adminUserExl + " Logged out ");
                }
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully ");
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
