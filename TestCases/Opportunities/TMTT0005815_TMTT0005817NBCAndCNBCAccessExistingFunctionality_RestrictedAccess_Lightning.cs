using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class TMTT0005815_TMTT0005817NBCAndCNBCAccessExistingFunctionality_RestrictedAccess_Lightning : BaseClass
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

        public static string fileCNBC = "TMTT0005815_NBCAndCNBCAccessExistingFunctionality_Lightning.xlsx";

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

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");                              

                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 3, 3);

                //Search for created opportunity
                string message = opportunityHome.SearchOpportunityWithJobTypeAndStge(valJobType, "Low");
                Assert.AreEqual("Record found", message);
                extentReports.CreateLog("Records matching to mentioned search criteria are displayed ");

                string title = opportunityDetails.ValidateNBCFormAdmin();
                Assert.AreEqual("Public Sensitivity", title);
                extentReports.CreateLog("Tab with name: " + title + " is displayed on Opportunity details with Job Type : " + valJobType + " for Admin user " );

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "Users");
                Console.WriteLine("rowCount " + rowJobType);

                for (int row = 2; row <= rowJobType; row++)
                {
                    string valJobType1 = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users",row, 1);
                   
                    if (valUser.Equals("Derek Janisch"))
                    {
                        usersLogin.SearchUserAndLogin(valUser);
                        string stdUser = login.ValidateUserLightning();
                        Assert.AreEqual(stdUser.Contains(valUser), true);
                        extentReports.CreateLog("User: " + stdUser + " logged in ");
                        opportunityHome.SearchMyOpportunitiesInLightning("[Ten10 Sell-side Dec-23]", valUser);
                        string title1 = opportunityDetails.ValidateNBCFormButtonLightning(valUser);
                        //Assert.AreEqual("NBC Form L", title1);
                        extentReports.CreateLog("Message : " + title1 + " is displayed upon clicking NBC button" + " for Non-CF LOB CAOs user: " + valUser);
                        usersLogin.DiffLightningLogout();
                    }
                else if(valUser.Equals("Mark Fisher")|| valUser.Equals("Brian Miller"))
                    {
                        usersLogin.SearchUserAndLogin(valUser);
                        string stdUser = login.ValidateUserLightningExcep();
                        Assert.AreEqual(stdUser.Contains(valUser), true);
                        extentReports.CreateLog("User: " + stdUser + " logged in ");
                        opportunityHome.SearchMyOpportunitiesInLightning("[Ten10 Sell-side Dec-23]", valUser);
                        string title1 = opportunityDetails.ValidateNBCFormButtonLightning(valUser);
                        //Assert.AreEqual("NBC Form L", title1);
                        extentReports.CreateLog("Tab : " + title1 + " is displayed upon clicking NBC button" + " for Non-CF LOB CAOs user: " + valUser);
                        usersLogin.DiffLightningLogout();
                    }
                    else
                    {
                        usersLogin.SearchUserAndLogin(valUser);
                        string stdUser = login.ValidateUser();
                        Assert.AreEqual(stdUser.Contains(valUser), true);
                        extentReports.CreateLog("User: " + stdUser + " logged in ");
                        //Search for created opportunity
                        string message1 = opportunityHome.SearchOpportunityWithJobTypeAndStge(valJobType, "Low");
                        Assert.AreEqual("Record found", message1);
                        extentReports.CreateLog("Records matching to mentioned search criteria are displayed ");

                        if (valUser.Equals("Rob Oudman"))
                        {
                            string title1 = opportunityDetails.ClickNBCFormL();
                            Assert.AreEqual("NBC button is not displayed", title1);
                            extentReports.CreateLog("Button with name: " + title1 + " is not displayed on Opportunity details with Job Type : " + valJobType + " and for non deal team member: " + valUser);
                            usersLogin.UserLogOut();

                        }
                        else if (valUser.Equals("Carlyn Verduin"))
                        {
                            string title1 = opportunityDetails.ClickNBCFormL();
                            Assert.AreEqual("NBC button is not displayed", title1);
                            extentReports.CreateLog("Button with name: " + title1 + " is not displayed on Opportunity details with Job Type : " + valJobType + " and for delegates of non deal team member: " + valUser);
                            usersLogin.UserLogOut();

                        }
                       
                        else if (valUser.Equals("Emre Abale"))
                        {
                            string title1 = opportunityDetails.ClickNBCFormL();
                            Assert.AreEqual("Public Sensitivity", title1);
                            extentReports.CreateLog("Tab with name: " + title1 + " is displayed upon clicking NBC button for delegate of deal team member: " + valUser);
                            usersLogin.DiffLightningLogout();
                        }
                        //else if (valUser.Equals("Brian Miller"))
                        //{
                        //    string title1 = opportunityDetails.ClickNBCFormL();
                        //    Assert.AreEqual("Public Sensitivity", title1);
                        //    extentReports.CreateLog("Tab with name: " + title1 + " is displayed upon clicking NBC button for CAO of CF group: " + valUser);
                        //    usersLogin.DiffLightningLogout();
                        //}
                        else
                        {
                            string title1 = opportunityDetails.ClickNBCFormL();
                            Assert.AreEqual("NBC Form L", title1);
                            extentReports.CreateLog("Button with name: " + title1 + " is displayed on Opportunity details with Job Type : " + valJobType + " and for user: " + valUser);
                        }
                    }                    
                }               
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
    



