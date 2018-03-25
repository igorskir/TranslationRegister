using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    // Контроллер обработки ЯЗЫКОВЫХ ПАР
    [Authorize]
    public class LanguagePairsController : RepositoryController
    {
        public LanguagePairsController(IRepository repository) : base(repository) { } // Конструктор

        // GET: LanguagePairs/AddCard
        public async Task<ActionResult> AddCard()
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

        // GET: LanguagePairs/EditCard/5
        public async Task<ActionResult> EditCard(int? id)
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
