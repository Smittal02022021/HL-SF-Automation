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
    class TMTT0007939_Opportunity_AdditionalClientAndSubject_NewOpportunity_ClientSubjectFunctionalities : BaseClass
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

        public static string file7935 = "TMTT0007939_Opportunity_AdditionalClientAndSubject2.xlsx";

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
                string excelPath = ReadJSONData.data.filePaths.testData + file7935;
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
                string value = addOpportunity.AddOpportunities(valJobType,file7935);
                Console.WriteLine("value : " + value);

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(file7935);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details  
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                 //Validate the buttons i.e., New Opportunity Client/Subject and Mass Edit Record
                 string buttonClientSubject = opportunityDetails.ValidateVisibilityOfNewOpportunityClientSubjectButton();
                 Assert.AreEqual("New Opportunity Client/Subject", buttonClientSubject);
                 extentReports.CreateLog("Button with name : " + buttonClientSubject + " is displayed on Opportunity Details page ");

                 //Validate the buttons i.e., Mass Edit Records
                 string buttonMassEditRecord = opportunityDetails.ValidateVisibilityOfMassEditRecordsButton();
                 Assert.AreEqual("Mass Edit Records", buttonMassEditRecord);
                 extentReports.CreateLog("Button with name : " + buttonMassEditRecord + " is displayed on Opportunity Details page ");
                                    
                 //Select Record Type, Click Continue and Save the Opportunity Client/Subject 
                 int rowContact = ReadExcelData.GetRowCount(excelPath, "AddContact");
                 Console.WriteLine("rowCount " + rowContact);

                  for (int rowCon = 2; rowCon <= rowContact; rowCon++)
                    {
                        //Validate the title of page upon clicking the New Opportunity Client/Subject button
                        string titeSelectionPage = opportunityDetails.ClickNewOpportunityClientSubjectButton();
                        Assert.AreEqual("Select Opportunity Client/Subject Record Type", titeSelectionPage);
                        extentReports.CreateLog("Page with title : " + titeSelectionPage + " is displayed upon clicking New Opportunity Client/Subject button ");

                        string valType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowCon, 5);
                        opportunityHome.SelectLOBAndClickContinue(valType);
                        string valClient = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowCon, 4);
                        string txtAddedCompany = opportunityDetails.ValidateSaveFunctionalityOfAdditionalClient(valClient, valJobType);
                        extentReports.CreateLog("Details are saved in Opportunity Client/Subject page ");

                        //Validate the added rows under Additional Clients/Subjects section
                        if (valJobType.Equals("Creditor Advisors") && valClient.Equals("Accupac"))
                        {
                            string txtAddedType = opportunityDetails.GetTypeOfAdditionalClient();
                            Assert.AreEqual(valClient, txtAddedCompany);
                            Assert.AreEqual("Client", txtAddedType);
                            extentReports.CreateLog("Company with name : " + txtAddedCompany + " with Type: " + txtAddedType + " is displayed under Additional Clients/Subjects section in Opportunity Details page ");
                            string addedKeyCreditor = opportunityDetails.GetCompanyNameOfFeeAttributionParty();
                            string typeKeyCre = opportunityDetails.GetTypeOfFeeAttributionParty();
                            string recTypeKeyCre = opportunityDetails.GetRecTypeOfFeeAttributionParty();
                            Assert.AreEqual(valClient, addedKeyCreditor);
                            Assert.AreEqual("Key Creditor", typeKeyCre);
                            Assert.AreEqual("Operating Company", recTypeKeyCre);
                            extentReports.CreateLog("Company with name: " + addedKeyCreditor + " with Type: " + typeKeyCre + " and Record Type as: " + recTypeKeyCre + " is displayed in Additional Clients/Subjects section ");
                        }

                        else
                        {
                            string additionalClient = opportunityDetails.ValidateAdditionalSubjectFromPopUp(valJobType,valClient);
                            Console.WriteLine(additionalClient);
                            if (valClient.Equals("ACD"))
                            {
                                Assert.AreEqual("Contra", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else if (valClient.Equals("Accupac"))
                            {
                                Assert.AreEqual("Client", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else if (valClient.Equals("Adobe Oil & Gas"))
                            {
                                Assert.AreEqual("Other", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else if (valClient.Equals("Ad Exchange Group"))
                            {
                                Assert.AreEqual("PE Firm", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                            else
                            {
                                Assert.AreEqual("Key Creditor", additionalClient);
                                extentReports.CreateLog("New company: " + valClient + " for " + additionalClient + " only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
                            }
                        }
                        //Validate the title of page upon clicking Mass Edit Records button
                        string titeMassEditPage = opportunityDetails.ClickMassEditRecordsButton();
                        opportunityDetails.ClickBackToOppButtonAndValidatePage(); 
                        Assert.AreEqual("Additional Clients/Subjects", titeMassEditPage);
                        extentReports.CreateLog("Page with title : " + titeMassEditPage + " is displayed upon clicking Mass Edit Records button ");
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




