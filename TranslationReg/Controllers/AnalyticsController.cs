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
    [Authorize]
    public class AnalyticsController : RepositoryController
    {
        public AnalyticsController(IRepository repository) : base(repository) { }

        // GET: Analytics
        public async Task<ActionResult> Index()
        {
            return View("Report", await ReportModel.GetModel(Rep));
        }

        // POST: Analytics
        public async Task<ActionResult> FormReport([Bind(Prefix = "filters")]
            ChosenFilters filters)
        {
            return View("Report", await ReportModel.GetModel(Rep, filters));
        }
    }
}