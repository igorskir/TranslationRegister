using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    public class HomeController : Controller
    {
        public IRepository repository { get; set; }
        public HomeController(IRepository repository)
        {
            this.repository = repository;
        }

        public HomeController(){}

        public async Task<ActionResult> Index()
        {
            var l = await repository.GetLanguages();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}