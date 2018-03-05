﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;
using System.IO;
using TranslationReg.Models;

namespace TranslationReg.Controllers
{
    [Authorize]
    public class User_StageController : RepositoryController
    {
        public User_StageController(IRepository repository) : base(repository) { }

        // GET: User_Stage
        public async Task<ActionResult> Index()
        {
            return View(await Rep.GetUser_Stages());
        }

        public async Task<ActionResult> Finalise(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User_Stage user_Stage = await Rep.GetUser_Stage(id.Value);

            user_Stage.Stage.Document.FinalFileId = user_Stage.DocFileId;
            await Rep.PutDocument(user_Stage.Stage.Document);
            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: User_Stage/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User_Stage user_Stage = await Rep.GetUser_Stage(id.Value);

            if (user_Stage == null)
                return HttpNotFound();

            return View(user_Stage);
        }

        // GET: User_Stage/Create
        // принимает id стадии
        public async Task<ActionResult> Create(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            DocStage docStage = await Rep.GetDocStage(id.Value);
            if (docStage == null)
                return HttpNotFound();

            return PartialView(await Models.User_StageModel.GetModel(Rep, User.Identity.Name, id.Value));
        }

        // POST: User_Stage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User_Stage user_Stage,[Bind(Include = "workFile")]HttpPostedFileBase workFile)
        {
            user_Stage.Date = DateTime.Now;
            if (user_Stage.UserId == 0)
                user_Stage.UserId = (await Rep.GetUser(User.Identity.Name)).Id;
            if (workFile != null && workFile.ContentLength != 0)
                user_Stage.DocFile = await Helper.SetFile(workFile, Rep, Server);

            if (ModelState.IsValid)
            {
                await Rep.AddUser_Stage(user_Stage);
                return Redirect(Request.UrlReferrer.ToString());
            }

            return View(user_Stage);
        }

        // GET: User_Stage/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User_Stage user_Stage = await Rep.GetUser_Stage(id.Value);

            if (user_Stage == null)
                return HttpNotFound();

            User_StageModel model = await User_StageModel.GetModel(Rep, User.Identity.Name, user_Stage.StageId, user_Stage);
            return PartialView(model);
        }

        // POST: User_Stage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User_Stage user_Stage,[Bind(Include = "workFile")] HttpPostedFileBase workFile)
        {
            if (workFile != null && workFile.ContentLength != 0)
                user_Stage.DocFileId = (await Helper.SetFile(workFile, Rep, Server)).Id;

            if (ModelState.IsValid)
            {
                await Rep.PutUser_Stage(user_Stage);
                return Redirect(Request.UrlReferrer.ToString());
            }

            return View(user_Stage);
        }

        // GET: User_Stage/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User_Stage user_Stage = await Rep.GetUser_Stage(id.Value);
            if (user_Stage == null)
                return HttpNotFound();

            return PartialView(user_Stage);
        }

        // POST: User_Stage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await Rep.DeleteUser_Stage(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        // GET: User_Stage/Download
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
    }
}
