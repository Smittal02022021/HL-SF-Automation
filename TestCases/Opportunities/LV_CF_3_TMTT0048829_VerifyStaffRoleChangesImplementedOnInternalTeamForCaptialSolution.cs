using AventStack.ExtentReports.Gherkin.Model;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Security.Cryptography;

namespace SF_Automation.TestCases.OpportunitiesInternalTeam
{
    class LV_CF_3_TMTT0048829_VerifyStaffRoleChangesImplementedOnInternalTeamForCaptialSolution:BaseClass
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
        //TMTI0120330	Verify the roles available to Registered Non-US Financial Contacts in the US Opportunity deals
        //TMTI0120332 Verify the roles available to Registered Non-US Financial Contacts in the foreign(Non-US) Opportunity deals
        //TMTI0123291 Verify the roles available to Registered US Financial Contacts in the US  Opportunity deals.
        //TMTI0123293 Verify the roles available to Registered US Financial Contacts in the foreign(Non-US) Opportunity deals
        //TMTI0123295 Verify the roles available to Non-Registered and Non-US Financial Contacts in the US  Opportunity deals.
        //TMTI0123297 Verify the roles available to Non-Registered and Non-US Financial Contacts in the Non-US Opportunity deals
        //TMTI0123301 Verify the roles available to Non-Registered US Financial Contacts in the Non-US Opportunity deals
        //TMTI0123303 Verify the roles available to Registered US Financial Contacts in the Non-PFG Non-US Opportunity deals
        //TMTI0123305 Verify the roles available to Registered Non-US Financial Contacts in the Non-PFG Non-US Opportunity deals
        //TMTI0123311 Verify the roles available to Non-Registered Non-US Financial Contacts in the Non-PFG US Opportunity deals
        //TMTI0123313 Verify the roles available to Registered US Non-PFG(Now SS) Contacts in the US PFG Opportunity deals
        //TMTI0123340 Verify the roles available to Registered US Non-PFG Contacts in the Non-US PFG Opportunity deals
        //TMTI0123342 Verify the roles available to Non-Registered Non-US Non-PFG Contacts in the Non-US PFG Opportunity deals
        //TMTI0123344 Verify the roles available to Registered US Non-PFG(CM) Contacts in the any US Opportunity deals
        //TMTI0123349 Verify the roles available to Registered Non-US PFG(CM) Contacts in the any US Opportunity deals
        //TMTI0123351 Verify the roles available to Non-Registered US PFG(CM) Contacts in the any Non-US Opportunity deals
        //TMTI0123353 Verify the roles available to Non-Registered Non-US PFG(CM) Contacts in the Non-US Opportunity deals

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
                string userExl = ReadExcelData.ReadData(excelPath, "UsersSet1", 1);

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
                int teamMember = ReadExcelData.GetRowCount(excelPath, "UsersSet3");
                for (int row = 2; row <= teamMember; row++)
                {
                    string teamMemberName = ReadExcelData.ReadDataMultipleRows(excelPath, "UsersSet3", row, 2);
                    string opportunityName = ReadExcelData.ReadDataMultipleRows(excelPath, "UsersSet3", row, 3);
                    string OppLocation = ReadExcelData.ReadDataMultipleRows(excelPath, "UsersSet3", row, 6);
                    string contactLocation = ReadExcelData.ReadDataMultipleRows(excelPath, "UsersSet3", row, 7);
                    string contactStatus = ReadExcelData.ReadDataMultipleRows(excelPath, "UsersSet3", row, 8);
                    string opportunityRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "UsersSet3", row, 9);
                    extentReports.CreateStepLogs("Info", "Verify The Available Roles  On '" + opportunityName + "' " + OppLocation + " Opportunity " + opportunityRecordType + " for Team member: " + teamMemberName + " of " + contactLocation + " as " + contactStatus + " contact");

                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Info", "User is on " + opportunityName + " Detail Page ");

