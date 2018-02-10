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
    public class WorkTypesController : Controller
    {
        private Entities db = new Entities();

        // GET: WorkTypes
        public async Task<ActionResult> Index()
        {
            var workTypes = db.WorkTypes.Include(w => w.UnitOfMeasure);
            return View(await workTypes.ToListAsync());
        }

        // GET: WorkTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkType workType = await db.WorkTypes.FindAsync(id);
            if (workType == null)
            {
                return HttpNotFound();
            }
            return View(workType);
        }

        // GET: WorkTypes/Create
        public ActionResult Create()
        {
            ViewBag.UnitOfMeasureId = new SelectList(db.UnitOfMeasures, "Id", "Name");
            return View();
        }

        // POST: WorkTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,UnitOfMeasureId")] WorkType workType)
        {
            if (ModelState.IsValid)
            {
                db.WorkTypes.Add(workType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.UnitOfMeasureId = new SelectList(db.UnitOfMeasures, "Id", "Name", workType.UnitOfMeasureId);
            return View(workType);
        }

        // GET: WorkTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkType workType = await db.WorkTypes.FindAsync(id);
            if (workType == null)
            {
                return HttpNotFound();
            }
            ViewBag.UnitOfMeasureId = new SelectList(db.UnitOfMeasures, "Id", "Name", workType.UnitOfMeasureId);
            return View(workType);
        }

        // POST: WorkTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,UnitOfMeasureId")] WorkType workType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UnitOfMeasureId = new SelectList(db.UnitOfMeasures, "Id", "Name", workType.UnitOfMeasureId);
            return View(workType);
        }

        // GET: WorkTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkType workType = await db.WorkTypes.FindAsync(id);
            if (workType == null)
            {
                return HttpNotFound();
            }
            return View(workType);
        }

        // POST: WorkTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WorkType workType = await db.WorkTypes.FindAsync(id);
            db.WorkTypes.Remove(workType);
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
