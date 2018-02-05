using System;
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

namespace TranslationReg.Controllers
{
    public class User_StageController : Controller
    {
        IRepository Rep;
        public User_StageController(IRepository Rep)
        {
            this.Rep = Rep;
        }

        // GET: User_Stage
        public async Task<ActionResult> Index()
        {
            return View(await Rep.GetUser_Stages());
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

            User_Stage userStage = new User_Stage { StageId = docStage.Id };

            return PartialView(userStage);
        }

        // POST: User_Stage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User_Stage user_Stage, [Bind(Include = "workFile")]HttpPostedFileBase workFile)
        {
            user_Stage.UserId = (await Rep.GetUser(User.Identity.Name)).Id;
            if (workFile != null && workFile.ContentLength != 0)
            {
                user_Stage.DocFile = await Helper.SetFile(workFile, Rep, Server);
            }

            if (ModelState.IsValid)
            {
                await Rep.AddUser_Stage(user_Stage);
                workFile.SaveAs(user_Stage.DocFile.Path);
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

            return View(user_Stage);
        }

        // POST: User_Stage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Amount,StageId,UserId,DocFileId")] User_Stage user_Stage)
        {
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Rep.Dispose();

            base.Dispose(disposing);
        }
    }
}
