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
    class TMTT0008023_Opportunity_AdditionalClientAndSubject_MassEditFunctionalities : BaseClass
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

        public static string file8023 = "TMTT0008023_Opportunity_AdditionalClientAndSubject_MassEditFunctionalities1.xlsx";

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
                string excelPath = ReadJSONData.data.filePaths.testData + file8023;
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
                    string value = addOpportunity.AddOpportunities(valJobType, file8023);
                    Console.WriteLine("value : " + value);

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    clientSubjectsPage.EnterStaffDetails(file8023);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                    Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //Validate the buttons i.e., Mass Edit Records
                    string buttonMassEditRecord = opportunityDetails.ValidateVisibilityOfMassEditRecordsButton();
                    Assert.AreEqual("Mass Edit Records", buttonMassEditRecord);
                    extentReports.CreateLog("Button with name : " + buttonMassEditRecord + " is displayed on Opportunity Details page ");

                    //Validate the title of page upon clicking Mass Edit Records button
                    string titeMassEditPage = opportunityDetails.ClickMassEditRecordsButton();
                    Assert.AreEqual("Additional Clients/Subjects", titeMassEditPage);
                    extentReports.CreateLog("Page with title : " + titeMassEditPage + " is displayed upon clicking Mass Edit Records button ");

                    //-----Validate all the buttons i.e., Additional Clients/Subjects, Delete Records, Edit and Refresh on tht page
                    string btnAdditionalClientSub = clientSubjectsPage.ValidateAdditionalClientSubjectButton();
                    Assert.AreEqual("Additional Clients/Subjects", btnAdditionalClientSub);
                    extentReports.CreateLog("Button with name : " + btnAdditionalClientSub + " is displayed ");

                    //Validate Delete Records button
                    string btnDeleteRecords = clientSubjectsPage.ValidateDeleteRecordsButton();
                    Assert.AreEqual("Delete Records", btnDeleteRecords);
                    extentReports.CreateLog("Button with name : " + btnDeleteRecords + " is displayed ");

                    //Validate Edit button
                    string btnEdit = clientSubjectsPage.ValidateEditButton();
                    Assert.AreEqual("Edit", btnEdit);
                    extentReports.CreateLog("Button with name : " + btnEdit + " is displayed ");

                    //Validate Refresh button
                    string btnRefresh = clientSubjectsPage.ValidateRefreshButton();
                    Assert.AreEqual("Refresh", btnRefresh);
                    extentReports.CreateLog("Button with name : " + btnRefresh + " is displayed ");

                    //Validate Types                  
                    Assert.IsTrue(clientSubjectsPage.VerifyTypes(), "Verified that displayed Types are same");
                    extentReports.CreateLog("Displayed Types are as expected ");

                    //Validate error message without selecting any record and clicking on Delete Records button
                    string alert = clientSubjectsPage.ClickDeleteAndValidateErrorMessage();
                    Assert.AreEqual("Select atleast one record for delete", alert);
                    extentReports.CreateLog("Message : " + alert + " is displayed upon clicking Delete Records button without selecting any record. ");

                    //Validate the error message upon closing it
                    string message = clientSubjectsPage.ClickCloseAndValidateErrorMessage();
                    Assert.AreEqual("No validate message is displayed", message);
                    extentReports.CreateLog(message + " upon closing the error message ");

                    if (valJobType.Equals("Creditor Advisors"))
                    {
                        //Validate Delete functionality
                        string delMessage = clientSubjectsPage.ValidateDeleteRecordsFunctionality();
                        Assert.AreEqual("Record is deleted successfully", delMessage);
                        extentReports.CreateLog(delMessage + " upon selecting a record and clicking Delete Records button ");
                    }
                    else
                    {
                        extentReports.CreateLog("Job Type is Debtor Advisors ");
                    }

                    //Validate columns displayed in the table
                    string Column1 = clientSubjectsPage.Get1stColumn();
                    Console.WriteLine("Column1:" + Column1 + "Column1:");
                    Assert.AreEqual("Client/Subject  ", Column1);

                    //clientSubjectsPage.ValidateTableColumns();
                    Assert.IsTrue(clientSubjectsPage.ValidateTableColumns(), "Verified that displayed columns are same");
                    extentReports.CreateLog("Displayed columns of Table are as expected ");

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

                        }

                        else
                        {
                            string additionalClient = clientSubjectsPage.ValidateAdditionalSubjectFromPopUp(valClient);
                            Console.WriteLine(additionalClient);
                            if (valClient.Equals("ACD"))
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
                        }

                        //Validate Edit button
                        string btnEditSelectedType = clientSubjectsPage.ValidateEditButton();
                        Assert.AreEqual("Edit", btnEditSelectedType);
                        extentReports.CreateLog("Button with name : " + btnEditSelectedType + " is displayed for type: " + valType + " ");

                        //Validate that added companies are displayed upon selecting appropriate Types
                        string type = clientSubjectsPage.SelectValueFromTypeDropdown(valType);
                        Assert.AreEqual(valType, type);
                        extentReports.CreateLog("Selected value is displayed in Type dropdown ");

                        string displayedComp = clientSubjectsPage.GetClientSubjectValue();
                        string displayedType = clientSubjectsPage.GetTypeOfAdditionalClient(valType);
                        Assert.AreEqual(valClient, displayedComp);
                        Assert.AreEqual(valType, displayedType);
                        extentReports.CreateLog("Company with name : " + displayedComp + " with Type: " + displayedType + " is displayed in the table upon selecting value: " + type + " in Type dropdown ");

                        if ( valType.Equals("Client") || valJobType.Equals("Creditor Advisors") && valType.Equals("Key Creditor") || valType.Equals("Subject"))
                        {
                            string displayed2ndComp = clientSubjectsPage.Get2ndClientSubjectValue();
                            string displayed2ndType = clientSubjectsPage.Get2ndTypeOfAdditionalClient(valJobType,valType);
                            Assert.AreEqual(valClient, displayedComp);
                            Assert.AreEqual(valType, displayed2ndType);
                            extentReports.CreateLog("Company with name : " + displayed2ndComp + " with Type: " + displayed2ndType + " is displayed in the table upon selecting value: " + type + " in Type dropdown ");
                        }
                        else
                        {
                            string displayed2ndType = clientSubjectsPage.Get2ndTypeOfAdditionalClient(valJobType, valType);
                            Assert.AreEqual("No 2nd company exists", displayed2ndType);
                            extentReports.CreateLog("No other company with Type: " + valType + " is displayed in the table upon selecting value: " + type + " in Type dropdown ");
                        }

                        //Validate the displayed columns by selecting each value from Type dropdown
                        Assert.IsTrue(clientSubjectsPage.ValidateTableColumnsForEachType(valType), "Verified that displayed columns are same");
                        extentReports.CreateLog("Displayed columns of Table are as expected for each selected Type value ");
                    }
                
                 //Validate the title of page upon clicking Back To Opportunity button
                string titleOppDetailstPage = opportunityDetails.ClickBackToOppButtonAndValidatePage();
                Assert.AreEqual("Opportunity Detail", titleOppDetailstPage);
                extentReports.CreateLog("Page with title : " + titleOppDetailstPage + " is displayed upon clicking Back to Opportunity button ");
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




