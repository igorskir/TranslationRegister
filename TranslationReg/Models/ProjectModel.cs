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

        public static async Task<ProjectModel> GetModel(IRepository rep, Project project = null)
        {
            if (project == null)
                project = new Project();

            ProjectModel model = new ProjectModel
            {
                Project = project,
                LanguagePairs = new SelectList(await rep.GetLanguagePairs(), "Id", "Name", project.LanguagePairId),
                ProjectStatus = new SelectList(await rep.GetProjectStatuses(), "Id", "Name", project.ProjectStatuseId)
            };
            return model;
        }
    }
}