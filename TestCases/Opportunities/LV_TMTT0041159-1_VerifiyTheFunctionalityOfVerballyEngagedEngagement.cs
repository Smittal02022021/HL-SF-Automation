using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SF_Automation.TestCases.OpportunitiesCounterparty
{
    class LV_TMTT0041159_1_VerifiyTheFunctionalityOfVerballyEngagedEngagement:BaseClass
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
        private string commentTypeExl;
        private string commentTextExl;
        private string commentType;
        private string[] selectedCounterpartyCompany = new string[3];
        private int index = 0;
        private string valPEEstTransMCap;
        private string valPEEBITDA;
        private string valPERetainer;
        private string valPEProgressMonthlyFee;
        private string valPEContingentFee;
        private string valPETotalFee;
        private string valPESSExpense;
        private string valPEExpenseCap;
        private string valPELegalCap;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0113224 Verify that the "Tail Expires" is removed as a required field on converting CF Verbally Engaged Engagement to Full Engagement.
        //TMTI0118719 Verify that the"Location where Benefit was Provided" field validation does not appears on converting an opportunity to Verbally engaged.
        //TMTI0118721 Verify that the "Location where Benefit was Provided" field value is mapped to the partial engagement
        //TMTI0118723 Verify that the"Location where Benefit was Provided" field value is mapped from partial engegement to full engagement.
        

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
                string actualListValidationErrors= opportunityDetails.GetOppVEValidationErrorsLV();                
                Assert.AreEqual(expectedListValidationErrors, actualListValidationErrors, "Verify that validation appears when user try to change the stage as Verbally Engaged");
                extentReports.CreateStepLogs("Passed", "Validations appeared when user try to change the stage to Verbally Engaged");

                ///////*********************////////////////
                ///TMTI0118719	Verify that the "Location where Benefit was Provided" field validation does not appears on converting an opportunity to Verbally engaged.
                ///Verify the errorlist of required fields does not displays “Location where Benefit was Provided” field as required.
                extentReports.CreateStepLogs("Info", "Verify the errorlist of required fields does not displays “Location where Benefit was Provided” field as required.");
                string txtExpectedRequiredFieldsValidation = ReadExcelData.ReadDataMultipleRows(excelPath, "VEValidationList", 6, 1);
                Assert.IsFalse(actualListValidationErrors.Contains(txtExpectedRequiredFieldsValidation));
                extentReports.CreateStepLogs("Info", "The error list of required fields does not displays “Location where Benefit was Provided” field as required while change the stage of opportunity to Verbally Enaged");

                //Updating the "Location where Benefit was Provided" field
                string valBenefitExl = ReadExcelData.ReadDataMultipleRows(excelPath, "VEValidationList", 6, 2);
                opportunityDetails.InlineUpdateLocationBenefitValueLV(valBenefitExl);
                string locationBenefit = opportunityDetails.GetValueLocationBenefitLV();
                extentReports.CreateStepLogs("Info", "Location where Benefit is Provided value is: " + locationBenefit);

                //////////****************************/////////

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

                opportunityDetails.EditOpportunityStageLV(stageExl);
                string updatedStage= opportunityDetails.GetStageLV();
                Assert.AreEqual(updatedStage, stageExl);
                extentReports.CreateStepLogs("Passed", "Opportunity Stage is updated from "+stage+" to "+ updatedStage);
                Assert.IsTrue(randomPages.GetVerballyEngCheckboxStatusLV(),"Verify Verbally Engaged checkbox is Checked after stage change of the Opportunity to Verbally Engaged");
                extentReports.CreateStepLogs("Passed", "Verbally Engaged checkbox is Checked after stage change of the Opportunity to Verbally Engaged");

                //TMTI0101388	Verify that once Partial Engagement is created, CF user have access to edit the Partial Engagement
                //Edit Opportunity Description on VE opportnity 
                opportunityDetails.UpdateVEOpportunityDescription();
                string expectedValidationErrors = ReadExcelData.ReadData(excelPath, "VEValidationList", 5);
                string actualValidationErrors = opportunityDetails.GetOppVEValidationErrorsLV();
                Assert.AreEqual(expectedListValidationErrors, actualListValidationErrors, "Verify that validation appears when user try to change the stage as Verbally Engaged");
                extentReports.CreateStepLogs("Passed", "Validations:"+actualValidationErrors+" appeared when CF user try to update the Verbally Engaged Opportunity");


                randomPages.CloseActiveTab(opportunityName);
                extentReports.CreateStepLogs("Info", updatedStage+" opportunity tab is closed");                
                //////////////TMTI0101380 Test Case End////////////////////

                //Start TMTI0101390	Verify that CF user can fill all the related list in Partial engagement like- Counterparty, FS Eng, Eng Contact, comments etc.

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "CF Financial User is on Partial Engaged "+moduleNameExl);
                CustomFunctions.PageReload(driver);
                engagementHome.GlobalSearchEngagementInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", " User is on "+ updatedStage+" Engagement page");

                //TMTI0118721	Verify that the "Location where Benefit was Provided" field value is mapped to the partial engagement
                extentReports.CreateStepLogs("Info", "Verify that the 'Location where Benefit was Provided' field value is mapped to the partial engagement");
                Assert.AreEqual(locationBenefit, engagementDetails.GetValueLocationBenefitLV());
                extentReports.CreateStepLogs("Passed", "Location where Benefit is to be Provided field is mapped from opoortunity on Partial Engaged Engagement");
                
                /////////////*****************//////////////

                ////**********Counterparties Actions********/////
                ///1. Click on top on View Counterparty button and click Add New Counterparty.
                //2.Try to add one counterparty from each available option like, Existing engagement, company list, Add Counterparty button.
                //3.Once CP added click to back on View Counterparty page.
                //4.Fill all the available fields for added counterparty including comment.
                //5.Now open any one of the Counterparty detail page and add contacts and comments with all available option on this page like multiple contact search option and multiple comment type options.
                ///*****************************************/////

                //TMTI0101388	Verify that once Partial Engagement is created, CF user have access to edit the Partial Engagement
                //Adding Contact on Partial Engaged Egagement to CF user have access to edit the Partial Engagement

                //CF Financial user add Engagement Contact
                engagementDetails.CickAddEngagementContactLV(valRecordType);
                string contactNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", 2, 1);
                string contactPartyExl = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", 2, 3);
                engagementDetails.CreateContactLV(contactNameExl, contactPartyExl);
                popupMessage = randomPages.GetLVMessagePopup();
                Assert.IsTrue(popupMessage.Contains("Engagement Contact"), "Verify the Added Engagement Contact is displayed in Popup message ");
                extentReports.CreateStepLogs("Pass", contactNameExl+ " Contact added on Engagement page(Required for Verbally Engaged Stage). Hence CF user is able to edit the Partial Engagement");

                //CF Financial User Add Comments on Parial Engaged Engagement
                int typeRowCount = ReadExcelData.GetRowCount(excelPath, "EngComments");
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EngComments", typeRow, 1);
                    commentTextExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EngComments", typeRow, 2);
                    engagementDetails.AddEngementCommentsLV(commentTypeExl, commentTextExl);
                    extentReports.CreateStepLogs("Info", "Comments added on Parial Engaged Engagement page with Type:  " + commentTypeExl);
                    string engCommentsID= engagementDetails.GetEngagementCommentsID();
                    randomPages.CloseActiveTab(engCommentsID);
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
                    selectedCounterpartyCompany[index] = addCounterparty.GetCompanyNameFromListLV();
                    // Checkbox of first company
                    addCounterparty.SelectCompanyFromListLV();
                    extentReports.CreateStepLogs("Info", selectedCounterpartyCompany[index] + " : Company selected from Company List ");
                    // Click on Add Counterparty oppname button
                    addCounterparty.ClickAddCounterpartyToOpportunity();
                    popupMessage = randomPages.GetLVMessagePopup();
                    Assert.AreEqual(popupMessage, "Selected Counterparty Records have been created.");
                    extentReports.CreateStepLogs("Passed", popupMessage + " message Displayed and company " + selectedCounterpartyCompany[index] + " is added in counterparty list ");

                    //Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page
                    addCounterparty.ClickBackButtonAndValidateViewCounterpartiesPageLV();
                    extentReports.CreateStepLogs("Info", "Clicked on Back button ");
                    Assert.IsTrue(addCounterparty.IsCounterpartiesListDisplayed(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                    Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(selectedCounterpartyCompany[index]), "Verify added Company: " + selectedCounterpartyCompany[index] + " is under Counterparties List ");
                    extentReports.CreateStepLogs("Passed", "User return to Counterparties List Page ");
                    extentReports.CreateStepLogs("Passed", selectedCounterpartyCompany[index] + " Company is added and displayed into Counterparties List ");
                    index++;
                }
                
                //-CF Financial User Verify the funtionality of adding Counterparty through Add Counterparty button
                string counterpartyCompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 1);
                string counterpartyTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 2);
                addCounterparty.ClickAddCounterpartiesButtonLV();
                addCounterparty.ButtonClick("Add Counterparty");
                extentReports.CreateStepLogs("Info", "Verifying the functionality of adding Counterparties Company from Add Counterparty button ");

                addCounterparty.AddNewCounterpartyLV(counterpartyCompanyNameExl, counterpartyTypeExl);
                popupMessage = randomPages.GetLVMessagePopup();
                Assert.IsTrue(popupMessage.Contains(counterpartyCompanyNameExl), "Verify the Added Counterparty name is displayed in Popup message ");
                extentReports.CreateStepLogs("Passed", popupMessage + " message Displayed and company " + counterpartyCompanyNameExl + " is added in counterparty list ");

                addCounterparty.CloseEngCounterpartyPage(counterpartyCompanyNameExl);
                addCounterparty.ClickBackButtonAndValidateViewCounterpartiesPageLV(); //ButtonClick("Back");
                extentReports.CreateStepLogs("Info", "Clicked on Back button");
                Assert.IsTrue(addCounterparty.IsCounterpartiesListDisplayed(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(counterpartyCompanyNameExl), "Verify added Company: " + counterpartyCompanyNameExl + " is under Counterparties List");
                extentReports.CreateStepLogs("Passed", "User returned to Counterparties List Page");
                extentReports.CreateLog(counterpartyCompanyNameExl + " Company is added and displayed into Counterparties List ");
                selectedCounterpartyCompany[index] = counterpartyCompanyNameExl;
                //#4.Fill all the available fields for added counterparty including comment.
                string commentsExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 3);
                addCounterparty.EditCoutnerpartyDetailsLV(commentsExl);
                addCounterparty.SaveCounterpartyChanges();
                popupMessage = randomPages.GetLVMessagePopup();
                Assert.AreEqual(popupMessage, "Records Updated Successfully!");
                extentReports.CreateStepLogs("Passed", "**Comments are not being added*** Added Counterparty details are updated ");

                //#//5.Now open any one of the Counterparty detail page and add contacts and comments with all available option on this page like multiple contact search option and multiple comment type options.
                addCounterparty.ClickCounterpartyCompanyLink(counterpartyCompanyNameExl);
                CustomFunctions.SwitchToWindow(driver, 1);
                extentReports.CreateStepLogs("Info", "User Switched to new tab ");
                addCounterparty.ButtonClick("New Engagement Counterparty Contact");
                
                string contactFilterType = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyContact", 2, 1);
                string contactNameCPExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyContact", 2, 1);                
                string contactNameResult = addCounterparty.GetContactSearchedLV(contactFilterType, contactNameCPExl);

                string valCPContact = addCounterparty.GetContactNameFromListLV();
                addCounterparty.SelectContactFromListLV();
                addCounterparty.ClickAddContactLV();
                extentReports.CreateStepLogs("Info", "New Engagement Counterparty Contact: "+ contactNameCPExl + " is added ");
                addCounterparty.ClickBackButtonAndValidateViewCounterpartiesPageLV();                
                string contactEngCP= addCounterparty.GetEngCounterpartyContactLV();
                Assert.IsTrue(contactNameCPExl.Contains(contactEngCP));
                extentReports.CreateStepLogs("Passed", "Contact: " + valCPContact + " is available on Engagement Counterparty Contact(s) Right Panel");
                randomPages.CloseActiveTab("Tab");

                //*******CF Financial User Adding Engagement Counterparty Comments with All Types ******************** 
                
                /// Comments are not being not added on eng page script failed for below section
                typeRowCount = ReadExcelData.GetRowCount(excelPath, "CounterpartyComments");
                for (int typeRow = 2; typeRow <= typeRowCount; typeRow++)
                {
                    addCounterparty.ClickEngCPCommentsLV();// remove text from comopany
                    commentTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyComments", typeRow, 1);
                    commentTextExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyComments", typeRow, 2);
                    addCounterparty.AddNewEngagementCounterpartyCommentLV(commentTypeExl, commentTextExl, counterpartyCompanyNameExl);
                    popupMessage = randomPages.GetLVMessagePopup();
                    Assert.IsTrue(popupMessage.Contains("Engagement Counterparty Comment"), "Verify the Engagement Counterparty Comments is displayed in Popup message ");
                    extentReports.CreateStepLogs("Passed", "Comments added for counterparty with Type:  " + commentTypeExl);
                    commentType= addCounterparty.GetCommentTypeLV();
                    Assert.AreEqual(commentType, commentTypeExl,"Verify Comments added with Type:  "+ commentTypeExl);
                    randomPages.CloseActiveTab("ECC");                    
                }
                /// Comments are not being not added on eng page script failed for below section*******
                
                CustomFunctions.CloseWindow(driver, 1);
                CustomFunctions.SwitchToWindow(driver, 0);
                randomPages.CloseActiveTab(opportunityName);
                //CustomFunctions.PageReload(driver);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CF Financial User logged out");
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
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "Compliance User: "+ complianceuserExl+" logged out");
                ////*****************************************////

                                                
                //Login as FS User to add FS Engagement 
                string userFSExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 6, 1);

                homePage.SearchUserByGlobalSearchN(userFSExl);
                extentReports.CreateStepLogs("Info", "FS User: " + userFSExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                extentReports.CreateStepLogs("Passed", "System Admin Switched to Lightning View ");
                //Go to Engagement module in Lightning View 
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
                popupMessage = randomPages.GetLVMessagePopup();
                Assert.IsTrue(popupMessage.Contains("FS Engagement"), "Verify the Added FS Engagement is displayed in Popup message ");
                extentReports.CreateStepLogs("Passed", " FS Engagement "+ nameFSEng+" added for Engagement with Sponsored Company: " + counterpartyCompanyNameExl);
                randomPages.CloseActiveTab(nameFSEng);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "FS User: " + userFSExl + " logged out");

                //////////////////////////////////////////               

                //TMTI0101392 Verify that CF User is able to Request Full engagement from Partial engagement.
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

                engagementHome.SearchEngagementInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "Engagement found and selected");
                engagementDetails.ClickRequestFullEngagementLV();
                extentReports.CreateStepLogs("Info", "Click on Request Full Engagement button and verify that below validation");
                
                string expectedHeaderErrorsList = ReadExcelData.ReadData(excelPath, "VEValidationList", 3);
                string actualHeaderErrorsList = engagementDetails.GetVerballyFullEngValidationHeaderErrorsLV();
                Assert.AreEqual(expectedHeaderErrorsList,actualHeaderErrorsList, "Verify the Validations in Header of Engagement Information page, while Request for Full Engagement a Verbally Engaged Engagement");
                extentReports.CreateStepLogs("Passed", "Validations in Header of Engagement Information page are correct, while Request for Full Engagement a Verbally Engaged Engagement");

                //TMTI0113224 Verify that the "Tail Expires" is removed as a required field on converting CF Verbally Engaged Engagement to Full Engagement.
                string expectedLabelList = ReadExcelData.ReadData(excelPath, "VEValidationList", 5);
                string actualLabelList= engagementDetails.GetVerballyFullEngReqFieldsLV();
                Assert.AreEqual(expectedLabelList, actualLabelList, "Verify the Required Fields on Engagement Information page, while Request for Full Engagement a Verbally Engaged Engagement");
                extentReports.CreateStepLogs("Passed", "Required Fields on Engagement Information page are correct, while Request for Full Engagement a Verbally Engaged Engagement");

                //Enter Required fields for Request Full Engagement tail Expired removed from below function
                //TMTI0113224 Verify that the "Tail Expires" is removed as a required field on converting CF Verbally Engaged Engagement to Full Engagement.
                engagementDetails.ClickRequestFullEngagementLV();
                engagementDetails.EnterRequestFullEngagementReqValuesLV();
                extentReports.CreateStepLogs("Info", randomPages.ClickInterruptionOKButtonLV());
                extentReports.CreateStepLogs("Info", "Required Fields for Request Full Engagement are entered");
                //popupMessage = randomPages.GetLVMessagePopup();
                //extentReports.CreateStepLogs("Passed", "Required Fields saved with popup message: " + popupMessage);                //Create Primary Contact 
                
                engagementDetails.CickAddEngagementContactLV(valRecordType);
                string billingContactNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", 3, 1);
                contactPartyExl = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", 3, 3);
                engagementDetails.CreateBillingContactLV(billingContactNameExl, contactPartyExl);
                popupMessage = randomPages.GetLVMessagePopup();
                Assert.IsTrue(popupMessage.Contains("Engagement Contact"), "Verify the Added Engagement Contact is displayed in Popup message ");
                extentReports.CreateStepLogs("Passed", billingContactNameExl + " Primary, Billing Contact added on Verbally Engaged Engagement page(Required for Full Engagement Request)");

                //Get Fee & Financial details 
                engagementDetails.ClickTabEngFeeAndFincnciaLV();

                valPEEstTransMCap= engagementDetails.GetValEstTansacttionMarketCapLV();
                valPEEBITDA=engagementDetails.GetValEbitdaLV();
                valPERetainer=engagementDetails.GetValRetainerLV();
                valPEProgressMonthlyFee=engagementDetails.GetValProgressMonthlyFeeLV();
                valPEContingentFee=engagementDetails.GetValContingentFeeLV();
                valPETotalFee=engagementDetails.GetValTotalFeeLV();
                valPESSExpense=engagementDetails.GetValSSExpenseLV();
                valPEExpenseCap=engagementDetails.GetValExpenseCapLV();
                valPELegalCap=engagementDetails.GetValLegalCapLV();

                engagementDetails.ClickRequestFullEngagementLV();                
                extentReports.CreateStepLogs("Info", "Click on Request Full Engagement button and Fill are required fields");
                //*******Don't have clarify of the Engagement Information pop-up******
                engagementDetails.ClickSaveEngagementInformationPopup();
                extentReports.CreateStepLogs("Info", randomPages.ClickInterruptionOKButtonLV());
                extentReports.CreateStepLogs("Passed", "*******Don't have clarify of the Engagement Information pop-up******");
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CF Financial User: "+ userExl + " logged out" );

                //TMTI0101394 Verify that CAO User can approve the Full engagement request
                string userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3,1);
                homePage.SearchUserByGlobalSearchN(userCAOExl);
                extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string userCAO = login.ValidateUserLightningView();
                //Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                extentReports.CreateStepLogs("Passed", "User: " + userCAOExl + " logged in on Lightning View");
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
                extentReports.CreateStepLogs("Passed", "Verbally Engagement is "+ status+" for Full Enagement ");
                opportunityDetails.CloseApprovalHistoryTabL();
                
                ////TMTI0101396	Verify that once CAO user approve the request, a full engagement get created
                
                Assert.IsFalse(randomPages.GetVerballyEngCheckboxStatusLV(), "Verify Verbally Engaged checkbox is un-checked after Partial Engagement is Approved for Full Engagement");
                extentReports.CreateStepLogs("Passed", "Verbally Engaged checkbox is un-checked after Partial Engagement is Approved for Full Engagement");

                //TMTI0101398	Verify all the details filled in psudo/Partial engagement is correctly mapped with Full engagement.
                //Validate the Engagement name in Engagement details page
                engagementDetails.ClickEngInfoTabLV();
                string engagementNumber = engagementDetails.GetEngagementNumberL();
                string engagementName = engagementDetails.GetEngagementNameL();
                Assert.AreEqual(opportunityName, engagementName);
                extentReports.CreateStepLogs("Passed", "Name of Engagement : " + engagementName + " is Same as Opportunity name ");

                //TMTI0118723	Verify that the"Location where Benefit was Provided" field value is mapped from partial engegement to full engagement.
                extentReports.CreateStepLogs("Info", "Verify that Location where Benefit is to be Provided field is mapped from opoortunity on Fully Engaged Engagement");
                Assert.AreEqual(locationBenefit, engagementDetails.GetValueLocationBenefitLV());
                extentReports.CreateStepLogs("Passed", "Location where Benefit is to be Provided field is mapped from opoortunity on Fully Engaged Engagement");


                //Engagement Comments
                engagementDetails.ClickEngCommentsTabLV();
                typeRowCount = ReadExcelData.GetRowCount(excelPath, "EngComments");
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {                    
                    commentTextExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EngComments", typeRow, 2);
                    Assert.IsTrue(engagementDetails.IsEngCommentsPresentLV(commentTextExl), "Verify All Comments added from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement");
                    extentReports.CreateStepLogs("Passed", commentTextExl+ " Comments added from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement");
                }
                //Validate Fee & Financial Fees on Fully Engaged Engagement ******Fee value contains USD conversion  
                engagementDetails.ClickTabEngFeeAndFincnciaLV();
                //Assert.AreEqual(valPEEstTransMCap,engagementDetails.GetValEstTansacttionMarketCapLV());
                Assert.IsTrue(engagementDetails.GetValEstTansacttionMarketCapLV().Contains(valPEEstTransMCap));
                extentReports.CreateStepLogs("Passed", "Est.Trans Market Cap(MM): " + valPEEstTransMCap+ " , from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement");
                //Assert.AreEqual(valPEEBITDA,engagementDetails.GetValEbitdaLV());
                Assert.IsTrue(engagementDetails.GetValEbitdaLV().Contains(valPEEBITDA));
                extentReports.CreateStepLogs("Passed", "EBITDA: " + valPEEBITDA + " from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement");
                //Assert.AreEqual(valPERetainer,engagementDetails.GetValRetainerLV());
                Assert.IsTrue(engagementDetails.GetValRetainerLV().Contains(valPERetainer));
                extentReports.CreateStepLogs("Passed", "Retainer: " + valPERetainer + " , from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement");
                //Assert.AreEqual(valPEProgressMonthlyFee,engagementDetails.GetValProgressMonthlyFeeLV());
                Assert.IsTrue(engagementDetails.GetValProgressMonthlyFeeLV().Contains(valPEProgressMonthlyFee));
                extentReports.CreateStepLogs("Passed", "Progress Monthly Fee: " + valPEProgressMonthlyFee + " , from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement");
                //Assert.AreEqual(valPEContingentFee, engagementDetails.GetValContingentFeeLV());
                Assert.IsTrue(engagementDetails.GetValContingentFeeLV().Contains(valPEContingentFee));
                extentReports.CreateStepLogs("Passed", "Contingent Fee: " + valPEContingentFee + " , from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement");
                //Assert.AreEqual(valPETotalFee,engagementDetails.GetValTotalFeeLV());
                Assert.IsTrue(engagementDetails.GetValTotalFeeLV().Contains(valPETotalFee));
                extentReports.CreateStepLogs("Passed", "Total Fee: " + valPETotalFee + " , from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement");
                //Assert.AreEqual(valPESSExpense,engagementDetails.GetValSSExpenseLV());
                Assert.IsTrue(engagementDetails.GetValSSExpenseLV().Contains(valPESSExpense));
                extentReports.CreateStepLogs("Passed", "Shared Services Expense: " + valPESSExpense + " from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement");
                //Assert.AreEqual(valPEExpenseCap,engagementDetails.GetValExpenseCapLV());
                Assert.IsTrue(engagementDetails.GetValExpenseCapLV().Contains(valPEExpenseCap));
                extentReports.CreateStepLogs("Passed", "Expense Cap: " + valPEExpenseCap + " , from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement");
                //Assert.AreEqual(valPELegalCap,engagementDetails.GetValLegalCapLV());
                Assert.IsTrue(engagementDetails.GetValLegalCapLV().Contains(valPELegalCap));
                extentReports.CreateStepLogs("Passed", "Legal Cap: " + valPELegalCap + " , from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement");

                //Validate Contacts on Fully Engaged Engagement
                engagementDetails.ClickEngContactTabLV();
                int contactsCount = ReadExcelData.GetRowCount(excelPath, "AddContact");
                for (int row = 2; row <= contactsCount; row++)
                {                    
                    string contactExl = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", row, 1);
                    Assert.IsTrue(engagementDetails.IsEngContactPresentLV(contactExl), "Contacts: "+ contactExl+" added from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement");
                    extentReports.CreateStepLogs("Passed", "Contacts: "+ contactExl+" added from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement ");
                }

                /*****
                //Validate FS Eng is not visbible on Fully Engaged Engagement for CAO              
                engagementDetails.ClickTabFSEngagementLV();
                extentReports.CreateStepLogs("Info", "User is on FS Engagement tab");
                string fullFSEngName = engagementDetails.GetFSEngagementIDLV();
                Assert.AreEqual(nameFSEng, fullFSEngName);
                extentReports.CreateStepLogs("Passed", "FS Engagement with ID: "+ fullFSEngName+" added on Partial Engaged Engagement is present on Fully Engaged Engagement");
                *****/
                extentReports.CreateStepLogs("Passed", "**FS Eng is not visible on Fully Engaged Engagement for CAO**");
                //Validate Eng Counterparties
                engagementDetails.ClickViewCounterpartyButtonEngagementPageLV();
                for(int i = 0;i< selectedCounterpartyCompany.Length; i++)
                {
                    Assert.IsTrue(engagementDetails.IsEngCounterparyCompaniesPresentLV(selectedCounterpartyCompany[i]), "Verify Counterparties added from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement");
                    extentReports.CreateStepLogs("Passed", selectedCounterpartyCompany[i] + " Counterparties added from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement");
                }

                //Validate contact of Counterparty 
                addCounterparty.ClickCounterpartyCompanyLink(counterpartyCompanyNameExl);
                CustomFunctions.SwitchToWindow(driver, 1);
                extentReports.CreateStepLogs("Info", "User Switched to Counterparty Details new tab ");
                string contactApprovedEngCP = addCounterparty.GetEngCounterpartyContactLV();
                Assert.IsTrue(contactNameCPExl.Contains(contactApprovedEngCP));
                extentReports.CreateStepLogs("Info", "Counterparty Contact: "+ contactNameCPExl+" added from Opp/Verbally Engaged Engagement Counterparty is present on Fully Engaged Engagement Counterparty");

                //Counterparty Comments
                addCounterparty.ClickViewAllEngCPCommentsLV();
                typeRowCount = ReadExcelData.GetRowCount(excelPath, "CounterpartyComments");
                for (int typeRow = 2; typeRow <= typeRowCount; typeRow++)
                {                    
                    commentTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyComments", typeRow, 1);
                    Assert.IsTrue(addCounterparty.IsEngCPCommentPresentLV(commentTypeExl));
                    extentReports.CreateStepLogs("Passed", "Comment Type: "+commentTypeExl + " Counterparty with Comments added from Opp/Verbally Engaged Engagement is present on Fully Engaged Engagement");
                 }
                CustomFunctions.CloseWindow(driver, 1);
                CustomFunctions.SwitchToWindow(driver, 0);
                randomPages.CloseActiveTab(engagementName); 
                
                //Moved to Opportunity Module to Verify the Stage of Opportunity (should be engaged)
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "Opportunity found and selected");                
                //Verify the Stage of Opportunity (should be engaged)
                string Oppstage = opportunityDetails.GetStageLV();
                Assert.AreEqual("Engaged", Oppstage, "Verify the Stage of Opportunity (should be engaged) after Full Engagement Request Approval");
                extentReports.CreateStepLogs("Passed", "Stage of Opportunity changed to "+ Oppstage+ " after Full Engagement Request Approval");

                //-------------------------------//

                randomPages.CloseActiveTab(opportunityName);
                extentReports.CreateStepLogs("Info", "Opportunity tab closed");
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CAO User: " + userCAOExl + " logged out");

                //Login as Compliance user and Verfy the Eng Compliance comments               
                homePage.SearchUserByGlobalSearchN(complianceuserExl);
                extentReports.CreateStepLogs("Info", "Compliance User: " + complianceuserExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                Assert.AreEqual(login.ValidateUserLightningView().Contains(complianceuserExl), true);
                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                engagementHome.SearchEngagementInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "Compliance User is on Fully Engaged Engagement page");

                engagementDetails.ClickEngCommentsTabLV();
                commentTextExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EngComments", 5, 2);
                Assert.IsTrue(engagementDetails.IsEngCommentsPresentLV(commentTextExl), "Verify Compliance Comments added from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement");
                extentReports.CreateStepLogs("Passed", commentTextExl + " , Comments added from Opp/Verbally Engaged Engagement are present on Fully Engaged Engagement");
                
                //---------------//
                randomPages.CloseActiveTab(engagementName);
                extentReports.CreateStepLogs("Info", "Engagement tab closed");
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "Compliance User: " + complianceuserExl + " logged out");
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed SuccessFully");
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