using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Models
{
    public class LanguagePairsModel
    {
        public LanguagePair languagePair;
        public SelectList languageSelectlist;

        public static async Task<LanguagePairsModel> GetModel(IRepository Rep, LanguagePair languagePair = null)
        {
            if (languagePair == null)
                languagePair = new LanguagePair();
            LanguagePairsModel model = new LanguagePairsModel
            {
                languagePair = languagePair,
                languageSelectlist = new SelectList(await Rep.GetLanguages(), "Id", "Name")
            };
            return model;
        }
    }
}