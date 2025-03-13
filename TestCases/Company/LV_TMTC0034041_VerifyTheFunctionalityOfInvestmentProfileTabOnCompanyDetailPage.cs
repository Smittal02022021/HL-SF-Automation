using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using AventStack.ExtentReports.Gherkin.Model;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTC0034041_VerifyTheFunctionalityOfInvestmentProfileTabOnCompanyDetailPage:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        HomeMainPage homePage = new HomeMainPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTC0034041 = "TMTC0034027_VerifyTheFunctionalityOfFinancialsTabOnCompanyDetailPage";

        private int rowCompanyName;
        private string companyNameExl;
        private string excelPath;
        private string valUser;
        private string tabNameExl;
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
        //TMT0076538	Verify the availability of the "Investment Profile" tab on the Company detail page
        //TMT0076540	Verify that the "Investment Profile" tab lists the Company Sectors and Investment Preferences of the Company
        //TMT0076542	Verify that the "New" button is available on the Company Sectors and Investment Preferences sections of the Investment Profile tab
        //TMT0076544	Verify that the user can add Company Sector using the "New" button on the Investment Profile Page followed by a success message and redirect the user to the Company Sector detail page
        //TMT0076546	Verify that the user can update the Company Sector using the "Edit" button on the Company Sector list
        //TMT0076548    Verify the "Delete" functionality of the Company Sector on the Investment Profile of the Company
        //TMT0076550	Verify that clicking the "New" button of the Investment preferences redirects the user to the New Investment Preferences page
        [Test]
        public void VerifyTheFunctionalityOfActivitiesReportTabOnCompanyDetailPageLV()
        {
            try
            {
                //Get path of Test data file
                excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0034041;
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
                    extentReports.CreateStepLogs("Passed", "Company: " + companyNameExl + " found and selected");

                    //TMT0076538	Verify the availability of the "Investment Profile" tab on the Company detail page
                    tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TabName", 4, 1);
                    Assert.IsTrue(companyDetail.IsCompanyDetailPageTabPresentLV(tabNameExl), "Verify the availability of the '" + tabNameExl + "' tab on the Company detail page");
                    extentReports.CreateStepLogs("Passed", tabNameExl + " tab available on the Company detail page");

                    //TMT0076542	Verify that the "New" button is available on the Company Sectors and Investment Preferences sections of the Investment Profile tab
                    companyDetail.ClickCompanyDetailPageTabLV(tabNameExl);
                    Assert.IsTrue(companyDetail.IsCompanySectorsNewButtonDisplayedLV(), "Verify that the 'New' button is available on the Company Sectors and Investment Preferences sections of the Investment Profile tab");
                    extentReports.CreateStepLogs("Passed"," '"+ tabNameExl+ "' 'New' button is available on the Company Sectors and Investment Preferences sections of the Investment Profile tab");
                    
                    //Test Case number not present
                    Assert.IsTrue(companyDetail.IsInvestmentPreferencesNewButtonDisplayedLV(), "Verify that the 'New' button is available");
                    extentReports.CreateStepLogs("Passed", "'New' button is available");

                    //TMT0076550	Verify that clicking the "New" button of the Investment preferences redirects the user to the New Investment Preferences page
                    companyDetail.ClickInvestmentPrefrenceNewButtonLV();
                    bool recordsFound= companyDetail.AreInvestmentPreferenceTypesPresentLV(fileTMTC0034041);
                    Assert.IsTrue(recordsFound, "Verify that clicking the 'New' button of the Investment preferences redirects the user to the New Investment Preferences page with all record types present");
                    extentReports.CreateStepLogs("Passed", "All Investment preferences Record Types are present ");

                    //TMT0076544	Verify that the user can add Company Sector using the "New" button on the Investment Profile Page followed by a success message and redirect the user to the Company Sector detail page
                    string sectorExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Financials", 2, 7);
                    string msgSuccess= companyDetail.AddNewCompanySectorLV(sectorExl);
                    string companySector = companyDetail.GetCompanySectorLV();
                    extentReports.CreateStepLogs("Passed", "New Company Sector added under Investment Profile tab with message: "+ msgSuccess);
                    
                    randomPages.CloseActiveTab(companySector);
                    //TMT0076540 Verify that the "Investment Profile" tab lists the Company Sectors and Investment Preferences of the Company
                    Assert.IsTrue(companyDetail.IsCompanySectorDisplayedLV(companySector), "Verify that the 'Investment Profile' tab lists the Company Sectors");
                    extentReports.CreateStepLogs("Passed", " Company Sectors: "+ companySector+" present under Investment Profile tab");
                    
                    //TMT0076546	Verify that the user can update the Company Sector using the "Edit" button on the Company Sector list
                    string newSectorExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Financials", 2, 8);
                    msgSuccess = companyDetail.UpdateCompanySectorRecordLV(companySector, newSectorExl);
                    extentReports.CreateStepLogs("Passed", "Company Sector: "+ companySector+ " updated under Investment Profile tab with new Sector Category:" +newSectorExl+" followed by message: " + msgSuccess);

                    //TMT0076548	Verify the "Delete" functionality of the Company Sector on the Investment Profile of the Company
                    msgSuccess = companyDetail.DeleteCompanySectorRecordLV(tabNameExl, companySector);
                    extentReports.CreateStepLogs("Passed", "Company Sector: " + companySector + " Deleted from list under Investment Profile tab followed by message: " + msgSuccess);

                    randomPages.CloseActiveTab(companyNameExl);

                }
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");

            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                randomPages.CloseActiveTab(companyNameExl);
                usersLogin.ClickLogoutFromLightningView();
            }
        }
    }
}