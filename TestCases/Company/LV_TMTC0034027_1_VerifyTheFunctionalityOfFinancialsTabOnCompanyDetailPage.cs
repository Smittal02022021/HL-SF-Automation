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
    class LV_TMTC0034027_1_VerifyTheFunctionalityOfFinancialsTabOnCompanyDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        HomeMainPage homePage = new HomeMainPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTC0034027 = "TMTC0034027_VerifyTheFunctionalityOfFinancialsTabOnCompanyDetailPage";

        private int rowCompanyName;
        private string companyNameExl;
        private string excelPath;
        private string valUser;
        private string tabNameExl;
        private string valAdminUser;
        private string companyHLFinancialName;
        private string user;
        private string appNameExl;
        private string appName;
        private string moduleNameExl;
        private string msgSuccess;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        //TMT0076489 Verify the availability of the "Financials" tab on the Company detail page for the CF Financials User.
        //TMT0076491 Verify that the "Financials" tab lists all the company financials added from the different data sources.
        //TMT0076493 Verify that the "New" button is available on the Financials tab to add new HL Company Financials for System Admin.
        //TMT0076495 Verify that the System Admin can add HL Company Financials using the "New" button on the Financials tab of the Company Detail Page.
        //TMT0076497 Verify that the System Admin can update HL Company Financials using the "Edit" button on the Financials tab of the Company Detail Page
        //TMT0076499 Verify that the most recent Year and Date information is updated on the Company's Annual Financials
        //TMT0076501 Verify that clicking "Delete" will delete the HL Company Financials and give a success message on the screen.

    [Test]
        public void VerifyTheFunctionalityOfFinancialsTabOnCompanyDetailPageLV()
    {
        try
        {
            //Get path of Test data file
                excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0034027;
            //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

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
                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Companies");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 1);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Passed", "Company: "+companyNameExl + " found and selected");

                    //TMT0076489 Verify the availability of the "Financials" tab on the Company detail page for the CF Financials User.
                    tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TabName", 2, 1);
                    Assert.IsTrue(companyDetail.IsCompanyDetailPageTabPresentLV(tabNameExl), "Verify the availability of the '" + tabNameExl + "' tab on the Company detail page");
                    extentReports.CreateStepLogs("Passed", tabNameExl + " tab available on the Company detail page");

                    //TMT0076491 Verify that the "Financials" tab lists all the company financials added from the different data sources.
                    companyDetail.ClickCompanyDetailPageTabLV(tabNameExl);
                    Assert.IsTrue(companyDetail.IsCompanyFiancialRecordsFoundLV(), "Verify that the 'Financials' tab lists all the company financials added from the different data sources.");
                    extentReports.CreateStepLogs("Passed", "'Financials' tab lists all the company financials added from the different data sources on the Company detail page");
                    randomPages.CloseActiveTab(companyNameExl);

                }
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");

                valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                homePage.SearchUserByGlobalSearchN(valAdminUser);
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valAdminUser), true);
                extentReports.CreateStepLogs("Passed", "System Admin User: " + valAdminUser + " logged in on Lightning View");

                //appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                for (int row = 2; row <= rowCompanyName; row++)
                {
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 1);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Passed", "Company: " + companyNameExl + " found and selected");

                    tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TabName", 2, 1);
                    Assert.IsTrue(companyDetail.IsCompanyDetailPageTabPresentLV(tabNameExl), "Verify the availability of the '" + tabNameExl + "' tab on the Company detail page");
                    extentReports.CreateStepLogs("Passed", tabNameExl + " tab available on the Company detail page");

                    companyDetail.ClickCompanyDetailPageTabLV(tabNameExl);
                    
                    //TMT0076493 Verify that the "New" button is available on the Financials tab to add new HL Company Financials for System Admin.
                    Assert.IsTrue(companyDetail.IsNewHLCompanyFinancialsDisplayedLV(), "Verify that the 'New' button is available on the Financials tab to add new HL Company Financials for System Admin.");
                    extentReports.CreateStepLogs("Passed", "'New' button is available on the Financials tab to add new HL Company Financials for System Admin.");

                    //TMT0076495 Verify that the System Admin can add HL Company Financials using the "New" button on the Financials tab of the Company Detail Page.
                    companyDetail.ClickNewButtonCompanyFinancialsLV();
                    string valYarExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Financials", 2, 1);
                    string valSourceExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Financials", 2, 2);
                    string valRevenueExl= ReadExcelData.ReadDataMultipleRows(excelPath, "Financials", 2, 3);
                    string valEBITDAExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Financials", 2, 4);
                    string valDataSourceExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Financials", 2, 5);
                    msgSuccess=companyDetail.AddNewHLCompanyFinancialLV(valYarExl,valSourceExl,valRevenueExl,valEBITDAExl, valDataSourceExl);
                    companyHLFinancialName=companyDetail.GetHLFinancialNameLV();
                    extentReports.CreateStepLogs("Passed", "HL Company Financial Created with success message: "+msgSuccess);
                    randomPages.CloseActiveTab(companyHLFinancialName);

                    //TMT0076497 Verify that the System Admin can update HL Company Financials using the "Edit" button on the Financials tab of the Company Detail Page
                    //TMT0076499 Verify that the most recent Year and Date information is updated on the Company's Annual Financials
                    valRevenueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Financials", 2, 6);
                    string updatedAsOfDate = companyDetail.EditHLFinancialRecordLV(companyHLFinancialName, valRevenueExl);                    
                    string updatedRevenue= companyDetail.GetHLFinancialRecordRevenueLV(companyHLFinancialName);
                    Assert.AreEqual(valRevenueExl, updatedRevenue);
                    extentReports.CreateStepLogs("Passed", " New HL Company Financials: "+ companyHLFinancialName+" Revenue is updated");
                    string latestAsOfDate= companyDetail.GetHLFinancialRecordAsOfDateLV(companyHLFinancialName);
                    Assert.AreEqual(updatedAsOfDate, latestAsOfDate);
                    extentReports.CreateStepLogs("Passed", " New HL Company Financials: "+ companyHLFinancialName+" As Of Date is updated");

                    //TMT0076501 Verify that clicking "Delete" will delete the HL Company Financials and give a success message on the screen.
                    msgSuccess= companyDetail.DeleteHLFinancialRecordLV(tabNameExl,companyHLFinancialName);
                    extentReports.CreateStepLogs("Passed", msgSuccess);
                    randomPages.CloseActiveTab(companyNameExl);

                }

                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "System Admin: " + appNameExl + " logged out");
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
                
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.ClickLogoutFromLightningView();
                //valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                homePage.SearchUserByGlobalSearchN(valAdminUser);
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valAdminUser), true);
                extentReports.CreateStepLogs("Passed", "System Admin User: " + valAdminUser + " logged in on Lightning View");

                //appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                for (int row = 2; row <= rowCompanyName; row++)
                {
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 1);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Passed", "Company: " + companyNameExl + " found and selected");

                    try
                    {
                        companyDetail.ClickCompanyDetailPageTabLV(tabNameExl);
                        msgSuccess = companyDetail.DeleteHLFinancialRecordLV(tabNameExl, companyHLFinancialName);
                        extentReports.CreateStepLogs("Passed", "HL Company Financials: " + companyHLFinancialName + " Deleted with followed by message: " + msgSuccess);

                    }
                    catch (Exception ex)
                    {

                    }

                    randomPages.CloseActiveTab(companyNameExl);

                }
                driver.Quit();
            }
        }
    }
}
