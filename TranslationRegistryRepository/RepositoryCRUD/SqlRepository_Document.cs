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
        public async Task AddDocument(Document doc)
        {
            context.Documents.Add(doc);
            await context.SaveChangesAsync();
        }

        public async Task<Document> GetDocument(int id)
        {
            return await context.Documents.FindAsync(id);
        }

        public async Task<List<Document>> GetDocuments()
        {
            return await context.Documents.ToListAsync();
        }

        public async Task PutDocument(Document doc)
        {
            context.Entry(doc).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { }
        }

        public async Task DeleteDocument(int id)
        {
            var doc = await context.Documents.SingleOrDefaultAsync(m => m.Id == id);
            if (doc == null)
                return;

            context.Documents.Remove(doc);
            await context.SaveChangesAsync();
        }
    }
}
