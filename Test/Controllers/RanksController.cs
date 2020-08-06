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
    public class RanksController : Controller
    {
        private SRSEntities db = new SRSEntities();

        // GET: Ranks
        public ActionResult Index()
        {
            return View(db.SP_Select_Ranks());
        }
        // GET: Ranks/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ranks ranks = db.Ranks.Find(id);
            if (ranks == null)
            {
                return HttpNotFound();
            }
            return View(ranks);
        }

        // GET: Ranks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ranks/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Rank,Rank_name")] Ranks ranks)
        {
            if (ModelState.IsValid)
            {
                db.Ranks.Add(ranks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ranks);
        }

        // GET: Ranks/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ranks ranks = db.Ranks.Find(id);
            if (ranks == null)
            {
                return HttpNotFound();
            }
            return View(ranks);
        }

        // POST: Ranks/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Rank,Rank_name")] Ranks ranks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ranks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ranks);
        }

        // GET: Ranks/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ranks ranks = db.Ranks.Find(id);
            if (ranks == null)
            {
                return HttpNotFound();
            }
            return View(ranks);
        }

        // POST: Ranks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            Ranks ranks = db.Ranks.Find(id);
            db.Ranks.Remove(ranks);
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
