using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_TMTT0024069_VerifyNewJobTypesInJobTypeColumnOnOpportunityEngagementSearchpage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        EngagementHomePage engagementHome = new EngagementHomePage();

        public static string fileTMTI0055401 = "TMTI0055401_VerifyNewJobTypesInJobTypeColumnOnOpportunityEngagementSearchpage";
        string moduleNameExl;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void SearchCFOpportunityWithNewJobTypeLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0055401;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowOpp = ReadExcelData.GetRowCount(excelPath, "JobType");
                for (int row = 2; row <= rowOpp; row++)                {

                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 1);

                    //Login as Standard User profile and validate the user
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "StandardUsers", row, 1);
                    usersLogin.SearchUserAndLogin(valUser);
                    login.SwitchToClassicView();

                    string stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser + " logged in ");

                    login.SwitchToLightningExperience();
                    extentReports.CreateLog("User: " + stdUser + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();

                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");

                    //TMTI0055401 Verify the availability of new Job Types in Job Type column on Opportunity Search page
                    // Existing Opportunity with desired Job Type
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");                    
                    string valOppName = ReadExcelData.ReadDataMultipleRows(excelPath, "Opportunities", row, 1);
                    Assert.IsTrue(opportunityHome.SearchRecentOpportunityLV(valOppName),"Verify searched Opportunity is found ");
                    Assert.AreEqual(valJobType, opportunityHome.GetSearchedOppJobType(), "Verify searched Opportunity have the newly added Job Type ");
                    extentReports.CreateLog("Opportunity: "+valOppName+" is found with Job Type: " + valJobType + " on Opportunity Home Page ");


                    //TMTI0055390 Verify the availability of new Job Types in Job Type column on Engagement Search page
                    // Existing Engagement with desired Job Type// search the eng once before execution 
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");
                    string valEngName = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagements", row, 1);
                    Assert.IsTrue(engagementHome.SearchRecentEngagementLV(valEngName), "Verify searched Engagement is found ");
                    Assert.AreEqual(valJobType, engagementHome.GetSearchedEngJobType(), "Verify searched Engagement have the newly added Job Type ");
                    extentReports.CreateLog("Engagement: " + valEngName + " is found with Job Type: " + valJobType + " on Engagement Home Page ");
                }
                homePageLV.UserLogoutFromSFLightningView();
                driver.Quit();
                extentReports.CreateLog("Browser Closed ");
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                login.SwitchToClassicView();
                driver.Quit();
            }

        }
    }
}
