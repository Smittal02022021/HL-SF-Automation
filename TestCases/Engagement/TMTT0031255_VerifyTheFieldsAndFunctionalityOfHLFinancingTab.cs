using AventStack.ExtentReports.Gherkin.Model;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;


namespace SF_Automation.TestCases.Engagement
{
    class TMTT0031255_VerifyTheFieldsAndFunctionalityOfHLFinancingTab : BaseClass
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
        public void VerifyTheFieldsAndFunctionalityOfHLFinancingTab()
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

                //---TMTI0073013_ Verify the availability of the "HL Financing" tab on the FR Engagement Summary page
                 string value = engagementDetails.ClickFREngSummaryButtonL();
                 string HLFin = engagementDetails.ValidateHLFinancingTabL();
                 Assert.AreEqual("HL Financing", HLFin);
                 extentReports.CreateLog("Tab with name: " + HLFin + " is displayed on FR Engagement Summary ");

                //---TMTI0073015_ Verify the fields available under the "HL Financing" tab
                Assert.IsTrue(summaryPage.VerifyHLFinancingTableFieldsL(), "Verified that displayed HL Financing Table fields are same");
                extentReports.CreateLog("HL Financing Table fields are displayed as expected ");

                string TotalFin =summaryPage.ValidateLabelTotalFinancingAmount();
                Assert.AreEqual("Total Financing Amount", TotalFin);
                extentReports.CreateLog("Field with name: " + TotalFin + " is displayed on HL Financing tab ");

                string FinDesc = summaryPage.ValidateLabelFinancingDesc();
                Assert.AreEqual("Financing Description", FinDesc);
                extentReports.CreateLog("Field with name: " + FinDesc + " is displayed on HL Financing tab ");

                //---TMTI0073017_ Verify that clicking the "Add HL Financing" button opens up the screen to enter the details
                string FinType = summaryPage.VerifyFinancingTypeFieldL();
                Assert.AreEqual("*Financing Type", FinType);
                extentReports.CreateLog("Field with name: " + FinType + " is displayed on Add HL Financing window ");

                string SecType = summaryPage.VerifySecurityTypeFieldL();
                Assert.AreEqual("*Security Type", SecType);
                extentReports.CreateLog("Field with name: " + SecType + " is displayed on Add HL Financing window ");

                string FinAmt = summaryPage.VerifyFinancingAmountFieldL();
                Assert.AreEqual("Financing Amount (MM)", FinAmt);
                extentReports.CreateLog("Field with name: " + FinAmt + " is displayed on Add HL Financing window ");

                string Other = summaryPage.VerifyOtherFieldL();
                Assert.AreEqual("Other", Other);
                extentReports.CreateLog("Field with name: " + Other + " is displayed on Add HL Financing window ");

                //Validate values of Financing Type
                Assert.IsTrue(summaryPage.VerifyFinancingTypeValuesL(), "Verified that displayed Financing Type values are same");
                extentReports.CreateLog("Financing Type values are displayed as expected ");

                //Validate values of Security Type
                Assert.IsTrue(summaryPage.VerifySecurityTypeValuesL(), "Verified that displayed Security Type values are same");
                extentReports.CreateLog("Security Type values are displayed as expected ");


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


