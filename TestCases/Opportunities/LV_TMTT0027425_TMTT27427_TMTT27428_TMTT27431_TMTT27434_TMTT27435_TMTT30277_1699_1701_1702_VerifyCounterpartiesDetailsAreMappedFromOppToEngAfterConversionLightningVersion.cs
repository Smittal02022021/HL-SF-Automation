using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;
using OpenQA.Selenium;

namespace SF_Automation.TestCases.OpportunitiesCounterparty
{
    class LV_TMTT0027425_TMTT27427_TMTT27428_TMTT27431_TMTT27434_TMTT27435_TMTT30277_1699_1701_1702_VerifyCounterpartiesDetailsAreMappedFromOppToEngAfterConversionLightningVersion : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        AddOppCounterparty addCounterparty = new AddOppCounterparty();
        LVHomePage homePageLV = new LVHomePage();
        MassRelationshipCreatorPage creatorPage = new MassRelationshipCreatorPage();
        HomeMainPage homePage = new HomeMainPage();
        RandomPages randomPages = new RandomPages();

        public static string TMTI0063910 = "LV_TMTI0063910_VerifyCounterpartiesDetailsAreMappedFromOpportunityToEngagementAfterConversion";
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
        public void VerifyCounterpartiesDetailsAreMappedFromOpportunityToEngagementLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + TMTI0063910;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowOpp = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                for (int row = 2; row <= rowOpp; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    //Login as Standard User profile and validate the user
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "StandardUsers", row, 1);
                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                    homePage.SearchUserByGlobalSearchN(valUser);
                    extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");                    
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string stduser = login.ValidateUserLightningView();
                    Assert.AreEqual(stduser.Contains(valUser), true);
                    extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");

                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    //Validating Title of New Opportunity Page
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectCFOpp();
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                    extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, TMTI0063910);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");                    

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(TMTI0063910);
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
                    addOpportunityContact.CreateContactL2(TMTI0063910, valRecordType);
                    extentReports.CreateStepLogs("Passed", valContactType + " is added as " + valContactType+"opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForCFConversionLV2(TMTI0063910, valJobType);
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(TMTI0063910);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityL();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab("Internal Team");
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Info", valUser + " Standard User logged out ");
                    opportunityHome.SearchOpportunity(opportunityName);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(TMTI0063910);
                    if (valJobType.Equals("Buyside") || valJobType.Equals("Sellside"))
                    {
                        opportunityDetails.UpdateNBCApproval();
                        extentReports.CreateStepLogs("Info", "Conflict Check and NBC fields are updated ");
                    }
                    else
                    {
                        extentReports.CreateStepLogs("Info", "Conflict Check fields are updated ");
                    }
                    //Update Client and Subject to Accupac bypass EBITDA field validation for JobType- Sellside
                    if (valJobType.Equals("Sellside"))
                    {
                        opportunityDetails.UpdateClientandSubject("Accupac");
                        extentReports.CreateStepLogs("Info", "Updated Client and Subject fields ");
                    }
                    else
                    {
                        extentReports.CreateStepLogs("Info", "Updated Client and Subject not required ");
                    }

                    //Login again as Standard User
                    homePage.SearchUserByGlobalSearchN(valUser);
                    extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    stduser = login.ValidateUserLightningView();
                    Assert.AreEqual(stduser.Contains(valUser), true);
                    extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);//updated

                    //Verify is View Counterparty Button is Displayed on selected Opportunity Detail page
                    Assert.IsTrue(opportunityDetails.IsViewCounterpartyButtonOpportunityPageL());
                    extentReports.CreateStepLogs("Passed", "View Counterparty Button is displayed on Oppordunity Detail Page");

                    //Verify All available Buttons on View Counterparty Detail page
                    opportunityDetails.ClickViewCounterpartyButtonOpportunityPageL();

                    string counterpartyCompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", row, 1);
                    string counterpartyTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", row, 2);
                    addCounterparty.ClickAddCounterpartiesButtonLV();
                    addCounterparty.ButtonClick("Add Counterparty");
                    extentReports.CreateStepLogs("Info", "Verifying the functionality of adding Counterparties Company from Add Counterparty button ");

                    addCounterparty.AddNewOpportunityCounterparty(counterpartyCompanyNameExl, counterpartyTypeExl);
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.IsTrue(popupMessage.Contains(counterpartyCompanyNameExl), "Verify the Added Counterparty name is displayed in Popup message ");
                    extentReports.CreateLog(popupMessage + " message Displayed and company " + counterpartyCompanyNameExl + " is added in counterparty list ");
                    addCounterparty.CloseOppCounterpartyPage(counterpartyCompanyNameExl);
                    addCounterparty.ButtonClick("Back");
                    extentReports.CreateStepLogs("Info", "Clicked on Back button");
                    Assert.IsTrue(addCounterparty.IsCounterpartiesListDisplayed(),"Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                    Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(counterpartyCompanyNameExl), "Verify added Company: " + counterpartyCompanyNameExl + " is under Counterparties List");
                    extentReports.CreateStepLogs("Passed", "User returned to Counterparties List Page");
                    extentReports.CreateStepLogs("Passed", counterpartyCompanyNameExl + " Company is added and displayed into Counterparties List ");

                    //TMTI0063910 Verify that the user is able to edit and save the multiple entries with Comments                    
                    string commentsExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", row, 6);
                    addCounterparty.EditCoutnerpartyDetailsLV(commentsExl);
                    addCounterparty.SaveCounterpartyChanges();
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.AreEqual(popupMessage, "Records Updated Successfully!");
                    extentReports.CreateStepLogs("Passed", "Added Counterparty details are updated ");
                    //CustomFunctions.PageReload(driver);
                    //randomPages.CloseActiveTab("OCC");

                    extentReports.CreateStepLogs("Passed", "****Comments are not dislayed on CP Detail page*****");

                    /*
                    // Comments are not dislayed on CP Detail page
                    //Verify Comments added from mass edit screen on CP Detail page                  
                    addCounterparty.ClickCounterpartyCompanyLink(counterpartyCompanyNameExl);
                    //CustomFunctions.PageReload(driver);
                    CustomFunctions.SwitchToWindow(driver, 1);
                    extentReports.CreateStepLogs("Info", "User Switched to new tab ");
                    //TMTI0063918 Verify that the counterparty's comments are visible on the counterparty's details page and mapped correctly after conversion into the engagement 
                    addCounterparty.AreMassCommentsDisplayedOnCounterpartyDetailPageLV(commentsExl);
                    Assert.IsTrue(addCounterparty.AreMassCommentsDisplayedOnCounterpartyDetailPageLV(commentsExl), "Verify comments added through Mass Edit from Counterparty list page are saved and available on related counterparty detail page");
                    extentReports.CreateStepLogs("Passed", "Comments added through Mass Edit from Counterparty list page are saved and available on related counterparty detail page");

                    */


                    //TMTI0063912 Verify the functionality of adding new Counterparty Contact in Opportunity Counter party Detail Page
                    //Adding Contact with email id in added Counterparty

                    //****Counterparty detail page changes not able to add Counterparty Contact,Comments

                    //addCounterparty.ClickCounterpartyCompanyLink(counterpartyCompanyNameExl);//updated
                    //CustomFunctions.SwitchToWindow(driver, 1);
                    //extentReports.CreateStepLogs("Info", "User Switched to new tab ");

                    //Add CP Comments on detail page
                    //Add & Get Counterparty Comments
                    //addCounterparty.ClickAddOppCPCommentsLV();// remove text from comopany



                    extentReports.CreateStepLogs("Info", "********Opportunity CP add Comments button removed from footer as well on CP Detail page************");
                    /********CP add Comments button removed from footer as well on CP Detail page************                     
                    //TMTI0063918 Verify that the counterparty's comments are visible on the counterparty's details page and mapped correctly after conversion into the engagement 
                    
                    //Adding Opportunity Counterparty Comments from updated View(Footer section on detail page) 

                    
                    
                    addCounterparty.ClickAddOppCPCommentsLV();
                    string commentTypeCPExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 3, 2);
                    string commentTextCPExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", 2, 3);
                    addCounterparty.AddNewOpportunityCounterpartyCommentLV(commentTypeCPExl, commentTextCPExl, counterpartyCompanyNameExl);
                    popupMessage = randomPages.GetLVMessagePopup();
                    Assert.IsTrue(popupMessage.Contains("Opportunity Counterparty Comment"), "Verify the Opportunity Counterparty Comments is displayed in Popup message ");
                    extentReports.CreateStepLogs("Passed", "Comments added for counterparty with Type:  " + commentTypeCPExl);
                    string commentTypeCP = addCounterparty.GetCommentTypeLV();
                    Assert.AreEqual(commentTypeCP, commentTypeCPExl, "Verify Comments added with Type:  " + commentTypeCPExl);
                    randomPages.CloseActiveTab("OCC");
                    */

                    ///////**********************
                    ///

                    //**************************************//
                    //Checking the ISU0012080: Not able to convert opportunity into engagement with counterparty

                    //contact is not being deisplayed on CP detail page
                    //Verify the ways of add contact and Adding Contacts 

                    /********************
                    addCounterparty.ClickCounterpartyCompanyLink(counterpartyCompanyNameExl);
                    CustomFunctions.SwitchToWindow(driver, 1);
                    extentReports.CreateStepLogs("Info", "User Switched to new tab ");
                    addCounterparty.ButtonClick("New Opportunity Counterparty Contact");
                    string counterpartyContactNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyContact", 2, 1);
                    string companyNameResult = addCounterparty.GetContactSearchedLV("Company", "8K Miles");
                    string industryNameResult = addCounterparty.GetContactSearchedLV("Industry/Product Focus", "FIG");
                    string contactNameResult = addCounterparty.GetContactSearchedLV("Name", counterpartyContactNameExl);//Updated
                    
                    string valCPContact = addCounterparty.GetContactNameFromListLV();
                    addCounterparty.SelectContactFromListLV();
                    addCounterparty.ClickAddContactLV();
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.AreEqual(popupMessage, "Counterparty Contact(s) were created successfully");
                    extentReports.CreateStepLogs("Passed", "New Opportunity Counterparty Contact is added ");
                    addCounterparty.ButtonClick("Back");
                    CustomFunctions.CloseWindow(driver, 1);
                    CustomFunctions.SwitchToWindow(driver, 0);
                    */////////////////////


                    //CustomFunctions.PageReload(driver);

                    //****UI Changes not QuickLink to view added contact
                    //addCounterparty.ClickCounterparyQuickLink("Contacts");
                    //Assert.IsTrue(addCounterparty.IsContactDisplayedInQuickLinkList(valCPContact));
                    // extentReports.CreateStepLogs("Passed", "Contact: " + valCPContact + " is available under Counterparty Contact(s) Quicklink");
                    //CustomFunctions.CloseWindow(driver, 1);
                    //CustomFunctions.SwitchToWindow(driver, 0);
                    //CustomFunctions.PageReload(driver);

                    //Assert.IsTrue(addCounterparty.IsContactAddedCounterpartyListLV(counterpartyCompanyNameExl), "Verify Contact is added under Company name in Counterparty Companies List");
                    //extentReports.CreateStepLogs("Passed", "Added Contact is available under Counterparty Record List ");

                    //opportunityDetails.CloseOpprtunityTabL("Counterparty Editor");

                    //TMTI0063923 Verify the functionality of the Email button on the Counterparties Editor Page
                    //string contactEmailExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyContact", row, 2);
                    //addCounterparty.SelectOpportunityCounterpartyAndClickEmailButton();//need to update logic of seletion of template
                    // string contactEmail = addCounterparty.GetOpportunityCounterpartyContactEmailOnEmailTemplate(counterpartyCompanyNameExl);
                    // Assert.AreEqual(contactEmail, contactEmailExl, "Verify Contact Email id is present on Email Template ");
                    // extentReports.CreateStepLogs("Passed", "Contact Email: " + contactEmail + " is present on Email Template ");
                    // Assert.IsTrue(addCounterparty.IsAddedCounterpartyCompanyDisplayedOnEmailTemplate(counterpartyCompanyNameExl), "Verify Company Counterparty name is present on Email Template ");
                    // extentReports.CreateStepLogs("Passed", "Company Counterparty name:" + counterpartyCompanyNameExl + " is present on Email Template ");


                    //TMTI0070811	Verification of Export Data feature available on View Counterparty screen
                    //
                    //CustomFunctions.CloseWindow(driver, 1);
                    //CustomFunctions.SwitchToWindow(driver, 0);
                    //

                    addCounterparty.ClickOpportunityCounterpartyExportDataButton();
                    string locationExportedFile = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", row, 4);
                    string exportedFileName = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", row, 5);
                    string pathExportedFile = locationExportedFile;
                    string exportedFile = pathExportedFile + exportedFileName;
                    ///Need to Revisit for Download find logic
                    bool isFilePresent = CustomFunctions.ValidateFileExists(pathExportedFile, exportedFileName);
                    Assert.IsTrue(isFilePresent, " Verify File name:" + exportedFileName + " is downloaded ");
                    extentReports.CreateStepLogs("Passed", "File name:" + exportedFileName + " is downloaded and available at location:" + pathExportedFile + " ");

                    int rowCounterparties = ReadExcelData.GetRowCount(exportedFile, "Counter-Party-List");
                    for (int rows = 2; rows <= rowCounterparties; rows++)
                    {
                        string CompanyNameExportedExl = ReadExcelData.ReadDataMultipleRows(exportedFile, "Counter-Party-List", rows, 2);
                        Assert.AreEqual(CompanyNameExportedExl, counterpartyCompanyNameExl);
                    }
                    CustomFunctions.DeleteFile(pathExportedFile, exportedFileName);
                    extentReports.CreateStepLogs("Info", "File name:" + exportedFileName + " is deleted from location:" + pathExportedFile + " ");
                    opportunityDetails.CloseOpprtunityTabL("Counterparty Editor");

                    //
                    //opportunityDetails.CloseOpprtunityTabL("Counterparty Editor");
                    //Add Client Contact on Opportunity     
                    string valClientContact = ReadExcelData.ReadData(excelPath, "AddContact", 6);
                    party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                    valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 5);
                    addOpportunityContact.CickAddCFOpportunityContact();
                    addOpportunityContact.CreateClientContactLV(valClientContact, party, valContactType);
                    //addOpportunityContact.CreateContactL2(TMTI0063910);
                    extentReports.CreateLog(valClientContact+ "is added as " + valContactType + " ");

                    //Requesting for engagement and validate the success message
                    opportunityDetails.ClickRequestToEngL();

                    //Submit Request To Engagement Conversion 
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");

                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Passed", valUser + " logged out ");

                    //////////////1699-1701-1702///////////////////////
                    extentReports.CreateLog("System Administrator Performing the Manage Relationship Activities as CF financial user do not have access to Manage Relationship button ");
                    string userAdmin = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 3, 1);
                    homePage.SearchUserByGlobalSearchN(userAdmin);
                    extentReports.CreateStepLogs("Info", "User: " + userAdmin + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    string user = login.ValidateUserLightningView();
                    //Assert.AreEqual(user.Contains(userAdmin), true);
                    login.SwitchToLightningExperience();
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");

                    //moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                    string pageHeader= opportunityDetails.ClickManageRelationshipsLV();
                    Assert.AreEqual(pageHeader, "Mass Relationship Creator");
                    extentReports.CreateStepLogs("Passed", "Mass Relationship Creator Page is displayed ");

                    string valHeaders = creatorPage.ValidateContactsTableHeaders(ReadExcelData.ReadData(excelPath, "AddContact", 8));
                    Assert.AreEqual("CONTACT NAME COMPANY APPEAR ON STRENGTH RATING SYNC", valHeaders);
                    extentReports.CreateStepLogs("Passed", "Headers: " + valHeaders + " are displayed ");

                    //Validate default checked radio button and contacts displayed
                    bool btnAllContacts = creatorPage.ValidateRadiobuttonLV();
                    Assert.IsTrue(btnAllContacts);
                    extentReports.CreateStepLogs("Passed", "All Contacts checkbox is default checked ");

                    extentReports.CreateLog("All Contacts radio button is selected " + btnAllContacts + " ");
                    string valContactNames = creatorPage.ValidateAllContacts();
                    //Assert.AreEqual(valCPContact + " " + valContact + " " + valClientContact, valContactNames);
                    extentReports.CreateStepLogs("Passed", "Contact Names: " + valContactNames + " are displayed ");

                    //Update the Strength Rating  
                    creatorPage.UpdateRating();
                    extentReports.CreateStepLogs("Info", "Strength ratings are updated ");

                    //T1701 Opportunity - Opportunity Details Page - Manage Relationships - Mass Relationship Creator Page - Edit Relationhsip Strength Rating
                    //T1702 Opportunity - Opportunity Details Page - Mass Relationship Creator Page - Contacts Related To Opportunity Sorting
                    //Get column name and validate sorting of columns
                    IWebElement colName = creatorPage.GetColName();
                    string descResult = creatorPage.ValidateSorting(colName, "Descending");
                    Assert.AreEqual("True", descResult);
                    extentReports.CreateStepLogs("Passed", "Contact Name column is sorted in descending order " + descResult + " ");

                    string ascResult = creatorPage.ValidateSorting(colName, "Ascending");
                    Assert.AreEqual("True", ascResult);
                    extentReports.CreateStepLogs("Passed", "Contact Name column is sorted in ascending order " + ascResult + " ");

                    //Validate details by selecting External Team, Client Team and CP Contact
                    creatorPage.ClickExternalTeam();
                    string extContactName = creatorPage.ValidateContactName();
                    Assert.AreEqual(valContact, extContactName);
                    extentReports.CreateStepLogs("Passed", "External Contact: " + extContactName + " is displayed ");
                    string extRating = creatorPage.ValidateRatings();
                    Assert.AreEqual("Low", extRating);
                    extentReports.CreateStepLogs("Passed", "External Contact's ratings " + extRating + " is displayed ");

                    //ClientTeam not available 
                    creatorPage.ClickClientTeam();
                    string clientContactName = creatorPage.ValidateContactName();
                    Assert.AreEqual(valClientContact, clientContactName);
                    extentReports.CreateStepLogs("Passed", "Client Contact: " + clientContactName + " is displayed ");
                    string clientRating = creatorPage.ValidateRatings();
                    Assert.AreEqual("Low", clientRating);
                    extentReports.CreateStepLogs("Passed", "Client Contact's ratings " + clientRating + " is displayed ");

                    //addCounterparty Contacts not available
                    //creatorPage.ClickCPContacts();
                    //string cpContactName = creatorPage.ValidateContactName();
                   // Assert.AreEqual(valCPContact, cpContactName);
                    //extentReports.CreateStepLogs("Passed", "Client Contact: " + cpContactName + " is displayed ");

                    string cpRating = creatorPage.ValidateRatings();
                    Assert.AreEqual("Low", cpRating);
                    extentReports.CreateStepLogs("Passed", "CP Contact's ratings " + cpRating + " is displayed ");
                    driver.SwitchTo().DefaultContent();
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Passed", userAdmin + " System Admin User logged out ");

                    ///////////////////////////
                    //Login as CAO user to approve the Opportunity
                    string userCAO = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", row, 1);
                    homePage.SearchUserByGlobalSearchN(userCAO);
                    extentReports.CreateStepLogs("Info", "CAO User: " + userCAO + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(userCAO), true);
                    extentReports.CreateStepLogs("Passed", "CAO User: " + userCAO + " logged in on Lightning View");

                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "CAO User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                    //Approve the Opportunity 
                    string status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Passed", "Opportunity is " + status + " by CAO User ");
                    opportunityDetails.CloseApprovalHistoryTabL();

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEngagementL();
                    extentReports.CreateStepLogs("Info", "Opportunity Converted into Engagement ");
                    //Validate the Engagement name in Engagement details page
                    string engagementNumber = engagementDetails.GetEngagementNumberL();
                    string engagementName = engagementDetails.GetEngagementNameL();                    
                    Assert.AreEqual(opportunityName, engagementName);
                    extentReports.CreateStepLogs("Passed", "Name of Engagement : " + engagementName + " is Same as Opportunity name ");

                    //TMTI0063913 Verify the case when an opportunity is converted into the engagement and counterparties are already added                     
                    //TMTI0105423 Verify that Opportunity converted into Engagement is correctly mapped with Counterparties added at Opportunity with all details

                    Assert.IsTrue(engagementDetails.IsViewCounterpartyButtonEngagementPageLV(), "Verify View Counterparty Button is displayed on Engagement Detail Page ");
                    extentReports.CreateStepLogs("Passed", "View Counterparty Button is displayed on Engagement Detail Page");
                    engagementDetails.ClickViewCounterpartyButtonEngagementPageLV();
                    Assert.IsTrue(addCounterparty.IsCounterpartiesListDisplayed(), "Verify User is redirected back to Counterparties List page ");
                    extentReports.CreateStepLogs("Passed", "User is redirected to Counterparties List page");                       
                    Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(counterpartyCompanyNameExl), "Verify added Company: " + counterpartyCompanyNameExl + " is under Counterparties List");
                    extentReports.CreateStepLogs("Passed", "Opportunity Counterparties Company: " + counterpartyCompanyNameExl + " is Mapped on Engagement under Counterparties List after conversion ");


                    extentReports.CreateStepLogs("Info", "********CP add Comments button removed from footer as well on CP Detail page************");

                    /* PanelRight removed from UI 
                    //TMTI0063918 Verify that the counterparty's comments are visible on the counterparty's details page and mapped correctly after conversion into the engagement
                    //TMTI0063922 Verify the Opportunity Counterparties comments and contacts are being mapped to Engagement Counterparties upon conversion

                    addCounterparty.ClickCounterpartyCompanyLink(counterpartyCompanyNameExl);
                    CustomFunctions.SwitchToWindow(driver, 1);
                    extentReports.CreateLog("User Clicked on Company name from Counterparties List and switched to New Tab ");
                    engagementDetails.ClickPanelRightEngagementPageLV("Comments");
                    Assert.IsTrue(addCounterparty.IsCommentDisplayedInQuickLinkList(commentsExl));
                    extentReports.CreateStepLogs("Passed", "Opportunity Counterparties Comments are mapped on Engagement page after conversion ");
                    addCounterparty.CloseEngCounterpartiesCommentsTab();
                    */

                    /*//Checking the ISU0012080 PanelRight removed from UI 
                    engagementDetails.ClickPanelRightEngagementPageLV("Contacts");
                    Assert.IsTrue(addCounterparty.IsContactDisplayedInQuickLinkList(valCPContact));
                    extentReports.CreateStepLogs("Passed", "Opportunity Counterparties Contact: " + valCPContact + " is mapped on Engagement page after conversion ");
                    CustomFunctions.CloseWindow(driver, 1);
                    CustomFunctions.SwitchToWindow(driver, 0);
                    extentReports.CreateStepLogs("Info", "Counterparty Company Page is closed user switched previous Tab ");
                    
                    */
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Info", valUser + " logged out ");
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Browser Closed Succesfully!");
                }
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
