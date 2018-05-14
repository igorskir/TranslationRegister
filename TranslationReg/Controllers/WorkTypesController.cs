using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TranslationReg.Models;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    // Контроллер обработки ТИПОВ РАБОТ
    [Authorize]
    public class WorkTypesController : RepositoryController
    {
        public WorkTypesController(IRepository repository) : base(repository) { } // Конструктор

        // GET: WorkTypes/Card
        public async Task<ActionResult> Card(int id)
        {
            return PartialView(await Rep.GetWorkType(id));
        }
        // GET: WorkTypes/Cards
        public async Task<ActionResult> Cards()
        {
            var workTypesModel = await WorkTypesModel.GetModel(Rep);
            if (Request.IsAjaxRequest())
                return PartialView(workTypesModel);
            return View(workTypesModel);
        }

        // GET: WorkTypes/AddCard
        public async Task<ActionResult> AddCard()
        {
            var workTypeModel = await WorkTypeModel.GetModel(Rep);
            return PartialView(workTypeModel);
        }
        // POST: WorkTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(WorkType workType)
        {
            if (ModelState.IsValid)
            {
                await Rep.AddWorkType(workType);
                return Redirect(Request.UrlReferrer.ToString());
            }

            ViewBag.UnitOfMeasureId = new SelectList(await Rep.GetUnitsOfMeasure(), "Id", "Name", workType.UnitOfMeasureId);
            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: WorkTypes/EditCard/5
        public async Task<ActionResult> EditCard(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            WorkType workType = await Rep.GetWorkType(id.Value);

            if (workType == null)
                return HttpNotFound();

            var workTypeModel = await WorkTypeModel.GetModel(Rep, workType);
            return PartialView(workTypeModel);
        }
        // POST: WorkTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(WorkType workType)
        {
            if (ModelState.IsValid)
            {
                await Rep.PutWorkType(workType);
                return Redirect(Request.UrlReferrer.ToString());
            }
            return View(workType);
        }

        // GET: WorkTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            WorkType workType = await Rep.GetWorkType(id.Value);

            if (workType == null)
                return HttpNotFound();
            return PartialView(workType);
        }
        // POST: WorkTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await Rep.DeleteWorkType(id);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
