using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;

namespace SF_Automation.TestCases.Engagement
{
    class TMTT0042714_VerificationOfBuyersListFeatureOnEngagementScreen : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();

        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        LV_EngagementDetailsPage lvEngagementDetails = new LV_EngagementDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();

        Outlook outlook = new Outlook();

        public static string fileTMTT0042714 = "TMTT0042714_VerificationOfBuyersListFeatureOnEngagementScreen";
        public static string fileOutlook = "Outlook";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyActivityIsLinkedToTheRelatedEngagement()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0042714;
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                string FSCOUser = ReadExcelData.ReadData(excelPath, "Users", 2);
                string userCAOExl = ReadExcelData.ReadData(excelPath, "Users", 3);

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

                //Select HL Banker app
                try
                {
                    lvHomePage.SelectAppLV("HL Banker");
                }
                catch(Exception)
                {
                    lvHomePage.SelectAppLV1("HL Banker");
                }

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

                //Select HL Banker app
                try
                {
                    lvHomePage.SelectAppLV("HL Banker");
                }
                catch(Exception)
                {
                    lvHomePage.SelectAppLV1("HL Banker");
                }

                //Navigate to Opportunities page
                lvHomePage.NavigateToAnItemFromHLBankerDropdown("Opportunities");
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Recently Viewed | Opportunities | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", "User navigated to Opportunities list page. ");

