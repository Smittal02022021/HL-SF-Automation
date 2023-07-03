﻿using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.GiftLog;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.GiftLog
{
    class T1510_ClientGiftPre_ApprovalPageLayoutAndFields : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();

        public static string fileTC1510 = "T1510_ClientGiftPre_ApprovalPageLayoutAndFields";
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ApproveGifts_DefaultLayoutFieldsAndFieldsValues()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1510;
               
                Console.WriteLine(excelPath);
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                //Search standard user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTC1510, user);

                //Verify searched user
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateStepLogs("Passed", "User " + userPeople + " details are displayed ");

                //Login as Gift Log User and validate the user
                usersLogin.LoginAsSelectedUser();
                string giftLogUser = login.ValidateUser();
                string giftLogUserExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(giftLogUserExl.Contains(giftLogUser), true);
                extentReports.CreateStepLogs("Passed", "Gift Log User: " + giftLogUser + " is able to login ");

                //Navigate to Client Gift Pre-Approval page
                giftRequest.GoToGiftRequestPage();
                string giftRequestTitle = giftRequest.GetGiftRequestPageTitle();
                string giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                //Validate fields under Gift Billing Details Section
                Assert.True(giftRequest.ValidateFieldsUnderGiftBillingDetailsSection());
                extentReports.CreateStepLogs("Passed", "Fields defined under Gift Billing Details Section are displayed. ");

                //Validate fields under Gift Details Section
                Assert.True(giftRequest.ValidateFieldsUnderGiftDetailsSection());
                extentReports.CreateStepLogs("Passed", "Fields defined under Gift Details Section are displayed. ");

                //Validate fields under Gift Recipients Section
                Assert.True(giftRequest.ValidateFieldsUnderGiftRecipientsSection());
                extentReports.CreateStepLogs("Passed", "Fields defined under Gift Recipients Section are displayed. ");

                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);                
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}