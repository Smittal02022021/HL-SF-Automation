using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class TMTT0013974_VerifyThePopUpLoadsToSelectTheFormType : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        CNBCForm form = new CNBCForm();
        NBCForm nform = new NBCForm();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileCNBC = "TMTT0013974_VerifyThePopUpLoadsToSelectTheFormType.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void CNBCFormSubmitforReview()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileCNBC;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                Console.WriteLine("rowCount " + rowJobType);

                for (int row = 2; row <= rowJobType; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    //Search for created opportunity
                   string message= opportunityHome.SearchOpportunityWithJobTypeAndStge(valJobType, "Low");
                    //opportunityHome.SearchMyOpportunitiesInLightning("78446",valUser);                    
                    extentReports.CreateLog("Records matching to mentioned search criteria are displayed ");
                    
                    if (valJobType.Equals("Illiquid Financial Assets")|| valJobType.Equals("Buyside & Financing Advisory"))
                    {
                        string title = opportunityDetails.ClickNBCForm();
                        //Validate the pop up
                        Assert.AreEqual("Please select a form type.", title);
                        extentReports.CreateLog("Page with title: " + title + " is displayed upon clicking NBC-L form button for Opportunity with Job Type : " + valJobType + " ");

                        //Validate Radio buttons
                        string MA = opportunityDetails.ValidateMARadioButton();
                        Assert.AreEqual("M&A", MA);
                        extentReports.CreateLog("Radio button with name: " + MA + " is displayed on the pop up ");

                        string CapitalMkt = opportunityDetails.ValidateCapitalMktRadioButton();
                        Assert.AreEqual("Capital Market", CapitalMkt);
                        extentReports.CreateLog("Radio button with name: " + CapitalMkt + " is displayed on the pop up ");
                    }
                    else
                    {
                        string title = opportunityDetails.ClickNBCFormL();
                        extentReports.CreateLog("Page with default tab: " + title + " is displayed upon clicking NBC-L form button for Opportunity with Job Type : "+valJobType +" ");
                        
                    }
                    form.SwitchFrameClassic();
                }
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
    



