using System.Threading.Tasks;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    // Контроллер для сквозного поиска
    [Authorize]
    public class SearchController : RepositoryController
    {
        public SearchController(IRepository repository) : base(repository) { } // Конструктор

        public async Task<ActionResult> SearchByName(string searchToken)
        {
            if (!string.IsNullOrWhiteSpace(searchToken))
            {
                var model = await Models.SearchDocsAndProjectsModel.GetModel(Rep, searchToken);
                return PartialView("SearchResults", model);
            }
            return PartialView("SearchResults", null);
        }
    }
}