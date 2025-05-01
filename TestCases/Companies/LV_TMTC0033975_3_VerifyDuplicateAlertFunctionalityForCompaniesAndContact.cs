using AventStack.ExtentReports;
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
    class LV_TMTC0033975_3_VerifyDuplicateAlertFunctionalityForCompaniesAndContact:BaseClass
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

        public static string fileTMTC0033975 = "LV_TMTC0033975_VerifyDuplicateAlertFunctionalityOfCompaniesAndContact";

        private int rowCompanyName;
        private string excelPath;
        private string valRecordTypeExl;
        private string companyNameExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMT0076751 Verify that the Duplicate alert message appears on the screen when creating a new company matches the existing company record
        //TMT0076753 Verify that the duplicate alert message appears on the screen when the creation of the new contact matches with the existing contact of the company
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

                //TMT0076275 Verify the availability of "Companies" under HL Banker drop list.
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int rowComp = 2; rowComp <= rowCompanyName; rowComp++)
                {
                    for (int row = rowComp; rowComp <= rowCompanyName; rowComp++)
                    {
                        string btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 2, 1);
                        companyHome.ClickButtonCompanyHomePageLV(btnNameExl);
                        string valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1);
                        companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 2);
                        companySelectRecord.SelectCompanyRecordTypeAndClickNextLV(valRecordTypeExl);
                        // Select company record type                    
                        string createCompanyPage = createCompany.GetCreateCompanyPageHeaderLV();
                        Assert.IsTrue(createCompanyPage.Contains("New Company"));
                        extentReports.CreateStepLogs("Passed", "Page with heading: " + createCompanyPage + " is displayed upon selecting company record type ");
                        //TMT0076751	Verify that the Duplicate alert message appears on the screen when creating a new company matches the existing company record
                        // Create a  company
                        bool isAlertFound = createCompany.ValidateDuplicateAlertForCreateNewCompanyLV(fileTMTC0033975);
                        if (isAlertFound)
                        {
                            Assert.IsTrue(isAlertFound);
                            extentReports.CreateStepLogs("Passed", "Duplicate Alert message displayed for " + companyNameExl);
                            break;
                        }
                        else
                        {
                            //extentReports.CreateStepLogs("Passed", companyDetail.GetCompanyNameHeaderLV() + " Created and detail page is closed");
                            randomPages.CloseActiveTab(companyNameExl);
                        }
                    }
                }

                //for (int row = 3; row <= rowCompanyName; row++)
                //{
                //    string btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 2, 1);
                //    companyHome.ClickButtonCompanyHomePageLV(btnNameExl);
                //    string valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 3, 1);
                //    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 3, 2);
                //    companySelectRecord.SelectCompanyRecordTypeAndClickNextLV(valRecordTypeExl);
                //    // Select company record type                    
                //    string createCompanyPage = createCompany.GetCreateCompanyPageHeaderLV();
                //    Assert.IsTrue(createCompanyPage.Contains("New Company"));
                //    extentReports.CreateStepLogs("Passed", "Page with heading: " + createCompanyPage + " is displayed upon selecting company record type ");
                //    //TMT0076751	Verify that the Duplicate alert message appears on the screen when creating a new company matches the existing company record
                //    // Create a  company
                //    bool isAlertFound = createCompany.ValidateDuplicateAlertForCreateNewCompanyLV(fileTMTC0033975);
                //    if (isAlertFound)
                //    {
                //        Assert.IsTrue(isAlertFound);
                //        extentReports.CreateStepLogs("Passed", "Duplicate Alert message displayed for " + companyNameExl);
                //        break;
                //    }
                //    else
                //    {
                //        //extentReports.CreateStepLogs("Passed", companyDetail.GetCompanyNameHeaderLV() + " Created and detail page is closed");
                //        randomPages.CloseActiveTab(companyNameExl);
                //    }
                //}
                companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 2);
                companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                int rowContact = ReadExcelData.GetRowCount(excelPath, "Contact");
                for (int row = 2; row <= rowContact; row++)
                { 
                    for (int cRow = 2; cRow <= rowContact; cRow++)
                    {
                        companyDetail.ClickNewContactLV();
                        //TMT0076753 Verify that the duplicate alert message appears on the screen when the creation of the new contact matches with the existing contact of the company
                        bool isAlertFound = companyDetail.ValidateDuplicateAlertForCreateContactLV(fileTMTC0033975);
                        if (isAlertFound)
                        {
                            Assert.IsTrue(isAlertFound);
                            extentReports.CreateStepLogs("Passed", "Duplicate Contact Alert message displayed while creating contact on " + companyNameExl);
                            break;
                        }
                    }
                }
                randomPages.CloseActiveTab(companyNameExl);
                usersLogin.ClickLogoutFromLightningView();
                string valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                homePage.SearchUserByGlobalSearchN(valAdminUser);
                extentReports.CreateStepLogs("Info", "CF Fin User: " + valAdminUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valAdminUser), true);
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valAdminUser + " logged in on Lightning View");

                //appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                //string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    companyDetail.DeleteCompanyLV();
                    extentReports.CreateStepLogs("Passed", companyNameExl + " Company Deleted");
                }
                usersLogin.ClickLogoutFromLightningView();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.SwitchTo().DefaultContent();
                usersLogin.ClickLogoutFromLightningView();
                string valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                homePage.SearchUserByGlobalSearchN(valAdminUser);
                extentReports.CreateStepLogs("Info", "CF Fin User: " + valAdminUser + " details are displayed. ");
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
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    //string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 2);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    companyDetail.DeleteCompanyLV();
                    extentReports.CreateStepLogs("Passed", companyNameExl + " Company Deleted");
                }
                usersLogin.ClickLogoutFromLightningView();
                driver.Quit();
            }
        }
    }
}
