using AventStack.ExtentReports;
using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunity
{
    class TMTI0027307_27308_27314_27315_VerifyIndustryGroupIsChangedToTECHOnOpportunityAndEngagementManagerpage:BaseClass
    {
       ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTI0027307 = "TMTI0027307_VerifyIndustryGroupIsChangedToTECHOnOpportunityAndEngagementManagerpage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyNewJobTypeOnOppEngManagerPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0027307;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in    
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowIndustryType = ReadExcelData.GetRowCount(excelPath, "IndustryType");                
                extentReports.CreateLog("Verify Industry Group Type in updated on On Opportunity & Engagement Manager Page ");
                extentReports.CreateLog("rowCount " + rowIndustryType);

                //Login as Standard User profile and validate the user
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                for (int row = 2; row <= rowIndustryType; row++)
                {
                    string valIndustryType = ReadExcelData.ReadDataMultipleRows(excelPath, "IndustryType", row, 1);
                    //TMTI0027315	Verify the Industry Group is changed on Opportunity Manager page
                    string pageTitle = opportunityHome.ClickOppManager();
                    Assert.AreEqual("Opportunity Manager", pageTitle);
                    extentReports.CreateLog("Page with title: " + pageTitle + " is displayed ");
                    Assert.IsTrue(randomPages.IsIndustryTypePresentInDropdownOppManager(valIndustryType), "Verify Updated Industry Group Type: TECH is available on Opportunity Manager page ");
                    extentReports.CreateLog("Opportunity with Industry Group Type: " + valIndustryType + " is available on Opportunity Manager page ");

                    //TMTI0027314	Verify "TECH" Industry Group Filter is working on Opportunity Manager page 
                    Assert.IsTrue(randomPages.IsOpportunityFoundWithIndustryType(valIndustryType), "Verify Opportunity with Industry Group Type:" + valIndustryType + " is Found in list on Opportunity Manager page");
                    extentReports.CreateLog("Opportunity with Industry Group Type:" + valIndustryType + " is Found in list on Opportunity Manager page ");

                    //TMTI0027307	Verify the Industry Group is changed on the Engagement Manager page 
                    pageTitle = opportunityHome.ClickEngManager();
                    Assert.AreEqual("Engagement Manager", pageTitle);
                    extentReports.CreateLog("Page with title: " + pageTitle + " is displayed ");
                    Assert.IsTrue(randomPages.IsIndustryTypePresentInDropdownEngManager(valIndustryType), "Verify Updated Industry Group Type: TECH is available on Engagement Manager page ");
                    extentReports.CreateLog("Engagement with Industry Group Type: " + valIndustryType + " is available on Engagement Manager page ");

                    //TMTI0027308 Verify "TECH" Industry Group Filter is working on Engagement Manager page
                    Assert.IsTrue(randomPages.IsEngagementFoundWithIndustryType(valIndustryType), "Verify Engagement with Industry Group Type:" + valIndustryType + " is Found in list on Engagement Manager page");
                    extentReports.CreateLog("Engagement with Industry Group Type:" + valIndustryType + " is Found in list on Engagement Manager page ");

                }

                usersLogin.UserLogOut();
                extentReports.CreateLog("User logged Out ");
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
