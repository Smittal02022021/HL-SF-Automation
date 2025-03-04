using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Net.NetworkInformation;

namespace SF_Automation.TestCases.Engagement
{
    class zTMTI0049639_VerifyTheFunctionalityOfVenueSourcingOnMeetingManagementPresentationPage: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        public static string fileTMTI0049639 = "TMTI0049639_VerifyTheFunctionalityOfVenueSourcingOn_MeetingManagementPresentationPage";
        string engNumber;
        string appNameExl;
        string moduleNameExl1;
        string caseNumber;
        string successMsg;
        string meetingNumber;
        string venueTypeExl;
        string venueNameExl;
        string venueLocationExl;
        string PhoneExl;
        string websiteExl;
        string actualVenueName;
        string actualVenueLocation;
        string actualPhone;
        string actualWebsite;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void LightniningCSTMeetingManagementPage()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0049639;
                int formRowsCountExl = ReadExcelData.GetRowCount(excelPath, "MeetingTypes");
                for (int row = 2; row <= formRowsCountExl; row++)
                {
                    Initialize();
                    
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
                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");
                    moduleNameExl1 = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl1);
                    engNumber = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagements", 2, 1);
                    engagementHome.SelectEngagement(engNumber);
                    extentReports.CreateLog("User is on Engagement:" + engNumber + " Detail page");

                    engagementDetails.ClickButtonCSQ("Client Services Questionnaire");
                    string meetingTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "MeetingTypes", row, 1);
                    engagementDetails.SelectMeetingType(meetingTypeExl);
                    engagementDetails.ButtonClickContinue();
                    //Verify that the Questionnaire details page contains different sections in the Questionnaire Information section
                    Assert.IsTrue(engagementDetails.VerifyQuestionnaireInfoPage(), "Verify Questionnaire Information page is displayed");
                    extentReports.CreateLog("Questionnaire Information page is displayed ");

                    engagementDetails.ClickEditInlineIcon();
                    engagementDetails.FillCSTFormRequiredFields(meetingTypeExl);
                    extentReports.CreateLog("All Required Fields are filled on CST Questionnaire Information Page for Meeting Type: " + meetingTypeExl + " ");
                    engagementDetails.ClickFormSave();
                    Assert.IsFalse(engagementDetails.IsFieldsWarningMessageDisplayed(), "Verify Required Fields Warning message box is not Displayed after filling all the Required Fields ");
                    extentReports.CreateLog(meetingTypeExl + ":: Required Fields Warning message box is not Displayed after filling all the Required Fields ");
                    extentReports.CreateLog("Verify that the case id has been generated once the user submits the Questionnaire ");
                    successMsg = engagementDetails.SubmitQuestionnaire();
                    extentReports.CreateLog(successMsg);
                    extentReports.CreateLog(meetingTypeExl + ":: Questionnaire Submitted ");
                    caseNumber = engagementDetails.GetCaseNumber();
                    Assert.IsNotEmpty(caseNumber);
                    extentReports.CreateLog("Case Number of Submitted Questionnaire: " + caseNumber);                    
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateLog("Deal Team User: " + cfUser + " Logged out ");

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
                    extentReports.CreateLog(caseNumber+" is opening and user is no case detail page");
                    //look for the Accept button on top right and accept the case to become case owner.
                    Assert.IsTrue(engagementDetails.IsBtnAcceptDisplayed(), "Verify Accept button is Displayed on Case Detail Page and Accept the Case to become the Case Owner ");
                    string message = engagementDetails.ClickBtnAccept();
                    Assert.IsTrue(message.Contains("accepted"));
                    extentReports.CreateLog(caseNumber + ":: " + message + " ");
                    string caseOwner = engagementDetails.GetCaseOwner();
                    Assert.AreEqual(caseOwner, valUser);
                    extentReports.CreateLog("Case Owner :: " + caseOwner);                   

                    //Verify that after assigning Case Co-Ordinator case status changed from Pending to Assigned
                    engagementDetails.GetCaseStatus();
                    extentReports.CreateLog("Before Assign to Co-Ordinator Case Status: " + engagementDetails.GetCaseStatus() + " ");
                    engagementDetails.AssignToCoOrdinator(valUser);
                    engagementDetails.ClickFormSave();
                    Assert.AreEqual("Assigned", engagementDetails.GetCaseStatus());
                    extentReports.CreateLog("After Assign to Co-Ordinator Case Status: " + engagementDetails.GetCaseStatus() + " and Case Co-Ordinator is " + valUser + " ");

                    string conterpartyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Counterparty", 2, 2);                    
                    int RowVanueTypeCount = ReadExcelData.GetRowCount(excelPath, "VenueTypesData");
                    for(int rec = 2; rec<= RowVanueTypeCount; rec++)
                    {
                        //Verify Company Selection details are synced on Meeting detail page for Vanue Type and Company.
                        extentReports.CreateLog("Add New Meeting for " + meetingTypeExl);
                        engagementDetails.AddMeeting(conterpartyNameExl);
                        engagementDetails.ClickFormSave();
                        meetingNumber = engagementDetails.GetMeetingNumberToastMessage();
                        extentReports.CreateLog(meetingNumber + " is created ");
                        venueTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "VenueTypesData", rec, 1);
                        venueNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "VenueTypesData", rec, 2);
                        venueLocationExl = ReadExcelData.ReadDataMultipleRows(excelPath, "VenueTypesData", rec, 3);
                        PhoneExl = ReadExcelData.ReadDataMultipleRows(excelPath, "VenueTypesData", rec, 4);
                        websiteExl = ReadExcelData.ReadDataMultipleRows(excelPath, "VenueTypesData", rec, 5);
                        engagementDetails.MeetingInlineEdit();
                        Assert.IsTrue(engagementDetails.ValidateVenueTypeOption(fileTMTI0049639),"Verify Venue Type Options are as expected");
                        engagementDetails.SelectVanueType(venueTypeExl);
                        engagementDetails.SelectMeetingCompany(venueNameExl);
                        engagementDetails.ClickFormSave();
                        //Verify Company Selection details are synced on Meeting detail page for selected Vanue Tupe
                        extentReports.CreateLog("Verify Company Selection details are synced on Meeting detail page for Venue Type: " + venueTypeExl);
                        actualVenueName = engagementDetails.GetVenueName();
                        actualVenueLocation = engagementDetails.GetVenueLocation();
                        actualPhone = engagementDetails.GetPhone();
                        actualWebsite = engagementDetails.GetWebsite();

                        Assert.AreEqual(actualVenueName, venueNameExl);
                        extentReports.CreateLog("Venue Name:: Actual: " + actualVenueName + " **Expected:" + venueNameExl + " ");
                        Assert.AreEqual(actualVenueLocation, venueLocationExl);
                        extentReports.CreateLog("VenueLocation:: Actual: " + actualVenueLocation + " **Expected:" + venueNameExl + " ");
                        Assert.AreEqual(actualPhone, PhoneExl);
                        extentReports.CreateLog("Phone:: Actual: " + actualPhone + " **Expected:" + PhoneExl + " ");
                        Assert.AreEqual(actualWebsite, websiteExl);
                        extentReports.CreateLog("Website:: Actual: " + actualWebsite + " **Expected:" + websiteExl + " ");
                        engagementDetails.CloseTab(meetingNumber);
                    }                                     
                    
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateLog("CST Group User : " + cfUser + " Logged out ");

                }
            }
            catch(Exception ex)
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
