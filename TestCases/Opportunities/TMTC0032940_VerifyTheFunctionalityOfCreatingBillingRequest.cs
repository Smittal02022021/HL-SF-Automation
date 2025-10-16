using NUnit.Framework;
using SF_Automation.UtilityFunctions;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using System;
using SalesForce_Project.Pages;
using NUnit.Framework.Interfaces;

namespace SF_Automation.TestCases.Opportunities
{
    class TMTC0032940_VerifyTheFunctionalityOfCreatingBillingRequest: BaseClass
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
        public void FunctionalityOfBillingRequest()
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

                //1.  TMT0074711_Verify that the "Billing Request" quick link is placed on the Parent Project
                //project.ValidateSearchFunctionalityOfParentProject("Updated Project");
                project.ValidateSearchFunctionalityOfParentProject("Combo O’Connor Global");

                string valExpEng1 = project.GetExpenseCapOfEngagement1();
                Console.WriteLine("valExpEng1 " + valExpEng1);
                string valExpEng2 = project.GetExpenseCapOfEngagement2();
                Console.WriteLine("valExpEng2 " + valExpEng2);

                string billing = project.ValidateBillingRequestLink();
                Assert.AreEqual("Billing Requests", billing);
                extentReports.CreateLog("Link "+ billing + " is displayed on the Parent Project ");

                //2.  TMT0074713_Verify that the "Billing Request" section is placed on the related tab of the Parent Project.  
                // Covered in previous test case

                //3.  TMT0074715_Verify that on clicking Save button of the Billing Request, validation appears for the required fields
                Assert.IsTrue(project.ValidateBillingRequestValidations(), "Verified that displayed mandatory validations are same ");
                extentReports.CreateLog("Displayed mandatory validations of Billing Request are correct ");

                //32. TMT0075999_Verify that the "Accounting Send Final Invoice" will be checked by default. 
                string accInvoice = project.ValidateDefaultCheckOfAccountingInvoice();
                Assert.AreEqual("Accounting Send Final Invoice is checked", accInvoice);
                extentReports.CreateLog("The Accounting Send Final Invoice is checked by default. ");

                //4.  TMT0074717_Verify that if "Accounting Send Final Invoice" is unchecked, Principal/Manager field enabled and becomes required field to enter the name of the deal team
                string messagePrincipal = project.SaveAllMandatoryFieldsOfBillingRequest();
                Assert.AreEqual("Please select either \"Accounting Send Final Invoice\" or \"Principal/Manager\".", messagePrincipal);
                extentReports.CreateLog("Mandatory validation :"+ messagePrincipal + " is displayed when 'Accounting Send Final Invoice' is unchecked and Save button is clicked ");

                //5.  TMT0074720_Verify the functionality of creating the Billing Request. 
                string billingReq = project.SelectFinalInvoice();
                Assert.NotNull(billingReq);
                extentReports.CreateLog("Billing Request with number: " +billingReq + " is displayed on Billing Request detail page. ");

                //6.   TMT0074722_Verify the details and sections displayed on the Billing Request Detail Page.
                Assert.IsTrue(project.ValidateBillingRequestHeaders(), "Verified that displayed headers of Billing Request are same ");
                extentReports.CreateLog("Displayed headers of Billing Request are as expected ");

                Assert.IsTrue(project.ValidateBillingRequestHeaderLinks(), "Verified that displayed headers hyperlinks of Billing Request are same ");
                extentReports.CreateLog("Displayed headers hyperlinks of Billing Request are as expected ");

                //7.   TMT0074724_Verify that user is able to Edit the Billing Request details
                string comments = project.ValidateEditFunctionalityOfBillingRequest();
                Assert.AreEqual("Testing", comments);
                extentReports.CreateLog("Updated details of Billing Request are saved successfully ");

