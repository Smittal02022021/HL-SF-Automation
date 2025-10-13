using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.OpportunitiesConversion
{
    class LV_CF_TMTT0049771_1_TMTT0048726_TMTT0048726_VerifyMergersAcquisitionsMAChangesInOpportunityToEngagementConversionWithTheApprovalProcess:BaseClass
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

        public static string fileTMTT0049771 = "LV_1_CF_TMTT0049771_VerifyMergersAcquisitionsMAChangesInOpportunityCreationToEngagementConversionWithTheApprovalProcess";
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0123097	Verify that the M&A & CS opportunity is converted into an engagement and revenue accrual is added on to the engagement. 
        //TMTI0124212	Verify that the CAO who is part of "CAO CF" group and has assigned the "CAO MA" permission sets  has modify internal team access for the engagement having ERP product type code = 'MA' 
        //TMTI0124581   Verify that the CS CAO who is part of "CAO CF" group and has assigned the "CAO CS" permission sets does not have modify internal team access for the engagement having ERP product type code = 'MA' 
        //TMTI0124588	Verify that the CAO who is part of "CAO CF" group and has assigned the "CAO CS" permission sets  has modify internal team access for the engagement having ERP product type code = 'CS' 
        //TMTI0124593	Verify that the CS CAO who is part of "CAO CF" group and has assigned the "CAO MA" permission sets does not have modify internal team access for the engagement having ERP product type code = 'CS' 
        //TMTI0121572	Verify that the revenue accruals record is created on "Add Accruals" on the Engagement for the new job types.

        [Test]
        public void OpportunityToEngagementConversionMappingForCFLightningView()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0049771;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");
                
                int rowOpp = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                for (int row = 2; row <= rowOpp; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                    extentReports.CreateStepLogs("Info", "Creating Opportunity for : " + valJobType + " ");
                    //Login as Standard User profile and validate the user
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "StandardUsers", row,1);
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
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTMTT0049771);//updated move to jobtype
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0049771);
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
                    addOpportunityContact.CreateContactL2(fileTMTT0049771, valRecordType);
                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");
                    
                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForCFConversionLV2(fileTMTT0049771, valJobType);//udated Move to element
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled");
                    
                    // TMTI0124212 Verify that the CAO who is part of "CAO CF" group and has assigned the "CAO MA" permission sets  has modify internal team access for the engagement having ERP product type code = 'MA' 
                    //3 Click on “Internal Teams” tab and observe that the “Modify Roles” and “Roles Definitions” buttons are visible and editable.
                    Assert.IsTrue(opportunityDetails.IsModifyRoleButtonDisplayedInternalTeamDetailsLV(), "Verify Modify Role button is dispayed to the CF Finanical user on Opportunity Page");
                    extentReports.CreateStepLogs("Passed", "Modify Role button is dispayed to the CF Finanical user on Opportunity Page");
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0049771);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    randomPages.CloseActiveTab("Internal Team");
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", valUser + " Standard User logged out ");

                    string adminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 4, 1);
                    homePage.SearchUserByGlobalSearchN(adminUser);
                    extentReports.CreateStepLogs("Info", "Admin User: " + adminUser + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "Admin User: " + adminUser + " logged in on Lightning View");
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
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", adminUser + " System Administrator logged out ");

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

                    //TMTI0123097 Verify that the M & A opportunity is converted into an engagement and revenue accrual is added on to the engagement.
                    //Validate the ERP status on Engagement details page
                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected");

                    //3. Navigate to the "Oracle ERP" tab and verify the "Product Type", "ERP Product Type Code" field value. 
                    string productType = randomPages.GetERPProductTypeLV();
                    string prodType = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 1);
                    Assert.AreEqual(prodType, productType, "Verify the Product Type in Oracle ERP Information Opportunity details page for the opportunity having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type  " + productType + " in ERP section for Job Type: " + valJobType);

                    string prodTypeCodeERP = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 2);
                    string productTypeCodeERP = randomPages.GetERPProductTypeCodeLV();
                    Assert.AreEqual(prodTypeCodeERP, productTypeCodeERP, "Verify the Product code in Oracle ERP Information Opportunity details page for the opportunity having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type Code: " + productTypeCodeERP + " in ERP section for Job Type: " + valJobType);
                    string ERPStatusIG = randomPages.GetERPLastIntegrationStatusLV();
                    Assert.AreEqual("Success", ERPStatusIG);
                    extentReports.CreateStepLogs("Passed", "ERP Last Integration Status in ERP section: " + ERPStatusIG + " is displayed on Opportunity Detail page ");

                    //Submit Request To Engagement Conversion
                    opportunityDetails.ClickRequestToEngL();                     
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");
                    //randomPages.ReloadPage();

                    //5.Refresh the page and check the details in Approval history section.
                    //Verify the details in Opp History 
                    /*	Assigned To: Conversion CF MA
                    	Actual Approver: Conversion CF MA
                    */
                    string historyExpectedAssignTo= ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 3);
                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(),"Verify the Assign To for Approval Group Name before approval as CF inancial User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: "+ historyExpectedAssignTo+ " before approval as CF inancial User");

                    string historyExpectedActualApprover = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 4);
                    Assert.AreEqual(historyExpectedActualApprover, randomPages.GetHistoryActualApproverLV(), "Verify the Actual Approver Group Name before approval as CF inancial User") ;
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + historyExpectedActualApprover+ " before approval as CF inancial User");

                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover(); 

                    //Login as CAO user to approve the Opportunity
                    string userCAOExl = ReadExcelData.ReadData(excelPath, "CAOUsers", 1);
                    homePage.SearchUserByGlobalSearchN(userCAOExl);
                    extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    extentReports.CreateStepLogs("Passed", "User: " + userCAOExl + " logged in on Lightning View");
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity &Approve the Opportunity 
                    opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);

                    //TMTI0124212 Verify that the CAO who is part of "CAO CF" group and has assigned the "CAO MA" permission sets  has modify internal team access for the engagement having ERP product type code = 'MA' 
                    //6.Click on “Internal Teams” tab and observe that the “Modify Roles” and “Roles Definitions” buttons are visible and editable.
                    Assert.IsTrue(opportunityDetails.IsModifyRoleButtonDisplayedInternalTeamDetailsLV(), "Verify Modify Role button is dispayed to the CAO MA user on Opportunity Page");
                    extentReports.CreateStepLogs("Passed", "Modify Role button is dispayed to the CAO MA user");

                    //TMTI0123097	Verify that the M&A opportunity is converted into an engagement and revenue accrual is added on to the engagement. 
                    //6. Login as MA CAO and load the opportunity to Check the details in Approval history section.
                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name before approval as CAO User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo+ " before approval as CAO User  on Opportunity Page");

                    Assert.AreEqual(historyExpectedActualApprover, randomPages.GetHistoryActualApproverLV(), "Verify the Actual Approver Group Name before approval as CAO User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + historyExpectedActualApprover+ " before approval as CAO User");


                    string status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Passed", "Opportunity " + status + " ");;
                    randomPages.ReloadPage();
                    randomPages.CloseActiveTab("Approval History");

                    //7.Approve the request and check the Approval history details
                    /*Status: Approved
                    • Assigned To: Conversion CF MA/CS
                    • Actual Approver: CAO user 
                    • Comments: Approved
                    */
                    string historyStatus = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 5);
                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Assign To for Approval Group Name after approval as CAO User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyStatus+ " after approval as CAO User");

                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name after approval as CAO User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " after approval as CAO User");

                    Assert.AreEqual(userCAOExl, randomPages.GetHistoryActualApprovedLV(), "Verify the Actual Approver Group Name after approval as CAO User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + historyExpectedActualApprover+ "after approval as CAO User");

                    string historyComments = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 6);
                    Assert.AreEqual(historyComments, randomPages.GetHistoryCommentsLV(), "Verify the Actual Approver Group Name");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + historyComments);


                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEngagementL2();
                    extentReports.CreateStepLogs("Info", "Opportunity Converted into Engagement ");
                    //Validate the Engagement name in Engagement details page
                    string engagementNumber = engagementDetails.GetEngagementNumberL();
                    string engagementName = engagementDetails.GetEngagementNameL();
                    //Need to get Name of Opp and Eng
                    Assert.AreEqual(opportunityName, engagementName);
                    extentReports.CreateStepLogs("Passed", "Name of Engagement : " + engagementName + " is Same as Opportunity name ");

                    randomPages.CloseActiveTab(engagementName);
                    randomPages.CloseActiveTab(opportunityName);

                    //TMTI0124593	Verify that the CS CAO who is part of "CAO CF" group and has assigned the "CAO MA" permission sets does not have modify internal team access for the engagement having ERP product type code = 'CS' 

                    if (valJobType == "Equity Placements")
                    {
                        homePageLV.LogoutFromSFLightningAsApprover();
                        extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " logged out ");
                        //CS CAO who is part of "CAO CF" group and has assigned the "CAO MA" permission sets
                        userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 6, 1);
                        homePage.SearchUserByGlobalSearchN(userCAOExl);
                        extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " details are displayed. ");
                        //Login user
                        usersLogin.LoginAsSelectedUser();
                        login.SwitchToLightningExperience();
                        userCAO = login.ValidateUserLightningView();
                        Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                        extentReports.CreateStepLogs("Passed", "User: " + userCAOExl + " logged in on Lightning View");
                        //Go to Opportunity module in Lightning View 
                        homePageLV.SelectAppLV(appNameExl);
                        appName = homePageLV.GetAppName();
                        Assert.AreEqual(appNameExl, appName);
                        extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                        moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                        homePageLV.SelectModule(moduleNameExl);
                        extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                        engagementHome.GlobalSearchEngagementInLightningView(engagementName);

                        //Verify that the CS CAO who is part of "CAO CF" group and has assigned the "CAO MA" permission sets does not have modify internal team access for the engagement having ERP product type code = 'CS' 
                        Assert.IsFalse(engagementDetails.IsModifyRoleButtonDisplayedInternalTeamDetailsLV(), "Verify Modify Role button is not dispayed to CAO who is part of 'CAO CF' group and has assigned the 'CAO MA' permission sets on Engagement page");
                        extentReports.CreateStepLogs("Passed", "Modify Role button is not dispayed to CAO who is part of 'CAO CF' group and has assigned the 'CAO MA' permission sets on Engagement page");

                    }
                    else
                    {
                        moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                        homePageLV.SelectModule(moduleNameExl);
                        extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                        engagementHome.GlobalSearchEngagementInLightningView(engagementName);

                        //TMTI0124212
                        //9. CAO being a part of “CAO CF” group and is assigned the "CAO MA" permission sets, clicks on “Internal Teams” tab and observe that the “Modify Roles” and “Roles Definitions” buttons are visible and editable
                        Assert.IsTrue(engagementDetails.IsModifyRoleButtonDisplayedInternalTeamDetailsLV(), "Verify Modify Role button is dispayed to 'CAO CF' group and is assigned the 'CAO MA' permission sets on Engagement page");
                        extentReports.CreateStepLogs("Passed", "Modify Role button is dispayed to 'CAO CF' group and is assigned the 'CAO MA' permission sets on Engagement page");
                        
                        //TMTI0121572 Verify that the revenue accruals record is created on "Add Accruals" on the Engagement for the new job types.
                        //Click RevenueTab
                        string feesPA = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 7);
                        engagementDetails.AddAccrualLV(feesPA);
                        extentReports.CreateStepLogs("Passed", randomPages.GetLVMessagePopup());

                        //Get Estimated fees
                        string historyNewValue = engagementDetails.GetHistoryNewValueLV();
                        Assert.IsTrue(historyNewValue.Contains(feesPA), "Verify the Period Accrued Fees is saved and displayed in Engegement History section");
                        extentReports.CreateStepLogs("Passed", "Period Accrued Fees is saved and displayed in Engegement History section");

                        string revAccruTotalestimatedfee = engagementDetails.GetRevenueAccruTotalEstimatedFeeLV();
                        Assert.IsTrue(revAccruTotalestimatedfee.Contains(historyNewValue), "Verify the Total Estimated fees in Revenue Accrual section is matching the saved fees in Engegement History section");
                        extentReports.CreateStepLogs("Passed", "Total Estimated fees in Revenue Accrual section is matching the saved fees in Engegement History section");

                    }

                    //Validate the value of Record Type in Engagement details page
                    engagementDetails.ClickEngAdministrationTabLV();
                    string engRecordType = engagementDetails.GetRecordTypeLV();
                    string recordTypeExpected = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 2);
                    Assert.AreEqual(recordTypeExpected, engRecordType);
                    extentReports.CreateStepLogs("Passed", "Value of Record type is : " + engRecordType + " for Job Type " + valJobType + " ");
                    
                    //Validate the ERP status on Engagement details page
                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected");

                    productType = randomPages.GetERPProductTypeLV();
                    prodType = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 1);
                    Assert.AreEqual(prodType, productType, "Verify the Product Type in Oracle ERP Information Opportunity details page for the opportunity having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type  " + productType + " in ERP section for Job Type: " + valJobType);

                    prodTypeCodeERP = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 2);
                    productTypeCodeERP = randomPages.GetERPProductTypeCodeLV();
                    Assert.AreEqual(prodTypeCodeERP, productTypeCodeERP, "Verify the Product code in Oracle ERP Information Opportunity details page for the opportunity having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type Code: " + productTypeCodeERP + " in ERP section for Job Type: " + valJobType);

                    randomPages.ClickTabOracleERPLV();
                    ERPStatusIG = randomPages.GetERPLastIntegrationStatusLV();
                    Assert.AreEqual("Success", ERPStatusIG);
                    extentReports.CreateStepLogs("Passed", "ERP Last Integration Status in ERP section: " + ERPStatusIG + " is displayed on Engagement Detail page ");
                                        
                    randomPages.CloseActiveTab(engagementName);
                    randomPages.CloseActiveTab(engagementName);
                    //TMTI0124212	Verify that the CAO who is part of "CAO CF" group and has assigned the "CAO MA" permission sets  has modify internal team access for the engagement having ERP product type code = 'MA' 
                    //10 Opportunity “Internal teams” does not display “Modify Roles” and “Roles Definitions” buttons after converting the opportunity into an engagement. 

                    opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                    Assert.IsFalse(opportunityDetails.IsModifyRoleButtonDisplayedInternalTeamDetailsLV(), "Verify Modify Role button is not dispayed to the CF Finanical user on Engagement page");
                    extentReports.CreateStepLogs("Passed", "Modify Role button is not dispayed to the CF Finanical user  on Engagement page");
                    randomPages.CloseActiveTab(engagementName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " logged out ");


                    //11. Login as User - Alex Scott, load the Opportunity “Internal teams” does not display “Modify Roles” and “Roles Definitions” buttons after converting the opportunity into an engagement. 

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
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);

                    Assert.IsFalse(opportunityDetails.IsModifyRoleButtonDisplayedInternalTeamDetailsLV(), "Verify Modify Role button is not dispayed to the CF Finanical user on Engaged Opportunity page");
                    extentReports.CreateStepLogs("Passed", "Modify Role button is not dispayed to the CF Finanical user on Engaged Opportunity page");
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "CF Financiial User: " + valUser + " logged out ");


                    //******************************************************//
                    //TMTI0124581: Verify that the CS CAO who is part of "CAO CF" group and has assigned the "CAO CS" permission sets does not have modify internal team access for the engagement having ERP product type code = 'MA'
                    
                    string csCAOUser = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 3, 1);
                    homePage.SearchUserByGlobalSearchN(csCAOUser);
                    extentReports.CreateStepLogs("Info", "CS CAO User: " + csCAOUser + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "CS CAO User: " + csCAOUser + " logged in on Lightning View");
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "CS CAO User is on " + moduleNameExl + " Page ");
                    //Search for Engagement
                    engagementHome.GlobalSearchEngagementInLightningView(engagementName);

                    if (valJobType == "Sellside")
                    {
                        //3.Engagement “Internal teams” does not display “Modify Roles” and “Roles Definitions” buttons as the access to CS CAO is read - only for a MA engagement.
                        Assert.IsFalse(engagementDetails.IsModifyRoleButtonDisplayedInternalTeamDetailsLV(), "Verify Modify Role button is not dispayed to the CS CAO user on Engagement page");
                        extentReports.CreateStepLogs("Passed", "Modify Role button is not dispayed to the CS CAO user on Engagement page");


                        //4.Add Accrual option is not seen as the CAO – CS does not have access to add accrual to MA Engagement.
                        Assert.IsFalse(engagementDetails.IsButtonAddRevenueDisplayedLV(), "Add Accrual option is not seen as the CAO – CS does not have access to add accrual to MA Engagement.");
                        extentReports.CreateStepLogs("Passed", "Add Accrual button is not displayed for CS CAO user on MA Engagement");
                    }

                    //TMTI0124588	Verify that the CAO who is part of "CAO CF" group and has assigned the "CAO CS" permission sets  has modify internal team access for the engagement having ERP product type code = 'CS' 

                    if (valJobType == "Equity Placements")
                    {
                        Assert.IsTrue(engagementDetails.IsModifyRoleButtonDisplayedInternalTeamDetailsLV(), "Verify Modify Role button is dispayed to the CS CAO user on Engagement page");
                        extentReports.CreateStepLogs("Passed", "Modify Role button is dispayed to the CS CAO user on Engagement page");

                    }
                    randomPages.CloseActiveTab(engagementName);
                    randomPages.CloseActiveTab(engagementName);
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "CS CAO User is on " + moduleNameExl + " Page ");

                    if (valJobType == "Sellside")
                    {
                        //5.CS CAO who is part of "CAO CF" group Opportunity “Internal teams” does not display Modify Roles” and “Roles Definitions” buttons after converting the opportunity into an engagement. 
                        opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                        Assert.IsFalse(opportunityDetails.IsModifyRoleButtonDisplayedInternalTeamDetailsLV(), "Verify Modify Role button is not dispayed to the CS CAO user on Engaged Opportunity page");
                        extentReports.CreateStepLogs("Passed", "Modify Role button is not dispayed to the CS CAO user on Engaged Opportunity page");
                    }
                        randomPages.CloseActiveTab(opportunityName);
                        homePageLV.LogoutFromSFLightningAsApprover();
                        extentReports.CreateStepLogs("Info", "CS CAO User: " + csCAOUser + " logged out ");                     
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