﻿using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.Company;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.Engagement;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.Opportunity;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.ERPtoOracle
{
    class TS10_VerifyICOContractWhenStaffFromDifferentLegalEntity_Part2 : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        CompanyHomePage companyHome = new CompanyHomePage();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CompanySelectRecordPage companySelectRecord = new CompanySelectRecordPage();
        HomeMainPage homePage = new HomeMainPage();
        CompanyCreatePage createCompany = new CompanyCreatePage();
        CompanyEditPage companyEdit = new CompanyEditPage();
        UsersLogin usersLogin = new UsersLogin();
        AddOpportunityPage addOpportunity = new AddOpportunityPage();
        OpportunityDetailsPage oppDetails = new OpportunityDetailsPage();
        AdditionalClientSubjectsPage clientSubjectsPage = new AdditionalClientSubjectsPage();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        AddOpportunityContact addOpportunityContact = new AddOpportunityContact();
        OpportunityHomePage opportunityHome = new OpportunityHomePage();
        ContactCreatePage contactCreate = new ContactCreatePage();
        ContactDetailsPage contactDetail = new ContactDetailsPage();
        EngagementDetailsPage engageDetail = new EngagementDetailsPage();
        ContractDetailsPage contractDetail = new ContractDetailsPage();
        EngagementHomePage enggHome = new EngagementHomePage();

        public static string ERPSectionWithExistingCompany = "TS10_VerifyICOContractWhenStaffFromDiffLegalEntity.xlsx";
        public static string xlPath = "TS10_EngagementDetails.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void TestICOContractExistingCompany2()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + ERPSectionWithExistingCompany;
                Console.WriteLine(excelPath);

                string enggExcelPath = ReadJSONData.data.filePaths.testData + xlPath;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowCompanyName = ReadExcelData.GetRowCount(excelPath, "Company");
                for (int row = 2; row <= rowCompanyName; row++)
                {
                    // Search standard user by global search
                    string user = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);

                    homePage.SearchUserByGlobalSearch(ERPSectionWithExistingCompany, user);
                    string userPeople = homePage.GetPeopleOrUserName();
                    string userPeopleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    Assert.AreEqual(userPeopleExl, userPeople);
                    extentReports.CreateLog("User " + userPeople + " details are displayed ");

                    //Login as standard user
                    usersLogin.LoginAsSelectedUser();
                    string StandardUser = login.ValidateUser();
                    Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1).Contains(StandardUser), true);
                    extentReports.CreateLog("Standard User: " + StandardUser + " is able to login ");
                    string enggName = ReadExcelData.ReadDataMultipleRows(enggExcelPath, "EngagementNames", row, 1).ToString();
                    enggHome.SearchEngagementWithName(enggName);

                    string engNumber = engageDetail.GetEngagementNumber();
                    string erpSubmittedToSyncEngg = engageDetail.GetERPSubmittedToSync();
                    string erpLegalEntity = engageDetail.GetERPLegalEntity();
                    string erpBusinessUnitId = engageDetail.GetERPBusinessUnitId();
                    string erpBusinessUnit = engageDetail.GetERPBusinessUnit();
                    string erpProjectNumber = engageDetail.GetERPProjectNumber();
                    string erpId = engageDetail.GetERPId();

                    engageDetail.ClickICOContractLink(row);

                    string erpSubmittedToSyncContract = contractDetail.GetERPSubmittedToSyncICO();
                    //Assert.AreEqual(erpSubmittedToSyncEngg, erpSubmittedToSyncContract);
                    DateTime submittedToSyncContract = Convert.ToDateTime(erpSubmittedToSyncContract);
                    DateTime submittedTosyncEngg = Convert.ToDateTime(erpSubmittedToSyncEngg);
                    int ERPSubmittedDateCompare = DateTime.Compare(submittedToSyncContract, submittedTosyncEngg);
                    Assert.AreEqual(1, ERPSubmittedDateCompare);
                    extentReports.CreateLog("Contract ERP Submitted To Sync DateTime stamp: " + erpSubmittedToSyncContract + " matched with time stamp of engagment ");


                    string erpLegalEntityContract = contractDetail.GetERPLegalEntityICO();
                    Assert.AreEqual(erpLegalEntity, erpLegalEntityContract);
                    extentReports.CreateLog("Contract ERP Legal Entity: " + erpLegalEntityContract + " matched with legal entity of engagement ");

                    string erpLegalEntityNameContract = contractDetail.GetERPLegalEntityNameICO();
                    string valRecordType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 25);
                    string erpLegalEntityLOB = contractDetail.GetLegalEntityBasedOnLOB(valRecordType);
                    Assert.AreEqual(erpLegalEntityLOB, erpLegalEntityNameContract);
                    extentReports.CreateLog("Contract ERP Legal Entity Name: " + erpLegalEntityNameContract + " is displayed ");

                    string erpBusinessUnitContract = contractDetail.GetERPBusinessUnitICO();
                    string erpBusinessUnitICO = contractDetail.GetERPBusinessUnitICO(erpLegalEntityLOB);
                    Assert.AreEqual(erpBusinessUnitICO, erpBusinessUnitContract);
                    extentReports.CreateLog("Contract ERP Business Unit: " + erpBusinessUnitContract + " is matched with deal team member's BU ");

                    string erpExpenditureBusinessUnit = contractDetail.GetERPExpenditureBusinessUnitICO();
                    Assert.AreEqual(erpBusinessUnit, erpExpenditureBusinessUnit);
                    extentReports.CreateLog("Contract Expenditure ERP Business Unit: " + erpExpenditureBusinessUnit + " is matched with Business Unit of engagement ");

                    string erpBusinessUnitIdContract = contractDetail.GetERPBusinessUnitIdICOContract();
                    string erpBusinessUnitIdICO = contractDetail.GetERPBusinessUnitIdICO(erpLegalEntityLOB);
                    Assert.AreEqual(erpBusinessUnitIdICO, erpBusinessUnitIdContract);
                    extentReports.CreateLog("Contract ERP Business Unit Id: " + erpBusinessUnitContract + " is matched with deal team member's BU ");

                    string erpExpenditureTypeName = contractDetail.GetERPExpenditureTypeNameICO();
                    Assert.AreEqual("65540-Other Seminars", erpExpenditureTypeName);
                    extentReports.CreateLog("Contract ERP Expenditure Type Name: " + erpExpenditureTypeName + " is displayed ");

                    string erpProjectNumberContract = contractDetail.GetERPProjectNumberICO();
                    Assert.AreEqual(erpProjectNumber, erpProjectNumberContract);
                    extentReports.CreateLog("Contract ERP Project Number: " + erpProjectNumberContract + " is matched with ERP Project Number of engagement ");

                    string erpOrganization = contractDetail.GetERPOrganizationICO();
                    Assert.AreEqual(erpLegalEntityNameContract, erpOrganization);
                    extentReports.CreateLog("Contract ERP Organization: " + erpOrganization + " is displayed ");

                    Assert.IsTrue(contractDetail.GetERPContractNumber());
                    extentReports.CreateLog("ERP Contract Number is displayed ");

                    Assert.IsTrue(contractDetail.GetERPIdContract());
                    extentReports.CreateLog("ERP Id is displayed ");

                    string erpProjectId = contractDetail.GetERPProjectIdICO();
                    Assert.AreEqual(erpId, erpProjectId);
                    extentReports.CreateLog("Contract ERP Project Id: " + erpProjectId + " is matched with ERP Id of engagement ");

                    //Verify task number
                    string erpTaskNumber = contractDetail.GetERPTaskNumber();
                    string erpTaskNumberBasedOnLegalEntity = contractDetail.GetERPTaskValue(erpLegalEntityLOB, erpTaskNumber);//(erpLegalEntityContract, erpTaskNumber);
                    Assert.AreEqual(erpTaskNumberBasedOnLegalEntity, erpTaskNumber);
                    extentReports.CreateLog("ERP Task Number: " + erpTaskNumber + " is displayed ");

                    //Verify task name on contract detail page
                    string erpTaskName = contractDetail.GetERPTaskName();
                    Assert.AreEqual(erpTaskNumberBasedOnLegalEntity, erpTaskName);
                    extentReports.CreateLog("ERP Task Name: " + erpTaskName + " is displayed ");

                    //Verify task desc on contract detail page
                    string erpTaskDescription = contractDetail.GetERPTaskDesc();
                    Assert.AreEqual(erpTaskNumberBasedOnLegalEntity, erpTaskDescription);
                    extentReports.CreateLog("ERP Task Name: " + erpTaskName + " is displayed ");

                    //Validate ERP submitted to sync date
                    string cstDate = CustomFunctions.GetCurrentPSTDate();
                    string ERPLastIntegrationResponseDate = contractDetail.GetERPLastIntegrationResponseDateICO();
                    string ERPSubmittedToSync = contractDetail.GetERPSubmittedToSync();
                    //extentReports.CreateTest("Verify ERP Last Integration response date");
                    Assert.IsTrue(ERPLastIntegrationResponseDate.Contains(cstDate));
                    extentReports.CreateLog("ERP Last Integration response date is updated with latest time stamp ");

                    //Verify Last Integration status
                    string ERPLastIntegrationStatus = contractDetail.GetERPLastIntegrationStatusICO();
                    //extentReports.CreateTest("Verify ERP Last Integration Status:");
                    Assert.AreEqual("Success", ERPLastIntegrationStatus);
                    extentReports.CreateLog("ERP Last Integration Status: " + ERPLastIntegrationStatus + " is updated ");

                    contractDetail.ClickClientLinks(row);

                    string erpOrgPartyId = companyDetail.GetERPOrgPartyId();
                    string erpBillToAddressId = companyDetail.GetERPBillToAddressId();
                    string erpContactId = companyDetail.GetERPContactId();
                    string erpShipToAddressId = companyDetail.GetERPShipToAddressId();
                    string clientNumber = companyDetail.GetClientNumber();
                    string erpFirstName = companyDetail.GetERPContactFirstName();
                    string erpLastName = companyDetail.GetERPContactLastName();

                    //companyDetail.ClickPrimaryBillingContact(row);
                    //string companyNameFromContact = contactDetail.GetCompanyName();
                    //string contactName = contactDetail.GetContactName();

                    if (row.Equals(2))
                    {
                        CustomFunctions.SwitchToWindow(driver, 1);
                    }
                    else if (row.Equals(3))
                    {
                        CustomFunctions.SwitchToWindow(driver, 3);
                    }
                    else
                    {
                        CustomFunctions.SwitchToWindow(driver, 7);
                    }

                    string billTo = contractDetail.GetBillToICO();
                    Assert.AreEqual(erpLegalEntity, billTo);
                    extentReports.CreateLog("Bill To: " + billTo + " matches with Legal Entity ");

                    string billingContact = contractDetail.GetBillingContactICO();
                    string contactName = erpFirstName + " " + erpLastName;
                    Assert.AreEqual(contactName, billingContact);
                    extentReports.CreateLog("Contract Billing Contact: " + billingContact + " matches with billing contact ");

                    string erpBillToCustomerId = contractDetail.GetBillToCustomerIdICO();
                    Assert.AreEqual(erpOrgPartyId, erpBillToCustomerId);
                    extentReports.CreateLog("Contract Bill to Customer Id: " + erpBillToCustomerId + " matches with erp org party id of company ");

                    string contractErpBillToAddressId = contractDetail.GetERPBillToAddressIdICO();
                    Assert.AreEqual(erpBillToAddressId, contractErpBillToAddressId);
                    extentReports.CreateLog("Contract Bill to Address Id: " + contractErpBillToAddressId + " matches with bill to address id of company ");

                    string contractErpBillToContactId = contractDetail.GetERPBillToContactIdICO();
                    Assert.AreEqual(erpContactId, contractErpBillToContactId);
                    extentReports.CreateLog("Contract Bill to Contact Id: " + contractErpBillToContactId + " matches with bill to contact id of company ");

                    string shipTo = contractDetail.GetShipToICO();
                    Assert.AreEqual(erpLegalEntity, shipTo);
                    extentReports.CreateLog("Ship To: " + shipTo + " matches with company of billing contact ");

                    string contractErpShipToCustomerId = contractDetail.GetERPShipToCustomerIdICO();
                    Assert.AreEqual(erpOrgPartyId, contractErpShipToCustomerId);
                    extentReports.CreateLog("Contract ship to customer Id: " + contractErpShipToCustomerId + " matches with erp org party id of ship to company ");

                    string contractErpShipToAddressId = contractDetail.GetERPShipToAddressIdICO();
                    Assert.AreEqual(erpShipToAddressId, contractErpShipToAddressId);
                    extentReports.CreateLog("Contract ship to address Id: " + contractErpShipToAddressId + " matches with erp ship to address id of ship to company ");

                    string contractShipToAccountNumber = contractDetail.GetERPShipToAccountNumberICO();
                    Assert.AreEqual(clientNumber, contractShipToAccountNumber);
                    extentReports.CreateLog("Contract ERP ship to account number: " + contractShipToAccountNumber + " matches with client number of ship to company ");

                    usersLogin.UserLogOut();
                    extentReports.CreateLog("Standard user is logged out ");

                }
                // Logout from standard User
                usersLogin.UserLogOut();
                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }
    }
}          