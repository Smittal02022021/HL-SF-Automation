using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_T1683AndT2074_OpportunityDetails_HLInternalTeam_EditAndUpdateAndClientSubjectAsPrimary : BaseClass
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
        RandomPages randomPages = new RandomPages();

        public static string fileLV_T1683 = "LV_T1683_HLInternalTeam_EditAndUpdate";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void HLInternalTeam_EditAndUpdateLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileLV_T1683;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

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
                    string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
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
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileLV_T1683);//updated move to jobtype and cong aggree field                    
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileLV_T1683);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Pass", "User is on Opportunity detail " + displayedTab + " tab ");

                    //opportunityDetails
                    opportunityDetails.NavigaterToClientSubjectTabLV();
                    extentReports.CreateStepLogs("Info", "Navigate To Client Subject Tab ");
                    //Validate if Primary checkbox is checked for added Client and Subject company
                    string valueClient = opportunityDetails.GetPrimaryCheckboxOfClientCompanyLV();
                    Assert.AreEqual("Checked", valueClient);
                    extentReports.CreateStepLogs("Pass", "Primary checkbox corresponding to added Client Company is " + valueClient + " ");

                    string valueSubject = opportunityDetails.GetPrimaryCheckboxOfSubjectCompanyLV();
                    Assert.AreEqual("Checked", valueSubject);
                    extentReports.CreateStepLogs("Pass", "Primary checkbox corresponding to added Subject Company is " + valueSubject + " ");

                    //Call function to update HL -Internal Team details
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileLV_T1683);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickRetutnToOpportunityL();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                    //Validate if user still exists in deal
                    string userExist = opportunityDetails.ValidateUserIfExistsLV(fileLV_T1683);                    
                    Assert.AreEqual("User exists", userExist);
                    extentReports.CreateStepLogs("Pass", userExist + " in the deal ");

                    //Close opened Opportunity
                    randomPages.CloseActiveTab(opportunityName);
                    extentReports.CreateStepLogs("Info", opportunityName + " : Tab is closed ");

                    //Select List View and Validate if Opportunity exists under My Active Opportunities
                    extentReports.CreateStepLogs("Info", "Selecing My Active Opportunities View from List to Verify Opportunity "+ opportunityName+" is present in selected List before changing the user from Internal Team");
                    randomPages.SelectListView("My Active Opportunities");                    
                    string recFound = opportunityHome.SearchMyOpportunitiesLV(opportunityName);
                    Assert.AreEqual("Record found", recFound);
                    extentReports.CreateStepLogs("Pass", "Opportunity is displayed in My Opportunities for user:"+ valUser);

                    //Call fnction to remove the user and it's assigned roles
                    string msgSave = opportunityDetails.RemoveUserFromITTeamLV();
                    Assert.AreEqual("Success: Staff Roles Updated.", msgSave);
                    extentReports.CreateLog(msgSave + " is displayed upon removing and adding new user from HL Internal Team members ");

                    //Validate if user still exists in deal
                    string userExists = opportunityDetails.ValidateUserIfExistsLV(fileLV_T1683);
                    Assert.AreEqual("User does not exist", userExists);
                    extentReports.CreateStepLogs("Pass", userExists + " anymore in the deal ");

                    randomPages.CloseActiveTab(opportunityName);
                    extentReports.CreateStepLogs("Info", opportunityName+" : Tab is closed ");

                    //Select List View and Validate if Opportunity exists under My Active Opportunities 
                    //driver.Navigate().Refresh();
                    //Thread.Sleep(5000);
                    extentReports.CreateStepLogs("Info", "Selecing My Active Opportunities View from List to Verify Opportunity " + opportunityName + " is present in selected List after changing the user from Internal Team");
                    //Select List View
                    randomPages.SelectListView("My Active Opportunities");
                    recFound = opportunityHome.SearchMyOpportunitiesLV(opportunityName);
                    Assert.AreEqual("No record found", recFound);
                    extentReports.CreateStepLogs("Pass", "Opportunity is not displayed in My Opportunities for user:" + valUser);

                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Pass", "User: "+valUser + " Logged out");
                    usersLogin.UserLogOut();
                    driver.Quit();
                    extentReports.CreateStepLogs("Info", "Browser Closed");
                }
            }catch(Exception e)
            {
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
            }

            }
    }
}