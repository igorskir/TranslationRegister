using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
            // ------------------------------------ Языки ------------------------------------
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
            // ------------------------------------------------------------------------------


            // ------------------------------------ Языковые пары ---------------------------
            var LanguagePairName1 = $"{languageName1[0]} - {languageName2[0]}";
            var LanguagePairName2 = $"{languageName2[0]} - {languageName1[0]}";
            var language1 = context.Languages.Where(x => x.Name == languageName1).FirstOrDefault();
            var language2 = context.Languages.Where(x => x.Name == languageName2).FirstOrDefault();
            List<LanguagePair> languagePairs = new List<LanguagePair>() {
                new LanguagePair(){Name = LanguagePairName1, OriginalLanguageId = language1.Id, TranslationLanguageId = language2.Id},
                new LanguagePair(){Name = LanguagePairName2, OriginalLanguageId = language2.Id, TranslationLanguageId = language1.Id }};
            context.LanguagePairs.AddRange(languagePairs);
            context.SaveChanges();
            // ------------------------------------------------------------------------------


            // ------------------------------------ Единицы измерения -----------------------
            var UnitOfMeasureName1 = "слова";
            var UnitOfMeasureName2 = "часы";
            List<UnitOfMeasure> units = new List<UnitOfMeasure>() {
                new UnitOfMeasure(){Name = UnitOfMeasureName1 },
                new UnitOfMeasure(){Name = UnitOfMeasureName2}};
            context.UnitOfMeasures.AddRange(units);
            context.SaveChanges();
            // ------------------------------------------------------------------------------


            // ------------------------------------ Типы работ ------------------------------
            var workTypeName1 = "Перевод";
            var workTypeName2 = "Вычитка";
            var workTypeName3 = "Корректировка";
            List<WorkType> worktypes = new List<WorkType>() {
                new WorkType(){Name = workTypeName1, UnitOfMeasureId =
                    context.UnitOfMeasures.Where(x=>x.Name == UnitOfMeasureName1).FirstOrDefault().Id},
                new WorkType(){Name = workTypeName2, UnitOfMeasureId =
                    context.UnitOfMeasures.Where(x=>x.Name == UnitOfMeasureName2).FirstOrDefault().Id},
                new WorkType(){Name = workTypeName3, UnitOfMeasureId =
                    context.UnitOfMeasures.Where(x=>x.Name == UnitOfMeasureName2).FirstOrDefault().Id}
            };
            context.WorkTypes.AddRange(worktypes);
            context.SaveChanges();
            // ------------------------------------------------------------------------------


            // ------------------------------------ Пользователи ----------------------------
            if (context.Users.Count() < 2)
            {
                List<User> users = new List<User>()
                {
                    new User() { Name = "Галина", Password = "1" },
                    new User() { Name = "Марина", Password = "1" }
                };
                context.Users.AddRange(users);
            }
            context.SaveChanges();
            // ------------------------------------------------------------------------------


            // ------------------------------------ Проекты ---------------------------------
            var projectName1 = "Перевод презентации";
            var projectName2 = "Перевод гранта";
            var projectName3 = "Перевод сообщения";
            var projectName4 = "Перевод научной статьи \"Working with Entity Framework\"";
            var projectWords1 = 1500;
            var projectWords2 = 2400;
            var projectWords3 = 234;
            var projectWords4 = 963;
            var projectCustomer = "ТПУ";
            var user1 = context.Users.ToArray()[0];
            var user2 = context.Users.ToArray()[1];
            var pair1 = context.LanguagePairs.ToArray()[0];
            var pair2 = context.LanguagePairs.ToArray()[1];
            List<Project> projects = new List<Project>() {
                new Project { Name = projectName1, CreatorId = user1.Id, LanguagePairId = pair1.Id,
                    WordsNumber = projectWords1,  Customer = projectCustomer},
                new Project { Name = projectName2, CreatorId = user2.Id, LanguagePairId = pair2.Id,
                    WordsNumber = projectWords2,  Customer = projectCustomer},
                new Project { Name = projectName3, CreatorId = user1.Id, LanguagePairId = pair1.Id,
                    WordsNumber = projectWords3,  Customer = projectCustomer},
                new Project { Name = projectName4, CreatorId = user2.Id, LanguagePairId = pair2.Id,
                    WordsNumber = projectWords4,  Customer = projectCustomer}
            };
            context.Projects.AddRange(projects);
            context.SaveChanges();
            // ------------------------------------------------------------------------------


            // ------------------------------------ Файлы -----------------------------------
            DateTime[] dates = new DateTime[8];
            Random r = new Random();
            for (int i = 0; i < dates.Length; i++)
                dates[i] = DateTime.Now - TimeSpan.FromHours(r.NextDouble() * 200 + 5);

            var uploadsDir = @"D:\RegTrans\TranslationReg\Uploads";

            string[] filepaths = new string[8];
            for (int i = 0; i < filepaths.Length; i++)
            {
                filepaths[i] = Path.Combine(uploadsDir, $"{i + 1}.txt");
                File.WriteAllText(filepaths[i], "Тестовый документ!");
            }

            DocFile[] files = new DocFile[8];
            for (int i = 0; i < files.Length; i++)
                files[i] = new DocFile() { Date = dates[i], Path = filepaths[i] };

            context.DocFiles.AddRange(files);
            context.SaveChanges();
            // ------------------------------------------------------------------------------


            // ------------------------------------ Документы -------------------------------
            string[] docNames = new string[]
            {
                "Текст слайдов",
                "Текст речи",
                "Текст раздаточного материала",
                "Текст гранта",
                "Текст сообщения",
                "Текст ответа",
                "Текст статьи",
                "Текст дополнений"
            };


            //var projectWords1 = 1500;
            //var projectWords2 = 2400;
            //var projectWords3 = 234;
            //var projectWords4 = 963;
            List<Document> docs = new List<Document>()
            {
                new Document(){  Name = docNames[0] , OriginalFile = files[0], OwnerId = user1.Id, Project = projects[0],  WordsNumber = 1000 },
                new Document(){  Name = docNames[1] , OriginalFile = files[1], OwnerId = user1.Id, Project = projects[0],  WordsNumber = 500 },
                new Document(){  Name = docNames[2] , OriginalFile = files[2], OwnerId = user1.Id, Project = projects[1],  WordsNumber = 1000 },
                new Document(){  Name = docNames[3] , OriginalFile = files[3], OwnerId = user1.Id, Project = projects[1],  WordsNumber = 1400 },
                new Document(){  Name = docNames[4] , OriginalFile = files[4], OwnerId = user2.Id, Project = projects[2],  WordsNumber = 100 },
                new Document(){  Name = docNames[5] , OriginalFile = files[5], OwnerId = user2.Id, Project = projects[2],  WordsNumber = 134 },
                new Document(){  Name = docNames[6] , OriginalFile = files[6], OwnerId = user2.Id, Project = projects[3],  WordsNumber = 300 },
                new Document(){  Name = docNames[7] , OriginalFile = files[7], OwnerId = user2.Id, Project = projects[3],  WordsNumber = 663 },

            };
            context.Documents.AddRange(docs);
            context.SaveChanges();
            // ------------------------------------------------------------------------------
        }
    }
}
