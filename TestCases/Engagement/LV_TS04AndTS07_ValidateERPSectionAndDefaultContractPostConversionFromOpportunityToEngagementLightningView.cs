using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.Engagement;
using System.Globalization;

namespace SF_Automation.TestCases.Engagements
{
    class LV_TS04AndTS07_ValidateERPSectionAndDefaultContractPostConversionFromOpportunityToEngagementLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        RandomPages pages = new RandomPages();
        LegalEntityDetail entityDetails = new LegalEntityDetail();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        ContactHomePage contactHome = new ContactHomePage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engagementHome = new EngagementHomePage();

        public static string ERPTS04 = "LV_TS04AndTS07_ValidateERPSection";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            InitializeZoom70();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ValidateERPSectionLV()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + ERPTS04;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                //Calling functions to validate for all LOBs operation
                int rowUsers = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row <= rowUsers; row++)
                {
                    string valUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 25);

                    usersLogin.SearchUserAndLogin(valUserExl);
                    login.SwitchToLightningExperience();
                    string stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(valUserExl), true);
                    extentReports.CreateStepLogs("Passed", "User: " + valUserExl + " logged in on Lightning View");

                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                    //Validating Title of New Opportunity Page
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                    extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                    extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType);
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, ERPTS04);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(ERPTS04);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Passed", "User is on Opportunity detail " + displayedTab + " tab ");

                    ////Validating Opportunity details  
                    string oppNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                    extentReports.CreateStepLogs("Passed", "Opportunity with number : " + oppNumber + " is created ");

                    //Create External Primary Contact      
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    addOpportunityContact.ClickAddOpportunityContactLV(valRecordType);
                    addOpportunityContact.CreateContactL2(ERPTS04);
                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                    //Fetch values of Opportunity Name, Client, Subject and Job Type
                    string oppName = opportunityDetails.GetOpportunityNameL();
                    string clientName = opportunityDetails.GetClientLV();
                    string subjectName = opportunityDetails.GetSubjectLV();
                    string jobType = opportunityDetails.GetJobTypeLV();

                    if (valRecordType == "CF")
                    {
                        opportunityDetails.UpdateReqFieldsForCFConversionLV2(ERPTS04, valJobType);                        
                    }
                    if (valRecordType == "FR")
                    {
                       opportunityDetails.UpdateReqFieldsForFRConversionLV(ERPTS04);
                        opportunityDetails.UpdateTotalDebtConfirmedLV();
                    }
                    if (valRecordType == "FVA")
                    {
                        opportunityDetails.UpdateReqFieldsForFVAConversionLV(ERPTS04);
                    }
                    extentReports.CreateStepLogs("Info", "Required fields are entered for LOB: " + valRecordType);
                    randomPages.CloseActiveTab(oppName);
                    extentReports.CreateStepLogs("Info", "Opportunity tab is closed");
                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "User: " + valUserExl + " logged out");

                    //------Only System Admin can see the ERP Section on Opportunity Detail page//
                    string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 3);
                    usersLogin.SearchUserAndLogin(adminUserExl);
                    login.SwitchToLightningExperience();
                    string userName = login.ValidateUserLightningView();
                    Assert.AreEqual(userName.Contains(adminUserExl), true);
                    extentReports.CreateLog("System Administrator User: " + adminUserExl + " logged in on Lightning View");
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    opportunityHome.SearchOpportunitiesInLightningView(oppName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                    randomPages.DetailPageFullViewLV();
                    opportunityDetails.UpdateCCOutcomeDetailsLV();
                    extentReports.CreateStepLogs("Info", "Conflict Check Details Provided ");
                    if (jobType == "Debt Capital Markets" || jobType == "Buyside" || jobType == "Sellside")
                    {
                        opportunityDetails.UpdateNBCApprovalLV();
                        extentReports.CreateStepLogs("Info", "NBC Approved Chekbox is Checked ");
                    }
                    opportunityDetails.UpdateInternalTeamDetailsLV(ERPTS04);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityL();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab("Internal Team");
                    randomPages.CloseActiveTab(oppName);
                    extentReports.CreateStepLogs("Info", "Opportunity tab is closed");
                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "System Administrator User: " + adminUserExl + " logged out");

                    //Login again as Standard User
                    usersLogin.SearchUserAndLogin(valUserExl);
                    login.SwitchToLightningExperience();
                    stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(valUserExl), true);
                    extentReports.CreateLog("Standard User: " + valUserExl + " logged in on Lightning View");
                    
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Pass", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    opportunityDetails.ClickRequestToEngL();
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Pass", valUserExl + " logged out ");

                    //Login as CAO user to approve the Opportunity
                    string userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);
                    usersLogin.SearchUserAndLogin(userCAOExl);
                    login.SwitchToLightningExperience();
                    string userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    extentReports.CreateStepLogs("Info", "CAO User:" + userCAOExl + " logged in on Lightning View");
                    
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    string status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Passed", "Opportunity Status: " + status + " ");
                    opportunityDetails.CloseApprovalHistoryTabL();
                    opportunityDetails.ClickConvertToEngagementL2();
                    extentReports.CreateStepLogs("Info", "Opportunity Converted into Engagement ");

                    // Validate the Engagement name in Engagement details page
                    string engNumber = engagementDetails.GetEngagementNumberL();
                    string engName = engagementDetails.GetEngagementNameL();
                    Assert.AreEqual(opportunityName, engName);
                    extentReports.CreateStepLogs("Passed", "Name of Engagement : " + engName + " is Same as Opportunity name ");
                    randomPages.CloseActiveTab(oppName);
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Pass", "CAO User: " + userCAOExl + " logged out ");

                    ////////////////////////System Administrator performing ERP related Activities/////////////////
                    usersLogin.SearchUserAndLogin(adminUserExl);
                    login.SwitchToLightningExperience();
                    userName = login.ValidateUserLightningView();
                    Assert.AreEqual(userName.Contains(adminUserExl), true);
                    extentReports.CreateLog("System Administrator User: " + adminUserExl + " logged in on Lightning View for ERP related Activities");
                   
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    engagementHome.GlobalSearchEngagementInLightningView(engName);
                    extentReports.CreateStepLogs("Passed", "Engagement: " + engName + " found and selected ");
                    //Get LOB
                    string LOB = randomPages.GetLOBLV();
                    //Validate HL Entity
                    //Click Info>>Admintrator tab
                    engagementDetails.ClickEngInfoTabLV();
                    engagementDetails.ClickEngAdministrationTabLV();
                    string entity = engagementDetails.GetLegalEntityLV();

                    //Full View
                    //randomPages.DetailPageFullViewLV();
                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected");

                    //Validate ERP section details------
                    //Validate ERP Submitted To Sync                 
                    string ERPSubmitted = randomPages.GetERPSubmittedToSyncLV();
                    Assert.NotNull(ERPSubmitted);
                    extentReports.CreateLog("ERP Submitted To Sync in ERP section: " + ERPSubmitted + " is displayed ");

                    //Validate ERP ID
                    string ERPID = randomPages.GetERPIDLV();
                    Assert.NotNull(ERPID);
                    extentReports.CreateLog("ERP ID in ERP section: " + ERPID + " is displayed ");

                    
                    //string HLEntity = randomPages.GetHLEntityLV();
                    //Assert.AreEqual(entity, HLEntity);
                    //extentReports.CreateLog("HL Entity in ERP section: " + HLEntity + " matches with Legal Entity of Engagement ");

                    //Validate ERP HL Entity                 
                    string ERPEntity = randomPages.GetERPHLEntityLV();
                    Assert.AreEqual(entity, ERPEntity);
                    extentReports.CreateLog("ERP HL Entity in ERP section: " + ERPEntity + " matches with Legal Entity of Engagement ");

                    //Validate ERP Legal Entity                 
                    string ERPLegalEntity = randomPages.GetERPLegalEntityLV();
                    Assert.AreEqual(entity, ERPLegalEntity);
                    extentReports.CreateLog("ERP Legal Entity in ERP section: " + ERPLegalEntity + " matches with Legal Entity of Engagement ");

                    //Validate ERP Project Number                
                    string ERPProjectNumber = randomPages.GetERPProjectNumberLV();
                    Assert.AreEqual(engNumber, ERPProjectNumber);
                    extentReports.CreateLog("ERP Project Number in ERP section: " + ERPProjectNumber + " matches with Engagement Number ");

                    //Validate ERP Project Name                
                    string ERPProjectName = randomPages.GetERPProjectNameLV();
                    Assert.AreEqual(engName + " " + engNumber, ERPProjectName);
                    extentReports.CreateLog("ERP Project Name in ERP section: " + ERPProjectName + " is combination of both Engagement name and number ");

                    //Validate ERP LOB
                    
                    string ERPLOB = randomPages.GetERPLOBLV();
                    if (LOB.Equals("CF") || LOB.Equals("FR"))
                    {
                        Assert.AreEqual(LOB, ERPLOB);
                        extentReports.CreateLog("ERP LOB in ERP section: " + ERPLOB + " matches with Engagement's LOB ");
                    }
                    else
                    {
                        Assert.AreEqual("FA", ERPLOB);
                        extentReports.CreateLog("ERP LOB in ERP section: " + ERPLOB + " matches with Engagement's LOB ");
                    }

                    //Validate ERP Industry Group
                    //string IG = engagementDetails.GetIndustryGroup();
                    //string ERPIG = engagementDetails.GetERPIndustryGroup();
                    //Assert.AreEqual(IG, ERPIG);
                    //extentReports.CreateLog("ERP IG in ERP section: " + ERPIG + " matches with Engagement's IG ");

                    //Validate ERP Last Integration Status
                    string intStatus = randomPages.GetERPLastIntegrationStatusLV();
                    Assert.NotNull(intStatus);
                    extentReports.CreateLog("ERP Last Integration Status in ERP section: " + intStatus + " is displayed ");

                    //Validate ERP Last Integration Response Date
                    string resDate = randomPages.GetERPLastIntegrationResponseDateLV();
                    Assert.NotNull(resDate);
                    extentReports.CreateLog("ERP Last Integration Response Date in ERP section: " + resDate + " is displayed ");
                    randomPages.CloseActiveTab(engName);
                    extentReports.CreateLog("Engagement "+ engName+" page closed");


                    //*******Validate ERP Last Integration Error Description// Working without Error ******	
                    //string error = randomPages.GetERPIntegrationErrorLV();
                    //extentReports.CreateLog("*****Need to revisit ERP Last Integration Error Description in ERP section: " + error + " is displayed ");

                    ////----Validate Product Line by getting from Job Types page
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 4, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    pageTitle = randomPages.SelectJobTypesLV(jobType);
                    Assert.AreEqual(jobType, pageTitle);
                    extentReports.CreateStepLogs("Passed", "Page with title: " + pageTitle + " is displayed upon clicking Job Types link ");

                    //Get value of Product Type, Product Type Code 
                    string prodLine = randomPages.GetJobTypeProductLineLV();
                    string prodCode = randomPages.GetJobTypeProductTypeCodeLV();
                    randomPages.CloseActiveTab(jobType);
                    
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    engagementHome.SearchEngagementInLightningView(engName);
                    extentReports.CreateStepLogs("Passed", "Engagement: " + engName + " found and selected ");
                    //Full View
                    //randomPages.DetailPageFullViewLV();
                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected");

                    string productLine = randomPages.GetERPProductTypeLV();
                    Assert.AreEqual(prodLine, productLine);
                    extentReports.CreateStepLogs("Passed", "Product Type in ERP section: " + productLine + " matches with Product Line in Job Type Detail ");

                    //Validate ERP Product Type Code
                    string productCode = randomPages.GetERPProductTypeCodeLV();
                    Assert.AreEqual(prodCode, productCode);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type Code in ERP section: " + productCode + " matches with Product Type Code in Job Type Detail ");
                    randomPages.CloseActiveTab(engName);

                    //Already logged in as System admin
                    //Get legal entities details with admin login
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 5, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    extentReports.CreateStepLogs("Info", "List View is set to All Legal Entities");
                    pageTitle = randomPages.SelectLegalEntityLV(entity);
                    Assert.AreEqual(entity, pageTitle);
                    extentReports.CreateStepLogs("Passed", "Page with title: " + pageTitle + " is displayed upon clicking Legal Entiry link ");

                    //Get ERP Legal Entity ID,ERP Business Unit ID,ERP Template number,ERP Business Unit,ERP Entity Code,ERP Legislation Code
                    string templateNum = entityDetails.GetERPTemplateNumberLV();
                    string unitID = entityDetails.GetERPBusinessUnitIDLV();
                    string unit = entityDetails.GetERPBusinessUnitLV();
                    string entityID = entityDetails.GetERPLegalEntityIDLV();
                    string code = entityDetails.GetERPEntityCodeLV();
                    string legisCode = entityDetails.GetERPLegislationCodeLV();
                    randomPages.CloseActiveTab(entity);
                    //Search for the contact of the user added in Internal Team member                    
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 6, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    string contact = moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 2);
                    contactHome.SearchContactInLightning(contact);
                    string contactEmailID = contactHome.GetEmailIDOfContactLV();
                    randomPages.CloseActiveTab(contact);
                    extentReports.CreateStepLogs("Info", "Contact tab is closed");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    engagementHome.SearchEngagementInLightningView(engName);
                    extentReports.CreateStepLogs("Passed", "Engagement: " + engName + " found and selected ");
                    //Full View
                    //randomPages.DetailPageFullViewLV();
                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected");
                    //Validate ERP Template                
                    string ERPTemplate = randomPages.GetERPTemplateLV();
                    Assert.AreEqual(templateNum, ERPTemplate);
                    extentReports.CreateStepLogs("Passed", "ERP Template in ERP section: " + ERPTemplate + " matches with template number of Legal Entity: " + entity + " ");

                    //Validate ERP Business Unit ID             
                    string ERPUnitID = randomPages.GetERPBusinessUnitIDLV();
                    Assert.AreEqual(unitID, ERPUnitID);
                    extentReports.CreateStepLogs("Passed", "ERP Business Unit ID in ERP section: " + ERPUnitID + " matches with ERP Business Unit ID of Legal Entity: " + entity + " ");

                    //Validate ERP Business Unit              
                    string ERPUnit = randomPages.GetERPBusinessUnitLV();
                    Assert.AreEqual(unit, ERPUnit);
                    extentReports.CreateStepLogs("Passed", "ERP Business Unit in ERP section: " + ERPUnit + " matches with ERP Business Unit of Legal Entity: " + entity + " ");

                    //Validate ERP Legal Entity ID              
                    string ERPEntityID = randomPages.GetERPLegalEntityIDLV();
                    Assert.AreEqual(entityID, ERPEntityID);
                    extentReports.CreateStepLogs("Passed", "ERP Legal Entity ID in ERP section: " + ERPEntityID + " matches with ERP Legal Entity ID of Legal Entity: " + entity + " ");

                    //Validate ERP Entity Code              
                    string ERPEntityCode = randomPages.GetERPEntityCodeLV();
                    Assert.AreEqual(code, ERPEntityCode);
                    extentReports.CreateStepLogs("Passed", "ERP Entity Code in ERP section: " + ERPEntityCode + " matches with ERP Legal Entity ID of Legal Entity: " + entity + " ");

                    //Validate ERP Legislation Code             
                    string ERPLegCode = randomPages.GetERPLegCodeLV();
                    Assert.AreEqual(legisCode, ERPLegCode);
                    extentReports.CreateStepLogs("Passed", "ERP Legislation Code in ERP section: " + ERPLegCode + " matches with ERP Legal Entity ID of Legal Entity: " + entity + " ");
                                        
                    //Validate the creation of default contract
                    engagementDetails.ClickQuickLink("Contract");
                    extentReports.CreateStepLogs("Info", " Contract List Page is opened");                    
                    string contractID = engagementDetails.GetExistingContractNameLV();
                    Assert.AreEqual(contractID, engName);
                    extentReports.CreateStepLogs("Passed", "Contract with name: " + contractID + " similar to Engagement's name is created upon conversion to engagement ");
                    
                    //Validate ERP Contract Type
                    string contractType = engagementDetails.GetERPContractTypeLV();
                    Assert.AreEqual("Engagement", contractType);
                    extentReports.CreateStepLogs("Passed", "ERP Contract Type: " + contractType + " is displayed in Contract section ");

                    //Validate ERP Business Unit
                    string contractUnit = engagementDetails.GetContractERPBusinessUnitLV();
                    Assert.AreEqual(unit, contractUnit);
                    extentReports.CreateStepLogs("Passed", "ERP Business Unit: " + contractUnit + " is displayed in Contract section ");

                    //Validate ERP Legal Entity Name
                    string contractEntity = engagementDetails.GetContractERPLegalEntityNameLV();
                    Assert.AreEqual(entity, contractEntity);
                    extentReports.CreateStepLogs("Passed", "ERP Legal Entity Name: " + contractEntity + " is displayed in Contract section ");

                    //Validate ERP Bill Plan
                    string contractPlan = engagementDetails.GetContractERPBillPlanLV();
                    Assert.AreEqual("Bill Plan", contractPlan);
                    extentReports.CreateStepLogs("Passed", "ERP Bill Plan: " + contractPlan + " is displayed in Contract section ");

                    //Get Bill To Company
                    string contractBillTo = engagementDetails.GetContractBillToLV();

                    //Validate Contract Start Date
                    string contractStartDate = engagementDetails.GetContractStartDateLV();
                    if (LOB.Equals("FR") || LOB.Equals("FVA"))
                    {
                        Assert.AreEqual(DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture), contractStartDate);
                        extentReports.CreateStepLogs("Passed", "Contract Start Date: " + contractStartDate + " is displayed same as current date ");
                    }
                    else
                    {
                        Assert.AreEqual(DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture), contractStartDate);
                        extentReports.CreateStepLogs("Passed", "Contract Start Date: " + contractStartDate + " is displayed same as current date ");
                    }
                    //Validate Is Main Contract checkbox is checked
                    string mainContract = engagementDetails.GetIsMainContractStateLV();
                    Assert.AreEqual("Is Main Contract checkbox is checked", mainContract);
                    extentReports.CreateStepLogs("Passed", mainContract);
                    randomPages.CloseActiveTab("Contract");
                    extentReports.CreateStepLogs("Info", "Contract List Closed");

                    //Validate Bill To Company on Contact Page need to go to Contact section
                    engagementDetails.ClickEngContactTabLV();
                    extentReports.CreateStepLogs("Info", " Engagement Contact Tab is opened");
                    string compName = engagementDetails.GetCompanyNameOfContactLV();                    
                    Assert.AreEqual(compName, contractBillTo);
                    extentReports.CreateStepLogs("Passed", "Bill To: " + contractBillTo + " is displayed same as Engagement contact's company in Contract section ");
                   
                    //------------------------------
                    randomPages.CloseActiveTab("Engagement Contacts");
                    extentReports.CreateStepLogs("Info", "Engagement Contacts List Closed");
                    randomPages.CloseActiveTab(engName);
                    extentReports.CreateStepLogs("Info", "Engagement Tab is Closed");
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Pass", "System Administrator: " + adminUserExl + " logged out ");
                }
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

