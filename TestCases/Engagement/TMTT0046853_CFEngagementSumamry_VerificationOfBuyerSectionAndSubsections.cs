using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Reflection;

namespace SF_Automation.TestCases.Engagement
{
    class TMTT0046853_CFEngagementSumamry_VerificationOfBuyerSectionAndSubsections : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        CFEngagementSummaryPage summaryPage = new CFEngagementSummaryPage();
        public static string fileTMTT0031164 = "TMTT0046849_VerificationOfEngagementBasicInformation";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerificationOfBuyerSectionAndSubsections()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0031164;
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
                string valJobType = ReadExcelData.ReadData(excelPath, "Engagement", 1);
                string message = engHome.SearchEngagementWithNumberOnLightning(ReadExcelData.ReadData(excelPath, "Engagement", 2), valJobType);
                Assert.AreEqual("Project Moon", message);
                extentReports.CreateLog("Records matching with selected Job Type are displayed ");                

                //Fetch the Company and Type from Counterparty of Closing Info tab
                engagementDetails.ClickClosingInfo();
                string company = engagementDetails.GetCompanyOfCounterparty();
                string type = engagementDetails.GetTypeOfCounterparty();

                //Fetch the IG and Client Ownership from Company of Closing Info tab                
                string IG = engagementDetails.GetIGvalueFromCompany();
                string Ownership = engagementDetails.GetOwnershipFromCompany();

                //1.  TMTI0114566_ Verify that the Buyer's basic information is displayed under the Parties Buyer section
                engagementDetails.ClickEngTab();
                engagementDetails.ClickCFEngsummaryButtonL();
                string secParties = summaryPage.ValidatePartiesSection();
                string secSeller = summaryPage.ValidateSellerSection();
                string secBuyer = summaryPage.ValidateBuyerSection();
                Assert.AreEqual("Buyer", secBuyer);
                extentReports.CreateLog("Section with name: " +secBuyer +" is displayed after clicking Buyer section ");

                string companyBuyside = summaryPage.ValidateCompanyOfBuyer();
                Console.WriteLine("companyBuyside" + companyBuyside);
                string typeBuyside = summaryPage.ValidateTypeOfBuyer();
                Console.WriteLine("typeBuyside" + typeBuyside);
                Assert.AreEqual(company, companyBuyside);
                Assert.AreEqual(type, typeBuyside);
                extentReports.CreateLog("Company: " + companyBuyside + " and Type: " + typeBuyside + " are mapped to Company and Type of Winning Counterparty if the deal is of Sellside ");

                string IGBuyside = summaryPage.ValidateIGOfBuyer();
                string OwnershipBuyside = summaryPage.ValidateOwnershipOfBuyer();
                Assert.AreEqual(IG, IGBuyside);
                Assert.AreEqual(Ownership, OwnershipBuyside);               
                extentReports.CreateLog("Industry Group: " + IGBuyside + " and Ownership: " + OwnershipBuyside + " are mapped to IG and Ownership of Winning Counterparty's Company ");

                string reqField = summaryPage.ValidateMandatoryValidationOfBuyerCompany();
                Assert.AreEqual("Company", reqField);
                extentReports.CreateLog("Mandatory Field " + reqField + " is displayed upon mover hover on Buyer ");

                //2.	TMTI0114575_ Verify that the "Buyer's Background" information is displayed under the subsection Buyer Background
                
                
                
                
                
                
                usersLogin.LightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();            
            }

            catch (Exception e)
            {
                extentReports.CreateLog(e.Message);
                usersLogin.LightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}


