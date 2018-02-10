using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    public class UnitOfMeasureController : Controller
    {
        private Entities db = new Entities();

        // GET: UnitOfMeasure
        public async Task<ActionResult> Index()
        {
            return View(await db.UnitOfMeasures.ToListAsync());
        }

        // GET: UnitOfMeasure/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitOfMeasure unitOfMeasure = await db.UnitOfMeasures.FindAsync(id);
            if (unitOfMeasure == null)
            {
                return HttpNotFound();
            }
            return View(unitOfMeasure);
        }

        // GET: UnitOfMeasure/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UnitOfMeasure/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description")] UnitOfMeasure unitOfMeasure)
        {
            if (ModelState.IsValid)
            {
                db.UnitOfMeasures.Add(unitOfMeasure);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(unitOfMeasure);
        }

        // GET: UnitOfMeasure/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitOfMeasure unitOfMeasure = await db.UnitOfMeasures.FindAsync(id);
            if (unitOfMeasure == null)
            {
                return HttpNotFound();
            }
            return View(unitOfMeasure);
        }

        // POST: UnitOfMeasure/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description")] UnitOfMeasure unitOfMeasure)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unitOfMeasure).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(unitOfMeasure);
        }

        // GET: UnitOfMeasure/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitOfMeasure unitOfMeasure = await db.UnitOfMeasures.FindAsync(id);
            if (unitOfMeasure == null)
            {
                return HttpNotFound();
            }
            return View(unitOfMeasure);
        }

        // POST: UnitOfMeasure/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UnitOfMeasure unitOfMeasure = await db.UnitOfMeasures.FindAsync(id);
            db.UnitOfMeasures.Remove(unitOfMeasure);
            await db.SaveChangesAsync();
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
