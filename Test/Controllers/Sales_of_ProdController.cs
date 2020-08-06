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
    public class Sales_of_ProdController : Controller
    {
        private SRSEntities db = new SRSEntities();

        // GET: Sales_of_Prod
        public ActionResult Index()
        {
            //var sales_of_Prod = db.Sales_of_Prod.Include(s => s.Employers).Include(s => s.Finished_Production);
            //return View(sales_of_Prod.ToList());
            return View(db.Production_SP_Select_Sales_Of_Prod());
        }

        // GET: Sales_of_Prod/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_of_Prod sales_of_Prod = db.Sales_of_Prod.Find(id);
            if (sales_of_Prod == null)
            {
                return HttpNotFound();
            }
            return View(sales_of_Prod);
        }

        // GET: Sales_of_Prod/Create
        public ActionResult Create()
        {
            ViewBag.message = "";
            ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp");
            ViewBag.FK_Production = new SelectList(db.Finished_Production, "ID_FinPr", "Name_FinPr");
            return View();
        }

        // POST: Sales_of_Prod/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_SalesProd,FK_Production,Total_Amount,Sum,Date,FK_Employer")] Sales_of_Prod sales_of_Prod)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Sales_of_Prod.Add(sales_of_Prod);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.message = "Недостаточно продукции для продажи!";
                    ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp", sales_of_Prod.FK_Employer);
                    ViewBag.FK_Production = new SelectList(db.Finished_Production, "ID_FinPr", "Name_FinPr", sales_of_Prod.FK_Production);
                    return View(sales_of_Prod);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Sales_of_Prod/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_of_Prod sales_of_Prod = db.Sales_of_Prod.Find(id);
            if (sales_of_Prod == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp", sales_of_Prod.FK_Employer);
            ViewBag.FK_Production = new SelectList(db.Finished_Production, "ID_FinPr", "Name_FinPr", sales_of_Prod.FK_Production);
            return View(sales_of_Prod);
        }

        // POST: Sales_of_Prod/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_SalesProd,FK_Production,Total_Amount,Sum,Date,FK_Employer")] Sales_of_Prod sales_of_Prod)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales_of_Prod).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp", sales_of_Prod.FK_Employer);
            ViewBag.FK_Production = new SelectList(db.Finished_Production, "ID_FinPr", "Name_FinPr", sales_of_Prod.FK_Production);
            return View(sales_of_Prod);
        }

        // GET: Sales_of_Prod/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales_of_Prod sales_of_Prod = db.Sales_of_Prod.Find(id);
            if (sales_of_Prod == null)
            {
                return HttpNotFound();
            }
            return View(sales_of_Prod);
        }

        // POST: Sales_of_Prod/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sales_of_Prod sales_of_Prod = db.Sales_of_Prod.Find(id);
            db.Sales_of_Prod.Remove(sales_of_Prod);
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
