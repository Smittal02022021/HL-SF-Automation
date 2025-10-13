using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTC0034044_VerifyTheAvailabilityOfTheLocationsTabOnTheCompanyDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        HomeMainPage homePage = new HomeMainPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTC0034044 = "TMTC0034027_VerifyTheFunctionalityOfFinancialsTabOnCompanyDetailPage";

        private int rowCompanyName;
        private string companyNameExl;
        private string excelPath;
        private string valUser;
        private string valAdminUser;
        private string user;
        private string appNameExl;
        private string appName;
        private string moduleNameExl;
        private string tabNameExl;
        private string msgSuccess;
        private string addLocationID;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMT0076553 Verify the availability of the "Locations" tab on the Company detail page
        //TMT0076557 Verify that the "Locations" tab lists all the Addresses where the company is located
        //TMT0076559 Verify that the "New" button is available on the Locations to add a new address of the company.
        //TMT0076561 Verify that the user can add a New Address using the "New" button on the Locations tab followed by a success message and redirect the user to a detail page
        //TMT0076563 Verify that the user can update the added address using the "Edit" button given corresponding to each Address
        //TMT0076565 Verify the "Delete" functionality of the added addresses on the Locations tab.

        [Test]
        public void VerifyTheAvailabilityOfTheLocationsTabOnTheCompanyDetailPageLV()
        {
            try
            {
                //Get path of Test data file
                excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0034044;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Companies");
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
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 1);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Passed", "Company: " + companyNameExl + " found and selected");

                    //TMT0076553	Verify the availability of the "Locations" tab on the Company detail page
                    tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TabName", 5, 1);
                    Assert.IsTrue(companyDetail.IsCompanyDetailPageTabPresentLV(tabNameExl), "Verify the availability of the '" + tabNameExl + "' tab on the Company detail page");
                    extentReports.CreateStepLogs("Passed", tabNameExl + " tab available on the Company detail page");

                    //TMT0076559	Verify that the "New" button is available on the Locations to add a new address of the company.
                    companyDetail.ClickCompanyDetailPageTabLV(tabNameExl);
                    Assert.IsTrue(companyDetail.IsCompanyAddLocationNewButtonDisplayedLV(), "Verify that the 'New' button is available on the Locations to add a new address of the company");
                    extentReports.CreateStepLogs("Passed", " '" + tabNameExl + "' tab lists all related Activities");

                    //TMT0076557	Verify that the "Locations" tab lists all the Addresses where the company is located
                    Assert.IsTrue(companyDetail.IsLocationAddressRecordPresentLV(), "Verify that the 'Locations' tab lists all the Addresses where the company is located");
                    extentReports.CreateStepLogs("Passed", " '" + tabNameExl + "' tab lists all the Addresses where the company is located");

                    //TMT0076561	Verify that the user can add a New Address using the "New" button on the Locations tab followed by a success message and redirect the user to a detail page
                    string addressTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 3);
                    string addressExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 5);

                    msgSuccess = companyDetail.AddNewCompanyLocationLV(addressTypeExl, addressExl);
                    extentReports.CreateStepLogs("Passed", " New Address added with Success message: " + msgSuccess);
                    addLocationID = companyDetail.GetAddLocationIDLV();
                    randomPages.CloseActiveTab(addLocationID);

                    bool IsAddLocRecordDisplayed = companyDetail.IsAddLocationRecordDisplayedLV(addLocationID);
                    Assert.IsTrue(IsAddLocRecordDisplayed, "Verify New Address location is displayed under Address List");
                    extentReports.CreateStepLogs("Passed", "New Address location record is displayed under Address List");

                    //TMT0076563	Verify that the user can update the added address using the "Edit" button given corresponding to each Address
                    string updatedaddressTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 4);
                    companyDetail.SelectAddLocationRecordLV(addLocationID);
                    companyDetail.UpdateAddLocationLV(updatedaddressTypeExl);
                    extentReports.CreateStepLogs("Passed", "Address location: " + addLocationID + "  record is updated");

                    randomPages.CloseActiveTab(companyNameExl);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");

                    //System Admin actions    
                    valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
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
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                    //companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Passed", "Company:" + companyNameExl + " found and selected");
                    //TMT0076565	Verify the "Delete" functionality of the added addresses on the Locations tab.
                    companyDetail.ClickCompanyDetailPageTabLV(tabNameExl);
                    companyDetail.SelectAddLocationRecordLV(addLocationID);
                    companyDetail.DeleteAddLocationLV();
                    extentReports.CreateStepLogs("Passed", "Location deleted from Company " + companyNameExl);
                    randomPages.CloseActiveTab(companyNameExl);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Passed", "System Administrator User: " + valAdminUser + " logged out");
                }
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }

            catch(Exception ex)
            {
                extentReports.CreateExceptionLog(ex.Message);
                randomPages.CloseActiveTab(companyNameExl);
                homePageLV.LogoutFromSFLightningAsApprover();
                valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
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
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Companies");
                for(int row = 2; row <= rowCompanyName; row++)
                {
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 1);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);

                    companyDetail.ClickCompanyDetailPageTabLV(tabNameExl);
                    try
                    {
                        companyDetail.SelectAddLocationRecordLV(addLocationID);
                        companyDetail.DeleteAddLocationLV();
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