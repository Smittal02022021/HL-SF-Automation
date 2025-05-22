using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Globalization;

namespace SF_Automation.TestCases.Engagement
{
    class TMTT0027453_ViewCounterparties_MassCheckbox  : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        AddCounterparty counterparty = new AddCounterparty();
        public static string fileTC7877 = "TMTT0027453_MassCheckbox";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void MassCheckboxOnTheViewCounterpartyPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC7877;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as CAO User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 2);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " is able to login ");

                //Search for Engagement on lightning
                string message = engHome.SearchEngagementWithNumberOnLightning("111861", "Buyside");                   
                 extentReports.CreateLog("Engagement details are displayed upon searching required engagement ");

                //Validate the View Counterparties button
                string viewCounterparty = engagementDetails.ValidateViewCounterpartiesButton("Buyside");
                Assert.AreEqual("View Counterparties", viewCounterparty);
                extentReports.CreateLog("Button with name : " + viewCounterparty + " is displayed on Engagement Details page for Job Type: "+ "Buyside ");
                
                //Click on Lightning Counterparties button, click on Mass Checkbox 
                engagementDetails.ClickViewCounterpartiesButton();
                counterparty.ClickMassCheckbox();               
                string valDate = ReadExcelData.ReadData(excelPath, "Columns", 3);
                string valCompDate = ReadExcelData.ReadData(excelPath, "Columns", 4);

                //Fill all the details in available columns and Click Save button and then reset it
                string val1stDeclined = counterparty.ValidateSaveFunctonalityAfterEnteringValuesForAllColumns(valDate);
                Assert.AreEqual(valCompDate, val1stDeclined);
                string val2ndDeclined = counterparty.GetDeclinedDateOf2ndCounterparty();
                Assert.AreEqual(valCompDate, val2ndDeclined);
                extentReports.CreateLog("Values of Declined date for both the counterparties has been updated ");

                //Validate values for 2nd column
                string val1stInitial = counterparty.GetInitialContactOf1stCounterparty();
                Assert.AreEqual(valCompDate, val1stInitial);
                string val2ndInitial = counterparty.GetInitialContactOf2ndCounterparty();
                Assert.AreEqual(valCompDate, val2ndInitial);
                extentReports.CreateLog("Values of Initial Contact for both the counterparties has been updated ");

                //Validate values for 3rd column
                string val1stSent = counterparty.GetSentTeaserOf1stCounterparty();
                Assert.AreEqual(valCompDate, val1stSent);
                string val2ndSent = counterparty.GetSentTeaserOf2ndCounterparty();
                Assert.AreEqual(valCompDate, val2ndSent);
                extentReports.CreateLog("Values of Sent Teaser for both the counterparties has been updated ");

                //Validate values for 4th column
                string val1stMarkUpSent = counterparty.GetMarkUpSentOf1stCounterparty();
                Assert.AreEqual(valCompDate, val1stMarkUpSent);
                string val2ndMarkUpSent = counterparty.GetMarkUpSentOf2ndCounterparty();
                Assert.AreEqual(valCompDate, val2ndMarkUpSent);
                extentReports.CreateLog("Values of Marp Up Sent for both the counterparties has been updated ");

                //Validate values for 5th column
                string val1stMarkUpRec = counterparty.GetMarkUpRecOf1stCounterparty();
                Assert.AreEqual(valCompDate, val1stMarkUpRec);
                string val2ndMarkUpRec = counterparty.GetMarkUpRecOf2ndCounterparty();
                Assert.AreEqual(valCompDate, val2ndMarkUpRec);
                extentReports.CreateLog("Values of Marp Up Received for both the counterparties has been updated ");

                //Select the mass checkbox and unselect one row
                counterparty.ClickMassCheckbox();
                string valUpdatedDate = ReadExcelData.ReadData(excelPath, "Columns", 14);
                string valUpdCompDate = ReadExcelData.ReadData(excelPath, "Columns", 15);

                string val1stDeclinedUp = counterparty.SelectOnlyOneRowAndValidateSaveFunctionality(valUpdatedDate);
                Assert.AreEqual(valUpdCompDate, val1stDeclinedUp);
                string val2ndDeclinedUp = counterparty.GetDeclinedDateOf2ndCounterparty();
                Assert.AreNotEqual(valUpdCompDate, val2ndDeclined);
                extentReports.CreateLog("Value of Declined date for only 1st Counterparty has been updated ");

                //Validate values for 2nd column
                string val1stInitialUp = counterparty.GetInitialContactOf1stCounterparty();
                Assert.AreEqual(valCompDate, val1stInitialUp);
                string val2ndInitialUp = counterparty.GetInitialContactOf2ndCounterparty();
                Assert.AreEqual(valCompDate, val2ndInitialUp);
                extentReports.CreateLog("Value of Initial Contact for only 1st Counterparty has been updated ");
                extentReports.CreateLog("Values of only selected row have been updated even Mass Checkbox is checked ");
                                
                usersLogin.DiffLightningLogout();   
                driver.Quit();

            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }       
    }
}
