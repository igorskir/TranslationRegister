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
        public async Task<ProjectStatus> GetProjectStatus(int id)
        {
            return await context.ProjectStatuses.FindAsync(id);
        }

        public async Task<List<ProjectStatus>> GetProjectStatuses()
        {
            return await context.ProjectStatuses.ToListAsync();
        }

        public async Task PutProjectStatus(ProjectStatus language)
        {
            context.Entry(language).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { }
        }

        public async Task AddProjectStatus(ProjectStatus language)
        {
            context.ProjectStatuses.Add(language);
            await context.SaveChangesAsync();
        }

        public async Task DeleteProjectStatus(int id)
        {
            var language = await GetProjectStatus(id);
            if (language == null)
                return;

            context.ProjectStatuses.Remove(language);
            await context.SaveChangesAsync();
        }
    }
}
