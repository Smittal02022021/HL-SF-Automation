using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using SF_Automation.TestData;
using NUnit.Framework;
using System;
using AventStack.ExtentReports.Gherkin.Model;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_2_TMTT0047923_TMTT0047926_TMTT0047929_VerifyJobTypeOnOpportunityEngagementDetailPage:BaseClass
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
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();
        public static string fileT47926 = "LV_TMTT0047923_TMTT0047926_TMTT0047929_1_VerifyJobTypeDisplayedInJobTypeDropDownWhileAddingNewOpportunity";
        private string nameRevAccu;
        private string jobTypeCC;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0117825 Verify that the Job type "DRC - Dispute" is displayed in the Job type drop down while adding a new opportunity. 
        //TMTI0117814 Verify that the user is able to create a new Opportunity with Job Type "DRC - Dispute".
        //TMTI0117820 Verify that the Job Type "DRC – Dispute" is displayed under Job type in search result on searching with the Opportunity number upon global search.
        //TMTI0117818 Verify that the Job Type "DRC – Dispute" gets displayed in the search results on searching with the Engagement number in Global search
        //TMTI0117817 Verify that the Job type is displayed as "DRC - Dispute"  upon conversion of an opportunity to an engagement.
        //TMTI0117810 Verify that the new Job type "DRC -Dispute" gets displayed in Engagement -> Conflict check page.
        //TMTI0117827 Verify the Job code and the Record type on Engagement details page for the Job type "DRC - Dispute".
        //TMTI0117832 Verify the Product code and the ERP Integration status in
        //ERP Information section in Opportunity details page for the opportunity having Job type as "DRC - Dispute".
        //TMTI0117813 Verify that the new/ updated Job type "DRC - Dispute" and its code is listed under Job type Object/ tab.
        //TMTI0117824 Verify the Product code and the ERP Integration status in Oracle ERP Information section in Engagement details page having Job type as "DRC - Dispute".
        //TMTI0117819 Verify that the new Job type "DRC -Dispute" gets displayed in Opportunities -> Conflict check page.
        //TMTI0117826 Verify that the Job type "DRC – Dispute" is displayed in the Job type drop down in Opportunity edit page.
        //TMTI0117822 Verify that the user is able to edit/update other Job type to "DRC - Dispute" for an existing opportunity.
        //TMTI0117815 Verify that the Job type "DRC -Dispute" gets dispalyed on adding Job type column in Filters while creating the reports.
        //TMTI0117831 Verify that the new Job type "DRC -Dispute" gets displayed in Billing Request page.
        //TMTI0119041 Verify that the new Job type "DRC -Dispute" gets displayed in Revenue Accrual page.
        //TMTI0117835 Verify that the Job type "DRC - ESOP" is displayed in the Job type drop down while adding a new opportunity.
        //TMTI0117848 Verify that the user is able to create a new Opportunity with Job Type "DRC - ESOP".
        //TMTI0117841 Verify that the Job Type "DRC – ESOP" is displayed under Job type in search result on searching with the Opportunity number upon global search.
        //TMTI0117853 Verify that the Job Type "DRC – ESOP" gets displayed in the search results on searching with the Engagement number in Global search
        //TMTI0117833 Verify that the Job type is displayed as "DRC - ESOP"  upon conversion of an opportunity to an engagement.
        //TMTI0117842 Verify that the new Job type "DRC -ESOP" gets displayed in Engagement -> Conflict check page.
        //TMTI0117847 Verify the Job code and the Record type on Engagement details page for the Job type "DRC - ESOP".
        //TMTI0117844 Verify the Product code and the ERP Integration status in Oracle ERP Information section in Opportunity details page for the opportunity having Job type as "DRC - ESOP".
        //TMTI0117854 Verify that the new/ updated Job type "DRC - ESOP " and its code is listed under Job type Object/ tab.
        //TMTI0117851 Verify the Product code and the ERP Integration status in Oracle ERP Information section in Engagement details page having Job type as "DRC - ESOP".
        //TMTI0117839 Verify that the new Job type "DRC -ESOP" gets displayed in Opportunities -> Conflict check page.
        //TMTI0117850 Verify that the Job type "DRC – ESOP" is displayed in the Job type drop down in Opportunity edit page.
        //TMTI0117837 Verify that the user is able to edit/update other Job type to "DRC - ESOP" for an existing opportunity.
        //TMTI0117852 Verify that the Job type "DRC -ESOP" gets displayed on adding Job type column in Filters while creating the reports.
        //TMTI0117840 Verify that the new Job type "DRC -ESOP" gets displayed in Billing Request page.
        //TMTI0117838 Verify that the new Job type "DRC -ESOP" gets displayed in Revenue Accrual page.
        //TMTI0117863 Verify that the Job type "DRC - Estate & Gift" is displayed in the Job type drop down while adding a new opportunity.
        //TMTI0117877 Verify that the user is able to create a new Opportunity with Job Type "DRC - Estate & Gift".
        //TMTI0117873 Verify that the Job Type "DRC – Estate & Gift" is displayed under Job type in search result on searching with the Opportunity number upon global search.
        //TMTI0117857 Verify that the Job Type "DRC – Estate & Gift" gets displayed in the search results on searching with the Engagement number in Global search
        //TMTI0117858 Verify that the Job type is displayed as "DRC - Estate & Gift"  upon conversion of an opportunity to an engagement.
        //TMTI0117879 Verify that the new Job type "DRC -Estate & Gift" gets displayed in Engagement -> Conflict check page.
        //TMTI0117878 Verify the Job code and the Record type on Engagement details page for the Job type "DRC - Estate & Gift".
        //TMTI0117868 Verify the Product code and the ERP Integration status in Oracle ERP Information section in Opportunity details page for the opportunity having Job type as "DRC - Estate & Gift".
        //TMTI0117859 Verify that the new/ updated Job type "DRC - Estate & Gift" and its code is listed under Job type Object/ tab.
        //TMTI0117860 Verify the Product code and the ERP Integration status in Oracle ERP Information section in Engagement details page having Job type as "DRC - Estate & Gift".
        //TMTI0117872 Verify that the new Job type "DRC -Estate & Gift" gets displayed in Opportunities -> Conflict check page.
        //TMTI0117875 Verify that the Job type "DRC – Estate & Gift" is displayed in the Job type drop down in Opportunity edit page.
        //TMTI0117864 Verify that the Job type "DRC - Estate & Gift" gets displayed on adding Job type column in Filters while creating the reports.
        //TMTI0117862 Verify that the user is able to edit/update other Job type to "DRC - Estate & Gift" for an existing opportunity.
        //TMTI0117871 Verify that the new Job type "DRC -Estate & Gift" gets displayed in Billing Request page.
        //TMTI0117869 Verify that the new Job type "DRC -Estate & Gift" gets displayed in Revenue Accrual page.        


       [Test]
        public void VerifyJobTypeDisplayedInJobTypeDropDownWhileAddingNewOpportunity()
        {
           try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT47926;
                extentReports.CreateStepLogs("Info", "Verify Functionality of Opportunity to Engagement conversion for LOB:FVA On LightningView");
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

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
                    
                    //TMTI0117825 Verify that the Job type "DRC - Dispute" is displayed in the Job type drop down while adding a new opportunity. 
                    //TMTI0117835 Verify that the Job type "DRC - ESOP" is displayed in the Job type drop down while adding a new opportunity.
                    //TMTI0117863 Verify that the Job type "DRC - Estate & Gift" is displayed in the Job type drop down while adding a new opportunity. 

                    Assert.IsTrue(addOpportunity.IsJobTypePresentLV(valJobType), "Verify that the Job type "+ valJobType+" is displayed in the Job type drop down while adding a new opportunity");
                    extentReports.CreateStepLogs("Passed", "Job Type " + valJobType + " is displayed in the Job type drop down while adding a new opportunity");
                    
                    pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                    extentReports.CreateStepLogs("Passed", "Creating Opportunity for Job Type: " + valJobType);
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileT47926);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created with Job Type: "+ valJobType);
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileT47926);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Passed", "User is on Opportunity detail " + displayedTab + " tab after addin Internal deal team members");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    extentReports.CreateStepLogs("Passed", "Opportunity with number : " + opportunityNumber + " is created with Job Type: " + valJobType);

                    //TMTI0117848	Verify that the user is able to create a new Opportunity with Job Type "DRC - ESOP".
                    //TMTI0117877	Verify that the user is able to create a new Opportunity with Job Type "DRC - Estate & Gift".
                    //TMTI0117814	Verify that the user is able to create a new Opportunity with Job Type "DRC - Dispute".

                    Assert.AreEqual(valJobType,opportunityDetails.GetJobTypeLV(), "Verify that the user is able to create a new Opportunity with Job Type "+valJobType);
                    extentReports.CreateStepLogs("Passed", "User is able to create a new Opportunity with Job Type " + valJobType);

                    //Create External Primary Contact      
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    addOpportunityContact.CickAddOpportunityContactLV();
                    addOpportunityContact.CreateContactL2(fileT47926, valRecordType);

                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");
                    opportunityDetails.UpdateReqFieldsForFVAConversionLV(fileT47926);
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");

                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "CF Financial User is logged out from Lightning View");

                    //Login as System Admin user 
                    string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 3, 1);
                    extentReports.CreateStepLogs("Info", "System Admin User: " + adminUserExl + " Updating the Required details ");
                    homePage.SearchUserByGlobalSearchN(adminUserExl);
                    extentReports.CreateStepLogs("Info", "Admin User: " + adminUserExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "Admin User: " + adminUserExl + " logged in on Lightning View");
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "Admin User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Info", "Admin is Performing Required Actions ");
                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeNBCApproveDetailsLV(valJobType);
                    
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileT47926);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    randomPages.CloseActiveTab("Internal Team");
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Passed", "Admin: " + adminUserExl + "switched to Classic and Loggout ");

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

                    //TMTI0117826	Verify that the Job type "DRC – Dispute" is displayed in the Job type drop down in Opportunity edit page.
                    //TMTI0117850	Verify that the Job type "DRC – ESOP" is displayed in the Job type drop down in Opportunity edit page.
                    //TMTI0117875	Verify that the Job type "DRC – Estate & Gift" is displayed in the Job type drop down in Opportunity edit page.

                    Assert.IsTrue(opportunityDetails.IsJobTypePresentLV(valJobType), "Verify that the Job type " + valJobType + " is displayed in the Job type drop down while adding a new opportunity");
                    extentReports.CreateStepLogs("Passed", "Job Type " + valJobType + " is displayed in the Job type drop down while Edit existing opportunity");

                    //TMTI0117832	Verify the Product code and the ERP Integration status in Oracle ERP Information section in Opportunity details page for the opportunity having Job type as "DRC - Dispute".
                    //TMTI0117844	Verify the Product code and the ERP Integration status in Oracle ERP Information section in Opportunity details page for the opportunity having Job type as "DRC - ESOP".
                    //TMTI0117868	Verify the Product code and the ERP Integration status in Oracle ERP Information section in Opportunity details page for the opportunity having Job type as "DRC - Estate & Gift".

                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected");
                    string prodCode = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductTypeCode", row, 1);
                    string productCode = randomPages.GetERPProductTypeCodeLV();
                    Assert.AreEqual(prodCode, productCode, "Verify the Product code in Oracle ERP Information Opportunity details page for the opportunity having Job type as "+valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type Code: " + productCode + " in ERP section for Job Type: " + valJobType);

                    string ERPStatus = randomPages.GetERPLastIntegrationStatusLV();
                    Assert.AreEqual("Success", ERPStatus, "Verify the Opportunity ERP Last Integration Status as Success ");
                    extentReports.CreateStepLogs("Passed", "Opportunity ERP Last Integration Status updated: " + ERPStatus+ " for the opportunity having Job type as "+valJobType);

                    //Only works on VM only 
                    //TMTI0117819	Verify that the new Job type "DRC -Dispute" gets displayed in Opportunities -> Conflict check page. 
                    //TMTI0117872	Verify that the new Job type "DRC -Estate & Gift" gets displayed in Opportunities -> Conflict check page. 
                    //TMTI0117839	Verify that the new Job type "DRC -ESOP" gets displayed in Opportunities -> Conflict check page. 

                    //opportunityDetails.ClickConflicksCheckLV();
                    //jobTypeCC = opportunityDetails.GetConflictTypeJobTypeLV();
                    //Assert.AreEqual(jobTypeCC, valJobType);
                    //extentReports.CreateStepLogs("Passed", "Job Type: " + jobTypeCC + " updated on Opportunity Conflicts Check form ");

                    //Submit Request To Engagement Conversion  
                    opportunityDetails.ClickRequestToEngL();                    
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");
                    randomPages.CloseActiveTab(opportunityName);

                    //TMTI0117820	Verify that the Job Type "DRC – Dispute" is displayed under Job type in search result on searching with the Opportunity number upon global search.
                    //TMTI0117841	Verify that the Job Type "DRC – ESOP" is displayed under Job type in search result on searching with the Opportunity number upon global search.
                    //TMTI0117873	Verify that the Job Type "DRC – Estate & Gift" is displayed under Job type in search result on searching with the Opportunity number upon global search.
                    Assert.IsTrue(opportunityHome.IsOpportunityWithJobTypeFoundLV(opportunityName, valJobType),"Verify that the Job Type: " + valJobType + " gets displayed in the search results on searching with the Opportunity Name in Global search");
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " with the Job Type: " + valJobType + " gets displayed in the search results on searching with the Opportunity Name in Global search");
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "CF Financial User: " + stdUserExl + " Loggout ");

                    //Approve and convert the Opporunity into Engagement
                    string caoUserExl = ReadExcelData.ReadData(excelPath, "CAOUser", 1);
                    extentReports.CreateStepLogs("Info", "CAO User: " + caoUserExl + " Approving the Request for Engagement and converting into Engagement ");
                    //Search and Approve the Opp
                    homePage.SearchUserByGlobalSearchN(caoUserExl);
                    extentReports.CreateStepLogs("Info", "CAO User: " + caoUserExl + " details are displayed. ");
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
                    //moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Pass", "User is on " + moduleNameExl + " Page ");
                    //Search for Approved opportunity with new name
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

                    //TMTI0117817	Verify that the Job type is displayed as "DRC - Dispute"  upon conversion of an opportunity to an engagement.
                    //TMTI0117833	Verify that the Job type is displayed as "DRC - ESOP"  upon conversion of an opportunity to an engagement.
                    //TMTI0117858	Verify that the Job type is displayed as "DRC - Estate & Gift"  upon conversion of an opportunity to an engagement.
                    //get JobType on engement page
                    string engJobType = engagementDetails.GetJobTypeL();
                    Assert.AreEqual(valJobType, engJobType, "Verify that the Job type is displayed as "+ engJobType+"  upon conversion of an opportunity to an engagement.");
                    extentReports.CreateStepLogs("Passed", "Engagemnt "+ engName + " with Job Type: "+engJobType+" upon conversion of an opportunity to an engagement");
                    randomPages.CloseActiveTab(engName);
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    //Search for Approved opportunity with new name
                    result = engagementHome.SearchEngagementInLightningView(engName);
                    Assert.AreEqual("Record found", result);
                    extentReports.CreateStepLogs("Passed", "Engagement found and selected after conversion from an opportunity");

                    //TMTI0117827	Verify the Job code and the Record type on Engagement details page for the Job type "DRC - Dispute".
                    //TMTI0117847	Verify the Job code and the Record type on Engagement details page for the Job type "DRC - ESOP".
                    //TMTI0117878	Verify the Job code and the Record type on Engagement details page for the Job type "DRC - Estate & Gift".
                    //Validate the value of Record Type in Engagement details page
                    
                    engagementDetails.NavigateToAdministratorTabLV();                    
                    string engRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 2);
                    string engRecordType = engagementDetails.GetRecordTypeLV();
                    Assert.AreEqual(engRecordTypeExl, engRecordType, "Verify the Record type on Engagement details page for the Job type: " +engJobType);
                    extentReports.CreateStepLogs("Passed", "Engagement Record type is : " + engRecordType + " for Job Type " + valJobType);

                    //TMTI0117824	erify the Product code and the ERP Integration status in Oracle ERP Information section in Engagement details page having Job type as "DRC - Dispute".
                    //TMTI0117851	Verify the Product code and the ERP Integration status in Oracle ERP Information section in Engagement details page having Job type as "DRC - ESOP".
                    //TMTI0117860	Verify the Product code and the ERP Integration status in Oracle ERP Information section in Engagement details page having Job type as "DRC - Estate & Gift".
                    //Validate the value of Product Type Code in Engagement details page
                    
                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected");
                    productCode = randomPages.GetERPProductTypeCodeLV();
                    Assert.AreEqual(prodCode, productCode);
                    extentReports.CreateStepLogs("Passed", "Engagement ERP Product Type Code: " + productCode + " in ERP section for Job Type: " + valJobType);
                    ERPStatus = randomPages.GetERPLastIntegrationStatusLV();
                    Assert.AreEqual("Success", ERPStatus, "Verify the Engagement ERP Last Integration Status as Success ");
                    extentReports.CreateStepLogs("Passed", "Engagement ERP Last Integration Status: " + ERPStatus + " in ERP section for Job Type: " + valJobType);


                    //TMTI0117831 Verify that the new Job type "DRC -Dispute" gets displayed in Billing Request page.
                    //TMTI0117840 Verify that the new Job type "DRC -ESOP" gets displayed in Billing Request page.
                    //TMTI0117871 Verify that the new Job type "DRC -Estate & Gift" gets displayed in Billing Request page.

                    engagementDetails.ClickBillingRequestButtonLV();
                    string billingReqJobType= engagementDetails.GetBillingEmailBodyJobTypeLV();
                    Assert.AreEqual(billingReqJobType, valJobType);
                    extentReports.CreateStepLogs("Passed", "Job Type: " + billingReqJobType + "present on Billing Request email page");
                    engagementDetails.CancelBillingRequestEmailLV();
                    extentReports.CreateStepLogs("Passed", "Billing Request Email page closed");

                    //TMTI0119041	Verify that the new Job type "DRC -Dispute" gets displayed in Revenue Accrual page.
                    //TMTI0117838	Verify that the new Job type "DRC -ESOP" gets displayed in Revenue Accrual page.
                    //TMTI0117869	Verify that the new Job type "DRC -Estate & Gift" gets displayed in Revenue Accrual page.

                    if (valJobType == "DRC - Estate & Gift")
                    {
                        string engStageExl= ReadExcelData.ReadData(excelPath, "EngStage", 1);
                        engagementDetails.UpdateStageLV(engStageExl);
                        engagementDetails.ClickRevenueTabLV();
                        engagementDetails.SelectRevenueAccrualLV();
                        nameRevAccu = engagementDetails.GetRevenueAccrualNumberLV();
                        extentReports.CreateStepLogs("Passed", "New Revenue Accrual: " + nameRevAccu + " Added");
                    }
                    else
                    {
                        engagementDetails.ClickRevenueTabLV();
                        engagementDetails.AddNewRevenueAccuralsLV();
                        nameRevAccu = engagementDetails.GetRevenueAccrualNumberLV();
                        extentReports.CreateStepLogs("Passed", "New Revenue Accrual: " + nameRevAccu + " Added");
                    }
                    string revAccuJobType = engagementDetails.GetRevAccuralJobTypeLV();
                    Assert.AreEqual(valJobType, revAccuJobType, "Verify that the new Job type: "+valJobType+" gets displayed in Revenue Accrual page");
                    extentReports.CreateStepLogs("Passed", "New Job type: " + valJobType + " gets displayed in Revenue Accrual page");

                    randomPages.CloseActiveTab(nameRevAccu);
                    //********Need to close new rev window for Job type: "DRC -Estate & Gift"************
                    randomPages.CloseActiveTab("New Revenue Accrual");

                    //Only works on VM only
                    //TMTI0117810 Verify that the new Job type "DRC -Dispute" gets displayed in Engagement -> Conflict check page. 
                    //TMTI0117842 Verify that the new Job type "DRC -ESOP" gets displayed in Engagement->Conflict check page. 
                    //TMTI0117879 Verify that the new Job type "DRC -Estate & Gift" gets displayed in Engagement->Conflict check page.     

                    //engagementDetails.ClickConflicksCheckLV();
                    //jobTypeCC = engagementDetails.GetConflictTypeJobTypeLV();
                    //Assert.AreEqual(jobTypeCC, valJobType);
                    //extentReports.CreateStepLogs("Passed", "Job Type: " + jobTypeCC + " updatd on Engagement Conflicts Check form ");
                    //randomPages.CloseActiveTab(opportunityName);



                    //TMTI0117818 Verify that the Job Type "DRC – Dispute" gets displayed in the search results on searching with the Engagement number in Global search
                    //TMTI0117853 Verify that the Job Type "DRC – ESOP" gets displayed in the search results on searching with the Engagement number in Global search
                    //TMTI0117857 Verify that the Job Type "DRC – Estate & Gift" gets displayed in the search results on searching with the Engagement number in Global search

                    Assert.IsTrue(engagementHome.IsEngagementWithJobTypeFoundLV(engName, valJobType), "Verify that the Job Type: "+valJobType+ " gets displayed in the search results on searching with the Engagement Name in Global search");
                    extentReports.CreateStepLogs("Passed", "Engagement: " + engName + " with the Job Type: " + valJobType + " gets displayed in the search results on searching with the Engagement Name in Global search");

                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Passed", "CAO User: " + caoUserExl + "Loggout ");
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