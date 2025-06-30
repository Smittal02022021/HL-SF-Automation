using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Threading;

namespace SF_Automation.TestCases.Engagement
{
    class TMTT0046981_VerificationOfEngCompanyRoundtripFlagFunctionalityOnTheSellsideDeals_SubjectIsAPotentialRoundTrip : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();

        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        LV_EngagementDetailsPage lvEngagementDetails = new LV_EngagementDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        LV_CompanyDetailsPage companyDetailsPage = new LV_CompanyDetailsPage();
        AddCounterparty addCounterparty = new AddCounterparty();

        public static string fileTMTT0046981 = "TMTT0046981_VerificationOfEngCompanyRoundtripFlagFunctionalityOnTheSellsideDeals_SubjectIsAPotentialRoundTrip";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerificationOfEngCompanyRoundtripFlagFunctionalityOnTheSellsideDeals_SubjectIsAPotentialRoundTrip()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0046981;
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                string userCAOExl = ReadExcelData.ReadData(excelPath, "Users", 2);
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                string iconDesc = ReadExcelData.ReadData(excelPath, "HoverIcon", 1);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                //Validate user logged in
                Assert.AreEqual(driver.Url.Contains("lightning"), true);
                extentReports.CreateStepLogs("Passed", "Admin User is able to login into SF");

                int rowCount = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");

