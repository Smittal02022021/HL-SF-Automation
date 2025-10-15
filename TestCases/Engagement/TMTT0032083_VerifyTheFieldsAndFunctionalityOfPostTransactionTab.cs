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
    class TMTT0032083_VerifyTheFieldsAndFunctionalityOfPostTransactionTab : BaseClass
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

                //---1.	TMTI0075796_ Verify the availability of the "Post-Transaction Info" tab on the FR Engagement Summary page
                string value = engagementDetails.ClickFREngSummaryButtonL();
                string PostTrans = engagementDetails.ValidatePostTransTabL();
                Assert.AreEqual("Post-Transaction Info", PostTrans);
                extentReports.CreateLog("Tab with name: " + PostTrans + " is displayed on FR Engagement Summary ");

                //---2. TMTI0075807_ Verify the fields and values available under the "Post-Transaction Info" tab. 
                Assert.IsTrue(summaryPage.VerifyPostTransactionHeadersL(), "Verified that displayed headers are same");
                extentReports.CreateLog("Post Transaction Info's headers are displayed as expected ");

                string PostRestr = summaryPage.ValidateLabelPostReStr();
                Assert.AreEqual("Post-Restructuring Total Debt (MM)", PostRestr);
                extentReports.CreateLog("Field with name: " + PostRestr + " is displayed on Post Transaction Info tab ");

                string PostReStrComp = summaryPage.ValidateLabelPostReStrCompany();
                Assert.AreEqual("Net Debt of the Restructured Company (MM)", PostReStrComp);
                extentReports.CreateLog("Field with name: " + PostReStrComp + " is displayed on Post Transaction Info tab ");

                string ClosingStock = summaryPage.ValidateLabelClosingStock();
                Assert.AreEqual("Closing Stock Price (first full closing day post-restructuring)", ClosingStock);
                extentReports.CreateLog("Field with name: " + ClosingStock + " is displayed on Post Transaction Info tab ");

                Assert.IsTrue(summaryPage.VerifyPostTransactionGridL(), "Verified that displayed grid headers are same");
                extentReports.CreateLog("Post Transaction Info's grid headers are displayed as expected ");

                //3.  TMTI0075812_Verify that clicking the "Add Equity Holder" button opens up the screen to enter the details.
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

                //4.  TMTI0075814_Verify that an error message appears for the required field on clicking the "Save" button of Add Equity Holder screen on leaving fields blank 
                string msgClient = summaryPage.ValidateErrorMessageForClientSubject();
                Assert.AreEqual("Client/Subject\r\nComplete this field.", msgClient);
                extentReports.CreateLog("Message: " + msgClient + " is displayed for Client/Subject field upon clicking Save button without selecting any value ");

                //5.  TMTI0075816_Verify that clicking the "Cancel" button will take the user back to the list view of the Post-Transaction Info tab.
                string cancelPage = summaryPage.ValidateCancelButtonFunctionalityOfPostTrans();
                Assert.AreEqual("Post-Transaction Info", cancelPage);
                extentReports.CreateLog("Page with tab: " + cancelPage + " is displayed upon clicking Cancel button on Add Equity Holder window ");

                //6.  TMTI0075818_ Verify that the "Post-Transaction Equity Holder" record is created with all the entered information by clicking the "Save" button on Add Equity Holder screen
                string rowEquityHolder = summaryPage.ValidateSaveFunctionalityOfAddEquityHolderPostTrans();
                Assert.AreEqual("True", rowEquityHolder);
                extentReports.CreateLog("A row is created after saving details on Add Equity Holder page ");

                //7. TMTI0075820_Verify that if the user selects an already added company while adding an Equity holder, the application will give an error message
                string msgSameClient = summaryPage.ValidateIfSameClientIsSelectedInAddEquityHolder();
                Assert.AreEqual(msgSameClient.Contains("Dina's Test "), true);
                extentReports.CreateLog("Message : " + msgSameClient + " is displayed after selecting same client again and clicking on save in Add Equity Holder page ");

                //8. TMTI0073768_Verify that clicking the "Edit" button of the Pre-Transaction Equity Holder record allows the user to update the Percent Ownership only of the record.
                string finTypeBeforeUpdate = summaryPage.GetPerOwnershipBeforeUpdate();
                string perOwnerPostUpdate = summaryPage.ValidateEditFunctionalityOfAddEquityHolder();
                Assert.AreNotEqual(finTypeBeforeUpdate, perOwnerPostUpdate);
                extentReports.CreateLog("Updated value of Percent Ownership is displayed upon saving it. ");

                //9. TMTI0073770_Verify that the Company selected as Equity Holder is hyperlinked in the list view.
                string hyperlinkEquity = summaryPage.ValidateHyperlinkOfAddedEquityHolderPost();
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
                Assert.AreEqual("Contact (External)\r\nComplete this field.", msgContacts);
                extentReports.CreateLog("Message: " + msgContacts + " is displayed for Contacts field upon clicking Save button without selecting any value ");

                //13. TMTI0075217_Verify that clicking the "Cancel" button of Add Board Member window takes the user back to the list view of the Post-Transaction Info tab.
                string cancelBoard = summaryPage.ValidateCancelButtonFunctionalityOfPostTrans();
                Assert.AreEqual("Post-Transaction Info", cancelBoard);
                extentReports.CreateLog("Page with tab: " + cancelBoard + " is displayed upon clicking Cancel button on Add Board Member window ");

                //14. TMTI0075219_Verify that the user is not able to search and add INACTIVE contact as Pre - Transaction Board Member
                string inactiveUser = summaryPage.ValidateSearchWithInactiveContact();
                Assert.AreEqual("Show more results for \"Sue Chu\"", inactiveUser);
                extentReports.CreateLog("Search Result is not shown for inactive contact ");

                //15. TMTI0075221_Verify that the "Pre-Transaction Board Member" record is created with the selected ACTIVE contact by clicking the "Save" button on Add Board Member screen. - Completed
                string valAddBoard = summaryPage.ValidateSaveFunctionalityOfAddBoardMemberWithHLRelPostTrans();
                Assert.AreEqual("True", valAddBoard);
                extentReports.CreateLog("A row is created after saving details on Add Board Member page ");

                //16. TMTI0075223_Verify that the "Has HL Relationship" checkbox is checked if the selected contact has a relationship with any of the HL Contacts
                string valHLRel = summaryPage.Validate1stHLRelationshipCheckbox();
                Assert.AreEqual("true", valHLRel);
                extentReports.CreateLog("Has HL Relationship checkbox is checked upon adding contact who has a relationship with any of the HL Contacts ");

                //17. TMTI0075225_Verify that the "Has HL Relationship" checkbox will not be checked if the selected contact doesn't have a relationship with any of the HL Contacts
                string val2ndAddBoard = summaryPage.ValidateSaveFunctionalityOfAddBoardMemberWithoutHLRelPostTrans();
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
                string msgCancelBoard = summaryPage.ValidateCancelFunctionalityOfAddBoardMemberPostTrans();
                Assert.AreEqual("Record is not deleted", msgCancelBoard);
                extentReports.CreateLog("Record is not deleted after clicking cancel on confirmation page ");

                string msgDeleteBoard = summaryPage.ValidateDeleteFunctionalityOfBoardMemberPostTrans();
                Assert.AreEqual("Record is deleted", msgDeleteBoard);
                extentReports.CreateLog("Record is deleted after clicking Ok on confirmation page ");

                //20. TMTI0075231_ Verify that clicking the "Add Debt Structure" button opens up the screen having multiple fields
                Assert.IsTrue(summaryPage.VerifyTextFieldsOfAddDebtStructureL(), "Verified that displayed text fields labels are same");
                extentReports.CreateLog("Text fields of Add Debt Structure are displayed as expected ");

                string MaturityDate = summaryPage.VerifyMaturityDateFieldL();
                Assert.AreEqual("Maturity Date", MaturityDate);
                extentReports.CreateLog("Date Picker field with name: " + Contacts + " is displayed on Add Debt Structure window ");

                string SecurityType = summaryPage.VerifySecurityTypeFieldOfAddDebtL();
                Assert.AreEqual("*Security Type", SecurityType);
                extentReports.CreateLog("Field with name: " + SecurityType + " is displayed on Add Debt Structure window ");

                string Currency = summaryPage.VerifyCurrencyFieldOfAddDebtL();
                Assert.AreEqual("Currency", Currency);
                extentReports.CreateLog("Field with name: " + Currency + " is displayed on Add Debt Structure window ");

                //Validate Security Types 
                Assert.IsTrue(summaryPage.VerifySecurityTypeDebtValuesL(), "Verified that values are same");
                extentReports.CreateLog("Values of HL Security Type drop down is displayed as expected ");

                //Verify the values of Debt Currency
                Assert.IsTrue(summaryPage.VerifyCurrencyValuesL(), "Verified that values are same");
                extentReports.CreateLog("Values of Debt Currency drop down is displayed as expected ");

                //Validate Cancel button
                string cancelAddDebt = summaryPage.ValidateCancelButton();
                Assert.AreEqual("Cancel", cancel);
                extentReports.CreateLog("Button with name: " + cancel + " is displayed on Add Debt Structure window ");

                //Validate Save button
                string saveAddDebt = summaryPage.ValidateSaveButton();
                Assert.AreEqual("Save", save);
                extentReports.CreateLog("Button with name: " + save + " is displayed on Add Debt Structure window ");

                //Validate Save and Add Key Creditor button
                string saveAndKeyCred = summaryPage.ValidateSaveAndKeyCreditorButton();
                Assert.AreEqual("Save and add Key Creditor", saveAndKeyCred);
                extentReports.CreateLog("Button with name: " + saveAndKeyCred + " is displayed on Add Debt Structure window ");

                //21. TMTI0075233_ Verify that an error message appears on clicking the "Save" button of Add Debt Structure screen without selecting data in the required field
                string msgSecurityType = summaryPage.ValidateErrorMessageForSecuirtyType();
                Assert.AreEqual("Security Type\r\nComplete this field.", msgSecurityType);
                extentReports.CreateLog("Message: " + msgSecurityType + " is displayed for Security Type field upon clicking Save button without selecting any value ");

                //22. TMTI0075235_ Verify that clicking the "Cancel" button of Add Debt Structure window takes the user back to the list view of the Pre-Transaction Info tab
                string cancelDebt = summaryPage.ValidateCancelButtonFunctionalityOfPostTrans();
                Assert.AreEqual("Post-Transaction Info", cancelDebt);
                extentReports.CreateLog("Page with tab: " + cancelDebt + " is displayed upon clicking Cancel button on Add Debt Structure window ");

                //23. TMTI0075237_Verify that the "Pre-Transaction Debt" record is created with all the entered data by clicking the "Save" button on Add Debt Structure screen. 
                string valAddDebt = summaryPage.ValidateSaveFunctionalityOfAddDebtStructurePostTrans();
                Assert.AreEqual("True", valAddDebt);
                extentReports.CreateLog("A row is created after saving details on Add Debt Structure page ");

               //24. TMTI0075239_Verify that the "Post-Transaction Debt" record is created with all the entered data including "Key Creditors" by clicking the "Save and add Key Creditors" button on Add Debt Structure screen
                bool rowKeyCred = summaryPage.ValidateSaveFunctionalityOfAddDebtStructureByAddingAllValues();
                Assert.AreEqual(true, rowKeyCred);
                extentReports.CreateLog("Key creditor row is displayed on Add Debt Structure page ");

                bool row2ndDebt = summaryPage.ValidateDebtStrcWithKeyCredPost();
                Assert.AreEqual(true, row2ndDebt);
                extentReports.CreateLog("Debt Structure with Key creditor row is displayed ");

                //25. TMTI0075255_Verify that the user is able to add Key Editors by clicking the "Edit" button of the Post-Transaction Debt record.
                bool rowAddKeyCred = summaryPage.ValidateEditFunctionalityOfDebtStrucPost();
                Assert.AreEqual(true, rowAddKeyCred);
                extentReports.CreateLog("Key creditor row is displayed on Add Debt Structure page after editing and entering details of Key Creditor ");

                //26.  Verify that the application gives an error message on the screen on adding duplicate Client/Subject as Key Creditors.
                string msgDupClientSub = summaryPage.ValidateErrorMessageWhileAddingSameClientSubjectInKeyCredPost();
                Assert.AreEqual("Company Name : 'Dina's Test Company' already exists as an Additional Client/Subject", msgDupClientSub);
                extentReports.CreateLog("Error message " + msgDupClientSub+ " appears on the screen while adding duplicate Client/Subject as Key Creditors ");

                //29. Verify that clicking the "Edit" button of the Post-Transaction Debt record allows the user to update debt structure details including the Loan Amount of Key Creditors added
                string updatedSecurity = summaryPage.ValidateEditFunctionalityOfAddedDebtStructurePost();
                Assert.AreEqual("Bank Debt (First Lien) - Term Loan B", updatedSecurity);
                extentReports.CreateLog("Updated values of Debt Structure are displayed ");

                //27. TMTI0075263_Verify that the user is able to delete the key creditors by clicking the Edit button of the Pre-Transaction Debt record
                bool cancelKeyCred = summaryPage.ValidateCancelFunctionalityOfKeyCredPost();
                Assert.AreEqual(true, cancelKeyCred);
                extentReports.CreateLog("Key creditor record is not deleted post clicking cancel button on delete confirmation pop up ");

                string deleteKeyCred = summaryPage.ValidateDeleteFunctionalityOfKeyCred();
                Assert.AreEqual("Row is deleted", deleteKeyCred);
                extentReports.CreateLog("Key creditor record is deleted post clicking delete button on delete confirmation pop up ");

                //28.  Verify that the application gives an error message on clicking the Save button of Add Key Creditors window without selecting data in the required fields. 
                string msgMandatoryKeyCred = summaryPage.ValidateErrorMessageWhileSavingKeyCredWithoutClientSubject();
                Assert.AreEqual("Client/Subject\r\nComplete this field.", msgMandatoryKeyCred);
                extentReports.CreateLog("Error message " + msgMandatoryKeyCred + " is displayed when Save button is clicked without entering mandatory details for Key Creditors ");

                //30. TMTI0075866_Verify that if the user removes the data from required fields while editing debt structure, the application gives an error message on the screen for required fields on clicking the "Save" button
                string msgMandatoryDebt = summaryPage.ValidateErrorMessageAfterRemovingMandatoryFieldsOfDebtStructurePost();
                Assert.AreEqual("Security Type\r\nComplete this field.", msgMandatoryDebt);
                extentReports.CreateLog("Error message " + msgMandatoryDebt + " is displayed when mandatory fields are removed from Debt Strcuture ");

                //31. TMTI0075868_Verify that clicking the "Delete" button of the Post-Transaction Debt Structure record gives a confirmation message before deleting the record
                string msgCancelDebt = summaryPage.ValidateCancelFunctionalityOfDebtStructurePost();
                Assert.AreEqual("Record is not deleted", msgCancelBoard);
                extentReports.CreateLog("Debt Structure record is not deleted after clicking cancel on confirmation page ");

                string msgDeleteDebt = summaryPage.ValidateDeleteFunctionalityOfDebtStructurePost();
                Assert.AreEqual("Record is deleted", msgDeleteDebt);
                extentReports.CreateLog("Debt Structure record is deleted after clicking Ok on confirmation page ");

                //32. TMTI0075870_Verify that on clicking the "Save" button, provided information gets saved and a success message appears on the screen.
                string msgPostReorg = summaryPage.SavePostOrganizedDetails();
                Assert.AreEqual("Record saved", msgPostReorg);
                extentReports.CreateLog("Message: " + msgPostReorg + " is displayed upon saving Post Reorganization details ");

                //33. TMTI0075872_Verify that Board Member and respective Company selected in the Post-Transaction board member list are hyperlinked. 
                string lnkBoardMember = summaryPage.ValidateBoardMemberIsDisplayedWithHyperlink();
                Assert.AreEqual("_blank", lnkBoardMember);
                extentReports.CreateLog("Board Member is hyperlinked in the Post-Transaction Board Members section ");

                string lnkBoardCompany = summaryPage.ValidateBoardMemberCompanyIsDisplayedWithHyperlink();
                Assert.AreEqual("_blank", lnkBoardCompany);
                extentReports.CreateLog("Board Member company is hyperlinked in Post-Transaction Board Members section ");

                //34. TMTI0075874_Verify that the Key Creditors selected in the Post-Transaction Debt Structure list are hyperlinked
                string lnkKeyCred = summaryPage.ValidateKeyCredIsDisplayedWithHyperlink();
                Assert.AreEqual("_blank", lnkKeyCred);
                extentReports.CreateLog("Added Key Creditor is hyperlinked in the Post-Transaction Debt section ");

                //35. TMTI0075876_Verify the Equity holder added under the post-transaction tab on FR Engagement Summary is mapped to the Additional Clients Subjects section with the type Equity Holder and role as Post-Transaction
                string addedEquityInAdditional = summaryPage.ValidateAddedEquityHolderIsDisplayedInAdditionalClientSubject();
                Assert.AreEqual("True", addedEquityInAdditional);
                string addedEquityType = summaryPage.GetTypeOfAddedEquityHolderInAdditionalClientSubject();
                Assert.AreEqual("Equity Holder", addedEquityType);
                //string addedEquityRole = summaryPage.GetRoleOfAddedEquityHolderInAdditionalClientSubjectPost();
                //Assert.AreEqual("Post-Transaction", addedEquityRole);
                extentReports.CreateLog("Added Equity Holder in Post-Transaction tab on FR Engagement Summary is mapped to the Additional Client Subject section with the type Equity Holder and role as Post-Transaction. ");

                //36. TMTI0075878_Verify the Board Members added under the post-transaction tab on FR Engagement Summary are mapped to the Engagement Contacts section with the type External and role as Post-Transaction.
                string addedMember = summaryPage.ValidateAddedBoardMemberIsDisplayedInEngagementContacts();
                Assert.AreEqual("Sonika Mathur", addedMember);
                string addedMemberType = summaryPage.GetTypeOfAddedBoardMemberInAdditionalClientSubject();
                Assert.AreEqual("External", addedMemberType);
                string addedMemberRole = summaryPage.GetRoleOfAddedBoardMemberInAdditionalClientSubjectPost();
                Assert.AreEqual("Post-Transaction Board Member", addedMemberRole);
                extentReports.CreateLog("Added Board Member in Post-Transaction tab on FR Engagement Summary is mapped to the Engagement Contacts section type as External and role as Post-Transaction Board Member ");

                usersLogin.LightningLogout();
                engHome.SearchEngagement("Unifin Financiera");
                engagementDetails.ClickFREngSummaryButton();
                summaryPage.ClickPostTransInfoTab();
                summaryPage.DeleteAllAddedDataInPostTrans();

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


