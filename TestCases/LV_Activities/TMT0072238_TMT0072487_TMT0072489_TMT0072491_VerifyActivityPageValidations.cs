using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;
using SF_Automation.Pages.Activities;
using SF_Automation.Pages.Contact;

namespace SF_Automation.TestCases.LV_Activities
{
    class TMT0072238_TMT0072487_TMT0072489_TMT0072491_VerifyActivityPageValidations : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_ContactDetailsPage lvContactDetails = new LV_ContactDetailsPage();
        LV_ContactsActivityListPage LV_ContactsActivityList = new LV_ContactsActivityListPage();
        LV_ContactsActivityDetailPage lV_ContactsActivityDetailPage = new LV_ContactsActivityDetailPage();

        LV_AddActivity addActivity = new LV_AddActivity();

        public static string fileTMTC0032668 = "TMTC0032668_VerifyActivityPageValidations";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyActivityPageValidations()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0032668;
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                string extContactName = ReadExcelData.ReadData(excelPath, "Contact", 1);
                string relatedCompany = ReadExcelData.ReadData(excelPath, "Contact", 2);
                string tabName = ReadExcelData.ReadData(excelPath, "Contact", 3);

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
                extentReports.CreateStepLogs("Passed", "Admin User is able to login into SF");

                //Select HL Banker app
                try
                {
                    lvHomePage.SelectAppLV("HL Banker");
                }
                catch(Exception)
                {
                    lvHomePage.SelectAppLV1("HL Banker");
                }

                //Search external contact
                lvHomePage.SearchContactFromMainSearch(extContactName);
                Assert.IsTrue(lvContactDetails.VerifyUserLandedOnCorrectContactDetailsPage(extContactName));
                extentReports.CreateStepLogs("Passed", "User navigated to external contact details page. ");

                //Navigate to Activity tab
                lvContactDetails.NavigateToActivityTabInsideCFFinancialUser();
                Assert.IsTrue(LV_ContactsActivityList.VerifyUserLandsOnActivityTab());
                extentReports.CreateStepLogs("Passed", "User landed on the Activity tab of external contact. ");

                string type = ReadExcelData.ReadData(excelPath, "Activity", 1);
                string subject = ReadExcelData.ReadData(excelPath, "Activity", 2);
                string msg1 = ReadExcelData.ReadData(excelPath, "Validations", 1);
                string msg2 = ReadExcelData.ReadData(excelPath, "Validations", 4);
                string msg3 = ReadExcelData.ReadData(excelPath, "Validations", 5);
                string msg4 = ReadExcelData.ReadData(excelPath, "Validations", 6);

                addActivity.ClickAddActivityBtn();

                //TMT0072487 Verify that validation appears on clicking Save without filling in any details in mandatory fields.
                Assert.IsTrue(addActivity.VerifyValidationsOnClickingSaveWithoutFillingInMandatoryFields(fileTMTC0032668));
                extentReports.CreateStepLogs("Passed", "Validation message appears on clicking Save without filling in any details in mandatory fields.");

                addActivity.CloseTab("Add New Activity");
                addActivity.ClickAddActivityBtn();

                //TMT0072489 Verify that validation appears if the banker selects End Date Time before Start Date Time.
                Assert.IsTrue(addActivity.VerifyValidationMessageThatAppearsIfEndDateIsLessThanFromDate(fileTMTC0032668));
                extentReports.CreateStepLogs("Passed", "Validation message :" + msg2 + " and " + msg3 + " appears if the banker selects End Date Time before Start Date Time.");

                addActivity.CloseTab("Add New Activity");
                addActivity.ClickAddActivityBtn();

                //TMT0072491 Verify that validation appears if the External Contact or Company is not selected while creating an activity.
                Assert.IsTrue(addActivity.VerifyValidationMessageIfBankerTriesToSaveActivityWithoutHLAttendee(fileTMTC0032668, valUser));
                extentReports.CreateStepLogs("Passed", "Validation message :" + msg4 + " appears if Banker tries to create activity without HL Attendee.");

                addActivity.CloseTab("Add New Activity");
                addActivity.ClickAddActivityBtn();

                //TMT0072238 Verify that the validation appears on creating Activity without External Attendee and Company.
                Assert.IsTrue(addActivity.VerifyValidationMessageWithoutExternalAttendeeAndCompany(fileTMTC0032668, extContactName));
                extentReports.CreateStepLogs("Passed", "Validation message :" + msg1 + " appears if Banker tries to create activity without External Attendee and Company.");

                //TC - End
                lvHomePage.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", "Admin User Logged Out from SF Lightning View. ");

                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
            }
        }
    }
}
