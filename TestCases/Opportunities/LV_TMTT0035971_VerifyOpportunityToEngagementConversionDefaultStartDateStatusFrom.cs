﻿using AventStack.ExtentReports.Gherkin.Model;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Globalization;
using static SF_Automation.TestData.ReadJSONData;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_TMTT0035971_VerifyOpportunityToEngagementConversionDefaultStartDateStatus : BaseClass
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
        LVHomePage homePageLV = new LVHomePage();

        public static string fileTMTT0035971 = "TMTT0035971_VerifyOpportunityToEngagementConversionDefaultStartDateStatus";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyStartDateStatusWhileUpdatingExistingOpportunityLightningView()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0035971;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", "User " + login.ValidateUser() + " is able to login ");

                int rowOpp = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                for (int row = 2; row <= rowOpp; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                    extentReports.CreateStepLogs("Info", "Creating Opportunity for : " + valJobType + " ");
                    //Login as Standard User profile and validate the user
                    string valUser = ReadExcelData.ReadData(excelPath, "StandardUsers", 1);
                    usersLogin.SearchUserAndLogin(valUser);
                    login.SwitchToClassicView();

                    string stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateStepLogs("Info", "User: " + stdUser + " logged in ");

                    login.SwitchToLightningExperience();
                    extentReports.CreateLog("User: " + stdUser + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();

                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Pass", appName + " App is selected from App Launcher ");

                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    //Validating Title of New Opportunity Page
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                    extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                    extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType);
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTMTT0035971);//updated move to jobtype and cong aggree field
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0035971);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Pass", "User is on Opportunity detail " + displayedTab + " tab ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                    extentReports.CreateStepLogs("Pass", "Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact      
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);

                    addOpportunityContact.CickAddCFOpportunityContact();
                    addOpportunityContact.CreateContactL2(fileTMTT0035971);
                    extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForCFConversionLV2(fileTMTT0035971);//udated Move to element
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0035971);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");


                    login.SwitchToClassicView();
                    extentReports.CreateLog(stdUser + " Standard User Switched to Classic View ");
                    //Logout of user and validate Admin login
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Info", stdUser + " Standard User logged out ");

                    extentReports.CreateLog("Admin is Performing Required Actions ");
                    opportunityHome.SearchOpportunity(opportunityName);
                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(fileTMTT0035971);
                    if (valJobType.Equals("Buyside") || valJobType.Equals("Sellside"))
                    {
                        opportunityDetails.UpdateNBCApproval();
                        extentReports.CreateStepLogs("Info", "Conflict Check and NBC fields are updated ");
                    }
                    else
                    {
                        extentReports.CreateStepLogs("Info", "Conflict Check fields are updated ");
                    }
                    //Get NBC Approved Default Status
                    Assert.AreEqual(opportunityDetails.GetNBCApprovedStatus(), "Checked");
                    extentReports.CreateStepLogs("Info", "NBC Approved Checkbox is already Checked ");

                    //Login again as Standard User
                    usersLogin.SearchUserAndLogin(valUser);
                    login.SwitchToClassicView();

                    stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateStepLogs("Info", "User: " + stdUser + " Standard User logged in ");

                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Info", "User: " + stdUser + " Standard User Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();

                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Info", appName + " App is selected from App Launcher ");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    opportunityHome.SearchMyOpportunitiesInLightning(opportunityName, stdUser);

                    //TMTI0085986 Verify that if an opportunity is converted into engagement prior to a verbally engaged opportunity, then the "Start Date" field on the engagement detail page gets populated with the date from the Date Engaged field

                    //Opp Stage/Priority is Updted to Verbally Engaged 
                    //opportunityDetails.UpdateStagePriorityL(fileTMTT0035971,row);
                    extentReports.CreateStepLogs("Info", "Verify that if an opportunity is converted into engagement prior to a verbally engaged opportunity, then the Start Date field on the engagement detail page gets populated with the date from the Date Engaged field");
                    //Get DateEngaged on Opportunity Detail Page
                    string valOppDateEngaged = opportunityDetails.GetOppDateEngagedL();
                    extentReports.CreateStepLogs("Info", "Opportunity Date Engaged: " + valOppDateEngaged);

                    //Requesting for engagement and validate the success message
                    extentReports.CreateStepLogs("Info", "Opportunity with Stage other than Verbally Engaged Requesting for Engagement conversion : " + valOppDateEngaged);
                    opportunityDetails.ClickRequestToEngL();

                    //Submit Request To Engagement Conversion 
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");

                    login.SwitchToClassicView();
                    //Log out of Standard User
                    usersLogin.UserLogOut();

                    //Login as CAO user to approve the Opportunity
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "CAOUsers", 1));
                    login.SwitchToClassicView();

                    string caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "CAOUsers", 1)), true);
                    extentReports.CreateLog("User: " + caoUser + " CAO User logged in ");

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
                    opportunityHome.SearchMyOpportunitiesInLightning(opportunityName, caoUser);

                    //Approve the Opportunity 
                    string status = opportunityDetails.ClickApproveButtonL();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateLog("Opportunity " + status + " ");
                    opportunityDetails.CloseApprovalHistoryTabL();

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEngagementL();
                    extentReports.CreateLog("Opportunity Converted into Engagement ");
                    //Validate the Engagement name in Engagement details page
                    string engagementNumber = engagementDetails.GetEngagementNumberL();

                    string engagementName = engagementDetails.GetEngagementNameL();
                    //Need to get Name of Opp and Eng
                    Assert.AreEqual(opportunityName, engagementName);
                    extentReports.CreateLog("Name of Engagement : " + engagementName + " is Same as Opportunity name ");

                    //Get DateENgaged                  
                    string valEngDateEngaged = engagementDetails.GetEngStartDate();   
                    extentReports.CreateStepLogs("Info", " Engagement Start Date: " + valEngDateEngaged+" Opportunity Page Date Engaged: "+ valEngDateEngaged);                    
                    Assert.AreEqual(valEngDateEngaged, valOppDateEngaged);
                    extentReports.CreateStepLogs("Pass", "Date Engaged on Opportunity Page and Start Date on Engagement Page are same after conversion");


                    login.SwitchToClassicView();                    
                    usersLogin.UserLogOut();
                    driver.Quit();
                }
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
        }
    }
}