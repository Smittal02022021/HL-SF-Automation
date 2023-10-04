using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Companies
{
    class TMTI0046479_46477_46473_VerifyCompaniesHasNestedListToShowHLRelationshipForCoverage:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        CompanyHomePage companyhome = new CompanyHomePage();
        CompanyDetailsPage companyDetails = new CompanyDetailsPage();

        public static string fileTMTI0046479 = "TMTI0046479_VerifyCompaniesHasNestedListToShowHLRelationshipForCoverage";

        public string appNameExl;
        public string moduleNameExl;
        public bool tabDetailPageDisplayed;
        public string tabNameExl;
        public string coverageOfficerNameExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyCompaniesHasNestedListToShowHLRelationshipForCoverageLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0046479;
                string fileToUpload = ReadJSONData.data.filePaths.testData + "FileToUpload.txt";

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                // Calling Login function                
                login.LoginApplication();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                //Login again as CF Financial User
                string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                usersLogin.SearchCFUserAndLogin(valUser);
                string cfUser = login.ValidateUser();
                Assert.AreEqual(cfUser.Contains(valUser), true);
                extentReports.CreateLog("CF User: " + cfUser + " logged in ");

                //Switching to LightningView
                login.SwitchToLightningExperience();
                homePageLV.ClickAppLauncher();
                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectApp(appNameExl);

                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateLog(appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateLog(moduleNameExl + ": Module is selected from menu ");

                int companiesRowsCountExl = ReadExcelData.GetRowCount(excelPath, "Companies");
                for (int row = 2; row <= companiesRowsCountExl; row++)
                {
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 1);
                    companyhome.SearchCompanyInLightning(companyNameExl);
                    extentReports.CreateLog(companyNameExl + ": Company is searched and selected ");

                    string companyTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 2);
                    string valueCompanyType = companyDetails.GetCompanyTypeL();
                    Assert.AreEqual(companyTypeExl, valueCompanyType);
                    extentReports.CreateLog("Selected Company Type is " + valueCompanyType + " ");

                    //TMTI0046477,TMTI0046479-Verify that for Capital Provider & Operating Company companies, Nested List to show Coverage Sector is displaying for Coverage.
                    tabNameExl = ReadExcelData.ReadData(excelPath, "TabName", 1);
                    tabDetailPageDisplayed = companyDetails.ClickCompanyDetailPageTabL(tabNameExl);
                    Assert.IsTrue(tabDetailPageDisplayed, "Verify Coverage Detail section Displayed after clicking on Opportunities Tab ");
                    extentReports.CreateLog("Detail section Displayed after clicking on " + tabNameExl + " Tab ");

                    //Verify that there will be Nested List to show HL Relationship displaying for Contacts.
                    coverageOfficerNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CoverageOfficer", row, 1);
                    Assert.IsTrue(companyDetails.IsCoverageNestedListOfficerL(coverageOfficerNameExl), "Verify that there will be Nested List to show HL Relationship displaying for Contacts");
                    extentReports.CreateLog("Nested List is Displayed to show HL Relationship for Coverage Officer:  " + coverageOfficerNameExl + " for " + valueCompanyType + " Company ");

                    string txtHeaderNestedList = companyDetails.ClickCoverageNestedList(coverageOfficerNameExl);
                    Assert.IsTrue(txtHeaderNestedList.Contains("Coverage"));
                    extentReports.CreateLog("Nested List of Coverage is displayed for Coverage Officer" + coverageOfficerNameExl + "  ");
                    
                    //Get Coverage Type from Nested List of Coverage Officer
                    string txtCompanyOfficeNameCoverageType = companyDetails.GetCompanyOfficeNameCoverageTypeL();
                    extentReports.CreateLog("Coverage type for selected officer in nested list " + txtCompanyOfficeNameCoverageType + " ");
                    
                    companyDetails.ClickNestedCoverageTeamOfficerL(coverageOfficerNameExl);
                   
                    tabNameExl = ReadExcelData.ReadData(excelPath, "TabName", 1);
                    bool IsCoverageTeamDetailsPageDisplayed = companyDetails.IsCoverageTeamDetailsPageDisplayedL(tabNameExl);
                    Assert.IsTrue(IsCoverageTeamDetailsPageDisplayed, "Verify User is on Coverage Team Detail Page ");
                    extentReports.CreateLog("User is on Coverage Team Detail Page ");

                    string txtCoverageTeamCompanyName = companyDetails.GetCoverageTeamCompanyNameL();
                    Assert.AreEqual(txtCoverageTeamCompanyName, companyNameExl);
                    extentReports.CreateLog("Coverage Team Company name is " + txtCoverageTeamCompanyName + " ");

                    //TMTI0046473-Verify that Officer Name is showing same nested Coverage sector that exists in Contacts detail page
                    string txtCoverageOfficerName = companyDetails.GetCoverageOfficerNameL();
                    Assert.AreEqual(txtCoverageOfficerName, coverageOfficerNameExl);
                    extentReports.CreateLog("Coverage Officer Name: " + txtCoverageOfficerName + " Detail Page ");

                    //Match Coverage type of selected Officer Name from Company Detail Page.
                    companyDetails.ClickCoverageSectorPanelL();
                    string txtOfficerCoverageType = companyDetails.GetOfficerCoverageTypeL();
                    Assert.AreEqual(txtOfficerCoverageType, txtCompanyOfficeNameCoverageType);
                    extentReports.CreateLog("Officer Coverage Type is " + txtOfficerCoverageType + " on Coverage Detail Page ");

                    //companyDetails.CloseCompanyTabL(companyNameExl);
                    companyDetails.CloseCoverageTeamDetailPageL();
                    extentReports.CreateLog(companyNameExl + ": Coverage Team Tab Closed ");
                    //companyDetails.CloseCompanyTabL(companyNameExl);
                    //extentReports.CreateLog(companyNameExl + ": Company Tab Closed ");
                }
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                extentReports.CreateLog("User Logged out ");
                driver.Quit();
                extentReports.CreateLog("Browser Closed ");
            }
            catch (Exception ex)
            {
                extentReports.CreateLog(ex.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateLog("Browser Closed ");
            }
        }

    }
}
