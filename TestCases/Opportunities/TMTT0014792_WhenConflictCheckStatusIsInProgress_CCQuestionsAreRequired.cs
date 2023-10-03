using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunity
{
    class TMTT0014792_WhenConflictCheckStatusIsInProgress_CCQuestionsAreRequired : BaseClass
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
        public void CCQuestionsAreRequired()
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

                //Search for existing opportunity
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 3, 3);
                string message = opportunityHome.SearchOpportunityWithJobTypeAndStge(valJobType, "Low");
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matching to mentioned search criteria are displayed ");                                       

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Open the NBC form and navigate to Opportunity Overview tab
                opportunityHome.SearchOpportunityWithJobTypeAndStge(valJobType, "Low");
                string title = opportunityDetails.ClickNBCFormL();
                Assert.AreEqual("Public Sensitivity",title);
                extentReports.CreateLog("Page with default tab: " + title + " is displayed upon clicking NBC-L form button for Opportunity with Job Type : "+valJobType +" ");

                string fairness = nform.ClickFairnessAdminChecklistTab();
                string admin = nform.ClickAdministrativeTab(); 
                Assert.AreEqual("Administrative", admin);
                extentReports.CreateLog("Page with tab: " + admin + " is displayed upon clicking Administrative tab ");

                //Get CC Status Message
                string msgNotReq = nform.GetCCStatusMessage();
                Assert.AreEqual("Conflicts Check not requested yet - Please return to the Opportunity and request a Conflicts Check.", msgNotReq);
                extentReports.CreateLog("Message: " + msgNotReq + " is displayed when CC is not requested ");
                nform.SwitchFrame();
                usersLogin.UserLogOut();

                //Login as admin, search for same Opportunity and Request for CC
                opportunityHome.SearchOpportunityWithJobTypeAndStge(valJobType, "Low");
                string reqDate = opportunityDetails.SaveRequestDate();
                extentReports.CreateLog("Request Date " + reqDate + " is displayed upon save ");

                //Login as Standard User and validate the user               
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Open the NBC form and navigate to Opportunity Overview tab
                opportunityHome.SearchOpportunityWithJobTypeAndStge(valJobType, "Low");
                opportunityDetails.ClickNBCFormL();
                nform.ClickFairnessAdminChecklistTab();
                nform.ClickAdministrativeTab();

                //Get CC Status Message
                string msgReq = nform.GetCCStatusMessage();
                Assert.AreEqual("In Progress, requested on: "+reqDate, msgReq);
                extentReports.CreateLog("Message: " + msgReq + " is displayed when CC is requested ");

                nform.ClickReviewSubmission();

                //Validate CC mandatory field validations
                string CC1 = nform.GetCC1stValidation();
                Assert.AreEqual("Administrative: Please answer all Conflicts Check Information questions." , CC1);
                extentReports.CreateLog("Validation: " + CC1 + " is displayed ");
               
                string CC2 = nform.GetCC2ndValidation();
                Assert.AreEqual("Administrative: Please answer all Conflicts Check Information questions.", CC2);
                extentReports.CreateLog("Validation: " + CC2 + " is displayed ");

                string CC3 = nform.GetCC2ndValidation();
                Assert.AreEqual("Administrative: Please answer all Conflicts Check Information questions.", CC3);
                extentReports.CreateLog("Validation: " + CC3 + " is displayed ");

                string CC4 = nform.GetCC2ndValidation();
                Assert.AreEqual("Administrative: Please answer all Conflicts Check Information questions.", CC4);
                extentReports.CreateLog("Validation: " + CC4 + " is displayed ");

                string CC5 = nform.GetCC2ndValidation();
                Assert.AreEqual("Administrative: Please answer all Conflicts Check Information questions.", CC5);
                extentReports.CreateLog("Validation: " + CC5 + " is displayed ");

                form.SwitchFrame();
                usersLogin.UserLogOut();

                opportunityHome.SearchOpportunityWithJobTypeAndStge(valJobType, "Low");
                opportunityDetails.UpdateRequestDate();

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
    



