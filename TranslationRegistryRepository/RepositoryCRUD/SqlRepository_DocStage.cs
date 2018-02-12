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
        public async Task<DocStage> GetDocStage(int id)
        {
            return await context.DocStages.FindAsync(id);
        }

        public async Task<List<DocStage>> GetDocStages()
        {
            return await context.DocStages.ToListAsync();
        }

        public async Task PutDocStage(DocStage docStage)
        {
            context.Entry(docStage).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { }
        }

        public async Task AddDocStage(DocStage docStage)
        {
            context.DocStages.Add(docStage);
            await context.SaveChangesAsync();
        }

        public async Task DeleteDocStage(int id)
        {
            var DocStage = await GetDocStage(id);
            if (DocStage == null)
                return;

            context.DocStages.Remove(DocStage);
            await context.SaveChangesAsync();
        }
    }
}
