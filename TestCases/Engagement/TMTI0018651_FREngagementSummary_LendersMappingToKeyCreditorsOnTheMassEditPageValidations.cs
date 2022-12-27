using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
namespace SF_Automation.TestCases.Engagement
{
    class TMTI0018651_FREngagementSummary_LendersMappingToKeyCreditorsOnTheMassEditPageValidations : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        UsersLogin usersLogin = new UsersLogin();
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
                string stdUser = login.ValidateUser();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");
                                
                //Search engagement with LOB - FR
                string message = engHome.SearchEngagementWithName(ReadExcelData.ReadData(excelPath, "Engagement", 2));
                Console.WriteLine(message);
                extentReports.CreateLog("Records matches to LOB-FR are found and Engagement Detail page is displayed ");

                //Validate FR Engagement Summary page and its fields
                string titleFRSummary = engagementDetails.ClickFREngSummaryButton();
                Assert.AreEqual("FR Engagement Summary", titleFRSummary);
                extentReports.CreateLog("Page with title: " + titleFRSummary + " is displayed upon clicking FR Engagement Summary button ");

                //Click Pre-Transactions Information Tab
                summaryPage.ClickPreTransInfoTab();

                //Validate title of window i.e. Add New Pre-Transaction Debt Structure
                string titleAddDebtStructure = summaryPage.ValidateAddDebtTitle();
                Assert.AreEqual("Add New Pre-Transaction Debt Structure", titleAddDebtStructure);
                extentReports.CreateLog("Window with title: " + titleAddDebtStructure + " is displayed upon clicking Add Debt Structure button ");

                //Validate message "*Save Record to Add Key Creditors"
                string msgSaveRecord = summaryPage.ValidateSaveRecordMessage();
                Assert.AreEqual("*Save Record to Add Key Creditors", msgSaveRecord);
                extentReports.CreateLog("Message: " + msgSaveRecord + " is displayed in Add Debt Structure window ");

                //Enter and save Debt Structure details
                string msgSuccess = summaryPage.SaveDebtStructureDetails(fileTMTI0018624);
                Assert.AreEqual("Success:" + '\r' + '\n' + "Record Saved", msgSuccess);
                extentReports.CreateLog(msgSuccess + " is displayed upon adding Debt Structure ");

                //Validate the added Debt Structure details
                Assert.IsTrue(summaryPage.ValidateDebtStructureValues(), "Verified that values are same");
                extentReports.CreateLog("Debt Structure values are displayed as expected in Pre-Transaction Information tab ");

                //Click on Edit link and Add Lender details
                string msgLender = summaryPage.AddLenderDetails();
                Assert.AreEqual("Success:" + '\r' + '\n' + "Selections Saved", msgLender);
                extentReports.CreateLog(msgLender + " is displayed upon adding Key Creditors ");

                //Validate added lender details on Edit Pre-Transaction Debt Structure window
                Assert.IsTrue(summaryPage.ValidateLenderValues(), "Verified that values are same");
                extentReports.CreateLog("Key Creditors values are displayed as expected in Key Creditor section ");

                //Delete and Validate lender details on Edit Pre-Transaction Debt Structure window
                summaryPage.CloseLenderDetailsPage();

                //Click Post-Transactions Information Tab
                summaryPage.ClickPostTransInfoTab();

                //Validate title of window i.e. Add New Post-Transaction Debt Structure
                string titleAddDebtStructurePost = summaryPage.ValidatePostAddDebtTitle();
                Assert.AreEqual("Add New Post-Transaction Debt Structure", titleAddDebtStructurePost);
                extentReports.CreateLog("Window with title: " + titleAddDebtStructurePost + " is displayed upon clicking Add Debt Structure button ");

                //Validate message "*Save Record to Add Key Creditors"
                string msgSaveRecordPost = summaryPage.ValidateSaveRecordMessage();
                Assert.AreEqual("*Save Record to Add Key Creditors", msgSaveRecordPost);
                extentReports.CreateLog("Message: " + msgSaveRecordPost + " is displayed in Add Debt Structure window ");

