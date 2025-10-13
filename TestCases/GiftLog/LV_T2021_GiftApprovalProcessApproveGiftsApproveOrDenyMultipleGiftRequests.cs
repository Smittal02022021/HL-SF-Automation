using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.GiftLog;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.GiftLog
{
    class LV_T2021_GiftApprovalProcessApproveGiftsApproveOrDenyMultipleGiftRequests:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        public static string fileT2021 = "LV_T2021_GiftApprovalProcessApproveGiftsApproveOrDenyMultipleGiftRequests";
        private string userComplianceExl;
        private string giftRequestTitle;
        private string stdUser;
        private string giftRequestTitleExl;
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
        public void VerifyT2021_GiftApprovalProcessApproveGiftsApproveOrDenyMultipleGiftRequestsLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2021;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login ");
                                
                for (int i = 1; i <= 2; i++)
                {
                    //Search standard user by global search
                    valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                    homePage.SearchUserByGlobalSearchN(valUser);
                    extentReports.CreateStepLogs("Info", "CF Fin User: " + valUser + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    stdUser = login.ValidateUserLightningView();
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
                    giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                    Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                    extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                    //Enter required details in client gift pre- approval page
                    giftRequest.SetDesiredDateToCurrentDateLV();
                    string valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT2021);

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

                    //Adding recipient from add recipient section to selected recipient section
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
                    extentReports.CreateStepLogs("Passed", "Congratulations message: " + congratulationMsg + " in displayed upon successful submission of 1st gift request ");

                    //Navigate to Gift Request page
                    randomPages.ReloadPage();
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                    Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                    extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request module ");

                    //Enter required details in client gift pre- approval page
                    giftRequest.SetDesiredDateToCurrentDateLV();
                    string valGiftNameEntered1 = giftRequest.EnterDetailsGiftRequestLV(fileT2021);
                    extentReports.CreateStepLogs("Info", "Gift details entered for a new gift request. ");

                    //Adding recipient from add recipient section to selected recipient section
                    giftRequest.AddRecipientToSelectedRecipientsLV();
                    extentReports.CreateStepLogs("Info", "Recipient added. ");

                    //Click on submit gift request
                    giftRequest.ClickSubmitGiftRequestLV();
                    giftApprove.ClickSubmitRequestLV();
                    extentReports.CreateStepLogs("Info", "Congratulations message: " + congratulationMsg + " in displayed upon successful submission of 2nd gift request. ");

                    driver.SwitchTo().DefaultContent();
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");

                    //Search Compliance user by global search
                    //Login as Compliance User
                    userComplianceExl = ReadExcelData.ReadData(excelPath, "Users", 2);
                    homePage.SearchUserByGlobalSearchN(userComplianceExl);
                    extentReports.CreateStepLogs("Info", "Compliance User: " + userComplianceExl + " details are displayed.");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string user = login.ValidateUserLightningView();
                    Assert.AreEqual(user.Contains(userComplianceExl), true);
                    extentReports.CreateStepLogs("Passed", "Compliance User: " + userComplianceExl + " logged in on Lightning View");

                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");

                    Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                    extentReports.CreateStepLogs("Passed", "Approve Selected button is visible on click of approve gifts tab ");

                    //Search gift details by recipient last name
                    string valRecipientLastNameExl = ReadExcelData.ReadData(excelPath, "GiftLog", 13);
                    giftApprove.SearchByRecipientLastNameLV(valRecipientLastNameExl);
                    extentReports.CreateStepLogs("Info", "Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                    //Click on approve selected button and approve multiple gifts at once
                    Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                    Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered1));
                    giftApprove.SetApprovalDenialCommentsLV();

                    if (i == 1)
                    {
                        giftApprove.ClickApproveSelectedButtonLV();
                        extentReports.CreateStepLogs("Info", "Multiple gifts are approved at once by clicking Approve selected button. ");

                        //Search the approved gifts under approved status
                        valRecipientLastNameExl = ReadExcelData.ReadData(excelPath, "GiftLog", 13);
                        giftApprove.SearchByRecipientLastNameAndStatusLV(valRecipientLastNameExl, "Approved");
                        string txtStatus = giftApprove.GetStatusCompareGiftDescWithGiftNameLV(valGiftNameEntered);
                        string txtStatus1 = giftApprove.GetStatusCompareGiftDescWithGiftNameLV(valGiftNameEntered1);

                        //Verification of gift status displaying in Approved list
                        Assert.AreEqual("Approved", txtStatus);
                        extentReports.CreateStepLogs("Passed", txtStatus + " is displaying in gift status for 1st gift. ");
                        Assert.AreEqual("Approved", txtStatus1);
                        extentReports.CreateStepLogs("Passed", txtStatus1 + " is displaying in gift status for 2nd gift. ");

                        //Click on Deny selected button and deny multiple gifts from approved list at once
                        Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                        Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered1));

                        giftApprove.ClickDenySelectedButtonLV();
                        extentReports.CreateStepLogs("Passed", "Multiple gifts are denied at once by clicking Deny selected button from approved gifts list. ");

                        //Search the Denied gifts under Denied status
                        giftApprove.SearchByRecipientLastNameAndStatusLV(valRecipientLastNameExl, "Denied");
                        string txtStatus2 = giftApprove.GetStatusCompareGiftDescWithGiftNameLV(valGiftNameEntered);
                        string txtStatus3 = giftApprove.GetStatusCompareGiftDescWithGiftNameLV(valGiftNameEntered1);

                        //Verification of gift status displaying in Denied list
                        Assert.AreEqual("Denied", txtStatus2);
                        extentReports.CreateStepLogs("Passed", txtStatus2 + " is displaying in gift status for 1st gift. ");
                        Assert.AreEqual("Denied", txtStatus3);
                        extentReports.CreateStepLogs("Passed", txtStatus3 + " is displaying in gift status for 2nd gift. ");

                        driver.SwitchTo().DefaultContent();
                        homePageLV.LogoutFromSFLightningAsApprover();
                        extentReports.CreateStepLogs("Info", "Compliance User: " + userComplianceExl + " logged out");
                    }
                    else
                    {
                        giftApprove.ClickDenySelectedButtonLV();
                        extentReports.CreateLog("Multiple gifts are denied at once by clicking Deny selected button. ");

                        //Search the approved gifts under approved status
                        giftApprove.SearchByRecipientLastNameAndStatusLV(valRecipientLastNameExl, "Denied");
                        string txtStatus4 = giftApprove.GetStatusCompareGiftDescWithGiftNameLV(valGiftNameEntered);
                        string txtStatus5 = giftApprove.GetStatusCompareGiftDescWithGiftNameLV(valGiftNameEntered1);

                        //Verification of gift status displaying in Approved list
                        Assert.AreEqual("Denied", txtStatus4);
                        extentReports.CreateStepLogs("Passed", txtStatus4 + " is displaying in gift status for 1st gift. ");
                        Assert.AreEqual("Denied", txtStatus5);
                        extentReports.CreateStepLogs("Passed", txtStatus5 + " is displaying in gift status for 2nd gift. ");

                        //Click on Approve selected button and approve multiple gifts from approved list at once
                        Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                        Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered1));
                        giftApprove.ClickApproveSelectedButton();
                        extentReports.CreateStepLogs("Passed", "Multiple gifts are approved at once by clicking Approve selected button from denied gifts list. ");

                        //Search the Approved gifts under Denied status
                        giftApprove.SearchByRecipientLastNameAndStatusLV(valRecipientLastNameExl, "Approved");
                        string txtStatus6 = giftApprove.GetStatusCompareGiftDescWithGiftNameLV(valGiftNameEntered);
                        string txtStatus7 = giftApprove.GetStatusCompareGiftDescWithGiftNameLV(valGiftNameEntered1);

                        //Verification of gift status displaying in Approved list
                        Assert.AreEqual("Approved", txtStatus6);
                        extentReports.CreateStepLogs("Passed", txtStatus6 + " is displaying in gift status for 1st gift. ");
                        Assert.AreEqual("Approved", txtStatus7);
                        extentReports.CreateStepLogs("Passed", txtStatus7 + " is displaying in gift status for 2nd gift. ");

                        driver.SwitchTo().DefaultContent();
                        homePageLV.LogoutFromSFLightningAsApprover();
                        extentReports.CreateStepLogs("Info", "Compliance User: " + userComplianceExl + " logged out");
                    }
                }
                driver.Quit();
                extentReports.CreateStepLogs("Passed", "Browser Closed Successfully!");
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