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

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTC0033978_1_VerifyTheFunctionalityOfContactsTabOnCompanyDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        HomeMainPage homePage = new HomeMainPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        ContactHomePage contactHome = new ContactHomePage();
        ContactDetailsPage contactDetail = new ContactDetailsPage();

        public static string fileTMTC0033978 = "LV_TMTC0033978_VerifyTheFunctionalityOfContactsTabOnCompanyDetailPage";

        private int rowCompanyName;
        private string newCompanyName;
        private string excelPath;
        private string moduleNameExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMT0076323 Verify the availability of the "Contacts" tab on the Company detail page
        //TMT0076325 Verify the availability of the "New" button in the Contacts tab of the Company Details Page.
        //TMT0076327 Verify that the required field validation appears on clicking the "Save" button without filling in any details on the New Contact of the Company Contact
        //TMT0076329 Verify that entering data in all required fields will create the new contact and redirect the user to the contact detail page with a success message on the screen.
        //TMT0076331 Verify that the created contact will be listed under the Contacts tab of the Company Detail Page
        //TMT0076333 Verify the functionality of "Edit" action button on the Contact - HL Relationship

        [Test]
        public void VerifyFunctionalityOfCompaniesContactsTabLV()
        {
            try
            {
                //Get path of Test data file
                excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0033978;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
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

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for(int row = 2; row <= rowCompanyName; row++)
                {
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
                    createCompany.CreateNewCompanyLV(fileTMTC0033978, row);
                    extentReports.CreateStepLogs("Info", " New Company Created ");
                    //Validate company detail heading
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    newCompanyName = companyDetail.GetCompanyNameHeaderLV();
                    Assert.IsTrue(newCompanyName.Contains(companyNameExl));
                    extentReports.CreateStepLogs("Passed", valRecordTypeExl + " Company created and name :" + newCompanyName + " displayed on Company Detail page Header ");

                    //TMT0076323 Verify the availability of the "Contacts" tab on the Company detail page
                    Assert.IsTrue(companyDetail.IsContactTabDisplayedLV(), "Verify the availability of the 'Contacts' tab on the Company detail page");
                    extentReports.CreateStepLogs("Passed", valRecordTypeExl + " 'Contacts' tab is available on " + newCompanyName + " Company Detail page ");

                    //TMT0076325 Verify the availability of the "New" button in the Contacts tab of the Company Details Page.
                    companyDetail.ClickContactTabLV();
                    Assert.IsTrue(companyDetail.IsNewButtonContactDisplayedLV(), "Verify the availability of the 'New' button in the Contacts tab of the Company Details Page.");
                    extentReports.CreateStepLogs("Passed", valRecordTypeExl + " 'New' button in the Contacts tab of the Company Details Page is Displayed");

                    //TMT0076327 Verify that the required field validation appears on clicking the "Save" button without filling in any details on the New Contact of the Company Contact
                    companyDetail.ClickNewButtonContactTabLV();
                    companyDetail.ClickNextButtonRecordTypeLV();
                    Assert.IsTrue(companyDetail.IsContactNameValidationDisplayedLV(), "Verify that the required field validation appears on clicking the 'Save' button without filling in any details on the New Contact of the Company Contact");
                    extentReports.CreateStepLogs("Passed", valRecordTypeExl + " Required field validation appeared on clicking the 'Save' button without filling in any details on the New Contact of the Company Contact");

                    //TMT0076329 Verify that entering data in all required fields will create the new contact and redirect the user to the contact detail page with a success message on the screen.
                    companyDetail.ClickNewButtonContactTabLV();
                    companyDetail.ClickNextButtonRecordTypeLV();
                    string valFirstName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1);
                    string valLastName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2);
                    string contactFullName = valFirstName + " " + valLastName;
                    string msgSuccess = companyDetail.CreateContactLV(valFirstName, valLastName);
                    Assert.IsTrue(msgSuccess.Contains(contactFullName), "Verify Success messag is poped up on creation of New Contact");
                    extentReports.CreateStepLogs("Passed", msgSuccess);
                    randomPages.CloseActiveTab(contactFullName);

                    //TMT0076331 Verify that the created contact will be listed under the Contacts tab of the Company Detail Page
                    companyDetail.ClickContactTabLV();
                    Assert.IsTrue(companyDetail.IsContactPresentInRelatedTabListLV(contactFullName), "Verify that the created contact will be listed under the Contacts tab of the Company Detail Page");
                    extentReports.CreateStepLogs("Passed", contactFullName + " created contact is listed under the Contacts tab of the Company Detail Page");

                    //TMT0076333 Verify the functionality of "Edit" action button on the Contact - HL Relationship
                    CustomFunctions.PageReload(driver);
                    companyDetail.ClickContactTabLV();
                    companyDetail.ClickEditContactContactTabLV();
                    string valPhoneNumber = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 4);
                    companyDetail.UpdateContactPhoneNumberLV(valPhoneNumber);
                    Assert.AreEqual(valPhoneNumber, companyDetail.GetContactPhoneNumberInRelatedTab(), "Verify the functionality of 'Edit' action button on the Contact - HL Relationship");
                    extentReports.CreateStepLogs("Passed", contactFullName + " Phone Number updated and saved via 'Edit' Action button on the Contact - HL Relationship ");

                    randomPages.CloseActiveTab(newCompanyName);
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

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                for(int row = 2; row <= rowCompanyName; row++)
                {
                    string valFirstName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1);
                    string valLastName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2);
                    string contactFullName = valFirstName + " " + valLastName;
                    contactHome.GlobalSearchContactInLightningView(contactFullName);
                    contactDetail.DeleteContactLV();
                    extentReports.CreateStepLogs("Passed", contactFullName + " Contact Deleted");
                }

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                for(int row = 2; row <= rowCompanyName; row++)
                {
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    companyDetail.DeleteCompanyLV();
                    extentReports.CreateStepLogs("Passed", companyNameExl + " Company Deleted");

                }
                homePageLV.LogoutFromSFLightningAsApprover();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch(Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.SwitchTo().DefaultContent();
                homePageLV.LogoutFromSFLightningAsApprover();
                string valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
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
                try
                {
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    for(int row = 2; row <= rowCompanyName; row++)
                    {
                        string valFirstName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1);
                        string valLastName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2);
                        string contactFullName = valFirstName + " " + valLastName;
                        contactHome.GlobalSearchContactInLightningView(contactFullName);
                        contactDetail.DeleteContactLV();
                        extentReports.CreateStepLogs("Passed", contactFullName + " Contact Deleted");
                    }
                }
                catch
                {
                    CustomFunctions.PageReload(driver);
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    for(int row = 2; row <= rowCompanyName; row++)
                    {
                        string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                        companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                        companyDetail.DeleteCompanyLV();
                        extentReports.CreateStepLogs("Passed", companyNameExl + " Company Deleted");

                    }
                }
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                for(int row = 2; row <= rowCompanyName; row++)
                {
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    companyDetail.DeleteCompanyLV();
                    extentReports.CreateStepLogs("Passed", companyNameExl + " Company Deleted");

                }
                homePageLV.LogoutFromSFLightningAsApprover();
                driver.Quit();
            }
        }
    }
}