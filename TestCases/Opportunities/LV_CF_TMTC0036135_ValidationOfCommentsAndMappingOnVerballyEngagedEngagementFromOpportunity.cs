using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunities
{
    class LV_CF_TMTC0036135_ValidationOfCommentsAndMappingOnVerballyEngagedEngagementFromOpportunity : BaseClass
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

        public static string fileTMTC0036135 = "LV_TMTC0036135_ValidationOfCommentsAndMappingOnConvertingOpportunityToEngagementCF";
        private string commentTextOppExl;
        private string commentTypeOppExl;
        private string userCompliance;
        private string oppCommentsText;
        private string oppCommentsCeatedBy;
        private string oppCommentsCeatedDate;
        private string commentOppType;
        private string userCAO;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        /// <summary>
        /// /CF***/////
        /// </summary>
        //TMT0082992	Verify that the standard user is able to view the standard user's Administrative/Internal/ Next step comments and CAO's Internal and Next step comments on changing Stage to Verbally Engaged.
        //TMT0082994	Verify that CAO can view standard users' and CAO's Administrative/Internal/ Next step comments on changing Stage to Verbally Engaged.
        //TMT0082996    Verify that the Compliance user can view standard users' and CAO's Internal/ Next step comments and Compliance comments on changing Stage to Verbally Engaged.
        //TMT0083031	Verify that the Standard user can view standard user's Administrative/Internal/ Next step comments and CAO's Internal and Next step comments on changing the engagement from Verbally Engaged to Fully Engaged.
        //TMT0083036    Verify that the CAO can view standard user's and CAO's Administrative/Internal/ Next step comments on changing the engagement from Verbally Engaged to Fully Engaged.
        //TMT0083039    Verify that the Compliance user is able to view only the Compliance comments on changing the engagement from Verbally Engaged to Fully Engaged.
        
        [Test]
        public void VerifyTheVerballyEngagedMappingOfComplianceAndLegalFieldsFromOpportunityToEngagementCFLV()
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
                string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType);
                string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileTMTC0036135); ;//updated move to jobtype
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
                opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTC0036135);
                extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                opportunityDetails.ClickReturnToOpportunityLV();
                randomPages.CloseActiveTab("Internal Team");
                extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                //CF Financial User Add Comments on Opportunity detail page                
                int typeRowCount = ReadExcelData.GetRowCount(excelPath, "Comments");
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    opportunityDetails.ClickOppNewCommentsLV();
                    opportunityDetails.AddNewOppCommentLV(commentTypeOppExl, commentTextOppExl);
                    commentOppType = opportunityDetails.GetCommentTypeLV();
                    Assert.AreEqual(commentOppType, commentTypeOppExl, "Verify Comments added with Type:  " + commentTypeOppExl);
                    extentReports.CreateStepLogs("Passed", "CF Financial User added '" + commentTypeOppExl + "' Comments on Opportunity page");
                    randomPages.CloseActiveTab(opportunityDetails.GetCommentIDLV());
                    extentReports.CreateStepLogs("Info", "Comments Detail page is closed");

                    //User redirected to Opp Details page
                    opportunityDetails.ClickViewAllCommentsLV();
                    oppCommentsText = opportunityDetails.GetOppCommentsTextLV(commentTypeOppExl);
                    oppCommentsCeatedBy = opportunityDetails.GetOppCommentsCeatedByLV(commentTypeOppExl);
                    oppCommentsCeatedDate = opportunityDetails.GetOppCommentsCeatedDateLV(commentTypeOppExl);
                    extentReports.CreateStepLogs("Info", oppCommentsText + ", " + oppCommentsCeatedBy + "," + oppCommentsCeatedDate + " CF Financial User added Comments added on Opportunity page with Type:  " + commentTypeOppExl);
                    Assert.AreEqual(valUser, oppCommentsCeatedBy);
                    extentReports.CreateStepLogs("Info", commentTypeOppExl + ": " + oppCommentsText + "Created by " + oppCommentsCeatedBy + "on Date: " + oppCommentsCeatedDate + " are saved ");
                    randomPages.CloseActiveTab("Opportunity Comments");
                }
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + valUser + " logged out");

                /////--------Compliance user-----------///////                
                userCompliance = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                homePage.SearchUserByGlobalSearchN(userCompliance);
                extentReports.CreateStepLogs("Info", "Compliance User: " + userCompliance + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                extentReports.CreateStepLogs("Passed", "Compliance User: " + userCompliance + " logged in on Lightning View");
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                //Search for created opportunity
                opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "Opportunity: " + opportunityName + " found and selected");
                
                //Compliance user can add "Compliance" comments to an opportunity.
                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 1);
                commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 2);

                opportunityDetails.ClickOppNewCommentsLV();
                opportunityDetails.AddNewOppCommentLV(commentTypeOppExl, commentTextOppExl);
                extentReports.CreateStepLogs("Info", "Comments added on Opportunity page with Type:  " + commentTypeOppExl);
                commentOppType = opportunityDetails.GetCommentTypeLV();
                Assert.AreEqual(commentOppType, commentTypeOppExl, "Verify Comments added with Type:  " + commentTypeOppExl);
                extentReports.CreateStepLogs("Passed", "Compliance User added '" + commentOppType + "' Comments on Opportunity");
                randomPages.CloseActiveTab(opportunityDetails.GetCommentIDLV());

                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "Compliance User: " + valUser + " logged out");

                /////////////------CAO User------/////////////////
                userCAO = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 4, 1);
                homePage.SearchUserByGlobalSearchN(userCAO);
                extentReports.CreateStepLogs("Info", "CAO User: " + userCAO + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                extentReports.CreateStepLogs("Passed", "CAO User: " + userCAO + " logged in on Lightning View");
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "CAO User is on " + moduleNameExl + " Page ");
                //Search for created opportunity
                opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "Opportunity: " + opportunityName + " found and selected");
                
                //CAO can add Administrative/ Internal/ Next step comments to an opportunity.
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    opportunityDetails.ClickOppNewCommentsLV();
                    opportunityDetails.AddNewOppCommentLV(commentTypeOppExl, commentTextOppExl);
                    commentOppType = opportunityDetails.GetCommentTypeLV();
                    Assert.AreEqual(commentOppType, commentTypeOppExl, "Verify Comments added with Type:  " + commentTypeOppExl);
                    extentReports.CreateStepLogs("Passed", "CF Financial User added '" + commentTypeOppExl + "' Comments on Opportunity page");
                    randomPages.CloseActiveTab(opportunityDetails.GetCommentIDLV());
                    extentReports.CreateStepLogs("Info", "Comments Detail page is closed");

                    //User redirected to Opp Details page
                    opportunityDetails.ClickViewAllCommentsLV();
                    oppCommentsText = opportunityDetails.GetOppCommentsTextLV(commentTypeOppExl);
                    oppCommentsCeatedBy = opportunityDetails.GetOppCommentsCeatedByLV(commentTypeOppExl);
                    oppCommentsCeatedDate = opportunityDetails.GetOppCommentsCeatedDateLV(commentTypeOppExl);
                    extentReports.CreateStepLogs("Info", oppCommentsText + ", " + oppCommentsCeatedBy + "," + oppCommentsCeatedDate + " CF Financial User added Comments added on Opportunity page with Type:  " + commentTypeOppExl);
                    Assert.AreEqual(userCAO, oppCommentsCeatedBy);
                    extentReports.CreateStepLogs("Info", commentTypeOppExl + ": " + oppCommentsText + "Created by " + oppCommentsCeatedBy + "on Date: " + oppCommentsCeatedDate + " are saved ");
                    randomPages.CloseActiveTab("Opportunity Comments");
                }

                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CAO User: " + userCAO + " logged out");


                //--------------//                  
                //System Admin Performin required actions
                string adminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 5, 1);
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
                opportunityDetails.UpdateOutcomeNBCApproveDetailsLV(valJobType);
                extentReports.CreateStepLogs("Info", "Conflict Check and NBC details are provided");

                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUser + " logged out");

                //standard user can view the standard user enter req fields for VE
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");
                //appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateLog("User is on " + moduleNameExl + " Page ");
                //Search for created opportunity
                opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "Opportunity: " + opportunityName + " found and selected");
                
                opportunityDetails.EnterVerballyEngagedRequiredFieldsLV(valJobType, fileTMTC0036135);
                extentReports.CreateStepLogs("Info", "Entered All Field level Required values for Verbally Engage");
                string stageExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 31);
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
                
                //////////**********************//////////////
                //TMT0082992	Verify that the standard user is able to view the standard user's Administrative/Internal/ Next step comments and CAO's Internal and Next step comments on changing Stage to Verbally Engaged.
                engagementDetails.ClickViewAllCommentsLV();
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    //**Issue
                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "**Issue CF Financial user can see '" + commentTypeOppExl + "' on added by CF Financial User " + valUser + " On Verbally Engaged Engagement:: " + opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));
                }
                for (int typeRow = 3; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    //**Issue
                    //Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "//**Issue CF Financial user can see '" + commentTypeOppExl + "' on added by CAO User " + userCAO + " On Verbally Engaged Engagement:: ");// + opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));
                }
                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 2, 1);
                commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 2, 2);
                //**Issue
                //Assert.IsFalse(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCompliance,commentTextOppExl));
                extentReports.CreateStepLogs("Passed", " **Issue CF Financial user can'nt see '" + commentTypeOppExl + "' on added by User " + userCompliance + " On Verbally Engaged Engagement");

                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 1);
                commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 2);
                //**Issue
                //Assert.IsFalse(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCompliance,commentTextOppExl));
                extentReports.CreateStepLogs("Passed", "**Issue CF Financial user can'nt see '" + commentTypeOppExl + "' on added by User " + userCompliance + " On Verbally Engaged Engagement");

                randomPages.CloseActiveTab("Engagement Comments");
                randomPages.CloseActiveTab(opportunityName);

                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "Standard User: " + valUser + " logged out");

                ///---------------------
                ///TMT0082994	Verify that CAO can view standard users' and CAO's Administrative/Internal/ Next step comments on changing Stage to Verbally Engaged.

                homePage.SearchUserByGlobalSearchN(userCAO);
                extentReports.CreateStepLogs("Info", "CAO User: " + userCAO + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                extentReports.CreateStepLogs("Passed", "CAO User: " + userCAO + " logged in on Lightning View");

                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "CAO User is on " + moduleNameExl + " Page ");
                //Search for created opportunity
                opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "Opportunity: " + opportunityName + " found and selected");
                opportunityDetails.ClickVerballyEngagedEngagementNumberLV(opportunityName);
                engagementDetails.ClickViewAllCommentsLV();
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    //**Issue
                    //Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "**Issue CAO User can see '" + commentTypeOppExl + "' on added by CF Financial User " + valUser + " On Verbally Engaged Engagement:: "); // + opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));

                }
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    //**Issue
                    //Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "//**Issue CAO User can see '" + commentTypeOppExl + "' on added by CAO User " + userCAO + " On Verbally Engaged Engagement:: ");// + opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));

                }
                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 1);
                commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 2);
                //**Issue
                //Assert.IsFalse(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCompliance,commentTextOppExl));
                extentReports.CreateStepLogs("Passed", "**Issue CAO user can't see '" + commentTypeOppExl + "' on added by User " + userCompliance + " On Verbally Engaged Engagement");
                randomPages.CloseActiveTab("Engagement Comments");
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CAO User: " + userCAO + " logged out");

                //-------------------//
                //TMT0082996 Verify that the Compliance user can view only the Compliance comments on changing Stage to Verbally Engaged.

                homePage.SearchUserByGlobalSearchN(userCompliance);
                extentReports.CreateStepLogs("Info", "Compliance User: " + userCompliance + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                extentReports.CreateStepLogs("Passed", "Compliance User: " + userCompliance + " logged in on Lightning View");

                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                //Search for created opportunity
                opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "Opportunity: " + opportunityName + " found and selected");
                opportunityDetails.ClickVerballyEngagedEngagementNumberLV(opportunityName);

                engagementDetails.ClickViewAllCommentsLV();
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    //**Issue
                    Assert.IsFalse(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "**Issue Compliance user can't see '" + commentTypeOppExl + "' on added by CF Financial User " + valUser + " On Verbally Engaged Engagement:: ");// + opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));

                }
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    //**Issue
                    Assert.IsFalse(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "//**Issue Compliance user can't see '" + commentTypeOppExl + "' on added by CAO User " + userCAO + " On Verbally Engaged Engagement:: ");// + opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));

                }
                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 1);
                commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 2);
                //**Issue
                //Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCompliance, commentTextOppExl));
                extentReports.CreateStepLogs("Passed", "**Issue Compliance user can see '" + commentTypeOppExl + "' on added by User " + userCompliance + " On Verbally Engaged Engagement");
                randomPages.CloseActiveTab("Engagement Comments");
                randomPages.CloseActiveTab(opportunityName);                
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "Compliance User: " + userCompliance + " logged out");

                ///------------//
                ///Standard user Requested Verbally Engaged to Fully Engaged.

                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");
                //appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateLog("User is on " + moduleNameExl + " Page ");
                //Search for created opportunity
                opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "Opportunity: " + opportunityName + " found and selected");
                opportunityDetails.ClickVerballyEngagedEngagementNumberLV(opportunityName);
                engagementDetails.ClickRequestFullEngagementLV();
                engagementDetails.EnterRequestFullEngagementReqValuesLV();
                extentReports.CreateStepLogs("Info", "Required Fields for Request Full Engagement are entered");
                string popupMessage = randomPages.GetLVMessagePopup();
                extentReports.CreateStepLogs("Passed", "Required Fields saved with popup message: " + popupMessage);                //Create Primary Contact 
                 
                //engagementDetails.ClickRequestFullEngagementLV();
                extentReports.CreateStepLogs("Info", "Click on Request Full Engagement button and Fill are required fields");
                //*******Don't have clarify of the Engagement Information pop-up******
                engagementDetails.ClickSaveEngagementInformationPopup();
                extentReports.CreateStepLogs("Passed", "Save button clicked on Engagement Information pop-up");
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + valUser + " logged out");

                //--------------//
                //TMT0083036	Verify that the CAO can view standard user's and CAO's Administrative/Internal/ Next step comments on changing the engagement from Verbally Engaged to Fully Engaged.

                //CAO User can approve the Fully engagement request
                //string userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                homePage.SearchUserByGlobalSearchN(userCAO);
                extentReports.CreateStepLogs("Info", "User: " + userCAO + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                extentReports.CreateStepLogs("Passed", "User: " + userCAO + " logged in on Lightning View");
                //Go to Opportunity module in Lightning View 
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                engagementHome.SearchEngagementInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "Engagement found and selected");
                string status = opportunityDetails.ClickApproveButtonLV2();
                Assert.AreEqual(status, "Approved");
                extentReports.CreateStepLogs("Passed", "Verbally Engagement is " + status + " for Full Enagement ");
                opportunityDetails.CloseApprovalHistoryTabL();
                CustomFunctions.PageReload(driver);
                //Verify CAO User can see standard user's and CAO Administrative/Internal/ Next step comments
                opportunityDetails.ClickViewAllCommentsLV();
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    //**Issue
                    //Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "**Issue CAO User can see '" + commentTypeOppExl + "' on added by CF Financial User " + valUser + " On Fully Engaged Engagement:: "); // + opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));

                }
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    //**Issue
                    //Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "//**Issue CAO User can see '" + commentTypeOppExl + "' on added by CAO User " + userCAO + " On Fully Engaged Engagement:: ");// + opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));

                }
                //Verify CAOUser cann't see Compliance comments
                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 1);
                commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 2);
                //**Issue
                //Assert.IsFalse(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCompliance,commentTextOppExl));
                extentReports.CreateStepLogs("Passed", "**Issue CAO user can't see '" + commentTypeOppExl + "' on added by User " + userCompliance + " On Verbally Engaged Engagement");
                randomPages.CloseActiveTab("Engagement Comments");
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CAO User: " + userCAO + " logged out");

                //----------------------//
                //TMT0083031	Verify that the Standard user can view standard user's Administrative/Internal/ Next step comments and CAO's Internal and Next step comments on changing the engagement from Verbally Engaged to Fully Engaged.

                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");
                //appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateLog("User is on " + moduleNameExl + " Page ");
                engagementHome.SearchEngagementInLightningView(opportunityName);

                opportunityDetails.ClickViewAllCommentsLV();
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    //**Issue
                    //Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "**Issue CF Financial User can see '" + commentTypeOppExl + "' on added by CF Financial User " + valUser + " On Fully Engaged Engagement:: "); // + opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));

                }
                for (int typeRow = 3; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    //**Issue
                    //Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "//**Issue CF Financial User can see '" + commentTypeOppExl + "' on added by CAO User " + userCAO + " On Fully Engaged Engagement:: ");// + opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));

                }
                //Verify CAOUser cann't see Compliance comments
                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 1);
                commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 2);
                //**Issue
                //Assert.IsFalse(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCompliance,commentTextOppExl));
                extentReports.CreateStepLogs("Passed", "**Issue CF Financial user can't see '" + commentTypeOppExl + "' on added by User " + userCompliance + " On Verbally Engaged Engagement");
                randomPages.CloseActiveTab("Engagement Comments");
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + valUser + " logged out");

                //---------------------------//
                //TMT0083039	Verify that the Compliance user is able to view only the Compliance comments on changing the engagement from Verbally Engaged to Fully Engaged.

                homePage.SearchUserByGlobalSearchN(userCompliance);
                extentReports.CreateStepLogs("Info", "Compliance User: " + userCompliance + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                extentReports.CreateStepLogs("Passed", "Compliance User: " + userCompliance + " logged in on Lightning View");

                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                //Search for created opportunity
                engagementHome.GlobalSearchEngagementInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "Engagement: " + opportunityName + " found and selected");

                opportunityDetails.ClickViewAllCommentsLV();
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    //**Issue
                    Assert.IsFalse(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "**Issue Compliance user can't see '" + commentTypeOppExl + "' on added by CF Financial User " + valUser + " On Verbally Engaged Engagement:: ");// + opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));

                }
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    //**Issue
                    Assert.IsFalse(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "//**Issue Compliance user can't see '" + commentTypeOppExl + "' on added by CAO User " + userCAO + " On Verbally Engaged Engagement:: ");// + opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));

                }
                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 1);
                commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 2);
                //**Issue
                //Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCompliance, commentTextOppExl));
                extentReports.CreateStepLogs("Passed", "**Issue Compliance user can see '" + commentTypeOppExl + "' on added by User " + userCompliance + " On Verbally Engaged Engagement");
                randomPages.CloseActiveTab("Engagement Comments");
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "Compliance User: " + userCompliance + " logged out");

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
