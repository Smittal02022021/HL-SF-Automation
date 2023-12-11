using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework.Internal;


namespace SF_Automation.TestCases.LV_Activities
{
    class LV_TMTC0025346_2_VerifyActivityAccessRelatedFunctionalityOnCompanyDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage homePageLV = new LVHomePage();
        LV_CompanyDetailsPage lvCompanyDetailsPage = new LV_CompanyDetailsPage();
        LVCompaniesActivityDetailPage lvCompaniesActivityDetailPage = new LVCompaniesActivityDetailPage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();

        public static string fileTMT0047476 = "TMT0047476_VerifyActivityAccessRelatedFunctionalityOnCompanyDetailPage";
        string msgSaveActivity;
        string msgSaveActivityExl;
        string CompanyNameExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyActivityAccessRelatedFunctionalityOnCompanyDetailPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT0047476;
                string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2,1);
                string fileToUpload = ReadJSONData.data.filePaths.testData + "FileToUpload.txt";

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login. ");

                //Search CF Financial User user by global search                
                extentReports.CreateStepLogs("Info", "User " + valUser + " details are displayed. ");

                //Login user
                homePage.SearchUserByGlobalSearch(fileTMT0047476, valUser);
                usersLogin.LoginAsSelectedUser();
                login.SwitchToClassicView();

                string cfFinancialUser = login.ValidateUser();
                Assert.AreEqual(cfFinancialUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + cfFinancialUser + " is able to login into lightning view. ");

                login.SwitchToLightningExperience();
                extentReports.CreateLog("User: " + cfFinancialUser + " Switched to Lightning View ");
                homePageLV.ClickAppLauncher();
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectApp(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Info", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
               
                CompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1);
                lvCompanyDetailsPage.SearchCompanyInLightning(CompanyNameExl);
                extentReports.CreateStepLogs("Info", " Company: " + CompanyNameExl + " found and selected ");

                /*
                 lvCompanyDetailsPage.ClickNewCompanyButton();
                 lvCompanyDetailsPage.SelectRecordType("Capital Provider");
                 CompanyNameExl = lvCompanyDetailsPage.SaveCompany();
                */
                string tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 2);
                extentReports.CreateStepLogs("Info", tabNameExl + " tab is available on " + CompanyNameExl + " Company Detail Page. ");
                lvCompanyDetailsPage.NavigateToAParticularTab("Activity");
                extentReports.CreateStepLogs("Info", " User navigated to Activity Detail page from " + CompanyNameExl + " :Company Detail Page. ");

                /////////////////////////////////////////////////////
                //Crteating new Activity with additional HL Attandee
                /////////////////////////////////////////////////////
                lvCompanyDetailsPage.CreateNewActivityAdditionalHLAttandeeFromCompanyDetailPage(fileTMT0047476);
                extentReports.CreateStepLogs("Info", " User navigated to Add Activity Detail page ");
                lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Save");
                

                //Get popup message
                msgSaveActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                msgSaveActivityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SaveActivityPopUpMsg", 2, 1);
                Assert.AreEqual(msgSaveActivityExl, msgSaveActivity);
                extentReports.CreateStepLogs("Passed", "Message: " + msgSaveActivity + "is Displayed for Required fields ");
                lvCompanyDetailsPage.RefreshActivitiesList();

                //TMT0047463 Verify the available action buttons given correspond to each activity on the activity list view.

                bool areMenuOptionDisplayed = lvCompanyDetailsPage.VerifyAvailableActionsOnCompaniesActivitiesListViewLV(fileTMT0047476);//expected buttons are mached from Excel file 
                Assert.IsTrue(areMenuOptionDisplayed, "Verify the available action buttons on the activity list view ");
                extentReports.CreateStepLogs("Passed", "All action buttons of Activity List view on the Company Activity tab are displayed ");

                //TMT0047465 Verify that clicking the "View" action button opens up the activity detail page with available buttons.

                bool isActivityDetailsPageDisplayed = lvCompanyDetailsPage.ClickActivityViewOption();
                Assert.IsTrue(isActivityDetailsPageDisplayed, "Verify user is redirected to Activity Details page if click on View action button");
                extentReports.CreateStepLogs("Passed", "Activity Details page is displayed when user click on View action button ");

                //Verify Header Buttons
                bool areActivityDetailPageButtonsAvailable = lvCompaniesActivityDetailPage.VerifyActivityDetailPageAvailableHeaderButtons(fileTMT0047476);//expected buttons are mached from Excel file 
                Assert.IsTrue(areActivityDetailPageButtonsAvailable, "Verify Available buttons on Activity Detail Page");
                extentReports.CreateStepLogs("Passed", "All Buttons are displayed on Activity Details page after clicking on View action button ");

                //Verify File Upload Buttons
                bool IsFileUploadButtonDisplayed = lvCompaniesActivityDetailPage.VerifyActivityDetailPageFileUploadButton();
                Assert.IsTrue(IsFileUploadButtonDisplayed, "Verify File Upload Button buttons is displayed on Activity Detail Page");
                extentReports.CreateStepLogs("Passed", "File Upload Button displayed on Activity Details page after clicking on View action button ");

                //TMT0047502 Verify that the HL Attendee is able to save selected files using the "Upload File" button while viewing the activity.

                string uploadedFileName = lvCompaniesActivityDetailPage.UploadFileAndValidate(fileToUpload);
                extentReports.CreateStepLogs("Info", uploadedFileName + " Uploaded File Name ");

                //TMT0047494 Verify that the Activity Details on the activity are being mapped to Email Template with the same format as the Activity Details.

                lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Send Notification");
                string messagebody = lvCompaniesActivityDetailPage.GetEmailTemplate();
                //extentReports.CreateStepLogs("Info", "Template: " + messagebody + "  ");
                string type = ReadExcelData.ReadData(excelPath, "Activity", 1);
                string subject = ReadExcelData.ReadData(excelPath, "Activity", 2);
                string description = ReadExcelData.ReadData(excelPath, "Activity", 5);
                string meetingNotes = ReadExcelData.ReadData(excelPath, "Activity", 6);
                string extAttendee = ReadExcelData.ReadData(excelPath, "Activity", 7);
                string addHLAttandee = ReadExcelData.ReadData(excelPath, "Activity", 8);
                Assert.IsTrue(messagebody.Contains(type),"Verify Activity Type is added in Email Template ");
                extentReports.CreateStepLogs("Passed", "Activity Type: " + type+ " in Email Template ");
                Assert.IsTrue(messagebody.Contains(subject), "Verify Activity Subject is added in Email Template ");
                extentReports.CreateStepLogs("Passed", "Activity Subject: " + subject + " in Email Template ");
                Assert.IsTrue(messagebody.Contains(description), "Verify Activity Description is added in Email Template ");
                extentReports.CreateStepLogs("Passed", "Activity Description: " + description + " in Email Template ");
                Assert.IsTrue(messagebody.Contains(meetingNotes), "Verify Activity Meeting Notes is added in Email Template ");
                extentReports.CreateStepLogs("Passed", "Activity Meeting Notes: " + meetingNotes + " in Email Template ");
                Assert.IsTrue(messagebody.Contains(extAttendee), "Verify Activity Attendee is added in Email Template ");
                extentReports.CreateStepLogs("Passed", "Activity Ext Attendee: " + extAttendee + " in Email Template ");
                Assert.IsTrue(messagebody.Contains(addHLAttandee), "Verify Activity HL Attandee is added in Email Template ");
                extentReports.CreateStepLogs("Passed", "Activity HL Attandee: " + addHLAttandee + " in Email Template ");
                                
                lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Cancel");
                

                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                extentReports.CreateLog("User: " + cfFinancialUser + " logged out ");

                //TMT0047478 Verify that the non - activity member is not able to edit the activity.

                string nonActivityUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 4,1);
                //Search CF Financial User Non-Activity user by global search                
                extentReports.CreateStepLogs("Info", "User " + nonActivityUser + " details are displayed. ");
                //Login user
                usersLogin.SearchUserAndLogin(nonActivityUser);
                login.SwitchToClassicView();

                cfFinancialUser = login.ValidateUser();
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + nonActivityUser + " is able to login into lightning view. ");

                login.SwitchToLightningExperience();
                extentReports.CreateLog("User: " + nonActivityUser + " Switched to Lightning View ");
                homePageLV.ClickAppLauncher();
                homePageLV.SelectApp(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Info",appName + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                lvCompanyDetailsPage.SearchCompanyInLightning(CompanyNameExl);
                extentReports.CreateStepLogs("Info", " Company: " + CompanyNameExl + " found and selected ");
                extentReports.CreateStepLogs("Info", tabNameExl + " tab is available on " + CompanyNameExl + " Company Detail Page. ");
                lvCompanyDetailsPage.NavigateToAParticularTab("Activity");
                extentReports.CreateStepLogs("Info", " User navigated to Activity Detail page from " + CompanyNameExl + " :Company Detail Page. ");

                //Select the Activity
                lvCompanyDetailsPage.ClickActivityViewOption();
                extentReports.CreateStepLogs("Info", "Non-Activity User clicked on View option from Activities List ");
                lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Edit");
                //Get Validation Message
                msgSaveActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                msgSaveActivityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SaveActivityPopUpMsg", 2, 3);
                Assert.AreEqual(msgSaveActivityExl, msgSaveActivity, "Verify Non - Activity member is not able to edit the activity ");
                extentReports.CreateStepLogs("Passed", "Message: " + msgSaveActivity + "is Displayed for updated Activity ");

                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                extentReports.CreateLog("User: " + nonActivityUser + " logged out ");

                //TMT0047476 Verify that Non-Primary HL Attendee is able to Edit the activity and changes get reflected in an activity.

                valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3,1);
                //Search CF Financial User Non-Primary for Created Activity user by global search                
                extentReports.CreateStepLogs("Info", "User " + valUser + " details are displayed. ");

                //Login user
                usersLogin.SearchUserAndLogin(valUser);
                login.SwitchToClassicView();

                string nonPrimaryCFFinancialUser = login.ValidateUser();
                Assert.AreEqual(nonPrimaryCFFinancialUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + nonPrimaryCFFinancialUser + " is able to login into lightning view. ");

                login.SwitchToLightningExperience();
                extentReports.CreateLog("User: " + nonPrimaryCFFinancialUser + " Switched to Lightning View ");
                homePageLV.ClickAppLauncher();
                homePageLV.SelectApp(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Info", appName + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                lvCompanyDetailsPage.SearchCompanyInLightning(CompanyNameExl);
                extentReports.CreateStepLogs("Info", " Company: " + CompanyNameExl + " found and selected ");
                extentReports.CreateStepLogs("Info", tabNameExl + " tab is available on " + CompanyNameExl + " Company Detail Page. ");
                lvCompanyDetailsPage.NavigateToAParticularTab("Activity");
                extentReports.CreateStepLogs("Info", " User navigated to Activity Detail page from " + CompanyNameExl + " :Company Detail Page. ");
                
                //Select the Activity
                lvCompanyDetailsPage.ClickActivityViewOption();
                extentReports.CreateStepLogs("Info", "Non-Primary HL Attendee clicked on View option from Activities List and redirected Activity Detail Page ");
                string updateSubjectExl = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", 2, 1);
                lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Edit");
                extentReports.CreateStepLogs("Info", "Activity Details page is Enabled after clicking on Edit button ");

                //Edit  Activity Subject
                lvCompaniesActivityDetailPage.UpdateActivity(updateSubjectExl);
                extentReports.CreateStepLogs("Info", "Non-Primary HL Attendee Updated Activity Details ");
                lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Save");

                //Get Success Message
                msgSaveActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                msgSaveActivityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SaveActivityPopUpMsg", 2, 1);
                Assert.AreEqual(msgSaveActivityExl, msgSaveActivity);
                extentReports.CreateStepLogs("Passed", "Message: " + msgSaveActivity + "is Displayed for updated Activity ");

                //Get Activity Subject
                string activitySubject = lvCompanyDetailsPage.GetActivitySubject();
                Assert.IsTrue(activitySubject.Contains(updateSubjectExl));
                extentReports.CreateStepLogs("Passed", "Non-Primary HL Attendee Updated Activity Details and are saved when edited via Edit Button");

                //TMT0047498 Verify that the HL Attendee of the activity is able to delete the activity.
                msgSaveActivityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SaveActivityPopUpMsg", 2, 2);
                lvCompanyDetailsPage.DeleteActivity();
                msgSaveActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                Assert.AreEqual(msgSaveActivityExl, msgSaveActivity);
                extentReports.CreateStepLogs("Pass", "Activity is deleted by HL Attandee and message: "+msgSaveActivity+" is displayed ");
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                extentReports.CreateStepLogs("Info",nonPrimaryCFFinancialUser + " logged out ");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
        /*
        [TearDown]
        public void TearDown()
        {
            //companyhome.SearchCompany(CompanyNameExl);
            companyDetail.DeleteCompany(CompanyNameExl);
            Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
            extentReports.CreateStepLogs("Info", "Created company is deleted successfully ");
        }
        */
    }
}
