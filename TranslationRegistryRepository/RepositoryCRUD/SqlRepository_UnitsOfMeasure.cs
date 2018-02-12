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
        public async Task<UnitOfMeasure> GetUnitOfMeasure(int id)
        {
            return await context.UnitOfMeasures.FindAsync(id);
        }

        public async Task<List<UnitOfMeasure>> GetUnitsOfMeasure()
        {
            return await context.UnitOfMeasures.ToListAsync();
        }

        public async Task PutUnitOfMeasure(UnitOfMeasure UnitOfMeasure)
        {
            context.Entry(UnitOfMeasure).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) { }
        }

        public async Task AddUnitOfMeasure(UnitOfMeasure UnitOfMeasure)
        {
            context.UnitOfMeasures.Add(UnitOfMeasure);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUnitOfMeasure(int id)
        {
            var UnitOfMeasure = await GetUnitOfMeasure(id);
            if (UnitOfMeasure == null)
                return;

            context.UnitOfMeasures.Remove(UnitOfMeasure);
            await context.SaveChangesAsync();
        }
    }
}
