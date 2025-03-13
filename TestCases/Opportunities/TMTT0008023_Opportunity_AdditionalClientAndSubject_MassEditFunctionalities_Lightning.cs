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
    class TMTT0008023_Opportunity_AdditionalClientAndSubject_MassEditFunctionalities_Lightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();        
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
                    string value = addOpportunity.AddOpportunitiesLightning(valJobType, file8023);
                    extentReports.CreateLog("Page with title: " + titleOpp + " is displayed upon clicking next button ");
                    Console.WriteLine("Opportunity with number : " + value + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(file8023);
                    Assert.AreEqual("Info", displayedTab);
                    extentReports.CreateLog("Tab with name: " + displayedTab + " is displayed upon saving internal deal team members details ");

                    //Validate the buttons i.e., New Opportunity Client/Subject and Mass Edit Record
                    opportunityDetails.ValidateClientSubjectAndReferralTabL();
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

                    //-----Validate all the buttons i.e., Additional Clients/Subjects, Delete Records, Edit and Refresh on tht page
                    string btnAdditionalClientSub = clientSubjectsPage.ValidateAdditionalClientSubjectButtonL();
                    Assert.AreEqual("Additional Clients/Subjects", btnAdditionalClientSub);
                    extentReports.CreateLog("Button with name : " + btnAdditionalClientSub + " is displayed ");

                    //Validate Delete Records button
                    string btnDeleteRecords = clientSubjectsPage.ValidateDeleteRecordsButtonL();
                    Assert.AreEqual("Delete Records", btnDeleteRecords);
                    extentReports.CreateLog("Button with name : " + btnDeleteRecords + " is displayed ");

                    //Validate Edit button
                    string btnEdit = clientSubjectsPage.ValidateEditButtonL();
                    Assert.AreEqual("Edit", btnEdit);
                    extentReports.CreateLog("Button with name : " + btnEdit + " is displayed ");

                    //Validate Refresh button
                    string btnRefresh = clientSubjectsPage.ValidateRefreshButton();
                    Assert.AreEqual("Refresh", btnRefresh);
                    extentReports.CreateLog("Refresh button is displayed ");

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
                       string titeSelectionPage= clientSubjectsPage.ClickAdditionalClientsSubjectsButtonL();
                        Assert.AreEqual("New Opportunity Client/Subject", titeSelectionPage);
                        extentReports.CreateLog("Page with title : " + titeSelectionPage + " is displayed upon clicking Additional Clients/Subjects button ");

                        string valType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowCon, 5);
                        string valClient = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowCon, 4);
                        opportunityDetails.SelectClientTypeAndClickNext(valType);
                        string txtAddedCompany = opportunityDetails.ValidateSaveFunctionalityOfAdditionalClientThruAdditionalClientButtonL(valClient, valJobType, valType);
                        extentReports.CreateLog("Details are saved in Opportunity Client/Subject page ");

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
                        }
                        else
                        {
                            string additionalClient = opportunityDetails.ValidateAddedTypesOfClientL(valJobType, valClient, valType);
                            Console.WriteLine(additionalClient);
                            if (valClient.Equals("ACD Direct"))
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

                        //Validate Edit button
                        opportunityDetails.ClickMassEditRecordsButtonLightning();
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
                string titleOppDetailstPage = opportunityDetails.ClickBackToOppButtonAndValidatePageL();
                Assert.AreEqual("Details", titleOppDetailstPage);
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




