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
    class LV_FR_TMTC0036078_TMTC0036082_VerifyTheMappingOfComplianceAndLegalFieldsFromOpportunityToEngagementFR:BaseClass
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
        EngagementHomePage engagementHome = new EngagementHomePage();
        RandomPages randomPages = new RandomPages();

        public static string fileTC1624 = "LV_TMTC0036078_TMTC0036082_VerifyTheMappingOfComplianceAndLegalFieldsFromOpportunityToEngagementFR";
        private string valReceivedByComplianceDate;
        private string valVerifiedByComplianceDate;
        private string valLegalHoldNotes;
        private string valDateOnHold;
        private string valPutOnHold;
        private string valNotesLegalMatters;
        private bool valLegalHold = false;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyTheMappingOfComplianceAndLegalFieldsFromOpportunityToEngagementFRLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1624;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", login.ValidateUser() + " is able to login ");

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                for (int row = 2; row <= rowJobType; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                    extentReports.CreateStepLogs("Info", "Creating Opportunity with Job Type: " + valJobType + " ");
                    //Login as Standard User profile and validate the user
                    string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                    homePage.SearchUserByGlobalSearchN(userExl);
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();

                    login.SwitchToLightningExperience();
                    string stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(userExl), true);
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " logged in on Lightning View");
                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Pass", appName + " App is selected from App Launcher ");
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    //Validating Title of New Opportunity Page
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB: " + valRecordType);
                    extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                    string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTC1624);//updated totalDbt
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTC1624);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Pass", "User is on Opportunity detail " + displayedTab + " tab ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                    extentReports.CreateStepLogs("Pass", "Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact         
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    addOpportunityContact.CickAddFROpportunityContact();
                    addOpportunityContact.CreateContactL2(fileTC1624, valRecordType);
                    extentReports.CreateStepLogs("Info", valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForFRConversionLV(fileTC1624);                    
                    opportunityDetails.UpdateTotalDebtConfirmedLV();
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTC1624);
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

                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Info", userExl + " Standard User logged out ");

                    extentReports.CreateLog("Admin is Performing Required Actions ");
                    login.SwitchToClassicView();
                    opportunityHome.SearchOpportunity(opportunityName);
                    //update CC 
                    opportunityDetails.UpdateOutcomeDetails(fileTC1624);
                    extentReports.CreateStepLogs("Info", " Required Outcome Details are provided ");

                    //TMT0082742 Verify that the Compliance user can update the fields on the Compliance subtab of the Compliance & Legal tab.
                    string userCompliance = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 2, 1);

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

                    //updating Compliance fields
                    opportunityDetails.ClickTabComplianceLegalLV();
                    opportunityDetails.UpdateComplianceReceivedVerfifiedDateLV();
                    extentReports.CreateStepLogs("Info", "Opportunity Compliance Received & Verfified Date are updated and saved");

                    valReceivedByComplianceDate = opportunityDetails.GetReceivedByComplianceDateLV();
                    valVerifiedByComplianceDate = opportunityDetails.GetVerifiedByComplianceDateLV();
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", userCompliance + " Compliance User logged out ");

                    /////////////////
                    //TMT0082744 Verify that the Legal user can update the fields on the Legal Matters subtab of the Compliance & Legal tab
                    string userLegal = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 3, 1);

                    homePage.SearchUserByGlobalSearchN(userLegal);
                    extentReports.CreateStepLogs("Info", "Legal User: " + userLegal + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "Legal User: " + userLegal + " logged in on Lightning View");

                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Info", "Opportunity: " + opportunityName + " found and selected");

                    //updating Compliance fields
                    opportunityDetails.ClickTabComplianceLegalLV();
                    opportunityDetails.CLickTabLegalMattersLV();
                    valNotesLegalMatters = ReadExcelData.ReadData(excelPath, "Notes", 1);
                    opportunityDetails.UpdateLegalMattersLV(valNotesLegalMatters);
                    extentReports.CreateStepLogs("Info", "Opportunity Legal Matters are updated and saved");
                    valLegalHold = opportunityDetails.GetLegaHoldLV();
                    valLegalHoldNotes = opportunityDetails.GetLegalHoldNotesLV();
                    valDateOnHold = opportunityDetails.GetDateOnHoldLV();
                    valPutOnHold = opportunityDetails.GetPutOnHoldLV();
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", userLegal + " Legal User logged out ");

                    ////--------

                    //Login again as Standard User
                    homePage.SearchUserByGlobalSearchN(userExl);
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(userExl), true);
                    extentReports.CreateLog("User: " + userExl + " logged in on Lightning View");

                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Pass", appName + " App is selected from App Launcher ");
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
                    extentReports.CreateStepLogs("Pass", userExl + " logged out ");

                    string userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);
                    //Login as CAO user to approve the Opportunity                   
                    homePage.SearchUserByGlobalSearchN(userCAOExl);
                    extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();

                    login.SwitchToLightningExperience();
                    string UserCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(UserCAO.Contains(userCAOExl), true);
                    extentReports.CreateLog("User: " + userCAOExl + " logged in on Lightning View");

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
                    extentReports.CreateStepLogs("Info", "Opportunity: " + opportunityName + " Converted into Engagement ");

                    //Validate the Engagement name in Engagement details page
                    string engNumber = engagementDetails.GetEngagementNumberL();
                    Assert.AreEqual(opportunityNumber, engNumber);
                    extentReports.CreateStepLogs("Info", "Number of Engagement : " + engNumber + " is Same as Opportunity number ");
                    string engName = engagementDetails.GetEngagementNameL();
                    Assert.AreEqual(opportunityName, engName);
                    extentReports.CreateStepLogs("Passed", "Name of Engagement : " + engName + " is Same as Opportunity Name : " + opportunityName);
                    randomPages.CloseActiveTab(engName);

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    engagementHome.GlobalSearchEngagementInLightningView(engName);

                    //TMT0082753	Verify that the Compliance fields updated by the Compliance user get mapped to the engagement's Compliance tab as CAO user.
                    engagementDetails.ClickTabComplianceLegalLV();

                    //Get complianceReview and verifyfied by                        
                    Assert.AreEqual(valReceivedByComplianceDate, engagementDetails.GetReceivedByComplianceDate());
                    extentReports.CreateStepLogs("Passed", "Received By Compliance Date: '" + valReceivedByComplianceDate + "' is mapped on Engagement page after conversion from Opportunity verified as CAO User");

                    Assert.AreEqual(valVerifiedByComplianceDate, engagementDetails.GetVerifiedByComplianceDate());
                    extentReports.CreateStepLogs("Passed", "Verified By Compliance Date: '" + valVerifiedByComplianceDate + "' is mapped on Engagement page after conversion from Opportunity verified as CAO User");

                    ////TMT0082754	Verify that the Legal fields updated by the legal user get mapped to the engagement's Legal Matters tab verified as CAO User.
                    engagementDetails.CLickTabLegalMattersLV();

                    Assert.AreEqual(valLegalHold, opportunityDetails.GetLegaHoldLV());
                    extentReports.CreateStepLogs("Passed", "Legal Hold selection is: '" + valLegalHold + "' mapped on Engagement page after conversion from Opportunity verified as CAO User");

                    Assert.AreEqual(valLegalHoldNotes, opportunityDetails.GetLegalHoldNotesLV());
                    extentReports.CreateStepLogs("Passed", "Legal Hold Notes: '" + valLegalHoldNotes + "' mapped on Engagement page after conversion from Opportunity verified as CAO User");

                    Assert.AreEqual(valDateOnHold, opportunityDetails.GetDateOnHoldLV());
                    extentReports.CreateStepLogs("Passed", "Date On Hold: '" + valDateOnHold + "' mapped on Engagement page after conversion from Opportunity verified as CAO User");

                    Assert.AreEqual(valPutOnHold, opportunityDetails.GetPutOnHoldLV());
                    extentReports.CreateStepLogs("Passed", "Put On Hold: '" + valPutOnHold + "' mapped on Engagement page after conversion from Opportunity verified as CAO User");

                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();

                    // string userCompliance = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 4, 1);

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
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    engagementHome.GlobalSearchEngagementInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Info", "Engagement: " + opportunityName + " found and selected");

                    //TMT0082753	Verify that the Compliance fields updated by the Compliance user get mapped to the engagement's Compliance tab.
                    engagementDetails.ClickTabComplianceLegalLV();

                    //Get complianceReview and verified by                        
                    Assert.AreEqual(valReceivedByComplianceDate, engagementDetails.GetReceivedByComplianceDate());
                    extentReports.CreateStepLogs("Passed", "Received By Compliance Date: '" + valReceivedByComplianceDate + "' is mapped on Engagement page after conversion from Opportunity");

                    Assert.AreEqual(valVerifiedByComplianceDate, engagementDetails.GetVerifiedByComplianceDate());
                    extentReports.CreateStepLogs("Passed", "Verified By Compliance Date: '" + valVerifiedByComplianceDate + "' is mapped on Engagement page after conversion from Opportunity");

                    //opportunityDetails.UpdateComplianceReceivedVerifiedDateLV();                        
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", userCompliance + " Compliance User logged out ");

                    /////////////////
                    //TMT0082744 Verify that the Legal user can update the fields on the Legal Matters subtab of the Compliance & Legal tab
                    //string userLegal = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 5, 1);

                    homePage.SearchUserByGlobalSearchN(userLegal);
                    extentReports.CreateStepLogs("Info", "Legal User: " + userLegal + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "Legal User: " + userLegal + " logged in on Lightning View");

                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    engagementHome.GlobalSearchEngagementInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Info", "Engagement: " + opportunityName + " found and selected");

                    //TMT0082754	Verify that the Legal fields updated by the legal user get mapped to the engagement's Legal Matters tab.
                    engagementDetails.ClickTabComplianceLegalLV();
                    engagementDetails.CLickTabLegalMattersLV();
                    ////TMT0082754	Verify that the Legal fields updated by the legal user get mapped to the engagement's Legal Matters tab verified as CAO User.
                    engagementDetails.CLickTabLegalMattersLV();

                    Assert.AreEqual(valLegalHold, opportunityDetails.GetLegaHoldLV());
                    extentReports.CreateStepLogs("Passed", "Legal Hold selection is: '" + valLegalHold + "' mapped on Engagement page after conversion from Opportunity verified as CAO User");

                    Assert.AreEqual(valLegalHoldNotes, opportunityDetails.GetLegalHoldNotesLV());
                    extentReports.CreateStepLogs("Passed", "Legal Hold Notes: '" + valLegalHoldNotes + "' mapped on Engagement page after conversion from Opportunity verified as CAO User");

                    Assert.AreEqual(valDateOnHold, opportunityDetails.GetDateOnHoldLV());
                    extentReports.CreateStepLogs("Passed", "Date On Hold: '" + valDateOnHold + "' mapped on Engagement page after conversion from Opportunity verified as CAO User");

                    Assert.AreEqual(valPutOnHold, opportunityDetails.GetPutOnHoldLV());
                    extentReports.CreateStepLogs("Passed", "Put On Hold: '" + valPutOnHold + "' mapped on Engagement page after conversion from Opportunity verified as CAO User");

                    randomPages.CloseActiveTab(opportunityName);
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", userLegal + " Legal User logged out ");
                }
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