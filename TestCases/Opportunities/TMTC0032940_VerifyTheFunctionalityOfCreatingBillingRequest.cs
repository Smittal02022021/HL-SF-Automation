using NUnit.Framework;
using SalesForce_Project.Pages;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunity
{
    class TMTC0032940_VerifyTheFunctionalityOfCreatingBillingRequest: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        EngagementHomePage engHome = new EngagementHomePage();
        ParentProject project = new ParentProject();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOppCounterparty counterparty = new AddOppCounterparty();       
        AddOpportunityContact addContact = new AddOpportunityContact();
        EngagementDetailsPage engDetails = new EngagementDetailsPage();

        public static string TMTT0017889 = "TMTC0032938_VerifyTheFunctionalityOfCreatingParentProjectAndAssociatingTheEngagementToParentProject.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void FunctionalityOfBillingRequest()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + TMTT0017889;
                string excelPath1 = ReadJSONData.data.filePaths.testData;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");   

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);

                //Login as Financial User and validate the user                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Verify the availability of Opportunity under HL Banker list
                string tagOpp = opportunityHome.ValidateParentProjectUnderHLBanker();
                Assert.AreEqual("Parent Projects", tagOpp);
                extentReports.CreateLog(tagOpp + " is displayed under Home dropdown ");

                //1. TMT0074711_Verify that the "Billing Request" quick link is placed on the Parent Project
                project.ValidateSearchFunctionalityOfParentProject("Updated Project");
                string billing = project.ValidateBillingRequestLink();
                Assert.AreEqual("Billing Requests", billing);
                extentReports.CreateLog("Link "+ billing + " is displayed on the Parent Project ");

                //2. TMT0074713_Verify that the "Billing Request" section is placed on the related tab of the Parent Project.  
                // Covered in previous test case

                //3. TMT0074715_Verify that on clicking Save button of the Billing Request, validation appears for the required fields
                Assert.IsTrue(project.ValidateBillingRequestValidations(), "Verified that displayed mandatory validations are same ");
                extentReports.CreateLog("Displayed mandatory validations of Billing Request are correct ");

                //4. TMT0074717_Verify that if "Accounting Send Final Invoice" is unchecked, Principal/Manager field enabled and becomes required field to enter the name of the deal team
                
                
                
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

    

