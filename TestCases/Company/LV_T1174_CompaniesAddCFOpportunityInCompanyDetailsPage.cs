using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Company;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using NUnit.Framework;
using System;
using OpenQA.Selenium.DevTools.V125.Browser;

namespace SalesForce_Project.TestCases.Company
{
    class LV_T1174_CompaniesAddCFOpportunityInCompanyDetailsPage:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        HomeMainPage homePage = new HomeMainPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        OpportunityDetailsPage oppDetails = new OpportunityDetailsPage();

        public static string fileTC1174 = "LV_T1174_CompaniesAddCFOpportunityOnCompanyDetailPage";
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
        [Test]
        public void AddCFOpportunityInCompanyDetailsPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1174;
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
                for (int row = 2; row <= rowCompanyName-2; row++)
                {
                    
                    // Calling Search Company function
                    string companyType = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    string companyNameExl= ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    companyHome.SearchCompanyInLightning(companyNameExl);
                    string companyDetailHeading = companyDetail.GetCompanyDetailsHeadingLV();
                    Assert.AreEqual(companyNameExl, companyDetailHeading);
                    extentReports.CreateStepLogs("Passed", "Page with heading: " + companyDetailHeading + " is displayed upon searching company ");

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

                    //Pre -requisite to clear mandatory values on add opportunity page.
                    addOpportunity.ClearPreFilledMandatoryValuesOnAddOpportunityLV();
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                    string opportunityName = addOpportunity.AddCompanyOpportunitiesLightningView(companyNameExl, valOppLOBExl, valJobType, fileTC1174);
                    //AddOpportunitiesLightningV3(valOppLOBExl, valJobType, fileTC1174);//updated move to jobtype
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTC1174);
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

                    //Validate Industry group
                    // validate Sector
                    string sector = oppDetails.GetSectorLV();
                    string sectorExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 4);
                    Assert.IsTrue(sector.Contains(sectorExl));
                    extentReports.CreateStepLogs("Passed", "Sector: " + sector + " in add opportunity page matches on Opportunity details page ");

                    randomPages.DetailPageFullViewLV();
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
                    string beneOwnerInfo = oppDetails.GetBeneOwnerAndControlPersonFormLV();
                    string beneOwnerInfoExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 10);
                    Assert.AreEqual(beneOwnerInfoExl, beneOwnerInfo);
                    extentReports.CreateStepLogs("Passed", "Beneficial owner and control person form: " + beneOwnerInfo + " in add opportunity page matches on Opportunity details page ");

                    //Validate primary office
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
                    extentReports.CreateStepLogs("Passed", "External disclosure status " + extDisclosureStat + " as opportunity external disclosure status matches on Opportunity details page ");

                    //Validate staffMember 
                    string staffMember = oppDetails.GetStaffMemberLV();
                    string staffMemberExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 14);
                    Assert.AreEqual(staffMemberExl, staffMember);
                    extentReports.CreateStepLogs("Passed", "Staff member " + staffMember + " as Opportunity staff member matches on Opportunity details page ");

                    driver.SwitchTo().DefaultContent();
                    randomPages.CloseActiveTab(opportunityName); 
                }
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "CF Fin User: " + valUser + " logged out\n, Add Opportunitues button is not present for Houlihan Company and Conflicts Check LDCCR Company Type");
                driver.Quit();
                extentReports.CreateStepLogs("Passed","Browser Closed");
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