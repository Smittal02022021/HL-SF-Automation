using SF_Automation.Pages.Common;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SF_Automation.TestData;
using SF_Automation.Pages.Company;

namespace SF_Automation.TestCases.Companies
{
    class TMTI0027300_TMTI0027318_VerifyIndustryTypeIsUpdatedForCampaignsAndCapIQCompanies:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        CampaignHomePage campaignHome = new CampaignHomePage();
        RandomPages randomPages = new RandomPages();
        CapIQCompaniesHomePage capIQCompaniesHome = new CapIQCompaniesHomePage();

        public static string fileTMTI0027300 = "TMTI0027300_VerifyIndustryTypeIsUpdatedForCampaignsAndCapIQCompanies";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyIndustryTypeIsUpdatedForCampaignsAndCapIQCompanies()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0027300;               

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                // Calling Login function                
                login.LoginApplication();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //TMTI0027300	Verify the Industry Name is updated for Campaigns
                extentReports.CreateLog("Verify Industry Group Type is updated on Campaign Home List ");
                campaignHome.ClickCampaignTab();
                extentReports.CreateLog("User is on Campaigns Home Page ");

                string IndustryTypeExl= ReadExcelData.ReadData(excelPath, "IndustryType", 1);
                string CampaignsTypeExl= ReadExcelData.ReadData(excelPath, "CampaignsType", 1);
                Assert.IsTrue(randomPages.IsIndustryGroupAvailableOnCampaignPage(CampaignsTypeExl, IndustryTypeExl));
                extentReports.CreateLog("Campaign with Industry Group Type: "+ IndustryTypeExl+" is found in Campaign List ");

                //TMTI0027318	Verify the Industry Name is updated for CapIQCompanies page
                extentReports.CreateLog("Verify Industry Group Type is updated on CapIQ Company page ");
                capIQCompaniesHome.ClickCapIQCompanyModule();
                Assert.IsTrue(capIQCompaniesHome.IsIndustryGroupAvailableOnNewCapIQCompanyPage(IndustryTypeExl), "Verify Industry Group Type is updated on CapIQ Company page");
                extentReports.CreateLog("Industry Group Type: " + IndustryTypeExl + " is updated on CapIQ Company Page ");

                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception ex)
            {
                extentReports.CreateLog(ex.Message);
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateLog("Browser Closed ");

            }
        }
    }
}
