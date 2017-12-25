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
    [Authorize]
    public class DocumentsController : Controller
    {
        public IRepository rep { get; set; }
        public DocumentsController(IRepository repository)
        {
            this.rep = repository;
        }

        // GET: Documents
        public async Task<ActionResult> Index()
        {
            return View(await rep.GetDocuments());
        }

        // GET: Documents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Document document = await rep.GetDocument(id.Value);

            if (document == null)
                return HttpNotFound();

            return View(document);
        }

        // GET: Documents/Create
        public async Task<ActionResult> Create()
        {
            DocumentModel DocCreateViewModel = await DocumentModel.GetModel(rep);
            return View(DocCreateViewModel);
        }

        // GET: Documents/Download
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

        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        //public async Task<ActionResult> Create([Bind(Include = "Id,Name,Path")] Document document)

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Document document, [Bind(Include = "originalFile")] HttpPostedFileBase originalFile, [Bind(Include = "finalFile")] HttpPostedFileBase finalFile)
        {
            document.OwnerId = (await rep.GetUser(User.Identity.Name)).Id;

            if (originalFile!= null && originalFile.ContentLength != 0)
            {

                document.OriginalFile = await SetFile(originalFile);

                if (finalFile != null && finalFile.ContentLength != 0)
                    document.FinalFile = await SetFile(finalFile); ;

                if (ModelState.IsValid)
                {
                    originalFile.SaveAs(document.OriginalFile.Path);

                    if (document.FinalFile!=null)
                        finalFile.SaveAs(document.FinalFile.Path);

                    await rep.AddDocument(document);
                    return RedirectToAction("Index");
                }
            }

            return View(document);
        }

        private async Task<DocFile> SetFile(HttpPostedFileBase file)
        {
            DocFile docfile = new DocFile();
            docfile.Date = DateTime.Now;
            docfile.Path = GetValidPath(file);
            await rep.AddDocFile(docfile);
            return docfile;
        }

        private string GetValidPath(HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
            if (System.IO.File.Exists(path))
            {
                Guid guid = Guid.NewGuid();
                fileName = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                fileName += guid.ToString() + extension;
                path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
            }
            return path;
        }

        // GET: Documents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Document document = await rep.GetDocument(id.Value);

            if (document == null)
                return HttpNotFound();

            //todo viewmodel
            ViewBag.ProjectId = new SelectList(await rep.GetLanguages(), "Id", "Name", document.ProjectId);

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
                await rep.PutDocument(document);
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Document document = await rep.GetDocument(id.Value);

            if (document == null)
                return HttpNotFound();

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await rep.DeleteDocument(id);
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
