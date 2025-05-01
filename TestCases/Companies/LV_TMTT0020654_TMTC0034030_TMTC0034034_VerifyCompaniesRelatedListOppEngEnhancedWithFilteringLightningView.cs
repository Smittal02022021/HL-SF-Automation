using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTT0020654_TMTC0034030_TMTC0034034_VerifyCompaniesRelatedListOppEngEnhancedWithFilteringLightningView : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        CompanyHomePage companyhome = new CompanyHomePage();
        CompanyDetailsPage companyDetails = new CompanyDetailsPage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTMTI0046461 = "TMTI0046461_VerifyCompaniesRelatedListOpportunitiesEngagementEnhancedWithFiltering";
        public string engNumber;
        public string appNameExl;
        public string moduleNameExl;
        public string caseNumber;
        public string questionnaireNumber;
        public string successMsg;
        public string quickLinkExl;
        public string tabNameExl;
        public bool tabDetailPageDisplayed;
        public bool searchOpportunitiesBoxDisplayed;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMT0076505 Verify the availability of the "Opportunity" tab on the Company detail page
        //TMTI0046461,TMTI0046464 Verify that for related list - opportunities enhanced with filtering/search Capability on Company Page.
        //TMTI0046459 Verify that filtering/search bar is available on clicking "View All" link of opportunity related list.
        //TMTI0046465 Verify that filtering/search bar is available on clicking "View All" link of engagement related list.
        //TMT0076507 Verify that the "Opportunity" tab lists all the Opportunities in which the company is associated
        //TMTI0046462 Verify that user is capable of search an opportunity using search bar of related list under company detail pag
        //TMT0076509 Verify the "Search" functionality on the "Opportunity" tab of the Company detail page
        //TMT0076513 Verify the availability of the "Engagement" tab on the Company detail page
        //TMTI0046463,TMTI0046458 Verify that for related list - Engagement enhanced with filtering/search Capability on Company Page.
        //TMT0076515 Verify that the "Engagement" tab lists all the Engagement in which the company is associated
        //TMT0076517 Verify the "Search" functionality on the "Engagement" tab of the Company detail page


        [Test]
        public void CompaniesRelatedListOppEngEnhancedWithFilteringLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0046461;
                string fileToUpload = ReadJSONData.data.filePaths.testData + "FileToUpload.txt";
                
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                //Login again as CF Financial User
                string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + valUser + " logged in on Lightning View");

                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateLog(appName + " App is selected from App Launcher ");
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateLog(moduleNameExl + ": Module is selected from menu ");

                int companiesRowsCountExl = ReadExcelData.GetRowCount(excelPath, "Companies");
                for (int row = 2; row <= companiesRowsCountExl; row++)
                {                    
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 1);
                    companyhome.SearchCompanyInLightning(companyNameExl);
                    extentReports.CreateLog(companyNameExl + ": Company is searched and selected ");

                    string companyTypeExl= ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 2);
                    string valueCompanyType= companyDetails.GetCompanyTypeLV();
                    Assert.AreEqual(companyTypeExl, valueCompanyType);
                    extentReports.CreateLog("Selected Company Type is " +valueCompanyType+ " ");

                    //TMT0076505 Verify the availability of the "Opportunity" tab on the Company detail page
                    tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TabName", 2, 1);
                    Assert.IsTrue(companyDetails.IsCompanyDetailPageTabPresentLV(tabNameExl), "Verify the availability of the '"+ tabNameExl+"' tab on the Company detail page");
                    extentReports.CreateStepLogs("Passed", tabNameExl+" tab available on the Company detail page");

                    tabDetailPageDisplayed = companyDetails.ClickCompanyDetailPageTabLV(tabNameExl);
                    Assert.IsTrue(tabDetailPageDisplayed, "Verify Detail Page Displayed after clicking on Opportunities Tab ");
                    extentReports.CreateStepLogs("Info", "Detail Page Displayed after clicking on " + tabNameExl+" Tab ");

                    //TMTI0046461,TMTI0046464 Verify that for related list - opportunities enhanced with filtering/search Capability on Company Page.
                    
                    searchOpportunitiesBoxDisplayed = companyDetails.IsOpportunitiesSearchBoxL();
                    Assert.IsTrue(searchOpportunitiesBoxDisplayed, "Verify search Opportunities Box is Displayed after clicking on Opportunities Tab ");
                    extentReports.CreateLog("Search Opportunities Box is Displayed after clicking on Opportunities Tab ");

                    //Search functionality on Opportunity 
                    //TMTI0046462 Verify that user is capable of search an opportunity using search bar of related list under company detail pag
                    //TMT0076507 Verify that the "Opportunity" tab lists all the Opportunities in which the company is associated
                    //TMT0076509 Verify the "Search" functionality on the "Opportunity" tab of the Company detail page

                    string oppNameExcel = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 3);
                    string oppNumberExcel = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 4);
                    string oppStageExcel = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 5);

                    Assert.IsTrue(companyDetails.IsOppoortunitiesFoundByNameLV(oppNameExcel),"Verify Opportunity is found with Opportunity Name ");
                    extentReports.CreateLog("Opportunity is found with Opportunity Name:: "+ oppNameExcel+" ");
                    Assert.IsTrue(companyDetails.IsOppoortunitiesFoundByNumberLV(oppNumberExcel), "Verify Opportunity is found with Opportunity Number");
                    extentReports.CreateLog("Opportunity is found with Opportunity Number:: "+ oppNumberExcel+" ");
                    Assert.IsTrue(companyDetails.IsOppoortunitiesFoundByStageLV(oppStageExcel), "Verify Opportunity is found with Opportunity Stage");
                    extentReports.CreateLog("Opportunity is found with Opportunity Stage:: "+ oppStageExcel+" ");

                    //Search from View All
                    //TMTI0046459 Verify that filtering/search bar is available on clicking "View All" link of opportunity related list.
                    companyDetails.ClickViewAllOpportunities();
                    extentReports.CreateLog(" User clicked on View All link on "+ tabNameExl+" tab");

                    Assert.IsTrue(companyDetails.IsOppEngFoundByNameOnViewAllLV(oppNameExcel));
                    extentReports.CreateLog("Opportunity is found on View All with Opportunity Name:: " + oppNameExcel + " ");
                    Assert.IsTrue(companyDetails.IsOpportunitiesFoundByNumberOnViewAllLV(oppNumberExcel));
                    extentReports.CreateLog("Opportunity is found on View All with Opportunity Name:: " + oppNumberExcel + " ");
                    Assert.IsTrue(companyDetails.IsOppEngFoundByStageOnViewAllLV(oppStageExcel));
                    extentReports.CreateLog("Opportunity is found on View All with Opportunity Name:: " + oppStageExcel + " ");
                    
                    companyDetails.CloseViewAllPopup();
                    extentReports.CreateLog("View All Pop-Up is closed ");

                    //TMT0076513 Verify the availability of the "Engagement" tab on the Company detail page
                    tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TabName", 3, 1);
                    Assert.IsTrue(companyDetails.IsCompanyDetailPageTabPresentLV(tabNameExl), "Verify the availability of the '" + tabNameExl + "' tab on the Company detail page");
                    extentReports.CreateStepLogs("Passed", tabNameExl + " tab available on the Company detail page");

                    tabDetailPageDisplayed = companyDetails.ClickCompanyDetailPageTabLV(tabNameExl);
                    Assert.IsTrue(tabDetailPageDisplayed, "Verify Detail Page Displayed after clicking on Opportunities Tab ");
                    extentReports.CreateLog("Detail Page Displayed after clicking on "+ tabNameExl+" Tab ");

                    //TMTI0046463,TMTI0046458 Verify that for related list - Engagement enhanced with filtering/search Capability on Company Page.
                    //TMT0076515 Verify that the "Engagement" tab lists all the Engagement in which the company is associated
                    //TMT0076517 Verify the "Search" functionality on the "Engagement" tab of the Company detail page

                    searchOpportunitiesBoxDisplayed = companyDetails.IsEngagementSearchBoxLV();
                    Assert.IsTrue(searchOpportunitiesBoxDisplayed, "Verify search Engagement Box is Displayed after clicking on Engagement Tab ");
                    extentReports.CreateLog("Search Engagement Box is Displayed after clicking on Opportunities Tab ");

                    //Search functionality on Engagement
                    //TMTI0046460	Verify that user is capable of search an engagement using search bar of related list under company detail page
                    string engNameExcel = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 6);
                    string engNumberExcel = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 7);
                    string engStageExcel = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 8);

                    Assert.IsTrue(companyDetails.IsEngagementFoundByNameLV(engNameExcel), "Verify Engagement is found with Engagement Name");
                    extentReports.CreateLog("Engagement is found with Engagement Name: "+ engNameExcel+" ");
                    Assert.IsTrue(companyDetails.IsEngagementFoundByNumberLV(engNumberExcel), "Verify Engagement is found with Engagement Number");
                    extentReports.CreateLog("Engagement is found with Engagement Name: " + engNumberExcel+" ");
                    Assert.IsTrue(companyDetails.IsEngagementFoundByStageLV(engStageExcel), "Verify Engagement is found with Engagement Stage");
                    extentReports.CreateLog("Engagement is found with Engagement Name: " + engStageExcel + " ");

                    //Search from View All
                    //TMTI0046465 Verify that filtering/search bar is available on clicking "View All" link of engagement related list.
                    companyDetails.ClickViewAllEngagements();
                    extentReports.CreateLog(" User clicked on View All link on " + tabNameExl + " tab");


                    Assert.IsTrue(companyDetails.IsOppEngFoundByNameOnViewAllLV(engNameExcel));
                    extentReports.CreateLog("Engagement is found on View All with Opportunity Name:: " + engNameExcel + " ");
                    Assert.IsTrue(companyDetails.IsEngagementsFoundByNumberOnViewAllLV(engNumberExcel));
                    extentReports.CreateLog("Engagement is found on View All with Opportunity Number:: " + engNumberExcel + " ");
                    Assert.IsTrue(companyDetails.IsOppEngFoundByStageOnViewAllLV(engStageExcel));
                    extentReports.CreateLog("Engagement is found on View All with Opportunity Stage:: " + engStageExcel + " ");

                    companyDetails.CloseViewAllPopup();
                    extentReports.CreateLog("View All Pop-Up is closed ");

                    companyDetails.CloseCompanyTabLV(companyNameExl);
                    extentReports.CreateLog(companyNameExl+": Company Tab Closed ");
                }
                homePageLV.UserLogoutFromSFLightningView();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateLog("Browser Closed ");
            }

        } 
    }
}
