using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Engagement
{
    class TMTT0017877_7888_7883_7884_7886_7882_7890_7887_7879_7880_View_Email_Access_Add_ExistingEng_ButtonControls_Delete_ViewAll : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        AddCounterparty counterparty = new AddCounterparty();
        public static string fileTC7877 = "TMTT0017877_LightningEngagement";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ViewCounterparties_EmailButton()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC7877;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as CAO User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 2);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(valUser, stdUser);
                extentReports.CreateLog("User: " + stdUser + " is able to login ");

                //Fetching all rows as per different Job Types
                int rowJobTypes = ReadExcelData.GetRowCount(excelPath, "Engagement");
                Console.WriteLine("rowUsers " + rowJobTypes);

                for (int row = 2; row <= rowJobTypes; row++)
                {
                    string valEngNum = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 1);
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", row, 2);

                    //Search for Engagement on lightning
                    string message = engHome.SearchEngagementWithNumberOnLightning(valEngNum, valJobType);
                    extentReports.CreateLog("Engagement details are displayed upon searching required engagement ");

                    //Validate the View Counterparties button
                    string viewCounterparty = engagementDetails.ValidateViewCounterpartiesButton(valJobType);
                    Assert.AreEqual("View Counterparties", viewCounterparty);
                    extentReports.CreateLog("Button with name : " + viewCounterparty + " is displayed on Engagement Details page for Job Type: " + valJobType);

                }

                //Click on Lightning Counterparties button, click on details and click on Eng Counterparty Contact
                engagementDetails.ClickViewCounterpartiesButton();

                //Validate Add Remove Columns, Delete, Cancel,Add Counterparties, Email, View all buttons
                string btnAddRemove = counterparty.ValidateAddRemoveColumnsButton();
                Assert.AreEqual("Add/Remove Columns", btnAddRemove);
                extentReports.CreateLog("Button with name : " + btnAddRemove + " is displayed on Counterparty Details page ");

                string btnDelete = counterparty.ValidateDeleteButton();
                Assert.AreEqual("Delete", btnDelete);
                extentReports.CreateLog("Button with name : " + btnDelete + " is displayed on Counterparty Details page ");

                string btnAddCounterparties = counterparty.ValidateAddCounterpartyButton();
                Assert.AreEqual("Add Counterparties", btnAddCounterparties);
                extentReports.CreateLog("Button with name : " + btnAddCounterparties + " is displayed on Counterparty Details page ");

                string btnBidTracking = counterparty.ValidateBidTrackingReportButton();
                Assert.AreEqual("Bid Tracking Report", btnBidTracking);
                extentReports.CreateLog("Button with name : " + btnBidTracking + " is displayed on Counterparty Details page ");

                string btnEditBids = counterparty.ValidateEditBidsButton();
                Assert.AreEqual("Edit Bids", btnEditBids);
                extentReports.CreateLog("Button with name : " + btnEditBids + " is displayed on Counterparty Details page ");

                string btnImport = counterparty.ValidateImportWithDataloaderButton();
                Assert.AreEqual("Import with Dataloader", btnImport);
                extentReports.CreateLog("Button with name : " + btnImport + " is displayed on Counterparty Details page ");

                string btnExport = counterparty.ValidateExportDataButton();
                Assert.AreEqual("Export Data", btnExport);
                extentReports.CreateLog("Button with name : " + btnExport + " is displayed on Counterparty Details page ");

                string btnEmail = counterparty.ValidateEmailButton();
                Assert.AreEqual("Email", btnEmail);
                extentReports.CreateLog("Button with name : " + btnEmail + " is displayed on Counterparty Details page ");

                string btnViewAll = counterparty.ValidateViewAllButton();
                Assert.AreEqual("View All", btnViewAll);
                extentReports.CreateLog("Button with name : " + btnViewAll + " is displayed on Counterparty Details page ");

                string txtSearch = counterparty.ValidateSearchTextBoxButton();
                Assert.AreEqual("Search", txtSearch);
                extentReports.CreateLog("Text box with name : " + txtSearch + " is displayed on Counterparty Details page ");

                //Get the value of existing company
                string valCompName = counterparty.GetExistingCompany();

                //Enter some data in counterparty records and click cancel and then validate the same
                string valDefType = counterparty.GetDefaultValueOfType();
                Console.WriteLine("valDefType", valDefType);
                string valDefTier = counterparty.GetDefaultValueOfTier();
                Console.WriteLine("valDefTier", valDefTier);

                string valCancelType = counterparty.SelectTypeTierAndClickCancel();
                Assert.AreEqual(valDefType, valCancelType);
                extentReports.CreateLog("Selected value of Type is not saved upon clicking cancel button on Counterparty Records page ");

                string valCancelTier = counterparty.GetDefaultValueOfTier();
                Assert.AreEqual(valDefTier, valCancelTier);
                extentReports.CreateLog("Selected value of Tier is not saved upon clicking cancel button on Counterparty Records page ");

                string valSaveType = counterparty.UpdateTypeTierAndClickSave();
                Assert.AreEqual("Financial", valSaveType);
                extentReports.CreateLog("Selected value of Type : " + valSaveType + " is saved upon clicking Save button on Counterparty Records page ");

                string valSaveTier = counterparty.GetDefaultValueOfTier();
                Assert.AreEqual("B", valSaveTier);
                extentReports.CreateLog("Selected value of Tier : " + valSaveType + " is saved upon clicking Save button on Counterparty Records page ");

                //Update some data in counterparty records, click cancel and validate it
                string valUpdCancelType = counterparty.UpdateTypeTierAndClickCancel();
                Assert.AreEqual(valSaveType, valUpdCancelType);
                extentReports.CreateLog("Updated value of Type is not saved upon clicking cancel button on Counterparty Records page ");

                string valUpdCancelTier = counterparty.GetDefaultValueOfTier();
                Assert.AreEqual(valSaveTier, valUpdCancelTier);
                extentReports.CreateLog("Updated value of Tier is not saved upon clicking cancel button on Counterparty Records page ");

                //Click on View All button and validate the counterparties
                string valCounterpartyName = counterparty.ClickViewAllAndValidateCounterpartiesName();
                Assert.AreEqual("EC - " + valCompName, valCounterpartyName);
                extentReports.CreateLog("Same Counterparty name is displayed on View all page ");

                //Validate the all the displayed columns and buttons on the View all page
                string lblCounterpartyName = counterparty.ValidateCounterpartyNameColumn();
                Assert.AreEqual("Counterparty Name", lblCounterpartyName);
                extentReports.CreateLog("Column with name: " + lblCounterpartyName + " is displayed on View all page ");

                string lblStatus = counterparty.ValidateStatusColumn();
                Assert.AreEqual("Status", lblStatus);
                extentReports.CreateLog("Column with name: " + lblStatus + " is displayed on View all page ");

                string lblDateOfLast = counterparty.ValidateDateOfLastStatusChangeColumn();
                Assert.AreEqual("Date of Last Status Change", lblDateOfLast);
                extentReports.CreateLog("Column with name: " + lblDateOfLast + " is displayed on View all page ");

                string lblNew = counterparty.ValidateNewButton();
                Assert.AreEqual("New", lblNew);
                extentReports.CreateLog("Button with name: " + lblNew + " is displayed on View all page ");

                string lblEdit = counterparty.ValidateEditOption();
                Assert.AreEqual("Edit", lblEdit);
                extentReports.CreateLog("Link with name: " + lblEdit + " is displayed to edit the Counterparty details ");

                string lblDelete = counterparty.ValidateDeleteOption();
                Assert.AreEqual("Delete", lblDelete);
                extentReports.CreateLog("Link with name: " + lblDelete + " is displayed to delete the Counterparty details ");

                //Click on Add Counterparties and validate all the displayed fields.
                string titleCounterparty = counterparty.ClickAddCounterpartiesAndValidatePage();
                Assert.AreEqual("Counterparties", titleCounterparty);
                extentReports.CreateLog("Page with title : " + titleCounterparty + " is displayed upon clicking Add Counterparty button ");

                string valExistingEng = counterparty.ValidateLabelGetCompaniesFromExistingEng();
                Assert.AreEqual("Get Companies from existing Engagement", valExistingEng);
                extentReports.CreateLog("Field with label : " + valExistingEng + " is displayed on Counterparties page ");

                string valSearchBox = counterparty.ValidateSearchBox();
                Assert.AreEqual("search", valSearchBox);
                extentReports.CreateLog("Field with label : " + valSearchBox + " is displayed on Counterparties page ");

                string valCompanyList = counterparty.ValidateLabelGetCompaniesFromExistingCompanyList();
                Assert.AreEqual("Get Companies from existing Company List", valCompanyList);
                extentReports.CreateLog("Field with label : " + valCompanyList + " is displayed on Counterparties page ");

                string valSearchCompanyList = counterparty.ExpandCompanyListAndValidateSearchBox();
                Assert.AreEqual("search", valSearchCompanyList);
                extentReports.CreateLog("Field with label : " + valSearchCompanyList + " is displayed upon expanding Get Companies from existing Company List field ");

                //Select Company and add it
                string titleCompanyList = counterparty.EnterCompanyAndValidateThePage();
                Assert.AreEqual("Company List", titleCompanyList);
                extentReports.CreateLog("Page with title : " + titleCompanyList + " is displayed upon clicking View All Company List ");

                string valCompany = counterparty.SelectAndAddCompany();
                Assert.AreEqual("Ahana Cloud, Inc.", valCompany);
                extentReports.CreateLog("Company with name : " + valCompany + " is displayed on Counterparties page after adding it ");

                //Update some details in newly added counterparty and validate the same
                string valSave2ndType = counterparty.UpdateTypeTierOf2ndCompanyAndClickSave();
                Assert.AreEqual("Financial", valSave2ndType);
                extentReports.CreateLog("Selected value of Type : " + valSave2ndType + " is saved upon clicking Save button on Counterparty Records page for 2nd counterparty ");

                string valSave2ndTier = counterparty.GetValueOfTierOf2ndCounterparty();
                Assert.AreEqual("B", valSave2ndTier);
                extentReports.CreateLog("Selected value of Tier : " + valSave2ndTier + " is saved upon clicking Save button on Counterparty Records page for 2nd counterparty ");

                string valView = counterparty.ClickBackButtonAndValidatePage();
                Assert.AreEqual("View", valView);
                extentReports.CreateLog("Counterparties List page is displayed upon clicking back button ");

                //Validate the validation without selecting any record and clicking delete button
                string msgAnyRec = counterparty.ValidateSelectAnyRecordValidation();
                Assert.AreEqual("Please select at least one row to delete.", msgAnyRec);
                extentReports.CreateLog("Message: " + msgAnyRec + " is displayed upon clicking Delete button without selecting any record ");

                string confirmMessage = counterparty.SelectAnyRecordAndClickDelete();
                Assert.AreEqual("Are you sure you want to delete the selected rows?", confirmMessage);
                extentReports.CreateLog("Message: " + confirmMessage + " is displayed upon clicking Delete button after selecting a record ");

                //Validate if 2nd added company is still displayed or not
                string msg2ndCompany = counterparty.Validate2ndCompanyPostDeletion();
                Assert.AreEqual("So-sure Limited", msg2ndCompany);
                extentReports.CreateLog("Added company is not displayed post clicking Delete button ");

                //Add Contact and Valdiate the same
                string titleContact = engagementDetails.ClickEngCounterpartyButton();
                Assert.AreEqual("Engagement Counterparty Contact Search", titleContact);
                extentReports.CreateLog("Engagement Counterparty Contact Search page is displayed upon clicking New Engagement Counterparty Contact button ");

                //Search Contact using search options i.e., Name 
                counterparty.SearchContactUsingName();
                string selectedName = counterparty.AddContact();
                string addedName = counterparty.ValidateAddedContact();
                Assert.AreEqual(selectedName, addedName);
                extentReports.CreateLog("Selected Contact : " + selectedName + " is added and displayed upon hovering the Contacts link ");

                //Change the default view and validate that if records are still displayed or not as per selected Job Types
                string updatedView = counterparty.UpdateDefaultView();
                Assert.AreEqual("Sellside Stages", updatedView);
                extentReports.CreateLog("View : " + updatedView + " has been selected ");

                string records = counterparty.ValidateCounterpartyRecords();
                Assert.AreEqual("No records found", records);
                extentReports.CreateLog(records + " upon changing the default view ");

                //Update view again to Buyside
                string updatedBuysideView = counterparty.RevertDefaultView();
                Assert.AreEqual("Buyside Stages", updatedBuysideView);
                extentReports.CreateLog("View : " + updatedBuysideView + " has been selected again ");

                //Select Counterparty, click on Email button and validate the page
                string titleConfirm = counterparty.SelectCounterpartyAndClickEmailButton();
                Assert.AreEqual("Confirm emails", titleConfirm);
                extentReports.CreateLog("Page with title : " + titleConfirm + " is displayed upon clicking the Email button ");

                //Validate Milestone dropdown and its values
                string lblMilestone = counterparty.ValidateMilestoneDropdown();
                Assert.AreEqual("Milestone", lblMilestone);
                extentReports.CreateLog("Fild with name : " + lblMilestone + " is displayed ");

                Assert.IsTrue(counterparty.ValidateMilestoneValues(), "Verified that displayed Milestone values are same");
                extentReports.CreateLog("Displayed Milestone values are correct ");

                //Validate Milestone dropdown and its values
                string lblTemplate = counterparty.ValidateTemplateDropdown();
                Assert.AreEqual("Template", lblTemplate);
                extentReports.CreateLog("Field with name : " + lblTemplate + " is displayed ");

                Assert.IsTrue(counterparty.ValidateTemplateValues(), "Verified that displayed Template values are same");
                extentReports.CreateLog("Displayed Template values are correct ");

                usersLogin.LightningLogout();
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
