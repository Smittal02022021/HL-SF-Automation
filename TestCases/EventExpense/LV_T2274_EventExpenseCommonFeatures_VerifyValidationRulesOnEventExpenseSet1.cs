using AventStack.ExtentReports;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Diagnostics;

namespace SalesForce_Project.TestCases.EventExpense
{
    internal class LV_T2274_EventExpenseCommonFeatures_VerifyValidationRulesOnEventExpenseSet1 : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ExpenseRequestPage expRequest = new ExpenseRequestPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages random= new RandomPages();

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
        public void SearchCFOpportunityWithNewJobTypeLV()
        {
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + fileT2274;

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
                usersLogin.SearchUserAndLogin(valUser);
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
                Assert.AreEqual("Complete this field.", msgLOB);
                extentReports.CreateStepLogs("Passed", "Message: " + msgLOB + " is displayed for LOB ");

                string valLOBExl = ReadExcelData.ReadData(excelPath, "EventExp", 1);
                string msgEventType = expRequest.ValidateEventTypeMessageLWC(valLOBExl);
                Assert.AreEqual("Complete this field.", msgEventType);
                extentReports.CreateStepLogs("Passed", "Message: " + msgEventType + " is displayed for Event Type");

                string msgRequestor = expRequest.ValidateRequestorMessageLWC(ReadExcelData.ReadData(excelPath, "EventExp", 2));
                Assert.AreEqual("Complete this field.", msgRequestor);
                extentReports.CreateStepLogs("Passed", "Message: " + msgRequestor + " is displayed for Requestor");

                string msgProductType = expRequest.ValidateProductTypeLWC();
                Assert.AreEqual("Complete this field.", msgProductType);
                extentReports.CreateStepLogs("Passed", "Message: " + msgProductType + " is displayed for Product Type");

                string msgEventName = expRequest.ValidateEventNameLWC();
                Assert.AreEqual("Complete this field.", msgEventName);
                extentReports.CreateStepLogs("Passed", "Message: " + msgEventName + " is displayed ");

                string msgCity = expRequest.ValidateCityLWC();
                Assert.AreEqual("Complete this field.", msgCity);
                extentReports.CreateStepLogs("Passed", "Message: " + msgCity + " is displayed for City");

                string msgETCost = expRequest.ValidateETCostLWC();
                Assert.AreEqual("Complete this field.", msgETCost);
                extentReports.CreateStepLogs("Passed", "Message: " + msgETCost + " is displayed for xpected Travel Cost ");

                string msgEFBCost = expRequest.ValidateEFBCostLWC();
                Assert.AreEqual("Complete this field.", msgEFBCost);
                extentReports.CreateStepLogs("Passed", "Message: " + msgEFBCost + " is displayed for Expected F&B Cost");

                string msgOtherCost = expRequest.ValidateOtherCostLWC();
                Assert.AreEqual("Complete this field.", msgOtherCost);
                extentReports.CreateStepLogs("Passed", "Message: " + msgOtherCost + " is displayed for Other Cost");

                // Fill All Required fields
                string nameRequestor = valUser;
                string nameProductType = ReadExcelData.ReadData(excelPath, "EventExp", 4);
                string nameEventContact= ReadExcelData.ReadData(excelPath, "EventExp", 5);                
                string nameEvent= ReadExcelData.ReadData(excelPath, "EventExp", 6);
                string nameCity= ReadExcelData.ReadData(excelPath, "EventExp", 7);
                
                string costET= ReadExcelData.ReadData(excelPath, "EventExp", 10);
                string costOther = ReadExcelData.ReadData(excelPath, "EventExp", 11);
                string costEFB = ReadExcelData.ReadData(excelPath, "EventExp", 12);                
                string costDscother = ReadExcelData.ReadData(excelPath, "EventExp", 13);
                

                expRequest.SaveExpenseRequestRequiredFieldsLWC(nameRequestor, nameEventContact, nameProductType, nameEvent, nameCity, costET, costEFB, costOther, costDscother);

                string errorHLOpportunity = random.GetLVMessagePopup();
                Assert.AreEqual("Please select hl opportunity", errorHLOpportunity);
                extentReports.CreateStepLogs("Passed", "Error: " + errorHLOpportunity + " is displayed for HL Internal Opportunity");

                string msgEventFormat = expRequest.ValidateEventFormatMessageLWC();
                Assert.AreEqual("Complete this field.", msgEventFormat);
                extentReports.CreateStepLogs("Passed", "Message: " + msgEventFormat + " is displayed for Event Format");
                
                string eventFormat = ReadExcelData.ReadData(excelPath, "EventExp", 8);
                expRequest.AssignEventFormatLWC(eventFormat);
                extentReports.CreateStepLogs("Passed", "Event Format: " + eventFormat + " value is assigned ");

                string nameHLOpportunity = ReadExcelData.ReadData(excelPath, "EventExp", 14);
                expRequest.AssignHLOpportunityLWC(nameHLOpportunity); 
                string msgSuccess = random.GetLVMessagePopup();
                Assert.AreNotEqual("Please select hl opportunity", errorHLOpportunity);
                extentReports.CreateStepLogs("Passed", errorHLOpportunity + " is displayed for HL Internal Opportunity");

                Assert.AreEqual("Expense Record Created Successfully", msgSuccess);
                extentReports.CreateStepLogs("Passed", msgSuccess + " is displayed");
                extentReports.CreateStepLogs("Passed", "Event Format value is assign ");
                string pageHeader = expRequest.getPageHeaderLWC();
                Assert.AreEqual("HL_Expense Request", pageHeader);
                extentReports.CreateStepLogs("Passed", "User is on "+pageHeader+" detail page");
                string expenseRequestNumber = expRequest.GetRequestNumberLWC();
                extentReports.CreateStepLogs("Passed", "Expense Request with Expense Preapproval Number:: " + expenseRequestNumber + " is created");
                random.CloseActiveTab(expenseRequestNumber);

                ////Save Requestor details and validate Event Expense details page is displayed
                //string lblRequestor = expRequest.SaveRequestorDetails(ReadExcelData.ReadData(excelPath, "Users", 1));
                //Assert.AreEqual("Requestor/Host Information", lblRequestor);
                //extentReports.CreateLog("Event Expense details page is displayed upon clicking Save button ");

                ////Click Back To Expense Request List and validate Event Expense Home page
                //string lblNewExp = expRequest.ClickBackAndValidatePage();
                //Assert.AreEqual("New Expense Request", lblNewExp);
                //extentReports.CreateLog("Event Expense Home page is displayed upon clicking Back To Expense Request List button ");

                ////Click on edit link and validate Event Expense Edit details page
                //string lblRequestorInfo = expRequest.ValidateEditFeature();
                //Assert.AreEqual("Requestor/Host information", lblRequestorInfo);
                //extentReports.CreateLog("Event Expense edit page is displayed upon clicking edit link ");

                ////Click on cancel button and validate Event Expense details page
                //string lblPage = expRequest.ValidateCancelFeature();
                //Assert.AreEqual("New Expense Request", lblPage);
                //extentReports.CreateLog("Event Expense details page is displayed upon clicking cancel button ");

                //Click Submit without filling mandatory details and validate all validations
                //string validationsList = expRequest.ClickSubmitWithoutMandatoryFields();
                //Console.WriteLine(validationsList);
                //Assert.AreEqual(ReadExcelData.ReadData(excelPath, "EventExp", 3), validationsList);
                //extentReports.CreateLog("Validations: " + validationsList + " are displayed ");

                ////Fill all the mandatory details of Event Expense Request, submit the request and validate the status
                //expRequest.ClickReturnToExpense();
                //expRequest.SaveAllValuesOfEventExpense(fileT2274);
                //expRequest.SubmitEventExpenseRequest();
                //string status = expRequest.GetRequestStatus();
                //Assert.AreEqual("Waiting for Approval", status);
                //extentReports.CreateLog("Ëvent Expense Request is submitted for approval ");


                homePageLV.UserLogoutFromSFLightningView();
                driver.Quit();

            }
            catch (Exception ex)
            {
                homePageLV.UserLogoutFromSFLightningView();
                driver.Quit();

            }
        }
    }
}
