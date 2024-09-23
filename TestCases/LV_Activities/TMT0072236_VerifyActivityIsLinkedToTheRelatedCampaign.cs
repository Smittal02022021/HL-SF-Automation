using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;
using SF_Automation.Pages.Activities;
using SF_Automation.Pages.Contact;

namespace SF_Automation.TestCases.LV_Activities
{
    class TMT0072236_VerifyActivityIsLinkedToTheRelatedCampaign : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();
        LV_ContactsActivityListPage LV_ContactsActivityList = new LV_ContactsActivityListPage();
        LV_ContactsActivityDetailPage lV_ContactsActivityDetailPage = new LV_ContactsActivityDetailPage();

        LV_AddActivity addActivity = new LV_AddActivity();
        CampaignHomePage campaignHome = new CampaignHomePage();
        NewCampaignPage newCampaign = new NewCampaignPage();

        public static string fileTMTC0032668 = "TMTC0032668_VerifyActivityIsLinkedToTheRelatedCampaign";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyActivityIsLinkedToTheRelatedCampaign()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0032668;
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                string extContactName = ReadExcelData.ReadData(excelPath, "Contact", 1);
                string relatedCompany = ReadExcelData.ReadData(excelPath, "Contact", 2);
                string tabName = ReadExcelData.ReadData(excelPath, "Contact", 3);
                string campRecordType = ReadExcelData.ReadData(excelPath, "Campaign", 1);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login. ");

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Passed", "Admin User is able to login into lightning view. ");
                }

                //Navigate to Campaigns page
                lvHomePage.NavigateToAnItemFromHLBankerDropdown("Campaigns");
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Recently Viewed | Campaigns | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User navigated to Campaigns list page. ");

                //Navigate to Select New Campaign Record Type page
                campaignHome.NavigateToNewCampaignPage();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Campaign | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User navigated to Select New Campaign Record Type page. ");

                //Select Campaign record type
                newCampaign.SelectCampaignType(campRecordType);
                extentReports.CreateStepLogs("Info", "Campaign Record Type : "+ campRecordType + " selected. ");

                //Create New Campaign
                newCampaign.CreateNewParentCampaign(fileTMTC0032668);
                Assert.IsTrue(newCampaign.VerifyIfNewCampaignIsCreatedSuccessfully(campRecordType));
                extentReports.CreateStepLogs("Passed", "Campaign with Record Type : "+ campRecordType + " created successfully. ");

                //Search external contact
                lvHomePage.SearchContactFromMainSearch(extContactName);
                Assert.IsTrue(lvContactDetails.VerifyUserLandedOnCorrectContactDetailsPage(extContactName));
                extentReports.CreateStepLogs("Passed", "User navigated to external contact details page. ");

                //Navigate to Activity tab
                lvContactDetails.NavigateToActivityTabInsideCFFinancialUser();
                Assert.IsTrue(LV_ContactsActivityList.VerifyUserLandsOnActivityTab());
                extentReports.CreateStepLogs("Passed", "User landed on the Activity tab of external contact. ");

                //TMT0072236 Verify that the Activity is linked to the related Campaign.

                int totalActivity = ReadExcelData.GetRowCount(excelPath, "Activity");

                string type = ReadExcelData.ReadData(excelPath, "Activity", 1);
                string subject = ReadExcelData.ReadData(excelPath, "Activity", 2);
                string companyDis = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 7);
                string oppDis = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 8);
                string campaign = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 9);

                //Create new activity
                int beforeCount = LV_ContactsActivityList.GetActivityCount();
                addActivity.CreateNewActivityWithCampaign(fileTMTC0032668);
                lvContactDetails.CloseTab("View Activity");

                Assert.IsTrue(LV_ContactsActivityList.VerifyCreatedActivityIsDisplayedUnderActivitiesList(beforeCount));
                extentReports.CreateStepLogs("Passed", "Activity created successfully with campaign: " + campaign  + " for call type: " + type);

                //Navigate to Activity Detail Page
                LV_ContactsActivityList.ViewActivityFromList(subject);
                extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");

                //Navigate to Campaign Detail from Activity Detail page
                Assert.IsTrue(lV_ContactsActivityDetailPage.NavigateToCampaignDetailPage(campaign));
                extentReports.CreateStepLogs("Passed", "User landed on the Campaign detail page. ");

                //Verify Activity Is Linked To Campaign
                
                //Switch Back to Classic View
                lvHomePage.SwitchBackToClassicView();

                //Logout from SF Classic View
                usersLogin.UserLogOut();
                extentReports.CreateStepLogs("Info", "Admin User Logged Out from SF Classic View. ");

                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
            }
        }
    }
}
