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
            var languages = await Rep.GetLanguages();

            if (languagePair == null)
                languagePair = new LanguagePair
                {
                    OriginalLanguageId = languages[0].Id,
                    TranslationLanguageId = languages[1].Id

                };

            LanguagePairsModel model = new LanguagePairsModel
            {
                languagePair = languagePair,
                originalLanguageSelectlist = new SelectList(languages, "Id", "Name", languagePair.OriginalLanguageId),
                translationLanguageSelectlist = new SelectList(languages, "Id", "Name", languagePair.TranslationLanguageId)
            };
            return model;
        }
    }
}