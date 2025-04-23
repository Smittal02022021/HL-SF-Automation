using SF_Automation.Pages.Common;
using SF_Automation.Pages.HomePage;
using SF_Automation.Pages.TimeRecordManager;
using SF_Automation.Pages;
using SF_Automation.UtilityFunctions;
using System;
using NUnit.Framework;
using SF_Automation.TestData;

namespace SF_Automation.TestCases.TimeRecordManager
{
    class LV_TMTT0005430_TimeRecorderFunctionalitiesLightningView:BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        UsersLogin usersLogin = new UsersLogin();
        TimeRecordManagerEntryPage timeEntry = new TimeRecordManagerEntryPage();
        RefreshButtonFunctionality refreshButton = new RefreshButtonFunctionality();
        LVHomePage homePageLV = new LVHomePage();
        RandomPages randomPages = new RandomPages();
        HomeMainPage homePage = new HomeMainPage();
        LVHomePage lvHomePage = new LVHomePage();

        public static string fileTMT5430 = "LV_TMTT0005430_TimeRecorderFunctionalities";
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyTimeRecorderFunctionalitiesLV()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMT5430;

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

                string userExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 1);
                string userGrpNameExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Users", 2, 2);

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
                refreshButton.GoToTimeClockRecorderPageLV();
                extentReports.CreateLog("User navigated to Time Clock Recorder ");
                randomPages.WaitForPageLoaderLV();

                //Validate Time Record Period Page Title
                string TimeClockRecorderPageTitle = refreshButton.GetTitleTimeClockRecorderPageLV();
                string TimeClockRecorderPageTitleExl = ReadExcelData.ReadDataMultipleRows(excelPath, "PageTitle", 2, 1);
                Assert.AreEqual(TimeClockRecorderPageTitleExl, TimeClockRecorderPageTitle);
                extentReports.CreateStepLogs("Passed", TimeClockRecorderPageTitle + " is displayed upon clicking Time Clock Recorder ");
                refreshButton.ClickResetButtonLV();

                // TMTI0010266 User should be able to add opportunity or engagement as a project
                //Select Project and Activity from Drop Down
                string projectExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ProjectTitle", 2, 1);
                string activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ProjectTitle", 2, 2);
                refreshButton.SelectDropDownProjectandActivityLV(projectExl, activityExl);
                extentReports.CreateStepLogs("Passed", "Selected Project: " + projectExl+" and Activity: "+ activityExl+" from Drop down");

                //TMTI0010006 User should be able to edit set time in Weekly Entry Matrix and Detail Logs tabs
                //Click Start Button
                refreshButton.ClickStartButtonLV();
                extentReports.CreateStepLogs("Info", "Clicked the 'Start Button ");
                //Add minutes to Timmer
                string additionalMinutes = ReadExcelData.ReadDataMultipleRows(excelPath, "Update_Timer", 2, 1);
                refreshButton.AddMinutesToTimerLV(additionalMinutes);
                extentReports.CreateStepLogs("Passed", "Additional " + additionalMinutes+ " Minutes are added after Starting the Clock");
                //Click Finish Button
                refreshButton.ClickFinishButtonLV();
                extentReports.CreateStepLogs("Info", "Clicked the 'Finish Button' ");
                //Go to Weekly Entry Matrix
                timeEntry.GoToWeeklyEntryMatrixLV();
                extentReports.CreateStepLogs("Info", "User navigated to Weekly Entry Matrix ");
                //Edit the Weekly Entry Matrix Recorded from Time Clock Recorder
                string updateHoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Update_Timer", 2, 2);
                timeEntry.SelectProjectWeeklyEntryMatrixLV(projectExl);
                string weekDay = timeEntry.LogCurrentDateHoursLV(updateHoursExl);//1
                extentReports.CreateStepLogs("Info", "User has edited the Weekly Entry Matrix time Recorder from Time Clock Recorder fo Day: " + weekDay + " ");
                //Go to Details log
                timeEntry.GoToDetailLogsLV();
                extentReports.CreateStepLogs("Info", "User navigated to Details Logs ");
                string DetailLogTime = timeEntry.GetDetailLogsTimeEntryLV();
                double DetailLogTime2db = Convert.ToDouble(DetailLogTime);
                Assert.AreEqual(Convert.ToDouble(updateHoursExl), DetailLogTime2db);
                extentReports.CreateStepLogs("Passed", "Time displaying in Detail Logs: " + DetailLogTime2db + " edited from Weekly Entry Matrix and is same as updated " + updateHoursExl);
                //Edit the details log
                updateHoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Update_Timer", 2, 3);
                activityExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Update_Timer", 2, 5);
                timeEntry.UpdateDetailLogsHoursLV(activityExl, updateHoursExl);
                extentReports.CreateStepLogs("Info", "User has edited Details Logs ");
                //Get hopurs from detail log
                timeEntry.GoToDetailLogsLV();
                DetailLogTime = timeEntry.GetDetailLogsTimeEntryLV();
                DetailLogTime2db = Convert.ToDouble(DetailLogTime);
                Assert.AreEqual(Convert.ToDouble(updateHoursExl), DetailLogTime2db);
                extentReports.CreateStepLogs("Passed", "Updated Time is saved and displaying on Detail Logs Hours: " + DetailLogTime2db);
                
                //TMTI0010130 User should be able to edit Hours Worked time
                //Click Time Record Manager Tab
                timeEntry.GoToWeeklyEntryMatrixLV();
                extentReports.CreateStepLogs("Info", "User navigated to Weekly Entry Matrix ");
                //Edit the Weekly Entry Matrix
                updateHoursExl = ReadExcelData.ReadDataMultipleRows(excelPath, "Update_Timer", 2, 4);
                weekDay = timeEntry.LogCurrentDateHoursLV(updateHoursExl);//1
                extentReports.CreateStepLogs("Info", "User has edited the existing hours on Weekly Entry Matrix for Day: " + weekDay);
                //Go to Details log
                timeEntry.GoToDetailLogsLV();
                timeEntry.GoToWeeklyEntryMatrixLV();
                timeEntry.GoToDetailLogsLV();
                extentReports.CreateStepLogs("Info", "User navigated to Detail Logs ");
                //Verify detail logged hours
                DetailLogTime = timeEntry.GetDetailLogsTimeEntryLV();
                DetailLogTime2db = Convert.ToDouble(DetailLogTime);
                Assert.AreEqual(Convert.ToDouble(updateHoursExl), DetailLogTime2db);
                extentReports.CreateStepLogs("Passed", "Time updated on Weekly Entry Matrix is saved and displaying on Detail Logs Hours: " + DetailLogTime2db);
                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntryLV();
                extentReports.CreateStepLogs("Info", "User has deleted the record ");

                //TMTI0010009 Hours should round up to 0.1 even when only a few seconds has been recorded
                //Click on Time Clock Recorder Tab
                refreshButton.GoToTimeClockRecorderPageLV();
                extentReports.CreateStepLogs("Info", "User navigated to Time Clock Recorder");
                refreshButton.ClickResetButtonLV();
                //Select Project and Activity from Drop Down
                refreshButton.SelectDropDownProjectandActivityLV(projectExl, activityExl);
                extentReports.CreateStepLogs("Info", "Selected Project: " + projectExl + " and Activity: " + activityExl + " from Drop down");
                //Click Start Button
                refreshButton.ClickStartButtonLV();
                extentReports.CreateStepLogs("Info", "Clicked the 'Start Button ");

                //TMTI0010319 User should be able to submit time when clicking finish
                //Click Finish Button
                refreshButton.ClickFinishButtonLV();
                extentReports.CreateStepLogs("Info", "Clicked the 'Finished Button ");
                //Go to Summary Logs
                timeEntry.GoToSummaryLogLV();
                extentReports.CreateStepLogs("Info", "User navigated to Summary logs ");
                //Get Summary Log Time Entry
                string summaryLogTime = timeEntry.GetSummaryLogsTimeEntryLV();
                Assert.AreNotEqual("0", summaryLogTime);
                extentReports.CreateStepLogs("Passed", "Hours are round up to " + summaryLogTime + " even when only a few seconds has been recorded from Time Clock Recorder");
                //Delete Time Entry Matrix
                timeEntry.DeleteTimeEntryLV();
                extentReports.CreateLog("User has deleted the record ");

                usersLogin.ClickLogoutFromLightningView();
                extentReports.CreateStepLogs("Info", "User: " + userExl + " logged out");
                
                //TC - End
                lvHomePage.LogoutFromSFLightningAsApprover();
                extentReports.CreateStepLogs("Info", "Admin User Logged Out from SF Lightning View. ");

                driver.Quit();
            }
            catch (Exception e)
            {
                extentReports.CreateExceptionLog(e.Message);
                driver.Quit();
            }
        }
    }
}
   
