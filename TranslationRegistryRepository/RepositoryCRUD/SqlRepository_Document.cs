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
        public async Task<List<Document>> GetMyDocuments(string login)
        {
            var userId = (await context.Users.FirstOrDefaultAsync(x => x.Name == login)).Id;
            return await context.Documents
                .Where(x => x.Owner.Id == userId)
                .ToListAsync();
        }

        public async Task<List<Document>> GetDocumentsByStr(string searchToken)
        {
            return await context.Documents.
                OrderBy(x => x.Project.ProjectStatuseId).
                ThenByDescending(x => x.Date).
                Where(x => x.Name.Contains(searchToken) ||
                    // todo заменить на формат
                    x.OriginalFile.Path.Contains(searchToken)).
                ToListAsync();
        }

        public async Task<List<Document>> GetMyWorkDocuments(string login)
        {
            var userId = (await context.Users.FirstOrDefaultAsync(x => x.Name == login)).Id;
            return await context.Documents.Where(x => x.Stages.Any(y => y.User_Stage.Any(z => z.UserId == userId))).ToListAsync();
        }
        public async Task<Document> GetDocument(int id)
        {
            return await context.Documents.FindAsync(id);
        }

        public async Task<List<Document>> GetDocuments()
        {
            return await context.Documents.OrderByDescending(x=>x.Date).ToListAsync();
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
            var doc = context.Documents.SingleOrDefault(m => m.Id == id);
            if (doc == null)
                return;

            context.Documents.Remove(doc);
            await context.SaveChangesAsync();
        }
    }
}
