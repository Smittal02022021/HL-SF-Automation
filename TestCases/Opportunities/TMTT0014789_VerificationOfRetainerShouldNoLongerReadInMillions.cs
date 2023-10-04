using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunity
{
    class TMTT0014789_VerificationOfRetainerShouldNoLongerReadInMillions : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        CNBCForm form = new CNBCForm();
        NBCForm nform = new NBCForm();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileCNBC = "TMTT0013974_VerifyThePopUpLoadsToSelectTheFormType.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerificationOfRetainer()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileCNBC;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 3, 3);
              
                //Search for created opportunity
                string message= opportunityHome.SearchOpportunityWithJobTypeAndStge(valJobType, "Low");
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matching to mentioned search criteria are displayed ");

                //Open the NBC form
                string title = opportunityDetails.ClickNBCFormL();
                extentReports.CreateLog("Page with default tab: " + title + " is displayed upon clicking NBC-L form button for Opportunity with Job Type : "+valJobType +" ");

                string txtFees = nform.ClickFeesTab();
                Assert.AreEqual("Fees", txtFees);
                extentReports.CreateLog("Tab with name " + txtFees + " is displayed upon clicking Fees tab. ");

                string txtRetainer = nform.GetLabelRetainer();
                Assert.AreEqual("Retainer", txtRetainer);
                extentReports.CreateLog("Field with name: " + txtRetainer + " is displayed without (MM) ");

                string value = nform.UpdateRetainerAndValidate();                
                Assert.AreEqual("EUR 1,000,000.00", value);
                extentReports.CreateLog("Retainer Value: " + value + " is displayed as it is saved ");

                string EstFee = nform.GetEstimatedTotalFee();
                Console.WriteLine("EstFee: " + EstFee);
                Assert.AreEqual("EUR 20.0", EstFee);
                extentReports.CreateLog("Estimated Total Fee: " + EstFee + " is displayed in MM ");                                

                form.SwitchFrame();
                
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
    



