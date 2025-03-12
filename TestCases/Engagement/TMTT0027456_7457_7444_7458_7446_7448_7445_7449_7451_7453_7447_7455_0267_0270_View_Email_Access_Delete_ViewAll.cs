using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Globalization;

namespace SF_Automation.TestCases.Engagement
{
    class TMTT0027456_7457_7444_7458_7446_7448_7445_7449_7451_7453_7447_7455_0267_0270_View_Email_Access_Delete_ViewAll : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        AddCounterparty counterparty = new AddCounterparty();
        public static string fileTC7877 = "TMTT0027456_7457_7444_7458_7446_7448_7445_7449_View_Email_Access_Delete_ViewAll.xls";

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

                //TC_01_And_TC_03_Validate View Counterparties button
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
                    extentReports.CreateLog("Button with name : " + viewCounterparty + " is displayed on Engagement Details page for Job Type: " + valJobType + " ");

                }

                //Click on Lightning Counterparties button, click on details and click on Eng Counterparty Contact
                engagementDetails.ClickViewCounterpartiesButton();

                //TC_03_Validate the value in View dropdown is same as Job Type of Engagement
                string view = counterparty.GetViewValue();
                Assert.AreEqual("Buyside", view);
                extentReports.CreateLog("View is displayed with same Job type for which engagement is associated ");

                //TC_02_Validate Add Remove Columns, Delete, Cancel,Add Counterparties, Email, View all buttons
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

                //TC_12_Validate Add counterparty functionality
                string addedComp = counterparty.ValidateAddCounterpartyFunctionality();
                Assert.AreEqual("Skyhive", addedComp);
                extentReports.CreateLog("Counterparty with Company name : " + addedComp + " is displayed on Counterparty page after adding it ");

                //TC_13_Search Counterparty
                string searchCounterparty = counterparty.ValidateSearchCounterpartyFunctionality();
                Assert.AreEqual("Skyhive", searchCounterparty);
                extentReports.CreateLog("Searched Counterparty with Company name : " + searchCounterparty + " is displayed on Counterparty page ");

                //TC_04__TC_07_Validate Email functionality
                //Add Contact and Valdiate the same
                string titleContact = engagementDetails.ClickEngCounterpartyButton();
                Assert.AreEqual("Engagement Counterparty Contact Search", titleContact);
                extentReports.CreateLog("Engagement Counterparty Contact Search page is displayed upon clicking New Engagement Counterparty Contact button ");

                //Search Contact using search options i.e., Name and validate added contact under Engagement Counterparty Contacts section
                counterparty.SearchContactUsingName();
                string selectedName = counterparty.AddContact();
                string titleEngCounterparty = counterparty.ValidateEngCounterpartiesPostClickingBackButton();
                string val1stName = counterparty.Get1stName();
                string val2ndName = counterparty.Get2ndName();
                Assert.AreEqual("Engagement Counterparty", titleEngCounterparty);
                extentReports.CreateLog("Page with title : " + titleEngCounterparty + " is displayed upon clicking Back button ");
                Assert.AreEqual(selectedName.Replace(" ",""), val1stName+val2ndName);
                extentReports.CreateLog("Selected Contact : " + selectedName + " is added and displayed under Engagement Counterparty Contacts section ");

                //Navigate to Counterparties page and validate Contacts link and Validate added contact along with its email id
                string addedContactID = counterparty.ValidateContactDetailsOnCounterpartiesPage();
                string addedCompCounterparty= counterparty.GetCompanyOfCounterparty();

                Assert.AreEqual("Contacts", addedContactID);
                extentReports.CreateLog("Contacts link is displayed after adding counterparty contact ");

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

                //Select a template and validate the email id and added counterparty's company
                string emailID = counterparty.ValidateEmailIdOnEmailTemplate();
                Assert.AreEqual("salmaan.jaffery@difc.ae", emailID);
                extentReports.CreateLog("Email - " + emailID +" id of added counterparty contact is displayed ");

                string company = counterparty.GetCompanyOfAddedCounterparty();
                Assert.AreEqual(addedCompCounterparty, company);
                extentReports.CreateLog("Company - " +company+ " of added counterparty contact is displayed ");

                //TC06_Validate View all functionality
                string pageViewAll = counterparty.ValidateViewAllFunctionality();
                Assert.AreEqual("Counterparties", pageViewAll);
                extentReports.CreateLog("Page with title - " + pageViewAll + " is displayed upon clicking View All button ");

                string tblCounterparties = counterparty.ValidateCounterpartyRecords();
                Assert.AreEqual("Counterparty records are displayed", tblCounterparties);
                extentReports.CreateLog("Existing counterparties are displayed ");

                string newViewAll = counterparty.ValidateViewAllNewButton();
                Assert.AreEqual("New", newViewAll);
                extentReports.CreateLog("Button with name:- "+newViewAll + " is displayed ");

                string editViewAll = counterparty.ValidateEditLinkOnViewAll();
                Assert.AreEqual("Edit", editViewAll);
                extentReports.CreateLog("Link - " + editViewAll + " is displayed to update counterparty details ");

                string deleteViewAll = counterparty.ValidateDeleteLinkOnViewAll();
                Assert.AreEqual("Delete", deleteViewAll);
                extentReports.CreateLog("Link - " + deleteViewAll + " is displayed to delete added counterparty ");

                //TC_08__Validate Engagement CP Comment functionality
                counterparty.ValidateEngCPComment();
                /*
                Assert.AreEqual("Testing", addedComment);
                string addedCommentType = counterparty.GetCPCommentType();
                Assert.AreEqual("Internal", addedCommentType);
                string addedCommentCreator = counterparty.GetCPCommentCreator();
                Assert.AreEqual(valUser, addedCommentCreator);
                string addedCommentCreatedDate = counterparty.GetCPCommentCreatedDate();
               // Assert.AreEqual(DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture), addedCommentCreatedDate);
                extentReports.CreateLog("Added comments - " + addedComment + " of Type: " + addedCommentType+ " by User: "+ addedCommentCreator+ " along with Date: " + addedCommentCreatedDate+ " is displayed in Engagement Counterparty Comments section ");

                //Delete added comments before deleting counterparty
                string valComment =counterparty.DeleteEngCounterpartyComment();
                Assert.AreEqual("(0)", valComment);
                extentReports.CreateLog("Added counterparty comments have been delete successfully. ");
                */

                //TC_13_Validate the displayed KPIs and click on any KPI & validate the displayed records
                string val1stKPI = counterparty.GetNumberOf1stKPI();
                string total = counterparty.GetTextOf1stKPI();
                Assert.AreEqual("2", val1stKPI);
                Assert.AreEqual("Total", total);
                extentReports.CreateLog("KPI with name :" +total +" is displayed with Count: "+val1stKPI +" as per the number of Counterparties ");

                string val2ndKPI = counterparty.GetNumberOf2ndKPI();
                string initialContact = counterparty.GetTextOf2ndKPI();
                Assert.AreEqual("1", val2ndKPI);
                Assert.AreEqual("Initial Contact", initialContact);
                extentReports.CreateLog("KPI with name :" + initialContact + " is displayed with Count: " + val2ndKPI + " as per its value entered in the displayed Counterparties ");

                string val3rdKPI = counterparty.GetNumberOf3rdKPI();
                string sentTeaser = counterparty.GetTextOf3rdKPI();
                Assert.AreEqual("1", val2ndKPI);
                Assert.AreEqual("Sent Teaser", sentTeaser);
                extentReports.CreateLog("KPI with name :" + sentTeaser + " is displayed with Count: " + val3rdKPI + " as per its value entered in the displayed Counterparties ");

                string displayedRec = counterparty.ValidateKPIFunctionality();
                Assert.AreEqual("Displaying 1 to 1 of 1 records. Page 1 of 1.", displayedRec);
                extentReports.CreateLog("Counterparty with selected KPI's details is displayed upon clicking it. ");
                                
                //TC05__Validate delete functionality
                string confirmMessage = counterparty.SelectAnyRecordAndClickDelete();
                Assert.AreEqual("Are you sure you want to delete the selected rows ?", confirmMessage);
                extentReports.CreateLog("Message: " + confirmMessage + " is displayed upon clicking Delete button after selecting a record ");

                string finalmessage = counterparty.ValidateIfRecordIsDeleted();
                Assert.AreEqual("Displaying 1 to 1 of 1 records. Page 1 of 1.", finalmessage);
                extentReports.CreateLog("Added Counterparty is deleted post clicking Of button on the confirmation window ");

                //TC11__Validate the Edit Bid functionality on the view counterparty
                string tabBid = counterparty.ClickEditBidAndValidateNewTab();
                Assert.AreEqual("Round First", tabBid);
                extentReports.CreateLog("Tab with name: " + tabBid+ " is displayed upon adding bid ");

                //Add all bid details and validate it
                string value = ReadExcelData.ReadData(excelPath, "Bid", 1);
                string minBid = counterparty.SaveBidValues(value);
                Assert.AreEqual(value, minBid);
                extentReports.CreateLog("Bid with Min Bid: " + minBid + " is displayed upon saving bid details ");

                //Validate value of Max bid value
                string maxBid = counterparty.GetMaxBid();
                Assert.AreEqual(value, maxBid);
                extentReports.CreateLog("Bid with Max Bid: " + maxBid + " is displayed upon saving bid details ");

                //Validate added bid details on Counterparties details page
                string minBidCounterparty = counterparty.ValidateMinBidDetailOnCounterpartiesPage();
                Assert.AreEqual("GBP " + value, minBidCounterparty);
                extentReports.CreateLog("Bid with Round Minimum (MM): " + minBidCounterparty + " is displayed on counterparty details page after saving ");

                string maxBidCounterparty = counterparty.ValidateMaxBidDetailOnCounterpartiesPage();
                Assert.AreEqual("GBP " + value, maxBidCounterparty);
                extentReports.CreateLog("Bid with Round Maximum (MM): " + maxBidCounterparty + " is displayed on counterparty details page after saving ");
                                                       
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
