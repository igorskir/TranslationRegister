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
using System.IO;
using TranslationReg.Models;

namespace TranslationReg.Controllers
{
    public class DocumentsController : Controller
    {
        private SqlContext db = new SqlContext();

        // GET: Documents
        public async Task<ActionResult> Index()
        {
            return View(await db.Documents.ToListAsync());
        }

        // GET: Documents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.Documents.FindAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        public async Task<ActionResult> Create()
        {
            DocUploadModel viewmodel = new DocUploadModel();
            viewmodel.document = new Document();
            viewmodel.languages = new SelectList(await db.Languages.ToArrayAsync(), "Id", "ShortName");
            viewmodel.projects = new SelectList(await db.Projects.ToArrayAsync(), "Id", "Name");

            return View(viewmodel);
        }

        // GET: Documents/Create
        public ActionResult Download(string filepath)
        {
            if (System.IO.File.Exists(filepath))
            {
                FileInfo file = new FileInfo(filepath);
                byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
                string fileName = file.Name;
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            return HttpNotFound("Файл не найден");
        }

        // POST: Documents/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Name,Path")] Document document)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Documents.Add(document);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(document);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Document document, HttpPostedFileBase file)
        {
            //todo исправить при вводе юзеров
            document.UserId = 1;
            if (file!= null && file.ContentLength != 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                document.Path = path;

                if (ModelState.IsValid)
                {
                    db.Documents.Add(document);
                    await db.SaveChangesAsync();
                    file.SaveAs(path);
                    return RedirectToAction("Index");
                }
            }

            return View(document);
        }

        // GET: Documents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.Documents.FindAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            ViewBag.LanguageId = new SelectList(db.Languages, "Id", "ShortName", document.LanguageId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", document.ProjectId);
            return View(document);
        }

        // POST: Documents/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.Documents.FindAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Document document = await db.Documents.FindAsync(id);
            db.Documents.Remove(document);
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
