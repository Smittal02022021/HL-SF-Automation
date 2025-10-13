using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Company;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using NUnit.Framework;
using SF_Automation.TestData;
using System;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTC0033975_1_VerifyFunctionalityOfCompaniesInfoCFFinUser : BaseClass
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

        public static string fileTMTC0033975 = "LV_TMTC0033975_VerifyFunctionalityOfCompaniesInfo";

        //string excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0033975;
        //private string[] newCompanyName = new string[5];
        //private int index = 0;
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
        //TMT0076275 Verify the availability of "Companies" under HL Banker drop list.
        //TMT0076278 Verify that when you select Companies, you will list the "Recently Viewed" default list of the chosen view of the Companies.
        //TMT0076281 Verify the different list view options available to view Companies on the Companies object
        //TMT0076283 Verify that the Search functionality available on Companies.
        //TMT0076285 Verify that the Search functionality on the Company will display the results as per the entered keyword.
        //TMT0076287 Verify that on clicking "New" will allow user to choose Company Type to add New Company
        //TMT0076289 Verify that the Company Record Types- Capital Provider, Operating Company for CF Financial User
        //TMT0076293 Verify the required field validation by clicking the "Save" button without filling in any details
        //TMT0076295 Verify that the Company is not created by clicking the "Cancel" button and the Page is reverted to the Company listing page
        //TMT0076297 Verify that the Company is created by clicking the "Save" button and redirecting the user to the Company detail page.
        //TMT0076299 Verify the "Info" tab on Company detail page
        //TMT0076301 Verify that clicking the pencil icon will allow the user to update the details of the Company under the info tab.
        //TMT0076303 Verify that clicking the "Save" button will save the updated details on the Company info page
        //TMT0076305 Verify that the user can update the Company details by clicking the "Edit" button given at the top of the company detail page
        //TMT0076307 Verify the functionality to update the existing Record Type in the Company Detail Page System Admin 
        //TMT0076669 Verify that the error message appears for the "Sector" field when Industry is selected and Sector is blank
        //TMT0076679 Verify the functionality of the "Delete" button on the Company detail page for the System Admin

        [Test]
        public void VerifyFunctionalityOfCompaniesInfoLV()
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

                string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
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

                //TMT0076275 Verify the availability of "Companies" under HL Banker drop list.
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                //TMT0076278 Verify that when you select Companies, you will list the "Recently Viewed" default list of the chosen view of the Companies.
                string valViewExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Views", 6, 1);
                Assert.AreEqual(valViewExl, companyHome.GetDefaultSelectedViewLV(), "Verify the Default View selection is " + valViewExl);
                extentReports.CreateStepLogs("Passed", "Company Home page Default selected View is " + valViewExl);

                //TMT0076281 Verify the different list view options available to view Companies on the Companies object
                //companyHome.ClickIconViewsOptionLV();
                Assert.IsTrue(companyHome.AreViewOptionsDisplayedLV(fileTMTC0033975), "Verify the different list view options available to view Companies on the Companies object");
                extentReports.CreateStepLogs("Passed", "All List View options available to view Companies on the Companies object");

                //TMT0076283 Verify that the Search functionality available on Companies.
                Assert.IsTrue(companyHome.IsSearchRecentOptionDisplayedLV(), "Verify that the Search functionality available on Companies");
                extentReports.CreateStepLogs("Passed", "Search functionality available on Companies");

                //TMT0076285 Verify that the Search functionality on the Company will display the results as per the entered keyword.
                string companyName = companyHome.GetCompanyFromDislayedListLV();
                Assert.IsTrue(companyHome.SearchRecentCompanyLV(companyName), "Verify that the Search functionality on the Company will display the results as per the entered keyword");
                extentReports.CreateStepLogs("Passed", "Search functionality is working on Companies home page, Company " + companyName + " found");

                string btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 2, 1);
                companyHome.ClearRecentSearchAreaLV();
                // Click New Company button
                companyHome.ClickButtonCompanyHomePageLV(btnNameExl);
                extentReports.CreateStepLogs("Passed", btnNameExl + " button clicked from Company Home page");
                //TMT0076287 Verify that on clicking "New" will allow user to choose Company Type to add New Company as CF Fin User
                Assert.IsTrue(companySelectRecord.AreCompanyRecordTypesDisplayedLV(fileTMTC0033975), "Verify All Companies Record Types are Displayed ");
                extentReports.CreateStepLogs("Passed", "Company All Record Types Displayed");

                //TMT0076289 Verify that the Company Record Types - Capital Provider, Operating Company with description for CF Financial User
                Assert.IsTrue(companySelectRecord.AreCompanyRecordTypesDescriptionDisplayedLV(fileTMTC0033975), "Verify All Companies Record Type's Descriptions are Displayed ");
                extentReports.CreateStepLogs("Passed", "Company All Record Type's Descriptions Displayed");
                btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 3, 1);
                companySelectRecord.ClickButtonCompanyChangeRecordTypePageLV(btnNameExl);
                extentReports.CreateStepLogs("Passed", "Company Record type page canceled");

                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for(int row = 2; row <= rowCompanyName; row++)
                {
                    btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 2, 1);
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

                    // TMT0076293 Verify the required field validation by clicking the "Save" button without filling in any details
                    createCompany.ClickSaveNewCompanyButtonLV();
                    string txtError = createCompany.GetErrorMessageCreateCompanyPageLV();
                    string errorExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Error", 2, 1);
                    Assert.AreEqual(errorExl, txtError);
                    extentReports.CreateStepLogs("Passed", "Error message:" + txtError + "is displaying for blank input data for company record type");

                    //TMT0076295 Verify that the Company is not created by clicking the "Cancel" button and the Page is reverted to the Company listing page
                    createCompany.ClickCancelNewCompanyButtonLV();
                    extentReports.CreateStepLogs("Passed", "Page with heading: " + createCompanyPage + " form Cancelled");
                    Assert.IsTrue(companyHome.IsSearchRecentOptionDisplayedLV());
                    extentReports.CreateStepLogs("Passed", "User Redirected to Company List page and Search functionality available on Companies");

                }

                for(int row = 2; row <= rowCompanyName; row++)
                {
                    btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 2, 1);
                    companyHome.ClickButtonCompanyHomePageLV(btnNameExl);

                    // TMT0076293 Verify the required field validation by clicking the "Save" button without filling in any details
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
                    createCompany.CreateNewCompanyLV(fileTMTC0033975, row);
                    extentReports.CreateStepLogs("Info", " New Company Created ");
                    //TMT0076297 Verify that the Company is created by clicking the "Save" button and redirecting the user to the Company detail page.
                    //Validate company detail heading
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    newCompanyName = companyDetail.GetCompanyNameHeaderLV();
                    Assert.IsTrue(newCompanyName.Contains(companyNameExl));
                    extentReports.CreateStepLogs("Passed", valRecordTypeExl + " Company created and name :" + newCompanyName + " displayed on Company Detail page Header ");

                    //TMT0076299 Verify the "Info" tab on Company detail page
                    companyDetail.ClickInfoTabLV();
                    //Connected Data is present for now removed from File need to confirm
                    Assert.IsTrue(companyDetail.AreAllSectionsDisplayedLV(valRecordTypeExl, fileTMTC0033975), "Verify the available Info tab sections on Company detail page");
                    extentReports.CreateStepLogs("Passed", "All sections are available under Info tab on " + valRecordTypeExl + " Company detail page ");

                    //TMT0076669 Verify that the error message appears for the "Sector" field when Industry is selected and Sector is blank
                    companyDetail.ClickEditCompanyButtonLV();
                    bool isSectorValidationDisplayed = companyDetail.GetIGSectorValidationLV();
                    if(valRecordTypeExl == "Operating Company")
                    {
                        Assert.IsTrue(isSectorValidationDisplayed);
                        extentReports.CreateStepLogs("Passed", "Validation error message appeared for the 'Sector' field when Industry is selected and Sector is blank for " + valRecordTypeExl);
                    }
                    if(valRecordTypeExl == "Capital Provider")
                    {
                        Assert.IsFalse(isSectorValidationDisplayed);
                        extentReports.CreateStepLogs("Passed", "Validation error message not appeared for the 'Sector' field when Industry is selected and Sector is blank for " + valRecordTypeExl);
                    }

                    // TMT0076301 Verify that clicking the pencil icon will allow the user to update the details of the Company under the info tab.
                    companyDetail.ClickInEditInlinePhoneNumberLV();
                    Assert.IsTrue(companyDetail.IsPhoneEditableInlineLV(), "Verify that clicking the pencil icon will allow the user to update the details of the Company under the info tab.");
                    extentReports.CreateStepLogs("Passed", "Clicking the Pencil/Inline icon allows the user to update the details of " + valRecordTypeExl + " the Company under the info tab.");

                    //TMT0076303 Verify that clicking the "Save" button will save the updated details on the Company info page
                    string phoneNumberExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 17);
                    companyDetail.UpdatePhoneNumberInlineEditLV(phoneNumberExl);
                    Assert.AreEqual(companyDetail.GetCompanyPhoneNumberLV(), phoneNumberExl, "Verify that clicking the 'Save' button will save the updated details on the Company info page ");
                    extentReports.CreateStepLogs("Passed", "Phone number updated by Clicking the Pencil/Inline icon on " + valRecordTypeExl + " Company Detail page");

                    //TMT0076305 Verify that the user can update the Company details by clicking the "Edit" button given at the top of the company detail page
                    phoneNumberExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 18);
                    companyDetail.EditCompanyPhoneNumberLV(phoneNumberExl);
                    Assert.AreEqual(companyDetail.GetCompanyPhoneNumberLV(), phoneNumberExl, "Verify user can update the Company details by clicking the 'Edit' button given at the top of the company detail page");
                    extentReports.CreateStepLogs("Passed", "Phone number updated by clicking the 'Edit' button given at the top of the " + valRecordTypeExl + " company detail page");
                    randomPages.CloseActiveTab(newCompanyName);
                }
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");

                //System Admin actions    
                string valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(valAdminUser);
                extentReports.CreateStepLogs("Info", "CF Fin User: " + valAdminUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valAdminUser), true);
                extentReports.CreateStepLogs("Passed", "System Administrator User: " + valAdminUser + " logged in on Lightning View");

                //string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                //TMT0076275 Verify the availability of "Companies" under HL Banker drop list.
                //string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                //TMT0076307 Verify the functionality to update the existing Record Type in the Company Detail Page System Admin 
                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for(int row = 2; row <= rowCompanyName; row++)
                {
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    string companyType = companyDetail.GetCompanyTypeLV();
                    companyDetail.ClickInlineChangeRecordTypeButtonLV();
                    string valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RecordTypes", row, 1);
                    companyDetail.ChangeCompanyRadioTypeLV(valRecordTypeExl);
                    string newCompanyType = companyDetail.GetCompanyTypeLV();
                    Assert.AreEqual(valRecordTypeExl, newCompanyType, "Verify Record Type is changed ");
                    extentReports.CreateStepLogs("Info", "Company Record Type changed from " + companyType + " to " + newCompanyType);
                    //TMT0076679 Verify the functionality of the "Delete" button on the Company detail page for the System Admin
                    companyDetail.DeleteCompanyLV();
                    extentReports.CreateStepLogs("Info", "Company: " + companyNameExl + " Deleted via 'Delete' button on the Company detail page for the System Admin");
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