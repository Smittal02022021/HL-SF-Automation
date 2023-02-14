using NUnit.Framework;
using SalesForce_Project.Pages;
using SalesForce_Project.Pages.Common;
using SalesForce_Project.Pages.Engagement;
using SalesForce_Project.TestData;
using SalesForce_Project.UtilityFunctions;
using System;
using System.Globalization;

namespace SalesForce_Project.TestCases.Engagement
{
    class TMTT0017891_ViewCounterparties_EditBidFunctionality : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        AddCounterparty counterparty = new AddCounterparty();
        public static string fileTC7877 = "TMTT0017877_LightningEngagement2";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ViewCounterparties_EmailButton()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC7877;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowUsers = ReadExcelData.GetRowCount(excelPath, "Users");
                Console.WriteLine("rowCount " + rowUsers);

                for (int row = 2; row <= rowUsers; row++)
                {

                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);


                    //Login as CF Financial User and validate the user
                    if (valUser.Equals("William Peluchiwski"))
                    {
                        usersLogin.SearchUserAndLogin(valUser);
                        string cfUser = login.ValidateUserLightningCF();
                        Assert.AreEqual(valUser, cfUser);
                        extentReports.CreateLog("User: " + cfUser + " is able to login ");
                    }
                    else
                    {
                        usersLogin.SearchUserAndLogin(valUser);
                        string stdUser = login.ValidateUserLightning();
                        Assert.AreEqual(valUser, stdUser);
                        extentReports.CreateLog("User: " + stdUser + " is able to login ");
                    }

                    //Search for Engagement on lightning
                    string message = engHome.SearchEngagementWithNumberOnLightning("111861", "Buyside");
                    extentReports.CreateLog("Engagement details are displayed upon searching required engagement ");
                                        

                    if (valUser.Equals("William Peluchiwski"))
                    {

                        //Validate the View Counterparties button
                        string viewCounterparty = engagementDetails.ValidateVisibilityOfViewCounterpartiesButton();
                        Assert.AreEqual("View Counterparties button is not displayed", viewCounterparty);
                        extentReports.CreateLog(viewCounterparty + " for User: " + valUser);
                    }
                    else
                    {
                        //Validate the View Counterparties button
                        string viewCounterparty = engagementDetails.ValidateVisibilityOfViewCounterpartiesButton();
                        Assert.AreEqual("View Counterparties button is displayed", viewCounterparty);
                        extentReports.CreateLog("Button with name : " + viewCounterparty + " for user: " + valUser);

                        //Click on Lightning Counterparties button, click on details and click on Eng Counterparty Contact
                        engagementDetails.ClickViewCounterpartiesButton();

                        //Click on Edit Bids button and New Bid Round button
                        counterparty.ClickEditBidsButton();
                        counterparty.ClickNewBidRoundAndSelectFirstRound();

                        //Validate all columns corresponding to selected Bid
                        string compName = counterparty.ValidateCompanyName();
                        Assert.AreEqual("Company Name", compName);
                        extentReports.CreateLog("Column with name : " + compName + " is displayed for Round First ");

                        string minBid = counterparty.ValidateMinBid();
                        Assert.AreEqual("Min Bid", minBid);
                        extentReports.CreateLog("Column with name : " + minBid + " is displayed for Round First ");

                        string maxBid = counterparty.ValidateMaxBid();
                        Assert.AreEqual("Max Bid", maxBid);
                        extentReports.CreateLog("Column with name : " + maxBid + " is displayed for Round First ");

                        string equity = counterparty.ValidateEquity();
                        Assert.AreEqual("Equity %", equity);
                        extentReports.CreateLog("Column with name : " + equity + " is displayed for Round First ");

                        string debt = counterparty.ValidateDebt();
                        Assert.AreEqual("Debt %", debt);
                        extentReports.CreateLog("Column with name : " + debt + " is displayed for Round First ");

                        string bidDate = counterparty.ValidateBidDate();
                        Assert.AreEqual("Bid Date", bidDate);
                        extentReports.CreateLog("Column with name : " + bidDate + " is displayed for Round First ");

                        string comments = counterparty.ValidateComments();
                        Assert.AreEqual("Comments", comments);
                        extentReports.CreateLog("Column with name : " + comments + " is displayed for Round First ");

                        counterparty.SaveAllDetailsOfBid();
                    }

                    usersLogin.DiffLightningLogout();
                }               
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
