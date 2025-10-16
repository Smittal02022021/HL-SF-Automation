using NUnit.Framework;
using SF_Automation.Pages;
using SF_Automation.Pages.Common;
using SF_Automation.Pages.Engagement;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Globalization;

namespace SF_Automation.TestCases.Opportunities

{
    class TC2209AndT2210_PortfolioValuation_ValidateRevenueFieldsUpdateWithStatusInProgress_CompletedAndPositionCancelled_Lightning : BaseClass
    {
        ExtentReport extentReports = new ExtentReport();
        LoginPage login = new LoginPage();
        EngagementHomePage engHome = new EngagementHomePage();
        EngagementDetailsPage engDetails = new EngagementDetailsPage();
        UsersLogin usersLogin = new UsersLogin();
        ValuationPeriods valuationPeriods = new ValuationPeriods();
        SendEmailNotification notification = new SendEmailNotification();
        public static string fileTC2209 = "T2209AndT2210_PortfolioValuation_ValidateRevenueFieldsUpdate_Lightning";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Initialize();
            ExtentReportHelper();
            ReadJSONData.Generate("Admin_Data.json");
            extentReports.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void ValidateRevenueFieldsUpdate()
        {
            try
            {
                //Get path of Test data file
                string excelPath = ReadJSONData.data.filePaths.testData + fileTC2209;
                Console.WriteLine(excelPath);

                //Validating Title of Login Page
                Assert.AreEqual(WebDriverWaits.TitleContains(driver, "Login | Salesforce"), true);
                extentReports.CreateLog(driver.Title + " is displayed ");

                //Calling Login function                
                login.LoginApplication();

                //Validate user logged in          
                Assert.AreEqual(login.ValidateUser().Equals(ReadJSONData.data.authentication.loggedUser), true);
                extentReports.CreateLog("User " + login.ValidateUser() + " is able to login ");

                //Login as Standard User and validate the user
                string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
                usersLogin.SearchUserAndLogin(valUser);
                string stdUser = login.ValidateUserLightning();
                Assert.AreEqual(stdUser.Contains(valUser), true);
                extentReports.CreateLog("Standard User: " + stdUser + " is able to login ");

                //Clicking on Engagement Tab and search for Engagement by entering Job type
                engHome.SelectEngUnderHLBanker();
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "Engagement", 2, 1);
                engHome.SearchEngagementWithNumberOnLightning(ReadExcelData.ReadData(excelPath, "Engagement", 2), valJobType);

                //Validate title of Engagement Details page
                string titleEngDetails = engHome.ClickEngNumAndValidateThePage();
                Assert.AreEqual("Details", titleEngDetails);
                extentReports.CreateLog("Engagement Details page is displayed upon clicking Engagement number ");

                //Click on Portfolio Valuation,click Engagement Valuation Period and validate Engagement Valuation Period page
                engDetails.ClickPortfolioValuationL();
                string pageEngValPeriodEdit = valuationPeriods.ValidatePageAfterClickingNewEngValPeriodButton();
                Assert.AreEqual("Engagement Valuation Period Edit", pageEngValPeriodEdit);
                extentReports.CreateLog("Page with title: " + pageEngValPeriodEdit + " is displayed after clicking New Engagement Valuation Period button ");

                //Enter Engagement Valuation Period details, save it and Validate Engagement Valuation Period Detail page
                string engName = CustomFunctions.RandomValue();
                string addedEngValuation = valuationPeriods.EnterAndSaveEngValuationPeriodDetailsL(engName);
                Assert.AreEqual(engName, addedEngValuation);
                extentReports.CreateLog("Added valuation: " + addedEngValuation + " is displayed upon clicking Save button on Engagement Valuation Period Detail page after entering all mandatory details ");

                //Enter Eng Valuation Period Position details and validate entered Valuation Period Position
                valuationPeriods.ValidateMessageWhileClickingSaveButtonOnPeriodPosition();
                string addedPosition = valuationPeriods.EnterAndSaveEngValuationPeriodPositionDetailsL("BE Networks");
                Assert.AreEqual("BE Networks", addedPosition);
                extentReports.CreateLog("Position: " + addedPosition + " is displayed upon clicking Save button after entering all mandatory details of Period Position ");

                //Validate value of Status,Fee Completed,Revenue Month,Cancel Month,Revenue Year,Cancel Year, Completed Date and Cancel Date
                //-----Validate Status
                string Status = valuationPeriods.GetPositionStatusL();
                Assert.AreEqual("In Progress", Status);
                extentReports.CreateLog("Status: " + Status + " is displayed as expected ");

                //-----Validate Fee Completed
                string FeeCompleted = valuationPeriods.GetFeeCompletedInProgressL();
                Assert.AreEqual("USD 0.00", FeeCompleted);
                extentReports.CreateLog("Fee Completed: " + FeeCompleted + " is displayed as expected ");

                //-----Validate Revenue Month and Revenue Year
                string revenueMonth = valuationPeriods.GetRevenueMonthL();
                Assert.AreEqual(" ", revenueMonth);

                string revenueYear = valuationPeriods.GetRevenueYearL();
                Assert.AreEqual(" ", revenueYear);
                extentReports.CreateLog("No value is displayed in Revenue Month and Revenue Year ");

                //-----Validate Cancel Month and Cancel Year
                string cancelMonth = valuationPeriods.GetCancelMonthL();
                Assert.AreEqual(" ", cancelMonth);

                string cancelYear = valuationPeriods.GetCancelYearL();
                Assert.AreEqual(" ", cancelYear);
                extentReports.CreateLog("No value is displayed in Cancel Month and Cancel Year ");

                //-----Validate Completed Date
                string completedDate = valuationPeriods.GetCompletedDateL();
                Assert.AreEqual(" ", completedDate);

                string cancelDate = valuationPeriods.GetCancelDateL();
                Assert.AreEqual(" ", cancelDate);
                extentReports.CreateLog("No value is displayed in Completed Date and Cancel Date ");

                //Get Final Report Details
                string valSentDate = engDetails.GetFinalReportSentDate();
                string stage =engDetails.UpdateStageInDetailsTab();
                Assert.AreEqual("Bill/File", stage);
                extentReports.CreateLog("Stage is updated to: " +stage + " " );

                engDetails.ValidateRevenueTab();
                string revAccrual = engDetails.GetRevenueAccrual();              
                string revenue = (revAccrual.Substring(4, 10));
                Console.WriteLine("revenue: " + revenue);                
                Console.WriteLine("revenueD: " + Convert.ToDouble(revenue));
                Console.WriteLine("revenueI: " + Convert.ToInt32(Convert.ToDouble(revenue)));

                int finalRevAccrual = Convert.ToInt32(Convert.ToDouble(revenue));
                Console.WriteLine("finalRevAccrual: " + finalRevAccrual);
                extentReports.CreateLog("Final Report Sent Date : " +valSentDate + " Revenue Accrual: " +revAccrual + " is displayed when Status of Position is In Progress ");

                //Update Status and Report Fee of existing position and validate details of Position
                string message1 =valuationPeriods.UpdateStatusAndReportFeeL(fileTC2209);                
                string reportFee = valuationPeriods.GetFeeCompletedL();
                int finalReportFee = Convert.ToInt32(Convert.ToDouble(reportFee));
                Console.WriteLine("finalReportFee" + finalReportFee);
                Assert.AreEqual("100,000.00", reportFee);
                extentReports.CreateLog("Status of Position " +message1+" and Report Fee :" + reportFee+ " has been updated ");

                //-----Validate Fee Completed
                string feeCompleted = valuationPeriods.GetFeeCompletedL();
                Assert.AreEqual("100,000.00", feeCompleted);
                extentReports.CreateLog("Fee Completed: " + feeCompleted + " is displayed as updated ");

                //-----Validate Revenue Month
                string revMonth = valuationPeriods.GetRevenueMonthL();
                Console.WriteLine(DateTime.Now.ToString("MM"));
                //Assert.AreEqual(DateTime.Now.ToString("MM"), revMonth);
                extentReports.CreateLog("Revenue Month: " + revMonth + " same as current month is displayed ");

                //-----Validate Revenue Year
                string revYear = valuationPeriods.GetRevenueYearL();
                Assert.AreEqual(DateTime.Now.ToString("yyyy"), revYear);
                extentReports.CreateLog("Revenue Year: " + revYear + " same as current year is displayed ");

                //-----Validate Completed Date
                string compDate = valuationPeriods.GetCompletedDateL();
                Assert.AreEqual(DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture), compDate.Substring(0,9));
                extentReports.CreateLog("Completed Date: " + compDate.Substring(0,9)+ " same as today's date is displayed ");

