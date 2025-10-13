using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.GiftLog;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.GiftLog
{
    class LV_T2007_GiftLogGiftRequestProcessSplitGiftValueInEqualDistributionForSelectedRecipient:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileT2007 = "LV_T2007_GiftRequestProcessSplitGiftValueInEqualDistributionForSelectedRecipient";
        
        private string giftRequestTitleExl;
        private string giftRequestTitle;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }


        [Test]
        public void VerifySplitGiftValueInEqualDistributionForSelectedRecipientLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2007;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();                  
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login ");

                string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "CF Fin User: " + valUser + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged in on Lightning View");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "CF Fin is on " + moduleNameExl + " Page ");

                //Navigate to Gift Request page                
                giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                // Enter required details in client gift pre- approval page
                string valGiftNameEntered = giftRequest.EnterGiftRequestDetailsLV(fileT2007);

                //Add multiple recipients to selected recipients
                int sizeOfAvailableRecipient = giftRequest.GetSizeOfAvailableRecipientLV();
                for (int i = 1; i <= sizeOfAvailableRecipient; i++)
                {
                    giftRequest.AddMultipleRecipientToSelectedRecipientsLV(i - 1);
                }

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.ClickAddRecipientLV();
                extentReports.CreateStepLogs("Info", "Multiple Recipients added to Selected Recipients successfully ");

                int sizeOfSelectedRecipient = giftRequest.GetSizeOfSelectedRecipientLV();
                string giftValue = ReadExcelData.ReadData(excelPath, "GiftLog", 3);
                double totalValueOfGift = double.Parse(giftValue);
                double divideGiftValue = totalValueOfGift / sizeOfSelectedRecipient;
                double expectedDollarValueSplit = Math.Round(divideGiftValue, 1);
                for (int i = 1; i <= sizeOfSelectedRecipient; i++)
                {
                    string value = giftRequest.GetDollarValueLV(i - 1);
                    double dollarValueSplit = double.Parse(value);
                    Assert.AreEqual(expectedDollarValueSplit, dollarValueSplit);
                }
                extentReports.CreateStepLogs("Info", "Total dollar value split equal between the selected recipient ");
                giftRequest.RemoveSelectedRecipientLV();

                extentReports.CreateLog("One selected recipient is removed from the list ");
                int updatedSizeOfSelectedRecipient = giftRequest.GetSizeOfSelectedRecipientLV();
                double divideGiftValueWithUpdatedList = totalValueOfGift / updatedSizeOfSelectedRecipient;
                double expectedDollarValueSplitAfterUpdate = Math.Round(divideGiftValueWithUpdatedList, 1);
                for (int i = 1; i <= updatedSizeOfSelectedRecipient; i++)
                {
                    string value = giftRequest.GetDollarValueLV(i - 1);
                    double dollarValueSplit = double.Parse(value);
                    Assert.AreEqual(expectedDollarValueSplitAfterUpdate, dollarValueSplit);
                }

                extentReports.CreateStepLogs("Info", "Total dollar value split equal between the selected recipient after removing one recipient ");

                giftRequest.AddRecipientToSelectedRecipientsLV();
                extentReports.CreateStepLogs("Info", "Add one recipient back to selected recipient list ");

                int resetSizeOfSelectedRecipient = giftRequest.GetSizeOfSelectedRecipientLV();
                double divideGiftValueWithResetList = totalValueOfGift / resetSizeOfSelectedRecipient;
                double expectedDollarValueSplitAfterReset = Math.Round(divideGiftValueWithResetList, 1);
                for (int i = 1; i <= resetSizeOfSelectedRecipient; i++)
                {
                    string value = giftRequest.GetDollarValueLV(i - 1);
                    double dollarValueSplit = double.Parse(value);
                    Assert.AreEqual(expectedDollarValueSplitAfterReset, dollarValueSplit);
                }
                extentReports.CreateStepLogs("Info", "Total dollar value split equal between the selected recipient after adding back one recipient ");

                string DesireDate = giftRequest.EnterDesiredDateLV(364);
                giftRequest.ClickRefreshButton();

                //int resetSizeOfSelectedRecipient = giftRequest.GetSizeOfSelectedRecipient();
                double divideGiftWithTotalNextYear = totalValueOfGift / resetSizeOfSelectedRecipient;
                double expectedDollarTotalNextYear = Math.Round(divideGiftWithTotalNextYear, 1);
                for (int i = 1; i <= resetSizeOfSelectedRecipient; i++)
                {
                    string value = giftRequest.GetDollarValueTotalNextYearLV(i - 1);
                    double dollarValueSplit = double.Parse(value);
                    Assert.AreEqual(expectedDollarTotalNextYear, dollarValueSplit);
                }
                extentReports.CreateStepLogs("Info", "Total dollar value split is coming equal in total next year ");

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