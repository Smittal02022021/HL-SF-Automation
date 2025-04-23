using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.GiftLog;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.Contact;

namespace SF_Automation.TestCases.GiftLog
{
    class LV_T1516_T1517_T1518_T1519_T1520_T2005_ClientGiftPreApprovalPageRecipientsExceedsYearlyGiftAllowanceReviseRequestAndSubmitsRequestForCurrencyTypeUSDollar : BaseClass
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

        public static string fileT1516 = "LV_T1516_GiftLog_ClientGiftPreApprovalPageRecipientsExceedsYearlyGiftAllowance";
        private string actualRecipientContactName;
        private string actualRecipientCompanyName;
        private string currencyCode;
        private string colorOfGiftValue;
        private string colorOfGiftValueExl;
        private string giftRequestTitle;
        private string warningMessage;
        private string congratulationMsg;
        private string congratulationMsgExl;
        private string recipientOnGiftRequestDetail;
        private string recipientNameExl;
        private string valGiftNameEntered;
        private string submittedForValue;
        private string submittedForValueExl;
        private string expectedContactName;
        private string contactType;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        /*
         *  TMTC0011803/T1516	Client Gift Pre-Approval Page - Recipients exceeds yearly gift allowance (>100$)  - Revise Request and Submits Request for Currency Type U.S. Dollar
            TMTC0011806/T1517	Client Gift Pre-Approval Page - Recipients exceeds yearly gift allowance(>100 Euro ) for Currency Type Euro (Not in France)
            TMTC0011809/T1518	Client Gift Pre-Approval Page - Recipients exceeds yearly gift allowance(>65 Euro ) for Currency Type Euro (in France)
            TMTC0011812/T1519	Client Gift Pre-Approval Page - Recipients exceeds yearly gift allowance (>100 Pounds) for Currency Type British Pounds
            TMTC0011815/T1520	Client Gift Pre-Approval Page - Recipients exceeds yearly gift allowance (>100 dollars) for Currency Type Hong Kong dollar
            TMTC0011850/T2005	GiftLog – Gift Request Process – Recipients exceeds yearly gift allowance (>100 AUD) for Currency Type Australian Dollar.
         */

        [Test]
        public void VerifyGiftPreApprovalPageRecipientsExceedsYearlyGiftAllowanceReviseRequestAndSubmitsRequestForCurrencyTypeUSDollarLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT1516;

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
                extentReports.CreateStepLogs("Info", "CF Fin User: " + valUser + " details are displayed. ");
                //Login user
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
                extentReports.CreateStepLogs("Info", "CF Fin User is on " + moduleNameExl + " Page ");

                //Navigate to Gift Request page                
                giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                string giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");


                // Enter required details in client gift pre- approval page
                giftRequest.EnterDetailsGiftRequestLV(fileT1516);
                //Verify company name
                actualRecipientCompanyName = giftRequest.GetAvailableRecipientCompanyLV();
                string expectedCompanyName = ReadExcelData.ReadData(excelPath, "GiftLog", 8);
                Assert.AreEqual(expectedCompanyName, actualRecipientCompanyName);
                extentReports.CreateStepLogs("Passed", "Company Name: " + actualRecipientCompanyName + " is listed in Available Recipient(s) table ");

                //Verify recipient contact name
                actualRecipientContactName = giftRequest.GetAvailableRecipientNameLV();
                expectedContactName = ReadExcelData.ReadData(excelPath, "GiftLog", 9);
                Assert.AreEqual(expectedContactName, actualRecipientContactName);
                extentReports.CreateStepLogs("Passed", "Recipient Name: " + actualRecipientContactName + " is listed in Available Recipient(s) table ");

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipientsLV();
                string labelGiftAmtYTD = giftRequest.GetLabelNewGiftAmtYTDLV();
                string expectedLabelGiftAmtYTD = ReadExcelData.ReadData(excelPath, "GiftLog", 12);
                Assert.AreEqual(expectedLabelGiftAmtYTD, labelGiftAmtYTD);
                extentReports.CreateStepLogs("Passed", "Gift Label: " + labelGiftAmtYTD + " is displayed in Selected Recipient(s) table ");

