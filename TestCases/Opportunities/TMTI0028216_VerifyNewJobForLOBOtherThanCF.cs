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

namespace SF_Automation.TestCases.Opportunities
{
    class TMTI0028216_VerifyNewJobForLOBOtherThanCF:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        public static string fileTMTI0028216 = "TMTI0028216_VerifyNewJobForLOBOtherThanCF";
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
        public void VerifyNewJobTypeIsForCFOnly()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0028216;
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

                extentReports.CreateLog("Verify the JobTypes are available only for Opportunity LOB: CF ");
                int rowJRecordType = ReadExcelData.GetRowCount(excelPath, "RecordType");
                for (int row = 1; row < rowJRecordType; row++)
                {
                    //Call function to open Add Opportunity Page
                    opportunityHome.ClickOpportunity();
                    string valRecordType = ReadExcelData.ReadData(excelPath, "RecordType", row);
                    opportunityHome.SelectLOBAndClickContinue(valRecordType);

                    //TMTI0028216	Verify new job for LOB other than CF
                    //TMTI0055392	Verify new job for LOB other than CF
                    int rowJobType = ReadExcelData.GetRowCount(excelPath, "JobType");
                    for (int rec = 1; rec < rowJobType; rec++)
                    {
                        //Validating Title of New Opportunity Page
                        Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 100), true);
                        extentReports.CreateLog(driver.Title + " is displayed for Job Type: " + valRecordType + " ");
                        valJobType = ReadExcelData.ReadData(excelPath, "JobType", rec);
                        Assert.IsFalse(opportunityDetails.IsJobTypePresentInDropdownOppDetailPage(valJobType), " Verify " + valJobType + " is present not Present on Opportunity Detail Page for LOB: " + valRecordType + "under Job Type Dropdown ");
                        extentReports.CreateLog(" Job Type: " + valJobType + " is not Found for LOB: " + valRecordType + " ");
                    }
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
