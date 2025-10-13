using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.EventExpense;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;


namespace SF_Automation.TestCases.EventExpense
{
    class LV_TMTT0032247_TMTT0032249_EventExpenseEmailNotificationRequestMoreInformationApproveAsFirstLevelApproverSet2:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        LVExpenseRequestCreatePage expRequest = new LVExpenseRequestCreatePage();
        LVExpenseRequestHomePage expRequestHomePage = new LVExpenseRequestHomePage();
        LVExpenseRequestDetailPage expRequestDetailPage = new LVExpenseRequestDetailPage();
        Outlook outlook = new Outlook();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages random = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileT2278 = "LV_T2278_EventExpenseEmailNotificationRequestMoreInformationApproveAsFirstLevelApproverSet2";
        public static string fileOutlook = "Outlook";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void RequestMoreInformationApproveAsFirstLevelApproverForEventExpenseRequestSet2()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2278;
                int rowUser = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row <= rowUser; row++)
                {
                    Initialize();
                    //Validating Title of Login Page
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                    extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                    // Calling Login function                
                    login.LoginApplication();
                    login.SwitchToClassicView();
                    // Validate user logged in                   
                    Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    homePage.SearchUserByGlobalSearchN(valUser);
                    extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
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
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Module Page ");

                    //CreateNewExpenseRequest with All required fields for submition 
                    string nameRequestor = valUser;
                    string valLOBExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", row, 1);
                    string eventTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", row, 2);
                    string nameEventContactExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", row, 4);
                    string nameProductTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", row, 5);
                    string nameEventExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", row, 6);
                    string nameCityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", row, 7);
                    string eventFormatExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", row, 8);
                    string noOfGuestsExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", row, 9);
                    string costETExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", row, 10);
                    string costOtherExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", row, 12);
                    string costEFBExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", row, 11);
                    string costDscotherExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", row, 13);
                    string nameHLOppExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", row, 14);
                    string nameTeamMemberExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", row, 15);

                    expRequest.SaveExpenseRequestRequiredFieldstoSubmitLWC(valLOBExl, eventTypeExl, nameRequestor, nameEventContactExl, nameProductTypeExl, nameEventExl, nameCityExl, eventFormatExl, noOfGuestsExl, costETExl, costEFBExl, costOtherExl, costDscotherExl, nameHLOppExl, nameTeamMemberExl);

                    //Validate Requestor value of expense request
                    string requestor = expRequestDetailPage.GetRequestorLWC();
                    Assert.AreEqual(nameRequestor, requestor);
                    extentReports.CreateStepLogs("Passed", "Requestor value is validated as " + requestor);

                    ////Validate status of expense request created
                    string ExpReqStatus = expRequestDetailPage.GetExpenseRequestStatusLWC(0);//1
                    Assert.AreEqual("Saved", ExpReqStatus);
                    extentReports.CreateStepLogs("Passed", "Expense request status is validated as " + ExpReqStatus);

                    ////Get expense pre approver number
                    string expensePreAppNumber = expRequestDetailPage.GetRequestNumberLWC();
                    extentReports.CreateStepLogs("Passed", "Expense Request created with number " + expensePreAppNumber);

                    ////Validate event contact
                    string eventContact = expRequestDetailPage.GetEventContactLWC();
                    Assert.AreEqual(nameEventContactExl, eventContact);
                    extentReports.CreateStepLogs("Passed", "Expense request event contact is validated as " + eventContact);

                    ////Validate product type from event expense detail page
                    string productType = expRequestDetailPage.GetProductTypeLWC();
                    Assert.AreEqual(nameProductTypeExl, productType);
                    extentReports.CreateStepLogs("Passed", "Expense request product type is validated as " + productType);

                    ////Validate event name from event expense detail page
                    string eventName = expRequestDetailPage.GetEventNameLWC();
                    Assert.IsTrue(eventName.Contains(nameEventExl));
                    extentReports.CreateStepLogs("Passed", "Expense request event name is validated as " + eventName);

                    ////Validate event city from event expense detail page
                    string eventCity = expRequestDetailPage.GetEventCityLWC();
                    Assert.AreEqual(nameCityExl, eventCity);
                    extentReports.CreateStepLogs("Passed", "Expense request event city is validated as " + eventCity);

                    ////Validate event LOB from event expense detail page
                    string eventLOB = expRequestDetailPage.GetLOBLWC();
                    Assert.AreEqual(valLOBExl, eventLOB);
                    extentReports.CreateStepLogs("Passed", "Expense request event LOB is validated as " + eventLOB);

                    ////Validate event type from event expense detail page
                    string eventType = expRequestDetailPage.GetEventTypeLWC();
                    Assert.AreEqual(eventTypeExl, eventType);
                    extentReports.CreateStepLogs("Passed", "Expense request event type is validated as " + eventType);

                    ////Validate event format from event expense detail page
                    string eventFormat = expRequestDetailPage.GetEventFormatLWC();
                    Assert.AreEqual(eventFormatExl, eventFormat);
                    extentReports.CreateStepLogs("Passed", "Expense request event format is validated as " + eventFormat);

                    //Click submit for approval button
                    expRequestDetailPage.ClickEventExpenseRequestButtonLWC("Submit for Approval (LWC)");
                    string requestStatus = expRequestDetailPage.GetExpenseRequestStatusLWC(0);//1
                    Assert.AreEqual("Waiting for Approval", requestStatus);
                    extentReports.CreateStepLogs("Passed", "Event Expense Request:: " + expensePreAppNumber + "  is submitted for approval and status is " + requestStatus);

                    // Verify the Total Budget Requested
                    decimal totalBudgetRequested = expRequestDetailPage.GetTotalBudgetRequestedLWC();
                    extentReports.CreateStepLogs("Info", "Request Budget Requested is " + totalBudgetRequested + " ");

                    // Click back to expense request list button
                    random.CloseActiveTab(expensePreAppNumber);
                    extentReports.CreateStepLogs("Info", "Submitted Expense Request Detail page is closed and user returned to Expense List page");


                    //Validate expense request submitted is displayed in list
                    expRequestHomePage.SelectRequestTabLWC("My Requests");
                    string headerExpNumber = expRequestHomePage.SearchAndSelectExpenseRequestLWC(expensePreAppNumber, "My Requests");
                    Assert.AreEqual(headerExpNumber, expensePreAppNumber);
                    extentReports.CreateStepLogs("Passed", "Expense Request:: " + headerExpNumber + " is found under My Requests List and selected");

                    string status = expRequestDetailPage.GetExpenseRequestStatusLWC(1);
                    Assert.AreEqual(requestStatus, status);
                    extentReports.CreateStepLogs("Passed", "Verified newly submitted form is available in My requests with Status: " + status);

                    homePageLV.UserLogoutFromSFLightningView();
                    login.SwitchToClassicView();
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Requestor Logout and close the browser ");

                    //Launch outlook window
                    OutLookInitialize();
                    outlook.LoginOutlook(fileOutlook);
                    string outlookLabel = outlook.GetLabelOfOutlook();
                    Assert.AreEqual("Outlook", outlookLabel);
                    extentReports.CreateStepLogs("Passed", "Verified and Validation is done for User is logged in to outlook ");

                    //Selecting Expense Request Approval email
                    outlook.SelectExpenseApprovalEmailV();
                    //Validating Title of Login Page
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                    extentReports.CreateStepLogs("Passed", "User is redirected to salesforce with " + driver.Title + " is displayed ");

                    //Switch to Salesforce from outlook
                    login.LoginAsExpenseRequestApproverV(fileT2278, row);
                    extentReports.CreateStepLogs("Info", "Verified and Validation of User being redirected to Event Expense Form upon successful authentication ");

                    //Check if UI is Classic, Click back to List button, Switch to lV Search Pending Requests
                    string RequestUIStatus = expRequestHomePage.OpenPendingApprovalExpenseRequestLWC(expensePreAppNumber);
                    extentReports.CreateStepLogs("Info", RequestUIStatus);

                    // Validate status of the event request on my request page in the request list
                    requestStatus = expRequestDetailPage.GetExpenseRequestStatusLWC(1);
                    Assert.AreEqual("Waiting for Approval", requestStatus);
                    extentReports.CreateStepLogs("Passed", "Expense request status is validated as " + requestStatus + " ");

                    //Verify request for more information button
                    bool requiestMoreInfoStatus = expRequestDetailPage.IsButtonDisplayedLWC("Request More Information");
                    Assert.IsTrue(requiestMoreInfoStatus, "Verify Request More Information button is Displayed on Request Details Page");
                    extentReports.CreateStepLogs("Passed", "Request More Information button is Displayed on Request Details Page");

                    //Request More Information
                    expRequestDetailPage.ClickRequestMoreInformationButtonLWC();
                    //string bubbleMessage = random.GetLVMessagePopup();
                    //string validationMessage = expRequest.GetValidationsLWC(bubbleMessage);
                    //Assert.AreEqual("Request Submitted Successfully", validationMessage, " Validate the Sucess Pop-up after More Information Requested Request");
                    //extentReports.CreateStepLogs("Passed", "Pop-up Message after More Information Requested is " + validationMessage);

                    status = expRequestDetailPage.GetExpenseRequestStatusLWC(1);
                    //Issue Page is not being Reloaded to reflect the latest Sstaus 
                    Assert.AreEqual("More Information Requested", status, "Verify the Status of Request after More Information Requested");
                    extentReports.CreateStepLogs("Passed", "More Information Requested expense request and expense request status validated as " + status + " ");

                    random.CloseActiveTab(expensePreAppNumber);
                    homePageLV.UserLogoutFromSFLightningView();
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Approver Logged out and close the browser");

                    extentReports.CreateStepLogs("Info", "Verify the Request For More Information email in outlook");
                    //Verify the More Information Requested email in outlook 
                    OutLookInitialize();
                    outlook.LoginOutlook(fileOutlook);
                    string outlookLabels = outlook.GetLabelOfOutlook();
                    Assert.AreEqual("Outlook", outlookLabels);
                    extentReports.CreateStepLogs("Passed", "Verified and Validation is done for User logged in to outlook");

                    //Verify More Information Requested email recieved on outlook
                    string expenseReqNumberFromEmail = outlook.VerifyExpenseRequestForRequestForMoreInfoEmail(0);
                    Assert.AreEqual(expensePreAppNumber, expenseReqNumberFromEmail);
                    extentReports.CreateStepLogs("Passed", "Request For More Information email is recieved with expense Request Number " + expenseReqNumberFromEmail + " ");
                    driver.Quit();

                    /**Resubmit More Information Requested expense request for approval ***/
                    Initialize();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                    extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                    login.LoginApplication();
                    login.SwitchToClassicView();
                    Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                    //Login as user who created the Expense Requst 
                    homePage.SearchUserByGlobalSearchN(valUser);
                    extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateStepLogs("Passed", "User: " + valUser + " logged in on Lightning View");

                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Module Page ");

                    //Validate expense request submitted is displayed in list
                    expRequestHomePage.SelectRequestTabLWC("My Requests");
                    headerExpNumber = expRequestHomePage.SearchAndSelectExpenseRequestLWC(expensePreAppNumber, "My Requests");

                    //Verify the status of Event Request
                    status = expRequestDetailPage.GetExpenseRequestStatusLWC(1);
                    Assert.AreEqual("More Information Requested", status);
                    extentReports.CreateStepLogs("Info", "Expense request status validated as " + status);

                    //Edit expense request
                    string editCityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", row, 17);
                    expRequestDetailPage.EditExpenseRequestCityLWC(editCityExl);
                    extentReports.CreateStepLogs("Info", "More Information Requested Expense Request updated with Differnt City ");

                    //Validate updated event city from event expense detail page
                    string latestEventCity = expRequestDetailPage.GetEventCityLWC();
                    Assert.AreEqual(editCityExl, latestEventCity);
                    extentReports.CreateStepLogs("Passed", "Expense request event city is validated as " + latestEventCity);

                    //Click on submit for approval button
                    expRequestDetailPage.ClickEventExpenseRequestButtonLWC("Submit for Approval (LWC)");
                    extentReports.CreateStepLogs("Info", "More Information Requested Expense Request is Edited and Resubmitted for approval");

                    //Validate expense request status after resubmitting for approval
                    requestStatus = expRequestDetailPage.GetExpenseRequestStatusLWC(1);
                    Assert.AreEqual("Waiting for Approval", requestStatus);
                    extentReports.CreateStepLogs("Passed", "Event Expense Request:: " + expensePreAppNumber + "  is submitted for approval and status is " + requestStatus);

                    random.CloseActiveTab(expensePreAppNumber);
                    extentReports.CreateStepLogs("Info", "Submitted Expense Request Detail page is closed and user returned to Expense List page");

                    homePageLV.UserLogoutFromSFLightningView();
                    login.SwitchToClassicView();
                    extentReports.CreateStepLogs("Info", "Event Expense Requestor logout after Resubmitting the event expense request which was More Information Requested ");
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Close Browser ");

                    ////*****Approving the request form first level approver****////
                    OutLookInitialize();
                    outlook.LoginOutlook(fileOutlook);
                    string outlookTitle = outlook.GetLabelOfOutlook();
                    Assert.AreEqual("Outlook", outlookTitle);
                    extentReports.CreateStepLogs("Passed", "Verified and Validation is done for User is logged in to outlook ");

                    //Select expense request approval email 
                    outlook.SelectExpenseApprovalEmailV();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                    extentReports.CreateStepLogs("Info", "User is redirected to salesforce with " + driver.Title + " is displayed ");

                    //Login as Expense Request approver
                    login.LoginAsExpenseRequestApproverV(fileT2278, row);
                    extentReports.CreateStepLogs("Info", "Login into outlook as first level approver of event expense requested ");

                    //Check if UI is Classic, Click back to List button, Switch to LV Search Pending Requests
                    RequestUIStatus = expRequestHomePage.OpenPendingApprovalExpenseRequestLWC(expensePreAppNumber);
                    extentReports.CreateStepLogs("Info", RequestUIStatus);

                    // Validate status of the event request on my request page in the request list
                    requestStatus = expRequestDetailPage.GetExpenseRequestStatusLWC(1);
                    Assert.AreEqual("Waiting for Approval", requestStatus);
                    extentReports.CreateStepLogs("Passed", "Event Expense Request:: " + expensePreAppNumber + "  is submitted for approval and status is " + requestStatus);

                    ////Approve expense request
                    expRequestDetailPage.ClickApproveButtonLWC();
                    requestStatus = expRequestDetailPage.GetExpenseRequestStatusLWC(1);
                    Assert.AreEqual("Approved", requestStatus);
                    extentReports.CreateStepLogs("Passed", "Expense Request Status is Approved");
                    if (((totalBudgetRequested >= 5000) || (totalBudgetRequested <= 10000)) && (productType.Equals("TO")) || productType.Equals("FO"))
                    {
                        extentReports.CreateStepLogs("Info", "Total Budget Requested: " + totalBudgetRequested + "and Product Type: " + productType + ", 1st Level and 2nd Level Approver are same Request and *No Action Required* from 2nd level Approval");
                    }

                    random.CloseActiveTab(expensePreAppNumber);
                    extentReports.CreateStepLogs("Info", "Approver Approved the Expense Request");

                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Info", "Event Expense Approver logout after Approving the Event Expense Request");
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Close Browser ");

                    //Launch outlook  and verify the Email of Aproved Request
                    OutLookInitialize();
                    outlook.LoginOutlook(fileOutlook);
                    extentReports.CreateStepLogs("Info", "Login into outlook to verfied approved email recieved ");

                    //Verify approval email 
                    string expenseReqNumberFromApprovedEmail = outlook.VerifyExpenseRequestForApprovedEmail(0);
                    Assert.AreEqual(expensePreAppNumber, expenseReqNumberFromApprovedEmail);
                    extentReports.CreateStepLogs("Passed", "Approval email is recieved with expense Request Number " + expenseReqNumberFromApprovedEmail + " ");

                    extentReports.CreateStepLogs("Info", "Logout from outlook as approver of event expense requested ");
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Close the browser ");
                }
            }
            catch (Exception ex)
            {
                extentReports.CreateExceptionLog(ex.Message);
                homePageLV.UserLogoutFromSFLightningView();
                driver.Quit();
            }
        }
    }
}