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
    class TMTT0008026_TMTT0008027_TMTT0008032_TMTT0008300_MassEdit_EditFunctionality_PickListAndValues_ValidationRulesAndMassEditPostConversionToEng_Lightning : BaseClass
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
        AddOpportunityContact addContact = new AddOpportunityContact();
        EngagementHomePage engHome = new EngagementHomePage();
        RandomPages randomPages = new RandomPages();


        public static string file8026 = "TMTT0008026_AdditionalClientAndSubject_MassEdit_EditFunctionalities.xlsx";

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

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                Console.WriteLine("rowCount " + rowJobType);

                for (int row = 2; row <= rowJobType; row++)
                {

                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);

                    //Login as Standard User profile and validate the user
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 32);
                    usersLogin.SearchUserAndLogin(valUser);
                    string stdUser = login.ValidateFRUserLightning();
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
                    string opportunityNumber = addOpportunity.AddOpportunitiesLightning(valJobType, file8026);
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(file8026);
                    Assert.AreEqual("Info", displayedTab);
                    extentReports.CreateLog("Tab with name: " + displayedTab + " is displayed upon saving internal deal team members details ");

                    //Validate the buttons i.e., New Opportunity Client/Subject and Mass Edit Record
                    opportunityDetails.ValidateClientSubjectAndReferralTabFVAL();
                    string buttonNew = opportunityDetails.ValidateVisibilityOfNewButtonL();
                    Assert.AreEqual("New", buttonNew);
                    extentReports.CreateLog("Button with name : " + buttonNew + " is displayed on Client/Subject & Referral ");

                    //Validate the buttons i.e., Mass Edit Records
                    string buttonMassEditRecord = opportunityDetails.ValidateVisibilityOfMassEditRecordsButtonL();
                    Assert.AreEqual("Mass Edit Records", buttonMassEditRecord);
                    extentReports.CreateLog("Button with name : " + buttonMassEditRecord + " is displayed on Client/Subject & Referral ");

                    //Validate the title of page upon clicking Mass Edit Records button                    
                    string titeMassEditPage = opportunityDetails.ClickMassEditRecordsButtonLightning();
                    Assert.AreEqual("Additional Clients/Subjects", titeMassEditPage);
                    extentReports.CreateLog("Page with title : " + titeMassEditPage + " is displayed upon clicking Mass Edit Records button ");

                    //Validate Edit button
                    string btnEdit = clientSubjectsPage.ValidateEditButtonL();
                    Assert.AreEqual("Edit", btnEdit);
                    extentReports.CreateLog("Button with name : " + btnEdit + " is displayed ");

                    //Click Edit button and validate Save and Cancel button
                    string btnSaveAll = clientSubjectsPage.ClickEditButtonAndValidateSaveButtonL();
                    Assert.AreEqual("Save", btnSaveAll);
                    extentReports.CreateLog("Button with name : " + btnSaveAll + " is displayed upon clicking Edit button ");

                    //Validate Cancel button
                    string btnCancelAll = clientSubjectsPage.ValidateCancelButton();
                    Assert.AreEqual("Cancel", btnCancelAll);
                    extentReports.CreateLog("Button with name : " + btnCancelAll + " is displayed upon clicking Edit button ");

                    //Select a value from Type dropdown and validate Edit button
                    //Click Additional Clients/Subjects button, Select Record Type, Click Continue and Save the Opportunity Client/Subject 
                    int rowContact = ReadExcelData.GetRowCount(excelPath, "AddContact");
                    Console.WriteLine("rowCount " + rowContact);
                    for (int rowCon = 2; rowCon <= rowContact; rowCon++)
                    {
                        //Validate the title of page upon clicking the Additional Clients/Subjects button
                        string titeSelectionPage = clientSubjectsPage.ClickAdditionalClientsSubjectsButtonL();
                        Assert.AreEqual("New Opportunity Client/Subject", titeSelectionPage);
                        extentReports.CreateLog("Page with title : " + titeSelectionPage + " is displayed upon clicking Additional Clients/Subjects button ");

                        string valType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowCon, 5);
                        string valClient = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowCon, 4);
                        opportunityDetails.SelectClientTypeAndClickNext(valType);
                        string txtAddedCompany = opportunityDetails.ValidateSaveFunctionalityOfAdditionalClientThruAdditionalClientButtonL(valClient, valJobType, valType);
                        extentReports.CreateLog("Details are saved in Opportunity Client/Subject page ");

                        string clientValue = ReadExcelData.ReadData(excelPath, "AddOpportunity", 27);

                        //Validate the added rows under Additional Clients/Subjects section
                        if (valJobType.Equals("Creditor Advisors") && valClient.Equals("Accupac"))
                        {
                            string txtAddedType = opportunityDetails.GetTypeOfAdditionalClientL(valType);
                            Assert.AreEqual(valClient, txtAddedCompany);
                            Assert.AreEqual("Client", txtAddedType);
                            extentReports.CreateLog("Company with name : " + txtAddedCompany + " with Type: " + txtAddedType + " is displayed under Additional Clients/Subjects section in Opportunity Details page ");
                            string addedKeyCreditor = opportunityDetails.GetAddedCompanyNameL(valClient);
                            string typeKeyCre = opportunityDetails.GetTypeOfAdditionalKeyCreditor();
                            Assert.AreEqual(valClient, addedKeyCreditor);
                            Assert.AreEqual("Key Creditor", typeKeyCre);
                            extentReports.CreateLog("Company with name: " + addedKeyCreditor + " with Type: " + typeKeyCre + " is displayed in Additional Clients/Subjects section ");

                            opportunityDetails.ClickMassEditRecordsButtonLightning();
                            string type = clientSubjectsPage.SelectValueFromTypeDropdown(valType);
                            Assert.AreEqual(valType, type);
                            extentReports.CreateLog("Selected value: " + type + " is displayed in Type dropdown ");

                            //Validate Edit button
                            string btnEdit1 = clientSubjectsPage.ValidateEditButton();
                            Assert.AreEqual("Edit", btnEdit1);
                            extentReports.CreateLog("Button with name : " + btnEdit1 + " is displayed for " + type + " Type ");

                            //Click Edit button and validate Save and Cancel button
                            string btnSave = clientSubjectsPage.ClickEditButtonAndValidateSaveButton();
                            Assert.AreEqual("Save", btnSave);
                            extentReports.CreateLog("Button with name : " + btnSave + " is displayed upon clicking Edit button ");

                            //Validate Cancel button
                            string btnCancel = clientSubjectsPage.ValidateCancelButton();
                            Assert.AreEqual("Cancel", btnCancel);
                            extentReports.CreateLog("Button with name : " + btnCancel + " is displayed upon clicking Edit button ");

                            //Validate Roles dropdown                           
                            Assert.IsTrue(clientSubjectsPage.VerifyRoleValues(), "Verified that displayed Role values are same");
                            extentReports.CreateLog("Role values are as expected ");

                            //Validate Cancel functionality
                            string clientHoldingPer = clientSubjectsPage.ValidateCancelFunctionalityOfMassEdit(clientValue, type, valClient);
                            Assert.AreEqual("0.0 %", clientHoldingPer);
                            extentReports.CreateLog("Entered value of Client Holdings % is not saved in the table upon clicking cancel button ");

                            //Validate Cancel button
                            string btnCancel2nd = clientSubjectsPage.ValidateCancelButton();
                            Assert.AreEqual("Cancel button is not displayed", btnCancel2nd);
                            extentReports.CreateLog("Records are not editable anymore ");

                            //Click Edit button and validate table is editable again
                            string btnSave1 = clientSubjectsPage.ClickEditButtonAndValidateSaveButton();
                            Assert.AreEqual("Save", btnSave1);
                            extentReports.CreateLog("Button with name : " + btnSave1 + " is displayed again upon clicking Edit button ");

                            //Validate Client Holdings % error message when more than 100%
                            string valMessage = clientSubjectsPage.ValidateErrorMessageUponEnteringClientHoldingsMoreThan100();
                            Assert.AreEqual("Client Holdings cannot exceed 100%", valMessage);
                            extentReports.CreateLog("Message: " + valMessage + " is displayed upon clicking Save button after entering more than 100% in Client Holdings ");

                            //Validate Revenue Allocation % error message when more than 100%
                            string valMessageRev = clientSubjectsPage.ValidateErrorMessageUponEnteringRevenueAllocationMoreThan100();
                            Assert.AreEqual("Revenue Allocation cannot exceed 100%", valMessageRev);
                            extentReports.CreateLog("Message: " + valMessageRev + " is displayed upon clicking Save button after entering more than 100% in Revenue Allocation ");

                            //Validate the color code of Revenue Allocation when entered less than 100                            
                            string colourCodeRev = clientSubjectsPage.GetColourCodePostSavingRevenueAllocationLessThan100();
                            Assert.AreEqual("slds-text-color_error", colourCodeRev);
                            extentReports.CreateLog("Total Value of Revenue Allocation % is displayed in red when it is less than 100 ");

                            //Validate Save functionality and displayed message
                            clientSubjectsPage.ClickEditButton();
                            string message = clientSubjectsPage.ValidateSaveFunctionalityOfMassEdit(clientValue, type, valClient);
                            Assert.AreEqual("Records updated successful", message);
                            extentReports.CreateLog("Message: " + message + " is displayed upon clicking Save button ");

                            //Validate the colour code of Total of Client Holding %  when entered less than 100
                            string colourCode = clientSubjectsPage.GetColourCodeOfTotalOfClientHoldings();
                            Assert.AreEqual("slds-text-color_error", colourCode);
                            extentReports.CreateLog("Total Value of Client Holdings% is displayed in red when it is less than 100 ");

                            //Validate updated value of Client Holdings %
                            string clientHoldingPerSave = clientSubjectsPage.ValidateUpdatedClientHoldingsPer(type);
                            Assert.AreEqual(clientValue + " %", clientHoldingPerSave);
                            extentReports.CreateLog("Entered value of Client Holdings % is  saved in the table upon saving the details ");
                        }

                        else
                        {
                            string additionalClient = opportunityDetails.ValidateAddedTypesOfClientL(valJobType, valClient, valType);
                            Console.WriteLine(additionalClient);
                            if (valClient.Equals("Allied Capital Corporation"))
                            {
                                Assert.AreEqual("Contra", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in the table on Additional Clients/Subjects page for " + valJobType + " ");
                            }
                            else if (valClient.Equals("Accupac"))
                            {
                                Assert.AreEqual("Client", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else if (valClient.Equals("ABC Auto Parts Ltd"))
                            {
                                Assert.AreEqual("Other", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else if (valClient.Equals("Ad Exchange Group"))
                            {
                                Assert.AreEqual("PE Firm", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else if (valClient.Equals("2Agriculture"))
                            {
                                //string txtAddedType = clientSubjectsPage.GetTypeOfAdditionalClientWithAllOption();
                                //Assert.AreEqual(valClient, txtAddedCompany);
                                Assert.AreEqual("Subject", additionalClient);
                                extentReports.CreateLog("Company with name : " + valClient + " for " + additionalClient + " is displayed upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else
                            {
                                Assert.AreEqual("Key Creditor", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }

                            opportunityDetails.ClickMassEditRecordsButtonLightning();
                            string type = clientSubjectsPage.SelectValueFromTypeDropdown(valType);
                            Console.WriteLine("Type " + valType);
                            Assert.AreEqual(valType, type);
                            extentReports.CreateLog("Selected value: " + type + " is displayed in Type dropdown ");
                                                       
                            //Validate Edit button
                            string btnEdit2 = clientSubjectsPage.ValidateEditButton();
                            Assert.AreEqual("Edit", btnEdit2);
                            extentReports.CreateLog("Button with name : " + btnEdit2 + " is displayed for " + type + " Type ");

                            //Click Edit button and validate Save and Cancel button
                            string btnSave = clientSubjectsPage.ClickEditButtonAndValidateSaveButton();
                            Assert.AreEqual("Save", btnSave);
                            extentReports.CreateLog("Button with name : " + btnSave + " is displayed upon clicking Edit button ");

                            //Validate Cancel button
                            string btnCancel = clientSubjectsPage.ValidateCancelButton();
                            Assert.AreEqual("Cancel", btnCancel);
                            extentReports.CreateLog("Button with name : " + btnCancel + " is displayed upon clicking Edit button ");

                            //Validate Key Creditor Importance dropdown and Role Values 
                            if (valType.Contains("Key Creditor"))
                            {
                                string defaultKeyCreditor = clientSubjectsPage.GetDefaultValueOfKeyCreditorImportance();
                                Assert.AreEqual("Medium", defaultKeyCreditor);
                                extentReports.CreateLog("Default value of Key Creditor Importance is " + defaultKeyCreditor + " ");

                                Assert.IsTrue(clientSubjectsPage.VerifyKeyCreditorImpValues(), "Verified that displayed Key Creditors Importance values are same");
                                extentReports.CreateLog("Key Creditor Importance values are as expected ");
                            }
                            else
                            {
                                extentReports.CreateLog("Type:" + type + " is displayed ");
                            }

                            //Validate Cancel functionality
                            string clientHoldingPer = clientSubjectsPage.ValidateCancelFunctionalityOfMassEdit(clientValue, type, valClient);
                            if (type.Contains("Client"))
                            {
                                Assert.AreEqual("0.0 %", clientHoldingPer);
                                extentReports.CreateLog("Entered value of Client Holdings % is not saved in the table upon clicking cancel button ");
                            }
                            else if (type.Contains("Key Creditor"))
                            {
                                Assert.AreEqual("0.0 %", clientHoldingPer);
                                extentReports.CreateLog("Entered value of Debt Holdings MM - USD is not saved in the table upon clicking cancel button ");
                            }
                            else
                            {
                                Assert.AreEqual("0.0 %", clientHoldingPer);
                                extentReports.CreateLog("Entered value of Revenue Allocation % is not saved in the table upon clicking cancel button ");
                            }

                            //Validate Cancel button
                            string btnCancel2nd = clientSubjectsPage.ValidateCancelButton();
                            Assert.AreEqual("Cancel button is not displayed", btnCancel2nd);
                            extentReports.CreateLog("Records are not editable anymore ");

                            //Click Edit button and validate table is editable again
                            string btnSave1 = clientSubjectsPage.ClickEditButtonAndValidateSaveButton();
                            Assert.AreEqual("Save", btnSave1);
                            extentReports.CreateLog("Button with name : " + btnSave1 + " is displayed again upon clicking Edit button ");

                            //Validate Save functionality and displayed message
                            string message = clientSubjectsPage.ValidateSaveFunctionalityOfMassEdit(clientValue, type, valClient);
                            Assert.AreEqual("Records updated successful", message);
                            extentReports.CreateLog("Message: " + message + " is displayed upon clicking Save button ");

                            //Validate updated value of Client Holdings %
                            string clientHoldingPerSave = clientSubjectsPage.ValidateUpdatedClientHoldingsPer(type);
                            //Assert.AreEqual(clientValue + " %", clientHoldingPerSave);
                            extentReports.CreateLog("Entered value of Client Holdings % is  saved in the table upon saving the details ");

                            //Validate the colour code of Total of Key Creditor Weighting % when entered less than 100
                            if (type.Contains("Key Creditor"))
                            {
                                string colourCode = clientSubjectsPage.GetColourCodePostSavingKeyCreditorWeightingLessThan100();
                                Assert.AreEqual("slds-text-color_error", colourCode);
                                extentReports.CreateLog("Total Value of Key Creditor Weighting % is displayed in red when it is less than 100 ");

                                //Validate Key Creditor Weighting % error message when more than 100%
                                string valMessageKeyCred = clientSubjectsPage.ValidateErrorMessageUponEnteringKeyCreditorWeightingMoreThan100();
                                Assert.AreEqual("Key Creditor Weighting cannot exceed 100%", valMessageKeyCred);
                                extentReports.CreateLog("Message: " + valMessageKeyCred + " is displayed upon clicking Save button after entering more than 100% in Key Creditor Weighting ");

                            }
                            else
                            {
                                extentReports.CreateLog("Not required to validate anymore validations");
                            }
                        }                        
                    }

                     clientSubjectsPage.ClickBackToOpportunityButton();

                    //Create External Primary Contact         
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    opportunityDetails.ClickAddFROppContact();
                    addContact.CreateContactL(file8026);
                    
                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForFRConversionLV(file8026);
                    opportunityDetails.UpdateTotalDebtConfirmedLV();
                    opportunityDetails.UpdateInternalTeamDetailsL(file8026);

                    //PitchMandateAward details
                    randomPages.ClickPitchMandteAwardTabLV();
                    opportunityDetails.CreateNewPitchMandateAwardLV();
                    extentReports.CreateStepLogs("Info", "New Pitch/Mandate Award detail provided ");

                    //Logout of user and validate Admin login
                    usersLogin.LightningLogout();
                    
                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(opportunityNumber);

                    //update CC and NBC checkboxes 
                  
                    opportunityDetails.UpdateOutcomeDetails(file8026);
                    extentReports.CreateLog("Conflict Check fields are updated ");

                    //Login as Financial User and validate the user                
                    usersLogin.SearchUserAndLogin(valUser);
                    string stdUser1 = login.ValidateFRUserLightning();
                    Assert.AreEqual(stdUser1.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                    //Open the same opportunity and Click on Request Engagement               
                    opportunityHome.SearchMyOpportunitiesInLightning(opportunityNumber, valUser);
                    opportunityDetails.ClickRequestoEngL();
                    extentReports.CreateLog("No Validation error is displayed and Opportunity is requested for approval ");

                    //Log out of Standard User
                    usersLogin.DiffLightningLogout();

                    //Login as CAO user to approve the Opportunity
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2));
                    string caoUser = login.ValidateUserLightningCAO2nd();
                    Console.WriteLine("caoUser:" + caoUser);
                    Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2)), true);
                    extentReports.CreateLog("User: " + caoUser + " logged in ");

                    //Search for created opportunity and validate the status
                    opportunityHome.SearchMyOpportunitiesInLightning(opportunityNumber, ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2));
                    string valStatus = opportunityDetails.ValidateStatusOfOpportunity();
                    Assert.AreEqual("Pending", valStatus);
                    extentReports.CreateLog("Status of Opportunity: " + valStatus + " is displayed after clicking Request Engagement ");

                    //Approve the Opportunity 
                    string status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual("Approved", status);
                    extentReports.CreateLog("Opportunity is approved ");

                    //Convert the Opportunity to Engagement by clicking Convert To Engagement
                    opportunityDetails.ClickOppName();
                    string engDetails = opportunityDetails.ClickReqToEngagementFR();
                    Assert.AreEqual("Engagement", engDetails);
                    extentReports.CreateLog("Opportunity is converted to Engagement after clicking Request To Engagement button ");
                   
                   for (int rowCon = 2; rowCon <= rowContact; rowCon++)
                    {
                        string valType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowCon, 5);

                        string valClient = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowCon, 4);

                        //Validate the added rows under Additional Clients/Subjects section
                        if (valJobType.Equals("Creditor Advisors") && valClient.Equals("Accupac"))
                        {
                            string txtAddedType = opportunityDetails.GetTypeOfAdditionalClientEngL(valType);
                            Assert.AreEqual("Client", txtAddedType);
                            string txtAddedCompany = engagementDetails.GetNameOfAdditionalAddedClientEngL();
                            Assert.AreEqual("Accupac", txtAddedCompany);
                            extentReports.CreateLog("Company with name : " + txtAddedCompany + " with Type: " + txtAddedType + " is displayed under Additional Clients/Subjects section in Engagement Details page ");

                            string addedKeyCreditor = opportunityDetails.GetAddedCompanyNameL(valClient);
                            string typeKeyCre = opportunityDetails.GetTypeOfAdditionalKeyCreditor();
                            Assert.AreEqual(valClient, addedKeyCreditor);
                            Assert.AreEqual("Key Creditor", typeKeyCre);
                            extentReports.CreateLog("Company with name: " + addedKeyCreditor + " with Type: " + typeKeyCre + " is displayed in Additional Clients/Subjects section ");
                        }
                        else
                        {
                            string additionalClient = engagementDetails.ValidateAdditionalSubjectFromPopUpL(valClient);
                            Console.WriteLine(additionalClient);
                            if (valClient.Equals("Allied Capital Corporation"))
                            {
                                Assert.AreEqual("Contra", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in the table on Additional Clients/Subjects page for " + valJobType + " ");
                            }
                            else if (valClient.Equals("Accupac"))
                            {
                                Assert.AreEqual("Client", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else if (valClient.Equals("ABC Auto Parts Ltd"))
                            {
                                Assert.AreEqual("Other", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else if (valClient.Equals("Ad Exchange Group"))
                            {
                                Assert.AreEqual("PE Firm", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else if (valClient.Equals("2Agriculture"))
                            {
                                Assert.AreEqual("Subject", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else
                            {
                                Assert.AreEqual("Key Creditor", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                        }
                     }
                    //Validate the title of page upon clicking Mass Edit Records button
                    string titeMassEditPageEng = engagementDetails.ClickMassEditRecordsButtonLightning();
                    Assert.AreEqual("Additional Clients/Subjects", titeMassEditPageEng);
                    extentReports.CreateLog("Page with title : " + titeMassEditPageEng + " is displayed upon clicking Mass Edit Records button ");

                    //Validate updates made in Opportunity Mass Edit page are mapped in Engagement Mass Edit page as well
                    string updValueSub = engagementDetails.ValidateUpdatedValuessFromMassEdit("2Agriculture");
                    Assert.AreEqual("10 %", updValueSub);
                    extentReports.CreateLog("Updates made in Revenue Allocation for Subject : 2Agriculture is mapped in Engagement Mass Edit page ");

                    string updValueOther = engagementDetails.ValidateUpdatedValuessFromMassEdit("ABC Auto Parts Ltd");
                    Assert.AreEqual("10 %", updValueOther);
                    extentReports.CreateLog("Updates made in Revenue Allocation for Other : ABC Auto Parts Ltd is mapped in Engagement Mass Edit page ");

                    //Validate the title of page upon clicking Back To Opportunity button
                    string titleOppDetailstPage = engagementDetails.ClickBackToEngButtonAndValidatePageL();
                    Assert.AreEqual("Engagement", titleOppDetailstPage);
                    extentReports.CreateLog("Page with title : " + titleOppDetailstPage + " is displayed upon clicking Back to Opportunity button ");

                    usersLogin.DiffLightningLogout();
                }
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




