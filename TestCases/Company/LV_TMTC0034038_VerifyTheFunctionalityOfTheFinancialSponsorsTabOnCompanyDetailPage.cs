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

namespace SalesForce_Project.TestCases.Company
{
    class LV_TMTC0034038_VerifyTheFunctionalityOfTheFinancialSponsorsTabOnCompanyDetailPage:BaseClass
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

        public static string fileTMTC0034038 = "LV_TMTC0034038_VerifyTheFunctionalityOfTheFinancialSponsorsTabOnCompanyDetailPage";

        private int rowCompanyName;
        private string newCompanyName;
        private string companyNameExl;
        private string excelPath;
        private string valAdminUser;
        private string user;
        private string appNameExl;
        private string appName;
        private string moduleNameExl;
        private string btnNameExl;
        private string tabNameExl;
        private string msgBubble;
        private string investmentNumber;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTheFunctionalityOfTheFinancialSponsorsTabOnCompanyDetailPageLV()
        {
            try
            {
                //Get path of Test data file
                excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0034038;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                homePage.SearchUserByGlobalSearchN(valAdminUser);
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
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 2, 1);
                    companyHome.ClickButtonCompanyHomePageLV(btnNameExl);

                    string valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    companySelectRecord.SelectCompanyRecordTypeAndClickNextLV(valRecordTypeExl);
                    // Select company record type                    
                    string createCompanyPage = createCompany.GetCreateCompanyPageHeaderLV();
                    Assert.IsTrue(createCompanyPage.Contains("New Company"));
                    extentReports.CreateStepLogs("Passed", "Page with heading: " + createCompanyPage + " is displayed upon selecting company record type ");
                    // Create a  company
                    createCompany.CreateNewCompanyLV(fileTMTC0034038, row);
                    newCompanyName = companyDetail.GetCompanyNameHeaderLV();
                    extentReports.CreateStepLogs("Info", " New Company : "+newCompanyName+" Created ");
                    //Validate company detail heading
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    newCompanyName = companyDetail.GetCompanyNameHeaderLV();
                    Assert.IsTrue(newCompanyName.Contains(companyNameExl));
                    extentReports.CreateStepLogs("Passed", valRecordTypeExl + " Company created and name :" + newCompanyName + " displayed on Company Detail page Header ");

                    //TMT0076521 Verify the availability of the "Financial Sponsors" tab on the Company detail page.
                    tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TabName", row, 1);
                    Assert.IsTrue(companyDetail.IsCompanyDetailPageTabPresentLV(tabNameExl), "Verify the availability of the '" + tabNameExl + "' tab on the Company detail page");
                    extentReports.CreateStepLogs("Passed", tabNameExl + " tab available on the Company detail page");

                    //TMT0076527 Verify that the "New" button is available for the System Admin on the Financial Sponsor tab to add Current and Previous Sponsor Companies.
                    companyDetail.ClickCompanyDetailPageTabLV(tabNameExl);
                    Assert.IsTrue(companyDetail.IsNewFinancialSPButtonDisplayedLV(), "Verify that the 'New' button is available for the System Admin on the Financial Sponsor tab to add Current and Previous Sponsor Companies");
                    extentReports.CreateStepLogs("Passed", "'New' button is available on the company.");

                    //TMT0076529 Verify that the System Admin can add a Sponsor Company using the "New" button on the Financial Sponsor tab of the Company Detail Page
                    int rowCount = ReadExcelData.GetRowCount(excelPath, "Company");

                    for (int invRow = 2; invRow <= rowCount; invRow++)
                    {
                        companyDetail.ClickFinancialSponsorsNewButtonDisplayedLV();
                        string investmentStatusExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Investment", invRow, 1);
                        companyDetail.AddNewCompanyFinancialsSponsorsLV(investmentStatusExl);
                        msgBubble = randomPages.GetPopUpMessagelV();
                        Assert.IsTrue(msgBubble.Contains("was created"));
                        extentReports.CreateStepLogs("Passed", " New FS Sponsors added followed by the success message: " + msgBubble);

                        investmentNumber = companyDetail.GetFinancialSponsorsNameLV();
                        randomPages.CloseActiveTab(investmentNumber);
                        //TMT0076523	Verify that the "Financial Sponsor" tab lists all the Current and Previous Sponsors of the Company.
                        Assert.IsTrue(companyDetail.IsFinancialSPRecordDisplayedLV(investmentNumber));
                        extentReports.CreateStepLogs("Passed", " Added Investment " + investmentNumber + " is present in Record List under "+investmentStatusExl+" Sponsors");

                        //TMT0076531 Verify that the System Admin can update the Sponsor Company using the "Edit" button on the Investment list
                        string amountExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Investment", invRow, 2);
                        companyDetail.EditFinancialSPRecord(investmentNumber, amountExl);
                        msgBubble = randomPages.GetPopUpMessagelV();
                        Assert.IsTrue(msgBubble.Contains("was saved"));
                        extentReports.CreateStepLogs("Passed", " Investment Record: " + investmentNumber + "  updated followed by the success message: " + msgBubble);

                        //TMT0076533 Verify that clicking "Delete" will delete the Investment List and give a success message on the screen.


                        randomPages.CloseActiveTab(investmentNumber);                        
                    }
                    randomPages.CloseActiveTab(newCompanyName);
                }
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                randomPages.CloseActiveTab(investmentNumber);
                randomPages.CloseActiveTab(newCompanyName);
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
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Passed", "Company: " + companyNameExl + " found and selected");
                    try
                    {
                        randomPages.CloseActiveTab(investmentNumber);
                        companyDetail.DeleteCompanyLV();
                       
                    }
                    catch (Exception ex)
                    {

                    }

                   // randomPages.CloseActiveTab(companyNameExl);

                }
                driver.Quit();
            }
        }
    }
}