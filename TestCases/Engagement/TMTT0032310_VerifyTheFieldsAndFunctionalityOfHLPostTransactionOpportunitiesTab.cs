using AventStack.ExtentReports.Gherkin.Model;
using Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using Sikuli4Net.sikuli_REST;
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
                //19.TMTI0075925_Verify that on clicking the "Save" button, provided information gets saved and a success message appears on the screen.
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
                Assert.AreEqual("Contact (Internal)\r\nComplete this field.", msgClient);
                extentReports.CreateLog("Message: " + msgClient + " is displayed for Contact Internal field upon clicking Save button without selecting any value ");

                string msgRole = summaryPage.ValidateErrorMessageForRole();
                Assert.AreEqual("Role\r\nComplete this field.", msgRole);
                extentReports.CreateLog("Message: " + msgRole + " is displayed for Role field upon clicking Save button without selecting any value ");

                //7.  TMTI0075899_Verify that clicking the "Cancel" button will take user back to list view of HL Post-Transaction Opp tab
                string cancelStaffRole = summaryPage.ValidateCancelButtonFunctionalityOfHLPostTrans();
                Assert.AreEqual("Post-Transaction Opportunities", cancelStaffRole);
                extentReports.CreateLog("Page with field: " + cancelStaffRole + " is displayed upon clicking Cancel button on Add Staff Role window ");

                //8.  TMTI0075901_Verify that the "Post-Transaction Staff Role" record is created with all the entered information by clicking the "Save" button on Add Staff Role screen
                string saveStaffRole = summaryPage.ValidateSaveButtonFunctionalityOfHLPostTrans();
                string role = summaryPage.GetRoleOfAddedContact();
                Assert.AreEqual("True", saveStaffRole);               
                extentReports.CreateLog("A row is added under Post-Transaction Staff Roles ");

                //9.  TMTI0075903_Verify that if the user selects an already added contact while adding Staff Role, the application will give an error message
                string msgDupStaffContact = summaryPage.ValidateErrorMessageWhileAddingExistingStaffContact();
                Assert.AreEqual("Duplicate record detected.", msgDupStaffContact);
                extentReports.CreateLog("Error message :" +msgDupStaffContact + " is displayed while adding earlier added staff contact ");

                //11. TMTI0075907_Verify the Staff Role added under the HL Post Transaction Opp tab on FR Engagement Summary is mapped to the Engagement Contacts section with the type External and role as selected
                string addedMember = summaryPage.ValidateAddedBoardMemberIsDisplayedInEngagementContacts();
                Assert.AreEqual("Sonika Goyal", addedMember);
                string addedMemberType = summaryPage.GetTypeOfAddedBoardMemberInAdditionalClientSubject();
                Assert.AreEqual("External", addedMemberType);
                string addedMemberRole = summaryPage.GetRoleOfAddedStaffInEngContacts();
                Assert.AreEqual("Financing", addedMemberRole);
                extentReports.CreateLog("Added Board Member in HL Post Transaction Opp tab on FR Engagement Summary is mapped to the Engagement Contacts section type as " + addedMemberType + " and role as " + addedMemberRole);

                //10. TMTI0075905_ Verify that clicking the "Delete" button of the Post-Transaction Staff Role record gives a confirmation message before deleting the record. 
                string msgCancelRole = summaryPage.ValidateCancelFunctionalityOfAddedStaffRole();
                Assert.AreEqual("Record is not deleted", msgCancelRole);
                extentReports.CreateLog("Record is not deleted after clicking cancel on confirmation page ");

                string msgDeleteRole = summaryPage.ValidateDeleteFunctionalityOfStaffRole();
                Assert.AreEqual("Record is deleted", msgDeleteRole);
                extentReports.CreateLog("Record is deleted after clicking Ok on confirmation page ");

                //12. TMTI0075910_ Verify that clicking the "Add Key External Contact" button opens up the screen to enter the details
                string ContactExt = summaryPage.ValidateContactFieldOnAddKeyExternalContact();
                Assert.AreEqual("*Contact (External)", ContactExt);
                extentReports.CreateLog("Field with name: " + ContactExt + " is displayed on Add Key External Contact window ");

                string RoleExt = summaryPage.ValidateRoleField();
                Assert.AreEqual("*Role", RoleExt);
                extentReports.CreateLog("Field with name: " + RoleExt + " is displayed on Add Key External Contact window ");

                string CancelExt = summaryPage.ValidateCancelButton();
                Assert.AreEqual("Cancel", CancelExt);
                extentReports.CreateLog("Button with name: " + CancelExt + " is displayed on Add Key External Contact window ");

                string SaveExt = summaryPage.ValidateSaveButton();
                Assert.AreEqual("Save", SaveExt);
                extentReports.CreateLog("Button with name: " + SaveExt + " is displayed on Add Key External Contact window ");


                //13. TMTI0075912_Verify that an error message appears for the required field on clicking the "Save" button of Add Key External Contact screen on leaving fields blank
                string msgClientExt = summaryPage.ValidateErrorMessageForContactExt();
                Assert.AreEqual("Contact (External)\r\nComplete this field.", msgClientExt);
                extentReports.CreateLog("Message: " + msgClientExt + " is displayed for Contact External field upon clicking Save button without selecting any value ");

                string msgRoleExt = summaryPage.ValidateErrorMessageForRole();
                Assert.AreEqual("Role\r\nComplete this field.", msgRoleExt);
                extentReports.CreateLog("Message: " + msgRoleExt + " is displayed for Role field upon clicking Save button without selecting any value ");

                //14. TMTI0075914_ Verify that clicking the "Cancel" button will take the user back to the list view of the HL Post-Transaction Opp tab
                string cancelKeyExt = summaryPage.ValidateCancelButtonFunctionalityOfHLPostTrans();
                Assert.AreEqual("Post-Transaction Opportunities", cancelStaffRole);
                extentReports.CreateLog("Page with field: " + cancelStaffRole + " is displayed upon clicking Cancel button on Add Key External Contact window ");

                //15. TMTI0075916_Verify that the "Post-Transaction Key External Contact" record is created with all the entered information by clicking the "Save" button on Add Key External Contact screen
                string saveKeyExt = summaryPage.ValidateSaveButtonFunctionalityOfKeyExtContact();
                string roleExt = summaryPage.GetRoleOfAddedContact();
                Assert.AreEqual("True", saveKeyExt);
                extentReports.CreateLog("A row is added under Post-Transaction Key External Contact ");

                //16.  TMTI0075918_ Verify that if the user tries to add the same contact that is added in engagement while adding Key External Contact, the application will give an appropriate error message
                string msgDupKeyExt = summaryPage.ValidateErrorMessageWhileAddingExistingKeyContact();
                Assert.AreEqual("This person has already been added as an Engagement Contact. Please go to their Engagement Contact record and click the “Key External Contact” checkbox to add them to this list.", msgDupKeyExt);
                extentReports.CreateLog("Error message :" + msgDupKeyExt + " is displayed while adding earlier added Key contact ");

                //18.  TMTI0075922_ Verify the Key External Contact added under the HL Post Transaction Opp tab on FR Engagement Summary is mapped to the Engagement Contacts section with the type External and role as selected
                string addedKeyContact = summaryPage.ValidateAddedBoardMemberIsDisplayedInEngagementContacts();
                Assert.AreEqual("Chris Sobecki", addedKeyContact);
                string addedContactType = summaryPage.GetTypeOfAddedBoardMemberInAdditionalClientSubject();
                Assert.AreEqual("External", addedContactType);
                string addedContactRole = summaryPage.GetRoleOfAddedStaffInEngContacts();
                Assert.AreEqual("Accountant to Company/Debtor", addedContactRole);
                extentReports.CreateLog("Added Key Contact in HL Post Transaction Opp tab on FR Engagement Summary is mapped to the Engagement Contacts section with type as " + addedMemberType + " and role as " + addedMemberRole);

                //17.  TMTI0075920_ Verify that clicking the "Delete" button of the Post-Transaction Key External Contact record gives a confirmation message before deleting the record
                string msgCancelKeyContact = summaryPage.ValidateCancelFunctionalityOfAddedKeyContact();
                Assert.AreEqual("Record is not deleted", msgCancelRole);
                extentReports.CreateLog("Added Key Contact record is not deleted after clicking cancel on confirmation page ");

                string msgDeleteKeyContact = summaryPage.ValidateDeleteFunctionalityOfKeyContact();
                Assert.AreEqual("Record is deleted", msgDeleteRole);
                extentReports.CreateLog("Added Key Contact record is deleted after clicking Ok on confirmation page ");

                //20. TMTI0075927_ Verify that clicking "Post-Transaction Opportunity Report" will open up a one-page report with all the details. 
                string titleReport = summaryPage.ValidateReportAfterClickingPostTransOppReport();
                Assert.AreEqual("Your connection is not private", titleReport);
                extentReports.CreateLog("Cognos report page is displayed after clicking Post-Transaction Opportunity Report button ");

                //22. TMTI0075931_Verify that on clicking the "Submit Engagement Summary" button, submits the engagement summary with a success message appears on the screen and the Closing Info section will also get updated 
                string titleSendEmail = summaryPage.ValidatePageAfterClickingSubmitEngSummary();
                Assert.AreEqual("Send Email", titleSendEmail);
                extentReports.CreateLog("Page with title:" +titleSendEmail + " is displayed upon clicking Submit Engagement Summary button ");

                //23.  TMTI0075934_Verify that clicking the "Send BTP Email" button gives a Success message on the screen and the Closing Info section will get updated with the sender's name.
                string sendEmailUser =summaryPage.ValidateMessageAfterClickingSendBTPEmail();
                Assert.AreEqual(stdUser, sendEmailUser);
                extentReports.CreateLog("Closing Info section gets updated with the sender's name.:" + sendEmailUser + " ");


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


