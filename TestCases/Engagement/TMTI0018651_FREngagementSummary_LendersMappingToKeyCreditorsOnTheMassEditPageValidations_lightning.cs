using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
namespace SF_Automation.TestCases.Engagement
{
    class TMTI0018651_FREngagementSummary_LendersMappingToKeyCreditorsOnTheMassEditPageValidations_lightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();

        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();

        public static string fileTMTI0018624 = "TMTI0018624_FREngagementSummary_VerifyLendersAddedunderPreAndPostTransaction";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void FREngagementSummary_VerifyLenderAddedUnderPreAndPostTransaction()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTI0018624;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                //Search engagement with LOB - FR
                string message = engHome.SearchEngagementWithNumberOnLightning(ReadExcelData.ReadData(excelPath, "Engagement", 2), "Debtor Advisors");
                Console.WriteLine(message);
                extentReports.CreateLog("Records matches to LOB-FR are found and Engagement Detail page is displayed ");

                //Validate FR Engagement Summary page and its fields
                engagementDetails.ClickFREngSummaryButtonL();
                
                //Click Pre-Transactions Information Tab
                string PreTrans= engagementDetails.ValidatePreTransTabL();
                Assert.AreEqual("Pre-Transaction Info", PreTrans);
                extentReports.CreateLog("Tab with name: " + PreTrans + " is displayed on FR Engagement Summary ");

                //Validate title of window i.e. Add New Pre-Transaction Debt Structure
                Assert.IsTrue(summaryPage.VerifyTextFieldsOfAddDebtStructureL(), "Verified that displayed text fields labels are same");
                extentReports.CreateLog("Text fields of Add Debt Structure are displayed as expected ");

                ////Enter and save Debt Structure details
                //string valAddDebt = summaryPage.ValidateSaveFunctionalityOfAddDebtStructure();
                //Assert.AreEqual("True", valAddDebt);
                //extentReports.CreateLog("A row is created after saving details on Add Debt Structure page ");

                //Validate the added Debt Structure details
                string rowKeyCred = summaryPage.ValidateSaveFunctionalityOfAddDebtStructureByAddingAllValuesWithoutClickingAddDebt();
                Assert.AreEqual("Saved details", rowKeyCred);
                extentReports.CreateLog("Key creditor row is displayed on Add Debt Structure page ");

                usersLogin.DiffLightningLogout();
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUserLightning();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                //Search engagement with LOB - FR
                string message1 = engHome.SearchEngagementWithNumberOnLightning(ReadExcelData.ReadData(excelPath, "Engagement", 2), "Debtor Advisors");
                Console.WriteLine(message1);
                extentReports.CreateLog("Records matches to LOB-FR are found and Engagement Detail page is displayed ");

                //Validate FR Engagement Summary page and its fields
                engagementDetails.ClickFREngSummaryButtonL();

                //Click Pre-Transactions Information Tab
                string PostTrans = engagementDetails.ValidatePostTransTabL();
                Assert.AreEqual("Post-Transaction Info", PostTrans);
                extentReports.CreateLog("Tab with name: " + PostTrans + " is displayed on FR Engagement Summary ");

                //Validate title of window i.e. Add New Pre-Transaction Debt Structure
                Assert.IsTrue(summaryPage.VerifyTextFieldsOfAddDebtStructureL(), "Verified that displayed text fields labels are same");
                extentReports.CreateLog("Text fields of Add Debt Structure are displayed as expected ");

                string rowKeyCred1 = summaryPage.ValidateSaveFunctionalityOfAddDebtStructureByAddingAllValuesWithoutClickingAddDebt();
                Assert.AreEqual("Saved details", rowKeyCred1);
                extentReports.CreateLog("Key creditor row is displayed on Add Debt Structure page ");

                //Validate the title of page upon clicking Mass Edit Records button
                engagementDetails.ClickEngagementTabL();
                opportunityDetails.ValidateClientSubjectAndReferralTabL();                                 
                string titeMassEditPage = opportunityDetails.ClickMassEditRecordsButtonLightning();
                Assert.AreEqual("Additional Clients/Subjects", titeMassEditPage);
                extentReports.CreateLog("Page with title : " + titeMassEditPage + " is displayed upon clicking Mass Edit Records button ");

                //Validate the selected value in the dropdown
                string type = engagementDetails.SelectValueFromTypeDropdown("Key Creditor");
                Assert.AreEqual("Key Creditor", type);
                extentReports.CreateLog("Selected value is displayed in Type dropdown ");

                //Validate that added Creditors are displayed upon selecting Key Creditor
                string displayedComp = clientSubjectsPage.GetClientSubjectValue();
                string displayedType = clientSubjectsPage.GetTypeOfAdditionalClient("Key Creditor");
                Assert.AreEqual("Adobe Oil & Gas", displayedComp);
                Assert.AreEqual("Key Creditor", displayedType);
                extentReports.CreateLog("Company with name : " + displayedComp + " with Type: " + displayedType + " is displayed in the table upon selecting value: " + type + " in Type dropdown ");

                string displayed2ndComp = clientSubjectsPage.Get2ndClientSubjectValue();
                string displayed2ndType = clientSubjectsPage.Get2ndTypeOfAdditionalClient("Debtor Advisors", "Key Creditor");
                Assert.AreEqual("Adobe Oil & Gas", displayed2ndComp);
                extentReports.CreateLog("Company with name : " + displayed2ndComp + " is displayed in the table upon selecting value: " + type + " in Type dropdown ");

                //Delete displayed added companies
                string value = clientSubjectsPage.DeleteAddedRecordsAndValidate();
                Assert.AreEqual("Records are deleted successfully", value);
                extentReports.CreateLog("Added records are deleted successfully ");
                engagementDetails.ClickBackToEngButtonAndValidatePageL();

                //Delete added creditors in Pre and Post Transaction tabs
                engagementDetails.ClickFREngSummaryButtonL();
                engagementDetails.ValidatePreTransTabL();
                string msgDelete = summaryPage.DeleteAndValidatePreDebtStructureRecordL();
                Assert.AreEqual("No records to display", msgDelete);
                extentReports.CreateLog(msgDelete + " is displayed upon deleting the Debt Structure record ");

                //Delete the added Debt Structure details and Validate the same
                engagementDetails.ValidatePostTransTabL();
                string msgDeletePost = summaryPage.DeleteAndValidatePostDebtStructureRecordL();
                Assert.AreEqual("No records to display", msgDeletePost);
                extentReports.CreateLog(msgDeletePost + " is displayed upon deleting the Debt Structure record ");

                usersLogin.DiffLightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();
            }

            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.DiffLightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}



