using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Models
{
    public class SearchDocsAndProjectsModel
    {
        public string searchToken;
        public List<Document> Documents;
        public List<Project> Projects;

        public static async Task<SearchDocsAndProjectsModel> GetModel(IRepository rep, string searchStr)
        {
            SearchDocsAndProjectsModel model = new SearchDocsAndProjectsModel
            {
                Projects = await rep.GetProjectsByStr(searchStr),
                Documents = await rep.GetDocumentsByStr(searchStr),
                searchToken = searchStr
            };

            return model;
        }
    }
}