                //8.  TMT0074726_Verify that status of Billing Request
                string status = project.GetStatusOfBillingRequest();
                Assert.AreEqual("Draft Billing Request", status);
                extentReports.CreateLog("Status: " +status+" of the Billing Request is displayed ");

                //9.  TMT0074728_Verify that the user is able to create another Billing Request(multiple billing requests) even if existing billing request is not approved yet.
                string billingReq2nd = project.Save2ndBillingRequest();
                Assert.NotNull(billingReq2nd);
                extentReports.CreateLog("Billing Request with number: " + billingReq2nd + " is created even when the existing billing request is not approved yet ");

                //10. TMT0074730_Verify that the "New" button is available on the Fees to Bill Section to add Fees to Bill
                string newFile = project.ValidateNewButtonInFeesToBillSection();
                Assert.AreEqual("New",newFile);
                extentReports.CreateLog("Button with name: " + newFile + " is displayed in Fees To Bill section ");

                //11.	TMT0074755_Verify that validation appears for required fields on New Fees to Bill screen
                Assert.IsTrue(project.ValidateNewFeesToBillValidations(), "Verified that displayed mandatory validations are same ");
                extentReports.CreateLog("Displayed mandatory validations of New Fees To Bill are correct ");

                //12.	TMT0074757_Verify that user is able to add Fees to Bill with the required information
                string saveProgFee = project.SaveFeeToBill("100328", "Progress Fee","100000");
                string progAmount = project.GetAddedFeevalue("Progress Fee");
                Console.WriteLine(progAmount.Substring(4, 7).Replace(",", ""));
                Assert.AreEqual("Progress Fee", saveProgFee);
                Assert.AreEqual("USD 100,000.00", progAmount);
                extentReports.CreateLog("Fee with Type: " + saveProgFee + " and amount: " + progAmount + " is displayed on Fee To Bill page ");

                //13.	TMT0074759_Verify that once user Add Fees to Bill, Admin Fee will be created automatically. (Based on the percentage fixed in backend)
                string feeBillingReq =project.ValidateAddedFeeInBillingRequest();
                string adminprogressAmount = project.GetGeneratedAdminFeevalue("Admin Fee");
                double actualProgressFee = Convert.ToDouble(progAmount.Substring(4,7));                            
                Assert.AreEqual("Admin Fee", feeBillingReq);
                double finalProgressFee= ((actualProgressFee * 5.5) / 100);
                Console.WriteLine("finalProgressFee: " + finalProgressFee.ToString());
                
                Assert.AreEqual(finalProgressFee.ToString(), adminprogressAmount.Substring(4,5).Replace(",", ""));
                extentReports.CreateLog("Fee with Type: " + feeBillingReq    + " and amount : "+ adminprogressAmount+" is displayed on Billing Request page after adding Progress Fee on Fee To Bill page ");

                //15.	TMT0074763_Verify that user is not allowed to delete the added Fees to Bill
                string feeDelete = project.ValidateDeleteFunctionalityOfAddedFeeInBillingRequest();
                Assert.AreEqual("Edit", feeDelete);
                extentReports.CreateLog("Delete option to delete fee is not available to the user: " +stdUser + " " );

                //34 and 35. Verify the calculation of the Admin Fee if the aggregate of the Historical Fee is "NULL." It will be calculated based on the associated engagements' current fees. 
                //--- After adding Retainer Fee
                project.clickNewFeeesButton();
                string saveRetainFee = project.SaveFeeToBill("100328", "Retainer Fee","300000");
                string retainAmount = project.GetAddedFeevalue("Retainer Fee");
                Assert.AreEqual("Retainer Fee", saveRetainFee);
                Assert.AreEqual("USD 300,000.00", retainAmount);
                extentReports.CreateLog("Fee with Type: " + saveRetainFee + " and amount: " + retainAmount + " is displayed on Fee To Bill page ");

