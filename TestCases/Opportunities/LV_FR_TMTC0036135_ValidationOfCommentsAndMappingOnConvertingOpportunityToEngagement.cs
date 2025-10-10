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
    class LV_FR_TMTC0036135_ValidationOfCommentsAndMappingOnConvertingOpportunityToEngagement:BaseClass
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

        public static string fileTMTC0036135 = "LV_TMTC0036135_ValidationOfCommentsAndMappingOnConvertingOpportunityToEngagementFR";
        private string commentTextOppExl;
        private string commentTypeOppExl;
        private string userCompliance;
        private string oppCommentsText;
        private string oppCommentsCeatedBy;
        private string oppCommentsCeatedDate;
        private string commentOppType;
        private string fieldValidation;
        private string errorFieldLevelExl;
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
        /// /FR***/////
        /// </summary>
        //TMT0082945 Verify that a standard user can add "Administrative" comments to an opportunity.
        //TMT0082949 Verify that a standard user can add "Internal" comments to an opportunity.
        //TMT0082951 Verify that a standard user can add "Next Step" comments to an opportunity.
        //TMT0082955 Verify that the Compliance user can view the standard user's Internal and Next step comments added to an opportunity.
        //TMT0082958 Verify that the Compliance user can add "Compliance" comments to an opportunity.
        //TMT0082960  Verify that the Compliance user cannot add "Administrative/ Internal/ Next Steps" comments to an opportunity.


        [Test]
        public void VerifyTheMappingOfComplianceAndLegalFieldsFromOpportunityToEngagementFRLV()
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
                string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 25);
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

                addOpportunityContact.CickAddOpportunityContactLV();
                addOpportunityContact.CreateContactL2(fileTMTC0036135, valRecordType);
                extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                //Update required Opportunity fields for conversion and Internal team details
                opportunityDetails.UpdateReqFieldsForFRConversionLV(fileTMTC0036135);
                opportunityDetails.UpdateTotalDebtConfirmedLV();
                extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                
                opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTC0036135);
                extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                opportunityDetails.ClickReturnToOpportunityL();
                randomPages.CloseActiveTab("Internal Team");
                extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                //PitchMandateAward details
                randomPages.ClickPitchMandteAwardTabLV();
                opportunityDetails.CreateNewPitchMandateAwardLV();
                extentReports.CreateStepLogs("Info", "New Pitch/Mandate Award detail provided ");
                string idPMA = opportunityDetails.GetPitchMandateAwardID();
                randomPages.CloseActiveTab(idPMA + " | Pitch/Mandate Award");

                //CF Financial User Add Comments on Opportunity detail page
                //TMT0082945 Verify that a standard user can add "Administrative" comments to an opportunity.
                //TMT0082949 Verify that a standard user can add "Internal" comments to an opportunity.
                //TMT0082951 Verify that a standard user can add "Next Step" comments to an opportunity.

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

                //TMT0082953 Verify that a standard user cannot add "Compliance" comments to an opportunity.
                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 1);
                commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 2);
                errorFieldLevelExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 3);

                opportunityDetails.ClickOppNewCommentsLV();
                opportunityDetails.AddNewOppCommentLV(commentTypeOppExl, commentTextOppExl);
                fieldValidation = opportunityDetails.GetFieldLevelValidationLV();
                Assert.AreEqual(errorFieldLevelExl, fieldValidation);
                extentReports.CreateStepLogs("Passed", "CF Financial user is not able to add Compliance commetns with validation message: " + fieldValidation);
                opportunityDetails.CancelFormLV();
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
                //Verify Compliance user can see added comments
                opportunityDetails.ClickViewAllCommentsLV();

                //TMT0082955 Verify that the Compliance user can view the standard user's Internal and Next step comments added to an opportunity.
                for (int typeRow = 3; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    oppCommentsText = opportunityDetails.GetOppCommentsTextLV(commentTypeOppExl);
                    Assert.AreEqual(commentTextOppExl, oppCommentsText);
                    oppCommentsCeatedBy = opportunityDetails.GetOppCommentsCeatedByLV(commentTypeOppExl);
                    Assert.AreEqual(valUser, oppCommentsCeatedBy);
                    extentReports.CreateStepLogs("Passed", "Compliance user can see '" + commentTypeOppExl + "' added by CF Financial User:" + valUser + " on  Opportunity" + opportunityName);
                    oppCommentsCeatedDate = opportunityDetails.GetOppCommentsCeatedDateLV(commentTypeOppExl);
                    extentReports.CreateStepLogs("Info", commentTypeOppExl + ": " + oppCommentsText + ", " + oppCommentsCeatedBy + "," + oppCommentsCeatedDate + " are displayed to Compliance user ");

                }
                randomPages.CloseActiveTab("Opportunity Comments");

                //TMT0082958	Verify that the Compliance user can add "Compliance" comments to an opportunity.
                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 1);
                commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 2);

                opportunityDetails.ClickOppNewCommentsLV();
                opportunityDetails.AddNewOppCommentLV(commentTypeOppExl, commentTextOppExl);
                extentReports.CreateStepLogs("Info", "Comments added on Opportunity page with Type:  " + commentTypeOppExl);
                commentOppType = opportunityDetails.GetCommentTypeLV();
                Assert.AreEqual(commentOppType, commentTypeOppExl, "Verify Comments added with Type:  " + commentTypeOppExl);
                extentReports.CreateStepLogs("Passed", "Compliance User added '" + commentOppType + "' Comments on Opportunity");
                randomPages.CloseActiveTab(opportunityDetails.GetCommentIDLV());

                opportunityDetails.ClickViewAllCommentsLV();

                oppCommentsText = opportunityDetails.GetOppCommentsTextLV(commentTypeOppExl);
                oppCommentsCeatedBy = opportunityDetails.GetOppCommentsCeatedByLV(commentTypeOppExl);
                oppCommentsCeatedDate = opportunityDetails.GetOppCommentsCeatedDateLV(commentTypeOppExl);
                extentReports.CreateStepLogs("Info", oppCommentsText + ", " + oppCommentsCeatedBy + "," + oppCommentsCeatedDate + " Compliance User added Comments added on Opportunity page with Type:  " + commentTypeOppExl);
                Assert.AreEqual(userCompliance, oppCommentsCeatedBy);
                extentReports.CreateStepLogs("Info", commentTypeOppExl + ": " + oppCommentsText + "Created by " + oppCommentsCeatedBy + "on Date: " + oppCommentsCeatedDate + " are saved ");
                randomPages.CloseActiveTab("Opportunity Comments");

                //TMT0082960 Verify that the Compliance user cannot add "Administrative/ Internal/ Next Steps" comments to an opportunity.
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    errorFieldLevelExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 3);
                    opportunityDetails.ClickOppNewCommentsLV();
                    opportunityDetails.AddNewOppCommentLV(commentTypeOppExl, commentTextOppExl);
                    fieldValidation = opportunityDetails.GetFieldLevelValidationLV();
                    Assert.AreEqual(errorFieldLevelExl, fieldValidation);
                    extentReports.CreateStepLogs("Passed", "Compliance user is not able to add '" + commentTypeOppExl + "' Compliance comments with validation message: " + fieldValidation);
                    opportunityDetails.CancelFormLV();
                }
                randomPages.CloseActiveTab(opportunityName);
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
                //Verify Compliance user can see added comments
                opportunityDetails.ClickViewAllCommentsLV();

                //TMT0082962	Verify that CAO is able to view standard users' Administrative/ Internal/ Next step comments and Compliance user's "Compliance" comments added to an opportunity.
                for (int typeRow = 2; typeRow <= typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    oppCommentsText = opportunityDetails.GetOppCommentsTextLV(commentTypeOppExl);
                    Assert.AreEqual(commentTextOppExl, oppCommentsText);
                    oppCommentsCeatedBy = opportunityDetails.GetOppCommentsCeatedByLV(commentTypeOppExl);
                    if (commentTypeOppExl == "Compliance")
                    {
                        Assert.AreEqual(userCompliance, oppCommentsCeatedBy);
                    }
                    else
                    {
                        Assert.AreEqual(valUser, oppCommentsCeatedBy);
                    }

                    extentReports.CreateStepLogs("Passed", "CAO user can see '" + commentTypeOppExl + "' on  Opportunity " + opportunityName);
                    oppCommentsCeatedDate = opportunityDetails.GetOppCommentsCeatedDateLV(commentTypeOppExl);
                    extentReports.CreateStepLogs("Info", commentTypeOppExl + ": " + oppCommentsText + ", " + oppCommentsCeatedBy + "," + oppCommentsCeatedDate + " are displayed to CAO user ");

                }
                randomPages.CloseActiveTab("Opportunity Comments");

                //TMT0082964	Verify that CAO can add Administrative/ Internal/ Next step comments to an opportunity.
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

                //TMT0082966 Verify that CAO is not able to add Compliance comments to an opportunity.
                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 1);
                commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 2);
                errorFieldLevelExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 3);

                opportunityDetails.ClickOppNewCommentsLV();
                opportunityDetails.AddNewOppCommentLV(commentTypeOppExl, commentTextOppExl);
                fieldValidation = opportunityDetails.GetFieldLevelValidationLV();
                Assert.AreEqual(errorFieldLevelExl, fieldValidation);
                extentReports.CreateStepLogs("Passed", "CAO user is not able to add Compliance commetns with validation message: " + fieldValidation);
                opportunityDetails.CancelFormLV();
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CAO User: " + userCAO + " logged out");


                //--------------

                //TMT0082970	Verify that the Compliance user can view standard users' and CAO's Internal and Next step comments and Compliance comments added to an opportunity.
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
                //Verify Compliance user can see added comments
                opportunityDetails.ClickViewAllCommentsLV();
                //Compliance User can see added Comments added by CF Financial  User
                for (int typeRow = 3; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "Compliane user can see '" + commentTypeOppExl + "' on added by CF Financial User " + valUser);
                }
                //Compliance User can see added Comments added by CAO User
                for (int typeRow = 3; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "Compliane user can see '" + commentTypeOppExl + "' on added by CAO User " + userCAO);
                }

                //Compliance User can see added ComplianceComments
                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 1);
                commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 2);
                Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCompliance, commentTextOppExl));
                extentReports.CreateStepLogs("Passed", "Compliane user can see '" + commentTypeOppExl + "' on added by Compliance User " + userCompliance);
                randomPages.CloseActiveTab("Opportunity Comments");
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "Compliane User: " + userCompliance + " logged out");

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
                //opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTC0036135);
                //extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                //opportunityDetails.ClickReturnToOpportunityLV();
                //randomPages.CloseActiveTab("Internal Team");

                randomPages.DetailPageFullViewLV();
                opportunityDetails.UpdateCCOutcomeDetailsLV();

                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUser + " logged out");

                //TMT0082968	Verify that the standard user can view the standard user's Administrative/Internal/ Next step comments and CAO's Internal and Next step comments.

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
                //Verify Compliance user can see added comments
                opportunityDetails.ClickViewAllCommentsLV();
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "CF Financial user can see '" + commentTypeOppExl + "' on added by CF Financial User " + valUser);

                }
                for (int typeRow = 3; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "CF Financial user can see '" + commentTypeOppExl + "' on added by CAO User " + userCAO);

                }
                randomPages.CloseActiveTab("Opportunity Comments");
                //Requesting for engagement
                opportunityDetails.ClickRequestToEngL();
                //Submit Request To Engagement Conversion 
                string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");

                //TMT0082972	Verify that the standard user can view the standard user's Administrative/Internal/ Next step comments and CAO's Internal and Next step comments on requesting engagement, but before approval.
                opportunityDetails.ClickViewAllCommentsLV();
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "CF Financial user can see '" + commentTypeOppExl + "' on added by CF Financial User " + valUser + " On Requested Opportunity before approval for Engagement");

                }
                for (int typeRow = 3; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "CF Financial user can see '" + commentTypeOppExl + "' on added by CAO User " + userCAO + " On Requested Opportunity before approval for Engagement ");

                }
                randomPages.CloseActiveTab("Opportunity Comments");
                randomPages.CloseActiveTab(opportunityName);

                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "Standard User: " + valUser + " logged out");

                //TMT0082974	Verify that the Compliance user can view standard users' and CAO's Internal/ Next step comments and Compliance comments on the opportunity before approval.
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
                //Verify Compliance user can see added comments
                opportunityDetails.ClickViewAllCommentsLV();
                //Compliance User can see added Comments added by CF Financial  User
                for (int typeRow = 3; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "Compliane user can see '" + commentTypeOppExl + "' on added by CF Financial User " + valUser + " On Requested Opportunity before approval for Engagement");
                }
                //Compliance User can see added Comments added by CAO User
                for (int typeRow = 3; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "Compliane user can see '" + commentTypeOppExl + "' on added by CAO User " + userCAO + " On Requested Opportunity before approval for Engagement");
                }

                //Compliance User can see added ComplianceComments
                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 1);
                commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 2);
                Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCompliance, commentTextOppExl));
                extentReports.CreateStepLogs("Passed", "Compliane user can see '" + commentTypeOppExl + "' on added by Compliance User " + userCompliance + " On Requested Opportunity before approval for Engagement");
                randomPages.CloseActiveTab("Opportunity Comments");
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "Compliane User: " + userCompliance + " logged out");

                //TMT0082976	Verify that CAO can view standard users' and CAO's Administrative/Internal/ Next step comments and Compliance comments on the opportunity before approval.
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
                //Verify Compliance user can see added comments
                opportunityDetails.ClickViewAllCommentsLV();
                int commentsRowCount = opportunityDetails.GetCommentsCountLV();
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "CAO user can see '" + commentTypeOppExl + "' on added by User " + valUser + " On Requested Opportunity before approval for Engagement");

                }

                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "CAO user can see '" + commentTypeOppExl + "' on added by User " + userCAO + " On Requested Opportunity before approval for Engagement");
                }

                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 1);
                Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCompliance, commentTextOppExl));
                extentReports.CreateStepLogs("Passed", "CAO user can see '" + commentTypeOppExl + "' on added by User " + userCompliance + " On Requested Opportunity before approval for Engagement");
                randomPages.CloseActiveTab("Opportunity Comments");
                string status = opportunityDetails.ClickApproveButtonLV2();
                Assert.AreEqual(status, "Approved");
                extentReports.CreateStepLogs("Passed", "Opportunity Status: " + status + " ");
                opportunityDetails.CloseApprovalHistoryTabL();

                //TMT0082979	Verify that CAO can view standard users' and CAO's Administrative/Internal/ Next step comments and Compliance comments after approving the opportunity.
                opportunityDetails.ClickViewAllCommentsLV();
                commentsRowCount = opportunityDetails.GetCommentsCountLV();
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "CAO user can see '" + commentTypeOppExl + "' on added by User " + valUser + " On Requested Opportunity After approval for Engagement");

                }

                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "CAO user can see '" + commentTypeOppExl + "' on added by User " + userCAO + " On Requested Opportunity After approval for Engagement");
                }

                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 1);
                Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCompliance, commentTextOppExl));
                extentReports.CreateStepLogs("Passed", "CAO user can see '" + commentTypeOppExl + "' on added by User " + userCompliance + " On Requested Opportunity After approval for Engagement");
                randomPages.CloseActiveTab("Opportunity Comments");
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CAO User: " + userCAO + " logged out");

                //TMT0082981	Verify that the standard user is able to view the standard user's Administrative/Internal/ Next step comments and CAO's Internal and Next step comments on approved opportunities before conversion to engagement.

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

                opportunityDetails.ClickViewAllCommentsLV();
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "CF Financial user can see '" + commentTypeOppExl + "' on added by CF Financial User " + valUser + " On Requested Opportunity After approval for Engagement");

                }
                for (int typeRow = 3; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "CF Financial user can see '" + commentTypeOppExl + "' on added by CAO User " + userCAO + " On Requested Opportunity After approval for Engagement ");

                }
                randomPages.CloseActiveTab("Opportunity Comments");
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + valUser + " logged out");

                //TMT0082983	Verify that the Compliance user can view standard users' and CAO's Internal/ Next step comments and Compliance comments after approval, before converting to engagement.
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
                //Verify Compliance user can see added comments
                opportunityDetails.ClickViewAllCommentsLV();
                //Compliance User can see added Comments added by CF Financial  User
                for (int typeRow = 3; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "Compliane user can see '" + commentTypeOppExl + "' on added by CF Financial User " + valUser + " On Requested Opportunity After approval for Engagement");
                }
                //Compliance User can see added Comments added by CAO User
                for (int typeRow = 3; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);

                    Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO, commentTextOppExl));
                    extentReports.CreateStepLogs("Passed", "Compliane user can see '" + commentTypeOppExl + "' on added by CAO User " + userCAO + " On Requested Opportunity before After for Engagement");
                }

                //Compliance User can see added ComplianceComments
                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 1);
                commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 2);
                Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCompliance, commentTextOppExl));
                extentReports.CreateStepLogs("Passed", "Compliane user can see '" + commentTypeOppExl + "' on added by Compliance User " + userCompliance + " On Requested Opportunity After approval for Engagement");
                randomPages.CloseActiveTab("Opportunity Comments");
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "Compliane User: " + userCompliance + " logged out");

                //TMT0082985	Verify the mapping of the comments on the conversion of opportunity into engagement as a CAO.

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
                opportunityDetails.ClickConvertToEngagementL2();
                //CustomFunctions.PageReload(driver);
                extentReports.CreateStepLogs("Info", "CAO converted Opportunity into Engagement");

                engagementDetails.ClickViewAllCommentsLV();
                commentsRowCount = opportunityDetails.GetCommentsCountLV();
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    //**Issue
                    //Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser));
                    extentReports.CreateStepLogs("Passed", "//**Issue CAO user can see '" + commentTypeOppExl + "' on added by User " + userCAO + " are mapped on Converted Engagement");

                }

                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    //**Issue
                    //Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO));
                    extentReports.CreateStepLogs("Passed", "//**Issue CAO user can see '" + commentTypeOppExl + "' on added by User " + userCAO + " are mapped on Converted Engagement");
                }

                commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", 5, 1);
                //**Issue
                //Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCompliance));
                extentReports.CreateStepLogs("Passed", "CAO user can see '" + commentTypeOppExl + "' on added by User " + userCompliance + " are mapped on Converted Engagement");
                randomPages.CloseActiveTab("Opportunity Comments");
                randomPages.CloseActiveTab(opportunityName);
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CAO User: " + userCAO + " logged out");


                //TMT0082987	Verify the mapping of the comments on the engagement as a Standard User.

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
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateLog("User is on " + moduleNameExl + " Page ");
                //Search for created opportunity
                engagementHome.GlobalSearchEngagementInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "Engagement: " + opportunityName + " found and selected");

                engagementDetails.ClickViewAllCommentsLV();
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    //**Issue
                    //Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, valUser));
                    extentReports.CreateStepLogs("Passed", "//**Issue CF Financial user can see '" + commentTypeOppExl + "' on added by CF Financial User " + valUser + " are mapped on Converted Engagement");

                }
                for (int typeRow = 3; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 1);
                    commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Comments", typeRow, 2);
                    //**Issue
                    //Assert.IsTrue(opportunityDetails.IsUserCommentFoundLV(commentTypeOppExl, userCAO));
                    extentReports.CreateStepLogs("Passed", "//**Issue CF Financial user can see '" + commentTypeOppExl + "' on added by CAO User " + userCAO + " are mapped on Converted Engagement");

                }
                randomPages.CloseActiveTab("Opportunity Comments");
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + valUser + " logged out");

                //TMT0082989	Verify the mapping of the comments on the engagement as a Compliance User.

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
                extentReports.CreateStepLogs("Info", "Opportunity: " + opportunityName + " found and selected");
                //Verify Compliance user cannot see added comments
                Assert.IsFalse(engagementDetails.IsViewAllCommentsDisplayedLV());
                extentReports.CreateStepLogs("Passed", "Compliance user cannot see any comments as expected");
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
