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
        public async Task AddProject(Project doc)
        {
            context.Projects.Add(doc);
            await context.SaveChangesAsync();
        }

        public async Task<Project> GetProject(int id)
        {
            return await context.Projects
                .Include(x => x.Documents)
                .Include(x => x.FinalLanguage)
                .Include(x => x.OriginalLanguage)
                .FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<List<Project>> GetProjects()
        {
            return await context.Projects.Include(x=>x.FinalLanguage).Include(x=>x.OriginalLanguage).ToListAsync();
        }

        public async Task PutProject(Project doc)
        {
            context.Entry(doc).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { }
        }

        public async Task DeleteProject(int id)
        {
            var doc = context.Projects.SingleOrDefault(m => m.Id == id);
            if (doc == null)
                return;

            context.Projects.Remove(doc);
            await context.SaveChangesAsync();
        }
    }
}
