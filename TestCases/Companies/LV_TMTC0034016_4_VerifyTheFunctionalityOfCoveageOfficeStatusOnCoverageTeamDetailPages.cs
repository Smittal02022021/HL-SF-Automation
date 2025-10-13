using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Company;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.Contact;
using Microsoft.Office.Interop.Excel;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTC0034016_4_VerifyTheFunctionalityOfCoveageOfficeStatusOnCoverageTeamDetailPages : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        HomeMainPage homePage = new HomeMainPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        AddCoverageTeam coverageTeam = new AddCoverageTeam();
        CoverageTeamDetail coverageTeamDetail = new CoverageTeamDetail();
        ContactHomePage contactHome = new ContactHomePage();
        ContactDetailsPage contactDetail = new ContactDetailsPage();

        public static string fileTMTC0034016 = "LV_TMT0076578_VerifyTheFunctionalityOfCoveageOfficeStatusOnCoverageTeamDetailPages";

        private int rowCompanyName;
        private string newCompanyName;
        private string companyNameExl;
        private string excelPath;
        private string valUser;
        private string officerExl;
        private string valAdminUser;
        private string user;
        private string appNameExl;
        private string appName;
        private string moduleNameExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMT0076578 Verify the functionality when an HL Contact becomes Inactive, All related Coverage Teams have the field Coverage Team Status set to Inactive
        //TMT0076582 Verify that when an HL Contact becomes "Active" again, all related Coverage Teams have the field Coverage Team Status is yet "Inactive".
        //TMT0076584 Verify that the status of the coverage officer is updated back to "Active".

        [Test]
        public void VerifyTheFunctionalityOfCoveageOfficeStatusOnCoverageTeamDetailPagesLV()
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
                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for(int row = 2; row <= rowCompanyName; row++)
                {
                    valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                    homePage.SearchUserByGlobalSearchN(valUser);
                    extentReports.CreateStepLogs("Info", "CF Fin User: " + valUser + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(valUser), true);
                    extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged in on Lightning View");

                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                    string btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 2, 1);
                    companyHome.ClickButtonCompanyHomePageLV(btnNameExl);

                    string valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    companySelectRecord.SelectCompanyRecordTypeAndClickNextLV(valRecordTypeExl);
                    // Select company record type                    
                    string createCompanyPage = createCompany.GetCreateCompanyPageHeaderLV();
                    Assert.IsTrue(createCompanyPage.Contains("New Company"));
                    extentReports.CreateStepLogs("Passed", "Page with heading: " + createCompanyPage + " is displayed upon selecting company record type ");

                    // Validate company type display as selected 
                    Assert.AreEqual(valRecordTypeExl, createCompany.GetSelectedCompanyTypeLV());
                    extentReports.CreateStepLogs("Passed", "Selected company type: " + valRecordTypeExl + " choosen on select company record type page is matching on Company create page ");
                    // Create a  company
                    createCompany.CreateNewCompanyLV(fileTMTC0034016, row);
                    extentReports.CreateStepLogs("Info", " New Company Created ");
                    //Validate company detail heading
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    newCompanyName = companyDetail.GetCompanyNameHeaderLV();
                    Assert.IsTrue(newCompanyName.Contains(companyNameExl));
                    extentReports.CreateStepLogs("Passed", valRecordTypeExl + " Company created and name :" + newCompanyName + " displayed on Company Detail page Header ");
                    companyDetail.ClickCoverageTabLV();
                    coverageTeam.ClickNewButtonSponsorCoverageDisplayedLV();
                    coverageTeam.ClickNextButtonRecordTypeLV();
                    officerExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CoverageTeam", row, 1);
                    string tierExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CoverageTeam", row, 2);
                    string levelExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CoverageTeam", row, 4);
                    string typeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CoverageTeam", row, 5);
                    string msgSuccess = coverageTeam.AddNewCoverageTeamLV(officerExl, tierExl, levelExl, typeExl);
                    extentReports.CreateStepLogs("Passed", "New Coverage Team Added with Success Message: " + msgSuccess);
                    string coverageTeamId = coverageTeamDetail.GetCoverageTeamIDLV();
                    extentReports.CreateStepLogs("Passed", "New Coverage Team with ID: " + coverageTeamId + " and user is redirected to Coverage Team Detail Page");
                    string officerStatusExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CoverageTeam", row, 9);
                    string coverageTeamStatus = coverageTeamDetail.GetCoverageTeamStatusLV();
                    Assert.AreEqual(officerStatusExl, coverageTeamStatus);
                    randomPages.CloseActiveTab(coverageTeamId);
                    extentReports.CreateStepLogs("Passed", "Coverage Team Officer Status is " + coverageTeamStatus);
                    Assert.IsTrue(companyDetail.IsIndustryCoverageTamMemberDisplayedRelatedTabListLV(officerExl), "Verify Coverage Team officer should be displayed in List");
                    extentReports.CreateStepLogs("Passed", "Coverage Team officer should be displayed in List");

                    randomPages.CloseActiveTab(companyNameExl);
                    //////////////////////////////////////////////////////////////

                    //Changing the status of Officer as System admin 
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");

                    valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                    homePage.SearchUserByGlobalSearchN(valAdminUser);
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
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    contactHome.GlobalSearchContactInLightningView(officerExl);
                    //In-Activating the HL Contact
                    string contactStatus = ReadExcelData.ReadDataMultipleRows(excelPath, "CoverageTeam", row, 10);
                    contactDetail.UpdateContactStatusLV(contactStatus);
                    extentReports.CreateStepLogs("Passed", "Coverage Team Officer Contact is updated to Inactive");
                    randomPages.CloseActiveTab(officerExl);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Passed", "System Admin: " + valAdminUser + " logged out");
                    /////////////////////////////////////////////////////////
                    ///Verify the Oficer Status as CF Fin user
                    homePage.SearchUserByGlobalSearchN(valUser);
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(valUser), true);
                    extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged in on Lightning View");

                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Passed", "Company: " + companyNameExl + "found and selected ");
                    companyDetail.ClickCoverageTabLV();
                    //Record should be removed
                    //TMT0076578	Verify the functionality when an HL Contact becomes Inactive, All related Coverage Teams have the field Coverage Team Status set to Inactive
                    //#16. Verify that the Coverage officer i.e. Houlihan Contact is Inactive has been removed from the respective types of Coverage Team.
                    Assert.IsFalse(companyDetail.IsIndustryCoverageTamMemberDisplayedRelatedTabListLV(officerExl), "Verify that the Coverage officer i.e. Houlihan Contact is Inactive has been removed from the respective types of Coverage Team.");
                    extentReports.CreateStepLogs("Passed", "Inactive Houlihan Contact is has been removed from the respective types of Coverage Team List.");
                    randomPages.CloseActiveTab(companyNameExl);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");

                    //Changing the status of Contact as System Admin
                    //valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                    homePage.SearchUserByGlobalSearchN(valAdminUser);//Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(valAdminUser), true);
                    extentReports.CreateStepLogs("Passed", "System Admin User: " + valAdminUser + " logged in on Lightning View to update the status of Selected Coverage officer");

                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    contactHome.GlobalSearchContactInLightningView(officerExl);
                    //Activating the HL Contact
                    contactStatus = ReadExcelData.ReadDataMultipleRows(excelPath, "CoverageTeam", row, 9);
                    contactDetail.UpdateContactStatusLV(contactStatus);
                    randomPages.CloseActiveTab(officerExl);

                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Passed", "System Admin: " + valAdminUser + " logged out");

                    //Verify the status of Officer as CF Fin User
                    homePage.SearchUserByGlobalSearchN(valUser);
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(valUser), true);
                    extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged in on Lightning View");

                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Passed", "Company: " + companyNameExl + "found and selected ");

                    // TMT0076582  Verify that when an HL Contact becomes "Active" again, related Coverage Teams will not be displayed as Coverage Officer until the user adds that Coverage Officer again.
                    //#16. Verify that when an HL Contact becomes "Active" again, related Coverage Teams will not be displayed as Coverage Officer until the user adds that Coverage Officer again.
                    companyDetail.ClickCoverageTabLV();
                    Assert.IsFalse(companyDetail.IsIndustryCoverageTamMemberDisplayedRelatedTabListLV(officerExl), "Verify that the Coverage officer i.e. Houlihan Contact is Inactive has been removed from the respective types of Coverage Team.");
                    extentReports.CreateStepLogs("Passed", "Re-Activated Houlihan Contact is not displaying under the respective types of Coverage Team List.");

                    //TMT0076584 Verify that the user can add the same coverage officer once its status is updated to "Active".
                    //# 16.Verify that the user can add the same coverage officer once its status is updated to "Active".
                    coverageTeam.ClickNewButtonSponsorCoverageDisplayedLV();
                    coverageTeam.ClickNextButtonRecordTypeLV();
                    msgSuccess = coverageTeam.AddNewCoverageTeamLV(officerExl, tierExl, levelExl, typeExl);
                    extentReports.CreateStepLogs("Passed", "New Coverage Team Added with Success Message: " + msgSuccess);
                    coverageTeamId = coverageTeamDetail.GetCoverageTeamIDLV();
                    extentReports.CreateStepLogs("Passed", "New Coverage Team with ID: " + coverageTeamId + " and user is redirected to Coverage Team Detail Page");
                    officerStatusExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CoverageTeam", row, 9);
                    coverageTeamStatus = coverageTeamDetail.GetCoverageTeamStatusLV();
                    Assert.AreEqual(officerStatusExl, coverageTeamStatus);
                    randomPages.CloseActiveTab(coverageTeamId);
                    extentReports.CreateStepLogs("Passed", "Coverage Team Officer Status is " + coverageTeamStatus);
                    Assert.IsTrue(companyDetail.IsIndustryCoverageTamMemberDisplayedRelatedTabListLV(officerExl), "Verify Coverage Team officer should be displayed in List");
                    extentReports.CreateStepLogs("Passed", "Coverage Team officer should be displayed in List");

                    randomPages.CloseActiveTab(companyNameExl);
                    //Change the Coverage team satatus as CF FIn user need to work 

                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");

                    //Delete crated company,coverage team, as system admin 
                    homePage.SearchUserByGlobalSearchN(valAdminUser);
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(valAdminUser), true);
                    extentReports.CreateStepLogs("Passed", "System Admin User: " + valAdminUser + " logged in on Lightning View to update the status of Selected Coverage officer");

                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    companyDetail.ClickCoverageTabLV();
                    companyDetail.ClickIndustryCoverageTamMemberLV(officerExl);
                    coverageTeamDetail.DeleteCoverageTeamLV();
                    extentReports.CreateStepLogs("Info", "Created Coverage Team Deleted");
                    companyDetail.DeleteCompanyLV();
                    extentReports.CreateStepLogs("Passed", companyNameExl + " Company Deleted");

                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Passed", "System Administrator: " + valAdminUser + " logged out");
                }
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch(Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                homePageLV.LogoutFromSFLightningAsApprover();
                valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                homePage.SearchUserByGlobalSearchN(valAdminUser);
                extentReports.CreateStepLogs("Info", "System Admin User: " + valAdminUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valAdminUser), true);
                extentReports.CreateStepLogs("Passed", "System Admin User: " + valAdminUser + " logged in on Lightning View");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                contactHome.GlobalSearchContactInLightningView(officerExl);

                if(contactDetail.GetContactStatusLV() == "Inactive")
                {
                    contactDetail.UpdateContactStatusLV("Active");
                    randomPages.CloseActiveTab(officerExl);
                }

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                for(int row = 2; row <= rowCompanyName; row++)
                {
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    companyDetail.ClickCoverageTabLV();
                    try
                    {
                        companyDetail.ClickIndustryCoverageTamMemberLV(officerExl);
                        coverageTeamDetail.DeleteCoverageTeamLV();
                        extentReports.CreateStepLogs("Info", "Created Coverage Team Deleted");
                        companyDetail.DeleteCompanyLV();
                        extentReports.CreateStepLogs("Passed", companyNameExl + " Company Deleted");
                    }
                    catch
                    {
                        companyDetail.DeleteCompanyLV();
                        extentReports.CreateStepLogs("Passed", companyNameExl + " Company Deleted");
                    }
                }
                homePageLV.LogoutFromSFLightningAsApprover();
                driver.Quit();
            }
        }
    }
}