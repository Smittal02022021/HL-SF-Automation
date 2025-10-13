using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using SalesForce_Project.Pages.JobTypes;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.OpportunitiesOracleERP
{
    class LV_TS01_ValidateERPSection_PostCreationOfOpportunityLightningView: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        LegalEntityDetail entityDetails = new LegalEntityDetail();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        ContactHomePage contactHome = new ContactHomePage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();
        JobTypesPage jobTypesPage = new JobTypesPage();

        public static string ERPTS01 = "LV_TS01_ValidateERPSection";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ValidateERPSectionLV()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + ERPTS01;
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
                    homePage.SearchUserByGlobalSearchN(valUserExl);
                    extentReports.CreateStepLogs("Info", "User: " + valUserExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
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
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, ERPTS01);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(ERPTS01);
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
                    addOpportunityContact.CreateContactL2(ERPTS01, valRecordType);
                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                    //Fetch values of Opportunity Name, Client, Subject and Job Type
                    string oppName = opportunityDetails.GetOpportunityNameL();
                    string clientName = opportunityDetails.GetClientLV();
                    string subjectName = opportunityDetails.GetSubjectLV();                    
                    string jobType = opportunityDetails.GetJobTypeLV();
                    
                    extentReports.CreateStepLogs("Info", "Required fields are entered for LOB: "+ valRecordType);
                    randomPages.CloseActiveTab(oppName);
                    extentReports.CreateStepLogs("Info", "Opportunity tab is closed");
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "User: " + valUserExl + " logged out");

                    //------Only System Admin can see the ERP Section on Opportunity Detail page//
                    string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 3);
                    homePage.SearchUserByGlobalSearchN(adminUserExl);
                    extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string userName = login.ValidateUserLightningView();
                    //Assert.AreEqual(userName.Contains(adminUserExl), true);
                    extentReports.CreateLog("System Administrator User: " + adminUserExl + " logged in on Lightning View");
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    opportunityHome.GlobalSearchOpportunityInLightningView(oppName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(ERPTS01);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityL();
                    randomPages.CloseActiveTab("Internal Team");
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                    //Get ERP LOB
                    string LOB = opportunityDetails.GetLOBLV();
                    //Validate ERP section details------
                    //Full View
                    randomPages.DetailPageFullViewLV();
                    extentReports.CreateStepLogs("Info", "Detail Page Full View is displayed ");

                    //Validate ERP Submitted To Sync
                    string ERPSubmittedDate = randomPages.GetERPSubmittedToSyncLV();
                    extentReports.CreateStepLogs("Info", "ERP Submitted To Sync in ERP section:: " + ERPSubmittedDate);                    

                    //Validate ERP ID
                    string ERPID = randomPages.GetERPIDLV();
                    Assert.NotNull(ERPID);
                    extentReports.CreateStepLogs("Passed", "ERP ID in ERP section: " + ERPID + " is displayed ");

                    //Validate ERP Project Status Code
                    string ERPProjStatusCode = randomPages.GetERPProjStatusCodeLV();
                    Assert.NotNull(ERPProjStatusCode);
                    extentReports.CreateStepLogs("Passed", "ERP Project Status Code in ERP section: " + ERPProjStatusCode + " is displayed ");

                    //Validate HL Entity
                    string entity = ReadExcelData.ReadData(excelPath, "AddOpportunity", 12);
                    string HLEntity = randomPages.GetHLEntityLV();
                    Assert.AreEqual(entity, HLEntity);
                    extentReports.CreateStepLogs("Passed", "HL Entity in ERP section: " + HLEntity + " matches with Legal Entity entered while creating Opportunity ");

                    //Validate ERP HL Entity                 
                    string ERPEntity = randomPages.GetERPHLEntityLV();
                    Assert.AreEqual(entity, ERPEntity);
                    extentReports.CreateStepLogs("Passed", "ERP HL Entity in ERP section: " + ERPEntity + " matches with Legal Entity entered while creating Opportunity ");

                    //Validate ERP Legal Entity                 
                    string ERPLegalEntity = randomPages.GetERPLegalEntityLV();
                    Assert.AreEqual(entity, ERPLegalEntity);
                    extentReports.CreateStepLogs("Passed", "ERP Legal Entity in ERP section: " + ERPLegalEntity + " matches with Legal Entity entered while creating Opportunity ");

                    //Validate ERP Project Number                
                    string ERPProjectNumber = randomPages.GetERPProjectNumberLV();
                    Assert.AreEqual(oppNumber, ERPProjectNumber);
                    extentReports.CreateStepLogs("Passed", "ERP Project Number in ERP section: " + ERPProjectNumber + " matches with Opportunity Number ");

                    //Validate ERP Project Name                
                    string ERPProjectName = randomPages.GetERPProjectNameLV();
                    Assert.AreEqual(oppName + " " + oppNumber, ERPProjectName);
                    extentReports.CreateStepLogs("Passed", "ERP Project Name in ERP section: " + ERPProjectName + " is combination of both Opportunity name and number ");
                    
                    string ERPLOB = randomPages.GetERPLOBLV();
                    if (LOB.Equals("CF") || LOB.Equals("FR"))
                    {
                        Assert.AreEqual(LOB, ERPLOB);
                        extentReports.CreateStepLogs("Passed", "ERP LOB in ERP section: " + ERPLOB + " matches with Opportunity's LOB ");
                    }
                    else
                    {
                        Assert.AreEqual("FA", ERPLOB);
                        extentReports.CreateStepLogs("Passed", "ERP LOB in ERP section: " + ERPLOB + " matches with Opportunity's LOB ");
                    }
                    //Validate ERP Industry Group// IG removed from UI 
                    //string IG = opportunityDetails.GetIndustryGroup();
                    //string ERPIG = randomPages.GetERPIndustryGroupLV();
                    //Assert.AreEqual(IG, ERPIG);

                    extentReports.CreateStepLogs("Info", "****Industry Group field is removed from Opp Detail page  ");//+ ERPIG +

                    //Validate ERP Last Integration Status
                    string intStatus = randomPages.GetERPLastIntegrationStatusLV();
                    Assert.NotNull(intStatus);
                    extentReports.CreateStepLogs("Passed", "ERP Last Integration Status in ERP section: " + intStatus + " is displayed ");

                    //Validate ERP Last Integration Response Date
                    string resDate = randomPages.GetERPLastIntegrationResponseDateLV();
                    Assert.NotNull(resDate);
                    extentReports.CreateStepLogs("Passed", "ERP Last Integration Response Date in ERP section: " + resDate + " is displayed ");

                    //Validate ERP Last Integration Error Description	
                    if(intStatus== "Success")
                    {
                        extentReports.CreateStepLogs("Passed", "Error Description nor present ");

                    }
                    else
                    {
                        string error = randomPages.GetERPIntegrationErrorLV();
                        extentReports.CreateStepLogs("Passed", "ERP Last Integration Error Description in ERP section: " + error + " is displayed ");
                        
                    }
                    randomPages.CloseActiveTab(oppName);
                    ////----Validate Product Line by getting from Job Types page
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 4, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    randomPages.SelectListViewLV("All");
                    extentReports.CreateStepLogs("Info", " All List option is selected ");
                    jobTypesPage.SearchJobtypeLV(jobType);
                    pageTitle = randomPages.SelectJobTypesLV(jobType);
                    Assert.AreEqual(jobType, pageTitle);
                    extentReports.CreateStepLogs("Passed", "Page with title: " + pageTitle + " is displayed upon clicking Job Types link ");

                    //Get value of Product Type, Product Type Code from Job Type Object page
                    string prodLine = randomPages.GetJobTypeProductLineLV();
                    string prodCode = randomPages.GetJobTypeProductTypeCodeLV();

                    randomPages.CloseActiveTab(jobType); //Close Active Job Type Tab
                    extentReports.CreateStepLogs("Info", "Job Types tab is closed");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    opportunityHome.SearchOpportunitiesInLightningView(oppName);
                    randomPages.DetailPageFullViewLV();
                    extentReports.CreateStepLogs("Info", "Detail Page Full View is displayed ");
                    //Validate Product Line
                    string productLine = randomPages.GetERPProductTypeLV();
                    Assert.AreEqual(prodLine, productLine);
                    extentReports.CreateStepLogs("Passed", "Product Type in ERP section: " + productLine + " matches with Product Line in Job Type Detail ");

                    //Validate ERP Product Type Code 15
                    string productCode = randomPages.GetERPProductTypeCodeLV();
                    Assert.AreEqual(prodCode, productCode);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type Code in ERP section: " + productCode + " matches with Product Type Code in Job Type Detail ");
                    randomPages.CloseActiveTab(oppName);
                    extentReports.CreateStepLogs("Info", "Opportunity tab is closed");

                    //Already logged in as System admin
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 5, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //randomPages.SelectListViewLV("All Legal Entities");
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
                    extentReports.CreateStepLogs("Info", "Entity tab is closed");

                    //Search for the contact of the user added in Internal Team member
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 6, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    string contact= moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 2);
                    contactHome.SearchContactInLightning(contact);
                    string contactEmailID = contactHome.GetEmailIDOfContactLV();
                    randomPages.CloseActiveTab(contact);
                    extentReports.CreateStepLogs("Info", "Contact tab is closed");                  

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    opportunityHome.SearchOpportunitiesInLightningView(oppName);
                    randomPages.DetailPageFullViewLV();
                    extentReports.CreateStepLogs("Info", "Detail Page Full View is displayed ");

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

                    //Validate ERP Principal Manager           
                    string ERPEmailID = randomPages.GetERPEmailIDLV();
                    Assert.AreEqual(contactEmailID, ERPEmailID);
                    string valStaff = ReadExcelData.ReadData(excelPath, "AddOpportunity", 14);
                    extentReports.CreateStepLogs("Passed", "ERP Principal Manager in ERP section: " + ERPEmailID + " matches with email id of contact of Internal team member: " + valStaff + " ");
                    randomPages.CloseActiveTab(oppName);
                    extentReports.CreateStepLogs("Info", "Opportunity tab is closed");
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "System Admin : " + adminUserExl + " logged out");
                }
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
