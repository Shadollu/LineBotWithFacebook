using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MerchantDB.Models;
using System.Threading.Tasks;

namespace MerchantDB.Controllers
{
    public class MerchantController : Controller
    {
        private TestDB2Entities db = new TestDB2Entities();


        // GET: Merchant
        public async Task<ActionResult> Index(string searchString, string OptionValue)

        {
            var merchant = db.正式商店設定資料.Include(M => M.ACQUIRE_BANK).Include(M => M.ACQUIRE_MODE).Include(M => M.Alipay_Product1).Include(M => M.AUTO_SETTLE_TIME).Include(M => M.HOSTS).Include(M => M.HOSTS).Include(M => M.MALL_TYPE).Include(M => M.MER_STATE).Include(M => M.MER_TYPE).Include(M => M.OBJ_VER).Include(M => M.OnCmpData).Include(M => M.SETTLE_CYCLE).Include(M => M.客戶基本資料);


            if (!string.IsNullOrEmpty(searchString))
            {
                switch (OptionValue)
                {
                    case "CustName":
                        merchant = merchant.Where(s => s.CustName.Contains(searchString)).Distinct();
                        break;
                    case "Merno_F":
                        merchant = merchant.Where(s => s.MerNo_F.Contains(searchString)).Distinct();
                        break;
                    case "Custname_Extn":
                        merchant = merchant.Where(s => s.Custname_Extn.Contains(searchString)).Distinct();
                        break;
                }
                return View(await merchant.ToListAsync());
            }
            else
                return View(await merchant.Where(o => o.No == 0).ToListAsync());

        }


        // GET: Merchant/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            正式商店設定資料 Merchant = db.正式商店設定資料.Find(id);

            if (Merchant == null)
            {
                return HttpNotFound();
            }
            
            return View(Merchant);
        }
            
            //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    正式商店設定資料 正式商店設定資料 = db.正式商店設定資料.Find(id);
        //    if (正式商店設定資料 == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(正式商店設定資料);
        //}

        //// GET: Merchant/Create
        //public ActionResult Create()
        //{
        //    ViewBag.AcquireBank = new SelectList(db.ACQUIRE_BANK, "AcquireBank", "Description");
        //    ViewBag.AcquireMode = new SelectList(db.ACQUIRE_MODE, "AcquireMode", "Description");
        //    ViewBag.Alipay_Product = new SelectList(db.Alipay_Product, "Product", "Product");
        //    ViewBag.AutoSettleTime = new SelectList(db.AUTO_SETTLE_TIME, "AutoSettleTime", "Description");
        //    ViewBag.HostID = new SelectList(db.HOSTS, "HostID", "ENV");
        //    ViewBag.FrontHostID = new SelectList(db.HOSTS, "HostID", "ENV");
        //    ViewBag.MallType = new SelectList(db.MALL_TYPE, "MallType", "Description");
        //    ViewBag.MerState = new SelectList(db.MER_STATE, "MerState", "Description");
        //    ViewBag.MerType = new SelectList(db.MER_TYPE, "MerType", "Description");
        //    ViewBag.ObjVer_N = new SelectList(db.OBJ_VER, "ObjVer", "Description");
        //    ViewBag.No = new SelectList(db.OnCmpDatas, "OnCmpCorreCode", "Merno_F");
        //    ViewBag.SettleCycle = new SelectList(db.SETTLE_CYCLE, "SettleCycle", "Description");
        //    ViewBag.CustName = new SelectList(db.客戶基本資料, "CustName", "CustName_En");
        //    return View();
        //}

