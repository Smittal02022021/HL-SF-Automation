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

        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages random = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileT2276 = "LV_T2276_T2277_EventExpenseEmailNotificationsRejectApproveRequestAsFirstLevelApprover";

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
                    string ExpReqStatus = expRequestDetailPage.GetRequestStatusLWC();
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
