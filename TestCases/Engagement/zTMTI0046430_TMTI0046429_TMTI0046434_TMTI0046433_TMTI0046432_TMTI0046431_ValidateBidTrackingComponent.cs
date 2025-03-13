using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Engagement

{
    class zTMTI0046430_TMTI0046429_TMTI0046434_TMTI0046433_TMTI0046432_TMTI0046431_ValidateBidTrackingComponent : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOppCounterparty counterparty = new AddOppCounterparty();       
        AddOpportunityContact addContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engHome = new EngagementHomePage();

        public static string TMTT0017889 = "TMTT0017889_CommentsAndContactsMappingToEngUponConversionFromOpportunity.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void BidTracking()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + TMTT0017889;
                string excelPath1 = ReadJSONData.data.filePaths.testData;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");   

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                 Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                 extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                               
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);

                //Login as Financial User and validate the user                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Open the selected Engagement
                string searchedEng = engHome.ValidateSearchFunctionalityOfEngagements("111861");
                engHome.ClickEngNumber();

                //Validate Bid tab
                string bid= engagementDetails.ValidateBidTab();
                Assert.AreEqual("Bids", bid);
                extentReports.CreateLog("Tab: " + bid + " is displayed under More tab on Engagement details page ");

                //Validate existing bids
                string bidsTable = engagementDetails.ValidateExistingBids();
                Assert.AreEqual("True", bidsTable);
                extentReports.CreateLog("List of existing bids is displayed upon clicking Bids tab for Internal team member of the engagement ");
                               
                //Add new bid and validate the same
                string addedBid = engagementDetails.ValidateAddedBid();
                Assert.AreEqual("Round First", addedBid);
                extentReports.CreateLog("Tab with name " +addedBid +" is displayed upon adding bid ");

                //Validate mandatory field validations
                engagementDetails.ValidateBidTab();
                string msgMinBid =  engagementDetails.ValidateMandatoryFieldValidationOfMinBid();
                Assert.AreEqual("Round Minimum (MM) is required", msgMinBid);
                extentReports.CreateLog("Validation: " + msgMinBid + " is displayed for Min Bid when no value is entered ");

                string msgMaxBid = engagementDetails.ValidateMandatoryFieldValidationOfMaxBid();
                Assert.AreEqual("Round Maximum (MM) is required", msgMaxBid);
                extentReports.CreateLog("Validation: " + msgMaxBid + " is displayed for Max Bid when no value is entered ");

                string msgBidDate = engagementDetails.ValidateMandatoryFieldValidationOfBidDate();
                Assert.AreEqual("Date is required", msgBidDate);
                extentReports.CreateLog("Validation: " + msgBidDate + " is displayed for Bid Date when no value is entered ");

                //Validate Manage functionality
                engagementDetails.ValidateBidTab();
                string minBid=engagementDetails.GetMinBidValue();
                string updMinBid = engagementDetails.ValidateManageBidFunctionality("15");
                Assert.AreNotEqual(minBid, updMinBid);
                extentReports.CreateLog("Min Bid Value: " + updMinBid + " is displayed after updating ");

                //Revert the updated value
                string revertMinBid = engagementDetails.ValidateManageBidFunctionality("10");
                Assert.AreNotEqual(updMinBid, revertMinBid);
                extentReports.CreateLog("Bid Value: " + revertMinBid + " is displayed upon updating it again ");

                //Logout of the user and click on Switch To Lightning Experience link
                usersLogin.LightningLogout();
                usersLogin.ClickSwitchToLightning();

                //Open the selected Engagement
                engHome.ValidateSearchFunctionalityOfEngagementsForAdmin("106347");
                engHome.ClickEngNumber();
                string bidAdmin = engagementDetails.ValidateBidTabForAdmin();
                Assert.AreEqual("Bids", bidAdmin);
                extentReports.CreateLog("Tab: " + bidAdmin + " is displayed under More tab on Engagement details page for Admin ");

                //Validate existing bids
                string bidsTableAdmin = engagementDetails.ValidateExistingBids();
                Assert.AreEqual("True", bidsTableAdmin);
                extentReports.CreateLog("List of existing bids is displayed upon clicking Bids tab for Admin ");

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

    

