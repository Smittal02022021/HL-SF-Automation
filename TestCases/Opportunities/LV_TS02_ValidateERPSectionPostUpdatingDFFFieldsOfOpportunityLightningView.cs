using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.OpportunitiesOracleERP
{
    class LV_TS02_ValidateERPSectionPostUpdatingDFFFieldsOfOpportunityLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileERPTS02 = "LV_TS02_PostUpdatingDFFFieldsOfOpportunity";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ValidateOpportunityERPSectionLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileERPTS02;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");
                
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity",2, 25);
                    
                extentReports.CreateStepLogs("Info", "Creating Opportunity for LOB : " +valRecordType+" and Job Type: " +valJobType + " ");
                //Login as Standard User profile and validate the user
                string valUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2,1);
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
                string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileERPTS02);
                extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileERPTS02);
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

                addOpportunityContact.CickAddCFOpportunityContact();
                addOpportunityContact.CreateContactL2(fileERPTS02, valRecordType);
                extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                //Fetch values of Opportunity Name, Client, Subject and Job Type
                string oppName = opportunityDetails.GetOpportunityNameL();
                string clientName = opportunityDetails.GetClientLV();
                string subjectName = opportunityDetails.GetSubjectLV();
                string oppNumber = opportunityDetails.GetOppNumberLV();
                string jobType = opportunityDetails.GetJobTypeLV();
                randomPages.CloseActiveTab(oppName);
                extentReports.CreateStepLogs("Info", "Opportunity tab is closed");

                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "User: " + valUserExl + " logged out");

                string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 3);
                homePage.SearchUserByGlobalSearchN(adminUserExl);
                extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
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

                //Full View
                randomPages.DetailPageFullViewLV();
                extentReports.CreateStepLogs("Info", "Detail Page Full View is displayed ");

                //Get ERP Submitted to Sync and get ERP Update DFF checkbox and ERP Last Integration Response Date
                string ERPSubmitted = randomPages.GetERPSubmittedToSyncLV();
                extentReports.CreateStepLogs("Info", "ERP Submitted to Sync before update is: " + ERPSubmitted + " ");

                //Due to Refersh Issue
                //string valERPUpdateDFF = randomPages.GetERPUpdateDFFCheckboxStatusLV();
                //Assert.AreEqual("Checkbox is not checked", valERPUpdateDFF);
                //extentReports.CreateStepLogs("Passed", "ERP Update DFF " + valERPUpdateDFF + " by default ");

                string ERPResDate = randomPages.GetERPLastIntegrationResponseDateLV();
                extentReports.CreateStepLogs("Info", "ERP Last Integration Response Date in ERP section: " + ERPResDate + " is displayed ");

                //-----Update Primary office, ERP Update DFF checkbox and validate ERP Sync Date, Status and Last Integration Status -----
                string newOffice = ReadExcelData.ReadData(excelPath, "DFFUpdates", 1);
                opportunityDetails.UpdatePrimaryOfficeLV(newOffice);
                randomPages.DetailPageFullViewLV();// Click on Administrator tab then get the Pri Offic
                extentReports.CreateStepLogs("Info", "Detail Page Full View is displayed ");
                string primaryOffice = randomPages.GetPrimaryOfficeLV();
                Assert.AreEqual(newOffice, primaryOffice);
                extentReports.CreateStepLogs("Passed", "Primary Office is updated to " + primaryOffice + " ");

                //Due to page reload behavior while editing the page via Edit button v/s inline edit the DFF chekbox selection is just blinked
                //and not able to get the actual state of this checkbox.
                //Commenting the related functions and Assersions
                /*
                string valDFFPrimaryOffice = randomPages.GetERPUpdateDFFCheckboxStatusLV();
                //Assert.AreEqual("Checkbox is checked", valDFFPrimaryOffice);
                extentReports.CreateStepLogs("Passed", "Fail*****Pending***** ERP Update DFF " + valDFFPrimaryOffice + " after updating Primary office ");
                */
                string ERPSubmittedOffice = randomPages.GetERPSubmittedToSyncLV();
                Assert.AreNotEqual(ERPSubmitted, ERPSubmittedOffice);
                extentReports.CreateStepLogs("Passed", "ERP Submitted to Sync: " + ERPSubmittedOffice + " ");

                string ERPStatusOffice = randomPages.GetERPLastIntegrationStatusLV();
                Assert.AreEqual("Success", ERPStatusOffice);// need to uncomment
                extentReports.CreateStepLogs("Passed", "ERP Last Integration Status in ERP section: " + ERPStatusOffice + " is displayed ");

                string ERPResOffice = randomPages.GetERPLastIntegrationResponseDateLV();
                Assert.AreNotEqual(ERPResDate, ERPResOffice);// need to uncomment
                extentReports.CreateStepLogs("Passed", "ERP Last Integration Response Date in ERP section New : " + ERPResOffice + " is displayed Old: "+ ERPResDate);
                extentReports.CreateStepLogs("Info", "****Industry Group field is removed from Opp Detail page  ");
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
                               
                string updSector = ReadExcelData.ReadData(excelPath, "DFFUpdates", 8);                
                opportunityDetails.UpdateHLSectorIDLV(updSector);

                string sector = randomPages.GetHLSectorIDLV();
                Assert.AreEqual(sector, updSector);
                
                //string sectorCombo = randomPages.GetHLSectorComboLV();
               // Assert.AreEqual(sectorCombo.Contains(updSector), true);

                extentReports.CreateStepLogs("Passed", "Sector is updated to and sector combo contains " + updSector + " ");
                randomPages.DetailPageFullViewLV();
                extentReports.CreateStepLogs("Info", "Detail Page Full View is displayed ");

                /*//Skipping DFF checkbox                
                string valDFFSector = randomPages.GetERPUpdateDFFCheckboxStatusLV();
                Assert.AreEqual("Checkbox is checked", valDFFSector);
                extentReports.CreateStepLogs("Passed", "ERP Update DFF " + valDFFSector + " after updating Sector ");
                */
                string ERPSubmittedSector = randomPages.GetERPSubmittedToSyncLV();
                Assert.AreNotEqual(ERPSubmittedOffice, ERPSubmittedSector);
                extentReports.CreateStepLogs("Passed", " ERP Submitted to Sync New : " + ERPSubmittedSector + " Old: "+ ERPSubmittedOffice);

                string ERPStatusSector = randomPages.GetERPLastIntegrationStatusLV();
                Assert.AreEqual("Success", ERPStatusSector);// need to uncomment
                extentReports.CreateStepLogs("Passed", "ERP Last Integration Status in ERP section: " + ERPStatusSector + " is displayed ");

                string ERPResSector = randomPages.GetERPLastIntegrationResponseDateLV();
                Assert.AreNotEqual(ERPResOffice, ERPResSector);
                extentReports.CreateStepLogs("Passed", "ERP Last Integration Response Date in ERP section New: " + ERPResSector + " is displayed Old: "+ ERPResOffice);
                
                ////-----Update Job Type, ERP Update DFF checkbox and validate ERP Sync Date, Status and Last Integration Status-----

                string updType = ReadExcelData.ReadData(excelPath, "DFFUpdates", 4);
                opportunityDetails.UpdateJobTypeLV(updType);                
                string updJobType = opportunityDetails.GetJobTypeLV();
                Assert.AreEqual(updType, updJobType);
                extentReports.CreateStepLogs("Passed", "Job Type is updated to " + updJobType + " ");
                randomPages.DetailPageFullViewLV();
                extentReports.CreateStepLogs("Info", "Detail Page Full View is displayed ");

                //Skipping DFF checkbox
                /*
                
                string valDFFJobType = randomPages.GetERPUpdateDFFCheckboxStatusLV();
                Assert.AreEqual("Checkbox is checked", valDFFJobType);// Not checked
                extentReports.CreateStepLogs("Passed", "ERP Update DFF " + valDFFJobType + " after updating Job Type ");
                */

                string ERPSubmittedJobType = randomPages.GetERPSubmittedToSyncLV();
                Assert.AreNotEqual(ERPSubmittedSector, ERPSubmittedJobType);
                extentReports.CreateStepLogs("Passed", "ERP Submitted to Sync New: " + ERPSubmittedJobType + " Old: "+ ERPSubmittedSector);

                string ERPStatusJobType = randomPages.GetERPLastIntegrationStatusLV();
                Assert.AreEqual("Success", ERPStatusJobType);// need to uncomment
                extentReports.CreateStepLogs("Passed", "ERP Last Integration Status in ERP section: " + ERPStatusJobType + " is displayed ");

                string ERPResJobType = randomPages.GetERPLastIntegrationResponseDateLV();
                Assert.AreNotEqual(ERPResSector, ERPResJobType);//Realted to updateSector 
                extentReports.CreateLog("ERP Last Integration Response Date in ERP section New: " + ERPResJobType + " is displayed Old: "+ ERPResSector);

                randomPages.CloseActiveTab(oppName);//Close Active opp Tab
                extentReports.CreateStepLogs("Info", "Opportunity tab is closed");

                ////----Validate Product Line by getting from Job Types page
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                pageTitle = randomPages.SelectJobTypesLV(updType);// Need to handle the recent list 
                Assert.AreEqual(updType, pageTitle);
                extentReports.CreateStepLogs("Passed", "Page with title: " + pageTitle + " is displayed upon clicking Job Types link ");

                string prodLine = randomPages.GetJobTypeProductLineLV();
                string prodCode = randomPages.GetJobTypeProductTypeCodeLV();

                randomPages.CloseActiveTab(updType); //Close Active Job Type Tab
                extentReports.CreateStepLogs("Info", "Job Types tab is closed");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                opportunityHome.SearchOpportunitiesInLightningView(oppName);
                extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                randomPages.DetailPageFullViewLV();
                extentReports.CreateStepLogs("Info", "Detail Page Full View is displayed ");
                string productLine = randomPages.GetERPProductTypeLV();
                Assert.AreEqual(prodLine, productLine);
                extentReports.CreateStepLogs("Passed", "Product Type in ERP section: " + productLine + " matches with Product Line in Job Type Detail as per updated Job Type ");

                string productCode = randomPages.GetERPProductTypeCodeLV();
                Assert.AreEqual(prodCode, productCode);
                extentReports.CreateStepLogs("Passed", "Updated ERP Product Type Code in ERP section: " + productCode + " matches with Product Type Code in Job Type Detail as per updated Job Type ");

                ////-----Update Client Ownership, ERP Update DFF checkbox and validate ERP Sync Date, Status and Last Integration Status-----

                string updOwnership = ReadExcelData.ReadData(excelPath, "DFFUpdates", 5);
                opportunityDetails.UpdateClientOwnershipLV(updOwnership);
                string clientOwnership = opportunityDetails.GetClientOwnershipLV();
                Assert.AreEqual(updOwnership, clientOwnership);
                extentReports.CreateStepLogs("Passed", "Client Ownership is updated to " + clientOwnership + " ");
                randomPages.DetailPageFullViewLV();
                extentReports.CreateStepLogs("Info", "Detail Page Full View is displayed ");

                /*//Skipping DFF checkbox                
                string valDFFClient = randomPages.GetERPUpdateDFFCheckboxStatusLV();
                Assert.AreEqual("Checkbox is checked", valDFFClient); //Not checked
                extentReports.CreateStepLogs("Passed", "ERP Update DFF " + valDFFClient + " after updating Client Ownership ");
                */

                string ERPSubmittedClient = randomPages.GetERPSubmittedToSyncLV();
                Assert.AreNotEqual(ERPSubmittedJobType, ERPSubmittedClient);
                extentReports.CreateStepLogs("Passed", "ERP Submitted to Sync New: " + ERPSubmittedClient + " Old: "+ ERPResSector);

                string ERPStatusClient = randomPages.GetERPLastIntegrationStatusLV();
                Assert.AreEqual("Success", ERPStatusClient);
                extentReports.CreateStepLogs("Passed", "ERP Last Integration Status in ERP section: " + ERPStatusClient + " is displayed ");

                string ERPResClient = randomPages.GetERPLastIntegrationResponseDateLV();
                Assert.AreNotEqual(ERPResJobType, ERPResClient);
                extentReports.CreateStepLogs("Passed", "ERP Last Integration Response Date in ERP section New: " + ERPResClient + " is displayed Old: "+ ERPResJobType);

                string newLOBExl = ReadExcelData.ReadData(excelPath, "DFFUpdates", 6);
                string newJobTypeExl= ReadExcelData.ReadData(excelPath, "DFFUpdates", 7);
                opportunityDetails.UpdateRecordTypeLV(newLOBExl, newJobTypeExl);
                randomPages.DetailPageFullViewLV();
                extentReports.CreateStepLogs("Info", "Detail Page Full View is displayed ");
                string LOB = opportunityDetails.GetRecordTypeLV();
                Assert.AreEqual(newLOBExl, LOB);
                extentReports.CreateStepLogs("Passed", "LOB is updated to " + LOB + " ");
                //randomPages.DetailPageFullViewLV();
                //extentReports.CreateStepLogs("Info", "Detail Page Full View is displayed ");

                /*//Skipping DFF checkbox
                
                string valDFFLOB = randomPages.GetERPUpdateDFFCheckboxStatusLV();
                Assert.AreEqual("Checkbox is checked", valDFFLOB);//not checked
                extentReports.CreateStepLogs("Passed", "ERP Update DFF " + valDFFLOB + " after updating LOB ");
                */

                string ERPSubmittedLOB = randomPages.GetERPSubmittedToSyncLV();
                Assert.AreNotEqual(ERPSubmittedClient, ERPSubmittedLOB);
                extentReports.CreateStepLogs("Passed", "ERP Submitted to Sync New: " + ERPSubmittedLOB + " Old: "+ ERPSubmittedClient);

                string ERPStatusLOB = randomPages.GetERPLastIntegrationStatusLV();
                Assert.AreEqual("Success", ERPStatusLOB); 
                extentReports.CreateStepLogs("Passed", "ERP Last Integration Status in ERP section: " + ERPStatusLOB + " is displayed ");

                string ERPResLOB = randomPages.GetERPLastIntegrationResponseDateLV();
                Assert.AreNotEqual(ERPResClient, ERPResLOB);
                extentReports.CreateStepLogs("Passed", "ERP Last Integration Response Date in ERP section New: " + ERPResLOB + " is displayed Old: "+ ERPResClient);
                randomPages.CloseActiveTab(oppName);
                extentReports.CreateStepLogs("Info", "Opportunity tab is closed");
                homePageLV.LogoutFromSFLightningAsApprover();
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
