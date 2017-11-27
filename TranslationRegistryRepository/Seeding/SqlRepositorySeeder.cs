using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TranslationRegistryModel;

namespace SqlRepository
{
    public class SqlRepositorySeeder
    {
        /*
        public static void Initialize(SqlContext context)
        {
            context.Database.EnsureCreated();
            if (context.Languages.Count() == 0)
            {
                var languages = new List<Language> {
                    new Language { Name = "English", ShortName = "en"},
                    new Language { Name = "Русский", ShortName = "ru"},
                };
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    foreach (var language in languages)
                    {
                        context.Languages.Add(language);
                    }
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
            }

            if (context.Users.Count() == 0)
            {
                var users = new List<User> {
                    new User { Name = "admin", Password = "1"},
                    new User { Name = "user", Password = "1"},
                };
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    foreach (var user in users)
                    {
                        context.Users.Add(user);
                    }
                    context.SaveChanges();
                    dbContextTransaction.Commit();
                }
            }
        }
        */
    }
}