                string feeBillingReq2 = project.ValidateAddedFeeInBillingRequest();
                string adminRetainAmount = project.GetGeneratedAdminFeevalue("Admin Fee");
                double actualRetainFee = Convert.ToDouble(retainAmount.Substring(4, 7));                
                Console.WriteLine(((actualRetainFee * 4.5) / 100).ToString());
                Assert.AreEqual("Admin Fee", feeBillingReq2);
                double finalRetainerFee = ((actualProgressFee + actualRetainFee )* 4.5) / 100;
                Console.WriteLine("finalRetainerFee: " + finalRetainerFee.ToString());

                Assert.AreEqual(finalRetainerFee.ToString(), (adminRetainAmount.Substring(4, 6)).Replace(",",""));
                extentReports.CreateLog("Fee with Type: " + feeBillingReq2 + " and amount : " + adminRetainAmount + " is displayed on Billing Request page as well after adding Retainer Fee on Fee To Bill page ");

                //--- After adding Contingent Fee
                project.clickNewFeeesButton();
                string saveConFee = project.SaveFeeToBill("100328", "Contingent Fee", "300000");
                string conAmount = project.GetAddedFeevalue("Contingent Fee");
                Assert.AreEqual("Contingent Fee", saveConFee);
                Assert.AreEqual("USD 300,000.00", conAmount);
                extentReports.CreateLog("Fee with Type: " + saveConFee + " and amount: " + conAmount + " is displayed on Fee To Bill page ");

                string feeBillingReq3 = project.ValidateAddedFeeInBillingRequest();
                string adminConAmount = project.GetGeneratedAdminFeevalue("Admin Fee");
                double actualConAmountFee = Convert.ToDouble(conAmount.Substring(4, 7));
                Assert.AreEqual("Admin Fee", feeBillingReq3);
                Assert.AreEqual((((actualProgressFee+actualRetainFee+actualConAmountFee) * 3.5) / 100).ToString(), (adminConAmount.Substring(4, 6)).Replace(",", ""));
                extentReports.CreateLog("Fee with Type: " + feeBillingReq3 + " and amount : " + adminConAmount + " is displayed on Billing Request page as well after adding Contingent Fee on Fee To Bill page ");

                //---Adding monthly fee to 2nd Engagement 
                project.clickNewFeeesButton();
                string saveMonthlyFee = project.SaveFeeToBill("132357", "Monthly Fee", "100000");
                string monthlyAmount = project.GetAddedFeevalue("Monthly Fee");
                Console.WriteLine(monthlyAmount.Substring(4, 7).Replace(",", ""));
                Assert.AreEqual("Monthly Fee", saveMonthlyFee);
                Assert.AreEqual("USD 100,000.00", monthlyAmount);
                extentReports.CreateLog("Fee with Type: " + saveMonthlyFee + " and amount: " + monthlyAmount + " is displayed on Fee To Bill page ");

                string feeBillingReq4 = project.ValidateAddedFeeInBillingRequest2ndEng();
                string adminMonthlyAmount = project.GetGenerated2ndAdminFeevalue("Admin Fee");
                double actualMonthlyFee = Convert.ToDouble(monthlyAmount.Substring(4, 7));
                Assert.AreEqual("Admin Fee", feeBillingReq4);
                double finalMonthlyFee = ((actualMonthlyFee * 2.5) / 100); //2.5  is applicable as all total fees from all engagements is considered to choose % slab, although fee calculation will be only on 2nd enagagement's fee
                Console.WriteLine("finalMonthlyFee: " + finalMonthlyFee.ToString());

                Assert.AreEqual(finalMonthlyFee.ToString(), adminMonthlyAmount.Substring(4, 5).Replace(",", ""));
                extentReports.CreateLog("Separate row for " + feeBillingReq4 + " and amount : " + adminMonthlyAmount + " is displayed on Billing Request page after adding Monthly Fee to 2nd associated engagement on Fee To Bill section ");

