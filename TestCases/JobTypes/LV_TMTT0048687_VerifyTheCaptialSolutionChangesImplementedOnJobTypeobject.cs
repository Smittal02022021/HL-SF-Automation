using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;
using SalesForce_Project.Pages.JobTypes;

namespace SalesForce_Project.TestCases.JobTypes
{
    class LV_TMTT0048687_VerifyTheCaptialSolutionChangesImplementedOnJobTypeobject:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();
        JobTypesPage jobTypesPage = new JobTypesPage();

        public static string fileTMTT0024858 = "LV_TMTT0048687_VerifyTheCaptialSolutionChangesImplementedOnJobTypeobject";
        private string jobTypeNameExl;
        private int rowJobType;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0119936	Verify that the new Job types are added to the Job types object.
        //TMTI0119938 Verify that the mentioned existing Job types are displayed Inactive in the Job types object.
        //TMTI0119940 Verify that the new CS job type has the new values created for the mentioned fields.
        //TMTI0119942 Verify that the "Lender Education", "Liability Management" and "Buyside & Financing Advisory" Job types product classifications is changed to "CS".
        //TMTI0119944 Verify that the mentioned fields are derived from existing CM/PFG jobs for the new job types, consistent with mapped job types.
        //TMTI0122901 Verify that "Debt Financing", "Buyside & Financing Advisory", "Equity Placements" and "Financial Asset Sale" has the Closed Deal Memo enforced in Job Type Required Items. 


