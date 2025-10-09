using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.EventExpense;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.EventExpense
{
    class LV_TMTC0036647_1_VerifyTheFRAndSCLOBPicklistOptionsAreRemovedFromExpenseRequestLWC:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        LVExpenseRequestCreatePage expRequest = new LVExpenseRequestCreatePage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTTMTC0036647 = "LV_TMTC0036647_VerifyTheFRAndSCLOBPicklistOptionsAreRemovedFromExpenseRequest(LWC)";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        //TMT0084912 Verify as a Standard user that the "FR" LOB picklist option is removed from the Expense Request (LWC) application.
        //TMT0084913 Verify as a Standard user that the "SC" LOB picklist option is removed from the Expense Request(LWC) application.
        //TMT0084914 Verify as a System Admin that the "FR" LOB picklist option is removed from the Expense Request(LWC) application.
        //TMT0084915 Verify as a System Admin that the "SC" LOB picklist option is removed from the Expense Request(LWC) application.
        //TMT0084916 Verify as a CAO that the "FR" LOB picklist option is removed from the Expense Request(LWC) application.
        //TMT0084917 Verify as a CAO that the "SC" LOB picklist option is removed from the Expense Request(LWC) application.        

        [Test]
        public void VerifyTheFRAndSCLOBPicklistOptionsAreRemovedFromExpenseRequestLWC()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + fileTTMTC0036647;
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");
                //Login as Diffeerent users
                int rowUser = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row <= rowUser; row++)
                {
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    string userProfile = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);
                    homePage.SearchUserByGlobalSearchN(valUser);
                    extentReports.CreateStepLogs("Info", userProfile+" User: " + valUser + " details are displayed. ");
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateStepLogs("Passed", userProfile+" User: " + valUser + " logged in on Lightning View");
                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", userProfile+" User is on " + moduleNameExl + " Module Page ");

                    //Verify the LOB picklist options 
                    int rowCountLOB = ReadExcelData.GetRowCount(excelPath, "LOB");
                    for(int rowLOB = 2; rowLOB <= rowCountLOB; rowLOB++)
                    {
                        string lobName = ReadExcelData.ReadDataMultipleRows(excelPath, "LOB", rowLOB, 1);
                        Assert.IsFalse(expRequest.IsLOBPresentLV(lobName), "Verify LOB: "+ lobName+" should not be available for User profile: "+ userProfile);
                        extentReports.CreateStepLogs("Passed", "LOB: "+ lobName+" is not be available for User profile: "+ userProfile) ;
                    }
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Passed", userProfile +" User:: " + valUser + " Logged out ");
                }
                driver.Quit();
                extentReports.CreateLog("Browser Closed Successfully");
            }
            catch (Exception ex)
            {
                extentReports.CreateExceptionLog(ex.Message);
                homePageLV.UserLogoutFromSFLightningView();
                driver.Quit();
            }
        }
    }
}
