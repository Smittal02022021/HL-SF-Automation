using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Globalization;

namespace SalesForce_Project.TestCases.Engagement
{
    class TMTI0017881_ViewCounterparties_MassCheckbox  : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        AddCounterparty counterparty = new AddCounterparty();
        public static string fileTC7877 = "TMTT0017877_LightningEngagement2";

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
                Assert.AreEqual(valUser,stdUser);
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

                //Fill all the details in available columns and Click Cancel button
                string valDate = ReadExcelData.ReadData(excelPath, "Engagement", 3);
                string valType =counterparty.ValidateCancelFunctonalityAfterEnteringValuesForAllColumns(valDate);
                Assert.AreEqual("Capital Provider", valType);
                string valTier = counterparty.GetValueOfTier();
                Assert.AreEqual("A", valTier);
                extentReports.CreateLog("Entered values of the columns are not saved after clicking Cancel button ");

                //Fill all the details in available columns and Click Save button
                counterparty.ClickMassCheckbox();
                string valTypeSave = counterparty.ValidateSaveFunctonalityAfterEnteringValuesForAllColumns(valDate);
                Assert.AreEqual("Capital Provider", valTypeSave);
                string valTierSave = counterparty.GetValueOfTier();
                Assert.AreEqual("A", valTierSave);
                extentReports.CreateLog("Entered values of the columns are saved after clicking Save button ");

                //Validate Delete functionality
                counterparty.ValidateDeleteFunctonalityAfterEnteringValuesForAllColumns();
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
