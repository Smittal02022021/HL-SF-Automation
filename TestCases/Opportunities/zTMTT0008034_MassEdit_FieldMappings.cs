using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class zTMTT0008034_MassEdit_FieldMappings : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string file8026 = "TMTT0008026_AdditionalClientAndSubject_MassEdit_EditFunctionalities3.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void AdditionalClientAndSubject_ClientSubjectFunctionalities()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + file8026;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User profile and validate the user
                string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 32);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Search FR Opportunity 
                string opp = opportunityHome.SearchOpportunityWithJobTypeAndStge("Creditor Advisors","High");
                Assert.AreEqual("Record found",opp);
                extentReports.CreateLog("Records matches to LOB-FR are found and Opportunity Detail page is displayed ");

                //Get currency of Total Debt
                string debtCurrency= opportunityDetails.GetTotalDebtCurrency();
                string dCurrency = debtCurrency.Substring(0, 3);
                extentReports.CreateLog("Total Debt Currency: "+debtCurrency + " is displayed ");

                //Get Total Debt MM value
                string totalDebt = opportunityDetails.GetTotalDebtMM();
                string debt = totalDebt.Substring(0, 4);
                extentReports.CreateLog("Total Debt MM's valie: " + totalDebt + " is displayed in Opportunity details page ");

                //Validate the title of page upon clicking Mass Edit Records button
                string titeMassEditPage = opportunityDetails.ClickMassEditRecordsButton();
                Assert.AreEqual("Additional Clients/Subjects", titeMassEditPage);
                extentReports.CreateLog("Page with title : " + titeMassEditPage + " is displayed upon clicking Mass Edit Records button ");

                //Get currency of column of Client Holdings (MM) and validate if it is same as Total Debt currency
                string colClientHoldings = clientSubjectsPage.GetClientHoldingsMM();
                string clientCurrency = colClientHoldings.Substring(23, 3);
                Console.WriteLine("Currency: " + clientCurrency);
                Assert.AreEqual(dCurrency, clientCurrency);
                extentReports.CreateLog("Column Client Holdings (MM): "+ clientCurrency + " is suffixed with same currency as of Total Debt Currency in Opportunity details page ");

                //Get currency of column of Debt Holdings (MM) and validate if it is same as Total Debt currency
                string colDebtHoldings = clientSubjectsPage.GetDebtHoldingsMM();
                string debtColCurrency = colDebtHoldings.Substring(21, 3);
                Console.WriteLine("Currency: " + debtColCurrency);
                Assert.AreEqual(dCurrency, debtColCurrency);
                extentReports.CreateLog("Column Debt Holdings (MM): " + debtColCurrency + " is suffixed with same currency as of Total Debt Currency in Opportunity details page ");

                //Select Key Creditor type and click Edit button
                string value = clientSubjectsPage.SelectValueFromTypeDropdown("Key Creditor");
                Assert.AreEqual("Key Creditor",value);
                extentReports.CreateLog("Key Creditor type is selected ");
                clientSubjectsPage.ClickEditButton();

                //Get value of Total Debt Holdings MM and validate if it is same as Total Debt (MM) in opportunity
                string valTotalDebt = clientSubjectsPage.GetTotalDebtHoldingsMM();
                Assert.AreEqual(debt, valTotalDebt);
                extentReports.CreateLog("Total of Column Debt Holdings MM is matching with Total Debt MM pulled from Opportunity ");

                //Get the Other Creditors of Debt Holdings (MM) 
                clientSubjectsPage.ClearAllDebtHoldings();
                string valCreditor = clientSubjectsPage.GetOtherCreditorsOfDebtHoldingsMM();
                Assert.AreEqual(debt, valCreditor);
                extentReports.CreateLog("Other Creditor of Debt Holdings MM is matching with Total Debt MM pulled from Opportunity when there is no other Debt Holdings ");

                //Update few Key Creditors
                clientSubjectsPage.UpdateAllDebtHoldings();
                string valCreditorLatest = clientSubjectsPage.GetOtherCreditorsOfDebtHoldingsMM();
                Assert.AreNotEqual(valTotalDebt, valCreditorLatest);
                extentReports.CreateLog("Other Creditor of Debt Holdings MM is updated according to match with Total Debt MM pulled from Opportunity when other Debt Holdings are updated ");
                clientSubjectsPage.ResetAllDebtHoldings();

                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }        
    }
}




