using NUnit.Framework;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using SF_Automation.Pages.Company;

namespace SF_Automation.TestCases.Companies
{
    class LV_TMTC0034058_VerifyTheFunctionalityOfFilesTabOnTheCompanyDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        HomeMainPage homePage = new HomeMainPage();
        UsersLogin usersLogin = new UsersLogin();
        LVHomePage homePageLV = new LVHomePage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        RandomPages randomPages = new RandomPages();
        CompanyCreatePage createCompany = new CompanyCreatePage();

        public static string fileTMTC0034058 = "LV_TMTC0034058_VerifyTheFunctionalityOfFilesTabOnTheCompanyDetailPage";

        private int rowCompanyName;
        private string companyNameExl;
        private string excelPath;
        private string valUser;
        private string fileAction;
        private string user;
        private string appNameExl;
        private string appName;
        private string moduleNameExl;
        private string tabNameExl;
        private string fileName;
        private string fileToUpload;
        private string btnNameExl;
        private string newCompanyName;
        private string valAdminUser;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        //TMT0076592 Verify the availability of "Files" tab on the Company detail page.
        //TMT0076594  Verify that the "Files" tab lists all the uploaded files related to the company.
        //TMT0076596 Verify that the "Upload Files or Add Files" button is available on the Files tab to add files to the company.
        //TMT0076598 Verify that the user is able to upload the file followed by the success message and the list gets updated on the screen.
        //TMT0076600 Verify that the user can download the file using the "Download" button given corresponding to each File.
        //TMT0076602 Verify the "Delete" functionality of the added File on the Files tab.
        
