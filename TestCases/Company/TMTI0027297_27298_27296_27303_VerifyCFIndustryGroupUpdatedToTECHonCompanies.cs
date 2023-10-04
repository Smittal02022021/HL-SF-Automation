using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.Companies;

namespace SF_Automation.TestCases.Companies
{
    class TMTI0027297_27298_27296_27303_VerifyCFIndustryGroupUpdatedToTECHonCompanies : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetails = new CompanyDetailsPage();


        public static string fileTMTI0027313 = "TMTI0027313_VerifyCFIndustryGroupUpdatedToTECHonOpportunities";
       
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ValidateCompaniesIndstryTypesUpdatedForCF()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0027313;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowIndustryType = ReadExcelData.GetRowCount(excelPath, "IndustryType");

                //Login as Standard User profile and validate the user
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                extentReports.CreateLog("Verify the Industry Type is present on Opportunity Home Page ");
                companyHome.ClickCompaniesTabAdvanceSearch();

                //TMTI0027297 Verify the CF Industry Group(Drop-down) Changes TECH is updated in place of TMT & D&A on Companies  Home Page
                string industryGroupExl = ReadExcelData.ReadData(excelPath, "IndustryType", 1);
                extentReports.CreateLog("User is on Companies Home Page ");
                Assert.IsTrue(opportunityHome.IsIndustryTypePresentInDropdownHomePage(industryGroupExl), " Verify " + industryGroupExl + " is present on Engagement Home Page under Industry Group Dropdown ");
                extentReports.CreateLog(" Industry Group: " + industryGroupExl + " Found on Companies Home Page ");
                
                //Search via New Industry Group Type
                //TMTI0027298 Verify User is able to search Companies with Industry Group TECH is on Companies Home page
                Assert.AreEqual("Record found", companyHome.SearchCompanyWithIndustryType(industryGroupExl));
                extentReports.CreateLog("Company is displayed on Companies Home Page with Industry Group: " + industryGroupExl+" ");

                //Add New Company
                //TMTI0027296 Verify the CF Industry Group Changes TECH is updated in place of TMT & D&A While Creating Companies 
                int countCompanyRecordTypeExl = ReadExcelData.GetRowCount(excelPath, "CompanyType");
                for(int record=2; record <= countCompanyRecordTypeExl; record++)
                {
                    string companyRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyType", record, 1);
                    companyHome.ClickAddCompany();
                    companyHome.CreateCompany(companyRecordTypeExl);
                    
                    extentReports.CreateLog("Company Created with Record Type: " + companyRecordTypeExl+" ");
                    Assert.IsTrue(companyDetails.IsIndustryTypePresent(industryGroupExl),"Verify New Industry Group Type:TECH is available on Company Detail page");
                    extentReports.CreateLog("Industry Group Type: TECH is available on Company Detail page ");

                    //TMTI0027303	Verify the Industry Name is updated for Coverage Team page
                    string quickLinkExl = ReadExcelData.ReadData(excelPath, "QuickLink", 1);
                    companyDetails.ClickDetailPageQuickLink(quickLinkExl);
                    extentReports.CreateLog("Company Created with Record Type: ");
                    companyDetails.ClickNewCoverageTeamButton();
                    extentReports.CreateLog("Click New Coverage Team button from Company page ");
                    
                    Assert.IsTrue(companyDetails.IsIndustryTypePresentonCoverageTeam(industryGroupExl),"Verify Industry Group is updated on Company's Coverage Team Type DropDown ");
                    extentReports.CreateLog("Industry Group: "+ industryGroupExl+" is updated on Company's Coverage Team Type DropDown ");
                }
                

                usersLogin.UserLogOut();
                extentReports.CreateLog("User: " + stdUser + " logged Out ");
                driver.Quit();

            }
            catch (Exception ex)
            {
                extentReports.CreateLog(ex.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
