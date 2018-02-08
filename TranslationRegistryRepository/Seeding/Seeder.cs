using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationRegistryModel;

namespace SqlRepository.Seeding
{
    public static class Seeder
    {
        public static void Seed(Entities context)
        {
            if (context.Languages.Count() == 0)
            {
                var languageName1 = "Русский";
                var languageShortName1 = "ru";
                var languageName2 = "Английский";
                var languageShortName2 = "en";
                var languageName3 = "Немецкий";
                var languageShortName3 = "de";
                var languageName4 = "Французский";
                var languageShortName4 = "fr";

                List<Language> languages = new List<Language>() {
                    new Language(){Name = languageName1, ShortName = languageShortName1 },
                    new Language(){Name = languageName2, ShortName = languageShortName2 },
                    new Language(){Name = languageName3, ShortName = languageShortName3 },
                    new Language(){Name = languageName4, ShortName = languageShortName4 }};
                context.Languages.AddRange(languages);
                context.SaveChanges();

                var LanguagePairName1 = $"{languageName1[0]} - {languageName2[0]}";
                var LanguagePairName2 = $"{languageName2[0]} - {languageName1[0]}";
                var language1 = context.Languages.Where(x => x.Name == languageName1).FirstOrDefault();
                var language2 = context.Languages.Where(x => x.Name == languageName2).FirstOrDefault();
                List<LanguagePair> languagePairs = new List<LanguagePair>() {
                    new LanguagePair(){Name = LanguagePairName1, OriginalLanguageId = language1.Id, TranslationLanguageId = language2.Id},
                    new LanguagePair(){Name = LanguagePairName2, OriginalLanguageId = language2.Id, TranslationLanguageId = language1.Id }};
                context.LanguagePairs.AddRange(languagePairs);
                context.SaveChanges();

                var UnitOfMeasureName1 = "слова";
                var UnitOfMeasureName2 = "часы";
                List<UnitOfMeasure> units = new List<UnitOfMeasure>() {
                    new UnitOfMeasure(){Name = UnitOfMeasureName1 },
                    new UnitOfMeasure(){Name = UnitOfMeasureName2}};
                context.UnitOfMeasures.AddRange(units);
                context.SaveChanges();

                var workTypeName1 = "Перевод";
                var workTypeName2 = "Вычитка";
                var workTypeName3 = "Корректировка";
                List<WorkType> wortypes = new List<WorkType>() {
                    new WorkType(){Name = workTypeName1, UnitOfMeasureId = context.UnitOfMeasures.Where(x=>x.Name == UnitOfMeasureName1).FirstOrDefault().Id},
                    new WorkType(){Name = workTypeName2, UnitOfMeasureId = context.UnitOfMeasures.Where(x=>x.Name == UnitOfMeasureName2).FirstOrDefault().Id},
                    new WorkType(){Name = workTypeName3, UnitOfMeasureId = context.UnitOfMeasures.Where(x=>x.Name == UnitOfMeasureName2).FirstOrDefault().Id}};

                if (context.Users.Count() == 0)
                {
                    var newUser = new User() { Name = "1", Password = "1" };
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }

                var projectName1 = "Перевод презентации";
                var projectWords1 = 1500;
                var projectCustomer = "ТПУ";
                var user = context.Users.FirstOrDefault();
                var pair = context.LanguagePairs.FirstOrDefault();
                List<Project> projects = new List<Project>() {
                    new Project { Name = projectName1, CreatorId = user.Id, LanguagePairId = pair.Id,  WordsNumber = projectWords1,  Customer = projectCustomer}};
                context.Projects.AddRange(projects);

                context.SaveChanges();
            }
        }
    }
}
