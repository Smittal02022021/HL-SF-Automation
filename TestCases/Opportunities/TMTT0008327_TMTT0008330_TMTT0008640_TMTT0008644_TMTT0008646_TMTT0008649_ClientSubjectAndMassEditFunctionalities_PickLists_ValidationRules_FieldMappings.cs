using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunity
{
    class TMTT0008327_TMTT0008330_TMTT0008640_TMTT0008644_TMTT0008646_TMTT0008649_ClientSubjectAndMassEditFunctionalities_PickLists_ValidationRules_FieldMappings : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();        
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engHome = new EngagementHomePage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();

        public static string file8330 = "TMTT0008330_AdditionalClientSubjects_MassEditFunctionalities2";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void AdditionalClientAndSubject_MassEditFunctionalities()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + file8330;
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
                    string value = addOpportunity.AddOpportunities(valJobType, file8330);
                    Console.WriteLine("value : " + value);

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    clientSubjectsPage.EnterStaffDetails(file8330);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                    Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact         
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    addOpportunityContact.CreateContact(file8330, valContact, valRecordType, valContactType);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForFRConversion(file8330);
                    opportunityDetails.UpdateInternalTeamDetails(file8330);

                    //Logout of user and validate Admin login
                    usersLogin.UserLogOut();
                    Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(file8330);
                    extentReports.CreateLog("Conflict Check fields are updated ");

                    //Login again as Standard User
                    usersLogin.SearchUserAndLogin(valUser);
                    string stdUser1 = login.ValidateUser();
                    Assert.AreEqual(stdUser1.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //Get currency of Total Debt
                    string debtCurrency = opportunityDetails.GetTotalDebtCurrency();
                    string dCurrency = debtCurrency.Substring(0, 3);
                    extentReports.CreateLog("Total Debt Currency: " + debtCurrency + " is displayed ");

                    //Get Total Debt MM value
                    string totalDebt = opportunityDetails.GetTotalDebtMM();
                    string debt = totalDebt.Substring(0, 4);
                    extentReports.CreateLog("Total Debt MM's valie: " + totalDebt + " is displayed in Opportunity details page ");
                    
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
                                        
                    usersLogin.UserLogOut();

                    //Login again as Standard User
                    usersLogin.SearchUserAndLogin(valUser);
                    string stdUser2 = login.ValidateUser();
                    Assert.AreEqual(stdUser2.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser2 + " logged in ");

                    //Search for same engagement
                    engHome.SearchEngagementWithName(engName);

                    //Validate the buttons i.e.,New Engagement Client/Subject button and Mass Edit Records button
                    string btnAdditonalClientSub = engagementDetails.ValidateNewEngClientSubjectButton();
                    Assert.AreEqual("New Engagement Client/Subject", btnAdditonalClientSub);
                    extentReports.CreateLog("Button with name : " + btnAdditonalClientSub + " is displayed in Additional Clients/Subjects section ");

                    string btnMassEditRecords = engagementDetails.ValidateMassEditRecordsButton();
                    Assert.AreEqual("Mass Edit Records", btnMassEditRecords);
                    extentReports.CreateLog("Button with name : " + btnMassEditRecords + " is displayed in Additional Clients/Subjects section ");

                    //Validate the title of page upon clicking Mass Edit Records button
                    string titleAdditionalClientSubEng = engagementDetails.ClickMassEditRecordsButton();
                    Assert.AreEqual("Additional Clients/Subjects", titleAdditionalClientSubEng);
                    extentReports.CreateLog("Page with title : " + titleAdditionalClientSubEng + " is displayed upon clicking Mass Edit Records button ");

                    //-----Validate all the buttons i.e., Additional Clients/Subjects, Delete Records, Edit and Refresh on tht page
                    string btnAdditionalClientSub = engagementDetails.ValidateAdditionalClientSubjectButton();
                    Assert.AreEqual("Additional Clients/Subjects", btnAdditionalClientSub);
                    extentReports.CreateLog("Button with name : " + btnAdditionalClientSub + " is displayed ");

                    //Validate Delete Records button
                    string btnDeleteRecords = engagementDetails.ValidateDeleteRecordsButton();
                    Assert.AreEqual("Delete Records", btnDeleteRecords);
                    extentReports.CreateLog("Button with name : " + btnDeleteRecords + " is displayed ");

                    //Validate Edit button
                    string btnEdit = engagementDetails.ValidateEditButton();
                    Assert.AreEqual("Edit", btnEdit);
                    extentReports.CreateLog("Button with name : " + btnEdit + " is displayed ");

                    //Validate Refresh button
                    string btnRefresh = engagementDetails.ValidateRefreshButton();
                    Assert.AreEqual("Refresh", btnRefresh);
                    extentReports.CreateLog("Button with name : " + btnRefresh + " is displayed ");

                    //Validate Types                  
                    Assert.IsTrue(engagementDetails.VerifyTypes(), "Verified that displayed Types are same");
                    extentReports.CreateLog("Displayed Types are as expected ");

                    //Validate error message without selecting any record and clicking on Delete Records button
                    string alert = engagementDetails.ClickDeleteAndValidateErrorMessage();
                    Assert.AreEqual("Select atleast one record for delete", alert);
                    extentReports.CreateLog("Message : " + alert + " is displayed upon clicking Delete Records button without selecting any record. ");

                    //Validate the error message upon closing it
                    string message = engagementDetails.ClickCloseAndValidateErrorMessage();
                    Assert.AreEqual("No validate message is displayed", message);
                    extentReports.CreateLog(message + " upon closing the error message ");
                                     
                    //Validate columns displayed in the table
                    string Column1 = engagementDetails.Get1stColumn();
                    Console.WriteLine("Column1:" + Column1 + "Column1:");
                    Assert.AreEqual("Client/Subject  ", Column1);                    
                    Assert.IsTrue(engagementDetails.ValidateTableColumns(), "Verified that displayed columns are same");
                    extentReports.CreateLog("Displayed columns of Table are as expected ");
                    engagementDetails.ClickBackToEngButtonAndValidatePage();

                    //Click Additional Clients/Subjects button, Select Record Type, Click Continue and Save the Opportunity Client/Subject 
                    int rowContact = ReadExcelData.GetRowCount(excelPath, "AddContact");
                    Console.WriteLine("rowCount " + rowContact);
                    for (int rowCon = 2; rowCon <= rowContact; rowCon++)
                    {
                        //Validate the title of page upon clicking the New Engagement Client/Subject button
                        string titleMassEditPageEng = engagementDetails.ClickAdditionalClientSubjectButton();
                        Assert.AreEqual("Select Engagement Client/Subject Record Type", titleMassEditPageEng);
                        extentReports.CreateLog("Page with title : " + titleMassEditPageEng + " is displayed upon clicking New Engagement Client/Subject button ");

                        string valType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowCon, 5);
                        opportunityHome.SelectLOBAndClickContinue(valType);
                        string valClient = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowCon, 4);
                        string txtAddedCompany = clientSubjectsPage.ValidateSaveFunctionalityOfEngAdditionalClient(valClient,valJobType);
                        extentReports.CreateLog("Details are saved in Engagement Client/Subject page ");

                        //Validate the added rows under Additional Clients/Subjects section
                        if (valJobType.Equals("Creditor Advisors") && valClient.Equals("Accupac"))
                        {
                            string txtAddedType = clientSubjectsPage.GetTypeOfEngAdditionalClientWithAllOption();
                            Assert.AreEqual(valClient, txtAddedCompany);
                            Assert.AreEqual("Client", txtAddedType);
                            extentReports.CreateLog("Company with name : " + txtAddedCompany + " with Type: " + txtAddedType + " is displayed in the table on Additional Clients/Subjects page ");
                            string addedKeyCreditor = clientSubjectsPage.GetCompanyNameOfEngKeyCreditor();
                            string typeKeyCre = clientSubjectsPage.GetTypeOfEngKeyCreditor();
                            Assert.AreEqual(valClient, addedKeyCreditor);
                            Assert.AreEqual("Key Creditor", typeKeyCre);
                            extentReports.CreateLog("Company with name: " + addedKeyCreditor + " with Type: " + typeKeyCre + " is displayed in the table on Additional Clients/Subjects page ");

                            //Validate Delete functionality
                            engagementDetails.ClickMassEditRecordsButton();
                            string delMessage = clientSubjectsPage.ValidateDeleteRecordsFunctionalityOfMassEdit();
                            Assert.AreEqual("Record is deleted successfully", delMessage);
                            extentReports.CreateLog(delMessage + " upon selecting a record and clicking Delete Records button ");
                       
                         }

                        else
                        {
                            string additionalClient = engagementDetails.ValidateEngAdditionalSubjectFromPopUp(valClient, valJobType);
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
                                Assert.AreEqual("Subject", additionalClient);
                                extentReports.CreateLog("Company with name : " + valClient + " with Type: " + additionalClient + " is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else if (valClient.Equals("Bel Pastry Inc.")) 
                            {
                                Assert.AreEqual("Counterparty", additionalClient);
                                extentReports.CreateLog("Company with name : " + valClient + " with Type: " + additionalClient + " is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else if (valClient.Equals("Bell & Howell"))
                            {
                                Assert.AreEqual("Equity Holder", additionalClient);
                                extentReports.CreateLog("Company with name : " + valClient + " with Type: " + additionalClient + " is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else
                            {
                                Assert.AreEqual("Key Creditor", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            engagementDetails.ClickMassEditRecordsButton();
                        }

                        //Validate Edit button
                        string btnEditAll = clientSubjectsPage.ValidateEditButton();
                        Assert.AreEqual("Edit", btnEditAll);
                        extentReports.CreateLog("Button with name : " + btnEditAll + " is displayed for type All ");

                        //Validate that added companies are displayed upon selecting appropriate Types
                        string type = engagementDetails.SelectValueFromTypeDropdown(valType);
                        Assert.AreEqual(valType, type);
                        extentReports.CreateLog("Selected value is displayed in Type dropdown ");

                        string btnEditSelectedType = clientSubjectsPage.ValidateEditButton();
                        Assert.AreEqual("Edit", btnEditSelectedType);
                        extentReports.CreateLog("Button with name : " + btnEditSelectedType + " is displayed for type: " + valType + " ");
                        
                       string displayedComp = clientSubjectsPage.GetClientSubjectValue();
                       string displayedType = clientSubjectsPage.GetTypeOfAdditionalClient(valType);
                       if (valType.Equals("Client") && valJobType.Equals("Debtor Advisors"))
                        {
                            Assert.AreEqual("Accupac", displayedComp);
                            Assert.AreEqual(valType, displayedType);
                            extentReports.CreateLog("Company with name : " + displayedComp + " with Type: " + displayedType + " is displayed in the table upon selecting value: " + type + " in Type dropdown ");
                        }
                       else if (valType.Equals("Client") && valJobType.Equals("Creditor Advisors"))
                        {
                            Assert.AreEqual("Plackal Techno", displayedComp);
                            Assert.AreEqual(valType, displayedType);
                            extentReports.CreateLog("Company with name : " + displayedComp + " with Type: " + displayedType + " is displayed in the table upon selecting value: " + type + " in Type dropdown ");
                        }
                        else
                        {
                            Assert.AreEqual(valClient, displayedComp);
                            
                            Assert.AreEqual(valType, displayedType);
                            extentReports.CreateLog("Company with name : " + displayedComp + " with Type: " + displayedType + " is displayed in the table upon selecting value: " + type + " in Type dropdown ");
                        }

                        if (valType.Equals("Client") || valJobType.Equals("Creditor Advisors") && valType.Equals("Key Creditor") || valType.Equals("Subject"))
                        {
                            if (valType.Equals("Client") && valJobType.Equals("Debtor Advisors"))
                            {
                                string displayed2ndComp = clientSubjectsPage.Get2ndClientSubjectValue();
                                string displayed2ndType = clientSubjectsPage.Get2ndTypeOfAdditionalClient(valJobType, valType);
                                Assert.AreEqual(valClient, displayedComp);
                                Assert.AreEqual(valType, displayed2ndType);
                                extentReports.CreateLog("Company with name : " + displayed2ndComp + " with Type: " + displayed2ndType + " is displayed in the table upon selecting value: " + type + " in Type dropdown ");

                                //Get currency of column of Client Holdings (MM) and validate if it is same as Total Debt currency
                                string colClientHoldings = clientSubjectsPage.GetClientHoldingsMM();
                                Console.WriteLine("colClientHoldings: " + colClientHoldings);
                                string clientCurrency = colClientHoldings.Substring(23, 3);
                                Console.WriteLine("Currency: " + clientCurrency);
                                Assert.AreEqual(dCurrency, clientCurrency);
                                extentReports.CreateLog("Column Client Holdings (MM): " + clientCurrency + " is suffixed with same currency as of Total Debt Currency in Opportunity details page ");

                                //Get currency of column of Debt Holdings (MM) and validate if it is same as Total Debt currency
                                string colDebtHoldings = clientSubjectsPage.GetDebtHoldingsMM();
                                Console.WriteLine("colDebtHoldings: " + colDebtHoldings);
                                string debtColCurrency = colDebtHoldings.Substring(21, 3);
                                Console.WriteLine("Currency: " + debtColCurrency);
                                Assert.AreEqual(dCurrency, debtColCurrency);
                                extentReports.CreateLog("Column Debt Holdings (MM): " + debtColCurrency + " is suffixed with same currency as of Total Debt Currency in Opportunity details page ");
                            }
                            else if (valJobType.Equals("Creditor Advisors") && valType.Equals("Client"))
                            {//Need to check it
                                string displayed2ndComp = clientSubjectsPage.GetOtherCreditorsValue();                               
                                Assert.AreEqual("Other Creditors", displayed2ndComp);
                                extentReports.CreateLog(displayed2ndComp + " is displayed in the table instead of any other company ");
                            }
                            else if (valJobType.Equals("Creditor Advisors") && valType.Equals("Key Creditor"))
                            {
                                string displayed2ndComp = clientSubjectsPage.Get2ndClientSubjectValue();
                                string displayed2ndType = clientSubjectsPage.Get2ndTypeOfAdditionalClient(valJobType, valType);
                                Assert.AreEqual(valClient, displayedComp);
                                Assert.AreEqual(valType, displayed2ndType);
                                extentReports.CreateLog("Company with name : " + displayed2ndComp + " with Type: " + displayed2ndType + " is displayed in the table upon selecting value: " + type + " in Type dropdown ");

                            }
                            else
                            {
                                string displayed2ndComp = clientSubjectsPage.Get2ndClientSubjectValue();
                                string displayed2ndType = clientSubjectsPage.Get2ndTypeOfAdditionalClient(valJobType, valType);
                                Assert.AreEqual(valClient, displayedComp);
                                Assert.AreEqual(valType, displayed2ndType);
                                extentReports.CreateLog("Company with name : " + displayed2ndComp + " with Type: " + displayed2ndType + " is displayed in the table upon selecting value: " + type + " in Type dropdown ");
                            }
                        }
                        else
                        {
                            string displayed2ndType = clientSubjectsPage.Get2ndTypeOfAdditionalClient(valJobType, valType);
                            Assert.AreEqual("No 2nd company exists", displayed2ndType);
                            extentReports.CreateLog("No other company with Type: " + valType + " is displayed in the table upon selecting value: " + type + " in Type dropdown ");
                        }

                        //Validate the displayed columns by selecting each value from Type dropdown
                        Assert.IsTrue(engagementDetails.ValidateTableColumnsForEachType(valType), "Verified that displayed columns are same");
                        extentReports.CreateLog("Displayed columns of Table are as expected for type : " +valType+" as per selected Type value ");

                        if (valJobType.Equals("Creditor Advisors"))
                        {
                            //Click on Edit button and validate Save and Cancel button
                            string btnSave = clientSubjectsPage.ClickEditButtonAndValidateSaveButton();
                            Assert.AreEqual("Save", btnSave);
                            extentReports.CreateLog("Button with name : " + btnSave + " is displayed upon clicking Edit button ");

                            //Validate Cancel button
                            string btnCancel = clientSubjectsPage.ValidateCancelButton();
                            Assert.AreEqual("Cancel", btnCancel);
                            extentReports.CreateLog("Button with name : " + btnCancel + " is displayed upon clicking Edit button ");

                            if (valType.Equals("Client"))
                            {
                                //Validate Roles dropdown                           
                                Assert.IsTrue(clientSubjectsPage.VerifyRoleValuesInEngagement(), "Verified that displayed Role values are same");
                                extentReports.CreateLog("Role values are as expected ");
                            }
                            else if(valType.Equals("Key Creditor"))
                            {
                                string defaultKeyCreditor = clientSubjectsPage.GetDefaultValueOfKeyCreditorImportanceInEngagement();
                                Assert.AreEqual("Medium", defaultKeyCreditor);
                                extentReports.CreateLog("Default value of Key Creditor Importance is " + defaultKeyCreditor + " ");

                                Assert.IsTrue(clientSubjectsPage.VerifyKeyCreditorImpValuesInEngagement(), "Verified that displayed Key Creditors Importance values are same");
                                extentReports.CreateLog("Key Creditor Importance values are as expected ");
                                
                            }
                            else
                            {
                                Console.WriteLine("No need to validate picklist ");
                            }
                            //Validate Cancel functionality
                            string clientValue = ReadExcelData.ReadData(excelPath, "AddOpportunity", 27);
                            string clientHoldingPer = clientSubjectsPage.ValidateCancelFunctionalityOfMassEditOfEngagement(clientValue, type);
                            if (valType.Equals("Client") || valType.Equals("Key Creditor"))
                            {
                                Assert.AreEqual("0.0 %", clientHoldingPer);
                            }
                            else
                            {
                                Assert.AreEqual("0.0", clientHoldingPer);
                            }
                            extentReports.CreateLog("Entered value is not saved in the table upon clicking cancel button ");

                            //Click Edit button and validate table is editable again
                            string btnSave1 = clientSubjectsPage.ClickEditButtonAndValidateSaveButton();
                            Assert.AreEqual("Save", btnSave1);
                            extentReports.CreateLog("Button with name : " + btnSave1 + " is displayed again upon clicking Edit button ");

                            //Validate Save functionality and displayed message                        
                            string message1 = clientSubjectsPage.ValidateSaveFunctionalityOfMassEditOfEngagement(valJobType, type);
                            Assert.AreEqual("Records updated successful", message1);
                            extentReports.CreateLog("Message: " + message1 + " is displayed upon clicking Save button ");
                        }
                        else
                        {
                            if (valType.Equals("Client"))
                            {
                                //Validate Client Holdings % error message when more than 100%
                                clientSubjectsPage.ClickEditButtonAndValidateSaveButton();
                                string valMessage = clientSubjectsPage.ValidateErrorMessageUponEnteringClientHoldingsMoreThan100InEng();
                                Assert.AreEqual("Client Holdings cannot exceed 100%", valMessage);
                                extentReports.CreateLog("Message: " + valMessage + " is displayed upon clicking Save button after entering more than 100% in Client Holdings ");

                                //Validate Revenue Allocation % error message when more than 100%
                                string valMessageRev = clientSubjectsPage.ValidateErrorMessageUponEnteringRevenueAllocationMoreThan100InEng();
                                Assert.AreEqual("Revenue Allocation cannot exceed 100%", valMessageRev);
                                extentReports.CreateLog("Message: " + valMessageRev + " is displayed upon clicking Save button after entering more than 100% in Revenue Allocation ");

                                //Validate the color code of Revenue Allocation when entered less than 100                            
                                string colourCodeRev = clientSubjectsPage.GetColourCodePostSavingRevenueAllocationLessThan100InEng();
                                Assert.AreEqual("slds-text-color_error", colourCodeRev);
                                extentReports.CreateLog("Total Value of Revenue Allocation % is displayed in red when it is less than 100 ");

                                //Validate the colour code of Total of Client Holding %  when entered less than 100
                                clientSubjectsPage.ClickEditButtonAndValidateSaveButton();
                                clientSubjectsPage.SaveClientHoldings();
                                string colourCode = clientSubjectsPage.GetColourCodeOfTotalOfClientHoldings();
                                Assert.AreEqual("slds-text-color_error", colourCode);
                                extentReports.CreateLog("Total Value of Client Holdings% is displayed in red when it is less than 100 ");

                            }
                            else if (valType.Equals("Key Creditor"))
                            {
                                clientSubjectsPage.ClickEditButtonAndValidateSaveButton();

                                //Validate Key Creditor Weighting % error message when more than 100%
                                string valMessageKeyCred = clientSubjectsPage.ValidateErrorMessageUponEnteringKeyCreditorWeightingMoreThan100InEng();
                                Assert.AreEqual("Key Creditor Weighting cannot exceed 100%", valMessageKeyCred);
                                extentReports.CreateLog("Message: " + valMessageKeyCred + " is displayed upon clicking Save button after entering more than 100% in Key Creditor Weighting ");

                                clientSubjectsPage.SaveKeyCreditorWeighting();
                                string colourCode = clientSubjectsPage.GetColourCodePostSavingKeyCreditorWeightingLessThan100();
                                Assert.AreEqual("slds-text-color_error", colourCode);
                                extentReports.CreateLog("Total Value of Key Creditor Weighting % is displayed in red when it is less than 100 ");

                                //Validate Total Debt Holding and other creditors upon adding more creditors
                                clientSubjectsPage.ClickEditButton();

                                //Get value of Total Debt Holdings MM and validate if it is same as Total Debt (MM) in opportunity
                                string valTotalDebt = clientSubjectsPage.GetTotalDebtHoldingsMM();
                                Assert.AreEqual(debt, valTotalDebt);
                                extentReports.CreateLog("Total of Column Debt Holdings MM is matching with Total Debt MM pulled from Opportunity ");

                                //Get the Other Creditors of Debt Holdings (MM) 
                                clientSubjectsPage.ClearAllDebtHoldingsOfEngagement();
                                string valCreditor = clientSubjectsPage.GetOtherCreditorsOfDebtHoldingsMM();
                                Assert.AreEqual(debt, valCreditor);
                                extentReports.CreateLog("Other Creditor of Debt Holdings MM is matching with Total Debt MM pulled from Opportunity when there is no other Debt Holdings ");

                                //Update few Key Creditors
                                clientSubjectsPage.UpdateAllDebtHoldingsOfEngagement();
                                string valCreditorLatest = clientSubjectsPage.GetOtherCreditorsOfDebtHoldingsMM();
                                Assert.AreNotEqual(valTotalDebt, valCreditorLatest);
                                extentReports.CreateLog("Other Creditor of Debt Holdings MM is updated according to match with Total Debt MM pulled from Opportunity when other Debt Holdings are updated ");
                                clientSubjectsPage.ResetAllDebtHoldingsOfEngagement();

                            }
                            else
                            {
                                Console.WriteLine("No need to validate validations ");
                            }
                        }
                        engagementDetails.ClickBackToEngButtonAndValidatePage();
                    }
                    
                    usersLogin.UserLogOut();
                }
                usersLogin.UserLogOut();
                driver.Quit();
            }

            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}




