using AventStack.ExtentReports.Gherkin.Model;
using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunity
{
    class T1426_11215_11219_11220_11221_28220_28213_28198_28209_OpportunityToEngagementConversionMappingForCFJobTypesToResultingRTAdvisory_Update : BaseClass
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

        public static string fileTC1426 = "T1426_OpportunityToEngagementConversionMappingForCFJobTypes2.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void OpportunityToEngagementConversionMappingForCF()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1426;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowJobType = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");
                extentReports.CreateLog("rowCount " + rowJobType);

                for (int row = 2; row <= rowJobType; row++)
                {
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);

                    //Login as Standard User profile and validate the user
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 29);
                    usersLogin.SearchUserAndLogin(valUser);
                    string stdUser = login.ValidateUser();
                    Assert.AreEqual(stdUser.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser + " logged in ");

                    //Call function to open Add Opportunity Page
                    opportunityHome.ClickOpportunity();
                    string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);                
                    opportunityHome.SelectLOBAndClickContinue(valRecordType);

                    //Validating Title of New Opportunity Page
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity Edit: New Opportunity ~ Salesforce - Unlimited Edition", 100), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Validate Women Led field and Calling AddOpportunities function      
                    string womenLed = addOpportunity.ValidateWomenLedField(valRecordType);
                    string secName = addOpportunity.GetAdminSectionName(valRecordType);
                    Assert.AreEqual("Women Led", womenLed);
                    Assert.AreEqual("Administration", secName);
                    extentReports.CreateLog("Field with name: " +womenLed +" is displayed under section: "+secName + " " );

                    //TMTI0028220 Verify the availability of new Job Types in Job Type Picklist while adding new Opportunity
                    //TMTI0028213 Verify user is able to create new Opportunity with new Type
                    string value = addOpportunity.AddOpportunities(valJobType,fileTC1426);
                
                    extentReports.CreateLog("Opportunity : " + value + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    clientSubjectsPage.EnterStaffDetails(fileTC1426);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + value + " ~ Salesforce - Unlimited Edition"), true);
                    extentReports.CreateLog(driver.Title + " is displayed ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.ValidateOpportunityDetails();
                    Assert.IsNotNull(opportunityDetails.ValidateOpportunityDetails());
                    extentReports.CreateLog("Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact         
                    string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                    string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                    addOpportunityContact.CreateContact(fileTC1426, valContact, valRecordType, valContactType);
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Opportunity: " + opportunityNumber + " ~ Salesforce - Unlimited Edition", 60), true);
                    extentReports.CreateLog(valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForCFConversion(fileTC1426);
                    opportunityDetails.UpdateInternalTeamDetails(fileTC1426);

                    //Logout of user and validate Admin login
                    usersLogin.UserLogOut();
                    Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                    extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

                    //update CC and NBC checkboxes 
                    opportunityDetails.UpdateOutcomeDetails(fileTC1426);
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
                        Console.WriteLine("Not required to update ");
                    }                    
                
                    //Login again as Standard User
                    usersLogin.SearchUserAndLogin(valUser);
                    string stdUser1 = login.ValidateUser();
                    Assert.AreEqual(stdUser1.Contains(valUser), true);
                    extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                    //Search for created opportunity
                    opportunityHome.SearchOpportunity(value);

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

                    //TMTI0028198 Verify the availability of Job Types for converted engagement on the Engagement page                 
                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEng();

                    //Validate the Engagement name in Engagement details page
                    string engName = engagementDetails.GetEngName();
                    Assert.AreEqual(opportunityNumber, engName);
                    extentReports.CreateLog("Name of Engagement : " + engName + " is similar to Opportunity name ");

                    //Validate the value of Stage in Engagement details page
                    string engStage = engagementDetails.GetStage();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement",row, 1), engStage);
                    extentReports.CreateLog("Value of Stage field is : " + engStage + " for Job Type " + valJobType + " ");

                    //Validate the value of Record Type in Engagement details page
                    string engRecordType = engagementDetails.GetRecordType();
                    Console.WriteLine("engRecordType: " + engRecordType);
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 2), engRecordType);
                    extentReports.CreateLog("Value of Record type is : " + engRecordType + " for Job Type " + valJobType + " ");

                    //Validate the value of HL Entity in Engagement details page
                    string engHLEntity = engagementDetails.GetHLEntity();
                    Console.WriteLine("engHLEntity: " + engHLEntity);
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement",row, 3), engHLEntity);
                    extentReports.CreateLog("Value of HL Entity is : " + engHLEntity + " ");

                    //Validate the section in which Women led fiels is displayed
                    string lblWomenLed = engagementDetails.ValidateWomenLedField(valJobType);
                    Console.WriteLine(lblWomenLed);
                    Assert.AreEqual("Women Led", lblWomenLed);                    
                    string secWomenLed = engagementDetails.GetSectionNameOfWomenLedField(valJobType);
                    Console.WriteLine(secWomenLed);
                    if (valJobType.Contains("ESOP Corporate Finance")||valJobType.Contains("General Financial Advisory") || valJobType.Contains("Real Estate Brokerage") || valJobType.Contains("Special Committee Advisory") || valJobType.Contains("Strategic Alternatives Study") || valJobType.Contains("Take Over Defense") || valJobType.Equals("Activism Advisory") || valJobType.Equals("Strategy") || valJobType.Equals("Post Merger Integration") || valJobType.Equals("Valuation Advisory"))
                    {
                        Assert.AreEqual("Closing - Admin Details", secWomenLed); 
                    }
                    else
                    {
                        Assert.AreEqual("Closing - Document Checklist", secWomenLed);
                    }

                    extentReports.CreateLog(lblWomenLed +" field is displayed under section: " + secWomenLed +" ");

                    //Validate the value of Women Led in Engagement details page
                    string engWomenLed = engagementDetails.GetWomenLed();                   
                    Assert.AreEqual(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30), engWomenLed);
                    extentReports.CreateLog("Value of Women Led is : " + engWomenLed + " is same as selected in Opportunity page ");


                    ////Validate the ERP Last Integration Status on Engagement details page
                    //string ERPStatusIG = opportunityDetails.GetERPIntegrationStatus();
                    //Assert.AreEqual("Success", ERPStatusIG);
                    //extentReports.CreateLog("ERP Last Integration Status in ERP section: " + ERPStatusIG + " is displayed ");

                        string EngContactMember = engagementDetails.GetEngExternalContact();
                    string EngDealTeamMember = engagementDetails.GetEngDealTeamMember();
                    Assert.AreEqual(OppContactMember, EngContactMember, " Verify External Contact added on Opportunity page is mapped on Engagement page after conversion");
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
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }        
    }
}


