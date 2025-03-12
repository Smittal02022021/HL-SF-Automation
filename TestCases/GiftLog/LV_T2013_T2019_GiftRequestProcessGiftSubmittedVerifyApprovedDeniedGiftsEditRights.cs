using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.GiftLog;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.GiftLog
{
    class LV_T2013_T2019_GiftRequestProcessGiftSubmittedVerifyApprovedDeniedGiftsEditRights:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();
        GiftSubmittedPage giftsSubmit = new GiftSubmittedPage();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileT2013 = "LV_T2017_GiftLogGiftRequestProcessGiftRequestsVerifyTheTotalsOfTheGiftsAreMeasuredSeparatelyFromTheCurrentYear";

        private string giftRequestTitleExl;
        private string giftRequestTitle;
        private string userCompliance;
        private string stdUser;
        private string user;
        private string appNameExl;
        private string appName;
        private string moduleNameExl;
        private string valGiftNameEntered;
        private string actualRecipientCompanyName;
        private string expectedCompanyName;
        private string actualRecipientContactName;
        private string expectedContactName;
        private string selectedRecipientName;
        private string congratulationMsg;
        private string congratulationMsgExl;
        private string valUser;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyApprovedDeniedGiftsEditRightsLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2013;
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login ");

                valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "Compliance User: " + valUser + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "Compliance User: " + valUser + " logged in on Lightning View");

                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "CF Fin User is on " + moduleNameExl + " Page ");

                //Navigate to Gift Request page                
                giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");


                // Enter required details in client gift pre- approval page
                valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT2013);

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

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipientsLV();

                //Verify recipient name
                selectedRecipientName = giftRequest.GetSelectedRecipientNameLV();
                Assert.AreEqual(actualRecipientContactName, selectedRecipientName);
                extentReports.CreateStepLogs("Passed", "Recipient Name: " + selectedRecipientName + " in selected recipient(s) table matches with available recipient name listed in Available Recipient(s) table ");

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequestLV();
                giftApprove.ClickSubmitRequestLV();
                congratulationMsg = giftRequest.GetCongratulationsMsgLV();
                congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                extentReports.CreateStepLogs("Passed", "Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                driver.SwitchTo().DefaultContent();
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "CF Fin User logged out");

                // Search Complaince user by global search
                userCompliance = ReadExcelData.ReadData(excelPath, "Users", 2);
                homePage.SearchUserByGlobalSearchN(userCompliance);
                extentReports.CreateStepLogs("Info", "Compliance User: " + userCompliance + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(userCompliance), true);
                extentReports.CreateStepLogs("Passed", "Compliance User: " + userCompliance + " logged in on Lightning View");

                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 4, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");

                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Approve Selected button is visible on click of approve gifts tab ");

                //Search gift details by recipient last name
                string recipientLastNameExl = ReadExcelData.ReadData(excelPath, "GiftLog", 13);
                giftApprove.SearchByRecipientLastNameLV(recipientLastNameExl);
                extentReports.CreateLog("Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                //Approve gift
                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                giftApprove.SetApprovalDenialCommentsLV();
                giftApprove.ClickApproveSelectedButtonLV();
                extentReports.CreateStepLogs("Info", "Approve selected button is clicked successfully ");

                driver.SwitchTo().DefaultContent();
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "Compliance User logged out");

                // Search standard user by global search
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "CF Fin User: " + valUser + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged in on Lightning View");

                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);

                //Search the approved gift
                Assert.IsTrue(giftsSubmit.IsTableListPresentLV());
                extentReports.CreateStepLogs("Passed", "Gift Submitted Page is displayed with table upon click of cancel button ");

                giftsSubmit.SearchByOnlyStatusLV("Approved");
                extentReports.CreateStepLogs("Info", "Landed on approved gifts page. ");

                //Verify if Gift Description is clickable
                Assert.IsFalse(giftsSubmit.ValidateIfGiftDescIsClickableLV(valGiftNameEntered));
                extentReports.CreateStepLogs("Passed", "Gift description is not clickable. ");
                driver.SwitchTo().DefaultContent();
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);

                //Navigate to Gift Request page                
                giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                // Enter required details in client gift pre- approval page
                valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT2013);

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

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipientsLV();

                //Verify recipient name
                selectedRecipientName = giftRequest.GetSelectedRecipientNameLV();
                Assert.AreEqual(actualRecipientContactName, selectedRecipientName);
                extentReports.CreateStepLogs("Passed", "Recipient Name: " + selectedRecipientName + " in selected recipient(s) table matches with available recipient name listed in Available Recipient(s) table ");

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequestLV();
                giftApprove.ClickSubmitRequestLV();
                extentReports.CreateLog("Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                driver.SwitchTo().DefaultContent();
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");

                // Search Complaince user by global search
                homePage.SearchUserByGlobalSearchN(userCompliance);
                extentReports.CreateStepLogs("Info", "Compliance User: " + userCompliance + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(userCompliance), true);
                extentReports.CreateStepLogs("Passed", "Compliance User: " + userCompliance + " logged in on Lightning View");

                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 4, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");

                Assert.IsTrue(giftApprove.DenySelectedButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Deny Selected button is visible on click of approve gifts tab ");

                //Search gift details by recipient last name
                recipientLastNameExl = ReadExcelData.ReadData(excelPath, "GiftLog", 13);
                giftApprove.SearchByRecipientLastNameLV(recipientLastNameExl);
                extentReports.CreateStepLogs("Info", " 'Pending' Status as default and upon search gifts list is displayed ");

                //Approve gift
                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                giftApprove.SetApprovalDenialCommentsLV();
                giftApprove.ClickDenySelectedButtonLV();
                extentReports.CreateStepLogs("Info", "Deny selected button is clicked successfully ");

                driver.SwitchTo().DefaultContent();
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "Compliance User: " + valUser + " logged out");

                // Search standard user by global search
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "CF Fin User: " + valUser + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged in on Lightning View");

                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);

                Assert.IsTrue(giftsSubmit.IsTableListPresentLV());
                extentReports.CreateStepLogs("Passed", "Gift Submitted Page is displayed with table upon click of cancel button ");

                //Search the approved gift
                giftsSubmit.SearchByOnlyStatusLV("Denied");
                extentReports.CreateStepLogs("Info", "Landed on denied gifts page. ");

                //Verify if Gift Description is clickable
                Assert.IsFalse(giftsSubmit.ValidateIfGiftDescIsClickableLV(valGiftNameEntered));
                extentReports.CreateStepLogs("Passed", "Gift description is not clickable. ");

                driver.SwitchTo().DefaultContent();
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "Compliance User: " + valUser + " logged out");
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