                //---Adding Service Fee to 2nd Engagement 
                project.clickNewFeeesButton();
                string saveServiceFee = project.SaveFeeToBill("132357", "Service Fee", "300000");
                string serviceAmount = project.GetAddedFeevalue("Service Fee");
                Console.WriteLine(serviceAmount.Substring(4, 7).Replace(",", ""));
                Assert.AreEqual("Service Fee", saveServiceFee);
                Assert.AreEqual("USD 300,000.00", serviceAmount);
                extentReports.CreateLog("Fee with Type: " + saveServiceFee + " and amount: " + serviceAmount + " is displayed on Fee To Bill page after adding to 2nd associated engagement ");

                string feeBillingReq5 = project.ValidateAddedFeeInBillingRequest2ndEng();
                string adminServiceAmount = project.GetGenerated2ndAdminFeevalue("Admin Fee");
                double actualServiceFee = Convert.ToDouble(serviceAmount.Substring(4, 7));
                Assert.AreEqual("Admin Fee", feeBillingReq5);
                double finalServiceFee = (((actualMonthlyFee+ actualServiceFee) * 1) / 100);
                Console.WriteLine("finalServiceFee: " + finalServiceFee.ToString());

                Assert.AreEqual(finalServiceFee.ToString(), adminServiceAmount.Substring(4, 5).Replace(",", ""));
                extentReports.CreateLog("Fee with Type: " + feeBillingReq5 + " and amount : " + adminServiceAmount + " is displayed on Billing Request page as well after adding Service Fee on Fee To Bill page ");

                //14.	TMT0074761_ Verify that user is able to Edit the added fees to bill and update the calculated Admin Fee into Fee Amount field manually
                string feeAmtBillingReq = project.ValidateEditFunctionalityOfAddedFeeInBillingRequest();
                Assert.AreEqual("USD 20.00", feeAmtBillingReq);
                extentReports.CreateLog("Fee with Amount: " + feeAmtBillingReq + " is displayed on Billing Request page after updating Fee on Fee To Bill page ");

                //16.	TMT0074765_Verify that the user is not able to delete the billing request
                string billingReqDelete = project.ValidateDeleteFunctionalityOfBillingRequest();
                Assert.AreEqual("Edit", billingReqDelete);
                extentReports.CreateLog("Delete option to delete Billing Request is not available to the user: " + stdUser + " ");

                ////17.	TMT0074975_Verify that the expenses of each associated engagement will reflect under Expenses to Bill section.
                //string expenseType = project.GetExpenseTypeOfBillingRequest();
                //Assert.AreEqual("67150-Overtime Meals", expenseType);
                //extentReports.CreateLog("Expense Type of: " + expenseType +" is displayed in the Billing Request ");

                ////18.	TMT0074977_Verify that the user is able to exclude selected expenses using "Update to Bill" from the current billing request
                //string totalExp =  project.GetTotalExpenseOfBillingRequest();
                //string expenseAfterExclude = project.ValidateExcludeExpenseFunctionalityOfBillingRequest();
                //Assert.AreNotEqual(totalExp, expenseAfterExclude);
                //extentReports.CreateLog("Total Expense amount is updated after excluding expense from the Billing Request ");

                ////19.	TMT0074979_Verify that the user is able to include that excluded expenses using "Update to Bill" in the currency billing request
                //string expenseAfterInclude = project.ValidateIncludeExpenseFunctionalityOfBillingRequest();
                //Assert.AreNotEqual(expenseAfterExclude, expenseAfterInclude);
                //extentReports.CreateLog("Total Expense amount is updated after including expense in the Billing Request ");

                ////21.	TMT0074984_Verify that the user is not allowed to delete the added expenses to bill
                //string expenseDelete = project.ValidateDeleteFunctionalityOfExpenseToBill();
                //Assert.AreEqual("Edit", expenseDelete);
                //extentReports.CreateLog("Delete option to delete Expenses is not available to the user: " + stdUser + " ");

