using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SF_Automation.TestCases.LV_Activities
{
    class LV_TMTC0025346_1_VerifyActivityFunctionalityOnCompanyDetailPage:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage homePageLV = new LVHomePage();
        LV_CompanyDetailsPage lvCompanyDetailsPage = new LV_CompanyDetailsPage();
        LVCompaniesActivityDetailPage lvCompaniesActivityDetailPage = new LVCompaniesActivityDetailPage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();

        public static string fileTMT0047429 = "TMT0047429_VerifyActivityFunctionalityOnCompanyDetailPage";
        string msgActivity;
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
        public void VerifyActivityFunctionality()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT0047429;
                string fileToUpload = ReadJSONData.data.filePaths.testData + "FileToUpload.txt";
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);

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
                homePage.SearchUserByGlobalSearch(fileTMT0047429, valUser);
                usersLogin.LoginAsSelectedUser();
                login.SwitchToClassicView();

                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                //extentReports.CreateLog("User: " + stdUser + " logged in ");
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + stdUser + " is able to login into lightning view. ");

                login.SwitchToLightningExperience();
                extentReports.CreateLog("User: " + stdUser + " Switched to Lightning View ");
                homePageLV.ClickAppLauncher();
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectApp(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateLog(appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                
                CompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1);
                lvCompanyDetailsPage.SearchCompanyInLightning(CompanyNameExl);
                extentReports.CreateStepLogs("Info"," Company: "+ CompanyNameExl+" found and selected ");

                /*
                                lvCompanyDetailsPage.ClickNewCompanyButton();
                                lvCompanyDetailsPage.SelectRecordType("Capital Provider");
                                CompanyNameExl = lvCompanyDetailsPage.SaveCompany();
                */

                //TMT0047429	Verify the availability of the "Activity" tab on the Company Detail Page.
                string tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 2);
                Assert.IsTrue(lvCompanyDetailsPage.IsTabAvailable(tabNameExl));
                extentReports.CreateStepLogs("Passed", tabNameExl+ " tab is available on "+ CompanyNameExl+" Company Detail Page. ");

                //TMT0047431 Verify that the Activity Chart is displayed at the top of the list view.8/14/2023
                lvCompanyDetailsPage.NavigateToAParticularTab("Activity");
                Assert.IsTrue(lvCompaniesActivityDetailPage.IsActivitiesChartDisplayed());//need to click on tab
                extentReports.CreateStepLogs("Passed", " Activities Chart is Displayed on Activity Detail page from "+ CompanyNameExl+" :Company Detail Page. ");

                //TMT0047433 Verify that the default "3 Months Ago" is selected on the Activity Chart.8/14/2023
                string activityDefaultStartDateExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityStartDate", 2, 1);
                string isDefaultDateSelected =lvCompaniesActivityDetailPage.DefaultDateSelection(activityDefaultStartDateExl);
                Assert.AreEqual("true", isDefaultDateSelected);
                extentReports.CreateStepLogs("Passed", "Activity Default Start Date is "+ activityDefaultStartDateExl+" ");

                //TMT0047435 Verify the availability of the "Add Activity" button on the right top corner of the list view.
                Assert.IsTrue(lvCompanyDetailsPage.IsAddActivityButtonDisplayed());
                extentReports.CreateStepLogs("Passed", "Activity tab is opened successfully and Add Activity button is Displayed  ");

                //TMT0047439 Verify that the error message appears on clicking the Save button without filling in data in the required fields
                lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Add Activity");
                lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Save");
                string msgSaveActivityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SaveActivityPopUpMsg", 2, 1);
                string msgSaveActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                Assert.AreEqual(msgSaveActivityExl, msgSaveActivity);
                extentReports.CreateStepLogs("Passed", "Message: "+ msgSaveActivity+ "is Displayed for Required fields ");

                //TMT0047441 Verify that the logged-in user(CF Financial User) who is creating an activity is selected as Primary HL Attendee by default. 
               
                string defaultHlAttandee = lvCompaniesActivityDetailPage.GetDefaultPrimaryHlAttandeeHLAttandee();
                Assert.AreEqual(valUser, defaultHlAttandee, " Verify that the logged-in user(CF Financial User) who is creating an activity is selected as Primary HL Attendee by default ");
                extentReports.CreateStepLogs("Passed", defaultHlAttandee + " (Logged in CF Financial User) is default Primary HL Attendee ");

                //TMT0047443 Verify that the Company from where the user is creating an Activity is selected in the "Companies Discussed" section
                string defaultCompaniesDiscussed= lvCompaniesActivityDetailPage.GetDefaultCompaniesDiscussed();
                Assert.AreEqual(CompanyNameExl, defaultCompaniesDiscussed, "Verify the Companies Discussed is the selected Company from where the user is creating an Activity ");
                extentReports.CreateStepLogs("Passed", defaultCompaniesDiscussed + " is the selected as Companies Discussed ");

                //TMT0047445 Verify that the user redirects to list view on clicking "Cancel" button of Add New Activity page.
                lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Cancel");
                Assert.IsTrue(lvCompaniesActivityDetailPage.IsActivityListDisplayed(), "Verify user redirects to list view on clicking Cancel button of Add New Activity page ");
                extentReports.CreateStepLogs("Passed", "User redirected to Activity list view on clicking Cancel button from Add New Activity page ");

                //TMT0047447 Verify that the activity of type "Meeting" is created by clicking the "Save" button and redirecting the user to the list view with a success message.
                //TMT0047449 Verify that the user is able to create an activity of type - Call by clicking the "Save" button and redirect the user to the list view with a success message.
                //TMT0047451 Verify that the user is able to create an activity of type - Email on clicking the "Save" button and redirects the user to the list view with a success message.
                //TMT0047453 Verify that the user is able to create an activity of type - Other on clicking the "Save" button and redirects the user to the list view with the success message.

                int rowActivity = ReadExcelData.GetRowCount(excelPath, "Activity");
                
                for (int row = 2; row <= rowActivity; row++)
                {
                    string type = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 1);
                    string subject = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 2);
                    string industryGroup = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 3);
                    string productType = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 4);
                    string description = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 5);
                    string meetingNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 6);
                    string extAttendee = ReadExcelData.ReadDataMultipleRows(excelPath, "Activity", row, 7);

                    lvCompanyDetailsPage.CreateNewActivityFromCompanyDetailPage(type, subject, industryGroup, productType, description, meetingNotes, extAttendee);
                    msgSaveActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                    msgSaveActivityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SaveActivityPopUpMsg", 2, 2);
                    Assert.AreEqual(msgSaveActivityExl, msgSaveActivity);
                    extentReports.CreateStepLogs("Passed", "Message: " + msgSaveActivity + "is Displayed for Activity Type: " + type + " ");
                    Assert.IsTrue(lvCompaniesActivityDetailPage.IsActivityListDisplayed(), "Verify user redirects to list view on clicking Cancel button of Add New Activity page ");
                    extentReports.CreateStepLogs("Passed", "User redirected to Activity list view on clicking Cancel button from Add New Activity page ");
                    lvCompanyDetailsPage.RefreshActivitiesList();
                    //Deleting Created Activity
                    lvCompanyDetailsPage.DeleteActivity();
                }

                //TMT0047455 Verify that the user is able to create a follow-up task while creating an activity.

                lvCompanyDetailsPage.CreateNewActivityAndFollowupFromCompanyDetailPage(fileTMT0047429);
                msgActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                msgSaveActivityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SaveActivityPopUpMsg", 2, 2);
                Assert.AreEqual(msgSaveActivityExl, msgActivity);
                extentReports.CreateStepLogs("Passed", "Message: " + msgSaveActivity + "is Displayed for Activity and Followup creation ");
                lvCompanyDetailsPage.RefreshActivitiesList();
                //TMT0047459	Verify the Company Activity List view on the Activity tab.

                bool areColumnsDisplayed = lvCompanyDetailsPage.VerifyAvailableColumnsOnCompaniesActivitiesListView(fileTMT0047429);
                Assert.IsTrue(areColumnsDisplayed, "Verify the Columns of Company Activity List view on the Activity tab ");
                extentReports.CreateStepLogs("Passed", "All Columns of Company Activity List view on the Activity tab are displayed ");

                //TMT0047461	Verify that the "Primary Contact" is hyperlinked and navigates the user to the contact record(External Contact).
                string activityPrimaryContact=lvCompanyDetailsPage.GetActivityPrimayContact();
                Assert.IsTrue(lvCompanyDetailsPage.IsActivityPrimaryContactHyperlinked(), "Verify Activity Primary Contact has Hyperlink ");
                extentReports.CreateStepLogs("Passed", "Activity Primary Contact is Hyperlinked ");

                bool isContactPageDisplayed = lvCompanyDetailsPage.ClickActivityPrimaryContactHyperlink();
                Assert.IsTrue(isContactPageDisplayed, "Verify Contact Page is displayed if user clicked on Activity Primay Contact hyperlink ");
                extentReports.CreateStepLogs("Passed", "User navigated to Contact Page is displayed on Activity Primay Contact hyperlink Click ");
                lvCompanyDetailsPage.ClosePrimaryContactPage(activityPrimaryContact);

                //Deleting Created Activity
                lvCompanyDetailsPage.DeleteActivity();

                //Deleting Created Follow up
                lvCompanyDetailsPage.DeleteActivity();
                
                //// TMT0047467 Verify that clicking the "Cancel" button on the activity detail page redirects the user to the activity list view.

                //lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Cancel");
                //Assert.IsTrue(lvCompaniesActivityDetailPage.IsActivityListDisplayed(), "Verify user redirects to list view on clicking Cancel button of Add New Activity page ");
                //extentReports.CreateStepLogs("Passed", "User redirected to Activity list view on clicking Cancel button from Activity Detail page ");

                //TMT0047469	Verify that clicking the "Edit" button on the activity detail page opens up the activity detail in editable mode.

                lvCompanyDetailsPage.ClickActivityViewOption();
                extentReports.CreateStepLogs("Info", "User clicked on View option from Activities List ");
                bool isActivityetailPageEnabled= lvCompaniesActivityDetailPage.VerifyActivityDetailPageStatus();
                Assert.IsFalse(isActivityetailPageEnabled, "View action button redirects the the user to Activity Details page and page is disabled for any change ");
                extentReports.CreateStepLogs("Passed", "View action button redirects the the user to Activity Details page and page is disabled for any change ");
                lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Edit");
                isActivityetailPageEnabled = lvCompaniesActivityDetailPage.VerifyActivityDetailPageStatus();
                Assert.IsTrue(isActivityetailPageEnabled, "Verify Activity Details page is Enabled after clicking on Edit button ");
                extentReports.CreateStepLogs("Passed", "Activity Details page is Enabled after clicking on Edit button ");


                //lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Cancel");
                //extentReports.CreateStepLogs("Info", "User redirected to Activity list view on clicking Cancel button from Activity details page ");
               
                // TMT0047467 Verify that clicking the "Cancel" button on the activity detail page redirects the user to the activity list view.

                lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Cancel");
                Assert.IsTrue(lvCompaniesActivityDetailPage.IsActivityListDisplayed(), "Verify user redirects to list view on clicking Cancel button of Add New Activity page ");
                extentReports.CreateStepLogs("Passed", "User redirected to Activity list view on clicking Cancel button from Activity Detail page ");


                //TMT0047471 Verify that Primary HL Attendee is able to Edit the activity and that changes get reflected in the activity. 
                //TMT0047474 Verify that HL Attendee is able to add follow-up meetings while editing an activity.

                lvCompanyDetailsPage.ClickActivityViewOption();
                extentReports.CreateStepLogs("Info", "User clicked on View option from Activities List and redirected Activity Detail Page ");
                string updateSubjectExl = ReadExcelData.ReadDataMultipleRows(excelPath, "UpdateActivity", 2, 1);
                lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Edit");
                extentReports.CreateStepLogs("Info", "Activity Details page is Enabled after clicking on Edit button ");

                lvCompaniesActivityDetailPage.UpdateActivity(updateSubjectExl);
                extentReports.CreateStepLogs("Info", "Updated Activity Details ");

                //TMT0047474	Verify that HL Attendee is able to add follow-up meetings while editing an activity.
                lvCompaniesActivityDetailPage.CreateFollowup(fileTMT0047429);
                lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Save");
                extentReports.CreateStepLogs("Info", "Followup Details provided ");

                msgSaveActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                msgSaveActivityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SaveActivityPopUpMsg", 2, 2);
                Assert.AreEqual(msgSaveActivityExl, msgSaveActivity);
                extentReports.CreateStepLogs("Passed", "Message: " + msgSaveActivity + "is Displayed for updated Activity ");
                lvCompanyDetailsPage.RefreshActivitiesList();

                Assert.IsTrue(lvCompaniesActivityDetailPage.IsActivityListDisplayed(), "Verify user redirects to list view on clicking Save button from Activity Detail page ");
                extentReports.CreateStepLogs("Passed", "User redirected to Activity list view on clicking Save button from Activity Detail page ");
                lvCompanyDetailsPage.RefreshActivitiesList();

                string activitySubject= lvCompanyDetailsPage.GetActivitySubject();
                Assert.IsTrue(activitySubject.Contains(updateSubjectExl));
                extentReports.CreateStepLogs("Passed", "Updated Activity Details are saved when edited via Edit Button");                
                //Deleting Created Follow up
                lvCompanyDetailsPage.DeleteActivity();

                //Logout from SF Lightning View
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "User Logged Out from SF Lightning View. ");

                //Logout from SF Classic View
                usersLogin.UserLogOut();
                extentReports.CreateStepLogs("Info", "User Logged Out from SF Classic View. ");

                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
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
