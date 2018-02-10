using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Controllers
{
    public class RepositoryController : Controller
    {
        public IRepository Rep { get; set; }
        public RepositoryController(IRepository repository)
        {
            this.Rep = repository;
        }
    }
}