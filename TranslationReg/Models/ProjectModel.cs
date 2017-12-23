using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TranslationRegistryModel;

namespace TranslationReg.Models
{
    public class ProjectModel
    {
        public Project Project;
        public SelectList LanguagePairs;
        public SelectList ProjectStatus;

        public static async Task<ProjectModel> GetModel(IRepository rep)
        {
            ProjectModel model = new ProjectModel();
            model.Project = new Project();
            model.LanguagePairs = new SelectList(await rep.GetLanguagePairs(), "Id", "Name");
            model.ProjectStatus = new SelectList(await rep.GetProjectStatuses(), "Id", "Name");

            return model;
        }
    }
}