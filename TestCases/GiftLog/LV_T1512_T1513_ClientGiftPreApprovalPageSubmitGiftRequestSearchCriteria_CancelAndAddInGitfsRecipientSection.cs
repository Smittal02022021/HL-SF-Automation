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
    class LV_T1512_T1513_ClientGiftPreApprovalPageSubmitGiftRequestSearchCriteria_CancelAndAddInGitfsRecipientSection : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileTC1513 = "LV_T1513_ClientGiftPre_ApprovalPage_AddingaRecipientToTheSelectedRecipientSection";
        private string actualRecipientContactName;
        private string actualRecipientCompanyName;
        private string comboSelectionExl;
        private string nameCompanyExl;
        private string nameContactExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyGiftRequestSearchCriteriaCancelAndAddInGitfsRecipientSectionLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1513;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login ");

                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
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
                string giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                string giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                //TMTC0011791/T1512-Client Gift Pre-Approval Page - Submit Gift Request - Search criteria in Gitfs Recipient(s) section.
                string valGiftName = giftRequest.EnterDetailsGiftRequestLV(fileTC1513);
                extentReports.CreateStepLogs("Info", valGiftName + " Gift Details are entered successfully without adding recipients to selected recipients");

                for (int row = 2; row <= 5; row++)
                {
                    giftRequest.ClearGiftRecipientsDetailsLV();
                    comboSelectionExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SearchCriteria", row, 2);
                    nameCompanyExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SearchCriteria", row, 1);
                    
                    giftRequest.SearchWithCompnyNameComboBoxLV(comboSelectionExl, nameCompanyExl);
                    //Verify company name Combo Box
                    actualRecipientCompanyName = giftRequest.GetAvailableRecipientCompanyLV();
                    Assert.IsTrue(actualRecipientCompanyName.Contains(nameCompanyExl));
                    extentReports.CreateStepLogs("Passed", "Company Name: " + actualRecipientCompanyName + " is listed in Available Recipient(s) table for contains combo box in Cmmpany Name ");

                    giftRequest.ClearGiftRecipientsDetailsLV();
                    nameContactExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SearchCriteria", row + 4, 1);
                    giftRequest.SearchWithContactNameComboBoxLV(comboSelectionExl, nameContactExl);

                    //Verify contact name Combo Box
                    actualRecipientContactName = giftRequest.GetAvailableRecipientNameLV();
                    Assert.IsTrue(actualRecipientContactName.Contains(nameContactExl));
                    extentReports.CreateStepLogs("Passed", "Recipient Name: " + actualRecipientContactName + " is listed in Available Recipient(s) table for Contains combo box in Contact Name ");

                    giftRequest.ClearGiftRecipientsDetailsLV();
                    nameCompanyExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SearchCriteria", row + 8, 1);
                    nameContactExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SearchCriteria", row + 12, 1);
                    giftRequest.SearchWithCompanyContactNameComboBoxLV(comboSelectionExl, comboSelectionExl, nameCompanyExl, nameContactExl);

                    //Verify Company and Contact name Combo Box
                    actualRecipientContactName = giftRequest.GetAvailableRecipientNameLV();
                    actualRecipientCompanyName = giftRequest.GetAvailableRecipientCompanyLV();
                    Assert.IsTrue(actualRecipientCompanyName.Contains(nameCompanyExl));
                    Assert.IsTrue(actualRecipientContactName.Contains(nameContactExl));
                    extentReports.CreateStepLogs("Passed", "Company Name: " + actualRecipientCompanyName + " and Recipient Name: " + actualRecipientContactName + " is listed in Available Recipient(s) table for Contains combo box in Contact Name ");
                }

                //TMTC0011794/T1513- Client Gift Pre-Approval Page - Submit Gift Request - Cancel and Add

                giftRequest.ClearGiftRecipientsDetailsLV();
                comboSelectionExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SearchCriteria", 2, 2);
                
                nameCompanyExl= ReadExcelData.ReadDataMultipleRows(excelPath, "SearchCriteria", 3, 1);
                giftRequest.SearchWithCompnyNameComboBoxLV(comboSelectionExl, nameCompanyExl);// "StandardTestCompany");
                
                nameContactExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SearchCriteria", 6, 1);
                giftRequest.SearchWithContactNameComboBoxLV(comboSelectionExl, nameContactExl);// "test external");

                //Verify company name
                actualRecipientCompanyName = giftRequest.GetAvailableRecipientCompanyLV();
                //string expectedCompanyNameExl = ReadExcelData.ReadData(excelPath, "GiftLog", 8);
                Assert.AreEqual(nameCompanyExl, actualRecipientCompanyName);
                extentReports.CreateStepLogs("Passed", "Company Name: " + actualRecipientCompanyName + " is listed in Available Recipient(s) table ");

                //Verify recipient contact name
                actualRecipientContactName = giftRequest.GetAvailableRecipientNameLV();
                //string expectedContactNameExl = ReadExcelData.ReadData(excelPath, "GiftLog", 9);
                Assert.AreEqual(nameContactExl, actualRecipientContactName);
                extentReports.CreateStepLogs("Passed", "Recipient Name: " + actualRecipientContactName + " is listed in Available Recipient(s) table ");

                // Adding recipient from add recipient section to selected recipient section
                giftRequest.AddRecipientToSelectedRecipientsLV();

                //Verify recipient name
                string selectedRecipientName = giftRequest.GetSelectedRecipientNameLV();
                Assert.AreEqual(actualRecipientContactName, selectedRecipientName);
                extentReports.CreateStepLogs("Passed", "Recipient Name: " + selectedRecipientName + " in selected recipient(s) table matches with available recipient name listed in Available Recipient(s) table ");

                //Verify company name
                string selectedCompanyName = giftRequest.GetSelectedCompanyNameLV();
                Assert.AreEqual(actualRecipientCompanyName, selectedCompanyName);
                extentReports.CreateStepLogs("Passed", "Company Name: " + selectedCompanyName + " in selected recipient(s) table matches with available company name listed in Available Recipient(s) table ");

                // Verify removal of recipient from selected recipients
                giftRequest.RemoveRecipientFromSelectedRecipientsLV();
                Assert.IsFalse(giftRequest.IsSelectedRecipientDisplayedLV(),"Verify Record is removed from the Selected Recipient(s) section after Remove Recipient Action");
                extentReports.CreateStepLogs("Passed", "Recipient details are not displayed in Selected Recipient(s) section after Remove Recipient ");

                // Enter required details in client gift pre- approval page
                string valGiftNameEntered=giftRequest.EnterDetailsGiftRequestLV(fileTC1513);

                //Verify recipient name
                selectedRecipientName = giftRequest.GetAvailableRecipientNameLV();
                Assert.AreEqual(actualRecipientContactName, selectedRecipientName);
                extentReports.CreateStepLogs("Passed", "Recipient Name: " + selectedRecipientName + " in selected recipient(s) table matches with available recipient name listed in Available Recipient(s) table ");

                //Verify company name
                selectedCompanyName = giftRequest.GetAvailableRecipientCompanyLV();
                Assert.AreEqual(actualRecipientCompanyName, selectedCompanyName);
                extentReports.CreateStepLogs("Passed", "Company Name: " + selectedCompanyName + " in selected recipient(s) table matches with available company name listed in Available Recipient(s) table ");

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

                extentReports.CreateStepLogs("Info", "****Pending Issue on Cancel Button ISU0011899 ****");

                //****Pending Issue on Cancel Button ISU0011899 ****//
                //Click on cancel button

                /*giftRequest.ClickCancelButtonLV();
                giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of cancel button and gift request is not created ");

                // Enter required details in client gift pre- approval page
                valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileTC1513);

                //Verify recipient name
                selectedRecipientName = giftRequest.GetSelectedRecipientNameLV();
                Assert.AreEqual(actualRecipientContactName, selectedRecipientName);
                extentReports.CreateStepLogs("Passed", "Recipient Name: " + selectedRecipientName + " in selected recipient(s) table matches with available recipient name listed in Available Recipient(s) table ");

                //Verify company name
                selectedCompanyName = giftRequest.GetSelectedCompanyNameLV();
                Assert.AreEqual(actualRecipientCompanyName, selectedCompanyName);
                extentReports.CreateStepLogs("Passed", "Company Name: " + selectedCompanyName + " in selected recipient(s) table matches with available company name listed in Available Recipient(s) table ");

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

                */


                //Click on submit gift request
                giftRequest.ClickSubmitGiftRequestLV();
                giftApprove.ClickSubmitRequestLV();
                string actualCongratulationMsg = giftRequest.GetCongratulationsMsgLV();
                string congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                Assert.AreEqual(congratulationMsgExl, actualCongratulationMsg);
                extentReports.CreateStepLogs("Passed", "Congratulations message: " + actualCongratulationMsg + " in displayed upon successful submission of gift request ");

                //Verify Gift description 
                string giftDescriptionGiftRequestDetail = giftRequest.GetGiftDescriptionOnGiftRequestDetailLV();
                Assert.AreEqual(valGiftNameEntered, giftDescriptionGiftRequestDetail);
                extentReports.CreateStepLogs("Passed", "Gift Description: " + giftDescriptionGiftRequestDetail + " is listed on gift request submission detail page ");

                //Verify recipient name
                string RecipientOnGiftRequestDetail = giftRequest.GetRecipientNameOnGiftRequestDetailLV();
                string recipientName = ReadExcelData.ReadData(excelPath, "GiftLog", 9);
                Assert.AreEqual(recipientName, RecipientOnGiftRequestDetail);
                extentReports.CreateStepLogs("Passed", "Recipient Name: " + RecipientOnGiftRequestDetail + " is listed on gift request submission detail page ");

                //Click on return to pre-approval page button
                giftRequest.ClickReturnToPreApprovalPageLV();
                giftRequestTitle = giftRequest.GetGiftRequestPageTitleAfterReturnToPreApprovalPageLV();
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of return to pre approval page ");

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