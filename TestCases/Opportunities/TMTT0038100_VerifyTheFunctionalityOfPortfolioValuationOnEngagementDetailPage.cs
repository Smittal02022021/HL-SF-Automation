
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
using System.Collections.Generic;

namespace SF_Automation.TestCases.Opportunities
{
    class TMTT0038100_VerifyTheFunctionalityOfPortfolioValuationOnEngagementDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        OppValuationPeriods period = new OppValuationPeriods();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engDetails = new EngagementDetailsPage();
        AddOppCounterparty counterparty = new AddOppCounterparty();
        AddOpportunityContact addContact = new AddOpportunityContact();
        ValuationPeriods engValPeriod = new ValuationPeriods();

        public static string fileTC1644 = "TMTT0038098_VerifyTheFunctionalityOfPortfolioValuationOnOpportunityDetailPage.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void FunctionalityOfPortfolioValuation()
        {
            try
            {
                //Get path of Test data file (need to add Karan in PV Supervisor)
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1644;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user               
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Verify the availability of Opportunity under HL Banker list
                string tagOpp = opportunityHome.ClickOppUnderHLBanker();
                Assert.AreEqual("Opportunities", tagOpp);
                extentReports.CreateLog(tagOpp + " is displayed under HL Banker dropdown ");

                //Verify that choose LOB is displayed after clicking New button
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                string titleOpp = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                Assert.AreEqual("New Opportunity: " + valRecordType, titleOpp);
                extentReports.CreateLog("Page with title: " + titleOpp + " is displayed upon clicking next button ");

                //Add FVA Opportunity
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunitiesLightning(valJobType, fileTC1644);
                extentReports.CreateLog("Opportunity with name: " + value + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileTC1644);
                Assert.AreEqual("Info", displayedTab);
                extentReports.CreateLog("Tab with name: " + displayedTab + " is displayed upon saving internal deal team members details ");

                //Update all required fields for Conversion to Engagement and add Portfolio Valuation and Period details
                //counterparty.ClickViewCounterparties();
                opportunityDetails.UpdateReqFieldsForFVAConversionLForPV(fileTC1644);
                extentReports.CreateLog("All required details are saved ");
                opportunityDetails.ClickAddFVAOppContact();
                addContact.CreateContactL(fileTC1644);

                period.ClickOppValuationAndValidateFields();
                string name = CustomFunctions.RandomValue();
                string addedValuation = period.EnterAndSaveOppValuationPeriodDetailsL(name);
                Assert.AreEqual(name, addedValuation);
                extentReports.CreateLog("Added valuation: " + addedValuation + " is displayed upon clicking Save button on Opportunity Valuation Period edit page after entering all mandatory details ");

                period.ClickNewPeriodPositionButtonWithoutFrameL();
                string addedPeriod1st = period.EnterAndSave1stOppValuationPeriodPositionDetailsLWithDiffFrame("Techno Aide");

                //period.ClickNewPeriodPositionButtonWithoutFrameL();
                //string addedPeriod3rd = period.EnterAndSaveOppValuationPeriodPositionDetailsLWithDiffFrame("Walker Edison");

                period.ClickBackToOppValPeriodList();
                string name2 = CustomFunctions.RandomValue();
                string addedValuation3rd = period.EnterAndSaveOppValuationPeriodDetailsL(name2);

                period.ClickNewPeriodPositionButtonWithoutFrameL();
                string addedPeriod2nd = period.EnterAndSaveOppValuationPeriodPositionDetailsLWithDiffFrame("Techno Alpha");
                                
                //Logout
                usersLogin.LightningLogout();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Search for Opportunity
                opportunityHome.SearchOpportunity(value);
                opportunityDetails.UpdateInternalTeamDetails(fileTC1644);
                extentReports.CreateLog("Internal Team members details are saved ");
                opportunityDetails.UpdateOutcomeDetails(fileTC1644);

                //Login as Financial User and validate the user                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUserLightning();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Open the same opportunity and Click on Request Engagement           
                opportunityHome.SearchMyOpportunitiesInLightning(value, valUser);
                opportunityDetails.ClickRequestoEngL();

                //Login as CAO user and Validate the status of Opportunity post Request Engagement
                usersLogin.DiffLightningLogout();
                string valCAOUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);
                usersLogin.SearchUserAndLogin(valCAOUser);
                string caoUser = login.ValidateUserLightningCAO3rd();
                Console.WriteLine("valCAOUser:" + valCAOUser.Substring(0, 12));
                Assert.AreEqual(caoUser.Contains(valCAOUser.Substring(1, 10)), true);
                extentReports.CreateLog("User: " + valCAOUser + " logged in ");

                //Search for created opportunity and validate the status
                opportunityHome.ClickOppUnderHLBankerCAO();
                opportunityHome.SearchMyOpportunitiesInLightning(value, valCAOUser);
                string status = opportunityDetails.ClickApproveButtonLV2();
                Assert.AreEqual("Approved", status);
                extentReports.CreateLog("Opportunity is approved ");

                //Convert the Opportunity to Engagement by clicking Convert To Engagement and log out of CAO user
                opportunityDetails.ClickOppName();
                string engagement = opportunityDetails.ClickReqToEngagementPV();
                Assert.AreEqual("Engagement", engagement);
                extentReports.CreateLog("Opportunity is converted to Engagement after clicking Request To Engagement button ");

                usersLogin.DiffLightningLogout();
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser2 = login.ValidateUserLightning();
                Assert.AreEqual(stdUser2.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser2 + " logged in ");

                engHome.SelectEngUnderHLBanker();
                engHome.SearchEngagementWithNumberOnLightning(value, valJobType);               

                //1.  TMTI0092391_Verify that the "Portfolio Valuation" button is available on the portfolio Engagement
                string portfolioValuation = opportunityDetails.ValidatePortfolioValuationButton();
                string jobTypePV = engDetails.GetJobTypeL();
                Assert.AreEqual("Portfolio Valuation button is displayed", portfolioValuation);
                extentReports.CreateLog("Portfolio Valuation Button is displayed for the Engagement with Job type: " + jobTypePV + " ");

                //2.  TMTI0092393_Verify that clicking the "Portfolio Valuation" button opens up a new tab with the added list of valuations on the screen
                engDetails.ClickPortfolioValuationL();                

                string backToEng = engDetails.ValidateReturnToEngButton();
                Assert.AreEqual("Back To Engagement", backToEng);
                extentReports.CreateLog("Button : " + backToEng + " is displayed ");

                string newEngVal = engDetails.ValidateNewEngValuationPeriodButton();
                Assert.AreEqual("New Engagement Valuation Period", newEngVal);
                extentReports.CreateLog("Button : " + newEngVal + " is displayed ");

                //3.  TMTI0092395_Verify that clicking "Back to Engagement" takes the user back to the Engagement detail page
                string details = engDetails.ValidateEngDetailsPageUponClickOfBackToEngButton();
                Assert.AreEqual("Details", details);
                extentReports.CreateLog("Engagement details page is displayed upon clicking Back To Engagement button ");

                //4.  TMTI0092397_Verify Portfolio Valuation and all the associated details that are imported from Opportunity on conversion
                engDetails.ClickPortfolioValuationL();
                string importedValPeriod = engDetails.ValidateImportedValPeriod();               
                //Assert.AreEqual(addedValuation, importedValPeriod);
                extentReports.CreateLog("Valuation Period: " +importedValPeriod+" added in Opportunity is imported to Engagement post conversion successfully ");

                string importedValPosition = engDetails.ValidateImportedPeriodPosition(name2);
                Assert.AreEqual(addedPeriod2nd, importedValPosition);
                extentReports.CreateLog("Added Period position in Opportunity is imported to Engagement post conversion successfully ");

                //5.   TMTI0092399_Verify that the FVA User can edit the Engagement Valuation Period
                //6.   TMTI0093015_Verify that the validation message appears for required fields on clicking the Save button of the Engagement Valuation Period Edit page
                string messageEngPeriod = engValPeriod.ValidateMandatoryMessageOfValuationPeriod();
                Assert.AreEqual(addedPeriod2nd, importedValPosition);
                Assert.AreEqual("Error:\r\nClient Final Deadline: You must enter a value", messageEngPeriod);
                extentReports.CreateLog("FVA user can edit imported Valuation Period and mandatory validation: " + messageEngPeriod + " is displayed when all mandatory fields are not filled ");

                //7.    TMTI0093017-Verify that clicking the Cancel button of the Engagement Valuation Period Edit page takes the user back to the Valuation Periods detail page
                string engValPeriodUponCancel = engValPeriod.ValidateEngValDetailsPageUponClickingCancelButton();
                Assert.AreEqual("Engagement Valuation Period", engValPeriodUponCancel);
                extentReports.CreateLog("Page " + engValPeriodUponCancel + " is displayed upon clicking cancel button on Engagement Valuation Period edit page ");

                //8.  TMTI0093019_Verify that the user can update the required fields on the Engagement Valuation Period Edit page and can save the valuation period
                string updatedClientFinal = engValPeriod.EditFunctionalityOfValuationPeriod();
                Assert.NotNull(updatedClientFinal);
                extentReports.CreateLog("Updated value of Client Final Deadline : " + updatedClientFinal + " is displayed upon clicking save button on Engagement Valuation Period detail page ");

                //9.  TMTI0093021_Verify that on selecting "Client's Final Deadline" on the Engagement Valuation Period Detail page, the Engagement Valuation Period Allocation record will be created
                string periodAllocation = engValPeriod.ValidatePeriodAllocationRecordAfterUpdatingClientFinalDeadline();
                Assert.AreEqual("True", periodAllocation);
                extentReports.CreateLog("Period Allocation record is created after saving Client Final Deadline on Engagement Valuation Period Edit page ");

                //10.  TMTI0093023_ Verify that clicking the "Import Positions" button opens up a screen that shows the "Existing Valuation Period" listing with the following buttons
                Assert.IsTrue(engValPeriod.ValidateButtonsOnImportPositions(), "Verified that displayed buttons are same");
                extentReports.CreateLog("Displayed buttons are as expected after clicking Import Position button ");

                //11.  TMTI0093025_Verify that the list of related positions opens from the existing valuation period by clicking the "Search Valuation Period for Positions" button. 
                Assert.IsTrue(engValPeriod.ValidateDisplayedImportButtonsUponClickingSearchValPeriod(), "Verified that displayed Import Position buttons are same");
                extentReports.CreateLog("Displayed Import Position buttons are as expected ");

                Assert.IsTrue(period.ValidateDisplayedBottomButtonsUponClickingSearchValPeriod(), "Verified that displayed buttons at the bottom of Related Positions page are same");
                extentReports.CreateLog("Displayed buttons at the bottom of Related Positions page are as expected ");

                //12.  TMTI0093027_Verify that if the "Automation Tool Usage" field is blank in the position that the user selected to Import, the application gives an error message on the screen. 
                string messageTool = engValPeriod.ValidateAutomationToolMandatoryFieldMessage();
                Assert.AreEqual("Please update Automation Tool usage information for selected position(s).", messageTool);
                extentReports.CreateLog("Message: "+ messageTool + " is displayed when Automation Tool Usage field is blank in the position that the user selected to Import ");

                //13.  TMTI0093029_Verify that the user can update "Automation Team Utilized" by clicking "Update Automation Tool Usage". 
                Assert.IsTrue(engValPeriod.ValidateColumnsOnAutomationToolPage(), "Verified that displayed columns are same");
                extentReports.CreateLog("Displayed columns for Automation tool are as expected ");

                string pageRelated = engValPeriod.ValidatePositionsListPageUponClickingSaveButtonOnAutomationToolPage();
                Assert.AreEqual("Related Positions", pageRelated);
                extentReports.CreateLog("Page: " + pageRelated + " is displayed upon clicking Save button on Automation Tool page after filling all the details ");

                //14.  TMTI0093031_Verify that the user can Import Positions with Team Members from the existing valuation period and all the details are imported successfully
                string importedPosition = engValPeriod.ValidateImportWithTeamMember();
                Assert.AreEqual(addedPeriod1st, importedPosition);
                extentReports.CreateLog("Position with name: " + importedPosition + " is displayed after importing position with team member  from the existing valuation period ");

                //15.  TMTI0093033_Verify that the user can Import Positions without a Team Member from the existing valuation period and all the details are imported successfully except the Team Member
                engDetails.ValidateImportedPeriodPosition(name);
                engValPeriod.ValidateButtonsOnImportPositions();

                string importedPositionWithoutTeam = engValPeriod.ValidateImportWithoutTeamMember();
                Assert.AreEqual(addedPeriod2nd, importedPositionWithoutTeam);
                extentReports.CreateLog("Position with name: " + importedPositionWithoutTeam + " is displayed after importing position without team member  from the existing valuation period ");

                //16.  TMTI0093035_Verify that clicking "New Engagement Valuation Period" opens up the Engagement Valuation Period Edit page with has required fields with Save and Cancel buttons
                string pageEngValPeriodEdit = engValPeriod.ValidatePageAfterClickingNewEngValPeriodButton();
                Assert.AreEqual("Engagement Valuation Period Edit", pageEngValPeriodEdit);
                extentReports.CreateLog("Page with title: " + pageEngValPeriodEdit + " is displayed after clicking New Engagement Valuation Period button ");

                Assert.IsTrue(engValPeriod.ValidateMandatoryFieldsOnEngValPeriodEdit(), "Verified that displayed mandatory fields are same");
                extentReports.CreateLog("Displayed mandatory fields on Engagement Valuation Period Edit page are as expected ");

                Assert.IsTrue(engValPeriod.ValidateButtonsOnEngValPeriodEdit(), "Verified that displayed buttons are same");
                extentReports.CreateLog("Displayed buttons on Engagement Valuation Period Edit page are as expected ");

                //17.  TMTI0093037_Verify that the validation message appears for required fields on clicking the Save button of the Engagement Valuation Period Edit page
                Assert.IsTrue(engValPeriod.ValidateMandatoryValidationsUponClickingSaveOnEngValPeriodEdit(), "Verified that displayed mandatory validations are same");
                extentReports.CreateLog("Displayed mandatory validations on Engagement Valuation Period Edit page are as expected upon clicking Save button without entering mandatory fields ");

                //18.  TMTI0093039_Verify that clicking the Cancel button of the Engagement Valuation Period edit page takes the user back to the Valuation Periods Listing View
                string pageValPeriods = engValPeriod.ValidatePageAfterClickingCancelButtonOnEngValPeriodEditPage();
                Assert.AreEqual("New Engagement Valuation Period", pageValPeriods);
                extentReports.CreateLog("Page with title: " + pageValPeriods + " is displayed after clicking cancel on Engagement Valuation Period Edit page ");

                //19.  TMTI0093041_ Verify that the "New Engagement Valuation Period" is created by clicking the Save button on the Engagement Valuation Period Edit page and redirecting the user to the Valuation Period detail page
                string engName = CustomFunctions.RandomValue();
                string addedEngValuation = engValPeriod.EnterAndSaveEngValuationPeriodDetailsL(engName);
                Assert.AreEqual(engName, addedEngValuation);
                extentReports.CreateLog("Added valuation: " + addedEngValuation + " is displayed upon clicking Save button on Engagement Valuation Period Detail page after entering all mandatory details ");

                //20. TMTI0093043_Verify that a validation message appears for required fields on clicking the Save button of the "New Eng Valuation Period Position" button
                Assert.IsTrue(engValPeriod.ValidateMessageWhileClickingSaveButtonOnPeriodPosition(), "Verified that displayed mandatory field validations are same");
                extentReports.CreateLog("Displayed mandatory field validations on Engagement Valuation Period Position are as expected ");

                //21.  TMTI0093045_Verify that the user can add positions by clicking the "New Eng Valuation Period Position" button with all the required details
                string addedPosition = engValPeriod.EnterAndSaveEngValuationPeriodPositionDetailsL("BE Networks");
                Assert.AreEqual("BE Networks", addedPosition);
                extentReports.CreateLog("Position: " + addedPosition + " is displayed upon clicking Save button after entering all mandatory details of Period Position ");

                //22.   TMTI0093047_ Verify that clicking the "Edit" button given on "Eng Valuation Period Position" allows the user to update Eng Valuation Period position details and updates get reflected on the position
                string updatedPosition = engValPeriod.EditFunctionalityOfPeriodPosition("XYZ");
                Assert.AreEqual("XYZ", updatedPosition);
                extentReports.CreateLog("Updated Position name is displayed on Valuation Position Detail page after updating the name ");

                //23.  TMTI0093049_Verify that the "Eng Valuation Period Team Members" section is available at the bottom of the Eng Valuation Period Position detail page
                string secTeamMember = engValPeriod.ValidateSecEngValTeamMember();
                Assert.AreEqual("Eng Valuation Period Team Members", secTeamMember);
                extentReports.CreateLog("Section: " + secTeamMember + " is displayed at the bottom of the Eng Valuation Period Position detail page ");

                string buttonTeamMember = engValPeriod.ValidateAddNewTeamMemberButton();
                Assert.AreEqual("Add New Team Member", buttonTeamMember);
                extentReports.CreateLog("Button with name: " + buttonTeamMember + " is displayed at the bottom of the Eng Valuation Period Position detail page ");

                //24.  TMTI0093051_Verify that on clicking "Add New Team Member" opens up fields like a staff member and with Save Team Member and Delete action buttons
                Assert.IsTrue(engValPeriod.ValidateTeamMemberColumns(), "Verified that displayed Team members table columns are same");
                extentReports.CreateLog("Displayed Team members table columns on Engagement Valuation Period Position are as expected ");

                string saveTeam = engValPeriod.ValidateSaveTeamMemberButton();
                Assert.AreEqual("Save Team Members", saveTeam);
                extentReports.CreateLog("Button with name: " + saveTeam + " is displayed in " + secTeamMember + " ");

                string deleteTeam = engValPeriod.ValidateDeleteLinkTeamMember();
                Assert.AreEqual("Delete", deleteTeam);
                extentReports.CreateLog("Link with name: " + deleteTeam + " is displayed in " + secTeamMember + " ");

                //25.  TMTI0093053_Verify that clicking "Save Team Member" adds the user as a Team Member on the selected role with Status
                string addedStaff = period.SaveTeamMembersAndValidateEng();
                string addedRole = period.GetSavedRoleOfStaff();
                Assert.AreEqual("Associate", addedRole);
                Assert.AreEqual("Karan Chopra", addedStaff);
                extentReports.CreateLog("Staff with name: " + addedStaff + " and Role: " + addedRole + " is displayed after saving details ");

                //26-1st part. TMTI0093055 _Verify that the user can "Void Position" from the Engagement Valuation Position Detail page.
                string confirmMessage = engValPeriod.ValidateConfirmationMessageoAfterClickingVoidPositionOnPeriodPosition();
                Assert.AreEqual("Are you sure you want to cancel this position? This process will reverse any accruals from this position.", confirmMessage);
                extentReports.CreateLog("Confirmation Message: " + confirmMessage + " is displayed after clicking Void Position button ");

                Assert.IsTrue(engValPeriod.ValidateVoidPositionButtons(), "Verified that displayed buttons after clicking Void Position button are same");
                extentReports.CreateLog("Displayed buttons after clicking Void Position button are as expected ");

                string valPosition = engValPeriod.ValidateVoidPositionByClickingNo();
                Assert.AreEqual("In Progress", valPosition);
                extentReports.CreateLog("Period Position's status: " + valPosition + " and is not cancelled after clicking No on confirmation page ");

                //27- 1st part. TMTI0093057_Verify the functionality of the "Update Automation Tool" button given on the Engagement Valuation Period Detail page 
                string cancelTool = engValPeriod.ValidateCancelFunctionalityOfUpdateAutomationToolUsage();
                Assert.AreEqual("Engagement Valuation Period Detail", cancelTool);
                extentReports.CreateLog("Page with title: " + cancelTool + " is displayed after clicking cancel on Update Automation Tool Usage page ");

                string AcceptTool = engValPeriod.ValidateAcceptFunctionalityOfUpdateAutomationToolUsage();
                Assert.AreEqual("Engagement Valuation Period Detail", cancelTool);
                extentReports.CreateLog("Page with title: " + cancelTool + " is displayed after updating and clicking Save on Update Automation Tool Usage page ");

                //26-2nd Part
                string statusPosition = engValPeriod.ValidateVoidPositionByClickingYes();
                Assert.AreEqual("Cancelled", statusPosition);
                extentReports.CreateLog("Period Position: " + statusPosition + " is cancelled after clicking Yes on confirmation page ");

                //28. TMTI0093794_Verify the functionality of the "New Eng Valuation Period Allocation" button given in the Eng Valuation Period Allocation section
                string titleAllocation = engValPeriod.ValidateNewEngValPeriodAllocation();
                Assert.AreEqual("New Eng Valuation Period Allocation", titleAllocation);
                extentReports.CreateLog("Page with title: " + titleAllocation + " is displayed after clicking New Eng Valuation Period Allocation button ");
                
                string cancelAllocation = engValPeriod.ValidateCancelFunctionalityOfEngValPeriodAllocation();
                Assert.AreEqual("Engagement Valuation Period Detail", cancelAllocation);
                extentReports.CreateLog("Page with title: " + cancelAllocation + " is displayed after clicking cancel on New Eng Valuation Period Allocation page ");

                string addedAllocation = engValPeriod.ValidateSaveFunctionalityOfValuationPeriodAllocation();
                Assert.AreEqual("True", addedAllocation);
                extentReports.CreateLog("Record is added after clicking Save on Engagement Valuation Period Allocation on Engagement Valuation Period Detail ");

                //29.  TMTI0099310_Verify that if the user selects the same dates of already existing records while adding or editing "New Eng Valuation Period Allocation", the application gives validation on clicking the Save button
                string duplicateAllocation = engValPeriod.ValidateDuplicateValidationOfValuationPeriodAllocation();
                Assert.AreEqual("Duplicate Record/s Exists", duplicateAllocation);
                extentReports.CreateLog("Message: " + duplicateAllocation + " is displayed while adding same dates of already added engagement valuation period allocation ");

                //31.  TMTI0099314_Verify that the user can edit the "Eng Valuation Period Allocations" by clicking the "Edit" button given with the individual records.Verify that the user can edit the "Eng Valuation Period Allocations" by clicking the "Edit" button given with the individual records.
                string updAllocation = engValPeriod.ValidateEditFunctionalityOfValuationPeriodAllocation();
                Assert.AreEqual("10", updAllocation);
                extentReports.CreateLog("Updated value: " + updAllocation + " is displayed for Analyst Allocation after updating it ");

                //30.  TMTI0099312_Verify the functionality of "Mass Edit" button given on the Eng Valuation Period Allocation section.
                Assert.IsTrue(engValPeriod.ValidateMassEditButtons(), "Verified that displayed buttons after clicking Masss Edit button are same");
                extentReports.CreateLog("Displayed buttons after clicking Mass Edit button are as expected ");

                string totalAllocation = engValPeriod.ValidateInLineEditFunctionalityOfPeriodAllocations();
                Assert.AreEqual("30%", totalAllocation);
                extentReports.CreateLog("Saved value of allocation is displayed ");

                string title = engValPeriod.ValidateBackToEngagementFunctionality();
                Assert.AreEqual("Engagement Valuation Period Detail", title);
                extentReports.CreateLog("Page with title: " + title + " is displayed when Back to Engagement Valuation Period button is clicked ");

                //32.  TMTI0099316_Verify the functionality of "Billing Request" given in the "Eng Valuation Period Positions" section
                Assert.IsTrue(engValPeriod.ValidateBillingRequestButtons(), "Verified that displayed radio buttons after clicking Billing Request button are same");
                extentReports.CreateLog("Displayed radio buttons after clicking Billing Request button are as expected ");

                string titleSendEmail = engValPeriod.ValidateSendEmailPageUponSavingTotalReportFee();
                Assert.AreEqual("Send Email", titleSendEmail);
                extentReports.CreateLog("Page : " +titleSendEmail + " is displayed upon saving Total Report Fee ");

                string pageSendEmail = engValPeriod.ValidateSendEmailFunctionality();
                Assert.AreEqual("Engagement Valuation Period Detail", pageSendEmail);
                extentReports.CreateLog("Page with title: " +pageSendEmail +" is displayed upon clicking Send Email button ");

                engValPeriod.ValidateBillingRequestButtons();
                string titleIndivSendEmail = engValPeriod.ValidateSendEmailPageUponSavingIndivReportFee();
                Assert.AreEqual("Send Email", titleIndivSendEmail);
                extentReports.CreateLog("Page : " + titleIndivSendEmail + " is displayed upon saving Individual Report Fee ");

                string pageIndivSendEmail = engValPeriod.ValidateSendEmailFunctionality();
                Assert.AreEqual("Engagement Valuation Period Detail", pageIndivSendEmail);
                extentReports.CreateLog("Page with title: " + pageIndivSendEmail + " is displayed upon clicking Send Email button ");

                //33.	TMTI0099318_Verify that the CAO can "Delete" the "Eng Valuation Period Allocation
                //Login as CAO user 
                engValPeriod.SwitchFrame();
                usersLogin.DiffLightningLogout();                
                usersLogin.SearchUserAndLogin(valCAOUser);               

                string caoUser1 = login.ValidateUserLightningCAO();
                Console.WriteLine("caoUser1:" + caoUser1);
                Console.WriteLine("valCAOUser:" + valCAOUser.Substring(0,10));
                Assert.AreEqual(caoUser1.Contains(valCAOUser.Substring(0, 10)), true);
                extentReports.CreateLog("User: " + valCAOUser + " logged in ");

                engHome.SelectEngUnderHLBanker();
                engHome.SearchEngagementWithNumberOnLightning(value, valJobType);
                engDetails.ClickPortfolioValuationL();

                string allocation = engValPeriod.GetAllocation(engName);

                string cancelDelete = engValPeriod.ValidateDeleteFunctionalityOfPeriodAllocationAfterSelectingNo();
                Assert.AreEqual(allocation, cancelDelete);
                extentReports.CreateLog("Eng Valuation Period Allocation is not deleted after clicking cancel on delete confirmation pop up ");

                string acceptDelete = engValPeriod.ValidateDeleteFunctionalityOfPeriodAllocationAfterAccepting();
                Assert.AreNotEqual(allocation, acceptDelete);
                extentReports.CreateLog("Eng Valuation Period Allocation is deleted after clicking OK on delete confirmation pop up ");

                //34.  TMTI0099321_Verify that the CAO is able to "Delete" the "Engagement Valuation Period Positions" 
                string cancelPeriodPosition = engValPeriod.ValidateDeleteFunctionalityOfPeriodPositionAfterSelectingNo();
                Assert.AreEqual("True", cancelPeriodPosition);
                extentReports.CreateLog("Eng Valuation Period Position is not deleted after clicking cancel on delete confirmation pop up ");
                engValPeriod.SwitchFrame();         
                
                usersLogin.DiffLightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }

        }
    }
}


 