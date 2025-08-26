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
using System.Threading;

namespace SF_Automation.TestCases.GiftLog
{
    class LV_T1524_T2014_GiftApprovalProcessApproveGiftsVerifyPendingApprovedAndDeniedGiftsEditRights:BaseClass
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
        GiftRequestEditPage giftEdit = new GiftRequestEditPage();
        ContactHomePage conHome = new ContactHomePage();
        RandomPages randomPages = new RandomPages();
        GiftSubmittedPage giftsSubmit = new GiftSubmittedPage();

        public static string fileT2014 = "LV_T2014_GiftApprovalProcessApproveGiftsVerifyPendingApprovedAndDeniedGiftsEditRights";
        private string giftRequestTitle;
        private string valGiftNameEntered;
        private string contactType;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyPendingApprovedAndDeniedGiftsEditRightsLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2014;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");               
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
                giftRequest.SetDesiredDateToCurrentDateLV();
                valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT2014);

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

                //Approve page
                randomPages.ReloadPage();
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");

                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Approve Selected button is visible on click of approve gifts tab ");

                //Search gift details by recipient last name
                string valRecipientLastNameExl = ReadExcelData.ReadData(excelPath, "GiftLog", 13);
                giftApprove.SearchByRecipientLastNameLV(valRecipientLastNameExl);
                extentReports.CreateStepLogs("Info", "Approved Column is displayed with 'Pending' Status as default ");

                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                extentReports.CreateStepLogs("Passed", "Gift Description link matches with Gift Name and clicked ");

                //Verify edit button is visible
                Assert.IsTrue(giftApprove.EditButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Edit button is visible in Gift Request Detail section upon click of gift description link ");

                //Verify delete button is not visible
                Assert.IsFalse(giftApprove.IsDeleteButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Delete button is not visible in Gift Request Detail section upon click of gift description link ");

                //Click edit button
                giftsSubmit.ClickEditButtonLV();
                string giftEditTitle = giftEdit.GetGiftRequestEditTitleLV();
                string giftEditTitleExl = ReadExcelData.ReadData(excelPath, "GiftEdit", 8);
                Assert.AreEqual(giftEditTitleExl, giftEditTitle);
                extentReports.CreateStepLogs("Passed", "Gift Edit Page Title: " + giftEditTitle + " in displayed upon upon click of edit button ");

                //Verify gift name field is editable
                Assert.AreEqual("Element is editable", giftEdit.IsGiftNameEditableLV());
                extentReports.CreateStepLogs("Passed", "Field: Gift Name is editable on gift request edit page ");

                //Verify gift type field is editable
                Assert.AreEqual("Element is editable", giftEdit.IsGiftTypeEditableLV());
                extentReports.CreateStepLogs("Passed", "Field: Gift Type is editable on gift request edit page ");

                //Verify currency field is editable
                Assert.AreEqual("Element is editable", giftEdit.IsCurrencyEditableLV());
                extentReports.CreateStepLogs("Passed", "Field: Currency is editable on gift request edit page ");

                //Verify HL relationship field is editable
                Assert.AreEqual("Element is editable", giftEdit.IsHLRelationshipEditableLV());
                extentReports.CreateStepLogs("Passed", "Field: HL Relationship is editable on gift request edit page ");

                //Verify vendor field is editable
                Assert.AreEqual("Element is editable", giftEdit.IsVendorEditableLV());
                extentReports.CreateStepLogs("Passed", "Field: Vendor is editable on gift request edit page ");

                //Verify gift value field is editable
                Assert.AreEqual("Element is editable", giftEdit.IsGiftValueEditableLV());
                extentReports.CreateStepLogs("Passed", "Field: Gift Value is editable on gift request edit page ");

                //Verify desired date field is editable
                Assert.AreEqual("Element is editable", giftEdit.IsDesiredDateEditableLV());
                extentReports.CreateStepLogs("Passed", "Field: Desired Date is editable on gift request edit page ");
                //CloseActivetab
                randomPages.CloseActiveTab("Edit "+ valGiftNameEntered);
                randomPages.ReloadPage();
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Approve Selected button is visible on click of approve gifts tab ");

                //Search gift details by recipient last name
                giftApprove.SearchByRecipientLastNameLV(valRecipientLastNameExl);
                extentReports.CreateStepLogs("Info", "Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                // Compare gift name with gift description available
                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                extentReports.CreateStepLogs("Passed", "Gift Description link matches with Gift Name and checkbox clicked ");

                // Click on approve selected button
                giftApprove.SetApprovalDenialCommentsLV();
                giftApprove.ClickApproveSelectedButtonLV();
                extentReports.CreateStepLogs("Info", "Approve selected button is clicked successfully ");

                // Search gifts by apporved status
                giftApprove.SearchByRecipientLastNameAndStatusLV(valRecipientLastNameExl, "Approved");
                extentReports.CreateStepLogs("Info", "Gift List table is displayed with approved status upon search by selecting 'Approved' option in Approved Status ");

                // Compare gift name with gift description available
                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                extentReports.CreateStepLogs("Passed", "Gift Description link matches with Gift Name and checkbox clicked ");

                //Click edit button
                giftsSubmit.ClickEditButtonLV();
                string giftEditTitle2 = giftEdit.GetGiftRequestEditTitleLV();
                Assert.AreEqual(giftEditTitleExl, giftEditTitle2);
                extentReports.CreateStepLogs("Passed", "Gift Edit Page Title: " + giftEditTitle + " in displayed upon upon click of edit button ");

                //Verify gift value field is editable 
                Assert.AreEqual("Element is editable", giftEdit.IsGiftValueAfterGiftApproveEditableLV());
                extentReports.CreateStepLogs("Passed", "Field: Gift Name is editable on gift request edit page ");

                //Verify gift value field is editable 
                Assert.AreEqual("Element is editable", giftEdit.IsApporvedDropDownEditableLV());//Classic Element is editable
                extentReports.CreateStepLogs("Passed", "Field: Gift Value is editable on gift request edit page ");

                //Verify Apporval Comments field is editable
                Assert.AreEqual("Element is editable", giftEdit.IsApporvalCommentsEditableLV());
                extentReports.CreateStepLogs("Passed", "Field: Apporval Comments is editable on gift request edit page ");

                //Verify gift selected view
                Assert.AreEqual("Approved", giftEdit.GetGiftSelectedViewLV());
                extentReports.CreateStepLogs("Passed", "Request Edit Page is displayed for the gift selected with Approved View ");
                randomPages.CloseActiveTab("Edit " + valGiftNameEntered);
                //Click on Approve Gifts tab
                randomPages.ReloadPage();
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Approve Selected button is visible on click of approve gifts tab ");

                // Search gifts by apporved status  
                giftApprove.SearchByRecipientLastNameAndStatusLV(valRecipientLastNameExl, "Approved");
                extentReports.CreateStepLogs("Info", "Gift List table is displayed with approved status upon search by selecting 'Approved' option in Approved Status ");

                // Compare gift name with gift description available
                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                extentReports.CreateStepLogs("Passed", "Gift Description link matches with Gift Name and checkbox clicked ");

                // Verification of deny selected button
                Assert.IsTrue(giftApprove.DenySelectedButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Deny Selected Button is displayed ");

                //Click on deny selected button
                giftApprove.SetApprovalDenialCommentsLV();
                giftApprove.ClickDenySelectedButtonLV();
                extentReports.CreateStepLogs("Info", "Click on Deny Selected Button successfully ");

                // Search gifts by denied status  
                giftApprove.SearchByRecipientLastNameAndStatusLV(valRecipientLastNameExl, "Denied");
                extentReports.CreateStepLogs("Info", "Gift List table is displayed with Denied status upon search by selecting 'Denied' option in Approved Status ");

                // Compare gift name with gift description available
                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                extentReports.CreateStepLogs("Passed", "Gift Description link matches with Gift Name and checkbox clicked ");

                //Click edit button
                giftsSubmit.ClickEditButtonLV();
                string giftEditTitle3 = giftEdit.GetGiftRequestEditTitleLV();
                Assert.AreEqual(giftEditTitleExl, giftEditTitle3);
                extentReports.CreateStepLogs("Passed", "Gift Edit Page Title: " + giftEditTitle + " in displayed upon upon click of edit button ");

                //Verify approved dropdown is editable
                Assert.AreEqual("Element is editable", giftEdit.IsApporvedDropDownEditableLV());
                extentReports.CreateStepLogs("Passed", "Field: Approved is editable on gift request edit page ");

                //Verify approval comments is editable
                Assert.AreEqual("Element is editable", giftEdit.IsApporvalCommentsEditableLV());
                extentReports.CreateStepLogs("Passed", "Field: Approval comments is editable on gift request edit page ");

                //Verify gifts list is displayed with denied view
                Assert.AreEqual("Denied", giftEdit.GetGiftSelectedViewLV());
                extentReports.CreateLog("Request Edit Page is displayed for the gift selected with Denied View ");
                randomPages.CloseActiveTab("Edit " + valGiftNameEntered);
                randomPages.ReloadPage();
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "CF Fin User is on " + moduleNameExl + " Page ");

                //Navigate to Gift Request page                
                giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                // Enter required details in client gift pre- approval page
                giftRequest.SetDesiredDateToCurrentDateLV();
                string valGiftNameEntered1 = giftRequest.EnterDetailsGiftRequestLV(fileT2014);
                giftRequest.EnterGiftValue(ReadExcelData.ReadData(excelPath, "GiftValue", 1));

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipientsLV();

                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequestLV();
                giftRequest.ClickSubmitRequestButtonLV();
                string congratulationMsg1 = giftRequest.GetCongratulationsMsgLV();
                Assert.AreEqual(congratulationMsgExl, congratulationMsg1);
                extentReports.CreateStepLogs("Passed", "Congratulations message: " + congratulationMsg1 + " in displayed upon successful submission of gift request ");

                //Click on approve gifts tab
                randomPages.ReloadPage();
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");
                Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                extentReports.CreateStepLogs("Passed", "Approve Selected button is visible on click of approve gifts tab ");

                //Search gift details by recipient last name
                giftApprove.SearchByRecipientLastNameLV(valRecipientLastNameExl);
                extentReports.CreateStepLogs("Info", "Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                // Click on approve selected button
                giftApprove.ClickApproveSelectedButtonLV();
                extentReports.CreateStepLogs("Info", "Approve selected button is clicked successfully ");

                String ErrorMsgApproveGiftText = giftApprove.ErrorMsgForApproveGiftLV();
                Assert.IsTrue(ErrorMsgApproveGiftText.Contains("You must select at least one gift to approve."));
                extentReports.CreateStepLogs("Passed", "Error message:" + ErrorMsgApproveGiftText + " is displaying ");

                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered1));
                giftApprove.ClickApproveSelectedButtonLV();

                string ErrorMsgApprovalComment = giftApprove.ErrorMsgApprovalCommentLV();                
                Assert.IsTrue(ErrorMsgApprovalComment.Contains("You MUST enter an Approval Comment to exceed the yearly limit. Recipients exceeding yearly limit"));
                extentReports.CreateStepLogs("Passed", "Error message:" + ErrorMsgApprovalComment + " is displaying ");

                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered1));
                giftApprove.SetApprovalDenialCommentsLV();
                giftApprove.ClickApproveSelectedButtonLV();

                giftApprove.SearchByRecipientLastNameAndStatusLV(valRecipientLastNameExl, "Approved");
                Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered1));

                //Click on deny selected button
                giftApprove.ClickDenySelectedButtonLV();
                extentReports.CreateStepLogs("Info", "Click on Deny Selected Button successfully ");

                driver.SwitchTo().DefaultContent();
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "Compliance User: " + valUser + " logged out");
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.SwitchTo().DefaultContent();
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2014;
                conHome.ClickContact();
                //To Delete created contact
                try
                {
                    contactDetails.DeleteCreatedContact(fileT2014, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));
                }
                catch{
                    //no record found
                }
                conHome.ClickContact();
                conHome.ClickAddContact();

                // Calling select record type and click continue function
                contactType = ReadExcelData.ReadData(excelPath, "Contact", 7);
                conSelectRecord.SelectContactRecordType(fileT2014, contactType);
                extentReports.CreateStepLogs("Info", "User navigate to create contact page upon click of continue button ");

                createContact.CreateContact(fileT2014);
                extentReports.CreateStepLogs("Info", "External Contact created again ");

                usersLogin.UserLogOut();
                driver.Quit();
                
            }
        }        
    }
}