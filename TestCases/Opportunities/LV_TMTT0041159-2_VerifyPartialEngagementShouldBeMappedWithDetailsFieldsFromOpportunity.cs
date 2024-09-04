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
                usersLogin.ClickLogoutFromLightningView();
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

                ////Add & Get FS Opp
                //engagementDetails.ClickTabFSOpportunityLV();
                //extentReports.CreateStepLogs("Info", "User is on FS Engagement tab");
                //string nameFSEng = engagementDetails.CreateNewFSEngagementLV(counterpartyCompanyNameExl);
                //popupMessage = addCounterparty.GetLVMessagePopup();
                //Assert.IsTrue(popupMessage.Contains("FS Engagement"), "Verify the Added FS Engagement is displayed in Popup message ");
                //extentReports.CreateStepLogs("Passed", " FS Engagement " + nameFSEng + " added for Engagement with Sponsored Company: " + counterpartyCompanyNameExl);

                //Add & Get Opp comments
                
                int typeRowCount = ReadExcelData.GetRowCount(excelPath, "OppComments");
                for (int typeRow = 2; typeRow < typeRowCount; typeRow++)
                {
                    commentTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OppComments", typeRow, 1);
                    commentTextExl = ReadExcelData.ReadDataMultipleRows(excelPath, "OppComments", typeRow, 2);
                    opportunityDetails.ClickOppNewCommentsLV();
                    opportunityDetails.AddNewOppCommentLV(commentTypeExl, commentTextExl);
                    extentReports.CreateStepLogs("Info", "Comments added on Opportunity page with Type:  " + commentTypeExl);
                    commentType = addCounterparty.GetCommentTypeLV();
                    Assert.AreEqual(commentType, commentTypeExl, "Verify Comments added with Type:  " + commentTypeExl);
                    randomPages.CloseActiveTab(opportunityDetails.GetCommentIDLV());
                }
                //Add & Get Counterparties
                opportunityDetails.ClickOnViewCounterpartyButton();
                int filtersCount = ReadExcelData.GetRowCount(excelPath, "FilterSections");
                for (int optionRow = 2; optionRow <= filtersCount; optionRow++)
                {
                    filterSection = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", optionRow, 4);
                    subFilterSection = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", optionRow, 5);
                    addCounterparty.ClickAddCounterpartiesButtonLV();
                    filterValue = ReadExcelData.ReadDataMultipleRows(excelPath, "FilterSections", optionRow, 6);
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
                    Assert.IsTrue(addCounterparty.VerifyUserIsOnCounterpartiesListPage(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
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

                addCounterparty.AddNewNewCounterpartyLV(counterpartyCompanyNameExl, counterpartyTypeExl);
                popupMessage = randomPages.GetLVMessagePopup();
                Assert.IsTrue(popupMessage.Contains(counterpartyCompanyNameExl), "Verify the Added Counterparty name is displayed in Popup message ");
                extentReports.CreateStepLogs("Passed", popupMessage + " message Displayed and company " + counterpartyCompanyNameExl + " is added in counterparty list ");

                addCounterparty.CloseOppCounterpartyPage(counterpartyCompanyNameExl);
                addCounterparty.ClickBackButtonAndValidateViewCounterpartiesPageLV(); //ButtonClick("Back");
                extentReports.CreateStepLogs("Info", "Clicked on Back button");
                Assert.IsTrue(addCounterparty.VerifyUserIsOnCounterpartiesListPage(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(counterpartyCompanyNameExl), "Verify added Company: " + counterpartyCompanyNameExl + " is under Counterparties List");
                extentReports.CreateStepLogs("Passed", "User returned to Counterparties List Page");
                extentReports.CreateLog(counterpartyCompanyNameExl + " Company is added and displayed into Counterparties List ");
                selectedCounterpartyCompany[index] = counterpartyCompanyNameExl;


                //Add & Get Counterparty Contact
                //Add & Get Counterparty Comments

                opportunityDetails.EditOpportunityStageLV(stageExl);
                string updatedStage = opportunityDetails.GetStageLV();
                Assert.AreEqual(updatedStage, stageExl);
                extentReports.CreateStepLogs("Passed", "Opportunity Stage is updated from " + stage + " to " + updatedStage);
                Assert.IsTrue(randomPages.GetVerballyEngCheckboxStatusLV(), "Verify Verbally Engaged checkbox is Checked after stage change of the Opportunity to Verbally Engaged");
                extentReports.CreateStepLogs("Passed", "Verbally Engaged checkbox is Checked after stage change of the Opportunity to Verbally Engaged");

                //Click Eng Link from right panel 

                //validate added 
                // Opp Contact
                // FS Opp
                // Opp comments
                // Counterparties
                // Counterparty Contact
                // Counterparty Comments

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