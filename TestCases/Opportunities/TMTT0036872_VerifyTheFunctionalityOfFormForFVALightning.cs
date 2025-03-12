using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;


using System;

namespace SF_Automation.TestCases.Opportunities
{
    class TMTT0036872_VerifyTheFunctionalityOfFormForFVALightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        FEISForm form = new FEISForm();
        AddOpportunityContact addContact = new AddOpportunityContact();
        AddOppCounterparty counterparty = new AddOppCounterparty();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTC1644 = "TMTT0036872_VerifyTheFunctionalityOfFormForFVALightning.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void FEISFormSubmitforReview()
        {
            try
            {
                //Get path of Test data file  (need to add Karan in PV Supervisor)
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1644;
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
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
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

                //Add FVA Opportunity
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunitiesLightning(valJobType, fileTC1644);

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileTC1644);
                Assert.AreEqual("Info", displayedTab);
                extentReports.CreateLog("Tab with name: " + displayedTab + " is displayed upon saving internal deal team members details ");

                //Update all required fields for Conversion to Engagement
                //counterparty.ClickViewCounterparties();
                opportunityDetails.UpdateReqFieldsForFVAConversionL(fileTC1644);
                extentReports.CreateLog("All required details are saved ");
                opportunityDetails.ClickAddFVAOppContact();
                addContact.CreateContactL(fileTC1644);

                //Logout
                usersLogin.LightningLogout();

                //Search for Opportunity
                opportunityHome.SearchOpportunity(value);
                opportunityDetails.UpdateInternalTeamDetails(fileTC1644);
                extentReports.CreateLog("Internal Team members details are saved ");
                opportunityDetails.UpdateOutcomeDetails(fileTC1644);

                //Login as Financial User and validate the user                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUserLightning();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Open the same opportunity               
                opportunityHome.SearchMyOpportunitiesInLightning(value, valUser);

                //1. TMTI0088216_Verify the availability of the FEIS Form button availability on all Opportunities of Fairness Job Types
                string FEISForm = opportunityDetails.ValidateFEISFormButton();
                string clientName = opportunityDetails.GetClientCompanyL();
                string subjectName = opportunityDetails.GetSubjectCompanyL();
                opportunityDetails.ValidateClientSubjectAndReferralTabL();
                string valUpdRefType = opportunityDetails.GetRefTypePostUpdate();
                addOpportunity.ClickInfoTab();
                Assert.AreEqual("FEIS Form button is displayed", FEISForm);
                extentReports.CreateLog("FEIS Form Button is displayed for the Opportunity with Fairness Job type ");

                //2. TMTI0088218_Verify that if the FEIS Form is not completed, then Requesting Engagement applications will give an error message to Complete and Submit the FEIS form
                string validations = opportunityDetails.ClickRequestoEngFVAL();
                Console.WriteLine("Validations:" + validations);
                Assert.AreEqual("Items to complete: Info-->Details-->General: Tombstone Permission is required. Info-->Administration: \"Women Led\" is required. Please update this field with the correct value Approved FEIS form - Please complete and submit this form via the FEIS button. Opportunity Contacts - Add at least one Contact with an approrpriate Role - confirm with FVA BUAs. Info-->Administration-->Service & Transaction Type: Transaction Type is required. Fees & Financials-->Estimated Fees: Total Anticipated Revenue is required. It should be Greater Than or Equal to the Fee.", validations);
                extentReports.CreateLog("Validation: " + validations + " is displayed when Request To Engagement Button is clicked without filling FEIS Form ");

                //3. TMTI0088220_Verify that clicking the FEIS Form button will redirect the user to the FEIS Form page
                string title = opportunityDetails.ClickFEISFormL();
                Assert.AreEqual("FEIS (Part I) Form", title);
                extentReports.CreateLog("Page with title:" + title + " is displayed upon clicking FEIS Form button ");

                //4.  TMTI0088234_Verify that clicking the FEIS form redirects the user to the Opportunity Overview tab by default
                string tabDisplayed = form.ValidateDefaultTabOfFEISForm();
                Assert.AreEqual("Opportunity Overview", tabDisplayed);
                extentReports.CreateLog("Tab with name:" + tabDisplayed + " is displayed when FEIS Form is opened ");

                //5.  TMTI0088236_Verify the informative message given at the top of the FEIS Form
                string msgInfo = form.ValidateInformativeMessageOnFEISForm();
                Assert.AreEqual("Please check this box and press Save to ensure all required fields are completed.", msgInfo);
                extentReports.CreateLog("Message:" + msgInfo + " is displayed on the top of FEIS Form ");

                //6. TMTI0088240_Verify that the "Opportunity Overview" section will be pre-filled from the Opportunity detail page. 
                string oppFEIS = form.ValidateOppNameL();
                Assert.AreEqual(value, oppFEIS);
                extentReports.CreateLog("Opportunity Name: " + oppFEIS + " in FEIS form matches with Opportunity details page ");

                string clientNBC = form.ValidateClientL();
                Assert.AreEqual(clientName, clientNBC);
                extentReports.CreateLog("Client Company: " + clientNBC + " in FEIS form matches with Opportunity details page ");

                string subjectNBC = form.ValidateSubjectL();
                Assert.AreEqual(subjectName, subjectNBC);
                extentReports.CreateLog("Subject Company: " + subjectNBC + " in FEIS form matches with Opportunity details page ");

                string jobTypeFEIS = form.ValidateJobTypeL();
                Assert.AreEqual(valJobType, jobTypeFEIS);
                extentReports.CreateLog("Job Type: " + jobTypeFEIS + " in FEIS form matches with Opportunity details page ");

                string refTypeFEIS = form.ValidateRefTypeL();
                Assert.AreEqual(valUpdRefType, refTypeFEIS);
                extentReports.CreateLog("Referral Type: " + refTypeFEIS + " in FEIS form matches with Opportunity details page ");

                //7. TMTI0088242_Verify that on inline editing and clicking on the Save button, the application will give an error message for all required fields.
                Assert.IsTrue(form.GetErrorMessagesOnFEISForm(), "Verified all the displayed validations on FEIS form ");
                extentReports.CreateLog("Displayed Validations on FEIS Form is correct ");

                //8. TMTI0088244_ Verify that on choosing "Other" as "Transaction Type", will open up the field to describe the Other Transaction Type
                string desOther = form.ValidateAdditionalFieldsOnTransInfo();
                Assert.AreEqual("*Describe Other Transaction Type", desOther);
                extentReports.CreateLog("Field: " + desOther + " displayed upon selecting Transaction Type as Other ");

                //9. TMTI0088246_Verify that on choosing "Other" as "Legal Structure", will open up a field to describe Other Legal Structure Desc
                string desOtherLegal = form.ValidateAdditionalOtherLegalField();
                Assert.AreEqual("*FEIS - Other Legal Structure Desc", desOtherLegal);
                extentReports.CreateLog("Field: " + desOtherLegal + " displayed upon selecting Legal Structure as Other ");

                //10. TMTI0088248_Verify that choosing "Other" as "Form of Consideration", will make "FEIS - Other Forms of Consideration Desc" as a required field
                string desOtherForm = form.ValidateOtherFormofConsideration();
                Assert.AreEqual("FEIS - Other Forms of Consideration Desc", desOtherForm);
                extentReports.CreateLog("Field: " + desOtherForm + " displayed upon selecting Form of Consideration as Other ");

                //11. TMTI0088250_Verify that choosing "Yes" in "Opinion Parties Affiliated", will open up the field "Opinion Affiliated Parties Summary" to summarize
                string desOpinion = form.ValidateAdditionalOpinionPartiesAffiliated();
                Assert.AreEqual("*Opinion Affiliated Parties Summary", desOpinion);
                extentReports.CreateLog("Field: " + desOpinion + " displayed upon selecting Form of Consideration as Other ");

                //Fill all mandatort details of Transaction Information tab
                form.SaveAllMandatoryFieldsOfTransactionInfoTab();

                ////12.TMTI0088252_ Verify the message given at the top of the "Ownership & Advisors" tab of the FEIS Form
                //string msgOwnership = form.ValidateInformativeMessageOnOwnershipTab();
                //Assert.AreEqual("To Add Target/Subject and/or Counterparty(ies), please use the buttons on the top right of this form.", msgOwnership);
                //extentReports.CreateLog("Message:" + msgOwnership + " is displayed at the top of the 'Ownership & Advisors' tab of FEIS Form ");

                ////13.  TMTI0088254_Verify that the "Add Target/Subject" button is available at the top right corner to add the target company
                //string addTarget = form.ValidateAddTargetButton();
                //Assert.AreEqual("Add Target/Subject", addTarget);
                //extentReports.CreateLog("Button:" + addTarget + " is displayed at the top of the 'Ownership & Advisors' tab of FEIS Form ");

                ////14. TMTI0088256_Verify that clicking the "Add Target/Subject" button opens up the form to fill in all details with the Company field as a required field
                //string companyL = form.ValidateCompanyFieldOnAddTargetDetails();
                //Assert.AreEqual("*\r\nCompany", companyL);
                //extentReports.CreateLog("Mandatory Field:" + companyL + "  with company details is displayed after clicking Add Target/Subject button ");

                ////16. TMTI0088260_Verify that if "Does any PE Firm individually own 10% or more of the equity?" is "Yes", the Add PE Firm button will be enabled to select PE Firm
                //string addPEFirm = form.ValidateSelectPEFirm();
                //Assert.AreEqual("Add PE Firm", addPEFirm);
                //extentReports.CreateLog("Button with name:" + addPEFirm + "  is displayed after selecting Does any PE Firm individually own 10% or more of the equity? as 'Yes' ");

                ////17. TMTI0088262_Verify that clicking the "Add PE Firm" button opens up a dialog box to search and select a company. 
                //string search = form.ValidateSearchWindowUponClickingAddPEFirmButton();
                //Assert.AreEqual("True", search);
                //extentReports.CreateLog("Field to search company is displayed after clicking Add PE Firm button ");

                ////18. TMTI0088264_Verify that clicking the "Add Selected" button of Add PE Firm will add that selected company as a PE Firm
                //string valComp = ReadExcelData.ReadDataMultipleRows(excelPath, "FEISForm", 2, 22);
                //string valCompCounter = ReadExcelData.ReadDataMultipleRows(excelPath, "FEISForm", 2, 23);

                //string valOwnership = ReadExcelData.ReadDataMultipleRows(excelPath, "FEISForm", 2, 2);
                //string msgCompany = form.ValidateAddCompanyDetails(valComp,valOwnership);
                //Assert.AreEqual("Success:", msgCompany);
                //extentReports.CreateLog("Success message is displayed after clicking Add Selected button post selecting company and ownership % ");

                //string edit = form.ValidateEditLinkOfAddedPEFirmDetail();
                //Assert.AreEqual("Edit", edit);
                //string del = form.ValidateDelLinkOfAddedPEFirmDetail();
                //Assert.AreEqual("Del", del);
                //string addedComp = form.ValidateAddedCompanyInPEFirmDetail();
                //Assert.AreEqual(valComp, addedComp);
                //string addedOwnership = form.ValidateAddedOwnershipInAddedPEFirmDetail();
                //Assert.AreEqual(valOwnership+"%", addedOwnership);
                //extentReports.CreateLog("Edit and Delete link with added PE FIrm company along with added ownership % are displayed ");

                ////19. TMTI0088267_Verify that the user is able to edit ownership% of the added PE Firm
                //string updatedOwnership = form.ValidateEditCompanyDetails();
                //Assert.AreEqual("5%", updatedOwnership);
                //extentReports.CreateLog("Updated value of Ownership % is displayed upon editing ownership % ");

                ////20.TMTI0088269_Verify that the user is not able to delete the added PE Firm.
                //string cancelOwnership = form.ValidateCancelCompanyDetails();
                //Assert.AreEqual("5%", cancelOwnership);
                //extentReports.CreateLog("Added PE Firm is not deleted upon clicking Cancel button ");

                ////string deleteOwnership = form.ValidateDeleteCompanyDetails();
                ////Assert.AreEqual("Added PE Firm record has been deleted", deleteOwnership);
                ////extentReports.CreateLog("Added PE Firm got deleted upon clicking Delete button ");

                ////15. TMTI0088258_Verify that clicking "Save Record" will save the target company
                //string msgSuccess = form.SaveTargetCompanyDetails();
                //Assert.AreEqual("True", msgSuccess);
                //extentReports.CreateLog("Target company details are saved after clicking Save button ");

                ////21. TMTI0088271_Verify that the user is able to Edit the added Target Company and that updates are getting reflected in the list view.
                //string updatedOwner =form.EditTargetCompanyDetails(valOwnership);
                //Assert.AreEqual(valOwnership+"%", updatedOwner);
                //extentReports.CreateLog("Details of Target Company are updated after editing Private Equity % ");

                ////22. TMTI0088273_Verify that the user is able to delete the added Target Company.
                //string delOwner = form.DeleteTargetCompanyDetails();
                //Assert.AreEqual("Record has been deleted", delOwner);
                //extentReports.CreateLog("Details of Target Company are deleted after deleting it ");

                ////23. TMTI0088275_Verify that the "Add Counterparty" button is available at the top right corner to add counterparty company.
                //string addCounterparty = form.ValidateAddCounterpartyButton();
                //Assert.AreEqual("Add Counterparty", addCounterparty);
                //extentReports.CreateLog("Button:" + addCounterparty + " is displayed at the top of the 'Ownership & Advisors' tab of FEIS Form ");

                ////24. TMTI0088277_Verify that clicking the "Add Counterparty" button opens up the form to fill in all details with the Company field as a required field.
                //string counterpartyL = form.ValidateCompanyFieldOnAddCounterpartyDetails();
                //Assert.AreEqual("*\r\nCompany", counterpartyL);
                //extentReports.CreateLog("Mandatory Field:" + counterpartyL + "  with company details is displayed after clicking Add Counterparty button ");

                ////26. TMTI0088284_Verify that if "Does any PE Firm individually own 10% or more of the equity?" is "Yes", the Add PE Firm button will be enabled to select PE Firm.
                //string addPEFirmCounter = form.ValidateSelectPEFirm();
                //Assert.AreEqual("Add PE Firm", addPEFirmCounter);
                //extentReports.CreateLog("Button with name:" + addPEFirm + "  is displayed after selecting Does any PE Firm individually own 10% or more of the equity? as 'Yes' ");

                ////27.	TMTI0088290_Verify that clicking the "Add PE Firm" button opens up a dialog box to search and select a company
                //string searchCounter = form.ValidateSearchWindowUponClickingAddPEFirmButton();
                //Assert.AreEqual("True", searchCounter);
                //extentReports.CreateLog("Field to search company is displayed after clicking Add PE Firm button ");

                ////28. TMTI0088292_Verify that clicking the "Add Selected" button of Add PE Firm will add that selected company as a PE Firm
                //string msgCompanyCounter = form.ValidateAddCompanyDetails(valCompCounter, valOwnership);
                //Assert.AreEqual("Success:", msgCompanyCounter);
                //extentReports.CreateLog("Success message is displayed after clicking Add Selected button post selecting company and ownership % ");

                //string editCounter = form.ValidateEditLinkOfAddedPEFirmDetail();
                //Assert.AreEqual("Edit", editCounter);
                //string delCounter = form.ValidateDelLinkOfAddedPEFirmDetail();
                //Assert.AreEqual("Del", delCounter);
                //string addedCompCounter = form.ValidateAddedCompanyInPEFirmDetail();
                //Assert.AreEqual(valCompCounter, addedCompCounter);
                //string addedOwnershipCounter = form.ValidateAddedOwnershipInAddedPEFirmDetail();
                //Assert.AreEqual(valOwnership + "%", addedOwnershipCounter);
                //extentReports.CreateLog("Edit and Delete link with added PE FIrm company along with added ownership % are displayed ");

                ////29. TMTI0088294_Verify that the user is able to edit ownership% of the added PE Firm
                //string updatedOwnershipCounter = form.ValidateEditCompanyDetails();
                //Assert.AreEqual("5%", updatedOwnershipCounter);
                //extentReports.CreateLog("Updated value of Ownership % is displayed upon editing ownership % ");

                ////30. TMTI0088296_ Verify that the user is not able to delete the added PE Firm
                //string cancelOwnershipCounter = form.ValidateCancelCompanyDetails();
                //Assert.AreEqual("5%", cancelOwnershipCounter);
                //extentReports.CreateLog("Added PE Firm is not deleted upon clicking Cancel button ");

                ////string deleteOwnership = form.ValidateDeleteCompanyDetails();
                ////Assert.AreEqual("Added PE Firm record has been deleted", deleteOwnership);
                ////extentReports.CreateLog("Added PE Firm got deleted upon clicking Delete button ");

                ////25. TMTI0088279_ Verify that clicking "Save Record" will save the Counterparty company
                //string addedCounterparty = form.SaveTargetCompanyDetails();
                //Assert.AreEqual("True", addedCounterparty);
                //extentReports.CreateLog("Counterparty details are saved after entering the details and clicking Save button ");

                ////31.  TMTI0088298_Verify that the user is able to Edit the added Counterparty Company and that updates are getting reflected in the list view. 
                //string updatedOwnerCounter = form.EditTargetCompanyDetails(valOwnership);
                //Assert.AreEqual(valOwnership + "%", updatedOwner);
                //extentReports.CreateLog("Details of Counterparty Company are updated after editing Private Equity % ");

                ////32.  TMTI0088300_Verify that the user is able to delete the added Counterparty Company. 
                //string delOwnerCounter = form.DeleteTargetCompanyDetails();
                //Assert.AreEqual("Record has been deleted", delOwner);
                //extentReports.CreateLog("Details of Counterparty Company are deleted after deleting it ");

                //35.   TMTI0088306_ Verify that on clicking the error message "Yes/No", the application will redirect the user to the Relationship Question tab
                //33.  TMTI0088302_Verify that the user is able to check checkboxes for all the questions given under the Form of Opinion tab. 
                string tabRelationship = form.ValidateRelatioshipValidationPostSavingCheckboxesOnFormOfOpinion();
                Assert.AreEqual("Relationship Questions", tabRelationship);
                extentReports.CreateLog("Tab: " + tabRelationship + " is displayed upon clicking error message saying Yes/No and saving all questions under Form of Opinion tab ");

                //34.	TMTI0088304_ Verify that on clicking the save button, the application gives a validation message to answer all the Relationship Questions with Yes or No
                Assert.IsTrue(form.VerifyAllRelatioshipQValidations(), "Verified that displayed validations are same");
                extentReports.CreateLog("Displayed validations for Relationship Questions are displayed as expected ");

                //36.   TMTI0088308_Verify that on selecting the option "Yes" for all relationship questions, another required text field will open up to share an explanation for choosing Yes
                Assert.IsTrue(form.SaveAllQuestionsAsYesAndValidateDisplayedExpTextBox(), "Verified that displayed 'If Yes, Please Explain' fields are same");
                extentReports.CreateLog("Displayed 'If Yes, Please Explain' fields are displayed as expected after selecting Yes for all relationship questions ");

                //37.	TMTI0088310_Verify that on selecting the option "No" for all relationship questions, no text field will open up to share an explanation for choosing No. 
                Assert.IsFalse(form.SaveAllQuestionsAsNoAndValidateDisplayedExpTextBox(), "Verified that no 'If Yes, Please Explain' fields are displayed");
                extentReports.CreateLog("No field like 'If Yes, Please Explain' fields are displayed as expected after selecting No for all relationship questions ");

                //38.   TMTI0088312_Verify that on clicking the Save button all the selected options of the Relationship Questions tab will saved and no error message will appear on the screen
                Assert.IsFalse(form.VerifyNoRelatioshipQValidationsUponSelectingNo(), "Verified that no validations are displayed upon selecting No in Relatioship questions ");
                extentReports.CreateLog("Verified that no validations are displayed upon selecting No in Relationship questions ");

                //40.   TMTI0088316_Verify that on clicking the "Fairness" error message, the application will redirect the user to the Legal Review Criteria tab on the respective questions.
                string legal = form.ValidateLegalReviewCriteriaTabUponClickingFairnessQuestions();
                Assert.AreEqual("Legal Review Criteria", legal);
                extentReports.CreateLog("Tab: " + legal + " is displayed upon clicking Fairness error message ");

                //39.	TMTI0088314_Verify that on clicking the save button, the application gives validation messages to answer the required questions of the Legal Review Criteria tab
                Assert.IsTrue(form.VerifyAllLegalReviewValidations(), "Verified that displayed validations are same");
                extentReports.CreateLog("Displayed validations for Legal Review Criteria tab are displayed as expected ");

                //41.   TMTI0088318_Verify that on clicking the Save button, all the selected options of the Legal Review Criteria tab will get saved and no error message will appear on the screen.
                Assert.IsFalse(form.VerifyNoFairnessValidationIsDisplayedUponSelectingValue(), "Verified that no validations are displayed upon selecting No in Fairness questions ");
                extentReports.CreateLog("Verified that no validations are displayed upon selecting No in Fairness questions ");

                //43.	TMTI0088322_Verify that on clicking the "Opinion Special Committee" error message, the application will redirect the user to the Other Opinion Information tab on the respective questions
                string otherOpinion = form.ValidateOtherOpinionInfoTabUponClickingOpinionSpecialQuestion();
                Assert.AreEqual("Other Opinion Information", otherOpinion);
                extentReports.CreateLog("Tab: " + otherOpinion + " is displayed upon clicking Opinion Special Committee message ");

                //42.   TMTI0088320_Verify that on clicking the save button, the application gives validation messages to answer the required questions of the Other Opinion Information tab
                string messageOpinion = form.VerifyAllOtherOpinionInfoValidations();
                Assert.AreEqual("Opinion Special Committee\r\nComplete this field.", messageOpinion);
                extentReports.CreateLog("Mandatory field validation: " + messageOpinion + " is displayed upon clicking save button on Other Opinion Information tab ");

                //44.	TMTI0088324_Verify that on clicking the Save button, the selected options of the Opinion Special Committee question will get saved and no error message will appear on the screen
                string noMessage = form.VerifyNoValidationIsDisplayedUponSelectingValueOnOtherOpinionInfoTab();
                Assert.AreEqual("No validation is displayed", noMessage);
                extentReports.CreateLog("No mandatory field validation is displayed upon saving value for mandatory field on Other Opinion Information tab ");

                //45.	TMTI0088326_ Verify that on clicking the Form check checkbox (Required to Submit) and clicking on Save, the application will give validation for fields that are required to Submit the FEIS form. 
                //Assert.IsFalse(form.VerifyAllPendingValidations(), "Verified that all validations are displayed upon selecting No in Fairness questions ");
                //extentReports.CreateLog("Verified that validations are displayed as expected ");

                //46.    TMTI0088328_Verify that the user is able to fix validation error messages that appear on submitting the FEIS form
                //Assert.IsFalse(form.SaveAllMandatoryFieldsAndValidateAnyValidations(), "Verified that no validations are displayed upon saving all mandatory fields ");
                //extentReports.CreateLog("Verified that no validations are displayed upon saving all mandatory fields ");

                //47.   TMTI0088330_Verify that the "Submit FEIS Form" button will be enabled at the top right corner once all validation messages are fixed
                string submitFEIS = form.ValidateSubmitFEISFormButton();
                Assert.AreEqual("Submit FEIS (Part I) Form", submitFEIS);
                extentReports.CreateLog("Button: " + submitFEIS + " is displayed after fixing all mandatory validations ");

                //48.   TMTI0088332_Verify that the user will redirected to the Email format with all the FEIS form details by clicking the "Submit FEIS Form" button
                string pageTitle = form.ValidateEmailFormatAfterClickingFEISFormButton();
                Assert.AreEqual("Send Email", pageTitle);
                extentReports.CreateLog(pageTitle + " is displayed after clicking 'Submit FEIS Form' button");

                //Validate Opportunity Name in Email and navigate to Opportunity details page
                string emailOppName = form.GetOppNameL();
                Assert.AreEqual(value, emailOppName);
                extentReports.CreateLog("Email Template with Opportunity " + emailOppName + " is displayed ");

                //49.  TMTI0088334_Verify that the user will be able to Send an Email with all FEIS form details to the desired recipients and redirect the user back to the FEIS form where the submit button disappears and a note in red color will appear on the screen.
                string submitForm = form.ValidateSendEmailFunctionality();
                Assert.AreEqual("Button is not displayed", submitForm);
                extentReports.CreateLog("Button: Submit FEIS (Part I) Form  is not displayed anymore ");

                //50. TMTI0088336_Verify that the user is not able to update the FEIS form anymore once Submitted and will give a warning message.
                string subMessage = form.ValidateIfFormIsEditablePostSubmission();
                Assert.AreEqual("Since this form has previously been sent to the Fairness Engagement Committee, you dont have necessary permission to update", subMessage);
                extentReports.CreateLog("Message: " + subMessage + " is displayed while udpating FEIS form post submiting it ");

                //51. TMTI0088338_Verify that the FVA User is not able to access the Review tab.
                //string tabReview = form.ValidateReviewTabPostSubmissionFVA();
                //Assert.AreEqual("Review tab is not accessible", tabReview);
                //extentReports.CreateLog("Review tab is not accessible to FVA user ");
                usersLogin.DiffLightningLogout();
                usersLogin.DiscardChanges();

                //52. TMTI0088340_Verify that the FVA CAO is able the Review tab where the Reviewed checkbox is checked by default.
                string valCAOUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);
                usersLogin.SearchUserAndLogin(valCAOUser);
                string caoUser = login.ValidateUserLightningCAO();
                Console.WriteLine("caoUser:" + caoUser);
                Console.WriteLine("valCAOUser:" + valCAOUser.Substring(1, 10));
                Assert.AreEqual(caoUser.Contains(valCAOUser.Substring(1, 10)), true);
                extentReports.CreateLog("User: " + valCAOUser + " logged in ");

                opportunityHome.SearchMyOpportunitiesInLightning(value, caoUser);
                opportunityDetails.ClickFEISFormL();

                string tabReviewCAO = form.ValidateReviewTabPostSubmissionCAO();
                Assert.AreEqual("Reviewed", tabReviewCAO);
                extentReports.CreateLog("Review tab along with Review section details is displayed ");

                usersLogin.DiffLightningLogout();
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


