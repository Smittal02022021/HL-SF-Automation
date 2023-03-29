using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;

namespace SF_Automation.TestCases.Opportunity
{
    class TMTI0056880_TMTI0056878_ValidateNewJobTypesAvailability: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engagementHome = new EngagementHomePage();


        public static string fileTC1432 = "TMTI0056880_ValidateNewJobTypesAvailability";
        string valJobType;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ValidateNewJobTypesAvailabilityForFVA()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1432;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "JobType");
                Console.WriteLine("rowCount " + rowJobType);
                //Login as Standard User profile and validate the user
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                extentReports.CreateLog("Verify the JobTypes are present on Opportunity Home Page ");
                //Call function to open Opportunity Home Page
                // TMTI0056880- Verify the availability of new Job Types in Job Type column on Opportunity Search page
                opportunityHome.ClickOpportunityTabAdvanceSearch();
                for (int row = 1; row <rowJobType; row++)
                {                    
                    valJobType = ReadExcelData.ReadData(excelPath, "JobType", row);                 
                    Assert.IsTrue(opportunityHome.IsJobTypePresentInDropdown(valJobType)," Verify "+ valJobType+" is present on Opportunity Home Page under Job Type Dropdown ");
                    extentReports.CreateLog(" Job Type: " + valJobType + " Found ");
                }
                extentReports.CreateLog("Desired JobTypes are present on Opportunity Home page ");
                extentReports.CreateLog("Verify the JobTypes are present on Engagement Home Page ");
                //Call function to open Engagement Home Page
                //TMTI0056878- Verify the availability of new Job Types in Job Type column on Engagement Search page
                engagementHome.ClickEngagementTabAdvanceSearch();
                for (int row = 1; row < rowJobType; row++)
                {
                    valJobType = ReadExcelData.ReadData(excelPath, "JobType", row);
                    Assert.IsTrue(opportunityHome.IsJobTypePresentInDropdown(valJobType), " Verify " + valJobType + " is present on Engagement Home Page under Job Type Dropdown ");
                    extentReports.CreateLog(" Job Type: " + valJobType + " Found ");
                }
                extentReports.CreateLog("Desired JobTypes are present on Engagement Home Page ");

                usersLogin.UserLogOut();                
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
