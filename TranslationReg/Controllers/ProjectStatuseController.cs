using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    // Контроллер обработки СТАТУСОВ ПРОЕКТА
    [Authorize]
    public class ProjectStatuseController : RepositoryController
    {
        public ProjectStatuseController(IRepository repository): base (repository){ } // Конструктор

        // GET: ProjectStatuse/Card
        public async Task<ActionResult> Card(int id)
        {
            return PartialView(await Rep.GetProjectStatus(id));
        }
        // GET: ProjectStatuse/AddCard
        public ActionResult AddCard()
        {
            return PartialView();
        }
        // POST: ProjectStatuse/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description")] ProjectStatus projectStatus)
        {
            if (ModelState.IsValid)
            {
                await Rep.AddProjectStatus(projectStatus);
                return Redirect(Request.UrlReferrer.ToString());
            }

            return View(projectStatus);
        }

        // GET: ProjectStatuse/EditCard
        public async Task<ActionResult> EditCard(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ProjectStatus projectStatus = await Rep.GetProjectStatus(id.Value);

            if (projectStatus == null)
                return HttpNotFound();

            return PartialView(projectStatus);
        }
        // POST: ProjectStatuse/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description")] ProjectStatus projectStatus)
        {
            if (ModelState.IsValid)
            {
                await Rep.PutProjectStatus(projectStatus);
                return Redirect(Request.UrlReferrer.ToString());
            }
            return PartialView(projectStatus);
        }

        // GET: ProjectStatuse/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ProjectStatus projectStatus = await Rep.GetProjectStatus(id.Value);

            if (projectStatus == null)
                return HttpNotFound();

            return PartialView(projectStatus);
        }
        // POST: ProjectStatuse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await Rep.DeleteProjectStatus(id);
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
