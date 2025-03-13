using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;


namespace SF_Automation.TestCases.Engagement
{
    class zTMTI0046858_Verify_Functionality_Of_Outlook_Invitation_Tab_on_CST_Questionnaire_Page: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        public static string fileTMTI0045436 = "TMTI0046858_Verify_Functionality_Of_Outlook_Invitation_Tab_on_CST_Questionnaire_Page";
        string appNameExl;
        string engNumber;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void LightniningQuestnionaireOutlookInvitations()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0045436;
                Console.WriteLine(excelPath);
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                // Calling Login function                
                login.LoginApplication();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                //Login again as CF Financial User
                string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                usersLogin.SearchCFUserAndLogin(valUser);
                string cfUser = login.ValidateUser();
                Assert.AreEqual(cfUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + cfUser + " logged in ");
                //Switching to LightningView
                login.SwitchToLightningExperience();
                homePageLV.ClickAppLauncher();
                //homePageLV.SelectHLBankerApp("Hl Banker");
                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectApp(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateLog(appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                engNumber = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagements", 2, 1);
                engagementHome.SelectEngagement(engNumber);
                //TMTI0046858	Verify the functionality of Outlook Invitation tab on the CST Questionnaire page
                extentReports.CreateLog("Verify the functionality of Outlook Invitation tab on the CST Questionnaire page ");
                extentReports.CreateLog("User is on Engagement:" + engNumber + " Detail page");
                int formRowsCountExl = ReadExcelData.GetRowCount(excelPath, "MeetingTypes");
                for (int row = 2; row <= formRowsCountExl; row++)
                {
                    engagementDetails.ClickButtonCSQ("Client Services Questionnaire");
                    string meetingTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "MeetingTypes", row, 1);
                    engagementDetails.SelectMeetingType(meetingTypeExl);
                    engagementDetails.ButtonClickContinue();
                    //Verify that the Questionnaire details page contains different sections in the Questionnaire Information section
                    Assert.IsTrue(engagementDetails.VerifyQuestionnaireInfoPage(), "Verify Questionnaire Information page is displayed");
                    extentReports.CreateLog("Questionnaire Information page is displayed ");
                    //Verify that next fields Would you like CST to send invitations? and "Outlook Invitation Preference "
                    //engagementDetails.ClickEditInlineIcon();
                    extentReports.CreateLog("Selected Meeting Type is: "+ meetingTypeExl);
                    Assert.IsTrue(engagementDetails.ValidateOutlookInvitaionFieldDisplayed(), "Validate send CST invitations field is displayed under Outlook Invication section ");
                    extentReports.CreateLog("Send CST invitations field is displayed under Outlook Invitation section ");
                    Assert.IsTrue(engagementDetails.ValidateOutlookInvitaionPreferenceFieldDisplayed(), "Validate Outlook Invitation Preference field is displayed under Outlook Invication section ");
                    extentReports.CreateLog("Outlook Invitation Preference field is displayed under Outlook Invication section ");
                    Assert.IsTrue(engagementDetails.ValidateCSTOutlookInvitationsOptions(fileTMTI0045436, row), "Verify all options(None, Yes, No) are displayed in dropdown");
                    extentReports.CreateLog("All options(None, Yes, No) are displayed in dropdown ");
                    Assert.IsTrue(engagementDetails.ValidateCSTOutlookInvitationsPreferenceOptions(fileTMTI0045436,row), "Verify all options are displayed in dropdown");
                    extentReports.CreateLog("All options are displayed in Outlook Invitation Preference dropdown ");
                    Assert.IsTrue(engagementDetails.AreCSTOutlookInvitationFieldsDisplayed(fileTMTI0045436,row),"Verify the CST Outlook Invitation fields are Displayed ");
                    extentReports.CreateLog("CST Outlook Invitation fields are Displayed ");

                    engagementDetails.SelectDealTeamContact();
                    extentReports.CreateLog("Deal Team Contact is added ");
                    engagementDetails.SelectDealTeamMember();
                    extentReports.CreateLog("Deal Team Member is added ");
                    engagementDetails.SelectClientInvitation();
                    extentReports.CreateLog("Client is added ");
                    engagementDetails.SelectCounterpartiesInvitation();
                    extentReports.CreateLog("Counterparty is added ");
                    string toasMessage= engagementDetails.SaveOutlookInvitationFields();
                    Assert.AreEqual(toasMessage, "Contacts updated on record");
                    extentReports.CreateLog("All Contacts are Added and saved ");
                    engagementDetails.RemoveSelectedOptions();
                    Assert.AreEqual(engagementDetails.SaveOutlookInvitationFields(), "Contacts updated on record");
                    extentReports.CreateLog("All Contacts are Removed  and saved ");
                    string questionnaireNumber = engagementDetails.GetQuestionnaireNumber();
                    engagementDetails.CloseTab(questionnaireNumber);
                    extentReports.CreateLog("Current Tab with Questionnaire # " + questionnaireNumber + " is Closed ");
                }
                homePageLV.UserLogoutFromSFLightningView();
            }
            catch (Exception ex)
            {
                extentReports.CreateLog(ex.Message);
                homePageLV.UserLogoutFromSFLightningView();

            }
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            login.SwitchToLightningExperience();
            homePageLV.ClickAppLauncher();
            homePageLV.SelectApp(appNameExl);
            homePageLV.SelectModule("Engagements");
            engagementHome.SelectEngagement(engNumber);
            engagementDetails.DeleteQuestionnaires("Questionnaires");
            usersLogin.UserLogOut();
            driver.Quit();
        }
    }
}
