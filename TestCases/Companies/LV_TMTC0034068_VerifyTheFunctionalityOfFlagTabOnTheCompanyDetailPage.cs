using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using AventStack.ExtentReports.Gherkin.Model;
using Microsoft.Office.Interop.Excel;
using static SF_Automation.TestData.ReadJSONData;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTC0034068_VerifyTheFunctionalityOfFlagTabOnTheCompanyDetailPage:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        HomeMainPage homePage = new HomeMainPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTC0034068 = "TMTC0034027_VerifyTheFunctionalityOfFinancialsTabOnCompanyDetailPage";

        private int rowCompanyName;
        private string companyNameExl;
        private string excelPath;
        private string valUser;
        private string user;
        private string appNameExl;
        private string appName;
        private string moduleNameExl;
        private string tabNameExl;
        private string flagReasonExl;
        string flagReasonCommentExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        //TMT0076616 Verify the "Flag" tab availability on the Company detail page
        //TMT0076618 Verify that the "Flag" tab lists all the Flag Company details and the section for Data Hygiene
        //TMT0076620 Verify that clicking the pencil icon allows the user to update the Flag Reason and Flag Comment on the Flag Company

        [Test]
        public void VerifyTheFunctionalityOfFlagTabOnTheCompanyDetailPageLV()
        {
            try
            {
                //Get path of Test data file
                excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0034068;
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

                    //TMT0076616 Verify the "Flag" tab availability on the Company detail page
                    tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TabName", 8, 1);
                    Assert.IsTrue(companyDetail.IsCompanyDetailPageTabPresentLV(tabNameExl), "Verify the availability of the '" + tabNameExl + "' tab on the Company detail page");
                    extentReports.CreateStepLogs("Passed", tabNameExl + " tab available on the Company detail page");

                    //TMT0076618 Verify that the "Flag" tab lists all the Flag Company details and the section for Data Hygiene
                    companyDetail.ClickCompanyDetailPageTabLV(tabNameExl);
                    extentReports.CreateStepLogs("Passed", tabNameExl + " tab Clicked/selected on the Company detail page");

                    //TMT0076620 Verify that clicking the pencil icon allows the user to update the Flag Reason and Flag Comment on the Flag Company
                    flagReasonExl = ReadExcelData.ReadDataMultipleRows(excelPath, "FlagSection", 2, 1);
                    flagReasonCommentExl = ReadExcelData.ReadDataMultipleRows(excelPath, "FlagSection", 2, 2);
                    companyDetail.EditCompanyFlagLV(flagReasonExl, flagReasonCommentExl);

                    string flagReason=companyDetail.GetFlagReasonLV();
                    Assert.AreEqual(flagReasonExl, flagReason);
                    string flagReasonComment= companyDetail.GetFlagReasonCommentLV();                    
                    Assert.AreEqual(flagReasonCommentExl, flagReasonComment);
                    extentReports.CreateStepLogs("Passed", "Flag Reason:: "+ flagReason+" and Flag Comment:: "+ flagReasonComment+" updated on the Company detail page");

                    //Flag Reason and Comment Reverting back 
                    flagReasonExl = ReadExcelData.ReadDataMultipleRows(excelPath, "FlagSection", 3, 1);
                    flagReasonCommentExl = ReadExcelData.ReadDataMultipleRows(excelPath, "FlagSection", 3, 2);
                    companyDetail.EditCompanyFlagLV(flagReasonExl, flagReasonCommentExl);
                    extentReports.CreateStepLogs("Passed", "Flag Reason and Flag Comment reverted Company detail page");
                    randomPages.CloseActiveTab(companyNameExl);
                }
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "Fin user: " + valUser + " logged out");
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch (Exception ex)
            {
                extentReports.CreateExceptionLog(ex.Message);
                randomPages.CloseActiveTab(companyNameExl);                
                usersLogin.ClickLogoutFromLightningView();
                driver.Quit();
            }
        }
    }
}
