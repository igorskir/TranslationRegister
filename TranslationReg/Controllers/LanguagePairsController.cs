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
    public class LanguagePairsController : Controller
    {
        IRepository Rep;

        public LanguagePairsController(IRepository Rep)
        {
            this.Rep = Rep;
        }

        // GET: LanguagePairs/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.OriginalLanguageId = new SelectList(await Rep.GetLanguages(), "Id", "Name");
            ViewBag.TranslationLanguageId = new SelectList(await Rep.GetLanguages(), "Id", "Name");

            return View();
        }

        // POST: LanguagePairs/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,OriginalLanguageId,TranslationLanguageId")] LanguagePair languagePair)
        {
            if (ModelState.IsValid)
            {
                await Rep.AddLanguagePair(languagePair);
                return RedirectToAction("Index", "Languages");
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
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,OriginalLanguageId,TranslationLanguageId")] LanguagePair languagePair)
        {
            if (ModelState.IsValid)
            {
                await Rep.PutLanguagePair(languagePair);
                return RedirectToAction("Index", "Languages");
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
            return RedirectToAction("Index", "Languages");

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
