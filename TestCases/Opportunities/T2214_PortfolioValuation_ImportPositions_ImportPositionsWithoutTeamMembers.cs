﻿using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunity
{
    class T2214_PortfolioValuation_ImportPositions_ImportPositionsWithoutTeamMembers : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        ValuationPeriods valPeriods = new ValuationPeriods();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTC2214 = "T2214_ImportPositionsWithoutTeamMembers";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ImportPositionsWithoutTeamMembers()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2214;
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
                usersLogin.SearchUserAndLogin(valUser);
                //
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                Console.WriteLine("valRecordType:" + valRecordType);
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                //Calling AddOpportunities function                
                string value = addOpportunity.AddOpportunities(valJobType, fileTC2214);
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC2214);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details page 
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityDetails.ValidateOpportunityDetails() + " is created ");

                //Call function to update HL -Internal Team details
                opportunityDetails.UpdateInternalTeamDetails(fileTC2214);

                //Validate Portfolio Valuation button, click and validate Valuation periods title
                string titleVP = opportunityDetails.ClickPortfolioValuation();
                Assert.AreEqual(value + " - Valuation Periods", titleVP);
                extentReports.CreateLog("Page with title: " + titleVP + " is displayed upon clicking Portfolio Valuation button ");

                //Validate New Opp Valuation button, click and validate New Opportunity Valuation Period title
                string titleNewOppVP = valPeriods.ClickOppValuationPeriod();
                Assert.AreEqual("New Opportunity Valuation Period", titleNewOppVP);
                extentReports.CreateLog("Page with title: " + titleNewOppVP + " is displayed upon clicking New Opportunity Valuation Period button ");

                //Save Valuation Period details and validate Opportunity Valuation Period Detail page title
                string enteredName = valPeriods.EnterAndSaveOppValuationDetails(fileTC2214);
                string titleOppVal = valPeriods.GetOppValPeriodDetailTitle();
                Assert.AreEqual(enteredName, titleOppVal);
                extentReports.CreateLog("Valuation details are saved and page with title: " + titleOppVal + " is displayed ");

                //Validate error message "Notes is required if Asset Classes is 'Other' in Valuation Period Position page
                string valMessage = valPeriods.ValidateErrorMessage(fileTC2214);
                Assert.AreEqual("Error:\r\nNotes is required if Asset Classes is 'Other'.", valMessage);
                extentReports.CreateLog("Error message: " + valMessage + " is displayed ");

                //Enter all required details, Save and validate record exists in Opp Valuation Period Position section
                bool tableDisplayed = valPeriods.EnterValPeriodPositionDetails(fileTC2214);
                Assert.IsTrue(tableDisplayed);
                extentReports.CreateLog("Details are saved and record exists in Opp Valuation Period Positions section ");

                //Save Opp Valuation Period Team members and add Opp Valuation Period 
                valPeriods.ClickPositionAndSaveTeamMembers();
                extentReports.CreateLog("Opportunity Valuation Period Team members details are saved ");
                valPeriods.ClickOppValuationPeriod();
                Assert.AreEqual("New Opportunity Valuation Period", titleNewOppVP);
                extentReports.CreateLog("Page with title: " + titleNewOppVP + " is displayed upon clicking New Opportunity Valuation Period button ");
                string Name = valPeriods.EnterAndSaveOppValuationDetails(fileTC2214);
                extentReports.CreateLog("Opportunity Valuation Period: " + Name + " is created ");

                //Import Positions and validate the status of imported position
                string titleOppValPeriodDetail = valPeriods.ImportPositionsWithoutTM();
                Assert.AreEqual("Opportunity Valuation Period Detail", titleOppValPeriodDetail);
                extentReports.CreateLog("Positions are imported and Page with title: " + titleOppValPeriodDetail + " is displayed ");

                string Status = valPeriods.GetStatus();
                Assert.AreEqual("In Progress", Status);
                extentReports.CreateLog("Status of imported Position: " + Status + " is as expected ");

                //Click Position and validate team members message on Opportunity validation Position detail page
                string titleOppVal1 = valPeriods.ValidateOppValPositionDetailTitle();
                Assert.AreEqual("Opp Valuation Period Position", titleOppVal1);
                extentReports.CreateLog("Page: " + titleOppVal1 + " is displayed upon clicking the Position ");

                string message = valPeriods.GetMessageTeamMembers();
                Assert.AreEqual("Please add new team members to this position by selecting the 'Add New Team Member' button.", message);
                extentReports.CreateLog("Message: " + message + " is displayed under Opp Valuation Period Team Members section ");

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
