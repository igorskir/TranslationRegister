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
    public class DocumentsController : RepositoryController
    {
        public DocumentsController(IRepository repository) : base(repository) { }

        // GET: Documents
        public async Task<ActionResult> Index()
        {
            return View(await Rep.GetDocuments());
        }

        // GET: Documents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Document document = await Rep.GetDocument(id.Value);

            if (document == null)
                return HttpNotFound();

            return View(document);
        }

        // GET: Documents/DocAndStages/5
        public async Task<ActionResult> DocAndStages(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Document doc = await Rep.GetDocument(id.Value);

            if (doc == null)
                return HttpNotFound();

            return View(doc);
        }

        // GET: Documents/Create
        public async Task<ActionResult> Create()
        {
            DocumentModel DocCreateViewModel = await DocumentModel.GetModel(Rep);
            return PartialView(DocCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( Document document, [Bind(Include = "originalFile")] HttpPostedFileBase originalFile, [Bind(Include = "finalFile")] HttpPostedFileBase finalFile)
        {
            document.OwnerId = (await Rep.GetUser(User.Identity.Name)).Id;

            if (originalFile!= null && originalFile.ContentLength != 0)
            {

                document.OriginalFile = await Helper.SetFile(originalFile,Rep,Server);

                if (finalFile != null && finalFile.ContentLength != 0)
                    document.FinalFile = await Helper.SetFile(originalFile, Rep, Server);

                if (ModelState.IsValid)
                {
                    originalFile.SaveAs(document.OriginalFile.Path);

                    if (document.FinalFile!=null)
                        finalFile.SaveAs(document.FinalFile.Path);

                    await Rep.AddDocument(document);
                    return Redirect(Request.UrlReferrer.ToString());
                }
            }

            return View(document);
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

        // GET: Documents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Document document = await Rep.GetDocument(id.Value);

            if (document == null)
                return HttpNotFound();

            //todo viewmodel
            ViewBag.ProjectId = new SelectList(await Rep.GetProjects(), "Id", "Name", document.ProjectId);

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
                await Rep.PutDocument(document);
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Document document = await Rep.GetDocument(id.Value);

            if (document == null)
                return HttpNotFound();

            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await Rep.DeleteDocument(id);
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
