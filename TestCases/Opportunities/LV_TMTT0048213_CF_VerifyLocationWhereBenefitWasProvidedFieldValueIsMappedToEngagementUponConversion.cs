using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using AventStack.ExtentReports.Gherkin.Model;
using System.Collections.Generic;

namespace SF_Automation.TestCases.OpportunitiesConversion
{
    class LV_TMTT0048213_CF_VerifyLocationWhereBenefitWasProvidedFieldValueIsMappedToEngagementUponConversion:BaseClass
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

        public static string fileTMTT0048213 = "LV_TMTT0048213_VerifyLocationWhereBenefitWasProvidedFieldValueIsMappedToEngagementUponConversionCF";
        private string locationBenefit;
        private string valBenefitExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0118691 Verify that the "Location where Benefit was Provided" field is required when user request an engagement.
        //TMTI0118696 Verify that the "Location where Benefit was Provided" field having value is not listed in the yellow list of required fields when user request an engagement.
        //TMTI0118698 Verify that the user is able to update the "Location where Benefit was Provided" field value and successfully request an engagement.
        //TMTI0118700 Verify that the "Location where Benefit was Provided" field value is mapped to an engagement upon conversion.
        //TMTI0118702 Verify that the "Location where Benefit was Provided" field is editable on the engagement by the following user
        //TMTI0118706 Verify that the "Location where Benefit was Provided" field is not editable by non - deal Team Members.


        [Test]
        public void VerifyLocationWhereBenefitWasProvidedFieldValueIsMappedToEngagementCFLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0048213;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");
                
