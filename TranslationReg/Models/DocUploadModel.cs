using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Models
{
    public class DocUploadModel
    {
        public Document document;
        public SelectList languages;
        public SelectList projects;
    }
}