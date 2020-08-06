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
    public class Finished_ProductionController : Controller
    {
        private SRSEntities db = new SRSEntities();

        // GET: Finished_Production
        public ActionResult Index()
        {
            //var finished_Production = db.Finished_Production.Include(f => f.Measurement_Unit);
            //return View(finished_Production.ToList());
            return View(db.SP_Select_Production());
        }

        // GET: Finished_Production/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Finished_Production finished_Production = db.Finished_Production.Find(id);
            if (finished_Production == null)
            {
                return HttpNotFound();
            }
            return View(finished_Production);
        }

        // GET: Finished_Production/Create
        public ActionResult Create()
        {
            ViewBag.FK_Measurement_Unit = new SelectList(db.Measurement_Unit, "ID_MerUn", "Name_MerUn");
            return View();
        }

        // POST: Finished_Production/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_FinPr,Name_FinPr,FK_Measurement_Unit,Sum,Total_Amount")] Finished_Production finished_Production)
        {
            if (ModelState.IsValid)
            {
                db.Finished_Production.Add(finished_Production);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_Measurement_Unit = new SelectList(db.Measurement_Unit, "ID_MerUn", "Name_MerUn", finished_Production.FK_Measurement_Unit);
            return View(finished_Production);
        }

        // GET: Finished_Production/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Finished_Production finished_Production = db.Finished_Production.Find(id);
            if (finished_Production == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Measurement_Unit = new SelectList(db.Measurement_Unit, "ID_MerUn", "Name_MerUn", finished_Production.FK_Measurement_Unit);
            return View(finished_Production);
        }

        // POST: Finished_Production/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_FinPr,Name_FinPr,FK_Measurement_Unit,Sum,Total_Amount")] Finished_Production finished_Production)
        {
            if (ModelState.IsValid)
            {
                db.Entry(finished_Production).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Measurement_Unit = new SelectList(db.Measurement_Unit, "ID_MerUn", "Name_MerUn", finished_Production.FK_Measurement_Unit);
            return View(finished_Production);
        }

        // GET: Finished_Production/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Finished_Production finished_Production = db.Finished_Production.Find(id);
            if (finished_Production == null)
            {
                return HttpNotFound();
            }
            return View(finished_Production);
        }

        // POST: Finished_Production/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Finished_Production finished_Production = db.Finished_Production.Find(id);
            db.Finished_Production.Remove(finished_Production);
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
