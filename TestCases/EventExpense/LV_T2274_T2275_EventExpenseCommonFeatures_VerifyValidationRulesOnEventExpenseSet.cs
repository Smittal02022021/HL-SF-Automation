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
    internal class LV_T2274_T2275_EventExpenseCommonFeatures_VerifyValidationRulesOnEventExpenseSet : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        LVExpenseRequestCreatePage expRequest = new LVExpenseRequestCreatePage();
        LVExpenseRequestHomePage expRequestHomePage = new LVExpenseRequestHomePage();
        LVExpenseRequestDetailPage expRequestDetailPage  = new LVExpenseRequestDetailPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages random= new RandomPages();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileT2274 = "LV_T2274_VerifyValidationRulesOnEventExpense";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyValidationRulesOnEventExpenseLV()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2274;
                string futureDate= DateTime.Today.AddDays(1).ToString("MMM dd, yyyy");
                string pastDate = DateTime.Today.AddDays(-2).ToString("MMM dd, yyyy");
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

                //Validate Required fields validation on Expense Request Form
                expRequest.ClickCreateNewExpenseFormLWC();
                string msgLOB = expRequest.ValidateLOBMessageLWC();
                Assert.AreEqual("LOB Complete this field.", msgLOB);
                extentReports.CreateStepLogs("Passed", "Error Message: " + msgLOB + " is displayed for LOB ");

                //**Need to Multiple Expense Request with different Event type because Submit for approval required fields are different for diff event type
                string valLOBExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp",2, 1);
                string msgEventType = expRequest.ValidateEventTypeMessageLWC(valLOBExl);
                Assert.AreEqual("Event Type Complete this field.", msgEventType);
                extentReports.CreateStepLogs("Passed", "Error Message: " + msgEventType + " is displayed for Event Type");
                string eventTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", 2, 2);
                string msgRequestor = expRequest.ValidateRequestorMessageLWC(eventTypeExl);
                Assert.AreEqual("Requestor Complete this field.", msgRequestor);
                extentReports.CreateStepLogs("Passed", "Error Message: " + msgRequestor + " is displayed for Requestor");

                string msgProductType = expRequest.ValidateProductTypeLWC();
                Assert.AreEqual("Product Type Complete this field.", msgProductType);
                extentReports.CreateStepLogs("Passed", "Error Message: " + msgProductType + " is displayed for Product Type");

                string msgEventName = expRequest.ValidateEventNameLWC();
                Assert.AreEqual("Event Name Complete this field.", msgEventName);
                extentReports.CreateStepLogs("Passed", "Error Message: " + msgEventName + " is displayed ");

                string msgCity = expRequest.ValidateCityLWC();
                Assert.AreEqual("City Complete this field.", msgCity);
                extentReports.CreateStepLogs("Passed", "Error Message: " + msgCity + " is displayed for City");

                string msgETCost = expRequest.ValidateETCostLWC();
                Assert.AreEqual("Expected Travel Cost Complete this field.", msgETCost);
                extentReports.CreateStepLogs("Passed", "Error Message: " + msgETCost + " is displayed for xpected Travel Cost ");

                string msgEFBCost = expRequest.ValidateEFBCostLWC();
                Assert.AreEqual("Expected F&B Cost Complete this field.", msgEFBCost);
                extentReports.CreateStepLogs("Passed", "Error Message: " + msgEFBCost + " is displayed for Expected F&B Cost");

                //T2275 Other cost fields and validate displayed messages
                string msgOtherCost = expRequest.ValidateOtherCostLWC();
                Assert.AreEqual("Other Cost Complete this field.", msgOtherCost);
                extentReports.CreateStepLogs("Passed", "Error Message: " + msgOtherCost + " is displayed for Other Cost");

                ///---------------
                //T2275 add Other Cost and validation message for Description of Other Cost
                string msgDscOtherCost = expRequest.ValidateDscOtherCostLWC();
                Assert.AreEqual("Description of Other Cost Complete this field.", msgDscOtherCost);
                extentReports.CreateStepLogs("Passed", "Error Message: " + msgDscOtherCost + " is displayed for Description of Other Cost");

                //T2275 Verify the Description of Marketing Support field state for Yes/No
                expRequest.SelectMarketingSupportLWC("Yes");
                bool isDescMarketingSupport = expRequest.GetDescriptionMarketingSupportStateLWC();
                Assert.IsTrue(isDescMarketingSupport, "Verify Description Marketing Support field is Enabled after selecting Marketing support as Yes");
                extentReports.CreateStepLogs("Passed", "Description of Marketing Support field is Enabled after selecting Marketing support as Yes ");
                                
                expRequest.SelectMarketingSupportLWC("No");
                isDescMarketingSupport = expRequest.GetDescriptionMarketingSupportStateLWC();
                Assert.IsFalse(isDescMarketingSupport, "Verify Description Marketing Support field is Disabled after selecting Marketing support as No");
                extentReports.CreateStepLogs("Passed", "Description of Marketing Support field is Disabled after selecting Marketing support as No ");

                //----------------------
                // Fill All Required fields
                string nameRequestor = valUser;
                string nameProductType = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp",2, 4);
                string nameEventContact= ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp",2, 5);                
                string nameEvent= ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp",2, 6);
                string nameCity= ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp",2, 7);                
                string costET= ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp",2, 10);
                string costOther = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp",2, 11);
                string costEFB = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", 2, 12);                
                string costDscother = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", 2, 13);                

                expRequest.SaveExpenseRequestRequiredFieldsLWC(nameRequestor, nameEventContact, nameProductType, nameEvent, nameCity, costET, costEFB, costOther, costDscother);
                extentReports.CreateStepLogs("Passed", "Values filled for all highlighted fields");
                string errorHLOpportunity = random.GetLVMessagePopup();
                Assert.AreEqual("Please select hl opportunity", errorHLOpportunity);
                extentReports.CreateStepLogs("Passed", "Bubble Message::  " + errorHLOpportunity + " is displayed for HL Internal Opportunity");

                string msgEventFormat = expRequest.ValidateEventFormatMessageLWC();
                Assert.AreEqual("Event Format Complete this field.", msgEventFormat);
                extentReports.CreateStepLogs("Passed", "Error Message: " + msgEventFormat + " is displayed for Event Format");
                                
                string eventFormat = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", 2, 8);
                expRequest.AssignEventFormatLWC(eventFormat);
                extentReports.CreateStepLogs("Passed", "Event Format: " + eventFormat + " value is assigned ");

                // Internal Opportunity should created as per Event Type
                // e.g. Request Event Type: ADM Staff Entertainment and Internal HL opp should be of Classification: ADM 
                // and can be automated when we have manual test scripts for this 

                string nameHLOpportunity = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", 2, 14);
                expRequest.AssignHLOpportunityLWC(nameHLOpportunity); 

                //Save Requestor/Required details and validate Event Expense details page is displayed
                string pageHeader = expRequestHomePage.GetPageHeaderLWC();
                Assert.AreEqual("HL_Expense Request", pageHeader);
                extentReports.CreateStepLogs("Passed", "User is on "+pageHeader+" detail page");
                string expenseRequestNumber = expRequestDetailPage.GetRequestNumberLWC();
                extentReports.CreateStepLogs("Passed", "Expense Request with Expense Preapproval Number:: " + expenseRequestNumber + " is created");

                //Click Back To Expense Request/Closing the Expense Request detail page and validate Event Expense Home page
                random.CloseActiveTab(expenseRequestNumber);
                extentReports.CreateStepLogs("Passed", "Newly created Expense Request:: "+ expenseRequestNumber+" detail page is closed ");
                string txtButtonName = expRequest.GetButtonnameLWC();
                Assert.AreEqual("Create New Expense Form", txtButtonName);
                extentReports.CreateStepLogs("Passed", "After closing Expense Request detail page user is redirected to "+ txtButtonName);
                
                expRequestHomePage.SelectRequestTabLWC("My Requests");
                string headerExpNumber = expRequestHomePage.SearchAndSelectExpenseRequestLWC(expenseRequestNumber, "My Requests");
                Assert.AreEqual(headerExpNumber, expenseRequestNumber);
                extentReports.CreateStepLogs("Passed", "User is on Expense Request :: "+ headerExpNumber+" detail page");


                //****************************WIll work*****************************/////
                ////Click on edit link and validate Event Expense Edit details page
                //string lblRequestorInfo = expRequest.ValidateEditFeature();
                //Assert.AreEqual("Requestor/Host information", lblRequestorInfo);
                //extentReports.CreateLog("Event Expense edit page is displayed upon clicking edit link ");

                ////Click on cancel button and validate Event Expense details page
                //string lblPage = expRequest.ValidateCancelFeature();
                //Assert.AreEqual("New Expense Request", lblPage);
                //extentReports.CreateLog("Event Expense details page is displayed upon clicking cancel button ");

                //****************************Out of Scope*****************************/////

                //Click Submit without filling mandatory details and validate all validations
                expRequestDetailPage.ClickEventExpenseRequestButtonLWC("Submit for Approval (LWC)");
                string bubbleMessage = random.GetLVMessagePopup();
                string validationMessage = expRequest.GetValidationsLWC(bubbleMessage);
                string validationMsgExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp",2, 3);
                Assert.AreEqual(validationMsgExl, validationMessage);
                extentReports.CreateStepLogs("Passed", "Bubble Message:: " + bubbleMessage + " is displayed after assiging HL Internal Opportunity");

                //Fill all the mandatory details of Event Expense Request, submit the request and validate the status
                expRequestDetailPage.ClickEditExpenseRequestButtonLWC();
                string numberOfGuestExl= ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", 2, 9);
                string nameTeamMemberExl= ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp", 2, 15);

                expRequest.SaveExpenseRequestSubmitForApprovalRequiredFieldsLWC(eventTypeExl, numberOfGuestExl, nameTeamMemberExl);
                extentReports.CreateStepLogs("Passed", "All required fields are filled for Event Type:  " + eventTypeExl);

                //T2275 Verify the End date should be greater than Start date validation 
                expRequestDetailPage.EditExpenseRequestEndDateLWC(pastDate);
                expRequestDetailPage.ClickEventExpenseRequestButtonLWC("Submit for Approval (LWC)");
                bubbleMessage = random.GetLVMessagePopup();
                validationMessage = expRequest.GetValidationsLWC(bubbleMessage);
                validationMsgExl = ReadExcelData.ReadDataMultipleRows(excelPath, "EventExp2", 2, 3);
                Assert.AreEqual(validationMsgExl, validationMessage);
                extentReports.CreateStepLogs("Passed", "Bubble Message:: " + validationMessage + "  is displayed for End date");
                expRequestDetailPage.EditExpenseRequestEndDateLWC(futureDate);

                expRequestDetailPage.ClickEventExpenseRequestButtonLWC("Submit for Approval (LWC)");
                string status = expRequestDetailPage.GetExpenseRequestStatusLWC();
                Assert.AreEqual("Waiting for Approval", status);
                extentReports.CreateStepLogs("Passed","Event Expense Request:: "+ headerExpNumber+"  is submitted for approval and status is "+ status);
                
                random.CloseActiveTab(expenseRequestNumber);
                homePageLV.UserLogoutFromSFLightningView();                
                extentReports.CreateLog("User:: "+ valUser+" Logged out ");
                driver.Quit();
                extentReports.CreateLog("Browser Closed ");
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
