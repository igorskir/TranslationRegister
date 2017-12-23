using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Models
{
    public class DocumentModel
    {
        public Document document;
        public SelectList projects;

        public static async Task<DocumentModel> GetModel(IRepository rep)
        {
            DocumentModel model = new DocumentModel();
            model.document = new Document();
            model.projects = new SelectList(await rep.GetProjects(), "Id", "Name");

            return model;
        }
    }
}