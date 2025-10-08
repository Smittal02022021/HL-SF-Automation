using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Company;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTC0033975_2_VerifyFunctionalityOfCompaniesSystemAdmnUser : BaseClass
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

        public static string fileTMTC0033975 = "LV_TMTC0033975_VerifyFunctionalityOfCompaniesInfoSystemAdmin";

        int rowCompanyName;
        string newCompanyName;
        string excelPath;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMT0076291 Verify that the Company Record Types- Capital Provider, Operating Company, Houlihan Company, Legal Entity, Conflicts Check LDCCR for System Admin
        //TMT0076307 Verify the functionality to update the existing Record Type in the Company Detail Page System Admin
        //TMT0076755 Verify the required field validation by clicking the "Save" button without filling in any details for Houlihan Lockey company System Admin
        //TMT0076757 Verify that the Houlihan Lockey Company is created by clicking the "Save" button and redirecting the user to a Company detail page System Admin

        [Test]
        public void VerifyFunctionalityOfCompaniesSystemAdmnUserLV()
        {
            try
            {
                //Get path of Test data file
                excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0033975;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                string valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(valAdminUser);
                extentReports.CreateStepLogs("Info", "System Admin User: " + valAdminUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valAdminUser), true);
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valAdminUser + " logged in on Lightning View");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                string btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 2, 1);
                // Click New Company button
                companyHome.ClickButtonCompanyHomePageLV(btnNameExl);
                extentReports.CreateStepLogs("Passed", btnNameExl + " button clicked from Company Home page");

                //TMT0076291 Verify that the Company Record Types- Capital Provider, Operating Company, Houlihan Company, Legal Entity, Conflicts Check LDCCR for System Admin
                Assert.IsTrue(companySelectRecord.AreCompanyRecordTypesDisplayedLV(fileTMTC0033975), "Verify All Companies Record Types are Displayed ");
                extentReports.CreateStepLogs("Passed", "Company All Record Types Displayed");

                //TMT0076291 Verify that the Company Record Types- Capital Provider, Operating Company, Houlihan Company, Legal Entity, Conflicts Check LDCCR for System Admin
                Assert.IsTrue(companySelectRecord.AreCompanyRecordTypesDescriptionDisplayedLV(fileTMTC0033975), "Verify All Companies Record Type's Descriptions are Displayed ");
                extentReports.CreateStepLogs("Passed", "Company All Record Type's Descriptions Displayed");
                btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 3, 1);
                companySelectRecord.ClickButtonCompanyChangeRecordTypePageLV(btnNameExl);
                extentReports.CreateStepLogs("Passed", "Company Record type page canceled");
                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");

                btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 2, 1);
                companyHome.ClickButtonCompanyHomePageLV(btnNameExl);
                string valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1);
                companySelectRecord.SelectCompanyRecordTypeAndClickNextLV(valRecordTypeExl);
                // Select company record type                    
                string createCompanyPage = createCompany.GetCreateCompanyPageHeaderLV();
                Assert.IsTrue(createCompanyPage.Contains("New Company"));
                extentReports.CreateStepLogs("Passed", "Page with heading: " + createCompanyPage + " is displayed upon selecting company record type ");

                // Validate company type display as selected 
                Assert.AreEqual(valRecordTypeExl, createCompany.GetSelectedCompanyTypeLV());
                extentReports.CreateStepLogs("Passed", "Selected company type: " + valRecordTypeExl + " choosen on select company record type page is matching on Company create page ");

                // TMT0076755 Verify the required field validation by clicking the "Save" button without filling in any details for Houlihan Lockey company System Admin
                createCompany.ClickSaveNewCompanyButtonLV();
                string txtError = createCompany.GetErrorMessageCreateCompanyPageLV();
                string errorExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Error", 2, 1);
                Assert.AreEqual(errorExl, txtError);
                extentReports.CreateStepLogs("Passed", "Error message:" + txtError + "is displaying for blank input data for company record type");
                createCompany.ClickCancelNewCompanyButtonLV();
                extentReports.CreateStepLogs("Passed", "Page with heading: " + createCompanyPage + " form Cancelled");
                Assert.IsTrue(companyHome.IsSearchRecentOptionDisplayedLV());
                extentReports.CreateStepLogs("Passed", "User Redirected to Company List page and Search functionality available on Companies");

                ///////
                btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 2, 1);
                companyHome.ClickButtonCompanyHomePageLV(btnNameExl);
                valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1);
                companySelectRecord.SelectCompanyRecordTypeAndClickNextLV(valRecordTypeExl);
                createCompanyPage = createCompany.GetCreateCompanyPageHeaderLV();
                Assert.IsTrue(createCompanyPage.Contains("New Company"));
                extentReports.CreateStepLogs("Passed", "Page with heading: " + createCompanyPage + " is displayed upon selecting company record type ");

                // Validate company type display as selected 
                Assert.AreEqual(valRecordTypeExl, createCompany.GetSelectedCompanyTypeLV());
                extentReports.CreateStepLogs("Passed", "Selected company type: " + valRecordTypeExl + " choosen on select company record type page is matching on Company create page ");

                //TMT0076757 Verify that the Houlihan Lockey Company is created by clicking the "Save" button and redirecting the user to a Company detail page System Admin
                //Create a  company
                createCompany.CreateNewCompanyLV(fileTMTC0033975, 2);
                extentReports.CreateStepLogs("Info", " New Company Created ");
                //Validate company detail heading
                string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 2);
                newCompanyName = companyDetail.GetCompanyNameHeaderLV();
                Assert.IsTrue(newCompanyName.Contains(companyNameExl));
                extentReports.CreateStepLogs("Passed", valRecordTypeExl + " Company created and name :" + newCompanyName + " displayed on Company Detail page Header ");

                //TMT0076307 Verify the functionality to update the existing Record Type in the Company Detail Page System Admin
                string companyType = companyDetail.GetCompanyTypeLV();
                companyDetail.ClickInlineChangeRecordTypeButtonLV();
                valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyRecordTypes", 2, 1);
                companyDetail.ChangeCompanyRadioTypeLV(valRecordTypeExl);
                string newCompanyType = companyDetail.GetCompanyTypeLV();
                Assert.AreEqual(valRecordTypeExl, newCompanyType, "Verify Record Type is changed ");
                extentReports.CreateStepLogs("Info", "Company Record Type changed from " + companyType + " to " + newCompanyType);
                companyDetail.DeleteCompanyLV();
                randomPages.CloseActiveTab(companyNameExl);

                homePageLV.LogoutFromSFLightningAsApprover();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch(Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.SwitchTo().DefaultContent();
                homePageLV.LogoutFromSFLightningAsApprover();
                string valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
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
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
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
