using AventStack.ExtentReports;
using Microsoft.Office.Interop.Excel;
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
    class TMTI0048263_0048282_45498_45523_48654_48269_48651_VerifyOnceQuestionnaireSubmittedEmailWillGoOutToDealUserandCST_group : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        Outlook outlook = new Outlook();
        public static string TMTI0048263 = "TMTI0048263_VerifyOnceQuestionnaireSubmittedEmailWillGoOutToDealUserandCST_group";
        public static string fileOutlook = "Outlook";
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
        public void VerifyCSTQuestionnaireEmailFunctionality()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + TMTI0048263;

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
                    extentReports.CreateLog(cfUser + " User is on Engagement:" + engNumber + " Detail page");

                    engagementDetails.ClickButtonCSQ("Client Services Questionnaire");
                    string meetingTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "MeetingTypes", row, 1);
                    engagementDetails.SelectMeetingType(meetingTypeExl);
                    engagementDetails.ButtonClickContinue();
                    //Verify that the Questionnaire details page contains different sections in the Questionnaire Information section
                    Assert.IsTrue(engagementDetails.VerifyQuestionnaireInfoPage(), "Verify Questionnaire Information page is displayed");
                    extentReports.CreateLog("Questionnaire Information page is displayed ");
                    extentReports.CreateLog("Verify User can submit the form with required fields values ");
                    engagementDetails.ClickEditInlineIcon();
                    engagementDetails.FillCSTFormRequiredFields(meetingTypeExl);
                    extentReports.CreateLog("All Required Fields are filled on CST Questionnaire Information Page for Meeting Type: " + meetingTypeExl + " ");
                    engagementDetails.ClickFormSave();
                    Assert.IsFalse(engagementDetails.IsFieldsWarningMessageDisplayed(), "Verify Required Fields Warning message box is not Displayed after filling all the Required Fields ");
                    string msgSuccess = engagementDetails.SubmitQuestionnaire();
                    extentReports.CreateLog(msgSuccess);
                    extentReports.CreateLog(meetingTypeExl + ":: Questionnaire Submitted ");
                    caseNumber = engagementDetails.GetCaseNumber();
                    Assert.IsNotEmpty(caseNumber);
                    engagementDetails.ClickCaseNumber();

                    //TMTI0045498- #10 & #11 Verify the Deal team member is not able to edit the form after Submittion 
                    extentReports.CreateLog("Verify the Deal team member is not able to edit the form after Submittion ");
                    engagementDetails.EditCaseDetails(cfUser);
                    engagementDetails.ClickFormSave();
                    string validationMsgExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CSTFormRequiredFields", row, 2);
                    string actualValidation = engagementDetails.GetAccessValidationMessage();
                    Assert.AreEqual(actualValidation, validationMsgExl);
                    extentReports.CreateLog(actualValidation);
                    engagementDetails.ClickFormCancel();

                    //TMTI0048269- Verify that the deal user is able to add case comments on the case page
                    extentReports.CreateLog("Verify that the deal user is able to add case comments on the Case Detail page ");
                    Assert.IsTrue(engagementDetails.IsRelatedTabDisplayed(), "Verify Related Tab is Displayed on Case Detail page ");
                    Assert.IsTrue(engagementDetails.IsCommentSectionDisplayed(), "Verify Comments section is Displayed under Related Tab on Case Detail page ");
                    string commentsExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CaseDetailQuickLink", 4, 2);
                    string txtMsg = engagementDetails.AddCaseComments(commentsExl);
                    extentReports.CreateLog(txtMsg + " for " + meetingTypeExl + " on " + caseNumber);
                    string savedComments = engagementDetails.GetSavedCaseComments();
                    Assert.AreEqual(savedComments, commentsExl);
                    extentReports.CreateLog("Deal user is able to add case comments on the Case details page ");
                    driver.Quit();
                    extentReports.CreateLog("Logout and close the browser ");

                    // Launch Outlook in Browser
                    OutLookInitialize();
                    extentReports.CreateLog("Launch Outlook in Browser ");
                    outlook.LoginOutlook(fileOutlook);
                    string outlookLabel = outlook.GetLabelOfOutlook();
                    Assert.AreEqual("Outlook", outlookLabel);
                    extentReports.CreateLog("Verified and Validation is done for User is logged in to outlook ");

                    //Search and select email
                    outlook.SelectEmail("Sandbox: CST Questionnaire for Review");
                    extentReports.CreateLog("Email found and selected ");
                    string caseLinkFound = outlook.IsCaseLinkPresent();
                    Assert.AreEqual(caseLinkFound, "Case Link is Present");
                    extentReports.CreateLog("Case Link is present in selected email ");

                    //TMTI0045523 Verify that Once the questionnaire is submitted, the email will go out to deal user and CST Group
                    string submitterName = outlook.IsSubmitterPresentInEmail(cfUser);
                    Assert.AreEqual(submitterName, "Submitter name is Present");
                    extentReports.CreateLog("Submitter Name is present in selected email ");

                    //TMTI0048654: Verify the email for case comments by the deal user                    
                    outlook.SelectEmail("Sandbox: New case comment notification");
                    extentReports.CreateLog("New case comment notification Email found and selected ");
                    driver.Quit();
                    extentReports.CreateLog("Browser Closed ");

                    //TMTI0048651 Verify the email functionality when the Questionnaire is edited after the case is accepted by the CST user and the Coordinator and Backup Coordinator are assigned. 
                    // Deal Team member is editing the submitted Questionnaire
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
                    ////////////
                    //CST Group userLogin 
                    //Login again as CST Group, CF Financial User
                    valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "CSTUsers", 2, 1);
                    usersLogin.SearchCFUserAndLogin(valUser);
                    cfUser = login.ValidateUser();
                    Assert.AreEqual(cfUser.Contains(valUser), true);
                    extentReports.CreateLog("CST Group User: " + cfUser + " logged in ");
                    //Switching to LightningView
                    login.SwitchToLightningExperience();
                    homePageLV.ClickAppLauncher();
                    //homePageLV.SelectHLBankerApp("Hl Banker");
                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");
                    moduleNameExl1 = ReadExcelData.ReadData(excelPath, "ModuleName", 2);

                    //Verify Cases module is available in dropdown menu list & redirected to Cases List page when clicked on Cases module
                    extentReports.CreateLog("Verify Cases module is available in dropdown menu list & redirected to Cases List page when clicked on Cases module ");
                    homePageLV.SelectModule(moduleNameExl1);
                    extentReports.CreateLog("Module: " + moduleNameExl1 + " is available for logged in user and selected ");
                    Assert.IsTrue(homePageLV.IsModulePageDisplayed(moduleNameExl1), "Verify user is Redirected to " + moduleNameExl1 + " page on Selection of Module ");
                    extentReports.CreateLog("User is Redirected to " + moduleNameExl1 + " page on Selection of Module ");
                    engagementDetails.SelectListView("All Open Cases");

                    extentReports.CreateLog("Verify that the CST users should be able to access the CST Questionnaire via Cases ");
                    engagementDetails.SearchCaseNumber(caseNumber);

                    engagementDetails.SelectOpenCase();
                    extentReports.CreateLog("User is on Case : " + caseNumber + " Detail page");

                    //look for the Accept button on top right and accept the case to become case owner.
                    Assert.IsTrue(engagementDetails.IsBtnAcceptDisplayed(), "Verify Accept button is Displayed on Case Detail Page and Accept the Case to become the Case Owner ");
                    string message = engagementDetails.ClickBtnAccept();
                    Assert.IsTrue(message.Contains("accepted"));
                    extentReports.CreateLog(caseNumber + ":: " + message + " ");
                    string caseOwner = engagementDetails.GetCaseOwner();
                    Assert.AreEqual(caseOwner, valUser);
                    extentReports.CreateLog("Case Owner ::" + caseOwner);

                    engagementDetails.GetCaseStatus();
                    extentReports.CreateLog("Before Assign to Co-Ordinator Case Status: " + engagementDetails.GetCaseStatus() + " ");
                    engagementDetails.AssignToCoOrdinator(valUser);
                    engagementDetails.ClickFormSave();
                    Assert.AreEqual("Assigned", engagementDetails.GetCaseStatus());
                    extentReports.CreateLog("After Assign to Co-Ordinator Case Status: " + engagementDetails.GetCaseStatus() + " and Case Co-Ordinator is " + valUser + " ");

                    homePageLV.UserLogoutFromSFLightningView();
                    ////////////////^^#11. Click on save button. and logout from CST User
                    ///
                    //#12: Login with Deal team member

                    valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                    extentReports.CreateLog("Login with Deal Team Member:: " + valUser);
                    usersLogin.SearchCFUserAndLogin(valUser);
                    cfUser = login.ValidateUser();
                    Assert.AreEqual(cfUser.Contains(valUser), true);
                    extentReports.CreateLog("Deal Team CF User: " + cfUser + " logged in ");
                    //Switching to LightningView
                    login.SwitchToLightningExperience();
                    homePageLV.ClickAppLauncher();
                    //homePageLV.SelectHLBankerApp("Hl Banker");
                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");
                    moduleNameExl1 = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl1);
                    engNumber = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagements", 2, 1);
                    engagementHome.SelectEngagement(engNumber);
                    extentReports.CreateLog(cfUser + " User is on Engagement:" + engNumber + " Detail page");
                    string tabCSTDisplayed = engagementDetails.IsCSTTabDisplayed();
                    Assert.AreEqual(tabCSTDisplayed, "CST tab is Displayed");
                    extentReports.CreateLog(" Selecting the Questionnaire from CST tab ");
                    engagementDetails.QuestionnaireList();
                    engagementDetails.ClickQuestionnaireLink(caseNumber);
                    engagementDetails.UpdateQuestionnaire(row);
                    engagementDetails.ClickFormSave();
                    extentReports.CreateLog(" Selected the Questionnaire is updated ");
                    driver.Quit();
                    extentReports.CreateLog("Browser Closed ");

                    // Launch Outlook in Browser
                    OutLookInitialize();
                    extentReports.CreateLog("Launch Outlook in Browser ");
                    outlook.LoginOutlook(fileOutlook);
                    outlookLabel = outlook.GetLabelOfOutlook();
                    Assert.AreEqual("Outlook", outlookLabel);
                    extentReports.CreateLog("Verified and Validation is done for User is logged in to outlook ");

                    //Search and select email
                    outlook.SelectEmail("Sandbox: CST Questionnaire for Review");
                    extentReports.CreateLog("Email found and selected ");
                    bool IsUpdatedCaseEmail = outlook.IsUpdatedCaseEmailFound();
                    Assert.IsTrue(IsUpdatedCaseEmail, "Verify Review new changes Email found");
                    extentReports.CreateLog("Please review new changes Comments are found in selected email ");
                    driver.Quit();
                    extentReports.CreateLog("Browser Closed ");
                }

            }
            catch (Exception ex)
            {
                extentReports.CreateLog(ex.Message);
                homePageLV.UserLogoutFromSFLightningView();
                driver.Quit();
            }
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            //*Delete the saved/submittedCST via Admin*//
            Initialize();
            login.LoginApplication();
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

