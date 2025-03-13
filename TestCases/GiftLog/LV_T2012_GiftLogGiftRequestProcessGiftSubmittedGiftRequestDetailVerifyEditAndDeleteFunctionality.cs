using NUnit.Framework;
using SF_Automation.Pages.Common;
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
using SF_Automation.Pages.Companies;

namespace SF_Automation.TestCases.GiftLog
{
    class LV_T2012_GiftLogGiftRequestProcessGiftSubmittedGiftRequestDetailVerifyEditAndDeleteFunctionality:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();
        GiftSubmittedPage giftsSubmit = new GiftSubmittedPage();
        GiftRequestEditPage giftEdit = new GiftRequestEditPage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();

        public static string fileT2012 = "LV_T2012_GiftLogGiftRequestProcessGiftSubmittedGiftRequestDetailVerifyEditAndDeleteFunctionality";

        private string appNameExl;
        private string appName;
        private string moduleNameExl;
        private string actualRecipientContactName;
        private string actualRecipientCompanyName;
        private string giftRequestTitle;
        private string congratulationMsg;
        private string valGiftNameEntered;
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
         * TMTC0011872/T2017-	GiftLog – Gift Request Process – Gift Requests – Verify the Totals of the gifts are measured separately from the current year.

        */
        [Test]
        public void VerifyEditAndDeleteFunctionalityLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2012;

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
                string giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request Module ");

