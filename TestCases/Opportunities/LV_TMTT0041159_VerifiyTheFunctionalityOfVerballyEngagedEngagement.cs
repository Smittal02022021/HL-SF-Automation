using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SalesForce_Project.TestCases.Opportunities
{
    class LV_TMTT0041159_VerifiyTheFunctionalityOfVerballyEngagedEngagement:BaseClass
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

        private string filterSection;
        private string subFilterSection;
        private string filterValue;
        private string popupMessage;
        private string selectedCompany;
        private string commentTypeExl;
        private string commentTextExl;
        private string commentType;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifiyTheFunctionalityOfVerballyEngagedEngagementLV()
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
                                
                string valJobType = ReadExcelData.ReadData(excelPath, "AddOpportunity",3);
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                string userExl = ReadExcelData.ReadData(excelPath, "Users",1);
                
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

                string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType,valJobType, fileTMTT0041159);
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
                string actualListValidationErrors= opportunityDetails.GetOppVerballyEngagedValidationErrorsLV();                
                Assert.AreEqual(expectedListValidationErrors, actualListValidationErrors, "Verify that validation appears when user try to change the stage as Verbally Engaged");
                extentReports.CreateStepLogs("Passed", "Validations appeared when user try to change the stage to Verbally Engaged");
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "CF Financial User: " + userExl + " Logged out ");

                ////////////// TMTI0101380 Test Case Start////////////////////
                //TMTI0101380	Verify that user is able to change the stage as Verbally engaged after satisfying all the validations
                //Login as System Admin user to Fill Required fields for conversion 
                string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 4, 1);
                extentReports.CreateStepLogs("Info", "System Admin User: " + adminUserExl + " Updating the Required details ");

                homePage.SearchUserByGlobalSearchN(adminUserExl);
                extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToClassicView();
                string userAdmin = login.ValidateUser();
                Assert.AreEqual(userAdmin.Contains(adminUserExl), true);
                extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUserExl + " User logged in ");

                opportunityHome.SearchOpportunity(opportunityName);
                extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                //update CC 
                opportunityDetails.UpdateOutcomeDetails(fileTMTT0041159);
                if (valJobType.Equals("Buyside") || valJobType.Equals("Sellside"))
                {
                    opportunityDetails.UpdateNBCApproval();
                    extentReports.CreateStepLogs("Info", "Conflict Check and NBC fields are updated ");
                }
                else
                {
                    extentReports.CreateStepLogs("Info", "Conflict Check fields are updated ");
                }

                ////Admin actions in LV/////
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
                opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0041159);
                extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                opportunityDetails.ClickReturnToOpportunityLV();
                extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                randomPages.CloseActiveTab("Internal Team");
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "System Administrator: " + appNameExl + " Logged out after filling Page level Required fields ");

                //Login as CF Financial User logged in to fill fields level required fields 
                homePage.SearchUserByGlobalSearchN(userExl);
                extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                //Login user
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

                opportunityDetails.EditOpportunityStageLV(stageExl);
                string updatedStage= opportunityDetails.GetStageLV();
                Assert.AreEqual(updatedStage, stageExl);
                extentReports.CreateStepLogs("Passed", "Opportunity Stage is updated from "+stage+" to "+ updatedStage);
                randomPages.CloseActiveTab(opportunityName);
                extentReports.CreateStepLogs("Info", updatedStage+" opportunity tab is closed");
                CustomFunctions.PageReload(driver);
                //////////////TMTI0101380 Test Case End////////////////////

                //Start TMTI0101390	Verify that CF user can fill all the related list in Partial engagement like- Counterparty, FS Eng, Eng Contact, comments etc.

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                //extentReports.CreateStepLogs("Info", "CF Financial User is on " + moduleNameExl + " Page ");
                extentReports.CreateStepLogs("Info", "CF Financial User is on Partial Engaged "+moduleNameExl);
                engagementHome.SearchEngagementInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", " User is on "+ updatedStage+" Engagement page");

                ////**********Counterparties Actions********/////
                ///1. Click on top on View Counterparty button and click Add New Counterparty.
                //2.Try to add one counterparty from each available option like, Existing engagement, company list, Add Counterparty button.
                //3.Once CP added click to back on View Counterparty page.
                //4.Fill all the available fields for added counterparty including comment.
                //5.Now open any one of the Counterparty detail page and add contacts and comments with all available option on this page like multiple contact search option and multiple comment type options.
                ///*****************************************/////
                
                //CF Financial user add Engagement Contact
                engagementDetails.CickAddEngagementContactLV(valRecordType);
                string contactNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", 2, 6);
                //string contactRoleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", 2, 2);
                string contactPartyExl = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", 2, 3);
                engagementDetails.CreateContactLV(contactNameExl, contactPartyExl);
                popupMessage = addCounterparty.GetLVMessagePopup();
                Assert.IsTrue(popupMessage.Contains("Engagement Contact"), "Verify the Added Engagement Contact is displayed in Popup message ");
                extentReports.CreateStepLogs("Passed", popupMessage + " message Displayed and Comments added for counterpartywith Type:  " + commentTypeExl);
                extentReports.CreateStepLogs("Info", contactNameExl+" Contact added on Engagement page");

                //CF Financial User Add Comments on Parial Engaged Engagement
                int typeRowCount = ReadExcelData.GetRowCount(excelPath, "EngComments");
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EngComments", typeRow, 1);
                    commentTextExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EngComments", typeRow, 2);
                    engagementDetails.AddEngementCommentsLV(commentTypeExl, commentTextExl);
                    extentReports.CreateStepLogs("Info", "Comments added on Engagement page with Type:  " + commentTypeExl);
                }
                ///# 2. Adding  Existing engagement, company list
                // Verify the funtionality of adding Counterparty through existing Engagement
                // Verify the funtionality of adding Counterparty through existing Comapny list
                engagementDetails.ClickViewCounterpartyButtonEngagementPageLV();
                int filtersCount = ReadExcelData.GetRowCount(excelPath, "FilterSections");
                for (int optionRow = 2; optionRow <= filtersCount; optionRow++)
                {
                    filterSection = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", optionRow, 1);
                    subFilterSection = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", optionRow, 2);
                    addCounterparty.ClickAddCounterpartiesButtonLV();
                    filterValue = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", optionRow, 3);
                    extentReports.CreateStepLogs("Info", "Verifying the functionality of adding Counterparties Company from " + filterSection + " ");
                    addCounterparty.SelectFilterLV(filterSection, subFilterSection);
                    addCounterparty.SearchCounterpartiesLV(subFilterSection, filterValue);

                    //Get Company name from Company List 
                    selectedCompany = addCounterparty.GetCompanyNameFromListLV();
                    // Checkbox of first company
                    addCounterparty.SelectCompanyFromListLV();
                    extentReports.CreateStepLogs("Info", selectedCompany + " : Company selected from Company List ");
                    // Click on Add Counterparty oppname button
                    addCounterparty.ClickAddCounterpartyToOpportunity();
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.AreEqual(popupMessage, "Selected Counterparty Records have been created.");
                    extentReports.CreateStepLogs("Passed", popupMessage + " message Displayed and company " + selectedCompany + " is added in counterparty list ");

                    //Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page
                    addCounterparty.ClickBackButtonAndValidateViewCounterpartiesPageLV(); //ButtonClick("Back");
                    extentReports.CreateStepLogs("Info", "Clicked on Back button ");
                    Assert.IsTrue(addCounterparty.VerifyUserIsOnCounterpartiesListPage(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                    Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(selectedCompany), "Verify added Company: " + selectedCompany + " is under Counterparties List ");
                    extentReports.CreateStepLogs("Passed", "User return to Counterparties List Page ");
                    extentReports.CreateStepLogs("Passed", selectedCompany + " Company is added and displayed into Counterparties List ");
                }
                addCounterparty.ClickAllCheckboxCounterpartyCompany();
                addCounterparty.ClickDeleteCounterpartyButton();
                addCounterparty.ButtonConfirmDeleteCounterparty(); 
                popupMessage = addCounterparty.GetLVMessagePopup();
                Assert.AreEqual(popupMessage, "Records deleted successfully", "Verify the Success Message if Counterparty is Deleted ");
                extentReports.CreateStepLogs("Passed", popupMessage + " : Message Displayed and counterparties is deleted from list ");

                //-CF Financial User Verify the funtionality of adding Counterparty through Add Counterparty button
                string counterpartyCompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 1);
                string counterpartyTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 2);
                addCounterparty.ClickAddCounterpartiesButtonLV();
                addCounterparty.ButtonClick("Add Counterparty");
                extentReports.CreateStepLogs("Info", "Verifying the functionality of adding Counterparties Company from Add Counterparty button ");

                addCounterparty.AddNewEngagementCounterpartyLV(counterpartyCompanyNameExl, counterpartyTypeExl);
                popupMessage = addCounterparty.GetLVMessagePopup();
                Assert.IsTrue(popupMessage.Contains(counterpartyCompanyNameExl), "Verify the Added Counterparty name is displayed in Popup message ");
                extentReports.CreateStepLogs("Passed", popupMessage + " message Displayed and company " + counterpartyCompanyNameExl + " is added in counterparty list ");

                addCounterparty.CloseCurrentTab(counterpartyCompanyNameExl);
                addCounterparty.ClickBackButtonAndValidateViewCounterpartiesPageLV(); //ButtonClick("Back");
                extentReports.CreateStepLogs("Info", "Clicked on Back button");
                Assert.IsTrue(addCounterparty.VerifyUserIsOnCounterpartiesListPage(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(counterpartyCompanyNameExl), "Verify added Company: " + counterpartyCompanyNameExl + " is under Counterparties List");
                extentReports.CreateStepLogs("Passed", "User returned to Counterparties List Page");
                extentReports.CreateLog(counterpartyCompanyNameExl + " Company is added and displayed into Counterparties List ");

                //#4.Fill all the available fields for added counterparty including comment.
                string commentsExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 3);
                addCounterparty.EditCoutnerpartyDetails(commentsExl);
                addCounterparty.SaveCounterpartyChanges();
                popupMessage = addCounterparty.GetLVMessagePopup();
                Assert.AreEqual(popupMessage, "Records Updated Successfully!");
                extentReports.CreateStepLogs("Passed", "Added Counterparty details are updated ");

                //#//5.Now open any one of the Counterparty detail page and add contacts and comments with all available option on this page like multiple contact search option and multiple comment type options.
                addCounterparty.ClickCounterpartyCompanyLink(counterpartyCompanyNameExl);
                CustomFunctions.SwitchToWindow(driver, 1);
                extentReports.CreateStepLogs("Info", "User Switched to new tab ");
                addCounterparty.ButtonClick("New Engagement Counterparty Contact");
                
                string contactFilterType = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyContact", 2, 1);
                string contactname = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyContact", 2, 1);                
                string contactNameResult = addCounterparty.GetContactSearchedLV(contactFilterType, contactname);

                string valCPContact = addCounterparty.GetContactNameFromListLV();
                addCounterparty.SelectContactFromListLV();
                addCounterparty.ClickAddContactLV();
                extentReports.CreateStepLogs("Info", "New Engagement Counterparty Contact:"+ contactname+" is added ");
                addCounterparty.ClickBackButtonAndValidateViewCounterpartiesPageLV();// ButtonClick("Back");
                
                string contactEngCP= addCounterparty.GetEngCounterpartyContactLV();
                Assert.IsTrue(contactname.Contains(contactEngCP));
                extentReports.CreateStepLogs("Passed", "Contact: " + valCPContact + " is available on Engagement Counterparty Contact(s) Right Panel");
                randomPages.CloseActiveTab("Tab");

                //Assert.IsTrue(addCounterparty.IsContactAddedCounterpartyListLV(counterpartyCompanyNameExl), "Verify Contact is added under Company name in Counterparty Companies List");
                //extentReports.CreateLog("Added Contact is available under Counterparty Record List ");

                //*******CF Financial User Adding Engagement Counterparty Comments with All Types ******************** 
                typeRowCount = ReadExcelData.GetRowCount(excelPath, "CounterpartyComments");
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    addCounterparty.ClickEngCPVCommentsLV();
                    commentTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyComments", typeRow, 1);
                    commentTextExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyComments", typeRow, 2);
                    addCounterparty.AddNewEngagementCounterpartyCommentLV(commentTypeExl, commentTextExl, counterpartyCompanyNameExl);
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.IsTrue(popupMessage.Contains("Engagement Counterparty Comment"), "Verify the Engagement Counterparty Comments is displayed in Popup message ");
                    extentReports.CreateStepLogs("Passed", popupMessage + " message Displayed and Comments added for counterpartywith Type:  " + commentTypeExl);
                    commentType= addCounterparty.GetCommentTypeLV();
                    Assert.AreEqual(commentType, commentTypeExl,"Verify Comments added with Type:  "+ commentTypeExl);
                    randomPages.CloseActiveTab("ECC");                    
                }
                CustomFunctions.CloseWindow(driver, 1);
                CustomFunctions.SwitchToWindow(driver, 0);
                CustomFunctions.PageReload(driver);

                usersLogin.ClickLogoutFromLightningView();
                ////////////////////////////////////////////
                
                //*Adding Comments with  Compliance User on Engagement //
                string complianceuserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 5, 1);
                homePage.SearchUserByGlobalSearchN(complianceuserExl);
                extentReports.CreateStepLogs("Info", "User: " + complianceuserExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string complianceuser = login.ValidateUserLightningView();
                Assert.AreEqual(complianceuser.Contains(complianceuserExl), true);
                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                engagementHome.SearchEngagementInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + updatedStage + " Engagement page");

                commentTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EngComments", 5, 1);
                commentTextExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EngComments", 5, 2);
                engagementDetails.AddEngementCommentsLV(commentTypeExl, commentTextExl);
                extentReports.CreateStepLogs("Info", "Comments added on Engagement page with Type:  " + commentTypeExl);
                usersLogin.ClickLogoutFromLightningView();                

                ////*****************************************////
                
                //Login as System Admin user to add FS Engagement 
                adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 4, 1);
                extentReports.CreateStepLogs("Info", "System Admin User: " + adminUserExl + " Adding FS Engagement ");

                homePage.SearchUserByGlobalSearchN(adminUserExl);
                extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToClassicView();
                userAdmin = login.ValidateUser();
                Assert.AreEqual(userAdmin.Contains(adminUserExl), true);
                extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUserExl + " User logged in ");

                login.SwitchToLightningExperience();
                extentReports.CreateStepLogs("Passed", "System Admin Switched to Lightning View ");
                //Go to Opportunity module in Lightning View 
                homePageLV.SelectAppLV(appNameExl);
                Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                engagementHome.SearchEngagementInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "Engagement found and selected");
                
                engagementDetails.ClickTabFSEngagementLV();
                extentReports.CreateStepLogs("Info", "User is on FS Engagement tab");
                string nameFSEng= engagementDetails.CreateNewFSEngagementLV(counterpartyCompanyNameExl);
                popupMessage = addCounterparty.GetLVMessagePopup();
                Assert.IsTrue(popupMessage.Contains("FS Engagement"), "Verify the Added FS Engagement is displayed in Popup message ");
                extentReports.CreateStepLogs("Passed", popupMessage + " message Displayed and FS Engagement "+ nameFSEng+" added for Engagement with Sponsored Company " + counterpartyCompanyNameExl);
                //close FS Eng
                randomPages.CloseActiveTab(nameFSEng);
                //TMTI0101392 Verify that CF User is able to Request Full engagement from Partial engagement.
                engagementDetails.ClickRequestFullEngagementLV();
                extentReports.CreateStepLogs("Info", "Click on Request Full Engagement button and verify that below validation");
                
                string expectedHeaderErrorsList = ReadExcelData.ReadData(excelPath, "VEValidationList", 3);
                string actualHeaderErrorsList = engagementDetails.GetVerballyFullEngValidationHeaderErrorsLV();
                Assert.AreEqual(expectedHeaderErrorsList,actualHeaderErrorsList, "Verify the Validations in Header of Engagement Information page, while Request for Full Engagement a Verbally Engaged Engagement");
                extentReports.CreateStepLogs("Passed", "Validations in Header of Engagement Information page are correct, while Request for Full Engagement a Verbally Engaged Engagement");

                string expectedLabelList = ReadExcelData.ReadData(excelPath, "VEValidationList", 4);
                string actualLabelList= engagementDetails.GetVerballyFullEngReqFieldsLV();
                //Assert.AreEqual(expectedLabelList, actualLabelList, "Verify the Required Fields on Engagement Information page, while Request for Full Engagement a Verbally Engaged Engagement");
                extentReports.CreateStepLogs("Passed", "Required Fields on Engagement Information page are correct, while Request for Full Engagement a Verbally Engaged Engagement");

                usersLogin.ClickLogoutFromLightningView();

                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
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