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
    class LV_CF_TMTT0049771_2_VerifyTheCAOAndNonCAOCFGroupAndCAOMAAndOrCAOCSPermissionSetsHasModifiedInternalTeamAccessOnEngagementWithERPMAAndOrCS:BaseClass
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

        public static string fileTMTT0049771 = "LV_2_CF_TMTT0049771_VerifyTheCAOAndNonCAOCFGroupAndCAOMAAndOrCAOCSPermissionSetsHasModifiedInternalTeamAccessOnEngagementWithERPMAAndOrCS";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        //TMTI0124596 Verify that the CAO who is part of "CAO CF" group and has assigned the "CAO MA" and "CAO CS" permission sets  has modify internal team access for the engagement having ERP product type code = 'MA' and 'CS'
        //TMTI0124600 Verify that the CAO who is part of "CAO CF" group and has assigned neither "CAO MA" or "CAO CS" permission sets has modify internal team access for the engagement having ERP product type code = 'MA' or 'CS'
        //TMTI0124607 Verify that the CAO who is not part of "CAO CF" group and has assigned either or both "CAO MA", "CAO CS" permission sets  does not have modify internal team access for the engagement having ERP product type code = 'MA' or 'CS'


        [Test]
        public void VerifyTheCAOAndNonCAOCFGroupAndCAOMAAndOrCAOCSPermissionSetsHasModifiedInternalTeamAccessOnEngagementWithERPMAAndOrCSCFLightningView()
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
                    string valUser = ReadExcelData.ReadData(excelPath, "StandardUsers", 1);
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

                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0049771);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    randomPages.CloseActiveTab("Internal Team");
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", valUser + " Standard User logged out ");

                    string adminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 5, 1);
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
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "CF Financial User: " + valUser + " Logged out. ");
                    //////////////--------------------------------//////////////////

                    //Login as CAO user to approve the Opportunity
                    string userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 2, 1);
                    homePage.SearchUserByGlobalSearchN(userCAOExl);
                    extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " details are displayed. ");
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
                    string status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Passed", "Opportunity " + status + " ");
                    randomPages.CloseActiveTab("Approval History");
                    
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
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    engagementHome.GlobalSearchEngagementInLightningView(engagementName);

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

                    //TMTI0124596
                    //3. CAO being a part of “CAO CF” group and is assigned the "CAO CS" and "CAO MA" permission sets, clicks on “Internal Teams” tab and observe that the “Modify Roles” and “Roles Definitions” buttons are visible and editable on CS Engagement.
                    //5. CAO being a part of “CAO CF” group and is assigned the "CAO CS" and "CAO MA" permission sets, clicks on “Internal Teams” tab and observe that the “Modify Roles” and “Roles Definitions” buttons are visible and editable on MA Engagement .
                    Assert.IsTrue(engagementDetails.IsModifyRoleButtonDisplayedInternalTeamDetailsLV(), "Verify CAO user part of 'CAO CF' group and is assigned the 'CAO CS' and 'CAO MA' permission sets, clicks on 'Internal Teams' tab and observe that the 'Modify Roles' buttons are visible and editable on '"+ productTypeCodeERP+"' Engagement page");
                    extentReports.CreateStepLogs("Passed", "Modify Role button is dispayed to the CAO user: "+ userCAOExl+ " part of 'CAO CF' group and is assigned the 'CAO CS' and 'CAO MA' permission sets, clicks on 'Internal Teams' tab and observe that the 'Modify Roles' buttons are visible and editable on '" + productTypeCodeERP + "' Engagement page");

                    randomPages.CloseActiveTab(engagementName);
                    randomPages.CloseActiveTab(engagementName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info"," CAO User: "+ userCAOExl + " logged out ");

                    /////**************************///////////////
                    //TMTI0124600: Verify that the CAO who is part of "CAO CF" group and has neither assigned the "CAO MA" or "CAO CS" permission sets  does not have modify internal team access for the engagement having ERP product type code = 'MA' or'CS'
                    userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 3,1);
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
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    //moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    engagementHome.SearchEngagementInLightningView(engagementName);

                    //TMTI0124600
                    //3. CAO being a part of “CAO CF” group and is not assigned the "CAO CS" permission set, clicks on “Internal Teams” tab and observe that the “Modify Roles” and “Roles Definitions” buttons are visible and editable CS Engagement.
                    //5. CAO being a part of “CAO CF” group and is not assigned the "CAO MA" permission sets, clicks on “Internal Teams” tab and observe that the “Modify Roles” and “Roles Definitions” buttons are visible and editable on MA Engagement .
                    Assert.IsTrue(engagementDetails.IsModifyRoleButtonDisplayedInternalTeamDetailsLV(), "Verify CAO user part of 'CAO CF' group and is assigned the 'CAO CS' and 'CAO MA' permission sets, clicks on 'Internal Teams' tab and observe that the 'Modify Roles' buttons are visible and editable on '" + productTypeCodeERP + "' Engagement page");
                    extentReports.CreateStepLogs("Passed", "Modify Role button is dispayed to the CAO user: " + userCAOExl + " part of 'CAO CF' group and is assigned the 'CAO CS' and 'CAO MA' permission sets, clicks on 'Internal Teams' tab and observe that the 'Modify Roles' buttons are visible and editable on '" + productTypeCodeERP + "' Engagement page");

                    randomPages.CloseActiveTab(engagementName);
                    randomPages.CloseActiveTab(engagementName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", " CAO User: " + userCAOExl + " logged out ");
                    ///*********************************************///


                    ///********************************************///
                    //TMTI0124607: Verify that the CAO who is not part of "CAO CF" group and has assigned either  or both "CAO MA", "CAO CS" permission sets  does not have modify internal team access for the engagement having ERP product type code = 'MA' or 'CS'
                    userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 4, 1);
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
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    //moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    engagementHome.SearchEngagementInLightningView(engagementName);

                    //TMTI0124607
                    //3. CAO being a part of “CAO CF” group and is not assigned the "CAO CS" permission set, clicks on “Internal Teams” tab and observe that the “Modify Roles” and “Roles Definitions” buttons are not visible and editable CS Engagement.
                    //5. CAO being a part of “CAO CF” group and is not assigned the "CAO CS" permission set, clicks on “Internal Teams” tab and observe that the “Modify Roles” and “Roles Definitions” buttons are not visible and editable MA Engagement.
                    Assert.IsFalse(engagementDetails.IsModifyRoleButtonDisplayedInternalTeamDetailsLV(), "Verify CAO user part of 'CAO CF' group and is assigned the 'CAO CS' and 'CAO MA' permission sets, clicks on 'Internal Teams' tab and observe that the 'Modify Roles' buttons are not visible and editable on '" + productTypeCodeERP + "' Engagement page");
                    extentReports.CreateStepLogs("Passed", "Modify Role button is not dispayed to the CAO user: " + userCAOExl + " part of 'CAO CF' group and is assigned the 'CAO CS' and 'CAO MA' permission sets, clicks on 'Internal Teams' tab and observe that the 'Modify Roles' buttons are not visible and editable on '" + productTypeCodeERP + "' Engagement page");

                    randomPages.CloseActiveTab(engagementName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", " CAO User: " + userCAOExl + " logged out ");
                    ///*********************************************///
                    ///

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