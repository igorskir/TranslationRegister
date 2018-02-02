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

namespace TranslationReg.Controllers
{
    public class User_StageController : Controller
    {
        private Entities db = new Entities();

        // GET: User_Stage
        public async Task<ActionResult> Index()
        {
            var user_Stage = db.User_Stage.Include(u => u.Stage).Include(u => u.User).Include(u => u.DocFile);
            return View(await user_Stage.ToListAsync());
        }

        // GET: User_Stage/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Stage user_Stage = await db.User_Stage.FindAsync(id);
            if (user_Stage == null)
            {
                return HttpNotFound();
            }
            return View(user_Stage);
        }

        // GET: User_Stage/Create
        public ActionResult Create()
        {
            ViewBag.StageId = new SelectList(db.DocStages, "Id", "Id");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name");
            ViewBag.DocFileId = new SelectList(db.DocFiles, "Id", "Path");
            return View();
        }

        // POST: User_Stage/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Amount,StageId,UserId,DocFileId")] User_Stage user_Stage)
        {
            if (ModelState.IsValid)
            {
                db.User_Stage.Add(user_Stage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StageId = new SelectList(db.DocStages, "Id", "Id", user_Stage.StageId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", user_Stage.UserId);
            ViewBag.DocFileId = new SelectList(db.DocFiles, "Id", "Path", user_Stage.DocFileId);
            return View(user_Stage);
        }

        // GET: User_Stage/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Stage user_Stage = await db.User_Stage.FindAsync(id);
            if (user_Stage == null)
            {
                return HttpNotFound();
            }
            ViewBag.StageId = new SelectList(db.DocStages, "Id", "Id", user_Stage.StageId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", user_Stage.UserId);
            ViewBag.DocFileId = new SelectList(db.DocFiles, "Id", "Path", user_Stage.DocFileId);
            return View(user_Stage);
        }

        // POST: User_Stage/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Amount,StageId,UserId,DocFileId")] User_Stage user_Stage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_Stage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.StageId = new SelectList(db.DocStages, "Id", "Id", user_Stage.StageId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Name", user_Stage.UserId);
            ViewBag.DocFileId = new SelectList(db.DocFiles, "Id", "Path", user_Stage.DocFileId);
            return View(user_Stage);
        }

        // GET: User_Stage/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User_Stage user_Stage = await db.User_Stage.FindAsync(id);
            if (user_Stage == null)
            {
                return HttpNotFound();
            }
            return View(user_Stage);
        }

        // POST: User_Stage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User_Stage user_Stage = await db.User_Stage.FindAsync(id);
            db.User_Stage.Remove(user_Stage);
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
