using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using SF_Automation.Pages.Contact;
using System;

namespace SF_Automation.TestCases.Contact
{
    class TMTT0033378_VerifyTheFunctionalityOfRelationshipTreemapAddedToExternalContactDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_ContactDetailsPage lvContactDetailsPage = new LV_ContactDetailsPage();
        LV_ContactRelationshipPage lvContactRelationshipPage = new LV_ContactRelationshipPage();
        LV_CompanyDetailsPage lvCompanyDetailsPage = new LV_CompanyDetailsPage();
        LV_EngagementDetailsPage lvEngagementDetailsPage = new LV_EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        
        public static string fileTCTMTT0033378 = "TMTT0033378_VerifyTheFunctionalityOfRelationshipTreemapAddedToExternalContactDetailPage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTheFunctionalityOfRelationshipTreemapAddedToExternalContactDetailPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTCTMTT0033378;
                Console.WriteLine(excelPath);

                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                string externalContactName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExternalContact", 1, 1);
                string externalContactName1 = ReadExcelData.ReadDataMultipleRows(excelPath, "ExternalContact", 2, 1);
                string engContactName = ReadExcelData.ReadData(excelPath, "EngContact", 1);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " page is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "Admin User: " + login.ValidateUser() + " is able to login in SF TEST environment. ");

                //Search CF Financial user by global search
                homePage.SearchUserByGlobalSearch(fileTCTMTT0033378, user);
                extentReports.CreateStepLogs("Info", "CF Financial User: " + user + " details are displayed after doing a global search. ");

