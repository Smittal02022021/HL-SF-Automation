using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SF_Automation.Pages.Opportunity;
using AventStack.ExtentReports.Gherkin.Model;

namespace SF_Automation.TestCases.Opportunity
{
    class TMTI0056873_TMTT0030610_VerifyUserCanEditAnyOtherJobTypesToNewJobTypeForExistingFVAOpportunity _VerifyUserCanEditAnyOtherJobTypesToNewJobTypeForExistingFVAOpportunity : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTMTI0056873 = "TMTI0056873_VerifyUserCanEditAnyOtherJobTypesToNewJobTypeForExistingFVAOpportunity";
       
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]

        public void ValidateJobTypesIsUpdatedWithNewJobTypeFVAOppDetailPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0056873;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                                
                //Login as Standard User profile and validate the user
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");
                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                string valJobType = ReadExcelData.ReadData(excelPath, "AddOpportunity",3);
                //Calling AddOpportunities function
                string value = addOpportunity.AddOpportunities(valJobType, fileTMTI0056873);
                extentReports.CreateLog("Opportunity : " + value + " is created ");

                //Call function to enter Internal Team details and validate opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTMTI0056873);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details  
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                //Create External Primary Contact         
                String valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                String valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                //string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                addOpportunityContact.CreateContact(fileTMTI0056873, valContact, valRecordType, valContactType);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                
                //TMTI0056873	Verify user is able to edit any other Job types to new job type for existing opportunity
                //TMTI0071642 Verify the user is able to edit any other Job types to a new job type for an existing opportunity.
                int rowJobType = ReadExcelData.GetRowCount(excelPath, "NewJobType");
                extentReports.CreateLog("Job Types Count " + rowJobType + " ");
                for (int row = 1; row < rowJobType; row++)
                {
                    string oldJobType = opportunityDetails.GetOppJobType();
                    string jobTypeExl = ReadExcelData.ReadData(excelPath, "NewJobType", row);
                    opportunityDetails.UpdateJobType(jobTypeExl);
                    string updatedJobType = opportunityDetails.GetOppJobType();
                    Assert.AreEqual(jobTypeExl, updatedJobType);
                    extentReports.CreateLog("Job Type: " + oldJobType + " is updated with new JobType: " + updatedJobType+" ");
                }
                usersLogin.UserLogOut();
                extentReports.CreateLog("User: " + stdUser + " logged Out ");
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