                usersLogin.DiffLightningLogout();

                //string expenseDeleteAdmin = project.ValidateDeleteFunctionalityOfExpenseToBillWithAdmin();
                //Assert.AreEqual("Expense deleted successfully", expenseDeleteAdmin);
                //extentReports.CreateLog("Expense is deleted successfully by Admin ");

                //20.	TMT0074981_Verify that the user can add expenses to billing request using "Add Expenses to Bill
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser1 = login.ValidateUserLightning();
                Assert.AreEqual(stdUser1.Contains(valUser), true);
                extentReports.CreateLog("User: " + stdUser1 + " logged in ");

                project.ValidateSearchFunctionalityOfParentProject("Combo O’Connor Global");
                project.ValidateBillingRequestLink();
                project.ValidateAddExpenseToBillFunctionality();
                //string messageAddExp = project.ValidateAddExpenseToBillFunctionality();
                //Assert.AreEqual("records created successfully", messageAddExp);
                //extentReports.CreateLog("Message: " + messageAddExp + " is displayed after adding Expense To Bill ");

                //22.   TMT0074988_Verify that the "Add PV Positions" button is available on the Billing Request
                string addPVPosition = project.ValidateAddPVPositions();
                Assert.AreEqual("Add PV Positions", addPVPosition);
                extentReports.CreateLog("Button: " + addPVPosition + " is displayed on the Billing Request ");

                //25.	TMT0074994 _Verify that the user is able to add PV Positions to Bill to include into the Billing Request that are completed and yet to Invoiced
                string reportFeePV1 = project.ValidateAddPVPositionsFunctionality();
                Assert.NotNull(reportFeePV1);
                extentReports.CreateLog("1st PV Position with Report fee: " + reportFeePV1 + " is displayed on the Billing Request ");

                string reportFeePV2 = project.GetReportFeeOf2ndPVPosition();
                Assert.NotNull(reportFeePV2);
                extentReports.CreateLog("2nd PV Position with Report fee: " + reportFeePV2 + " is displayed on the Billing Request ");

                //23.	TMT0074990_Verify that the user is able to exclude selected PV Positions using "Update to Bill" from the current billing request. 

                //24.	TMT0074992_Verify that the user is able to include that excluded PV Positions using "Update to Bill" in the current billing request. 
                string includePV = project.ValidateIncludePVPositionFunctionalityOfBillingRequest();
                Assert.AreEqual("Records updated Successfully", includePV);
                extentReports.CreateLog("Message: " + includePV + " is displayed after including PV Position using Update To Bill ");

                //26.	TMT0074996_Verify that the user is able to add aggregate of the report fee of all the selected positions in Fees to Bill and it will reflect in the Total Fee to Bill. 
                double fee1 = Convert.ToDouble(reportFeePV1.Substring(4, 6));
                Console.WriteLine("fee1: " + fee1.ToString("0.00"));
                double fee2 = Convert.ToDouble(reportFeePV2.Substring(4, 6));
                Console.WriteLine("fee2: " + fee2.ToString("0.00"));

                string totalReportFee = (Convert.ToDouble(fee1.ToString("0.00")) + Convert.ToDouble(fee2.ToString("0.00"))).ToString();
                Console.WriteLine("totalReportFee: " + totalReportFee);

                string totalFeeToBill=project.ValidateAggregateOfReportFeeFunctionality(totalReportFee);
                Assert.AreEqual(totalFeeToBill, "USD " + totalReportFee+".00");
                extentReports.CreateLog("Total Fee To Bill: " + totalFeeToBill + " is displayed in Billing Request after adding all the selected positions ");

