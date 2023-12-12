using AventStack.ExtentReports.Gherkin.Model;
using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;


namespace SF_Automation.TestCases.Engagement
{
    class TMTT0032310_VerifyTheFieldsAndFunctionalityOfHLPostTransactionOpportunitiesTab   : BaseClass
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
        public void VerifyTheFieldsAndFunctionalityOfPostTransTab()
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

                //---1.	TMTI0075886_ Verify the availability of the "HL Post-Transaction Opp" tab on the FR Engagement Summary page
                string value = engagementDetails.ClickFREngSummaryButtonL();
                string HLPostTrans = engagementDetails.ValidateHLPostTransOppTabL();
                Assert.AreEqual("HL Post-Transaction Opportunities", HLPostTrans);
                extentReports.CreateLog("Tab with name: " + HLPostTrans + " is displayed on FR Engagement Summary ");

                //---2. TMTI0075888_ Verify the fields and values available under the "HL Post-Transaction Opp" tab. 
                string PostTransOpp = summaryPage.ValidateLabelPostTransOpp();
                Assert.AreEqual("Post-Transaction Opportunities", PostTransOpp);
                extentReports.CreateLog("Section with name: " + PostTransOpp + " is displayed on HL Post-Transaction Opp tab ");

                Assert.IsTrue(summaryPage.VerifyHLPostTransactionOppValuesL(), "Verified that displayed values are same");
                extentReports.CreateLog("Post Transaction Opportunities values are displayed as expected ");

                string PostTransOppNotes = summaryPage.ValidateLabelPostTransOppNotes();
                Assert.AreEqual("Post-Transaction Opportunity Notes", PostTransOppNotes);
                extentReports.CreateLog("Section with name: " + PostTransOppNotes + " is displayed on HL Post-Transaction Opp tab ");

                string PostTransStaffRoles = summaryPage.ValidateLabelPostTransStaffRoles();
                Assert.AreEqual("Post-Transaction Staff Roles", PostTransStaffRoles);
                extentReports.CreateLog("Section with name: " + PostTransStaffRoles + " is displayed on HL Post-Transaction Opp tab ");

                Assert.IsTrue(summaryPage.VerifyPostTransactionStaffRolesHeadersL(), "Verified that displayed Staff Roles headers are same");
                extentReports.CreateLog("Displayed Staff Roles headers are displayed as expected ");

                string PostTransKeyContact = summaryPage.ValidateLabelPostTransKeyExternalContact();
                Assert.AreEqual("Post-Transaction Key External Contact", PostTransKeyContact);
                extentReports.CreateLog("Section with name: " + PostTransKeyContact + " is displayed on HL Post-Transaction Opp tab ");

                Assert.IsTrue(summaryPage.VerifyPostTransactionKeyContactL(), "Verified that displayed Key External Contact headers are same");
                extentReports.CreateLog("Displayed Key External Contact headers are displayed as expected ");

                //3.  TMTI0075890_ Verify that selected values in Post Transaction Opportunity section displays under chosen values on the right side
                string valChosen = summaryPage.ValidateSaveFunctionalityOfHLPostTransOpp();
                Assert.AreEqual("M&A - Buyside", valChosen);
                extentReports.CreateLog("Selected value in the Post Transaction Opportunity section displays under the chosen values ");

                //4.  TMTI0075892_Verify that the user is able to save "Post-Transaction Opportunity Notes". 
                string msgNotes = summaryPage.ValidateSaveFunctionalityOfPostTransOppNotes();
                Assert.AreEqual("Record saved", msgNotes);
                extentReports.CreateLog("Post-Transaction Opportunity Notes are saved successfully ");

                //5.  TMTI0075894_Verify that on clicking "Add Staff Role" button opens up screen to enter the details. 
                string Contact = summaryPage.ValidateContactField();
                Assert.AreEqual("*Contact (Internal)", Contact);
                extentReports.CreateLog("Field with name: " + Contact + " is displayed on Add Staff Role window ");

                string Role = summaryPage.ValidateRoleField();
                Assert.AreEqual("*Role", Role);
                extentReports.CreateLog("Field with name: " + Role + " is displayed on Add Staff Role window ");

                string Cancel = summaryPage.ValidateCancelButton();
                Assert.AreEqual("Cancel", Cancel);
                extentReports.CreateLog("Button with name: " + Cancel + " is displayed on Add Staff Role window ");

                string Save = summaryPage.ValidateSaveButton();
                Assert.AreEqual("Save", Save);
                extentReports.CreateLog("Button with name: " + Save + " is displayed on Add Staff Role window ");

                //6. TMTI0075896_Verify that an error message appears for the required field on clicking the "Save" button of Add Staff Role screen on leaving fields blank
                string msgClient = summaryPage.ValidateErrorMessageForContactInt();
                Assert.AreEqual("Complete this field.", msgClient);
                extentReports.CreateLog("Message: " + msgClient + " is displayed for Contact Internal field upon clicking Save button without selecting any value ");

                string msgRole = summaryPage.ValidateErrorMessageForRole();
                Assert.AreEqual("Complete this field.", msgRole);
                extentReports.CreateLog("Message: " + msgRole + " is displayed for Role field upon clicking Save button without selecting any value ");

                //7.  TMTI0075899_Verify that clicking the "Cancel" button will take user back to list view of HL Post-Transaction Opp tab
                string cancelPage = summaryPage.ValidateCancelButtonFunctionalityOfHLPostTrans();
                Assert.AreEqual("Post-Transaction Opportunities", cancelPage);
                extentReports.CreateLog("Page with field: " + cancelPage + " is displayed upon clicking Cancel button on Add Staff Role window ");

                //8.  TMTI0075901_Verify that the "Post-Transaction Staff Role" record is created with all the entered information by clicking the "Save" button on Add Staff Role screen



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


