using NUnit.Framework;
using SF_Automation.Pages.Tableau;
using SF_Automation.Pages;
using SF_Automation.TestData;
using SF_Automation.Pages.Common;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Tableau
{
    class TestSikuli : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        TableauPage tableau = new TableauPage();
        UsersLogin usersLogin = new UsersLogin();

        public static string username = "Screenshot_Username.png";
        public static string password = "Screenshot_Password.png";
        public static string login = "Screenshot_Login.png";
        public static string bankerDropdown = "BankerDropdown.png";
        public static string bankerTextBox = "BankerTextbox.png";
        public static string bankerName = "BankerName.png";
        public static string lblCoverageOfficer = "CoverageOfficer.png";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            TestInitialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void POC()
        {
            try 
            { 
                tableau.SikuliTableauLogin(username, password, login);
                extentReports.CreateLog("User able to sign into Tableau dashboard. ");

                tableau.SikuliSelectBanker(bankerDropdown, bankerTextBox, bankerName, lblCoverageOfficer);
                extentReports.CreateLog("User selects banker. ");
            }
            catch (Exception e)
             {
                extentReports.CreateExceptionLog(e.Message);
                driver.Quit();
            }
        }
    }
}