using NUnit.Framework;
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
    class TMTT0017341_EventExpense_VerifyTheCFExpenseRequestFunctionalityOfSearchFilters : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LVExpenseRequestHomePage lvExpenseRequest = new LVExpenseRequestHomePage();
        LVExpenseRequestCreatePage lvCreateExpRequest = new LVExpenseRequestCreatePage();
        LVExpenseRequestDetailPage lvExpRequestDetail = new LVExpenseRequestDetailPage();

        public static string fileTC17341 = "TMTT0017341_EventExpense_VerifyTheCFExpenseRequestFunctionalityOfSearchFilters";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTheCFExpenseRequestFunctionalityOfSearchFilters()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC17341;
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

                int userCount = ReadExcelData.GetRowCount(excelPath, "Users");
                for (int row = 2; row <= userCount; row++)
                {
                    string evName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 4);

                    //Search CF Financial user by global search
                    string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    lvHomePage.SearchUserFromMainSearch(user);

                    //Verify searched user
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, user + " | Salesforce"), true);
                    extentReports.CreateLog("User " + user + " details are displayed ");

                    //Login as CF Financial user
                    lvHomePage.UserLogin();
                    Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(user));

                    if(row==2)
                    {
                        extentReports.CreateLog("CF Financial User: " + user + " is able to login. ");
                    }
                    else
                    {
                        extentReports.CreateLog("CAO User: " + user + " is able to login. ");
                    }

                    //Click on the Menu button
                    lvHomePage.ClickHomePageMenu();

                    //Go to Expense Request Page
                    lvHomePage.SearchItemExpenseRequestLWC("Expense Request(LWC)");
                    Assert.IsTrue(lvExpenseRequest.VerifyIfExpenseRequestPageIsOpenedSuccessfully());
                    extentReports.CreateLog("Expense Request page opened successfully in SF Lightning View. ");

                    //TC - TMTI0038495 - Verify My Request tab is displayed
                    Assert.IsTrue(lvExpenseRequest.VerifyIfMyRequestTabIsDisplayed());
                    extentReports.CreateLog("My Request tab is displayed upon opening Expense Reuest page in SF Lightning View. ");
                    
                    //TC - TMTI0038495 -Verify Requests Pending My Approval Tab is displayed
                    Assert.IsTrue(lvExpenseRequest.VerifyIfRequestsPendingMyApprovalTabIsDisplayed());
                    extentReports.CreateLog("Requests Pending My Approval tab is also displayed upon opening Expense Reuest page in SF Lightning View. ");

                    if (row==3)
                    {
                        //TC - TMTI0038496 - Verify My Request tab is displayed for CAO User Only
                        Assert.IsTrue(lvExpenseRequest.VerifyIfAllRequestsTabIsDisplayed());
                        extentReports.CreateLog("All Requests tab is displayed upon opening Expense Reuest page in SF Lightning View. ");
                    }

                    if (row==2)
                    {
                        //TC - TMTI0038497 - Verify the default values in search filters on expense request page
                        Assert.IsTrue(lvExpenseRequest.VerifyDefaultValuesInSearchFiltersOnExpenseRequestPage());
                        extentReports.CreateLog("Default value selected for Criteria Filter is : AND and for other filters is : None ");

                        //Create a new Expense Request LWC
                        string lobName = ReadExcelData.ReadDataMultipleRows(excelPath, "ExpenseRequest", row, 1);
                        lvCreateExpRequest.CreateNewExpenseRequestLWC(lobName, fileTC17341, row);
                        Assert.IsTrue(lvExpRequestDetail.VerifyIfExpensePreapprovalNumberIsDisplayed());

                        //Get Expense Request Pre-approval Number
                        string expReqpreApprovalNo = lvExpRequestDetail.GetExpensePreapprovalNumber();
                        string eventType = lvExpRequestDetail.GetEventTypeInfo();
                        string eventFormat = lvExpRequestDetail.GetEventFormatInfo();

                        extentReports.CreateLog("Expense Request(LWC) of LOB: " + lobName + " is created successfully. Expense Preapproval Number: " + expReqpreApprovalNo + " ");

                        //Click on the Menu button
                        lvHomePage.ClickHomePageMenu();

                        //Go to Expense Request Page
                        lvHomePage.SearchItemExpenseRequestLWC("Expense Request(LWC)");

                        //TC - TMTI0038498, TMTI0038504 - Verify the filter functionality with default criteria selected as "AND" and data in other filter fields.
                        Assert.IsTrue(lvExpenseRequest.VerifyIfCreatedExpenseRequestIsDisplayedUnderMyRequestsTab(expReqpreApprovalNo));
                        extentReports.CreateLog("Created Expense Request is visible under My Requests tab upon selecting ddefault criteria as AND and entering data in other fields. ");

                        //TC - TMTI0038500 - Verify that filter options "Event Type" and "Event Format" fields should get enabled on selection of LOB.
                        Assert.IsTrue(lvExpenseRequest.VerifyFilterOptionsEventTypeAndEventFormatUponLOBSelection(lobName, eventType, eventFormat, expReqpreApprovalNo));
                        extentReports.CreateLog("Filter options : Event Type gets enabled on selection of LOB and Event Format field gets enabled upon selection of Event Type field. ");

                        //TC - TMTI0038501 - Verify the filter functionality on the selection of "Submission Date".
                        Assert.IsTrue(lvExpenseRequest.VerifyFilterFunctionalityOnSelectionOfSubmissionDate());
                        extentReports.CreateLog("Searched results are displayed as per Submission Date filter. ");

                        //TC - TMTI0038502 - Verify the filter functionality on the selection of "Created Date".
                        Assert.IsTrue(lvExpenseRequest.VerifyFilterFunctionalityOnSelectionOfCreatedDate());
                        extentReports.CreateLog("Searched results are displayed as per Created Date filter. ");

                        //TC - TMTI0038503 - Verify the filter functionality on the selection of "Event Name".
                        Assert.IsTrue(lvExpenseRequest.VerifyFilterFunctionalityOnSelectionOfEventName(evName));
                        extentReports.CreateLog("Searched results are displayed as per Event Name filter. ");

                        //TC - TMTI0038505 - Verify the filter functionality on the selection of "Product Type".
                        Assert.IsTrue(lvExpenseRequest.VerifyFilterFunctionalityOnSelectionOfProductType());
                        extentReports.CreateLog("Searched results are displayed as per Product Type filter. ");

                        //TC - TMTI0038506 - Verify the filter functionality on the selection of "Requestor".
                        Assert.IsTrue(lvExpenseRequest.VerifyFilterFunctionalityOnSelectionOfRequestor(user));
                        extentReports.CreateLog("Searched results are displayed as per Requestor filter. ");

                        //TC - TMTI0038507 - Verify the filter functionality in combination of "Created Date" with "Event Name".
                        Assert.IsTrue(lvExpenseRequest.VerifyFilterFunctionalityInCombinationOfCreatedDateWithEventName(evName));
                        extentReports.CreateLog("Searched results are displayed as per combination of Created Date with Event Name. ");
                    }

                    //Logout from SF Lightning View
                    lvHomePage.LogoutFromSFLightningAsApprover();
                    extentReports.CreateLog("User Logged Out from SF Lightning View. ");
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