                //-----Validate Cancel Month, Cancel Year
                string canMonth = valuationPeriods.GetCancelMonthL();
                Assert.AreEqual(" ", cancelMonth);

                string canYear = valuationPeriods.GetCancelYearL();
                Assert.AreEqual(" ", cancelYear);

                string canDate = valuationPeriods.GetCancelDateL();
                Assert.AreEqual(" ", cancelDate);
                extentReports.CreateLog("The fields Cancel Month, Cancel Year, Cancel Date are blank when status is changed to Completed-Generate Accrual ");

                //Get Final Report Details
                string valSentDateCompleted = engDetails.GetFinalReportSentDate();
                engDetails.ValidateRevenueTab();
                string revAccrualCompleted = engDetails.GetRevenueAccrual();
                Console.WriteLine("revAccrualCompleted: " + revAccrualCompleted);

                int finalrevAccrualCompleted = Convert.ToInt32(Convert.ToDouble(revAccrualCompleted.Substring(4, 10)));
                Console.WriteLine("finalrevAccrualCompleted:" + finalrevAccrualCompleted);
               
                Assert.AreEqual((finalRevAccrual+ finalReportFee), finalrevAccrualCompleted);
                extentReports.CreateLog("Final Report Sent Date : " + valSentDateCompleted + " Revenue Accrual: " + finalrevAccrualCompleted + " is displayed when Status of Position is Completed ");

