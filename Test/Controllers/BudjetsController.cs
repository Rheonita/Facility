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
    public class BudjetsController : Controller
    {
        private SRSEntities db = new SRSEntities();

        // GET: Budjets
        public ActionResult Index()
        {
            return View(db.SP_Select_Budjet());
        }

        // GET: Budjets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budjet budjet = db.Budjet.Find(id);
            if (budjet == null)
            {
                return HttpNotFound();
            }
            return View(budjet);
        }

        // GET: Budjets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Budjets/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Budget,Budget_Sum,Sale_Markup,Emp_Sale_Bonus")] Budjet budjet )
        {
            if (ModelState.IsValid)
            {

                db.Budjet.Add(budjet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(budjet);
        }

        // GET: Budjets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budjet budjet = db.Budjet.Find(id);
            if (budjet == null)
            {
                return HttpNotFound();
            }
            return View(budjet);
        }

        // POST: Budjets/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Budget,Budget_Sum,Sale_Markup,Emp_Sale_Bonus")] Budjet budjet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(budjet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(budjet);
        }

        // GET: Budjets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budjet budjet = db.Budjet.Find(id);
            if (budjet == null)
            {
                return HttpNotFound();
            }
            return View(budjet);
        }

        // POST: Budjets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Budjet budjet = db.Budjet.Find(id);
            db.Budjet.Remove(budjet);
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
