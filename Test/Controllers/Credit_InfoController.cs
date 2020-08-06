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
    public class Credit_InfoController : Controller
    {
        private SRSEntities db = new SRSEntities();

        // GET: Credit_Info
        public ActionResult Index()
        {
            return View(db.Financial_SP_Select_CreditInfo());
        }

        // GET: Credit_Info/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit_Info credit_Info = db.Credit_Info.Find(id);
            if (credit_Info == null)
            {
                return HttpNotFound();
            }
            return View(credit_Info);
        }

        // GET: Credit_Info/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Credit_Info/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Credit,Credit_Description,Sum_of_Credit,Date_of_issue,Credit_Term,Year_Percent,Fine_Sum,Sum_of_Month_Pay,Total_Sum_With_Year")] Credit_Info credit_Info)
        {
            if (ModelState.IsValid)
            {
                db.Credit_Info.Add(credit_Info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(credit_Info);
        }

        // GET: Credit_Info/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit_Info credit_Info = db.Credit_Info.Find(id);
            if (credit_Info == null)
            {
                return HttpNotFound();
            }
            return View(credit_Info);
        }

        // POST: Credit_Info/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Credit,Credit_Description,Sum_of_Credit,Date_of_issue,Credit_Term,Year_Percent,Fine_Sum,Sum_of_Month_Pay,Total_Sum_With_Year")] Credit_Info credit_Info)
        {
            if (ModelState.IsValid)
            {
                db.Entry(credit_Info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(credit_Info);
        }

        // GET: Credit_Info/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Credit_Info credit_Info = db.Credit_Info.Find(id);
            if (credit_Info == null)
            {
                return HttpNotFound();
            }
            return View(credit_Info);
        }

        // POST: Credit_Info/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Credit_Info credit_Info = db.Credit_Info.Find(id);
            db.Credit_Info.Remove(credit_Info);
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
