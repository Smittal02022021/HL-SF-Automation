using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;
using System.Diagnostics;
using OpenQA.Selenium;
using AventStack.ExtentReports;

namespace SF_Automation.TestCases.Opportunities
{
    class TMTI63910_63912_63913_63918_63922_63923_70811_T1699_1701_1702_VerifyCounterpartiesDetailsAreMappedFromOpportunityToEngagementAfterConversion : BaseClass
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

        public static string TMTI0063910 = "TMTI0063910_VerifyCounterpartiesDetailsAreMappedFromOpportunityToEngagementAfterConversion";

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

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowOpp = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                for (int row = 2; row <= rowOpp; row++)
                {

                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);

                    //Login as Standard User profile and validate the user
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "StandardUsers", row, 1);
                    usersLogin.SearchCFUserAndLogin(valUser);
                    login.SwitchToClassicView();

                    string stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser + " logged in ");

                    login.SwitchToLightningExperience();
                    extentReports.CreateLog("User: " + stdUser + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();

                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");

                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                    //Validating Title of New Opportunity Page
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectCFOpp();
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    string opportunityName = addOpportunity.AddOpportunitiesLightning(valJobType, TMTI0063910);
                    extentReports.CreateLog("Opportunity : " + opportunityName + " is created ");

                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(TMTI0063910);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateLog("User is on Opportunity detail " + displayedTab + " tab ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact      
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);

                    addOpportunityContact.CickAddCFOpportunityContact();
                    addOpportunityContact.CreateContactL2(TMTI0063910);
                    extentReports.CreateLog(valContactType + " is added as " + valContactType+"opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForCFConversionLV(TMTI0063910);
                    extentReports.CreateLog("Opportunity Required Fields for Converting into Engagement are Filled ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(TMTI0063910);
                    extentReports.CreateLog("Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    extentReports.CreateLog("Return to Opportunity Detail page ");

                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateLog(stdUser + " Standard User logged out ");

                    extentReports.CreateLog("Admin is Performing Required Actions ");
                    opportunityHome.SearchOpportunity(opportunityName);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(TMTI0063910);
                    if (valJobType.Equals("Buyside") || valJobType.Equals("Sellside"))
                    {
                        opportunityDetails.UpdateNBCApproval();
                        extentReports.CreateLog("Conflict Check and NBC fields are updated ");
                    }
                    else
                    {
                        extentReports.CreateLog("Conflict Check fields are updated ");
                    }

                    //Update Client and Subject to Accupac bypass EBITDA field validation for JobType- Sellside
                    if (valJobType.Equals("Sellside"))
                    {
                        opportunityDetails.UpdateClientandSubject("Accupac");
                        extentReports.CreateLog("Updated Client and Subject fields ");
                    }
                    else
                    {
                        Console.WriteLine("Not required to update ");
                    }

                    //Login again as Standard User
                    usersLogin.SearchUserAndLogin(valUser);
                    login.SwitchToClassicView();

                    stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser + " Standard User logged in ");

                    login.SwitchToLightningExperience();
                    extentReports.CreateLog("User: " + stdUser + " Standard User Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();

                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");

                    moduleNameExl = ReadExcelData.ReadData(excelPath, "ModuleName", 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    opportunityHome.SearchMyOpportunitiesInLightning(opportunityName, stdUser);//updated

                    //Verify is View Counterparty Button is Displayed on selected Opportunity Detail page
                    Assert.IsTrue(opportunityDetails.IsViewCounterpartyButtonOpportunityPageL());
                    extentReports.CreateLog("View Counterparty Button is displayed on Oppordunity Detail Page");

                    //Verify All available Buttons on View Counterparty Detail page
                    opportunityDetails.ClickViewCounterpartyButtonOpportunityPageL();

                    string counterpartyCompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", row, 1);
                    string counterpartyTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", row, 2);

                    addCounterparty.ClickAddCounterpartiesButton();
                    addCounterparty.ButtonClick("Add Counterparty");
                    extentReports.CreateLog("Verifying the functionality of adding Counterparties Company from Add Counterparty button ");

                    addCounterparty.AddNewOpportunityCounterparty(counterpartyCompanyNameExl, counterpartyTypeExl);
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.IsTrue(popupMessage.Contains(counterpartyCompanyNameExl), "Verify the Added Counterparty name is displayed in Popup message ");
                    extentReports.CreateLog(popupMessage + " message Displayed and company " + counterpartyCompanyNameExl + " is added in counterparty list ");

                    addCounterparty.CloseCurrentTab(counterpartyCompanyNameExl);
                    addCounterparty.ButtonClick("Back");
                    extentReports.CreateLog("Clicked on Back button");
                    Assert.IsTrue(addCounterparty.VerifyUserIsOnCounterpartiesListPage(), "Verify User is redirected back to Counterparties List page when clicked on Back button from Add Counterparties page");
                    Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(counterpartyCompanyNameExl), "Verify added Company: " + counterpartyCompanyNameExl + " is under Counterparties List");
                    extentReports.CreateLog("User returned to Counterparties List Page");
                    extentReports.CreateLog(counterpartyCompanyNameExl + " Company is added and displayed into Counterparties List ");

                    //TMTI0063910 Verify that the user is able to edit and save the multiple entries with Comments
                    
                    string commentsExl = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", row, 3);
                    addCounterparty.EditCoutnerpartyDetails(commentsExl);
                    addCounterparty.SaveCounterpartyChanges();
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.AreEqual(popupMessage, "Records Updated Successfully!");
                    extentReports.CreateLog("Added Counterparty details are updated ");

                    //TMTI0063912 Verify the functionality of adding new Counterparty Contact in Opportunity Counter party Detail Page
                    //Adding Contact with email id in added Counterparty

                    string counterpartyContactNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyContact", row, 1);
                    addCounterparty.ClickCounterpartyCompanyLink(counterpartyCompanyNameExl);//updated

                    CustomFunctions.SwitchToWindow(driver, 1);
                    addCounterparty.ButtonClick("New Opportunity Counterparty Contact");
                    
                    //Verify the ways of add contact and Adding Contacts
                    string companyNameResult = addCounterparty.GetContactSearched("Company", "8K Miles");
                    string industryNameResult = addCounterparty.GetContactSearched("Industry/Product Focus", "FIG");
                    string contactNameResult = addCounterparty.GetContactSearched("Name", counterpartyContactNameExl);//Updated
                    
                    string valCPContact = addCounterparty.GetItemNameFromList();
                    addCounterparty.CheckBoxSelectRecord();
                    addCounterparty.ClickAddContact();
                    popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.AreEqual(popupMessage, "Counterparty Contact(s) were created successfully");
                    extentReports.CreateLog("New Opportunity Counterparty Contact is added ");
                    addCounterparty.ButtonClick("Back");

                    addCounterparty.ClickCounterparyQuickLink("Contacts");
                    Assert.IsTrue(addCounterparty.IsContactDisplayedInQuickLinkList(valCPContact));
                    extentReports.CreateLog("Contact: " + valCPContact + " is available under Counterparty Contact(s) Quicklink");
                    CustomFunctions.CloseWindow(driver, 1);
                    CustomFunctions.SwitchToWindow(driver, 0);
                    CustomFunctions.PageReload(driver);

                    Assert.IsTrue(addCounterparty.IsContactAddedCounterpartyList(counterpartyCompanyNameExl), "Verify Contact is added under Company name in Counterparty Companies List");
                    extentReports.CreateLog("Added Contact is available under Counterparty Record List ");

                    //opportunityDetails.CloseOpprtunityTabL("Counterparty Editor");

                    //TMTI0063923 Verify the functionality of the Email button on the Counterparties Editor Page
                    string contactEmailExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CounterpartyContact", row, 2);
                    addCounterparty.SelectOpportunityCounterpartyAndClickEmailButton();
                    string contactEmail = addCounterparty.GetOpportunityCounterpartyContactEmailOnEmailTemplate(counterpartyCompanyNameExl);
                    Assert.AreEqual(contactEmail, contactEmailExl, "Verify Contact Email id is present on Email Template ");
                    extentReports.CreateLog("Contact Email: " + contactEmail + " is present on Email Template ");

                    Assert.IsTrue(addCounterparty.IsAddedCounterpartyCompanyDisplayedOnEmailTemplate(counterpartyCompanyNameExl), "Verify Company Counterparty name is present on Email Template ");
                    extentReports.CreateLog("Company Counterparty name:" + counterpartyCompanyNameExl + " is present on Email Template ");

                    //TMTI0070811	Verification of Export Data feature available on View Counterparty screen
                    
                    ///////Not Working- Opening windows pop to save the file //////////
                    //addCounterparty.ClickOpportunityCounterpartyExportDataButton();
                    //string locationExportedFile = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", row, 4);
                    //string exportedFileName = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOpportunityCounterparty", row, 5);
                    //string pathExportedFile = locationExportedFile;
                    //string exportedFile = pathExportedFile + exportedFileName;

                    //bool isFilePresent = CustomFunctions.ValidateFileExists(pathExportedFile, exportedFileName);
                    //Assert.IsTrue(isFilePresent, " Verify File name:" + exportedFileName + " is downloaded ");
                    //extentReports.CreateLog("File name:" + exportedFileName + " is downloaded and available at location:" + pathExportedFile + " ");

                    //int rowCounterparties = ReadExcelData.GetRowCount(exportedFile, "Counter-Party-List");
                    //for (int rows = 2; rows <= rowCounterparties; rows++)
                    //{
                    //    string CompanyNameExportedExl = ReadExcelData.ReadDataMultipleRows(exportedFile, "Counter-Party-List", rows, 2);
                    //    Assert.AreEqual(CompanyNameExportedExl, counterpartyCompanyNameExl);
                    //}

                    //CustomFunctions.DeleteFile(pathExportedFile, exportedFileName);
                    //extentReports.CreateLog("File name:" + exportedFileName + " is deleted from location:" + pathExportedFile + " ");
                    ///////Not Working//////////
                    ///
                    opportunityDetails.CloseOpprtunityTabL("Counterparty Editor");

                    //Add Client Contact on Opportunity     
                    string valClientContact = ReadExcelData.ReadData(excelPath, "AddContact", 6);
                    party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                    valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 5);
                    addOpportunityContact.CickAddCFOpportunityContact();
                    addOpportunityContact.CreateClientContactL(valClientContact, party, valContactType);
                    extentReports.CreateLog(valClientContact+ "is added as " + valContactType + " ");
                    //Requesting for engagement and validate the success message
                    opportunityDetails.ClickRequestToEngL();

                    //Submit Request To Engagement Conversion 
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");

                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateLog(valUser + " logged out ");

                    //////////////1699-1701-1702///////////////////////
                    extentReports.CreateLog("System Administrator Performing the Manage Relationship Activities as CF financial user do not have access to Manage Relationship button ");
                    string user = login.ValidateUser();
                    login.SwitchToLightningExperience();

                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");
                    //user = "Indrajeet Singh";

                    //Search for created opportunity
                    opportunityHome.SearchMyOpportunitiesInLightning(opportunityName, user);

                    string pageHeader= opportunityDetails.ClickManageRelationshipsLV();
                    Assert.AreEqual(pageHeader, "Mass Relationship Creator");
                    extentReports.CreateLog("Mass Relationship Creator Page is displayed ");

                    string valHeaders = creatorPage.ValidateContactsTableHeaders(ReadExcelData.ReadData(excelPath, "AddContact", 8));
                    Assert.AreEqual("CONTACT NAME COMPANY APPEAR ON STRENGTH RATING SYNC", valHeaders);
                    extentReports.CreateLog("Headers: " + valHeaders + " are displayed ");

                    //Validate default checked radio button and contacts displayed
                    bool btnAllContacts = creatorPage.ValidateRadiobuttonLV();
                    Assert.IsTrue(btnAllContacts);
                    extentReports.CreateLog("All Contacts checkbox is default checked ");

                    extentReports.CreateLog("All Contacts radio button is selected " + btnAllContacts + " ");
                    string valContactNames = creatorPage.ValidateAllContacts();
                    Assert.AreEqual(valCPContact + " " + valContact + " " + valClientContact, valContactNames);
                    extentReports.CreateLog("Contact Names: " + valContactNames + " are displayed ");

                    //Update the Strength Rating  
                    creatorPage.UpdateRating();
                    extentReports.CreateLog("Strength ratings are updated ");

                    //T1701 Opportunity - Opportunity Details Page - Manage Relationships - Mass Relationship Creator Page - Edit Relationhsip Strength Rating
                    //T1702 Opportunity - Opportunity Details Page - Mass Relationship Creator Page - Contacts Related To Opportunity Sorting
                    //Get column name and validate sorting of columns
                    IWebElement colName = creatorPage.GetColName();
                    string descResult = creatorPage.ValidateSorting(colName, "Descending");
                    Assert.AreEqual("True", descResult);
                    extentReports.CreateLog("Contact Name column is sorted in descending order " + descResult + " ");

                    string ascResult = creatorPage.ValidateSorting(colName, "Ascending");
                    Assert.AreEqual("True", ascResult);
                    extentReports.CreateLog("Contact Name column is sorted in ascending order " + ascResult + " ");

                    //Validate details by selecting External Team, Client Team and CP Contact
                    //
                    creatorPage.ClickExternalTeam();
                    string extContactName = creatorPage.ValidateContactName();
                    Assert.AreEqual(valContact, extContactName);
                    extentReports.CreateLog("External Contact: " + extContactName + " is displayed ");
                    string extRating = creatorPage.ValidateRatings();
                    Assert.AreEqual("Low", extRating);
                    extentReports.CreateLog("External Contact's ratings " + extRating + " is displayed ");

                    //ClientTeam not available 
                    creatorPage.ClickClientTeam();
                    string clientContactName = creatorPage.ValidateContactName();
                    Assert.AreEqual(valClientContact, clientContactName);
                    extentReports.CreateLog("Client Contact: " + clientContactName + " is displayed ");
                    string clientRating = creatorPage.ValidateRatings();
                    Assert.AreEqual("Low", clientRating);
                    extentReports.CreateLog("Client Contact's ratings " + clientRating + " is displayed ");

                    //addCounterparty Contacts not available
                    creatorPage.ClickCPContacts();
                    string cpContactName = creatorPage.ValidateContactName();
                    Assert.AreEqual(valCPContact, cpContactName);
                    extentReports.CreateLog("Client Contact: " + cpContactName + " is displayed ");
                    string cpRating = creatorPage.ValidateRatings();
                    Assert.AreEqual("Low", cpRating);
                    extentReports.CreateLog("CP Contact's ratings " + cpRating + " is displayed ");
                    driver.SwitchTo().DefaultContent();
                    login.SwitchToClassicView();

                    ///////////////////////////

                    //Login as CAO user to approve the Opportunity
                    string userCAO = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", row, 1);
                    usersLogin.SearchUserAndLogin(userCAO);
                    login.SwitchToClassicView();

                    string caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(userCAO), true);
                    extentReports.CreateLog("User: " + caoUser + " CAO User logged in ");

                    login.SwitchToLightningExperience();
                    extentReports.CreateLog("User: " + caoUser + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();

                    //Go to Opportunity module in Lightning View 
                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    opportunityHome.SearchMyOpportunitiesInLightning(opportunityName, caoUser);

                    //Approve the Opportunity 
                    string status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateLog("Opportunity is " + status + " by CAO User ");
                    opportunityDetails.CloseApprovalHistoryTabL();

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEngagementL();
                    extentReports.CreateLog("Opportunity Converted into Engagement ");
                    //Validate the Engagement name in Engagement details page
                    string engagementNumber = engagementDetails.GetEngagementNumberL();
                    string engagementName = engagementDetails.GetEngagementNameL();                    
                    Assert.AreEqual(opportunityName, engagementName);
                    extentReports.CreateLog("Name of Engagement : " + engagementName + " is Same as Opportunity name ");


                    //TMTI0063913 Verify the case when an opportunity is converted into the engagement and counterparties are already added 
                    
                    Assert.IsTrue(engagementDetails.IsViewCounterpartyButtonEngagementPageL(), "Verify View Counterparty Button is displayed on Engagement Detail Page ");
                    extentReports.CreateLog("View Counterparty Button is displayed on Engagement Detail Page");

                    engagementDetails.ClickViewCounterpartyButtonEngagementPageL();
                    Assert.IsTrue(addCounterparty.VerifyUserIsOnCounterpartiesListPage(), "Verify User is redirected back to Counterparties List page ");
                    extentReports.CreateLog("User is redirected to Counterparties List page");
                    
                    
                    Assert.IsTrue(addCounterparty.IsCompanyInCounterpartyList(counterpartyCompanyNameExl), "Verify added Company: " + counterpartyCompanyNameExl + " is under Counterparties List");
                    extentReports.CreateLog("Opportunity Counterparties Company: " + counterpartyCompanyNameExl + " is Mapped on Engagement under Counterparties List after conversion ");

                    //TMTI0063918 Verify that the counterparty's comments are visible on the counterparty's details page and mapped correctly after conversion into the engagement
                    //TMTI0063922 Verify the Opportunity Counterparties comments and contacts are being mapped to Engagement Counterparties upon conversion
                    addCounterparty.ClickCounterpartyCompanyLink(counterpartyCompanyNameExl);
                    CustomFunctions.SwitchToWindow(driver, 1);
                    extentReports.CreateLog("User Clicked on Company name from Counterparties List and switched to New Tab ");

                    engagementDetails.ClickPanelRightEngagementPage("Comments");
                    Assert.IsTrue(addCounterparty.IsCommentDisplayedInQuickLinkList(commentsExl));
                    extentReports.CreateLog("Opportunity Counterparties Comments are mapped on Engagement page after conversion ");
                    addCounterparty.CloseEngCounterpartiesCommentsTab();

                    engagementDetails.ClickPanelRightEngagementPage("Contacts");
                    Assert.IsTrue(addCounterparty.IsContactDisplayedInQuickLinkList(valCPContact));                    
                    extentReports.CreateLog("Opportunity Counterparties Contact: " + valCPContact + " is mapped on Engagement page after conversion ");
                    CustomFunctions.CloseWindow(driver, 1);
                    CustomFunctions.SwitchToWindow(driver, 0);
                    extentReports.CreateLog("Counterparty Company Page is closed user switched previous Tab ");

                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateLog(valUser + " logged out ");
                    driver.Quit();
                }
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
            }
        }
    }
}
