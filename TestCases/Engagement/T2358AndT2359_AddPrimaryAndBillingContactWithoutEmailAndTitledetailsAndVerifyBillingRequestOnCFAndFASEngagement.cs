using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
namespace SF_Automation.TestCases.Engagement
{
    class T2358AndT2359_AddPrimaryAndBillingContactWithoutEmailAndTitledetailsAndVerifyBillingRequestOnCFAndFASEngagement : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        UsersLogin usersLogin = new UsersLogin();
        SendEmailNotification notification = new SendEmailNotification();

        public static string fileTC2358 = "T2358_TMTC0003243_AddPrimaryAndBillingContactWithoutEmailAndTitledetails1";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void HLPostTransactionOpp_FieldsAndValues()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2358;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");


                int rowJobType = ReadExcelData.GetRowCount(excelPath, "Engagement");
                Console.WriteLine("rowCount " + rowJobType);
                for (int row = 2; row <= rowJobType; row++)
                {
                    string engName = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 1);
                    string LOB = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 4);

                    if (LOB.Equals("CF"))
                    {
                        //Login as standard User and validate the user
                        string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 5);
                        usersLogin.SearchUserAndLogin(valUser);
                        string caoUser = login.ValidateUser();
                        //Assert.AreEqual(caoUser.Contains(valUser), true);
                        extentReports.CreateLog("User: " + caoUser + " logged in ");
                    }
                    else
                    {
                        //Login as standard User and validate the user
                        string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 5);
                        usersLogin.SearchUserAndLogin(valUser);
                        string caoUser = login.ValidateUser();
                        // Assert.AreEqual(caoUser.Contains(valUser), true);
                        extentReports.CreateLog("User: " + caoUser + " logged in ");
                    }
                    //Search engagement with Engagement number
                    string message = engHome.SearchEngagementWithName(engName);
                    Assert.AreEqual("Record found", message);
                    extentReports.CreateLog("Enagagement with LOB: " + LOB + " is found and Engagement Detail page is displayed ");

                    //Update the Primary and billing contacts for that email and title fields are empty
                    string value = engagementDetails.UpdateEngContact(ReadExcelData.ReadData(excelPath, "Engagement", 2), LOB);
                    Console.WriteLine("value: " + value);
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Engagement", 2), value);
                    extentReports.CreateLog("Primary and billing contacts is updated such that email and title fields are empty ");

                    //Click on Billing request button and validate error message
                    engagementDetails.ClickBillingRequestButton();
                    string contact_validation = engagementDetails.GetContactValidationMessage();
                    Assert.AreEqual("Please enter the title and email for the below contact(s). Contact email address and title are required for billing request submission.", contact_validation);
                    extentReports.CreateLog("Validation: " + contact_validation + " is displayed upon clicking Billing request button ");

                    //Navigate to Engagement details and update Primary and billing contacts
                    string updatedValue = engagementDetails.UpdateEngContact(ReadExcelData.ReadData(excelPath, "Engagement", 3), LOB);
                    Console.WriteLine("updatedValue: " + updatedValue);
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Engagement", 3), updatedValue);
                    extentReports.CreateLog("Primary and billing contacts is updated such that email and title fields are not empty ");

                    //Click on Billing request button and validate the Billing Request Form
                    engagementDetails.ClickBillingRequestButton();
                    string titleBilling = engagementDetails.GetTitleOfSendEmailButton();
                    Assert.AreEqual("Send Email", titleBilling);
                    extentReports.CreateLog("Billing Request Form is displayed upon clicking Billing request button ");
                    usersLogin.UserLogOut();
                }
                usersLogin.UserLogOut();
            }

            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
            }
        }
        [OneTimeTearDown]
        public new void ExtentClose()
        {
            try
            {
                extent.Flush();
            }
            catch (Exception e)
            {
                throw e;
            }
            driver.Quit();
            notification.SendOutlookEmailWithTestExecutionReport("Test Execution Report - Engagement");
        }
    }
}


