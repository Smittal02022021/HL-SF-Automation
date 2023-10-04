using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.Engagement;

namespace SF_Automation.TestCases.LV_Activities
{
    class LV_TMTC0025346_6_VerifyActivityMappedSelectedOpportunityEngagementCampaign : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        LV_CompanyDetailsPage lvCompanyDetailsPage = new LV_CompanyDetailsPage();
        LVCompaniesActivityDetailPage lvCompaniesActivityDetailPage = new LVCompaniesActivityDetailPage();
        OpportunityDetailsPage opportunityDetailsPage = new OpportunityDetailsPage();
        EngagementDetailsPage engagementDetailsPage = new EngagementDetailsPage();
        CampaignDetailPage campaignDetailPage = new CampaignDetailPage();
        RandomPages randomPages = new RandomPages();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();

        public static string fileTMT0047486 = "TMT0047484_VerifyPrimaryAttendeeSelection";

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
        public void VerifyActivityMappedSelectedOpportunityEngagementCampaign()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT0047486;
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
                // homePage.SearchUserByGlobalSearch(fileTMTT0022150, user);
                extentReports.CreateStepLogs("Info", "User " + valUser + " details are displayed. ");

                //Login user
                usersLogin.SearchUserAndLogin(valUser);
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

                
                string CompanyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 1);
                lvCompanyDetailsPage.SearchCompanyInLightning(CompanyNameExl);
                extentReports.CreateStepLogs("Info"," Company: "+ CompanyNameExl+" found and selected ");
                
                /*
                lvCompanyDetailsPage.ClickNewCompanyButton();
                lvCompanyDetailsPage.SelectRecordType("Capital Provider");
                CompanyNameExl = lvCompanyDetailsPage.SaveCompany();
                */
                string tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", 2, 2);
                extentReports.CreateStepLogs("Info", tabNameExl + " tab is available on " + CompanyNameExl + " Company Detail Page. ");
                lvCompanyDetailsPage.NavigateToAParticularTab("Activity");
                extentReports.CreateStepLogs("Info", " User navigated to Activity Detail page from " + CompanyNameExl + " :Company Detail Page. ");
                           

                lvCompanyDetailsPage.CreateNewActivitywithAllFieldsFromCompanyDetailPage(fileTMT0047486);
                extentReports.CreateStepLogs("Info", " User navigated to Add Activity Detail page ");

                lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Save");
                //Get popup message
                msgSaveActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                msgSaveActivityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SaveActivityPopUpMsg", 2, 1);
                Assert.AreEqual(msgSaveActivityExl, msgSaveActivity);
                extentReports.CreateStepLogs("Passed", "Message: " + msgSaveActivity + "is Displayed for Required fields ");

                lvCompanyDetailsPage.RefreshActivitiesList();
                //Select the Activity
                lvCompanyDetailsPage.ClickActivityViewOption();
                extentReports.CreateStepLogs("Info", "User clicked on View option from Activities List and redirected Activity Detail Page ");
                
                string activitySubject = ReadExcelData.ReadData(excelPath, "Activity", 2);
                string opportunitiesDiscussed = ReadExcelData.ReadData(excelPath, "Activity", 9);
                string engagementsDiscussed = ReadExcelData.ReadData(excelPath, "Activity", 10);
                string campaignsDiscussed = ReadExcelData.ReadData(excelPath, "Activity", 11);

                //TMT0047486 Verify that Activity is mapped to Opportunity that is selected while creating an Activity. 
                //Click OppDiscussed
                lvCompaniesActivityDetailPage.ClickDicsussionItemName(opportunitiesDiscussed);
                bool isOppPageDisplayed = randomPages.IsPageHeaderDisplayedLV(opportunitiesDiscussed);
                Assert.IsTrue(isOppPageDisplayed, "Verify User is redirected Opportunities Discussed: " + opportunitiesDiscussed+" Details page");
                extentReports.CreateStepLogs("Passed", "User is redirected Opportunities Discussed: " + opportunitiesDiscussed + " Details page");


                randomPages.ClickActivityTab();
                bool isActivityMapped=opportunityDetailsPage.IsLinkedActivityDisplayed(activitySubject);
                Assert.IsTrue(isActivityMapped, "Verify Activity is mapped with Opportunity Discussed ");
                extentReports.CreateStepLogs("Passed", "Activity is mapped with Opportunity Discussed: "+ opportunitiesDiscussed+" ");
                randomPages.CloseActiveTab(opportunitiesDiscussed);

                //TMT0047488 Verify that Activity is mapped to Engagement that is selected while creating an Activity.
                //Click engagementsDiscussed
                
                lvCompaniesActivityDetailPage.ClickDicsussionItemName(engagementsDiscussed);
                bool isEngPageDisplayed = randomPages.IsPageHeaderDisplayedLV(engagementsDiscussed);
                Assert.IsTrue(isEngPageDisplayed, "Verify User is redirected Engagements Discussed: " + engagementsDiscussed + " Details page");
                extentReports.CreateStepLogs("Passed", "User is redirected Engagements Discussed: " + engagementsDiscussed + " Details page");


                randomPages.ClickActivityTab();
                isActivityMapped = engagementDetailsPage.IsLinkedActivityDisplayed(activitySubject);
                Assert.IsTrue(isActivityMapped, "Verify Activity is mapped with Engagements Discussed ");
                extentReports.CreateStepLogs("Passed", "Activity is mapped with Engagements Discussed: " + engagementsDiscussed + " ");
                randomPages.CloseActiveTab(engagementsDiscussed);

                //TMT0047490 Verify that the Activity is mapped to the Related Campaign that is selected while creating an Activity.
                //Click campaignsDiscussed
                
                lvCompaniesActivityDetailPage.ClickDicsussionItemName(campaignsDiscussed);
                bool isCampPageDisplayed = campaignDetailPage.IsPageHeaderDisplayedLV(campaignsDiscussed);
                Assert.IsTrue(isCampPageDisplayed, "Verify User is redirected Campaigns Discussed: " + campaignsDiscussed + " Details page");
                extentReports.CreateStepLogs("Passed", "User is redirected Campaigns Discussed: " + campaignsDiscussed + " Details page");  
                

                campaignDetailPage.ClickActivityTab();
                isActivityMapped = campaignDetailPage.IsLinkedActivityDisplayed(activitySubject);
                Assert.IsTrue(isActivityMapped, "Verify Activity is mapped with Campaigns Discussed ");
                extentReports.CreateStepLogs("Passed", "Activity is mapped with Campaigns Discussed: " + campaignsDiscussed + " ");
                randomPages.CloseActiveTab(campaignsDiscussed);

                lvCompaniesActivityDetailPage.ClickActivityDetailPageButton("Cancel");
                lvCompanyDetailsPage.DeleteActivity();
                msgSaveActivity = lvCompaniesActivityDetailPage.GetLVMessagePopup();
                extentReports.CreateStepLogs("Info", msgSaveActivity);
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
        }*/
    }
}
