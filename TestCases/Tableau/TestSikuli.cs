using NUnit.Framework;
using SalesForce_Project.Pages.Tableau;
using SalesForce_Project.Pages;
using SalesForce_Project.TestData;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.UtilityFunctions;
using System;

namespace SalesForce_Project.TestCases.Tableau
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
                extentReports.CreateLog(e.Message);
                driver.Quit();
            }
        }
    }
}