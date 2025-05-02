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
                extentReports.CreateStepLogs("Passed", "Admin User is able to login into SF");

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
                    extentReports.CreateStepLogs("Info", "User " + caoUser + " details are displayed ");

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

                    //TMTI0115244  = Verify that a hover icon for Potential Round Trip field gives expected text
                    string toolTipTextExl = ReadExcelData.ReadData(excelPath, "Tooltip", 1);
                    Assert.IsTrue(companyDetailsPage.VerifyHoverTextForPotentialRoundTripField(toolTipTextExl));
                    extentReports.CreateStepLogs("Passed", "Tooltip text displayed for Potential Round Trip field is: " + toolTipTextExl);

                    //TMTI0115250 = Verify that on the Account Page, fields - Potential Round Trip, Round Trip Engagement & Round Trip Comment are editable
                    Assert.IsTrue(companyDetailsPage.VerifyOnlyExpectedRoundTripFieldsAreEditable());
                    extentReports.CreateStepLogs("Passed", "Only Potential Round Trip, Round Trip Engagement and Round Trip Comment fields are editable. ");

                    //TMTI0115254 = Verify that if the user selects "No" AND 'Company' is an OpCo, No Warning message will appear on the screen.
                    //TMTI0115257 = Verify that if the user selects "No" AND 'Company' is NOT OpCo, No Warning message will appear on the screen.

                    Assert.IsTrue(companyDetailsPage.VerifyNoWarningMsgIsDisplayedIfUserSelectsNoInPotentialRoundTripField());
                    extentReports.CreateStepLogs("Passed", "No warning message is displayed if user selects 'No' in Potential Round Trip field for company type: " + valRecordTypeExl + ".");

                    if(valRecordTypeExl=="Operating Company")
                    {
                        //TMTI0115261 = Verify that if the user selects "Yes" AND 'Company' is an OpCo, No Warning message will appear on the screen.
                        //Assert.IsTrue(companyDetailsPage.VerifyNoWarningMsgIsDisplayedIfUserSelectsYesInPotentialRoundTripField());
                        //extentReports.CreateStepLogs("Passed", "No warning message is displayed if user selects 'Yes' in Potential Round Trip field for company type: " + valRecordTypeExl + ".");
                    }
                    else
                    {
                        //TMTI0115263 = Verify that if user selects "Yes" AND 'Company' is NOT OpCo, then warning message  will appear on the screen
                        Assert.IsTrue(companyDetailsPage.VerifyWarningMsgIsDisplayedIfUserSelectsYesInPotentialRoundTripField());
                        extentReports.CreateStepLogs("Passed", "Warning message is displayed if user selects 'Yes' in Potential Round Trip field for company type: " + valRecordTypeExl + ".");

                        string msg = ReadExcelData.ReadData(excelPath, "Warning", 1);
                        Assert.IsTrue(companyDetailsPage.VerifyWarningMsg(msg));
                        extentReports.CreateStepLogs("Passed", "Expected warning message is displayed : " + msg);

                        string fReason = ReadExcelData.ReadData(excelPath, "FlagReason", 1);
                        string fReasonComment = ReadExcelData.ReadData(excelPath, "FlagReason", 2);

                        Assert.IsTrue(companyDetailsPage.VerifyFlagDetailsAreUpdatedForTheCompany(fReason, fReasonComment, caoUser));
                        extentReports.CreateStepLogs("Passed", "Flag details are updated for the company. \r\n Flag Reason: " + fReason + "\r\n Flag Reason Comment: " + fReasonComment + "\r\n Flag Reason Change By: " + caoUser + ".");

                        //TMTI0115265 = Verify that all the flag updates are tracked on the "Audit Records Report - Companies"
                        driver.Navigate().GoToUrl("https://hl--test.sandbox.lightning.force.com/lightning/r/Report/00OOx00000927oDMAQ/edit?0.source=alohaHeader");
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Report Builder | Salesforce"), true);

                        Assert.IsTrue(companyDetailsPage.VerifyFlagDetailsAreUpdatedOnTheCompanyAuditRecordsReport(valCompanyNameExl, fReason, fReasonComment, caoUser));
                        extentReports.CreateStepLogs("Passed", "Flag details are updated as expected on the company audit records report.");
                    }

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
                    extentReports.CreateStepLogs("Passed", "Admin User lands on the new company detail page. ");

                    //Delete company
                    companyDetail.DeleteCompanyLV();
                    extentReports.CreateStepLogs("Info", "Created company is deleted successfully by Admin user.");

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