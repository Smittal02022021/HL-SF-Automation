using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SF_Automation.TestCases.Opportunities
{
    class TMTT0042712_VerificationOfBuyersListFeatureOnOpportunityScreen : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();

        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        Outlook outlook = new Outlook();

        public static string fileTMTT0042712 = "TMTT0042712_VerificationOfBuyersListFeatureOnOpportunityScreen";
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
        public void VerifyActivityIsLinkedToTheRelatedOpportunity()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0042712;
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                string FSCOUser = ReadExcelData.ReadData(excelPath, "Users", 2);

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
                string opportunityName = addOpportunity.AddOpportunitiesLightningV2(valJobType, fileTMTT0042712);
                extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0042712);
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

                opportunityDetails.FillRequestBuyersListDetails(fileTMTT0042712);
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

                //TC - TMTI0104951 - Verify that FSCO user of related region can access the case and add company list on the case

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

                //Add Company List Members
                opportunityDetails.ClickAddCompanyListMembersNewButton();
                extentReports.CreateStepLogs("Info", "Add Company List Members new button is clicked. ");








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
