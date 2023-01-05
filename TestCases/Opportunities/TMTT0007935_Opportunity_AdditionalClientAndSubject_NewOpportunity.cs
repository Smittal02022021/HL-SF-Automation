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
    class TMTT0007935_Opportunity_AdditionalClientAndSubject_NewOpportunity : BaseClass
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

        public static string file7935 = "TMTT0007935_Opportunity_AdditionalClientAndSubject_NewOpportunity1.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void AdditionalClientAndSubject_NewOpportunity()
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
                    string value = addOpportunity.AddOpportunities(valJobType, file7935);
                    Console.WriteLine("value : " + value);

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    clientSubjectsPage.EnterStaffDetails(file7935);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                    Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //Validate added client with new types of Fee Attribution Party and Key Creditor along with additional Client and Subject
                    //--Validate Additional Client and Subject
                    //--Validate added client in Additional Clients/Subjects section
                    string addedCompany = clientSubjectsPage.ValidateAddedClient();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 1), addedCompany);
                    string addedCompanyType = clientSubjectsPage.ValidateTypeOfAddedClient();
                    Assert.AreEqual("Client", addedCompanyType);
                    string addedCompanyRecType = clientSubjectsPage.ValidateRecTypeOfAddedClient();
                    Assert.AreEqual("Operating Company", addedCompanyRecType);
                    extentReports.CreateLog(addedCompany + " with Type: " + addedCompanyType + " and Record Type: " + addedCompanyRecType + " is added in Additional Client/Subject section ");

                    //--Validate added subject in Additional Clients/Subjects section               
                    if (valJobType.Equals("Creditor Advisors"))
                    {
                        string addedSubject = opportunityDetails.GetCompanyNameOfKeyCreditor();
                        string addedSubjectType = opportunityDetails.GetTypeOfKeyCreditor();
                        string addedSubjectRecType = opportunityDetails.GetRecTypeOfKeyCreditor();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 2), addedSubject);
                        Assert.AreEqual("Subject", addedSubjectType);
                        Assert.AreEqual("Operating Company", addedSubjectRecType);
                        extentReports.CreateLog(addedSubject + " with Type: " + addedSubjectType + " and Record Type: " + addedSubjectRecType + " is added in Additional Client/Subject section ");
                    }
                    else
                    {
                        string addedKeyCre = opportunityDetails.GetCompanyNameOfKeyCreditor();
                        string typeKeyCre = opportunityDetails.GetTypeOfKeyCreditor();
                        string recTypeKeyCre = opportunityDetails.GetRecTypeOfKeyCreditor();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 2), addedKeyCre);
                        Assert.AreEqual("Subject", typeKeyCre);
                        Assert.AreEqual("Operating Company", recTypeKeyCre);
                        extentReports.CreateLog("Company with name: " + addedKeyCre + " with Type: " + typeKeyCre + " and Record Type as: " + recTypeKeyCre + " is displayed in Additional Clients/Subjects section ");
                    }

                    //---Validate added client for Key Creditors
                    string addedKey = clientSubjectsPage.ValidateAddedSubjectWithKeyCreditor();
                    string typeKey = clientSubjectsPage.ValidateTypeOfAddedSubject();
                    Console.WriteLine("typeKey: " + typeKey);
                    string recTypeKey = clientSubjectsPage.ValidateRecTypeOfAddedSubject();
                    Console.WriteLine("recTypeKey: " + recTypeKey);
                    if (valJobType.Equals("Creditor Advisors"))
                    {
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 1), addedKey);
                        Assert.AreEqual("Key Creditor", typeKey);
                        Assert.AreEqual("Operating Company", recTypeKey);
                        extentReports.CreateLog("Company with name: " + addedKey + " with Type: " + typeKey + " and Record Type as: " + recTypeKey + " is displayed in Additional Clients/Subjects section ");
                    }

                    else
                    {
                        Assert.AreEqual("No new client exists", addedKey);
                        Assert.AreNotEqual("Key Creditor", typeKey);
                        Assert.AreNotEqual("Key Creditor", recTypeKey);
                        extentReports.CreateLog("No company with Key Creditors exists in Additional Clients/Subjects section ");
                    }

                    //Update Additional Client and Subject to Yes and validate title of Additional Client/Subject Required Pop up  
                    opportunityDetails.UpdateAdditionalClientandSubject();
                    Assert.AreEqual(clientSubjectsPage.ValidateAdditionalClientSubjectTitle(), "Additional Clients/Subjects Required");
                    extentReports.CreateLog(clientSubjectsPage.ValidateAdditionalClientSubjectTitle() + " is displayed upon setting Yes for Additional Client and Subject ");

                    //Clicking Add Client and validating title of Add Additional Client(s) Pop up
                    clientSubjectsPage.ClickAddClient();
                    Console.WriteLine(clientSubjectsPage.ValidateAdditionalClientTitle());
                    Assert.AreEqual(clientSubjectsPage.ValidateAdditionalClientTitle(), "Add Additional Client(s)");
                    extentReports.CreateLog(clientSubjectsPage.ValidateAdditionalClientTitle() + " window is displayed ");

                    //Calling Add AdditionalClient function and validating message
                    clientSubjectsPage.AddAdditionalClient(file7935);
                    Assert.AreEqual("Success:" + '\r' + '\n' + "Company Added To Additional Clients", clientSubjectsPage.ValidateMessage());
                    extentReports.CreateLog(clientSubjectsPage.ValidateMessage() + " is displayed ");
                    clientSubjectsPage.CloseClientPopUp();

                    //Validate additional clients in Table               
                    Assert.AreEqual("True", clientSubjectsPage.ValidateTableDetails());
                    extentReports.CreateLog("Client is added in Additional Clients Section ");

                    //Calling AddAdditionalSubject function and validating message
                    clientSubjectsPage.AddAdditionalSubject(file7935);
                    Assert.AreEqual("Success:" + '\r' + '\n' + "Company Added To Additional Subjects", clientSubjectsPage.ValidateSubjectMessage());
                    Console.WriteLine("Message: " + clientSubjectsPage.ValidateSubjectMessage());
                    extentReports.CreateLog(clientSubjectsPage.ValidateSubjectMessage() + " is displayed ");
                    clientSubjectsPage.CloseSubjectPopUp();

                    //Validate additional Subject in Table                               
                    Assert.AreEqual("True", clientSubjectsPage.ValidateSubjectTableDetails());
                    extentReports.CreateLog("Subject is added in Additional Subjects Section ");

                    //Call selectClientInterest funtion 
                    clientSubjectsPage.selectClientInterest(file7935);

                    //--Validate added subject in Additional Clients/Subjects section while added from additional client Subject Pop Up
                    string additionalSubject = opportunityDetails.ValidateAdditionalSubjectFromPopUp(valJobType, ReadExcelData.ReadData(excelPath, "AddOpportunity", 35));
                    Assert.AreEqual("Subject", additionalSubject);
                    extentReports.CreateLog("New company: " + ReadExcelData.ReadData(excelPath, "AddOpportunity", 35) + " for Subject is displayed in Additional Clients/Subjects section upon adding Subject from Additional Clients/Subjects pop up for " + valJobType + " ");

                    if (valJobType.Equals("Creditor Advisors"))
                    {
                        //--Validate added client and Key Creditor in Additional Clients/Subjects section while added from additional client Subject Pop Up
                        string additionalClient = opportunityDetails.ValidateAdditionalClientFromPopUp(ReadExcelData.ReadData(excelPath, "AddOpportunity", 33));
                        Assert.AreEqual("Client", additionalClient);
                        extentReports.CreateLog("New company: " + ReadExcelData.ReadData(excelPath, "AddOpportunity", 33) + " for Client is displayed in Additional Clients/Subjects section upon adding Subject from Additional Clients/Subjects pop up for " + valJobType + " ");

                        string additionalKeyCred = opportunityDetails.GetCompanyNameOfFeeAttributionParty();
                        string additionalKeyCredKey = opportunityDetails.GetTypeOfFeeAttributionParty();
                        string additionalKeyCredRecType = opportunityDetails.GetRecTypeOfFeeAttributionParty();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 33), additionalKeyCred);
                        Assert.AreEqual("Key Creditor", additionalKeyCredKey);
                        Assert.AreEqual("Operating Company", additionalKeyCredRecType);
                        extentReports.CreateLog("Company with name: " + additionalKeyCred + " with Type: " + additionalKeyCredKey + " and Record Type as: " + additionalKeyCredRecType + " is displayed in Additional Clients/Subjects section upon adding client from Additional Clients/Subjects pop up " + valJobType + " ");
                    }
                    else
                    {
                        string additionalClient = opportunityDetails.ValidateAdditionalSubjectFromPopUp(valJobType, ReadExcelData.ReadData(excelPath, "AddOpportunity", 33));
                        Assert.AreEqual("Client", additionalClient);
                        extentReports.CreateLog("New company: " + ReadExcelData.ReadData(excelPath, "AddOpportunity", 33) + " for Client only is displayed in Additional Clients/Subjects section upon adding Client from Additional Clients/Subjects pop up for " + valJobType + " ");
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




