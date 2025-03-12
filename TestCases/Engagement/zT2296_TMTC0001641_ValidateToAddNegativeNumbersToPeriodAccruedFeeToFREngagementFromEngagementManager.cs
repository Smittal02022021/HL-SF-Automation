﻿using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Engagement
{
    class zT2296_TMTC0001641_ValidateToAddNegativeNumbersToPeriodAccruedFeeToFREngagementFromEngagementManager : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        EngagementManager engManager = new EngagementManager();
        SendEmailNotification notification = new SendEmailNotification();


        public static string fileTC2296 = "T2296_TMTC0001641_ValidateToAddNegativeNumbersToPeriodAccruedFee1";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void RevenueAccrual_FASRevenueAccrual_StageChange()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2296;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as CAO User and validate the user
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 2));
                string caoUser = login.ValidateUser();
                Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                extentReports.CreateLog("User: " + caoUser + " logged in ");

                //Clicking on Engagement Manager link and Validate the title of page
                string titleEngMgr = engHome.ClickEngageManager();
                Assert.AreEqual("Engagement Manager", titleEngMgr);
                extentReports.CreateLog("Page with title: " + titleEngMgr + " is displayed ");

                //Calling function to select Show Records 
                engManager.SelectShowRecords("FR Engagements");

                //Calling function to reset filters and navigate to Engagement details page
                engManager.ResetFiltersForFR("Active");
                extentReports.CreateLog("Filters are reset as per FR Engagements ");
                string titleEngDetails = engManager.ClickFREngagementName();
                Assert.AreEqual("Engagement Detail", titleEngDetails);
                extentReports.CreateLog("Page with title: " + titleEngDetails + " is displayed upon clicking Engagement Name ");
                string engName = engagementDetails.GetEngName();

                //Logout of SFFRCAO user and validate with admin user and delete any existing Revenue Accurals  
                usersLogin.UserLogOut();
                extentReports.CreateLog(caoUser + " logged out successfully ");
                engHome.SearchEngagementWithName(engName);
                engagementDetails.DeleteExistingAccurals();
                extentReports.CreateLog("No revenue accruals exists on Engagement details page ");

                //Login as SFFRCAO User and validate the user
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 2));
                string caoUser1 = login.ValidateUser();
                Assert.AreEqual(caoUser1.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                extentReports.CreateLog("User: " + caoUser1 + " logged in ");

                //Navigate back to Engagement Manager and Update Actual Monthly Fees and Actual Transaction Fee with negative value
                engHome.ClickEngageManager();
                engManager.UpdateActualMonthlyAndTxnFeesValue(ReadExcelData.ReadData(excelPath, "EngMgr", 1), ReadExcelData.ReadData(excelPath, "EngMgr", 2));
                extentReports.CreateLog("Actual Monthly Fees and Actual Transaction Fees are updated with negative values ");

                //Navigate to engagement details and validate if Period Accrured Fee Net is updated 
                engManager.ClickFREngagementName();
                string accrualFee = engagementDetails.GetPeriodAccrualFeeNetValue();
                Assert.AreEqual("USD -11.00", accrualFee);
                extentReports.CreateLog("Period Accrued Fee Net is updated with sum of Actual Monthly Fee and Actual Transaction Fee value ");

                //To update value of Period Accrual fee to a positive value               
                engHome.ClickEngageManager();
                engManager.UpdateActualMonthlyAndTxnFeesValue(ReadExcelData.ReadData(excelPath, "EngMgr", 3), ReadExcelData.ReadData(excelPath, "EngMgr", 4));
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
