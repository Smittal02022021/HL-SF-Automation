﻿using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities

{
    class ZObsolated_VT_TMTT0023919_VerifyInternalDealTeamSpecialtyRoleIncreasedLimitForFVALOBOpportunityEngagement : BaseClass
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

        public static string fileTMTI0055011 = "TMTI0055011_VerifyInternalDealTeamSpecialtyRoleIncreasedLimitForFVALOBOpportunityEngagement";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyDealTeamSpecialtyRoleOnFVAOppEngManagerPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0055011;
                

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

                    //Login as Standard User profile and validate the user
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                    string stdUser = login.ValidateUser();
                    login.SwitchToClassicView();
                    Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                    extentReports.CreateLog("User: " + stdUser + " logged in ");

                    //Call function to open Add Opportunity Page
                    opportunityHome.ClickOpportunity();
                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                    extentReports.CreateLog("Opportunity Record Type: " + valRecordType+ " ");
                    opportunityHome.SelectLOBAndClickContinue(valRecordType);

                    //Validating Title of New Opportunity Page
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Calling AddOpportunities function
                    string opportunityName = addOpportunity.AddOpportunities(valJobType, fileTMTI0055011);
                    extentReports.CreateLog("Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate opportunity detail page
                    clientSubjectsPage.EnterStaffDetails(fileTMTI0055011);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityName + " ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                    Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact         
                    String valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    String valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    addOpportunityContact.CreateContact(fileTMTI0055011, valContact, valRecordType, valContactType);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForFVAConversion(fileTMTI0055011);
                    //opportunityDetails.UpdateInternalTeamDetails(fileTMTI0055011);

                    ////TMTI0055011_VerifyInternalDealTeamSpecialtyRoleIncreasedLimitForFVALOBOpportunity
                    ////AddMultiple Staff
                    //int countDealTeamMember = opportunityDetails.AddOppMultipleDealTeamMembers(valRecordType,fileTMTI0055011);
                    //extentReports.CreateLog(countDealTeamMember + " Internal Team Members with Role Specialty are added to Opportunity ");

                    //string msgActualLimit= opportunityDetails.ValidateDealTeamMemberOverLimit();//extra +1
                    //string exectedLimitMessage = ReadExcelData.ReadData(excelPath, "OverLimitMessage", 1);
                    //Assert.AreEqual(msgActualLimit, exectedLimitMessage);
                    //extentReports.CreateLog("Popup with Message: "+ msgActualLimit+" is Displayed ");

                    ////get the line error message from internal staff page.
                    //string txtLineErrorMessage= opportunityDetails.GetLineErrorMessage();
                    //string maxMemberLimit= ReadExcelData.ReadData(excelPath, "OverLimitMessage", 2);
                    //Assert.IsTrue(txtLineErrorMessage.Contains(maxMemberLimit));
                    //extentReports.CreateLog("Line Message: " + txtLineErrorMessage + " is Displayed on header of Opportunity Internal Team Member page ");

                    //Logout of user and validate Admin login
                    usersLogin.UserLogOut();
                    Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(opportunityName);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(fileTMTI0055011);
                    extentReports.CreateLog("Conflict Check fields are updated By Admin ");

                    opportunityDetails.UpdateInternalTeamDetails(fileTMTI0055011);
                    //TMTI0055011_VerifyInternalDealTeamSpecialtyRoleIncreasedLimitForFVALOBOpportunity
                    //AddMultiple Staff
                    int countDealTeamMember = opportunityDetails.AddOppMultipleDealTeamMembers(valRecordType, fileTMTI0055011);
                    extentReports.CreateLog(countDealTeamMember + " Internal Team Members with Role Specialty are added to Opportunity ");

                    string msgActualLimit = opportunityDetails.ValidateDealTeamMemberOverLimit();//extra +1
                    string exectedLimitMessage = ReadExcelData.ReadData(excelPath, "OverLimitMessage", 1);
                    Assert.AreEqual(msgActualLimit, exectedLimitMessage);
                    extentReports.CreateLog("Popup with Message: " + msgActualLimit + " is Displayed ");

                    //get the line error message from internal staff page.
                    string txtLineErrorMessage = opportunityDetails.GetLineErrorMessage();
                    string maxMemberLimit = ReadExcelData.ReadData(excelPath, "OverLimitMessage", 2);
                    Assert.IsTrue(txtLineErrorMessage.Contains(maxMemberLimit));
                    extentReports.CreateLog("Line Message: " + txtLineErrorMessage + " is Displayed on header of Opportunity Internal Team Member page ");


                    //Login again as Standard User
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                    login.SwitchToClassicView();
                    string stdUser1 = login.ValidateUser();
                    Assert.AreEqual(stdUser1.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                    extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(opportunityName);

                    //Update Total Anticipated Revenue
                    opportunityDetails.UpdateTotalAnticipatedRevenueForValidations();

                    //Requesting for engagement and validate the success message
                    string msgSuccess = opportunityDetails.ClickRequestEng();
                    Assert.AreEqual(msgSuccess, "Submission successful! Please wait for the approval");
                    extentReports.CreateLog("Success message: " + msgSuccess + " is displayed ");

                    //Log out of Standard User
                    usersLogin.UserLogOut();

                    //Login as CAO user to approve the Opportunity
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 2));
                    login.SwitchToClassicView();
                    string caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                    extentReports.CreateLog("User: " + caoUser + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(opportunityName);
                    
                    //Approve the Opportunity 
                    opportunityDetails.ClickApproveButton();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog("Opportunity is approved ");

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEng();

                    //Validate the Engagement name in Engagement details page
                    string engName = engagementDetails.GetEngName();
                    Assert.AreEqual(opportunityNumber, engName);
                    extentReports.CreateLog("Opportunity is Converted to Engagement ");
                    extentReports.CreateLog("Name of Engagement : " + engName + " is similar to Opportunity name ");

                    //TMTI0055013_VerifyInternalDealTeamSpecialtyRoleIncreasedLimitForFVALOBEngagement
                    countDealTeamMember = engagementDetails.AddEngMultipleDealTeamMembers(valRecordType, fileTMTI0055011);
                    extentReports.CreateLog(countDealTeamMember + " Internal Team Members with Role Specialty are added to Engagement after conversion after Conversion ");

                    msgActualLimit = opportunityDetails.ValidateDealTeamMemberOverLimit();
                    exectedLimitMessage = ReadExcelData.ReadData(excelPath, "OverLimitMessage", 1);
                    Assert.AreEqual(msgActualLimit, exectedLimitMessage);
                    extentReports.CreateLog("Popup with Message: " + msgActualLimit + " is Displayed ");

                    //get the line error message from internal staff page.
                    txtLineErrorMessage = opportunityDetails.GetLineErrorMessage();
                    Assert.IsTrue(txtLineErrorMessage.Contains(maxMemberLimit));
                    extentReports.CreateLog("Line Message: " + txtLineErrorMessage + " is Displayed on header of Engagement Internal Team Member page ");
                    usersLogin.UserLogOut();
                    extentReports.CreateLog("User: " + caoUser + " logged out ");
                }
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateLog("Browser Closed ");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
            }
        }
    }
}
