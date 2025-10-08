using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.Company;
using AventStack.ExtentReports.Gherkin.Model;
using Microsoft.Office.Interop.Excel;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTC0034072_VerifyTheFunctionalityOftheFSFundTabOnTheCompanyDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        HomeMainPage homePage = new HomeMainPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        RandomPages randomPages = new RandomPages();
        CompanyCreatePage createCompany = new CompanyCreatePage();

        public static string fileTMTC0034058 = "LV_TMTC0034072_VerifyTheFunctionalityOftheFSFundTabOnTheCompanyDetailPage";

        private int rowCompanyName;
        private string companyNameExl;
        private string excelPath;
        private string valUser;
        private string user;
        private string appNameExl;
        private string appName;
        private string moduleNameExl;
        private string tabNameExl;
        private string btnNameExl;
        private string newCompanyName;
        private string valAdminUser;
        private string msgBubble;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMT0076623 Verify the availability of the "FS Fund" tab on the Company detail page.
        //TMT0076650 Verify that the "FS Fund" tab lists all the FS Fund details related to the company
        //TMT0076653 Verify that the "New" button is available on the FS Fund tab
        //TMT0076655 Verify that the user is able to add FS Fund using the "New" button on the FS Fund Page followed by a success message and redirects user to the fund detail page
        //TMT0076657 Verify that the user can update the FS Fund using the "Edit" button on the fund list
        [Test]
        public void VerifyTheFunctionalityOftheFSFundTabOnTheCompanyDetailPageLV()
        {
            try
            {
                //Get path of Test data file
                excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0034058;
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
                    // Create a  company
                    createCompany.CreateNewCompanyLV(fileTMTC0034058, row);
                    extentReports.CreateStepLogs("Info", " New Company Created ");
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    newCompanyName = companyDetail.GetCompanyNameHeaderLV();
                    Assert.IsTrue(newCompanyName.Contains(companyNameExl));
                    extentReports.CreateStepLogs("Passed", valRecordTypeExl + " Company created and name :" + newCompanyName + " displayed on Company Detail page Header ");

                    //TMT0076623	Verify the availability of the "FS Fund" tab on the Company detail page.
                    tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TabName", 2, 1);
                    Assert.IsTrue(companyDetail.IsCompanyDetailPageTabPresentLV(tabNameExl), "Verify the availability of the '" + tabNameExl + "' tab on the Company detail page");
                    extentReports.CreateStepLogs("Passed", tabNameExl + " tab available on the Company detail page");

                    //TMT0076653	Verify that the "New" button is available on the FS Fund tab
                    companyDetail.ClickCompanyDetailPageTabLV(tabNameExl);
                    extentReports.CreateStepLogs("Passed", tabNameExl + " tab Clicked/selected on the Company detail page");
                    Assert.IsTrue(companyDetail.IsNewFSFundsButtonDisplayedLV(), "Verify that the 'Add Files' button is available on the Files tab to add files to the company.");
                    extentReports.CreateStepLogs("Passed", "'Add Files' button is available on the Files tab to add files to the company.");

                    //TMT0076655	Verify that the user is able to add FS Fund using the "New" button on the FS Fund Page followed by a success message and redirects user to the fund detail page
                    companyDetail.ClickFSFundsNewButtonDisplayedLV();
                    //9. Verify that the Company field is pre-filled with the selected Company name. 
                    string nameFSFundsCompany = companyDetail.GetNewFundsCompanyNameLV();
                    Assert.AreEqual(newCompanyName, nameFSFundsCompany, "Verify that the Company field is pre-filled with the selected Company name. ");
                    extentReports.CreateStepLogs("Passed", "The Company field is pre-filled with the selected Company name on New FS Funds form.");

                    companyDetail.AddNewCompanyFSFundsLV(fileTMTC0034058);
                    msgBubble = randomPages.GetPopUpMessagelV();
                    Assert.IsTrue(msgBubble.Contains("was created"));
                    extentReports.CreateStepLogs("Passed", " New FS Funds added followed by the success message: " + msgBubble);

                    string valFSFundsName = companyDetail.GetFSFundsNameLV();
                    extentReports.CreateStepLogs("Passed", " New FS Funds: " + valFSFundsName + " added");
                    extentReports.CreateStepLogs("Passed", " FS Funds updated followed by the success message: " + msgBubble);
                    randomPages.CloseActiveTab(valFSFundsName);

                    //TMT0076650	Verify that the "FS Fund" tab lists all the FS Fund details related to the company
                    Assert.IsTrue(companyDetail.IsFSFundRecordDisplayedLV(valFSFundsName), "Verify that the 'FS Fund' tab lists all the FS Fund details related to the company");
                    extentReports.CreateStepLogs("Passed", "'FS Fund' tab lists all the FS Fund details related to the company");

                    //TMT0076657	Verify that the user can update the FS Fund using the "Edit" button on the fund list
                    string recordAction = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 3, 1);
                    companyDetail.FundsRecordMoreActionsLV(valFSFundsName, recordAction);
                    companyDetail.EditCompanyFSFundsLV(fileTMTC0034058);
                    msgBubble = randomPages.GetPopUpMessagelV();
                    Assert.IsTrue(msgBubble.Contains("was saved"));

                    randomPages.CloseActiveTab(companyNameExl);
                }
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "Fin user: " + valUser + " logged out");

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
                for(int row = 2; row <= rowCompanyName; row++)
                {
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Passed", companyNameExl + " found and selected");
                    //companyDetail.ClickCompanyDetailPageTabLV(tabNameExl);
                    companyDetail.DeleteCompanyLV();
                    extentReports.CreateStepLogs("Passed", companyNameExl + " Company Deleted Successfully");
                }
                homePageLV.LogoutFromSFLightningAsApprover();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch(Exception ex)
            {
                extentReports.CreateExceptionLog(ex.Message);
                //Assert.IsTrue(msgBubble.Contains("File was deleted"));
                homePageLV.LogoutFromSFLightningAsApprover();
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
                for(int row = 2; row <= rowCompanyName; row++)
                {
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Passed", companyNameExl + " found and selected");
                    //randomPages.CloseActiveTab("Files");
                    //companyDetail.ClickCompanyDetailPageTabLV(tabNameExl);
                    //companyDetail.UploadedFileMoreActionsLV(fileName, fileAction);
                    //randomPages.GetPopUpMessagelV();
                    //extentReports.CreateStepLogs("Passed", "Uploaded file" + randomPages.GetPopUpMessagelV());

                    companyDetail.DeleteCompanyLV();
                    extentReports.CreateStepLogs("Passed", companyNameExl + " Company Deleted Successfully");
                }

                driver.Quit();
            }
        }
    }
}