using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class TMTI0045469_TMTI0045470_VerifyNewTitleAddedinTitleRateSheet : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheet = new RateSheetManagementPage();

        public static string fileTMTI0006173 = "TMTI0045469_VerifyNewTitleAddedinTitleRateSheet";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]

        public void VerifyNewTitleAddedinTitleRateSheet()
        {

            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0006173;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                // Select the Title Rate Sheet Object
                rateSheet.SelectTitleRateSheetObject();
                extentReports.CreateLog("User " + login.ValidateUser() + " selected the Title Rate Sheet Object ");

                int rowRateSheet = ReadExcelData.GetRowCount(excelPath, "TitleRateSheet");
                for (int row = 2; row <= rowRateSheet; row++)
                {
                    string rowRateSheetname = ReadExcelData.ReadDataMultipleRows(excelPath, "TitleRateSheet",row, 1);
                    extentReports.CreateLog("Rate Sheet : " + rowRateSheetname+ " is selected ");
                    rateSheet.SelectAllRateSheets();
                    string initialValue = ReadExcelData.ReadDataMultipleRows(excelPath, "TitleRateSheet", row, 4);
                    rateSheet.SelectSheetIntials(initialValue);
                    extentReports.CreateLog(rowRateSheetname+ " Rate Sheet Initials Selected ");
                    //Selecting the desired Rate Sheet 
                    rateSheet.SelectRateSheet(rowRateSheetname);
                    extentReports.CreateLog("User Selected the " + rowRateSheetname);
                    //Assert.IsTrue(WebDriverWaits.WaitUntilTitleContains(driver, rowRateSheetname),"Verifying user is on Rate Sheet detail page");
                    //Verifying user is on Rate Sheet detail page
                    string nameRateSheetDetailPage = rateSheet.GetRateSheetDetailPage();
                    Assert.AreEqual(nameRateSheetDetailPage, rowRateSheetname);
                    extentReports.CreateLog("User is on " + rowRateSheetname+" detail page ");
                    //Verify Title is added and Rates are as expected on Rate Sheet detail page
                    string title = ReadExcelData.ReadDataMultipleRows(excelPath, "TitleRateSheet", row, 2);
                    string rate = ReadExcelData.ReadDataMultipleRows(excelPath, "TitleRateSheet", row, 3);
                    Assert.IsTrue(rateSheet.IsRateAsPerTitle(title, rate),"Verifying the Title:"+ title+ " and Rate: "+ rate);
                    extentReports.CreateLog("Title: " + title + " and Rate: " + rate + " under " +rowRateSheetname);

                }

                usersLogin.UserLogOut();
                driver.Quit();

            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }

        }
}
