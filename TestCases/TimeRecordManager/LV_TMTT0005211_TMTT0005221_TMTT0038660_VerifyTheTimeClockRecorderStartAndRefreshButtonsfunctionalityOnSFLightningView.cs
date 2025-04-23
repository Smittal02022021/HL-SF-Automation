using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class LV_TMTT0005211_TMTT0005221_TMTT0038660_VerifyTheTimeClockRecorderStartAndRefreshButtonsfunctionalityOnSFLightningView:BaseClass
    {
        //Time Tracking Litigation
        //Time Tracking Litigation Supervisor

        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RefreshButtonFunctionality refreshButton = new RefreshButtonFunctionality();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();

        public static string fileTMTT0038660 = "LV_TMTT0038660_VerifyTheFunctionalityOfTimeClockRecorderForFVAStandardUserOnSFLightningView";
        
        private int GetSecondsTimer;
        private int GetHoursTimer;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }
        [Test]
        public void VerifyFVAStandardUserTimeClockRecorderFunctionalitiesLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0038660;

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed. ");

                //Calling Login function                
                login.LoginApplication();

                //Switch to lightning view
                if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                {
                    homePage.SwitchToLightningView();
                    extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                }

                //Validate user logged in
                Assert.AreEqual(driver.Url.Contains("lightning"), true);
                extentReports.CreateStepLogs("PAssed", "Admin User is able to login into SF");

                //Select HL Banker app
                try
                {
                    lvHomePage.SelectAppLV("HL Banker");
                }
                catch(Exception)
                {
                    lvHomePage.SelectAppLV1("HL Banker");
                }

                int rowCount = ReadExcelData.GetRowCount(excelPath, "Users");

                for (int row = 2; row <= rowCount; row++)
                {
                    //Login as Standard User and validate the user
                    string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);

                    //Search CF Financial user by global search
                    lvHomePage.SearchUserFromMainSearch(userExl);

                    //Verify searched user
                    Assert.AreEqual(WebDriverWaits.TitleContains(driver, userExl + " | Salesforce"), true);
                    extentReports.CreateStepLogs("Passed", "User " + userExl + " details are displayed ");

                    //Login as CF Financial user
                    lvHomePage.UserLogin();

                    //Switch to lightning view
                    if(driver.Title.Contains("Salesforce - Unlimited Edition"))
                    {
                        homePage.SwitchToLightningView();
                        extentReports.CreateStepLogs("Info", "User switched to lightning view. ");
                    }

                    Assert.IsTrue(lvHomePage.VerifyUserIsAbleToLogin(userExl));
                    extentReports.CreateStepLogs("Passed", "CF Financial User: " + userExl + " has logged in ");

                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectAppLV(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");

                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + userExl);
                    randomPages.WaitForPageLoaderLV();
                    //TMTT0038660
                    //TMTI0093775 Verify that the FVA user can access the Time Clock Recorder tab and can start the time clock
                    // TMTI0093778 Verify that the FVA Supervisor can access the Time Clock Recorder tab and can start the time clock.

                    //Click on Time Clock Recorder Tab
                    refreshButton.GoToTimeClockRecorderPageLV();

                    //Validate Time Record Period Page Title
                    string TimeClockRecorderPageTitle = refreshButton.GetTitleTimeClockRecorderPageLV();
                    string TimeClockRecorderPageTitleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "TimeClockRecorder",row, 4);
                    Assert.AreEqual(TimeClockRecorderPageTitleExl, TimeClockRecorderPageTitle);
                    extentReports.CreateLog(TimeClockRecorderPageTitle + " is displayed upon clicking Time Clock Recorder ");

                    //Check recorder is reset or not
                    refreshButton.ClickResetButtonLV();

                    //Select Project and Activity from Drop Down
                    string projectExl= ReadExcelData.ReadDataMultipleRows(excelPath, "TimeClockRecorder", row, 1);
                    string activityExl= ReadExcelData.ReadDataMultipleRows(excelPath, "TimeClockRecorder", row, 3);

                    //TMTT0005221- Start button functionalities
                    refreshButton.ClickStartButtonLV();
                    string txtErrorMesageExl= ReadExcelData.ReadData(excelPath, "Error_Message", 1);
                    string errMsg = refreshButton.GetErrorMessageStartLV();
                    Assert.IsTrue(errMsg.Contains(txtErrorMesageExl), "Error message is dispalying correct");
                    extentReports.CreateStepLogs("Passed", " Error Message: " + errMsg + " is displaying upon clicking Start Button without Selecting the Project ");
                    //----

                    refreshButton.SelectDropDownProjectandActivityLV(projectExl, activityExl);
                    extentReports.CreateStepLogs("Passed", "Selected Project and Activity from Drop down");

                    bool btnStatus= refreshButton.GetButtonStatusLV("Start");
                    Assert.IsTrue(btnStatus);
                    extentReports.CreateStepLogs("Passed", "Intially Only Start is Enabled after Selecting Project and Activity from Drop down");
                    btnStatus= refreshButton.GetButtonStatusLV("Finish");
                    Assert.IsFalse(btnStatus);
                    extentReports.CreateStepLogs("Passed", "Finish Button is Disabled Before starting the Clock Recorder");
                                        
                    //Click Start Button
                    refreshButton.ClickStartButtonLV();
                    extentReports.CreateStepLogs("Passed", "User Clicked the Start button ");

                    btnStatus= refreshButton.GetButtonStatusLV("Finish");
                    Assert.IsTrue(btnStatus);
                    extentReports.CreateStepLogs("Passed", "Finish Button is Enabled After starting the Clock Recorder");

                    //Add minutes to Timmer
                    string additionalMinutes = ReadExcelData.ReadDataMultipleRows(excelPath, "TimeClockRecorder", row, 5);
                    refreshButton.AddMinutesToTimerLV(additionalMinutes);
                    extentReports.CreateStepLogs("Info", additionalMinutes+" Minutes Added Clock Recorder Timer ");

                    //Click Finish Button
                    refreshButton.ClickFinishButtonLV();
                    extentReports.CreateStepLogs("Info", "Clicked the Finish button ");

                    //Delete Time Entry Matrix
                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Info", "Time Entry Deleted");

                    //Click on Time Clock Recorder Tab
                    refreshButton.GoToTimeClockRecorderPageLV();

                    //TMTT0005211_RefreshButtonfunctionalityLightningView                    
                    //Validate Time Record Period Page Title
                    extentReports.CreateStepLogs("Passed", "Verify Refresh Button functionality Lightning View");
                    TimeClockRecorderPageTitle = refreshButton.GetTitleTimeClockRecorderPageLV();                    
                    Assert.AreEqual(TimeClockRecorderPageTitleExl, TimeClockRecorderPageTitle);
                    extentReports.CreateStepLogs("Info", TimeClockRecorderPageTitle + " is displayed upon clicking Time Clock Recorder ");
                    refreshButton.ClickResetButtonLV();

                    //TMTI0009566
                    //Verify Refresh button is not displaying when project is not selected

                    Assert.AreEqual(btnStatus = refreshButton.GetButtonStatusLV("Reset"), false, "Verify Refresh button is not displaying before project selection ");
                    extentReports.CreateStepLogs("Passed", "Refresh button is not Active before project selection");

                    //Select Project and Activity from Drop Down
                    refreshButton.SelectDropDownProjectandActivityLV(projectExl, activityExl);
                    extentReports.CreateStepLogs("Info", "Selected Project and Activity from Drop down");

                    //Click Start Button
                    refreshButton.ClickStartButtonLV();
                    extentReports.CreateStepLogs("Info", "User Clicked the Start button ");

                    // TMTI0009559 Timer should resume upon clicking the Refresh button
                    //Click Refresh Button
                    refreshButton.ClickRefreshButtonLV();
                    extentReports.CreateStepLogs("Info", "Click Refresh button in Timer ");
                   
                    //Get Seconds Text
                    GetSecondsTimer = int.Parse(refreshButton.GetSecondsTimerLV());
                    Assert.AreNotEqual(0, GetSecondsTimer, "Seconds timer is not working fine ", true);
                    extentReports.CreateStepLogs("Passed", "Timer is resumed upon clicking Refresh Button ");

                    timeEntry.GoToWeeklyEntryMatrixLV();
                    refreshButton.GoToTimeClockRecorderPageLV();
                    extentReports.CreateStepLogs("Info", "User is navigated to Clock Recorder Page again ");

                    //Click Resume Button
                    refreshButton.ClickResumeButtonLV();
                    extentReports.CreateStepLogs("Info", "User had clicked on resume button ");
                    //Click Pause Button
                    refreshButton.ClickPauseButtonLV();
                    extentReports.CreateStepLogs("Info", "User had clicked on Pause button ");

                    //TMTI0009582 Timer should display accurate time upon clicking Refresh
                    //Click Refresh Button
                    refreshButton.ClickRefreshButtonLV();
                    extentReports.CreateStepLogs("Info", "Click Refresh button in Timer ");
                    
                    //Get Seconds Text
                    GetSecondsTimer = int.Parse(refreshButton.GetSecondsTimerLV());
                    Assert.AreNotEqual(0, GetSecondsTimer, "Seconds timer is not working fine ", true);
                    extentReports.CreateStepLogs("Passed", "Timer is displaying accurate time upon clicking Refresh Button ");

                    //TMTI0009878 Added minutes should reflect after refresh button is clicked.
                    //Update Timer
                    refreshButton.AddMinutesToTimerLV(additionalMinutes);
                    extentReports.CreateStepLogs("Info", "Update the timer ");

                    //Click Refresh Button
                    refreshButton.ClickRefreshButtonLV();
                    extentReports.CreateStepLogs("Info", "Click Refresh button in Timer ");
                    
                    //Get Hours Text
                    GetHoursTimer = int.Parse(refreshButton.GetHoursTimerLV());
                    Assert.AreEqual(2, GetHoursTimer, "Hours timer is working fine ", true);
                    extentReports.CreateStepLogs("Passed", "Added minutes are reflecting after refresh button is clicked ");

                    //Click Pause Button
                    refreshButton.ClickPauseButtonLV();
                    extentReports.CreateStepLogs("Info", "User had clicked on Pause button ");

                    //TMTI0009599 Clicking the Refresh button should not set back the hours or the minutes of the timer
                    //Click Refresh Button
                    refreshButton.ClickRefreshButtonLV();
                    refreshButton.ClickRefreshButtonLV();
                    extentReports.CreateStepLogs("Info", "Double Click Refresh button in Timer ");

                    GetHoursTimer = int.Parse(refreshButton.GetHoursTimerLV());
                    Assert.AreEqual(2, GetHoursTimer, "Hours timer is working fine ", true);
                    extentReports.CreateStepLogs("Passed", "Clicking on Refresh button is not setting back the hours or minutes of timer");
                    
                    //Delete Time Entry Matrix
                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");

                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "User: " + userExl + " logged out");

                    //Select HL Banker app
                    try
                    {
                        lvHomePage.SelectAppLV("HL Banker");
                    }
                    catch(Exception)
                    {
                        lvHomePage.SelectAppLV1("HL Banker");
                    }
                }

                //TC - End
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "Admin User Logged Out from SF Lightning View. ");

                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                
                driver.Quit();
            }
        }

    }
}
