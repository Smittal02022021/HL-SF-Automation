using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.CoverageTeam;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.CoverageTeam
{
    class LV_TMTT0048759_VerifyTheCaptialSolutionChangesImplementedOnCoverageTeamObject:BaseClass
    {

        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        HomeMainPage homePage = new HomeMainPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        CoverageTeamPage coverageTeamPage = new CoverageTeamPage();

        public static string fileTMTT0048759 = "LV_TMTT0048759_VerifyTheCaptialSolutionChangesImplementedOnCoverageTeamObject";
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0120128 Verify that the existing picklist values "EU Capital Markets" and "PFG – Private Funds Group" are removed from the Type field in the  New Coverage Team form.  
        //TMTI0120133 Verify that the listed new picklist values are added and displayed in the Type field in the Coverage Team details.  
        //TMTI0121298 Verify that the new picklist values "CSMA, ECS, GPA, PCA" are added and displaying in the "Expected Products" field in New Coverage Team form.  
        //TMTI0121395 Verify that the picklist values "Capital Markets, DCM , ECM " are removed in the "Expected Products" field in Coverage Team.
        
        [Test]
        public void VerifyTheCaptialSolutionChangesImplementedOnCoverageTeamObjectLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0048759;
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
                extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                //TMTI0120128	Verify that the existing picklist values "EU Capital Markets" and "PFG – Private Funds Group" are removed from the Type field in the  New Coverage Team form.  
                coverageTeamPage.ClickNewCoverageTeamButton();
                int rowType = ReadExcelData.GetRowCount(excelPath, "TypeOld");
                for (int row = 2; row <= rowType; row++)
                {                    
                    string type= ReadExcelData.ReadDataMultipleRows(excelPath, "TypeOld", row, 1);
                    string expectedResult = ReadExcelData.ReadDataMultipleRows(excelPath, "TypeOld", row, 2);
                    Assert.AreEqual(expectedResult,coverageTeamPage.IsTypePresentLV(type), "Verify that the existing picklist values "+ type+" removed from the Type field on the New Coverage Team form");
                    extentReports.CreateStepLogs("Passed", "Existing picklist value: '" + type + "' removed from the Type field and not present on the New Coverage Team form ");
                }
                coverageTeamPage.CancelFormLV();

                //TMTI0120133	Verify that the listed new picklist values are added and displayed in the Type field in the Coverage Team details.  
                coverageTeamPage.ClickNewCoverageTeamButton();
                rowType = ReadExcelData.GetRowCount(excelPath, "TypesNew");
                for (int row = 2; row <= rowType; row++)
                {
                    string type = ReadExcelData.ReadDataMultipleRows(excelPath, "TypesNew", row, 1);
                    string expectedResult = ReadExcelData.ReadDataMultipleRows(excelPath, "TypesNew", row, 2);
                    Assert.AreEqual(expectedResult, coverageTeamPage.IsTypePresentLV(type), "Verify that the New picklist values '" + type + "' updated and present in the Type field on the New Coverage Team form");
                    extentReports.CreateStepLogs("Passed", "New picklist value: '" + type + "' updated and present in the Type field on the New Coverage Team form");
                }
                coverageTeamPage.CancelFormLV();

                //TMTI0121298	Verify that the new picklist values "CSMA, ECS, GPA, PCA" are added and displaying in the "Expected Products" field in New Coverage Team form.  
                coverageTeamPage.ClickNewCoverageTeamButton();
                rowType = ReadExcelData.GetRowCount(excelPath, "ExpProductsOld");
                for (int row = 2; row <= rowType; row++)
                {
                    string type = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpProductsOld", row, 1);
                    string expectedResult = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpProductsOld", row, 2);
                    Assert.AreEqual(expectedResult, coverageTeamPage.IsExpectedProductsPresentLV(type), "Verify that existing Expected Products values '" + type + "' removed from Expected Products field on the New Coverage Team form");
                    extentReports.CreateStepLogs("Passed", "Existing Expected Products value: '" + type + "'  removed from Expected Products field on the New Coverage Team form");
                }
                coverageTeamPage.CancelFormLV();

                //TMTI0121395	Verify that the picklist values "Capital Markets, DCM , ECM " are removed in the "Expected Products" field in Coverage Team .  
                coverageTeamPage.ClickNewCoverageTeamButton();
                rowType = ReadExcelData.GetRowCount(excelPath, "ExpProductsNew");
                for (int row = 2; row <= rowType; row++)
                {
                    string type = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpProductsNew", row, 1);
                    string expectedResult = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpProductsNew", row, 2);
                    Assert.AreEqual(expectedResult, coverageTeamPage.IsExpectedProductsPresentLV(type), "Verify that New  Expected Products values '" + type + "' updated in Expected Products field on the New Coverage Team form");
                    extentReports.CreateStepLogs("Passed", "New  Expected Product value: '" + type + "' updated in Expected Products field on the New Coverage Team form");
                }
                coverageTeamPage.CancelFormLV();

                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + valUser + " logged out");
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