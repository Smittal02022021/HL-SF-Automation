using AventStack.ExtentReports.Gherkin.Model;
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
    class ZObsolated_VT_TMTT0024858_TMTT0030610_TMTT0035436_VerifyStatusIsUpdatedInOracleERPInformationSectionForFVARecordTypes : BaseClass
    {

        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        SendEmailNotification notification = new SendEmailNotification();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();


        public static string fileTMTI0056868 = "TMTI0056868_VerifyStatusIsUpdatedInOracleERPInformationSectionForFVARecordTypes";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyStatusIsUpdatedInOracleERP()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0056868;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                for (int row = 2; row <= rowJobType; row++)
                {

                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);

                    //Login as Standard User profile and validate the user
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                    login.SwitchToClassicView();
                    string stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                    extentReports.CreateLog("User: " + stdUser + " logged in ");

                    //Call function to open Add Opportunity Page
                    opportunityHome.ClickOpportunity();
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity",row, 25);
                    Console.WriteLine("valRecordType:" + valRecordType);
                    opportunityHome.SelectLOBAndClickContinue(valRecordType);

                    //Validating Title of New Opportunity Page
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                    
                    //Calling AddOpportunities function
                    string value = addOpportunity.AddOpportunities(valJobType, fileTMTI0056868);
                    Console.WriteLine("value : " + value);
                    extentReports.CreateLog("Opportunity : " + value + " is created ");

                    //Call function to enter Internal Team details and validate opportunity detail page
                    clientSubjectsPage.EnterStaffDetails(fileTMTI0056868);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                    Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact         
                    String valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    String valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    addOpportunityContact.CreateContact(fileTMTI0056868, valContact, valRecordType, valContactType);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForFVAConversion(fileTMTI0056868);
                    //opportunityDetails.UpdateInternalTeamDetails(fileTMTI0056868);

                    if (valJobType.Contains("TAS"))
                    {
                        opportunityDetails.UpdateTASServices();
                    }

                    //Logout of user and validate Admin login
                    usersLogin.UserLogOut();
                    login.SwitchToClassicView();
                    Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(fileTMTI0056868);
                    extentReports.CreateLog("Conflict Check fields are updated By Admin");

                    opportunityDetails.UpdateInternalTeamDetails(fileTMTI0056868);

                    //Login again as Standard User
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                    string stdUser1 = login.ValidateUser();
                    Assert.AreEqual(stdUser1.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                    extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //Update Total Anticipated Revenue
                    opportunityDetails.UpdateTotalAnticipatedRevenueForValidations();

                    //Requesting for engagement and validate the success message
                    string msgSuccess = opportunityDetails.ClickRequestEng();
                    Assert.AreEqual(msgSuccess, "Submission successful! Please wait for the approval");
                    extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");

                    //Log out of Standard User
                    usersLogin.UserLogOut();

                    //Login as CAO user to approve the Opportunity
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 2));
                    login.SwitchToClassicView();
                    string caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                    extentReports.CreateLog("User: " + caoUser + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);
                    string OppContactMember = opportunityDetails.GetOppExternalContact();
                    extentReports.CreateLog(OppContactMember + " is added as External Contact on Opportunity page ");
                    string OppDealTeamMember = opportunityDetails.GetOppDealTeamMember();
                    extentReports.CreateLog(OppDealTeamMember + " is added as Deal Team Member on Opportunity page ");

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

                    //TMTI0056869 Verify the availability of new Job Types on Edit Engagement page
                    //TMTI0071654 Verify the availability of new Job Types on the Edit Engagement page
                    //TMTI0084220 Verify the availability of new Job Types on Edit Engagement page
                    Assert.IsTrue(engagementDetails.IsJobTypePresentInDropdownOppDetailPage(valJobType));
                    extentReports.CreateLog("Job Type: " + valJobType + " is present on edit Engageent page ");

                    //TMTI0071647 Verify the status is updated in the Oracle ERP Information section
                    //TMTI0084221 Verify the status is updated in Oracle ERP Information section
                    //Validate the ERP Last Integration Status on Engagement details page
                    string ERPStatusIG = engagementDetails.GetEngERPIntegrationStatus();
                    Assert.AreEqual("Success", ERPStatusIG);
                    extentReports.CreateLog("ERP Last Integration Status in ERP section: " + ERPStatusIG + " is displayed ");
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



