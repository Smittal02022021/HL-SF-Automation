using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.OpportunitiesConversion
{
    class LV_CF_TMTT0049771_3_TMTT0048726_VerifyMACSVerballyEngagedAndConvertedIntoFullyEngagedEngagementAndRevenueAccrualIsAddedOnEngagementPage:BaseClass
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

        public static string fileTMTT0049771 = "LV_TMTT0049771_VerifyMACSVerballyEngagedAndConvertedIntoFullyEngagedEngagementAndRevenueAccrualIsAddedOnEngagement";
                

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0123102	Verify that the M&A opportunity is converted into verbally engaged and converted into fully engaged engagement and revenue accrual is added on to the engagement.
        // MA & CS Job types scenarios
        //TMTI0121870	Verify that the new Job types are retained and displayed on converting the Opportunity to Verbally engaged
        //TMTI0121873	Verify that the new Job types are retained and displayed on converting a Verbally engaged Opportunity to a Fully engaged Opportunity

        [Test]
        public void VerifyMAOpportunityIsConvertedIntoVerballyEngagedAndConvertedIntoFullyEngagedEngagementAndRevenueAccrualIsAddedOnEngagementLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0049771;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

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
                    string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StandardUsers", row,1);

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

                    //Validating Title of New Opportunity Page
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                    extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileTMTT0049771);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0049771);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Info", "User is on Opportunity detail " + displayedTab + " tab ");

                    //Validating Opportunity details  
                    string oppName = opportunityDetails.GetOpportunityNameL();
                    string oppNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(oppNumber);
                    extentReports.CreateStepLogs("Passed", "Opportunity with number : " + oppNumber + " is created ");

                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "CF Financial User: " + userExl + " Logged out ");

                    string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 6, 1);
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
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0049771);
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
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " Job Type:  " + opportunityDetails.GetJobTypeLV());

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

                    opportunityDetails.EnterVerballyEngagedRequiredFieldsLV(valJobType, fileTMTT0049771);
                    extentReports.CreateStepLogs("Info", "Entered All Field level Required values");
                    opportunityDetails.ClickTabInfoLV();
                    string stage = opportunityDetails.GetStageLV();
                    string stageExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 31);
                    opportunityDetails.EditOpportunityStageLV(stageExl);
                    string updatedStage = opportunityDetails.GetStageLV();
                    Assert.AreEqual(updatedStage, stageExl);
                    extentReports.CreateStepLogs("Passed", "Opportunity Stage is updated from " + stage + " to " + updatedStage);
                    Assert.IsTrue(randomPages.GetVerballyEngCheckboxStatusLV(), "Verify Verbally Engaged checkbox is Checked after stage change of the Opportunity to Verbally Engaged");
                    extentReports.CreateStepLogs("Passed", "Verbally Engaged checkbox is Checked after stage change of the Opportunity to Verbally Engaged");


                    //TMTI0121870	Verify that the new Job types are retained and displayed on converting the Opportunity to Verbally engaged
                    Assert.AreEqual(valJobType,opportunityDetails.GetJobTypeLV(), "Verify that the new Job types are retained and displayed on converting the Opportunity to Verbally engaged");
                    extentReports.CreateStepLogs("Passed", "New Job type: "+ valJobType+" retained and displayed on converting the Opportunity to Verbally engaged");

                    randomPages.CloseActiveTab(opportunityName);
                    extentReports.CreateStepLogs("Info", updatedStage + " opportunity tab is closed");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "CF Financial User is on Partial Engaged " + moduleNameExl);
                    CustomFunctions.PageReload(driver);
                    engagementHome.GlobalSearchEngagementInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Info", " User is on " + updatedStage + " Engagement page");

                    //5.Click on the partial engagement link and load the partial engagement
                    Assert.IsTrue(randomPages.GetVerballyEngCheckboxStatusLV(), "Verify Verbally Engaged checkbox is Checked on Verbally Engaged Engagement");
                    extentReports.CreateStepLogs("Passed", "Verbally Engaged checkbox is Checked on Verbally Engaged Engagement");

                    //Validate the value of Record Type in Engagement details page
                    engagementDetails.ClickEngAdministrationTabLV();
                    string engRecordType = engagementDetails.GetRecordTypeLV();
                    string recordTypeExpected = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 2);
                    Assert.AreEqual(recordTypeExpected, engRecordType);
                    extentReports.CreateStepLogs("Passed", "Value of Record type is : " + engRecordType + " for Job Type " + valJobType + " ");

                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected");

                    //7. Navigate to the "Oracle ERP" tab and verify the "Product Type", "ERP Product Type Code" field value. 
                    productType = randomPages.GetERPProductTypeLV();                    
                    Assert.AreEqual(prodType, productType, "Verify the Product Type in Oracle ERP Information Opportunity details page for the opportunity having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type  " + productType + " in ERP section for Job Type: " + valJobType);

                   
                    productTypeCodeERP = randomPages.GetERPProductTypeCodeLV();
                    Assert.AreEqual(prodTypeCodeERP, productTypeCodeERP, "Verify the Product code in Oracle ERP Information Opportunity details page for the opportunity having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type Code: " + productTypeCodeERP + " in ERP section for Job Type: " + valJobType);

                    //8.Click on “Request Full Engagement” button and convert the partial engaged engagement to Full Engagement.

                    engagementDetails.ClickRequestFullEngagementLV();
                    engagementDetails.EnterRequestFullEngagementReqValuesLV();
                    extentReports.CreateStepLogs("Info", "Required Fields for Request Full Engagement are entered");
                    extentReports.CreateStepLogs("Info", randomPages.ClickInterruptionOKButtonLV());

                    //CF Financial user add Engagement Contact
                    engagementDetails.CickAddEngagementContactLV(valRecordType);
                    string billingContactNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", 3, 1);
                    string contactPartyExl = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", 3, 3);
                    engagementDetails.CreateBillingContactLV(billingContactNameExl, contactPartyExl);
                    string popupMessage = randomPages.GetLVMessagePopup();
                    //Assert.IsTrue(popupMessage.Contains("Engagement Contact"), "Verify the Added Engagement Contact is displayed in Popup message ");
                    extentReports.CreateStepLogs("Passed", billingContactNameExl + " Primary, Billing Contact added on Verbally Engaged Engagement page(Required for Full Engagement Request)");

                    engagementDetails.ClickRequestFullEngagementLV();
                    extentReports.CreateStepLogs("Info", "Click on Request Full Engagement button and Fill are required fields");
                    //*******Don't have clarify of the Engagement Information pop-up******
                    engagementDetails.ClickSaveEngagementInformationPopup();
                    extentReports.CreateStepLogs("Info", randomPages.ClickInterruptionOKButtonLV());
                    randomPages.ReloadPage();

                    //Check the details in Approval history section. 
                    /*MAEngagementReview
                    •	Status: Pending
                    •	Assigned To: Conversion CF MA
                    •	Actual Approver: Conversion CF MA
                    */
                    string historyStatus = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 8);
                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Status on Requested Full engaged Engagement");
                    extentReports.CreateStepLogs("Passed", "Status: " + historyStatus + " on Requested Full engaged Engagement");

                    string historyExpectedAssignTo = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 3);
                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name on Requested Full engaged Engagement");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " on Requested Full engaged Engagement");

                    string actualApprover = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 4);
                    Assert.AreEqual(actualApprover, randomPages.GetHistoryActualApproverLV(), "Verify the Actual Approver Group Name on Requested Full engaged Engagement");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + actualApprover + " on Requested Full engaged Engagement");
                                        
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "CF Financial User: " + userExl + " Logged out");

                    //9. Login as MA CAO – Brian Miller and load the partial engagement 
                    string userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", row,1);
                    homePage.SearchUserByGlobalSearchN(userCAOExl);
                    extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    extentReports.CreateStepLogs("Passed", "CAO User: " + userCAOExl + " logged in on Lightning View");
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " detail Page");

                    engagementHome.GlobalSearchEngagementInLightningView(opportunityName);
                    /*9. Login as MA CAO – Brian Miller and load the partial engagement – 142956. 
                     * check the details in Approval history section.
                    Conversion group is assigned to MA - 
                    •	Assigned To: Conversion CF MA
                    •	Actual Approver: Conversion CF MA
                    */
                    
                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Assign To for Approval Group Name Requested Full engaged Engagement as CAO User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyStatus + " Requested Full engaged Engagement as CAO User");

                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name Requested Full engaged Engagement as CAO User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " Requested Full engaged Engagement as CAO User");

                    Assert.AreEqual(actualApprover, randomPages.GetHistoryActualApproverLV(), "Verify the Actual Approver Group Name on Requested Full engaged Engagement as CAO User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + actualApprover + "on Requested Full engaged Engagement as CAO User as CAO User");

                    //10. Approve the Full Engagement request 
                    engagementDetails.ApproveVEEngagementLV();                    
                    randomPages.ReloadPage();
                    randomPages.CloseActiveTab("Approval History");

                    /*Approval History: 
                        MAEngagementReview
                        •	Status: Approved
                        •	Assigned To: Conversion CF MA
                        •	Actual Approver: Brian Miller
                        •	Comments: Full Engagement Request Approved
                        */

                    historyStatus = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 6);
                    Assert.AreEqual(historyStatus, randomPages.GetHistoryStatusLV(), "Verify the Assign To for Approval Group Name on Full engaged Engagement as CAO User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyStatus + " on Full engaged Engagement as CAO User");

                    //historyExpectedAssignTo = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 3);
                    Assert.AreEqual(historyExpectedAssignTo, randomPages.GetHistoryAssignToNameLV(), "Verify the Assign To for Approval Group Name on Full engaged Engagement as CAO User");
                    extentReports.CreateStepLogs("Passed", "Assigned To for Approval Group Name: " + historyExpectedAssignTo + " on Full engaged Engagement as CAO User");

                    Assert.AreEqual(userCAOExl, randomPages.GetHistoryActualApprovedLV(), "Verify the Actual Approver Group Name on Full engaged Engagement as CAO User");
                    extentReports.CreateStepLogs("Passed", "Actual Approver Group Name: " + actualApprover + "on Full engaged Engagement as CAO User as CAO User");

                    string historyComments = ReadExcelData.ReadDataMultipleRows(excelPath, "ProductType", row, 6);
                    Assert.AreEqual(historyComments, randomPages.GetHistoryCommentsLV(), "Verify the Comments on Requested Full engaged Engagement");
                    extentReports.CreateStepLogs("Passed", "Comments on Full engaged Engagement: " + historyComments);


                    //5.Click on the partial engagement link and load the partial engagement
                    Assert.IsFalse(randomPages.GetVerballyEngCheckboxStatusLV(), "Verify Verbally Engaged checkbox is Checked on Verbally Engaged Engagement");
                    extentReports.CreateStepLogs("Passed", "Verbally Engaged checkbox is not Checked on Fully Engaged Engagement");

                    //TMTI0121873	Verify that the new Job types are retained and displayed on converting a Verbally engaged Opportunity to a Fully engaged Opportunity
                    Assert.AreEqual(valJobType, engagementDetails.GetJobTypeL(), "Verify that the new Job types are retained and displayed on converting a Verbally engaged Opportunity to a Fully engaged Opportunity");
                    extentReports.CreateStepLogs("Passed", "New Job type: " + valJobType + " retained and displayed on converting the Opportunity to Verbally engaged to a Fully engaged Opportunity");


                    //11. Navigate to Administration tab and check the Record type. 
                    //Record Type: Sellside/Ac Adv

                    engagementDetails.ClickEngAdministrationTabLV();
                    engRecordType = engagementDetails.GetRecordTypeLV();
                    //recordTypeExpected = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 2);
                    Assert.AreEqual(recordTypeExpected, engRecordType);
                    extentReports.CreateStepLogs("Passed", "Value of Record type is : " + engRecordType + " for Job Type " + valJobType + " ");

                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected");

                    //12. Navigate to the "Oracle ERP" tab and verify the "Product Type", "ERP Product Type Code" field value
                    productType = randomPages.GetERPProductTypeLV();
                    Assert.AreEqual(prodType, productType, "Verify the Product Type in Oracle ERP Information Opportunity details page for the opportunity having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type  " + productType + " in ERP section for Job Type: " + valJobType);

                    productTypeCodeERP = randomPages.GetERPProductTypeCodeLV();
                    Assert.AreEqual(prodTypeCodeERP, productTypeCodeERP, "Verify the Product code in Oracle ERP Information Opportunity details page for the opportunity having Job type as " + valJobType);
                    extentReports.CreateStepLogs("Passed", "ERP Product Type Code: " + productTypeCodeERP + " in ERP section for Job Type: " + valJobType);

                    //13. Navigate to Revenue tab and check the revenue Accruals. 
                    //14. Click on “Add Accrual” button and enter value in “Period Accrued Fees” field and click save. 
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

                    randomPages.CloseActiveTab(opportunityName);
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " Logged out ");

                }
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Passed", "Browser Closed Successfully ");

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