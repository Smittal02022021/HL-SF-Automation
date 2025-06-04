using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Opportunity;

namespace SF_Automation.TestCases.Engagement
{
    class TMTT0046984_VerificationOfEngCompanyRoundtripFlagFunctionalityOnTheBuysideDeals_NeitherSubjectNorBuyerIsAPotentialRoundTrip : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();

        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        LV_EngagementDetailsPage lvEngagementDetails = new LV_EngagementDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        LV_CompanyDetailsPage companyDetailsPage = new LV_CompanyDetailsPage();


        public static string fileTMTT0046984 = "TMTT0046984_VerificationOfEngCompanyRoundtripFlagFunctionalityOnTheBuysideDeals_NeitherSubjectNorBuyerIsAPotentialRoundTrip";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerificationOfEngCompanyRoundtripFlagFunctionalityOnTheBuysideDeals_NeitherSubjectNorBuyerIsAPotentialRoundTrip()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0046984;
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                string userCAOExl = ReadExcelData.ReadData(excelPath, "Users", 2);
                string valContactType = ReadExcelData.ReadData(excelPath, "AddContact", 4);
                string valContact = ReadExcelData.ReadData(excelPath, "AddContact", 1);
                string iconDesc = ReadExcelData.ReadData(excelPath, "HoverIcon", 1);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                //Validate user logged in
                Assert.AreEqual(driver.Url.Contains("lightning"), true);
                extentReports.CreateStepLogs("Passed", "Admin User is able to login into SF");

                int rowCount = ReadExcelData.GetRowCount(excelPath, "AddOpportunity");

