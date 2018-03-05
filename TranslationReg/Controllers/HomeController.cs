using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    
    public class HomeController : RepositoryController
    {
        public HomeController(IRepository repository) : base(repository) { }

        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("Index","Projects");
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Реестр переводов";
            return View();
        }
    }
}