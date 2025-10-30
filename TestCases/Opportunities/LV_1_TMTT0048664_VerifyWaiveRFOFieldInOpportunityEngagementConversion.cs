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
    class LV_1_TMTT0048664_VerifyWaiveRFOFieldInOpportunityEngagementConversion:BaseClass
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

        public static string fileTMTC0036135 = "LV_TMTT0048664_VerifyWaiveRFOFieldInOpportunityEngagementCF";        
        private bool isWaiveRFOFieldEditable=false;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0119866 Verify that "Waive RFO" field is added and displayed in "Fees and Financials" tab-> "Estimated Fees" section while creating a new Opportunity. 
        //TMTI0119892 Verify that Financial user cannot edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section while creating a new Opportunity
        //TMTI0119901 Verify that CAO can edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in newly converted opportunity to  Engagement
        //TMTI0119903 Verify that System Admin can edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in in newly converted opportunity to  Engagement
        //TMTI0119905 Verify that Financial user cannot edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in newly converted opportunity to  Engagement.
        
        [Test]
        public void VerifyWaiveRFOFieldInCFOpportunity_Engagement()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0036135;
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

                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                extentReports.CreateStepLogs("Info", "Creating Opportunity for : " + valJobType + " ");
                //Login as Standard User profile and validate the user
                string stdUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearchN(stdUser);
                extentReports.CreateStepLogs("Info", "User: " + stdUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(stdUser), true);
                extentReports.CreateStepLogs("Passed", "User: " + stdUser + " logged in on Lightning View");
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

                // TMTI0119866	Verify that "Waive RFO" field is added and displayed in "Fees and Financials" tab-> "Estimated Fees" section while creating a new Opportunity. 
                bool IsWaiveRFOFieldFound = randomPages.IsWaiveRFOFieldPresentLV();
                Assert.IsTrue(IsWaiveRFOFieldFound, "Verify that 'Waive RFO' field is added and displayed in 'Fees and Financials' tab-> 'Estimated Fees' section while creating a new Opportunity");
                extentReports.CreateStepLogs("Passed", "'Waive RFO' field is added and displayed in 'Fees and Financials' tab-> 'Estimated Fees' section while creating a new Opportunity");

                //TMTI0119892	Verify that Financial user cannot edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section while creating a new Opportunity

                isWaiveRFOFieldEditable = randomPages.IsWaiveRFOFieldEditableLV();
                Assert.IsFalse(isWaiveRFOFieldEditable, "Verify CF Financial user cannot edit 'Waive RFO' field is editable in 'Fees and Financials' tab-> 'Estimated Fees' section while creating a new Opportunity");
                extentReports.CreateStepLogs("Passed", "CF Financial user cannot edit 'Waive RFO' field while creating a new Opportunity");



                extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType);
                string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTMTC0036135);//updated move to jobtype
                extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTC0036135);
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
                addOpportunityContact.CreateContactL2(fileTMTC0036135, valRecordType);
                extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                //Update required Opportunity fields for conversion and Internal team details
                opportunityDetails.UpdateReqFieldsForCFConversionLV2(fileTMTC0036135, valJobType);//udated Move to element
                extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTC0036135);
                extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                opportunityDetails.ClickReturnToOpportunityLV();
                randomPages.CloseActiveTab("Internal Team");
                extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", stdUser + " CF Financial User logged out ");

                ///////////---------------------------///////////

                //System Admin Performin required actions
                string adminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 4, 1);
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

                ///////////--------------------------///////////////

                //Login as CF Fin user to request opp to convert into eng.
                homePage.SearchUserByGlobalSearchN(stdUser);
                extentReports.CreateStepLogs("Info", "User: " + stdUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(stdUser), true);
                extentReports.CreateStepLogs("Passed", "User: " + stdUser + " logged in on Lightning View");

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
                extentReports.CreateStepLogs("Info", stdUser + " CF Financial User logged out ");

                //Login as CAO user to approve the Opportunity
                string userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
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

                //TMTI0119907	Verify that the "Waive RFO" field value which is checked is mapped upon converting an opportunity to Engagement
                //CAO Udating WaiveRFO
                opportunityDetails.ClickTabOppFeeAndFincnciaLV();
                randomPages.UpdateWaiveRFOLV();
                opportunityDetails.ClickTabOppFeeAndFincnciaLV();
                bool isOppWaiveRFOCheckboxSelected= randomPages.GetWaiveRFOStatusLV();
                extentReports.CreateStepLogs("Passed", "CAO Updated Waive RFO checkbox selection : "+ isOppWaiveRFOCheckboxSelected);


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
                CustomFunctions.PageReload(driver);
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                //TMTI0119901	Verify that CAO can edit the "Waive RFOAngeles$2025
                //" field in "Fees and Financials" tab-> "Estimated Fees" section in newly converted opportunity to  Engagement
                engagementHome.GlobalSearchEngagementInLightningView(engagementName);

                //CAO verifying WaiveRFO status mapping from opportunity 
                engagementDetails.ClickTabEngFeeAndFincnciaLV();
                bool isEngWaiveRFOCheckboxSelected = randomPages.GetWaiveRFOStatusLV();
                Assert.AreEqual(isEngWaiveRFOCheckboxSelected, isOppWaiveRFOCheckboxSelected);
                extentReports.CreateStepLogs("Passed", "Engagement Waive RFO checkbox selection is same as from Opportunity");
                //engagementDetails.ClickTabInfoLV();
                
                //on Detail Page
                //engagementDetails.ClickTabEngFeeAndFincnciaLV();
                isWaiveRFOFieldEditable=randomPages.IsWaveRFPInlineEditableLV();
                Assert.IsTrue(isWaiveRFOFieldEditable, "Verify that CAO user can Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in newly converted opportunity to  Engagement");
                extentReports.CreateStepLogs("Passed", "CAO User can Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in newly converted opportunity to  Engagement");
                engagementDetails.ClickTabInfoLV();

                //On Edit form 
                engagementDetails.ClickEditEngagementLV();
                isWaiveRFOFieldEditable = randomPages.IsWaiveRFOFieldEditableLV();
                Assert.IsTrue(isWaiveRFOFieldEditable, "Verify that CAO can edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in newly converted opportunity to  Engagement");
                extentReports.CreateStepLogs("Passed", "CAO can edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in newly converted opportunity to  Engagement");
                randomPages.CancelEditFormLV();
                randomPages.CloseActiveTab(engagementName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", userCAOExl + " CAO User logged out ");

                //TMTI0119903	Verify that System Admin can edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in in newly converted opportunity to  Engagement
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
                engagementHome.GlobalSearchEngagementInLightningView(engagementName);

                engagementDetails.ClickTabEngFeeAndFincnciaLV();
                isWaiveRFOFieldEditable=randomPages.IsWaveRFPInlineEditableLV();
                Assert.IsTrue(isWaiveRFOFieldEditable, "Verify that Admin user cannot Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in newly converted opportunity to  Engagement");
                extentReports.CreateStepLogs("Passed", "Admin User can Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in newly converted opportunity to  Engagement");
                engagementDetails.ClickTabInfoLV();

                engagementDetails.ClickEditEngagementLV();
                isWaiveRFOFieldEditable = randomPages.IsWaiveRFOFieldEditableLV();
                Assert.IsTrue(isWaiveRFOFieldEditable, "Verify that Admin User can edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in newly converted opportunity to  Engagement");
                extentReports.CreateStepLogs("Passed", "Admin User can edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in newly converted opportunity to  Engagement");
                randomPages.CancelEditFormLV();
                randomPages.CloseActiveTab(engagementName);
                randomPages.CloseActiveTab(engagementName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", adminUser + " System Administrator logged out ");

                //TMTI0119905	Verify that Financial user cannot edit the "Waive RFO" field in "Fees and Financials" tab-> "Estimated Fees" section in newly converted opportunity to  Engagement. 
                homePage.SearchUserByGlobalSearchN(stdUser);
                extentReports.CreateStepLogs("Info", "CF Financial User: " + stdUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(stdUser), true);
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + stdUser + " logged in on Lightning View");                
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "CF Financial User is on " + moduleNameExl + " Page ");
                engagementHome.GlobalSearchEngagementInLightningView(engagementName);
                
                //On Detail Page
                engagementDetails.ClickTabEngFeeAndFincnciaLV();
                isWaiveRFOFieldEditable=randomPages.IsWaveRFPInlineEditableLV();
                Assert.IsFalse(isWaiveRFOFieldEditable, "Verify that CF Financial user cannot Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in newly converted opportunity to  Engagement");
                extentReports.CreateStepLogs("Passed", "CF Financial User can Inline edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in newly converted opportunity to  Engagement");
                engagementDetails.ClickTabInfoLV();

                //On Edit page
                engagementDetails.ClickEditEngagementLV();
                isWaiveRFOFieldEditable = randomPages.IsWaiveRFOFieldEditableLV();
                Assert.IsFalse(isWaiveRFOFieldEditable, "Verify that Financial user cannot edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in newly converted opportunity to  Engagement"); 
                extentReports.CreateStepLogs("Passed", "CF Financial User can edit the 'Waive RFO' field in 'Fees and Financials' tab-> 'Estimated Fees' section in newly converted opportunity to  Engagement");
                randomPages.CancelEditFormLV();
                randomPages.CloseActiveTab(engagementName);
                randomPages.CloseActiveTab(engagementName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", stdUser + " CF Financial User logged out ");


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