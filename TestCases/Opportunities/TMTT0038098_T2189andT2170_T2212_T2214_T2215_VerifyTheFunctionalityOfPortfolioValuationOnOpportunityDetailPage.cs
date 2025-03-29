
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;     
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class TMTT0038098_T2189andT2170_T2212_T2214_T2215_VerifyTheFunctionalityOfPortfolioValuationOnOpportunityDetailPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        OppValuationPeriods period = new OppValuationPeriods();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engDetails = new EngagementDetailsPage();
        
        public static string fileTC1644 = "TMTT0038098_VerifyTheFunctionalityOfPortfolioValuationOnOpportunityDetailPage.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void FunctionalityOfPortfolioValuation()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC1644;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Calling Login function                
                login.LoginApplication();

                // Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user               
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("User: " + stdUser + " logged in ");

                //Verify the availability of Opportunity under HL Banker list
                string tagOpp = opportunityHome.ValidateOppUnderHLBanker();
                Assert.AreEqual("Opportunities", tagOpp);
                extentReports.CreateLog(tagOpp + " is displayed under HL Banker dropdown ");

                //Verify that choose LOB is displayed after clicking New button
                string valRecordType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 25);
                string titleOpp = opportunityHome.ClickNewButtonAndSelectOppRecordTypeLV(valRecordType);
                Assert.AreEqual("New Opportunity: " + valRecordType, titleOpp);
                extentReports.CreateLog("Page with title: " + titleOpp + " is displayed upon clicking next button ");

                //Add FVA Opportunity
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                string value = addOpportunity.AddOpportunitiesLightning(valJobType, fileTC1644);

                //Call function to enter Internal Team details and validate Opportunity detail page
                string displayedTab = addOpportunity.EnterStaffDetailsL(fileTC1644);
                Assert.AreEqual("Info", displayedTab);
                extentReports.CreateLog("Tab with name: " + displayedTab + " is displayed upon saving internal deal team members details ");

                //1.  TMTI0092012_Verify that the "Portfolio Valuation" button is available on the portfolio opportunities. – Completed
                string portfolioValuation = opportunityDetails.ValidatePortfolioValuationButtonL();
                string jobTypePV = opportunityDetails.GetJobTypeL();
                Assert.AreEqual("Portfolio Valuation", portfolioValuation);
                extentReports.CreateLog("Portfolio Valuation Button is displayed for the Opportunity with Job type: " + jobTypePV + " ");

                ////2.  TMTI0092014_Verify that the "Portfolio Valuation" button is not available on opportunities other than portfolio job types. – Completed
                //opportunityHome.ClickOpportunityTab();
                //opportunityHome.ValidateSearchFunctionalityOfOpportunities("119337");
                //string portfolioValuationNo = opportunityDetails.ValidatePortfolioValuationButton();
                //string jobType = opportunityDetails.Get2ndJobTypeL();
                //Assert.AreEqual("Portfolio Valuation button is not displayed", portfolioValuationNo);
                //extentReports.CreateLog("Portfolio Valuation Button is not displayed for the Opportunity with Job type: " + jobType + " ");

                //3.  TMTI0092016_Verify that clicking the "Portfolio Valuation" button opens up a new tab to add the valuation period with buttons and messages on the screen
                //opportunityHome.Click1stOpportunityTab();
                string message = opportunityDetails.ClickPortfolioValuationL();
                Assert.AreEqual("Currently there are no valuation periods for this Opportunity. To proceed, please create a new valuation period.", message);
                extentReports.CreateLog("Message : " + message + " ");

                string backToOpp = opportunityDetails.ValidateReturnToOppButton();
                Assert.AreEqual("Back To Opportunity button is displayed", backToOpp);
                extentReports.CreateLog("Button : " + backToOpp + " ");

                string newOppVal = opportunityDetails.ValidateNewOppValuationPeriodButton();
                Assert.AreEqual("New Opportunity Valuation Period button is displayed", newOppVal);
                extentReports.CreateLog("Button : " + newOppVal + " ");

                //4.  TMTI0092018_ Verify that clicking "Back to Opportunity" takes the user back to the Opportunity detail page
                string oppDetails = opportunityDetails.ValidateOppDetailsPageUponClickOfBackToOppButton();
                Assert.AreEqual("Details", oppDetails);
                extentReports.CreateLog("Opportunity details page is displayed upon clicking Back To Opportunity button ");

                //5.  TMTI0092020_Verify that clicking "New Opportunity Valuation Period" opens up the Opportunity Valuation period edit page has required fields with Save and Cancel buttons
                Assert.IsTrue(period.ClickOppValuationAndValidateFields(), "Verified that displayed fields are same");
                extentReports.CreateLog("Displayed fields on Opportunity Valuation Period are as expected ");

                Assert.IsTrue(period.ValidateButtonsOnValuationPeriod(), "Verified that displayed buttons are same");
                extentReports.CreateLog("Displayed buttons on Opportunity Valuation Period are as expected ");

                //6.  TMTI0092022_Verify that the validation message appears for required fields on clicking the Save button of the Opportunity Valuation Period Edit page. 
                Assert.IsTrue(period.ValidateMandatoryFieldValidationsOnClickOfSaveButton(), "Verified that displayed mandatory field validations are same");
                extentReports.CreateLog("Displayed mandatory field validations on Opportunity Valuation Period are as expected ");

                //7.  TMTI0092023_Verify that clicking the Cancel button of the Opportunity Valuation Period edit page takes the user back to the Valuation Periods Listing View. 
                string oppDetailsUponCancel = period.ValidateOppDetailsPageUponClickingCancelButton();
                Assert.AreEqual("Currently there are no valuation periods for this Opportunity. To proceed, please create a new valuation period.", oppDetailsUponCancel);
                extentReports.CreateLog("Valuation Period page is displayed upon clicking cancel button on Opportunity Valuation Period edit page ");

                //8.  TMTI0092025_Verify that the "New Opportunity Valuation Period" is created by clicking the Save button on the Opportunity Valuation Period Edit page and redirecting the user to the Valuation Period detail page. 
                string name = CustomFunctions.RandomValue();
                string addedValuation = period.EnterAndSaveOppValuationPeriodDetailsL(name);
                Assert.AreEqual(name, addedValuation);
                extentReports.CreateLog("Added valuation: " + addedValuation + " is displayed upon clicking Save button on Opportunity Valuation Period edit page after entering all mandatory details ");

                //9.  TMTI0092027_Verify the sections, fields, and buttons available on the Opportunity Valuation Period Detail page
                Assert.IsTrue(period.ValidateMainSectionsOfOppValuationPeriodDetail(), "Verified that displayed main sections of Opportunity Valuation Period Detail page are same");
                extentReports.CreateLog("Displayed main sections on Opportunity Valuation Period Detail page are as expected ");

                Assert.IsTrue(period.ValidateSectionsOfOppValuationPeriodDetail(), "Verified that displayed sections of Opportunity Valuation Period Detail page are same");
                extentReports.CreateLog("Displayed sections on Opportunity Valuation Period Detail page are as expected ");

                Assert.IsTrue(period.ValidateButtonsOfOppValuationPeriodDetail(), "Verified that displayed buttons on Opportunity Valuation Period Detail page are same");
                extentReports.CreateLog("Displayed buttons on Opportunity Valuation Period Detail page are as expected ");

                //10. TMTI0092029_Verify that clicking the "Back to Opp Valuation Period List" button takes the user to the valuation period listing page.
                string valPeriods = period.ValidateOppDetailsPageUponClickOfBackToOppButton();
                Assert.AreEqual(value + " - Valuation Periods", valPeriods);
                extentReports.CreateLog("Valuation period listing page is displayed upon clicking Back to Opp Valuation Period List button ");

                //11.  TMTI0092031_Verify that the "Edit" action button given corresponds to each valuation period and allows the user to edit the Valuation Period name and valuation date.
                string updatedPeriod = period.EditFunctionalityOfValuationPeriod("Testing");
                Assert.AreEqual("Testing", updatedPeriod);
                extentReports.CreateLog("Updated valuation Period name is displayed on Valuation period listing page after updating the name ");

                //12.  TMTI0092033_Verify that clicking "Valuation Period Name" from the valuation period listing page will take the user to the Opportunity Valuation Period Detail page
                string titlePeriodDetail = period.ValidateOppValPeriodDetailPageUponClickOfValPeriodNameLink();
                Assert.AreEqual("Opportunity Valuation Period", titlePeriodDetail);
                extentReports.CreateLog("Page : " + titlePeriodDetail + " is displayed on clicking the Valuation Period Name link on valuation period listing page ");

                //13.  TMTI0092035_Verify that if there is no existing valuation period to import positions, a message will display on the screen on clicking the "Import Positions" button
                string msgImport = period.ValidateMessageWhileClickingOnImportButton();
                Assert.AreEqual("There is no valuation period available to import position.", msgImport);
                extentReports.CreateLog("Message : " + msgImport + " is displayed on clicking Import Positions button when there is no existing valuation period ");

                //14.  TMTI0092037_Verify that clicking the "New Opp Valuation Period Position" button to add positions opens up the form
                string titlePeriodPosition = period.ValidatePeriodPositionPageWhileClickingNewOppValPeriodPositionButton();
                Assert.AreEqual("New Opp Valuation Period Position", titlePeriodPosition);
                extentReports.CreateLog("Page : " + titlePeriodPosition + " is displayed on clicking New Opp Valuation Period Position button ");

                Assert.IsTrue(period.ValidateFieldsOfOppValuationPeriodPositionL(), "Verified that displayed fields on Opportunity Valuation Period Position page are same");
                extentReports.CreateLog("Displayed fields on Opportunity Valuation Period Position page are as expected ");

                Assert.IsTrue(period.ValidateButtonsOnValuationPeriod(), "Verified that displayed buttons on Opportunity Valuation Period Position page are same");
                extentReports.CreateLog("Displayed buttons on Opportunity Valuation Period Position page are as expected ");

                //15.  TMTI0092039_Verify that the validation message appears for the required fields on clicking the "Save" button on the "New Opp Valuation Period Position" detail page
                Assert.IsTrue(period.ValidateMessageWhileClickingSaveButtonOnPeriodPosition(), "Verified that displayed mandatory field validations are same");
                extentReports.CreateLog("Displayed mandatory field validations on Opportunity Valuation Period Position are as expected ");

                //16.  TMTI0092041_Verify that clicking the "Cancel" button given on the New Opp Valuation Period Position takes the user back to the Opportunity Valuation Period Detail page.
                string oppValPeriodDetailsUponCancel = period.ValidateOppValPeriodDetailsPageUponClickingCancelButton();
                Assert.AreEqual("Opportunity Valuation Period Detail", oppValPeriodDetailsUponCancel);
                extentReports.CreateLog("Page: " + oppValPeriodDetailsUponCancel + " is displayed upon clicking cancel button on New Opp Valuation Period Position page ");

                //17.  TMTI0092043_Verify that the user can add positions by clicking the "New Opp Valuation Period Position" button with all the required details 
                string addedPosition = period.EnterAndSaveOppValuationPeriodPositionDetailsL("Techno Alpha");
                Assert.AreEqual("Techno Alpha", addedPosition);
                extentReports.CreateLog("Position: " + addedPosition + " is displayed upon clicking Save button after entering all mandatory details of Period Position ");

                //18.  TMTI0092045_Verify that clicking "Opp Valuation Period Position Name" opens up the Opp Valuation Period Position details page
                string oppValPositionDetail = period.ValidateOppValPeriodPositionPageUponClickingAddedPeriodPosition();
                Assert.AreEqual("Opportunity Valuation Position Detail", oppValPositionDetail);
                extentReports.CreateLog("Page: " + oppValPositionDetail + " is displayed upon clicking on added Period Position Name ");

                //19.  TMTI0092047_Verify that clicking the "Back to Valuation Period" button given on the "Opp Valuation Period Position" page takes the user back to the Opportunity Valuation Period detail page
                string oppValPeriodDetailsUponBack = period.ValidateOppValPeriodDetailsPageUponClickingBackToValuationButton();
                Assert.AreEqual("Opportunity Valuation Period Detail", oppValPeriodDetailsUponBack);
                extentReports.CreateLog("Page: " + oppValPeriodDetailsUponBack + " is displayed upon clicking Back to Valuation Period button on Opp Valuation Period Position page ");

                //20.  TMTI0092049_Verify that clicking the "Edit" button given on "Opp Valuation Period Position" allows the user to update Opp Valuation Period position details and updates get reflected on the position
                string updatedPosition = period.EditFunctionalityOfPeriodPosition("XYZ");
                Assert.AreEqual("XYZ", updatedPosition);
                extentReports.CreateLog("Updated Position name is displayed on Valuation Position Detail page after updating the name ");

                //21.  TMTI0092051_Verify that the "Opp Valuation Period Team Members" section is available at the bottom of the Opp Valuation Period Position detail page. 
                string secTeamMember = period.ValidateSecOppValTeamMember();
                Assert.AreEqual("Opp Valuation Period Team Members", secTeamMember);
                extentReports.CreateLog("Section: " + secTeamMember + " is displayed at the bottom of the Opp Valuation Period Position detail page ");

                string msgTeamMember = period.ValidateAddTeamMemberMessage();
                Assert.AreEqual("Please add new team members to this position by selecting the 'Add New Team Member' button.", msgTeamMember);
                extentReports.CreateLog("Message: " + msgTeamMember + " is displayed at the bottom of the Opp Valuation Period Position detail page ");

                string buttonTeamMember = period.ValidateAddNewTeamMemberButton();
                Assert.AreEqual("Add New Team Member", buttonTeamMember);
                extentReports.CreateLog("Button with name: " + buttonTeamMember + " is displayed at the bottom of the Opp Valuation Period Position detail page ");

                //22. TMTI0092053_Verify that on clicking "Add New Team Member" opens up fields like a staff member and with Save Team Member and Delete action buttons
                Assert.IsTrue(period.ValidateTeamMemberColumns(), "Verified that displayed Team members table columns are same");
                extentReports.CreateLog("Displayed Team members table columns on Opportunity Valuation Period Position are as expected ");

                string saveTeam = period.ValidateSaveTeamMemberButton();
                Assert.AreEqual("Save Team Members", saveTeam);
                extentReports.CreateLog("Button with name: " + saveTeam + " is displayed in " + secTeamMember + " ");

                string deleteTeam = period.ValidateDeleteLinkTeamMember();
                Assert.AreEqual("Delete", deleteTeam);
                extentReports.CreateLog("Link with name: " + deleteTeam + " is displayed in " + deleteTeam + " ");

                //23. TMTI0092055_Verify that clicking "Save Team Member" adds the user as a Team Member on the selected role with Status
                string addedStaff = period.SaveTeamMembersAndValidate();
                string addedRole = period.GetSavedRoleOfStaff();
                Assert.AreEqual("Associate", addedRole);
                Assert.AreEqual("Karan Chopra", addedStaff);
                extentReports.CreateLog("Staff with name: " + addedStaff + " and Role: " + addedRole + " is displayed after saving details ");

                //24.  TMTI0092057_Verify the functionality of the "Delete" action button that appears while adding a new team member on the Opportunity Valuation Position Detail
                string cancelTeam = period.ValidateCancelFunctionalityOfTeamMembers();
                Assert.AreEqual("Delete", cancelTeam);
                extentReports.CreateLog("Team Member is not deleted after clicking Cancel button post clicking Delete link ");

                string delTeam = period.ValidateDeleteFunctionalityOfTeamMembers();
                Assert.AreEqual("Team member is deleted", delTeam);
                extentReports.CreateLog("Team Member got deleted after clicking Ok button post clicking Delete link ");

                //25. TMTI0092059_Verify that clicking the "Import Positions" button opens up a screen that shows the "Existing Valuation Period" listing with the following buttons
                period.ClickHLRelatedTab();
                string name1 = CustomFunctions.RandomValue();
                string addedValuation2nd = period.EnterAndSaveOppValuationPeriodDetailsL(name1);
                period.ClickImportButton();
                Assert.IsTrue(period.ValidateButtonsOfExistingValPeriodL(), "Verified that displayed buttons are same");
                extentReports.CreateLog("Displayed buttons on Existing Valuation Period are as expected ");

                //26.  TMTI0092061_Verify that the list of related positions opens from the existing valuation period on clicking the "Search Valuation Period for Positions" button. 
                Assert.IsTrue(period.ValidateDisplayedImportButtonsUponClickingSearchValPeriod(), "Verified that displayed Import Position buttons are same");
                extentReports.CreateLog("Displayed Import Position buttons are as expected ");

                Assert.IsTrue(period.ValidateDisplayedBottomButtonsUponClickingSearchValPeriod(), "Verified that displayed buttons at the bottom of Related Positions page are same");
                extentReports.CreateLog("Displayed buttons at the bottom of Related Positions page are as expected ");

                //27.  TMTI0092063_ Verify that the user can Import Positions with Team Members from the existing valuation period and all the details are imported successfully
                string importWithTeam = period.ValidateImportWithTeamMembers();
                Assert.AreEqual("XYZ", importWithTeam);
                extentReports.CreateLog("Period position with name: " + importWithTeam + " has been added successfully with team member ");

                //28. TMTI0092065_Verify that the user can Import Positions without a Team Member from the existing valuation period and all the details are imported successfully except the Team Member
                period.ClickBackToOppValPeriodList();
                string name2 = CustomFunctions.RandomValue();
                string addedValuation3rd = period.EnterAndSaveOppValuationPeriodDetailsL(name2);
                period.ClickImportButton();

                string importWithoutTeam = period.ValidateImportWithoutTeamMembers();
                Assert.AreEqual("XYZ", importWithoutTeam);
                extentReports.CreateLog("Period position with name: " + importWithTeam + " has been added successfully without team member ");

                //29.  TMTI0092067_Verify that the Deal Team Member is not allowed to update the status from In-Progress to Completed on the Opportunity Valuation. 
                string statusPeriodPosition = period.ValidateEditFunctionalityOfPeriodPositionWithDealTeamMember();
                Assert.AreEqual("In Progress", statusPeriodPosition);
                extentReports.CreateLog("Status of Period position : " + statusPeriodPosition + " is  displayed with no option to deal team member to update it ");

                //31. TMTI0092071_Verify that the "Delete" button is not available and allowed to delete "Opp Valuation Period" as a deal team member.
                string deleteValPeriod = period.ValidateDeleteFunctionalityOfValPeriodWithDealTeamMember();
                Assert.AreEqual("Delete button is not displayed", deleteValPeriod);
                extentReports.CreateLog(deleteValPeriod + " for deal team member to delete Opportunity Valuation Period ");

                //30.  TMTI0092069_Verify that the "Delete" button is not available and allowed to delete "Opp Valuation Period Positions" as a deal team member.
                string deletePeriodPosition = period.ValidateDeleteFunctionalityOfPeriodPositionWithDealTeamMember();
                Assert.AreEqual("Delete button is not displayed", deletePeriodPosition);
                extentReports.CreateLog(deletePeriodPosition + " for deal team member to delete Period Position ");

                //32. TMTI0092074_ Verify that the CAO can add Portfolio Valuation period and position including Report Fees on the Opportunity Valuation
                //33.  TMTI0092076_Verify that the CAO can edit the Portfolio Valuation period and position including Report Fees on the Opportunity Valuation

                usersLogin.DiffLightningLogout();
                string valCAOUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);
                usersLogin.SearchUserAndLogin(valCAOUser);
                string caoUser = login.ValidateUserLightningCAO3rd();
                Console.WriteLine("caoUser:" + caoUser);
                Console.WriteLine("valCAOUser:" + valCAOUser.Substring(0, 12));
                Assert.AreEqual(caoUser.Contains(valCAOUser.Substring(1, 10)), true);
                extentReports.CreateLog("User: " + valCAOUser + " logged in ");

               // string name = "30552024095507";

                opportunityHome.SearchMyOpportunitiesInLightning(value, caoUser);
                opportunityDetails.ClickPortfolioValuationCAOL();

                string nameCAO = CustomFunctions.RandomValue();
                string addedValuationCAO = period.EnterAndSaveOppValuationPeriodDetailsL(name);
                Assert.AreEqual(name, addedValuationCAO);
                extentReports.CreateLog("Added valuation: " + addedValuationCAO + " is displayed upon clicking Save button on Opportunity Valuation Period edit page after entering all mandatory details by "+ caoUser + " ");

                period.ValidateOppDetailsPageUponClickOfBackToOppButton();
                string updValuationCAO= period.EditFunctionalityOfValuationPeriod("Testing CAO");
                Assert.AreEqual("Testing CAO", updValuationCAO);
                extentReports.CreateLog("Updated valuation Period name "+ updValuationCAO + " is displayed on Valuation period listing page after updating the name by " + caoUser + " ");

                //Add Period Position
                period.ValidateOppValPeriodDetailPageUponClickOfValPeriodNameLink();
                period.ClickNewPeriodPositionButtonL();
                string addedPositionCAO = period.EnterAndSaveOppValuationPeriodPositionDetailsL("AB Enterprises");
                Assert.AreEqual("AB Enterprises", addedPositionCAO);
                extentReports.CreateLog("Position: " + addedPositionCAO + " is displayed upon clicking Save button after entering all mandatory details of Period Position by "+ caoUser + " ");
                
                //Edit Period Position
                string updPositionCAO = period.EditFunctionalityOfPeriodPosition("ABC");
                Assert.AreEqual("ABC", updPositionCAO);
                extentReports.CreateLog("Updated Position name is displayed on Valuation Position Detail page after updating the name by " + caoUser + " ");

                //36.  TMTI0092079_Verify that the CAO is not allowed to update the status from In-Progress to Completed of the Opportunity Valuation. 
                string statusPeriodPositionCAO = period.ValidateStatusOfPeriodPositionWithCAO();
                Assert.AreEqual("In Progress", statusPeriodPositionCAO);
                extentReports.CreateLog("Status of Period position : " + statusPeriodPositionCAO + " is  displayed with no option to CAO to update it ");

                //34. TMTI0092078_Verify that the CAO can "Delete" the "Opportunity Valuation Period Position". 
                string cancelPositionCAO = period.ValidateWhenNoIsSelectedUponClickingDeleteButton();
                Assert.AreEqual("AB Enterprises", cancelPositionCAO);
                extentReports.CreateLog("Position name is not deleted on Opportunity Valuation Period Detail page after clicking No on Delete confirmation pop up for " + caoUser + " ");

                string delPositionCAO = period.ValidateWhenYesIsSelectedUponClickingDeleteButton();
                Assert.AreNotEqual("AB Enterprises", delPositionCAO);
                extentReports.CreateLog("Position name is deleted on Opportunity Valuation Period Detail page after clicking Ok on Delete confirmation pop up " + caoUser + " ");

                //35.  TMTI0092081_Verify that the CAO can "Delete" the "Opportunity Valuation Period".                
                string delPeriodCAO= period.ValidateDeleteFunctionalityOfOppValPeriod();
                Assert.AreNotEqual("Back To Opportunity", delPositionCAO);
                extentReports.CreateLog("Opp Valuation Period is deleted on Opportunity Valuation Period Detail page after clicking Delete button by " + caoUser + " ");

                //38. Verify that the reports are available on the Opp Valuation Period detail page for CAO.
                Assert.IsTrue(period.ValidateReportSectionOfOppValPeriod(), "Verified that displayed reports are same");
                extentReports.CreateLog("Displayed reports are as expected for CAO: " + caoUser + " ");

                //37.  TMTI0092383_Verify that once the opportunity gets converted into engagement, the CAO is not able to perform any action or add a new opp valuation period
                //39.  TMTI0092387_Verify that once the opportunity gets converted into engagement, the FVA User is not able to perform any action or add a new opp valuation period. 
                engHome.SelectEngUnderHLBanker();
                engHome.ValidateSearchFunctionalityOfEngagements("FGIC 2024");
                engHome.ClickEngNumber();
               string newOppValPeriodCAO= engDetails.ValidateNewOppValPeriodButtonOfRelatedOpp(caoUser);
                Assert.AreEqual("New Opportunity Valuation Period button is not displayed", newOppValPeriodCAO);
                extentReports.CreateLog(caoUser + " is not allowed to add New Opportunity Valuation Period " );

                usersLogin.DiffLightningLogout();
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser2 = login.ValidateUserLightning();
                Assert.AreEqual(stdUser2.Contains(ReadExcelData.ReadData(excelPath, "Users", 1)), true);
                extentReports.CreateLog("User: " + stdUser2 + " logged in ");
                engHome.SelectEngUnderHLBanker();
                engHome.ValidateSearchFunctionalityOfEngagements("FGIC 2024");
                engHome.ClickEngNumber();
                string newOppValPeriodFVAUser = engDetails.ValidateNewOppValPeriodButtonOfRelatedOpp(stdUser2);
                Assert.AreEqual("New Opportunity Valuation Period button is not displayed", newOppValPeriodFVAUser);
                extentReports.CreateLog(stdUser2 + " is not allowed to add New Opportunity Valuation Period ");

                usersLogin.DiffLightningLogout();
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }

        }
    }
}


