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
    [Authorize]
    public class LanguagePairsController : Controller
    {
        IRepository Rep;

        public LanguagePairsController(IRepository Rep)
        {
            this.Rep = Rep;
        }

        // GET: LanguagePairs/List
        public async Task<ActionResult> List()
        {
            return PartialView(await Rep.GetLanguagePairs());
        }

        // GET: LanguagePairs/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.OriginalLanguageId = new SelectList(await Rep.GetLanguages(), "Id", "Name");
            ViewBag.TranslationLanguageId = new SelectList(await Rep.GetLanguages(), "Id", "Name");

            return PartialView();
        }

        // POST: LanguagePairs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(LanguagePair languagePair)
        {
            if (ModelState.IsValid)
            {
                await Rep.AddLanguagePair(languagePair);
                return Redirect(Request.UrlReferrer.ToString());
            }

            ViewBag.OriginalLanguageId = new SelectList(await Rep.GetLanguages(), "Id", "Name", languagePair.OriginalLanguageId);
            ViewBag.TranslationLanguageId = new SelectList(await Rep.GetLanguages(), "Id", "Name", languagePair.TranslationLanguageId);
            return View(languagePair);
        }

        // GET: LanguagePairs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LanguagePair languagePair = await Rep.GetLanguagePair(id.Value);
            if (languagePair == null)
            {
                return HttpNotFound();
            }
            ViewBag.OriginalLanguageId = new SelectList(await Rep.GetLanguages(), "Id", "Name", languagePair.OriginalLanguageId);
            ViewBag.TranslationLanguageId = new SelectList(await Rep.GetLanguages(), "Id", "Name", languagePair.TranslationLanguageId);
            return PartialView(languagePair);
        }

        // POST: LanguagePairs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LanguagePair languagePair)
        {
            if (ModelState.IsValid)
            {
                await Rep.PutLanguagePair(languagePair);
                return Redirect(Request.UrlReferrer.ToString());
            }
            ViewBag.OriginalLanguageId = new SelectList(await Rep.GetLanguages(), "Id", "Name", languagePair.OriginalLanguageId);
            ViewBag.TranslationLanguageId = new SelectList(await Rep.GetLanguages(), "Id", "Name", languagePair.TranslationLanguageId);
            return View(languagePair);
        }

        // GET: LanguagePairs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LanguagePair languagePair = await Rep.GetLanguagePair(id.Value);
            if (languagePair == null)
            {
                return HttpNotFound();
            }
            return View(languagePair);
        }

        // POST: LanguagePairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await Rep.DeleteLanguagePair(id);
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
