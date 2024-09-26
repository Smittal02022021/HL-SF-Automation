using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.GiftLog;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.Contact;

namespace SalesForce_Project.TestCases.GiftLog
{
    class LV_T1523_T2010_T1515_ApproveGiftsDefaultLayoutFieldsAndFieldsValues:BaseClass
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

        public static string fileT1523 = "LV_T1523_GiftLogApproveGiftsDefaultLayoutFieldsAndFieldsValues";
        private string giftRequestTitleExl;
        private string giftRequestTitle;
        private string congratulationMsg;
        private string congratulationMsgExl;
        private string valRecipientLastNameExl;
        private string valGiftNameEntered;
        private string contactType;
        private string valUser;
        private string userCompliance;
        private string user;
        private string appNameExl;
        private string appName;
        private string moduleNameExl;
        private string currentGiftAmtYTD;
        private string selectedCompanyName;
        private string actualRecipientCompanyName;
        private string actualRecipientContactName;
        private string expectedCompanyName;
        private string expectedContactName;
        private string selectedRecipientName;
        private string defaultApprovedStatus;
        private string approvedColumnValueInTable;
        private string giftStatus;
        private string warningMessage;
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
        public void VerifyApproveGiftsDefaultLayoutFieldsAndFieldsValuesLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT1523;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login ");

