using AventStack.ExtentReports.Gherkin.Model;
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
    class LV_T1426_112115_11219_11220_11221_TMTT0024069_TMTT0048726_VerifyOpportunityToEngagementConversionMappingForCFJobTypesOnOpportunityEngagementPageLightningView : BaseClass
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

        public static string fileTMTI0055384 = "LV_T1426_OpportunityToEngagementConversionMappingForCF";
        private string locationBenefit;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0055384 Verify the availability of new Job Type- Lender Education in Job Type Picklist while adding new CF Opportunity
        //TMTI0055395 Verify user is able to create new Opportunity with new Job Type - Lender Education
        //TMTI0113217 Verify that the "Tail Expires" field does not display on the CF page layout for different Job Types on creating new opportunities
        //TMTI0113236 Verify that the "Tail Expires" field is removed as a required field on the Engagement conversion errors page while converting existing opportunities into engagement
        //TMTI0120008 Verify that the new Job types are available in the Job type picklist in the New Opportunity page
        //TMTI0120042 Verify the Oracle ERP Information details in the Opportunity details page for the new Job type
        //TMTI0120044 Verify the Record type in the Engagement details page for the new Job type. 
        //TMTI0120046 Verify the Oracle ERP Information details in the Engagement details page for the new Job type.
        

        [Test]
        public void OpportunityToEngagementConversionMappingForCFLightningView()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0055384;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");
                //TMTI0055384 Verify the availability of new Job Type- Lender Education in Job Type Picklist while adding new CF Opportunity
                //TMTI0055395 Verify user is able to create new Opportunity with new Job Type - Lender Education

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
                    //TMTT0011215- Verify the Women Led field is available for all LOB:CF Opportunity
                    //TMTT0011221- Verify the Women-Led field under the administration section on the Opportunity page
                    ///////////////////////////////
                    //Validate Women Led field and Calling AddOpportunities function      
                    string womenLed = addOpportunity.ValidateWomenLedFieldLV(valRecordType);
                    string secName = addOpportunity.GetAdminSectionNameLV(valRecordType);
                    Assert.AreEqual("Women Led", womenLed);
                    Assert.AreEqual("Administration", secName);
                    extentReports.CreateStepLogs("Passed", "Field with name: " + womenLed + " is displayed under section: " + secName + " ");
                    /////////////////////////////////////                    

                    //TMTI0055384 Verify the availability of new Job Type- Lender Education in Job Type Picklist while adding new CF Opportunity
                    //TMTI0055395 Verify user is able to create new Opportunity with new Job Type - Lender Education
                    //TMTI0120008	Verify that the new Job types are available in the Job type picklist in the New Opportunity page

                    extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType);
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTMTI0055384);//updated move to jobtype
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTI0055384);
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
                    addOpportunityContact.CreateContactL2(fileTMTI0055384, valRecordType);
                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                    //TMTI0113217 Verify that the "Tail Expires" field does not display on the CF page layout for different Job Types on creating new opportunities
                    //TMTI0113236 Verify that the "Tail Expires" field is removed as a required field on the Engagement conversion errors page while converting existing opportunities into engagement
                    //TMTI0118698 Verify that the user is able to update the "Location where Benefit was Provided" field value and successfully request an engagement.
                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForCFConversionLV2(fileTMTI0055384, valJobType);//udated Move to element
                    extentReports.CreateStepLogs("Info", "Location where Benefit was Provided value filled ");
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTI0055384);
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

                    //TMTI0120042	Verify the Oracle ERP Information details in the Opportunity details page for the new Job type

                    //Validate the ERP status on Engagement details page
                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected");

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


                    opportunityDetails.ClickRequestToEngL();

                    //Submit Request To Engagement Conversion 
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");
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

                    //TMTI0055402 Verify the availability of Job Types for converted engagement on the Engagement page
                    //Search for created opportunity &Approve the Opportunity 
                    opportunityHome.GlobalSearchOpportunityInLightningView(opportunityName);
                    string status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Passed", "Opportunity " + status + " ");
                    opportunityDetails.CloseApprovalHistoryTabL();

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
                    engagementHome.SearchEngagementInLightningView(engagementName);
                    
                    // Get Job Type on Engagement page 

                    //TMTI0113232	Verify that the "Tail Expires" field continues to display on the CF Engagement page under Important Dates
                    engagementDetails.ClickImpDatesLV();
                    Assert.IsTrue(engagementDetails.IsTailExpiresFieldPresentLV(), "Verify that the 'Tail Expires' field continues to display on the CF Engagement page under Important Dates");
                    extentReports.CreateStepLogs("Passed", "'Tail Expires' field continues to display on the CF Engagement page under Important Dates");

                    //

                    //TMTI0055391 Verify the Record Type conversion of Opportunity to Engagement
                    //TMTI0120044	Verify the Record type in the Engagement details page for the new Job type. 

                    //Validate the value of Record Type in Engagement details page
                    engagementDetails.ClickEngAdministrationTabLV();                   
                    string engRecordType = engagementDetails.GetRecordTypeLV();
                    string recordTypeExpected =ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 2);
                    Assert.AreEqual(recordTypeExpected, engRecordType);
                    extentReports.CreateStepLogs("Passed", "Value of Record type is : " + engRecordType + " for Job Type " + valJobType + " ");


                    //TMTI0055387 Verify the status is updated in Oracle ERP Information section after creating the Opportunity
                    //TMTI0120046	Verify the Oracle ERP Information details in the Engagement details page for the new Job type. 

                    //Validate the ERP status on Engagement details page
                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected on Engagement Detail page");

                    productType = randomPages.GetERPProductTypeLV();
                    prodType = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 1);
                    Assert.AreEqual(prodType, productType, "Verify the Product Type in Oracle ERP Information Engagement details page for the opportunity having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "Engagement ERP Product Type  " + productType + " in ERP section for Job Type: " + valJobType);

                    prodTypeCodeERP = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 2);
                    productTypeCodeERP = randomPages.GetERPProductTypeCodeLV();
                    Assert.AreEqual(prodTypeCodeERP, productTypeCodeERP, "Verify the Product code in Oracle ERP Information Engagement details page for the opportunity having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "Engagement ERP Product Type Code: " + productTypeCodeERP + " in ERP section for Job Type: " + valJobType);

                    //randomPages.ClickTabOracleERPLV();
                    ERPStatusIG = randomPages.GetERPLastIntegrationStatusLV();
                    Assert.AreEqual("Success", ERPStatusIG);
                    extentReports.CreateStepLogs("Passed", "ERP Last Integration Status in ERP section: " + ERPStatusIG + " is displayed on Engagement Detail page ");
                   // extentReports.CreateStepLogs("Passed", valJobType+ "******************ERP Last Integration Status in ERP section: " + ERPStatusIG + " is displayed on Engagement Detail page***********");

                    //TMTT0011220 - Verify the Women Led field under Closing-**section on Engagement page
                    ///////////////////////////////////////////////////
                    //Validate the section in which Women led fiels is displayed
                    engagementDetails.NavigateToAdministratorTabLV();
                    string lblWomenLed = engagementDetails.ValidateWomenLedFieldLV();
                    Assert.AreEqual("Women Led", lblWomenLed);
                    extentReports.CreateStepLogs("Passed", "Field : " + lblWomenLed + " is found on converted Engagement ");

                    string secWomenLed = engagementDetails.GetSectionNameOfWomenLedFieldLV(valJobType);                    
                    Assert.AreEqual("Administrative Info", secWomenLed);
                    extentReports.CreateStepLogs("Passed", "Job Type:: "+ valJobType+ " "+lblWomenLed + " field is displayed under section: " + secWomenLed);

                    //TMTT0011219- Verify the Women-Led field is mapped to Engagement after conversion
                    //Validate the value of Women Led in Engagement details page
                    //string engWomenLed = engagementDetails.GetWomenLed();
                    string engWomenLed = engagementDetails.GetWomenLedLV();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 6), engWomenLed);
                    extentReports.CreateStepLogs("Passed", "Value of Women Led is : " + engWomenLed + " is same as selected in Opportunity page ");
                                        
                    
                    ///////////////////////////////////////////////// 
                    //randomPages.CloseActiveTab(engagementName);
                    //CustomFunctions.PageReload(driver);
                    //opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    //randomPages.ClickTabOracleERPLV();
                    ////engagementDetails.ClickRelatedOpportunityLink();
                    ////Validate the ERP status on Opp details page   
                    //string valERPStatus = randomPages.GetERPLastIntegrationStatusLV();
                    //Assert.AreEqual("Success", valERPStatus);
                    //extentReports.CreateStepLogs("Passed", "ERP Last Integration Status in ERP section: " + valERPStatus + " is displayed on Opportunity Detail page");

                    
                    randomPages.CloseActiveTab(engagementName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " logged out ");                    
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
