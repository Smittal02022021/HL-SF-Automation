using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using AventStack.ExtentReports.Gherkin.Model;
using Microsoft.Office.Interop.Excel;

namespace SF_Automation.TestCases.Opportunity
{
    class TMTI0056880_56878_28219_28203_28208_28214_ValidateNewJobTypesAvailability: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        EngagementHomePage engagementHome = new EngagementHomePage();
        RandomPages randomPages = new RandomPages();


        public static string fileTMTI0056880 = "TMTI0056880_ValidateNewJobTypesAvailability";
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
        public void ValidateNewJobTypesAvailability()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0056880;
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
                //TMTI0028219 Verify the availability of new Job Types in Job Type Picklist on Opportunity Search page

                opportunityHome.ClickOpportunityTabAdvanceSearch();
                for (int row = 1; row <rowJobType; row++)
                {                    
                    valJobType = ReadExcelData.ReadData(excelPath, "JobType", row);                 
                    Assert.IsTrue(randomPages.IsJobTypePresentInDropdownHomePage(valJobType)," Verify "+ valJobType+" is present on Opportunity Home Page under Job Type Dropdown ");
                    extentReports.CreateLog(" Job Type: " + valJobType + " Found ");

                    //TMTI0028203	Verify the search opportunity functionality with new Job Types on the Opportunity Search page
                    Assert.AreEqual("Record found", opportunityHome.SearchOpportunityWithJobType(valJobType));
                    extentReports.CreateLog("Opportunity with Job Type: " + valJobType + " is Found ");
                }
                
                extentReports.CreateLog("Verify the JobTypes are present on Engagement Home Page ");
                
                //Call function to open Engagement Home Page
                //TMTI0056878- Verify the availability of new Job Types in Job Type column on Engagement Search page
                //TMTI0028208	Verify the availability of new Job in Job Type Picklist on the Engagement Search page 

                engagementHome.ClickEngagementTabAdvanceSearch();
                for (int row = 1; row < rowJobType; row++)
                {
                    valJobType = ReadExcelData.ReadData(excelPath, "JobType", row);
                    Assert.IsTrue(randomPages.IsJobTypePresentInDropdownHomePage(valJobType), " Verify " + valJobType + " is present on Engagement Home Page under Job Type Dropdown ");
                    extentReports.CreateLog(" Job Type: " + valJobType + " Found ");

                    //TMTI0028214	Verify the search Engagement functionality with new Job Types on the Engagement Search page
                    Assert.AreEqual("Record found", engagementHome.SearchEngagemenstWithJobType(valJobType));
                    extentReports.CreateLog("Engagement with Job Type: " + valJobType + " is Found ");
                }
                extentReports.CreateLog("Desired JobTypes are present on Engagement Home Page ");

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
