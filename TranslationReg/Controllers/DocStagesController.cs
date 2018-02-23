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
    public class DocStagesController : RepositoryController
    {
        public DocStagesController(IRepository repository) : base(repository) { }

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
        public async Task<ActionResult> Create(int? id)
        {
            //проверка привязки к документу
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Document parentDoc = await Rep.GetDocument(id.Value);
            if (parentDoc == null)
                return HttpNotFound();

            //список доступных стадий
            List<WorkType> availableWorkTypes = await Rep.GetWorkTypes();
            List<WorkType> takenWorktypes = parentDoc.Stages.Select(x => x.WorkType).ToList();
            availableWorkTypes = availableWorkTypes.Except(takenWorktypes).ToList();
            ViewBag.WorkTypeId = new SelectList(availableWorkTypes, "Id", "Name");
            //флаг возможности добавления стадии
            ViewBag.Available = (availableWorkTypes.Count != 0);

            DocStage stage = new DocStage() { DocumentId = parentDoc.Id };
            return PartialView(stage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DocStage docStage)
        {
            if (ModelState.IsValid)
            {
                await Rep.AddDocStage(docStage);
                return Redirect(Request.UrlReferrer.ToString());
            }

            ViewBag.WorkTypeId = new SelectList(await Rep.GetWorkTypes(), "Id", "Name");
            return RedirectToAction("Details","",docStage);
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
                return Redirect(Request.UrlReferrer.ToString());
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

            return PartialView(docStage);
        }

        // POST: DocStages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await Rep.DeleteDocStage(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}
