using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
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
                    string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                    usersLogin.SearchCFUserAndLogin(userExl);

                    //login.SwitchToClassicView();
                    //string stdUser = login.ValidateUser();
                    //Assert.AreEqual(stdUser.Contains(user), true);
                    //extentReports.CreateStepLogs("Pass", "User: " + stdUser + " logged in ");
                    //login.SwitchToLightningExperience();
                    //extentReports.CreateLog("User: " + stdUser + " Switched to Lightning View ");
                    //homePageLV.ClickAppLauncher();

                    login.SwitchToLightningExperience();
                    string stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(userExl), true);
                    extentReports.CreateLog("User: " + userExl + " logged in on Lightning View");

                    homePageLV.ClickAppLauncher();

                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);                    
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");

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

                    //login.SwitchToClassicView();
                    //usersLogin.UserLogOut();
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Pass", "User: "+ userExl + "switched to Classic and Loggout ");


                    //Login as CAO user 
                    string userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);
                    usersLogin.SearchUserAndLogin(userCAOExl);
                    //login.SwitchToClassicView();

                    //string caoUser = login.ValidateUser();
                    //Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2)), true);
                    //extentReports.CreateLog("User: " + caoUser + " CAO User logged in ");

                    //login.SwitchToLightningExperience();
                    //extentReports.CreateLog("User: " + caoUser + " Switched to Lightning View ");
                    //homePageLV.ClickAppLauncher();

                    login.SwitchToLightningExperience();
                    string userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    extentReports.CreateLog("User: " + userCAOExl + " logged in on Lightning View");

                    homePageLV.ClickAppLauncher();

                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");

                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");
                    
                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                    //Validate he DND On/Off buton 
                    bool isButtonDisplayed =opportunityDetails.IsButtonDNDOnOffDisplayedLV();
                    Assert.IsTrue(isButtonDisplayed);
                    extentReports.CreateStepLogs("Pass", "DND On/Off button is displayed for user:  with number : " + userCAOExl);

                    opportunityDetails.ClickDNDOnOffButtonLV();
                    extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " Clicked on DND On/Off Button ");

                    string txtMessage= randomPages.GetLVMessagePopup();
                    extentReports.CreateStepLogs("Pass", txtMessage);

                    //login.SwitchToClassicView();
                    //usersLogin.UserLogOut();
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Pass", "User: " + userCAOExl + " switched to Classic and Loggout ");

                    //Login as user from group DND Approval Q
                    string userDNDApproverExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 3);
                    usersLogin.SearchUserAndLogin(userDNDApproverExl);
                    //login.SwitchToClassicView();

                    //string userApproverDND = login.ValidateUser();
                    //Assert.AreEqual(userApproverDND.Contains(userDNDApproverExl), true);
                    //extentReports.CreateStepLogs("Pass","User: " + userApproverDND + " DND Approver User logged in ");

                    //login.SwitchToLightningExperience();
                    //extentReports.CreateStepLogs("Info", "User: " + userApproverDND + " Switched to Lightning View ");
                    //homePageLV.ClickAppLauncher();

                    login.SwitchToLightningExperience();
                    string userDND = login.ValidateUserLightningView();
                    Assert.AreEqual(userDND.Contains(userDNDApproverExl), true);
                    extentReports.CreateLog("User: " + userDNDApproverExl + " logged in on Lightning View");
                    homePageLV.ClickAppLauncher();

                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");

                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                    //Approve the Opportunity 
                    string status = opportunityDetails.ClickApproveButtonL();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Pass", "Opportunity DND Request: " + status + " ");
                    opportunityDetails.CloseApprovalHistoryTabL();

                    //Get OppName after DND approved
                    string approvedOppName = opportunityDetails.GetOpportunityNameL();
                    extentReports.CreateStepLogs("Pass", opportunityDetails.ValidateOpportunityNameL(approvedOppName));

                    //login.SwitchToClassicView();
                    //usersLogin.UserLogOut();
                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Pass", "User: " + userDNDApproverExl + "switched to Classic and Loggout ");

                    //Login as CAO user 
                    
                    usersLogin.SearchUserAndLogin(userCAOExl);
                    //login.SwitchToClassicView();

                    //userCAO = login.ValidateUser();
                    //Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    //extentReports.CreateStepLogs("Pass", "User: " + userCAOExl + " CAO User logged in ");

                    //login.SwitchToLightningExperience();
                    //extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " Switched to Lightning View ");
                    //homePageLV.ClickAppLauncher();

                    login.SwitchToLightningExperience();
                    userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    extentReports.CreateLog("User: " + userCAOExl + " logged in on Lightning View");

                    //Go to Opportunity module in Lightning View 
                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Pass", appNameExl + " App is selected from App Launcher ");

                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    string result= opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    Assert.AreEqual("No record found", result);
                    extentReports.CreateStepLogs("Pass", result + " with old opportunity name after DND Approval as expected");

                    //Search for DND Approved opportunity with new name
                    string updatedOpp = opportunityHome.UpdateOppAndSearchLV(approvedOppName);
                    Assert.AreEqual("Record found", updatedOpp);
                    extentReports.CreateStepLogs("Pass", updatedOpp + " with DND Opportunity Name: " + approvedOppName);

                    //login.SwitchToClassicView();
                    //usersLogin.UserLogOut();
                    homePageLV.UserLogoutFromSFLightningView();
                    usersLogin.UserLogOut();
                    driver.Quit();
                    extentReports.CreateStepLogs("Pass", "Browser Closed");
                }
            }
                catch(Exception e)
                {
                    extentReports.CreateExceptionLog(e.Message);
                    login.SwitchToClassicView();
                    usersLogin.UserLogOut();
                    driver.Quit();
                }
        }
    }
}
