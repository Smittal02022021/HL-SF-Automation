using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;


namespace SF_Automation.TestCases.Companies
{
    class LV_TMTT0020659_VerifyCompaniesHasNestedListToShowHLRelationshipIsDisplayingForContactsSystemAdminLightningView : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        CompanyHomePage companyhome = new CompanyHomePage();
        CompanyDetailsPage companyDetails = new CompanyDetailsPage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTMTI0046471 = "TMTI0046471_VerifyCompaniesHasNestedListToShowHLRelationshipForContacts";

        public string appNameExl;
        public string moduleNameExl;
        public bool tabDetailPageDisplayed;
        public string tabNameExl;
        public string contactNameExl;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyCompaniesHasNestedListToShowHLRelationshipForContactsSystemAdminLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0046471;
                string fileToUpload = ReadJSONData.data.filePaths.testData + "FileToUpload.txt";

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Info", driver.Title + " is displayed ");
                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                //Login as System Admin user 
                string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                homePage.SearchUserByGlobalSearchN(adminUserExl);
                extentReports.CreateStepLogs("Info", "System Administrator User: " + adminUserExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(adminUserExl), true);
                extentReports.CreateStepLogs("Passed", "System Administrator User: " + adminUserExl + " logged in on Lightning View");

                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);

                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", moduleNameExl + ": Module is selected from menu ");

                int companiesRowsCountExl = ReadExcelData.GetRowCount(excelPath, "Companies");
                for(int row = 2; row <= companiesRowsCountExl; row++)
                {

                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 1);
                    companyhome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Info", companyNameExl + ": Company is searched and selected ");

                    string companyTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 2);
                    string valueCompanyType = companyDetails.GetCompanyTypeLV();
                    Assert.AreEqual(companyTypeExl, valueCompanyType);
                    extentReports.CreateStepLogs("Passed", "Selected Company Type is " + valueCompanyType + " ");

                    //Go to Contacts tab from Company detail page
                    tabNameExl = ReadExcelData.ReadData(excelPath, "TabName", 1);
                    tabDetailPageDisplayed = companyDetails.ClickCompanyDetailPageTabLV(tabNameExl);
                    Assert.IsTrue(tabDetailPageDisplayed, "Verify Contacts Detail section Displayed after clicking on Opportunities Tab ");
                    extentReports.CreateStepLogs("Passed", "Detail section Displayed after clicking on " + tabNameExl + " Tab ");

                    //Verify that there will be Nested List to show HL Relationship displaying for Contacts.
                    contactNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Contacts", row, 1);
                    Assert.IsTrue(companyDetails.IsContactNestedListHLRelationshipLV(contactNameExl), "Verify that there will be Nested List to show HL Relationship displaying for Contacts");
                    extentReports.CreateStepLogs("Passed", "Nested List is Displayed to show HL Relationship for Contact:  " + contactNameExl + " for " + valueCompanyType + " Company ");

                    //TMTI0046475,TMTI0046476- Verify that Contact is showing same nested HL Relationship that exists in Contacts detail page.
                    string txtHeaderNestedList = companyDetails.ClickContactNestedListHLRelationshipLV(contactNameExl);
                    Assert.IsTrue(txtHeaderNestedList.Contains("Relationship"));
                    string companyHLRelationContact = companyDetails.GetCompanyHLRelationshipContactLV();
                    extentReports.CreateStepLogs("Info", "HL Relationship Contact from Nested List on Company Detail Page :  " + companyHLRelationContact + " ");

                    companyDetails.ClickCompanyNestedContactLV(contactNameExl);
                    // On Contact Detail page click Relationship
                    tabNameExl = ReadExcelData.ReadData(excelPath, "TabName", 2);
                    tabDetailPageDisplayed = contactDetails.ClickContactDetailsPageTabLV(tabNameExl);
                    Assert.IsTrue(tabDetailPageDisplayed, "Verify Contacts Detail section Displayed after clicking on Opportunities Tab ");

                    string contactHLRelationshipContact = contactDetails.GetContactHLRelationshipCotactLV();
                    extentReports.CreateStepLogs("Info", "HL Relationship Contact from Nested List on Contact Detail Page :  " + contactHLRelationshipContact + " ");

                    Assert.AreEqual(companyHLRelationContact, contactHLRelationshipContact);

                    companyDetails.CloseCompanyTabLV(companyNameExl);
                    extentReports.CreateStepLogs("Info", companyNameExl + ": Company Tab Closed ");

                }
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "User: " + adminUserExl + " logged out");
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch(Exception ex)
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