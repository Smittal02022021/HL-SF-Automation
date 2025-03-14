﻿using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using NUnit.Framework;
using SF_Automation.TestData;
using System;

namespace SF_Automation.TestCases.OpportunitiesConversion
{
    class LV_T1432_TMTT0024858_TMTT0030610_TMTT0035436_OppToEngConversionMappingForFVAJobTypesToResultingERPRTPortfolioValuationRoleLimit : BaseClass
    {
        //Test Data is updated to check the New FVA Jo Type for following Tes Cases.//
        /*done
         TMTI0056866 Verify the availability of new Job Type- FA - Portfolio-Auto Struct Prd/Consulting in Job Type Picklist while adding new FVA Opportunity
         TMTI0056870 Verify user is able to create new Opportunity with new Job Type - FA - Portfolio-Auto Struct Prd/Consulting
         TMTI0056872 Verify the availability of Job Types for converted engagement on the Engagement page
         TMTI0056884 Verify the Record Type conversion of Opportunity to Engagement
         TMTI0028220 Verify the availability of new Job Types in Job Type Picklist while adding new Opportunity
         TMTI0028213 Verify user is able to create new Opportunity with new  Type 
        
        //TMTT0030610 done
         TMTI0071643 Verify the availability of new Job Type- CVAS - IP Valuation in Job Type Picklist while adding new FVA Opportunity
         TMTI0071652 Verify the availability of Job Types for converted engagement on the Engagement page 
         TMTI0071653 Verify that the user is able to create new Opportunity with new  Job Type - CVAS - IP Valuation
         TMTI0071656 Verify the Record Type conversion of Opportunity to Engagement
        
        //TMTT0035436 done
        TMTI0084227	Verify the availability of new Job Type- TAS - ESG Due Diligence & Analytics in Job Type Picklist while adding new FVA Opportunity
        TMTI0084215	Verify the availability of Job Types for converted engagement on the Engagement page
        TMTI0084219	Verify user is able to create new Opportunity with new  Job Type -TAS - ESG Due Diligence & Analytics        
        TMTI0084224	Verify the Record Type conversion of Opportunity to Engagement
        
        TMTT0024858 done
        TMTI0056869 Verify the availability of new Job Types on the Edit Engagement page

        TMTT0030610 done
        TMTI0071654 Verify the availability of new Job Types on the Edit Engagement page
        TMTI0071647 Verify the status is updated in the Oracle ERP Information section

        TMTT0035436 done
        TMTI0084220 Verify the availability of new Job Types on the Edit Engagement page
        TMTI0084221 Verify the status is updated in the Oracle ERP Information section

        */
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

        public static string fileT1432 = "LV_T1432_OpportunityToEngagementConversionMappingForFVAJobTypes";
        string exectedMaxLimit;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void OpportunityToEngagementConversionMappingForFVAOnLightningView()
        {
            try
            {                
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT1432;
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
                    string stdUserExl = ReadExcelData.ReadData(excelPath, "StandardUser",1);
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
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileT1432);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");
                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileT1432);
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
                    addOpportunityContact.CickAddOpportunityContactLV();
                    addOpportunityContact.CreateContactL2(fileT1432);
                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForFVAConversionLV(fileT1432);
                    if (valJobType.Contains("TAS"))
                    {
                        opportunityDetails.UpdateTASServicesLV();
                    }
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    
                   //Login as System Admin user 
                    string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUser", 3, 1);
                    extentReports.CreateStepLogs("Info", "System Admin User: " + adminUserExl + " Updating the Required details ");

                    homePage.SearchUserByGlobalSearchN(adminUserExl);
                    extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();

                    login.SwitchToClassicView();
                    string userAdmin = login.ValidateUser();
                    Assert.AreEqual(userAdmin.Contains(adminUserExl), true);
                    extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUserExl + " User logged in ");

                    login.SwitchToClassicView();
                    opportunityHome.SearchOpportunity(opportunityName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                    //update CC 
                    opportunityDetails.UpdateOutcomeDetails(fileT1432);
                    extentReports.CreateStepLogs("Info", "Conflict Check fields are updated");

                    /////////////////////////////////////////////////////////////////////
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "System Admin Switched to Lightning View ");
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");

