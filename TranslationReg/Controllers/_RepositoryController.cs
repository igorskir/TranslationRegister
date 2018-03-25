using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    // Контроллер получает ИНТЕРФЕЙС для РАБОТЫ С РЕПОЗИТОРИЕМ. Базовый класс для контроллеров, работающих с БД
    public abstract class RepositoryController : Controller
    {
        public IRepository Rep { get; set; }
        public RepositoryController(IRepository repository)
        {
            this.Rep = repository;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Rep.Dispose();
            base.Dispose(disposing);
        }
    }
}