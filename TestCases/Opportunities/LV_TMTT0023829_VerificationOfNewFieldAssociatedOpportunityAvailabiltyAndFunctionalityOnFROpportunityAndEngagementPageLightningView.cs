﻿using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.HomePage;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_TMTT0023829_VerificationOfNewFieldAssociatedOpportunityAvailabiltyAndFunctionalityOnFROpportunityAndEngagementPageLightningView: BaseClass
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
        LVHomePage homePageLV = new LVHomePage();

        public static string fileTMTI0054683 = "LV_TMTI0054683_VerificationOfNewFieldAssociatedOpportunityAvailabiltyAndFunctionalityOnFROpportunityAndEngagementPage";

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
        public void NewFieldAssociatedOpportunityAvailabiltyForFRLV()
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
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 25);
                    //Login as Standard User profile and validate the user
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);

                    usersLogin.SearchUserAndLogin(valUser);
                    login.SwitchToClassicView();

                    stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser + " logged in ");

                    //Switch to Lightning View 
                    login.SwitchToLightningExperience();
                    extentReports.CreateLog("User: " + stdUser + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();

                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");

                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                    //Validating Title of New Opportunity Page
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileTMTI0054683);
                    extentReports.CreateLog("Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTI0054683);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateLog("User is on Opportunity detail " + displayedTab + " tab ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityNumber);
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //TMTI0054683	Verification of new field named as "Associated Opportunity" availabilty and functionality on FR Opportunity page.
                    // New Field on Opportunity Detail Page is not editable for Standard User
                    Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldPresentLV());
                    extentReports.CreateLog("New Field i.e. Associated Opportunity is Present on Opportunity Detail Page for Standard User: " + valUser + " ");

                    // New Field on Opportunity Detail Page is not editable for Standard User
                    Assert.IsFalse(opportunityDetails.IsAssociatedOppFieldEditableLV(), "Verify Associated Engagement should not be editable for Standard User ");
                    extentReports.CreateLog("New Field i.e. Associated Opportunity is not Editable for Standard User: " + valUser + " ");

                    // Create External Primary Contact
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    addOpportunityContact.CickAddFROpportunityContact();
                    addOpportunityContact.CreateContactL2(fileTMTI0054683);
                    extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForFRConversionLV(fileTMTI0054683);
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    //opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTI0054728);
                    //extentReports.CreateLog("Opportunity Internal Team Details are provided ");
                    //opportunityDetails.ClickReturnToOpportunityLV();
                    //extentReports.CreateLog("Return to Opportunity Detail page ");

                    login.SwitchToClassicView();
                    extentReports.CreateLog(stdUser + " Standard User Switched to Classic View ");

                    //Logout of user and validate Admin login
                    usersLogin.UserLogOut();
                    //user = login.ValidateUser();
                    //Assert.AreEqual(user.Equals(ReadJSONData.data.authentication.loggedUser), true);
                    //extentReports.CreateLog("User " + user + " is logged in ");

                    ////System Administrator Search for created opportunity
                    //extentReports.CreateLog("System Administrator Search for Created Opportunity");
                    //opportunityHome.SearchOpportunity(opportunityName);

                    ////New Field is Present on Opportunity Detail Page for Admin login
                    //Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldPresent());
                    //extentReports.CreateLog("New Field i.e. Associated Opportunity is Present on Opportunity Detail Page for System Administrator: " + user + " ");

                    //// New Field on Opportunity Detail Page is not editable for Admin login
                    //Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldEditable(), "Verify Associated Opportunity should be editable for System Administrator ");
                    //extentReports.CreateLog("New Field i.e. Associated Opportunity is Editable for System Administrator: " + user + " ");

                    ////Enter the Associated Opportunity name
                    //valAssociatedOpp = ReadExcelData.ReadDataMultipleRows(excelPath, "AssociatedOpp", 2, 1);
                    //nameAssociatedOpp = opportunityDetails.EnterAssociatedOpportunity(valAssociatedOpp);
                    //Assert.AreEqual(nameAssociatedOpp, valAssociatedOpp, "Verify Entered Associated Opportunity as saved ");
                    //extentReports.CreateLog(user + " Entered " + valAssociatedOpp + " as Associated Opportunity and " + nameAssociatedOpp + " is Saved ");

                    ////update CC and NBC checkboxes 
                    //opportunityDetails.UpdateOutcomeDetails(fileTMTI0054728);
                    //opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTI0054728);
                    //extentReports.CreateLog("Opportunity Internal Team Details are provided ");
                    //opportunityDetails.ClickReturnToOpportunityLV();
                    //Login as System Admin user 
                    string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 3);
                    extentReports.CreateStepLogs("Info", "System Admin User: " + adminUserExl + " Updating the Required details ");
                    usersLogin.SearchUserAndLogin(adminUserExl);
                    login.SwitchToClassicView();
                    user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(adminUserExl), true);
                    extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUserExl + " User logged in ");
                    //login.SwitchToClassicView();
                    opportunityHome.SearchOpportunity(opportunityName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");

                    //New Field is Present on Opportunity Detail Page for Admin login
                    Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldPresent());
                    extentReports.CreateLog("New Field i.e. Associated Opportunity is Present on Opportunity Detail Page for System Administrator: " + user + " ");

                    // New Field on Opportunity Detail Page is not editable for Admin login
                    Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldEditable(), "Verify Associated Opportunity should be editable for System Administrator ");
                    extentReports.CreateLog("New Field i.e. Associated Opportunity is Editable for System Administrator: " + user + " ");

                    //Enter the Associated Opportunity name
                    valAssociatedOpp = ReadExcelData.ReadDataMultipleRows(excelPath, "AssociatedOpp", 2, 1);
                    nameAssociatedOpp = opportunityDetails.EnterAssociatedOpportunity(valAssociatedOpp);
                    Assert.AreEqual(nameAssociatedOpp, valAssociatedOpp, "Verify Entered Associated Opportunity as saved ");
                    extentReports.CreateLog(user + " Entered " + valAssociatedOpp + " as Associated Opportunity and " + nameAssociatedOpp + " is Saved ");

                    opportunityDetails.UpdateOutcomeDetails(fileTMTI0054683);
                    extentReports.CreateStepLogs("Info", "Conflict Check fields are updated");

                    /////////////////////////////////////////////////////////////////////
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "System Admin Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTI0054683);
                    extentReports.CreateStepLogs("Info", "User with all required roles for requesting Engagement is added  ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                    login.SwitchToClassicView();
                    //Logout of user and validate Admin login
                    usersLogin.UserLogOut();

                    //Login again as Standard User
                    extentReports.CreateLog("login as Standard User ");
                    usersLogin.SearchUserAndLogin(valUser);
                    login.SwitchToClassicView();

                    stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("Standard User: " + stdUser + " logged in ");
                    login.SwitchToLightningExperience();

                    extentReports.CreateLog("User: " + stdUser + " Standard User Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();

                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    extentReports.CreateLog(stdUser + " Standard User Search for Created Opportunity ");
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                    //Requesting for engagement and validate the success message
                    opportunityDetails.ClickRequestToEngL();
                    //Submit Request To Engagement Conversion 
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");

                    //Log out of Standard User
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateLog("Standard User: " + stdUser + " logged out ");

                    //Login as CAO user to approve the Opportunity
                    extentReports.CreateLog("login as CAO  User switched to Lightning View ");
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 2));
                    login.SwitchToClassicView();

                    caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                    extentReports.CreateLog("CAO User: " + caoUser + " logged in ");

                    login.SwitchToLightningExperience();
                    extentReports.CreateLog("User: " + caoUser + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();

                    //Go to Opportunity module in Lightning View 
                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    extentReports.CreateLog(caoUser + " CAO User Search for Created Opportunity");
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                    //New Field is Present on Opportunity Detail Page for CAO user
                    Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldPresentLV());
                    extentReports.CreateLog("New Field i.e. Associated Opportunity is Present on Opportunity Detail Page for CAO User: " + caoUser + " ");

                    //New Field on Opportunity Detail Page is not editable for CAO User
                    Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldEditableLV(), "Verify Associated Engagement should be editable for CAO User ");
                    extentReports.CreateLog("New Field i.e. Associated Opportunity is Editable for CAO User: " + caoUser + " ");

                    //Enter the Associated Opportunity name
                    valAssociatedOpp = ReadExcelData.ReadDataMultipleRows(excelPath, "AssociatedOpp", 3, 1);
                    nameAssociatedOpp = opportunityDetails.EnterAssociatedOpportunityLV(valAssociatedOpp);
                    Assert.AreEqual(nameAssociatedOpp, valAssociatedOpp, "Verify Entered Associated Opportunity as saved ");
                    extentReports.CreateLog(caoUser + " Entered " + valAssociatedOpp + " as Associated Opportunity and " + nameAssociatedOpp + " is Saved ");

                    //Approve the Opportunity 
                    string status = opportunityDetails.ClickApproveButtonL();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateLog("Opportunity " + status + " ");
                    opportunityDetails.CloseApprovalHistoryTabL();

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEngagementL2();
                    extentReports.CreateLog("Opportunity Converted into Engagement ");

                    //Validate the Engagement name in Engagement details page
                    string engagementNumber = engagementDetails.GetEngagementNumberL();
                    string engagementName = engagementDetails.GetEngagementNameL();

                    //Need to get Name of Opp and Eng
                    Assert.AreEqual(opportunityName, engagementName);
                    extentReports.CreateLog("Name of Engagement : " + engagementName + " is Same as Opportunity name ");

                    //TMTI0054712 Verification of new field named as "Associated Engagement" availabilty and functionality on FR Engagment page.

                    //New Field is Present on Opportunity Detail Page for CAO User
                    Assert.IsTrue(engagementDetails.IsAssociatedEngFieldPresentL());
                    extentReports.CreateLog("New Field i.e. Associated Opportunity is Present on Engagement Detail Page for CAO User: " + caoUser + " ");

                    //New Field on Opportunity Detail Page is not editable for CAO User
                    Assert.IsTrue(engagementDetails.IsAssociatedEngFieldEditableLV(), "Verify Associated Engagement should be editable for CAO User ");
                    extentReports.CreateLog("New Field i.e. Associated Engagement is Editable for CAO User: " + caoUser + " ");

                    //Enter the Associated Opportunity name
                    valAssociatedEng = ReadExcelData.ReadDataMultipleRows(excelPath, "AssociatedEng", 2, 1);
                    nameAssociatedEng = engagementDetails.EnterAssociatedEngagementL(valAssociatedEng);
                    Assert.AreEqual(nameAssociatedEng, valAssociatedEng, "Verify Entered Associated Engagement as saved ");
                    extentReports.CreateLog(caoUser + " Entered " + valAssociatedEng + " as Associated Engagement and " + nameAssociatedEng + " is Saved ");

                    //CAO Logged Out
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateLog("CAO User " + caoUser + "Logged Out");

                    //Logout of user and validate Admin login
                    user = login.ValidateUser();
                    extentReports.CreateLog("User " + user + " is able to login ");
                    extentReports.CreateLog("System Administrator Search for Engagement");

                    //Search for created Engagement
                    engagementHome.SearchEngagement(engagementName);

                    //New Field is Present on Opportunity Detail Page for System Admin 
                    Assert.IsTrue(engagementDetails.IsAssociatedEngFieldPresent());
                    extentReports.CreateLog("New Field i.e. Associated Engagement is Present on Engagement Detail Page for System Administrator: " + user + " ");

                    // New Field on Opportunity Detail Page is not editable for System Admin 
                    Assert.IsTrue(engagementDetails.IsAssociatedEngFieldEditable(), "Verify Associated Engagement should be editable for System Administrator ");
                    extentReports.CreateLog("New Field i.e. Associated Engagement is Editable for System Administrator: " + user + " ");

                    //Enter the Associated Opportunity name
                    valAssociatedEng = ReadExcelData.ReadDataMultipleRows(excelPath, "AssociatedEng", 3, 1);
                    nameAssociatedEng = engagementDetails.EnterAssociatedEngagement(valAssociatedEng);
                    Assert.AreEqual(nameAssociatedEng, valAssociatedEng, "Verify Entered Associated Engagement as saved ");
                    extentReports.CreateLog(user + " Entered " + valAssociatedEng + " as Associated Engagement and " + nameAssociatedEng + " is Saved ");

                    //Standard User Login 
                    usersLogin.SearchUserAndLogin(valUser);
                    login.SwitchToClassicView();

                    stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateLog("User: " + stdUser + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();

                    homePageLV.SelectApp(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 2);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                    extentReports.CreateLog("Standard User Search for converted Engagement ");

                    //Search for created Engagement
                    engagementHome.SearchMyEngagementInLightning(engagementName, stdUser);

                    //New Field is Present on Opportunity Detail Page for Standard User
                    Assert.IsTrue(engagementDetails.IsAssociatedEngFieldPresentL());
                    extentReports.CreateLog("New Field i.e. Associated Engagement is Present on Engagement Detail Page for Standard User " + stdUser + " ");

                    // New Field on Opportunity Detail Page is not editable for Standard User
                    Assert.IsFalse(engagementDetails.IsAssociatedEngFieldEditableLV(), "Verify Associated Engagement should not be editable for Standard User ");
                    extentReports.CreateLog("New Field i.e. Associated Engagement is not Editable for Standard User " + stdUser + " ");

                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Browser Closed");
                }
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateLog("Browser Closed ");

            }
        }

    }
}
