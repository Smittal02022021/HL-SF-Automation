using AventStack.ExtentReports.Gherkin.Model;
using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;


namespace SF_Automation.TestCases.Engagement
{
    class TMTT0031255_VerifyTheFieldsAndFunctionalityOfHLFinancingTab : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        EngagementSummaryPage summaryPage = new EngagementSummaryPage();
        public static string fileTMTT0031167 = "TMTT0024518_VerifyTheFunctionalityOfFREngagementSummaryInSFLightning";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTheFieldsAndFunctionalityOfHLFinancingTab()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0031167;
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
                int rowJobType = ReadExcelData.GetRowCount(excelPath, "Engagement");
                Console.WriteLine("rowCount " + rowJobType);
                string JobType = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", 2, 1);
                engHome.ValidateSearchFunctionalityOfEngagementsByJobType(JobType);

                //---TMTI0073013_ Verify the availability of the "HL Financing" tab on the FR Engagement Summary page
                 string value = engagementDetails.ClickFREngSummaryButtonL();
                 string HLFin = engagementDetails.ValidateHLFinancingTabL();
                 Assert.AreEqual("HL Financing", HLFin);
                 extentReports.CreateLog("Tab with name: " + HLFin + " is displayed on FR Engagement Summary ");

                //---TMTI0073015_ Verify the fields available under the "HL Financing" tab
                Assert.IsTrue(summaryPage.VerifyHLFinancingTableFieldsL(), "Verified that displayed HL Financing Table fields are same");
                extentReports.CreateLog("HL Financing Table fields are displayed as expected ");

                string TotalFin =summaryPage.ValidateLabelTotalFinancingAmount();
                Assert.AreEqual("Total Financing Amount", TotalFin);
                extentReports.CreateLog("Field with name: " + TotalFin + " is displayed on HL Financing tab ");

                string FinDesc = summaryPage.ValidateLabelFinancingDesc();
                Assert.AreEqual("Financing Description", FinDesc);
                extentReports.CreateLog("Field with name: " + FinDesc + " is displayed on HL Financing tab ");

                //---TMTI0073017_ Verify that clicking the "Add HL Financing" button opens up the screen to enter the details
                string FinType = summaryPage.VerifyFinancingTypeFieldL();
                Assert.AreEqual("*Financing Type", FinType);
                extentReports.CreateLog("Field with name: " + FinType + " is displayed on Add HL Financing window ");

                string SecType = summaryPage.VerifySecurityTypeFieldL();
                Assert.AreEqual("*Security Type", SecType);
                extentReports.CreateLog("Field with name: " + SecType + " is displayed on Add HL Financing window ");

                string FinAmt = summaryPage.VerifyFinancingAmountFieldL();
                Assert.AreEqual("Financing Amount (MM)", FinAmt);
                extentReports.CreateLog("Field with name: " + FinAmt + " is displayed on Add HL Financing window ");

                string Other = summaryPage.VerifyOtherFieldL();
                Assert.AreEqual("Notes", Other);
                extentReports.CreateLog("Field with name: " + Other + " is displayed on Add HL Financing window ");

                //Validate values of Financing Type
                Assert.IsTrue(summaryPage.VerifyFinancingTypeValuesL(), "Verified that displayed Financing Type values are same");
                extentReports.CreateLog("Financing Type values are displayed as expected ");

                //Validate values of Security Type
                Assert.IsTrue(summaryPage.VerifySecurityTypeValuesL(), "Verified that displayed Security Type values are same");
                extentReports.CreateLog("Security Type values are displayed as expected ");

                //Validate Cancel button
                string cancel = summaryPage.ValidateCancelButton();
                Assert.AreEqual("Cancel", cancel);
                extentReports.CreateLog("Button with name: " + cancel + " is displayed on Add HL Financing window ");

                //Validate Save button
                string save = summaryPage.ValidateSaveButton();
                Assert.AreEqual("Save", save);
                extentReports.CreateLog("Button with name: " + save + " is displayed on Add HL Financing window ");

                //---TMTI0073019_Verify that an error message appears for the required field on clicking the "Save" button of Add HL Financing on leaving fields blank
                //Validate the error message for Financing Type if blank
                string msgFin = summaryPage.ValidateErrorMessageForFinancingType();
                Assert.AreEqual("Complete this field.", msgFin);
                extentReports.CreateLog("Message: " + msgFin + " is displayed for Financing Type field upon clicking Save button without selecting any value ");

                //Validate the error message for Security Type if blank
                string msgSec = summaryPage.ValidateErrorMessageForSecurityType();
                Assert.AreEqual("Complete this field.", msgSec);
                extentReports.CreateLog("Message: " + msgSec + " is displayed for Security Type field upon clicking Save button without selecting any value ");

