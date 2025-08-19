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
    class TMTT0007939_Opportunity_AdditionalClientAndSubject_NewOpportunity_ClientSubjectFunctionalities_Lightning : BaseClass
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
                    string value = addOpportunity.AddOpportunitiesLightning(valJobType, file7935);
                    Console.WriteLine("value : " + value);

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(file7935);
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
                                    
                 //Select Record Type, Click Continue and Save the Opportunity Client/Subject 
                 int rowContact = ReadExcelData.GetRowCount(excelPath, "AddContact");
                 Console.WriteLine("rowCount " + rowContact);

                  for (int rowCon = 2; rowCon <= rowContact; rowCon++)
                    {
                        //Validate the title of page upon clicking the New Opportunity Client/Subject button
                        string titleSelectionPage = opportunityDetails.ClickNewButtonL();
                        Assert.AreEqual("New Opportunity Client/Subject", titleSelectionPage);
                        extentReports.CreateLog("Page with title : " + titleSelectionPage + " is displayed upon clicking New button ");

                        string valType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowCon, 5);
                        opportunityDetails.SelectClientTypeAndClickNext(valType);
                        string valClient = ReadExcelData.ReadDataMultipleRows(excelPath, "AddContact", rowCon, 4);
                        string txtAddedCompany = opportunityDetails.ValidateSaveFunctionalityOfAdditionalClientL(valClient, valJobType,valType);
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
                            string additionalClient = opportunityDetails.ValidateAdditionalSubjectFromPopUpL(valJobType,valClient,valType);
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
                        string titeMassEditPage = opportunityDetails.ClickMassEditRecordsButtonL(valJobType, valType);
                        opportunityDetails.ClickBackToOppButtonAndValidatePageL(); 
                        Assert.AreEqual("Additional Clients/Subjects", titeMassEditPage);
                        extentReports.CreateLog("Page with title : " + titeMassEditPage + " is displayed upon clicking Mass Edit Records button ");

                        opportunityDetails.ValidateClientSubjectAndReferralTabFVAL();
                    }
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




