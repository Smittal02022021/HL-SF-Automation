using SF_Automation.Pages.Common;
using SF_Automation.Pages.GiftLog;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SF_Automation.TestCases.GiftLog
{
    class LV_T2009_GiftRequestProcessRecipientsExceedsYearlyGiftAllowanceGreaterThan100ForCurrencyTypeAustralianDollar : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();
        LVHomePage homePageLV = new LVHomePage();
        public static string fileT2009 = "LV_T2009_GiftLogRecipientsExceedsYearlyGiftAllowanceGreaterThan100ForCurrencyTypeAustralianDollar";
        private string currencyCode;
        private string colorOfGiftValue;
        private string colorOfGiftValueExl;
        private string giftRequestTitle;
        private string warningMessage;
        private string congratulationMsg;
        private string congratulationMsgExl;
        private string stdUser;
        private string newDesireDate;
        private string valueOfGift;
        private string labelGiftAmtYTD;
        private string expectedLabelGiftAmtYTD;
        private string giftRequestTitleExl;
        private string warningMessageExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyRecipientsExceedsYearlyGiftAllowanceGreaterThan100ForCurrencyTypeAustralianDollarLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2009;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login ");

                //Standard User Logged in 
                string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "CF Fin User: " + valUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged in on Lightning View");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "CF Fin User is on " + moduleNameExl + " Page ");

                //Navigate to Gift Request page                
                giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");
                //Enter required details in client gift pre- approval page
                giftRequest.EnterDetailsGiftRequestLV(fileT2009);

                //Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipientsLV();
                labelGiftAmtYTD = giftRequest.GetLabelNewGiftAmtYTDLV();
                expectedLabelGiftAmtYTD = ReadExcelData.ReadData(excelPath, "GiftLog", 12);
                Assert.AreEqual(expectedLabelGiftAmtYTD, labelGiftAmtYTD);
                extentReports.CreateLog("Gift Label: " + labelGiftAmtYTD + " is displayed in Selected Recipient(s) table ");

                //Verify value of gift
                valueOfGift = giftRequest.GetGiftValueInGiftAmtYTDLV();
                extentReports.CreateLog("Gift Value: " + valueOfGift + " is displayed in Selected Recipient(s) table ");

                //Verify currency of gift
                currencyCode = giftRequest.GetGiftCurrencyCodeLV();
                Assert.AreEqual("USD", currencyCode);
                extentReports.CreateLog("Currency Code: " + currencyCode + " is displayed in Selected Recipient(s) table ");

                //Verification of NewGiftAmtYTD Value turn to red to indicate Currency max limit Exceeded for Current Calendar Year
                colorOfGiftValue = giftRequest.GetGiftValueColorInGiftAmtYTDLV();
                colorOfGiftValueExl = ReadExcelData.ReadData(excelPath, "GiftLog", 13);
                Assert.AreEqual(colorOfGiftValueExl, colorOfGiftValue);
                extentReports.CreateLog("Color Of Gift Value: " + colorOfGiftValue + " is displayed in Selected Recipient(s) table ");

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequestLV();
                warningMessage = giftRequest.GetWarningMessageOnAmountLimitExceedLV();
                warningMessageExl = ReadExcelData.ReadData(excelPath, "GiftLog", 14);
                Assert.AreEqual(warningMessageExl, warningMessage);
                extentReports.CreateLog("Warning Message: " + warningMessage + " is displayed upon submitting a gift request with gift amount exceeding $100 ");

                //Verify revise request button visible
                Assert.IsTrue(giftRequest.IsReviseRequestButtonVisibleLV());
                extentReports.CreateLog("Revise Request button is visible on warning message page ");

                //Verify submit request button visible
                Assert.IsTrue(giftRequest.IsSubmitRequestButtonVisibleLV());
                extentReports.CreateLog("Submit Request button is visible on warning message page ");

                //Click on revise request button
                giftRequest.ClickReviseRequestButtonLV();

                //Verification of landing on Pre-approval page upon Click on Revise Request Button
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateLog("Page Title: " + giftRequestTitle + " is diplayed upon click of revise request button ");

                //Set Desire date to Next Calendar year
                newDesireDate = giftRequest.EnterDesiredDateLV(363);//365
                extentReports.CreateLog("Next Calendar Year date is set to: " + newDesireDate);

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequestLV();

                //Verify the warning message
                warningMessage = giftRequest.GetWarningMessageOnAmountLimitExceedLV();
                warningMessageExl = ReadExcelData.ReadData(excelPath, "GiftLog", 14);
                Assert.AreEqual(warningMessageExl, warningMessage);
                extentReports.CreateLog("Warning Message: " + warningMessage + " is displayed upon submitting a gift request with gift amount exceeding $100 ");

                //Submit the request even after warning message
                giftApprove.ClickSubmitRequestLV();
                congratulationMsg = giftRequest.GetCongratulationsMsgLV();
                congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                extentReports.CreateLog(congratulationMsg + " message is displayed upon successful submission of gift request. ");

                driver.SwitchTo().DefaultContent();
                homePageLV.LogoutFromSFLightningAsApprover();
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