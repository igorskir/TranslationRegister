using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    // Контроллер обработки СТАДИЙ (этапов работ) внутри документа
    [Authorize]
    public class DocStagesController : RepositoryController
    {
        public DocStagesController(IRepository repository) : base(repository) { } // Конструктор

        // GET: DocStages/Create
        // Принимает id документа для которого создается
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

        // GET: DocStages/CreateModal
        // Принимает id документа для которого создается
        public async Task<ActionResult> CreateModal(int? id)
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

            var model = await TranslationReg.Models.StageForDocModel.GetModel(Rep, parentDoc);
            return PartialView(model);
        }

        // POST: DocStages/Create
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
            return RedirectToAction("Details", "", docStage);
        }

        // POST: DocStages/CreateForDoc
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateForDoc(int? DocId, int? WorkTypeId, HttpPostedFileBase stageOriginal)
        {
            //проверка наличия файла и привязки к документу и типу работ
            if (DocId == null || WorkTypeId == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Document parentDoc = await Rep.GetDocument(DocId.Value);
            WorkType workType = await Rep.GetWorkType(WorkTypeId.Value);

            if (parentDoc == null || parentDoc == null || stageOriginal == null)
                return HttpNotFound();

            //здесь и сохранение файла и кортежа DocFile
            var originalFile = (await Helper.SetFile(stageOriginal, Rep, Server));
            // дополняем информацию об оригинале
            DocStage stage = new DocStage
            {
                DocumentId = parentDoc.Id,
                WorkTypeId = workType.Id,
            };
            stage.OriginalId = originalFile.Id;
            await Rep.AddDocStage(stage);

            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: Documents/AddOriginalFile/5
        // принимает id стадии
        public async Task<ActionResult> AddOriginalFile(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DocStage stage = await Rep.GetDocStage(id.Value);

            if (stage == null)
                return HttpNotFound();

            return PartialView(stage);
        }
        // POST: Documents/AddOriginalFile
        [HttpPost]
        public async Task<ActionResult> AddOriginalFile(int? id, HttpPostedFileBase original)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DocStage stage = await Rep.GetDocStage(id.Value);
            if (stage == null || original == null)
                return HttpNotFound();

            // добавляем информацию о финальном файле
            var originalFile = (await Helper.SetFile(original, Rep, Server));
            stage.OriginalId = originalFile.Id;
            await Rep.PutDocStage(stage);

            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: Documents/AddResultFile/5
        // принимает id стадии
        public async Task<ActionResult> AddFinalFile(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DocStage stage = await Rep.GetDocStage(id.Value);

            if (stage == null)
                return HttpNotFound();

            return PartialView(stage);
        }
        // POST: Documents/AddFinalFile
        [HttpPost]
        public async Task<ActionResult> AddFinalFile(int? id, HttpPostedFileBase result)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DocStage stage = await Rep.GetDocStage(id.Value);

            if (stage == null || result == null)
                return HttpNotFound();

            // добавляем информацию о финальном файле
            var finalStageFile = (await Helper.SetFile(result, Rep, Server));
            stage.ResultId = finalStageFile.Id;
            await Rep.PutDocStage(stage);

            return Redirect(Request.UrlReferrer.ToString());
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
        // POST: DocStages/Edit/5
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
