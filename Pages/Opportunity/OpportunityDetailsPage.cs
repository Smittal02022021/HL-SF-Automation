using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using RazorEngine.Compilation.ImpromptuInterface.Optimization;
using SF_Automation.TestCases.Contact;
using SF_Automation.TestData;
using SF_Automation.UtilityFunctions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.PeerToPeer;
using System.Text.RegularExpressions;
using System.Threading;

namespace SF_Automation.Pages
{
    class OpportunityDetailsPage : BaseClass
    {
        By btnMore = By.XPath("(//button[@title='More Tabs'])[3]");
        By btnDeleteActivity = By.XPath("//button[@title='Delete']");

        By imgCoverageSectorDependencyLookUp = By.XPath("//img[@alt='Coverage Sector Dependency Lookup (New Window)']");
        By txtSearchBox = By.XPath("//input[@title='Go!']/preceding::input[1]");
        By btnGo = By.XPath("//input[@type='submit']");
        By buttonViewCounterparty = By.XPath("//button[contains(text(),'View Counterparties')]");
        By btnEdit = By.CssSelector("input[value=' Edit ']");
        By valOpportunity = By.XPath("//div[contains(@id,'Name')]");
        By btnDNDOnOff = By.CssSelector("input[name='dnd_on_off']");
        By imgLock = By.CssSelector("img[title='Locked']");
        By titleMessage = By.XPath("//span[contains(text(),'Record Locked')]");
        By txtMessage = By.XPath("//*/tr[2]/td");
        By linkHere = By.CssSelector("#bodyCell > table > tbody > tr:nth-child(2) > td > a");
        By tblApprovalHistory = By.CssSelector("div[class='relatedProcessHistory']");
        //By txtStatus = By.CssSelector("div.extraStatusDiv_P>span");
        By txtStatus = By.CssSelector("div[class*='extraStatusDiv']>span");
        By txtApprover = By.CssSelector("[id*='RelatedProcessHistoryList_body'] > table > tbody > tr:nth-child(3) > td:nth-child(5) > a");
        By linkApproveReject = By.CssSelector("[id*='RelatedProcessHistoryList_body']>table>tbody> tr:nth-child(3) > td.actionColumn > a:nth-child(2)");
        By txtComments = By.Id("Comments");
        By btnReject = By.CssSelector("input[title='Reject']");
        By btnApprove = By.CssSelector("input[title='Approve']");
        By txtOpportunityName = By.CssSelector("div[id*='Namej_id0_j_id55']");
        By btnRequestEng = By.CssSelector("input[title='Request Engagement']");
        By msgSuccess = By.CssSelector("div[class='messageText']");
        By valClient = By.CssSelector("div[id*='CF00Ni000000D7zoCj']");
        By valClientOwnership = By.CssSelector("div[id*='00N5A00000M0d2Tj']");
        By valSubject = By.CssSelector("div[id*='CF00Ni000000D80OZj']");
        By valSubjectOwnership = By.CssSelector("div[id*='00N5A00000M0d2Uj']");
        By drpdownWomenLed = By.CssSelector("select[name='00N6e00000MRNgW']");
        By valIG = By.CssSelector("div[id*='00Ni000000D8VT3j']");
        By valJobType = By.CssSelector("div[id*='00Ni000000D8hWWj']");
        By btnNBCForm = By.CssSelector("input[name='nbc_form']");
        By btnNBCFormL = By.CssSelector("input[name='nbc_form_c']");
        By btnNBCFormType = By.CssSelector("input[name='nbc_form_cr']");
        By btnMA = By.XPath("//fieldset/table/tbody/tr/td[1]/label");
        By btnCapMkt = By.XPath("//fieldset/table/tbody/tr/td[2]/label");
        By titleNBCForm = By.CssSelector(" div.pbBody > table > tbody > tr > td.instructions > p:nth-child(1)");
        By linkRequestDate = By.CssSelector("div:nth-child(23) > table > tbody > tr:nth-child(3) > td:nth-child(4) > span > span > a");
        By linkPitchDate = By.XPath("//div[3]/table/tbody/tr[6]/td[4]/span/span/a");
        By btnSave = By.CssSelector("input[name='save']");
        By chkInternalTeamPrompt = By.CssSelector("input[name*='FnLTz']");
        By chkInitiator = By.CssSelector("input[name*='internalTeam:j_id73:0:j_id75']");
        By chkPrincipal = By.CssSelector("input[name*='internalTeam:j_id73:2:j_id75']");
        By chkManager = By.CssSelector("input[name*='internalTeam:j_id73:3:j_id75']");
        By btnSaveITTeam = By.XPath("//div[3]/table/tbody/tr/td[2]/span/input[1]");
        By listStaff = By.XPath("/html/body/ul");
        By btnReturnToOpp = By.CssSelector("input[value*='Return To Opportunity']");
        By tabAfterReturnToOpp = By.XPath("//a[text()='Details']");
        By btnConverttoEng = By.CssSelector("input[value='Convert to Engagement']");
        By chkConvertedtoEng = By.CssSelector("input[name*='FaP8F']");
        By comboJobType = By.CssSelector("select[id*= 'hWW']");
        By txtStaff = By.CssSelector("input[placeholder*='Begin Typing Name']");
        By chkUpPrincipal1 = By.CssSelector("input[name*=':2:j_id44']");
        By chkUpSeller1 = By.CssSelector("input[name*=':1:j_id44']");
        By chkUpManager1 = By.CssSelector("input[name*=':3:j_id44']");
        By chkAdmin1 = By.CssSelector("input[name*='9:j_id65']");
        By chkUpPrincipal = By.CssSelector("input[name*=':3:j_id44']"); //43
        By chkUpSeller = By.CssSelector("input[name*=':2:j_id44']");
        By chkUpManager = By.CssSelector("input[name*=':4:j_id44']");//43
        By chkAdmin = By.CssSelector("input[name*='j_id73:9:j_id75']");
        By chkAdmin2 = By.CssSelector("input[name *= 'j_id64:9:j_id66']");
        By btnFEIS = By.Name("feis_and_fairness_forms");
        By linkRequestDateFAS = By.CssSelector("div.pbBody > div:nth-child(23) > table > tbody > tr:nth-child(2) > td:nth-child(4) > span > span > a");
        By btnCounterparties = By.CssSelector(".pbButton > input[title = 'Counterparties']");
        By btnPortfolioValuation = By.CssSelector("input[title='Portfolio Valuation']");
        By lblValuationPeriods = By.CssSelector("div[class='pbBody']> font > b");
        By valStage = By.CssSelector("div[id*='OA']");
        By valPitchDate = By.CssSelector("div[id*='PDj']");
        By valWinProb = By.CssSelector("div[id*='OKj']");
        By valTxnSize = By.CssSelector("div[id*='80P4j']");
        By valRetainer = By.CssSelector("div[id*='DwTdFj']");
        By valMonthlyFee = By.CssSelector("td[id*='00Ni000000FmBzij']>div[id*='FmBzij']");
        By valContingentFee = By.CssSelector("div[id*='GE9j']");
        By rowOppComments = By.CssSelector("div[id*='00Ni000000FnLT7_body']>table>tbody>tr");
        By msgOppComments = By.CssSelector("div[id*='00Ni000000FnLT7_body']>table>tbody>tr>th");
        By valOppComments = By.CssSelector("div[id*='00Ni000000FnLT7_body']>table>tbody>tr[class*='first']>td:nth-child(3)");
        By linkDel = By.CssSelector("td[class='actionColumn']>a[title='Delete - Record 1 - View']");
        By btnClone = By.CssSelector("input[title='Clone']");
        //By chkUpMgr = By.CssSelector("input[name*='4:j_id47']");
        By chkUpAssociate1 = By.CssSelector("input[name*=':4:j_id44']");
        By chkUpAnalyst1 = By.CssSelector("input[name*=':5:j_id44']");
        By lnkReDisplayRec = By.CssSelector("table > tbody > tr:nth-child(2) > td > a:nth-child(4)");
        By chkUpMgr = By.CssSelector("input[name*='1:j_id44']");//43
        By chkUpAssociate = By.CssSelector("input[name*=':5:j_id44']");//43
        By chkUpAnalyst = By.CssSelector("input[name*=':6:j_id44']");  //43    
        By rowUser = By.XPath("//html/body/span[2]/form/div[1]/div/div/div/div[2]/table/tbody/tr/td[1]/div/label");
        By chkCheckedAdmin = By.CssSelector("input[name*='1:j_id45:9:j_id47']");
        By chkCheckedInitiator = By.CssSelector("input[name*='0:j_id42:0:j_id44']");
        By msgHLIntTeam = By.CssSelector("div[id*='pgfrmId:internalTeam:j']");
        By lnkRecordTypeChange = By.CssSelector("div[id*='RecordTypej_id0_j_id55_ileinner'] > a");
        By comboRecType = By.CssSelector("select[id*='p3']");
        By btnContinue = By.CssSelector("input[value='Continue']");
        By comboLOB = By.CssSelector("select[id*='hW2']");
        By valLOB = By.CssSelector("div[id*='W2j_id0_j_id55_ileinner']");
        By txtMonthlyFee = By.CssSelector("input[name*='FmBzi']");
        By lnkTrialExp = By.CssSelector("div:nth-child(17) > table > tbody > tr:nth-child(2) > td.dataCol.col02 > span > span > a");
        By txtContingentFee = By.CssSelector("input[name*='FkGE9']");
        By lnkEstClosedDate = By.CssSelector("div:nth-child(25) > table > tbody > tr:nth-child(3) > td.dataCol.col02 > span > span > a");
        By comboWomenLed = By.CssSelector("select[name*='RNgW']");
        By comboFairnessOpinion = By.CssSelector("select[id*='GbaZ7']");
        By valRegEng = By.CssSelector("span[id*='id45']>ul");
        By txtSICCode = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000G9CKu']");
        By txtOppDesc = By.CssSelector("textarea[name*='D80Oy']");
        By txtMarketCap = By.CssSelector("input[name*='D80P4']");
        By txtRetainer = By.CssSelector("input[name*='TdF']");
        By txtReferralContact = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000D80Oo']");
        By comboConfAgreement = By.CssSelector("select[name*='D8war']");
        By lnkOutcomeDate = By.CssSelector("div:nth-child(23) > table > tbody > tr:nth-child(5) > td:nth-child(4) > span > span > a");
        By comboOutcome = By.CssSelector("select[name*='D8hIa']");
        By lnkDateEngagedCF = By.CssSelector("div:nth-child(25) > table > tbody > tr:nth-child(2) > td:nth-child(4) > span > span > a");
        By checkNBCApproved = By.CssSelector("input[name='00Ni000000FmBzh']");
        By txtClient = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000D7zoC']");
        By txtSubject = By.XPath("//span[@class='lookupInput']/input[@name='CF00Ni000000D80OZ']");
        By valEstFin = By.CssSelector("div[id*='id35']");
        By valEstFinCF = By.CssSelector("div[class='message errorM3']");
        By comboTombstonePermission = By.CssSelector("select[name='00N5A00000GzTIZ']");
        By txtFee = By.CssSelector("input[name*='FmBzg']");
        By lnkDateEngaged = By.CssSelector("div:nth-child(3) > table > tbody > tr:nth-child(10) > td:nth-child(4) > span > span > a");
        By checkFEISApproved = By.CssSelector("input[name='00Ni000000FmBzh']");
        By lnkOutcomeDateFAS = By.CssSelector("div:nth-child(25) > table > tbody > tr:nth-child(5) > td:nth-child(4) > span > span > a");//23
        By comboRecordType = By.CssSelector("select[name='00Ni000000D8hW2']");
        By valRecType = By.CssSelector("div[id*='RecordTypej']");
        By valOppNum = By.CssSelector("div[id*='VbIj_id0_j_id55_ileinner']");
        By btnNewComment = By.CssSelector("input[value='New Opportunity Comment']");// input[value='New Comment']");
        By btnNewComment1 = By.CssSelector("input[value=' New ']");
        By comboCommentType = By.CssSelector("select[id*='00Ni000000FnLSo']");
        By txtCommentDesc = By.CssSelector("textarea[id*='FnLSp']");
        By msgError = By.CssSelector("div[class='errorMsg']");
        By linkCoverageSectorDependencyName = By.XPath("//a[@href='#']");
        By valComment = By.XPath("//table[@class='detailList']//tr//td[text()='Comment']/../td/div");// div[id*='LT7_body'] > table > tbody > tr.dataRow.even.last.first > td:nth-child(3)");
        By lnkDelComment = By.CssSelector("div[id*='LT7_body']> table > tbody > tr.dataRow.even.last.first > td.actionColumn > a:nth-child(2)");
        By btnDelComment = By.XPath("(//table//input[@value='Delete'])[1]");
        By txtDateEngagedCF = By.CssSelector("input[name*='FnLTv']");
        By lnkPitchDateFAS = By.CssSelector("div:nth-child(3) > table > tbody > tr:nth-child(7) > td:nth-child(4) > span > span > a");
        By lnkValuationDate = By.CssSelector("div:nth-child(3) > table > tbody > tr:nth-child(8) > td:nth-child(4) > span > span > a");
        By lnkDateCASignedFAS = By.CssSelector("div:nth-child(23) > table > tbody > tr:nth-child(1) > td:nth-child(4) > span > span > a");//21
        By lnkDateCAExpiresFAS = By.CssSelector("div:nth-child(23) > table > tbody > tr:nth-child(2) > td:nth-child(4) > span > span > a");//21
        By lnkPitchDateFR = By.CssSelector("div:nth-child(3) > table > tbody > tr:nth-child(6) > td:nth-child(4) > span > span > a");
        By chkDebtConfirmed = By.CssSelector("input[name*='4yQC']");
        By txtTotalDebtHL = By.CssSelector("input[name*='4yQD']");
        By txtClientDesc = By.CssSelector("textarea[name*='4yQ7']");
        By comboLegalAdvisorComp = By.CssSelector("span>select[name*='4yQA']");
        By comboLegalAdvisorHL = By.CssSelector("span>select[name*='4yQB']");
        By comboEUSecurities = By.CssSelector("span>select[name*='4yQ8']");
        By lnkDateCASignedFR = By.CssSelector("div:nth-child(17) > table > tbody > tr:nth-child(1) > td:nth-child(4) > span > span > a");
        By lnkDateCAExpiresFR = By.CssSelector("div:nth-child(17) > table > tbody > tr:nth-child(2) > td:nth-child(4) > span > span > a");
        By lnkEstimatedClosedDateFR = By.CssSelector("div:nth-child(21) > table > tbody > tr:nth-child(3) > td:nth-child(4) > span > span > a");
        By lnkOutcomeDateFR = By.CssSelector("div:nth-child(19) > table > tbody > tr:nth-child(5) > td:nth-child(4) > span > span > a");
        By chk2ndInitiator = By.CssSelector("input[name*=':0:j_id66']");
        By valHLEntity = By.CssSelector("div[id*='WRj']");
        By valERPHLEntity = By.CssSelector("div[id*='fYj']");
        By valERPLegalEntityId = By.CssSelector("div[id*='fhj']");
        By valERPSubmittedToSync = By.CssSelector("div[id*='ftj']");
        By valERPProjectNumber = By.CssSelector("div[id*='fpj']");
        By valERPProjectName = By.CssSelector("div[id*='foj']");
        By valERPLOB = By.CssSelector("div[id*='fbj']");
        By valERPIG = By.CssSelector("div[id*='faj']");
        By valERPTemplate = By.CssSelector("div[id*='fxj']");
        By valERPUnitID = By.CssSelector("div[id*='fVj']");
        By valERPUnit = By.CssSelector("div[id*='fWj']");
        By valERPLegalEntityID = By.CssSelector("div[id*='fgj']");
        By valERPEntityCode = By.CssSelector("div[id*='fXj']");
        By valERPLegCode = By.CssSelector("div[id*='fij']");
        //By valIG = By.CssSelector("div[id*='T3j']");
        By valERPID = By.CssSelector("div[id*='fZj']");
        By valERPProjStatusCode = By.CssSelector("div[id*='frj']");
        By valERPLastIntStatus = By.CssSelector("div[id*='ffj']");// eeCj
        By valERPResponseDate = By.CssSelector("div[id*='efej']");
        By valERPError = By.CssSelector("div[id*='fdj']");
        By valERPProductType = By.CssSelector("div[id*='g8j']");
        By valERPProductTypeCode = By.CssSelector("div[id*='fkj']");
        By valERPEmailID = By.CssSelector("div[id*='fjj']");
        By checkERPUpdateDFF = By.CssSelector("div[id*='efzj']>img");
        By comboPrimaryOffice = By.CssSelector("select[id*='VIA']");
        By valPrimaryOffice = By.CssSelector("div[id*='VIA']");
        By comboIG = By.CssSelector("select[id*='VT3']");
        By valIGCF = By.CssSelector("div[id*='VT3']");
        By comboSector = By.CssSelector("select[id*='PI']");
        By valSector = By.CssSelector("div[id*='PI']");
        By comboClientOwnership = By.CssSelector("select[id*='d2T']");
        //By valClientOwnership = By.CssSelector("div[id*='d2Tj']");
        By comboSubjectOwnership = By.CssSelector("select[id*='d2U']");
        //By valSubjectOwnership = By.CssSelector("div[id*='2Uj']");
        By lnkSyncDate = By.CssSelector("table > tbody > tr:nth-child(1) > td.dataCol.col02 > span > span > a");
        By btnNewContract = By.CssSelector("input[value='New Contract']");
        By titlePage = By.CssSelector("h2[class='pageDescription']");
        By txtContractName = By.CssSelector("input[id*='Name']");
        By txtBillingContact = By.CssSelector("span>input[id*='CF00N5A00000M0ebh']");
        By lnkOpportunity = By.CssSelector("a[id*='A00000M0ed1']");
        By checkIsMain = By.CssSelector("div[id*='ed1_body']> table > tbody > tr.dataRow.even.last.first > td.dataCell.booleanColumn > img");
        By lnkEditContract = By.CssSelector("div[id*='ed1_body'] > table > tbody > tr.dataRow.even.last.first > td.actionColumn > a:nth-child(1)");
        By titleEditContract = By.CssSelector("h1[class='pageType']");
        By checkSelectedIsMain = By.CssSelector("input[name*='ZS']");
        By checkIsMainContract1 = By.CssSelector("div[id*='ed1_body']> table > tbody > tr.dataRow.even.first > td.dataCell.booleanColumn > img");
        By checkIsMainContract2 = By.CssSelector("div[id*='ed1_body']> table > tbody > tr.dataRow.odd.last > td.dataCell.booleanColumn > img");
        By valContract1 = By.CssSelector("div[id*='ed1_body'] > table > tbody > tr:nth-child(2) > th > a");
        By valContract2 = By.CssSelector("div[id*='ed1_body'] > table > tbody > tr:nth-child(3) > th > a");
        By txtClientContract = By.CssSelector("span>input[id*='CF00N5A00000M0ebj']");
        By btnModifyRoles = By.CssSelector("td[id*='j_id0:j_id1:j_id2:j_id3:pbHLInternalTeam:j_id4:bottom']>a");
        By checkInitiator = By.CssSelector("input[name*='internalTeam:j_id64:0:j_id66']");
        By checkSeller = By.CssSelector("input[name*='internalTeam:j_id64:1:j_id66']");
        By checkPrincipal = By.CssSelector("input[name*='internalTeam:j_id64:2:j_id66']");
        By checkManager = By.CssSelector("input[name*='internalTeam:j_id64:3:j_id66']");
        By checkAssociate = By.CssSelector("input[name*='internalTeam:j_id64:4:j_id66']");
        By checkAnalyst = By.CssSelector("input[name*='internalTeam:j_id64:5:j_id66']");
        By checkSpeciality = By.CssSelector("input[name*='internalTeam:j_id64:6:j_id66']");//7
        By checkPE = By.CssSelector("input[name*='internalTeam:j_id64:7:j_id66']");
        By checkPublic = By.CssSelector("input[name*='internalTeam:j_id64:8:j_id66']");
        By checkAdmin = By.CssSelector("input[name*='internalTeam:j_id64:9:j_id66']");
        By checkRMS = By.CssSelector("input[name*='internalTeam:j_id64:10:j_id66']");
        By checkExpenseOnly = By.CssSelector("input[name*='internalTeam:j_id64:11:j_id66']");
        By checkNonRegistered = By.CssSelector("input[name*='internalTeam:j_id64:12:j_id66']");
        By btnSaveDealTeam = By.CssSelector("input[value='Save']");
        By valAddedMember = By.XPath("//div[2]/span[2]/table/tbody/tr[1]/td[1]");

        By lnkEngagement = By.CssSelector("div[id*='zAz_body']>table>tbody>tr.dataRow.even.last.first>th>a");
        By txtAnticipatedRevenue = By.CssSelector("input[name*='zNU']");
        By valDefaultClient = By.CssSelector("div[id*='DuhQp_body'] > table > tbody > tr:nth-child(2)>th>a");
        By txtClientSubject = By.CssSelector("span>input[id*='CF00Ni000000D9DcG']");
        By valNewClient = By.CssSelector("div[id*='p_body'] > table > tbody > tr:nth-child(5)> th > a");
        By valClientType = By.CssSelector("div[id*='uhQp_body'] > table > tbody > tr:nth-child(5)>td:nth-child(3)");
        By lnkEditClient = By.CssSelector("div[id*='hQp_body'] > table > tbody > tr.dataRow.even.last > td.actionColumn > a:nth-child(1)");
        By comboType = By.CssSelector("select[name*='D9DcL']");
        By lnkDelClient = By.CssSelector("div[id*='DuhQp_body'] > table > tbody > tr.dataRow.even.last > td.actionColumn > a:nth-child(2)");
        By btnAdditionalClient = By.CssSelector("input[value*='Additional Client']");
        By titleAdditionalClient = By.CssSelector("h2[class='pageDescription']");
        By comboTypes = By.XPath("//select[@name='00Ni000000D9DcL']/option");
        By valType = By.CssSelector("div:nth-child(3)>table>tbody>tr:nth-child(2)>td:nth-child(2)>span>select>option:nth-child(2)");
        By valPrimary = By.CssSelector("div[id*='DuhQp_body']>table > tbody > tr.dataRow.even.last > td.dataCell.booleanColumn > img");
        By btnCancel = By.CssSelector("input[value='Cancel']");
        By titleOpp = By.CssSelector("h2[class='mainTitle']");
        By valOppClientSubject = By.CssSelector("span>input[id*='CF00Ni000000DuhQp']");
        By checkPrimary = By.CssSelector("input[name*='Dca']");
        By rowAdditional = By.CssSelector("div[id*='DuhQp_body']>table>tbody>tr:nth-child(4)");
        By valPrimaryClient = By.CssSelector("div[id*='DuhQp_body']>table > tbody > tr.dataRow.even.first > td.dataCell.booleanColumn > img");
        By valPrimarySubject = By.CssSelector("div[id*='DuhQp_body']>table > tbody > tr.dataRow.odd.last > td.dataCell.booleanColumn > img");
        By valCompFeeAttrParty = By.CssSelector("div[id*='DuhQp_body']>table > tbody >tr:nth-child(6)>th>a");
        By valCompKeyCreditor = By.CssSelector("div[id*='DuhQp_body']>table > tbody >tr:nth-child(3)>th>a");
        By valTypeFeeAttrParty = By.CssSelector("div[id*='DuhQp_body']>table > tbody >tr:nth-child(6)>td:nth-child(3)");
        By valTypeKeyCreditor = By.CssSelector("div[id*='DuhQp_body']>table > tbody >tr:nth-child(3)>td:nth-child(3)");
        By valRecTypeFeeAttrParty = By.CssSelector("div[id*='DuhQp_body']>table > tbody >tr:nth-child(6)>td:nth-child(4)");
        By valRecTypeKeyCreditor = By.CssSelector("div[id*='DuhQp_body']>table > tbody >tr:nth-child(3)>td:nth-child(4)");
        By comboAdditionalClient = By.CssSelector("select[name*='FmBza']");
        By comboAdditionalSubject = By.CssSelector("select[name*='FmBzb']");
        By valWomenLed = By.CssSelector("div[id*='NgWj_id0_j_id55_ileinner']");
        By txtWomenLed = By.CssSelector("div:nth-child(27)>table>tbody>tr:nth-child(4)>td:nth-child(3)");
        By txtWomenLedFVA = By.CssSelector("div:nth-child(29)>table>tbody>tr:nth-child(3)>td:nth-child(3)");
        By txtWomenLedFR = By.CssSelector("div:nth-child(23)>table>tbody>tr:nth-child(4)>td:nth-child(3)");
        By btnAdditionalClientSubject = By.CssSelector("input[value*='New Opportunity Client/Subject']");
        By btnMassEditRecords = By.CssSelector("input[value*='Mass Edit Records']");
        By titleMassEditPage = By.XPath("//span[@class='slds-text-heading_small slds-truncate']");
        By btnBackToOpp = By.XPath("//div[1]/span/lightning-button/button");
        By titleOppDetails = By.CssSelector("div[id*='j_id55'] > div.pbHeader > table > tbody > tr > td.pbTitle > h2");
        By btnAdditionalClientSub = By.XPath("//div[2]/span/lightning-button/button");
        By btnEditMassEdit = By.XPath("//header/div[2]/slot/lightning-button/button");
        By txtRefresh = By.XPath("//div[2]/div[2]/span/p");
        By comboTypeMassEdit = By.XPath("//lightning-base-combobox-item[contains(@id,'input-16')]");
        By btnDeleteRecords = By.XPath("//div[3]/span/lightning-button/button");
        By txtAlertMessage = By.XPath("//slot/div/div/h2");
        By btnClose = By.XPath("//div/div/div/lightning-button-icon/button");
        By checkRecord = By.XPath("//tr[2]/td[1]/div/lightning-input/div/span/label/span[1]");
        By btnYes = By.XPath("//footer/lightning-button[1]/button");
        By txtType = By.XPath("//tr[2]/td[4]/div/lightning-formatted-text");
        By lnkShowMore = By.CssSelector("div[id*='DuhQp_body'] > div > a:nth-child(1)");
        By valTotalDebtCurrency = By.XPath("//div[7]/table/tbody/tr[5]/td[2]");
        By valTotalDebtMM = By.XPath("//div[3]/div[2]/div[7]/table/tbody/tr[4]/td[2]");
        By txtDefaultTab = By.XPath("//lightning-tab-bar/ul/li[@title='Public Sensitivity']");
        By txtDefaultTabCNBC = By.XPath("//lightning-tab-bar/ul/li[@class='slds-tabs_default__item slds-is-active']/a[@aria-controls='tab-1']");
        By chkNBCApproved = By.CssSelector("img[id*='FmBzhj_id0_j_id55_chkbox']");
        By titlePopUpNBC = By.XPath("//div[@class='custPopup']/p");
        By btnReturnToOppL = By.XPath("//div[1]/table/tbody/tr/td[2]/span/input[2]");
        By txtEstTxnSize = By.CssSelector("input[name*='P4']");
        By valEstTxnSize = By.CssSelector("div[id*='P4']");
        By txtRequestDate = By.CssSelector("input[id*='yN']");
        By valRequestDate = By.CssSelector("div[id*='yNj']");
        By txtEstimatedFees = By.XPath("//*[@id='00N6e00000H0zNU']");
        By shwAllTab = By.CssSelector("li[id='AllTab_Tab'] > a > img");
        By imgCoverageSectorDependencies = By.CssSelector("img[alt = 'Coverage Sector Dependencies']");

        //Lightning

        By btnRecentlyViewed = By.XPath("//div/div/div[2]/div/button");
        By valRec1st = By.XPath("//table/tbody/tr[1]/th/span/a");
        By tabOpp = By.XPath("//span[text()='Opportunities']");
        By btnEditL = By.XPath("//li[contains(@data-target-selection-name,'Opportunity__c.Edit')]"); //records-lwc-highlights-panel/records-lwc-record-layout//records-highlights2/div[1]/div[1]/div[3]/div/runtime_platform_actions-actions-ribbon/ul/li[1]/runtime_platform_actions-action-renderer"); 
        //By btnEditL = By.XPath("//records-lwc-highlights-panel/records-lwc-record-layout/forcegenerated-highlightspanel_opportunity__c___012i0000000tpyfaau___compact___view___recordlayout2/records-highlights2/div[1]/div[1]/div[3]/div/runtime_platform_actions-actions-ribbon/ul/li[1]/runtime_platform_actions-action-renderer/runtime_platform_actions-executor-page-reference/slot/slot/lightning-button/button");
        By comboClientOwnershipL = By.XPath("//label[text()='Client Ownership']/parent::div//button");//button[@aria-label='Client Ownership, --None--']");
        By comboSubjectOwnershipL = By.XPath("//label[text()='Subject Ownership']/parent::div//button");//button[@aria-label='Subject Ownership, --None--']");
        By comboSICL = By.XPath("//ul/li/lightning-base-combobox-item/span[2]/span[1]/lightning-base-combobox-formatted-text/strong");
        By txtRefContactL = By.XPath("//flexipage-component2[8]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field/slot/record_flexipage-record-field/div/div/slot/records-record-layout-lookup/lightning-lookup/lightning-lookup-desktop/lightning-grouped-combobox/div/div/lightning-base-combobox/div/div/div[1]/div[1]/div/input");
        By comboRefContactL = By.XPath("//ul/li[2]/lightning-base-combobox-item/span[2]/span[1]/lightning-base-combobox-formatted-text/strong");

        By comboUpdBenOwnerL = By.XPath("//label[text()='Beneficial Owner & Control Person form?']/parent::div//button");//button[@aria-label='Beneficial Owner & Control Person form?, No']");
        By btnConfAgreeL = By.XPath("//flexipage-component2[11]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[1]/button");

        By txtEstTxnSizeL = By.XPath("//input[@name='Estimated_Transaction_Size_MM__c']");
        By btnWomenLedL = By.XPath("//button[contains(@aria-label,'Women Led')]");
        By txtDateEngL = By.XPath("//input[@name='Date_Engaged__c']");
        By tabInternalTeamL = By.XPath("//lightning-tab-bar/ul/li/a[text()='Internal Team']");
        By btnModifyRolesL = By.XPath("//div[1]/table/tbody/tr/td[2]/a");

        
        By txtStaffL = By.XPath("//input[@placeholder='Begin Typing Name...']");
        By chkInitiatorL = By.XPath("//table/tbody/tr[3]/td[2]/input");
        By chkSellerL = By.XPath("//table/tbody/tr[3]/td[3]/input");
        By chkPrincipalL = By.XPath("//table/tbody/tr[3]/td[4]/input");
        By chkManagerL = By.XPath("//table/tbody/tr[3]/td[5]/input");
        By chkAssociateL = By.XPath("//table/tbody/tr[3]/td[6]/input");
        By chkAnalystL = By.XPath("//table/tbody/tr[3]/td[7]/input");
        By btnSaveDetailsL = By.XPath("//button[@name='SaveEdit']");
        By btnSaveTeamL = By.XPath("//div[1]/table/tbody/tr/td[2]/span/input[1]");
        By btnPartyL = By.XPath("//div[4]//dl[4]/div[1]/div/div/div/div/div[1]/div/div/a");
        By tabEngTeamL = By.XPath("//section/div[1]/div/div[1]/div[2]/div/div/ul[2]/li[2]/a/span[2]");
        By btnAddCFOppContactL = By.XPath("//button[@name='Opportunity__c.Add_CF_Opportunity_Contact']");

        By txtContactL = By.XPath("//input[@placeholder='Search Contacts...']");
        By chkBillingContactL = By.XPath("//span[text()='Billing Contact']/following::input[1]");
        By chkAckBillingContactL = By.XPath("//span[text()='Acknowledge Billing Contact']/following::input[1]");
        By chkPrimaryContactL = By.XPath("//span[text()='Primary Contact']/following::input[1]");
        By btnSaveContactL = By.XPath("//footer/button[2]/span");
        By btnConvertToEngL = By.XPath("//span[text()='Convert to Engagement']");
        By lblEngagement = By.XPath("//div[text()='Engagement']");
        By lnkViewAllL = By.XPath("//article[@aria-label='Approval History']//span[text()='View All']");
        By titleApproveL = By.XPath("//h1[@title='Approval History']");
        By btnRejectL = By.XPath("//div[@title='Reject']");
        By btnApproveOppL = By.XPath("//span[text()='Approve']");
        By txtCommentsL = By.XPath("//textarea[@class='inputTextArea cuf-messageTextArea textarea']");
        By valStatusL = By.XPath("//section[2]/div/div[2]/div[1]/div[1]/div/div/div/div/div[2]/div/div[1]/div[2]/div[2]/div[1]/div/div/table/tbody/tr[1]/td[3]/span/span");

