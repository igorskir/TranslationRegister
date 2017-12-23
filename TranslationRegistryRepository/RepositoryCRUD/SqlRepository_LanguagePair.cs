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
        public async Task<LanguagePair> GetLanguagePair(int id)
        {
            return await context.LanguagePairs.FindAsync(id);
        }

        public async Task<List<LanguagePair>> GetLanguagePairs()
        {
            return await context.LanguagePairs.ToListAsync();
        }

        public async Task PutLanguagePair(LanguagePair languagePair)
        {
            context.Entry(languagePair).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { }
        }

        public async Task AddLanguagePair(LanguagePair languagePair)
        {
            context.LanguagePairs.Add(languagePair);
            await context.SaveChangesAsync();
        }

        public async Task DeleteLanguagePair(int id)
        {
            var languagePair = await GetLanguagePair(id);
            if (languagePair == null)
                return;

            context.LanguagePairs.Remove(languagePair);
            await context.SaveChangesAsync();
        }
    }
}
