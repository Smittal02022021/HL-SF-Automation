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
    class TC2362_TMTC0003198_VerifyModificationOfLOBValueForGivenOpportunity_Lightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        FEISForm form = new FEISForm();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        SendEmailNotification notification = new SendEmailNotification();
        AddOppCounterparty counterparty = new AddOppCounterparty();
        AddOpportunityContact addContact = new AddOpportunityContact();
        public static string fileTC2362 = "T2362_TMTC0003198_VerifyModificationOfLOBValueForGivenOpportunity";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyModificationOfLOBValueForGivenOpportunity()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2362;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                //Login as Financial User and validate the user                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();
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
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string opportunityNumber = addOpportunity.AddOpportunitiesLightning(valJobType, fileTC2362);
                Console.WriteLine("value : " + opportunityNumber);
                extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileTC2362);
                Assert.AreEqual("Info", displayedTab);
                extentReports.CreateLog("Tab with name: " + displayedTab + " is displayed upon saving internal deal team members details ");

                //Create External Primary Contact      
                //Update all required fields for Conversion to Engagement
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);               
                opportunityDetails.UpdateReqFieldsForFVAConversionL(fileTC2362);
                extentReports.CreateLog("All required details are saved ");
                opportunityDetails.ClickAddFVAOppContact();
                addContact.CreateContactL(fileTC2362);

                //Logout
                usersLogin.LightningLogout();

                //Search for Opportunity
                opportunityHome.SearchOpportunity(opportunityNumber);
                opportunityDetails.UpdateInternalTeamDetails(fileTC2362);
                extentReports.CreateLog("Internal Team members details are saved ");
                opportunityDetails.UpdateOutcomeDetails(fileTC2362);
                extentReports.CreateLog("Conflict Check fields are updated ");

                //Update Record Type to CF, LOB to CF, Job Type to Negotiated Fairness and validate Job Type and LOB                
                string lob = opportunityDetails.UpdateRecordTypeAndLOB();
                Assert.AreEqual("CF", lob);
                string jobType = opportunityDetails.GetJobType();
                Assert.AreEqual("Negotiated Fairness", jobType);
                extentReports.CreateLog("LOB and Job Type are updated to " + lob + " and " + jobType + " ");

                //Update additional fields i.e Estimated Fees and Fairness Opinion Component
                opportunityDetails.AddEstFeesWithAdmin(fileTC2362);

                //Login as Financial User and validate the user                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUserLightning();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Open the same opportunity               
                opportunityHome.SearchMyOpportunitiesInLightning(opportunityNumber, valUser);

                //Update new fields i.e Estimated Fees and Indemnification language
                opportunityDetails.AddAdditionalFieldsForCFL(fileTC2362);

                //Requesting for engagement and validate the success message
                opportunityDetails.ClickRequestoEngL();

                //Login as CAO user and Validate the status of Opportunity post Request Engagement
                usersLogin.DiffLightningLogout();
                string valCAOUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);
                usersLogin.SearchUserAndLogin(valCAOUser);
                string caoUser2 = login.ValidateUserLightning();
                Assert.AreEqual(caoUser2.Contains(valCAOUser), true);
                extentReports.CreateLog("CAO User: " + caoUser2 + " is able to login ");

                //Search for created opportunity and validate the status
                opportunityHome.ClickOppUnderHLBankerCAO();
                opportunityHome.SearchMyOpportunitiesInLightning(opportunityNumber, valCAOUser);
                string status = opportunityDetails.ClickApproveButtonLV2();
                Assert.AreEqual("Approved", status);
                extentReports.CreateLog("Opportunity is approved ");

                //Convert the Opportunity to Engagement by clicking Convert To Engagement and log out of CAO user
                opportunityDetails.ClickOppName();
                string engDetails = opportunityDetails.ClickReqToEngagement();
                Assert.AreEqual("Engagement", engDetails);
                extentReports.CreateLog("Opportunity is converted to Engagement after clicking Request To Engagement button ");

                //Validate the Engagement name in Engagement details page
                string engName = engagementDetails.GetEngNumL();
                Assert.AreEqual(opportunityNumber, engName);
                extentReports.CreateLog("Name of Engagement : " + engName + " is similar to Opportunity name ");
                               
                //Get Record Type of Engagement 
                string engJobType = engagementDetails.GetRecordTypeL();
                Console.WriteLine("engJobType " + engJobType);
                Assert.AreEqual("Fairness (CF)", engJobType);
                extentReports.CreateLog("Record Type of Engagement is displayed as " +engJobType + " ");
                
                usersLogin.DiffLightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
            }
        }
        [OneTimeTearDown]
        public new void ExtentClose()
        {
            try
            {
                extent.Flush();
            }
            catch (Exception e)
            {
                throw e;
            }
            driver.Quit();
            notification.SendOutlookEmailWithTestExecutionReport("Test Execution Report - Opportunity");
        }
    }
}
