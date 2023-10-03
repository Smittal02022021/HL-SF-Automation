
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
    class TMTI0045436_45443_45461_48254_46851_45467_45490_45542_45547_45556_45516_49645_45494_45538_45519_48957_45485_Verify_Client_Services_Questionnaire_Button_Functionality : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        public static string fileTMTI0045436 = "TMTI0045436_Verify_Client_Services_Questionnaire_Button_Functionality";
        string engNumber;
        string appNameExl;
        string moduleNameExl1;
        string caseNumber;
        string questionnaireNumber;
        string successMsg;
        string quickLinkExl;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void LightniningCSTQuestnionaire()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0045436;
                string fileToUpload = ReadJSONData.data.filePaths.testData + "FileToUpload.txt";
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
                extentReports.CreateLog("User is on Engagement:"+ engNumber + " Detail page");
                string engagementDetailPageEngagementName = engagementDetails.GetEngagementDetailEngagementName();
                string engagementDetailPageEngagementNumber = engagementDetails.GetEngagementDetailEngagementNumber();
                
                //TMTI0045436 Verify is Client Services Questionaire Button is Displayed on selected Engagement Detail page
                extentReports.CreateLog("Verify is Client Services Questionaire Button is Displayed on selected Engagement Detail page");
                Assert.IsTrue(engagementDetails.IsButtonClientServicesQuestionnaire("Client Services Questionnaire"), "Verify Client Services Questionnaire button is available on Engagement detail page ");
                extentReports.CreateLog("Client Services Questionaireis button is available on Engagement detail pager ");
                
                //TMTI0045443&TMTI0045461 Verify that the CST Questionnaire details tab is available under the Engagement info details and that having drop-down selection as Yes and NO
                extentReports.CreateLog("Verify that the CST Questionnaire details tab is available under the Engagement info details and that having drop-down selection as Yes and NO ");
                engagementDetails.SelectTabDetailPage("CST Questionnaire Details");
                extentReports.CreateLog("Seleting CST Questionnaire Details tab ");
                Assert.IsTrue(engagementDetails.ValidateCSTQuestionnaireDropdownOptions(fileTMTI0045436), "Verify the Options in Dropdown are as expected(None, Yes, No");
                extentReports.CreateLog("CST Questionnaire Dropdown Options are as expected(None, Yes, No) ");
                Assert.IsTrue(engagementDetails.ValidateAddionalField("No"), "Verify the Additional Field is displayed if selected No");
                extentReports.CreateLog("Additional Field is displayed if selected No ");
                Assert.IsTrue(engagementDetails.ValidateButtonSaveOption("No"), "Verify the Result is saved with No option ");
                extentReports.CreateLog("CST Questionnaire Dropdown Options Saved with Value No ");
                Assert.IsTrue(engagementDetails.ValidateButtonSaveOption("Yes"),"Verify the Result is saved with Yes option ");
                extentReports.CreateLog("CST Questionnaire Dropdown Options Saved with Value Yes ");
               
                //TMTI0048254 Verify that the deal team/internal team user can View 4 types of meeting type
                extentReports.CreateLog("Verify that the deal team/internal team user can View 4 types of meeting type ");
                engagementDetails.ClickButtonCSQ("Client Services Questionnaire");
                Assert.IsTrue(engagementDetails.ValidateMeetingTypes(fileTMTI0045436), "Verify all available Meeting Types while clicking on Client Services Questionnaire Button ");
                extentReports.CreateLog("Client Services Questionnaire has all 4 types of Meeting Types ");
                
                //Validate the Sections on Meeting Type Page
                extentReports.CreateLog("Validate the Sections on Questionnaire with different Meeting Types Page ");
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
                    //TMTI0046851 Verify the availaibilty of Outlook Invitation tab on the CST Questionnaire page
                    extentReports.CreateLog("Verify the availaibilty of Outlook Invitation tab on the CST Questionnaire page ");
                    Assert.IsTrue(engagementDetails.ValidateFormSections(meetingTypeExl, fileTMTI0045436), "Verify Desired sections are displayed as per Meeting Type " + meetingTypeExl + " ");
                    extentReports.CreateLog("Desired sections are displayed as per Meeting Type " + meetingTypeExl + " ");
                    //TMTI004546 Verify that If the user clicks the "Client Services Questionnaire" button, then the user redirects to the Questionnaire object where the user can fill in the required field.
                    extentReports.CreateLog("Verify that If the user clicks the Client Services Questionnaire button, then the user redirects to the Questionnaire object where the user can fill in the required field. ");
                    engagementDetails.ClickEditInlineIcon();
                    // click on save button wihout filling any value to validate the Required Fields Validations 
                    engagementDetails.ClickFormSave();
                    Assert.IsTrue(engagementDetails.IsFieldsWarningMessageDisplayed(),"Verify Required Fields Warning message box is Displayed for All Required Fields");
                    extentReports.CreateLog(meetingTypeExl+":: Required Fields Warning Message box is Displayed ");
                    string requiredFieldsValidation= engagementDetails.ValidateRequiredFields(fileTMTI0045436, row);
                    Assert.AreEqual(requiredFieldsValidation, "Desired Required fields are Displayed in Validation Popup");
                    extentReports.CreateLog(meetingTypeExl+":: Desired Required fields are Displayed in Validation Popup ");
                    engagementDetails.ClickFormCancel();
                    engagementDetails.ClickEditInlineIcon();
                    engagementDetails.FillCSTFormRequiredFields(meetingTypeExl);
                    extentReports.CreateLog("All Required Fields are filled on CST Questionnaire Information Page for Meeting Type: " + meetingTypeExl + " ");
                    //engagementDetails.ClickFormCancel();
                    engagementDetails.ClickFormSave();
                    Assert.IsFalse(engagementDetails.IsFieldsWarningMessageDisplayed(),"Verify Required Fields Warning message box is not Displayed after filling all the Required Fields ");
                    extentReports.CreateLog(meetingTypeExl + ":: Required Fields Warning message box is not Displayed after filling all the Required Fields ");
                    //TMTI0045490-Verify that the case id has been generated once the user submits the Questionnaire
                    extentReports.CreateLog("Verify that the case id has been generated once the user submits the Questionnaire ");
                    successMsg = engagementDetails.SubmitQuestionnaire();
                    extentReports.CreateLog(successMsg);
                    extentReports.CreateLog(meetingTypeExl + ":: Questionnaire Submitted ");
                    caseNumber = engagementDetails.GetCaseNumber();
                    Assert.IsNotEmpty(caseNumber);
                    extentReports.CreateLog("Case Number of Submitted Questionnaire: "+caseNumber);
                    //Click on Case number to see the Questionnaire related information
                    //TMTI0045494 Verify that case also relates to the engagement itself 
                    extentReports.CreateLog("Verify that case also relates to the engagement itself ");
                    engagementDetails.ClickCaseNumber();
                    string caseDetailPageEngagementName = engagementDetails.GetCaseDetailEngagementName();
                    Assert.AreEqual(caseDetailPageEngagementName, engagementDetailPageEngagementName);
                    extentReports.CreateLog("Engagement Name:: " + engagementDetailPageEngagementName+ " ,on Engagement Detail page and Engagement Name:: " + caseDetailPageEngagementName+" ,on Case Detail page are same ");
                    
                    string caseDetailPageEngagementNumber = engagementDetails.GetCaseDetailEngagementNumber();
                    Assert.AreEqual(caseDetailPageEngagementNumber, engagementDetailPageEngagementNumber);
                    extentReports.CreateLog("Engagement Number:: " + engagementDetailPageEngagementNumber + " ,on Engagement Detail page and Engagement Number:: " + caseDetailPageEngagementNumber + " ,on Case Detail page are same ");
                    
                    string CaseDetailMeetingType = engagementDetails.GetCaseDetailMeetingType();
                    Assert.AreEqual(meetingTypeExl, CaseDetailMeetingType);
                    extentReports.CreateLog("Meeting Type is same on Questionnaire Detail page and Case Detail page ");
                    engagementDetails.AreCaseDetailPageQuickLinksDisplayed(fileTMTI0045436);
                    engagementDetails.CloseTab(caseNumber);
                    
                    //Verify that the user can see related list quick links on the Case page like Case comments, Case history, Questionnaires, and meetings.
                    extentReports.CreateLog("Current Tab with Case# " + caseNumber+ " is closed ");
                    questionnaireNumber = engagementDetails.GetQuestionnaireNumber();             
                    engagementDetails.CloseTab(questionnaireNumber);
                    extentReports.CreateLog("Current Tab with Questionnaire # " + questionnaireNumber + " is Closed ");                    
                }
                homePageLV.UserLogoutFromSFLightningView();
                extentReports.CreateLog("Deal Team User: "+cfUser + " Logged out ");
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
                //TMTI0045485 Verify that the CF User can Submit to CST for review, once the CST Questionnaire has all required fields filled in
                
                //TMTI0045547	Verify that the CST users should be able to access the CST Questionnaire via Cases
                extentReports.CreateLog("Verify that the CST users should be able to access the CST Questionnaire via Cases ");
                engagementDetails.SearchCaseNumber(caseNumber);

                engagementDetails.SelectOpenCase();
                extentReports.CreateLog("User is on Case : " + caseNumber + " Detail page");
                quickLinkExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CaseDetailQuickLink",2, 1);
                Assert.IsTrue(engagementDetails.ValidateRelatedQuickLink(quickLinkExl), "Verify " + quickLinkExl + "Related Quick Link is displayed on Case: " + caseNumber + " detail page ");
                extentReports.CreateLog(quickLinkExl + " Related Quick Link is displayed on Case: " + caseNumber + " detail page ");
                engagementDetails.ClickRelatedQuickLink(quickLinkExl);
                string caseQuestionnaireNumber = engagementDetails.GetCaseQuestionnaireNumber();
                extentReports.CreateLog("Verify User can click on the Case Link from Questionnaire Related Quick Link ");
                
                //Verify User can click on the Case Link from Questionnaire Related Quick Link
                extentReports.CreateLog("Questionnaire Number:: " + caseQuestionnaireNumber);
                //Verify that the CST users navigate to Questionnaire page after clicking Questionnaire number and able to see submitted Questionnaire details(Questionnaire Number)
                engagementDetails.ClickQuestionnairesLink();
                questionnaireNumber = engagementDetails.GetQuestionnaireNumber();
                Assert.AreEqual(caseQuestionnaireNumber, questionnaireNumber, "Validate selected Questionnaire Detail page is dislayed is Opened ");
                extentReports.CreateLog("Selected Questionnaire Detail page is dislayed is Opened with same Questionnaire Number  ");
                
                //TMTI0045538 Verify that the case initial status coming pending upon creation and changed to Assigned once Co-ordinatior is assigned to the case.
                extentReports.CreateLog("Verify that the case initial status coming pending upon creation and changed to Assigned once Co-ordinatior is assigned to the case. ");
                engagementDetails.ClickCaseNumber();
                //look for the Accept button on top right and accept the case to become case owner.
                Assert.IsTrue(engagementDetails.IsBtnAcceptDisplayed(), "Verify Accept button is Displayed on Case Detail Page and Accept the Case to become the Case Owner ");
                string message = engagementDetails.ClickBtnAccept();
                Assert.IsTrue(message.Contains("accepted"));
                extentReports.CreateLog(caseNumber + ":: " + message+" ");
                string caseOwner = engagementDetails.GetCaseOwner();
                Assert.AreEqual(caseOwner, valUser);
                extentReports.CreateLog("Case Owner ::" + caseOwner);
                engagementDetails.CloseTab(questionnaireNumber);
                engagementDetails.CloseTab(quickLinkExl);
                
                //Verify that after assigning Case Co-Ordinator case status changed from Pending to Assigned
                engagementDetails.GetCaseStatus();
                extentReports.CreateLog("Before Assign to Co-Ordinator Case Status: "+ engagementDetails.GetCaseStatus()+" ");
                engagementDetails.AssignToCoOrdinator(valUser);
                engagementDetails.ClickFormSave();
                Assert.AreEqual("Assigned", engagementDetails.GetCaseStatus());
                extentReports.CreateLog("After Assign to Co-Ordinator Case Status: " + engagementDetails.GetCaseStatus()+" and Case Co-Ordinator is "+ valUser+" ");
                
                //TMTI0045556 Verify that the cases are only visible(Read/ Write permission) to the users who are part of the CST group
                extentReports.CreateLog("Verify that the cases are only visible(Read/ Write permission) to the users who are part of the CST group  ");
                engagementDetails.EditCaseDetails(valUser);
                engagementDetails.ClickFormSave();
                Assert.IsFalse(engagementDetails.IsFieldsWarningMessageDisplayed(), "Verify Required Fields Warning message box is not Displayed after filling all the Required Fields ");
                extentReports.CreateLog("Questionnaire Case values are updated and saved by the CST User ");

                //TMTI0045516,TMTI0049645 Verify that the CST users should be able to add multiple meetings on Case page
                //TMTI0048957 Verify that Case Owner/Assigned Coordinator/Backup Coordinator will have complete access on Cases and its related objects
                extentReports.CreateLog("Verify that the CST users should be able to add multiple meetings on Case page ");
                string conterpartyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Counterparty", 2, 2);
                for(int count = 1; count <= 2; count++)
                {
                    engagementDetails.AddMeeting(conterpartyNameExl);
                    engagementDetails.ClickFormSave();
                    string meetingNumber = engagementDetails.GetMeetingNumberToastMessage();
                    extentReports.CreateLog(meetingNumber + " is created ");
                    engagementDetails.CloseTab(meetingNumber);
                }
                quickLinkExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CaseDetailQuickLink", 3, 1);
                string meetingCount = engagementDetails.GetRelatedListQuicklinkCount(quickLinkExl, caseNumber);
                extentReports.CreateLog(meetingCount + " Meeting are created and Count is retrieved from Meeting Related Quick Link ");
                
                //CST Group User Add Comments
                extentReports.CreateLog("Verify that Case Owner is able to add case comments on the Case Detail page ");
                Assert.IsTrue(engagementDetails.IsRelatedTabDisplayed(), "Verify Related Tab is Displayed on Case Detail page ");
                Assert.IsTrue(engagementDetails.IsCommentSectionDisplayed(), "Verify Comments section is Displayed under Related Tab on Case Detail page ");
                quickLinkExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CaseDetailQuickLink", 4, 1);
                string commentsExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CaseDetailQuickLink",4, 2);

                string txtMsg = engagementDetails.AddCaseComments(commentsExl);
                string savedComments = engagementDetails.GetSavedCaseComments();
                Assert.AreEqual(savedComments, commentsExl);
                extentReports.CreateLog("Case Owner is able to add case comments on the Case details page ");
                string commentsCount = engagementDetails.GetRelatedListQuicklinkCount(quickLinkExl, caseNumber);
                extentReports.CreateLog(commentsCount + " Comments are added ");

                //CST Group User Add Notes to the Case
                extentReports.CreateLog("Verify that Case Owner is able to add case Notes on the Case Detail page ");
                quickLinkExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CaseDetailQuickLink", 5, 1);
                string notesCommentsExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CaseDetailQuickLink", 5, 2);
                engagementDetails.AddCaseNotes(notesCommentsExl);

                string notesCount = engagementDetails.GetRelatedListQuicklinkCount(quickLinkExl, caseNumber);
                //Assert.AreNotEqual(notesCount, "0");
                extentReports.CreateLog(notesCount + " Notes is/are added ");

                //CST Group User Add File to Case
                extentReports.CreateLog("Verify that Case Owner is able to Upload File on the Case Detail page ");
                quickLinkExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CaseDetailQuickLink", 6, 1);
                successMsg = engagementDetails.CSTCaseUploadFile(fileToUpload);
                Assert.AreEqual(successMsg, "1 file was added to the Case.");
                string fileCount = engagementDetails.GetRelatedListQuicklinkCount(quickLinkExl, caseNumber);
                //Assert.AreNotEqual(fileCount, "0");
                extentReports.CreateLog(fileCount+" file was/were added ");
                extentReports.CreateLog(successMsg);

                engagementDetails.CloseTab(caseNumber);
                homePageLV.UserLogoutFromSFLightningView();
                extentReports.CreateLog("CST Group User:: " + cfUser + " Logged out ");

                //TMTI0045519 Verify the Case Delete option available for System Admin
                extentReports.CreateLog("Verify Sysem Admin is able to delete the Case from Cases List ");
                login.SwitchToLightningExperience();
                homePageLV.ClickAppLauncher();
                homePageLV.SelectApp(appNameExl);
                homePageLV.SelectModule(moduleNameExl1);
                engagementDetails.SelectListView("All Open Cases");
                engagementDetails.SearchCaseNumber(caseNumber);
                Assert.IsTrue(engagementDetails.IsDeleteOptionDisplayed(),"Verify Delete Option is Displayed under Row Action icon ");
                extentReports.CreateLog("Delete Option is Displayed under Row Action icon for System Admin ");
                successMsg= engagementDetails.DeleteRecord();
                Assert.IsTrue(successMsg.Contains(caseNumber),"Verify the Case Number is present Toast Message ");
                extentReports.CreateLog(successMsg);
                engagementDetails.SwitchToClassicView();
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
            //*Delete the saved/submittedCST via Admin*//
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
