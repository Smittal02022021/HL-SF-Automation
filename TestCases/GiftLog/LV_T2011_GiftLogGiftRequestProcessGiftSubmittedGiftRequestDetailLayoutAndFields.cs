using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
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

namespace SF_Automation.TestCases.GiftLog
{
    class LV_T2011_GiftLogGiftRequestProcessGiftSubmittedGiftRequestDetailLayoutAndFields:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();
        GiftSubmittedPage giftsSubmit = new GiftSubmittedPage();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileT2011 = "LV_T2011_GiftLogGiftRequestProcessGiftSubmittedGiftRequestDetailLayoutAndFields";
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
        public void VerifyGiftSubmittedGiftRequestDetailLayoutAndFields()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2011;
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
                extentReports.CreateStepLogs("Info", "CF Financial User: " + valUser + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + valUser + " logged in on Lightning View");

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
                string valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT2011);

                //Verify company name
                string actualRecipientCompanyName = giftRequest.GetAvailableRecipientCompanyLV();
                string expectedCompanyName = ReadExcelData.ReadData(excelPath, "GiftLog", 8);
                Assert.AreEqual(expectedCompanyName, actualRecipientCompanyName);
                extentReports.CreateStepLogs("Passed", "Company Name: " + actualRecipientCompanyName + " is listed in Available Recipient(s) table ");

                //Verify recipient contact name
                string actualRecipientContactName = giftRequest.GetAvailableRecipientNameLV();
                string expectedContactName = ReadExcelData.ReadData(excelPath, "GiftLog", 9);
                Assert.AreEqual(expectedContactName, actualRecipientContactName);
                extentReports.CreateStepLogs("Passed", "Recipient Name: " + actualRecipientContactName + " is listed in Available Recipient(s) table ");

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipientsLV();

                //Verify recipient name
                string selectedRecipientName = giftRequest.GetSelectedRecipientNameLV();
                Assert.AreEqual(actualRecipientContactName, selectedRecipientName);
                extentReports.CreateStepLogs("Passed", "Recipient Name: " + selectedRecipientName + " in selected recipient(s) table matches with available recipient name listed in Available Recipient(s) table ");

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequestLV();
                giftApprove.ClickSubmitRequestLV();
                string congratulationMsg = giftRequest.GetCongratulationsMsgLV();
                string congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                extentReports.CreateStepLogs("Passed", "Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                driver.SwitchTo().DefaultContent();
                //giftsSubmit.GoToGiftsSubmittedPage();
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                giftsSubmit.CompareAndClickGiftDescWithGiftNameLV(valGiftNameEntered);
                extentReports.CreateStepLogs("Info", "Clicked on gift description successfully. ");

                //Validate Gift Name field and value
                Assert.IsTrue(giftApprove.VerifyGiftNameInGiftRequestDetailsLV(valGiftNameEntered));
                extentReports.CreateStepLogs("Passed", "Gift Name field is displayed with correct value under Gift Request detail section on gifts submitted page. ");

                //Validate Gift Type field and value
                string valGiftTypeExl = ReadExcelData.ReadData(excelPath, "GiftLog", 1);
                Assert.IsTrue(giftApprove.VerifyGiftTypeInGiftRequestDetailsLV(valGiftTypeExl));
                extentReports.CreateStepLogs("Passed", "Gift Type: "+ valGiftTypeExl+" field is displayed with correct value under Gift Request detail section on gifts submitted page. ");

                //Validate Recipient For Gift field and value
                string valRecipientNameExl = ReadExcelData.ReadData(excelPath, "GiftLog", 9);
                Assert.IsTrue(giftApprove.VerifyRecipientForInGiftRequestDetailsLV(valRecipientNameExl));
                extentReports.CreateStepLogs("Passed", "Recipient For Gift: "+valRecipientNameExl+" field is displayed with correct value under Gift Request detail section on gifts submitted page. ");

                //Validate Submitted For field and value
                string valSubmittedForExl = ReadExcelData.ReadData(excelPath, "GiftLog", 2);
                Assert.IsTrue(giftApprove.VerifySubmittedForInGiftRequestDetailsLV(valSubmittedForExl));
                extentReports.CreateStepLogs("Passed", "Submitted For: "+ valSubmittedForExl+" field is displayed with correct value under Gift Request detail section on gifts submitted page. ");

                //Validate Vendor field and value
                string valVandorExl = ReadExcelData.ReadData(excelPath, "GiftLog", 6);
                Assert.IsTrue(giftApprove.VerifyVendorInGiftRequestDetailsLV(valVandorExl));
                extentReports.CreateStepLogs("Passed","Vendor: "+ valVandorExl+" field is displayed with correct value under Gift Request detail section on gifts submitted page. ");

                //Validate Gift Value field and value
                string valGiftExl = ReadExcelData.ReadData(excelPath, "GiftLog", 3);
                Assert.IsTrue(giftApprove.VerifyGiftValueInGiftRequestDetailsLV(valGiftExl));
                extentReports.CreateStepLogs("Passed", "Gift Value: "+ valGiftExl+" field is displayed with correct value under Gift Request detail section on gifts submitted page. ");

                //Validate Currency field and value
                string valCurrencyExl = ReadExcelData.ReadData(excelPath, "GiftLog", 5);
                Assert.IsTrue(giftApprove.VerifyCurrencyInGiftRequestDetailsLV(valCurrencyExl));
                extentReports.CreateStepLogs("Passed", "Currency: "+ valCurrencyExl+" field is displayed with correct value under Gift Request detail section on gifts submitted page. ");

                //Validate HL Relationship field and value
                string valHLRelationshipExl = ReadExcelData.ReadData(excelPath, "GiftLog", 4);
                Assert.IsTrue(giftApprove.VerifyHLRelationshipInGiftRequestDetailsLV(valHLRelationshipExl));
                extentReports.CreateStepLogs("Passed", "HL Relationship: "+ valHLRelationshipExl+" field is displayed with correct value under Gift Request detail section on gifts submitted page. ");

                //Validate Reason For Gift field and value
                string valReasonForGiftExl = ReadExcelData.ReadData(excelPath, "GiftLog", 7);
                Assert.IsTrue(giftApprove.VerifyReasonForGiftInGiftRequestDetailsLV(valReasonForGiftExl));
                extentReports.CreateStepLogs("Passed", "Reason For Gift: "+ valReasonForGiftExl+" field is displayed with correct value under Gift Request detail section on gifts submitted page. ");

                //Validate Approval Number field and value
                Assert.AreEqual("Pending", giftApprove.GetApprovalNumberFromGiftRequestDetailsLV());
                extentReports.CreateStepLogs("Passed", "Approval Number field is displayed with value as Pending on gifts submitted page. ");

                //Validate Created By field and value                
                Assert.IsTrue(giftApprove.VerifyCreatedByInGiftRequestDetailsLV(valUser));
                extentReports.CreateStepLogs("Passed", "Created By: "+ valUser+" field is displayed with correct value under Gift Request detail section on gifts submitted page. ");


                driver.SwitchTo().DefaultContent();
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + valUser + " logged out");
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