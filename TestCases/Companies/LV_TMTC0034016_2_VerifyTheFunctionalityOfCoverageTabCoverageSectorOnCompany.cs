using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Company;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTC0034016_2_VerifyTheFunctionalityOfCoverageTabCoverageSectorOnCompany : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        HomeMainPage homePage = new HomeMainPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        CoverageTeamDetail coverageTeamDetail = new CoverageTeamDetail();

        public static string fileTMTC0034016 = "LV_TMTC0034016_VerifyTheFunctionalityOfCoverageTabOnCompany";

        private int rowCompanyName;
        private string newCompanyName;
        private string companyNameExl;
        private string excelPath;
        private string valUser;
        private string msgSuccess;
        private string[] sectorDependencyName = new string[3];

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMT0076441 Verify that the user can add "Coverage Sector" on the Standard Coverage Team Member and redirect the user to Coverage Sector detail page followed by a success message
        //TMT0076443 Verify that the user can "Edit" the Coverage Sector added on the Standard Coverage Team Member

        [Test]
        public void VerifyTheFunctionalityOfCoverageTabCoverageSectorOnCompanyLV()
        {
            try
            {
                //Get path of Test data file
                excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0034016;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "CF Fin User: " + valUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged in on Lightning View");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "ExistingCompany");
                for(int row = 2; row <= rowCompanyName; row++)
                {
                    string valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ExistingCompany", row, 1);
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ExistingCompany", row, 2);

                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Passed", "Company: " + companyNameExl + " with Record Type :" + valRecordTypeExl + " found and selected ");
                    newCompanyName = companyDetail.GetCompanyNameHeaderLV();
                    Assert.IsTrue(newCompanyName.Contains(companyNameExl));
                    extentReports.CreateStepLogs("Passed", valRecordTypeExl + " Company name :" + newCompanyName + " found and selected");
                    extentReports.CreateStepLogs("Passed", companyDetail.IsCoverageTabDisplayedLV() + " 'Coverage' tab is available on " + newCompanyName + " Company Detail page ");
                    companyDetail.ClickCoverageTabLV();
                    companyDetail.ClickIndustryCoverageTamMemberLV(valUser);
                    string coverageID = coverageTeamDetail.GetCoverageTeamIDLV();
                    //TMT0076441 Verify that the user can add "Coverage Sector" on the Standard Coverage Team Member and redirect the user to Coverage Sector detail page followed by a success message
                    coverageTeamDetail.ClickCoverageSectorPanelLV();
                    coverageTeamDetail.ClickNewCoverageSectorsSectionButtonLV();
                    string coverageSectorDepExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CoverageTeam", row, 10);
                    msgSuccess = coverageTeamDetail.AddNewCoverageSectorLV(coverageSectorDepExl);
                    sectorDependencyName[row - 2] = coverageTeamDetail.GetCoverageSectorDependencyNameLV();
                    extentReports.CreateStepLogs("Passed", "Coverage Sector Dependency:: " + sectorDependencyName[row - 2] + " Added with Success message: " + msgSuccess);
                    randomPages.CloseActiveTab(sectorDependencyName[row - 2]);

                    //TMT0076443 Verify that the user can "Edit" the Coverage Sector added on the Standard Coverage Team Member
                    coverageTeamDetail.ClickCoverageSectorPanelLV();
                    coverageSectorDepExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CoverageTeam", row, 11);
                    msgSuccess = coverageTeamDetail.UpdateCoverageSectorDependenciesLV(sectorDependencyName[row - 2], coverageSectorDepExl);
                    extentReports.CreateStepLogs("Passed", "Coverage Sector Dependency:: " + sectorDependencyName[row - 2] + " Updated with Success message: " + msgSuccess);

                    randomPages.CloseActiveTab(coverageID);
                    randomPages.CloseActiveTab(companyNameExl);
                }
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");
                string valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                homePage.SearchUserByGlobalSearchN(valAdminUser);
                extentReports.CreateStepLogs("Info", "System Admin User: " + valAdminUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valAdminUser), true);
                extentReports.CreateStepLogs("Passed", "System Admin User: " + valAdminUser + " logged in on Lightning View");

                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                for(int row = 2; row <= rowCompanyName; row++)
                {
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ExistingCompany", row, 2);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Passed", companyNameExl + " found and selected");
                    companyDetail.ClickCoverageTabLV();
                    companyDetail.ClickIndustryCoverageTamMemberLV(valUser);
                    //Need to delete sector
                    coverageTeamDetail.ClickCoverageSectorPanelLV();
                    coverageTeamDetail.DeleteCoverageSector(sectorDependencyName[row - 2]);
                    randomPages.CloseActiveTab(companyNameExl);
                }
                homePageLV.LogoutFromSFLightningAsApprover();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch(Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                homePageLV.LogoutFromSFLightningAsApprover();
                string valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                homePage.SearchUserByGlobalSearchN(valAdminUser);
                extentReports.CreateStepLogs("Info", "System Admin User: " + valAdminUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valAdminUser), true);
                extentReports.CreateStepLogs("Info", "System Admin User: " + valAdminUser + " logged in on Lightning View");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Info", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                for(int row = 2; row <= rowCompanyName; row++)
                {
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ExistingCompany", row, 2);
                    try
                    {
                        companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                        companyDetail.ClickCoverageTabLV();
                        companyDetail.ClickIndustryCoverageTamMemberLV(valUser);
                        coverageTeamDetail.ClickCoverageSectorPanelLV();
                        coverageTeamDetail.DeleteCoverageSector(sectorDependencyName[row - 2]);
                        randomPages.CloseActiveTab(companyNameExl);
                    }
                    catch
                    {
                        randomPages.CloseActiveTab(companyNameExl);
                    }
                }
                homePageLV.LogoutFromSFLightningAsApprover();
                driver.Quit();
            }
        }
    }
}
