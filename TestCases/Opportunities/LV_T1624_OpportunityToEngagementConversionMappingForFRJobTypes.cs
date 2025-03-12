using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using SF_Automation.Pages.HomePage;
using System;

namespace SF_Automation.TestCases.OpportunitiesConversion
{
    class LV_T1624_OpportunityToEngagementConversionMappingForFRJobTypes : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        LVHomePage homePageLV = new LVHomePage();
        HomeMainPage homePage = new HomeMainPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        RandomPages randomPages = new RandomPages();

        public static string fileTC1624 = "LV_T1624_OpportunityToEngagementConversionMappingForFRJobTypes";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void OpportunityToEngagementConversionMappingForFRLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1624;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
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
                    extentReports.CreateStepLogs("Pass", appName + " App is selected from App Launcher ");
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                    //Validating Title of New Opportunity Page
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB: "+valRecordType);
                    extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                    //Validate Women Led field and its associated section
                    string secName = addOpportunity.GetWomenLedSectionNameLV(valRecordType);
                    string womenLed = addOpportunity.ValidateWomenLedFieldLV(valRecordType);                
                    Assert.AreEqual("Administration", secName);
                    Assert.AreEqual("Women Led", womenLed);
                    extentReports.CreateStepLogs("Pass", "Field with name: " + womenLed + " is displayed under section: " + secName + " ");
                    
                    string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTC1624);//updated totalDbt
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTC1624);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Pass", "User is on Opportunity detail " + displayedTab + " tab ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                    extentReports.CreateStepLogs("Pass", "Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact         
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    addOpportunityContact.CickAddFROpportunityContact();
                    addOpportunityContact.CreateContactL2(fileTC1624);
                    extentReports.CreateStepLogs("Info", valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForFRConversionLV(fileTC1624);
                    opportunityDetails.UpdateTotalDebtConfirmedLV();
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTC1624);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickReturnToOpportunityL();
                    randomPages.CloseActiveTab("Internal Team");
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Info", userExl + " Standard User logged out ");

                    extentReports.CreateLog("Admin is Performing Required Actions ");
                    login.SwitchToClassicView();
                    opportunityHome.SearchOpportunity(opportunityName);
                    //update CC 
                    opportunityDetails.UpdateOutcomeDetails(fileTC1624);
                    extentReports.CreateStepLogs("Info", " Required Outcome Details are provided ");
                    //Login again as Standard User
                    homePage.SearchUserByGlobalSearchN(userExl);
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();
                    login.SwitchToLightningExperience();
                    stdUser = login.ValidateUserLightningView();
                    Assert.AreEqual(stdUser.Contains(userExl), true);
                    extentReports.CreateLog("User: " + userExl + " logged in on Lightning View");

                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Pass", appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                    //Requesting for engagement and validate the success message
                    opportunityDetails.ClickRequestToEngL();
                    //Submit Request To Engagement Conversion 
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");

                    homePageLV.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Pass", userExl + " logged out ");

                    string userCAOExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);
                    //Login as CAO user to approve the Opportunity                   
                    homePage.SearchUserByGlobalSearchN(userCAOExl);
                    extentReports.CreateStepLogs("Info", "User: " + userCAOExl + " details are displayed. ");
                    //Login user
                    usersLogin.LoginAsSelectedUser();

                    login.SwitchToLightningExperience();
                    string UserCAO = login.ValidateUserLightningView();
                    Assert.AreEqual(UserCAO.Contains(userCAOExl), true);
                    extentReports.CreateLog("User: " + userCAOExl + " logged in on Lightning View");

                    //Go to Opportunity module in Lightning View 
                    homePageLV.SelectAppLV(appNameExl);
                    appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateLog(appName + " App is selected from App Launcher ");
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                    //Approve the Opportunity 
                    string status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateLog("Opportunity " + status + " ");
                    opportunityDetails.CloseApprovalHistoryTabL();

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEngagementL2();
                    extentReports.CreateLog("Opportunity Converted into Engagement ");

                    // Validate the Engagement name in Engagement details page
                    string engagementNumber = engagementDetails.GetEngagementNumberL();
                    string engagementName = engagementDetails.GetEngagementNameL();

                    //Need to get Name of Opp and Eng
                    Assert.AreEqual(opportunityName, engagementName);
                    extentReports.CreateLog("Name of Engagement : " + engagementName + " is Same as Opportunity name ");

                    randomPages.CloseActiveTab(engagementName);
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    engagementHome.SearchEngagementInLightningView(engagementName);



                    //Validate the value of Stage in Engagement details page
                    string engStage = engagementDetails.GetStageL();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Engagement", 1), engStage);
                    extentReports.CreateLog("Value of Stage field is : " + engStage + " for Job Type " + valJobType + " ");

                    //Validate the value of Record Type in Engagement details page
                    engagementDetails.NavigateToAdministratorTabLV();
                    string engRecordType = engagementDetails.GetRecordTypeLV();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 2), engRecordType);
                    extentReports.CreateLog("Value of Record type is : " + engRecordType + " for Job Type " + valJobType + " ");

                    //Validate the value of HL Entity in Engagement details pages
                    string engHLEntity = engagementDetails.GetLegalEntityLV();
                    extentReports.CreateLog("Engagement HL Entity: " + engHLEntity);
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Engagement", 3), engHLEntity);
                    extentReports.CreateLog("Value of HL Entity is : " + engHLEntity + " ");

                    //Validate the section in which Women led field and value is displayed
                    string secWomenLed = engagementDetails.GetSectionNameOfWomenLedFieldLV(valJobType);
                    Assert.AreEqual("Administrative Info", secWomenLed);
                    string lblWomenLed = engagementDetails.ValidateWomenLedFieldLV();
                    Assert.AreEqual("Women Led", lblWomenLed);
                    extentReports.CreateLog(lblWomenLed + " field is displayed under section: " + secWomenLed + " ");

                    //Validate the value of Women Led in Engagement details page
                    string engWomenLed = engagementDetails.GetValueWomenLedLV();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30), engWomenLed);
                    extentReports.CreateLog("Value of Women Led is : " + engWomenLed + " is same as selected in Opportunity page ");

                    //Internal Deal Tea member on eng page 
                    string engInternalTeamMember = engagementDetails.GetEngDealTeammMemberLV();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 14), engInternalTeamMember);
                    extentReports.CreateStepLogs("Pass", "Internal Deal Team member: " + engInternalTeamMember + " is mapped on Engagement detail page after conversion ");
                                        
                    //Contact on eng page in smapped fom Opportunity
                    string engContactName = engagementDetails.GetEngExternalContactLV();
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddContact", 1), engContactName);
                    extentReports.CreateStepLogs("Pass", "Opportunity Contact: " + engContactName + " is mapped on Engagement detail page after conversion ");

                    homePageLV.UserLogoutFromSFLightningView();                    
                }
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
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