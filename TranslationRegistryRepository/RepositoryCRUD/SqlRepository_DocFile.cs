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
        public async Task<DocFile> GetDocFile(int id)
        {
            return await context.DocFiles.FindAsync(id);
        }

        public async Task<List<DocFile>> GetDocFiles()
        {
            return await context.DocFiles.ToListAsync();
        }

        public async Task PutDocFile(DocFile docFile)
        {
            context.Entry(docFile).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { }
        }

        public async Task AddDocFile(DocFile docFile)
        {
            context.DocFiles.Add(docFile);
            await context.SaveChangesAsync();
        }

        public async Task DeleteDocFile(int id)
        {
            var docFile = await GetDocFile(id);
            if (docFile == null)
                return;

            context.DocFiles.Remove(docFile);
            await context.SaveChangesAsync();
        }
    }
}
