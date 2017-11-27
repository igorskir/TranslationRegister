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
    public class ProjectsController : Controller
    {
        private SqlContext db = new SqlContext();

        // GET: Projects
        public async Task<ActionResult> Index()
        {
            var projects = db.Projects.Include(p => p.FinalLanguage).Include(p => p.OriginalLanguage);
            return View(await projects.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await db.Projects.Include(x=>x.Documents).Where(x=>x.Id == id).FirstOrDefaultAsync();
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.FinalLanguageId = new SelectList(db.Languages, "Id", "Name");
            ViewBag.OriginalLanguageId = new SelectList(db.Languages, "Id", "Name");
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
                db.Projects.Add(project);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FinalLanguageId = new SelectList(db.Languages, "Id", "Name", project.FinalLanguageId);
            ViewBag.OriginalLanguageId = new SelectList(db.Languages, "Id", "Name", project.OriginalLanguageId);
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await db.Projects.FindAsync(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.FinalLanguageId = new SelectList(db.Languages, "Id", "Name", project.FinalLanguageId);
            ViewBag.OriginalLanguageId = new SelectList(db.Languages, "Id", "Name", project.OriginalLanguageId);
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
                db.Entry(project).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FinalLanguageId = new SelectList(db.Languages, "Id", "Name", project.FinalLanguageId);
            ViewBag.OriginalLanguageId = new SelectList(db.Languages, "Id", "Name", project.OriginalLanguageId);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = await db.Projects.FindAsync(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Project project = await db.Projects.FindAsync(id);
            db.Projects.Remove(project);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
