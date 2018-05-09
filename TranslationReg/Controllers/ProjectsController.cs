using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TranslationReg.Models;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    // Контроллер обработки ПРОЕКТОВ
    [Authorize]
    public class ProjectsController : RepositoryController
    {
        public ProjectsController(IRepository repository) : base(repository) { } // Конструктор

        // GET: Projects
        public async Task<ActionResult> Index()
        {
            var projects = await Rep.GetProjectsInWork();
            if (this.Request.IsAjaxRequest())
                return PartialView(projects);
            return View(projects);
        }

        // GET: Project
        public async Task<ActionResult> Project(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Project project = await Rep.GetProject(id.Value);

            if (project == null)
                return HttpNotFound();

            return View(project);
        }

        // GET: Projects
        public async Task<ActionResult> Projects()
        {
            var projects = await Rep.GetProjectsInWork();

            if (this.Request.IsAjaxRequest())
                return PartialView("Index", projects);
            return View("Index", projects);
        }
        //                                          ФИЛЬТРЫ
        // GET: Projects/All
        public async Task<ActionResult> All()
        {
            var projects = await Rep.GetProjects();
            return PartialView("List", projects);
        }
        // GET: Projects/My
        public async Task<ActionResult> My()
        {
            var projects = await Rep.GetMyProjects(User.Identity.Name);
            return PartialView("List",projects);
        }
        // GET: Projects/InWork
        public async Task<ActionResult> InWork()
        {
            var projects = await Rep.GetProjectsInWork();
            return PartialView("List", projects);
        }
        // GET: Projects/Done
        public async Task<ActionResult> Done()
        {
            var projects = await Rep.GetDoneProjects();
            return PartialView("List", projects);
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
        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Project project)
        {
            project.Date = DateTime.Now;
            project.CreatorId = (await Rep.GetUser(User.Identity.Name)).Id;
            if (ModelState.IsValid)
            {
                await Rep.AddProject(project);
                return RedirectToAction("InWork");
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
        // GET: Projects/EditModal/5
        public async Task<ActionResult> EditModal(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Project project = await Rep.GetProject(id.Value);

            if (project == null)
                return HttpNotFound();

            ProjectModel model = await ProjectModel.GetModel(Rep, project);
            return PartialView(model);
        }
        // POST: Projects/Edit/5
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

        // POST: Projects/DeleteFromList/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteFromList(int id)
        {
            await Rep.DeleteProject(id);
            return RedirectToAction("Index");
        }
        [HttpGet]

        public async Task<JsonResult> DeadlineSet(int project, int workTypeid, DateTime deadline)
        {
            var obj = await Rep.GetDeadline(project, workTypeid);
            obj.Date = deadline;
            await Rep.PutDeadline(obj);
            return Json("Дедлайн обновлен", JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public async Task<JsonResult> DeadlineGet(int project, int workTypeid)
        {
            var result = await Rep.GetDeadline(project, workTypeid);
            if (result != null)
                return Json(result.Date.ToString("yyyy-MM-dd"), JsonRequestBehavior.AllowGet);
            else
                throw new Exception("Нет данный о дедлайне");
        }
    }
}
