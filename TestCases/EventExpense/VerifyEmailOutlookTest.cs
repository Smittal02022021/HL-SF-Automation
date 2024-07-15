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
    class VerifyEmailOutlookTest : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        LVExpenseRequestCreatePage expRequest = new LVExpenseRequestCreatePage();
        LVExpenseRequestHomePage expRequestHomePage = new LVExpenseRequestHomePage();
        LVExpenseRequestDetailPage expRequestDetailPage = new LVExpenseRequestDetailPage();
        Outlook outlook = new Outlook();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages random = new RandomPages();

        public static string fileT2278 = "LV_T2278_EventExpenseEmailNotificationRequestMoreInformationApproveAsFirstLevelApproverSet1";
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
        public void RequestMoreInformationApproveAsFirstLevelApproverForEventExpenseRequestSet1()
        {
            //Launch outlook window
            OutLookInitialize();
            outlook.LoginOutlook(fileOutlook);
            string outlookLabel = outlook.GetLabelOfOutlook();
            Assert.AreEqual("Outlook", outlookLabel);
            extentReports.CreateStepLogs("Passed", "Verified and Validation is done for User is logged in to outlook ");
            string expensePreAppNumber = "EE00007168";
            //Selecting Expense Request Approval email
            outlook.SelectExpenseApprovalEmailV();
            //Validating Title of Login Page
            Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
            extentReports.CreateStepLogs("Passed", "User is redirected to salesforce with " + driver.Title + " is displayed ");

            //Switch to Salesforce from outlook
            login.LoginAsExpenseRequestApproverV(fileT2278, 1);
            extentReports.CreateStepLogs("Info", "Verified and Validation of User being redirected to Event Expense Form upon successful authentication ");

            //Check if UI is Classic, Click back to List button, Switch to lV Search Pending Requests
            string RequestUIStatus = expRequestHomePage.OpenPendingApprovalExpenseRequestLWC(expensePreAppNumber);
            extentReports.CreateStepLogs("Info", RequestUIStatus);

            // Validate status of the event request on my request page in the request list
            string requestStatus = expRequestDetailPage.GetExpenseRequestStatusLWC(1);
            Assert.AreEqual("Waiting for Approval", requestStatus);
            extentReports.CreateStepLogs("Passed", "Expense request status is validated as " + requestStatus + " ");

            //Verify request for more information button
            bool requiestMoreInfoStatus = expRequestDetailPage.IsButtonDisplayedLWC("Request More Information");
            Assert.IsTrue(requiestMoreInfoStatus, "Verify Request More Information button is Displayed on Request Details Page");
            extentReports.CreateStepLogs("Passed", "Request More Information button is Displayed on Request Details Page");

            //Request More Information
            expRequestDetailPage.ClickRequestMoreInformationButtonLWC();
            string bubbleMessage = random.GetLVMessagePopup();
            string validationMessage = expRequest.GetValidationsLWC(bubbleMessage);
            Assert.AreEqual("Request Submitted Successfully", validationMessage, " Validate the Sucess Pop-up after More Information Requested Request");
            extentReports.CreateStepLogs("Info", "Pop-up Message after More Information Requested is " + validationMessage);

            string status = expRequestDetailPage.GetExpenseRequestStatusLWC();
            //Issue Page is not being Reloaded to reflect the latest Sstaus 
            Assert.AreEqual("More Information Requested", status, "Verify the Status of Request after More Information Requested");
            extentReports.CreateStepLogs("Info", "****Fail*****Pending Issue********** More Information Requested expense request and expense request status validated as " + status + " ");
            
            homePageLV.UserLogoutFromSFLightningView();
            driver.Quit();
            extentReports.CreateStepLogs("Info", "Approver Logged out and close the browser");

        }
    }
}