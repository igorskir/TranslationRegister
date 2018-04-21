using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationReg.Models;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    // Контроллер обработки ЗАДАЧ исполнителей
    [Authorize]
    public class User_StageController : RepositoryController
    {
        public User_StageController(IRepository repository) : base(repository) { } // Конструктор

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

        // GET: User_Stage/Tasks
        // принимает id стадии
        public async Task<ActionResult> Tasks()
        {
            var tasks = await Rep.GetMyCurrentTasks(User.Identity.Name);

            return PartialView(tasks);
        }

        // GET: User_Stage/InWork
        // принимает id стадии
        public async Task<ActionResult> InWork()
        {
            var tasks = await Rep.GetMyCurrentTasks(User.Identity.Name);
            return PartialView("List", tasks);
        }
        // GET: User_Stage/Done
        // принимает id стадии
        public async Task<ActionResult> Done()
        {
            var tasks = await Rep.GetMyDoneTasks(User.Identity.Name);
            return PartialView("List", tasks);
        }

        // POST: User_Stage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User_Stage user_Stage, [Bind(Include = "workFile")]HttpPostedFileBase workFile)
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
            return PartialView("ListItemEdit", model);
        }
        // POST: User_Stage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User_Stage user_Stage, [Bind(Include = "workFile")] HttpPostedFileBase workFile)
        {
            if (workFile != null && workFile.ContentLength != 0)
                user_Stage.DocFileId = (await Helper.SetFile(workFile, Rep, Server)).Id;

            if (ModelState.IsValid)
            {
                await Rep.PutUser_Stage(user_Stage);
                //todo
                var taskList = await Rep.GetMyCurrentTasks(User.Identity.Name);
                return PartialView("List", taskList);
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

        // POST: User_Stage/DeleteFromTasks/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteFromTasks(int id)
        {
            await Rep.DeleteUser_Stage(id);
            return RedirectToAction("Tasks");
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

        public async Task<ActionResult> Finalise(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User_Stage user_Stage = await Rep.GetUser_Stage(id.Value);

            user_Stage.Stage.Document.FinalFileId = user_Stage.DocFileId;
            await Rep.PutDocument(user_Stage.Stage.Document);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
