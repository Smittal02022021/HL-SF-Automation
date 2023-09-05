using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using NUnit.Framework;
using SF_Automation.TestData;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class LV_TMTT0024069_VerifyUserIsAbleToEditOtherJobTypeToNewJobTypeForExistingOpportunityEngagementLightningView : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engagementHome = new EngagementHomePage();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileTMTI0055389 = "TMTI0055389_EditExistingOppEngToNewCFJobType";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void EditCFOpportunityEngagementWithNewJobTypeLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0055389;

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
                    string newJobType= ReadExcelData.ReadData(excelPath, "NewJobTypes", 1);
                    //Login as Standard User profile and validate the user
                    string valUser = ReadExcelData.ReadData(excelPath, "StandardUsers", 1);
                    usersLogin.SearchUserAndLogin(valUser);
                    login.SwitchToClassicView();

                    string stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser + " logged in ");

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
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectCFOpp();
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTMTI0055389);//updated move to jobtype
                    extentReports.CreateLog("Opportunity : " + opportunityName + " is created ");

                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTI0055389);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateLog("User is on Opportunity detail " + displayedTab + " tab ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact      
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);

                    addOpportunityContact.CickAddCFOpportunityContact();
                    addOpportunityContact.CreateContactL2(fileTMTI0055389);
                    extentReports.CreateLog(valContact + " is added as " + valContactType + " opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForCFConversionLV2(fileTMTI0055389);//udated Move to element
                    extentReports.CreateLog("Opportunity Required Fields for Converting into Engagement are Filled ");
                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTI0055389);
                    extentReports.CreateLog("Opportunity Internal Team Details are provided ");
                    opportunityDetails.ClickRetutnToOpportunityLV();
                    extentReports.CreateLog("Return to Opportunity Detail page ");

                    //TMTI0055389	Verify user is able to edit any other Job types to new job type for existing opportunity
                    //Get Existing JobType
                    string existingJobType =opportunityDetails.GetDetailPageJobTypeLV();

                    //Get Existing JobType with New JobType
                    opportunityDetails.UpdateJobTypeLV(existingJobType, newJobType);
                    string updatedJobType = opportunityDetails.GetDetailPageJobTypeLV();
                    Assert.AreEqual(newJobType, updatedJobType);
                    extentReports.CreateLog("Job Type is updated from Existing Job Type: "+ existingJobType+" to Job Type: "+ updatedJobType+" Opportunity Detail page ");
                    
                    //Reverting Job Type to Actual Job Type
                    opportunityDetails.UpdateJobTypeLV(newJobType,existingJobType);

                    login.SwitchToClassicView();
                    extentReports.CreateLog(stdUser + " Standard User Switched to Classic View ");
                    //Logout of user and validate Admin login
                    usersLogin.UserLogOut();
                    extentReports.CreateLog(stdUser + " Standard User logged out ");

                    extentReports.CreateLog("Admin is Performing Required Actions ");
                    opportunityHome.SearchOpportunity(opportunityName);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(fileTMTI0055389);
                    if (valJobType.Equals("Buyside") || valJobType.Equals("Sellside"))
                    {
                        opportunityDetails.UpdateNBCApproval();
                        extentReports.CreateLog("Conflict Check and NBC fields are updated ");
                    }
                    else
                    {
                        extentReports.CreateLog("Conflict Check fields are updated ");
                    }

                    //Update Client and Subject to Accupac bypass EBITDA field validation for JobType- Sellside
                    if (valJobType.Equals("Sellside"))
                    {
                        opportunityDetails.UpdateClientandSubject("Accupac");
                        extentReports.CreateLog("Updated Client and Subject fields ");
                    }
                    else
                    {
                        extentReports.CreateLog("Not required to update NBC Approval ");
                    }

                    //TMTI0056861 Verify that NBC form is not required for new Job type - Lender education
                    //Get NBC Approved Default Status
                    Assert.AreEqual(opportunityDetails.GetNBCApprovedStatus(), "Checked");
                    extentReports.CreateLog("NBC Approved Checkbos is already Checked ");

                    //Login again as Standard User
                    usersLogin.SearchUserAndLogin(valUser);
                    login.SwitchToClassicView();

                    stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser + " Standard User logged in ");

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
                    opportunityHome.SearchMyOpportunitiesInLightning(opportunityName, stdUser);

                    //Requesting for engagement and validate the success message
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

                    //TMTI0055386 Verify the availability of new Job Types on Edit Engagement page

                    string existingEngJobType = engagementDetails.GetDetailPageJobTypeLV();
                    //Get Existing JobType with New JobType
                    engagementDetails.UpdateJobType(existingJobType, newJobType);
                    updatedJobType = engagementDetails.GetDetailPageJobTypeLV();
                    Assert.AreEqual(newJobType, updatedJobType);
                    extentReports.CreateLog("Job Type is updated from Existing Job Type: " + existingJobType + " to Job Type: " + updatedJobType + " Engagement Detail page ");

                    //Reverting Job Type to Actual Job Type
                    engagementDetails.UpdateJobType(newJobType, existingJobType);
                    login.SwitchToClassicView();

                    //engagementHome.ClickEngagementTab();
                    //engagementHome.SearchEngagementWithNumber(engagementNumber);
                    //Validate the value of Record Type in Engagement details page
                    //string engRecordType = engagementDetails.GetRecordType();
                    //string recordTypeExpected = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 2);
                    //Assert.AreEqual(recordTypeExpected, engRecordType);
                    //extentReports.CreateLog("Value of Record type is : " + engRecordType + " for Job Type " + valJobType + " ");

                    
                    usersLogin.UserLogOut();
                    extentReports.CreateLog("User: " + caoUser + " logged out ");
                }
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateLog("Browser Closed ");
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                login.SwitchToClassicView();
                driver.Quit();
            }
        }
    }
}
