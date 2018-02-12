using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationRegistryModel;

namespace SqlRepository
{
    public partial class SqlRep : IRepository
    {
        private readonly Entities context;

        public SqlRep()
        {
            context = new Entities();
            //флаг пустой базы
            if (context.Languages.Count() == 0)
            {
                Seeding.Seeder.Seed(context);
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
