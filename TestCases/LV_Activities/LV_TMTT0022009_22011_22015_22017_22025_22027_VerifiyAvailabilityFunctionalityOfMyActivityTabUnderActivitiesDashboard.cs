using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.LV_Activities
{
    class LV_TMTT0022009_22011_22015_22017_22025_22027_VerifiyAvailabilityFunctionalityOfMyActivityTabUnderActivitiesDashboard : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();       
        LVHomePage homePageLV = new LVHomePage();
        LV_CompanyDetailsPage lvCompanyDetailsPage = new LV_CompanyDetailsPage();

        public static string fileTMTI0050336 = "TMTT0022009_VerifiyAvailabilityFunctionalityOfMyActivityTabUnderActivitiesDashboard";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerificationOfMyActivitiesDashboardNewUIAndFunctionalityLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0050336;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");
                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in       
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login. ");

                //Search CF Financial User by global search
                //Login user
                string adminUserExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(adminUserExl);
                login.SwitchToLightningExperience();
                string userName = login.ValidateUserLightningView();
                Assert.AreEqual(userName.Contains(adminUserExl), true);
                extentReports.CreateStepLogs("Passed", "**Need to change with CF Financial User System Administrator User: " + adminUserExl + " logged in on Lightning View");
                homePageLV.ClickAppLauncher();
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectApp(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateLog(appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateLog("User is on " + moduleNameExl + " Page ");
                extentReports.CreateStepLogs("Info", "User has navigated to Homepage tab under Home option from HL Banker dropdown. ");

                Assert.IsTrue(homePageLV.IsActivitiesTabAvailable(), "Verify If Activities Tab Is Available Next To Eng And Opp Filters");
                extentReports.CreateStepLogs("Passed", "Activities filter grid is available next to Engagement & Opportunity filters. ");

                //TMTI0050336 Verify the availability of My Activity tab under Activities filter
                Assert.IsTrue(homePageLV.IsMyActivityTabDisplayedUnderActivities(), "Verify the availability of My Activity tab under Activities filter");
                extentReports.CreateStepLogs("Passed", "My Activity tab is available on Activities dashboard for System Admin/CF users. ");

                //TMTI0050215 Verify the availabilty of Activity Start Date Filter on Activity Dashboard.
                Assert.IsTrue(homePageLV.AreFilterOptionsCorrectOnActivityDashboard(fileTMTI0050336), "Verify the availabilty of Activity Start Date Filter on Activity Dashboard");
                extentReports.CreateStepLogs("Passed", "Expected Activity filter options are available on Activity Dashboard. ");

                //TMTI0050222	Verify that Activity Start Date Filter cell is bydefault selected 7 days ago filtering
                string expectedDefaultFilterOption = ReadExcelData.ReadData(excelPath, "StartDateFilterOptions",1);
                string actualDefaultFilterOption = homePageLV.GetDefaultSelectedValueInActivityStartDateFilter();
                Assert.AreEqual(expectedDefaultFilterOption, actualDefaultFilterOption);
                extentReports.CreateStepLogs("Passed", "Default value selected under Activity Start Date Filter is: " + actualDefaultFilterOption);

                //TMTI0050252	Verify the available coulmns on My Activity dashboard in detail activities table
                Assert.IsTrue(homePageLV.AreAvailableColumnsInActivitiesTableCorrectOnMyActivityDashboard(fileTMTI0050336), "Verify the available coulmns on My Activity dashboard in detail activities table");
                extentReports.CreateStepLogs("Passed", "All columns are displayed as expected on My Activity Dashboard in detail activities table. ");

                //TMTI0050235	Verify the  availability of KPI banner metrices on My Activity Dashboard
                Assert.IsTrue(homePageLV.IsKPIMetricesCorrectOnMyActivitiyDashboard(fileTMTI0050336));
                extentReports.CreateStepLogs("Passed", "All KPI Metrices are available on My Activity Dashboard. ");

                //TMTI0050219	Verify the functionality of Activity Start Date grid cells available on My Activity Dashboard
                Assert.IsTrue(homePageLV.IsActivityListSortedWithSelectedActivityStartDateFilterOnMyActivityDashboard(),"Verify the functionality of Activity Start Date grid cells av ailable on My Activity Dashboard");
                extentReports.CreateStepLogs("Passed", "The functionality of Activity Start Date grid filter is working as expected. ");

                //TC - TMTI0050237	Verify the functionality of KPI metrices on My Activity Dashboard
                Assert.IsTrue(homePageLV.AreKPIMetricesCorrectOnMyActivityDashboard(fileTMTI0050336), "Verify the functionality of KPI metrices on My Activity Dashboard");
                extentReports.CreateStepLogs("Passed", "The functionality of KPI Metrices Lable Count is correct for each KPI details page. ");

                // TMTI0050263 Verify that Subject of each activity should be clickable and should redirect user to the activity in a new tab when clicked
                string actionMenu = homePageLV.GetActionMenuText();
                Assert.AreEqual("Open Record", actionMenu, "Verify Subject name field having small arrow 'Open Record' ");
                extentReports.CreateStepLogs("Passed", "Subject name field have Arrow with Menu "+ actionMenu);

                //Verify after clicking Open Record Activity detail page opens in a new tab.
                Assert.IsTrue(homePageLV.IsActivityOpenNewWindow(), "Verify after clicking Open Record Activity detail page opens in a new tab");
                extentReports.CreateStepLogs("Passed", "After clicking Open Record Activity detail page opens in a new tab");

                //TMTI0050268	Verify that Meeting/Call notes coulmn in Activity detail table should have a small arrow and while click it will give the user the ability to update those notes in real time
                string msgSuccess = homePageLV.UpdateActivityMeetingCallNotes();
                Assert.IsTrue(msgSuccess.Contains("saved"));
                extentReports.CreateStepLogs("Passed", "Activity Meeting Call Notes saved with Success Message: "+ msgSuccess);
                /////////////////////////////////////////////Below are not updated yet////////////////
                //TC - TMTI0054960 - Check the functionality for adding new activities and verify added activity in My Coverage dashboard
                //homePageLV.NavigateToAnItemFromHLBankerDropdown("Companies");
                //extentReports.CreateStepLogs("Info", "User has navigated to Companies option from HL Banker dropdown. ");

                //string companyName = ReadExcelData.ReadData(excelPath,"Company",1);
                //homePageLV.SearchCompanyFromMainSearch(companyName);
                //extentReports.CreateStepLogs("Info", "Company : " + companyName + " detail page is opened. ");

                //lvCompanyDetailsPage.NavigateToAParticularTab("Coverage");
                //Assert.IsTrue(lvCompanyDetailsPage.VerifyCoverageTabIsOpened());
                //extentReports.CreateStepLogs("Passed", "Coverage tab is opened successfully. ");

                //Assert.IsTrue(lvCompanyDetailsPage.VerifyLoggedInUserHasIndustryCoverageForACompany(adminUserExl));
                //extentReports.CreateStepLogs("Passed", "User : " + adminUserExl + " has coverage for the company : " + companyName + " .");

                //lvCompanyDetailsPage.NavigateToAParticularTab("Activity");
                //Assert.IsTrue(lvCompanyDetailsPage.VerifyActivityTabIsOpened());
                //extentReports.CreateStepLogs("Passed", "Activity tab is opened successfully. ");

                //lvCompanyDetailsPage.CreateNewActivityFromCompanyDetailPage(fileTMTI0050336);
                //homePageLV.NavigateToHomePageTabFromHLBankerDropdown();
                //Assert.IsTrue(homePageLV.VerifyIfActivitiesFilterGridIsAvailableNextToEngAndOppFilters());
                //Assert.IsTrue(homePageLV.VerifyIfUserCanSeeMyCoverageTabUnderActivitiesFilter());

                ////extentReports.CreateStepLogs("Passed", "A new activity is created and is visible under My Coverage dashboard. ");

                ////Logout from SF Lightning View                
                login.SwitchToClassicView();
                usersLogin.UserLogOut();                
                extentReports.CreateStepLogs("Info", "Switched to classic ");
                //Logout from SF Classic View
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
