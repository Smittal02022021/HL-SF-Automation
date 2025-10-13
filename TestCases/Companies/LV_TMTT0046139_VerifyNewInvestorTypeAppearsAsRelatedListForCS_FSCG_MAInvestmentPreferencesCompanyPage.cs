using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTT0046139_VerifyNewInvestorTypeAppearsAsRelatedListForCS_FSCG_MAInvestmentPreferencesCompanyPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        HomeMainPage homePage = new HomeMainPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTT0046139 = "LV_TMTT0046139_VerifyNewInvestorTypeAppearsAsRelatedListForCS_FSCG_MAInvestmentPreferencesCompanyPage";

        private int rowCompanyName;
        private string companyNameExl, excelPath, linkQuickExl, adminUserExl,msg, user, sidepanel, appNameExl, appName, moduleNameExl;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTI0113152 TC_01: Verify that new Investor Type appears as related list for CM Investment Preferences
        //TMTI0113154 TC_02: Verify that new Investor Type appears as related list for FSCG Investment Preferences
        //TMTI0113156 TC_03: Verify that new Investor Type appears as related list for M&A Investment Preferences
        //TMTI0113160 TC_04: Verify that Use Of Proceeds section has below changes made for FSCG Investment Prefrences
        //TMTI0116572 TC_05: Verify that Use Of Proceeds section has below changes made for CM Investment Prefrences
        //TMTI0116574 TC_06: Verify that Use Of Proceeds section has below changes made for M&A Investment Prefrences
        //TMTI0113158 TC_07: Verify that "Investment Preferences InvestmentType" related list has changes in existing value for M&A Investment Prefrences
        //TMTI0116580 TC_08: Verify that ""Investment Preferences InvestmentType"" related list has changes in existing value for CM Investment Prefrences	"
        //TMTI0116582 TC_09: Verify that "Investment Preferences InvestmentType" related list has changes in existing value for FSCG Investment Prefrences
        //TMTI0116584 TC_10: Verify that "New Investment Preference Contact" related list  for M&A Investment Prefrences
        //TMTI0116586 TC_11: Verify that "New Investment Preference Contact" related list  for CM Investment Prefrences
        //TMTI0116589 TC_12: Verify that "New Investment Preference Contact" related list  for FSCG Investment Prefrences
        //TMTI0116591 TC_13: Verify that "Investment Preference Sector" related list  for M&A Investment Prefrences
        //TMTI0116593 TC_14: Verify that "Investment Preference Sector" related list  for CM Investment Prefrences
        //TMTI0116595 TC_15: Verify that ""Investment Preference Sector"" related list  for FSCG Investment Prefrences	"
        //TMTI0122579 TC_16 Verify the Investment Preferences Investment Type & Investment Preferences Ownership for CS Investment preferences
        //TMTI0122587 TC_17 Verify the Investment Preferences Investment Type  for FSCG Investment preferences
        //TMTI0122592 TC_18 Verify the Investment Preferences Investment Type  for M&A Investment preferences

        [Test]
        public void VerifyNewInvestorTypeAppearsAsRelatedListForCS_FSCG_MAInvestmentPreferencesCompanyPage()
        {
            try
            {
                //Get path of Test data file
                excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0046139;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                adminUserExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(adminUserExl);
                extentReports.CreateStepLogs("Info", "System Administrator User: " + adminUserExl + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(adminUserExl), true);
                extentReports.CreateStepLogs("Passed", "System Administrator User: " + adminUserExl + " logged in on Lightning View");

                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Companies");                
                companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", 2, 1);
                companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                extentReports.CreateStepLogs("Passed", "Company: " + companyNameExl + " found and selected");

                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "IPreferencesType");
                for (int rowIP = 2; rowIP <= rowCompanyName; rowIP++)
                {
                    linkQuickExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", 2, 3);
                    companyDetail.MouseHoverDetailPageQuickLink(linkQuickExl);
                    companyDetail.ClickNewIPButtonLV();
                    extentReports.CreateStepLogs("Passed", "New Investment Preferences page is opened for company: " + companyNameExl);

                    string typeIP = ReadExcelData.ReadDataMultipleRows(excelPath, "IPreferencesType", rowIP, 1);
                    companyDetail.SelectIPTypeLV(typeIP);
                    extentReports.CreateStepLogs("Info", "Investment Preference Type: " + typeIP + " is selected");
                    companyDetail.ClickSaveIPDefaultDetailsLV();
                    string InvestmentPreferenceName= companyDetail.GetIPNameLV();
                    extentReports.CreateStepLogs("Info", "Investment Preference is saved for company: " + companyNameExl + " with type: " + typeIP+": and name: "+ InvestmentPreferenceName);

                    //TMTI0113152 TC_01: Verify that new Investor Type appears as related list for CM Investment Preferences
                    //TMTI0113154 TC_02: Verify that new Investor Type appears as related list for FSCG Investment Preferences
                    //TMTI0113156 TC_03: Verify that new Investor Type appears as related list for M & A  Investment Preferences
                    extentReports.CreateStepLogs("Info", "Verify that new Investor Type appears as related list for '" + typeIP + "' Investment Prefrences");
                    sidepanel = ReadExcelData.ReadDataMultipleRows(excelPath, "IPreferencesType", rowIP, 2);
                    companyDetail.ClickSidePanelActionIconLV(sidepanel);
                    companyDetail.ClickSidePanelActionLV("New");
                    Assert.IsTrue(companyDetail.AreAllInvestorTypeFoundLV(fileTMTT0046139), "Verify Investor Type drop-down options on 'New Investment Preference Investor Type'");
                    extentReports.CreateStepLogs("Passed", "Investor Type drop-down options on 'New Investment Preference Investor Type' All options are available");
                    
                    string valIntestorType = ReadExcelData.ReadDataMultipleRows(excelPath, "InvestorTypes", 3, 1);
                    companyDetail.ClickSidePanelActionIconLV(sidepanel);
                    companyDetail.ClickSidePanelActionLV("New");
                    companyDetail.CreateNewIPInvestorType(valIntestorType);
                    msg = randomPages.GetLVMessagePopup();
                    extentReports.CreateStepLogs("Info", "New Investment Preference Investor Type added with success message " + msg + " on Investment Preference of Type : " + typeIP);
                    string nameIPInvestor = companyDetail.GetIPInvestorNumberLV();
                    randomPages.CloseActiveTab(nameIPInvestor);
                    Assert.AreEqual(valIntestorType, companyDetail.GetIPInvestorTypeSidePanelLV(), "Verify Investment Preferences Investment Type in the right panel should show the selected value");
                    extentReports.CreateStepLogs("Passed", "New Investment Preference Investor Type added and available on side panel on Investment Preference of Type : " + typeIP);

                    //TMTI0113160 TC_04: Verify that Use Of Proceeds section has below changes made for FSCG Investment Prefrences
                    //TMTI0116572 TC_05: Verify that Use Of Proceeds section has below changes made for CM Investment Prefrences
                    //TMTI0116574 TC_06: Verify that Use Of Proceeds section has below changes made for M & A Investment Prefrences
                    extentReports.CreateStepLogs("Info", "Verify that Use Of Proceeds section has below changes made for '" + typeIP + "' Investment Prefrences");
                    sidepanel = ReadExcelData.ReadDataMultipleRows(excelPath, "IPreferencesType", rowIP, 3);
                    companyDetail.ClickSidePanelActionIconLV(sidepanel);
                    companyDetail.ClickSidePanelActionLV("New");
                    Assert.IsTrue(companyDetail.AreAllUseOfProceedsFoundLV(fileTMTT0046139), "Verify Use Of Proceeds drop-down options on 'New Investment Preference Use of Proceeds'");;
                    extentReports.CreateStepLogs("Passed", "Use Of Proceeds drop-down options on 'New Investment Preference Use of Proceeds' All options are available ");
                    randomPages.CloseActiveTab("New Investment Preference Use of Proceeds");

                    string valUseOfProceedsType = ReadExcelData.ReadDataMultipleRows(excelPath, "UseofProceeds", 3, 1);
                    companyDetail.ClickSidePanelActionIconLV(sidepanel);
                    companyDetail.ClickSidePanelActionLV("New");
                    companyDetail.CreateNewIPUseOfProceeds(valUseOfProceedsType);
                    msg = randomPages.GetLVMessagePopup();
                    extentReports.CreateStepLogs("Info", "New Investment Preference Use Of Proceeds added with success message " + msg + " on Investment Preference of Type : " + typeIP);
                    string nameIPUseOfProceeds = companyDetail.GetIPUseOfProceedsNumberLV();
                    randomPages.CloseActiveTab(nameIPUseOfProceeds);
                    Assert.AreEqual(valUseOfProceedsType, companyDetail.GetIPUseOfProceedsSidePanelLV(), "Verify Investment Preferences Use Of Proceeds in the right panel should show the selected value");
                    extentReports.CreateStepLogs("Passed", "New Investment Preference Use Of Proceeds added and available on side panel on Investment Preference of Type : " + typeIP);


                    //TMTI0113158 TC_07: Verify that "Investment Preferences Investment Type" related list has changes in existing value for M & A Investment Prefrences
                    //TMTI0116580 TC_08: Verify that "Investment Preferences Investment Type" related list has changes in existing value for CM Investment Prefrences
                    //TMTI0116582 TC_09: Verify that "Investment Preferences Investment Type" related list has changes in existing value for FSCG Investment Prefrences

                    extentReports.CreateStepLogs("Info", "Verify that 'Investment Preferences Investment Type' related list has changes in existing value for '"+ typeIP + "' Investment Prefrences");
                    sidepanel = ReadExcelData.ReadDataMultipleRows(excelPath, "IPreferencesType", rowIP, 4);
                    companyDetail.ClickSidePanelActionIconLV(sidepanel);
                    companyDetail.ClickSidePanelActionLV("New");
                    Assert.IsTrue(companyDetail.AreAllInvestmentTypesFoundLV(fileTMTT0046139), "Verify Investment Preference Investment Type drop-down options on 'New Investment Preference Investment Type'"); ;
                    extentReports.CreateStepLogs("Passed", "Investment Preference Investment Type drop-down options on 'New Investment Preference Investment Type' All options are available ");
                    randomPages.CloseActiveTab("New Investment Preference Investment Type");

                    //TMTI0122579 TC_16 Verify the Investment Preferences Investment Type for CS Investment preferences
                    //TMTI0122587 TC_17 Verify the Investment Preferences Investment Type for FSCG Investment preferences
                    //TMTI0122592 TC_18 Verify the Investment Preferences Investment Type for M&A Investment preferences
                    
                    string valIntestmentType = ReadExcelData.ReadDataMultipleRows(excelPath, "InvestmentTypes", 3, 1);
                    companyDetail.ClickSidePanelActionIconLV(sidepanel);
                    companyDetail.ClickSidePanelActionLV("New");
                    companyDetail.CreateNewIPInvestmentType(valIntestmentType);
                    msg = randomPages.GetLVMessagePopup();
                    extentReports.CreateStepLogs("Info", "New Investment Preference Investment Type added with success message " + msg + " on Investment Preference of Type : " + typeIP);
                    string nameIPType = companyDetail.GetIPTypeNumberLV();
                    randomPages.CloseActiveTab(nameIPType);
                    Assert.AreEqual(valIntestmentType,companyDetail.GetIPInvestmentTypeSidePanelLV(), "Verify Investment Preferences Investment Type in the right panel should show the selected value");
                    extentReports.CreateStepLogs("Passed", "New Investment Preference Investment Type added and available on side panel on Investment Preference of Type : " + typeIP);

                    //TMTI0116584 TC_10: Verify that "New Investment Preference Contact" related list  for M&A Investment Prefrences
                    //TMTI0116586 TC_11: Verify that "New Investment Preference Contact" related list  for CM Investment Prefrences
                    //TMTI0116589 TC_12: Verify that "New Investment Preference Contact" related list  for FSCG Investment Prefrences

                    extentReports.CreateStepLogs("Info", "Verify that 'New Investment Preference Contact' related list  for '"+ typeIP + "' Investment Prefrences");
                    sidepanel = ReadExcelData.ReadDataMultipleRows(excelPath, "IPreferencesType", rowIP, 5);
                    companyDetail.ClickSidePanelActionIconLV(sidepanel);
                    companyDetail.ClickSidePanelActionLV("New");
                    //7. Verify that Contact Lookup field appears here.
                    Assert.IsTrue(companyDetail.IsIPContactInputFieldDisplayedLV(), "Verify that Contact Lookup field appears here");
                    extentReports.CreateStepLogs("Passed", "Contact field is present on New Investment Preference Contact page");
                    string contactName = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", 2, 4);
                    string contactRole = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", 2, 5);
                    companyDetail.AddInvestmentPreferenceContactLV(contactName, contactRole);
                    msg = randomPages.GetLVMessagePopup();
                    extentReports.CreateStepLogs("Info", "Investment Preference Contact added with success message "+msg + " on Investment Preference of Type : " + typeIP);
                    string contactNumber = companyDetail.GetIPContactNumberLV();
                    extentReports.CreateStepLogs("Passed", contactNumber+ " Contact added on Investment Preference Contacts Related List");
                    randomPages.CloseActiveTab(contactNumber);
                    Assert.AreEqual(contactName,companyDetail.GetIPContactNameLV());
                    extentReports.CreateStepLogs("Passed", "Contact added on Investment Preference Contacts is available in Investment Preference Contacts Related List on Investment Preference Page for Type : " + typeIP);

                    //TMTI0116591 TC_13: Verify that "Investment Preference Sector" related list  for M & A Investment Prefrences
                    //TMTI0116593 TC_14: Verify that "Investment Preference Sector" related list  for CM Investment Prefrences
                    //TMTI0116595 TC_15: Verify that ""Investment Preference Sector"" related list  for FSCG Investment Prefrences
                    extentReports.CreateStepLogs("Info", "Verify that 'Investment Preference Sector' related list for '" + typeIP + "' Investment Prefrences");
                    sidepanel = ReadExcelData.ReadDataMultipleRows(excelPath, "IPreferencesType", rowIP, 6);
                    companyDetail.ClickSidePanelActionIconLV(sidepanel);
                    companyDetail.ClickSidePanelActionLV("New");
                    //7. Verify that Sector Lookup field appears here.
                    Assert.IsTrue(companyDetail.IsSectorInputFieldDisplayedLV(), "Verify that Sector Lookup field appears here"); ;
                    extentReports.CreateStepLogs("Passed", "Sector Lookup field is present on New Investment Preference Sector page");
                    string valCSDN = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", 2, 6);
                    companyDetail.AddInvestmentPreferenceSectorLV(valCSDN);
                    msg = randomPages.GetLVMessagePopup();
                    extentReports.CreateStepLogs("Info", "Investment Sector with success message " + msg + " on Investment Preference of Type : " + typeIP);
                    string sectorID = companyDetail.GetIPSectorLV();
                    extentReports.CreateStepLogs("Passed", sectorID + " Investment Sector added on Investment Preference Sector Related List");
                    string typeSector = companyDetail.GetIPSectorTypeLV();
                    Assert.NotNull(typeSector, "Verify Sector type is auto-populated as per the selected Sector");
                    extentReports.CreateStepLogs("Passed", "Sector Type: "+ typeSector+ " is auto-populated on Investment Preference Sector page for Type : " + typeIP);
                    randomPages.CloseActiveTab(sectorID);
                    Assert.AreEqual(sectorID, companyDetail.GetIPSectorSidePaneIDLV());
                    extentReports.CreateStepLogs("Passed", "Sector added on Investment Preference Sector is available in Investment Preference Sector Reated list on Preference Page for Type : " + typeIP);

                    if (typeIP == "CS")
                    {
                        extentReports.CreateStepLogs("Info", "Verify that new Investment Preferences Ownership for CS Investment preferences as related list for '" + typeIP + "' Investment Prefrences");
                        sidepanel = ReadExcelData.ReadDataMultipleRows(excelPath, "IPreferencesType", rowIP, 7);
                        companyDetail.ClickSidePanelActionIconLV(sidepanel);
                        companyDetail.ClickSidePanelActionLV("New");
                        Assert.IsTrue(companyDetail.AreAllOwnershipFoundLV(fileTMTT0046139), "Verify Ownership drop-down options on Investment Preferences Ownership for page for Type : " + typeIP);
                        extentReports.CreateStepLogs("Passed", "Ownership drop-down options on Investment Preferences Ownership for page for Type : " + typeIP);
                       
                        //TMTI0122579 TC_16 Verify the Investment Preferences Ownership for CS Investment preferences
                        companyDetail.ClickSidePanelActionIconLV(sidepanel);
                        companyDetail.ClickSidePanelActionLV("New");
                        string valOwnershipType = ReadExcelData.ReadDataMultipleRows(excelPath, "Ownership", 7, 1);
                        companyDetail.CreateNewIPOwnership(valOwnershipType);
                        msg = randomPages.GetLVMessagePopup();
                        extentReports.CreateStepLogs("Info", "New Investment Preference Ownership added with success message " + msg + " on Investment Preference of Type : " + typeIP);
                        string nameIPOwnershipType = companyDetail.GetIPOwnershipNumberLV();
                        randomPages.CloseActiveTab(nameIPOwnershipType);
                        Assert.AreEqual(valOwnershipType, companyDetail.GetIPOwnershipSidePanelLV(), "Verify Investment Preferences Ownership in the right panel should show the selected value");
                        extentReports.CreateStepLogs("Passed", "New Investment Preference Ownership added and available on side panel on Investment Preference detail page of Type : " + typeIP);

                    }
                    companyDetail.DeleteIPLV();
                    msg= randomPages.GetLVMessagePopup();
                    extentReports.CreateStepLogs("Info", msg+ " from Investment Preference of Type : " + typeIP);
                }
                randomPages.CloseActiveTab(companyNameExl);
                homePageLV.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Passed", "Admin User: " + adminUserExl + " logged out");
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                randomPages.CloseActiveTab(companyNameExl);
                homePageLV.LogoutFromSFLightningAsApprover();
            }
        }
    }
}
