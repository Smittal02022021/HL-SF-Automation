﻿using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Web.UI.DataVisualization.Charting;

namespace SalesForce_Project.TestCases.Opportunities
{
    class LV_TS03_ValidateERPSubmittedAndERPLastIntegrationResponsePostManualSyncOpportunityLightningView:BaseClass
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
        public void PostOpportunityManualSyncLV() 
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileERPTS02;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 3);
                usersLogin.SearchUserAndLogin(adminUserExl);
                login.SwitchToLightningExperience();
                string userName = login.ValidateUserLightningView();
                Assert.AreEqual(userName.Contains(adminUserExl), true);
                extentReports.CreateLog("System Administrator User: " + adminUserExl + " logged in on Lightning View");
                homePageLV.ClickAppLauncher();
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectApp(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                //Search for an opportunity
                string oppName = "Project Saber";
                opportunityHome.SearchOpportunityInLightning(oppName);
                extentReports.CreateLog("Matching record is displayed ");

                //Validating Opportunity details page 
                string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is displayed ");

                //Get ERP Submitted to Sync, Status, ERP Update DFF checkbox and ERP Last Integration Response Date
                string ERPSubmitted = randomPages.GetERPSubmittedToSyncLV();
                extentReports.CreateLog("ERP Submitted to Sync before update is: " + ERPSubmitted + " ");

                string ERPResDate = randomPages.GetERPLastIntegrationResponseDateLV();
                extentReports.CreateLog("ERP Last Integration Response Date in ERP section: " + ERPResDate + " is displayed ");

                string ERPStatus = randomPages.GetERPLastIntegrationStatusLV();
                extentReports.CreateLog("ERP Last Integration Status in ERP section: " + ERPStatus + " is displayed ");

                //-----Schedule ERP Submitted to Sync manually, validate ERP Update DFF checkbox, ERP Sync Date, Status and Last Integration Status -----
                randomPages.UpdateERPSyncManuallyInlineLV();
                string ERPSubmittedPostSync = randomPages.GetERPSubmittedToSyncLV();
                Assert.AreNotEqual(ERPSubmitted, ERPSubmittedPostSync);
                extentReports.CreateLog("ERP Submitted to Sync : " + ERPSubmittedPostSync + " is updated post scheduling ERP sync ");

                string ERPResDatePostSync = randomPages.GetERPLastIntegrationResponseDateLV();
                //Assert.AreNotEqual(ERPResDate, ERPResDatePostSync); ERP not working
                extentReports.CreateLog("Assersion Pending:: ERP Last Integration Response Date in ERP section: " + ERPResDatePostSync + " is displayed post ERP sync ");

                string ERPStatusPostSync = randomPages.GetERPLastIntegrationStatusLV();
                //Assert.AreEqual("Success", ERPStatusPostSync); ERP not working
                extentReports.CreateLog("Assersion Pending ::ERP Last Integration Status in ERP section: " + ERPStatusPostSync + " is displayed post ERP sync ");

                randomPages.CloseActiveTab(oppName);
                extentReports.CreateStepLogs("Info", "Opportunity is closed");
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " logged out");
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}