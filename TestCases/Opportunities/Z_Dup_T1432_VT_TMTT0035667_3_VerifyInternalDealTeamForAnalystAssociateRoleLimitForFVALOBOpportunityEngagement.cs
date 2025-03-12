using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class Z_Dup_T1432_VT_TMTT0035667_3_VerifyInternalDealTeamForAnalystAssociateRoleLimitForFVALOBOpportunityEngagement:BaseClass
    {
        //included in T1432
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTMTI0055011 = "TMTT0035667_VerifyInternalDealTeamForAnalystRoleLimitForFVALOBOpportunityEngagement";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyDealTeamAnalystAssociateRoleOnFVAOppEngPage()
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
                //extentReports.CreateStepLogs("Info", "Verify Internal Deal Team For Analyst Role Limit For FVA LOB Opportunity & Engagement ");
                //Reading the Desired Roles from Excel
                int rowRole = ReadExcelData.GetRowCount(excelPath, "Roles");

                for (int row = 2; row <= rowRole; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);

                    //Login as Standard User profile and validate the user
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                    string stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                    extentReports.CreateStepLogs("Pass", "User: " + stdUser + " logged in ");

                    //Call function to open Add Opportunity Page
                    opportunityHome.ClickOpportunity();
                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                    extentReports.CreateStepLogs("Info", "Opportunity Record Type: " + valRecordType + " ");
                    opportunityHome.SelectLOBAndClickContinue(valRecordType);

                    //Validating Title of New Opportunity Page
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateStepLogs("Pass", driver.Title + " is displayed ");

                    //Calling AddOpportunities function
                    string opportunityName = addOpportunity.AddOpportunities(valJobType, fileTMTI0055011);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate opportunity detail page
                    clientSubjectsPage.EnterStaffDetails(fileTMTI0055011);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityName + " ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateStepLogs("Info", driver.Title + " is displayed ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                    Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                    extentReports.CreateStepLogs("Pass", "Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact         
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    addOpportunityContact.CreateContact(fileTMTI0055011, valContact, valRecordType, valContactType);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateStepLogs("Pass", valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForFVAConversion(fileTMTI0055011);
                    extentReports.CreateStepLogs("Info", " All Fields Required to Request for Engagement are provided ");
                    opportunityDetails.UpdateInternalTeamDetails(fileTMTI0055011);
                    extentReports.CreateStepLogs("Info", " All Necassary Internal Team Details for Required to Request for Engagement are provided ");

                    /////////////////////////////////////////////////
                    //TMTI0085044	Verify the Internal deal team "Analyst and Associate Roles" role increased limit for FVA LOB Opportunity

                    //AddMultiple Staff 
                    string memberRole = ReadExcelData.ReadDataMultipleRows(excelPath, "Roles", row, 1);
                    string exectedMaxLimit = ReadExcelData.ReadDataMultipleRows(excelPath, "OverLimitMessage", row, 2);
                    extentReports.CreateStepLogs("Info", " Internal Team Members Limit with Role:" + memberRole + " on  Opportunity ");
                    int countOppDealTeamMember = opportunityDetails.AddOppMultipleDealTeamMembers(valRecordType, memberRole, fileTMTI0055011);
                    Assert.AreEqual(exectedMaxLimit, countOppDealTeamMember.ToString());
                    extentReports.CreateStepLogs("Pass", countOppDealTeamMember + " Internal Team Members with Role:"+ memberRole+" are added to Opportunity ");


                    string msgActualLimit = opportunityDetails.ValidateDealTeamMemberOverLimit();//extra +1
                    string exectedLimitMessage = ReadExcelData.ReadDataMultipleRows(excelPath, "OverLimitMessage",row, 1);
                    Assert.AreNotEqual(exectedLimitMessage, msgActualLimit);
                    extentReports.CreateStepLogs("Pass", msgActualLimit + " is Displayed after Adding "+ countOppDealTeamMember+" deal team members");

                    //get the line error message from internal staff page.
                    string txtLineErrorMessage = opportunityDetails.GetLineErrorMessage();
                    string maxMemberLimit = ReadExcelData.ReadDataMultipleRows(excelPath, "OverLimitMessage",row, 2);
                    Assert.IsFalse(txtLineErrorMessage.Contains(maxMemberLimit));
                    extentReports.CreateStepLogs("Pass", "Line Message: " + txtLineErrorMessage + " is Displayed on header of Opportunity Internal Team Member page ");
                    ////////////////////////////////////////
                    ///

                    //Logout of user and validate Admin login
                    usersLogin.UserLogOut();
                    Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateStepLogs("Pass", "User " + login.ValidateUser() + " is able to login ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(opportunityName);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(fileTMTI0055011);
                    extentReports.CreateStepLogs("Info", "Conflict Check fields are updated ");

                    //Login again as Standard User
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                    string stdUser1 = login.ValidateUser();
                    Assert.AreEqual(stdUser1.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                    extentReports.CreateStepLogs("Pass", "User: " + stdUser1 + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(opportunityName);

                    //Update Total Anticipated Revenue
                    opportunityDetails.UpdateTotalAnticipatedRevenueForValidations();

                    //Requesting for engagement and validate the success message
                    string msgSuccess = opportunityDetails.ClickRequestEng();
                    Assert.AreEqual(msgSuccess, "Submission successful! Please wait for the approval");
                    extentReports.CreateStepLogs("Pass", "Success message: " + msgSuccess + " is displayed ");

                    //Log out of Standard User
                    usersLogin.UserLogOut();

                    //Login as CAO user to approve the Opportunity
                    usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 2));
                    string caoUser = login.ValidateUser();
                    Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                    extentReports.CreateStepLogs("Pass", "User: " + caoUser + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(opportunityName);

                    //Approve the Opportunity 
                    opportunityDetails.ClickApproveButton();
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateStepLogs("Pass", "Opportunity is approved ");

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEng();

                    //Validate the Engagement name in Engagement details page
                    string engName = engagementDetails.GetEngName();
                    Assert.AreEqual(opportunityNumber, engName);
                    extentReports.CreateStepLogs("Pass", "Opportunity is Converted to Engagement ");
                    extentReports.CreateStepLogs("Info", "Name of Engagement : " + engName + " is similar to Opportunity name ");

                    ///////////////////////////////////////////
                    //TMTI0085043	Verify the Internal deal team "Analyst and Associate Roles" role increased limit for FVA LOB Engagement
                    int countEngDealTeamMember = engagementDetails.GetInernalTeamMembersCount();
                    Assert.AreEqual(exectedMaxLimit,(countEngDealTeamMember - 1).ToString());
                    extentReports.CreateStepLogs("Pass", "Opportunity Deal Team Member : " + (countEngDealTeamMember - 1) + " are Present on Converted Engagement ");
                   //////////////////////////////////////////////////
                    
                    //Not need to add more members as per the test case //

                    /*int countEngDealTeamMember = engagementDetails.AddEngMultipleDealTeamMembers(valRecordType, memberRole, fileTMTI0055011);
                    extentReports.CreateLog(countEngDealTeamMember + " more Internal Team Members with Role: " + memberRole+" are added to Engagement after conversion after Conversion ");

                    msgActualLimit = opportunityDetails.ValidateDealTeamMemberOverLimit();//extra +1
                    //string exectedLimitMessage = ReadExcelData.ReadDataMultipleRows(excelPath, "OverLimitMessage",2, 1);
                    Assert.AreEqual("No Pop-up Message Displayed", msgActualLimit);
                    extentReports.CreateLog("Popup with Message: " + msgActualLimit + " is Displayed ");

                    //get the line error message from internal staff page.
                    txtLineErrorMessage = opportunityDetails.GetLineErrorMessage();
                    //string maxMemberLimit = ReadExcelData.ReadDataMultipleRows(excelPath, "OverLimitMessage",2, 2);
                    Assert.AreEqual("No Line Error Validation Message", txtLineErrorMessage);
                    extentReports.CreateLog("Line Message: " + txtLineErrorMessage + " is Displayed on header of Opportunity Internal Team Member page ");
                    */

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
