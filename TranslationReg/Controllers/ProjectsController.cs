using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SqlRepository;
using TranslationRegistryModel;
using TranslationReg.Models;

namespace TranslationReg.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        public IRepository rep { get; set; }
        public ProjectsController(IRepository repository)
        {
            this.rep = repository;
        }

        // GET: Projects
        public async Task<ActionResult> Index()
        {
            var projects = await rep.GetProjects();
            return View(projects);
        }

        // GET: Projects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Project project = await rep.GetProject(id.Value);

            if (project == null)
                return HttpNotFound();

            return View(project);
        }

        // GET: Projects/Create
        public async Task<ActionResult> Create()
        {
            ProjectModel model = await ProjectModel.GetModel(rep);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Project project)
        {
            project.CreatorId = (await rep.GetUser(User.Identity.Name)).Id;
            if (ModelState.IsValid)
            {
                await rep.AddProject(project);
                return RedirectToAction("Index");
            }

            ProjectModel model = await ProjectModel.GetModel(rep);
            model.Project = project;
            return View(model);
        }

        // GET: Projects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Project project = await rep.GetProject(id.Value);

            if (project == null)
                return HttpNotFound();

            var languages = await rep.GetLanguages();
            ViewBag.LanguagePairs = new SelectList(await rep.GetLanguagePairs(), "Id", "Name", project.LanguagePairId);
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                await rep.PutProject(project);
                return RedirectToAction("Index");
            }

            var languages = await rep.GetLanguages();
            ViewBag.LanguagePair = new SelectList(await rep.GetLanguagePairs(), "Id", "Name", project.LanguagePairId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Project project = await rep.GetProject(id.Value);

            if (project == null)
                return HttpNotFound();

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await rep.DeleteProject(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                rep.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