        [Test]
        public void VerifyTheCaptialSolutionChangesImplementedOnJobTypeobjectLV()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0024858;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + login.ValidateUser() + " is able to login ");

                string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(userExl);
                extentReports.CreateStepLogs("Info", "CF Financial User: " + userExl + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string userName = login.ValidateUserLightningView();
                Assert.AreEqual(userName.Contains(userExl), true);
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + userExl + " logged in on Lightning View");
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadData(excelPath, "ModuleName", 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on Module: " + moduleNameExl + " Page ");

                randomPages.SelectListViewLV("All");
                extentReports.CreateStepLogs("Info", " All List option is selected ");
                //Calling functions to validate for all LOBs operation
                rowJobType = ReadExcelData.GetRowCount(excelPath, "JobType");
                for (int row = 2; row <= rowJobType; row++)
                {
                    //TMTI0119936	Verify that the new Job types are added to the Job types object.

                    jobTypeNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 1);
                    jobTypesPage.SearchJobtypeLV(jobTypeNameExl);
                    Assert.IsTrue(jobTypesPage.IsJobTypeDisplayedLV(jobTypeNameExl), "Verify that the new Job types: " + jobTypeNameExl + " is added to the Job types object");
                    extentReports.CreateStepLogs("Passed", "<b>Job Type: <b> " + jobTypeNameExl + " is available in Job Type List");

                    //TMTI0119940	Verify that the new CS job type has the new values created for the mentioned fields .

                    jobTypesPage.SelectJobTypeLV(jobTypeNameExl);
                    Assert.IsTrue(jobTypesPage.IsPageHeaderDisplayedLV(jobTypeNameExl), "Verify User Redirected to Job Type Details Page");
                    extentReports.CreateStepLogs("Passed", "User Redirected to Job Type: " + jobTypeNameExl + " Details Page");
                    Assert.AreEqual(jobTypeNameExl, jobTypesPage.GetJobTypeNameLV(), "Verify the Job Type Name on Job Type detail page");
                    string jobCodeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 2);
                    Assert.AreEqual(jobCodeExl, jobTypesPage.GetJobCodeLV(), "Verify the Job Code on Job Type detail page");

                    //TMTI0119942	Verify that the "Lender Education", "Liability Management" and "Buyside & Financing Advisory" Job types product classifications is changed to "CS".

                    string jobProductTypeCodeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 3);
                    Assert.AreEqual(jobProductTypeCodeExl, jobTypesPage.GetProductTypeCodeLV(), "Verify the Product Type Code on Job Type detail page");

                    string jobProductTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 4);
                    Assert.AreEqual(jobProductTypeExl, jobTypesPage.GetProductTypeLV(), "Verify the Product Type on Job Type detail page");

                    string jobProductLineReportingExl = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 5);
                    Assert.AreEqual(jobProductLineReportingExl, jobTypesPage.GetProductLineReportingLV(), "Verify the Product Line Reporting on Job Type detail page");

                    string jobProductLineExl = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 6);
                    Assert.AreEqual(jobProductLineExl, jobTypesPage.GetProductLineLV(), "Verify the Product Line on Job Type detail page");

                    string jobProductTypeReportingExl = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 7);
                    Assert.AreEqual(jobProductTypeReportingExl, jobTypesPage.GetProductTypeReportingLV(), "Verify the Product Type Reporting on Job Type detail page");

                   
                    
                    //TMTI0119944	Verify that the mentioned fields are derived from existing CM/PFG jobs for the new job types, consistent with mapped job types.

                    string jobPrimaryLOBExl = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 9);
                    Assert.AreEqual(jobPrimaryLOBExl, jobTypesPage.GetPrimaryLineBusinessLV(), "Verify the Primary Line of Business on Job Type detail page");                   

                    string jobReqCapSourceExl = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 11);
                    Assert.AreEqual(jobReqCapSourceExl, jobTypesPage.GetRequireCapsourceLV(), "Verify the Require Capsource on Job Type detail page");

                    string jobReqMAClosedExl = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 12);
                    Assert.AreEqual(jobReqMAClosedExl, jobTypesPage.GetRequireMAClosedWithLV(), "Verify the Require MA Closed With on Job Type detail page");

                    //TMTI0122901 Verify that "Debt Financing", "Buyside & Financing Advisory", "Equity Placements" and "Financial Asset Sale" has the Closed Deal Memo enforced in Job Type Required Items.
                    string jobTypeRequiredItemExl = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", 1, 13);
                    string isPresntExl = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 13);

                    Assert.AreEqual(isPresntExl, jobTypesPage.IsRequiredItemDisplayedLV(jobTypeRequiredItemExl), "Verify that " + jobTypeNameExl + " the 'Closed Deal Memo' enforced in Job Type Required Items.");
                    extentReports.CreateStepLogs("Passed", "Job Type: " + jobTypeNameExl + " 'Closed Deal Memo' enforced in Job Type Required Items is : " + isPresntExl);

                    //Click on Code Information tab
                    jobTypesPage.ClickTabCodeInformationLV();
                    string jobEngagementDetailtStageExl = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 8);
                    Assert.AreEqual(jobEngagementDetailtStageExl, jobTypesPage.GetEngagementDetailtStageLV(), "Verify the Engagement Detailt Stage on Job Type detail page");

                    string jobEngRTExl = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 10);
                    Assert.AreEqual(jobEngRTExl, jobTypesPage.GetEngagementRecordTypeLV(), "Verify the Engagement Record Type on Job Type detail page");

                    extentReports.CreateStepLogs("Passed", "Job Type: " + jobTypeNameExl + " Available with Product Type Code: " + jobCodeExl + ", Product Type Code: " + jobProductTypeCodeExl + ", Product Type: " + jobProductTypeExl + ", Product Line Reporting: " + jobProductLineReportingExl + ", Product Line: " + jobProductLineExl + ", Product Type Reporting" + jobProductTypeReportingExl + ", Engagement Detailt Stage: " + jobEngagementDetailtStageExl);
                    extentReports.CreateStepLogs("Passed", "Job Type: " + jobTypeNameExl + " derived from existing CM/PFG jobs with following fields Primary Line of Business: " + jobPrimaryLOBExl + ", Engagement Record type: " + jobEngRTExl + ", Primary Line of Business: " + jobPrimaryLOBExl + ", Require MA ClosedWith: " + jobReqMAClosedExl + ", Require Capsource: " + jobReqCapSourceExl);

                    

                    randomPages.CloseActiveTab(jobTypeNameExl);
                }
                //TMTI0119938	Verify that the mentioned existing Job types are displayed Inactive in the Job types object.
                rowJobType = ReadExcelData.GetRowCount(excelPath, "InActiveJobType");
                for (int rowJob = 2; rowJob <= rowJobType; rowJob++)
                {
                        
                    jobTypeNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "InActiveJobType", rowJob, 1);
                    jobTypesPage.SearchJobtypeLV(jobTypeNameExl);
                    Assert.IsTrue(jobTypesPage.IsJobTypeDisplayedLV(jobTypeNameExl), "Verify that the new Job types: " + jobTypeNameExl + " is added to the Job types object");
                    extentReports.CreateStepLogs("Passed", "<b>Job Type: <b> " + jobTypeNameExl + " is available in Job Type List");

                    jobTypesPage.SelectJobTypeLV(jobTypeNameExl);
                    Assert.IsTrue(jobTypesPage.IsPageHeaderDisplayedLV(jobTypeNameExl), "Verify User Redirected to Job Type Details Page");
                    extentReports.CreateStepLogs("Passed", "User Redirected to Job Type: " + jobTypeNameExl + " Details Page");
                    Assert.AreEqual(jobTypeNameExl, jobTypesPage.GetJobTypeNameLV(), "Verify the Job Type Name on Job Type detail page");

                    string jobTypeStatusExl = ReadExcelData.ReadDataMultipleRows(excelPath, "InActiveJobType", rowJob, 2);
                    string stateJobType = jobTypesPage.IsJobTypeActiveLV();
                    Assert.AreEqual(jobTypeStatusExl, stateJobType);
                    extentReports.CreateStepLogs("Passed", "Job Type: " + jobTypeNameExl + " Is Active Status: " + stateJobType);
                    randomPages.CloseActiveTab(jobTypeNameExl);
                }
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Pass", "CF Financial User: " + userExl + " logged out ");
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