                // Verify value of gift
                string valueOfGift = giftRequest.GetGiftValueInGiftAmtYTDLV();
                string valueOfGiftExl = ReadExcelData.ReadData(excelPath, "GiftLog", 3);
                Assert.AreEqual(valueOfGiftExl, valueOfGift);
                extentReports.CreateStepLogs("Passed", "Gift Value: " + valueOfGift + " is displayed in Selected Recipient(s) table ");

                //Verify currency of gift
                currencyCode = giftRequest.GetGiftCurrencyCodeLV();
                Assert.AreEqual("USD", currencyCode);
                extentReports.CreateStepLogs("Passed", "Currency Code: " + currencyCode + " is displayed in Selected Recipient(s) table ");

                //Verification of NewGiftAmtYTD Value turn to red to indicate Currency max limit Exceeded for Current Calendar Year
                colorOfGiftValue = giftRequest.GetGiftValueColorInGiftAmtYTDLV();
                colorOfGiftValueExl = ReadExcelData.ReadData(excelPath, "GiftLog", 13);
                Assert.AreEqual(colorOfGiftValueExl, colorOfGiftValue);
                extentReports.CreateStepLogs("Passed", "Color Of Gift Value: " + colorOfGiftValue + " is displayed in Selected Recipient(s) table ");

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequestLV();
                warningMessage = giftRequest.GetWarningMessageOnAmountLimitExceedLV();
                string warningMessageExl = ReadExcelData.ReadData(excelPath, "GiftLog", 14);
                Assert.AreEqual(warningMessageExl, warningMessage);
                extentReports.CreateStepLogs("Passed", "Warning Message: " + warningMessage + " is displayed upon submitting a gift request with gift amount exceeding $100 ");

                //Verify revise request button visible
                Assert.IsTrue(giftRequest.IsReviseRequestButtonVisibleLV());
                extentReports.CreateStepLogs("Passed", "Revise Request button is visible on warning message page ");

                //Verify submit request button visible
                Assert.IsTrue(giftRequest.IsSubmitRequestButtonVisibleLV());
                extentReports.CreateStepLogs("Passed", "Submit Request button is visible on warning message page ");

                //Click on revise request button
                giftRequest.ClickReviseRequestButtonLV();

                //Verify Submit Gift Request button visibile after click on revise request button
                Assert.IsTrue(giftRequest.IsSubmitGiftRequestButtonVisibleLV());
                extentReports.CreateStepLogs("Passed", "Submit Gift Request button is visible upon click of revise request button ");

                //Verification of landing on Pre-approval page upon Click on Revise Request Button
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of revise request button ");

                //Verification of recipient exceeding yearly allowance for different currency types
                int rowCount = ReadExcelData.GetRowCount(excelPath, "GiftLog_Currency");
                for(int row = 2; row <= rowCount; row++)
                {
                    randomPages.ReloadPage();
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                    Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                    extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                    // Enter required details in client gift pre- approval page
                    valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT1516);
                    extentReports.CreateStepLogs("Info", valGiftNameEntered + ": details entered ");

                    //Select currency drop down
                    string giftCurrency = ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLog_Currency", row, 1);
                    string giftval = ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLog_Currency", row, 2);
                    giftRequest.SelectCurrencyDrpDownLV(giftCurrency);

                    giftRequest.EnterGiftValueLV(giftval);
                    giftRequest.ClickAddRecipientLV();

                    giftRequest.AddRecipientToSelectedRecipientsLV();
                    string newGiftAmtYTD = giftRequest.GetGiftValueInGiftAmtYTDLV();
                    extentReports.CreateStepLogs("Info", "New Gift Amount: " + newGiftAmtYTD + " is displaying");

                    // Verify currency of gift
                    string currencyCode = giftRequest.GetGiftCurrencyCodeLV();
                    string currencyCodeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLog_Currency", row, 3);
                    Assert.AreEqual(currencyCodeExl, currencyCode);
                    extentReports.CreateStepLogs("Passed", "Currency Code: " + currencyCode + " is displayed in Selected Recipient(s) table ");

                    //Verification of NewGiftAmtYTD Value turn to red to indicate Currency max limit Exceeded for Current Calendar Year
                    colorOfGiftValue = giftRequest.GetGiftValueColorInGiftAmtYTDLV();
                    string colorOfGiftValueExl = ReadExcelData.ReadData(excelPath, "GiftLog", 13);
                    Assert.AreEqual(colorOfGiftValueExl, colorOfGiftValue);
                    extentReports.CreateStepLogs("Passed", "Color Of Gift Value: " + colorOfGiftValue + " is displayed in Selected Recipient(s) table ");

