using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Company;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.EventExpense;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Threading;

namespace SF_Automation.TestCases.EventExpense
{
    class T2279_TMTI0036293_91_92_95_97_98_EventExpense_ApproveEventExpenseFormAsSecondLevelApprover : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        CoverageTeamDetail coverageTeamDetail = new CoverageTeamDetail();
        ExpenseRequestCreatePage expReqCreate = new ExpenseRequestCreatePage();
        ExpenseRequestDetailPage expReqDetail = new ExpenseRequestDetailPage();
        ExpenseRequestHomePage expRequest = new ExpenseRequestHomePage();
        UsersLogin usersLogin = new UsersLogin();
        Outlook outlook = new Outlook();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC2279 = "T2279_TMTI0036293_91_92_93_95_97_98_ApproveEventExpenseFormAsSecondLevelApprover";
        public static string fileOutlook = "Outlook";

        private string requestStatus;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ApproveEventExpenseFormAsSecondLevelApprover()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2279;
                Console.WriteLine(excelPath);

                int rowExpenseRequest = ReadExcelData.GetRowCount(excelPath, "ExpenseRequest");
                for (int row = 2; row <= rowExpenseRequest; row++)
                {
                    Initialize();
                    //Validating Title of Login Page
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Calling Login function                
                    login.LoginApplication();

                    //Handling salesforce Lightning
                    login.HandleSalesforceLightningPage();

                    //Validate user logged in       
                    Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                    // Search standard user by global search
                    string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users",row, 1);
                    homePage.SearchUserByGlobalSearch(fileTC2279, user);

                    //Verify searched user
                    string userPeople = homePage.GetPeopleOrUserName();
                    string userPeopleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    Assert.AreEqual(userPeopleExl, userPeople);
                    extentReports.CreateLog("User " + userPeople + " details are displayed ");

                    //Login as standard user
                    usersLogin.LoginAsSelectedUser();
                    string standardUser = login.ValidateUser();
                    string standardUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users",row, 1);
                    Assert.AreEqual(standardUserExl.Contains(standardUser), true);
                    extentReports.CreateLog("Standard User: " + standardUser + " is able to login ");

                    // Create Expense Request
                    expReqCreate.CreateExpenseRequest(row, fileTC2279);
                    extentReports.CreateLog("Expense request is created successfully ");

                    //Validate Requestor value of expense request
                    string requestor = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest",row, 3);
                    Assert.AreEqual(requestor, expReqDetail.GetRequestor());
                    extentReports.CreateLog("Requestor value is validated as " + expReqDetail.GetRequestor() + " ");

                    //Validate status of expense request created
                    Assert.AreEqual("Saved", expReqDetail.GetExpenseRequestStatus(0));
                    extentReports.CreateLog("Expense request status is validated as " + expReqDetail.GetExpenseRequestStatus(0) + " ");

                    //Get expense pre approver number
                    string expensePreAppNumber = expReqDetail.GetExpensePreApproverNumberFromDetail();

                    //Validate event contact
                    string eventContact = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest",row, 4);
                    Assert.AreEqual(eventContact, expReqDetail.GetEventContact());
                    extentReports.CreateLog("Expense request event contact is validated as " + expReqDetail.GetEventContact() + " ");

                    //Validate product type from event expense detail page
                    string productType = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 5);
                    Assert.AreEqual(productType, expReqDetail.GetProductType());
                    extentReports.CreateLog("Expense request product type is validated as " + expReqDetail.GetProductType() + " ");

