using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    // Контроллер для страничек, не работающих с БД
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Реестр переводов";
            return View();
        }

    }
}