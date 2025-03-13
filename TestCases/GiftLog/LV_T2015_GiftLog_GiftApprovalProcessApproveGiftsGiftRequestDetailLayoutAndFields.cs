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
using System.Numerics;

namespace SF_Automation.TestCases.GiftLog
{
    class LV_T2015_GiftLog_GiftApprovalProcessApproveGiftsGiftRequestDetailLayoutAndFields:BaseClass
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

        public static string fileT2015 = "LV_T2017_GiftLogGiftRequestProcessGiftRequestsVerifyTheTotalsOfTheGiftsAreMeasuredSeparatelyFromTheCurrentYear";
        private string expectedCompanyName;
        private string actualRecipientCompanyName;
        private string actualRecipientContactName;
        private string colorOfGiftValue;
        private string colorOfGiftValueExl;
        private string giftRequestTitle;
        private string giftRequestTitleExl;
        private string congratulationMsg;
        private string congratulationMsgExl;
        private string recipientOnGiftRequestDetail;
        private string recipientNameExl;
        private string valGiftNameEntered;
        private string submittedForValue;
        private string submittedForValueExl;
        private string expectedContactName;
        private string selectedRecipientName;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        /*
         *TMTC0011866	GiftLog – Gift Approval Process – Approve Gifts – Gift Request Detail layout and fields. 
         */

        [Test]
        public void VerifyGiftRequestDetailLayoutAndFieldsLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2015;

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
                extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                //Navigate to Gift Request page                
                giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                //Enter required details in client gift pre- approval page
                valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT2015);

                //Verify company name
                actualRecipientCompanyName = giftRequest.GetAvailableRecipientCompanyLV();
                expectedCompanyName = ReadExcelData.ReadData(excelPath, "GiftLog", 8);
                Assert.AreEqual(expectedCompanyName, actualRecipientCompanyName);
                extentReports.CreateLog("Company Name: " + actualRecipientCompanyName + " is listed in Available Recipient(s) table ");

                //Verify recipient contact name
                actualRecipientContactName = giftRequest.GetAvailableRecipientNameLV();
                expectedContactName = ReadExcelData.ReadData(excelPath, "GiftLog", 9);
                Assert.AreEqual(expectedContactName, actualRecipientContactName);
                extentReports.CreateLog("Recipient Name: " + actualRecipientContactName + " is listed in Available Recipient(s) table ");

                //Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipientsLV();

                //Verify recipient name
                selectedRecipientName = giftRequest.GetSelectedRecipientNameLV();
                Assert.AreEqual(actualRecipientContactName, selectedRecipientName);
                extentReports.CreateLog("Recipient Name: " + selectedRecipientName + " in selected recipient(s) table matches with available recipient name listed in Available Recipient(s) table ");

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequestLV();
                giftApprove.ClickSubmitRequestLV();
                congratulationMsg = giftRequest.GetCongratulationsMsgLV();
                extentReports.CreateLog("Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                driver.SwitchTo().DefaultContent();
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out ");

                // Search Complaince user by global search
                string userCompliance = ReadExcelData.ReadData(excelPath, "Users", 2);
                homePage.SearchUserByGlobalSearchN(userCompliance);
                extentReports.CreateStepLogs("Info", "Compliance User: " + userCompliance + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
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
                extentReports.CreateLog("Approve Selected button is visible on click of approve gifts tab ");

                //Search gift details by recipient last name for current year
                string recipientLastNameExl = ReadExcelData.ReadData(excelPath, "GiftLog", 13);
                giftApprove.SearchByRecipientLastNameLV(recipientLastNameExl);
                extentReports.CreateLog("Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                //Approve the current year gift
                giftApprove.CompareAndClickGiftDescLV(valGiftNameEntered);
                extentReports.CreateLog("Gift desc is clicked successfully ");

                //Validate Gift Name field and value                
                Assert.IsTrue(giftApprove.IsGiftNameCorrectInGiftRequestDetailsLV(valGiftNameEntered));
                extentReports.CreateLog("Gift Name field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate Gift Type field and value
                string giftTypeExl = ReadExcelData.ReadData(excelPath, "GiftLog", 1);
                Assert.IsTrue(giftApprove.ISGiftTypeCorrectInGiftRequestDetailsLV(giftTypeExl));
                extentReports.CreateLog("Gift Type field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate Recipient For Gift field and value
                string recipientForExl = ReadExcelData.ReadData(excelPath, "GiftLog", 9);
                Assert.IsTrue(giftApprove.IsRecipientForCorrectInGiftRequestDetailsLV(recipientForExl));
                extentReports.CreateLog("Recipient For Gift field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate Submitted For field and value
                string submittedForExl = ReadExcelData.ReadData(excelPath, "GiftLog", 2);
                Assert.IsTrue(giftApprove.IsSubmittedForCorrectInGiftRequestDetailsLV(submittedForExl));
                extentReports.CreateLog("Submitted For field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate Vendor field and value
                string vendorExl = ReadExcelData.ReadData(excelPath, "GiftLog", 6);
                Assert.IsTrue(giftApprove.IsVendorCorrectInGiftRequestDetailsLV(vendorExl));
                extentReports.CreateLog("Vendor field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate Gift Value field and value
                string giftValueExl= ReadExcelData.ReadData(excelPath, "GiftLog", 3);
                Assert.IsTrue(giftApprove.IsyGiftValueCorrectInGiftRequestDetailsLV(giftValueExl));
                extentReports.CreateLog("Gift Value field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate Currency field and value
                string currencyExl= ReadExcelData.ReadData(excelPath, "GiftLog", 5);
                Assert.IsTrue(giftApprove.IsCurrencyCorrectInGiftRequestDetailsLV(currencyExl));
                extentReports.CreateLog("Currency field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate HL Relationship field and value
                string hlRelationshipExl = ReadExcelData.ReadData(excelPath, "GiftLog", 4);
                Assert.IsTrue(giftApprove.IsHLRelationshipCorrectInGiftRequestDetailsLV(hlRelationshipExl));
                extentReports.CreateLog("HL Relationship field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate Reason For Gift field and value
                string reasonForGiftExl = ReadExcelData.ReadData(excelPath, "GiftLog", 7);
                Assert.IsTrue(giftApprove.IsReasonForGiftCorrectInGiftRequestDetailsLV(reasonForGiftExl));
                extentReports.CreateLog("Reason For Gift field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                //Validate Approval Number field and value
                Assert.AreEqual("Pending", giftApprove.GetApprovalNumberFromGiftRequestDetailsLV());
                extentReports.CreateLog("Approval Number field is displayed with value as Pending on approve gifts page. ");

                //Validate Created By field and value
                string createdByExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.IsTrue(giftApprove.IsCreatedByCorrectInGiftRequestDetailsLV(createdByExl));
                extentReports.CreateLog("Created By field is displayed with correct value under Gift Request detail section on approve gifts page. ");

                driver.SwitchTo().DefaultContent();
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + userCompliance + " logged out");
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