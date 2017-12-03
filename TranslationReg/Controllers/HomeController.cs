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
        public IRepository rep { get; set; }
        public HomeController(IRepository repository)
        {
            this.rep = repository;
        }

        public async Task<ActionResult> Index()
        {
            var l = await rep.GetLanguages();
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