        [Test]
        public void VerifyTheFunctionalityOfFilesTabOnTheCompanyDetailPageLV()
        {
            try
            {
                //Get path of Test data file
                excelPath = ReadJSONData.data.filePaths.testData + fileTMTC0034058;
                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();
                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
                valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                homePage.SearchUserByGlobalSearchN(valUser);
                extentReports.CreateStepLogs("Info", "CF Fin User: " + valUser + " details are displayed. ");
                //Login user
                usersLogin.LoginAsSelectedUser();
                login.SwitchToLightningExperience();
                user = login.ValidateUserLightningView();
                Assert.AreEqual(user.Contains(valUser), true);
                extentReports.CreateStepLogs("Passed", "CF Fin User: " + valUser + " logged in on Lightning View");

                appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                homePageLV.SelectAppLV(appNameExl);
                appName = homePageLV.GetAppName();
                Assert.AreEqual(appNameExl, appName);
                extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                homePageLV.SelectModule(moduleNameExl);
                extentReports.CreateStepLogs("Passed", "User is on " + moduleNameExl + " Page ");
                rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    //companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Companies", row, 1);
                    //companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    //extentReports.CreateStepLogs("Passed", "Company: " + companyNameExl + " found and selected");

                    btnNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Buttons", 2, 1);
                    companyHome.ClickButtonCompanyHomePageLV(btnNameExl);

                    string valRecordTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 1);
                    companySelectRecord.SelectCompanyRecordTypeAndClickNextLV(valRecordTypeExl);
                    // Select company record type                    
                    string createCompanyPage = createCompany.GetCreateCompanyPageHeaderLV();
                    Assert.IsTrue(createCompanyPage.Contains("New Company"));
                    extentReports.CreateStepLogs("Passed", "Page with heading: " + createCompanyPage + " is displayed upon selecting company record type ");
                    // Create a  company
                    createCompany.CreateNewCompanyLV(fileTMTC0034058, row);
                    extentReports.CreateStepLogs("Info", " New Company Created ");
                    //TMT0076297 Verify that the Company is created by clicking the "Save" button and redirecting the user to the Company detail page.
                    //Validate company detail heading
                    string companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    newCompanyName = companyDetail.GetCompanyNameHeaderLV();
                    Assert.IsTrue(newCompanyName.Contains(companyNameExl));
                    extentReports.CreateStepLogs("Passed", valRecordTypeExl + " Company created and name :" + newCompanyName + " displayed on Company Detail page Header ");

                    //TMT0076592 Verify the availability of "Files" tab on the Company detail page.
                    tabNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TabName", 7, 1);
                    Assert.IsTrue(companyDetail.IsCompanyDetailPageTabPresentLV(tabNameExl), "Verify the availability of the '" + tabNameExl + "' tab on the Company detail page");
                    extentReports.CreateStepLogs("Passed", tabNameExl + " tab available on the Company detail page");

                    //TMT0076596 Verify that the "Upload Files or Add Files" button is available on the Files tab to add files to the company.
                    companyDetail.ClickCompanyDetailPageTabLV(tabNameExl);
                    extentReports.CreateStepLogs("Passed", tabNameExl + " tab Clicked/selected on the Company detail page");
                    Assert.IsTrue(companyDetail.IsAddFilesButtonDisplayedLV(), "Verify that the 'Add Files' button is available on the Files tab to add files to the company.");
                    extentReports.CreateStepLogs("Passed", tabNameExl + "Verify that the 'Add Files' button is available on the Files tab to add files to the company.");

                    Assert.IsTrue(companyDetail.IsUploadFilesButtonDisplayedLV(), "Verify that the 'Add Files' button is available on the Files tab to add files to the company.");
                    extentReports.CreateStepLogs("Passed", tabNameExl + "Verify that the 'UploadFiles' button is available on the Files tab to add files to the company.");
                    
                    //TMT0076598 Verify that the user is able to upload the file followed by the success message and the list gets updated on the screen.
                    fileName= ReadExcelData.ReadDataMultipleRows(excelPath, "FileName", 2, 1);
                    fileToUpload = ReadJSONData.data.filePaths.testData + fileName;
                    CustomFunctions.FileUpload(driver, fileToUpload);
                    string msgBubble= randomPages.GetPopUpMessagelV();
                    Assert.IsTrue(msgBubble.Contains("file was added"));
                    extentReports.CreateStepLogs("Passed"," File is uploaded followed by the success message: "+ msgBubble);

                    //TMT0076594 Verify that the "Files" tab lists all the uploaded files related to the company.
                    Assert.IsTrue(companyDetail.IsUploadedFileDisplayedLV(fileName), "Verify that the 'Files' tab lists all the uploaded files related to the company.");
                    extentReports.CreateStepLogs("Passed", "'Files' tab lists all the uploaded files related to the company");

                    //TMT0076600 Verify that the user can download the file using the "Download" button given corresponding to each File.
                    companyDetail.ClickViewAllUploadedFilesLV();
                    fileAction= ReadExcelData.ReadDataMultipleRows(excelPath, "FileName", 2, 3);
                    companyDetail.UploadedFileMoreActionsLV(fileName, fileAction);
                    ///Need to Revisit for Download find logic
                    string locationExportedFile = ReadExcelData.ReadDataMultipleRows(excelPath, "FileName", 2,2);
                    bool isFilePresent = CustomFunctions.ValidateFileExists(locationExportedFile, fileName);
                    Assert.IsTrue(isFilePresent, " Verify File name:" + fileName + " is downloaded ");
                    extentReports.CreateStepLogs("Passed", "File name:" + fileName + " is downloaded and available at location:" + locationExportedFile);
                    //delete downloaded file
                    CustomFunctions.DeleteFile(locationExportedFile, fileName);
                    extentReports.CreateStepLogs("Info", "Downloaded File:" + fileName + " is deleted from location:" + locationExportedFile);
                    
                    //TMT0076602 Verify the "Delete" functionality of the added File on the Files tab.
                    fileAction = ReadExcelData.ReadDataMultipleRows(excelPath, "FileName", 3, 3);
                    companyDetail.UploadedFileMoreActionsLV(fileName, fileAction);
                    msgBubble = randomPages.GetPopUpMessagelV();
                    Assert.IsTrue(msgBubble.Contains("File was deleted"));
                    extentReports.CreateStepLogs("Passed", " Uploaded file was Deleted followed by the success message: " + msgBubble);

                    randomPages.CloseActiveTab("Files");
                    Assert.IsFalse(companyDetail.IsUploadedFileDisplayedLV(fileName), "Verify that Deleted file is not present on the 'Files' tab lists.");
                    extentReports.CreateStepLogs("Passed", "Deleted file is not present on the 'Files' tab lists.");
                    randomPages.CloseActiveTab(companyNameExl);                    
                }
                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "Fin user: " + valUser + " logged out");

                valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                homePage.SearchUserByGlobalSearchN(valAdminUser);
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
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Passed", companyNameExl + " found and selected");
                    companyDetail.DeleteCompanyLV();
                    extentReports.CreateStepLogs("Passed", companyNameExl + " Company Deleted Successfully");
                }
                usersLogin.ClickLogoutFromLightningView();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed Successfully");                
            }
            catch (Exception ex)
            {
                extentReports.CreateExceptionLog(ex.Message);
                //Assert.IsTrue(msgBubble.Contains("File was deleted"));
                usersLogin.ClickLogoutFromLightningView();
                valAdminUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 3, 1);
                homePage.SearchUserByGlobalSearchN(valAdminUser);
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
                    companyNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Company", row, 2);
                    companyHome.GlobalSearchCompanyInLightningView(companyNameExl);
                    extentReports.CreateStepLogs("Passed", companyNameExl + " found and selected");
                    randomPages.CloseActiveTab("Files");
                    companyDetail.ClickCompanyDetailPageTabLV(tabNameExl);                    
                    companyDetail.UploadedFileMoreActionsLV(fileName, fileAction);
                    randomPages.GetPopUpMessagelV();
                    extentReports.CreateStepLogs("Passed", "Uploaded file" + randomPages.GetPopUpMessagelV());

                    companyDetail.DeleteCompanyLV();
                    extentReports.CreateStepLogs("Passed", companyNameExl + " Company Deleted Successfully");
                }
                driver.Quit();
            }
        }
    }
}
