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

namespace SalesForce_Project.TestCases.GiftLog
{
    class LV_T2020_GiftApprovalProcessApproveGiftsVerifyColumnSortingOfTheGiftDetailsTableOnGiftsApprovedTab:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();
        GiftSubmittedPage giftsSubmit = new GiftSubmittedPage();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileT2020 = "LV_T2020_GiftApprovalProcessVerifyColumnSortingOfTheGiftDetailsTableOnGiftsApprovedTab";

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
        public void VerifyColumnSortingOfTheGiftDetailsTableOnGiftsApprovedTabLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2020;
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login ");

                valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
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
                                
                for (int i = 1; i <= 2; i++)
                {
                    CustomFunctions.PageReload(driver);
                    //Navigate to Gift Request page
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "CF Fin User is on " + moduleNameExl + " Page ");

                    //Navigate to Gift Request page                
                    giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                    giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                    Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                    extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");
                                        
                    //Enter required details in client gift pre- approval page
                    giftRequest.SetDesiredDateToCurrentDateLV();
                    valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT2020);

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
                    string selectedRecipientName = giftRequest.GetSelectedRecipientNameLV();
                    Assert.AreEqual(actualRecipientContactName, selectedRecipientName);
                    extentReports.CreateLog("Recipient Name: " + selectedRecipientName + " in selected recipient(s) table matches with available recipient name listed in Available Recipient(s) table ");

                    //Click on submit gift request
                    giftRequest.ClickSubmitGiftRequestLV();
                    giftApprove.ClickSubmitRequestLV();
                    congratulationMsg = giftRequest.GetCongratulationsMsgLV();
                    congratulationMsgExl = ReadExcelData.ReadData(excelPath, "GiftLog", 11);
                    Assert.AreEqual(congratulationMsgExl, congratulationMsg);
                    extentReports.CreateLog("Congratulations message: " + congratulationMsg + " in displayed upon successful submission of 1st gift request ");

                    //Navigate to Gift Request page
                    CustomFunctions.PageReload(driver);
                    //Navigate to Gift Request page
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "CF Fin User is on " + moduleNameExl + " Page ");

                    //Navigate to Gift Request page                
                    giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                    giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                    Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                    extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                    //Enter required details in client gift pre- approval page
                    giftRequest.SetDesiredDateToCurrentDateLV();
                    valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT2020);
                    extentReports.CreateLog("Gift details entered for a new gift request. ");

                    //Adding recipient from add recipient section to selected recipient section
                    giftRequest.AddRecipientToSelectedRecipientsLV();
                    extentReports.CreateLog("Recipient added. ");

                    //Click on submit gift request
                    giftRequest.ClickSubmitGiftRequestLV();
                    giftApprove.ClickSubmitRequestLV();
                    extentReports.CreateLog("Congratulations message: " + congratulationMsg + " in displayed upon successful submission of 2nd gift request. ");

                }

                driver.SwitchTo().DefaultContent();
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");


                //Search Compliance user by global search
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

                //Sorting By Gift Description column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASCLV("GIFT DESCRIPTION"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by Gift Description column successfully. ");

                //Sorting By Gift Description column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESCLV("GIFT DESCRIPTION"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by Gift Description column successfully. ");

                //Sorting By Recipient column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASCLV("RECIPIENT"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by Recipient column successfully. ");

                //Sorting By Recipient column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESC("RECIPIENT"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by Recipient column successfully. ");

                //Sorting By Recipient Company column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASCLV("RECIPIENT COMPANY"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by Recipient Company column successfully. ");

                //Sorting By Recipient Company column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESCLV("RECIPIENT COMPANY"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by Recipient Company column successfully. ");

                //Sorting By Submitted For column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASCLV("SUBMITTED FOR"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by Submitted For column successfully. ");

                //Sorting By Submitted For column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESCLV("SUBMITTED FOR"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by Submitted For column successfully. ");

                //Sorting By Submitted By column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASCLV("SUBMITTED BY"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by Submitted By column successfully. ");

                //Sorting By Submitted By column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESCLV("SUBMITTED BY"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by Submitted By column successfully. ");

                //Sorting By Prev YTD column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASCLV("PREV YTD"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by Prev YTD column successfully. ");

                //Sorting By Prev YTD column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESCLV("PREV YTD"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by Prev YTD column successfully. ");

                //Sorting By New YTD column in ASC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByASCLV("NEW YTD"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Ascending order by New YTD column successfully. ");

                //Sorting By New YTD column in DESC order and verifying the sort
                Assert.IsTrue(giftApprove.SortGiftDetailsTableColumnsByDESCLV("NEW YTD"));
                extentReports.CreateLog("Gift details table on approve gifts tab is sorted in Descending order by New YTD column successfully. ");


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