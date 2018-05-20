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
        public async Task<List<Deadline>> GetDeadlines()
        {
            return await context.Deadlines.ToListAsync();
        }

        public async Task<Deadline> GetDeadline(int id)
        {
            return await context.Deadlines.FindAsync(id);
        }
        public async Task<Deadline> GetDeadline(int projectId, int workTypeId)
        {
            try
            {
                return await context.Deadlines.Where(x => x.ProjectId == projectId && x.WorkTypeId == workTypeId).FirstOrDefaultAsync();
            }
            catch(Exception) { return null; }
             
        }
        public async Task AddDeadline(Deadline deadline)
        {
            context.Deadlines.Add(deadline);
            await context.SaveChangesAsync(); 
        }
        public async Task PutDeadline(Deadline deadline)
        {
            context.Entry(deadline).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { }
        }
        public async Task DeleteDeadline(int id)
        {
            var d = await GetDeadline(id);
            if (d == null)
                return;

            context.Deadlines.Remove(d);
            await context.SaveChangesAsync();
        }
    }


}
