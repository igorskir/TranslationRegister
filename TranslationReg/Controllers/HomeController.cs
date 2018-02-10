using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    [AllowAnonymous]
    public class HomeController : RepositoryController
    {
        public HomeController(IRepository repository) : base(repository) { }

        public ActionResult Index()
        {
            return RedirectToAction("Index","Projects");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Реестр переводов";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Контакты";
            return View();
        }
    }
}