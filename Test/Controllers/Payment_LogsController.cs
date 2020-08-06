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
    public class Payment_LogsController : Controller
    {
        private SRSEntities db = new SRSEntities();

        // GET: Payment_Logs
        public ActionResult Index()
        {
            //var payment_Logs = db.Payment_Logs.Include(p => p.Employers);
            //return View(payment_Logs.ToList());
            return View(db.Financial_SP_Select_Payment());
        }

        // GET: Payment_Logs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_Logs payment_Logs = db.Payment_Logs.Find(id);
            if (payment_Logs == null)
            {
                return HttpNotFound();
            }
            return View(payment_Logs);
        }

        // GET: Payment_Logs/Create
        public ActionResult Create()
        {
            ViewBag.message = "";
            ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp");
            return View();
        }

        // POST: Payment_Logs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PaymentLog,FK_Employer,Amount_of_work,Sum_of_Bonus,Salary,Total_Payment,BuyStock_Amount,Manufacture_Amount,Sales_Amount,PaymentDate,Additional_Pay")] Payment_Logs payment_Logs)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Payment_Logs.Add(payment_Logs);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex) // ловим исключение в случае если триггер не позволит добавить запись
                {
                    var sqlexception = ex.GetBaseException() as SqlException; // определяем SqlException 
                    if (sqlexception != null)
                    {
                        if (sqlexception.Errors.Count > 0)
                        {
                            if(sqlexception.Errors[0].Class == 15) // В случае если состояние RAISERROR в триггере = 15, то получаем следующее сообщение
                            {
                                ViewBag.message = "Вы не можете выдать сотруднику зарплату за один месяц два раза!";
                            }
                            else if (sqlexception.Errors[0].Class == 16) // В случае если состояние RAISERROR в триггере 16, то получаем следующее сообщение
                            {
                                ViewBag.message = "В бюджете недостаточно средств, для выдачи зарплаты!";
                            }
                        }
                    }                    
                        ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp", payment_Logs.FK_Employer);
                        return View(payment_Logs);                    
                }
               
            }
            ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp", payment_Logs.FK_Employer);
            return View(payment_Logs);
        }

        // GET: Payment_Logs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_Logs payment_Logs = db.Payment_Logs.Find(id);
            if (payment_Logs == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp", payment_Logs.FK_Employer);
            return View(payment_Logs);
        }

        // POST: Payment_Logs/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PaymentLog,FK_Employer,Amount_of_work,Sum_of_Bonus,Salary,Total_Payment,BuyStock_Amount,Manufacture_Amount,Sales_Amount,PaymentDate,Additional_Pay")] Payment_Logs payment_Logs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment_Logs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp", payment_Logs.FK_Employer);
            return View(payment_Logs);
        }

        // GET: Payment_Logs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment_Logs payment_Logs = db.Payment_Logs.Find(id);
            if (payment_Logs == null)
            {
                return HttpNotFound();
            }
            return View(payment_Logs);
        }

        // POST: Payment_Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment_Logs payment_Logs = db.Payment_Logs.Find(id);
            db.Payment_Logs.Remove(payment_Logs);
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