                //Login user
                usersLogin.LoginAsSelectedUser();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Passed", "User switched to lightning view. ");
                }

                Assert.IsTrue(login.ValidateUserLightningView(fileTCTMTT0033378, 2));
                extentReports.CreateStepLogs("Passed", "User is able to login into lightning view. ");

                //TC - TMTI0078920, TMTI0078922 - Verify the availability of "Relationship Treemap" and its sections on the External Contact Detail Page.
                lvHomePage.SearchContactFromMainSearch(externalContactName);

                Assert.IsTrue(lvContactDetailsPage.VerifyUserLandedOnCorrectContactDetailsPage(externalContactName));
                extentReports.CreateStepLogs("Passed", "User successfully navigated to an external contact: " + externalContactName + " details page. ");

                Assert.IsTrue(lvContactDetailsPage.VerifyTheAvailableSectionsUnderRelationshipTreeMapOnContactDetailsPage(excelPath));
                extentReports.CreateStepLogs("Passed", "All the expected Relationship Treemap sections are available on the external contact details page. ");

                //TC - TMTI0078924 - Verify the fields displayed in the "Contact Information" section on the Relationship tree.
                Assert.IsTrue(lvContactDetailsPage.VerifyTheAvailableFieldsUnderContactInformationSectionOnContactDetailsPage(excelPath));
                extentReports.CreateStepLogs("Passed", "All the expected fields are available under Contact Information section on the external contact details page. ");

                //TC - TMTI0078926 - Verify the fields displayed in the "Top Relationships" section on the Relationship tree.
                Assert.IsTrue(lvContactDetailsPage.VerifyTheAvailableFieldsUnderTopRelationshipsSectionOnContactDetailsPage(excelPath));
                extentReports.CreateStepLogs("Passed", "All the expected fields are available under Top Relationships section on the external contact details page. ");

                //TC - TMTI0078928, TMTI0078931 - Verify that the contacts in the Top Relationship will be sorted by Strength rating & will be sorted by the most recent activities if there is a tie in the Strength rating.
                Assert.IsTrue(lvContactDetailsPage.VerifyContactsUnderTopRelationshipSectionIsSortedCorrectly());
                extentReports.CreateStepLogs("Passed", "Sorting of contacts under Top Relationship section is working as expected. ");

                //TC - TMTI0078933 - Verify that if the strength rating is strong and the latest activity date is within the last month, then Contact Name will be displayed in bold text.
                Assert.IsTrue(lvContactDetailsPage.VerifyContactNameUnderTopRelationshipIsBoldIfStrengthRatingIsStrongAndActivityDateIsWithinLastMonth());
                extentReports.CreateStepLogs("Passed", "Contact name under Top Relationship is Bold if strength rating is strong and activity date is within Last Month. ");

                //TC - TMTI0078935 - Verify that the Contact Name is linked to the relationship detail page.
                Assert.IsTrue(lvContactRelationshipPage.VerifyClickingOnTheContactNameTakesTheUserToItsRelationshipDetailPage(externalContactName));
                extentReports.CreateStepLogs("Passed", "Clicking on the contact name takes the user to its relationship detail page. ");

                //TC - TMTI0078937 - Verify the fields displayed in the "Affiliated Companies" section on the Relationship tree.
                Assert.IsTrue(lvContactDetailsPage.VerifyFieldsDisplayedUnderAffiliatedCompaniesSection(excelPath));
                extentReports.CreateStepLogs("Passed", "All the expected fields are available under Affiliated Companies section on the external contact details page. ");

                //TC - TMTI0078939 - Verify that the Company Name is linked to the Company Detail page.
                Assert.IsTrue(lvCompanyDetailsPage.VerifyClickingOnTheCompanyNameUnderAffiliatedCompaniesSectionTakesUserToCompanyDetailsPage());
                extentReports.CreateStepLogs("Passed", "Clicking on the company name takes the user to the company detail page. ");

                //TC - TMTI0078941 - Verify that if Affiliation Type is "Inside Board Member or Outside Board Member", then it will be displayed in bold text.
                Assert.IsTrue(lvContactDetailsPage.VerifyAffiliationTypeIsDisplayedInBoldIfItIsInsideBoardMemberOrOutsideBoardMember());
                extentReports.CreateStepLogs("Passed", "If Affiliation Type is Inside Board Member or Outside Board Member, then it is displayed in bold text. ");

                //TC - TMTI0078943 - Verify the fields displayed in the "Associated Engagements" section on the Relationship tree.
                Assert.IsTrue(lvContactDetailsPage.VerifyFieldsDisplayedUnderAssociatedEngagementsSection(excelPath));
                extentReports.CreateStepLogs("Passed", "All the expected fields are available under Associated Engagements section on the external contact details page. ");

                lvContactDetailsPage.CloseDuplicateCompanyAlertMessageDialogBox();
                extentReports.CreateStepLogs("Info", "Close duplicate company alert box. ");

                //TC - TMTI0078945 - Verify that the "Associated Engagements" section displays those engagements where this contact is placed as Engagement Contact.
                Assert.IsTrue(lvEngagementDetailsPage.VerifyAssociatedEngagementsSectionOnContactDetailsPageDisplaysEngagementsWhereTheExternalContactIsAnEngagementContact(engContactName));
                extentReports.CreateStepLogs("Passed", "Associated Engagements section displays those engagements where the external contact is placed as Engagement Contact. ");

                //TC - TMTI0078947 - Verify that if there are both Active and closed Engagements, then it will list only Active Engagements.
                Assert.IsTrue(lvContactDetailsPage.VerifyIfThereAreBothActiveAndInactiveEngagementsThenOnlyActiveEngagementsAreDisplayedUnderAssociatedEngagementsSection());
                extentReports.CreateStepLogs("Passed", "If there are both Active and Closed Engagements, then only Active Engagements are listed under Associated Engagements section. ");

                //TC - TMTI0078951 - Verify that the Engagement Name is linked to the Engagement Detail page.
                lvContactDetailsPage.CloseTab("Engagement Contacts");
                Assert.IsTrue(lvEngagementDetailsPage.VerifyClickingOnTheEngagementNameUnderAssociatedEngagementsSectionTakesUserToEngagementDetailsPage());
                extentReports.CreateStepLogs("Passed", "Clicking on the engagement name from associated engagement section takes the user to the engagement detail page. ");

                //TC - TMTI0078953 - Verify that the fields are displayed in the "Referrals" section on the Relationship tree.
                //TC - TMTI0078957 - If status is Active then field displayed is: Date Engaged
                //TC - TMTI0078959 - If status is Closed then field displayed is: Closed Date
                Assert.IsTrue(lvContactDetailsPage.VerifyFieldsDisplayedUnderReferralsSection(excelPath));
                extentReports.CreateStepLogs("Passed", "All the expected fields are available under Referrals section on the external contact details page. Also if status is Active then field displayed is: Date Engaged and If Status is Closed, then the field displayed is: Closed Date. ");

                //TC - TMTI0078961 - Verify that the Engagement Name under REferrals section is linked to the Engagement Detail page.
                Assert.IsTrue(lvEngagementDetailsPage.VerifyClickingOnTheEngagementNameUnderReferralsSectionTakesUserToEngagementDetailsPage());
                extentReports.CreateStepLogs("Passed", "Clicking on the engagement name from referrals section takes the user to the engagement detail page. ");

                //Switch to a different external contact details page
                lvContactDetailsPage.CloseTab(externalContactName);
                lvContactDetailsPage.CloseTab(externalContactName);
                lvHomePage.SearchContactFromMainSearch(externalContactName1);
                Assert.IsTrue(lvContactDetailsPage.VerifyUserLandedOnCorrectContactDetailsPage(externalContactName1));
                extentReports.CreateStepLogs("Passed", "User successfully navigated to an external contact: " + externalContactName1 + " details page. ");

                //TC - TMTI0078949 - Verify that if there are no Active Engagements, then it will list the latest 5 Closed Engagements.
                Assert.IsTrue(lvContactDetailsPage.VerifyIfThereAreNoActiveEngagementsThenLatest5ClosedEngagementsAreDisplayedUnderAssociatedEngagementsSection());
                extentReports.CreateStepLogs("Passed", "If there are no Active Engagements, then latest 5 Closed Engagements are listed under Associated Engagements section. ");

                //Logout from SF Lightning View
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "CF Financial User Logged Out from SF Lightning View. ");

                //Logout from SF Classic View
                usersLogin.UserLogOut();
                extentReports.CreateStepLogs("Info", "Admin User Logged Out from SF Classic View. ");

                driver.Quit();
            }
            catch(Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.Quit();
            }
        }
    }
}
