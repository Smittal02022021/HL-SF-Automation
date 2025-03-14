﻿using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class ZObsoleted_T1692_TMTT0011214_FAS_ValidationsToBeCompletedBeforeFASOpportunityIsApproved_ValidateWomenLedValues : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        CNBCForm form = new CNBCForm();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();


        public static string fileTC1692 = "T1692_ValidationsToBeCompletedBeforeFASOpportunityIsApproved";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ValidationsToBeCompletedBeforeCFOpportunityIsApproved()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1692;
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
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validate Yes/No values of Women Led field
                Assert.IsTrue(addOpportunity.VerifyWomenLedValues(), "Verified that displayed Women Led values are same");
                extentReports.CreateLog("Displayed Women Led values are correct ");

                //Calling AddOpportunities function                
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunitiesForFVA(valJobType, fileTC1692);
                Assert.AreEqual("Error: The fee amount must be greater than or equal to $10,000. Please include the total estimated fees for the next 12 months", value);
                extentReports.CreateLog("Validation: " +value + " is displayed upon entering Fee less than $10,000 ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC1692);
                //Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details page 
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityDetails.ValidateOpportunityDetails() + " is created ");

                //Fetch values of Opportunity Name, Client, Subject and Job Type
                string oppName = opportunityDetails.GetOpportunityName();
                string clientName = opportunityDetails.GetClient();
                string subjectName = opportunityDetails.GetSubject();
                string jobType = opportunityDetails.GetJobType();
                Console.WriteLine(jobType);

                //Validate value of Women Led field
                string womenLed = opportunityDetails.GetWomenLedValue();
                Assert.AreEqual(" ", womenLed);
                extentReports.CreateLog("Opportunity is created without selecting Women Led field ");

                //Click on Request Enagagement button and validate all validations
                string val = opportunityDetails.ClickRequestEngWithoutDetails();
                Console.WriteLine("val: " +val);
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddContact", 9),val);
                extentReports.CreateLog("Validations: " + val + " are displayed upon clicking Request Engagement button without entering additional details ");
                
                //Call function to update HL -Internal Team details, Est Fees values and other required details
                opportunityDetails.UpdateInternalTeamDetails(fileTC1692);
                opportunityDetails.UpdateFieldsForConversion(fileTC1692);
                extentReports.CreateLog("All required details are updated in Opportunity details ");

                //Create External Primary Contact         
                String valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                String valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                addOpportunityContact.CreateContact(fileTC1692, valContact, valRecordType, valContactType);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                opportunityDetails.UpdateTotalAnticipatedRevenueForValidations();

                //Log out from standard User and validate admin
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("Admin user " + login.ValidateUser() + " logged in ");

                //Search created opportunity, update conflict check and NBC details
                string valSearch = opportunityHome.SearchOpportunity(opportunityNumber);
                Console.WriteLine("result : " + valSearch);
                opportunityDetails.UpdateOutcomeDetails(fileTC1692);

                //Click on Request Enagagement button and validate all validations
                opportunityDetails.UpdateJobType("FA - Portfolio-Valuation");         
                string val1 = opportunityDetails.GetEstFinValidation();
                Console.WriteLine("val1: " + val1);
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddContact", 10), val1);
                extentReports.CreateLog("Validations: " + val1 + " are displayed upon clicking Request Engagement button when Job Type is updated to Portfolio-Valuation ");

                //Update client and Subject 
                opportunityDetails.UpdateClientandSubject("A + K Agency GmbH");
                string val2 = opportunityDetails.ClickRequestEngWithoutDetails();
                Console.WriteLine("val2: " + val2);
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddContact", 11), val2);
                extentReports.CreateLog("Validations: " + val2 + " are displayed upon clicking Request Engagement button when Company details are null ");

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
