using AventStack.ExtentReports.Gherkin.Model;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Data;

namespace SF_Automation.TestCases.Opportunity
{
    class T1432_TMTT0024858_TMTT0030610_TMTT0035436_OpportunityToEngagementConversionMappingForFASJobTypesToResultingRTPortfolioValuation : BaseClass
    {
        //Test Data is updated to check the New FVA Jo Type for following Tes Cases.//
        /*
         TMTI0056866_TMTI0056870_TMTI0056872_TMTI0056884 Verify the availability of new Job Type- FA - Portfolio-Auto Struct Prd/Consulting in Job Type Picklist while adding new FVA Opportunity
         TMTI0056870 Verify user is able to create new Opportunity with new Job Type - FA - Portfolio-Auto Struct Prd/Consulting
         TMTI0056872 Verify the availability of Job Types for converted engagement on the Engagement page
         TMTI0056884 Verify the Record Type conversion of Opportunity to Engagement

         TMTI0071643 Verify the availability of new Job Type- CVAS - IP Valuation in Job Type Picklist while adding new FVA Opportunity
         TMTI0071652 Verify the availability of Job Types for converted engagement on the Engagement page 
         TMTI0071653 Verify that the user is able to create new Opportunity with new  Job Type - CVAS - IP Valuation
         TMTI0071656 Verify the Record Type conversion of Opportunity to Engagement

        TMTI0084227	Verify the availability of new Job Type- TAS - ESG Due Diligence & Analytics in Job Type Picklist while adding new FVA Opportunity
        TMTI0084215	Verify the availability of Job Types for converted engagement on the Engagement page
        TMTI0084219	Verify user is able to create new Opportunity with new  Job Type -TAS - ESG Due Diligence & Analytics        
        TMTI0084224	Verify the Record Type conversion of Opportunity to Engagement

        */

        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();


        public static string fileTC1432 = "T1432_OpportunityToEngagementConversionMappingForFASJobTypes.xlsx";
        string memberRole;
        string exectedMaxLimit;
        int countOppDealTeamMember;
        string msgActualLimit;
        string exectedLimitMessage;
        string txtLineErrorMessage;
        string maxMemberLimit;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void OpportunityToEngagementConversionMappingForFAS()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1432;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");                

