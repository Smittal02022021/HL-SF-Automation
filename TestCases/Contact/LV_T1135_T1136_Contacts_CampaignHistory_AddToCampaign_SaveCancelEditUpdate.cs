using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Activities;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Contact
{
    class LV_T1135_T1136_Contacts_CampaignHistory_AddToCampaign_SaveCancelEditUpdate : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        HomeMainPage homePage = new HomeMainPage();
        UsersLogin usersLogin = new UsersLogin();

        LVHomePage lvHomePage = new LVHomePage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();

        CampaignHomePage campaignHome = new CampaignHomePage();
        NewCampaignPage newCampaign = new NewCampaignPage();
        CampaignDetailPage campaignDetail = new CampaignDetailPage();

        LV_RecentlyViewedContactsPage lvRecentlyViewContact = new LV_RecentlyViewedContactsPage();
        LV_ContactsCreatePage lvCreateContact = new LV_ContactsCreatePage();
        CampaignMemberEditPage camMemEdit = new CampaignMemberEditPage();


        public static string fileTC1135_TC1136 = "T1135_T1136_Contacts_CampaignHistory_AddToCampaign_SaveCancelEditUpdate";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Contacts_CampaignHistory_AddToCampaign_SaveCancelEditUpdate()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1135_TC1136;
                string extContactFullName = ReadExcelData.ReadData(excelPath, "Contact", 6);
                string relatedCompany = ReadExcelData.ReadData(excelPath, "Contact", 1);
                string campRecordType = ReadExcelData.ReadData(excelPath, "Campaign", 1);
                string campName = ReadExcelData.ReadData(excelPath, "Campaign", 2);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

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
                extentReports.CreateLog("Admin User is able to login into SF Lightning View");

                //Select HL Banker app
                try
                {
                    lvHomePage.SelectAppLV("HL Banker");
                }
                catch(Exception)
                {
                    lvHomePage.SelectAppLV1("HL Banker");
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
                extentReports.CreateStepLogs("Info", "Campaign Record Type : " + campRecordType + " selected. ");

                //Create New Campaign
                newCampaign.CreateNewParentCampaign(fileTC1135_TC1136);
                Assert.IsTrue(newCampaign.VerifyIfNewCampaignIsCreatedSuccessfully(campRecordType));
                extentReports.CreateStepLogs("Passed", "Campaign with Record Type : " + campRecordType + " created successfully. ");

                //Navigate to Contacts page
                lvHomePage.NavigateToAnItemFromHLBankerDropdown("Contacts");
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Recently Viewed | Contacts | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User navigated to contacts list page. ");

                //Select Contact type and click continue
                lvRecentlyViewContact.NavigateToContactTypeSelectionPage();
                extentReports.CreateStepLogs("Info", "User navigated to contacts type selection page. ");

                //Calling select record type and click continue function
                string contactType = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", 2, 7);
                lvRecentlyViewContact.SelectContactType(contactType);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Contact: " + contactType + " | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User selected contact type as :" + contactType + ".");

                //Create New External Contact
                lvCreateContact.CreateNewContact(fileTC1135_TC1136);
                driver.SwitchTo().DefaultContent();

                //Assertion to validate contact name displayed on the contacts detail page
                string extContactName = lvContactDetails.GetExternalContactName();
                Assert.AreEqual(extContactFullName, extContactName);
                extentReports.CreateStepLogs("Passed", "New External contact: " + extContactFullName + " is created successfully.");

                //Navigate to Campaign History Tab
                lvContactDetails.NavigateToCampaignHistoryTab();
                extentReports.CreateStepLogs("Info", "User navigated to Campaign History tab. ");

                //Click on Add To Campaign button
                lvContactDetails.ClickAddToCampaignButton();
                extentReports.CreateStepLogs("Info", "Add to campaign history popup is open. ");

                //Search for an existing campaign
                lvContactDetails.SearchAndSelectCampaignName(campName);
                extentReports.CreateStepLogs("Info", "Recently created campaign is selected. ");

                //Validating Title of New Campaign Member Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "New Campaign Member | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                //Validate the error message displayed upon clicking Save button
                string err = camMemEdit.GetErrorMessage();
                Assert.AreEqual("These required fields must be completed: Response Method", err);
                extentReports.CreateLog("Error message : " + err + " is displayed upon clicking save button. ");

                //Add Campaign Member
                camMemEdit.AddCampaignMember(fileTC1135_TC1136);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, campName + " | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "Campaign Member has been successfully attached with the external contact");

                //Delete Campaign Member
                campaignDetail.DeleteCampaignMember();
                extentReports.CreateStepLogs("Info", "Campaign deleted successfully. ");

                //Delete Created Contact
                lvContactDetails.CloseTab(campName);
                lvContactDetails.DeleteContact();
                extentReports.CreateStepLogs("Info", "Created contact deleted successfully.");

                //TC - End
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "Admin User Logged Out from SF Lightning View. ");

                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
}
            
    }
}
