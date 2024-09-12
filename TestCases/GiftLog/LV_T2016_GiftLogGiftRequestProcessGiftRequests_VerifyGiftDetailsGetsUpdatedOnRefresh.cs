using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.GiftLog;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.GiftLog
{
    class LV_T2016_GiftLogGiftRequestProcessGiftRequests_VerifyGiftDetailsGetsUpdatedOnRefresh:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileTC2016 = "LV_T2017_GiftLogGiftRequestProcessGiftRequestsVerifyTheTotalsOfTheGiftsAreMeasuredSeparatelyFromTheCurrentYear";
        private string appNameExl;
        private string appName;
        private string moduleNameExl;
        private string actualRecipientContactName;
        private string actualRecipientCompanyName;
        private string giftRequestTitle;
        private string expectedCompanyName;
        private string expectedContactName;
        private string giftRequestTitleExl;
        private string selectedRecipientName;
        private string valueOfGift;
        private string updatedGiftValue;
        private string desireDate;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        /*
         * TMTC0011869	GiftLog – Gift Request Process – Gift Requests – Verify Gift details gets updated on Refresh
        */
        [Test]
        public void VerifyGiftDetailsGetsUpdatedOnRefreshLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2016;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login ");

                string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");

                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                //Navigate to Gift Request page                
                giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request Module ");

                //Enter required details in client gift pre- approval page
                string valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileTC2016);

                //Verify company name
                actualRecipientCompanyName = giftRequest.GetAvailableRecipientCompanyLV();
                expectedCompanyName = ReadExcelData.ReadData(excelPath, "GiftLog", 8);
                Assert.AreEqual(expectedCompanyName, actualRecipientCompanyName);
                extentReports.CreateStepLogs("Passed", "Company Name: " + actualRecipientCompanyName + " is listed in Available Recipient(s) table ");

                //Verify recipient contact name
                actualRecipientContactName = giftRequest.GetAvailableRecipientNameLV();
                expectedContactName = ReadExcelData.ReadData(excelPath, "GiftLog", 9);
                Assert.AreEqual(expectedContactName, actualRecipientContactName);
                extentReports.CreateStepLogs("Passed", "Recipient Name: " + actualRecipientContactName + " is listed in Available Recipient(s) table ");

                //Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipientsLV();

                //Verify recipient name
                selectedRecipientName = giftRequest.GetSelectedRecipientNameLV();
                Assert.AreEqual(actualRecipientContactName, selectedRecipientName);
                extentReports.CreateStepLogs("Passed", "Recipient Name: " + selectedRecipientName + " in selected recipient(s) table matches with available recipient name listed in Available Recipient(s) table ");

                //Edit Gift Value
                string editValue = ReadExcelData.ReadData(excelPath, "GiftEdit", 6);
                giftRequest.EnterGiftValueLV(editValue);
                extentReports.CreateStepLogs("Info", "Gift Value is updated to: " + editValue);

                //Click Refresh button
                giftRequest.ClickRefreshButton();
                extentReports.CreateStepLogs("Info", "Refresh button is clicked. ");

                //Verify if gift value is updated
                updatedGiftValue = giftRequest.GetGiftValueInGiftAmtYTDLV();   
                //Need to Apply Assersion
                extentReports.CreateStepLogs("Info", "Updated gift value: " + updatedGiftValue + " is visible in Selected Recipient(s) table. ");

                //Change currency name
                string editCurrencyName = ReadExcelData.ReadData(excelPath, "GiftEdit", 3);
                giftRequest.SelectCurrencyDrpDownLV(editCurrencyName);
                extentReports.CreateStepLogs("Info", "Currency name is updated to: " + editCurrencyName);

                //Click Refresh button
                giftRequest.ClickRefreshButton();
                extentReports.CreateLog("Refresh button is clicked. ");

                //Verify if Currency amount is updated
                valueOfGift = giftRequest.GetGiftValueInGiftAmtYTDLV();
                extentReports.CreateStepLogs("Info", "Gift Value: " + valueOfGift + " is updated as per the currency name change in Selected Recipient(s) table. ");

                //Change desire date to next year
                desireDate = giftRequest.EnterDesiredDateLV(364);
                extentReports.CreateStepLogs("Info", "Desire Date: " + desireDate + " entered as next year date. ");

                //Click Refresh button
                giftRequest.ClickRefreshButton();
                extentReports.CreateLog("Refresh button is clicked. ");

                //Verify if Currency amount is still displayed
                valueOfGift = giftRequest.GetGiftValueInGiftTotalNextYearLV();
                extentReports.CreateStepLogs("Info", "Gift Value: " + valueOfGift + " is displayed for next year. ");

                driver.SwitchTo().DefaultContent();
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");

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