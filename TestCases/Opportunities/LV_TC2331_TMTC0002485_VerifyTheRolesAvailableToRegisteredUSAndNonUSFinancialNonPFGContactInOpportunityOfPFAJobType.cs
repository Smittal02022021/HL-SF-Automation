using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.HomePage;

namespace SF_Automation.TestCases.OpportunitiesInternalTeam
{
    class LV_TC2331_TMTC0002485_VerifyTheRolesAvailableToRegisteredUSAndNonUSFinancialNonPFGContactInOpportunityOfPFAJobType : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileT2331 = "LV_TC2331_TMTC0002485_VerifyTheRolesAvailable";

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
            {//Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2331;

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

                int teamMember = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row <= teamMember; row++)
                {
                    string opportunityName = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 3);
                    string teamMemberName = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);
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
                    Assert.AreEqual("False", chkPrin);
                    extentReports.CreateLog("Principal role checkbox is not displayed ");

                    //Verify Manager role
                    string chkMgr = opportunityDetails.VerifyManagerRole();
                    Assert.AreEqual("False", chkMgr);
                    extentReports.CreateLog("Manager role checkbox is not displayed ");

                    //Verify Associate role
                    string chkAssociate = opportunityDetails.VerifyAssociateRole();
                    Assert.AreEqual("False", chkAssociate);
                    extentReports.CreateLog("Associate role checkbox is not displayed ");

                    //Verify Analyst role
                    string chkAnalyst = opportunityDetails.VerifyAnalystRole();
                    Assert.AreEqual("False", chkAnalyst);
                    extentReports.CreateLog("Analyst role checkbox is not displayed ");

                    //Verify Specialty role
                    string chkSpecialty = opportunityDetails.VerifySpecialtyRole();
                    Assert.AreEqual("True", chkSpecialty);
                    extentReports.CreateLog("Specialty role checkbox is displayed ");

                    //Verify PE/HF role
                    string chkPE = opportunityDetails.VerifyPERole();
                    Assert.AreEqual("False", chkPE);
                    extentReports.CreateLog("PE role checkbox is not displayed ");

                    //Verify Public role
                    string chkPublic = opportunityDetails.VerifyPublicRole();
                    Assert.AreEqual("False", chkPublic);
                    extentReports.CreateLog("Public role checkbox is not displayed ");

                    //Verify Admin role
                    string chkAdmin = opportunityDetails.VerifyAdminRole();
                    Assert.AreEqual("False", chkAdmin);
                    extentReports.CreateLog("Admin role checkbox is not displayed ");

                    //Verify RMS role
                    string chkRMS = opportunityDetails.VerifyRMSRole();
                    Assert.AreEqual("False", chkRMS);
                    extentReports.CreateLog("RMS role checkbox is not displayed ");

                    //Verify Expense role
                    string chkExpense = opportunityDetails.VerifyExpenseOnlyRole();
                    Assert.AreEqual("False", chkExpense);
                    extentReports.CreateLog("Expense role checkbox is not displayed ");

                    //Verify Non Registered role
                    string chkNonReg = opportunityDetails.VerifyNonRegisteredRole();
                    Assert.AreEqual("False", chkNonReg);
                    extentReports.CreateLog("Non Registered role checkbox is not displayed ");

                    if (opportunityName.Equals("Project Peloton"))
                    {
                        if (teamMemberName.Equals("Philip Keffer"))//Kevin Ma"))
                        {
                            extentReports.CreateLog("Only Initiator, Seller and Specialty role's checkboxes are displayed for US Opportunity with PFA Job Type for Registered US FIN Non PFG contact ");
                        }
                        else
                        {
                            extentReports.CreateLog("Only Initiator, Seller and Specialty role's checkboxes are displayed for US Opportunity with PFA Job Type for Registered Non US FIN Non PFG contact ");
                        }
                    }
                    else
                    {
                        if (teamMemberName.Equals("Philip Keffer"))//Kevin Ma"))
                        {
                            extentReports.CreateLog("Only Initiator, Seller and Specialty role's checkboxes are displayed for Foreign Opportunity with PFA Job Type for Registered US FIN Non PFG contact ");
                        }
                        else
                        {
                            extentReports.CreateLog("Only Initiator, Seller and Specialty role's checkboxes are displayed for Foreign Opportunity with PFA Job Type for Registered Non US FIN Non PFG contact ");
                        }
                    }
                    opportunityDetails.ClickReturnToOpportunityL();// switched to DefaultView
                    randomPages.CloseActiveTab("Internal Team");
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");
                    randomPages.CloseActiveTab(opportunityName);
                }
                homePageLV.UserLogoutFromSFLightningView();
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
