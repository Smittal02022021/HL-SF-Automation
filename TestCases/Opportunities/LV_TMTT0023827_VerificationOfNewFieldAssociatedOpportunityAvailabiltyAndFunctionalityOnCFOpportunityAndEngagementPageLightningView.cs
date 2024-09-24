using NUnit.Framework;
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
    class LV_TMTT0023827_VerificationOfNewFieldAssociatedOpportunityAvailabiltyAndFunctionalityOnCFOpportunityAndEngagementPageLightningView:BaseClass
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
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileTMTI0054719 = "LV_TMTI0054719_VerificationOfNewFieldAssociatedOpportunityAvailabiltyAndFunctionalityOnCFOpportunityAndEngagementPage";

        private string valAssociatedEng;
        private string nameAssociatedEng;
        private string caoUser;
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
        public void NewFieldAssociatedOpportunityAvailabiltyForCFLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0054719;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                int rowOpp = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                for (int row = 2; row <= rowOpp; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                    //Login as Standard User profile and validate the user
                    string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);

                    homePage.SearchUserByGlobalSearchN(userExl);
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(userExl), true);
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " logged in on Lightning View");
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
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTMTI0054719);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTI0054719);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Info", "User is on Opportunity detail " + displayedTab + " tab ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityNumber);
                    extentReports.CreateStepLogs("Passed", "Opportunity with number : " + opportunityNumber + " is created ");

                    //TMTI0054719	Verification of new field named as "Associated Opportunity" availabilty and functionality on CF Opportunity page.
                    //New Field is Present on Opportunity Detail Page for Standard User
                    Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldPresentLV());
                    extentReports.CreateStepLogs("Passed", "New Field i.e. Associated Opportunity is Present on Opportunity Detail Page for Standard User: " + userExl + " ");

                    // New Field on Opportunity Detail Page is not editable for Standard User
                    Assert.IsFalse(opportunityDetails.IsAssociatedOppFieldEditableLV(), "Verify Associated Engagement should not be editable for Standard User ");
                    extentReports.CreateStepLogs("Passed", "New Field i.e. Associated Opportunity is not Editable for Standard User: " + userExl + " ");

                    // Create External Primary Contact
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    addOpportunityContact.CickAddCFOpportunityContact();
                    addOpportunityContact.CreateContactL2(fileTMTI0054719);
                    extentReports.CreateStepLogs("Info", valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForCFConversionLV2(fileTMTI0054719, valJobType);
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTI0054719);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityLV();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Info", userExl + " Standard User Logged out ");

                    //Logout of user and validate Admin login
                    login.SwitchToClassicView();
                    string user = login.ValidateUser();
                    Assert.AreEqual(user.Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateStepLogs("Passed", "User " + user + " is logged in ");

                    //System Administrator Search for created opportunity
                    extentReports.CreateLog("System Administrator Search for Created Opportunity");
                    opportunityHome.SearchOpportunity(opportunityName);

                    //New Field is Present on Opportunity Detail Page for Admin login
                    Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldPresent());
                    extentReports.CreateStepLogs("Passed", "New Field i.e. Associated Opportunity is Present on Opportunity Detail Page for System Administrator: " + user + " ");

                    // New Field on Opportunity Detail Page is not editable for Admin login
                    Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldEditable(), "Verify Associated Opportunity should be editable for System Administrator ");
                    extentReports.CreateStepLogs("Passed", "New Field i.e. Associated Opportunity is Editable for System Administrator: " + user + " ");

                    //Enter the Associated Opportunity name
                    valAssociatedOpp = ReadExcelData.ReadDataMultipleRows(excelPath, "AssociatedOpp", 2, 1);
                    opportunityDetails.EnterAssociatedOpportunity(valAssociatedOpp);
                    nameAssociatedOpp = opportunityDetails.GetAssociatedOpportunity(); 
                    Assert.AreEqual(nameAssociatedOpp, valAssociatedOpp, "Verify Entered Associated Opportunity as saved ");
                    extentReports.CreateStepLogs("Passed", user + " Entered " + valAssociatedOpp + " as Associated Opportunity and " + nameAssociatedOpp + " is Saved ");

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(fileTMTI0054719);
                    if (valJobType.Equals("Buyside") || valJobType.Equals("Sellside"))
                    {
                        opportunityDetails.UpdateNBCApproval();
                        extentReports.CreateStepLogs("Ingo", "Conflict Check and NBC fields are updated ");
                    }
                    else
                    {
                        extentReports.CreateStepLogs("Info", "Conflict Check fields are updated ");
                    }

                    //Update Client and Subject to Accupac bypass EBITDA field validation for JobType- Sellside
                    if (valJobType.Equals("Sellside"))
                    {
                        opportunityDetails.UpdateClientandSubject("Accupac");
                        extentReports.CreateStepLogs("Info", "Updated Client and Subject fields ");
                    }
                    else
                    {
                        extentReports.CreateStepLogs("Info", "Not required to update ");
                    }
                    homePage.SearchUserByGlobalSearchN(userExl);
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(userExl), true);
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " logged in on Lightning View");
                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    extentReports.CreateLog(stdUser+" Standard User Search for Created Opportunity ");
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                    //Requesting for engagement and validate the success message
                    opportunityDetails.ClickRequestToEngL();
                    //Submit Request To Engagement Conversion 
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");

                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Passed", "Standard User: " + stdUser + " logged out ");

                    //Login as CAO user to approve the Opportunity
                    string userCAOExl = ReadExcelData.ReadData(excelPath, "Users", 2);
                    extentReports.CreateStepLogs("Info", "login as CAO  User switched to Lightning View ");
                    homePage.SearchUserByGlobalSearchN(userCAOExl);
                    extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    string userCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(userCAO.Contains(userCAOExl), true);
                    extentReports.CreateStepLogs("Passed", "User: " + userCAOExl + " logged in on Lightning View");

                    //Go to Opportunity module in Lightning View 
                    appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    extentReports.CreateLog(userCAOExl + " CAO User Search for Created Opportunity");
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                    //New Field is Present on Opportunity Detail Page for CAO user
                    Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldPresentLV());
                    extentReports.CreateStepLogs("Passed", "New Field i.e. Associated Opportunity is Present on Opportunity Detail Page for CAO User: " + userCAOExl + " ");

                    //New Field on Opportunity Detail Page is not editable for CAO User
                    Assert.IsTrue(opportunityDetails.IsAssociatedOppFieldEditableLV(), "Verify Associated Engagement should be editable for CAO User ");
                    extentReports.CreateStepLogs("Passed", "New Field i.e. Associated Opportunity is Editable for CAO User: " + userCAOExl + " ");

                    //Enter the Associated Opportunity name
                    valAssociatedOpp = ReadExcelData.ReadDataMultipleRows(excelPath, "AssociatedOpp", 2, 2);
                    opportunityDetails.EnterAssociatedOpportunityLV(valAssociatedOpp);
                    nameAssociatedOpp = opportunityDetails.GetAssociatedOpportunityLV();
                    Assert.AreEqual(nameAssociatedOpp, valAssociatedOpp, "Verify Entered Associated Opportunity as saved ");
                    extentReports.CreateStepLogs("Passed", userCAOExl + " Entered " + valAssociatedOpp + " as Associated Opportunity and " + nameAssociatedOpp + " is Saved ");

                    //Approve the Opportunity 
                    string status = opportunityDetails.ClickApproveButtonL();
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

                    //TMTI0054723 Verification of new field named as "Associated Engagement" availabilty and functionality on CF Engagment page.
                    //New Field is Present on Opportunity Detail Page for CAO User
                    Assert.IsTrue(engagementDetails.IsAssociatedEngFieldPresentLV());
                    extentReports.CreateStepLogs("Passed", "New Field i.e. Associated Opportunity is Present on Engagement Detail Page for CAO User: " + userCAOExl + " ");

                    //New Field on Opportunity Detail Page is not editable for CAO User
                    Assert.IsTrue(engagementDetails.IsAssociatedEngFieldEditableLV(), "Verify Associated Engagement should be editable for CAO User ");
                    extentReports.CreateStepLogs("Passed", "New Field i.e. Associated Engagement is Editable for CAO User: " + userCAOExl + " ");

                    //Enter the Associated Opportunity name
                    valAssociatedEng = ReadExcelData.ReadDataMultipleRows(excelPath, "AssociatedEng", 2, 1);
                    engagementDetails.EnterAssociatedEngagementLV(valAssociatedEng);
                    nameAssociatedEng = engagementDetails.GetAssociatedEngagementLV();  
                    Assert.AreEqual(nameAssociatedEng, valAssociatedEng, "Verify Entered Associated Engagement as saved ");
                    extentReports.CreateStepLogs("Passed", caoUser + " Entered " + valAssociatedEng + " as Associated Engagement and " + nameAssociatedEng + " is Saved ");

                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Info", "CAO User " + userCAOExl + "Logged Out");
                    login.SwitchToClassicView();
                    //Logout of user and validate Admin login
                    user = login.ValidateUser();
                    extentReports.CreateStepLogs("Info", "User " + user + " is able to login ");
                    extentReports.CreateStepLogs("Info", "System Administrator Search for Created Opportunity");

                    //Search for created Engagement
                    engagementHome.SearchEngagement(engagementName);
                    //New Field is Present on Opportunity Detail Page for System Admin 
                    Assert.IsTrue(engagementDetails.IsAssociatedEngFieldPresent());
                    extentReports.CreateStepLogs("Passed", "New Field i.e. Associated Engagement is Present on Engagement Detail Page for System Administrator: " + user + " ");

                    // New Field on Opportunity Detail Page is not editable for System Admin 
                    Assert.IsTrue(engagementDetails.IsAssociatedEngFieldEditable(), "Verify Associated Engagement should be editable for System Administrator ");
                    extentReports.CreateStepLogs("Passed", "New Field i.e. Associated Engagement is Editable for System Administrator: " + user + " ");

                    //Enter the Associated Opportunity name
                    valAssociatedEng = ReadExcelData.ReadDataMultipleRows(excelPath, "AssociatedEng", 3, 1);
                    engagementDetails.EnterAssociatedEngagement(valAssociatedEng);
                    //nameAssociatedEng = engagementDetails.GetAssociatedEngagement();
                    //Assert.AreEqual(nameAssociatedEng, valAssociatedEng, "Verify Entered Associated Engagement as saved ");
                    //extentReports.CreateStepLogs("Passed", user + " Entered " + valAssociatedEng + " as Associated Engagement and " + nameAssociatedEng + " is Saved ");
                    homePage.SearchUserByGlobalSearchN(userExl);
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(userExl), true);
                    extentReports.CreateStepLogs("Passed", "User: " + userExl + " logged in on Lightning View");

                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 2);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");                    
                    extentReports.CreateLog("Standard User Search for converted Engagement ");

                    //Search for created Engagement
                    engagementHome.SearchEngagementInLightningView(engagementName);

                    //New Field is Present on Opportunity Detail Page for Standard User
                    Assert.IsTrue(engagementDetails.IsAssociatedEngFieldPresentLV());
                    extentReports.CreateStepLogs("Passed", "New Field i.e. Associated Engagement is Present on Engagement Detail Page for Standard User " + userExl + " ");

                    // New Field on Opportunity Detail Page is not editable for Standard User
                    Assert.IsFalse(engagementDetails.IsAssociatedEngFieldEditableLV(), "Verify Associated Engagement should not be editable for Standard User ");
                    extentReports.CreateStepLogs("Passed", "New Field i.e. Associated Engagement is not Editable for Standard User " + userExl + " ");

                    homePageLV.UserLogoutFromSFLightningView();
                    usersLogin.UserLogOut();
                    driver.Quit();
                    extentReports.CreateStepLogs("Pass", "Browser Closed");
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
