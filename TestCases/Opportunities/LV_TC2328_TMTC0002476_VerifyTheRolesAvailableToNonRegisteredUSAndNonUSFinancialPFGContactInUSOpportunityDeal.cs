﻿using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_TC2328_TMTC0002476_VerifyTheRolesAvailableToNonRegisteredUSAndNonUSFinancialPFGContactInUSOpportunityDeal:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileT2328 = "LV_TC2328_TMTC0002476_VerifyTheRolesAvailableToNonRegisteredUSAndNonUS";

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2328;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User profile and validate the user
                string userExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(userExl);
                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(userExl), true);
                extentReports.CreateLog("User: " + userExl + " Switched to Lightning View ");
                int users = ReadExcelData.GetRowCount(excelPath, "Users");

                for (int row = 2; row <= users; row++)
                {
                    string opportunityName = ReadExcelData.ReadData(excelPath, "Users", 3);
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

                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Info", "User is on " + opportunityName + " Detail Page ");

                    extentReports.CreateStepLogs("Info", "Validating Roles for " + teamMemberName);
                    string chkInitiator = opportunityDetails.ModifyInternalTeamMembersLV(teamMemberName);

                    //------Validate all the roles checkbox
                    //Verify Initiator role
                    if (teamMemberName.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkInitiator);
                        extentReports.CreateStepLogs("Pass", "Initiator role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkInitiator);
                        extentReports.CreateStepLogs("Pass", "Initiator role checkbox is not displayed ");
                    }

                    //Verify Seller role
                    string chkSeller = opportunityDetails.VerifySellerRole();
                    if (teamMemberName.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkSeller);
                        extentReports.CreateStepLogs("Pass", "Seller role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkSeller);
                        extentReports.CreateStepLogs("Pass", "Seller role checkbox is not displayed ");
                    }

                    //Verify Principal role
                    string chkPrin = opportunityDetails.VerifyPrincipalRole();
                    if (teamMemberName.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkPrin);
                        extentReports.CreateStepLogs("Pass", "Principal role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkPrin);
                        extentReports.CreateStepLogs("Pass", "Principal role checkbox is not displayed ");
                    }
                    //Verify Manager role
                    string chkMgr = opportunityDetails.VerifyManagerRole();
                    if (teamMemberName.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkMgr);
                        extentReports.CreateStepLogs("Pass", "Manager role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkMgr);
                        extentReports.CreateStepLogs("Pass", "Manager role checkbox is not displayed ");
                    }

                    //Verify Associate role
                    string chkAssociate = opportunityDetails.VerifyAssociateRole();
                    if (teamMemberName.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkAssociate);
                        extentReports.CreateStepLogs("Pass", "Associate role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkAssociate);
                        extentReports.CreateStepLogs("Pass", "Associate role checkbox is not displayed ");
                    }

                    //Verify Analyst role
                    string chkAnalyst = opportunityDetails.VerifyAnalystRole();
                    if (teamMemberName.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkAnalyst);
                        extentReports.CreateStepLogs("Pass", "Analyst role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkAnalyst);
                        extentReports.CreateStepLogs("Pass", "Analyst role checkbox is not displayed ");
                    }

                    //Verify Specialty role
                    string chkSpecialty = opportunityDetails.VerifySpecialtyRole();
                    if (teamMemberName.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkSpecialty);
                        extentReports.CreateStepLogs("Pass", "Specialty role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkSpecialty);
                        extentReports.CreateStepLogs("Pass", "Specialty role checkbox is not displayed ");
                    }


                    //Verify PE/HF role
                    string chkPE = opportunityDetails.VerifyPERole();
                    if (teamMemberName.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkPE);
                        extentReports.CreateStepLogs("Pass", "PE role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkPE);
                        extentReports.CreateStepLogs("Pass", "PE role checkbox is not displayed ");
                    }

                    //Verify Public role
                    string chkPublic = opportunityDetails.VerifyPublicRole();
                    if (teamMemberName.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkPublic);
                        extentReports.CreateStepLogs("Pass", "Public role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkPublic);
                        extentReports.CreateStepLogs("Pass", "Public role checkbox is not displayed ");
                    }

                    //Verify Admin role
                    string chkAdmin = opportunityDetails.VerifyAdminRole();
                    if (teamMemberName.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkAdmin);
                        extentReports.CreateStepLogs("Pass", "Admin role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkAdmin);
                        extentReports.CreateStepLogs("Pass", "Admin role checkbox is not displayed ");
                    }

                    //Verify RMS role
                    string chkRMS = opportunityDetails.VerifyRMSRole();
                    if (teamMemberName.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkRMS);
                        extentReports.CreateStepLogs("Pass", "RMS role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkRMS);
                        extentReports.CreateStepLogs("Pass", "RMS role checkbox is not displayed ");
                    }

                    //Verify Expense role
                    string chkExpense = opportunityDetails.VerifyExpenseOnlyRole();
                    if (teamMemberName.Equals("Alexander Odysseos"))
                    {
                        Assert.AreEqual("True", chkExpense);
                        extentReports.CreateStepLogs("Pass", "Expense role checkbox is displayed ");
                    }
                    else
                    {
                        Assert.AreEqual("False", chkExpense);
                        extentReports.CreateStepLogs("Pass", "Expense role checkbox is not displayed ");
                    }

                    //Verify Non Registered role
                    string chkNonReg = opportunityDetails.VerifyNonRegisteredRole();
                    if (teamMemberName.Equals("Alexander Odysseos") || teamMemberName.Equals("Faisal Roukbi"))
                    {
                        Assert.AreEqual("True", chkNonReg);
                        extentReports.CreateLog("Non Registered role checkbox is displayed ");
                        if (teamMemberName.Equals("Alexander Odysseos"))
                            extentReports.CreateStepLogs("Pass", "All role checkboxes are displayed for non registered Non US FIN PFG contact in US Opportunity ");
                        else
                            extentReports.CreateStepLogs("Pass", "Only Non Registered role checkbox is displayed for non registered US FIN PFG contact in US Opportunity ");

                    }
                    else
                    {
                        Assert.AreEqual("False", chkNonReg);
                        extentReports.CreateStepLogs("Pass", "Non Registered role checkbox is not displayed ");
                        extentReports.CreateStepLogs("Pass", "All role checkboxes are displayed for registered Non US PFG contact in Non - US Opportunity ");
                    }
                    opportunityDetails.ClickReturnToOpportunityL();// switched to DefaultView
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                }
                homePageLV.UserLogoutFromSFLightningView();
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Pass", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}