using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.EventExpense;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.EventExpense
{
    class LV_TMTC0036647_3_VerifyNewCFExpenseRequestCreationCloneDeleteSubmissionApproveFunctionalityAsCAOUser:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        LVExpenseRequestCreatePage expRequest = new LVExpenseRequestCreatePage();
        LVExpenseRequestHomePage expRequestHomePage = new LVExpenseRequestHomePage();
        LVExpenseRequestDetailPage expRequestDetailPage = new LVExpenseRequestDetailPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileT36647 = "LV_TMTC0036647_3_VerifyNewCFExpenseRequestCreationCloneDeleteSubmissionApproveFunctionalityAsCAOUser";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMT0084918	Verify that the New Expense request can be created using the available picklist option "CF" in LOB field as a CAO. 
        [Test]
        public void VerifyCFExpenseRequestCloneDeleteSubmissionApproveFunctionalityAsCAOUserLV()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + fileT36647;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "CAO User: " + valUser + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string stdUser = login.ValidateUserLightningView();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CAO User: " + valUser + " logged in on Lightning View");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "CAO User is on " + moduleNameExl + " Module Page ");

                int rowRequest = ReadExcelData.GetRowCount(excelPath, "ExpenseRequest");
                for (int row = 2; row <= rowRequest; row++)
                {
                    //CreateNewExpenseRequest with All required field for submition 
                    string nameRequestor = valUser;
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

                    //Create a new Expense Request.
                    expRequest.CreateNewExpenseRequestLWC(lobName, fileT36647, row);
                    ////Get expense pre approver number
                    string expensePreAppNumber = expRequestDetailPage.GetRequestNumberLWC();
                    extentReports.CreateStepLogs("Passed", "Expense Request created with number " + expensePreAppNumber);
                    Assert.IsTrue(expRequestDetailPage.VerifyIfExpensePreapprovalNumberIsDisplayed());

                    string expReqpreApprovalNo = expRequestDetailPage.GetExpensePreapprovalNumber();
                    string eventStatus = expRequestDetailPage.GetEventStatusInfo();
                    extentReports.CreateLog("Expense Request of LOB: " + lobName + " is created successfully with Status as: " + eventStatus + " and Expense Preapproval Number: " + expReqpreApprovalNo + " ");
                    Assert.IsTrue(expRequestDetailPage.VerifyFVACloneButtonFunctionalityLV());
                    extentReports.CreateStepLogs("Passed", "New expense form is openning with pre-filled details from the cloned request. ");

                    expRequestDetailPage.CreateCloneExpenseRequestLV();
                    string requestStatus = expRequestDetailPage.GetClonedExpenseRequestStatusLWC();
                    string clonedExpenseRequestNumber = expRequestDetailPage.GetCloneExpensePreapprovalNumber();
                    Assert.AreEqual("Saved", requestStatus);
                    extentReports.CreateStepLogs("Passed", "Expense Request: " + clonedExpenseRequestNumber + " Cloned from Expense Request:" + expensePreAppNumber + " with status: " + requestStatus);

                    //6. Verify the Delete functionality. 
                    expRequestDetailPage.DeleteClonedRequestLV();
                    Assert.AreEqual("Deleted", expRequestDetailPage.GetDeletedExpenseRequestStatusLWC(), "Verify deleted cloned request status should be deleted");
                    Assert.AreEqual(clonedExpenseRequestNumber, expRequestDetailPage.GetDeletedExpensePreapprovalNumber());
                    extentReports.CreateStepLogs("Passed", "Cloned Expense Request: " + clonedExpenseRequestNumber + " Deleted and status is updated as 'Deleted'");
                    randomPages.CloseActiveTab(clonedExpenseRequestNumber);

                    //7. Click on Expense Request - "Submit for Approval (LWC) button and submit the Approval request. 
                    //Click submit for approval button
                    expRequestDetailPage.ClickEventExpenseRequestButtonLWC("Submit for Approval (LWC)");
                    requestStatus = expRequestDetailPage.GetExpenseRequestStatusLWC();
                    Assert.AreEqual("Waiting for Approval", requestStatus);
                    extentReports.CreateStepLogs("Passed", "Event Expense Request:: " + expensePreAppNumber + "  is submitted for approval and status is " + requestStatus);

                    randomPages.CloseActiveTab(expensePreAppNumber);
                    extentReports.CreateStepLogs("Passed", "Submitted Expense Request Detail page is closed and user returned to Expense List page");
                    homePageLV.LogoutFromSFLightningAsApprover();

                    //9. Login as an Expense Approver and load the Expense request (LWC)
                    string approver = ReadExcelData.ReadDataMultipleRows(excelPath, "Approver", 2, 1);
                    homePage.SearchUserByGlobalSearchN(approver);
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string approverUser = login.ValidateUserLightningView();
                    Assert.AreEqual(approverUser.Contains(approver), true);
                    extentReports.CreateStepLogs("Passed", "Approverr: " + approver + " logged in on Lightning View");

                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "Expense Approver is on " + moduleNameExl + " Module Page ");
                    expRequestHomePage.SelectRequestTabLWC("Requests Pending My Approval");
                    expRequestHomePage.SearchAndSelectExpenseRequestLWC(expensePreAppNumber, "Requests Pending My Approval");

                    // Validate status of the event request on my request page in the request list
                    requestStatus = expRequestDetailPage.GetExpenseRequestStatusLWC(1);
                    Assert.AreEqual("Waiting for Approval", requestStatus);
                    extentReports.CreateStepLogs("Passed", "Expense request status is validated as " + requestStatus + " ");

                    //Verify Approve button 
                    bool approveBtnStatus = expRequestDetailPage.IsButtonDisplayedLWC("Approve(LWC)");
                    Assert.IsTrue(approveBtnStatus, "Verify Approve(LWC) button is Displayed on Request Details Page");
                    extentReports.CreateStepLogs("Passed", "Approve button is Displayed on expense request detail page ");

                    ////Approve expense request
                    expRequestDetailPage.ClickApproveButtonLWC();
                    requestStatus = expRequestDetailPage.GetExpenseRequestStatusLWC();
                    Assert.AreEqual("Approved", requestStatus);
                    extentReports.CreateStepLogs("Passed", "Expense Request Status is Approved");
                    randomPages.CloseActiveTab(expensePreAppNumber);
                    homePageLV.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Passed", "Expense Approver: " + approver + " logged out");
                   
                }

                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
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