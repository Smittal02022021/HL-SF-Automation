﻿using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.OpportunitiesInternalTeam
{
    class LV_TC2154_TMTC0002470_VerifyTheRolesAvailableToRegisteredUSFinancialPFGContactInUSAndForeignOpportunityDeal:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileT2330 = "LV_T2327_TMTC0002473_VerifyTheRolesAvailableToRegistered";

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2330;
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
                string userExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearchN(userExl);
                extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(userExl), true);
                extentReports.CreateLog("User: " + userExl + " Switched to Lightning View ");

                extentReports.CreateLog("User: " + stdUser + " Switched to Lightning View ");
                int teamMember = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row <= teamMember; row++)
                {
                    string opportunityName = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 3);
                    string teamMemberName = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);
                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Pass", appName + " App is selected from App Launcher ");

                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName); 
                    extentReports.CreateStepLogs("Info", "User is on " + opportunityName + " Detail Page ");

                    extentReports.CreateStepLogs("Info", "Validating Roles for " + teamMemberName );
                    string chkInitiator = opportunityDetails.ModifyInternalTeamMembersLV(teamMemberName);

                    //------Validate all the roles checkbox
                    //Verify Initiator role
                    Assert.AreEqual("True", chkInitiator);
                    extentReports.CreateStepLogs("Pass", "Initiator role checkbox is displayed ");

                    //Verify Seller role
                    string chkSeller = opportunityDetails.VerifySellerRole();
                    Assert.AreEqual("True", chkSeller);
                    extentReports.CreateStepLogs("Pass", "Seller role checkbox is displayed ");

                    //Verify Principal role
                    string chkPrin = opportunityDetails.VerifyPrincipalRole();
                    Assert.AreEqual("True", chkPrin);
                    extentReports.CreateStepLogs("Pass", "Principal role checkbox is displayed ");

                    //Verify Manager role
                    string chkMgr = opportunityDetails.VerifyManagerRole();
                    Assert.AreEqual("True", chkMgr);
                    extentReports.CreateStepLogs("Pass", "Manager role checkbox is displayed ");

                    //Verify Associate role
                    string chkAssociate = opportunityDetails.VerifyAssociateRole();
                    Assert.AreEqual("True", chkAssociate);
                    extentReports.CreateStepLogs("Pass", "Associate role checkbox is displayed ");

                    //Verify Analyst role
                    string chkAnalyst = opportunityDetails.VerifyAnalystRole();
                    Assert.AreEqual("True", chkAnalyst);
                    extentReports.CreateStepLogs("Pass", "Analyst role checkbox is displayed ");

                    //Verify Specialty role
                    string chkSpecialty = opportunityDetails.VerifySpecialtyRole();
                    Assert.AreEqual("True", chkSpecialty);
                    extentReports.CreateStepLogs("Pass", "Specialty role checkbox is displayed ");

                    //Verify PE/HF role
                    string chkPE = opportunityDetails.VerifyPERole();
                    Assert.AreEqual("True", chkPE);
                    extentReports.CreateStepLogs("Pass", "PE role checkbox is displayed ");

                    //Verify Public role
                    string chkPublic = opportunityDetails.VerifyPublicRole();
                    Assert.AreEqual("True", chkPublic);
                    extentReports.CreateStepLogs("Pass", "Public role checkbox is displayed ");

                    //Verify Admin role
                    string chkAdmin = opportunityDetails.VerifyAdminRole();
                    Assert.AreEqual("True", chkAdmin);
                    extentReports.CreateStepLogs("Pass", "Admin role checkbox is displayed ");

                    //Verify RMS role
                    string chkRMS = opportunityDetails.VerifyRMSRole();
                    Assert.AreEqual("True", chkRMS);
                    extentReports.CreateStepLogs("Pass", "RMS role checkbox is displayed ");

                    //Verify Expense role
                    string chkExpense = opportunityDetails.VerifyExpenseOnlyRole();
                    Assert.AreEqual("True", chkExpense);
                    extentReports.CreateStepLogs("Pass", "Expense role checkbox is displayed ");

                    //Verify Non Registered role
                    string chkNonReg = opportunityDetails.VerifyNonRegisteredRole();
                    if (opportunityName.Equals("Project Peloton"))
                    {
                        Assert.AreEqual("False", chkNonReg);
                        extentReports.CreateStepLogs("Pass", "Non Registered role checkbox is not displayed ");
                        extentReports.CreateStepLogs("Pass", "All role checkboxes except Non Registered are displayed for registered Non US PFG contact in US Opportunity ");
                    }
                    else
                    {
                        Assert.AreEqual("True", chkNonReg);
                        extentReports.CreateStepLogs("Pass", "Non Registered role checkbox is displayed ");
                        extentReports.CreateStepLogs("Pass", "All role checkboxes are displayed for registered Non US PFG contact in Non US Opportunity ");
                    }

                    opportunityDetails.ClickReturnToOpportunityL();// switched to DefaultView
                    randomPages.CloseActiveTab("Internal Team");
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab(opportunityName);
                }
                homePageLV.UserLogoutFromSFLightningView();
                usersLogin.UserLogOut();
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
