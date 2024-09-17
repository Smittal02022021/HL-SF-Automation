using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunity
{
    class TMTI0054728_TMTI0054730_VerificationOfNewFieldAssociatedOpportunityAvailabiltyAndFunctionalityOnFVAOpportunityAndEngagementPage:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTMTI0054683 = "TMTI0054728_VerificationOfNewFieldAssociatedOpportunityAvailabiltyAndFunctionalityOnFVAOpportunityAndEngagementPage";

        private string valAssociatedEng;
        private string nameAssociatedEng;
        private string stdUser;
        private string user;
        private string caoUser;
        private string opportunityName;
        private string valRecordType;
        private string valContactType;
        private string valContact;
        private string valAssociatedOpp;
        private string nameAssociatedOpp;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void NewFieldAssociatedOpportunityAvailabiltyForFVA()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0054683;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowOpp = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");

                for (int row = 2; row <= rowOpp; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);

                    //Login as Standard User profile and validate the user
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);

                    usersLogin.SearchUserAndLogin(valUser);
                    stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser + " logged in ");

                    //Call function to open Add Opportunity Page
                    opportunityHome.ClickOpportunity();
                    valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                    extentReports.CreateLog("Record Type: " + valRecordType+" ");
                    opportunityHome.SelectLOBAndClickContinue(valRecordType);

                    //Validating Title of New Opportunity Page
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog("Page Title: " + driver.Title + " is displayed ");

                    //Calling AddOpportunities function                  
                    opportunityName = addOpportunity.AddOpportunities(valJobType, fileTMTI0054683);
                    extentReports.CreateLog("Opportunity Name : " + opportunityName);

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    clientSubjectsPage.EnterStaffDetails(fileTMTI0054683);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityName + " ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog("Page Title: " + driver.Title + " is displayed ");

                    //Validating Opportunity details  
                    opportunityName = opportunityDetails.ValidateOpportunityDetails();
                    Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                    extentReports.CreateLog("Opportunity with Name : " + opportunityName + " is created ");

                    //New Field is Present on Opportunity Detail Page for Standard User
                    Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldPresent());
                    extentReports.CreateLog("New Field i.e. Associated Opportunity is Present on Opportunity Detail Page for Standard User: " + valUser + " ");

                    // New Field on Opportunity Detail Page is not editable for Standard User
                    Assert.IsFalse(opportunityDetails.IsAssociatedOppFieldEditable(), "Verify Associated Engagement should not be editable for Standard User ");
                    extentReports.CreateLog("New Field i.e. Associated Opportunity is not Editable for Standard User: " + valUser + " ");

                    //Create External Primary Contact         
                    valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    addOpportunityContact.CreateContact(fileTMTI0054683, valContact, valRecordType, valContactType);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityName + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForFVAConversion(fileTMTI0054683);
                    opportunityDetails.UpdateInternalTeamDetails(fileTMTI0054683);
                    extentReports.CreateLog("All Required fields and Deal tam is updated ");

                    //Logout of user and validate Admin login
                    usersLogin.UserLogOut();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateLog("User " + user + " is logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(opportunityName);

                    //New Field is Present on Opportunity Detail Page for Admin login
                    Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldPresent());
                    extentReports.CreateLog("New Field i.e. Associated Opportunity is Present on Opportunity Detail Page for System Administrator: " + user + " ");

                    // New Field on Opportunity Detail Page is not editable for Admin login
                    Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldEditable(), "Verify Associated Engagement should be editable for System Administrator ");
                    extentReports.CreateLog("New Field i.e. Associated Opportunity is Editable for System Administrator: " + user + " ");

                    //Enter the Associated Opportunity name
                    valAssociatedOpp = ReadExcelData.ReadDataMultipleRows(excelPath, "AssociatedOpp", 2, 1);
                    opportunityDetails.EnterAssociatedOpportunity(valAssociatedOpp);
                    nameAssociatedOpp = opportunityDetails.GetAssociatedOpportunity();

                    Assert.AreEqual(nameAssociatedOpp, valAssociatedOpp, "Verify Entered Associated Opportunity as saved ");
                    extentReports.CreateLog(user + " Entered " + valAssociatedOpp + " as Associated Opportunity and " + nameAssociatedOpp + " is Saved ");

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(fileTMTI0054683);
                    extentReports.CreateLog("Conflict Check fields are updated ");

                    //Login again as Standard User
                    usersLogin.SearchUserAndLogin(valUser);
                    stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("Standard User: " + stdUser + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(opportunityName);

                    //Update Total Anticipated Revenue
                    opportunityDetails.UpdateTotalAnticipatedRevenueForValidations();
                    //Requesting for engagement and validate the success message
                    string msgSuccess = opportunityDetails.ClickRequestEng();
                    Assert.AreEqual(msgSuccess, "Submission successful! Please wait for the approval");
                    extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");

                    //Log out of Standard User
                    usersLogin.UserLogOut();
                    extentReports.CreateLog("Standard User: " + stdUser + " logged out ");

                    //Login as CAO user to approve the Opportunity
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 2));
                    caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                    extentReports.CreateLog("CAO User: " + caoUser + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(opportunityName);

                    //New Field is Present on Opportunity Detail Page for CAO user
                    Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldPresent());
                    extentReports.CreateLog("New Field i.e. Associated Opportunity is Present on Opportunity Detail Page for CAO User: " + caoUser + " ");

                    //New Field on Opportunity Detail Page is not editable for CAO User
                    Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldEditable(), "Verify Associated Engagement should be editable for CAO User ");
                    extentReports.CreateLog("New Field i.e. Associated Opportunity is Editable for CAO User: " + caoUser + " ");

                    //Enter the Associated Opportunity name
                    valAssociatedOpp = ReadExcelData.ReadDataMultipleRows(excelPath, "AssociatedOpp", 2, 1);
                    opportunityDetails.EnterAssociatedOpportunity(valAssociatedOpp);
                    nameAssociatedOpp = opportunityDetails.GetAssociatedOpportunity();

                    Assert.AreEqual(nameAssociatedOpp, valAssociatedOpp, "Verify Entered Associated Opportunity as saved ");
                    extentReports.CreateLog(user + " Entered " + valAssociatedOpp + " as Associated Opportunity and " + nameAssociatedOpp + " is Saved ");

                    //Approve the Opportunity 
                    opportunityDetails.ClickApproveButton();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityName + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog("Opportunity is approved ");

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEng();
                    extentReports.CreateLog("Opportunity" + opportunityName + " is Converted into Engagement and CAO user is on Engagement Detail page ");

                    //Validate the Engagement name in Engagement details page
                    string engagementName = engagementDetails.GetEngName();
                    Assert.AreEqual(opportunityName, engagementName);
                    extentReports.CreateLog("Name of Engagement : " + engagementName + " is same as Opportunity name ");

                    //New Field is Present on Opportunity Detail Page for CAO User
                    Assert.IsTrue(engagementDetails.IsAssociatedEngFieldPresent());
                    extentReports.CreateLog("New Field i.e. Associated Opportunity is Present on Engagement Detail Page for CAO User: " + caoUser + " ");

                    //New Field on Opportunity Detail Page is not editable for CAO User
                    Assert.IsTrue(engagementDetails.IsAssociatedEngFieldEditable(), "Verify Associated Engagement should be editable for CAO User ");
                    extentReports.CreateLog("New Field i.e. Associated Engagement is Editable for CAO User: " + caoUser + " ");

                    //Enter the Associated Opportunity name
                    valAssociatedEng = ReadExcelData.ReadDataMultipleRows(excelPath, "AssociatedEng", 3, 1);
                    nameAssociatedEng = engagementDetails.EnterAssociatedEngagement(valAssociatedEng);
                    Assert.AreEqual(nameAssociatedEng, valAssociatedEng, "Verify Entered Associated Engagement as saved ");
                    extentReports.CreateLog(caoUser + " Entered " + valAssociatedEng + " as Associated Engagement and " + nameAssociatedEng + " is Saved ");
                    usersLogin.UserLogOut();
                    extentReports.CreateLog("CAO User " + caoUser + "Logged Out");

                    //Logout of user and validate Admin login
                    user = login.ValidateUser();
                    extentReports.CreateLog("User " + user + " is able to login ");

                    //Search for created Engagement
                    engagementHome.SearchEngagement(engagementName);

                    //New Field is Present on Opportunity Detail Page for System Admin 
                    Assert.IsTrue(engagementDetails.IsAssociatedEngFieldPresent());
                    extentReports.CreateLog("New Field i.e. Associated Engagement is Present on Engagement Detail Page for System Administrator: " + user + " ");

                    // New Field on Opportunity Detail Page is not editable for System Admin 
                    Assert.IsTrue(engagementDetails.IsAssociatedEngFieldEditable(), "Verify Associated Engagement should be editable for System Administrator ");
                    extentReports.CreateLog("New Field i.e. Associated Engagement is Editable for System Administrator: " + user + " ");

                    //Enter the Associated Opportunity name
                    valAssociatedEng = ReadExcelData.ReadDataMultipleRows(excelPath, "AssociatedEng", 2, 1);
                    nameAssociatedEng = engagementDetails.EnterAssociatedEngagement(valAssociatedEng);
                    Assert.AreEqual(nameAssociatedEng, valAssociatedEng, "Verify Entered Associated Engagement as saved ");
                    extentReports.CreateLog(user + " Entered " + valAssociatedEng + " as Associated Engagement and " + nameAssociatedEng + " is Saved ");

                    //Standard User Login 
                    usersLogin.SearchUserAndLogin(valUser);
                    stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("Standard User: " + stdUser + " logged in ");

                    //Search for created Engagement
                    engagementHome.SearchEngagement(engagementName);

                    //New Field is Present on Opportunity Detail Page for Standard User
                    Assert.IsTrue(engagementDetails.IsAssociatedEngFieldPresent());
                    extentReports.CreateLog("New Field i.e. Associated Engagement is Present on Engagement Detail Page for Standard User " + stdUser + " ");

                    // New Field on Opportunity Detail Page is not editable for Standard User
                    Assert.IsFalse(engagementDetails.IsAssociatedEngFieldEditable(), "Verify Associated Engagement should not be editable for Standard stdUser ");
                    extentReports.CreateLog("New Field i.e. Associated Engagement is not Editable for Standard User " + stdUser + " ");
                    usersLogin.UserLogOut();

                    driver.Quit();
                    extentReports.CreateLog("Browser Closed ");
                }

            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateLog("Browser Closed ");

            }
        }

    }
}
