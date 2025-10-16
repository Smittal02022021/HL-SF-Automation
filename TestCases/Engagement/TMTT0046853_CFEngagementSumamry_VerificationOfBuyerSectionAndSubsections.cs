using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Data;
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
                Console.WriteLine("type:" + type);

                //Fetch the IG, sector and Client Ownership from Company of Closing Info tab                
                string IG = engagementDetails.GetIGvalueFromCompany();
                string Ownership = engagementDetails.GetOwnershipFromCompany();
                string sector = engagementDetails.GetSectorFromCompany();
                string compType = engagementDetails.GetCompanyTypeFromCompany();

                //1.  TMTI0114566_ Verify that the Buyer's basic information is displayed under the Parties Buyer section
                engagementDetails.ClickEngTab();
                engagementDetails.ClickCFEngsummaryButtonL();
                string secParties = summaryPage.ValidatePartiesSection();
                string secSeller = summaryPage.ValidateSellerSection();
                string secBuyer = summaryPage.ValidateBuyerSection();
                Assert.AreEqual("Buyer", secBuyer);
                extentReports.CreateLog("Section with name: " +secBuyer +" is displayed after clicking Buyer section ");

                string companyBuyer = summaryPage.ValidateCompanyOfBuyer();
                Console.WriteLine("companyBuyside" + companyBuyer);
                string typeBuyer = summaryPage.ValidateTypeOfBuyer();
                Console.WriteLine("typeBuyside" + typeBuyer);
                Assert.AreEqual(company, companyBuyer);
                Assert.AreEqual(type, typeBuyer);
                extentReports.CreateLog("Company: " + companyBuyer + " and Type: " + typeBuyer + " are mapped to Company and Type of Winning Counterparty if the deal is of Sellside ");

                string IGBuyer = summaryPage.ValidateIGOfBuyer();
                string OwnershipBuyer = summaryPage.ValidateOwnershipOfBuyer();
                Assert.AreEqual(IG, IGBuyer);
                Assert.AreEqual(Ownership, OwnershipBuyer);               
                extentReports.CreateLog("Industry Group: " + IGBuyer + " and Ownership: " + OwnershipBuyer + " are mapped to IG and Ownership of Winning Counterparty's Company ");

                string reqField = summaryPage.ValidateMandatoryValidationOfBuyerCompany();
                Assert.AreEqual("Company", reqField);
                extentReports.CreateLog("Mandatory Field " + reqField + " is displayed upon mover hover on Buyer ");

                //2.	TMTI0114575_ Verify that the "Buyer's Background" information is displayed under the subsection Buyer Background
                string sectorBuyside = summaryPage.ValidateSectorOfBuyer();
                Assert.AreEqual(sector, sectorBuyside);
                extentReports.CreateLog("Sector: " + sectorBuyside + " is mapped to Sector of Winning Counterparty's Company ");

                Assert.AreEqual("Operating Company", compType);
                extentReports.CreateLog("For Company Type: " + compType + " Buyer Type: " + typeBuyer + "is displayed under Buyer Section on CF Engagement Summary page ");

                //3.  TMTI0114573_Verify that the "Buyer's Strategy" information is displayed under the subsection of the Buyer section
                string secBuyerStrategy = summaryPage.ValidateStrategySectionOfBuyer();
                Assert.AreEqual("Buyer Strategy", secBuyerStrategy); 
                extentReports.CreateLog("Subsection: " + secBuyerStrategy + " is displayed under Parties section ");

                Assert.IsTrue(summaryPage.VerifyFieldsUnderBuyerStrategySection(), "Verify that displayed fields under Buyer Strategy section are same");
                extentReports.CreateStepLogs("Passed", "Displayed fields under Buyer Strategy section are as expected ");

                string messageBuyerStrategy1 = summaryPage.ValidateMandatoryField1OfBuyerStrategy();
                Assert.AreEqual("Buyer Process Type", messageBuyerStrategy1);
                extentReports.CreateLog("Mandatory Validation 1: " + messageBuyerStrategy1 + " is displayed upon hovering mouse at Buyer Strategy ");

                string messageBuyerStrategy2 = summaryPage.ValidateMandatoryField2OfBuyerStrategy();
                Assert.AreEqual("Buyer Platform Type", messageBuyerStrategy2);
                extentReports.CreateLog("Mandatory Validation 2: " + messageBuyerStrategy2 + " is displayed upon hovering mouse at Buyer Strategy ");

                string valProcessType = summaryPage.GetValueOfBuyerProcessType();
                string valPlatformType = summaryPage.GetValueOfBuyerPlatformType();

                Assert.IsTrue(summaryPage.VerifyValuesOfBuyerProcessType(), "Verify that displayed values of Buyer Process Type are same");
                extentReports.CreateStepLogs("Passed", "Displayed  values of Buyer Process Type are as expected ");

                Assert.IsTrue(summaryPage.VerifyValuesOfBuyerPlatformType(), "Verify that displayed values of Buyer Platform Type are same");
                extentReports.CreateStepLogs("Passed", "Displayed  values of Buyer Platform Type are as expected ");

                //4.  TMTI0114572_Verify the Edit functionality of the Buyer's Strategy section
                //Validate the cancel functinality
                string cancelProcess = summaryPage.ValidateCancelFunctionalityOfBuyerStrategySection("Controlled Auction","Bolt-On");
                string cancelPlatform = summaryPage.GetValueOfBuyerPlatformType();
                
                Assert.AreEqual(valProcessType, cancelProcess);
                Assert.AreEqual(valPlatformType, cancelPlatform);
                extentReports.CreateLog("Buyer Strategy's section  details are not saved post clicking Cancel button ");

                //Validate the Edit functinality
                string editProcess = summaryPage.ValidateEditFunctionalityOfBuyerStrategySection("Controlled Auction", "Bolt-On");
                string editPlatform = summaryPage.GetValueOfBuyerPlatformType();

                Assert.AreNotEqual(valProcessType, editProcess);
                Assert.AreNotEqual(valPlatformType, editPlatform);
                extentReports.CreateLog("Buyer Strategy's section  details are updated post clicking Save button ");

               summaryPage.ValidateEditFunctionalityOfBuyerStrategySection("Broad Auction", "Platform");

                //5.  TMTI0114570_Verify the "Buyer Contacts" displayed under the Buyer's subsection
                string iconAddRecordContact = summaryPage.ValidateAddRecordBuyerContactIcon();
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

                //6.   TMTI0114565_Verify that the fields "Engagement Contacts Check" and "Engagement Contact No Attorney" are displayed
                string iconEngContactCheck = summaryPage.ValidateEngContactCheckIconBuyer();
                Assert.AreEqual("Indicates if required Engagement Contacts for Buyer are present (an Attorney contact along with Company Contact and/or Board of Directors contacts are required)", iconEngContactCheck);
                extentReports.CreateLog("Message: " + iconEngContactCheck + " is displayed upon hovering Engagement Contacts Buyer Check ");

                string iconEngContactNoCheck = summaryPage.ValidateEngContactAtorneyCheckIconBuyer();
                Assert.AreEqual("Please \"check\" if a Buyer Attorney was not required for this Engagement", iconEngContactNoCheck);
                extentReports.CreateLog("Message: " + iconEngContactNoCheck + " is displayed upon hovering Engagement Contact Buyer No Attorney checkbox ");

                //7.   TMTI0114574 _Verify that the fields "Engagement Contacts Check" and "Engagement Contact No Attorney" default to being unchecked, and "Engagement Contacts Check" gets checked if and only if the contacts with role Attorney and Company Contact / Board Of Directors are present on the Buyer Contacts
                //8.   TMTI0114571_Verify the Add Record functionality on the Buyer Contact section
                string checkboxEngContact = summaryPage.ValidateEngContactBuyerCheckbox();
                Assert.AreEqual("Engagement Contacts Buyer Check checkbox is displayed and not-checked", checkboxEngContact);
                extentReports.CreateLog("Engagement Contacts Buyer Check checkbox is not checked by default ");

                string checkboxEngContactAttorney = summaryPage.ValidateEngContactBuyerNoAttorneyCheckbox();
                Assert.AreEqual("Engagement Contact Buyer No Attorney checkbox is displayed and not-checked", checkboxEngContactAttorney);
                extentReports.CreateLog("Engagement Contact Buyer No Attorney checkbox is not checked by default ");

                //11.  TMTI0114569_Verify the lookup search functionality on the Buyer's Contact  Add/Edit record section
                string addedContact = summaryPage.ValidateSaveBuyerContactFunctionality("Sonika Goyal", "Board of Directors");
                
                string checkboxEngContactNoAttorney = summaryPage.ValidateEngContactBuyerCheckbox();
                Assert.AreEqual("Engagement Contacts Buyer Check checkbox is displayed and not-checked", checkboxEngContactNoAttorney);
                extentReports.CreateLog("Engagement Contacts Buyer Check checkbox is not checked when Contact with only Board of Directors role is added ");

                string checkboxEngAttorneyWithNoAttorney = summaryPage.ValidateEngContactBuyerNoAttorneyCheckbox();
                Assert.AreEqual("Engagement Contact Buyer No Attorney checkbox is displayed and not-checked", checkboxEngAttorneyWithNoAttorney);
                extentReports.CreateLog("Engagement Contact Buyer No Attorney checkbox is not checked when Contact with only Board of Directors role is added ");

                string contactEng = engagementDetails.ValidateContactDisplayedInEng();
                Assert.AreEqual("Sonika Goyal", contactEng);
                string roleEng = engagementDetails.GetRoleOfContactInEng("Sonika Goyal");
                Assert.AreEqual("Board of Directors", roleEng);
                extentReports.CreateLog("Contact: " + addedContact + " with role: " + roleEng + " is displayed under Engagement Contacts after adding in Buyer Contacts ");

                //----Add Contact in Engagement and validate the same in Engagement summary 
                engagementDetails.ClickAddCFOppContact();
                string contactAddedInEng = engagementDetails.CreateContactCFL("Vijay Kumar", "Attorney", "Buyer");
                string roleContactAddedinEng = engagementDetails.GetRoleOfContactInEng("Vijay Kumar");

                //----Get added Contact and role of Buyer Contact section
                string contactSummary = summaryPage.ValidateAddedBuyerContactInEng();
                Console.WriteLine("contactSummary" + contactSummary);
                string roleSummary = summaryPage.GetRoleOfBuyerContactAddedInEng("Vijay Kumar");
                Assert.AreEqual(contactAddedInEng, contactSummary);
                Assert.AreEqual(roleContactAddedinEng, roleSummary);
                extentReports.CreateLog("Contact: " + contactSummary + " with role: " + roleSummary + " is displayed under Engagement Contacts after adding in Engagement Contacts ");

                string checkboxEngContactBoard = summaryPage.ValidateEngContactBuyerCheckbox();
                Assert.AreEqual("Engagement Contacts Buyer Check checkbox is displayed and checked", checkboxEngContactBoard);
                extentReports.CreateLog("Engagement Contacts Buyer Check checkbox is checked when Contact with both Attorney and Board of Directors role is added ");

                string checkboxEngContactNoAttorneyBoard = summaryPage.ValidateEngContactBuyerNoAttorneyCheckbox();
                Assert.AreEqual("Engagement Contact Buyer No Attorney checkbox is displayed and not-checked", checkboxEngContactNoAttorneyBoard);
                extentReports.CreateLog("Engagement Contact Buyer No Attorney checkbox is not checked even when Contact with both Attorney and Board of Directors role is added ");
                                
                //9.  TMTI0114564_Verify the Edit Record functionality of the Buyer Contact   
                //--Validate Cancel functionality
                string cancelContact = summaryPage.ValidateCancelBuyerContactFunctionality("Mr. Sonika Goyal", "Attorney");
                Assert.AreEqual(roleEng, cancelContact);
                extentReports.CreateLog("Contact details are not updated under Seller Contacts section after clicking Cancel button ");

                string editContact = summaryPage.ValidateEditBuyerContactFunctionality("Mr. Sonika Goyal", "Attorney");
                Assert.AreNotEqual(roleEng, editContact);
                extentReports.CreateLog("Contact details with Role: " + editContact + " is updated under Seller Contacts section after clicking Save button ");

                //10.	TMTI0114563_Verify the "Delete" functionality on the Buyer Contacts
                //Cancel Delete Functionality----
                string cancelDeleteContact = summaryPage.ValidateCancelDeleteFunctionalityOfBuyerContact();
                Assert.AreEqual(addedContact, cancelDeleteContact);
                extentReports.CreateLog("Contact is not deleted after clicking Cancel button on Delete Confirmation pop up window ");

                //Confirm Delete Functionality----
                string confirmDeleteContact = summaryPage.ValidateConfirmDeleteFunctionalityOfBuyerContact();
                Assert.AreNotEqual(addedContact, confirmDeleteContact);
                extentReports.CreateLog("Contact:" + addedContact + " is deleted after clicking Ok button on Delete Confirmation pop up window ");

                //12.  TMTI0114568_Verify that the Attorney role requirement is bypassed when the "Engagement Contact Buyer No Attorney" checkbox is checked
                summaryPage.ValidateEditBuyerContactFunctionality("Vijay Kumar", "Board of Directors");
                string checkboxEngContactWithNoAttorneyChecked = summaryPage.ValidateEngContactBuyerCheckboxAfterAttorneyChecked();
                Assert.AreEqual("Engagement Contacts Buyer Check checkbox is displayed and checked", checkboxEngContactWithNoAttorneyChecked);
                extentReports.CreateLog("Engagement Contacts Buyer Check checkbox is now checked when Engagement Contact Buyer No Attorney checkbox is checked ");

                string checkboxEngContactWithNoAttorneyNotChecked = summaryPage.ValidateEngContactBuyerCheckboxAfterAttorneyChecked();
                Assert.AreEqual("Engagement Contacts Buyer Check checkbox is displayed and not checked", checkboxEngContactWithNoAttorneyNotChecked);
                extentReports.CreateLog("Engagement Contacts Buyer Check checkbox is not checked when Engagement Contact Buyer No Attorney checkbox is not checked ");

                //13.   

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


