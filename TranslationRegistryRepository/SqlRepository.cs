﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslationRegistryModel;

namespace SqlRepository
{
    public partial class SqlRep : IRepository
    {
        //private readonly SqlContext context;
        private readonly Entities context;

        public SqlRep()
        {
            this.context = new Entities();
            if (context.Languages.Count() == 0)
            {
                context.Languages.Add(new Language() {Name = "Русский", ShortName = "ru"});
                context.SaveChanges();
            }
        }

        //public SqlRep(string connStr)
        //{
        //    this.context = new SqlContext(connStr);
        //}

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
