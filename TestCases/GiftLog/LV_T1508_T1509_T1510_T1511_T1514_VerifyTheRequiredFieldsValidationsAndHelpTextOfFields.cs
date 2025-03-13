using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.GiftLog;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.GiftLog
{
    class LV_T1508_T1509_T1510_T1511_T1514_VerifyTheRequiredFieldsValidationsAndHelpTextOfFields : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        GiftRequestPage giftRequest = new GiftRequestPage();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileTC1508 = "LV_T1508_T1511_T1514_VerifyTheRequiredFieldsValidationsAndHelpTextOfFields";
        private string actualHelpTextGiftValue;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTheRequiredFieldsValidationsAndHelpTextOfFieldsLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1508;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login ");

                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                //Login user
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
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                //Navigate to Gift Request page                
                string giftRequestTitle = giftRequest.GetGiftRequestPageTitleLV();
                string giftRequestTitleExl = ReadExcelData.ReadData(excelPath, "GiftLog", 10);
                Assert.AreEqual(giftRequestTitleExl, giftRequestTitle);
                extentReports.CreateStepLogs("Passed", "Page Title: " + giftRequestTitle + " is diplayed upon click of Gift Request link ");

                //TMTC0011785/T1510 Client Gift Pre-Approval Page Layout and Fields.
                //Validate fields under Gift Billing Details Section
                Assert.True(giftRequest.ValidateFieldsUnderGiftBillingDetailsSectionLV());
                extentReports.CreateStepLogs("Passed", "Fields defined under Gift Billing Details Section are displayed. ");

                //Validate fields under Gift Details Section
                Assert.True(giftRequest.ValidateFieldsUnderGiftDetailsSectionLV());
                extentReports.CreateStepLogs("Passed", "Fields defined under Gift Details Section are displayed. ");

                //Validate fields under Gift Recipients Section
                Assert.True(giftRequest.ValidateFieldsUnderGiftRecipientsSectionLV());
                extentReports.CreateStepLogs("Passed", "Fields defined under Gift Recipients Section are displayed. ");


                //TMTC0011779/T1508	Client Gift Pre-Approval Page – Submit Gift Request - Required Information field validation
                giftRequest.ClickSubmitGiftRequestLV();
                string validationFound = giftRequest.AreRequiredFieldsValidationDisplayedLV(fileTC1508);
                Assert.AreEqual("All errors for required fields are correct", validationFound);
                extentReports.CreateStepLogs("Passed", validationFound);
                string valGiftName= giftRequest.EnterDetailsGiftRequestLV(fileTC1508);
                extentReports.CreateStepLogs("Info", valGiftName+ " Gift Details are entered successfully without adding recipients to selected recipients");

                //TMTC0011788/T1511 Client Gift Pre-Approval Page - Submit Gift Request – Verify At least one recipient required error
                //Click on Submit gift request button
                giftRequest.ClickSubmitGiftRequestLV();
                extentReports.CreateStepLogs("Info", "Click on Submit Gift Request button successfully ");
                string errorRecipientExl = ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLogErrors", 8, 1);
                string errorMsg = giftRequest.GetRecipientErrorMessageLV();
                Assert.AreEqual(errorRecipientExl, errorMsg);
                extentReports.CreateStepLogs("Passed", "Error Message: " + errorMsg + " is diplayed upon submittion of gift request without adding atleast one recipient to selected recipients list ");

                //TMTC0011797/T1514	Client Gift Pre-Approval Page - Help Text for Fields
                int rowCount = ReadExcelData.GetRowCount(excelPath, "HelpText");
                for(int row = 2; row <= rowCount; row++)
                {
                    string valFieldName = ReadExcelData.ReadDataMultipleRows(excelPath, "HelpText", row, 1);
                    string valExpectedHelptext = ReadExcelData.ReadDataMultipleRows(excelPath, "HelpText", row, 2);

                    actualHelpTextGiftValue = giftRequest.GetHelpFieldTextLV(valFieldName);
                    Assert.AreEqual(valExpectedHelptext, actualHelpTextGiftValue);
                    extentReports.CreateStepLogs("Passed", "Help Text for "+ valFieldName+" : "+ actualHelpTextGiftValue + " is displaying on Gift Pre-Approval Page ");
                }

                //TMTC0011782/T1509 Client Gift Pre-Approval Page - Submitted For Look Up - should list Houlihan Employee Record Type.
                //Verify text displaying on Submitted for look up
                String txtFrame = giftRequest.GetLookupSubittedForHeaderLV();
                string infoLookupHeader = ReadExcelData.ReadDataMultipleRows(excelPath, "GiftLogErrors", 9, 1);
                Assert.AreEqual(infoLookupHeader, txtFrame);
                extentReports.CreateStepLogs("Passed", "Text: " + txtFrame + " is displaying on frame ");
                
                //Verify Name Checkbox is selected by default
                Assert.AreEqual(giftRequest.VerifyRadioBtnNameLV(), true);
                extentReports.CreateStepLogs("Passed", "Name checkbox is selected by Default ");

                //Verify when user searched external contacts then zero contacts is retrieved
                string contactName = ReadExcelData.ReadDataMultipleRows(excelPath, "SubmittedFor", 2, 1);
                String txtSrchResults = giftRequest.SearchExternalContctLV(contactName);
                Assert.AreEqual("No records were found based on your criteria", txtSrchResults);
                extentReports.CreateStepLogs("Passed", txtSrchResults+" for search External Contacts");

                //Verify appropriate results are displayed when user searched with partial name of Houlihan Employee
                contactName = ReadExcelData.ReadDataMultipleRows(excelPath, "SubmittedFor", 3, 1);
                string cntemployee = giftRequest.SearchHLEmployeeLV(contactName);
                Assert.IsTrue(cntemployee.Contains(contactName));
                extentReports.CreateStepLogs("Passed", "Record found when user searched with partial name of Houlihan Employee ");

                //Verify appropriate results are displayed when user searched for title
                string contactTitle = ReadExcelData.ReadDataMultipleRows(excelPath, "SubmittedFor", 3, 3);
                string titleSrchResults = giftRequest.SearchWithTitleLV(contactTitle);
                Assert.AreEqual(contactTitle, titleSrchResults);
                extentReports.CreateStepLogs("Passed", "Record found when user searched for Title: "+ titleSrchResults);

                //Verify appropriate results are displayed when user searched for Department
                string contactDept = ReadExcelData.ReadDataMultipleRows(excelPath, "SubmittedFor", 3, 4);
                string DeptSrchResults = giftRequest.SearchWithDeptLV(contactDept);
                Assert.AreEqual(contactDept, DeptSrchResults);
                extentReports.CreateStepLogs("Passed", "Record found when user searched for Department: " + DeptSrchResults);
                
                driver.SwitchTo().DefaultContent();
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.SwitchTo().DefaultContent();
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
