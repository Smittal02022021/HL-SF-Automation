using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.GiftLog;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesForce_Project.TestCases.GiftLog
{
    //Issue in Search 

    class N_LV_T1525_ApproveGiftsDenySelectedToDenyTheGiftsFromPendingList:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();
        LVHomePage homePageLV = new LVHomePage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactHomePage conHome = new ContactHomePage();
        RandomPages randomPages = new RandomPages();

        public static string fileT1525 = "LV_T1525_ApproveGiftsDenySelectedToDenyTheGiftsFromPendingList";
        private string errorMsgApproveGiftText;
        private string txtStatus;
        private string giftRequestTitleExl;
        private string giftRequestTitle;
        private string congratulationMsg;
        private string congratulationMsgExl;
        private string recipientLastNameExl;
        private string valGiftNameEntered;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
       

        [Test]
        public void VerifySelectedGiftToDenyTheGiftsFromPendingListLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT1525;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login ");

                string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "Compliance User: " + valUser + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "Compliance User: " + valUser + " logged in on Lightning View");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");

                //Navigate to Gift Request page                
                giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                // Enter required details in client gift pre- approval page
                valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT1525);

                //giftRequest.EnterGiftValue("110");

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipientsLV();

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequestLV();
                congratulationMsg = giftRequest.GetCongratulationsMsgLV();
                congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                extentReports.CreateStepLogs("Passed", "Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                //CustomFunctions.PageReload(driver);
                driver.SwitchTo().DefaultContent();
                //Click on approve gifts tab
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");

                //Click on approve gifts tab
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Approve Selected button is visible on click of approve gifts tab ");

                //Search gift details by recipient last name
                recipientLastNameExl = ReadExcelData.ReadData(excelPath, "GiftLog", 13);
                giftApprove.SearchByRecipientLastNameLV(recipientLastNameExl);
                extentReports.CreateLog("Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                // Click on approve selected button
                giftApprove.ClickApproveSelectedButtonLV();
                extentReports.CreateStepLogs("Passed", "Approve selected button is clicked successfully ");

                errorMsgApproveGiftText = giftApprove.ErrorMsgForApproveGiftLV();
                Assert.IsTrue(errorMsgApproveGiftText.Contains("Error:You must select at least one gift to approve."));
                extentReports.CreateStepLogs("Passed", "Error message:" + errorMsgApproveGiftText + " is displaying ");
                                
                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                giftApprove.ClickDenySelectedButtonLV();
                
                giftApprove.SearchByStatusLV(recipientLastNameExl, "Denied");
                txtStatus = giftApprove.GetStatusCompareGiftDescWithGiftNameLV(valGiftNameEntered);

                // Verification of gift status displaying in Denied list
                Assert.AreEqual("Denied", txtStatus);
                extentReports.CreateStepLogs("Passed", txtStatus + " is displaying in gift status ");
                driver.SwitchTo().DefaultContent();
                //Navigate to Gift Request page
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");

                //Navigate to Gift Request page                
                giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                // Enter required details in client gift pre- approval page
                valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT1525);
                giftRequest.EnterGiftValue(ReadExcelData.ReadData(excelPath, "GiftValue", 1));

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipientsLV();

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequestLV();
                giftRequest.ClickSubmitRequestButtonLV();
                congratulationMsg = giftRequest.GetCongratulationsMsgLV();

                Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                extentReports.CreateStepLogs("Passed", "Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");
                driver.SwitchTo().DefaultContent();
                //Click on approve gifts tab
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");

                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Approve Selected button is visible on click of approve gifts tab ");

                //Search gift details by recipient last name
                giftApprove.SearchByRecipientLastNameLV(recipientLastNameExl);
                extentReports.CreateStepLogs("Passed", "Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                giftApprove.ClickDenySelectedButtonLV();

                giftApprove.SearchByStatusLV(recipientLastNameExl, "Denied");

                //Verification of gift status displaying in Denied list
                txtStatus = giftApprove.GetStatusCompareGiftDescWithGiftNameLV(valGiftNameEntered);

                Assert.AreEqual("Denied", txtStatus);
                extentReports.CreateStepLogs("Passed", " is displaying in gift status ");

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