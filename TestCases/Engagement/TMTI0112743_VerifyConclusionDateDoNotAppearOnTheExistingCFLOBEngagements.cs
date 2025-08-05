using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Threading;
using static SF_Automation.TestData.ReadJSONData;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace SF_Automation.TestCases.Engagement
{
    class TMTI0112743_VerifyConclusionDateDoNotAppearOnTheExistingCFLOBEngagements : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();
        LV_EngagementDetailsPage lvEngagementDetails = new LV_EngagementDetailsPage();
        
        public static string fileTMTI0112743 = "TMTI0112743_VerifyConclusionDateDoNotAppearOnTheExistingCFLOBEngagements";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyConclusionDateDoNotAppearOnTheExistingCFLOBEngagements()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0112743;

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

                int rowCount = ReadExcelData.GetRowCount(excelPath, "Engagements");

                for(int row = 2; row <= rowCount; row++)
                {
                    //Select HL Banker app
                    try
                    {
                        lvHomePage.SelectAppLV("HL Banker");
                    }
                    catch(Exception)
                    {
                        lvHomePage.SelectAppLV1("HL Banker");
                    }

                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    string engName = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagements", row, 1);
                    string engLOB = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagements", row, 2);

                    //Search Deal Team member user by global search
                    lvHomePage.SearchUserFromMainSearch(valUser);

                    //Verify searched user
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, valUser + " | Salesforce"), true);
                    extentReports.CreateLog("User " + valUser + " details are displayed ");

                    //Login as Deal Team member user
                    lvHomePage.UserLogin();

                    //Switch to lightning view
                    if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                    {
                        homePage.SwitchToLightningView();
                        extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                    }

                    //Validate user logged in
                    Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(valUser));
                    extentReports.CreateStepLogs("Passed", "Deal Team member: " + valUser + " is able to login into lightning view. ");

                    //Search the Engagement by global search
                    lvHomePage.SearchEngFromMainSearch(engName);
                    extentReports.CreateStepLogs("Info", "User navigated to " + engLOB + " Engagement detail page - " + engName);

                    //Navigate to Closing Info Tab
                    lvEngagementDetails.NavigateToClosingInfoTab();
                    extentReports.CreateStepLogs("Info", "User navigated to Closing Info Tab.");

                    //TC - TMTI0112743, TMTI0112745, TMTI0112747 - Verify that the Conclusion date no longer appears on the CF, FR & FVA LOB Engagements.

                    //Verify Conclusion Date field is not displayed on the existing CFLOB Engagements
                    Assert.IsTrue(lvEngagementDetails.VerifyConclusionDateFieldIsNotDisplayed());
                    extentReports.CreateStepLogs("Passed", "Conclusion Date field is not displayed on the existing " + engLOB + " Engagements.");

                    if(engLOB=="CF")
                    {
                        //TC - TMTI0112749 - Verify that the Conclusion Date is removed from the CF Engagement Summary report.

                        //Navigate to Engagement Summary Report page
                        Assert.IsTrue(lvEngagementDetails.NavigateToEngagementSummaryReportPage());
                        extentReports.CreateStepLogs("Passed", "User navigated to CF Engagement Summary Report page.");

                        //Verify Close Date is displayed and get the close date value
                        Assert.IsTrue(lvEngagementDetails.VerifyCloseDateIsDisplayed());
                        extentReports.CreateStepLogs("Passed", "Close Date is displayed on CF Engagement Summary Report page.");

                        string closeDate = lvEngagementDetails.GetCloseDateValue();
                        extentReports.CreateStepLogs("Info", "Close Date displayed is = " + closeDate);

                        Assert.IsTrue(lvEngagementDetails.VerifyCloseDateIsSameUnderEngagementTimelinesSection(closeDate));
                        extentReports.CreateStepLogs("Passed", "Close Date is same under Engagement Timelines section on CF Engagement Summary Report page.");

                        //TC - TMTI0112752 - Verify the field called "Conclusion Weeks from Date Engaged" on the CF Summary report will be changed to "Closed-Weeks from Date Engaged" and show the value based on the below formula=(Close Date - Date Engaged)/7
                        string dateEngaged = lvEngagementDetails.GetDateEngagedValue();
                        extentReports.CreateStepLogs("Info", "Date Engaged displayed is = " + dateEngaged);

                        string closedWeeksFromDateEngaged = lvEngagementDetails.GetClosedWeeksFromDateEngagedValue();
                        extentReports.CreateStepLogs("Info", "Closed Weeks From Date Engaged displayed is = " + closedWeeksFromDateEngaged);

                        Assert.IsTrue(lvEngagementDetails.VerifyClosedWeeksFromDateEngagedValueIsAsPerFormula(closedWeeksFromDateEngaged, closeDate, dateEngaged));
                        extentReports.CreateStepLogs("Passed", "Closed-Weeks from Date Engaged is displayed and the value is based on the below formula = (Close Date - Date Engaged)/7");
                    }

                    //TC - End
                    lvHomePage.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Info", "Deal Team member Logged Out from SF Lightning View. ");
                }

                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.Quit();
            }
        }
    }
}
