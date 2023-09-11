using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;


namespace SF_Automation.TestCases.Engagement
{
    class TMTT0031209_VerifyTheFieldsAndFunctionalityOfTheDistressedAInformationTab : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        public static string fileTMTT0031167 = "TMTT0024518_VerifyTheFunctionalityOfFREngagementSummaryInSFLightning";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTheInformationUnderDistressedAInformationTab()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0031167;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Financial User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);                               
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateFRUserLightning();
                Console.WriteLine("stdUser: " + stdUser);
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");
                               
                //Search for the required engagement           
                int rowJobType = ReadExcelData.GetRowCount(excelPath, "Engagement");
                Console.WriteLine("rowCount " + rowJobType);
                string JobType = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", 2, 1);
                engHome.ValidateSearchFunctionalityOfEngagementsByJobType(JobType);

                //TMTI0072994_Verify the availability of the "Distressed M&A Information" tab on the FR Engagement Summary page
                 string value = engagementDetails.ClickFREngSummaryButtonL();
                 string DMA = engagementDetails.ValidateDMAndInformationTabL();
                 Assert.AreEqual("DM&A Info", DMA);
                 extentReports.CreateLog("Tab with name: " + DMA + " is displayed on FR Engagement Summary ");

                //TMTI0072997_Verify the fields available under the "Distressed M&A Information" tab
                Assert.IsTrue(summaryPage.VerifyDMAndAInfoFieldsL(), "Verified that displayed DM&A Info fields are same");
                extentReports.CreateLog("DM&A Info fields are displayed as expected ");

                //TMTI0072999_Verify that clicking the "Add Distressed M&A Information" button opens up the screen to enter the details
                Assert.IsTrue(summaryPage.VerifyAddDistressedFieldsL(), "Verified that displayed Add Distressed fields are same");
                extentReports.CreateLog("Add Distressed fields are displayed as expected ");

                //TMTI0073001_Verify that an error message appears for the required field on clicking the "Save" button of Add Distressed M&A Information on leaving fields blank. 
                string messageAssetSold = summaryPage.ValidateMandatoryMessageForAssetSoldField();
                Assert.AreEqual("Complete this field.", messageAssetSold);
                extentReports.CreateLog("Message : " + messageAssetSold + " is displayed when no value is entered into Asset Sold field ");







                usersLogin.LightningLogout();
                usersLogin.UserLogOut();
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


