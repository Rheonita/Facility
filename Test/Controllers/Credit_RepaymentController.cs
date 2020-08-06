using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class Credit_RepaymentController : Controller
    {
        private SRSEntities db = new SRSEntities();

        // GET: Credit_Repayment
        public ActionResult Index()
        {
            //var credit_Repayment = db.Credit_Repayment.Include(c => c.Credit_Info);
            //return View(credit_Repayment.ToList());
            return View(db.Financial_SP_Select_CreditPayment());
        }

        // GET: Credit_Repayment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit_Repayment credit_Repayment = db.Credit_Repayment.Find(id);
            if (credit_Repayment == null)
            {
                return HttpNotFound();
            }
            return View(credit_Repayment);
        }

        // GET: Credit_Repayment/Create
        public ActionResult Create()
        {
            ViewBag.message = "";
            ViewBag.FK_Credit = new SelectList(db.Credit_Info, "ID_Credit", "Credit_Description");
            return View();
        }

        // POST: Credit_Repayment/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Repayment,FK_Credit,Date_of_Repayment,Fair_Sum,Sum_of_Repayment")] Credit_Repayment credit_Repayment)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    db.Credit_Repayment.Add(credit_Repayment);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    var sqlexception = ex.GetBaseException() as SqlException; // определяем SqlException 
                    if (sqlexception != null)
                    {
                        if (sqlexception.Errors.Count > 0)
                        {
                            if (sqlexception.Errors[0].Class == 15) // В случае если состояние RAISERROR в триггере = 15, то получаем следующее сообщение
                            {
                                ViewBag.message = "Некорректная дата кредита!";
                            }
                            else if (sqlexception.Errors[0].Class == 16) // В случае если состояние RAISERROR в триггере 16, то получаем следующее сообщение
                            {
                                ViewBag.message = "Вы не можете делать двойные взносы за один месяц!";
                            }
                        }
                    }
                }
            }

            ViewBag.FK_Credit = new SelectList(db.Credit_Info, "ID_Credit", "Credit_Description", credit_Repayment.FK_Credit);
            return View(credit_Repayment);
        }

        // GET: Credit_Repayment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit_Repayment credit_Repayment = db.Credit_Repayment.Find(id);
            if (credit_Repayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Credit = new SelectList(db.Credit_Info, "ID_Credit", "Credit_Description", credit_Repayment.FK_Credit);
            return View(credit_Repayment);
        }

        // POST: Credit_Repayment/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Repayment,FK_Credit,Date_of_Repayment,Fair_Sum,Sum_of_Repayment")] Credit_Repayment credit_Repayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(credit_Repayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Credit = new SelectList(db.Credit_Info, "ID_Credit", "Credit_Description", credit_Repayment.FK_Credit);
            return View(credit_Repayment);
        }

        // GET: Credit_Repayment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit_Repayment credit_Repayment = db.Credit_Repayment.Find(id);
            if (credit_Repayment == null)
            {
                return HttpNotFound();
            }
            return View(credit_Repayment);
        }

        // POST: Credit_Repayment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Credit_Repayment credit_Repayment = db.Credit_Repayment.Find(id);
            db.Credit_Repayment.Remove(credit_Repayment);
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