                for (int row = 2; row <= rowJobType; row++)
                {
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                //Login as Standard User profile and validate the user
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Call function to open Add Opportunity Page
                opportunityHome.ClickOpportunity();
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                extentReports.CreateLog("RecordType: " + valRecordType+" ");
                opportunityHome.SelectLOBAndClickContinue(valRecordType);

                //Validating Title of New Opportunity Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validate Women Led field and Calling AddOpportunities function      
                string womenLed = addOpportunity.ValidateWomenLedField(valRecordType);
                string secName = addOpportunity.GetAdminSectionName(valRecordType);
                Assert.AreEqual("Women Led", womenLed);
                Assert.AreEqual("Administration", secName);
                extentReports.CreateLog("Field with name: " + womenLed + " is displayed under section: " + secName + " ");
                    
                //Calling AddOpportunities function
                string value = addOpportunity.AddOpportunities(valJobType,fileTC1432);
                Console.WriteLine("value : " + value);
                extentReports.CreateLog("Opportunity : " + value + " is created ");

                //Call function to enter Internal Team details and validate opportunity detail page
                clientSubjectsPage.EnterStaffDetails(fileTC1432);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Validating Opportunity details  
                string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                //Create External Primary Contact         
                String valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                String valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                addOpportunityContact.CreateContact(fileTC1432, valContact, valRecordType, valContactType);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                //Update required Opportunity fields for conversion and Internal team details
                opportunityDetails.UpdateReqFieldsForFVAConversion(fileTC1432);
                opportunityDetails.UpdateInternalTeamDetails(fileTC1432);

                if (valJobType.Contains("TAS"))
                {
                    opportunityDetails.UpdateTASServices();
                }


                /////////////////////////////////////////////////
                //TMTI0085044	Verify the Internal deal team "Analyst and Associate Roles" role increased limit for FVA LOB Opportunity                

                if (valJobType == "CVAS - IP Valuation")
                {
                    //AddMultiple Staff for Specific Role
                    memberRole = ReadExcelData.ReadDataMultipleRows(excelPath, "Roles", 2, 1);
                    exectedMaxLimit = ReadExcelData.ReadDataMultipleRows(excelPath, "OverLimitMessage", row, 2);
                    extentReports.CreateStepLogs("Info", "Verify the Internal deal team limit is increased for FVA LOB Opportunity of Role: "+ memberRole+" ");

                    countOppDealTeamMember = opportunityDetails.AddOppMultipleDealTeamMembers(valRecordType, memberRole, fileTC1432);
                    Assert.AreEqual(exectedMaxLimit, countOppDealTeamMember.ToString());
                    extentReports.CreateStepLogs("Pass", countOppDealTeamMember + " Internal Team Members with Role:" + memberRole + " are added to Opportunity ");

                    msgActualLimit = opportunityDetails.ValidateDealTeamMemberOverLimit();//extra +1
                    exectedLimitMessage = ReadExcelData.ReadDataMultipleRows(excelPath, "OverLimitMessage", row, 1);
                    Assert.AreNotEqual(exectedLimitMessage, msgActualLimit);
                    extentReports.CreateStepLogs("Pass", msgActualLimit + " is Displayed after Adding " + countOppDealTeamMember + " deal team members");

                    //get the line error message from internal staff page.
                    txtLineErrorMessage = opportunityDetails.GetLineErrorMessage();
                    maxMemberLimit = ReadExcelData.ReadDataMultipleRows(excelPath, "OverLimitMessage", row, 2);
                    Assert.IsFalse(txtLineErrorMessage.Contains(maxMemberLimit));
                    extentReports.CreateStepLogs("Pass", "Line Message: " + txtLineErrorMessage + " is Displayed on header of Opportunity Internal Team Member page ");
                    
                }
                if (valJobType == "TAS - ESG Due Diligence & Analytics")
                {                 
                    //AddMultiple Staff for Specific Role
                    memberRole = ReadExcelData.ReadDataMultipleRows(excelPath, "Roles", 3, 1);
                    exectedMaxLimit = ReadExcelData.ReadDataMultipleRows(excelPath, "OverLimitMessage", row, 2);
                    extentReports.CreateStepLogs("Info", "Verify the Internal deal team limit is increased for FVA LOB Opportunity of Role: " + memberRole + " ");

                    countOppDealTeamMember = opportunityDetails.AddOppMultipleDealTeamMembers(valRecordType, memberRole, fileTC1432);
                    Assert.AreEqual(exectedMaxLimit, countOppDealTeamMember.ToString());
                    extentReports.CreateStepLogs("Pass", countOppDealTeamMember + " Internal Team Members with Role:" + memberRole + " are added to Opportunity ");

                    msgActualLimit = opportunityDetails.ValidateDealTeamMemberOverLimit();//extra +1
                    exectedLimitMessage = ReadExcelData.ReadDataMultipleRows(excelPath, "OverLimitMessage", row, 1);
                    Assert.AreNotEqual(exectedLimitMessage, msgActualLimit);
                    extentReports.CreateStepLogs("Pass", msgActualLimit + " is Displayed after Adding " + countOppDealTeamMember + " deal team members");

                    //get the line error message from internal staff page.
                    txtLineErrorMessage = opportunityDetails.GetLineErrorMessage();
                    maxMemberLimit = ReadExcelData.ReadDataMultipleRows(excelPath, "OverLimitMessage", row, 2);
                    Assert.IsFalse(txtLineErrorMessage.Contains(maxMemberLimit));
                    extentReports.CreateStepLogs("Pass", "Line Message: " + txtLineErrorMessage + " is Displayed on header of Opportunity Internal Team Member page ");                 
                }
                //////////////////////////////////////////////////

                //Logout of user and validate Admin login
                usersLogin.UserLogOut();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

                //update CC and NBC checkboxes 
                opportunityDetails.UpdateOutcomeDetails(fileTC1432);                
                extentReports.CreateLog("Conflict Check fields are updated ");

                //Login again as Standard User
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadData(excelPath, "Users", 1));
                string stdUser1 = login.ValidateUser();
                Assert.AreEqual(stdUser1.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);

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
                string caoUser = login.ValidateUser();
                Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 2)), true);
                extentReports.CreateLog("User: " + caoUser + " logged in ");

                //Search for created opportunity
                opportunityHome.SearchOpportunity(value);
                string OppContactMember = opportunityDetails.GetOppExternalContact();
                extentReports.CreateLog(OppContactMember + " is added as External Contact on Opportunity page ");
                string OppDealTeamMember = opportunityDetails.GetOppDealTeamMember();
                extentReports.CreateLog(OppDealTeamMember + " is added as Deal Team Member on Opportunity page ");

                //Approve the Opportunity 
                opportunityDetails.ClickApproveButton();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                extentReports.CreateLog("Opportunity is approved ");

                //Calling function to convert to Engagement
                opportunityDetails.ClickConvertToEng();

                //Validate the Engagement name in Engagement details page
                string engName = engagementDetails.GetEngName();
                Assert.AreEqual(opportunityNumber, engName);
                extentReports.CreateLog("Name of Engagement : " + engName + " is similar to Opportunity name ");

                ///////////////////////////////////////////
                /////TMTI0085043   Verify the Internal deal team "Analyst and Associate Roles" role increased limit for FVA LOB Engagement
              
                if (valJobType == "CVAS - IP Valuation"|| valJobType == "TAS - ESG Due Diligence & Analytics")
                {                        
                    int countEngDealTeamMember = engagementDetails.GetInernalTeamMembersCount();
                    Assert.AreEqual(exectedMaxLimit, (countEngDealTeamMember - 1).ToString());
                    extentReports.CreateStepLogs("Pass", "Opportunity Deal Team Member : " + (countEngDealTeamMember - 1) + " are Present on Converted Engagement ");
                }
                //////////////////////////////////////////////////

                //Validate the value of Stage in Engagement details page
                string engStage = engagementDetails.GetStage();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Engagement", 1), engStage);
                extentReports.CreateLog("Value of Stage field is : " + engStage + " for Job Type " + valJobType + " ");

                //Validate the value of Record Type in Engagement details page
                string engRecordType = engagementDetails.GetRecordType();                    
                Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 2), engRecordType);
                extentReports.CreateLog("Value of Record type is : " + engRecordType + " for Job Type " + valJobType + " ");

                //Validate the value of HL Entity in Engagement details page
                string engLegalEntity = engagementDetails.GetLegalEntity();
                Console.WriteLine("engHLEntity: " + engLegalEntity);
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "Engagement", 3), engLegalEntity);
                extentReports.CreateLog("Value of HL Entity is : " + engLegalEntity + " ");

                //Validate the section in which Women led field is displayed
                string lblWomenLed = engagementDetails.ValidateWomenLedField(valJobType);//Updated
                Assert.AreEqual("Women Led", lblWomenLed);
                string secWomenLed = engagementDetails.GetSectionNameOfWomenLedField(valJobType);//Updated
                if (valJobType.Contains("CVAS")|| valJobType.Contains("TAS"))
                {
                    Assert.AreEqual("Closing - Admin Details", secWomenLed);
                }
                else
                {
                    Assert.AreEqual("Closing - Document Checklist", secWomenLed);
                }
                extentReports.CreateLog(lblWomenLed + " field is displayed under section: " + secWomenLed + " ");

                //Validate the value of Women Led in Engagement details page
                string engWomenLed = engagementDetails.GetWomenLed();
                Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30), engWomenLed);
                extentReports.CreateLog("Value of Women Led is : " + engWomenLed + " is same as selected in Opportunity page ");

                string EngContactMember = engagementDetails.GetEngExternalContact();
                string EngDealTeamMember = engagementDetails.GetEngDealTeamMember();
                Assert.AreEqual(OppContactMember, EngContactMember, " Verify Contact added on Opportunity page is mapped on Engagement page after conversion");
                extentReports.CreateLog(EngContactMember + " External Contact added on Opportunity page is mapped on Engagement page after conversion ");
                Assert.AreEqual(OppDealTeamMember, EngDealTeamMember, " Verify Deal Team added on Opportunity page is mapped on Engagement page after conversion");
                extentReports.CreateLog(EngDealTeamMember + " Deal Team added on Opportunity page is mapped on Engagement page after conversion ");

                usersLogin.UserLogOut();
                }
                usersLogin.UserLogOut();
                driver.Quit();                
            }
            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }       
    }
}