                    //Click on submit gift request
                    giftRequest.ClickSubmitGiftRequestLV();
                    //Verification of error message displaying on submit of gift request exceeds yearly gift allowance for currency type euro(not in france)
                    warningMessage = giftRequest.GetWarningMessageOnAmountLimitExceedLV();

                    Assert.AreEqual(warningMessageExl, warningMessage);
                    extentReports.CreateStepLogs("Passed", "Warning Message: " + warningMessage + " is displayed upon submitting a gift request with gift amount exceeding $100 ");

                    //CLick on submit request button
                    giftRequest.ClickSubmitRequestButtonLV();
                    Assert.IsTrue(giftRequest.IsReturnToPreApprovalPageVisibleLV());
                    extentReports.CreateStepLogs("Passed", "Return to pre approval page button is visible upon click of submit request button ");

                    //Verify congratulation message upon successful gift request completion
                    congratulationMsg = giftRequest.GetCongratulationsMsgLV();
                    congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                    Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                    extentReports.CreateStepLogs("Passed", "Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                    //Verify Gift description 
                    string giftDescriptionGiftRequestDetail = giftRequest.GetGiftDescriptionOnGiftRequestDetailLV();
                    Assert.AreEqual(valGiftNameEntered, giftDescriptionGiftRequestDetail);
                    extentReports.CreateStepLogs("Passed", "Gift Description: " + giftDescriptionGiftRequestDetail + " is listed on gift request submission detail page ");

                    //Verify recipient name
                    recipientOnGiftRequestDetail = giftRequest.GetRecipientNameOnGiftRequestDetail();
                    recipientNameExl = ReadExcelData.ReadData(excelPath, "GiftLog", 9);
                    Assert.AreEqual(recipientNameExl, recipientOnGiftRequestDetail);
                    extentReports.CreateLog("Recipient Name: " + recipientOnGiftRequestDetail + " is listed on gift request submission detail page ");

                    submittedForValue = giftApprove.GetvalueSubmittedForLV();
                    submittedForValueExl = ReadExcelData.ReadData(excelPath, "GiftLog", 2);
                    Assert.AreEqual(submittedForValueExl, submittedForValue);
                    extentReports.CreateLog("Submitted For: " + submittedForValue + " is listed on gift request submission detail page ");

                    //Verify currency value
                    currencyCode = giftApprove.GetGiftCurrencyCodeLV();
                    currencyCodeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLog_Currency", row, 4);
                    Assert.AreEqual(currencyCodeExl, currencyCode);
                    extentReports.CreateStepLogs("Passed", "Currency: " + currencyCode + " is listed on gift request submission detail page ");

                    string giftValueOnGiftDetail1 = giftApprove.GetGiftValueLV();
                    Assert.AreEqual(giftval, giftValueOnGiftDetail1);
                    extentReports.CreateStepLogs("Passed", "Gift Value: " + giftValueOnGiftDetail1 + " is listed on gift request submission detail page ");

                    //Click on return to pre-approval page button
                    giftRequest.ClickReturnToPreApprovalPageLV();
                    randomPages.CloseActiveTab("SL_GiftPreApproval");
                }
                driver.SwitchTo().DefaultContent();
                usersLogin.ClickLogoutFromLightningView();
                driver.Quit();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch(Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.SwitchTo().DefaultContent();
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                string excelPath = ReadJSONData.data.filePaths.testData + fileT1516;
                conHome.ClickContact();
                //conHome.SearchContact(fileT1516);
                //To Delete created contact                
                try
                {
                    contactDetails.DeleteCreatedContact(fileT1516, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));
                }
                catch
                {//no record found
                }
                conHome.ClickContact();
                conHome.ClickAddContact();

                // Calling select record type and click continue function
                contactType = ReadExcelData.ReadData(excelPath, "Contact", 7);
                conSelectRecord.SelectContactRecordType(fileT1516, contactType);
                extentReports.CreateLog("user navigate to create contact page upon click of continue button ");

                createContact.CreateContact(fileT1516);
                extentReports.CreateLog("External Contact created again ");

                usersLogin.UserLogOut();
                driver.Quit();

            }

        }
    }
}