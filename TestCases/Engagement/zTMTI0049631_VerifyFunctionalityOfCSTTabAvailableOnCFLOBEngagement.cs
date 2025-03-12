using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using Microsoft.Office.Interop.Excel;
using MongoDB.Driver;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Web.UI.DataVisualization.Charting;

namespace SF_Automation.TestCases.Engagement
{
    class zTMTI0049631_VerifyFunctionalityOfCSTTabAvailableOnCFLOBEngagement : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        public static string fileTMTI0049631 = "TMTI0049631_VerifyFunctionalityOfCSTTabAvailableOnCFLOBEngagement";
        string engNumber;
        string appNameExl;
        string moduleNameExl1;
        string caseNumber;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void LightniningCSTTabQuestnionaire()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0049631;
                extentReports.CreateLog("Validate the Sections on Questionnaire with different Meeting Types Page ");
                int formRowsCountExl = ReadExcelData.GetRowCount(excelPath, "MeetingTypes");
                for (int row = 2; row <= formRowsCountExl; row++)
                {
                    Initialize();
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
                    extentReports.CreateLog("Deal Team CF User: " + cfUser + " logged in ");
                    //Switching to LightningView
                    login.SwitchToLightningExperience();
                    homePageLV.ClickAppLauncher();
                    //homePageLV.SelectHLBankerApp("Hl Banker");
                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");
                    moduleNameExl1 = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl1);
                    engNumber = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagements", 2, 1);
                    engagementHome.SelectEngagement(engNumber);
                    extentReports.CreateLog(cfUser + "User is on Engagement:" + engNumber + " Detail page");
                    engagementDetails.ClickButtonCSQ("Client Services Questionnaire");
                    string meetingTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "MeetingTypes", row, 1);
                    engagementDetails.SelectMeetingType(meetingTypeExl);
                    engagementDetails.ButtonClickContinue();
                    //Verify that the Questionnaire details page contains different sections in the Questionnaire Information section
                    Assert.IsTrue(engagementDetails.VerifyQuestionnaireInfoPage(), "Verify Questionnaire Information page is displayed");
                    extentReports.CreateLog("Questionnaire Information page is displayed ");
                    extentReports.CreateLog("Verify the availaibilty of Outlook Invitation tab on the CST Questionnaire page ");
                    Assert.IsTrue(engagementDetails.ValidateFormSections(meetingTypeExl, fileTMTI0049631), "Verify Desired sections are displayed as per Meeting Type " + meetingTypeExl + " ");
                    extentReports.CreateLog("Desired sections are displayed as per Meeting Type " + meetingTypeExl + " "); extentReports.CreateLog("Verify that If the user clicks the Client Services Questionnaire button, then the user redirects to the Questionnaire object where the user can fill in the required field. ");
                    engagementDetails.ClickEditInlineIcon();
                    engagementDetails.FillCSTFormRequiredFields(meetingTypeExl);
                    extentReports.CreateLog("All Required Fields are filled on CST Questionnaire Information Page for Meeting Type: " + meetingTypeExl + " ");
                    engagementDetails.ClickFormSave();
                    Assert.IsFalse(engagementDetails.IsFieldsWarningMessageDisplayed(), "Verify Required Fields Warning message box is not Displayed after filling all the Required Fields ");
                    extentReports.CreateLog(meetingTypeExl + ":: Required Fields Warning message box is not Displayed after filling all the Required Fields ");

                    extentReports.CreateLog("Verify that the case id has been generated once the user submits the Questionnaire ");
                    string successMsg = engagementDetails.SubmitQuestionnaire();
                    extentReports.CreateLog(successMsg);
                    extentReports.CreateLog(meetingTypeExl + ":: Questionnaire Submitted ");
                    caseNumber = engagementDetails.GetCaseNumber();
                    Assert.IsNotEmpty(caseNumber);
                    extentReports.CreateLog("Case Number of Submitted Questionnaire: " + caseNumber);

                    string questionnaireNumber = engagementDetails.GetQuestionnaireNumber();
                    string caseStatus = engagementDetails.GetCaseStatus();

                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateLog(valUser + " Logged Out ");

                    // As System Admin 
                    try
                    {
                        login.SwitchToLightningExperience();
                        homePageLV.ClickAppLauncher();
                        homePageLV.SelectApp(appNameExl);
                        homePageLV.SelectModule(moduleNameExl1);
                        engagementHome.SelectEngagement(engNumber);
                        string tabCSTDisplayed = engagementDetails.IsCSTTabDisplayed();
                        Assert.AreEqual(tabCSTDisplayed, "CST tab is Displayed");
                        engagementDetails.QuestionnaireList();
                        string QuestionnaireListQuestionnaireNumber = engagementDetails.GetQuestionnaireListQuestionnaireNumber();
                        string QuestionnaireListMeetingType = engagementDetails.GetQuestionnaireListMeetingType();
                        string QuestionnaireListCaseNumber = engagementDetails.GetQuestionnaireListCaseNumber();
                        string QuestionnaireListCaseStatus = engagementDetails.GetQuestionnaireListCaseStatus();

                        extentReports.CreateLog("Varifying Questionnare detail in CST tab on Engagement page");
                        Assert.AreEqual(questionnaireNumber, QuestionnaireListQuestionnaireNumber);
                        extentReports.CreateLog("Verified Questionnaire Number:: " + questionnaireNumber + " == " + QuestionnaireListQuestionnaireNumber + " ");
                        Assert.AreEqual(meetingTypeExl, QuestionnaireListMeetingType);
                        extentReports.CreateLog("Verified Meeting Type:: " + meetingTypeExl + " == " + QuestionnaireListMeetingType + " ");
                        Assert.AreEqual(caseNumber, QuestionnaireListCaseNumber);
                        extentReports.CreateLog("Verified Case Number:: " + caseNumber + " == " + QuestionnaireListCaseNumber + " ");
                        Assert.AreEqual(caseStatus, QuestionnaireListCaseStatus);
                        extentReports.CreateLog("Verified Case Status:: " + caseStatus + " == " + QuestionnaireListCaseStatus + " ");
                        successMsg = engagementDetails.DeleteCretedQuestionnaire("Questionnaires");
                        extentReports.CreateLog(successMsg);
                        login.SwitchToClassicView();
                        usersLogin.UserLogOut();
                        driver.Quit();
                    }
                    catch (Exception ex)
                    {
                        extentReports.CreateLog(ex.Message);
                        login.SwitchToClassicView();
                        usersLogin.UserLogOut();
                        driver.Quit();
                    }
                }
            }
            catch (Exception ex)
            {
                extentReports.CreateLog(ex.Message);
                homePageLV.UserLogoutFromSFLightningView();
                login.SwitchToLightningExperience();
                homePageLV.ClickAppLauncher();
                homePageLV.SelectApp(appNameExl);
                homePageLV.SelectModule("Engagements");
                engagementHome.SelectEngagement(engNumber);
                engagementDetails.DeleteQuestionnaires("Questionnaires");
                extentReports.CreateLog("All Created QUestionnaires are Deleted");
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
        //[OneTimeTearDown]
        //public void TearDown()
        //{
        //    usersLogin.UserLogOut();
        //    driver.Quit();
        //}
    }
}
