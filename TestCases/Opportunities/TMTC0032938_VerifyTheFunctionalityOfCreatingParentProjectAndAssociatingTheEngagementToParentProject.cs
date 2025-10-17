using NUnit.Framework;
using SalesForce_Project.Pages;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.Opportunities
{
    class TMTC0032938_VerifyTheFunctionalityOfCreatingParentProjectAndAssociatingTheEngagementToParentProject : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        EngagementHomePage engHome = new EngagementHomePage();
        ParentProject project = new ParentProject();
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOppCounterparty counterparty = new AddOppCounterparty();       
        AddOpportunityContact addContact = new AddOpportunityContact();
        EngagementDetailsPage engDetails = new EngagementDetailsPage();

        public static string TMTT0017889 = "TMTC0032938_VerifyTheFunctionalityOfCreatingParentProjectAndAssociatingTheEngagementToParentProject.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void FunctionalityOfParentProject()
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

                //Verify the availability of Opportunity under HL Banker list
                string tagOpp = opportunityHome.ValidateParentProjectUnderHLBanker();
                Assert.AreEqual("Project Billings", tagOpp);
                extentReports.CreateLog(tagOpp + " is displayed under Home dropdown ");

                string titleProject = project.ClickNewButton();
                Assert.AreEqual("New Project Billing", titleProject);
                extentReports.CreateLog(titleProject + " page is displayed after clicking New button ");

                Assert.IsTrue(project.VerifyParentProjectMandatoryValdiations(), "Verified that displayed mandatory validations are same ");
                extentReports.CreateLog("Displayed mandatory validations are correct ");

                //4.	TMT0073610_Verify the functionality of creating New Parent Project. 
                string value = CustomFunctions.RandomValue();
                string addedProject = project.CreateNewParentProject(value);
                Assert.AreEqual(value, addedProject);
                extentReports.CreateLog("Parent Project with name: " + addedProject + " is displayed after saving all the mandatory fields ");
               
                //5.	TMT0073612_Verify the functionality of associating first engagement to the Parent Project.
                engHome.SelectDirectEngUnderHLBanker();
                engHome.SearchEngagementWithNumberOnLightning("Project Palm Tree - Tax", "TAS - Due Diligence-Buyside");
                string addedProjectEng = project.AssociateParentProjectToEng(value);
                Assert.AreEqual(value, addedProjectEng);
                string engLOB =engDetails.GetLOBL();
                Console.WriteLine("LOB:" + engLOB);
                string engClient = engDetails.GetEngClientCompanyL();
                Console.WriteLine("LOB:" + engClient);
                string engLegalEntity = engDetails.GetLegalEntityL();
                Console.WriteLine("LOB:" + engLegalEntity);
                string engCurrency = engDetails.GetCurrencyL();
                Console.WriteLine("LOB:" + engCurrency);
                string engFee = engDetails.GetTotalEstimatedFeeL();
                Console.WriteLine("Fee:" + engFee);
                extentReports.CreateLog("Engagement gets associated with Parent Project: " + addedProjectEng + " ");

                //6.	TMT0073614_Verify that on associating engagement to parent project, engagement will populate on the parent project
                opportunityHome.ValidateParentProjectUnderHLBanker();
                string associatedEng = project.ValidateAssociatedEngToParentProject();
                Assert.AreEqual("Project Palm Tree - Tax", associatedEng);
                extentReports.CreateLog("Associated Engagement : " + associatedEng + " is populated on the Parent Project ");

                //7.	TMT0073616_Verify that based on associated engagement, parent project will populate LOB, Legal Entity, Client, and Currency type. 
                string LOB = project.GetLOBL();
                string Client = project.GetClientCompanyL();
                string Currency = project.GetCurrencyL();
                string LegalEntity = project.GetLegalEntityL();
                Assert.AreEqual(engLOB,LOB );
                Assert.AreEqual(engClient, Client);
                Assert.AreEqual(engLegalEntity, LegalEntity);
                Assert.AreEqual(engCurrency, Currency);
                extentReports.CreateLog("Parent project is populated with similar LOB, Legal Entity, Client, and Currency as of Engagement after associated with Engagement ");

                //8.	TMT0073618_Verify that based on associating engagements, Parent Contract will get created in the parent project. 
                string projectContract = project.GetParentContract();
                Assert.AreEqual("Project Palm Tree - Tax", projectContract);
                extentReports.CreateLog("Parent Contract : " + projectContract + " is created in  Parent Project after associating engagement ");

                //9.	TMT0073620_Verify that the "Parent Contract" created in Parent Project on associating engagement, it will also get associated to engagement and its related opportunity
                string contractNumber = project.GetParentContractNumber();
                Console.WriteLine("contractNumber: " + contractNumber);
                engHome.SearchEngagementWithNumberOnLightning("Project Palm Tree - Tax", "TAS - Due Diligence-Buyside");
                string engContractNumber = engDetails.GetContractNumberL();
                string oppContractNumber =opportunityDetails.GetContractNumberL();
                Assert.AreEqual(contractNumber, engContractNumber);
                Assert.AreEqual(contractNumber, oppContractNumber);
                extentReports.CreateLog("Parent Contract created in Parent Project with number: " + contractNumber+ " is associated to  its associated engagement and its related opportunity. ");

                //10.   TMT0073622_Verify that the Engagement Contract is selected as Main Contract and not the Parent Project Contract. 
                string isMain = engDetails.ValidateIsMainOfAddedContractL();
                Console.WriteLine("isMain : " + isMain);
                Assert.AreEqual("", isMain);
                extentReports.CreateLog("Parent Contract in associated engagement is not selected as the Main Contract. ");

                //11. TMT0073624_Verify that on adding additional engagements, those engagements will get associated to the parent project
                engHome.SelectDirectEngUnderHLBanker();
                engHome.SearchEngagementWithNumberOnLightning("Project Guide", "TAS - Due Diligence-Buyside");
                string addedProjectEng2nd = project.AssociateParentProjectToEng(value);

                string engFee2nd = engDetails.GetTotalEstimatedFeeOf2ndEngL();
                Console.WriteLine("Fee:" + engFee2nd);

                string associatedEng2nd = project.ValidateAssociated2ndEngToParentProject();
                Assert.AreEqual("Project Guide", associatedEng2nd);
                extentReports.CreateLog("2nd Associated Engagement : " + associatedEng2nd + " is populated as well on the Parent Project ");

                //12. TMT0073626_Verify that the "Total Fee" and "Funding Fee" fields of the parent contract, will be aggregate of Total Estimated Fee of all the associated engagements on the Parent project
                string totalFee= project.GetContractTotalFee();
                string fundingAmount= project.GetContractFundingAmount();
                Console.WriteLine("totalFee:" + totalFee);
                Console.WriteLine("fundingAmount:" + fundingAmount);
                string totalFeeOfEngs =  (Convert.ToDouble(engFee)  + Convert.ToDouble(engFee2nd)).ToString("0.00");
                Console.WriteLine("totalFeeOfEngs:" + totalFeeOfEngs);
                Assert.AreEqual(totalFee.Replace(",",""),"USD "+ totalFeeOfEngs);
                Assert.AreEqual(fundingAmount.Replace(",", ""), totalFeeOfEngs);
                extentReports.CreateLog("Total Fee: " + totalFee + " and Funding Fee: " + fundingAmount + " of the parent Contract is aggregate of Total Estimated Fee of both the associated engagements on the Parent project " );

                //13.  TMT0073628_Verify that the "Related Tab" allows you to view all the details for each associated engagement
                //opportunityHome.ValidateParentProjectUnderHLBanker();
                //project.ValidateSearchFunctionalityOfParentProject(addedProject);
                Assert.IsTrue(project.VerifyRelatedTabSections(), "Verified that displayed sub sections of Related tab are same");
                extentReports.CreateLog("Displayed sub sections of Related tab are correct ");

                string billing =project.ValidateBillingRequestSection();
                Assert.AreEqual("Billing Requests", billing);
                extentReports.CreateLog("Section with name : " + billing + " is displayed on Related tab ");

                //14.  TMT0073630_Verify that user is able to "Edit" the Parent Project
                string updatedProj = project.ValidateEditFunctionalityOfParentProject();
                Assert.AreEqual("Updated Project", updatedProj);
                extentReports.CreateLog("Parent Project's name : " + updatedProj + " is updated after editing it ");

                //15.  TMT0073632_Verify that the "Delete" button is only not available for assistants/accountants/biller
                string deleteProj = project.ValidateDeleteParenttProjectButton();
                Assert.AreEqual("Delete button is displayed", deleteProj);
                extentReports.CreateLog(deleteProj + " for CF Financial User ");

                usersLogin.DiffLightningLogout();

                //16. TMT0073634_Verify that the System Admin is able to delete the Parent Project 
                string deleteProjAdmin = project.ValidateDeleteParenttProjectButtonForAdmin();
                Assert.AreEqual("Delete button is displayed", deleteProjAdmin);
                extentReports.CreateLog(deleteProjAdmin + " for Admin ");

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

    