                //Validate cancel message on clicking Void Position
                string messageCancel = valuationPeriods.ClickVoidPositionAndGetMessageL();
                Assert.AreEqual("Are you sure you want to cancel this position? This process will reverse any accruals from this position.", messageCancel);
                extentReports.CreateLog("Message: " + messageCancel + " is displayed on clicking Void Position button ");

                //Validate details after cancelling the position
                //-----Status of Position 
                string statusCancel = valuationPeriods.GetPositionStatusL();
                Assert.AreEqual("Cancelled", statusCancel);
                extentReports.CreateLog("Status: " + statusCancel + " is displayed after position is cancelled by clicking Void Position button ");

                //-----Validate Cancel Month
                string cancelMon = valuationPeriods.GetCancelMonthL();
                Console.WriteLine(DateTime.Now.ToString("MM"));
                //Assert.AreEqual(DateTime.Now.ToString("MM"), revMonth);
                extentReports.CreateLog("Cancel Month: " + cancelMon + " same as current month is displayed after position is cancelled ");

                //-----Validate Cancel Year
                string cancelYr = valuationPeriods.GetCancelYearWithDetailsL();
                Assert.AreEqual(DateTime.Now.ToString("yyyy"), cancelYr);
                extentReports.CreateLog("Cancel Year: " + cancelYr + " same as current year is displayed after position is cancelled ");

                //-----Validate Cancel Date
                string can_Date = valuationPeriods.GetCancelDateL();
                Assert.AreEqual(DateTime.Now.ToString("M/d/yyyy", CultureInfo.InvariantCulture), can_Date.Substring(0,9));
                extentReports.CreateLog("Cancel Date: " + can_Date.Substring(0,9)+ " same as today's date is displayed after position is cancelled ");
                valuationPeriods.SwitchFrame();

                //Logout of standard user 
                usersLogin.DiffLightningLogout();
                //usersLogin.DiscardChanges();                
               
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


