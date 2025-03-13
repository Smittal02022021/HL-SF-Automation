using AventStack.ExtentReports.Gherkin.Model;
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
    class TMTC0032942_VerifyTheFunctionalityOfCreatingBillingEventsAndSubmitForApprovalProcess:BaseClass
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

        public static string TMTC0032942 = "TMTC0032942_VerifyTheFunctionalityOfCreatingBillingEventsAndSubmitForApprovalProcess.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void FunctionalityOfBillingEvent()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + TMTC0032942;
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
                Assert.AreEqual("Parent Projects", tagOpp);
                extentReports.CreateLog(tagOpp + " is displayed under Home dropdown ");

                //1.  TMT0075162_Verify that the "Billing Request" quick link is placed on the Parent Project               
                project.ValidateSearchFunctionalityOfParentProject("Combo O’Connor Global");
                project.ValidateBillingRequestLink();
                string billingEvent = project.ValidateAccessToAddBillingEventFunctionality();
                Assert.AreEqual("New ERP Revenue Billing Event", billingEvent);
                extentReports.CreateLog("Page with title " + billingEvent + " is displayed after clicking New Billing Event ");

                //3.  TMT0075167_Verify that on clicking Save button of the Billing Event without filling details, validation appears for the required fields
                Assert.IsTrue(project.ValidateBillingEventValidations(), "Verified that displayed validations of Billing Event are same ");
                extentReports.CreateLog("Displayed mandatory validations of Billing Event are as expected ");

                //4.  TMT0075169_Verify the functionality of creating the Billing Event
                string eventName = project.SaveBillingEventDetails();
                Assert.NotNull(eventName);
                extentReports.CreateLog("Billing Event with number: " + eventName + " is displayed on ERP Revenue Billing Event page. ");

                string eventAmount = project.GetTotalEventAmount();

                //5.   TMT0075171 Verify that the created billing event is displayed under the Accounting Tab of Billing Request
                string eventNameAcc = project.ValidateCreatedBillingEventInAccountingTab();
                Assert.AreEqual(eventName,eventNameAcc);
                extentReports.CreateLog("The created billing event is displayed under the Accounting Tab of the Billing Request.");

                //6.   TMT0075173_Verify that the biller is not able to create billing events on Parent Contract created on the Parent Project
                string messageContract = project.ValidateBillingEventOnParentContractFunctionality();
                Assert.AreEqual("Billing events cannot be created on parent contract.", messageContract);
                extentReports.CreateLog("Message: " + messageContract + " is displayed while trying to create billing events on Parent Contract of Parent Project ");

                //7.	TMT0075175_Verify that the biller is not able to create billing events on the ICO Contract
                string messageICO = project.ValidateBillingEventOnICOContractFunctionality();
                Assert.AreEqual("Contract\r\nBilling Event cannot be created on ICO contracts", messageICO);
                extentReports.CreateLog("Message: " + messageICO + " is displayed while trying to create billing events on ICO Contract ");

                //8.	TMT0075177_Verify that if Total Event Amount is not Equal to Total Fees to Bill, validation appears on screen on Updating Status of the Billing Request. 
               string messageTotalFee= project.ValidateTotalFeesToBillValidation();
                Assert.AreEqual("Validation total fee to bill should equal to total event amount", messageTotalFee);
                extentReports.CreateLog("Message: " + messageTotalFee + " is displayed while trying to create billing events on ICO Contract ");

                //9.	TMT0075179_Verify that the Biller is able to Update Status from Draft Billing Request to Sent to ERP(Sync to Oracle). 
                string statusBillingRq =project.UpdateEventAmountAndValidateStatusOfBillingReq();
                Assert.AreEqual("Sent to ERP", statusBillingRq);
                extentReports.CreateLog("Status of the billing request: " + statusBillingRq + " is displayed after upating Status from Draft Billing Request to Sent to ERP ");


                //2.  TMT0075164_Verify that the Biller is able to update the Billing Request, and updated details are reflecting on the Details tab of the billing request

                usersLogin.DiscardChanges();
                usersLogin.DiffLightningLogout();
                usersLogin.DiffLightningLogout();

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

    

