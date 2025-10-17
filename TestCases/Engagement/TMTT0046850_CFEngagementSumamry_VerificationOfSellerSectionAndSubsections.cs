using AventStack.ExtentReports.Gherkin.Model;
using Microsoft.Office.Interop.Excel;
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
    class TMTT0046850_CFEngagementSumamry_VerificationOfSellerSectionAndSubsections : BaseClass
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
        public void VerifyTheInformationUnderEngagementInformationTab()
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

                //1. TMTI0114550_Verify the availability of sections "Seller" and "Buyer" are displayed under Parties
                engagementDetails.ClickCFEngsummaryButtonL();
                string secParties = summaryPage.ValidatePartiesSection();
                string secSeller = summaryPage.ValidateSellerSection();
                string secBuyer = summaryPage.ValidateBuyerSection();

                Assert.AreEqual("Parties", secParties);
                Assert.AreEqual("Seller", secSeller);
                Assert.AreEqual("Buyer", secBuyer);
                extentReports.CreateLog("Sub sections " + secSeller + " and " + secBuyer + " is displayed under section " + secParties + " on CF Engagement Summary page ");

                //2. TMTI0114547_Verify the Seller's basic information is displayed and mapped to the fields on the corresponding Engagement
                Assert.IsTrue(summaryPage.VerifyFieldsUnderSellerSection(), "Verify that displayed fields under Seller section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields under Seller section are as expected for job Type: " + valJobType + " ");

                string iconSeller = summaryPage.ValidateSellerIcon();
                Assert.AreEqual("success", iconSeller);
                extentReports.CreateLog("Green tick is displayed on section " + secSeller + " indicating the field information is pre-populated ");

                //3.  TMTI0114551_Seller - Verify that the "Seller's Background" information is displayed under the subsection Seller Background
                Assert.IsTrue(summaryPage.VerifyFieldsUnderSellerBackGroundSection(), "Verify that displayed fields under Seller Background section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields under Seller Background section are as expected for job Type: " + valJobType + " ");

                string IG = summaryPage.ValidateIGValueInSellerBackground();
                string Sector = summaryPage.ValidateSectorValueInSellerBackground();
                string Desc = summaryPage.ValidateDescriptionValueInSellerBackground();
                Console.WriteLine("Desc " + Desc);

                string industryGroup = summaryPage.ValidateIGValueOfCompany();
                Assert.AreEqual(industryGroup, IG);
                extentReports.CreateLog("Industry Group " + industryGroup + " value is mapped with Company's Industry Group ");

                string sector = summaryPage.ValidateSectorValueOfCompany();
                Assert.AreEqual(Sector, sector);
                extentReports.CreateLog("Sector " + sector + " value is mapped with Company's Sector ");

                string description = summaryPage.ValidateDescriptionValueOfCompany();
                Assert.AreEqual(Desc, description);
                extentReports.CreateLog("Description " + description + " value is mapped with Company's Description ");

                //4.   TMTI0114555_Verify that the "Seller's Details" information is displayed under the subsection Seller Details.
                Assert.IsTrue(summaryPage.VerifyFieldsUnderSellerDetailsSection(), "Verify that displayed fields under Seller Details section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields under Seller Details section are as expected for job Type: " + valJobType + " ");

                string iconSellerDetails = summaryPage.ValidateSellerDetailsIcon();
                Assert.AreEqual("Transaction Rationale", iconSellerDetails);
                extentReports.CreateLog("Mandatory field " + iconSellerDetails + " is displayed upon clicking Seller Details icon ");

                string txnRationale = summaryPage.GetValueOfTxnRationale();

                Assert.IsTrue(summaryPage.VerifyTxnRationaleValues(), "Verify that displayed values of Transaction Rationale are same");
                extentReports.CreateStepLogs("Passed", "Displayed values of Transaction Rationale are as expected ");

                //5. TMTI0114556_Verify the "Edit" functionality on the Seller Details of the CF Engagement Summary
                //---Validate Cancel functionality of Selller details
                string cancelTxnRationale = summaryPage.ValidateCancelFunctionalityOfSellerDetailsSection();
                Console.WriteLine("cancelTxnRationale" + cancelTxnRationale);
                Assert.AreEqual(txnRationale, cancelTxnRationale);
                extentReports.CreateLog("Details are not saved upon clicking Cancel button in Seller Details button ");

                //-- Valdiate Save functionality of Seller details
                string saveTxnRationale = summaryPage.ValidateSaveFunctionalityOfSellerDetailsSection("Public - Hostile");
                Assert.AreNotEqual(cancelTxnRationale, saveTxnRationale);
                extentReports.CreateLog("Details are saved upon clicking Save button in Seller Details button ");

                summaryPage.ValidateSaveFunctionalityOfSellerDetailsSection("Public - Activist Shareholder");

                //6. TMTI0114540_Verify that the "Seller Financials" is displayed under subsection Seller Financials.
                string iconAddRecord = summaryPage.ValidateAddRecordIcon();
                Assert.AreEqual("Add Record", iconAddRecord);
                extentReports.CreateLog("Icon " + iconAddRecord + " is displayed in Seller Financials section ");

                Assert.IsTrue(summaryPage.VerifyAddRecordFields(), "Verify that displayed fields on Add Record section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields on Add Record section are as expected ");

                Assert.IsTrue(summaryPage.ValidateTypeValues(), "Verify that displayed Values of Type dropdown are same");
                extentReports.CreateStepLogs("Passed", "Displayed Values of Type dropdown are as expected ");

                //string messageType = summaryPage.ValidateTypeMessage();
                //Assert.AreEqual("Please indicate when these financial metrics were gathered.", messageType);
                //extentReports.CreateLog("Message: " + messageType + " is displayed on Type icon ");

                //7.  TMTI0114552_Verify the add record functionality for "Seller Financials".
                string Revenue1st = summaryPage.ValidateSaveFunctionalityOfAddRecord("10", "Final");
                Console.WriteLine("Revenue1st:" + Revenue1st);
                Assert.AreEqual("GBP 10.00", Revenue1st);
                extentReports.CreateLog("Revenue: " + Revenue1st + " is displayed after saving Add Record details ");

                summaryPage.ValidateAddRecordIcon();
                summaryPage.ValidateSaveFunctionalityOfAddRecord("20", "Closing");
                string Revenue2nd = summaryPage.GetAdded2ndRevenueInSellerFinanials();
                Assert.AreEqual("GBP 20.00", Revenue2nd);
                extentReports.CreateLog("Revenue: " + Revenue2nd + " is displayed after saving Add Record details ");

                //11. TMTI0114553_Verify that the Engagement Financials Check field is unchecked by default and gets checked only when the First, Final, and Closing financials are present
                string checkboxEngFin = summaryPage.ValidateEngFinCheckbox();
                Assert.AreEqual("Engagement Financials Check checkbox is displayed and not-checked", checkboxEngFin);
                extentReports.CreateLog("Engagement Financials Check checkbox is not checked ");

                Assert.IsTrue(summaryPage.ValidateAddedRecordsInEngDetails(), "Verify that added Revenue records are on Add Record section are same");
                extentReports.CreateStepLogs("Passed", "Added Revenue records in Seller Financials are displayed as expected ");

                string addedFin = summaryPage.ValidateAddFinancialsFunctionalityOfEngagement();
                Console.WriteLine("addedFin:" + addedFin);

                //Validate added record in Engagement is displayed in Eng Summary
                string Revenue3rd = summaryPage.ValidateAddFinancialsInCFEngSummary();
                Assert.AreEqual(addedFin, Revenue3rd);
                extentReports.CreateLog("Financal record added in Engagement is displayed under Seller Financials section of CF Engagement Summary ");

                //11 -- after entering Final Type
                string checkboxEngFinFinal = summaryPage.ValidateEngFinCheckbox();
                Assert.AreEqual("Engagement Financials Check checkbox is displayed and checked", checkboxEngFinFinal);
                extentReports.CreateLog("Engagement Financials Check checkbox is checked when Engagement Financials are entered for First, Final and Closing Type with Revenue LTM (MM) and EBITDA LTM (MM) ");

                //8. TMTI0114544_Verify the "Edit" functionality on the Seller Financials record. 
                //Cancel Functionality----
                string cancelRev = summaryPage.ValidateCancelFunctionalityOfAddRecord("35");
                Assert.AreEqual(Revenue3rd, cancelRev);
                extentReports.CreateLog("Revenue value is not updated after clicking cancel button post updating Revenue value in Edit Record window ");

                //Save Functionality----
                string editRev = summaryPage.ValidateEditFunctionalityOfAddRecord("35");
                Assert.AreNotEqual(Revenue3rd, editRev);
                extentReports.CreateLog("Revenue value : " + editRev + " is updated after clicking save button post updating Revenue value in Edit Record window ");

                //9. TMTI0114543_Verify the "Delete" functionality on the Seller Financials Delete Record
                //Cancel Delete Functionality----
                string cancelDelete = summaryPage.ValidateCancelDeleteFunctionalityOfAddRecord();
                Assert.AreEqual(editRev, cancelDelete);
                extentReports.CreateLog("Revenue record is not deleted after clicking Cancel button on Delete Confirmation pop up window ");

                //Confirm Delete Functionality----
                string confirmDelete = summaryPage.ValidateConfirmDeleteFunctionalityOfAddRecord();
                Assert.AreEqual("Record was deleted.", confirmDelete);
                extentReports.CreateLog("Revenue record is deleted after clicking Ok button on Delete Confirmation pop up window ");

                //To delete the 2nd record (for script workflow)
                summaryPage.ValidateConfirmDeleteFunctionalityOfAddRecord();
                summaryPage.ValidateConfirmDeleteFunctionalityOfAddRecord();

                //10. TMTI0114546 _Verify that the "Engagement Financial Check" field is required field. 
                string iconSellerFin = summaryPage.ValidateSellerFinIcon();
                string messageSellerFin = summaryPage.ValidateMandatoryMessageOfSellerFin();
                Assert.AreEqual("error", iconSellerFin);
                Assert.AreEqual("Engagement Financials Check", messageSellerFin);
                extentReports.CreateLog("Red asterisk is displayed on Seller financial section along with message: " + messageSellerFin + " upon hovering it ");

                string iconEngFinCheck = summaryPage.ValidateEngFinCheckIcon();
                Assert.AreEqual("Indicates if required Engagement Financials are present (First, Final, & Closing with Revenue LTM (MM) and EBITDA LTM (MM). For FIG, First, Final, & Closing with Book Value (Current), Total Assets, Net Income (LTM), or EBITDA (LTM))", iconEngFinCheck);
                extentReports.CreateLog("Message: " + iconEngFinCheck + " is displayed upon hovering Engagement Financials Check checkbox ");

                //12.	TMTI0114545_Verify that the "Seller Contacts" are displayed under the subsection of the Seller Financials 
                string iconAddRecordContact = summaryPage.ValidateAddRecordContactIcon();
                Assert.AreEqual("Add Record", iconAddRecordContact);
                extentReports.CreateLog("Icon " + iconAddRecordContact + " is displayed in Seller Contacts section ");

                Assert.IsTrue(summaryPage.VerifyAddRecordFieldsOfAddContact(), "Verify that displayed fields on Add Record section of Seller Contacts are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields on Add Record section of Seller Contacts are as expected ");

                Assert.IsTrue(summaryPage.ValidateTypeValuesOfSellerContacts(), "Verify that displayed Values of Type dropdown are same");
                extentReports.CreateStepLogs("Passed", "Displayed Values of Type dropdown are as expected ");

                Assert.IsTrue(summaryPage.ValidateRoleValuesOfSellerContacts(), "Verify that displayed Values of Role dropdown are same");
                extentReports.CreateStepLogs("Passed", "Displayed Values of Role dropdown are as expected ");

                string messageContact = summaryPage.ValidateMandatoryMessageOfContact();
                Assert.AreEqual("Contact\r\nComplete this field.", messageContact);
                extentReports.CreateLog("Message: " + messageContact + " is displayed on Contact field when save button is clickd without entering it ");

                //13.  TMTI0114542_ Verify that the "Seller Contact" lookup search functionality
                string addedContact = summaryPage.ValidateSaveContactFunctionality("Sonika Goyal", "Board of Directors");
                Assert.AreEqual("Mr. Sonika Goyal", addedContact);
                extentReports.CreateLog("Contact: " + addedContact + " is displayed under Seller Contacts section upon saving contact ");

                //18.  TMTI0114548_Verify that the fields "Engagement Contacts Check" and "Engagement Contact No Attorney" default to being unchecked and get checked if and only if the contacts with role Attorney and Company Contact / Board of Directors are present on the Seller Contacts
                string checkboxEngContact = summaryPage.ValidateEngContactSellerCheckbox();
                Assert.AreEqual("Engagement Contacts Seller Check checkbox is displayed and not-checked", checkboxEngContact);
                extentReports.CreateLog("Engagement Contacts Seller Check checkbox is not checked when Contact with only Board of Directors role is added ");

                string checkboxEngContactNoAttorney = summaryPage.ValidateEngContactSellerNoAttorneyCheckbox();
                Assert.AreEqual("Engagement Contact Seller No Attorney checkbox is displayed and not-checked", checkboxEngContactNoAttorney);
                extentReports.CreateLog("Engagement Contact Seller No Attorney checkbox is not checked when Contact with only Board of Directors role is added ");

                //14.  TMTI0114539_Verify that the Add Contact functionality is on the Seller Contact.
                string contactEng = engagementDetails.ValidateContactDisplayedInEng();
                Assert.AreEqual("Sonika Goyal", contactEng);
                string roleEng = engagementDetails.GetRoleOfContactInEng("Sonika Goyal");
                Assert.AreEqual("Board of Directors", roleEng);
                extentReports.CreateLog("Contact: " + addedContact + " with role: " + roleEng + " is displayed under Engagement Contacts after adding in Seller Contacts ");

                //14----Add Contact in Engagement and validate the same in Engagement summary 
                engagementDetails.ClickAddCFOppContact();
                string contactAddedInEng = engagementDetails.CreateContactCFL("Vijay Kumar", "Attorney","Seller");
                string roleContactAddedinEng = engagementDetails.GetRoleOfContactInEng("Vijay Kumar");

                //----Get added Contact and role of Seller Contact section
                string contactSummary = summaryPage.ValidateAddedContactInEng();
                Console.WriteLine("contactSummary" + contactSummary);
                string roleSummary = summaryPage.GetRoleOfContactAddedInEng("Vijay Kumar");
                Assert.AreEqual(contactAddedInEng, contactSummary);
                Assert.AreEqual(roleContactAddedinEng, roleSummary);
                extentReports.CreateLog("Contact: " + contactSummary + " with role: " + roleSummary + " is displayed under Engagement Contacts after adding in Engagement Contacts ");

                //18.  TMTI0114548_Verify that the fields "Engagement Contacts Check" and "Engagement Contact No Attorney" default to being unchecked and get checked if and only if the contacts with role Attorney and Company Contact / Board of Directors are present on the Seller Contacts
                string checkboxEngContactBoard = summaryPage.ValidateEngContactSellerCheckbox();
                Assert.AreEqual("Engagement Contacts Seller Check checkbox is displayed and checked", checkboxEngContactBoard);
                extentReports.CreateLog("Engagement Contacts Seller Check checkbox is checked when Contact with both Attorney and Board of Directors role is added ");

                string checkboxEngContactNoAttorneyBoard = summaryPage.ValidateEngContactSellerNoAttorneyCheckbox();
                Assert.AreEqual("Engagement Contact Seller No Attorney checkbox is displayed and not-checked", checkboxEngContactNoAttorneyBoard);
                extentReports.CreateLog("Engagement Contact Seller No Attorney checkbox is not checked even when Contact with both Attorney and Board of Directors role is added ");

                //19.	TMTI0114554_Verify that the Attorney role requirement is independent of the Seller Contacts
                string checkboxEngContactWithNoAttorney = summaryPage.ValidateEngContactSellerCheckboxAfterCheckingNoAttorney();
                Assert.AreEqual("Engagement Contacts Seller Check checkbox is displayed and checked", checkboxEngContactWithNoAttorney);
                extentReports.CreateLog("Engagement Contacts Seller Check checkbox is still checked when Engagement Contact Seller No Attorney checkbox is checked ");

                //15.  TMTI0114549_Verify the "Edit" functionality of the Seller Contact on the Seller Contacts.
                //--Validate Cancel functionality
                string cancelContact = summaryPage.ValidateCancelContactFunctionality("Mr. Sonika Goyal", "Attorney");
                Assert.AreEqual(roleEng, cancelContact);
                extentReports.CreateLog("Contact details are not updated under Seller Contacts section after clicking Cancel button ");

                string editContact = summaryPage.ValidateEditContactFunctionality("Mr. Sonika Goyal", "Attorney");
                Assert.AreNotEqual(roleEng, editContact);
                extentReports.CreateLog("Contact details with Role: " + editContact + " is updated under Seller Contacts section after clicking Save button ");

                //16. TMTI0114558_Verify the "Delete" functionality on the Seller Contacts
                //Cancel Delete Functionality----
                string cancelDeleteContact = summaryPage.ValidateCancelDeleteFunctionalityOfSellerContact();
                Assert.AreEqual(addedContact, cancelDeleteContact);
                extentReports.CreateLog("Contact is not deleted after clicking Cancel button on Delete Confirmation pop up window ");

                //Confirm Delete Functionality----
                string confirmDeleteContact = summaryPage.ValidateConfirmDeleteFunctionalityOfSellerContact();
                Assert.AreNotEqual(addedContact, confirmDeleteContact);
                extentReports.CreateLog("Contact:" + addedContact + " is deleted after clicking Ok button on Delete Confirmation pop up window ");

                //17.  TMTI0114541_Verify that the fields "Engagement Contacts Check" and "Engagement Contact No Attorney" fields are displayed on the Seller Contacts
                string iconEngContactCheck = summaryPage.ValidateEngContactCheckIcon();
                Assert.AreEqual("Indicates if required Engagement Contacts for Seller are present (an Attorney contact along with Company Contact and/or Board of Directors contacts are required)", iconEngContactCheck);
                extentReports.CreateLog("Message: " + iconEngContactCheck + " is displayed upon hovering Engagement Contacts Seller Check ");

                string iconEngContactNoCheck = summaryPage.ValidateEngContactAtorneyCheckIcon();
                Assert.AreEqual("Please \"check\" if a Seller Attorney was not required for this Engagement", iconEngContactNoCheck);
                extentReports.CreateLog("Message: " + iconEngContactNoCheck + " is displayed upon hovering Engagement Contact Seller No Attorney checkbox ");

                //20.	TMTI0114557_Verify that the Attorney role requirement is bypassed when the check box Engagement Contact No Attorney is checked
                summaryPage.ValidateEditContactFunctionality("Vijay Kumar", "Board of Directors");
                string checkboxEngContactWithAttorneyByPass = summaryPage.ValidateEngContactSellerCheckboxAfterAttorneyByPass();
                Assert.AreEqual("Engagement Contacts Seller Check checkbox is displayed and checked", checkboxEngContactWithAttorneyByPass);
                extentReports.CreateLog("Engagement Contacts Seller Check checkbox is still checked when Engagement Contact Seller No Attorney checkbox is checked and Attorney contact is not added ");
                summaryPage.ValidateConfirmDeleteFunctionalityOfSellerContact();

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


