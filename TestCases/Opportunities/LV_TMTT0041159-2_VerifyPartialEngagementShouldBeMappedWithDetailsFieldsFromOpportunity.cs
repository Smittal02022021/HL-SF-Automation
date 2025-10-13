using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;
using Microsoft.Office.Interop.Excel;

namespace SF_Automation.TestCases.OpportunitiesCounterparty
{
    class LV_TMTT0041159_2_VerifyPartialEngagementShouldBeMappedWithDetailsFieldsFromOpportunity:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();
        RandomPages randomPages = new RandomPages();
        AddOppCounterparty addCounterparty = new AddOppCounterparty();

        public static string fileTMTT0041159 = "LV_TMTT0041159_VerifiyTheFunctionalityOfVerballyEngagedCFEngagement";

        private string popupMessage;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyPartialEngagementShouldBeMappedWithDetailsFieldsFromOpportunityLV() 
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0041159;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                string valJobType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 3);
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                string userExl = ReadExcelData.ReadData(excelPath, "Users", 1);

                //Login as Standard User profile and validate the user
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
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                //TMTI0101376 Verify that CF User is able to create new Opportunity
                //Validating Title of New Opportunity Page
                string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileTMTT0041159);
                extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0041159);
                Assert.AreEqual(displayedTab, "Info");
                extentReports.CreateStepLogs("Info", "User is on Opportunity detail " + displayedTab + " tab ");

                //Validating Opportunity details  
                string oppName = opportunityDetails.GetOpportunityNameL();
                string oppNumber = opportunityDetails.GetOpportunityNumberL();
                Assert.IsNotNull(oppNumber);
                extentReports.CreateStepLogs("Passed", "Opportunity with number : " + oppNumber + " is created ");

                //TMTI0101378 Verify that validation appears when user try to change the stage as Verbally Engaged.
                //Change Stage of the created opportunity//
                string stage = opportunityDetails.GetStageLV();
                string stageExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 31);
                opportunityDetails.EditOpportunityStageLV(stageExl);
                string expectedListValidationErrors = ReadExcelData.ReadData(excelPath, "VEValidationList", 1);
                string actualListValidationErrors = opportunityDetails.GetOppVEValidationErrorsLV();
                Assert.AreEqual(expectedListValidationErrors, actualListValidationErrors, "Verify that validation appears when user try to change the stage as Verbally Engaged");
                extentReports.CreateStepLogs("Passed", "Validations appeared when user try to change the stage to Verbally Engaged");
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "CF Financial User: " + userExl + " Logged out ");

                ////////////// TMTI0101380 Test Case Start////////////////////
                //TMTI0101380	Verify that user is able to change the stage as Verbally engaged after satisfying all the validations
                //Login as System Admin user to Fill Required fields for conversion 
                string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 4, 1);
                extentReports.CreateStepLogs("Info", "System Admin User: " + adminUserExl + " Updating the Required details ");

                homePage.SearchUserByGlobalSearchN(adminUserExl);
                extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string userAdmin = login.ValidateUserLightningView();
                Assert.AreEqual(userAdmin.Contains(adminUserExl), true);
                extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUserExl + " User logged in ");

                //Go to Opportunity module in Lightning View 
                homePageLV.SelectAppLV(appNameExl);
                Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                //Search for created opportunity
                opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                opportunityDetails.UpdateOutcomeNBCApproveDetailsLV(valJobType);
                extentReports.CreateStepLogs("Info", "Conflict Check and NBC details are provided");
                //////Standard User don't have permission to modify the Internal team so System Admin is modifying the roles////////
                opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0041159);
                extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                opportunityDetails.ClickReturnToOpportunityLV();
                extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                randomPages.CloseActiveTab("Internal Team");
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "System Administrator: " + appNameExl + " Logged out after filling Page level Required fields ");

                //Login as CF Financial User logged in to fill fields level required fields 
                homePage.SearchUserByGlobalSearchN(userExl);
                extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(userExl), true);
                extentReports.CreateStepLogs("Passed", "User: " + userExl + " logged in on Lightning View");

                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                //Search for created opportunity
                opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");

                opportunityDetails.EnterVerballyEngagedRequiredFieldsLV(valJobType, fileTMTT0041159);
                extentReports.CreateStepLogs("Info", "Entered All Field level Required values");

                //CF Financial user add Opportunity Contact
                opportunityDetails.CickAddOpportunityContactLV(valRecordType);
                string contactNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", 2, 1);
                string contactPartyExl = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", 2, 3);
                opportunityDetails.CreateContactLV(contactNameExl, contactPartyExl);
                popupMessage = randomPages.GetLVMessagePopup();
                Assert.IsTrue(popupMessage.Contains("Opportunity Contact"), "Verify the Added Engagement Contact is displayed in Popup message ");
                extentReports.CreateStepLogs("Pass", contactNameExl + " Contact added on Engagement page(Required for Verbally Engaged Stage). Hence CF user is able to edit the Partial Engagement");

                //Add & Get Opp comments                
                int typeRowCount = ReadExcelData.GetRowCount(excelPath, "OppComments");                
                string commentTypeOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OppComments", 3, 1);
                string commentTextOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OppComments", 3, 2);
                opportunityDetails.ClickOppNewCommentsLV();
                opportunityDetails.AddNewOppCommentLV(commentTypeOppExl, commentTextOppExl);
                extentReports.CreateStepLogs("Info", "Comments added on Opportunity page with Type:  " + commentTypeOppExl);
                string commentOppType = addCounterparty.GetCommentTypeLV();
                Assert.AreEqual(commentOppType, commentTypeOppExl, "Verify Comments added with Type:  " + commentTypeOppExl);
                randomPages.CloseActiveTab(opportunityDetails.GetCommentIDLV());
                 
                //Adding Counterparty through Add Counterparty button
                opportunityDetails.ClickOnViewCounterpartyButton();   
                string counterpartyCompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 1);
                string counterpartyTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 2);
                addCounterparty.ClickAddCounterpartiesButtonLV();
                addCounterparty.ButtonClick("Add Counterparty");
                extentReports.CreateStepLogs("Info", "Verifying the functionality of adding Counterparties Company from Add Counterparty button ");

                addCounterparty.AddNewCounterpartyLV(counterpartyCompanyNameExl, counterpartyTypeExl);
                popupMessage = randomPages.GetLVMessagePopup();
                Assert.IsTrue(popupMessage.Contains(counterpartyCompanyNameExl), "Verify the Added Counterparty name is displayed in Popup message ");
                extentReports.CreateStepLogs("Passed", popupMessage + " message Displayed and company " + counterpartyCompanyNameExl + " is added in counterparty list ");

                addCounterparty.CloseOppCounterpartyPage(counterpartyCompanyNameExl);
                addCounterparty.ClickBackButtonAndValidateViewCounterpartiesPageLV(); //ButtonClick("Back");
                extentReports.CreateStepLogs("Info", "Clicked on Back button");
                Assert.IsTrue(addCounterparty.IsCounterpartiesListDisplayed(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(counterpartyCompanyNameExl), "Verify added Company: " + counterpartyCompanyNameExl + " is under Counterparties List");
                extentReports.CreateStepLogs("Passed", "User returned to Counterparties List Page");
                extentReports.CreateLog(counterpartyCompanyNameExl + " Company is added and displayed into Counterparties List ");

                ////////*****Opp Counterparty detail page chagned no way to/check Opportunity Counterparty Contact,Comments*******/////
                ///
                extentReports.CreateStepLogs("Info", "*****Opportunities Counterparty detail page chagned no way to add/check Opportunity Counterparty Contact,Comments******* ");
                //Add & Get Counterparty Contact
                                
                addCounterparty.ClickCounterpartyCompanyLink(counterpartyCompanyNameExl);
                CustomFunctions.SwitchToWindow(driver, 1);
                extentReports.CreateStepLogs("Info", "User Switched to new tab ");
                addCounterparty.ButtonClick("New Opportunity Counterparty Contact");

                string contactFilterType = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyContact", 4, 2);
                string contactNameCPExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyContact", 2, 1);
                string contactNameResult = addCounterparty.GetContactSearchedLV(contactFilterType, contactNameCPExl);

                string valCPContact = addCounterparty.GetContactNameFromListLV();
                addCounterparty.SelectContactFromListLV();
                addCounterparty.ClickAddContactLV();
                extentReports.CreateStepLogs("Info", "New Opportunity Contact:" + contactNameCPExl + " is added ");
                addCounterparty.ClickBackButtonAndValidateViewCounterpartiesPageLV();
                CustomFunctions.PageReload(driver);

                /*
                string contactOppCP = addCounterparty.GetOppCounterpartyContactLV();
                Assert.IsTrue(contactNameCPExl.Contains(contactOppCP));
                extentReports.CreateStepLogs("Passed", "Contact: " + valCPContact + " is available on Opportunity Counterparty Contact(s) Right Panel");
                randomPages.CloseActiveTab("Tab");
                */

                extentReports.CreateStepLogs("Info", "********Opportunity CP add Comments button removed from footer as well on CP Detail page************");
                /*
                //Add & Get Counterparty Comments
                addCounterparty.ClickAddOppCPCommentsLV();// remove text from comopany
                string commentTypeCPExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyComments", 3, 1);
                string commentTextCPExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyComments", 3, 2);
                addCounterparty.AddNewOpportunityCounterpartyCommentLV(commentTypeCPExl, commentTextCPExl, counterpartyCompanyNameExl);
                popupMessage = randomPages.GetLVMessagePopup();
                Assert.IsTrue(popupMessage.Contains("Opportunity Counterparty Comment"), "Verify the Opportunity Counterparty Comments is displayed in Popup message ");
                extentReports.CreateStepLogs("Passed", "Comments added for counterparty with Type:  " + commentTypeCPExl);
                string commentTypeCP = addCounterparty.GetCommentTypeLV();
                Assert.AreEqual(commentTypeCP, commentTypeCPExl, "Verify Comments added with Type:  " + commentTypeCPExl);
                randomPages.CloseActiveTab("OCC");

                */
                CustomFunctions.CloseWindow(driver, 1);
                CustomFunctions.SwitchToWindow(driver, 0);
                CustomFunctions.PageReload(driver);
                randomPages.CloseActiveTab("Counterparty Editor");
                
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + userExl + " logged out");


                //Login as System Admin user to add FS Opportunity 
                //string userFSExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 6, 1);

                //homePage.SearchUserByGlobalSearchN(userFSExl);
                //extentReports.CreateStepLogs("Info", " FS User: " + userFSExl + " details are displayed. ");
                //usersLogin.LoginAsSelectedUser();
                //login.SwitchToLightningExperience();
                //extentReports.CreateStepLogs("Passed", "FS User Switched to Lightning View ");
                ////Go to Opportunity module in Lightning View 
                //homePageLV.SelectAppLV(appNameExl);
                //Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                //extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                //homePageLV.SelectModule(moduleNameExl);
                //extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                //opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                //extentReports.CreateStepLogs("Info", "Opportunity found and selected");

                //opportunityDetails.ClickTabFSOppLV();
                //extentReports.CreateStepLogs("Info", "User is on FS Opportunity tab");
                //string idFSOpp = opportunityDetails.CreateNewFSOppLV(counterpartyCompanyNameExl);
                //popupMessage = randomPages.GetLVMessagePopup();
                //Assert.IsTrue(popupMessage.Contains("FS Opp"), "Verify the Added FS Opportunity is displayed in Popup message ");
                //extentReports.CreateStepLogs("Passed", " FS Opportunity " + idFSOpp + " added for Opportunity with Sponsored Company: " + counterpartyCompanyNameExl);
                //randomPages.CloseActiveTab(idFSOpp);
                //randomPages.CloseActiveTab(opportunityName);
                //homePageLV.LogoutFromSFLightningAsApprover();
                //extentReports.CreateStepLogs("Passed", "FS User: " + userFSExl + " logged out");

                extentReports.CreateStepLogs("Info", "System Admin : " + adminUserExl + " Updating the Required details ");

                homePage.SearchUserByGlobalSearchN(adminUserExl);
                extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                userAdmin = login.ValidateUserLightningView();
                Assert.AreEqual(userAdmin.Contains(adminUserExl), true);
                extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUserExl + " User logged in ");

                //Go to Opportunity module in Lightning View 
                homePageLV.SelectAppLV(appNameExl);
                Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                //Search for created opportunity
                opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");

                opportunityDetails.ClickTabFSOppLV();
                extentReports.CreateStepLogs("Info", "User is on FS Opportunity tab");
                string idFSOpp = opportunityDetails.CreateNewFSOppLV(counterpartyCompanyNameExl);
                popupMessage = randomPages.GetLVMessagePopup();
                Assert.IsTrue(popupMessage.Contains("FS Opp"), "Verify the Added FS Opportunity is displayed in Popup message ");
                extentReports.CreateStepLogs("Passed", " FS Opportunity " + idFSOpp + " added for Opportunity with Sponsored Company: " + counterpartyCompanyNameExl);
                randomPages.CloseActiveTab(idFSOpp);

                opportunityDetails.EditOpportunityStageLV(stageExl);
                string updatedStage = opportunityDetails.GetStageLV();
                Assert.AreEqual(updatedStage, stageExl);
                extentReports.CreateStepLogs("Passed", "Opportunity Stage is updated from " + stage + " to " + updatedStage);
                Assert.IsTrue(randomPages.GetVerballyEngCheckboxStatusLV(), "Verify Verbally Engaged checkbox is Checked after stage change of the Opportunity to Verbally Engaged");
                extentReports.CreateStepLogs("Passed", "Verbally Engaged checkbox is Checked after stage change of the Opportunity to Verbally Engaged");
                CustomFunctions.PageReload(driver);
                
                //TMTI0101382	Verify that changing stage to Verbally Engaged will create a Partial engagement
                //Click Eng Link from right panel 
                Assert.IsTrue(opportunityDetails.IsVerballyEngagedEngCreatedLV(opportunityName), "Verify changing stage to Verbally Engaged creates a Partial Engagement");
                extentReports.CreateStepLogs("Passed", "Changing stage to Verbally Engaged creates a Partial Engagement");
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUserExl + " logged out");

                string userFSExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 6, 1);
                homePage.SearchUserByGlobalSearchN(userFSExl);
                extentReports.CreateStepLogs("Info", " FS User: " + userFSExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                extentReports.CreateStepLogs("Passed", "FS User Switched to Lightning View ");
                //Go to Opportunity module in Lightning View 
                homePageLV.SelectAppLV(appNameExl);
                Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");                

                //TMTI0101384 Verify that Partial Engagement should be mapped with all the details filled from Opportunity
                //Validate relevant objects on VE Eng 
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");                
                engagementHome.GlobalSearchEngagementInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "Engagement found and selected");

                //TMTI0101390: Verify that CF user can fill all the related list in Partial engagement like - Counterparty, FS Eng, Eng Contact, comments etc.
                // Click FS Eng
                engagementDetails.ClickTabFSEngagementLV();
                string idFSEng=engagementDetails.GetFSEngagementIDLV();
                string engSponsorCmpny= engagementDetails.GetFSEngSponsorCompanyLV();
                Assert.AreEqual(counterpartyCompanyNameExl, engSponsorCmpny);
                extentReports.CreateStepLogs("Passed", "FS Engagement with ID: " + idFSEng + " and Sponsor Company: "+ counterpartyCompanyNameExl+" is mapped on Engagement page same as added on Oporunity page  ");
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "FS User: " + userFSExl + " logged out");

                //Login as CF Financial User logged in to fill fields level required fields 
                homePage.SearchUserByGlobalSearchN(userExl);
                extentReports.CreateStepLogs("Info", "CF User: " + userExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(userExl), true);
                extentReports.CreateStepLogs("Passed", "User: " + userExl + " logged in on Lightning View");

                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher "); 
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "CF Fin User is on " + moduleNameExl + " Page ");
                engagementHome.GlobalSearchEngagementInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "Engagement found and selected");
                                
                //Validate Contact
                engagementDetails.ClickEngContactTabLV();
                Assert.IsTrue(engagementDetails.IsEngContactPresentLV(contactNameExl),"Verify Opportuniy Contact is present on VE Engagement page as well ");
                extentReports.CreateStepLogs("Passed", "Contact added on Opportuniy page is present on VE Engagement page as well ");

                //*****Opportunities Counterparty detail page chagned no way to add/check Opportunity Counterparty Contact,Comments*******
                extentReports.CreateStepLogs("Info", "*****Opportunities Counterparty detail page chagned no way to check Engagement Counterparty Contact,Comments******* ");
                
                
                //Validate Eng comments
                engagementDetails.ClickEngInfoCommentsTabLV();
                string commentsEng= engagementDetails.GetEngCommentPresentLV(commentOppType);
                Assert.AreEqual(commentTextOppExl, commentsEng, "Verify Comments added on Opportunity page is available on VE Engagement Comments page");
                
                // Validate Counterparties
                engagementDetails.ClickViewCounterpartiesButton();
                Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(counterpartyCompanyNameExl), "Verify added Company: " + counterpartyCompanyNameExl + " is under Counterparties List from Opportunity Page is available on VE Engagement Counterparty ");
                extentReports.CreateStepLogs("Passed", "Counterparty Company: " + counterpartyCompanyNameExl + " Added from Opportunity Page is available on VE Engagement Counterparty");
                // Validate Counterparties Contact
                addCounterparty.ClickCounterpartyCompanyLink(counterpartyCompanyNameExl);
                CustomFunctions.SwitchToWindow(driver, 1);
                extentReports.CreateStepLogs("Info", "User Switched to Counterparty detail tab ");
                // Get Counterparty Contact
                string contactEngCP = addCounterparty.GetEngCounterpartyContactLV();
                Assert.IsTrue(contactNameCPExl.Contains(contactEngCP));//, contactNameCPExl); // contactOppCP);
                extentReports.CreateStepLogs("Passed", "Counter Contact: " + valCPContact + " sdded on Opportunity page is available on VE Engagement Counterparty Contact(s) Right Panel");

                extentReports.CreateStepLogs("Info", "********Opportunity CP add Comments button removed from footer as well on CP Detail page************");
                /*
                // Validate Counterparties Comments
                addCounterparty.ClickViewAllEngCPCommentsLV();
                Assert.IsTrue(addCounterparty.IsEngCPCommentPresentLV(commentTypeCP), "Verify Comments Type added on Opportunity page is availale on VE Engagement Counterparty Comment(s) Right Panel");
                extentReports.CreateStepLogs("Passed", "Counterparty Comments Type: " + commentTypeCP + "  added on Opportunity page is availale on VE Engagement Counterparty Comment(s) Right Panel");
                randomPages.CloseActiveTab("Engagement Counterpart Comments");
                randomPages.CloseActiveTab("EC");
                */

                CustomFunctions.CloseWindow(driver, 1);
                CustomFunctions.SwitchToWindow(driver, 0);
                randomPages.CloseActiveTab("Counterparty Editor");
                
                randomPages.CloseActiveTab(opportunityName);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + userExl + " logged out");
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
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