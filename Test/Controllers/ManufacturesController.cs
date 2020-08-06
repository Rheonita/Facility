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
    public class ManufacturesController : Controller
    {
        private SRSEntities db = new SRSEntities();

        // GET: Manufactures
        public ActionResult Index()
        {
            //var manufacture = db.Manufacture.Include(m => m.Employers).Include(m => m.Finished_Production);
            //return View(manufacture.ToList());
            return View(db.Production_SP_Select_Manufacture());
        }

        // GET: Manufactures/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacture manufacture = db.Manufacture.Find(id);
            if (manufacture == null)
            {
                return HttpNotFound();
            }
            return View(manufacture);
        }

        // GET: Manufactures/Create
        public ActionResult Create()
        {
            ViewBag.message = "";
            ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp");
            ViewBag.FK_Production = new SelectList(db.Finished_Production, "ID_FinPr", "Name_FinPr");
            return View();
        }

        // POST: Manufactures/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Manufacture,FK_Production,Total_Amount,Date,FK_Employer")] Manufacture manufacture)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Manufacture.Add(manufacture);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.message = "Недостаточно сырья для производства продукции!";
                    ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp", manufacture.FK_Employer);
                    ViewBag.FK_Production = new SelectList(db.Finished_Production, "ID_FinPr", "Name_FinPr", manufacture.FK_Production);
                    return View(manufacture);
                }

            }
            return RedirectToAction("Index");
        }

        // GET: Manufactures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacture manufacture = db.Manufacture.Find(id);
            if (manufacture == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp", manufacture.FK_Employer);
            ViewBag.FK_Production = new SelectList(db.Finished_Production, "ID_FinPr", "Name_FinPr", manufacture.FK_Production);
            return View(manufacture);
        }

        // POST: Manufactures/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Manufacture,FK_Production,Total_Amount,Date,FK_Employer")] Manufacture manufacture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manufacture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Employer = new SelectList(db.Employers, "ID_Employers", "Name_of_Emp", manufacture.FK_Employer);
            ViewBag.FK_Production = new SelectList(db.Finished_Production, "ID_FinPr", "Name_FinPr", manufacture.FK_Production);
            return View(manufacture);
        }

        // GET: Manufactures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manufacture manufacture = db.Manufacture.Find(id);
            if (manufacture == null)
            {
                return HttpNotFound();
            }
            return View(manufacture);
        }

        // POST: Manufactures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manufacture manufacture = db.Manufacture.Find(id);
            db.Manufacture.Remove(manufacture);
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
