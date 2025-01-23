using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class LV_TMTT0013748_TMTT0013750_TMTT0013751_TMTT0013753_VerifyNewTitleRateSheetAvailability_CalculatedAmountOnTimeRecordManagerAfterAddingRateSheetLightningView : BaseClass
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
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTMTT0013748 = "LV_TMTT0013748_VerifyNewTitleRateSheetAvailability_CalculatedAmountOnTimeRecordManagerAfterAddingRateSheet";

        private string engagementExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyNewTitleRateSheetCalculatedAmountOnTimeRecordManagerAfterAddingRateSheetLV(){
            try
            {
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0013748;
                extentReports.CreateStepLogs("Info", "Creating New Opportunity and Converting to Engagement LOB:FVA On Lightning View");
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);

                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 25);

                extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType + " ");
                //Login as Standard User profile and validate the user
                string UserCFExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                //usersLogin.SearchUserAndLogin(UserCFExl);
                homePage.SearchUserByGlobalSearchN(UserCFExl);
                extentReports.CreateStepLogs("Info", "User: " + UserCFExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();

                login.SwitchToLightningExperience();
                string userName = login.ValidateUserLightningView();
                Assert.AreEqual(userName.Contains(UserCFExl), true);
                extentReports.CreateLog("User: " + UserCFExl + " logged in on Lightning View");
                //homePageLV.ClickAppLauncher();

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                //Validating Title of New Opportunity Page
                string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");
                extentReports.CreateStepLogs("Info", "Creating Opportunity for LOB: " + valRecordType + " and Job Type: " + valJobType);

                string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileTMTT0013748);
                extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0013748);
                Assert.AreEqual(displayedTab, "Info");
                extentReports.CreateStepLogs("Passed", "User is on Opportunity detail " + displayedTab + " tab ");

                //Validating Opportunity details  
                string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                extentReports.CreateStepLogs("Passed", "Opportunity with number : " + opportunityNumber + " is created ");

                //Create External Primary Contact      
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                addOpportunityContact.CickAddOpportunityContactLV();
                addOpportunityContact.CreateContactL2(fileTMTT0013748);
                extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                //Update required Opportunity fields for conversion and Internal team details
                opportunityDetails.UpdateReqFieldsForFVAConversionLV(fileTMTT0013748);
                extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "CF Financial User Logged out ");

                //Login as System Admin user 
                string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 3);
                extentReports.CreateStepLogs("Info", "System Admin User: " + adminUserExl + " Updating the Required details ");
                //usersLogin.SearchUserAndLogin(adminUserExl);

                homePage.SearchUserByGlobalSearchN(adminUserExl);
                extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();

                login.SwitchToClassicView();
                string userAdmin = login.ValidateUser();
                Assert.AreEqual(userAdmin.Contains(adminUserExl), true);
                extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUserExl + " User logged in ");

                //login.SwitchToClassicView();
                opportunityHome.SearchOpportunity(opportunityName);
                extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                //update CC 
                opportunityDetails.UpdateOutcomeDetails(fileTMTT0013748);
                extentReports.CreateStepLogs("Info", "Conflict Check fields are updated");
                /////////////////////////////////////////////////////////////////////
                login.SwitchToLightningExperience();
                extentReports.CreateStepLogs("Passed", "System Admin Switched to Lightning View ");
                //homePageLV.ClickAppLauncher();
                //Go to Opportunity module in Lightning View 
                homePageLV.SelectAppLV(appNameExl);
                Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                //Search for created opportunity
                opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");

                //////Standard User don't have permission to modify the Internal team so System Admin is modifying the roles////////
                opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0013748);
                extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                opportunityDetails.ClickReturnToOpportunityLV();

                //Adding Multiple Deal team members with diff Title
                clientSubjectsPage.EnterMembersToDealTeamLV(fileTMTT0013748);
                extentReports.CreateLog("Members added to the deal team with different titles");
                extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                homePageLV.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Passed", "Admin: " + adminUserExl + "switched to Classic and Loggout ");

                //Submit Request to Convert opportunity into Engagement.
                extentReports.CreateStepLogs("Info", "Submit Request to Convert opportunity into Engagement");
                //usersLogin.SearchUserAndLogin(UserCFExl);

                homePage.SearchUserByGlobalSearchN(UserCFExl);
                extentReports.CreateStepLogs("Info", "User: " + UserCFExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();

                login.SwitchToLightningExperience();
                userName = login.ValidateUserLightningView();
                Assert.AreEqual(userName.Contains(UserCFExl), true);
                extentReports.CreateLog("User: " + UserCFExl + " logged in on Lightning View");
                //homePageLV.ClickAppLauncher();

                //Go to Opportunity module in Lightning View                     
                homePageLV.SelectAppLV(appNameExl);
                Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                string result = opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                Assert.AreEqual("Record found", result);
                extentReports.CreateStepLogs("Passed", result + " and selected");
                opportunityDetails.ClickRequestToEngL();
                //Submit Request To Engagement Conversion 
                string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");

                homePageLV.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", "Standard User: " + UserCFExl + " Loggout ");

                //Approve and convert the Opporunity into Engagement
                string userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);
                extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " Approving the Request for Engagement and converting into Engagement ");

                //usersLogin.SearchUserAndLogin(userCAOExl);

                homePage.SearchUserByGlobalSearchN(userCAOExl);
                extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();

                login.SwitchToLightningExperience();
                string userCAO = login.ValidateUserLightningView();
                Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                extentReports.CreateStepLogs("Info", "CAO User: " + userCAOExl + " Switched to Lightning View ");
                //homePageLV.ClickAppLauncher();

                //Go to Opportunity module in Lightning View 
                homePageLV.SelectAppLV(appNameExl);
                Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Pass", "User is on " + moduleNameExl + " Page ");

                //Search for opportunity to Approveconversion request
                result = opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                Assert.AreEqual("Record found", result);
                //Approve the Opportunity 
                string status = opportunityDetails.ClickApproveButtonL();
                Assert.AreEqual(status, "Approved");
                extentReports.CreateStepLogs("Pass", "Opportunity: " + status + " and ready for conversion ");
                opportunityDetails.CloseApprovalHistoryTabL();

                //Calling function to convert to Engagement
                opportunityDetails.ClickConvertToEngagementL2();
                extentReports.CreateStepLogs("Info", "Opportunity: " + opportunityName + " Converted into Engagement ");

                //Validate the Engagement name in Engagement details page
                string engNumber = engagementDetails.GetEngagementNumberL();
                Assert.AreEqual(opportunityNumber, engNumber);
                extentReports.CreateStepLogs("Passed", "Number of Engagement : " + engNumber + " is Same as Opportunity number ");
                string engName = engagementDetails.GetEngagementNameL();
                Assert.AreEqual(opportunityName, engName);
                extentReports.CreateStepLogs("Passed", "Converted Name of Engagement : " + engName + " is Same as Opportunity Name : " + opportunityName);

                homePageLV.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", "CAO User: " + UserCFExl + " Loggout ");

                ////////////////Login again as Supervisor  User////////////////
                string userSupervisorExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SupervisorUser", 2, 1);
                string userGrpNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SupervisorUser", 2, 2);

                //usersLogin.SearchUserAndLogin(userSupervisorExl);

                homePage.SearchUserByGlobalSearchN(userSupervisorExl);
                extentReports.CreateStepLogs("Info", "User: " + userSupervisorExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();

                login.SwitchToLightningExperience();
                string userSupervisor = login.ValidateUserLightningView();
                Assert.AreEqual(userSupervisor.Contains(userSupervisorExl), true);
                extentReports.CreateStepLogs("Passed", "Supervosor User: " + userSupervisorExl + " from Time Tracking Group: " + userGrpNameExl + "  logged in ");
                //homePageLV.ClickAppLauncher();

                //Go to Opportunity module in Lightning View                 
                homePageLV.SelectAppLV(appNameExl);
                Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");                

                int rowCount = ReadExcelData.GetRowCount(excelPath, "RateSheetManagement");

                for (int row = 2; row <= rowCount; row++)
                {
                    //Navigate to Title Rate Sheets page
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 4, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    randomPages.SelectListViewLV("All");
                    extentReports.CreateStepLogs("Info"," All List option is selected ");

                    //Click on the new title rate sheet name
                    string nameRateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 3);
                    pageTitle= rateSheetMgt.SelectTitleRateSheetLV(nameRateSheetExl);
                    extentReports.CreateStepLogs("Info", "User is on "+ pageTitle+" Page");                    

                    string userTitleExl= ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 1);
                    double defaultRate= rateSheetMgt.GetDefaultRateAsPerRoleLV(userTitleExl);
                    extentReports.CreateStepLogs("Info", "Title: "+ userTitleExl+" Default Rate: USD "+ defaultRate);

                    //Verify the correct title rate sheet is opened
                    //Assert.AreEqual(WebDriverWaits.TitleContains(driver, nameRateSheetExl), true);
                    extentReports.CreateStepLogs("Info", driver.Title + " page is displayed ");

                    randomPages.CloseActiveTab(nameRateSheetExl);
                    extentReports.CreateStepLogs("Info", "Rate Sheet: " + nameRateSheetExl + " page is closed ");

                    //Navigate to Time Record Manager
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Pass", "User is on " + moduleNameExl + " Page ");

                    //Select Staff Member from the list
                    string staffNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", row, 2);
                    timeEntry.GoToStaffTimeSheetTabLV();
                    timeEntry.SelectStaffMemberLV(staffNameExl);
                    string staffName = timeEntry.GetSelectedStaffNameLV();
                    Assert.AreEqual(staffNameExl, staffName);
                    extentReports.CreateStepLogs("Passed", "Staff : " + staffName + " with Title: "+ userTitleExl+ " is Selected from list");                    

                    //Go to Weekly Time Entry 
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    extentReports.CreateStepLogs("Info", "User is on Weekly Entry Matrix Page");
                    engagementExl = engNumber;
                    string hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Update_Hours", row, 1);
                    string activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Update_Hours", row, 2);
                    timeEntry.SelectProjectWeeklyEntryMatrixLV(engagementExl);
                    extentReports.CreateStepLogs("Info", "Project: " + engagementExl + " selected on Weekly Entry Matrix ");
                    timeEntry.LogCurrentDateHoursLV(hoursExl);
                    extentReports.CreateStepLogs("Passed", "Hours: " + hoursExl+" logged for Staff: " + staffName + " on Weekly Entry Matrix ");

                    //Go to Summary Logs
                    timeEntry.GoToSummaryLogLV();
                    extentReports.CreateStepLogs("Info", "User has navigated to Summary logs to get the Calculated Amount");

                    // Get total amount displayed
                    double totalAmountDisplayedInSummaryLog = timeEntry.GetTotalAmountLV();

                    //Verify displayed amount before entering a new time sheet
                    Assert.AreEqual(0, totalAmountDisplayedInSummaryLog);
                    extentReports.CreateStepLogs("Passed", "Total Amount: "+totalAmountDisplayedInSummaryLog+"is displayed under Summary logs before Adding the Time Sheet : " + nameRateSheetExl);

                    //Go to Details log
                    timeEntry.GoToDetailLogsLV();
                    extentReports.CreateStepLogs("Info", "User has naigated to Details logs to get the Calculated Amount");

                    // Get total amount displayed
                    double totalAmountDisplayedInDetailLog = timeEntry.GetTotalAmountLV();

                    //Verify displayed amount before entering a new time sheet
                    Assert.AreEqual(0, totalAmountDisplayedInDetailLog);
                    extentReports.CreateStepLogs("Passed", "Total Amount: "+totalAmountDisplayedInDetailLog+ " is displayed under Detail logs before Adding the Time Sheet : " + nameRateSheetExl);

                    timeEntry.GoToWeeklyEntryMatrixLV();
                    //Enter Rate Sheet details
                    string engagement = engName + " - " + engNumber;
                    rateSheetMgt.EnterRateSheetLV(engagement, nameRateSheetExl);

                    //Verify selected rate sheet
                    string selectedRateSheet = rateSheetMgt.GetSelectedRateSheetLV();
                    Assert.AreEqual(nameRateSheetExl, selectedRateSheet);
                    extentReports.CreateStepLogs("Passed", "Rate Sheet added for Project: " + selectedRateSheet);

                    //Navigate to Weekly Entry Matrix page
                    timeEntry.GoToStaffTimeSheetTabLV(); 
                    //Go to Summary Logs
                    timeEntry.GoToSummaryLogLV();
                    extentReports.CreateStepLogs("Info", "User has navigated to Summary logs to get the Calculated Amount");

                    //Get Entered Hours displayed
                    //Get total amount calculated with default rate and entered hours
                    double enteredHoursSummaryLogs = double.Parse(timeEntry.GetEnteredHoursInSummaryLogsLV());
                    double totalAmountCalculated = defaultRate * enteredHoursSummaryLogs;

                    // Get total amount displayed
                    double totalAmountDisplayedSummaryLogs = timeEntry.GetTotalAmountLV();

                    //Verify calculated and displayed amount should matches
                    Assert.AreEqual(totalAmountCalculated, totalAmountDisplayedSummaryLogs);
                    extentReports.CreateStepLogs("Passed", "Total Amount: USD " + totalAmountDisplayedSummaryLogs + " is displayed and matching with the calculation based on total hours entered and the default rate based on selected staff under summary log tab");

                    //Go to Details log
                    timeEntry.GoToDetailLogsLV();
                    extentReports.CreateStepLogs("Info", "User has navigated to details log to get the Calculated Amount");

                    //Get Entered Hours displayed
                    //Get total amount calculated with default rate and entered hours
                    double enteredHoursDetailLogs = double.Parse(timeEntry.GetEnteredHoursInDetailLogsLV());
                    totalAmountCalculated = defaultRate * enteredHoursDetailLogs;

                    //Get total amount displayed
                    double totalAmountDisplayedDetailLog = timeEntry.GetTotalAmountLV();

                    //Verify calculated and displayed amount should matches
                    Assert.AreEqual(totalAmountCalculated, totalAmountDisplayedDetailLog);
                    extentReports.CreateStepLogs("Passed", "Total Amount: USD " + totalAmountDisplayedDetailLog + " is displayed and matching with the calculation based on total hours entered and the default rate based on selected staff under details log tab");

                    //Edit hours in detail log
                    //timeEntry.EditEnteredHoursInDetailLog(fileTMTT0013748);
                    string newHoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Update_Hours", row, 3);
                    string newActivityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Update_Hours", row, 2);

                    timeEntry.UpdateDetailLogsHoursLV(activityExl, newHoursExl);
                    extentReports.CreateStepLogs("Info", "Hours updated on detail log section");

                    //Navigate to Weekly Entry Matrix page
                    timeEntry.GoToWeeklyEntryMatrixLV();

                    //Go to Summary Logs
                    timeEntry.GoToSummaryLogLV();
                    extentReports.CreateStepLogs("Info", "User navigated to Summary logs to get the Calculated Amount");

                    //Get Entered Hours displayed Get total amount calculated with default rate and entered hours
                    double updatedHoursInSummaryLogs = double.Parse(timeEntry.GetEnteredHoursInSummaryLogsLV());                    
                    double totalAmountReCalculated = defaultRate * updatedHoursInSummaryLogs;

                    // Get total amount displayed
                    double totalEditedAmountDisplayedInSummaryLogs = timeEntry.GetTotalAmountLV();

                    //Verify calculated and displayed amount should matches
                    Assert.AreEqual(totalAmountReCalculated, totalEditedAmountDisplayedInSummaryLogs);
                    extentReports.CreateStepLogs("Passed", "Total Amount: USD " + totalEditedAmountDisplayedInSummaryLogs + " is displayed is matching with the calculation based on total hours entered and the default rate based on staff under summary log tab");

                    //Go to Details log
                    timeEntry.GoToDetailLogsLV();
                    extentReports.CreateStepLogs("Info", "User navigated to Details logs to get the Calculated Amount");

                    double updatedHoursInDetailLogs = double.Parse(timeEntry.GetEnteredHoursInDetailLogsLV());
                    totalAmountReCalculated = defaultRate * updatedHoursInDetailLogs;

                    //Get total amount displayed
                    double totalUpdatedAmountDisplayedInDetailLogs = timeEntry.GetTotalAmountLV();

                    //Verify calculated and displayed amount should matches
                    Assert.AreEqual(totalAmountReCalculated, totalUpdatedAmountDisplayedInDetailLogs);
                    extentReports.CreateStepLogs("Passed", "Total Amount: USD " + totalUpdatedAmountDisplayedInDetailLogs + " is displayed is matching with the calculation based on total hours entered and the default rate based on staff under details log tab");

                    //Delete time entry records from detail log 
                    timeEntry.RemoveRecordFromDetailLogsLV();
                    extentReports.CreateStepLogs("Info", "Deleted record entry successfully from detail log ");

                    //Delete rate sheet
                    rateSheetMgt.DeleteRateSheetLV(engagement);                    
                    extentReports.CreateLog("Deleted rate sheet entry successfully after verification ");                     
                }
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "User: " + userSupervisorExl + " logged out");
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
                {
                    extentReports.CreateExceptionLog(e.Message);
                    //timeEntry.GoToStaffTimeSheetTabLV();
                    timeEntry.DeleteTimeEntryLV();
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    usersLogin.UserLogOut();
                    driver.Quit();
                }
        }
    }
}

