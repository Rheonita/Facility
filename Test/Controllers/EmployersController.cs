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
    public class EmployersController : Controller
    {
        private SRSEntities db = new SRSEntities();

        // GET: Employers
        public ActionResult Index()
        {
            return View(db.SP_Select_Employers());
        }

        // GET: Employers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employers employers = db.Employers.Find(id);
            if (employers == null)
            {
                return HttpNotFound();
            }
            return View(employers);
        }

        // GET: Employers/Create
        public ActionResult Create()
        {
            ViewBag.FK_Rank = new SelectList(db.Ranks, "ID_Rank", "Rank_name");
            return View();
        }

        // POST: Employers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Employers,Name_of_Emp,FK_Rank,Payment,Adress,Phone_Number")] Employers employers)
        {
            if (ModelState.IsValid)
            {
                db.Employers.Add(employers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_Rank = new SelectList(db.Ranks, "ID_Rank", "Rank_name", employers.FK_Rank);
            return View(employers);
        }

        // GET: Employers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employers employers = db.Employers.Find(id);
            if (employers == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Rank = new SelectList(db.Ranks, "ID_Rank", "Rank_name", employers.FK_Rank);
            return View(employers);
        }

        // POST: Employers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Employers,Name_of_Emp,FK_Rank,Payment,Adress,Phone_Number")] Employers employers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Rank = new SelectList(db.Ranks, "ID_Rank", "Rank_name", employers.FK_Rank);
            return View(employers);
        }

        // GET: Employers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employers employers = db.Employers.Find(id);
            if (employers == null)
            {
                return HttpNotFound();
            }
            return View(employers);
        }

        // POST: Employers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employers employers = db.Employers.Find(id);
            db.Employers.Remove(employers);
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
