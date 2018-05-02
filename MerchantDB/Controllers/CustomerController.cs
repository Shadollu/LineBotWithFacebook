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
    public class CustomerController : Controller
    {
        private TestDB2Entities db = new TestDB2Entities();

        // GET: Customer
        public async Task<ActionResult> Index(string searchString, string OptionValue)
  
        {
            var Customer = db.客戶基本資料.Include(c => c.EMPLOYEE);

            if (!string.IsNullOrEmpty(searchString))
            {
                switch (OptionValue)
                {
                    case "CustName":
                        Customer = Customer.Where(s => s.CustName.Contains(searchString));
                        break;
                    case "BusinessNo":
                        Customer = Customer.Where(s => s.BusinessNo.Contains(searchString));
                        break;
                    case "CustTel":
                        Customer = Customer.Where(s => s.CustTel.Contains(searchString));
                        break;
                    case "AcctNum":
                        Customer = Customer.Where(s => s.AcctNum.Contains(searchString));
                        break;
                    case "SalesMail_Phone":
                        var Sales = db.客戶業務窗口資料.Where(s => s.SalesMail.Contains(searchString) | s.SalesTel.Contains(searchString)).Select(s=>s.CustName).ToList();
                        Customer = from data in Customer
                                   where Sales.Contains(data.CustName)
                                   select data;
                        break;
                }
                return View(await Customer.ToListAsync());
            }
            else
                return View(await Customer.Where(o => o.No == 0).ToListAsync());
            
        }

        // GET: Customer/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            
            客戶基本資料 Customer = db.客戶基本資料.Find(id);

            if (Customer == null)
            {
                return HttpNotFound();
            }
            return View(Customer);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            ViewBag.NewebSales = new SelectList(db.EMPLOYEE, "Em_Name", "Department");
            return View();
        }

        // POST: Customer/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "No,CustName,CustName_En,BusinessNo,CustName_Extn,CustName_Rept,CustName_Bill,AcctTitle,AcctNum,AcctBank,BankBranch,InvAddress,CustTel,CustFax,CustURL,MerURL,FileNo,Capital,Director,DirectorID,AuthPerson,AuthMail,AuthTel,NewebSales,CustAdrs,CustName_EnCode,AddresseeName,InvZipcode,CustZipcode,Remarks,IS_Distributor,InspectStatus,ProjectRecord,CompanySetupDate,ChangeApprovalDate")] 客戶基本資料 Customer)
        {
            if (ModelState.IsValid)
            {
                db.客戶基本資料.Add(Customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NewebSales = new SelectList(db.EMPLOYEE, "Em_Name", "Department", Customer.NewebSales);
            return View(Customer);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶基本資料 Customer = db.客戶基本資料.Find(id);
            if (Customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.NewebSales = new SelectList(db.EMPLOYEE, "Em_Name", "Department", Customer.NewebSales);
            return View(Customer);
        }

        // POST: Customer/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "No,CustName,CustName_En,BusinessNo,CustName_Extn,CustName_Rept,CustName_Bill,AcctTitle,AcctNum,AcctBank,BankBranch,InvAddress,CustTel,CustFax,CustURL,MerURL,FileNo,Capital,Director,DirectorID,AuthPerson,AuthMail,AuthTel,NewebSales,CustAdrs,CustName_EnCode,AddresseeName,InvZipcode,CustZipcode,Remarks,IS_Distributor,InspectStatus,ProjectRecord,CompanySetupDate,ChangeApprovalDate")] 客戶基本資料 Customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NewebSales = new SelectList(db.EMPLOYEE, "Em_Name", "Department", Customer.NewebSales);
            return View(Customer);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶基本資料 Customer = db.客戶基本資料.Find(id);
            if (Customer == null)
            {
                return HttpNotFound();
            }
            return View(Customer);
        }

        // POST: Customer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            客戶基本資料 Customer = db.客戶基本資料.Find(id);
            db.客戶基本資料.Remove(Customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
