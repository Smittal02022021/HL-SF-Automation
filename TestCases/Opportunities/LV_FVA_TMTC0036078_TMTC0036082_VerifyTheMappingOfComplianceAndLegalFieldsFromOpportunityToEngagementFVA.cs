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
    class LV_FVA_TMTC0036078_TMTC0036082_VerifyTheMappingOfComplianceAndLegalFieldsFromOpportunityToEngagementFVA : BaseClass
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

        public static string fileTMTC0036078 = "LV_TMTC0036078_TMTC0036082_VerifyTheMappingOfComplianceAndLegalFieldsFromOpportunityToEngagementFVA";
        private string valReceivedByComplianceDate;
        private string valVerifiedByComplianceDate;
        private string valLegalHoldNotes;
        private string valDateOnHold;
        private string valPutOnHold;
        private bool valLegalHold = false;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        /// <summary>
        /// /FVA***/////
        /// </summary>
        //TMT0082742	Verify that the Compliance user can update the fields on the Compliance subtab of the Compliance & Legal tab.
        //TMT0082744 Verify that the Legal user can update the fields on the Legal Matters subtab of the Compliance & Legal tab.
        //TMT0082753  Verify that the Compliance fields updated by the Compliance user get mapped to the engagement's Compliance tab.
        //TMT0082754 Verify that the Legal fields updated by the legal user get mapped to the engagement's Legal Matters tab.
        [Test]

        public void VerifyTheMappingOfComplianceAndLegalFieldsFromOpportunityToEngagementFVALV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0036078;
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
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileTMTC0036078);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");
                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTC0036078);
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
                    addOpportunityContact.CreateContactL2(fileTMTC0036078, valRecordType);
                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForFVAConversionLV(fileTMTC0036078);
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
                    opportunityDetails.UpdateOutcomeDetails(fileTMTC0036078);
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
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTC0036078);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab("Internal Team");
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Passed", "Admin: " + adminUserExl + "switched to Classic and Loggout ");

                    //TMT0082742 Verify that the Compliance user can update the fields on the Compliance subtab of the Compliance & Legal tab.
                    string userCompliance = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 4, 1);
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
                    string userLegal = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 5, 1);
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
                    string notesLegalMatters = ReadExcelData.ReadData(excelPath, "Notes", 1);
                    opportunityDetails.UpdateLegalMattersLV(notesLegalMatters);
                    extentReports.CreateStepLogs("Info", "Opportunity Legal Matters are updated and saved");
                    valLegalHold = opportunityDetails.GetLegaHoldLV();
                    valLegalHoldNotes = opportunityDetails.GetLegalHoldNotesLV();
                    valDateOnHold = opportunityDetails.GetDateOnHoldLV();
                    valPutOnHold = opportunityDetails.GetPutOnHoldLV();
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", userLegal + " Legal User logged out ");

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

                    //Get complianceReview and verifyfied by                        
                    Assert.AreEqual(valReceivedByComplianceDate, engagementDetails.GetReceivedByComplianceDate());
                    extentReports.CreateStepLogs("Passed", "Received By Compliance Date: '" + valReceivedByComplianceDate + "' is mapped on Engagement page after conversion from Opportunity");

                    Assert.AreEqual(valVerifiedByComplianceDate, engagementDetails.GetVerifiedByComplianceDate());
                    extentReports.CreateStepLogs("Passed", "Verified By Compliance Date: '" + valVerifiedByComplianceDate + "' is mapped on Engagement page after conversion from Opportunity");

                    //opportunityDetails.UpdateComplianceReceivedVerfifiedDateLV();                        
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
            