                //CF User
                valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "CF Fin User: " + valUser + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                 user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged in on Lightning View");

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
                valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT1523);

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

                //Verify company name
                selectedCompanyName = giftRequest.GetSelectedCompanyNameLV();
                Assert.AreEqual(actualRecipientCompanyName, selectedCompanyName);
                extentReports.CreateStepLogs("Passed", "Company Name: " + selectedCompanyName + " in selected recipient(s) table matches with available company name listed in Available Recipient(s) table ");

                //Verify Current GIft Amount YTD
                 currentGiftAmtYTD = giftRequest.GetCurrentGiftAmtYTDLV();
                extentReports.CreateStepLogs("Info", "Current GiftAmtYTD: " + currentGiftAmtYTD + " is displaying");

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequestLV();
                congratulationMsg = giftRequest.GetCongratulationsMsgLV();
                congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                extentReports.CreateLog("Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                driver.SwitchTo().DefaultContent();
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");

                //Login as Compliance User
                userCompliance = ReadExcelData.ReadData(excelPath, "Users", 2);
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

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Approve Selected button is visible on click of Approve Gifts Module");

                //Verification of default year selected in dropdown
                string getYear = DateTime.Today.ToString("yyyy");
                string defaultSelectedYear = giftApprove.GetDefaultSelectedYearLV();
                Assert.AreEqual(getYear, defaultSelectedYear);
                extentReports.CreateStepLogs("Passed", "Year: " + defaultSelectedYear + " is selected as default value in Year dropdown ");

                //Verification of default approved status
                 defaultApprovedStatus = giftApprove.GetDefaultSelectedApprovedStatusLV();
                Assert.AreEqual("Pending", defaultApprovedStatus);
                extentReports.CreateStepLogs("Passed", "ApprovedStatus: " + defaultApprovedStatus + " is selected as default value in Approved Status dropdown ");

                //Verification of text box for recipient name entry
                Assert.IsTrue(giftApprove.IsSearchTextBoxOfRecipientNameVisibilityLV(), "Search text box corresponding to recipient name is visible ");
                extentReports.CreateStepLogs("Passed", "Search text box corresponding to recipient last name is visible ");

                //Verification of text box for approval denial comment
                Assert.IsTrue(giftApprove.IsTextBoxForApprovalDenialCommentVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Text box for Approval/Denial comment is visible ");

                //Verification of Approval / Denial Comment
                string labelAppDenialCommentsExl = ReadExcelData.ReadData(excelPath, "GiftLog", 12);
                string labelAppDenialComments = giftApprove.GetlabelApprovalDenialCommentsLV();
                Assert.AreEqual(labelAppDenialCommentsExl, labelAppDenialComments);
                extentReports.CreateLog("Label: " + labelAppDenialComments + " is displayed ");

                //Verification of approve selected button
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Approve Selected Button is displayed ");

                //Verification of deny selected button
                Assert.IsTrue(giftApprove.DenySelectedButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Deny Selected Button is displayed ");

                //Search gift details by recipient last name
                valRecipientLastNameExl = ReadExcelData.ReadData(excelPath, "GiftLog", 13);
                giftApprove.SearchByRecipientLastNameLV(valRecipientLastNameExl);
                extentReports.CreateStepLogs("Info", "Approved Column is displayed with 'Pending' Status as default ");

                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                extentReports.CreateStepLogs("Passed", "Gift Description link matches with Gift Name. ");

                approvedColumnValueInTable = giftApprove.GetDefaultValuesUnderApprovedColumnInTableLV();
                Assert.AreEqual(defaultApprovedStatus, approvedColumnValueInTable);
                extentReports.CreateStepLogs("Passed", "Grid details are filtered based on Recipient Last name ");

                //Approve the gift created
                giftApprove.ClickApproveSelectedButtonLV();
                extentReports.CreateStepLogs("Info", "Gift Approved. ");

                //Search gift details by approved status
                //Note**Gift Log Gifts are not filtered when selected approved status from the drop-down(without Contact Last name
                //giftApprove.SelectApprovedStatusCombo("Approved");
                giftApprove.SearchByMonthYearAndStatusRecipientLastNameLV(valRecipientLastNameExl,"Approved");

                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                extentReports.CreateStepLogs("Passed", "Gift Description link matches with Gift Name. ");

                giftStatus = giftApprove.GetGiftStatusLV();
                Assert.AreEqual("Approved", giftStatus);
                extentReports.CreateStepLogs("Passed", giftStatus + "Filters are working correctly ");

                CustomFunctions.PageReload(driver);
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "Compliance User: " + userCompliance + " logged out");

                //CF Financial User login 
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "CF Fin User: " + valUser + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged in on Lightning View");

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

                //Enter required details in client gift pre- approval page
                valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT1523);

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

                //Verify company name
                selectedCompanyName = giftRequest.GetSelectedCompanyNameLV();
                Assert.AreEqual(actualRecipientCompanyName, selectedCompanyName);
                extentReports.CreateStepLogs("Passed", "Company Name: " + selectedCompanyName + " in selected recipient(s) table matches with available company name listed in Available Recipient(s) table ");

                //Verify Current GIft Amount YTD
                currentGiftAmtYTD = giftRequest.GetCurrentGiftAmtYTDLV();
                extentReports.CreateStepLogs("Info", "Current GiftAmtYTD: " + currentGiftAmtYTD + " is displaying");

                //Updating gift value to exceed current gift value
                giftRequest.EnterGiftValue(ReadExcelData.ReadData(excelPath, "GiftValue", 1));

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipientsLV();

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequestLV();
                warningMessage = giftRequest.GetWarningMessageOnAmountLimitExceedLV();
                warningMessageExl = ReadExcelData.ReadData(excelPath, "GiftLog", 15);
                Assert.AreEqual(warningMessageExl, warningMessage);
                extentReports.CreateStepLogs("Passed", "Warning Message: " + warningMessage + " is displayed upon submitting a gift request with gift amount exceeding $100 ");

                //Submit gift request after warning
                giftApprove.ClickSubmitRequestLV();

                //Switch from gift log to HL Force
                CustomFunctions.PageReload(driver);
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + userCompliance + " logged out");


                //Compliance User 
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

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Approve Selected button is visible on click of Approve Gifts Module");

                //Search gift details by recipient last name
                giftApprove.SearchByRecipientLastNameLV(valRecipientLastNameExl);
                extentReports.CreateStepLogs("Passed", "Approved Column is displayed with 'Pending' Status as default ");

                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                extentReports.CreateStepLogs("Passed", "Gift Description link matches with Gift Name. ");

                approvedColumnValueInTable = giftApprove.GetDefaultValuesUnderApprovedColumnInTableLV();
                Assert.AreEqual(defaultApprovedStatus, approvedColumnValueInTable);
                extentReports.CreateStepLogs("Passed", "Grid details are filtered based on Recipient Last name ");

                //Deny the gift created
                giftApprove.ClickDenySelectedButtonLV();
                extentReports.CreateStepLogs("Info", "Gift Denied. ");

                //Search gift details by denied status
                //Note**Gift Log Gifts are not filtered when selected approved status from the drop-down(without Contact Last name
                //giftApprove.SelectApprovedStatusCombo("Denied");
                giftApprove.SearchByMonthYearAndStatusRecipientLastNameLV(valRecipientLastNameExl,"Denied");

                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                extentReports.CreateStepLogs("Passed", "Gift Description link matches with Gift Name. ");

                giftStatus = giftApprove.GetGiftStatusLV();
                Assert.AreEqual("Denied", giftStatus);
                extentReports.CreateStepLogs("Passed", giftStatus + "Filters are working correctly ");

                driver.SwitchTo().DefaultContent();
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "Compliance User: " + userCompliance + " logged out");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.SwitchTo().DefaultContent();
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                string excelPath = ReadJSONData.data.filePaths.testData + fileT1523;
                conHome.ClickContact();
                conHome.SearchContact(fileT1523);
                //To Delete created contact
                contactDetails.DeleteCreatedContact(fileT1523, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));
                conHome.ClickContact();
                conHome.ClickAddContact();

                // Calling select record type and click continue function
                contactType = ReadExcelData.ReadData(excelPath, "Contact", 7);
                conSelectRecord.SelectContactRecordType(fileT1523, contactType);
                extentReports.CreateStepLogs("Info", "user navigate to create contact page upon click of continue button ");

                createContact.CreateContact(fileT1523);
                extentReports.CreateStepLogs("Info", "External Contact created again ");

                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }

        }
        [TearDown]
        public void TearDown()
        {
            string excelPath = ReadJSONData.data.filePaths.testData + fileT1523;
            conHome.ClickContact();
            conHome.SearchContact(fileT1523);
            //To Delete created contact
            contactDetails.DeleteCreatedContact(fileT1523, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));
            conHome.ClickContact();
            conHome.ClickAddContact();

            // Calling select record type and click continue function
            contactType = ReadExcelData.ReadData(excelPath, "Contact", 7);
            conSelectRecord.SelectContactRecordType(fileT1523, contactType);
            extentReports.CreateStepLogs("Info", " TearDown user navigate to create contact page upon click of continue button ");

            createContact.CreateContact(fileT1523);
            extentReports.CreateStepLogs("Info", "External Contact created again ");

            usersLogin.UserLogOut();
            driver.Quit();
            extentReports.CreateStepLogs("Info", "Browser Closed");
        }
    }
}