                int rowOpp = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                for (int row = 2; row <= rowOpp; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                    extentReports.CreateStepLogs("Info", "Creating Opportunity for : " + valJobType + " ");
                    //Login as Standard User profile and validate the user
                    string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
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
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                    //Validating Title of New Opportunity Page
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectCFOpp();
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                    extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");                                        
                    extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType);
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTMTT0048213);//updated move to jobtype
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0048213);
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
                    addOpportunityContact.CreateContactL2(fileTMTT0048213, valRecordType);
                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");
                    
                    //TMTI0118691 Verify that the "Location where Benefit was Provided" field is required when user request an engagement
                    opportunityDetails.ClickRequestToEngL();
                    string txtActualRequiredFieldsValidation = opportunityDetails.GetActualRequiredFieldsValidationForConversionLV();
                    string txtExpectedRequiredFieldsValidation = ReadExcelData.ReadDataMultipleRows(excelPath, "Validation", 2, 1);
                    Assert.IsTrue(txtActualRequiredFieldsValidation.Contains(txtExpectedRequiredFieldsValidation));
                    extentReports.CreateStepLogs("Passed", "'Location where Benefit was Provided' field is required when user request an engagement ");

                    //TMTI0118696	Verify that the "Location where Benefit was Provided" field having value is not listed in the yellow list of required fields when user request an engagement.
                    valBenefitExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Validation", 2, 2);
                    opportunityDetails.InlineUpdateLocationBenefitValueLV(valBenefitExl);

                    opportunityDetails.ClickRequestToEngL();
                    txtActualRequiredFieldsValidation = opportunityDetails.GetActualRequiredFieldsValidationForConversionLV();
                    Assert.IsFalse(txtActualRequiredFieldsValidation.Contains(txtExpectedRequiredFieldsValidation));
                    extentReports.CreateStepLogs("Passed", "'Location where Benefit was Provided' field having value is not listed in the yellow list of required fields when user request an engagement");
                    
                    //Get Location where Benefit is to be Provided value to validate it on converted Engagement
                    locationBenefit = opportunityDetails.GetValueLocationBenefitLV();
                    extentReports.CreateStepLogs("Info", "Location where Benefit is Provided value is: " + locationBenefit);

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForCFConversionWithoutLocationBenefitLV(fileTMTT0048213, valJobType);//udated Move to element
                    extentReports.CreateStepLogs("Info", "Location where Benefit was Provided value filled ");
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0048213);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    randomPages.CloseActiveTab("Internal Team");
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", valUser + " Standard User logged out ");

                    //System Admin Performin required actions
                    string adminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 2, 1);
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
                    
                    //Requesting for engagement and validate the success message
                    opportunityDetails.ClickRequestToEngL();
                    //Submit Request To Engagement Conversion 
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();

                    //Login as CAO user to approve the Opportunity
                    string userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 3, 1);
                    homePage.SearchUserByGlobalSearchN(userCAOExl);
                    extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    extentReports.CreateStepLogs("Passed", "User: " + userCAOExl + " logged in on Lightning View");
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                    //TMTI0055402 Verify the availability of Job Types for converted engagement on the Engagement page
                    //Search for created opportunity &Approve the Opportunity 
                    opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                    string status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Passed", "Opportunity " + status + " ");
                    opportunityDetails.CloseApprovalHistoryTabL();

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEngagementL2();
                    extentReports.CreateStepLogs("Info", "Opportunity Converted into Engagement ");
                    //Validate the Engagement name in Engagement details page
                    string engagementNumber = engagementDetails.GetEngagementNumberL();
                    string engagementName = engagementDetails.GetEngagementNameL();
                    //Need to get Name of Opp and Eng
                    Assert.AreEqual(opportunityName, engagementName);
                    extentReports.CreateStepLogs("Passed", "Name of Engagement : " + engagementName + " is Same as Opportunity name ");
                    randomPages.CloseActiveTab(engagementName);

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    engagementHome.SearchEngagementInLightningView(engagementName);
                    
                    // TMTI0118700	Verify that the "Location where Benefit was Provided" field value is mapped to an engagement upon conversion.
                    Assert.AreEqual(locationBenefit, engagementDetails.GetValueLocationBenefitLV());
                    extentReports.CreateStepLogs("Passed", "Location where Benefit is to be Provided field is mapped from opoortunity on converted Engagement");

                    //TMTI0118702	Verify that the "Location where Benefit was Provided" field is editable on the engagement by the following user
                    extentReports.CreateStepLogs("Info", "Verify CAO can Edit the Location where Benefit was Provided” field");
                    //Code to check editable field for Benefit 
                    Assert.IsTrue(engagementDetails.IsInlineEditLocationBenefitButtonPresentLV(), "Verify Inline Edit Location where Benefit was Provided icon is present ");
                    extentReports.CreateStepLogs("Passed", "Inline Edit Location where Benefit was Provided icon is present ");
                    // update Location where Benefit was Provided value
                    valBenefitExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Validation", 3, 2);
                    engagementDetails.InlineUpdateLocationBenefitValueLV(valBenefitExl);
                    Assert.AreEqual(valBenefitExl, opportunityDetails.GetValueLocationBenefitLV());
                    extentReports.CreateStepLogs("Info", "Location where Benefit is Provided value is Editable for CAO user and updated to : " + valBenefitExl);
                    randomPages.CloseActiveTab(engagementName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " logged out ");


                    //TMTI0118702	Verify that the "Location where Benefit was Provided" field is editable on the engagement by the following user
                    //Verify Deal Team member can Edit the Location where Benefit was Provided” field
                    extentReports.CreateStepLogs("Info", "Verify Deal Team member can Edit the Location where Benefit was Provided” field");
                    string valStaff = ReadExcelData.ReadData(excelPath, "AddOpportunity", 14);
                    homePage.SearchUserByGlobalSearchN(valStaff);
                    extentReports.CreateStepLogs("Info", "User: " + valStaff + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(valStaff), true);
                    extentReports.CreateStepLogs("Passed", "Deal team Member: " + valStaff + " logged in on Lightning View");
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Deal team Member is on " + moduleNameExl + " Page ");
                    engagementHome.SearchEngagementInLightningView(engagementName);

                    //Code to check editable field for Benefit 
                    Assert.IsTrue(engagementDetails.IsInlineEditLocationBenefitButtonPresentLV(), "Verify Inline Edit Location where Benefit was Provided icon is present ");
                    extentReports.CreateStepLogs("Passed", "Inline Edit Location where Benefit was Provided icon is present ");
                    // update Location where Benefit was Provided value
                    valBenefitExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Validation", 2, 2);
                    engagementDetails.InlineUpdateLocationBenefitValueLV(valBenefitExl);
                    Assert.AreEqual(valBenefitExl, opportunityDetails.GetValueLocationBenefitLV());
                    extentReports.CreateStepLogs("Info", "Location where Benefit is Provided value is Editable for Deal Team Member and updated to : " + valBenefitExl);
                    randomPages.CloseActiveTab(engagementName);

                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "Deal team Member:: " + valStaff + " logged out ");
                    ///////////////////

                    extentReports.CreateStepLogs("Info", "Verify Data Hygiene User can Edit the Location where Benefit was Provided” field");
                    string userDataHygieneExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 4, 1);
                    homePage.SearchUserByGlobalSearchN(userDataHygieneExl);
                    extentReports.CreateStepLogs("Info", " Data Hygiene User: " + userDataHygieneExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(userDataHygieneExl), true);
                    extentReports.CreateStepLogs("Passed", "Data Hygiene User: " + userDataHygieneExl + " logged in on Lightning View");
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Data Hygiene User is on " + moduleNameExl + " Page ");
                    engagementHome.SearchEngagementInLightningView(engagementName);
                    Assert.IsFalse(engagementDetails.IsInlineEditLocationBenefitButtonPresentLV(), "Verify Inline Edit Location where Benefit was Provided icon isn't present ");
                    extentReports.CreateStepLogs("Passed", "Inline Edit Location where Benefit was Provided icon is not present and field is not editable ");
                    randomPages.CloseActiveTab(engagementName);

                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "Data Hygiene User: " + userDataHygieneExl + " logged out ");
                    /////////////////////

                    extentReports.CreateStepLogs("Info", "Verify Compliance User can Edit the Location where Benefit was Provided” field");
                    string userComplianceExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 5, 1);
                    homePage.SearchUserByGlobalSearchN(userComplianceExl);
                    extentReports.CreateStepLogs("Info", " Compliance User: " + userComplianceExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(userComplianceExl), true);
                    extentReports.CreateStepLogs("Passed", "Compliance User: " + userComplianceExl + " logged in on Lightning View");
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Compliance User is on " + moduleNameExl + " Page ");
                    engagementHome.SearchEngagementInLightningView(engagementName);

                    //Code to check editable field for Benefit 
                    Assert.IsTrue(engagementDetails.IsInlineEditLocationBenefitButtonPresentLV(), "Verify Inline Edit Location where Benefit was Provided icon is present ");
                    extentReports.CreateStepLogs("Passed", "Inline Edit Location where Benefit was Provided icon is present ");
                    // update Location where Benefit was Provided value
                    valBenefitExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Validation", 2, 2);
                    engagementDetails.InlineUpdateLocationBenefitValueLV(valBenefitExl);
                    Assert.AreEqual(valBenefitExl, opportunityDetails.GetValueLocationBenefitLV());
                    extentReports.CreateStepLogs("Info", "Location where Benefit is Provided value is Editable for Compliance user and updated to : " + valBenefitExl);
                    randomPages.CloseActiveTab(engagementName);

                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "Compliance User: " + userDataHygieneExl + " logged out ");
                    ////////////////////
                    ///
                    extentReports.CreateStepLogs("Info", "Verify System Adminitrator User can Edit the Location where Benefit was Provided” field");
                    homePage.SearchUserByGlobalSearchN(adminUser);
                    extentReports.CreateStepLogs("Info", " Compliance User: " + adminUser + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(adminUser), true);
                    extentReports.CreateStepLogs("Passed", "System Adminitrator User: " + adminUser + " logged in on Lightning View");
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "System Adminitrator User is on " + moduleNameExl + " Page ");
                    engagementHome.SearchEngagementInLightningView(engagementName);

                    //Code to check editable field for Benefit 
                    Assert.IsTrue(engagementDetails.IsInlineEditLocationBenefitButtonPresentLV(), "Verify Inline Edit Location where Benefit was Provided icon is present ");
                    extentReports.CreateStepLogs("Passed", "Inline Edit Location where Benefit was Provided icon is present ");
                    // update Location where Benefit was Provided value
                    valBenefitExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Validation", 3, 2);
                    engagementDetails.InlineUpdateLocationBenefitValueLV(valBenefitExl);
                    Assert.AreEqual(valBenefitExl, opportunityDetails.GetValueLocationBenefitLV());
                    extentReports.CreateStepLogs("Info", "Location where Benefit is Provided value is Editable for System Administrator and updated to : " + valBenefitExl);
                    randomPages.CloseActiveTab(engagementName);

                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "System Adminitrator User: " + adminUser + " logged out ");

                    ////////////////////////**************************     ///
                    //TMTI0118706	Verify that the "Location where Benefit was Provided" field is not editable by non - deal Team Members.
                    string userNonDeal = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 6, 1);
                    extentReports.CreateStepLogs("Info", "Verify that the 'Service Benefit Location' field is required for Engagement creation for all LOBs and map its value upon engagement creation");
                    homePage.SearchUserByGlobalSearchN(userNonDeal);
                    extentReports.CreateStepLogs("Info", " Compliance User: " + userNonDeal + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string user1 = login.ValidateUserLightningView();
                    Assert.AreEqual(user1.Contains(userNonDeal), true);
                    extentReports.CreateStepLogs("Passed", "System Adminitrator User: " + userNonDeal + " logged in on Lightning View");
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "System Adminitrator User is on " + moduleNameExl + " Page ");
                    engagementHome.SearchEngagementInLightningView(engagementName);

                    //Code to check editable field for Benefit 
                    Assert.IsTrue(engagementDetails.IsInlineEditLocationBenefitButtonPresentLV(), "Verify Inline Edit Location where Benefit was Provided icon is present ");
                    extentReports.CreateStepLogs("Passed", "Inline Edit Location where Benefit was Provided icon is present ");
                    // update Location where Benefit was Provided value
                    valBenefitExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Validation", 3, 2);
                    engagementDetails.InlineUpdateLocationBenefitValueLV(valBenefitExl);
                    string expectedListValidationErrors = ReadExcelData.ReadDataMultipleRows(excelPath, "Validation", 3, 1);
                    string actualListValidationErrors = engagementDetails.GetPageLevelAccessWarningLV();

                    Assert.AreEqual(expectedListValidationErrors, actualListValidationErrors);
                    extentReports.CreateStepLogs("Info", "'Location where Benefit is Provided' is not editable for Non-Deal Team Member with error message '" + actualListValidationErrors);
                    randomPages.CloseActiveTab(engagementName);

                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "Non-Deal Team Member User: " + userNonDeal + " logged out ");

                    ////************************************//////
                    ///


                }
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
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