                    //Validate event name from event expense detail page
                    string eventName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 6);
                    Assert.AreEqual(eventName, expReqDetail.GetEventName());
                    extentReports.CreateLog("Expense request event name is validated as " + expReqDetail.GetEventName() + " ");

                    //Validate event city from event expense detail page
                    string eventCity = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 7);
                    Assert.AreEqual(eventCity, expReqDetail.GetEventCity());
                    extentReports.CreateLog("Expense request event city is validated as " + expReqDetail.GetEventCity() + " ");

                    //Validate event LOB from event expense detail page
                    string eventLOB = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 1);
                    Assert.AreEqual(eventLOB, expReqDetail.GetLOB());
                    extentReports.CreateLog("Expense request event LOB is validated as " + expReqDetail.GetLOB() + " ");

                    //Validate event type from event expense detail page
                    string eventType = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 2);
                    Assert.AreEqual(eventType, expReqDetail.GetEventType());
                    extentReports.CreateLog("Expense request event type is validated as " + expReqDetail.GetEventType() + " ");

                    //Validate event format from event expense detail page
                    string eventFormat = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 8);
                    Assert.AreEqual(eventFormat, expReqDetail.GetEventFormat());
                    extentReports.CreateLog("Expense request event format is validated as " + expReqDetail.GetEventFormat() + " ");

                    //Click submit for approval button
                    expReqDetail.ClickSubmitForApproval();
                    extentReports.CreateLog("Submit for approval button is clicked ");

                    //Verify the status of Request
                    requestStatus = expReqDetail.getRequestStatus();
                    extentReports.CreateLog("Expense request Latest Status is " + requestStatus + " ");
                    Assert.AreEqual("Waiting for Approval", requestStatus);

                    // Verify the Total Budget Requested
                    decimal totalBudgetRequested = expReqDetail.getTotalBudgetRequested();
                    extentReports.CreateLog("Budget Requested is " + totalBudgetRequested + " ");

                    // Click back to expense request list button
                    expReqDetail.ClickBackToExpenseRequestList(0);
                    extentReports.CreateLog("Click Back to expense request list button ");

                    //Validate expense request submitted is displayed in list
                    string recordStatus = expRequest.CompareNewExpenseRequestInMyRequest(expensePreAppNumber);
                    Assert.AreEqual("Event Expense Pre Approver number matches in My Requests", recordStatus);
                    extentReports.CreateLog("Verified newly submitted form is available in My requests Tab is successful");
                    usersLogin.UserLogOut();
                    usersLogin.UserLogOut();
                    driver.Quit();
                    extentReports.CreateLog("Logout and close the browser ");

                    //Launch outlook window
                    OutLookInitialize();

                    //Login into Outlook
                    outlook.LoginOutlook(fileOutlook);
                    string outlookLabel = outlook.GetLabelOfOutlook();
                    Assert.AreEqual("Outlook", outlookLabel);
                    extentReports.CreateLog("Verified and Validation is done for User is logged in to outlook ");

                    //Selecting Expense Request Approval email
                    outlook.SelectExpenseApprovalEmailV();
                    //Validating Title of Login Page
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                    extentReports.CreateLog("User is redirected to salesforce with " + driver.Title + " is displayed ");

                    //Switch to Salesforce from outlook
                    login.LoginAsFirstLevelExpenseRequest(fileTC2279, row);
                    extentReports.CreateLog("Verified and Validation of User being redirected to Event Expense Form upon successful authentication ");

                    // Validate status of the event request on my request page in the request list
                    requestStatus = expReqDetail.GetExpenseRequestStatus(1);
                    Assert.AreEqual("Waiting for Approval", requestStatus);
                    
                    //Approve expense request
                    expReqDetail.ApproveExpenseRequest(1);
                    extentReports.CreateLog("Approved expense request ");

                    //Verify the status of Request
                    requestStatus = expReqDetail.getRequestStatus();
                    extentReports.CreateLog("Expense request Latest Status is " + requestStatus + " ");
                    Assert.AreEqual("Waiting for Approval", requestStatus);
                    extentReports.CreateLog("Budget Requested is " + totalBudgetRequested + " Expense request status is validated as " + requestStatus + " 2nd Level Arroval is also required");

                    expReqDetail.ClickBackToExpenseRequestList(1);
                    extentReports.CreateLog("Click Back to expense request list button ");
                    usersLogin.UserLogOut();

                    extentReports.CreateLog("Logout from salesforce as first level approver of event expense requested ");
                    //outlook.OutLookLogOut();
                    extentReports.CreateLog("Logout from outlook as first level approver of event expense requested ");

                    driver.Quit();
                    extentReports.CreateLog("Logout and close the browser ");

                    OutLookInitialize();
                    outlook.LoginOutlook(fileOutlook);
                    extentReports.CreateLog("Verified and Validation is done for User being redirected to salesforce login ");
                    outlook.SelectSecondLevelExpenseApprovalEmail();

                    login.LoginAsExpenseRequestApprover(fileTC2279, row);
                    extentReports.CreateLog("Verified and Validation of User being redirected to Event Expense Form upon successful authentication ");
                    //Approve expense request
                    expReqDetail.ApproveExpenseRequest(1);
                    extentReports.CreateLog("Approved expense request ");

                    //Verify the status of Request
                    requestStatus = expReqDetail.getRequestStatus();
                    extentReports.CreateLog("Expense request Latest Status is " + requestStatus + " ");
                    Assert.AreEqual("Approved", requestStatus);

                    //Return back to expense request list
                    expReqDetail.ClickBackToExpenseRequestList(1);
                    extentReports.CreateLog("Return back to expense request list page ");

                    usersLogin.UserLogOut();
                    extentReports.CreateLog("Logout from salesforce as second level approver of event expense requested ");
                    //outlook.OutLookLogOut();
                    extentReports.CreateLog("Logout from outlook as second level approver of event expense requested ");
                    driver.Quit();

                    //Launch outlook 
                    OutLookInitialize();
                    outlook.LoginOutlook(fileOutlook);
                    extentReports.CreateLog("Login into outlook to verfied approved email recieved ");

                    //Verify approval email 
                   string expenseReqNumberFromApprovedEmail = outlook.VerifyExpenseRequestForApprovedEmail(0);
                    Assert.AreEqual(expensePreAppNumber, expenseReqNumberFromApprovedEmail);
                    extentReports.CreateLog("Approval email is recieved with expense RequestNumber " + expenseReqNumberFromApprovedEmail + " ");

                    //outlook.OutLookLogOut();
                    extentReports.CreateLog("Logout from outlook as approver of event expense requested ");
                    driver.Quit();
                    extentReports.CreateLog("Close the browser ");
                }
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}