        By lnkMoreL = By.XPath("//runtime_platform_actions-actions-ribbon/ul/li[11]/lightning-button-menu/button");
        By tabDetails = By.XPath("//a[text()='Details']");
        By tabAdmin = By.XPath("//a[text()='Administration']");
        By lnkEditOppName = By.XPath("//flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");
        By valClientOwnershipBefore = By.XPath("//label[text()='Client Ownership']/ancestor::div/div[1]/lightning-base-combobox/div/div[1]/div/button/span");
        By btnClientOwnership = By.XPath("//label[text()='Client Ownership']/ancestor::lightning-combobox/div/div[1]/lightning-base-combobox/div/div[1]/div/button");
        By valClientOwnershipAfter = By.XPath("//flexipage-column2[2]/div/slot/flexipage-field[3]/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/lightning-formatted-text");
        By lnkEditPrimaryOffice = By.XPath("//button[@title='Edit Primary Office']");
        By valPrimaryOfficeBefore = By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/div/lightning-base-combobox/div/div/div[1]/button/span");
        By btnPO = By.XPath("//label[text()='Primary Office']/ancestor::lightning-combobox/div/div[1]/lightning-base-combobox/div/div[1]/div/button");
        By valPOAfter = By.XPath("//div[@data-target-selection-name='sfdc:RecordField.Opportunity__c.Primary_Office__c']/div[1]/div[2]/span/slot/lightning-formatted-text");
        By tabFees = By.XPath("//a[text()='Fees & Financials']");
        By secEstimatedFees = By.XPath("//span[text()='Estimated Fees']");
        By secFeeNotes = By.XPath("//span[text()='Fees Notes & Description']");
        By secFunds = By.XPath("//span[text()='Funds & Financials']");
        By lnkEditCurrency = By.XPath("//button[@title='Edit Currency']");
        By valCurrencyBefore = By.XPath("//button[@data-value='GBP - British Pound']/span");
        By btnCurrency = By.XPath("//label[text()='Currency']/ancestor::lightning-combobox/div/div[1]/lightning-base-combobox/div/div[1]/div/button");
        By valCurrencyAfter = By.XPath("//lightning-formatted-text[text()='CHF - Swiss Franc']");
        By btnCloseL = By.XPath("//records-record-edit-error-header/lightning-button-icon/button/lightning-primitive-icon");
        By msgEstTxnSize = By.XPath("//div[text()='The Est.Transaction Size/Market Cap (MM) cannot exceed $100,000 MM.']");
        By valEstTxnSizeL = By.XPath("//flexipage-component2[3]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/lightning-formatted-text");
        By lnkEditRetainer = By.XPath("//div[2]/div[1]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/slot/slot/flexipage-tab2[2]/slot/flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[2]/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");
        By lnkEditProgressFee = By.XPath("//div[2]/div[1]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/slot/slot/flexipage-tab2[2]/slot/flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");
        By tabClientSubject = By.XPath("//a[text()='Client/Subject & Referral']");
        By tabIT = By.XPath("//a[text()='Internal Team']");
        By tabComments = By.XPath("//a[text()='Comments']");
        By btnComments = By.XPath("//label[text()='Comment Type']/parent::div//button");//button[@aria-label='Comment Type, Internal']");
        By valCommentsType = By.XPath("//lightning-base-combobox-item[2]/span[2]/span");
        By txtCommentNotes = By.XPath("//textarea[@name='Comment__c']");
        By btnSaveComments = By.XPath("//button[@name='save']");
        By valAddedCommentType = By.XPath("//dt[text()='Comment Type:']/ancestor::dl/dd[2]/lst-template-list-field/lst-formatted-text");
        By valAddedComment = By.XPath("//dt[text()='Comment:']/ancestor::dl/dd[1]/lst-template-list-field/lightning-base-formatted-text");
        By txtUploadFiles = By.XPath("//span[text()='Upload Files']");
        By toastMsgPopup = By.XPath("//span[@title='UploadFile']");
        By txtClearOppName = By.XPath("//input[@name='Name']");
        By msgOppName = By.XPath("//div[text()='Complete this field.']");
        By valOppNameL = By.XPath("//flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/lightning-formatted-text");
        By tabOppNameL = By.XPath("//section[1]/div/div/div/div/div/ul[2]/li[2]/a");
        By btnAddCFContact = By.XPath("//button[@name='Opportunity__c.Add_CF_Opportunity_Contact']");
        By titleAddCFOppContact = By.XPath("//h2[text()='Add CF Opportunity Contact']");

        By secReferralInfo = By.XPath("//span[text()='Referral Info']");
        By secAdditionalClient = By.XPath("//span[text()='Additional Client/Subject']");
        By lnkEditRefType = By.XPath("//flexipage-tab2[5]/slot/flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/button");
        By valRefTypeBefore = By.XPath("//button[@data-value='Accountant']");
        By btnRefType = By.XPath("//label[text()='Referral Type']/ancestor::lightning-combobox/div/div[1]/lightning-base-combobox/div/div[1]/div/button");
        By valRefTypeAfter = By.XPath("//flexipage-tab2[5]/slot/flexipage-component2[1]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/span/slot[1]/lightning-formatted-text");
        By valConfAfter = By.XPath("//flexipage-field[@data-field-id='RecordConfidentiality_Agreement__cField']/slot/record_flexipage-record-field/div/div[1]/div[2]/span[1]/slot[1]/lightning-formatted-text");

        By valBenOwnerAfter = By.XPath("//flexipage-field[@data-field-id='RecordBeneficial_Owner_Control_Person_form__cField']/slot/record_flexipage-record-field/div/div[1]/div[2]/span[1]/slot[1]/lightning-formatted-text");
        By tabCompliance = By.XPath("//a[text()='Compliance & Legal']");
        By subTabCompliance = By.XPath("//a[text()='Compliance']");
        By subTabLegalMatters = By.XPath("//a[text()='Legal Matters']");
        By subTabConflictsCheck = By.XPath("//a[text()='Conflicts Check']");
        By lnkEditBeneficial = By.XPath("//slot/flexipage-tab2[6]/slot/flexipage-component2/slot/flexipage-tabset2/div/lightning-tabset/div/slot/slot/flexipage-tab2[1]/slot/flexipage-component2/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/button");
        By lnkEditConfAgreement = By.XPath("//flexipage-tab2[2]/slot/flexipage-component2/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/div[2]/button/span[1]");


        By valBenOwnerBefore = By.XPath("//label[text()='Beneficial Owner & Control Person form?']/ancestor::lightning-combobox/div/div/lightning-base-combobox/div/div/div[1]/button/span");
        By valConfAgreeBefore = By.XPath("//label[text()='Confidentiality Agreement']/ancestor::lightning-combobox/div/div/lightning-base-combobox/div/div/div[1]/button/span");

        By btnBenOwner = By.XPath("//label[text()='Beneficial Owner & Control Person form?']/ancestor::lightning-combobox/div[1]/div/lightning-base-combobox/div/div[1]/div/button");
        By btnConfAgreement = By.XPath("//label[text()='Confidentiality Agreement']/ancestor::lightning-combobox/div/div[1]/lightning-base-combobox/div/div[1]/div/button");

        By valLineOfBusiness = By.CssSelector("div[id*='00Ni000000D8hW2j']");
        By valAdditionalClient = By.CssSelector("div[id*='00Ni000000FmBzaj']");
        By valAdditionalSubject = By.CssSelector("div[id*='00Ni000000FmBzbj']");
        By valReferralType = By.CssSelector("div[id*='00Ni000000FF5uSj']");
        By linkOpportunitySector = By.XPath("//*/span[contains(text(),'Opportunity Sectors')]");
        By btnNewOpportunitySector = By.XPath("//input[@value='New Opportunity Sector']");
        By valBeneOwnerAndControlPersonForm = By.CssSelector("div[id*='00N5A00000HERR2j']");
        By valNonPublicInfo = By.CssSelector("div[id*='00Ni000000FaBznj']");
        By valLegalEntity = By.CssSelector("div[id*='CF00N5A00000M0eg5j'] a");
        By valStaffMember = By.CssSelector("div[id*='team:0:j_id7'] > label");
        By btnDeleteOpportunity = By.CssSelector("td[id*='topButtonRow'] > input[value='Delete']");
        By lnkInternalTeam = By.CssSelector("th[class=' dataCell  '] a");
        By btnDeleteInternalTeam = By.CssSelector("td[id='topButtonRow'] > input[value='Delete']");

        By txtAdditionalClientSubjects = By.CssSelector("h2[class='slds-card__header-title']>span");
        By lnkAdditionalClientSubjects = By.CssSelector("a[id*='DuhQp_link']>span");
        By btnnewAdditionalClientSubject = By.CssSelector("input[name *= 'DuhQp']");
        By inputAdditionalClientSubject = By.CssSelector("input[id = 'CF00Ni000000D9DcG']");

        By checkBoxCoExist = By.CssSelector("div[id*='00N6e00000MRVFOj_id0_j_id55_ileinner'] > img");
        By imputCoExist = By.XPath("//input[@id='00N6e00000MRVFO']");
        By txtStagePriority = By.CssSelector("select[id*='D80OA']");
        By valICOContractName = By.CssSelector("div[id*='M0ed1_body'] > table > tbody > tr:nth-child(2) > th > a");
        By valERPContractType = By.CssSelector("div[id*='M0ed1_body'] > table > tbody > tr:nth-child(2) > td:nth-child(4)");
        By valBillTo = By.CssSelector("div[id*='M0ed1_body'] > table > tbody > tr:nth-child(2) > td:nth-child(8) > a");
        By valContractStartDate = By.CssSelector("div[id*='M0ed1_body'] > table > tbody > tr:nth-child(2) > td:nth-child(9)");
        By valExternalDisclosureStat = By.CssSelector("div[id*='00Ni000000FlHaPj']");
        By btnSaveOpportunitySector = By.XPath("(//input[@title='Save'])[1]");
        By btnDeleteOpportunitySector = By.XPath("(//input[@title='Delete'])[1]");

        By valOpportunitySectorName = By.XPath("//td[contains(text(),'Opportunity Sector')]/following::div[1]");
        By linkOpportunityName = By.XPath("(//td[contains(text(),'Opportunity')])[2]/../td[2]/div/a");
        By linkSectorName = By.XPath("//*/th[contains(text(),'Opportunity Sector')]/following::tr/th/a");
        By txtCoverageType = By.CssSelector("input[id='00N6e00000MRMtkEAHCoverage_Sector_Dependency__c']");
        By txtPrimarySector = By.CssSelector("input[id='00N6e00000MRMtlEAHCoverage_Sector_Dependency__c']");
        By txtSecondarySector = By.CssSelector("input[id='00N6e00000MRMtmEAHCoverage_Sector_Dependency__c']");
        By txtTertiarySector = By.CssSelector("input[id='00N6e00000MRMtnEAHCoverage_Sector_Dependency__c']");
        By btnApplyFilters = By.XPath("//input[@title='Apply Filters']");
        By linkShowAllResults = By.XPath("//a[contains(text(),'Show all results')]");
        By inputCoverageType = By.XPath("//input[@id='00N6e00000MRMtkEAHCoverage_Sector_Dependency__c']");
        By inputPrimarySector = By.XPath("//input[@id='00N6e00000MRMtlEAHCoverage_Sector_Dependency__c']");
        By inputSecondarySector = By.XPath("//input[@id='00N6e00000MRMtmEAHCoverage_Sector_Dependency__c']");
        By inputTertiarySector = By.XPath("//input[@id='00N6e00000MRMtnEAHCoverage_Sector_Dependency__c']");
        By btnEditCompCoverageSector = By.XPath("//input[@title='Edit']");
        By valOppContact = By.CssSelector("div[id*='D7zBE_body'] table th a:nth-child(2)");
        By valOppInternalMember = By.CssSelector("[action*='HL_OpportunityInternalTeamView'] table tbody tr.dataRow.even.first.last label");

        By valOppInternalMemberMulti = By.XPath("//form[contains(@action,'HL_OpportunityInternalTeamView')]//table[contains(@id,'HLInternalTeam')]//tbody//tr[1]//label");
        //Lightning
        By tabEngagementNumL = By.XPath("//section/div/div/div/div/div/ul[2]/li[2]/a/span[2]");
        By txtSICL = By.XPath("//input[@placeholder='Search SIC Codes...']");
        By txtRetainerL = By.XPath("//input[@name= 'Retainer__c']");
        By txtMonthlyL = By.XPath("//input[@name= 'ProgressMonthly_Fee__c']");
        By txtContigentL = By.XPath("//input[@name= 'Contingent_Fee__c']");
        By txtSharedServExpL = By.XPath("//input[@name= 'Admin_Fee__c']");
        By txtEstimatedCapL = By.XPath("//input[@name= 'Expense_Cap__c']");
        By txtLegalCapL = By.XPath("//input[@name= 'Legal_Cap__c']");
        By txtTailExpiresL = By.XPath("//input[@name= 'Tail_Expires__c']");
        By comboBenOwnerL = By.XPath("//label[text()='Beneficial Owner & Control Person form?']/parent::div//button");//button[@aria-label='Beneficial Owner & Control Person form?, --None--']");
        By txtEstCloseDateL = By.XPath("//input[@name='Estimated_Close_Date__c']");
        By btnFairnessL = By.XPath("//button[contains(@aria-label,'Fairness Opinion Component')]");////button[@aria-label='Fairness Opinion Component, --None--']");
        By btnConfAgree = By.XPath("//label[text()='Confidentiality Agreement']/parent::div//button");//button[@aria-label='Confidentiality Agreement, --None--']");


        By btnReqEngL = By.XPath("//button[text()='Request Engagement']");
        By btnApproveL = By.XPath("//div[@title='Approve']");
        By lnkConvertToEngL = By.XPath("//runtime_platform_actions-actions-ribbon/ul/li[11]/lightning-button-menu/div/div/slot/runtime_platform_actions-action-renderer[2]/runtime_platform_actions-executor-page-reference/slot/slot/runtime_platform_actions-ribbon-menu-item/a/span");

        By btnRejectOppL = By.XPath("//span[text()='Reject']");
        By comboTypesOptions = By.CssSelector("select[id*= 'hWW'] option");
        By txtMsgOverlimit = By.XPath("//div[@class='message warningM2']//div[@class='messageText']");
        By btnBackPopup = By.XPath("//div[@class='message warningM2']//input[@type='Button']");
        By txtLineErrormsg = By.XPath("//div[@class='message errorM3']//li[1]");
        By btnReturnToOpporEng = By.XPath("//input[contains(@value,'Return To')]");
        By linkHLInternalTeam = By.XPath("//a//span[@id='internalTeamList_link']");
        By frameInternalTeam = By.XPath("(//iframe[@title='HL_EngagementInternalTeamView'])");
        By btnEngModifyRoles = By.XPath("(//div[contains(@class,'Custom')]//table//a[text()='Modify Roles'])[1]");
        By checkCFSpeciality = By.CssSelector("input[name*='internalTeam:j_id64:6:j_id66']");
        By txtOppNumberL = By.XPath("//span[contains(@class,'field-label')][normalize-space()='Opportunity Number']/ancestor::dt/following-sibling::dd//lightning-formatted-text");//span[contains(@class,'field-label')][normalize-space()='Opportunity Number']/parent::div/following-sibling::div//lightning-formatted-text");
        By txtRequestMsgL = By.XPath("//div[contains(@id,'modalbody')][contains(@class,'OppRequestEngagement')]");
        By btnPopupOKL = By.XPath("//div[contains(@class,'RecordTypeFooter')]//button");
        By tabApprovalHistoryL = By.XPath("//button[@title='Close Approval History']");
        By iconExpandMoreButonL = By.XPath("(//lightning-button-menu//button[contains(@class,'slds-button_icon-border-filled')])[1]");
        By btnMoreConvertToEngL = By.XPath("//span[contains(text(),'Convert to Engagement')]");
        By btnConvertToEngL2 = By.XPath("//button[contains(text(),'Convert to Engagement')]");

        By btnSharingL = By.XPath("//button[contains(text(),'Sharing')]");
        By btnMoreSharingL = By.XPath("//span[contains(text(),'Sharing')]");

        By frameInternalTeamDetailPage = By.XPath("//iframe[@title='accessibility title']");
        By frameInternalTeamModifyPage = By.XPath("//article/div[2]/div/iframe");

        By txtAssociatedOppLabelL = By.XPath("//span[text()='Associated Opportunity']");
        By editAssociatedOppFieldL = By.XPath("//flexipage-field[contains(@data-field-id,'Associated_Opportunity')]//input");
        By iconClearAssociatedOppL = By.XPath("//flexipage-field[contains(@data-field-id,'Associated_Opportunity')]//div[contains(@class,'icon-group_right')]//button");
        By txtAssociatedOppL = By.XPath("(//flexipage-field[contains(@data-field-id,'Associated_Opportunity')]//a//slot)[2]");//flexipage-field[contains(@data-field-id,'Associated_Opportunity')]//a//span");
        By btnCancelEditFormL = By.XPath("//button[@name='CancelEdit']");
        By linkReqEngL = By.XPath("//a/span[contains(text(),'Request Engagement')]");//a[contains(@name,'Request_Engagement')]");
        By txtAssociatedOppLabel = By.XPath("//table[@class='detailList']//td[text()='Associated Opportunity']");
        By editAssociatedOppField = By.XPath("//input[@name='CF00N6e00000MfcTx']");
        By txtAssociatedOpp = By.XPath("//table[@class='detailList']//td[text()='Associated Opportunity']//following::td//a[contains(@id,'MfcTx')]");
        By btnCancelEditForm = By.XPath("//td[@id='topButtonRow']//input[@name='cancel']");
        By comboIGOptions = By.CssSelector("select[id*='VT3'] option");
        By valOppERPLastIntStatus = By.CssSelector("div[id*='ffj']");
        By btnViewCounterparty = By.XPath("//button[contains(text(),'View Counterparties')]");
        By valReqFieldEng = By.CssSelector("div[class*='message error'] div");
        By lnkEstimatedClosedDateCF = By.CssSelector("div:nth-child(25) > table > tbody > tr:nth-child(3) > td:nth-child(2) > span > span > a");
        By lnkDateValuation = By.CssSelector("div:nth-child(3) > table > tbody > tr:nth-child(8) > td:nth-child(4) > span > span > a");

        By txtOppJobTypeL = By.XPath("//div//p[text()='Job Type']//following-sibling::p//lightning-formatted-text");
        By alertDuplicate = By.XPath("//div[@role='alertdialog']//button[@title='Close']");
        By loader = By.XPath("//b[contains(text(),'Loading')]");
        By btnManageRelationship = By.XPath("//span[contains(text(),'Manage Relationship')]");
        By checkboxNBC = By.XPath("//td[text()='NBC Approved']//following-sibling::td//img");
        By txtPreviousJobType = By.XPath("//flexipage-field[contains(@data-field-id,'RecordJob_Type')]//slot//lightning-formatted-text");
        By txtEngHeader = By.XPath("//h1//div//records-entity-label[text()='Engagement']");//h1//div[text()='Engagement']");

        By txtRefContactL2 = By.XPath("//label[text()='Referral Contact']/following::div[1]/div/lightning-base-combobox//input");

        By txtOppDescL2 = By.XPath("//label[text()='Opportunity Description']/following::div[1]/textarea");
        //By txtOppDescL = By.XPath("//flexipage-field[2]/slot/record_flexipage-record-field/div/span/slot/records-record-layout-text-area/lightning-textarea/div/textarea");
        By txtOppDescL = By.XPath("//label[text()='Opportunity Description']/ancestor::records-record-layout-text-area/lightning-textarea/div/textarea");

        By lblFeeNotesDes = By.XPath("//label[text()='Fee Notes & Description']");
        By lblAdditionalClient = By.XPath("//label[text()='Additional Client']");
        By lblSICCode = By.XPath("//label[text()='SIC Code']");
        By comboTASServices = By.CssSelector("select[name*='fIA']");
        By checkCFAssociate = By.CssSelector("input[name*='internalTeam:j_id64:4:j_id66']");
        By checkCFAnalyst = By.CssSelector("input[name*='internalTeam:j_id64:5:j_id66']");
        By txtEBITDAL = By.XPath("//label[text()='EBITDA (MM)']/following::div[1]//input");
        By txtEDITDA = By.XPath("//td/label[text()='EBITDA (MM)']/parent::td//following-sibling::td/input");
        By valERPLegalEntityName = By.CssSelector("div[id*='M0ed1_body'] > table > tbody > tr:nth-child(2) > td:nth-child(6)");
        By btnEditTopPanelL = By.XPath("//button[@name='Edit']");
        By btnViewCounterpartiesL = By.XPath("//button[@name='Opportunity__c.ViewCounterparties']");

        By lnkReqEngL = By.XPath("//button[@class='slds-button slds-button_icon-border-filled']");
        By valOppStatus = By.XPath("//li[1]/article/div/div[2]/ul/li[2]/div/div[2]/span");

        By tabOppActivity = By.XPath("//li[@title='Activity']//a[@id='flexipage_tab4__item']");
        By ComboStagePriorityL = By.XPath("//label[text()='Stage/Priority']/parent::div//button");//button[@aria-label='Stage/Priority, High']");
        By tabOppAdministratorL = By.XPath("//li/a[@data-label='Administration']");
        By txtDateEngaged = By.XPath("//flexipage-field[contains(@data-field-id,'Date_Engaged_cField')]//span[contains(@class,'field-value')]//lightning-formatted-text");
        By popupError = By.XPath("//div[contains(@class,'OppRequestEngagementAura')]");
        By txtErrorList = By.XPath("//div[contains(@class,'OppRequestEngagementAura')]//lightning-formatted-text");
        By iconCloseErrorL = By.XPath("//button[@title='Close this window']");
        By tabOppClientSubjectRefL = By.XPath("//li/a[contains(@data-label,'Subject & Referral')]");
        By tabOppaddClientSubjectL = By.XPath("//article[@aria-label='Additional Clients/Subjects']//h2/a");
        By lnkAdditionalClientL = By.XPath("//article//table[@aria-label='Additional Clients/Subjects']//tr[1]//th[contains(@data-label,'Opportunity Client/Subject')]//a//span");
        By lnkAdditionalSubjectL = By.XPath("//article//table[@aria-label='Additional Clients/Subjects']//tr[2]//th[contains(@data-label,'Opportunity Client/Subject')]//a//span");
        //By chkboxPrimaryL = By.XPath("//div[contains(@data-target-selection-name,'Opportunity_Client_Subject__c.Primary__c')]//span[@part='input-checkbox']//span[@part='indicator']");
        By chkboxPrimaryL = By.XPath("//article//table[@aria-label='Additional Clients/Subjects']//tr[1]//td//lst-checkbox//span[@part='indicator']");
        By btnTabCloseOppClientSubjectL = By.XPath("//button[contains(@title,'Close')][contains(@title,'Opportunity Client/Subject')]");
        By tabOppInternalTeamL = By.XPath("//li/a[contains(@data-label,'Internal Team')]");
        By txtDealTeamMember = By.XPath("//table[contains(@id,'HLInternalTeam')]//tbody/tr[1]//td//div[contains(@id,'HLInternalTeam')]/label");
        By tabInfo = By.XPath("//a[text()='Info']");
        By comboLegalAdvisorL = By.XPath("//button[contains(@aria-label,'Legal Advisor to Company')]");//button[@aria-label='Legal Advisor to Company, --None--']");
        By comboLegalAdvisorHLL = By.XPath("//button[contains(@aria-label,'Legal Advisor to HL Client Group')]");//button[@aria-label='Legal Advisor to HL Client Group, --None--']");
        By comboRefTypeL = By.XPath("//label[text()='Referral Type']/parent::div//button");//button[@aria-label='Referral Type, --None--']");
        By comboConfAggL = By.XPath("//label[text()='Confidentiality Agreement']/parent::div//button");//button[@aria-label='Confidentiality Agreement, --None--']");
        By txtLegalHoldNotes = By.XPath("(//label[text()='Legal Hold Notes']/following::div/textarea)[1]");
        By comboIndemLngL = By.XPath("//label[text()='Indemnification Language']/parent::div//button");
        By lblCAComments = By.XPath("//label[text()='CA Comments']");
        By txtTotalDebtMML = By.XPath("//input[@name='Total_Debt_MM__c']");
        By txtClientDescL = By.XPath("//label[text()='Client Description']//parent::lightning-textarea//div//textarea");
        By txtTotalDebtRepMML = By.XPath("//label[text()='Total Debt HL represents (MM)']//parent::div/div/input");
        By chkTotalDebtConfMML = By.XPath("//flexipage-field//span//input[@name='TotalDebtMMConfirmed__c']/parent::span/span");
        By cmboEUSecuritiesL = By.XPath("//button[contains(@aria-label,'EU Securities?')]");
        By headerText = By.XPath("//h1//div[text()='Engagement']");
        By labelESGLV = By.XPath("//flexipage-field[contains(@data-field-id,'ESG')]//label");
        By checkSpeciality1 = By.CssSelector("input[name*='internalTeam:j_id64:7:j_id66']");
        By txtOppNameL = By.XPath("//span[contains(@class,'field-label')][normalize-space()='Opportunity Name']/ancestor::dt/following-sibling::dd//lightning-formatted-text");//::dl//dd//lightning-formatted-text");////span[contains(@class,'field-label')][normalize-space()='Opportunity Name']/parent::div/following-sibling::div//lightning-formatted-text");
        By btnDNDOnOFF = By.XPath("//button[contains(@name,'Opportunity__c.DND_On_Off')]");
        By btnEditSharingGroup = By.XPath("//div[contains(@class,'recordsRecordShare')]//button[text()='Edit']");
        By btnCancelSharingGroup = By.XPath("//div[contains(@class,'recordsRecordShare')]//button[text()='Cancel']");
        By tblSharingGroup = By.XPath("//div[contains(@class,'recordsRecordShare')]//table//tbody");
        By txtvaluationDateL = By.XPath("//input[@name='Valuation_Date__c']");
        By comboTombstonePermissionL = By.XPath("//button[contains(@aria-label,'Tombstone Permission')]");
        By txtTotalAnticipatedRevenueL = By.XPath("//input[@name='Total_Anticipated_Revenue__c']");
        By comboTASServicesL = By.XPath("//button[contains(@aria-label,'TAS Services')]");
        By btnMoreTASDNDL = By.XPath("//span[contains(text(),'TAS DND On/Off')]");
        By btnTASDNDL = By.XPath("//button[contains(text(),'TAS DND On/Off')]");
        By chkInitiatorL2 = By.CssSelector("input[name*='internalTeam:j_id64:0:j_id66']");
        By chkPrincipalL2 = By.CssSelector("input[name*='internalTeam:j_id64:3:j_id66']");
        By chkSellerL2 = By.CssSelector("input[name*='internalTeam:j_id64:2:j_id66']");
        By labelTASDNDL = By.XPath("//div//flexipage-field[contains(@data-field-id,'TAS_DND')]");//span[@class='test-id__field-label'][normalize-space()='TAS DND']");
        By chkTASDNDL = By.XPath("//div//flexipage-field[contains(@data-field-id,'TAS_DND')]//input[@type='checkbox']");//div//flexipage-field[contains(@data-field-id,'TAS_DND')]//span[contains(@part,'indicator')]");
        By msgLVPopup = By.CssSelector("span.toastMessage.forceActionsText");
        By btnDNDConfirmL = By.XPath("//div[@role='dialog']//button[text()='Yes']");
        By checkFRSpeciality = By.CssSelector("input[name*='internalTeam:j_id64:7:j_id66']");
        By txtOppStage = By.XPath("//span[contains(@class,'field-label')][normalize-space()='Stage/Priority']/../../..//lightning-formatted-text");//span[contains(@class,'field-label')][normalize-space()='Stage/Priority']/parent::div/following-sibling::div//lightning-formatted-text");
        By txtClientNameL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordClient')]//records-hoverable-link//a//span");
        By txtSubjectNameL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordSubject')]//records-hoverable-link//a//span");
        By txtJobTypeL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordJob_Type')]//lightning-formatted-text");
        By valClientOwnershipL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordClient_Ownership')]//lightning-formatted-text");
        By valOppNumL = By.XPath("//flexipage-field[contains(@data-field-id,'Opportunity_Number__cField')]//lightning-formatted-text");
        By comboPrimaryOfficeL = By.XPath("//label[text()='Primary Office']/parent::div//button");
        By btnJobTypeL = By.XPath("//label[text()='Job Type']/parent::div//button");
        By btnLOBL = By.XPath("//label[text()='Line of Business']/parent::div//button");
        By headerEditBox = By.XPath("//h2[contains(text(),'Edit')]");
        By lblWomenLedL = By.XPath("//label[text()='Women Led']");
        By lblExpense = By.XPath("//span[text()='Expense']");
        By btnSaveL = By.XPath("//button[text()='Save']");
        By btnChangeRecordTypeL = By.XPath("//div[contains(@data-target-selection-name,'RecordType')]//button[@title='Change Record Type']");
        By headerChangeRT = By.XPath("//h1[contains(text(),'Change ')]");
        By valRecordTypeL = By.XPath("//div[contains(@data-target-selection-name,'RecordType')]//div[contains(@class,'recordTypeName')]/span");
        By btnChangeRTNextL = By.XPath("//div[contains(@class,'ChangeRecordTypeFooter')]//button[2]");
        By valERPProductTypeL = By.XPath("//div[@class='slds-form']//records-record-layout-item[@field-label='Product Type']//lightning-formatted-text");
        By valERPProductTypCodeL = By.XPath("//div[@class='slds-form']//records-record-layout-item[@field-label='ERP Product Type Code']//dd//lightning-formatted-text");
        By txtEstFee = By.XPath("//input[@name='Fee__c']");
        By btnClearHLSectionL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordIndustry_Sector')]//lightning-base-combobox//button");
        By inputHLSectorIDL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordIndustry_Sector')]//lightning-base-combobox//input");
        By listHLSectorL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordIndustry_Sector')]//div[@role='listbox']/ul/li[2]");
        By iconInlinePrimaryOfficeL = By.XPath("//div[@class='slds-form']//records-record-layout-item[@field-label='Primary Office']//dd//button");
        By btnCancelL = By.XPath("//button[@name='CancelEdit']");
        By optionsJobTypeL = By.XPath("//div[@aria-label='Job Type']//lightning-base-combobox-item//span[@class='slds-truncate']");        
        By frameWarningPopup = By.XPath("//iframe[contains(@src,'HL_InternalTeamModifyView')]");
        By btnHeader = By.XPath("//div[contains(@id,'internalTeam')]/div[@class='pbHeader']");
        By lblConflictTypeL= By.XPath("//label[text()='Conflicts Type']/parent::div//button");
        By comboOutcomeL = By.XPath("//label[text()='Outcome']/parent::div//button");
        By dateOutcomeDateL = By.XPath("//label[text()='Outcome Date']/parent::div//input");
        By lblAssociatedAddL = By.XPath("//records-record-layout-item[@field-label='Associated Address']");//label[text()='Associated Address']");
        By btnInlineEditCCOutComeL = By.XPath("//records-record-layout-item[@field-label='Outcome']//dd//button");
        By lblConflictsRunL = By.XPath("//flexipage-field[contains(@data-field-id,'Conflicts_Check')]//span[text()='Conflicts Run']");
        By txtConflictsRun = By.XPath("(//flexipage-field[contains(@data-field-id,'Conflicts_Check')]//input)[1]");
        By lblIBL = By.XPath("//label[text()='Industry Banker']");
        By iconCloseConversionPopup = By.XPath("//button[@title='Close this window']");
        By popHitaSang = By.XPath("//div[@aria-label='We hit a snag.']");
        By txtPageLevelError = By.XPath("//div[@class='pageLevelErrors']//li");
        By txtFieldLevelErrors = By.XPath("//div[contains(@class,'fieldLevelErrors')]//li/a");
        By lblHLSectorIDL = By.XPath("//label[text()='HL Sector ID']");
        By btnInlineEditCoExistL = By.XPath("//button[@title='Edit Co-exist']");
        By btnInlineEditNBCL = By.XPath("//button[@title='Edit NBC Approved']");
        By chkCCBypassL = By.XPath("//input[@name='Conflicts_Bypass__c']");
        By chkNBCBypassL = By.XPath("//input[@name='NBC_Approved__c']");
        By lblAssAddL = By.XPath("//Span[text()='Associated Address']");
        By valStageL = By.XPath("//span[contains(@class,'field-label')][text()='Stage/Priority']/../../..//lightning-formatted-text");

        By _elmRecordType(string text)
        {
            return By.XPath($"//div[contains(@class,'changeRecordTypeRightColumn')]//label//div//span[@class='slds-form-element__label'][text()='{text}']");
        }
        By _sharingGroup(string text)
        {
            return By.XPath($"//div[contains(@class,'recordsRecordShare')]//table//tbody//tr//lightning-base-formatted-text[text()='{text}']");
        }
        private By _ActivitySubject(string activitySubject)
        {
            return By.XPath($"//h2//span[text()='Opportunity Activity']//ancestor::article//lightning-primitive-cell-factory[@data-label='Subject']//lightning-base-formatted-text[text()='{activitySubject}']");
        }

