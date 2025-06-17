using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using NUnit.Framework;
using SF_Automation.TestData;
using System;
using SF_Automation.Pages.HomePage;

namespace SF_Automation.TestCases.OpportunitiesInternalTeam
{
    class LV_TMTT0035666_VerifyInternalDealTeamForAnalystAssociateRoleLimitForFRLOBOpportunityEngagementLightningView:BaseClass
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
        RandomPages randomPages= new RandomPages();

        public static string fileTMTT0035666 = "LV_TMTT0035666_VerifyInternalDealTeamForAnalystRoleLimitForFRLOBOpportunityEngagement";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyDealTeamAnalystRoleOnFROppEngPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0035666;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                //extentReports.CreateStepLogs("Info", "Verify Internal Deal Team For Analyst Role Limit For CF LOB Opportunity & Engagement ");
                int rowRole = ReadExcelData.GetRowCount(excelPath, "Roles");
                for (int row = 2; row <= rowRole; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 25);
                    //Login as Standard User profile and validate the user
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                    homePage.SearchUserByGlobalSearchN(valUser);
                    extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("User: " + valUser + " logged in on Lightning View");

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

                    string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileTMTT0035666);
                    extentReports.CreateLog("Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0035666);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateLog("User is on Opportunity detail " + displayedTab + " tab ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact         
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    addOpportunityContact.CickAddFROpportunityContact();
                    addOpportunityContact.CreateContactL2(fileTMTT0035666, valRecordType);
                    extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForFRConversionLV(fileTMTT0035666);
                    opportunityDetails.UpdateTotalDebtConfirmedLV();                    

                    extentReports.CreateLog("Opportunity Required Fields for Converting into Engagement are Filled ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0035666);
                    extentReports.CreateLog("Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityL();
                    extentReports.CreateLog("Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab("Internal Team");

                    //PitchMandateAward details
                    randomPages.ClickPitchMandteAwardTabLV();
                    opportunityDetails.CreateNewPitchMandateAwardLV();
                    extentReports.CreateStepLogs("Info", "New Pitch/Mandate Award detail provided ");
                    string idPMA = opportunityDetails.GetPitchMandateAwardID();
                    randomPages.CloseActiveTab(idPMA + " | Pitch/Mandate Award");

                    //AddMultiple Staff 
                    string memberRole = ReadExcelData.ReadDataMultipleRows(excelPath, "Roles", row, 1);
                    string exectedMaxLimit = ReadExcelData.ReadDataMultipleRows(excelPath, "OverLimitMessage", row, 2);
                    extentReports.CreateStepLogs("Info", " Internal Team Members Limit with Role:" + memberRole + " on  Opportunity ");

                    //TMTI0085042 Verify the Internal deal team "Analyst and Associate Roles" role increased limit for FR LOB Opportunity
                    int countOppDealTeamMember = opportunityDetails.AddOppMultipleDealTeamMembersLV(valRecordType, memberRole, fileTMTT0035666);//updated multiselect user function,SwitchTO
                    Assert.AreEqual(exectedMaxLimit, countOppDealTeamMember.ToString());
                    extentReports.CreateStepLogs("Pass", countOppDealTeamMember + " Internal Team Members with Role:" + memberRole + " are added to Opportunity ");

                    string msgActualLimit = opportunityDetails.ValidateDealTeamMemberOverLimitLV();//extra +1
                    string expectedLimitMessage = ReadExcelData.ReadData(excelPath, "OverLimitMessage", 1);
                    Assert.AreNotEqual(expectedLimitMessage, msgActualLimit);
                    extentReports.CreateLog("Popup with Message: " + msgActualLimit + " is Displayed ");

                    //Get the line error message from internal staff page.
                    string txtLineErrorMessage = opportunityDetails.GetLineErrorMessageLV();
                    string maxMemberLimit = ReadExcelData.ReadData(excelPath, "OverLimitMessage", 2);
                    Assert.IsFalse(txtLineErrorMessage.Contains(maxMemberLimit));
                    extentReports.CreateLog("Line Message: " + txtLineErrorMessage + " is Displayed on header of Opportunity Internal Team Member page ");
                    extentReports.CreateLog("User returned to Opportunity Detail page ");
                    
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateLog(valUser + " Standard User logged out ");
                    extentReports.CreateLog("Admin is Performing Required Actions ");
                    opportunityHome.SearchOpportunity(opportunityName);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(fileTMTT0035666);

                    //Login again as Standard User
                    homePage.SearchUserByGlobalSearchN(valUser);
                    extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("User: " + valUser + " logged in on Lightning View");

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
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateLog(valUser + " Standard User logged out ");

                    //Login as CAO user to approve the Opportunity
                    string userCAO = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);
                    homePage.SearchUserByGlobalSearchN(userCAO);
                    extentReports.CreateStepLogs("Info", "User: " + userCAO + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(userCAO), true);
                    extentReports.CreateLog("User: " + userCAO + " logged in on Lightning View");

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
                    opportunityDetails.ClickConvertToEngagementL();
                    extentReports.CreateLog("Opportunity Converted into Engagement ");
                    //Validate the Engagement name in Engagement details page
                    string engagementNumber = engagementDetails.GetEngagementNumberL();
                    string engagementName = engagementDetails.GetEngagementNameL();
                    //Need to get Name of Opp and Eng
                    Assert.AreEqual(opportunityName, engagementName);
                    extentReports.CreateLog("Name of Engagement : " + engagementName + " is Same as Opportunity name ");

                    //////////////////Need to above below function on eng page 
                    int countEngDealTeamMember = engagementDetails.GetInernalTeamMembersCountLV();
                    Assert.AreEqual(exectedMaxLimit, (countEngDealTeamMember - 1).ToString());
                    extentReports.CreateStepLogs("Pass", "Opportunity Deal Team Member : " + (countEngDealTeamMember - 1) + " are Present on Converted Engagement ");
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateLog("CAO User: " + userCAO + " logged out ");
                    extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
                }
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