        //// POST: Merchant/Create
        //// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        //// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "No,MerNo_F,MerNo_T,CustName,MerName_En,MerName_Ch,Account,PassWord,New_PassWord,CooperMode,MID,TID,CMID,CTID,Mall_ID,Org_ID,HostID,BankMode,MerState,MerType2,OnRemarks,BatchType_N,OnNtcTime,OnCmpTime,OffNtcTime,OffCmpTime,OffRemarks,ObjVer_N,ReportMail,VBV,MerNo_New,RSP_CardNo,SysOn_TimeSt,SysOff_TimeSt,Total_Rate,Bank_Rate,Neweb_Rate,Mall_Rate,Credit,ACH,RateExp,BusinessItem,OnLineCs,OffLineCs,IS_IM,IS_CTID,RPCCMerNo,MallType,MallTypeNote,IMRate_3,IMRate_6,IMRate_9,IMRate_12,IMRate_18,IMRate_24,IMRate_36,Created,MerType,AcquireBank,AcquireMode,Is_UnionPay,SettleCycle,IS_RPRecCCNo,IS_RPFirstCap,IS_RPAutoDelFail,HostIPs,AdminIPs,RPUploadIPs,AutoSettleTime,ReportGiven,AsyncTxtNoteURL,MerLogo,MerTxtNote,Mer_Graph,ContraNo,SinSerChar,FrontHostID,MerNo_Old,SMS,BlackList,CS_VAP,CS_IBON,CS_CS,CS_VAPCHK,CS_WEBATMCHK,CS_IBONCHK,CS_FAMIPORTCHK,CS_LIFEETCHK,CS_OKGOCHK,CS_ALIPAYCHK,CS_ALIPAYWAPCHK,CS_CSCHK,AliFee,AliRatio,AliMallFee,AliMallRatio,Ali_Overtime,AliWap_Fee,AliWap_Ratio,AliWapMall_Fee,AliWapMall_Ratio,AliWap_Overtime,Is_Autobilling,MPOSCorreCode,Alipay_Product,P33,TransferFee,Custname_Extn,Custname_Rept,Custname_Bill,abroadBankRate,abroadMallRate,abroadNewebRate,abroadTotalRate,abroadACH,abroadRateExp,CAUTION,Sales,isApplePay,isAndroidPay,EBPP_TOTALFEE,EBPP_STOREFEE,EBPP_QWAREFEE,EBPP_ENTITIESBILL,EBPP_IBONBILL,TICKET_PUBLISHERNO,TICKET_ACCOUNT,TICKET_PWD,EBPP_From,IS_QRMPP")] 正式商店設定資料 正式商店設定資料)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.正式商店設定資料.Add(正式商店設定資料);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.AcquireBank = new SelectList(db.ACQUIRE_BANK, "AcquireBank", "Description", 正式商店設定資料.AcquireBank);
        //    ViewBag.AcquireMode = new SelectList(db.ACQUIRE_MODE, "AcquireMode", "Description", 正式商店設定資料.AcquireMode);
        //    ViewBag.Alipay_Product = new SelectList(db.Alipay_Product, "Product", "Product", 正式商店設定資料.Alipay_Product);
        //    ViewBag.AutoSettleTime = new SelectList(db.AUTO_SETTLE_TIME, "AutoSettleTime", "Description", 正式商店設定資料.AutoSettleTime);
        //    ViewBag.HostID = new SelectList(db.HOSTS, "HostID", "ENV", 正式商店設定資料.HostID);
        //    ViewBag.FrontHostID = new SelectList(db.HOSTS, "HostID", "ENV", 正式商店設定資料.FrontHostID);
        //    ViewBag.MallType = new SelectList(db.MALL_TYPE, "MallType", "Description", 正式商店設定資料.MallType);
        //    ViewBag.MerState = new SelectList(db.MER_STATE, "MerState", "Description", 正式商店設定資料.MerState);
        //    ViewBag.MerType = new SelectList(db.MER_TYPE, "MerType", "Description", 正式商店設定資料.MerType);
        //    ViewBag.ObjVer_N = new SelectList(db.OBJ_VER, "ObjVer", "Description", 正式商店設定資料.ObjVer_N);
        //    ViewBag.No = new SelectList(db.OnCmpDatas, "OnCmpCorreCode", "Merno_F", 正式商店設定資料.No);
        //    ViewBag.SettleCycle = new SelectList(db.SETTLE_CYCLE, "SettleCycle", "Description", 正式商店設定資料.SettleCycle);
        //    ViewBag.CustName = new SelectList(db.客戶基本資料, "CustName", "CustName_En", 正式商店設定資料.CustName);
        //    return View(正式商店設定資料);
        //}

