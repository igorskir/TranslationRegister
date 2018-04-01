using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TranslationReg.Models;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    // Контроллер обработки ЯЗЫКОВ
    [Authorize]
    public class LanguagesController : RepositoryController
    {
        public LanguagesController(IRepository repository) : base(repository) { } // Конструктор

        // GET: Languages/Cards            списки карточек языков и языковых пар
        public async Task<ActionResult> Cards()
        {
            return PartialView(await LanguagesModel.GetModel(Rep));
        }

        // GET: Languages/Card
        public async Task<ActionResult> Card(int id)
        {
            return PartialView(await Rep.GetLanguage(id));
        }

        // GET: Languages/AddCard
        public ActionResult AddCard()
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

        // GET: Languages/EditCard/5
        public async Task<ActionResult> EditCard(int? id)
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
    }
}
