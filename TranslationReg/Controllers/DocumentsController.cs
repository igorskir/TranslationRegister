﻿using System;
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
            return PartialView(await Rep.GetDocuments());
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
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            
            
            foreach (var file in files)
            {
                string Name = file.FileName;
                
                string fileName = Path.GetFileName(file.FileName);
                // сохраняем файл в папку Files в проекте
                string filePath = Server.MapPath("~/Uploads/" + fileName);
                file.SaveAs(filePath);
                
            }
            return Json("file uploaded successfully");
        }
     


        // POST: Documents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Document document, [Bind(Include = "original")] HttpPostedFileBase original, [Bind(Include = "final")] HttpPostedFileBase final)
        {
            // дополняем информацию о владельце и дате
            document.OwnerId = (await Rep.GetUser(User.Identity.Name)).Id;
            document.Date = DateTime.Now;


            if (original != null && original.ContentLength != 0)
            {
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
                    catch (Exception){}
                }
           
                // если прикрепили сразу перевод, добавляем в базу
                if (final != null && final.ContentLength != 0)
                    document.FinalFileId = (await Helper.SetFile(final, Rep, Server)).Id;

                if (ModelState.IsValid)
                {
                    // сохраняем док
                    await Rep.AddDocument(document);

                    // добавляем стадию автоматом. Id дока обновился после сохранения
                    var project = await Rep.GetProject(document.ProjectId.Value);
                    DocStage initialStage = new DocStage {
                        OriginalId = document.OriginalFileId,
                        DocumentId = document.Id,
                        WorkTypeId = project.WorkTypeId.Value,
                    };
                    await Rep.AddDocStage(initialStage);

                    return Redirect("InWork");
                }
                else
                    await Rep.DeleteDocFile(originalFile.Id);
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
    }
}
