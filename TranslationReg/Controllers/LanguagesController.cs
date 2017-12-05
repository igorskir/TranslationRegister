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
    public class LanguagesController : Controller
    {
        public IRepository rep { get; set; }
        public LanguagesController(IRepository repository)
        {
            this.rep = repository;
        }

        // GET: Languages
        public async Task<ActionResult> Index()
        {
            return View(await rep.GetLanguages());
        }

        // GET: Languages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Languages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Language language)
        {
            if (ModelState.IsValid)
            {
                await rep.AddLanguage(language);
                return RedirectToAction("Index");
            }

            return View(language);
        }

        // GET: Languages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Language language = await rep.GetLanguage(id.Value);

            if (language == null)
                return HttpNotFound();

            return View(language);
        }

        // POST: Languages/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Language language)
        {
            if (ModelState.IsValid)
            {
                await rep.PutLanguage(language);
                return RedirectToAction("Index");
            }
            return View(language);
        }

        // GET: Languages/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Language language = await rep.GetLanguage(id.Value);

            if (language == null)
                return HttpNotFound();

            return View(language);
        }

        // POST: Languages/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await rep.DeleteLanguage(id);
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
