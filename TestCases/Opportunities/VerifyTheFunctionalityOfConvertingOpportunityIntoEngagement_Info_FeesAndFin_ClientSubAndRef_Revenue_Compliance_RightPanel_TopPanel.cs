using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Opportunity;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunity
{
    class VerifyTheFunctionalityOfConvertingOpportunityIntoEngagement_Info_FeesAndFin_ClientSubAndRef_Revenue_Compliance_RightPanel_TopPanel : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOppCounterparty counterparty = new AddOppCounterparty();       
        AddOpportunityContact addContact = new AddOpportunityContact();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engHome = new EngagementHomePage();

        public static string TMTT0017889 = "TMTT0017889_CommentsAndContactsMappingToEngUponConversionFromOpportunity.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void FunctionalityOfOpportunity()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + TMTT0017889;
                string excelPath1 = ReadJSONData.data.filePaths.testData;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");   

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                 Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                 extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);

                //Login as Financial User and validate the user                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Verify the availablity of Opportunity under HL Banker list
                string tagOpp= opportunityHome.ValidateOppUnderHLBanker();
                Assert.AreEqual("Opportunities", tagOpp);
                extentReports.CreateLog(tagOpp + " is displayed under HL Banker dropdown ");           
              
                //Verify that choose LOB is displayed after clicking New button
                Assert.IsTrue(opportunityHome.ValidateChooseLOBPostClickingNewButton(), "Verified that displayed LOBs are same");
                extentReports.CreateLog("Choose LOB screen is displayed upon clicking New button ");

                //Validate title of the page upon clicking next page
                string titleOpp = opportunityHome.ClickNextAndValidatePage();
                Assert.AreEqual("New Opportunity: CF", titleOpp);
                extentReports.CreateLog("Page with title: " + titleOpp + " is displayed upon clicking next button ");
                              
                //Enter details for all mandatory fields
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);

				string value = addOpportunity.AddOpportunitiesLightning(valJobType, TMTT0017889);
                Console.WriteLine("value : " + value);
                extentReports.CreateLog("Opportunity with number : " + value + " is created ");

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab=  addOpportunity.EnterStaffDetailsL(TMTT0017889);
                Assert.AreEqual("Info", displayedTab);
                extentReports.CreateLog("Tab with name: " + displayedTab + " is displayed upon saving internal deal team members details ");

                //Validate Request Engagement button
                string btnRequestToEng = opportunityDetails.ValidateRequestEngButton();
                Assert.AreEqual("True", btnRequestToEng);
                extentReports.CreateLog("Request Engagement button is displayed on top panel of Opportuniy details page ");
                                   
                //Update all required fields for Conversion to Engagement
                counterparty.ClickViewCounterparties();
                opportunityDetails.UpdateReqFieldsForCFConversionL(TMTT0017889);
                extentReports.CreateLog("All required details are saved ");
                opportunityDetails.UpdateInternalTeamDetailsL(TMTT0017889);
                extentReports.CreateLog("Internal Team members details are saved ");
                opportunityDetails.ClickAddCFOppContact();
                addContact.CreateContactL(TMTT0017889);

                //Update CC and NBC
                usersLogin.LightningLogout();

                //Search for Opportunity
                opportunityHome.SearchOpportunity(value);                

                opportunityDetails.UpdateNBCApproval();
                opportunityDetails.UpdateOutcomeDetails(TMTT0017889);

                //Login as Financial User and validate the user                
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUserLightning();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                //Open the same opportunity and Click on Request Engagement               
                opportunityHome.SearchMyOpportunitiesInLightning(value, valUser);
                opportunityDetails.ClickRequestoEngL();
                extentReports.CreateLog("No Validation error is displayed and Opportunity is requested for approval ");

                //Login as CAO user and Validate the status of Opportunity post Request Engagement
                usersLogin.DiffLightningLogout();               
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2));
                string caoUser = login.ValidateUserLightningCAO();
                Console.WriteLine("caoUser:" + caoUser);
                Assert.AreEqual(caoUser.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2)), true);
                extentReports.CreateLog("User: " + caoUser + " logged in ");

                //Search for created opportunity and validate the status
                opportunityHome.SearchMyOpportunitiesInLightning(value, ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2));
                string valStatus = opportunityDetails.ValidateStatusOfOpportunity();
                Assert.AreEqual("Pending", valStatus);
                extentReports.CreateLog("Status of Opportunity: " + valStatus + " is displayed after clicking Request Engagement ");

                //Validate Approve and Reject buttons
                string approve = opportunityDetails.ValidateApproveButton();
                string approvalHistory = opportunityDetails.ValidateApprovalHistoryPage();
                Assert.AreEqual("Approve", approve);
                Assert.AreEqual("Approval History", approvalHistory);
                extentReports.CreateLog("Button with name: " + approve + " is displayed on " + approvalHistory +" page ");

                string reject = opportunityDetails.ValidateRejectButton();               
                Assert.AreEqual("Reject", reject);                
                extentReports.CreateLog("Button with name: " + reject + " is displayed on " + approvalHistory + " page ");

                //Reject the Opportunity
                string valRejectStatus = opportunityDetails.ClickRejectButtonL();
                Assert.AreEqual("Rejected", valRejectStatus);
                extentReports.CreateLog("Status of Opportunity after rejecting is displayed as : " + valRejectStatus + " ");

                //Logout of CAO, Login as CF Financial user and Resubmit the Opportunity to approve now
                usersLogin.DiffLightningLogout();
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser2 = login.ValidateUserLightning();
                Assert.AreEqual(stdUser2.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser2 + " logged in ");

                opportunityHome.SearchMyOpportunitiesInLightning(value, valUser);
                opportunityDetails.ClickRequestoEngL();

                //Login as CAO user 
                usersLogin.DiffLightningLogout();
                usersLogin.SearchUserAndLogin(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2));
                string caoUser1 = login.ValidateUserLightningCAO();
                Console.WriteLine("caoUser:" + caoUser1);
                Assert.AreEqual(caoUser1.Contains(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2)), true);
                extentReports.CreateLog("User: " + caoUser1 + " logged in ");

                //Search for created opportunity and approve the Opportunity
                opportunityHome.SearchMyOpportunitiesInLightning(value, ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2));
                string status = opportunityDetails.ClickApproveButtonL();
                Assert.AreEqual("Approved", status);
                extentReports.CreateLog("Opportunity is approved ");

                //Convert the Opportunity to Engagement by clicking Convert To Engagement
                opportunityDetails.ClickOppName();
                string engDetails = opportunityDetails.ClickReqToEngagement();
                Assert.AreEqual("Engagement", engDetails);
                extentReports.CreateLog("Opportunity is converted to Engagement after clicking Request To Engagement button ");

                //Validate the information displayed on Engagement is copied from Opportunity
                string valClient = ReadExcelData.ReadData(excelPath, "AddOpportunity", 1);
                string valSubject = ReadExcelData.ReadData(excelPath, "AddOpportunity", 2);

                string clientComp = engagementDetails.GetClientCompanyL();
                string subjectComp = engagementDetails.GetSubjectCompanyL();
                Console.Write(clientComp);
                string engNum = engagementDetails.GetEngNumL();
                Assert.AreEqual(valClient, clientComp);
                Assert.AreEqual(valSubject, subjectComp);
                extentReports.CreateLog("Client and Subject companies are copied in Engagement from created Opportunity ");
                               
                //Verify the availablity of Engagement under HL Banker list
                string tagEng = engHome.ValidateEngUnderHLBanker();
                Assert.AreEqual("Engagements", tagEng);
                extentReports.CreateLog(tagEng + " is displayed under HL Banker dropdown ");

                //Verify that Recently Viewed is displayed along with list of Engagements
                string lblRecently = engHome.ValidateRecentViewedUponSelectingEngagements();
                Assert.AreEqual("Recently Viewed", lblRecently);
                extentReports.CreateLog(lblRecently + " is displayed upon selecting Engagements ");

                //List of Recent Viewed Engagements are displayed
                string engList = engHome.ValidateIfRecentlyViewedEngagementsAreDisplayed();
                Assert.AreEqual("True", engList);
                extentReports.CreateLog("Recently viewed Engagements are displayed in Recently Viewed list ");

                //Validate all the values displayed under Recently Viewed
                Assert.IsTrue(engHome.ValidateRecentlyViewedValues(), "Verified that displayed Recently Viewed values are same");
                extentReports.CreateLog("Recently Viewed dropdown values are displayed as expected ");

                //Validate if Search functionality is available or not
                string searchOpp = engHome.ValidateSearchFunctionalityIsAvailable();
                Assert.AreEqual("True", searchOpp);
                extentReports.CreateLog("Search Engagements functionality is available ");

                //Verify Search Functionality of Engagements
                string searchedEng = engHome.ValidateSearchFunctionalityOfEngagements(engNum);
                Assert.AreEqual(searchedEng, engNum);
                extentReports.CreateLog("Engagement is displayed as per entered search criteria ");

                //Validate on clicking Engagement number, engagement details page is displayed
                engHome.ClickEngNumber();
                string titleEngDetails = engHome.ClickEngNumAndValidateThePage();
                Assert.AreEqual("Details", titleEngDetails);
                extentReports.CreateLog("Engagement Details page is displayed upon clicking Engagement number ");

                //Validate Info tab and all its Sub tabs
                string tabInfo = engagementDetails.ValidateInfoTab();
                Assert.AreEqual("Info", tabInfo);
                extentReports.CreateLog("Sub Tab " + tabInfo + " is displayed on Engagement Details page ");

                string details = engagementDetails.ValidateDetailsSubTab();
                Assert.AreEqual("Details", details);
                extentReports.CreateLog("Sub Tab " + details + " is displayed under Info Tab ");

                string impDates = engagementDetails.ValidateImportantDatesSubTab();
                Assert.AreEqual("Important Dates", impDates);
                extentReports.CreateLog("Sub Tab " + impDates + " is displayed under Info Tab ");

                string admin = engagementDetails.ValidateAdministrationSubTab();
                Assert.AreEqual("Administration", admin);
                extentReports.CreateLog("Sub Tab " + admin + " is displayed under Info Tab ");

                string closingInfo = engagementDetails.ValidateClosingInfoSubTab();
                Assert.AreEqual("Closing Info", closingInfo);
                extentReports.CreateLog("Sub Tab " + closingInfo + " is displayed under Info Tab ");

                string CST = engagementDetails.ValidateCSTQuestionnaireDetailsSubTab();
                Assert.AreEqual("CST Questionnaire Details", CST);
                extentReports.CreateLog("Sub Tab " + CST + " is displayed under Info Tab ");

                string billing = engagementDetails.ValidateBillingCommentsSubTab();
                Assert.AreEqual("Billing Comments", billing);
                extentReports.CreateLog("Sub Tab " + billing + " is displayed under Info Tab ");

                //Validate Edit functionality of Details tab                
                string tabDetailsEditable = engagementDetails.ValidateDetailsTabIsEditable();
                Assert.AreEqual("True", tabDetailsEditable);
                extentReports.CreateLog("Page is editable after clicking pencil icon and details can be edited ");

                //Update any value and validate if it gets saved post clicking saving button
                string valClientOwnership = engagementDetails.GetClientOwnershipL();
                engagementDetails.UpdateClientOwnershipL();
                string valUpdClientOwnership = engagementDetails.GetClientOwnershipLPostUpdate();
                Assert.AreNotEqual(valClientOwnership, valUpdClientOwnership);
                extentReports.CreateLog("Entered value : " + valUpdClientOwnership + " is displayed after updating details of Client Ownership in Details tab ");

                //Click Imp Dates tab and validate edit functionality
                engagementDetails.ClickImpDates();
                string tabImpEditable = engagementDetails.ValidateImpDatesTabIsEditable();
                Assert.AreEqual("True", tabDetailsEditable);
                extentReports.CreateLog("Page is editable after clicking pencil icon and details can be edited ");

                //Update any value and validate if it gets saved post clicking saving button                
                engagementDetails.UpdateExpectedMktDateL();
                string valUpdExpMktDate = engagementDetails.GetExpMktDatePostUpdate();
                Assert.NotNull(valUpdExpMktDate);
                extentReports.CreateLog("Entered value : " + valUpdExpMktDate + " is displayed after updating details of Expected In Market Date in Important Dates tab ");

                //Click Administration tab and validate edit functionality
                engagementDetails.ClickAdmin();
                string tabAdminEditable = engagementDetails.ValidateAdministrationTabIsEditable();
                Assert.AreEqual("True", tabAdminEditable);
                extentReports.CreateLog("Page is editable after clicking pencil icon and details can be edited ");

                //Update any value and validate if it gets saved post clicking saving button
                string deal = CustomFunctions.RandomValue();
                string valDeal = engagementDetails.UpdateDealCloudIDAndValidate(deal);
                Assert.AreEqual(deal, valDeal);
                extentReports.CreateLog("Entered value : " + valDeal + " is displayed after updating details of DealCloud ID in Administration tab ");

                //Click Closing Info tab and validate edit functionality
                engagementDetails.ClickClosingInfo();
                string secDoc = engagementDetails.ValidateSectionDocChecklist();
                Assert.AreEqual("Document Checklist", secDoc);
                extentReports.CreateLog("Section with name: " + secDoc + " is displayed on Closing Info tab ");

                string tabClosingInfoEditable = engagementDetails.ValidateClosingInfoTabIsEditable();
                Assert.AreEqual("True", tabClosingInfoEditable);
                extentReports.CreateLog("Page is editable after clicking pencil icon and details can be edited ");

                //Update any value and validate if it gets saved post clicking saving button               
                string valIntDeal = engagementDetails.UpdateIntDealAndValidate();
                Assert.AreEqual("Completed", valIntDeal);
                extentReports.CreateLog("Entered value : " + valIntDeal + " is displayed after updating details of Internal deal announcement in Closing Info tab ");

                //Click CST Questionnaire Details tab and validate edit functionality
                engagementDetails.ClickCSTQuesTab();
                string tabCSTEditable = engagementDetails.ValidateCSTTabIsEditable();
                Assert.AreEqual("True", tabCSTEditable);
                extentReports.CreateLog("Page is editable after clicking pencil icon and details can be edited ");

                //Update any value and validate if it gets saved post clicking saving button               
                string valCST = engagementDetails.UpdateCSTQuestionnaireAndValidate();
                Assert.AreEqual("Yes", valCST);
                extentReports.CreateLog("Entered value : " + valCST + " is displayed after updating details of CST Questionnaire in CST Questionnaire Details tab ");

                //Click Billing Comments tab and validate displayed validations
                engagementDetails.ClickBillingCommentsTab();
                string tabBillingEditable = engagementDetails.ValidateCommentDateMandatoryValidation();
                Assert.AreEqual("Complete this field.", tabBillingEditable);
                extentReports.CreateLog("Message: " + tabBillingEditable + " is displayed upon clicking Save button without entering Date ");

                string msgStatus = engagementDetails.ValidateCommentStatusMandatoryValidation();
                Assert.AreEqual("Complete this field.", msgStatus);
                extentReports.CreateLog("Message: " + msgStatus + " is displayed upon clicking Save button without entering Status ");

                string msgComment = engagementDetails.ValidateBillingCommentMandatoryValidation();
                Assert.AreEqual("Complete this field.", msgComment);
                extentReports.CreateLog("Message: " + msgComment + " is displayed upon clicking Save button without entering Comment ");

                //Save the billing comments
                string commentID = engagementDetails.SaveBillingComment();
                extentReports.CreateLog("Billing comment with id: " + commentID + " is displayed after adding it ");

                //Edit Billing Comments and validate the mandatory validation
                string msgCommentsUponEdit = engagementDetails.ValidateMandatoryMessageUponEditingBillingComment();
                Assert.AreEqual("Complete this field.", msgCommentsUponEdit);
                extentReports.CreateLog("Message: " + msgCommentsUponEdit + " is displayed upon clicking Save button without entering Comment on Edit Comments page ");

                //Edit Billing comments and validate the updated values
                string valCommentsUponEdit = engagementDetails.ValidateEditFunctionalityOfBillingComment("Testing Comments");
                Assert.AreEqual("Testing Comments", valCommentsUponEdit);
                extentReports.CreateLog("Updated comments : " + valCommentsUponEdit + " is displayed upon clicking editing Comment on Edit Comments page ");

                //Delete Billing comments and validate the same
                string valCommentsUponDelete = engagementDetails.ValidateDeleteFunctionalityOfBillingComment();
                Assert.AreEqual("Billing comment does not exist", valCommentsUponDelete);
                extentReports.CreateLog(valCommentsUponDelete + " after deleting Billing Comments ");

                //Validate Fees & Financials tab 
                string tabFee = engagementDetails.ValidateFeesTab();
                Assert.AreEqual("Fees & Financials", tabFee);
                extentReports.CreateLog("Sub Tab " + tabFee + " is displayed on Engagement Details page ");

                //Validate Edit functionality of Fees & Financials tab                
                string tabFeesEditable = engagementDetails.ValidateFeesTabIsEditable();
                Assert.AreEqual("True", tabFeesEditable);
                extentReports.CreateLog("Page is editable after clicking pencil icon and Fees & Financials details can be edited ");

                //Update any value and validate if it gets saved post clicking saving button               
                string valEBITDA = engagementDetails.UpdateFeesAndFinAndValidate();
                Assert.AreEqual("GBP 10.0", valEBITDA);
                extentReports.CreateLog("Entered value : " + valEBITDA + " is displayed after updating details of EBITDA in Fees & Financials tab ");

                //Validate Client/Subject & Referral tab 
                string tabClient = engagementDetails.ValidateClientSubjectAndReferralTab();
                Assert.AreEqual("Client/Subject & Referral", tabClient);
                extentReports.CreateLog("Sub Tab " + tabClient + " is displayed on Engagement Details page ");

                //Validate Edit functionality of Client/Subject & Referral tab                
                string tabClientEditable = engagementDetails.ValidateClientSubjectAndReferralTabIsEditable();
                Assert.AreEqual("True", tabClientEditable);
                extentReports.CreateLog("Page is editable after clicking pencil icon and Client/Subject & Referral details can be edited ");

                //Update any value and validate if it gets saved post clicking saving button               
                string valFee = engagementDetails.UpdateEstReferralFeeAndValidate();
                Assert.AreEqual("GBP 10.0", valFee);
                extentReports.CreateLog("Entered value : " + valFee + " is displayed after updating details of Est. Referral Fee in Client/Subject & Referral tab ");

                //Validate the updated value of Additional Client and Subject section            
                string valType = engagementDetails.ValidateMandatoryValidationOfClientSubject();
                Assert.AreEqual("Client", valType);
                extentReports.CreateLog("Updated value: " + valType + " is not displayed upon editing as Primary Client and Subject can not change the Type ");
               
                //TC_04 --Revenue
                //Validate Revenue tab 
                string tabRevenue = engagementDetails.ValidateRevenueTab();
                Assert.AreEqual("Revenue", tabRevenue);
                extentReports.CreateLog("Tab " + tabRevenue + " is displayed on Engagement Details page ");

                //Validate Add functionality of Revenue Accural tab                
                string RevID = engagementDetails.ValidateAddRevenueFunctionality();
                extentReports.CreateLog("Revenue Accural with id: " + RevID + " is created after adding Revenue Accural ");

                //Validate Edit Functionality of Revenue Accural tab    
                string Legacy = engagementDetails.ValidateEditRevenueFunctionality();
                Assert.AreEqual("Testing", Legacy);
                extentReports.CreateLog("Legacy DC ID wth value: " + Legacy + " is saved after updating Revenue Accural ");

                //Validate and click Revenue Projection tab
                string RevProj = engagementDetails.ValidateAndClickRevenueProjectionTab();
                Assert.AreEqual("Revenue Projections", RevProj);
                extentReports.CreateLog("Page with title: " + RevProj + " is displayed after clicking Revenue Projection tab ");

                //Validate Update functionality of Revenue Projection
                string valRevProj = engagementDetails.ValidateEditRevenueProjFunctionality();
                Assert.AreEqual("GBP 10.00", valRevProj);
                extentReports.CreateLog("Revenue Projection with Projected Monthly Fee: " + valRevProj + " is displayed after updating Revenue Projection ");

                //Validate Clear functionality of Revenue Projection
                string msgRevProjPostClear = engagementDetails.ValidateClearRevenueProjFunctionality();
                Assert.AreEqual("No Records To Display", msgRevProjPostClear);
                extentReports.CreateLog("Message: " + msgRevProjPostClear + " is displayed after clicking clear on Revenue Projection ");

                //Validate Submit functionality of Revenue Projection
                string valMonth = engagementDetails.ValidateSubmitRevenueProjFunctionality();
                Assert.AreEqual("05", valMonth);
                extentReports.CreateLog("Month: " + valMonth + " is displayed after selecting Month and clicking Submit on Revenue Projection ");

                //Validate Return To Engagement button functionality
                string title = engagementDetails.ValidateReturnToEngFunctionality();
                Assert.AreEqual("Revenue Projection", title);
                extentReports.CreateLog("Tab with name: " + title + " is displayed after clicking Return To Engagement button ");
               
                //TC_05 --Compliance and Legal
                //Validate Compliance & Legal tab 
                string tabCompliance = engagementDetails.ValidateComplianceAndLegalTab();
                Assert.AreEqual("Compliance & Legal", tabCompliance);
                extentReports.CreateLog("Tab " + tabCompliance + " is displayed on Engagement Details page ");

                //Validate Compliance Sub tab 
                string subTabCompliance = engagementDetails.ValidateComplianceSubTab();
                Assert.AreEqual("Compliance", subTabCompliance);
                extentReports.CreateLog("Sub Tab " + subTabCompliance + " is displayed under Compliance & Legal tab ");

                //Validate Edit functionality of Compliance sub tab                
                string subTabComplianceEdit = engagementDetails.ValidateComplianceSubTabIsEditable();
                Assert.AreEqual("True", subTabComplianceEdit);
                extentReports.CreateLog("Page is editable after clicking pencil icon and Compliance details can be edited ");

                //Update any value and validate if it gets saved post clicking saving button               
                string valCompliance = engagementDetails.UpdateComplianceDetailsAndValidate();
                Assert.AreEqual("No", valCompliance);
                extentReports.CreateLog("Entered value : " + valCompliance + " is displayed after updating details of Beneficial Owner Control Person form in Compliance Sub tab ");

                //Validate Legal Matters Sub tab 
                string subTabLegal = engagementDetails.ValidateLegalMattersSubTab();
                Assert.AreEqual("Legal Matters", subTabLegal);
                extentReports.CreateLog("Sub Tab " + subTabLegal + " is displayed under Compliance & Legal tab ");

                //Validate Edit functionality of Legal Matters sub tab                
                string subTabLegalEdit = engagementDetails.ValidateLegalMattersSubTabIsEditable();
                Assert.AreEqual("True", subTabLegalEdit);
                extentReports.CreateLog("Page is editable after clicking pencil icon and Compliance details can be edited ");

                //Update any value and validate if it gets saved post clicking saving button               
                string valDateSigned = engagementDetails.UpdateDateSignedAndValidate();
                Assert.AreEqual("4/11/2023", valDateSigned);
                extentReports.CreateLog("Entered value : " + valDateSigned + " is displayed after updating details of Date Signed in Legal Matters Sub tab ");

                //Validate Conflict Check Sub tab 
                string subTabCC = engagementDetails.ValidateConflictCheckSubTab();
                Assert.AreEqual("Conflict Check", subTabCC);
                extentReports.CreateLog("Sub Tab " + subTabCC + " is displayed under Compliance & Legal tab ");

                //TC_06 --Right Panel
                //Validate Comments tab                
                string tabComments = engagementDetails.ValidateCommentsTab();
                Assert.AreEqual("Comments", tabComments);
                extentReports.CreateLog("Tab " + tabComments + " is displayed in Right panel of Engagement details page ");

                //Validate Financials tab                
                string tabFinancials = engagementDetails.ValidateFinancialsTab();
                Assert.AreEqual("Financials", tabFinancials);
                extentReports.CreateLog("Tab " + tabFinancials + " is displayed in Right panel of Engagement details page ");

                //Validate Eng Contacts tab                
                string tabEngContacts = engagementDetails.ValidateEngContactsTab();
                Assert.AreEqual("Eng Contacts", tabEngContacts);
                extentReports.CreateLog("Tab " + tabEngContacts + " is displayed in Right panel of Engagement details page ");

                //Validate CST tab                
                string tabCST = engagementDetails.ValidateCSTTab();
                Assert.AreEqual("CST", tabCST);
                extentReports.CreateLog("Tab " + tabCST + " is displayed in Right panel of Engagement details page ");

                //Save an Engagement Comment and Validate the added
                string addedCommentsType = engagementDetails.AddEngCommentaAndValidate();
                string addedComments = opportunityDetails.GetOppCommentsL();
                Assert.AreEqual("Administrative", addedCommentsType);
                Assert.AreEqual("Testing", addedComments);
                extentReports.CreateLog("Added Engagement comments of Type: " + addedCommentsType + " and comments: " + addedComments + " is displayed under Comments section ");

                //Validate update functionality of existing comment
                string valUpdatedComment =engagementDetails.ValidateUpdateFunctionalityOfEngComment();
                Assert.AreEqual("Test Comments", valUpdatedComment);
                extentReports.CreateLog("Updated Engagement comments of Type: " + addedCommentsType + " and comments: " + valUpdatedComment + " is displayed after updation ");

                //Validate delete functionality of existing comment
                string msgDeletedComment = engagementDetails.ValidateDeleteFunctionalityOfEngComment();
                Assert.AreEqual("(0)", msgDeletedComment);
                extentReports.CreateLog("No comments is displayed after deleting the existing comment ");

                //Validate the File Upload functionality
                string uploadFiles = opportunityDetails.ValidateFileUploadsOption();
                Assert.AreEqual("Upload Files", uploadFiles);
                extentReports.CreateLog("Button with name: " + uploadFiles + " is displayed under Files section of Opportunity Details page ");

                string successMsg = opportunityDetails.UploadFileAndValidate(excelPath1 + "UploadFile.txt");
                Assert.AreEqual("UploadFile", successMsg);
                extentReports.CreateLog("Selected File has been uploaded ");

                //Validate new financials are getting added
                string finID = engagementDetails.AddFinancialsAndValidate();
                Assert.NotNull(finID);
                extentReports.CreateLog("Financials with ID: " + finID + " got added successfully ");

                //Validate Engagement contacts
                string contact = engagementDetails.ValidateEngContacts();
                string name = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                Assert.AreEqual(name,contact);
                extentReports.CreateLog("User can view added Engagement contacts ");

                //Validate that Engagement Contacts can be updated
                string updContact = engagementDetails.ValidateUpdateFunctionalityOfEngContacts();
                Assert.AreNotEqual(contact, updContact);
                extentReports.CreateLog("Engagement contact with new name:  "+ updContact+ " is updated ");

				//TC_07 -- Top Panel
				//Validate Update Backlog button   
				string uploadBacklog = engagementDetails.ValidateUpdateBacklogButton();
				Assert.AreEqual("Update Backlog", uploadBacklog);
				extentReports.CreateLog("Button: " + uploadBacklog + " is displayed on the Top panel of Engagement details page ");

				//Validate mandatory validation of Update Backlog button   
				string validationBacklog = engagementDetails.ValidateMandatoryValidationOfUpdateBacklog();
				Assert.AreEqual("These required fields must be completed: Engagement Name", validationBacklog);
				extentReports.CreateLog("Validation message: " + validationBacklog + " is displayed when Save button is clicked without entering mandatory field values. ");

				//Validate edit functionality of Update Backlog button   
				string updateBacklog = engagementDetails.ValidateEditFunctionalityOfUpdateBacklog();
				Assert.AreEqual("Testing", updateBacklog);
                extentReports.CreateLog("Updated value: " + updateBacklog + " of Engagement Name is displayed upon updation ");

				//Validate Add Client button   
				string addClient = engagementDetails.ValidateAddClientButton();
				Assert.AreEqual("Add Client", addClient);
				extentReports.CreateLog("Button: " + addClient + " is displayed on the Top panel of Engagement details page ");

				//Validate mandatory validation of Add Client button   
				string validationClient = engagementDetails.ValidateMandatoryValidationOfAddClient();
				Assert.AreEqual("These required fields must be completed: Client/Subject", validationClient);
				extentReports.CreateLog("Validation message: " + validationClient + " is displayed when Save button is clicked without entering mandatory field values. ");

				//Validate edit functionality of Add Client button   
				string updateClient= engagementDetails.ValidateEditFunctionalityOfAddClient();
				string typeClient = engagementDetails.GetTypeOfClientCompany();
				Assert.AreNotEqual(valClient, updateClient);
                Assert.AreEqual("Client",typeClient);
				extentReports.CreateLog("New company with name: " + updateClient + " and Type: " + typeClient + " is added upon adding new company ");

				//Validate Add Subject button   
				string addSubject = engagementDetails.ValidateAddSubjectButton();
				Assert.AreEqual("Add Subject", addSubject);
				extentReports.CreateLog("Button: " + addSubject + " is displayed on the Top panel of Engagement details page ");

				//Validate mandatory validation of Add Subject button   
				string validationSubject = engagementDetails.ValidateMandatoryValidationOfAddSubject();
				Assert.AreEqual("These required fields must be completed: Client/Subject", validationSubject);
				extentReports.CreateLog("Validation message: " + validationSubject + " is displayed when Save button is clicked without entering mandatory field values. ");

				//Validate edit functionality of Add Subject button   
				string updateSubject = engagementDetails.ValidateEditFunctionalityOfAddSubject();
                string typeSubject = engagementDetails.GetTypeOfSubCompany();
				Assert.AreNotEqual(valClient, updateSubject);
				Assert.AreEqual("Subject", typeSubject);
				extentReports.CreateLog("New company with name: " + updateSubject + " and Type: " + typeSubject + " is added upon adding new company ");

				//Validate Add Other Party button   
				string addOther = engagementDetails.ValidateAddOtherPartyButton();
				Assert.AreEqual("Add Other Party", addOther);
				extentReports.CreateLog("Button: " + addOther + " is displayed on the Top panel of Engagement details page ");

				//Validate mandatory validation of Add Other Party button   
				string validationOther = engagementDetails.ValidateMandatoryValidationOfAddOtherParty();
				Assert.AreEqual("These required fields must be completed: Client/Subject", validationOther);
				extentReports.CreateLog("Validation message: " + validationOther + " is displayed when Save button is clicked without entering mandatory field values. ");

				//Validate edit functionality of Add Other Party button   
				string updateOther = engagementDetails.ValidateEditFunctionalityOfAddOtherParty();
				string typeOther = engagementDetails.GetTypeOfOtherCompany();
				Assert.AreNotEqual(valClient, updateOther);
				//Assert.AreEqual("Other", typeOther);
				extentReports.CreateLog("New company with name: " + updateOther + " is added upon adding new company ");

				//Validate Add CF Engagement Contact button   
				string addEngContact = engagementDetails.ValidateAddCFEngContactButton();
				Assert.AreEqual("Add CF Engagement Contact", addEngContact);
				extentReports.CreateLog("Button: " + addEngContact + " is displayed on the Top panel of Engagement details page ");

				//Validate mandatory validation of Add CF Engagement Contact button   
				string validationAddContact = engagementDetails.ValidateMandatoryValidationOfAddCFEngContact();
				Assert.AreEqual("These required fields must be completed: Contact, Party", validationAddContact);
				extentReports.CreateLog("Validation message: " + validationAddContact + " is displayed when Save button is clicked without entering mandatory field values. ");

				//Validate add functionality of Add CF Engagement Contact button   
				string addedContactNum = engagementDetails.ValidateEditFunctionalityOfAddEngContact();
                Assert.AreEqual("(2)", addedContactNum);
                extentReports.CreateLog("New Contact is added under Engagement Contacts ");

                //Validate View Counterparties button   
                string viewCounterparties = engagementDetails.ValidateVisibilityOfViewCounterpartiesButton();
                Assert.AreEqual("View Counterparties button is displayed", viewCounterparties);
                extentReports.CreateLog("Button: " + viewCounterparties + " is displayed on the Top panel of Engagement details page ");

                //Validate Billing Request button   
                string billingReq = engagementDetails.ValidateBillingRequestButton();
                Assert.AreEqual("Billing Request", billingReq);
                extentReports.CreateLog("Button: " + billingReq + " is displayed on the Top panel of Engagement details page ");

                //Validate Additional CCs section   
                string additionalCC = engagementDetails.ValidateAdditionalCCSecction();
                Assert.AreEqual("Additional CCs", additionalCC);
                extentReports.CreateLog("Section with name: " + additionalCC + " is displayed after clicking Billing Request button ");

                //Validate mandatory validation of Billing Request 
                string validationSendEmail = engagementDetails.ValidateMandatoryValidationOfBillingRequest();
                Assert.AreEqual("Warning:To is required.", validationSendEmail);
                extentReports.CreateLog("Validation message: " + validationSendEmail + " is displayed when Send Email button is clicked without entering mandatory field values. ");

                //Validate cancel functionality of Billing Request 
                string titleEngDetailsL = engagementDetails.ValidateCancelFunctionalityOfBillingRequest();
                Assert.AreEqual("Info", titleEngDetailsL);
                extentReports.CreateLog("Page with title : " + titleEngDetailsL + " is displayed when cancel button is clicked on Billing Request page ");

                //Validate Send Email functionality of Billing Request
                engagementDetails.ValidateAdditionalCCSecction();
                string msgSendEmail = engagementDetails.ValidateSendEmailFunctionalityOfBillingRequest();
                Assert.AreEqual("Info", titleEngDetailsL);
                extentReports.CreateLog("Page with title : " + titleEngDetailsL + " is displayed when Send Email button is clicked after entering all required details on Billing Request page ");

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

    

