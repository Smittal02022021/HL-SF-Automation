using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.OpportunitiesConversion
{
    class LV_CF_TMTT0046653_2_VerifyTailExpireFieldOnVerballyEngagementValidationWhileChangingStage:BaseClass
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

        public static string fileTMTT0046653 = "LV_TMTT0046653_VerifyTailExpireFieldOnCFEngagementwithJobTypeShowsValidationWhileChangingStage";
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0114203	Verify that Tail Expire Field on CF Engagement shows validation while changing Stage to Dead having "Verbally Engaged" checked.
        //TMTI0114206	Verify that Tail Expire Field on CF Engagement shows validation while changing Stage to Hold having "Verbally Engaged" checked.

        [Test]
        public void VerifyTailExpireFieldOnCFVerballyEngagementValidationWhileChangingStage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0046653;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");
                int rowStage = ReadExcelData.GetRowCount(excelPath, "Stage");
                int rowOpp = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                
                    for (int rowStg = 2; rowStg < rowStage; rowStg++)
                    {
                        string valStage = ReadExcelData.ReadDataMultipleRows(excelPath, "Stage", rowStg, 1);
                        string valJobType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 3);
                        string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                        extentReports.CreateStepLogs("Info", "Creating Opportunity for : " + valJobType + " ");
                        //Login as Standard User profile and validate the user
                        string valUser = ReadExcelData.ReadData(excelPath, "StandardUsers", 1);
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

                        string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileTMTT0046653); ;
                        extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                        //Call function to enter Internal Team details and validate Opportunity detail page
                        string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0046653);
                        Assert.AreEqual(displayedTab, "Info");
                        extentReports.CreateStepLogs("Passed", "User is on Opportunity detail " + displayedTab + " tab ");

                        //Validating Opportunity details  
                        string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                        extentReports.CreateStepLogs("Passed", "Opportunity with number : " + opportunityNumber + " is created ");

                        //Create External Primary Contact      
                        string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                        string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                        string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                        addOpportunityContact.CickAddOpportunityContactLV();
                        addOpportunityContact.CreateContactL2(fileTMTT0046653, valRecordType);
                        extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");
                        extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                        opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0046653);
                        extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                        opportunityDetails.ClickReturnToOpportunityLV();
                        randomPages.CloseActiveTab("Internal Team");
                        extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");                      
                        randomPages.CloseActiveTab(opportunityName);
                        homePageLV.LogoutFromSFLightningAsApprover();
                        extentReports.CreateStepLogs("Info", valUser + " Standard User logged out ");
                        ///////////---------------------------///////////
                        ///

                        //System Admin Performin required actions
                        string adminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 3, 1);
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
                        extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                        //Search for created opportunity
                        opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                        extentReports.CreateStepLogs("Info", "Admin is Performing Required Actions ");
                        //opportunityHome.SearchOpportunity(opportunityName);
                        //update CC and NBC checkboxes 
                        opportunityDetails.UpdateOutcomeNBCApproveDetailsLV(valJobType);
                        homePageLV.LogoutFromSFLightningAsApprover();
                        extentReports.CreateStepLogs("Info", adminUser + " System Administrator logged out ");

                        ///////////--------------------------///////////////

                        //Login as CF Fin user to Enter Required fields on opp for Verbally Engaged.
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
                    
                        ////*******Verbally Enaged Actions********////
                        opportunityDetails.EnterVerballyEngagedRequiredFieldsLV(valJobType, fileTMTT0046653);
                        extentReports.CreateStepLogs("Info", "Entered All Field level Required values for Verbally Engage");
                        string stageExl = ReadExcelData.ReadData(excelPath, "Stage", 3);
                        string stage = opportunityDetails.GetStageLV();
                        opportunityDetails.EditOpportunityStageLV(stageExl);
                        string updatedStage = opportunityDetails.GetStageLV();
                        Assert.AreEqual(updatedStage, stageExl);
                        extentReports.CreateStepLogs("Passed", "Opportunity Stage is updated from " + stage + " to " + updatedStage);
                        Assert.IsTrue(randomPages.GetVerballyEngCheckboxStatusLV(), "Verify Verbally Engaged checkbox is Checked after stage change of the Opportunity to Verbally Engaged");
                        extentReports.CreateStepLogs("Passed", "Verbally Engaged checkbox is Checked after stage change of the Opportunity to Verbally Engaged");

                        Assert.IsTrue(opportunityDetails.IsVerballyEngagedEngCreatedLV(opportunityName), "Verify changing stage to Verbally Engaged creates a Partial Engagement");
                        extentReports.CreateStepLogs("Passed", "Changing stage to Verbally Engaged creates a Partial Engagement");
                        opportunityDetails.ClickVerballyEngagedEngagementNumberLV(opportunityName); 
                        randomPages.CloseActiveTab(opportunityName);
                        homePageLV.LogoutFromSFLightningAsApprover();
                        extentReports.CreateStepLogs("Info", "CF Financial User: "+valUser + " logged out ");

                        /////-------------------//////

                        //Login as CAO user on Verball Engaged Engagement
                        string userCAOExl = ReadExcelData.ReadData(excelPath, "CAOUsers", 1);
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
                        moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                        homePageLV.SelectModule(moduleNameExl);
                        extentReports.CreateLog("User is on " + moduleNameExl + " Page ");
                        engagementHome.GlobalSearchEngagementInLightningView(opportunityName);

                        //TMTI0114203	Verify that Tail Expire Field on CF Engagement shows validation while changing Stage to Dead having "Verbally Engaged" checked.
                        //TMTI0114206	Verify that Tail Expire Field on CF Engagement shows validation while changing Stage to Hold having "Verbally Engaged" checked.

                        stage = engagementDetails.GetStageLV();
                        engagementDetails.EditEngagementStageLV(valStage);
                        Assert.IsTrue(engagementDetails.IsValidationDisplayedLV(), "Verify the Required field's validation while changing the stage");
                        extentReports.CreateStepLogs("Passed", "Required field's validation displayed while changing the stage");
                        string actualListValidationErrors = engagementDetails.GetFieldLevelValidationErrorsLV();
                        string expectedListValidationErrors = ReadExcelData.ReadDataMultipleRows(excelPath, "ValidationList", rowStg, 1);
                        Assert.AreEqual(expectedListValidationErrors, actualListValidationErrors, "Verify that validation appears when user try to change the stage to '" + rowStg + "'");
                        extentReports.CreateStepLogs("Passed", "Validations appeared when user try to change the stage to '"+ valStage + "' of Verbally Engaged");
                        Assert.IsTrue(actualListValidationErrors.Contains("Tail Expires"), "Verify that Tail Expire Field on CF Engagement having Job type as " + valJobType + "shows validation while changing Stage to " + stage);
                        extentReports.CreateStepLogs("Passed", "CF Engagement having Job type: " + valJobType + " Tail Expire Field's validation on shows while changing Stage to '" + valStage + "'");
                        //enter required fields as per stage
                        engagementDetails.EntertStageChangeReqValuesLV(valStage);
                        string msgBubble = randomPages.GetPopUpMessagelV();
                        extentReports.CreateStepLogs("Info", "New Stage saved with Success Message: " + msgBubble);
                        stage = engagementDetails.GetStageLV();
                        Assert.AreEqual(valStage, stage, "Verify that Stage is updated to Dead after filling All required fields");
                        extentReports.CreateStepLogs("Passed", "CF Engagement having Job type as " + valJobType + " Stage changed to " + stage + " after filling required including Tail Expire ");
                        randomPages.CloseActiveTab(opportunityName);
                        homePageLV.LogoutFromSFLightningAsApprover();
                        extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " logged out ");
                    }
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
