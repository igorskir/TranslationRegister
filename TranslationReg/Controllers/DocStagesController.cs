using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TranslationReg.Models;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    public class DocStagesController : Controller
    {
        IRepository Rep;
        public DocStagesController(IRepository Rep)
        {
            this.Rep = Rep;
        }

        // GET: DocStages
        public async Task<ActionResult> Index()
        {
            return View(await Rep.GetDocStages());
        }

        // GET: DocStages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DocStage docStage = await Rep.GetDocStage(id.Value);

            if (docStage == null)
                return HttpNotFound();

            return View(docStage);
        }

        // GET: DocStages/Create
        // принимает id документа для которого создается
        public async Task<ActionResult> Create(int Id)
        {
            ViewBag.WorkTypeId = new SelectList(await Rep.GetWorkTypes(), "Id", "Name");
            DocStage stage = new DocStage() { DocumentId = Id };
            return PartialView(stage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DocStage docStage)
        {
            if (ModelState.IsValid)
            {
                await Rep.AddDocStage(docStage); 
                return RedirectToAction("Index");
            }

            ViewBag.WorkTypeId = new SelectList(await Rep.GetWorkTypes(), "Id", "Name");
            return View(docStage);
        }

        // GET: DocStages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DocStage docStage = await Rep.GetDocStage(id.Value);

            if (docStage == null)
                return HttpNotFound();

            ViewBag.WorkTypeId = new SelectList(await Rep.GetWorkTypes(), "Id", "Name");
            return View(docStage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DocStage docStage)
        {
            if (ModelState.IsValid)
            {
                await Rep.PutDocStage(docStage);
                return RedirectToAction("Index");
            }
            ViewBag.WorkTypeId = new SelectList(await Rep.GetWorkTypes(), "Id", "Name");
            return View(docStage);
        }

        // GET: DocStages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DocStage docStage = await Rep.GetDocStage(id.Value);

            if (docStage == null)
                return HttpNotFound();

            return View(docStage);
        }

        // POST: DocStages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await Rep.DeleteDocStage(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Rep.Dispose();

            base.Dispose(disposing);
        }
    }
}
