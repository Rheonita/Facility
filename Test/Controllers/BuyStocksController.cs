using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test.Models;

namespace Test.Controllers
{
    public class BuyStocksController : Controller
    {
        private SRSEntities db = new SRSEntities();

        // GET: BuyStocks
        public ActionResult Index()
        {
            //var buyStock = db.BuyStock.Include(b => b.Employers).Include(b => b.Stock);
            //return View(buyStock.ToList());
            return View(db.Production_SP_Select_Buystocks());
        }

        // GET: BuyStocks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyStock buyStock = db.BuyStock.Find(id);
            if (buyStock == null)
            {
                return HttpNotFound();
            }
            return View(buyStock);
        }

        // GET: BuyStocks/Create
        public ActionResult Create()
        {
            ViewBag.message = "";
            ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp");
            ViewBag.FK_Stock = new SelectList(db.Stock, "ID_Stock", "Name_of_Stock");
            return View();
        }

        // POST: BuyStocks/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BuyStock,FK_Stock,Total_Amount,Sum,Date,FK_Employer")] BuyStock buyStock)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.BuyStock.Add(buyStock);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.message = "Сумма закупки превышает сумму бюджета!";
                    ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp", buyStock.FK_Employer);
                    ViewBag.FK_Stock = new SelectList(db.Stock, "ID_Stock", "Name_of_Stock", buyStock.FK_Stock);
                    return View(buyStock);
                }
            }
            return RedirectToAction("Index");

        }

        // GET: BuyStocks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyStock buyStock = db.BuyStock.Find(id);
            if (buyStock == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp", buyStock.FK_Employer);
            ViewBag.FK_Stock = new SelectList(db.Stock, "ID_Stock", "Name_of_Stock", buyStock.FK_Stock);
            return View(buyStock);
        }

        // POST: BuyStocks/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_BuyStock,FK_Stock,Total_Amount,Sum,Date,FK_Employer")] BuyStock buyStock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(buyStock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp", buyStock.FK_Employer);
            ViewBag.FK_Stock = new SelectList(db.Stock, "ID_Stock", "Name_of_Stock", buyStock.FK_Stock);
            return View(buyStock);
        }

        // GET: BuyStocks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BuyStock buyStock = db.BuyStock.Find(id);
            if (buyStock == null)
            {
                return HttpNotFound();
            }
            return View(buyStock);
        }

        // POST: BuyStocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BuyStock buyStock = db.BuyStock.Find(id);
            db.BuyStock.Remove(buyStock);
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
