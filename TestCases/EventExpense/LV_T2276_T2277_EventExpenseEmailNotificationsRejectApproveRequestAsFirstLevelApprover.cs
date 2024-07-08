using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.EventExpense;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.EventExpense
{
    class LV_T2276_T2277_EventExpenseEmailNotificationsRejectApproveRequestAsFirstLevelApprover : BaseClass
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

        public static string fileT2276 = "LV_T2276_T2277_EventExpenseEmailNotificationsRejectApproveRequestAsFirstLevelApprover";
        public static string fileOutlook = "Outlook";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {            
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void RejectAndApproveEventExpenseFormAsFirstLevelApproverLV()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2276;
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

                    //CreateNewExpenseRequest with All required field for submition 
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
                    //string requestor = ReadExcelData.ReadData(excelPath, "ExpenseRequest", 3);
                    string requestor=expRequestDetailPage.GetRequestorLWC();
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

                    random.CloseActiveTab(expensePreAppNumber);
                    extentReports.CreateStepLogs("Passed", "Submitted Expense Request Detail page is closed and user returned to Expense List page");

                    //Validate expense request submitted is displayed in list
                    expRequestHomePage.SelectRequestTabLWC("My Requests");
                    string headerExpNumber = expRequestHomePage.SearchAndSelectExpenseRequestLWC(expensePreAppNumber, "My Requests");
                    Assert.AreEqual(headerExpNumber, expensePreAppNumber);
                    extentReports.CreateStepLogs("Passed", "Expense Request:: " + headerExpNumber + " is found and selected");

                    string status = expRequestDetailPage.GetExpenseRequestStatusLWC();
                    Assert.AreEqual(requestStatus, status);
                    extentReports.CreateStepLogs("Passed", "Verified newly submitted form is available in My requests with Status: " + status);

                    homePageLV.UserLogoutFromSFLightningView();
                    login.SwitchToClassicView();
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Requestor Logout and close the browser ");

                    //Launch outlook window
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
                    login.LoginAsExpenseRequestApprover(fileT2276);
                    extentReports.CreateStepLogs("Passed", "Verified and Validation of User being redirected to Event Expense Form upon successful authentication ");

                    //Check if UI is Classic, Click back to List button, Switch to lV Search Pending Requests
                    string RequestUIStatus = expRequestHomePage.OpenPendingApprovalExpenseRequestLWC(expensePreAppNumber);
                    extentReports.CreateStepLogs("Info", RequestUIStatus);

                    // Validate status of the event request on my request page in the request list
                    requestStatus = expRequestDetailPage.GetExpenseRequestStatusLWC(1);
                    Assert.AreEqual("Waiting for Approval", requestStatus);
                    extentReports.CreateStepLogs("Passed", "Expense request status is validated as " + requestStatus + " ");
                                         
                    //Verify Approve button 
                    bool approveBtnStatus = expRequestDetailPage.IsButtonDisplayedLWC("Approve(LWC)");
                    Assert.IsTrue(approveBtnStatus,"Verify Approve(LWC) button is Displayed on Request Details Page");
                    extentReports.CreateStepLogs("Passed", "Approve button is Displayed on expense request detail page ");

                    //Verify Reject button 
                    bool rejectBtnStatus = expRequestDetailPage.IsButtonDisplayedLWC("Reject(LWC)");
                    Assert.IsTrue(rejectBtnStatus, "Verify Reject(LWC) button is Displayed on Request Details Page");
                    extentReports.CreateStepLogs("Passed", "Reject button is Displayed on expense request detail page ");

                    //Verify request for more information button
                    bool requiestMoreInfoStatus = expRequestDetailPage.IsButtonDisplayedLWC("Request More Information");
                    Assert.IsTrue(rejectBtnStatus, "Verify Request More Information button is Displayed on Request Details Page");
                    extentReports.CreateStepLogs("Passed", "Request More Information button is Displayed on Request Details Page");

                    //Reject expense request
                    expRequestDetailPage.ClickRejectButtonLWC();
                    status = expRequestDetailPage.GetExpenseRequestStatusLWC();
                    Assert.AreEqual("Rejected", status,"Verify the Status of Request after Rejection");
                    extentReports.CreateLog("Rejected expense request and expense request status validated as " + status + " ");

                    random.CloseActiveTab(expensePreAppNumber);
                    homePageLV.UserLogoutFromSFLightningView();
                    login.SwitchToClassicView();
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Request 1st level Approver Logged out and close the browser ");




                    /////////-----------------------------------

                    //Require More Information
                    expRequestDetailPage.ClickRequestMoreInformationButtonLWC();
                    status = expRequestDetailPage.GetExpenseRequestStatusLWC();
                    Assert.AreEqual("More Information Requested", status, "Verify the Status of Request after Request for More Information");
                    extentReports.CreateLog("Rejected expense request and expense request status validated as " + status + " ");

                    //Validate expense request response under approval history section


                    homePageLV.UserLogoutFromSFLightningView();
                    driver.Quit();
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
