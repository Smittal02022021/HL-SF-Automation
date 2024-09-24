using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;
using SF_Automation.Pages.Activities;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;

namespace SF_Automation.TestCases.LV_Activities
{
    class TMT0072233_VerifyActivityIsLinkedToTheRelatedEngagement : BaseClass
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

        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();

        public static string fileTMTC0032668 = "TMTC0032668_VerifyActivityIsLinkedToTheRelatedEngagement";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyActivityIsLinkedToTheRelatedEngagement()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0032668;
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                string userCAOExl = ReadExcelData.ReadData(excelPath, "Users", 2);
                string extContactName = ReadExcelData.ReadData(excelPath, "Contact", 1);
                string relatedCompany = ReadExcelData.ReadData(excelPath, "Contact", 2);
                string tabName = ReadExcelData.ReadData(excelPath, "Contact", 3);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login. ");

                //Switch to lightning view
                try
                {
                    if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                    {
                        homePage.SwitchToLightningView();
                        extentReports.CreateStepLogs("Passed", "Admin User is able to login into lightning view. ");
                    }
                }
                catch(Exception)
                {

                }

                //Navigate to Opportunities page
                lvHomePage.NavigateToAnItemFromHLBankerDropdown("Opportunities");
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Recently Viewed | Opportunities | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User navigated to Opportunities list page. ");

                string valJobType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 3);
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);

                //Validating Title of New Opportunity Page
                string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                //Create New Opportunity
                string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTMTC0032668);
                extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTC0032668);
                Assert.AreEqual(displayedTab, "Info");
                extentReports.CreateStepLogs("Info", "User is on Opportunity detail " + displayedTab + " tab ");

                //Validating Opportunity details  
                string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                Assert.IsNotNull(opportunityNumber);
                extentReports.CreateStepLogs("Passed", "Opportunity with number : " + opportunityNumber + " is created ");

                //Create External Primary Contact
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                addOpportunityContact.CickAddCFOpportunityContact();
                addOpportunityContact.CreateContactL2(fileTMTC0032668);
                extentReports.CreateStepLogs("Info", valContactType + " Opportunity contact is saved ");

                //Update required Opportunity fields for conversion and Internal team details
                opportunityDetails.UpdateReqFieldsForCFConversionLV2(fileTMTC0032668, valJobType);
                extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");

                opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTC0032668);
                extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");

                opportunityDetails.ClickReturnToOpportunityLV();
                extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                //update CC and NBC checkboxes in LV
                opportunityDetails.UpdateOutcomeNBCApproveDetailsLV(valJobType);
                extentReports.CreateStepLogs("Info", "CC and NBC checkboxes updated. ");

                //Requesting for engagement and validate the success message
                opportunityDetails.ClickRequestToEngL();

                //Submit Request To Engagement Conversion 
                string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");

                //Switch Back to Classic View
                lvHomePage.SwitchBackToClassicView();
                extentReports.CreateStepLogs("Info", "Admin user switched back to classic view. ");

                //Search CAO User by global search
                extentReports.CreateStepLogs("Info", "User " + userCAOExl + " details are displayed. ");

                //Login user
                homePage.SearchUserByGlobalSearch(fileTMTC0032668, userCAOExl);
                usersLogin.LoginAsSelectedUser();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Passed", "CAO User: " + userCAOExl + " is able to login into lightning view. ");
                }
                else
                {
                    extentReports.CreateStepLogs("Passed", "CAO User: " + userCAOExl + " is able to login into lightning view. ");
                }

                //Search for created opportunity
                extentReports.CreateStepLogs("Info", " CAO User Search for Created Opportunity");
                opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                //Approve the Opportunity 
                string status = opportunityDetails.ClickApproveButtonL();
                Assert.AreEqual(status, "Approved");
                extentReports.CreateStepLogs("Passed", "Opportunity " + status + " ");
                opportunityDetails.CloseApprovalHistoryTabL();

                //Calling function to convert to Engagement
                opportunityDetails.ClickConvertToEngagementL2();
                extentReports.CreateStepLogs("Info", "Opportunity Converted into Engagement ");

                //Validate the Engagement name in Engagement details page
                string engagementNumber = engagementDetails.GetEngagementNumberL();
                string engagementName = engagementDetails.GetEngagementNameL();

                //Need to get Name of Opp and Eng
                Assert.AreEqual(opportunityName, engagementName);
                extentReports.CreateStepLogs("Passed", "Name of Engagement : " + engagementName + " is Same as Opportunity name ");

                //Logout from SF Lightning View
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "CAO User Logged Out from SF Lightning View. ");

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Passed", "Admin User is able to login into lightning view. ");
                }

                //Search external contact
                lvHomePage.SearchContactFromMainSearch(extContactName);
                Assert.IsTrue(lvContactDetails.VerifyUserLandedOnCorrectContactDetailsPage(extContactName));
                extentReports.CreateStepLogs("Passed", "User navigated to external contact details page. ");

                //Navigate to Activity tab
                lvContactDetails.NavigateToActivityTabInsideCFFinancialUser();
                Assert.IsTrue(LV_ContactsActivityList.VerifyUserLandsOnActivityTab());
                extentReports.CreateStepLogs("Passed", "User landed on the Activity tab of external contact. ");

                //TMT0072233 Verify that the Activity is linked to the related Engagement.

                int totalActivity = ReadExcelData.GetRowCount(excelPath, "Activity");

                string type = ReadExcelData.ReadData(excelPath, "Activity", 1);
                string subject = ReadExcelData.ReadData(excelPath, "Activity", 2);
                string companyDis = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 7);
                string oppDis = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", 2, 8);

                //Create new activity
                int beforeCount = LV_ContactsActivityList.GetActivityCount();
                addActivity.CreateNewActivityWithEngagementDiscussed(fileTMTC0032668, engagementName, opportunityName);
                lvContactDetails.CloseTab("View Activity");

                Assert.IsTrue(LV_ContactsActivityList.VerifyCreatedActivityIsDisplayedUnderActivitiesList(beforeCount));
                extentReports.CreateStepLogs("Passed", "Activity created successfully with Engagement discussed for call type: " + type);

                //Navigate to Engagement Detail from Activity Detail page
                LV_ContactsActivityList.ViewActivityFromList(subject);
                extentReports.CreateStepLogs("Info", "User redirected Activity Detail Page ");

                Assert.IsTrue(lV_ContactsActivityDetailPage.NavigateToEngagementDetailPage(engagementName));
                extentReports.CreateStepLogs("Passed", "User landed on the Engagement details page. ");

                //Verify Activity Is Linked To Engagement
                Assert.IsTrue(engagementDetails.VerifyActivityIsLinkedToEngagement(subject));
                extentReports.CreateStepLogs("Passed", "Activity linked with the Engagement is listed under Activity Tab of the Engagement. ");

                //Deleting Main Created Activity
                engagementDetails.DeleteActivity();
                extentReports.CreateStepLogs("Passed", "Main Activity with call type: " + type + " deleted successfully. ");

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
