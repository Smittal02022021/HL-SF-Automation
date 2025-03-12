using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Company;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTC0034016_3_VerifyTheFunctionalityOfCoverageTabOnCompanySystemAdmin:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        HomeMainPage homePage = new HomeMainPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        AddCoverageTeam coverageTeam = new AddCoverageTeam();
        CoverageTeamDetail coverageTeamDetail = new CoverageTeamDetail();

        public static string fileTMTC0034016 = "LV_TMTC0034016_VerifyTheFunctionalityOfCoverageTabOnCompany";

        private int rowCompanyName;
        private string newCompanyName;
        private string companyNameExl;
        private string excelPath;
        private string valUser;
        private string coverageID;
        private string OfficerExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMT0076447 Verify that the required field validation appears on clicking the "Save" button without filling in any details on the New Coverage Team - FS Coverage Team
        //TMT0076449 Verify that clicking the "Cancel" button of the New FS Coverage Team navigates the user back to the Coverage screen
        //TMT0076451 Verify that clicking the "Save" button of the New FS Coverage Team creates the Coverage Team with the provided details and redirects the user to the Coverage Team detail page with a success message on the screen
        //TMT0076453 Verify that the created FS Coverage Officer will be listed under the Sponsor Coverage section of the Coverage tab
        //TMT0076455 Verify the functionality of the "Edit" action button on the Sponsor Coverage record
        //TMT0076457 Verify that the user is able to add "Coverage Team Comment" on the FS Coverage Team Member and redirect the user to Coverage Team Comment followed by a success message
        //TMT0076459 Verify that the user can "Edit" the Coverage Team Comment added on the FS Coverage Team Officer
        //TMT0076461 Verify that the user can add "Coverage Contact" on the FS Coverage Team Member and redirect the user to the Coverage Contact detail page followed by a success message
        //TMT0076463 Verify that the user can "Edit" Coverage Contact added on the FS Coverage Team Officer
        //TMT0076471 Verify the functionality of the "Delete" button on the Sponsor Coverage Officer detail page
        
        [Test]
        public void VerifyTheFunctionalityOfCoverageTabOnCompanyLV()
        {
            try
            {
                //Get path of Test data file
                excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0034016;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "CF Fin User: " + valUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged in on Lightning View");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    string btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 2, 1);
                    companyHome.ClickButtonCompanyHomePageLV(btnNameExl);

                    string valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    companySelectRecord.SelectCompanyRecordTypeAndClickNextLV(valRecordTypeExl);
                    // Select company record type                    
                    string createCompanyPage = createCompany.GetCreateCompanyPageHeaderLV();
                    Assert.IsTrue(createCompanyPage.Contains("New Company"));
                    extentReports.CreateStepLogs("Passed", "Page with heading: " + createCompanyPage + " is displayed upon selecting company record type ");

                    // Validate company type display as selected 
                    Assert.AreEqual(valRecordTypeExl, createCompany.GetSelectedCompanyTypeLV());
                    extentReports.CreateStepLogs("Passed", "Selected company type: " + valRecordTypeExl + " choosen on select company record type page is matching on Company create page ");
                    // Create a  company
                    createCompany.CreateNewCompanyLV(fileTMTC0034016, row);
                    extentReports.CreateStepLogs("Info", " New Company Created ");
                    //Validate company detail heading
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    newCompanyName = companyDetail.GetCompanyNameHeaderLV();
                    Assert.IsTrue(newCompanyName.Contains(companyNameExl));
                    extentReports.CreateStepLogs("Passed", valRecordTypeExl + " Company created and name :" + newCompanyName + " displayed on Company Detail page Header ");

                    Assert.IsTrue(companyDetail.IsCoverageTabDisplayedLV(), "Verify the availability of the 'Contacts' tab on the Company detail page");
                    extentReports.CreateStepLogs("Passed", valRecordTypeExl + " 'Contacts' tab is available on " + newCompanyName + " Company Detail page ");
                    
                    companyDetail.ClickCoverageTabLV();
                    extentReports.CreateStepLogs("Passed", " 'New' button is displayed in the Industry Coverage section of the Company Details Page.");

                    //TMT0076447 Verify that the required field validation appears on clicking the "Save" button without filling in any details on the New Coverage Team - FS Coverage Team
                    coverageTeam.ClickNewButtonSponsorCoverageDisplayedLV();
                    coverageTeam.ClickNextButtonRecordTypeLV();
                    coverageTeam.ClickSaveNewCoverageTeamButtonLV();
                    string actualRequiredFields = coverageTeam.GetNewCoverageTeamReqFieldsLV();
                    string expectedRequiredFields = ReadExcelData.ReadDataMultipleRows(excelPath, "FSCoverageTeam", row, 4);
                    Assert.AreEqual(expectedRequiredFields, actualRequiredFields);

                    //TMT0076449 Verify that clicking the "Cancel" button of the New FS Coverage Team navigates the user back to the Coverage screen
                    coverageTeam.ClickCancelNewCoverageTeamButtonLV();
                    Assert.IsTrue(coverageTeam.IsNewButtonSponsorCoverageDisplayedLV(), "Verify the availability of the 'New' button in the Sponsor Coverage section of the Company Details Page.");
                    extentReports.CreateStepLogs("Passed", "Calceling the New Sponsor Coverage form redirects the user to  Coverage page");

                    //TMT0076451 Verify that clicking the "Save" button of the New FS Coverage Team creates the Coverage Team with the provided details and redirects the user to the Coverage Team detail page with a success message on the screen
                    coverageTeam.ClickNewButtonSponsorCoverageDisplayedLV();
                    coverageTeam.ClickNextButtonRecordTypeLV();
                    OfficerExl = ReadExcelData.ReadDataMultipleRows(excelPath, "FSCoverageTeam", row, 1);
                    string tierExl = ReadExcelData.ReadDataMultipleRows(excelPath, "FSCoverageTeam", row, 2);
                    string levelExl = ReadExcelData.ReadDataMultipleRows(excelPath, "FSCoverageTeam", row, 5);
                    string typeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "FSCoverageTeam", row, 6);
                    string msgSuccess = coverageTeam.AddNewCoverageTeamLV(OfficerExl, tierExl, levelExl, typeExl);
                    extentReports.CreateStepLogs("Passed", "New FS Coverage Team Added with Success Message: " + msgSuccess);
                    string coverageTeamId = coverageTeamDetail.GetCoverageTeamIDLV();
                    extentReports.CreateStepLogs("Passed", "New FS Coverage Team with ID: " + coverageTeamId + " created and user is redirected to Coverage Team Detail Page");
                    randomPages.CloseActiveTab(coverageTeamId);

                    //TMT0076453 Verify that the created FS Coverage Officer will be listed under the Sponsor Coverage section of the Coverage tab
                    Assert.IsTrue(companyDetail.IsSponsorCoverageTamMemberDisplayedRelatedTabListLV(OfficerExl), "Verify that the created FS Coverage Officer Team Member will be listed under the Sponsor Coverage section of the Coverage tab");
                    extentReports.CreateStepLogs("Passed", "Created Standard Coverage Team Member is listed under the Sponsor Coverage section of the Coverage tab");

                    //TMT0076455 Verify the functionality of the "Edit" action button on the Sponsor Coverage record
                    companyDetail.ClickEditLinkSCRecordLV();
                    tierExl = ReadExcelData.ReadDataMultipleRows(excelPath, "FSCoverageTeam", row, 7);
                    msgSuccess = coverageTeam.UpdateCoverageTeamTierLV(tierExl);
                    extentReports.CreateStepLogs("Passed", "Tier updated with Success Message: " + msgSuccess);

                    string coverageTeamTier = companyDetail.GetSponsorCoverageTeamMemberTierLV();
                    Assert.AreEqual(tierExl, coverageTeamTier, "Verify that the updates on the Sponsor Coverage section get reflected on the Coverage team detail page by clicking the save button of the Edit dialog box");
                    extentReports.CreateStepLogs("Passed", "Tier updated and saved on Sponsor Coverage section");

                    //TMT0076457 Verify that the user is able to add "Coverage Team Comment" on the FS Coverage Team Member and redirect the user to Coverage Team Comment followed by a success message
                    companyDetail.ClickSponsorCoverageTamMemberLV(OfficerExl);
                    coverageID = coverageTeamDetail.GetCoverageTeamIDLV();
                    Assert.AreEqual(coverageTeamId, coverageID);
                    extentReports.CreateStepLogs("Passed", "Coverage team member Clicked and user is redirected to Coverate team Detail page");
                    string coverageCommentsExl = ReadExcelData.ReadDataMultipleRows(excelPath, "FSCoverageTeam", row, 8);
                    coverageTeamDetail.SaveCoverageTeamCommentsLV(coverageCommentsExl);
                    string actualSavedComments = coverageTeamDetail.GetCoverageTeamCommentsLV();
                    Assert.AreEqual(coverageCommentsExl, actualSavedComments);
                    extentReports.CreateStepLogs("Passed", "Coverage team Comments saved");
                    
                    //TMT0076459 Verify that the user can "Edit" the Coverage Team Comment added on the FS Coverage Team Officer
                    coverageCommentsExl = ReadExcelData.ReadDataMultipleRows(excelPath, "FSCoverageTeam", row, 9);
                    coverageTeamDetail.UpdateCoverageCommentsLV(coverageCommentsExl);
                    actualSavedComments = coverageTeamDetail.GetCoverageTeamCommentsLV();
                    Assert.AreEqual(coverageCommentsExl, actualSavedComments);
                    extentReports.CreateStepLogs("Passed", "Coverage team Comments Updated");

                    //TMT0076461 Verify that the user can add "Coverage Contact" on the FS Coverage Team Member and redirect the user to the Coverage Contact detail page followed by a success message
                    coverageTeamDetail.ClickCoverageContactsPanelLV();
                    coverageTeamDetail.ClickNewCoverageContactsSectionButtonLV();
                    string coverageContactExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CoverageContact", row, 1);
                    string coverageContactFocusExl= ReadExcelData.ReadDataMultipleRows(excelPath, "CoverageContact", row, 2);

                    msgSuccess = coverageTeamDetail.AddNewCoverageContactsLV(coverageContactExl, coverageContactFocusExl, coverageID);
                    string coverageContactID = coverageTeamDetail.GetCoverageContactIDLV();
                    extentReports.CreateStepLogs("Passed", "Coverage Contact Added with Success message: " + msgSuccess);
                    randomPages.CloseActiveTab(coverageContactID);
                    
                    //TMT0076463 Verify that the user can "Edit" Coverage Contact added on the FS Coverage Team Officer
                    companyDetail.ClickCoverageTabLV();
                    companyDetail.ClickSponsorCoverageTamMemberLV(OfficerExl);
                    coverageID = coverageTeamDetail.GetCoverageTeamIDLV();
                    randomPages.RefreshActiveTab(coverageID);
                    Assert.AreEqual(coverageTeamId, coverageID);
                    extentReports.CreateStepLogs("Passed", "Coverage team member Clicked and user is redirected to Coverage team Detail page");
                    coverageTeamDetail.ClickCoverageContactsPanelLV();
                    coverageContactFocusExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CoverageContact", row, 3);

                    msgSuccess = coverageTeamDetail.UpdateCoverageContactLV(coverageContactFocusExl);
                    extentReports.CreateStepLogs("Passed", "Coverage Team updated New Focus: "+ coverageContactFocusExl+" followed by Success Message: " + msgSuccess);

                    //coverageTeamDetail.ClickCoverageSectorPanelLV();

                    randomPages.CloseActiveTab(coverageID);
                    randomPages.CloseActiveTab(companyNameExl);
                }
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Passed", companyNameExl + " found and selected");
                    companyDetail.ClickCoverageTabLV();
                    companyDetail.ClickSponsorCoverageTamMemberLV(OfficerExl);
                    coverageTeamDetail.DeleteCoverageCommentsLV();
                    extentReports.CreateStepLogs("Info", "Created Coverage Team Comments Deleted Successfully");
                    coverageTeamDetail.ClickCoverageContactsPanelLV();
                    coverageTeamDetail.DeleteCoverageContactLV();
                    extentReports.CreateStepLogs("Info", "Created Coverage Team Contact Deleted Successfully");

                    //TMT0076471 Verify the functionality of the "Delete" button on the Sponsor Coverage Officer detail page
                    coverageTeamDetail.DeleteCoverageTeamLV();
                    extentReports.CreateStepLogs("Passed", "Created Sponsor Coverage Team Deleted Successfully");
                    companyDetail.DeleteCompanyLV();
                    extentReports.CreateStepLogs("Passed", companyNameExl + " Company Deleted Successfully");

                }
                usersLogin.ClickLogoutFromLightningView();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                randomPages.CloseActiveTab(coverageID);
                randomPages.CloseActiveTab(companyNameExl);
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    try
                    {
                        companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                        companyDetail.ClickCoverageTabLV();
                        try
                        {
                            companyDetail.ClickSponsorCoverageTamMemberLV(valUser);
                            
                            coverageTeamDetail.DeleteCoverageCommentsLV();
                            extentReports.CreateStepLogs("Info", "Created Coverage Team Comments Deleted");
                            coverageTeamDetail.ClickCoverageContactsPanelLV();
                            coverageTeamDetail.DeleteCoverageContactLV();
                            extentReports.CreateStepLogs("Info", "Created Coverage Team Contact Deleted Successfully");

                            coverageTeamDetail.DeleteCoverageTeamLV();
                            extentReports.CreateStepLogs("Info", "Created Coverage Team Deleted");
                            companyDetail.DeleteCompanyLV();
                            extentReports.CreateStepLogs("Info", companyNameExl + " Company Deleted");
                        }
                        catch
                        {
                            coverageTeamDetail.ClickCoverageContactsPanelLV();
                            coverageTeamDetail.DeleteCoverageContactLV();
                            extentReports.CreateStepLogs("Info", "Created Coverage Team Contact Deleted Successfully");

                            coverageTeamDetail.DeleteCoverageTeamLV();
                            extentReports.CreateStepLogs("Info", "Created Coverage Team Deleted");
                            companyDetail.DeleteCompanyLV();
                            extentReports.CreateStepLogs("Info", companyNameExl + " Company Deleted");
                        }
                    }
                    catch
                    {
                        companyDetail.DeleteCompanyLV();
                        extentReports.CreateStepLogs("Info", companyNameExl + " Company Deleted");
                    }
                }
                usersLogin.ClickLogoutFromLightningView();
                driver.Quit();
            }
        }
    }
}