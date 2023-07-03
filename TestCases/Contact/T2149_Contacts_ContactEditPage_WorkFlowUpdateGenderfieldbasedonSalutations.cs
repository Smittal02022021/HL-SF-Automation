﻿using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Threading;

namespace SF_Automation.TestCases.Contact
{
    class T2149_Contacts_ContactEditPage_WorkFlowUpdateGenderfieldbasedonSalutations : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        ContactHomePage contactHome = new ContactHomePage();
        UsersLogin usersLogin = new UsersLogin();
        SendEmailNotification sendEmail = new SendEmailNotification();
        HomeMainPage homePage = new HomeMainPage();
        ContactEditPage contactEdit = new ContactEditPage();

        public static string fileTC2149 = "T2149_Contacts_ContactEditPage_WorkFlowUpdateGenderfieldbasedonSalutations";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void CreateContactTypeExternalContactwithSalutation()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2149;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");
               
                // Calling click contact function
                conHome.ClickContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Salesforce - Unlimited Edition"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                // Verify "Mr,Mrs and MS" Salutations auto updates Gender field in contacts page
                for (int row = 2; row <= ReadExcelData.GetRowCount(excelPath, "Contact"); row++)
                {
                    conHome.ClickAddContact();
                    
                    createContact.CreateContactwithSalutation(fileTC2149, row);
                   
                    string txtGender = createContact.GetGender();
                    Assert.AreEqual(txtGender, ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row,9));
                    extentReports.CreateLog("Gender: "+txtGender+" is displaying when salutation :"+ ReadExcelData.ReadDataMultipleRows(excelPath, "Contact", row, 8) + " is selected");

                   //To Delete created contact
                   contactDetails.DeleteCreatedContact(fileTC2149, ReadExcelData.ReadDataMultipleRows(excelPath, "ContactTypes", 2, 1));

                   
                }

                extentReports.CreateLog("Deletion of Created Contacts ");
                usersLogin.UserLogOut();
                driver.Quit();
            }

            catch(TimeoutException te)
            {
                extentReports.CreateLog(te.Message);
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