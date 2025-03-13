using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Company;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.Contact;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTC0033978_2_VerifyTheFunctionalityOfNewContactButtonAndHLRelationshipObjectOCompanyDetailsPage:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        HomeMainPage homePage = new HomeMainPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        ContactHomePage contactHome= new ContactHomePage();
        ContactDetailsPage contactDetail= new ContactDetailsPage();

        public static string fileTMT0076335 = "LV_TMT0076335_VerifyTheFunctionalityOfNewContactButtonAndHLRelationshipObjectOCompanyDetailsPage";

        int rowCompanyName;
        string newCompanyName;
        string excelPath;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        //TMT0076335 Verify the functionality of the "New Contact" button of the related Object of the Company Details Page
        //TMT0076338 Verify that the created HL Relationship displays under the Company Contacts as a child link with all the details
        //TMT0076340 Verify that clicking "View Details" of the Relationship of the company contact redirects the user to a relationship detail page
        //TMT0076342 Verify that clicking "Edit" on the Relationship of the company contact redirects the user to the relationship detail page
        //TMT0076344 Verify the functionality of "Delete" button on the Relationship details of the company contact

        [Test]
        public void VerifyTheFunctionalityOfNewContactButtonAndHLRelationshipObjectOCompanyDetailsPageLV()
        {
            try
            {
                //Get path of Test data file
                excelPath = ReadJSONData.data.filePaths.testData + fileTMT0076335;
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

                string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
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
                    createCompany.CreateNewCompanyLV(fileTMT0076335, row);
                    extentReports.CreateStepLogs("Info", " New Company Created ");
                    //Validate company detail heading
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    newCompanyName = companyDetail.GetCompanyNameHeaderLV();
                    Assert.IsTrue(newCompanyName.Contains(companyNameExl));
                    extentReports.CreateStepLogs("Passed", valRecordTypeExl + " Company created and name :" + newCompanyName + " displayed on Company Detail page Header ");

                    //TMT0076335	Verify the functionality of the "New Contact" button of the related Object of the Company Details Page
                    companyDetail.ClickNewContactLV();
                    string valFirstName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1);
                    string valLastName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2);
                    string contactFullName = valFirstName + " " + valLastName;
                    string msgSuccess = companyDetail.CreateNewContactLV(valFirstName, valLastName);
                    Assert.IsTrue(msgSuccess.Contains(contactFullName), "Verify Success messag is poped up on creation of New Contact");
                    companyDetail.ClickContactTabLV();
                    Assert.IsTrue(companyDetail.IsContactPresentInRelatedTabListLV(contactFullName), "Verify that the created contact will be listed under the Contacts tab of the Company Detail Page");
                    extentReports.CreateStepLogs("Passed", contactFullName + " created contact is listed under the Contacts tab of the Company Detail Page");

                    //TMT0076338	Verify that the created HL Relationship displays under the Company Contacts as a child link with all the details
                    companyDetail.ClickContactnameInRelatedTabListLV(contactFullName);
                    companyDetail.ClickAddRelationshipButtonLV();
                    msgSuccess= companyDetail.AddRelationshipLV(valUser);
                    extentReports.CreateStepLogs("Passed", "HL Contact added with Success message: "+ msgSuccess);
                    randomPages.CloseActiveTab(contactFullName);
                    Assert.IsTrue(companyDetail.IsContactNestedListHLRelationshipLV(contactFullName), "Verify that the created HL Relationship displays under the Company Contacts as a child link with all the details");
                    extentReports.CreateStepLogs("Passed", "Nested List is Displayed to show HL Relationship for Contact:  " + contactFullName);

                    //TMT0076347	Verify the functionality of the "View Contacts Relationship" tab on the Company detail page redirects HL_Contacts with the Relationships report.
                    companyDetail.ClickViewContactRelationshipLV();
                    Assert.IsTrue(companyDetail.IsContactRelationshipReportPageDisplayedLV(), "Verify the functionality of the'View Contacts Relationship' tab on the Company detail page redirects HL_Contacts with the Relationships report");
                    extentReports.CreateStepLogs("Passed", "Click on 'View Contacts Relationship' button on the Company detail page redirects to HL_Contacts with the Relationships report");

                    //TMT0076349	Verify that the Nested List to show HL Relationship is displaying for the Company's Contacts.
                    Assert.AreEqual(valUser,companyDetail.GetHLContactLV());
                    extentReports.CreateStepLogs("Passed", "HL Relationship Contact: "+valUser+" is available on HL Ralationship Report page after click on 'View Contacts Relationship' button ");
                    randomPages.CloseActiveTab("BUTTON - Contacts with Relationships");

                    //TMT0076340	Verify that clicking "View Details" of the Relationship of the company contact redirects the user to a relationship detail page
                    companyDetail.ClickViewNestedRContactLV(contactFullName);
                    companyDetail.ClickViewDetailsRelationshipContactLV();
                    extentReports.CreateStepLogs("Info", "View Detail link is clicked from Relationship Contact " + contactFullName+ " and user redirected to Detail Relationship page");
                    string contactHLRelationship=companyDetail.GetHLRelationshipContactLV();
                    string numberRelationship = contactDetail.GetRelationshipNumberLV();
                    Assert.AreEqual(valUser, contactHLRelationship);
                    extentReports.CreateStepLogs("Passed", "New Relationship: "+ numberRelationship+"  is added with HL Contact: " + contactHLRelationship);

                    //TMT0076342	Verify that clicking "Edit" on the Relationship of the company contact redirects the user to the relationship detail page
                    contactDetail.ClickEditRelationButtonLV();
                    msgSuccess=contactDetail.UpdateRelationshipDetailsNotesLV();
                    Assert.IsTrue(msgSuccess.Contains("saved"));
                    extentReports.CreateStepLogs("Passed", msgSuccess);

                    //User is not redirected to relationship detail page 
                    randomPages.CloseActiveTab("Edit "+numberRelationship);
                    extentReports.CreateStepLogs("Passed", numberRelationship+" Edit Relationship tab is closed");
                    
                    //TMT0076344	Verify the functionality of "Delete" button on the Relationship details of the company contact
                    contactDetail.DeleteHLRelationshipLV();
                    extentReports.CreateStepLogs("Info", "HL Relationship Contact: "+ contactHLRelationship + " Deleted");

                    randomPages.CloseActiveTab(newCompanyName);
                }
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged out");

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
                
                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    string valFirstName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1);
                    string valLastName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2);
                    string contactFullName = valFirstName + " " + valLastName;
                    contactHome.GlobalSearchContactInLightningView(contactFullName);
                    contactDetail.DeleteContactLV();
                    extentReports.CreateStepLogs("Passed", contactFullName + " Contact Deleted");
                }
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
                string moduleNameExl;
                string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                string appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                try
                {
                    moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 3, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                    for (int row = 2; row <= rowCompanyName; row++)
                    {                        
                        string valFirstName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 1);
                        string valLastName = ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 2);
                        string contactFullName = valFirstName + " " + valLastName;
                        contactHome.GlobalSearchContactInLightningView(contactFullName);
                        moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 4, 1);
                        contactDetail.ClickContactDetailsPageTabLV(moduleNameExl);
                        try
                        {
                            contactDetail.ClickViewDetailRelationshipLV();
                            contactDetail.DeleteHLRelationshipLV();
                            extentReports.CreateStepLogs("Info", "Contact HL Relationship Deleted");
                        }
                        catch
                        {

                        }
                        contactDetail.DeleteContactLV();
                        extentReports.CreateStepLogs("Passed", contactFullName + " Contact Deleted");
                    }
                }
                
                catch (Exception ex)
                {
                    extentReports.CreateExceptionLog(ex.Message);
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
                }
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
