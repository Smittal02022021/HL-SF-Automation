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
    class TS26_ValidateIsMainContractIsFromClientCompanyWhenIsMainContractFromOtherCompanyExistsAlready_Lightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        AddOppCounterparty counterparty = new AddOppCounterparty();
        AddOpportunityContact addContact = new AddOpportunityContact();

        //public static string ERP = "TS02_PostUpdatingDFFFieldsOfOpportunity.xlsx";
        public static string ERP = "TS25_ValidateAllContracts.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ValidateIsMainContractCheckboxIsTRUE()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + ERP;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);

                //Login as Financial User and validate the user                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Verify the availability of Opportunity under HL Banker list
                string tagOpp = opportunityHome.ValidateOppUnderHLBanker();
                Assert.AreEqual("Opportunities", tagOpp);
                extentReports.CreateLog(tagOpp + " is displayed under HL Banker dropdown ");

                //Verify that choose LOB is displayed after clicking New button
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                string titleOpp = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                Assert.AreEqual("New Opportunity: " + valRecordType, titleOpp);
                extentReports.CreateLog("Page with title: " + titleOpp + " is displayed upon clicking next button ");

                //Calling AddOpportunities function
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string opportunityNumber = addOpportunity.AddOpportunitiesLightning(valJobType, ERP);
                Console.WriteLine("value : " + opportunityNumber);
                extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(ERP);
                Assert.AreEqual("Info", displayedTab);
                extentReports.CreateLog("Tab with name: " + displayedTab + " is displayed upon saving internal deal team members details ");

                //Create External Primary Contact
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                //counterparty.ClickViewCounterparties();               
                opportunityDetails.ClickAddCFOppContact();
                addContact.CreateContactL(ERP);

                //Logout of Standard user
                usersLogin.DiffLightningLogout();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search for added opportunity
                opportunityHome.SearchOpportunity(opportunityNumber);

                //Click on New Contract button
                string titleContract = opportunityDetails.ClickNewContract();
                Assert.AreEqual("New Contract", titleContract);
                extentReports.CreateLog("Page with title : " + titleContract + " is displayed upon clicking on New Contract button ");

                //Add the contract by selecting any company other than Client Company
                string titleDetail = opportunityDetails.AddContractBySelectingACompany("Test Contract", valContact, "Test Enterprises, Inc.");
                Assert.AreEqual("Test Contract", titleDetail);
                extentReports.CreateLog("Contract with name: " + titleDetail + " is added ");

                //Validate if Is Main Contract checkbox is checked
                string valueIsMain = opportunityDetails.ValidateIsMainContract();
                Assert.AreEqual("Is Main Contract checkbox is checked", valueIsMain);
                extentReports.CreateLog(valueIsMain + " of added Contract even it was not selected while saving the details ");

                //Add one more contract from client company
                opportunityDetails.ClickNewContract();
                string clientComp = ReadExcelData.ReadData(excelPath, "AddOpportunity", 1);
                string titleDetailIsMainTrue = opportunityDetails.AddContractBySelectingACompany("Additional Contract", valContact, clientComp);
                Assert.AreEqual("Additional Contract", titleDetailIsMainTrue);
                extentReports.CreateLog("New Contract: " + titleDetailIsMainTrue + " from client company is added ");

                //Validate if Is Main Contract checkbox is checked for new contract added selecting client company
                string valueNewIsMainCon = opportunityDetails.ValidateIsMainContractOfNewContract();
                Assert.AreEqual("Is Main Contract checkbox is checked", valueNewIsMainCon);
                extentReports.CreateLog(valueNewIsMainCon + " for the contract added from client company ");

                //Validate if Is Main Contract checkbox is checked for previously added contract
                string valueNewIsMainPrev = opportunityDetails.ValidateIsMainContractOfOldContract();
                Assert.AreEqual("Is Main Contract checkbox is not checked", valueNewIsMainPrev);
                extentReports.CreateLog(valueNewIsMainPrev + " for the contract added earlier from company other than client company ");

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



