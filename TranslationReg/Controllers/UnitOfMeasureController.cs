using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    // Контроллер обработки ЕДНИЦ ИЗМЕРЕНИЯ объема работ
    [Authorize]
    public class UnitOfMeasureController : RepositoryController
    {
        public UnitOfMeasureController(IRepository repository) : base(repository) { } // Конструктор

        // GET: UnitOfMeasure/Card
        public async Task<ActionResult> Card(int id)
        {
            return PartialView(await Rep.GetUnitOfMeasure(id));
        }

        // GET: UnitOfMeasure/AddCard
        public ActionResult AddCard()
        {
            return PartialView();
        }
        // POST: UnitOfMeasure/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UnitOfMeasure unitOfMeasure)
        {
            if (ModelState.IsValid)
            {
                await Rep.AddUnitOfMeasure(unitOfMeasure);
                return Redirect(Request.UrlReferrer.ToString());
            }

            return View(unitOfMeasure);
        }

        // GET: UnitOfMeasure/EditCard/5
        public async Task<ActionResult> EditCard(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            UnitOfMeasure unitOfMeasure = await Rep.GetUnitOfMeasure(id.Value);

            if (unitOfMeasure == null)
                return HttpNotFound();

            return PartialView(unitOfMeasure);
        }
        // POST: UnitOfMeasure/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit( UnitOfMeasure unitOfMeasure)
        {
            if (ModelState.IsValid)
            {
                await Rep.PutUnitOfMeasure(unitOfMeasure);
                return Redirect(Request.UrlReferrer.ToString());
            }
            return View(unitOfMeasure);
        }

        // GET: UnitOfMeasure/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            UnitOfMeasure unitOfMeasure = await Rep.GetUnitOfMeasure(id.Value);

            if (unitOfMeasure == null)
                return HttpNotFound();

            return PartialView(unitOfMeasure);
        }
        // POST: UnitOfMeasure/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await Rep.DeleteUnitOfMeasure(id);
            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}
