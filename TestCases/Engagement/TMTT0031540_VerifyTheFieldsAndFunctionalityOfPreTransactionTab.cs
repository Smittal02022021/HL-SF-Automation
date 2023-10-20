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
    class TMTT0031540_VerifyTheFieldsAndFunctionalityOfPreTransactionTab : BaseClass
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
        public void VerifyTheFieldsAndFunctionalityOfPreTransTab()
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

                //---1.	TMTI0073749_ Verify the availability of the "Pre-Transaction Info" tab on the FR Engagement Summary page
                string value = engagementDetails.ClickFREngSummaryButtonL();
                string PreTrans = engagementDetails.ValidatePreTransTabL();
                Assert.AreEqual("Pre-Transaction Info", PreTrans);
                extentReports.CreateLog("Tab with name: " + PreTrans + " is displayed on FR Engagement Summary ");

                //---2.	TMTI0073751_ Verify the fields and values available under the "Pre-Transaction Info" tab
                Assert.IsTrue(summaryPage.VerifyPreTransactionHeadersL(), "Verified that displayed headers are same");
                extentReports.CreateLog("Pre Transaction Info's headers are displayed as expected ");

                string PreOrg = summaryPage.ValidateLabelPreReOrg();
                Assert.AreEqual("Pre Reorganization Constituent Debt", PreOrg);
                extentReports.CreateLog("Section with name: " + PreOrg + " is displayed on Pre Transaction Info tab ");

                string PreOrgTotal = summaryPage.ValidateLabelPreReOrgTotalDebt();
                Assert.AreEqual("Pre Reorganized Total Debt", PreOrgTotal);
                extentReports.CreateLog("Section with name: " + PreOrgTotal + " is displayed on Pre Transaction Info tab ");

                Assert.IsTrue(summaryPage.VerifyPreTransactionGridL(), "Verified that displayed grid headers are same");
                extentReports.CreateLog("Pre Transaction Info's grid headers are displayed as expected ");

                //3.  TMTI0073755_Verify that clicking the "Add Equity Holder" button opens up the screen to enter the details
                string ClientSub = summaryPage.VerifyClientSubjectFieldL();
                Assert.AreEqual("*Client/Subject", ClientSub);
                extentReports.CreateLog("Field with name: " + ClientSub + " is displayed on Add Equity Holder window ");

                string PerOwner = summaryPage.VerifyPercentOwnershipFieldL();
                Assert.AreEqual("Percent Ownership", PerOwner);
                extentReports.CreateLog("Field with name: " + PerOwner + " is displayed on Add Equity Holder window ");

                //Validate Cancel button
                string cancel = summaryPage.ValidateCancelButton();
                Assert.AreEqual("Cancel", cancel);
                extentReports.CreateLog("Button with name: " + cancel + " is displayed on Add Equity Holder window ");

                //Validate Save button
                string save = summaryPage.ValidateSaveButton();
                Assert.AreEqual("Save", save);
                extentReports.CreateLog("Button with name: " + save + " is displayed on Add Equity Holder window ");

                //4.  TMTI0073759_Verify that an error message appears for the required field on clicking the "Save" button of Add Equity Holder screen on leaving fields blank. 
                string msgClient = summaryPage.ValidateErrorMessageForClientSubject();
                Assert.AreEqual("Complete this field.", msgClient);
                extentReports.CreateLog("Message: " + msgClient + " is displayed for Client/Subject field upon clicking Save button without selecting any value ");

                //5.  TMTI0073762_ Verify that clicking the "Cancel" button will take the user back to the list view of the Pre-Transaction Info tab
                string cancelPage = summaryPage.ValidateCancelButtonFunctionalityOfPreTrans();
                Assert.AreEqual("Pre-Transaction Info", cancelPage);
                extentReports.CreateLog("Page with tab: " + cancelPage + " is displayed upon clicking Cancel button on Add Equity Holder window ");

                //6.  TMTI0073764_ Verify that the "Pre-Transaction Equity Holder" record is created with all the entered information by clicking the "Save" button on Add Equity Holder screen. 
                string rowEquityHolder = summaryPage.ValidateSaveFunctionalityOfAddEquityHolder();
                Assert.AreEqual("True", rowEquityHolder);
                extentReports.CreateLog("A row is created after saving details on Add Equity Holder page ");

                //7. TMTI0073766_Verify that if the user selects an already added company while adding an Equity holder, the application will give an error message
                string msgSameClient = summaryPage.ValidateIfSameClientIsSelectedInAddEquityHolder();
                Assert.AreEqual("Company Name : 'Dina's Test Company' already exists as an Additional Client/Subject", msgSameClient);
                extentReports.CreateLog("Message : " + msgSameClient + " is displayed after selecting same client again and clicking on save in Add Equity Holder page ");

                //8. TMTI0073768_Verify that clicking the "Edit" button of the Pre-Transaction Equity Holder record allows the user to update the Percent Ownership only of the record.
                string finTypeBeforeUpdate = summaryPage.GetPerOwnershipBeforeUpdate();
                string perOwnerPostUpdate = summaryPage.ValidateEditFunctionalityOfAddEquityHolder();
                Assert.AreNotEqual(finTypeBeforeUpdate, perOwnerPostUpdate);
                extentReports.CreateLog("Updated value of Percent Ownership is displayed upon saving it. ");

                //9. TMTI0073770_Verify that the Company selected as Equity Holder is hyperlinked in the list view.
                string hyperlinkEquity = summaryPage.ValidateHyperlinkOfAddedEquityHolder();
                Assert.AreEqual("_blank", hyperlinkEquity);
                extentReports.CreateLog("Selected company as Equity Holder is hyperlinked in the list view ");

                //10. TMTI0073772_Verify that clicking the "Delete" button of the Equity Holder record gives a confirmation message before deleting the record.
                string msgCancel = summaryPage.ValidateCancelFunctionalityOfEquityHolder();
                Assert.AreEqual("Record is not deleted", msgCancel);
                extentReports.CreateLog("Record is not deleted after clicking cancel on confirmation page ");

                //string msgDelete = summaryPage.ValidateDeleteFunctionalityOfEquityHolder();
                //Assert.AreEqual("Record is deleted", msgDelete);
                //extentReports.CreateLog("Record is deleted after clicking Ok on confirmation page ");

                //11.  TMTI0075213_Verify that clicking the "Add Board Member" button opens up the screen with a field to search contact
                string Contacts = summaryPage.VerifyContactsFieldL();
                Assert.AreEqual("*Contact (External)", Contacts);
                extentReports.CreateLog("Field with name: " + Contacts + " is displayed on Add Board Member window ");

                //Validate Cancel button
                string cancelAddBoard = summaryPage.ValidateCancelButton();
                Assert.AreEqual("Cancel", cancel);
                extentReports.CreateLog("Button with name: " + cancel + " is displayed on Add Equity Holder window ");

                //Validate Save button
                string saveAddBoard = summaryPage.ValidateSaveButton();
                Assert.AreEqual("Save", save);
                extentReports.CreateLog("Button with name: " + save + " is displayed on Add Equity Holder window ");

                //12. TMTI0075215_Verify that an error message appears on clicking the "Save" button of Add Board Member screen without selecting contacts
                string msgContacts = summaryPage.ValidateErrorMessageForContact();
                Assert.AreEqual("Complete this field.", msgContacts);
                extentReports.CreateLog("Message: " + msgContacts + " is displayed for Contacts field upon clicking Save button without selecting any value ");

                //13. TMTI0075217_Verify that clicking the "Cancel" button of Add Board Member window takes the user back to the list view of the Pre-Transaction Info tab.
                string cancelBoard = summaryPage.ValidateCancelButtonFunctionalityOfPreTrans();
                Assert.AreEqual("Pre-Transaction Info", cancelBoard);
                extentReports.CreateLog("Page with tab: " + cancelBoard + " is displayed upon clicking Cancel button on Add Board Member window ");

                //14. TMTI0075219_Verify that the user is not able to search and add INACTIVE contact as Pre - Transaction Board Member
                string inactiveUser = summaryPage.ValidateSearchWithInactiveContact();
                Assert.AreEqual("Show All Results for \"Sue Chu\"", inactiveUser);
                extentReports.CreateLog("Search Result is not shown for inactive contact ");

                //15. TMTI0075221_Verify that the "Pre-Transaction Board Member" record is created with the selected ACTIVE contact by clicking the "Save" button on Add Board Member screen. - Completed
                string valAddBoard = summaryPage.ValidateSaveFunctionalityOfAddBoardMemberWithHLRel();
                Assert.AreEqual("True", valAddBoard);
                extentReports.CreateLog("A row is created after saving details on Add Board Member page ");

                //16. TMTI0075223_Verify that the "Has HL Relationship" checkbox is checked if the selected contact has a relationship with any of the HL Contacts
                string valHLRel = summaryPage.Validate1stHLRelationshipCheckbox();
                Assert.AreEqual("true", valHLRel);
                extentReports.CreateLog("Has HL Relationship checkbox is checked upon adding contact who has a relationship with any of the HL Contacts ");

                //17. TMTI0075225_Verify that the "Has HL Relationship" checkbox will not be checked if the selected contact doesn't have a relationship with any of the HL Contacts
                string val2ndAddBoard = summaryPage.ValidateSaveFunctionalityOfAddBoardMemberWithoutHLRel();
                Assert.AreEqual("True", val2ndAddBoard);
                extentReports.CreateLog("One more row is created after saving whose contact has not a relationship with any of the HL Contacts ");

                string valNotHLRel = summaryPage.Validate2ndHLRelationshipCheckbox();
                Assert.AreEqual(null, valNotHLRel);
                extentReports.CreateLog("Has HL Relationship checkbox is not checked upon adding contact who has not a relationship with any of the HL Contacts ");

                //18.  TMTI0075227_ Verify that if the user selects already added Contact while adding Board Member, the application gives an error message
                string errorMessage = summaryPage.ValidateErrorMessageUponAddingDuplicateContact();
                Assert.AreEqual("Duplicate record detected.", errorMessage);
                extentReports.CreateLog("Error message: " +errorMessage + " is displayed upon adding duplicate contact in Add Board Member ");

                //19. TMTI0075229_Verify that clicking the "Delete" button of the Board Member record gives a confirmation message before deleting the record
                string msgCancelBoard = summaryPage.ValidateCancelFunctionalityOfAddBoardMember();
                Assert.AreEqual("Record is not deleted", msgCancelBoard);
                extentReports.CreateLog("Record is not deleted after clicking cancel on confirmation page ");

                string msgDelete = summaryPage.ValidateDeleteFunctionalityOfBoardMember();
                Assert.AreEqual("Record is deleted", msgDelete);
                extentReports.CreateLog("Record is deleted after clicking Ok on confirmation page ");

                //20. TMTI0075231_ Verify that clicking the "Add Debt Structure" button opens up the screen having multiple fields
                
                
                
                
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


