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
        public void DeleteDocument(int id)
        {
            var doc = context.Documents.SingleOrDefault(m => m.Id == id);
            //todo log?
            if (doc == null)
                return;

            context.Documents.Remove(doc);
            context.SaveChanges();
        }

        public Document GetDocument(int id)
        {
            return context.Documents.SingleOrDefault(m => m.Id == id);
        }

        public List<Document> GetDocuments()
        {
            return context.Documents.ToList();
        }

        public void PutDocument(Document doc)
        {
            context.Entry(doc).State = EntityState.Modified;

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) { }
        }

        public void AddDocument(Document doc)
        {
            context.Documents.Add(doc);
            context.SaveChangesAsync();
        }

    }
}
