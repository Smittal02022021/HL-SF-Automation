using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.EventExpense;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Threading;

namespace SF_Automation.TestCases.EventExpense
{
    class TMTT0017339_EventExpense_VerifyTheCFExpenseRequestFunctionalityAsApprover : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LVExpenseRequestHomePage lvExpenseRequest = new LVExpenseRequestHomePage();
        LVExpenseRequestCreatePage lvCreateExpRequest = new LVExpenseRequestCreatePage();
        LVExpenseRequestDetailPage lvExpRequestDetail = new LVExpenseRequestDetailPage();
        Outlook outlook = new Outlook();

        public static string fileTC17339 = "TMTT0017339_EventExpense_VerifyTheCFExpenseRequestFunctionalityAsApprover";
        public static string fileOutlook = "Outlook";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTheCFExpenseRequestFunctionalityAsApprover()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC17339;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                //Validate user logged in
                Assert.AreEqual(driver.Url.Contains("lightning"), true);
                extentReports.CreateLog("User is able to login into SF");

                int row = 2;

                string evName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 4);
                string lobName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 1);
                string mandatoryFieldErrMsg = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 12);
                string errorMessage = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 13);
                string updatedEventInfo = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 15);
                string approverResp = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 16);
                string approverNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 17);
                string approverErrMsg = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 18);
                string rejectionNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 19);
                string requestNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 20);
                string approvalNotes = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 21);

                string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                string user1 = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                int rowCount = ReadExcelData.GetRowCount(excelPath, "Actions");

                //Select HL Banker app
                try
                {
                    lvHomePage.SelectAppLV("HL Banker");
                }
                catch(Exception)
                {
                    lvHomePage.SelectAppLV1("HL Banker");
                }

                //Search CF Financial user by global search
                lvHomePage.SearchUserFromMainSearch(user);

                //Verify searched user
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, user + " | Salesforce"), true);
                extentReports.CreateLog("User " + user + " details are displayed ");

                //Login as CF Financial user
                lvHomePage.UserLogin();
                Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(user));
                extentReports.CreateLog("CF Financial User: " + user + " is able to login into lightning view. ");

                //Click on the Menu button
                lvHomePage.ClickHomePageMenu();

                for (int actionRow = 2; actionRow <= rowCount; actionRow++)
                {
                    Actions action2 = new Actions(driver);

                    //Read what action approver needs to perform
                    string action = ReadExcelData.ReadDataMultipleRows(excelPath, "Actions", actionRow, 1);
                    
                    //Go to Expense Request Page
                    lvHomePage.SearchItemExpenseRequestLWC("Expense Request(LWC)");
                    Assert.IsTrue(lvExpenseRequest.VerifyIfExpenseRequestPageIsOpenedSuccessfully());
                    extentReports.CreateLog("Expense Request page opened successfully. ");

                    //Create a new Expense Request.
                    lvCreateExpRequest.CreateNewExpenseRequestLWC(lobName, fileTC17339, row);
                    Assert.IsTrue(lvExpRequestDetail.VerifyIfExpensePreapprovalNumberIsDisplayed());

                    string expReqpreApprovalNo = lvExpRequestDetail.GetExpensePreapprovalNumber();
                    string eventStatus = lvExpRequestDetail.GetEventStatusInfo();

                    extentReports.CreateLog("Expense Request of LOB: " + lobName + " is created successfully with Status as: " + eventStatus + " and Expense Preapproval Number: " + expReqpreApprovalNo + " ");

                    //TC - TMTI0038456 - Verify that email notification has been sent to appropriate Approver on Submission of the Expense Request for approval
                    Assert.IsTrue(lvExpRequestDetail.SubmitExpenseRequestForApproval());
                    extentReports.CreateLog("Expense Request submitted for approval successfully. ");

                    //Logout from SF Lightning View
                    lvHomePage.LogoutFromSFLightningAsApprover();
                    extentReports.CreateLog("User Logged Out from SF Lightning View. ");

                    driver.Quit();
                    
                    //Launch outlook window
                    OutLookInitialize();

                    //Login into Outlook
                    outlook.LoginOutlook(fileOutlook);
                    string outlookLabel = outlook.GetLabelOfOutlook();
                    Assert.AreEqual("Outlook", outlookLabel);
                    extentReports.CreateLog("User is logged in to outlook ");

                    outlook.SelectExpenseApprovalEmail();
                    extentReports.CreateLog("An Email notification with subject line : Sandbox: Marketing Expense submission confirmation ");
                    
                    login.LoginAsExpenseRequestApprover(fileTC17339);

                    //Switch to lightning view
                    if (driver.Title.Contains("Salesforce - Unlimited Edition"))
                    {
                        homePage.SwitchToLightningView();
                    }

                    extentReports.CreateLog("Approver: " + user1 + " is able to login into lightning view. ");

                    // Validate status of the event request on my request page in the request list
                    string eventStatus1 = lvExpRequestDetail.GetEventStatusInfoForApprover();
                    Assert.AreEqual("Waiting for Approval", eventStatus1);
                    extentReports.CreateLog("Expense request status is verified as " + eventStatus1 + " ");

                    //TC - TMTI0038457 - Verify that the approver of the expense request can see buttons like  "Approve, Reject, Required more Information"
                    Assert.IsTrue(lvExpRequestDetail.VerifyNecessaryButtonsAreDisplayedWhenApproverLandsOnExpenseDetailPage());
                    extentReports.CreateLog("Approver of the expense request can see buttons like: Delete, Approve, Reject, Edit and Required more Information. ");

                    if (action == "Edit")
                    {
                        //TC - TMTI0038450 - Verify the Edit functionality from expense request detail page as approver.
                        lvExpRequestDetail.EditExpenseRequestAsApprover(approverNotes);
                        string updatedNotes = lvExpRequestDetail.GetApproverNotesDetailsUnderAdditionalInfo();
                        Assert.AreEqual(approverNotes, updatedNotes);
                        extentReports.CreateLog("Approver is able to edit expense request successfully. ");
                    }
                    else if (action == "Delete")
                    {
                        //TC - TMTI0038452 - Verify the "Delete" functionality from expense request detail page as approver.
                        Assert.IsTrue(lvExpRequestDetail.VerifyDeleteExpenseRequestFunctionalityAsApprover());
                        string eventStatus3 = lvExpRequestDetail.GetEventStatusInfoForApprover();
                        Assert.AreEqual(eventStatus3, "Deleted");
                        extentReports.CreateLog("Expense request with Expense Preapproval Number: " + expReqpreApprovalNo + " is deleted succssfully with status: " + eventStatus3 + " ");

                        //TC - TMTI0038454 - Verify that approver is not able to "Edit" the deleted request.
                        Assert.IsTrue(lvExpRequestDetail.VerifyApproverIsNotAbleToEditExpenseRequest(approverErrMsg));
                        extentReports.CreateLog("Approver is not able to edit the deleted request and getting the expected error message: " + approverErrMsg + " ");
                    }
                    else if (action == "Reject")
                    {
                        //TC - TMTI0038458 - Verify that approver is able to "Reject" the expense request.
                        lvExpRequestDetail.RejectExpenseRequest(rejectionNotes);
                        
                        string eventRejectStatus = lvExpRequestDetail.GetEventStatusInfoForApprover();
                        Assert.AreEqual(eventRejectStatus, "Rejected");
                        extentReports.CreateLog("Event Request is rejected. Status is updated to: " + eventRejectStatus + " and notes are updated as expected under Additional Information section. ");
                    }
                    else if (action == "Request More Information")
                    {
                        //TC - TMTI0038459 - Verify that approver is able to "Request more Information" from the requester.
                        lvExpRequestDetail.RequestForMoreInformation(requestNotes);
                        
                        string eventMoreInfoStatus = lvExpRequestDetail.GetEventStatusInfoForApprover();
                        Assert.AreEqual(eventMoreInfoStatus, "More Information Requested");
                        extentReports.CreateLog("Approver has requested for more information for the Event Request. Status is updated to: " + eventMoreInfoStatus + " and notes are updated as expected under Additional Information section. ");
                    }
                    else if (action == "Approve")
                    {
                        //TC - TMTI0038460 - Verify that approver is able to "Approve" the request.
                        lvExpRequestDetail.ApproveExpenseRequest();
                        
                        string eventApproveStatus = lvExpRequestDetail.GetEventStatusInfoForApprover();
                        Assert.AreEqual(eventApproveStatus, "Approved");
                        extentReports.CreateLog("Event Request is approved. Status is updated to: " + eventApproveStatus + " ");
                    }

                    //Logout from SF Lightning View
                    lvHomePage.LogoutFromSFLightningAsApprover();
                    extentReports.CreateLog("User Logged Out from SF Lightning View. ");

                    driver.Quit();

                    Initialize();

                    //Calling Login function                
                    login.LoginApplication();

                    //Switch to lightning view
                    if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                    {
                        homePage.SwitchToLightningView();
                        extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                    }

                    //Validate user logged in
                    Assert.AreEqual(driver.Url.Contains("lightning"), true);
                    extentReports.CreateLog("User is able to login into SF");

                    //Select HL Banker app
                    try
                    {
                        lvHomePage.SelectAppLV("HL Banker");
                    }
                    catch(Exception)
                    {
                        lvHomePage.SelectAppLV1("HL Banker");
                    }

                    //Search CF Financial user by global search
                    lvHomePage.SearchUserFromMainSearch(user);

                    //Verify searched user
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, user + " | Salesforce"), true);
                    extentReports.CreateLog("User " + user + " details are displayed ");

                    //Login as CF Financial user
                    lvHomePage.UserLogin();
                    Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(user));
                    extentReports.CreateLog("CF Financial User: " + user + " is able to login into lightning view. ");

                    //Click on the Menu button
                    lvHomePage.ClickHomePageMenu();
                }

                //TC - End
                lvHomePage.UserLogoutFromSFLightningView();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.Quit();
            }
        }
    }
}
