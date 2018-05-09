using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationReg.Models;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    // Контроллер обработки ДОКУМЕНТОВ
    [Authorize]
    public class DocumentsController : RepositoryController
    {
        public DocumentsController(IRepository repository) : base(repository) { } // Конструктор

        // GET: Documents
        public async Task<ActionResult> Index()
        {
            if (Request.IsAjaxRequest())
                return PartialView(await Rep.GetDocuments());
            return View(await Rep.GetDocuments());
        }
        
        //                                          ФИЛЬТРЫ
        // GET: Documents/All
        public async Task<ActionResult> All()
        {
            var documents = await Rep.GetDocuments();
            return PartialView("List", documents);
        }
        // GET: Documents/My
        public async Task<ActionResult> My()
        {
            var documents = await Rep.GetMyDocuments(User.Identity.Name);
            return PartialView("List", documents);
        }
        // GET: Documents/InWork
        public async Task<ActionResult> InWork()
        {
            var documents = await Rep.GetMyWorkDocuments(User.Identity.Name);
            return PartialView("List", documents);
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
        public async Task<ActionResult> AddFinalFile(int? id, HttpPostedFileBase file)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Document doc = await Rep.GetDocument(id.Value);

            if (doc == null)
                return HttpNotFound();

            // добавляем информацию о финальном файле
            var finalDocFile = (await Helper.SetFile(file, Rep, Server));
            doc.FinalFileId = finalDocFile.Id;

            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: Documents/Create
        public async Task<ActionResult> Create()
        {
            DocumentModel DocCreateViewModel = await DocumentModel.GetModel(Rep);
            return PartialView(DocCreateViewModel);
        }
        // GET: Documents/CreateProjectDoc/1
        public ActionResult CreateProjectDoc(int Id)
        {
            return PartialView(new Document { ProjectId = Id });
        }

        public ActionResult AddProjectDocs()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> UploadFiles(int id, IEnumerable<HttpPostedFileBase> files)
        {
            foreach (var original in files)
            {

                Document document = new Document
                {
                    ProjectId = id,
                    OwnerId = (await Rep.GetUser(User.Identity.Name)).Id,
                    Date = DateTime.Now
                };

                if (original != null && original.ContentLength != 0)
                {
                    document.Name = HttpUtility.UrlDecode(original.FileName);
                    //здесь и сохранение файла и кортежа DocFile
                    var originalFile = (await Helper.SetFile(original, Rep, Server));
                    // дополняем информацию об оригинале
                    document.OriginalFileId = originalFile.Id;

                    // определение числа слов автоматом
                    var MsWordDocMime = "application/msword";
                    var MsWordDocxMime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    if (original.ContentType == MsWordDocMime || original.ContentType == MsWordDocxMime)
                    {
                        try
                        {
                            using (Syncfusion.DocIO.DLS.WordDocument doc = new Syncfusion.DocIO.DLS.WordDocument(originalFile.Path))
                            {
                                doc.UpdateWordCount(false);
                                int wordCount = doc.BuiltinDocumentProperties.WordCount;
                                document.WordsNumber = wordCount;
                            }
                        }
                        catch (Exception) { }
                    }

                    if (ModelState.IsValid)
                    {
                        
                        // сохраняем док
                        await Rep.AddDocument(document);

                        // добавляем стадию автоматом. Id дока обновился после сохранения
                        var project = await Rep.GetProject(document.ProjectId.Value);
                        DocStage initialStage = new DocStage
                        {
                            OriginalId = document.OriginalFileId,
                            DocumentId = document.Id,
                            WorkTypeId = project.WorkTypeId.Value,
                        };
                        await Rep.AddDocStage(initialStage);

                        //return new HttpStatusCodeResult(HttpStatusCode.OK);
                        //return View("Index", await Rep.GetDocuments());
                    }
                    else
                        await Rep.DeleteDocFile(originalFile.Id);
                    //todo refresh
                }

            }

            return View("Index", await Rep.GetDocuments());
        }



        // POST: Documents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Document document, [Bind(Include = "original")] IEnumerable<HttpPostedFileBase> original, [Bind(Include = "final")] HttpPostedFileBase final)
        {

            foreach (var originalOne in original)
            {
                Document NewDoc = new Document
                {
                    ProjectId = document.ProjectId,
                    OwnerId = (await Rep.GetUser(User.Identity.Name)).Id,
                    Date = DateTime.Now
                };
                if (originalOne != null && originalOne.ContentLength != 0)
                {
                    NewDoc.Name = originalOne.FileName;
                    //здесь и сохранение файла и кортежа DocFile
                    var originalFile = (await Helper.SetFile(originalOne, Rep, Server));
                    // дополняем информацию об оригинале
                    NewDoc.OriginalFileId = originalFile.Id;

                    // определение числа слов автоматом
                    var MsWordDocMime = "application/msword";
                    var MsWordDocxMime = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    if (originalOne.ContentType == MsWordDocMime || originalOne.ContentType == MsWordDocxMime)
                    {
                        try
                        {
                            using (Syncfusion.DocIO.DLS.WordDocument doc = new Syncfusion.DocIO.DLS.WordDocument(originalFile.Path))
                            {
                                doc.UpdateWordCount(false);
                                int wordCount = doc.BuiltinDocumentProperties.WordCount;
                                NewDoc.WordsNumber = wordCount;
                            }
                        }
                        catch (Exception) { }
                    }


                    if (ModelState.IsValid)
                    {
                        // сохраняем док
                        await Rep.AddDocument(NewDoc);

                        // добавляем стадию автоматом. Id дока обновился после сохранения
                        var project = await Rep.GetProject(document.ProjectId.Value);
                        DocStage initialStage = new DocStage
                        {
                            OriginalId = NewDoc.OriginalFileId,
                            DocumentId = NewDoc.Id,
                            WorkTypeId = project.WorkTypeId.Value,
                        };
                        await Rep.AddDocStage(initialStage);

                        //return View("Index", await Rep.GetDocuments());
                        //return Redirect(Request.UrlReferrer.ToString());


                        // если прикрепили сразу перевод, добавляем в базу
                        if (final != null && final.ContentLength != 0)
                            NewDoc.FinalFileId = (await Helper.SetFile(final, Rep, Server)).Id;
                        
                    }
                    else
                        await Rep.DeleteDocFile(originalFile.Id);
                }
                
            }
            return View("Index", await Rep.GetDocuments());
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

                return PartialView(document);
            } 

        // POST: Documents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Document document, [Bind(Include = "newOriginalFile")] HttpPostedFileBase newOriginalFile)
        {
            if (ModelState.IsValid)
            {
                if (newOriginalFile != null && newOriginalFile.ContentLength != 0)
                {
                    var newFile = await Helper.SetFile(newOriginalFile, Rep, Server);
                    document.OriginalFileId = newFile.Id;
                }

                await Rep.PutDocument(document);
                return Redirect(Request.UrlReferrer.ToString());
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

            return PartialView(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await Rep.DeleteDocument(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        // POST: Documents/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteFromList(int id)
        {
            await Rep.DeleteDocument(id);
            return RedirectToAction("Index");
        }
    }
}