                //31. TMT0075994_Verify that the "Expense Cap" displays on the billing requets will be the sum of the Expense Cap(Fee& Financial section) of all the engagements associated to this billing request.
                double exp1 = Convert.ToDouble(valExpEng1.Substring(4, 4));
                Console.WriteLine("exp1:" + exp1);
                double exp2 = Convert.ToDouble(valExpEng2.Substring(4, 4));
                Console.WriteLine("exp2:" + exp2);
                string totalExpFee = (Convert.ToDouble(exp1.ToString("0.00")) + Convert.ToDouble(exp2.ToString("0.00"))).ToString();
                Console.WriteLine("totalExpFee: " + totalExpFee);
                string expCap = project.ValidateExpenseCapFee();
                Console.WriteLine("expCap:" + expCap);
                Assert.AreEqual(totalExpFee, expCap);
                extentReports.CreateLog("Expense Cap: " + expCap + " is displayed from the sum of the Expense Cap (Fee & Financial section) of all the engagements associated to this billing request. ");

                //27.  TMT0074999_Verify that the Assistant or Deal Team member will not be able to access or add Billing Event on Accounting Tab. 
                string accessBillingEvent = project.ValidateBillingEventAccessInAccountingTab();
                Assert.AreEqual("ERP Revenue Billing Events", accessBillingEvent);
                extentReports.CreateLog( accessBillingEvent + " section is displayed for User: " + valUser + " ");

                //29.  TMT0075003_Verify that the user is able to submit the billing request to biller using "Submit to Biller". 
                string submitBiller = project.ValidateSubmitToBillerFunctionality();
                Assert.AreEqual("shivali.sharma0427@hl.com", submitBiller);
                extentReports.CreateLog("User with  email id: "+ submitBiller+" is able to submit the Billing Request ");

                //30. TMT0075158_Verify that the Email Notification sent to biller once deal team or assistant clicks on "Submit to Biller" of the selected Accounting Distribution
                 string emailNotify = project.ValidateEmailNotificationFunctionality();
                 Assert.AreEqual("FVA Invoices (US ENGAGEMENTS ONLY)", emailNotify);
                 extentReports.CreateLog("Email notification is sent to the billers of distribution list: " + emailNotify + " upon clicking the Submit To Biller button ");

                //28.  TMT0075001_Verify that the user is not allowed to delete the added PV Positions to Bill.
                //project.ValidateSharingFunctionalityOfBillingRequest();

                Assert.IsTrue(project.ValidateDeleteFunctionalityOfPVPositionsToBill(), "Verified that displayed headers hyperlinks of Billing Request are same ");
                extentReports.CreateLog("Delete option to delete PV Position is not available to the user: " + stdUser + " ");

                //33.  TMT0076112_Verify that the deal team member will be able to access the billing request once it is shared with selected deal team with Read/Write access 
                usersLogin.DiffLightningLogout();
                usersLogin.SearchUserAndLogin("Hugh Nelson");
                string stdUser2 = login.ValidateUserLightning();
                Assert.AreEqual(stdUser2.Contains("Hugh Nelson"), true);
                extentReports.CreateLog("User: " + stdUser2 + " logged in ");

                opportunityHome.ValidateParentProjectUnderHLBanker();
                project.ValidateSearchFunctionalityOfParentProject("Combo O’Connor Global");
                project.ValidateBillingRequestLink();
                project.ClickCreatedBillingRequest();
                Assert.IsTrue(project.ValidateBillingRequestHeaders(), "Verified that displayed headers of Billing Request are same ");
                extentReports.CreateLog("Billing Request page with required headers is displayed to Deal Team member post sharing the Billing Request ");

                //28. -Admin  TMT0075001_Verify that the user is not allowed to delete the added PV Positions to Bill.
                usersLogin.DiffLightningLogout();
                string PVDeleteAdmin = project.ValidateDeleteFunctionalityOfPVPositionsToBillWithAdmin();
                Assert.AreEqual("PV Position To Bill deleted successfully", PVDeleteAdmin);
                extentReports.CreateLog("PV Position To Bill is deleted successfully by Admin ");

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

    

