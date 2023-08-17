using SF_Automation.Pages.Common;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SF_Automation.TestCases.Opportunity
{
    class TMTI0056883_71649_VerifyNewJobTypeIsForFVAOnly : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();        
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        
        public static string fileTMTI0056883 = "TMTI0056883_VerifyNewJobTypeIsForFVAOnly";
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
        public void VerifyNewJobTypeIsForFVAOnly()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0056883;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowJRecordType = ReadExcelData.GetRowCount(excelPath, "RecordType");

                //Login as Standard User profile and validate the user
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //TMTI0056883 Verify new job type for LOB other than FVA
                //TMTI0071649 Verify new job type - CVAS-IP Valuation for CF and FR LOB.
                extentReports.CreateLog("Verify the JobTypes are available only for Opportunity LOB: FVA ");
                for (int row = 1; row < rowJRecordType; row++)
                {
                    //Call function to open Add Opportunity Page
                    opportunityHome.ClickOpportunity();
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "RecordType", row,1);
                    opportunityHome.SelectLOBAndClickContinue(valRecordType);

                    //Validating Title of New Opportunity Page
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 100), true);
                    extentReports.CreateLog(driver.Title + " is displayed for Job Type: "+ valRecordType);
                    valJobType = ReadExcelData.ReadData(excelPath, "JobType", row);
                    Assert.IsFalse(opportunityDetails.IsJobTypePresentInDropdownOppDetailPage(valJobType), " Verify " + valJobType + " is present not Present on Opportunity Detail Page for LOB: " + valRecordType + "under Job Type Dropdown ");
                    extentReports.CreateLog(" Job Type: " + valJobType + " is not Found for LOB: "+ valRecordType);
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
