using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationReg.Models;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    public class AnalyticsController : RepositoryController
    {
        public AnalyticsController(IRepository repository) : base(repository) { }

        // GET: Analytics
        public async Task<ActionResult> Index()
        {
            return View("Filters", await AnaliticsFiltersModel.GetModel(Rep));
        }
    }
}