﻿using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_TC2330_TMTC0002482_VerifyTheRolesAvailableToRegisteredUSAndNonUSFinancialPFGContactInOpportunityOfNonPFAJobType : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileT2330 = "LV_T2330_TMTC0002482_VerifyTheRolesAvailable.xlsx";

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

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User profile and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                login.SwitchToClassicView();

                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Info", "User: " + stdUser + " logged in ");

                login.SwitchToLightningExperience();
                extentReports.CreateLog("User: " + stdUser + " Switched to Lightning View ");

                int teamMember = ReadExcelData.GetRowCount(excelPath, "Users");

                for (int row = 2; row <= teamMember; row++)
                {
                    string opportunityName = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 3);
                    string teamMemberName = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);                   
                    
                    homePageLV.ClickAppLauncher();

                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Pass", appName + " App is selected from App Launcher ");

                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    opportunityHome.SearchMyOpportunitiesInLightning(opportunityName, stdUser);
                    extentReports.CreateStepLogs("Info", "User is on " + opportunityName + " Detail Page ");

                    extentReports.CreateStepLogs("Info", "Validating Roles for " + teamMemberName);
                    string chkInitiator = opportunityDetails.ModifyInternalTeamMembersLV(teamMemberName);

                    //------Validate all the roles checkbox
                    //Verify Initiator role
                    Assert.AreEqual("True", chkInitiator);
                    extentReports.CreateStepLogs("Pass","Initiator role checkbox is displayed ");

                    //Verify Seller role
                    string chkSeller = opportunityDetails.VerifySellerRole();
                    Assert.AreEqual("True", chkSeller);
                    extentReports.CreateStepLogs("Pass", "Seller role checkbox is displayed ");

                    //Verify Principal role
                    string chkPrin = opportunityDetails.VerifyPrincipalRole();
                    Assert.AreEqual("False", chkPrin);
                   extentReports.CreateStepLogs("Pass", "Principal role checkbox is not displayed ");

                    //Verify Manager role
                    string chkMgr = opportunityDetails.VerifyManagerRole();
                    Assert.AreEqual("False", chkMgr);
                    extentReports.CreateStepLogs("Pass", "Manager role checkbox is not displayed ");

                    //Verify Associate role
                    string chkAssociate = opportunityDetails.VerifyAssociateRole();
                    Assert.AreEqual("False", chkAssociate);
                    extentReports.CreateStepLogs("Pass", "Associate role checkbox is not displayed ");

                    //Verify Analyst role
                    string chkAnalyst = opportunityDetails.VerifyAnalystRole();
                    Assert.AreEqual("False", chkAnalyst);
                    extentReports.CreateStepLogs("Pass", "Analyst role checkbox is not displayed ");

                    //Verify Specialty role
                    string chkSpecialty = opportunityDetails.VerifySpecialtyRole();
                    Assert.AreEqual("True", chkSpecialty);
                    extentReports.CreateStepLogs("Pass", "Specialty role checkbox is displayed ");

                    //Verify PE/HF role
                    string chkPE = opportunityDetails.VerifyPERole();
                    Assert.AreEqual("False", chkPE);
                    extentReports.CreateStepLogs("Pass", "PE role checkbox is not displayed ");

                    //Verify Public role
                    string chkPublic = opportunityDetails.VerifyPublicRole();
                    Assert.AreEqual("False", chkPublic);
                    extentReports.CreateStepLogs("Pass", "Public role checkbox is not displayed ");

                    //Verify Admin role
                    string chkAdmin = opportunityDetails.VerifyAdminRole();
                    Assert.AreEqual("False", chkAdmin);
                    extentReports.CreateStepLogs("Pass", "Admin role checkbox is not displayed ");

                    //Verify RMS role
                    string chkRMS = opportunityDetails.VerifyRMSRole();
                    Assert.AreEqual("False", chkRMS);
                    extentReports.CreateStepLogs("Pass", "RMS role checkbox is not displayed ");

                    //Verify Expense role
                    string chkExpense = opportunityDetails.VerifyExpenseOnlyRole();
                    Assert.AreEqual("False", chkExpense);
                    extentReports.CreateStepLogs("Pass", "Expense role checkbox is not displayed ");

                    //Verify Non Registered role
                    string chkNonReg = opportunityDetails.VerifyNonRegisteredRole();
                    Assert.AreEqual("False", chkNonReg);
                    extentReports.CreateStepLogs("Pass", "Non Registered role checkbox is not displayed ");

                    if (opportunityName.Equals("Project Neon"))
                    {
                        if (teamMemberName.Equals("Jeffrey Michelson"))
                        {
                            extentReports.CreateStepLogs("Pass", "Only Initiator, Seller and Specialty role's checkboxes are displayed for US Opportunity with Non PFA Job Type for Registered US FIN PFG contact ");
                        }
                        else
                        {
                            extentReports.CreateStepLogs("Pass", "Only Initiator, Seller and Specialty role's checkboxes are displayed for US Opportunity with Non PFA Job Type for Registered Non US FIN PFG contact ");
                        }
                    }
                    else
                    {
                        if (teamMemberName.Equals("Jeffrey Michelson"))
                        {
                            extentReports.CreateStepLogs("Pass", "Only Initiator, Seller and Specialty role's checkboxes are displayed for Foreign Opportunity with Non PFA Job Type for Registered US FIN PFG contact ");
                        }
                        else
                        {
                            extentReports.CreateStepLogs("Pass", "Only Initiator, Seller and Specialty role's checkboxes are displayed for Foreign Opportunity with Non PFA Job Type for Registered Non US FIN PFG contact ");
                        }
                    }
                    opportunityDetails.ClickReturnToOpportunityL();// switched to DefaultView
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                }
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
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