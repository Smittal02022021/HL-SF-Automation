using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_TMTT0012113_TMTT0012114_TMTT0012116_TMTT0012119_TMTT0012120_ValidateCoExistFieldForGCATracking : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();

        public static string fileGCATracking = "TMTT0012113_ValidateCoExistFieldForGCATracking";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ValidateCoExistCheckboxWithOnlyHLMemberInDealTeamLV()
        {
            try
            { //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileGCATracking;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                int rowCount = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                for (int row = 2; row <= rowCount; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row,25);
                    string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);
                    homePage.SearchUserByGlobalSearchN(userExl);
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(userExl), true);
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " logged in on Lightning View");
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
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileGCATracking);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileGCATracking);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Info", "User is on Opportunity detail " + displayedTab + " tab ");
                    randomPages.CloseActiveTab("Internal Team");
                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                    extentReports.CreateStepLogs("Passed", "Opportunity with number : " + opportunityNumber + " is created ");

                    //////****************************//////////////////
                    //Co-Exist checkbox is no more available for CF Financial user
                    //randomPages.DetailPageFullViewLV();

                    //Validate CoExist checkbox exist and checked or not on Opportunity Details page
                    //string checkboxValidationResult = opportunityDetails.ValidateIfCoExistFieldIsPresentAndCheckedOrNotLV();
                    //Assert.AreEqual("Co-Exist checkbox is displayed and not-checked", checkboxValidationResult);
                    //extentReports.CreateLog(checkboxValidationResult + " on Opportunity detail page. ");

                    //Validate if Standard User is able to edit the CoExist field
                    //string editValue = opportunityDetails.VerifyIfCoExistFieldIsEditableOrNotLV();
                    //Assert.AreEqual("Co-Exist field is not editable", editValue);
                    //extentReports.CreateLog(editValue + " for Standard User on Opportunity detail page. ");

                    //////****************************//////////////////
                    ///
                    //Create External Primary Contact      
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);

                    addOpportunityContact.ClickAddOpportunityContactLV(valRecordType);
                    addOpportunityContact.CreateContactL2(fileGCATracking);
                    extentReports.CreateStepLogs("Info", "Contact " + valContact + " is added as " + valContactType + " for opportunity with LOB: " + valRecordType);
                                        
                    //Update required Opportunity fields for conversion and Internal team details
                    if (valRecordType == "CF")
                    {
                        opportunityDetails.UpdateReqFieldsForCFConversionLV2(fileGCATracking, valJobType);
                        extentReports.CreateStepLogs("Info", "Fields required for converting CF opportunity to engagement are updated. ");
                    }
                    else if (valRecordType == "FVA")
                    {
                        opportunityDetails.UpdateReqFieldsForFVAConversionLV(fileGCATracking);
                    }
                    else
                    {
                        opportunityDetails.UpdateReqFieldsForFRConversionLV(fileGCATracking);
                        opportunityDetails.UpdateTotalDebtConfirmedLV();
                    }
                    extentReports.CreateStepLogs("Info", "Opportunity of LOB: "+ valRecordType + " All Required Fields for Converting into Engagement are Filled ");
                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "Standard User:" + userExl + " logged out");

                    //Login as System Admin user to Fill Required fields for conversion 
                    string adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 4);
                    extentReports.CreateStepLogs("Info", "System Admin User: " + adminUserExl + " Updating the Required details ");

                    homePage.SearchUserByGlobalSearchN(adminUserExl);
                    extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " details are displayed. ");
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToClassicView();
                    string userAdmin = login.ValidateUser();
                    Assert.AreEqual(userAdmin.Contains(adminUserExl), true);
                    extentReports.CreateStepLogs("Passed", "System Admin User: " + adminUserExl + " User logged in ");

                    opportunityHome.SearchOpportunity(opportunityName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                    //update CC 
                    opportunityDetails.UpdateOutcomeDetails(fileGCATracking);
                    extentReports.CreateStepLogs("Info", "Conflict Check fields are updated");

                    /////////////////////////////////////////////////////////////////////
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "System Admin Switched to Lightning View ");
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
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileGCATracking);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");                    
                    randomPages.CloseActiveTab("Internal Team");

                    randomPages.DetailPageFullViewLV();
                    //Validate CoExist checkbox exist and checked or not on Opportunity Details page for for System Administrator
                    string checkboxValidationResult = opportunityDetails.ValidateIfCoExistFieldIsPresentAndCheckedOrNotLV();
                    Assert.AreEqual("Co-Exist checkbox is displayed and not-checked", checkboxValidationResult);
                    extentReports.CreateStepLogs("Passed", checkboxValidationResult + " for for System Administrator on Opportunity detail page. ");

                    //Validate if Standard User is able to edit the CoExist field for for System Administrator
                    string editValue = opportunityDetails.VerifyIfCoExistFieldIsEditableOrNotLV();
                    Assert.AreEqual("Co-Exist field is editable", editValue);
                    extentReports.CreateStepLogs("Passed", editValue + " for System Administrator on Opportunity detail page. ");
                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "System Administrator: " + adminUserExl + " logged out");
                    
                    //Login as Standard User To Request for Eng
                    homePage.SearchUserByGlobalSearchN(userExl);
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " logged in on Lightning View");
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");
                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                    
                    opportunityDetails.ClickRequestToEngL();                    
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");
                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "Standard User loggout after request for Engagement");

                    //Login as CAO user to approve the Opportunity
                    string userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users",row, 3);
                    homePage.SearchUserByGlobalSearchN(userCAOExl);
                    extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    extentReports.CreateStepLogs("Passed", "User: " + userCAOExl + " logged in on Lightning View");
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    randomPages.DetailPageFullViewLV();

                    //Validate CoExist checkbox exist and checked or not on Opportunity Details page for for CAO
                    checkboxValidationResult = opportunityDetails.ValidateIfCoExistFieldIsPresentAndCheckedOrNotLV();
                    Assert.AreEqual("Co-Exist checkbox is displayed and not-checked", checkboxValidationResult);
                    extentReports.CreateStepLogs("Passed", checkboxValidationResult + " for System Administrator on Opportunity detail page");

                    //Validate if CAO User is able to edit the CoExist field
                    editValue = opportunityDetails.VerifyIfCoExistFieldIsEditableOrNotLV();
                    Assert.AreEqual("Co-Exist field is editable", editValue);
                    extentReports.CreateStepLogs("Passed", editValue + "for CAO User on Opportunity detail page. ");

                    string status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Passed", "Opportunity " + status + " ");
                    opportunityDetails.CloseApprovalHistoryTabL();

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEngagementL2();
                    extentReports.CreateStepLogs("Info", "Opportunity Converted into Engagement ");
                    //Validate the Engagement name in Engagement details page
                    string engagementNumber = engagementDetails.GetEngagementNumberL();
                    string engagementName = engagementDetails.GetEngagementNameL();
                    //Need to get Name of Opp and Eng
                    Assert.AreEqual(opportunityName, engagementName);
                    extentReports.CreateStepLogs("Passed", "Name of Engagement : " + engagementName + " is Same as Opportunity name ");

                    randomPages.DetailPageFullViewLV();//
                    //Validate CoExist checkbox exist and checked or not on Engagement Details page for CAO
                    string checkboxValidationResult1 = engagementDetails.ValidateIfCoExistFieldIsPresentAndCheckedOrNotLV();
                    Assert.AreEqual("Co-Exist checkbox is displayed and not-checked", checkboxValidationResult);
                    extentReports.CreateStepLogs("Passed", checkboxValidationResult1 + " on Engagement detail page. ");

                    //Validate if CAO User is able to edit the CoExist field
                    string editEngValue = engagementDetails.VerifyIfCoExistFieldIsEditableOrNotLV();
                    Assert.AreEqual("Co-Exist field is editable", editValue);
                    extentReports.CreateStepLogs("Passed", editEngValue + " for CAO User on Engagement detail page. ");
                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", userCAOExl+ " CAO User loggout after converting Opportunity into Engagement");

                    //Validate if Admin User is able to edit the CoExist field
                    homePage.SearchUserByGlobalSearchN(adminUserExl);
                    extentReports.CreateStepLogs("Info", "User: " + adminUserExl + " details are displayed. ");
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    extentReports.CreateStepLogs("Passed", "System Admin Switched to Lightning View ");
                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    Assert.AreEqual(appNameExl, homePageLV.GetAppName());
                    extentReports.CreateStepLogs("Passed", appNameExl + " App is selected from App Launcher ");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    randomPages.DetailPageFullViewLV();
                    //Validate CoExist checkbox exist and checked or not on existing Opportunity Details page
                    checkboxValidationResult = opportunityDetails.ValidateIfCoExistFieldIsPresentAndCheckedOrNotLV();
                    Assert.AreEqual("Co-Exist checkbox is displayed and not-checked", checkboxValidationResult);
                    extentReports.CreateStepLogs("Passed", checkboxValidationResult + " for System Administrator on Opportunity detail page");
                    editValue = opportunityDetails.VerifyIfCoExistFieldIsEditableOrNotLV();
                    Assert.AreEqual("Co-Exist field is editable", editValue);
                    extentReports.CreateStepLogs("Passed", editValue + " for System Administrator on Opportunity detail page");
                    randomPages.CloseActiveTab(opportunityName);
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    engagementHome.SearchEngagementInLightningView(engagementName);
                    extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");
                    randomPages.DetailPageFullViewLV();
                    //Validate CoExist checkbox exist and checked or not on existing Opportunity Details page
                    checkboxValidationResult = engagementDetails.ValidateIfCoExistFieldIsPresentAndCheckedOrNotLV();
                    Assert.AreEqual("Co-Exist checkbox is displayed and not-checked", checkboxValidationResult);
                    extentReports.CreateStepLogs("Passed", checkboxValidationResult + " for System Administrator on Engagement detail page");
                    randomPages.CloseActiveTab(engagementName);

                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "System Administrator Logged out after final validation on Opportunity and Engagement detail page for LOB: "+ valRecordType);
                }
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.ClickLogoutFromLightningView();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
