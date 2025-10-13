using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.OpportunitiesOracleERP
{
    class LV_TS22_TS23_TS24_ValidateIsMainContractIsFromClientCompanyWhenIsMainContractFromOtherCompanyExistsAlreadyEditingonOpportunityPageLV : BaseClass
    {//TS22_TS23_TS24_ValidateIsMainContractIsTRUEWhenItWasSetAsFalseWhileSavingAndEditingContractAndFalseWhenNewContractIsAddedAsIsMainContractTRUELV
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();

        public static string fileContract = "LV_TS02_PostUpdatingDFFFieldsOfOpportunity";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void ValidateIsMainContractCheckboxOnOpportunityPageLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileContract;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                // Calling Login function                
                login.LoginApplication();
                login.SwitchToClassicView();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");

                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 25);

                extentReports.CreateStepLogs("Info", "Creating Opportunity for LOB : " + valRecordType + " and Job Type: " + valJobType + " ");
                //Login as Standard User profile and validate the user
                string valUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                //Search CF Financial user by global search
                homePage.SearchUserByGlobalSearchN(valUserExl);
                extentReports.CreateStepLogs("Info", "User: " + valUserExl + " details are displayed. ");

                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valUserExl), true);
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateLog("User is on " + moduleNameExl + " Page ");

                //Validating Title of New Opportunity Page
                string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                extentReports.CreateStepLogs("Info", "Creating Opportunity for Job Type: " + valJobType);
                string opportunityName = addOpportunity.AddOpportunitiesLightningV3(valRecordType, valJobType, fileContract);
                extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileContract);
                Assert.AreEqual(displayedTab, "Info");
                extentReports.CreateStepLogs("Passed", "User is on Opportunity detail " + displayedTab + " tab ");

                ////Validating Opportunity details  
                string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                Assert.IsNotNull(opportunityDetails.GetOpportunityNumberL());
                extentReports.CreateStepLogs("Passed", "Opportunity with number : " + opportunityNumber + " is created ");

                //Create External Primary Contact      
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                string party = ReadExcelData.ReadData(excelPath, "AddContact", 3);
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);

                addOpportunityContact.CickAddCFOpportunityContact();
                addOpportunityContact.CreateContactL2(fileContract, valRecordType);
                extentReports.CreateStepLogs("Info", valContact + " is added as " + valContactType + " opportunity contact is saved ");

                randomPages.CloseActiveTab(opportunityName);
                extentReports.CreateStepLogs("Info", "Opportunity tab is closed");
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", valUserExl + " Logged out");

                //Performing actions with System Admin
                string caoUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);
                homePage.SearchUserByGlobalSearchN(caoUserExl);
                extentReports.CreateStepLogs("Info", "User: " + caoUserExl + " details are displayed. ");
                usersLogin.LoginAsSelectedUser();

                login.SwitchToLightningExperience();
                string userName = login.ValidateUserLightningView();
                Assert.AreEqual(userName.Contains(caoUserExl), true);
                extentReports.CreateLog("System Administrator User: " + caoUserExl + " logged in on Lightning View");
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                //Search for added opportunity
                opportunityHome.SearchOpportunitiesInLightningView(opportunityName);
                extentReports.CreateStepLogs("Passed", "Opportunity: " + opportunityName + " found and selected ");

                //Click on New Contract button
                opportunityDetails.GoToContractTabLV();
                //Add the contract by selecting any company other than Client Company
                string contractName1Exl = ReadExcelData.ReadData(excelPath, "AddContact", 9);
                string companynameExl = ReadExcelData.ReadData(excelPath, "AddContact", 10);
                opportunityDetails.AddContractBySelectingACompanyLV(contractName1Exl, valContact,companynameExl);
                string msgPop = randomPages.GetLVMessagePopup();
                Assert.IsTrue(msgPop.Contains(contractName1Exl));
                extentReports.CreateStepLogs("Passed", msgPop);

                //Validate if Is Main Contract checkbox is checked need to work 
                string valueIsMain = opportunityDetails.ValidateIsMainContractLV();
                Assert.AreEqual("Is Main Contract checkbox is checked", valueIsMain);
                extentReports.CreateLog(valueIsMain + " of added Contract: "+contractName1Exl+" even it was not selected while saving the details ");

                //---------------------------------------------------------------//
                //TS22_TS23_TS24_ValidateIsMainContractIsTRUEWhenItWasSetAsFalseWhileSavingAndEditingContractAndFalseWhenNewContractIsAddedAsIsMainContractTRUELV
                //Click Edit link of added Contract
                string titleEdit = opportunityDetails.ClickEditContractLV();
                Assert.IsTrue(titleEdit.Contains("Edit"));
                extentReports.CreateLog("Page with title: " + titleEdit + " is displayed upon clicking edit button from added Contract page");

                //Updated the contract by deselecting Is Main Contract checkbox
                opportunityDetails.EditContractByDeselectingIsMainContractLV();
                msgPop = randomPages.GetLVMessagePopup();
                extentReports.CreateStepLogs("Passed", msgPop);
                Assert.IsTrue(msgPop.Contains(contractName1Exl));                
                extentReports.CreateLog("Contract details are saved by deselecting Is Main Contract checkbox ");

                //Validate if Is Main Contract checkbox is checked
                valueIsMain = opportunityDetails.ValidateIsMainContractLV();
                Assert.AreEqual("Is Main Contract checkbox is checked", valueIsMain);
                extentReports.CreateLog(valueIsMain + " even it was deselected while editing the contract details ");

                //---------------------------------------------------------------//
                randomPages.CloseActiveTab(contractName1Exl);
                extentReports.CreateStepLogs("Info", contractName1Exl+" detail page is closed");

                //Add one more contract from client company
                opportunityDetails.GoToContractTabLV();
                string clientComp = ReadExcelData.ReadData(excelPath, "AddOpportunity", 1);
                string contractName2Exl = ReadExcelData.ReadData(excelPath, "AddContact", 11);
                opportunityDetails.AddContractBySelectingACompanyLV(contractName2Exl, valContact, clientComp);
                msgPop = randomPages.GetLVMessagePopup();
                Assert.IsTrue(msgPop.Contains(contractName2Exl));
                extentReports.CreateStepLogs("Passed", msgPop);
                //Validate if Is Main Contract checkbox is checked for new contract added selecting client company need to work 
                valueIsMain = opportunityDetails.ValidateIsMainContractLV();
                Assert.AreEqual("Is Main Contract checkbox is checked", valueIsMain);
                extentReports.CreateLog(valueIsMain + " of added Contract: "+contractName2Exl+"  even it was not selected while saving the details ");
                randomPages.CloseActiveTab(contractName2Exl);
                extentReports.CreateStepLogs("Info", contractName2Exl + " detail page is closed");

                //Validate if Is Main Contract checkbox is checked for previously added contract need to work 
                string valueNewIsMainPrev = opportunityDetails.ValidateContractIsMainCheboxLV(contractName1Exl);
                Assert.AreEqual("Is Main Contract checkbox is not checked", valueNewIsMainPrev);
                extentReports.CreateLog(valueNewIsMainPrev + " for the earlier added Contract: "+contractName1Exl+" from company other than client company ");
                randomPages.CloseActiveTab(opportunityName);
                extentReports.CreateStepLogs("Info", contractName1Exl + " detail page is closed");

                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info","CAO User: "+ caoUserExl + " Logged out");
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}
