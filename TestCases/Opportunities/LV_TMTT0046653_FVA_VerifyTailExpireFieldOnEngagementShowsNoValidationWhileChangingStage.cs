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
    class LV_TMTT0046653_VerifyTailExpireFieldOnFVAEngagementShowsNoValidationWhileChangingStage:BaseClass
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

        public static string fileTMTT0046653 = "LV_TMTT0046653_VerifyTailExpireFieldOnFVAEngagementNoValidationWhileChangingStage";
        private string userCAOExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        //TMTI0114212 Verify that Tail Expire Field on FVA Engagement is not seen and able to change Stage to Dead without Tail Expire Field validation check.
        //TMTI0114214 Verify that Tail Expire Field on FVA Engagement is not seen and able to change Stage to Hold without Tail Expire Field validation check.

        [Test]
        public void VerifyTailExpireFieldShowsNoValidationWhileChangingStageFVA()
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
                int user = ReadExcelData.GetRowCount(excelPath, "StandardUsers");

                //for (int rowUser = 2; rowUser <= user; rowUser++)                {

                for (int row = 2; row <= rowOpp; row++)
                {
                    for (int rowStg = 2; rowStg <= rowStage; rowStg++)
                    {
                        string valStage = ReadExcelData.ReadDataMultipleRows(excelPath, "Stage", rowStg, 1);
                        string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                        string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                        extentReports.CreateStepLogs("Info", "Creating Opportunity for : " + valJobType + " ");

                        //Login as Standard User profile and validate the user
                        string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "StandardUsers", row, 1);
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
                        string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
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

                        //Update required Opportunity fields for conversionfor CF/FR/FVA and Internal team details                            
                        
                        //if (valRecordType == "FVA")
                        //{
                            opportunityDetails.UpdateReqFieldsForFVAConversionLV(fileTMTT0046653);
                        //}
                        //opportunityDetails.UpdateReqFieldsForConversionLV(fileTMTT0046653, valJobType, valRecordType);

                        extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                        randomPages.CloseActiveTab(opportunityName);
                        homePageLV.LogoutFromSFLightningAsApprover();
                        extentReports.CreateStepLogs("Info", valUser + " Standard User logged out ");
                        ///////////---------------------------///////////

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
                        opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0046653);
                        extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                        opportunityDetails.ClickReturnToOpportunityLV();
                        randomPages.CloseActiveTab("Internal Team");
                        extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                        homePageLV.LogoutFromSFLightningAsApprover();
                        extentReports.CreateStepLogs("Info", adminUser + " System Administrator logged out ");

                        ///////////--------------------------///////////////

                        //Login as CF Fin user to request opp to convert into eng.
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

                        /////-------------------//////

                        //Login as CAO user to approve the Opportunity
                        userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", row, 1);
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

                        //Search for created opportunity & Approve the Opportunity 
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
                        extentReports.CreateLog("User is on " + moduleNameExl + " Page ");
                        engagementHome.GlobalSearchEngagementInLightningView(engagementName);

                        //TMTI0114212	Verify that Tail Expire Field on FVA Engagement is not seen and able to change Stage to Dead without Tail Expire Field validation check.
                        //TMTI0114214 Verify that Tail Expire Field on FVA Engagement is not seen and able to change Stage to Hold without Tail Expire Field validation check.

                        string stage = engagementDetails.GetStageLV();
                        engagementDetails.EnterCommentsForStageChangeLV(valStage);
                        extentReports.CreateStepLogs("Passed", "Tail Expires Required field's validation Not displayed while changing the stage");
                        stage = engagementDetails.GetStageLV();
                        Assert.AreEqual(valStage, stage, "Verify that Stage is updated to Dead after filling All required fields");
                        extentReports.CreateStepLogs("Passed", "FR Engagement having Job type as " + valJobType + " Stage changed to " + stage + " without Tail Expire field validation");
                        randomPages.CloseActiveTab(engagementName);
                        randomPages.ReloadPage();
                        randomPages.CloseActiveTab(engagementName);
                        homePageLV.LogoutFromSFLightningAsApprover();
                        extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " logged out ");
                    }
                }

                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                homePageLV.LogoutFromSFLightningAsApprover();
                driver.Quit();
            }
        }
    }
}
