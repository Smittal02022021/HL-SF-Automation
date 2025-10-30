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
    class TMTT0046849_VerificationOfEngagementBasicInformationOnEngagementSummaryPage : BaseClass
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
                string message=  engHome.SearchEngagementWithNumberOnLightning(ReadExcelData.ReadData(excelPath, "Engagement", 2), ReadExcelData.ReadData(excelPath, "Engagement", 1));
                Assert.AreEqual("Project Moon", message);
                extentReports.CreateLog("Records matching with selected Job Type are displayed ");
                string engNum = engagementDetails.GetEngagementNumberHeaderL();
                string engNumValue = engagementDetails.GetEngagementNumberValueHeaderL();

                string subOwner = engagementDetails.GetSubOwnershipHeaderL();
                string subOwnerValue = engagementDetails.GetSubOwnershipValueHeaderL();

                string LOB = engagementDetails.ValidateLOBOnHeader();
                string lobValue= engagementDetails.ValidateLOBValueOnHeader();                
                
                string discStatus = engagementDetails.GetExtDisclosureStatus();
                string discStatusValue = engagementDetails.GetDisclosureStatusValue();
                Console.WriteLine(discStatusValue);

                string subject = engagementDetails.ValidateSubjectOnHeader();
                string subjectValue = engagementDetails.ValidateSubjectValueOnHeader();

                string client = engagementDetails.ValidateClientOnHeader();
                string clientValue = engagementDetails.ValidateClientValueOnHeader();
                Console.WriteLine(clientValue);

                string closeDate = engagementDetails.ValidateCloseDateLabel();
                string closeDateValue = engagementDetails.ValidateCloseDateValue();

                string jobType = engagementDetails.ValidateJobTypeOnHeader();
                string jobTypeValue = engagementDetails.ValidateJobtypeValueOnHeader();
                Console.WriteLine(jobTypeValue);

                engagementDetails.ValidateFeesTab();
                string txnSizeValue = engagementDetails.GetValEstTansacttionMarketCapLV();
                string txnSize = engagementDetails.GetEstTansacttionMarketCapLabel();

                //1. TMTI0114529_Verify the "Engagement Number" label and mapping value are displayed
                engagementDetails.ClickCFEngsummaryButtonL();
                string engNumSummary = summaryPage.ValidateEngNumOnHeader();
                string engNumSummaryValue = summaryPage.ValidateEngNumValueOnHeader();
                //string engNumSummaryMessage = summaryPage.ValidateEngNumberMessageOnHeader();

                Assert.AreEqual(engNum, engNumSummary);
                Assert.AreEqual(engNumValue, engNumSummaryValue);
                extentReports.CreateLog("The label " + engNumSummary + " and mapping value " + engNumSummaryValue + " is displayed on CF Engagement Summary as displayed in Engagement details page.");

                //Assert.AreEqual("Case Number", engNumSummaryMessage);
                //extentReports.CreateLog("The Tool tip message " + engNumSummaryMessage + " is displayed on Engagement Number in CF Engagement Summary ");

                //2. TMTI0114530_Verify the "Subject Ownership" label and mapping value are displayed.
                string subOwnerSummary = summaryPage.ValidateSubOwnerOnHeader();
                string subOwnerSummaryValue = summaryPage.ValidateSubOwnerValueOnHeader();
                Assert.AreEqual(subOwner, subOwnerSummary);
                Assert.AreEqual(subOwnerValue, subOwnerSummaryValue);
                extentReports.CreateLog("The label " + subOwnerSummary + " and mapping value " + subOwnerSummaryValue + " is displayed on CF Engagement Summary as displayed in Engagement details page.");

                //3. TMTI0114531_Verify the label Est. Transaction Size / Market Cap (MM) mapping values are displayed and the tooltip.
                string txnSizeSummary = summaryPage.ValidateEstTxnSizeOnHeader();
                string txnSizeSummaryValue = summaryPage.ValidateEstTxnSizeValueOnHeader();
                //string txnSizeSummaryMessage = summaryPage.ValidateEstTxnSizeMessageOnHeader();

                Assert.AreEqual(txnSize, txnSizeSummary);
                Assert.AreEqual(txnSizeValue, txnSizeSummaryValue);
                extentReports.CreateLog("The label " + txnSizeSummary + " and mapping value " + txnSizeSummaryValue + " is displayed on CF Engagement Summary as displayed in Engagement details page. ");

                //Assert.AreEqual("If Engagement involves a majority stake in a Public Company, insert Market Cap as of Engagement Letter date.", txnSizeSummaryMessage);
                //extentReports.CreateLog("The Tool tip message " + txnSizeSummaryMessage + " is displayed on Est. Transaction Size / Market Cap (MM) in CF Engagement Summary ");

                //4. TMTI0114532_Verify the link Summary Report is displayed and leads to Cogno's ES Report.
                string summaryReport =summaryPage.ValidateSummaryReportButtonOnHeader();
                Assert.AreEqual("Summary Report", summaryReport);
                extentReports.CreateLog("Button with name " + summaryReport + " is displayed on CF Engagement Summary page. ");

                string report =summaryPage.ConnectCognoAndOpenPDF();
                string engSummaryReport = summaryPage.VerifyEngSummaryinReport();
                Assert.AreEqual("Engagement Summary Report ", engSummaryReport);
                extentReports.CreateLog("Report with name: " + engSummaryReport +  " is displayed after clicking Summary Report button on CF Engagement Summary . ");

                //5. TMTI0114533_Verify the "Closed Date" label and mapping value are displayed.
                string closeDateSummary = summaryPage.ValidateCloseDateOnHeader();
                string closeDateSummaryValue = summaryPage.ValidateCloseDateValueOnHeader();
                
                Assert.AreEqual(closeDate, closeDateSummary);
                Assert.AreEqual(closeDateValue, closeDateSummaryValue);
                extentReports.CreateLog("The label " + closeDateSummary + " and mapping value " + closeDateSummaryValue + " is displayed on CF Engagement Summary as displayed in Engagement details page. ");

                //6. TMTI0114534_Verify the "Job Type" label and mapping value are displayed.
                string jobTypeSummary = summaryPage.ValidateJobTypeOnHeader();
                string jobTypeSummaryValue = summaryPage.ValidateJobTypeValueOnHeader();

                Assert.AreEqual(jobType, jobTypeSummary);
                Assert.AreEqual(jobTypeValue, jobTypeSummaryValue);
                extentReports.CreateLog("The label " + jobTypeSummary + " and mapping value " + jobTypeSummaryValue + " is displayed on CF Engagement Summary as displayed in Engagement details page. ");


                //7. TMTI0114535_Verify that the label External Disclosure Status and mapping value are displayed.
                string discSummary = summaryPage.ValidateDiscStatusOnHeader();
                string discSummaryValue = summaryPage.ValidateDiscStatusValueOnHeader();

                Assert.AreEqual(discStatus, discSummary);
                Assert.AreEqual(discStatusValue, discSummaryValue);
                extentReports.CreateLog("The label " + discSummary + " and mapping value " + discSummaryValue + " is displayed on CF Engagement Summary as displayed in Engagement details page. ");

                //8. TMTI0114536_Verify the "Line Of Business" label and mapping value is displayed.
                string lobSummary = summaryPage.ValidateLOBOnHeader();
                string lobSummaryValue = summaryPage.ValidateLOBValueOnHeader();

                Assert.AreEqual(LOB, lobSummary);
                Assert.AreEqual(lobValue, lobSummaryValue);
                extentReports.CreateLog("The label " + lobSummary + " and mapping value " + lobSummaryValue + " is displayed on CF Engagement Summary as displayed in Engagement details page.");
                
                       
                //9. TMTI0114537_Verify that the "Subject" label and mapping value are displayed.
                string subSummary = summaryPage.ValidateSubjectOnHeader();
                string subSummaryValue = summaryPage.ValidateSubjectValueOnHeader();

                Assert.AreEqual(subject, subSummary);
                Assert.AreEqual(subjectValue, subSummaryValue);
                extentReports.CreateLog("The label " + subSummary + " and mapping value " + subSummaryValue + " is displayed on CF Engagement Summary as displayed in Engagement details page. ");

                //10. TMTI0114538_Verify that the "Client" label and mapping value are displayed.
                string clientSummary = summaryPage.ValidateClientOnHeader();
                string clientSummaryValue = summaryPage.ValidateClientValueOnHeader();

                Assert.AreEqual(client, clientSummary);
                Assert.AreEqual(clientValue, clientSummaryValue);
                extentReports.CreateLog("The label " + clientSummary + " and mapping value " + clientSummaryValue + " is displayed on CF Engagement Summary as displayed in Engagement details page. ");

                usersLogin.LightningLogout();
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


