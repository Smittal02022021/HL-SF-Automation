using SF_Automation.Pages.Contact;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using SF_Automation.Pages.Common;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SF_Automation.TestCases.Contact
{
    class TMTI0027301_VerifyTheIndustryNameIsUpdatedOnContactDetailPage: BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        ContactHomePage contactHome = new ContactHomePage();
        ContactDetailsPage contactDetail = new ContactDetailsPage();

        public static string fileTMTI0027301 = "TMTI0027301_VerifyTheIndustryNameIsUpdatedOnContactDetailPage";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyTheIndustryNameIsUpdatedOnContactDetailPage()
        {
            //Get path of Test data file
            string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0027301;

            //Validating Title of Login Page
            Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
            extentReports.CreateLog(driver.Title + " is displayed ");

            // Calling Login function                
            login.LoginApplication();

            // Validate user logged in                   
            Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
            extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");                        

            extentReports.CreateLog("Verify the Industry Type is present on Contact Detail Page ");
            
            string industryGroupExl = ReadExcelData.ReadData(excelPath, "IndustryType", 1);
            Assert.AreEqual("Record found", contactHome.SearchContact(fileTMTI0027301));
            extentReports.CreateLog("Contact found and selected on Contact Home Page ");

            //Verify the industryGroup on Contact Detail page
            contactDetail.IsIndustryTypePresentInDropdownContactDetailPage(industryGroupExl);
            extentReports.CreateLog("Industry Group Type: "+ industryGroupExl+" is found on Contact Detail Page ");
        }
    }
}