        //// GET: Merchant/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    正式商店設定資料 正式商店設定資料 = db.正式商店設定資料.Find(id);
        //    if (正式商店設定資料 == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.AcquireBank = new SelectList(db.ACQUIRE_BANK, "AcquireBank", "Description", 正式商店設定資料.AcquireBank);
        //    ViewBag.AcquireMode = new SelectList(db.ACQUIRE_MODE, "AcquireMode", "Description", 正式商店設定資料.AcquireMode);
        //    ViewBag.Alipay_Product = new SelectList(db.Alipay_Product, "Product", "Product", 正式商店設定資料.Alipay_Product);
        //    ViewBag.AutoSettleTime = new SelectList(db.AUTO_SETTLE_TIME, "AutoSettleTime", "Description", 正式商店設定資料.AutoSettleTime);
        //    ViewBag.HostID = new SelectList(db.HOSTS, "HostID", "ENV", 正式商店設定資料.HostID);
        //    ViewBag.FrontHostID = new SelectList(db.HOSTS, "HostID", "ENV", 正式商店設定資料.FrontHostID);
        //    ViewBag.MallType = new SelectList(db.MALL_TYPE, "MallType", "Description", 正式商店設定資料.MallType);
        //    ViewBag.MerState = new SelectList(db.MER_STATE, "MerState", "Description", 正式商店設定資料.MerState);
        //    ViewBag.MerType = new SelectList(db.MER_TYPE, "MerType", "Description", 正式商店設定資料.MerType);
        //    ViewBag.ObjVer_N = new SelectList(db.OBJ_VER, "ObjVer", "Description", 正式商店設定資料.ObjVer_N);
        //    ViewBag.No = new SelectList(db.OnCmpDatas, "OnCmpCorreCode", "Merno_F", 正式商店設定資料.No);
        //    ViewBag.SettleCycle = new SelectList(db.SETTLE_CYCLE, "SettleCycle", "Description", 正式商店設定資料.SettleCycle);
        //    ViewBag.CustName = new SelectList(db.客戶基本資料, "CustName", "CustName_En", 正式商店設定資料.CustName);
        //    return View(正式商店設定資料);
        //}

