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
    class TMTT0008026_TMTT0008027_TMTT0008032_TMTT0008300_MassEdit_EditFunctionality_PickListAndValues_ValidationRulesAndMassEditPostConversionToEng : BaseClass
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

                    //Calling AddOpportunities function                  
                    string value = addOpportunity.AddOpportunities(valJobType, file8026);
                    Console.WriteLine("value : " + value);

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    clientSubjectsPage.EnterStaffDetails(file8026);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                    Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //Validate the title of page upon clicking Mass Edit Records button
                    string titeMassEditPage = opportunityDetails.ClickMassEditRecordsButton();
                    Assert.AreEqual("Additional Clients/Subjects", titeMassEditPage);
                    extentReports.CreateLog("Page with title : " + titeMassEditPage + " is displayed upon clicking Mass Edit Records button ");

                    //Validate Edit button
                    string btnEditAll = clientSubjectsPage.ValidateEditButton();
                    Assert.AreEqual("Edit", btnEditAll);
                    extentReports.CreateLog("Button with name : " + btnEditAll + " is displayed for all Type ");

                    //Click Edit button and validate Save and Cancel button
                    string btnSaveAll = clientSubjectsPage.ClickEditButtonAndValidateSaveButton();
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
                        string titeSelectionPage = clientSubjectsPage.ClickAdditionalClientsSubjectsButton();
                        Assert.AreEqual("Select Opportunity Client/Subject Record Type", titeSelectionPage);
                        extentReports.CreateLog("Page with title : " + titeSelectionPage + " is displayed upon clicking Additional Clients/Subjects button ");

                        string valType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowCon, 5);
                        opportunityHome.SelectLOBAndClickContinue(valType);
                        string valClient = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowCon, 4);
                        string txtAddedCompany = clientSubjectsPage.ValidateSaveFunctionalityOfAdditionalClient(valClient);
                        extentReports.CreateLog("Details are saved in Opportunity Client/Subject page ");
                        string clientValue = ReadExcelData.ReadData(excelPath, "AddOpportunity", 27);

                        //Validate the added rows under Additional Clients/Subjects section
                        if (valJobType.Equals("Creditor Advisors") && valClient.Equals("Accupac"))
                        {
                            string txtAddedType = clientSubjectsPage.GetTypeOfAdditionalClientWithAllOption();
                            Assert.AreEqual(valClient, txtAddedCompany);
                            Assert.AreEqual("Client", txtAddedType);
                            extentReports.CreateLog("Company with name : " + txtAddedCompany + " with Type: " + txtAddedType + " is displayed in the table on Additional Clients/Subjects page ");
                            string addedKeyCreditor = clientSubjectsPage.GetCompanyNameOfKeyCreditor();
                            string typeKeyCre = clientSubjectsPage.GetTypeOfKeyCreditor();
                            Assert.AreEqual(valClient, addedKeyCreditor);
                            Assert.AreEqual("Key Creditor", typeKeyCre);
                            extentReports.CreateLog("Company with name: " + addedKeyCreditor + " with Type: " + typeKeyCre + " is displayed in the table on Additional Clients/Subjects page ");

                            string type = clientSubjectsPage.SelectValueFromTypeDropdown(valType);
                            Assert.AreEqual(valType, type);
                            extentReports.CreateLog("Selected value: " + type + " is displayed in Type dropdown ");

                            //Validate Edit button
                            string btnEdit = clientSubjectsPage.ValidateEditButton();
                            Assert.AreEqual("Edit", btnEdit);
                            extentReports.CreateLog("Button with name : " + btnEdit + " is displayed for " + type + " Type ");

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
                            string message = clientSubjectsPage.ValidateSaveFunctionalityOfMassEdit(clientValue, type);
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
                            string additionalClient = clientSubjectsPage.ValidateAdditionalSubjectFromPopUp(valClient);
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
                            else if (valClient.Equals("ABC"))
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
                                string txtAddedType = clientSubjectsPage.GetTypeOfAdditionalClientWithAllOption();
                                Assert.AreEqual(valClient, txtAddedCompany);
                                Assert.AreEqual("Subject", txtAddedType);
                                extentReports.CreateLog("Company with name : " + txtAddedCompany + " with Type: " + txtAddedType + " is displayed in the table on Additional Clients/Subjects page ");
                            }
                            else
                            {
                                Assert.AreEqual("Key Creditor", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            string type = clientSubjectsPage.SelectValueFromTypeDropdown(valType);
                            Console.WriteLine("Type " + valType);
                            Assert.AreEqual(valType, type);
                            extentReports.CreateLog("Selected value: " + type + " is displayed in Type dropdown ");
                                                       
                            //Validate Edit button
                            string btnEdit = clientSubjectsPage.ValidateEditButton();
                            Assert.AreEqual("Edit", btnEdit);
                            extentReports.CreateLog("Button with name : " + btnEdit + " is displayed for " + type + " Type ");

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
                            string message = clientSubjectsPage.ValidateSaveFunctionalityOfMassEdit(clientValue, type);
                            Assert.AreEqual("Records updated successful", message);
                            extentReports.CreateLog("Message: " + message + " is displayed upon clicking Save button ");

                            //Validate updated value of Client Holdings %
                            string clientHoldingPerSave = clientSubjectsPage.ValidateUpdatedClientHoldingsPer(type);
                            Assert.AreEqual(clientValue + " %", clientHoldingPerSave);
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
                    addOpportunityContact.CreateContact(file8026, valContact, valRecordType, valContactType);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForFRConversion(file8026);
                    opportunityDetails.UpdateInternalTeamDetails(file8026);

                    //Logout of user and validate Admin login
                    usersLogin.UserLogOut();
                    Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(file8026);
                    extentReports.CreateLog("Conflict Check fields are updated ");

                    //Login again as Standard User
                    usersLogin.SearchUserAndLogin(valUser);
                    string stdUser1 = login.ValidateUser();
                    Assert.AreEqual(stdUser1.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //Requesting for engagement and validate the success message
                    string msgSuccess = opportunityDetails.ClickRequestEng();
                    Assert.AreEqual(msgSuccess, "Submission successful! Please wait for the approval");
                    extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");

                    //Log out of Standard User
                    usersLogin.UserLogOut();

                    //Login as CAO user to approve the Opportunity
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 2));
                    string caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                    extentReports.CreateLog("User: " + caoUser + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //Approve the Opportunity 
                    opportunityDetails.ClickApproveButton();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog("Opportunity is approved ");

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEng();

                    //Validate the Engagement name in Engagement details page
                    string engName = engagementDetails.GetEngName();
                    Assert.AreEqual(opportunityNumber, engName);
                    extentReports.CreateLog("Name of Engagement : " + engName + " is similar to Opportunity name ");

                    for (int rowCon = 2; rowCon <= rowContact; rowCon++)
                    {

                        string valClient = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowCon, 4);

                        //Validate the added rows under Additional Clients/Subjects section
                        if (valJobType.Equals("Creditor Advisors") && valClient.Equals("Accupac"))
                        {
                            string txtAddedType = engagementDetails.GetTypeOfAdditionalAddedClient();
                            Assert.AreEqual("Client", txtAddedType);
                            string txtAddedCompany = engagementDetails.GetNameOfAdditionalAddedClient();
                            Assert.AreEqual("Accupac", txtAddedCompany);
                            extentReports.CreateLog("Company with name : " + txtAddedCompany + " with Type: " + txtAddedType + " is displayed under Additional Clients/Subjects section in Engagement Details page ");

                            string addedKeyCreditor = engagementDetails.GetCompanyNameOfAddedKeyCreditor();
                            string typeKeyCre = engagementDetails.GetTypeOfAdditionalAddedKeyCreditor();
                            Assert.AreEqual(valClient, addedKeyCreditor);
                            Assert.AreEqual("Key Creditor", typeKeyCre);
                            extentReports.CreateLog("Company with name: " + addedKeyCreditor + " with Type: " + typeKeyCre + " is displayed in Additional Clients/Subjects section ");
                        }

                        else
                        {
                            string additionalClient = engagementDetails.ValidateAdditionalSubjectFromPopUp(valClient);
                            Console.WriteLine(additionalClient);
                            if (valClient.Equals("Allied Capital Corporation"))
                            {
                                Assert.AreEqual("Contra", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else if (valClient.Equals("Accupac"))
                            {
                                Assert.AreEqual("Client", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else if (valClient.Equals("ABC"))
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
                    string titeMassEditPageEng = engagementDetails.ClickMassEditRecordsButton();
                    Assert.AreEqual("Additional Clients/Subjects", titeMassEditPageEng);
                    extentReports.CreateLog("Page with title : " + titeMassEditPageEng + " is displayed upon clicking Mass Edit Records button ");

                    //Validate updates made in Opportunity Mass Edit page are mapped in Engagement Mass Edit page as well
                    string updValueSub = engagementDetails.ValidateUpdatedValuessFromMassEdit("2Agriculture");
                    Assert.AreEqual("10 %", updValueSub);
                    extentReports.CreateLog("Updates made in Revenue Allocation for Subject : 2Agriculture is mapped in Engagement Mass Edit page ");

                    string updValueOther = engagementDetails.ValidateUpdatedValuessFromMassEdit("ABC");
                    Assert.AreEqual("10 %", updValueOther);
                    extentReports.CreateLog("Updates made in Revenue Allocation for Other : ABC is mapped in Engagement Mass Edit page ");

                    //Validate the title of page upon clicking Back To Opportunity button
                    string titleOppDetailstPage = engagementDetails.ClickBackToEngButtonAndValidatePage();
                    Assert.AreEqual("Engagement Detail", titleOppDetailstPage);
                    extentReports.CreateLog("Page with title : " + titleOppDetailstPage + " is displayed upon clicking Back to Opportunity button ");

                    usersLogin.UserLogOut();
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




