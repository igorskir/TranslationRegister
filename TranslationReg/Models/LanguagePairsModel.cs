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
        public SelectList originalLanguageSelectlist;
        public SelectList translationLanguageSelectlist;

        public static async Task<LanguagePairsModel> GetModel(IRepository Rep, LanguagePair languagePair = null)
        {
            if (languagePair == null)
                languagePair = new LanguagePair();
            var languages = await Rep.GetLanguages();
            LanguagePairsModel model = new LanguagePairsModel
            {
                languagePair = languagePair,
                originalLanguageSelectlist = new SelectList(languages, "Id", "Name", languagePair.OriginalLanguage.Id),
                translationLanguageSelectlist = new SelectList(languages, "Id", "Name", languagePair.TranslationLanguage.Id)
            };
            return model;
        }
    }
}