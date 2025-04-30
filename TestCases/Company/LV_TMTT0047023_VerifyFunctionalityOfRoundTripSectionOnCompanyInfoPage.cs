using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Company;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using NUnit.Framework;
using SF_Automation.TestData;
using System;

namespace SalesForce_Project.TestCases.Company
{
    class LV_TMTT0047023_VerifyFunctionalityOfRoundTripSectionOnCompanyInfoPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();

        LVHomePage lvHomePage = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();
        LV_CompanyDetailsPage companyDetailsPage = new LV_CompanyDetailsPage();

        public static string fileT47023 = "LV_TMTT0047023_VerifyFunctionalityOfRoundTripSectionOnCompanyInfoPage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
       
        [Test]
        public void VerifyFunctionalityOfRoundTripSectionOnCompanyInfoPage()
        {
            try
            {
                ///Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT47023;
                Console.WriteLine(excelPath);

                string caoUser = ReadExcelData.ReadData(excelPath, "Users", 1);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                //Validate user logged in
                Assert.AreEqual(driver.Url.Contains("lightning"), true);
                extentReports.CreateLog("Admin User is able to login into SF");

                //Select HL Banker app
                try
                {
                    lvHomePage.SelectAppLV("HL Banker");
                }
                catch(Exception)
                {
                    lvHomePage.SelectAppLV1("HL Banker");
                }

                //Navigate to Companies page
                lvHomePage.NavigateToAnItemFromHLBankerDropdown("Companies");
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Recently Viewed | Companies | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User navigated to companies list page. ");

                int totalCompanies = ReadExcelData.GetRowCount(excelPath, "Company");
                for(int row = 2; row <= totalCompanies; row++)
                {
                    //Click New button to create new company
                    companyHome.ClickNewButton();
                    extentReports.CreateStepLogs("Info", "New button is clicked on companies list page. ");

                    //Select company record type
                    string valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    companySelectRecord.SelectCompanyRecordTypeAndClickNextLV(valRecordTypeExl);

                    string createCompanyPage = createCompany.GetCreateCompanyPageHeaderLV();
                    Assert.IsTrue(createCompanyPage.Contains(valRecordTypeExl));
                    extentReports.CreateStepLogs("Passed", "Page with heading: " + createCompanyPage + " is displayed upon selecting company record type.");

                    //Create a new company
                    createCompany.CreateNewCompanyLV(fileT47023, row);
                    string valCompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    extentReports.CreateStepLogs("Info", "New Company: " + valCompanyNameExl + " is Created.");

                    //Validate company name on company detail heading
                    string companyName = companyDetail.GetCompanyNameHeaderLV();
                    Assert.IsTrue(companyName.Contains(valCompanyNameExl));
                    extentReports.CreateStepLogs("Passed", "Company name: : " + companyName + " is displayed on Company Detail page Header upon adding new company.");

                    //Validate company type value
                    string companyType = companyDetail.GetCompanyTypeLV();
                    Assert.AreEqual(valRecordTypeExl, companyType);
                    extentReports.CreateStepLogs("Passed", "Company Type: " + companyType + " in add company page matches on company details page.");

                    //Search CAO user by global search
                    lvHomePage.SearchUserFromMainSearch(caoUser);

                    //Verify searched user
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, caoUser + " | Salesforce"), true);
                    extentReports.CreateLog("User " + caoUser + " details are displayed ");

                    //Login as CAO user
                    lvHomePage.UserLogin();

                    //Switch to lightning view
                    if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                    {
                        homePage.SwitchToLightningView();
                        extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                    }

                    Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(caoUser));
                    extentReports.CreateStepLogs("Passed", "CAO User: " + caoUser + " is able to login into lightning view. ");

                    //Search created company
                    lvHomePage.SearchCompanyFromMainSearch(valCompanyNameExl);
                    Assert.IsTrue(companyDetailsPage.VerifyUserLandsOnCorrectCompanyDetailPage(companyName));
                    extentReports.CreateStepLogs("Passed", "User lands on the new company detail page. ");

                    //TMTI0115237 = Verify that the "Round Trip" section is available on the Company Detail Page.
                    Assert.IsTrue(companyDetailsPage.VerifyRoundTripSectionIsDisplayed());
                    extentReports.CreateStepLogs("Passed", "Round Trip section is displayed on the Company detail page. ");

                    //TMTI0115240 = Verify the fields under the Round Trip section
                    Assert.IsTrue(companyDetailsPage.VerifyRoundTripSectionFields(fileT47023));
                    extentReports.CreateStepLogs("Passed", "Fields displayed under round trip section are : Potential Round Trip, Round Trip Engagement, Round Trip Comment and Potential Round Trip Last Modified Date. ");

                    //Logout from SF Lightning View
                    lvHomePage.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "CAO User Logged Out from SF Lightning View. ");

                    //Select HL Banker app
                    try
                    {
                        lvHomePage.SelectAppLV("HL Banker");
                    }
                    catch(Exception)
                    {
                        lvHomePage.SelectAppLV1("HL Banker");
                    }

                    //Search created company
                    lvHomePage.SearchCompanyFromMainSearch(valCompanyNameExl);
                    Assert.IsTrue(companyDetailsPage.VerifyUserLandsOnCorrectCompanyDetailPage(companyName));
                    extentReports.CreateStepLogs("Passed", "User lands on the new company detail page. ");

                    //Delete company
                    companyDetail.DeleteCompanyLV();
                    extentReports.CreateLog("Created company is deleted successfully ");

                    //Close Tab
                    companyDetailsPage.CloseTab(valCompanyNameExl);
                }

                //TC - End
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "Admin User Logged Out from SF Lightning View. ");

                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.Quit();
            }
        }
    }
}