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
    class TMTT0008327_AdditionalClientSubjects_NewEngagementClientSubjectFunctionalities : BaseClass
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

        public static string file8026 = "TMTT0008327_AdditionalClientSubjects_NewEngagementClientSubjectFunctionalities1";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void AdditionalClientAndSubject_NewEngagementClientSubjectFunctionalities()
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
                            else if (valClient.Equals("Ad Results Media, LLC"))
                            {
                                Assert.AreEqual("PE Firm", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else if (valClient.Equals("2Agriculture"))
                            {                                
                                Assert.AreEqual("Subject", additionalClient);
                                extentReports.CreateLog("Company with name : " + valClient + " with Type: " + additionalClient + " is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else
                            {
                                Assert.AreEqual("Key Creditor", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                        }
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




