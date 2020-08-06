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
    public class IngredientsController : Controller
    {
        private SRSEntities db = new SRSEntities();

        // GET: Ingredients
        public ActionResult Index(string ProductsType, string SearchString)
        {
            var ProductionTypeFilter = new List<Ingredients>(); // Коллекция для выборки
            var ProductionTypeQuery = db.SP_Select_Ingredients(); // Данные в коллекцию для выборки
            ProductionTypeFilter.AddRange(ProductionTypeQuery.Distinct()); // Получает данные коллекции
            ViewBag.ProductsType = new SelectList(ProductionTypeFilter, "Первая продукция"); // Задает значение по умолчанию

            //string SearchString = id;
            var ProductionType = db.SP_Select_Ingredients(); // поиск в текстбоксе
            //ProductionType.Where(s=>s.)
            ;
            if (!String.IsNullOrEmpty(SearchString)) //
            {
                return View(ProductionType.Where(s => s.Finished_Production.Name_FinPr.Contains(SearchString)));
            }
            if (!String.IsNullOrEmpty(ProductsType))
            {
                 return View(ProductionType.Where(x => x.Finished_Production.Name_FinPr == ProductsType));
            }
            //var ingredients = db.Ingredients.Include(i => i.Finished_Production).Include(i => i.Stock); // стоковый вариант
            return View(ProductionType);
            //return View(ingredients.ToList()); // стоковый вариант
        }
        
        // GET: Ingredients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredients ingredients = db.Ingredients.Find(id);
            if (ingredients == null)
            {
                return HttpNotFound();
            }
            return View(ingredients);
        }

        // GET: Ingredients/Create
        public ActionResult Create()
        {
            ViewBag.FK_Production = new SelectList(db.Finished_Production, "ID_FinPr", "Name_FinPr");
            ViewBag.FK_Stock = new SelectList(db.Stock, "ID_Stock", "Name_of_Stock");
            return View();
        }

        // POST: Ingredients/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Ingredient,FK_Production,FK_Stock,Total_Amount")] Ingredients ingredients)
        {
            if (ModelState.IsValid)
            {
                db.Ingredients.Add(ingredients);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_Production = new SelectList(db.Finished_Production, "ID_FinPr", "Name_FinPr", ingredients.FK_Production);
            ViewBag.FK_Stock = new SelectList(db.Stock, "ID_Stock", "Name_of_Stock", ingredients.FK_Stock);
            return View(ingredients);
        }

        // GET: Ingredients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredients ingredients = db.Ingredients.Find(id);
            if (ingredients == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Production = new SelectList(db.Finished_Production, "ID_FinPr", "Name_FinPr", ingredients.FK_Production);
            ViewBag.FK_Stock = new SelectList(db.Stock, "ID_Stock", "Name_of_Stock", ingredients.FK_Stock);
            return View(ingredients);
        }

        // POST: Ingredients/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Ingredient,FK_Production,FK_Stock,Total_Amount")] Ingredients ingredients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Production = new SelectList(db.Finished_Production, "ID_FinPr", "Name_FinPr", ingredients.FK_Production);
            ViewBag.FK_Stock = new SelectList(db.Stock, "ID_Stock", "Name_of_Stock", ingredients.FK_Stock);
            return View(ingredients);
        }

        // GET: Ingredients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredients ingredients = db.Ingredients.Find(id);
            if (ingredients == null)
            {
                return HttpNotFound();
            }
            return View(ingredients);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingredients ingredients = db.Ingredients.Find(id);
            db.Ingredients.Remove(ingredients);
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
