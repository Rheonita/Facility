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
    public class Measurement_UnitController : Controller
    {
        private SRSEntities db = new SRSEntities();

        // GET: Measurement_Unit
        public ActionResult Index()
        {
            return View(db.SP_Select_MeasurementUnit());
        }

        // GET: Measurement_Unit/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Measurement_Unit measurement_Unit = db.Measurement_Unit.Find(id);
            if (measurement_Unit == null)
            {
                return HttpNotFound();
            }
            return View(measurement_Unit);
        }

        // GET: Measurement_Unit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Measurement_Unit/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_MerUn,Name_MerUn")] Measurement_Unit measurement_Unit)
        {
            if (ModelState.IsValid)
            {
                db.Measurement_Unit.Add(measurement_Unit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(measurement_Unit);
        }

        // GET: Measurement_Unit/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Measurement_Unit measurement_Unit = db.Measurement_Unit.Find(id);
            if (measurement_Unit == null)
            {
                return HttpNotFound();
            }
            return View(measurement_Unit);
        }

        // POST: Measurement_Unit/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_MerUn,Name_MerUn")] Measurement_Unit measurement_Unit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(measurement_Unit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(measurement_Unit);
        }

        // GET: Measurement_Unit/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Measurement_Unit measurement_Unit = db.Measurement_Unit.Find(id);
            if (measurement_Unit == null)
            {
                return HttpNotFound();
            }
            return View(measurement_Unit);
        }

        // POST: Measurement_Unit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Measurement_Unit measurement_Unit = db.Measurement_Unit.Find(id);
            db.Measurement_Unit.Remove(measurement_Unit);
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
