using AventStack.ExtentReports;
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
    class LV_TMTI0045474_VerifytheAmountIsCalculatedOnTimeRecordManagerForNewEngagement:BaseClass
    {//TMTT0020334
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileTC45474 = "LV_TMTI0045474_VerifytheAmountIsCalculatedOnTimeRecordManagerForNewEngagement";

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
        private string engagementExl;
        private string textMessage;
        private string hoursExl;
        private string activityExl;
        private string rateSheetExl;
        //private string userExl;
        private string userGrpNameExl;



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
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC45474;
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
                usersLogin.SearchUserAndLogin(UserCFExl);

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
                extentReports.CreateStepLogs("Info", "Creating Opportunity for LOB: " + valRecordType + " and Job Type: "+ valJobType);
                string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileTC45474);
                extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");
                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileTC45474);
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
                addOpportunityContact.CreateContactL2(fileTC45474);
                extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                //Update required Opportunity fields for conversion and Internal team details
                opportunityDetails.UpdateReqFieldsForFVAConversionLV(fileTC45474);                

                extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "CF Financial User Logged out ");

                //Login as System Admin user 
                string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 3);
                extentReports.CreateStepLogs("Info", "System Admin User: " + adminUserExl + " Updating the Required details ");
                usersLogin.SearchUserAndLogin(adminUserExl);

                login.SwitchToClassicView();
                string userAdmin = login.ValidateUser();
                Assert.AreEqual(userAdmin.Contains(adminUserExl), true);
                extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUserExl + " User logged in ");

                //login.SwitchToClassicView();
                opportunityHome.SearchOpportunity(opportunityName);
                extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                //update CC 
                opportunityDetails.UpdateOutcomeDetails(fileTC45474);
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
                opportunityDetails.UpdateInternalTeamDetailsLV(fileTC45474);
                extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                opportunityDetails.ClickReturnToOpportunityLV();
                extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                homePageLV.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Passed", "Admin: " + adminUserExl + "switched to Classic and Loggout ");

                //Submit Request to Convert opportunity into Engagement.
                extentReports.CreateStepLogs("Info", "Submit Request to Convert opportunity into Engagement");
                usersLogin.SearchUserAndLogin(UserCFExl);
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
                    
                usersLogin.SearchUserAndLogin(userCAOExl);
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
                extentReports.CreateStepLogs("Info", "Number of Engagement : " + engNumber + " is Same as Opportunity number ");
                string engName = engagementDetails.GetEngagementNameL();
                Assert.AreEqual(opportunityName, engName);
                extentReports.CreateStepLogs("Passed", "Converted Name of Engagement : " + engName + " is Same as Opportunity Name : " + opportunityName);

                homePageLV.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", "CAO User: " + UserCFExl + " Loggout ");

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //Login as supervisor
                string userSupervisorExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SupervisorUser", 2, 1);
                userGrpNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SupervisorUser", 2, 2);
                homePage.SearchUserByGlobalSearch(userSupervisorExl);
                usersLogin.LoginAsSelectedUser();

                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(userSupervisorExl), true);
                extentReports.CreateStepLogs("Passed", "Supervosor User: " + userSupervisorExl + " from Time Tracking Group: " + userGrpNameExl + "  logged in ");
               // homePageLV.ClickAppLauncher();
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", userSupervisorExl + " is  on Module:  " + moduleNameExl);

                //Get the default rate of user as per role                
                string staffNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", 2, 1);
                string staffTitle = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", 2, 2);
                string defaultRateExl = ReadExcelData.ReadDataMultipleRows(excelPath, "StaffMember", 2, 3);
                extentReports.CreateLog(staffTitle + " is : USD " + defaultRateExl + " ");

                //Select Staff Member from the list                    
                timeEntry.GoToStaffTimeSheetTabLV();
                timeEntry.SelectStaffMemberLV(staffNameExl);
                string staffName = timeEntry.GetSelectedStaffNameLV();
                Assert.AreEqual(staffNameExl, staffName);
                extentReports.CreateStepLogs("Passed", "Staff : " + staffName + " is Selected from list ");

                //Enter Rate Sheet details
                engagementExl = engName;
                rateSheetExl = ReadExcelData.ReadDataMultipleRows(excelPath, "RateSheetManagement", 2, 2);
                rateSheetMgt.EnterRateSheetLV(engagementExl, rateSheetExl);

                //Verify selected rate sheet
                string selectedRateSheet = rateSheetMgt.GetSelectedRateSheetLV();
                Assert.AreEqual(rateSheetExl, selectedRateSheet);
                extentReports.CreateStepLogs("Passed", "Rate Sheet added for Project: " + engagementExl);

                timeEntry.GoToStaffTimeSheetTabLV();
                timeEntry.GoToSummaryLogLV();
                extentReports.CreateStepLogs("Passed", "User: " + userSupervisorExl + " is on Summary Log Page for Selected Staff: " + staffName);
                selectProject = engagementExl;
                hoursExl = ReadExcelData.ReadData(excelPath, "SummaryLogs", 2);
                activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "SummaryLogs", 2, 3);

                //Enter time under Summary Logs                    
                textMessage = timeEntry.EnterSummaryLogsHoursLV(selectProject, activityExl, hoursExl);
                Assert.AreEqual(textMessage, "Time Record Added");
                extentReports.CreateStepLogs("Passed", " Hours entered on Summary Logs Page with Success Message: " + textMessage);

                double expectedBilledAmount = Convert.ToDouble(hoursExl) * Convert.ToDouble(defaultRateExl);
                extentReports.CreateStepLogs("Passed", "Expected Calculated Amount with the Selected sheet for Staff Title: " + staffTitle + " should be:: " + expectedBilledAmount);

                //Verify that the FVA Supervisor can see the calculated on Summay logs amount as per the rate sheet                
                double actualSummaryLogsBilledAmount = timeEntry.GetTotalAmountLV();
                Assert.AreEqual(expectedBilledAmount, actualSummaryLogsBilledAmount, "Verify the Amount is calculaton as per the selected Rate sheet on Summary Logs page ");
                
                extentReports.CreateStepLogs("Passed", "Amount is calculaton as per the selected Rate sheet on Summary Logs page");

                timeEntry.DeleteTimeEntryLV();
                extentReports.CreateStepLogs("Passed", "Time Entry Deleted");

                //Verify that the FVA user can access the Detail Logs tab.
                timeEntry.GoToDetailLogsLV();
                extentReports.CreateStepLogs("Passed", "User: " + userSupervisorExl + " is on Detail Logs Pagefor Selected Staff: " + staffName);
                //selectProject = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", 2, 1);
                hoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", 2, 2);
                activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "DetailLogs", 2, 3);

                textMessage = timeEntry.EnterDetailLogsHoursLV(selectProject, activityExl, hoursExl);
                Assert.AreEqual(textMessage, "Time Record Added");
                extentReports.CreateStepLogs("Passed", " Hours entered on Detail Logs Page with Success Message: " + textMessage);

                //Verify that the FVA Supervisor can see the calculated on Detail Logs logs amount as per the rate sheet
                double actualDetailLogsBilledAmount = timeEntry.GetTotalAmountLV();
                Assert.AreEqual(expectedBilledAmount, actualDetailLogsBilledAmount, "Verify the Amount is calculaton as per the selected Rate sheet on Detail Logs page ");
                
                extentReports.CreateStepLogs("Passed", "Amount is calculaton as per the selected Rate sheet on Detail Logs page");

                timeEntry.RemoveRecordFromDetailLogsLV();
                extentReports.CreateStepLogs("Passed", "Time Entry Deleted from Detail Logs Page");

                timeEntry.GoToStaffTimeSheetTabLV();
                rateSheetMgt.DeleteRateSheetLV(engagementExl);
                extentReports.CreateStepLogs("Passed", "Ratesheet: " + rateSheetExl + " deleted from Ratesheet Manageement page");
            
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "User: " + userSupervisorExl + " logged out");
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
                catch (Exception e)
                {
                    extentReports.CreateExceptionLog(e.Message);
                    timeEntry.GoToStaffTimeSheetTabLV();
                    timeEntry.DeleteTimeEntryLV();
                    rateSheetMgt.DeleteRateSheetLV(engagementExl);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    usersLogin.UserLogOut();
                    driver.Quit();
                }
        }
    }
}

