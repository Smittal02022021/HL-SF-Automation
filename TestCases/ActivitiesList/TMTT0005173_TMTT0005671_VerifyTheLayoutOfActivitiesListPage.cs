using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.ActivitiesList;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Company;
using SF_Automation.Pages.Contact;
using SF_Automation.Pages.HomePage;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Threading;

namespace SF_Automation.TestCases.ActivitiesList
{
    class TMTT0005173_TMTT0005671_VerifyTheLayoutOfActivitiesListPage : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        HomeMainPage homePage = new HomeMainPage();
        UsersLogin usersLogin = new UsersLogin();
        ActivitiesListDetailPage activityList = new ActivitiesListDetailPage();
        ActivityDetailPage activityDetail = new ActivityDetailPage();

        public static string fileTMTT0005173 = "TMTT0005173_ActivitiesList_VerifyLayoutOfActivitiesList.xlsx";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void VerifyLayoutOfActivitiesList()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTMTT0005173;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateStepLogs("Passed", driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Handling salesforce Lightning
                //login.HandleSalesforceLightningPage();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateStepLogs("Passed", "User " + login.ValidateUser() + " is able to login ");
               
                // Search standard user by global search
                string user = ReadExcelData.ReadData(excelPath, "Users", 1);
                homePage.SearchUserByGlobalSearch(fileTMTT0005173, user);

                //Verify searched user
                string userPeople = homePage.GetPeopleOrUserName();
                string userPeopleExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(userPeopleExl, userPeople);
                extentReports.CreateStepLogs("Passed", "User " + userPeople + " details are displayed ");

                //Login as standard user
                usersLogin.LoginAsSelectedUser();
                string standardUser = login.ValidateUser();
                string standardUserExl = ReadExcelData.ReadData(excelPath, "Users", 1);
                Assert.AreEqual(standardUserExl.Contains(standardUser), true);
                extentReports.CreateStepLogs("Passed", "Standard User: " + standardUser + " is able to login ");

                //Click on activities list tab
                activityList.ClickActivitiesListTab();
                extentReports.CreateStepLogs("Info", "Activities List Tab is clicked ");

                string preSetTemplate = ReadExcelData.ReadData(excelPath, "ActivityList", 5);
                activityList.SelectFirstPresetTemplate(fileTMTT0005173, preSetTemplate);

                //Click refresh button
                activityList.ClickRefreshButton();
                extentReports.CreateStepLogs("Info", "Click refresh button ");

                //Verify alphabetical sorting of table details
                activityList.VerifyAlphabeticalSorting();
                extentReports.CreateStepLogs("Info", "Verify alphabetical sorting of table details");

                string valEventTaskType = activityList.getEventTaskType();
                string valEventTaskTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityList", 2, 1);
                extentReports.CreateStepLogs("Info", "valEventTaskType:" + valEventTaskType+ "valEventTaskTypeExl");

                Assert.AreEqual(valEventTaskTypeExl, valEventTaskType);
                extentReports.CreateStepLogs("Passed", "Event Task Type: " + valEventTaskType + " related activities are sorted ");
                
                //Verify alphabetical sorting of table details
                activityList.ClickAlphabetMforSorting();
                string valEventOrTaskType = activityList.getEventTaskType();
                string valEventOrTaskTypeExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityList", 3, 1);
                Assert.AreEqual(valEventOrTaskTypeExl, valEventOrTaskType);
                extentReports.CreateStepLogs("Passed", "Event Task Type: " + valEventOrTaskType + " related activities are sorted ");

                string activityTypeFromList = activityList.getEventTaskType();
                string activitySubjectFromList = activityList.getActivitySubject();
                string activityPrimaryAttendee = activityList.getPrimaryAttendee();
                string primaryExternalContact = activityList.getPrimaryExternalContact();

                //Verify view link
                activityList.ClickViewLink();

                //CustomFunctions.SwitchToWindow(driver,1);

                //Verify activity type from detail
                string activityTypeFromDetail = activityDetail.GetActivityTypeValue();
                Assert.AreEqual(activityTypeFromList, activityTypeFromDetail);
                extentReports.CreateStepLogs("Passed", "Event Task Type: " + activityTypeFromDetail + " in activity detail page matching with Event Task type in activity list ");

                //Verify activity subject from detail
                string activitySubjectFromDetail = activityDetail.GetActivitySubjectValue();
                Assert.AreEqual(activitySubjectFromList, activitySubjectFromDetail);
                extentReports.CreateStepLogs("Passed", "Activity subject: " + activitySubjectFromDetail + " in activity detail page matching with activity subject in activity list ");

                //Verify activity external attendees
                if (activityDetail.GetActivityExternalAttendees() != "Test External")
                {
                    activityList.enterExternalContact();
                }

                string activityExternalContact = activityDetail.GetActivityExternalAttendees();
                Assert.AreEqual(primaryExternalContact, activityExternalContact);
                extentReports.CreateStepLogs("Passed", "Activity External Contact: " + activityExternalContact + " in detail subject page matching with activity primary external contact in activity list ");

                //Verify activity HL Attendees
                string activityHLAttendee = activityDetail.GetActivityHLAttendees();
                Assert.AreEqual(activityPrimaryAttendee, activityHLAttendee);
                extentReports.CreateStepLogs("Passed", "Activity HL Attendee: " + activityHLAttendee + " in activity detail page matching with activity primary attendee in activity list ");

                //CustomFunctions.SwitchToWindow(driver, 0);
                activityList.ClickActivitiesListTab();
                bool newTaskBtn = activityList.NewTaskButtonAvailability();
                Assert.AreEqual(false, newTaskBtn);
                extentReports.CreateStepLogs("Passed", "New Task Button availability on page is: " + newTaskBtn + " ");

                bool EditLinkAction = activityList.EditActionLinkAvailability();
                Assert.AreEqual(false, EditLinkAction);
                extentReports.CreateStepLogs("Passed", "Edit link under action column availability on page is: " + EditLinkAction + " ");

                bool DeleteLinkAction = activityList.DeleteActionLinkAvailability();
                Assert.AreEqual(false, DeleteLinkAction);
                extentReports.CreateStepLogs("Passed", "Delete link under action column availability on page is: " + DeleteLinkAction + " ");

                //Click on print link
                activityList.ClickPrintLink();

                CustomFunctions.SwitchToWindow(driver, 1);
                string currentURL = driver.Url;
                string currentURLExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityList", 2, 2);
                Assert.IsTrue(currentURL.Contains(currentURLExl));
                extentReports.CreateStepLogs("Passed", "Upon click of print link user redirect to: " + currentURL+" ");

                CustomFunctions.SwitchToWindow(driver, 0);
                //Logout from standard user
                usersLogin.UserLogOut();
                
                //Click on activities list tab
                activityList.ClickActivitiesListTab();
                extentReports.CreateStepLogs("Info", "Activities List Tab is clicked ");
                driver.Navigate().Refresh();
                Thread.Sleep(3000);

                //Create New View
                activityList.CreateNewView();

                string getSelectedView = activityList.GetSelectedView();
                string getSelectedViewExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityList", 2, 3);
                Assert.AreEqual(getSelectedViewExl, getSelectedView);
                extentReports.CreateStepLogs("Passed", "Selected View: " + getSelectedView +" is new created view ");

                //Edit New View
                activityList.EditNewView();

                string getSelectedUpdatedView = activityList.GetSelectedView();
                string getSelectedUpdatedViewExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ActivityList", 2, 4);
                Assert.AreEqual(getSelectedUpdatedViewExl, getSelectedUpdatedView);
                extentReports.CreateStepLogs("Passed", "Selected View: " + getSelectedUpdatedView + " is new created view which is updated ");

                //Delete New View
                activityList.DeleteNewView();
                extentReports.CreateStepLogs("Info", "New created view is deleted ");

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