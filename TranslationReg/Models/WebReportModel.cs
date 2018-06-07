using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TranslationReg.Models
{
    public class WebReportModel
    {
        public string User { get;set; }
        public string Work { get; set; }
        public int Count { get; set; }

        public string UnitOfMeasure { get; set; }
    }
}