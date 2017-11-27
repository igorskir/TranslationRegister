using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationRegistryModel;

namespace SqlRepository
{
    public partial class SqlRep : IRepository
    {

        public async Task DeleteLanguage(int id)
        {
            var language = await context.Languages.SingleOrDefaultAsync(m => m.Id == id);
            //todo log?
            if (language == null)
                return;

            context.Languages.Remove(language);
            await context.SaveChangesAsync();
        }

        public Task<Language> GetLanguage(int id)
        {
            return context.Languages.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Language>> GetLanguages()
        {
            return await context.Languages.ToListAsync();
        }

        public async Task PutLanguage(Language language)
        {
            context.Entry(language).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { }
        }

        public async Task AddLanguage(Language language)
        {
            context.Languages.Add(language);
            await context.SaveChangesAsync();
        }
    }
}
