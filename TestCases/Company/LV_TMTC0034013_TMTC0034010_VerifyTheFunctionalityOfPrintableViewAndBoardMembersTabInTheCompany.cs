using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Company;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTC0034013_TMTC0034010_VerifyTheFunctionalityOfPrintableViewAndBoardMembersTabInTheCompany : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        HomeMainPage homePage = new HomeMainPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();

        public static string fileTMTC0034013 = "LV_TMTC0034013_VerifyTheFunctionalityOfTheBoardMembersTabInTheCompany";

        private int rowCompanyName;
        private string newCompanyName;
        private string excelPath;
        private string moduleNameExl;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMTC0034010 Verify the functionality of the "Printable View" tab in Company
            //TMT0076401 Verify the availability of the "Printable View" tab on the Company detail page.
            //TMT0076403 Verify that "Printable View" opens up a tab to get the print view of the Company detail page
        
        //TMTC0034013Verify the functionality of the "Board Members" tab in the Company
            //TMT0076406 Verify the availability of the "Board Members" tab on the Company detail page
            //TMT0076408 Verify that clicking "New" from Board Members navigates to the New Affiliation Page and is prefilled with the selected company's name and Status as Current
            //TMT0076410 Verify that the required field validation appears on clicking the "Save" button without filling in any details on the Board Members - New Affiliation screen
            //TMT0076412 Verify that clicking the "Cancel" button of New Affiliation navigates the user back to the Board Member screen
            //TMT0076414 Verify that the user can save the board member of the "Inside Board Member" type by clicking the "Save" button of New Affiliation
            //TMT0076416 Verify that the created Board Member(Affiliation) will be listed under the Board Members tab of the Company Detail Page.
            //TMT0076418 Verify the functionality of the "Edit" action button on the Board Member - Affiliation detail page
            //TMT0076420 Verify the functionality of the "Delete" button on the Board Member - Affiliation details of the Board Member
            //TMT0076747 Verify that the user can save the board member of the "Outside Board Member" type by clicking the "Save" button of New Affiliation

        [Test]
        public void VerifyFunctionalityOfCompaniesInfoLV()
        {
            try
            {
                //Get path of Test data file
                excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0034013;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "CF Fin User: " + valUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged in on Lightning View");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Info", "User is on " + moduleNameExl + " Page ");

                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    string btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 2, 1);
                    companyHome.ClickButtonCompanyHomePageLV(btnNameExl);

                    string valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    companySelectRecord.SelectCompanyRecordTypeAndClickNextLV(valRecordTypeExl);
                    // Select company record type                    
                    string createCompanyPage = createCompany.GetCreateCompanyPageHeaderLV();
                    Assert.IsTrue(createCompanyPage.Contains("New Company"));
                    extentReports.CreateStepLogs("Passed", "Page with heading: " + createCompanyPage + " is displayed upon selecting company record type ");

                    // Validate company type display as selected 
                    Assert.AreEqual(valRecordTypeExl, createCompany.GetSelectedCompanyTypeLV());
                    extentReports.CreateStepLogs("Passed", "Selected company type: " + valRecordTypeExl + " choosen on select company record type page is matching on Company create page ");
                    // Create a  company
                    createCompany.CreateNewCompanyLV(fileTMTC0034013, row);
                    extentReports.CreateStepLogs("Info", " New Company Created ");
                    //Validate company detail heading
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    newCompanyName = companyDetail.GetCompanyNameHeaderLV();
                    Assert.IsTrue(newCompanyName.Contains(companyNameExl));
                    extentReports.CreateStepLogs("Passed", valRecordTypeExl + " Company created and name :" + newCompanyName + " displayed on Company Detail page Header ");

                    //TMT0076401	Verify the availability of the "Printable View" tab on the Company detail page.
                    Assert.IsTrue(companyDetail.IsPrintableViewButtonDisplayed(), "Verify the availability of the 'Printable View' button on the Company detail page.");
                    extentReports.CreateStepLogs("Passed", valRecordTypeExl + " 'Printable View' button is displayed on the created Company: " + newCompanyName);

                    //TMT0076403	Verify that "Printable View" opens up a tab to get the print view of the Company detail page
                    companyDetail.ClickPritableViewLV();
                    CustomFunctions.SwitchToWindow(driver, 1);
                    Assert.IsTrue(companyDetail.IsPrintThisPageLinkDisplayedLV(), "Verify that 'Printable View' opens up a tab to get the print view of the Company detail page");
                    extentReports.CreateStepLogs("Passed", "Clicking on Printable View' opens up a tab to get the print view of the Company detail page");
                    driver.Close();
                    CustomFunctions.SwitchToWindow(driver, 0);
                    extentReports.CreateStepLogs("Info", "Printable View window is closed");
                    //TMT0076406	Verify the availability of the "Board Members" tab on the Company detail page
                    companyDetail.ClickBoardMembersTabLV();
                    extentReports.CreateStepLogs("Passed", "Board Members tab is available and clicked on created Company created ");

                    //TMT0076408	Verify that clicking "New" from Board Members navigates to the New Affiliation Page and is prefilled with the selected company's name and Status as Current
                    companyDetail.ClickNewBoardMemberButtonLV();
                    extentReports.CreateStepLogs("Passed", "New Button Clicked from Board Members tab on created Company created ");

                    string companyAff=companyDetail.GetPrefilledAffCompanyNameLV();
                    Assert.AreEqual(companyNameExl, companyAff);
                    string statusAff=companyDetail.GetPrefilledAffStatusLV();
                    Assert.AreEqual("Current", statusAff);

                    //TMT0076410	Verify that the required field validation appears on clicking the "Save" button without filling in any details on the Board Members - New Affiliation screen
                    companyDetail.ClickSaveNewAffiliationButtonLV();
                    string expectedRequiredFields = ReadExcelData.ReadDataMultipleRows(excelPath, "Affiliation", row, 6);

                    string actualRequiredFields= companyDetail.GetNewAffiliationReqFieldsLV();
                    Assert.AreEqual(expectedRequiredFields, actualRequiredFields);
                    extentReports.CreateStepLogs("Passed", "All Required Fields Validations are displayed on New Affiliation page");

                    //TMT0076412	Verify that clicking the "Cancel" button of New Affiliation navigates the user back to the Board Member screen
                    companyDetail.ClickCancelNewAffiliationButtonLV();
                    Assert.IsTrue(companyDetail.IsBoardMembersHeaderDisplayedLV(), "Verify that clicking the 'Cancel' button of New Affiliation navigates the user back to the Board Member screen");
                    extentReports.CreateStepLogs("Passed", "Clicking the 'Cancel' button on New Affiliation page navigates the user back to the Board Member screen");

                    //TMT0076414 Verify that the user can save the board member of the "Inside Board Member" type by clicking the "Save" button of New Affiliation
                    //TMT0076747 Verify that the user can save the board member of the "Outside Board Member" type by clicking the "Save" button of New Affiliation

                    companyDetail.ClickNewBoardMemberButtonLV();
                    extentReports.CreateStepLogs("Passed", "New Button Clicked from Board Members tab on created Company created ");

                    string contactNameAff= ReadExcelData.ReadDataMultipleRows(excelPath, "Affiliation", row, 1);
                    string typeAff= ReadExcelData.ReadDataMultipleRows(excelPath, "Affiliation", row, 2);
                    string msgSuccess=companyDetail.CreateNewAffiliationLV(contactNameAff, typeAff);
                    Assert.IsTrue(msgSuccess.Contains("created"));
                    extentReports.CreateStepLogs("Passed", "New Affiliation Added with Success message: " + msgSuccess);

                    string numberAffiliation = companyDetail.GetAffiliationNumberLV();
                    extentReports.CreateStepLogs("Passed", "New Affiliation Added with Number: " + numberAffiliation + " and User redirected to Affiliation detail page ");

                    string detailAffiliationType = companyDetail.GetAffiliationTypeLV();
                    Assert.AreEqual(typeAff, detailAffiliationType);
                    extentReports.CreateStepLogs("Passed", "User redirected to Affiliation detail page with Type: "+ detailAffiliationType);
                    randomPages.CloseActiveTab(numberAffiliation);

                    //TMT0076416 Verify that the created Board Member(Affiliation) will be listed under the Board Members tab of the Company Detail Page.
                    companyDetail.ClickBoardMembersTabLV();
                    string nameBoardMemberType= companyDetail.GetBoardMemberAffiliationTypeLV();
                    Assert.IsTrue(nameBoardMemberType.Contains(typeAff));
                    extentReports.CreateStepLogs("Passed", "Created Board Member(Affiliation)listed under the Board Members tab of the Company Detail Page");

                    //TMT0076418	Verify the functionality of the "Edit" action button on the Board Member - Affiliation detail page
                    companyDetail.ClickEditBoardMemberLinkLV();
                    string notesExl= ReadExcelData.ReadDataMultipleRows(excelPath, "Affiliation", row, 3);
                    companyDetail.UpdateAffiliationNotesLV(notesExl);
                    string valAffNotes= companyDetail.GetBoardMemberAffiliationNotesLV();
                    Assert.AreEqual(notesExl, valAffNotes);
                    extentReports.CreateStepLogs("Passed", "Updated value is saved on created Board Member(Affiliation) listed under the Board Members tab of the Company Detail Page");

                    //TMT0076420	Verify the functionality of the "Delete" button on the Board Member - Affiliation details of the Board Member
                    companyDetail.DeleteBoardMemberRecordLinkLV();
                    extentReports.CreateStepLogs("Passed", "Added Affiliation record is Deleted listed under the Board Members tab of the Company Detail Page");

                    randomPages.CloseActiveTab(companyNameExl);
                }

                usersLogin.ClickLogoutFromLightningView();
                string valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                homePage.SearchUserByGlobalSearchN(valAdminUser);
                extentReports.CreateStepLogs("Info", "System Admin User: " + valAdminUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valAdminUser), true);
                extentReports.CreateStepLogs("Passed", "System Admin User: " + valAdminUser + " logged in on Lightning View");

                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    companyDetail.DeleteCompanyLV();
                    extentReports.CreateStepLogs("Passed", companyNameExl + " Company Deleted");
                }
                usersLogin.ClickLogoutFromLightningView();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.SwitchTo().DefaultContent();
                usersLogin.ClickLogoutFromLightningView();
                string valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                homePage.SearchUserByGlobalSearchN(valAdminUser);
                extentReports.CreateStepLogs("Info", "System Admin User: " + valAdminUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                string user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valAdminUser), true);
                extentReports.CreateStepLogs("Passed", "System Admin User: " + valAdminUser + " logged in on Lightning View");

                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    companyDetail.DeleteCompanyLV();
                    extentReports.CreateStepLogs("Passed", companyNameExl + " Company Deleted");
                }
                usersLogin.ClickLogoutFromLightningView();
                driver.Quit();
            }
        }
    }
}

