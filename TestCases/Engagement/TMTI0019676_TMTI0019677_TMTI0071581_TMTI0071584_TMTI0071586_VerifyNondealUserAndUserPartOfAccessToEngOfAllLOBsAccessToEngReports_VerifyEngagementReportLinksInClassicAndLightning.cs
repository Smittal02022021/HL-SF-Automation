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
    class TMTI0019676_TMTI0019677_TMTI0071581_TMTI0071584_TMTI0071586_VerifyNondealUserAndUserPartOfAccessToEngOfAllLOBsAccessToEngReports_VerifyEngagementReportLinksInClassicAndLightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();    
        UsersLogin usersLogin = new UsersLogin();
        OpportunityDetailsPage opportunityDetails = new OpportunityDetailsPage();
        EngagementDetailsPage engagementDetails = new EngagementDetailsPage();
        EngagementHomePage engHome = new EngagementHomePage();

        public static string TMTI0019676 = "VerifyEngagementReportLinks1.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyEngagementReportLinks()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + TMTI0019676;                
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");   

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                 Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                 extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as CF Financial User and validate the user
                string valUser1 = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 4, 1);
                Console.WriteLine("valUser1 - " + valUser1);

                usersLogin.SearchUserAndLogin(valUser1);
                bool stdUser1 = login.ValidateUserLightningView(TMTI0019676, 4);
                Assert.IsTrue(stdUser1);
                extentReports.CreateLog("User: " + valUser1 + " logged in ");

                //Open the selected Engagement
                string searchedEng1 = engHome.ValidateSearchFunctionalityOfEngagements("114595");
                engHome.ClickEngNumber();

                //Validate Report tab
                string report = engagementDetails.ValidateReportTab();
                Assert.AreEqual("Report", report);
                extentReports.CreateLog("Tab: " + report + " is displayed under More tab on Engagement details page ");

                //Validate Engagement AR Receipt report
                string titleEngAR = engagementDetails.ValidateEngARReceiptReport();
                Assert.AreEqual("Engagement AR Receipt", titleEngAR);
                extentReports.CreateLog("Page with title: " + titleEngAR + " is displayed upon clicking Engagement AR Receipt report link ");

                //Validate Engagement Expenses report
                string titleEngExp = engagementDetails.ValidateEngExpReport();
                Assert.AreEqual("Engagement Expenses", titleEngExp);
                extentReports.CreateLog("Page with title: " + titleEngExp + " is displayed upon clicking Engagement Expenses report link ");

                //Validate Engagement Invoice Details report
                string titleEngInvoice = engagementDetails.ValidateEngInvoiceReport();
                Assert.AreEqual("Engagement Invoice Details", titleEngInvoice);
                extentReports.CreateLog("Page with title: " + titleEngInvoice + " is displayed upon clicking Engagement Invoice Details link ");

                //Logout of the user and click on Switch To Lightning Experience link
                usersLogin.LightningLogout();
                usersLogin.SearchUserAndLogin("Emre Abale");
                string stdUser2 = login.ValidateUser();
                Assert.AreEqual(stdUser2.Contains("Emre Abale"), true);
                extentReports.CreateLog("User: " + stdUser2 + " logged in ");

                //Open the selected Engagement
                engHome.SearchEngagementWithNumber("106347");

                //Validate Engagement Report page
                string reportAdmin = engagementDetails.ValidateEngReportButton();
                Assert.AreEqual("Report", reportAdmin);
                extentReports.CreateLog("Page: " + reportAdmin + " is displayed after clicking the Engagement Report button ");

                //Validate Engagement Working Group list
                string titleEngWorking = engagementDetails.ValidateEngWorkingGroupReport();
                Assert.AreEqual("Working Group List", titleEngWorking);
                extentReports.CreateLog("Page with title: " + titleEngWorking + " is displayed upon clicking Engagement Working Group list ");

                usersLogin.UserLogOut();

                int rowUser = ReadExcelData.GetRowCount(excelPath, "Users");

                //Validate that non deal team member can view only 2 reports
                for (int row = 2; row <= rowUser; row++)
                {
                    string valUser = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    string valEng = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 2);

                    //Login as Financial User and validate the user                
                    usersLogin.SearchUserAndLogin(valUser);
                  
                    if (valUser.Equals("Thomas Bailey") || valUser.Equals("Mark Martin") || valUser.Equals("James Craven"))
                    {

                        bool stdUser = login.ValidateUserLightningView(TMTI0019676, row);
                        Assert.IsTrue(stdUser);
                        extentReports.CreateLog("User: " + valUser + " logged in ");

                        //Open the selected Engagement
                        string searchedEng = engHome.ValidateSearchFunctionalityOfEngagements(valEng);
                        engHome.ClickEngNumber();

                        //Validate displayed Engagement Reports for non deal team member, deal team member and user who is in Public group
                        engagementDetails.ClickEngReportsButton();

                        if (valUser.Equals("Thomas Bailey"))
                        {
                            Assert.IsTrue(engagementDetails.VerifyReportNamesForNonDealTeamMemberLightning(), "Verified that displayed reports are same");
                            extentReports.CreateLog("Only 2 reports are displayed for non deal team member - " + valUser + " for CF engagement  ");
                            usersLogin.LightningLogout();
                        }
                        else
                        {
                            Assert.IsTrue(engagementDetails.VerifyReportNamesForDealTeamMemberLightning(), "Verified that displayed reports are same");
                            if (valUser.Equals("Mark Martin"))
                            {
                                extentReports.CreateLog("All required reports are displayed for deal team member - " + valUser + " for CF engagement ");
                            }
                            else
                            {
                                extentReports.CreateLog("All required reports are displayed for user - " + valUser + " who is part of Access To Engagement Report public group for CF engagement ");
                            }
                            usersLogin.LightningLogout();
                        }
                    }

                    else if(valUser.Equals("Drew Koecher") || valUser.Equals("Jennifer Muller") || valUser.Equals("Danielle Morello"))
                    {
                        string stdUser = login.ValidateUser();
                        Assert.AreEqual(stdUser.Contains(valUser), true);
                        extentReports.CreateLog("User: " + valUser + " logged in ");

                        //Open the selected Engagement 
                        string searchedEng = engHome.SearchEngagementWithNumber(valEng);

                        //Validate displayed Engagement Reports for non deal team member, deal team member and user who is in Public group
                        engagementDetails.ValidateEngReportButton();
                        if (valUser.Equals("Jennifer Muller"))
                        {
                            Assert.IsTrue(engagementDetails.VerifyReportNamesForNonDealMemberClassic(), "Verified that displayed reports are same");
                            extentReports.CreateLog("Only 2 reports are displayed for non deal team member - " + valUser + " for FVA engagement ");
                            usersLogin.UserLogOut();
                        }
                        else
                        {
                            Assert.IsTrue(engagementDetails.VerifyReportNamesForDealTeamMemberClassic(), "Verified that displayed reports are same");
                            if (valUser.Equals("Danielle Morello"))
                            {
                                extentReports.CreateLog("All required reports are displayed for user - " + valUser + " who is part of Access To Engagement Report public group for FVA engagement ");
                                //driver.SwitchTo().DefaultContent();
                            }
                            else
                            {
                                extentReports.CreateLog("All required reports are displayed for deal team member - " + valUser + " for FVA engagement ");
                            }
                            usersLogin.UserLogOut();
                            Console.WriteLine("User " + valUser + "log out successfully");
                        }
                    }

                    else
                    {
                        string stdUser = login.ValidateUser();
                        Assert.AreEqual(stdUser.Contains(valUser), true);
                        extentReports.CreateLog("User: " + valUser + " logged in ");

                        //Open the selected Engagement 
                        string searchedEng = engHome.SearchEngagementWithNumber(valEng);

                        //Validate displayed Engagement Reports for non deal team member, deal team member and user who is in Public group
                        engagementDetails.ValidateEngReportButton();
                        if (valUser.Equals("Aaron Schultz"))
                        {
                            Assert.IsTrue(engagementDetails.VerifyReportNamesForNonDealMemberClassic(), "Verified that displayed reports are same");
                            extentReports.CreateLog("Only 2 reports are displayed for non deal team member - " + valUser + " for FR engagement ");
                            usersLogin.UserLogOut();
                        }
                        else
                        {
                            Assert.IsTrue(engagementDetails.VerifyReportNamesForDealTeamMemberClassic(), "Verified that displayed reports are same");
                            if (valUser.Equals("Ayati Arvind"))
                            {
                                extentReports.CreateLog("All required reports are displayed for user - " + valUser + " who is part of Access To Engagement Report public group for FR engagement ");
                                //driver.SwitchTo().DefaultContent();
                            }
                            else
                            {
                                extentReports.CreateLog("All required reports are displayed for deal team member - " + valUser + " for FR engagement ");
                            }
                            usersLogin.UserLogOut();
                            Console.WriteLine("User " + valUser + "log out successfully");                           
                        }                        
                    }
                    
                }
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

    