                    extentReports.CreateStepLogs("Info", "Validating Roles for " + teamMemberName);
                    string chkInitiator = opportunityDetails.ModifyInternalTeamMembersLV(teamMemberName);

                    //------Validate all the roles checkbox
                    //Verify Initiator role
                    Assert.AreEqual("False", chkInitiator, "Verify Initiator role should be False ");
                    extentReports.CreateStepLogs("Pass", "Initiator role checkbox is not displayed ");

                    //Verify Seller role                    
                    Assert.IsFalse(opportunityDetails.IsSellerRoleCheckboxDisplayedLV(), "Verify Seller role should be False ");
                    extentReports.CreateStepLogs("Pass", "Seller role checkbox is not displayed ");

                    //Verify Principal role                    
                    Assert.IsFalse(opportunityDetails.IsPrincipalRoleDisplayedLV(), "Verify Principal role should be False ");
                    extentReports.CreateStepLogs("Pass", "Principal role checkbox is not displayed ");

                    //Verify Manager role
                    Assert.IsFalse(opportunityDetails.IsManagerRoleDisplayedLV(), "Verify Manager role should be False ");
                    extentReports.CreateStepLogs("Pass", "Manager role checkbox is not displayed ");

                    //Verify Associate role                    
                    Assert.IsFalse(opportunityDetails.IsAssociateRoleDisplayedLV(), "Verify Associate role should be False ");
                    extentReports.CreateStepLogs("Pass", "Associate role checkbox is not displayed ");

                    //Verify Analyst role
                    Assert.IsFalse(opportunityDetails.IsAnalystRoleDisplayedLV(), "Verify Analyst role should be False ");
                    extentReports.CreateStepLogs("Pass", "Analyst role checkbox is not displayed ");

                    //Verify Specialty role 
                    Assert.IsFalse(opportunityDetails.IsSpecialtyRoleDisplayedLV(), "Verify Specialty role should be False ");
                    extentReports.CreateStepLogs("Pass", "Specialty role checkbox is not displayed ");

                    //Verify PE/HF role 
                    Assert.IsFalse(opportunityDetails.IsPERoleDisplayedLV(), "Verify PE/HF role should be False ");
                    extentReports.CreateStepLogs("Pass", "PE role checkbox is not displayed ");

                    //Verify Public role 
                    Assert.IsFalse(opportunityDetails.IsPublicRoleDisplayedLV(), "Verify Public role should be False ");
                    extentReports.CreateStepLogs("Pass", "Public role checkbox is not displayed ");

                    //Verify Admin role
                    Assert.IsFalse(opportunityDetails.IsAdminRoleDisplayedLV(), "Verify Admin role should be False ");
                    extentReports.CreateStepLogs("Pass", "Admin role checkbox is not displayed ");

                    //Verify RMS role
                    Assert.IsTrue(opportunityDetails.IsRMSRoleDisplayedLV(), "Verify RMS role should be False ");
                    extentReports.CreateStepLogs("Pass", "RMS role checkbox is displayed ");

                    //Verify Expense role
                    Assert.IsTrue(opportunityDetails.IsExpenseOnlyRoleDisplayedLV(), "Verify Expense role should be False ");
                    extentReports.CreateStepLogs("Pass", "Expense role checkbox is displayed ");

                    //Verify Non Registered role 
                    Assert.IsTrue(opportunityDetails.IsNonRegisteredRoleDisplayedLV(), "Verify Registered role should be False ");
                    extentReports.CreateStepLogs("Pass", "Registered role checkbox is displayed ");
                    
                    opportunityDetails.ClickReturnToOpportunityL();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab("Internal Team");
                    randomPages.CloseActiveTab(opportunityName);            
                }
                homePageLV.UserLogoutFromSFLightningView();
                driver.Quit();
                extentReports.CreateStepLogs("Pass", "Browser Closed Successfully");
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
