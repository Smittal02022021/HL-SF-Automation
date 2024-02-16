using Microsoft.Office.Interop.Excel;
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
    class LV_TMTT0038660_VerifyTheFunctionalityOfTimeClockRecorderForFVAStandardUserOnSFLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        HomeMainPage homePage = new HomeMainPage();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RateSheetManagementPage rateSheetMgt = new RateSheetManagementPage();
        RefreshButtonFunctionality refreshButton = new RefreshButtonFunctionality();
        TimeRecorderFunctionalities timeRecorder = new TimeRecorderFunctionalities();
        LVHomePage homePageLV = new LVHomePage();

        public static string fileTMTT0038660 = "LV_TMTT0038660_VerifyTheFunctionalityOfTimeClockRecorderForFVAStandardUserOnSFLightningView";
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
                extentReports.CreateLog(driver.Title + " is displayed ");
                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in                   
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                int rowCount = ReadExcelData.GetRowCount(excelPath, "Users");

                for (int row = 2; row <= rowCount; row++)
                {
                    //Login as Standard User and validate the user
                    string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", row, 1);
                    usersLogin.SearchUserAndLogin(userExl);
                    login.SwitchToClassicView();
                    string user = login.ValidateUser();
                    Assert.AreEqual(user.Contains(userExl), true);
                    extentReports.CreateStepLogs("Passed", "Standard User: " + userExl + " logged in ");
                    login.SwitchToLightningExperience();
                    extentReports.CreateLog("User: " + userExl + " Switched to Lightning View ");
                    homePageLV.ClickAppLauncher();
                    string appNameExl = ReadExcelData.ReadData(excelPath, "AppName", 1);
                    homePageLV.SelectApp(appNameExl);
                    string appName = homePageLV.GetAppName();
                    Assert.AreEqual(appNameExl, appName);
                    extentReports.CreateStepLogs("Passed", appName + " App is selected from App Launcher ");
                    string moduleNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ModuleName", 2, 1);
                    homePageLV.SelectModule(moduleNameExl);
                    extentReports.CreateStepLogs("Passed", "Module: : " + moduleNameExl + " is available for Logged-in user: " + user);

                    //TMTI0093775	Verify that the FVA user can access the Time Clock Recorder tab and can start the time clock
                   // TMTI0093778 Verify that the FVA Supervisor can access the Time Clock Recorder tab and can start the time clock.

                    //Click on Time Clock Recorder Tab
                    refreshButton.GotoTimeClockRecorderPageLV();

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

                    refreshButton.SelectDropDownProjectandActivityLV(projectExl, activityExl);
                    extentReports.CreateStepLogs("Passed", "Selected Project and Activity from Drop down");

                    bool btnStatus= refreshButton.GetButtonStatus("Start");
                    Assert.IsTrue(btnStatus);
                    extentReports.CreateStepLogs("Passed", "Intially Only Start is Enabled after Selecting Project and Activity from Drop down");
                    btnStatus= refreshButton.GetButtonStatus("Finish");
                    Assert.IsFalse(btnStatus);
                    extentReports.CreateStepLogs("Passed", "Finish Button is Disabled Before starting the Clock Recorder");
                                        
                    //Click Start Button
                    refreshButton.ClickStartButtonLV();
                    extentReports.CreateStepLogs("Passed", "User Clicked the Start button ");


                    btnStatus= refreshButton.GetButtonStatus("Finish");
                    Assert.IsTrue(btnStatus);
                    extentReports.CreateStepLogs("Passed", "Finish Button is Enabled After starting the Clock Recorder");


                    //Click Finish Button
                    refreshButton.ClickFinishButtonLV();
                    extentReports.CreateLog("Clicked the Finish button ");

                    //Go to Weekly Entry Matrix
                    timeEntry.GoToWeeklyEntryMatrixLV();
                    extentReports.CreateStepLogs("Info", "User is navigated to Weekly Entry Matrix ");

                    //Delete Time Entry Matrix
                    timeEntry.DeleteTimeEntryLV();
                    extentReports.CreateStepLogs("Passed", "Time Entry Deleted");


                    ////Go to Weekly Entry Matrix
                    //timeEntry.GoToWeeklyEntryMatrix();
                    //extentReports.CreateLog("User is navigated to Weekly Entry Matrix ");
                    //Thread.Sleep(3000);
                    ////Edit the Weekly Entry Matrix
                    //string weekDay = timeRecorder.EditWeeklyEntryMatrix();
                    //extentReports.CreateLog("User has edited the Weekly Entry Matrix: " + weekDay + " ");

                    ////Go to Details log
                    //timeRecorder.GoToDetailLogs();
                    //extentReports.CreateLog("User has navigated to details log ");

                    //string DetailLogTIme = timeRecorder.GetDetailLogsTimeHour();
                    //double DetailLogTIme2db = Convert.ToDouble(DetailLogTIme);

                    ////extentReports.CreateLog(ReadExcelData.ReadData(excelPath, "Update_Hours", 2).ToString());
                    //Assert.AreEqual(1, DetailLogTIme2db);
                    //extentReports.CreateLog("TIme displaying in detail log: " + DetailLogTIme2db + " Hours ");

                    ////Edit the details log
                    //timeRecorder.EditDetailsLog();
                    //extentReports.CreateLog("User has edited details log ");

                    ////Get hopurs from detail log
                    //timeRecorder.GoToDetailLogs();
                    //string DetailLogTIme1 = timeRecorder.GetDetailLogsTimeHour();
                    //double DetailLogTIme1db = Convert.ToDouble(DetailLogTIme1);
                    //Assert.AreEqual(2, DetailLogTIme1db);

                    //usersLogin.UserLogOut();

                    ////Login as Standard User and validate the user
                    //string valUser2 = ReadExcelData.ReadData(excelPath, "Users", 1);
                    //usersLogin.SearchUserAndLogin(valUser2);
                    //string stdUser2 = login.ValidateUser();
                    //Assert.AreEqual(stdUser2.Contains(valUser), true);
                    //extentReports.CreateLog("Standard User: " + stdUser2 + " is able to login ");

                    ////Click Time Record Manager Tab
                    //homePage.ClickTimeRecordManagerTab();

                    ////Edit the Weekly Entry Matrix
                    //string weekDay2 = timeRecorder.EditWeeklyEntryMatrix();
                    //extentReports.CreateLog("User has edited the Weekly Entry Matrix: " + weekDay2 + " ");

                    ////Go to Details log
                    //timeRecorder.GoToDetailLogs();
                    //extentReports.CreateLog("User has naigated to details log ");
                    ////Verify detail logged hours
                    //string DetailLogTIme3 = timeRecorder.GetDetailLogsTimeHour();
                    //Double DetailLogTIme3db = Convert.ToDouble(DetailLogTIme3);

                    //Assert.AreEqual(1, DetailLogTIme3db);
                    //extentReports.CreateLog("TIme displaying in detail log: " + DetailLogTIme3db + " Hours ");

                    ////Delete Time Entry Matrix
                    //timeEntry.DeleteTimeEntry();
                    //extentReports.CreateLog("User has deleted the record ");

                    ////TMTI0010009
                    //// Click on Time Clock Recorder Tab
                    //refreshButton.GotoTimeClockRecorderPage();
                    //extentReports.CreateLog("User has navigated to Time Clock Recorder Tab");

                    ////Check recorder is reset or not
                    //refreshButton.ClickResetButton();
                    //extentReports.CreateLog("User has clicked on 'Reset Button ");

                    ////Select Project and Activity from Drop Down
                    //refreshButton.SelectDropDownProjectandActivity(excelPath);
                    //extentReports.CreateLog("Selected Project and Activity from Drop down ");

                    ////Click Start Button
                    //refreshButton.ClickStartButton();
                    //extentReports.CreateLog("User has clicked on Start Button ");

                    ////Click Finish Button
                    //refreshButton.ClickFinishButton();
                    //extentReports.CreateLog("Clicked the finish button ");

                    ////Go to Summary Logs
                    //timeEntry.GoToSummaryLogs();
                    //extentReports.CreateLog("User has navigated to Summary logs ");

                    ////Get Summary Log Time Entry
                    //string summaryLogTime = timeRecorder.GetSummaryLogsTimeHour();
                    ////Assert.AreEqual("0.1", summaryLogTime);
                    //extentReports.CreateLog("Hours are round up to " + summaryLogTime + " even when only a few seconds has been recorded ");

                    ////Go to Weekly Entry Matrix
                    //timeEntry.GoToWeeklyEntryMatrix();
                    //extentReports.CreateLog("User is navigated to Weekly Entry Matrix ");

                    ////Delete Time Entry Matrix
                    //timeEntry.DeleteTimeEntry();
                    //extentReports.CreateLog("User has deleted the record ");

                    ////TMTI0010319
                    //// Click on Time Clock Recorder Tab
                    //refreshButton.GotoTimeClockRecorderPage();
                    //extentReports.CreateLog("User has navigated to Time Clock Recorder Tab ");

                    ////Check recorder is reset or not
                    //refreshButton.ClickResetButton();
                    //extentReports.CreateLog("User has clicked on 'Reset Button ");

                    ////Select Project and Activity from Drop Down
                    //refreshButton.SelectDropDownProjectandActivity(excelPath);
                    //extentReports.CreateLog("Selected Project and Activity from Drop down ");

                    ////Click Start Button
                    //refreshButton.ClickStartButton();
                    //extentReports.CreateLog("User has clicked on Start Button ");

                    ////Thread.Sleep(3000);
                    ////Click Finish Button
                    //refreshButton.ClickFinishButton();
                    //extentReports.CreateLog("Clicked the finish button ");

                    ////Go to Summary Logs
                    //timeEntry.GoToSummaryLogs();
                    //extentReports.CreateLog("User has navigated to Summary logs ");

                    ////Get Summary Log Time Entry
                    //string summaryLogTime1 = timeRecorder.GetSummaryLogsTimeHour();
                    //extentReports.CreateLog("Hours are round up to " + summaryLogTime1 + " even when only a few seconds has been recorded ");

                    usersLogin.ClickLogoutFromLightningView();
                    extentReports.CreateStepLogs("Info", "User: " + user + " logged out");                    
                }
                usersLogin.UserLogOut();
                driver.Quit();
                extentReports.CreateStepLogs("Info", "Browser Closed");
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                timeEntry.DeleteTimeEntryLV();
                login.SwitchToClassicView();
                usersLogin.UserLogOut();
                usersLogin.UserLogOut();
                driver.Quit();
            }
        }

    }
}
