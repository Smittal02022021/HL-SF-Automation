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
    class LV_T1140_T1141_T1151_T1144_T1146_CompaniesAddNewCompanyWithRecordTypeForCapitalProviderAndOperatingCompany:BaseClass
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
        public static string fileT1140 = "LV_T1140_T1141_T1151_T1144_T1146_CompaniesAddNewCompanyWithRecordTypeForCapitalProviderAndOperatingCompany";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        // TMTC0005824/T1140 Verify Company Record Types- Capital Provider, Operating Company, Houlihan Company, Legal Entity, and Conflicts Check LDCCR and Description.
        // TMTC0005827/T1141 Company Create - Required Information Fields
        // TMTC0005845/T1151 Functionality not available on LV  
        // TMTC0008508/T1144 Verify New Company with Record Type Operating Company is created upon furnishing the detail information along with required information.
        // TMTC0008421/T1146 Add New Company with Record Type Capital Provider
        [Test]
        public void AddNewCompanyWithRecordTypeForCapitalProviderAndOperatingCompanyLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT1140;
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
                extentReports.CreateStepLogs("Info", "User: " + valAdminUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valAdminUser), true);
                extentReports.CreateStepLogs("Passed", "User: " + valAdminUser + " logged in on Lightning View");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                string btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 2, 1);
                // Click New Company button
                companyHome.ClickButtonCompanyHomePageLV(btnNameExl);

                //TMTC0005824/T1140 Verify company record types and description on company record page
                Assert.IsTrue(companySelectRecord.AreCompanyRecordTypesDisplayedLV(fileT1140),"Verify All Companies Record Types are Displayed ");
                extentReports.CreateStepLogs("Passed", "Company All Record Types Displayed");
                Assert.IsTrue(companySelectRecord.AreCompanyRecordTypesDescriptionDisplayedLV(fileT1140), "Verify All Companies Record Type's Descriptions are Displayed ");
                extentReports.CreateStepLogs("Passed", "Company All Record Type's Descriptions Displayed");

                btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 3, 1);
                Assert.IsTrue(companySelectRecord.IsButtonPresentOnRecordTypePageLV(btnNameExl));
                extentReports.CreateStepLogs("Passed", "Button: "+ btnNameExl+" present on Company Record Type Page");
                btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 4, 1);
                Assert.IsTrue(companySelectRecord.IsButtonPresentOnRecordTypePageLV(btnNameExl));
                extentReports.CreateStepLogs("Passed", "Button: " + btnNameExl + " present on Company Record Type Page");

                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "System Administrator User: " + valAdminUser + " logged out");
                                
                int rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    // Search standard user by global search
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                    homePage.SearchUserByGlobalSearchN(valUser);
                    extentReports.CreateStepLogs("Info", "User: " + valAdminUser + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string stduser = login.ValidateUserLightningView();
                    Assert.AreEqual(stduser.Contains(valUser), true);
                    extentReports.CreateStepLogs("Passed", "User: " + valAdminUser + " logged in on Lightning View");

                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    // Click Add Company button
                    btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 2, 1);
                    companyHome.ClickButtonCompanyHomePageLV(btnNameExl);

                    // TMTC0005827/T1141 Company Create - Required Information Fields
                    string valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    companySelectRecord.SelectCompanyRecordTypeAndClickNextLV(valRecordTypeExl);
                    // Select company record type                    
                    string createCompanyPage = createCompany.GetCreateCompanyPageHeaderLV();
                    Assert.IsTrue(createCompanyPage.Contains("New Company"));
                    extentReports.CreateStepLogs("Passed", "Page with heading: " + createCompanyPage + " is displayed upon selecting company record type ");

                    // Validate company type display as selected 
                    Assert.AreEqual(valRecordTypeExl, createCompany.GetSelectedCompanyTypeLV());
                    extentReports.CreateStepLogs("Passed", "Selected company type: " + valRecordTypeExl + " choosen on select company record type page is matching on Company create page ");

                    createCompany.ClickSaveNewCompanyButtonLV();
                    string txtError = createCompany.GetErrorMessageCreateCompanyPageLV();
                    string errorExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Error", 2, 1);
                    Assert.AreEqual(errorExl, txtError);
                    extentReports.CreateStepLogs("Passed", "Error message:" + txtError + "is displaying for blank input data for company record type");

                    // Create a  company
                    createCompany.CreateNewCompanyLV(fileT1140, row);
                    extentReports.CreateStepLogs("Info", "New Company Created ");

                    //Validate company detail heading
                    string companyName = companyDetail.GetCompanyNameHeaderLV();
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    Assert.IsTrue(companyName.Contains(companyNameExl));
                    extentReports.CreateStepLogs("Passed", "Company name: : " + companyName + " is displayed upon adding company on Company Detail page Header ");

                    // Validate company name value
                    //string companyName = companyDetail.GetCompanyName();
                    //string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    //Assert.AreEqual(companyNameExl, companyName);
                    extentReports.CreateLog("Company name: " + companyName + " in add company page matches on company details page ");

                    // Validate company type value
                    string companyType = companyDetail.GetCompanyTypeLV();
                    string companyTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    Assert.AreEqual(companyTypeExl, companyType);
                    extentReports.CreateStepLogs("Passed", "Company Type: " + companyType + " in add company page matches on company details page ");

                    // Logout from standard User
                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");

                    valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                    homePage.SearchUserByGlobalSearchN(valAdminUser);
                    extentReports.CreateStepLogs("Info", "User: " + valAdminUser + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(valAdminUser), true);
                    extentReports.CreateStepLogs("Passed", "User: " + valAdminUser + " logged in on Lightning View");
                                        
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    companyHome.GlobalSearchCompanyInLightningView(companyName);
                    companyDetail.ClickEditButtonLV();
                    string companyEditHeading = companyEdit.GetCompanyEditPageHeadingLV();
                    Assert.IsTrue(companyEditHeading.Contains("Edit"));
                    extentReports.CreateStepLogs("Passed", "Page with heading: " + companyEditHeading + " is displayed upon click edit button ");
                    //TMT0015181/ T1144 Verify New Company with Record Type Operating Company is created upon furnishing the detail information along with required information.
                    // Enter all Required values in edit company page
                    companyEdit.EditCompanyDetailsLV(fileT1140, companyType);

                    extentReports.CreateStepLogs("Info", "Required field values are entered into edit company details page ");

                    if (companyType.Equals(ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1)))
                    {
                        //Validate company sub type
                        string companySubType = companyDetail.GetCompanySubTypeLV();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 9), companySubType);
                        extentReports.CreateStepLogs("Passed", "Company sub type: " + companySubType + " is displayed on company details page ");

                        //Validate Ownership value
                        string ownership = companyDetail.GetOwnershipLV();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 10), ownership);
                        extentReports.CreateStepLogs("Passed", "Ownership: " + ownership + " is displayed on company details page ");

                        //Validate industry focus value
                        string industryFocus = companyDetail.GetIndustryFocusLV();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Company", 15), industryFocus);
                        extentReports.CreateStepLogs("Passed", "Industry Focus: " + industryFocus + " is displayed on company details page ");

                        //Validate geographical focus value Not Available  
                        //string geographicalFocus = companyDetail.GetGeographicalFocus();
                        //Assert.AreEqual("Asia", geographicalFocus);
                        //extentReports.CreateLog("Company's Geographical focus: " + geographicalFocus + " is displayed on company details page ");

                        ////Validate deal preference focus value Not Available
                        //string dealPreference = companyDetail.GetDealPreference();
                        //Assert.AreEqual("Credit - Corporate", dealPreference);
                        //extentReports.CreateLog("Deal Preference: " + dealPreference + " is displayed on company details page ");
                    }

                    //Validate description input
                    string descriptionInput = companyDetail.GetDescriptionValueLV();
                    string descriptionInputExl = ReadExcelData.ReadData(excelPath, "Company", 12);
                    Assert.AreEqual(descriptionInputExl, descriptionInput);
                    extentReports.CreateStepLogs("Passed", "Company description: " + descriptionInput + " is displayed on company details page ");

                    //companyHome.SearchCompany(fileT1140, companyType);

                    //if (CustomFunctions.IsElementPresent(driver, txtError))
                    //{
                    //    companyHome.SearchCompany(fileT1140, companyType);
                    //}

                    //Delete company
                   companyDetail.DeleteCompanyLV();
                   extentReports.CreateLog("Created company is deleted successfully ");
                   randomPages.CloseActiveTab(companyName);
                   usersLogin.ClickLogoutFromLightningView();
                   extentReports.CreateStepLogs("Passed", "System Admin: " + valAdminUser + " logged out");
                }           

                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.SwitchTo().DefaultContent();
                companyDetail.DeleteCompanyLV();
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}