using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SF_Automation.TestCases.Opportunities
{
    class TMTT0012824_VerifyUserCanEditAnyOtherJobTypesToNewJobTypeForExistingCFOpportunityEngagement:BaseClass
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

        public static string fileTMTI0028212 = "TMTI0028212_VerifyUserCanEditAnyOtherJobTypesToNewJobTypeForExistingCFOpportunity";

        string oldJobType;
        string jobTypeExl;        
        string updatedJobType;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void EditAnyOtherJobTypesToNewJobTypeForExistingCFOpportunityEngagement()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0028212;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");

                for (int row = 2; row <= rowJobType; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);

                    //Login as Standard User profile and validate the user
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                    usersLogin.SearchUserAndLogin(valUser);
                    string stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser + " logged in ");

                    //Call function to open Add Opportunity Page
                    opportunityHome.ClickOpportunity();
                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                    opportunityHome.SelectLOBAndClickContinue(valRecordType);

                    //Validating Title of New Opportunity Page
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 100), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Validate Women Led field and Calling AddOpportunities function      
                    string value = addOpportunity.AddOpportunities(valJobType, fileTMTI0028212);

                    extentReports.CreateLog("Opportunity : " + value + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    clientSubjectsPage.EnterStaffDetails(fileTMTI0028212);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                    Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact         
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    addOpportunityContact.CreateContact(fileTMTI0028212, valContact, valRecordType, valContactType);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog(valContactType + " Opportunity contact is saved ");
                    
                    //TMTI0028212	Verify user is able to edit any other Job types to new job type for existing opportunity
                    //Get Actual Job Type of selected Opportunity and updated it with new Job Type
                    extentReports.CreateLog("Verify User Can Edit Any Other Job Types To New Job Type For Existing CF Opportunity ");
                    int rowJobTypes = ReadExcelData.GetRowCount(excelPath, "JobTypes");

                    for (int rowNewJobType = 2; rowNewJobType <= rowJobTypes; rowNewJobType++)
                    {
                        oldJobType = opportunityDetails.GetOppJobType();
                        jobTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "JobTypes", rowNewJobType, 1);
                        opportunityDetails.UpdateJobType(jobTypeExl);
                        updatedJobType = opportunityDetails.GetOppJobType();
                        Assert.AreEqual(jobTypeExl, updatedJobType);
                        extentReports.CreateLog("Job Type: " + oldJobType + " is updated with new JobType: " + updatedJobType+" ");
                    }
                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForCFConversion(fileTMTI0028212);
                    opportunityDetails.UpdateInternalTeamDetails(fileTMTI0028212);

                    //Logout of user and validate Admin login
                    usersLogin.UserLogOut();
                    Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //update Update Outcome Details 
                    opportunityDetails.UpdateOutcomeDetails(fileTMTI0028212);
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
                    extentReports.CreateLog("Opportunity is Converted into Engagement ");
                    //Validate the Engagement name in Engagement details page
                    string engName = engagementDetails.GetEngName();
                    Assert.AreEqual(opportunityNumber, engName);
                    extentReports.CreateLog("Name of Engagement : " + engName + " is similar to Opportunity name ");

                    //TMTI0028218 Verify user is able to edit any other Job types to new job type for existing Engagement
                    //TMTI0028204 Verify the availability of new Job Types on Edit Engagement page                  

                    for (int rowNewJobType = 2; rowNewJobType <= rowJobTypes; rowNewJobType++)
                    {
                        oldJobType = engagementDetails.GetOppJobType();
                        jobTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "JobTypes", rowNewJobType, 1);
                        engagementDetails.UpdateJobType(jobTypeExl);
                        updatedJobType = engagementDetails.GetOppJobType();
                        Assert.AreEqual(jobTypeExl, updatedJobType);
                        extentReports.CreateLog("Job Type: " + oldJobType + " is updated with new JobType: " + updatedJobType+" ");
                    }
                    //Verify the ERP Status is Updated on Opportunity Page
                    //TMTI0028210 Verify the status is updated in Oracle ERP Information section after creating the Opportunity
                    engagementDetails.ClickRelatedOpportunityLink();
                    extentReports.CreateLog("User is on Opportunity Detail page");
                    string ERPStatusIG = opportunityDetails.GetOppERPIntegrationStatus();
                    Assert.AreEqual("Success", ERPStatusIG);
                    extentReports.CreateLog("ERP Last Integration Status in ERP section: " + ERPStatusIG + " is displayed ");

                }
                usersLogin.UserLogOut();    
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception ex)
            {
                extentReports.CreateLog(ex.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();

            }
        }
    }
}