                string valJobType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 3);
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);

                //Validating Title of New Opportunity Page
                string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                //Create New Opportunity
                string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTMTT0042714);
                extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0042714);
                Assert.AreEqual(displayedTab, "Info");
                extentReports.CreateStepLogs("Info", "User is on Opportunity detail " + displayedTab + " tab ");

                //Validating Opportunity details  
                string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                Assert.IsNotNull(opportunityNumber);
                extentReports.CreateStepLogs("Passed", "Opportunity with number : " + opportunityNumber + " is created ");

                //TC - TMTI0104944 - Verify that CF User request Buyers list on Opportunity screen
                Assert.IsTrue(opportunityDetails.VerifyRequestBuyersListButtonOnOpportunityDetailPage());
                extentReports.CreateStepLogs("Passed", "Request Buyers List button is displayed on opportunity screen. ");

                opportunityDetails.ClickRequestBuyersListButton();
                extentReports.CreateStepLogs("Info", "Request Buyers List button is clicked. ");

                opportunityDetails.FillRequestBuyersListDetails(fileTMTT0042714);
                extentReports.CreateStepLogs("Info", "Request Buyers List Details filled successfully. ");

                Assert.IsTrue(opportunityDetails.VerifyBuyersListTabIsDisplayed());
                extentReports.CreateStepLogs("Passed", "Buyers List tab is displayed. ");

                opportunityDetails.ClickBuyersListTab();
                string buyerRequestCaseID = opportunityDetails.GetParentRequestID();
                extentReports.CreateStepLogs("Passed", "Buyer Request Case Number: " + buyerRequestCaseID + " is generated. ");

                string getCaseTitle = opportunityDetails.GetCaseTitle();
                extentReports.CreateStepLogs("Info", "Case title: " + getCaseTitle + " is displayed on case details page. ");

                driver.Quit();

                /*
                //TC - TMTI0104949 - Verify that an email sent to requestor and FSCO user of related region once parent case is created.

                //Launch outlook window
                OutLookInitialize();

                //Login into Outlook
                outlook.LoginOutlook(fileOutlook);
                string outlookLabel = outlook.GetLabelOfOutlook();
                Assert.AreEqual("Outlook", outlookLabel);
                extentReports.CreateStepLogs("Passed", "User is logged in to outlook ");

                string region = ReadExcelData.ReadData(excelPath, "RequestBuyersList", 2);

                Assert.IsTrue(outlook.VerifyBuyersListGenerationEmailIsRecievedWithSubmitter(buyerRequestCaseID));
                extentReports.CreateStepLogs("Passed", "Email sent to submitted CF Financial user once parent case is created.");

                //Assert.IsTrue(outlook.VerifyBuyersListGenerationEmailIsRecievedWithFSCO(buyerRequestCaseID, region, getCaseTitle));
                //extentReports.CreateStepLogs("Passed", "Email sent to FSCO user related region once parent case is created.");

                driver.Quit();
                */

                Initialize();

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
                extentReports.CreateLog("Admin User is able to login into SF");

                //Search FSCO user by global search
                lvHomePage.SearchUserFromMainSearch(FSCOUser);

                //Verify searched user
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, FSCOUser + " | Salesforce"), true);
                extentReports.CreateLog("User " + FSCOUser + " details are displayed ");

                //Login as FSCO user
                lvHomePage.UserLogin();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                //Validate user logged in
                Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(FSCOUser));
                extentReports.CreateStepLogs("Passed", "FSCO User: " + FSCOUser + " is able to login into lightning view. ");

                //Select HL Banker app
                try
                {
                    lvHomePage.SelectAppLV("HL Banker");
                }
                catch(Exception)
                {
                    lvHomePage.SelectAppLV1("HL Banker");
                }

                //TC - TMTI0104958 - Verify the Buyers list/Counterparty on Engagement screen once Opportunity converted

                //Search for created opportunity
                opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", " FSCO User Search for Created Opportunity");

                opportunityDetails.ClickBuyersListTab();
                string buyerRequestCaseID2 = opportunityDetails.GetParentRequestID();
                Assert.AreEqual(buyerRequestCaseID, buyerRequestCaseID2);
                extentReports.CreateStepLogs("Passed", "Buyer Request Case Number: " + buyerRequestCaseID2 + " is displayed under Buyer List Tab of opportunity for FSCO user. ");

                string getCaseTitle2 = opportunityDetails.GetCaseTitle();
                Assert.AreEqual(getCaseTitle, getCaseTitle2);
                extentReports.CreateStepLogs("Info", "Case title: " + getCaseTitle2 + " is displayed on case details page for FSCO user. ");

                //Verify create new counterparty list button
                Assert.IsTrue(opportunityDetails.VerifyCreateNewCounterpartyListButtonOnOpportunityBuyerListPage());
                extentReports.CreateStepLogs("Passed", "Create new counterparty List button is displayed on buyer list page for FSCO user. ");

                opportunityDetails.ClickCreateNewCounterpartyListButton();
                extentReports.CreateStepLogs("Info", "Create new counterparty List button is clicked. ");

                //Select Report
                Assert.IsTrue(opportunityDetails.VerifyReportSelectionPopupIsDisplayed());
                extentReports.CreateStepLogs("Passed", "Report Selection popup is displayed. ");

                string firstNameFirstLetter = FSCOUser.Substring(0, 1);
                string[] lastName = FSCOUser.Split(' ');
                string reportName = firstNameFirstLetter + lastName[1];
                opportunityDetails.SelectReport(reportName);
                extentReports.CreateStepLogs("Info", "Report: " + reportName + " is selected. ");

                //Select company from the list
                string companyName = ReadExcelData.ReadData(excelPath, "RequestBuyersList", 4);
                opportunityDetails.SelectCompanyFromTheList(companyName);
                extentReports.CreateStepLogs("Info", "Company is selected from the list. ");

                Assert.IsTrue(opportunityDetails.VerifyCompanyListIsCreatedOnCaseByFSCOUser());
                extentReports.CreateStepLogs("Passed", "Company list is created on case by FSCO user. ");

                string compListName = opportunityDetails.GetCompanyListName();
                extentReports.CreateStepLogs("Info", "Company List Name: " + compListName + " is displayed on company list page. ");

                //Logout FSCO User
                lvHomePage.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", "FSCO User: " + FSCOUser + " is Logged Out from SF Lightning View. ");

                //Select HL Banker app
                try
                {
                    lvHomePage.SelectAppLV("HL Banker");
                }
                catch(Exception)
                {
                    lvHomePage.SelectAppLV1("HL Banker");
                }

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
                extentReports.CreateStepLogs("Passed", "CF Financial User: " + valUser + " is logged in again.");

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
                opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                extentReports.CreateStepLogs("Info", "FSCO User Search for Created Opportunity");

                opportunityDetails.ClickBuyersListTab();
                extentReports.CreateStepLogs("Info", "Buyers list tab is clicked");

                Assert.IsTrue(opportunityDetails.VerifyBuyerListRequestIsGeneratedAndDisplayed(getCaseTitle2));
                extentReports.CreateStepLogs("Passed", "Buyer list request is generated and displayed on buyer list page for CF Financial User. ");

                //Create External Primary Contact
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                addOpportunityContact.CickAddCFOpportunityContact();
                addOpportunityContact.CreateContactL2(fileTMTT0042714);
                extentReports.CreateStepLogs("Info", valContactType + " Opportunity contact is saved ");

                //Update required Opportunity fields for conversion and Internal team details
                opportunityDetails.UpdateReqFieldsForCFConversionLV2(fileTMTT0042714, valJobType);
                extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");

                opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0042714);
                extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");

                opportunityDetails.ClickReturnToOpportunityLV();
                extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                //update CC and NBC checkboxes in LV
                opportunityDetails.UpdateOutcomeNBCApproveDetailsLV(valJobType);
                extentReports.CreateStepLogs("Info", "CC and NBC checkboxes updated. ");

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
                opportunityHome.SearchOpportunitiesInLightningView(opportunityName);

                //Approve the Opportunity 
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


                //TC - End
                lvHomePage.UserLogoutFromSFLightningView();
                extentReports.CreateStepLogs("Info", "CF Financial User Logged Out from SF Lightning View. ");

                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
            }
        }
    }
}