        //// POST: Merchant/Edit/5
        //// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        //// 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "No,MerNo_F,MerNo_T,CustName,MerName_En,MerName_Ch,Account,PassWord,New_PassWord,CooperMode,MID,TID,CMID,CTID,Mall_ID,Org_ID,HostID,BankMode,MerState,MerType2,OnRemarks,BatchType_N,OnNtcTime,OnCmpTime,OffNtcTime,OffCmpTime,OffRemarks,ObjVer_N,ReportMail,VBV,MerNo_New,RSP_CardNo,SysOn_TimeSt,SysOff_TimeSt,Total_Rate,Bank_Rate,Neweb_Rate,Mall_Rate,Credit,ACH,RateExp,BusinessItem,OnLineCs,OffLineCs,IS_IM,IS_CTID,RPCCMerNo,MallType,MallTypeNote,IMRate_3,IMRate_6,IMRate_9,IMRate_12,IMRate_18,IMRate_24,IMRate_36,Created,MerType,AcquireBank,AcquireMode,Is_UnionPay,SettleCycle,IS_RPRecCCNo,IS_RPFirstCap,IS_RPAutoDelFail,HostIPs,AdminIPs,RPUploadIPs,AutoSettleTime,ReportGiven,AsyncTxtNoteURL,MerLogo,MerTxtNote,Mer_Graph,ContraNo,SinSerChar,FrontHostID,MerNo_Old,SMS,BlackList,CS_VAP,CS_IBON,CS_CS,CS_VAPCHK,CS_WEBATMCHK,CS_IBONCHK,CS_FAMIPORTCHK,CS_LIFEETCHK,CS_OKGOCHK,CS_ALIPAYCHK,CS_ALIPAYWAPCHK,CS_CSCHK,AliFee,AliRatio,AliMallFee,AliMallRatio,Ali_Overtime,AliWap_Fee,AliWap_Ratio,AliWapMall_Fee,AliWapMall_Ratio,AliWap_Overtime,Is_Autobilling,MPOSCorreCode,Alipay_Product,P33,TransferFee,Custname_Extn,Custname_Rept,Custname_Bill,abroadBankRate,abroadMallRate,abroadNewebRate,abroadTotalRate,abroadACH,abroadRateExp,CAUTION,Sales,isApplePay,isAndroidPay,EBPP_TOTALFEE,EBPP_STOREFEE,EBPP_QWAREFEE,EBPP_ENTITIESBILL,EBPP_IBONBILL,TICKET_PUBLISHERNO,TICKET_ACCOUNT,TICKET_PWD,EBPP_From,IS_QRMPP")] 正式商店設定資料 正式商店設定資料)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(正式商店設定資料).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.AcquireBank = new SelectList(db.ACQUIRE_BANK, "AcquireBank", "Description", 正式商店設定資料.AcquireBank);
        //    ViewBag.AcquireMode = new SelectList(db.ACQUIRE_MODE, "AcquireMode", "Description", 正式商店設定資料.AcquireMode);
        //    ViewBag.Alipay_Product = new SelectList(db.Alipay_Product, "Product", "Product", 正式商店設定資料.Alipay_Product);
        //    ViewBag.AutoSettleTime = new SelectList(db.AUTO_SETTLE_TIME, "AutoSettleTime", "Description", 正式商店設定資料.AutoSettleTime);
        //    ViewBag.HostID = new SelectList(db.HOSTS, "HostID", "ENV", 正式商店設定資料.HostID);
        //    ViewBag.FrontHostID = new SelectList(db.HOSTS, "HostID", "ENV", 正式商店設定資料.FrontHostID);
        //    ViewBag.MallType = new SelectList(db.MALL_TYPE, "MallType", "Description", 正式商店設定資料.MallType);
        //    ViewBag.MerState = new SelectList(db.MER_STATE, "MerState", "Description", 正式商店設定資料.MerState);
        //    ViewBag.MerType = new SelectList(db.MER_TYPE, "MerType", "Description", 正式商店設定資料.MerType);
        //    ViewBag.ObjVer_N = new SelectList(db.OBJ_VER, "ObjVer", "Description", 正式商店設定資料.ObjVer_N);
        //    ViewBag.No = new SelectList(db.OnCmpDatas, "OnCmpCorreCode", "Merno_F", 正式商店設定資料.No);
        //    ViewBag.SettleCycle = new SelectList(db.SETTLE_CYCLE, "SettleCycle", "Description", 正式商店設定資料.SettleCycle);
        //    ViewBag.CustName = new SelectList(db.客戶基本資料, "CustName", "CustName_En", 正式商店設定資料.CustName);
        //    return View(正式商店設定資料);
        //}

        //// GET: Merchant/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    正式商店設定資料 正式商店設定資料 = db.正式商店設定資料.Find(id);
        //    if (正式商店設定資料 == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(正式商店設定資料);
        //}

        //// POST: Merchant/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    正式商店設定資料 正式商店設定資料 = db.正式商店設定資料.Find(id);
        //    db.正式商店設定資料.Remove(正式商店設定資料);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
