﻿using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SF_Automation.Pages.HomePage;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_TC2332_TMTC0002488_VerifyTheRolesAvailableToRegisteredUSAndNonUSFinancialNonPFGContactCMInOpportunityOfNonPFAJobType : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileT2332 = "LV_TC2332_TMTC0002488_VerifyTheRolesAvailable.xlsx";

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2332;

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
                int users = ReadExcelData.GetRowCount(excelPath, "Users");
                Console.WriteLine("rowCount " + users);

                for (int row = 2; row <= users; row++)
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
                    extentReports.CreateLog("Initiator role checkbox is displayed ");

                    //Verify Seller role
                    string chkSeller = opportunityDetails.VerifySellerRole();
                    Assert.AreEqual("True", chkSeller);
                    extentReports.CreateLog("Seller role checkbox is displayed ");

                    //Verify Principal role
                    string chkPrin = opportunityDetails.VerifyPrincipalRole();
                    Assert.AreEqual("True", chkPrin);
                    extentReports.CreateLog("Principal role checkbox is displayed ");

                    //Verify Manager role
                    string chkMgr = opportunityDetails.VerifyManagerRole();
                    Assert.AreEqual("True", chkMgr);
                    extentReports.CreateLog("Manager role checkbox is displayed ");

                    //Verify Associate role
                    string chkAssociate = opportunityDetails.VerifyAssociateRole();
                    Assert.AreEqual("True", chkAssociate);
                    extentReports.CreateLog("Associate role checkbox is displayed ");

                    //Verify Analyst role
                    string chkAnalyst = opportunityDetails.VerifyAnalystRole();
                    Assert.AreEqual("True", chkAnalyst);
                    extentReports.CreateLog("Analyst role checkbox is displayed ");

                    //Verify Specialty role
                    string chkSpecialty = opportunityDetails.VerifySpecialtyRole();
                    Assert.AreEqual("True", chkSpecialty);
                    extentReports.CreateLog("Specialty role checkbox is displayed ");

                    //Verify PE/HF role
                    string chkPE = opportunityDetails.VerifyPERole();
                    Assert.AreEqual("True", chkPE);
                    extentReports.CreateLog("PE role checkbox is displayed ");

                    //Verify Public role
                    string chkPublic = opportunityDetails.VerifyPublicRole();
                    Assert.AreEqual("True", chkPublic);
                    extentReports.CreateLog("Public role checkbox is displayed ");

                    //Verify Admin role
                    string chkAdmin = opportunityDetails.VerifyAdminRole();
                    Assert.AreEqual("False", chkAdmin);
                    extentReports.CreateLog("Admin role checkbox is not displayed ");

                    //Verify RMS role
                    string chkRMS = opportunityDetails.VerifyRMSRole();
                    Assert.AreEqual("True", chkRMS);
                    extentReports.CreateLog("RMS role checkbox is displayed ");

                    //Verify Expense role
                    string chkExpense = opportunityDetails.VerifyExpenseOnlyRole();
                    Assert.AreEqual("True", chkExpense);
                    extentReports.CreateLog("Expense role checkbox is displayed ");

                    //Verify Non Registered role
                    string chkNonReg = opportunityDetails.VerifyNonRegisteredRole();
                    Assert.AreEqual("False", chkNonReg);
                    extentReports.CreateLog("Non Registered role checkbox is not displayed ");

                    if (opportunityName.Equals("Project Neon"))
                    {
                        if (teamMemberName.Equals("Peter Wu"))
                        {
                            extentReports.CreateLog("Except Non-Registered and Admin/Intern role checkbox, all other role's checkboxes are displayed for US Opportunity with Registered US FIN Non PFG contact(CM) ");
                        }
                        else
                        {
                            extentReports.CreateLog("Except Non-Registered and Admin/Intern role checkbox, all other role's checkboxes are displayed for US Opportunity with Registered Non US FIN Non PFG contact(CM) ");
                        }
                    }
                    else
                    {
                        if (teamMemberName.Equals("Peter Wu"))
                        {
                            extentReports.CreateLog("Except Non-Registered and Admin/Intern role checkbox, all other role's checkboxes are displayed for Non US Opportunity with Registered US FIN Non PFG contact(CM) ");
                        }
                        else
                        {
                            extentReports.CreateLog("Except Non-Registered and Admin/Intern role checkbox, all other role's checkboxes are displayed for Non US Opportunity with Registered Non US FIN Non PFG contact(CM) ");
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