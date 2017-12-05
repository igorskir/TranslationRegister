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
        //private SqlContext db = new SqlContext();

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
            ViewBag.FinalLanguageId = new SelectList(await rep.GetLanguages(), "Id", "Name");
            ViewBag.OriginalLanguageId = new SelectList(await rep.GetLanguages(), "Id", "Name");
            return View();
        }

        // POST: Projects/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,OriginalLanguageId,FinalLanguageId,WordsNumber")] Project project)
        {
            if (ModelState.IsValid)
            {
                await rep.AddProject(project);
                return RedirectToAction("Index");
            }

            ViewBag.FinalLanguageId = new SelectList(await rep.GetLanguages(), "Id", "Name", project.FinalLanguageId);
            ViewBag.OriginalLanguageId = new SelectList(await rep.GetLanguages(), "Id", "Name", project.OriginalLanguageId);
            return View(project);
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
            ViewBag.FinalLanguageId = new SelectList(languages, "Id", "Name", project.FinalLanguageId);
            ViewBag.OriginalLanguageId = new SelectList(languages, "Id", "Name", project.OriginalLanguageId);
            return View(project);
        }

        // POST: Projects/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,OriginalLanguageId,FinalLanguageId,WordsNumber")] Project project)
        {
            if (ModelState.IsValid)
            {
                await rep.PutProject(project);
                return RedirectToAction("Index");
            }

            var languages = await rep.GetLanguages();
            ViewBag.FinalLanguageId = new SelectList(languages, "Id", "Name", project.FinalLanguageId);
            ViewBag.OriginalLanguageId = new SelectList(languages, "Id", "Name", project.OriginalLanguageId);
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