                //---TMTI0073021_Verify that clicking the "Cancel" button will take the user back to the list view of the HL Financing tab
                string cancelPage =summaryPage.ValidateCancelButtonFunctionality();
                Assert.AreEqual("HL Financing", cancelPage);
                extentReports.CreateLog("Page with tab: " + cancelPage + " is displayed upon clicking Cancel button on Add HL Financing window ");

                //---TMTI0073023_Verify that the HL Financing record is created with all the entered information by clicking the "Save" button on Add HL Financing screen
                string rowHLFin = summaryPage.ValidateSaveFunctionalityOfAddHLFinancing();
                Assert.AreEqual("True", rowHLFin);
                extentReports.CreateLog("A row is created after saving details on Add HL Financing page ");

                //---TMTI0073025_Verify that if the user enters data in the "Other" field of Add HL Financing screen without selecting Financing Type - Other, the application will give an error message.
                string msgOther = summaryPage.ValidateErrorMessageOfOtherField();
                Console.WriteLine(msgOther);
                Assert.AreEqual("Notes for Other Financing Types can only be saved if \"Other\" is selected from the drop-down.", msgOther);
                extentReports.CreateLog("Message : " + msgOther + "is displayed when Other field is entered without selecting Financing Type as Other ");

                //---TMTI0073027_Verify that clicking the "Edit" button of the HL Financing record allows the user to update data in any of the fields
                string finTypeBeforeUpdate = summaryPage.GetFinTypeBeforeUpdate();
                string finTypePostUpdate = summaryPage.ValidateEditFunctionalityOfAddHLFinancing();
                Assert.AreNotEqual(finTypeBeforeUpdate, finTypePostUpdate);
                extentReports.CreateLog("Updated value of Financing Type is displayed upon saving it. ");

                //---TMTI0073029_Verify that if the user enters data in the "Other" field while editing HL Financing without selecting Financing Type - Other, the application will give an error message
                string msgOtherEdit = summaryPage.ValidateErrorMessageOfOtherFieldWhileEdit();
                Assert.AreEqual("Notes for Other Financing Types can only be saved if \"Other\" is selected from the drop-down.", msgOtherEdit);
                extentReports.CreateLog("Message : " + msgOtherEdit + "is displayed when Other field is entered without selecting Financing Type as Other while editing HL Financing ");

                //---TMTI0073031_Verify that if the user enters data in the "Other" field while editing HL Financing with Financing Type - Other, the application will not give any error message
                string valOther = summaryPage.ValidateFunctionalityOfOtherField();
                Assert.AreEqual("Testing", valOther);
                extentReports.CreateLog("Updated value: " +valOther+" of Other field is displayed when Other field is entered after selecting Financing Type as Other while editing HL Financing ");

                //---TMTI0073033_Verify that if the user removes data from required fields while editing HL Financing, the application will give an error message for required fields
                string msgFinType = summaryPage.ValidateMandatoryFieldValidationForFinType();
                Assert.AreEqual("Complete this field.", msgFinType);
                extentReports.CreateLog("Validation: " + msgFinType + " for Financing Type field is displayed when no value is selected ");

                string msgSecType = summaryPage.ValidateMandatoryFieldValidationForSecType();
                Assert.AreEqual("Complete this field.", msgSecType);
                extentReports.CreateLog("Validation: " + msgSecType + " for Security Type field is displayed when no value is selected ");

                //---TMTI0073035_Verify that clicking the "Delete" button of the HL Financing record gives a confirmation message before deleting the record
                string msgCancel = summaryPage.ValidateCancelFunctionalityOfHLFinancing();
                Assert.AreEqual("Record is not deleted", msgCancel);
                extentReports.CreateLog("Record is not deleted after clicking cancel on confirmation page ");

                string msgDelete = summaryPage.ValidateDeleteFunctionalityOfHLFinancing();
                Assert.AreEqual("Record is deleted", msgDelete);
                extentReports.CreateLog("Record is deleted after clicking Ok on confirmation page ");

                //---TMTI0073037_Verify the "Total Financing Amount" field is a formula field and can be overridden
                string updMessage = summaryPage.UpdateTotalFinancingAmountValue();
                Assert.AreEqual("Record saved", updMessage);
                extentReports.CreateLog("Message :" + updMessage+ " is displayed after updating value of Total Financing Amount ");

                //---TMTI0073039_Verify that on clicking the "Save" button, provided information gets saved and a success message appears on the screen
                string updMessageDesc = summaryPage.UpdateFinancingDescValue();
                Assert.AreEqual("Record saved", updMessageDesc);
                extentReports.CreateLog("Message :" + updMessageDesc + " is displayed after updating value of Financing Description ");

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


