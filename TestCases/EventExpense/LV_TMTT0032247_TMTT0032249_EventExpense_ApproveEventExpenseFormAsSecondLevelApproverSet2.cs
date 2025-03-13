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
    class LV_TMTT0032247_TMTT0032249_EventExpense_ApproveEventExpenseFormAsSecondLevelApproverSet2 : BaseClass
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

        public static string fileT2279 = "LV_T2279_TMTI0036293_ApproveEventExpenseFormAsSecondLevelApproverSet2";
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
        public void ApproveEventExpenseFormAsSecondLevelApproverLV2()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2279;
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
                    string ExpReqStatus = expRequestDetailPage.GetExpenseRequestStatusLWC();
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
                    string requestStatus = expRequestDetailPage.GetExpenseRequestStatusLWC();
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

                    string status = expRequestDetailPage.GetExpenseRequestStatusLWC();
                    Assert.AreEqual(requestStatus, status);
                    extentReports.CreateStepLogs("Passed", "Verified newly Created Expense Request::" + expensePreAppNumber + " is available in My Requests List with Status: " + status);
                    random.CloseActiveTab(expensePreAppNumber);
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Passed", "Requestor Logged out after creating Expense Requests:: " + expensePreAppNumber);

                    login.SwitchToClassicView();
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Browser Closed");

                    //Verify and Approve the Submitted Request by 1st level approver
                    OutLookInitialize();

                    //Login into Outlook
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
                    login.LoginAsFirstLevelExpenseRequest(fileT2279, row);
                    //login.LoginAsExpenseRequestApproverV(fileT2279, row);
                    extentReports.CreateStepLogs("Info", "Verified and Validation of User being redirected to Event Expense Form upon successful authentication ");

                    //Check if UI is Classic, Click back to List button, Switch to lV Search Pending Requests
                    string RequestUIStatus = expRequestHomePage.OpenPendingApprovalExpenseRequestLWC(expensePreAppNumber);
                    extentReports.CreateStepLogs("Info", RequestUIStatus);

                    // Validate status of the opened event request 
                    requestStatus = expRequestDetailPage.GetExpenseRequestStatusLWC(1);
                    Assert.AreEqual("Waiting for Approval", requestStatus);
                    extentReports.CreateStepLogs("Passed", "Expense request status is validated as " + requestStatus + " ");

                    //Approving the request
                    expRequestDetailPage.ClickApproveButtonLWC();

                    requestStatus = expRequestDetailPage.GetExpenseRequestStatusLWC();
                    Assert.AreEqual("Waiting for Approval", requestStatus);
                    extentReports.CreateStepLogs("Passed", "Expense Request Status is Approved");
                    random.CloseActiveTab(expensePreAppNumber);
                    extentReports.CreateStepLogs("Info", "Logout from salesforce as 1st level approver of Event Expense Requested accessed via Email received");

                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Browser Closed");

                    // Approving Request by 2nd level 
                    OutLookInitialize();
                    outlook.LoginOutlook(fileOutlook);
                    outlookLabel = outlook.GetLabelOfOutlook();
                    Assert.AreEqual("Outlook", outlookLabel);
                    extentReports.CreateStepLogs("Passed", "Verified and Validation is done for User is logged in to outlook ");

                    //Verify and opening the Request from Received Email for 2nd Approval
                    outlook.SelectSecondLevelExpenseApprovalEmail();

                    login.LoginAsExpenseRequestApproverV(fileT2279, row);
                    extentReports.CreateStepLogs("Info", "Verified and Validation of User being redirected to Event Expense Form upon successful authentication ");

                    //Check if UI is Classic, Click back to List button, Switch to lV Search Pending Requests
                    RequestUIStatus = expRequestHomePage.OpenPendingApprovalExpenseRequestLWC(expensePreAppNumber);
                    extentReports.CreateStepLogs("Info", RequestUIStatus);

                    // Validate status of the opened event request 
                    requestStatus = expRequestDetailPage.GetExpenseRequestStatusLWC(1);
                    Assert.AreEqual("Waiting for Approval", requestStatus);
                    extentReports.CreateStepLogs("Passed", "Expense request status is validated as " + requestStatus + " ");

                    //Approving the request
                    expRequestDetailPage.ClickApproveButtonLWC();

                    //Verify the status of Request
                    requestStatus = expRequestDetailPage.GetExpenseRequestStatusLWC();
                    Assert.AreEqual("Approved", requestStatus);
                    extentReports.CreateStepLogs("Passed", "Expense Request Status is Approved");
                    random.CloseActiveTab(expensePreAppNumber);

                    //Return back to expense request list
                    random.CloseActiveTab(expensePreAppNumber);
                    extentReports.CreateStepLogs("Info", "Logout from salesforce as 2nd level approver of Event Expense Requested accessed via Email received");
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Browser Closed");

                    //Verify the final Approval Email 
                    OutLookInitialize();
                    outlook.LoginOutlook(fileOutlook);
                    extentReports.CreateStepLogs("Info", "Login into Outlook as 2nd level approver to Verify the Expense Request Approval Email ");

                    //Verify approval email 
                    string expenseReqNumberFromApprovedEmail = outlook.VerifyExpenseRequestForApprovedEmail(0);
                    Assert.AreEqual(expensePreAppNumber, expenseReqNumberFromApprovedEmail);
                    extentReports.CreateStepLogs("Passed", "Approval email is recieved with expense RequestNumber " + expenseReqNumberFromApprovedEmail + " ");

                    //outlook.OutLookLogOut();
                    extentReports.CreateStepLogs("Info", "Logout from Outlook as 2nd level approver of event expense requested ");
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Browser Closed");

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