                for(int row = 2; row <= rowCount; row++)
                {
                    //Select HL Banker app
                    try
                    {
                        lvHomePage.SelectAppLV("HL Banker");
                    }
                    catch(Exception)
                    {
                        lvHomePage.SelectAppLV1("HL Banker");
                    }

                    string valClientCompName = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 1);
                    string valSubjectCompName = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 2);
                    string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 3);
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 25);

                    //Search CF Financial user by global search
                    lvHomePage.SearchUserFromMainSearch(valUser);

                    //Verify searched user
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, valUser + " | Salesforce"), true);
                    extentReports.CreateLog("User " + valUser + " details are displayed ");

                    //Login as CF Financial user
                    lvHomePage.UserLogin();

                    //Switch to lightning view
                    if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                    {
                        homePage.SwitchToLightningView();
                        extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                    }

                    //Validate user logged in
                    Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(valUser));
                    extentReports.CreateStepLogs("Passed", "CF Financial User: " + valUser + " is able to login into lightning view. ");

                    //Navigate to Opportunities page
                    lvHomePage.NavigateToAnItemFromHLBankerDropdown("Opportunities");
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Recently Viewed | Opportunities | Salesforce"), true);
                    extentReports.CreateStepLogs("Passed", "User navigated to Opportunities list page. ");

                    //Validating Title of New Opportunity Page
                    string pageTitle = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                    Assert.IsTrue(pageTitle.Contains("New Opportunity"), "Verify user is on New opportunity pape for selected LOB ");
                    extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                    //Create New Opportunity
                    string opportunityName = addOpportunity.AddOpportunitiesMutipleRows(valJobType, fileTMTT0046984, row);
                    extentReports.CreateStepLogs("Info", "Opportunity : " + opportunityName + " is created ");

                    //Call function to enter Internal Team details and validate Opportunity detail page
                    string displayedTab = addOpportunity.EnterStaffDetailsL(fileTMTT0046984);
                    Assert.AreEqual(displayedTab, "Info");
                    extentReports.CreateStepLogs("Info", "User is on Opportunity detail " + displayedTab + " tab ");

                    //Validating Opportunity details  
                    string opportunityNumber = opportunityDetails.GetOpportunityNumberL();
                    Assert.IsNotNull(opportunityNumber);
                    extentReports.CreateStepLogs("Passed", "Opportunity with number : " + opportunityNumber + " is created ");

                    //Create External Primary Contact
                    addOpportunityContact.CickAddCFOpportunityContact();
                    addOpportunityContact.CreateContactL2(fileTMTT0046984);
                    extentReports.CreateStepLogs("Info", valContactType + " Opportunity contact is saved ");

                    //Update required Opportunity fields for conversion and Internal team details
                    opportunityDetails.UpdateReqFieldsForCFConversionMultipleRows(fileTMTT0046984, valJobType, row);
                    extentReports.CreateStepLogs("Info", "Opportunity Required Fields for Converting into Engagement are Filled ");

                    opportunityDetails.UpdateInternalTeamDetailsLV(fileTMTT0046984);
                    extentReports.CreateStepLogs("Info", "Opportunity Internal Team Details are provided ");

                    opportunityDetails.ClickReturnToOpportunityLV();
                    extentReports.CreateStepLogs("Info", "Return to Opportunity Detail page ");

                    //TC - End
                    lvHomePage.UserLogoutFromSFLightningView();
                    extentReports.CreateStepLogs("Info", "CF Financial User Logged Out from SF Lightning View. ");

                    //Select HL Banker app
                    try
                    {
                        lvHomePage.SelectAppLV("HL Banker");
                    }
                    catch(Exception)
                    {
                        lvHomePage.SelectAppLV1("HL Banker");
                    }

                    //Search for created opportunity
                    lvHomePage.SearchOpportunityFromMainSearch(opportunityName);
                    extentReports.CreateStepLogs("Info", "Admin User Search for Created Opportunity");

                    //update CC and NBC checkboxes in LV
                    opportunityDetails.UpdateOutcomeNBCApproveDetailsLV(valJobType);
                    extentReports.CreateStepLogs("Info", "CC and NBC checkboxes updated by Admin user. ");

                    //Requesting for engagement and validate the success message
                    opportunityDetails.ClickRequestToEngL();

                    //Submit Request To Engagement Conversion 
                    string msgSuccess = opportunityDetails.GetRequestToEngMsgL();
                    Assert.AreEqual(msgSuccess, "Opportunity has been submitted for Approval.");
                    extentReports.CreateStepLogs("Passed", "Success message: " + msgSuccess + " is displayed ");

                    //Search CAO Financial user by global search
                    lvHomePage.SearchUserFromMainSearch(userCAOExl);

                    //Verify searched user
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, userCAOExl + " | Salesforce"), true);
                    extentReports.CreateLog("User " + userCAOExl + " details are displayed ");

                    //Login as CAO user
                    lvHomePage.UserLogin();

                    //Switch to lightning view
                    if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                    {
                        homePage.SwitchToLightningView();
                        extentReports.CreateStepLogs("Passed", "CAO User: " + userCAOExl + " is able to login into lightning view. ");
                    }
                    else
                    {
                        extentReports.CreateStepLogs("Passed", "CAO User: " + userCAOExl + " is able to login into lightning view. ");
                    }

                    Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(userCAOExl));
                    extentReports.CreateStepLogs("Passed", "CAO User: " + userCAOExl + " is able to login into lightning view. ");

                    //Search for created opportunity
                    extentReports.CreateStepLogs("Info", " CAO User Search for Created Opportunity");
                    lvHomePage.SearchOpportunityFromMainSearch(opportunityName);

                    //Approve the Opportunity 
                    string status = opportunityDetails.ClickApproveButtonLV2();
                    Assert.AreEqual(status, "Approved");
                    extentReports.CreateStepLogs("Passed", "Opportunity " + status + " ");
                    opportunityDetails.CloseApprovalHistoryTabL();

                    //Calling function to convert to Engagement
                    opportunityDetails.ClickConvertToEngagementL2();
                    extentReports.CreateStepLogs("Info", "Opportunity Converted into Engagement ");

                    //Validate the Engagement name in Engagement details page
                    string engagementNumber = lvEngagementDetails.GetEngagementNumberL();
                    string engagementName = lvEngagementDetails.GetEngagementNameL();

                    //Need to get Name of Opp and Eng
                    Assert.AreEqual(opportunityName, engagementName);
                    extentReports.CreateStepLogs("Passed", "Name of Engagement : " + engagementName + " is Same as Opportunity name ");

                    //Close the engagement
                    lvEngagementDetails.ChangeEngagementStageToClosed();
                    lvEngagementDetails.CloseEstimatedRevenueDateReminderPopup();

                    string stage = lvEngagementDetails.GetEngagementStage();
                    Assert.AreEqual("Closed", stage);
                    extentReports.CreateStepLogs("Passed", "Stage of Engagement is change to : " + stage);

                    string compType = lvEngagementDetails.GetSubjectCompanyType(valSubjectCompName);
                    string clientOwnership = lvEngagementDetails.GetClientOwnership(valClientCompName);
                    
                    //Check Subject Company Type 
                    if(compType == "Operating Company")
                    {
                        //Check Client Ownership
                        if (clientOwnership == "Private Equity Group" || clientOwnership == "Family Office" || clientOwnership == "Hedge Fund" || clientOwnership == "Institutional Debt Holder")
                        {
                            //TMTI0114985 - Verify that if the user selects "Neither subject nor buyer are round trip" in Engagement is Potential Round Trip, 'SUBJECT' is OpCo, AND 'CLIENT' is OpCo PE Owned, warning message will appear, and verify the respective company updates

                            //Verify No warning message is displayed
                            lvEngagementDetails.SelectValueInPotentialRoundTripField("Neither subject nor buyer are round trip");
                            Assert.IsTrue(lvEngagementDetails.VerifyWarningMsgIsDisplayed());
                            extentReports.CreateStepLogs("Passed", "Warning Message is Displayed when user selects: Neither subject nor buyer are round trip under Engagement is a Potential Round Trip field when Subject Company = Operating Company & Client Ownership = Private Equity Group.");

                            //Verify updates on Subject Company
                            string subPotentialRoundTripExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyUpdates", row, 1);
                            string roundTripCommentExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyUpdates", row, 2);

                            lvEngagementDetails.VerifyUpdatesOnSubjectCompany(engagementName, subPotentialRoundTripExl, roundTripCommentExl, valSubjectCompName);
                            extentReports.CreateStepLogs("Passed", "Round Trip section of Subject Company is updated as follows: a) Potential RT = No b) RT Comment = Source – Engagement c) RT Engagement = " + engagementName + " d) RT Modified Date = " + DateTime.Now.ToString("MM/dd/yyyy").Replace('-', '/'));

                            //Verify Flag Reason and comments for Subject Company
                            string fReason = ReadExcelData.ReadDataMultipleRows(excelPath, "FlagReason", row, 1);
                            string fReasonComment = ReadExcelData.ReadDataMultipleRows(excelPath, "FlagReason", row, 2);

                            Assert.IsTrue(companyDetailsPage.VerifyFlagDetailsAreUpdatedForTheCompany(fReason, fReasonComment, userCAOExl));
                            extentReports.CreateStepLogs("Passed", "Flag details are updated for the company. \r\n Flag Reason: " + fReason + "\r\n Flag Reason Comment: " + fReasonComment + "\r\n Flag Reason Change By: " + userCAOExl + ".");

                            //Close duplicate company warning msg
                            companyDetailsPage.CloseDuplicateCompanyWarningMsg();

                            //Close Company tab
                            companyDetailsPage.CloseTab(valSubjectCompName);

                            //Verify updates on Client Company
                            string clientPotentialRoundTripExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyUpdates", row, 3);
                            lvEngagementDetails.VerifyUpdatesOnClientCompany(engagementName, clientPotentialRoundTripExl, roundTripCommentExl, valClientCompName);
                            extentReports.CreateStepLogs("Passed", "Round Trip section of Client Company is updated as follows: a) Potential RT = Yes b) RT Comment = Source – Engagement c) RT Engagement = " + engagementName + " d) RT Modified Date = " + DateTime.Now.ToString("MM/dd/yyyy").Replace('-', '/'));

                            //Verify Flag Reason and comments for Client Company
                            string fReason1 = ReadExcelData.ReadDataMultipleRows(excelPath, "FlagReason", row, 1);
                            string fReasonComment1 = ReadExcelData.ReadDataMultipleRows(excelPath, "FlagReason", row, 3);

                            Assert.IsTrue(companyDetailsPage.VerifyFlagDetailsAreUpdatedForTheCompany(fReason1, fReasonComment1, userCAOExl));
                            extentReports.CreateStepLogs("Passed", "Flag details are updated for the company. \r\n Flag Reason: " + fReason1 + "\r\n Flag Reason Comment: " + fReasonComment1 + "\r\n Flag Reason Change By: " + userCAOExl + ".");

                            //Close duplicate company warning msg
                            companyDetailsPage.CloseDuplicateCompanyWarningMsg();

                            //Close Company tab
                            companyDetailsPage.CloseTab(valClientCompName);
                        }
                        else
                        {
                            //TMTI0115069 - Verify that if the user selects "Neither subject nor buyer are round trip" in Engagement is a Potential Round Trip AND 'SUBJECT' is OpCo & 'CLIENT(Buyer)' is NOT PE or PE Owned, warning message will appear, and verify the respective company updates

                            //Verify warning message should be displayed
                            lvEngagementDetails.SelectValueInPotentialRoundTripField("Neither subject nor buyer are round trip");
                            Assert.IsTrue(lvEngagementDetails.VerifyWarningMsgIsDisplayed());
                            extentReports.CreateStepLogs("Passed", "Warning Message is Displayed when user selects: Neither subject nor buyer are round trip under Engagement is a Potential Round Trip field when Subject Company = Operating Company & Client Ownership != Private Equity Group.");

                            string msg = ReadExcelData.ReadData(excelPath, "Warning", 2);
                            Assert.IsTrue(lvEngagementDetails.VerifBuyeryWarningMsg(msg));
                            extentReports.CreateStepLogs("Passed", "Expected warning message is displayed : " + msg);

                            //Verify updates on Subject Company
                            string subPotentialRoundTripExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyUpdates", row, 1);
                            string roundTripCommentExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyUpdates", row, 2);

                            lvEngagementDetails.VerifyUpdatesOnSubjectCompany(engagementName, subPotentialRoundTripExl, roundTripCommentExl, valSubjectCompName);
                            extentReports.CreateStepLogs("Passed", "Round Trip section of Subject Company is updated as follows: a) Potential RT = No b) RT Comment = Source – Engagement c) RT Engagement = " + engagementName + " d) RT Modified Date = " + DateTime.Now.ToString("MM/dd/yyyy").Replace('-', '/'));

                            //Verify Flag Reason and comments for Subject Company
                            string fReason4 = ReadExcelData.ReadDataMultipleRows(excelPath, "FlagReason", row, 1);
                            string fReasonComment4 = ReadExcelData.ReadDataMultipleRows(excelPath, "FlagReason", row, 2);

                            Assert.IsTrue(companyDetailsPage.VerifyFlagDetailsAreUpdatedForTheCompany(fReason4, fReasonComment4, userCAOExl));
                            extentReports.CreateStepLogs("Passed", "Flag details are updated for the company. \r\n Flag Reason: " + fReason4 + "\r\n Flag Reason Comment: " + fReasonComment4 + "\r\n Flag Reason Change By: " + userCAOExl + ".");

                            //Close duplicate company warning msg
                            companyDetailsPage.CloseDuplicateCompanyWarningMsg();

                            //Close Company tab
                            companyDetailsPage.CloseTab(valSubjectCompName);

                            //Verify updates on Client Company
                            string clientPotentialRoundTripExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyUpdates", row, 3);
                            lvEngagementDetails.VerifyUpdatesOnClientCompany(engagementName, clientPotentialRoundTripExl, roundTripCommentExl, valClientCompName);
                            extentReports.CreateStepLogs("Passed", "Round Trip section of Client Company is updated as follows: a) Potential RT = Yes b) RT Comment = Source – Engagement c) RT Engagement = " + engagementName + " d) RT Modified Date = " + DateTime.Now.ToString("MM/dd/yyyy").Replace('-', '/'));

                            //Verify Flag Reason and comments for Client Company
                            string fReason5 = ReadExcelData.ReadData(excelPath, "FlagReason", 1);
                            string fReasonComment5 = ReadExcelData.ReadData(excelPath, "FlagReason", 3);

                            Assert.IsTrue(companyDetailsPage.VerifyFlagDetailsAreUpdatedForTheCompany(fReason5, fReasonComment5, userCAOExl));
                            extentReports.CreateStepLogs("Passed", "Flag details are updated for the company. \r\n Flag Reason: " + fReason5 + "\r\n Flag Reason Comment: " + fReasonComment5 + "\r\n Flag Reason Change By: " + userCAOExl + ".");

                            //Close duplicate company warning msg
                            companyDetailsPage.CloseDuplicateCompanyWarningMsg();

                            //Close Company tab
                            companyDetailsPage.CloseTab(valClientCompName);
                        }
                    }
                    else
                    {
                        if(clientOwnership == "Private Equity Group" || clientOwnership == "Family Office" || clientOwnership == "Hedge Fund" || clientOwnership == "Institutional Debt Holder")
                        {
                            //TMTI0114988 - Verify that if the user selects "Neither subject nor buyer are round trip" in Engagement is a Potential Round Trip AND 'SUBJECT' is NOT OpCo & 'CLIENT' is PE or PE Owned, no warning message should appear on the screen

                            //Verify warning message should be displayed
                            lvEngagementDetails.SelectValueInPotentialRoundTripField("Neither subject nor buyer are round trip");
                            Assert.IsTrue(lvEngagementDetails.VerifyNoWarningMsgIsDisplayed());
                            extentReports.CreateStepLogs("Passed", "Warning Message is Displayed when user selects: Neither subject nor buyer are round trip under Engagement is a Potential Round Trip field when Subject Company != Operating Company & Client Ownership = Private Equity Group.");

                            //Verify updates on Subject Company
                            string subPotentialRoundTripExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyUpdates", row, 1);
                            string roundTripCommentExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyUpdates", row, 2);

                            lvEngagementDetails.VerifyUpdatesOnSubjectCompany(engagementName, subPotentialRoundTripExl, roundTripCommentExl, valSubjectCompName);
                            extentReports.CreateStepLogs("Passed", "Round Trip section of Subject Company is updated as follows: a) Potential RT = No b) RT Comment = Source – Engagement c) RT Engagement = " + engagementName + " d) RT Modified Date = " + DateTime.Now.ToString("MM/dd/yyyy").Replace('-', '/'));

                            //Verify Flag Reason and comments for Subject Company
                            string fReason2 = "";
                            string fReasonComment2 = "";

                            Assert.IsTrue(companyDetailsPage.VerifyFlagDetailsAreUpdatedForTheCompany(fReason2, fReasonComment2, userCAOExl));
                            extentReports.CreateStepLogs("Passed", "Flag details are updated for the company. \r\n Flag Reason: " + fReason2 + "\r\n Flag Reason Comment: " + fReasonComment2 + "\r\n Flag Reason Change By: " + userCAOExl + ".");

                            //Close duplicate company warning msg
                            companyDetailsPage.CloseDuplicateCompanyWarningMsg();

                            //Close Company tab
                            companyDetailsPage.CloseTab(valSubjectCompName);

                            //Verify updates on Client Company
                            string clientPotentialRoundTripExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyUpdates", row, 3);
                            lvEngagementDetails.VerifyUpdatesOnClientCompany(engagementName, clientPotentialRoundTripExl, roundTripCommentExl, valClientCompName);
                            extentReports.CreateStepLogs("Passed", "Round Trip section of Client Company is updated as follows: a) Potential RT = Yes b) RT Comment = Source – Engagement c) RT Engagement = " + engagementName + " d) RT Modified Date = " + DateTime.Now.ToString("MM/dd/yyyy").Replace('-', '/'));

                            //Verify Flag Reason and comments for Client Company
                            string fReason3 = "";
                            string fReasonComment3 = "";

                            Assert.IsTrue(companyDetailsPage.VerifyFlagDetailsAreUpdatedForTheCompany(fReason3, fReasonComment3, userCAOExl));
                            extentReports.CreateStepLogs("Passed", "Flag details are updated for the company. \r\n Flag Reason: " + fReason3 + "\r\n Flag Reason Comment: " + fReasonComment3 + "\r\n Flag Reason Change By: " + userCAOExl + ".");


                            //Close duplicate company warning msg
                            companyDetailsPage.CloseDuplicateCompanyWarningMsg();

                            //Close Company tab
                            companyDetailsPage.CloseTab(valClientCompName);
                        }
                        else
                        {
                            //TMTI0115071 - Verify that if the user selects "Neither subject nor buyer are round trip" in Engagement is a Potential Round Trip AND 'SUBJECT' is NOT OpCo & 'CLIENT' is NOT PE or PE Owned, the warning message will appear, and verify the respective company updates.

                            //Verify warning message should be displayed
                            lvEngagementDetails.SelectValueInPotentialRoundTripField("Neither subject nor buyer are round trip");
                            Assert.IsTrue(lvEngagementDetails.VerifyWarningMsgIsDisplayed());
                            extentReports.CreateStepLogs("Passed", "Warning Message is Displayed when user selects: Neither subject nor buyer are round trip under Engagement is a Potential Round Trip field when Subject Company != Operating Company & Client Ownership != Private Equity Group.");

                            string msg = ReadExcelData.ReadData(excelPath, "Warning", 3);
                            Assert.IsTrue(lvEngagementDetails.VerifyWarningMsg(msg));
                            extentReports.CreateStepLogs("Passed", "Expected warning message is displayed : " + msg);

                            //Verify updates on Subject Company
                            string subPotentialRoundTripExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyUpdates", row, 1);
                            string roundTripCommentExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyUpdates", row, 2);

                            lvEngagementDetails.VerifyUpdatesOnSubjectCompany(engagementName, subPotentialRoundTripExl, roundTripCommentExl, valSubjectCompName);
                            extentReports.CreateStepLogs("Passed", "Round Trip section of Subject Company is updated as follows: a) Potential RT = No b) RT Comment = Source – Engagement c) RT Engagement = " + engagementName + " d) RT Modified Date = " + DateTime.Now.ToString("MM/dd/yyyy").Replace('-', '/'));

                            //Verify Flag Reason and comments for subject Company
                            string fReason6 = ReadExcelData.ReadData(excelPath, "FlagReason", 1);
                            string fReasonComment6 = ReadExcelData.ReadData(excelPath, "FlagReason", 2);

                            Assert.IsTrue(companyDetailsPage.VerifyFlagDetailsAreUpdatedForTheCompany(fReason6, fReasonComment6, userCAOExl));
                            extentReports.CreateStepLogs("Passed", "Flag details are updated for the company. \r\n Flag Reason: " + fReason6 + "\r\n Flag Reason Comment: " + fReasonComment6 + "\r\n Flag Reason Change By: " + userCAOExl + ".");

                            //Close duplicate company warning msg
                            companyDetailsPage.CloseDuplicateCompanyWarningMsg();

                            //Close Company tab
                            companyDetailsPage.CloseTab(valSubjectCompName);

                            //Verify updates on Client Company
                            string clientPotentialRoundTripExl = ReadExcelData.ReadDataMultipleRows(excelPath, "CompanyUpdates", row, 3);
                            lvEngagementDetails.VerifyUpdatesOnClientCompany(engagementName, clientPotentialRoundTripExl, roundTripCommentExl, valClientCompName);
                            extentReports.CreateStepLogs("Passed", "Round Trip section of Client Company is updated as follows: a) Potential RT = Yes b) RT Comment = Source – Engagement c) RT Engagement = " + engagementName + " d) RT Modified Date = " + DateTime.Now.ToString("MM/dd/yyyy").Replace('-', '/'));

                            //Verify Flag Reason and comments for Client Company
                            string fReason7 = ReadExcelData.ReadData(excelPath, "FlagReason", 1);
                            string fReasonComment7 = ReadExcelData.ReadData(excelPath, "FlagReason", 3);

                            Assert.IsTrue(companyDetailsPage.VerifyFlagDetailsAreUpdatedForTheCompany(fReason7, fReasonComment7, userCAOExl));
                            extentReports.CreateStepLogs("Passed", "Flag details are updated for the company. \r\n Flag Reason: " + fReason7 + "\r\n Flag Reason Comment: " + fReasonComment7 + "\r\n Flag Reason Change By: " + userCAOExl + ".");

                            //Close duplicate company warning msg
                            companyDetailsPage.CloseDuplicateCompanyWarningMsg();

                            //Close Company tab
                            companyDetailsPage.CloseTab(valClientCompName);
                        }
                    }

                    //TC - End
                    lvHomePage.LogoutFromSFLightningAsApprover();
                    extentReports.CreateStepLogs("Info", "CF Financial User Logged Out from SF Lightning View. ");
                }
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.Quit();
            }
        }
    }
}
