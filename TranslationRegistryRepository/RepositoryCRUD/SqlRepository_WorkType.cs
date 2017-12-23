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
        public async Task<WorkType> GetWorkType(int id)
        {
            return await context.WorkTypes.FindAsync(id);
        }

        public async Task<List<WorkType>> GetWorkTypes()
        {
            return await context.WorkTypes.ToListAsync();
        }

        public async Task PutWorkType(WorkType workType)
        {
            context.Entry(workType).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { }
        }

        public async Task AddWorkType(WorkType workType)
        {
            context.WorkTypes.Add(workType);
            await context.SaveChangesAsync();
        }

        public async Task DeleteWorkType(int id)
        {
            var workType = await GetWorkType(id);
            if (workType == null)
                return;

            context.WorkTypes.Remove(workType);
            await context.SaveChangesAsync();
        }
    }
}
