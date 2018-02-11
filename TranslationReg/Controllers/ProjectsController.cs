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
    public class ProjectsController : RepositoryController
    {
        public ProjectsController(IRepository repository) : base(repository) { }

        // GET: Projects
        public async Task<ActionResult> Index()
        {
            var projects = await Rep.GetProjects();
            return View(projects);
        }

        // GET: Projects/Tabpages
        public async Task<ActionResult> Tabpages(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Project project = await Rep.GetProject(id.Value);

            if (project == null)
                return HttpNotFound();

            return View(project);
        }

        // GET: Projects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Project project = await Rep.GetProject(id.Value);

            if (project == null)
                return HttpNotFound();

            return View(project);
        }

        // GET: Projects/DetailsShort/5
        public async Task<ActionResult> DetailsShort(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Project project = await Rep.GetProject(id.Value);

            if (project == null)
                return HttpNotFound();

            return PartialView(project);
        }

        // GET: Projects/Create
        public async Task<ActionResult> Create()
        {
            ProjectModel model = await ProjectModel.GetModel(Rep);
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Project project)
        {
            project.CreatorId = (await Rep.GetUser(User.Identity.Name)).Id;
            if (ModelState.IsValid)
            {
                await Rep.AddProject(project);
                return Redirect(Request.UrlReferrer.ToString());
            }

            ProjectModel model = await ProjectModel.GetModel(Rep);
            model.Project = project;
            return PartialView(model);
        }

        // GET: Projects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Project project = await Rep.GetProject(id.Value);

            if (project == null)
                return HttpNotFound();

            ProjectModel model = await ProjectModel.GetModel(Rep, project);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                await Rep.PutProject(project);
                return Redirect(Request.UrlReferrer.ToString());
            }

            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Project project = await Rep.GetProject(id.Value);

            if (project == null)
                return HttpNotFound();

            return PartialView(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await Rep.DeleteProject(id);
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
