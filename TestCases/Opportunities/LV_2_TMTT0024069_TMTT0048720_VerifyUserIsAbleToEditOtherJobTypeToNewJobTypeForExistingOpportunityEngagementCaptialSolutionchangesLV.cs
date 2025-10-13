using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using NUnit.Framework;
using SF_Automation.TestData;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_2_TMTT0024069_TMTT0048720_VerifyUserIsAbleToEditOtherJobTypeToNewJobTypeForExistingOpportunityEngagementCaptialSolutionchangesLV : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTMTI0055389 = "TMTI0055389_EditExistingOppEngToNewCFJobType";
        private string oldJobType; 
        private string newJobType;
        private string updatedJobType;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0056861 Verify that NBC form is not required for new Job type- Lender education
        //TMTI0055386 Verify the availability of new Job Types on Edit Engagement page
        //TMTI0120011 Verify that the new Job types are available in the Job type picklist in existing Opportunities. 
        //TMTI0120025 Verify that the Job type value is updated for all the existing opportunities

        [Test]
        public void EditCFOpportunityEngagementWithNewJobTypeLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0055389;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");                
                
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);                    
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
                extentReports.CreateStepLogs("Info", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                //Validating Title of New Opportunity Page
                string pageTitle = opportunityHome.ClickNewButtonAndSelectCFOpp();
                Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                extentReports.CreateLog(driver.Title + " is displayed ");
                string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTMTI0055389);//updated move to jobtype
                extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTI0055389);
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
                addOpportunityContact.CreateContactL2(fileTMTI0055389, valRecordType);
                extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                //Update required Opportunity fields for conversion and Internal team details
                opportunityDetails.UpdateReqFieldsForCFConversionLV2(fileTMTI0055389, valJobType);//udated Move to element
                extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTI0055389);
                extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                opportunityDetails.ClickReturnToOpportunityLV();
                //randompages.CloseActiveTab("Internal Team");
                extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                //TMTI0055389	Verify user is able to edit any other Job types to new job type for existing opportunity
                //TMTI0120011	Verify that the new Job types are available in the Job type picklist in existing Opportunities. 
                //TMTI0120025 Verify that the Job type value is updated for all the existing opportunities
                //Get Existing JobType
                int rowOpp = ReadExcelData.GetRowCount(excelPath, "NewOppJobTypes");
                for (int row = 2; row <= rowOpp; row++)
                {
                    newJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "NewOppJobTypes", row, 1);
                    oldJobType = opportunityDetails.GetDetailPageJobTypeLV();
                    //Get Existing JobType with New JobType
                    opportunityDetails.UpdateJobTypeLV(oldJobType, newJobType);
                    updatedJobType = opportunityDetails.GetDetailPageJobTypeLV();
                    Assert.AreEqual(newJobType, updatedJobType);
                    extentReports.CreateStepLogs("Passed", "New Job Type: "+ newJobType+" is available and updated/saved from Existing Job Type: " + oldJobType +" Opportunity Detail page");

                    //Reverting Job Type to Actual Job Type
                    opportunityDetails.UpdateJobTypeLV(newJobType, oldJobType);
                    extentReports.CreateStepLogs("Passed", "Job Type Reverted back to Existing Job Type: " + oldJobType + " Opportunity Detail page ");

                }
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", valUser + " User logged out ");


                extentReports.CreateLog("Admin is Performing Required Actions ");
                //System Admin Performin required actions
                string adminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "CAOUsers", 3, 1);
                homePage.SearchUserByGlobalSearchN(adminUser);
                extentReports.CreateStepLogs("Info", "Admin User: " + adminUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                extentReports.CreateStepLogs("Info", "Admin User: " + adminUser + " logged in on Lightning View");
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

                //Login again as Standard User
                //usersLogin.SearchUserAndLogin(valUser);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valUser), true);

                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                //Search for created opportunity
                opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                opportunityDetails.ClickRequestToEngL();

                //Submit Request To Engagement Conversion 
                string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");

                //Log out of Standard User
                homePageLV.LogoutFromSFLightningAsApprover();

                //Login as CAO user to approve the Opportunity
                string userCAOExl = ReadExcelData.ReadData(excelPath, "CAOUsers", 1);
                //usersLogin.SearchUserAndLogin(userCAOExl);
                homePage.SearchUserByGlobalSearchN(userCAOExl);
                extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();

                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(userCAOExl), true);

                //homePageLV.ClickAppLauncher();
                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Info", appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                //Search for created opportunity
                opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                //Approve the Opportunity 
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


                //***************Need to recheck and rework All Job Types are not preasent on eng job type picklist ********************
                //TMTI0055386 Verify the availability of new Job Types on Edit Engagement page
                int rowEng = ReadExcelData.GetRowCount(excelPath, "Engagement");
                for (int row = 2; row <= rowEng; row++)
                {
                    newJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 1);
                    oldJobType = engagementDetails.GetDetailPageJobTypeLV();
                    //Get Existing JobType with New JobType
                    engagementDetails.UpdateJobTypeLV(oldJobType, newJobType);
                    updatedJobType = engagementDetails.GetDetailPageJobTypeLV();
                    Assert.AreEqual(newJobType, updatedJobType);
                    extentReports.CreateStepLogs("Passed", "New Job Type: " + newJobType + " is available and updated/saved from Existing Job Type: " + oldJobType + " Engagement Detail page");
                    //Reverting Job Type to Actual Job Type
                    engagementDetails.UpdateJobTypeLV(newJobType, oldJobType);
                    extentReports.CreateStepLogs("Passed", "Job Type Reverted back to Existing Job Type: " + oldJobType + " Engagement Detail page ");
                }
                //***************Need to recheck and rework********************


                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " logged out");                
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                homePageLV.LogoutFromSFLightningAsApprover();
                driver.Quit();
            }
        }
    }
}
