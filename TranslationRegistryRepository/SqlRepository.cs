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
        private readonly SqlContext context;

        public SqlRep()
        {
            this.context = new SqlContext();
        }

        public SqlRep(string connStr)
        {
            this.context = new SqlContext(connStr);
        }
    }
}
