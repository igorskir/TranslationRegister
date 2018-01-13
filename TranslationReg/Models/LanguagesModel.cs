using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Models
{
    public class LanguagesModel
    {
        public List<Language> languages;
        public List<LanguagePair> languagePairs;

        public static async Task<LanguagesModel> GetModel(IRepository rep)
        {
            LanguagesModel model = new LanguagesModel();
            model.languages = await rep.GetLanguages();
            model.languagePairs = await rep.GetLanguagePairs();
            return model;
        }
    }
}