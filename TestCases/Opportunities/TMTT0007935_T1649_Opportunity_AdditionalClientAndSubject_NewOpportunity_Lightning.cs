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
    class TMTT0007935_T1649_Opportunity_AdditionalClientAndSubject_NewOpportunity_Lightning : BaseClass
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

                    //Validate added client with new types of Fee Attribution Party and Key Creditor along with additional Client and Subject
                    //--Validate Additional Client and Subject
                    //--Validate added client in Additional Clients/Subjects section
                    opportunityDetails.ValidateClientSubjectAndReferralTabFVAL();
                    string addedCompany = clientSubjectsPage.ValidateAddedClientL();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 1), addedCompany);
                    Console.WriteLine("addedCompany:" + addedCompany);
                    string addedCompanyType = clientSubjectsPage.ValidateTypeOfAddedClientL();
                    Assert.AreEqual("Client", addedCompanyType);
                    Console.WriteLine("addedCompanyType:" + addedCompanyType);
                    //string addedCompanyRecType = clientSubjectsPage.ValidateRecTypeOfAddedClientL();
                    //Assert.AreEqual("Operating Company", addedCompanyRecType);
                    //extentReports.CreateLog(addedCompany + " with Type: " + addedCompanyType + " and Record Type: " + addedCompanyRecType + " is added in Additional Client/Subject section ");
                    extentReports.CreateLog(addedCompany + " with Type: " + addedCompanyType + " is added in Additional Client/Subject section ");

                    //--Validate added subject in Additional Clients/Subjects section               
                    if (valJobType.Equals("Creditor Advisors"))
                    {
                        string addedSubject = clientSubjectsPage.ValidateAddedSubjectWithKeyCreditorL();
                        string addedSubjectType = clientSubjectsPage.ValidateTypeOfAddedSubjectL();
                        //string addedSubjectRecType = clientSubjectsPage.ValidateRecTypeOfAddedSubjectL();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 2), addedSubject);
                        Assert.AreEqual("Subject", addedSubjectType);
                        //Assert.AreEqual("Operating Company", addedSubjectRecType);
                        //extentReports.CreateLog(addedSubject + " with Type: " + addedSubjectType + " and Record Type: " + addedSubjectRecType + " is added in Additional Client/Subject section ");
                        extentReports.CreateLog(addedSubject + " with Type: " + addedSubjectType + " is added in Additional Client/Subject section ");
                    }
                    else
                    {
                        string addedKeyCre = clientSubjectsPage.ValidateAddedSubjectWithKeyCreditorL();
                        string typeKeyCre = clientSubjectsPage.ValidateTypeOfAddedSubjectL();
                        //string recTypeKeyCre = clientSubjectsPage.ValidateRecTypeOfAddedSubjectL();
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 2), addedKeyCre);
                        Assert.AreEqual("Subject", typeKeyCre);
                        //Assert.AreEqual("Operating Company", recTypeKeyCre);
                        //extentReports.CreateLog("Company with name: " + addedKeyCre + " with Type: " + typeKeyCre + " and Record Type as: " + recTypeKeyCre + " is displayed in Additional Clients/Subjects section ");
                         extentReports.CreateLog("Company with name: " + addedKeyCre + " with Type: " + typeKeyCre + " is displayed in Additional Clients/Subjects section ");
                    }

                    //---Validate added client for Key Creditors
                    string addedKey = opportunityDetails.GetCompanyNameOfKeyCreditorL();
                    string typeKey = opportunityDetails.GetTypeOfKeyCreditorL();
                    Console.WriteLine("typeKey: " + typeKey);
                    //string recTypeKey = opportunityDetails.GetRecTypeOfKeyCreditorL();
                    //Console.WriteLine("recTypeKey: " + recTypeKey);
                    if (valJobType.Equals("Creditor Advisors"))
                    {
                        Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 1), addedKey);
                        Assert.AreEqual("Key Creditor", typeKey);
                       // Assert.AreEqual("Operating Company", recTypeKey);
                        //extentReports.CreateLog("Company with name: " + addedKey + " with Type: " + typeKey + " and Record Type as: " + recTypeKey + " is displayed in Additional Clients/Subjects section ");
                        extentReports.CreateLog("Company with name: " + addedKey + " with Type: " + typeKey + " is displayed in Additional Clients/Subjects section ");

                        //T1649 -Additional Client and Subject as Yes
                        string additionalClient =   opportunityDetails.UpdateAdditionalClientL();
                        Assert.AreEqual("Referral Info", additionalClient);
                        extentReports.CreateLog("Tab with Additional Client/Subject & Referral is displayed only even when Additional Client is selectded as YES ");

                        string additionalSubject = opportunityDetails.UpdateAdditionalSubjectL();
                        Assert.AreEqual("Referral Info", additionalSubject);
                        extentReports.CreateLog("Tab with Additional Client/Subject & Referral is displayed only even when Additional Client is selectded as YES ");

                    }

                    else
                    {
                        Assert.AreEqual("No new client exists", addedKey);
                        Assert.AreEqual("No new client exists", typeKey);
                        //Assert.AreEqual("No new client exists", recTypeKey);
                        extentReports.CreateLog("No company with Key Creditors exists in Additional Clients/Subjects section ");
                    }                 
                  
                    usersLogin.DiffLightningLogout();
                    Console.WriteLine("User logged out");
                }    
                
                usersLogin.UserLogOut();
                Console.WriteLine("Admin logged out");
                driver.Quit();                
            }
            catch(Exception)
            {               
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}




