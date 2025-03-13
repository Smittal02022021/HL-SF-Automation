using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class T1967AndT1968_OpportunityComment_ValidationRules_OnlyComplianceUserCanCreateCommentsOfTypeCompliance_Lightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

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
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser2 = login.ValidateUserLightning();
                Assert.AreEqual(stdUser2.Contains(valUser), true);
                extentReports.CreateLog("User: " + valUser + " logged in ");

                //Search for required Opportunity  
                opportunityHome.SearchMyOpportunitiesInLightning("Rockwood - PV 2021", valUser);                                           
                
                //Validate the Compliance Comment's validation message
                opportunityDetails.AddOppCommentaAndValidate("Compliance");
                string messageComp = opportunityDetails.GetComplianceCommentsMessageL();
                Assert.AreEqual("Comment Type\r\nOnly Compliance users can create comments of type Compliance.", messageComp);
                extentReports.CreateLog("Message: " + messageComp + " is displayed with Standard User: "+stdUser2 +" ");

                //Logout of Standard user and login with Compliance User
                usersLogin.DiffLightningLogout();
                string valUser2 = ReadExcelData.ReadData(excelPath, "Users", 2);
                usersLogin.SearchUserAndLogin(valUser2);
                string stdUser = login.ValidateUserLightning();
                Console.WriteLine("stdUser" + stdUser);
                Assert.AreEqual(stdUser.Contains(valUser2), true);
                extentReports.CreateLog("User: " + valUser2 + " logged in ");

                //Search for required Opportunity
                opportunityHome.SearchMyOpportunitiesInLightning("Rockwood - PV 2021", valUser2);

                opportunityDetails.AddOppCommentaAndValidate("Compliance");
                string addedComment = opportunityDetails.GetOppCommentsL();
                Assert.AreEqual("Testing", addedComment);
                extentReports.CreateLog("Added comment: " + addedComment + " of type compliance is saved successfully with Compliance user ");

                //Delete the added comment
                string comment =opportunityDetails.DeleteAddedOppComments();
                Assert.AreEqual("(0)", comment);
                extentReports.CreateLog("Added comment is deleted successfully ");
                
                //Validate any other type of comment with Compliance User
                opportunityDetails.AddOppCommentaAndValidate("Internal");
                string messageInt = opportunityDetails.GetCommentsSectionMessage();
                Assert.AreEqual("Comment Type\r\nCompliance users can only create Compliance comments.", messageInt);
                extentReports.CreateLog("Message: " + messageInt + " is displayed while adding any other type of comment with Compliance User ");

                usersLogin.DiffLightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
            }
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            usersLogin.UserLogOut();
            usersLogin.UserLogOut();
        }
    }
}

    

