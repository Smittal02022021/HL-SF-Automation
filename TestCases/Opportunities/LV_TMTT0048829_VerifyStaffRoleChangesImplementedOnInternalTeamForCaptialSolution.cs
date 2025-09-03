using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunities
{
    class LV_TMTT0048829_VerifyStaffRoleChangesImplementedOnInternalTeamForCaptialSolution:BaseClass
    {

        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();
        public static string fileTMTT0048829 = "LV_TMTT0048829_VerifyStaffRoleChangesImplementedOnInternalTeamForCaptialSolution";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTheRolesAvailableLightningView()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0048829;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User profile and validate the user
                string userExl = ReadExcelData.ReadData(excelPath, "UsersFinancial", 1);

                homePage.SearchUserByGlobalSearchN(userExl);
                extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(userExl), true);
                extentReports.CreateLog("User: " + userExl + " Switched to Lightning View ");
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Pass", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                int teamMember = ReadExcelData.GetRowCount(excelPath, "UsersFinancial");
                for (int row = 2; row <= teamMember; row++)
                {
                    string teamMemberName = ReadExcelData.ReadDataMultipleRows(excelPath, "UsersFinancial", row, 2);
                    string opportunityName = ReadExcelData.ReadDataMultipleRows(excelPath, "UsersFinancial", row, 3);
                    string OppLocation = ReadExcelData.ReadDataMultipleRows(excelPath, "UsersFinancial", row, 6);
                    string contactLocation = ReadExcelData.ReadDataMultipleRows(excelPath, "UsersFinancial", row, 7);
                    string contactStatus = ReadExcelData.ReadDataMultipleRows(excelPath, "UsersFinancial", row, 8);
                    string opportunityRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "UsersFinancial", row, 9);
                    extentReports.CreateStepLogs("Info", "Verify The Available Roles  On " + OppLocation + " Opportunity " + opportunityRecordType + " for Team member: " + teamMemberName + " of " + contactLocation + " as " + contactStatus + " contact");

                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Info", "User is on " + opportunityName + " Detail Page ");

                    extentReports.CreateStepLogs("Info", "Validating Roles for " + teamMemberName);
                    string chkInitiator = opportunityDetails.ModifyInternalTeamMembersLV(teamMemberName);

                    //------Validate all the roles checkbox
                    //Verify Initiator role
                    Assert.AreEqual("True", chkInitiator);
                    extentReports.CreateStepLogs("Pass", "Initiator role checkbox is displayed ");

                    //Verify Seller role                    
                    Assert.IsTrue(opportunityDetails.IsSellerRoleCheckboxDisplayedLV());
                    extentReports.CreateStepLogs("Pass", "Seller role checkbox is displayed ");

                    //Verify Principal role                    
                    Assert.IsTrue(opportunityDetails.IsPrincipalRoleDisplayedLV());
                    extentReports.CreateStepLogs("Pass", "Principal role checkbox is not displayed ");

                    //Verify Manager role
                    Assert.IsTrue(opportunityDetails.IsManagerRoleDisplayedLV());
                    extentReports.CreateStepLogs("Pass", "Manager role checkbox is not displayed ");

                    //Verify Associate role                    
                    Assert.IsTrue(opportunityDetails.IsAssociateRoleDisplayedLV());
                    extentReports.CreateStepLogs("Pass", "Associate role checkbox is not displayed ");

                    //Verify Analyst role
                    Assert.IsTrue(opportunityDetails.IsAnalystRoleDisplayedLV());
                    extentReports.CreateStepLogs("Pass", "Analyst role checkbox is not displayed ");

                    //Verify Specialty role 
                    Assert.IsTrue(opportunityDetails.IsSpecialtyRoleDisplayedLV());
                    extentReports.CreateStepLogs("Pass", "Specialty role checkbox is displayed ");

                    //Verify PE/HF role 
                    Assert.IsTrue(opportunityDetails.IsPERoleDisplayedLV());
                    extentReports.CreateStepLogs("Pass", "PE role checkbox is not displayed ");

                    //Verify Public role 
                    Assert.IsTrue(opportunityDetails.IsPublicRoleDisplayedLV());
                    extentReports.CreateStepLogs("Pass", "Public role checkbox is not displayed ");

                    //Verify Admin role
                    Assert.IsFalse(opportunityDetails.IsAdminRoleDisplayedLV());
                    extentReports.CreateStepLogs("Pass", "Admin role checkbox is not displayed ");

                    //Verify RMS role
                    Assert.IsTrue(opportunityDetails.IsRMSRoleDisplayedLV());
                    extentReports.CreateStepLogs("Pass", "RMS role checkbox is not displayed ");

                    //Verify Expense role
                    Assert.IsTrue(opportunityDetails.IsExpenseOnlyRoleDisplayedLV());
                    extentReports.CreateStepLogs("Pass", "Expense role checkbox is not displayed ");

                    //Verify Non Registered role                   

                    if (contactStatus == "Registered")
                    {
                        Assert.IsFalse(opportunityDetails.IsNonRegisteredRoleDisplayedLV());
                        extentReports.CreateStepLogs("Pass", "Registered role checkbox is not displayed ");
                    }

                    if (contactStatus == "Non-Registered")
                    {
                        Assert.IsTrue(opportunityDetails.IsNonRegisteredRoleDisplayedLV());
                        extentReports.CreateStepLogs("Pass", "Registered role checkbox is not displayed ");

                    }

                    opportunityDetails.ClickReturnToOpportunityL();// switched to DefaultView
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab("Internal Team");
                    randomPages.CloseActiveTab(opportunityName);            
                }
                homePageLV.UserLogoutFromSFLightningView();
                driver.Quit();
                extentReports.CreateStepLogs("Pass", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }

    }
}
