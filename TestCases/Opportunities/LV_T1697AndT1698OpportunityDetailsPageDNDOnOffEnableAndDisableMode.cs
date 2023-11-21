using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Opportunities
{
    class LV_T1697AndT1698OpportunityDetailsPageDNDOnOffEnableAndDisableMode:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();

        public static string fileTC1692 = "LV_T1697AndT1698DNDOnOffEnableAndDisableMode";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void DNDOnOffEnableAndDisableModeLigtningView()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1692;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Info", login.ValidateUser() + " is able to login ");

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");

                for (int row = 2; row <= rowJobType; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                    extentReports.CreateStepLogs("Info", "Creating Opportunity with Job Type: " + valJobType + " ");
                    //Login as Standard User profile and validate the user
                    string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                    usersLogin.SearchCFUserAndLogin(user);
                    //login.SwitchToClassicView();
                    string stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(user), true);
                    extentReports.CreateStepLogs("Pass", "User: " + stdUser + " logged in ");
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
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB: " + valRecordType);
                    extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                    string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTC1692);//updated totalDbt
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTC1692);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Pass", "User is on Opportunity detail " + displayedTab + " tab ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                    extentReports.CreateStepLogs("Pass", "Opportunity with number : " + opportunityNumber + " is created ");

                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Pass", "User: "+stdUser+ "switched to Classic and Loggout ");

                   
                    //Login as CAO user 
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2));
                    login.SwitchToClassicView();

                    string caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2)), true);
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

                    //Validate he DND On/Off buton 
                    bool isButtonDisplayed =opportunityDetails.IsButtonDNDOnOffDisplayed();
                    Assert.IsTrue(isButtonDisplayed);
                    extentReports.CreateStepLogs("Pass", "DND On/Off button is displayed for user:  with number : " + caoUser );

                    opportunityDetails.ClickDNDOnOffButtonL();
                    extentReports.CreateStepLogs("Info", "User: " + caoUser + "Clicked on DND On/Off Button ");

                    string txtMessage= randomPages.GetLVMessagePopup();
                    extentReports.CreateStepLogs("Pass", txtMessage);

                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Pass", "User: " + caoUser + "switched to Classic and Loggout ");

                    //Login as user from group DND Approval Q
                    string userDNDApproverExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 3);
                    usersLogin.SearchUserAndLogin(userDNDApproverExl);
                    login.SwitchToClassicView();

                    string userApproverDND = login.ValidateUser();
                    Assert.AreEqual(userApproverDND.Contains(userDNDApproverExl), true);
                    extentReports.CreateLog("User: " + userApproverDND + " DND Approver User logged in ");

                    login.SwitchToLightningExperience();
                    extentReports.CreateLog("User: " + userApproverDND + " Switched to Lightning View ");
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
                    opportunityHome.SearchMyOpportunitiesInLightning(opportunityName, userApproverDND);

                    //Approve the Opportunity 
                    string status = opportunityDetails.ClickApproveButtonL();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateLog("Opportunity " + status + " ");
                    opportunityDetails.CloseApprovalHistoryTabL();

                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    extentReports.CreateStepLogs("Pass", "User: " + userApproverDND + "switched to Classic and Loggout ");

                    //Login as CAO user 
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2));
                    login.SwitchToClassicView();

                    caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2)), true);
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

                }

            }
                catch(Exception e)
                {
                    extentReports.CreateLog(e.Message);
                    usersLogin.UserLogOut();
                    driver.Quit();
                }
        }
    }
}
