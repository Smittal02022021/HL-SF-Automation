using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_3_TMTT0047923_TMTT0047926_TMTT0047929_VerifyUserIsAbleTo_EditUpdateOtherJobTypeToNewJobTypeForExistingOpportunity:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();
        public static string fileT47926 = "LV_3_TMTT0047923_TMTT0047926_TMTT0047929_VerifyUserIsAbleTo_EditUpdateOtherJobTypeToNewJobTypeForExistingOpportunity";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        //TMTI0117822	Verify that the user is able to edit/update other Job type to "DRC - Dispute" for an existing opportunity.
        //TMTI0117837	Verify that the user is able to edit/update other Job type to "DRC - ESOP" for an existing opportunity.
        //TMTI0117862	Verify that the user is able to edit/update other Job type to "DRC - Estate & Gift" for an existing opportunity.

        [Test]
        public void VerifyUserIsAbleTo_EditUpdateOtherJobTypeToNewJobTypeForExistingOpportunityLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT47926;
                extentReports.CreateStepLogs("Info", "Verify Functionality of Opportunity to Engagement conversion for LOB:FVA On LightningView");
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");                
                
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 25);

                extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType + " ");
                //Login as Standard User profile and validate the user
                string stdUserExl = ReadExcelData.ReadData(excelPath, "StandardUser", 1);
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
                extentReports.CreateStepLogs("Passed", "Creating Opportunity for Job Type: " + valJobType);
                string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileT47926);
                extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created with Job Type: " + valJobType);
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileT47926);
                extentReports.CreateLog("Return to Opportunity Detail page ");
                extentReports.CreateStepLogs("Passed", "User is on Opportunity detail " + displayedTab + " tab after addin Internal deal team members");

                //Validating Opportunity details  
                string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                extentReports.CreateStepLogs("Passed", "Opportunity with number : " + opportunityNumber + " is created with Job Type: " + valJobType);

                int rowNewJobType = ReadExcelData.GetRowCount(excelPath, "UpdateJobType");                
                for (int row = 2; row <= rowNewJobType; row++)
                {
                    //Get Existing Job Type from Opportunity Detail page
                    string existingJobType = opportunityDetails.GetDetailPageJobTypeLV();
                    //Get Existing JobType with New JobType
                    string newJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateJobType", row, 1);
                    opportunityDetails.UpdateJobTypeLV(existingJobType, newJobType);
                    string updatedJobType = opportunityDetails.GetDetailPageJobTypeLV();
                    Assert.AreEqual(newJobType, updatedJobType);
                    extentReports.CreateLog("Job Type is updated from Existing Job Type: " + existingJobType + " to New Job Type: " + updatedJobType + " Opportunity Detail page ");

                    //Reverting Job Type to Actual Job Type
                    opportunityDetails.UpdateJobTypeLV(newJobType, existingJobType);
                    extentReports.CreateLog("Job Type is Reverted to to old Job Type: " + existingJobType + " to check other scenarios");

                }
                randomPages.CloseActiveTab(opportunityName);

                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + stdUserExl + " Loggout ");
                extentReports.CreateStepLogs("Info", "Browser Closed SuccessFully");
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