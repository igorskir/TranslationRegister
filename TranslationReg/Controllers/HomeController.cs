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
            if (Request.IsAjaxRequest())
            return PartialView();
            return View();
        }

    }
}