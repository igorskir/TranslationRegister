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
using TranslationReg.Models;

namespace TranslationReg.Controllers
{
    [Authorize]
    public class LanguagesController : Controller
    {
        public IRepository Rep { get; set; }
        public LanguagesController(IRepository repository)
        {
            this.Rep = repository;
        }

        // GET: Languages & LanguagePairs
        public async Task<ActionResult> Index()
        {
            return View(await LanguagesModel.GetModel(Rep));
        }

        // GET: Languages
        public async Task<ActionResult> Item(int id)
        {
            return PartialView(await Rep.GetLanguage(id));
        }

        // GET: Languages
        public async Task<ActionResult> List()
        {
            return PartialView(await Rep.GetLanguages());
        }

        // GET: Languages/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Languages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Language language)
        {
            if (ModelState.IsValid)
            {
                await Rep.AddLanguage(language);
                return Redirect(Request.UrlReferrer.ToString());
            }

            return View(language);
        }

        // GET: Languages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Language language = await Rep.GetLanguage(id.Value);

            if (language == null)
                return HttpNotFound();

            return PartialView(language);
        }

        // POST: Languages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Language language)
        {
            if (ModelState.IsValid)
            {
                await Rep.PutLanguage(language);
                return Redirect(Request.UrlReferrer.ToString());
            }
            return PartialView(language);
        }

        // GET: Languages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Language language = await Rep.GetLanguage(id.Value);

            if (language == null)
                return HttpNotFound();

            return PartialView(language);
        }

        // POST: Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await Rep.DeleteLanguage(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Rep.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
