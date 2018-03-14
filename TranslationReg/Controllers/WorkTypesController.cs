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
using TranslationReg.Models;

namespace TranslationReg.Controllers
{
    [Authorize]
    public class WorkTypesController : RepositoryController
    {
        public WorkTypesController(IRepository repository) : base(repository) { }

        // GET: WorkTypes
        public async Task<ActionResult> Index()
        {
            var workTypesModel = await WorkTypesModel.GetModel(Rep);
            return View(workTypesModel);
        }

        // GET: WorkTypes/Cards
        public async Task<ActionResult> Cards()
        {
            var workTypesModel = await WorkTypesModel.GetModel(Rep);
            return View(workTypesModel);
        }

        // GET: WorkTypes/AddCard
        public async Task<ActionResult> AddCard()
        {
            var workTypeModel = await WorkTypeModel.GetModel(Rep);
            return PartialView(workTypeModel);
        }

        // GET: WorkTypes/EditCard
        public async Task<ActionResult> EditCard(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            WorkType workType = await Rep.GetWorkType(id.Value);

            if (workType == null)
                return HttpNotFound();

            var workTypeModel = await WorkTypeModel.GetModel(Rep, workType);
            return View(workTypeModel);
        }

        // GET: WorkTypes/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.UnitOfMeasureId = new SelectList(await Rep.GetUnitsOfMeasure(), "Id", "Name");
            return PartialView();
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
            return View(workType);
        }

        // GET: WorkTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            WorkType workType = await Rep.GetWorkType(id.Value);

            if (workType == null)
                return HttpNotFound();

            ViewBag.UnitOfMeasureId = new SelectList(await Rep.GetUnitsOfMeasure(), "Id", "Name", workType.UnitOfMeasureId);
            return PartialView(workType);
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
            ViewBag.UnitOfMeasureId = new SelectList(await Rep.GetUnitsOfMeasure(), "Id", "Name", workType.UnitOfMeasureId);
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