        public int AddOppMultipleDealTeamMembers(string RecordType, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 20);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(chkInternalTeamPrompt).Click();
            driver.FindElement(btnSave).Click();
            int rowCount = ReadExcelData.GetRowCount(excelPath, "OppDealTeamMembers");
            int totalDealTeamMemberadded = 0;
            for (int row = 2; row <= rowCount; row++)
            {
                string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", row, 1);
                Thread.Sleep(5000);
                try
                {
                    CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveITTeam));
                    WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 20);
                    driver.FindElement(txtStaff).SendKeys(valStaff);
                    Thread.Sleep(5000);
                    CustomFunctions.MultiSelectValueWithoutSelect(driver, listStaff, valStaff);
                    Thread.Sleep(2000);
                    try
                    {
                        if (RecordType == "CF")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkCFSpeciality, 20);
                            driver.FindElement(checkCFSpeciality).Click();
                        }
                        else
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkSpeciality1, 20);
                            driver.FindElement(checkSpeciality1).Click();
                        }
                        driver.FindElement(btnSaveITTeam).Click();
                        totalDealTeamMemberadded = row - 2;
                    }
                    catch (Exception e)
                    {
                        WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 20);
                        driver.FindElement(txtStaff).Clear();
                        driver.FindElement(txtStaff).SendKeys(valStaff);
                        Thread.Sleep(5000);
                        CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valStaff);
                        Thread.Sleep(2000);
                        if (RecordType == "CF")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkCFSpeciality, 20);
                            driver.FindElement(checkCFSpeciality).Click();
                        }
                        else
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkSpeciality, 20);
                            driver.FindElement(checkSpeciality).Click();
                        }
                        driver.FindElement(btnSaveITTeam).Click();
                        totalDealTeamMemberadded = row - 2;
                    }
                }
                catch (Exception)
                {
                    return row - 2;
                }
            }
            return totalDealTeamMemberadded;
        }
        

        public string ValidateDealTeamMemberOverLimit()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtMsgOverlimit, 20);
                string msgPopup = driver.FindElement(txtMsgOverlimit).Text;
                WebDriverWaits.WaitUntilEleVisible(driver, btnBackPopup, 10);
                driver.FindElement(btnBackPopup).Click();
                return msgPopup;
            }
            catch { return "No Pop-up Message Displayed"; }
        }

        public string ValidateDealTeamMemberOverLimitLV()
        {
            try
            {
                driver.SwitchTo().Frame(driver.FindElement(frameWarningPopup));
                WebDriverWaits.WaitUntilEleVisible(driver, txtMsgOverlimit, 20);
                CustomFunctions.MoveToElement(driver, driver.FindElement(txtMsgOverlimit));
                string msgPopup = driver.FindElement(txtMsgOverlimit).Text;
                WebDriverWaits.WaitUntilEleVisible(driver, btnBackPopup, 10);
                driver.FindElement(btnBackPopup).Click();
                driver.SwitchTo().DefaultContent();
                return msgPopup;
            }
            catch { driver.SwitchTo().DefaultContent();
                return "No Pop-up Message Displayed";
            }
        }

        public string GetLineErrorMessage()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtLineErrormsg, 10);
                string txtLineErroeMsg = driver.FindElement(txtLineErrormsg).Text;
                WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpporEng);
                //CustomFunctions.MoveToElement(driver, driver.FindElement(btnReturnToOpporEng));
                CustomFunctions.MoveToElement(driver, driver.FindElement(btnHeader));
                driver.FindElement(btnReturnToOpporEng).Click();
                Thread.Sleep(5000);
                return txtLineErroeMsg;
            }
            catch
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpporEng);
                CustomFunctions.MoveToElement(driver, driver.FindElement(btnReturnToOpporEng));
                driver.FindElement(btnReturnToOpporEng).Click();
                Thread.Sleep(5000);
                return "No Line Error Validation Message";
            }
        }
        public string GetLineErrorMessageLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, frameInternalTeamModifyPage, 10);
                driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamModifyPage));
                WebDriverWaits.WaitUntilEleVisible(driver, txtLineErrormsg, 10);
                string txtLineErrorMsg = driver.FindElement(txtLineErrormsg).Text;
                WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpporEng);
                CustomFunctions.MoveToElement(driver, driver.FindElement(btnReturnToOpporEng));
                driver.FindElement(btnReturnToOpporEng).Click();
                driver.SwitchTo().DefaultContent();
                Thread.Sleep(5000);
                return txtLineErrorMsg;
            }
            catch
            {
                driver.SwitchTo().DefaultContent();
                driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamModifyPage));
                WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpporEng);
                CustomFunctions.MoveToElement(driver, driver.FindElement(btnReturnToOpporEng));
                driver.FindElement(btnReturnToOpporEng).Click();
                driver.SwitchTo().DefaultContent();
                Thread.Sleep(5000);
                return "No Line Error Validation Message";
            }
        }
        public string GetOppJobType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valJobType, 10);
            return driver.FindElement(valJobType).Text;
        }

        public bool IsJobTypePresentInDropdownOppDetailPage(string JobType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType, 10);
            driver.FindElement(comboJobType).Click();
            bool isFound = false;
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboTypesOptions);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            for (int row = 0; row < actualValue.Length; row++)
            {
                Console.WriteLine(actualValue[row]);
                if (actualValue[row].Contains(JobType))
                {
                    isFound = true;
                    break;
                }
            }
            return isFound;
        }

        //Get Opportunity Number
        //Validate additional Subject added from Additional Client/Subject Pop up
        public string ValidateAdditionalSubjectFromPopUp(string jobType, string name)
        {
            if (jobType.Equals("Creditor Advisors"))
            {
                driver.SwitchTo().DefaultContent();
                Thread.Sleep(5000);
                WebDriverWaits.WaitUntilEleVisible(driver, lnkShowMore, 200);
                driver.FindElement(lnkShowMore).Click();
                Thread.Sleep(7000);
                string value = driver.FindElement(By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr/th/a[text()='" + name + "']")).Displayed.ToString();
                string type = driver.FindElement(By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr/th/a[text()='" + name + "']/ancestor::th/following-sibling::td[1]")).Text;
                return type;
            }

            else
            {
                driver.SwitchTo().DefaultContent();
                Thread.Sleep(6000);
                string value = driver.FindElement(By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr/th/a[text()='" + name + "']")).Displayed.ToString();
                string type = driver.FindElement(By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr/th/a[text()='" + name + "']/ancestor::th/following-sibling::td[1]")).Text;
                return type;
            }
        }


        //Validate the visibility of New Opportunity Client/Subject button
        public string ValidateVisibilityOfNewOpportunityClientSubjectButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            string name = driver.FindElement(btnAdditionalClientSubject).GetAttribute("title");
            return name;
        }

        //Get the value of Est Txn Size
        public string GetEstTransactionSize()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 100);
            string value = driver.FindElement(valEstTxnSize).Text;
            string estTxn = value.Substring(0, 8);
            return estTxn;
        }

        //Update Est Txn Size
        public string UpdateEstTransactionSize()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 100);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(txtEstTxnSize).Clear();
            driver.FindElement(txtEstTxnSize).SendKeys("15");
            driver.FindElement(btnSave).Click();
            string value = driver.FindElement(valEstTxnSize).Text;
            string estTxn = value.Substring(0, 8);
            return estTxn;
        }


        public string GetExternalDisclosureStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valExternalDisclosureStat, 100);
            string extDisclosureStat = driver.FindElement(valExternalDisclosureStat).Text;
            return extDisclosureStat;
        }
        public void ClickOnViewCounterpartyButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, buttonViewCounterparty, 60);
            driver.FindElement(buttonViewCounterparty).Click();
        }
        public string GetERPContractType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPContractType);
            string valueERPContractType = driver.FindElement(valERPContractType).Text;
            return valueERPContractType;
        }   //Get Bill To Company
        public string GetBillTo()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valBillTo);
            string valueBillTo = driver.FindElement(valBillTo).Text;
            return valueBillTo;
        }
        public string GetContractStartDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContractStartDate);
            string valueContractStartDate = driver.FindElement(valContractStartDate).Text;
            return valueContractStartDate;
        }

        public bool ValidateIfTailExpiresFieldIsRequiredWhenRequestingForEngagement()
        {
            driver.FindElement(btnRequestEng).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valRegEng, 80);
            if (driver.FindElement(valRegEng).Displayed)
            {
                string errorMessages = driver.FindElement(valRegEng).Text;
                if (errorMessages.Contains("Estimated Fees - Tail Expires."))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public void UpdateTombstonePermissionField()
        {
            Thread.Sleep(3000); WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 180);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
            driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
            driver.FindElement(btnSave).Click();
        }

        public void UpdateStagePriority(string file, int row)
        {
            if (row.Equals(3))
            {
                ReadJSONData.Generate("Admin_Data.json");
                string dir = ReadJSONData.data.filePaths.testData;
                string excelPath = dir + file;
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
                driver.FindElement(btnEdit).Click();
                driver.FindElement(txtStagePriority).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 35));
                driver.FindElement(btnSave).Click();
            }
        }

        public string VerifyIfCoExistFieldIsEditableOrNot()
        {
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
            string enb = driver.FindElement(imputCoExist).GetAttribute("Type");
            if (enb == "hidden")
            {
                driver.FindElement(btnCancel).Click();
                Thread.Sleep(2000);
                return "CoExist field is not editable ";
            }
            else
            {
                driver.FindElement(btnCancel).Click();
                Thread.Sleep(2000);
                return "CoExist field is editable ";
            }
        }
       
        public string VerifyIfCoExistFieldIsEditableOrNotLV()
        {
           // driver.FindElement(tabAdministationL).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, checkBoxCoExistL, 5);
                CustomFunctions.MoveToElement(driver, driver.FindElement(checkBoxCoExistL));
                if (driver.FindElement(btnInlineEditCoExistL).Displayed)
                {
                    return "Co-Exist field is editable";
                }
                else
                {
                    return "Co-Exist field is not editable";
                }
            }
            catch { return "Co-Exist field is not editable"; }
            

        }
        public string ValidateIfCoExistFieldIsPresentAndCheckedOrNot()
        {
            if (driver.FindElement(checkBoxCoExist).Displayed)
            {
                string value = driver.FindElement(checkBoxCoExist).GetAttribute("alt");
                if (value == "Not Checked")
                {
                    return "CoExist checkbox is displayed and not-checked";
                }
                else
                {
                    return "CoExist checkbox is displayed and checked";
                }
            }
            else
            {
                return "CoExist checkbox is not displayed.";
            }
        }
        By checkBoxCoExistL = By.XPath("//input[@name='Co_exist__c']");
        By tabAdministationL = By.XPath("//lightning-tab-bar/ul/li/a[text()='Administration']");
        public string ValidateIfCoExistFieldIsPresentAndCheckedOrNotLV()
        {            
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, checkBoxCoExistL, 5);
                if (driver.FindElement(checkBoxCoExistL).Displayed)
                {
                    CustomFunctions.MoveToElement(driver, driver.FindElement(checkBoxCoExistL));     
                    if (driver.FindElement(checkBoxCoExistL).Selected)
                    {
                        return "Co-Exist checkbox is displayed and checked";
                    }
                    else
                    {
                        return "Co-Exist checkbox is displayed and not-checked";
                    }
                }
                else
                {
                    return "Co-Exist checkbox is not displayed";
                }
            }
            catch { 
                driver.FindElement(tabAdministationL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, checkBoxCoExistL, 5);
                if (driver.FindElement(checkBoxCoExistL).Displayed)
                {
                    CustomFunctions.MoveToElement(driver, driver.FindElement(checkBoxCoExistL));
                    if (driver.FindElement(checkBoxCoExistL).Selected)
                    {
                        return "Co-Exist checkbox is displayed and checked";
                    }
                    else
                    {
                        return "Co-Exist checkbox is displayed and not-checked";
                    }
                }
                else
                {
                    return "Co-Exist checkbox is not displayed";
                }
            }
            
        }

        public bool VerifyIfOpportunitySectorQuickLinkIsDisplayed()
        {
            bool result = false;
            if (driver.FindElement(linkOpportunitySector).Displayed)
            {
                result = true;
            }
            return result;
        }

        public void ClickNewOpportunitySectorButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewOpportunitySector, 120);
            driver.FindElement(btnNewOpportunitySector).Click();
            Thread.Sleep(2000);

        }


        //Validate NBC Approved checkbox
        public string ValidateNBCApprovedCheckbox()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chkNBCApproved, 200);
            string value = driver.FindElement(chkNBCApproved).GetAttribute("title");
            if (value.Equals("Checked"))
            {
                return "NBC Approved checkbox is checked";
            }
            else
            {
                return "NBC Approved checkbox is not checked";
            }

        }

        public void SelectCoverageSectorDependency(string covSectorDependencyName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, imgCoverageSectorDependencyLookUp, 120);
            driver.FindElement(imgCoverageSectorDependencyLookUp).Click();
            Thread.Sleep(2000);             // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);             //Enter search frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("searchFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='searchFrame']")));
            Thread.Sleep(2000);             //Enter dependency name
            driver.FindElement(txtSearchBox).SendKeys(covSectorDependencyName);
            driver.FindElement(btnGo).Click();
            Thread.Sleep(2000); driver.SwitchTo().DefaultContent();             //Enter results frame & click on the result
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("resultsFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='resultsFrame']")));
            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkInternalTeam, 60);
            driver.FindElement(lnkInternalTeam).Click();
            CustomFunctions.ActionClicks(driver, btnDeleteInternalTeam);
            Thread.Sleep(10000);
            alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        //To get staff member	
        public string GetStaffMember()
        {
            Thread.Sleep(2000);
            driver.SwitchTo().Frame("066i0000004Z1At");
            WebDriverWaits.WaitUntilEleVisible(driver, valStaffMember, 100);
            string staffMember = driver.FindElement(valStaffMember).Text;
            driver.SwitchTo().DefaultContent();
            return staffMember;
        }



        //Get ICO Contract Name	
        public string GetICOContractName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valICOContractName);
            string ICOContractName = driver.FindElement(valICOContractName).Text;
            return ICOContractName;
        }


        //Get ERP Legal Entity Name	
        public string GetERPLegalEntityName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityName);
            string valueERPLegalEntityName = driver.FindElement(valERPLegalEntityName).Text;
            return valueERPLegalEntityName;
        }

        public string GetOppDealTeamMember()
        {
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='HL_OpportunityInternalTeamView']")));
            string value = driver.FindElement(valOppInternalMemberMulti).Text.Trim();
            driver.SwitchTo().DefaultContent();
            return value;
        }

        public string GetOppExternalContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valOppContact, 30);
            return driver.FindElement(valOppContact).Text.Trim();
        }

        //To get Client Ownership	
        public string GetClientOwnership()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valClientOwnership, 90);
            string clientName = driver.FindElement(valClientOwnership).Text;
            return clientName;
        }

        //To get Subject Ownership	
        public string GetSubjectOwnership()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSubjectOwnership, 90);
            string clientName = driver.FindElement(valSubjectOwnership).Text;
            return clientName;
        }

        public string ClickNBCFormType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNBCFormType, 120);
            driver.FindElement(btnNBCFormType).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            WebDriverWaits.WaitUntilEleVisible(driver, titlePopUpNBC, 180);
            string title = driver.FindElement(titlePopUpNBC).Text;
            return title;
        }


        //Validate radio button Capital Market	
        public string ValidateCapitalMktRadioButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCapMkt, 120);
            string button = driver.FindElement(btnCapMkt).Text;
            return button;
        }


        //To update Pitch Date	
        public void UpdatePitchDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(linkPitchDate).Click();
            driver.FindElement(btnSave).Click();
        }


        //To Enter Request Date	
        public string SaveRequestDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(txtRequestDate).Clear();
            driver.FindElement(txtRequestDate).SendKeys("9/20/2022");
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valRequestDate, 80);
            string value = driver.FindElement(valRequestDate).Text;
            return value;
        }

        //Remove the entered Request Date	
        public void UpdateRequestDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(txtRequestDate).Clear();
            driver.FindElement(btnSave).Click();
        }

        //To Click Mass Edit Records button button
        public string ClickMassEditRecordsButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnMassEditRecords).Click();
            Thread.Sleep(3000);
            driver.SwitchTo().Frame(0);
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, titleMassEditPage, 120);
            string name = driver.FindElement(titleMassEditPage).Text;
            return name;
        }

        //Get Opportunity Number
        public string GetOppNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valOppNum);
            string oppNum = driver.FindElement(valOppNum).Text;
            return oppNum;
        }
        //Get LOB
        public string GetLOB()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLOB);
            string LOB = driver.FindElement(valLOB).Text;
            return LOB;
        }

        //Get Industry Group
        public string GetIndustryGroup()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valIG);
            string IG = driver.FindElement(valIG).Text.Substring(0, 3);
            return IG;
        }

        public string ValidateOpportunityDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            string opportunityValue = driver.FindElement(valOpportunity).Text;
            return opportunityValue;
        }

        public string ValidateDNDButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 60);
            Thread.Sleep(4000);
            string value = driver.FindElement(btnDNDOnOff).Displayed.ToString();
            Console.WriteLine(value);
            if (value.Equals("True"))
            {
                return "DND On/Off button is displayed";
            }
            else
            {
                return "DND On/Off button is not displayed";
            }
        }

        public void ClickDNDButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDNDOnOff, 80);
            driver.FindElement(btnDNDOnOff).Click();
            Thread.Sleep(3000);
            IAlert alert = driver.SwitchTo().Alert();
            //string txtMessage = alert.Text;
            alert.Accept();
        }

        //To validate lock image
        public string ValidateLockImage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 50);
            // bool valImage = WebDriverWaits.WaitUntilElementIsPresent(driver, imgLock);         
            try
            {
                string valImage = driver.FindElement(imgLock).Displayed.ToString();
                Console.WriteLine("Lock Image: " + valImage);
                return "Lock Image is displayed";
            }
            catch (Exception)
            {
                return "Lock Image is not displayed";
            }
        }

        //To validate record lock message
        public string ValidateRecordLockMessage()
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleMessage, 50);
            string lockMessage = driver.FindElement(txtMessage).Text;
            return lockMessage;
        }

        //To validate Approver 
        public string ValidateActualApprover()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkHere);
            driver.FindElement(linkHere).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tblApprovalHistory, 80);
            string valApprover = driver.FindElement(txtApprover).Text;
            return valApprover;
        }
        //To validate Status
        public string ValidateOverallStatus()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            string valStatus = driver.FindElement(txtStatus).Text;
            return valStatus;
        }
        //To validate 
        public void ClickRejectButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(linkApproveReject).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtComments, 80);
            driver.FindElement(txtComments).SendKeys("Rejected");
            driver.FindElement(btnReject).Click();
        }

        public void ClickApproveButton()
        {

            WebDriverWaits.WaitUntilEleVisible(driver, linkApproveReject, 120);
            driver.FindElement(linkApproveReject).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtComments, 100);
            driver.FindElement(txtComments).SendKeys("Approved");
            driver.FindElement(btnApprove).Click();
        }
        public string GetOpportunityName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtOpportunityName, 80);
            string oppName = driver.FindElement(txtOpportunityName).Text;
            return oppName;
        }

        public string ValidateOpportunityName(string name)
        {
            if (name.StartsWith("DND"))
            {
                return "Opportunity Name " + name + " is updated with DND - upon approval on DND Submission ";
            }
            else
            {
                return "Opportunity Name " + name + " is not updated with DND - upon approval on DND Submission ";
            }
        }
        public string ClickRequestEng()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnRequestEng).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgSuccess, 50);
            string msgSubmission = driver.FindElement(msgSuccess).Text;
            return msgSubmission;
        }
        //To get Client Name
        public string GetClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valClient, 90);
            string clientName = driver.FindElement(valClient).Text;
            return clientName;
        }

        //To get Subject 
        public string GetSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSubject, 60);
            string subName = driver.FindElement(valSubject).Text;
            return subName;
        }


        //To get Job Type
        public string GetJobType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valJobType, 70);
            string jobType = driver.FindElement(valJobType).Text;
            return jobType;
        }
        //To get IG
        public string GetIG()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valIG, 70);
            string jobType = driver.FindElement(valIG).Text;
            return jobType;
        }
        public string ClickNBCForm()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNBCForm, 120);
            driver.FindElement(btnNBCForm).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleNBCForm, 80);
            string title = driver.FindElement(titleNBCForm).Text;
            return title;
        }

        public string ClickNBCFormL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNBCFormL, 120);
            driver.FindElement(btnNBCFormL).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtDefaultTab, 140);
            string title = driver.FindElement(txtDefaultTab).Text;
            return title;
        }


        //Validate radio button M&A
        public string ValidateMARadioButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnMA, 120);
            string button = driver.FindElement(btnMA).Text;
            return button;
        }

        public string ClickNBCFormLCNBC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNBCFormL, 120);
            driver.FindElement(btnNBCFormL).Click();
            driver.SwitchTo().Window(driver.WindowHandles.Last());
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtDefaultTabCNBC, 140);
            string title = driver.FindElement(txtDefaultTabCNBC).Text;
            return title;
        }
        public void UpdateCC()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(linkRequestDate).Click();
            driver.FindElement(chkInternalTeamPrompt).Click();
            driver.FindElement(btnSave).Click();
        }

        //To update CC details only
        public void UpdateCCOnly()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(linkRequestDate).Click();
            driver.FindElement(btnSave).Click();
        }


        //To update Retainer and Monthly Fee 
        public void UpdateRetainerAndMonthlyFee()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 350);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(txtRetainer).SendKeys("12");
            driver.FindElement(txtMonthlyFee).SendKeys("15");
            driver.FindElement(btnSave).Click();
        }


        //To update CC details only for FAS records
        public void UpdateCCOnlyFAS()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(linkRequestDateFAS).Click();
            driver.FindElement(btnSave).Click();
        }

        //Click FEIS button and get title of page
        public string ClickFEISForm()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnFEIS, 120);
            driver.FindElement(btnFEIS).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleNBCForm, 60);
            string title = driver.FindElement(titleNBCForm).Text;
            return title;
        }

        //Enter HL Internal team details
        public void EnterInternalTeamDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valStaff = ReadExcelData.ReadData(excelPath, "AddOpportunity", 14);
            WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 60);
            driver.FindElement(txtStaff).SendKeys(valStaff);
            Thread.Sleep(3000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valStaff);
            WebDriverWaits.WaitUntilEleVisible(driver, chkInitiator, 50);
            //Check required checkboxes
            driver.FindElement(chkInitiator).Click();
            driver.FindElement(chkPrincipal).Click();
            driver.FindElement(chkManager).Click();
            driver.FindElement(btnSaveITTeam).Click();

            //Click to return back to Opportunity details
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 60);
            driver.FindElement(btnReturnToOpp).Click();
            Thread.Sleep(2000);
        }
        //To click on Convert to Engagement button
        public void ClickConvertToEng()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnConverttoEng, 80);
            driver.FindElement(btnConverttoEng).Click();
            driver.Navigate().Refresh();
            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEngagement, 100);
            driver.FindElement(lnkEngagement).Click();

            //try
            //{
            //    IAlert alert = driver.SwitchTo().Alert();
            //    alert.Accept();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }

        //To update Job type
        public string UpdateJobType(string jobType)
        {
        again:
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
                driver.FindElement(btnEdit).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, comboJobType, 80);
                driver.FindElement(comboJobType).SendKeys(jobType);
                driver.FindElement(btnSave).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valJobType, 20);
                return driver.FindElement(valJobType).Text;
            }


            catch (Exception)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 20);
                driver.FindElement(lnkReDisplayRec).Click();
                goto again;
                // WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 100);                
                //driver.FindElement(btnEdit).Click();
                //WebDriverWaits.WaitUntilEleVisible(driver, comboJobType, 80);
                //driver.FindElement(comboJobType).SendKeys(jobType);
                //driver.FindElement(btnSave).Click();
                //WebDriverWaits.WaitUntilEleVisible(driver, valJobType, 20);
                //return driver.FindElement(valJobType).Text;

            }
        }
        //To check the checkbox for Converted To Engagement in Opportunity details
        public void CheckConvertedToEng()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, chkConvertedtoEng, 60);
            driver.FindElement(chkConvertedtoEng).Click();
            driver.FindElement(btnSave).Click();
        }
        public string ValidateCounterparties()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCounterparties, 100);
            string exist = driver.FindElement(btnCounterparties).Displayed.ToString();
            if (exist == "True")
            {
                driver.FindElement(btnCounterparties).Click();
                return "Counterparties button is displayed";
            }
            else
            {
                return "Counterparties button is not displayed";
            }
        }

        //To update HL Internal team details
        public void UpdateInternalTeamDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 350);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(chkInternalTeamPrompt).Click();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, chkUpPrincipal, 130);
                //Check required checkboxes         
                driver.FindElement(chkUpPrincipal).Click();
                Thread.Sleep(2000);
                driver.FindElement(chkUpSeller).Click();
                driver.FindElement(chkUpManager).Click();
                driver.FindElement(chkUpMgr).Click();
                driver.FindElement(chkUpAssociate).Click();
                driver.FindElement(chkUpAnalyst).Click();
                driver.FindElement(btnSaveITTeam).Click();
                Thread.Sleep(3000);

                //Click to return back to Opportunity details
                WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 90);
                driver.FindElement(btnReturnToOpp).Click();
            }
            catch (Exception)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 100);
                driver.FindElement(lnkReDisplayRec).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 100);
                driver.FindElement(btnEdit).Click();
                driver.FindElement(chkInternalTeamPrompt).Click();
                driver.FindElement(btnSave).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, chkUpPrincipal, 130);

                //Check required checkboxes         
                driver.FindElement(chkUpPrincipal).Click();
                driver.FindElement(chkUpSeller).Click();
                driver.FindElement(chkUpManager).Click();
                driver.FindElement(chkUpMgr).Click();
                driver.FindElement(chkUpAssociate).Click();
                driver.FindElement(chkUpAnalyst).Click();

                ////Enter logged in user and assign admin role
                //driver.FindElement(txtStaff).SendKeys(valUser);
                //Thread.Sleep(3000);
                //CustomFunctions.SelectValueWithoutSelect(driver, listStaff, valUser);
                //WebDriverWaits.WaitUntilEleVisible(driver, chkAdmin, 50);
                //driver.FindElement(chkAdmin).Click();
                driver.FindElement(btnSaveITTeam).Click();

                //Click to return back to Opportunity details
                WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 60);
                driver.FindElement(btnReturnToOpp).Click();
            }
        }

        //To update Internal Team Prompt checkbox only
        public void UpdateITPrompt()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(chkInternalTeamPrompt).Click();
            driver.FindElement(btnSave).Click();
        }


        //Validate Portfolio Valuation button and click on it
        public string ClickPortfolioValuation()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, btnPortfolioValuation, 120);
            driver.FindElement(btnPortfolioValuation).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblValuationPeriods, 60);
            string title = driver.FindElement(lblValuationPeriods).Text;
            return title;
        }
        //Click Return to Opp
        public string ValidateReturnToOpp()
        {
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 90);
            driver.FindElement(btnReturnToOpp).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabAfterReturnToOpp, 90);
            string name = driver.FindElement(tabAfterReturnToOpp).Text;
            return name;
        }

        //To get Stage
        public string GetStage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStage, 60);
            Thread.Sleep(2000);
            string stage = driver.FindElement(valStage).Text;
            return stage;
        }
        //To get Pitch Date
        public string GetPitchDate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPitchDate, 60);
            string pitchDate = driver.FindElement(valPitchDate).Text;
            return pitchDate;
        }
        //To get Win Probability
        public string GetWinProbability()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valWinProb, 100);
            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            string winProb = driver.FindElement(valWinProb).Text;
            return winProb;
        }
        //To get Transaction Size
        public string GetTransactionSize()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTxnSize, 80);
            Thread.Sleep(3000);
            string txnSize = driver.FindElement(valTxnSize).Text;
            return txnSize;
        }
        //To get Retainer
        public string GetRetainer()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, valRetainer, 90);
            Thread.Sleep(2000);
            driver.Navigate().Refresh();
            string retainer = driver.FindElement(valRetainer).Text;
            return retainer;
        }
        //To get Monthly Fee
        public string GetMonthlyFee()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valMonthlyFee, 90);
            Thread.Sleep(3000);
            string monthlyFee = driver.FindElement(valMonthlyFee).Text;
            return monthlyFee;
        }
        //To get Contingent Fee
        public string GetContingentFee()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContingentFee, 100);
            string contingentFee = driver.FindElement(valContingentFee).Text;
            return contingentFee;
        }
        //To get Opportunity Comments
        public string GetOppComments()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowOppComments, 90);
            int rowCount = driver.FindElements(rowOppComments).Count;
            Console.WriteLine("rowCount: " + rowCount);
            if (rowCount > 1)
            {
                string oppComments = driver.FindElement(valOppComments).Text;
                return oppComments;
            }
            else
                return "Opportunity comments are not added ";
        }
        //To delete added Opportunity comments
        public string DeleteOppCommentsAndValidateComments()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkDel, 60);
            driver.FindElement(linkDel).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            WebDriverWaits.WaitUntilEleVisible(driver, msgOppComments, 120);
            string msg = driver.FindElement(msgOppComments).Text;
            Console.WriteLine("rowCount: " + msg);
            if (msg.Equals("No records to display"))
            {
                return "No Opportunity comments exist";
            }
            else
                return "Opportunity comments are displayed";
        }
        //To delete any existing Opportunity comments
        public string DeleteExistingComments()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, rowOppComments, 90);
            int rowCount = driver.FindElements(rowOppComments).Count;
            Console.WriteLine("rowCount: " + rowCount);
            if (rowCount > 1)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, linkDel, 60);
                driver.FindElement(linkDel).Click();
                Thread.Sleep(2000);
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
                return "Opportunity's existing comments are deleted ";
            }
            else
                return "No Opportunity comments exist ";
        }


        //To Validate Clone button
        public string ValidateCloneButton()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 60);
            Thread.Sleep(4000);
            try
            {
                string value = driver.FindElement(btnClone).Displayed.ToString();
                Console.WriteLine(value);
                if (value.Equals("True"))
                {
                    return "Clone button is displayed";
                }
                else
                {
                    return "Clone button is not displayed";
                }
            }
            catch (Exception)
            {
                return "Clone button is not displayed";
            }
        }

        //Check if user exists in deal
        public string ValidateUserIfExists()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            try
            {
                Thread.Sleep(2000);
                driver.Navigate().Refresh();
                driver.SwitchTo().Frame(2);
                Thread.Sleep(5000);
                string value1 = driver.FindElement(rowUser).Text;
                driver.SwitchTo().DefaultContent();
                Console.WriteLine(value1);
                if (value1.Equals("Emre Abale"))
                {
                    return "User exists";
                }
                else
                {
                    return "User does not exist";
                }
            }
            catch (Exception)
            {
                return "User does not exist";
            }
        }

        //To remove added user 
        public string RemoveUserFromITTeam()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 60);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(chkInternalTeamPrompt).Click();
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, chkUpPrincipal1, 70);

            driver.FindElement(chkUpPrincipal1).Click();
            driver.FindElement(chkUpSeller1).Click();
            driver.FindElement(chkUpManager1).Click();
            //driver.FindElement(chkUpMgr).Click();
            driver.FindElement(chkUpAssociate1).Click();
            driver.FindElement(chkUpAnalyst1).Click();
            driver.FindElement(chkCheckedInitiator).Click();

            driver.FindElement(txtStaff).SendKeys("Sonika Goyal");
            Thread.Sleep(3000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, "Sonika Goyal");
            WebDriverWaits.WaitUntilEleVisible(driver, chkAdmin2, 50);
            driver.FindElement(chk2ndInitiator).Click();

            driver.FindElement(btnSaveITTeam).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgHLIntTeam, 90);
            string message = driver.FindElement(msgHLIntTeam).Text.Replace("\r\n", " ");

            //Click to return back to Opportunity details
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 60);
            driver.FindElement(btnReturnToOpp).Click();
            return message;
        }

        //To update Record Type
        public string UpdateRecordTypeAndLOB()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRecordTypeChange, 90);
            driver.FindElement(lnkRecordTypeChange).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboRecType, 90);
            driver.FindElement(comboRecType).SendKeys("CF");
            driver.FindElement(btnContinue).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboLOB, 90);
            driver.FindElement(comboLOB).SendKeys("CF");
            driver.FindElement(comboJobType).SendKeys("Negotiated Fairness");
            driver.FindElement(btnSave).Click();
            string LOB = driver.FindElement(valLOB).Text;
            return LOB;
        }

        //To update additional fields when FAS changed to CF
        public void AddEstFeesWithAdmin(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtMonthlyFee, 90);
            driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
            driver.FindElement(lnkTrialExp).Click();
            driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
            driver.FindElement(lnkEstClosedDate).Click();
            driver.FindElement(comboFairnessOpinion).SendKeys("No");
            driver.FindElement(comboWomenLed).SendKeys("No");
            driver.FindElement(btnSave).Click();
        }

        //To Click Request Engagement Button         
        public string ClickRequestEngWithoutDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnRequestEng).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valRegEng, 50);
            string validations = driver.FindElement(valRegEng).Text.Replace("\r\n", ", ").ToString();
            return validations;
        }

        //To update fields to validate CF validations
        public void UpdateFieldsForConversion(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 70);
            driver.FindElement(btnEdit).Click();
            if (driver.FindElement(comboRecordType).Text.Contains("CF"))
            {
                driver.FindElement(lnkDateEngagedCF).Click();
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(comboWomenLed).SendKeys("Yes");
            }
            else if (driver.FindElement(comboRecordType).Text.Contains("FVA"))
            {
                driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(txtFee).Clear();
                driver.FindElement(txtFee).SendKeys("10000");
                Thread.Sleep(2000);
                driver.FindElement(comboWomenLed).SendKeys("Yes");
                driver.FindElement(lnkDateEngaged).Click();
            }
            else
            {
                driver.FindElement(lnkPitchDateFR).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(chkDebtConfirmed).Click();
                driver.FindElement(txtTotalDebtHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(txtClientDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(comboLegalAdvisorComp).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                driver.FindElement(comboLegalAdvisorHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 31));
                driver.FindElement(comboEUSecurities).SendKeys("No");
                driver.FindElement(lnkDateCASignedFR).Click();
                driver.FindElement(lnkDateCAExpiresFR).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                //driver.FindElement(lnkOutcomeDateFR).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkEstimatedClosedDateFR).Click();
            }
            //driver.FindElement(txtSICCode).SendKeys("9999");
            driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
            driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
            driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));
            driver.FindElement(btnSave).Click();
        }
        /// /////////////////////////////////////////////////////////////////////////////// 
        public string ValidationForTransactionSizeMarketCapWithContingentFeeValue(string file)
        {
            driver.FindElement(btnEdit).Click();
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, txtContingentFee, 20);
            driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
            driver.FindElement(txtMarketCap).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 50);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForPitchDateFR()
        {
            driver.FindElement(btnEdit).Click();
            driver.FindElement(By.CssSelector("input[name*='DwfqC']")).Clear();
            driver.FindElement(btnSave).Click();

            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 50);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }

        }
        public string ValidationForMonthlyFee()
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkPitchDateFR, 20);
            driver.FindElement(txtMonthlyFee).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 50);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForContingentFeeWithMonthlyFeeValue(string file)
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtMonthlyFee, 20);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
            driver.FindElement(txtContingentFee).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 50);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForTotalDebtHLWithContingentFeeValue(string file)
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtContingentFee, 20);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
            driver.FindElement(txtTotalDebtHL).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForDebtConfirmedWithTotalDebtValue(string file)
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtTotalDebtHL, 20);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(txtTotalDebtHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
            driver.FindElement(chkDebtConfirmed).Click();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForClientDescWithDebtConfirmedValue()
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, chkDebtConfirmed, 20);
            driver.FindElement(chkDebtConfirmed).Click();
            driver.FindElement(txtClientDesc).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForLegalAdvisorCompWithClientDescValue(string file)
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtClientDesc, 20);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(txtClientDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
            driver.FindElement(comboLegalAdvisorComp).SendKeys("--None--");
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }

        }
        public string ValidationForLegalAdvisorHLWithLegalAdvisorCompValue(string file)
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboLegalAdvisorComp, 20);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(comboLegalAdvisorComp).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
            driver.FindElement(comboLegalAdvisorHL).SendKeys("--None--");
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }

        }
        public string ValidationForEUSecurities(string file)
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboLegalAdvisorHL, 20);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(comboLegalAdvisorHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 31));
            driver.FindElement(comboEUSecurities).SendKeys("--None--");
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForDateCASignedFR()
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboEUSecurities, 20);
            driver.FindElement(comboEUSecurities).SendKeys("No");
            driver.FindElement(By.CssSelector("input[name*='wmN']")).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForDateCAExpiresFR()
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDateCASignedFR, 20);
            driver.FindElement(lnkDateCASignedFR).Click();
            driver.FindElement(By.CssSelector("input[name*='wmS']")).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForWomenLedWithLegalAdvisorValue(string file)
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboLegalAdvisorHL, 20);
            driver.FindElement(comboLegalAdvisorHL).SendKeys("Yes");
            driver.FindElement(comboWomenLed).SendKeys("--None--");
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForMarketCapWithValuationDateValue()
        {
            driver.FindElement(btnEdit).Click();
            driver.FindElement(lnkDateValuation).Click();
            driver.FindElement(txtMarketCap).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForWomenLedWithMarketCapValue(string file)
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtMarketCap, 20);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
            driver.FindElement(lnkEstClosedDate).Click();
            driver.FindElement(comboWomenLed).SendKeys("--None--");
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForWomenLedWithMarketCapValueCF(string file)
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtMarketCap, 20);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
            driver.FindElement(comboWomenLed).SendKeys("--None--");
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForDateEngagedWithWomenLedValue(string file)
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboWomenLed, 20);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(comboWomenLed).SendKeys("No");
            driver.FindElement(txtDateEngagedCF).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForTombstonePermissionWithDateEngagedValue()
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDateEngaged, 20);
            driver.FindElement(lnkDateEngaged).Click();
            driver.FindElement(comboTombstonePermission).SendKeys("--None--");
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForSICCodeWithTombstonePermissionValue()
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboTombstonePermission, 20);
            driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
            driver.FindElement(txtSICCode).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForEstimatedClosedDateWithDateEngagedValue()
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtDateEngagedCF, 20);
            driver.FindElement(txtDateEngagedCF).SendKeys("02/07/2023");
            driver.FindElement(By.CssSelector("input[name*='LTw']")).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }

        }
        public string ValidationForSICCodeWithEstimatedClosedDateValue()
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEstimatedClosedDateFR, 20);
            driver.FindElement(lnkEstimatedClosedDateFR).Click();
            driver.FindElement(txtSICCode).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForSICCodeCFWithEstimatedClosedDateValue()
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEstimatedClosedDateCF, 20);
            driver.FindElement(lnkEstimatedClosedDateCF).Click();
            driver.FindElement(txtSICCode).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForOppDescWithSICCodeValue()
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtSICCode, 20);
            driver.FindElement(txtSICCode).SendKeys("9999");
            driver.FindElement(txtOppDesc).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForRetainerWithOppDescValue(string file)
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtOppDesc, 20);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
            driver.FindElement(txtRetainer).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForReferralContactWithRetainerValue(string file)
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtRetainer, 20);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtReferralContact).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForConfAgreementWithReferralContactValue(string file)
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtReferralContact, 20);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
            driver.FindElement(comboConfAgreement).SendKeys("--None--");
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForTotalAnticipatedRevenueWithReferralContactValue(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtReferralContact, 80);
            driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
            driver.FindElement(txtAnticipatedRevenue).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForTailExpiresWithConfAgreementValue(string file)
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboConfAgreement, 20);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));
            driver.FindElement(By.CssSelector("input[name*='GAsL3']")).Clear();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForFairnessOpinionComponentWithTrialExpValue()
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkTrialExp, 20);
            driver.FindElement(lnkTrialExp).Click();
            driver.FindElement(comboFairnessOpinion).SendKeys("--None--");
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }
        }
        public string ValidationForValuationDate()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnRequestEng, 20);
                driver.FindElement(btnRequestEng).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, valReqFieldEng, 20);
                string validations = driver.FindElement(valReqFieldEng).Text.Replace("\r\n", ", ").ToString();
                return validations;
            }
            catch (Exception ex) { return "Validation not Found"; }

        }
        public void SaveWithFairnessOpinionComponent()
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkTrialExp, 20);
            driver.FindElement(comboFairnessOpinion).SendKeys("No");
            driver.FindElement(btnSave).Click();
        }
        public void SaveWithConfAgreement(string file)
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtReferralContact, 20);
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));
            driver.FindElement(btnSave).Click();
        }
        public void SaveWithDateEngaged()
        {
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtDateEngagedCF, 20);
            driver.FindElement(lnkDateEngaged).Click();
            driver.FindElement(btnSave).Click();
        }

        /// ///////////////////////////////////////////////////////////////////////////////



        //To update Client and Subject to company without address details
        public void UpdateClientandSubject(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(txtClient).Clear();
            driver.FindElement(txtClient).SendKeys(name);
            driver.FindElement(txtSubject).Clear();
            driver.FindElement(txtSubject).SendKeys(name);
            driver.FindElement(btnSave).Click();
        }


        //To update Outcome details
        public void UpdateOutcomeDetails(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 100);
            driver.FindElement(btnEdit).Click();
            try
            {
                if (driver.FindElement(comboRecordType).Text.Contains("CF"))
                {
                    driver.FindElement(lnkOutcomeDate).Click();
                }
                else if (driver.FindElement(comboRecordType).Text.Contains("FR"))
                {
                    driver.FindElement(lnkOutcomeDateFR).Click();
                }
                else
                {
                    driver.FindElement(lnkOutcomeDateFAS).Click();
                }
                driver.FindElement(comboOutcome).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 24));
                driver.FindElement(btnSave).Click();
            }
            catch (Exception)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 100);
                driver.FindElement(lnkReDisplayRec).Click();
                Thread.Sleep(1000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 110);
                driver.FindElement(btnEdit).Click();
                if (driver.FindElement(comboRecordType).Text.Contains("CF"))
                {
                    driver.FindElement(lnkOutcomeDate).Click();
                }
                else if (driver.FindElement(comboRecordType).Text.Contains("FR"))
                {
                    driver.FindElement(lnkOutcomeDateFR).Click();
                }
                else
                {
                    driver.FindElement(lnkOutcomeDateFAS).Click();
                }
                driver.FindElement(comboOutcome).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 24));
                driver.FindElement(btnSaveDetailsL).Click();
            }
        }

        By lblHLEntityL = By.XPath("//label[text()='HL Entity']");
        By inputCCOutcomeDateL = By.XPath("//label[text()='Outcome Date']/..//input");
        By comboCCOutcomeL = By.XPath("//button[@aria-label='Outcome']");
        By optionCCOutcomeL = By.XPath("//button[@aria-label='Outcome']/../..//lightning-base-combobox-item//span[@title='Cleared']");
        By lblCreatedBy = By.XPath("//span[text()='Created By']");
        By chkNBCApproveL = By.XPath("//span[text()='NBC Approved']/../..//input");

        //To update Outcome details
        public void UpdateOutcomeNBCApproveDetailsLV(string valJobType)
        {
            string dateCCOutcome = DateTime.Today.AddDays(-2).ToString("M/d/yyyy").Replace('-', '/');
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditL));
            driver.FindElement(btnEditL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lblHLEntityL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblHLEntityL));
            Thread.Sleep(2000);
            driver.FindElement(inputCCOutcomeDateL).SendKeys(dateCCOutcome);
            driver.FindElement(comboCCOutcomeL).Click();
            Thread.Sleep(2000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, optionCCOutcomeL, 5);
            }
            catch
            {
                driver.FindElement(comboCCOutcomeL).Click();
                Thread.Sleep(2000);
            }
            driver.FindElement(optionCCOutcomeL).Click();
            if (valJobType.Equals("Buyside") || valJobType.Equals("Sellside"))
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(lblCreatedBy));
                Thread.Sleep(2000);
                driver.FindElement(chkNBCApproveL).Click();
                Thread.Sleep(2000);
            }
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(8000);             
        }       

        //To update NBC Approval
        public void UpdateNBCApproval()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            Thread.Sleep(4000);
            driver.FindElement(btnEdit).Click();
            try
            {
                Thread.Sleep(5000);
                WebDriverWaits.WaitUntilEleVisible(driver, checkNBCApproved, 250);
                driver.FindElement(checkNBCApproved).Click();
                driver.FindElement(btnSave).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            }
            catch (Exception)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 100);
                driver.FindElement(lnkReDisplayRec).Click();
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 100);
                driver.FindElement(btnEdit).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, checkNBCApproved, 100);
                driver.FindElement(checkNBCApproved).Click();
                Thread.Sleep(2000);
                driver.FindElement(btnSave).Click();
            }
        }

        //To Click Request Engagement Button         
        public string GetEstFinValidation()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnRequestEng).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valEstFin, 100);
            string validations = driver.FindElement(valEstFin).Text.Replace("\r\n", ", ").ToString();
            return validations;
        }

        //To Click Request Engagement Button         
        public string GetEstFinValidationForCF()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnRequestEng).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valEstFinCF, 100);
            string validations = driver.FindElement(valEstFinCF).Text.Replace("\r\n", ", ").ToString();
            return validations;
        }
        //To update Record Type
        public void UpdateRecordType(string type)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRecordTypeChange, 90);
            driver.FindElement(lnkRecordTypeChange).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboRecType, 90);
            driver.FindElement(comboRecType).SendKeys(type);
            driver.FindElement(btnContinue).Click();
        }

        //Click Save
        public void ClickSaveButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSave);
            driver.FindElement(btnSave).Click();
        }
        //Get Record Type
        public string GetRecType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRecType);
            string recType = driver.FindElement(valRecType).Text;
            return recType;
        }

        //Add Opportunity Comments
        public void AddOppComments(string Type)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnNewComment, 10);
                driver.FindElement(btnNewComment).Click();
            }
            catch
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnNewComment1, 10);
                driver.FindElement(btnNewComment1).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, comboCommentType);
            driver.FindElement(comboCommentType).SendKeys(Type);
            driver.FindElement(txtCommentDesc).SendKeys("Testing");
            driver.FindElement(btnSave).Click();
        }

        //Get validatin message while adding comment
        public string GetCommentsSectionMessage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, msgError, 70);
            string message = driver.FindElement(msgError).Text;
            return message;
        }

        //Get added comment in Comment section
        public string GetAddedComment()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valComment, 20);
            string comment = driver.FindElement(valComment).Text;
            return comment;
        }

        //Delete added comments
        public string DeleteAddedOppComments()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDelComment, 20);
            driver.FindElement(btnDelComment).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            return "Opportunity's existing comments are deleted";
        }

        //To updated required opportunity fields of CF opp for conversion to engagement
        public void UpdateReqFieldsForCFConversion(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(2000);
            //WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 170);
            //driver.FindElement(btnEdit).Click();
            //Thread.Sleep(2000);
            try
            {
                string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 170);
                driver.FindElement(btnEdit).Click();
                Thread.Sleep(2000);
                //driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
                Console.WriteLine("Referral contact added ");
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));

                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                Console.WriteLine("txtMarketCap added ");
                if (valJobType == "Sellside")
                {
                    driver.FindElement(txtEDITDA).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                }
                driver.FindElement(lnkTrialExp).Click();
                Console.WriteLine("lnkTrialExp added ");
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(txtDateEngagedCF).SendKeys("19/02/2021");
                driver.FindElement(comboFairnessOpinion).SendKeys("No");
                driver.FindElement(lnkEstClosedDate).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                driver.FindElement(btnSave).Click();
            }
            catch (Exception)
            {
                Console.WriteLine("in catch block ");
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 80);
                driver.FindElement(lnkReDisplayRec).Click();
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
                driver.FindElement(btnEdit).Click();
                driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));

                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(lnkTrialExp).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(txtDateEngagedCF).SendKeys("19/02/2021");
                driver.FindElement(comboFairnessOpinion).SendKeys("No");
                driver.FindElement(lnkEstClosedDate).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                driver.FindElement(btnSave).Click();

            }
        }

        //To updated required opportunity fields of FVA opp for conversion to engagement
        public void UpdateReqFieldsForFVAConversion(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 180);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
            try
            {
                //driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
                Console.WriteLine("Referral contact added ");
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));
                driver.FindElement(lnkPitchDateFAS).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkValuationDate).Click();
                driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                //driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                driver.FindElement(lnkDateCASignedFAS).Click();
                driver.FindElement(lnkDateCAExpiresFAS).Click();
                driver.FindElement(btnSave).Click();
            }
            catch (Exception)
            {
                Console.WriteLine("in catch block ");
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 60);
                driver.FindElement(lnkReDisplayRec).Click();
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
                driver.FindElement(btnEdit).Click();
                //driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));

                driver.FindElement(lnkPitchDateFAS).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkValuationDate).Click();
                driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                //driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                driver.FindElement(lnkDateCASignedFAS).Click();
                driver.FindElement(lnkDateCAExpiresFAS).Click();
                Console.WriteLine("lnkDateCAExpiresFAS added ");
                driver.FindElement(btnSave).Click();
            }
        }

        //To update required Opportunity fields for conversion to engagement
        public void UpdateReqFieldsForFRConversion(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 180);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
            try
            {
                //driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
                Console.WriteLine("Referral contact added ");
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));


                driver.FindElement(lnkPitchDateFR).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(chkDebtConfirmed).Click();
                driver.FindElement(txtTotalDebtHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(txtClientDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(comboLegalAdvisorComp).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                driver.FindElement(comboLegalAdvisorHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 31));
                driver.FindElement(comboEUSecurities).SendKeys("No");
                driver.FindElement(lnkDateCASignedFR).Click();
                driver.FindElement(lnkDateCAExpiresFR).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                //driver.FindElement(lnkOutcomeDateFR).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkEstimatedClosedDateFR).Click();


                driver.FindElement(btnSave).Click();
            }
            catch (Exception)
            {
                Console.WriteLine("in catch block ");
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 60);
                driver.FindElement(lnkReDisplayRec).Click();
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
                driver.FindElement(btnEdit).Click();
                //driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));

                driver.FindElement(lnkPitchDateFR).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(chkDebtConfirmed).Click();
                driver.FindElement(txtTotalDebtHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(txtClientDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(comboLegalAdvisorComp).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                driver.FindElement(comboLegalAdvisorHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 31));
                driver.FindElement(comboEUSecurities).SendKeys("No");
                driver.FindElement(lnkDateCASignedFR).Click();
                driver.FindElement(lnkDateCAExpiresFR).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                //driver.FindElement(lnkOutcomeDateFR).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkEstimatedClosedDateFR).Click();
                driver.FindElement(btnSave).Click();
            }
        }

        //Get HL Entity in ERP section
        public string GetHLEntity()
        {
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valHLEntity, 100);
            string entity = driver.FindElement(valHLEntity).Text;
            return entity;
        }

        //Get ERP HL Entity in ERP section
        public string GetERPHLEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPHLEntity, 80);
            string ERPEntity = driver.FindElement(valERPHLEntity).Text;
            return ERPEntity;
        }

        //Get ERP Legal Entity in ERP section
        public string GetERPLegalEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityId, 80);
            string ERPEntity = driver.FindElement(valERPLegalEntityId).Text;
            return ERPEntity;
        }

        //Get ERP Submitted To Sync in ERP section
        public string GetERPSubmittedToSync()
        {
            Thread.Sleep(2000);
            driver.Navigate().Refresh();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valERPSubmittedToSync, 120);
            string syncDate = driver.FindElement(valERPSubmittedToSync).Text;
            return syncDate;
        }

        //Get ERP ID in ERP section
        public string GetERPID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPID, 80);
            string id = driver.FindElement(valERPID).Text;
            return id;
        }

        //Get ERP Project Status Code in ERP section
        public string GetERPProjStatusCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjStatusCode, 80);
            string code = driver.FindElement(valERPProjStatusCode).Text;
            return code;
        }

        //Get ERP Project Number in ERP section
        public string GetERPProjectNumber()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjectNumber, 80);
            string Number = driver.FindElement(valERPProjectNumber).Text;
            return Number;
        }

        //Get ERP Project Name in ERP section
        public string GetERPProjectName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProjectName, 80);
            string Number = driver.FindElement(valERPProjectName).Text;
            return Number;
        }

        //Get ERP LOB in ERP section
        public string GetERPLOB()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLOB, 80);
            string Number = driver.FindElement(valERPLOB).Text;
            return Number;
        }

        //Get ERP Template
        public string GetERPTemplate()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPTemplate, 80);
            string number = driver.FindElement(valERPTemplate).Text;
            return number;
        }

        //Get ERP Business Unit ID
        public string GetERPBusinessUnitID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPUnitID, 80);
            string number = driver.FindElement(valERPUnitID).Text;
            return number;
        }

        //Get ERP Business Unit
        public string GetERPBusinessUnit()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPUnit, 80);
            string unit = driver.FindElement(valERPUnit).Text;
            return unit;
        }

        //Get ERP Legal Entity ID
        public string GetERPLegalEntityID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegalEntityID, 80);
            string id = driver.FindElement(valERPLegalEntityID).Text;
            return id;
        }

        //Get ERP Entity Code
        public string GetERPEntityCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPEntityCode, 80);
            string code = driver.FindElement(valERPEntityCode).Text;
            return code;
        }

        //Get ERP Legislation Code
        public string GetERPLegCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLegCode, 80);
            string code = driver.FindElement(valERPLegCode).Text;
            return code;
        }

        //Get ERP Industry Group
        public string GetERPIndustryGroup()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPIG, 80);
            string IG = driver.FindElement(valERPIG).Text;
            return IG;
        }

        //Get ERP Last Integration Status
        public string GetERPIntegrationStatus()
        {
            Thread.Sleep(8000);
            driver.Navigate().Refresh();
            Thread.Sleep(2000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valERPLastIntStatus, 80);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valERPLastIntStatus));
            string status = driver.FindElement(valERPLastIntStatus).Text;
            return status;
        }

        //Get ERP Last Integration Response Date
        public string GetERPIntegrationResponseDate()
        {
            Thread.Sleep(8000);
            driver.Navigate().Refresh();
            Thread.Sleep(2000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valERPResponseDate, 120);
            string date = driver.FindElement(valERPResponseDate).Text;
            return date;
        }

        //Get ERP Last Integration Error Description
        public string GetERPIntegrationError()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPError, 80);
            string error = driver.FindElement(valERPError).Text;
            return error;
        }

        //Get ERP Product Type
        public string GetERPProductType()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProductType, 80);
            string type = driver.FindElement(valERPProductType).Text;
            return type;
        }

        //Get ERP Product Type Code
        public string GetERPProductTypeCode()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPProductTypeCode, 80);
            string code = driver.FindElement(valERPProductTypeCode).Text;
            return code;
        }

        //Get ERP Principal Manager ID
        public string GetERPEmailID()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valERPEmailID, 80);
            string id = driver.FindElement(valERPEmailID).Text;
            return id;
        }

        //Get ERP Update DFF
        public string GetERPUpdateDFFIfChecked()
        {
            //Thread.Sleep(2000);
            //driver.Navigate().Refresh();            
            WebDriverWaits.WaitUntilEleVisible(driver, checkERPUpdateDFF, 100);
            string value = driver.FindElement(checkERPUpdateDFF).GetAttribute("title");
            if (value.Equals("Not Checked"))
            {
                return "Checkbox is not checked";
            }
            else
            {
                return "Checkbox is checked";
            }
        }

        //Update Primary Office
        public string UpdatePrimaryOffice(string value)
        {
            Thread.Sleep(50000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboPrimaryOffice).SendKeys(value);
            driver.FindElement(btnSave).Click();
            string office = driver.FindElement(valPrimaryOffice).Text;
            return office;
        }

        //Update Industry Group
        public string UpdateIndustryGroup(string group)
        {
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboIG).SendKeys(group);
            driver.FindElement(comboSector).SendKeys("Dental");
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valIG, 100);
            string IG = driver.FindElement(valIG).Text;
            return IG;
        }

        //Update Sector
        public string UpdateSector(string sector)
        {
            Thread.Sleep(70000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboSector).SendKeys(sector);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valSector, 100);
            string Sector = driver.FindElement(valSector).Text;
            return Sector;
        }

        //Update Client Ownership
        public string UpdateClientOwnership(string client)
        {
            Thread.Sleep(70000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboClientOwnership).SendKeys(client);
            driver.FindElement(btnSave).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, valClientOwnership, 130);
            string Client = driver.FindElement(valClientOwnership).Text;
            return Client;
        }

        //Update Subject Ownership
        public string UpdateSubjectOwnership(string subject)
        {
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboSubjectOwnership).SendKeys(subject);
            driver.FindElement(btnSave).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, valSubjectOwnership, 100);
            string Subject = driver.FindElement(valSubjectOwnership).Text;
            return Subject;
        }

        //To update Job type for ERP
        public string UpdateJobTypeERP(string jobType)
        {
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboJobType, 80);
            driver.FindElement(comboJobType).SendKeys(jobType);
            driver.FindElement(btnSave).Click();
            string type = driver.FindElement(valJobType).Text;
            return type;
        }

        //To update ERP Record Type
        public string UpdateRecordTypeAndLOBERP()
        {
            Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkRecordTypeChange, 90);
            driver.FindElement(lnkRecordTypeChange).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboRecType, 90);
            driver.FindElement(comboRecType).SendKeys("FVA");
            driver.FindElement(btnContinue).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboLOB, 90);
            driver.FindElement(comboLOB).SendKeys("FVA");
            driver.FindElement(comboJobType).SendKeys("Consulting");
            driver.FindElement(txtFee).SendKeys("10000.00");
            driver.FindElement(btnSave).Click();
            string LOB = driver.FindElement(valLOB).Text;
            return LOB;
        }

        //To schedule ERP Submitted to Sync manually
        public void ScheduleERPSyncManually()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, lnkSyncDate, 90);
            driver.FindElement(lnkSyncDate).Click();
            driver.FindElement(btnSave).Click();
        }

        //Click New Contract button
        public string ClickNewContract()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewContract, 90);
            driver.FindElement(btnNewContract).Click();
            string title = driver.FindElement(titlePage).Text;
            return title;
        }
        By tabContractL = By.XPath("//a[text()='Contract']");
        By btnNewL = By.XPath("//button[@name='New']");
        By titleNewContractPageL = By.XPath("//h2[text()='New Contract']");
        By inputContractNameL = By.XPath("//input[@name='Name']");
        By txtClientL = By.XPath("//label[text()='Client']/following::div[1]/div/lightning-base-combobox//input");
        By txtBillingContactL= By.XPath("//label[text()='Billing Contact']//parent::lightning-grouped-combobox/div//input");

        public void GoToContractTabLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabContractL, 10);
            driver.FindElement(tabContractL).Click();            
        }

        public void AddContractBySelectingACompanyLV(string name, string contact, string client)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnNewL, 20);
            driver.FindElement(btnNewL).Click();
            //WebDriverWaits.WaitUntilEleVisible(driver, titleNewContractPageL, 20);
            WebDriverWaits.WaitUntilEleVisible(driver, inputContractNameL, 20);
            driver.FindElement(inputContractNameL).SendKeys(name);
            driver.FindElement(txtClientL).SendKeys(client);
            By eleClient = By.XPath($"//label[text()='Client']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{client}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleClient, 10);
            driver.FindElement(eleClient).Click();
            driver.FindElement(txtBillingContactL).SendKeys(contact);
            By eleBillingContactL = By.XPath($"//div[@role='listbox']//ul//li//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{contact}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleBillingContactL, 10);
            driver.FindElement(eleBillingContactL).Click();
            driver.FindElement(btnSaveL).Click();
        }

        //Add Contract by not selecting Is Main Contract option
        public string AddContractByNotSelectingIsMainContract(string name, string contact)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtContractName, 90);
            driver.FindElement(txtContractName).SendKeys(name);
            driver.FindElement(txtBillingContact).SendKeys(contact);
            driver.FindElement(btnSave).Click();
            string title = driver.FindElement(titlePage).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkOpportunity, 80);
            driver.FindElement(lnkOpportunity).Click();
            return title;
        }

        //Validate that Is Main Contract checkbox is checked or not
        public string ValidateIsMainContract()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkIsMain, 90);
            string main = driver.FindElement(checkIsMain).GetAttribute("title");
            if (main.Equals("Checked"))
            {
                return "Is Main Contract checkbox is checked";
            }
            else
            {
                return "Is Main Contract checkbox is not checked";
            }
        }
        By chkIsMainL = By.XPath("//input[@name='Is_Main_Contract__c']");//..//span[contains(@class,'checkbox')]");
        By linkViewAllContractL = By.XPath("//article[@aria-label='Contract']//span[@class='view-all-label']/..");
        public string ValidateIsMainContractLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chkIsMainL, 10);
            bool isSelected = driver.FindElement(chkIsMainL).Selected;
            if (isSelected)
            {
                return "Is Main Contract checkbox is checked";
            }
            else
            {
                return "Is Main Contract checkbox is not checked";
            }
        }
        public void ClickViewAllContracts()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;            
            WebDriverWaits.WaitUntilEleVisible(driver, linkViewAllContractL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(linkViewAllContractL));
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(linkViewAllContractL));
            //driver.FindElement(linkViewAllContractL).Click();
        }
        public string ValidateContractIsMainCheboxLV(string contractName)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            IWebElement elelinkContractName=driver.FindElement(By.XPath($"//table[@aria-label='Contract']//tbody//a//slot[text()='{contractName}']/.."));
            CustomFunctions.MoveToElement(driver, elelinkContractName);
            jse.ExecuteScript("arguments[0].click();", elelinkContractName);
            WebDriverWaits.WaitUntilEleVisible(driver, chkIsMainL, 20);
            bool isSelected = elelinkContractName.Selected;
            if (isSelected)
            {
                return "Is Main Contract checkbox is checked";
            }
            else
            {
                return "Is Main Contract checkbox is not checked";
            }
        }

        //Click on Edit link of Contract 
        public string ClickEditContract()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditContract, 90);
            driver.FindElement(lnkEditContract).Click();
            string title = driver.FindElement(titleEditContract).Text;
            return title;
        }

        By btnEditContractL = By.XPath("//li[contains(@data-target-selection-name,'Button.Contract__c.Edit')]//button[@name='Edit']");
        By titleEditContractL = By.XPath("//div[contains(@class,'ViewMode-normal')]//h2");
        By labelIsMainCheckboxL= By.XPath("(//input[@name='Is_Main_Contract__c'])[2]/..");
        By chkIsMainCheckboxL = By.XPath("(//input[@name='Is_Main_Contract__c'])[2]");
        By labelERPInfoL = By.XPath("//h3/span[text()='ERP Information']");
        public string ClickEditContractLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditContractL, 10);
            driver.FindElement(btnEditContractL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleEditContractL, 20);
            string title = driver.FindElement(titleEditContractL).Text;
            return title;
        }
        //Edit Contract by deselecting Is Main Contract option
        public void EditContractByDeselectingIsMainContractLV()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, labelIsMainCheckboxL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(labelERPInfoL));
            driver.FindElement(chkIsMainCheckboxL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 10);
            driver.FindElement(btnSaveL).Click();              
        }

        //Edit Contract by deselecting Is Main Contract option
        public string EditContractByDeselectingIsMainContract()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, checkSelectedIsMain, 120);
                driver.FindElement(checkSelectedIsMain).Click();
                driver.FindElement(btnSave).Click();
                Thread.Sleep(4000);
                string title = driver.FindElement(titleEditContract).Text;
                return title;
            }
            catch (Exception)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 100);
                driver.FindElement(lnkReDisplayRec).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, checkSelectedIsMain, 80);
                driver.FindElement(checkSelectedIsMain).Click();
                driver.FindElement(btnSave).Click();
                string title = driver.FindElement(titleEditContract).Text;
                return title;
            }
        }

        //Add Contract by selecting Is Main Contract option
        public string AddContractBySelectingIsMainContract(string name, string contact)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtContractName, 90);
            driver.FindElement(txtContractName).SendKeys(name);
            driver.FindElement(txtBillingContact).SendKeys(contact);
            driver.FindElement(checkSelectedIsMain).Click();
            driver.FindElement(btnSave).Click();
            string title = driver.FindElement(titlePage).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkOpportunity, 80);
            driver.FindElement(lnkOpportunity).Click();
            return title;
        }

        //Validate that Is Main Contract checkbox is checked for earlier contract when multiple contracts are there
        public string ValidateIsMainContractOfNewContract()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkIsMainContract1, 90);
            string main = driver.FindElement(checkIsMainContract1).GetAttribute("title");
            if (main.Equals("Checked"))
            {
                return "Is Main Contract checkbox is checked";
            }
            else
            {
                return "Is Main Contract checkbox is not checked";
            }
        }

        //Validate that Is Main Contract checkbox is checked or not when multiple contracts are there
        public string ValidateIsMainContractOfOldContract()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, checkIsMainContract2, 90);
            driver.Navigate().Refresh();
            Thread.Sleep(2000);
            string main = driver.FindElement(checkIsMainContract2).GetAttribute("title");
            if (main.Equals("Checked"))
            {
                return "Is Main Contract checkbox is checked";
            }
            else
            {
                return "Is Main Contract checkbox is not checked";
            }
        }

        public string Get1stContractName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContract1, 100);
            Thread.Sleep(2000);
            string name = driver.FindElement(valContract1).Text;
            return name;
        }

        //Get 2nd contract name
        public string Get2ndContractName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valContract2, 100);
            string name = driver.FindElement(valContract2).Text;
            return name;
        }

        //Add Contract by selecting a Company
        public string AddContractBySelectingACompany(string name, string contact, string client)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtContractName, 90);
            driver.FindElement(txtContractName).SendKeys(name);
            driver.FindElement(txtClientContract).SendKeys(client);
            driver.FindElement(txtBillingContact).SendKeys(contact);
            driver.FindElement(btnSave).Click();
            string title = driver.FindElement(titlePage).Text;
            WebDriverWaits.WaitUntilEleVisible(driver, lnkOpportunity, 80);
            driver.FindElement(lnkOpportunity).Click();
            return title;
        }

        //Modify Internal Team Members
        public string ModifyInternalTeamMembers(string name)
        {
            driver.SwitchTo().Frame(2);
            WebDriverWaits.WaitUntilEleVisible(driver, btnModifyRoles, 80);
            driver.FindElement(btnModifyRoles).Click();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 80);
            driver.FindElement(txtStaff).SendKeys(name);
            Thread.Sleep(7000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, name);
            Thread.Sleep(5000);

            try
            {
                if (driver.FindElement(checkInitiator).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate Seller role
        public string VerifySellerRole()
        {
            try
            {
                if (driver.FindElement(checkSeller).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate Principal role
        public string VerifyPrincipalRole()
        {
            try
            {
                if (driver.FindElement(checkPrincipal).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate Manager role
        public string VerifyManagerRole()
        {
            try
            {
                if (driver.FindElement(checkManager).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate Associate role
        public string VerifyAssociateRole()
        {
            try
            {
                if (driver.FindElement(checkAssociate).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate Analyst role
        public string VerifyAnalystRole()
        {
            try
            {
                if (driver.FindElement(checkAnalyst).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate Specialty role
        public string VerifySpecialtyRole()
        {
            try
            {
                if (driver.FindElement(checkSpeciality).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate PE/HF role
        public string VerifyPERole()
        {
            try
            {
                if (driver.FindElement(checkPE).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate Public role
        public string VerifyPublicRole()
        {
            try
            {
                if (driver.FindElement(checkPublic).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }


        //Validate Admin role
        public string VerifyAdminRole()
        {
            try
            {
                if (driver.FindElement(checkAdmin).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate RMS role
        public string VerifyRMSRole()
        {
            try
            {
                if (driver.FindElement(checkRMS).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }

        //Validate Expense Only role
        public string VerifyExpenseOnlyRole()
        {
            try
            {
                if (driver.FindElement(checkExpenseOnly).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }


        //Validate Non Registered role
        public string VerifyNonRegisteredRole()
        { try
            {
                if (driver.FindElement(checkNonRegistered).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }

        }

        //To update Total Anticipated Revenue field
        public void UpdateTotalAnticipatedRevenue(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 100);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtAnticipatedRevenue, 80);
            driver.FindElement(txtAnticipatedRevenue).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
            driver.FindElement(btnSave).Click();
        }

        //To update Total Anticipated Revenue field
        public void UpdateTotalAnticipatedRevenueForValidations()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 100);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtAnticipatedRevenue, 80);
            driver.FindElement(txtAnticipatedRevenue).SendKeys("10,000");
            driver.FindElement(btnSave).Click();
        }

        //To Validate NBC button
        public string ValidateNBCButton()
        {
            Thread.Sleep(2000);
            try
            {
                string value = driver.FindElement(btnNBCForm).Displayed.ToString();
                Console.WriteLine(value);
                if (value.Equals("True"))
                {
                    return "NBC Form button is displayed";
                }
                else
                {
                    return "NBC Form button is not displayed";
                }
            }
            catch (Exception)
            {
                return "Form button is not displayed";
            }
        }

        //Get default Company name of Additional Client
        public string GetCompanyNameOfAdditionalClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultClient, 120);
            string value = driver.FindElement(valDefaultClient).Text;
            return value;
        }

        //Validate if additional row got added after clicking cancel button
        public string ValidateIfNewRowGotAdded()
        {
            try
            {
                string value = driver.FindElement(rowAdditional).Displayed.ToString();
                Console.WriteLine(value);
                return value;
            }
            catch (Exception)
            {
                return "Row does not exist";
            }
        }

        // To validate save functionality of Additional client
        public string ValidateSaveFunctionalityOfAdditionalClient(string name, string type)
        {
            if (type.Equals("Creditor Advisors"))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtClientSubject, 80);
                driver.FindElement(txtClientSubject).SendKeys(name);
                driver.FindElement(btnSave).Click();
                Thread.Sleep(4000);
                WebDriverWaits.WaitUntilEleVisible(driver, valNewClient, 100);
                string value = driver.FindElement(valNewClient).Text;
                return value;
            }
            else
            {

                WebDriverWaits.WaitUntilEleVisible(driver, txtClientSubject, 80);
                driver.FindElement(txtClientSubject).SendKeys(name);
                driver.FindElement(btnSave).Click();

                if (name.Equals("Adobe Oil & Gas") || name.Equals("Ad Exchange Group") || name.Equals("Ad Results Media, LLC"))
                {
                    Thread.Sleep(4000);
                    WebDriverWaits.WaitUntilEleVisible(driver, lnkShowMore, 150);
                    driver.FindElement(lnkShowMore).Click();
                    Thread.Sleep(5000);
                    string value = driver.FindElement(By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr/th/a[text()='" + name + "']")).Displayed.ToString();
                    string Type = driver.FindElement(By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr/th/a[text()='" + name + "']/ancestor::th/following-sibling::td[1]")).Text;
                    return Type;
                }
                else
                {
                    Thread.Sleep(5000);
                    string value = driver.FindElement(By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr/th/a[text()='" + name + "']")).Displayed.ToString();
                    string Type = driver.FindElement(By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr/th/a[text()='" + name + "']/ancestor::th/following-sibling::td[1]")).Text;
                    return Type;
                }
            }
        }

        //Get type of added additional client record
        public string GetTypeOfAdditionalClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valClientType, 80);
            string value = driver.FindElement(valClientType).Text;
            return value;
        }

        //Validate Edit functionality of Additional Client
        public void ValidateEditFunctionalityOfAdditionalClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditClient, 80);
            driver.FindElement(lnkEditClient).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, checkPrimary, 80);
            driver.FindElement(checkPrimary).Click();
            driver.FindElement(btnSave).Click();
        }

        //Get checkbox value of added additional client record
        public string GetPrimaryCheckboxOfAdditionalClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPrimary, 80);
            string value = driver.FindElement(valPrimary).GetAttribute("title");
            return value;
        }

        //Validate Delete functionality of Additional Client
        public string ValidateDeleteFunctionalityOfAdditionalClient()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, lnkDelClient, 80);
            driver.FindElement(lnkDelClient).Click();
            driver.SwitchTo().Alert().Accept();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, valDefaultClient, 100);
            string name = driver.FindElement(valDefaultClient).Text;
            return name;
        }

        //Click Additional Client button 
        public string ClickAdditionalClientButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAdditionalClient, 120);
            driver.FindElement(btnAdditionalClient).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, titleAdditionalClient, 90);
            string title = driver.FindElement(titleAdditionalClient).Text;
            return title;
        }

        //Validate Type drop down values
        public bool VerifyTypeValues()
        {
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboTypes);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            //string[] expectedValue = {"CF", "Conflicts Check", "FAS","FR", "HL Internal Opportunity", "OPP DEL","SC"};
            string[] expectedValue = { "--None--", "Client", "Subject" };
            bool isSame = true;

            if (expectedValue.Length != actualValue.Length)
            {
                return !isSame;
            }
            for (int rec = 0; rec < expectedValue.Length; rec++)
            {
                if (!expectedValue[rec].Equals(actualValue[rec]))
                {
                    isSame = false;
                    break;
                }
            }
            return isSame;
        }

        //Get Type value from Opportunity Client Subject Edit
        public string GetTypeFromClientSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valType, 120);
            string value = driver.FindElement(valType).Text;
            return value;
        }

        // To validate cancel functionality of Additional client
        public string ValidateCancelFunctionalityOfAdditionalClient(string company)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtClientSubject, 80);
            driver.FindElement(txtClientSubject).SendKeys(company);
            driver.FindElement(btnCancel).Click();
            string page = driver.FindElement(titleOpp).Text;
            return page;
        }

        //Get Opportunity value from Opportunity Client Subject Edit
        public string GetOpportunityFromClientSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valOppClientSubject, 120);
            string value = driver.FindElement(valOppClientSubject).GetAttribute("value");
            return value;
        }

        //Get checkbox value of added Client Company
        public string GetPrimaryCheckboxOfClientCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPrimaryClient, 80);
            string value = driver.FindElement(valPrimaryClient).GetAttribute("title");
            return value;
        }

        //Get checkbox value of added Subject Company
        public string GetPrimaryCheckboxOfSubjectCompany()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPrimarySubject, 80);
            string value = driver.FindElement(valPrimarySubject).GetAttribute("title");
            return value;
        }

        //Validate the company name of Fee Attribution Party 
        public string GetCompanyNameOfFeeAttributionParty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valCompFeeAttrParty, 90);
            string value = driver.FindElement(valCompFeeAttrParty).Text;
            return value;
        }

        //Validate the company name of Key Creditors 
        public string GetCompanyNameOfKeyCreditor()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valCompKeyCreditor, 40);
                string value = driver.FindElement(valCompKeyCreditor).Text;
                return value;
            }
            catch (Exception e)
            {
                return "No new client exists";
            }
        }

        //Validate the type of Fee Attribution Party 
        public string GetTypeOfFeeAttributionParty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTypeFeeAttrParty, 100);
            string value = driver.FindElement(valTypeFeeAttrParty).Text;
            return value;
        }

        //Validate the type of Key Creditors 
        public string GetTypeOfKeyCreditor()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valTypeKeyCreditor, 40);
                string value = driver.FindElement(valTypeKeyCreditor).Text;
                return value;
            }
            catch (Exception e)
            {
                return "Key Creditor";
            }
        }

        //Validate the Record Type of Fee Attribution Party 
        public string GetRecTypeOfFeeAttributionParty()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRecTypeFeeAttrParty, 110);
            string value = driver.FindElement(valRecTypeFeeAttrParty).Text;
            return value;
        }

        //Validate the type of Key Creditors 
        public string GetRecTypeOfKeyCreditor()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, valRecTypeKeyCreditor, 40);
                string value = driver.FindElement(valRecTypeKeyCreditor).Text;
                return value;
            }
            catch (Exception e)
            {
                return "Key Creditor";
            }
        }

        //To update Additional Client and Additional Subject 
        public void UpdateAdditionalClientandSubject()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(comboAdditionalClient).SendKeys("Yes");
            driver.FindElement(comboAdditionalSubject).SendKeys("Yes");
            driver.FindElement(btnSave).Click();
        }

        //Get the value of Women Led
        public string GetWomenLedValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            string value = driver.FindElement(valWomenLed).Text;
            return value;
        }

        //Get the text of Women Led
        public string GetWomenLedText(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            if (name.Equals("Emre Abale"))
            {
                string value = driver.FindElement(txtWomenLed).Text;
                return value;
            }
            else if (name.Equals("Drew Koecher"))
            {
                string value = driver.FindElement(txtWomenLedFVA).Text;
                return value;
            }
            else
            {
                string value = driver.FindElement(txtWomenLedFR).Text;
                return value;
            }
        }

        //public void switch frame
        public void SwitchFrame()
        {
            driver.SwitchTo().Frame(0);
        }
        //Validate additional client added from Additional Client/Subject Pop up
        public string ValidateAdditionalClientFromPopUp(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            Thread.Sleep(8000);
            string value = driver.FindElement(By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[5]/th/a[text()='" + name + "']")).Displayed.ToString();
            if (value.Equals("True"))
            {
                string type = driver.FindElement(By.XPath("//*[contains(@id,'DuhQp_body')]/table/tbody/tr[5]/th/a[text()='" + name + "']/ancestor::th/following-sibling::td[1]")).Text;
                return type;
            }
            else
            {
                return "Not required value";
            }
        }

        //Validate the visibility of Mass Edit Records button
        public string ValidateVisibilityOfMassEditRecordsButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            string name = driver.FindElement(btnMassEditRecords).GetAttribute("title");
            return name;
        }

        //To Click New Opportunity Client/Subject button
        public string ClickNewOpportunityClientSubjectButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 80);
            driver.FindElement(btnAdditionalClientSubject).Click();
            string name = driver.FindElement(titlePage).Text;
            return name;
        }

        //To click on Back To Opportunity button
        public string ClickBackToOppButtonAndValidatePage()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnBackToOpp, 150);
            driver.FindElement(btnBackToOpp).Click();
            driver.SwitchTo().DefaultContent();
            string name = driver.FindElement(titleOppDetails).Text;
            return name;
        }

        //Get Total Debt Currency
        public string GetTotalDebtCurrency()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTotalDebtCurrency);
            string currency = driver.FindElement(valTotalDebtCurrency).Text;
            return currency;
        }

        //Get Total Debt Currency
        public string GetTotalDebtMM()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valTotalDebtMM);
            string value = driver.FindElement(valTotalDebtMM).Text;
            return value;
        }


        //Validate the pop up upon clicking NBC L button
        public string ValidatePopUpUponClickingNBCButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, titlePopUpNBC, 150);
            string title = driver.FindElement(titlePopUpNBC).Text;
            return title;
        }


        //Lightning-----------------------------
        //Update all required fields to convert Opportunity to Engagement 
        public void UpdateReqFieldsForCFConversionL(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngagementNumL, 320);
            driver.FindElement(tabEngagementNumL).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 100);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboClientOwnershipL, 100);
            string valClient = ReadExcelData.ReadData(excelPath, "AddOpportunity", 18);
            driver.FindElement(comboClientOwnershipL).SendKeys(valClient);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//lightning-base-combobox-item/span[2]/span[text()='" + valClient + "']")).Click();

            string valSubject = ReadExcelData.ReadData(excelPath, "AddOpportunity", 19);
            driver.FindElement(comboSubjectOwnershipL).SendKeys(valSubject);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//flexipage-field[4]/slot/record_flexipage-record-field/div/div/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/div/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valSubject + "']")).Click();

            //Enter SIC
            //driver.FindElement(txtSICL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 20));
            //Thread.Sleep(3000);
            //driver.FindElement(comboSICL).Click();

            //Opp Desc
            driver.FindElement(txtOppDescL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));

            //Estimated Fees
            driver.FindElement(txtRetainerL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtTailExpiresL).SendKeys("07/01/2023");
            driver.FindElement(txtMonthlyL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtContigentL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));

            //Ref Contact
            string valRef = ReadExcelData.ReadData(excelPath, "AddOpportunity", 22);
            driver.FindElement(txtRefContactL).SendKeys(valRef);
            Thread.Sleep(5000);
            driver.FindElement(comboRefContactL).Click();

            string valWomen = ReadExcelData.ReadData(excelPath, "AddOpportunity", 6);

            //Select Beneficial Owner
            string valBenOwner = ReadExcelData.ReadData(excelPath, "AddOpportunity", 10);
            driver.FindElement(comboUpdBenOwnerL).SendKeys(valBenOwner);
            driver.FindElement(By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/div/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/div/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();


            driver.FindElement(txtEstTxnSizeL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            string closeDate = DateTime.Today.AddDays(3).ToString("dd/mm/yyyy");
            driver.FindElement(txtEstCloseDateL).SendKeys(closeDate);
            driver.FindElement(btnWomenLedL).Click();
            Thread.Sleep(4000);

            driver.FindElement(By.XPath("//label[text()='Women Led']/ancestor::div/div/lightning-base-combobox/div[1]/div[1]/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valWomen + "']")).Click();

            ////Select Conf Agreement
            //string valConf = ReadExcelData.ReadData(excelPath, "AddOpportunity", 23);
            //Thread.Sleep(4000);
            //driver.FindElement(btnConfAgreeL).Click();
            //driver.FindElement(btnConfAgreeL).SendKeys(valConf);
            //Thread.Sleep(4000);
            //driver.FindElement(By.XPath("//lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valConf + "']")).Click();

            //Select Fairness
            Thread.Sleep(4000);
            driver.FindElement(btnFairnessL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[3]/slot/record_flexipage-record-field/div/div/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/div/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valWomen + "']")).Click();

            //Date Engaged
            string dateEng=DateTime.Today.AddDays(-3).ToString("dd/mm/yyyy");
            driver.FindElement(txtDateEngL).SendKeys(dateEng);
            Thread.Sleep(4000);
            driver.FindElement(btnSaveDetailsL).Click();
        }

        //Update Internal Team members details
        public void UpdateInternalTeamDetailsL(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeamL, 30);
            driver.FindElement(tabInternalTeamL).Click();
            Thread.Sleep(3000);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamDetailPage));
            Thread.Sleep(4000);
            driver.FindElement(btnModifyRolesL).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamModifyPage));
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamModifyPage));
            string name = ReadExcelData.ReadData(excelPath, "Users", 1);
            driver.FindElement(txtStaffL).SendKeys(name);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, name);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkInitiatorL, 240);
            //driver.FindElement(chkInitiatorL).Click();
            driver.FindElement(chkSellerL).Click();
            driver.FindElement(chkPrincipalL).Click();
            driver.FindElement(chkManagerL).Click();
            driver.FindElement(chkAssociateL).Click();
            driver.FindElement(chkAnalystL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveTeamL, 240);
            driver.FindElement(btnSaveTeamL).Click();
            Thread.Sleep(7000);
            //driver.SwitchTo().Frame(driver.FindElement(By.XPath("//article/div[2]/div/iframe")));
            driver.FindElement(btnReturnToOppL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            //WebDriverWaits.WaitUntilEleVisible(driver, tabEngTeamL, 320);
            //driver.FindElement(tabEngTeamL).Click();
        }


        //Add contact in Opportunity
        public void AddContactDetailsInOpp(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCFOppContactL, 320);
            driver.FindElement(btnAddCFOppContactL).Click();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactL, 320);
            string name = ReadExcelData.ReadData(excelPath, "AddOpportunity", 22);
            //Select Contact
            driver.FindElement(txtContactL).SendKeys(name);
            Thread.Sleep(7000);
            driver.FindElement(By.XPath("//div[2]/ul/li[4]/a/div[1]/span/img")).Click();

            driver.FindElement(chkBillingContactL).Click();
            driver.FindElement(chkAckBillingContactL).Click();

            //Select Party
            driver.FindElement(btnPartyL).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div[8]/div/ul/li[2]/a")).Click();


            driver.FindElement(chkPrimaryContactL).Click();

            driver.FindElement(btnSaveContactL).Click();
            Thread.Sleep(4000);
        }

        //Click Return to Opportunity button
        public void ClickRequestoEngL()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnReqEngL, 320);
            driver.FindElement(btnReqEngL).Click();
        }

        //Reject the submitted Opportunity
        public string ClickRejectButtonL()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,600)");
            WebDriverWaits.WaitUntilEleVisible(driver, btnRejectL, 100);
            driver.FindElement(btnRejectL).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtCommentsL).SendKeys("Rejected");
            driver.FindElement(btnRejectOppL).Click();
            Thread.Sleep(4000);
            string status = driver.FindElement(valStatusL).Text;
            return status;
        }

        //Approve in Lightning
        public string ClickApproveButtonL()
        {
            Thread.Sleep(4000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);
            driver.FindElement(tabInfo).Click();
            Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo(0,1200)");
            WebDriverWaits.WaitUntilEleVisible(driver, lnkViewAllL, 120);
            driver.FindElement(lnkViewAllL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnApproveL, 100);
            driver.FindElement(btnApproveL).Click();
            Thread.Sleep(4000);
            driver.FindElement(txtCommentsL).SendKeys("Approved");
            driver.FindElement(btnApproveOppL).Click();
            Thread.Sleep(7000);
            string status = driver.FindElement(valStatusL).Text;
            return status;
        }

        //Validate the status
        public string ValidateStatusOfOpportunity()
        {
            Thread.Sleep(4000);
            driver.Navigate().Refresh();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, valOppStatus, 100);
            string status = driver.FindElement(valOppStatus).Text;
            return status;
        }

        //Validate Approve  button
        public string ValidateApproveButton()
        {

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,500)");
            WebDriverWaits.WaitUntilEleVisible(driver, lnkViewAllL, 100);
            driver.FindElement(lnkViewAllL).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnApproveL, 100);
            string name = driver.FindElement(btnApproveL).Text;
            return name;
        }

        //Validate Approval page 
        public string ValidateApprovalHistoryPage()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, titleApproveL, 100);
            string name = driver.FindElement(titleApproveL).Text;
            return name;
        }

        //Validate Reject button
        public string ValidateRejectButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnRejectL, 100);
            string name = driver.FindElement(btnRejectL).Text;
            return name;
        }
        //To click on Convert to Engagement button
        public void ClickConvertToEngL()
        {
            Thread.Sleep(4000);
            driver.FindElement(tabEngagementNumL).Click();
            Thread.Sleep(5000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,-500)");
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkMoreL, 80);
            driver.FindElement(lnkMoreL).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkConvertToEngL, 100);
            driver.FindElement(lnkConvertToEngL).Click();
            Thread.Sleep(12000);
        }

        //Validate Details tab
        public string ValidateDetailsTabL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabDetails);
            string name = driver.FindElement(tabDetails).Text;
            return name;
        }

        //Validate Administration tab
        public string ValidateAdministrationTabL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabAdmin);
            string name = driver.FindElement(tabAdmin).Text;
            return name;
        }

        //Validate if Details tab is editable after clicking pencil icon
        public string ValidateDetailsTabIsEditable()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditOppName, 150);
            driver.FindElement(lnkEditOppName).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            string value = driver.FindElement(btnSaveDetailsL).Displayed.ToString();
            return value;
        }


        //Get default value of Client Ownership
        public string GetClientOwnershipLPostUpdate()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valClientOwnershipAfter, 150);
            string value = driver.FindElement(valClientOwnershipAfter).Text;
            return value;
        }

        //Update the value of Client Ownership
        public void UpdateClientOwnershipL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnClientOwnership, 150);
            driver.FindElement(btnClientOwnership).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//label[text()='Client Ownership']/ancestor::lightning-combobox/div[1]/div/lightning-base-combobox/div/div[1]/div/lightning-base-combobox-item[8]/span[2]/span")).Click();
            Thread.Sleep(4000);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(3000);
        }

        //Validate if Details tab is editable after clicking pencil icon
        public string ValidateAdminTabIsEditable()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabAdmin, 150);
            driver.FindElement(tabAdmin).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditPrimaryOffice, 150);
            driver.FindElement(lnkEditPrimaryOffice).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            string value = driver.FindElement(btnSaveDetailsL).Displayed.ToString();
            return value;
        }

        //Get default value of Primary Office
        public string GetPrimaryOfficeL()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valPrimaryOfficeBefore, 150);
            string value = driver.FindElement(valPrimaryOfficeBefore).Text;
            return value;
        }

        //Update the value of Primary Office
        public void UpdatePrimaryOfficeL()
        {
            driver.FindElement(btnPO).Click();
            driver.FindElement(By.XPath("//lightning-combobox/div/div/lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[3]/span[2]/span")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(3000);
        }

        //Get default value of PO
        public string GetPOPostUpdate()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valPOAfter, 150);
            string value = driver.FindElement(valPOAfter).Text;
            return value;
        }

        //Validate Fees & Financials tab
        public string ValidateFeesAndFinancialsTabL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabFees);
            string name = driver.FindElement(tabFees).Text;
            driver.FindElement(tabFees).Click();
            return name;
        }

        //Validate Estimated Fees section
        public string ValidateEstimatedFeesSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secEstimatedFees);
            string name = driver.FindElement(secEstimatedFees).Text;
            return name;
        }

        //Validate Fees Notes & Description section
        public string ValidateFeesNotesAndDescriptionSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secFeeNotes);
            string name = driver.FindElement(secFeeNotes).Text;
            return name;
        }

        //Validate Funds & Financials section
        public string ValidateFundsAndFinancialsSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secFunds);
            string name = driver.FindElement(secFunds).Text;
            return name;
        }

        //Validate if Fees & Financials tab is editable after clicking pencil icon
        public string ValidateFeesAndFinancialsTabIsEditable()
        {
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditCurrency, 150);
            driver.FindElement(lnkEditCurrency).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            string value = driver.FindElement(btnSaveDetailsL).Displayed.ToString();
            return value;
        }
        public void ClickEditRetainer()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditRetainer, 150);
            driver.FindElement(lnkEditRetainer).Click();
        }

        public void ClickEditProgressFee()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditProgressFee, 150);
            driver.FindElement(lnkEditProgressFee).Click();
        }
        //Get default value of Currency
        public string GetCurrencyL()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valCurrencyBefore, 150);
            string value = driver.FindElement(valCurrencyBefore).Text;
            return value;
        }

        //Get default value of Referral Type
        public string GetRefTypeL()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valRefTypeBefore, 150);
            string value = driver.FindElement(valRefTypeBefore).Text;
            return value;
        }

        //Get default value of Beneficial Owner
        public string GetBeneficialOwnerL()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valBenOwnerBefore, 150);
            string value = driver.FindElement(valBenOwnerBefore).Text;
            return value;
        }

        //Get default value of Legal Matters
        public string GetConfAgreementL()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valConfAgreeBefore, 160);
            string value = driver.FindElement(valBenOwnerBefore).Text;
            return value;
        }


        //Update the value of Currency
        public void UpdateCurrencyL()
        {
            driver.FindElement(btnCurrency).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[6]/span[2]/span")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(3000);
        }

        //Update the value of Ref Type
        public void UpdateRefTypeL()
        {
            driver.FindElement(btnRefType).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[6]/span[2]/span")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(3000);
        }

        //Update the value of Beneficial Owner
        public void UpdateBenOwnerL()
        {
            driver.FindElement(btnBenOwner).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(3000);
        }

        //Update the value of Confidential Agreement
        public void UpdateConfAgreementL()
        {
            driver.FindElement(btnConfAgreement).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//lightning-base-combobox/div/div/div[2]/lightning-base-combobox-item[3]/span[2]/span")).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(3000);
        }

        //Get updated value of Currency
        public string GetCurrencyPostUpdate()
        {
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, valCurrencyAfter, 150);
            string value = driver.FindElement(valCurrencyAfter).Text;
            return value;
        }

        //Get updated value of Ref Type
        public string GetRefTypePostUpdate()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valRefTypeAfter, 150);
            string value = driver.FindElement(valRefTypeAfter).Text;
            return value;
        }

        //Get updated value of Beneficial Owner
        public string GetBenOwnerPostUpdate()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valBenOwnerAfter, 150);
            string value = driver.FindElement(valBenOwnerAfter).Text;
            return value;
        }

        //Get updated value of Confidential Agreement
        public string GetConfAgreementPostUpdate()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valConfAfter, 150);
            string value = driver.FindElement(valConfAfter).Text;
            return value;
        }

        //Get the validation message when Est Transaction size exceeds than 100000

        public string GetValidationOfEstTxnSizeWhenItExceeds100000()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditRetainer, 150);
            driver.FindElement(lnkEditRetainer).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtEstTxnSizeL, 150);
            driver.FindElement(txtEstTxnSizeL).SendKeys("100001");
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(5000);
            driver.FindElement(btnCloseL).Click();
            Thread.Sleep(3000);
            string message = driver.FindElement(msgEstTxnSize).Text;
            return message;
        }

        public string GetOfEstTxnSizeWhenItIsLessThan100000()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtEstTxnSizeL, 150);
            driver.FindElement(txtEstTxnSizeL).Clear();
            driver.FindElement(txtEstTxnSizeL).SendKeys("100000");
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(3000);
            string value = driver.FindElement(valEstTxnSizeL).Text.Substring(4, 7);
            return value;
        }

        //Validate Client/Subject and Referral tab
        public string ValidateClientSubjectAndReferralTabL()
        {
            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabClientSubject);
            string name = driver.FindElement(tabClientSubject).Text;
            Thread.Sleep(3000);
            driver.FindElement(tabClientSubject).Click();
            return name;
        }

        //Validate Internal Team tab
        public string ValidateInternalTeamTabL()
        {
            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabIT);
            string name = driver.FindElement(tabIT).Text;
            driver.FindElement(tabIT).Click();
            return name;
        }

        //Validate Modify Roles button
        public string ValidateModifyRolesButton()
        {
            Thread.Sleep(6000);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamDetailPage));
            WebDriverWaits.WaitUntilEleVisible(driver, btnModifyRoles, 170);
            driver.FindElement(btnModifyRoles).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//div[1]/div[1]/div/div/article/div[2]/div/iframe")));
            WebDriverWaits.WaitUntilEleVisible(driver, txtStaffL, 120);
            driver.FindElement(txtStaffL).SendKeys("Rob Oudman");
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, "Rob Oudman");
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, checkInitiator, 240);
            driver.FindElement(checkInitiator).Click();
            driver.FindElement(btnSaveDealTeam).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedMember, 240);
            string name = driver.FindElement(valAddedMember).Text;
            return name;
        }

        //Validate Client/Subject and Referral tab
        public string ValidateComplianceAndLegalTabL()
        {
            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabCompliance);
            string name = driver.FindElement(tabCompliance).Text;
            driver.FindElement(tabCompliance).Click();
            return name;
        }

        //Validate Referral Info Section
        public string ValidateReferralInfoSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secReferralInfo, 120);
            string name = driver.FindElement(secReferralInfo).Text;
            driver.FindElement(secReferralInfo).Click();
            return name;
        }

        //Validate Additional Client/Subject Section
        public string ValidateAdditionalClientAndSubjectSection()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, secAdditionalClient);
            string name = driver.FindElement(secAdditionalClient).Text;
            driver.FindElement(secAdditionalClient).Click();
            return name;
        }

        //Validate Compliance tab
        public string ValidateComplianceTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, subTabCompliance);
            string name = driver.FindElement(subTabCompliance).Text;
            return name;
        }

        //Validate Legal Matters tab
        public string ValidateLegalMattersTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, subTabLegalMatters);
            string name = driver.FindElement(subTabLegalMatters).Text;
            return name;
        }

        //Validate Conflicts Check tab
        public string ValidateConflictsCheckTab()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, subTabConflictsCheck);
            string name = driver.FindElement(subTabConflictsCheck).Text;
            return name;
        }

        //Validate if Client/Subject and Referral tab is editable after clicking pencil icon
        public string ValidateClientSubjectRefTabIsEditable()
        {
            Thread.Sleep(5000);
            driver.FindElement(secReferralInfo).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditRefType, 150);
            driver.FindElement(lnkEditRefType).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            string value = driver.FindElement(btnSaveDetailsL).Displayed.ToString();
            return value;
        }

        //Validate if Compliance tab is editable after clicking pencil icon
        public string ValidateComplianceTabIsEditable()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditBeneficial, 150);
            driver.FindElement(lnkEditBeneficial).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            string value = driver.FindElement(btnSaveDetailsL).Displayed.ToString();
            return value;
        }

        //Validate if Legal Matters tab is editable after clicking pencil icon
        public string ValidateLegalMattersTabIsEditable()
        {
            Thread.Sleep(3000);
            driver.FindElement(subTabLegalMatters).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkEditConfAgreement, 150);
            driver.FindElement(lnkEditConfAgreement).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveDetailsL, 150);
            string value = driver.FindElement(btnSaveDetailsL).Displayed.ToString();
            return value;
        }

        //To get Line Of Business    
        public string GetLineOfBusiness()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLineOfBusiness, 100);
            string LOBvalue = driver.FindElement(valLineOfBusiness).Text;
            return LOBvalue;
        }
        //To get Industry group    
        public string GetSector()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valSector, 100);
            string sector = driver.FindElement(valSector).Text;
            return sector;
        }
        //To get additional client     
        public string GetAdditionalClientBoolValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAdditionalClient, 100);
            string addClient = driver.FindElement(valAdditionalClient).Text;
            return addClient;
        }
        //To get additional subject value     
        public string GetAdditionalSubjectBoolValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAdditionalSubject, 100);
            string addSubject = driver.FindElement(valAdditionalSubject).Text;
            return addSubject;
        }
        //To get additional subject value     
        public string GetReferalTypeValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valReferralType, 100);
            string ReferalType = driver.FindElement(valReferralType).Text;
            return ReferalType;
        }         //To get non public info value     
        public string GetNonPublicInfoValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valNonPublicInfo, 100);
            string nonPublicInfo = driver.FindElement(valNonPublicInfo).Text;
            return nonPublicInfo;
        }         //To get Bene Owner and Control Person FormValue    
        public string GetBeneOwnerAndControlPersonFormValue()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valBeneOwnerAndControlPersonForm, 100);
            string beneOwner = driver.FindElement(valBeneOwnerAndControlPersonForm).Text;
            return beneOwner;
        }
        //To get Bene Owner and Control Person FormValue    
        public string GetPrimaryOffice()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valPrimaryOffice, 100);
            string primaryOffice = driver.FindElement(valPrimaryOffice).Text;
            return primaryOffice;
        }

        //To get Legal entity    
        public string GetLegalEntity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valLegalEntity, 100);
            string legalEntity = driver.FindElement(valLegalEntity).Text;
            return legalEntity;
        }

        public void DeleteInternalTeamOfOpportunity()
        {
            CustomFunctions.ActionClicks(driver, btnDeleteOpportunity);
            Thread.Sleep(2000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkInternalTeam, 60);
            driver.FindElement(lnkInternalTeam).Click();
            CustomFunctions.ActionClicks(driver, btnDeleteInternalTeam);
            Thread.Sleep(3000);
            alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }
        public void DeleteOpportunity()
        {
            Thread.Sleep(2000);
            CustomFunctions.ActionClicks(driver, btnDeleteOpportunity);
            Thread.Sleep(3000);
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);
        }

        //To updated required opportunity fields of CF opp for conversion to engagement
        public void UpdateReqFieldsForCFConversionMultipleRows(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(2000);
            //WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 170);
            //driver.FindElement(btnEdit).Click();
            //Thread.Sleep(2000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 170);
                driver.FindElement(btnEdit).Click();
                Thread.Sleep(2000);
               // driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 22));
                Console.WriteLine("Referral contact added ");
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 23)); driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 27));
                Console.WriteLine("txtMarketCap added ");
                driver.FindElement(lnkTrialExp).Click();
                Console.WriteLine("lnkTrialExp added ");
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 17));
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(comboFairnessOpinion).SendKeys("No");
                driver.FindElement(lnkEstClosedDate).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                driver.FindElement(btnSave).Click();
            }
            catch (Exception)
            {
                Console.WriteLine("in catch block ");
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 80);
                driver.FindElement(lnkReDisplayRec).Click();
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
                driver.FindElement(btnEdit).Click();
                //driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 22));
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 23)); driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 27));
                driver.FindElement(lnkTrialExp).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 17));
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(comboFairnessOpinion).SendKeys("No");
                driver.FindElement(lnkEstClosedDate).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                driver.FindElement(btnSave).Click();
            }
        }

        //To update required Opportunity fields of FR opp for conversion to engagement
        public void UpdateReqFieldsForFRConversionMultipleRows(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 180);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
            try
            {
               // driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 22));
                Console.WriteLine("Referral contact added ");
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 23));
                driver.FindElement(lnkPitchDateFR).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 17));
                driver.FindElement(chkDebtConfirmed).Click();
                driver.FindElement(txtTotalDebtHL).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 17));
                driver.FindElement(txtClientDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(comboLegalAdvisorComp).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                driver.FindElement(comboLegalAdvisorHL).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 31));
                driver.FindElement(comboEUSecurities).SendKeys("No");
                driver.FindElement(lnkDateCASignedFR).Click();
                driver.FindElement(lnkDateCAExpiresFR).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                //driver.FindElement(lnkOutcomeDateFR).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkEstimatedClosedDateFR).Click();
                driver.FindElement(btnSave).Click();
            }
            catch (Exception)
            {
                Console.WriteLine("in catch block ");
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 60);
                driver.FindElement(lnkReDisplayRec).Click();
                Thread.Sleep(3000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
                driver.FindElement(btnEdit).Click();
                //driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 22));
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 23)); driver.FindElement(lnkPitchDateFR).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 17));
                driver.FindElement(chkDebtConfirmed).Click();
                driver.FindElement(txtTotalDebtHL).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 17));
                driver.FindElement(txtClientDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(comboLegalAdvisorComp).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                driver.FindElement(comboLegalAdvisorHL).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 31));
                driver.FindElement(comboEUSecurities).SendKeys("No");
                driver.FindElement(lnkDateCASignedFR).Click();
                driver.FindElement(lnkDateCAExpiresFR).Click();
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                //driver.FindElement(lnkOutcomeDateFR).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkEstimatedClosedDateFR).Click();
                driver.FindElement(btnSave).Click();
            }
        }

        //To updated required opportunity fields of FVA opp for conversion to engagement
        public void UpdateReqFieldsForFVAConversionMultipleRows(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 180);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
            try
            {
                //driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 22));
                Console.WriteLine("Referral contact added ");
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 23));
                driver.FindElement(lnkPitchDateFAS).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkValuationDate).Click();
                driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 27));
                //driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                driver.FindElement(lnkDateCASignedFAS).Click();
                driver.FindElement(lnkDateCAExpiresFAS).Click();
                driver.FindElement(btnSave).Click();
            }
            catch (Exception)
            {
                Console.WriteLine("in catch block ");
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 60);
                driver.FindElement(lnkReDisplayRec).Click();
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
                driver.FindElement(btnEdit).Click();
               // driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 22));
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 23)); driver.FindElement(lnkPitchDateFAS).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkValuationDate).Click();
                driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 27));
                //driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                driver.FindElement(lnkDateCASignedFAS).Click();
                driver.FindElement(lnkDateCAExpiresFAS).Click();
                Console.WriteLine("lnkDateCAExpiresFAS added ");
                driver.FindElement(btnSave).Click();
            }
        }

        public void UpdateReqFieldsForConversion(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 120);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(1000);
            try
            {
                if ((driver.FindElement(txtEstimatedFees)) != null)
                {
                    driver.FindElement(txtEstimatedFees).Clear();
                    driver.FindElement(txtEstimatedFees).SendKeys("100001");
                }
            }
            catch (Exception e)
            {
            }
            try
            {
                if ((driver.FindElement(drpdownWomenLed)) != null)
                {
                    CustomFunctions.SelectByText(driver, driver.FindElement(drpdownWomenLed), "No");
                }
            }
            catch (Exception e)
            {
            }
            driver.FindElement(txtSICCode).Clear();
            //driver.FindElement(txtSICCode).SendKeys("9999");
            driver.FindElement(txtOppDesc).Clear();
            driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
            driver.FindElement(txtRetainer).Clear();
            driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtReferralContact).Clear();
            driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
            Console.WriteLine("Referral contact added ");
            driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));
            if (driver.FindElement(comboRecordType).Text.Contains("CF"))
            {
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                Console.WriteLine("txtMarketCap added ");
                driver.FindElement(lnkTrialExp).Click();
                Console.WriteLine("lnkTrialExp added ");
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                Console.WriteLine("Conti added ");
                driver.FindElement(txtDateEngagedCF).SendKeys("02/02/2021");
                driver.FindElement(comboFairnessOpinion).SendKeys("No");
                driver.FindElement(lnkEstClosedDate).Click();
            }
            else if (driver.FindElement(comboRecordType).Text.Contains("FVA"))
            {
                driver.FindElement(lnkPitchDateFAS).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/02/2022");
                driver.FindElement(lnkValuationDate).Click();
                Console.WriteLine("lnkValuationDate added ");
                driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                driver.FindElement(txtMarketCap).Clear();
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                Console.WriteLine("txtMarketCap added ");
                driver.FindElement(txtFee).Clear();
                driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(lnkDateCASignedFAS).Click();
                driver.FindElement(lnkDateCAExpiresFAS).Click();
                Console.WriteLine("lnkDateCAExpiresFAS added ");
            }
            else
            {
                driver.FindElement(lnkPitchDateFR).Click();
                driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(chkDebtConfirmed).Click();
                driver.FindElement(txtTotalDebtHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                driver.FindElement(txtClientDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(comboLegalAdvisorComp).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                driver.FindElement(comboLegalAdvisorHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 31));
                driver.FindElement(comboEUSecurities).SendKeys("No");
                driver.FindElement(lnkDateCASignedFR).Click();
                driver.FindElement(lnkDateCAExpiresFR).Click();
                //driver.FindElement(lnkOutcomeDateFR).Click();	
                driver.FindElement(txtDateEngagedCF).SendKeys("02/02/2021");
                driver.FindElement(lnkEstimatedClosedDateFR).Click();
            }
            driver.FindElement(btnSave).Click();
            if (CustomFunctions.IsElementPresent(driver, lnkReDisplayRec))
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 100);
                driver.FindElement(lnkReDisplayRec).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 90);
                driver.FindElement(btnEdit).Click();
                //driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 22));
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 23));
                if (driver.FindElement(comboRecordType).Text.Contains("CF"))
                {
                    driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                    driver.FindElement(lnkTrialExp).Click();
                    driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                    driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                    driver.FindElement(txtDateEngagedCF).SendKeys("02/02/2021");
                    driver.FindElement(comboFairnessOpinion).SendKeys("No");
                    driver.FindElement(lnkEstClosedDate).Click();
                }
                else if (driver.FindElement(comboRecordType).Text.Contains("FVA"))
                {
                    driver.FindElement(lnkPitchDateFAS).Click();
                    driver.FindElement(txtDateEngagedCF).SendKeys("02/02/2021");
                    driver.FindElement(lnkValuationDate).Click();
                    driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                    driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                    driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                    driver.FindElement(lnkDateCASignedFAS).Click();
                    driver.FindElement(lnkDateCAExpiresFAS).Click();
                }
                else
                {
                    driver.FindElement(lnkPitchDateFR).Click();
                    driver.FindElement(txtMonthlyFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 16));
                    driver.FindElement(txtContingentFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                    driver.FindElement(chkDebtConfirmed).Click();
                    driver.FindElement(txtTotalDebtHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 17));
                    driver.FindElement(txtClientDesc).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));
                    driver.FindElement(comboLegalAdvisorComp).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 30));
                    driver.FindElement(comboLegalAdvisorHL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 31));
                    driver.FindElement(comboEUSecurities).SendKeys("No");
                    driver.FindElement(lnkDateCASignedFR).Click();
                    driver.FindElement(lnkDateCAExpiresFR).Click();
                    //driver.FindElement(lnkOutcomeDateFR).Click();	
                    driver.FindElement(txtDateEngagedCF).SendKeys("02/02/2021");
                    driver.FindElement(lnkEstimatedClosedDateFR).Click();
                }
                driver.FindElement(btnSave).Click();
            }
        }

        public void DeleteOpportunitySector()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkSectorName, 120);
            driver.FindElement(linkSectorName).Click();
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteOpportunitySector, 120);
            driver.FindElement(btnDeleteOpportunitySector).Click();
            Thread.Sleep(2000);

            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(3000);
        }

        //Get default value of Client Ownership
        public string GetClientOwnershipL()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, valClientOwnershipBefore, 150);
            string value = driver.FindElement(valClientOwnershipBefore).Text;
            return value;
        }

        public void UpdateReqFieldsForFVAConversionMultipleRows1(string file, int row)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(3000); WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 180);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
            try
            {
                //driver.FindElement(txtSICCode).SendKeys("9999");
                driver.FindElement(txtOppDesc).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 21));
                driver.FindElement(txtRetainer).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 15));
                driver.FindElement(txtReferralContact).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 22));
                Console.WriteLine("Referral contact added ");
                driver.FindElement(comboConfAgreement).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 23));
                driver.FindElement(lnkPitchDateFAS).Click();
                driver.FindElement(txtDateEngagedCF).SendKeys("02/09/2021");
                driver.FindElement(lnkValuationDate).Click();
                //driver.FindElement(comboTombstonePermission).SendKeys("No Restrictions");
                driver.FindElement(txtMarketCap).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 27));
                //driver.FindElement(txtFee).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 27));
                driver.FindElement(comboWomenLed).SendKeys(ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", row, 30));
                driver.FindElement(lnkDateCASignedFAS).Click();
                driver.FindElement(lnkDateCAExpiresFAS).Click();
                driver.FindElement(btnSave).Click();
            }
            catch (Exception)
            {
            }
        }
        //Lightning-----------------------------
        //Update all required fields to convert Opportunity to Engagement 
        public void UpdateReqFieldsForCFConversionL2(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Console.WriteLine("path:" + excelPath);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 100);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboClientOwnershipL, 100);
            string valClient = ReadExcelData.ReadData(excelPath, "AddOpportunity", 18);
            driver.FindElement(comboClientOwnershipL).SendKeys(valClient);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//flexipage-field[3]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valClient + "']")).Click();

            string valSubject = ReadExcelData.ReadData(excelPath, "AddOpportunity", 19);
            driver.FindElement(comboSubjectOwnershipL).SendKeys(valSubject);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div/slot/flexipage-field[4]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valSubject + "']")).Click();

            //Enter SIC
            //driver.FindElement(txtSICL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 20));
            //Thread.Sleep(3000);
            //driver.FindElement(comboSICL).Click();

            //Opp Desc
            driver.FindElement(txtOppDescL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));

            //Estimated Fees
            driver.FindElement(txtRetainerL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtTailExpiresL).SendKeys("07/01/2023");
            driver.FindElement(txtMonthlyL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtContigentL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));

            //Ref Contact
            string valRef = ReadExcelData.ReadData(excelPath, "AddOpportunity", 22);
            driver.FindElement(txtRefContactL).SendKeys(valRef);
            Thread.Sleep(5000);
            driver.FindElement(comboRefContactL).Click();

            string valWomen = ReadExcelData.ReadData(excelPath, "AddOpportunity", 6);

            //Select Beneficial Owner
            string valBenOwner = ReadExcelData.ReadData(excelPath, "AddOpportunity", 10);
            driver.FindElement(comboUpdBenOwnerL).SendKeys(valBenOwner);
            driver.FindElement(By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();

            driver.FindElement(txtEstTxnSizeL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtEstCloseDateL).SendKeys("07/01/2023");
            driver.FindElement(btnWomenLedL).Click();
            Thread.Sleep(4000);

            driver.FindElement(By.XPath("//lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valWomen + "']")).Click();

            //Select Fairness
            Thread.Sleep(4000);
            driver.FindElement(btnFairnessL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span[text()='" + valWomen + "']")).Click();

            //Date Engaged            
            driver.FindElement(txtDateEngL).SendKeys("07/12/2022");
            Thread.Sleep(4000);
            driver.FindElement(btnSaveDetailsL).Click();
        }

        public void ClickReturnToOpportunityLV()
        {
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamModifyPage));
            Thread.Sleep(2000);
            driver.FindElement(btnReturnToOpp).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 30);
        }
        //Click Return to Opportunity button
        public void ClickRequestToEngL()
        {
            try
            {
                Thread.Sleep(5000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnReqEngL, 20);
                driver.FindElement(btnReqEngL).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButonL, 10);
                driver.FindElement(iconExpandMoreButonL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, linkReqEngL, 20);
                driver.FindElement(linkReqEngL).Click();
            }
        }


        public string GetOpportunityNumberL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtOppNumberL, 20);
            return driver.FindElement(txtOppNumberL).Text;
        }

        public int AddOppMultipleDealTeamMembersLV(string RecordType, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeamL, 30);
            driver.FindElement(tabInternalTeamL).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamDetailPage));
            Thread.Sleep(4000);
            driver.FindElement(btnModifyRolesL).Click();
            Thread.Sleep(6000);
            driver.SwitchTo().DefaultContent();
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamModifyPage));

            int rowCount = ReadExcelData.GetRowCount(excelPath, "OppDealTeamMembers");
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveITTeam));
            int totalDealTeamMemberadded = 0;
            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", row, 1);
                    Thread.Sleep(5000);
                    WebDriverWaits.WaitUntilEleVisible(driver, txtStaffL, 20);
                    driver.FindElement(txtStaffL).SendKeys(valStaff);
                    Thread.Sleep(5000);
                    CustomFunctions.MultiSelectValueWithoutSelect(driver, listStaff, valStaff);
                    Thread.Sleep(2000);
                    try
                    {
                        if (RecordType == "CF")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkCFSpeciality, 20);
                            driver.FindElement(checkCFSpeciality).Click();
                        }
                        else if (RecordType == "FR")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkFRSpeciality, 20);
                            driver.FindElement(checkFRSpeciality).Click();
                        }
                        else
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkFRSpeciality, 20);
                            driver.FindElement(checkFRSpeciality).Click();
                        }

                        driver.FindElement(btnSaveITTeam).Click();
                        totalDealTeamMemberadded = row - 2;
                    }
                    catch (Exception e)
                    {
                        WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 20);
                        driver.FindElement(txtStaff).Clear();
                        driver.FindElement(txtStaff).SendKeys(valStaff);
                        Thread.Sleep(5000);
                        CustomFunctions.MultiSelectValueWithoutSelect(driver, listStaff, valStaff);
                        Thread.Sleep(2000);
                        if (RecordType == "CF")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkCFSpeciality, 20);
                            driver.FindElement(checkCFSpeciality).Click();
                        }
                        else
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkSpeciality, 20);
                            driver.FindElement(checkSpeciality).Click();
                        }
                        driver.FindElement(btnSaveITTeam).Click();
                        totalDealTeamMemberadded = row - 2;
                    }
                }
                catch (Exception)
                {
                    return row - 2;
                }
            }
            driver.SwitchTo().DefaultContent();
            return totalDealTeamMemberadded;
        }
        public string GetRequestToEngMsgL()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtRequestMsgL, 20);
            string txtMsg = driver.FindElement(txtRequestMsgL).Text;
            driver.FindElement(btnPopupOKL).Click();
            Thread.Sleep(5000);
            return txtMsg;
        }
        public void CloseApprovalHistoryTabL()
        {
            driver.FindElement(tabApprovalHistoryL).Click();
        }
        public void ClickConvertToEngagementL()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);
            try
            {
                js.ExecuteScript("window.scrollTo(0,0)");
                WebDriverWaits.WaitUntilEleVisible(driver, btnConvertToEngL2, 10);
                driver.FindElement(btnConvertToEngL2).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButonL, 10);
                driver.FindElement(iconExpandMoreButonL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnMoreConvertToEngL, 10);
                driver.FindElement(btnMoreConvertToEngL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 30);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngHeader, 60);
        }
        public void ClickConvertToEngagementL2()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);
            try
            {
                js.ExecuteScript("window.scrollTo(0,0)");
                WebDriverWaits.WaitUntilEleVisible(driver, btnConvertToEngL2, 10);
                driver.FindElement(btnConvertToEngL2).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButonL, 10);
                driver.FindElement(iconExpandMoreButonL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnMoreConvertToEngL, 10);
                driver.FindElement(btnMoreConvertToEngL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 30);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngHeader, 60);
        }
        public bool IsAssociatedOppFieldPresent()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtAssociatedOppLabel, 10);
                return driver.FindElement(txtAssociatedOppLabel).Displayed;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public bool IsAssociatedOppFieldPresentLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtAssociatedOppLabelL, 10);
                return driver.FindElement(txtAssociatedOppLabelL).Displayed;
            }
            catch (Exception e)
            {
                return false;
            }



        }
        public bool IsAssociatedOppFieldEditable()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 10);
                driver.FindElement(btnEdit).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, editAssociatedOppField, 10);
                bool IsDisplayed = driver.FindElement(editAssociatedOppField).Displayed;
                WebDriverWaits.WaitUntilEleVisible(driver, btnCancelEditForm, 10);
                driver.FindElement(btnCancelEditForm).Click();
                return IsDisplayed;
            }
            catch (Exception e)
            {
                driver.FindElement(btnCancelEditForm).Click();
                return false;
            }
        }
        public bool IsAssociatedOppFieldEditableLV()
        {
            try
            {
                Thread.Sleep(5000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 10);
                driver.FindElement(btnEditL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, editAssociatedOppFieldL, 10);
                CustomFunctions.MoveToElement(driver, driver.FindElement(editAssociatedOppFieldL));
                bool IsDisplayed = driver.FindElement(editAssociatedOppFieldL).Displayed;
                WebDriverWaits.WaitUntilEleVisible(driver, btnCancelEditFormL, 10);
                driver.FindElement(btnCancelEditFormL).Click();
                return IsDisplayed;
            }
            catch (Exception e)
            {
                driver.FindElement(btnCancelEditFormL).Click();
                return false;
            }
        }
        public void EnterAssociatedOpportunity(string name)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 10);
                driver.FindElement(btnEdit).Click();
                driver.FindElement(editAssociatedOppField).Clear();
                driver.FindElement(editAssociatedOppField).SendKeys(name);
                driver.FindElement(btnSave).Click();
                Thread.Sleep(8000);
                WebDriverWaits.WaitUntilEleVisible(driver, txtAssociatedOpp, 20);                
            }
            catch (Exception e)
            {
                driver.FindElement(btnCancelEditForm).Click();                
            }

        }

        public string GetAssociatedOpportunity()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtAssociatedOpp, 20);
            return driver.FindElement(txtAssociatedOpp).Text;
        }

        By editLbloppDescL = By.XPath("//label[text()='Opportunity Description']");
        public void EnterAssociatedOpportunityLV(string name)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            try
            {
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 10);
                driver.FindElement(btnEditL).Click();
                CustomFunctions.MoveToElement(driver, driver.FindElement(editLbloppDescL));
                WebDriverWaits.WaitUntilEleVisible(driver, iconClearAssociatedOppL, 10);
                jse.ExecuteScript("arguments[0].click();", driver.FindElement(iconClearAssociatedOppL));

                driver.FindElement(editAssociatedOppFieldL).SendKeys(name);
                Thread.Sleep(3000);
                driver.FindElement(By.XPath($"//div[@role='listbox']//ul//li//lightning-base-combobox-formatted-text[@title='{name}']")).Click();
                driver.FindElement(btnSaveDetailsL).Click();
                Thread.Sleep(10000);
            }
            catch (Exception e)
            {
                driver.FindElement(btnCancelEditFormL).Click();
            }
        }

        public string GetAssociatedOpportunityLV()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtAssociatedOppL, 20);
            return driver.FindElement(txtAssociatedOppL).Text;
        }

        public bool IsGetIndustryGroupSaved(string value)
        {
            By txtIndustryGroup = By.XPath($"//td[text()='Industry Group']//following-sibling::td//div[contains(text(),'{value}')]");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtIndustryGroup, 20);
                return driver.FindElement(txtIndustryGroup).Displayed;
            }
            catch { return false; }
        }

        public bool IsIndustryTypePresentInDropdownOppDetailPage(string IndustryType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 10);
            driver.FindElement(btnEdit).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboIG, 10);
            driver.FindElement(comboIG).Click();
            bool isFound = false;
            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(comboIGOptions);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            for (int row = 0; row < actualValue.Length; row++)
            {
                Console.WriteLine(actualValue[row]);
                if (actualValue[row].Contains(IndustryType))
                {
                    isFound = true;
                    break;
                }
            }
            driver.FindElement(btnCancel).Click();
            return isFound;
        }

        public string GetOppERPIntegrationStatus()
        {
            Thread.Sleep(8000);
            driver.Navigate().Refresh();
            Thread.Sleep(2000);
            driver.Navigate().Refresh();
            WebDriverWaits.WaitUntilEleVisible(driver, valOppERPLastIntStatus, 80);
            string status = driver.FindElement(valOppERPLastIntStatus).Text;
            return status;
        }

        public bool IsViewCounterpartyButtonOpportunityPageL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewCounterparty, 60);
            return driver.FindElement(btnViewCounterparty).Displayed;
        }

        public void ClickViewCounterpartyButtonOpportunityPageL()
        {
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewCounterparty, 60);
            driver.FindElement(btnViewCounterparty).Click();
            //WebDriverWaits.WaitTillElementVisible(driver, loader);
            Thread.Sleep(8000);
        }

        public string GetOpportunityJobTypeL()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtOppJobTypeL, 10);
                return driver.FindElement(txtOppJobTypeL).Text;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void CloseOpprtunityTabL(string name)
        {
            By buttonCloseTab = By.XPath($"//button[contains(@title,'Close {name}')]");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, alertDuplicate, 5);
                driver.FindElement(alertDuplicate).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, buttonCloseTab, 10);
                driver.FindElement(buttonCloseTab).Click();
            }
            catch (Exception e)
            {
                Thread.Sleep(2000);
                WebDriverWaits.WaitUntilEleVisible(driver, buttonCloseTab, 10);
                driver.FindElement(buttonCloseTab).Click();
                Thread.Sleep(2000);
            }
        }

        public void UpdateReqFieldsForCFConversionLV(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Console.WriteLine("path:" + excelPath);
            string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboClientOwnershipL, 10);
            string valClient = ReadExcelData.ReadData(excelPath, "AddOpportunity", 18);
            driver.FindElement(comboClientOwnershipL).SendKeys(valClient);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//flexipage-field[3]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valClient + "']")).Click();

            string valSubject = ReadExcelData.ReadData(excelPath, "AddOpportunity", 19);
            driver.FindElement(comboSubjectOwnershipL).SendKeys(valSubject);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div/slot/flexipage-field[4]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valSubject + "']")).Click();

            //Enter SIC
            //driver.FindElement(txtSICL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 20));
            //Thread.Sleep(3000);
            //driver.FindElement(comboSICL).Click();

            //Opp Desc
            WebDriverWaits.WaitUntilEleVisible(driver, txtOppDescL, 20);
            driver.FindElement(txtOppDescL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));

            //Estimated Fees
            driver.FindElement(txtRetainerL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtTailExpiresL).SendKeys("07/01/2023");
            driver.FindElement(txtMonthlyL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtContigentL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            //Ref Contact
            string valRef = ReadExcelData.ReadData(excelPath, "AddOpportunity", 22);
            if (valJobType == "Lender Education")
            {
                By refContactL = By.XPath("//flexipage-component2[9]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field/slot/record_flexipage-record-field/div/span/slot/records-record-layout-lookup/lightning-lookup/lightning-lookup-desktop/lightning-grouped-combobox/div/div/lightning-base-combobox/div/div[1]/input");
                CustomFunctions.MoveToElement(driver, driver.FindElement(refContactL));
                driver.FindElement(refContactL).SendKeys(valRef);
            }
            else
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(txtRefContactL));
                driver.FindElement(txtRefContactL).SendKeys(valRef);
            }

            Thread.Sleep(5000);
            driver.FindElement(comboRefContactL).Click();

            //Select Beneficial Owner
            string valWomen = ReadExcelData.ReadData(excelPath, "AddOpportunity", 6);            
            string valBenOwner = ReadExcelData.ReadData(excelPath, "AddOpportunity", 10);
            driver.FindElement(comboUpdBenOwnerL).SendKeys(valBenOwner);
            driver.FindElement(By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();

            driver.FindElement(txtEstTxnSizeL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtEstCloseDateL).SendKeys("07/01/2023");
            driver.FindElement(btnWomenLedL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valWomen + "']")).Click();

            ////Select Conf Agreement
            //string valConf = ReadExcelData.ReadData(excelPath, "AddOpportunity", 23);
            //Thread.Sleep(4000);
            //driver.FindElement(btnConfAgreeL).Click();
            //driver.FindElement(btnConfAgreeL).SendKeys(valConf);
            //Thread.Sleep(4000);
            //driver.FindElement(By.XPath("//lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valConf + "']")).Click();


            //Select Fairness
            Thread.Sleep(4000);
            driver.FindElement(btnFairnessL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span[text()='" + valWomen + "']")).Click();

            //Date Engaged            
            driver.FindElement(txtDateEngL).SendKeys("07/12/2022");
            Thread.Sleep(4000);
            driver.FindElement(btnSaveDetailsL).Click();
        }

        /*
        public void UpdateInternalTeamDetailsLV(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeamL, 20);
            driver.FindElement(tabInternalTeamL).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//iframe[@title='accessibility title']")));
            Thread.Sleep(5000);
            driver.FindElement(btnModifyRolesL).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(10000);
            By internalTeamFrame = By.XPath("//article/div[2]/div/iframe");
            WebDriverWaits.WaitUntilEleVisible(driver, internalTeamFrame, 20);
            driver.SwitchTo().Frame(driver.FindElement(internalTeamFrame));
            string name = ReadExcelData.ReadData(excelPath, "StandardUsers", 1);
            WebDriverWaits.WaitUntilEleVisible(driver, txtStaffL, 20);
            driver.FindElement(txtStaffL).SendKeys(name);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, name);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkInitiatorL, 240);
            //driver.FindElement(chkInitiatorL).Click();
            driver.FindElement(chkSellerL).Click();
            driver.FindElement(chkPrincipalL).Click();
            driver.FindElement(chkManagerL).Click();
            driver.FindElement(chkAssociateL).Click();
            driver.FindElement(chkAnalystL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveTeamL, 240);
            driver.FindElement(btnSaveTeamL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, tabEngTeamL, 320);
            driver.FindElement(tabEngTeamL).Click();
        }*/

        //Update Internal Team members details
        public void UpdateInternalTeamDetailsLV(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeamL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tabInternalTeamL));
            driver.FindElement(tabInternalTeamL).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamDetailPage));
            Thread.Sleep(5000);
            driver.FindElement(btnModifyRolesL).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(10000);

            By internalTeamFrame = By.XPath("//iframe[contains(@src,'InternalTeamModifyView')]");  //article/div[2]/div/iframe"); 
            driver.SwitchTo().Frame(driver.FindElement(internalTeamFrame));

            string name = ReadExcelData.ReadData(excelPath, "AddOpportunity", 14);
            WebDriverWaits.WaitUntilEleVisible(driver, txtStaffL, 20);
            driver.FindElement(txtStaffL).SendKeys(name);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, name);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkInitiatorL, 20);
            driver.FindElement(chkSellerL).Click();
            driver.FindElement(chkPrincipalL).Click();
            driver.FindElement(chkManagerL).Click();
            driver.FindElement(chkAssociateL).Click();
            driver.FindElement(chkAnalystL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveTeamL, 20);
            driver.FindElement(btnSaveTeamL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
        }

        public string ClickManageRelationshipsLV()
        {
            try
            {
                Thread.Sleep(8000);
                WebDriverWaits.WaitUntilEleVisible(driver, btnManageRelationship, 20);
                driver.FindElement(btnManageRelationship).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButonL, 10);
                driver.FindElement(iconExpandMoreButonL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnManageRelationship, 20);
                driver.FindElement(btnManageRelationship).Click();
            }
            By frameMassRelationship = By.XPath("//div[@class='oneAlohaPage']//iframe");
            Thread.Sleep(15000);
            WebDriverWaits.WaitUntilEleVisible(driver, frameMassRelationship, 20);
            driver.SwitchTo().Frame(driver.FindElement(frameMassRelationship));
            By txtMangeRelationshipHeader = By.XPath("//span[@id='j_id0:j_id1']/form/table/tbody/tr/td/label");
            return driver.FindElement(txtMangeRelationshipHeader).Text.Replace("\r\n", " ");
        }

        public string GetNBCApprovedStatus()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, checkboxNBC, 20);
                return driver.FindElement(checkboxNBC).GetAttribute("title");
            }
            catch (Exception ex) { return "Not Checked"; }



        }

        public string GetDetailPageJobTypeLV()
        {
            Thread.Sleep(4000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, txtPreviousJobType, 20);
                return driver.FindElement(txtPreviousJobType).Text;
            }
            catch (Exception) { return "Job Type not found"; }
        }

        public void UpdateJobTypeLV(string oldJobType, string newJobType)
        {

            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblHLSectorIDL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblHLSectorIDL));// lblSICCode))
            By btnJobTypeL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordJob_Type')]//button[@data-value='" + oldJobType + "']");
            WebDriverWaits.WaitUntilEleVisible(driver, btnJobTypeL, 20);
            driver.FindElement(btnJobTypeL).Click();
            Thread.Sleep(3000);
            By eleJobType = By.XPath("//flexipage-field[contains(@data-field-id,'RecordJob_Type')]//lightning-base-combobox//div[2]//lightning-base-combobox-item//span[text()='" + newJobType + "']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleJobType));
            WebDriverWaits.WaitUntilEleVisible(driver, eleJobType, 20);
            driver.FindElement(eleJobType).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
        }
        /*
        public void UpdateReqFieldsForCFConversionLV2(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;            
            string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboClientOwnershipL, 10);
            string valClient = ReadExcelData.ReadData(excelPath, "AddOpportunity", 18);
            driver.FindElement(comboClientOwnershipL).Click();// SendKeys(valClient);
            Thread.Sleep(3000);
            By eleClientOwnership = By.XPath($"//label[text()='Client Ownership']/following::lightning-base-combobox-item//span[@title='{valClient}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleClientOwnership));
            driver.FindElement(eleClientOwnership).Click();//flexipage-field[3]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valClient + "']")).Click();

            string valSubject = ReadExcelData.ReadData(excelPath, "AddOpportunity", 19);
            driver.FindElement(comboSubjectOwnershipL).SendKeys(valSubject);
            Thread.Sleep(3000);
            By eleSubjectOwnership = By.XPath($"//label[text()='Subject Ownership']/following::lightning-base-combobox-item//span[@title='{valSubject}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleSubjectOwnership));
            driver.FindElement(eleSubjectOwnership).Click();//div/slot/flexipage-field[4]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valSubject + "']")).Click();

            //Enter SIC
            string valSICCode = ReadExcelData.ReadData(excelPath, "AddOpportunity", 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtOppDescL2));
            driver.FindElement(txtSICL).SendKeys(valSICCode);
            Thread.Sleep(3000);
            By eleSICCode = By.XPath($"//label[text()='SIC Code']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{valSICCode}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleSICCode, 10);
            driver.FindElement(eleSICCode).Click();

            //Opp Desc
            WebDriverWaits.WaitUntilEleVisible(driver, txtOppDescL2, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtOppDescL2));
            driver.FindElement(txtOppDescL2).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));

            //Estimated Fees
            string valRetainerL = ReadExcelData.ReadData(excelPath, "AddOpportunity", 15);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblFeeNotesDes));
            driver.FindElement(txtRetainerL).SendKeys(valRetainerL);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtTailExpiresL));
            driver.FindElement(txtTailExpiresL).SendKeys("07/01/2023");
            driver.FindElement(txtMonthlyL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtContigentL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));

            //Ref Contact
            string valRef = ReadExcelData.ReadData(excelPath, "AddOpportunity", 22);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblAdditionalClient));

            if (valJobType == "Lender Education")
            {
                driver.FindElement(txtRefContactL2).SendKeys(valRef);
                Thread.Sleep(4000);
                By refContactL = By.XPath($"//label[text()='Referral Contact']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{valRef}']"); //flexipage-component2[9]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field/slot/record_flexipage-record-field/div/span/slot/records-record-layout-lookup/lightning-lookup/lightning-lookup-desktop/lightning-grouped-combobox/div/div/lightning-base-combobox/div/div[1]/input");
                CustomFunctions.MoveToElement(driver, driver.FindElement(refContactL));
                driver.FindElement(refContactL).Click();
            }
            else
            {
                //CustomFunctions.MoveToElement(driver, driver.FindElement(btnAdditionalClient));
                driver.FindElement(txtRefContactL2).SendKeys(valRef);
                Thread.Sleep(4000);
                By refContactL = By.XPath($"//label[text()='Referral Contact']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{valRef}']"); //flexipage-component2[9]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field/slot/record_flexipage-record-field/div/span/slot/records-record-layout-lookup/lightning-lookup/lightning-lookup-desktop/lightning-grouped-combobox/div/div/lightning-base-combobox/div/div[1]/input");
                CustomFunctions.MoveToElement(driver, driver.FindElement(refContactL));
                driver.FindElement(refContactL).Click();
            }
            Thread.Sleep(5000);
            string valWomen = ReadExcelData.ReadData(excelPath, "AddOpportunity", 6);

            //Select Beneficial Owner
            //string valBenOwner = ReadExcelData.ReadData(excelPath, "AddOpportunity", 10);
            //driver.FindElement(comboUpdBenOwnerL).SendKeys(valBenOwner);
            //driver.FindElement(By.XPath("//flexipage-column2[1]/div/slot/flexipage-field[1]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item[2]/span[2]/span")).Click();

            driver.FindElement(txtEstTxnSizeL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtEstCloseDateL).SendKeys("07/01/2023");
            driver.FindElement(btnWomenLedL).Click();
            Thread.Sleep(4000);

            driver.FindElement(By.XPath("//label[text()='Women Led']/following::lightning-base-combobox-item//span[text()='" + valWomen + "']")).Click();

            ////Select Conf Agreement
            //string valConf = ReadExcelData.ReadData(excelPath, "AddOpportunity", 23);
            //Thread.Sleep(4000);
            //driver.FindElement(btnConfAgreeL).Click();
            //driver.FindElement(btnConfAgreeL).SendKeys(valConf);
            //Thread.Sleep(4000);
            //driver.FindElement(By.XPath("//lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valConf + "']")).Click();

            //Select Fairness
            Thread.Sleep(4000);
            driver.FindElement(btnFairnessL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//label[text()='Fairness Opinion Component']/following::lightning-base-combobox-item//span[text()='" + valWomen + "']")).Click();

            //Date Engaged            
            driver.FindElement(txtDateEngL).SendKeys("07/12/2022");
            Thread.Sleep(4000);
            driver.FindElement(btnSaveDetailsL).Click();
        }*/
        public void UpdateTASServices()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 20);
            driver.FindElement(btnEdit).Click();
            Thread.Sleep(2000);
            driver.FindElement(comboTASServices).SendKeys("Buyside AFR");
            driver.FindElement(btnSave).Click();
        }

        public bool IsLinkedActivityDisplayed(string activity)
        {
            Thread.Sleep(5000);
            try

            {
                WebDriverWaits.WaitUntilEleVisible(driver, tabOppActivity, 20);
                driver.FindElement(tabOppActivity).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, _ActivitySubject(activity), 20);
                return driver.FindElement(_ActivitySubject(activity)).Displayed;
            }
            catch { return false; }
        }

        public int AddOppMultipleDealTeamMembers(string RecordType, string role, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 20);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(chkInternalTeamPrompt).Click();
            driver.FindElement(btnSave).Click();
            int rowCount = ReadExcelData.GetRowCount(excelPath, "OppDealTeamMembers");
            int totalDealTeamMemberadded = 0;
            for (int row = 2; row <= rowCount; row++)
            {
                string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", row, 1);
                Thread.Sleep(5000);
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 20);
                    driver.FindElement(txtStaff).SendKeys(valStaff);
                    Thread.Sleep(5000);
                    CustomFunctions.MultiSelectValueWithoutSelect(driver, listStaff, valStaff);
                    Thread.Sleep(2000);
                    if (RecordType == "CF")
                    {
                        if (role == "Specialty")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkCFSpeciality, 20);
                            driver.FindElement(checkCFSpeciality).Click();
                        }
                        if (role == "Analyst")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkCFAnalyst, 20);
                            driver.FindElement(checkCFAnalyst).Click();
                        }
                        if (role == "Associate")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkCFAssociate, 20);
                            driver.FindElement(checkCFAssociate).Click();
                        }
                    }
                    else
                    {
                        if (role == "Specialty")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkSpeciality, 20);
                            driver.FindElement(checkSpeciality).Click();
                        }
                        if (role == "Analyst")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkCFSpeciality, 20);
                            driver.FindElement(checkCFSpeciality).Click();
                        }
                        if (role == "Associate")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkCFAnalyst, 20);
                            driver.FindElement(checkCFAnalyst).Click();
                        }
                    }
                    CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveITTeam));
                    driver.FindElement(btnSaveITTeam).Click();
                    Thread.Sleep(5000);
                    totalDealTeamMemberadded = row - 1;
                }
                catch (Exception)
                {
                    return row - 1;
                }
            }
            return totalDealTeamMemberadded;
        }
        public void ClickReturnToOpportunityL()
        {
            driver.SwitchTo().DefaultContent();
            By btnReturnToOpp = By.XPath("//span[contains(@id,'internalTeam')]//input[@value='Return To Opportunity']");
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamModifyPage));
            Thread.Sleep(2000);
            driver.FindElement(btnReturnToOpp).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 30);
        }

        public void UpdateJobType(string oldJobType, string newJobType)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(4000);
            By btnJobTypeL = By.XPath("//flexipage-field[contains(@data-field-id,'RecordJob_Type')]//button[@data-value='" + oldJobType + "']");
            WebDriverWaits.WaitUntilEleVisible(driver, btnJobTypeL, 20);
            driver.FindElement(btnJobTypeL).Click();
            Thread.Sleep(3000);
            By eleJobType = By.XPath("//flexipage-field[contains(@data-field-id,'RecordJob_Type')]//lightning-base-combobox//div[2]//lightning-base-combobox-item//span[text()='" + newJobType + "']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleJobType));
            WebDriverWaits.WaitUntilEleVisible(driver, eleJobType, 20);
            driver.FindElement(eleJobType).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
        }
        By iconInlineEditNBCCheckBox = By.XPath("//records-record-layout-item[@field-label='NBC Approved']//dd//button");
        By chkNBCApprovedL = By.XPath("//records-record-layout-item[@field-label='NBC Approved']//input");
        
        public void UpdateNBCApprovalLV()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            WebDriverWaits.WaitUntilEleVisible(driver, iconInlineEditNBCCheckBox, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblAssociatedAddL));
            driver.FindElement(iconInlineEditNBCCheckBox).Click();
            Thread.Sleep(5000);
            //driver.FindElement(chkNBCApprovedL).Click();
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(chkNBCApprovedL));//Need to move 
            Thread.Sleep(2000);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(10000);
        }
        public void UpdateReqFieldsForCFConversionLV2(string file, string valJobType)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            //string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboClientOwnershipL, 10);
            string valClient = ReadExcelData.ReadData(excelPath, "AddOpportunity", 18);
            driver.FindElement(comboClientOwnershipL).Click();// SendKeys(valClient);
            Thread.Sleep(3000);
            By eleClientOwnership = By.XPath($"//label[text()='Client Ownership']/following::lightning-base-combobox-item//span[@title='{valClient}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleClientOwnership));
            driver.FindElement(eleClientOwnership).Click();//flexipage-field[3]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valClient + "']")).Click();

            string valSubject = ReadExcelData.ReadData(excelPath, "AddOpportunity", 19);
            driver.FindElement(comboSubjectOwnershipL).SendKeys(valSubject);
            Thread.Sleep(3000);
            By eleSubjectOwnership = By.XPath($"//label[text()='Subject Ownership']/following::lightning-base-combobox-item//span[@title='{valSubject}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleSubjectOwnership));
            driver.FindElement(eleSubjectOwnership).Click();//div/slot/flexipage-field[4]/slot/record_flexipage-record-field/div/span/slot/records-record-picklist/records-form-picklist/lightning-picklist/lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valSubject + "']")).Click();

            //Enter SIC
            //string valSICCode = ReadExcelData.ReadData(excelPath, "AddOpportunity", 20);
            //CustomFunctions.MoveToElement(driver, driver.FindElement(txtOppDescL2));
            //driver.FindElement(txtSICL).SendKeys(valSICCode);
            //Thread.Sleep(3000);
            //By eleSICCode = By.XPath($"//label[text()='SIC Code']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{valSICCode}']");
            //WebDriverWaits.WaitUntilEleVisible(driver, eleSICCode, 10);
            //driver.FindElement(eleSICCode).Click();

            //Opp Desc
            WebDriverWaits.WaitUntilEleVisible(driver, txtOppDescL2, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtOppDescL2));
            driver.FindElement(txtOppDescL2).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));

            //Estimated Fees
            string valRetainerL = ReadExcelData.ReadData(excelPath, "AddOpportunity", 15);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblFeeNotesDes));
            driver.FindElement(txtRetainerL).SendKeys(valRetainerL);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtTailExpiresL));
            driver.FindElement(txtTailExpiresL).SendKeys("07/01/2023");
            driver.FindElement(txtMonthlyL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtContigentL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));

            //New required fields for CF conversion 
            driver.FindElement(txtSharedServExpL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtEstimatedCapL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtLegalCapL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15)); 

            //Ref Contact
            string valRef = ReadExcelData.ReadData(excelPath, "AddOpportunity", 22);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblAdditionalClient));
            if (valJobType == "Lender Education")
            {
                driver.FindElement(txtRefContactL2).SendKeys(valRef);
                Thread.Sleep(4000);
                By refContactL = By.XPath($"//label[text()='Referral Contact']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{valRef}']"); //flexipage-component2[9]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field/slot/record_flexipage-record-field/div/span/slot/records-record-layout-lookup/lightning-lookup/lightning-lookup-desktop/lightning-grouped-combobox/div/div/lightning-base-combobox/div/div[1]/input");
                CustomFunctions.MoveToElement(driver, driver.FindElement(refContactL));
                driver.FindElement(refContactL).Click();
            }
            else
            {
                driver.FindElement(txtRefContactL2).SendKeys(valRef);
                Thread.Sleep(4000);
                By refContactL = By.XPath($"//label[text()='Referral Contact']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{valRef}']"); //flexipage-component2[9]/slot/flexipage-field-section2/div/div/div/laf-progressive-container/slot/div/slot/flexipage-column2[2]/div/slot/flexipage-field/slot/record_flexipage-record-field/div/span/slot/records-record-layout-lookup/lightning-lookup/lightning-lookup-desktop/lightning-grouped-combobox/div/div/lightning-base-combobox/div/div[1]/input");
                CustomFunctions.MoveToElement(driver, driver.FindElement(refContactL));
                driver.FindElement(refContactL).Click();
            }
            string valWomen = ReadExcelData.ReadData(excelPath, "AddOpportunity", 6);
            if (valJobType == "Sellside")
            {
                driver.FindElement(txtEBITDAL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
                Thread.Sleep(2000);
            }
            //Date Engaged
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnWomenLedL));
            driver.FindElement(txtDateEngL).SendKeys("10/12/2022");
            Thread.Sleep(4000);
            //Funds & Financials
            driver.FindElement(txtEstTxnSizeL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            //string closeDate = DateTime.Today.AddDays(2).ToString("MM/dd/yyyy");
            driver.FindElement(txtEstCloseDateL).SendKeys("07/01/2023");

            //Select Fairness
            Thread.Sleep(4000);
            driver.FindElement(btnFairnessL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//label[text()='Fairness Opinion Component']/following::lightning-base-combobox-item//span[text()='" + valWomen + "']")).Click();

            //WomenLed
            CustomFunctions.MoveToElement(driver, driver.FindElement(labelESGLV));//Available for James Craven
            driver.FindElement(btnWomenLedL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//label[text()='Women Led']/following::lightning-base-combobox-item//span[text()='" + valWomen + "']")).Click();

            //Select Conf Agreement
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtLegalHoldNotes));
            string valConf = ReadExcelData.ReadData(excelPath, "AddOpportunity", 23);
            driver.FindElement(comboConfAggL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath($"//label[text()='Confidentiality Agreement']/following::lightning-base-combobox-item//span[@title='{valConf}']")).Click();

            //New fields for for CF conversion
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtConflictsRun));
            driver.FindElement(comboIndemLngL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath($"//label[text()='Indemnification Language']/following::lightning-base-combobox-item//span[@title='No']")).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(10000);
        }

        public bool IsViewCounterpartyButton()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, buttonViewCounterparty, 60);
            return driver.FindElement(buttonViewCounterparty).Displayed;
        }

        public void NavigateToCoverageSectorDependenciesPage()
        {
            driver.FindElement(shwAllTab).Click();
            Thread.Sleep(2000);
            driver.FindElement(imgCoverageSectorDependencies).Click();
            Thread.Sleep(2000);
        }

        public void SaveNewOpportunitySectorDetails()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveOpportunitySector, 120);
            driver.FindElement(btnSaveOpportunitySector).Click();
            Thread.Sleep(2000);
        }
        public string GetOpportunitySectorName()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valOpportunitySectorName, 120);
            string name = driver.FindElement(valOpportunitySectorName).Text;
            return name;
        }

        public bool VerifyFiltersFunctionalityOnCoverageSectorDependencyPopUp(string file, string covSectorDependencyName)
        {
            bool result = false; ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;            
            //Click Edit button on Company Sector detail page 
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditCompCoverageSector, 120);
            driver.FindElement(btnEditCompCoverageSector).Click();
            Thread.Sleep(2000);             
            //Click on Coverage Sector Dependency LookUp icon
            WebDriverWaits.WaitUntilEleVisible(driver, imgCoverageSectorDependencyLookUp, 120);
            driver.FindElement(imgCoverageSectorDependencyLookUp).Click();
            Thread.Sleep(2000);             
            // Switch to second window
            CustomFunctions.SwitchToWindow(driver, 1);
            Thread.Sleep(2000);             
            //Enter search frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("searchFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='searchFrame']")));
            Thread.Sleep(2000);             
            //Clear Search box
            driver.FindElement(txtSearchBox).Clear(); driver.SwitchTo().DefaultContent();             
            //Enter results frame
            WebDriverWaits.WaitUntilEleVisible(driver, By.Id("resultsFrame"));
            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='resultsFrame']")));
            Thread.Sleep(2000);            
            //Click on Show Filters link
            //driver.FindElement(linkShowFilters).Click();             
            //Enter filter values
            driver.FindElement(inputCoverageType).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 1));
            driver.FindElement(inputPrimarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 2));
            driver.FindElement(inputSecondarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 3));
            driver.FindElement(inputTertiarySector).SendKeys(ReadExcelData.ReadData(excelPath, "CoverageSectorDependency", 4));             //Click on Apply filters button
            driver.FindElement(btnApplyFilters).Click();
            Thread.Sleep(2000); 
            if (driver.FindElement(linkCoverageSectorDependencyName).Text == covSectorDependencyName)
            {
                //Select the desired dependency name from the result
                driver.FindElement(linkCoverageSectorDependencyName).Click();
                Thread.Sleep(4000);                 
                //Switch back to original window
                CustomFunctions.SwitchToWindow(driver, 0); result = true;
            }
            return result;
        }

        public void NavigateToOpportunityDetailPageFromOpportunitySectorDetailPage()
        {
            Thread.Sleep(2000);
            WebDriverWaits.WaitUntilEleVisible(driver, linkOpportunityName, 120);
            driver.FindElement(linkOpportunityName).Click();
            Thread.Sleep(2000);
        }


        public bool VerifyOpportunitySectorAddedToOpportunityOrNot(string sectorName)
        {
            Thread.Sleep(5000);
            bool result = false;
            if (driver.FindElement(linkSectorName).Text == sectorName)
            {
                result = true;
            }
            return result;
        }

        //Validate Comments tab
        public string ValidateCommentsTabL()
        {
            Thread.Sleep(4000);
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, tabOpp, 260);
            driver.FindElement(tabOpp).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnRecentlyViewed, 350);
            driver.FindElement(btnRecentlyViewed).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//div[1]/div/ul/li[9]")).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, valRec1st, 240);
            driver.FindElement(valRec1st).Click();
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabComments, 110);
            string name = driver.FindElement(tabComments).Text;
            return name;
        }


        //Validate Edit tab
        public string ValidateEditTabL()
        {
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditTopPanelL);
            string name = driver.FindElement(btnEditTopPanelL).Text;
            driver.FindElement(btnEditTopPanelL).Click();
            return name;
        }
        //Click Return to Opp
        public void ClickReturnToOpp()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 90);
            driver.FindElement(btnReturnToOpp).Click();
        }

        //Validate View Counterparties tab
        public string ValidateViewCounterpartiesTabL()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnViewCounterpartiesL);
            string name = driver.FindElement(btnViewCounterpartiesL).Text;
            driver.FindElement(btnViewCounterpartiesL).Click();
            return name;
        }


        //Add Opportunity Comments
        public void AddOppCommentaAndValidate()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnComments, 150);
            driver.FindElement(btnComments).Click();
            Thread.Sleep(4000);
            driver.FindElement(valCommentsType).Click();
            driver.FindElement(txtCommentNotes).SendKeys("Testing");
            driver.FindElement(btnSaveComments).Click();
            Thread.Sleep(4000);
            //WebDriverWaits.WaitUntilEleVisible(driver, valAddedCommentType, 170);
            //string commentType = driver.FindElement(valAddedCommentType).Text;
            //return commentType;          
        }

        //Get added Opportunity comments
        public string GetOppCommentsL()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valAddedComment, 160);
            string comment = driver.FindElement(valAddedComment).Text;
            return comment;
        }

        //Validate File upload option under Files
        public string ValidateFileUploadsOption()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,300)");
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtUploadFiles, 190);
            string comment = driver.FindElement(txtUploadFiles).Text;
            return comment;
        }

        //Upload a file and validate the same
        public string UploadFileAndValidate(string path)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            js.ExecuteScript("window.scrollTo(0,350)");
            //WebDriverWaits.WaitUntilEleVisible(driver, PanelCSTFiles, 30);
            CustomFunctions.FileUpload(driver, path);
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, toastMsgPopup, 100);
            return driver.FindElement(toastMsgPopup).Text;

            //WebDriverWaits.WaitUntilEleVisible(driver, txtUploadFiles, 160);
            //driver.FindElement(txtUploadFiles).Click();

        }

        //Leave any mandatory field blank and get the validation
        public string GetMandatoryFieldValidationOfGeneral()
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtClearOppName, 100);
            driver.FindElement(txtClearOppName).Clear();
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnCloseL, 250);
            driver.FindElement(btnCloseL).Click();
            Thread.Sleep(3000);
            string message = driver.FindElement(msgOppName).Text;
            return message;
        }

        //Update the data and validate the same
        public string UpdateDataAndValidate(string name)
        {
            driver.FindElement(txtClearOppName).SendKeys(name);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(6000);
            string value = driver.FindElement(valOppNameL).Text;
            return value;
        }

        //Click Opp Name tab
        public void ClickOppName()
        {
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabOppNameL, 150);
            driver.FindElement(tabOppNameL).Click();
        }

        //Click Add CF Opprotunity Contact 
        public string ClickAddCFOppContact()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnAddCFContact, 150);
            driver.FindElement(btnAddCFContact).Click();
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, titleAddCFOppContact, 150);
            string title = driver.FindElement(titleAddCFOppContact).Text;
            return title;
        }

        //Validate Request Engagement button
        public string ValidateRequestEngButton()
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkReqEngL, 350);
            driver.FindElement(lnkReqEngL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnReqEngL, 350);
            string value = driver.FindElement(btnReqEngL).Displayed.ToString();
            return value;
        }

        public string ClickReqToEngagement()
        {
            Thread.Sleep(4000);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,-500)");
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lnkReqEngL, 350);
            driver.FindElement(lnkReqEngL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnConvertToEngL, 350);
            driver.FindElement(btnConvertToEngL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, lblEngagement, 350);
            string value = driver.FindElement(lblEngagement).Text;
            return value;
        }

        public void UpdateInternalTeamDetails2(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valUser = ReadExcelData.ReadData(excelPath, "Users", 1);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 350);
            driver.FindElement(btnEdit).Click();
            driver.FindElement(chkInternalTeamPrompt).Click();
            driver.FindElement(btnSave).Click();
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, chkUpPrincipal, 130);
                //Check required checkboxes         
                driver.FindElement(chkUpPrincipal).Click();
                Thread.Sleep(2000);
                driver.FindElement(chkUpSeller).Click();
                driver.FindElement(chkUpManager).Click();
                driver.FindElement(chkUpMgr).Click();
                driver.FindElement(chkUpAssociate).Click();
                driver.FindElement(btnSaveITTeam).Click();
                Thread.Sleep(3000);

                //Click to return back to Opportunity details
                WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 90);
                driver.FindElement(btnReturnToOpp).Click();
            }
            catch (Exception)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, lnkReDisplayRec, 100);
                driver.FindElement(lnkReDisplayRec).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnEdit, 100);
                driver.FindElement(btnEdit).Click();
                driver.FindElement(chkInternalTeamPrompt).Click();
                driver.FindElement(btnSave).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, chkUpPrincipal, 130);

                //Check required checkboxes         
                driver.FindElement(chkUpPrincipal).Click();
                driver.FindElement(chkUpSeller).Click();
                driver.FindElement(chkUpManager).Click();
                driver.FindElement(chkUpMgr).Click();
                driver.FindElement(chkUpAssociate).Click();

                driver.FindElement(btnSaveITTeam).Click();

                //Click to return back to Opportunity details
                WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 60);
                driver.FindElement(btnReturnToOpp).Click();
            }
        }


        public string UpdateStagePriorityLV(string valStagePriority)
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 60);
            driver.FindElement(btnEditL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, ComboStagePriorityL, 20);
            driver.FindElement(ComboStagePriorityL).Click();            

            By eleClientOwnership = By.XPath($"//label[text()='Stage/Priority']/following::lightning-base-combobox-item//span[@title='{valStagePriority}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleClientOwnership));
            driver.FindElement(eleClientOwnership).Click();
            Thread.Sleep(2000);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtEBITDAL));
            driver.FindElement(txtEBITDAL).SendKeys("10");
            driver.FindElement(btnSaveDetailsL).Click();
            DateTime currentDate = DateTime.Today;
            string dateStageChange = currentDate.ToString("MM/dd/yyyy");
            Thread.Sleep(5000);
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
            return dateStageChange;

        }

        public string GetOppDateEngagedLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabOppAdministratorL, 20);
            driver.FindElement(tabOppAdministratorL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, txtDateEngaged, 20);
            return driver.FindElement(txtDateEngaged).Text;
        }

        //Validate Reuired fields validation to Request Opportunity for Engagement
        public string GetActualRequiredFieldsValidationForConversionLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, popupError, 10);
            Thread.Sleep(2000);
            string actualErrorsList = driver.FindElement(txtErrorList).Text;
            string formatedActualErrorsList = Regex.Replace(actualErrorsList, @"\t|\n|\r", "");
            driver.FindElement(iconCloseErrorL).Click();
            return formatedActualErrorsList;
        }
        public string GetExpectedRequiredFieldsValidationForConversionLV(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valueExl = ReadExcelData.ReadDataMultipleRows(excelPath, "ErrorsList", 2, 1);
            string formatedvalueExl = Regex.Replace(valueExl, @"\\", "");
            return formatedvalueExl;
        }

        public void NavigaterToClientSubjectTabLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabOppClientSubjectRefL, 20);
            driver.FindElement(tabOppClientSubjectRefL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, tabOppaddClientSubjectL, 20);
        }
        public string GetPrimaryCheckboxOfClientCompanyLV()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, lnkAdditionalClientL, 20);
            //driver.FindElement(lnkAdditionalClientL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, chkboxPrimaryL, 20);
            bool Enabled = driver.FindElement(chkboxPrimaryL).Enabled;
            //driver.FindElement(btnTabCloseOppClientSubjectL).Click();
            if (Enabled)
            {
                return "Checked";
            }
            else
            {
                return "Not Checked";
            }
        }

        public string GetPrimaryCheckboxOfSubjectCompanyLV()
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, lnkAdditionalSubjectL, 20);
            //driver.FindElement(lnkAdditionalSubjectL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, chkboxPrimaryL, 20);
            bool Enabled = driver.FindElement(chkboxPrimaryL).Enabled;
            //driver.FindElement(btnTabCloseOppClientSubjectL).Click();
            if (Enabled)
            {
                return "Checked";
            }
            else
            {
                return "Not Checked";
            }
        }

        public string ValidateUserIfExistsLV(string file)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabOppInternalTeamL, 20);
            driver.FindElement(tabOppInternalTeamL).Click();
            ReadJSONData.Generate("Admin_Data.json");
            Console.WriteLine("Entered staff function");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(5000);
            string valStaff = ReadExcelData.ReadData(excelPath, "AddOpportunity", 14);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamDetailPage));
            WebDriverWaits.WaitUntilEleVisible(driver, txtDealTeamMember, 20);
            string deamMember = driver.FindElement(txtDealTeamMember).Text;
            driver.SwitchTo().DefaultContent();
            if (deamMember == valStaff)
            {
                return "User exists";
            }
            else
            {
                return "User does not exist";
            }
        }

        public string RemoveUserFromITTeamLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeamL, 20);
            driver.FindElement(tabInternalTeamL).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamDetailPage));
            driver.FindElement(btnModifyRolesL).Click();
            Thread.Sleep(10000);
            driver.SwitchTo().DefaultContent();
            By internalTeamFrame = By.XPath("//iframe[contains(@src,'InternalTeamModifyView')]");
            WebDriverWaits.WaitUntilEleVisible(driver, internalTeamFrame, 20);
            driver.SwitchTo().Frame(driver.FindElement(internalTeamFrame));
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkUpPrincipal1, 20);

            driver.FindElement(chkUpManager1).Click();
            driver.FindElement(chkUpSeller1).Click();
            driver.FindElement(chkUpAssociate1).Click();
            driver.FindElement(chkUpAnalyst1).Click();
            driver.FindElement(chkUpPrincipal1).Click();
            driver.FindElement(chkCheckedInitiator).Click();

            driver.FindElement(txtStaff).SendKeys("Sonika Goyal");
            Thread.Sleep(3000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, "Sonika Goyal");
            WebDriverWaits.WaitUntilEleVisible(driver, chkAdmin2, 20);
            driver.FindElement(chk2ndInitiator).Click();

            driver.FindElement(btnSaveITTeam).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgHLIntTeam, 20);
            string message = driver.FindElement(msgHLIntTeam).Text.Replace("\r\n", " ");

            //Click to return back to Opportunity details
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 20);
            driver.FindElement(btnReturnToOpp).Click();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, tabInfo);
            return message;
        }
        By iconInlineEditTDConfirmed = By.XPath("(//button[@title='Edit Total Debt (MM) Confirmed'])[1]");
        By chkTDConfirmed1 = By.XPath("(//input[@name='TotalDebtMMConfirmed__c'])[1]");
        public void UpdateTotalDebtConfirmedLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            driver.FindElement(tabInfo).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, iconInlineEditTDConfirmed, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(iconInlineEditTDConfirmed));
            driver.FindElement(iconInlineEditTDConfirmed).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, chkTotalDebtConfMML, 10);
            try
            {
                driver.FindElement(chkTDConfirmed1).Click();
            }
            catch
            {
                driver.FindElement(chkTDConfirmed1).Click();
            }
            driver.FindElement(btnSaveDetailsL).Click();
           Thread.Sleep(5000);            
        }
        public void UpdateReqFieldsForFRConversionLV(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Console.WriteLine("path:" + excelPath);
            string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboClientOwnershipL, 10);
            string valClient = ReadExcelData.ReadData(excelPath, "AddOpportunity", 18);
            driver.FindElement(comboClientOwnershipL).Click();
            Thread.Sleep(3000);
            By eleClientOwnership = By.XPath($"//label[text()='Client Ownership']/following::lightning-base-combobox-item//span[@title='{valClient}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleClientOwnership));
            driver.FindElement(eleClientOwnership).Click();
            string valSubject = ReadExcelData.ReadData(excelPath, "AddOpportunity", 19);
            driver.FindElement(comboSubjectOwnershipL).SendKeys(valSubject);
            Thread.Sleep(3000);
            By eleSubjectOwnership = By.XPath($"//label[text()='Subject Ownership']/following::lightning-base-combobox-item//span[@title='{valSubject}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleSubjectOwnership));
            driver.FindElement(eleSubjectOwnership).Click();

            //Enter SIC
            //string valSICCode = ReadExcelData.ReadData(excelPath, "AddOpportunity", 20);
            //CustomFunctions.MoveToElement(driver, driver.FindElement(txtOppDescL2));
            //driver.FindElement(txtSICL).SendKeys(valSICCode);
            //Thread.Sleep(3000);
            //By eleSICCode = By.XPath($"//label[text()='SIC Code']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{valSICCode}']");
            //WebDriverWaits.WaitUntilEleVisible(driver, eleSICCode, 10);
            //driver.FindElement(eleSICCode).Click();

            //Opp Desc
            WebDriverWaits.WaitUntilEleVisible(driver, txtOppDescL2, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtOppDescL2));
            driver.FindElement(txtOppDescL2).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));

            //Estimated Fees
            string valRetainerL = ReadExcelData.ReadData(excelPath, "AddOpportunity", 15);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblFeeNotesDes));
            driver.FindElement(txtRetainerL).SendKeys(valRetainerL);
            driver.FindElement(txtMonthlyL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            driver.FindElement(txtContigentL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboLegalAdvisorL));
            driver.FindElement(txtTotalDebtRepMML).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            //WebDriverWaits.WaitUntilEleVisible(driver, chkTotalDebtConfMML, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtClientDescL)); 
            //driver.FindElement(chkTotalDebtConfMML).Click();

            //Clien Desc      
            driver.FindElement(txtClientDescL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));

            //LegalAdvisor  
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblAdditionalClient));
            string valRef = ReadExcelData.ReadData(excelPath, "AddOpportunity", 22);
            WebDriverWaits.WaitUntilEleVisible(driver, comboLegalAdvisorL, 10);
            Thread.Sleep(3000);
            string valLegalAdvisor = ReadExcelData.ReadData(excelPath, "AddOpportunity", 30);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(comboLegalAdvisorL));
            Thread.Sleep(3000);
            By elevalLegalAdvisor = By.XPath($"//label[text()='Legal Advisor to Company']/following::lightning-base-combobox-item//span[@title='{valLegalAdvisor}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(elevalLegalAdvisor));
            driver.FindElement(elevalLegalAdvisor).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, comboLegalAdvisorHLL, 10);
            string valcomboLegalAdvisorHL = ReadExcelData.ReadData(excelPath, "AddOpportunity", 31);
            js.ExecuteScript("arguments[0].click();", driver.FindElement(comboLegalAdvisorHLL));
            Thread.Sleep(3000);
            By elevalLegalAdvisorHL = By.XPath($"//label[text()='Legal Advisor to HL Client Group']/following::lightning-base-combobox-item//span[@title='{valcomboLegalAdvisorHL}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(elevalLegalAdvisorHL));
            driver.FindElement(elevalLegalAdvisorHL).Click();

            //Ref Contact
            //Select Referral Type //Need to move in UpdteReq function 

            string valRefType = ReadExcelData.ReadData(excelPath, "AddOpportunity", 8);
            //CustomFunctions.MoveToElement(driver, driver.FindElement(comboAddClientL));
            driver.FindElement(comboRefTypeL).Click();
            By eleReferralType = By.XPath($"//label[text()='Referral Type']/following::lightning-base-combobox-item//span[@title='{valRefType}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleReferralType, 80);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleReferralType));
            driver.FindElement(eleReferralType).Click();
            driver.FindElement(txtRefContactL2).SendKeys(valRef);
            Thread.Sleep(4000);
            By refContactL = By.XPath($"//label[text()='Referral Contact']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{valRef}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(refContactL));
            driver.FindElement(refContactL).Click();
            Thread.Sleep(5000);

            //driver.FindElement(comboRefContactL).Click();
            CustomFunctions.MoveToElement(driver, driver.FindElement(labelESGLV));
            string valWomen = ReadExcelData.ReadData(excelPath, "AddOpportunity", 6);

            //Date Engaged &Estimated Closed Date  
            driver.FindElement(txtDateEngL).SendKeys("10/12/2022");
            Thread.Sleep(2000);
            driver.FindElement(txtEstCloseDateL).SendKeys("10/11/2023");

            //WomenLed
            driver.FindElement(btnWomenLedL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//label[text()='Women Led']/following::lightning-base-combobox-item//span[text()='" + valWomen + "']")).Click();

            //EU Securities 
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboConfAggL));
            driver.FindElement(cmboEUSecuritiesL).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//label[contains(text(),'EU Securities')]/following::lightning-base-combobox-item//span[text()='" + valWomen + "']")).Click();

            ///Select Conf Agreement   
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblCAComments));//for FR
            string valConf = ReadExcelData.ReadData(excelPath, "AddOpportunity", 23);
            driver.FindElement(comboConfAggL).Click();// SendKeys(valConf);
            Thread.Sleep(4000);
            driver.FindElement(By.XPath($"//label[text()='Confidentiality Agreement']/following::lightning-base-combobox-item//span[@title='{valConf}']")).Click();//lightning-combobox/div/lightning-base-combobox/div/div[2]/lightning-base-combobox-item/span[2]/span[text()='" + valConf + "']")).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(5000);
        }
        public int AddOppMultipleDealTeamMembersLV(string RecordType, string role, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeamL, 30);
            driver.FindElement(tabInternalTeamL).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamDetailPage));
            Thread.Sleep(4000);
            driver.FindElement(btnModifyRolesL).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(6000);
            By internalTeamFrame = By.XPath("//iframe[contains(@src,'InternalTeamModifyView')]");
            WebDriverWaits.WaitUntilEleVisible(driver, internalTeamFrame, 20);
            driver.SwitchTo().Frame(driver.FindElement(internalTeamFrame));

            int rowCount = ReadExcelData.GetRowCount(excelPath, "OppDealTeamMembers");
            int totalDealTeamMemberadded = 0;
            for (int row = 2; row <= rowCount; row++)
            {
                string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", row, 1);
                Thread.Sleep(5000);
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 20);
                    driver.FindElement(txtStaff).SendKeys(valStaff);
                    Thread.Sleep(5000);
                    CustomFunctions.MultiSelectValueWithoutSelect(driver, listStaff, valStaff);
                    Thread.Sleep(2000);
                    if (RecordType == "CF")
                    {
                        if (role == "Specialty")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkCFSpeciality, 20);
                            driver.FindElement(checkCFSpeciality).Click();
                        }
                        if (role == "Analyst")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkCFAnalyst, 20);
                            driver.FindElement(checkCFAnalyst).Click();
                        }
                        if (role == "Associate")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkCFAssociate, 20);
                            driver.FindElement(checkCFAssociate).Click();
                        }
                    }
                    else
                    {
                        if (role == "Specialty")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkSpeciality, 20);
                            driver.FindElement(checkSpeciality).Click();
                        }
                        if (role == "Analyst")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkCFSpeciality, 20);
                            driver.FindElement(checkCFSpeciality).Click();
                        }
                        if (role == "Associate")
                        {
                            WebDriverWaits.WaitUntilEleVisible(driver, checkCFAnalyst, 20);
                            driver.FindElement(checkCFAnalyst).Click();
                        }
                    }
                    IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(btnSaveITTeam));
                    driver.FindElement(btnSaveITTeam).Click();
                    Thread.Sleep(5000);
                    totalDealTeamMemberadded = row - 1;
                }
                catch (Exception)
                {
                    return row - 1;
                }
            }
            driver.SwitchTo().DefaultContent();
            return totalDealTeamMemberadded;
        }

        public string ModifyInternalTeamMembersLV(string name)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeamL, 20);
            driver.FindElement(tabInternalTeamL).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamDetailPage));
            Thread.Sleep(5000);
            driver.FindElement(btnModifyRolesL).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(10000);

            By internalTeamFrame = By.XPath("//iframe[contains(@src,'InternalTeamModifyView')]");
            WebDriverWaits.WaitUntilEleVisible(driver, internalTeamFrame, 20);
            driver.SwitchTo().Frame(driver.FindElement(internalTeamFrame));
            WebDriverWaits.WaitUntilEleVisible(driver, txtStaffL, 20);
            driver.FindElement(txtStaffL).SendKeys(name);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, name);
            Thread.Sleep(5000);
            try
            {
                if (driver.FindElement(checkInitiator).Displayed.ToString().Equals("True"))
                    return "True";
                else
                    return "False";
            }
            catch (Exception)
            {
                return "False";
            }
        }



        //Verify Is DND On/Off button displayed
        public bool IsButtonDNDOnOffDisplayedLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnDNDOnOFF, 20);
                return driver.FindElement(btnDNDOnOFF).Displayed;
            }
            catch { return false; }
        }

        public void ClickDNDOnOffButtonLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnDNDOnOFF, 20);
            driver.FindElement(btnDNDOnOFF).Click();
            //Thread.Sleep(8000);
        }


        public string GetOpportunityNameL()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            Thread.Sleep(2000);
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtOppNameL, 20);
            return driver.FindElement(txtOppNameL).Text;
        }
        public string ValidateOpportunityNameL(string name)
        {
            if (name.StartsWith("DND"))
            {
                return "Opportunity Name " + name + " is updated with DND - upon approval on DND Submission ";
            }
            else
            {
                return "Opportunity Name " + name + " is not updated with DND - upon approval on DND Submission ";
            }
        }

        public bool IsButtonSharingDisplayedLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnSharingL, 10);
                if (driver.FindElement(btnSharingL).Displayed)
                {
                    driver.FindElement(btnSharingL).Click();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButonL, 10);
                driver.FindElement(iconExpandMoreButonL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnMoreSharingL, 10);
                if (driver.FindElement(btnMoreSharingL).Displayed)
                {
                    driver.FindElement(btnMoreSharingL).Click();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void ClickButtonSharingL()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);
            try
            {
                js.ExecuteScript("window.scrollTo(0,0)");
                WebDriverWaits.WaitUntilEleVisible(driver, btnSharingL, 10);
                driver.FindElement(btnSharingL).Click();
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButonL, 10);
                driver.FindElement(iconExpandMoreButonL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnMoreSharingL, 10);
                driver.FindElement(btnMoreSharingL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, btnMoreSharingL, 30);
            }
            WebDriverWaits.WaitUntilEleVisible(driver, txtEngHeader, 60);
        }



        public bool IsSharingGroupDisplayedLV(string value)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            try
            {
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, btnEditSharingGroup, 10);
                    driver.FindElement(btnEditSharingGroup).Click();
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(tblSharingGroup));
                    WebDriverWaits.WaitUntilEleVisible(driver, tblSharingGroup, 10);
                    return driver.FindElement(_sharingGroup(value)).Displayed;
                }
                catch
                {
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(tblSharingGroup));
                    WebDriverWaits.WaitUntilEleVisible(driver, tblSharingGroup, 10);
                    return driver.FindElement(_sharingGroup(value)).Displayed;

                }
            }
            catch { return false; }
        }

        public void CloseSharingGroupPopupLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnCancelSharingGroup, 10);
            driver.FindElement(btnCancelSharingGroup).Click();
        }


        public string RemoveUserFromITTeamLV(string userName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeamL, 20);
            driver.FindElement(tabInternalTeamL).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamDetailPage));
            driver.FindElement(btnModifyRolesL).Click();
            Thread.Sleep(10000);
            driver.SwitchTo().DefaultContent();
            By internalTeamFrame = By.XPath("//iframe[contains(@src,'InternalTeamModifyView')]");
            WebDriverWaits.WaitUntilEleVisible(driver, internalTeamFrame, 20);
            driver.SwitchTo().Frame(driver.FindElement(internalTeamFrame));
            Thread.Sleep(5000);
            By unCheckRole = By.XPath($"//input[contains(@name,':1:j_id44')][contains(@title,'{userName}')]");
            WebDriverWaits.WaitUntilEleVisible(driver, unCheckRole, 20);
            driver.FindElement(unCheckRole).Click();
            driver.FindElement(btnSaveITTeam).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgHLIntTeam, 20);
            string message = driver.FindElement(msgHLIntTeam).Text.Replace("\r\n", " ");
            //Click to return back to Opportunity details
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 60);
            driver.FindElement(btnReturnToOpp).Click();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, tabInfo);
            return message;
        }

        public bool IsSharingUserDisplayedLV(string value)
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, tblSharingGroup, 10);
                return driver.FindElement(_sharingGroup(value)).Displayed;
            }
            catch
            {
                string lower = value.ToLower();
                string[] name = lower.Split(' ');
                string newName = name[0] + "." + name[1];
                By txtname = By.XPath($"//div[contains(@class,'recordsRecordShare')]//table//tbody//tr//lightning-base-formatted-text[contains(text(),'{newName}')]");
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, txtname, 10);
                    js.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(txtname));
                    return driver.FindElement(txtname).Displayed;
                }
                catch { return false; }
            }
        }
        public void UpdateReqFieldsForFVAConversionLV(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valJobType = ReadExcelData.ReadDataMultipleRows(excelPath, "AddOpportunity", 2, 3);
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, comboClientOwnershipL, 10);
            string valClient = ReadExcelData.ReadData(excelPath, "AddOpportunity", 18);
            driver.FindElement(comboClientOwnershipL).Click();
            Thread.Sleep(3000);
            By eleClientOwnership = By.XPath($"//label[text()='Client Ownership']/following::lightning-base-combobox-item//span[@title='{valClient}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleClientOwnership));
            driver.FindElement(eleClientOwnership).Click();

            string valSubject = ReadExcelData.ReadData(excelPath, "AddOpportunity", 19);
            driver.FindElement(comboSubjectOwnershipL).SendKeys(valSubject);
            Thread.Sleep(3000);
            By eleSubjectOwnership = By.XPath($"//label[text()='Subject Ownership']/following::lightning-base-combobox-item//span[@title='{valSubject}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleSubjectOwnership));
            driver.FindElement(eleSubjectOwnership).Click();

            //Enter SIC
            //string valSICCode = ReadExcelData.ReadData(excelPath, "AddOpportunity", 20);
            //CustomFunctions.MoveToElement(driver, driver.FindElement(txtOppDescL2));
            //driver.FindElement(txtSICL).SendKeys(valSICCode);
            //Thread.Sleep(3000);
            //By eleSICCode = By.XPath($"//label[text()='SIC Code']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{valSICCode}']");
            //WebDriverWaits.WaitUntilEleVisible(driver, eleSICCode, 10);
            //driver.FindElement(eleSICCode).Click();

            //Opp Desc
            WebDriverWaits.WaitUntilEleVisible(driver, txtOppDescL2, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtOppDescL2));
            driver.FindElement(txtOppDescL2).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));

            //Tombstone Permission
            string tombstonePermissionExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 29);
            driver.FindElement(comboTombstonePermissionL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath($"//label[text()='Tombstone Permission']/following::lightning-base-combobox-item//span[text()='No Restrictions']")).Click();

            //Valuation Date
            WebDriverWaits.WaitUntilEleVisible(driver, txtvaluationDateL, 20);
            driver.FindElement(txtvaluationDateL).SendKeys("10/12/2022");

            //Estimated Fees
            string valRetainerL = ReadExcelData.ReadData(excelPath, "AddOpportunity", 15);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblFeeNotesDes));
            driver.FindElement(txtRetainerL).SendKeys(valRetainerL);
            string TotalAnticipatedRevenueExl = ReadExcelData.ReadData(excelPath, "AddOpportunity", 28);
            driver.FindElement(txtTotalAnticipatedRevenueL).SendKeys(TotalAnticipatedRevenueExl);

            //Ref Contact
            string valRef = ReadExcelData.ReadData(excelPath, "AddOpportunity", 22);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblAdditionalClient));
            driver.FindElement(txtRefContactL2).SendKeys(valRef);
            Thread.Sleep(4000);
            By refContactL = By.XPath($"//label[text()='Referral Contact']/following::ul//lightning-base-combobox-item//lightning-base-combobox-formatted-text[@title='{valRef}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(refContactL));
            driver.FindElement(refContactL).Click();

            string valWomen = ReadExcelData.ReadData(excelPath, "AddOpportunity", 6);
            //Date Engaged
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnWomenLedL));
            driver.FindElement(txtDateEngL).SendKeys("10/12/2022");
            Thread.Sleep(2000);

            //Funds & Financials
            driver.FindElement(txtEstTxnSizeL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));

            //WomenLed
            CustomFunctions.MoveToElement(driver, driver.FindElement(labelESGLV));//Available for James Craven
            driver.FindElement(btnWomenLedL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("//label[text()='Women Led']/following::lightning-base-combobox-item//span[text()='" + valWomen + "']")).Click();

            ////Select Conf Agreement
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblCAComments));
            string valConf = ReadExcelData.ReadData(excelPath, "AddOpportunity", 23);
            driver.FindElement(comboConfAggL).Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath($"//label[text()='Confidentiality Agreement']/following::lightning-base-combobox-item//span[@title='{valConf}']")).Click();

            driver.FindElement(btnSaveDetailsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            Thread.Sleep(8000);
        }
        public void UpdateTASServicesLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnWomenLedL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnWomenLedL));
            Thread.Sleep(1000);
            driver.FindElement(comboTASServicesL).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//label[text()='TAS Services']/..//following::lightning-base-combobox-item//span[@title='Buyside AFR']")).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(5000);
        }
        public bool IsBtnTASDNDDisplayedLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnTASDNDL, 10);
                bool btnDisplayed = driver.FindElement(btnTASDNDL).Displayed;
                return btnDisplayed;
            }
            catch (Exception e)
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButonL, 5);
                driver.FindElement(iconExpandMoreButonL).Click();
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, btnMoreTASDNDL, 5);
                    bool btnDisplayed = driver.FindElement(btnMoreTASDNDL).Displayed;
                    return btnDisplayed;
                }
                catch { return false; }
            }
        }
        public void UpdateInternalTeamDetailsLV(string name, string role)
        {
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeamL, 20);
            driver.FindElement(tabInternalTeamL).Click();
            Thread.Sleep(8000);
            WebDriverWaits.WaitUntilEleVisible(driver, frameInternalTeamDetailPage, 20);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamDetailPage));
            Thread.Sleep(5000);
            driver.FindElement(btnModifyRolesL).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(10000);

            By internalTeamFrame = By.XPath("//iframe[contains(@src,'InternalTeamModifyView')]");  //article/div[2]/div/iframe"); 
            //WebDriverWaits.WaitUntilEleVisible(driver, internalTeamFrame, 20);
            driver.SwitchTo().Frame(driver.FindElement(internalTeamFrame));
            WebDriverWaits.WaitUntilEleVisible(driver, txtStaffL, 20);
            driver.FindElement(txtStaffL).SendKeys(name);
            Thread.Sleep(5000);
            CustomFunctions.SelectValueWithoutSelect(driver, listStaff, name);
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, chkInitiatorL, 20);
            if (role == "Principal" || role == "PRINCIPAL")
            {
                driver.FindElement(chkPrincipalL).Click();
            }
            else
            {
                driver.FindElement(chkSellerL).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveTeamL, 20);
            driver.FindElement(btnSaveTeamL).Click();
            Thread.Sleep(5000);
            driver.SwitchTo().DefaultContent();
        }
        public void AddOppMultipleDealTeamMembersLV(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeamL, 30);
            driver.FindElement(tabInternalTeamL).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamDetailPage));
            Thread.Sleep(4000);
            driver.FindElement(btnModifyRolesL).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(6000);
            By internalTeamFrame = By.XPath("//iframe[contains(@src,'InternalTeamModifyView')]");
            WebDriverWaits.WaitUntilEleVisible(driver, internalTeamFrame, 20);
            driver.SwitchTo().Frame(driver.FindElement(internalTeamFrame));
            int rowCount = ReadExcelData.GetRowCount(excelPath, "OppDealTeamMembers");
            for (int row = 2; row <= rowCount; row++)
            {
                string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", row, 1);
                string role = ReadExcelData.ReadDataMultipleRows(excelPath, "OppDealTeamMembers", row, 4);
                Thread.Sleep(5000);
                WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 20);
                driver.FindElement(txtStaff).SendKeys(valStaff);
                Thread.Sleep(5000);
                CustomFunctions.MultiSelectValueWithoutSelect(driver, listStaff, valStaff);
                WebDriverWaits.WaitUntilEleVisible(driver, chkInitiatorL2, 20);
                if (role == "Principal" || role == "PRINCIPAL")
                {
                    driver.FindElement(chkPrincipalL2).Click();
                }
                else
                {
                    driver.FindElement(chkSellerL2).Click();
                }
                WebDriverWaits.WaitUntilEleVisible(driver, btnSaveTeamL, 20);
                driver.FindElement(btnSaveTeamL).Click();
                Thread.Sleep(5000);
            }
        }
        public bool IsTASDNDFieldDisplayedLV()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, labelTASDNDL, 20);
                return driver.FindElement(labelTASDNDL).Displayed;
            }
            catch { return false; }
        }
        public string GetTASDNDCheckBoxStatusLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, chkTASDNDL, 10);
            bool enabled = driver.FindElement(chkTASDNDL).Selected;
            if (enabled)
            {
                return "Checked";
            }
            else
            {
                return "Not Checked";
            }
        }
        public void ClickBtnTASDNDLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnTASDNDL, 5);
                driver.FindElement(btnTASDNDL).Click();
            }
            catch {
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, btnMoreTASDNDL, 5);
                    driver.FindElement(btnMoreTASDNDL).Click();
                }
                catch {
                    WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButonL, 5);
                    driver.FindElement(iconExpandMoreButonL).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, btnMoreTASDNDL, 5);
                    driver.FindElement(btnMoreTASDNDL).Click();
                }
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnDNDConfirmL, 10);
            driver.FindElement(btnDNDConfirmL).Click();
            Thread.Sleep(10000);
        }
        public void AddOppDealTeamMembersLV(string valStaff, string role)
        {
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeamL, 30);
            driver.FindElement(tabInternalTeamL).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamDetailPage));
            Thread.Sleep(4000);
            driver.FindElement(btnModifyRolesL).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(6000);
            By internalTeamFrame = By.XPath("//iframe[contains(@src,'InternalTeamModifyView')]");
            WebDriverWaits.WaitUntilEleVisible(driver, internalTeamFrame, 20);
            driver.SwitchTo().Frame(driver.FindElement(internalTeamFrame));
            Thread.Sleep(5000);
            WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 20);
            driver.FindElement(txtStaff).SendKeys(valStaff);
            Thread.Sleep(5000);
            CustomFunctions.MultiSelectValueWithoutSelect(driver, listStaff, valStaff);
            WebDriverWaits.WaitUntilEleVisible(driver, chkInitiatorL2, 20);
            if (role == "Principal" || role == "PRINCIPAL")
            {
                driver.FindElement(chkPrincipalL2).Click();
            }
            else
            {
                driver.FindElement(chkSellerL2).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveTeamL, 20);
            driver.FindElement(btnSaveTeamL).Click();
            Thread.Sleep(5000);
        }
        public void AddOppMultipleDealTeamMembersLV2(string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            Thread.Sleep(7000);
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeamL, 30);
            driver.FindElement(tabInternalTeamL).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamDetailPage));
            Thread.Sleep(4000);
            driver.FindElement(btnModifyRolesL).Click();
            driver.SwitchTo().DefaultContent();
            Thread.Sleep(6000);
            By internalTeamFrame = By.XPath("//iframe[contains(@src,'InternalTeamModifyView')]");
            WebDriverWaits.WaitUntilEleVisible(driver, internalTeamFrame, 20);
            driver.SwitchTo().Frame(driver.FindElement(internalTeamFrame));
            int rowCount = ReadExcelData.GetRowCount(excelPath, "NewDealTeamMembers");
            for (int row = 2; row <= rowCount; row++)
            {
                string valStaff = ReadExcelData.ReadDataMultipleRows(excelPath, "NewDealTeamMembers", row, 1);
                string role = ReadExcelData.ReadDataMultipleRows(excelPath, "NewDealTeamMembers", row, 4);
                //Thread.Sleep(5000);
                WebDriverWaits.WaitUntilEleVisible(driver, txtStaff, 20);
                driver.FindElement(txtStaff).SendKeys(valStaff);
                Thread.Sleep(5000);
                CustomFunctions.MultiSelectValueWithoutSelect(driver, listStaff, valStaff);
                WebDriverWaits.WaitUntilEleVisible(driver, chkInitiatorL2, 20);
                if (role == "Principal" || role == "PRINCIPAL")
                {
                    driver.FindElement(chkPrincipalL2).Click();
                }
                else
                {
                    driver.FindElement(chkSellerL2).Click();
                }
                WebDriverWaits.WaitUntilEleVisible(driver, btnSaveTeamL, 20);
                driver.FindElement(btnSaveTeamL).Click();
                Thread.Sleep(8000);
            }
        }
        public string ClickBtnTASDNDAndGetValidationLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, btnTASDNDL, 5);
                driver.FindElement(btnTASDNDL).Click();
            }
            catch
            {
                try
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, btnMoreTASDNDL, 5);
                    driver.FindElement(btnMoreTASDNDL).Click();
                }
                catch
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, iconExpandMoreButonL, 5);
                    driver.FindElement(iconExpandMoreButonL).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, btnMoreTASDNDL, 5);
                    driver.FindElement(btnMoreTASDNDL).Click();
                }
            }
            WebDriverWaits.WaitUntilEleVisible(driver, msgLVPopup, 30);
            return driver.FindElement(msgLVPopup).Text;
        }

        public string GetOppStage()
        {            
            WebDriverWaits.WaitUntilEleVisible(driver, txtOppStage, 20);
            return driver.FindElement(txtOppStage).Text;   
        }
        
        public void CloseConversionPopup()
        {
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, iconCloseConversionPopup, 10);
                driver.FindElement(iconCloseConversionPopup).Click();
            }
            catch
            {
                //No Pop-up Displaying
            }
        }

        public string RemoveUserFromEngagedOppITTeamLV(string userName)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabInternalTeamL, 20);
            driver.FindElement(tabInternalTeamL).Click();
            Thread.Sleep(8000);
            driver.SwitchTo().Frame(driver.FindElement(frameInternalTeamDetailPage));
            driver.FindElement(btnModifyRolesL).Click();
            Thread.Sleep(10000);
            driver.SwitchTo().DefaultContent();
            By internalTeamFrame = By.XPath("//iframe[contains(@src,'InternalTeamModifyView')]");
            WebDriverWaits.WaitUntilEleVisible(driver, internalTeamFrame, 20);
            driver.SwitchTo().Frame(driver.FindElement(internalTeamFrame));
            Thread.Sleep(5000);
            By unCheckRole = By.XPath($"//input[contains(@name,':1:j_id44')][contains(@title,'{userName}')]");
            WebDriverWaits.WaitUntilEleVisible(driver, unCheckRole, 20);
            driver.FindElement(unCheckRole).Click();
            driver.FindElement(btnSaveITTeam).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, msgHLIntTeam, 20);
            string message = driver.FindElement(msgHLIntTeam).Text.Replace("\r\n", " ");
            //Click to return back to Opportunity details
            WebDriverWaits.WaitUntilEleVisible(driver, btnReturnToOpp, 60);
            driver.FindElement(btnReturnToOpp).Click();
            driver.SwitchTo().DefaultContent();
            WebDriverWaits.WaitUntilEleVisible(driver, tabInfo);
            return message;
        }

        public string GetOppNumberLV()
        {
            //Thread.Sleep(4000);
            WebDriverWaits.WaitUntilEleVisible(driver, valOppNumL, 10);
            string oppNum = driver.FindElement(valOppNumL).Text;
            return oppNum;
        }
        public string GetClientLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtClientNameL, 10);
            return driver.FindElement(txtClientNameL).Text;
        }
        public string GetSubjectLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtSubjectNameL, 10);
            return driver.FindElement(txtSubjectNameL).Text;
        }
        public string GetJobTypeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtJobTypeL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtJobTypeL));
            return driver.FindElement(txtJobTypeL).Text;
        }

        public void UpdateField(string fieldName, string fieldValue)
        {
            switch (fieldName)
            {
                case "Primary Office":
                    WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
                    CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditL));
                    driver.FindElement(btnEditL).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, headerEditBox, 20);

                    CustomFunctions.MoveToElement(driver, driver.FindElement(lblWomenLedL));
                    driver.FindElement(comboPrimaryOfficeL).Click();
                    By elePO = By.XPath($"//label[text()='Primary Office']/..//lightning-base-combobox-item//span[@title='{fieldValue}']");
                    CustomFunctions.MoveToElement(driver, driver.FindElement(elePO));
                    WebDriverWaits.WaitUntilEleVisible(driver, elePO, 20);
                    CustomFunctions.MoveToElement(driver, driver.FindElement(elePO));
                    driver.FindElement(elePO).Click();
                    driver.FindElement(btnSaveL).Click();
                    Thread.Sleep(10000);
                    break;
            }

        }

        public void UpdatePrimaryOfficeLV(string value)
        {
            Thread.Sleep(6000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditL));
            driver.FindElement(btnEditL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerEditBox, 20);

            CustomFunctions.MoveToElement(driver, driver.FindElement(lblWomenLedL));
            driver.FindElement(comboPrimaryOfficeL).Click();
            By elePO = By.XPath($"//label[text()='Primary Office']/..//lightning-base-combobox-item//span[@title='{value}']");
            CustomFunctions.MoveToElement(driver, driver.FindElement(elePO));
            WebDriverWaits.WaitUntilEleVisible(driver, elePO, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(elePO));
            driver.FindElement(elePO).Click();

            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
        }

        public void UpdateJobTypeLV(string jobType)
        {
           // Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditL));
            driver.FindElement(btnEditL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerEditBox, 20);
            WebDriverWaits.WaitUntilEleVisible(driver, btnJobTypeL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnJobTypeL));
            driver.FindElement(btnJobTypeL).Click();
            Thread.Sleep(2000);
            By eleJobType = By.XPath($"//label[text()='Job Type']/..//lightning-base-combobox-item//span[@title='{jobType}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleJobType, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleJobType));
            driver.FindElement(eleJobType).Click();
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
        }
        public void UpdateClientOwnershipLV(string client)
        {
            //Thread.Sleep(70000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditL));
            driver.FindElement(btnEditL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerEditBox, 20);
            WebDriverWaits.WaitUntilEleVisible(driver, comboClientOwnershipL, 10);
            driver.FindElement(comboClientOwnershipL).Click();
            Thread.Sleep(2000);
            By eleClientOwnership = By.XPath($"//label[text()='Client Ownership']/..//lightning-base-combobox-item//span[@title='{client}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleClientOwnership, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleClientOwnership));
            driver.FindElement(eleClientOwnership).Click();
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(5000);
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
        }
        public string GetClientOwnershipLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valClientOwnershipL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valClientOwnershipL));
            string clientOwnership = driver.FindElement(valClientOwnershipL).Text;
            return clientOwnership;
        }

        public void UpdateRecordTypeLV(string recordType, string jobType)
        {
            //Thread.Sleep(60000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnChangeRecordTypeL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnChangeRecordTypeL));
            driver.FindElement(btnChangeRecordTypeL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerChangeRT, 20);
            driver.FindElement(_elmRecordType(recordType)).Click();
            driver.FindElement(btnChangeRTNextL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerEditBox, 20);
            driver.FindElement(btnLOBL).Click();
            //Thread.Sleep(2000);
            By eleLOB = By.XPath($"//label[text()='Line of Business']/..//lightning-base-combobox-item//span[@title='{recordType}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleLOB, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleLOB));
            driver.FindElement(eleLOB).Click();
            //Thread.Sleep(2000);
            driver.FindElement(btnJobTypeL).Click();
            By eleJobType = By.XPath($"//label[text()='Job Type']/..//lightning-base-combobox-item//span[@title='{jobType}']");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, eleJobType, 5);
                CustomFunctions.MoveToElement(driver, driver.FindElement(eleJobType));
            }
            catch
            {
                driver.FindElement(btnJobTypeL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, eleJobType, 5);
                CustomFunctions.MoveToElement(driver, driver.FindElement(eleJobType));
            }
            driver.FindElement(eleJobType).Click();

            if (recordType == "FVA")
            {
                CustomFunctions.MoveToElement(driver, driver.FindElement(txtEstFee));
                driver.FindElement(txtEstFee).SendKeys("10000");
            }

            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 20);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(8000);
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
        }
        public string GetRecordTypeLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valRecordTypeL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(valRecordTypeL));
            return driver.FindElement(valRecordTypeL).Text;
        }
        public void UpdateHLSectorIDLV(string sector)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditL));
            driver.FindElement(btnEditL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerEditBox, 20);            
            WebDriverWaits.WaitUntilEleVisible(driver, btnClearHLSectionL, 10);
            WebDriverWaits.WaitUntilEleVisible(driver, lblIBL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblIBL));
            Thread.Sleep(2000);
            driver.FindElement(btnClearHLSectionL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, inputHLSectorIDL, 20);
            driver.FindElement(inputHLSectorIDL).Click();
            driver.FindElement(inputHLSectorIDL).SendKeys(sector);
            WebDriverWaits.WaitUntilEleVisible(driver, listHLSectorL, 20);
            driver.FindElement(listHLSectorL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 20);
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(10000);
            driver.Navigate().Refresh();
            Thread.Sleep(10000);
        }

        public bool IsJobTypePresentInDropdownOppDetailPageLV(string JobType)
        {
            //WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 10);
            //driver.FindElement(btnEditL).Click();
            //WebDriverWaits.WaitUntilEleVisible(driver, headerEditBox, 20);
            WebDriverWaits.WaitUntilEleVisible(driver, btnJobTypeL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnJobTypeL));
            driver.FindElement(btnJobTypeL).Click();
            Thread.Sleep(2000);
            bool isFound = false;

            IReadOnlyCollection<IWebElement> valTypes = driver.FindElements(optionsJobTypeL);
            var actualValue = valTypes.Select(x => x.Text).ToArray();
            if (actualValue.Length <= 0)
            {
                driver.FindElement(btnJobTypeL).Click();
                Thread.Sleep(2000);
            }
            for (int row = 0; row < actualValue.Length; row++)
            {
                if (actualValue[row].Contains(JobType))
                {
                    isFound = true;
                    break;
                }
            }
            driver.FindElement(comboClientOwnershipL).Click();
            driver.FindElement(btnCancelL).Click();
            //WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            return isFound;
        }
        
        public void UpdateCCOutcomeDetailsLV()
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            DateTime Time = DateTime.Now;
            string dateToday = Time.ToString("MM/dd/yyyy");
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 10);
            WebDriverWaits.WaitUntilEleVisible(driver, btnInlineEditCCOutComeL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnInlineEditCCOutComeL));
            driver.FindElement(btnInlineEditCCOutComeL).Click();            
            CustomFunctions.MoveToElement(driver, driver.FindElement(comboPrimaryOfficeL));
            driver.FindElement(dateOutcomeDateL).Click();
            driver.FindElement(dateOutcomeDateL).Clear();
            driver.FindElement(dateOutcomeDateL).SendKeys(dateToday);
            Thread.Sleep(1000);
            //driver.FindElement(comboOutcomeL).Click();
            jse.ExecuteScript("arguments[0].click();", driver.FindElement(comboOutcomeL));
            Thread.Sleep(1000);
            By eleOptOutcome = By.XPath("//label[text()='Outcome']/..//lightning-base-combobox-item//span[@title='Cleared']");
            driver.FindElement(eleOptOutcome).Click();                        
            driver.FindElement(btnSaveL).Click();
            Thread.Sleep(10000);
        }        
        public void EditOpportunityStageLV(string stage)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditL));
            driver.FindElement(btnEditL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerEditBox, 20);
            WebDriverWaits.WaitUntilEleVisible(driver, ComboStagePriorityL, 20);
            driver.FindElement(ComboStagePriorityL).Click();
            Thread.Sleep(1000);
            By eleStage = By.XPath($"//lightning-base-combobox-item/span[2]/span[text()='{stage}']");
            try
            {
                WebDriverWaits.WaitUntilEleVisible(driver, eleStage, 5);
                CustomFunctions.MoveToElement(driver, driver.FindElement(eleStage));
            }
            catch
            {
                driver.FindElement(ComboStagePriorityL).Click();
                WebDriverWaits.WaitUntilEleVisible(driver, eleStage, 5);
                CustomFunctions.MoveToElement(driver, driver.FindElement(eleStage));
            }

            driver.FindElement(eleStage).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveL));
            driver.FindElement(btnSaveL).Click();
            //Thread.Sleep(2000);
            //WebDriverWaits.WaitTillElementVisible(driver, iconLoadSpinner);
            Thread.Sleep(20000);
        }

        public void ClickSaveEditOpportunityPageLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, btnSaveL, 10);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnSaveL));
            driver.FindElement(btnSaveL).Click();
        }

        public string GetOppVEValidationErrorsLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, popHitaSang, 10);
            Thread.Sleep(2000);
            string pageLevelError = driver.FindElement(txtPageLevelError).Text;
            string formatedpageLevelError = Regex.Replace(pageLevelError, @"\t|\n|\r", "");            

            IList<IWebElement> fieldLevelErrors = driver.FindElements(txtFieldLevelErrors);
            string formatedFieldLevelErrors = "";
            foreach (IWebElement txtFieldLevelError in fieldLevelErrors)
            {
                string fieldLevelError = txtFieldLevelError.Text;
                string formatedfieldLevelError = Regex.Replace(fieldLevelError, @"\t|\n|\r", "");
                formatedFieldLevelErrors = formatedFieldLevelErrors + formatedfieldLevelError;
            }

            string finalErroList = formatedpageLevelError + formatedFieldLevelErrors;
            driver.FindElement(iconCloseErrorL).Click();
            return finalErroList;
        }

        public void EnterVerballyEngagedRequiredFieldsLV(string jobType, string file)
        {
            ReadJSONData.Generate("Admin_Data.json");
            string dir = ReadJSONData.data.filePaths.testData;
            string excelPath = dir + file;
            string valJobType = jobType;
            Thread.Sleep(10000);
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            driver.FindElement(btnEditL).Click();
            Thread.Sleep(2000);

            //Opportunity Description
            WebDriverWaits.WaitUntilEleVisible(driver, txtOppDescL2, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtOppDescL2));
            driver.FindElement(txtOppDescL2).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 21));

            //Contingent Fee/
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblFeeNotesDes));
            driver.FindElement(txtContigentL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));

            // EBITDA(MM)
            string valWomen = ReadExcelData.ReadData(excelPath, "AddOpportunity", 6);
            if (valJobType == "Sellside"|| valJobType== "Buyside")
            {
                driver.FindElement(txtEBITDAL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
                //Thread.Sleep(2000);
            }

            //Estimated Close Date & Est. Transaction Size / Market Cap (MM)
            driver.FindElement(txtEstTxnSizeL).SendKeys(ReadExcelData.ReadData(excelPath, "AddOpportunity", 15));
            string dateClose = DateTime.Today.AddDays(10).ToString("dd/MM/yyyy");
            driver.FindElement(txtEstCloseDateL).SendKeys(dateClose);

            //WomenLed
            CustomFunctions.MoveToElement(driver, driver.FindElement(labelESGLV));//Available for James Craven
            driver.FindElement(btnWomenLedL).Click();
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//label[text()='Women Led']/following::lightning-base-combobox-item//span[text()='" + valWomen + "']")).Click();
            Thread.Sleep(1000);
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(10000);
        }
        
        public void ByPassCCNBCLV()
        {
            CustomFunctions.MoveToElement(driver, driver.FindElement(lblAssAddL));
            Thread.Sleep(2000);
            driver.FindElement(btnInlineEditNBCL).Click();
            Thread.Sleep(2000);
            driver.FindElement(chkCCBypassL).Click();
            Thread.Sleep(2000);
            driver.FindElement(chkNBCBypassL).Click();
            driver.FindElement(btnSaveDetailsL).Click();
            Thread.Sleep(10000);
        
        }
        
        public string GetStageLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, valStageL, 20);
            Thread.Sleep(2000);
            string stage = driver.FindElement(valStageL).Text;
            return stage;
        }
        
        public void UpdateVEOpportunityDescription()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWaits.WaitUntilEleVisible(driver, btnEditL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnEditL));
            driver.FindElement(btnEditL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, headerEditBox, 20);
            //Opp Desc
            WebDriverWaits.WaitUntilEleVisible(driver, txtOppDescL2, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(txtOppDescL2));
            driver.FindElement(txtOppDescL2).SendKeys("Test VE Opportunity Description");
            driver.FindElement(btnSaveL).Click();
        }
        private By _btnAddContactL(string lob)
        {
            return By.XPath($"//button[contains(@name,'Add_{lob}_Opportunity_Contact')]");
        }

        By tabFSOppL = By.XPath("//lightning-tab-bar/ul/li/a[text()='FS Opps']");
        By lnkMoretabFSOppL = By.XPath("//lightning-tab-bar/ul/li/lightning-button-menu//a/span[text()='FS Engagements']");
        By iconHeaderMoreTabsL = By.XPath("(//lightning-tab-bar/ul/li/lightning-button-menu/button[@title='More Tabs'])[1]");
        By btnNewFSOppL = By.XPath("//article[@aria-label='FS Opportunities']//button[@name='New']");

        By comboCommentTypeL = By.XPath("//button[@aria-label='Comment Type']");
        By tabCommentsL = By.XPath("//div[contains(@class,'sidebar-right')]//li[@title='Comments']");
        By inputCommentL = By.XPath("//label[text()='Comment']/..//textarea");
        By btnSidebarSave = By.XPath("//div[contains(@class,'sidebar-right')]//button[@name='save']");
        By btnOppCommentsL = By.XPath("//article[@aria-label='Comments']//lightning-button-menu//button");
        By linkNewOppCommentL = By.XPath("//article[@aria-label='Comments']//a//span[text()='New']");
        By buttonSaveL = By.XPath("//button[@name='SaveEdit']");
        By txtCommentsIDL = By.XPath("//h1//records-entity-label[text()='Opportunity Comment']/../../..//lightning-formatted-text");
        public void ClickOppNewCommentsLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, tabCommentsL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(tabCommentsL));
            driver.FindElement(tabCommentsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, btnOppCommentsL, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnOppCommentsL));
            Thread.Sleep(2000);
            driver.FindElement(btnOppCommentsL).Click();
            WebDriverWaits.WaitUntilEleVisible(driver, linkNewOppCommentL, 20);
            driver.FindElement(linkNewOppCommentL).Click();
        }
        public void AddNewOppCommentLV(string commentType, string commentText)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, comboCommentTypeL, 20);
            driver.FindElement(comboCommentTypeL).Click();
            By eleType = By.XPath($"//label[text()='Comment Type']/..//lightning-base-combobox-item//span[@title='{commentType}']");
            WebDriverWaits.WaitUntilEleVisible(driver, eleType, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(eleType));
            driver.FindElement(eleType).Click();
            Thread.Sleep(2000);
            driver.FindElement(inputCommentL).SendKeys(commentText);
            driver.FindElement(buttonSaveL).Click();
            //Thread.Sleep(5000);
        }

        public string GetCommentIDLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtCommentsIDL, 20);
            return driver.FindElement(txtCommentsIDL).Text;
        }
        public void ClickTabFSOpportunityLV()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(1000);
            try
            {
                try
                {
                    js.ExecuteScript("window.scrollTo(0,0)");
                    WebDriverWaits.WaitUntilEleVisible(driver, tabFSOppL, 5);
                    driver.FindElement(tabFSOppL).Click();
                }
                catch (Exception e)
                {
                    WebDriverWaits.WaitUntilEleVisible(driver, iconHeaderMoreTabsL, 5);
                    driver.FindElement(iconHeaderMoreTabsL).Click();
                    WebDriverWaits.WaitUntilEleVisible(driver, lnkMoretabFSOppL, 5);
                    driver.FindElement(lnkMoretabFSOppL).Click();
                }
            }
            catch { }

            Thread.Sleep(10000);
        }

        //public string CreateNewFSEngagementLV(string sponsor)
        //{
        //    WebDriverWaits.WaitUntilEleVisible(driver, btnNewFSOppL, 10);
        //    driver.FindElement(btnNewFSOppL).Click();
        //    Thread.Sleep(2000);
        //    WebDriverWaits.WaitUntilEleVisible(driver, inputSponsorCompanyL, 10);
        //    driver.FindElement(inputSponsorCompanyL).Click();
        //    driver.FindElement(inputSponsorCompanyL).SendKeys(sponsor);
        //    Thread.Sleep(2000);
        //    WebDriverWaits.WaitUntilEleVisible(driver, optionSponsorCompanyL, 10);
        //    driver.FindElement(optionSponsorCompanyL).Click();
        //    Thread.Sleep(2000);
        //    driver.FindElement(btnSaveDetailsL).Click();
        //    //Thread.Sleep(2000);
        //    WebDriverWaits.WaitUntilEleVisible(driver, txtFSEngNameL, 10);
        //    return driver.FindElement(txtFSEngNameL).Text;
        //}

        //public string GetFSEngagementIDLV()
        //{
        //    WebDriverWaits.WaitUntilEleVisible(driver, txtFSEngIDL, 10);
        //    return driver.FindElement(txtFSEngIDL).Text;
        //}

        public void CickAddOpportunityContactLV(string RecordType)
        {
            Thread.Sleep(3000);
            WebDriverWaits.WaitUntilEleVisible(driver, _btnAddContactL(RecordType), 20);
            driver.FindElement(_btnAddContactL(RecordType)).Click();
        }
        public void CreateContactLV(string name, string party)
        {
            WebDriverWaits.WaitUntilEleVisible(driver, txtContactL, 20);
            driver.FindElement(txtContactL).SendKeys(name);
            Thread.Sleep(3000);
            try
            {
                By listContactOption = By.XPath($"//div[@role='listbox']//ul//li//a//div[2]//div[1][@title='{name}']");
                WebDriverWaits.WaitUntilEleVisible(driver, listContactOption, 5);
                driver.FindElement(listContactOption).Click();
            }
            catch (Exception ex)
            {
                By iconContactSearchItem = By.XPath("//div[contains(@class,'searchButton')]");
                WebDriverWaits.WaitUntilEleVisible(driver, iconContactSearchItem, 5);
                driver.FindElement(iconContactSearchItem).Click();
                By txtContact = By.XPath("//div[contains(@class,'gridInScroller')]//table//tbody//tr[1]//td[1]//a");
                WebDriverWaits.WaitUntilEleVisible(driver, txtContact, 20);
                driver.FindElement(txtContact).Click();
            }
            WebDriverWaits.WaitUntilEleVisible(driver, btnPartyL, 20);
            driver.FindElement(btnPartyL).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div[8]/div/ul/li/a[text()='" + party + "']")).Click();
            driver.FindElement(btnSaveContactL).Click();
            Thread.Sleep(5000);
        }
        By linkEng = By.XPath("//article[@aria-label='Engagements']//article//h3//a/../..");
        public void ClickEngagementLinkLV()
        {
            WebDriverWaits.WaitUntilEleVisible(driver, linkEng, 20);
            CustomFunctions.MoveToElement(driver, driver.FindElement(linkEng));
            driver.FindElement(linkEng).Click();
        }

        public bool VerifyActivityIsLinkedToOpportunity(string sub)
        {
            bool result = false;
            driver.FindElement(btnMore).Click();
            Thread.Sleep(2000);
            try
            {
                driver.FindElement(By.XPath("(//button[@title='More Tabs'])[1]")).Click();
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("((//button[@title='More Tabs'])[1]/following::lightning-menu-item//a)[1]")).Click();
                Thread.Sleep(2000);
            }
            catch (Exception)
            {
                driver.FindElement(By.XPath("(//a[text()='Activity'])[2]")).Click();
                Thread.Sleep(2000);
            }

            if(driver.FindElement(By.XPath($"(//a[text()='{sub}'])[3]")).Displayed)
            {
                result = true;
            }
            return result;
        }

        public void ViewActivityFromList(string name)
        {
            Thread.Sleep(2000);
            CustomFunctions.ActionClick(driver, driver.FindElement(By.XPath($"(//a[text()='{name}'])[3]")), 60);
            Thread.Sleep(3000);
        }

        public void DeleteActivity()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor) driver;
            js.ExecuteScript("window.scrollTo(0,0)");
            Thread.Sleep(2000);

            WebDriverWaits.WaitUntilEleVisible(driver, btnDeleteActivity, 60);
            CustomFunctions.MoveToElement(driver, driver.FindElement(btnDeleteActivity));
            driver.FindElement(btnDeleteActivity).Click();
            Thread.Sleep(2000);
        }


    }
}


