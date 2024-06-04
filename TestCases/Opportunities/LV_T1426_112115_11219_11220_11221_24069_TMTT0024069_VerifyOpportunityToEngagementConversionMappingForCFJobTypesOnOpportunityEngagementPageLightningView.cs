using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_T1426_112115_11219_11220_11221_24069_TMTT0024069_VerifyOpportunityToEngagementConversionMappingForCFJobTypesOnOpportunityEngagementPageLightningView : BaseClass
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

        public static string fileTMTI0055384 = "LV_T1426_OpportunityToEngagementConversionMappingForCF";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
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
                    usersLogin.SearchUserAndLogin(valUser);
                    login.SwitchToLightningExperience();   
                    string stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");                    
                    //homePageLV.ClickAppLauncher();
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
                    addOpportunityContact.CreateContactL2(fileTMTI0055384);
                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForCFConversionLV2(fileTMTI0055384);//udated Move to element
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTI0055384);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", valUser + " Standard User logged out ");

                    extentReports.CreateStepLogs("Info", "Admin is Performing Required Actions ");
                    opportunityHome.SearchOpportunity(opportunityName);
                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(fileTMTI0055384);
                    if (valJobType.Equals("Buyside") || valJobType.Equals("Sellside"))
                    {
                        opportunityDetails.UpdateNBCApproval();
                        extentReports.CreateStepLogs("Info", "Conflict Check and NBC fields are updated ");
                    }
                    else
                    {
                        extentReports.CreateStepLogs("Info", "Conflict Check fields are updated ");
                    }  
                    //TMTI0056861 Verify that NBC form is not required for new Job type - Lender education
                    //Get NBC Approved Default Status
                    Assert.AreEqual(opportunityDetails.GetNBCApprovedStatus(),"Checked");
                    extentReports.CreateStepLogs("Info", "NBC Approved Checkbox is already Checked ");

                    //Login again as Standard User
                    usersLogin.SearchUserAndLogin(valUser);
                    login.SwitchToLightningExperience();
                    stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");

                   // homePageLV.ClickAppLauncher();                    
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                    //Requesting for engagement and validate the success message
                    opportunityDetails.ClickRequestToEngL();

                    //Submit Request To Engagement Conversion 
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");
                    usersLogin.ClickLogoutFromLightningView();

                    //Login as CAO user to approve the Opportunity
                    string userCAOExl = ReadExcelData.ReadData(excelPath, "CAOUsers", 1);
                    usersLogin.SearchUserAndLogin(userCAOExl);

                    login.SwitchToLightningExperience();
                    string userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    extentReports.CreateStepLogs("Passed", "User: " + userCAOExl + " logged in on Lightning View");

                    //homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                    //TMTI0055402 Verify the availability of Job Types for converted engagement on the Engagement page
                    //Search for created opportunity &Approve the Opportunity 
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                    string status = opportunityDetails.ClickApproveButtonL();
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

                    login.SwitchToClassicView();

                    engagementHome.ClickEngagementTab();
                    engagementHome.SearchEngagementWithNumber(engagementNumber);

                    //TMTI0055391 Verify the Record Type conversion of Opportunity to Engagement
                    //Validate the value of Record Type in Engagement details page
                    string engRecordType = engagementDetails.GetRecordType();
                    string recordTypeExpected =ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 2);
                    Assert.AreEqual(recordTypeExpected, engRecordType);
                    extentReports.CreateStepLogs("Passed", "Value of Record type is : " + engRecordType + " for Job Type " + valJobType + " ");

                    //TMTI0055387 Verify the status is updated in Oracle ERP Information section after creating the Opportunity
                    //Validate the ERP status on Engagement details page
                    string ERPStatusIG = engagementDetails.GetEngERPIntegrationStatus();
                    Assert.AreEqual("Success", ERPStatusIG);
                    extentReports.CreateStepLogs("Passed", "ERP Last Integration Status in ERP section: " + ERPStatusIG + " is displayed on Engagement Detail page ");

                    //TMTT0011220 - Verify the Women Led field under Closing-**section on Engagement page
                    ///////////////////////////////////////////////////
                    //Validate the section in which Women led fiels is displayed
                    string lblWomenLed = engagementDetails.ValidateWomenLedField(valJobType);
                    Assert.AreEqual("Women Led", lblWomenLed);
                    extentReports.CreateStepLogs("Passed", "Field : " + lblWomenLed + " is found on converted Engagement ");
                    string secWomenLed = engagementDetails.GetSectionNameOfWomenLedField(valJobType);

                    if (valJobType.Contains("ESOP Corporate Finance") || valJobType.Contains("General Financial Advisory") || valJobType.Contains("Real Estate Brokerage") || valJobType.Contains("Special Committee Advisory") || valJobType.Contains("Strategic Alternatives Study") || valJobType.Contains("Take Over Defense") || valJobType.Equals("Activism Advisory") || valJobType.Equals("Strategy") || valJobType.Equals("Post Merger Integration") || valJobType.Equals("Valuation Advisory"))
                    {
                        Assert.AreEqual("Closing - Admin Details", secWomenLed);
                        extentReports.CreateStepLogs("Passed", "Section for Women Led is : " + secWomenLed + " ");
                    }
                    else
                    {
                        Assert.AreEqual("Closing - Document Checklist", secWomenLed);
                        extentReports.CreateStepLogs("Passed", "Section for Women Led is : " + secWomenLed + " ");
                    }
                    extentReports.CreateLog(lblWomenLed + " field is displayed under section: " + secWomenLed + " ");

                    //TMTT0011219- Verify the Women-Led field is mapped to Engagement after conversion
                    //Validate the value of Women Led in Engagement details page
                    string engWomenLed = engagementDetails.GetWomenLed();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 6), engWomenLed);
                    extentReports.CreateStepLogs("Passed", "Value of Women Led is : " + engWomenLed + " is same as selected in Opportunity page ");

                    /////////////////////////////////////////////////    

                    engagementDetails.ClickRelatedOpportunityLink();
                    //Validate the ERP status on Opp details page                
                    ERPStatusIG = opportunityDetails.GetERPIntegrationStatus();
                    Assert.AreEqual("Success", ERPStatusIG);
                    extentReports.CreateStepLogs("Passed", "ERP Last Integration Status in ERP section: " + ERPStatusIG + " is displayed on Opportunity Detail page ");

                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " logged out ");
                }
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
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