                for(int row = 2; row <= rowCount; row++)
                {
                    //Select HL Banker app
                    try
                    {
                        lvHomePage.SelectAppLV("HL Banker");
                    }
                    catch(Exception)
                    {
                        lvHomePage.SelectAppLV1("HL Banker");
                    }

                    string valClientCompName = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 1);
                    string valSubjectCompName = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 2);
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 25);

                    //Search CF Financial user by global search
                    lvHomePage.SearchUserFromMainSearch(valUser);

                    //Verify searched user
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, valUser + " | Salesforce"), true);
                    extentReports.CreateLog("User " + valUser + " details are displayed ");

                    //Login as CF Financial user
                    lvHomePage.UserLogin();

                    //Switch to lightning view
                    if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                    {
                        homePage.SwitchToLightningView();
                        extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                    }

                    //Validate user logged in
                    Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(valUser));
                    extentReports.CreateStepLogs("Passed", "CF Financial User: " + valUser + " is able to login into lightning view. ");

                    //Navigate to Opportunities page
                    lvHomePage.NavigateToAnItemFromHLBankerDropdown("Opportunities");
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Recently Viewed | Opportunities | Salesforce"), true);
                    extentReports.CreateStepLogs("Passed", "User navigated to Opportunities list page. ");

                    //Validating Title of New Opportunity Page
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity page for selected LOB ");
                    extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                    //Create New Opportunity
                    string opportunityName = addOpportunity.AddOpportunitiesMutipleRows(valJobType, fileTMTT0046981, row);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0046981);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Info", "User is on Opportunity detail " + displayedTab + " tab ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityNumber);
                    extentReports.CreateStepLogs("Passed", "Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact
                    addOpportunityContact.CickAddCFOpportunityContact();
                    addOpportunityContact.CreateContactL2(fileTMTT0046981);
                    extentReports.CreateStepLogs("Info", valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForCFConversionMultipleRows(fileTMTT0046981, valJobType, row);
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");

                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0046981);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");

                    opportunityDetails.ClickReturnToOpportunityLV();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                    //TC - End
                    lvHomePage.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Info", "CF Financial User Logged Out from SF Lightning View. ");

                    //Select HL Banker app
                    try
                    {
                        lvHomePage.SelectAppLV("HL Banker");
                    }
                    catch(Exception)
                    {
                        lvHomePage.SelectAppLV1("HL Banker");
                    }

                    //Search for created opportunity
                    lvHomePage.SearchOpportunityFromMainSearch(opportunityName);
                    extentReports.CreateStepLogs("Info", "Admin User Search for Created Opportunity");

                    //update CC and NBC checkboxes in LV
                    opportunityDetails.UpdateOutcomeNBCApproveDetailsLV(valJobType);
                    extentReports.CreateStepLogs("Info", "CC and NBC checkboxes updated by Admin user. ");

                    //Requesting for engagement and validate the success message
                    opportunityDetails.ClickRequestToEngL();

                    //Submit Request To Engagement Conversion 
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");

                    //Search CAO Financial user by global search
                    lvHomePage.SearchUserFromMainSearch(userCAOExl);

                    //Verify searched user
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, userCAOExl + " | Salesforce"), true);
                    extentReports.CreateLog("User " + userCAOExl + " details are displayed ");

                    //Login as CAO user
                    lvHomePage.UserLogin();

                    //Switch to lightning view
                    if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                    {
                        homePage.SwitchToLightningView();
                        extentReports.CreateStepLogs("Passed", "CAO User: " + userCAOExl + " is able to login into lightning view. ");
                    }
                    else
                    {
                        extentReports.CreateStepLogs("Passed", "CAO User: " + userCAOExl + " is able to login into lightning view. ");
                    }

                    Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(userCAOExl));
                    extentReports.CreateStepLogs("Passed", "CAO User: " + userCAOExl + " is able to login into lightning view. ");

                    //Search for created opportunity
                    extentReports.CreateStepLogs("Info", " CAO User Search for Created Opportunity");
                    lvHomePage.SearchOpportunityFromMainSearch(opportunityName);

                    //Approve the Opportunity 
                    string status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Passed", "Opportunity " + status + " ");
                    opportunityDetails.CloseApprovalHistoryTabL();

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEngagementL2();
                    extentReports.CreateStepLogs("Info", "Opportunity Converted into Engagement ");

                    //Validate the Engagement name in Engagement details page
                    string engagementNumber = lvEngagementDetails.GetEngagementNumberL();
                    string engagementName = lvEngagementDetails.GetEngagementNameL();

                    //Need to get Name of Opp and Eng
                    Assert.AreEqual(opportunityName, engagementName);
                    extentReports.CreateStepLogs("Passed", "Name of Engagement : " + engagementName + " is Same as Opportunity name ");

                    //Close the engagement
                    lvEngagementDetails.ChangeEngagementStageToClosed();
                    lvEngagementDetails.CloseEstimatedRevenueDateReminderPopup();

                    string stage = lvEngagementDetails.GetEngagementStage();
                    Assert.AreEqual("Closed", stage);
                    extentReports.CreateStepLogs("Passed", "Stage of Engagement is change to : " + stage);

                    //TMTI0115206 - Verify that the "Engagement is a Potential Round Trip" picklist is added on the Engagement detail page and that it is "None" by default.
                    Assert.IsTrue(lvEngagementDetails.VerifyIfEngagementIsAPotentialRoundTripIsAddedAndItIsNoneByDefault());
                    extentReports.CreateStepLogs("Passed", "Engagement is a Potential Round Trip Picklist is added on Engagement detail page and it is None by default. ");

                    //TMTI0115210 - Verify the following picklist values are added to the field "Engagement is a Potential Round Trip": - a) Subject is a potential round trip b) Buyer is a potential round trip c) Neither subject nor buyer are round trip
                    Assert.IsTrue(lvEngagementDetails.VerifyPotentialRoundTripPicklistValues(fileTMTT0046981));
                    extentReports.CreateStepLogs("Passed", "Picklist values displayed under Engagement is a Potential Round Trip field are: Subject is a potential round trip, Buyer is a potential round trip, Neither subject nor buyer are round trip. ");

                    //TMTI0115212 - Verify that a hover icon displays the expected description for the Engagement is a Potential Round Trip field
                    Assert.IsTrue(lvEngagementDetails.VerifyHoverIconDescriptionForEngagementIsAPotentialRoundTripField(iconDesc));
                    extentReports.CreateStepLogs("Passed", "Hover icon displays the expected description for the Engagement is a Potential Round Trip field: " + iconDesc);

                    //TMTI0115214 - Verify that the a validation appears if 'COMPANIES CLOSED WITH' is missing and they attempt to select a value for either "Engagement is a Potential Round Trip"
                    Assert.IsTrue(lvEngagementDetails.VerifyIfCompaniesClosedWithIsMissing());
                    extentReports.CreateStepLogs("Passed", "Companies Closed With is missing for the engagement.");

                    //Verify warning message should be displayed when user selects a value in round trip
                    lvEngagementDetails.SelectValueInPotentialRoundTripField("Subject is a potential round trip");
                    Assert.IsTrue(lvEngagementDetails.VerifyWarningMsgIsDisplayedUponMissingCompaniesClosedWith());
                    extentReports.CreateStepLogs("Passed", "Warning Message is Displayed when user selects a value under Engagement is a Potential Round Trip field when 'COMPANIES CLOSED WITH' is missing.");

                    string msg = ReadExcelData.ReadData(excelPath, "Warning", 1);
                    Assert.IsTrue(lvEngagementDetails.VerifyWarningMsgUponMissingCompaniesClosedWith(msg));
                    extentReports.CreateStepLogs("Passed", "Expected warning message is displayed : " + msg);

                    //******************Start - Add Company Closed with****************************************

                    //Click on View Counterparties button & navigate to View Counterparties page
                    lvEngagementDetails.ClickViewCounterpartiesButton();
                    Assert.IsTrue(lvEngagementDetails.VerifyViewCounterpartiesPageIsDisplayed());
                    extentReports.CreateStepLogs("Passed", "View Counterparties page is displayed. ");

                    //Click Add Counterparties and validate the page
                    addCounterparty.ClickAddCounterpartiesBtn();
                    Assert.IsTrue(addCounterparty.VerifyAddCounterpartiesPageIsDisplayed());
                    extentReports.CreateLog("Add Counterparties page is displayed. ");

                    //Add Counterparty record and validate the success message
                    string counterpartyCompanyNameExl = ReadExcelData.ReadData(excelPath, "AddCounterparty", 1);
                    string counterpartyTypeExl = ReadExcelData.ReadData(excelPath, "AddCounterparty", 2);
                    addCounterparty.AddNewCounterparty(counterpartyCompanyNameExl, counterpartyTypeExl);

                    string popupMessage = addCounterparty.GetLVMessagePopup();
                    Assert.IsTrue(popupMessage.Contains(counterpartyCompanyNameExl), "Verify the Added Counterparty name is displayed in Popup message ");
                    extentReports.CreateLog(popupMessage + " message Displayed and company " + counterpartyCompanyNameExl + " is added in counterparty list ");

                    addCounterparty.CloseTab("EC - ");
                    addCounterparty.CloseTab("Counterparty Editor");

                    //Click on View Counterparties button & navigate to View Counterparties page
                    lvEngagementDetails.ClickViewCounterpartiesButton();
                    Assert.IsTrue(lvEngagementDetails.VerifyViewCounterpartiesPageIsDisplayed());
                    extentReports.CreateStepLogs("Passed", "View Counterparties page is displayed. ");

                    //Click Edit Bids and validate the page
                    string roundName = ReadExcelData.ReadData(excelPath, "Bid", 1);
                    addCounterparty.ClickEditBidsButtonAndNavigateToBidRoundsPage(roundName);

                    //Add all bid details and validate it
                    string value = ReadExcelData.ReadData(excelPath, "Bid", 2);
                    string minBid = addCounterparty.SaveBidValues(value);
                    Assert.AreEqual(value, minBid);
                    extentReports.CreateLog("Bid with Min Bid: " + minBid + " is displayed upon saving bid details ");

                    //Validate value of Max bid value
                    string maxBid = addCounterparty.GetMaxBid();
                    Assert.AreEqual(value, maxBid);
                    extentReports.CreateLog("Bid with Max Bid: " + maxBid + " is displayed upon saving bid details ");

                    addCounterparty.CloseTab("Edit Bids");
                    addCounterparty.CloseTab("Counterparty Editor");

                    CustomFunctions.PageReload(driver);
                    Thread.Sleep(10000);

                    //Verify if Companies Closed With is added Successfully
                    Assert.IsTrue(lvEngagementDetails.VerifyIfCompaniesClosedWithIsPresent());
                    extentReports.CreateStepLogs("Passed", "Companies Closed With is present for the engagement.");

                    //******************End  - Add Company Closed with****************************************

                    //Get Subject Company Type
                    string compType = lvEngagementDetails.GetSubjectCompanyType(valSubjectCompName);
                    //string counterpartyCompOwnership = lvEngagementDetails.GetCounterpartyCompanyOwnership(valSubjectCompName);

                    //Check Subject Company Type 
                    if (compType == "Operating Company")
                    {
                        
                    }

                    //TC - End
                    lvHomePage.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "CF Financial User Logged Out from SF Lightning View. ");
                }
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.Quit();
            }
        }
    }
}
