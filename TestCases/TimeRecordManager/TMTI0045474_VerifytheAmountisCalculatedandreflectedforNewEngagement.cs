using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class TMTI0045474_VerifytheAmountisCalculatedandreflectedforNewEngagement : BaseClass
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
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();

        

        public static string fileTC45474 = "TMTI0045474_VerifytheAmountisCalculatedandreflectedforNewEngagement";
        private string valueActivity;
        private string valueProjectOrEngagment;
        private string valueActivityExl;
        private string valueDefaultDollarBasedOnTitle;
        private string valueEnteredHours;
        private string valueEnteredHoursExl;
        private string selectProject;
        private double DefaultRateForStaffWithRateSheet;
        private double totalAmountDisplayedSummaryLogs;
        private double enteredHoursSummaryLogs;
        private double totalAmountCalculated;
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void AddTimeEntriesToNewlyConvertedEngagementAsSupervisor()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC45474;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);

                //Login as Standard User profile and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
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
                string value = addOpportunity.AddOpportunities(valJobType, fileTC45474);
                Console.WriteLine("value : " + value);
                extentReports.CreateLog("Opportunity : " + value + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC45474);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details  
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                //Create External Primary Contact         
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                addOpportunityContact.CreateContact(fileTC45474, valContact, valRecordType, valContactType,2);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                //Update required Opportunity fields for conversion and Internal team details
                opportunityDetails.UpdateReqFieldsForConversion(fileTC45474);
                opportunityDetails.UpdateInternalTeamDetails(fileTC45474);

                //Logout of user and validate Admin login
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //update CC and NBC checkboxes 
                opportunityDetails.UpdateOutcomeDetails(fileTC45474);
                //opportunityDetails.UpdateNBCApproval();
                extentReports.CreateLog("Conflict Check and NBC fields are updated ");

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
                
                //Convert Opportunity into Engagement
                opportunityDetails.ClickConvertToEng();

                //Validate the Engagement name in Engagement details page
                string engName = engagementDetails.GetEngName();
                Assert.AreEqual(opportunityNumber, engName);
                extentReports.CreateLog("Name of Engagement : " + engName + " is similar to Opportunity name ");
                usersLogin.UserLogOut();
                extentReports.CreateLog("Logout from CAO user ");

                string supervisorUser = ReadExcelData.ReadData(excelPath, "SupervisorUser", 1);

                //Login as supervisor User
                usersLogin.SearchUserAndLogin(supervisorUser);
                string supUser = login.ValidateUser();
                Assert.AreEqual(supUser.Contains(supervisorUser), true);
                extentReports.CreateLog("Time Record Manager Supervisor User: " + supUser + " logged in ");

                //Click Time Record Manager Tab
                homePage.ClickTimeRecordManagerTab();

                string name = ReadExcelData.ReadData(excelPath, "StaffMember", 1);

                timeEntry.SelectStaffMember(name);
                string selectedStaffMember = timeEntry.GetSelectedStaffMember();
                string selectedStaffMemberExl = ReadExcelData.ReadData(excelPath, "StaffMember", 1);
                Assert.AreEqual(selectedStaffMemberExl, selectedStaffMember);
                extentReports.CreateLog("Staff Member Title: " + selectedStaffMember + " is displayed upon click of staff Member link ");

                string timeRecordManagerTitle = timeEntry.GetTimeRecordManagerTitle();
                string timeRecordManagerTitleExl = ReadExcelData.ReadData(excelPath, "StaffMember", 3);
                Assert.AreEqual(timeRecordManagerTitleExl, timeRecordManagerTitle);
                extentReports.CreateLog("Time Record Manager Title: " + timeRecordManagerTitle + " is displayed upon click of Time Record Manager tab ");

                //Enter Rate Sheet details

                selectProject = "E - " + opportunityNumber + "-FVA";
                //Enter Rate Sheet details
                string rateSheet = ReadExcelData.ReadData(excelPath, "RateSheetManagement", 1);
                rateSheetMgt.EnterRateSheet(opportunityNumber, rateSheet);

                //Verify selected rate sheet
                string selectedRateSheet = rateSheetMgt.GetSelectedRateSheet();
                string selectedRateSheetExl = ReadExcelData.ReadData(excelPath, "RateSheetManagement", 1);
                Assert.AreEqual(selectedRateSheetExl, selectedRateSheet);

                //---------------Weekly Time Entry Matrix---------------------//

                //Go to Weekly Time Entry Matrix section
                timeEntry.GoToWeeklyEntryMatrix();
                timeEntry.SelectProjectWeeklyEntryMatrix(selectProject, fileTC45474);
                timeEntry.LogCurrentDateHours(fileTC45474);

                extentReports.CreateLog("Entering Hours for " + selectProject + " via Weekly Time Entry Matrix ");

                //Verify project or engagement on Summary Logs
                timeEntry.GoToSummaryLog();
                extentReports.CreateLog("Verifying the entered hours from Time Entry Matrix for " + selectProject + " on Summary Logs ");
                valueProjectOrEngagment = timeEntry.GetProjectOrEngagementValue();
                if (selectProject.Contains(valueProjectOrEngagment))
                {
                    extentReports.CreateLog("Project Or Engagement: " + valueProjectOrEngagment + " is displayed upon entering time entry from Weekly Entry Matrix ");
                }

                //Verify selected activity on Summary Logs
                valueActivity = timeEntry.GetSelectedActivity();
                valueActivityExl = ReadExcelData.ReadData(excelPath, "Update_Timer", 2);
                Assert.AreEqual(valueActivityExl, valueActivity);
                extentReports.CreateLog("Selected Activity: " + valueActivity + " is displayed upon entering time entry from Weekly Entry Matrix ");

                //Verify entered hours 
                valueEnteredHours = timeEntry.GetEnteredHoursInSummaryLog();
                valueEnteredHoursExl = ReadExcelData.ReadData(excelPath, "Update_Timer", 1);
                Assert.AreEqual(valueEnteredHoursExl, valueEnteredHours);
                extentReports.CreateLog("Entered Hours: " + valueEnteredHours + " is displayed upon entering time entry from Weekly Entry Matrix ");

                valueDefaultDollarBasedOnTitle = timeEntry.GetDefaultRateForStaff();

                extentReports.CreateLog("Default Dollar Based On title amount: " + valueDefaultDollarBasedOnTitle + " is displayed upon entering time entry  from Weekly Entry Matrix with Rate Sheet Added");

                //Get default rate displayed on Summary Logs
                DefaultRateForStaffWithRateSheet = timeEntry.GetDefaultRateForStaffValue();

                //Get Entered Hours displayed
                enteredHoursSummaryLogs = timeEntry.GetEnteredHoursInSummaryLogValue();

                // Get total amount calculated with default rate and entered hours on Summary Logs
                totalAmountCalculated = DefaultRateForStaffWithRateSheet * enteredHoursSummaryLogs;

                // Get total amount displayed on Summary Logs
                totalAmountDisplayedSummaryLogs = timeEntry.GetTotalAmount();

                //Verify calculated and displayed amount should matches on Summary Logs
                Assert.AreEqual(totalAmountCalculated, totalAmountDisplayedSummaryLogs);
                extentReports.CreateLog("Total Amount: " + totalAmountDisplayedSummaryLogs + " displayed is matching with the calculation based on total hours entered and the default rate based on staff title ");

                //Delete Time Entry
                timeEntry.DeleteTimeEntry();
                extentReports.CreateLog("Deleted time entry successfully after verification ");


                //---------------Summary Logs---------------------//

                //Enter summary log details
                selectProject = "Engagement " + opportunityNumber + "-FVA";
                timeEntry.EnterSummaryLogs(selectProject, fileTC45474);
                timeEntry.ClickAddButton();

                //Verify project or engagement on Summary Logs
                valueProjectOrEngagment = timeEntry.GetProjectOrEngagementValue();
                if (selectProject.Contains(valueProjectOrEngagment))
                {
                    extentReports.CreateLog("Project Or Engagement: " + valueProjectOrEngagment + " is displayed upon entering time entry in summary log ");
                }

                //Verify entered hours 
                valueEnteredHours = timeEntry.GetEnteredHoursInSummaryLog();
                valueEnteredHoursExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 2);
                Assert.AreEqual(valueEnteredHoursExl, valueEnteredHours);
                extentReports.CreateLog("Entered Hours: " + valueEnteredHours + " is displayed upon entering time entry in summary log ");

                //Verify selected activity on Summary Logs
                valueActivity = timeEntry.GetSelectedActivity();
                valueActivityExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 3);
                Assert.AreEqual(valueActivityExl, valueActivity);
                extentReports.CreateLog("Selected Activity: " + valueActivity + " is displayed upon entering time entry in summary log ");

                

                //Verify default dollar based on the title of the staff member on Summary Logs
                valueDefaultDollarBasedOnTitle = timeEntry.GetDefaultRateForStaff();

                extentReports.CreateLog("Default Dollar Based On title amount: " + valueDefaultDollarBasedOnTitle + " is displayed upon entering time entry in summary log with Rate Sheet Added");

                //Get default rate displayed on Summary Logs
                DefaultRateForStaffWithRateSheet = timeEntry.GetDefaultRateForStaffValue();

                //Get Entered Hours displayed
                enteredHoursSummaryLogs = timeEntry.GetEnteredHoursInSummaryLogValue();

                // Get total amount calculated with default rate and entered hours on Summary Logs
                //double 
                totalAmountCalculated = DefaultRateForStaffWithRateSheet * enteredHoursSummaryLogs;

                // Get total amount displayed on Summary Logs
                totalAmountDisplayedSummaryLogs = timeEntry.GetTotalAmount();

                //Verify calculated and displayed amount should matches on Summary Logs
                Assert.AreEqual(totalAmountCalculated, totalAmountDisplayedSummaryLogs);
                extentReports.CreateLog("Total Amount: " + totalAmountDisplayedSummaryLogs + " displayed is matching with the calculation based on total hours entered and the default rate based on staff title ");

                //Delete Time Entry
                timeEntry.DeleteTimeEntry();
                extentReports.CreateLog("Deleted time entry successfully after verification ");


                //---------------Detail logs---------------------//
                // Enter Detail logs
                timeEntry.EnterDetailLogs(selectProject, fileTC45474);
                timeEntry.ClickAddButton();

                //Verify project or engagement on Detail Logs 
                string valueProjectOrEngagmentInDetailLogs = timeEntry.GetProjectOrEngagementValue();
                if (selectProject.Contains(valueProjectOrEngagmentInDetailLogs))
                {
                    extentReports.CreateLog("Project Or Engagement: " + valueProjectOrEngagment + " is displayed upon entering time entry in detail log ");
                }

                //Verify selected activity on Detail Logs 
                string valueActivityInDetailLogs = timeEntry.GetSelectedActivityOnDetailLog();
                string valueActivityInExlDetailLogs = ReadExcelData.ReadData(excelPath, "DetailLogs", 3);
                Assert.AreEqual(valueActivityInExlDetailLogs, valueActivityInDetailLogs);
                extentReports.CreateLog("Selected Activity: " + valueActivity + " is displayed upon entering time entry on Detail Logs ");

                //Verify entered hours on Detail Logs 
                string  valueEnteredHoursInDetailLogs = timeEntry.GetEnteredHoursInDetailLogs();
                string  valueEnteredHoursInExl = ReadExcelData.ReadData(excelPath, "DetailLogs", 2);
                Assert.AreEqual(valueEnteredHoursInExl, valueEnteredHoursInDetailLogs);
                extentReports.CreateLog("Entered Hours: " + valueEnteredHoursInDetailLogs + " is displayed upon entering time entry in detail log ");

                //Verify default dollar based on the title of the staff member on Detail Logs 

                string  valueDefaultDollarDetailLogs = timeEntry.GetDefaultRateForStaffInDetailLogs();

                string valueDefaultDollarDetailLogsExl = ReadExcelData.ReadData(excelPath, "DetailLogs", 4);
                Assert.AreEqual(valueDefaultDollarDetailLogsExl, valueDefaultDollarDetailLogs);
                extentReports.CreateLog("Default Dollar Based On title amount: " + valueDefaultDollarDetailLogs + " is displayed upon entering time entry on Detail Logs ");

                //Get default rate displayed on Detail Logs 
                double defaultRateForStaffDetailLogs = timeEntry.GetDefaultRateForStaffValueInDetailLogs();

                //Get Entered Hours displayed on Detail Logs 
                double enteredHoursDetailLogs = timeEntry.GetEnteredHoursInDetailLogValue();

                // Get total amount calculated with default rate and entered hours on Detail Logs 
                totalAmountCalculated = defaultRateForStaffDetailLogs * enteredHoursDetailLogs;

                // Get total amount displayed
                double totalAmountDisplayedInDetailLogs = timeEntry.GetTotalAmount();

                //Verify calculated and displayed amount should matches on Detail Logs 
                Assert.AreEqual(totalAmountCalculated, totalAmountDisplayedInDetailLogs);
                extentReports.CreateLog("Total Amount: " + totalAmountDisplayedInDetailLogs + " displayed is matching with the calculation based on total hours entered and the default rate based on staff title ");

                // Delete time entry records from detail log 
                timeEntry.RemoveRecordFromDetailLogs();
                extentReports.CreateLog("Deleted record entry successfully from detail log ");

                //Delete rate sheet
                rateSheetMgt.DeleteRateSheet(opportunityNumber);
                extentReports.CreateLog("Deleted rate sheet entry successfully after verification ");
                usersLogin.UserLogOut();
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