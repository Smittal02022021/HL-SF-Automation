using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunity
{
    class VerifyTheFunctionalityOfInfo_Fees_ClientSubjectReferral_Compliance_InternalTeam_RightPanel_TopPanel_OppInSFLightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOppCounterparty counterparty = new AddOppCounterparty();       
        AddOpportunityContact addContact = new AddOpportunityContact();

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
                string excelPath1 = ReadJSONData.data.filePaths.testData;
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

                //PRJ0019053 - Opportunity Initiator - TC01----
                //Validate HL Internal Team page
                string titleHL = addOpportunity.ValidateHLInternalTeamPage();
                Assert.AreEqual( value +" - HL Internal Team", titleHL);
                extentReports.CreateLog("Page with title: " + titleHL + " is displayed upon saving all mandatory details while creating Opportunity ");

                //PRJ0019053 - Opportunity Initiator - TC02----
                //Verify Initiator message
                string msgInitiator = addOpportunity.ValidateInitiatorMessage();
                Assert.AreEqual("*Note: An Initiator is Required", msgInitiator);
                extentReports.CreateLog("Message: " + msgInitiator + " is displayed on HL Internal Team page ");

                //PRJ0019053 - Opportunity Initiator - TC03----
                //Verify Roles validation
                string msgRoles = addOpportunity.ValidateRolesValidation(TMTT0017889);
                Assert.AreEqual("Error:\r\nPlease Select at least one Role for the New Team Member.", msgRoles);
                extentReports.CreateLog("Message: " + msgRoles + " is displayed when no role is selected for entered staff on HL Internal Team page ");

                //PRJ0019053 - Opportunity Initiator - TC04----
                //Validate User is redirected to Internal team page if Initiator is not selected
                string titleHLRedirect = addOpportunity.ValidateUserIsRedirectedToHLInternalPage();
                Assert.AreEqual(value + " - HL Internal Team", titleHLRedirect);
                extentReports.CreateLog("Page with title: " + titleHLRedirect + " is displayed when Initiator role is not selected and Opportunity is opened again from Recent items ");

                //PRJ0019053 - Opportunity Initiator - TC05----
                //Validate User is redirected to Internal team page if Initiator is not selected
                string titleHLGlobal = addOpportunity.ValidateUserIsRedirectedToHLInternalPageWhenOppOpenedFromGlobalSearch(value);
                Assert.AreEqual(value + " - HL Internal Team", titleHLGlobal);
                extentReports.CreateLog("Page with title: " + titleHLGlobal + " is displayed when Initiator role is not selected and Opportunity is opened again from Global Search ");
                
                //PRJ0019053 - Opportunity Initiator - TC07----
                //Validate User is redirected to Internal team page if Initiator is not selected
                //string titleHLActiveList = addOpportunity.ValidateUserIsRedirectedToHLInternalPageFromMyActiveOpp();
                //Assert.AreEqual("HL Internal Team", titleHLActiveList);
                //extentReports.CreateLog("Page with title: " + titleHLActiveList + " is displayed when Initiator role is not selected and Opportunity is opened again from My Active Opportunities ");

                //PRJ0019053 - Opportunity Initiator - TC09----
                //Validate Return to Opportunity button is displayed if Initiator is  selected
                string btnReturnToOpp = addOpportunity.ValidateReturnToOppButtonWhenInitiatorIsSelected();
                Assert.AreEqual("Return To Opportunity", btnReturnToOpp);
                extentReports.CreateLog("Button with name: " + btnReturnToOpp + " is displayed when Initiator role is selected ");

                //PRJ0019053 - Opportunity Initiator - TC10----
                //Validate User is not redirected to Internal team page if Initiator is selected
                string titlePage = addOpportunity.ValidatePageWhenInitiatorRoleIsSelected();
                Assert.AreEqual("Info", titlePage);
                extentReports.CreateLog("Page with tab: " + titlePage + " is displayed when Initiator role is selected and Opportunity is opened again ");
                               
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

                //Update any value and validate if it gets saved post clicking saving button
                string valBenOwner = opportunityDetails.GetBeneficialOwnerL();
                Console.WriteLine(valBenOwner);
                opportunityDetails.UpdateBenOwnerL();
                string valUpdBenOwner = opportunityDetails.GetBenOwnerPostUpdate();
                Assert.AreNotEqual(valBenOwner, valUpdBenOwner);
                extentReports.CreateLog("Entered value : " + valUpdBenOwner + " is displayed after updating details of Beneficial Owner & Control Person form? ");

                //Validate Edit functionality of Legal Matters tab                
                string subTabLegal = opportunityDetails.ValidateLegalMattersTabIsEditable();
                Assert.AreEqual("True", subTabLegal);
                extentReports.CreateLog("Page is editable after clicking pencil icon and Legal Matters details can be edited ");

                //Update any value and validate if it gets saved post clicking saving button
                string valConf = opportunityDetails.GetConfAgreementL();
                Console.WriteLine(valConf);
                opportunityDetails.UpdateConfAgreementL();
                string valUpdConf = opportunityDetails.GetConfAgreementPostUpdate();
                Assert.AreNotEqual(valConf, valUpdConf);
                extentReports.CreateLog("Entered value : " + valUpdConf + " is displayed after updating details of Confidentiality Agreement ");

                //Validate Internal Team tab 
                string tabIT = opportunityDetails.ValidateInternalTeamTabL();
                Assert.AreEqual("Internal Team", tabIT);
                extentReports.CreateLog("Tab with name: " + tabIT + " is displayed under Opportunity Details page ");
                
                //Validate Modify Roles button and save funcitonality  
                string addedStaff = opportunityDetails.ValidateModifyRolesButton();
                Assert.AreEqual("Rob Oudman", addedStaff);
                extentReports.CreateLog("Team member with name: " + addedStaff + " is displayed upon saving it ");

                ////Validate Return to Opportunity button and its functionality  
                //string btnReturnToOpp1 = opportunityDetails.ValidateReturnToOpp();
                //Assert.AreEqual("Details", btnReturnToOpp1);
                //extentReports.CreateLog("Opportunity details page is displayed after clicking Return To Opportunity button ");

                //TC_06:- Validate Comments tab                
                string tabComments = opportunityDetails.ValidateCommentsTabL();
                Assert.AreEqual("Comments", tabComments);
                extentReports.CreateLog("Tab with name: " + tabComments + " is displayed under Opportunity Details page ");

                //Save an Opportunity Comment
                opportunityDetails.AddOppCommentaAndValidate();
                string addedComments = opportunityDetails.GetOppCommentsL();
                //Assert.AreEqual("Administrative", addedCommentsType);
                Assert.AreEqual("Testing", addedComments);
                extentReports.CreateLog("Added Opportunity comments: " + addedComments+ " is displayed under Comments section ");
                
                string uploadFiles = opportunityDetails.ValidateFileUploadsOption();
                Assert.AreEqual("Upload Files", uploadFiles);
                extentReports.CreateLog("Button with name: " + uploadFiles + " is displayed under Files section of Opportunity Details page ");

                string successMsg= opportunityDetails.UploadFileAndValidate(excelPath1 + "UploadFile.txt");
                Assert.AreEqual("UploadFile", successMsg);
                extentReports.CreateLog("Selected File has been uploaded ");

                //TC_07:- Validate Top Panel                 
                string tabEdit = opportunityDetails.ValidateEditTabL();
                Assert.AreEqual("Edit", tabEdit);
                extentReports.CreateLog("Tab with name: " + tabEdit + " is displayed under Opportunity Details page ");

                string messageClient = opportunityDetails.GetMandatoryFieldValidationOfGeneral();
                Assert.AreEqual("Complete this field.", messageClient);
                extentReports.CreateLog("Validation message: " + messageClient + " is displayed when Save button is clicked on after removing one of the mandatory field ");

                //Save the data and validate it
                string valUpdatedOpp = opportunityDetails.UpdateDataAndValidate(value);
                Assert.AreEqual(value, valUpdatedOpp);
                extentReports.CreateLog("Updated Opportunity Name: " + valUpdatedOpp + " is displayed after updating its value ");

                //Validate View Counterparties
                string tabCounterparties = opportunityDetails.ValidateViewCounterpartiesTabL();
                Assert.AreEqual("View Counterparties", tabCounterparties);
                extentReports.CreateLog("Tab with name: " + tabCounterparties + " is displayed under Opportunity Details page ");

                //Vaidate add Counterparties
                string btnAddCounterparties = counterparty.ValidateAddCounterpartiesButtonL();
                Assert.AreEqual("Get Companies from existing Opportunity", btnAddCounterparties);
                extentReports.CreateLog("Section with name: " + btnAddCounterparties + " is displayed after clicking Add Counterparties button ");

                string secAddCounterparties2 = counterparty.Get2ndSectionOfAddCounterpartyL();
                Assert.AreEqual("Get Companies from existing Company List", secAddCounterparties2);
                extentReports.CreateLog("Section with name: " + secAddCounterparties2 + " is displayed after clicking Add Counterparties button ");

                //Validate error message when companies are not added from existing Opportunity
                string msgNoCompany = counterparty.GetErrorMessageWhenNoCompanyIsAdded();
                Assert.AreEqual("Please select counterparty record(s) to add", msgNoCompany);
                extentReports.CreateLog("Error message : " + msgNoCompany + " is displayed when no company is selected from existing Opportunity ");

                //Validate success message when a company is added from existing Opportunity
                string msgSuccess = counterparty.GetSuccessMessageWhenACompanyIsAdded();
                Assert.AreEqual("Selected Counterparty Records have been created.", msgSuccess);
                extentReports.CreateLog("Success message : " + msgSuccess + " is displayed when a company is added from existing Opportunity ");

                //Validate success message when multiple companies are added from existing Opportunity
                string msgSuccessMultiple = counterparty.GetSuccessMessageWhenMultipleCompaniesAreAdded();
                Assert.AreEqual("Selected Counterparty Records have been created.", msgSuccessMultiple);
                extentReports.CreateLog("Success message : " + msgSuccessMultiple + " is displayed when multiple companies are added from existing Opportunity ");

                //Validate error message when companies are not added from existing Company List
                string msgNoCompanyFromComp = counterparty.GetErrorMessageWhenNoCompanyIsAddedFromExistingCompany();
                Assert.AreEqual("Please select counterparty record(s) to add", msgNoCompanyFromComp);
                extentReports.CreateLog("Error message : " + msgNoCompanyFromComp + " is displayed when no company is selected from existing Company List ");

                //Validate success message when a company is added from existing Company List
                string msgSuccessFromComp = counterparty.GetSuccessMessageWhenACompanyIsAddedFromExistingCompany();
                Assert.AreEqual("Selected Counterparty Records have been created.", msgSuccessFromComp);
                extentReports.CreateLog("Success message : " + msgSuccessFromComp + " is displayed when a company is added from existing Company List ");

                //Validate success message when multiple companies are added from existing Company List
                string msgSuccessMultipleFromComp = counterparty.GetSuccessMessageWhenMultipleCompaniesAreAdded();
                Assert.AreEqual("Selected Counterparty Records have been created.", msgSuccessMultipleFromComp);
                extentReports.CreateLog("Success message : " + msgSuccessMultipleFromComp + " is displayed when multiple companies are added from existing Company List ");

                //Validate Counterparties page after clicking Back button
                string titleCounterparties = counterparty.ClickBackButtonAndValidateViewCounterpartiesPage();
                Assert.AreEqual("Counterparties", titleCounterparties);
                extentReports.CreateLog("Page with title : " + titleCounterparties + " is displayed after clicking Back button on Add Counterparties page ");

                //Validate if added counterparties are displayed on View Counterparties
                string addedCounterparties = counterparty.ValidateIfAddedCounterpartiesExists();
                Assert.AreEqual("Added Counterparties are displayed", addedCounterparties);
                extentReports.CreateLog("Counterparties have been added after saving from Add Counterparties page ");

                //Validate all View values
                Assert.IsTrue(counterparty.VerifyViewTypes(), "Verified that displayed View values are same");
                extentReports.CreateLog("Displayed View values are correct ");

                ////Validate displayed Counterparties after selecting View 
                //string msgNoRec = counterparty.ValidateDisplayedRecordsAsPerSelectedView();
                //Assert.AreEqual("No records found", msgNoRec);
                //extentReports.CreateLog("No Counterparties are displayed when view is updated ");

                //Click on Opp Name tab and click on Add CF Opportunity Contact
                opportunityDetails.ClickOppName();
                string titleCFContact = opportunityDetails.ClickAddCFOppContact();
                Assert.AreEqual("Add CF Opportunity Contact", titleCFContact);
                extentReports.CreateLog("Page with title: " + titleCFContact + " is displayed after clicking Add CF Opportunity Contact button ");

                //Validate the mandatory validations
                string msgContact = addContact.GetContactValidation();
                Assert.AreEqual("Complete this field.", msgContact);
                extentReports.CreateLog("Validation message for Contact field : " + msgContact + " is displayed when Save button is clicked without entering any value in it ");

                //Validate the mandatory validations
                string msgParty = addContact.GetPartyValidation();
                Assert.AreEqual("Complete this field.", msgParty);
                extentReports.CreateLog("Validation message for Party field : " + msgParty + " is displayed when Save button is clicked without entering any value in it ");

                //Validate Cancel Functionality of Add Opp CF Contact
                addContact.ValidateCancelFunctionalityOfContactL(TMTT0017889);
                string contact = addContact.ValidateAddedContact();
                string name = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                Assert.AreEqual("No contact is added", contact);
                extentReports.CreateLog(contact + " is not added after clicking cancel button ");

                //Add Opportunity CF Contact and validate the added contact
                opportunityDetails.ClickAddCFOppContact();
                addContact.CreateContactL(TMTT0017889);
                string addedContact = addContact.ValidateAddedContact();               
                Assert.AreEqual(name, addedContact);
                extentReports.CreateLog("Added Opportunity Contact: " + addedContact + " is displayed after clicking Save button ");

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

    

