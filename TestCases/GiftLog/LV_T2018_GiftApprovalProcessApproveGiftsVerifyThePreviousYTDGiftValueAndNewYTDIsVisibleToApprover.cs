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
    class LV_T2018_GiftApprovalProcessApproveGiftsVerifyThePreviousYTDGiftValueAndNewYTDIsVisibleToApprover:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        GiftApprovePage giftApprove = new GiftApprovePage();
        GiftSubmittedPage giftsSubmit = new GiftSubmittedPage();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileT2018 = "LV_T2018_GiftApprovalProcessApproveGiftsVerifyThePreviousYTDGiftValueAndNewYTDIsVisibleToApprover";

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
        private string valUser;
        private string valRecipientLastNameExl;
        private string currentGift;
        private double enteredGiftValue;
        private double currentGiftValue;
        private double result;
        private string newGiftValue;
        private string prevYTD;
        private double prevYTDResult;
        private string giftVal;
        private double giftValResult;
        private string newYTD;
        private double newYTDResult;
        private string approvedPrevYTD;
        private double approvedPrevYTDResult;
        private string approvedGiftVal;
        private double approvedGiftValResult;
        private string approvedNewYTD;
        private double approvedNewYTDResult;
        private string giftValueNextYear;
        private double actualGiftValueNextYear;
        private string currentNextYearGift;
        private double currentNextYearGiftValue;
        private double actualnewGiftYTD;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyThePreviousYTDGiftValueAndNewYTDIsVisibleToApproverLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2018;
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login ");

                //valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                //homePage.SearchUserByGlobalSearchN(valUser);
                //extentReports.CreateStepLogs("Info", "CF Fin User: " + valUser + " details are displayed. ");
                //usersLogin.LoginAsSelectedUser();
                //login.SwitchToLightningExperience();
                //stdUser = login.ValidateUserLightningView();
                //Assert.AreEqual(stdUser.Contains(valUser), true);
                //extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged in on Lightning View");

                //appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                //homePageLV.SelectAppLV(appNameExl);
                //appName = homePageLV.GetAppName();
                //Assert.AreEqual(appNameExl, appName);
                //extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                //moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                //homePageLV.SelectModule(moduleNameExl);
                //extentReports.CreateStepLogs("Info", "CF Fin User is on " + moduleNameExl + " Page ");
                for (int i = 1; i < 3; i++)
                {
                    if (i == 1)
                    {
                        // Search standard user by global search
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

                        moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                        homePageLV.SelectModule(moduleNameExl);
                        extentReports.CreateStepLogs("Info", "CF Fin User is on " + moduleNameExl + " Page ");

                        giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                        giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                        Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                        extentReports.CreateStepLogs("Info","Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                        //Enter required details in client gift pre- approval page
                        valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT2018);
                        extentReports.CreateStepLogs("Info","Gift Reques Created with Name: " + valGiftNameEntered);

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

                        enteredGiftValue = double.Parse(ReadExcelData.ReadData(excelPath, "GiftLog", 3));
                        currentGift = giftRequest.GetCurrentGiftAmtYTDLV();
                        currentGiftValue = double.Parse(currentGift);

                        //Adding recipient from add recipient section to selected recipient section
                        giftRequest.AddRecipientToSelectedRecipientsLV();
                        result = Math.Round(currentGiftValue + enteredGiftValue, 1);
                        newGiftValue = giftRequest.GetGiftValueInGiftAmtYTDLV();
                        actualnewGiftYTD = double.Parse(newGiftValue);

                        //Click on submit gift request
                        giftRequest.ClickSubmitGiftRequestLV();
                        giftApprove.ClickSubmitRequestLV();
                        congratulationMsg = giftRequest.GetCongratulationsMsgLV();
                        extentReports.CreateStepLogs("Info","Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                        driver.SwitchTo().DefaultContent();
                        homePageLV.LogoutFromSFLightningAsApprover();
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

                        moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                        homePageLV.SelectModule(moduleNameExl);
                        extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");

                        Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                        extentReports.CreateStepLogs("Passed", "Approve Selected button is visible on click of approve gifts tab ");

                        //Search gift details by recipient last name for current year
                        valRecipientLastNameExl = ReadExcelData.ReadData(excelPath, "GiftLog", 13);
                        giftApprove.SearchByRecipientLastNameLV(valRecipientLastNameExl);
                        extentReports.CreateStepLogs("Info", "Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                        //Get Prev YTD Value                       
                        prevYTD = giftApprove.GetPrevYTDValueLV(valGiftNameEntered);
                        prevYTDResult = double.Parse(prevYTD);
                        extentReports.CreateStepLogs("Info", "Prev YTD Value : " + prevYTDResult + " is displayed for current year gift in pending gifts table. ");

                        //Match Value with current gift Value
                        giftVal = giftApprove.GetGiftValueFromPendingGiftsLV(valGiftNameEntered);
                        giftValResult = double.Parse(giftVal);
                        Assert.AreEqual(giftValResult, enteredGiftValue);
                        extentReports.CreateStepLogs("Passed", "Gift Value: " + giftValResult + " matches with current gift value in the pending gifts table. ");

                        //Get New YTD Value and verify the results
                        newYTD = giftApprove.GetNewYTDValueLV(valGiftNameEntered);
                        newYTDResult = double.Parse(newYTD);
                        result = prevYTDResult + giftValResult;
                        Assert.AreEqual(newYTDResult, result);
                        extentReports.CreateStepLogs("Passed", "New YTD: " + newYTDResult + " is the sum of Prev YTD: " + prevYTDResult + " and Gift Value: " + giftValResult + " in pending gifts table. ");

                        //Approve the current year gift
                        Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                        giftApprove.SetApprovalDenialCommentsLV();
                        giftApprove.ClickApproveSelectedButtonLV();
                        extentReports.CreateStepLogs("Info", "Gift is approved successfully ");

                        //Click on approve gifts tab
                        CustomFunctions.PageReload(driver);
                        //Navigate to Gift Request page
                        //moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                        homePageLV.SelectModule(moduleNameExl);
                        extentReports.CreateStepLogs("Info", "CF Fin User is on " + moduleNameExl + " Page ");
                        Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                        extentReports.CreateStepLogs("Passed", "Approve Selected button is visible on click of approve gifts tab ");

                        //Search gift in approved list
                        giftApprove.SearchByRecipientLastNameAndStatusLV(valRecipientLastNameExl,"Approved");

                        //Get Prev YTD Value                        
                        approvedPrevYTD = giftApprove.GetApprovedPrevYTDValueLV(valGiftNameEntered);
                        approvedPrevYTDResult = double.Parse(approvedPrevYTD);
                        extentReports.CreateStepLogs("Info", "Prev YTD Value : " + approvedPrevYTDResult + " is displayed for current year approved gift in approved gifts table. ");

                        //Match Value with current gift Value
                        approvedGiftVal = giftApprove.GetGiftValueFromApprovedGiftsLV(valGiftNameEntered);
                        approvedGiftValResult = double.Parse(approvedGiftVal);
                        Assert.AreEqual(approvedGiftValResult, enteredGiftValue);
                        extentReports.CreateStepLogs("Passed", "Gift Value: " + approvedGiftValResult + " matches with current gift value in approved gifts table. ");

                        //Get New YTD Value and verify the results
                        approvedNewYTD = giftApprove.GetApprovedNewYTDValueLV(valGiftNameEntered);
                        approvedNewYTDResult = double.Parse(approvedNewYTD);
                        Assert.AreEqual(approvedPrevYTDResult, approvedNewYTDResult);
                        extentReports.CreateStepLogs("Passed", "New YTD Value : " + approvedNewYTDResult + " is equal to Prev YTD Value: " + approvedPrevYTDResult + " in approved gifts table. ");

                        driver.SwitchTo().DefaultContent();
                        homePageLV.LogoutFromSFLightningAsApprover();
                        extentReports.CreateStepLogs("Passed", "Compliance User: " + valUser + " logged out");

                    }
                    else if (i == 2)
                    {
                        // Search standard user by global search
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

                        moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                        homePageLV.SelectModule(moduleNameExl);
                        extentReports.CreateStepLogs("Info", "CF Fin User is on " + moduleNameExl + " Page ");

                        giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                        giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                        Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                        extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                        //Enter desire date as next year
                        giftRequest.EnterDesiredDateLV(360);
                        //Enter required details in client gift pre- approval page
                        valGiftNameEntered = giftRequest.EnterDetailsGiftRequestLV(fileT2018);
                        extentReports.CreateStepLogs("Info", "Gift Request Created with Name: " + valGiftNameEntered);

                        enteredGiftValue = double.Parse(ReadExcelData.ReadData(excelPath, "GiftLog", 3));
                        currentNextYearGift = giftRequest.GetCurrentNextYearGiftAmtLV();
                        currentNextYearGiftValue = double.Parse(currentNextYearGift);

                        //Adding recipient from add recipient section to selected recipient section
                        giftRequest.AddRecipientToSelectedRecipientsLV();
                        result = Math.Round(currentNextYearGiftValue + enteredGiftValue, 1);
                        giftValueNextYear = giftRequest.GetGiftValueInGiftTotalNextYearLV();
                        actualGiftValueNextYear = double.Parse(giftValueNextYear);

                        //Click on submit gift request
                        giftRequest.ClickSubmitGiftRequestLV();
                        giftApprove.ClickSubmitRequestLV();
                        congratulationMsg = giftRequest.GetCongratulationsMsgLV();
                        extentReports.CreateStepLogs("Info", "Congratulations message: " + congratulationMsg + " in displayed upon successful submission of gift request ");

                        driver.SwitchTo().DefaultContent();
                        homePageLV.LogoutFromSFLightningAsApprover();
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

                        moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                        homePageLV.SelectModule(moduleNameExl);
                        extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");

                        Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                        extentReports.CreateStepLogs("Passed", "Approve Selected button is visible on click of Approve Gifts Module ");

                        //Search gift details by recipient last name for current year
                        valRecipientLastNameExl = ReadExcelData.ReadData(excelPath, "GiftLog", 13);
                        giftApprove.SearchByRecipientLastNameForNextYearLV(valRecipientLastNameExl);
                        extentReports.CreateStepLogs("Info", "Approved Column is displayed with 'Pending' Status as default and upon search gifts list is displayed ");

                        //Get Prev YTD Value
                        prevYTD = giftApprove.GetPrevYTDValueLV(valGiftNameEntered);
                        prevYTDResult = double.Parse(prevYTD);
                        extentReports.CreateStepLogs("Info", "Prev YTD Value : " + prevYTDResult + " is displayed for next year gift in pending gifts table. ");

                        //Match Value with current gift Value
                        giftVal = giftApprove.GetGiftValueFromPendingGiftsLV(valGiftNameEntered);
                        giftValResult = double.Parse(giftVal);
                        Assert.AreEqual(giftValResult, enteredGiftValue);
                        extentReports.CreateStepLogs("Passed", "Gift Value: " + giftValResult + " matches with current gift value in pending gifts table. ");

                        //Get New YTD Value and verify the results
                        newYTD = giftApprove.GetNewYTDValueLV(valGiftNameEntered);
                        newYTDResult = double.Parse(newYTD);
                        result = prevYTDResult + giftValResult;
                        Assert.AreEqual(newYTDResult, result);
                        extentReports.CreateStepLogs("Passed", "New YTD: " + newYTDResult + " is the sum of Prev YTD: " + prevYTDResult + " and Gift Value: " + giftValResult + " in pending gifts table. ");

                        //Approve the gift
                        Assert.IsTrue(giftApprove.CompareGiftDescWithGiftNameLV(valGiftNameEntered));
                        giftApprove.SetApprovalDenialCommentsLV();
                        giftApprove.ClickApproveSelectedButtonLV();
                        extentReports.CreateStepLogs("Info", "Gift is approved successfully ");

                        //Click on approve gifts tab
                        CustomFunctions.PageReload(driver);
                        //moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                        homePageLV.SelectModule(moduleNameExl);
                        extentReports.CreateStepLogs("Info", "Compliance User is on " + moduleNameExl + " Page ");

                        Assert.IsTrue(giftApprove.ApproveSelectedButtonVisibilityLV());
                        extentReports.CreateStepLogs("Passed", "Approve Selected button is visible on click of Approve Gifts Module ");

                        //Search gift in approved list                        
                        giftApprove.SearchByRecipientLastNameAndStatusForNextYearLV(valRecipientLastNameExl, "Approved");

                        //Get Prev YTD Value
                        approvedPrevYTD = giftApprove.GetApprovedPrevYTDValueLV(valGiftNameEntered);
                        approvedPrevYTDResult = double.Parse(approvedPrevYTD);
                        extentReports.CreateStepLogs("Info", "Prev YTD Value : " + approvedPrevYTDResult + " is displayed for approved gift in approved gifts table. ");

                        //Match Value with current gift Value
                        approvedGiftVal = giftApprove.GetGiftValueFromApprovedGiftsLV(valGiftNameEntered);
                        approvedGiftValResult = double.Parse(approvedGiftVal);
                        Assert.AreEqual(approvedGiftValResult, enteredGiftValue);
                        extentReports.CreateStepLogs("Passed", "Gift Value: " + approvedGiftValResult + " matches with current gift value in approved gifts table. ");

                        //Get New YTD Value and verify the results
                        approvedNewYTD = giftApprove.GetApprovedNewYTDValueLV(valGiftNameEntered);
                        approvedNewYTDResult = double.Parse(approvedNewYTD);
                        Assert.AreEqual(approvedPrevYTDResult, approvedNewYTDResult);
                        extentReports.CreateStepLogs("Passed", "New YTD Value : " + approvedNewYTDResult + " is equal to Prev YTD Value: " + approvedPrevYTDResult + " in approved gifts table. ");

                        driver.SwitchTo().DefaultContent();
                        homePageLV.LogoutFromSFLightningAsApprover();
                        extentReports.CreateStepLogs("Passed", "Compliance User: " + valUser + " logged out");
                    }
                }
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");

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