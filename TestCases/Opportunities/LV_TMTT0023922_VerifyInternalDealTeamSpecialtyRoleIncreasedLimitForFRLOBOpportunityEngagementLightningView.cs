using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.HomePage;

namespace SF_Automation.TestCases.OpportunitiesInternalTeam
{
    class LV_TMTT0023922_VerifyInternalDealTeamSpecialtyRoleIncreasedLimitForFRLOBOpportunityEngagementLightningView: BaseClass
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

        public static string fileTMTI0055018 = "LV_TMTI0055018_VerifyInternalDealTeamSpecialtyRoleIncreasedLimitForFRLOBOpportunityEngagement";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyDealTeamSpecialtyRoleOnFROppEngManagerPageLV()
        {
            {
                try
                {
                    //Get path of Test data file
                    string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0055018;

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

                        string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileTMTI0055018);
                        extentReports.CreateLog("Opportunity : " + opportunityName + " is created ");                        

                        //Call function to enter Internal Team details and validate Opportunity detail page
                        string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTI0055018);
                        Assert.AreEqual(displayedTab, "Info");
                        extentReports.CreateLog("User is on Opportunity detail " + displayedTab + " tab ");

                        //Validating Opportunity with new opportunity number details  
                        string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                        Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                        extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                        //Create External Primary Contact         
                        string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                        string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                        addOpportunityContact.CickAddFROpportunityContact();
                        addOpportunityContact.CreateContactL2(fileTMTI0055018);
                        extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                        //Update required Opportunity fields for conversion and Internal team details
                        opportunityDetails.UpdateReqFieldsForFRConversionLV(fileTMTI0055018);
                        opportunityDetails.UpdateTotalDebtConfirmedLV();
                        extentReports.CreateLog("Opportunity Required Fields for Converting into Engagement are Filled ");
                        opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTI0055018);
                        extentReports.CreateLog("Opportunity Internal Team Details are provided ");
                        opportunityDetails.ClickReturnToOpportunityLV();
                        extentReports.CreateLog("Return to Opportunity Detail page ");

                        //TMTI0055018		Verify the Internal deal team "Specialty" role increased limit for FR LOB Opportunity
                        //AddMultiple Staff to Internal Deal Team
                        int countDealTeamMember = opportunityDetails.AddOppMultipleDealTeamMembersLV(valRecordType, fileTMTI0055018);
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
                        homePageLV.UserLogoutFromSFLightningView();
                        extentReports.CreateLog(valUser + " Standard User logged out ");
                        extentReports.CreateLog("Admin is Performing Required Actions ");
                        opportunityHome.SearchOpportunity(opportunityName);

                        //update CC and NBC checkboxes 
                        opportunityDetails.UpdateOutcomeDetails(fileTMTI0055018);
                        //Login again as Standard User            
                        homePage.SearchUserByGlobalSearchN(valUser);
                        extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                        //Login user
                        usersLogin.LoginAsSelectedUser();
                        login.SwitchToLightningExperience();
                        stdUser = login.ValidateUserLightningView();
                        Assert.AreEqual(stdUser.Contains(valUser), true);
                        extentReports.CreateLog("User: " + valUser + " logged in on Lightning View");homePageLV.ClickAppLauncher();

                        appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                        homePageLV.SelectApp(appNameExl);
                        appName = homePageLV.GetAppName();
                        Assert.AreEqual(appNameExl, appName);
                        extentReports.CreateLog(appName + " App is selected from App Launcher ");

                        moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
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
                        string userCAO = ReadExcelData.ReadData(excelPath, "Users", 2);
                        //usersLogin.SearchUserAndLogin(userCAO);
                        homePage.SearchUserByGlobalSearchN(userCAO);
                        extentReports.CreateStepLogs("Info", "User: " + userCAO + " details are displayed. ");
                        //Login user
                        usersLogin.LoginAsSelectedUser();

                        login.SwitchToLightningExperience();
                        string user = login.ValidateUserLightningView();
                        Assert.AreEqual(user.Contains(userCAO), true);
                        extentReports.CreateLog("CAO User: " + userCAO + " logged in on Lightning View");

                        //Go to Opportunity module in Lightning View 
                        appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                        homePageLV.SelectAppLV(appNameExl);
                        appName = homePageLV.GetAppName();
                        Assert.AreEqual(appNameExl, appName);
                        extentReports.CreateLog(appName + " App is selected from App Launcher ");

                        moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
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

                        //TMTI0055021		Verify the Internal deal team "Specialty" role increased limit for FR  LOB Engagement
                        countDealTeamMember = engagementDetails.AddEngMultipleDealTeamMembersLV(valRecordType, fileTMTI0055018);
                        extentReports.CreateLog(countDealTeamMember + " Internal Team Members with Role Specialty are added to Engagement after conversion after Conversion ");

                        msgActualLimit = opportunityDetails.ValidateDealTeamMemberOverLimitLV();
                        exectedLimitMessage = ReadExcelData.ReadData(excelPath, "OverLimitMessage", 1);
                        Assert.AreEqual(msgActualLimit, exectedLimitMessage);
                        extentReports.CreateLog("Popup with Message: " + msgActualLimit + " is Displayed ");

                        //get the line error message from internal staff page.
                        txtLineErrorMessage = opportunityDetails.GetLineErrorMessageLV();
                        Assert.IsTrue(txtLineErrorMessage.Contains(maxMemberLimit));
                        extentReports.CreateLog("Line Message: " + txtLineErrorMessage + " is Displayed on header of Engagement Internal Team Member page ");

                        homePageLV.UserLogoutFromSFLightningView();
                        extentReports.CreateLog("User: " + userCAO + " logged out ");
                    }
                    usersLogin.UserLogOut();
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Browser Closed");
                }
                catch (Exception e)
                {
                    extentReports.CreateExceptionLog(e.Message);
                    homePageLV.UserLogoutFromSFLightningView();
                    usersLogin.UserLogOut();
                    driver.Quit();
                }
            }
        }
    }
}
