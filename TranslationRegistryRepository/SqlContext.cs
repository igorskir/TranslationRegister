using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationRegistryModel;

namespace SqlRepository
{
    public class SqlContext : DbContext
    {
        public SqlContext() : base(){ }
        public SqlContext(string connStr) : base(connStr){ }

        public DbSet<User> Users { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Project> Projects { get; set; }

    }
}
