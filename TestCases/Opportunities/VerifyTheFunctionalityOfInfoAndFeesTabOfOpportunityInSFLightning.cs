﻿using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;


namespace SF_Automation.TestCases.Opportunity
{
    class VerifyTheFunctionalityOfInfoAndFeesTabOfOpportunityInSFLightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOppCounterparty counterparty = new AddOppCounterparty();       
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string TMTT0017889 = "TMTT0017889_CommentsAndContactsMappingToEngUponConversionFromOpportunity.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void FunctionalityOfOpportunity()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + TMTT0017889;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");   

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                 Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                 extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);

                //Login as Financial User and validate the user                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Verify the availablity of Opportunity under HL Banker list
                string tagOpp= opportunityHome.ValidateOppUnderHLBanker();
                Assert.AreEqual("Opportunities", tagOpp);
                extentReports.CreateLog(tagOpp + " is displayed under HL Banker dropdown ");

                //Verify that Recently Viewed is displayed along with list of Opportunities
                string lblRecently = opportunityHome.ValidateRecentViewedUponSelectingOpportunities();
                Assert.AreEqual("Recently Viewed", lblRecently);
                extentReports.CreateLog(lblRecently + " is displayed upon selecting Opportunities ");

                //List of Recent Viewed Opportunities are displayed
                string oppList = opportunityHome.ValidateIfRecentlyViewedOpportunitiesAreDisplayed();
                Assert.AreEqual("True", oppList);
                extentReports.CreateLog("Recently viewed Opportunities are displayed in Recently Viewed list ");

                //Validate all the values displayed under Recently Viewed
                Assert.IsTrue(opportunityHome.ValidateRecentlyViewedValues(), "Verified that displayed Recently Viewed values are same");
                extentReports.CreateLog("Recently Viewed dropdown values are displayed as expected ");

                //Validate if Search functionality is available or not
                string searchOpp =opportunityHome.ValidateSearchFunctionalityIsAvailable();
                Assert.AreEqual("True", searchOpp);
                extentReports.CreateLog("Search Opportunities functionality is available ");

                //Verify Search Functionality of Opportunities
                string searchedOpp = opportunityHome.ValidateSearchFunctionalityOfOpportunities("110980");
                Assert.AreEqual("110980", searchedOpp);
                extentReports.CreateLog("Opportunity is displayed as per entered search criteria ");

                //Verify that choose LOB is displayed after clicking New button
                Assert.IsTrue(opportunityHome.ValidateChooseLOBPostClickingNewButton(), "Verified that displayed LOBs are same");
                extentReports.CreateLog("Choose LOB screen is displayed upon clicking New button ");

                //Validate title of the page upon clicking next page
                string titleOpp = opportunityHome.ClickNextAndValidatePage();
                Assert.AreEqual("New Opportunity: CF", titleOpp);
                extentReports.CreateLog("Page with title: " + titleOpp + " is displayed upon clicking next button ");

                //Validate that mandatory field validations are displayed when save button is clicked without entering any values
                string oppNameValidation = addOpportunity.ValidateMandatoryFieldsValidations();
                Assert.AreEqual("Complete this field.", oppNameValidation);
                extentReports.CreateLog("Validation: " + oppNameValidation + " is displayed for Opportunity Name field when Save button is clicked without entering any value ");

                //Validate that mandatory field validations for Client
                string clientValidation = addOpportunity.ValidateMandatoryValidationOfClient();
                Assert.AreEqual("Complete this field.", clientValidation);
                extentReports.CreateLog("Validation: " + clientValidation + " is displayed for Client field when Save button is clicked without entering any value ");

                //Validate that mandatory field validations for Subject
                string subValidation = addOpportunity.ValidateMandatoryValidationOfSubject();
                Assert.AreEqual("Complete this field.", subValidation);
                extentReports.CreateLog("Validation: " + subValidation + " is displayed for Subject field when Save button is clicked without entering any value ");

                //Validate that mandatory field validations for Job Type
                string jobTypeValidation = addOpportunity.ValidateMandatoryValidationOfJobType();
                Assert.AreEqual("Complete this field.", jobTypeValidation);
                extentReports.CreateLog("Validation: " + jobTypeValidation + " is displayed for Job Type field when Save button is clicked without entering any value ");

                //Validate that mandatory field validations for Industry Group
                string IGValidation = addOpportunity.ValidateMandatoryValidationOfIG();
                Assert.AreEqual("Complete this field.", IGValidation);
                extentReports.CreateLog("Validation: " + IGValidation + " is displayed for Industry Group field when Save button is clicked without entering any value ");

                //Validate that mandatory field validations for Primary Office
                string primaryValidation = addOpportunity.ValidateMandatoryValidationOfPrimaryOffice();
                Assert.AreEqual("Complete this field.", primaryValidation);
                extentReports.CreateLog("Validation: " + primaryValidation + " is displayed for Primary Office field when Save button is clicked without entering any value ");

                //Validate that mandatory field validations for Legal Entity
                string legalValidation = addOpportunity.ValidateMandatoryValidationOfLegalEntity();
                Assert.AreEqual("Complete this field.", legalValidation);
                extentReports.CreateLog("Validation: " + legalValidation + " is displayed for Legal Entity field when Save button is clicked without entering any value ");

                //Validate that mandatory field validations for Referral Type
                string refTypeValidation = addOpportunity.ValidateMandatoryValidationOfRefType();
                Assert.AreEqual("Complete this field.", refTypeValidation);
                extentReports.CreateLog("Validation: " + refTypeValidation + " is displayed for Referral Type field when Save button is clicked without entering any value ");

                //Validate that mandatory field validations for Additional Client
                string addClientValidation = addOpportunity.ValidateMandatoryValidationOfAdditionalClient();
                Assert.AreEqual("Complete this field.", addClientValidation);
                extentReports.CreateLog("Validation: " + addClientValidation + " is displayed for Additional Client field when Save button is clicked without entering any value ");

                //Validate that mandatory field validations for Additional Subject
                string addSubjectValidation = addOpportunity.ValidateMandatoryValidationOfAdditionalSubject();
                Assert.AreEqual("Complete this field.", addSubjectValidation);
                extentReports.CreateLog("Validation: " + addSubjectValidation + " is displayed for Additional Subject field when Save button is clicked without entering any value ");

                //Validate that mandatory field validations for Beneficial Owner & Control Person form?
                string benOwnerValidation = addOpportunity.ValidateMandatoryValidationOfBeneficialOwner();
                Assert.AreEqual("Complete this field.", benOwnerValidation);
                extentReports.CreateLog("Validation: " + benOwnerValidation + " is displayed for Beneficial Owner & Control Person form? field when Save button is clicked without entering any value ");

                //Validate that mandatory field validations for Does HL Have Material Non-Public Info?
                string doesHLValidation = addOpportunity.ValidateMandatoryValidationOfDoesHL();
                Assert.AreEqual("Complete this field.", doesHLValidation);
                extentReports.CreateLog("Validation: " + doesHLValidation + " is displayed for Does HL Have Material Non-Public Info? field when Save button is clicked without entering any value ");

                //Enter details for all mandatory fields
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunitiesLightning(valJobType, TMTT0017889);
                Console.WriteLine("value : " + value);
                extentReports.CreateLog("Opportunity with number : " + value + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab=  addOpportunity.EnterStaffDetailsL(TMTT0017889);
                Assert.AreEqual("Info", displayedTab);
                extentReports.CreateLog("Tab with name: " + displayedTab + " is displayed upon saving internal deal team members details ");

                string tabDetails = opportunityDetails.ValidateDetailsTabL();
                Assert.AreEqual("Details", tabDetails);
                extentReports.CreateLog("Sub Tab with name: " + tabDetails + " is displayed under Info Tab ");
                
                string tabAdmin = opportunityDetails.ValidateAdministrationTabL();
                Assert.AreEqual("Administration", tabAdmin);
                extentReports.CreateLog("Sub Tab with name: " + tabAdmin + " is displayed under Info Tab ");

                //Validate Edit functionality of Details sub tab under Info tab                
                string tabInfoEditable = opportunityDetails.ValidateDetailsTabIsEditable();
                Assert.AreEqual("True", tabInfoEditable);
                extentReports.CreateLog("Page is editable after clicking pencil icon and General details can be edited ");

                //Update any value and validate if it gets saved post clicking saving button
                string valClientOwnership = opportunityDetails.GetClientOwnershipL();
                opportunityDetails.UpdateClientOwnershipL();
                string valUpdClientOwnership = opportunityDetails.GetClientOwnershipLPostUpdate();
                Assert.AreNotEqual(valClientOwnership, valUpdClientOwnership);
                extentReports.CreateLog("Entered value : " +valUpdClientOwnership + " is displayed after updating details of Client Ownership ");

                //Validate Edit functionality of Details sub tab under Info tab                
                string tabAdminEditable = opportunityDetails.ValidateAdminTabIsEditable();
                Assert.AreEqual("True", tabAdminEditable);
                extentReports.CreateLog("Page is editable after clicking pencil icon and Administration details can be edited ");

                //Update any value and validate if it gets saved post clicking saving button
                string valPM = opportunityDetails.GetPrimaryOfficeL();
                opportunityDetails.UpdatePrimaryOfficeL();
                string valUpdPO = opportunityDetails.GetPOPostUpdate();
                Assert.AreNotEqual(valPM, valUpdPO);
                extentReports.CreateLog("Entered value : " + valUpdPO + " is displayed after updating details of Primary Office ");

                //Validate Fees & Financials tab and its sections
                string tabFees = opportunityDetails.ValidateFeesAndFinancialsTabL();
                Assert.AreEqual("Fees & Financials", tabFees);
                extentReports.CreateLog("Tab with name: " + tabFees + " is displayed under Opportunity Details page ");

                //Validate Estimated Fees section
                string secEstFees = opportunityDetails.ValidateEstimatedFeesSection();
                Assert.AreEqual("Estimated Fees", secEstFees);
                extentReports.CreateLog("Section with name: " + secEstFees + " is displayed under Fees & Financials tab ");

                //Validate Fees Notes & Description section
                string secFeeNotes = opportunityDetails.ValidateFeesNotesAndDescriptionSection();
                Assert.AreEqual("Fees Notes & Description", secFeeNotes);
                extentReports.CreateLog("Section with name: " + secFeeNotes + " is displayed under Fees & Financials tab ");

                //Validate Funds & Financials section
                string secFunds = opportunityDetails.ValidateFundsAndFinancialsSection();
                Assert.AreEqual("Funds & Financials", secFunds);
                extentReports.CreateLog("Section with name: " + secFunds + " is displayed under Fees & Financials tab ");

                //Validate Edit functionality of Fees And Financials tab                
                string tabFeesEditable = opportunityDetails.ValidateFeesAndFinancialsTabIsEditable();
                Assert.AreEqual("True", tabFeesEditable);
                extentReports.CreateLog("Page is editable after clicking pencil icon and Fees And Financials details can be edited ");

                //Update any value and validate if it gets saved post clicking saving button
                string valCurrency = opportunityDetails.GetCurrencyL();
                Console.WriteLine(valCurrency);
                opportunityDetails.UpdateCurrencyL();
                string valUpdCurrency = opportunityDetails.GetCurrencyPostUpdate();
                Assert.AreNotEqual(valCurrency, valUpdCurrency);
                extentReports.CreateLog("Entered value : " + valUpdCurrency + " is displayed after updating details of Currency ");

                //Validate the validation displayed for Est. Transaction Size when it is more than 100000
                string estTxnValidation = opportunityDetails.GetValidationOfEstTxnSizeWhenItExceeds100000();
                Assert.AreEqual("The Est.Transaction Size/Market Cap (MM) cannot exceed $100,000 MM.", estTxnValidation);
                extentReports.CreateLog("Validation: " + estTxnValidation + " is displayed when Est. Transaction Size is entered more than 100000 ");

                //Validate the value displayed for Est. Transaction Size when it is less than 100000
                string estTxnValue = opportunityDetails.GetOfEstTxnSizeWhenItIsLessThan100000();
                Assert.AreEqual("100,000", estTxnValue);
                extentReports.CreateLog("Est Transaction Size with value: " + estTxnValue + " is displayed when it is entered less than 100000 ");

                //Validate Client/Subject & Referral tab and its sections
                string tabClient = opportunityDetails.ValidateClientSubjectAndReferralTabL();
                Assert.AreEqual("Client/Subject & Referral", tabClient);
                extentReports.CreateLog("Tab with name: " + tabClient + " is displayed under Opportunity Details page ");

                //Validate Referral Info section
                string secRefInfo = opportunityDetails.ValidateReferralInfoSection();
                Assert.AreEqual("Referral Info", secRefInfo);
                extentReports.CreateLog("Section with name: " + secRefInfo + " is displayed under Client/Subject & Referral tab ");

                //Validate Additional Client/Subject section
                string secAddClient = opportunityDetails.ValidateAdditionalClientAndSubjectSection();
                Assert.AreEqual("Additional Client/Subject", secAddClient);
                extentReports.CreateLog("Section with name: " + secAddClient + " is displayed under Client/Subject & Referral tab ");

                //Validate Edit functionality of Client/Subject & Referral tab              
                string tabAddClientEditable = opportunityDetails.ValidateClientSubjectRefTabIsEditable();
                Assert.AreEqual("True", tabAddClientEditable);
                extentReports.CreateLog("Page is editable after clicking pencil icon and Client/Subject & Referral details can be edited ");

                //Update any value and validate if it gets saved post clicking saving button
                string valRefType = opportunityDetails.GetRefTypeL();
                Console.WriteLine(valRefType);
                opportunityDetails.UpdateRefTypeL();
                string valUpdRefType = opportunityDetails.GetRefTypePostUpdate();
                Assert.AreNotEqual(valRefType, valUpdRefType);
                extentReports.CreateLog("Entered value : " + valUpdRefType + " is displayed after updating details of Referral Type ");

                //Validate Compliance and Legal tab and its sections
                string tabCompliance = opportunityDetails.ValidateComplianceAndLegalTabL();
                Assert.AreEqual("Compliance & Legal", tabCompliance);
                extentReports.CreateLog("Tab with name: " + tabCompliance + " is displayed under Opportunity Details page ");

                //Validate Compliance tab
                string secCompliance = opportunityDetails.ValidateComplianceTab();
                Assert.AreEqual("Compliance", secCompliance);
                extentReports.CreateLog("Sub tab with name: " + secCompliance + " is displayed under Compliance and Legal tab ");

                //Validate Legal Matters tab
                string secLegal = opportunityDetails.ValidateLegalMattersTab();
                Assert.AreEqual("Legal Matters", secLegal);
                extentReports.CreateLog("Sub tab with name: " + secLegal + " is displayed under Compliance and Legal tab ");

                //Validate Conflicts Check tab
                string secConflicts = opportunityDetails.ValidateConflictsCheckTab();
                Assert.AreEqual("Conflicts Check", secConflicts);
                extentReports.CreateLog("Sub tab with name: " + secConflicts + " is displayed under Compliance and Legal tab ");

                //Validate Edit functionality of Compliance tab              
                string subTabCompliance = opportunityDetails.ValidateComplianceTabIsEditable();
                Assert.AreEqual("True", subTabCompliance);
                extentReports.CreateLog("Page is editable after clicking pencil icon and Compliance details can be edited ");

                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }        
    }
}

    
