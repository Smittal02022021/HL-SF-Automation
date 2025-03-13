using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class ZObsolated_T1967AndT1968_OpportunityComment_ValidationRules_OnlyComplianceUserCanCreateCommentsOfTypeCompliance : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTC2214 = "T1967AndT1968_OpportunityComment";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ImportPositionsWithoutTeamMembers()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2214;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);                
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "User: " + valUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToClassicView();
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Search for required Opportunity
                opportunityHome.SearchOpportunity("Rockwood - PV 2021");
                opportunityDetails.AddOppComments("Compliance");
                string message = opportunityDetails.GetCommentsSectionMessage();
                Assert.AreEqual("Error: Only Compliance users can create comments of type Compliance.", message);
                extentReports.CreateLog("Message: " + message + " is displayed with Standard User ");

                //Logout of Standard user and login with Compliance User
                usersLogin.UserLogOut();                
                string valCompUser = ReadExcelData.ReadData(excelPath, "Users", 2);
                homePage.SearchUserByGlobalSearchN(valCompUser);
                extentReports.CreateStepLogs("Info", "User: " + valCompUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToClassicView();
                string compUser = login.ValidateUser();
                Assert.AreEqual(compUser.Contains(valCompUser), true);
                extentReports.CreateLog("User: " + compUser + " logged in ");

                //Search for required Opportunity
                opportunityHome.SearchOpportunity("Rockwood - PV 2021");
                opportunityDetails.AddOppComments("Compliance");
                string addedComment = opportunityDetails.GetAddedComment();
                Assert.AreEqual("Testing", addedComment);
                extentReports.CreateLog("Added comment: " + addedComment + " of type compliance is saved successfully with Compliance user ");

                //Delete the added comment
                string comment =opportunityDetails.DeleteAddedOppComments();
                Assert.AreEqual("Opportunity's existing comments are deleted", comment);
                extentReports.CreateLog("Added comment is deleted successfully ");
                
                //Validate any other type of comment with Compliance User
                opportunityDetails.AddOppComments("Internal");
                string message1 = opportunityDetails.GetCommentsSectionMessage();
                Assert.AreEqual("Error: You must enter a value", message1);
                extentReports.CreateLog("Message: " + message1 + " is displayed while adding any other type of comment with Compliance User ");
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            usersLogin.UserLogOut();
            driver.Quit();
        }
    }
}

    

