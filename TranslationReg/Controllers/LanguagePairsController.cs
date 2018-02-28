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
    public class LanguagePairsController : RepositoryController
    {
        public LanguagePairsController(IRepository repository) : base(repository) { }

        // GET: LanguagePairs/List
        public async Task<ActionResult> List()
        {
            return PartialView(await Rep.GetLanguagePairs());
        }

        // GET: LanguagePairs/Create
        public async Task<ActionResult> Create()
        {
            var model = await Models.LanguagePairsModel.GetModel(Rep);
            return PartialView(model);
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

            var model = await Models.LanguagePairsModel.GetModel(Rep, languagePair);
            return PartialView(model);
        }

        // GET: LanguagePairs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            LanguagePair languagePair = await Rep.GetLanguagePair(id.Value);

            if (languagePair == null)
                return HttpNotFound();

            var model = await Models.LanguagePairsModel.GetModel(Rep, languagePair);
            return PartialView(model);
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

            var model = await Models.LanguagePairsModel.GetModel(Rep, languagePair);
            return PartialView(model);
        }

        // GET: LanguagePairs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            LanguagePair languagePair = await Rep.GetLanguagePair(id.Value);

            if (languagePair == null)
                return HttpNotFound();

            return PartialView(languagePair);
        }

        // POST: LanguagePairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await Rep.DeleteLanguagePair(id);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
