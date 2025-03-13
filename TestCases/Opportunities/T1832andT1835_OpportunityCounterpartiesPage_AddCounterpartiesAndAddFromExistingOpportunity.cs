using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;


namespace SF_Automation.TestCases.Opportunities
{
    class T1832andT1835_OpportunityCounterpartiesPage_AddCounterpartiesAndAddFromExistingOpportunity_Lightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOppCounterparty addCounterparty = new AddOppCounterparty();


        public static string fileTC1832 = "T1832_OpportunityCounterpartiesPageAddCounterparties";
                
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");            
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void AddCounterparties()
        {
           try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1832;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                //Login as Financial User and validate the user                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Search for opportunity and open the details page
                opportunityHome.SearchMyOpportunitiesInLightning("[AP Buyside 24]", valUser);               
                extentReports.CreateLog("Matching records are displayed ");

                //Validate Counterparties button
                string counterParty = addCounterparty.ClickViewCounterparties();                
                Assert.AreEqual("Counterparty Editor", counterParty);
                extentReports.CreateLog("Page with tab: "+counterParty +" is displayed ");

                //Validate Add Counterparty button and navigate back to Opportunity details page
                string pageTitle = addCounterparty.ValidateAddCounterpartiesAndGetPageHeaderL(fileTC1832);
                Assert.AreEqual("Counterparties", pageTitle);
                extentReports.CreateLog("Page with title: "+pageTitle +" is displayed ");

                //Validation all sections on the page i.e. Filter, Get Companies from existing Opportunity/Company List
               
                string labelExistingOpp = addCounterparty.ValidateExistingOpportunitySectionL();
                Assert.AreEqual("Get Companies from existing Opportunity", labelExistingOpp);
                extentReports.CreateLog(labelExistingOpp + " section is displayed ");

                string labelExistingCompany = addCounterparty.ValidateExistingCompanySectionL();
                Assert.AreEqual("Get Companies from existing Company List", labelExistingCompany);
                extentReports.CreateLog(labelExistingCompany + " section is displayed ");

                //Expand sections i.e. Get Companies from existing Opportunity and validate fields
                string labelOpp = addCounterparty.ValidateFieldsOfExistingOppSectionL();
                Assert.AreEqual("Opportunity", labelOpp);
                extentReports.CreateLog("Field " + labelOpp + " under Get Companies from existing Opportunity section is displayed ");

                //Add Company from existing opportunity
                string msgSuccess = addCounterparty.AddCompanyFromExistingOppL("2020 Buyside - Imperative Chemical Partners");
                Assert.AreEqual("Selected Counterparty Records have been created.", msgSuccess);
                extentReports.CreateLog("Message " + msgSuccess + " is displayed upon adding company from exisitng opportunity ");

                //Validate title of Counterparty records page
                string titleCounterparties = addCounterparty.ClickBackButtonAndValidateViewCounterpartiesPage();
                Assert.AreEqual("Counterparties", titleCounterparties);
                extentReports.CreateLog("Page with title : " + titleCounterparties + " is displayed after clicking Back button on Add Counterparties page ");

                //Validate if company get added and delete the same
                string value = addCounterparty.ValidateAddedCompanyExistsL();
                Assert.AreEqual("Acceldata", value);
                extentReports.CreateLog("Added company: "+value+" is displayed on Edit Counterparty Records ");
                string message = addCounterparty.DeleteAddedCompanyL();
                Assert.AreEqual("Displaying 0 to 0 of 0 records. Page 1 of 0.", message);
                extentReports.CreateLog("Message :" +message+ " is displayed upon deleting added company ");

                //Expand sections i.e. Get Companies from existing Company List and validate fields
                addCounterparty.AddCounterpartiesL();
                string lblLookUp = addCounterparty.ValidateFieldsOfExistingCompanySectionL();
                Assert.AreEqual("True", lblLookUp);
                extentReports.CreateLog("LookUp Field under Get Companies from existing Opportunity section is displayed ");

                usersLogin.LightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();

            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
            }           
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            usersLogin.LightningLogout();
            usersLogin.UserLogOut();
        }
    }
}
