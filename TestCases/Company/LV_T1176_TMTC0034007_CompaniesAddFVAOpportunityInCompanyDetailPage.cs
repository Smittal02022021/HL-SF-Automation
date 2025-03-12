using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Companies
{
    class LV_T1176_TMTC0034007_CompaniesAddFVAOpportunityInCompanyDetailPage: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        HomeMainPage homePage = new HomeMainPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        OpportunityDetailsPage oppDetails = new OpportunityDetailsPage();

        public static string fileTC1176 = "LV_T1176_CompaniesAddFVAOpportunityInCompanyDetailPage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        /*Houlihan Company, Conflicts Check LDCCR
          Add Opportunity button is not available on LV
         */
        //TMT0076392 Verify the availability of the "Add FVA Opportunity" tab on the Company detail page
        //TMT0076394 Verify that clicking "Add FVA Opportunity"  navigates to the New Opportunity Page and is prefilled with the selected company's name, Line of Business, and Record Type as FVA
        //TMT0076396 Verify that clicking the "Cancel" button navigates the user back to the Company details page
        //TMT0076398 Verify that on clicking the "Save" button of New Opportunity: CF page creates the New FVA Opportunity with provided details and redirects the user to the Opportunity detail page.

        [Test]
        public void AddFVAOpportunityInCompanyDetailsPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1176;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
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
                extentReports.CreateStepLogs("Info", "CF Fin User is on " + moduleNameExl + " Page ");

                //-----Add Cancel Delete CF opportunity by all types of companies ----//
                int rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int row = 2; row <= rowCompanyName - 2; row++)
                {

                    // Calling Search Company function
                    string companyType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    companyHome.SearchCompanyInLightning(companyNameExl);
                    string companyDetailHeading = companyDetail.GetCompanyDetailsHeadingLV();
                    Assert.AreEqual(companyNameExl, companyDetailHeading);
                    extentReports.CreateStepLogs("Passed", "Page with heading: " + companyDetailHeading + " is displayed upon searching company ");

                    //TMT0076392 Verify the availability of the "Add FVA Opportunity" tab on the Company detail page
                    // Calling function ClickOpportunityButton to click on Add CF opportunity button                    
                    string valOppLOBExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 15);
                    companyDetail.ClickAddOpportunityButtonLV(valOppLOBExl);

                    //Verify edit opportunity heading page
                    string txtOpportunitypageHeading = addOpportunity.GetNewOpportunityPageHeadingLV();
                    Assert.IsTrue(txtOpportunitypageHeading.Contains(valOppLOBExl));
                    extentReports.CreateStepLogs("Passed", "Opportunity Page with heading:  " + txtOpportunitypageHeading + " is displayed upon clicking opportunity button ");

                    //Validating prefilled opportunity name N/A on LV
                    //string opportunityName = addOpportunity.GetPrefilledOpportunityName();
                    //string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    //Assert.AreEqual(companyNameExl, opportunityName);
                    //extentReports.CreateLog("Prefilled opportunity name as " + opportunityName + " is displayed ");

                    //TMT0076394	Verify that clicking "Add FVA Opportunity"  navigates to the New Opportunity Page and is prefilled with the selected company's name, Line of Business, and Record Type as FVA
                    //Validating prefilled client name
                    string clientName = addOpportunity.GetPrefilledClientNameLV();
                    Assert.AreEqual(companyNameExl, clientName);
                    extentReports.CreateStepLogs("Passed", "Prefilled client name as " + clientName + " is displayed ");

                    //Validating prefilled opportunity subject  N/A on LV
                    //string opportunitySubject = addOpportunity.GetPrefilledOpportunitySubject();
                    //Assert.AreEqual(companyNameExl, opportunitySubject);
                    //extentReports.CreateLog("Prefilled opportunity subject as " + opportunitySubject + " is displayed ");

                    //Validating prefilled line of business
                    string prefilledLineOfBusiness = addOpportunity.GetPrefilledLineOfBusinessLV();
                    Assert.AreEqual(valOppLOBExl, prefilledLineOfBusiness);
                    extentReports.CreateStepLogs("Passed", "Prefilled opportunity line of business as " + prefilledLineOfBusiness + " is displayed ");

                    //Validating prefilled Record Type
                    string addOppRecordType = addOpportunity.GetRecordTypeLV();
                    Assert.AreEqual(valOppLOBExl, addOppRecordType);
                    extentReports.CreateStepLogs("Passed", "Prefilled Record Type as " + addOppRecordType + " is displayed ");

                    //TMT0076396	Verify that clicking the "Cancel" button navigates the user back to the Company details page
                    //Click Cancel and check user is redireced to Company page 
                    addOpportunity.ClickCancelAddOpportunityPageLV();
                    randomPages.CloseActiveTab(companyNameExl);
                    companyHome.SearchCompanyInLightning(companyNameExl);
                    companyDetailHeading = companyDetail.GetCompanyDetailsHeadingLV();
                    Assert.AreEqual(companyNameExl, companyDetailHeading, "Verify that clicking the 'Cancel' button navigates the user back to the Company details page.");
                    extentReports.CreateStepLogs("Passed", "Clicking the 'Cancel' button navigates the user back to the Company details page.");

                    //TMT0076398 Verify that on clicking the "Save" button of New Opportunity: CF page creates the New FVA Opportunity with provided details and redirects the user to the Opportunity detail page.
                    companyDetail.ClickAddOpportunityButtonLV(valOppLOBExl);
                    extentReports.CreateStepLogs("Passed", "Add: " + valOppLOBExl + " Opportunity button is displayed and Clicked company page");

                    //Verify edit opportunity heading page
                    txtOpportunitypageHeading = addOpportunity.GetNewOpportunityPageHeadingLV();
                    Assert.IsTrue(txtOpportunitypageHeading.Contains(valOppLOBExl));
                    extentReports.CreateStepLogs("Passed", "Opportunity Page with heading:  " + txtOpportunitypageHeading + " is displayed upon clicking opportunity button ");

                    //Pre -requisite to clear mandatory values on add opportunity page.
                    addOpportunity.ClearPreFilledMandatoryValuesOnAddOpportunityLV();
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                    string opportunityName = addOpportunity.AddCompanyOpportunitiesLightningView(companyNameExl, valOppLOBExl, valJobType, fileTC1176);
                    //AddOpportunitiesLightningV3(valOppLOBExl, valJobType, fileTC1174);//updated move to jobtype
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTC1176);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Passed", "User is on Opportunity detail " + displayedTab + " tab ");

                    // Validate Line of business value
                    string lineOfBusiness = oppDetails.GetLOBLV();
                    string LOBExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 15);
                    Assert.AreEqual(LOBExl, lineOfBusiness);
                    extentReports.CreateStepLogs("Passed", "Line of Business: " + lineOfBusiness + " in add opportunity page matches on Opportunity details page ");

                    // Validate Job type selected
                    string jobType = oppDetails.GetJobTypeLV();
                    string jobTypeExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 3);
                    Assert.AreEqual(jobTypeExl, jobType);
                    extentReports.CreateStepLogs("Passed", "Job Type: " + jobType + " in add opportunity page matches on Opportunity details page ");

                    //Validate Industry group N/A
                    // validate Sector
                    //string sector = oppDetails.GetSectorLV();
                    //string sectorExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 4);
                    //Assert.IsTrue(sector.Contains(sectorExl));
                    //extentReports.CreateStepLogs("Passed", "Sector: " + sector + " in add opportunity page matches on Opportunity details page ");

                    //randomPages.DetailPageFullViewLV();
                    companyDetail.ClickCompanyDetailPageTabLV("Client/Subject & Referral");
                    // Validate additonal client 
                    string additionalClient = oppDetails.GetAdditionalClientLV();
                    string additionalClientExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 6);
                    Assert.AreEqual(additionalClientExl, additionalClient);
                    extentReports.CreateStepLogs("Passed", "Additional Client: " + additionalClient + " in add opportunity page matches on Opportunity details page ");

                    //Validate additional subject
                    string additionalSubject = oppDetails.GetAdditionalSubjectLV();
                    string additionalSubjectExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 7);
                    Assert.AreEqual(additionalSubjectExl, additionalSubject);
                    extentReports.CreateStepLogs("Passed", "Additional Subject: " + additionalSubject + " in add opportunity page matches on Opportunity details page ");

                    // Validate referal type 
                    string referalType = oppDetails.GetReferalTypeLV();
                    string referalTypeExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 8);
                    Assert.AreEqual(referalTypeExl, referalType);
                    extentReports.CreateStepLogs("Passed", "Referal Type: " + referalType + " in add opportunity page matches on Opportunity details page ");

                    // Validate beneficiary owner info value
                    companyDetail.ClickCompanyDetailPageTabLV("Compliance & Legal");
                    string beneOwnerInfo = oppDetails.GetBeneOwnerAndControlPersonFormLV();
                    string beneOwnerInfoExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 10);
                    Assert.AreEqual(beneOwnerInfoExl, beneOwnerInfo);
                    extentReports.CreateStepLogs("Passed", "Beneficial owner and control person form: " + beneOwnerInfo + " in add opportunity page matches on Opportunity details page ");

                    //Validate primary office
                    companyDetail.ClickCompanyDetailPageTabLV("Info");
                    companyDetail.ClickCompanyDetailPageTabLV("Administration");
                    string primaryOffice = oppDetails.GetPrimaryOfficeLV();
                    string primaryOfficeExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 11);
                    Assert.AreEqual(primaryOfficeExl, primaryOffice);
                    extentReports.CreateStepLogs("Passed", "Primary office: " + primaryOffice + " as opportunity primary office matches on Opportunity details page ");

                    //Validate legal Entity 
                    string legalEntity = oppDetails.GetLegalEntityLV();
                    string legalEntityExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 12);
                    Assert.AreEqual(legalEntityExl, legalEntity);
                    extentReports.CreateStepLogs("Passed", "Legal Entity: " + legalEntity + " as opportunity legal entity matches on Opportunity details page ");

                    //validate external disclosure  status
                    string extDisclosureStat = oppDetails.GetExternalDisclosureStatusLV();
                    string extDisclosureStatExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 13);
                    Assert.AreEqual(extDisclosureStatExl, extDisclosureStat);
                    extentReports.CreateStepLogs("Passed", "External disclosure status " + extDisclosureStat + " as opportunity external disclosure status matches on Opportunity details page instead of Do Not Disclose in Classic");

                    //Validate staffMember 
                    companyDetail.ClickCompanyDetailPageTabLV("Internal Team");
                    string staffMember = oppDetails.GetStaffMemberLV();
                    string staffMemberExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 14);
                    Assert.AreEqual(staffMemberExl, staffMember);
                    extentReports.CreateStepLogs("Passed", "Staff member " + staffMember + " as Opportunity staff member matches on Opportunity details page ");

                    driver.SwitchTo().DefaultContent();
                    randomPages.CloseActiveTab(opportunityName);
                    randomPages.CloseActiveTab("Tab");
                }
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out\r, Add Opportunitues button is not present for Houlihan Company and Conflicts Check LDCCR Company Type");
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.SwitchTo().DefaultContent();
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}