                // Enter required details in client gift pre- approval page
                valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT2012);

                //Verify company name
                actualRecipientCompanyName = giftRequest.GetAvailableRecipientCompanyLV();
                string expectedCompanyName = ReadExcelData.ReadData(excelPath, "GiftLog", 8);
                Assert.AreEqual(expectedCompanyName, actualRecipientCompanyName);
                extentReports.CreateLog("Company Name: " + actualRecipientCompanyName + " is listed in Available Recipient(s) table ");

                //Verify recipient contact name
                actualRecipientContactName = giftRequest.GetAvailableRecipientNameLV();
                expectedContactName = ReadExcelData.ReadData(excelPath, "GiftLog", 9);
                Assert.AreEqual(expectedContactName, actualRecipientContactName);
                extentReports.CreateLog("Recipient Name: " + actualRecipientContactName + " is listed in Available Recipient(s) table ");

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipientsLV();

                //Verify recipient name
                selectedRecipientName = giftRequest.GetSelectedRecipientNameLV();
                Assert.AreEqual(actualRecipientContactName, selectedRecipientName);
                extentReports.CreateLog("Recipient Name: " + selectedRecipientName + " in selected recipient(s) table matches with available recipient name listed in Available Recipient(s) table ");

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequestLV();
                giftApprove.ClickSubmitRequestLV();
                congratulationMsg = giftRequest.GetCongratulationsMsgLV();
                string congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                extentReports.CreateLog("Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                CustomFunctions.PageReload(driver);
                //giftsSubmit.GoToGiftsSubmittedPage();
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                giftsSubmit.CompareAndClickGiftDescWithGiftNameLV(valGiftNameEntered);

                string giftDetailTitle = giftsSubmit.GetGiftSubmittedDetailTitleLV();
                string giftDetailTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 12);
                Assert.AreEqual(giftDetailTitleExl, giftDetailTitle);
                extentReports.CreateLog("Gift Detail Title: " + giftDetailTitle + " in displayed upon click of Gift description link ");

                giftsSubmit.ClickEditButtonLV();
                string giftEditTitle = giftEdit.GetGiftRequestEditTitleLV();
                string giftEditTitleExl = ReadExcelData.ReadData(excelPath, "GiftEdit", 8);
                Assert.AreEqual(giftEditTitleExl, giftEditTitle);
                extentReports.CreateLog("Gift Edit Page Title: " + giftEditTitle + " in displayed upon upon click of edit button ");

                Assert.IsTrue(giftEdit.ValidateMandatoryFieldsLV());
                extentReports.CreateLog("Verification of on the Edit page -the fields “Gift Name, Gift Type, HL Relationship, Gift Value, Desired Date , Vendor, Reason For Gift, “ are displayed as required fields ");

                string UpdatedGiftNameEntered = giftEdit.EnterDetailsGiftEditRequestLV(fileT2012);

                string successMsg = giftEdit.GetSuccessGiftUpdateMessageLV();
                string successMsgExl= ReadExcelData.ReadData(excelPath, "GiftEdit", 7);
                Assert.AreEqual(successMsgExl, successMsg);
                extentReports.CreateLog("Success Message: " + successMsg + " is displayed upon successful editing gift details ");

                giftEdit.ClickCancelButtonLV();
                CustomFunctions.PageReload(driver);
                Assert.IsTrue(giftsSubmit.IsTableListPresentLV());
                extentReports.CreateLog("Gift Submitted Page is displayed with table upon click of cancel button ");
                
                giftsSubmit.CompareAndClickGiftDescWithGiftNameLV(UpdatedGiftNameEntered);

                string giftDetailTitles = giftsSubmit.GetGiftSubmittedDetailTitleLV();
                Assert.AreEqual(giftDetailTitleExl, giftDetailTitles);
                extentReports.CreateLog("Gift Detail Title: " + giftDetailTitles + " in displayed upon click of Gift description link ");

                string giftTypeEdit = giftsSubmit.GetGiftTypeUpdatedValueLV();
                string giftTypeEditExl = ReadExcelData.ReadData(excelPath, "GiftEdit", 1);
                Assert.AreEqual(giftTypeEditExl, giftTypeEdit);
                extentReports.CreateLog("Updated Gift Type: " + giftTypeEdit + " is matching with the value of gift type entered on gift edit page ");

                string giftHLRelationship = giftsSubmit.GetHLRelationshipUpdatedValueLV();
                string giftHLRelationshipExl = ReadExcelData.ReadData(excelPath, "GiftEdit", 4);
                Assert.AreEqual(giftHLRelationshipExl, giftHLRelationship);
                extentReports.CreateLog("Updated HL Relationship: " + giftHLRelationship + " is matching with the value of HL Relationship entered on gift edit page ");

                string giftValueEdit = giftsSubmit.GetGiftValueUpdatedLV();
                string giftValueEditExl = ReadExcelData.ReadData(excelPath, "GiftEdit", 6);
                Assert.AreEqual("USD " + giftValueEditExl, giftValueEdit);
                extentReports.CreateLog("Updated gift value: " + giftValueEdit + " is matching with the value of Gift value entered on gift edit page ");

                string giftReasonEdit = giftsSubmit.GetGiftReasonUpdatedValueLV();
                string giftReasonEditExl = ReadExcelData.ReadData(excelPath, "GiftEdit", 5);
                Assert.AreEqual(giftReasonEditExl, giftReasonEdit);
                extentReports.CreateLog("Updated gift reason: " + giftReasonEdit + " is matching with the value of Gift reason entered on gift edit page ");

                giftsSubmit.ClickEditButtonLV();
                string giftEditTitle2 = giftEdit.GetGiftRequestEditTitleLV();
                Assert.AreEqual(giftEditTitleExl, giftEditTitle2);
                extentReports.CreateLog("Gift Edit Page Title: " + giftEditTitle2 + " in displayed upon upon click of edit button ");

                giftEdit.ClickNewGiftRequestLV();
                string newGiftRequestTitle = giftRequest.GetEditPageNewGiftRequestPageTitleLV();
                Assert.AreEqual(giftRequestTitleExl, newGiftRequestTitle);
                extentReports.CreateLog("Page Title: " + newGiftRequestTitle + " is displayed upon click of new gift request button ");

                randomPages.CloseActiveTab("SL_GiftPreApproval");

                giftsSubmit.CompareAndClickGiftDescWithGiftNameLV(UpdatedGiftNameEntered);
                string giftDetailsTitle = giftsSubmit.GetGiftSubmittedDetailTitleLV();
                Assert.AreEqual(giftDetailTitleExl, giftDetailsTitle);
                extentReports.CreateLog("Gift Detail Title: " + giftDetailTitle + " in displayed upon click of Gift description link ");

                giftsSubmit.DeleteGiftSubmittedLV();
                CustomFunctions.SwitchToWindow(driver, 1);                
                string giftRequestTitles = giftRequest.GetGiftRequestPageTitleLV();
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitles);
                extentReports.CreateLog("Page Title: " + giftRequestTitles + " is displayed upon deletion of Gift Request ");
                driver.Close();
                CustomFunctions.SwitchToWindow(driver, 0);

                CustomFunctions.PageReload(driver);
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                Assert.IsFalse(giftsSubmit.IsGiftLogPresentInSubmittedListLV(UpdatedGiftNameEntered));
                extentReports.CreateLog("Selected Gift is not listed under Gift Submitted as part of delete functionality ");
                
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

