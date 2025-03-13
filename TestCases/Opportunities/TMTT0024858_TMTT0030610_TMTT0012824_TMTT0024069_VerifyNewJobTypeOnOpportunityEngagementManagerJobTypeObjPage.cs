using AventStack.ExtentReports.Gherkin.Model;
using Microsoft.Office.Interop.Excel;
using MongoDB.Driver;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Reflection;

namespace SF_Automation.TestCases.Opportunities
{
    class TMTT0024858_TMTT0030610_TMTT0012824_TMTT0024069_VerifyNewJobTypeOnOpportunityEngagementManagerJobTypeObjPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();    
        UsersLogin usersLogin = new UsersLogin();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTI0056877 = "TMTI0056875_56876_56877_VerifyNewJobTypeOnOpportunityEngagementManagerJobTypeObjPage";

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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0056877;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "JobType");
                extentReports.CreateLog("rowCount " + rowJobType+ " ");

                for (int row = 2; row <= rowJobType; row++)
                {
                    extentReports.CreateLog("Verify New JobType On Opportunity & Engagement Manager Page For FVA RecordTypes ");
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 1); 

                    //Login as Standard User profile and validate the user
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadDataMultipleRows(excelPath, "Users",row, 1));
                    string stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users",row, 1)), true);
                    extentReports.CreateLog("User: " + stdUser + " logged in ");

                    //Clicking on Opportunity Manager link and Validate the title of page
                    //TMTI0071640 Verify the new Job Type on the Opportunity Manager page
                    //TMTI0056877 Verify the new Job Type in Opportunity Manager page
                    //TMTI0028215 Verify the new Job Type in Opportunity Manager page
                    //TMTI0055397 Verify the new Job Type in Opportunity Manager page
                    
                    string pageTitle = opportunityHome.ClickOppManager();
                    Assert.AreEqual("Opportunity Manager", pageTitle);
                    extentReports.CreateLog("Page with title: " + pageTitle + " is displayed ");
                    Assert.IsTrue(randomPages.IsJobTypeVailableOnPage(pageTitle, valJobType),"Verify Opportunity with New Job Type is available on Opportunity Manager page ");
                    extentReports.CreateLog("Opportunity with New Job Type: " + valJobType+" is available on Opportunity Manager page ");

                    //Clicking on Engagement Manager link and Validate the title of page
                    //TMTI0071644 Verify the new Job Type on the Engagement Manager page.
                    //TMTI0056875	Verify the new Job Type in Engagement Manager page
                    //TMTI0028199 Verify the new Job Type in Engagement Manager page
                    //TMTI0055403 Verify the new Job Type in Engagement Manager page
                    pageTitle = opportunityHome.ClickEngManager();
                    Assert.AreEqual("Engagement Manager", pageTitle);
                    extentReports.CreateLog("Page with title: " + pageTitle + " is displayed ");
                    Assert.IsTrue(randomPages.IsJobTypeVailableOnPage(pageTitle, valJobType), "Verify Engagement with New Job Type is available on Engagement Manager page ");
                    extentReports.CreateLog("Engagement with New Job Type: " + valJobType + " is available on Engagement Manager page ");
                    usersLogin.UserLogOut();
                    extentReports.CreateLog("User: " + stdUser + " logged Out ");

                    //System Admin: Verify New Job Type is present on Job Type Object page
                    //TMTI0056876 Verify New / Updated Job Type And Job Code Under Job Type Object/tab
                    //TMTI0071641 Verify New / Updated Job type and Job code under Job Type Object/tab.
                    //TMTI0028200 Verify New / updated Job type and Job code under Job type Object/tab
                    //TMTI0055398 Verify New/updated Job type and code under Job type Object/tab
                    extentReports.CreateLog("Verify New Job Type is present on Job Type Object page as System Administrator ");
                    string valView = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 3);
                    pageTitle = randomPages.selectJobTypesObject(valView);
                    extentReports.CreateLog("Page with title: " + pageTitle + " is displayed with View: " + valView + " ");
                    
                    string valJobCode = ReadExcelData.ReadDataMultipleRows(excelPath, "JobType", row, 2);
                    extentReports.CreateLog("Page with title: " + pageTitle + " is displayed ");
                    Assert.IsTrue(randomPages.IsJobTypeVailableOnPage(pageTitle, valJobType), "Verify New Job Type is available in Job Type Object List ");
                    extentReports.CreateLog("New Job Type: " + valJobType + " is available in Job Type Object List ");

                    Assert.IsTrue(randomPages.IsJobCodeAvailable(valJobCode), "Verify New Job Type Code is available in Job Type Object List ");
                    extentReports.CreateLog("New Job Code: " + valJobCode + " is available in Job Type Object List for Jon Type: "+ valJobType+" ");

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
