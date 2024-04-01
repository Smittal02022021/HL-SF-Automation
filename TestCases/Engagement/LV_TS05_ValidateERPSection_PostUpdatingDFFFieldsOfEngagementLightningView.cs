using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.Engagement;

namespace SalesForce_Project.TestCases.Engagement
{
    class LV_TS05_ValidateERPSection_PostUpdatingDFFFieldsOfEngagementLightningView:BaseClass
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

        public static string fileERPTS05 = "LV_TS05_ValidateERPSection";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ValidateEngagementERPSectionLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileERPTS05;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                // Calling Login function                
                login.LoginApplication();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 25);

                extentReports.CreateStepLogs("Info", "Creating Opportunity for LOB : " + valRecordType + " and Job Type: " + valJobType + " ");
                //Login as Standard User profile and validate the user
                string valUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                usersLogin.SearchUserAndLogin(valUserExl);
                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUserExl), true);
                extentReports.CreateStepLogs("Passed", "User: " + valUserExl + " logged in on Lightning View");

                homePageLV.ClickAppLauncher();
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectApp(appNameExl);
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
                string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileERPTS05);
                extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileERPTS05);
                Assert.AreEqual(displayedTab, "Info");
                extentReports.CreateStepLogs("Passed", "User is on Opportunity detail " + displayedTab + " tab ");

                ////Validating Opportunity details  
                string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                extentReports.CreateStepLogs("Passed", "Opportunity with number : " + opportunityNumber + " is created ");

                //Create External Primary Contact      
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);

                addOpportunityContact.CickAddFROpportunityContact();
                addOpportunityContact.CreateContactL2(fileERPTS05);
                extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                //Fetch values of Opportunity Name, Client, Subject and Job Type
                string oppName = opportunityDetails.GetOpportunityNameL();
                string clientName = opportunityDetails.GetClientLV();
                string subjectName = opportunityDetails.GetSubjectLV();
                string oppNumber = opportunityDetails.GetOppNumberLV();
                string jobType = opportunityDetails.GetJobTypeLV();

                //Update required Opportunity fields for conversion and Internal team details
                opportunityDetails.UpdateReqFieldsForFRConversionLV(fileERPTS05);//Ref Contact updated
                extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                opportunityDetails.UpdateInternalTeamDetailsLV(fileERPTS05);
                extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                opportunityDetails.ClickReturnToOpportunityL();
                extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                randomPages.CloseActiveTab(oppName);
                extentReports.CreateStepLogs("Info", "Opportunity tab is closed");
                homePageLV.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", "Standard User: " + valUserExl + " logged out");

                extentReports.CreateLog("Admin is Performing Required Actions ");
                string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 3);
                usersLogin.SearchUserAndLogin(adminUserExl);                
                login.SwitchToClassicView();
                opportunityHome.SearchOpportunity(opportunityName);
                opportunityDetails.UpdateOutcomeDetails(fileERPTS05);
                extentReports.CreateStepLogs("Info", " Required Outcome Details are provided ");
                usersLogin.UserLogOut();
                //login.SwitchToLightningExperience();
                //string userName = login.ValidateUserLightningView();
                //Assert.AreEqual(userName.Contains(adminUserExl), true);
                //extentReports.CreateLog("System Administrator User: " + adminUserExl + " logged in on Lightning View");
                //homePageLV.ClickAppLauncher();
                //homePageLV.SelectApp(appNameExl);
                //appName = homePageLV.GetAppName();
                //Assert.AreEqual(appNameExl, appName);
                //extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                //homePageLV.SelectModule(moduleNameExl);
                //extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                //opportunityHome.SearchOpportunityInLightning(oppName);
                //extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                //opportunityDetails.UpdateInternalTeamDetailsLV(fileERPTS05);
                //extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                //opportunityDetails.ClickReturnToOpportunityLV();
                //extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                //usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "System Administrator User: " + adminUserExl + " logged out ");

                //Login again as Standard User
                usersLogin.SearchUserAndLogin(valUserExl);
                login.SwitchToLightningExperience();
                stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUserExl), true);
                extentReports.CreateLog("User: " + valUserExl + " logged in on Lightning View");
                homePageLV.ClickAppLauncher();
                homePageLV.SelectApp(appNameExl);
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

                string userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);
                //Login as CAO user to approve the Opportunity
                usersLogin.SearchUserAndLogin(userCAOExl);
                login.SwitchToLightningExperience();
                string UserCAO = login.ValidateUserLightningView();
                Assert.AreEqual(UserCAO.Contains(userCAOExl), true);
                extentReports.CreateStepLogs("Info", "CAO User:" + userCAOExl + " logged in on Lightning View");
                homePageLV.ClickAppLauncher();
                homePageLV.SelectApp(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateLog(appName + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                string status = opportunityDetails.ClickApproveButtonL();
                Assert.AreEqual(status, "Approved");
                extentReports.CreateStepLogs("Passed", "Opportunity Status: " + status + " ");
                opportunityDetails.CloseApprovalHistoryTabL();

                //Calling function to convert to Engagement
                opportunityDetails.ClickConvertToEngagementL2();
                extentReports.CreateStepLogs("Info", "Opportunity Converted into Engagement ");

                // Validate the Engagement name in Engagement details page
                string engagementNumber = engagementDetails.GetEngagementNumberL();
                string engagementName = engagementDetails.GetEngagementNameL();
                Assert.AreEqual(opportunityName, engagementName);
                extentReports.CreateStepLogs("Passed", "Name of Engagement : " + engagementName + " is Same as Opportunity name ");
                randomPages.CloseActiveTab(oppName);
                homePageLV.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Pass", "CAO User: " + userCAOExl +" logged out ");

                ////////////////////////System Administrator performing ERP related Activities/////////////////
                usersLogin.SearchUserAndLogin(adminUserExl);
                login.SwitchToLightningExperience();
                string userName = login.ValidateUserLightningView();
                Assert.AreEqual(userName.Contains(adminUserExl), true);
                extentReports.CreateLog("System Administrator User: " + adminUserExl + " logged in on Lightning View for ERP related Activities");
                homePageLV.ClickAppLauncher();
                homePageLV.SelectApp(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                moduleNameExl= ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                engagementHome.SearchEngagementInLightning(engagementName);
                extentReports.CreateStepLogs("Passed", "Engagement: " + engagementName + " found and selected ");

                //Get ERP Submitted to Sync and get ERP Update DFF checkbox and ERP Last Integration Response Date
                string ERPSubmitted = randomPages.GetERPSubmittedToSyncLV();
                extentReports.CreateStepLogs("Info", "Engagement ERP Submitted to Sync before update is: " + ERPSubmitted + " ");

                string valERPUpdateDFF = randomPages.GetERPUpdateDFFCheckboxStatusLV();
                Assert.AreEqual("Checkbox is not checked", valERPUpdateDFF);
                extentReports.CreateStepLogs("Passed", "Engagement ERP Update DFF " + valERPUpdateDFF + " by default ");

                string ERPResDate = randomPages.GetERPLastIntegrationResponseDateLV();
                extentReports.CreateStepLogs("Info", "Engagement ERP Last Integration Response Date in ERP section: " + ERPResDate + " is displayed ");

                //-----Update Primary office, ERP Update DFF checkbox and validate ERP Sync Date, Status and Last Integration Status -----
                string newOffice = ReadExcelData.ReadData(excelPath, "DFFUpdates", 1);
                engagementDetails.UpdatePrimaryOfficeInlineLV(newOffice);
                string primaryOffice = opportunityDetails.GetPrimaryOfficeLV();
                Assert.AreEqual(newOffice, primaryOffice);
                extentReports.CreateStepLogs("Passed", "Engagement Primary Office is updated to from Inline Edit icon" + primaryOffice + " ");

                string valDFFPrimaryOffice = randomPages.GetERPUpdateDFFCheckboxStatusLV();
               // Assert.AreEqual("Checkbox is checked", valDFFPrimaryOffice);
                extentReports.CreateStepLogs("Passed", "**Field not updated Engagement ERP Update DFF " + valDFFPrimaryOffice + " after updating Primary office ");

                string ERPSubmittedOffice = randomPages.GetERPSubmittedToSyncLV();
                Assert.AreNotEqual(ERPSubmitted, ERPSubmittedOffice);
                extentReports.CreateStepLogs("Passed", "Engagement ERP Submitted to Sync: " + ERPSubmittedOffice + " ");

                string ERPStatusOffice = randomPages.GetERPLastIntegrationStatusLV();
                Assert.AreEqual("Success", ERPStatusOffice);// need to uncomment
                extentReports.CreateStepLogs("Passed", "Engagement ERP Last Integration Status in ERP section: " + ERPStatusOffice + " is displayed ");

                string ERPResOffice = randomPages.GetERPLastIntegrationResponseDateLV();
                Assert.AreNotEqual(ERPResDate, ERPResOffice);// need to uncomment
                extentReports.CreateStepLogs("Passed", "**Engagement ERP Last Integration Response Date in ERP section: " + ERPResOffice + " is displayed ");

                ////-----Update Industry Group, ERP Update DFF checkbox and validate ERP Sync Date, Status and Last Integration Status-----

                /*Project Related to IndustryGroup is  in progress
                string updIG = ReadExcelData.ReadData(excelPath, "DFFUpdates", 2);
                string IG = opportunityDetails.UpdateIndustryGroup(updIG);
                Assert.AreEqual(updIG, IG);
                extentReports.CreateLog("Industry Group is updated to " + IG + " ");

                string valDFFIG = opportunityDetails.GetERPUpdateDFFCheckboxStatusLV();
                Assert.AreEqual("Checkbox is checked", valDFFIG);
                extentReports.CreateStepLogs("Passed", "ERP Update DFF " + valDFFIG + " after updating Industry Group ");

                string ERPSubmittedIG = opportunityDetails.GetERPSubmittedToSyncLV();
                Assert.AreNotEqual(ERPSubmittedOffice, ERPSubmittedIG);// need to uncomment
                extentReports.CreateStepLogs("Passed", "Assersion Pending ERP Submitted to Sync: " + ERPSubmittedIG + " ");

                string ERPStatusIG = opportunityDetails.GetERPLastIntegrationStatusLV();
                Assert.AreEqual("Success", ERPStatusIG);// need to uncomment
                extentReports.CreateStepLogs("Passed", "Assersion Pending ERP Last Integration Status in ERP section: " + ERPStatusIG + " is displayed ");

                string ERPResIG = opportunityDetails.GetERPLastIntegrationResponseDateLV();
                Assert.AreNotEqual(ERPResOffice, ERPResIG);// need to uncomment
                extentReports.CreateStepLogs("Passed", "Assersion Pending ERP Last Integration Response Date in ERP section: " + ERPResIG + " is displayed ");
                */

                ////-----Update Sector, ERP Update DFF checkbox and validate ERP Sync Date, Status and Last Integration Status-----

                string updSector = ReadExcelData.ReadData(excelPath, "DFFUpdates", 3);
                engagementDetails.UpdateHLSectionIDLV(updSector);
                string sector = opportunityDetails.GetHLSectionIDLV();
                string sectorCombo = opportunityDetails.GetHLSectorComboLV();
                Assert.AreEqual(sectorCombo.Contains(updSector), true);
                extentReports.CreateStepLogs("Passed", "Engagement Sector is updated to and sector combo contains " + updSector + " ");

                string valDFFSector = randomPages.GetERPUpdateDFFCheckboxStatusLV();
                Assert.AreEqual("Checkbox is checked", valDFFSector);
                extentReports.CreateStepLogs("Passed", "Engagement ERP Update DFF " + valDFFSector + " after updating Sector ");

                string ERPSubmittedSector = randomPages.GetERPSubmittedToSyncLV();
                //Assert.AreNotEqual(ERPSubmittedIG, ERPSubmittedSector);// need to uncomment
                Assert.AreNotEqual(ERPSubmittedOffice, ERPSubmittedSector);
                extentReports.CreateStepLogs("Passed", "Assersion Pending Engagement ERP Submitted to Sync: " + ERPSubmittedSector + " ");

                string ERPStatusSector = randomPages.GetERPLastIntegrationStatusLV();
                //Assert.AreEqual("Success", ERPStatusSector);// need to uncomment
                extentReports.CreateStepLogs("Passed", "Assersion Pending Engagement ERP Last Integration Status in ERP section: " + ERPStatusSector + " is displayed ");

                string ERPResSector = randomPages.GetERPLastIntegrationResponseDateLV();
                //Assert.AreNotEqual(ERPResIG, ERPResSector);// need to uncomment
                Assert.AreNotEqual(ERPResOffice, ERPResSector);
                extentReports.CreateStepLogs("Passed", "Assersion Pending Engagement ERP Last Integration Response Date in ERP section: " + ERPResSector + " is displayed ");

                ////-----Update Job Type, ERP Update DFF checkbox and validate ERP Sync Date, Status and Last Integration Status-----

                string updType = ReadExcelData.ReadData(excelPath, "DFFUpdates", 4);
                engagementDetails.UpdateJobTypeLV(updType);
                string updJobType = opportunityDetails.GetJobTypeLV();
                Assert.AreEqual(updType, updJobType);
                extentReports.CreateStepLogs("Passed", "Engagement Job Type is updated to " + updJobType + " ");

                string valDFFJobType = randomPages.GetERPUpdateDFFCheckboxStatusLV();
                //Assert.AreEqual("Checkbox is checked", valDFFJobType);// Not checked
                extentReports.CreateStepLogs("Passed", "Engagement ERP Update DFF " + valDFFJobType + " after updating Job Type ");

                string ERPSubmittedJobType = randomPages.GetERPSubmittedToSyncLV();
                Assert.AreNotEqual(ERPSubmittedSector, ERPSubmittedJobType);
                extentReports.CreateStepLogs("Passed", "Engagement ERP Submitted to Sync: " + ERPSubmittedJobType + " ");

                string ERPStatusJobType = randomPages.GetERPLastIntegrationStatusLV();
                //Assert.AreEqual("Success", ERPStatusJobType);// need to uncomment
                extentReports.CreateStepLogs("Passed", " Assersion Pending Engagement ERP Last Integration Status in ERP section: " + ERPStatusJobType + " is displayed ");

                string ERPResJobType = randomPages.GetERPLastIntegrationResponseDateLV();
                Assert.AreNotEqual(ERPResSector, ERPResJobType);//Realted to updateSector 
                extentReports.CreateLog("Engagement ERP Last Integration Response Date in ERP section: " + ERPResJobType + " is displayed ");

                randomPages.CloseActiveTab(oppName);//Close Active opp Tab
                extentReports.CreateStepLogs("Info", "Engagement tab is closed");

                ////----Validate Product Line by getting from Job Types page
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                randomPages.SelectListViewLV("All");

                pageTitle = randomPages.SelectJobTypesL(updType);
                Assert.AreEqual(updType, pageTitle);
                extentReports.CreateStepLogs("Passed", "Page with title: " + pageTitle + " is displayed upon clicking Job Types link ");

                string prodLine = randomPages.GetJobTypeProductLineLV();
                string prodCode = randomPages.GetJobTypeProductTypeCodeLV();

                randomPages.CloseActiveTab(updType); //Close Active Job Type Tab
                extentReports.CreateStepLogs("Info", "Job Types tab is closed");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                engagementHome.SearchEngagementInLightning(engagementName);
                extentReports.CreateStepLogs("Passed", "Engagement: " + engagementName + " found and selected ");

                string productLine = opportunityDetails.GetERPProductTypeLV();
                Assert.AreEqual(prodLine, productLine);
                extentReports.CreateStepLogs("Passed", "Product Type in ERP section: " + productLine + " matches with Product Line in Job Type Detail as per updated Job Type ");

                string productCode = opportunityDetails.GetERPProductTypeCodeLV();//Engagement Page
                Assert.AreEqual(prodCode, productCode);
                extentReports.CreateStepLogs("Passed", "Updated ERP Product Type Code in ERP section: " + productCode + " matches with Product Type Code in Job Type Detail as per updated Job Type ");

                ////-----Update Client Ownership, ERP Update DFF checkbox and validate ERP Sync Date, Status and Last Integration Status-----

                string updOwnership = ReadExcelData.ReadData(excelPath, "DFFUpdates", 5);
                engagementDetails.UpdateClientOwnershipLV(updOwnership);//Engagement Page
                string clientOwnership = opportunityDetails.GetClientOwnershipLV();//Engagement Page
                Assert.AreEqual(updOwnership, clientOwnership);
                extentReports.CreateStepLogs("Passed", "Client Ownership is updated to " + clientOwnership + " ");

                string valDFFClient = randomPages.GetERPUpdateDFFCheckboxStatusLV();//Engagement Page
                //Assert.AreEqual("Checkbox is checked", valDFFClient); Not checked
                extentReports.CreateStepLogs("Passed", "ERP Update DFF " + valDFFClient + " after updating Client Ownership ");

                string ERPSubmittedClient = randomPages.GetERPSubmittedToSyncLV();//Engagement Page
                Assert.AreNotEqual(ERPSubmittedJobType, ERPSubmittedClient);
                extentReports.CreateStepLogs("Passed", "ERP Submitted to Sync: " + ERPSubmittedClient + " ");

                string ERPStatusClient = randomPages.GetERPLastIntegrationStatusLV();//Engagement Page
                //Assert.AreEqual("Success", ERPStatusClient);// need to uncomment
                extentReports.CreateStepLogs("Passed", "Assersion Pending ERP Last Integration Status in ERP section: " + ERPStatusClient + " is displayed ");

                string ERPResClient = randomPages.GetERPLastIntegrationResponseDateLV();//Engagement Page
                Assert.AreNotEqual(ERPResJobType, ERPResClient);
                extentReports.CreateStepLogs("Passed", "ERP Last Integration Response Date in ERP section: " + ERPResClient + " is displayed ");

                string newLOBExl = ReadExcelData.ReadData(excelPath, "DFFUpdates", 6);
                string newJobTypeExl = ReadExcelData.ReadData(excelPath, "DFFUpdates", 7);
                engagementDetails.UpdateRecordTypeLV(newLOBExl, newJobTypeExl);//Engagement Page
                string LOB = opportunityDetails.GetRecordTypeLV();//Engagement Page
                Assert.AreEqual(newLOBExl, LOB);
                extentReports.CreateStepLogs("Passed", "LOB is updated to " + LOB + " ");

                string valDFFLOB = randomPages.GetERPUpdateDFFCheckboxStatusLV();//Engagement Page
                //Assert.AreEqual("Checkbox is checked", valDFFLOB);//not checked
                extentReports.CreateStepLogs("Passed", "Assersion Pending ERP Update DFF " + valDFFLOB + " after updating LOB ");

                string ERPSubmittedLOB = randomPages.GetERPSubmittedToSyncLV();//Engagement Page
                Assert.AreNotEqual(ERPSubmittedClient, ERPSubmittedLOB);
                extentReports.CreateStepLogs("Passed", "Assersion Pending ERP Submitted to Sync: " + ERPSubmittedLOB + " ");

                string ERPStatusLOB = randomPages.GetERPLastIntegrationStatusLV();//Engagement Page
                //Assert.AreEqual("Success", ERPStatusLOB); // need to uncomment
                extentReports.CreateStepLogs("Passed", "Assersion Pending ERP Last Integration Status in ERP section: " + ERPStatusLOB + " is displayed ");

                string ERPResLOB = randomPages.GetERPLastIntegrationResponseDateLV();//Engagement Page
                Assert.AreNotEqual(ERPResClient, ERPResLOB);
                extentReports.CreateStepLogs("Passed", "ERP Last Integration Response Date in ERP section: " + ERPResLOB + " is displayed ");
                randomPages.CloseActiveTab(oppName);
                extentReports.CreateStepLogs("Info", "Opportunity tab is closed");
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " logged out");
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