                    //////Standard User don't have permission to modify the Internal team so System Admin is modifying the roles////////
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileT1432);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab("Internal Team");    
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab(opportunityName);
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Passed", "Admin: " + adminUserExl + "switched to Classic and Loggout ");

                    //Submit Request to Convert opportunity into Engagement.
                    extentReports.CreateStepLogs("Info", "Submit Request to Convert opportunity into Engagement");
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
                    opportunityDetails.ClickRequestToEngL();
                    //Submit Request To Engagement Conversion 
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");
                    //Log out of Standard User
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Info", "Standard User: " + stdUserExl + " switched to Classic and Loggout ");

                    //Approve and convert the Opporunity into Engagement
                    string caoUserExl = ReadExcelData.ReadData(excelPath, "CAOUser",1);
                    extentReports.CreateStepLogs("Info", "CAO User: " + caoUserExl + " Approving the Request for Engagement and converting into Engagement ");
                    //Search and Approve the DND Opp
                    homePage.SearchUserByGlobalSearchN(caoUserExl);
                    extentReports.CreateStepLogs("Info", "User: " + caoUserExl + " details are displayed. ");
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
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Pass", "User is on " + moduleNameExl + " Page ");
                    //Search for DND Approved opportunity with new name
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


                    randomPages.CloseActiveTab(engName);
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    engagementHome.SearchEngagementInLightningView(engName);


                    //TMTI0056869 Verify the availability of new Job Types on Edit Engagement page
                    //TMTI0071654 Verify the availability of new Job Types on the Edit Engagement page
                    //TMTI0084220 Verify the availability of new Job Types on Edit Engagement page
                    Assert.IsTrue(engagementDetails.IsJobTypePresentInDropdownOppDetailPageLV(valJobType));
                    extentReports.CreateLog("Job Type: " + valJobType + " is present on edit Engageent page ");
 
                    string engStage = engagementDetails.GetStageL();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row,1), engStage);
                    extentReports.CreateLog("Value of Stage field is : " + engStage + " for Job Type " + valJobType + " ");
                    engagementDetails.NavigateToAdministratorTabLV();

                    //Validate the value of Record Type in Engagement details page
                    string engRecordType = engagementDetails.GetRecordTypeLV();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 2), engRecordType);
                    extentReports.CreateLog("Value of Record type is : " + engRecordType + " for Job Type " + valJobType + " ");
                                       
                    //Validate the value of HL Entity in Engagement details page
                    string engLegalEntity = engagementDetails.GetLegalEntityLV();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row,3), engLegalEntity);
                    extentReports.CreateLog("Value of HL Entity is : " + engLegalEntity + " ");

                    //Validate the section in which Women led field and value is displayed
                    string secWomenLed = engagementDetails.GetSectionNameOfWomenLedFieldLV(valRecordType);
                    Assert.AreEqual("Administrative Info", secWomenLed);
                    string lblWomenLed = engagementDetails.ValidateWomenLedFieldLV();
                    Assert.AreEqual("Women Led", lblWomenLed);
                    extentReports.CreateLog(lblWomenLed + " field is displayed under section: " + secWomenLed + " ");

                    //if (valJobType.Contains("CVAS") || valJobType.Contains("TAS"))
                    //{
                       // Assert.AreEqual("Administrative Info", secWomenLed);
                    //}
                    //else
                    //{
                        Assert.AreEqual("Administrative Info", secWomenLed);
                    //}
                    extentReports.CreateLog(lblWomenLed + " field is displayed under section: " + secWomenLed + " ");

                    //Validate the value of Women Led in Engagement details page
                    string engWomenLed = engagementDetails.GetWomenLedLV();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 6), engWomenLed);
                    extentReports.CreateLog("Value of Women Led is : " + engWomenLed + " is same as selected in Opportunity page ");

                    //Internal Deal Team member on eng page are mapped from Opp page   
                    //string engInternalTeamMember = engagementDetails.GetEngDealTeammMemberLV();
                    //Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 14), engInternalTeamMember);
                    //extentReports.CreateStepLogs("Pass", "Internal Deal Team member: " + engInternalTeamMember + " is mapped on Engagement detail page after conversion ");

                    //Contact on eng page in mapped fom Opportunity
                    string engContactName = engagementDetails.GetEngExternalContactLV();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddContact", 1), engContactName);
                    extentReports.CreateStepLogs("Pass", "Opportunity Contact: " + engContactName + " is mapped on Engagement detail page after conversion ");
                    randomPages.CloseActiveTab(opportunityName);
                    /////////
                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "CAO User: " + caoUserExl + " switched to Classic and Loggout ");

                    //---------------------------------------------------------//
                    //Login Via System Admin to verify Last Integration the ERP Status
                    homePage.SearchUserByGlobalSearchN(adminUserExl);
                    extentReports.CreateStepLogs("Info", "User: " + caoUserExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();

                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "System Admin Loggin to Lightning View ");
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    engagementHome.GlobalSearchEngagementInLightningView(engName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");

                    //TMTI0071647 Verify the status is updated in the Oracle ERP Information section
                    //TMTI0084221 Verify the status is updated in Oracle ERP Information section
                    //Validate the ERP Last Integration Status on Engagement details page
                    //Full View
                    //randomPages.DetailPageFullViewLV();
                    randomPages.ClickTabOracleERPLV();
                    extentReports.CreateStepLogs("Info", "Oracle ERP tab is selected");

                    string ERPStatusOffice = randomPages.GetERPLastIntegrationStatusLV();
                    Assert.AreEqual("Success", ERPStatusOffice, "Verify the Engagement ERP Last Integration Status as Success ");
                    extentReports.CreateStepLogs("Passed", "Engagement ERP Last Integration Status in ERP section: " + ERPStatusOffice + " is displayed ");
                    randomPages.CloseActiveTab(engName);
                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "System Administrator: " + adminUserExl + " Logged out ");
                }
                login.SwitchToClassicView();
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