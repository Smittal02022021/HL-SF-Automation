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
    class LV_CF_TMTC0036078_TMTC0036082_VerifyTheMappingOfComplianceAndLegalFieldsFromOpportunityToEngagementCF:BaseClass
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

        public static string fileTMTI0055384 = "LV_TMTC0036078_TMTC0036082_VerifyTheMappingOfComplianceAndLegalFieldsFromOpportunityToEngagementCF";
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
        /// /CF***/////
        /// </summary>
        //TMT0082742	Verify that the Compliance user can update the fields on the Compliance subtab of the Compliance & Legal tab.
        //TMT0082744 Verify that the Legal user can update the fields on the Legal Matters subtab of the Compliance & Legal tab.
        //TMT0082753  Verify that the Compliance fields updated by the Compliance user get mapped to the engagement's Compliance tab.
        //TMT0082754 Verify that the Legal fields updated by the legal user get mapped to the engagement's Legal Matters tab.
        [Test]

        public void VerifyTheMappingOfComplianceAndLegalFieldsFromOpportunityToEngagementCFLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0055384;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");
                //TMTI0055384 Verify the availability of new Job Type- Lender Education in Job Type Picklist while adding new CF Opportunity
                //TMTI0055395 Verify user is able to create new Opportunity with new Job Type - Lender Education

                int rowOpp = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                for (int row = 2; row <= rowOpp; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
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
                   
                    extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType);
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTMTI0055384);//updated move to jobtype
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTI0055384);
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
                    addOpportunityContact.CreateContactL2(fileTMTI0055384, valRecordType);
                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForCFConversionLV2(fileTMTI0055384, valJobType);//udated Move to element
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTI0055384);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    randomPages.CloseActiveTab("Internal Team");
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", valUser + " Standard User logged out ");

                    extentReports.CreateStepLogs("Info", "Admin is Performing Required Actions ");
                    opportunityHome.SearchOpportunity(opportunityName);
                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(fileTMTI0055384);
                    if (valJobType.Equals("Buyside") || valJobType.Equals("Sellside"))
                    {
                        opportunityDetails.UpdateNBCApproval();
                        extentReports.CreateStepLogs("Info", "Conflict Check and NBC fields are updated ");
                    }
                    else
                    {
                        extentReports.CreateStepLogs("Info", "Conflict Check fields are updated ");
                    }
                    
                    //TMT0082742 Verify that the Compliance user can update the fields on the Compliance subtab of the Compliance & Legal tab.
                    
                    string userCompliance = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 3, 1);

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
                    string userLegal = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 4, 1);

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
                    string NotesLegalMatters = ReadExcelData.ReadData(excelPath, "Notes", 1);
                    opportunityDetails.UpdateLegalMattersLV(NotesLegalMatters);
                    extentReports.CreateStepLogs("Info", "Opportunity Legal Matters are updated and saved");

                    valLegalHold = opportunityDetails.GetLegaHoldLV();
                    valLegalHoldNotes = opportunityDetails.GetLegalHoldNotesLV();
                    valDateOnHold = opportunityDetails.GetDateOnHoldLV();
                    valPutOnHold = opportunityDetails.GetPutOnHoldLV();
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", userLegal + " Legal User logged out ");

                    ////--------
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
                    string userCAOExl = ReadExcelData.ReadData(excelPath, "CAOUsers", 1);
                    homePage.SearchUserByGlobalSearchN(userCAOExl);
                    extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    extentReports.CreateStepLogs("Passed", "CAO User: " + userCAOExl + " logged in on Lightning View");
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    
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
                    randomPages.CloseActiveTab(opportunityName);

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    engagementHome.GlobalSearchEngagementInLightningView(engagementName);

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

                    randomPages.CloseActiveTab(engagementName);
                    homePageLV.LogoutFromSFLightningAsApprover();

                    //string userCompliance = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 3, 1);

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
                    engagementHome.GlobalSearchEngagementInLightningView(engagementName);
                    extentReports.CreateStepLogs("Info", "Engagement: " + engagementName + " found and selected");

                    //TMT0082753	Verify that the Compliance fields updated by the Compliance user get mapped to the engagement's Compliance tab.
                    engagementDetails.ClickTabComplianceLegalLV();

                    //Get complianceReview and verifyfied by                        
                    Assert.AreEqual(valReceivedByComplianceDate, engagementDetails.GetReceivedByComplianceDate());
                    extentReports.CreateStepLogs("Passed", "Received By Compliance Date: '" + valReceivedByComplianceDate + "' is mapped on Engagement page after conversion from Opportunity");

                    Assert.AreEqual(valVerifiedByComplianceDate, engagementDetails.GetVerifiedByComplianceDate());
                    extentReports.CreateStepLogs("Passed", "Verified By Compliance Date: '" + valVerifiedByComplianceDate + "' is mapped on Engagement page after conversion from Opportunity");

                    //opportunityDetails.UpdateComplianceReceivedVerfifiedDateLV();                        
                    randomPages.CloseActiveTab(engagementName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", userCompliance + " Compliance User logged out ");

                    /////////////////
                    //TMT0082744 Verify that the Legal user can update the fields on the Legal Matters subtab of the Compliance & Legal tab
                    //string userLegal = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 4, 1);

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
                    engagementHome.GlobalSearchEngagementInLightningView(engagementName);
                    extentReports.CreateStepLogs("Info", "Engagement: " + engagementName + " found and selected");

                    //TMT0082754	Verify that the Legal fields updated by the legal user get mapped to the engagement's Legal Matters tab.
                    engagementDetails.ClickTabComplianceLegalLV();
                    engagementDetails.CLickTabLegalMattersLV();

                    Assert.AreEqual(valLegalHold, opportunityDetails.GetLegaHoldLV());
                    extentReports.CreateStepLogs("Passed", "Legal Hold selection is: '" + valLegalHold + "' mapped on Engagement page after conversion from Opportunity");

                    Assert.AreEqual(valLegalHoldNotes, opportunityDetails.GetLegalHoldNotesLV());
                    extentReports.CreateStepLogs("Passed", "Legal Hold Notes: '" + valLegalHoldNotes + "' mapped on Engagement page after conversion from Opportunity");

                    Assert.AreEqual(valDateOnHold, opportunityDetails.GetDateOnHoldLV());
                    extentReports.CreateStepLogs("Passed", "Date On Hold: '" + valDateOnHold + "' mapped on Engagement page after conversion from Opportunity");

                    Assert.AreEqual(valPutOnHold, opportunityDetails.GetPutOnHoldLV());
                    extentReports.CreateStepLogs("Passed", "Put On Hold: '" + valPutOnHold + "' mapped on Engagement page after conversion from Opportunity");

                    randomPages.CloseActiveTab(engagementName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", userLegal + " Legal User logged out ");
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
