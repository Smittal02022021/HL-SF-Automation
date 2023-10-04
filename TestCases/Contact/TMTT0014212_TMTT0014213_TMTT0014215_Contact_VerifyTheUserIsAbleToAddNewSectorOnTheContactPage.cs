using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Companies;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using SF_Automation.Pages.Contact;
using System;


namespace SF_Automation.TestCases.Contact
{
    class TMTT0014212_TMTT0014213_TMTT0014215_Contact_VerifyTheUserIsAbleToAddNewSectorOnTheContactPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        ContactHomePage conHome = new ContactHomePage();
        ContactCreatePage createContact = new ContactCreatePage();
        ContactDetailsPage contactDetails = new ContactDetailsPage();
        ContactSelectRecordPage conSelectRecord = new ContactSelectRecordPage();
        HomeMainPage homePage = new HomeMainPage();
        UsersLogin usersLogin = new UsersLogin();
        CompanyDetailsPage companyDetail = new CompanyDetailsPage();
        CoverageSectorDependenciesHomePage coverageSectorDependenciesHome = new CoverageSectorDependenciesHomePage();
        NewCoverageSectorDependenciesPage newCoverageSectorDependencies = new NewCoverageSectorDependenciesPage();
        CoverageSectorDependenciesDetailPage coverageSectorDependenciesDetail = new CoverageSectorDependenciesDetailPage();

        public static string fileTC14212 = "TMTT0014212_VerifyTheUserIsAbleToAddNewSectorOnContactCompanyOpportunityEngagementPages";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void Contact_VerifyTheUserIsAbleToAddNewSectorOnTheContactPage()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC14212;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Handling salesforce Lightning
                login.HandleSalesforceLightningPage();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Navigate to Coverage Sector Dependencies home page
                companyDetail.NavigateToCoverageSectorDependenciesPage();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver,"Coverage Sector Dependencies: Home ~ Salesforce - Unlimited Edition"),true);
                extentReports.CreateLog(driver.Title + " page is displayed ");

                //Click New Coverage Sector Dependencies Button
                coverageSectorDependenciesHome.ClickNewCoverageDependenciesButton();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver,"Coverage Sector Dependency Edit: New Coverage Sector Dependency ~ Salesforce - Unlimited Edition"),true);
                extentReports.CreateLog(driver.Title + " page is displayed ");

                //Create New coverage Sector Dependency
                newCoverageSectorDependencies.CreateNewCoverageSectorDependency(fileTC14212);

                //Fetch the Coverage Sector Dependency Name from its detail page
                string coverageSectorDependencyName = coverageSectorDependenciesDetail.GetCoverageSectorDependencyName();
                extentReports.CreateLog("Coverage Sector Dependency Name is: " + coverageSectorDependencyName + " ");

                //Search user by global search
                string user = ReadExcelData.ReadDataMultipleRows(excelPath,"Users",2,1);
                homePage.SearchUserByGlobalSearch(fileTC14212,user);
                string userPeople = homePage.GetPeopleOrUserName();
                Assert.AreEqual(user,userPeople);
                extentReports.CreateLog("User " + userPeople + " details are displayed ");

                //Login user
                usersLogin.LoginAsSelectedUser();

                //Handling salesforce Lightning
                login.HandleSalesforceLightningPage();

                string userName = login.ValidateUser();
                Assert.AreEqual(ReadExcelData.ReadDataMultipleRows(excelPath,"Users",2,1).Contains(userName),true);
                extentReports.CreateLog("Standard User: " + userName + " is able to login ");

                //Calling click contact function
                conHome.ClickContact();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver,"Salesforce - Unlimited Edition"),true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling click add contact function
                conHome.ClickAddContact();
                extentReports.CreateLog("User navigate to add contact page. ");

                //Calling Create Contact function to create new external contact
                string contactName = ReadExcelData.ReadData(excelPath, "Contact", 4);
                createContact.CreateContact(fileTC14212);
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Contact: " + contactName + " ~ Salesforce - Unlimited Edition"),true);
                extentReports.CreateLog("New external contact is created. ");

                //Verify if Contact Sector Quick Link is displayed
                Assert.IsTrue(contactDetails.VerifyIfContactSectorQuickLinkIsDisplayed());
                extentReports.CreateLog("Contact Sector Quick Link is displayed on contact details page. ");

                //Click on new Contact Sector button
                contactDetails.ClickNewContactSectorButton();

                //Validating Title of Contact Sector Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver,"Contact Sector Edit: New Contact Sector ~ Salesforce - Unlimited Edition"),true);
                extentReports.CreateLog(driver.Title + " is displayed. ");

                //Select Coverage Sector Dependency
                contactDetails.SelectCoverageSectorDependencyForContactSector(coverageSectorDependencyName);

                //Save new contact sector details
                contactDetails.SaveNewContactSectorDetails();
                string conSecName = contactDetails.GetContactSectorName();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver,"Contact Sector: " + conSecName + " ~ Salesforce - Unlimited Edition"),true);
                extentReports.CreateLog("Contact Sector with name: " + conSecName + " is created successfully. ");

                //Validating filters functionality is working properly on coverage sector dependency popup
                Assert.IsTrue(contactDetails.VerifyFiltersFunctionalityOnCoverageSectorDependencyPopUp(fileTC14212,coverageSectorDependencyName));
                extentReports.CreateLog("Filters functionality is working properly on coverage sector dependency popup. ");

                //Save contact sector details
                contactDetails.SaveNewContactSectorDetails();

                //Navigate to contact detail page from contact sector detail page
                contactDetails.NavigateToContactDetailPageFromContactSectorDetailPage();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver,"Contact: " + contactName + " ~ Salesforce - Unlimited Edition"),true);
                extentReports.CreateLog("User navigated back to Contact detail page. ");

                //Verify if contact sector is added successfully to a company or not
                Assert.IsTrue(contactDetails.VerifyContactSectorAddedToContactOrNot(conSecName));
                extentReports.CreateLog("Contact sector: " + conSecName + " is added successfully to a contact: " + contactName + " ");

                //Delete contact sector
                contactDetails.DeleteContactSector();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver,"Contact Sectors: Home ~ Salesforce - Unlimited Edition"),true);
                extentReports.CreateLog("Contact Sector deleted successfully. ");

                usersLogin.UserLogOut();

                //Delete Coverage Sector Dependency
                contactDetails.NavigateToCoverageSectorDependenciesPage();
                coverageSectorDependenciesHome.NavigateToCoverageSectorDependencyDetailPage(coverageSectorDependencyName);
                coverageSectorDependenciesDetail.DeleteCoverageSectorDependency();
                Assert.AreEqual(WebDriverWaits.TitleContains(driver,"Coverage Sector Dependencies: Home ~ Salesforce - Unlimited Edition"),true);
                extentReports.CreateLog("Coverage sector dependency deleted successfully. ");

                //To Delete created contact
                contactDetails.DeleteCreatedContact(fileTC14212,"External Contact");
                Assert.AreEqual(WebDriverWaits.TitleContains(driver,"Salesforce - Unlimited Edition"),true);
                extentReports.CreateLog("Created contact is deleted successfully ");

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
