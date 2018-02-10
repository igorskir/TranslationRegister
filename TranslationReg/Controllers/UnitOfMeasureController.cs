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
        public IRepository Rep { get; set; }
        public UnitOfMeasureController(IRepository repository)
        {
            this.Rep = repository;
        }

        // GET: UnitOfMeasure/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UnitOfMeasure/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UnitOfMeasure unitOfMeasure)
        {
            if (ModelState.IsValid)
            {
                await Rep.AddUnitOfMeasure(unitOfMeasure);
                return RedirectToAction("Index");
            }

            return View(unitOfMeasure);
        }

        // GET: UnitOfMeasure/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            UnitOfMeasure unitOfMeasure = await Rep.GetUnitOfMeasure(id.Value);

            if (unitOfMeasure == null)
                return HttpNotFound();

            return View(unitOfMeasure);
        }

        // POST: UnitOfMeasure/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( UnitOfMeasure unitOfMeasure)
        {
            if (ModelState.IsValid)
            {
                await Rep.PutUnitOfMeasure(unitOfMeasure);
                return Redirect(Request.UrlReferrer.ToString());
            }
            return View(unitOfMeasure);
        }

        // GET: UnitOfMeasure/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            UnitOfMeasure unitOfMeasure = await Rep.GetUnitOfMeasure(id.Value);

            if (unitOfMeasure == null)
                return HttpNotFound();

            return View(unitOfMeasure);
        }

        // POST: UnitOfMeasure/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await Rep.DeleteUnitOfMeasure(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Rep.Dispose();
            base.Dispose(disposing);
        }
    }
}