                //Enter and save Debt Structure details
                string msgSuccessPost = summaryPage.SaveDebtStructureDetails(fileTMTI0018624);
                Assert.AreEqual("Success:" + '\r' + '\n' + "Record Saved", msgSuccessPost);
                extentReports.CreateLog(msgSuccessPost + " is displayed upon adding Debt Structure ");

                //Validate the added Debt Structure details
                Assert.IsTrue(summaryPage.ValidatePostDebtStructureValues(), "Verified that values are same");
                extentReports.CreateLog("Debt Structure values are displayed as expected in Post-Transaction Information tab ");

                //Click on Edit link and Add Key Creditors details
                string msgLenderPost = summaryPage.AddLenderDetailsPostTrans();
                Assert.AreEqual("Success:" + '\r' + '\n' + "Selections Saved", msgLenderPost);
                extentReports.CreateLog(msgLenderPost + " is displayed upon adding Key Creditor ");

                //Delete the added Debt Structure details and Validate the same
                summaryPage.CloseLenderDetailsPage();
                summaryPage.ClickReturnToEngagement();

                //Validate the title of page upon clicking Mass Edit Records button
                string titleAdditionalClientSubEng = engagementDetails.ClickMassEditRecordsButton();
                Assert.AreEqual("Additional Clients/Subjects", titleAdditionalClientSubEng);
                extentReports.CreateLog("Page with title : " + titleAdditionalClientSubEng + " is displayed upon clicking Mass Edit Records button ");

                //Validate the selected value in the dropdown
                string type = engagementDetails.SelectValueFromTypeDropdown("Key Creditor");
                Assert.AreEqual("Key Creditor", type);
                extentReports.CreateLog("Selected value is displayed in Type dropdown ");

                //Validate that added Creditors are displayed upon selecting Key Creditor
                string displayedComp = clientSubjectsPage.GetClientSubjectValue();
                string displayedType = clientSubjectsPage.GetTypeOfAdditionalClient("Key Creditor");
                Assert.AreEqual("ABC", displayedComp);
                Assert.AreEqual("Key Creditor", displayedType);
                extentReports.CreateLog("Company with name : " + displayedComp + " with Type: " + displayedType + " is displayed in the table upon selecting value: " + type + " in Type dropdown ");

                string displayed2ndComp = clientSubjectsPage.Get2ndClientSubjectValue();
                string displayed2ndType = clientSubjectsPage.Get2ndTypeOfAdditionalClient("Debtor Advisors", "Key Creditor");
                Assert.AreEqual("ABC", displayed2ndComp);               
                extentReports.CreateLog("Company with name : " + displayed2ndComp + " is displayed in the table upon selecting value: " + type + " in Type dropdown ");

                //Delete displayed added companies
                string value= clientSubjectsPage.DeleteAddedRecordsAndValidate();
                Assert.AreEqual("Records are deleted successfully", value);
                extentReports.CreateLog("Added records are deleted successfully ");
                engagementDetails.ClickBackToEngButtonAndValidatePage();

                //Delete added creditors in Pre and Post Transaction tabs
                engagementDetails.ClickFREngSummaryButton();
                summaryPage.ClickPreTransInfoTab();                
                string msgDelete = summaryPage.DeleteAndValidateDebtStructureRecord();
                Assert.AreEqual("No records to display", msgDelete);
                extentReports.CreateLog(msgDelete + " is displayed upon deleting the Debt Structure record ");

                //Delete the added Debt Structure details and Validate the same
                summaryPage.ClickPostTransInfoTab();
                string msgDeletePost = summaryPage.DeleteAndValidatePostDebtStructureRecord();
                Assert.AreEqual("No records to display", msgDeletePost);
                extentReports.CreateLog(msgDeletePost + " is displayed upon deleting the Debt Structure record ");
                
                usersLogin.UserLogOut();
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



