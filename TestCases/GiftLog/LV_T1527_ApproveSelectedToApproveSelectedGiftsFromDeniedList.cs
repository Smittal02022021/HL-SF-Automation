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
    class LV_T1527_ApproveSelectedToApproveSelectedGiftsFromDeniedList:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileT1527 = "LV_T1527_ApproveSelectedToApproveSelectedGiftsFromDeniedList";
        private string giftRequestTitleExl;
        private string giftRequestTitle;
        private string errorMsgDenyGiftText;
        private string userCompliance;
        private string appNameExl;
        private string appName;
        private string moduleNameExl;
        private string valGiftNameEntered;
        private string congratulationMsg;
        private string congratulationMsgExl;
        private string user;
        private string valRecipientLastNameExl;
        private string txtStatus;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyApproveSelectedGiftsFromDeniedListLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT1527;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login");

                //Login as Compliance User
                userCompliance = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearchN(userCompliance);
                extentReports.CreateStepLogs("Info", "Compliance User: " + userCompliance + " details are displayed.");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(user), true);
                extentReports.CreateStepLogs("Passed", "Compliance User: " + userCompliance + " logged in on Lightning View");

                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");

                //Navigate to Gift Request page                
                giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request Module");

                //Enter required details in client gift pre- approval page
                giftRequest.SetDesiredDateToCurrentDateLV();
                valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT1527);

                //Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipientsLV();
                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequestLV();
                giftApprove.ClickSubmitRequestLV();
                congratulationMsg = giftRequest.GetCongratulationsMsgLV();
                congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                extentReports.CreateStepLogs("Passed", congratulationMsg + " message is displayed upon successful submission of gift request.");

                CustomFunctions.PageReload(driver);
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Approve Selected button is visible on click of Approve Gifts Module");


                //Search gift details by recipient last name
                valRecipientLastNameExl = ReadExcelData.ReadData(excelPath, "GiftLog", 13);
                giftApprove.SearchByRecipientLastNameLV(valRecipientLastNameExl);
                extentReports.CreateStepLogs("Info", "Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed");

                //Click on Deny selected button without selecting atleast one gift
                giftApprove.ClickDenySelectedButtonLV();
                //validate the error message
                errorMsgDenyGiftText = giftApprove.ErrorMsgForApproveGiftLV();
                Assert.IsTrue(errorMsgDenyGiftText.Contains("Error:You must select at least one gift to deny."));
                extentReports.CreateStepLogs("Passed", "Error message: " + errorMsgDenyGiftText + " is displaying.");

                //Select a gift to Deny
                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));

                //Click on Deny Selected Button to Deny the gift
                giftApprove.SetApprovalDenialCommentsLV();
                giftApprove.ClickDenySelectedButtonLV();
                extentReports.CreateStepLogs("Info", "Deny selected button is clicked successfully.");

                //Searching the Denied gift under Denied Status                
                giftApprove.SearchByRecipientLastNameAndStatusLV(valRecipientLastNameExl, "Denied");

                //Click on Approve selected button without selecting atleast one gift
                giftApprove.ClickApproveSelectedButtonLV();
                //validate the error message
                errorMsgDenyGiftText = giftApprove.ErrorMsgForApproveGiftLV();
                Assert.IsTrue(errorMsgDenyGiftText.Contains("Error:You must select at least one gift to approve."));
                extentReports.CreateStepLogs("Passed", "Error message: " + errorMsgDenyGiftText + " is displaying.");

                //Select a gift to Approve
                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                giftApprove.ClickApproveSelectedButtonLV();
                extentReports.CreateStepLogs("Passed", "Gift approved from denied list. ");

                //Searching the Approved gift under Approved Status
                giftApprove.SearchByRecipientLastNameAndStatusLV(valRecipientLastNameExl, "Approved");

                //Validate if the gift is moved under approved status
                Assert.IsTrue(giftApprove.ValidateGiftDescWithGiftNameLV(valGiftNameEntered));
                extentReports.CreateStepLogs("Passed", "Gift moved from denied list to approved list successfully.");

                //Getting the updated status of gift
                txtStatus = giftApprove.GetStatusCompareGiftDescWithGiftNameLV(valGiftNameEntered);

                //Verification of gift updated status displaying in Approved list
                Assert.AreEqual("Approved", txtStatus);
                extentReports.CreateStepLogs("Passed", txtStatus + " is displaying in gift status.");

                //Navigate to Gift Request page
                CustomFunctions.PageReload(driver);
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");

                //Navigate to Gift Request page                
                giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request Module");

                //Enter required details in client gift pre- approval page
                giftRequest.SetDesiredDateToCurrentDateLV();
                valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT1527);
                giftRequest.EnterGiftValue(ReadExcelData.ReadData(excelPath, "GiftValue", 1));

                //Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipientsLV();

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequestLV();
                giftRequest.ClickSubmitRequestButtonLV();
                Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                extentReports.CreateStepLogs("Passed", congratulationMsg + " in displayed upon successful submission of gift request");

                //Click on approve gifts tab
                CustomFunctions.PageReload(driver);
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Approve Selected button is visible on click of Approve Gifts Module");

                //Search gift details by recipient last name
                giftApprove.SearchByRecipientLastNameLV(valRecipientLastNameExl);
                extentReports.CreateStepLogs("Info", "Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                //Click on Deny selected button without selecting atleast one gift
                giftApprove.ClickDenySelectedButtonLV();

                //validate the error message
                errorMsgDenyGiftText = giftApprove.ErrorMsgForApproveGiftLV();
                Assert.IsTrue(errorMsgDenyGiftText.Contains("Error:You must select at least one gift to deny."));
                extentReports.CreateStepLogs("Passed", "Error message: " + errorMsgDenyGiftText + " is displaying.");

                //Select a gift to Deny
                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));

                //Click on Deny Selected Button to Deny the gift
                giftApprove.SetApprovalDenialCommentsLV();
                giftApprove.ClickDenySelectedButtonLV();
                extentReports.CreateStepLogs("Info", "Gift exceeding max limit value is denied successfully.");

                //Searching the Denied gift under Denied Status
                giftApprove.SearchByRecipientLastNameAndStatusLV(valRecipientLastNameExl, "Denied");

                //Click on Approve selected button without selecting atleast one gift
                giftApprove.ClickApproveSelectedButtonLV();
                //validate the error message
                errorMsgDenyGiftText = giftApprove.ErrorMsgForApproveGiftLV();
                Assert.IsTrue(errorMsgDenyGiftText.Contains("Error:You must select at least one gift to approve."));
                extentReports.CreateStepLogs("Passed", "Error message: " + errorMsgDenyGiftText + " is displaying.");

                //Select a gift to Approve
                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                giftApprove.ClickApproveSelectedButtonLV();
                extentReports.CreateStepLogs("Passed", "Gift approved from denied list. ");

                //Searching the Approved gift under Approved Status
                giftApprove.SearchByRecipientLastNameAndStatusLV(valRecipientLastNameExl, "Approved");

                //Validate if the gift is moved under approved status
                Assert.IsTrue(giftApprove.ValidateGiftDescWithGiftNameLV(valGiftNameEntered));
                extentReports.CreateStepLogs("Passed", "Gift moved from denied list to approved list successfully.");

                //Getting the updated status of gift
                txtStatus = giftApprove.GetStatusCompareGiftDescWithGiftNameLV(valGiftNameEntered);

                //Verification of gift updated status displaying in Approved list
                Assert.AreEqual("Approved", txtStatus);
                extentReports.CreateStepLogs("Passed", txtStatus + " is displaying in gift status.");

                driver.SwitchTo().DefaultContent();
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "Compliance User: " + userCompliance + " logged out");
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